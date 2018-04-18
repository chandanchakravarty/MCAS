IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolClauses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolClauses]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:  Charles Gomes    
-- Create date: 19-April-2010    
-- Description: Update User Defined Policy Clauses     
-- DROP PROC Proc_UpdatePolClauses    
-- =============================================    
CREATE PROC [dbo].[Proc_UpdatePolClauses]    
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,    
@POL_CLAUSE_ID INT,    
@CLAUSE_TITLE NVARCHAR(100) = NULL,    
@CLAUSE_DESCRIPTION TEXT =NULL,   
 @ATTACH_FILE_NAME NVARCHAR(MAX)=NULL, 
 @CLAUSE_TYPE int=null,
@MODIFIED_BY INT  ,  
@SUSEP_LOB_ID INT =null ,
@CLAUSE_CODE nvarchar(100)=null,
@PREVIOUS_VERSION_ID INT =null 
   
AS    
BEGIN     
  IF EXISTS(SELECT CLAUSE_CODE FROM POL_CLAUSES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID   
    AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID and CLAUSE_CODE=@CLAUSE_CODE 
    AND POL_CLAUSE_ID<>@POL_CLAUSE_ID)  
 BEGIN  
   
 RETURN   -2
 END      
 ELSE IF EXISTS(SELECT POL_CLAUSE_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE POL_CLAUSE_ID =@POL_CLAUSE_ID
  AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID)    
 BEGIN    
  IF @SUSEP_LOB_ID is not null or @SUSEP_LOB_ID !=0  
  BEGIN  
   UPDATE POL_CLAUSES SET CLAUSE_TITLE = @CLAUSE_TITLE , CLAUSE_DESCRIPTION = @CLAUSE_DESCRIPTION,    
   MODIFIED_BY = @MODIFIED_BY,SUSEP_LOB_ID= @SUSEP_LOB_ID ,LAST_UPDATED_DATETIME = GETDATE() ,
    ATTACH_FILE_NAME=@ATTACH_FILE_NAME,CLAUSE_TYPE=@CLAUSE_TYPE,CLAUSE_CODE=@CLAUSE_CODE,
    PREVIOUS_VERSION_ID=@PREVIOUS_VERSION_ID
   WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
   AND  POLICY_VERSION_ID=@POLICY_VERSION_ID     
   RETURN 2
   END  
   ELSE  
     
   BEGIN  
  UPDATE POL_CLAUSES SET CLAUSE_TITLE = @CLAUSE_TITLE , CLAUSE_DESCRIPTION = @CLAUSE_DESCRIPTION,    
   MODIFIED_BY = @MODIFIED_BY,LAST_UPDATED_DATETIME = GETDATE() ,ATTACH_FILE_NAME=@ATTACH_FILE_NAME,
   CLAUSE_TYPE=@CLAUSE_TYPE,CLAUSE_CODE=@CLAUSE_CODE , PREVIOUS_VERSION_ID=@PREVIOUS_VERSION_ID  
    
   WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID  AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
   AND  POLICY_VERSION_ID=@POLICY_VERSION_ID    
     RETURN 2
   END  
 END    
END  
  
GO

