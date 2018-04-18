IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_Save_APP_DWELLINGS_INFO_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_Save_APP_DWELLINGS_INFO_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE       PROC Dbo.Proc_UM_Save_APP_DWELLINGS_INFO_ACORD          
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
  @OCCUPANCY_CODE     NVarChar(10),          
  @NEED_OF_UNITS     smallint,          
  @USAGE     nvarchar(50),   
  @NEIGHBOURS_VISIBLE     nchar(1),          
  @OCCUPIED_DAILY     nchar(1) = NULL,          
  @NO_WEEKS_RENTED     smallint = NULL,          
  @CREATED_BY     int,          
  @MODIFIED_BY     int,          
  @DWELLING_ID int output          
          
)          
          
AS          
          
DECLARE @MAX BigInt          
DECLARE @OCC_ID Int          
DECLARE @DWELLING_ID_EXISTS smallint          
          
BEGIN          
           
         
 EXECUTE @OCC_ID = Proc_GetLookupID 'OCCUPA',@OCCUPANCY_CODE        
        
 --Check existence for this location          
 SELECT @DWELLING_ID_EXISTS = DWELLING_ID          
 FROM APP_UMBRELLA_DWELLINGS_INFO          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
              APP_ID = @APP_ID AND          
        APP_VERSION_ID = @APP_VERSION_ID AND          
        LOCATION_ID = @LOCATION_ID AND          
        ISNULL(SUB_LOC_ID,0) = ISNULL(@SUB_LOC_ID,0)              
           
           
 IF ( @DWELLING_ID_EXISTS IS NULL )          
 BEGIN           
        
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
         
 SELECT  @DWELLING_NUMBER = ISNULL(MAX(DWELLING_NUMBER),0) + 1          
   FROM APP_UMBRELLA_DWELLINGS_INFO          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
         APP_ID = @APP_ID AND          
         APP_VERSION_ID = @APP_VERSION_ID            
        
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
    IS_ACTIVE           
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
    @OCC_ID,          
    @NEED_OF_UNITS,          
    @USAGE,          
   @NEIGHBOURS_VISIBLE,          
    @OCCUPIED_DAILY,          
    @NO_WEEKS_RENTED,          
    @CREATED_BY,          
    GetDate(),    
    'Y'           
  )          
           
  SET @DWELLING_ID = @MAX          
            
  RETURN          
 END          
          
 --Update           
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
  LAST_UPDATED_DATETIME = GetDate()          
           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  APP_ID = @APP_ID AND           
  APP_VERSION_ID = @APP_VERSION_ID AND          
  DWELLING_ID = @DWELLING_ID_EXISTS           
          
 SET @DWELLING_ID = @DWELLING_ID_EXISTS          
END          





GO

