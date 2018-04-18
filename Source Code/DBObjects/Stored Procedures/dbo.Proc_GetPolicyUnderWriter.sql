IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyUnderWriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyUnderWriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetPolicyUnderWriter      
Created by      : Vijay Arora          
Date            : 01-02-2006      
Purpose         : Get the UnderWriter ID of the Policy    
Revison History :              
Used In         : Wolverine       
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/            
CREATE PROC Proc_GetPolicyUnderWriter    
(    
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID SMALLINT    
)          
AS          
BEGIN          
   
SELECT UNDERWRITER FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID    
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
  
     
END          
        
      
      
      
  
  
      
      
        
      
      
      
      
    
  



GO

