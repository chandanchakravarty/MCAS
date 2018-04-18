IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyUmbrellaVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyUmbrellaVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_DeletePolicyUmbrellaVehicle        
Created by         : Sumit Chhabra  
Date               : 03-20-2006        
Purpose            : Delete the Policy Umbrella Vehicle Information.        
Revison History :              
Used In            :   Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
 CREATE  PROCEDURE Proc_DeletePolicyUmbrellaVehicle            
(            
 @CUSTOMER_ID  INT,            
 @POLICY_ID  INT,            
 @POLICY_VERSION_ID INT,            
 @VEHICLE_ID INT        
)            
AS            
BEGIN            
        
/*DELETE FROM POL_VEHICLE_COVERAGES             
WHERE         
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            
            
DELETE FROM POL_VEHICLE_ENDORSEMENTS             
WHERE         
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            
            
DELETE FROM POL_ADD_OTHER_INT             
WHERE         
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                
  
DELETE FROM POL_VEHICLES             
WHERE         
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                
      
--set vehicle_id of driver to null wherever the vehicle is assigned      
 UPDATE POL_DRIVER_DETAILS SET VEHICLE_ID=NULL            
 WHERE      
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                
    
--Call to proc to set the value at gen info table when there are vehicles having amount>30000    
exec  Proc_MotorGreaterAmountRulePolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID  */  
            
DELETE FROM POL_UMBRELLA_VEHICLE_INFO             
WHERE         
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                

UPDATE  POL_UMBRELLA_DRIVER_DETAILS SET VEHICLE_ID=NULL WHERE
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                
END            
            
            



GO

