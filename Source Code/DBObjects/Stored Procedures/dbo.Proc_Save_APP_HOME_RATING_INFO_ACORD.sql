IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_APP_HOME_RATING_INFO_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_APP_HOME_RATING_INFO_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_Save_APP_HOME_RATING_INFO_ACORD                          
Created by      : Pradeep                          
Date            : 8/23/2005                          
Purpose        : Inserting records in APP_HOME_RATING_INFO                          
Revison History :                          
    
Modified By : Ravindra     
Modified On  : 08-17-2006    
Purpose  : Make changes so that Lookup Unique ID for Protection Class is Saved while make App    


Modified By : Praveen Kasana     
Modified On  :1 Dec 2009
Purpose  : Make changes so that Suburban Class Discount is Saved while make App    
    
Used In        : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                  
--                      
CREATE PROCEDURE [dbo].Proc_Save_APP_HOME_RATING_INFO_ACORD                          
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
 @PERSONAL_LIAB_TER_CODE     nvarchar(50),                                            
 @PROT_CLASS     nvarchar(50),                          
 @RATING_METHOD    int =null,                      
 @NO_OF_FAMILIES               smallint, 
 @NEED_OF_UNITS varchar(10)=null  ,                        
 @EXTERIOR_CONSTRUCTION        int,                            
 @EXTERIOR_CONSTRUCTION_CODE  VarChar(50),                              
 @EXTERIOR_OTHER_DESC  varchar(250),                            
 @FOUNDATION               INT  ,                         
 @FOUNDATION_CODE    VarChar(50)  ,                          
 @FOUNDATION_OTHER_DESC     varchar(250),                            
 @ROOF_TYPE             int,                           
 @ROOF_TYPE_CODE             VarChar(50),                         
 @ROOF_OTHER_DESC             varchar(250)  ,                            
 @WIRING_CODE                      VarChar(50),                         
 @PRIMARY_HEAT_TYPE            int,                        
 @PRIMARY_HEAT_TYPE_CODE       VarChar(50),                          
 @SECONDARY_HEAT_TYPE          int,                        
 @SECONDARY_HEAT_TYPE_CODE          VarChar(50),                             
 @MONTH_OCC_EACH_YEAR           smallint,    
 @CIRCUIT_BREAKERS varchar(5),                            
 @PROTECTIVE_DEVICES NVarChar(500),                          
 @TEMPERATURE int,                          
 @SMOKE int,                          
 @BURGLAR int,                         
 @BURGLAR_CODE NVarChar(20),                         
 @FIRE_PLACES  nchar,                          
 @NO_OF_WOOD_STOVES int,      
 @NUM_LOC_ALARMS_APPLIES int,   --for No of alarms                      
 @CENT_ST_BURG_FIRE Char(1),
 @CENT_ST_FIRE Char(1),
 @CENT_ST_BURG Char(1),
 @DIR_FIRE_AND_POLICE Char(1),
 @DIR_FIRE Char(1),
 @DIR_POLICE Char(1),
 @LOC_FIRE_GAS Char(1),
 @TWO_MORE_FIRE Char(1),
 @ALARM_CERT_ATTACHED nvarchar(10)=null,
 @SUBURBAN_CLASS    char(1) = null,
 @LOCATED_IN_SUBDIVISION  nvarchar(20)      
)                          
AS                          
BEGIN                       
               
--Get the LOB_ID from APP_LIST              
Declare @LOB_ID Nvarchar(5)              
            
SELECT  @LOB_ID = APP_LOB                              
FROM APP_LIST                      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
APP_ID = @APP_ID AND                              
APP_VERSION_ID = @APP_VERSION_ID       
--              
--Get the codes from LookUp              
           
EXECUTE @FOUNDATION = Proc_GetLookupID 'FNDCD',@FOUNDATION_CODE                         
EXECUTE @ROOF_TYPE = Proc_GetLookupID 'RFTYP',@ROOF_TYPE_CODE                        
EXECUTE @PRIMARY_HEAT_TYPE = Proc_GetLookupID 'PHEAT',@PRIMARY_HEAT_TYPE_CODE                        
EXECUTE @SECONDARY_HEAT_TYPE = Proc_GetLookupID 'PHEAT',@SECONDARY_HEAT_TYPE_CODE                         
--EXECUTE @WIRING = Proc_GetLookupID 'WIRING',@WIRING_CODE                         
EXECUTE @BURGLAR = Proc_GetLookupID '%BURG',@BURGLAR_CODE            
    
EXECUTE @PROT_CLASS = Proc_GetLookupID 'PRCLS',@PROT_CLASS                         
    
IF EXISTS                        
(                        
 SELECT * FROM APP_HOME_RATING_INFO                        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                        
 APP_ID = @APP_ID AND                        
 APP_VERSION_ID =  @APP_VERSION_ID AND                        
 DWELLING_ID = @DWELLING_ID                          
)                        
BEGIN                        
 UPDATE APP_HOME_RATING_INFO                          
 SET                           
 HYDRANT_DIST=@HYDRANT_DIST  ,                          
 FIRE_STATION_DIST =@FIRE_STATION_DIST    ,                          
 IS_UNDER_CONSTRUCTION =@IS_UNDER_CONSTRUCTION    ,                          
 EXPERIENCE_CREDIT=@EXPERIENCE_CREDIT   ,                          
 IS_AUTO_POL_WITH_CARRIER =@IS_AUTO_POL_WITH_CARRIER   ,                          
 PERSONAL_LIAB_TER_CODE=@PERSONAL_LIAB_TER_CODE     ,                          
 PROT_CLASS   =@PROT_CLASS   ,                          
 RATING_METHOD     =@RATING_METHOD,                      
 NO_OF_FAMILIES =@NO_OF_FAMILIES, 
 NEED_OF_UNITS =@NEED_OF_UNITS ,                               
 EXTERIOR_CONSTRUCTION =@EXTERIOR_CONSTRUCTION,                                   
 EXTERIOR_OTHER_DESC= @EXTERIOR_OTHER_DESC ,                            
 FOUNDATION=@FOUNDATION,                            
 FOUNDATION_OTHER_DESC=@FOUNDATION_OTHER_DESC ,                               
 ROOF_TYPE= @ROOF_TYPE,                                        
 ROOF_OTHER_DESC =@ROOF_OTHER_DESC,                            
 PRIMARY_HEAT_TYPE=@PRIMARY_HEAT_TYPE,                            
 SECONDARY_HEAT_TYPE= @SECONDARY_HEAT_TYPE,                           
 MONTH_OCC_EACH_YEAR= @MONTH_OCC_EACH_YEAR,    
 CIRCUIT_BREAKERS=@CIRCUIT_BREAKERS,                                     
 PROTECTIVE_DEVICES=@PROTECTIVE_DEVICES,                          
 TEMPERATURE=@TEMPERATURE,                          
 SMOKE=@SMOKE,                          
 BURGLAR=@BURGLAR,                          
 FIRE_PLACES=@FIRE_PLACES,                          
 NO_OF_WOOD_STOVES=@NO_OF_WOOD_STOVES,      
 NUM_LOC_ALARMS_APPLIES=@NUM_LOC_ALARMS_APPLIES,                          
     
 CENT_ST_BURG_FIRE = @CENT_ST_BURG_FIRE,                      
 CENT_ST_FIRE = @CENT_ST_FIRE,                      
 CENT_ST_BURG = @CENT_ST_BURG,                      
 DIR_FIRE_AND_POLICE = @DIR_FIRE_AND_POLICE,                      
 DIR_FIRE = @DIR_FIRE,                      
 DIR_POLICE = @DIR_POLICE,                      
 LOC_FIRE_GAS = @LOC_FIRE_GAS,                      
 TWO_MORE_FIRE = @TWO_MORE_FIRE,  
 ALARM_CERT_ATTACHED = @ALARM_CERT_ATTACHED,
 SUBURBAN_CLASS = @SUBURBAN_CLASS,
 LOCATED_IN_SUBDIVISION = @LOCATED_IN_SUBDIVISION
                 
     
 WHERE                           
 CUSTOMER_ID=@CUSTOMER_ID AND                         
 APP_ID=@APP_ID AND                          
 APP_VERSION_ID=@APP_VERSION_ID AND                         
 DWELLING_ID=@DWELLING_ID                          
END                        
ELSE                        
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
 PERSONAL_LIAB_TER_CODE,                          
 PROT_CLASS,                          
 RATING_METHOD,                      
 NO_OF_FAMILIES , 
 NEED_OF_UNITS ,                                      
 EXTERIOR_CONSTRUCTION ,           
 EXTERIOR_OTHER_DESC  ,                            
 FOUNDATION,              
 FOUNDATION_OTHER_DESC ,                               
 ROOF_TYPE ,                                        
 ROOF_OTHER_DESC,                            
 PRIMARY_HEAT_TYPE,                            
 SECONDARY_HEAT_TYPE ,                                    
 MONTH_OCC_EACH_YEAR ,    
 CIRCUIT_BREAKERS, --CIRCUIT BREAKERS    
 PROTECTIVE_DEVICES,                          
 TEMPERATURE,            
 SMOKE,                          
 BURGLAR,                          
 FIRE_PLACES,                          
 NO_OF_WOOD_STOVES,      
 NUM_LOC_ALARMS_APPLIES,                          
 CENT_ST_BURG_FIRE,                      
 CENT_ST_FIRE,                      
 CENT_ST_BURG,                      
 DIR_FIRE_AND_POLICE,                      
 DIR_FIRE,                      
 DIR_POLICE,                      
 LOC_FIRE_GAS,                      
 TWO_MORE_FIRE,  
 ALARM_CERT_ATTACHED,
 SUBURBAN_CLASS,
 LOCATED_IN_SUBDIVISION
 )                          
 VALUES              
 (                          
 @CUSTOMER_ID,                          
 @APP_ID,                          
 @APP_VERSION_ID,                          
 @DWELLING_ID,                          
 @HYDRANT_DIST,                          
 @FIRE_STATION_DIST,                          
 @IS_UNDER_CONSTRUCTION,                          
 @EXPERIENCE_CREDIT,                          
 @IS_AUTO_POL_WITH_CARRIER,                          
 @PERSONAL_LIAB_TER_CODE,               
 @PROT_CLASS,                          
 @RATING_METHOD,                      
 @NO_OF_FAMILIES ,
 @NEED_OF_UNITS ,                                       
 @EXTERIOR_CONSTRUCTION ,                                   
 @EXTERIOR_OTHER_DESC  ,                            
 @FOUNDATION,                            
 @FOUNDATION_OTHER_DESC,                               
 @ROOF_TYPE ,                                        
 @ROOF_OTHER_DESC,                            
 @PRIMARY_HEAT_TYPE,                            
 @SECONDARY_HEAT_TYPE ,                        
 @MONTH_OCC_EACH_YEAR ,     
 @CIRCUIT_BREAKERS,    
 @PROTECTIVE_DEVICES,                          
 @TEMPERATURE,                          
 @SMOKE,                          
 @BURGLAR,                          
 @FIRE_PLACES,                          
 @NO_OF_WOOD_STOVES,      
 @NUM_LOC_ALARMS_APPLIES,                          
 @CENT_ST_BURG_FIRE,                      
 @CENT_ST_FIRE,                      
 @CENT_ST_BURG,                      
 @DIR_FIRE_AND_POLICE,                      
 @DIR_FIRE,                      
 @DIR_POLICE,                      
 @LOC_FIRE_GAS,                      
 @TWO_MORE_FIRE,  
 @ALARM_CERT_ATTACHED,
 @SUBURBAN_CLASS,
 @LOCATED_IN_SUBDIVISION                                    
 )             
END                        
 RETURN 1                        
END                      




GO

