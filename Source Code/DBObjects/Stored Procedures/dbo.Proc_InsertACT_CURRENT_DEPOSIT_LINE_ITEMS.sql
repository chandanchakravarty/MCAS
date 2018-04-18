IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       :Proc_InsertACT_CURRENT_DEPOSIT_LINE_ITEMS
Created by      :Vijay Joshi
Date            :6/23/2005
Purpose    		:Insert record in ACT_CURRENT_DEPOSIT_LINE_ITEMS
Revison History :
Used In 		:Wolverine
Return values
 1	Policy saved successfully without any error
 2	if saved sucessfully, and reconciled also
-2 	Invalid policy number
-1	Some other error occured
-3  Policy is not a normal
-4  Deposit has been committed hence can not be updated
------------------------------------------------------------
Date     Review By          Comments
------------------------------------------------------------*/
CREATE PROC Dbo.Proc_InsertACT_CURRENT_DEPOSIT_LINE_ITEMS
(
	@CD_LINE_ITEM_ID			int OUTPUT,
	@DEPOSIT_ID     			int,
	@LINE_ITEM_INTERNAL_NUMBER	int OUTPUT,
	@ACCOUNT_ID     			int,
	@RECEIPT_AMOUNT     		decimal(18,2),
	@PAYOR_TYPE     			nvarchar(10),
	@RECEIPT_FROM_ID     		int,
	@RECEIPT_FROM_NAME     		nvarchar(255),
	@RECEIPT_FROM_CODE     		nvarchar(14),
	@POLICY_ID     				smallint,
	@POLICY_VERSION_ID     		smallint,
	@CREATED_BY     			int,
	@CREATED_DATETIME     		datetime,
	@POLICY_NUMBER				varchar(15),
	@CLAIM_NUMBER				varchar(50),
	@POLICY_MONTH				smallint,
	@MONTH_YEAR					INT = null
)
AS
BEGIN
	DECLARE @CUSTOMER_ID INT
	--Checking if the the deposit has been committed or not
	--If commited, then updation should be aborted
	if (SELECT IsNull(IS_COMMITED,'') FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'
		return -4

	
	/*Retreiving the type of deposit as validationof input is defferent depending on deposit type*/
	Declare @DEPOSIT_TYPE varchar(15), @POLICY_ACCOUNT_STATUS varchar(20)
	SELECT @DEPOSIT_TYPE = DEPOSIT_TYPE FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID
	
	/*Checking for the validatity of policy number and other validation*/	
	if @DEPOSIT_TYPE = 'CUST'
	BEGIN
				
		--Validating the inputs of customer receipts (deposit line item)

		--Checking whether policy id is valid or not
		SELECT 
			@POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),
			@POLICY_ACCOUNT_STATUS = IsNull(POLICY_ACCOUNT_STATUS,0), @CUSTOMER_ID = AL.CUSTOMER_ID
		FROM POL_CUSTOMER_POLICY_LIST CPL
		LEFT JOIN APP_LIST AL ON AL.APP_ID = CPL.APP_ID 
			AND AL.APP_VERSION_ID = CPL.APP_VERSION_ID 
			AND AL.CUSTOMER_ID = CPL.CUSTOMER_ID
		WHERE POLICY_NUMBER = @POLICY_NUMBER AND AL.BILL_TYPE IN ( 'DB', 'DM')
		GROUP BY IsNull(POLICY_ID,0), IsNull(POLICY_ACCOUNT_STATUS,0), AL.CUSTOMER_ID

		if IsNull(@POLICY_ID,0) = 0 
		BEGIN
			--Policy number is not valid, hence exiting with return status
			return -2			
		END
		
		--Updating is confirm flag as not confirm
		UPDATE ACT_CURRENT_DEPOSITS
		SET IS_CONFIRM = 'N'
		WHERE DEPOSIT_ID = @DEPOSIT_ID		
	END
	ELSE
	BEGIN
		--Deposit will be treated as confirm by default for other type of deposit
		UPDATE ACT_CURRENT_DEPOSITS
		SET IS_CONFIRM = 'Y'
		WHERE DEPOSIT_ID = @DEPOSIT_ID		
	END

	SELECT @CD_LINE_ITEM_ID = IsNull(Max(CD_LINE_ITEM_ID),0) +1, 
		@LINE_ITEM_INTERNAL_NUMBER = IsNull(Max(LINE_ITEM_INTERNAL_NUMBER),0) + 1
	FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS

	INSERT INTO ACT_CURRENT_DEPOSIT_LINE_ITEMS
	(
		CD_LINE_ITEM_ID, DEPOSIT_ID, LINE_ITEM_INTERNAL_NUMBER,	ACCOUNT_ID,
		DEPOSIT_TYPE, RECEIPT_AMOUNT, PAYOR_TYPE, RECEIPT_FROM_ID,
		RECEIPT_FROM_NAME, RECEIPT_FROM_CODE,
		POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID,
		IS_ACTIVE, CREATED_BY, CREATED_DATETIME, POLICY_NUMBER, CLAIM_NUMBER, POLICY_MONTH, MONTH_YEAR
	)
	VALUES
	(
		@CD_LINE_ITEM_ID, @DEPOSIT_ID, @LINE_ITEM_INTERNAL_NUMBER, @ACCOUNT_ID,
		@DEPOSIT_TYPE, @RECEIPT_AMOUNT, @PAYOR_TYPE, @RECEIPT_FROM_ID, 
		@RECEIPT_FROM_NAME, @RECEIPT_FROM_CODE,
		@POLICY_ID, @POLICY_VERSION_ID, @CUSTOMER_ID,
		'Y', @CREATED_BY, @CREATED_DATETIME, @POLICY_NUMBER, @CLAIM_NUMBER, @POLICY_MONTH, @MONTH_YEAR
	)

	--Auto applying the amount to open items 
	--exec Proc_AutoApplyOpenItems @CD_LINE_ITEM_ID, NULL, NULL, NULL

	if @@Error <> 0
	BEGIN	
		--Some error occured
		RETURN -1
	END
	
	
	--Updating the total amount
	UPDATE ACT_CURRENT_DEPOSITS
	SET TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT) 
					FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS 
					WHERE DEPOSIT_ID = @DEPOSIT_ID)
	WHERE DEPOSIT_ID = @DEPOSIT_ID
	-- Updating TAPE_TOTAL
	UPDATE ACT_CURRENT_DEPOSITS
	SET TAPE_TOTAL = (SELECT SUM(RECEIPT_AMOUNT) 
					FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS 
					WHERE DEPOSIT_ID = @DEPOSIT_ID)
	WHERE DEPOSIT_ID = @DEPOSIT_ID

	if @DEPOSIT_TYPE = 'AGNC'
	BEGIN
		--print 'agnc'
		/*For agency deposit type, doing the reconcillation also*/
		DECLARE @ReturnCode int

		EXECUTE @ReturnCode = Proc_AutoApplyOpenItems @CD_LINE_ITEM_ID, NULL, NULL, NULL

		if @ReturnCode = 1
			return 2	--Reconciled sucessfully
		else
			return 1	--Unable to reconciled
	END

	/*Returning the success status*/
	return 1
	
END








GO

