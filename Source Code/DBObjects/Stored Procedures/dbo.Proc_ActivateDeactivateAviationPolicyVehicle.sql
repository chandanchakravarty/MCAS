IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAviationPolicyVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAviationPolicyVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAviationPolicyVehicle
Created by      : Pravesh K Chandel
Date            : 14 jan 2010
Purpose         :Activate/ Deactivate vehicle       
Revison History :                
Used In         : Brics
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC dbo.Proc_ActivateDeactivateAviationPolicyVehicle
(                
@CUSTOMER_ID     INT,                
@POLICY_ID     INT,                
@POLICY_VERSION_ID     SMALLINT,                
@VEHICLE_ID     SMALLINT,        
@IS_ACTIVE NCHAR(2)
)                
AS                
BEGIN                
        
UPDATE POL_AVIATION_VEHICLES 
SET IS_ACTIVE=@IS_ACTIVE WHERE        
 CUSTOMER_ID=@CUSTOMER_ID AND         
 POLICY_ID=@POLICY_ID AND        
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND        
 VEHICLE_ID=@VEHICLE_ID     

END        
    
      
    
  







GO

