IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_GETXML_CONTRACTTYPE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_GETXML_CONTRACTTYPE]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Created by  : Deepak Batra
Purpose    	: Evaluation for the Contract Type screen
Date		: 16 Jan 2006
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC [dbo].[MNT_REIN_GETXML_CONTRACTTYPE]
(
@CONTRACT_TYPE_ID	INT
)
AS
BEGIN

SELECT
	CONTRACT_TYPE_ID,
	CONTRACT_TYPE_DESC,
	IS_ACTIVE

FROM  MNT_REIN_CONTRACT_TYPE

WHERE 	CONTRACT_TYPE_ID = @CONTRACT_TYPE_ID
	
END


GO

