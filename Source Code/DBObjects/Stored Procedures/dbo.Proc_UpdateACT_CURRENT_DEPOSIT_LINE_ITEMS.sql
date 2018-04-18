IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       :Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS    
Created by      :Vijay Joshi    
Date            :6/23/2005    
Purpose     :Update record in ACT_CURRENT_DEPOSIT_LINE_ITEMS    a
Revison History :    
Used In  :Wolverine    
return value    
-4  Deposit has been commited, hence can not be updated    
-2  Invalid policy number    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS    
CREATE PROC dbo.Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS    
(    
 @CD_LINE_ITEM_ID    int,    
 @DEPOSIT_ID         int,    
 @LINE_ITEM_INTERNAL_NUMBER      int,    
 @ACCOUNT_ID         int,    
 @RECEIPT_AMOUNT        decimal(18,2),    
 @PAYOR_TYPE         nvarchar(10),    
 @RECEIPT_FROM_ID        int,    
 @RECEIPT_FROM_CODE        nvarchar(14),    
 @RECEIPT_FROM_NAME        nvarchar(255),    
 @POLICY_ID          smallint,    
 @POLICY_VERSION_ID        smallint,    
 @MODIFIED_BY         int,    
 @LAST_UPDATED_DATETIME       datetime,    
 @POLICY_NUMBER     varchar(8),    
 @CLAIM_NUMBER     varchar(20),    
 @POLICY_MONTH     smallint,    
 @MONTH_YEAR      INT,  
 @DEPOSIT_TYPE nvarchar(5) = null  
)    
AS    
BEGIN    
  

--Checking if the deposit has been commited or not, if commited then can not be update    
if (SELECT IsNull(IS_COMMITED,'') FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'    
	return -4 --can not proceed further as deposit has been commited    
    
 /*Retreiving the type of deposit as validation of input is defferent depending on deposit type*/    
--Commented the deposit type as it will be sent thru sp parameters  
-- Declare @DEPOSIT_TYPE varchar(6)    
-- SELECT @DEPOSIT_TYPE = DEPOSIT_TYPE FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID    
     
/*Checking for the validatity of policy number and other validation*/     
if @DEPOSIT_TYPE = 'CUST'    
BEGIN    
	--Validating the inputs of customer receipts (deposit line item)    
	
	--Checking whether policy id is valid or not    
	SELECT @POLICY_ID = IsNull(POLICY_ID,0), @POLICY_VERSION_ID = POLICY_VERSION_ID    
	FROM POL_CUSTOMER_POLICY_LIST     
	WHERE POLICY_NUMBER = @POLICY_NUMBER    
	if IsNull(@POLICY_ID,0) = 0     
	BEGIN    
		--Policy number is not valid, hence exiting with return status    
		return -2       
	END    
END    
    
    
UPDATE ACT_CURRENT_DEPOSIT_LINE_ITEMS    
SET CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID,     
DEPOSIT_ID = @DEPOSIT_ID,     
LINE_ITEM_INTERNAL_NUMBER = @LINE_ITEM_INTERNAL_NUMBER,     
ACCOUNT_ID = @ACCOUNT_ID,    
DEPOSIT_TYPE = @DEPOSIT_TYPE,     
RECEIPT_AMOUNT = @RECEIPT_AMOUNT,     
PAYOR_TYPE = @PAYOR_TYPE,     
RECEIPT_FROM_ID = @RECEIPT_FROM_ID,     
RECEIPT_FROM_CODE = @RECEIPT_FROM_CODE,    
RECEIPT_FROM_NAME = @RECEIPT_FROM_NAME,     
POLICY_ID = @POLICY_ID,     
POLICY_VERSION_ID = @POLICY_VERSION_ID,     
MODIFIED_BY = @MODIFIED_BY,     
LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,    
POLICY_NUMBER = @POLICY_NUMBER,    
CLAIM_NUMBER = @CLAIM_NUMBER,    
POLICY_MONTH = @POLICY_MONTH,    
MONTH_YEAR = @MONTH_YEAR
WHERE    
CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID    
    
--Updating the total amount    
UPDATE ACT_CURRENT_DEPOSITS    
SET     
TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT)     
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS     
WHERE DEPOSIT_ID = @DEPOSIT_ID),    
IS_CONFIRM = 'N'    
WHERE DEPOSIT_ID = @DEPOSIT_ID    
     
     
IF @DEPOSIT_TYPE = 'AGNC'    
BEGIN    
	
	--Deleting the old reconciliation    
	DECLARE @GROUP_ID INT    
	SELECT @GROUP_ID = GROUP_ID FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID    
	
	--Updating the reconciled amount in open item table        
	
	DELETE FROM ACT_CUSTOMER_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID    
	DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID    
	DELETE FROM ACT_RECONCILIATION_GROUPS WHERE GROUP_ID = @GROUP_ID    
	DELETE FROM ACT_CUSTOMER_OPEN_ITEMS WHERE SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND UPDATED_FROM = 'D'     
	
	SELECT *     
	FROM ACT_AGENCY_STATEMENT, ACT_AGENCY_OPEN_ITEMS       
	WHERE     
	ACT_AGENCY_STATEMENT.AGENCY_OPEN_ITEM_ID = ACT_AGENCY_OPEN_ITEMS.IDEN_ROW_ID    
	AND ACT_AGENCY_OPEN_ITEMS.SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND ACT_AGENCY_OPEN_ITEMS.UPDATED_FROM = 'D'    
	
	
	DELETE ACT_AGENCY_STATEMENT    
	FROM ACT_AGENCY_OPEN_ITEMS       
	WHERE     
	ACT_AGENCY_STATEMENT.AGENCY_OPEN_ITEM_ID = ACT_AGENCY_OPEN_ITEMS.IDEN_ROW_ID    
	AND ACT_AGENCY_OPEN_ITEMS.SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND ACT_AGENCY_OPEN_ITEMS.UPDATED_FROM = 'D'    
	
	DELETE FROM ACT_AGENCY_OPEN_ITEMS WHERE SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND UPDATED_FROM = 'D'    
	
	--Making the auto apply open items    
	DECLARE @ReturnCode SMALLINT    
	EXECUTE @ReturnCode = Proc_AutoApplyOpenItems @CD_LINE_ITEM_ID, NULL, NULL, NULL    
	
	if @ReturnCode = 1    
		return 2 --Reconciled sucessfully    
	else    
		return 1 --Unable to reconciled      
	END    
	RETURN 1     
END    
    





GO

