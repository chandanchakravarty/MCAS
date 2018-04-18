IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFDocumentMessage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFDocumentMessage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetPDFDocumentMessage]           
(                  
  @DOCUMENT_CODE  varchar(20)                
)                  
AS                  
BEGIN               
   SELECT ISNULL(DOCUMENT_MESSAGE,'0') AS DOCUMENT_MESSAGE,ISNULL(SEND_MESSAGE,'') AS SEND_MESSAGE FROM MNT_PRINT_DOCUMENT_TYPE with(nolock) WHERE DOCUMENT_CODE =@DOCUMENT_CODE
END  
GO

