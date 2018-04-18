IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillPolicyMotorVehicleDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillPolicyMotorVehicleDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name           : Dbo.Proc_FillPolicyMotorVehicleDropDown        
Created by            : Vijay Arora      
Date                    : 14-11-2005      
Purpose                : Gets the Vehicles details related to a particular motorcycle policy      
Revison History  :        
Used In                 : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments      
--DROP PROC [dbo].[Proc_FillPolicyMotorVehicleDropDown]  
------   ------------       -------------------------*/         
CREATE PROCEDURE [dbo].[Proc_FillPolicyMotorVehicleDropDown]         
(        
 @CUSTOMER_ID int,         
 @POLICY_ID int,         
 @POLICY_VERSION_ID int        
)        
AS        
begin        
        
--SELECT VEHICLE_ID, IsNull(MAKE,'') + ' ' + isNull(MODEL,'') As MODEL_MAKE FROM POL_VEHICLES         
SELECT VEHICLE_ID, convert(varchar,VEHICLE_ID) + ' '  + isNull(VEHICLE_YEAR,'')  + ' ' + IsNull(MAKE,'') As MODEL_MAKE FROM POL_VEHICLES         
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID     
and UPPER(ISNULL(IS_ACTIVE,'N'))='Y'       
ORDER BY VEHICLE_ID        
        
End    
GO

