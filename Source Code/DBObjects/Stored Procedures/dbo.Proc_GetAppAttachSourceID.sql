IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppAttachSourceID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppAttachSourceID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetAppAttachSourceID
Created by      : Vijay Arora
Date            : 06-10-2005
Purpose         : To Get the source id for showing the link of the file.
Revison History :
Used In         :   wolvorine

------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetAppAttachSourceID
(
@ATTACH_ID			INTEGER,	
@ATTACH_SOURCE_ID	INTEGER OUTPUT
)
AS
BEGIN
	SELECT @ATTACH_SOURCE_ID = SOURCE_ATTACH_ID FROM MNT_ATTACHMENT_LIST WHERE ATTACH_ID = @ATTACH_ID
END



GO

