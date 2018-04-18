IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
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

CREATE PROC [dbo].[MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]
(
@REIN_COMPANY_ID	INT,
@STATUS_CHECK char(1)
)
AS
BEGIN

IF @STATUS_CHECK='Y'
UPDATE MNT_REIN_COMAPANY_LIST SET IS_ACTIVE='Y'
WHERE  REIN_COMAPANY_ID=@REIN_COMPANY_ID
ELSE
UPDATE MNT_REIN_COMAPANY_LIST SET IS_ACTIVE='N'
WHERE  REIN_COMAPANY_ID=@REIN_COMPANY_ID

END

GO

