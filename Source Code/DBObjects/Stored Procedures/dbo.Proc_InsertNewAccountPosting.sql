IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertNewAccountPosting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertNewAccountPosting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_InsertNewAccountPosting               
Created by      : Vijay Arora                    
Date            : 09-02-2006                
Purpose         : Insert the New Entries in Account Posting Details Table.
Revison History :                        
Used In         : Wolverine                 
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                   
CREATE PROC Proc_InsertNewAccountPosting                  
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID SMALLINT,
@ACCOUNT_ID INT
AS                  
BEGIN                  
               

   
	-- INSERT THE NEW ENTRIES
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	(
		ACCOUNT_ID,
		UPDATED_FROM,
		SUBLEDGER_TYPE,
		SOURCE_ROW_ID,
		MAPPING_SUBLEDGER_ID,
		SOURCE_NUM,
		SOURCE_TRAN_DATE,
		SOURCE_EFF_DATE,
		POSTING_DATE,
		TRANSACTION_AMOUNT,
		AGENCY_COMM_PERC,
		AGENCY_COMM_AMT,
		AGENCY_ID,
		CUSTOMER_ID,
		POLICY_ID,
		POLICY_VERSION_ID,
		DIV_ID,
		DEPT_ID,
		PC_ID,
		IS_PREBILL,
		BILL_CODE,
		GROSS_AMOUNT,
		ITEM_TRAN_CODE,
		ITEM_TRAN_CODE_TYPE,
		TRAN_ID,
		LOB_ID,
		SUB_LOB_ID,
		COUNTRY_ID,
		STATE_ID,
		VENDOR_ID,
		TAX_ID,
		ADNL_INFO,
		IS_BANK_RECONCILED,
		RECUR_JOURNAL_VERSION,
		IN_BNK_RECON,
		IS_BALANCE_UPDATED,
		COMMITED_BY,
		COMMITED_BY_CODE,
		COMMITED_BY_NAME,
		COMMISSION_TYPE
	)
	SELECT 
		@ACCOUNT_ID,
		UPDATED_FROM,
		SUBLEDGER_TYPE,
		SOURCE_ROW_ID,
		MAPPING_SUBLEDGER_ID,
		SOURCE_NUM,
		SOURCE_TRAN_DATE,
		SOURCE_EFF_DATE,
		GETDATE(),
		TRANSACTION_AMOUNT,
		AGENCY_COMM_PERC,
		AGENCY_COMM_AMT,
		AGENCY_ID,
		CUSTOMER_ID,
		POLICY_ID,
		POLICY_VERSION_ID,
		DIV_ID,
		DEPT_ID,
		PC_ID,
		IS_PREBILL,
		BILL_CODE,
		GROSS_AMOUNT,
		ITEM_TRAN_CODE,
		ITEM_TRAN_CODE_TYPE,
		TRAN_ID,
		LOB_ID,
		SUB_LOB_ID,
		COUNTRY_ID,
		STATE_ID,
		VENDOR_ID,
		TAX_ID,
		ADNL_INFO,
		IS_BANK_RECONCILED,
		RECUR_JOURNAL_VERSION,
		IN_BNK_RECON,
		IS_BALANCE_UPDATED,
		COMMITED_BY,
		COMMITED_BY_CODE,
		COMMITED_BY_NAME,
		COMMISSION_TYPE
 	FROM ACT_ACCOUNTS_POSTING_DETAILS
  	WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND
	ACCOUNT_ID = @ACCOUNT_ID

END                  
                
        
      
    
  







GO

