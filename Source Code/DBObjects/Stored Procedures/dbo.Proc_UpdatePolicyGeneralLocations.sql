IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyGeneralLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyGeneralLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo. Proc_UpdatePolicyGeneralLocations      
Created by      : Sumit Chhabra      
Date            : 03/04/2006    
Purpose         Update record to POL_LOCATIONS      
Revison History :      
Used In        : Wolverine      
---------------------------------------------      
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
create   PROCEDURE  Proc_UpdatePolicyGeneralLocations      
(      
 @CUSTOMER_ID         int,      
 @POLICY_ID              int,      
 @POLICY_VERSION_ID      int,      
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
/*If Not Exists(SELECT LOC_NUM FROM POL_LOCATIONS       
  WHERE LOC_NUM = @LOC_NUM AND      
  CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID      
AND LOCATION_ID <> @LOCATION_ID )       
BEGIN     */  
UPDATE  POL_LOCATIONS      
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
LOC_COUNTRY=@LOC_COUNTRY     
      
WHERE  CUSTOMER_ID=@CUSTOMER_ID       
AND        POLICY_ID=@POLICY_ID       
AND        POLICY_VERSION_ID=@POLICY_VERSION_ID      
--AND       LOCATION_ID=@LOCATION_ID      
END      
--END    
    
  



GO

