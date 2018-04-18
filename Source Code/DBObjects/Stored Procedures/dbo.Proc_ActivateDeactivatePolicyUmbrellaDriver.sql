IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicyUmbrellaDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicyUmbrellaDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ActivateDeactivatePolicyUmbrellaDriver  
Created by      : Sumit Chhabra
Date            : 21-03-20065  
Purpose         : To Activate/Deactivate the record of Policy umbrella driver    
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC dbo.Proc_ActivateDeactivatePolicyUmbrellaDriver    
(        
 @CUSTOMER_ID int,    
 @POLICY_ID  int,    
 @POLICY_VERSION_ID int,    
 @DRIVER_ID int,    
 @IS_ACTIVE   Char(1)        
)        
AS        
BEGIN        
 UPDATE POL_UMBRELLA_DRIVER_DETAILS    
 SET         
    IS_ACTIVE  = @IS_ACTIVE       
 WHERE        
  CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND    
  DRIVER_ID = @DRIVER_ID    
END    
    


GO

