IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcUM_GetPolicyRatingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcUM_GetPolicyRatingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------            
Proc Name       : dbo.ProcUM_GetPolicyRatingInfo            
Created by      : Ravindra
Date            : 03-22-2006
Purpose         : To fetch rating details
Revison History :       
Used In         :   Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/ 

CREATE PROC Dbo.ProcUM_GetPolicyRatingInfo                    
(                    
 @CUSTOMER_ID int,                    
 @POLICY_ID int,                    
 @POLICY_VERSION_ID smallint,                    
 @DWELLING_ID  smallint                    
)                    
AS                    
BEGIN                    
                    
SELECT                       
 HYDRANT_DIST,                      
 FIRE_STATION_DIST,                      
 IS_UNDER_CONSTRUCTION,                      
 EXPERIENCE_CREDIT,                      
 IS_AUTO_POL_WITH_CARRIER,                      
 PROT_CLASS,                      
 NO_OF_FAMILIES,      
 CONSTRUCTION_CODE,                  
 EXTERIOR_CONSTRUCTION,                    
 EXTERIOR_OTHER_DESC,                    
 FOUNDATION,                    
 FOUNDATION_OTHER_DESC,                    
 ROOF_TYPE,                     
 ROOF_OTHER_DESC,                  
 PRIMARY_HEAT_TYPE,                    
 SECONDARY_HEAT_TYPE,                    
 MONTH_OCC_EACH_YEAR,ADD_COVERAGE_INFO,                    
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
 PROTECTIVE_DEVICES,                    
 TEMPERATURE,SMOKE,BURGLAR,                      
 FIRE_PLACES,NO_OF_WOOD_STOVES,                    
 PRIMARY_HEAT_OTHER_DESC,                  
 SECONDARY_HEAT_OTHER_DESC,
 Convert(varchar,DWELLING_CONST_DATE,101)  as DWELLING_CONST_DATE ,                
 CENT_ST_BURG_FIRE,                
 CENT_ST_FIRE,                
 CENT_ST_BURG,                
 DIR_FIRE_AND_POLICE,                
 DIR_FIRE,                
 DIR_POLICE,                
 LOC_FIRE_GAS,                
 TWO_MORE_FIRE,  
 NUM_LOC_ALARMS_APPLIES                           
  
 FROM POL_UMBRELLA_RATING_INFO                       
 WHERE POLICY_ID=@POLICY_ID AND                 
 POLICY_VERSION_ID=@POLICY_VERSION_ID                      
 AND CUSTOMER_ID=@CUSTOMER_ID AND                     
 DWELLING_ID=@DWELLING_ID                      
                    
                    
END                    
  



GO

