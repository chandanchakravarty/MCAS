IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyUmbrellaVehicleUseClassType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyUmbrellaVehicleUseClassType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name          : Dbo.Proc_GetPolicyUmbrellaVehicleUseClassType      
Created by         : Sumit Chhabra
Date               : 03-20-2006  
Purpose            : To get the vehicle use, class and type from pol_vehicle table        
Revison History :        
Used In                :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Dbo.Proc_GetPolicyUmbrellaVehicleUseClassType        
(        
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,      
@VEHICLE_ID INT      
)        
AS        
BEGIN        
 SELECT        
ISNULL(USE_VEHICLE, 0) AS APP_USE_VEHICLE_ID,      
ISNULL(CLASS_PER, 0) AS APP_VEHICLE_PERCLASS_ID,      
ISNULL(CLASS_COM, 0) AS APP_VEHICLE_COMCLASS_ID,      
ISNULL(VEHICLE_TYPE_PER, 0) AS APP_VEHICLE_PERTYPE_ID,      
ISNULL(VEHICLE_TYPE_COM, 0) AS APP_VEHICLE_COMTYPE_ID     
       
 FROM   POL_UMBRELLA_VEHICLE_INFO 
       
 WHERE  CUSTOMER_ID = @CUSTOMER_ID      
 AND POLICY_ID=@POLICY_ID      
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID      
 AND VEHICLE_ID = @VEHICLE_ID      
END      
      
    


GO

