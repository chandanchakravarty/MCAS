IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTotalBalances]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTotalBalances]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateTotalBalances
Created by      : Vijay Joshi
Date            : 30/June/2005
Purpose    	: Updates the total balance table from temporary to main table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROCEDURE Proc_UpdateTotalBalances
AS
BEGIN
	/*Copying all records from temp table to main table*/
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	SELECT 
		ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,
		SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,
		AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID,
		POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID, 
		IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID, 
		SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, 
		TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,
		IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME
	FROM
		TEMP_ACT_ACCOUNTS_POSTING_DETAILS
	
	/*Updating the total balance table*/

	/*Cleaning the old table*/
	TRUNCATE TABLE TEMP_ACT_ACCOUNTS_POSTING_DETAILS
END



GO

