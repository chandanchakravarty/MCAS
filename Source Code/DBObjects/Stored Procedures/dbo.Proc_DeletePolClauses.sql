IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolClauses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolClauses]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:  Charles Gomes    
-- Create date: 15-April-2010    
-- Description: Delete Policy Clauses     
-- DROP PROC Proc_DeletePolClauses 2043,112,1,0,2    
-- =============================================    
CREATE PROC [dbo].[Proc_DeletePolClauses]     
@CUSTOMER_ID INT = NULL,    
@POLICY_ID INT = NULL,    
@POLICY_VERSION_ID INT = NULL,    
@CLAUSE_ID INT = NULL,    
@POL_CLAUSE_ID INT =NULL
--@RET_VALUE INT OUT    
AS    
BEGIN    
 --SET NOCOUNT ON;    
     
IF @CLAUSE_ID IS NOT NULL AND @CLAUSE_ID != 0   
  BEGIN     
   IF EXISTS(SELECT POL_CLAUSE_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND CLAUSE_ID = @CLAUSE_ID)    
		BEGIN     
		 DELETE FROM POL_CLAUSES WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
		 AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND CLAUSE_ID = @CLAUSE_ID    
	   
			--SET @RET_VALUE = 1 --Deleted Successfully    
	     END    
   ELSE    
		BEGIN    
		 --SET @RET_VALUE = 2 -- Record Does Not Exist    
		 RETURN -3;    
		END      
   END    
 ELSE    
    BEGIN    
		IF EXISTS(SELECT POL_CLAUSE_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID
				AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
				AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
				)    
			BEGIN     
			 DELETE FROM POL_CLAUSES WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID    
				AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
				AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
			 --SET @RET_VALUE = 1 --Deleted Successfully    
		       
			END    
		ELSE    
			BEGIN    
			 --SET @RET_VALUE = 2 -- Record Does Not Exist    
			 RETURN -3;    
			END     
	  END    
END

GO

