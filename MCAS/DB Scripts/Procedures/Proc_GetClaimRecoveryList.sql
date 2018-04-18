-- =============================================
-- Create basic stored procedure template
-- =============================================

-- Drop stored procedure if it already exists
IF EXISTS (
  SELECT * 
    FROM INFORMATION_SCHEMA.ROUTINES 
   WHERE SPECIFIC_SCHEMA = N'dbo'
     AND SPECIFIC_NAME = N'Proc_GetClaimRecoveryList' 
)
   DROP PROCEDURE dbo.Proc_GetClaimRecoveryList
GO

CREATE PROCEDURE dbo.Proc_GetClaimRecoveryList
(  
@AccidentId nvarchar(100)  
) 
AS
BEGIN  
SET FMTONLY OFF; 
	SELECT * INTO #TMP1	FROM ( SELECT ROW_NUMBER() OVER(PARTITION BY b.ClaimType ORDER BY RecoveryId) RecordNumber,b.ClaimRecordNo ,b.ClaimantName,
	ISNULL(b.ClaimType,0) AS ClaimTypeId,a.AccidentClaimId,a.PolicyId,ISNULL(c.RecoveryId,0) AS RecoveryId,b.ClaimID,p.PaymentId
	FROM   ClaimAccidentDetails a
	LEFT OUTER JOIN CLM_Claims  b  on a.AccidentClaimId = b.AccidentClaimId
	LEFT OUTER JOIN CLM_ClaimRecovery c on a.AccidentClaimId = c.AccidentClaimId and b.ClaimID = c.ClaimentId  
	LEFT OUTER JOIN CLM_Payment p on a.AccidentClaimId = p.AccidentClaimId and  b.ClaimId = p.ClaimID      
	WHERE a.AccidentClaimId = @AccidentId and p.ApprovePayment = 'Y' and (a.IsRecoveryBI = 'Y' OR IsRecoveryOD = 'Y')) TBL1  
	
	 SELECT *,lk.Description AS ClaimTypeDesc,lk.Lookupdesc AS ClaimTypeCode 
	 FROM #TMP1 Recoverylst (NOLOCK) 
	 LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = Recoverylst.ClaimTypeId AND lk.Category = 'ClaimType' 
	 WHERE AccidentClaimId = @AccidentId ORDER BY ClaimTypeId 
END

