IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETWATERCRAFTDRIVERINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETWATERCRAFTDRIVERINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name      : dbo.Proc_GetWatercraftDriverInfo              
Created by       : Nidhi               
Date             : 5/18/2005              
Purpose       : retrieving data from APP_WATERCRAFT_driver_INFO              
Revison History :              
Used In        : Wolverine              
              
Modified By : Anurag Verma              
Modified On : 22/09/2005              
Purpose :  Removing fields of driver title,driver home phone, driver marital status, Date of license              
              
Modified By : Anurag Verma              
Modified On : 23/09/2005              
Purpose :  Applying celing function on percen_driven field              
            
Modified By : Vijay Arora            
Modified On : 17-10-2005            
Purpose :  Added a field named APP_VEHICLE_PRIN_OCC_ID in select clause.            
            
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
 -- drop proc dbo.PROC_GETWATERCRAFTDRIVERINFO              
CREATE PROC dbo.PROC_GETWATERCRAFTDRIVERINFO              
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
MARITAL_STATUS,              
CONVERT(VARCHAR(10),DRIVER_DOB,101) AS DRIVER_DOB,              
VIOLATIONS,              
MVR_ORDERED,              
CONVERT(VARCHAR(10),DATE_ORDERED,101) AS DATE_ORDERED,              
DRIVER_SSN,              
DRIVER_SEX,              
DRIVER_DRIV_LIC,              
DRIVER_LIC_STATE,              
DRIVER_COST_GAURAD_AUX,              
VEHICLE_ID,              
IS_ACTIVE,          
CEILING(PERCENT_DRIVEN)PERCENT_DRIVEN,            
ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) AS APP_VEHICLE_PRIN_OCC_ID,        
WAT_SAFETY_COURSE,        
CERT_COAST_GUARD,      
ISNULL(APP_REC_VEHICLE_PRIN_OCC_ID,0) AS APP_REC_VEHICLE_PRIN_OCC_ID,      
REC_VEH_ID,    
MVR_CLASS,    
MVR_LIC_CLASS,    
MVR_LIC_RESTR,    
MVR_DRIV_LIC_APPL,       
MVR_REMARKS,  
MVR_STATUS,               
DRIVER_DRIV_TYPE        
FROM APP_WATERCRAFT_DRIVER_DETAILS               
WHERE APP_ID=@APP_ID AND               
APP_VERSION_ID=@APP_VERSION_ID              
AND CUSTOMER_ID=@CUSTOMER_ID AND              
DRIVER_ID=@DRIVER_ID              
END            
            
          
        
        
        
      
      
      
      
      
    
  



GO

