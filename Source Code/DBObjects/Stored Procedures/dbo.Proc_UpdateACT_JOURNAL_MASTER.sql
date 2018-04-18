IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_JOURNAL_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_JOURNAL_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateACT_JOURNAL_MASTER
Created by      : Vijay Joshi
Date            : 6/9/2005
Purpose    	: Update values in Journal Entry Details table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateACT_JOURNAL_MASTER
(
	@JOURNAL_ID     	int,
	@JOURNAL_GROUP_TYPE	nchar(4),
	@TRANS_DATE     	datetime,
	@JOURNAL_GROUP_CODE     nvarchar(20),
	@JOURNAL_ENTRY_NO     	nvarchar(20),
	@DESCRIPTION     	nvarchar(200),
	@DIV_ID     		smallint,
	@DEPT_ID     		smallint,
	@PC_ID     		smallint,
	@GL_ID     		int,
	@FISCAL_ID     		smallint,
	@FREQUENCY     		nchar(2),
	@START_DATE     	datetime,
	@END_DATE     		datetime,
	@DAY_OF_THE_WK     	nchar(2),
	@LAST_PROCESSED_DATE    datetime,
	@IS_COMMITED     	nchar(2),
	@DATE_COMMITED     	datetime,
	@IMPORTSTATUS     	nchar(4),
	@LAST_VALID_POSTING_DATE     datetime,
	@NO_OF_RUN     		smallint,
	@MODIFIED_BY     	int,
	@LAST_UPDATED_DATETIME  datetime
)
AS
BEGIN
	--Checking whether record commited or not
	--If commited then we will not update this record	
	If (SELECT Upper(IS_COMMITED) FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID) = 'Y'
	BEGIN
		--Record commited , hence we will not update it
		return -2
	END

	/*Checking the duplicate entry no*/
	If Not Exists(SELECT JOURNAL_ENTRY_NO 
			FROM ACT_JOURNAL_MASTER 
			WHERE JOURNAL_ENTRY_NO = @JOURNAL_ENTRY_NO AND
				JOURNAL_ID <> @JOURNAL_ID)
	BEGIN
		
		SELECT @GL_ID = GL_ID FROM ACT_GENERAL_LEDGER WHERE FISCAL_ID = @FISCAL_ID
		
		/*Now checking child records of this records exists or not*/
		/*If exists the we will not updated certain fields*/
		--If not exists then will will update all fields

		If Not Exists( SELECT JE_LINE_ITEM_ID FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID)
		BEGIN
			--Updating all items if line items records not exists
			UPDATE ACT_JOURNAL_MASTER
			SET JOURNAL_GROUP_TYPE = @JOURNAL_GROUP_TYPE,
				TRANS_DATE = @TRANS_DATE, 
				JOURNAL_GROUP_CODE = @JOURNAL_GROUP_CODE,
				JOURNAL_ENTRY_NO = @JOURNAL_ENTRY_NO, 
				[DESCRIPTION] = @DESCRIPTION, 
				DIV_ID = @DIV_ID, 
				DEPT_ID = @DEPT_ID, 
				PC_ID = @PC_ID, 
				GL_ID = @GL_ID,
				FISCAL_ID = @FISCAL_ID, 
				FREQUENCY = @FREQUENCY, 
				START_DATE = @START_DATE, 
				END_DATE = @END_DATE, 
				DAY_OF_THE_WK = @DAY_OF_THE_WK,
				LAST_PROCESSED_DATE = @LAST_PROCESSED_DATE, 
				IS_COMMITED = @IS_COMMITED, 
				DATE_COMMITED = @DATE_COMMITED, 
				IMPORTSTATUS = @IMPORTSTATUS,
				LAST_VALID_POSTING_DATE = @LAST_VALID_POSTING_DATE, 
				NO_OF_RUN = @NO_OF_RUN, 
				MODIFIED_BY = @MODIFIED_BY, 
				LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
			WHERE JOURNAL_ID = @JOURNAL_ID
		END
		ELSE
		BEGIN
			UPDATE ACT_JOURNAL_MASTER
			SET JOURNAL_GROUP_TYPE = @JOURNAL_GROUP_TYPE,
				TRANS_DATE = @TRANS_DATE, 
				JOURNAL_ENTRY_NO = @JOURNAL_ENTRY_NO, 
				[DESCRIPTION] = @DESCRIPTION, 
				FREQUENCY = @FREQUENCY, 
				START_DATE = @START_DATE, 
				END_DATE = @END_DATE, 
				DAY_OF_THE_WK = @DAY_OF_THE_WK,
				LAST_PROCESSED_DATE = @LAST_PROCESSED_DATE, 
				IMPORTSTATUS = @IMPORTSTATUS,
				LAST_VALID_POSTING_DATE = @LAST_VALID_POSTING_DATE, 
				NO_OF_RUN = @NO_OF_RUN, 
				MODIFIED_BY = @MODIFIED_BY, 
				LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
			WHERE JOURNAL_ID = @JOURNAL_ID
		END
		return 1

	END
END








GO

