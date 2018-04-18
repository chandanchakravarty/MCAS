IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLMNT_REINSURANCE_MAJORMINOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLMNT_REINSURANCE_MAJORMINOR_PARTICIPATION]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Created by      : Deepak Batra
Purpose    	: Get Data for the Major/Minor Participation screens
Date		: 16 Jan 2006
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/

CREATE PROC Dbo.Proc_GetXMLMNT_REINSURANCE_MAJORMINOR_PARTICIPATION
(
	@PARTICIPATION_ID INT,
	@PARTICIPATION_TYPE CHAR(5),
	@CONTRACT_ID INT	
)
AS
BEGIN

SELECT
	PARTICIPATION_ID,
	PARTICIPATION_TYPE,
	NET_RETENTION,
	REINSURANCE_COMPANY,
	WHOLE_PERCENT,
	SIGNED_LINE_PERCENT,
	REINSURANCE_ACC_NUMBER,
	SEP_ACC,
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME,
	MODIFIED_BY,
	LAST_UPDATED_DATETIME	

FROM  MNT_REINSURANCE_MAJORMINOR_PARTICIPATION

WHERE 	PARTICIPATION_ID = @PARTICIPATION_ID AND
	PARTICIPATION_TYPE = @PARTICIPATION_TYPE AND
	CONTRACT_ID	= @CONTRACT_ID	
	
END


GO

