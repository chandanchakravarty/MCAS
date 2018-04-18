IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Stored Procedure dbo.Proc_InsertPolicyWatercraft    Script Date: 5/15/2006 12:28:29 PM ******/    
/*----------------------------------------------------------            
Proc Name       : Dbo.Proc_InsertPolicyWatercraft            
Created by      : Vijay Arora        
Date            : 21-11-2005        
Purpose        :Insert a policy watercraft information            
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
-- drop proc dbo.Proc_InsertPolicyWatercraft    
CREATE     PROC dbo.Proc_InsertPolicyWatercraft            
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
@INCHES     NVARCHAR(10),           
@MAX_SPEED     DECIMAL(10,2),            
@BERTH_LOC     NVARCHAR(100),            
@WATERS_NAVIGATED     VARCHAR(250),            
@TERRITORY     NVARCHAR(25),            
@CREATED_BY     INT,            
@CREATED_DATETIME     DATETIME,            
@TYPE_OF_WATERCRAFT NCHAR(10),            
@INSURING_VALUE DECIMAL(10,2),            
@WATERCRAFT_HORSE_POWER DECIMAL(10,2),            
@TWIN_SINGLE INT,          
@DESC_OTHER_WATERCRAFT VARCHAR(150),    
@LORAN_NAV_SYSTEM SMALLINT,    
@DIESEL_ENGINE  SMALLINT,    
@SHORE_STATION  SMALLINT,    
@HALON_FIRE_EXT_SYSTEM  SMALLINT,    
@DUAL_OWNERSHIP  SMALLINT,    
@REMOVE_SAILBOAT  SMALLINT,    
@COV_TYPE_BASIS INT,    
@PHOTO_ATTACHED INT, @MARINE_SURVEY INT,    
@DATE_MARINE_SURVEY DATETIME = NULL,    
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
 DECLARE @BOATNOCOUNT INT            
            
 SELECT @BOATNOCOUNT=COUNT(BOAT_NO)  FROM POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
  POLICY_ID = @POLICY_ID AND            
  POLICY_VERSION_ID = @POLICY_VERSION_ID            
  AND BOAT_NO=@BOAT_NO;            
            
 IF @BOATNOCOUNT>0             
  BEGIN               
   SET @BOAT_ID=-2             
  END            
 ELSE            
  BEGIN            
            
   SELECT @BOAT_ID=ISNULL(MAX(BOAT_ID),0)+1 FROM POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND            
   POLICY_ID = @POLICY_ID AND            
   POLICY_VERSION_ID = @POLICY_VERSION_ID;            
        
   INSERT INTO  POL_WATERCRAFT_INFO            
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
    INCHES,           
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
    TWIN_SINGLE,          
    DESC_OTHER_WATERCRAFT,    
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
    LOCATION_ADDRESS,    
    LOCATION_CITY,    
    LOCATION_STATE,    
    LOCATION_ZIP,    
    LAY_UP_PERIOD_FROM_DAY,    
    LAY_UP_PERIOD_FROM_MONTH,    
    LAY_UP_PERIOD_TO_DAY,    
    LAY_UP_PERIOD_TO_MONTH,
    LOSSREPORT_ORDER,
    LOSSREPORT_DATETIME              
               
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
    @INCHES ,           
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
    @TWIN_SINGLE,          
    @DESC_OTHER_WATERCRAFT,    
    @LORAN_NAV_SYSTEM,     
    @DIESEL_ENGINE,    
    @SHORE_STATION,     
    @HALON_FIRE_EXT_SYSTEM,     
    @DUAL_OWNERSHIP,     
    @REMOVE_SAILBOAT,    
    @COV_TYPE_BASIS,    
    @PHOTO_ATTACHED,    
    @MARINE_SURVEY,    
    @DATE_MARINE_SURVEY,    
    @LOCATION_ADDRESS,    
    @LOCATION_CITY,    
    @LOCATION_STATE,    
    @LOCATION_ZIP,    
    @LAY_UP_PERIOD_FROM_DAY,    
    @LAY_UP_PERIOD_FROM_MONTH,    
    @LAY_UP_PERIOD_TO_DAY,    
    @LAY_UP_PERIOD_TO_MONTH,    
    @LOSSREPORT_ORDER,
    @LOSSREPORT_DATETIME              
     
   )            
  END            
END            
            
    
    
    
  



GO

