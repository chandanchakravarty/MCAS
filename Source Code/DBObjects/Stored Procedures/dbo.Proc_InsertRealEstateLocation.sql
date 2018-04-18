IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_InsertRealEstateLocation      
Created by      : Priya      
Date            : 5/25/2005      
Purpose         : To add record in real estate table      
Revison History :      
Used In         :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/ 
--drop PROC dbo.Proc_InsertRealEstateLocation      
CREATE PROC dbo.Proc_InsertRealEstateLocation      
(      
 @CUSTOMER_ID              int,      
 @APP_ID                   int,      
 @APP_VERSION_ID           smallint ,    
 @CLIENT_LOCATION_NUMBER   nvarchar(20),      
 @LOCATION_ID              smallint OUTPUT,      
 @LOCATION_NUMBER          int,    
 @ADDRESS_1                nvarchar(75),      
 @ADDRESS_2                nvarchar(75),      
 @CITY                     nvarchar(75) ,      
 @COUNTY                   nvarchar(75),      
 @STATE                    int,      
 @ZIPCODE                  nvarchar(10),      
 @PHONE_NUMBER             nvarchar(15),      
 @FAX_NUMBER               nvarchar(15) ,    
   
 @CREATED_BY               int,      
 @CREATED_DATETIME         datetime,  
 @OCCUPIED_BY smallint,  
 @NUM_FAMILIES smallint,  
 @BUSS_FARM_PURSUITS smallint,  
 @BUSS_FARM_PURSUITS_DESC varchar(50),  
 @LOC_EXCLUDED smallint,  
 @PERS_INJ_COV_82 smallint,
 @OTHER_POLICY nvarchar(150) = null   
)      
AS      
     
BEGIN    
    
If Not Exists(SELECT LOCATION_NUMBER FROM APP_UMBRELLA_REAL_ESTATE_LOCATION     
  WHERE LOCATION_NUMBER = @LOCATION_NUMBER AND    
  CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID )     
BEGIN     
      
 SELECT @LOCATION_ID = ISNULL(Max(LOCATION_ID),0)+1      
    FROM APP_UMBRELLA_REAL_ESTATE_LOCATION        
    
    
      
 INSERT INTO APP_UMBRELLA_REAL_ESTATE_LOCATION      
  (      
 CUSTOMER_ID,         
 APP_ID,                     
 APP_VERSION_ID,               
 CLIENT_LOCATION_NUMBER,    
 LOCATION_NUMBER,    
 LOCATION_ID,            
 ADDRESS_1,                 
 ADDRESS_2,    
 CITY,                    
 COUNTY,                     
 STATE,                    
 ZIPCODE,                    
 PHONE_NUMBER,               
 FAX_NUMBER,     
 IS_ACTIVE,    
 CREATED_BY,               
 CREATED_DATETIME,  
 OCCUPIED_BY,  
 NUM_FAMILIES,  
 BUSS_FARM_PURSUITS,  
 BUSS_FARM_PURSUITS_DESC,  
 LOC_EXCLUDED,  
 PERS_INJ_COV_82,
 OTHER_POLICY  
 )         
  values      
 (      
     
 @CUSTOMER_ID ,         
 @APP_ID ,                     
 @APP_VERSION_ID ,               
 @CLIENT_LOCATION_NUMBER ,    
 @LOCATION_NUMBER,    
 @LOCATION_ID ,            
 @ADDRESS_1,                 
 @ADDRESS_2 ,    
 @CITY,                   
 @COUNTY  ,                     
 @STATE  ,                    
 @ZIPCODE  ,                    
 @PHONE_NUMBER ,               
 @FAX_NUMBER,     
 'Y',    
 @CREATED_BY   ,               
 @CREATED_DATETIME,  
 @OCCUPIED_BY,  
 @NUM_FAMILIES,  
 @BUSS_FARM_PURSUITS,  
 @BUSS_FARM_PURSUITS_DESC,  
 @LOC_EXCLUDED,  
 @PERS_INJ_COV_82,
 @OTHER_POLICY    
    )      
                    
END    
    
END    
    
    
    
    
  



GO

