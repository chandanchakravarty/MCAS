IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetPolicyDwellingInfoByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetPolicyDwellingInfoByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UM_GetPolicyDwellingInfoByID            
Created by      : Ravindra
Date            : 03-21-2006
Purpose         : To fetch dwelling details
Revison History :       
Used In         :   Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   


CREATE  PROCEDURE Proc_UM_GetPolicyDwellingInfoByID      
(      
       
 @CUSTOMER_ID Int,      
 @POLICY_ID Int,      
 @POLICY_VERSION_ID SmallInt,      
 @DWELLING_ID SmallInt      
)      
      
As      
      
SELECT       
 CUSTOMER_ID,      
 POLICY_ID,      
 POLICY_VERSION_ID,      
 DWELLING_ID,      
 DWELLING_NUMBER,      
 LOCATION_ID,      
 SUB_LOC_ID,      
 YEAR_BUILT,      
 PURCHASE_YEAR,      
 floor(PURCHASE_PRICE) PURCHASE_PRICE,      
 floor(MARKET_VALUE) MARKET_VALUE,      
 floor(REPLACEMENT_COST) REPLACEMENT_COST,      
 BUILDING_TYPE,      
 OCCUPANCY,      
 NEED_OF_UNITS,      
 USAGE,      
 NEIGHBOURS_VISIBLE,      
 OCCUPIED_DAILY,      
 NO_WEEKS_RENTED,      
 IS_ACTIVE,      
 REPAIR_COST,    
 CREATED_BY,      
 CREATED_DATETIME,      
 MODIFIED_BY,      
 LAST_UPDATED_DATETIME,      
 CONVERT(VARCHAR(10),LOCATION_ID) + ',' +       
 CONVERT(VARCHAR(10),ISNULL(SUB_LOC_ID,0)) as LOC_SUBLOC       
       
FROM POL_UMBRELLA_DWELLINGS_INFO      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      POLICY_ID = @POLICY_ID AND       
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
      DWELLING_ID = @DWELLING_ID      
  
  



GO

