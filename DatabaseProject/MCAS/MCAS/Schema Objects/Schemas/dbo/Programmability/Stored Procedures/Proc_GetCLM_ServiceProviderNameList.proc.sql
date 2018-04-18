CREATE PROCEDURE [dbo].[Proc_GetCLM_ServiceProviderNameList] @AccidentId [nvarchar](100),
@claimId [nvarchar](100) WITH EXECUTE AS CALLER
AS
BEGIN
  SET FMTONLY OFF;
  SELECT
    CASE
      WHEN ServiceProviderId IS NULL THEN ''
      ELSE 'S' + '-' + CONVERT(varchar(10), ServiceProviderId)
    END
    AS ServiceProviderId,
    cedent_name =
                 CASE
                   WHEN ServiceProviderId IS not NULL and ServiceProviderId != '' THEN 
                   (CASE
                       WHEN PartyTypeId = 1 THEN (SELECT
                           CedantName
                         FROM MNT_Cedant
                         WHERE MNT_Cedant.CedantId = s.CompanyNameId)
                       WHEN PartyTypeId = 2 THEN (SELECT
                           AdjusterName
                         FROM MNT_Adjusters
                         WHERE MNT_Adjusters.AdjusterId = s.CompanyNameId)
                       WHEN PartyTypeId = 3 THEN (SELECT
                           AdjusterName
                         FROM MNT_Adjusters
                         WHERE MNT_Adjusters.AdjusterId = s.CompanyNameId)
                       WHEN PartyTypeId = 4 THEN (SELECT
                           CompanyName
                         FROM MNT_DepotMaster
                         WHERE MNT_DepotMaster.DepotId = s.CompanyNameId)
                     END)
                   ELSE ''
                 END,
    c.ClaimantName,
    'C' + '-' + CONVERT(varchar(10), c.ClaimID) AS ClaimID
  FROM CLM_Claims c
  LEFT JOIN CLM_ServiceProvider s
    ON c.ClaimID = s.ClaimantNameId
  WHERE c.ClaimID = @claimId
  AND c.AccidentClaimId = @AccidentId
END


GO


