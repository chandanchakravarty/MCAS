IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimActivitySummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimActivitySummary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*            
Proc Name       : dbo.Proc_GetClaimActivitySummary                
Created by      : Sumit Chhabra                
Date            : 07/03/2006                
Purpose        : Obtain activity summary                
Revison History :                
Used In  : Wolverine                
------------------------------------------------------------                
MODIFIED BY   : Asfa Praveen                  
Date    : 18-Sept-2007                  
Purpose   : Modify Activities result                  
------------------------------------------------------------                        
Date     Review By          Comments                
------   ------------       -------------------------*/                
--DROP PROC dbo.Proc_GetClaimActivitySummary                 
CREATE PROC [dbo].[Proc_GetClaimActivitySummary]                
(                
 @CLAIM_ID             int     ,
 @POLICY_CURRENCY      int   =1        
)                
AS                
BEGIN                
              
--DECLARE @OUTSTANDING DECIMAL(20,2)                
--DECLARE @REINSURANCE_OUTSTANDING DECIMAL(20,2)                
--DECLARE @PAID_LOSS DECIMAL(20,2)                
--DECLARE @PAID_EXPENSE DECIMAL(20,2)                
--DECLARE @RECOVERIES DECIMAL(20,2)                
--DECLARE @COMPLETED_ACTIVITY INT                
--DECLARE @VOIDED_ACTIVITY INT               
                
/*                
Summary row should not sum the values of New Reserve Activity Estimation Recovery, Expense Col. for this                
we will declare two variables and minus the amount from the summary row of both the col.                
*/                
--DECLARE @CLAIM_RESERVE_AMOUNT DECIMAL(20,2)                 
--DECLARE @TEMP_EST_RECOVERY_AMOUNT DECIMAL(20,2)                
--DECLARE @TEMP_EST_EXPENSE_AMOUNT DECIMAL(20,2)                
                
--SET @TEMP_EST_RECOVERY_AMOUNT = 0                
--SET @COMPLETED_ACTIVITY = 11801             
--SET @VOIDED_ACTIVITY = 11986               
--SET @TEMP_EST_EXPENSE_AMOUNT = 0                
              
-- Commented by Asfa (18-Sept-2007) in order to correct summary of Activity as per mail sent by Gagan              
/*                
                
SELECT @TEMP_EST_RECOVERY_AMOUNT = ISNULL(SUM([RECOVERY]),0), @TEMP_EST_EXPENSE_AMOUNT = ISNULL(SUM(EXPENSES),0) FROM CLM_ACTIVITY                
WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_REASON <> 11836 and IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)                
              
                
SELECT top 1 @OUTSTANDING = isnull(CLAIM_RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID                 
  and IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)            
  order by created_datetime desc                
              
 SELECT @OUTSTANDING AS OUTSTANDING,ISNULL(SUM(PAYMENT_AMOUNT),0) - ISNULL(@TEMP_EST_RECOVERY_AMOUNT,0) AS PAID,                 
--(ISNULL(SUM(RECOVERY),0)- @TEMP_EST_RECOVERY_AMOUNT) AS [RECOVERY],                
--ISNULL(SUM(RECOVERY),0) AS [RECOVERY],                
@TEMP_EST_RECOVERY_AMOUNT AS [RECOVERY],                
 (@OUTSTANDING + ISNULL(SUM(PAYMENT_AMOUNT),0) - ISNULL(@TEMP_EST_RECOVERY_AMOUNT,0) ) AS INCURRED,                
--(ISNULL(SUM(EXPENSES),0) - @TEMP_EST_EXPENSE_AMOUNT ) AS  EXPENSE,                
--ISNULL(SUM(EXPENSES),0) AS  EXPENSE,                
-@TEMP_EST_EXPENSE_AMOUNT AS EXPENSE,                
(ISNULL(SUM(RI_RESERVE),0) - ISNULL(SUM(RECOVERY),0)) AS REINSURANCE_OUTSTANDING,                
'' AS  LOSS_REINSURANCE_RECOVERED,                
 '' AS EXPENSE_REINSURANCE_RECOVERED                
  FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID and IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)            
*/              
              
--SELECT @OUTSTANDING = ISNULL(SUM(RESERVE_AMOUNT),0) - ISNULL(SUM(PAYMENT_AMOUNT),0),            
--@PAID_LOSS = ISNULL(SUM(PAYMENT_AMOUNT),0),            
--@PAID_EXPENSE = ISNULL(SUM(EXPENSES),0),            
--@RECOVERIES = ISNULL(SUM([RECOVERY]),0)            
--FROM CLM_ACTIVITY WITH (NOLOCK)              
--WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR         
--ACTIVITY_STATUS=@VOIDED_ACTIVITY)            
             
--SELECT TOP 1 @CLAIM_RESERVE_AMOUNT = ISNULL(CLAIM_RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WITH (NOLOCK)         
--WHERE CLAIM_ID=@CLAIM_ID                 
--  AND IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)            
--  ORDER BY CREATED_DATETIME DESC              
              
----SELECT TOP 1 @REINSURANCE_OUTSTANDING = ISNULL(CLAIM_RI_RESERVE,0) FROM CLM_ACTIVITY WITH (NOLOCK)         
----WHERE CLAIM_ID=@CLAIM_ID                 
----  AND IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)            
----  ORDER BY CREATED_DATETIME DESC         
----Code Commented And Added For Itrack Issue #6144.        
--SELECT @REINSURANCE_OUTSTANDING = ISNULL(SUM(RI_RESERVE),0)         
--FROM CLM_ACTIVITY WITH (NOLOCK)              
--WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' AND (ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR         
--ACTIVITY_STATUS=@VOIDED_ACTIVITY)        
             
              
--SELECT @OUTSTANDING AS OUTSTANDING,    
--ISNULL(SUM(PAYMENT_AMOUNT),0) AS PAID,                 
--ISNULL(SUM([RECOVERY]),0) AS [RECOVERY],                
--@OUTSTANDING + ISNULL(SUM(PAYMENT_AMOUNT),0) + ISNULL(SUM([RECOVERY]),0) AS INCURRED,            
----@CLAIM_RESERVE_AMOUNT + ISNULL(SUM(PAYMENT_AMOUNT),0) AS INCURRED,                
--ISNULL(SUM(EXPENSES),0) AS  EXPENSE,                
--@REINSURANCE_OUTSTANDING AS REINSURANCE_OUTSTANDING,                
--ISNULL(SUM(LOSS_REINSURANCE_RECOVERED),0) AS  LOSS_REINSURANCE_RECOVERED,                
--ISNULL(SUM(EXPENSE_REINSURANCE_RECOVERED),0) AS EXPENSE_REINSURANCE_RECOVERED                
--  FROM CLM_ACTIVITY WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID and IS_ACTIVE='Y' AND         
--(ACTIVITY_STATUS=@COMPLETED_ACTIVITY OR ACTIVITY_STATUS=@VOIDED_ACTIVITY)            

SELECT TOP 1 dbo.fun_FormatCurrency(OUTSTANDING_RESERVE,@POLICY_CURRENCY) AS CLAIM_RESERVE_AMOUNT,    
dbo.fun_FormatCurrency((ISNULL((PAID_LOSS),0)+ISNULL((PAID_EXPENSE),0)) ,@POLICY_CURRENCY) AS CLAIM_PAYMENT_AMOUNT,
dbo.fun_FormatCurrency((ISNULL(B.PAYMENT_AMOUNT,0)+ ISNULL(B.EXPENSES,0)),@POLICY_CURRENCY) AS PAYMENT_AMOUNT,
dbo.fun_FormatCurrency(RESINSURANCE_RESERVE ,@POLICY_CURRENCY) AS CLAIM_RI_RESERVE,   
dbo.fun_FormatCurrency(ISNULL((B.RI_NET_PAID_RESERVE),0),@POLICY_CURRENCY) AS RI_PAID_RESERVE,
dbo.fun_FormatCurrency(A.CO_TOTAL_RESERVE_AMT ,@POLICY_CURRENCY) AS CO_TOTAL_RESERVE_AMT,
dbo.fun_FormatCurrency(B.COI_NET_PAID_RESERVE ,@POLICY_CURRENCY) AS CO_PAID_RESREVE
FROM CLM_CLAIM_INFO A WITH (NOLOCK) INNER JOIN
     CLM_ACTIVITY B on A.CLAIM_ID=B.CLAIM_ID     
WHERE A.CLAIM_ID=@CLAIM_ID  AND B.ACTIVITY_STATUS=11801 AND B.IS_ACTIVE='Y' 
ORDER BY  B.ACTIVITY_ID DESC     
   
--UPDATE CLM_CLAIM_INFO SET OUTSTANDING_RESERVE = @OUTSTANDING,       
--RESINSURANCE_RESERVE = @REINSURANCE_OUTSTANDING,             
--PAID_LOSS = @PAID_LOSS, PAID_EXPENSE = @PAID_EXPENSE, RECOVERIES = @RECOVERIES            
--WHERE CLAIM_ID=@CLAIM_ID           
select EXPENSES from   CLM_ACTIVITY
            
END        
        
        
        
GO

