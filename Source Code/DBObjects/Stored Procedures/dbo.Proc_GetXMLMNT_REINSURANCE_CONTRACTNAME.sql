IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACTNAME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLMNT_REINSURANCE_CONTRACTNAME]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Created by      : Deepak Batra
Purpose    	: Evaluation for the Contract Name  screen
Date		: 16 Jan 2006
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_GetXMLMNT_REINSURANCE_CONTRACTNAME
(
@CONTRACT_NAME_ID INT
)
AS
BEGIN

SELECT
	CONTRACT_NAME_ID,
	CONTRACT_NAME,
	CONTRACT_DESCRIPTION

FROM  MNT_CONTRACT_NAME

WHERE 	CONTRACT_NAME_ID = @CONTRACT_NAME_ID	
	
END


GO

