IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GenerateAppPolNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GenerateAppPolNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name   : dbo.Proc_GenerateAppNumber        
Created by          : Pravesh k Chandel      
Date                : 11 May 2010      
Purpose             : Generate the Application number  in EbixAdvantage      
Revison History :        
------------------------------------------------------------        
Date     Review By          Comments        
DROP PROC [Proc_GenerateAppPolNumber] 19,290,'2010-6-18','APP'      
    
Proc_GenerateAppPolNumber 9,314,'04/21/2011','POL' ,14549     
------   ------------       -------------------------*/        
CREATE proc  [dbo].[Proc_GenerateAppPolNumber]      
(      
 @POLICY_LOB int  ,      
 @DIV_ID int  ,      
 @ISSUE_DATE DATETIME  ,    
 @CALLED_FROM NVARCHAR(10) = NULL  ,  
 @CO_INSURANCE INT = NULL  ,
 @CUSTOMER_ID INT = NULL ,
 @POLICY_ID INT =   NULL,
 @POLICY_VERSION_ID INT = NULL
)      
AS      
BEGIN      
      
DECLARE @CARRIER_SUSEP_CODE NVARCHAR(5),      
  @ISSUE_YEAR VARCHAR(4),      
  @BRANCH_CODE NVARCHAR(2),      
  @SUSEP_LOB_CODE NVARCHAR(4),      
  @APP_NUMBER_SQUENCE VARCHAR(6),      
  @POL_APP_NUMBER VARCHAR(21) ,    
  @APPLICATIONNUMBER VARCHAR(21)    ,  
  @CO_INSURANCE_FOLLOWER  INT= 14549  

DECLARE @COMMITTED_DATE DATE=GETDATE();

SET @CARRIER_SUSEP_CODE =''    
if (@POLICY_LOB=22 and @CALLED_FROM<>'APP') -- changed by Pravesh on 4 feb Itrack 401    
   set @POLICY_LOB = 21    
--FETCHING CARRIER SUSEP CODE    
SELECT @CARRIER_SUSEP_CODE=LEFT(ISNULL(SUSEP_NUM,''),5)     
FROM MNT_SYSTEM_PARAMS MNT WITH(NOLOCK)    
Join MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK)    
ON REIN.REIN_COMAPANY_ID=MNT.SYS_CARRIER_ID    
    
SET @ISSUE_YEAR = CONVERT(VARCHAR,YEAR(getdate()))      
      
select @BRANCH_CODE=BRANCH_CODE from MNT_DIV_LIST WITH(NOLOCK) WHERE DIV_ID=@DIV_ID      
--ADDED by Lalit For itack 1493

	 IF EXISTS(SELECT * FROM MNT_LOB_SUSEPCODE_MASTER WITH(NOLOCK) WHERE LOB_ID =  @POLICY_LOB  AND EFFECTIVE_FROM <= @COMMITTED_DATE AND  EFFECTIVE_TO >= @COMMITTED_DATE )  
		 SELECT  @SUSEP_LOB_CODE = SUSEP_LOB_CODE  FROM MNT_LOB_SUSEPCODE_MASTER WITH(NOLOCK) WHERE LOB_ID = @POLICY_LOB AND EFFECTIVE_FROM <= @COMMITTED_DATE AND  EFFECTIVE_TO >= @COMMITTED_DATE  
	 ELSE  
		 SELECT  @SUSEP_LOB_CODE = SUSEP_LOB_CODE  FROM MNT_LOB_MASTER WITH(NOLOCK) WHERE LOB_ID = CASE WHEN @POLICY_LOB=22 THEN 21 ELSE @POLICY_LOB END 

	/* UPDATE POL_CUSTOMER_POLICY_LIST SET SUSEP_LOB_CODE = @SUSEP_LOB_CODE
	 WHERE CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID
	 */
	/*IF EXISTS(SELECT SUSEP_LOB_CODE FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID AND SUSEP_LOB_CODE IS NOT NULL)
		select @SUSEP_LOB_CODE = SUSEP_LOB_CODE FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID 
	ELSE 
		select @SUSEP_LOB_CODE=SUSEP_LOB_CODE from MNT_LOB_MASTER WITH(NOLOCK) WHERE LOB_ID=@POLICY_LOB      */
      
SET @POL_APP_NUMBER = @CARRIER_SUSEP_CODE + @ISSUE_YEAR +  LEFT(@BRANCH_CODE,2) + LEFT(@SUSEP_LOB_CODE ,4)      
      
--If the LOB has pOLICY      
if exists (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)      
   WHERE POLICY_LOB=@POLICY_LOB      
   AND substring(CASE @CALLED_FROM WHEN 'APP' THEN APP_NUMBER ELSE POLICY_NUMBER END,6,4)= @ISSUE_YEAR    
  )          
BEGIN          
--   08737-2009-01-0196-000001 -aPP nUMBER FORMAT      
 /*  
 Added By lalit April 21, 2010  
 if policy accepted co-insurance(follwer) then 1st digit will be '9' in 6 digit squence no  
 and policy squence no will be follow follower pollicy no   
 */  
  IF(@CO_INSURANCE <> @CO_INSURANCE_FOLLOWER OR @CO_INSURANCE IS NULL)  
   BEGIN  
    SELECT   @APP_NUMBER_SQUENCE = RIGHT('000000'+  cast(ISNULL(MAX(CAST(substring(    
    CASE @CALLED_FROM  WHEN 'APP'     
    THEN APP_NUMBER ELSE 
    CASE WHEN SUBSTRING(SUBSTRING(POLICY_NUMBER,16,6),1,1) <> '9'
    THEN POLICY_NUMBER 
    ELSE '000000' END
    END,16,6) AS INT)),0)+1 as varchar(7)),6)      
    FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)      
    WHERE POL_CUSTOMER_POLICY_LIST.POLICY_LOB = @POLICY_LOB      
    --AND substring(app_number,6,4)=@ISSUE_YEAR      
    AND substring(CASE @CALLED_FROM WHEN 'APP' THEN APP_NUMBER ELSE POLICY_NUMBER END,6,4)= @ISSUE_YEAR    
   END  
 ELSE  
  BEGIN  
     SELECT   @APP_NUMBER_SQUENCE = RIGHT('000000'+  cast(ISNULL(MAX(CAST(substring(    
     CASE @CALLED_FROM  WHEN 'APP'     
     THEN APP_NUMBER ELSE   
     CASE WHEN SUBSTRING(SUBSTRING(POLICY_NUMBER,16,6),1,1) = '9'  
      THEN POLICY_NUMBER   
      ELSE '000000' END  
     END,16,6) AS INT)),0)+1 as varchar(7)),6)      
     FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)      
     WHERE POL_CUSTOMER_POLICY_LIST.POLICY_LOB = @POLICY_LOB      
     AND CO_INSURANCE =  @CO_INSURANCE_FOLLOWER  
     AND substring(CASE @CALLED_FROM WHEN 'APP' THEN APP_NUMBER ELSE POLICY_NUMBER END,6,4)= @ISSUE_YEAR      
  END  
      
END      
ELSE      
BEGIN       
 SELECT   @APP_NUMBER_SQUENCE =  SUBSTRING(CAST( ISNULL(MNT_LOB_MASTER.LOB_SEED,0) AS VARCHAR(7)),2,6)       
 FROM     MNT_LOB_MASTER WITH(NOLOCK)           
 WHERE LOB_ID =@POLICY_LOB       
 END        
       
 --SELECT @POL_APP_NUMBER + @APP_NUMBER_SQUENCE AS APPLICATIONNUMBER      
 /*if policy accepted co-insurance then 1st digit will be '9' in 6 digit squence no  
 */  
 IF(@CO_INSURANCE = @CO_INSURANCE_FOLLOWER and LEN(@APP_NUMBER_SQUENCE)=6 )  
 BEGIN   
 IF (SUBSTRING(@APP_NUMBER_SQUENCE,1,1) <> '9' )  
  SELECT @APP_NUMBER_SQUENCE = '9'+ SUBSTRING(@APP_NUMBER_SQUENCE,2,5)  
 END  
   
 SET @APPLICATIONNUMBER = @POL_APP_NUMBER + @APP_NUMBER_SQUENCE     
 declare @found bit    
 set @found=0    
while(@found=0)      
begin      
 if not exists(select POLICY_NUMBER from pol_customer_policy_list with(nolock) where POLICY_NUMBER=@APPLICATIONNUMBER)     
    and     
    not exists(select APP_NUMBER from pol_customer_policy_list with(nolock) where APP_NUMBER=@APPLICATIONNUMBER)    
    begin    
    set @found=1     
 end      
 else      
  begin      
   set @APP_NUMBER_SQUENCE = replace(str(@APP_NUMBER_SQUENCE +1,6),' ','0')     
   set @APPLICATIONNUMBER = @POL_APP_NUMBER + @APP_NUMBER_SQUENCE     
  end      
      
end     
    
SELECT @APPLICATIONNUMBER AS APPLICATIONNUMBER    ,@SUSEP_LOB_CODE AS SUSEP_LOB_CODE      
        
END     
GO

