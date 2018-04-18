IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClausesAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClausesAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:  PRAVEER PANGHAL   
-- Create date: 12-01-2011    
-- Description: Update Clauses Attachment    
-- DROP Proc_UpdateClausesAttachment  
-- =============================================    
--SELECT * FROM  MNT_CLAUSES  
  
  
CREATE  PROC [dbo].[Proc_UpdateClausesAttachment]  
@CLAUSE_ID INT,  
 @ATTACH_FILE_NAME nvarchar(510)=null  
AS  
BEGIN  
IF EXISTS(SELECT CLAUSE_ID FROM MNT_CLAUSES WITH(NOLOCK) WHERE CLAUSE_ID =@CLAUSE_ID)    
BEGIN  
UPDATE MNT_CLAUSES SET ATTACH_FILE_NAME=@ATTACH_FILE_NAME WHERE CLAUSE_ID=@CLAUSE_ID  
  
  
END  
 END
GO

