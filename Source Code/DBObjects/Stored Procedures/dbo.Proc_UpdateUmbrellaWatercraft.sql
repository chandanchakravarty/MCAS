IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_UpdateUmbrellaWatercraft                          
Created by       : Nidhi                          
Date               :17/05/2005                          
Purpose         : To update  record in watercraft information   table                          
Revison History :                          
Used In         :   Wolverine                          
                        
Modified by    : Mohit                        
Modified on    : 15/11/2005                        
Purpose        : Adding field DESC_OTHER_WATERCRAFT                        
                  
Modified by    : Sumit Chhabra                  
Modified on    : 09/11/2006                        
Purpose        : Adding fields LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,DUAL_OWNERSHIP                  
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
-- drop proc dbo.Proc_UpdateUmbrellaWatercraft                          
create PROC dbo.Proc_UpdateUmbrellaWatercraft                          
(                          
 @CUSTOMER_ID     int,                          
@APP_ID     int,                          
@APP_VERSION_ID     smallint,                          
@BOAT_ID     smallint output,                          
@BOAT_NO     int,                          
@BOAT_NAME     nvarchar(50),                          
@YEAR     int,                          
@MAKE     nvarchar(150),                          
@MODEL     nvarchar(150),                          
@HULL_ID_NO     nvarchar(150),                          
@STATE_REG     nvarchar(10),                          
@REG_NO     nvarchar(50),                          
@POWER     nvarchar(10),                          
@HULL_TYPE     int,                          
@HULL_MATERIAL     int,                          
@FUEL_TYPE     int,                          
@HULL_DESIGN     int,                          
@DATE_PURCHASED     datetime =null,                          
@LENGTH     nvarchar(20),                          
@MAX_SPEED     decimal(10,2),                          
@COST_NEW     decimal(12,2),                          
@PRESENT_VALUE     decimal(12,2),                          
@BERTH_LOC     nvarchar(200),                          
@WATERS_NAVIGATED     varchar(250),                          
@TERRITORY     nvarchar(50),                          
 @MODIFIED_BY int,                          
 @LAST_UPDATED_DATETIME datetime,                          
@TYPE_OF_WATERCRAFT nchar(10),                          
@INSURING_VALUE decimal(10,2),                          
--@DEDUCTIBLE decimal(10,2),                       
@WATERCRAFT_HORSE_POWER int,                        
@DESC_OTHER_WATERCRAFT varchar(150),                  
@LORAN_NAV_SYSTEM smallint,                  
@DIESEL_ENGINE smallint,                  
@SHORE_STATION smallint,                  
@HALON_FIRE_EXT_SYSTEM smallint,                  
@DUAL_OWNERSHIP smallint,            
@REMOVE_SAILBOAT smallint,        
@INCHES  nvarchar(20),                  
@TWIN_SINGLE int,    
@LOCATION_ADDRESS varchar(200),    
@LOCATION_CITY varchar(50),    
@LOCATION_STATE varchar(50),    
@LOCATION_ZIP varchar(20),    
@COV_TYPE_BASIS int,                          
@USED_PARTICIPATE smallint,  
@WATERCRAFT_CONTEST varchar(250),
@OTHER_POLICY nvarchar(150) = null,
@IS_BOAT_EXCLUDED smallint = nuill       
)                          
AS                          
                          
BEGIN             

 IF EXISTS(SELECT CUSTOMER_ID  FROM APP_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                            
		  APP_ID = @APP_ID AND                            
		  APP_VERSION_ID = @APP_VERSION_ID          
		  AND BOAT_NO=@BOAT_NO AND BOAT_ID<>@BOAT_ID)
	return -2               
  
                          
UPDATE APP_UMBRELLA_WATERCRAFT_INFO                          
        SET                           
 BOAT_NO =@BOAT_NO,                          
 BOAT_NAME=@BOAT_NAME,                          
 YEAR=@YEAR,                          
 MAKE=@MAKE,                          
 MODEL=@MODEL,                          
 HULL_ID_NO=@HULL_ID_NO,           
 STATE_REG=@STATE_REG,                          
 REG_NO=@REG_NO,             
 POWER=@POWER,                          
 HULL_TYPE=@HULL_TYPE,                          
 HULL_MATERIAL=@HULL_MATERIAL,                      
 FUEL_TYPE=@FUEL_TYPE,                          
 HULL_DESIGN=@HULL_DESIGN,                          
 DATE_PURCHASED=@DATE_PURCHASED,                          
 LENGTH=@LENGTH,           
 MAX_SPEED=@MAX_SPEED,                          
 COST_NEW=@COST_NEW,                          
 PRESENT_VALUE=@PRESENT_VALUE,         
 BERTH_LOC=@BERTH_LOC,                          
 WATERS_NAVIGATED=@WATERS_NAVIGATED,                          
 TERRITORY=@TERRITORY,                          
 MODIFIED_BY=@MODIFIED_BY,                          
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,                  TYPE_OF_WATERCRAFT=@TYPE_OF_WATERCRAFT ,                          
INSURING_VALUE =@INSURING_VALUE ,                          
--DEDUCTIBLE =@DEDUCTIBLE ,                      
WATERCRAFT_HORSE_POWER =@WATERCRAFT_HORSE_POWER,                        
DESC_OTHER_WATERCRAFT=@DESC_OTHER_WATERCRAFT,                  
LORAN_NAV_SYSTEM=@LORAN_NAV_SYSTEM,                  
DIESEL_ENGINE=@DIESEL_ENGINE,                  
SHORE_STATION=@SHORE_STATION,                  
HALON_FIRE_EXT_SYSTEM=@HALON_FIRE_EXT_SYSTEM,                  
DUAL_OWNERSHIP=@DUAL_OWNERSHIP,            
REMOVE_SAILBOAT=@REMOVE_SAILBOAT,        
INCHES = @INCHES,        
TWIN_SINGLE = @TWIN_SINGLE,    
LOCATION_ADDRESS = @LOCATION_ADDRESS,    
LOCATION_CITY=@LOCATION_CITY,    
LOCATION_STATE=@LOCATION_STATE,    
LOCATION_ZIP=@LOCATION_ZIP,    
COV_TYPE_BASIS=@COV_TYPE_BASIS,  
USED_PARTICIPATE = @USED_PARTICIPATE,  
WATERCRAFT_CONTEST = @WATERCRAFT_CONTEST,
OTHER_POLICY  = @OTHER_POLICY,
IS_BOAT_EXCLUDED = @IS_BOAT_EXCLUDED
    
        
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                          
 APP_ID = @APP_ID AND                           
 APP_VERSION_ID = @APP_VERSION_ID AND                          
 BOAT_ID= @BOAT_ID                           
                      
--DELETE FROM WATERCRAFT ENGINE INFO IF TYPE OF WATERCRAFT HAS BEEN CHANGED FROM OUTBOARD BOAT                      
  /*11489>Bass boat (w/Motor)          
  11369>Outboard Boat          
  11487>Outboard (w/Motor)          
  11374>Pontoon (w/Motor)      
  11672>Sailboat w/outboard*/          
          
/*  if(@type_of_watercraft<>11369 and @type_of_watercraft<>11489 and @type_of_watercraft<>11487 and @type_of_watercraft<>11374 and @type_of_watercraft<>11672)      
  delete from APP_WATERCRAFT_ENGINE_INFO                       
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
    APP_ID = @APP_ID AND                               
    APP_VERSION_ID = @APP_VERSION_ID AND                              
    ASSOCIATED_BOAT= @BOAT_ID                       */
return 1

END                          
                        
                      
                      
                            
                          
                        
                      
                    
                  
                  
                  
                
              
            
          
        
      
    
    
    
    
    
    
    
  





GO

