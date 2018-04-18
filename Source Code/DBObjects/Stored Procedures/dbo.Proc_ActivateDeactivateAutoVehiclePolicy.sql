IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAutoVehiclePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAutoVehiclePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                      
Proc Name       : dbo.Proc_ActivateDeactivateAutoVehiclePolicy      
Created by      : Sumit Chhabra      
Date            : 16/12/2005                                
Purpose         :Activate/ Deactivate vehicle             
Revison History :                      
Used In         : Wolverine                      
      
                     
------------------------------------------------------------                      
Date     Review By          Comments        
DROP PROC dbo.Proc_ActivateDeactivateAutoVehiclePolicy                      
              
------   ------------       -------------------------*/                      
          
CREATE PROC [dbo].[Proc_ActivateDeactivateAutoVehiclePolicy]                      
(                      
@CUSTOMER_ID     INT,                      
@POLICY_ID     INT,                      
@POLICY_VERSION_ID     SMALLINT,                      
@VEHICLE_ID     SMALLINT,              
@IS_ACTIVE NCHAR(2) ,
@RET_VAL INT =NULL  OUTPUT             
)                      
AS                      
BEGIN                      

			UPDATE POL_VEHICLES SET IS_ACTIVE=@IS_ACTIVE WHERE              
			CUSTOMER_ID=@CUSTOMER_ID AND               
			POLICY_ID=@POLICY_ID AND              
			POLICY_VERSION_ID=@POLICY_VERSION_ID AND              
			VEHICLE_ID=@VEHICLE_ID           
			
			UPDATE POL_REMUNERATION SET IS_ACTIVE= @IS_ACTIVE WHERE CUSTOMER_ID=@CUSTOMER_ID AND               
			POLICY_ID=@POLICY_ID AND              
			POLICY_VERSION_ID=@POLICY_VERSION_ID AND              
			RISK_ID = @VEHICLE_ID      
			
			---------------------------------------------------------------          
			-- DEASSIGN THE VEHICLE FROM POLICY_DRIVER_DETAILS IF IT'S DEACTIVATED --- ADDED BY ASHWANI ON <7 FEB. 2006 >          
			IF EXISTS(SELECT CUSTOMER_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID           
			AND VEHICLE_ID=@VEHICLE_ID AND UPPER(IS_ACTIVE)='N'  )          
			BEGIN           
			UPDATE POL_DRIVER_DETAILS           
			SET VEHICLE_ID=NULL          
			WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID          
			END   
			
			SET @RET_VAL = 1 
			
			
     
--Call to proc to set the value at gen info table when there are vehicles having amount>30000    
--exec  Proc_MotorGreaterAmountRulePolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID    
END              
          
            
          
      
    
  
  
GO

