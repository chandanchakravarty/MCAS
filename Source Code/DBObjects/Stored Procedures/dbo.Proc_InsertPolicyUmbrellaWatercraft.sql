IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 

  
    
/*----------------------------------------------------------                
Proc Name       : Dbo.Proc_InsertPolicyUmbrellaWatercraft                
Created by      : Sumit Chhabra      
Date            : 21-03-2006            
Purpose        :Insert a policy umbrella watercraft information                
Revison History :                
Used In         : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--drop proc dbo.Proc_InsertPolicyUmbrellaWatercraft                
CREATE    PROC dbo.Proc_InsertPolicyUmbrellaWatercraft                
(                
@CUSTOMER_ID     INT,                
@POLICY_ID     INT,                
@POLICY_VERSION_ID     SMALLINT,                
@BOAT_ID     SMALLINT OUTPUT,                
@BOAT_NO     INT,                
@BOAT_NAME     NVARCHAR(25),                
@YEAR     INT,                
@MAKE     NVARCHAR(75),                
@MODEL     NVARCHAR(75),                
@HULL_ID_NO     NVARCHAR(75),                
@STATE_REG     NVARCHAR(5),                
@HULL_MATERIAL     INT,                
@FUEL_TYPE     INT,                
@DATE_PURCHASED     DATETIME =NULL,                
@LENGTH     NVARCHAR(10),           
--@INCHES     NVARCHAR(10),               
@MAX_SPEED     DECIMAL(10,2),                
@BERTH_LOC     NVARCHAR(100),                
@WATERS_NAVIGATED     VARCHAR(250),                
@TERRITORY     NVARCHAR(25),                
@CREATED_BY     INT,                
@CREATED_DATETIME     DATETIME,                
@TYPE_OF_WATERCRAFT NCHAR(10),                
@INSURING_VALUE DECIMAL(10,2),                
@WATERCRAFT_HORSE_POWER INT,                
--@TWIN_SINGLE INT,              
@DESC_OTHER_WATERCRAFT VARCHAR(150),        
@LORAN_NAV_SYSTEM SMALLINT,        
@DIESEL_ENGINE  SMALLINT,        
@SHORE_STATION  SMALLINT,        
@HALON_FIRE_EXT_SYSTEM  SMALLINT,        
@DUAL_OWNERSHIP  SMALLINT,        
@REMOVE_SAILBOAT  SMALLINT,    
@INCHES  nvarchar(20),              
@TWIN_SINGLE int,    
@LOCATION_ADDRESS varchar(200),    
@LOCATION_CITY varchar(50),    
@LOCATION_STATE varchar(50),    
@LOCATION_ZIP varchar(20),    
@COV_TYPE_BASIS INT,  
@USED_PARTICIPATE smallint,  
@WATERCRAFT_CONTEST varchar(250),
@OTHER_POLICY nvarchar(150) = null,
@IS_BOAT_EXCLUDED smallint = null    
)                
AS                
BEGIN                
 DECLARE @BOATNOCOUNT INT     

if (@BOAT_NO is null or @BOAT_NO=0)
   SELECT @BOAT_ID=ISNULL(MAX(BOAT_ID),0)+1,@BOAT_NO=ISNULL(MAX(BOAT_NO),0)+1 FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                
   POLICY_ID = @POLICY_ID AND                
   POLICY_VERSION_ID = @POLICY_VERSION_ID; 
else
   SELECT @BOAT_ID=ISNULL(MAX(BOAT_ID),0)+1 FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                
   POLICY_ID = @POLICY_ID AND                
   POLICY_VERSION_ID = @POLICY_VERSION_ID;            
                
 SELECT @BOATNOCOUNT=COUNT(BOAT_NO)  FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                
  POLICY_ID = @POLICY_ID AND                
  POLICY_VERSION_ID = @POLICY_VERSION_ID                
  AND BOAT_NO=@BOAT_NO;                
                
 IF @BOATNOCOUNT>0                 
  BEGIN                   
   SET @BOAT_ID=-2                 
  END                
 ELSE                
  BEGIN                
                
   SELECT @BOAT_ID=ISNULL(MAX(BOAT_ID),0)+1 FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                
   POLICY_ID = @POLICY_ID AND                
   POLICY_VERSION_ID = @POLICY_VERSION_ID;                
            
   INSERT INTO  POL_UMBRELLA_WATERCRAFT_INFO                
   (                
    CUSTOMER_ID,                
    POLICY_ID,                
    POLICY_VERSION_ID,                
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
--    INCHES,               
    MAX_SPEED,                
    BERTH_LOC,                
    WATERS_NAVIGATED,                
    TERRITORY,                
    IS_ACTIVE,                
    CREATED_BY,                
    CREATED_DATETIME,                
    TYPE_OF_WATERCRAFT,                
    INSURING_VALUE ,                
    WATERCRAFT_HORSE_POWER,                
--    TWIN_SINGLE,              
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
OTHER_POLICY,
IS_BOAT_EXCLUDED
   )                
   VALUES            
   (                
    @CUSTOMER_ID,                
    @POLICY_ID,                
    @POLICY_VERSION_ID,                
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
--    @INCHES ,               
    @MAX_SPEED,                
    @BERTH_LOC,                
    @WATERS_NAVIGATED,                
    @TERRITORY,               
    'Y',                
    @CREATED_BY,                
    @CREATED_DATETIME,                
    @TYPE_OF_WATERCRAFT,                
    @INSURING_VALUE ,                
    @WATERCRAFT_HORSE_POWER,                
--    @TWIN_SINGLE,              
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
 @OTHER_POLICY,
 @IS_BOAT_EXCLUDED  
   )                
  END                
END                
    
    
    
    
  





GO

