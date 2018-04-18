IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AutoApplyOpenItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AutoApplyOpenItems]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

/*----------------------------------------------------------
Proc Name       : Proc_AutoApplyOpenItems
Created by      : Vijay Joshi
Date            : 29/June/2005
Purpose    	: Automatically adjust the open items in open items table if total due is equal to total paid
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
-----------------------------------------------------------
Return :  1 applied sucessfully
Return : -1 Unable to apply items 
*/
-- drop proc Dbo.Proc_AutoApplyOpenItems
CREATE PROCEDURE Dbo.Proc_AutoApplyOpenItems
(
	@CD_LINE_ITEM_ID int,
	@PARAM1		 int,
	@PARAM2		 int,
	@PARAM3		 int
)
AS
BEGIN
	
	--Variables used for retreiving database value from ACT_CURRENT_DEPOSIT_LINE_ITEMS table
	DECLARE @RECEIPT_AMOUNT decimal(18,2), @DEPOSIT_TYPE nvarchar(5), @RECEIPT_FROM_ID int
	DECLARE @DEPOSIT_NO int, @DEPOSIT_TRAN_DATE datetime, @DEPOSIT_NUMBER int
	DECLARE @POLICY_ID int, @POLICY_VERSION_ID int, @DEPOSIT_ID int
	DECLARE @POLICY_MONTH smallint, @AGENCY_ID int, @POLICY_YEAR SMALLINT
	
	--Retreiving the information of this deposit
	SELECT 
		@RECEIPT_AMOUNT = CDLI.RECEIPT_AMOUNT, @DEPOSIT_TYPE = CDLI.DEPOSIT_TYPE, 
		@RECEIPT_FROM_ID = CDLI.RECEIPT_FROM_ID, @DEPOSIT_TRAN_DATE = CD.DEPOSIT_TRAN_DATE,
		@DEPOSIT_NUMBER = CD.DEPOSIT_NUMBER, @DEPOSIT_ID = CD.DEPOSIT_ID, 
		@POLICY_ID = POLICY_ID, @POLICY_VERSION_ID = POLICY_VERSION_ID,
		@POLICY_MONTH = POLICY_MONTH, @AGENCY_ID = CDLI.RECEIPT_FROM_ID,
		@POLICY_YEAR = MONTH_YEAR
	FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI
	INNER JOIN ACT_CURRENT_DEPOSITS CD
		ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID
	WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID


	/*Calculating the total unpaid amount*/
	DECLARE @TOTAL_UNPAID decimal(18,2)
	Declare @GROUP_ID int

	IF @DEPOSIT_TYPE = 'AGN'		--Deposit is for agency hence applying the amount
	BEGIN
		
		SELECT @TOTAL_UNPAID = ISNULL(SUM(ISNULL(DUE_AMOUNT,0) - ISNULL(TOTAL_PAID, 0)),0)
		FROM ACT_AGENCY_STATEMENT
		WHERE AGENCY_ID = @RECEIPT_FROM_ID AND MONTH_NUMBER = @POLICY_MONTH AND MONTH_YEAR = @POLICY_YEAR
		AND COMM_TYPE = 'REG'

		--Temporary commiting the specified deposit
		INSERT INTO ACT_AGENCY_OPEN_ITEMS
		(
			UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE,
			TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC, AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID,
			POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS, NOT_COUNTED_RECEIVABLE,
			PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL, BILL_CODE, GROSS_AMOUNT,
			ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, CASH_ACCOUNTING, RECUR_JOURNAL_VERSION, JE_RECON_COUNTER,
			AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, COMMISSION_TYPE, POLICY_MONTH
		)		
		VALUES
		( 
			'D', @CD_LINE_ITEM_ID, @DEPOSIT_NUMBER, @DEPOSIT_TRAN_DATE, @DEPOSIT_TRAN_DATE, Getdate(),
			-@RECEIPT_AMOUNT, 0, NULL, NULL, NULL, @AGENCY_ID, NULL, 
			@POLICY_ID, @POLICY_VERSION_ID, NULL, NULL, NULL, NULL, 
			'AGN', NULL, NULL, NULL, 'Y', NULL, NULL, NULL, NULL,
			'DEP','DEP',  NULL, NULL, NULL, NULL, 
			NULL, NULL, NULL, NULL, NULL, NULL, NULL, @POLICY_MONTH
		)
		
		
		if @RECEIPT_AMOUNT <> @TOTAL_UNPAID
		BEGIN
			--As total unpaid is not equal to total paid amount
			--Hence we can not automatically apply open items
			return -1
		END
	
		/*first of all creating the recon group*/	
		DECLARE @RECON_GROUP_ID INT	

		EXEC PROC_INSERTACT_RECONCILIATION_GROUPS @RECON_GROUP_ID OUTPUT,
			@AGENCY_ID, 'AGN', 'N', NULL, 
			NULL, NULL, NULL, @CD_LINE_ITEM_ID

		--Creating the reconcilation items
		INSERT INTO ACT_AGENCY_RECON_GROUP_DETAILS
		(
			GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID, SUB_LEDGER_TYPE, RECON_AMOUNT,
			NOTE, DIV_ID, DEPT_ID, PC_ID
		)
		SELECT @RECON_GROUP_ID, 'AGN', AAS.ROW_ID, NULL, ISNULL(AAS.DUE_AMOUNT,0) - ISNULL(AAS.TOTAL_PAID,0),
			NULL, NULL, NULL, NULL
		FROM ACT_AGENCY_STATEMENT AAS
		WHERE AAS.AGENCY_ID = @RECEIPT_FROM_ID AND AAS.MONTH_NUMBER = @POLICY_MONTH AND MONTH_YEAR = @POLICY_YEAR
		AND COMM_TYPE = 'REG'


		/*end creating the recon group*/	
				

		--Applying the amount
		/*UPDATE ACT_AGENCY_OPEN_ITEMS 
		SET  TOTAL_PAID = TOTAL_DUE
		WHERE AGENCY_ID = @RECEIPT_FROM_ID AND POLICY_MONTH = @POLICY_MONTH 
		AND NOT (UPDATED_FROM = 'D' AND SOURCE_ROW_ID = @CD_LINE_ITEM_ID)*/
		
		--Applying the deposit amount also
		/*UPDATE ACT_AGENCY_OPEN_ITEMS 
		SET  TOTAL_PAID = TOTAL_DUE
		WHERE AGENCY_ID = @RECEIPT_FROM_ID AND POLICY_MONTH = @POLICY_MONTH 
		AND UPDATED_FROM = 'D' AND SOURCE_ROW_ID = @CD_LINE_ITEM_ID*/
	
		--Updating the total paid field of agency statement
		/*UPDATE ACT_AGENCY_STATEMENT
		SET TOTAL_PAID = ISNULL(PREMIUM_AMOUNT, 0) * (1 - ((ISNULL(COMMISSION_RATE, 0)/100)))
		WHERE AGENCY_ID = @AGENCY_ID AND MONTH_NUMBER = @POLICY_MONTH*/
	
		return 1		
	END
	return -1	--Not of agency type hence can not be reconciled
END








GO

