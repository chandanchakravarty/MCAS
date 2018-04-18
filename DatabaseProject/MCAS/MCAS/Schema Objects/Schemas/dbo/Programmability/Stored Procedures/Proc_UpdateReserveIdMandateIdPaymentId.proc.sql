CREATE PROCEDURE [dbo].[Proc_UpdateReserveIdMandateIdPaymentId]
	@ReserveId [int],
	@MandateId [int],
	@PaymentId [int],
	@ReserveId_P [int],
	@MandateId_P [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
SET NOCOUNT ON;  
UPDATE CLM_Reserve SET PaymentId=@PaymentId WHERE ReserveId=@ReserveId  
UPDATE CLM_Mandate SET ReserveId=@ReserveId,PaymentId=@PaymentId  WHERE MandateId=@MandateId  
UPDATE CLM_Payment SET ReserveId=@ReserveId_P,MandateId=@MandateId_P  WHERE PaymentId=@PaymentId  
UPDATE Clm_MandateTransactionHistory SET PaymentId = @PaymentId  WHERE MandateId=@MandateId  
END


