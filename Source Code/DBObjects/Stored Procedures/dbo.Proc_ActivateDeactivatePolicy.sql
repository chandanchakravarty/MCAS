IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivatePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivatePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------        
Proc Name       : Proc_ActivateDeactivatePolicy    
Created by      : Charles Gomes       
Date            : 7 Apr 10    
Purpose         : To Activate/Deactivate the application        
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc dbo.Proc_ActivateDeactivatePolicy    
CREATE  PROC [dbo].[Proc_ActivateDeactivatePolicy]  
(        
 @CUSTOMER_ID Int,    
 @POLICY_ID Int,    
 @POLICY_VERSION_ID SmallInt,    
 @IS_ACTIVE   NChar(1)        
)        
AS        
BEGIN      
    
 UPDATE POL_CUSTOMER_POLICY_LIST    
 SET         
    IS_ACTIVE  = @IS_ACTIVE       
 WHERE CUSTOMER_ID =  @CUSTOMER_ID AND    
   POLICY_ID = @POLICY_ID AND    
   POLICY_VERSION_ID = @POLICY_VERSION_ID  
    
 RETURN 1    
        
END    


GO

