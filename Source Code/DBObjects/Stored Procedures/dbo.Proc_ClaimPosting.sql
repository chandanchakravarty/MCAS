IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ClaimPosting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ClaimPosting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : Proc_ClaimPosting      
Created by      : ANURAG GUPTA   
Date            : 04 JAN 2011
Purpose			: Performs Claim postings
Revison History :      
Used In  : EbixAdvantage - Brasil
      
Reviewed By	:	
Reviewed On	:	

------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       --------------------------------*/      

CREATE PROC [dbo].[Proc_ClaimPosting]      
(      
	@CLAIM_ID				INT,      
	@ACTIVITY_ID			INT,      
	@DATE_COMMITED			DATETIME,      
	@COMMITTED_BY			INT
)      
AS      
BEGIN      
	DECLARE @CUSTOMER_ID 		INT
	DECLARE @POLICY_ID			INT
	DECLARE @POLICY_VERSION_ID	INT	
	DECLARE @DIV_ID				INT
	DECLARE @DEPT_ID			INT
	DECLARE @PC_ID				INT
	DECLARE @BILL_CODE			CHAR(2)
	DECLARE @LOB_ID				INT
	DECLARE @SUBLOB_ID			INT
	DECLARE @COUNTRY			INT
	DECLARE @STATE				INT
	DECLARE @COMMITTED_BY_CODE	VARCHAR(10)
	DECLARE	@COMMITTED_BY_NAME	VARCHAR(60)
	DECLARE @CUSTOMER_NAME		VARCHAR(200)
	
	SELECT 
		@COMMITTED_BY_CODE = USER_LOGIN_ID, @COMMITTED_BY_NAME = (USER_FNAME + ' ' + USER_LNAME)
	FROM 
		MNT_USER_LIST WITH(NOLOCK) 
	WHERE 
		[USER_ID] = @COMMITTED_BY
	
	SELECT 
		@CUSTOMER_ID = CLAIM.CUSTOMER_ID, @POLICY_ID = CLAIM.POLICY_ID, @POLICY_VERSION_ID = CLAIM.POLICY_VERSION_ID,
		@COUNTRY = CLAIM.COUNTRY, @STATE = CLAIM.STATE,
		@LOB_ID = CLAIM.LOB_ID, @SUBLOB_ID = POLICY.POLICY_SUBLOB, 
		@DIV_ID = POLICY.DIV_ID, @DEPT_ID = POLICY.DEPT_ID, @PC_ID = POLICY.PC_ID, @BILL_CODE = POLICY.BILL_TYPE
	FROM 
		CLM_CLAIM_INFO CLAIM WITH(NOLOCK)
	INNER JOIN
		POL_CUSTOMER_POLICY_LIST POLICY WITH(NOLOCK) ON (POLICY.CUSTOMER_ID = CLAIM.CUSTOMER_ID AND 
			POLICY.POLICY_ID = CLAIM.POLICY_ID AND POLICY.POLICY_VERSION_ID = CLAIM.POLICY_VERSION_ID)
	WHERE 
		CLAIM_ID = @CLAIM_ID
		
	SELECT 
		@CUSTOMER_NAME = ISNULL(CUSTOMER_CODE,'') + ' - ' + ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CUSTOMER_MIDDLE_NAME,'') + 
			CASE WHEN ISNULL(CUSTOMER_MIDDLE_NAME,'') = '' THEN '' ELSE ' ' END + ISNULL(CUSTOMER_LAST_NAME ,'') 
	FROM 
		CLT_CUSTOMER_LIST WITH(NOLOCK)
	WHERE 
		CUSTOMER_ID = @CUSTOMER_ID 
			
	--POSTING INTO ACCOUNTS POSTING TABLE
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	(
		ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, 
		TRANSACTION_AMOUNT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, 
		DIV_ID, DEPT_ID, PC_ID, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE,
		TRAN_ID, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME, 
		COMMISSION_TYPE, TRAN_ENTITY, TRAN_DESC, PAYOR_ID
	)
	--DEBIT POSTING
	SELECT
		ACCOUNTS.DEBIT_ACCOUNT_ID, 'C', 'CLM', @CLAIM_ID, @ACTIVITY_ID, GETDATE(), GETDATE(), GETDATE(), 
		CASE ACCOUNTS.ACCOUNT_FOR 
			WHEN 'RES' THEN SUM(RESERVE.OUTSTANDING_TRAN)
			WHEN 'PAY' THEN SUM(RESERVE.PAYMENT_AMOUNT)
			WHEN 'REC' THEN SUM(RESERVE.RECOVERY_AMOUNT)
		END, NULL AS AGENCY_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,
		@DIV_ID, @DEPT_ID, @PC_ID, @BILL_CODE, NULL AS GROSS_AMOUNT, ACCOUNTS.ACCOUNT_FOR, NULL AS ITEM_TRAN_CODE_TYPE,
		NULL AS TRAN_ID, @LOB_ID, @SUBLOB_ID, @COUNTRY, @STATE, @COMMITTED_BY,@COMMITTED_BY_CODE, @COMMITTED_BY_NAME,
		NULL AS COMMISSION_TYPE, @CUSTOMER_NAME, NULL AS TRAN_DESC, NULL AS PAYOR_ID
	FROM 
		CLM_ACTIVITY_RESERVE RESERVE WITH(NOLOCK)
	INNER JOIN
		CLM_ACTIVITY ACTIVITY WITH(NOLOCK) ON (ACTIVITY.CLAIM_ID = RESERVE.CLAIM_ID AND ACTIVITY.ACTIVITY_ID = RESERVE.ACTIVITY_ID)
	INNER JOIN
		CLM_POSTING_ACCOUNT ACCOUNTS WITH(NOLOCK) ON (ACCOUNTS.TRANSACTION_CODE = ACTIVITY.ACTIVITY_REASON AND ACCOUNTS.DETAIL_TYPE_ID = ACTIVITY.ACTION_ON_PAYMENT)
	WHERE 
		RESERVE.CLAIM_ID = @CLAIM_ID AND
		RESERVE.ACTIVITY_ID = @ACTIVITY_ID
	GROUP BY
		ACTIVITY.ACTIVITY_REASON, ACTIVITY.ACTION_ON_PAYMENT, 
		ACCOUNTS.ACCOUNT_FOR, ACCOUNTS.DEBIT_ACCOUNT_ID, ACCOUNTS.CREDIT_ACCOUNT_ID
		
	UNION ALL	--CREDIT POSTING
	
	SELECT
		ACCOUNTS.CREDIT_ACCOUNT_ID, 'C', 'CLM', @CLAIM_ID, @ACTIVITY_ID, GETDATE(), GETDATE(), 
		GETDATE(), 
		CASE ACCOUNTS.ACCOUNT_FOR 
			WHEN 'RES' THEN SUM(RESERVE.OUTSTANDING_TRAN) * -1
			WHEN 'PAY' THEN SUM(RESERVE.PAYMENT_AMOUNT) * -1
			WHEN 'REC' THEN SUM(RESERVE.RECOVERY_AMOUNT) * -1
		END, NULL AS AGENCY_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID,
		@DIV_ID, @DEPT_ID, @PC_ID, @BILL_CODE, NULL AS GROSS_AMOUNT, ACCOUNTS.ACCOUNT_FOR, NULL AS ITEM_TRAN_CODE_TYPE,
		NULL AS TRAN_ID, @LOB_ID, @SUBLOB_ID, @COUNTRY, @STATE, @COMMITTED_BY,@COMMITTED_BY_CODE, @COMMITTED_BY_NAME,
		NULL AS COMMISSION_TYPE, @CUSTOMER_NAME, NULL AS TRAN_DESC, NULL AS PAYOR_ID
	FROM 
		CLM_ACTIVITY_RESERVE RESERVE WITH(NOLOCK)
	INNER JOIN
		CLM_ACTIVITY ACTIVITY WITH(NOLOCK) ON (ACTIVITY.CLAIM_ID = RESERVE.CLAIM_ID AND ACTIVITY.ACTIVITY_ID = RESERVE.ACTIVITY_ID)
	INNER JOIN
		CLM_POSTING_ACCOUNT ACCOUNTS WITH(NOLOCK) ON (ACCOUNTS.TRANSACTION_CODE = ACTIVITY.ACTIVITY_REASON AND ACCOUNTS.DETAIL_TYPE_ID = ACTIVITY.ACTION_ON_PAYMENT)
	WHERE 
		RESERVE.CLAIM_ID = @CLAIM_ID AND
		RESERVE.ACTIVITY_ID = @ACTIVITY_ID
	GROUP BY
		ACTIVITY.ACTIVITY_REASON, ACTIVITY.ACTION_ON_PAYMENT, 
		ACCOUNTS.ACCOUNT_FOR, ACCOUNTS.DEBIT_ACCOUNT_ID, ACCOUNTS.CREDIT_ACCOUNT_ID

	RETURN 1      
END      

GO

