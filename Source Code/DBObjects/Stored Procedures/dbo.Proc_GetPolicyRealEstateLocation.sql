IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetPolicyRealEstateLocation      
Created by         : Ravindra    
Date               : 03-21-2006    
Purpose            : To get details from POL_UMBRELLA_REAL_ESTATE_LOCATION      
Revison History    :      
Used In            : Wolverine      
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/   
--drop PROC dbo.Proc_GetPolicyRealEstateLocation     
CREATE PROC dbo.Proc_GetPolicyRealEstateLocation    
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID smallint,      
 @LOCATION_ID   smallint      
          
)      
      
AS      
BEGIN      
 SELECT CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, CLIENT_LOCATION_NUMBER ,LOCATION_ID ,LOCATION_NUMBER,      
               ADDRESS_1, ADDRESS_2,CITY, COUNTY, STATE, ZIPCODE, PHONE_NUMBER , FAX_NUMBER, IS_ACTIVE,REMARKS,  
    OCCUPIED_BY,NUM_FAMILIES,BUSS_FARM_PURSUITS,BUSS_FARM_PURSUITS_DESC,LOC_EXCLUDED,PERS_INJ_COV_82,OTHER_POLICY  
           
 FROM POL_UMBRELLA_REAL_ESTATE_LOCATION      
      
 WHERE   CUSTOMER_ID = @CUSTOMER_ID AND      
  POLICY_ID = @POLICY_ID AND      
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
  LOCATION_ID    =@LOCATION_ID      
      
END      
      
      
      
      
      
      
      
      
    
  



GO

