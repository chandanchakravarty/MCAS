IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_EXPENSE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_EXPENSE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_UpdateCLM_ACTIVITY_EXPENSE_BREAKDOWN  
Created by      : Vijay Arora  
Date            : 6/6/2006  
Purpose     : To update the record in table named CLM_ACTIVITY_EXPENSE_BREAKDOWN  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC dbo.Proc_UpdateCLM_ACTIVITY_EXPENSE_BREAKDOWN  
(  
 @CLAIM_ID     int,  
 @EXPENSE_ID     int,  
 @EXPENSE_BREAKDOWN_ID     int,  
 @TRANSACTION_CODE     int,  
 @COVERAGE_ID     int,  
 @PAID_AMOUNT     decimal(9),  
 @MODIFIED_BY     int,  
 @ACTIVITY_ID     int  
)  
AS  
BEGIN  


--Check whether the addition of current amount to the existing amount makes the total amount greater than  
  --total expense payment stored under clm_activity_payment  
  DECLARE @TOTAL_EXPENSE_BREAKDOWN_AMOUNT DECIMAL(12,2)  
  DECLARE @TOTAL_PAYMENT DECIMAL(12,2)  
  SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM clm_activity_expense WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
    
  SELECT @TOTAL_EXPENSE_BREAKDOWN_AMOUNT=ISNULL(SUM(PAID_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE_BREAKDOWN WHERE 
		CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  AND EXPENSE_ID=@EXPENSE_ID and 
		EXPENSE_BREAKDOWN_ID <> @EXPENSE_BREAKDOWN_ID
    
  SET @TOTAL_EXPENSE_BREAKDOWN_AMOUNT = @TOTAL_EXPENSE_BREAKDOWN_AMOUNT + ISNULL(@PAID_AMOUNT,0)      
 
   
  IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<1 OR @TOTAL_EXPENSE_BREAKDOWN_AMOUNT>@TOTAL_PAYMENT)  
    RETURN -1 


UPDATE  CLM_ACTIVITY_EXPENSE_BREAKDOWN  
SET  
TRANSACTION_CODE  =  @TRANSACTION_CODE,  
COVERAGE_ID  =  @COVERAGE_ID,  
PAID_AMOUNT  =  @PAID_AMOUNT,  
MODIFIED_BY  =  @MODIFIED_BY,  
LAST_UPDATED_DATETIME  =  GETDATE()  
WHERE CLAIM_ID = @CLAIM_ID AND EXPENSE_ID = @EXPENSE_ID AND EXPENSE_BREAKDOWN_ID = @EXPENSE_BREAKDOWN_ID   
AND ACTIVITY_ID = @ACTIVITY_ID  
END  



GO

