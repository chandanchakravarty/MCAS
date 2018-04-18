IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------          
Proc Name       : dbo.Proc_UpdatePolDwellingsInfo          
Created by      : Anurag Verma          
Date            : 11/10/2005          
Purpose         : Updates a record in POL_DWELLINGS_INFO          
Revison History :          
Used In         :   Wolverine          
          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/     
-- drop proc  dbo.Proc_UpdatePolDwellingsInfo              
CREATE PROC dbo.Proc_UpdatePolDwellingsInfo          
(          
 @CUSTOMER_ID     int,          
 @POL_ID     int,          
 @POL_VERSION_ID     smallint,          
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
 @Months_Rented   int,
 @REPLACEMENTCOST_COVA NVARCHAR(20)           
          
)          
          
AS          
          
          
          
BEGIN          
           
 --Duplicate entry for location and sub location          
IF EXISTS          
 (          
  SELECT *          
  FROM POL_DWELLINGS_INFO          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
               POLICY_ID = @POL_ID AND          
         POLICY_VERSION_ID = @POL_VERSION_ID AND          
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
  FROM POL_DWELLINGS_INFO          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
               POLICY_ID = @POL_ID AND          
         POLICY_VERSION_ID = @POL_VERSION_ID AND          
         DWELLING_NUMBER = @DWELLING_NUMBER AND    
         DWELLING_ID <> @DWELLING_ID           
             
 )          
 BEGIN          
  RETURN -2          
 END          
          
 UPDATE POL_DWELLINGS_INFO          
 SET           
  DWELLING_NUMBER = @DWELLING_NUMBER,            
  LOCATION_ID = @LOCATION_ID,            
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
  LAST_UPDATED_DATETIME = GetDate(),
Months_Rented   =  @Months_Rented,
REPLACEMENTCOST_COVA = @REPLACEMENTCOST_COVA   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
  POLICY_ID = @POL_ID AND           
  POLICY_VERSION_ID = @POL_VERSION_ID AND          
  DWELLING_ID = @DWELLING_ID           
          
           
 RETURN 1          
           
           
END          
          
          
          
          
  









GO

