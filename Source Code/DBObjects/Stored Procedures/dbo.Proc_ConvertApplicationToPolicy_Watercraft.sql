IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_Watercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_Watercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
/* =======================================================================                                                                           
 Proc Name       : dbo.Proc_ConvertApplicationToPolicy_Watercraft                                                                                            
 Created by      : Ashwani                                                                                            
 Date            : 07 Nov. 2005                                                                                            
 Purpose         : Copy the Watercraft Application Data to Policy Tables.                                                                                                  
 Revison History :                                                                                                      
 Used In         : Wolverine                                                                          
                                                                          
 Modified By  : Vijay Arora                                                                          
 Modified Date  : 22-11-2005                                                                          
 Purpose  : Added the script to copy the data from table named APP_WATERCRAFT_ENGINE_INFO                                                                          
                   to table named POL_WATERCRAFT_ENGINE_INFO                                                                            
                                                                        
Modified By     : Vijay Arora                                                                            
Modified Date   : 07-12-2005                                                                        
Purpose    : Change the Display Version in Pol_Customer_Policy_List Table.                                                                        
                                                                      
Modified By     : Vijay Arora                                                                            
Modified Date   : 20-12-2005                                                                        
Purpose    : Make for check that child records will only copy when master record is active                                                           
                                                        
Modified By     : Deepak Batra                                                                           
Modified Date   : 23-01-2006                                                                      
Purpose         : Copy the newly added columns for some tables                                              
                                              
Modified By     : Shafi                                                                          
Modified Date   : 06-02-2006                                                                    
Purpose         : Update Status Of Application Of other Versions To Inactive ,Incomplete                                               
                                            
Modified By     : Vijay Arora                                            
Modified Date   : 08-02-2006                                                                    
Purpose         : Make check that if policy already created against application than does not ALTER  again.                                            
                            
Modified By     : Shafi                                         
Modified Date   : 20-03-2006                            
Purpose         : Add the Field of MULTI_POLICY_DISC_APPLIED ,MULTI_POLICY_DISC_APPLIED_PP_DESC      
                            
Modified By     : RPSINGH                        
Modified Date   : 15-06-2006                            
Purpose    : Add the Field of LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP,                             
    LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH                            
    in POL_WATERCRAFT_INFO table                              
                            
Modified By     : RPSINGH                                       
Modified Date   : 15-06-2006                            
Purpose        : Add the Field of EQUIPMENT_TYPE, OTHER_DESCRIPTION                            
    in table POL_WATERCRAFT_EQUIP_DETAILLS                            
Modified By     : Pravesh                  
Modified Date   : 18-10-2006                            
Purpose         : make MODifIED_BY,LasT_UPDATED_DATETIME null in POL_CUSTOMER_POLICY_LIST,                        
                  change Field VALUE TOUSERID, FROM @CREATED_BY TO @UNDERWRITER                        
Modified By     : Pravesh                                       
Modified Date   : 23-10-2006                            
Purpose         : make call a comman procedure for Comman data LIke POL_CUSTOMER_POLICY_LIST,POL_APPLICANT_LIST and TODOLIST                    
    
Modified By     : Asfa      
Modified Date   : 18-6-2007    
Purpose         : Added Trailer Ded Info in Watercraft:    
  
Modified By     : PKASANA  
Modified Date   : 13-9-2007    
Purpose         : Added ASSIGNED BOAT Info in watercraft (ASSIGNED OPERATOR FOR BOATS)  
                                       
                                                                                             
==========================================================================                                
Date     Review By          Comments                                                                                                      
========================================================================== */                                                                                            
--drop proc dbo.Proc_ConvertApplicationToPolicy_Watercraft                                                               
CREATE  proc dbo.Proc_ConvertApplicationToPolicy_Watercraft                                                                                                  
@CUSTOMER_ID int,                                                                                                  
@APP_ID int,                                                                                               
@APP_VERSION_ID smallint,                                                                                                  
@CREATED_BY int,                                                                                                  
@PARAM1 int = NULL,                                                                                                  
@PARAM2 int = NULL,                                                                                                  
@PARAM3 int = NULL,                                                                   
@CALLED_FROM NVARCHAR(30),                                                                                                
@RESULT int output,                                                
@temp_cust_id int=null,                                                          
@pol_id int=null,                                            
@pol_ver_id int=null,    
@pol_number nvarchar(75) = null                                                                                                  
as                                        
begin                                             
                    
                    
---ADDED BY PRAVESH                        
BEGIN TRAN                                                                           
                        
DECLARE @TEMP_POLICY_ID INT                                                                                                             
DECLARE @TEMP_ERROR_CODE INT                  
SET @TEMP_POLICY_ID=@pol_id                        
if(upper(@CALLED_FROM)<>'HOME')                                                                            
begin              
 EXEC     @RESULT=Proc_ConvertApplicationToPolicy_ALL  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,@PARAM1,@PARAM2,@PARAM3,@CALLED_FROM,@RESULT OUTPUT                          
 IF (@RESULT = -1)  GOTO PROBLEM                        
 SET @TEMP_POLICY_ID=@RESULT                        
end              
                     
              
                                                                                                        
            
DECLARE @TEMP_POLICY_VERSION_ID INT                  
--SET @TEMP_POLICY_ID=@RESULT                        
SELECT   @TEMP_POLICY_VERSION_ID = POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
--END HERE                        
/*                    
                    
                              
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
                                                          
if upper(@CALLED_FROM) ='HOME'                                                          
BEGIN                                                
set @CUSTOMER_ID=@temp_cust_id                                                          
set @TEMP_POLICY_ID=@pol_id                                                          
set @TEMP_POLICY_VERSION_ID=@pol_ver_id                                                          
END                                                          
                                            
                                            
                                            
--1. POL_CUSTOMER_POLICY_LIST                                                            
                              
if @CALLED_FROM = 'ANYWAY'                                                                
insert into POL_CUSTOMER_POLICY_LIST                                                                                                   
(                                                                                                  
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                                  
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                                      
 
  
   
  
  
    
     
    
    
    
     
 CREATED_DATETIME,       
MODifIED_BY,LasT_UPDATED_DATETIME,                        
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                               
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,    YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                       
     
        
          
            
             
                
                  
                    
                       
                        
                         
                        
                         
                          
                          
 POLICY_DISP_VERSION,IS_HOME_EMP                                                                         
 )                                                                                                        
select                                                                                                   
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                                                            
 APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                    
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),                        
--MODifIED_BY,LasT_UPDATED_DATETIME,                        
 NULL,NULL,                                         
 COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                             
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                        
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                       
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',IS_HOME_EMP                                                                                            
from APP_LIST                                                                                          
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                       
                                         
ELSE if upper(@CALLED_FROM) <> 'HOME'                                                            
insert into POL_CUSTOMER_POLICY_LIST                                                                                                   
(                                     
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                                                  
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                                      
  
  
  
   
  
  
    
     
     
     
      
        
         
            
             
 CREATED_DATETIME,                        
--MODifIED_BY,LasT_UPDATED_DATETIME,                        
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                                                                 
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                                       
 POLICY_DISP_VER  
SION,IS_HOME_EMP                                                                                                  
 )                                                                                  
select                                                      
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                          
 APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                             
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),                        
--MODifIED_BY,LasT_UPDATED_DATETIME,                                                                                             
 COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                                               
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'Suspended',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                                                                  
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0' ,IS_HOME_EMP                                                                                     
from APP_LIST                                             
where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                 
                                                                
                                              
select @TEMP_ERROR_CODE = @@ERROR                                                                
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                   
                                                
                                                
                                                
--ADDED BY VJ ON 01-02-2006                                                
IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                                
AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID)                                                
BEGIN                                                                      
                              
-- 2. POL_APPLICANT_LIST                                                           
IF upper(@CALLED_FROM) <>'HOME'                                                          
BEGIN                                                                                  
insert into POL_APPLICANT_LIST                                                                                                  
(                                                                                               
 POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_BY,CREATED_DATETIME,                                                                                                  
 IS_PRIMARY_APPLICANT                                                            
)                                                                                                  
select                                                                                 
 @TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@CUSTOMER_ID,A.APPLICANT_ID,@CREATED_BY,                                                                                                  
 GETDATE(),A.IS_PRIMARY_APPLICANT                                                                                                   
from APP_APPLICANT_LIST A, clt_APPLICANT_LIST  B,APP_LIST C                         
 WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                              
   and A.APPLICANT_ID = B.APPLICANT_ID and B.Is_Active='Y'       
   AND C.CUSTOMER_ID = @CUSTOMER_ID AND C.APP_ID = @APP_ID AND C.APP_VERSION_ID = @APP_VERSION_ID                                                                      
   AND C.IS_ACTIVE = 'Y'                                      
               
*/                                                                                        
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                    
-- END                                         
--3. POL_WATERCRAFT_INFO                                                       
insert into POL_WATERCRAFT_INFO                                                                               
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,BOAT_ID,BOAT_NO,BOAT_NAME,YEAR,MAKE,MODEL,HULL_ID_NO,STATE_REG,                                                                                       
HULL_MATERIAL,FUEL_TYPE,DATE_PURCHASED,LENGTH,MAX_SPEED,BERTH_LOC,WATERS_NAVIGATED,                                                                                          
TERRITORY,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,TYPE_OF_WATERCRAFT,INSURING_VALUE,                                                                                          
WATERCRAFT_HORSE_POWER,TWIN_SINGLE,INCHES,DESC_OTHER_WATERCRAFT,LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,                                                        
DUAL_OWNERSHIP,REMOVE_SAILBOAT,COV_TYPE_BASIS,PHOTO_ATTACHED,MARINE_SURVEY,DATE_MARINE_SURVEY,                            
LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH ,
--Added by Charles on 8-May-2009 for Itrack 5818
LOSSREPORT_ORDER, LOSSREPORT_DATETIME                                                                                               
)                                                                                                  
select                   
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,BOAT_ID,BOAT_NO,BOAT_NAME,YEAR,MAKE,MODEL,HULL_ID_NO,STATE_REG,                                                             
HULL_MATERIAL,FUEL_TYPE,DATE_PURCHASED,LENGTH,MAX_SPEED,BERTH_LOC,WATERS_NAVIGATED,                                     
TERRITORY,IS_ACTIVE,@CREATED_BY,getdate(),null,null,TYPE_OF_WATERCRAFT,INSURING_VALUE,WATERCRAFT_HORSE_POWER,TWIN_SINGLE,INCHES,                                                                                      
DESC_OTHER_WATERCRAFT,LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,DUAL_OWNERSHIP,REMOVE_SAILBOAT,COV_TYPE_BASIS,PHOTO_ATTACHED,MARINE_SURVEY,DATE_MARINE_SURVEY,                            
LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH ,
--Added by Charles on 8-May-2009 for Itrack 5818
LOSSREPORT_ORDER, LOSSREPORT_DATETIME                                                       
from APP_WATERCRAFT_INFO                                                                                          
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                               
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                           
                                                                          
                                                        
                  
--ADDED BY VJ ON 22-11-2005                                                      
INSERT INTO POL_WATERCRAFT_ENGINE_INFO                                                                          
 (CUSTOMER_ID,                                                                          
   POLICY_ID,                
 POLICY_VERSION_ID,                                                                          
 ENGINE_ID,                
 ENGINE_NO,                                               
 YEAR,                                                                          
 MAKE,                                                                          
 MODEL,                   
 SERIAL_NO,                                                                          
 HORSEPOWER,                                                                          
 ASSOCIATED_BOAT,                            
 FUEL_TYPE,                                                                          
 OTHER,                                                                          
 IS_ACTIVE,                                                                          
 CREATED_BY,                     
 CREATED_DATETIME,                                                                          
 INSURING_VALUE)                                                                          
SELECT                                
 @CUSTOMER_ID,                                                                          
 @TEMP_POLICY_ID,                                                 
 @TEMP_POLICY_VERSION_ID,                                
 ENGINE_ID,                                                                          
 ENGINE_NO,                                                                          
 YEAR,                                                                          
 MAKE,                                                                          
 MODEL,                                                                          
 SERIAL_NO,                                                         
 HORSEPOWER,                         
 ASSOCIATED_BOAT,                            
 FUEL_TYPE,                                                                          
 OTHER,                                                                      
 IS_ACTIVE,                                                   
 CREATED_BY,                                                                          
 GETDATE(),                                                                          
 INSURING_VALUE                                          
FROM APP_WATERCRAFT_ENGINE_INFO                                                          
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                       
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                    
                                                                          
--ENDED BY VJ ON 22-11-2005                                                                          
                                                                
                                                                                                
                                       
--4. POL_WATERCRAFT_COVERAGE_INFO                                                                                          
insert into POL_WATERCRAFT_COVERAGE_INFO        
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,BOAT_ID,COVERAGE_ID,WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_ACTIVE,CREATED_BY,                   
CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COVERAGE_CODE_ID,LIMIT_1,DEDUCTIBLE_1,LIMIT_OVERRIDE,LIMIT_1_TYPE,                                                                                          
LIMIT_2,LIMIT_2_TYPE,DEDUCTIBLE_2,DEDUCTIBLE_2_TYPE,DEDUCTIBLE_1_TYPE,IS_SYSTEM_COVERAGE,DEDUCT_OVERRIDE,                                                                                          
LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,SIGNATURE_OBTAINED,                                  
LIMIT_ID, DEDUC_ID                                                                                          
)                                                                                                  
select                                               
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,BOAT_ID,COVERAGE_ID,WRITTEN_PREMIUM,FULL_TERM_PREMIUM,IS_ACTIVE,                                                                                          
@CREATED_BY,getdate(),null,null,coverage_code_id,Limit_1,Deductible_1,Limit_override,Limit_1_type,Limit_2,                                                                                          
Limit_2_type,Deductible_2,deductible_2_type,deductible_1_type,Is_system_coverage,Deduct_override,                                                                     
LIMIT1_AMOUNT_TEXT,LIMIT2_AMOUNT_TEXT,DEDUCTIBLE1_AMOUNT_TEXT,DEDUCTIBLE2_AMOUNT_TEXT,SIGNATURE_OBTAINED,                                  
LIMIT_ID, DEDUC_ID                                                                                              
from APP_WATERCRAFT_COVERAGE_INFO                                                                                          
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID --AND IS_ACTIVE = 'Y'                                                                        
AND BOAT_ID IN (SELECT BOAT_ID FROM POL_WATERCRAFT_INFO                                                                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE = 'Y')                                                                                                        
                                                                                 
select @TEMP_ERROR_CODE = @@ERROR                                                                                       
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                      
                                           
-- 5. POL_WATERCRAFT_ENDORSEMENTS                                            
insert into POL_WATERCRAFT_ENDORSEMENTS                                                                                   
(                             
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,BOAT_ID,ENDORSEMENT_ID,REMARKS,VEHICLE_ENDORSEMENT_ID,EDITION_DATE      
                                                                            
)                                                                     
select                                                   
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,BOAT_ID,ENDORSEMENT_ID,REMARKS,VEHICLE_ENDORSEMENT_ID ,EDITION_DATE                                                                                       
from APP_WATERCRAFT_ENDORSEMENTS                                                                                          
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                       
AND BOAT_ID IN (SELECT BOAT_ID FROM POL_WATERCRAFT_INFO                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE = 'Y')                      
                                                          
select @TEMP_ERROR_CODE = @@ERROR                                              
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM     
                         
-- 6 POL_WATERCRAFT_COV_ADD_INT                                                                                          
                            
insert into POL_WATERCRAFT_COV_ADD_INT                                                                                                  
(                       
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,HOLDER_ID,BOAT_ID,MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,IS_ACTIVE,                                             
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,ADD_INT_ID,HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,                                                                                          
HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP                                                                                          
)                                                                                     
select                                                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,HOLDER_ID,BOAT_ID,MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,                                                                                          
IS_ACTIVE,@CREATED_BY,getdate(),null,null,ADD_INT_ID,HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,                                                                                          
HOLDER_STATE,HOLDER_ZIP                                                                            
from APP_WATERCRAFT_COV_ADD_INT                                                                                                  
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                        
AND BOAT_ID IN (SELECT BOAT_ID FROM POL_WATERCRAFT_INFO                                                                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE = 'Y')                                                                               
          
                                                        
select @TEMP_ERROR_CODE = @@ERROR                                                                        
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                  
                                                         
--7. POL_WATERCRAFT_DRIVER_DETAILS                                      
insert into POL_WATERCRAFT_DRIVER_DETAILS                                                                              
(                                            
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,                                                                                          
DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,                                  
DRIVER_LIC_STATE,DRIVER_COST_GAURAD_AUX,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                                                    
EXPERIENCE_CREDIT,VEHICLE_ID,PERCENT_DRIVEN,APP_VEHICLE_PRIN_OCC_ID,YEARS_LICENSED,WAT_SAFETY_COURSE,CERT_COAST_GUARD,          
REC_VEH_ID,APP_REC_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,  
MVR_CLASS,MVR_LIC_CLASS,MVR_LIC_RESTR,MVR_DRIV_LIC_APPL,DRIVER_DRIV_TYPE          
)   
select                            
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,DRIVER_ADD1,                                                                                          
DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,                                   
DRIVER_COST_GAURAD_AUX,IS_ACTIVE,@CREATED_BY,getdate(),null,null,EXPERIENCE_CREDIT,Vehicle_ID,Percent_Driven,APP_VEHICLE_PRIN_OCC_ID,YEARS_LICENSED,WAT_SAFETY_COURSE,CERT_COAST_GUARD,          
REC_VEH_ID,APP_REC_VEHICLE_PRIN_OCC_ID,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,MARITAL_STATUS,  
MVR_CLASS,MVR_LIC_CLASS,MVR_LIC_RESTR,MVR_DRIV_LIC_APPL,DRIVER_DRIV_TYPE            
from APP_WATERCRAFT_DRIVER_DETAILS                                                  
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                         
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                 
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                                 
                                                                                          
-- 8 POL_WATERCRAFT_MVR_INFORMATION                                                                                          
insert into POL_WATERCRAFT_MVR_INFORMATION                                                      
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,APP_WATER_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                                                                                      
VIOLATION_ID,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,  
OCCURENCE_DATE,DETAILS,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS                                                                                          
)                                                                                                  
select                                                   
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,DRIVER_ID,APP_WATER_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,VIOLATION_ID,                                                                                          
IS_ACTIVE,@CREATED_BY,getdate(),null,null,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,  
OCCURENCE_DATE,DETAILS,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS                                                                                           
from APP_WATER_MVR_INFORMATION                                                                                                  
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                      
AND DRIVER_ID IN (SELECT DRIVER_ID FROM POL_WATERCRAFT_DRIVER_DETAILS                                                    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE = 'Y')                                                                        
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                   
                                                                                          
-- 9 POL_WATERCRAFT_TRAILER_INFO          
insert into POL_WATERCRAFT_TRAILER_INFO                         
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,TRAILER_ID,TRAILER_NO,YEAR,MANUFACTURER,MODEL,SERIAL_NO,ASSOCIATED_BOAT,IS_ACTIVE,                                                                  
CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,INSURED_VALUE,TRAILER_TYPE,    
TRAILER_DED,    
TRAILER_DED_ID,    
TRAILER_DED_AMOUNT_TEXT    
                                                 
)                                   
select                                                            
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,TRAILER_ID,TRAILER_NO,YEAR,MANUFACTURER,MODEL,SERIAL_NO,ASSOCIATED_BOAT,IS_ACTIVE,                                                         
@CREATED_BY,getdate(),null,null,INSURED_VALUE,TRAILER_TYPE,    
TRAILER_DED,    
TRAILER_DED_ID,    
TRAILER_DED_AMOUNT_TEXT                                                                            
from APP_WATERCRAFT_TRAILER_INFO                                                                                                  
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                        
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                              
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                          
                                                                                          
-- 10 POL_WATERCRAFT_TRAILER_ADD_INT                                                                                          
insert into POL_WATERCRAFT_TRAILER_ADD_INT                                                                                                  
(                                                                        
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,HOLDER_ID,TRAILER_ID,MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,                                                                                          
IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,ADD_INT_ID,HOLDER_NAME,HOLDER_ADD1,                                         
HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP                                                                                          
)                                                                   
select                                                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,HOLDER_ID,TRAILER_ID,MEMO,NATURE_OF_INTEREST,RANK,LOAN_REF_NUMBER,IS_ACTIVE,                                                                                          
@CREATED_BY,getdate(),null,null,ADD_INT_ID,HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,                                                                                          
HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP                                                                                          
from APP_WATERCRAFT_TRAILER_ADD_INT                                                                                                  
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                      
AND TRAILER_ID IN (SELECT TRAILER_ID FROM POL_WATERCRAFT_TRAILER_INFO                                                                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE = 'Y')                                                                        
                                      
                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                            
                                       
-- 11 POL_WATERCRAFT_EQUIP_DETAILLS                                         
                                                                                          
insert into POL_WATERCRAFT_EQUIP_DETAILLS                                                                                              
(                                                 
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,EQUIP_ID,EQUIP_NO,EQUIP_TYPE,SHIP_TO_SHORE,YEAR,MAKE,MODEL,SERIAL_NO,                                                                 
ASSOCIATED_BOAT,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,INSURED_VALUE,EQUIP_AMOUNT,                         
EQUIPMENT_TYPE, OTHER_DESCRIPTION                            
                    
)                                                                                                  
select                                                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,EQUIP_ID,EQUIP_NO,EQUIP_TYPE,SHIP_TO_SHORE,YEAR,MAKE,MODEL,                                               
SERIAL_NO,ASSOCIATED_BOAT,IS_ACTIVE,@CREATED_BY,getdate(),null,null,INSURED_VALUE,EQUIP_AMOUNT,EQUIPMENT_TYPE, OTHER_DESCRIPTION                                                              
                                                                                          
from APP_WATERCRAFT_EQUIP_DETAILLS                                                                                              
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                           
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                 
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                                         
                                    
                   
-- 12 POL_WATERCRAFT_GEN_INFO                                                                                          
if(upper(@CALLED_FROM)<>'HOME')     
begin    
insert into POL_WATERCRAFT_GEN_INFO                                                                                                  
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,HAS_CURR_ADD_THREE_YEARS,PHY_MENTL_CHALLENGED,DRIVER_SUS_REVOKED,IS_CONVICTED_ACCIDENT,                                                        
ANY_OTH_INSU_COMP,OTHER_POLICY_NUMBER_LIST,ANY_LOSS_THREE_YEARS,COVERAGE_DECLINED,DEGREE_CONVICTION,IS_CREDIT,CREDIT_DETAILS,                                                                                          
IS_RENTED_OTHERS,IS_REGISTERED_OTHERS,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                                                                          
HAS_CURR_ADD_THREE_YEARS_DESC,PHY_MENTL_CHALLENGED_DESC,DRIVER_SUS_REVOKED_DESC,IS_CONVICTED_ACCIDENT_DESC,                                                       
ANY_LOSS_THREE_YEARS_DESC,COVERAGE_DECLINED_DESC,DEGREE_CONVICTION_DESC,IS_RENTED_OTHERS_DESC,IS_REGISTERED_OTHERS_DESC,                                                                          
DRINK_DRUG_VOILATION,MINOR_VIOLATION,PARTICIPATE_RACE,CARRY_PASSENGER_FOR_CHARGE,TOT_YRS_OPERATORS_EXP,PARTICIPATE_RACE_DESC,                                                                                          
--CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT                                                                                          
CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT,IS_BOAT_COOWNED,    
--added by shafi 20-03-2006                            
MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,ANY_BOAT_AMPHIBIOUS,ANY_BOAT_AMPHIBIOUS_DESC,ANY_BOAT_RESIDENCE,ANY_BOAT_RESIDENCE_DESC,  
IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC  
                                   
)                                                                             
select                                                                                          
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,HAS_CURR_ADD_THREE_YEARS,PHY_MENTL_CHALLENGED,DRIVER_SUS_REVOKED,IS_CONVICTED_ACCIDENT,                        
ANY_OTH_INSU_COMP,OTHER_POLICY_NUMBER_LIST,ANY_LOSS_THREE_YEARS,COVERAGE_DECLINED,DEGREE_CONVICTION,IS_CREDIT,CREDIT_DETAILS,                        
IS_RENTED_OTHERS,IS_REGISTERED_OTHERS,IS_ACTIVE,@CREATED_BY,getdate(),null,null,                        
HAS_CURR_ADD_THREE_YEARS_DESC,PHY_MENTL_CHALLENGED_DESC,DRIVER_SUS_REVOKED_DESC,IS_CONVICTED_ACCIDENT_DESC,                                                                  
ANY_LOSS_THREE_YEARS_DESC,COVERAGE_DECLINED_DESC,DEGREE_CONVICTION_DESC,IS_RENTED_OTHERS_DESC,IS_REGISTERED_OTHERS_DESC,                                          
DRINK_DRUG_VOILATION,MINOR_VIOLATION,PARTICIPATE_RACE,CARRY_PASSENGER_FOR_CHARGE,TOT_YRS_OPERATORS_EXP,PARTICIPATE_RACE_DESC,                                                           
--CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT                                                                                          
CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT,IS_BOAT_COOWNED,                            
MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,ANY_BOAT_AMPHIBIOUS,ANY_BOAT_AMPHIBIOUS_DESC,ANY_BOAT_RESIDENCE,ANY_BOAT_RESIDENCE_DESC,  
IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC  
                                   
                        
from APP_WATERCRAFT_GEN_INFO                                                                               
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                         
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                        
                                                                               
end    
else    
begin    
insert into POL_WATERCRAFT_GEN_INFO                                                                                                  
(                                                                                          
CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,HAS_CURR_ADD_THREE_YEARS,PHY_MENTL_CHALLENGED,DRIVER_SUS_REVOKED,IS_CONVICTED_ACCIDENT,                                                        
ANY_OTH_INSU_COMP,OTHER_POLICY_NUMBER_LIST,ANY_LOSS_THREE_YEARS,COVERAGE_DECLINED,DEGREE_CONVICTION,IS_CREDIT,CREDIT_DETAILS,                                                                                          
IS_RENTED_OTHERS,IS_REGISTERED_OTHERS,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,                                                                                          
HAS_CURR_ADD_THREE_YEARS_DESC,PHY_MENTL_CHALLENGED_DESC,DRIVER_SUS_REVOKED_DESC,IS_CONVICTED_ACCIDENT_DESC,                                                       
ANY_LOSS_THREE_YEARS_DESC,COVERAGE_DECLINED_DESC,DEGREE_CONVICTION_DESC,IS_RENTED_OTHERS_DESC,IS_REGISTERED_OTHERS_DESC,                                                                          
DRINK_DRUG_VOILATION,MINOR_VIOLATION,PARTICIPATE_RACE,CARRY_PASSENGER_FOR_CHARGE,TOT_YRS_OPERATORS_EXP,PARTICIPATE_RACE_DESC,                                                                                          
--CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT                                                                                          
CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT,IS_BOAT_COOWNED,       
--added by shafi 20-03-2006                            
MULTI_POLICY_DISC_APPLIED,MULTI_POLICY_DISC_APPLIED_PP_DESC,ANY_BOAT_AMPHIBIOUS,ANY_BOAT_AMPHIBIOUS_DESC,ANY_BOAT_RESIDENCE,ANY_BOAT_RESIDENCE_DESC,  
IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC  
)                                                                             
select                                                                     
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,HAS_CURR_ADD_THREE_YEARS,PHY_MENTL_CHALLENGED,DRIVER_SUS_REVOKED,IS_CONVICTED_ACCIDENT,                        
ANY_OTH_INSU_COMP,OTHER_POLICY_NUMBER_LIST,ANY_LOSS_THREE_YEARS,COVERAGE_DECLINED,DEGREE_CONVICTION,IS_CREDIT,CREDIT_DETAILS,                        
IS_RENTED_OTHERS,IS_REGISTERED_OTHERS,IS_ACTIVE,@CREATED_BY,getdate(),null,null,                        
HAS_CURR_ADD_THREE_YEARS_DESC,PHY_MENTL_CHALLENGED_DESC,DRIVER_SUS_REVOKED_DESC,IS_CONVICTED_ACCIDENT_DESC,                                                                  
ANY_LOSS_THREE_YEARS_DESC,COVERAGE_DECLINED_DESC,DEGREE_CONVICTION_DESC,IS_RENTED_OTHERS_DESC,IS_REGISTERED_OTHERS_DESC,                                          
DRINK_DRUG_VOILATION,MINOR_VIOLATION,PARTICIPATE_RACE,CARRY_PASSENGER_FOR_CHARGE,TOT_YRS_OPERATORS_EXP,PARTICIPATE_RACE_DESC,                                                           
--CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT                                                                                          
CARRY_PASSENGER_FOR_CHARGE_DESC,IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC,IS_BOAT_COOWNED_DESC,BOAT_HOME_DISCOUNT,IS_BOAT_COOWNED,                            
MULTI_POLICY_DISC_APPLIED,@pol_number,ANY_BOAT_AMPHIBIOUS,ANY_BOAT_AMPHIBIOUS_DESC,ANY_BOAT_RESIDENCE,ANY_BOAT_RESIDENCE_DESC,  
IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC  
                        
from APP_WATERCRAFT_GEN_INFO                                                                               
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                                         
                                                                                          
select @TEMP_ERROR_CODE = @@ERROR                                                                                                  
if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                        
         
end    
  
--//Added PKASANA  
INSERT INTO POL_OPERATOR_ASSIGNED_BOAT                                         
(                                                                                                    
CUSTOMER_ID,                            
POLICY_ID,                            
POLICY_VERSION_ID,                            
DRIVER_ID,                            
BOAT_ID,                            
APP_VEHICLE_PRIN_OCC_ID                            
                                       
)                                                                                                    
SELECT                                                                                                     
 @CUSTOMER_ID,                              
 @TEMP_POLICY_ID,     
 @TEMP_POLICY_VERSION_ID,                         
 DRIVER_ID,                            
 BOAT_ID,                            
 APP_VEHICLE_PRIN_OCC_ID                            
                                                   
 FROM APP_OPERATOR_ASSIGNED_BOAT                                                                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                                                                                                                                
 and DRIVER_ID in (Select DRIVER_ID from POL_WATERCRAFT_DRIVER_DETAILS                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@TEMP_POLICY_ID AND POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID AND IS_ACTIVE='Y')                                                                                                    
                                                  
 SELECT @TEMP_ERROR_CODE = @@ERROR                                                                                                    
    IF (@TEMP_ERROR_CODE <> 0) GOTO PROBLEM    
                                                                                    
--Commented by pravesh                                                                                    
/*                    
-- diary                           
if @CALLED_FROM <> 'Home'                                                          
BEGIN                                                             
                                                                                       
declare @LISTTYPEID int--New Business =7                                                                                        
declare @UNDERWRITER int -- 0                                                                                        
                                                                                        
select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                                        
from Pol_customer_policy_list                                                                                        
where Customer_ID=@CUSTOMER_ID and  POLICY_ID=@TEMP_POLICY_ID and   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                                                                 
  
  
  
  
    
    
    
    
    
INSERT into TODOLIST                                                                                          
 (                                              
  RECBYSYSTEM,    RECDATE,    FOLLOWUPDATE,    LISTTYPEID,    POLICYCLIENTID,    POLICYID,    POLICYVERSION,                                                                                          
  POLICYCARRIERID,    POLICYBROKERID,    SUBJECTLINE,    LISTOPEN,    SYSTEMFOLLOWUPID,    PRIORITY,                
  TOUSERID,    FROMUSERID, STARTTIME,    ENDTIME,    NOTE,    PROPOSALVERSION,    QUOTEID,    CLAIMID,             
  CLAIMMOVEMENTID,    TOENTITYID,    FROMENTITYID,    CUSTOMER_ID,    APP_ID,    APP_VERSION_ID,    POLICY_ID,                                                                                          
  POLICY_VERSION_ID                                                                                          
 )                                                                                           
                                                                                        
values                                                                
 (                  
  null,    getdate(),   getdate(),    7,    @CUSTOMER_ID,    @TEMP_POLICY_ID,                                                       
  @TEMP_POLICY_VERSION_ID,    null,    null,'New Application Submitted',   'Y' ,                                
  null,    'M',   @UNDERWRITER,    @CREATED_BY,    null,    null,    null,                                                                                          
                        
  null,    null,    null,    null,    null,    null,                                                                           
  @CUSTOMER_ID,    @APP_ID,   @APP_VERSION_ID,    @TEMP_POLICY_ID,    @TEMP_POLICY_VERSION_ID                               
 )                                         
                                               
                                                                                    
 select @TEMP_ERROR_CODE = @@ERROR                                                                
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                        
 */                                                                           
                                                                                        
                                                                                          
--update APP_LIST set IS_ACTIVE = 'N'                                                  
--where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID != @APP_VERSION_ID                                                                              
--Update status                                              
  UPDATE    APP_LIST SET IS_ACTIVE='N',APP_STATUS='Unconfirmed'                                              
  WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID <> @APP_VERSION_ID                                  
                                                  
update APP_LIST set APP_STATUS = 'Complete'                                
where CUSTOMER_ID=@CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                                   
                                                                                                  
select @TEMP_ERROR_CODE = @@ERROR                                                             
 if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                              
--END                                                                                              
                                                              
 commit tran                                                                                                   
 set @RESULT = ISNULL(@TEMP_POLICY_ID,0)                                              
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
   commit tran                                                                                               
   set @RESULT = ISNULL(@TEMP_POLICY_ID,0)                                   
   return @RESULT                                                               
  end                                            
                                            
END                
        
      
    
    
    
    
    
  
    
    
    
  
  
  
  
  
  
  
  
  
GO

