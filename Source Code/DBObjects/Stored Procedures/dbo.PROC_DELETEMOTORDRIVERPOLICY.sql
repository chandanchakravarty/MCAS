IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEMOTORDRIVERPOLICY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEMOTORDRIVERPOLICY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                          
Proc Name        : dbo.PROC_DELETEMOTORDRIVERPOLICY                                          
Created by       : Sumit Chhabra        
Date             : 7/18/2005                                          
Purpose        : Delete Policy Driver Details                                          
Revison History :                                          
Used In  : Wolverine                                           
                                  
------------------------------------------------------------                                          
Date     Review By          Comments          
    
--drop Proc PROC_DELETEMOTORDRIVERPOLICY                                    
------   ------------       -------------------------*/                                           
CREATE  PROCEDURE PROC_DELETEMOTORDRIVERPOLICY                                          
(                                          
                                           
 @CUSTOMER_ID int,                                          
 @POLICY_ID  int,                                          
 @POLICY_VERSION_ID smallint,                                          
 @DRIVER_ID INT                                         
        
)                                          
AS                                          
BEGIN                                          
      
--deactivate/delete      
   EXEC Proc_SetMotorVehicleClassRuleOnDeleteForPolicy @CUSTOMER_ID, @POLICY_ID,  @POLICY_VERSION_ID,@DRIVER_ID                           
--activate      
     --EXEC Proc_SetMotorVehicleClassRuleForPolicy @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB         
                                                                                                                          
 
  
 --delete data from the POLICY_mvr_information table also                                
 DELETE FROM POL_MVR_INFORMATION                                          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                                           
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID    

--Added by Sibin for Itrack Issue 5300 on 13 Feb 03
DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE                                          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                                           
   POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID 

 DELETE FROM POL_DRIVER_DETAILS                                           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                                           
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID      
      
END                


GO

