IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERTCIVILTRANSPORTVEHICLE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERTCIVILTRANSPORTVEHICLE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

        
  /*----------------------------------------------------------                  
PROC NAME       : DBO.POL_CIVIL_TRANSPORT_VEHICLES                  
CREATED BY      : PRADEEP KUSHWAHA         
DATE            : 23/04/2010                  
PURPOSE   :TO INSERT RECORDS IN POL_CIVIL_TRANSPORT_VEHICLES TABLE.                  
REVISON HISTORY :                  
USED IN   : EBIX ADVANTAGE                  
------------------------------------------------------------                  
DATE     REVIEW BY          COMMENTS                  
------   ------------       -------------------------*/                  
--DROP PROC DBO.PROC_INSERTCIVILTRANSPORTVEHICLE           
/****** SCRIPT FOR PROC_INSERTCIVILTRANSPORTVEHICLE INTO DATABASE  ******/        
     
         
               
CREATE PROC [dbo].[PROC_INSERTCIVILTRANSPORTVEHICLE]         
(              
 @CUSTOMER_ID INT,        
 @POLICY_ID INT,        
 @POLICY_VERSION_ID SMALLINT,        
 @VEHICLE_ID INT OUT,        
 @CLIENT_ORDER NUMERIC,        
 @VEHICLE_NUMBER NUMERIC,        
 @MANUFACTURED_YEAR NUMERIC,        
 @FIPE_CODE NVARCHAR(20),        
 @CATEGORY INT =NULL,        
 @CAPACITY NVARCHAR(6)=NULL,        
 @MAKE_MODEL NVARCHAR(100)=NULL,        
 @LICENSE_PLATE NVARCHAR(14)=NULL,        
 @CHASSIS NVARCHAR(50)=NULL,        
 @MANDATORY_DEDUCTIBLE NUMERIC=NULL,        
 @FACULTATIVE_DEDUCTIBLE NUMERIC=NULL,        
 @SUB_BRANCH INT =NULL,        
 @RISK_EFFECTIVE_DATE DATETIME,        
 @RISK_EXPIRE_DATE DATETIME,        
 @REGION INT =NULL,        
 @COV_GROUP_CODE NVARCHAR(30)=NULL,        
 @FINANCE_ADJUSTMENT NVARCHAR(100)=NULL,        
 @REFERENCE_PROPOSASL NVARCHAR(100)=NULL,        
 @REMARKS NVARCHAR(4000)=NULL,        
 @IS_ACTIVE NCHAR(1),        
 @CREATED_BY INT,        
 @CREATED_DATETIME DATETIME,  
 @CO_APPLICANT_ID INT ,
 @ZIP_CODE NVARCHAR(10)=NULL,   
 @CALLED_FROM NVARCHAR(50)=NULL,
 @ORIGINAL_VERSION_ID INT=NULL,
 @EXCEEDED_PREMIUM INT = NULL 
)                  
AS                  
BEGIN        
   
 DECLARE @MAKEMODEL NVARCHAR(100)    
 DECLARE @FLAG BIT   
 DECLARE @LOOUP_UNIQUE_ID INT
   
EXEC PROC_CHECK_RISK_EFFDATE_AND_RISK_EXPDATE @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@RISK_EFFECTIVE_DATE,@RISK_EXPIRE_DATE,@FLAG OUT  
    
IF(@FLAG=1)  
 BEGIN  
 IF(@CALLED_FROM <> 'AERO')
 BEGIN
	 IF (@MAKE_MODEL='' )   
	  BEGIN  
	   EXEC Proc_GetMakeModelData @FIPE_CODE, @MAKEMODEL OUT ,@LOOUP_UNIQUE_ID OUT,@CAPACITY OUT--Get the MakeModel using Fipe Code    
		IF (@MAKEMODEL<>'')  
		 BEGIN   
		  SELECT @MAKE_MODEL=@MAKEMODEL    
		  SELECT @CATEGORY =@LOOUP_UNIQUE_ID  
		   SELECT @CAPACITY =@CAPACITY  
		 END  
	  END  
 END
 IF(@MAKE_MODEL<>'' or @CALLED_FROM = 'AERO')  
  BEGIN      
   SELECT  @VEHICLE_ID=ISNULL(MAX(VEHICLE_ID),0)+1 FROM POL_CIVIL_TRANSPORT_VEHICLES WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID        
     
   INSERT INTO POL_CIVIL_TRANSPORT_VEHICLES                 
      (                  
               
        CUSTOMER_ID,        
        POLICY_ID,        
        POLICY_VERSION_ID ,        
        VEHICLE_ID ,        
        CLIENT_ORDER,        
        VEHICLE_NUMBER,        
        MANUFACTURED_YEAR ,        
        FIPE_CODE,        
        CATEGORY ,        
        CAPACITY ,        
        MAKE_MODEL ,        
        LICENSE_PLATE ,        
        CHASSIS ,        
        MANDATORY_DEDUCTIBLE ,        
        FACULTATIVE_DEDUCTIBLE ,        
        SUB_BRANCH ,        
        RISK_EFFECTIVE_DATE ,        
        RISK_EXPIRE_DATE ,        
        REGION ,        
        COV_GROUP_CODE,        
        FINANCE_ADJUSTMENT ,        
        REFERENCE_PROPOSASL,        
        REMARKS,        
        IS_ACTIVE,        
        CREATED_BY,        
        CREATED_DATETIME ,
        CO_APPLICANT_ID,
        ZIP_CODE ,
        ORIGINAL_VERSION_ID ,    
        EXCEEDED_PREMIUM       
               
      )                  
      VALUES                  
      (                  
        @CUSTOMER_ID ,        
        @POLICY_ID ,        
        @POLICY_VERSION_ID  ,        
        @VEHICLE_ID  ,        
        @CLIENT_ORDER  ,        
        @VEHICLE_NUMBER  ,        
        @MANUFACTURED_YEAR  ,        
        @FIPE_CODE  ,        
        @CATEGORY  ,        
        @CAPACITY  ,        
        @MAKE_MODEL  ,        
        @LICENSE_PLATE  ,        
        @CHASSIS  ,        
        @MANDATORY_DEDUCTIBLE ,        
        @FACULTATIVE_DEDUCTIBLE  ,        
        @SUB_BRANCH  ,                @RISK_EFFECTIVE_DATE ,        
        @RISK_EXPIRE_DATE,        
        @REGION,        
        @COV_GROUP_CODE ,        
        @FINANCE_ADJUSTMENT,        
        @REFERENCE_PROPOSASL,        
        @REMARKS,        
        @IS_ACTIVE,        
        @CREATED_BY,        
        @CREATED_DATETIME ,
        @CO_APPLICANT_ID ,
        @ZIP_CODE ,
        @ORIGINAL_VERSION_ID,
		@EXCEEDED_PREMIUM     
      )    
      --ADDED BY PRAVESH -ON 10 maY 2010- SAVE DEFAULT cOVERAGES  
      EXEC Proc_SaveProductDefaultCoverages        
       @CUSTOMER_ID ,  
       @POLICY_ID  ,  
       @POLICY_VERSION_ID,  
       @VEHICLE_ID ,  
       @CREATED_BY   
    
    RETURN 1  
   END  
 ELSE  
   BEGIN  
  RETURN -2  
   END  
 END  
   
ELSE  
 BEGIN  
  RETURN -3 --If the Risk Effective date and risk expiry date is not match with Applciation Effective date and expiry date.  
 END  
END 
GO

