IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDetailsForAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDetailsForAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetPolicyDetailsForAttachment     
Created by      : Vijay Arora      
Date            : 28/10/2005          
Purpose        : Get the policy Details.    
Revison History :          
Used In         : Wolverine            
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
  
CREATE PROC Proc_GetPolicyDetailsForAttachment      
@CUSTOMER_ID INT,      
@APP_ID INT--,      
--@APP_VERSION_ID SMALLINT    
AS      
BEGIN      
 SELECT POLICY_ID,POLICY_VERSION_ID,POLICY_LOB,POLICY_SUBLOB,POLICY_NUMBER    
 FROM POL_CUSTOMER_POLICY_LIST    
 WHERE CUSTOMER_ID = @CUSTOMER_ID      
 AND APP_ID = @APP_ID    
-- AND APP_VERSION_ID = @APP_VERSION_ID    
     
END      
    
  



GO

