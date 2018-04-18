IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- drop proc dbo.Proc_GetWatercraftInfo  
CREATE    PROC dbo.Proc_GetWatercraftInfo                                    
 (                                    
 @CUSTOMERID  int,                                    
 @APPID  int,                                    
 @APPVERSIONID int,                                    
 @BOAT_ID  int                                    
                                     
 )                                    
 AS                                    
 BEGIN                                    
                                     
                                     
 SELECT                                      
 BOAT_NO,                                    
 --BOAT_NAME,                                    
isnull(BOAT_NAME,'') BOAT_NAME,                          
 YEAR,                                    
 MAKE,                                    
 MODEL,                                    
HULL_ID_NO ,                                    
-- HULL_ID_NO as SERIAL_NO,                                
 STATE_REG,                                    
 HULL_MATERIAL,                                    
 FUEL_TYPE,                                    
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                                    
--isnull(convert(varchar,DATE_PURCHASED),'') DATE_PURCHASED,                                    
 LENGTH,                                    
--floor(MAX_SPEED) MAX_SPEED,                                    
--isnull(convert(varchar(10),MAX_SPEED),'') MAX_SPEED,                          
--isnull(MAX_SPEED,0) as MAX_SPEED,                          
MAX_SPEED,              
                          
 BERTH_LOC,                                    
 WATERS_NAVIGATED,                                    
 TERRITORY,                                    
 TYPE_OF_WATERCRAFT ,                                    
 floor(INSURING_VALUE) INSURING_VALUE,                                    
--floor(DEDUCTIBLE) DEDUCTIBLE,           
-- WATERCRAFT_HORSE_POWER,                                    
isnull(convert(varchar(10),WATERCRAFT_HORSE_POWER),'') WATERCRAFT_HORSE_POWER,                          
 BOAT_ID,                                    
 TWIN_SINGLE,                                  
 DESC_OTHER_WATERCRAFT ,             
 ISNULL(APP_WATERCRAFT_INFO.IS_ACTIVE, 'Y') AS IS_ACTIVE    ,                             
 INCHES,        
 LORAN_NAV_SYSTEM,        
 DIESEL_ENGINE,        
 SHORE_STATION,        
 HALON_FIRE_EXT_SYSTEM,        
 DUAL_OWNERSHIP,  
 REMOVE_SAILBOAT,  
 COV_TYPE_BASIS,  
 PHOTO_ATTACHED,  
 MARINE_SURVEY,  
 convert(varchar,DATE_MARINE_SURVEY,101) DATE_MARINE_SURVEY  
 --Added by RPSINGH - 12 May 2006  
 ,LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH  
, LOSSREPORT_ORDER, CONVERT(VARCHAR,LOSSREPORT_DATETIME, 101) AS LOSSREPORT_DATETIME
 FROM        app_watercraft_info                                        
 WHERE     (CUSTOMER_ID = @CUSTOMERID)   and (APP_ID=@APPID) AND (APP_VERSION_ID=@APPVERSIONID) AND (BOAT_ID= @BOAT_ID);                                    
 END                     
  
  
  
  
  



GO

