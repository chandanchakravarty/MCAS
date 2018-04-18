IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaWatercraftInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaWatercraftInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                            
Proc Name          : Dbo.Proc_GetUmbrellaWatercraftInfo                            
Created by           : Nidhi                            
Date                    : 17/05/2005                            
Purpose               :                             
Revison History :                            
Used In                :   Wolverine                            
                            
Modified By  : Anurag Verma                            
Modified On  : 21/07/2005                            
Purpose   : Retrieving boat_id in query                            
                            
Modified By  : Anurag Verma                            
Modified On  : 21/07/2005                            
Purpose   : applying floor funtions on max_speed and insuring_value                          
                          
Modified by    : Mohit                          
Modified on    : 15/11/2005                          
Purpose        : Adding field DESC_OTHER_WATERCRAFT                             
            
Modified by    : Sumit Chhabra            
Modified on    : 09/01/2006                         
Purpose        : Adding fields LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,DUAL_OWNERSHIP            
      
Modified by    : Shafi        
Modified on    : 01/16/2006      
Purpose        : Floor The Max Speed      
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                            
--drop proc dbo.Proc_GetUmbrellaWatercraftInfo      
CREATE    PROC dbo.Proc_GetUmbrellaWatercraftInfo                            
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
BOAT_NAME,                            
YEAR,                            
MAKE,                            
MODEL,                            
--HULL_ID_NO as SERIAL_NO,                            
HULL_ID_NO,                      
STATE_REG,                            
REG_NO,                            
POWER,                            
HULL_TYPE,                            
HULL_MATERIAL,                            
FUEL_TYPE,                            
HULL_DESIGN,                            
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                            
LENGTH,                            
floor(MAX_SPEED) MAX_SPEED,                          
--MAX_SPEED,                    
COST_NEW,                            
PRESENT_VALUE,                            
BERTH_LOC,                            
WATERS_NAVIGATED,                            
TERRITORY,                            
TYPE_OF_WATERCRAFT ,                            
floor(INSURING_VALUE) INSURING_VALUE,                            
--floor(DEDUCTIBLE) DEDUCTIBLE,               
WATERCRAFT_HORSE_POWER,                            
BOAT_ID,                          
DESC_OTHER_WATERCRAFT  ,                
ISNULL(app_umbrella_watercraft_info.IS_ACTIVE, 'Y') AS IS_ACTIVE,            
LORAN_NAV_SYSTEM,            
DIESEL_ENGINE,            
SHORE_STATION,            
HALON_FIRE_EXT_SYSTEM,            
DUAL_OWNERSHIP,    
REMOVE_SAILBOAT,  
INCHES,  
TWIN_SINGLE,LOCATION_ADDRESS,LOCATION_CITY,LOCATION_STATE,LOCATION_ZIP,COV_TYPE_BASIS,
USED_PARTICIPATE,
WATERCRAFT_CONTEST,
ISNULL(IS_BOAT_EXCLUDED,'') AS IS_BOAT_EXCLUDED,
ISNULL(OTHER_POLICY,'') AS OTHER_POLICY
FROM        app_umbrella_watercraft_info              
WHERE     (CUSTOMER_ID = @CUSTOMERID)   and (APP_ID=@APPID) AND (APP_VERSION_ID=@APPVERSIONID) AND (BOAT_ID= @BOAT_ID);                            
END                  
  
  
  
  





GO

