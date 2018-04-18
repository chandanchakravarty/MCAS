IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommitACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommitACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.ACT_VENDOR_INVOICES    
Created by      : Ajit Singh Chahal    
Date            : 6/28/2005    
Purpose     :To commit records of vendor invoices entity.    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_CommitACT_VENDOR_INVOICES    
CREATE PROC dbo.Proc_CommitACT_VENDOR_INVOICES    
(    
@INVOICE_ID     int,    
@VENDOR_ID int,    
@IS_COMMITTED     nvarchar(2),    
@DATE_COMMITTED     datetime,    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime,    
@DIV_ID int,    
@DEPT_ID int,    
@PC_ID int,
@IS_APPROVED     char(1) = null,
@APPROVED_BY     int =null,
@APPROVED_DATE_TIME datetime =null
)    
AS    
BEGIN


--STEP: 1    
/*Checking whether specified invoice id is commited or whther exists and any other conditions*/    
     
IF Not Exists(SELECT INVOICE_ID FROM ACT_VENDOR_INVOICES (NOLOCK) WHERE INVOICE_ID = @INVOICE_ID)    
BEGIN    
	--Record not existed, hence exiting     
	return -1    
END    
--STEP:2    
    
--Checking whether already commited    
IF (SELECT Upper(IS_COMMITTED) FROM ACT_VENDOR_INVOICES (NOLOCK)  WHERE INVOICE_ID = @INVOICE_ID) = 'Y'     
BEGIN    
	--Record already commited hence exiting    
	return -2    
END    
/*-- end checking*/    
    
 -- Checking  whther lock date is less then trans date    
Declare @TRANS_DATE datetime, @LOCK_DATE datetime     
SELECT @TRANS_DATE = TRANSACTION_DATE FROM ACT_VENDOR_INVOICES (NOLOCK)  WHERE INVOICE_ID = @INVOICE_ID     
SELECT @LOCK_DATE = POSTING_LOCK_DATE FROM ACT_GENERAL_LEDGER (NOLOCK)  WHERE FISCAL_ID =     
 (SELECT FISCAL_ID FROM ACT_VENDOR_INVOICES (NOLOCK)  WHERE INVOICE_ID = @INVOICE_ID)    
    
If @TRANS_DATE <= @LOCK_DATE     
BEGIN    
	--Transaction date is less then lock date, hence can not be commited    
	return -5    
END    
--STEP:3    
    
    
/* INVOICE AMOUNT AS TO BE DISTRIBUTED FULLY BEFORE COMMIT*/    
DECLARE @RemainingAmount decimal(18,2)    
select @RemainingAmount=(vinv.INVOICE_AMOUNT -     
isnull((SELECT SUM(DISTRIBUTION_AMOUNT) FROM ACT_DISTRIBUTION_DETAILS  (NOLOCK)    
WHERE      GROUP_TYPE = 'VEN'     
AND GROUP_ID = vinv.INVOICE_ID),0))     
from ACT_VENDOR_INVOICES vinv (NOLOCK) where INVOICE_ID = @INVOICE_ID    
IF @RemainingAmount <> 0    
	 return -3    
	--Invoice not distributed fully, therefore exiting    
    

--Step:3a
/* Approve the record */
--exec Proc_ApproveACT_VENDOR_INVOICES
update ACT_VENDOR_INVOICES
set
IS_APPROVED =    @IS_APPROVED,
APPROVED_DATE_TIME =    @APPROVED_DATE_TIME,
APPROVED_BY =   @APPROVED_BY ,
MODIFIED_BY =  @MODIFIED_BY  ,
LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME
where INVOICE_ID =  @INVOICE_ID 
--*************************************


DECLARE  @TRAN_ENTITY Varchar(200)

SELECT  @TRAN_ENTITY = ISNULL(COMPANY_NAME,'***  ')  FROM MNT_VENDOR_LIST (NOLOCK) 
WHERE VENDOR_ID = @VENDOR_ID
--STEP:4    
declare @updatedFrom  char(1)     
set @updatedFrom = 'I'    
    
--****************************
DECLARE @BAL DECIMAL(18,2),@TRAN_DATE DATETIME

SELECT TOP 1 @BAL = BALANCE,@TRAN_DATE = POSTING_DATE FROM ACT_VENDOR_OPEN_ITEMS (NOLOCK) 
WHERE VENDOR_ID = @VENDOR_ID
ORDER BY IDEN_ROW_ID DESC

--Inserting into vendor ledger     
INSERT INTO ACT_VENDOR_OPEN_ITEMS    
 (    
  UPDATED_FROM, SOURCE_ROW_ID, SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE,     
  POSTING_DATE, TOTAL_DUE, TOTAL_PAID,VENDOR_ID,     
  PAYMENT_DATE, DATE_FULL_PAID, PAYMENT_STATUS,     
  NOT_COUNTED_RECEIVABLE, DIV_ID, DEPT_ID, PC_ID, IS_TEMP_ENTRY, IN_RECON,     
  AMT_IN_RECON, OPEN_RECON_CTR,BALANCE    
 )      
 SELECT     
  @updatedFrom, INVOICE_ID, INVOICE_NUM, TRANSACTION_DATE, DUE_DATE,  
 Getdate(),INVOICE_AMOUNT, NULL,VENDOR_ID,     
  NULL, NULL, NULL,    
  NULL, @DIV_ID, @DEPT_ID, @PC_ID, NULL, NULL,     
  NULL, NULL,ISNULL(@BAL,0) + INVOICE_AMOUNT
 FROM     
  ACT_VENDOR_INVOICES (NOLOCK)     
 WHERE     
  INVOICE_ID = @INVOICE_ID    


--STEP:5    
--*******************************************************CREDITING VENDOR ACCOUNT***********************************    
/*Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/    
Declare @VendorAccountId int, @INVOICE_TRANSACTION_DATE datetime    
    
 select @INVOICE_TRANSACTION_DATE=TRANSACTION_DATE  from ACT_VENDOR_INVOICES (NOLOCK) where INVOICE_ID = @INVOICE_ID    
 --getting vendor a/c id from posting interface    
 select @VendorAccountId=GL.LIB_VENDOR_PAYB from ACT_GENERAL_LEDGER GL (NOLOCK) where  @INVOICE_TRANSACTION_DATE>=FISCAL_BEGIN_DATE and @INVOICE_TRANSACTION_DATE<=FISCAL_END_DATE    
     
INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS    
(    
ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,    
SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,    
AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID,     
IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID,     
SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID,     
TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,    
IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME   ,
TRAN_ENTITY,TRAN_DESC
)    
SELECT     
@VendorAccountId, @updatedFrom, NULL, @INVOICE_ID, NULL,    
INVOICE_NUM, TRANSACTION_DATE, DUE_DATE, Getdate(), -INVOICE_AMOUNT, NULL,    
NULL, NULL, null,null, null, @DIV_ID, @DEPT_ID, @PC_ID,    
NULL, NULL, INVOICE_AMOUNT, NULL, NULL, NULL, NULL,    
NULL, NULL, NULL,VENDOR_ID ,     
null, NULL, NULL, NULL,     
NULL, NULL, @MODIFIED_BY, NULL, NULL  ,
@TRAN_ENTITY ,REF_PO_NUM 
FROM     
ACT_VENDOR_INVOICES (NOLOCK)     
WHERE     
INVOICE_ID = @INVOICE_ID    
/*End Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/    

    
--STEP:6    
--*******************************************************DEBITING EXPENSE ACCOUNTS **********************************    
/*Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/    
 INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS    
 (    
  ACCOUNT_ID, UPDATED_FROM, SUBLEDGER_TYPE, SOURCE_ROW_ID, MAPPING_SUBLEDGER_ID,    
  SOURCE_NUM, SOURCE_TRAN_DATE, SOURCE_EFF_DATE, POSTING_DATE, TRANSACTION_AMOUNT, AGENCY_COMM_PERC,    
  AGENCY_COMM_AMT, AGENCY_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DIV_ID, DEPT_ID, PC_ID,     
  IS_PREBILL, BILL_CODE, GROSS_AMOUNT, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE, TRAN_ID, LOB_ID,     
  SUB_LOB_ID, COUNTRY_ID, STATE_ID, VENDOR_ID,     
  TAX_ID, ADNL_INFO, IS_BANK_RECONCILED, RECUR_JOURNAL_VERSION,    
  IN_BNK_RECON, IS_BALANCE_UPDATED, COMMITED_BY, COMMITED_BY_CODE, COMMITED_BY_NAME    ,
  TRAN_ENTITY,TRAN_DESC
 )    
 SELECT     
  ACCOUNT_ID, @updatedFrom, NULL, @INVOICE_ID, IDEN_ROW_ID,    
  INVOICE_NUM, TRANSACTION_DATE, DUE_DATE, Getdate(), DISTRIBUTION_AMOUNT , NULL,    
  NULL, NULL, null,null, null, @DIV_ID, @DEPT_ID, @PC_ID,    
  NULL, NULL, INVOICE_AMOUNT, NULL, NULL, NULL, NULL,    
  NULL, NULL, NULL,VENDOR_ID ,     
  null, NULL, NULL, NULL,     
  NULL, NULL, @MODIFIED_BY, NULL, NULL ,
  @TRAN_ENTITY ,ACT_DISTRIBUTION_DETAILS.NOTE
 FROM     
    ACT_DISTRIBUTION_DETAILS (NOLOCK) inner join ACT_VENDOR_INVOICES on GROUP_ID = INVOICE_ID    
  WHERE     
    GROUP_TYPE='VEN' AND INVOICE_ID=@INVOICE_ID    
 /*End Inserting the record in ACT_ACCOUNTS_POSTING_DETAILS*/    
    
    
    
--STEP:7    
/*Commiting the record*/    
update ACT_VENDOR_INVOICES    
set    
IS_COMMITTED  =   @IS_COMMITTED,    
DATE_COMMITTED  =   @DATE_COMMITTED ,    
MODIFIED_BY  =   @MODIFIED_BY  ,    
LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME   
where INVOICE_ID = @INVOICE_ID    
return 1    



END    
     
    
    
    
  


















GO

