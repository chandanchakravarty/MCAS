IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyGeneralLocations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyGeneralLocations]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name        : dbo.Proc_InsertPolicyGeneralLocations      
Created by       : Sumit Chhabra      
Date             : 03/04/2006    
Purpose         :To insert values in POL_LOCATIONS      
Revison History :      
Used In        : Wolverine      
------   ------------       -------------------------*/      
create PROC Dbo.Proc_InsertPolicyGeneralLocations      
(      
 @CUSTOMER_ID          int,      
 @POLICY_ID              int,      
 @POLICY_VERSION_ID         smallint,      
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
 If Not Exists(SELECT LOC_NUM FROM POL_LOCATIONS       
  WHERE LOC_NUM = @LOC_NUM AND      
  CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID )       
BEGIN       
   select @LOCATION_ID= isnull(max(LOCATION_ID)+1,1) from POL_LOCATIONS       
          INSERT INTO POL_LOCATIONS      
  (      
   CUSTOMER_ID,      
   POLICY_ID,      
   POLICY_VERSION_ID,      
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
   @POLICY_ID,      
   @POLICY_VERSION_ID,      
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

