CREATE PROCEDURE [dbo].[Proc_GetCLM_PaymentList_test]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
  IF OBJECT_ID('tempdb..#TMP') IS NOT NULL DROP TABLE #TMP
  IF OBJECT_ID('tempdb..#TMP1') IS NOT NULL DROP TABLE #TMP1
SET FMTONLY OFF;
SELECT * INTO #TMP FROM (SELECT COUNT(*) Counts, MAX(Createddate) DATE, ClaimType, ClaimID FROM CLM_Payment GROUP BY ClaimType, ClaimID) TBL

SELECT * INTO #TMP1 FROM (SELECT ROW_NUMBER() OVER (PARTITION BY U.ClaimType ORDER BY PaymentId) RecordNumber, Counts,(SELECT ClaimRecordNo FROM CLM_Claims WHERE ClaimID = t.ClaimID) AS ClaimRecordNo, U.* FROM #TMP T LEFT JOIN CLM_Payment U ON U.ClaimType = T.ClaimType AND U.ClaimID = T.ClaimID AND U.Createddate = T.DATE WHERE U.AccidentClaimId = @AccidentId) TBL1
ALTER TABLE #TMP1 Drop COLUMN IsActive
SELECT CASE  
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 1 THEN (SELECT CedantName FROM MNT_Cedant WHERE MNT_Cedant.CedantId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=Payee))  
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 2 THEN (SELECT AdjusterName FROM MNT_Adjusters WHERE MNT_Adjusters.AdjusterId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=Payee))  
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 3 THEN (SELECT AdjusterName FROM MNT_Adjusters WHERE MNT_Adjusters.AdjusterId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=Payee))  
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 4 THEN (SELECT CompanyName FROM MNT_DepotMaster WHERE MNT_DepotMaster.DepotId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=Payee))  
                   Else ''
                   END  as PayeeName
,CASE
When ApprovePayment='Y' Then 'Approved'
Else 'Not Approved'
END as ApprovePayment1, *,lk.Description AS ClaimTypeDesc, lk.Lookupdesc AS ClaimTypeCode FROM #TMP1 claim (NOLOCK) LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = claim.ClaimType AND lk.Category = 'ClaimType' WHERE AccidentClaimId = @AccidentId ORDER BY ClaimType
select * from #TMP1


