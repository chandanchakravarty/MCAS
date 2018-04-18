IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_Deposit_Line_Items]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_Deposit_Line_Items]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*                 
----------------------------------------------------------                                      
Proc Name       : dbo.Proc_Insert_Deposit_Line_Items                            
Created by      : Pradeep Kushwaha                       
Date            : 25/Oct/2010                                      
Purpose         : Insert Depost Line items                                
Revison History :                                      
Used In        : Ebix Advantage                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------                      
drop proc Proc_Insert_Deposit_Line_Items      
exec Proc_Insert_Deposit_Line_Items       
*/                    
                         
CREATE PROC  [dbo].[Proc_Insert_Deposit_Line_Items]  
 (                      
 @CUSTOMER_ID   INT=NULL,                     -- ID OF CUSTOMER WHOSE  POLICY PREMIUM BILL INSTALLMENT WILL BE POSTED                     
 @POLICY_ID  INT=NULL,                        -- POLICY IDENTIFICATION NUMBER                    
 @POLICY_VERSION_ID SMALLINT=NULL,            -- POLICY VERSION IDENTIFICATION NUMBER                    
 @CD_LINE_ITEM_ID INT OUT,  
 @DEPOSIT_ID INT,  
 @RISK_PREMIUM DECIMAL(18,2),           -- POLICY PREMIUM AMOUNT                
 @INTEREST DECIMAL(18,2),   -- POLICY INTEREST AMOUNT                
 @FEE DECIMAL(18,2),              -- POLICY FEES                
 @TAX DECIMAL(18,2)=NULL,             -- POLICY TAXES                  
 @RECEIPT_AMOUNT DECIMAL(25,2),            -- POLIY TOTAL AMOUNT SUM OF ALL AMOUNT(PREMIUM AMOUNT + INTEREST AMOUNT + FEES + TAXES)       
 @LATE_FEE DECIMAL(18,2)=NULL,  
 @IS_ACTIVE NCHAR(2)=NULL,       
 @CREATED_BY INT=NULL,    
 @CREATED_DATETIME DATETIME=NULL,  
 @BOLETO_NO INT=NULL,  
 @POLICY_NUMBER NVARCHAR(21)=NULL,  
 @BANK_NUMBER NVARCHAR(25)=NULL,  
 @BANK_AGENCY_NUMBER NVARCHAR(25)=NULL,  
 @BANK_ACCOUNT_NUMBER NVARCHAR(25)=NULL,  
 @PAY_MODE INT=NULL,  
 @IS_EXCEPTION NCHAR(3)=NULL,  
 @CREATED_FROM NVARCHAR(5)=NULL,  
 @PAGE_ID NVARCHAR(50)=NULL,  
 @PAYMENT_DATE DATETIME =NULL,                     
 @EXCEPTION_REASON INT=NULL,  
 --Coinsurance Columns  
 @DEPOSIT_TYPE NVARCHAR(10)=NULL,  
 @INSTALLMENT_NO INT=NULL,  
 @COMMISSION_AMOUNT DECIMAL(18,2)=NULL,  
 @TOTAL_PREMIUM_COLLECTION DECIMAL(25,2)=null,
 @LEADER_POLICY_ID nvarchar(25)=null,
 @LEADER_DOC_ID nvarchar(10)=null,
 @RECEIPT_FROM_ID int=null,
 @CALLED_FROM NVARCHAR(50)=NULL,
 @INCORRECT_OUR_NUMBER NVARCHAR(25)
 )  
AS                      
BEGIN     
  DECLARE @IS_APPROVE CHAR(1)=NULL
  DECLARE @DEPOSIT_NUMBER INT=NULL
  DECLARE @RETVAL INT=1	
  --iTRACK-913	
  ---Total Premium Collection is not equal to Receipt Amount. then we put  this into exception case 
  IF(@DEPOSIT_TYPE='14831')
  BEGIN
  if(@BOLETO_NO IS NOT NULL AND @TOTAL_PREMIUM_COLLECTION<>@RECEIPT_AMOUNT AND ISNULL(@IS_ACTIVE,'N')='Y')
  BEGIN
   SET @EXCEPTION_REASON=391 --Total Premium Collection is not equal to Receipt Amount.
   SET @IS_EXCEPTION='Y' --YES  
  END
  END
  DECLARE @IS_COMMITED CHAR(1)='N'
  --iTrack # 1412/1148/1363	
  ---434- Payment against installment already paid.
  ---435-Payment against installment already in progress.
  IF(@DEPOSIT_TYPE='14832' or @DEPOSIT_TYPE='14916' or @DEPOSIT_TYPE='14917' or @DEPOSIT_TYPE='14918')--Co-Insurance -- Modified by pradeep - itrack -1049 
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
		IF(@IS_COMMITED='N')
		BEGIN
			 IF EXISTS( SELECT CD_LINE_ITEM_ID FROM   
						 ACT_CURRENT_DEPOSIT_LINE_ITEMS LINE_ITEMS  WITH(NOLOCK)  
						 INNER JOIN  ACT_CURRENT_DEPOSITS ITEMS  WITH(NOLOCK)  
						 ON LINE_ITEMS.DEPOSIT_ID=ITEMS.DEPOSIT_ID  
						 AND ISNULL(ITEMS.IS_COMMITED, 'N')<>'Y'  
						 AND ISNULL(LINE_ITEMS.IS_ACTIVE,'N')='Y' 
		 				 --Modified by Pradeep- for itrack-1049 	
						 AND (ISNULL(LINE_ITEMS.IS_EXCEPTION,'N')='N' 
		 				 OR ( LINE_ITEMS.EXCEPTION_REASON= 
		 												 CASE 
		 													 WHEN @DEPOSIT_TYPE='14832' THEN 409 --409	Change in Original Net Installment Amount..
		 													 WHEN @DEPOSIT_TYPE='14916' THEN 406 --406	Change in Broker Refund Amount.
		 													 WHEN @DEPOSIT_TYPE='14918' THEN 407 --407	Change in Follower Refund Amount.
		 												 END 
		 					)
		 				 )
						 --Modified till here
			 			 WHERE LINE_ITEMS.CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND INSTALLMENT_NO=@INSTALLMENT_NO
			 		  )
			BEGIN
				--Modified by Pradeep- for itrack-1049 	
				SET @EXCEPTION_REASON=CASE 
										 WHEN @DEPOSIT_TYPE='14832' THEN 435 --Payment against installment already in progress.
										 ELSE 444 ---444-Refund related to this installment was already in progress  
		 							  END 
				--Modified till here	
				SET @IS_EXCEPTION='Y' --YES  
			END
		END
		ELSE
		BEGIN
			--Modified by Pradeep- for itrack-1049 	
			SET @EXCEPTION_REASON=CASE 
									WHEN @DEPOSIT_TYPE='14832' THEN 434 --Payment against installment already paid.
									ELSE 443 ---443-Refund related to this installment was already received  
		 						   END 
			--Modified till here
			SET @IS_EXCEPTION='Y' --YES  
		END
	END
  END
  --Till here           
  IF(@BOLETO_NO IS NOT NULL)  
  BEGIN  
   ----------------------------- ADDED BY ATUL i-Track 931 NOTE 32 -- START
 
  SELECT @IS_COMMITED=ACT_CURRENT_DEPOSITS.IS_COMMITED FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)
  JOIN ACT_CURRENT_DEPOSITS WITH (NOLOCK)
  ON ACT_CURRENT_DEPOSIT_LINE_ITEMS.DEPOSIT_ID=ACT_CURRENT_DEPOSITS.DEPOSIT_ID
    WHERE BOLETO_NO=@BOLETO_NO AND ACT_CURRENT_DEPOSITS.IS_COMMITED='Y'
 ------------------------------  END 
 
  IF EXISTS(SELECT * FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)  WHERE BOLETO_NO=@BOLETO_NO)  
   BEGIN  
   IF (@IS_COMMITED='N' AND ISNULL(@IS_ACTIVE,'N')='Y')
		BEGIN
		    IF EXISTS( SELECT CD_LINE_ITEM_ID FROM   
						 ACT_CURRENT_DEPOSIT_LINE_ITEMS LINE_ITEMS  WITH(NOLOCK)  
						 INNER JOIN  ACT_CURRENT_DEPOSITS ITEMS  WITH(NOLOCK)  
						 ON LINE_ITEMS.DEPOSIT_ID=ITEMS.DEPOSIT_ID  
						 AND ISNULL(ITEMS.IS_COMMITED, 'N')<>'Y'  
						 AND ISNULL(LINE_ITEMS.IS_ACTIVE,'N')='Y' -- NOT FOR THE INACTIVE BOLETOS
		 				 AND (ISNULL(LINE_ITEMS.IS_EXCEPTION,'N')='N' or ( LINE_ITEMS.EXCEPTION_REASON =385 or LINE_ITEMS.EXCEPTION_REASON =391 or LINE_ITEMS.EXCEPTION_REASON =403 ))
			 			 WHERE LINE_ITEMS.BOLETO_NO=@BOLETO_NO 
			 		  )
			 
			 BEGIN
				SET @IS_APPROVE='R'-- Mark as refund if the same boleto exists in deposit line items table
				SET @EXCEPTION_REASON=294 -- Boleto  in progress
				SET @IS_EXCEPTION='Y' --YES  
				SET @RETVAL=-10
			END
		END
	ELSE
	BEGIN
	 IF (ISNULL(@IS_ACTIVE,'N')='Y')
	 BEGIN
			SET @EXCEPTION_REASON=408 --Already Paid Boleto  
			SET @IS_EXCEPTION='Y' --YES 
	 END
	END 
	 
   END   
  END  
 SELECT @CD_LINE_ITEM_ID=ISNULL(MAX(CD_LINE_ITEM_ID),0)+1 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS WITH(NOLOCK)   
 IF(@CALLED_FROM='RTL')--Added for itrack 913 on 19-May-2011
 BEGIN 
 UPDATE ACT_CURRENT_DEPOSITS SET DEPOSIT_TRAN_DATE=@PAYMENT_DATE WHERE DEPOSIT_ID=@DEPOSIT_ID
 END   
 
 INSERT INTO ACT_CURRENT_DEPOSIT_LINE_ITEMS                
 (  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CD_LINE_ITEM_ID, DEPOSIT_ID,RISK_PREMIUM,  
 INTEREST,FEE,TAX,RECEIPT_AMOUNT,LATE_FEE ,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,BOLETO_NO,POLICY_NUMBER,BANK_NUMBER ,  
 BANK_AGENCY_NUMBER,BANK_ACCOUNT_NUMBER,PAY_MODE ,IS_EXCEPTION ,CREATED_FROM,PAGE_ID,PAYMENT_DATE ,EXCEPTION_REASON,  
 --Coinsurance Columns  
 DEPOSIT_TYPE,INSTALLMENT_NO,COMMISSION_AMOUNT,TOTAL_PREMIUM_COLLECTION,LEADER_POLICY_ID,LEADER_DOC_ID ,
 RECEIPT_FROM_ID ,INCORRECT_OUR_NUMBER,IS_APPROVE
 )  
 values(@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@CD_LINE_ITEM_ID,@DEPOSIT_ID,@RISK_PREMIUM,  
   @INTEREST,@FEE,@TAX,@RECEIPT_AMOUNT,@LATE_FEE,@IS_ACTIVE,@CREATED_BY,@CREATED_DATETIME,@BOLETO_NO,  
   @POLICY_NUMBER,@BANK_NUMBER,@BANK_AGENCY_NUMBER,@BANK_ACCOUNT_NUMBER,@PAY_MODE,@IS_EXCEPTION,@CREATED_FROM,@PAGE_ID,@PAYMENT_DATE,@EXCEPTION_REASON,  
   --Coinsurance Columns  
   @DEPOSIT_TYPE,@INSTALLMENT_NO,@COMMISSION_AMOUNT  ,@TOTAL_PREMIUM_COLLECTION,@LEADER_POLICY_ID,@LEADER_DOC_ID,
   @RECEIPT_FROM_ID,@INCORRECT_OUR_NUMBER,@IS_APPROVE
   )  
    
 --Updating the total amount                
 UPDATE ACT_CURRENT_DEPOSITS                
 SET TOTAL_DEPOSIT_AMOUNT = (SELECT SUM(RECEIPT_AMOUNT)                 
 FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS   WITH(NOLOCK)               
 WHERE DEPOSIT_ID = @DEPOSIT_ID)                
 WHERE DEPOSIT_ID = @DEPOSIT_ID                
   if @@Error <> 0                
 BEGIN                 
 --Some error occured                
  RETURN -1                
 END              
 else
 return @RETVAL            
END    
        
                   