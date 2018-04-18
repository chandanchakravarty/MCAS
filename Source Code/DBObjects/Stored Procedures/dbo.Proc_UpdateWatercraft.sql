IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                  
Proc Name       : dbo.Proc_UpdateWatercraft                                  
Created by       : Nidhi                                  
Date               :17/05/2005                                  
Purpose         : To update  record in watercraft information   table                                  
Revison History :                                  
Used In         :   Wolverine                                  
                                  
Modified By : Anurag verma                                  
Modified On : 18/10/2005                                  
Purpose  : Applying unique boat number check                                  
                                
Modified by    : Mohit                                
Modified on    : 15/11/2005                                
Purpose        : Adding field DESC_OTHER_WATERCRAFT                        
                  
Modified by    : Sumit Chhabra                  
Modified on    : 09/01/2006                               
Purpose        : Adding fields LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM and DUAL_OWNERSHIP                   
      
Modified by    : Raman Pal Singh      
Modified on    : 12 May 2006      
Purpose        : Adding fields LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP,       
   LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH      
                           
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
--drop proc dbo.Proc_UpdateWatercraft        
CREATE    PROC dbo.Proc_UpdateWatercraft                                  
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
@HULL_MATERIAL     int,                                  
@FUEL_TYPE     int,                                  
                                  
@DATE_PURCHASED     datetime =null,                                  
@LENGTH     nvarchar(20),                                  
@MAX_SPEED     decimal(10,0),                                  
@BERTH_LOC     nvarchar(200),                                  
@WATERS_NAVIGATED     varchar(250),                                  
@TERRITORY     nvarchar(50),                                  
 @MODIFIED_BY int,                                  
 @LAST_UPDATED_DATETIME datetime,                                  
@TYPE_OF_WATERCRAFT nchar(10),                                  
@INSURING_VALUE decimal(10,2),                      
--@DEDUCTIBLE decimal(10,2),                                   
@WATERCRAFT_HORSE_POWER decimal(10,2),                                  
@TWIN_SINGLE int,                                
@DESC_OTHER_WATERCRAFT varchar(150) ,                              
@INCHES  nvarchar(20),                  
@LORAN_NAV_SYSTEM smallint,                  
@DIESEL_ENGINE smallint,                  
@SHORE_STATION smallint,                  
@HALON_FIRE_EXT_SYSTEM smallint,                  
@DUAL_OWNERSHIP smallint,            
@REMOVE_SAILBOAT smallint,        
@COV_TYPE_BASIS int,        
@PHOTO_ATTACHED int =NULL,      
@MARINE_SURVEY int = NULL,      
@DATE_MARINE_SURVEY datetime = NULL,      
@LOCATION_ADDRESS VARCHAR(200) = NULL,      
@LOCATION_CITY VARCHAR(50) = NULL,      
@LOCATION_STATE VARCHAR(50) = NULL,      
@LOCATION_ZIP VARCHAR(20) = NULL,      
@LAY_UP_PERIOD_FROM_DAY INTEGER = NULL,      
@LAY_UP_PERIOD_FROM_MONTH INTEGER = NULL,      
@LAY_UP_PERIOD_TO_DAY INTEGER = NULL,      
@LAY_UP_PERIOD_TO_MONTH INTEGER = NULL,
@LOSSREPORT_ORDER int = null, 
@LOSSREPORT_DATETIME DateTime = null                       
      
)                                  
AS                                  
                                  
BEGIN                                  
                                  
    
                                  
if exists(select customer_id  from APP_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                  
    APP_ID = @APP_ID AND                                  
    APP_VERSION_ID = @APP_VERSION_ID                                  
    and boat_no=@BOAT_NO and boat_id<>@boat_id)    
 return -2                               
                                  
   UPDATE APP_WATERCRAFT_INFO                                  
   SET                                   
   BOAT_NO =@BOAT_NO,                                  
   BOAT_NAME=@BOAT_NAME,               
   YEAR=@YEAR,                                  
   MAKE=@MAKE,                                  
   MODEL=@MODEL,                                  
   HULL_ID_NO=@HULL_ID_NO,                                  
   STATE_REG=@STATE_REG,         
   HULL_MATERIAL=@HULL_MATERIAL,                                  
   FUEL_TYPE=@FUEL_TYPE,                                  
   DATE_PURCHASED=@DATE_PURCHASED,                                  
   LENGTH=@LENGTH,                                  
   MAX_SPEED=@MAX_SPEED,                                  
   BERTH_LOC=@BERTH_LOC,                                  
   WATERS_NAVIGATED=@WATERS_NAVIGATED,                                  
   TERRITORY=@TERRITORY,                                  
   MODIFIED_BY=@MODIFIED_BY,                                  
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,                                  
   TYPE_OF_WATERCRAFT=@TYPE_OF_WATERCRAFT ,                                  
   INSURING_VALUE =@INSURING_VALUE ,                                  
  --DEDUCTIBLE =@DEDUCTIBLE ,                        
   WATERCRAFT_HORSE_POWER =@WATERCRAFT_HORSE_POWER ,                                  
   TWIN_SINGLE = @TWIN_SINGLE,                                
   DESC_OTHER_WATERCRAFT=@DESC_OTHER_WATERCRAFT    ,                              
   INCHES =@INCHES,                  
  LORAN_NAV_SYSTEM = @LORAN_NAV_SYSTEM,                  
  DIESEL_ENGINE = @DIESEL_ENGINE,                  
  SHORE_STATION = @SHORE_STATION,                  
  HALON_FIRE_EXT_SYSTEM = @HALON_FIRE_EXT_SYSTEM,                  
  DUAL_OWNERSHIP = @DUAL_OWNERSHIP,            
  REMOVE_SAILBOAT = @REMOVE_SAILBOAT,        
  COV_TYPE_BASIS = @COV_TYPE_BASIS,        
  PHOTO_ATTACHED = @PHOTO_ATTACHED,      
  MARINE_SURVEY  = @MARINE_SURVEY,      
  DATE_MARINE_SURVEY  = @DATE_MARINE_SURVEY,      
  --RPSINGH  - 13 MAy 2006      
  LOCATION_ADDRESS=@LOCATION_ADDRESS,      
  LOCATION_CITY=@LOCATION_CITY,      
  LOCATION_STATE=@LOCATION_STATE,      
  LOCATION_ZIP=@LOCATION_ZIP,      
  LAY_UP_PERIOD_FROM_DAY=@LAY_UP_PERIOD_FROM_DAY,      
  LAY_UP_PERIOD_FROM_MONTH=@LAY_UP_PERIOD_FROM_MONTH,      
  LAY_UP_PERIOD_TO_DAY=@LAY_UP_PERIOD_TO_DAY,      
  LAY_UP_PERIOD_TO_MONTH=@LAY_UP_PERIOD_TO_MONTH,      
  LOSSREPORT_ORDER=@LOSSREPORT_ORDER,
  LOSSREPORT_DATETIME=@LOSSREPORT_DATETIME    
       
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
   APP_ID = @APP_ID AND                                   
   APP_VERSION_ID = @APP_VERSION_ID AND                            
   BOAT_ID= @BOAT_ID                                   
                          
  /*11489>Bass boat (w/Motor)          
  11369>Outboard Boat          
  11487>Outboard (w/Motor)          
  11374>Pontoon (w/Motor)        
  11672>Sailboat w/outboard*/         
       
   -- commented by Swastika on 22nd May'06 : Gen Iss 2778 : Engine Info to be visible for all Boat Types       
 /* if(@type_of_watercraft<>11369 and @type_of_watercraft<>11489 and @type_of_watercraft<>11487 and @type_of_watercraft<>11374 and @type_of_watercraft<>11376)        
  delete from APP_WATERCRAFT_ENGINE_INFO                           
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
    APP_ID = @APP_ID AND              
    APP_VERSION_ID = @APP_VERSION_ID AND                           
    ASSOCIATED_BOAT= @BOAT_ID               
   */          
                   
  return 1                                  
end                                  
                                  
        
        
      
      
      
      
      
      
    
  



GO

