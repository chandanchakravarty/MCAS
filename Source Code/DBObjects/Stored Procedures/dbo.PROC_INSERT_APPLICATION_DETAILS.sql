IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_APPLICATION_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_APPLICATION_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
--SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = 2222  
  
--SELECT * FROM ACT_POLICY_INSTALL_PLAN_DATA WHERE CUSTOMER_ID = 2222  
--  DECLARE @CUSTOMER_ID INT 
--EXEC PROC_INSERT_CUSTOMER_DETAILS @CUSTOMER_ID OUTPUT
--SELECT @CUSTOMER_ID
  
    
/*----------------------------------------------------------                                                                                                                                                                                
PROC NAME       : PROC_INSERT_CUSTOMER_DETAILS                    
CREATED BY      : ANKIT GOEL                                                                                                                                                                             
DATE            : 26 OCT 2010                                                                                                                                                                          
PURPOSE         : PROC_INSERT_APPLICATION_DETAILS and  risk details        
-------------------------------------*/         
        
--drop proc PROC_INSERT_APPLICATION_DETAILS        
        
CREATE PROC [dbo].[PROC_INSERT_APPLICATION_DETAILS]   
@CUSTOMER_ID int,  
@FLAG  int -- for risk insert execute if @flag = 1    
AS        
        
BEGIN        
   
-------------insert into  POL_CUSTOMER_POLICY_LIST---------------------        
        
Declare     
  
   
  --GLOBAL VARAIBlE---------  
  @POLICY_ID int,         
  @POLICY_VERSION_ID smallint,  
           
  @APP_ID int,--WILL BE POLICY ID   
           
  @APP_VERSION_ID smallint,         
  @POLICY_TYPE nvarchar (24),        
  @POLICY_NUMBER nvarchar (150),        
  @POLICY_DISP_VERSION nvarchar (912),        
  @POLICY_STATUS varchar (20),        
  @POLICY_LOB nvarchar (10),        
  @POLICY_SUBLOB nvarchar (10),        
  @POLICY_DESCRIPTION varchar (255),        
  @ACCOUNT_EXEC int,         
  @CSR int,         
  @UNDERWRITER int ,        
  @PROCESS_STATUS smallint ,        
  @IS_UNDER_CONFIRMATION nchar (2),        
  @LAST_PROCESS nvarchar (20),        
  @LAST_PROCESS_COMPLETED datetime,         
  @IS_ACTIVE nchar (2),        
  @CREATED_BY int ,        
  @CREATED_DATETIME datetime ,        
  @MODIFIED_BY int ,        
  @LAST_UPDATED_DATETIME datetime ,        
  @POLICY_ACCOUNT_STATUS int ,        
  @AGENCY_ID smallint ,        
  @PARENT_APP_VERSION_ID smallint ,        
  @APP_STATUS nvarchar (50),        
  @APP_NUMBER nvarchar (150),        
  @APP_VERSION nvarchar (8),        
  @APP_TERMS nvarchar (10),        
  @APP_INCEPTION_DATE datetime,    
  @APP_EFFECTIVE_DATE datetime,         
  @APP_EXPIRATION_DATE datetime ,        
  @IS_UNDER_REVIEW nchar (2),        
  @COUNTRY_ID int ,        
  @STATE_ID smallint ,        
  @DIV_ID smallint ,        
  @DEPT_ID smallint,         
  @PC_ID smallint ,        
  @BILL_TYPE char (2),        
  @COMPLETE_APP char(1),        
  @INSTALL_PLAN_ID int ,        
  @CHARGE_OFF_PRMIUM varchar (5),        
  @RECEIVED_PRMIUM decimal (18,2),        
  @PROXY_SIGN_OBTAINED int ,        
  @SHOW_QUOTE nchar (2),        
  @APP_VERIFICATION_XML nvarchar(max),         
  @YEAR_AT_CURR_RESI real,         
  @YEARS_AT_PREV_ADD varchar (250),        
  @POLICY_TERMS nvarchar (10),        
  @POLICY_EFFECTIVE_DATE datetime,         
  @POLICY_EXPIRATION_DATE datetime,         
  @POLICY_STATUS_CODE varchar (6),        
  @SEND_RENEWAL_DIARY_REM char (1),        
  @TO_BE_AUTO_RENEWED smallint,        
  @POLICY_PREMIUM_XML nvarchar(max),        
  @MVR_WIN_SERVICE char (1),        
  @ALL_DATA_VALID int ,        
  @PIC_OF_LOC int ,        
  @PROPRTY_INSP_CREDIT int ,        
  @BILL_TYPE_ID int ,        
  @IS_HOME_EMP bit ,        
  @RULE_INPUT_XML nvarchar(max),        
  @POL_VER_EFFECTIVE_DATE datetime,         
  @POL_VER_EXPIRATION_DATE datetime,         
  @APPLY_INSURANCE_SCORE int,         
  @DWELLING_ID smallint,         
  @ADD_INT_ID int,         
  @PRODUCER int,         
  @DOWN_PAY_MODE int,         
  @CURRENT_TERM smallint,         
  @NOT_RENEW char (1),        
  @NOT_RENEW_REASON int,         
  @REFER_UNDERWRITER char (1),        
  @REFERAL_INSTRUCTIONS nvarchar (2000),        
  @REINS_SPECIAL_ACPT int,        
  @FROM_AS400 char,         
  @CUSTOMER_REASON_CODE nvarchar (20),        
  @CUSTOMER_REASON_CODE2 nvarchar (20),        
  @CUSTOMER_REASON_CODE3 nvarchar (20),        
  @CUSTOMER_REASON_CODE4 nvarchar (20),        
  @IS_REWRITE_POLICY char (1),        
  @IS_YEAR_WITH_WOL_UPDATED char (1),        
  @POLICY_CURRENCY int,         
  @POLICY_LEVEL_COMISSION decimal (18,2),        
  @BILLTO nvarchar (100),        
  @PAYOR int,         
  @CO_INSURANCE int ,        
  @CONTACT_PERSON int ,        
  @TRANSACTION_TYPE int ,        
  @PREFERENCE_DAY smallint,         
  @BROKER_REQUEST_NO nvarchar (100),        
  @POLICY_LEVEL_COMM_APPLIES nchar (2),        
  @BROKER_COMM_FIRST_INSTM nchar (2)    
    
  IF(@CUSTOMER_ID =NULL OR @CUSTOMER_ID <=0)  
  RETURN -1  
    
  SELECT @POLICY_ID = ISNULL(MAX(POLICY_ID),0)+1 FROM POL_CUSTOMER_POLICY_LIST   
     WHERE CUSTOMER_ID =@CUSTOMER_ID       
        
  --SET @CUSTOMER_ID =  2222  
  --SET @POLICY_ID =1        
  SET @POLICY_VERSION_ID =1        
  SET @APP_ID =@POLICY_ID        
  SET @APP_VERSION_ID =1      
    
      
  SET @POLICY_TYPE =NULL        
  SET @POLICY_NUMBER = '889982010a10196000288'      
  SET @POLICY_DISP_VERSION = 1.0        
  SET @POLICY_STATUS ='Suspended'        
  SET @POLICY_LOB = 9        
  SET @POLICY_SUBLOB =1        
  SET @POLICY_DESCRIPTION=NULL        
  SET @ACCOUNT_EXEC=NULL        
  SET @CSR=61        
  SET @UNDERWRITER=241        
  SET @PROCESS_STATUS=NULL        
  SET @IS_UNDER_CONFIRMATION=NULL        
  SET @LAST_PROCESS=NULL        
  SET @LAST_PROCESS_COMPLETED=NULL        
  SET @IS_ACTIVE='Y'        
  SET @CREATED_BY=198        
  SET @CREATED_DATETIME='2010-10-27 10:08:07.233'        
  SET @MODIFIED_BY=NULL        
  SET @LAST_UPDATED_DATETIME=NULL        
  SET @POLICY_ACCOUNT_STATUS=NULL        
  SET @AGENCY_ID=83        
  SET @PARENT_APP_VERSION_ID=0        
  SET @APP_STATUS='COMPLETE'        
  SET @APP_NUMBER='P8998343APP'        
  SET @APP_VERSION='1.0'        
  SET @APP_TERMS='1'        
  SET @APP_INCEPTION_DATE='2010-10-27 00:00:00.000'        
  SET @APP_EFFECTIVE_DATE='2010-10-27 00:00:00.000'        
  SET @APP_EXPIRATION_DATE='2010-10-28 00:00:00.000'        
  SET @IS_UNDER_REVIEW=NULL        
  SET @COUNTRY_ID =1         
  SET @STATE_ID = 0        
  SET @DIV_ID =291        
  SET @DEPT_ID = 161        
  SET @PC_ID = 200        
  SET @BILL_TYPE ='DB'        
  SET @COMPLETE_APP=''        
  SET @INSTALL_PLAN_ID=55        
  SET @CHARGE_OFF_PRMIUM =''        
  SET @RECEIVED_PRMIUM =0.00        
  SET @PROXY_SIGN_OBTAINED=0        
  SET @SHOW_QUOTE=NULL        
  SET @APP_VERIFICATION_XML=NULL        
  SET @YEAR_AT_CURR_RESI=0        
  SET @YEARS_AT_PREV_ADD=''        
  SET @POLICY_TERMS=NULL        
  SET @POLICY_EFFECTIVE_DATE='2010-10-27 00:00:00.000'        
  SET @POLICY_EXPIRATION_DATE='2010-10-27 00:00:00.000'        
  SET @POLICY_STATUS_CODE=NULL        
  SET @SEND_RENEWAL_DIARY_REM=NULL        
  SET @TO_BE_AUTO_RENEWED=NULL        
  SET @POLICY_PREMIUM_XML=NULL        
  SET @MVR_WIN_SERVICE=NULL        
  SET @ALL_DATA_VALID=NULL        
  SET @PIC_OF_LOC=NULL        
  SET @PROPRTY_INSP_CREDIT=0        
  SET @BILL_TYPE_ID=8460        
  SET @IS_HOME_EMP=0        
  SET @RULE_INPUT_XML=NULL        
  SET @POL_VER_EFFECTIVE_DATE='2010-10-27 00:00:00.000'        
  SET @POL_VER_EXPIRATION_DATE='2010-10-28 00:00:00.000'        
  SET @APPLY_INSURANCE_SCORE=-1        
  SET @DWELLING_ID=NULL        
  SET @ADD_INT_ID=NULL        
  SET @PRODUCER=0        
  SET @DOWN_PAY_MODE=11974        
  SET @CURRENT_TERM=1        
  SET @NOT_RENEW=NULL        
  SET @NOT_RENEW_REASON=NULL        
  SET @REFER_UNDERWRITER=NULL        
  SET @REFERAL_INSTRUCTIONS=NULL        
  SET @REINS_SPECIAL_ACPT=NULL        
  SET @FROM_AS400='Y'        
  SET @CUSTOMER_REASON_CODE=''        
  SET @CUSTOMER_REASON_CODE2=''        
  SET @CUSTOMER_REASON_CODE3=''        
  SET @CUSTOMER_REASON_CODE4=''        
  SET @IS_REWRITE_POLICY=NULL        
  SET @IS_YEAR_WITH_WOL_UPDATED=NULL        
  SET @POLICY_CURRENCY=1        
  SET @POLICY_LEVEL_COMISSION=0.00        
  SET @BILLTO='Sanjay  Kumar  Garg'        
  SET @PAYOR=14542        
  SET @CO_INSURANCE=14548        
  SET @CONTACT_PERSON=0        
  SET @TRANSACTION_TYPE=0        
  SET @PREFERENCE_DAY=0        
  SET @BROKER_REQUEST_NO=''        
  SET @POLICY_LEVEL_COMM_APPLIES='N'        
  SET @BROKER_COMM_FIRST_INSTM='N'        
          
   INSERT INTO [POL_CUSTOMER_POLICY_LIST]        
           ([CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[APP_ID]        
           ,[APP_VERSION_ID]        
           ,[POLICY_TYPE]        
           ,[POLICY_NUMBER]        
           ,[POLICY_DISP_VERSION]        
           ,[POLICY_STATUS]        
           ,[POLICY_LOB]        
           ,[POLICY_SUBLOB]        
           ,[POLICY_DESCRIPTION]        
           ,[ACCOUNT_EXEC]        
           ,[CSR]        
           ,[UNDERWRITER]        
           ,[PROCESS_STATUS]        
           ,[IS_UNDER_CONFIRMATION]        
           ,[LAST_PROCESS]        
           ,[LAST_PROCESS_COMPLETED]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME]        
           ,[POLICY_ACCOUNT_STATUS]        
           ,[AGENCY_ID]        
           ,[PARENT_APP_VERSION_ID]        
           ,[APP_STATUS]        
           ,[APP_NUMBER]        
           ,[APP_VERSION]        
           ,[APP_TERMS]        
           ,[APP_INCEPTION_DATE]        
           ,[APP_EFFECTIVE_DATE]        
           ,[APP_EXPIRATION_DATE]        
           ,[IS_UNDER_REVIEW]        
           ,[COUNTRY_ID]        
           ,[STATE_ID]        
           ,[DIV_ID]        
           ,[DEPT_ID]        
           ,[PC_ID]        
           ,[BILL_TYPE]        
           ,[COMPLETE_APP]        
           ,[INSTALL_PLAN_ID]        
           ,[CHARGE_OFF_PRMIUM]        
           ,[RECEIVED_PRMIUM]        
           ,[PROXY_SIGN_OBTAINED]        
           ,[SHOW_QUOTE]        
           ,[APP_VERIFICATION_XML]        
           ,[YEAR_AT_CURR_RESI]        
           ,[YEARS_AT_PREV_ADD]        
           ,[POLICY_TERMS]        
           ,[POLICY_EFFECTIVE_DATE]        
           ,[POLICY_EXPIRATION_DATE]        
           ,[POLICY_STATUS_CODE]        
           ,[SEND_RENEWAL_DIARY_REM]        
           ,[TO_BE_AUTO_RENEWED]        
           ,[POLICY_PREMIUM_XML]        
           ,[MVR_WIN_SERVICE]        
           ,[ALL_DATA_VALID]        
           ,[PIC_OF_LOC]        
           ,[PROPRTY_INSP_CREDIT]        
           ,[BILL_TYPE_ID]        
           ,[IS_HOME_EMP]        
           ,[RULE_INPUT_XML]        
           ,[POL_VER_EFFECTIVE_DATE]        
           ,[POL_VER_EXPIRATION_DATE]        
           ,[APPLY_INSURANCE_SCORE]        
           ,[DWELLING_ID]        
          ,[ADD_INT_ID]        
           ,[PRODUCER]        
           ,[DOWN_PAY_MODE]        
           ,[CURRENT_TERM]        
           ,[NOT_RENEW]        
           ,[NOT_RENEW_REASON]        
           ,[REFER_UNDERWRITER]        
           ,[REFERAL_INSTRUCTIONS]        
           ,[REINS_SPECIAL_ACPT]        
           ,[FROM_AS400]        
           ,[CUSTOMER_REASON_CODE]        
           ,[CUSTOMER_REASON_CODE2]        
           ,[CUSTOMER_REASON_CODE3]        
           ,[CUSTOMER_REASON_CODE4]        
           ,[IS_REWRITE_POLICY]        
           ,[IS_YEAR_WITH_WOL_UPDATED]        
           ,[POLICY_CURRENCY]        
           ,[POLICY_LEVEL_COMISSION]        
           ,[BILLTO]        
           ,[PAYOR]        
           ,[CO_INSURANCE]        
           ,[CONTACT_PERSON]        
           ,[TRANSACTION_TYPE]        
           ,[PREFERENCE_DAY]        
           ,[BROKER_REQUEST_NO]        
           ,[POLICY_LEVEL_COMM_APPLIES]        
           ,[BROKER_COMM_FIRST_INSTM])        
     VALUES        
    (  @CUSTOMER_ID        
    ,@POLICY_ID         
    ,@POLICY_VERSION_ID        
    ,@APP_ID        
    ,@APP_VERSION_ID         
    ,@POLICY_TYPE           
    ,@POLICY_NUMBER        
    ,@POLICY_DISP_VERSION        
    ,@POLICY_STATUS            
    ,@POLICY_LOB            
    ,@POLICY_SUBLOB        
    ,@POLICY_DESCRIPTION        
    ,@ACCOUNT_EXEC           
    ,@CSR        
    ,@UNDERWRITER        
    ,@PROCESS_STATUS        
    ,@IS_UNDER_CONFIRMATION        
    ,@LAST_PROCESS          
    ,@LAST_PROCESS_COMPLETED        
    ,@IS_ACTIVE         
    ,@CREATED_BY        
    ,@CREATED_DATETIME        
    ,@MODIFIED_BY           
    ,@LAST_UPDATED_DATETIME        
    ,@POLICY_ACCOUNT_STATUS        
    ,@AGENCY_ID        
    ,@PARENT_APP_VERSION_ID        
    ,@APP_STATUS        
    ,@APP_NUMBER        
    ,@APP_VERSION        
    ,@APP_TERMS        
    ,@APP_INCEPTION_DATE        
    ,@APP_EFFECTIVE_DATE        
    ,@APP_EXPIRATION_DATE        
    ,@IS_UNDER_REVIEW        
    ,@COUNTRY_ID        
    ,@STATE_ID         
    ,@DIV_ID         
    ,@DEPT_ID        
    ,@PC_ID         
    ,@BILL_TYPE        
    ,@COMPLETE_APP        
    ,@INSTALL_PLAN_ID        
    ,@CHARGE_OFF_PRMIUM        
    ,@RECEIVED_PRMIUM         
    ,@PROXY_SIGN_OBTAINED        
    ,@SHOW_QUOTE        
    ,@APP_VERIFICATION_XML        
    ,@YEAR_AT_CURR_RESI        
    ,@YEARS_AT_PREV_ADD        
    ,@POLICY_TERMS           
    ,@POLICY_EFFECTIVE_DATE          
    ,@POLICY_EXPIRATION_DATE           
    ,@POLICY_STATUS_CODE          
    ,@SEND_RENEWAL_DIARY_REM          
    ,@TO_BE_AUTO_RENEWED           
    ,@POLICY_PREMIUM_XML          
    ,@MVR_WIN_SERVICE          
    ,@ALL_DATA_VALID          
    ,@PIC_OF_LOC          
    ,@PROPRTY_INSP_CREDIT           
    ,@BILL_TYPE_ID        
    ,@IS_HOME_EMP        
    ,@RULE_INPUT_XML        
    ,@POL_VER_EFFECTIVE_DATE        
    ,@POL_VER_EXPIRATION_DATE        
    ,@APPLY_INSURANCE_SCORE        
    ,@DWELLING_ID        
    ,@ADD_INT_ID        
    ,@PRODUCER        
    ,@DOWN_PAY_MODE        
    ,@CURRENT_TERM        
    ,@NOT_RENEW        
    ,@NOT_RENEW_REASON        
    ,@REFER_UNDERWRITER        
    ,@REFERAL_INSTRUCTIONS        
    ,@REINS_SPECIAL_ACPT        
    ,@FROM_AS400        
    ,@CUSTOMER_REASON_CODE        
    ,@CUSTOMER_REASON_CODE2        
    ,@CUSTOMER_REASON_CODE3        
    ,@CUSTOMER_REASON_CODE4        
    ,@IS_REWRITE_POLICY        
    ,@IS_YEAR_WITH_WOL_UPDATED        
    ,@POLICY_CURRENCY        
    ,@POLICY_LEVEL_COMISSION        
    ,@BILLTO        
    ,@PAYOR        
    ,@CO_INSURANCE        
    ,@CONTACT_PERSON        
    ,@TRANSACTION_TYPE        
    ,@PREFERENCE_DAY        
    ,@BROKER_REQUEST_NO        
    ,@POLICY_LEVEL_COMM_APPLIES        
    ,@BROKER_COMM_FIRST_INSTM        
                   
           )        
          
------------------POL_APPLICANT_LIST-------------------------------        
DECLARE         
 --@POLICY_ID int,        
 --@POLICY_VERSION_ID smallint,        
 --@CUSTOMER_ID int,        
 @APPLICANT_ID int,        
 --@CREATED_BY int,        
 --@MODIFIED_BY int,        
 --@MODIFIED_BY int,        
 --@MODIFIED_BY int,        
 --@CREATED_DATETIME datetime,        
 @LAST_UPDATED_TIME datetime,        
 @IS_PRIMARY_APPLICANT int,        
 @COMMISSION_PERCENT decimal(8,4),        
 @FEES_PERCENT decimal(8,4)        
         
         
 SET @APPLICANT_ID=0                      
    SET @IS_PRIMARY_APPLICANT=0                  
                      
 SELECT @APPLICANT_ID = APPLICANT_ID                       
 FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND IS_PRIMARY_APPLICANT = 1                      
               
 IF (@APPLICANT_ID > 0)                      
 BEGIN             
 SET @IS_PRIMARY_APPLICANT=1                  
 END         
         
         
 SET @LAST_UPDATED_TIME =NULL        
 SET @COMMISSION_PERCENT =0.0000        
 SET @FEES_PERCENT = 0.0000        
         
         
  INSERT INTO POL_APPLICANT_LIST                                                      
   (                                                                                                                    
 POLICY_ID,        
 POLICY_VERSION_ID,        
 CUSTOMER_ID,         
 APPLICANT_ID,        
 CREATED_BY,        
 MODIFIED_BY,        
 CREATED_DATETIME,        
 LAST_UPDATED_TIME,        
 IS_PRIMARY_APPLICANT,        
 COMMISSION_PERCENT,        
 FEES_PERCENT                          
   )             
   VALUES                                                                                                                   
   (@POLICY_ID,        
 @POLICY_VERSION_ID,        
 @CUSTOMER_ID,        
 @APPLICANT_ID,        
 @CREATED_BY,        
 @MODIFIED_BY,         
 @CREATED_DATETIME,        
 @LAST_UPDATED_TIME,        
 @IS_PRIMARY_APPLICANT,        
 @COMMISSION_PERCENT,        
 @FEES_PERCENT        
 )        
        
        
------------------------INSERT INTO LOCATION------------------------------------        
DECLARE         
 --@CUSTOMER_ID int,        
 --@POLICY_ID int,        
 --@POLICY_VERSION_ID smallint,        
 @LOCATION_ID smallint,        
 @LOC_NUM int,         
 @IS_PRIMARY nchar,        
 @LOC_ADD1 nvarchar(150),        
 @LOC_ADD2 nvarchar (150),        
 @LOC_CITY nvarchar (150),        
 @LOC_COUNTY nvarchar (150),        
 @LOC_STATE nvarchar (10),        
 @LOC_ZIP nvarchar (22),        
 @LOC_COUNTRY nvarchar (10),        
 @PHONE_NUMBER nvarchar (40),        
 @FAX_NUMBER nvarchar (40),        
 @DEDUCTIBLE nvarchar (40),        
 @NAMED_PERILL int,         
 @DESCRIPTION varchar (1000),        
 --@IS_ACTIVE nchar,         
 --@CREATED_BY int ,        
 --@CREATED_DATETIME datetime ,        
 --@MODIFIED_BY int ,        
 --@LAST_UPDATED_DATETIME datetime ,        
 @LOC_TERRITORY nvarchar (10),        
 @LOCATION_TYPE int ,        
 @RENTED_WEEKLY varchar (10),        
 @WEEKS_RENTED varchar (10),        
 @LOSSREPORT_ORDER int ,        
 @LOSSREPORT_DATETIME datetime,         
 @REPORT_STATUS char (1),        
 @CAL_NUM nvarchar (40),        
 @NAME nvarchar (60),        
 @NUMBER nvarchar (40),        
 @DISTRICT nvarchar (100),        
 @OCCUPIED int,        
 @EXT nvarchar (20),        
 @CATEGORY nvarchar (40),        
 @ACTIVITY_TYPE int ,        
 @CONSTRUCTION int ,        
 @SOURCE_LOCATION_ID int ,        
 @IS_BILLING nchar (2)        
         
         
 --SET @CUSTOMER_ID = 2525         
 --SET @POLICY_ID = 1        
 --SET @POLICY_VERSION_ID =1        
 SET @LOCATION_ID = 4        
 SET @LOC_NUM = 4        
 SET @IS_PRIMARY =NULL        
 SET @LOC_ADD1 ='RUA DIREITA'        
 SET @LOC_ADD2 =NULL        
 SET @LOC_CITY ='SAO PAULO'        
 SET @LOC_COUNTY = NULL        
 SET @LOC_STATE ='71'        
 SET @LOC_ZIP='1002000'        
 SET @LOC_COUNTRY='5'        
 SET @PHONE_NUMBER=NULL        
 SET @FAX_NUMBER=NULL        
 SET @DEDUCTIBLE=NULL        
 SET @NAMED_PERILL=NULL        
 SET @DESCRIPTION=NULL         
 SET @IS_ACTIVE='Y'        
 SET @CREATED_BY=198        
 SET @CREATED_DATETIME='2010-10-27 12:01:25.053'        
 SET @MODIFIED_BY=NULL        
 SET @LAST_UPDATED_DATETIME=NULL        
 SET @LOC_TERRITORY=NULL        
 SET @LOCATION_TYPE=0         
 SET @RENTED_WEEKLY=NULL        
 SET @WEEKS_RENTED=NULL        
 SET @LOSSREPORT_ORDER=NULL        
 SET @LOSSREPORT_DATETIME=NULL        
 SET @REPORT_STATUS=NULL        
 SET @CAL_NUM='Calculation'        
 SET @NAME='Building Name'        
 SET @NUMBER='12345'        
 SET @DISTRICT='SE'        
 SET @OCCUPIED=0        
 SET @EXT=NULL        
 SET @CATEGORY=NULL        
 SET @ACTIVITY_TYPE=0        
 SET @CONSTRUCTION=0        
 SET @SOURCE_LOCATION_ID=2         
 SET @IS_BILLING='N'        
        
 INSERT INTO [POL_LOCATIONS]        
           ([CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[LOCATION_ID]        
           ,[LOC_NUM]        
           ,[IS_PRIMARY]        
           ,[LOC_ADD1]        
           ,[LOC_ADD2]        
           ,[LOC_CITY]        
           ,[LOC_COUNTY]        
       ,[LOC_STATE]        
           ,[LOC_ZIP]        
           ,[LOC_COUNTRY]        
           ,[PHONE_NUMBER]               ,[FAX_NUMBER]        
           ,[DEDUCTIBLE]        
           ,[NAMED_PERILL]        
           ,[DESCRIPTION]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME]        
           ,[LOC_TERRITORY]        
           ,[LOCATION_TYPE]        
           ,[RENTED_WEEKLY]        
           ,[WEEKS_RENTED]        
           ,[LOSSREPORT_ORDER]        
           ,[LOSSREPORT_DATETIME]        
           ,[REPORT_STATUS]        
           ,[CAL_NUM]        
           ,[NAME]        
           ,[NUMBER]        
           ,[DISTRICT]        
           ,[OCCUPIED]        
           ,[EXT]        
           ,[CATEGORY]        
           ,[ACTIVITY_TYPE]        
           ,[CONSTRUCTION]        
           ,[SOURCE_LOCATION_ID]        
           ,[IS_BILLING])        
     VALUES        
           ( @CUSTOMER_ID ,         
    @POLICY_ID  ,        
    @POLICY_VERSION_ID ,        
    @LOCATION_ID  ,        
    @LOC_NUM  ,        
    @IS_PRIMARY ,        
    @LOC_ADD1,         
    @LOC_ADD2 ,        
    @LOC_CITY,        
    @LOC_COUNTY,        
    @LOC_STATE,        
    @LOC_ZIP,        
    @LOC_COUNTRY,        
    @PHONE_NUMBER,        
    @FAX_NUMBER,        
    @DEDUCTIBLE,        
    @NAMED_PERILL,        
    @DESCRIPTION,        
    @IS_ACTIVE,        
    @CREATED_BY,        
    @CREATED_DATETIME,        
    @MODIFIED_BY,        
    @LAST_UPDATED_DATETIME,        
    @LOC_TERRITORY,        
    @LOCATION_TYPE,        
    @RENTED_WEEKLY,        
    @WEEKS_RENTED,        
    @LOSSREPORT_ORDER,        
    @LOSSREPORT_DATETIME,        
    @REPORT_STATUS,        
    @CAL_NUM,        
    @NAME,        
    @NUMBER,        
    @DISTRICT,        
    @OCCUPIED,        
    @EXT,        
    @CATEGORY,        
    @ACTIVITY_TYPE,        
    @CONSTRUCTION,        
            
    @SOURCE_LOCATION_ID,        
    @IS_BILLING)        
            
----------------------------INSERT INTO POL_PROTECTIVE_DEVICES ------------------        
       DECLARE        
  --@CUSTOMER_ID int,         
  --@POLICY_ID int,         
  --@POLICY_VERSION_ID smallint,         
  @RISK_ID int,         
  @PROTECTIVE_DEVICE_ID int,         
  @FIRE_EXTINGUISHER int,         
  @SPL_FIRE_EXTINGUISHER_UNIT int,         
  @MANUAL_FOAM_SYSTEM int,         
  @FOAM int,         
  @INERT_GAS_SYSTEM int,         
  @MANUAL_INERT_GAS_SYSTEM int,         
  @COMBAT_CARS int,         
  @CORRAL_SYSTEM int,         
  @ALARM_SYSTEM int,         
  @FREE_HYDRANT int,         
  @SPRINKLERS int,         
  @SPRINKLERS_CLASSIFICATION nvarchar (140),        
  @FIRE_FIGHTER nvarchar (140),        
  @QUESTIION_POINTS numeric (18,2)        
          
  --@IS_ACTIVE nchar (2),        
  --@CREATED_BY int,         
  --@CREATED_DATETIME datetime,         
  --@MODIFIED_BY int,         
  --@LAST_UPDATED_DATETIME datetime ,        
  --@LOCATION_ID int,        
          
          
          
          
  --SET @CUSTOMER_ID = 2525          
  --SET @POLICY_ID = 1        
  --SET @POLICY_VERSION_ID =1        
  SET @RISK_ID =NULL        
  --SET @PROTECTIVE_DEVICE_ID =258        
  SELECT  @PROTECTIVE_DEVICE_ID=ISNULL(MAX(PROTECTIVE_DEVICE_ID),0)+1        
   FROM POL_PROTECTIVE_DEVICES WITH (NOLOCK)-- WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POLICY_ID            
        
  SET @FIRE_EXTINGUISHER =10963        
  SET @SPL_FIRE_EXTINGUISHER_UNIT =10963        
  SET @MANUAL_FOAM_SYSTEM=10964        
  SET @FOAM=10963        
  SET @INERT_GAS_SYSTEM=10964        
  SET @MANUAL_INERT_GAS_SYSTEM=10964        
  SET @COMBAT_CARS=10964        
  SET @CORRAL_SYSTEM=10964        
  SET @ALARM_SYSTEM=10964        
  SET @FREE_HYDRANT=NULL        
  SET @SPRINKLERS=10964        
  SET @SPRINKLERS_CLASSIFICATION=''        
  SET @FIRE_FIGHTER=''        
  SET @QUESTIION_POINTS=NULL        
          
SET @IS_ACTIVE='Y'        
  SET @CREATED_BY=198        
  SET @CREATED_DATETIME='2010-10-27 12:14:29.000'        
  SET @MODIFIED_BY=NULL        
  SET @LAST_UPDATED_DATETIME=NULL        
  SET @LOCATION_ID=4        
         
          
        
        
INSERT INTO [POL_PROTECTIVE_DEVICES]        
           ([CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[RISK_ID]        
           ,[PROTECTIVE_DEVICE_ID]        
           ,[FIRE_EXTINGUISHER]        
           ,[SPL_FIRE_EXTINGUISHER_UNIT]        
           ,[MANUAL_FOAM_SYSTEM]        
           ,[FOAM]        
           ,[INERT_GAS_SYSTEM]        
           ,[MANUAL_INERT_GAS_SYSTEM]        
           ,[COMBAT_CARS]        
           ,[CORRAL_SYSTEM]        
           ,[ALARM_SYSTEM]        
           ,[FREE_HYDRANT]        
           ,[SPRINKLERS]        
           ,[SPRINKLERS_CLASSIFICATION]        
           ,[FIRE_FIGHTER]        
           ,[QUESTIION_POINTS]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME]        
           ,[LOCATION_ID])        
     VALUES        
           ( @CUSTOMER_ID,         
    @POLICY_ID,        
    @POLICY_VERSION_ID,        
    @RISK_ID,        
    @PROTECTIVE_DEVICE_ID,        
    @FIRE_EXTINGUISHER,        
    @SPL_FIRE_EXTINGUISHER_UNIT,        
    @MANUAL_FOAM_SYSTEM,        
    @FOAM,        
    @INERT_GAS_SYSTEM,        
    @MANUAL_INERT_GAS_SYSTEM,        
    @COMBAT_CARS,        
    @CORRAL_SYSTEM,        
    @ALARM_SYSTEM,        
    @FREE_HYDRANT,        
    @SPRINKLERS,        
    @SPRINKLERS_CLASSIFICATION,        
    @FIRE_FIGHTER,        
    @QUESTIION_POINTS,           
    @IS_ACTIVE,        
    @CREATED_BY,        
    @CREATED_DATETIME,        
    @MODIFIED_BY,        
    @LAST_UPDATED_DATETIME,        
    @LOCATION_ID        
)        
        
        
---------------INSERT INTO POL_REMUNERATION--------------------------------        
DECLARE        
 @REMUNERATION_ID int,        
 --@CUSTOMER_ID int,        
 --@POLICY_ID int,        
 --@POLICY_VERSION_ID int,        
 @BROKER_ID int,        
 --@COMMISSION_PERCENT numeric(8,0),        
 @COMMISSION_TYPE int,        
 --@IS_ACTIVE nchar,        
 --@CREATED_BY int,        
 --@CREATED_DATETIME datetime,        
 --@MODIFIED_BY int,        
 --@LAST_UPDATED_DATETIME datetime,        
 @BRANCH numeric(8,0),        
 --@NAME nvarchar(200),        
 @AMOUNT decimal(12,2),        
 @LEADER int,        
 --@RISK_ID int,        
 @CO_APPLICANT_ID int        
         
           
    --SET  @CUSTOMER_ID =  2525        
 --SET  @REMUNERATION_ID =         
 SELECT @REMUNERATION_ID = ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH (NOLOCK)        
         WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID       
            
 --SET  @POLICY_ID =1        
 --SET  @POLICY_VERSION_ID = 1        
 SET  @BROKER_ID =83        
 SET  @COMMISSION_PERCENT = 10.0000        
 SET  @COMMISSION_TYPE = 43        
 SET  @IS_ACTIVE='Y'        
 SET  @CREATED_BY=NULL        
 SET  @CREATED_DATETIME='2010-10-27 10:08:08.433'        
 SET  @MODIFIED_BY=198        
 SET  @LAST_UPDATED_DATETIME='2010-10-27 10:08:08.433'        
 SET  @BRANCH=1212      
 SET  @NAME='A 1-1-0- (Active)'        
 SET  @AMOUNT=NULL        
 SET  @LEADER=10963        
 SET  @RISK_ID=0        
 SET  @CO_APPLICANT_ID=0        
         
         
         
 INSERT INTO [POL_REMUNERATION]        
           ([REMUNERATION_ID]        
           ,[CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[BROKER_ID]        
           ,[COMMISSION_PERCENT]        
           ,[COMMISSION_TYPE]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME]        
           ,[BRANCH]        
           ,[NAME]        
           ,[AMOUNT]        
           ,[LEADER]        
           ,[RISK_ID]        
           ,[CO_APPLICANT_ID])        
     VALUES        
      ( @REMUNERATION_ID        
    ,@CUSTOMER_ID        
    ,@POLICY_ID        
    ,@POLICY_VERSION_ID        
    ,@BROKER_ID        
    ,@COMMISSION_PERCENT        
    ,@COMMISSION_TYPE        
    ,@IS_ACTIVE        
    ,@CREATED_BY        
    ,@CREATED_DATETIME        
    ,@MODIFIED_BY        
    ,@LAST_UPDATED_DATETIME        
    ,@BRANCH        
    ,@NAME        
    ,@AMOUNT        
    ,@LEADER        
    ,@RISK_ID        
    ,@CO_APPLICANT_ID                   
           )        
                   
-----------------------------POL_DISCOUNT_SURCHARGE----------------------------------        
 Declare        
  --@CUSTOMER_ID int,         
  --@POLICY_ID int,         
  --@POLICY_VERSION_ID int,        
  --@RISK_ID int,        
  @DISCOUNT_ROW_ID int,        
  @DISCOUNT_ID int,        
  @PERCENTAGE decimal (12,4)        
  --@IS_ACTIVE nchar (2),        
  --@CREATED_BY int,         
  --@CREATED_DATETIME datetime,        
  --@MODIFIED_BY int ,        
  --@LAST_UPDATED_DATETIME datetime         
        
        
        
 SELECT @DISCOUNT_ROW_ID= ISNULL(max(DISCOUNT_ROW_ID),0)+1 FROM POL_DISCOUNT_SURCHARGE  WITH(NOLOCK)   
  WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID =@POLICY_ID   
          
 SET @DISCOUNT_ID =31         
 SET @PERCENTAGE =22.0000              
      
         
 INSERT INTO [POL_DISCOUNT_SURCHARGE]        
           (        
           [CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[RISK_ID]        
           ,[DISCOUNT_ROW_ID]        
           ,[DISCOUNT_ID]        
           ,[PERCENTAGE]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME])        
     VALUES        
           (        
           @CUSTOMER_ID,         
     @POLICY_ID,         
     @POLICY_VERSION_ID,        
     @RISK_ID ,        
     @DISCOUNT_ROW_ID,        
     @DISCOUNT_ID,        
     @PERCENTAGE,        
     @IS_ACTIVE,        
     @CREATED_BY,         
     @CREATED_DATETIME,        
     @MODIFIED_BY,        
     @LAST_UPDATED_DATETIME        
     )        
             
-----------------Insert into ACT_POLICY_INSTALLMENT_DETAILS--------------------            
-----------------Insert into POL_CO_INSURANCE------------------------------        
 DECLARE         
 @COINSURANCE_ID int,         
 @COMPANY_ID int ,        
 --@CUSTOMER_ID int,         
 --@POLICY_ID int,         
 --@POLICY_VERSION_ID int,         
 @CO_INSURER_NAME nvarchar (100),        
 @LEADER_FOLLOWER int,         
 @COINSURANCE_PERCENT decimal (18,2),        
 @COINSURANCE_FEE decimal (18,2),        
 @BROKER_COMMISSION decimal (18,2),        
 @TRANSACTION_ID nvarchar (50),        
 @LEADER_POLICY_NUMBER nvarchar (50),        
 --@IS_ACTIVE nchar (2),        
 --@CREATED_BY int,         
 --@CREATED_DATETIME datetime,        
 --@MODIFIED_BY int,         
 --@LAST_UPDATED_DATETIME datetime,         
 @BRANCH_COINSURANCE_ID int         
         
         
  SET  @COINSURANCE_ID =377        
  SET  @COMPANY_ID = 64        
  --SET  @CUSTOMER_ID = 2525        
  --SET  @POLICY_ID = 1        
  --SET  @POLICY_VERSION_ID =1        
  SET  @CO_INSURER_NAME = NULL          
  SET  @LEADER_FOLLOWER = 14548        
  SET  @COINSURANCE_PERCENT =NULL        
  SET  @COINSURANCE_FEE =NULL        
  SET  @BROKER_COMMISSION =NULL        
  SET  @TRANSACTION_ID =NULL        
  SET  @LEADER_POLICY_NUMBER =NULL        
  SET  @IS_ACTIVE ='Y'        
  SET  @CREATED_BY =NULL        
  SET  @CREATED_DATETIME ='2010-10-27 18:59:22.290'        
  SET  @MODIFIED_BY =NULL        
  SET  @LAST_UPDATED_DATETIME =NULL        
  SET  @BRANCH_COINSURANCE_ID =NULL        
          
 INSERT INTO [POL_CO_INSURANCE]        
           ([COINSURANCE_ID]        
           ,[COMPANY_ID]        
           ,[CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[CO_INSURER_NAME]        
           ,[LEADER_FOLLOWER]        
           ,[COINSURANCE_PERCENT]        
           ,[COINSURANCE_FEE]        
           ,[BROKER_COMMISSION]        
           ,[TRANSACTION_ID]        
           ,[LEADER_POLICY_NUMBER]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           ,[MODIFIED_BY]        
           ,[LAST_UPDATED_DATETIME]        
           ,[BRANCH_COINSURANCE_ID])        
     VALUES        
           (@COINSURANCE_ID        
   ,@COMPANY_ID        
   ,@CUSTOMER_ID        
   ,@POLICY_ID        
   ,@POLICY_VERSION_ID        
   ,@CO_INSURER_NAME        
   ,@LEADER_FOLLOWER        
   ,@COINSURANCE_PERCENT        
   ,@COINSURANCE_FEE        
   ,@BROKER_COMMISSION        
   ,@TRANSACTION_ID        
   ,@LEADER_POLICY_NUMBER        
   ,@IS_ACTIVE        
   ,@CREATED_BY        
   ,@CREATED_DATETIME        
   ,@MODIFIED_BY        
   ,@LAST_UPDATED_DATETIME        
   ,@BRANCH_COINSURANCE_ID        
   )        
        
            
----------------------Clauses-------------------------------------            
-----------------------Reinsurance----------------------------------        
        
        
 IF(@flag = 1)-----FOR EXCUTE INSERT FOR RISK LEVEL DETAILS  
 BEGIN  
  --------------------------RISK DETAIKS-----        
  ----------------------INSERT INTO POL_PERILS----------------------------        
  DECLARE         
    --@CUSTOMER_ID int,         
    --@POLICY_ID int,         
    --@POLICY_VERSION_ID smallint,         
    @PERIL_ID smallint,         
    @CALCULATION_NUMBER int,         
    @LOCATION int,         
    @ADDRESS nvarchar (160),        
    --@NUMBER nvarchar 12        
    @COMPLEMENT nvarchar (40),        
    @CITY nvarchar (60),        
    @COUNTRY nvarchar (10),        
    @STATE nvarchar (10),        
    @ZIP nvarchar (22),        
    @TELEPHONE nvarchar (24),        
    @EXTENTION nvarchar (12),        
    @FAX nvarchar (24),        
    --@CATEGORY nvarchar (12),        
    @ATIV_CONTROL int,         
    @LOC nvarchar (8),        
    @LOCALIZATION nvarchar (2),        
    @OCCUPANCY nvarchar (16),        
    --@CONSTRUCTION nvarchar (16),        
    --@LOC_CITY nvarchar (80),        
    @CONSTRUCTION_TYPE nvarchar (22),        
    --@ACTIVITY_TYPE nvarchar (100),        
    @RISK_TYPE nvarchar (100),        
    @VR numeric (12,0),        
    @LMI numeric (12,0),        
    @BUILDING int ,        
    @MMU numeric (12,0),        
    @MMP numeric (12,0),        
    @MRI numeric (12,0),        
    @TYPE int,         
    @LOSS numeric (12,0),        
    @LOYALTY numeric (12,0),        
    @PERC_LOYALTY numeric (12,0),        
    @DEDUCTIBLE_OPTION int,         
    @MULTIPLE_DEDUCTIBLE nvarchar (16),        
    @E_FIRE int,         
    @S_FIXED_FOAM int,         
    @S_FIXED_INSERT_GAS int ,        
    @CAR_COMBAT int,         
    @S_DETECT_ALARM int,         
    @S_FIRE_UNIT int,         
    @S_FOAM_PER_MANUAL int,         
    @S_MANUAL_INERT_GAS int,         
    @S_SEMI_HOSES int,         
    @HYDRANTS int,         
    @SHOWERS int,         
    @SHOWER_CLASSIFICATION nvarchar (200),        
    @FIRE_CORPS nvarchar (200),        
    @PUNCTUATION_QUEST int,         
    @DMP numeric (12,0),        
    @EXPLOSION_DEGREE nvarchar (2),        
    @PR_LIQUID int,         
    @COD_ATIV_DRAFTS nvarchar (174),        
    @OCCUPATION_TEXT nvarchar (100),        
    @ASSIST24 int,         
    @LMRA numeric (12,0),        
    @AGGRAVATION_RCG_AIR numeric (12,0),        
    @EXPLOSION_DESC numeric (12,0),        
    @PROTECTIVE_DESC numeric (12,0),        
    @LMI_DESC numeric (12,0),        
    @LOSS_DESC numeric (12,0),        
    @QUESTIONNAIRE_DESC numeric (12,0),        
    @DEDUCTIBLE_DESC numeric (12,0),        
    @GROUPING_DESC numeric (12,0),        
    @LOC_FLOATING nvarchar (8),        
    @ADJUSTABLE numeric (12,0),        
    --@IS_ACTIVE nchar (2),        
    --@CREATED_BY int,         
    --@CREATED_DATETIME datetime         
    --@MODIFIED_BY int         
    --@LAST_UPDATED_DATETIME datetime         
    --@CORRAL_SYSTEM nvarchar 40        
    @RAWVALUES nvarchar (40),        
    @REMARKS nvarchar (1000),        
    @PARKING_SPACES nvarchar (40),        
    @CLAIM_RATIO decimal (12,2),        
    @RAW_MATERIAL_VALUE nvarchar (40),        
    @CONTENT_VALUE nvarchar (40),        
    @BONUS decimal (12,2)        
            
            
    --SET  @CUSTOMER_ID = 2525        
    --SET  @POLICY_ID = 1        
    --SET  @POLICY_VERSION_ID =1        
    SET  @PERIL_ID = 1        
    SET  @CALCULATION_NUMBER = NULL        
    SET  @LOCATION =4        
    SET  @ADDRESS =NULL        
    SET  @NUMBER =NULL        
    SET  @COMPLEMENT =NULL        
    SET  @CITY =NULL        
    SET  @COUNTRY =NULL        
    SET  @STATE =NULL        
    SET  @ZIP =NULL        
    SET  @TELEPHONE =NULL        
    SET  @EXTENTION =NULL        
    SET  @FAX =NULL        
    SET  @CATEGORY =''        
    SET  @ATIV_CONTROL =NULL        
    SET  @LOC =NULL        
    SET  @LOCALIZATION =NULL        
    SET  @OCCUPANCY =''        
    SET  @CONSTRUCTION =''        
    SET  @LOC_CITY =NULL        
    SET  @CONSTRUCTION_TYPE =NULL        
    SET  @ACTIVITY_TYPE =''        
    SET  @RISK_TYPE =NULL        
    SET  @VR =NULL        
    SET  @LMI =NULL        
    SET  @BUILDING =500000        
    SET  @MMU =NULL        
    SET  @MMP =NULL        
    SET  @MRI =NULL        
    SET  @TYPE =NULL        
    SET  @LOSS =NULL        
    SET  @LOYALTY =NULL        
    SET  @PERC_LOYALTY =NULL        
    SET  @DEDUCTIBLE_OPTION =NULL        
    SET  @MULTIPLE_DEDUCTIBLE =''        
    SET  @E_FIRE =NULL        
    SET  @S_FIXED_FOAM =NULL        
    SET  @S_FIXED_INSERT_GAS =NULL        
    SET  @CAR_COMBAT =NULL        
    SET  @S_DETECT_ALARM =NULL        
    SET  @S_FIRE_UNIT =NULL        
    SET  @S_FOAM_PER_MANUAL =NULL        
    SET  @S_MANUAL_INERT_GAS =NULL        
    SET  @S_SEMI_HOSES =NULL        
    SET  @HYDRANTS =NULL        
    SET  @SHOWERS =NULL        
    SET  @SHOWER_CLASSIFICATION =NULL        
    SET  @FIRE_CORPS =NULL        
    SET  @PUNCTUATION_QUEST =NULL        
    SET  @DMP =NULL        
    SET  @EXPLOSION_DEGREE =NULL        
    SET  @PR_LIQUID =NULL        
    SET  @COD_ATIV_DRAFTS =NULL        
    SET  @OCCUPATION_TEXT =NULL        
    SET  @ASSIST24 = 10964        
    SET  @LMRA =NULL        
    SET  @AGGRAVATION_RCG_AIR =NULL        
    SET  @EXPLOSION_DESC =NULL        
    SET  @PROTECTIVE_DESC =NULL        
    SET  @LMI_DESC =NULL        
    SET  @LOSS_DESC =NULL        
    SET  @QUESTIONNAIRE_DESC =NULL        
    SET  @DEDUCTIBLE_DESC =NULL        
    SET  @GROUPING_DESC =NULL        
    SET  @LOC_FLOATING =NULL        
    SET  @ADJUSTABLE =NULL        
    SET  @IS_ACTIVE ='Y'        
    SET  @CREATED_BY =198        
    SET  @CREATED_DATETIME ='2010-10-27 00:00:00.000'        
    SET  @MODIFIED_BY =198        
    SET  @LAST_UPDATED_DATETIME ='2010-10-27 00:00:00.000'        
    SET  @CORRAL_SYSTEM =NULL        
    SET  @RAWVALUES ='Y'        
    SET  @REMARKS =''        
    SET  @PARKING_SPACES =23        
    SET  @CLAIM_RATIO =NULL        
    SET  @RAW_MATERIAL_VALUE =23          
    SET  @CONTENT_VALUE =100000        
    SET  @BONUS =1.00        
          
   INSERT INTO [POL_PERILS]        
     ([CUSTOMER_ID]        
     ,[POLICY_ID]        
     ,[POLICY_VERSION_ID]        
     ,[PERIL_ID]        
     ,[CALCULATION_NUMBER]        
     ,[LOCATION]        
     ,[ADDRESS]        
     ,[NUMBER]        
     ,[COMPLEMENT]        
     ,[CITY]        
     ,[COUNTRY]        
     ,[STATE]        
     ,[ZIP]        
     ,[TELEPHONE]        
     ,[EXTENTION]        
     ,[FAX]        
     ,[CATEGORY]        
     ,[ATIV_CONTROL]        
     ,[LOC]        
     ,[LOCALIZATION]        
     ,[OCCUPANCY]        
     ,[CONSTRUCTION]        
     ,[LOC_CITY]        
     ,[CONSTRUCTION_TYPE]        
   ,[ACTIVITY_TYPE]        
     ,[RISK_TYPE]        
     ,[VR]        
     ,[LMI]        
     ,[BUILDING]        
     ,[MMU]        
     ,[MMP]        
     ,[MRI]        
     ,[TYPE]        
     ,[LOSS]        
     ,[LOYALTY]        
     ,[PERC_LOYALTY]        
     ,[DEDUCTIBLE_OPTION]        
     ,[MULTIPLE_DEDUCTIBLE]        
     ,[E_FIRE]        
     ,[S_FIXED_FOAM]        
     ,[S_FIXED_INSERT_GAS]        
     ,[CAR_COMBAT]        
     ,[S_DETECT_ALARM]        
     ,[S_FIRE_UNIT]        
     ,[S_FOAM_PER_MANUAL]        
     ,[S_MANUAL_INERT_GAS]        
     ,[S_SEMI_HOSES]        
     ,[HYDRANTS]        
     ,[SHOWERS]        
     ,[SHOWER_CLASSIFICATION]        
     ,[FIRE_CORPS]        
     ,[PUNCTUATION_QUEST]        
     ,[DMP]        
     ,[EXPLOSION_DEGREE]        
     ,[PR_LIQUID]        
     ,[COD_ATIV_DRAFTS]        
     ,[OCCUPATION_TEXT]        
     ,[ASSIST24]        
     ,[LMRA]        
     ,[AGGRAVATION_RCG_AIR]        
     ,[EXPLOSION_DESC]        
     ,[PROTECTIVE_DESC]        
     ,[LMI_DESC]        
     ,[LOSS_DESC]        
     ,[QUESTIONNAIRE_DESC]        
     ,[DEDUCTIBLE_DESC]        
     ,[GROUPING_DESC]        
     ,[LOC_FLOATING]        
     ,[ADJUSTABLE]        
     ,[IS_ACTIVE]        
     ,[CREATED_BY]        
     ,[CREATED_DATETIME]        
     ,[MODIFIED_BY]        
     ,[LAST_UPDATED_DATETIME]        
     ,[CORRAL_SYSTEM]        
     ,[RAWVALUES]        
     ,[REMARKS]        
     ,[PARKING_SPACES]        
     ,[CLAIM_RATIO]        
     ,[RAW_MATERIAL_VALUE]        
     ,[CONTENT_VALUE]        
     ,[BONUS])        
     VALUES        
     ( @CUSTOMER_ID        
   ,@POLICY_ID        
   ,@POLICY_VERSION_ID        
   ,@PERIL_ID        
   ,@CALCULATION_NUMBER        
   ,@LOCATION        
   ,@ADDRESS        
   ,@NUMBER        
   ,@COMPLEMENT        
   ,@CITY        
   ,@COUNTRY        
   ,@STATE        
   ,@ZIP        
   ,@TELEPHONE        
   ,@EXTENTION        
   ,@FAX        
   ,@CATEGORY        
   ,@ATIV_CONTROL        
   ,@LOC        
   ,@LOCALIZATION        
   ,@OCCUPANCY        
   ,@CONSTRUCTION        
   ,@LOC_CITY        
   ,@CONSTRUCTION_TYPE        
   ,@ACTIVITY_TYPE        
   ,@RISK_TYPE        
   ,@VR        
   ,@LMI        
   ,@BUILDING        
   ,@MMU        
   ,@MMP        
   ,@MRI        
   ,@TYPE        
   ,@LOSS        
   ,@LOYALTY        
   ,@PERC_LOYALTY        
   ,@DEDUCTIBLE_OPTION        
   ,@MULTIPLE_DEDUCTIBLE        
   ,@E_FIRE        
   ,@S_FIXED_FOAM        
   ,@S_FIXED_INSERT_GAS        
   ,@CAR_COMBAT        
   ,@S_DETECT_ALARM        
   ,@S_FIRE_UNIT        
   ,@S_FOAM_PER_MANUAL        
   ,@S_MANUAL_INERT_GAS        
   ,@S_SEMI_HOSES        
   ,@HYDRANTS        
   ,@SHOWERS        
   ,@SHOWER_CLASSIFICATION        
   ,@FIRE_CORPS        
   ,@PUNCTUATION_QUEST        
   ,@DMP        
   ,@EXPLOSION_DEGREE        
   ,@PR_LIQUID        
   ,@COD_ATIV_DRAFTS        
   ,@OCCUPATION_TEXT        
   ,@ASSIST24        
   ,@LMRA        
   ,@AGGRAVATION_RCG_AIR        
   ,@EXPLOSION_DESC        
   ,@PROTECTIVE_DESC        
   ,@LMI_DESC        
   ,@LOSS_DESC        
   ,@QUESTIONNAIRE_DESC        
   ,@DEDUCTIBLE_DESC        
   ,@GROUPING_DESC        
   ,@LOC_FLOATING        
   ,@ADJUSTABLE        
   ,@IS_ACTIVE        
   ,@CREATED_BY        
   ,@CREATED_DATETIME        
   ,@MODIFIED_BY        
   ,@LAST_UPDATED_DATETIME        
   ,@CORRAL_SYSTEM        
   ,@RAWVALUES        
   ,@REMARKS        
   ,@PARKING_SPACES        
   ,@CLAIM_RATIO        
   ,@RAW_MATERIAL_VALUE        
   ,@CONTENT_VALUE        
   ,@BONUS        
   )        
            
  --------------------------------------POL_PRODUCT_COVERAGES----------------------------------------        
   DECLARE         
     --@CUSTOMER_ID int,         
     --@POLICY_ID int,         
     --@POLICY_VERSION_ID smallint,         
     --@RISK_ID smallint,         
     @COVERAGE_ID int,         
     @COVERAGE_CODE_ID int,         
     @RI_APPLIES nchar (2),        
     @LIMIT_OVERRIDE nvarchar (10),        
     @LIMIT_1 decimal (18,0),        
     @LIMIT_1_TYPE nvarchar (10),        
     @LIMIT_2 decimal (18,0),        
     @LIMIT_2_TYPE nvarchar (10),        
     @LIMIT1_AMOUNT_TEXT nvarchar (200),        
     @LIMIT2_AMOUNT_TEXT nvarchar (200),        
     @DEDUCT_OVERRIDE nchar (2),        
     @DEDUCTIBLE_1 decimal (18,0),        
     @DEDUCTIBLE_1_TYPE nvarchar (12),        
     @DEDUCTIBLE_2 decimal (18,0),        
     @DEDUCTIBLE_2_TYPE nvarchar (10),        
     @MINIMUM_DEDUCTIBLE decimal (18,0),        
     @DEDUCTIBLE1_AMOUNT_TEXT nvarchar (1000),        
     @DEDUCTIBLE2_AMOUNT_TEXT nvarchar (2000),        
     @DEDUCTIBLE_REDUCES nvarchar (2),        
     @INITIAL_RATE decimal (8,4),        
     @FINAL_RATE decimal (8,4),        
     @AVERAGE_RATE nchar (2),        
     @WRITTEN_PREMIUM decimal (18,2),        
     @FULL_TERM_PREMIUM decimal (18,2),        
     @IS_SYSTEM_COVERAGE nchar (2),        
     @LIMIT_ID int,         
     @DEDUC_ID int ,        
     @ADD_INFORMATION nvarchar (40),        
     --@CREATED_BY int,         
     --@CREATED_DATETIME datetime,         
     --@MODIFIED_BY int,         
     --@LAST_UPDATED_DATETIME datetime,         
     @INDEMNITY_PERIOD int         
             
     --SET  @CUSTOMER_ID = 2525        
     --SET  @POLICY_ID = 1        
     --SET  @POLICY_VERSION_ID = 1        
     SET  @RISK_ID = 1        
     SET  @COVERAGE_ID = 1        
    -- SET  @COVERAGE_CODE_ID = 1086        
     SET  @RI_APPLIES = 'N'        
     SET  @LIMIT_OVERRIDE =NULL         
     SET  @LIMIT_1 = 5000        
     SET  @LIMIT_1_TYPE = NULL        
     SET  @LIMIT_2 = NULL        
     SET  @LIMIT_2_TYPE =NULL         
     SET  @LIMIT1_AMOUNT_TEXT = NULL        
     SET  @LIMIT2_AMOUNT_TEXT = NULL        
     SET  @DEDUCT_OVERRIDE = NULL        
     SET  @DEDUCTIBLE_1 = NULL        
     SET  @DEDUCTIBLE_1_TYPE =14573         
     SET  @DEDUCTIBLE_2 = NULL        
     SET  @DEDUCTIBLE_2_TYPE =NULL         
     SET  @MINIMUM_DEDUCTIBLE = NULL        
     SET  @DEDUCTIBLE1_AMOUNT_TEXT =NULL         
     SET  @DEDUCTIBLE2_AMOUNT_TEXT = ''        
     SET  @DEDUCTIBLE_REDUCES = 'N'        
     SET  @INITIAL_RATE =NULL         
     SET  @FINAL_RATE = NULL        
     SET  @AVERAGE_RATE ='N'          
     SET  @WRITTEN_PREMIUM =5000.00        
     SET  @FULL_TERM_PREMIUM = NULL        
     SET  @IS_SYSTEM_COVERAGE = NULL        
     SET  @LIMIT_ID = NULL        
     SET  @DEDUC_ID = NULL        
     SET  @ADD_INFORMATION = NULL        
     SET  @CREATED_BY = 198           
     SET  @CREATED_DATETIME = '2010-10-27 15:42:38.703'        
     SET  @MODIFIED_BY = 198        
     SET  @LAST_UPDATED_DATETIME ='2010-10-27 00:00:00.000'         
     SET  @INDEMNITY_PERIOD = 4                 
  
        
     
      
        
     SET  @COVERAGE_CODE_ID = 1086      
             
   INSERT INTO [POL_PRODUCT_COVERAGES]    --1    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
    ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
   ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)      
         
         
         
     SET @COVERAGE_ID = @COVERAGE_ID + 1     
     SET  @COVERAGE_CODE_ID = 1087      
         
     INSERT INTO [POL_PRODUCT_COVERAGES]    --2    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)      
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1      
    SET  @COVERAGE_CODE_ID = 1089     
        
     INSERT INTO [POL_PRODUCT_COVERAGES]    --3    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1      
   SET  @COVERAGE_CODE_ID = 1090    
          
     INSERT INTO [POL_PRODUCT_COVERAGES]    --4    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1     
   SET  @COVERAGE_CODE_ID = 1104      
         
     INSERT INTO [POL_PRODUCT_COVERAGES]    --5    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1    
    SET  @COVERAGE_CODE_ID = 1108      
         
     INSERT INTO [POL_PRODUCT_COVERAGES]    --6    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1     
    SET  @COVERAGE_CODE_ID = 1109     
         
     INSERT INTO [POL_PRODUCT_COVERAGES]    --7    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]        
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
         
   SET @COVERAGE_ID = @COVERAGE_ID + 1    
    SET  @COVERAGE_CODE_ID = 1110      
         
     INSERT INTO [POL_PRODUCT_COVERAGES]    --8    
       ([CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]        
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]        
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]        
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]               ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]    ,[DEDUCTIBLE1_AMOUNT_TEXT]        
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]        
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]        
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]        
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD])        
    VALUES        
       ( @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID        
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE        
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE        
     ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE        
     ,@DEDUCTIBLE1_AMOUNT_TEXT    ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE        
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE        
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY        
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD)    
              
          
    -- END    
        
    ------------------------INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA----------------------    
    DECLARE     
   --@POLICY_ID int,      
   --@POLICY_VERSION_ID int,    
   --@CUSTOMER_ID int,    
   @PLAN_ID int,    
   --@APP_ID int,    
   --@APP_VERSION_ID int,    
   @PLAN_DESCRIPTION nvarchar (70),                
   @PLAN_TYPE nvarchar (20),                
   @NO_OF_PAYMENTS smallint,    
   @MONTHS_BETWEEN smallint,    
   @PERCENT_BREAKDOWN1 decimal (10,4),    
   @PERCENT_BREAKDOWN2 decimal (10,4),    
   @PERCENT_BREAKDOWN3 decimal (10,4),    
   @PERCENT_BREAKDOWN4 decimal (10,4),    
   @PERCENT_BREAKDOWN5 decimal (10,4),    
   @PERCENT_BREAKDOWN6 decimal (10,4),    
   @PERCENT_BREAKDOWN7 decimal (10,4),    
   @PERCENT_BREAKDOWN8 decimal (10,4),    
   @PERCENT_BREAKDOWN9 decimal (10,4),    
   @PERCENT_BREAKDOWN10 decimal (10,4),    
   @PERCENT_BREAKDOWN11 decimal (10,4),    
   @PERCENT_BREAKDOWN12 decimal (10,4),    
   @MODE_OF_DOWN_PAYMENT int,    
   @INSTALLMENTS_IN_DOWN_PAYMENT int,    
   @MODE_OF_PAYMENT int,    
   --@CURRENT_TERM smallint,    
   @IS_ACTIVE_PLAN char(1),                
   @TOTAL_PREMIUM decimal (12,2),    
   @TOTAL_INTEREST_AMOUNT decimal(12,2),    
   @TOTAL_FEES decimal (12,2),    
   @TOTAL_TAXES decimal (12,2),    
   @TOTAL_AMOUNT decimal (12,2),    
   @TRAN_TYPE nvarchar (20),    
   @TOTAL_TRAN_PREMIUM decimal (12,2),    
   @TOTAL_TRAN_INTEREST_AMOUNT decimal(12,2),    
   @TOTAL_TRAN_FEES decimal (12,2),    
   @TOTAL_TRAN_TAXES decimal (12,2),    
   @TOTAL_TRAN_AMOUNT decimal (12,2),    
   --@CREATED_BY int,     
   --@CREATED_DATETIME datetime,    
   --@MODIFIED_BY int,      
   --@LAST_UPDATED_DATETIME datetime,                 
   @TOTAL_CHANGE_INFORCE_PRM decimal (18,2),    
   @PRM_DIST_TYPE int,    
   @TOTAL_INFO_PRM int,    
   @TOTAL_STATE_FEES decimal(12, 2),    
   @TOTAL_TRAN_STATE_FEES decimal (12,2)    
   --@CO_APPLICANT_ID int       
       
       
    --SET @POLICY_ID =1    
    --SET @POLICY_VERSION_ID =1    
    --SET @CUSTOMER_ID =2220    
    SET @PLAN_ID = 55    
    --SET @APP_ID = 1    
    SET @APP_VERSION_ID =1    
    SET @PLAN_DESCRIPTION ='0 Down payment'    
    SET @PLAN_TYPE = NULL    
    SET @NO_OF_PAYMENTS =6    
    SET @MONTHS_BETWEEN =0    
    SET @PERCENT_BREAKDOWN1 =16.6650    
    SET @PERCENT_BREAKDOWN2 =16.6670    
    SET @PERCENT_BREAKDOWN3 =16.6670    
    SET @PERCENT_BREAKDOWN4 =16.6670    
    SET @PERCENT_BREAKDOWN5 =16.6670    
    SET @PERCENT_BREAKDOWN6 =16.6670    
    SET @PERCENT_BREAKDOWN7 =0.0000    
    SET @PERCENT_BREAKDOWN8 =0.0000    
    SET @PERCENT_BREAKDOWN9 =0.0000    
    SET @PERCENT_BREAKDOWN10 =0.0000    
    SET @PERCENT_BREAKDOWN11 =0.0000    
    SET @PERCENT_BREAKDOWN12 =0.0000    
    SET @MODE_OF_DOWN_PAYMENT =11974    
    SET @INSTALLMENTS_IN_DOWN_PAYMENT =0    
    SET @MODE_OF_PAYMENT =0    
    SET @CURRENT_TERM =1    
    SET @IS_ACTIVE_PLAN ='Y'    
    SET @TOTAL_PREMIUM =40000.00    
    SET @TOTAL_INTEREST_AMOUNT =0.00    
    SET @TOTAL_FEES =0.00    
    SET @TOTAL_TAXES =0.00    
    SET @TOTAL_AMOUNT =40000.00    
    SET @TRAN_TYPE ='NBS'    
    SET @TOTAL_TRAN_PREMIUM =40000.00    
    SET @TOTAL_TRAN_INTEREST_AMOUNT =0.00    
    SET @TOTAL_TRAN_FEES =0.00    
    SET @TOTAL_TRAN_TAXES =0.00    
    SET @TOTAL_TRAN_AMOUNT =40000.00    
    SET @CREATED_BY =198    
    SET @CREATED_DATETIME ='2010-10-29 12:53:30.000'    
    SET @MODIFIED_BY =NULL    
    SET @LAST_UPDATED_DATETIME =NULL    
    SET @TOTAL_CHANGE_INFORCE_PRM =40000.00    
    SET @PRM_DIST_TYPE =NULL    
    SET @TOTAL_INFO_PRM =40000    
    SET @TOTAL_STATE_FEES =0.00    
    SET @TOTAL_TRAN_STATE_FEES =0.00    
    SET @CO_APPLICANT_ID =0    
    SET @CREATED_BY=198        
    SET @CREATED_DATETIME='2010-10-27 10:08:07.233'        
    SET @MODIFIED_BY=NULL        
    SET @LAST_UPDATED_DATETIME=NULL        
       
      
        
    INSERT INTO [ACT_POLICY_INSTALL_PLAN_DATA]    
       ([POLICY_ID]    
       ,[POLICY_VERSION_ID]    
       ,[CUSTOMER_ID]    
       ,[PLAN_ID]    
       ,[APP_ID]    
       ,[APP_VERSION_ID]    
       ,[PLAN_DESCRIPTION]    
       ,[PLAN_TYPE]    
       ,[NO_OF_PAYMENTS]    
       ,[MONTHS_BETWEEN]    
       ,[PERCENT_BREAKDOWN1]    
       ,[PERCENT_BREAKDOWN2]    
       ,[PERCENT_BREAKDOWN3]    
       ,[PERCENT_BREAKDOWN4]    
       ,[PERCENT_BREAKDOWN5]    
       ,[PERCENT_BREAKDOWN6]    
       ,[PERCENT_BREAKDOWN7]    
       ,[PERCENT_BREAKDOWN8]    
       ,[PERCENT_BREAKDOWN9]    
       ,[PERCENT_BREAKDOWN10]    
       ,[PERCENT_BREAKDOWN11]    
       ,[PERCENT_BREAKDOWN12]    
       ,[MODE_OF_DOWN_PAYMENT]    
       ,[INSTALLMENTS_IN_DOWN_PAYMENT]    
       ,[MODE_OF_PAYMENT]    
       ,[CURRENT_TERM]    
       ,[IS_ACTIVE_PLAN]    
       ,[TOTAL_PREMIUM]    
       ,[TOTAL_INTEREST_AMOUNT]    
       ,[TOTAL_FEES]    
       ,[TOTAL_TAXES]    
       ,[TOTAL_AMOUNT]    
       ,[TRAN_TYPE]    
       ,[TOTAL_TRAN_PREMIUM]    
       ,[TOTAL_TRAN_INTEREST_AMOUNT]    
       ,[TOTAL_TRAN_FEES]    
       ,[TOTAL_TRAN_TAXES]    
       ,[TOTAL_TRAN_AMOUNT]    
       ,[CREATED_BY]    
       ,[CREATED_DATETIME]    
       ,[MODIFIED_BY]    
       ,[LAST_UPDATED_DATETIME]    
       ,[TOTAL_CHANGE_INFORCE_PRM]    
       ,[PRM_DIST_TYPE]    
       ,[TOTAL_INFO_PRM]    
       ,[TOTAL_STATE_FEES]    
       ,[TOTAL_TRAN_STATE_FEES]    
       ,[CO_APPLICANT_ID])    
    VALUES    
       ( @POLICY_ID    
     ,@POLICY_VERSION_ID    
     ,@CUSTOMER_ID    
     ,@PLAN_ID    
     ,@APP_ID    
     ,@APP_VERSION_ID    
     ,@PLAN_DESCRIPTION    
     ,@PLAN_TYPE    
     ,@NO_OF_PAYMENTS    
     ,@MONTHS_BETWEEN    
     ,@PERCENT_BREAKDOWN1    
     ,@PERCENT_BREAKDOWN2    
     ,@PERCENT_BREAKDOWN3    
     ,@PERCENT_BREAKDOWN4    
     ,@PERCENT_BREAKDOWN5    
     ,@PERCENT_BREAKDOWN6    
     ,@PERCENT_BREAKDOWN7    
     ,@PERCENT_BREAKDOWN8    
     ,@PERCENT_BREAKDOWN9    
     ,@PERCENT_BREAKDOWN10    
     ,@PERCENT_BREAKDOWN11    
     ,@PERCENT_BREAKDOWN12    
     ,@MODE_OF_DOWN_PAYMENT    
     ,@INSTALLMENTS_IN_DOWN_PAYMENT    
     ,@MODE_OF_PAYMENT    
     ,@CURRENT_TERM    
     ,@IS_ACTIVE_PLAN    
     ,@TOTAL_PREMIUM    
     ,@TOTAL_INTEREST_AMOUNT    
     ,@TOTAL_FEES    
     ,@TOTAL_TAXES    
     ,@TOTAL_AMOUNT    
     ,@TRAN_TYPE    
     ,@TOTAL_TRAN_PREMIUM    
     ,@TOTAL_TRAN_INTEREST_AMOUNT    
     ,@TOTAL_TRAN_FEES    
     ,@TOTAL_TRAN_TAXES    
     ,@TOTAL_TRAN_AMOUNT    
     ,@CREATED_BY    
     ,@CREATED_DATETIME    
     ,@MODIFIED_BY    
     ,@LAST_UPDATED_DATETIME    
     ,@TOTAL_CHANGE_INFORCE_PRM    
     ,@PRM_DIST_TYPE    
     ,@TOTAL_INFO_PRM    
     ,@TOTAL_STATE_FEES    
     ,@TOTAL_TRAN_STATE_FEES    
     ,@CO_APPLICANT_ID    
     )    
      
       
       
         
        
        
        
             
           
  -----------------------------INSERT INTO ACT_POLICY_INSTALLMENT_DETAILS---------------------        
   --ACT_POLICY_INSTALL_PLAN_DATA      
   --1 3 2187 1 1 61.33 2010-02-28 00:00:00.000 Y 20999 6 1 B 11972 2 16.6666       
   DECLARE         
    --@POLICY_ID int         
    --@POLICY_VERSION_ID int         
    --@CUSTOMER_ID int         
    --@APP_ID int         
    --@APP_VERSION_ID int         
    @INSTALLMENT_AMOUNT decimal (25,2),        
    @INSTALLMENT_EFFECTIVE_DATE datetime,         
    @RELEASED_STATUS char (1),        
    @ROW_ID int,         
    @INSTALLMENT_NO int,         
    --@RISK_ID int,         
    --@RISK_TYPE varchar 15,        
    @PAYMENT_MODE int,         
    --@CURRENT_TERM smallint,         
    @PERCENTAG_OF_PREMIUM decimal (9,4),        
    @INTEREST_AMOUNT decimal (12,2),        
    @FEE decimal (12,2),        
    @TAXES decimal (12,2),        
    @TOTAL decimal (12,2),        
    @TRAN_INTEREST_AMOUNT decimal (12,2),        
    @TRAN_FEE decimal (12,2),        
    @TRAN_TAXES decimal (12,2),        
    @TRAN_TOTAL decimal (12,2),        
    @BOLETO_NO nvarchar (200),        
    @IS_BOLETO_GENRATED nchar (2),        
    --@CREATED_BY int,        
    --@CREATED_DATETIME datetime,         
    --@MODIFIED_BY int,         
    --@LAST_UPDATED_DATETIME datetime,         
    @TRAN_PREMIUM_AMOUNT decimal (12,2)        
            
            
    --SET  @POLICY_ID = 1       
    --SET  @POLICY_VERSION_ID =1        
   -- SET  @CUSTOMER_ID = 2525       
    --SET  @APP_ID =  1      
    SET  @APP_VERSION_ID = 1       
    SET  @INSTALLMENT_AMOUNT =  6666.00      
    SET  @INSTALLMENT_EFFECTIVE_DATE =  '2010-10-27 00:00:00.000'      
    SET  @RELEASED_STATUS = 'N'       
    SET  @ROW_ID =1        
    --SET  @INSTALLMENT_NO =        
    SET  @RISK_ID = 1       
    SET  @RISK_TYPE = 'ARPERIL'       
    SET  @PAYMENT_MODE = 11974       
    SET  @CURRENT_TERM =  1      
    SET  @PERCENTAG_OF_PREMIUM = 16.6650       
    SET  @INTEREST_AMOUNT = 10.00       
    SET  @FEE =    100.00  
    SET  @TAXES =  0.00      
    SET  @TOTAL =  6666.00     
    SET  @TRAN_INTEREST_AMOUNT = 0.00       
    SET  @TRAN_FEE =  0.00      
    SET  @TRAN_TAXES =   0.00     
    SET  @TRAN_TOTAL = 0.00       
    SET  @BOLETO_NO =  NULL      
    SET  @IS_BOLETO_GENRATED = NULL       
    SET  @CREATED_BY = 198       
    SET  @CREATED_DATETIME = '2010-10-29 12:53:30.000'       
    SET  @MODIFIED_BY = NULL       
    SET  @LAST_UPDATED_DATETIME = NULL       
    SET  @TRAN_PREMIUM_AMOUNT =  0.00    
        
        
    SET  @INSTALLMENT_NO = 1    
    SET  @PERCENTAG_OF_PREMIUM = 16.6650      
         
    SET IDENTITY_INSERT [ACT_POLICY_INSTALLMENT_DETAILS] ON    
      
      
            
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]  --1      
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL   ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)      
         
     SET  @INSTALLMENT_NO = @INSTALLMENT_NO + 1    
     SET  @PERCENTAG_OF_PREMIUM = 16.6670    
     SET  @FEE =    0.00  
     SET @ROW_ID =@ROW_ID +1  
     SET @INSTALLMENT_AMOUNT =  6666.80      
            
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]    --2    
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES] ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES    ,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL    ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)      
         
     SET  @INSTALLMENT_NO = @INSTALLMENT_NO + 1    
     SET  @PERCENTAG_OF_PREMIUM = 16.6670    
     SET @ROW_ID =@ROW_ID +1     
            
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]    --3    
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES    ,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL    ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)      
         
     SET  @INSTALLMENT_NO = @INSTALLMENT_NO +1    
     SET  @PERCENTAG_OF_PREMIUM = 16.6670    
   SET @ROW_ID =@ROW_ID +1    
          
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]    --4    
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES    ,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL    ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)      
         
     SET  @INSTALLMENT_NO = @INSTALLMENT_NO + 1    
   SET  @PERCENTAG_OF_PREMIUM = 16.6670    
    SET @ROW_ID =@ROW_ID +1    
           
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]    --5    
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES    ,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL    ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)     
         
         
     SET  @INSTALLMENT_NO = @INSTALLMENT_NO + 1    
     SET  @PERCENTAG_OF_PREMIUM = 16.6670    
   SET @ROW_ID =@ROW_ID +1    
           
    INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]    --6    
       ([POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]        
       ,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]        
       ,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]        
       ,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]        
       ,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]        
       ,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]        
       ,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT])        
       VALUES        
      ( @POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID        
     ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@ROW_ID        
     ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM        
     ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@FEE+@INTEREST_AMOUNT+@TAXES    ,@TRAN_INTEREST_AMOUNT        
     ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL    ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY        
     ,@CREATED_DATETIME    ,@MODIFIED_BY    ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT)      
         
        
     SET IDENTITY_INSERT [ACT_POLICY_INSTALLMENT_DETAILS] off    
       
       
       
     --select * from ACT_POLICY_INSTALL_PLAN_DATA WHERE CUSTOMER_ID = 2222 AND POLICY_ID = 1  
       
       
     SET @TOTAL_PREMIUM =0.00  
     SET @TOTAL_INTEREST_AMOUNT =0.00  
     SET @TOTAL_FEES = 0.00  
     SET @TOTAL_TAXES = 0.00  
     SET @TOTAL_AMOUNT = 0.00  
       
    SELECT    @TOTAL_PREMIUM = SUM(INSTALLMENT_AMOUNT) ,@TOTAL_INTEREST_AMOUNT = SUM(INTEREST_AMOUNT),  
          @TOTAL_FEES = SUM(FEE),@TOTAL_TAXES=SUM(TAXES),@TOTAL_AMOUNT = SUM(TOTAL)  
    FROM  [ACT_POLICY_INSTALLMENT_DETAILS] WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
    GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID  
            
            
    PRINT @TOTAL_PREMIUM  
    PRINT @TOTAL_INTEREST_AMOUNT  
    PRINT @TOTAL_FEES  
    PRINT @TOTAL_TAXES  
    PRINT @TOTAL_AMOUNT  
      
    -----------UPDATE ACT_POLICY_INSTALL_PLAN_DATA WITH ABOVE VALUES-------------  
      
    UPDATE ACT_POLICY_INSTALL_PLAN_DATA  
    SET TOTAL_PREMIUM =@TOTAL_PREMIUM,TOTAL_INTEREST_AMOUNT=@TOTAL_INTEREST_AMOUNT,  
     TOTAL_FEES = @TOTAL_FEES,TOTAL_TAXES=@TOTAL_TAXES,TOTAL_AMOUNT=@TOTAL_AMOUNT  
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
           
      END  
END 
GO

