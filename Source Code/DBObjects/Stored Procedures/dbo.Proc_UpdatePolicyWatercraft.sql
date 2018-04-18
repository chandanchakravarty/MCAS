IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name        : dbo.Proc_UpdatePolicyWatercraft                    
Created by        : Vijay Arora                
Date                : 21-11-2005                
Purpose          : To update  record in watercraft information   table                    
Revison History  :                    
Used In          :   Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
-- drop proc dbo.Proc_UpdatePolicyWatercraft                    
CREATE      PROC dbo.Proc_UpdatePolicyWatercraft                    
(                 
@CUSTOMER_ID     INT,                    
@POLICY_ID     INT,                    
@POLICY_VERSION_ID     SMALLINT,                    
@BOAT_ID     SMALLINT,                    
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
@MODIFIED_BY INT,                    
@LAST_UPDATED_DATETIME DATETIME,                    
@TYPE_OF_WATERCRAFT NCHAR(10),                    
@INSURING_VALUE DECIMAL(10,2),                    
@WATERCRAFT_HORSE_POWER  DECIMAL(10,2),                    
@TWIN_SINGLE INT,                  
@DESC_OTHER_WATERCRAFT VARCHAR(150),            
@LORAN_NAV_SYSTEM SMALLINT,            
@DIESEL_ENGINE SMALLINT,            
@SHORE_STATION SMALLINT,            
@HALON_FIRE_EXT_SYSTEM SMALLINT,            
@DUAL_OWNERSHIP SMALLINT,            
@REMOVE_SAILBOAT SMALLINT,      
@COV_TYPE_BASIS INT,      
@PHOTO_ATTACHED INT =NULL,      
@MARINE_SURVEY INT =NULL,      
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
                    
declare @boatNoCount int                    
                    
if exists(select customer_id   from POL_WATERCRAFT_INFO WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                    
  POLICY_ID = @POLICY_ID AND                    
  POLICY_VERSION_ID = @POLICY_VERSION_ID                    
  and boat_no=@BOAT_NO and boat_id <> @boat_id)    
 return -2                    
                    
    
         
   UPDATE POL_WATERCRAFT_INFO                    
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
   INCHES=@INCHES,                 
   MAX_SPEED=@MAX_SPEED,                    
   BERTH_LOC=@BERTH_LOC,                    
   WATERS_NAVIGATED=@WATERS_NAVIGATED,                    
   TERRITORY=@TERRITORY,                    
   MODIFIED_BY=@MODIFIED_BY,                    
   LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,           
   TYPE_OF_WATERCRAFT=@TYPE_OF_WATERCRAFT ,                    
   INSURING_VALUE =@INSURING_VALUE ,                    
   WATERCRAFT_HORSE_POWER =@WATERCRAFT_HORSE_POWER ,                    
   TWIN_SINGLE = @TWIN_SINGLE,                  
   DESC_OTHER_WATERCRAFT=@DESC_OTHER_WATERCRAFT,            
  LORAN_NAV_SYSTEM = @LORAN_NAV_SYSTEM,            
  DIESEL_ENGINE = @DIESEL_ENGINE,            
  SHORE_STATION = @SHORE_STATION,            
  HALON_FIRE_EXT_SYSTEM = @HALON_FIRE_EXT_SYSTEM,            
  DUAL_OWNERSHIP = @DUAL_OWNERSHIP,            
  REMOVE_SAILBOAT = @REMOVE_SAILBOAT,      
  COV_TYPE_BASIS = @COV_TYPE_BASIS,      
  PHOTO_ATTACHED = @PHOTO_ATTACHED,      
  MARINE_SURVEY = @MARINE_SURVEY,      
  DATE_MARINE_SURVEY = @DATE_MARINE_SURVEY,      
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
   POLICY_ID = @POLICY_ID AND                     
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
   BOAT_ID= @BOAT_ID                     
      
return 1                    
-- Commented by swastika : 22nd MAy'06 : Gen Iss 2778 : Engine Info to be displayed for all Boat Types          
 --Delete records for Outboard Engine Info when the type of watercraft is not Outboard Boat          
 /* IF(@TYPE_OF_WATERCRAFT<>11369 AND @TYPE_OF_WATERCRAFT<>11489 AND @TYPE_OF_WATERCRAFT<>11487 AND @TYPE_OF_WATERCRAFT<>11374 AND @TYPE_OF_WATERCRAFT<>11672)      
  delete from POL_WATERCRAFT_ENGINE_INFO                           
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                  
    POLICY_ID = @POLICY_ID AND                                   
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND                           
    ASSOCIATED_BOAT= @BOAT_ID                             
*/      
end                    
                    
      
      
      
      
      
    
  



GO

