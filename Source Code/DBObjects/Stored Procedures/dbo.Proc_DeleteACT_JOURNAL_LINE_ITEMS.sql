IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_JOURNAL_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_JOURNAL_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_DeleteACT_JOURNAL_LINE_ITEMS
Created by      : Vijay Joshi
Date            : 14/June/2005
Purpose    	: Delete records from Journal Entry line items table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteACT_JOURNAL_LINE_ITEMS
(
	@JOURNAL_ID     	int,
	@JE_LINE_ITEM_ID	int
)
AS
BEGIN
	--Checking whether record commited or not
	--If commited then we will not delete record as deleted record can not deleted
	If (SELECT Upper(IS_COMMITED) FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID) = 'Y'
	BEGIN
		--Record commited , hence we will not update it
		Return -2
	END

	/*Deleting the child records*/
	DELETE FROM ACT_JOURNAL_LINE_ITEMS
		WHERE JOURNAL_ID = @JOURNAL_ID 
		AND JE_LINE_ITEM_ID = @JE_LINE_ITEM_ID
	Return 1
	
END




GO

