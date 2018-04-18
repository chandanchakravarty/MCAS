IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertGeneralLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertGeneralLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------- --        
Proc Name        : dbo.Proc_InsertGeneralLocations        
Created by       : Priya        
Date             : 8/18/2005        
Purpose       :To insert values in APP_GENERAL_EXPOSURE_RATING        
Revison History :        
Used In        : Wolverine        
--------------------------------------------------------------        
Modified By              :  Mohit Gupta        
Modified On             :  9/15/2005        
Purpose                   :  Including  LOC_TERRITORY field in insert statement.        
------   ------------       -------------------------*/        
create PROC Dbo.Proc_InsertGeneralLocations        
(        
 @CUSTOMER_ID          int,        
 @APP_ID              int,        
 @APP_VERSION_ID         smallint,        
-- @LOCATION_ID            smallint OUTPUT,        
 @LOC_NUM  int,        
 @LOC_ADD1  nvarchar(75),        
 @LOC_ADD2         nvarchar(75),        
 @LOC_CITY               nvarchar(75),        
 @LOC_COUNTY  nvarchar(75),        
 @LOC_STATE  nvarchar(5),        
 @LOC_ZIP  nvarchar(10),        
 @IS_ACTIVE  nchar(1),        
 @CREATED_BY      int,        
 @CREATED_DATETIME      datetime,        
 @LOC_TERRITORY                   nvarchar(5),
 @LOC_COUNTRY nvarchar(5)         
         
)        
AS        
BEGIN        
declare @LOCATION_ID int  
 If Not Exists(SELECT LOC_NUM FROM APP_LOCATIONS         
  WHERE LOC_NUM = @LOC_NUM AND        
  CUSTOMER_ID = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND        
  APP_VERSION_ID = @APP_VERSION_ID )         
BEGIN         
   select @LOCATION_ID= isnull(max(LOCATION_ID)+1,1) from APP_LOCATIONS where       
       CUSTOMER_ID = @CUSTOMER_ID AND  APP_ID = @APP_ID AND  APP_VERSION_ID = @APP_VERSION_ID       
          INSERT INTO APP_LOCATIONS        
  (        
   CUSTOMER_ID,        
   APP_ID,        
   APP_VERSION_ID,        
   LOCATION_ID,        
   LOC_NUM ,        
   LOC_ADD1,        
   LOC_ADD2,        
   LOC_CITY,        
   LOC_COUNTY,        
   LOC_STATE,        
   LOC_ZIP,        
   IS_ACTIVE,        
   CREATED_BY,        
   CREATED_DATETIME,        
   LOC_TERRITORY,
   LOC_COUNTRY
  )        
  VALUES        
  (        
   @CUSTOMER_ID,        
   @APP_ID,        
   @APP_VERSION_ID,        
   @LOCATION_ID,        
   @LOC_NUM,        
   @LOC_ADD1,        
   @LOC_ADD2,        
   @LOC_CITY,        
   @LOC_COUNTY,        
   @LOC_STATE,        
   @LOC_ZIP,        
   @IS_ACTIVE,        
   @CREATED_BY,        
   @CREATED_DATETIME,        
   @LOC_TERRITORY,
   @LOC_COUNTRY 
  )        
END        
END      
      
    
  



GO

