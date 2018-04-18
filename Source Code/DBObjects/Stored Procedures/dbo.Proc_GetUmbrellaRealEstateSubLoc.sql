IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaRealEstateSubLoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaRealEstateSubLoc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------  
Proc Name       : Dbo.Proc_GetUmbrellaRealEstateSubLoc  
Created by      : Priya  
Date            : 5/31/2005  
Purpose       : 
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetUmbrellaRealEstateSubLoc  
(  
 @CUSTOMER_ID  int,  
 @APP_ID      int,  
 @APP_VERSION_ID smallint,  
 @LOCATION_ID    smallint,  
 @SUB_LOC_ID     smallint  
)  
AS  
BEGIN  
  
 SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID, LOCATION_ID,  
  SUB_LOC_ID, SUB_LOC_NUMBER, SUB_LOC_TYPE, SUB_LOC_DESC,  
  SUB_LOC_CITY_LIMITS, SUB_LOC_INTEREST, SUB_LOC_OCCUPIED_PERCENT,  
  SUB_LOC_OCC_DESC,CREATED_BY, CREATED_DATETIME  
 FROM APP_UMBRELLA_REAL_ESTATE_SUB_LOC  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID AND  
  LOCATION_ID = @LOCATION_ID AND  
  SUB_LOC_ID = @SUB_LOC_ID  
    
END  
  
  
  
  




GO

