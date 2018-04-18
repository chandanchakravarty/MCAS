IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_UMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_UMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*            
drop proc dbo.Proc_ConvertApplicationToPolicy_UMB                                                     
Modified By     : Pravesh                               
Modified Date   : 23-10-2006                    
Purpose         : make call a comman procedure for Comman data LIke POL_CUSTOMER_POLICY_LIST,POL_APPLICANT_LIST and TODOLIST            

Modified By     : Pravesh                               
Modified Date   : 13 June 2007
Purpose         : Copy New added  Column CLIENT_UPDATE_DATE in APP_UMBRELLA_LIMITS and POL_UMBRELLA_LIMITS

*/            
-- drop proc dbo.Proc_ConvertApplicationToPolicy_UMB                                                                                                                           
CREATE proc dbo.Proc_ConvertApplicationToPolicy_UMB                                                                                                       
@CUSTOMER_ID int,                                                                                                      
@APP_ID int,                                                                                                      
@APP_VERSION_ID smallint,                                                                                                      
@CREATED_BY int,                                                                                                      
@PARAM1 int = NULL,                                                                              
@PARAM2 int = NULL,                                                                          
@PARAM3 int = NULL,                                           
@CALLED_FROM NVARCHAR(30),                                                                           
@RESULT int output                                                                                             
as                                                                                                      
begin             
--ADDED BY PRAVESH                
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
 RETURN @RESULT                                                 
END                                                  
ELSE                                                  
BEGIN                                                                                                  
begin tran                                                                                                      
declare @TEMP_ERROR_CODE int                                                                                                     
declare @TEMP_POLICY_ID int                                                                                                       
declare @TEMP_POLICY_VERSION_ID int                               
                                          
select @TEMP_POLICY_ID = MAX(ISNULL(POLICY_ID,0))+1  from POL_CUSTOMER_POLICY_LIST                                                               
where CUSTOMER_ID = @CUSTOMER_ID                               
if @TEMP_POLICY_ID IS NULL OR @TEMP_POLICY_ID = ''                             
 begin                            
  set @TEMP_POLICY_ID = 1                                                                                 
 end                                                                                                     
set @TEMP_POLICY_VERSION_ID = 1                                          
                                    
                                    
--1. Insert data in POL_CUSTOMER_POLICY_LIST                                                                   
                                 
if @CALLED_FROM = 'ANYWAY'                                                                                                
insert into POL_CUSTOMER_POLICY_LIST                                                                                                       
(                                                                                                      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                        
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                  
                
 CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP, PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                    
                     
                      
                         
                         
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                                                      



   
    
      
       
          
          
            
               
               
                  
                     
                      
                      
                        
                          
                            
 POLICY_DISP_VERSION ,IS_HOME_EMP                                                                                                     
 )                                                                                                            
select                                                                                         
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                                                                
 APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                                                      
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                                                                                                 
 COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                 
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                                                                     
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'SUSPENDED',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                  
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0'                 
 ,IS_HOME_EMP                            
 from APP_LIST                                               
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                       
                                          
        
ELSE                                                                     
                   
insert into POL_CUSTOMER_POLICY_LIST                                                                            
(                                                
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                                      
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                                      


 
     
      
        
         
          
             
              
                
 CREATED_DATETIME,MODifIED_BY,LasT_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                             
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                                                       


  
    
      
        
          
          
            
              
                
                  
                              
                               
 POLICY_DISP_VERSION ,IS_HOME_EMP                                                                     
 )                                                                                                            
select                  
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                    
APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                 
CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                                                                         
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                                              
CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                                                                                      
APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'SUSPENDED',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                                                                      
CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',IS_HOME_EMP                                                                                                       
 from APP_LIST                                                                        
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                          
                                                                                                      
                                          
                                  
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                    
                                                       
                                    
-- 2.To insert data in  POL_APPLICANT_LIST                                                               
                                          
 IF EXISTS (                                          
  SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                           
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                        
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                                          
                                          
  )                                                       
BEGIN                                                                              
                                    
insert into POL_APPLICANT_LIST                                      
(                                                                                                  
 POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_BY,CREATED_DATETIME,                                                                                                  
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
 ,CLT_APPLICANT_LIST  B,                                                                            
 APP_LIST C                                              
 WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                                                      
                                    
  AND A.APPLICANT_ID = B.APPLICANT_ID and B.IS_ACTIVE='Y'                                                                               
  AND C.CUSTOMER_ID = @CUSTOMER_ID AND C.APP_ID = @APP_ID AND C.APP_VERSION_ID = @APP_VERSION_ID                                                                            
  AND C.IS_ACTIVE = 'Y'                                                                                                     
*/                                                            
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                                                                  
                                                                            
--3. To insert data in POL_UMBRELLA_REAL_ESTATE_LOCATIONS           
                       
insert into POL_UMBRELLA_REAL_ESTATE_LOCATION                                                                                      
(                                                                                                 
 CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,LOCATION_ID,CLIENT_LOCATION_NUMBER,                                                                                                
 ADDRESS_1,ADDRESS_2,CITY,COUNTY,STATE,ZIPCODE,PHONE_NUMBER,FAX_NUMBER,                                   
 REMARKS,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                    
 LOCATION_NUMBER,OCCUPIED_BY,NUM_FAMILIES,BUSS_FARM_PURSUITS,BUSS_FARM_PURSUITS_DESC,LOC_EXCLUDED,PERS_INJ_COV_82,
OTHER_POLICY                      
)                                                                             
                                                                                
select                                                                                                       
   @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,LOCATION_ID,CLIENT_LOCATION_NUMBER,                                                        
   ADDRESS_1,ADDRESS_2,CITY,COUNTY,STATE,ZIPCODE,PHONE_NUMBER,FAX_NUMBER,                                    
   REMARKS,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,                                    
   LOCATION_NUMBER,OCCUPIED_BY,NUM_FAMILIES,BUSS_FARM_PURSUITS,BUSS_FARM_PURSUITS_DESC,LOC_EXCLUDED,PERS_INJ_COV_82,
OTHER_POLICY                      
   from APP_UMBRELLA_REAL_ESTATE_LOCATION                                                                                                          
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                    
   and IS_Active='Y'                                                                                              
                                                                                                      
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                          
                                                    
                
/*          
--Following code to insert data at POL_UMBRELLA_DWELLINGS_INFO is being commented as the screen has been removed                              
-- 4. To insert data in POL_UMBRELLA_DWELLINGS_INFO                                    
insert into POL_UMBRELLA_DWELLINGS_INFO                                     
(                                                                                                 
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,LOCATION_ID,                                                                                                
DWELLING_NUMBER,YEAR_BUILT,PURCHASE_YEAR,PURCHASE_PRICE,MARKET_VALUE,                                                                                    
REPLACEMENT_COST,BUILDING_TYPE,OCCUPANCY,NEED_OF_UNITS,USAGE,                                                                                                
NEIGHBOURS_VISIBLE,OCCUPIED_DAILY,NO_WEEKS_RENTED,IS_ACTIVE,CREATED_BY,                                                                                                
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COMMENTDWELLINGOWNED,SUB_LOC_ID,REPAIR_COST                                                                                                  
)                          
                                                        
select                                                                                                 
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,LOCATION_ID,                      
DWELLING_NUMBER,YEAR_BUILT,PURCHASE_YEAR,PURCHASE_PRICE,MARKET_VALUE,                                                               
REPLACEMENT_COST,BUILDING_TYPE,OCCUPANCY,NEED_OF_UNITS,USAGE,                                                                                                
NEIGHBOURS_VISIBLE,OCCUPIED_DAILY,NO_WEEKS_RENTED,IS_ACTIVE,@CREATED_BY,                                                                                                
getdate(),null,null,COMMENTDWELLINGOWNED,SUB_LOC_ID,REPAIR_COST                                                                                                  
from APP_UMBRELLA_DWELLINGS_INFO                                                                                                
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                    
  and Is_Active='Y' and LOCATION_ID in                                                                             
 (Select LOCATION_ID from POL_UMBRELLA_REAL_ESTATE_LOCATION                                                                             
 where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                   
                                                  
                                                                                                
 select @TEMP_ERROR_CODE = @@ERROR                                                                                               
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                                                                          
 --Following code to insert data at POL_UMBRELLA_RATING_INFO is being commented as the screen has been removed                                                                                                                   
-- 5. To insert data in POL_UMBRELLA_RATING_INFO                                     
                                                                                               
insert into POL_UMBRELLA_RATING_INFO                                                                                          
(                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,HYDRANT_DIST,                                                                                                
FIRE_STATION_DIST,IS_UNDER_CONSTRUCTION,EXPERIENCE_CREDIT,IS_AUTO_POL_WITH_CARRIER,PERSONAL_LIAB_TER_CODE,                                                                                                
PROT_CLASS,RATING_METHOD,NO_OF_FAMILIES,EXTERIOR_CONSTRUCTION,EXTERIOR_OTHER_DESC,                                                                                                
FOUNDATION,FOUNDATION_OTHER_DESC,ROOF_TYPE,ROOF_OTHER_DESC,PRIMARY_HEAT_TYPE,                                                                                                
SECONDARY_HEAT_TYPE,MONTH_OCC_EACH_YEAR,ADD_COVERAGE_INFO,IS_OUTSIDE_STAIR,TOT_SQR_FOOTAGE,                                        
GARAGE_SQR_FOOTAGE,BREEZE_SQR_FOOTAGE,BASMT_SQR_FOOTAGE,WIRING_RENOVATION,WIRING_UPDATE_YEAR,                                                                                                
PLUMBING_RENOVATION,PLUMBING_UPDATE_YEAR,HEATING_RENOVATION,HEATING_UPDATE_YEAR,ROOFING_RENOVATION,                                                                                                
ROOFING_UPDATE_YEAR,NO_OF_AMPS,CIRCUIT_BREAKERS,PROTECTIVE_DEVICES,TEMPERATURE,                                          
SMOKE,BURGLAR,FIRE_PLACES,NO_OF_WOOD_STOVES,SWIMMING_POOL,                                                                                     
SWIMMING_POOL_TYPE,SECONDARY_HEAT_OTHER_DESC,PRIMARY_HEAT_OTHER_DESC,DWELLING_CONST_DATE,CENT_ST_BURG_FIRE,                   
CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,TWO_MORE_FIRE,                                    
NUM_LOC_ALARMS_APPLIES,CONSTRUCTION_CODE                                        
)     
select                                                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,HYDRANT_DIST,                                                         
FIRE_STATION_DIST,IS_UNDER_CONSTRUCTION,EXPERIENCE_CREDIT,IS_AUTO_POL_WITH_CARRIER,PERSONAL_LIAB_TER_CODE,                                                                                 
PROT_CLASS,RATING_METHOD,NO_OF_FAMILIES,EXTERIOR_CONSTRUCTION,EXTERIOR_OTHER_DESC,                                                                                                
FOUNDATION,FOUNDATION_OTHER_DESC,ROOF_TYPE,ROOF_OTHER_DESC,PRIMARY_HEAT_TYPE,                                                                      
SECONDARY_HEAT_TYPE,MONTH_OCC_EACH_YEAR,ADD_COVERAGE_INFO,IS_OUTSIDE_STAIR,TOT_SQR_FOOTAGE,                                                                         
GARAGE_SQR_FOOTAGE,BREEZE_SQR_FOOTAGE,BASMT_SQR_FOOTAGE,WIRING_RENOVATION,WIRING_UPDATE_YEAR,                                                                                             
PLUMBING_RENOVATION,PLUMBING_UPDATE_YEAR,HEATING_RENOVATION,HEATING_UPDATE_YEAR,ROOFING_RENOVATION,                                      
ROOFING_UPDATE_YEAR,NO_OF_AMPS,CIRCUIT_BREAKERS,PROTECTIVE_DEVICES,TEMPERATURE,                                                                                                
SMOKE,BURGLAR,FIRE_PLACES,NO_OF_WOOD_STOVES,SWIMMING_POOL,            
SWIMMING_POOL_TYPE,SECONDARY_HEAT_OTHER_DESC,PRIMARY_HEAT_OTHER_DESC,DWELLING_CONST_DATE,CENT_ST_BURG_FIRE,                                                                                                
CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,TWO_MORE_FIRE,NUM_LOC_ALARMS_APPLIES,CONSTRUCTION_CODE                                                                                                
from APP_UMBRELLA_RATING_INFO                                    
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                 
  and DWELLING_ID in (Select DWELLING_ID from POL_UMBRELLA_DWELLINGS_INFO                                                                            
 where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                             
                                                                                          
 select @TEMP_ERROR_CODE = @@ERROR                                                             
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                             
                                                                                                
                                                                                           
*/                                  
-- 6. To insert data in POL_UMBRELLA_GEN_INFO                                    
                                    
insert into POL_UMBRELLA_GEN_INFO                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ANY_AIRCRAFT_OWNED_LEASED,                                    
ANY_OPERATOR_CON_TRAFFIC,ANY_OPERATOR_IMPIRED,ANY_SWIMMING_POOL,REAL_STATE_VEHICLE_USED,                                    
REAL_STATE_VEH_OWNED_HIRED,ENGAGED_IN_FARMING,HOLD_NON_COMP_POSITION,ANY_FULL_TIME_EMPLOYEE,                                    
NON_OWNED_PROPERTY_CARE,BUSINESS_PROF_ACTIVITY,REDUCED_LIMIT_OF_LIBLITY,ANIMALS_EXO_PETS_HISTORY,                                    
ANY_COVERAGE_DECLINED,ANIMALS_EXOTIC_PETS,INSU_TRANSFERED_IN_AGENCY,PENDING_LITIGATIONS,                                    
IS_TEMPOLINE,REMARKS,ANY_AIRCRAFT_OWNED_LEASED_DESC,ANY_OPERATOR_CON_TRAFFIC_DESC,ANY_OPERATOR_IMPIRED_DESC,                        
ANY_SWIMMING_POOL_DESC,REAL_STATE_VEHICLE_USED_DESC,REAL_STATE_VEH_OWNED_HIRED_DESC,ENGAGED_IN_FARMING_DESC,                      
HOLD_NON_COMP_POSITION_DESC,ANY_FULL_TIME_EMPLOYEE_DESC,NON_OWNED_PROPERTY_CARE_DESC,BUSINESS_PROF_ACTIVITY_DESC,                      
REDUCED_LIMIT_OF_LIBLITY_DESC,ANIMALS_EXOTIC_PETS_DESC,ANY_COVERAGE_DECLINED_DESC,INSU_TRANSFERED_IN_AGENCY_DESC,                      
PENDING_LITIGATIONS_DESC,IS_TEMPOLINE_DESC,HAVE_NON_OWNED_AUTO_POL_DESC,INS_DOMICILED_OUTSIDE_DESC,                      
HOME_DAY_CARE_DESC,HAVE_NON_OWNED_AUTO_POL,INS_DOMICILED_OUTSIDE,HOME_DAY_CARE,                    
CALCULATIONS,HOME_RENT_DWELL,HOME_RENT_DWELL_DESC,WAT_DWELL,WAT_DWELL_DESC,RECR_VEH,RECR_VEH_DESC,                    
AUTO_CYCL_TRUCKS,AUTO_CYCL_TRUCKS_DESC,APPLI_UNDERSTAND_LIABILITY_EXCLUDED,APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC,                    
UND_REMARKS,FAMILIES,OFFICE_PREMISES,RENTAL_DWELLINGS_UNIT,                    
IS_ACTIVE,CREATED_BY,                      
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME                     
)                              
select                                                                              
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,ANY_AIRCRAFT_OWNED_LEASED,                                    
ANY_OPERATOR_CON_TRAFFIC,ANY_OPERATOR_IMPIRED,ANY_SWIMMING_POOL,REAL_STATE_VEHICLE_USED,                                    
REAL_STATE_VEH_OWNED_HIRED,ENGAGED_IN_FARMING,HOLD_NON_COMP_POSITION,ANY_FULL_TIME_EMPLOYEE,                                    
NON_OWNED_PROPERTY_CARE,BUSINESS_PROF_ACTIVITY,REDUCED_LIMIT_OF_LIBLITY,ANIMALS_EXO_PETS_HISTORY,                                    
ANY_COVERAGE_DECLINED,ANIMALS_EXOTIC_PETS,INSU_TRANSFERED_IN_AGENCY,PENDING_LITIGATIONS,                                    
IS_TEMPOLINE,REMARKS,ANY_AIRCRAFT_OWNED_LEASED_DESC,ANY_OPERATOR_CON_TRAFFIC_DESC,ANY_OPERATOR_IMPIRED_DESC,                        
ANY_SWIMMING_POOL_DESC,REAL_STATE_VEHICLE_USED_DESC,REAL_STATE_VEH_OWNED_HIRED_DESC,ENGAGED_IN_FARMING_DESC,                      
HOLD_NON_COMP_POSITION_DESC,ANY_FULL_TIME_EMPLOYEE_DESC,NON_OWNED_PROPERTY_CARE_DESC,BUSINESS_PROF_ACTIVITY_DESC,                      
REDUCED_LIMIT_OF_LIBLITY_DESC,ANIMALS_EXOTIC_PETS_DESC,ANY_COVERAGE_DECLINED_DESC,INSU_TRANSFERED_IN_AGENCY_DESC,                      
PENDING_LITIGATIONS_DESC,IS_TEMPOLINE_DESC,HAVE_NON_OWNED_AUTO_POL_DESC,INS_DOMICILED_OUTSIDE_DESC,                      
HOME_DAY_CARE_DESC,HAVE_NON_OWNED_AUTO_POL,INS_DOMICILED_OUTSIDE,HOME_DAY_CARE,             
CALCULATIONS,HOME_RENT_DWELL,HOME_RENT_DWELL_DESC,WAT_DWELL,WAT_DWELL_DESC,RECR_VEH,RECR_VEH_DESC,                    
AUTO_CYCL_TRUCKS,AUTO_CYCL_TRUCKS_DESC,APPLI_UNDERSTAND_LIABILITY_EXCLUDED,APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC,                    
UND_REMARKS,FAMILIES,OFFICE_PREMISES,RENTAL_DWELLINGS_UNIT,                      
IS_ACTIVE,@CREATED_BY,GETDATE(),null,null                                      
from APP_UMBRELLA_GEN_INFO                                      
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                             
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                         
                                                                                            
                               
      
-- 8.  Insert data in POL_UMBRELLA_LIMITS                                    
                                     
                                                             
insert into POL_UMBRELLA_LIMITS                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_LIMITS,RETENTION_LIMITS,UNINSURED_MOTORIST_LIMIT,                                    
UNDERINSURED_MOTORIST_LIMIT,OTHER_LIMIT,OTHER_DESCRIPTION,BASIC,RESIDENCES_OWNER_OCCUPIED,                                   
NUM_OF_RENTAL_UNITS,RENTAL_UNITS,NUM_OF_AUTO,AUTOMOBILES,NUM_OF_OPERATORS,OPER_UNDER_AGE,                                    
NUM_OF_UNLIC_RV,UNLIC_RV,NUM_OF_UNINSU_MOTORIST,UNISU_MOTORIST,UNDER_INSURED_MOTORIST,                                    
WATERCRAFT,NUM_OF_OTHER,OTHER,DEPOSIT,ESTIMATED_TOTAL_PRE,CALCULATIONS,IS_ACTIVE,CREATED_BY,                                    
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,TERRITORY ,CLIENT_UPDATE_DATE        
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,POLICY_LIMITS,RETENTION_LIMITS,                                    
UNINSURED_MOTORIST_LIMIT,UNDERINSURED_MOTORIST_LIMIT,OTHER_LIMIT,OTHER_DESCRIPTION,BASIC,                                    
RESIDENCES_OWNER_OCCUPIED,NUM_OF_RENTAL_UNITS,RENTAL_UNITS,NUM_OF_AUTO,AUTOMOBILES,NUM_OF_OPERATORS,                                    
OPER_UNDER_AGE,NUM_OF_UNLIC_RV,UNLIC_RV,NUM_OF_UNINSU_MOTORIST,UNISU_MOTORIST,UNDER_INSURED_MOTORIST,                                    
WATERCRAFT,NUM_OF_OTHER,OTHER,DEPOSIT,ESTIMATED_TOTAL_PRE,CALCULATIONS,IS_ACTIVE,@CREATED_BY,                                    
GETDATE(),null,null,TERRITORY ,CLIENT_UPDATE_DATE                                  
  from APP_UMBRELLA_LIMITS                                    
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                      
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                    
                                  
                               
-- 9. Insert data in POL_UMBRELLA_DRIVER_DETAILS                                    
--------Driver / Operator Info                                    
                                    
insert into POL_UMBRELLA_DRIVER_DETAILS                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,                                    
DRIVER_CODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,                                    
DRIVER_COUNTRY,DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,                                    
DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,                                    
DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,                                    
DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                                    
DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                        
DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                                    
DRIVER_US_CITIZEN,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,SAFE_DRIVER_RENEWAL_DISCOUNT,                                    
APP_VEHICLE_PRIN_OCC_ID,VEHICLE_ID,OP_VEHICLE_ID,OP_APP_VEHICLE_PRIN_OCC_ID,OP_DRIVER_COST_GAURAD_AUX,                                    
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,FORM_F95,MOT_VEHICLE_ID,MOT_APP_VEHICLE_PRIN_OCC_ID                                    
)                                    
                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,                                    
DRIVER_CODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,                                    
DRIVER_COUNTRY,DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,                                    
DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,                                    
DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,                                    
DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                                    
DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                                    
DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                                    
DRIVER_US_CITIZEN,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,SAFE_DRIVER_RENEWAL_DISCOUNT,                                    
APP_VEHICLE_PRIN_OCC_ID,VEHICLE_ID,OP_VEHICLE_ID,OP_APP_VEHICLE_PRIN_OCC_ID,OP_DRIVER_COST_GAURAD_AUX,                                    
IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,FORM_F95,MOT_VEHICLE_ID,MOT_APP_VEHICLE_PRIN_OCC_ID                                    
                                     
 from APP_UMBRELLA_DRIVER_DETAILS                                    
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                         
                                    
/*            
--Following code to insert data at POL_UMBRELLA_RATING_INFO is being commented as the screen has been removed                                
-- 10. Insert Data in 'POL_UMBRELLA_MVR_INFORMATION'                                  
                                  
insert into POL_UMBRELLA_MVR_INFORMATION                                  
(                                  
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,POL_UMB_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,VIOLATION_ID,                                  
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE                                  
)                                
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DRIVER_ID,APP_UMB_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                                  
VIOLATION_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE                                  
 from  APP_UMBRELLA_MVR_INFORMATION                                  
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
and   DRIVER_ID in (select DRIVER_ID from POL_UMBRELLA_DRIVER_DETAILS where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y')                            
                                   
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                  
*/                                  
                            
                   
                                    
-- 11. Insert data in POL_UMBRELLA_VEHICLE_INFO                                    
      insert into POL_UMBRELLA_VEHICLE_INFO                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,INSURED_VEH_NUMBER,VEHICLE_YEAR,                                    
MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,                                    
TERRITORY,CLASS,REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,                                    
IS_NEW_USED,AMOUNT_COST_NEW,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,                                    
PASSIVE_SEAT_BELT,AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,VEHICLE_CC,                                    
MOTORCYCLE_TYPE,REMARKS,USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,          
UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,IS_ACTIVE,                                    
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,IS_EXCLUDED,OTHER_POLICY                                    
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,VEHICLE_ID,INSURED_VEH_NUMBER,VEHICLE_YEAR,                                    
MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,                                    
TERRITORY,CLASS,REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,                                    
IS_NEW_USED,AMOUNT_COST_NEW,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,                                 
PASSIVE_SEAT_BELT,AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,VEHICLE_CC,                                    
MOTORCYCLE_TYPE,REMARKS,USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,                                    
UNINS_MOTOR_INJURY_COVE,UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,IS_ACTIVE,                                    
@CREATED_BY,GETDATE(),null,null,IS_EXCLUDED,OTHER_POLICY        
                                    
 from APP_UMBRELLA_VEHICLE_INFO                                    
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                         
                                    
                                      
                          
-- 12. Insert data in POL_UMBRELLA_RECREATIONAL_VEHICLES                                    
                                    
                                    
insert into POL_UMBRELLA_RECREATIONAL_VEHICLES                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,REC_VEH_ID,COMPANY_ID_NUMBER,[YEAR],MAKE,                                    
MODEL,SERIAL,STATE_REGISTERED,MANUFACTURER_DESC,HORSE_POWER,DISPLACEMENT,REMARKS,                                    
USED_IN_RACE_SPEED,PRIOR_LOSSES,IS_UNIT_REG_IN_OTHER_STATE,RISK_DECL_BY_OTHER_COMP,                                    
DESC_RISK_DECL_BY_OTHER_COMP,VEHICLE_MODIFIED,VEHICLE_TYPE,ACTIVE,CREATED_BY,                                    
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,VEHICLE_MODIFIED_DETAILS,                        
VEH_LIC_ROAD,REC_VEH_TYPE,REC_VEH_TYPE_DESC,USED_IN_RACE_SPEED_CONTEST,    
OTHER_POLICY,C44,IS_BOAT_EXCLUDED    
                             
)               
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,REC_VEH_ID,COMPANY_ID_NUMBER,[YEAR],MAKE,                                    
MODEL,SERIAL,STATE_REGISTERED,MANUFACTURER_DESC,HORSE_POWER,DISPLACEMENT,REMARKS,                                    
USED_IN_RACE_SPEED,PRIOR_LOSSES,IS_UNIT_REG_IN_OTHER_STATE,RISK_DECL_BY_OTHER_COMP,                                    
DESC_RISK_DECL_BY_OTHER_COMP,VEHICLE_MODIFIED,VEHICLE_TYPE,ACTIVE,@CREATED_BY,                                    
GETDATE(),null,null,VEHICLE_MODIFIED_DETAILS,                        
VEH_LIC_ROAD,REC_VEH_TYPE,REC_VEH_TYPE_DESC,USED_IN_RACE_SPEED_CONTEST,    
OTHER_POLICY,C44,IS_BOAT_EXCLUDED                        
  from APP_UMBRELLA_RECREATIONAL_VEHICLES                
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and ACTIVE = 'Y'                                                              
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                         
                                    
                                    
-- 13. Insert data in POL_UMBRELLA_WATERCRAFT_INFO                                    
                                    
insert into POL_UMBRELLA_WATERCRAFT_INFO                                    
(                   
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,BOAT_ID,BOAT_NO,BOAT_NAME,[YEAR],                                    
MAKE,MODEL,HULL_ID_NO,STATE_REG,REG_NO,[POWER],HULL_TYPE,OTHER_HULL_TYPE,HULL_MATERIAL,                                    
FUEL_TYPE,HULL_DESIGN,DATE_PURCHASED,LENGTH,WEIGHT,MAX_SPEED,COST_NEW,PRESENT_VALUE,BERTH_LOC,                                    
WATERS_NAVIGATED,TERRITORY,TYPE_OF_WATERCRAFT,INSURING_VALUE,WATERCRAFT_HORSE_POWER,                                    
DESC_OTHER_WATERCRAFT,DEDUCTIBLE,LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,                                    
DUAL_OWNERSHIP,REMOVE_SAILBOAT,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                  
TWIN_SINGLE,INCHES,USED_PARTICIPATE,WATERCRAFT_CONTEST,          
LOCATION_ADDRESS,LOCATION_CITY,LOCATION_STATE,LOCATION_ZIP,COV_TYPE_BASIS,    
OTHER_POLICY,    
IS_BOAT_EXCLUDED    
    
                                  
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,BOAT_ID,BOAT_NO,BOAT_NAME,[YEAR],                                    
MAKE,MODEL,HULL_ID_NO,STATE_REG,REG_NO,[POWER],HULL_TYPE,OTHER_HULL_TYPE,HULL_MATERIAL,                                    
FUEL_TYPE,HULL_DESIGN,DATE_PURCHASED,LENGTH,WEIGHT,MAX_SPEED,COST_NEW,PRESENT_VALUE,BERTH_LOC,                                    
WATERS_NAVIGATED,TERRITORY,TYPE_OF_WATERCRAFT,INSURING_VALUE,WATERCRAFT_HORSE_POWER,                                    
DESC_OTHER_WATERCRAFT,DEDUCTIBLE,LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,                                    
DUAL_OWNERSHIP,REMOVE_SAILBOAT,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null,TWIN_SINGLE,INCHES,                  
USED_PARTICIPATE,WATERCRAFT_CONTEST,          
LOCATION_ADDRESS,LOCATION_CITY,LOCATION_STATE,LOCATION_ZIP,COV_TYPE_BASIS,   
OTHER_POLICY,    
IS_BOAT_EXCLUDED          
  from APP_UMBRELLA_WATERCRAFT_INFO                                    
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                   
              
/*          
--Following code to insert data at POL_UMBRELLA_WATERCRAFT_ENGINE_INFO is being commented as the screen has been removed                                                  
-- 14. Insert Data in POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                                    
                                    
INSERT INTO POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                                                                                  
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ENGINE_ID,ENGINE_NO,YEAR,MAKE,MODEL,SERIAL_NO,                                                                                  
HORSEPOWER,ASSOCIATED_BOAT,OTHER,INSURING_VALUE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                                    
MODIFIED_BY,LAST_UPDATED_DATETIME                                 
)                                                         
SELECT @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,ENGINE_ID,ENGINE_NO, YEAR,                                         
MAKE,MODEL,SERIAL_NO,HORSEPOWER,ASSOCIATED_BOAT,OTHER,INSURING_VALUE,IS_ACTIVE,CREATED_BY,                                    
GETDATE(),null,null                                                                                  
                                                                                  
FROM APP_WATERCRAFT_ENGINE_INFO                                                                                  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                               
                                                                                                  
select @TEMP_ERROR_CODE = @@ERROR                                                                                                     
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                        
                                    
          
--Following code to insert data at POL_UMBRELLA_FARM_INFO is being commented as the screen has been removed                                                  
-- 15. Insert data in POL_UMBRELLA_FARM_INFO                                    
                             
insert into POL_UMBRELLA_FARM_INFO                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,FARM_ID,LOCATION_NUMBER,ADDRESS_1,ADDRESS_2,CITY,                                    
COUNTY,STATE,ZIPCODE,PHONE_NUMBER,FAX_NUMBER,NO_OF_ACRES,OCCUPIED_BY_APPLICANT,RENTED_TO_OTHER,                                    
EMP_FULL_PART,REMARKS,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME                                    
)         
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,FARM_ID,LOCATION_NUMBER,ADDRESS_1,ADDRESS_2,CITY,                                    
COUNTY,STATE,ZIPCODE,PHONE_NUMBER,FAX_NUMBER,NO_OF_ACRES,OCCUPIED_BY_APPLICANT,RENTED_TO_OTHER,                                    
EMP_FULL_PART,REMARKS,IS_ACTIVE,@CREATED_BY,GETDATE(),null,null                                    
 from  APP_UMBRELLA_FARM_INFO                                    
   where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID and IS_ACTIVE = 'Y'                                                                                   
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                   
*/                                    
-- 16. Insert data in POL_UMBRELLA_UNDERLYING_POLICIES                                    
                           
insert into POL_UMBRELLA_UNDERLYING_POLICIES                  
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_NUMBER,POLICY_LOB,POLICY_COMPANY,                                   
POLICY_TERMS,POLICY_START_DATE,POLICY_EXPIRATION_DATE,POLICY_PREMIUM,QUESTION,QUES_DESC,                                    
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,IS_POLICY,STATE_ID                                    
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,POLICY_NUMBER,POLICY_LOB,POLICY_COMPANY,                                    
POLICY_TERMS,POLICY_START_DATE,POLICY_EXPIRATION_DATE,POLICY_PREMIUM,QUESTION,QUES_DESC,                            
@CREATED_BY,GETDATE(),null,null,IS_POLICY,STATE_ID                                    
  from APP_UMBRELLA_UNDERLYING_POLICIES                                    
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                    
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                    
                                    
                                    
                                    
-- 17. Insert data in POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                    
                                    
insert into POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                    
(                                    
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_NUMBER,COVERAGE_DESC,COVERAGE_AMOUNT,                                    
POLICY_TEXT,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COV_CODE,IS_POLICY                                    
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,POLICY_NUMBER,COVERAGE_DESC,COVERAGE_AMOUNT,                                    
POLICY_TEXT,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COV_CODE,IS_POLICY                                    
  from APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES                                    
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                    
                                    
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                      
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                    
                                    
/* by pravesh                                    
-- Insert Diary entry                                    
                                    
declare @UNDERWRITER int                                     
                                    
select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                                                
from POL_CUSTOMER_POLICY_LIST                                                                                                
where CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@TEMP_POLICY_ID AND   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                                                              
INSERT into TODOLIST                                                                                                  
 (                                                                                                  
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,                                    
 POLICYVERSION,POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,                                    
  SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,                                    
  PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,                                    
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                                                                                                  
 )                                                                                                   
values                                                                        
 (                                                                                                  
  null,getdate(),getdate(),7,@CUSTOMER_ID,@TEMP_POLICY_ID,                                                                                               
  @TEMP_POLICY_VERSION_ID,null, null,'New Application Submitted','Y',                                                                    
  null,'M',@CREATED_BY,@CREATED_BY,null,null,null,                                          
  null,null,null,null,null,null,                     
  @CUSTOMER_ID, @APP_ID,@APP_VERSION_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID                                                                                                  
 )                                                 
                                                       
                                                                                            
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                          
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                 
*/                                      
----ADDED BY PRAVESH                
                           
insert into POL_UMBRELLA_COVERAGES                                   
(                                    
CUSTOMER_ID,POL_ID,POL_VERSION_ID,COVERAGE_ID,COVERAGE_CODE_ID,                
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME                                    
)                                    
select @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,COVERAGE_ID,COVERAGE_CODE_ID,                
@CREATED_BY,GETDATE(),null,null                                    
  from APP_UMBRELLA_COVERAGES                                    
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                    
                                     
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                 
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                       
                                   
------END HERE                
                                
--New update of app_status being added to change the status of application                                                                                    
                                                                                      
-- UPDATE APP_LIST SET IS_ACTIVE = 'N'                                                
 --WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID != @APP_VERSION_ID                                             
                                            
 --Update status                                            
  UPDATE    APP_LIST SET IS_ACTIVE='N',APP_STATUS='Unconfirmed'                                            
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID <> @APP_VERSION_ID                                       
                                                   
 UPDATE APP_LIST SET APP_STATUS = 'Complete'                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                      
                                                                                     
 SELECT @TEMP_ERROR_CODE = @@ERROR                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                    
                                                                                                     
 commit tran                                                                                                      
 set @RESULT = @TEMP_POLICY_ID                                                        
                                                      
--Update status                                               
  return @RESULT                                                           
                                              
--END                                                                                                     
                                                                                                      
PROBLEM:                                              
                                          
if (@TEMP_ERROR_CODE <> 0)                       
 begin                        
   rollback tran                                            
    set @RESULT = -1                                              
    return @RESULT                                                                                                      
  end                                                                        
else                                                                                                      
  begin                                             
    set @RESULT = @TEMP_POLICY_ID                              
    return @RESULT                                                   
 end                                                               
--end                                  
END          
          
          
          
          
        
      
    









GO

