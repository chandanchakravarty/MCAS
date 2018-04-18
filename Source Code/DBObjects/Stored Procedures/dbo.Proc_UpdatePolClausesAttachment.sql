IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolClausesAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolClausesAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  PRAVEER PANGHAL 
-- Create date: 16-DEC-2010  
-- Description: Update User Defined Policy Clauses Attachment  
-- DROP PROC Proc_UpdatePolClausesAttachment
-- =============================================  
--SELECT * FROM  POL_CLAUSES


CREATE  PROC [dbo].[Proc_UpdatePolClausesAttachment]
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,  
@MODIFIED_BY INT  , 
@POL_CLAUSE_ID INT,
@ATTACH_FILE_NAME nvarchar(510)=null

AS
BEGIN
IF EXISTS(SELECT POL_CLAUSE_ID FROM POL_CLAUSES WITH(NOLOCK) WHERE POL_CLAUSE_ID =@POL_CLAUSE_ID
 AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID)  
BEGIN
UPDATE POL_CLAUSES SET ATTACH_FILE_NAME=@ATTACH_FILE_NAME , MODIFIED_BY = @MODIFIED_BY
 

WHERE POL_CLAUSE_ID=@POL_CLAUSE_ID  AND  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID


END
 END
GO

