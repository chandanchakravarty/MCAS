CREATE PROCEDURE [dbo].[Proc_GetPaymentListAll]
	@ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN
select m1.PaymentId,m2.CompanyName,m1.CreatedDate,m1.ClaimantID,m1.PayeeType,m1.PaymentType,m1.Payee,m1.PaymentDueDate,m1.PayableOrgAmt,m1.PayableLocalAmt,m1.PreReserveOrgAmt,
m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt,m1.FinalReserveLocalAmt,m1.CreatedBy from dbo.CLM_ClaimPayment m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID desc,m1.PaymentId desc

END


