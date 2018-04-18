IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcUM_InsertPolicyRatingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcUM_InsertPolicyRatingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.ProcUM_InsertPolicyRatingInfo            
Created by      : Ravindra
Date            : 03-22-2006
Purpose         : To insert record IN POL_UMBRELLA_RATING_INFO        
Revison History :       
Used In         :   Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   
--drop proc ProcUM_InsertPolicyRatingInfo
CREATE PROC Dbo.ProcUM_InsertPolicyRatingInfo                       
(                        
 @CUSTOMER_ID     int,                        
 @POLICY_ID     int,                        
 @POLICY_VERSION_ID     smallint,                        
 @DWELLING_ID     smallint,                        
 @HYDRANT_DIST     real,                        
 @FIRE_STATION_DIST     real,                        
 @IS_UNDER_CONSTRUCTION     char(1),                        
 @EXPERIENCE_CREDIT     char(1),                        
 @IS_AUTO_POL_WITH_CARRIER     char(1),                        
 @PROT_CLASS   nvarchar(100),                        
 @WIRING_RENOVATION     int,                        
 @WIRING_UPDATE_YEAR     smallint,                        
 @PLUMBING_RENOVATION     int,                        
 @PLUMBING_UPDATE_YEAR     smallint,                        
 @HEATING_RENOVATION     int,                        
 @HEATING_UPDATE_YEAR     smallint,                        
 @ROOFING_RENOVATION     int,                        
 @ROOFING_UPDATE_YEAR     smallint,                        
 @NO_OF_AMPS     smallint,                        
 @CIRCUIT_BREAKERS     nvarchar(5) ,                      
 @NO_OF_FAMILIES               smallint,       
 @CONSTRUCTION_CODE             nvarchar(250),             
 @EXTERIOR_CONSTRUCTION        int,                        
 @EXTERIOR_OTHER_DESC  varchar(250),                        
 @FOUNDATION               INT  ,                        
 @FOUNDATION_OTHER_DESC     varchar(250),                        
 @ROOF_TYPE             int,                        
 @ROOF_OTHER_DESC             varchar(250)  ,                        
 @PRIMARY_HEAT_TYPE            int,                        
 @SECONDARY_HEAT_TYPE          int,                        
 @MONTH_OCC_EACH_YEAR           smallint,                        
 @PROTECTIVE_DEVICES NVarChar(500),                        
 @TEMPERATURE NVarChar(100),                        
 @SMOKE NVarChar(100),                        
 @BURGLAR NVarChar(100),                        
 @FIRE_PLACES  nchar,                        
 @NO_OF_WOOD_STOVES int,           
 @PRIMARY_HEAT_OTHER_DESC nvarchar(250),                       
 @SECONDARY_HEAT_OTHER_DESC nvarchar(250),                    
 @DWELLING_CONST_DATE datetime,                  
 @CENT_ST_BURG_FIRE Char(1),                  
 @CENT_ST_FIRE Char(1),                  
 @CENT_ST_BURG Char(1),                  
 @DIR_FIRE_AND_POLICE Char(1),                  
 @DIR_FIRE Char(1),                  
 @DIR_POLICE Char(1),                  
 @LOC_FIRE_GAS Char(1),                  
 @TWO_MORE_FIRE Char(1),  
 @NUM_LOC_ALARMS_APPLIES int=null                       
)                        
AS                        
BEGIN                        
INSERT INTO POL_UMBRELLA_RATING_INFO                        
(                        
 CUSTOMER_ID,                        
 POLICY_ID,                        
 POLICY_VERSION_ID,                        
 DWELLING_ID,                        
 HYDRANT_DIST,                    
 FIRE_STATION_DIST,                        
 IS_UNDER_CONSTRUCTION,                        
 EXPERIENCE_CREDIT,                        
 IS_AUTO_POL_WITH_CARRIER,                        
 PROT_CLASS,                        
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
 NO_OF_FAMILIES ,       
 CONSTRUCTION_CODE,                                   
 EXTERIOR_CONSTRUCTION ,                               
 EXTERIOR_OTHER_DESC  ,                        
 FOUNDATION,                        
 FOUNDATION_OTHER_DESC ,                           
 ROOF_TYPE ,                                    
 ROOF_OTHER_DESC,                  
 PRIMARY_HEAT_TYPE,                        
 SECONDARY_HEAT_TYPE ,                                
 MONTH_OCC_EACH_YEAR ,                                 
 PROTECTIVE_DEVICES,                        
 TEMPERATURE,                        
 SMOKE,                        
 BURGLAR,                        
 FIRE_PLACES,                        
 NO_OF_WOOD_STOVES,                        
 PRIMARY_HEAT_OTHER_DESC,                            
 SECONDARY_HEAT_OTHER_DESC,                    
 DWELLING_CONST_DATE ,                  
 CENT_ST_BURG_FIRE,                  
 CENT_ST_FIRE,                  
 CENT_ST_BURG,                  
 DIR_FIRE_AND_POLICE,                  
 DIR_FIRE,                  
 DIR_POLICE,                  
 LOC_FIRE_GAS,                  
 TWO_MORE_FIRE,  
 NUM_LOC_ALARMS_APPLIES                                   
)                        
VALUES                        
(                        
 @CUSTOMER_ID,                        
 @POLICY_ID,                        
 @POLICY_VERSION_ID,                     
 @DWELLING_ID,                        
 @HYDRANT_DIST,
 @FIRE_STATION_DIST,                        
 @IS_UNDER_CONSTRUCTION,                        
 @EXPERIENCE_CREDIT,                        
 @IS_AUTO_POL_WITH_CARRIER,                        
 @PROT_CLASS,                        
 @WIRING_RENOVATION,                        
 @WIRING_UPDATE_YEAR,                        
 @PLUMBING_RENOVATION,                        
 @PLUMBING_UPDATE_YEAR,                        
 @HEATING_RENOVATION,                        
 @HEATING_UPDATE_YEAR,                        
 @ROOFING_RENOVATION,                        
 @ROOFING_UPDATE_YEAR,                        
 @NO_OF_AMPS,       
 @CIRCUIT_BREAKERS,                      
 @NO_OF_FAMILIES ,         
 @CONSTRUCTION_CODE,                                 
 @EXTERIOR_CONSTRUCTION ,                               
 @EXTERIOR_OTHER_DESC  ,                        
 @FOUNDATION,                        
 @FOUNDATION_OTHER_DESC,                           
 @ROOF_TYPE ,                                    
 @ROOF_OTHER_DESC,                        
 @PRIMARY_HEAT_TYPE,                        
 @SECONDARY_HEAT_TYPE ,      
 @MONTH_OCC_EACH_YEAR ,                                 
 @PROTECTIVE_DEVICES,                        
 @TEMPERATURE,                        
 @SMOKE,                        
 @BURGLAR,                        
 @FIRE_PLACES,                        
 @NO_OF_WOOD_STOVES,                        
 @PRIMARY_HEAT_OTHER_DESC,                            
 @SECONDARY_HEAT_OTHER_DESC,                    
 @DWELLING_CONST_DATE  ,                  
 @CENT_ST_BURG_FIRE,                  
 @CENT_ST_FIRE,                  
 @CENT_ST_BURG,                  
 @DIR_FIRE_AND_POLICE,                  
 @DIR_FIRE,                  
 @DIR_POLICE,                  
 @LOC_FIRE_GAS,                  
 @TWO_MORE_FIRE,                        
 @NUM_LOC_ALARMS_APPLIES  
)                        
END                    
                    
  



GO

