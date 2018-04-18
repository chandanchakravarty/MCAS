IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyInceptionDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyInceptionDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetPolicyInceptionDate         
Created by      : kranti singh          
Date            : 23/01/07              
Purpose        : Get the policy Details.        
Revison History :              
Used In         : Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
      
CREATE PROC [dbo].[Proc_GetPolicyInceptionDate]      
    
	@CUSTOMER_ID INT,          
	@POL_ID INT,          
	@POL_VERSION_ID SMALLINT        
AS          
BEGIN          
 SELECT --APP_INCEPTION_DATE      
 APP_EFFECTIVE_DATE
 FROM POL_CUSTOMER_POLICY_LIST        
 WHERE CUSTOMER_ID = @CUSTOMER_ID          
 AND POLICY_ID = @POL_ID        
 AND POLICY_VERSION_ID = @POL_VERSION_ID        
         
END    


  




GO

