IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_Deposit_Line_Items]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_Deposit_Line_Items]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_Update_Deposit_Line_Items                          
Created by      : Pradeep Kushwaha                     
Date            : 25/Oct/2010                                    
Purpose         : Update Depost Line items                              
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_Update_Deposit_Line_Items    
exec Proc_Update_Deposit_Line_Items     
*/                  
                       
CREATE PROC  [dbo].[Proc_Update_Deposit_Line_Items]
 (                    
 @CUSTOMER_ID   INT,                     -- ID OF CUSTOMER WHOSE  POLICY PREMIUM BILL INSTALLMENT WILL BE POSTED                   
 @POLICY_ID  INT,                        -- POLICY IDENTIFICATION NUMBER                  
 @POLICY_VERSION_ID SMALLINT,            -- POLICY VERSION IDENTIFICATION NUMBER                  
 @CD_LINE_ITEM_ID INT OUT,
 @DEPOSIT_ID INT,
 @RISK_PREMIUM DECIMAL(18,2),           -- POLICY PREMIUM AMOUNT              
 @INTEREST DECIMAL(18,2),   -- POLICY INTEREST AMOUNT              
 @FEE DECIMAL(18,2),              -- POLICY FEES              
 @TAX DECIMAL(18,2),             -- POLICY TAXES                
 @RECEIPT_AMOUNT DECIMAL(25,2),            -- POLIY TOTAL AMOUNT SUM OF ALL AMOUNT(PREMIUM AMOUNT + INTEREST AMOUNT + FEES + TAXES)     
 @LATE_FEE DECIMAL(18,2)=NULL,
 @MODIFIED_BY INT,
 @LAST_UPDATED_DATETIME DATETIME,
 @BOLETO_NO INT=NULL,
 @POLICY_NUMBER NVARCHAR(21)=NULL,
 @BANK_NUMBER NVARCHAR(25)=NULL,
 @BANK_AGENCY_NUMBER NVARCHAR(25)=NULL,
 @BANK_ACCOUNT_NUMBER NVARCHAR(25)=NULL,
 @PAY_MODE INT,
 @IS_EXCEPTION NCHAR(3)=NULL,
 @IS_APPROVE NCHAR(1)=NULL,
 @EXCEPTION_REASON INT=NULL,
 @TOTAL_PREMIUM_COLLECTION DECIMAL(25,2)=null,
 @RECEIPT_FROM_ID int=null,
 @COMMISSION_AMOUNT DECIMAL(18,2)=NULL,
 @PAYMENT_DATE DATETIME =NULL       
) 
AS                    
BEGIN         

--------------------------------------- ADDED BY ATUL  START
---------------------------------------- FETCH DEPOSIT TYPE
DECLARE @DEPOSIT_TYPE NVARCHAR(5)
DECLARE @IS_ACTIVE NCHAR(1)
DECLARE @RECEIPT_DATE DATETIME
DECLARE @EXPTION_REASON INT
DECLARE @INSTALLMENT_NO INT --itrack 1148/1363
SELECT @DEPOSIT_TYPE=DEPOSIT_TYPE,@IS_ACTIVE=IS_ACTIVE,@RECEIPT_DATE=PAYMENT_DATE,@EXPTION_REASON=EXCEPTION_REASON,@INSTALLMENT_NO=INSTALLMENT_NO FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH (NOLOCK)
WHERE CD_LINE_ITEM_ID=@CD_LINE_ITEM_ID
--------------------------------------- END
  --iTRACK-913	
  ---Total Premium Collection is not equal to Receipt Amount. then we put this into exception case 

   IF(@DEPOSIT_TYPE='14831')
   BEGIN 
   --SET  @RECEIPT_DATE=@PAYMENT_DATE commented for itrack- 1515
  if(@BOLETO_NO IS NOT NULL AND @TOTAL_PREMIUM_COLLECTION<>@RECEIPT_AMOUNT AND ISNULL(@IS_ACTIVE,'N')='Y' AND ISNULL(@EXPTION_REASON,0)<>294)
  BEGIN
   -- 294 --Boleto already has a deposit item in progress
   SET @EXCEPTION_REASON=391 --Total Premium Collection is not equal to Receipt Amount.
   SET @IS_EXCEPTION='Y' --YES  
  END
  END
   DECLARE @IS_COMMITED CHAR(1)='N'
   --iTrack # 1412/1148/1363
  ---434- Payment against installment already paid.
  ---435-Payment against installment already in progress.
  IF(@DEPOSIT_TYPE='14832' or @DEPOSIT_TYPE='14916' or @DEPOSIT_TYPE='14917' or @DEPOSIT_TYPE='14918')--Co-Insurance 
  BEGIN
  --Added to check if the total premium collection is not equal to receipt amount then mark it as exception - discuss with Anurag
  IF(@TOTAL_PREMIUM_COLLECTION<>@RECEIPT_AMOUNT AND ISNULL(@IS_ACTIVE,'N')='Y')
  BEGIN
   SET @EXCEPTION_REASON=436 --Total Premium Collection is not equal to Net Installment Amount.
   SET @IS_EXCEPTION='Y' --YES  
  END
  --Added till here
   SELECT @IS_COMMITED=ACT_CURRENT_DEPOSITS.IS_COMMITED FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)
   JOIN ACT_CURRENT_DEPOSITS WITH (NOLOCK)
   ON ACT_CURRENT_DEPOSIT_LINE_ITEMS.DEPOSIT_ID=ACT_CURRENT_DEPOSITS.DEPOSIT_ID
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NO AND ACT_CURRENT_DEPOSITS.IS_COMMITED='Y'
    
	IF EXISTS(SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NO)
	BEGIN
		IF(@IS_COMMITED='Y')
		BEGIN
			--Modified by Pradeep as per ther itrack-1049
			SET @EXCEPTION_REASON=CASE 
									WHEN @DEPOSIT_TYPE='14832' THEN 434 --Payment against installment already paid.
									ELSE 443 ---443-Refund related to this installment was already received  
		 						   END 
			--Modified tilh here 
			SET @IS_EXCEPTION='Y' --YES  
		END
	END 
  END
  --ADDED TILL HERE	 
  IF(@BOLETO_NO IS NOT NULL)
   BEGIN  
 
  
  SELECT @IS_COMMITED=ACT_CURRENT_DEPOSITS.IS_COMMITED FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)
  JOIN ACT_CURRENT_DEPOSITS WITH (NOLOCK)
  ON ACT_CURRENT_DEPOSIT_LINE_ITEMS.DEPOSIT_ID=ACT_CURRENT_DEPOSITS.DEPOSIT_ID
    WHERE BOLETO_NO=@BOLETO_NO AND ACT_CURRENT_DEPOSITS.IS_COMMITED='Y'
    
  IF EXISTS(SELECT * FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)  WHERE BOLETO_NO=@BOLETO_NO )  
   BEGIN  
		IF (@IS_COMMITED='Y')
		BEGIN
			SET @EXCEPTION_REASON=408 --Already Paid Boleto  
			SET @IS_EXCEPTION='Y' --YES 
			--SET @EXCEPTION_REASON=294 -- Boleto  in progress
			--SET @IS_EXCEPTION='Y' --YES 		
		
		END
		 
		
   END   
  END  
  
  
 UPDATE   ACT_CURRENT_DEPOSIT_LINE_ITEMS SET 
 RISK_PREMIUM=@RISK_PREMIUM,
 INTEREST=@INTEREST,
 FEE=@FEE,
 TAX=@TAX,
 RECEIPT_AMOUNT=@RECEIPT_AMOUNT,
 LATE_FEE=@LATE_FEE,
 MODIFIED_BY=@MODIFIED_BY,
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,
 BOLETO_NO=@BOLETO_NO,
 POLICY_NUMBER=@POLICY_NUMBER,
 BANK_NUMBER=@BANK_NUMBER,
 BANK_AGENCY_NUMBER= @BANK_AGENCY_NUMBER,
 BANK_ACCOUNT_NUMBER=@BANK_ACCOUNT_NUMBER,
 PAY_MODE=@PAY_MODE,
 IS_EXCEPTION=@IS_EXCEPTION,
 IS_APPROVE=@IS_APPROVE,
 EXCEPTION_REASON= @EXCEPTION_REASON,
 TOTAL_PREMIUM_COLLECTION=@TOTAL_PREMIUM_COLLECTION,
 RECEIPT_FROM_ID=@RECEIPT_FROM_ID,
 COMMISSION_AMOUNT=@COMMISSION_AMOUNT,
 PAYMENT_DATE=@PAYMENT_DATE --itrack- 1515
WHERE 
CD_LINE_ITEM_ID=@CD_LINE_ITEM_ID AND 
DEPOSIT_ID= @DEPOSIT_ID
   --Updating the total amount              
 UPDATE ACT_CURRENT_DEPOSITS              
 SET TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT)               
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS   WITH(NOLOCK)             
 WHERE DEPOSIT_ID = @DEPOSIT_ID)              
 WHERE DEPOSIT_ID = @DEPOSIT_ID              
            
   
END  
      
                 

GO

