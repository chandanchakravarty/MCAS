IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAttachment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAttachment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteAttachment
Created by      : Priya
Date            : 09 Jun,2005
Purpose         : To delete record from Attachment table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteAttachment
(

	@AttachmentId	INT
)
AS
BEGIN
	DELETE FROM MNT_ATTACHMENT_LIST
	WHERE ATTACH_ID = @AttachmentId
END



GO

