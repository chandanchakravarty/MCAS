IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWaterCraftOperator]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWaterCraftOperator]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc Proc_DeletePolicyWaterCraftOperator
--go
/*----------------------------------------------------------                      
Proc Name        : dbo.Proc_DeletePolicyWaterCraftOperator                      
Created by       : Vijay Arora  
Date             : 24-11-2005  
Purpose       : Delete Policy WaterCraft Operator Details  
Revison History  :                      
Used In    : Wolverine                       
Modified By     :Shafi
Date            :8/12/05
------------------------------------------------------------                      
Date     Review By          Comments         
--drop proc Proc_DeletePolicyWaterCraftOperator            
------   ------------       -------------------------*/                       
CREATE PROCEDURE Proc_DeletePolicyWaterCraftOperator                      
(                      
                       
 @CUSTOMER_ID int,                      
 @POLICY_ID  int,                      
 @POLICY_VERSION_ID smallint,                      
 @DRIVER_ID INT                       
)                      
AS                      
BEGIN                      
 DELETE FROM POL_WATERCRAFT_MVR_INFORMATION      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                       
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID        

 DELETE FROM POL_WATERCRAFT_DRIVER_DETAILS                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND                       
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID    

 DELETE FROM POL_OPERATOR_ASSIGNED_BOAT  
 WHERE
	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =  @POLICY_ID AND 
	POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND DRIVER_ID = @DRIVER_ID                    
      
DELETE FROM POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE  
 WHERE
	CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =  @POLICY_ID AND 
	POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND DRIVER_ID = @DRIVER_ID
END                        

--go
--exec Proc_DeletePolicyWaterCraftOperator 547,83,2,1
--rollback tran



GO

