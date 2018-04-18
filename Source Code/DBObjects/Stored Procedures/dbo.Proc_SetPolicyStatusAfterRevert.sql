IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetPolicyStatusAfterRevert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetPolicyStatusAfterRevert]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
PROC NAME        : dbo.Proc_SetPolicyStatusAfterRevert          
CREATED BY        :Pravesh K Chandel      
DATE                : 22-april 2008        
PURPOSE          : SETS THE POLICY STATUS after revert back process commit         
REVISON HISTORY  :                  
USED IN          :   WOLVERINE                  
------------------------------------------------------------                  
DATE     REVIEW BY          COMMENTS                  
-----   ------------       -------------------------*/                  
-- drop PROC dbo.Proc_SetPolicyStatusAfterRevert   
CREATE PROC dbo.Proc_SetPolicyStatusAfterRevert  
(                  
 @CUSTOMER_ID       INT,      
 @POLICY_ID        INT,      
 @POLICY_BASE_VERSION_ID  SMALLINT,      
 @NEW_POLICY_VERSION_ID     SMALLINT,    
 @POLICY_STATUS  VARCHAR(50)   
)                  
AS                                
BEGIN                  

UPDATE POL_CUSTOMER_POLICY_LIST       
SET POLICY_STATUS = @POLICY_STATUS          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID > @POLICY_BASE_VERSION_ID and POLICY_VERSION_ID < @NEW_POLICY_VERSION_ID     
   
END      
    
  
  



GO

