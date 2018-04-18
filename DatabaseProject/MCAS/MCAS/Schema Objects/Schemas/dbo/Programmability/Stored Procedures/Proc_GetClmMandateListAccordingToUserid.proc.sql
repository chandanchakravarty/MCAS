CREATE PROCEDURE [dbo].[Proc_GetClmMandateListAccordingToUserid]      
WITH EXECUTE AS CALLER      
AS      
SET FMTONLY OFF;            
SELECT      
  ROW_NUMBER() over (order by mandate.MandateId ) as SNo,      
  claim.AccidentClaimId, 
  claim.PolicyId,      
  LTRIM(RTRIM(acc.ClaimNo+'/'+ claim.ClaimRecordNo+'/'+mandate.MandateRecordNo)) as ClaimRecordNo,      
  claim.ClaimantName,      
  claim.ClaimType,
  mandate.Createddate,      
  mandate.Modifieddate,      
  mandate.CurrentMandate as [Total_C],      
  mandate.Createdby,      
  mandate.Modifiedby,      
  mandate.MandateId,      
  mandate.ClaimID,      
  lk.Description AS ClaimTypeDesc,      
  lk.Lookupdesc AS ClaimTypeCode,      
  CASE      
    WHEN acc.IsComplete = 2 THEN 'Adj'      
    ELSE ''      
  END AS mode,      
  acc.ClaimNo     
FROM CLM_MandateSummary mandate
INNER JOIN 
CLM_Claims claim 
ON mandate.ClaimID = claim.ClaimID
and 
claim.ClaimantName is not null 
LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = mandate.ClaimType AND lk.Category = 'ClaimType' 
INNER JOIN ClaimAccidentDetails acc
ON acc.AccidentClaimId = mandate.AccidentClaimId order by claim.ClaimType  
GO


