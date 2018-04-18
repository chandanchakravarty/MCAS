IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_EXPENSE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_EXPENSE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY_EXPENSE_BREAKDOWN    
Created by      : Vijay Arora    
Date            : 6/6/2006    
Purpose     : To insert a record in table named CLM_ACTIVITY_EXPENSE_BREAKDOWN    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_EXPENSE_BREAKDOWN    
(    
 @CLAIM_ID     int,    
 @EXPENSE_ID     int,    
 @EXPENSE_BREAKDOWN_ID     int OUTPUT,    
 @TRANSACTION_CODE     int,    
 @COVERAGE_ID     int,    
 @PAID_AMOUNT     decimal(9),    
 @CREATED_BY     int,    
 @ACTIVITY_ID     int    
)    
AS    
BEGIN    


--Check whether the addition of current amount to the existing amount makes the total amount greater than  
  --total expense payment stored under clm_activity_payment  
  DECLARE @TOTAL_EXPENSE_BREAKDOWN_AMOUNT DECIMAL(12,2)  
  DECLARE @TOTAL_PAYMENT DECIMAL(12,2)  
  SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM clm_activity_expense WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  
    
  SELECT @TOTAL_EXPENSE_BREAKDOWN_AMOUNT=ISNULL(SUM(PAID_AMOUNT),0) FROM CLM_ACTIVITY_EXPENSE_BREAKDOWN WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID  AND EXPENSE_ID=@EXPENSE_ID
    
  SET @TOTAL_EXPENSE_BREAKDOWN_AMOUNT = @TOTAL_EXPENSE_BREAKDOWN_AMOUNT + ISNULL(@PAID_AMOUNT,0)      
 
   
  IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<1 OR @TOTAL_EXPENSE_BREAKDOWN_AMOUNT>@TOTAL_PAYMENT)  
    RETURN -1  
  
select @EXPENSE_BREAKDOWN_ID=isnull(Max(EXPENSE_BREAKDOWN_ID),0)+1 from CLM_ACTIVITY_EXPENSE_BREAKDOWN   
where CLAIM_ID = @CLAIM_ID AND EXPENSE_ID=@EXPENSE_ID AND ACTIVITY_ID=@ACTIVITY_ID  
INSERT INTO CLM_ACTIVITY_EXPENSE_BREAKDOWN    
(    
 CLAIM_ID,    
 EXPENSE_ID,    
 EXPENSE_BREAKDOWN_ID,    
 TRANSACTION_CODE,    
 COVERAGE_ID,    
 PAID_AMOUNT,    
 IS_ACTIVE,    
 CREATED_BY,    
 CREATED_DATETIME,    
 ACTIVITY_ID    
)    
VALUES    
(    
 @CLAIM_ID,    
 @EXPENSE_ID,    
 @EXPENSE_BREAKDOWN_ID,    
 @TRANSACTION_CODE,    
 @COVERAGE_ID,    
 @PAID_AMOUNT,    
 'Y',    
 @CREATED_BY,    
 GETDATE(),    
 @ACTIVITY_ID    
)    
END    
    
  



GO

