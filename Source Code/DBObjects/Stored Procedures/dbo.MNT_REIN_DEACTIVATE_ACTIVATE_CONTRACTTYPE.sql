IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Created by      : Deepak Batra
Purpose    	: Evaluation for the Contract Type screen
Date		: 16 Jan 2006
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop PROC [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE]
CREATE PROC [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACTTYPE]
(
@CODE	VARCHAR(15),
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE MNT_REINSURANCE_CONTRACT_TYPE SET IS_ACTIVE='Y'
	WHERE  CONVERT(VARCHAR,CONTRACTTYPEID)=@CODE
ELSE
	UPDATE MNT_REINSURANCE_CONTRACT_TYPE SET IS_ACTIVE='N'
	WHERE  CONVERT(VARCHAR,CONTRACTTYPEID)=@CODE

END




GO

