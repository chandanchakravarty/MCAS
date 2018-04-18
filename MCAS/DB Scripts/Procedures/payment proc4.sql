/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentListAll]    Script Date: 07/15/2014 18:47:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPaymentListAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPaymentListAll]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






create Proc [dbo].[Proc_GetPaymentListAll]
(
@ClaimID int
)
as
BEGIN
select m1.PaymentId,m2.CompanyName,m1.CreatedDate,m1.ClaimantID,m1.PayeeType,m1.PaymentType,m1.Payee,m1.PaymentDueDate,m1.PayableOrgAmt,m1.PayableLocalAmt,m1.PreReserveOrgAmt,
m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt,m1.FinalReserveLocalAmt from dbo.CLM_ClaimPayment m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID desc,m1.PaymentId desc

END
GO


