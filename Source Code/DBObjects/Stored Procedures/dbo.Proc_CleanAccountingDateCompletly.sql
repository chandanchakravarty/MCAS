IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CleanAccountingDateCompletly]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CleanAccountingDateCompletly]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- drop proc [dbo].[Proc_CleanAccountingDateCompletly]
-- Start
CREATE procedure [dbo].[Proc_CleanAccountingDateCompletly]
(@CustomerId  Int = 0 ,
 @POLICY_ID  Int = NULL	
)
as
begin
		/*

		Data NOT to be deleted from these master tables
		Any master table to be added in this block

		ACT_GL_ACCOUNT_RANGES
		ACT_TYPE_MASTER
		ACT_GENERAL_LEDGER
		ACT_GL_ACCOUNTS
		ACT_BANK_INFORMATION
		ACT_REG_COMM_SETUP
		ACT_INSTALL_PARAMS
		ACT_TRANSACTION_CODES
		ACT_TRAN_CODE_GROUP
		ACT_TRAN_CODE_GROUP_DETAILS
		ACT_BUDGET_CATEGORY
		ACT_FREQUENCY_MASTER
		ACT_COMMISION_CLASS_MASTER
		ACT_INSTALL_PLAN_DETAIL
		ACT_MINIMUM_PREM_CANCEL
		*/

if (@CustomerId = 0)
begin
-- 		Update  ACT_GENERAL_LEDGER_TOTALS
-- 				Set BROUGHT_DOWN_AMOUNT = 0 , 
-- 				CURRENT_MTD_BALANCE = 0 , 
-- 				CURRENT_YTD_BALANCE = 0 , 
-- 				YEAR_JAN_MTD = 0 , 
-- 				YEAR_JAN_YTD = 0 , 
-- 				YEAR_FEB_MTD = 0 , 
-- 				YEAR_FEB_YTD = 0 , 
-- 				YEAR_MAR_MTD = 0 , 
-- 				YEAR_MAR_YTD = 0 , 
-- 				YEAR_APR_MTD = 0 , 
-- 				YEAR_APR_YTD = 0 , 
-- 				YEAR_MAY_MTD = 0 , 
-- 				YEAR_MAY_YTD = 0 , 
-- 				YEAR_JUN_MTD = 0 , 
-- 				YEAR_JUN_YTD = 0 , 
-- 				YEAR_JUL_MTD = 0 , 
-- 				YEAR_JUL_YTD = 0 , 
-- 				YEAR_AUG_MTD = 0 , 
-- 				YEAR_AUG_YTD = 0 , 
-- 				YEAR_SEP_MTD = 0 , 
-- 				YEAR_SEP_YTD = 0 , 
-- 				YEAR_OCT_MTD = 0 , 
-- 				YEAR_OCT_YTD = 0 , 
-- 				YEAR_NOV_MTD = 0 , 
-- 				YEAR_NOV_YTD = 0 , 
-- 				YEAR_DEC_MTD = 0 , 
-- 				YEAR_DEC_YTD = 0 , 
-- 				CARRY_FWDED_AMOUNT = 0 , 
-- 				YEAR_EOY_ENTRY = 0 

		DELETE FROM ACT_GENERAL_LEDGER_TOTALS
		delete from ACT_ACCOUNTS_POSTING_DETAILS
		
		delete from TEMP_ACT_DISTRIBUTION_DETAILS
		delete from TEMP_ACT_CHECK_INFORMATION
		delete from ACT_CUSTOMER_BALANCE_INFORMATION

		DELETE FROM ACT_TMP_CUSTOMER_OPEN_ITEMS	
		DELETE FROM TMP_ACT_AGENCY_RECON_GROUP_DETAILS

		DELETE FROM ACT_AGENCY_CHECK_DISTRIBUTION
		DELETE FROM ACT_VENDOR_CHECK_DISTRIBUTION

		DELETE FROM ACT_TMP_CUSTOMER_PAYMENTS_FROM_AGENCY
		DELETE FROM ACT_CUSTOMER_PAYMENTS_FROM_AGENCY

		delete from ACT_CUSTOMER_RECON_GROUP_DETAILS
		DELETE FROM ACT_BANK_RECON_CHECK_FILE
		DELETE FROM ACT_BANK_RECON_UPLOAD_FILE
		delete from ACT_BANK_RECONCILIATION_ITEMS_DETAILS
		delete from ACT_AGENCY_RECON_GROUP_DETAILS
		delete from ACT_VENDOR_RECON_GROUP_DETAILS
		delete from ACT_TAX_RECON_GROUP_DETAILS

		delete from ACT_RECONCILIATION_GROUPS
		delete from ACT_AGENCY_STATEMENT

		delete from ACT_VENDOR_OPEN_ITEMS
		delete from ACT_TAX_OPEN_ITEMS
		delete from ACT_AGENCY_OPEN_ITEMS
		delete from ACT_CUSTOMER_OPEN_ITEMS
		
		delete from ACT_DISTRIBUTION_DETAILS
		delete from ACT_JOURNAL_LINE_ITEMS
		delete from ACT_JOURNAL_MASTER
		delete from ACT_CURRENT_DEPOSIT_LINE_ITEMS
		delete from ACT_CURRENT_DEPOSITS
		delete from ACT_VENDOR_INVOICES
		delete from ACT_CHECK_INFORMATION
		delete from ACT_PREMIUM_PROCESS_SUB_DETAILS
		delete from ACT_PREMIUM_PROCESS_DETAILS
		delete from ACT_BANK_RECONCILIATION

		delete from ACT_POLICY_INSTALL_PLAN_DATA
		delete from ACT_POLICY_INSTALLMENT_DETAILS

		delete from ACT_EOD_PROCESS
		delete from ACT_FEE_REVERSAL
end
else
begin
	
		IF(@POLICY_ID IS NULL)
		BEGIN 
			delete from ACT_ACCOUNTS_POSTING_DETAILS where CUSTOMER_ID = @CustomerId
			
			delete from TEMP_ACT_DISTRIBUTION_DETAILS
			delete from TEMP_ACT_CHECK_INFORMATION
			delete from ACT_CUSTOMER_BALANCE_INFORMATION where CUSTOMER_ID = @CustomerId
			
			delete from ACT_CUSTOMER_RECON_GROUP_DETAILS 
				where GROUP_ID 	in (select GROUP_ID from ACT_RECONCILIATION_GROUPS where RECON_ENTITY_ID = @CustomerId and RECON_ENTITY_TYPE = 'CUST')
			delete from ACT_RECONCILIATION_GROUPS where RECON_ENTITY_ID = @CustomerId and RECON_ENTITY_TYPE = 'CUST'
			delete from ACT_AGENCY_STATEMENT  where CUSTOMER_ID = @CustomerId
	
			
			delete from ACT_AGENCY_OPEN_ITEMS where CUSTOMER_ID = @CustomerId
			delete from ACT_CUSTOMER_OPEN_ITEMS where CUSTOMER_ID = @CustomerId
			
			delete from ACT_JOURNAL_LINE_ITEMS WHERE TYPE ='CUST'  AND REGARDING = @CustomerId
			delete from ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE deposit_TYPE ='CUST'  AND customer_id = @CustomerId
		
			delete from ACT_CHECK_INFORMATION where check_type in (2474, 9935, 9936)
				and payee_entity_id = @CustomerId
			delete from ACT_PREMIUM_PROCESS_SUB_DETAILS where CUSTOMER_ID = @CustomerId

			delete from ACT_PREMIUM_PROCESS_DETAILS where CUSTOMER_ID = @CustomerId
			
			delete from ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID = @CustomerId
			delete from ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID = @CustomerId
	
			delete from ACT_FEE_REVERSAL where CUSTOMER_ID = @CustomerId
		END
		ELSE
		BEGIN 
			delete from ACT_ACCOUNTS_POSTING_DETAILS where CUSTOMER_ID = @CustomerId 
				AND POLICY_ID = @POLICY_ID
			
			delete from TEMP_ACT_DISTRIBUTION_DETAILS
			delete from TEMP_ACT_CHECK_INFORMATION
			delete from ACT_CUSTOMER_BALANCE_INFORMATION where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			
			delete from ACT_CUSTOMER_RECON_GROUP_DETAILS 
				where GROUP_ID 	in (select GROUP_ID from ACT_RECONCILIATION_GROUPS where RECON_ENTITY_ID = @CustomerId and RECON_ENTITY_TYPE = 'CUST')
			
			delete from ACT_RECONCILIATION_GROUPS where RECON_ENTITY_ID = @CustomerId and RECON_ENTITY_TYPE = 'CUST'
			delete from ACT_AGENCY_STATEMENT  where CUSTOMER_ID = @CustomerId
	
			
			delete from ACT_AGENCY_OPEN_ITEMS where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			delete from ACT_CUSTOMER_OPEN_ITEMS where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			
			delete from ACT_JOURNAL_LINE_ITEMS WHERE TYPE ='CUST'  AND REGARDING = @CustomerId  AND POLICY_ID = @POLICY_ID
			delete from ACT_CURRENT_DEPOSIT_LINE_ITEMS WHERE deposit_TYPE ='CUST'  AND customer_id = @CustomerId AND POLICY_ID = @POLICY_ID
		
			delete from ACT_CHECK_INFORMATION where check_type in (2474, 9935, 9936)
				and payee_entity_id = @CustomerId
			delete from ACT_PREMIUM_PROCESS_SUB_DETAILS where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			delete from ACT_PREMIUM_PROCESS_DETAILS where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			
			delete from ACT_POLICY_INSTALL_PLAN_DATA where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
			delete from ACT_POLICY_INSTALLMENT_DETAILS where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
	
			delete from ACT_FEE_REVERSAL where CUSTOMER_ID = @CustomerId AND POLICY_ID = @POLICY_ID
		END
		
end 


end










GO

