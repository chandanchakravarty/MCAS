IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Created by      : Harmanjeet Singh
Purpose    	: Evaluation for the Contract Type screen
Date		: April 20, 2007
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT]
CREATE PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTACT]
(
@CODE	INT,
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE MNT_REIN_CONTACT SET IS_ACTIVE='Y'
	WHERE  CONVERT(VARCHAR(10),REIN_CONTACT_ID)=@CODE
ELSE
	UPDATE MNT_REIN_CONTACT SET IS_ACTIVE='N'
	WHERE  CONVERT(VARCHAR(10),REIN_CONTACT_ID)=@CODE

END




GO

