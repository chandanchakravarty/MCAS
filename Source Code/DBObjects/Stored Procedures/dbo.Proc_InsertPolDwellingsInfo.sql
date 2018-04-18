IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolDwellingsInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolDwellingsInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertPolDwellingsInfo          
Created by      : Anurag verma        
Date            : 11/11/2005          
Purpose         : To add record in pol_dwelling_info         
Revison History :          
Used In         :   Wolverine          
      
Modified By     : Shafi              
Modified On     : 17/02/06                
Purpose         : Adding IS_ACTIVE Default Y               
      
            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--drop proc Proc_InsertPolDwellingsInfo      

CREATE PROC dbo.Proc_InsertPolDwellingsInfo          
(          
 @CUSTOMER_ID     int,          
 @POL_ID     int,          
 @POL_VERSION_ID     smallint,          
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
 @DWELLING_ID int output   ,
 @MONTHS_RENTED int ,
 @REPLACEMENTCOST_COVA NVARCHAR(20)
          
)          
          
AS          
          
DECLARE @MAX BigInt          
          
          
BEGIN          
           
 --Duplicate entry for location and sub location          
 IF EXISTS          
 (          
  SELECT *          
  FROM POL_DWELLINGS_INFO          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
               POLICY_ID = @POL_ID AND          
         POLICY_VERSION_ID = @POL_VERSION_ID AND          
         LOCATION_ID = @LOCATION_ID      
    and is_active = 'Y'       
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
  FROM POL_DWELLINGS_INFO          
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
               POLICY_ID = @POL_ID AND          
         POLICY_VERSION_ID = @POL_VERSION_ID AND          
         DWELLING_NUMBER = @DWELLING_NUMBER  and IS_ACTIVE='Y'         
             
 )          
 BEGIN          
  SET @DWELLING_ID = -2          
  RETURN          
 END          
          
 SELECT  @MAX = ISNULL(MAX(DWELLING_ID),0) + 1          
 FROM POL_DWELLINGS_INFO          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
       POLICY_ID = @POL_ID AND          
       POLICY_VERSION_ID = @POL_VERSION_ID          
           
           
 IF @MAX > 2147483647          
 BEGIN          
  SET @DWELLING_ID = -3          
  RETURN            
 END          
          
 INSERT INTO POL_DWELLINGS_INFO          
 (          
  CUSTOMER_ID,          
  POLICY_ID,          
  POLICY_VERSION_ID,          
  DWELLING_ID,          
  DWELLING_NUMBER,          
  LOCATION_ID,          
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
  IS_ACTIVE,
MONTHS_RENTED,
REPLACEMENTCOST_COVA
         
  --COMMENTDWELLINGOWNED          
 )          
 VALUES          
 (          
  @CUSTOMER_ID,          
  @POL_ID,          
  @POL_VERSION_ID,          
  @MAX,          
  @DWELLING_NUMBER,          
  @LOCATION_ID,          
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
  'Y'
,@MONTHS_RENTED,
 @REPLACEMENTCOST_COVA
  --@COMMENT_DWELLING_OWNED          
)          
          
 SET @DWELLING_ID = @MAX          
           
           
END          
          
          
          
          
        
        
        
      
    
  









GO

