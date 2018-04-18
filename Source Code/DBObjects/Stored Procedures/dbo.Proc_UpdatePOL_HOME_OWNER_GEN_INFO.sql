IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_HOME_OWNER_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_HOME_OWNER_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROC dbo.Proc_UpdatePOL_HOME_OWNER_GEN_INFO  
CREATE PROC dbo.Proc_UpdatePOL_HOME_OWNER_GEN_INFO        
(                  
 @CUSTOMER_ID     int,                  
 @POL_ID     int,                  
 @POL_VERSION_ID     smallint,                  
 @ANY_FARMING_BUSINESS_COND     nchar(2),                  
 --@DESC_BUSINESS     nvarchar(300),                  
 @ANY_RESIDENCE_EMPLOYEE     nchar(2),                  
 @DESC_RESIDENCE_EMPLOYEE     nvarchar(300),                  
 @ANY_OTHER_RESI_OWNED     nchar(2),                  
 @DESC_OTHER_RESIDENCE     nvarchar(300),                  
 @ANY_OTH_INSU_COMP     nchar(2),                  
 @DESC_OTHER_INSURANCE     nvarchar(300),                  
 @HAS_INSU_TRANSFERED_AGENCY     nchar(2),                  
 @DESC_INSU_TRANSFERED_AGENCY     nvarchar(300),                  
 @ANY_COV_DECLINED_CANCELED     nchar(2),                  
 @DESC_COV_DECLINED_CANCELED     nvarchar(300),                  
 @ANIMALS_EXO_PETS_HISTORY     int,                  
 @BREED     nvarchar(200),                  
 @OTHER_DESCRIPTION     nvarchar(200),                  
                 
                   
 @CONVICTION_DEGREE_IN_PAST     nchar(2),                  
 @DESC_CONVICTION_DEGREE_IN_PAST     nvarchar(300),            
                
 @ANY_RENOVATION     nchar(2),                  
 @DESC_RENOVATION     nvarchar(300),                  
                 
 @TRAMPOLINE     nchar(2),                  
 @DESC_TRAMPOLINE     nvarchar(300),                  
                 
 @LEAD_PAINT_HAZARD     nchar(2),                  
 @DESC_LEAD_PAINT_HAZARD     nvarchar(300),                  
                 
 @RENTERS     nchar(2),                  
 @DESC_RENTERS     nvarchar(300),                  
 @BUILD_UNDER_CON_GEN_CONT     nchar(2),                  
 @REMARKS     nvarchar(255),                  
 @MULTI_POLICY_DISC_APPLIED NCHAR(2),                  
 @IS_ACTIVE     nchar(2),                  
 @MODIFIED_BY     int,                  
 @LAST_UPDATED_DATETIME datetime,                  
 @NO_OF_PETS INT ,                
@IS_SWIMPOLL_HOTTUB char(1),                
@LAST_INSPECTED_DATE datetime,                
                
@IS_RENTED_IN_PART nchar(1),                 
@IS_VACENT_OCCUPY  nchar(1),                 
@IS_DWELLING_OWNED_BY_OTHER nchar(1),   @IS_PROP_NEXT_COMMERICAL nchar(1),                 
@DESC_PROPERTY nvarchar(150),                
@ARE_STAIRWAYS_PRESENT  nchar(1),                 
@DESC_STAIRWAYS nvarchar(150),                
@IS_OWNERS_DWELLING_CHANGED nchar(1),                 
@DESC_OWNER nvarchar(150),                
@DESC_VACENT_OCCUPY nvarchar(150),                
@DESC_RENTED_IN_PART nvarchar(150),                
@DESC_DWELLING_OWNED_BY_OTHER nvarchar(150),                
@ANY_HEATING_SOURCE nchar(1),                 
@DESC_ANY_HEATING_SOURCE nvarchar(150),                
@NON_SMOKER_CREDIT nchar(1),          
@SWIMMING_POOL     nchar(1),          
 @SWIMMING_POOL_TYPE int,      
 @ANY_FORMING NCHAR(1)= NULL,              
 @PREMISES INT = NULL,              
 @OF_ACRES DECIMAL(9)= NULL,              
 @OF_ACRES_P DECIMAL(9)= NULL,              
 @ISANY_HORSE NCHAR(1)= NULL,              
 @NO_HORSES INT= NULL ,            
 @LOCATION NVARCHAR(50)=NULL,            
 @DESC_LOCATION NVARCHAR(150)=NULL,    
 @DESC_IS_SWIMPOLL_HOTTUB nvarchar(150),    
 @DESC_MULTI_POLICY_DISC_APPLIED nvarchar(150),    
 @DESC_BUILD_UNDER_CON_GEN_CONT nvarchar(150),  
 @APPROVED_FENCE smallint,    
 @DIVING_BOARD smallint,    
 @SLIDE smallint,  
 @YEARS_INSU_WOL smallint,        
 @YEARS_INSU smallint   ,  
 @PROVIDE_HOME_DAY_CARE  nvarchar(2) = null,  
 @MODULAR_MANUFACTURED_HOME  nvarchar(2) = null,  
 @BUILT_ON_CONTINUOUS_FOUNDATION  nvarchar(2) = null,  
 @DESC_FARMING_BUSINESS_COND nvarchar(600) = null,  
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE  nvarchar(2)  = null,  
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC  nvarchar(600)  = null,  
 @PROPERTY_ON_MORE_THAN  nvarchar(600)  = null,  
 @PROPERTY_ON_MORE_THAN_DESC  nvarchar(600)  = null,  
 @DWELLING_MOBILE_HOME  nvarchar(600)  = null,  
 @DWELLING_MOBILE_HOME_DESC  nvarchar(600)  = null,  
 @PROPERTY_USED_WHOLE_PART  nvarchar(600)  = null,  
 @PROPERTY_USED_WHOLE_PART_DESC  nvarchar(600)  = null  ,
 @ANY_PRIOR_LOSSES nvarchar (10) = null,
 @ANY_PRIOR_LOSSES_DESC varchar(50)= null ,
 @BOAT_WITH_HOMEOWNER nchar(1) = null,
 @NON_WEATHER_CLAIMS smallint=null,
 @WEATHER_CLAIMS smallint=null            
)                  
AS                  
BEGIN                  
 UPDATE POL_HOME_OWNER_GEN_INFO SET                  
                   
  ANY_FARMING_BUSINESS_COND = @ANY_FARMING_BUSINESS_COND,                  
  --DESC_BUSINESS   = @DESC_BUSINESS,                  
  ANY_RESIDENCE_EMPLOYEE  = @ANY_RESIDENCE_EMPLOYEE,                  
  DESC_RESIDENCE_EMPLOYEE  = @DESC_RESIDENCE_EMPLOYEE,                  
  ANY_OTHER_RESI_OWNED  = @ANY_OTHER_RESI_OWNED,                  
  DESC_OTHER_RESIDENCE  = @DESC_OTHER_RESIDENCE,                  
ANY_OTH_INSU_COMP  = @ANY_OTH_INSU_COMP,                  
  DESC_OTHER_INSURANCE  = @DESC_OTHER_INSURANCE,                  
  HAS_INSU_TRANSFERED_AGENCY = @HAS_INSU_TRANSFERED_AGENCY,                  
  DESC_INSU_TRANSFERED_AGENCY = @DESC_INSU_TRANSFERED_AGENCY,                  
  ANY_COV_DECLINED_CANCELED = @ANY_COV_DECLINED_CANCELED,                  
  DESC_COV_DECLINED_CANCELED = @DESC_COV_DECLINED_CANCELED,                  
  ANIMALS_EXO_PETS_HISTORY = @ANIMALS_EXO_PETS_HISTORY,                  
  BREED    = @BREED,                  
  OTHER_DESCRIPTION  = @OTHER_DESCRIPTION,                   
                 
  CONVICTION_DEGREE_IN_PAST = @CONVICTION_DEGREE_IN_PAST,                  
  DESC_CONVICTION_DEGREE_IN_PAST = @DESC_CONVICTION_DEGREE_IN_PAST,                  
                 
  ANY_RENOVATION      = @ANY_RENOVATION,                  
  DESC_RENOVATION       = @DESC_RENOVATION,                  
                 
  TRAMPOLINE        = @TRAMPOLINE,                  
  DESC_TRAMPOLINE       = @DESC_TRAMPOLINE,                  
                 
  LEAD_PAINT_HAZARD  = @LEAD_PAINT_HAZARD,                  
  DESC_LEAD_PAINT_HAZARD  = @DESC_LEAD_PAINT_HAZARD,                  
                  
  RENTERS    = @RENTERS,                  
  DESC_RENTERS   = @DESC_RENTERS,                  
  BUILD_UNDER_CON_GEN_CONT = @BUILD_UNDER_CON_GEN_CONT,                  
  REMARKS    = @REMARKS,                  
  IS_ACTIVE   = @IS_ACTIVE,                  
  MODIFIED_BY   = @MODIFIED_BY,                  
  LAST_UPDATED_DATETIME  = @LAST_UPDATED_DATETIME,                  
  MULTI_POLICY_DISC_APPLIED = @MULTI_POLICY_DISC_APPLIED,                  
  NO_OF_PETS   = @NO_OF_PETS ,                
IS_SWIMPOLL_HOTTUB =@IS_SWIMPOLL_HOTTUB ,                
LAST_INSPECTED_DATE=@LAST_INSPECTED_DATE,                
IS_RENTED_IN_PART=@IS_RENTED_IN_PART ,                 
IS_VACENT_OCCUPY=@IS_VACENT_OCCUPY,                 
IS_DWELLING_OWNED_BY_OTHER=@IS_DWELLING_OWNED_BY_OTHER,                 
IS_PROP_NEXT_COMMERICAL=@IS_PROP_NEXT_COMMERICAL ,                 
DESC_PROPERTY =@DESC_PROPERTY,                
ARE_STAIRWAYS_PRESENT=@ARE_STAIRWAYS_PRESENT ,                 
DESC_STAIRWAYS =@DESC_STAIRWAYS,                
IS_OWNERS_DWELLING_CHANGED=@IS_OWNERS_DWELLING_CHANGED ,                 
DESC_OWNER =@DESC_OWNER,                
DESC_VACENT_OCCUPY=@DESC_VACENT_OCCUPY,                
DESC_RENTED_IN_PART=@DESC_RENTED_IN_PART,                
DESC_DWELLING_OWNED_BY_OTHER=@DESC_DWELLING_OWNED_BY_OTHER,                
ANY_HEATING_SOURCE=@ANY_HEATING_SOURCE,                
DESC_ANY_HEATING_SOURCE=@DESC_ANY_HEATING_SOURCE,                
NON_SMOKER_CREDIT=@NON_SMOKER_CREDIT,          
SWIMMING_POOL=@SWIMMING_POOL,          
SWIMMING_POOL_TYPE=@SWIMMING_POOL_TYPE,      
ANY_FORMING = @ANY_FORMING,      
PREMISES = @PREMISES,      
OF_ACRES =@OF_ACRES,      
OF_ACRES_P = @OF_ACRES_P,      
ISANY_HORSE =  @ISANY_HORSE,      
NO_HORSES = @NO_HORSES,      
LOCATION = @LOCATION,      
DESC_LOCATION  =@DESC_LOCATION,    
DESC_IS_SWIMPOLL_HOTTUB=@DESC_IS_SWIMPOLL_HOTTUB,    
DESC_MULTI_POLICY_DISC_APPLIED=@DESC_MULTI_POLICY_DISC_APPLIED,    
DESC_BUILD_UNDER_CON_GEN_CONT=@DESC_BUILD_UNDER_CON_GEN_CONT,  
APPROVED_FENCE=  @APPROVED_FENCE,  
DIVING_BOARD= @DIVING_BOARD,  
SLIDE  = @SLIDE,  
YEARS_INSU_WOL =@YEARS_INSU_WOL,  
YEARS_INSU =@YEARS_INSU             ,                     
PROVIDE_HOME_DAY_CARE = @PROVIDE_HOME_DAY_CARE,  
MODULAR_MANUFACTURED_HOME = @MODULAR_MANUFACTURED_HOME,  
BUILT_ON_CONTINUOUS_FOUNDATION = @BUILT_ON_CONTINUOUS_FOUNDATION,  
DESC_FARMING_BUSINESS_COND = @DESC_FARMING_BUSINESS_COND,  
VALUED_CUSTOMER_DISCOUNT_OVERRIDE = @VALUED_CUSTOMER_DISCOUNT_OVERRIDE ,  
VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC = @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,  
PROPERTY_ON_MORE_THAN = @PROPERTY_ON_MORE_THAN,  
PROPERTY_ON_MORE_THAN_DESC = @PROPERTY_ON_MORE_THAN_DESC,  
DWELLING_MOBILE_HOME = @DWELLING_MOBILE_HOME,  
DWELLING_MOBILE_HOME_DESC = @DWELLING_MOBILE_HOME_DESC,  
PROPERTY_USED_WHOLE_PART = @PROPERTY_USED_WHOLE_PART,  
PROPERTY_USED_WHOLE_PART_DESC = @PROPERTY_USED_WHOLE_PART_DESC,
ANY_PRIOR_LOSSES = @ANY_PRIOR_LOSSES,
ANY_PRIOR_LOSSES_DESC = @ANY_PRIOR_LOSSES_DESC,
BOAT_WITH_HOMEOWNER = @BOAT_WITH_HOMEOWNER,
NON_WEATHER_CLAIMS= @NON_WEATHER_CLAIMS ,
WEATHER_CLAIMS= @WEATHER_CLAIMS   
  
 WHERE                  
  CUSTOMER_ID   = @CUSTOMER_ID AND                  
  POLICY_ID    = @POL_ID AND                  
  POLICY_VERSION_ID   = @POL_VERSION_ID                  
END           
  
  
  
  
  
  
  
  
  
  






GO

