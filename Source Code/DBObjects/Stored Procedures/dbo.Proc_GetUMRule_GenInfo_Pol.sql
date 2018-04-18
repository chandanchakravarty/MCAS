IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_GenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_GenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*======================================================================================================      
Proc Name                : Dbo.Proc_GetUMRule_GenInfo_Pol       
Created by               : Ashwani                                                                                                  
Date                     : 12 Oct,2006                                                  
Purpose                  : To get the Underwriting Info for UM rules                                                  
Revison History          :                                                                                                  
Used In                  : Wolverine                                                                                                  

Reviewed By	:	Anurag Verma
Reviewed On	:	10-07-2007
======================================================================================================      
Date     Review By          Comments                                                                                                  
=====  ==============   =============================================================================*/      
-- drop proc Proc_GetUMRule_GenInfo_Pol    
CREATE proc dbo.Proc_GetUMRule_GenInfo_Pol                                                 
(                                                                                                  
 @CUSTOMERID    int,                                                                                                  
 @POLICYID    int,                                                                                                  
 @POLICYVERSIONID   int,                                                  
 @DESC varchar(10)                                                                                    
)                                                                                                  
as                                                                                                      
begin                                                     
 -- Rules / Mandatory Data                                                  
declare @ANY_AIRCRAFT_OWNED_LEASED nchar(1)      
declare @ANY_AIRCRAFT_OWNED_LEASED_DESC nvarchar(300)      
declare @BUSINESS_PROF_ACTIVITY nchar(1)      
declare @BUSINESS_PROF_ACTIVITY_DESC nvarchar(300)      
declare @ANY_COVERAGE_DECLINED nchar(1)      
declare @ANY_COVERAGE_DECLINED_DESC nvarchar(300)      
declare @ANY_FULL_TIME_EMPLOYEE nchar(1)      
declare @ANY_FULL_TIME_EMPLOYEE_DESC nvarchar(300)      
declare @PENDING_LITIGATIONS nchar(1)      
declare @PENDING_LITIGATIONS_DESC nvarchar(300)      
declare @REAL_STATE_VEH_OWNED_HIRED nchar(1)      
declare @REAL_STATE_VEH_OWNED_HIRED_DESC nvarchar(300)      
declare @REAL_STATE_VEHICLE_USED nchar(1)      
declare @REAL_STATE_VEHICLE_USED_DESC nvarchar(300)      
declare @AUTO_CYCL_TRUCKS  nchar(1)      
declare @AUTO_CYCL_TRUCKS_DESC nvarchar(300)      
declare @HOME_RENT_DWELL  nchar(1)      
declare @HOME_RENT_DWELL_DESC nvarchar(300)      
declare @RECR_VEH  nchar(1)      
declare @RECR_VEH_DESC nvarchar(300)      
declare @WAT_DWELL  nchar(1)      
declare @WAT_DWELL_DESC nvarchar(300)      
declare @REDUCED_LIMIT_OF_LIBLITY  nchar(1)      
declare @REDUCED_LIMIT_OF_LIBLITY_DESC nvarchar(300)      
declare @ENGAGED_IN_FARMING  nchar(1)      
declare @ENGAGED_IN_FARMING_DESC nvarchar(300)      
declare @IS_RECORD_EXISTS char        
-- Mandatory Only       
declare @ANY_OPERATOR_CON_TRAFFIC nchar(1)      
declare @ANY_OPERATOR_CON_TRAFFIC_DESC nvarchar(75)      
      
declare @ANY_OPERATOR_IMPIRED nchar(1)      
declare @ANY_OPERATOR_IMPIRED_DESC nvarchar(75)      
      
declare @ANY_SWIMMING_POOL nchar(1)      
declare @ANY_SWIMMING_POOL_DESC nvarchar(75)      
      
declare @HOLD_NON_COMP_POSITION nchar(1)      
declare @HOLD_NON_COMP_POSITION_DESC nvarchar(75)      
      
declare @NON_OWNED_PROPERTY_CARE nchar(1)      
declare @NON_OWNED_PROPERTY_CARE_DESC nvarchar(75)      
      
declare @ANIMALS_EXOTIC_PETS nchar(1)      
declare @ANIMALS_EXOTIC_PETS_DESC nvarchar(75)      
      
declare @INSU_TRANSFERED_IN_AGENCY nchar(1)      
declare @INSU_TRANSFERED_IN_AGENCY_DESC nvarchar(75)      
      
declare @IS_TEMPOLINE nchar(1)      
declare @IS_TEMPOLINE_DESC nvarchar(75)      
      
declare @HAVE_NON_OWNED_AUTO_POL nchar(1)      
declare @HAVE_NON_OWNED_AUTO_POL_DESC nvarchar(75)      
      
declare @INS_DOMICILED_OUTSIDE nchar(1)      
declare @INS_DOMICILED_OUTSIDE_DESC nvarchar(75)      
      
declare @HOME_DAY_CARE nchar(1)      
declare @HOME_DAY_CARE_DESC nvarchar(75)      
      
declare @APPLI_UNDERSTAND_LIABILITY_EXCLUDED nchar(1)      
declare @APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC nvarchar(75)      
  
DECLARE @FAMILIES nvarchar(10)
DECLARE @intFAMILIES  int 
DECLARE @RENTAL_DWELLINGS_UNIT NVARCHAR(10)       
-- Temp var                                             
declare @TEMP char      
set @TEMP = 'N'                                   
IF EXISTS (SELECT CUSTOMER_ID FROM POL_UMBRELLA_GEN_INFO                                      
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)                                                  
BEGIN                                     
 SET @IS_RECORD_EXISTS='N'  
 SELECT       
 @ANY_AIRCRAFT_OWNED_LEASED=isnull(ANY_AIRCRAFT_OWNED_LEASED,''),@ANY_AIRCRAFT_OWNED_LEASED_DESC = isnull(ANY_AIRCRAFT_OWNED_LEASED_DESC,''),      
 @BUSINESS_PROF_ACTIVITY = isnull(BUSINESS_PROF_ACTIVITY,''),@BUSINESS_PROF_ACTIVITY_DESC= isnull(BUSINESS_PROF_ACTIVITY_DESC,''),      
 @ANY_COVERAGE_DECLINED = isnull(ANY_COVERAGE_DECLINED,'') ,@ANY_COVERAGE_DECLINED_DESC = isnull(ANY_COVERAGE_DECLINED_DESC,''),      
 @ANY_FULL_TIME_EMPLOYEE = isnull(ANY_FULL_TIME_EMPLOYEE,''),@ANY_FULL_TIME_EMPLOYEE_DESC= isnull(ANY_FULL_TIME_EMPLOYEE_DESC,''),      
 @PENDING_LITIGATIONS =isnull(PENDING_LITIGATIONS,''),@PENDING_LITIGATIONS_DESC=isnull(PENDING_LITIGATIONS_DESC,''),      
 @REAL_STATE_VEH_OWNED_HIRED=isnull(REAL_STATE_VEH_OWNED_HIRED,''),@REAL_STATE_VEH_OWNED_HIRED_DESC =isnull(REAL_STATE_VEH_OWNED_HIRED_DESC,''),      
 @REAL_STATE_VEHICLE_USED = isnull(REAL_STATE_VEHICLE_USED,''),@REAL_STATE_VEHICLE_USED_DESC = isnull(REAL_STATE_VEHICLE_USED_DESC,''),      
 @AUTO_CYCL_TRUCKS=isnull(AUTO_CYCL_TRUCKS,'')  ,@AUTO_CYCL_TRUCKS_DESC = isnull(AUTO_CYCL_TRUCKS_DESC,''),      
 @HOME_RENT_DWELL=isnull(HOME_RENT_DWELL,''),@HOME_RENT_DWELL_DESC=isnull(HOME_RENT_DWELL_DESC,''),      
 @RECR_VEH=isnull(RECR_VEH,''),@RECR_VEH_DESC=isnull(RECR_VEH_DESC,''),@WAT_DWELL=isnull(WAT_DWELL,''),@WAT_DWELL_DESC=isnull(WAT_DWELL_DESC,''),      
 @REDUCED_LIMIT_OF_LIBLITY=isnull(REDUCED_LIMIT_OF_LIBLITY,''),@REDUCED_LIMIT_OF_LIBLITY_DESC=isnull(REDUCED_LIMIT_OF_LIBLITY_DESC,''),      
 @ENGAGED_IN_FARMING=isnull(ENGAGED_IN_FARMING,''),@ENGAGED_IN_FARMING_DESC =isnull(ENGAGED_IN_FARMING_DESC,''),      
 @ANY_OPERATOR_CON_TRAFFIC=isnull(ANY_OPERATOR_CON_TRAFFIC,''),@ANY_OPERATOR_CON_TRAFFIC_DESC = isnull(ANY_OPERATOR_CON_TRAFFIC_DESC,''),      
 @ANY_OPERATOR_IMPIRED=isnull(ANY_OPERATOR_IMPIRED,''),@ANY_OPERATOR_IMPIRED_DESC = isnull(ANY_OPERATOR_IMPIRED_DESC,''),      
 @ANY_SWIMMING_POOL=isnull(ANY_SWIMMING_POOL,''),@ANY_SWIMMING_POOL_DESC = isnull(ANY_SWIMMING_POOL_DESC,''),      
 @HOLD_NON_COMP_POSITION=isnull(HOLD_NON_COMP_POSITION,''),@HOLD_NON_COMP_POSITION_DESC = isnull(HOLD_NON_COMP_POSITION_DESC,''),      
 @NON_OWNED_PROPERTY_CARE=isnull(NON_OWNED_PROPERTY_CARE,''),@NON_OWNED_PROPERTY_CARE_DESC = isnull(NON_OWNED_PROPERTY_CARE_DESC,''),      
 @ANIMALS_EXOTIC_PETS=isnull(ANIMALS_EXOTIC_PETS,''),@ANIMALS_EXOTIC_PETS_DESC = isnull(ANIMALS_EXOTIC_PETS_DESC,''),      
 @INSU_TRANSFERED_IN_AGENCY=isnull(INSU_TRANSFERED_IN_AGENCY,''),@INSU_TRANSFERED_IN_AGENCY_DESC = isnull(INSU_TRANSFERED_IN_AGENCY_DESC,''),      
 @IS_TEMPOLINE=isnull(IS_TEMPOLINE,''),@IS_TEMPOLINE_DESC = isnull(IS_TEMPOLINE_DESC,''),      
 @HAVE_NON_OWNED_AUTO_POL=isnull(HAVE_NON_OWNED_AUTO_POL,''),@HAVE_NON_OWNED_AUTO_POL_DESC = isnull(HAVE_NON_OWNED_AUTO_POL_DESC,''),      
 @INS_DOMICILED_OUTSIDE=isnull(INS_DOMICILED_OUTSIDE,''),@INS_DOMICILED_OUTSIDE_DESC = isnull(INS_DOMICILED_OUTSIDE_DESC,''),      
 @HOME_DAY_CARE=isnull(HOME_DAY_CARE,''),@HOME_DAY_CARE_DESC = isnull(HOME_DAY_CARE_DESC,''),      
 @APPLI_UNDERSTAND_LIABILITY_EXCLUDED=isnull(APPLI_UNDERSTAND_LIABILITY_EXCLUDED,''),@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC = isnull(APPLI_UNDERSTAND_LIABILITY_EXCLUDED,''),  
 @FAMILIES=isnull(CONVERT(VARCHAR,FAMILIES),'') , 
 @intFAMILIES =isnull(FAMILIES,0),
 @RENTAL_DWELLINGS_UNIT=isnull(CONVERT(VARCHAR,RENTAL_DWELLINGS_UNIT),'')      
 FROM  POL_UMBRELLA_GEN_INFO  WITH(NOLOCK)                                               
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                  
END                                                  
ELSE                                                  
BEGIN                                                   
 SET @IS_RECORD_EXISTS ='Y'       
 SET @TEMP=''          
END  

    
--IF THE NUMBER OF FAMILIES IS GREATER THAN 4 - REFER TO UNDERWRITERS  
SET @FAMILIES='N'   
	IF (@intFAMILIES > 4)   
	BEGIN  
	SET    @FAMILIES = 'Y'   
	END  
  
--IF THE NUMBER OF RENTAL DWELLING IS GREATER THAN 4 REFER TO UNDERWRITERS   
SET @RENTAL_DWELLINGS_UNIT='N' 

--IF (@RENTAL_DWELLINGS_UNIT >'4')    
--BEGIN  
--SET @RENTAL_DWELLINGS_UNIT='Y'  
--END      
---------------------------------------------------------------------------------------------------------------------                                                
SELECT          
   
 @ENGAGED_IN_FARMING AS ENGAGED_IN_FARMING,                                                 
 CASE @ENGAGED_IN_FARMING      
 WHEN 'Y'THEN  @ENGAGED_IN_FARMING_DESC                                                     
 END AS ENGAGED_IN_FARMING_DESC,      
   
 @REDUCED_LIMIT_OF_LIBLITY AS REDUCED_LIMIT_OF_LIBLITY,                                                 
 CASE @REDUCED_LIMIT_OF_LIBLITY      
 WHEN 'Y'THEN  @REDUCED_LIMIT_OF_LIBLITY_DESC                                                     
 END AS REDUCED_LIMIT_OF_LIBLITY_DESC,      
   
 @WAT_DWELL AS WAT_DWELL,                                                 
 CASE @WAT_DWELL      
 WHEN 'N'THEN  @WAT_DWELL_DESC                                                     
 END AS WAT_DWELL_DESC,      
   
 @RECR_VEH AS RECR_VEH,                                                 
 CASE @RECR_VEH      
 WHEN 'N' THEN  @RECR_VEH_DESC                                                     
 END AS RECR_VEH_DESC,      
   
 @HOME_RENT_DWELL AS HOME_RENT_DWELL,                                                 
 CASE @HOME_RENT_DWELL      
 WHEN 'N'THEN  @HOME_RENT_DWELL_DESC                                                     
 END AS HOME_RENT_DWELL_DESC,      
   
 @AUTO_CYCL_TRUCKS AS AUTO_CYCL_TRUCKS,                                                 
 CASE @AUTO_CYCL_TRUCKS      
 WHEN 'N'THEN  @AUTO_CYCL_TRUCKS_DESC                                                     
 END AS AUTO_CYCL_TRUCKS_DESC,      
   
 @APPLI_UNDERSTAND_LIABILITY_EXCLUDED AS APPLI_UNDERSTAND_LIABILITY_EXCLUDED,                                                 
 CASE @APPLI_UNDERSTAND_LIABILITY_EXCLUDED      
 WHEN 'N'THEN  @APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC                                                     
 END AS APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC,      
   
   
 @REAL_STATE_VEHICLE_USED AS REAL_STATE_VEHICLE_USED,                                                 
 CASE @REAL_STATE_VEHICLE_USED                                                    
 WHEN 'Y'THEN  @REAL_STATE_VEHICLE_USED_DESC                                                     
 END AS REAL_STATE_VEHICLE_USED_DESC,      
   
   
 @REAL_STATE_VEH_OWNED_HIRED AS REAL_STATE_VEH_OWNED_HIRED,                                                 
 CASE @REAL_STATE_VEH_OWNED_HIRED                                                    
 WHEN 'Y'THEN  @REAL_STATE_VEH_OWNED_HIRED_DESC                                                     
 END AS REAL_STATE_VEH_OWNED_HIRED_DESC,      
   
 @PENDING_LITIGATIONS AS PENDING_LITIGATIONS,                              
 CASE @PENDING_LITIGATIONS                                                    
 WHEN 'Y'THEN  @PENDING_LITIGATIONS_DESC                                                     
 END AS PENDING_LITIGATIONS_DESC,      
   
 @ANY_FULL_TIME_EMPLOYEE AS ANY_FULL_TIME_EMPLOYEE,                                                 
 CASE @ANY_FULL_TIME_EMPLOYEE                                 
 WHEN 'Y'THEN  @ANY_FULL_TIME_EMPLOYEE_DESC                                
 END AS ANY_FULL_TIME_EMPLOYEE_DESC,         
   
 @ANY_COVERAGE_DECLINED AS ANY_COVERAGE_DECLINED,                                                 
 CASE @ANY_COVERAGE_DECLINED                                                    
 WHEN 'Y'THEN  @ANY_COVERAGE_DECLINED_DESC                                                     
 END AS ANY_COVERAGE_DECLINED_DESC,                                              
   
 @ANY_AIRCRAFT_OWNED_LEASED AS ANY_AIRCRAFT_OWNED_LEASED,                                                 
 CASE @ANY_AIRCRAFT_OWNED_LEASED                                                    
 WHEN 'Y'THEN  @ANY_AIRCRAFT_OWNED_LEASED_DESC                                                     
 END AS ANY_AIRCRAFT_OWNED_LEASED_DESC,                                                                                              
                                         
 @BUSINESS_PROF_ACTIVITY AS BUSINESS_PROF_ACTIVITY,                                                 
 CASE @BUSINESS_PROF_ACTIVITY                                                    
 WHEN 'Y'THEN  @BUSINESS_PROF_ACTIVITY_DESC                              
 END AS BUSINESS_PROF_ACTIVITY_DESC,   
 @ANIMALS_EXOTIC_PETS AS ANIMALS_EXOTIC_PETS ,  
 CASE @ANIMALS_EXOTIC_PETS  
 WHEN 'Y' THEN @ANIMALS_EXOTIC_PETS_DESC  
 END AS ANIMALS_EXOTIC_PETS_DESC,                                                                                                                 
 -- MANDATORY ONLY   
 @ANY_OPERATOR_CON_TRAFFIC AS ANY_OPERATOR_CON_TRAFFIC,        
 CASE @ANY_OPERATOR_CON_TRAFFIC                                                    
 WHEN 'Y'THEN  @ANY_OPERATOR_CON_TRAFFIC_DESC                                                     
 END AS ANY_OPERATOR_CON_TRAFFIC_DESC,    
 @ANY_OPERATOR_IMPIRED AS ANY_OPERATOR_IMPIRED,    
 CASE @ANY_OPERATOR_IMPIRED      
 WHEN 'Y'THEN  @ANY_OPERATOR_IMPIRED_DESC                                                     
 END AS ANY_OPERATOR_IMPIRED_DESC,      
 CASE @ANY_SWIMMING_POOL      
 WHEN 'Y'THEN  @ANY_SWIMMING_POOL_DESC                                                     
 END AS ANY_SWIMMING_POOL_DESC,      
 CASE @HOLD_NON_COMP_POSITION      
 WHEN 'Y'THEN  @HOLD_NON_COMP_POSITION_DESC                                                     
 END AS HOLD_NON_COMP_POSITION_DESC,      
 CASE @NON_OWNED_PROPERTY_CARE      
 WHEN 'Y'THEN  @NON_OWNED_PROPERTY_CARE_DESC                                                     
 END AS NON_OWNED_PROPERTY_CARE_DESC,      
 CASE @INSU_TRANSFERED_IN_AGENCY      
 WHEN 'Y'THEN  @INSU_TRANSFERED_IN_AGENCY_DESC                                                     
 END AS INSU_TRANSFERED_IN_AGENCY_DESC,      
 CASE @IS_TEMPOLINE      
 WHEN 'Y'THEN  @IS_TEMPOLINE_DESC                                                     
 END AS IS_TEMPOLINE_DESC,      
   
 CASE @HAVE_NON_OWNED_AUTO_POL      
 WHEN 'Y'THEN  @HAVE_NON_OWNED_AUTO_POL_DESC                                                     
 END AS HAVE_NON_OWNED_AUTO_POL,    
   
 @INS_DOMICILED_OUTSIDE AS INS_DOMICILED_OUTSIDE,  
 CASE @INS_DOMICILED_OUTSIDE    
 WHEN 'Y'THEN  @INS_DOMICILED_OUTSIDE_DESC                                                   
 END AS INS_DOMICILED_OUTSIDE_DESC,    
 @HOME_DAY_CARE AS HOME_DAY_CARE,   
 CASE @HOME_DAY_CARE    
 WHEN 'Y'THEN  @HOME_DAY_CARE_DESC                                                   
 END AS HOME_DAY_CARE_DESC,    
 @IS_RECORD_EXISTS AS IS_RECORD_EXISTS ,      
 @TEMP AS TEMP,  
 @FAMILIES AS FAMILIES ,  
 @RENTAL_DWELLINGS_UNIT AS RENTAL_DWELLINGS_UNIT                          
      
END       





GO

