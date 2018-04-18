IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppAviationVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppAviationVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
Proc Name          :  dbo.Proc_GetAppAviationVehicleInformation                                
Created by         : Pravesh K Chandel
Date               : 12 Jan 2010
Purpose            : To get the Aviation vehicle  information  from app_aviation_vehicles table                                
Revison History :                               
Used In            :   Brics
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                           
-- drop proc  dbo.Proc_GetAppAviationVehicleInformation                             
CREATE PROC dbo.Proc_GetAppAviationVehicleInformation         
(                                
@CUSTOMERID  int,                                
@APPID  int,                                
@APPVERSIONID int,                                
@VEHICLEID  int                                
                                
)                                
AS                                
BEGIN                                
                                
SELECT    
ISNULL(APP_AVIATION_VEHICLES.INSURED_VEH_NUMBER, 0) AS INSURED_VEH_NUMBER, 
APP_AVIATION_VEHICLES.VEHICLE_YEAR,                                 
APP_AVIATION_VEHICLES.MAKE AS MAKE, 
ISNULL(APP_AVIATION_VEHICLES.MAKE_OTHER, '') AS MAKE_OTHER, 
APP_AVIATION_VEHICLES.MODEL AS MODEL,
ISNULL(APP_AVIATION_VEHICLES.MODEL_OTHER, '') AS MODEL_OTHER,
ISNULL(APP_AVIATION_VEHICLES.USE_VEHICLE, 0) AS USE_VEHICLE,                                            
ISNULL(APP_AVIATION_VEHICLES.IS_ACTIVE, 'Y') AS IS_ACTIVE,                
COVG_PERIMETER,
REG_NUMBER,
SERIAL_NUMBER,
CERTIFICATION,
REGISTER,
ENGINE_TYPE,
WING_TYPE,
CREW,
PAX,
REMARKS
FROM APP_AVIATION_VEHICLES 
INNER JOIN APP_LIST ON APP_AVIATION_VEHICLES.CUSTOMER_ID = APP_LIST.CUSTOMER_ID 
			AND APP_AVIATION_VEHICLES.APP_ID = APP_LIST.APP_ID 
			AND APP_AVIATION_VEHICLES.APP_VERSION_ID = APP_LIST.APP_VERSION_ID              
WHERE   (APP_AVIATION_VEHICLES.CUSTOMER_ID = @CUSTOMERID)  
	AND (APP_AVIATION_VEHICLES.APP_ID=@APPID) 
	AND (APP_AVIATION_VEHICLES.APP_VERSION_ID=@APPVERSIONID) 
	AND (APP_AVIATION_VEHICLES.VEHICLE_ID= @VEHICLEID)          
      
END    
      
     

GO

