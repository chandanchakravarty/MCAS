IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_HOME_OWNER_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_HOME_OWNER_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_InsertAPP_HOME_OWNER_GEN_INFO                                
Created by      : Anshuman                                
Date            : 5/18/2005                                
Purpose     : Insert record in table APP_HOME_OWNER_GEN_INFO                                
Revison History :                                
Used In  : Brics                                
                                
Modified BY : Anurag Verma                                
Modified On : 13/07/2005                                
Purpose  : making changes for inserting values in no_of_pets field                                 
                                
Modified BY : Anurag Verma                                
Modified On : 07/09/2005                                
Purpose  : altering nchar column of @ANIMALS_EXO_PETS_HISTORY parameter to int                                
                                
Modified BY : Gaurav Tyagi                                
Modified On : 16/09/2005                                
Purpose  : Drop Fields                                
                                
Modified BY : Mohit Gupta                                
Modified On : 23/09/2005                                
Purpose  : adding columns( IS_RENTED_IN_PART,IS_VACENT_OCCUPY,IS_DWELLING_OWNED_BY_OTHER,IS_PROP_NEXT_COMMERICAL,DESC_PROPERTY                                 
    ARE_STAIRWAYS_PRESENT,DESC_STAIRWAYS,IS_OWNERS_DWELLING_CHANGED,DESC_OWNER,DESC_VACENT_OCCUPY,                                
    DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER)                                
                                
Modified BY : Mohit Gupta                                
Modified On : 27/09/2005                                
Purpose  : adding columns(ANY_HEATING_SOURCE,DESC_ANY_HEATING_SOURCE)                                
                                
Modified By     :Mohit Gupta                                
Modified On  :19/10/2005                                
Purpose         : Adding field NON_SMOKER_CREDIT.                           
                          
Modified By     :Mohit Gupta                                
Modified On  :4/11/2005                                
Purpose         : Adding field SWIMMING_POOL,SWIMMING_POOL_TYPE                          
Modified By     :Shafi                              
Modified On  :12/21/2005                                
Purpose         : Adding field Any Forming                
            
Modified By     :Sumit Chhabra            
Modified On   :11/01/2006                                
Purpose         : Adding field Dog Surchage            
          
Modified By     :Shafi          
Modified On     :17/01/2006          
Purpose         :Make IS_active 'Y'            
        
Modified By    : Shafee          
Modified On    : 27-03-2006        
Purpose        : add Column of YEARS_INSU_WOL and YEARS_INSU          
    
Modified By    : Sumit Chhabra    
Modified On    : 17-04-2006        
Purpose        : added Column of APPROVED_FENCE,SLIDE AND DIVING BOARD    
          
Modified By    : RPSINGH    
Modified On    : 12-06-2006        
Purpose        : added Column of PROVIDE_HOME_DAY_CARE    
     MODULAR_MANUFACTURED_HOME    
     BUILT_ON_CONTINUOUS_FOUNDATION          
    
Modified By    : RPSINGH    
Modified On    : 21-06-2006        
Purpose        : added Column of     
    VALUED_CUSTOMER_DISCOUNT_OVERRIDE    
    VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC    
          
Modified By    : RPSINGH    
Modified On    : 05-07-2006        
Purpose        : added Column of     
    PROPERTY_ON_MORE_THAN    
    PROPERTY_ON_MORE_THAN_DESC    
    DWELLING_MOBILE_HOME    
    DWELLING_MOBILE_HOME_DESC    
    PROPERTY_USED_WHOLE_PART    
    PROPERTY_USED_WHOLE_PART_DESC          
     
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                 
--drop proc Proc_InsertAPP_HOME_OWNER_GEN_INFO                         
CREATE      PROC Dbo.Proc_InsertAPP_HOME_OWNER_GEN_INFO                                
(                                
 @CUSTOMER_ID     int,                                
 @APP_ID     int,                                
 @APP_VERSION_ID     smallint,                                
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
 @ANY_COV_DECLINED_CANCELED     nchar(2),                           @DESC_COV_DECLINED_CANCELED     nvarchar(300),                                
 @ANIMALS_EXO_PETS_HISTORY    INT,                                
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
 @REMARKS     nvarchar(2000),                                
 @MULTI_POLICY_DISC_APPLIED NCHAR(2),                                
 @CREATED_BY     int,                                
 @CREATED_DATETIME     datetime,                                
 @NO_OF_PETS INT,                                
 @IS_SWIMPOLL_HOTTUB char(1),                                
 @LAST_INSPECTED_DATE datetime,                                
 @IS_RENTED_IN_PART nchar(1),                                 
 @IS_VACENT_OCCUPY  nchar(1),                                 
 @IS_DWELLING_OWNED_BY_OTHER nchar(1),                                 
 @IS_PROP_NEXT_COMMERICAL nchar(1),                                 
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
 @Any_Forming nchar(1)= null,                
 @Premises int = null,                
 @Of_Acres decimal(9)= null,               
 @Of_Acres_P decimal(9)= null,                
 @IsAny_Horse nchar(1)= null,                
 @No_Horses int= null ,              
 @DESC_FARMING_BUSINESS_COND nvarchar(150),              
 @location nvarchar(50)=null,              
 @DESC_location nvarchar(150)=null,            
 @DOG_SURCHARGE nchar(2) ,         
 @YEARS_INSU_WOL smallint,          
 @YEARS_INSU smallint,      
 @DESC_IS_SWIMPOLL_HOTTUB nvarchar(150),      
 @DESC_MULTI_POLICY_DISC_APPLIED nvarchar(150),      
 @DESC_BUILD_UNDER_CON_GEN_CONT nvarchar(150),     
 @APPROVED_FENCE smallint,      
 @DIVING_BOARD smallint,      
 @SLIDE smallint,    
 @PROVIDE_HOME_DAY_CARE nvarchar(2)=null,     
 @MODULAR_MANUFACTURED_HOME nvarchar(2)=null,    
 @BUILT_ON_CONTINUOUS_FOUNDATION nvarchar(2)=null ,                   
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE nvarchar(2)=null,                    
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC nvarchar(200)=null,    
 @PROPERTY_ON_MORE_THAN  NVARCHAR(2)=null,    
 @PROPERTY_ON_MORE_THAN_DESC  NVARCHAR(1000)=null,    
 @DWELLING_MOBILE_HOME  NVARCHAR(2)=null,    
 @DWELLING_MOBILE_HOME_DESC  NVARCHAR(1000)=null,    
 @PROPERTY_USED_WHOLE_PART  NVARCHAR(2)=null,    
 @PROPERTY_USED_WHOLE_PART_DESC  NVARCHAR(1000)=null ,  
 @ANY_PRIOR_LOSSES nvarchar (10) = null,  
 @ANY_PRIOR_LOSSES_DESC varchar(50)= null ,  
 @BOAT_WITH_HOMEOWNER nchar(1)           =null,
 @NON_WEATHER_CLAIMS smallint=null,
 @WEATHER_CLAIMS smallint =null 
)                                
AS                                
BEGIN  
IF NOT EXISTS(SELECT * FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID = @CUSTOMER_ID
AND APP_ID =@APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID)
BEGIN
                             
 INSERT INTO APP_HOME_OWNER_GEN_INFO                                
 (                                
  CUSTOMER_ID,                                
  APP_ID,                                
  APP_VERSION_ID,                                
  ANY_FARMING_BUSINESS_COND,                                
  --DESC_BUSINESS,                                
  ANY_RESIDENCE_EMPLOYEE,                                
  DESC_RESIDENCE_EMPLOYEE,                                
  ANY_OTHER_RESI_OWNED,                                
  DESC_OTHER_RESIDENCE,                                
  ANY_OTH_INSU_COMP,                                
  DESC_OTHER_INSURANCE,                  
  HAS_INSU_TRANSFERED_AGENCY,                                
  DESC_INSU_TRANSFERED_AGENCY,                                
  ANY_COV_DECLINED_CANCELED,                                
  DESC_COV_DECLINED_CANCELED,                                
  ANIMALS_EXO_PETS_HISTORY,                                
  BREED,                                
  OTHER_DESCRIPTION,                                
                                  
  CONVICTION_DEGREE_IN_PAST,                        
  DESC_CONVICTION_DEGREE_IN_PAST,                                
                                  
  ANY_RENOVATION,                                
  DESC_RENOVATION,                                
                                  
  TRAMPOLINE,                                
  DESC_TRAMPOLINE,                                
                                  
  LEAD_PAINT_HAZARD,                                
  DESC_LEAD_PAINT_HAZARD,                                
                                  
  RENTERS,                                
  DESC_RENTERS,                        
  BUILD_UNDER_CON_GEN_CONT,                                
  REMARKS,                                
  MULTI_POLICY_DISC_APPLIED,                                
  CREATED_BY,                                
  CREATED_DATETIME,                                
  NO_OF_PETS,                                
  IS_SWIMPOLL_HOTTUB,                                
  LAST_INSPECTED_DATE,                                
                                  
  IS_RENTED_IN_PART ,                                 
  IS_VACENT_OCCUPY,                                 
  IS_DWELLING_OWNED_BY_OTHER,                                 
  IS_PROP_NEXT_COMMERICAL ,                                 
  DESC_PROPERTY ,                                
  ARE_STAIRWAYS_PRESENT ,               
  DESC_STAIRWAYS ,        
  IS_OWNERS_DWELLING_CHANGED ,                                 
  DESC_OWNER,                                
  DESC_VACENT_OCCUPY,                                
  DESC_RENTED_IN_PART,                                
  DESC_DWELLING_OWNED_BY_OTHER,                            
  ANY_HEATING_SOURCE,                                
  DESC_ANY_HEATING_SOURCE,                                
  NON_SMOKER_CREDIT,                          
  SWIMMING_POOL,                          
  SWIMMING_POOL_TYPE,                
  Any_Forming ,                
  Premises ,                
  Of_Acres,                
  Of_Acres_P,                
  IsAny_Horse ,                
  No_Horses  ,              
 DESC_FARMING_BUSINESS_COND,              
 location,              
 DESC_location,            
 DOG_SURCHARGE,          
 IS_ACTIVE ,        
 YEARS_INSU_WOL,          
 YEARS_INSU,      
 DESC_IS_SWIMPOLL_HOTTUB,      
 DESC_MULTI_POLICY_DISC_APPLIED,      
 DESC_BUILD_UNDER_CON_GEN_CONT,    
 APPROVED_FENCE,    
 DIVING_BOARD,    
 SLIDE,    
PROVIDE_HOME_DAY_CARE,    
MODULAR_MANUFACTURED_HOME,    
BUILT_ON_CONTINUOUS_FOUNDATION,    
VALUED_CUSTOMER_DISCOUNT_OVERRIDE,    
VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,    
PROPERTY_ON_MORE_THAN,    
PROPERTY_ON_MORE_THAN_DESC,    
DWELLING_MOBILE_HOME,    
DWELLING_MOBILE_HOME_DESC,    
PROPERTY_USED_WHOLE_PART,    
PROPERTY_USED_WHOLE_PART_DESC,  
ANY_PRIOR_LOSSES,  
ANY_PRIOR_LOSSES_DESC,  
BOAT_WITH_HOMEOWNER,
NON_WEATHER_CLAIMS,
WEATHER_CLAIMS    
)                                
 VALUES                                
 (                                
  @CUSTOMER_ID,                                
  @APP_ID,                                
  @APP_VERSION_ID,                                
@ANY_FARMING_BUSINESS_COND,                                
  --@DESC_BUSINESS,                                
  @ANY_RESIDENCE_EMPLOYEE,                                
  @DESC_RESIDENCE_EMPLOYEE,                                
  @ANY_OTHER_RESI_OWNED,                                
  @DESC_OTHER_RESIDENCE,                                
  @ANY_OTH_INSU_COMP,                                
  @DESC_OTHER_INSURANCE,                                
  @HAS_INSU_TRANSFERED_AGENCY,                                
  @DESC_INSU_TRANSFERED_AGENCY,                                
  @ANY_COV_DECLINED_CANCELED,                                
  @DESC_COV_DECLINED_CANCELED,                                
  @ANIMALS_EXO_PETS_HISTORY,                                
  @BREED,                                
  @OTHER_DESCRIPTION,                                
                                  
  @CONVICTION_DEGREE_IN_PAST,                                
  @DESC_CONVICTION_DEGREE_IN_PAST,                                
                                  
  @ANY_RENOVATION,                                
  @DESC_RENOVATION,                                
                                  
  @TRAMPOLINE,                                
  @DESC_TRAMPOLINE,                        
                                  
  @LEAD_PAINT_HAZARD,                                
  @DESC_LEAD_PAINT_HAZARD,                                
                                  
  @RENTERS,                         
  @DESC_RENTERS,                                
  @BUILD_UNDER_CON_GEN_CONT,                    
  @REMARKS,                                
  @MULTI_POLICY_DISC_APPLIED,                                 
  @CREATED_BY,                                
  @CREATED_DATETIME,                                
  @NO_OF_PETS,                                
  @IS_SWIMPOLL_HOTTUB,                                
  @LAST_INSPECTED_DATE,                                
  @IS_RENTED_IN_PART ,                  
  @IS_VACENT_OCCUPY,                                 
  @IS_DWELLING_OWNED_BY_OTHER,                                 
  @IS_PROP_NEXT_COMMERICAL ,                                 
  @DESC_PROPERTY ,                                
  @ARE_STAIRWAYS_PRESENT ,                                 
  @DESC_STAIRWAYS ,           
  @IS_OWNERS_DWELLING_CHANGED ,                                 
  @DESC_OWNER,                                
  @DESC_VACENT_OCCUPY,                                
  @DESC_RENTED_IN_PART,                                
  @DESC_DWELLING_OWNED_BY_OTHER,                                
  @ANY_HEATING_SOURCE,                                
  @DESC_ANY_HEATING_SOURCE,                                
  @NON_SMOKER_CREDIT,                          
  @SWIMMING_POOL,           
  @SWIMMING_POOL_TYPE,                
  @Any_Forming,                
  @Premises ,                
  @Of_Acres ,                
  @Of_Acres_P ,                
  @IsAny_Horse,                
  @No_Horses,              
  @DESC_FARMING_BUSINESS_COND,              
  @location,              
  @DESC_location,             
  @DOG_SURCHARGE,          
  'Y'   ,        
   @YEARS_INSU_WOL,          
   @YEARS_INSU,      
  @DESC_IS_SWIMPOLL_HOTTUB,      
  @DESC_MULTI_POLICY_DISC_APPLIED,      
  @DESC_BUILD_UNDER_CON_GEN_CONT,    
  @APPROVED_FENCE,    
  @DIVING_BOARD,    
  @SLIDE,    
 @PROVIDE_HOME_DAY_CARE,    
 @MODULAR_MANUFACTURED_HOME,    
 @BUILT_ON_CONTINUOUS_FOUNDATION,    
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE,    
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,    
 @PROPERTY_ON_MORE_THAN,    
 @PROPERTY_ON_MORE_THAN_DESC,    
 @DWELLING_MOBILE_HOME,    
 @DWELLING_MOBILE_HOME_DESC,    
 @PROPERTY_USED_WHOLE_PART,    
 @PROPERTY_USED_WHOLE_PART_DESC,  
 @ANY_PRIOR_LOSSES,  
 @ANY_PRIOR_LOSSES_DESC  ,  
 @BOAT_WITH_HOMEOWNER,
 @NON_WEATHER_CLAIMS,
 @WEATHER_CLAIMS  
 )                                
END                                
END                                
                                
                                
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
    
    
    
    
    
  
  
  
 



GO

