IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_UMBRELLA_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_UMBRELLA_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP PROC dbo.Proc_UpdatePOL_UMBRELLA_GEN_INFO
CREATE  PROC dbo.Proc_UpdatePOL_UMBRELLA_GEN_INFO        
(        
 @CUSTOMER_ID     int,        
 @POLICY_ID     int,        
 @POLICY_VERSION_ID     smallint,        
 @ANY_AIRCRAFT_OWNED_LEASED     nchar(1),        
 @ANY_OPERATOR_CON_TRAFFIC     nchar(1),        
 @ANY_OPERATOR_IMPIRED     nchar(1),        
 @ANY_SWIMMING_POOL     nchar(1),        
 @REAL_STATE_VEHICLE_USED     nchar(1),        
 @REAL_STATE_VEH_OWNED_HIRED     nchar(1),        
 @ENGAGED_IN_FARMING     nchar(1),        
 @HOLD_NON_COMP_POSITION     nchar(1),        
 @ANY_FULL_TIME_EMPLOYEE     nchar(1),        
 @NON_OWNED_PROPERTY_CARE     nchar(1),        
 @BUSINESS_PROF_ACTIVITY     nchar(1),        
 @REDUCED_LIMIT_OF_LIBLITY     nchar(1),        
 @ANY_COVERAGE_DECLINED     nchar(1),        
 @ANIMALS_EXOTIC_PETS     nchar(1),        
 @INSU_TRANSFERED_IN_AGENCY     nchar(1),        
 @PENDING_LITIGATIONS     nchar(1),        
 @IS_TEMPOLINE     nchar(1),        
 @REMARKS     nvarchar(510),        
 @MODIFIED_BY     int,        
 @ANY_AIRCRAFT_OWNED_LEASED_DESC nvarchar(300),        
 @ANY_OPERATOR_CON_TRAFFIC_DESC nvarchar(300),        
 @ANY_OPERATOR_IMPIRED_DESC nvarchar(300),        
 @ANY_SWIMMING_POOL_DESC nvarchar(300),        
 @REAL_STATE_VEHICLE_USED_DESC nvarchar(300),        
 @REAL_STATE_VEH_OWNED_HIRED_DESC nvarchar(300),        
 @ENGAGED_IN_FARMING_DESC nvarchar(300),      
 @HOLD_NON_COMP_POSITION_DESC nvarchar(150),        
 @ANY_FULL_TIME_EMPLOYEE_DESC nvarchar(150),        
 @NON_OWNED_PROPERTY_CARE_DESC nvarchar(150),        
 @BUSINESS_PROF_ACTIVITY_DESC nvarchar(150),        
 @REDUCED_LIMIT_OF_LIBLITY_DESC nvarchar(150),        
 @ANIMALS_EXOTIC_PETS_DESC nvarchar(150),        
 @ANY_COVERAGE_DECLINED_DESC nvarchar(150),          
 @INSU_TRANSFERED_IN_AGENCY_DESC nvarchar(150),      
 @PENDING_LITIGATIONS_DESC nvarchar(150),        
 @IS_TEMPOLINE_DESC nvarchar(150),        
 @HAVE_NON_OWNED_AUTO_POL_DESC nvarchar(150),        
 @INS_DOMICILED_OUTSIDE_DESC nvarchar(150),          
 @HOME_DAY_CARE_DESC nvarchar(150),      
 @HAVE_NON_OWNED_AUTO_POL nchar(1),      
 @INS_DOMICILED_OUTSIDE nchar(1),      
 @HOME_DAY_CARE nchar(1),    
 @CALCULATIONS nvarchar(100),    
 @HOME_RENT_DWELL nchar (1),    
 @HOME_RENT_DWELL_DESC nvarchar(150),    
 @WAT_DWELL nchar(1),    
 @WAT_DWELL_DESC nvarchar(150),    
 @RECR_VEH nchar(1),    
 @RECR_VEH_DESC nvarchar(150),    
 @AUTO_CYCL_TRUCKS nchar(1),    
 @AUTO_CYCL_TRUCKS_DESC nvarchar(150),    
 @APPLI_UNDERSTAND_LIABILITY_EXCLUDED nchar(1),    
 @APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC nvarchar(150),    
 @UND_REMARKS  nvarchar(300),  
 @OFFICE_PREMISES int,    
 @RENTAL_DWELLINGS_UNIT int,
 @FAMILIES int  
)        
        
AS        
        
BEGIN        
 UPDATE POL_UMBRELLA_GEN_INFO          
 SET        
          
  ANY_AIRCRAFT_OWNED_LEASED = @ANY_AIRCRAFT_OWNED_LEASED,        
  ANY_OPERATOR_CON_TRAFFIC = @ANY_OPERATOR_CON_TRAFFIC,        
  ANY_OPERATOR_IMPIRED =  @ANY_OPERATOR_IMPIRED,        
  ANY_SWIMMING_POOL = @ANY_SWIMMING_POOL,        
  REAL_STATE_VEHICLE_USED = @REAL_STATE_VEHICLE_USED,        
  REAL_STATE_VEH_OWNED_HIRED = @REAL_STATE_VEH_OWNED_HIRED,        
  ENGAGED_IN_FARMING = @ENGAGED_IN_FARMING,        
  HOLD_NON_COMP_POSITION = @HOLD_NON_COMP_POSITION,        
  ANY_FULL_TIME_EMPLOYEE = @ANY_FULL_TIME_EMPLOYEE,        
  NON_OWNED_PROPERTY_CARE = @NON_OWNED_PROPERTY_CARE,        
  BUSINESS_PROF_ACTIVITY = @BUSINESS_PROF_ACTIVITY ,        
  REDUCED_LIMIT_OF_LIBLITY = @REDUCED_LIMIT_OF_LIBLITY,        
  ANY_COVERAGE_DECLINED = @ANY_COVERAGE_DECLINED,        
  ANIMALS_EXOTIC_PETS = @ANIMALS_EXOTIC_PETS,        
  INSU_TRANSFERED_IN_AGENCY = @INSU_TRANSFERED_IN_AGENCY,        
  PENDING_LITIGATIONS = @PENDING_LITIGATIONS,        
  IS_TEMPOLINE = @IS_TEMPOLINE,        
  REMARKS = @REMARKS,        
  MODIFIED_BY = @MODIFIED_BY,          
  LAST_UPDATED_DATETIME = GetDate(),        
  ANY_AIRCRAFT_OWNED_LEASED_DESC =@ANY_AIRCRAFT_OWNED_LEASED_DESC,        
  ANY_OPERATOR_CON_TRAFFIC_DESC =@ANY_OPERATOR_CON_TRAFFIC_DESC,        
  ANY_OPERATOR_IMPIRED_DESC =@ANY_OPERATOR_IMPIRED_DESC,        
  ANY_SWIMMING_POOL_DESC =@ANY_SWIMMING_POOL_DESC,        
  REAL_STATE_VEHICLE_USED_DESC =@REAL_STATE_VEHICLE_USED_DESC,        
  REAL_STATE_VEH_OWNED_HIRED_DESC =@REAL_STATE_VEH_OWNED_HIRED_DESC,        
  ENGAGED_IN_FARMING_DESC =@ENGAGED_IN_FARMING_DESC,      
  HOLD_NON_COMP_POSITION_DESC=@HOLD_NON_COMP_POSITION_DESC,      
  ANY_FULL_TIME_EMPLOYEE_DESC=@ANY_FULL_TIME_EMPLOYEE_DESC,      
  NON_OWNED_PROPERTY_CARE_DESC=@NON_OWNED_PROPERTY_CARE_DESC,      
  BUSINESS_PROF_ACTIVITY_DESC=@BUSINESS_PROF_ACTIVITY_DESC,      
  REDUCED_LIMIT_OF_LIBLITY_DESC=@REDUCED_LIMIT_OF_LIBLITY_DESC,      
  ANIMALS_EXOTIC_PETS_DESC=@ANIMALS_EXOTIC_PETS_DESC,      
  ANY_COVERAGE_DECLINED_DESC=@ANY_COVERAGE_DECLINED_DESC,      
  INSU_TRANSFERED_IN_AGENCY_DESC  = @INSU_TRANSFERED_IN_AGENCY_DESC,      
  PENDING_LITIGATIONS_DESC=@PENDING_LITIGATIONS_DESC,      
  IS_TEMPOLINE_DESC=@IS_TEMPOLINE_DESC,      
  HAVE_NON_OWNED_AUTO_POL_DESC=@HAVE_NON_OWNED_AUTO_POL_DESC,      
  INS_DOMICILED_OUTSIDE_DESC=@INS_DOMICILED_OUTSIDE_DESC,      
  HOME_DAY_CARE_DESC=@HOME_DAY_CARE_DESC,      
  HAVE_NON_OWNED_AUTO_POL=@HAVE_NON_OWNED_AUTO_POL,      
  INS_DOMICILED_OUTSIDE=@INS_DOMICILED_OUTSIDE,      
  HOME_DAY_CARE=@HOME_DAY_CARE,    
  CALCULATIONS=@CALCULATIONS,    
  HOME_RENT_DWELL=@HOME_RENT_DWELL,    
  HOME_RENT_DWELL_DESC=@HOME_RENT_DWELL_DESC,    
  WAT_DWELL=@WAT_DWELL,    
  WAT_DWELL_DESC=@WAT_DWELL_DESC,    
  RECR_VEH=@RECR_VEH,    
  RECR_VEH_DESC=@RECR_VEH_DESC,    
  AUTO_CYCL_TRUCKS=@AUTO_CYCL_TRUCKS,    
  AUTO_CYCL_TRUCKS_DESC=@AUTO_CYCL_TRUCKS_DESC,    
  APPLI_UNDERSTAND_LIABILITY_EXCLUDED=@APPLI_UNDERSTAND_LIABILITY_EXCLUDED,    
  APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC=@APPLI_UNDERSTAND_LIABILITY_EXCLUDED_DESC,    
  UND_REMARKS=@UND_REMARKS ,  
  OFFICE_PREMISES=@OFFICE_PREMISES,    
  RENTAL_DWELLINGS_UNIT=@RENTAL_DWELLINGS_UNIT,
  FAMILIES=@FAMILIES      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  POLICY_ID = @POLICY_ID AND          
  POLICY_VERSION_ID = @POLICY_VERSION_ID          
          
 RETURN 1          
           
END          
        
        
      
      
      
     
  



GO

