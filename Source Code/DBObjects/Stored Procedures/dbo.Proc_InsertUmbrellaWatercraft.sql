IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------                    
Proc Name       : Dbo.Proc_InsertUmbrellaWatercraft                    
Created by      : nidhi                    
Date            : 6/16/2005                    
Purpose       :Insert a umbrella watercraft information                    
Revison History :                    
Used In        : Wolverine                   
                  
Modified by    : Mohit                  
Modified on    : 15/11/2005                  
Purpose        : Adding field DESC_OTHER_WATERCRAFT                    
              
              
Modified by    : Sumit Chhabra              
Modified on    : 09/01/2006                  
Purpose        : Adding fields LORAN_NAV_SYSTEM,DIESEL_ENGINE,SHORE_STATION,HALON_FIRE_EXT_SYSTEM,DUAL_OWNERSHIP              
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
-- drop proc dbo.Proc_InsertUmbrellaWatercraft      
      
CREATE PROC dbo.Proc_InsertUmbrellaWatercraft                    
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
@CREATED_BY     int,                    
@CREATED_DATETIME     datetime,                    
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
@IS_BOAT_EXCLUDED smallint = null,      
@OTHER_POLICY nvarchar(150) = null
)                    
AS                    
BEGIN     
  
--Generate a new boat_no in case the boat_no passed is null  
if ((@BOAT_NO is null) or @BOAT_NO = 0)  
 select @BOAT_ID=isnull(max(BOAT_ID),0)+1,@BOAT_NO=isnull(max(BOAT_NO),0)+1 from APP_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                    
   APP_ID = @APP_ID AND      
   APP_VERSION_ID = @APP_VERSION_ID  
else  
 select @BOAT_ID=isnull(max(BOAT_ID),0)+1 from APP_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                    
   APP_ID = @APP_ID AND                    
   APP_VERSION_ID = @APP_VERSION_ID;          
  
if exists(select customer_id from APP_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                
		   APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  and  BOAT_NO=@BOAT_NO)
begin 
 set @BOAT_ID = -2
 return
end

  
INSERT INTO APP_UMBRELLA_WATERCRAFT_INFO                    
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
REG_NO,                    
POWER,                    
HULL_TYPE,                    
HULL_MATERIAL,                    
FUEL_TYPE,                    
HULL_DESIGN,                    
DATE_PURCHASED,                    
LENGTH,                    
MAX_SPEED,                    
COST_NEW,                    
PRESENT_VALUE,                    
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
DESC_OTHER_WATERCRAFT,              
LORAN_NAV_SYSTEM,              
DIESEL_ENGINE,              
SHORE_STATION,              
HALON_FIRE_EXT_SYSTEM,              
DUAL_OWNERSHIP,        
REMOVE_SAILBOAT,      
INCHES,      
TWIN_SINGLE,      
LOCATION_ADDRESS,      
LOCATION_CITY,      
LOCATION_STATE,      
LOCATION_ZIP,      
COV_TYPE_BASIS,    
USED_PARTICIPATE,    
WATERCRAFT_CONTEST,
IS_BOAT_EXCLUDED,
OTHER_POLICY
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
@REG_NO,                    
@POWER,                    
@HULL_TYPE,                  
@HULL_MATERIAL,                    
@FUEL_TYPE,                    
@HULL_DESIGN,                    
@DATE_PURCHASED,                    
@LENGTH,                    
@MAX_SPEED,                    
@COST_NEW,                    
@PRESENT_VALUE,                    
@BERTH_LOC,                    
@WATERS_NAVIGATED,                    
@TERRITORY,                    
'Y',                    
@CREATED_BY,                    
@CREATED_DATETIME,                    
@TYPE_OF_WATERCRAFT ,                    
@INSURING_VALUE ,                 
--@DEDUCTIBLE,                   
@WATERCRAFT_HORSE_POWER,                  
@DESC_OTHER_WATERCRAFT,              
@LORAN_NAV_SYSTEM,              
@DIESEL_ENGINE,              
@SHORE_STATION,              
@HALON_FIRE_EXT_SYSTEM,              
@DUAL_OWNERSHIP,        
@REMOVE_SAILBOAT,      
@INCHES,      
@TWIN_SINGLE,      
@LOCATION_ADDRESS,      
@LOCATION_CITY,      
@LOCATION_STATE,      
@LOCATION_ZIP,      
@COV_TYPE_BASIS,    
@USED_PARTICIPATE,    
@WATERCRAFT_CONTEST,
@IS_BOAT_EXCLUDED,
@OTHER_POLICY    
)                    
                    
END                    
  







GO

