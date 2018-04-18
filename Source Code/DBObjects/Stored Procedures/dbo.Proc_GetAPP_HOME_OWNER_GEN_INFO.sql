IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_HOME_OWNER_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_HOME_OWNER_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetAPP_HOME_OWNER_GEN_INFO                            
Created by      : Anshuman                            
Date            : 5/18/2005                            
Purpose     : Fetch record in table APP_HOME_OWNER_GEN_INFO                            
Revison History :                            
Used In  : Brics                            
                            
Modified By : 1/7/2005                            
Modified On : Anurag Verma                            
Purpose  : fetching Multi_policy_disc_applied in query                             
                            
Modified By : 13/7/2005                            
Modified On : Anurag Verma                            
Purpose  : fetching no_of_pets in query                             
                            
Modified BY : Mohit Gupta                            
Modified On : 23/09/2005                            
Purpose  : adding columns( IS_RENTED_IN_PART,IS_VACENT_OCCUPY,IS_DWELLING_OWNED_BY_OTHER,IS_PROP_NEXT_COMMERICAL,DESC_PROPERTY                             
    ARE_STAIRWAYS_PRESENT,DESC_STAIRWAYS,IS_OWNERS_DWELLING_CHANGED,DESC_OWNER)                            
Modified BY : Mohit Gupta                            
Modified On : 23/09/2005                            
Purpose  : adding columns.                            
                            
Modified BY : Mohit Gupta                            
Modified On : 23/09/2005                            
Purpose  : adding column NON_SMOKER_CREDIT.                            
Modified BY : Shafi              
Modified On : 12/21/2005                            
Purpose  : adding column Any Forming         
      
Modified By    : Shafee        
Modified On    : 27-03-2006      
Purpose        : add Column of YEARS_INSU_WOL and YEARS_INSU       
  
Modified By    : Sumit Chhabra  
Modified On    : 17-04-2006      
Purpose        : added Column of APPROVED_FENCE,SLIDE AND DIVING BOARD        
  
Modified By    : Raman Pal Singh  
Modified On    : 12-06-2006      
Purpose        : added Column of PROVIDE_HOME_DAY_CARE  
     MODULAR_MANUFACTURED_HOME  
     BUILT_ON_CONTINUOUS_FOUNDATION  
              
Modified By    : Raman Pal Singh  
Modified On    : 05-07-2006      
Purpose        : added Column of  PROPERTY_ON_MORE_THAN  
     PROPERTY_ON_MORE_THAN_DESC  
     DWELLING_MOBILE_HOME  
     DWELLING_MOBILE_HOME_DESC  
     PROPERTY_USED_WHOLE_PART  
     PROPERTY_USED_WHOLE_PART_DESC  
                  
                            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/       
--drop proc Proc_GetAPP_HOME_OWNER_GEN_INFO                            
create PROC Dbo.Proc_GetAPP_HOME_OWNER_GEN_INFO                            
(                            
  @CUSTOMER_ID  int,                            
  @APP_ID   int,                            
  @APP_VERSION_ID  smallint,                  
  @CALLED_FROM varchar(10)                  
)                            
AS                            
BEGIN                      
 DECLARE @IS_AUTO_POL_WITH_CARRIER INT                  
 DECLARE @SECONDARY_HEAT_TYPE INT                  
 --IF( EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID) OR UPPER(@CALLED_FROM)='RENTAL')                  
 --BEGIN                   
 -- IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)                  
 SELECT                            
  CUSTOMER_ID, APP_ID, APP_VERSION_ID, ANY_FARMING_BUSINESS_COND, --DESC_BUSINESS,            
  ANY_RESIDENCE_EMPLOYEE, DESC_RESIDENCE_EMPLOYEE,                            
  ANY_OTHER_RESI_OWNED, DESC_OTHER_RESIDENCE, ANY_OTH_INSU_COMP,                            
  DESC_OTHER_INSURANCE, HAS_INSU_TRANSFERED_AGENCY, DESC_INSU_TRANSFERED_AGENCY,                            
  ANY_COV_DECLINED_CANCELED, DESC_COV_DECLINED_CANCELED, ANIMALS_EXO_PETS_HISTORY,                             
  BREED, OTHER_DESCRIPTION, CONVICTION_DEGREE_IN_PAST,                            
  DESC_CONVICTION_DEGREE_IN_PAST,ANY_RENOVATION,DESC_RENOVATION,                            
  TRAMPOLINE,DESC_TRAMPOLINE,LEAD_PAINT_HAZARD,DESC_LEAD_PAINT_HAZARD,                            
  RENTERS,DESC_RENTERS,BUILD_UNDER_CON_GEN_CONT,                            
  REMARKS,IS_ACTIVE, CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME,MULTI_POLICY_DISC_APPLIED,NO_OF_PETS,IS_SWIMPOLL_HOTTUB,CONVERT(VARCHAR,LAST_INSPECTED_DATE,101) AS LAST_INSPECTED_DATE,                            
  IS_RENTED_IN_PART , IS_VACENT_OCCUPY, IS_DWELLING_OWNED_BY_OTHER, IS_PROP_NEXT_COMMERICAL , DESC_PROPERTY ,ARE_STAIRWAYS_PRESENT ,                             
  DESC_STAIRWAYS ,IS_OWNERS_DWELLING_CHANGED , DESC_OWNER,                            
  DESC_VACENT_OCCUPY,DESC_RENTED_IN_PART,DESC_DWELLING_OWNED_BY_OTHER,                             
  ANY_HEATING_SOURCE,DESC_ANY_HEATING_SOURCE,NON_SMOKER_CREDIT,SWIMMING_POOL,SWIMMING_POOL_TYPE,Any_Forming,Premises,              
  Of_Acres ,Of_Acres_P,IsAny_Horse,No_Horses,DESC_FARMING_BUSINESS_COND,location,DESC_location,DOG_SURCHARGE ,YEARS_INSU,YEARS_INSU_WOL,    
  DESC_IS_SWIMPOLL_HOTTUB,DESC_MULTI_POLICY_DISC_APPLIED,DESC_BUILD_UNDER_CON_GEN_CONT,  
  APPROVED_FENCE,SLIDE, DIVING_BOARD    ,  
 PROVIDE_HOME_DAY_CARE,  
 MODULAR_MANUFACTURED_HOME,  
 BUILT_ON_CONTINUOUS_FOUNDATION,   
PROPERTY_ON_MORE_THAN,  
PROPERTY_ON_MORE_THAN_DESC,  
DWELLING_MOBILE_HOME,  
DWELLING_MOBILE_HOME_DESC,  
PROPERTY_USED_WHOLE_PART,  
PROPERTY_USED_WHOLE_PART_DESC,
ANY_PRIOR_LOSSES,
ANY_PRIOR_LOSSES_DESC,
VALUED_CUSTOMER_DISCOUNT_OVERRIDE,
VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,
BOAT_WITH_HOMEOWNER,
NON_WEATHER_CLAIMS,    
WEATHER_CLAIMS     
         
 FROM               
  APP_HOME_OWNER_GEN_INFO                            
 WHERE                            
  CUSTOMER_ID   = @CUSTOMER_ID AND                            
  APP_ID    = @APP_ID AND                    
  APP_VERSION_ID   = @APP_VERSION_ID                            
               
 /*END                  
 ELSE                  
 BEGIN                  
 SELECT @IS_AUTO_POL_WITH_CARRIER=COUNT(IS_AUTO_POL_WITH_CARRIER) FROM APP_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_AUTO_POL_WITH_CARRIER='1'                  
 SELECT @SECONDARY_HEAT_TYPE=COUNT(SECONDARY_HEAT_TYPE) FROM APP_HOME_RATING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND SECONDARY_HEAT_TYPE <>'6211'                  
 SELECT CASE @IS_AUTO_POL_WITH_CARRIER WHEN 1 THEN '1' ELSE '0' END  MULTI_POLICY_DISC_APPLIED,CASE @SECONDARY_HEAT_TYPE WHEN 0 THEN '0' ELSE '1' END  ANY_HEATING_SOURCE                  
 END */                 
END                  
                  
                
              
            
          
        
      
    
  
  
  
  
  








GO

