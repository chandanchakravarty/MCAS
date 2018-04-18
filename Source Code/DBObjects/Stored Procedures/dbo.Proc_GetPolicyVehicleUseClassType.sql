IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyVehicleUseClassType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyVehicleUseClassType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetPolicyVehicleUseClassType    
Created by         : Vijay Arora    
Date               : 03-11-2005
Purpose            : To get the vehicle use, class and type from pol_vehicle table      
Revison History :      
Used In                :   Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetPolicyVehicleUseClassType      
(      
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,    
@VEHICLE_ID INT    
)      
AS      
BEGIN      
 SELECT      
ISNULL(POL_VEHICLES.APP_USE_VEHICLE_ID, 0) AS APP_USE_VEHICLE_ID,    
ISNULL(POL_VEHICLES.APP_VEHICLE_PERCLASS_ID, 0) AS APP_VEHICLE_PERCLASS_ID,    
ISNULL(POL_VEHICLES.APP_VEHICLE_COMCLASS_ID, 0) AS APP_VEHICLE_COMCLASS_ID,    
ISNULL(POL_VEHICLES.APP_VEHICLE_PERTYPE_ID, 0) AS APP_VEHICLE_PERTYPE_ID,    
ISNULL(POL_VEHICLES.APP_VEHICLE_COMTYPE_ID, 0) AS APP_VEHICLE_COMTYPE_ID   
     
 FROM   POL_VEHICLES     
 WHERE  CUSTOMER_ID = @CUSTOMER_ID    
 AND POLICY_ID=@POLICY_ID    
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
 AND VEHICLE_ID = @VEHICLE_ID    
END    
    
  



GO

