IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_InsertAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_InsertAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE     PROC Dbo.Proc_UM_InsertAppDwellingsInfo        
(        
 @CUSTOMER_ID     int,        
 @APP_ID     int,        
 @APP_VERSION_ID     smallint,        
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
 @CREATED_BY     int,        
 @DWELLING_ID int output,    
 @IS_ACTIVE char(1),  
 @REPAIR_COST DECIMAL(10)        
)        
        
AS        
        
DECLARE @MAX BigInt        
        
        
BEGIN        
         
 IF EXISTS        
 (        
  SELECT DWELLING_ID      
  FROM APP_UMBRELLA_DWELLINGS_INFO        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
               APP_ID = @APP_ID AND        
         APP_VERSION_ID = @APP_VERSION_ID AND        
         LOCATION_ID = @LOCATION_ID AND
				IS_ACTIVE = 'Y'
		
 )        
 BEGIN        
  SET @DWELLING_ID = -1        
  RETURN        
 END        
         
 IF EXISTS        
 (        
  SELECT DWELLING_ID         
  FROM APP_UMBRELLA_DWELLINGS_INFO        
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
               APP_ID = @APP_ID AND        
         APP_VERSION_ID = @APP_VERSION_ID AND        
         DWELLING_NUMBER = @DWELLING_NUMBER         
           
 )        
 BEGIN        
  SET @DWELLING_ID = -2        
  RETURN        
 END        
        
 SELECT  @MAX = ISNULL(MAX(DWELLING_ID),0) + 1        
 FROM APP_UMBRELLA_DWELLINGS_INFO        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
       APP_ID = @APP_ID AND        
       APP_VERSION_ID = @APP_VERSION_ID        
         
         
 IF @MAX > 2147483647        
 BEGIN        
  SET @DWELLING_ID = -3        
  RETURN          
 END        
        
 INSERT INTO APP_UMBRELLA_DWELLINGS_INFO        
 (        
  CUSTOMER_ID,        
  APP_ID,        
  APP_VERSION_ID,        
  DWELLING_ID,        
  DWELLING_NUMBER,        
  LOCATION_ID,        
  SUB_LOC_ID,        
  YEAR_BUILT,        
  PURCHASE_YEAR,        
  PURCHASE_PRICE,        
  MARKET_VALUE,        
  REPLACEMENT_COST,        
  BUILDING_TYPE,        
  OCCUPANCY,        
  NEED_OF_UNITS,        
  USAGE,        
  NEIGHBOURS_VISIBLE,        
  OCCUPIED_DAILY,        
  NO_WEEKS_RENTED,        
  CREATED_BY,        
  CREATED_DATETIME,    
  IS_ACTIVE,  
  REPAIR_COST      
 )        
 VALUES        
 (        
  @CUSTOMER_ID,        
  @APP_ID,        
  @APP_VERSION_ID,        
  @MAX,        
  @DWELLING_NUMBER,        
  @LOCATION_ID,        
  @SUB_LOC_ID,        
  @YEAR_BUILT,        
  @PURCHASE_YEAR,        
  @PURCHASE_PRICE,        
  @MARKET_VALUE,        
  @REPLACEMENT_COST,        
  @BUILDING_TYPE,        
  @OCCUPANCY,        
  @NEED_OF_UNITS,        
  @USAGE,        
  @NEIGHBOURS_VISIBLE,        
  @OCCUPIED_DAILY,        
  @NO_WEEKS_RENTED,        
  @CREATED_BY,        
  GetDate(),    
  @IS_ACTIVE,  
  @REPAIR_COST     
  
 )        
        
 SET @DWELLING_ID = @MAX        
         
         
END        



GO

