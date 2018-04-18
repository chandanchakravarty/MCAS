IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_JOURNAL_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_JOURNAL_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Dbo.Proc_InsertACT_JOURNAL_MASTER
Created by      : Vijay Joshi
Date            : 6/9/2005
Purpose    	: Insert values in Journal Entry Details table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_InsertACT_JOURNAL_MASTER
CREATE PROC dbo.Proc_InsertACT_JOURNAL_MASTER
(
	@JOURNAL_ID     	int OUTPUT,
	@JOURNAL_GROUP_TYPE	nchar(4),
	@TRANS_DATE     	datetime,
	@JOURNAL_GROUP_CODE     nvarchar(20),
	@JOURNAL_ENTRY_NO     	nvarchar(20) OUTPUT,
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
	@CREATED_BY     	int,
	@CREATED_DATETIME     	datetime,
	@TEMP_JE_NUM		int = null,
	@TMPVAR 			INT	OUTPUT
)
AS
BEGIN

	-- @TMPVAR used to display a msg in page for change in concurrent JE #
	IF EXISTS (SELECT JOURNAL_ENTRY_NO FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ENTRY_NO = @TEMP_JE_NUM)
		BEGIN
			SELECT  @JOURNAL_ENTRY_NO = IsNull(Max(Convert(numeric, RTrim(Ltrim(JOURNAL_ENTRY_NO)))), 0) + 1
			FROM ACT_JOURNAL_MASTER
			SET @TMPVAR = -1
		END
	ELSE
		BEGIN
			SET @JOURNAL_ENTRY_NO = @TEMP_JE_NUM
			SET @TMPVAR = 1
		END

	/*Retreiving the maximim journal id*/
	SELECT @JOURNAL_ID = IsNull(Max(JOURNAL_ID), 0) + 1	FROM ACT_JOURNAL_MASTER
	
	SELECT @GL_ID = GL_ID FROM ACT_GENERAL_LEDGER WHERE FISCAL_ID = @FISCAL_ID
	
	INSERT INTO ACT_JOURNAL_MASTER
	(
		JOURNAL_ID, JOURNAL_GROUP_TYPE, TRANS_DATE, JOURNAL_GROUP_CODE,
		JOURNAL_ENTRY_NO, [DESCRIPTION], DIV_ID, DEPT_ID, PC_ID, GL_ID,
		FISCAL_ID, FREQUENCY, START_DATE, END_DATE, DAY_OF_THE_WK,
		LAST_PROCESSED_DATE, IS_COMMITED, DATE_COMMITED, IMPORTSTATUS,
		LAST_VALID_POSTING_DATE, NO_OF_RUN, 
		IS_ACTIVE, CREATED_BY, CREATED_DATETIME
	)
	VALUES
	(
		@JOURNAL_ID, @JOURNAL_GROUP_TYPE, @TRANS_DATE, @JOURNAL_GROUP_CODE,
		@JOURNAL_ENTRY_NO, @DESCRIPTION, @DIV_ID, @DEPT_ID, @PC_ID, @GL_ID,
		@FISCAL_ID, @FREQUENCY, @START_DATE, @END_DATE, @DAY_OF_THE_WK,
		@LAST_PROCESSED_DATE, @IS_COMMITED, @DATE_COMMITED, @IMPORTSTATUS,
		@LAST_VALID_POSTING_DATE, @NO_OF_RUN,
		'Y', @CREATED_BY, @CREATED_DATETIME
	)
END














GO

