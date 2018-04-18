IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchUserDefinedPolClauseInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchUserDefinedPolClauseInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  Charles Gomes  
-- Create date: 16-April-2010  
-- Description: Fetch User Defined Policy Clause Information  
-- DROP PROC Proc_FetchUserDefinedPolClauseInfo 3,27998,1,1  
-- =============================================  
CREATE PROC [dbo].[Proc_FetchUserDefinedPolClauseInfo]   
@POL_CLAUSE_ID INT ,  
@CUSTOMER_ID INT ,  
@POLICY_ID INT,  
@POLICY_VERSION_ID INT  
  
AS  
BEGIN  
 SELECT POL_CLAUSE_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ISNULL(CLAUSE_ID,0) AS CLAUSE_ID,
 ISNULL(CLAUSE_TITLE,'') AS CLAUSE_TITLE,    
 ISNULL(CLAUSE_DESCRIPTION,'') AS CLAUSE_DESCRIPTION,ISNULL(IS_ACTIVE,'N') AS IS_ACTIVE ,ISNULL(SUSEP_LOB_ID,'') AS SUSEP_LOB_ID ,ATTACH_FILE_NAME ,CLAUSE_TYPE,CLAUSE_CODE , PREVIOUS_VERSION_ID
 FROM POL_CLAUSES WITH(NOLOCK) WHERE POL_CLAUSE_ID = @POL_CLAUSE_ID   
  AND CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
END
GO

