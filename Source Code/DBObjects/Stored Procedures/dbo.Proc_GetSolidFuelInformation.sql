IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSolidFuelInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSolidFuelInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetSolidFuelInformation  
Created by         : Priya  
Date               : 19/05/2005  
Purpose            : To get details from APP_HOME_OWNER_SOLID_FUEL  
Revison History :  
Used In                :   Wolverine  

Modified By        : Mohit
Modified On        : 15/11/2005
Purpose            : adding IS_ACTIVE.
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE  PROC Dbo.Proc_GetSolidFuelInformation  
(  
 @CUSTOMER_ID int,  
 @APP_ID int,  
 @APP_VERSION_ID smallint,  
        @FUEL_ID       smallint  
)  
  
AS  
BEGIN  
 SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID,FUEL_ID,LOCATION_ID,SUB_LOC_ID,  
               MANUFACTURER,BRAND_NAME,MODEL_NUMBER,FUEL,  
  STOVE_TYPE,HAVE_LABORATORY_LABEL,IS_UNIT,  
        UNIT_OTHER_DESC,CONSTRUCTION,LOCATION,LOC_OTHER_DESC,  
  YEAR_DEVICE_INSTALLED,  
               WAS_PROF_INSTALL_DONE,INSTALL_INSPECTED_BY,INSTALL_OTHER_DESC,  
  HEATING_USE,  
  HEATING_SOURCE,OTHER_DESC,IS_ACTIVE  , STOVE_INSTALLATION_CONFORM_SPECIFICATIONS
       
           
    
 FROM APP_HOME_OWNER_SOLID_FUEL  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND  
  FUEL_ID =@FUEL_ID AND  
  APP_VERSION_ID = @APP_VERSION_ID  
  
END  
  





GO

