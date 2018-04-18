IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_UpdateRealEstateLocation      
Created by      : Priya      
Date            : 5/25/2005      
Purpose         : To update  record in real estate table      
Revison History :      
Used In         :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
--drop PROC dbo.Proc_UpdateRealEstateLocation      
CREATE PROC dbo.Proc_UpdateRealEstateLocation      
(      
 @CUSTOMER_ID              int,      
 @APP_ID                   int,      
 @APP_VERSION_ID           smallint,      
 @CLIENT_LOCATION_NUMBER   nvarchar(20),      
 @LOCATION_ID              smallint,       
 @LOCATION_NUMBER         int,     
 @ADDRESS_1                nvarchar(75),      
 @ADDRESS_2                nvarchar(75),      
 @CITY                     nvarchar(75) ,      
 @COUNTY                   nvarchar(75),      
 @STATE                    int,      
 @ZIPCODE                  nvarchar(10),      
 @PHONE_NUMBER             nvarchar(15),      
 @FAX_NUMBER               nvarchar(15) ,      
 @MODIFIED_BY               int,      
 @LAST_UPDATED_DATETIME         datetime,  
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
  APP_VERSION_ID = @APP_VERSION_ID AND    
  LOCATION_ID <> @LOCATION_ID)     
  BEGIN    
 UPDATE APP_UMBRELLA_REAL_ESTATE_LOCATION      
     
 SET    
CUSTOMER_ID=@CUSTOMER_ID ,         
APP_ID=@APP_ID ,                     
APP_VERSION_ID=@APP_VERSION_ID ,               
CLIENT_LOCATION_NUMBER=@CLIENT_LOCATION_NUMBER ,    
LOCATION_NUMBER=@LOCATION_NUMBER,           
ADDRESS_1=@ADDRESS_1,                 
ADDRESS_2=@ADDRESS_2 ,                    
COUNTY=@COUNTY  ,                     
STATE = @STATE,                    
ZIPCODE =@ZIPCODE,                    
PHONE_NUMBER =@PHONE_NUMBER,               
FAX_NUMBER=@FAX_NUMBER,      
MODIFIED_BY=@MODIFIED_BY   ,               
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
OCCUPIED_BY=@OCCUPIED_BY,  
NUM_FAMILIES=@NUM_FAMILIES,  
BUSS_FARM_PURSUITS=@BUSS_FARM_PURSUITS,  
BUSS_FARM_PURSUITS_DESC=@BUSS_FARM_PURSUITS_DESC,  
LOC_EXCLUDED=@LOC_EXCLUDED,  
PERS_INJ_COV_82=@PERS_INJ_COV_82,  
OTHER_POLICY=@OTHER_POLICY          
 WHERE     
    
 CUSTOMER_ID=@CUSTOMER_ID AND         
 APP_ID=@APP_ID AND                     
 APP_VERSION_ID=@APP_VERSION_ID AND    
 LOCATION_ID =@LOCATION_ID         
      
END    
END    
    
  



GO

