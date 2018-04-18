IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAviationVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAviationVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAviationVehicle
Created by      : Pravesh K Chandel
Date            : 12 jan 2010
Purpose         :Activate/ Deactivate vehicle       
Revison History :                
Used In         : Brics
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC dbo.Proc_ActivateDeactivateAviationVehicle
(                
@CUSTOMER_ID     INT,                
@APP_ID     INT,                
@APP_VERSION_ID     SMALLINT,                
@VEHICLE_ID     SMALLINT,        
@IS_ACTIVE NCHAR(2)
)                
AS                
BEGIN                
        
UPDATE APP_AVIATION_VEHICLES 
SET IS_ACTIVE=@IS_ACTIVE WHERE        
 CUSTOMER_ID=@CUSTOMER_ID AND         
 APP_ID=@APP_ID AND        
 APP_VERSION_ID=@APP_VERSION_ID AND        
 VEHICLE_ID=@VEHICLE_ID     

END        
    
      
    
  






GO

