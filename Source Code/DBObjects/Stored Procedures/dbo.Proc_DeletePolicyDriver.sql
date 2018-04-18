IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC Proc_DeletePolicyDriver
CREATE PROCEDURE dbo.Proc_DeletePolicyDriver    
(          
           
 @CUSTOMER_ID int,          
 @POLICY_ID  int,          
 @POLICY_VERSION_ID smallint,          
 @DRIVER_ID SMALLINT    
)          
AS          
BEGIN          
  
 DELETE FROM POL_MVR_INFORMATION  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND           
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID    

  DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND           
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID          
        
  
 DELETE FROM POL_DRIVER_DETAILS           
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND           
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID = @DRIVER_ID          
END          
    
          
  



GO

