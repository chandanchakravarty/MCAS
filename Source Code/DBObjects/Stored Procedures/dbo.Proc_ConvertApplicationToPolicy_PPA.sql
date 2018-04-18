IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_PPA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_PPA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                        
Modify By:    Pravesh Chandel                        
Modify Date : 18 Oct 2006                        
Purpose     : Correct DATE_EXP_START field value from DATE_LICENSED and add NO_CYCLE_ENDMT in POL_DRIVER_DETAILS and UnderWriter field in todolist                        
Modify By:    Pravesh Chandel                        
Modify Date : 26 Oct 2006                        
Purpose     : column ADD_INFORMATION was not copied from app to pol                
  NO_OF_DEPENDENTS IN  POL_DRIVER_DETAILS AND SEAT_BELT_CREDIT COLUMN IN POL_AUTO_GEN_INFO                
*/           
--drop PROC dbo.Proc_ConvertApplicationToPolicy_PPA                       
CREATE PROC dbo.Proc_ConvertApplicationToPolicy_PPA                                                                               
@CUSTOMER_ID INT,                                                                
@APP_ID INT,                                                                                                        
@APP_VERSION_ID SMALLINT,                                                                                                        
@CREATED_BY INT,                                                                                                        
@PARAM1 INT = NULL,                                                                                                        
@PARAM2 INT = NULL,                                                                                                        
@PARAM3 INT = NULL,                                                                          
@CALLED_FROM NVARCHAR(30),                                                                        
@RESULT INT OUTPUT                                                                 
AS                                                                                                        
BEGIN                             
                      
---ADDED BY PRAVESH                      
BEGIN TRAN                                                                         
                      
DECLARE @TEMP_ERROR_CODE INT                                                                                                        
EXEC     @RESULT=Proc_ConvertApplicationToPolicy_ALL  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,@PARAM1,@PARAM2,@PARAM3,@CALLED_FROM,@RESULT                                                                                          
                      
IF (@RESULT = -1)  GOTO PROBLEM                      
                                                                                                      
DECLARE @TEMP_POLICY_ID INT                                                                                                         
DECLARE @TEMP_POLICY_VERSION_ID INT                      
SET @TEMP_POLICY_ID=@RESULT                      
SELECT   @TEMP_POLICY_VERSION_ID = POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                       
--END HERE                      
/*                                                          
IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                                           
           WHERE CUSTOMER_ID = @CUSTOMER_ID  AND APP_ID = @APP_ID)                                                          
                                                          
BEGIN                                                          
 SET @RESULT = -1                                                          
 RETURN                                                          
END                                                          
ELSE                                                          
BEGIN                                                          
                             
BEGIN TRAN                     
DECLARE @TEMP_ERROR_CODE INT          
                 
DECLARE @TEMP_POLICY_ID INT                                                                                      
DECLARE @TEMP_POLICY_VERSION_ID INT                                
                    
                                                                                                        
SELECT @TEMP_POLICY_ID = MAX(ISNULL(POLICY_ID,0))+1  FROM POL_CUSTOMER_POLICY_LIST                                                                                     
WHERE CUSTOMER_ID = @CUSTOMER_ID                                                                                       
                                                                                         
IF @TEMP_POLICY_ID IS NULL OR @TEMP_POLICY_ID = ''                                                   
BEGIN                                  
 SET @TEMP_POLICY_ID = 1                                                                                                        
END                                                                                                        
                                                                        
                                                                                                        
SET @TEMP_POLICY_VERSION_ID = 1                                                                                                        
-- 1. POL_CUSTOMER_POLICY_LIST                                                                             
--check for submit anyway                                                                          
if @CALLED_FROM = 'ANYWAY'                                                     
INSERT INTO POL_CUSTOMER_POLICY_LIST                                                
(                                 
 CUSTOMER_ID,                              
 POLICY_ID,                                                                                                         
 POLICY_VERSION_ID,                                                                                                         
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
 POLICY_LOB,                                                                                                        
 POLICY_SUBLOB,                                                                                                   
 CSR,                                                                                                   
 UNDERWRITER,                                                                
 IS_UNDER_REVIEW,                                                                                                        
 AGENCY_ID,                                                                                                        
 IS_ACTIVE,                                                                     
 CREATED_BY,                                                  
 CREATED_DATETIME,                                              
 MODIFIED_BY,                                       
 LAST_UPDATED_DATETIME,                                                       
 COUNTRY_ID,            
 STATE_ID,                                                                                
 DIV_ID,                                                        
 DEPT_ID,                                                                                                        
 PC_ID,                                                                                        
 BILL_TYPE,                                                                      
 BILL_TYPE_ID,                                                                                              
 COMPLETE_APP,                                                                        
 PROPRTY_INSP_CREDIT,                                                                                                        
 INSTALL_PLAN_ID,                                                                                         CHARGE_OFF_PRMIUM,                                                                                                        
 RECEIVED_PRMIUM,                                                                                                        
 PROXY_SIGN_OBTAINED,                      
 POLICY_TYPE,                                                                                                        
 SHOW_QUOTE,                                                                                   
 APP_VERIFICATION_XML,                                                                                                        
 YEAR_AT_CURR_RESI,                                                                                           
 YEARS_AT_PREV_ADD,                                                                                                        
 POLICY_STATUS,                                                                                                        
 POLICY_NUMBER,                                                             
 POLICY_DISP_VERSION,                                                                                  
 POLICY_EFFECTIVE_DATE ,                              
 IS_HOME_EMP                                                                                                       
)                                                                                    
                                             
SELECT                                                                                                         
 @CUSTOMER_ID ,                                      
 @TEMP_POLICY_ID,                                              
 @TEMP_POLICY_VERSION_ID,                                                                                                        
 @APP_ID,                                                                                   
 @APP_VERSION_ID,                                                                                                        
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
 @CREATED_BY,                                                                                                        
 GETDATE(),                                                                                                        
 MODIFIED_BY,                                      
 LAST_UPDATED_DATETIME,                                                                                                     
 COUNTRY_ID,                                                                                                        
 STATE_ID,                                                                                                      
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
 SHOW_QUOTE,                                                                                                        
 APP_VERIFICATION_XML,                                                      
 YEAR_AT_CURR_RESI,                                                                                                        
 YEARS_AT_PREV_ADD,                                                                         
 'Suspended',                                                                                                        
 SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0' ,                                                                                  
 APP_EFFECTIVE_DATE,                                   
 IS_HOME_EMP                                                                                                       
 FROM APP_LIST                                                                                                
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                              
 -- ADDED ON 20-12-2005                                                         
 AND IS_ACTIVE = 'Y'                                                        
else                                                                      
INSERT INTO POL_CUSTOMER_POLICY_LIST                                                                                                         
(                                                  
 CUSTOMER_ID,                                                                                                         
 POLICY_ID,                                                               
 POLICY_VERSION_ID,                                                 
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
 POLICY_LOB,                                                                                                        
 POLICY_SUBLOB,                                                                                                        
 CSR,                                                                                                   
 UNDERWRITER,                                                                                                        
 IS_UNDER_REVIEW,                                                                                                        
 AGENCY_ID,                                                                                                        
 IS_ACTIVE,                                                               
 CREATED_BY,                               
 CREATED_DATETIME,                                                                                                        
 MODIFIED_BY,                                                  
 LAST_UPDATED_DATETIME,                                                                                                        
 COUNTRY_ID,                                                                                                        
 STATE_ID,                                                                        
 DIV_ID,                                                                                                        
 DEPT_ID,                                                                     
 PC_ID,                                                                                                        
 BILL_TYPE,                                                                                                        
 COMPLETE_APP,                                                                                                        
 PROPRTY_INSP_CREDIT,                                                                                                        
 INSTALL_PLAN_ID,                                               
 CHARGE_OFF_PRMIUM,                                                                                                        
 RECEIVED_PRMIUM,                                                                                                        
 PROXY_SIGN_OBTAINED,                                                                 
 POLICY_TYPE,                                      
 SHOW_QUOTE,                         
 APP_VERIFICATION_XML,                                                                   
 YEAR_AT_CURR_RESI,                                                            
 YEARS_AT_PREV_ADD,                                                                      
 POLICY_STATUS,                                                                                  
 POLICY_NUMBER,                                                                        
POLICY_DISP_VERSION,                           
 POLICY_EFFECTIVE_DATE ,                         
 IS_HOME_EMP                        
)                                   
                
SELECT                                                                                                         
 @CUSTOMER_ID ,                                                                      
 @TEMP_POLICY_ID,                                                      
 @TEMP_POLICY_VERSION_ID,                                                                                                        
 @APP_ID,                                                                                                  
 @APP_VERSION_ID,                                                                                 
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
 @CREATED_BY,                                                                                                        
 GETDATE(),                                                                                                        
 MODIFIED_BY,                                                                                                        
 LAST_UPDATED_DATETIME,                                                                                                        
 COUNTRY_ID,                                                                                                        
 STATE_ID,                                                                                   
 DIV_ID,                                                                            
 DEPT_ID,                                                 
 PC_ID,                                                                                                        
 BILL_TYPE,                                                                                                        
 COMPLETE_APP,                                                                                                        
 PROPRTY_INSP_CREDIT,                                                         
 INSTALL_PLAN_ID,                                                                                                        
CHARGE_OFF_PRMIUM,                                                                 
 RECEIVED_PRMIUM,                         
 PROXY_SIGN_OBTAINED,                     
 POLICY_TYPE,                                         
 SHOW_QUOTE,                                                                                             
 APP_VERIFICATION_XML,              YEAR_AT_CURR_RESI,                                                                                                        
 YEARS_AT_PREV_ADD,                                                                    
 'Suspended',                                                                                                        
 SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                          
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0' ,                                                                                  
 APP_EFFECTIVE_DATE  ,                                                
 IS_HOME_EMP                                                                                                     
 FROM APP_LIST               
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                        
 -- ADDED ON 20-12-2005                                                                              
 AND IS_ACTIVE = 'Y'                                                                                   
                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                   
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                         
                                                                   
                                                                
--ADDED BY VJ ON 01-02-2006                                                                
IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                       
AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID)                                                                
                                                                
BEGIN                                 
                                                                
                                                                
                                                                
                                                          
-- 2. POL_APPLICANT_LIST                                                                                                  
                            
INSERT INTO POL_APPLICANT_LIST                                                                                                        
(                                                                                  
 POLICY_ID,                                                                                                        
 POLICY_VERSION_ID,                                                                                                         
 CUSTOMER_ID,                                                                                                        
 APPLICANT_ID,                                             
 CREATED_BY,                                                                                                        
 CREATED_DATETIME,             
 IS_PRIMARY_APPLICANT                                                                                                        
)                                                                                    
SELECT                                                             
 @TEMP_POLICY_ID,                                                                                                        
 @TEMP_POLICY_VERSION_ID,                                                                                                         
 @CUSTOMER_ID,            
 A.APPLICANT_ID,                                                                
 @CREATED_BY,                                                                                                        
 GETDATE(),        
 A.IS_PRIMARY_APPLICANT                           
 FROM APP_APPLICANT_LIST A                                                                                    
--Pawan                                                      
,clt_APPLICANT_LIST  B,                                                                              
-- VJ                                                                                               
APP_LIST C                                                                              
 WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                       
--Pawan                                                                                    
   and A.APPLICANT_ID = B.APPLICANT_ID and B.Is_Active='Y'                                                                                              
-- VJ                                                                              
 AND C.CUSTOMER_ID = @CUSTOMER_ID AND C.APP_ID = @APP_ID AND C.APP_VERSION_ID = @APP_VERSION_ID                                                                              
  AND C.IS_ACTIVE = 'Y'                                                                                 
                                                                              
*/                                                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                           
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                     
                      
                                         
-- 3. POL_VEHICLES                                                                                                  
                                                                                                 
                      
INSERT INTO POL_VEHICLES                                                                                                         
(                                                                                                        
 POLICY_ID,                             
 POLICY_VERSION_ID,                                                                   
 CUSTOMER_ID,                                                                                                     
 VEHICLE_ID,                                                                             
 INSURED_VEH_NUMBER,                                                                                                        
 VEHICLE_YEAR,                                                                                                        
 MAKE,                                                                                                        
 MODEL,                                            
 VIN,                                                           
 BODY_TYPE,                                                                                                        
 GRG_ADD1,                                       
 GRG_ADD2,                                                                                                        
 GRG_CITY,                                                                             
 GRG_COUNTRY,                                                                               
 GRG_STATE,                                                                 
 GRG_ZIP,                                     
 REGISTERED_STATE,                                                                     
 TERRITORY,                        
 CLASS,                                        
 REGN_PLATE_NUMBER,                                                                                                        
 ST_AMT_TYPE,                                 
 AMOUNT,                              
 SYMBOL,                                                                           
 VEHICLE_AGE,                                                              
 IS_OWN_LEASE,                                                                                                        
 PURCHASE_DATE,                                                                                                        
 IS_NEW_USED,                                                   
 MILES_TO_WORK,                                                                                                        
 VEHICLE_USE,                                                                                 
 VEH_PERFORMANCE,                         
 MULTI_CAR,                                                                                                        
 ANNUAL_MILEAGE,                                                                                           
 PASSIVE_SEAT_BELT,                                            
 AIR_BAG,                                                                                                        
 ANTI_LOCK_BRAKES,                                        
 DEACTIVATE_REACTIVATE_DATE,                                                                                                        
 IS_ACTIVE,                                                                                                        
 CREATED_BY,                                                                                                        
 CREATED_DATETIME,                                                                                                    
                      
 VEHICLE_CC,                                                                                                    
 MOTORCYCLE_TYPE,                                                                                                    
 UNINS_MOTOR_INJURY_COVE,                                                                                                    
 UNINS_PROPERTY_DAMAGE_COVE,                                                                                      
 UNDERINS_MOTOR_INJURY_COVE,                                                                                                    
 VEHICLE_TYPE,                                                             
 NATURE_OF_INTEREST,                                                            
 APP_USE_VEHICLE_ID,                                                                                                    
 APP_VEHICLE_PERCLASS_ID,                                                                                                    
 APP_VEHICLE_COMCLASS_ID,                                                                                                    
 APP_VEHICLE_PERTYPE_ID,                                                                    
 APP_VEHICLE_COMTYPE_ID,                                                                                                    
 APP_VEHICLE_CLASS,                                                
 BUSS_PERM_RESI,                                    
SNOWPLOW_CONDS,                                                
CAR_POOL,                                                
--SAFETY_BELT,                                                
AUTO_POL_NO,                                  
RADIUS_OF_USE,                                  
TRANSPORT_CHEMICAL,                                  
COVERED_BY_WC_INSU,                                  
CLASS_DESCRIPTION,                            
CYCL_REGD_ROAD_USE,                          
COMPRH_ONLY ,      
CLASS_DRIVERID ,       IS_SUSPENDED                                                                             
)                 
                              
SELECT                                                                                  
                                           
 @TEMP_POLICY_ID,                                                                                                      
 @TEMP_POLICY_VERSION_ID,                                                                              
 @CUSTOMER_ID,                                                                
VEHICLE_ID,                                                                                                   
 INSURED_VEH_NUMBER,                                                                                                     
 VEHICLE_YEAR,                                              
 MAKE,                                                                                                        
 MODEL,                                                                                                        
 VIN,                                                                                                        
 BODY_TYPE,                                          
 GRG_ADD1,                                                                                
 GRG_ADD2,                                                    
 GRG_CITY,                                                                               
 GRG_COUNTRY,                                                                    
 GRG_STATE,GRG_ZIP,                                                                                                        
 REGISTERED_STATE,                                                   
 TERRITORY,                                                                                                        
 CLASS,                                                           
 REGN_PLATE_NUMBER,                                                                                                        
 ST_AMT_TYPE,                                                                                  
 AMOUNT,                                                                                                        
 SYMBOL,                                                                                                        
 VEHICLE_AGE,                                                                  
 IS_OWN_LEASE,                                                            
 PURCHASE_DATE,                                                                                                        
 IS_NEW_USED,                                                                                                        
 MILES_TO_WORK,                                                                                                        
 VEHICLE_USE,                                                                                                        
 VEH_PERFORMANCE,                                                                             
 MULTI_CAR,                                                                                                        
 ANNUAL_MILEAGE,                                                                                             
 PASSIVE_SEAT_BELT,                                                                                                   
 AIR_BAG,                                                                                                         
 ANTI_LOCK_BRAKES,                                              
 DEACTIVATE_REACTIVATE_DATE,                                                                                                        
 IS_ACTIVE,                                                                                                        
 @CREATED_BY,                                                                                                        
 GETDATE(),                                                                                                    
 VEHICLE_CC,                                               
 MOTORCYCLE_TYPE,                                                                         
 UNINS_MOTOR_INJURY_COVE,                                                                                                    
 UNINS_PROPERTY_DAMAGE_COVE,                                                                                                    
 UNDERINS_MOTOR_INJURY_COVE,                                            
 VEHICLE_TYPE,                                                                                                    
 NATURE_OF_INTEREST,                                                    
 USE_VEHICLE,                                                           
 CLASS_PER,                                                                                                    
 CLASS_COM,                                                                                                    
 VEHICLE_TYPE_PER,                                                                               
 VEHICLE_TYPE_COM,                                                                                                    
 APP_VEHICLE_CLASS,                                                
 BUSS_PERM_RESI,                                                
 SNOWPLOW_CONDS,                                        
CAR_POOL,                                                
--SAFETY_BELT,                             
AUTO_POL_NO,                                  
RADIUS_OF_USE,                                  
TRANSPORT_CHEMICAL,                                  
COVERED_BY_WC_INSU,                                  
CLASS_DESCRIPTION,                            
CYCL_REGD_ROAD_USE,                          
COMPRH_ONLY ,      
CLASS_DRIVERID ,      
IS_SUSPENDED                                                
 FROM APP_VEHICLES                                                                                                         
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                           
--Pawan                                                                                    
and Is_Active = 'Y'                                                                                                        
                                           
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                        
                                        
-- POL_MISCELLANEOUS_EQUIPMENT_VALUES                                                                                                  
                                                                     
INSERT INTO POL_MISCELLANEOUS_EQUIPMENT_VALUES                                                                                                         
(                                                                                                        
CUSTOMER_ID,                                        
POLICY_ID,                                        
POLICY_VERSION_ID,                                        
VEHICLE_ID,                                        
ITEM_ID,                                        
ITEM_DESCRIPTION,                                        
ITEM_VALUE,                                        
IS_ACTIVE,                                        
CREATED_BY,                                        
CREATED_DATETIME                                        
)                  
    
-----New Query 6 APril 2009    
SELECT                                                                                            
@CUSTOMER_ID,                                                                                                           
@TEMP_POLICY_ID,                  
@TEMP_POLICY_VERSION_ID,                                                                                      
APP.VEHICLE_ID,                                          
ITEM_ID,                      
ITEM_DESCRIPTION,                                          
ITEM_VALUE,                                          
APP.IS_ACTIVE,                                          
@CREATED_BY,                                          
GETDATE()                                                                                                     
FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES MISC     
INNER JOIN  APP_VEHICLES APP    
ON  APP.CUSTOMER_ID =   MISC.CUSTOMER_ID    
AND APP.APP_ID = MISC.APP_ID     
AND APP.APP_VERSION_ID = MISC.APP_VERSION_ID    
AND APP.VEHICLE_ID = MISC.VEHICLE_ID    
WHERE    
MISC.CUSTOMER_ID=@CUSTOMER_ID AND MISC.APP_ID=@APP_ID AND MISC.APP_VERSION_ID=@APP_VERSION_ID                                                                                      
--PAWAN                                  
AND ISNULL(MISC.IS_ACTIVE,'N') = 'Y' AND ISNULL(APP.IS_ACTIVE,'') = 'Y'       
    
---Commented on 6 April 2009    
--Foregn Key Error -                                                                                         
                                                     
--SELECT                                                                                          
--@CUSTOMER_ID,                                                                                                         
--@TEMP_POLICY_ID,            
--@TEMP_POLICY_VERSION_ID,                                                                                    
--VEHICLE_ID,                                        
--ITEM_ID,                    
--ITEM_DESCRIPTION,                                        
--ITEM_VALUE,                                        
--IS_ACTIVE,                                        
--@CREATED_BY,                                        
--GetDate()                                                                                                   
-- FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES                                                                                                         
-- WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                    
----Pawan                                
--AND IS_ACTIVE = 'Y'                                                                                                        
                                                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                       
                                                                  
                                                                                     
-- 4. POL_VEHICLE_COVERAGES          
INSERT INTO POL_VEHICLE_COVERAGES                                             
(                                                                                                        
 POLICY_ID,                                                                                                        
 POLICY_VERSION_ID,                                                                                                        
 CUSTOMER_ID,                                                                                                        
 VEHICLE_ID,                                                                                                        
 COVERAGE_ID,                                                                                                 
 COVERAGE_CODE_ID,                                           
 LIMIT_OVERRIDE,                                  
 LIMIT_1,                                                                                          
 LIMIT_1_TYPE,                                                                       
 LIMIT_2,                                                                                                        
 LIMIT_2_TYPE,                                                       
 DEDUCT_OVERRIDE,                                                                                                        
DEDUCTIBLE_1,                                                                                                        
 DEDUCTIBLE_1_TYPE,                                                                                  
 DEDUCTIBLE_2,                                                        
 DEDUCTIBLE_2_TYPE,                                         
 WRITTEN_PREMIUM,                                                                                                        
 FULL_TERM_PREMIUM,                                                                     
 IS_SYSTEM_COVERAGE,                                                                  
 LIMIT1_AMOUNT_TEXT,                                                                                                    
 LIMIT2_AMOUNT_TEXT,                                                                                                    
DEDUCTIBLE1_AMOUNT_TEXT,                                                                                                    
 DEDUCTIBLE2_AMOUNT_TEXT,                                                                    
 SIGNATURE_OBTAINED,                                       
 LIMIT_ID,                                                  
 DEDUC_ID,                  
 ADD_INFORMATION                                                     
)                              
                                                                                     
SELECT                                                                                                         
 @TEMP_POLICY_ID,                                                                               
 @TEMP_POLICY_VERSION_ID,                                    
 @CUSTOMER_ID,                                                                                             
 VEHICLE_ID,                                                         
 COVERAGE_ID,                                                                                                        
 COVERAGE_CODE_ID,                                                                
 LIMIT_OVERRIDE,                                                                                                        
 LIMIT_1,                                                                                                 
 LIMIT_1_TYPE,                                                                                                        
 LIMIT_2,                                                                            
 LIMIT_2_TYPE,                            
 DEDUCT_OVERRIDE,                                                                                                        
 DEDUCTIBLE_1,                                                                                                        
 DEDUCTIBLE_1_TYPE,                                                                                                        
 DEDUCTIBLE_2,                                                                                                       
 DEDUCTIBLE_2_TYPE,                                                                                                        
 WRITTEN_PREMIUM,                                        
 FULL_TERM_PREMIUM,                                                                                    
 IS_SYSTEM_COVERAGE,                                                                                                    
 LIMIT1_AMOUNT_TEXT,                                                                    
 LIMIT2_AMOUNT_TEXT,                                                                                                    
 DEDUCTIBLE1_AMOUNT_TEXT,                                                                                                    
 DEDUCTIBLE2_AMOUNT_TEXT,                                                                    
 SIGNATURE_OBTAINED,                                                  
 LIMIT_ID,                                                  
 DEDUC_ID,                  
 ADD_INFORMATION                                                                                                           
 FROM APP_VEHICLE_COVERAGES                                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                      
  --Pawan                                                                                    
 and  VEHICLE_ID in (Select VEHICLE_ID from POL_VEHICLES                                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                                     
                                                                                                        
                                                         
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                   
                                       
                                                                                                        
-- 5. POL_VEHICLE_ENDORSEMENTS                                                                                                        
                                                    
INSERT INTO POL_VEHICLE_ENDORSEMENTS                                                                
(                                              
 POLICY_ID,                                                                                                        
 POLICY_VERSION_ID,                                                                          
 CUSTOMER_ID,                                                                                                        
VEHICLE_ID,                                                  
 ENDORSEMENT_ID,                                                             
 REMARKS,                                                                                                        
 VEHICLE_ENDORSEMENT_ID ,          
EDITION_DATE          
                                                
)                                                                                                        
                                                                                                        
SELECT                                                                                                         
 @TEMP_POLICY_ID,                                                                                       
 @TEMP_POLICY_VERSION_ID,                                                                                                         
 @CUSTOMER_ID,                                                                                                       
 VEHICLE_ID,                                                                                                        
 ENDORSEMENT_ID,                                                                                   
 REMARKS,                                      
 VEHICLE_ENDORSEMENT_ID ,          
EDITION_DATE          
 FROM APP_VEHICLE_ENDORSEMENTS                                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                    
--Pawan                                       
and  VEHICLE_ID in (Select VEHICLE_ID from POL_VEHICLES                                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                                                         
                                                                                           
                                                         
                                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                        
                                         
                                                                              
-- 6. POL_ADD_OTHER_INT                                                            
INSERT INTO POL_ADD_OTHER_INT                                                                                                         
(                                   
 POLICY_ID,                                         
 POLICY_VERSION_ID,                               
 CUSTOMER_ID,                                                                        
 HOLDER_ID,                                                                  
 VEHICLE_ID,                                                      
 MEMO,                                            
 NATURE_OF_INTEREST,                                                                                                        
 RANK,                                       
 LOAN_REF_NUMBER,                                                                                                      
 IS_ACTIVE,                                           
 CREATED_BY,                                                                                                        
 CREATED_DATETIME,                                                            
 ADD_INT_ID,                                                                                                    
 HOLDER_NAME,       
 HOLDER_ADD1,                                                                                       
 HOLDER_ADD2,                                                                                                    
 HOLDER_CITY,                                
 HOLDER_COUNTRY,                                                                                              
 HOLDER_STATE,                                                                                                    
 HOLDER_ZIP                                                                                   
)                                                                                    
SELECT                                                   
 @TEMP_POLICY_ID,                                                                                                        
 @TEMP_POLICY_VERSION_ID,                                                                         
 @CUSTOMER_ID,                 
 HOLDER_ID,                                                                                                        
 VEHICLE_ID,                                                    
 MEMO,                                                                                                        
 NATURE_OF_INTEREST,                                                                                                        
 RANK,                                                                                                        
 LOAN_REF_NUMBER,                                                                
 IS_ACTIVE,                                                                                         
 @CREATED_BY,                                    
 GETDATE() ,                                                                                                    
 ADD_INT_ID,                                                                                                    
 HOLDER_NAME,                                                   
 HOLDER_ADD1,                      
 HOLDER_ADD2,                                                                                                    
 HOLDER_CITY,                                                 
 HOLDER_COUNTRY,                                                                                                    
 HOLDER_STATE,                                                                                                    
 HOLDER_ZIP                                                                                          
 FROM APP_ADD_OTHER_INT                                                                               
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                     
 --Pawan                                                                                                        
 and IS_ACTIVE = 'Y'                                         
 and  VEHICLE_ID in (Select VEHICLE_ID from POL_VEHICLES                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                     
                                                                         
                                                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                             
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                        
                              
                                                                                                        
-- 7. POL_DRIVER_DETAILS                                                                    
                                                     
INSERT INTO POL_DRIVER_DETAILS                                               
(                                                                                        
 POLICY_ID,                                                                                              
 POLICY_VERSION_ID,                                                                                                        
 CUSTOMER_ID,                                                                                                        
 DRIVER_ID,                                                                                                       
 DRIVER_FNAME,                                                                                                         
 DRIVER_MNAME,                                                                                                        
 DRIVER_LNAME,                                                                                               
 DRIVER_CODE,                                                                                                        
 DRIVER_SUFFIX,                                                                                                        
 DRIVER_ADD1,                         
 DRIVER_ADD2,                                                                                
 DRIVER_CITY,                                                                                                        
 DRIVER_STATE,                                                                                                        
 DRIVER_ZIP,                                                                                                        
 DRIVER_COUNTRY,                                                                                                    
 DRIVER_HOME_PHONE,                                                                                    
 DRIVER_BUSINESS_PHONE,                                                                                                        
 DRIVER_EXT,                                         
 DRIVER_MOBILE,                                                                                                        
 DRIVER_DOB,                                                                                                        
 DRIVER_SSN,                                     
 DRIVER_MART_STAT,                                                                                                        
 DRIVER_SEX,                                                    
 DRIVER_DRIV_LIC,                                                                                                        
 DRIVER_LIC_STATE,                                                                       
 DRIVER_LIC_CLASS,                                                                                                        
 DATE_EXP_START,                                                                                         
 DATE_LICENSED,                                                 
 DRIVER_REL,                                                                                                  
 DRIVER_DRIV_TYPE,                                                                                         
 DRIVER_OCC_CODE,                                                 
 DRIVER_OCC_CLASS,                                                                                                        
 DRIVER_DRIVERLOYER_NAME,                                                                                                        
 DRIVER_DRIVERLOYER_ADD,                                
 DRIVER_INCOME,                                                                                                        
 DRIVER_BROADEND_NOFAULT,                                                                                                 
 DRIVER_PHYS_MED_IMPAIRE,                                               
 DRIVER_DRINK_VIOLATION,                                                                                                        
 DRIVER_PREF_RISK,                                                   
 DRIVER_GOOD_STUDENT,                                                                                                        
 DRIVER_STUD_DIST_OVER_HUNDRED,                                                                            
 DRIVER_LIC_SUSPENDED,                                                    
 DRIVER_VOLUNTEER_POLICE_FIRE,                                       
 DRIVER_US_CITIZEN,                                                                                  
 IS_ACTIVE,                                                                                                        
 CREATED_BY,                                                                                                        
 CREATED_DATETIME,                                                                                       
 DRIVER_FAX,                                                                                                        
 RELATIONSHIP,                                                                                                        
 DRIVER_TITLE,                                                          
 Good_Driver_Student_Discount,                 Premier_Driver_Discount,                                                                                                        
 Safe_Driver_Renewal_Discount,                                                                      
 Vehicle_ID,                                                                                                        
 Percent_Driven,                                                                                                         
 Mature_Driver,                                                                                                        
 Mature_Driver_Discount,                                                                 
 Preferred_Risk_Discount,                                                                                                        
 Preferred_Risk,                                                                                                         
 TransferExp_Renewal_Discount,                                                                                            
 TransferExperience_RenewalCredit ,                                                                                                    
 SAFE_DRIVER,                                                                                  
 APP_VEHICLE_PRIN_OCC_ID,                                                                        
 --Added By Shafi                                                                        
NO_DEPENDENTS,                                                                    
Waiver_Work_loss_benefits,                        
--added by pravesh                        
NO_CYCLE_ENDMT,                                                
FORM_F95,              
EXT_NON_OWN_COVG_INDIVI,                                                
HAVE_CAR,                                                
STATIONED_IN_US_TERR,                                                
IN_MILITARY,                                              
FULL_TIME_STUDENT,                                              
SUPPORT_DOCUMENT,                                              
SIGNED_WAIVER_BENEFITS_FORM,                                          
PARENTS_INSURANCE,                        
CYCL_WITH_YOU,                        
COLL_STUD_AWAY_HOME,               
DATE_ORDERED,            
MVR_ORDERED,            
VIOLATIONS,      
MVR_CLASS,      
MVR_LIC_CLASS,      
MVR_LIC_RESTR,      
MVR_DRIV_LIC_APPL,    
--Added by Sibin for Itrack Issue 5073 on 19 Nov 08    
MVR_REMARKS,    
MVR_STATUS,    
LOSSREPORT_ORDER,    
LOSSREPORT_DATETIME    
    
 --Added till here            
            
--NO_OF_DEPENDENTS               
)                                                                                                        
SELECT                                                                               
 @TEMP_POLICY_ID,                                                                                                     
 @TEMP_POLICY_VERSION_ID,                                                                                          
@CUSTOMER_ID,                                                                                                        
 DRIVER_ID,                                                                  
 DRIVER_FNAME,                                                                                                        
 DRIVER_MNAME,                                                                                                        
 DRIVER_LNAME,                                                                                            
 DRIVER_CODE,                                                                                                        
 DRIVER_SUFFIX,                                                  
 DRIVER_ADD1,                                             
 DRIVER_ADD2,                                                                                                       
 DRIVER_CITY,                                                                                           DRIVER_STATE,                                                                                 
 DRIVER_ZIP,                                                                                                        
 DRIVER_COUNTRY,                                         
 DRIVER_HOME_PHONE,                                             
 DRIVER_BUSINESS_PHONE,                                       
 DRIVER_EXT,                                             
 DRIVER_MOBILE,                                                                                                        
 DRIVER_DOB,                                                                                                        
 DRIVER_SSN,                                                                     
 DRIVER_MART_STAT,                                                           
 DRIVER_SEX,                                                                 
 DRIVER_DRIV_LIC,                                                                       
 DRIVER_LIC_STATE,                                                                                                        
 DRIVER_LIC_CLASS,                                                                      
 DATE_EXP_START,                                                                                             
 DATE_LICENSED,                                                                                      
 DRIVER_REL,                                       
 DRIVER_DRIV_TYPE,                                     
 DRIVER_OCC_CODE,                                  
 DRIVER_OCC_CLASS,                  
 DRIVER_DRIVERLOYER_NAME,                                
 DRIVER_DRIVERLOYER_ADD,                                                                                                        
 DRIVER_INCOME,                                                                                                        
 DRIVER_BROADEND_NOFAULT,                                           
 DRIVER_PHYS_MED_IMPAIRE,                                                                                                        
 DRIVER_DRINK_VIOLATION,                                             
 DRIVER_PREF_RISK,                                                              
 DRIVER_GOOD_STUDENT,                                                                                                        
 DRIVER_STUD_DIST_OVER_HUNDRED,                                                 
 DRIVER_LIC_SUSPENDED,                                                                                                        
 DRIVER_VOLUNTEER_POLICE_FIRE,                                                                      
 DRIVER_US_CITIZEN,                                                             
 IS_ACTIVE,                                                                                                        
  @CREATED_BY,                                                                       
 GETDATE(),                                                                                        
 DRIVER_FAX,                                                  
 RELATIONSHIP,                                                                                                        
 DRIVER_TITLE,                                                                                                        
 Good_Driver_Student_Discount,                                                                                                        
 Premier_Driver_Discount,                                                                                                        
 CONVERT(INTEGER,Safe_Driver_Renewal_Discount),                                                       
 Vehicle_ID,                        
 Percent_Driven,                                                                  
 Mature_Driver,                                                                                                        
 Mature_Driver_Discount,                                                                                                        
 Preferred_Risk_Discount,                                                                  
 Preferred_Risk,                                                                                 
 TransferExp_Renewal_Discount,                                   
 TransferExperience_RenewalCredit,                                                                                   
 SAFE_DRIVER,                                                     
 APP_VEHICLE_PRIN_OCC_ID,                                                                        
 NO_DEPENDENTS,                                                                    
 Waiver_Work_loss_benefits,                        
 NO_CYCLE_ENDMT,                                                
FORM_F95,                                                
EXT_NON_OWN_COVG_INDIVI,                                                
HAVE_CAR,                                                
STATIONED_IN_US_TERR,                                                
IN_MILITARY,                                              
FULL_TIME_STUDENT,                                              
SUPPORT_DOCUMENT,                                              
SIGNED_WAIVER_BENEFITS_FORM,                                      
PARENTS_INSURANCE,                        
CYCL_WITH_YOU,                        
COLL_STUD_AWAY_HOME,             
DATE_ORDERED,            
MVR_ORDERED,            
VIOLATIONS,      
MVR_CLASS,      
MVR_LIC_CLASS,      
MVR_LIC_RESTR,      
MVR_DRIV_LIC_APPL,    
    
--Added by Sibin for Itrack Issue 5073 on 19 Nov 08    
MVR_REMARKS,    
MVR_STATUS,    
LOSSREPORT_ORDER,    
LOSSREPORT_DATETIME    
    
 --Added till here                
               
--NO_OF_DEPENDENTS                                          
 FROM APP_DRIVER_DETAILS                                                                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                                        
 --Pawan                                                              
 and   IS_ACTIVE = 'Y'               
                                       
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                    
                                                     
                                            
-- 8. POL_MVR_INFORMATION                                                 
INSERT INTO POL_MVR_INFORMATION                                 
(                                                                                                        
 POLICY_ID,                                                                                                        
 POLICY_VERSION_ID,                                                                                                        
 CUSTOMER_ID,                                                                           
 POL_MVR_ID,                          
 DRIVER_ID,                                                                                                        
 MVR_AMOUNT,                                                                                                  
 MVR_DEATH,                                                                                 
 MVR_DATE,                                                  
 VIOLATION_ID,                                                                                                        
 IS_ACTIVE,                                                                    
 VIOLATION_SOURCE,                                                                    
 VERIFIED,                                                     
 VIOLATION_TYPE,      
 OCCURENCE_DATE,             
 DETAILS,           
 POINTS_ASSIGNED,           
 ADJUST_VIOLATION_POINTS                                                                                                      
)                                                                                                        
SELECT                                
 @TEMP_POLICY_ID,                                                                            
 @TEMP_POLICY_VERSION_ID,                                                                                             
 @CUSTOMER_ID,                                                                                                        
 APP_MVR_ID,                                                                                                        
 DRIVER_ID,                                                                                                        
 MVR_AMOUNT,                                            
 MVR_DEATH,                        
 MVR_DATE,                                                                                                      
 VIOLATION_ID,                                                                                  
 IS_ACTIVE,                                                                    
 VIOLATION_SOURCE,                                                                    
 VERIFIED,                                                    
 VIOLATION_TYPE,      
 OCCURENCE_DATE,             
 DETAILS,           
 POINTS_ASSIGNED,           
 ADJUST_VIOLATION_POINTS                                                                                  
 FROM APP_MVR_INFORMATION                                                                                                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                   
 --Pawan                                                    
 and DRIVER_ID in (Select DRIVER_ID from POL_DRIVER_DETAILS                                                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                      
                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                        
                                  
                                
                                
-- 8A POL_DRIVER_ASSIGNED_VEHICLE                                
INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                             
(                                                                                                        
CUSTOMER_ID,                                
POLICY_ID,                                
POLICY_VERSION_ID,                                
DRIVER_ID,                                
VEHICLE_ID,                                
APP_VEHICLE_PRIN_OCC_ID                                
                                                                                                   
)                                                                                                        
SELECT                                                                                                         
 @CUSTOMER_ID,                      
 @TEMP_POLICY_ID,                                       
 @TEMP_POLICY_VERSION_ID,                             
 DRIVER_ID,                                
 VEHICLE_ID,                                
 APP_VEHICLE_PRIN_OCC_ID                                
                                                       
 FROM APP_DRIVER_ASSIGNED_VEHICLE                                                                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                                                                    
 and DRIVER_ID in (Select DRIVER_ID from POL_DRIVER_DETAILS                                                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                                        
                                                      
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                        
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                                        
                                                 
-- 9. POL_AUTO_GEN_INFO                                                                                                            
INSERT INTO POL_AUTO_GEN_INFO                                                                      
(                                                                                                      
 POLICY_ID,                                                                                                        
 POLICY_VERSION_ID,                                            
 CUSTOMER_ID,                                                   
 ANY_NON_OWNED_VEH,                                                                                                        
 CAR_MODIFIED,                                                                                                    
 EXISTING_DMG,                                    
 ANY_CAR_AT_SCH,                                                                                               
 SCHOOL_CARS_LIST,                        
 ANY_OTH_AUTO_INSU,                                                                         
 ANY_OTH_INSU_COMP,                                          
 OTHER_POLICY_NUMBER_LIST,                                                                                                   
 H_MEM_IN_MILITARY,                                                                                                        
 H_MEM_IN_MILITARY_LIST,                                                                              
 DRIVER_SUS_REVOKED,                                                                          
 PHY_MENTL_CHALLENGED,                                                                                                        
 ANY_FINANCIAL_RESPONSIBILITY,                                                                             
 INS_AGENCY_TRANSFER,                                                                                    
 COVERAGE_DECLINED,                                                                                   
 AGENCY_VEH_INSPECTED,                                                                        
 USE_AS_TRANSPORT_FEE,                                                                                                        
 SALVAGE_TITLE,                                                                                         
 ANY_ANTIQUE_AUTO,                                                                 
 ANTIQUE_AUTO_LIST,                                                                                          
 REMARKS,                                                                
 IS_ACTIVE,                                                                                                  
 CREATED_BY,                                                                                                
 CREATED_DATETIME,                                                                                                    
 IS_COMMERCIAL_USE,                                                                                      
 IS_USEDFOR_RACING,                                                                       
 IS_COST_OVER_DEFINED_LIMIT,                                  
 IS_MORE_WHEELS,                                                                                                    
 IS_EXTENDED_FORKS,                                                                                                    
 IS_LICENSED_FOR_ROAD,                                                                                                    
 IS_MODIFIED_INCREASE_SPEED,                                                                                                  
 IS_MODIFIED_KIT,                                               
 IS_TAKEN_OUT,                                                                                         
 IS_CONVICTED_CARELESS_DRIVE,                                                                           
 IS_CONVICTED_ACCIDENT,                                                                                                    
 MULTI_POLICY_DISC_APPLIED,                                                                                                    
 ANY_NON_OWNED_VEH_PP_DESC,                                                          
 CAR_MODIFIED_DESC,                                                                                                    
 EXISTING_DMG_PP_DESC,                                                                               
 ANY_CAR_AT_SCH_DESC,                                                                                                    
 ANY_OTH_AUTO_INSU_DESC,                                                          
 ANY_OTH_INSU_COMP_PP_DESC,                                                                         
 H_MEM_IN_MILITARY_DESC,                                                      
 DRIVER_SUS_REVOKED_PP_DESC,                                                                                                    
 PHY_MENTL_CHALLENGED_PP_DESC,                                           
 ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,                                                                                                    
 INS_AGENCY_TRANSFER_PP_DESC,                                                                                                    
 COVERAGE_DECLINED_PP_DESC,                                                                          
 AGENCY_VEH_INSPECTED_PP_DESC,                          
 USE_AS_TRANSPORT_FEE_DESC,                                                                                                    
 SALVAGE_TITLE_PP_DESC,                                                       
 ANY_ANTIQUE_AUTO_DESC,                                                                         
 MULTI_POLICY_DISC_APPLIED_PP_DESC,                                                                                                    
 ANY_NON_OWNED_VEH_MC_DESC,                                   
 EXISTING_DMG_MC_DESC,                                                                                                    
 ANY_OTH_INSU_COMP_MC_DESC,                                                                                                    
 DRIVER_SUS_REVOKED_MC_DESC,                                                        
 PHY_MENTL_CHALLENGED_MC_DESC,                   
 ANY_FINANCIAL_RESPONSIBILITY_MC_DESC,                                                                                         
 INS_AGENCY_TRANSFER_MC_DESC,                                                                                                    
 COVERAGE_DECLINED_MC_DESC,           
 AGENCY_VEH_INSPECTED_MC_DESC,                                                                                                    
 SALVAGE_TITLE_MC_DESC,                                                             
 IS_COMMERCIAL_USE_DESC,                                                                                   
 IS_USEDFOR_RACING_DESC,                                                      
 IS_COST_OVER_DEFINED_LIMIT_DESC,                                              
 IS_MORE_WHEELS_DESC,                                                                                                    
 IS_EXTENDED_FORKS_DESC,                                                                                     
 IS_LICENSED_FOR_ROAD_DESC,                                                     
 IS_MODIFIED_INCREASE_SPEED_DESC,                                                                                                    
 IS_MODIFIED_KIT_DESC,                                                                               
 IS_TAKEN_OUT_DESC,                                                                                        
 IS_CONVICTED_CARELESS_DRIVE_DESC,                                  
 IS_CONVICTED_ACCIDENT_DESC,                                                                               
 MULTI_POLICY_DISC_APPLIED_MC_DESC,                                                                                                 
 FullName,                                                                
 DATE_OF_BIRTH,                 
 DrivingLisence,                                                                                                    
 InsuredElseWhere,                                                                                                    
 CompanyName,                                                                                                    
 PolicyNumber,                                                                                                    
 IS_OTHER_THAN_INSURED,                                                         
 CURR_RES_TYPE,                                                                        
 WhichCycle,                                                                                                    
 COST_EQUIPMENT_DESC,                                                                    
 YEARS_INSU_WOL,                
 YEARS_INSU,                                    
 ANY_PRIOR_LOSSES,                                    
 ANY_PRIOR_LOSSES_DESC,                                    
 APPLY_PERS_UMB_POL,                              
 APPLY_PERS_UMB_POL_DESC                
-- SEAT_BELT_CREDIT                                                                                
)                                                                                   
SELECT                                     
 @TEMP_POLICY_ID,                                            
 @TEMP_POLICY_VERSION_ID,                                                                                                         
 @CUSTOMER_ID,            
 ANY_NON_OWNED_VEH,                                                                                                        
 CAR_MODIFIED,                                                                                                        
 EXISTING_DMG,                                                                                                        
 ANY_CAR_AT_SCH,                                                                                                   
 SCHOOL_CARS_LIST,                          
 ANY_OTH_AUTO_INSU,                          
 ANY_OTH_INSU_COMP,                                               
 OTHER_POLICY_NUMBER_LIST,                                              
 H_MEM_IN_MILITARY,                                                                                                        
 H_MEM_IN_MILITARY_LIST,                                                           
 DRIVER_SUS_REVOKED,                                                                                                        
 PHY_MENTL_CHALLENGED,                                                                                                        
 ANY_FINANCIAL_RESPONSIBILITY,                                                                                
 INS_AGENCY_TRANSFER,                                                                                                        
 COVERAGE_DECLINED,                                                                                                        
 AGENCY_VEH_INSPECTED,                 
 USE_AS_TRANSPORT_FEE,                                                                                                        
 SALVAGE_TITLE,                                                                                                        
 ANY_ANTIQUE_AUTO,                                                         
 ANTIQUE_AUTO_LIST,                                                                                                        
 REMARKS,                
 IS_ACTIVE,                                                 
 @CREATED_BY,                                                                                                        
 GETDATE(),                                                                                  
IS_COMMERCIAL_USE,                                   
 IS_USEDFOR_RACING,                     
 IS_COST_OVER_DEFINED_LIMIT,                                                                                 
 IS_MORE_WHEELS,                                             
 IS_EXTENDED_FORKS,                                                                               
 IS_LICENSED_FOR_ROAD,                                                                                 
 IS_MODIFIED_INCREASE_SPEED,                                          
 IS_MODIFIED_KIT,                                                                                                    
 IS_TAKEN_OUT,                                                            
 IS_CONVICTED_CARELESS_DRIVE,                                                          
 IS_CONVICTED_ACCIDENT,           
 MULTI_POLICY_DISC_APPLIED,                                                       
 ANY_NON_OWNED_VEH_PP_DESC,                                                                                                    
 CAR_MODIFIED_DESC,                                                                      
 EXISTING_DMG_PP_DESC,                                                                                                    
 ANY_CAR_AT_SCH_DESC,                                                                                                    
 ANY_OTH_AUTO_INSU_DESC,                                                                                                    
 ANY_OTH_INSU_COMP_PP_DESC,                
 H_MEM_IN_MILITARY_DESC,                                                                                                    
 DRIVER_SUS_REVOKED_PP_DESC,                                                                                  
 PHY_MENTL_CHALLENGED_PP_DESC,                                        
 ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,                                                                                                    
 INS_AGENCY_TRANSFER_PP_DESC,                                                                                                    
 COVERAGE_DECLINED_PP_DESC,                                             
 AGENCY_VEH_INSPECTED_PP_DESC,                
 USE_AS_TRANSPORT_FEE_DESC,                                                                                                    
 SALVAGE_TITLE_PP_DESC,                                                                                                    
 ANY_ANTIQUE_AUTO_DESC,                                                              
 MULTI_POLICY_DISC_APPLIED_PP_DESC,                                                                                                    
 ANY_NON_OWNED_VEH_MC_DESC,                                                                                
 EXISTING_DMG_MC_DESC,                                                                                                    
 ANY_OTH_INSU_COMP_MC_DESC,                                                                                                    
 DRIVER_SUS_REVOKED_MC_DESC,                                                           
 PHY_MENTL_CHALLENGED_MC_DESC,                                                                                                    
 ANY_FINANCIAL_RESPONSIBILITY_MC_DESC,                                                                                                    
 INS_AGENCY_TRANSFER_MC_DESC,                                                       
 COVERAGE_DECLINED_MC_DESC,                                                     
 AGENCY_VEH_INSPECTED_MC_DESC,                                                                 
 SALVAGE_TITLE_MC_DESC,                                                        
 IS_COMMERCIAL_USE_DESC,                                                                                                    
 IS_USEDFOR_RACING_DESC,                                                                                                    
 IS_COST_OVER_DEFINED_LIMIT_DESC,                                                      
 IS_MORE_WHEELS_DESC,                                                                                                    
 IS_EXTENDED_FORKS_DESC,                                                                                     
 IS_LICENSED_FOR_ROAD_DESC,                                                                                                    
 IS_MODIFIED_INCREASE_SPEED_DESC,                                                                                                    
 IS_MODIFIED_KIT_DESC,                                                   
 IS_TAKEN_OUT_DESC,                                                                                                    
 IS_CONVICTED_CARELESS_DRIVE_DESC,                                                                                                    
IS_CONVICTED_ACCIDENT_DESC,                                                                                                    
 MULTI_POLICY_DISC_APPLIED_MC_DESC,                                                                      
 FullName,                                                    
DATE_OF_BIRTH,                                        
 DrivingLisence,                                                                                                    
 InsuredElseWhere,                                                                                                    
 CompanyName,                                                                                                    
 PolicyNumber,                    
 IS_OTHER_THAN_INSURED,                                                                                                  
CURR_RES_TYPE,                                                                    
 WhichCycle,                                                                                                    
 COST_EQUIPMENT_DESC,                                                                    
 YEARS_INSU_WOL,                                                                    
 YEARS_INSU,                          
 ANY_PRIOR_LOSSES,                                    
 ANY_PRIOR_LOSSES_DESC,                              
 APPLY_PERS_UMB_POL,                              
 APPLY_PERS_UMB_POL_DESC                                      
-- SEAT_BELT_CREDIT                 
FROM APP_AUTO_GEN_INFO                                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                               
 AND IS_ACTIVE = 'Y'                                                                          
                                                                                                        
                                                                                                        
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                         
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM      

--Added by Charles on 24-Dec-09 for Itrack 6830
INSERT INTO POL_UNDERWRITING_TIER  
(
CUSTOMER_ID,
POLICY_ID,
POLICY_VERSION_ID,
UNDERWRITING_TIER,
UNTIER_ASSIGNED_DATE,
CAP_INC,
CAP_DEC,
CAP_RATE_CHANGE_REL,
CAP_MIN_MAX_ADJUST,
ACL_PREMIUM,
CREATED_BY,
CREATED_DATETIME
)
select

@CUSTOMER_ID,
@TEMP_POLICY_ID,
@TEMP_POLICY_VERSION_ID,
UNDERWRITING_TIER,
UNTIER_ASSIGNED_DATE,
CAP_INC,
CAP_DEC,
CAP_RATE_CHANGE_REL,
CAP_MIN_MAX_ADJUST,
ACL_PREMIUM,
CREATED_BY,
CREATED_DATETIME
FROM APP_UNDERWRITING_TIER                                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                               
  --Added till here                                                                                            
/* BY PRAVESH                                                      
-- diary                                
                                                                                              
declare @LISTTYPEID int--New Business =7                                                                                              
declare @UNDERWRITER int -- 0                                                                       
                              
select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                                              
from Pol_customer_policy_list                                                                                              
where Customer_ID=@CUSTOMER_ID and  POLICY_ID=@TEMP_POLICY_ID and   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                                          
INSERT into TODOLIST                                                 
 (                                      
  RECBYSYSTEM,    RECDATE,    FOLLOWUPDATE,    LISTTYPEID,    POLICYCLIENTID,    POLICYID,    POLICYVERSION,                                                                           
  POLICYCARRIERID,    POLICYBROKERID,    SUBJECTLINE,    LISTOPEN,    SYSTEMFOLLOWUPID,    PRIORITY,                                                                                     
  TOUSERID,    FROMUSERID,    STARTTIME,    ENDTIME,    NOTE,    PROPOSALVERSION,    QUOTEID,    CLAIMID,                                
  CLAIMMOVEMENTID,    TOENTITYID,    FROMENTITYID,    CUSTOMER_ID,    APP_ID,    APP_VERSION_ID,    POLICY_ID,                                                                                                
  POLICY_VERSION_ID                                                           
 )                                     
values                                                                                                
 (                                                                                                
  null,    getdate(),    getdate(),    7,    @CUSTOMER_ID,    @TEMP_POLICY_ID,                                                                                   
  @TEMP_POLICY_VERSION_ID,    null,    null,'New Application Submitted',    'Y',                                                                                                
  null,    'M',   @UNDERWRITER,    @CREATED_BY,    null,    null,    null,                                    
  null,    null,    null,    null,    null,    null,                                                                                                
  @CUSTOMER_ID,    @APP_ID,    @APP_VERSION_ID,    @TEMP_POLICY_ID,    @TEMP_POLICY_VERSION_ID                                                              
 )                                                    
                        
                                                                                            
 select @TEMP_ERROR_CODE = @@ERROR                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                               
*/                                                                                                        
                                                  
-- UPDATE APP_LIST SET IS_ACTIVE = 'N'                                                                  
 --WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID != @APP_VERSION_ID                         
                                                              
 --Update status                                           
 UPDATE    APP_LIST SET IS_ACTIVE='N',APP_STATUS='Unconfirmed'                                                              
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID <> @APP_VERSION_ID                                                                                                               
                                                                     
 UPDATE APP_LIST SET APP_STATUS = 'Complete'                                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                           
                           
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                            
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                                                       
                                                       
                                                                                                        
 COMMIT TRAN                                               
 SET @RESULT = @TEMP_POLICY_ID                                                                                         
 RETURN @RESULT                                                                                  
                                                                
--ADDED BY VJ ON 01-02-2006                                                                
--END                                                                
                                                                                                        
 PROBLEM:                                                            
 IF (@TEMP_ERROR_CODE <> 0)                                                
  BEGIN                                                                                                        
     ROLLBACK TRAN                                                                                                        
   SET @RESULT = -1                                                                                     
   RETURN @RESULT                                                                                                        
  END                                                                                                        
                                                                                                         
 ELSE                                                                                                        
 BEGIN                                                                    
   SET @RESULT = @TEMP_POLICY_ID                                                                       
   RETURN @RESULT                                                                                                          
  END                          
                     
  IF EXISTS( SELECT * FROM APP_PRIOR_LOSS_INFO   
    WHERE DBO.PIECE(DRIVER_NAME,'^',1)= CAST(@CUSTOMER_ID AS VARCHAR)   
    AND DBO.PIECE(DRIVER_NAME,'^',2)  = CAST(@APP_ID AS VARCHAR(10))   
    AND DBO.PIECE(DRIVER_NAME,'^',3)  <> CAST(@APP_VERSION_ID AS VARCHAR)   
    AND DBO.PIECE(DRIVER_NAME,'^',5)  = 'APP'  
   )  
  BEGIN  
   UPDATE APP_PRIOR_LOSS_INFO SET DRIVER_NAME='',IS_ACTIVE='N'   
   WHERE DBO.PIECE(DRIVER_NAME,'^',1)= CAST(@CUSTOMER_ID AS VARCHAR(10))   
   AND DBO.PIECE(DRIVER_NAME,'^',2)  = CAST(@APP_ID AS VARCHAR(10))   
   AND DBO.PIECE(DRIVER_NAME,'^',3)  <> CAST(@APP_VERSION_ID AS VARCHAR(10))   
   AND DBO.PIECE(DRIVER_NAME,'^',5)  = 'APP'  
  END  
                                                                                                       
END


GO

