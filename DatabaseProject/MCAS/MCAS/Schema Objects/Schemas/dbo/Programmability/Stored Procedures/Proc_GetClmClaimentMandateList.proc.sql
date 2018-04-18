  
      
      
      
-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================      
--[Proc_GetClmClaimentMandateList] 125
CREATE PROCEDURE [dbo].[Proc_GetClmClaimentMandateList]
@AccidentId [nvarchar](100)        
WITH EXECUTE AS CALLER        
AS        
SET FMTONLY OFF;          
IF OBJECT_ID('tempdb..#TMP') IS NOT NULL DROP TABLE #TMP              
  IF OBJECT_ID('tempdb..#TMP1') IS NOT NULL DROP TABLE #TMP1              
SET FMTONLY OFF;         
      
SELECT * INTO #TMP FROM (SELECT COUNT(*) Counts, MAX(MandateId) MandateId, ClaimType,ClaimID,AccidentClaimId   
FROM CLM_MandateSummary GROUP BY ClaimType, ClaimID,AccidentClaimId,MandateRecordNo) TBL             
        
SELECT * INTO #TMP1 FROM (SELECT ROW_NUMBER() OVER (PARTITION BY U.ClaimType ORDER BY U.MandateId) RecordNumber, Counts,        
(SELECT ClaimRecordNo FROM CLM_Claims WHERE ClaimID = t.ClaimID) AS ClaimRecordNo, U.*         
FROM #TMP T   
LEFT JOIN CLM_MandateSummary U ON U.ClaimType = T.ClaimType AND U.ClaimID = T.ClaimID AND U.MandateId = T.MandateId
WHERE U.AccidentClaimId = @AccidentId) TBL1         
        
        
ALTER TABLE #TMP1 Drop COLUMN IsActive             
        
select CLM_Claims.AccidentClaimId,CLM_Claims.ClaimID,CLM_Claims.ClaimantName,CLM_Claims.TPVehicleNo,CLM_Claims.ClaimType,CLM_Claims.PolicyId,CLM_Claims.ClaimRecordNo,  
 dbo.ufnGetReserveId(CLM_Claims.ClaimID) AS ReserveId, #TMP1.MandateId,lk.Description AS ClaimTypeDesc, lk.Lookupdesc AS ClaimTypeCode,#TMP1.RecordNumber,      
  
ApproveRecommedations = case when ApproveRecommedations='Y' Then 'Yes' when ApproveRecommedations='N'  Then 'No' End ,      
#TMP1.MandateRecordNo ,CLM_Claims.ClaimantStatus, #TMP1.AssignedTo            
from CLM_Claims left outer join #TMP1 on CLM_Claims.ClaimID=#TMP1.ClaimID LEFT  JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = CLM_Claims.ClaimType AND lk.Category = 'ClaimType' WHERE CLM_Claims.AccidentClaimId = @AccidentId ORDER BY ClaimType     
  