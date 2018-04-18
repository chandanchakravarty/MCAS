IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CLM_AUTHORITY_LIMIT_DEACTIVATE_ACTIVATE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CLM_AUTHORITY_LIMIT_DEACTIVATE_ACTIVATE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Created by  : Manoj Rathore
Purpose    	: Evaluation for the Contract Type screen
Date		: 27 Nov 2007
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC [dbo].[Proc_CLM_AUTHORITY_LIMIT_DEACTIVATE_ACTIVATE]
CREATE PROC [dbo].[Proc_CLM_AUTHORITY_LIMIT_DEACTIVATE_ACTIVATE]
(
@CODE	VARCHAR(15),
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE CLM_AUTHORITY_LIMIT
	SET IS_ACTIVE='Y'
	WHERE  LIMIT_ID=@CODE
ELSE
	UPDATE CLM_AUTHORITY_LIMIT
	SET IS_ACTIVE='N'
	WHERE  LIMIT_ID=@CODE

END






GO

