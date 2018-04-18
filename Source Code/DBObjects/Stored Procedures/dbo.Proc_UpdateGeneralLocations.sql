IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateGeneralLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateGeneralLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo. Proc_UpdateGeneralLocations      
Created by      : Priya      
Date            : 8/23/2005      
Purpose       Updating record to APP_LOCATIONS      
Revison History :      
Used In        : Wolverine      
---------------------------------------------      
Modified By              :  Mohit Gupta      
Modified On             :  9/15/2005      
Purpose                   :  Including  LOC_TERRITORY field in update statement.      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
create   PROCEDURE  Proc_UpdateGeneralLocations      
(      
 @CUSTOMER_ID         int,      
 @APP_ID              int,      
 @APP_VERSION_ID      int,      
-- @LOCATION_ID         smallint,      
 @LOC_NUM             int,      
 @LOC_ADD1        nvarchar(75),      
 @LOC_ADD2            nvarchar(75),      
 @LOC_CITY            nvarchar(75),      
 @LOC_COUNTY      nvarchar(75),      
 @LOC_STATE      nvarchar(5),      
 @LOC_ZIP      nvarchar(10),      
 @IS_ACTIVE      nchar(1),      
 @MODIFIED_BY         int,      
 @LAST_UPDATED_DATETIME datetime,      
 @LOC_TERRITORY                   nvarchar(5),
 @LOC_COUNTRY nvarchar(5) 
       
)      
AS      
BEGIN      
/*If Not Exists(SELECT LOC_NUM FROM APP_LOCATIONS       
  WHERE LOC_NUM = @LOC_NUM AND      
  CUSTOMER_ID = @CUSTOMER_ID AND      
  APP_ID = @APP_ID AND      
  APP_VERSION_ID = @APP_VERSION_ID  )    
--AND LOCATION_ID <> @LOCATION_ID )       
BEGIN*/       
UPDATE  APP_LOCATIONS      
SET      
LOC_NUM=@LOC_NUM,      
LOC_ADD1=@LOC_ADD1,      
LOC_ADD2=@LOC_ADD2,      
LOC_CITY = @LOC_CITY,      
LOC_COUNTY=@LOC_COUNTY,      
LOC_STATE=@LOC_STATE,      
LOC_ZIP=@LOC_ZIP,      
IS_ACTIVE=@IS_ACTIVE,      
MODIFIED_BY=@MODIFIED_BY,      
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,      
LOC_TERRITORY=@LOC_TERRITORY,
LOC_COUNTRY = @LOC_COUNTRY
      
WHERE  CUSTOMER_ID=@CUSTOMER_ID       
AND        APP_ID=@APP_ID       
AND        APP_VERSION_ID=@APP_VERSION_ID      
--AND       LOCATION_ID=@LOCATION_ID      
END      
--END    
    
  



GO

