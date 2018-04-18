IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAppDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAppDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------        
Proc Name       : dbo.Proc_InsertAppDwellingsInfo        
Created by      : Pradeep        
Date            : 26/04/2005        
Purpose         : To add record in profit center table        
Revison History :        
Used In         :   Wolverine        
        
Modified By : Anurag Verma        
Modified On : 07/09/2005        
Purpose  : Adding COMMENT_DWELLING_OWNED field for insertion        
        
Modified By : Anurag Verma        
Modified On : 12/09/2005        
Purpose  : Removing comment of COMMENT_DWELLING_OWNED field for insertion        
        
Modified By : Mohit Gupta        
Modified On : 23/09/2005        
Purpose  : commenting fields(IS_RENTED_IN_PART ,IS_VACENT_OCCUPY ,IS_DWELLING_OWNED_BY_OTHER ) as droped.         
      
Modified By : Vijay Arora      
Modified On : 19-10-2005       
Purpose  : Commenting the check (field name sublocation for checking the on duplicate location).    
    
Modified By :Mohit    
Modified On :11/11/2005    
Purpose     : Adding field IS_ACTIVE.       

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
		MONTHS_RENTED
      
        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_InsertAppDwellingsInfo
CREATE       PROC dbo.Proc_InsertAppDwellingsInfo        
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
 --@IS_VACENT_OCCUPY     nchar(1),        
 --@IS_RENTED_IN_PART     nchar(1),        
 @OCCUPIED_DAILY     nchar(1),        
 @NO_WEEKS_RENTED     smallint,        
 --@IS_DWELLING_OWNED_BY_OTHER     nchar(1),        
 @CREATED_BY     int,        
 --@COMMENT_DWELLING_OWNED nvarchar(100),        
 @DWELLING_ID int output,    
 @IS_ACTIVE char(1),
 @DETACHED_OTHER_STRUCTURES NVARCHAR(5),
/*
 @PREMISES_LOCATION NVARCHAR(5),
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
 @INSURING_VALUE_OFF_PREMISES INTEGER,
*/
 @MONTHS_RENTED integer = null,	
 @REPLACEMENTCOST_COVA NVARCHAR(20)  
)        
        
AS        
        
DECLARE @MAX BigInt        
        
        
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
				IS_ACTIVE = 'Y'
		
  --AND SUB_LOC_ID = @SUB_LOC_ID            
 )        
 BEGIN        
  SET @DWELLING_ID = -1        
  RETURN        
 END        
         
 --Duplicate entry for Client Dwelling number        
 IF EXISTS        
 (        
  SELECT *        
  FROM APP_DWELLINGS_INFO        
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
 FROM APP_DWELLINGS_INFO        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
       APP_ID = @APP_ID AND        
       APP_VERSION_ID = @APP_VERSION_ID        
         
         
 IF @MAX > 2147483647        
 BEGIN        
  SET @DWELLING_ID = -3        
  RETURN          
 END        
        
 INSERT INTO APP_DWELLINGS_INFO        
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
  --IS_VACENT_OCCUPY,        
  --IS_RENTED_IN_PART,        
  OCCUPIED_DAILY,        
  NO_WEEKS_RENTED,        
  --IS_DWELLING_OWNED_BY_OTHER,        
  CREATED_BY,        
  CREATED_DATETIME,    
  IS_ACTIVE  ,
DETACHED_OTHER_STRUCTURES,
/*
PREMISES_LOCATION,
PREMISES_DESCRIPTION,
PREMISES_USE,
PREMISES_CONDITION,
PICTURE_ATTACHED,
COVERAGE_BASIS,
SATELLITE_EQUIPMENT,
LOCATION_ADDRESS,
LOCATION_CITY,
LOCATION_STATE,
LOCATION_ZIP,
ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,
INSURING_VALUE,
INSURING_VALUE_OFF_PREMISES,	
*/
MONTHS_RENTED,

REPLACEMENTCOST_COVA
  
  --COMMENTDWELLINGOWNED        
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
  --@IS_VACENT_OCCUPY,        
  --@IS_RENTED_IN_PART,        
  @OCCUPIED_DAILY,        
  @NO_WEEKS_RENTED,        
  --@IS_DWELLING_OWNED_BY_OTHER,        
  @CREATED_BY,        
  GetDate(),    
   @IS_ACTIVE ,

   @DETACHED_OTHER_STRUCTURES,
/*
   @PREMISES_LOCATION,
   @PREMISES_DESCRIPTION,
   @PREMISES_USE,
   @PREMISES_CONDITION,
   @PICTURE_ATTACHED,
   @COVERAGE_BASIS,
   @SATELLITE_EQUIPMENT,
   @LOCATION_ADDRESS,
   @LOCATION_CITY,
   @LOCATION_STATE,
   @LOCATION_ZIP,
   @ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,
   @INSURING_VALUE,
   @INSURING_VALUE_OFF_PREMISES ,  
*/
  @MONTHS_RENTED,
@REPLACEMENTCOST_COVA
 )        
        
 SET @DWELLING_ID = @MAX        
         
         
END        
        
        
        








GO

