IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCCNoteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCCNoteDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- drop PROC dbo.Proc_GetCCNoteDetails  918 

CREATE PROC dbo.Proc_GetCCNoteDetails 
(  
 @SPOOL_ID INT  
)  
AS  
  
SELECT ISNULL(NOTE,'') AS NOTE
 FROM EOD_CREDIT_CARD_SPOOL WITH(NOLOCK)  
WHERE IDEN_ROW_ID = @SPOOL_ID   
  
  
GO

