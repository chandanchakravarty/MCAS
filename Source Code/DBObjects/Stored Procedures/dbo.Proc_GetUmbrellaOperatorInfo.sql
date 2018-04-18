IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaOperatorInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaOperatorInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name      : dbo.Proc_GetUmbrellaOperatorInfo      
Created by       : Sumit Chhabra  
Date             : 24/10/2005      
Purpose       : retrieving data from APP_WATERCRAFT_driver_INFO      
Revison History :      
Used In        : Wolverine      
      
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetUmbrellaOperatorInfo      
@CUSTOMER_ID INT,      
@APP_ID INT,      
@APP_VERSION_ID INT,      
@DRIVER_ID INT      
AS      
BEGIN      
SELECT      
DRIVER_FNAME,      
DRIVER_MNAME,      
DRIVER_LNAME,      
DRIVER_CODE,      
DRIVER_SUFFIX,      
DRIVER_ADD1,      
DRIVER_ADD2,      
DRIVER_CITY,      
DRIVER_STATE,      
DRIVER_ZIP,      
DRIVER_COUNTRY,      
CONVERT(varchar(10),DRIVER_DOB,101) as DRIVER_DOB,      
DRIVER_SSN,      
DRIVER_SEX,      
DRIVER_DRIV_LIC,      
DRIVER_LIC_STATE,      
DRIVER_COST_GAURAD_AUX,      
vehicle_id,      
ceiling(percent_driven)percent_driven,
IS_ACTIVE,    
ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) AS APP_VEHICLE_PRIN_OCC_ID      
FROM APP_UMBRELLA_OPERATOR_INFO       
WHERE APP_ID=@APP_ID AND       
APP_VERSION_ID=@APP_VERSION_ID      
AND CUSTOMER_ID=@CUSTOMER_ID AND      
DRIVER_ID=@DRIVER_ID      
END    
    
  

  



GO

