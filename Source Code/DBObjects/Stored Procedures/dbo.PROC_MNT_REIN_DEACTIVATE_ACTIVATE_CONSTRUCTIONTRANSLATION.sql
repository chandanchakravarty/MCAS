IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Created by  : Harmanjeet Singh
Purpose    	: Evaluation for the Contract Type screen
Date		: April 27, 2007
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC [dbo].[PROC_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]
CREATE PROC [dbo].[PROC_MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]
(
@CODE	VARCHAR(15),
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE MNT_REIN_CONSTRUCTION_TRANSLATION 
	SET IS_ACTIVE='Y'
	WHERE  REIN_CONSTRUCTION_CODE_ID=@CODE
ELSE
	UPDATE MNT_REIN_CONSTRUCTION_TRANSLATION 
	SET IS_ACTIVE='N'
	WHERE  REIN_CONSTRUCTION_CODE_ID=@CODE

END





GO

