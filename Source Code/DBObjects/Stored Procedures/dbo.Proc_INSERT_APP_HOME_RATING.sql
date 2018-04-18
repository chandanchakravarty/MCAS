IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_INSERT_APP_HOME_RATING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_INSERT_APP_HOME_RATING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_INSERT_APP_HOME_RATING                        
Created by      : Pradeep                      
Date            : 5/13/2005                        
Purpose       :Inserting records in APP_HOME_RATING_INFO                        
Revison History :                        
Used In        : Wolverine                      
                    
Modified BY : Mohit Gupta                    
Modified On : 23/09/2005                    
Purpose  : Adding column DWELLING_CONST_DATE                
              
Modified By : Mohit              
Modified On : 4/11/2005              
Purpose     : Commenting fields ADD_COVERAGE_INFO,IS_OUTSIDE_STAIR as no more on the page.          
          
Modified By :  Mohit          
Modified On : 10/11/2005          
Purpose     : commenting(BASMT_SQR_FOOTAGE,BREEZE_SQR_FOOTAGE,GARAGE_SQR_FOOTAGE,TOT_SQR_FOOTAGE)                      
Modified By :  Sumit Chhabra        
Modified On : 11/11/2005          
Purpose     : Commented the fields rating method and personal territory        
Modified By :  Sumit Chhabra        
Modified On : 16/01/2006          
Purpose     : Added the field NUM_LOC_ALARMS_APPLIES  
Modified By : Sumit Chhabra        
Modified On : 05/04/2006          
Purpose     : Added the field SPRINKER

  
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/    

CREATE PROC dbo.Proc_INSERT_APP_HOME_RATING                        
(                        
 @CUSTOMER_ID     int,                        
 @APP_ID     int,                        
 @APP_VERSION_ID     smallint,                        
 @DWELLING_ID     smallint,                        
 @HYDRANT_DIST     real,                        
 @FIRE_STATION_DIST     real,                        
 @IS_UNDER_CONSTRUCTION     char(1),                        
 @EXPERIENCE_CREDIT     char(1),                        
 @IS_AUTO_POL_WITH_CARRIER     char(1),                        
-- @PERSONAL_LIAB_TER_CODE     nvarchar(100),                        
 @PROT_CLASS     nvarchar(100),                        
-- @RATING_METHOD    int =null ,                      
 --@TOT_SQR_FOOTAGE     real,                        
 --@GARAGE_SQR_FOOTAGE     real,                        
 --@BREEZE_SQR_FOOTAGE     real,                        
 --@BASMT_SQR_FOOTAGE     real,                        
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
 --@NO_OF_APARTMENTS             smallint,                        
  @NO_OF_FAMILIES               smallint,       
  @CONSTRUCTION_CODE             nvarchar(250),             
  @EXTERIOR_CONSTRUCTION        int,                        
  @EXTERIOR_OTHER_DESC  varchar(250),                        
  @FOUNDATION               INT  ,                        
  @FOUNDATION_OTHER_DESC     varchar(250),                        
  @ROOF_TYPE             int,                        
  @ROOF_OTHER_DESC             varchar(250)  ,               
 -- @WIRING                      INT  ,                        
  --@WIRING_LAST_INSPECTED         datetime,                        
  @PRIMARY_HEAT_TYPE            int,                        
  @SECONDARY_HEAT_TYPE     int,    
  @MONTH_OCC_EACH_YEAR           smallint,                        
  --@ADD_COVERAGE_INFO            varchar(250),                        
 -- @IS_OUTSIDE_STAIR             char(1),                      
  @PROTECTIVE_DEVICES NVarChar(500),                        
  @TEMPERATURE NVarChar(100),                        
  @SMOKE NVarChar(100),                        
  @BURGLAR NVarChar(100),                        
  @FIRE_PLACES  nchar,                        
  @NO_OF_WOOD_STOVES int,           
  --@SWIMMING_POOL nchar,                        
  --@SWIMMING_POOL_TYPE int        ,                    
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
@NUM_LOC_ALARMS_APPLIES int=null,
@SPRINKER SMALLINT,
@ALARM_CERT_ATTACHED NVARCHAR(60),
@IS_SUPERVISED char(1),
@NEED_OF_UNITS varchar(10)=null,
@SUBURBAN_CLASS char(1)=null,
@LOCATED_IN_SUBDIVISION NVARCHAR(40)=null                      
)                        
AS                        
BEGIN                        
INSERT INTO APP_HOME_RATING_INFO                        
(                        
 CUSTOMER_ID,                        
 APP_ID,                        
 APP_VERSION_ID,                        
 DWELLING_ID,                        
 HYDRANT_DIST,                    
 FIRE_STATION_DIST,                        
 IS_UNDER_CONSTRUCTION,                        
 EXPERIENCE_CREDIT,                        
 IS_AUTO_POL_WITH_CARRIER,                        
-- PERSONAL_LIAB_TER_CODE,                        
 PROT_CLASS,                        
-- RATING_METHOD,                      
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
-- NO_OF_APARTMENTS ,                                  
   NO_OF_FAMILIES ,       
   CONSTRUCTION_CODE,                                   
   EXTERIOR_CONSTRUCTION ,                               
   EXTERIOR_OTHER_DESC  ,                        
   FOUNDATION,                        
   FOUNDATION_OTHER_DESC ,                           
   ROOF_TYPE ,                                    
   ROOF_OTHER_DESC,                        
--   WIRING ,                                         
--   WIRING_LAST_INSPECTED ,                        
   PRIMARY_HEAT_TYPE,                        
   SECONDARY_HEAT_TYPE ,                                
   MONTH_OCC_EACH_YEAR ,                                 
   --ADD_COVERAGE_INFO ,                                 
   --IS_OUTSIDE_STAIR  ,                      
 PROTECTIVE_DEVICES,                        
 TEMPERATURE,                        
 SMOKE,                        
 BURGLAR,                        
 FIRE_PLACES,                        
 NO_OF_WOOD_STOVES,                        
-- SWIMMING_POOL,                        
-- SWIMMING_POOL_TYPE,                    
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
NUM_LOC_ALARMS_APPLIES,
SPRINKER,
ALARM_CERT_ATTACHED,
IS_SUPERVISED,
NEED_OF_UNITS,
SUBURBAN_CLASS,
LOCATED_IN_SUBDIVISION
)                        
VALUES                        
(                        
 @CUSTOMER_ID,                        
 @APP_ID,                        
 @APP_VERSION_ID,                     
 @DWELLING_ID,                        
 @HYDRANT_DIST,                         @FIRE_STATION_DIST,                        
 @IS_UNDER_CONSTRUCTION,                        
 @EXPERIENCE_CREDIT,                        
 @IS_AUTO_POL_WITH_CARRIER,                        
-- @PERSONAL_LIAB_TER_CODE,                        
 @PROT_CLASS,                        
-- @RATING_METHOD,                      
 --@TOT_SQR_FOOTAGE,                        
 --@GARAGE_SQR_FOOTAGE,                        
 --@BREEZE_SQR_FOOTAGE,                        
-- @BASMT_SQR_FOOTAGE,                        
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
-- @NO_OF_APARTMENTS,                      
   @NO_OF_FAMILIES ,         
   @CONSTRUCTION_CODE,                                 
   @EXTERIOR_CONSTRUCTION ,                               
   @EXTERIOR_OTHER_DESC  ,                        
   @FOUNDATION,                        
   @FOUNDATION_OTHER_DESC,                           
   @ROOF_TYPE ,                                    
   @ROOF_OTHER_DESC,                        
--   @WIRING ,                                         
--   @WIRING_LAST_INSPECTED ,                        
   @PRIMARY_HEAT_TYPE,                        
   @SECONDARY_HEAT_TYPE ,                           @MONTH_OCC_EACH_YEAR ,                                 
   --@ADD_COVERAGE_INFO ,                                 
   --@IS_OUTSIDE_STAIR,                      
 @PROTECTIVE_DEVICES,                        
 @TEMPERATURE,                        
 @SMOKE,                        
 @BURGLAR,                        
 @FIRE_PLACES,                        
 @NO_OF_WOOD_STOVES,                        
 --@SWIMMING_POOL,                        
 --@SWIMMING_POOL_TYPE ,                     
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
@NUM_LOC_ALARMS_APPLIES,
@SPRINKER,
@ALARM_CERT_ATTACHED  ,
@IS_SUPERVISED,
@NEED_OF_UNITS,
@SUBURBAN_CLASS,
@LOCATED_IN_SUBDIVISION
)                        
END                    
                    
                    
                  
                
              
            
          
        
      
    
  













GO

