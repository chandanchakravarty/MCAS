CREATE PROCEDURE [dbo].[Proc_GetCLM_PaymentList]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;      
  IF OBJECT_ID('tempdb..#TMP') IS NOT NULL DROP TABLE #TMP      
  IF OBJECT_ID('tempdb..#TMP1') IS NOT NULL DROP TABLE #TMP1      
SET FMTONLY OFF;      
    
SELECT * INTO #TMP FROM (SELECT COUNT(*) Counts, MAX(Createddate) DATE, ClaimType, ClaimID,AccidentClaimId FROM CLM_Payment GROUP BY ClaimType, ClaimID,AccidentClaimId) TBL      
      
SELECT * INTO #TMP1 FROM (SELECT ROW_NUMBER() OVER (PARTITION BY U.ClaimType ORDER BY PaymentId) RecordNumber, Counts,(SELECT ClaimRecordNo FROM CLM_Claims WHERE ClaimID = t.ClaimID) AS ClaimRecordNo, U.* FROM #TMP T LEFT JOIN CLM_Payment U ON U.ClaimType
  
    
 = T.ClaimType AND U.ClaimID = T.ClaimID AND U.Createddate = T.DATE WHERE U.AccidentClaimId = @AccidentId) TBL1      
    
ALTER TABLE #TMP1 Drop COLUMN IsActive      
    
select CLM_Claims.AccidentClaimId,CLM_Claims.ClaimID,CLM_Claims.ClaimantName,CLM_Claims.ClaimType,CLM_Claims.PolicyId,CLM_Claims.ClaimRecordNo,#TMP1.PaymentId,#TMP1.Total_D,#TMP1.ApprovedDate,    
CASE        
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee) = 1 THEN (SELECT CedantName FROM MNT_Cedant     
                   WHERE MNT_Cedant.CedantId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee))        
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee) = 2 THEN (SELECT AdjusterName FROM MNT_Adjusters    
                    WHERE MNT_Adjusters.AdjusterId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee))        
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 3 THEN (SELECT AdjusterName FROM MNT_Adjusters    
                    WHERE MNT_Adjusters.AdjusterId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee))        
                   WHEN (select PartyTypeId from CLM_ServiceProvider where ServiceProviderId=Payee) = 4 THEN (SELECT CompanyName FROM MNT_DepotMaster    
                    WHERE MNT_DepotMaster.DepotId = (select CompanyNameId from CLM_ServiceProvider where ServiceProviderId=#TMP1.Payee))        
                   Else ''      
                   END  as PayeeName      
,CASE      
When #TMP1.ApprovePayment='Y' Then 'Approved'      
Else 'Not Approved'      
END as ApprovePayment,lk.Description AS ClaimTypeDesc, lk.Lookupdesc AS ClaimTypeCode,    
#TMP1.RecordNumber from CLM_Claims left outer join #TMP1 on CLM_Claims.ClaimID=#TMP1.ClaimID LEFT  JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = CLM_Claims.ClaimType AND lk.Category = 'ClaimType' WHERE CLM_Claims.AccidentClaimId = @AccidentId ORDER BY ClaimType


