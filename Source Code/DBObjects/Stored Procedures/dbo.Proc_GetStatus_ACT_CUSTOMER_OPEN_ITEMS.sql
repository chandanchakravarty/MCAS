IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStatus_ACT_CUSTOMER_OPEN_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStatus_ACT_CUSTOMER_OPEN_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--DROP PROC Proc_GetStatus_ACT_CUSTOMER_OPEN_ITEMS
/*--------------------------------------------------------------------        
CREATED BY   : Vijay Arora        
CREATED DATE TIME : 04-01-2006        
PURPOSE    :  Get the Item Status in Table Named ACT_CUSTOMER_OPEN_ITEMS    
REVIEW HISTORY        
REVIEW BY  DATE  PURPOSE        
        
---------------------------------------------------------------------*/        
CREATE PROCEDURE dbo.Proc_GetStatus_ACT_CUSTOMER_OPEN_ITEMS    
(        
 @CUSTOMER_ID  INT,          
 @POLICY_ID    INT,           
 @POLICY_VERSION_ID  INT,           
 @ITEM_STATUS   VARCHAR(3)  OUTPUT      
)        
AS        
BEGIN        
        
IF EXISTS (SELECT TOP 1 ITEM_STATUS FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
         POLICY_ID = @POLICY_ID AND    
               POLICY_VERSION_ID = @POLICY_VERSION_ID)    
 BEGIN    
  SELECT TOP 1 @ITEM_STATUS = ITEM_STATUS FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = @CUSTOMER_ID AND         
                     POLICY_ID = @POLICY_ID AND    
                     POLICY_VERSION_ID = @POLICY_VERSION_ID    
 END    
     
   
END        
    
  



GO

