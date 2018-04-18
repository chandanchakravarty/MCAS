IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateAppDwellingsInfo    
Created by      : Pradeep    
Date            : 11/05/2005    
Purpose         : Updates a record in APP_DWELLINGS_INFO    
Revison History :    
Used In         :   Wolverine    
    
Modified By : Anurag verma    
Modified On : 07/09/2005    
Purpose  : Adding COMMENT_DWELLING_OWNED field for updation    
    
Modified By : Mohit Gupta    
Modified On : 23/09/2005    
Purpose  : commenting fields(IS_RENTED_IN_PART ,IS_VACENT_OCCUPY ,IS_DWELLING_OWNED_BY_OTHER ) as droped.     

Modified By :RPSINGH
Modified On :12/June/2006
Purpose     : Adding following fields
		DETACHED_OTHER_STRUCTURES
		PREMISES_LOCATION
		PREMISES_DESCRIPTION
		PREMISES_USE
		PREMISES_CONDITION
		PICTURE_ATTACHED
		COVERAGE_BASIS
		SATELLITE_EQUIPMENT
		LOCATION_ADDRESS
		LOCATION_CITY
		LOCATION_STATE
		LOCATION_ZIP		
		ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED
		INSURING_VALUE
		INSURING_VALUE_OFF_PREMISES
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_UpdateAppDwellingsInfo
CREATE   PROC dbo.Proc_UpdateAppDwellingsInfo    
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
 --@IS_VACENT_OCCUPY     nchar(1),    
 --@IS_RENTED_IN_PART     nchar(1),    
 @OCCUPIED_DAILY     nchar(1),    
 @NO_WEEKS_RENTED     smallint,    
 --@IS_DWELLING_OWNED_BY_OTHER     nchar(1),    
 @MODIFIED_BY     int,  
 @IS_ACTIVE CHAR,

@DETACHED_OTHER_STRUCTURES NVARCHAR(5),
/* @PREMISES_LOCATION NVARCHAR(5),
 @PREMISES_DESCRIPTION NVARCHAR(300),
 @PREMISES_USE NVARCHAR(150),
 @PREMISES_CONDITION NVARCHAR(5),
 @PICTURE_ATTACHED NVARCHAR(5),
 @COVERAGE_BASIS NVARCHAR(5),
 @SATELLITE_EQUIPMENT NVARCHAR(5),
 @LOCATION_ADDRESS NVARCHAR(100),
 @LOCATION_CITY NVARCHAR(50),
 @LOCATION_STATE NVARCHAR(5),
 @LOCATION_ZIP NVARCHAR(20),
 @ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED INTEGER,
 @INSURING_VALUE INTEGER,
 @INSURING_VALUE_OFF_PREMISES INTEGER,  */
 @MONTHS_RENTED INTEGER = null,
  @REPLACEMENTCOST_COVA NVARCHAR(20)
    
)    
    
AS    
    
    
    
BEGIN    
     
 --Duplicate entry for location and sub location    
 IF EXISTS    
 (    
  SELECT *    
  FROM APP_DWELLINGS_INFO    
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
     
 --Duplicate entry for Client Dwelling number    
 IF EXISTS    
 (    
  SELECT *    
  FROM APP_DWELLINGS_INFO    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
               APP_ID = @APP_ID AND    
         APP_VERSION_ID = @APP_VERSION_ID AND    
         DWELLING_NUMBER = @DWELLING_NUMBER AND    
         DWELLING_ID <> @DWELLING_ID     
       
 )    
 BEGIN    
  RETURN -2    
 END    
    
 UPDATE APP_DWELLINGS_INFO    
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
  --IS_VACENT_OCCUPY = @IS_VACENT_OCCUPY,    
  --IS_RENTED_IN_PART = @IS_RENTED_IN_PART,    
  OCCUPIED_DAILY = @OCCUPIED_DAILY,    
  NO_WEEKS_RENTED = @NO_WEEKS_RENTED,    
  --IS_DWELLING_OWNED_BY_OTHER = @IS_DWELLING_OWNED_BY_OTHER,    
  MODIFIED_BY = @MODIFIED_BY,    
  LAST_UPDATED_DATETIME = GetDate(), IS_ACTIVE= @IS_ACTIVE,

DETACHED_OTHER_STRUCTURES = @DETACHED_OTHER_STRUCTURES,
/*PREMISES_LOCATION = @PREMISES_LOCATION,
PREMISES_DESCRIPTION = @PREMISES_DESCRIPTION,
PREMISES_USE = @PREMISES_USE ,
PREMISES_CONDITION = @PREMISES_CONDITION ,
PICTURE_ATTACHED = @PICTURE_ATTACHED ,
COVERAGE_BASIS = @COVERAGE_BASIS ,
SATELLITE_EQUIPMENT = @SATELLITE_EQUIPMENT ,
LOCATION_ADDRESS = @LOCATION_ADDRESS ,
LOCATION_CITY = @LOCATION_CITY ,
LOCATION_STATE = @LOCATION_STATE, 
LOCATION_ZIP = @LOCATION_ZIP ,
ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED = @ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED ,
INSURING_VALUE = @INSURING_VALUE ,
INSURING_VALUE_OFF_PREMISES = @INSURING_VALUE_OFF_PREMISES ,*/
MONTHS_RENTED = @MONTHS_RENTED,
REPLACEMENTCOST_COVA = @REPLACEMENTCOST_COVA

--  COMMENTDWELLINGOWNED=@COMMENT_DWELLING_OWNED    
     
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND     
  APP_VERSION_ID = @APP_VERSION_ID AND    
  DWELLING_ID = @DWELLING_ID     
    
     
 RETURN 1    
     
     
END    
    
    
  











GO

