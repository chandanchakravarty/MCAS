IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name   : dbo.PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA                          
Created by  : Sumit Chhabra                
Date        : 11 Oc,2006                          
Purpose     : Fetch vehicles for umbrella sch.                 
Revison History  :                                          
 ------------------------------------------------------------                                                
Date     Review By          Comments                                              
                                     
------   ------------       -------------------------*/               
              
-- DROP PROC  dbo.PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA                              
CREATE PROC dbo.PROC_GET_DISTINCT_APP_BOATS_FOR_UMBRELLA                              
 @CUSTOMER_ID int,      
 @APP_ID int,      
 @APP_VERSION_ID smallint      
AS                              
BEGIN                                  
      
                                
 /* SELECT                               
BOAT_NO,isnull(BOAT_NAME,'') BOAT_NAME,                              
 YEAR,                                        
 MAKE,                                        
 MODEL,                                        
HULL_ID_NO ,                                        
    
 STATE_REG,                                        
 HULL_MATERIAL,                                        
 FUEL_TYPE,                                        
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                                        
    
 LENGTH,                                        
                            
MAX_SPEED,                  
                              
 BERTH_LOC,                                        
 WATERS_NAVIGATED,                                        
 TERRITORY,                                        
 TYPE_OF_WATERCRAFT , MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC AS WATERCRAFT_TYPE,                                       
 floor(INSURING_VALUE) INSURING_VALUE,                                        
isnull(convert(varchar(10),WATERCRAFT_HORSE_POWER),'') WATERCRAFT_HORSE_POWER,                              
 BOAT_ID,                                        
 TWIN_SINGLE,                                      
 DESC_OTHER_WATERCRAFT ,                  
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
 convert(varchar,DATE_MARINE_SURVEY,101) DATE_MARINE_SURVEY      
 --Added by RPSINGH - 12 May 2006      
 ,LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH      
      
      
  INTO #TEMP_BOATS                              
  FROM APP_WATERCRAFT_INFO                              
  INNER JOIN APP_LIST ON APP_WATERCRAFT_INFO.CUSTOMER_ID = APP_LIST.CUSTOMER_ID                              
  AND APP_WATERCRAFT_INFO.APP_ID = APP_LIST.APP_ID                               
  AND APP_WATERCRAFT_INFO.APP_VERSION_ID = APP_LIST.APP_VERSION_ID                               
  INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES ON APP_LIST.CUSTOMER_ID = APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID                              
  AND APP_LIST.APP_NUMBER =                             
  LTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                                
  AND APP_LIST.APP_VERSION_ID=                            
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                            
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,                          
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                          
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))                 
 LEFT OUTER JOIN MNT_LOOKUP_VALUES ON   
 MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = APP_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT  
  WHERE APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID                           
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_ID = @APP_ID                               
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_VERSION_ID = @APP_VERSION_ID                               
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,0)=0                        
  AND APP_WATERCRAFT_INFO.IS_ACTIVE='Y'                            
                          
  UNION                              
                                
  SELECT                               
BOAT_NO,                                        
     
isnull(BOAT_NAME,'') BOAT_NAME,                              
 YEAR,                                        
 MAKE,                                        
 MODEL,                                        
HULL_ID_NO ,                                        
    
 STATE_REG,                                        
 HULL_MATERIAL,                                        
 FUEL_TYPE,                                        
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                                        
    
 LENGTH,                                        
    
MAX_SPEED,                  
                              
 BERTH_LOC,                                        
 WATERS_NAVIGATED,                                        
 TERRITORY,                                        
 TYPE_OF_WATERCRAFT , MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC AS WATERCRAFT_TYPE,                                       
 floor(INSURING_VALUE) INSURING_VALUE,                                        
isnull(convert(varchar(10),WATERCRAFT_HORSE_POWER),'') WATERCRAFT_HORSE_POWER,                              
 BOAT_ID,                                        
 TWIN_SINGLE,                                      
 DESC_OTHER_WATERCRAFT ,                 
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
 convert(varchar,DATE_MARINE_SURVEY,101) DATE_MARINE_SURVEY      
    
 ,LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH      
    
  FROM POL_WATERCRAFT_INFO                              
  INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_WATERCRAFT_INFO.CUSTOMER_ID = POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                              
  AND POL_WATERCRAFT_INFO.POLICY_ID = POL_CUSTOMER_POLICY_LIST.POLICY_ID                               
  AND POL_WATERCRAFT_INFO.POLICY_VERSION_ID = POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID                               
  INNER JOIN APP_UMBRELLA_UNDERLYING_POLICIES ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID                           
  = APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID               
  AND POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER =                           
  LTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,0,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))                                
  AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=                          
  SUBSTRING(LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                          
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)))),0,   
  CHARINDEX('.',LTRIM(RTRIM(SUBSTRING(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER,                          
  CHARINDEX('-',APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER)+@APP_VERSION_ID,                          
  LEN(APP_UMBRELLA_UNDERLYING_POLICIES.POLICY_NUMBER))))))         
 LEFT OUTER JOIN MNT_LOOKUP_VALUES ON   
 MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = POL_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT      
  WHERE APP_UMBRELLA_UNDERLYING_POLICIES.CUSTOMER_ID = @CUSTOMER_ID AND                           
  APP_UMBRELLA_UNDERLYING_POLICIES.APP_ID = @APP_ID                               
  AND APP_UMBRELLA_UNDERLYING_POLICIES.APP_VERSION_ID = @APP_VERSION_ID                               
  AND UPPER(POLICY_COMPANY)='WOLVERINE' AND ISNULL(IS_POLICY,@APP_VERSION_ID)=@APP_VERSION_ID                      
  AND POL_WATERCRAFT_INFO.IS_ACTIVE='Y'                              
                      
SELECT            
BOAT_NO,                                        
     
isnull(BOAT_NAME,'') BOAT_NAME,                              
 YEAR,                                        
 MAKE,                                        
 MODEL,                                        
HULL_ID_NO ,                                        
    
 STATE_REG,                                        
 HULL_MATERIAL,                                        
 FUEL_TYPE,                                        
convert(varchar,DATE_PURCHASED,101) DATE_PURCHASED,                                        
    
 LENGTH,                                        
    
MAX_SPEED,                  
                              
 BERTH_LOC,                                        
 WATERS_NAVIGATED,                                        
 TERRITORY,                                        
 TYPE_OF_WATERCRAFT ,                                        
 floor(INSURING_VALUE) INSURING_VALUE,                                        
isnull(convert(varchar(10),WATERCRAFT_HORSE_POWER),'') WATERCRAFT_HORSE_POWER,                              
 BOAT_ID,                                        
 TWIN_SINGLE,                                      
 DESC_OTHER_WATERCRAFT ,                 
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
 convert(varchar,DATE_MARINE_SURVEY,101) DATE_MARINE_SURVEY,WATERCRAFT_TYPE      
    
 ,LOCATION_ADDRESS, LOCATION_CITY, LOCATION_STATE, LOCATION_ZIP, LAY_UP_PERIOD_FROM_DAY, LAY_UP_PERIOD_FROM_MONTH, LAY_UP_PERIOD_TO_DAY, LAY_UP_PERIOD_TO_MONTH      
    
    
  FROM #TEMP_BOATS                              
--  GROUP BY LOC_ADD1, LOC_ADD2  */
SELECT CUSTOMER_ID,APP_ID,APP_VERSION_ID,BOAT_ID,isnull(BOAT_NO, '') BOAT_NO, 
isnull(YEAR,'') YEAR,isnull(MAKE, '') MAKE, isnull(MODEL, '') MODEL, 
ISNULL(LTRIM(RTRIM(TYPE_OF_WATERCRAFT)),'') TYPE_OF_WATERCRAFT,CREATED_BY,
CONVERT(VARCHAR(15),CREATED_DATETIME,101) CREATED_DATETIME,MODIFIED_BY,CONVERT(VARCHAR(15),
APP_UMBRELLA_WATERCRAFT_INFO.LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME,MNTL.LOOKUP_VALUE_DESC as WATERCRAFT_TYPE
FROM APP_UMBRELLA_WATERCRAFT_INFO 
LEFT OUTER JOIN  MNT_LOOKUP_VALUES MNTL 
on APP_UMBRELLA_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNTL.LOOKUP_UNIQUE_ID
WHERE CUSTOMER_ID= @CUSTOMER_ID 
AND APP_ID = @APP_ID
AND APP_VERSION_ID = @APP_VERSION_ID                                                 
      
END      
      
    
  



GO

