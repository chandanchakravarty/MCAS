--Proc_GetCLM_ServiceProviderNameList
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ServiceProviderNameList]') AND type IN (N'P', N'PC'))DROP PROCEDURE [dbo].[Proc_GetCLM_ServiceProviderNameList]
GO

CREATE PROC [dbo].[Proc_GetCLM_ServiceProviderNameList] (
@AccidentId nvarchar(100),
@claimId nvarchar(100))
AS
BEGIN
  SET FMTONLY OFF;
  SELECT ServiceProviderId,
  cedent_name =    CASE
                   WHEN PartyTypeId = 1 THEN (SELECT CedantName FROM MNT_Cedant WHERE MNT_Cedant.CedantId = CLM_ServiceProvider.CompanyNameId)
                   WHEN PartyTypeId = 2 THEN (SELECT AdjusterName FROM MNT_Adjusters WHERE MNT_Adjusters.AdjusterId = CLM_ServiceProvider.CompanyNameId)
                   WHEN PartyTypeId = 3 THEN (SELECT AdjusterName FROM MNT_Adjusters WHERE MNT_Adjusters.AdjusterId = CLM_ServiceProvider.CompanyNameId)
                   WHEN PartyTypeId = 4 THEN (SELECT CompanyName FROM MNT_DepotMaster WHERE MNT_DepotMaster.DepotId = CLM_ServiceProvider.CompanyNameId)
                   END
  FROM CLM_ServiceProvider WHERE ClaimantNameId = @claimId AND AccidentId = @AccidentId
END


GO
