IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommitACT_JOURNAL_MASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommitACT_JOURNAL_MASTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc dbo.Proc_CommitACT_JOURNAL_MASTER  
--go 
/*----------------------------------------------------------    
Proc Name       : Proc_CommitACT_JOURNAL_MASTER    
Created by      : Vijay Joshi    
Date            : 27/June/2005    
Purpose     : Commits Journal Entry transaction    
  
Modified by      : Uday Shanker    
Date            : 17/Jan/2008    
Purpose     : Do not commit Journal Entry if Journal Item does not contain all details   
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------    
return  -1 , if record not exists    
 -2 , Record already commited    
 -3 , Proof is not equal to zero    
 -4 , Line items does not exists    
 -5 , date is less then lock day    
 -6 , line items exist with amount = 0    
 -7 , Journal Item does not contain all details  
*/    
-- drop proc dbo.Proc_CommitACT_JOURNAL_MASTER    
CREATE PROC [dbo].[Proc_CommitACT_JOURNAL_MASTER]    
(    
		@JOURNAL_ID int,    
		@COMMITED_BY smallint,    
		@PARAM1  varchar(10),    
		@PARAM2  varchar(10),    
		@PARAM3  varchar(10)    
)    
AS    
BEGIN    
     
	/*Checking whether specified journal id is commited or whther exists and any other conditions*/    

	IF Not Exists(SELECT JOURNAL_ID FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID)    
	BEGIN    
		--Record not existed, hence exiting     
		return -1    
	END    

	--Checking whether already commited    
	IF (SELECT Upper(IS_COMMITED) FROM ACT_JOURNAL_MASTER WHERE JOURNAL_ID = @JOURNAL_ID) = 'Y'     
	BEGIN    
		--Record already commited hence exiting    
		return -2    
	END    
	   

	IF (SELECT Count(JOURNAL_ID) FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID) = 0    
	BEGIN    
		--Line items does not exists hence can not commit    
		return -4    
	END    
     
	IF EXISTS(SELECT JOURNAL_ID FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID AND AMOUNT = 0)    
	BEGIN    
		--Line items exists amount = 0    
		return -6    
	END    
  
	--Uday(17-Jan-2008) Changed by uday to check all mandatory details before committing JE
	IF (SELECT Count(JOURNAL_ID) FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID AND CUSTOMER_ID IS NULL) <> 0     
	BEGIN    
		--Line items does not contain all mandatory details hence can not commit    
		return -7    
	END    

	/*Variable declaration, to be used in this proc*/      
	Declare @JOURNAL_TYPE varchar(1)  --Stores the type of journal entry, whether manual, template, recurring..    
	Declare @UPDATED_FROM nchar(1)   --Value to be inserted in UPDATED_FROM field in ACT_ACCOUNTS_POSTING_DETAILS    
	Declare @ITEM_TYPE VARCHAR(10)          --TYPE    
	--Ravindra(09-24-2007) Changed from decimal to Decimal(18,2)    
	Declare @LINE_ITEM_TOTAL_AMOUNT decimal(18,2) --Sum to amount in Line item table of specified journal id    
	Declare @COMMITED_BY_CODE nvarchar(20), @COMMITED_BY_NAME nvarchar(60), @FISCAL_ID int, @TRANS_DATE datetime    
	Declare @LOCK_DATE datetime;   

	/*Initialisation of variables*/    
	SET @UPDATED_FROM  = 'J'   --Updated from, should be J for journal entry     
	SET @ITEM_TYPE   = 'JE'    

    
     
	--Retreiving values from JOURNAL_MASTER and JOURNAL_LINE_ITEMS    
	SELECT      
	@JOURNAL_TYPE = JM.JOURNAL_GROUP_TYPE,     
	@LINE_ITEM_TOTAL_AMOUNT = Sum(JLI.AMOUNT),     
	@FISCAL_ID = JM.FISCAL_ID,     
	@TRANS_DATE = JM.TRANS_DATE    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI  
	INNER JOIN   ACT_JOURNAL_MASTER JM  
	ON JM.JOURNAL_ID = JLI.JOURNAL_ID    
	WHERE     
	JM.JOURNAL_ID = @JOURNAL_ID    
	GROUP BY    
	JM.JOURNAL_GROUP_TYPE,  JM.FISCAL_ID, JM.TRANS_DATE 
	SELECT @COMMITED_BY_CODE = USER_LOGIN_ID, @COMMITED_BY_NAME = IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'')    
	FROM MNT_USER_LIST     
	WHERE [USER_ID] = @COMMITED_BY    
    
	--Retreiving the lock day    
	SELECT @LOCK_DATE = POSTING_LOCK_DATE    
	FROM ACT_GENERAL_LEDGER    
	WHERE FISCAL_ID = @FISCAL_ID    
	/*End variables initialisation*/

	/*Checking the locking dates*/    

	IF @TRANS_DATE <= @LOCK_DATE    
	BEGIN    
		return -5
	END    


	--Checking whether proof is equal to zero or not    
	IF @LINE_ITEM_TOTAL_AMOUNT <> 0    
	BEGIN    
		--Proof is not equal to zero , hence we will not commit this journal    
		return -3    
	END 
	/*End--Checking for any type of invalid entry or any mandatory conditions*/ 



     
	/*Inserting records into sub ledgers */    
	--SELECT * FROM ACT_AGENCY_OPEN_ITEMS    
	--Inserting into agency ledger    
	/*On 1 Feb 2008 By Uday Changed the Source_Eff_Date from current date to Transaction date as in JE*/
	INSERT INTO ACT_AGENCY_OPEN_ITEMS    
	(    
	UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,     
	POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC, AGENCY_COMM_AMT,     
	AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
	NOT_COUNTED_RECEIVABLE, PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL,     
	BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, CASH_ACCOUNTING,     
	RECUR_JOURNAL_VERSION, JE_RECON_COUNTER, AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID,     
	COUNTRY_ID, STATE_ID, TRANS_DESC    
	)      
	SELECT     
	@UPDATED_FROM, JE_LINE_ITEM_ID, JM.JOURNAL_ENTRY_NO, JM.TRANS_DATE, JM.TRANS_DATE,    
	Getdate(), JLI.AMOUNT, NULL, NULL, NULL, NULL,    
	JLI.REGARDING, JLI.CUSTOMER_ID, JLI.POLICY_ID, JLI.POLICY_VERSION_ID, NULL, NULL, NULL,    
	NULL, 'AGN', JM.DIV_ID, JM.DEPT_ID, JM.PC_ID, NULL, NULL, NULL,    
	--Ravindra(03-04-2008)   BILL_CODE , Tran Code & Tran Type to be decided based on Tran Code of JE
	CASE JLI.TRAN_CODE 
	WHEN 'DB-COMM' Then 'DB'
	ELSE 'AB' END, 
	AMOUNT, 
	--Ravindra(03-04-2008)     
	CASE JLI.TRAN_CODE    
	   WHEN 'AB-PREM' Then 'ENDP'    
	   WHEN 'AB-COMM' Then 'ENDC'    
	   WHEN 'DB-COMM' Then 'ENDC'    
	END,   
	CASE JLI.TRAN_CODE    
	   WHEN 'AB-PREM' Then 'PREM'    
	   WHEN 'AB-COMM' Then 'COM'    
	   WHEN 'DB-COMM' Then 'COM'    
	END,  
	NULL, NULL,     
	NULL, NULL, NULL, NULL, NULL, NULL,    
	NULL, NULL, 'Journal entry committed.'    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM     
	ON JM.JOURNAL_ID = JLI.JOURNAL_ID    
	WHERE     
	JM.JOURNAL_ID = @JOURNAL_ID AND TYPE = 'AGN'    


	--Inserting into customer ledger    
	/*On 1 Feb 2008 By Uday Changed the Source_Eff_Date and DUE_DATE from current date to Transaction date as in JE*/ 
	INSERT INTO ACT_CUSTOMER_OPEN_ITEMS    
	(    
	UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,     
	POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC, AGENCY_COMM_AMT,     
	AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
	NOT_COUNTED_RECEIVABLE, PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL,     
	BILL_CODE, GROSS_AMOUNT,     
	ITEM_TRAN_CODE,     
	ITEM_TRAN_CODE_TYPE, TRAN_ID, CASH_ACCOUNTING,     
	RECUR_JOURNAL_VERSION, JE_RECON_COUNTER, AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID,     
	COUNTRY_ID, STATE_ID,     
	TRANS_DESC,DUE_DATE    
	)      
	SELECT     
	@UPDATED_FROM, JE_LINE_ITEM_ID, JOURNAL_ENTRY_NO, TRANS_DATE, TRANS_DATE,    
	Getdate(), JLI.AMOUNT, NULL, NULL, NULL, NULL,    
	NULL, REGARDING, POLICY_ID, POLICY_VERSION_ID, NULL, NULL, NULL,    
	0, 'CUS', JM.DIV_ID, JM.DEPT_ID, JM.PC_ID, NULL, NULL, NULL, 
	NULL, AMOUNT,     
	--Ravindra(09-26-2007)     
	CASE JLI.TRAN_CODE  WHEN 'LATE-FEE' Then 'LF'     
	   WHEN 'DB-PREM' Then 'JE'    
	   WHEN 'BILL-FEE' Then 'INSF'    
	   WHEN 'REINS-FEE' Then 'RENSF'    
	   WHEN 'NSF' Then 'NSFF'    
	   ELSE 'JE'    
	END,     
	CASE JLI.TRAN_CODE  WHEN 'LATE-FEE' Then 'FEES'     
	   WHEN 'DB-PREM' Then 'JE'    
	   WHEN 'BILL-FEE' Then 'FEES'    
	   WHEN 'REINS-FEE' Then 'FEES'    
	   WHEN 'NSF' Then 'FEES'    
	   ELSE 'JE'    
	END,     
	NULL, NULL,     
	NULL, NULL, NULL, NULL, NULL, NULL,    
	NULL, NULL,     
	CASE ISNULL(JLI.NOTE, '')    
	WHEN '' THEN CASE ISNULL(JM.[DESCRIPTION],'')    
	   WHEN '' THEN  'Journal Entry'    
	   ELSE JM.[DESCRIPTION] END    
	ELSE JLI.NOTE    
	END ,      
	TRANS_DATE    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM     
	ON JM.JOURNAL_ID = JLI.JOURNAL_ID    
	WHERE     
	JM.JOURNAL_ID = @JOURNAL_ID AND TYPE = 'CUS'    
    ORDER BY JLI.TRAN_CODE
	--************************************************    
    
	DECLARE @REGD INT    
	DECLARE @BAL DECIMAL    
	-- Find Vendor Id corresponding to Journal Id    
	SELECT @REGD = REGARDING FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = @JOURNAL_ID  AND TYPE = 'VEN'    
	-- select Balance of latest tran of that vendor    
	SELECT TOP 1 @BAL = BALANCE FROM ACT_VENDOR_OPEN_ITEMS     
	WHERE VENDOR_ID = @REGD    
	ORDER BY POSTING_DATE DESC    
    
	--Inserting into vendor ledger     
	/*On 1 Feb 2008 By Uday Changed the Source_Eff_Date from current date to Transaction date as in JE*/ 
	INSERT INTO ACT_VENDOR_OPEN_ITEMS    
	(    
	UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,     
	VENDOR_ID, POSTING_DATE, TOTAL_DUE, TOTAL_PAID,     
	PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
	NOT_COUNTED_RECEIVABLE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON,     
	AMT_IN_RECON, OPEN_RECON_CTR,BALANCE,ITEM_TRAN_TYPE,ITEM_TRAN_CODE    

	)      
	SELECT     
	@UPDATED_FROM, JE_LINE_ITEM_ID, JOURNAL_ENTRY_NO, TRANS_DATE, TRANS_DATE,    
	REGARDING, Getdate(), JLI.AMOUNT, NULL,     
	NULL, NULL, NULL,    
	NULL, JM.DIV_ID, JM.DEPT_ID, JM.PC_ID, NULL, NULL,     
	NULL, NULL,ISNULL(@BAL,0) + JLI.AMOUNT,@ITEM_TYPE,@ITEM_TYPE    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM     
	ON JM.JOURNAL_ID = JLI.JOURNAL_ID    
	WHERE     
	JM.JOURNAL_ID = @JOURNAL_ID AND TYPE = 'VEN'    
    
	--Inserting into tax sub ledger    
	INSERT INTO ACT_TAX_OPEN_ITEMS    
	(    
	UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, TAX_ID,    
	POSTING_DATE, TOTAL_DUE, TOTAL_PAID,     
	PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
	NOT_COUNTED_RECEIVABLE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON,     
	AMT_IN_RECON, OPEN_RECON_CTR    
	)      
	SELECT     
	@UPDATED_FROM, JE_LINE_ITEM_ID, JOURNAL_ENTRY_NO, TRANS_DATE, NULL, JLI.REGARDING,    
	Getdate(), JLI.AMOUNT, NULL,     
	NULL, NULL, NULL,    
	NULL, JM.DIV_ID, JM.DEPT_ID, JM.PC_ID, NULL, NULL,     
	NULL, NULL    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM     
	ON JM.JOURNAL_ID = JLI.JOURNAL_ID    
	WHERE     
	JM.JOURNAL_ID = @JOURNAL_ID AND TYPE = 'TAX'    

	/*End Inserting records into sub ledgers */        
     
     
    
	/*Inserting the record in temporary ACT_ACCOUNTS_POSTING_DETAILS*/    
	/*On 1 Feb 2008 By Uday Changed the Source_Eff_Date from current date to Transaction date as in JE*/ 
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS    
	(    
	ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,    
	SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,    
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID,    
	POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID,     
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID,     
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID,     
	TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,    
	IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,    
	TRAN_ENTITY , TRAN_DESC    

	)    
	SELECT     
	JLI.ACCOUNT_ID, @UPDATED_FROM, NULL , JLI.JE_LINE_ITEM_ID, NULL,    
	JM.JOURNAL_ENTRY_NO, JM.TRANS_DATE , JM.TRANS_DATE , JM.TRANS_DATE, AMOUNT, NULL,    
	NULL, CASE JLI.TYPE WHEN 'AGN' THEN REGARDING ELSE NULL END, CASE JLI.TYPE WHEN 'CUS' THEN REGARDING ELSE NULL END,    
	JLI.POLICY_ID, JLI.POLICY_VERSION_ID, JM.DIV_ID, JM.DEPT_ID, JM.PC_ID,    
	NULL, NULL, JLI.AMOUNT, @ITEM_TYPE, @ITEM_TYPE, NULL, NULL,    
	NULL, NULL, NULL, CASE TYPE WHEN 'VEN' THEN REGARDING ELSE NULL END,     
	CASE TYPE WHEN 'TAX' THEN REGARDING ELSE NULL END, NULL, NULL, NULL,     
	NULL, NULL, @COMMITED_BY, @COMMITED_BY_CODE, @COMMITED_BY_NAME,    
	CASE JLI.TYPE    
	WHEN 'CUS'    
	THEN jli.POLICY_NUMBER    
	WHEN 'AGN'    
		THEN (SELECT ISNULL(ML.AGENCY_DISPLAY_NAME,'') FROM    
		MNT_AGENCY_LIST ML WHERE ML.AGENCY_ID = JLI.REGARDING AND JLI.TYPE = 'AGN')    
	WHEN 'VEN'    
		THEN (SELECT ISNULL(COMPANY_NAME,'')    
		FROM MNT_VENDOR_LIST VL WHERE VL.VENDOR_ID = JLI.REGARDING AND JLI.TYPE = 'VEN')    
	ELSE    
	ISNULL(REGARDING,'')     
	END,    
	ISNULL(JLI.NOTE,'')    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM ON    
	JLI.JOURNAL_ID = JM.JOURNAL_ID    
	WHERE    
	JLI.JOURNAL_ID = @JOURNAL_ID    
	AND JLI.TYPE <> 'CUS' 

	-- Ravindra for customer JE save StateID and LObID
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS    
	(    
	ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,    
	SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,    
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID,    
	POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID,     
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID,     
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID,     
	TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,    
	IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,    
	TRAN_ENTITY , TRAN_DESC    

	)    
	SELECT     
	JLI.ACCOUNT_ID, @UPDATED_FROM, NULL , JLI.JE_LINE_ITEM_ID, NULL,    
	JM.JOURNAL_ENTRY_NO, JM.TRANS_DATE , JM.TRANS_DATE , JM.TRANS_DATE, AMOUNT, NULL,    
	NULL, CASE JLI.TYPE WHEN 'AGN' THEN REGARDING ELSE NULL END, CASE JLI.TYPE WHEN 'CUS' THEN REGARDING ELSE NULL END,    
	JLI.POLICY_ID, JLI.POLICY_VERSION_ID, JM.DIV_ID, JM.DEPT_ID, JM.PC_ID,    
	NULL, CPL.BILL_TYPE, JLI.AMOUNT, @ITEM_TYPE, @ITEM_TYPE, NULL, CPL.POLICY_LOB,    
	NULL, NULL, CPL.STATE_ID, CASE TYPE WHEN 'VEN' THEN REGARDING ELSE NULL END,     
	CASE TYPE WHEN 'TAX' THEN REGARDING ELSE NULL END, NULL, NULL, NULL,     
	NULL, NULL, @COMMITED_BY, @COMMITED_BY_CODE, @COMMITED_BY_NAME,    
	CASE JLI.TYPE    
	WHEN 'CUS'    
	THEN jli.POLICY_NUMBER    
	WHEN 'AGN'    
		THEN (SELECT ISNULL(ML.AGENCY_DISPLAY_NAME,'') FROM    
		MNT_AGENCY_LIST ML WHERE ML.AGENCY_ID = JLI.REGARDING AND JLI.TYPE = 'AGN')    
	WHEN 'VEN'    
		THEN (SELECT ISNULL(COMPANY_NAME,'')    
		FROM MNT_VENDOR_LIST VL WHERE VL.VENDOR_ID = JLI.REGARDING AND JLI.TYPE = 'VEN')    
	ELSE    
	ISNULL(REGARDING,'')     
	END,    
	ISNULL(JLI.NOTE,'')    
	FROM     
	ACT_JOURNAL_LINE_ITEMS JLI    
	INNER JOIN ACT_JOURNAL_MASTER JM ON    
	JLI.JOURNAL_ID = JM.JOURNAL_ID    
	INNER JOIN POL_CUSTOMER_POLICY_LIST CPL 
	ON JLI.CUSTOMER_ID = CPL.CUSTOMER_ID 
	AND JLI.POLICY_ID  = CPL.POLICY_ID 
	AND JLI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID 
	WHERE    
	JLI.JOURNAL_ID = @JOURNAL_ID    
	AND JLI.TYPE =  'CUS' 
	/*End Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/    
     
     
    
	--Inserting records into total balances table     
	--exec Proc_UpdateTotalBalances      
	IF @@ERROR <> 0    
		return -1     

	/*Commiting the record*/    
	UPDATE ACT_JOURNAL_MASTER     
	SET IS_COMMITED = 'Y',    
	COMMITTED_BY = @COMMITED_BY,    
	DATE_COMMITED = Getdate()    
	WHERE JOURNAL_ID = @JOURNAL_ID AND JOURNAL_GROUP_TYPE <> 'RC'    

	return 1     
    
	--An entry will go in customer balance informatio table from trigger of act_customer_open_items    
END    
    
--
--
--go 
--exec Proc_CommitACT_JOURNAL_MASTER  919 , 3 ,null ,null ,null 
--
--
--select top 10 bill_code,UPDATED_FROM ,TOTAL_DUE ,TOTAL_PAID , ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, * from act_agency_open_items 
--where agency_id = 207 order by iden_row_id desc
--
--
--rollback tran 
-- 







GO

