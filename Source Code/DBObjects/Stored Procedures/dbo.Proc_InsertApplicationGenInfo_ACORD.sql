IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertApplicationGenInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertApplicationGenInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Proc Name       : dbo.Proc_InsertApplicationGenInfo_ACORD                                      
Created by      : Pradeep                                      
Date            : 4/25/2005                                      
Purpose       :Insertion                                       
Revison History :                                      
Used In        : Wolverine                                      
                                      
Modified By : Anurag Verma                                      
Modified On : 13/07/2005                                      
Purpose  : Inserting Policy_type in insert statement      

Modified By : Praveen kasana                                      
Modified On : 17/03/2008                                     
Purpose  : Inserting Insurance Resons Codes.                 
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                         
--drop PROC dbo.Proc_InsertApplicationGenInfo_ACORD                                   
CREATE PROC dbo.Proc_InsertApplicationGenInfo_ACORD                                     
(                                      
 @CUSTOMER_ID     int,                                      
 @APP_ID     int   output  ,                                      
 @APP_VERSION_ID     smallint,                                      
 @PARENT_APP_VERSION_ID     smallint,                                      
 --@APP_STATUS     nvarchar(10),                      
 @APP_STATUS     nvarchar(25),                       
  --@APP_NUMBER     nvarchar(150),                      
 @APP_NUMBER     nvarchar(75),                                     
 --@APP_VERSION     nvarchar(6),                      
 @APP_VERSION     nvarchar(4),                       
 --@APP_TERMS     nvarchar(10),                      
 @APP_TERMS     nvarchar(5),                                        
 @APP_INCEPTION_DATE     datetime=null,                                      
 @APP_EFFECTIVE_DATE     datetime,                                      
 @APP_EXPIRATION_DATE     datetime,                                      
 --@APP_LOB     nvarchar(10),                      
 @APP_LOB     nvarchar(5),                                        
 --@APP_SUBLOB     nvarchar(10),                      
 @APP_SUBLOB     nvarchar(5),                      
 @CSR     int,                                      
 @UNDERWRITER     int,                                      
 --@IS_UNDER_REVIEW     nchar(2),                      
 @IS_UNDER_REVIEW     nchar(1),                      
 @APP_AGENCY_ID     smallint,                                      
 --@IS_ACTIVE     nchar(2),                      
 @IS_ACTIVE     nchar(1),                      
 @CREATED_BY     int,                                      
 @CREATED_DATETIME     datetime,                                      
 @MODIFIED_BY     int,                                      
 @LAST_UPDATED_DATETIME     datetime,                                      
 @COUNTRY_ID int,                                      
 @STATE_CODE NVarChar(100),                                      
 @DIV_ID     smallint,                                      
 @DEPT_ID     smallint,                                      
 @PC_ID     smallint,                                      
 @BILL_TYPE     char(2) ='DB',                                                       
 @COMPLETE_APP     char(1),                                      
 @PROPRTY_INSP_CREDIT     char(1),                                      
 @INSTALL_PLAN_ID int,      -- may be removed as we are not using this.We are setting the default installment plan                     
 @CHARGE_OFF_PRMIUM varchar(5) ='10964'   ,                                  
 @RECEIVED_PRMIUM decimal(18,2),                                      
 @PROXY_SIGN_OBTAINED int,            
 @POLICY_TYPE int,                                      
 @POLICY_TYPE_CODE NVarChar(20),                                      
 @NEW_APP_NUM VarChar(20) OUTPUT,                                      
 @YEARS_AT_PREV_ADD varchar(250) =null,                      
 @YEAR_AT_CURR_RESI int =null                                      
)                                      
AS                   
BEGIN                                      
DECLARE @STATE_ID Int                                      
DECLARE @LOB_ID Smallint                       
DECLARE @NEW_APP VarChar(20)                   
DECLARE @BILL_TYPE_ID int                
DECLARE @DEFAULT_PLAN int    
DECLARE @MODE_OF_DOWNPAY int                               

DECLARE @STATE_MICHIGAN nvarchar(18)      
SET @STATE_MICHIGAN ='MICHIGAN'      

DECLARE @STATE_INDIANA nvarchar(18)      
SET @STATE_INDIANA ='INDIANA'    

DECLARE @CARRIER_SYSTEM_ID VARCHAR(10)	                  
SET @CARRIER_SYSTEM_ID = 'W001'	

DECLARE @LOGGED_USER_SYSTEM_ID VARCHAR(10)

EXECUTE @STATE_ID = Proc_GetSTATE_ID_FROM_NAME 1,@STATE_CODE                                      

EXECUTE @LOB_ID = Proc_GetLOBID @APP_LOB                                       

IF ( @LOB_ID = 0 )                                      
BEGIN                                      
	RAISERROR ('LOB not found in LOB_MASTER', 16, 1)                                      
	RETURN                                      
END                                      

--default_plan              
--taking default plan based on app term if default does't exist then take system_generated_full_pay            
              
IF EXISTS(SELECT DEFAULT_PLAN FROM ACT_INSTALL_PLAN_DETAIL               
	WHERE CONVERT(VARCHAR ,ISNULL(APPLABLE_POLTERM,0)) = @APP_TERMS                
	AND DEFAULT_PLAN = 1 AND ISNULL(IS_ACTIVE,'Y')='Y')              
       
	SELECT @DEFAULT_PLAN= IDEN_PLAN_ID  
	FROM ACT_INSTALL_PLAN_DETAIL               
	WHERE CONVERT(VARCHAR ,ISNULL(APPLABLE_POLTERM,0)) = @APP_TERMS                
	AND DEFAULT_PLAN = 1 AND ISNULL(IS_ACTIVE,'Y')='Y'              
ELSE               
	SELECT @DEFAULT_PLAN= ISNULL(IDEN_PLAN_ID,0) FROM ACT_INSTALL_PLAN_DETAIL               
	WHERE SYSTEM_GENERATED_FULL_PAY = 1              
              
-------------  
--Mode of down payment----  
SELECT @MODE_OF_DOWNPAY = MODE_OF_DOWNPAY FROM ACT_INSTALL_PLAN_DETAIL   
WHERE IDEN_PLAN_ID=@DEFAULT_PLAN  
  
--------------------  
                        
-- Default bill type and charge off premium for all applications. -- added by nidhi.- jan 9,2006                        
      
set @CHARGE_OFF_PRMIUM ='10964'                              
set @IS_ACTIVE ='Y'                    
 --                  
IF ( @LOB_ID =1 or @LOB_ID =6)      --lob home or rental then Direct Bill to Agency      
 set @BILL_TYPE      ='DM'                        
ELSE      
 set @BILL_TYPE      ='DB'                        
      
                
EXECUTE @BILL_TYPE_ID = Proc_GetLookupID 'BLCODE',@BILL_TYPE                          


-- Ravindra (07-04-2007) : Bill TYpe will be 'DB' always DM is liikup value code 
-- where as in APP_LIST type will be saved which is 'DB' for Insured bill all terms
                      
 SET @BILL_TYPE      ='DB'                        

EXECUTE Proc_GenerateAppNumber_ACORD @LOB_ID,NULL,@NEW_APP OUTPUT                           
                                       
 IF @@ERROR <> 0                                      
 BEGIN                                      
  RAISERROR ('Unable to generate new Application number', 16, 1)                                      
  RETURN                                       
 END                                      
                        
--For Homeowners LOB-------------------------------------------------                                  
IF (@LOB_ID = 1)                        
BEGIN            
  
 IF ( LTRIM(RTRIM(UPPER(@STATE_CODE))) = @STATE_MICHIGAN)      
 BEGIN                                    
  EXECUTE @POLICY_TYPE = Proc_GetLookupID 'HOPTPM',@POLICY_TYPE_CODE                                      
 END                                    
        
 IF ( LTRIM(RTRIM(UPPER(@STATE_CODE))) = @STATE_INDIANA)                                    
 BEGIN                                    
  EXECUTE @POLICY_TYPE = Proc_GetLookupID 'HOPTYP',@POLICY_TYPE_CODE                              
 END                        
                                    
END                        
-------------------------------------------------------------------------                                    
                        
--For Rental LOB-------------------------------------------------                                  
IF (@LOB_ID = 6)                        
BEGIN                        
                         
 IF ( LTRIM(RTRIM(UPPER(@STATE_CODE))) = @STATE_MICHIGAN)                                    
 BEGIN                                    
  EXECUTE @POLICY_TYPE = Proc_GetLookupID 'RTPTYP',@POLICY_TYPE_CODE                          
 END                                    
                                     
 IF ( LTRIM(RTRIM(UPPER(@STATE_CODE))) = @STATE_INDIANA)                                    
 BEGIN                                    
  EXECUTE @POLICY_TYPE = Proc_GetLookupID 'RTPTYI',@POLICY_TYPE_CODE                                      
 END                        
                                    
END                        
-------------------------------------------------------------------------                                    
                                  
 SET @APP_VERSION = '1.0'                                      
 SET @NEW_APP_NUM = @NEW_APP                                       
                                      
 select @APP_ID=ISNULL(MAX(APP_ID),0)+1                                       
 from APP_LIST where CUSTOMER_ID = @CUSTOMER_ID;                                      
                                   
--Set default Expiration date and Term according to LOB--                                  
DECLARE @TERM Int                                  
SET @TERM = DATEDIFF(m,@APP_EFFECTIVE_DATE,@APP_EXPIRATION_DATE)                                  
-----------------                                  
                            
--SET @PROXY_SIGN_OBTAINED = 10964      
SET @PROXY_SIGN_OBTAINED = null                              
                    
--Set default billing plan                     
DECLARE @DEFAULT_INSTALLMENT_PLAN int                    
SELECT @DEFAULT_INSTALLMENT_PLAN = isnull(IDEN_PLAN_ID,0) FROM ACT_INSTALL_PLAN_DETAIL WHERE DEFAULT_PLAN =1 
AND ISNULL(IS_ACTIVE,'Y')='Y'
                    
                       
/*Insurance Score*/                    
DECLARE @APPLY_INSURANCE_SCORE numeric                    
                    
SELECT @APPLY_INSURANCE_SCORE= CUSTOMER_INSURANCE_SCORE                    
FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID                    
                    
                    
/*Insurance Score*/  


-- Fetch Logged in user's system ID and set value of Complete App Bonus accordingly

 SELECT @LOGGED_USER_SYSTEM_ID = USER_SYSTEM_ID FROM MNT_USER_LIST WHERE [USER_ID] = @CREATED_BY
 IF(@LOGGED_USER_SYSTEM_ID <> @CARRIER_SYSTEM_ID)
 BEGIN
  SET @COMPLETE_APP = 'Y'
 END     

--If this flag null when agency is "w001". For home employee this flag should be "Yes"
DECLARE @IS_HOME_EMP INT
SET @IS_HOME_EMP = 0
IF(SELECT  UPPER(AGENCY_CODE) FROM MNT_AGENCY_LIST WHERE AGENCY_ID = @APP_AGENCY_ID) = @CARRIER_SYSTEM_ID
BEGIN
 SET @IS_HOME_EMP = 1
END

                    
                                  
 INSERT INTO APP_LIST                                      
 (                                      
  CUSTOMER_ID,                                      
APP_ID,           
  APP_VERSION_ID,                                      
  PARENT_APP_VERSION_ID,                                      
  APP_STATUS,                                      
  APP_NUMBER,                                      
  APP_VERSION,                                  
  APP_TERMS,                                      
  APP_INCEPTION_DATE,                                      
  APP_EFFECTIVE_DATE,                                      
  APP_EXPIRATION_DATE,                                      
  APP_LOB,           
  APP_SUBLOB,                                      
  CSR,                       
  UNDERWRITER,                                      
  IS_UNDER_REVIEW,                                      
  APP_AGENCY_ID,                                      
  IS_ACTIVE,                                      
  CREATED_BY,                                      
  CREATED_DATETIME,                           
  MODIFIED_BY,                                      
  LAST_UPDATED_DATETIME,                                      
  COUNTRY_ID ,                                      
  STATE_ID ,                                  
  DIV_ID,                                      
  DEPT_ID,                                      
  PC_ID,                                      
  BILL_TYPE,                   
  BILL_TYPE_ID,                                     
  COMPLETE_APP,                        
  PROPRTY_INSP_CREDIT,                                      
  INSTALL_PLAN_ID,                                      
  CHARGE_OFF_PRMIUM,                                 
  RECEIVED_PRMIUM,                                      
  PROXY_SIGN_OBTAINED,                                      
  POLICY_TYPE,                                      
  YEARS_AT_PREV_ADD ,                                   
  YEAR_AT_CURR_RESI  ,                    
  APPLY_INSURANCE_SCORE,  
  DOWN_PAY_MODE,
  IS_HOME_EMP                                      
 )                                      
 VALUES                                      
 (                                      
  @CUSTOMER_ID,                                      
  @APP_ID,                           
  @APP_VERSION_ID,                                      
  @PARENT_APP_VERSION_ID,                                      
  @APP_STATUS,                         
  @NEW_APP,                                      
  @APP_VERSION,                                      
  @TERM,                                      
  @APP_INCEPTION_DATE,                                      
  @APP_EFFECTIVE_DATE,                                      
  @APP_EXPIRATION_DATE,                                      
  @LOB_ID,                                      
  @APP_SUBLOB,                                      
  @CSR,                                      
  @UNDERWRITER,                                      
  @IS_UNDER_REVIEW,                                      
  @APP_AGENCY_ID,                                      
  @IS_ACTIVE,                                      
  @CREATED_BY,                                      
  @CREATED_DATETIME,                                      
  @MODIFIED_BY,                          
  @LAST_UPDATED_DATETIME,                                      
  @COUNTRY_ID,                                      
  @STATE_ID,                                      
  @DIV_ID,                                      
  @DEPT_ID,                                      
  @PC_ID,                                      
  @BILL_TYPE,                  
  @BILL_TYPE_ID,                                  
  @COMPLETE_APP,                                      
  @PROPRTY_INSP_CREDIT,                                      
  @DEFAULT_PLAN,                                      
  @CHARGE_OFF_PRMIUM,                                      
  @RECEIVED_PRMIUM,                                      
  @PROXY_SIGN_OBTAINED,                
  @POLICY_TYPE,                                      
  @YEARS_AT_PREV_ADD,                                      
  @YEAR_AT_CURR_RESI  ,                    
  @APPLY_INSURANCE_SCORE,  
  @MODE_OF_DOWNPAY,
  @IS_HOME_EMP              
 )       
                          
                         
--Insert into CLT_APPLICANT_LIST                          
                          
DECLARE @APPLICANT_ID int                              
SET @APPLICANT_ID=0                    
SELECT @APPLICANT_ID = APPLICANT_ID                               
FROM CLT_APPLICANT_LIST                              
WHERE CUSTOMER_ID = @CUSTOMER_ID                           
 AND IS_PRIMARY_APPLICANT = 1                            
                          
/*                          
IF ( @APPLICANT_ID IS NOT NULL )                          
                              
BEGIN                              
 SELECT @APPCOUNT =  COUNT(*) FROM APP_APPLICANT_LIST                              
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                           
  APP_ID= @APP_ID AND                           
  APP_VERSION_ID = @APP_VERSION_ID                           
  AND APPLICANT_ID=@APPLICANT_ID                              
END                              
*/                          
                          
                          
 INSERT INTO APP_APPLICANT_LIST                              
 (                              
  APPLICANT_ID,                          
  CUSTOMER_ID,                          
  APP_ID,                          
  APP_VERSION_ID,                          
  CREATED_BY,                          
  CREATED_DATETIME,                          
  IS_PRIMARY_APPLICANT                              
 )                              
 VALUES                              
 (                              
  @APPLICANT_ID,
  @CUSTOMER_ID,                          
  @APP_ID,                          
  @APP_VERSION_ID,                          
  @CREATED_BY,                          
  GETDATE(),                          
  1                              
 )   

--Update Insurance Reasons


--By praveen
DECLARE @CUSTOMER_REASON_CODE nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE2 nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE3 nvarchar(10)  
DECLARE @CUSTOMER_REASON_CODE4 nvarchar(10)  

  
SELECT 

@CUSTOMER_REASON_CODE = ISNULL(CUSTOMER_REASON_CODE,''),
@CUSTOMER_REASON_CODE2 = ISNULL(CUSTOMER_REASON_CODE2,''),
@CUSTOMER_REASON_CODE3 = ISNULL(CUSTOMER_REASON_CODE3,''),
@CUSTOMER_REASON_CODE4 = ISNULL(CUSTOMER_REASON_CODE4,'')

FROM CLT_CUSTOMER_LIST  with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID

UPDATE  APP_LIST SET 
CUSTOMER_REASON_CODE = @CUSTOMER_REASON_CODE ,
CUSTOMER_REASON_CODE2 = @CUSTOMER_REASON_CODE2 ,
CUSTOMER_REASON_CODE3 = @CUSTOMER_REASON_CODE3  ,
CUSTOMER_REASON_CODE4 = @CUSTOMER_REASON_CODE4      
WHERE  
CUSTOMER_ID=@CUSTOMER_ID 
AND APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID    

--end     
                          
                                    
END                            
                  
              
            
          
        
      
    
  
  













GO

