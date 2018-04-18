IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_HO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_HO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* =======================================================================                                                                                         
 Proc Name       : dbo.Proc_ConvertApplicationToPolicy_HO                                                                                                      
 Created by      : Ashwani                                                                                                      
 Date            : 04 Nov. 2005                                                                                                      
 Purpose         : Copy the Homeowner Application Data to Policy Tables.                                                                                                            
                                                                                        
 Revison History :                                                                                                                
 Used In         : Wolverine                                                                                             
 Modified By     : Vijay Arora                                                                                            
 Modified Date   : 10-11-2005                                                                                            
 Purpose         : Add the condition of LOB dependent copying of tables.                                                                                                                      
                                                                                         
 Modified By     : Vijay Arora                                                                                            
 Modified Date   : 15-11-2005                                                                                            
 Purpose         : Added some changes as suggested by Pawan marked as --PAWAN.                                                                                     
                                                                                    
 Modified By     : Vijay Arora                                                                                        
 Modified Date   : 07-12-2005                                                                                    
 Purpose     : Change the Display Version in Pol_Customer_Policy_List Table.                                                                                     
                                                                                  
 Modified By     : Vijay Arora                                                                                        
 Modified Date   : 20-12-2005                                                                                    
 Purpose         : Make for check that child records will only copy when master record is active                                                                                  
                                                                      
 Modified By     : SHAFI                                                                                       
 Modified Date   : 18-01-2006                                                                                  
 Purpose         : copy Watercraft section                                                                          
                                                                    
 Modified By     : Deepak Batra                                                                                       
 Modified Date   : 23-01-2006                                                                                  
 Purpose         : Copy the newly added columns for some tables                            
                                                            
 Modified By     : Shafi                                                                                        
 Modified Date   : 06-02-2006     
 Purpose         : Update Status Of Application Of other Versions To Inactive ,Incomplete                                                           
                                     
 Modified By     : Vijay Arora               
 Modified Date   : 08-02-2006                                
 Purpose        : Make check that if policy already created against application than does not ALTER  again.                               
                            
 Modified By    : shafi                                                 
 Modified Date   : June 28 2006                                                                 
 Purpose         : Copy Other Structure Details                               
                  
 Modified By    : RPSINGH                  
 Modified Date   : JULY 10 2006                  
 Purpose         : Adding Code for APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS to POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                  
             
 Modified By    : Ravindra                   
 Modified Date   : JULY 21 2006                  
 Purpose         :             
               
Modify By:    Pravesh Chandel                
Modify Date : 23 Oct 2006                
Purpose     : call a comman Proc for Comman task while converting App To Pol          
             
            
==========================================================================                                                                                                      
Date      Review By          Comments                                                  
==========================================================================                                                                                                           
OCT 16 2006    RPSINGH                  
*/            
--drop proc dbo.Proc_ConvertApplicationToPolicy_HO             
CREATE  proc dbo.Proc_ConvertApplicationToPolicy_HO            
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
          
          
---ADDED BY PRAVESH              
BEGIN TRAN                                                                 
              
DECLARE @TEMP_ERROR_CODE INT                                                                                                
EXEC     @RESULT=Proc_ConvertApplicationToPolicy_ALL  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,@PARAM1,@PARAM2,@PARAM3,@CALLED_FROM,@RESULT                                                                                  
              
IF (@RESULT = -1)  GOTO PROBLEM              
                                                                                              
DECLARE @TEMP_POLICY_ID INT                                                                                                 
DECLARE @TEMP_POLICY_VERSION_ID INT              
DECLARE @TEMP_POLICY_NUMBER NVARCHAR(75)    
SET @TEMP_POLICY_ID=@RESULT              
SELECT   @TEMP_POLICY_VERSION_ID = POLICY_VERSION_ID,@TEMP_POLICY_NUMBER=POLICY_NUMBER FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID               
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
--1. POL_CUSTOMER_POLICY_LIST                                                                             
--CHECK FOR INPUT CONDITION                                                                    
                                   
if @CALLED_FROM = 'ANYWAY'                                                             
insert into POL_CUSTOMER_POLICY_LIST                                                                                                         
(                         
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                    
APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,                 
AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                                       CREATED_DATETIME,MODifIED_BY,LasT_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,       
  
  
  
  
  
  
  
  
   
  
  
    
    
      
        
          
PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                  
CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                      
POLICY_DISP_VERSION,PIC_OF_LOC,IS_HOME_EMP                                                                
 )                                                                                                                  
select                          
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                               
APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                              
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                  
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                       
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                                                         
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                        
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',PIC_OF_LOC ,IS_HOME_EMP                                                                                                            
from APP_LIST                                                     
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                             
                                                
                                                                            
ELSE                                                                           
                                                                            
insert into POL_CUSTOMER_POLICY_LIST                                                                                                             
(                                                                                                            
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                                            
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                           
 CREATED_DATETIME,MODifIED_BY,LasT_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                                                         
  
  
  
  
  
  
  
  
  
  
  
    
    
      
         
         
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                      
 POLICY_DISP_VERSION,PIC_OF_LOC ,IS_HOME_EMP                                 
 )                                                                                                                  
select                                                                
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                                                                      
APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                                                    
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                                                     
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                                                                  
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,          
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                                                                            
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',PIC_OF_LOC,IS_HOME_EMP                                                                                 from APP_LIST                                                                              
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                              
                                                        
                                                
                                    
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                   
                                                             
--ADDED BY VJ ON 01-02-2006                                                              
                                        
 IF EXISTS (                                                
  SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                                                 
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                              
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                                                
                                                
  )                                                              
BEGIN                                                                                    
-- 2. POL_APPLICANT_LIST                                                                     
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
--Pawan                                                                                        
,clt_APPLICANT_LIST  B,                                                                           
APP_LIST C                                                                                             
 WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                                     
--Pawan                                                                                        
   and A.APPLICANT_ID = B.APPLICANT_ID and B.Is_Active='Y'                 
  AND C.IS_ACTIVE = 'Y'                        
 */          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                          
                                                                                        
                                                                                                  
--3. POL_LOCATIONS                                                                                                
insert into POL_LOCATIONS                                                                                            
(                                                                                                       
 CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, LOC_NUM,                                                                                                      
 IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY, LOC_COUNTY,                                                                                                      
 LOC_STATE,LOC_ZIP,LOC_COUNTRY,PHONE_NUMBER,FAX_NUMBER,                   
 DEDUCTIBLE,NAMED_PERILL,DESCRIPTION,IS_ACTIVE,CREATED_BY,                                                                               
 CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,LOC_TERRITORY,LOCATION_TYPE,RENTED_WEEKLY,                
WEEKS_RENTED ,LOSSREPORT_ORDER  ,LOSSREPORT_DATETIME  ,REPORT_STATUS          
                                                                
)                                                                                                           
                                                                                                            
select                                                         
   @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,LOCATION_ID,LOC_NUM,                                                                                                                
   IS_PRIMARY,LOC_ADD1,LOC_ADD2,LOC_CITY,LOC_COUNTY,  
   LOC_STATE,LOC_ZIP, LOC_COUNTRY, PHONE_NUMBER,FAX_NUMBER,                                                                
   DEDUCTIBLE,NAMED_PERILL,DESCRIPTION,IS_ACTIVE,@CREATED_BY,                                                 
   GETDATE(),null,null,LOC_TERRITORY,LOCATION_TYPE,RENTED_WEEKLY,                
WEEKS_RENTED ,LOSSREPORT_ORDER  ,LOSSREPORT_DATETIME  ,REPORT_STATUS                
                                                
                                                              
  from APP_LOCATIONS                                                                                                                
  where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                   
 --Pawan                                                        
 and IS_Active='Y'                                                                                                    
                                                                                                            
 select @TEMP_ERROR_CODE = @@ERROR                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                
                                                          
-- 4. POL_DWELLINGS_INFO                                                                                        
insert into POL_DWELLINGS_INFO                       
(                                                                                                       
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,LOCATION_ID,                                                                                                      
DWELLING_NUMBER,YEAR_BUILT,PURCHASE_YEAR,PURCHASE_PRICE,MARKET_VALUE,                                   
REPLACEMENT_COST,BUILDING_TYPE,OCCUPANCY,NEED_OF_UNITS,USAGE,                                                                                                      
NEIGHBOURS_VISIBLE,OCCUPIED_DAILY,NO_WEEKS_RENTED,IS_ACTIVE,CREATED_BY,                                                                              
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COMMENTDWELLINGOWNED,SUB_LOC_ID,REPAIR_COST                                    
,DETACHED_OTHER_STRUCTURES                            
--,PREMISES_LOCATION,PREMISES_DESCRIPTION,PREMISES_USE,PREMISES_CONDITION    ,                     
--PICTURE_ATTACHED,COVERAGE_BASIS,SATELLITE_EQUIPMENT,LOCATION_ADDRESS,LOCATION_CITY,LOCATION_STATE    ,                                
--LOCATION_ZIP,ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,INSURING_VALUE,INSURING_VALUE_OFF_PREMISES                            
,MONTHS_RENTED                                                                                                       
)                     
 select                                                                                                       
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,LOCATION_ID,                                                                                                      
DWELLING_NUMBER,YEAR_BUILT,PURCHASE_YEAR,PURCHASE_PRICE,MARKET_VALUE,                                                 
REPLACEMENT_COST,BUILDING_TYPE,OCCUPANCY,NEED_OF_UNITS,USAGE,                                                                                                      
NEIGHBOURS_VISIBLE,OCCUPIED_DAILY,NO_WEEKS_RENTED,IS_ACTIVE,@CREATED_BY,                                                                         
getdate(),null,null,COMMENTDWELLINGOWNED,SUB_LOC_ID,REPAIR_COST                                     
,DETACHED_OTHER_STRUCTURES                            
--,PREMISES_LOCATION,PREMISES_DESCRIPTION,PREMISES_USE,PREMISES_CONDITION    ,                                
--PICTURE_ATTACHED,COVERAGE_BASIS,SATELLITE_EQUIPMENT,LOCATION_ADDRESS,LOCATION_CITY,LOCATION_STATE    ,                                
--LOCATION_ZIP,ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,INSURING_VALUE,INSURING_VALUE_OFF_PREMISES                            
,MONTHS_RENTED                                                                                                   
                                                                  
from APP_DWELLINGS_INFO                                                                                                     
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                          
--Pawan                                                                                
and Is_Active='Y' and LOCATION_ID in                                                                                   
(Select LOCATION_ID from POL_LOCATIONS                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                         
                            
                            
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                      
                            
-----------POL_OTHER_STRUCTURE_DWELLING     
  
insert into POL_OTHER_STRUCTURE_DWELLING  
 (  
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,OTHER_STRUCTURE_ID,  
PREMISES_LOCATION,PREMISES_DESCRIPTION,PREMISES_USE,PREMISES_CONDITION,  
PICTURE_ATTACHED,COVERAGE_BASIS,SATELLITE_EQUIPMENT,LOCATION_ADDRESS,  
LOCATION_CITY,LOCATION_STATE,LOCATION_ZIP,ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,  
INSURING_VALUE,INSURING_VALUE_OFF_PREMISES,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,  
MODIFIED_BY,LAST_UPDATED_DATETIME,COVERAGE_AMOUNT,LIABILITY_EXTENDED,SOLID_FUEL_DEVICE,APPLY_ENDS  --Added SOLID_FUEL_DEVICE,APPLY_ENDS,Charles(16-Dec-09) Itrack 6681
 )                   
 select                                                                                                       
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,OTHER_STRUCTURE_ID,  
PREMISES_LOCATION,PREMISES_DESCRIPTION,PREMISES_USE,PREMISES_CONDITION,  
PICTURE_ATTACHED,COVERAGE_BASIS,SATELLITE_EQUIPMENT,LOCATION_ADDRESS,  
LOCATION_CITY,LOCATION_STATE,LOCATION_ZIP,ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,   
INSURING_VALUE,INSURING_VALUE_OFF_PREMISES,IS_ACTIVE,@CREATED_BY,getdate(),     
null,null,COVERAGE_AMOUNT,LIABILITY_EXTENDED,SOLID_FUEL_DEVICE,APPLY_ENDS  --Added SOLID_FUEL_DEVICE,APPLY_ENDS,Charles(16-Dec-09) Itrack 6681
FROM APP_OTHER_STRUCTURE_DWELLING  WHERE                          
CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                         
--Pawan            
AND IS_ACTIVE = 'Y'       
and DWELLING_ID in                  
(Select DWELLING_ID from POL_DWELLINGS_INFO                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')   
  
  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                 
                             
/*SELECT * INTO #POL_OTHER_STRUCTURE_DWELLING FROM APP_OTHER_STRUCTURE_DWELLING  WHERE                          
CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                         
--Pawan            
AND IS_ACTIVE = 'Y'       
and DWELLING_ID in                 
(Select DWELLING_ID from POL_DWELLINGS_INFO                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                         
                            
                               
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                                        
    UPDATE #POL_OTHER_STRUCTURE_DWELLING                                 
    SET APP_ID = @TEMP_POLICY_ID, APP_VERSION_ID = @TEMP_POLICY_VERSION_ID,                                
 CREATED_BY = @CREATED_BY,CREATED_DATETIME = Getdate(),                                
 LAST_UPDATED_DATETIME = NULL, MODIFIED_BY = NULL                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                   
                                                        
    INSERT INTO POL_OTHER_STRUCTURE_DWELLING SELECT * FROM #POL_OTHER_STRUCTURE_DWELLING                                                         
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                         
    DROP TABLE #POL_OTHER_STRUCTURE_DWELLING                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM   */                                        
                                                                                                   
                          
                                                                                
                                                                                     
-- 5. POL_HOME_RATING_INFO                 
insert into POL_HOME_RATING_INFO    
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
CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,                            
TWO_MORE_FIRE,NUM_LOC_ALARMS_APPLIES,CONSTRUCTION_CODE,SPRINKER,IS_SUPERVISED,ALARM_CERT_ATTACHED,NEED_OF_UNITS,         
SUBURBAN_CLASS,LOCATED_IN_SUBDIVISION                          
                                       
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
CENT_ST_FIRE,CENT_ST_BURG,DIR_FIRE_AND_POLICE,DIR_FIRE,DIR_POLICE,LOC_FIRE_GAS,                                        
TWO_MORE_FIRE,NUM_LOC_ALARMS_APPLIES,CONSTRUCTION_CODE,SPRINKER,IS_SUPERVISED,ALARM_CERT_ATTACHED,                
NEED_OF_UNITS,SUBURBAN_CLASS,LOCATED_IN_SUBDIVISION                    
                          
               
from APP_HOME_RATING_INFO                           
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                            
--Pawan                                                                                         
and DWELLING_ID in (Select DWELLING_ID from POL_DWELLINGS_INFO                 
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                   
                                            
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                 
             
-- 6. POL_HOME_OWNER_ADD_INT                              
insert into POL_HOME_OWNER_ADD_INT                                             
(                                                                                                      
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,HOLDER_ID,DWELLING_ID,                                                                              
MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,IS_ACTIVE,                                
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,ADD_INT_ID,                                             
HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,                                                                   
HOLDER_STATE,HOLDER_ZIP,BILL_MORTAGAGEE                                                                                                       
)                                                                                                           
select                                        
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,HOLDER_ID,DWELLING_ID,                                                                                                      
MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,IS_ACTIVE,                                                                              
@CREATED_BY,getdate(),null,null,ADD_INT_ID,                                                                                                      
HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,                              
HOLDER_STATE,HOLDER_ZIP,BILL_MORTAGAGEE                                                                                                      
from APP_HOME_OWNER_ADD_INT                                                                                                         
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                         
--Pawan                                                                                         
and Is_Active='Y' and DWELLING_ID in (Select DWELLING_ID from POL_DWELLINGS_INFO                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                   
                                                                                               
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                           
                                   
-- 7. POL_DWELLING_COVERAGE             
/*insert into POL_DWELLING_COVERAGE                                                                                      
(                                                
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,DWELLING_LIMIT,                                                                                            
DWELLING_REPLACE_COST,OTHER_STRU_LIMIT,OTHER_STRU_DESC,PERSONAL_PROP_LIMIT,REPLACEMENT_COST_CONTS,                                                                 
LOSS_OF_USE,PERSONAL_LIAB_LIMIT,MED_PAY_EACH_PERSON,ALL_PERILL_DEDUCTIBLE_AMT,THEFT_DEDUCTIBLE_AMT                                                                              
)                                                                                                           
select                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,DWELLING_LIMIT,                                                                                                      
DWELLING_REPLACE_COST,OTHER_STRU_LIMIT,OTHER_STRU_DESC,PERSONAL_PROP_LIMIT,REPLACEMENT_COST_CONTS,                                                                                                      
LOSS_OF_USE,PERSONAL_LIAB_LIMIT,MED_PAY_EACH_PERSON,ALL_PERILL_DEDUCTIBLE_AMT,THEFT_DEDUCTIBLE_AMT                              
from APP_DWELLING_COVERAGE                                                                    
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                     
--Pawan                                   
and DWELLING_ID in (Select DWELLING_ID from POL_DWELLINGS_INFO                                                                  where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y' 
 
  
  
  
  
  
  
  
  
  
    
    
      
        
          
            
              
                
)                                                                                   
                                                                                                      
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                    
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                   
 */                               
                                
                                
------------POL_OTHER_LOCATIONS                                
 SELECT * INTO #POL_OTHER_LOCATIONS FROM APP_OTHER_LOCATIONS  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                
--Pawan                        
AND IS_ACTIVE = 'Y'                        
and DWELLING_ID in                                                                                   
(Select DWELLING_ID from POL_DWELLINGS_INFO                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                         
                             
                        
                        
SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                                    
    UPDATE #POL_OTHER_LOCATIONS                                 
    SET APP_ID = @TEMP_POLICY_ID, APP_VERSION_ID = @TEMP_POLICY_VERSION_ID,                                
 CREATED_BY = @CREATED_BY,CREATED_DATETIME = Getdate(),                               
 LAST_UPDATED_DATETIME = NULL, MODIFIED_BY = NULL                                
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                             
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM            
                                                        
    INSERT INTO POL_OTHER_LOCATIONS SELECT * FROM #POL_OTHER_LOCATIONS                                                         
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                           
                                                        
    DROP TABLE #POL_OTHER_LOCATIONS                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                               
------------                                
                                                      
                                                                          
-- 8. POL_DWELLING_SECTION_COVERAGES                                                                           
insert into POL_DWELLING_SECTION_COVERAGES                                                                                                      
(                                                             
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,COVERAGE_ID,COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,                                                      
LIMIT_2,LIMIT_2_TYPE,DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,WRITTEN_PREMIUM,                                                     
FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,COVERAGE_TYPE,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,                  
DEDUCTIBLE2_AMOUNT_TEXT,LIMIT_ID,DEDUC_ID,                                              
DEDUCTIBLE,                                              
DEDUCTIBLE_TEXT,ADDDEDUCTIBLE_ID                                            
)                                                                               
select                                                                                                      
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,COVERAGE_ID,                 
COVERAGE_CODE_ID,LIMIT_OVERRIDE,LIMIT_1,LIMIT_1_TYPE,LIMIT_2,                                                         
LIMIT_2_TYPE,DEDUCT_OVERRIDE,DEDUCTIBLE_1,DEDUCTIBLE_1_TYPE,DEDUCTIBLE_2,                                                                                                
DEDUCTIBLE_2_TYPE,WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_SYSTEM_COVERAGE,COVERAGE_TYPE,LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,                                                   
DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,LIMIT_ID,DEDUC_ID,                                              
DEDUCTIBLE,                                              
DEDUCTIBLE_TEXT ,ADDDEDUCTIBLE_ID                                                     
from APP_DWELLING_SECTION_COVERAGES                                                                
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                         
--Pawan                                                                                         
and DWELLING_ID in (Select DWELLING_ID from POL_DWELLINGS_INFO                                                                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                
                                                                                                      
 select @TEMP_ERROR_CODE = @@ERROR                                     
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                  
                                                                                                      
-- 9. POL_DWELLING_ENDORSEMENTS                                 
insert into POL_DWELLING_ENDORSEMENTS                                                                  
(                  
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DWELLING_ID,ENDORSEMENT_ID,                                                      
REMARKS,DWELLING_ENDORSEMENT_ID ,EDITION_DATE                                                                                                     
)                  
select              @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DWELLING_ID,ENDORSEMENT_ID,                  
REMARKS,DWELLING_ENDORSEMENT_ID   ,EDITION_DATE                                                                                                     
from APP_DWELLING_ENDORSEMENTS                                                                                       
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                              
--Pawan                                 
and DWELLING_ID in (Select DWELLING_ID from POL_DWELLINGS_INFO                                  
where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                  
                                                                                                      
 select @TEMP_ERROR_CODE = @@ERROR                          
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                         
                            
                            
                                                                                                 
-- 10. POL_HOME_OWNER_GEN_INFO     (UNDER WRITNG Questions)                                                                            
insert into POL_HOME_OWNER_GEN_INFO                                                                                                      
(                                        
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ANY_FARMING_BUSINESS_COND,DESC_BUSINESS,                                                                                                      
ANY_RESIDENCE_EMPLOYEE,DESC_RESIDENCE_EMPLOYEE,ANY_OTHER_RESI_OWNED,DESC_OTHER_RESIDENCE,ANY_OTH_INSU_COMP,                                                                                                      
DESC_OTHER_INSURANCE,HAS_INSU_TRANSFERED_AGENCY,DESC_INSU_TRANSFERED_AGENCY,ANY_COV_DECLINED_CANCELED,DESC_COV_DECLINED_CANCELED,                                                                                                 
ANIMALS_EXO_PETS_HISTORY,BREED,OTHER_DESCRIPTION,CONVICTION_DEGREE_IN_PAST,DESC_CONVICTION_DEGREE_IN_PAST,                                                 
ANY_RENOVATION,DESC_RENOVATION,TRAMPOLINE,DESC_TRAMPOLINE,LEAD_PAINT_HAZARD,DESC_LEAD_PAINT_HAZARD,RENTERS,                                                    
DESC_RENTERS,BUILD_UNDER_CON_GEN_CONT,REMARKS,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                                                                                      
MULTI_POLICY_DISC_APPLIED,NO_OF_PETS,IS_SWIMPOLL_HOTTUB,LAST_INSPECTED_DATE,IS_RENTED_IN_PART,IS_VACENT_OCCUPY,                                                           
IS_DWELLING_OWNED_BY_OTHER,IS_PROP_NEXT_COMMERICAL,DESC_PROPERTY,ARE_STAIRWAYS_PRESENT,DESC_STAIRWAYS,                                                                  
IS_OWNERS_DWELLING_CHANGED,DESC_OWNER,DESC_VACENT_OCCUPY,DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER,                          
ANY_HEATING_SOURCE,DESC_ANY_HEATING_SOURCE,NON_SMOKER_CREDIT,SWIMMING_POOL,SWIMMING_POOL_TYPE,location,ANY_FORMING,                                                                    
PREMISES,OF_ACRES,ISANY_HORSE,OF_ACRES_P,NO_HORSES,DOG_SURCHARGE,DESC_Location,DESC_FARMING_BUSINESS_COND,                           
DESC_IS_SWIMPOLL_HOTTUB,DESC_MULTI_POLICY_DISC_APPLIED,DESC_BUILD_UNDER_CON_GEN_CONT,                                      
APPROVED_FENCE,DIVING_BOARD,SLIDE , PROVIDE_HOME_DAY_CARE, MODULAR_MANUFACTURED_HOME,                                 
BUILT_ON_CONTINUOUS_FOUNDATION,YEARS_INSU,YEARS_INSU_WOL,                       
VALUED_CUSTOMER_DISCOUNT_OVERRIDE,  VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,PROPERTY_ON_MORE_THAN_DESC,              
PROPERTY_ON_MORE_THAN,DWELLING_MOBILE_HOME,DWELLING_MOBILE_HOME_DESC,PROPERTY_USED_WHOLE_PART,PROPERTY_USED_WHOLE_PART_DESC,ANY_PRIOR_LOSSES,ANY_PRIOR_LOSSES_DESC,  
BOAT_WITH_HOMEOWNER,  
--Added for Itrack Issue 6640 on 11 Dec 09  
NON_WEATHER_CLAIMS,  
WEATHER_CLAIMS  
             
                      
                       
)               
select                                                                                                      
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,ANY_FARMING_BUSINESS_COND,DESC_BUSINESS,ANY_RESIDENCE_EMPLOYEE,                                                                                           
DESC_RESIDENCE_EMPLOYEE,ANY_OTHER_RESI_OWNED,DESC_OTHER_RESIDENCE,ANY_OTH_INSU_COMP,DESC_OTHER_INSURANCE,                                                                                                      
HAS_INSU_TRANSFERED_AGENCY,DESC_INSU_TRANSFERED_AGENCY,ANY_COV_DECLINED_CANCELED,DESC_COV_DECLINED_CANCELED,                                                                       
ANIMALS_EXO_PETS_HISTORY,BREED,OTHER_DESCRIPTION,CONVICTION_DEGREE_IN_PAST,DESC_CONVICTION_DEGREE_IN_PAST,                                                            
ANY_RENOVATION,DESC_RENOVATION,TRAMPOLINE,DESC_TRAMPOLINE,LEAD_PAINT_HAZARD,DESC_LEAD_PAINT_HAZARD,                                                                                                      
RENTERS,DESC_RENTERS,BUILD_UNDER_CON_GEN_CONT,REMARKS,IS_ACTIVE,@CREATED_BY,getdate(),null,null,                                                                                        
MULTI_POLICY_DISC_APPLIED,NO_OF_PETS,IS_SWIMPOLL_HOTTUB,LAST_INSPECTED_DATE,IS_RENTED_IN_PART,IS_VACENT_OCCUPY,                                              
IS_DWELLING_OWNED_BY_OTHER,IS_PROP_NEXT_COMMERICAL,DESC_PROPERTY,ARE_STAIRWAYS_PRESENT,DESC_STAIRWAYS,                                       
IS_OWNERS_DWELLING_CHANGED,DESC_OWNER,DESC_VACENT_OCCUPY,DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER,                                        
ANY_HEATING_SOURCE,DESC_ANY_HEATING_SOURCE,NON_SMOKER_CREDIT,SWIMMING_POOL,SWIMMING_POOL_TYPE,location,ANY_FORMING,                                                                    
PREMISES,OF_ACRES,ISANY_HORSE,OF_ACRES_P,NO_HORSES,DOG_SURCHARGE,DESC_Location,DESC_FARMING_BUSINESS_COND,                                          
DESC_IS_SWIMPOLL_HOTTUB,DESC_MULTI_POLICY_DISC_APPLIED,DESC_BUILD_UNDER_CON_GEN_CONT,                                      
APPROVED_FENCE,DIVING_BOARD,SLIDE, PROVIDE_HOME_DAY_CARE, MODULAR_MANUFACTURED_HOME,                                 
BUILT_ON_CONTINUOUS_FOUNDATION,YEARS_INSU,YEARS_INSU_WOL,                            
VALUED_CUSTOMER_DISCOUNT_OVERRIDE,  VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,PROPERTY_ON_MORE_THAN_DESC,PROPERTY_ON_MORE_THAN,              
DWELLING_MOBILE_HOME,DWELLING_MOBILE_HOME_DESC,PROPERTY_USED_WHOLE_PART,PROPERTY_USED_WHOLE_PART_DESC,ANY_PRIOR_LOSSES,ANY_PRIOR_LOSSES_DESC,  
BOAT_WITH_HOMEOWNER,  
--Added for Itrack Issue 6640 on 11 Dec 09  
NON_WEATHER_CLAIMS,  
WEATHER_CLAIMS              
              
                               
from APP_HOME_OWNER_GEN_INFO                                                            
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                  
                                                                                         
 select @TEMP_ERROR_CODE = @@ERROR                                    
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                
                                                                       
 -------  --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710                                                                  
-- 13. POL_HOME_OWNER_RECREATIONAL_VEHICLES                                                                                                      
insert into POL_HOME_OWNER_RECREATIONAL_VEHICLES                                                 
(                                                                                              
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,REC_VEH_ID,COMPANY_ID_NUMBER,YEAR,MAKE,MODEL,SERIAL,STATE_REGISTERED,                                                              
MANUFACTURER_DESC,HORSE_POWER,DISPLACEMENT,REMARKS,USED_IN_RACE_SPEED,PRIOR_LOSSES,IS_UNIT_REG_IN_OTHER_STATE,                                                                                                      
RISK_DECL_BY_OTHER_COMP,DESC_RISK_DECL_BY_OTHER_COMP,VEHICLE_MODIFIED,ACTIVE,CREATED_BY,CREATED_DATETIME,                                                                                                  
MODIFIED_BY,LAST_UPDATED_DATETIME,VEHICLE_TYPE,INSURING_VALUE,DEDUCTIBLE ,UNIT_RENTED ,     
UNIT_OWNED_DEALERS,YOUTHFUL_OPERATOR_UNDER_25 , LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE                                                                               
)                                                 
select                                                                                                      
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,REC_VEH_ID,COMPANY_ID_NUMBER,YEAR,MAKE,MODEL,SERIAL,                                                      
STATE_REGISTERED,MANUFACTURER_DESC,HORSE_POWER,DISPLACEMENT,REMARKS,USED_IN_RACE_SPEED,PRIOR_LOSSES,                 
IS_UNIT_REG_IN_OTHER_STATE,RISK_DECL_BY_OTHER_COMP,DESC_RISK_DECL_BY_OTHER_COMP,VEHICLE_MODIFIED,                                                                                                      
ACTIVE,@CREATED_BY,getdate(),null,null,VEHICLE_TYPE,INSURING_VALUE,DEDUCTIBLE,UNIT_RENTED ,     
UNIT_OWNED_DEALERS,YOUTHFUL_OPERATOR_UNDER_25 , LIABILITY ,  MEDICAL_PAYMENTS , PHYSICAL_DAMAGE                                                                                                         
from APP_HOME_OWNER_RECREATIONAL_VEHICLES                                                                                                      
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                              
--Pawan                                                                                         
and Active='Y'                                                                                                      
                                                                                                      
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                           
                                
                                
--------------------------                                
-- POL_HOMEOWNER_REC_VEH_ADD_INT                                
                                
 SELECT Main.* INTO #POL_HOMEOWNER_REC_VEH_ADD_INT                         
FROM APP_HOMEOWNER_REC_VEH_ADD_INT Main                                
LEFT JOIN   APP_HOME_OWNER_RECREATIONAL_VEHICLES DET ON MAIN.CUSTOMER_ID = DET.CUSTOMER_ID                            
  AND MAIN.APP_ID = DET.APP_ID AND MAIN.APP_VERSION_ID = DET.APP_VERSION_ID                                 
  AND MAIN.REC_VEH_ID = DET.REC_VEH_ID                                
 WHERE  Main.CUSTOMER_ID = @CUSTOMER_ID AND Main.APP_ID = @APP_ID                  
  AND Main.APP_VERSION_ID = @APP_VERSION_ID AND Main.IS_ACTIVE = 'Y'                                
  AND DET.ACTIVE = 'Y'                                
  
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                               
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                                        
   UPDATE #POL_HOMEOWNER_REC_VEH_ADD_INT                                 
 SET APP_ID = @TEMP_POLICY_ID, APP_VERSION_ID = @TEMP_POLICY_VERSION_ID,                                
 CREATED_BY = @CREATED_BY,CREATED_DATETIME = Getdate(),                                
 LAST_UPDATED_DATETIME = NULL, MODIFIED_BY = NULL                                
                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                            
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
  
    INSERT INTO POL_HOMEOWNER_REC_VEH_ADD_INT SELECT * FROM #POL_HOMEOWNER_REC_VEH_ADD_INT        
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
 IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                           
    DROP TABLE #POL_HOMEOWNER_REC_VEH_ADD_INT                                
    SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM                                                          
                                
                                
--------------------------                                                                                         
                                                                  
IF (@PARAM1 != 6)  -- ADDED BY VJ      //When LOB is not rental                                                                                      
BEGIN                                       
                  
 --11. POL_HOME_OWNER_SCH_ITEMS_CVGS                                                                            
 insert into POL_HOME_OWNER_SCH_ITEMS_CVGS                                            
 (                                                                                                      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ITEM_ID,DETAILED_DESC,SN_DETAILS,AMOUNT_OF_INSURANCE,PREMIUM,                                                                                                      
 RATE,APPRAISAL,PURCHASE_APPRAISAL_DATE,BREAKAGE_COVERAGE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                                                                                    
 MODIFIED_BY,LAST_UPDATED_DATETIME,APPRAISAL_DESC,BREAKAGE_DESC,DEDUCTIBLE                                                                                                      
 )                                                                                                           
 select                                           
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,ITEM_ID,DETAILED_DESC,SN_DETAILS,AMOUNT_OF_INSURANCE,                                                                            
 PREMIUM,RATE,APPRAISAL,PURCHASE_APPRAISAL_DATE,BREAKAGE_COVERAGE,IS_ACTIVE,@CREATED_BY,getdate(),          
 null,null,APPRAISAL_DESC,BREAKAGE_DESC,DEDUCTIBLE                   
 from APP_HOME_OWNER_SCH_ITEMS_CVGS        
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE='Y'             
                                                                      
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                        
  
 -- 11A POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                  
                  
 insert into POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                  
 (                                      
 CUSTOMER_ID,                  
 POL_ID,                  
 POL_VERSION_ID,                  
 ITEM_ID,                  
 ITEM_DETAIL_ID,                  
 ITEM_NUMBER,                  
 ITEM_DESCRIPTION,                  
 ITEM_SERIAL_NUMBER,                  
 ITEM_INSURING_VALUE,                  
 ITEM_APPRAISAL_BILL,                  
 ITEM_PICTURE_ATTACHED,        
 IS_ACTIVE                                                                         
 )                                                                                                
 select                                                                           
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,                  
 ITEM_ID,                  
 ITEM_DETAIL_ID,     
 ITEM_NUMBER,                  
 ITEM_DESCRIPTION,                  
 ITEM_SERIAL_NUMBER,                  
 ITEM_INSURING_VALUE,                  
 ITEM_APPRAISAL_BILL,                  
 ITEM_PICTURE_ATTACHED,        
 IS_ACTIVE                                                                         
 from APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                  
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE='Y'                       
                                                                      
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                        
                  
                                                                                                       
 -- 12. POL_HOME_OWNER_PER_ART_GEN_INFO                                 
 insert into POL_HOME_OWNER_PER_ART_GEN_INFO                                                                                                      
 (                                                      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,PROPERTY_EXHIBITED,DESC_PROPERTY_EXHIBITED,DEDUCTIBLE_APPLY,                                                                         
 DESC_DEDUCTIBLE_APPLY,PROPERTY_USE_PROF_COMM,OTHER_INSU_WITH_COMPANY,DESC_INSU_WITH_COMPANY,LOSS_OCCURED_LAST_YEARS,                                     
 DESC_LOSS_OCCURED_LAST_YEARS,DECLINED_CANCELED_COVERAGE,ADD_RATING_COV_INFO,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                                                                                                      
 MODIFIED_BY,LAST_UPDATED_DATETIME,DESC_PROPERTY_USE_PROF_COMM                                                                                                      
 )                                                                
 select                                                                                                 
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,PROPERTY_EXHIBITED,DESC_PROPERTY_EXHIBITED,DEDUCTIBLE_APPLY,                                                                                                      
 DESC_DEDUCTIBLE_APPLY,PROPERTY_USE_PROF_COMM,OTHER_INSU_WITH_COMPANY,DESC_INSU_WITH_COMPANY,LOSS_OCCURED_LAST_YEARS,                           
 DESC_LOSS_OCCURED_LAST_YEARS,DECLINED_CANCELED_COVERAGE,ADD_RATING_COV_INFO,IS_ACTIVE,@CREATED_BY,getdate(),                                                                                                      
 null,null,DESC_PROPERTY_USE_PROF_COMM       
 from APP_HOME_OWNER_PER_ART_GEN_INFO                                                                                                      
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE='Y'                                                                                     
                                                                                                      
  select @TEMP_ERROR_CODE = @@ERROR                            
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM           
                                                                                            
                                                                       
 -- 14. POL_HOME_OWNER_SOLID_FUEL                                                                                                      
 insert into POL_HOME_OWNER_SOLID_FUEL                                                                 
 (                   
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,FUEL_ID,LOCATION_ID,SUB_LOC_ID,MANUFACTURER,BRAND_NAME,MODEL_NUMBER,                                              
 FUEL,STOVE_TYPE,HAVE_LABORATORY_LABEL,IS_UNIT,UNIT_OTHER_DESC,CONSTRUCTION,LOCATION,LOC_OTHER_DESC,                                                                
 YEAR_DEVICE_INSTALLED,WAS_PROF_INSTALL_DONE,INSTALL_INSPECTED_BY,INSTALL_OTHER_DESC,HEATING_USE,HEATING_SOURCE,                                          
 OTHER_DESC,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME, STOVE_INSTALLATION_CONFORM_SPECIFICATIONS                                                                                                      
 )                        
 select                                                          
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,FUEL_ID,LOCATION_ID,SUB_LOC_ID,MANUFACTURER,BRAND_NAME,MODEL_NUMBER,                                                         
 FUEL,STOVE_TYPE,HAVE_LABORATORY_LABEL,IS_UNIT,UNIT_OTHER_DESC,CONSTRUCTION,LOCATION,LOC_OTHER_DESC,                                  
 YEAR_DEVICE_INSTALLED,WAS_PROF_INSTALL_DONE,INSTALL_INSPECTED_BY,INSTALL_OTHER_DESC,HEATING_USE,HEATING_SOURCE,                                                                                                      
 OTHER_DESC,IS_ACTIVE,@CREATED_BY,getdate(),null,null, STOVE_INSTALLATION_CONFORM_SPECIFICATIONS                                
 from APP_HOME_OWNER_SOLID_FUEL                                                                         
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                          
--Pawan                                                                                
and IS_ACTIVE='Y'                                                                                                     
                                                                         
  select @TEMP_ERROR_CODE = @@ERROR                                                                                                            
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                               
                                                                              
 --15. POL_HOME_OWNER_FIRE_PROT_CLEAN                                                                                                      
 insert into POL_HOME_OWNER_FIRE_PROT_CLEAN         
 (                              
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,FUEL_ID,IS_SMOKE_DETECTOR,IS_PROTECTIVE_MAT_FLOOR,IS_PROTECTIVE_MAT_WALLS,                                                                                                      
 PROT_MAT_SPACED,STOVE_SMOKE_PIPE_CLEANED,STOVE_CLEANER,REMARKS                                      
 )                                                                                 
select                                                                                                 
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,FUEL_ID,IS_SMOKE_DETECTOR,IS_PROTECTIVE_MAT_FLOOR,IS_PROTECTIVE_MAT_WALLS,                                                 
 PROT_MAT_SPACED,STOVE_SMOKE_PIPE_CLEANED,STOVE_CLEANER,REMARKS                                                                                                 
 from APP_HOME_OWNER_FIRE_PROT_CLEAN                                                      
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                                   
 AND FUEL_ID IN (SELECT FUEL_ID FROM  POL_HOME_OWNER_SOLID_FUEL WHERE                                                                                   
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                          
                                                 
  select @TEMP_ERROR_CODE = @@ERROR             
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                         
                                 
 --16.                                                                                                      
 insert into POL_HOME_OWNER_CHIMNEY_STOVE                                                                                                      
 (                                          
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,FUEL_ID,IS_STOVE_VENTED,OTHER_DEVICES_ATTACHED,CONSTRUCT_OTHER_DESC,                                                                                                   
 IS_TILE_FLUE_LINING,IS_CHIMNEY_GROUND_UP,CHIMNEY_INST_AFTER_HOUSE_BLT,IS_CHIMNEY_COVERED,DIST_FROM_SMOKE_PIPE,                                                         
 THIMBLE_OR_MATERIAL,STOVE_PIPE_IS,DOES_SMOKE_PIPE_FIT,SMOKE_PIPE_WASTE_HEAT,STOVE_CONN_SECURE,SMOKE_PIPE_PASS,                                          
 SELECT_PASS,PASS_INCHES,CHIMNEY_CONSTRUCTION,IS_ACTIVE                                                                                                      
 )                                                                                                      
                                                                                    
 select                                                                                                      
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,FUEL_ID,IS_STOVE_VENTED,OTHER_DEVICES_ATTACHED,CONSTRUCT_OTHER_DESC,                                                                                            
 IS_TILE_FLUE_LINING,IS_CHIMNEY_GROUND_UP,CHIMNEY_INST_AFTER_HOUSE_BLT,IS_CHIMNEY_COVERED,DIST_FROM_SMOKE_PIPE,                                                                                                      
 THIMBLE_OR_MATERIAL,STOVE_PIPE_IS,DOES_SMOKE_PIPE_FIT,SMOKE_PIPE_WASTE_HEAT,STOVE_CONN_SECURE,SMOKE_PIPE_PASS,                                                               
 SELECT_PASS,PASS_INCHES,CHIMNEY_CONSTRUCTION,IS_ACTIVE                                                                              
 from APP_HOME_OWNER_CHIMNEY_STOVE                                                                                                      
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID   AND IS_ACTIVE='Y'                                                                                  
 AND FUEL_ID IN (SELECT FUEL_ID FROM  POL_HOME_OWNER_SOLID_FUEL WHERE                    
  CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                        
                                                                                                      
  select @TEMP_ERROR_CODE = @@ERROR                                                                             
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                
                                                                      
END                                                                                            
                      
      
IF (@PARAM1 != 6)  -- ADDED BY VJ                      
BEGIN                                              
declare @RESULT1 int          
declare @TRANCOUNT1 int              
BEGIN TRAN WATER                                                                  
 exec  @RESULT1=Proc_ConvertApplicationToPolicy_Watercraft @CUSTOMER_ID ,@APP_ID, @APP_VERSION_ID,                                                                                                
 @CREATED_BY ,NULL,NULL,NULL,'Home',@RESULT1 output,@CUSTOMER_ID,@TEMP_POLICY_ID,                                
  @TEMP_POLICY_VERSION_ID,@TEMP_POLICY_NUMBER                                                                                                    
SET @TRANCOUNT1=@@TRANCOUNT          
          
if (@RESULT1 <> 0)           
BEGIN          
COMMIT TRAN WATER          
END          
ELSE          
BEGIN          
ROLLBACK TRAN WATER          
          
goto PROBLEM            
END          
                                                                                                         
                                            
          
END                      
                      
/*-- commented by pravesh                                                                                                  
-- diary                                                                                                   
                                
declare @LISTTYPEID int--New Business =7                                                                           
declare @UNDERWRITER int -- 0                                                                                                  
                                                                     
select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                                                  
from Pol_customer_policy_list                                                                         
where Customer_ID=@CUSTOMER_ID and  POLICY_ID=@TEMP_POLICY_ID and   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                  
                                                                                                  
INSERT into TODOLIST                                                                      
 (                                
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,POLICYVERSION,                                       
  POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,                                                                                                  
  FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                
  FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                                                                                                    
 )                                                                                                     
values                                                  
 (                               
  null,getdate(),getdate(),7,@CUSTOMER_ID,@TEMP_POLICY_ID,                          
  @TEMP_POLICY_VERSION_ID,null,null,'New Application Submitted','Y',                                        
  null,'M',@CREATED_BY,@CREATED_BY,null,null,null,null,null,null,null,null,null,                                                    
  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID     
 )                                                                   
                                                                                                  
 select @TEMP_ERROR_CODE = @@ERROR                                                                                                    
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM   
*/                  
--Update Other version of application                      
                                           
  UPDATE    APP_LIST SET IS_ACTIVE='N',APP_STATUS='Unconfirmed'                 
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID <> @APP_VERSION_ID                                                            
                                                          
--Update save version of application                                                           
                                                                
 UPDATE APP_LIST set APP_STATUS = 'Complete'                                                                                                            
 where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                              
                                                                                                            
 select @TEMP_ERROR_CODE = @@ERROR                                                
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                   
                                               
                                                                                                  
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

