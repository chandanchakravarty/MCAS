IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Created by  : Harmanjeet Singh
Purpose    	: Evaluation for the Contract Type screen
Date		: May 7, 2007
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_CONTRACT]
(
@CONTRACT_ID	INT,
@STATUS_CHECK char(1)
)
AS
BEGIN

IF @STATUS_CHECK='Y'
UPDATE MNT_REINSURANCE_CONTRACT 
SET IS_ACTIVE='Y'
WHERE  CONTRACT_ID=@CONTRACT_ID
ELSE
UPDATE MNT_REINSURANCE_CONTRACT 
SET IS_ACTIVE='N'
WHERE  CONTRACT_ID=@CONTRACT_ID

END



GO

