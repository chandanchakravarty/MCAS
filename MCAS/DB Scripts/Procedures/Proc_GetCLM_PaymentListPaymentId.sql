---Proc_GetCLM_PaymentListPaymentId
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_PaymentListPaymentId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_PaymentListPaymentId]
GO

CREATE Proc [dbo].[Proc_GetCLM_PaymentListPaymentId]  
(  
@PaymentId int
)  
as  
BEGIN  
SET FMTONLY OFF;  
SELECT * FROM [CLM_Payment] where [PaymentId]=@PaymentId
END  
GO


