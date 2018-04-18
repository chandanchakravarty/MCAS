IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
     
 /*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetRealEstateLocation    
Created by         : Priya    
Date               : 25/05/2005    
Purpose            : To get details from APP_UMBRELLA_REAL_ESTATE_LOCATION    
Revison History :    
Used In                :   Wolverine    
Modified By     : Mohit Gupta    
Modified On        : 19/10/2005    
Purpose            : Adding field REMARKS in select clause.       
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/  
--drop PROC dbo.Proc_GetRealEstateLocation  
CREATE PROC dbo.Proc_GetRealEstateLocation    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID smallint,    
 @LOCATION_ID   smallint    
        
)    
    
AS    
BEGIN    
 SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID, CLIENT_LOCATION_NUMBER ,LOCATION_ID ,LOCATION_NUMBER,    
               ADDRESS_1, ADDRESS_2,CITY, COUNTY, STATE, ZIPCODE, PHONE_NUMBER , FAX_NUMBER, IS_ACTIVE,REMARKS,  
    OCCUPIED_BY,NUM_FAMILIES,BUSS_FARM_PURSUITS,BUSS_FARM_PURSUITS_DESC,LOC_EXCLUDED,PERS_INJ_COV_82,OTHER_POLICY  
         
   FROM APP_UMBRELLA_REAL_ESTATE_LOCATION    
    
 WHERE   CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
                APP_VERSION_ID = @APP_VERSION_ID AND    
  LOCATION_ID    =@LOCATION_ID    
    
END    
    
    
    
    
    
    
    
    
  



GO

