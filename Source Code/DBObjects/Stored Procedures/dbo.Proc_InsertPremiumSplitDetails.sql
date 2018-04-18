IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPremiumSplitDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPremiumSplitDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_InsertPremiumSplitDetails            
Created by      : Deepak Gupta                     
Date            : 09-26-2006                                     
Purpose         : To Insert Premium Split Details            
Revison History :                            
modified by	: pravesh K. Cahndel
modified Date	: 7 dec 2007
purpose		: add more fields and Parameters in CLT_PREMIUM_SPLIT_DETAILS table

modified by	: pravesh K. Cahndel
modified Date	: 23 MAY 2008
purpose		: add more fields EPR MONTH AND EPR YEAR and Parameters in CLT_PREMIUM_SPLIT_DETAILS table

Used In         : Wolverine                            
                           
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--drop proc DBO.Proc_InsertPremiumSplitDetails                
create PROC [dbo].[Proc_InsertPremiumSplitDetails]            
(                            
 @SPLIT_UNIQUE_ID numeric,            
 @COMPONENT_TYPE nvarchar(10),            
 @COMPONENT_CODE nvarchar(200),            
 @LIMIT nvarchar(50),            
 @DEDUCTIBLE nvarchar(50),            
 @PREMIUM  nvarchar(50),            
 @REMARKS  nvarchar(1000),            
 @EXCEPTION_XML text,            
 @COMP_ACT_PREMIUM nvarchar(50)=NULL,            
 @COMP_REMARKS nvarchar(500)=NULL,            
 @COMP_EXT  nvarchar(20)=NULL,            
 @COM_EXT_AD  nvarchar(20)=NULL ,
 @WRITTEN_PREM   nvarchar(50)=NULL ,
 @CHANGE_INFORCE_PREM      nvarchar(50)=NULL   
)                           
AS            
DECLARE @MAXSPLITID INT            
DECLARE @EXCEPTIONEXISTS INT            
DECLARE @EXCEPTIONMSG NVARCHAR(2000)            

BEGIN             
DECLARE @COMP_ACT_PREM	DECIMAL(25,4),            
		@COMP_EXT_ID			INT,
		@WRITTEN_PREMIUM		DECIMAL(25,4) ,
		@CHANGE_INFORCE_PREMIUM DECIMAL(25,4)

SET @COMP_ACT_PREM		=0.00
SET @COMP_EXT_ID		=0
SET @WRITTEN_PREMIUM	=0.00
SET @CHANGE_INFORCE_PREMIUM	=0.00
            
 SELECT @EXCEPTIONEXISTS = 0;            
            
 IF EXISTS (SELECT 1 FROM CLT_PREMIUM_SPLIT_DETAILS WHERE SPLIT_UNIQUE_ID = @SPLIT_UNIQUE_ID AND COMPONENT_CODE = @COMPONENT_CODE)            
  SELECT @EXCEPTIONEXISTS = 1;            
 ELSE IF @COMPONENT_TYPE = ''            
  SELECT @EXCEPTIONEXISTS = 2;            
 ELSE IF (@PREMIUM = '' and @COMPONENT_TYPE = '' ) OR @PREMIUM = '0' OR @PREMIUM = '0.00'     
  SELECT @EXCEPTIONEXISTS = 3;            
 ELSE IF @COMPONENT_CODE = ''            
  SELECT @EXCEPTIONEXISTS = 4;            
 --added by pravesh on may 13 2008. to add EPR_MONTH and EPR_YEAR Split Details
DECLARE @CUSTOMER_ID		INT,
		@POLICY_ID			SMALLINT,
		@POLICY_VERSION_ID  INT,
		@PROCESS_TYPE		VARCHAR(4),
		@PROCESS_EFF_DATE	DATETIME,
		@EPR_MONTH			SMALLINT,
		@EPR_YEAR			SMALLINT

SELECT	@CUSTOMER_ID		=CUSTOMER_ID,
		@POLICY_ID			=POLICY_ID,
		@POLICY_VERSION_ID	=POLICY_VERSION_ID ,
		@PROCESS_TYPE		=PROCESS_TYPE
		FROM CLT_PREMIUM_SPLIT WITH(NOLOCK) 
		WHERE UNIQUE_ID=@SPLIT_UNIQUE_ID
IF (ISNULL(@POLICY_ID,0)!=0 AND ISNULL(@POLICY_VERSION_ID,0)!=0)
  BEGIN
	SELECT @PROCESS_EFF_DATE=EFFECTIVE_DATETIME 
			FROM POL_POLICY_PROCESS WITH(NOLOCK) 
			WHERE CUSTOMER_ID			=@CUSTOMER_ID
			AND   POLICY_ID				=@POLICY_ID
			AND   NEW_POLICY_VERSION_ID	=@POLICY_VERSION_ID
			AND   PROCESS_STATUS IN ('PENDING','COMPLETE')

	IF (DATEDIFF(DD,@PROCESS_EFF_DATE,GETDATE())<0 ) -- IF EFFETIVE DATE IN FUTURE 
	BEGIN
		SET @EPR_MONTH=MONTH(@PROCESS_EFF_DATE)
		SET @EPR_YEAR=YEAR(@PROCESS_EFF_DATE)
	END
	ELSE
	BEGIN
		SET @EPR_MONTH=MONTH(GETDATE())
		SET @EPR_YEAR=YEAR(GETDATE())
	END
 END
ELSE
BEGIN
		SET @EPR_MONTH=NULL
		SET @EPR_YEAR=NULL
END
-----end here      
set @COMP_ACT_PREMIUM		= replace(@COMP_ACT_PREMIUM,',','')
set @WRITTEN_PREM			= replace(@WRITTEN_PREM,',','')
set @CHANGE_INFORCE_PREM    = replace(@CHANGE_INFORCE_PREM,',','')

IF(ISNUMERIC(@COMP_ACT_PREMIUM)=1)
   SET @COMP_ACT_PREM	= CAST(@COMP_ACT_PREMIUM AS DECIMAL(25,4))
IF(ISNUMERIC(@COMP_EXT)=1)
   SET @COMP_EXT_ID	= CAST(@COMP_EXT AS INT)
IF(ISNUMERIC(@WRITTEN_PREM)=1)
   SET @WRITTEN_PREMIUM	= CAST(@WRITTEN_PREM AS DECIMAL(25,4))
IF(ISNUMERIC(@CHANGE_INFORCE_PREM)=1)
   SET @CHANGE_INFORCE_PREMIUM	= CAST(@CHANGE_INFORCE_PREM AS DECIMAL(25,4))
 -- Added by Nidhi Apr 23 '07.. As discussed with Rajan - if deductible is zero, save blank to avoid zero in the reports.  
 IF (ltrim(rtrim(@DEDUCTIBLE)) = '0')  
 SET @DEDUCTIBLE=''  
     
 IF @EXCEPTIONEXISTS = 0            
 BEGIN            
  SELECT @MAXSPLITID = ISNULL(MAX(SPLIT_ID),0)+1 FROM CLT_PREMIUM_SPLIT_DETAILS WHERE SPLIT_UNIQUE_ID = @SPLIT_UNIQUE_ID            
  INSERT INTO CLT_PREMIUM_SPLIT_DETAILS ( SPLIT_UNIQUE_ID,            
       SPLIT_ID,            
       COMPONENT_TYPE,            
       COMPONENT_CODE,            
       LIMIT,            
       DEDUCTIBLE,            
       PREMIUM,            
       REMARKS,
       COMP_ACT_PREMIUM,
       COMP_REMARKS,
       COMP_EXT,
       COM_EXT_AD,
       WRITTEN_PREM,
       CHANGE_INFORCE_PREM,
	   EPR_MONTH,
	   EPR_YEAR		
	)             
      VALUES            
      ( @SPLIT_UNIQUE_ID,            
       @MAXSPLITID,            
       @COMPONENT_TYPE,            
       @COMPONENT_CODE,            
       replace(@LIMIT,',',''),            
       replace(@DEDUCTIBLE,',',''),            
       replace(@PREMIUM,',',''),             
       @REMARKS,
       @COMP_ACT_PREM,
       @COMP_REMARKS,
       @COMP_EXT_ID,
       @COM_EXT_AD,
       @WRITTEN_PREMIUM,
       @CHANGE_INFORCE_PREMIUM,
	   @EPR_MONTH,
	   @EPR_YEAR	
);            
 END            
    
 ELSE IF @EXCEPTIONEXISTS <> 0            
 BEGIN            
  IF @EXCEPTIONEXISTS = 1            
   SELECT @EXCEPTIONMSG = 'DUPLICATE PREMIUM EXISTS FOR COMPONENT CODE''' + @COMPONENT_CODE + '''';            
  ELSE IF @EXCEPTIONEXISTS = 2            
   SELECT @EXCEPTIONMSG = 'COMPONENT TYPE IS BLANK';            
  ELSE IF @EXCEPTIONEXISTS = 3            
   SELECT @EXCEPTIONMSG = 'STEP PREMIUM IS BLANK OR 0';            
  ELSE IF @EXCEPTIONEXISTS = 4            
   SELECT @EXCEPTIONMSG = 'COMPONENT CODE IS BLANK';            
            
  SELECT @MAXSPLITID = ISNULL(MAX(EXCEPTION_ID),0)+1 FROM CLT_PREMIUM_SPLIT_EXCEPTIONS WHERE SPLIT_UNIQUE_ID = @SPLIT_UNIQUE_ID            
  INSERT INTO CLT_PREMIUM_SPLIT_EXCEPTIONS ( SPLIT_UNIQUE_ID,            
        EXCEPTION_ID,            
        EXCEPTION_DESC,            
        EXCEPTION_XML)             
       VALUES            
       ( @SPLIT_UNIQUE_ID,            
        @MAXSPLITID,            
        @EXCEPTIONMSG,            
        @EXCEPTION_XML);            
 END            
END            
            
            




GO

