IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_JOURNAL_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_JOURNAL_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Dbo.Proc_DeleteACT_JOURNAL_MASTER
Created by      : Vijay Joshi
Date            : 14/June/2005
Purpose    	: Delete records from Journal Entry master table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc Proc_DeleteACT_JOURNAL_MASTER
CREATE PROC Dbo.Proc_DeleteACT_JOURNAL_MASTER
(
	@JOURNAL_ID     	int
)
AS
BEGIN
	--Checking whether record commited or not
	--If commited then we will not delete record as deleted record can not deleted
	If (SELECT Upper(IS_COMMITED) FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID) = 'Y'
	BEGIN
		--Record commited , hence we will not update it
		return -2
	END
                                                                                /*Deleting the child records*/
	DELETE FROM ACT_JOURNAL_LINE_ITEMS
		WHERE JOURNAL_ID = @JOURNAL_ID

	DELETE FROM ACT_JOURNAL_MASTER 
		WHERE JOURNAL_ID = @JOURNAL_ID

	
	return 1
	
END














GO

