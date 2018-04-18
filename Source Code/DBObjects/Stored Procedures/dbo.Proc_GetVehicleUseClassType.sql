IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleUseClassType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleUseClassType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetVehicleUseClassType  
Created by         : Vijay Arora  
Date               : 10-10-2005  
Purpose            : To get the vehicle use, class and type from app_vehicle table    
Revison History :    
Used In                :   Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetVehicleUseClassType    
(    
@CUSTOMER_ID INT,  
@APP_ID INT,  
@APP_VERSION_ID INT,  
@VEHICLE_ID INT  
)    
AS    
BEGIN    
 SELECT    
 --ISNULL(APP_VEHICLES.APP_USE_VEHICLE_ID, 0) AS APP_USE_VEHICLE_ID,  
 --ISNULL(APP_VEHICLES.APP_VEHICLE_PERCLASS_ID, 0) AS APP_VEHICLE_PERCLASS_ID,  
 --ISNULL(APP_VEHICLES.APP_VEHICLE_COMCLASS_ID, 0) AS APP_VEHICLE_COMCLASS_ID,  
 --ISNULL(APP_VEHICLES.APP_VEHICLE_PERTYPE_ID, 0) AS APP_VEHICLE_PERTYPE_ID,  
 --ISNULL(APP_VEHICLES.APP_VEHICLE_COMTYPE_ID, 0) AS APP_VEHICLE_COMTYPE_ID 
 
 ISNULL(APP_VEHICLES.USE_VEHICLE, 0) AS USE_VEHICLE,    
 ISNULL(APP_VEHICLES.CLASS_PER, 0) AS CLASS_PER,  
 ISNULL(APP_VEHICLES.CLASS_COM, 0) AS CLASS_COM,  
 ISNULL(APP_VEHICLES.VEHICLE_TYPE_PER, 0) AS VEHICLE_TYPE_PER,  
 ISNULL(APP_VEHICLES.VEHICLE_TYPE_COM, 0) AS VEHICLE_TYPE_COM    
   
 FROM   APP_VEHICLES   
 WHERE  CUSTOMER_ID = @CUSTOMER_ID  
 AND APP_ID=@APP_ID  
 AND APP_VERSION_ID = @APP_VERSION_ID  
 AND VEHICLE_ID = @VEHICLE_ID  
END  
  



GO

