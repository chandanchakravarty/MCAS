IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyWatercraftInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyWatercraftInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Stored Procedure dbo.Proc_GetPolicyWatercraftInfo    Script Date: 5/15/2006 12:06:11 PM ******/  
/*----------------------------------------------------------                  
Proc Name           : Dbo.Proc_GetPolicyWatercraftInfo                  
Created by            : Vijay Arora            
Date                    : 21-11-2005            
Purpose                : To get the information  from pol_watercraft_info  table                  
Revison History  :                  
Used In                 : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_GetPolicyWatercraftInfo                  
CREATE  PROC dbo.Proc_GetPolicyWatercraftInfo                  
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
HULL_MATERIAL,                  
FUEL_TYPE,                  
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                  
LENGTH,          
INCHES,                
--floor(MAX_SPEED) MAX_SPEED,       
MAX_SPEED,                  
BERTH_LOC,                  
WATERS_NAVIGATED,                  
TERRITORY,                  
TYPE_OF_WATERCRAFT ,                  
floor(INSURING_VALUE) INSURING_VALUE,                  
WATERCRAFT_HORSE_POWER,                  
BOAT_ID,                  
TWIN_SINGLE,          
DESC_OTHER_WATERCRAFT,    
LORAN_NAV_SYSTEM,     
DIESEL_ENGINE,    
SHORE_STATION,     
HALON_FIRE_EXT_SYSTEM,     
DUAL_OWNERSHIP,     
REMOVE_SAILBOAT,    
IS_ACTIVE,  
COV_TYPE_BASIS,  
PHOTO_ATTACHED,  
MARINE_SURVEY,  
CONVERT(VARCHAR,DATE_MARINE_SURVEY,101) DATE_MARINE_SURVEY,  
LOCATION_ADDRESS,  
LOCATION_CITY,  
LOCATION_STATE,  
LOCATION_ZIP,  
LAY_UP_PERIOD_FROM_DAY,  
LAY_UP_PERIOD_FROM_MONTH,  
LAY_UP_PERIOD_TO_DAY,  
LAY_UP_PERIOD_TO_MONTH,
LOSSREPORT_ORDER, 
CONVERT(VARCHAR,LOSSREPORT_DATETIME, 101) AS LOSSREPORT_DATETIME  
FROM        POL_WATERCRAFT_INFO                  
WHERE     (CUSTOMER_ID = @CUSTOMER_ID)   and (POLICY_ID=@POLICY_ID) AND (POLICY_VERSION_ID=@POLICY_VERSION_ID) AND (BOAT_ID= @BOAT_ID);                  
END                  
                  
              
  
  



GO

