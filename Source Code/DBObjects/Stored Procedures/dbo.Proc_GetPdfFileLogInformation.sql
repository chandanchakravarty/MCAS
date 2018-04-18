IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPdfFileLogInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPdfFileLogInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc Proc_GetPdfFileLogInformation
(          
 @CUSTOMER_ID  int,          
 @POLICY_ID  int,          
 @POLICY_VERSION_ID int          
)          
AS          
BEGIN          
SELECT * FROM PDF_FILE_LOG   with(nolock)
WHERE    (CUSTOMER_ID = @CUSTOMER_ID)   AND (POLICY_ID=@POLICY_ID) AND (POLICY_VERSION_ID=@POLICY_VERSION_ID);          
          
END          
GO

