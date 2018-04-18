IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_PaymentListForDashBoard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_PaymentListForDashBoard]
GO

CREATE PROC [dbo].[Proc_GetCLM_PaymentListForDashBoard]  
AS      
SET FMTONLY OFF;      
SELECT
  ROW_NUMBER() over (order by payment.MandateId ) as SNo,
  claim.AccidentClaimId,
  claim.PolicyId,
  claim.ClaimRecordNo,
  claim.ClaimantName,
  claim.ClaimType,
  payment.Createddate,
  payment.Modifieddate,
  payment.Total_D,
  payment.Createdby,
  payment.Modifiedby,
  payment.PaymentId,
  payment.ClaimID,
  lk.Description AS ClaimTypeDesc,
  lk.Lookupdesc AS ClaimTypeCode,
  CASE
    WHEN acc.IsComplete = 2 THEN 'Adj'
    ELSE ''
  END AS mode,
  acc.ClaimNo
FROM CLM_Payment payment INNER JOIN CLM_Claims claim ON payment.ClaimID = claim.ClaimID LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = payment.ClaimType AND lk.Category = 'ClaimType' INNER JOIN ClaimAccidentDetails acc ON acc.AccidentClaimId = payment.AccidentClaimId WHERE (payment.ApprovePayment = 'N' OR payment.ApprovePayment IS NULL) order by claim.ClaimType
GO  


