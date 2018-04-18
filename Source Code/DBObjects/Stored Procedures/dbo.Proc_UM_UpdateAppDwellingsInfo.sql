IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_UpdateAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_UpdateAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE               PROC Dbo.Proc_UM_UpdateAppDwellingsInfo    
(    
 @CUSTOMER_ID     int,    
 @APP_ID     int,    
 @APP_VERSION_ID     smallint,    
 @DWELLING_ID smallint,    
 @DWELLING_NUMBER     int,    
 @LOCATION_ID     smallint,    
 @SUB_LOC_ID     smallint,    
 @YEAR_BUILT     smallint,    
 @PURCHASE_YEAR     smallint,    
 @PURCHASE_PRICE     decimal(9),    
 @MARKET_VALUE     decimal(9),    
 @REPLACEMENT_COST     decimal(9),    
 @BUILDING_TYPE     int,    
 @OCCUPANCY     int,    
 @NEED_OF_UNITS     smallint,    
 @USAGE     nvarchar(100),    
 @NEIGHBOURS_VISIBLE     nchar(1),    
 @OCCUPIED_DAILY     nchar(1),    
 @NO_WEEKS_RENTED     smallint,    
 @MODIFIED_BY     int,  
 @IS_ACTIVE CHAR,
 @REPAIR_COST DECIMAL
 
)    
    
AS    
    
BEGIN    
     
 --Check If there is any entry for this location and sub location    
 IF EXISTS    
 (    
  SELECT DWELLING_ID  
  FROM APP_UMBRELLA_DWELLINGS_INFO    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
               APP_ID = @APP_ID AND    
         APP_VERSION_ID = @APP_VERSION_ID AND    
         LOCATION_ID = @LOCATION_ID AND    
         SUB_LOC_ID = @SUB_LOC_ID AND    
         DWELLING_ID <> @DWELLING_ID        
 )    
 BEGIN    
  RETURN -1    
 END    
     
 --Check If there is any entry for Client Dwelling number    
 IF EXISTS    
 (    
  SELECT DWELLING_ID     
  FROM APP_UMBRELLA_DWELLINGS_INFO    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
               APP_ID = @APP_ID AND    
         APP_VERSION_ID = @APP_VERSION_ID AND    
         DWELLING_NUMBER = @DWELLING_NUMBER AND    
         DWELLING_ID <> @DWELLING_ID     
       
 )    
 BEGIN    
  RETURN -2    
 END    
    
 UPDATE APP_UMBRELLA_DWELLINGS_INFO    
 SET     
  DWELLING_NUMBER = @DWELLING_NUMBER,      
  LOCATION_ID = @LOCATION_ID,    
  SUB_LOC_ID = @SUB_LOC_ID,    
  YEAR_BUILT = @YEAR_BUILT,    
  PURCHASE_YEAR = @PURCHASE_YEAR,    
  PURCHASE_PRICE = @PURCHASE_PRICE,    
  MARKET_VALUE = @MARKET_VALUE,    
  REPLACEMENT_COST = @REPLACEMENT_COST,    
  BUILDING_TYPE = @BUILDING_TYPE,    
  OCCUPANCY = @OCCUPANCY,    
  NEED_OF_UNITS = @NEED_OF_UNITS,    
  USAGE = @USAGE ,    
  NEIGHBOURS_VISIBLE = @NEIGHBOURS_VISIBLE,    
  OCCUPIED_DAILY = @OCCUPIED_DAILY,    
  NO_WEEKS_RENTED = @NO_WEEKS_RENTED,    
  MODIFIED_BY = @MODIFIED_BY,    
  LAST_UPDATED_DATETIME = GetDate(), IS_ACTIVE= @IS_ACTIVE,
  REPAIR_COST=@REPAIR_COST   
    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND     
  APP_VERSION_ID = @APP_VERSION_ID AND    
  DWELLING_ID = @DWELLING_ID     
    
     
 RETURN 1    
     
     
END    
    




GO

