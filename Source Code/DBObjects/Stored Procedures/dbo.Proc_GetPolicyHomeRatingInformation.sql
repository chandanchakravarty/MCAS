IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyHomeRatingInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyHomeRatingInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetPolicyHomeRatingInformation                          
Created by      : SHAFI                          
Date            :17 FEB 2006                  
Purpose         : Gets information from Home rating,Square footage,Construction info                          
                   and Protective devices                          
Revison History :                          
      
        
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--DROP PROC dbo.Proc_GetPolicyHomeRatingInformation      
CREATE PROC dbo.Proc_GetPolicyHomeRatingInformation                          
(                          
 @CUSTOMER_ID int,                          
 @POLICY_ID int,                          
 @POLICY_VERSION_ID smallint,                          
 @DWELLING_ID  smallint                          
)                          
AS                          
BEGIN                          
                          
--Rating Info                          
SELECT                             
 HYDRANT_DIST,                            
 FIRE_STATION_DIST,                            
 IS_UNDER_CONSTRUCTION,                            
 EXPERIENCE_CREDIT,                            
 IS_AUTO_POL_WITH_CARRIER,                            
-- PERSONAL_LIAB_TER_CODE,                            
 PROT_CLASS,                            
-- RATING_METHOD,                          
 --NO_OF_APARTMENTS,                          
 NO_OF_FAMILIES,            
 CONSTRUCTION_CODE,                        
 EXTERIOR_CONSTRUCTION,                          
       EXTERIOR_OTHER_DESC,                          
 FOUNDATION,                          
 FOUNDATION_OTHER_DESC,                          
 ROOF_TYPE,                           
 ROOF_OTHER_DESC,                        
--WIRING ,                          
 --Convert(Varchar, WIRING_LAST_INSPECTED, 101) As                          
 --      WIRING_LAST_INSPECTED,                          
 PRIMARY_HEAT_TYPE,                          
 SECONDARY_HEAT_TYPE,                          
 MONTH_OCC_EACH_YEAR,ADD_COVERAGE_INFO,                          
 --     IS_OUTSIDE_STAIR,                          
 --TOT_SQR_FOOTAGE,                            
 --GARAGE_SQR_FOOTAGE,                            
 --BREEZE_SQR_FOOTAGE,                            
 --BASMT_SQR_FOOTAGE,                                   
  WIRING_RENOVATION,                            
  WIRING_UPDATE_YEAR,                            
  PLUMBING_RENOVATION,                            
  PLUMBING_UPDATE_YEAR,                            
  HEATING_RENOVATION,                            
  HEATING_UPDATE_YEAR,                            
  ROOFING_RENOVATION,                            
  ROOFING_UPDATE_YEAR,                            
  NO_OF_AMPS,                            
  CIRCUIT_BREAKERS,    
  ALARM_CERT_ATTACHED,                          
   PROTECTIVE_DEVICES,                          
 TEMPERATURE,SMOKE,BURGLAR,                            
         FIRE_PLACES,NO_OF_WOOD_STOVES,                          
 --SWIMMING_POOL,                          
 --SWIMMING_POOL_TYPE,                        
PRIMARY_HEAT_OTHER_DESC,                        
SECONDARY_HEAT_OTHER_DESC,Convert(varchar,DWELLING_CONST_DATE,101)  as DWELLING_CONST_DATE ,                      
CENT_ST_BURG_FIRE,                      
CENT_ST_FIRE,                      
CENT_ST_BURG,                      
DIR_FIRE_AND_POLICE,                      
DIR_FIRE,                      
DIR_POLICE,          
LOC_FIRE_GAS,                      
TWO_MORE_FIRE,        
NUM_LOC_ALARMS_APPLIES,    
SPRINKER ,  
IS_SUPERVISED,  
NEED_OF_UNITS,  
SUBURBAN_CLASS,  
ISNULL(LOCATED_IN_SUBDIVISION,'') AS LOCATED_IN_SUBDIVISION                               
FROM POL_HOME_RATING_INFO                             
WHERE POLICY_ID=@POLICY_ID AND   
 POLICY_VERSION_ID=@POLICY_VERSION_ID                            
 AND CUSTOMER_ID=@CUSTOMER_ID AND                           
 DWELLING_ID=@DWELLING_ID                            
              
          
END                          
                    
                        
    
    
  
  
  
  
  
GO

