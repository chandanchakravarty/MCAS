CREATE PROCEDURE [dbo].[Proc_GetPaymentsListByAccId] (@AccidentClaimId int)
AS
BEGIN
  SELECT
    ROW_NUMBER() OVER (PARTITION BY clmc.ClaimType ORDER BY clmps.PaymentId) RecordNumber,
    clmm.MandateId,
    clmc.PolicyId,
    lk.[Description] AS ClaimTypeDesc,
    lk.Lookupdesc AS ClaimTypeCode,
    clmps.PaymentId,
    clmps.ReserveId,
    clmps.TotalPaymentDue,
    clmc.ClaimID,
    clmc.AccidentClaimId,
    clmm.MandateRecordNo,
    clmm.ApproveRecommedations,
    clmps.PaymentRecordNo,
    clmm.MandateRecordNo + '-' + CONVERT(nvarchar(20), clmc.ClaimType) + '-' + clmc.ClaimRecordNo AS uniq,
    clmc.ClaimType,
    clmc.ClaimantName,
    clmc.ClaimRecordNo,
    clmps.ApprovePayment,
    clmps.ApprovedDate,
    CASE
      WHEN
        clmps.Payee LIKE 'C%' THEN clmc.ClaimantName
      ELSE CASE
          WHEN (SELECT
              PartyTypeId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
            = 1 THEN (SELECT
              CedantName
            FROM MNT_Cedant
            WHERE MNT_Cedant.CedantId = (SELECT
              CompanyNameId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
          WHEN (SELECT
              PartyTypeId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
            = 2 THEN (SELECT
              AdjusterName
            FROM MNT_Adjusters
            WHERE MNT_Adjusters.AdjusterId = (SELECT
              CompanyNameId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
          WHEN (SELECT
              PartyTypeId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
            = 3 THEN (SELECT
              AdjusterName
            FROM MNT_Adjusters
            WHERE MNT_Adjusters.AdjusterId = (SELECT
              CompanyNameId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
          WHEN (SELECT
              PartyTypeId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
            = 4 THEN (SELECT
              CompanyName
            FROM MNT_DepotMaster
            WHERE MNT_DepotMaster.DepotId = (SELECT
              CompanyNameId
            FROM CLM_ServiceProvider
            WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
          ELSE ''
        END
    END
    AS PayeeName,
    clmc.ClaimantStatus
  FROM CLM_Claims AS clmc
  LEFT JOIN CLM_MandateSummary AS clmm
    ON clmc.ClaimID = clmm.ClaimID
  LEFT JOIN CLM_PaymentSummary AS clmps
    ON clmc.ClaimID = clmps.ClaimID
    AND clmm.PaymentId = clmps.PaymentId
    AND clmm.MovementType = 'P'
  --ON clmm.PaymentId = clmps.PaymentId     
  LEFT JOIN MNT_Lookups AS lk
    ON lk.Lookupvalue = clmc.ClaimType
    AND lk.Category = 'ClaimType'
  WHERE clmc.AccidentClaimId = @AccidentClaimId
  AND clmm.MovementType = 'P'
  AND clmm.PaymentId IS NOT NULL

  UNION ALL

  SELECT
    ROW_NUMBER() OVER (PARTITION BY clmc.ClaimType ORDER BY clmps.PaymentId) RecordNumber,
    clmm.MandateId,
    clmc.PolicyId,
    lk.[Description] AS ClaimTypeDesc,
    lk.Lookupdesc AS ClaimTypeCode,
    clmps.PaymentId,
    clmps.ReserveId,
    clmps.TotalPaymentDue,
    clmc.ClaimID,
    clmc.AccidentClaimId,
    clmm.MandateRecordNo,
    clmm.ApproveRecommedations,
    clmps.PaymentRecordNo,
    clmm.MandateRecordNo + '-' + CONVERT(nvarchar(20), clmc.ClaimType) + '-' + clmc.ClaimRecordNo AS uniq,
    clmc.ClaimType,
    clmc.ClaimantName,
    clmc.ClaimRecordNo,
    clmps.ApprovePayment,
    clmps.ApprovedDate,
    CASE
      WHEN
        clmps.Payee LIKE 'C%' THEN clmc.ClaimantName
      ELSE CASE
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 1 THEN (SELECT
          CedantName
        FROM MNT_Cedant
        WHERE MNT_Cedant.CedantId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 2 THEN (SELECT
          AdjusterName
        FROM MNT_Adjusters
        WHERE MNT_Adjusters.AdjusterId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 3 THEN (SELECT
          AdjusterName
        FROM MNT_Adjusters
        WHERE MNT_Adjusters.AdjusterId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 4 THEN (SELECT
          CompanyName
        FROM MNT_DepotMaster
        WHERE MNT_DepotMaster.DepotId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      ELSE ''
    END 
    End AS PayeeName,
    clmc.ClaimantStatus
  FROM CLM_Claims AS clmc
  LEFT JOIN CLM_MandateSummary AS clmm
    ON clmc.ClaimID = clmm.ClaimID
  LEFT JOIN CLM_PaymentSummary AS clmps
    ON clmm.ClaimID = clmps.ClaimID
    AND clmm.MovementType = 'I'
    AND clmm.PaymentId = clmps.PaymentId
  LEFT JOIN MNT_Lookups AS lk
    ON lk.Lookupvalue = clmc.ClaimType
    AND lk.Category = 'ClaimType'
  WHERE clmc.AccidentClaimId = @AccidentClaimId
  AND clmm.MovementType = 'I'
  --AND clmm.PaymentId IS NOT NULL    

  UNION

  SELECT
    ROW_NUMBER() OVER (PARTITION BY clmc.ClaimType ORDER BY clmps.PaymentId) RecordNumber,
    clmps.MandateId,
    clmc.PolicyId,
    lk.Description AS ClaimTypeDesc,
    lk.Lookupdesc AS ClaimTypeCode,
    clmps.PaymentId,
    clmps.ReserveId,
    clmps.TotalPaymentDue,
    clmc.ClaimID,
    clmc.AccidentClaimId,
    clmm.MandateRecordNo,
    clmm.ApproveRecommedations,
    clmps.PaymentRecordNo,
    clmm.MandateRecordNo + '-' + CONVERT(nvarchar(20), clmc.ClaimType) + '-' + clmc.ClaimRecordNo AS uniq,
    clmc.ClaimType,
    clmc.ClaimantName,
    clmc.ClaimRecordNo,
    clmps.ApprovePayment,
    clmps.ApprovedDate,
    CASE
      WHEN
        clmps.Payee LIKE 'C%' THEN clmc.ClaimantName
      ELSE CASE
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 1 THEN (SELECT
          CedantName
        FROM MNT_Cedant
        WHERE MNT_Cedant.CedantId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 2 THEN (SELECT
          AdjusterName
        FROM MNT_Adjusters
        WHERE MNT_Adjusters.AdjusterId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 3 THEN (SELECT
          AdjusterName
        FROM MNT_Adjusters
        WHERE MNT_Adjusters.AdjusterId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      WHEN (SELECT
          PartyTypeId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee)))
        = 4 THEN (SELECT
          CompanyName
        FROM MNT_DepotMaster
        WHERE MNT_DepotMaster.DepotId = (SELECT
          CompanyNameId
        FROM CLM_ServiceProvider
        WHERE ServiceProviderId = SUBSTRING(clmps.Payee, 3, LEN(clmps.Payee))))
      ELSE ''
    END 
   End AS PayeeName,
    clmc.ClaimantStatus
  FROM CLM_Claims AS clmc
  LEFT JOIN CLM_PaymentSummary AS clmps
    ON clmc.ClaimID = clmps.ClaimID
  LEFT JOIN CLM_MandateSummary AS clmm
    ON clmc.ClaimID = clmm.ClaimID
    AND clmm.MandateId = clmps.MandateId
  LEFT JOIN MNT_Lookups AS lk
    ON lk.Lookupvalue = clmc.ClaimType
    AND lk.Category = 'ClaimType'
  WHERE clmc.AccidentClaimId = @AccidentClaimId
  AND (clmps.ApprovePayment = 'N'
  OR clmps.ApprovePayment IS NULL)
END
GO


