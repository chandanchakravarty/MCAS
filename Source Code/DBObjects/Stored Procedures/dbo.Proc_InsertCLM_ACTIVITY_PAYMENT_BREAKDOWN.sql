IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                  
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN                                                                      
Created by      : Sumit Chhabra                                                                                
Date            : 05/06/2006                                                                                  
Purpose         : Insert Claim Payment Breakdown data    
Created by      : Sumit Chhabra                                                                                 
Revison History :                                                                                  
Used In        : Wolverine                                                                                  
------------------------------------------------------------                                                                                  
Date     Review By          Comments                                                                                  
------   ------------       -------------------------*/                                                                                  
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN        
@CLAIM_ID int,    
@ACTIVITY_ID int,    
@PAYMENT_BREAKDOWN_ID int output,    
@TRANSACTION_CODE int,    
@COVERAGE_ID int,    
@PAID_AMOUNT decimal(10,2),    
@CREATED_BY int,    
@CREATED_DATETIME   datetime    
AS                                                                                  
BEGIN           
    
--Check whether the addition of current payment to the existing payments makes the total payment greater than
--total payment stored under clm_activity_payment
DECLARE @TOTAL_PAYMENT_BREAKDOWN DECIMAL(12,2)
DECLARE @TOTAL_PAYMENT DECIMAL(12,2)
SELECT @TOTAL_PAYMENT=ISNULL(SUM(PAYMENT_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID

SELECT @TOTAL_PAYMENT_BREAKDOWN=ISNULL(SUM(PAID_AMOUNT),0) FROM CLM_ACTIVITY_PAYMENT_BREAKDOWN WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID

SET @TOTAL_PAYMENT_BREAKDOWN = @TOTAL_PAYMENT_BREAKDOWN + ISNULL(@PAID_AMOUNT,0)


IF (@TOTAL_PAYMENT IS NULL OR @TOTAL_PAYMENT<1 OR @TOTAL_PAYMENT_BREAKDOWN>@TOTAL_PAYMENT)
	RETURN -1

SELECT     
 @PAYMENT_BREAKDOWN_ID=ISNULL(MAX(PAYMENT_BREAKDOWN_ID),0)+1    
FROM    
 CLM_ACTIVITY_PAYMENT_BREAKDOWN        
WHERE     
 CLAIM_ID=@CLAIM_ID AND    
 ACTIVITY_ID=@ACTIVITY_ID    
    
INSERT INTO     
 CLM_ACTIVITY_PAYMENT_BREAKDOWN        
 (    
  CLAIM_ID,    
  ACTIVITY_ID,    
  PAYMENT_BREAKDOWN_ID,    
  TRANSACTION_CODE,    
  COVERAGE_ID,    
  PAID_AMOUNT,    
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME    
 )    
VALUES    
 (    
  @CLAIM_ID,    
  @ACTIVITY_ID,    
  @PAYMENT_BREAKDOWN_ID,    
  @TRANSACTION_CODE,    
  @COVERAGE_ID,    
  @PAID_AMOUNT,    
  'Y',    
  @CREATED_BY,    
  @CREATED_DATETIME     
 )    
     
END        
  



GO

