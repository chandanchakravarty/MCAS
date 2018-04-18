IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT]
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
--DROP PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT]
CREATE PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_SPLIT]
(
@CODE	VARCHAR(15),
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE MNT_REIN_SPLIT
	SET IS_ACTIVE='Y'
	WHERE  CONVERT(VARCHAR,REIN_SPLIT_DEDUCTION_ID)=@CODE
ELSE
	UPDATE MNT_REIN_SPLIT
	SET IS_ACTIVE='N'
	WHERE  CONVERT(VARCHAR,REIN_SPLIT_DEDUCTION_ID)=@CODE

END




GO

