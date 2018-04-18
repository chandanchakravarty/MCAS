IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
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

CREATE PROC [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONSTRUCTIONTRANSLATION]
(
@REIN_CONSTRUCTION_CODE_ID	INT,
@STATUS_CHECK char(1)
)
AS
BEGIN

IF @STATUS_CHECK='Y'
UPDATE MNT_REIN_CONSTRUCTION_TRANSLATION 
SET IS_ACTIVE='Y'
WHERE  REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID
ELSE
UPDATE MNT_REIN_CONSTRUCTION_TRANSLATION 
SET IS_ACTIVE='N'
WHERE  REIN_CONSTRUCTION_CODE_ID=@REIN_CONSTRUCTION_CODE_ID

END

GO

