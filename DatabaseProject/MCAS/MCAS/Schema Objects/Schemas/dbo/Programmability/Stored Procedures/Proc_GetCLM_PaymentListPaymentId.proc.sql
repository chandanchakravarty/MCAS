CREATE PROCEDURE [dbo].[Proc_GetCLM_PaymentListPaymentId]
	@PaymentId [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
SELECT * FROM [CLM_Payment] where [PaymentId]=@PaymentId
END


