IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ExistsACT_JOURNAL_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ExistsACT_JOURNAL_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_ExistsACT_JOURNAL_LINE_ITEMS
Created by      : Vijay Joshi
Date            : 13 June, 2005
Purpose    	: Checking whether journal entries details exists for given journal id
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_ExistsACT_JOURNAL_LINE_ITEMS
(
	@JOURNAL_ID 	int,
	@EXISTS		BIT OUTPUT
)
AS
BEGIN
	IF Exists(SELECT JOURNAL_ID
		FROM
			ACT_JOURNAL_LINE_ITEMS
		WHERE 
			JOURNAL_ID = @JOURNAL_ID)
	BEGIN
		SET @EXISTS = 1
	END
	ELSE
	BEGIN
		SET @EXISTS = 0
	END

END




GO

