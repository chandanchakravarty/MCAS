IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_VoidACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_VoidACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--  begin tran
--  drop proc dbo.Proc_VoidACT_CHECK_INFORMATION
--  go 
/*----------------------------------------------------------
Proc Name       : dbo.Proc_VoidACT_CHECK_INFORMATION
Created by      : Ajit Singh Chahal
Date            : 6/28/2005
Purpose    	 :To void records of check entity.
Revison History :
Used In  : Wolverine

Modified By 	: Ravindra Gupta
Date 		: 11-20-2006
Purpose 	: Added Code for reseting Open Item for which the original check was created
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc dbo.Proc_VoidACT_CHECK_INFORMATION
CREATE  PROC [dbo].[Proc_VoidACT_CHECK_INFORMATION]
(
@CHECK_ID      int,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime,		
@DIV_ID int,
@DEPT_ID int,
@PC_ID int
)
AS
BEGIN

-- Declare constants for Check Type

DECLARE	@ACC_Check	Int
DECLARE	@RPC_Check	Int
DECLARE	@ROP_Check	Int
DECLARE	@RSC_Check	Int
DECLARE	@CC_Check	Int
DECLARE	@VC_Check	Int
DECLARE	@MOC_Check	Int
DECLARE	@REC_Check	Int

SET	@ACC_Check	=	2472
SET	@RPC_Check	=	2474
SET	@ROP_Check	=	9935
SET	@RSC_Check	=	9936
SET	@CC_Check	=	9937
SET	@VC_Check	=	9938
SET	@MOC_Check	=	9940
SET	@REC_Check	=	9945



DECLARE @ITEM_TRAN_CODE Varchar(10)
DECLARE @ITEM_TRAN_CODE_TYPE Varchar(10)

SET @ITEM_TRAN_CODE_TYPE = 'CHK'
SET @ITEM_TRAN_CODE  = 'VOID'

-- Added For Entity Name & Transaction Description 
DECLARE @TRAN_ENTITY Varchar(200)
DECLARE @TRAN_DESC Varchar(500)


--STEP: 1
--Checking whether specified invoice id is commited or whther exists and any other conditions
 
IF Not Exists(SELECT CHECK_ID FROM ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID)
BEGIN
	--Record not existed, hence exiting 
	return -1
END

--STEP:2
--Checking whether already voided
IF Exists (SELECT CHECK_ID FROM ACT_CHECK_INFORMATION WHERE GL_UPDATE = 2 and CHECK_ID = @CHECK_ID)  
BEGIN
	--Record already voided hence exiting
	return -2
END

--STEP:3
declare @updatedFrom  char(1) 
set 	@updatedFrom = 'C' -- voided checks



-- Added By Ravindra To ReSet the Status of Original Open Items 

DECLARE @CHECK_TYPE Int
DECLARE @OPEN_ITEM_ROW_ID Int
DECLARE @CHECK_AMOUNT Decimal(18,2)
DECLARE @CHECK_NUMBER Varchar(40)
DECLARE @CUSTOMER_ID INT  

SELECT  @CHECK_TYPE = 	 CHECK_TYPE,
	@OPEN_ITEM_ROW_ID= OPEN_ITEM_ROW_ID,
	@CHECK_AMOUNT = CHECK_AMOUNT,
	@CHECK_NUMBER = CHECK_NUMBER,
	@CUSTOMER_ID  = CUSTOMER_ID ,
	@TRAN_DESC    = CHECK_NOTE
FROM ACT_CHECK_INFORMATION
WHERE CHECK_ID = @CHECK_ID

-- Open Customer Open Item For 
--Premium Refund Checks for Return Premium Payment
--Premium Refund Checks for Over Payment
--Premium Refund Checks for Suspense Amount

IF ((@CHECK_TYPE = @RPC_Check) OR (@CHECK_TYPE= @ROP_Check) OR (@CHECK_TYPE= @RSC_Check))
BEGIN
	--Fetch Insured Name 
	SELECT @TRAN_ENTITY = CUSTOMER_CODE + ' - ' + CUSTOMER_FIRST_NAME + CUSTOMER_MIDDLE_NAME +
		CUSTOMER_LAST_NAME FROM CLT_CUSTOMER_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID 	

--FETCH ITEM_STATUS 
DECLARE @ITEM_STATUS VARCHAR(10)
DECLARE @IDEN_ROW_ID int
DECLARE @CHK_ID INT
DECLARE @CHK_AMOUNT DECIMAL(18,2)
--SELECT  @ITEM_STATUS = ITEM_STATUS FROM ACT_CUSTOMER_OPEN_ITEMS WHERE 
-- IDEN_ROW_ID = @OPEN_ITEM_ROW_ID
-- Changes for itrack #5615 on 13/11/09.
DECLARE Cur_ItemStatus CURSOR
FOR SELECT ITEM_STATUS,ACOI.IDEN_ROW_ID, ACOI.CHECK_ID,ACOI.AMOUNT FROM ACT_CUSTOMER_OPEN_ITEMS OI
INNER JOIN ACT_CHECK_OPEN_ITEMS ACOI ON OI.IDEN_ROW_ID = ACOI.IDEN_ROW_ID AND ACOI.CHECK_ID = @CHECK_ID
OPEN Cur_ItemStatus
FETCH NEXT FROM Cur_ItemStatus INTO @ITEM_STATUS ,@IDEN_ROW_ID,@CHK_ID,@CHK_AMOUNT
WHILE (@@FETCH_STATUS = 0)
BEGIN
--End 	

	/*UPDATE ACT_CUSTOMER_OPEN_ITEMS 
	SET 	ITEM_STATUS = ITEM_ORIGINAL_STATUS,	
		ITEM_ORIGINAL_STATUS = @ITEM_STATUS,
		IS_CHECK_CREATED = 'N',
		TOTAL_PAID = ISNULL(TOTAL_PAID,0) + @CHECK_AMOUNT
	WHERE IDEN_ROW_ID = @OPEN_ITEM_ROW_ID*/

--select ACOI.AMOUNT, * from  ACT_CUSTOMER_OPEN_ITEMS  OI
--inner join ACT_CHECK_OPEN_ITEMS ACOI 
--on ACOI.IDEN_ROW_ID = OI.IDEN_ROW_ID
--WHERE ACOI.CHECK_ID = @CHECK_ID

--Added by Shikha for #5615 on 13/11/09
	UPDATE ACT_CUSTOMER_OPEN_ITEMS
	SET ITEM_STATUS = ITEM_ORIGINAL_STATUS,	
	ITEM_ORIGINAL_STATUS = @ITEM_STATUS,
	IS_CHECK_CREATED = 'N',
	TOTAL_PAID = ISNULL(ACT_CUSTOMER_OPEN_ITEMS.TOTAL_PAID,0) - @CHK_AMOUNT
	WHERE ACT_CUSTOMER_OPEN_ITEMS.IDEN_ROW_ID = @IDEN_ROW_ID AND @CHK_ID = @CHECK_ID

FETCH NEXT FROM Cur_ItemStatus INTO @ITEM_STATUS,@IDEN_ROW_ID,@CHK_ID,@CHK_AMOUNT
END
CLOSE Cur_ItemStatus
DEALLOCATE Cur_ItemStatus
--SELECT * FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 449 AND POLICY_ID = 5
--End
	
	--Create a record for void check 
	INSERT INTO ACT_CUSTOMER_OPEN_ITEMS  
	(  
	UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,   
	POSTING_DATE, TOTAL_DUE, TOTAL_PAID, AGENCY_COMM_APPLIES, AGENCY_COMM_PERC,   
	AGENCY_COMM_AMT, AGENCY_ID,   
	CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS, NOT_COUNTED_RECEIVABLE,   
	PAYOR_TYPE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON, IS_PREBILL, BILL_CODE, GROSS_AMOUNT, 
	ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, CASH_ACCOUNTING, RECUR_JOURNAL_VERSION, JE_RECON_COUNTER,   
	AMT_IN_RECON, OPEN_RECON_CTR, LOB_ID, SUB_LOB_ID, COUNTRY_ID, STATE_ID, TRANS_DESC, ITEM_STATUS,DUE_DATE  
	)   
	SELECT   
	'C', @CHECK_ID, CI.CHECK_NUMBER, GETDATE(), GETDATE(),  
	GETDATE(), -@CHECK_AMOUNT, -@CHECK_AMOUNT ,  NULL, NULL,   
	NULL, NULL,  
	CI.CUSTOMER_ID, CI.POLICY_ID, CI.POLICY_VER_TRACKING_ID, NULL, NULL, 1, 0,  
	'CUS', CI.DIV_ID, CI.DEPT_ID, CI.PC_ID, NULL, NULL, NULL, NULL, CI.CHECK_AMOUNT,  
	@ITEM_TRAN_CODE,@ITEM_TRAN_CODE_TYPE, NULL, NULL, NULL, NULL,   
	NULL, NULL, AL.APP_LOB, AL.APP_SUBLOB, AL.COUNTRY_ID, AL.STATE_ID,CASE ISNULL(CI.CHECK_NUMBER,'') WHEN '' THEN 'Check Voided.' ELSE 'Check Voided (No. ' + @CHECK_NUMBER + ' ).' END, NULL ,GETDATE()
	FROM   
	ACT_CHECK_INFORMATION CI WITH (NOLOCK)   
	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL WITH (NOLOCK)ON   
	PCL.POLICY_ID = CI.POLICY_ID AND PCL.POLICY_VERSION_ID = CI.POLICY_VER_TRACKING_ID AND PCL.CUSTOMER_ID = CI.CUSTOMER_ID  
	LEFT JOIN APP_LIST AL WITH (NOLOCK)ON   
	AL.APP_ID = PCL.APP_ID AND AL.APP_VERSION_ID = PCL.APP_VERSION_ID AND AL.CUSTOMER_ID = PCL.CUSTOMER_ID  
	WHERE   
	CI.CHECK_ID = @CHECK_ID  

	
	
	--Creating the reconciliation group  
	DECLARE @GROUP_ID INT, @CUR_DATE DATETIME  
	SELECT @CUR_DATE = GETDATE()  
	EXEC Proc_InsertACT_RECONCILIATION_GROUPS   
	@GROUP_ID OUTPUT, @CUSTOMER_ID, 'CUST', 'Y', @CUR_DATE, @MODIFIED_BY, @MODIFIED_BY, @CUR_DATE, NULL  
	
	--Creating customer reconciliation group detail (Void Check Entry)  
	INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS  
	(  
	GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID, SUB_LEDGER_TYPE,   
	RECON_AMOUNT, NOTE, DIV_ID, DEPT_ID, PC_ID  
	)  
	SELECT   
	@GROUP_ID, 'CUST', IDEN_ROW_ID, 'CUST',  
	@CHECK_AMOUNT * -1, '', ACT_CUSTOMER_OPEN_ITEMS.DIV_ID, ACT_CUSTOMER_OPEN_ITEMS.DEPT_ID, ACT_CUSTOMER_OPEN_ITEMS.PC_ID  
	FROM ACT_CUSTOMER_OPEN_ITEMS, ACT_CHECK_INFORMATION  
	WHERE ACT_CUSTOMER_OPEN_ITEMS.UPDATED_FROM = 'C'  
	AND ACT_CUSTOMER_OPEN_ITEMS.SOURCE_ROW_ID = @CHECK_ID  
	AND ACT_CUSTOMER_OPEN_ITEMS.ITEM_TRAN_CODE = @ITEM_TRAN_CODE 
	AND ACT_CHECK_INFORMATION.CHECK_ID = @CHECK_ID   
	
	--Creating customer reconciliation group detail (Deposit Entry)  
	INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS  
	(  
	GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID, SUB_LEDGER_TYPE,   
	RECON_AMOUNT, NOTE, DIV_ID, DEPT_ID, PC_ID  
	)  
	SELECT   
	@GROUP_ID, 'CUST', OI.IDEN_ROW_ID, 'CUST',  
	--@CHECK_AMOUNT, 
	ACOI.AMOUNT,'', OI.DIV_ID, OI.DEPT_ID, OI.PC_ID  
--	FROM ACT_CUSTOMER_OPEN_ITEMS, ACT_CHECK_INFORMATION  
--	WHERE ACT_CUSTOMER_OPEN_ITEMS.IDEN_ROW_ID = ACT_CHECK_INFORMATION.OPEN_ITEM_ROW_ID   
--	AND ACT_CHECK_INFORMATION.CHECK_ID = @CHECK_ID   
	FROM ACT_CUSTOMER_OPEN_ITEMS OI, ACT_CHECK_OPEN_ITEMS ACOI
	WHERE OI.IDEN_ROW_ID = ACOI.IDEN_ROW_ID AND ACOI.CHECK_ID = @CHECK_ID
	--End reconciling check  

	
END
-- Open Item IN Agency Statement Table for Agency Commission Check 
IF (@CHECK_TYPE  = @ACC_Check )
BEGIN 

	DECLARE @MONTH_NUMBER Int,@MONTH_YEAR Int, @AGENCY_ID int,@COMM_TYPE Varchar(10),
		@RECON_GROUP_ID Int,@CHECK_AMT Decimal(18,2)

	SELECT 
	@CHECK_AMT 	=  CHECK_AMOUNT,
	@AGENCY_ID 	=  PAYEE_ENTITY_ID,
	@MONTH_NUMBER 	= [MONTH],
	@MONTH_YEAR 	= [YEAR]
	FROM ACT_CHECK_INFORMATION 
	WHERE CHECK_ID   = @CHECK_ID
	AND CHECK_TYPE = @ACC_Check

	SELECT @RECON_GROUP_ID = RECON_GROUP_ID
	FROM  ACT_AGENCY_CHECK_DISTRIBUTION	
	WHERE CHECK_ID   = @CHECK_ID
	
	-- Fetching Agency Name
	SELECT @TRAN_ENTITY = ISNULL(AGENCY_DISPLAY_NAME,'') 
	FROM MNT_AGENCY_LIST WHERE AGENCY_ID = @AGENCY_ID

	--Setting Transaction Description
	SET @TRAN_DESC = 'Check Voided Against Agency Statement  ' + Convert(Varchar,ISNULL(@MONTH_NUMBER,0)) 
			+ ' / ' + Convert(Varchar,ISNULL(@MONTH_YEAR,0)) 

	
	--- Update Agency Statement Table From the Reconciled Records

	UPDATE ACT_AGENCY_STATEMENT  
	SET TOTAL_PAID = ISNULL(TOTAL_PAID,0) -  ISNULL(RECON_AMOUNT,0) ,
	    IS_CHECK_CREATED = 'N'	
	FROM ACT_AGENCY_RECON_GROUP_DETAILS ARGD  
	INNER JOIN ACT_RECONCILIATION_GROUPS RG 
		ON RG.GROUP_ID = ARGD.GROUP_ID  
		AND RG.GROUP_ID = @RECON_GROUP_ID
	WHERE ACT_AGENCY_STATEMENT.ROW_ID = ARGD.ITEM_REFERENCE_ID  
	AND ACT_AGENCY_STATEMENT.TRAN_TYPE <> 'CHK'
	
	--Inserting the Check Entry  in agency statement  
	INSERT INTO ACT_AGENCY_STATEMENT  
	(  
	MONTH_NUMBER, MONTH_YEAR, AGENCY_ID, POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, SOURCE_EFF_DATE,   
	PREMIUM_AMOUNT, COMMISSION_RATE ,  
	TOTAL_PAID, SOURCE_ROW_ID, AMOUNT_FOR_CALCULATION, COMMISSION_AMOUNT,  
	DUE_AMOUNT, PAYMENT_STATUS, IS_TEMP_ENTRY, IN_RECON, AMT_IN_RECON, AGENCY_OPEN_ITEM_ID ,
	ITEM_STATUS , TRAN_TYPE,TRAN_CODE,UPDATED_FROM
	)  
	VALUES
	(
	@MONTH_NUMBER , @MONTH_YEAR , @AGENCY_ID, NULL, NULL , NULL , GETDATE(),   
	NULL , NULL ,  
	@CHECK_AMT * -1  , @CHECK_ID , @CHECK_AMT * -1 , 0,  
	@CHECK_AMT * -1 , 0, 0, NULL, NULL, NULL  ,
	NULL , @ITEM_TRAN_CODE_TYPE , @ITEM_TRAN_CODE , 'C'
	)
	
	
	
END

-- Open Vendor Open Item For VendorCheck
IF (@CHECK_TYPE  = @VC_Check )
BEGIN 
	DECLARE @VENDOR INT
	DECLARE @BAL DECIMAL(18,2) , @TOTPAYAMT DECIMAL(18,2)
	-- Find Vendor Id corresponding to Check Id
	SELECT @VENDOR = PAYEE_ENTITY_ID ,
		@TRAN_DESC = CHECK_NOTE
	FROM ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID  AND CHECK_TYPE = '9938'

	SELECT @TRAN_ENTITY = ISNULL(VENDOR_CODE,'') + ' - ' + ISNULL(COMPANY_NAME,'') 
	FROM MNT_VENDOR_LIST WHERE VENDOR_ID = @VENDOR
	

	SELECT @TOTPAYAMT = SUM(AMOUNT_TO_APPLY)    
	FROM ACT_VENDOR_CHECK_DISTRIBUTION WHERE VENDOR_ID = @VENDOR AND CHECK_ID = @CHECK_ID  
	      
	UPDATE ACT_VENDOR_OPEN_ITEMS 
	SET ACT_VENDOR_OPEN_ITEMS.TOTAL_PAID = ISNULL(ACT_VENDOR_OPEN_ITEMS.TOTAL_PAID,0) - ISNULL(AMOUNT_TO_APPLY,0)
	FROM ACT_VENDOR_CHECK_DISTRIBUTION
	WHERE ACT_VENDOR_OPEN_ITEMS.IDEN_ROW_ID = ACT_VENDOR_CHECK_DISTRIBUTION.OPEN_ITEM_ROW_ID
	AND ACT_VENDOR_CHECK_DISTRIBUTION.CHECK_ID = @CHECK_ID

	-- select Balance of latest tran of that vendor
	SELECT TOP 1 @BAL = BALANCE FROM ACT_VENDOR_OPEN_ITEMS 
	WHERE VENDOR_ID = @VENDOR
	ORDER BY IDEN_ROW_ID DESC

	--Inserting into vendor ledger     
	INSERT INTO ACT_VENDOR_OPEN_ITEMS    
	 (    	
	  UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,     
	  POSTING_DATE, TOTAL_DUE, TOTAL_PAID,VENDOR_ID,     
	  PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
	  NOT_COUNTED_RECEIVABLE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON,     
	  AMT_IN_RECON, OPEN_RECON_CTR,BALANCE , ITEM_TRAN_TYPE , ITEM_TRAN_CODE   
	 )      
	 SELECT     
	  'C', CHECK_ID, CHECK_NUMBER, CHECK_DATE,NULL,  
	 CHECK_DATE,CHECK_AMOUNT , @TOTPAYAMT ,PAYEE_ENTITY_ID,     
	  NULL, NULL, NULL,    
	  NULL, NULL, NULL, @PC_ID, NULL, NULL,     
	  NULL, NULL,ISNULL(@BAL,0) +  CHECK_AMOUNT ,@ITEM_TRAN_CODE_TYPE , @ITEM_TRAN_CODE  
	 FROM     
	  ACT_CHECK_INFORMATION     
	 WHERE     
	  CHECK_ID = @CHECK_ID 
END 


--*******************************************************DEBITING Bank ACCOUNT - AS REVERSAL***********************************
/*Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/
Declare @BANKAccountId int, @CHECK_TRANSACTION_DATE datetime

select @CHECK_TRANSACTION_DATE=CHECK_DATE,@BANKAccountId=ACCOUNT_ID  
from ACT_CHECK_INFORMATION where CHECK_ID = @CHECK_ID

---Modifed on 06 Dec 2007
--Fetch Payee Name for MISC abd REIN Checks
IF((@CHECK_TYPE = @MOC_Check) OR (@CHECK_TYPE = @REC_Check))
BEGIN
	SELECT @TRAN_ENTITY = ISNULL(PAYEE_ENTITY_NAME,'')
	 FROM ACT_CHECK_INFORMATION WITH(NOLOCK) 
	 WHERE CHECK_ID= @CHECK_ID 	
END


-- For Vendor & Agency Commission Check TranEntity & TanDesc will differ
IF @CHECK_TYPE <> @VC_Check	OR @CHECK_TYPE <> @ACC_Check
BEGIN 

	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	(
	ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,
	SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID, 
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID, 
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, 
	TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,
	IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,
	TRAN_ENTITY,TRAN_DESC
	)
	SELECT 
	ACCOUNT_ID, 'C', SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,
	SOURCE_NUM, Getdate(), Getdate(), Getdate(), -TRANSACTION_AMOUNT, AGENCY_COMM_PERC,
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID, 
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, @ITEM_TRAN_CODE, @ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID, 
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, 
	TAX_ID, ADNL_INFO, 'N', RECUR_JOURNAL_VERSION,
	IN_BNK_RECON, IS_BALANCE_UPDATED, @MODIFIED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,
	@TRAN_ENTITY,TRAN_DESC
	FROM 
	ACT_ACCOUNTS_POSTING_DETAILS 
	WHERE 
	SOURCE_ROW_ID=@CHECK_ID and UPDATED_FROM='C'
END 
ELSE
BEGIN 
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS
	(
	ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,
	SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID, 
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID, 
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, 
	TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,
	IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,
	TRAN_ENTITY,TRAN_DESC
	)
	SELECT 
	ACCOUNT_ID, 'C', SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,
	SOURCE_NUM, Getdate(), Getdate(), Getdate(), -TRANSACTION_AMOUNT, AGENCY_COMM_PERC,
	AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID, 
	IS_PREBILL, BILL_CODE, GROSS_AMOUNT, @ITEM_TRAN_CODE, @ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID, 
	SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID, 
	TAX_ID, ADNL_INFO, 'N', RECUR_JOURNAL_VERSION,
	IN_BNK_RECON, IS_BALANCE_UPDATED, @MODIFIED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME,
	TRAN_ENTITY,TRAN_DESC
	FROM 
	ACT_ACCOUNTS_POSTING_DETAILS 
	WHERE 
	SOURCE_ROW_ID=@CHECK_ID and UPDATED_FROM='C'
END

/*End Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/


--Updating Record
UPDATE ACT_CHECK_INFORMATION
SET MODIFIED_BY 	=   @MODIFIED_BY  ,
LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,GL_UPDATE = 2
WHERE CHECK_ID = @CHECK_ID


RETURN 1
END

--   go 
--   SELECT * FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 449 AND POLICY_ID = 5
--   exec Proc_VoidACT_CHECK_INFORMATION  2906,340,'2009-11-16',null,null,null
--   SELECT * FROM ACT_CUSTOMER_OPEN_ITEMS WHERE CUSTOMER_ID = 449 AND POLICY_ID = 5
--   ROLLBACK TRAN
--  select * from ACT_ACCOUNTS_POSTING_DETAILS where source_row_id=1684

--  

-- s
-- 
-- -- select M.ACC_NUMBER ,M.ACC_DESCRIPTION ,D.TRANSACTION_AMOUNT
-- -- ,D.*  from ACT_ACCOUNTS_POSTING_DETAILS D
-- -- INNER JOIN ACT_GL_ACCOUNTS M ON D.ACCOUNT_ID = M.ACCOUNT_ID
-- -- where IDENTITY_ROW_ID > 29493
-- 
-- --select max(identity_row_id ) from act_accounts_posting_details
-- 
-- SELECT UPDATED_FROM,row_id,DUE_AMOUNT 
-- , TOTAL_PAID 
-- ,* FROM ACT_AGENCY_STATEMENT 
-- WHERE AGENCY_ID = 197 
-- AND MONTH_NUMBER = 11 AND MONTH_YEAR  =2006 
-- order by row_id 

-- Rollback tran 
-- 
-- 
-- 




























GO

