IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_TIVGROUP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_TIVGROUP]
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

CREATE PROC [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_TIVGROUP]
(
@REIN_TIV_GROUP_ID	INT,
@STATUS_CHECK char(1)
)
AS
BEGIN

IF @STATUS_CHECK='Y'
UPDATE MNT_REIN_TIV_GROUP 
SET IS_ACTIVE='Y'
WHERE  REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID
ELSE
UPDATE MNT_REIN_TIV_GROUP 
SET IS_ACTIVE='N'
WHERE  REIN_TIV_GROUP_ID=@REIN_TIV_GROUP_ID

END

GO

