IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClmMandateListAccordingToUserid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClmMandateListAccordingToUserid]
GO

CREATE Proc [dbo].[Proc_GetClmMandateListAccordingToUserid]        
As     
SET FMTONLY OFF;      
SELECT
  ROW_NUMBER() over (order by mandate.MandateId ) as SNo,
  claim.AccidentClaimId,
  claim.PolicyId,
  claim.ClaimRecordNo,
  claim.ClaimantName,
  claim.ClaimType,
  mandate.Createddate,
  mandate.Modifieddate,
  mandate.Total_C,
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
FROM CLM_Mandate mandate INNER JOIN CLM_Claims claim ON mandate.ClaimID = claim.ClaimID LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = mandate.ClaimType AND lk.Category = 'ClaimType' INNER JOIN ClaimAccidentDetails acc ON acc.AccidentClaimId = mandate.AccidentId WHERE (mandate.ApproveRecommedations = 'N' OR mandate.ApproveRecommedations IS NULL) AND mandate.Modifiedby IN (SELECT userid FROM MNT_Users u INNER JOIN MNT_GroupsMaster g ON u.GroupId = g.GroupId AND g.GroupCode = 'CO') order by claim.ClaimType


    GO


