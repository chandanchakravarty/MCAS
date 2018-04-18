IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : Dbo.Proc_InsertWatercraft                        
Created by      : Ebix                        
Date            : 5/16/2005                        
Purpose       :Insert a watercraft information                        
Revison History :                        
Used In        : Wolverine                        
                        
Modified By : Anurag Verma                        
Modified On : 30/09/2005                         
Purpose  : Removing fields in insert query (reg_no,power,hull_type,cost_new,present_value) adding a new field (twin_single)                         
                        
Modified By : Anurag verma                        
Modified On : 18/10/2005                        
Purpose  : Applying unique boat number check                        
                      
Modified by    : Mohit                      
Modified on    : 15/11/2005                      
Purpose        : Adding field DESC_OTHER_WATERCRAFT                      
            
Modified by    : Sumit Chhabra            
Modified on    : 09/01/2006                         
Purpose        : Adding fields LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM and DUAL_OWNERSHIP             
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
-- drop proc dbo.Proc_InsertWatercraft      
CREATE   PROC dbo.Proc_InsertWatercraft                        
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
@CREATED_BY     int,                        
@CREATED_DATETIME     datetime,                        
@TYPE_OF_WATERCRAFT nchar(10),                        
@INSURING_VALUE decimal(10,2),                        
--@DEDUCTIBLE decimal(10,2),                 
@WATERCRAFT_HORSE_POWER decimal(10,2),                        
@TWIN_SINGLE int,                      
@DESC_OTHER_WATERCRAFT varchar(150) ,                    
@INCHES    nvarchar(20),              
@LORAN_NAV_SYSTEM smallint,              
@DIESEL_ENGINE smallint,              
@SHORE_STATION smallint,              
@HALON_FIRE_EXT_SYSTEM smallint,              
@DUAL_OWNERSHIP smallint,      
@REMOVE_SAILBOAT smallint,      
@COV_TYPE_BASIS int,      
@PHOTO_ATTACHED int,      
@MARINE_SURVEY int=null,      
@DATE_MARINE_SURVEY datetime = null,     
@LOCATION_ADDRESS VARCHAR(200) = null,    
@LOCATION_CITY VARCHAR(50) = null,    
@LOCATION_STATE VARCHAR(50) = null,    
@LOCATION_ZIP VARCHAR(20) = null,    
@LAY_UP_PERIOD_FROM_DAY INTEGER = null,    
@LAY_UP_PERIOD_FROM_MONTH INTEGER = null,    
@LAY_UP_PERIOD_TO_DAY INTEGER = null,    
@LAY_UP_PERIOD_TO_MONTH INTEGER  = null,
@LOSSREPORT_ORDER int = null, 
@LOSSREPORT_DATETIME DateTime = null                       
)                        
AS                        
BEGIN                        
 declare @boatNoCount int                        
                        
 select @boatNoCount=count(BOAT_NO)  from APP_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND       
  APP_ID = @APP_ID AND                        
  APP_VERSION_ID = @APP_VERSION_ID                        
  and boat_no=@BOAT_NO;                        
    
 if @boatNoCount>0                         
  begin                           
   Set @BOAT_ID=-2                         
  end                        
 else                        
  begin                        
                        
   select @BOAT_ID=isnull(max(BOAT_ID),0)+1 from APP_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                        
   APP_ID = @APP_ID AND                        
   APP_VERSION_ID = @APP_VERSION_ID;                        
   INSERT INTO APP_WATERCRAFT_INFO                   
   (        
    CUSTOMER_ID,             
    APP_ID,                        
    APP_VERSION_ID,                        
    BOAT_ID,                        
    BOAT_NO,                        
    BOAT_NAME,                        
    YEAR,                        
    MAKE,                        
    MODEL,                        
    HULL_ID_NO,                        
    STATE_REG,                        
    HULL_MATERIAL,                        
    FUEL_TYPE,                        
    DATE_PURCHASED,                        
    LENGTH,                        
    MAX_SPEED,                        
    BERTH_LOC,                        
    WATERS_NAVIGATED,                        
    TERRITORY,                        
    IS_ACTIVE,                        
    CREATED_BY,                        
    CREATED_DATETIME,                        
    TYPE_OF_WATERCRAFT,                        
    INSURING_VALUE ,                 
    --DEDUCTIBLE,                       
    WATERCRAFT_HORSE_POWER,                        
    TWIN_SINGLE,                      
    DESC_OTHER_WATERCRAFT  ,                     
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
 DATE_MARINE_SURVEY,    
 LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH,
 LOSSREPORT_ORDER,
 LOSSREPORT_DATETIME              
   )                        
   VALUES                        
   (                        
    @CUSTOMER_ID,                        
    @APP_ID,                        
    @APP_VERSION_ID,                        
    @BOAT_ID,                        
    @BOAT_NO,                        
    @BOAT_NAME,                        
    @YEAR,                        
    @MAKE,                        
    @MODEL,                        
    @HULL_ID_NO,                        
    @STATE_REG,                        
    @HULL_MATERIAL,                        
    @FUEL_TYPE,                        
    @DATE_PURCHASED,                        
    @LENGTH,                        
    @MAX_SPEED,                        
    @BERTH_LOC,                        
    @WATERS_NAVIGATED,                        
    @TERRITORY,                        
    'Y',                        
    @CREATED_BY,                        
    @CREATED_DATETIME,                        
    @TYPE_OF_WATERCRAFT,                        
    @INSURING_VALUE ,                  
    --@DEDUCTIBLE,                      
    @WATERCRAFT_HORSE_POWER,                        
    @TWIN_SINGLE,                      
    @DESC_OTHER_WATERCRAFT  ,                    
    @INCHES,              
  @LORAN_NAV_SYSTEM,              
  @DIESEL_ENGINE,              
  @SHORE_STATION,              
  @HALON_FIRE_EXT_SYSTEM,              
  @DUAL_OWNERSHIP,      
  @REMOVE_SAILBOAT,      
  @COV_TYPE_BASIS,      
  @PHOTO_ATTACHED,      
  @MARINE_SURVEY,      
  @DATE_MARINE_SURVEY ,    
  @LOCATION_ADDRESS, @LOCATION_CITY, @LOCATION_STATE, @LOCATION_ZIP, @LAY_UP_PERIOD_FROM_DAY, @LAY_UP_PERIOD_FROM_MONTH, @LAY_UP_PERIOD_TO_DAY, @LAY_UP_PERIOD_TO_MONTH,
  @LOSSREPORT_ORDER,
  @LOSSREPORT_DATETIME    
   )                        
  end      
END      



GO

