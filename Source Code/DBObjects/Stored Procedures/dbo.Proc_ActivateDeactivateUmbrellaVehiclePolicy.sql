IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUmbrellaVehiclePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUmbrellaVehiclePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_ActivateDeactivateUmbrellaVehiclePolicy    
Created by      : Sumit Chhabra    
Date            : 03/20/2006                              
Purpose         :Activate/ Deactivate vehicle           
Revison History :                    
Used In         : Wolverine                    
    
                   
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
        
CREATE PROC DBO.Proc_ActivateDeactivateUmbrellaVehiclePolicy                    
(                    
@CUSTOMER_ID     INT,                    
@POLICY_ID     INT,                    
@POLICY_VERSION_ID     SMALLINT,                    
@VEHICLE_ID     SMALLINT,            
@IS_ACTIVE NCHAR(2)            
)                    
AS                    
BEGIN                    
            
UPDATE POL_UMBRELLA_VEHICLE_INFO SET IS_ACTIVE=@IS_ACTIVE WHERE            
 CUSTOMER_ID=@CUSTOMER_ID AND             
 POLICY_ID=@POLICY_ID AND            
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND            
 VEHICLE_ID=@VEHICLE_ID         
---------------------------------------------------------------        
-- DEASSIGN THE VEHICLE FROM POLICY_DRIVER_DETAILS IF IT'S DEACTIVATED --- ADDED BY ASHWANI ON <7 FEB. 2006 >        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID         
   AND VEHICLE_ID=@VEHICLE_ID AND UPPER(IS_ACTIVE)='N'  )        
 BEGIN         
  UPDATE POL_UMBRELLA_DRIVER_DETAILS         
  SET VEHICLE_ID=NULL        
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID        
 END        
END            
        
          
        
    
  



GO

