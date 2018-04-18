IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyClauses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyClauses]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================    
-- Author:  praveer panghal  
-- Create date: 21 feb 2010 
-- Description: Update User Defined Policy Clauses     
-- DROP PROC Proc_UpdatePolicyClauses    
-- =============================================    
CREATE PROC Proc_UpdatePolicyClauses     
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,   
@POL_CLAUSE_ID INT,    
@CLAUSE_TITLE NVARCHAR(100) = NULL,  
@MODIFIED_BY INT  ,  
@SUSEP_LOB_ID INT =null 
   
AS    
BEGIN     
   
 IF EXISTS(SELECT POL_CLAUSE_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE POL_CLAUSE_ID =@POL_CLAUSE_ID AND 
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)    
 BEGIN    
  if @SUSEP_LOB_ID is not null or @SUSEP_LOB_ID !=0  
  begin  
   UPDATE POL_CLAUSES SET --CLAUSE_TITLE = @CLAUSE_TITLE ,      -- changes by praveer for itrack no 1410   
   MODIFIED_BY = @MODIFIED_BY,SUSEP_LOB_ID= @SUSEP_LOB_ID ,LAST_UPDATED_DATETIME = GETDATE() ,IS_ACTIVE='Y'  
   WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID AND CUSTOMER_ID = @CUSTOMER_ID     
    AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
   end  
   else  
     
   begin  
  UPDATE POL_CLAUSES SET-- CLAUSE_TITLE = @CLAUSE_TITLE ,     -- changes by praveer for itrack no 1410    
   MODIFIED_BY = @MODIFIED_BY,LAST_UPDATED_DATETIME = GETDATE() ,IS_ACTIVE='Y'
    
   WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID  AND CUSTOMER_ID = @CUSTOMER_ID     
    AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
   end  
 END    
END  


GO

