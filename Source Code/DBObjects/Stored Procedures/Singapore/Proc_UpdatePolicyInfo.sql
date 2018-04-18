 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyInfo]]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyInfo]]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO         
/*----------------------------------------------------------                              
PROC NAME       : DBO.Proc_UpdatePolicyInfo                          
CREATED BY      : VIJAY ARORA                          
DATE            : 27/10/2005                              
PURPOSE        : UPDATE THE POLICY DATA.                                       
                      
MODIFIED BY : SHAFI                            
MODIFIED ON : 01/01/06                            
PURPOSE     : ADD POLICY TYPE                                   
                    
      
MODIFIED BY : SHAFI                            
MODIFIED ON : 09/02/06                            
PURPOSE     : ADD PROPERTY INSPECTION CREDIT APPLIES FILED TYPE                    
                  
MODIFIED BY : PRAVESH CHANDEL                  
MODIFIED ON : 05 DEC 2006                            
PURPOSE     : ADD NEW PARAMS DOWN_PAY_MODE FOR SAME FIELDS                  
DROP PROC DBO.PROC_UPDATEPOLICYINFO                  
MODIFIED BY : PRAVESH CHANDEL                  
MODIFIED ON : 24 JULY 2007                            
PURPOSE     : ADD NEW PARAMS @REFERAL_INSTRUCTIONS FOR SAME NEW FIELDS                  
MODIFIED BY : PRAVESH CHANDEL                  
MODIFIED ON : 10 AUG 2007                            
PURPOSE     : ADD NEW PARAMS @REINS_SPECIAL_ACPT FOR SAME NEW FIELDS                  
                
MODIFIED BY : PRAVESH K CHANDEL                  
MODIFIED ON : 31 DEC 2008                            
PURPOSE     : ADD NEW PARAMS POLICY_SUBLOB ITRACK 5165                
                  
DROP PROC DBO.PROC_UPDATEPOLICYINFO                  
------------------------------------------------------------                              
DATE     REVIEW BY          COMMENTS                              
------   ------------       -------------------------*/                            
--DROP PROC Proc_UpdatePolicyInfo              
                  
CREATE PROC [dbo].[Proc_UpdatePolicyInfo]                            
@CUSTOMER_ID INT,                            
@POLICY_ID INT,                            
@POLICY_VERSION_ID SMALLINT,                            
@APP_TERMS NVARCHAR(5),                            
@APP_INCEPTION_DATE DATETIME,                            
@APP_EFFECTIVE_DATE DATETIME,                            
@APP_EXPIRATION_DATE DATETIME,                            
@BILL_TYPE_ID INTEGER,  
--Added by kuldeep on14_jan_2012
@DIV_ID int,
@DEPT_ID int,
@PC_ID int,       
--till here          
@PROXY_SIGN_OBTAINED INT = NULL,                            
@UNDERWRITER INT = NULL,                            
@INSTALL_PLAN_ID INT,                            
@CHARGE_OFF_PRMIUM NVARCHAR(5) = NULL,                            
@RECEIVED_PRMIUM DECIMAL(18,2) = NULL,                            
@COMPLETE_APP NCHAR(1) = NULL,                            
@YEAR_AT_CURR_RESI REAL = NULL,                            
@YEARS_AT_PREV_ADD NVARCHAR(250),                            
@MODIFIED_BY INT,                            
@LAST_UPDATED_DATETIME DATETIME,                            
@POLICY_TYPE NVARCHAR(15),                      
@PROPRTY_INSP_CREDIT  INT,                      
@RESULT INT OUTPUT,                      
@PIC_OF_LOC INT,                       
@IS_HOME_EMP   BIT = NULL,                      
 --ADDED BY PRAVESH                      
@DOWN_PAY_MODE INT=NULL,                    
@NOT_RENEW  CHAR(1)=NULL,                    
@NOT_RENEW_REASON  INT=NULL,                    
@REFER_UNDERWRITER NCHAR(1)=NULL,                    
@STATE_ID INT=NULL,                    
@AGENCY_ID INT=NULL,                    
@REFERAL_INSTRUCTIONS NVARCHAR(300)=NULL,                    
@REINS_SPECIAL_ACPT INT =NULL,                  
@CSR  INT =NULL,                  
@PRODUCER  INT=NULL,                  
@POLICY_SUBLOB  NVARCHAR(5)='0',                  
--END HERE               
--ADDED BY CHARLES ON 18-MAR-2010 FOR POLICY PAGE IMPLEMENTATION                 
@POLICY_CURRENCY INT,            
@PAYOR INT,            
@CO_INSURANCE INT,            
@POLICY_LEVEL_COMISSION DECIMAL(18,2) =NULL,            
@BILLTO NVARCHAR(50) =NULL,            
@CONTACT_PERSON INT =NULL,              
--ADDED TILL HERE             
--ADDED BY CHARLES ON 14-MAY-2010 FOR POLICY PAGE IMPLEMENTATION                 
 @POLICY_LEVEL_COMM_APPLIES NCHAR(1) =NULL,          
 @TRANSACTION_TYPE INT =NULL,          
 @PREFERENCE_DAY SMALLINT =NULL,          
 @BROKER_REQUEST_NO NVARCHAR(100) =NULL,          
 @BROKER_COMM_FIRST_INSTM NCHAR(1) =NULL ,         
 --@REMARKS NVARCHAR(4000) = NULL   ,        
 @POLICY_DESCRIPTION  NVARCHAR(4000) = NULL,                   
--ADDED TILL HERE    \      
 @BILLING_CURRENCY INT = NULL,        
 @FUND_TYPE INT = NULL           
AS                            
BEGIN                        
                      
                    
 DECLARE @BILL_MORTAGAGEE SMALLINT,          
 @AGENCY_BILL_MORTAGAGEE SMALLINT,          
 @INSURED_BILL_MORTAGAGEE SMALLINT,          
 @MORTAGAGEE_INCEPTION SMALLINT,            
 @OLD_APP_EFFECTIVE_DATE DATETIME,           
 @BILL_TYPE NVARCHAR(2),           
 @POLICY_LOB SMALLINT,             
 @POLICY_STATUS VARCHAR(15),            
  @APP_STATUS NVARCHAR(15),        
   @POL_STATUS NVARCHAR(15)                       
 SET @AGENCY_BILL_MORTAGAGEE = 11277                              
 SET @INSURED_BILL_MORTAGAGEE = 11278                              
 SET @MORTAGAGEE_INCEPTION = 11276          
                       
DECLARE @PRODUCT_SUSEP_NUMBER NVARCHAR(10)        
        
--ADDED BY CHARLES ON 21-SEP-09 FOR ITRACK 6323                   
SELECT @APP_STATUS=APP_STATUS,@POL_STATUS =POLICY_STATUS ,@OLD_APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE, @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                    
--ADDED TILL HERE               
                     
SELECT @BILL_TYPE  = TYPE FROM MNT_LOOKUP_VALUES WITH(NOLOCK) WHERE LOOKUP_UNIQUE_ID= @BILL_TYPE_ID             
        
  /*comment -- Logic shifted to NBS Commit Level      
--Added by Lalit for itrack # 1493.      
  --control product LOB data on Effective and Expire dates      
  --as per itrack # 1178 lob 22 and 21 susep code consider same.      
IF EXISTS(SELECT * FROM MNT_LOB_SUSEPCODE_MASTER WHERE LOB_ID = CASE WHEN @POLICY_LOB=22 THEN 21 ELSE @POLICY_LOB END AND EFFECTIVE_FROM <= @APP_EFFECTIVE_DATE AND  EFFECTIVE_TO >= @APP_EFFECTIVE_DATE )      
 SELECT  @PRODUCT_SUSEP_NUMBER = SUSEP_LOB_CODE  FROM MNT_LOB_SUSEPCODE_MASTER WHERE LOB_ID = CASE WHEN @POLICY_LOB=22 THEN 21 ELSE @POLICY_LOB END  AND EFFECTIVE_FROM <= @APP_EFFECTIVE_DATE AND  EFFECTIVE_TO >= @APP_EFFECTIVE_DATE  ---- added for itrack 
  
   
-1178      
ELSE      
 SELECT  @PRODUCT_SUSEP_NUMBER = SUSEP_LOB_CODE  FROM MNT_LOB_MASTER WHERE LOB_ID = CASE WHEN @POLICY_LOB=22 THEN 21 ELSE @POLICY_LOB END -- added for itrack-1178      
 */      
                     
UPDATE POL_CUSTOMER_POLICY_LIST                             
SET                            
 APP_TERMS  = @APP_TERMS ,                            
 APP_INCEPTION_DATE = @APP_INCEPTION_DATE,                            
  --If the APP_STATUS is APPLICATION then APP_EFFECTIVE_DATE will change --By Pradeep 0n 23-sep-2010          
 APP_EFFECTIVE_DATE = CASE           
  WHEN  LTRIM(RTRIM(@APP_STATUS))= 'APPLICATION' OR  LTRIM(RTRIM(@POL_STATUS)) ='URENEW' THEN                            
   @APP_EFFECTIVE_DATE ELSE APP_EFFECTIVE_DATE          
  END ,            
 APP_EXPIRATION_DATE =CASE           
  WHEN  LTRIM(RTRIM(@APP_STATUS)) = 'APPLICATION' OR  LTRIM(RTRIM(@POL_STATUS)) ='URENEW' THEN                            
  @APP_EXPIRATION_DATE ELSE APP_EXPIRATION_DATE          
 END ,                        
 BILL_TYPE_ID = @BILL_TYPE_ID,                            
 BILL_TYPE = @BILL_TYPE,
 DIV_ID=@DIV_ID,
 DEPT_ID=@DEPT_ID,
 PC_ID=@PC_ID,                            
 PROXY_SIGN_OBTAINED = @PROXY_SIGN_OBTAINED,                            
 UNDERWRITER =         
 CASE           
  WHEN  LTRIM(RTRIM(UPPER(@POL_STATUS))) ='URENEW' THEN UNDERWRITER ELSE @UNDERWRITER END        
 ,                           
 INSTALL_PLAN_ID = @INSTALL_PLAN_ID,                            
 CHARGE_OFF_PRMIUM = @CHARGE_OFF_PRMIUM,                            
 RECEIVED_PRMIUM = @RECEIVED_PRMIUM,                            
 COMPLETE_APP = @COMPLETE_APP,                            
 YEAR_AT_CURR_RESI = @YEAR_AT_CURR_RESI,                            
 YEARS_AT_PREV_ADD = @YEARS_AT_PREV_ADD,                          
 POLICY_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE ,                        
 POLICY_TYPE =@POLICY_TYPE,                      
 PROPRTY_INSP_CREDIT  =@PROPRTY_INSP_CREDIT,                      
PIC_OF_LOC = @PIC_OF_LOC,                      
 POL_VER_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE,                       
 POL_VER_EXPIRATION_DATE = @APP_EXPIRATION_DATE,                      
 --ADDED BY PRAVESH                      
 DOWN_PAY_MODE   = @DOWN_PAY_MODE,                    
 NOT_RENEW = @NOT_RENEW  ,                    
 NOT_RENEW_REASON =@NOT_RENEW_REASON  ,                    
 REFER_UNDERWRITER =@REFER_UNDERWRITER ,                    
 REFERAL_INSTRUCTIONS =@REFERAL_INSTRUCTIONS,                    
 REINS_SPECIAL_ACPT =@REINS_SPECIAL_ACPT,                    
--END HERE                    
 IS_HOME_EMP = CASE WHEN @IS_HOME_EMP IS NULL THEN IS_HOME_EMP ELSE @IS_HOME_EMP END ,                      
 POLICY_SUBLOB=@POLICY_SUBLOB,            
 --ADDED BY CHARLES ON 18-MAR-2010 FOR POLICY PAGE IMPLEMENTATION            
 POLICY_CURRENCY = @POLICY_CURRENCY,           
 PAYOR = @PAYOR,           
 CO_INSURANCE = @CO_INSURANCE,           
 POLICY_LEVEL_COMISSION = @POLICY_LEVEL_COMISSION,             
 BILLTO = @BILLTO,           
 CONTACT_PERSON = @CONTACT_PERSON,            
 --ADDED TILL HERE            
 --ADDED BY CHARLES ON 14-MAY-2010 FOR POLICY PAGE IMPLEMENTATION           
 POLICY_LEVEL_COMM_APPLIES = @POLICY_LEVEL_COMM_APPLIES ,         
 TRANSACTION_TYPE = @TRANSACTION_TYPE ,        
 PREFERENCE_DAY = @PREFERENCE_DAY ,          
 BROKER_REQUEST_NO = @BROKER_REQUEST_NO ,          
 BROKER_COMM_FIRST_INSTM = @BROKER_COMM_FIRST_INSTM ,         
 POLICY_DESCRIPTION = @POLICY_DESCRIPTION ,         
-- REMARKS = @REMARKS,         
 --ADDED TILL HERE            
 --Added By Lalit 08 March,2011        
 POLICY_EXPIRATION_DATE  = CASE           
 WHEN  LTRIM(RTRIM(@APP_STATUS)) = 'APPLICATION' OR  LTRIM(RTRIM(@POL_STATUS)) ='URENEW' THEN         
   @APP_EXPIRATION_DATE ELSE POLICY_EXPIRATION_DATE         
  END,        
  SUSEP_LOB_CODE = @PRODUCT_SUSEP_NUMBER  ,      
 BILLING_CURRENCY = @BILLING_CURRENCY,        
 FUND_TYPE = @FUND_TYPE      
 WHERE                             
 CUSTOMER_ID = @CUSTOMER_ID                            
 AND POLICY_ID = @POLICY_ID                            
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID                            
                            
SET @RESULT = 1                    
                  
--CHANGED BY PRAVESH ON 3RD APRIL 2008 BY PRAVESH AS NOW IT APPLICABLE ONLY FOR MORTEGAGEE SINCE INCEPTION                    
--IF (@BILL_TYPE_ID <> @AGENCY_BILL_MORTAGAGEE  AND @BILL_TYPE_ID <> @INSURED_BILL_MORTAGAGEE AND @BILL_TYPE_ID <> @MORTAGAGEE_INCEPTION)                     
                   
IF (@BILL_TYPE_ID <> @MORTAGAGEE_INCEPTION)                   
  BEGIN                     
 UPDATE POL_CUSTOMER_POLICY_LIST SET DWELLING_ID=0,ADD_INT_ID=0 WHERE CUSTOMER_ID = @CUSTOMER_ID                            
  AND POLICY_ID = @POLICY_ID                            
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID          
                      
 --UPDATE ADD INTEREST TABLES                  
 IF (@POLICY_LOB='1' OR @POLICY_LOB ='6')                  
   UPDATE POL_HOME_OWNER_ADD_INT SET BILL_MORTAGAGEE=NULL                  
   WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                   
 END                  
                    
IF ( (@POLICY_LOB='1' OR @POLICY_LOB ='6') AND (ISNULL(@POLICY_TYPE,'') ='11458' OR ISNULL(@POLICY_TYPE,'')='11480' OR ISNULL(@POLICY_TYPE,'')='11482' OR ISNULL(@POLICY_TYPE,'')= '11290' OR ISNULL(@POLICY_TYPE,'')= '11292') )                  
 BEGIN                  
  UPDATE POL_HOME_RATING_INFO SET IS_UNDER_CONSTRUCTION = 0 WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                   
 END                
             
--IN CASE OF REWRITE THE POLICY  (BY PRAVESH)          
IF (@STATE_ID IS NOT NULL AND @STATE_ID !=0)                    
   UPDATE POL_CUSTOMER_POLICY_LIST SET STATE_ID=@STATE_ID WHERE CUSTOMER_ID = @CUSTOMER_ID                            
    AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID              
                      
IF (@AGENCY_ID IS NOT NULL AND @AGENCY_ID !=0)                    
   UPDATE POL_CUSTOMER_POLICY_LIST                   
   SET AGENCY_ID=@AGENCY_ID , CSR=@CSR , PRODUCER=@PRODUCER           
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
                    
-- BY PRAVESH IF POLICY IS UNDER REWRITE AND POLICY DATE IS CHANGED UPDATE POL_POLICY_PROCESS FOR THIS REWRITE                     
SELECT @POLICY_STATUS=POLICY_STATUS FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
                     
--UREWRITE    REWRITE IN PROGRESS                  
--REWRTSUSP   REWRITE IN SUSPENSE                
            
IF (@POLICY_STATUS='UREWRITE' OR @POLICY_STATUS='REWRTSUSP')                  
     UPDATE POL_POLICY_PROCESS SET POLICY_TERMS=@APP_TERMS,NEW_POLICY_TERM_EFFECTIVE_DATE=@APP_EFFECTIVE_DATE, NEW_POLICY_TERM_EXPIRATION_DATE=@APP_EXPIRATION_DATE                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID  AND PROCESS_STATUS='PENDING'            
                    
-- IF POLICY UNDER NBS IN PROGRESS OR REWRITE IN PROGRESS AND USER CHANGE POL EFFECTIVE DATE ADDED BY P K CHANDEL ON 7 AUG 09                  
IF (@POLICY_STATUS='UREWRITE' OR @POLICY_STATUS='REWRTSUSP' OR @POLICY_STATUS='UISSUE' )                 
 AND @OLD_APP_EFFECTIVE_DATE!=@APP_EFFECTIVE_DATE  --AND CONDITION ADDED BY CHARLES ON 21-SEP-09 FOR ITRACK 6323                   
     UPDATE POL_POLICY_PROCESS SET EFFECTIVE_DATETIME=@APP_EFFECTIVE_DATE                   
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID  AND PROCESS_STATUS='PENDING'                    
  --END HERE                      
                    
RETURN @RESULT                            
END 