IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyUmbrellaWatercraftInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyUmbrellaWatercraftInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
   
/*----------------------------------------------------------                        
Proc Name           : Dbo.Proc_GetPolicyUmbrellaWatercraftInfo                        
Created by          : Sumit Chhabra        
Date                : 21-11-2005                  
Purpose             : To get the information  from pol_watercraft_info  table                        
Revison History  :                        
Used In                 : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/            
-- drop proc dbo.Proc_GetPolicyUmbrellaWatercraftInfo                                     
CREATE PROC dbo.Proc_GetPolicyUmbrellaWatercraftInfo                        
(                        
@CUSTOMER_ID  int,                        
@POLICY_ID  int,                        
@POLICY_VERSION_ID int,                        
@BOAT_ID  int                        
)                        
AS                        
BEGIN                        
                        
                        
SELECT    
CUSTOMER_ID,  
POLICY_ID,  
POLICY_VERSION_ID,  
BOAT_ID,  
BOAT_NO,                        
BOAT_NAME,                        
YEAR,                        
MAKE,                        
MODEL,                        
HULL_ID_NO as SERIAL_NO,                    
STATE_REG,                        
OTHER_HULL_TYPE,                        
HULL_MATERIAL,                        
FUEL_TYPE,                        
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                        
LENGTH,                
--INCHES,                      
WEIGHT,                        
floor(MAX_SPEED) MAX_SPEED,             
--MAX_SPEED,                        
BERTH_LOC,                        
WATERS_NAVIGATED,                        
TERRITORY,                        
TYPE_OF_WATERCRAFT ,                        
floor(INSURING_VALUE) INSURING_VALUE,                        
WATERCRAFT_HORSE_POWER,                        
BOAT_ID,                        
--TWIN_SINGLE,                
DESC_OTHER_WATERCRAFT,          
LORAN_NAV_SYSTEM,           
DIESEL_ENGINE,          
SHORE_STATION,           
HALON_FIRE_EXT_SYSTEM,           
DUAL_OWNERSHIP,           
REMOVE_SAILBOAT,          
IS_ACTIVE,    
INCHES,    
TWIN_SINGLE,  
LOCATION_ADDRESS,  
LOCATION_CITY,  
LOCATION_STATE,  
LOCATION_ZIP,  
COV_TYPE_BASIS,
USED_PARTICIPATE,
WATERCRAFT_CONTEST,
ISNULL(IS_BOAT_EXCLUDED,'') AS IS_BOAT_EXCLUDED,
ISNULL(OTHER_POLICY,'') AS OTHER_POLICY
FROM        POL_UMBRELLA_WATERCRAFT_INFO                        
WHERE     (CUSTOMER_ID = @CUSTOMER_ID)   and (POLICY_ID=@POLICY_ID) AND (POLICY_VERSION_ID=@POLICY_VERSION_ID) AND (BOAT_ID= @BOAT_ID);                        
END                        
                        
                        
                        
                      
                    
                  
                
              
            
          
          
          
      
    
  
  
  
  
  





GO

