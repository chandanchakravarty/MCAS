IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommitToSpoolACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommitToSpoolACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------      
Proc Name       : Proc_CommitToSpoolACT_CURRENT_DEPOSITS      
Created by      : Praveen Kasana 
Date            : 24/March/2008      
Purpose			: Update Spool Status of Current Deposit     
Revison History :      
Used In  : Wolverine      
      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       --------------------------------*/      
-- drop proc dbo.Proc_CommitToSpoolACT_CURRENT_DEPOSITS    
CREATE PROC dbo.Proc_CommitToSpoolACT_CURRENT_DEPOSITS      
(      
	@DEPOSIT_ID     int,
	@MODIFIED_BY	int,      
	@DATE_COMMITED_TO_SPOOL Datetime
		   
)      
AS      
BEGIN      
  
/*Variable declaration, to be used in this proc*/          
    
DECLARE @DEPOSIT_TYPE_CUSTOMER Varchar(5),    
  @DEPOSIT_TYPE_AGENCY Varchar(5),    
  @DEPOSIT_TYPE_CLAIM Varchar(5),    
  @DEPOSIT_TYPE_REINSURANCE Varchar(5),    
  @DEPOSIT_TYPE_MISC Varchar(5),    
  @ITEM_TRAN_CODE_TYPE Varchar(10),    
  @ITEM_TRAN_CODE Varchar(10),    
  @EFT_MODE Int,    
  @CREDIT_CARD Int,  
  @RECEIPT_AMOUNT decimal(18,2) --Can not Commit if Amount is Neagtive:  
      
  
SET @EFT_MODE  = 11976    
SET @CREDIT_CARD   = 11974    
SET @ITEM_TRAN_CODE_TYPE = 'DEP'    
SET @ITEM_TRAN_CODE  = 'DEP'    
SET @DEPOSIT_TYPE_CUSTOMER = 'CUST'    
SET @DEPOSIT_TYPE_AGENCY = 'AGN'    
SET @DEPOSIT_TYPE_CLAIM = 'CLAM'    
SET @DEPOSIT_TYPE_REINSURANCE = 'RINS'    
SET @DEPOSIT_TYPE_MISC ='MISC'    
    
        
/*Checking whether specified deposit id is commited or whther exists and any other conditions*/        
IF Not Exists(SELECT DEPOSIT_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID)        
BEGIN        
 --Record not existed, hence exiting         
 return -1        
END        
  
--Checking whether already commited        
IF (SELECT Upper(IS_COMMITED) FROM ACT_CURRENT_DEPOSITS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'         
BEGIN        
 --Record already commited hence exiting        
 return -2        
END        
     
IF (SELECT Count(DEPOSIT_ID) FROM ACT_CURRENT_DEPOSITS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID) = 0        
BEGIN        
 --Line items does not exists hence can not commit        
 return -4        
END        
         
IF (SELECT IS_CONFIRM FROM ACT_CURRENT_DEPOSITS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID) <> 'Y'        
BEGIN        
 --Line items does not exists hence can not commit        
 return -6        
END        
    
----- Check if a particular deposit line item type has a balance of 0 amount    
-- Misc Rec    
IF EXISTS(SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)    
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE_MISC AND RECEIPT_AMOUNT = 0)    
BEGIN    
 RETURN -10    
END    
    
    
--- Cust    
IF EXISTS (SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)    
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE_CUSTOMER AND RECEIPT_AMOUNT = 0)    
BEGIN     
 RETURN -11    
END    
    
-- Agency Receipts    
IF EXISTS (SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)    
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE_AGENCY AND RECEIPT_AMOUNT = 0)    
BEGIN    
 RETURN -12    
END    
    
-- Claim & Reins Receipts    
IF EXISTS (SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)    
  WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE IN(@DEPOSIT_TYPE_CLAIM,@DEPOSIT_TYPE_REINSURANCE)    
AND RECEIPT_AMOUNT = 0)    
BEGIN      
 RETURN -13    
END    
--Restrict usert to commit deposit with -ve amount  
IF EXISTS (SELECT RECEIPT_AMOUNT FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID  
    AND (ISNULL(RECEIPT_AMOUNT,0) <= 0 ))  
BEGIN        
 -- amount is -ve hence exiting       
 return -14        
END    


Declare @DEPOSIT_TYPE varchar(15)   --Stores the type of journal entry, whether manual, template, recurring..        
Declare @UPDATED_FROM nchar(1)    --Value to be inserted in UPDATED_FROM field in ACT_ACCOUNTS_POSTING_DETAILS        
Declare @LINE_ITEM_TOTAL_AMOUNT decimal --Sum to amount in Line item table of specified journal id        
Declare @COMMITED_BY_CODE nvarchar(20), @COMMITED_BY_NAME nvarchar(60), @FISCAL_ID int, @TRANS_DATE datetime        
Declare @LOCK_DATE datetime, @ITEM_STATUS varchar(2)    
Declare @ACCOUNT_ID varchar(5) -- Stores Account ID in case of Claims/Reinsurance Acc    
/*Initialisation of variables*/        
SET @UPDATED_FROM = 'D'     --Updated from, should be D for Deposit        
    
    
    
SET @DATE_COMMITED_TO_SPOOL = GETDATE()         
    
--Retreiving values from Deposit Master and Deposit Line Items        
SELECT  @DEPOSIT_TYPE = CD.DEPOSIT_TYPE,         
  @FISCAL_ID = CD.FISCAL_ID,         
  @TRANS_DATE = CD.DEPOSIT_TRAN_DATE        
FROM ACT_CURRENT_DEPOSITS CD (NOLOCK)       
INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI        
 ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID        
WHERE CD.DEPOSIT_ID = @DEPOSIT_ID        
GROUP BY  CD.DEPOSIT_TYPE, CD.FISCAL_ID, CD.DEPOSIT_TRAN_DATE        
    
--Ravindra(09-10-2007) If fiscal id is null or 0 fetch Fiscal ID based on Current Date  
IF(@FISCAL_ID IS NULL OR @FISCAL_ID = 0 )  
BEGIN   
 SELECT @FISCAL_ID =  FISCAL_ID FROM ACT_GENERAL_LEDGER   
 WHERE CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME)   >=   
    CAST(CONVERT(VARCHAR, FISCAL_BEGIN_DATE,101) AS DATETIME)  
   AND CAST(CONVERT(VARCHAR, GETDATE() ,101) AS DATETIME) <=  
    CAST(CONVERT(VARCHAR, FISCAL_END_DATE,101) AS DATETIME)  
END    
  
SELECT @COMMITED_BY_CODE = USER_LOGIN_ID, @COMMITED_BY_NAME = IsNull(USER_FNAME,'') + ' ' + IsNull(USER_LNAME,'')        
FROM MNT_USER_LIST (NOLOCK)        
WHERE [USER_ID] = @MODIFIED_BY        
    
--Retreiving the lock day        
SELECT @LOCK_DATE = POSTING_LOCK_DATE        
FROM ACT_GENERAL_LEDGER (NOLOCK)       
WHERE FISCAL_ID = @FISCAL_ID        
/*End variables initialisation*/        
        
/*Checking for any type of invalid entry or any mandatory conditions*/        
    
/*Checking the locking dates*/        
IF @TRANS_DATE <= @LOCK_DATE        
BEGIN        
 return -5 --Can not committ any transaction after lock date        
END        
    
---For agency receipt , checking whether deposit amount is fully reconciled or not       
IF EXISTS( SELECT ISNULL(IS_RECONCILED,'N') FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS  CDLI  (NOLOCK)      
   INNER JOIN ACT_CURRENT_DEPOSITS CD     
   ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID        
   AND CDLI.DEPOSIT_TYPE = @DEPOSIT_TYPE_AGENCY    
   AND CDLI.IS_RECONCILED <> 'Y'    
   WHERE CD.DEPOSIT_ID = @DEPOSIT_ID  )    
BEGIN        
 --Some line items are completely reconciled hence can not commit        
 return -7        
END  

--in case of Deposit From EFT batch job there will be Customer Line Items and Mode of Payment EFT  
IF NOT EXISTS(SELECT DEPOSIT_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID    
    AND CREATED_FROM = 'EFT' )        
BEGIN       
 -- If Mode Of Reciept is EFT only Agency Deposits can exists    
 IF( (SELECT RECEIPT_MODE FROM ACT_CURRENT_DEPOSITS (NOLOCK) WHERE DEPOSIT_ID = @DEPOSIT_ID) = @EFT_MODE)    
 BEGIN     
  IF EXISTS(SELECT DEPOSIT_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)     
     WHERE DEPOSIT_ID = @DEPOSIT_ID  AND DEPOSIT_TYPE <> @DEPOSIT_TYPE_AGENCY     
    )        
  BEGIN     
   RETURN -9    
  END 
END
END


--- For Misc Deposit    
IF EXISTS ( SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)    
   WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE_MISC)    
BEGIN     
--Checking amount is completelydistributed or not        
--  IF NOT EXISTS (select iden_row_id FROM  ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI (NOLOCK)        
--   inner JOIN ACT_DISTRIBUTION_DETAILS AD        
--   ON AD.GROUP_TYPE='DEP' AND AD.GROUP_ID = CDLI.CD_LINE_ITEM_ID        
--   INNER JOIN ACT_CURRENT_DEPOSITS CD ON        
--   CDLI.DEPOSIT_ID = CD.DEPOSIT_ID        
--   AND CDLI.DEPOSIT_TYPE = 'MISC'    
--   WHERE CD.DEPOSIT_ID = @DEPOSIT_ID    )    
--  BEGIN     
--   --Deposit not distributed    
--   return -8          
--  END    
 IF EXISTS( SELECT SUM(AD.DISTRIBUTION_AMOUNT) FROM  ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI (NOLOCK)       
    LEFT JOIN ACT_DISTRIBUTION_DETAILS AD        
    ON AD.GROUP_TYPE='DEP' AND AD.GROUP_ID = CDLI.CD_LINE_ITEM_ID        
    INNER JOIN ACT_CURRENT_DEPOSITS CD ON        
    CDLI.DEPOSIT_ID = CD.DEPOSIT_ID        
    AND CDLI.DEPOSIT_TYPE = @DEPOSIT_TYPE_MISC     
    WHERE CD.DEPOSIT_ID = @DEPOSIT_ID        
    GROUP BY CDLI.CD_LINE_ITEM_ID, CDLI.RECEIPT_AMOUNT        
    HAVING isnull(RECEIPT_AMOUNT,0) <> SUM(isnull(AD.DISTRIBUTION_AMOUNT,0)))        
 BEGIN        
   --Deposit not distributed completely    
  return -8          
 END        
END       	 

------------------
    --Committing the record      
 UPDATE ACT_CURRENT_DEPOSITS      
 SET IS_COMMITED_TO_SPOOL = 'Y', LAST_UPDATED_DATETIME = @DATE_COMMITED_TO_SPOOL , MODIFIED_BY = @MODIFIED_BY     
 WHERE DEPOSIT_ID = @DEPOSIT_ID      
       
 return 1      
END      











GO

