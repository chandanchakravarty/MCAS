CREATE PROCEDURE [dbo].[Proc_GetCLM_PaymentListForDashBoard]  
WITH EXECUTE AS CALLER  
AS  
SET FMTONLY OFF;        
SELECT  
  ROW_NUMBER() over (order by payment.MandateId ) as SNo,  
  claim.AccidentClaimId,  
  claim.PolicyId,  
  LTRIM(RTRIM(acc.ClaimNo+'/'+ claim.ClaimRecordNo+'/'+ ISNULL(payment.MandateRecord,'')+'/'+payment.PaymentRecordNo)) as ClaimRecordNo,  
  claim.ClaimantName,  
  claim.ClaimType,  
  payment.Createddate,  
  payment.Modifieddate,  
  payment.TotalPaymentDue as [Total_D],  
  payment.Createdby,  
  payment.Modifiedby,  
  payment.PaymentId,  
  payment.ClaimID,
  CONVERT(INT, ISNULL(payment.AssignedTo,0)) as [AssignedTo],
  CONVERT(VARCHAR(10),payment.ApprovedDate,103) as [ApprovedDate],
  lk.Description AS ClaimTypeDesc,  
  lk.Lookupdesc AS ClaimTypeCode,  
  CASE  
    WHEN acc.IsComplete = 2 THEN 'Adj'  
    ELSE ''  
  END AS mode,  
  acc.ClaimNo  
FROM CLM_PaymentSummary payment INNER JOIN CLM_Claims claim ON payment.ClaimID = claim.ClaimID and claim.ClaimantName is not null  LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = payment.ClaimType AND lk.Category = 'ClaimType' INNER JOIN ClaimAccidentDetails acc ON acc.AccidentClaimId = payment.AccidentClaimId order by claim.ClaimType
GO


