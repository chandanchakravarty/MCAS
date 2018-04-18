IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]
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
--DROP PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]
CREATE PROC [dbo].[Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER]
(
@CODE	VARCHAR(10),
@IS_ACTIVE char(1)
)
AS
BEGIN

IF @IS_ACTIVE='Y'
	UPDATE MNT_REIN_COMAPANY_LIST SET IS_ACTIVE='Y'
	WHERE  CONVERT(VARCHAR(10),REIN_COMAPANY_ID)=@CODE
ELSE
	UPDATE MNT_REIN_COMAPANY_LIST SET IS_ACTIVE='N'
	WHERE  CONVERT(VARCHAR(10),REIN_COMAPANY_ID)=@CODE

END




GO

