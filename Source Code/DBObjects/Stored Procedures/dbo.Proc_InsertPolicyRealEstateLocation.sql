IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertPolicyRealEstateLocation          
Created by      : Ravindra      
Date            : 03-21-2006      
Purpose         : To add record in policy real estate table          
Revison History :          
Used In         :   Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      
--drop proc Proc_InsertPolicyRealEstateLocation              
CREATE PROC dbo.Proc_InsertPolicyRealEstateLocation          
 (          
 @CUSTOMER_ID              int,          
 @POLICY_ID                   int,          
 @POLICY_VERSION_ID           smallint ,        
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
 @OTHER_POLICY nvarchar (150)         
)          
AS          
         
BEGIN        
        
If Exists(SELECT LOCATION_NUMBER FROM POL_UMBRELLA_REAL_ESTATE_LOCATION         
  WHERE LOCATION_NUMBER = @LOCATION_NUMBER AND        
  CUSTOMER_ID = @CUSTOMER_ID AND        
  POLICY_ID = @POLICY_ID AND        
  POLICY_VERSION_ID = @POLICY_VERSION_ID )         
      
 Return -2    
          
 SELECT @LOCATION_ID = ISNULL(Max(LOCATION_ID),0)+1          
    FROM POL_UMBRELLA_REAL_ESTATE_LOCATION            
        
        
          
 INSERT INTO POL_UMBRELLA_REAL_ESTATE_LOCATION          
  (          
 CUSTOMER_ID,             
 POLICY_ID,                         
 POLICY_VERSION_ID,                   
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
 @POLICY_ID ,                         
 @POLICY_VERSION_ID ,                   
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
        
        
        
        
      
    
  



GO

