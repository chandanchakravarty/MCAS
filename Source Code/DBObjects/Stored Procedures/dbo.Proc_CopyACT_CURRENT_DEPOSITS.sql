IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CopyACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CopyACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Dbo.Proc_CopyACT_CURRENT_DEPOSITS
Created by      : Vijay Joshi
Date            : 23/June/2005
Purpose    	: Copies whole record and creates a new deposit
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_CopyACT_CURRENT_DEPOSITS
(
	@DEPOSIT_ID     	int,
	@NEW_DEPOSIT_ID 	int OUTPUT
)
AS
BEGIN
	Declare @DEPOSIT_NUMBER numeric
	/*Retreiving the maximim deposit id*/
	SELECT @NEW_DEPOSIT_ID = IsNull(Max(DEPOSIT_ID), 0) + 1, @DEPOSIT_NUMBER = IsNull(Max(Convert(numeric,DEPOSIT_NUMBER)), 0) + 1
	FROM ACT_CURRENT_DEPOSITS
	
	INSERT INTO ACT_CURRENT_DEPOSITS
	(
		DEPOSIT_ID, GL_ID, FISCAL_ID, ACCOUNT_ID,
		DEPOSIT_NUMBER, DEPOSIT_TRAN_DATE, TOTAL_DEPOSIT_AMOUNT,
		DEPOSIT_NOTE, IS_BNK_RECONCILED, IN_BNK_RECON,
		IS_COMMITED, DATE_COMMITED, IS_ACTIVE, CREATED_BY, CREATED_DATETIME,DEPOSIT_TYPE
	)
	SELECT
	
		@NEW_DEPOSIT_ID, GL_ID, FISCAL_ID, ACCOUNT_ID,
		@DEPOSIT_NUMBER, DEPOSIT_TRAN_DATE, TOTAL_DEPOSIT_AMOUNT,
		DEPOSIT_NOTE, IS_BNK_RECONCILED, IN_BNK_RECON,
		'N', NULL, 'Y', CREATED_BY, CREATED_DATETIME,DEPOSIT_TYPE
	FROM
		ACT_CURRENT_DEPOSITS COPY_FROM
	WHERE
		COPY_FROM.DEPOSIT_ID = @DEPOSIT_ID

	/*Inserting the child records*/

-- 	Declare @JE_LINE_ITEM_ID numeric
-- 	Declare @DIV_ID int, @DEPT_ID int, @PC_ID int, @CUSTOMER_ID int, @POLICY_ID int, @POLICY_VERSION_ID int,
-- 		@AMOUNT decimal, @TYPE nchar(5), @REGARDING int, @REF_CUSTOMER int, @ACCOUNT_ID int,
-- 			@BILL_TYPE int, @NOTE nvarchar(100), @CREATED_BY numeric, @CREATED_DATETIME datetime
-- 
-- 	Declare Line_Items Cursor FOR 
-- 		SELECT 
-- 			DIV_ID, DEPT_ID,
-- 			PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,
-- 			AMOUNT, TYPE, REGARDING, REF_CUSTOMER, ACCOUNT_ID,
-- 			BILL_TYPE, NOTE,
-- 			CREATED_BY, CREATED_DATETIME
-- 		FROM 
-- 			ACT_JOURNAL_LINE_ITEMS
-- 		WHERE
-- 			DEPOSIT_ID = @DEPOSIT_ID
-- 	
-- 	Open Line_Items
-- 	
-- 	While 1=1
-- 	BEGIN
-- 		Fetch Next From Line_ITems Into
-- 			@DIV_ID, @DEPT_ID,
-- 			@PC_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,
-- 			@AMOUNT, @TYPE, @REGARDING, @REF_CUSTOMER, @ACCOUNT_ID,
-- 			@BILL_TYPE, @NOTE,
-- 			@CREATED_BY, @CREATED_DATETIME
-- 		
-- 		IF @@FETCH_STATUS <> 0
-- 			break
-- 
-- 		/*Calling the insert sp for inserting the value*/	
-- 		Exec Dbo.Proc_InsertACT_JOURNAL_LINE_ITEMS
-- 			@JE_LINE_ITEM_ID, @NEW_DEPOSIT_ID, @DIV_ID, @DEPT_ID,
-- 		     	@PC_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,
-- 			@AMOUNT, @TYPE, @REGARDING, @REF_CUSTOMER, @ACCOUNT_ID,
-- 			@BILL_TYPE, @NOTE, @CREATED_BY, @CREATED_DATETIME
-- 	END
-- 	
-- 	Close Line_Items
-- 	Deallocate  Line_Items
END

















GO

