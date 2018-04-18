CREATE PROCEDURE [dbo].[Proc_GetClaimRecoveryList]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF; 
select * into #tmp  from (

SELECT distinct b.ClaimRecordNo ,b.ClaimantName,  
 ISNULL(b.ClaimType,0) AS ClaimTypeId,a.AccidentClaimId,a.PolicyId,ISNULL(c.RecoveryId,0) AS RecoveryId,b.ClaimID,m.PaymentId,MandateRecordNo,null as PaymentRecordNo,m.MandateId,null as ApprovedDate,null as TotalPaymentDue,c.TotalAmt_R,c.RecoverAmt,c.NetAmtRecovered  
 FROM   ClaimAccidentDetails a  
 LEFT OUTER JOIN CLM_Claims  b  on a.AccidentClaimId = b.AccidentClaimId  
 LEFT OUTER JOIN CLM_MandateSummary m on a.AccidentClaimId = m.AccidentClaimId and b.ClaimID = m.ClaimID and b.ClaimType = m.ClaimType  
 LEFT OUTER JOIN CLM_ClaimRecovery c on a.AccidentClaimId = c.AccidentClaimId and b.ClaimID = c.ClaimId   and m.MandateId = c.mandateid  
 WHERE a.AccidentClaimId = @AccidentId and m.ApproveRecommedations = 'Y' and (a.IsRecoveryBI = 'Y' OR IsRecoveryOD = 'Y') and b.ClaimType = 1 and  m.MovementType != 'P' and m.MandateId in (select MAX(mandateid) from CLM_MandateSummary ms join   
(select MandateRecordNo,AccidentClaimId,ClaimID,ClaimType from (  
select count(MandateId) countrecord,MAX(mandateid) mandateid,MandateRecordNo,AccidentClaimId,ClaimID,ClaimType from CLM_MandateSummary where AccidentClaimId = @AccidentId  
GROUP BY MandateRecordNo,AccidentClaimId,ClaimID,ClaimType) tbl where countrecord > 1) tbl1 on ms.AccidentClaimId = tbl1.AccidentClaimId and ms.ClaimID = tbl1.ClaimID and ms.ClaimType = tbl1.ClaimType and ms.MandateRecordNo = tbl1.MandateRecordNo  
group by ms.claimtype,ms.AccidentClaimId,ms.ClaimID)  
  
union all  
  
 SELECT distinct b.ClaimRecordNo ,b.ClaimantName,  
 ISNULL(b.ClaimType,0) AS ClaimTypeId,a.AccidentClaimId,a.PolicyId,ISNULL(c.RecoveryId,0) AS RecoveryId,b.ClaimID,p.PaymentId,MandateRecordNo,PaymentRecordNo,m.MandateId,p.ApprovedDate,p.TotalPaymentDue,c.TotalAmt_R,c.RecoverAmt,c.NetAmtRecovered  
 FROM   ClaimAccidentDetails a  
 LEFT OUTER JOIN CLM_Claims  b  on a.AccidentClaimId = b.AccidentClaimId  
 LEFT OUTER JOIN CLM_MandateSummary m on a.AccidentClaimId = m.AccidentClaimId and b.ClaimID = m.ClaimID and b.ClaimType = m.ClaimType    
 LEFT OUTER JOIN CLM_ClaimRecovery c on a.AccidentClaimId = c.AccidentClaimId and b.ClaimID = c.ClaimId  and m.MandateId = c.mandateid  
 LEFT OUTER JOIN CLM_PaymentSummary p on a.AccidentClaimId = p.AccidentClaimId and  b.ClaimId = p.ClaimID  and m.PaymentId = p.PaymentId    
 WHERE a.AccidentClaimId = @AccidentId and (a.IsRecoveryBI = 'Y' OR IsRecoveryOD = 'Y') and b.ClaimType = 1 and m.ApproveRecommedations != 'N' and  m.MovementType != 'P' and m.MandateRecordNo not in (select MandateRecordNo from (  
select count(MandateId) countrecord,MandateRecordNo from CLM_MandateSummary where AccidentClaimId = @AccidentId  and ClaimType = 1  and MovementType != 'P'
GROUP BY MandateRecordNo,AccidentClaimId,ClaimID,ClaimType) tbl where countrecord > 1)  

union all
	
SELECT distinct b.ClaimRecordNo ,b.ClaimantName,
	ISNULL(b.ClaimType,0) AS ClaimTypeId,a.AccidentClaimId,a.PolicyId,ISNULL(c.RecoveryId,0) AS RecoveryId,b.ClaimID,p.PaymentId,MandateRecordNo,PaymentRecordNo,m.MandateId,p.ApprovedDate,p.TotalPaymentDue,c.TotalAmt_R,c.RecoverAmt,c.NetAmtRecovered
	FROM   ClaimAccidentDetails a
	LEFT OUTER JOIN CLM_Claims  b  on a.AccidentClaimId = b.AccidentClaimId
	LEFT OUTER JOIN CLM_MandateSummary m on a.AccidentClaimId = m.AccidentClaimId and b.ClaimID = m.ClaimID and b.ClaimType = m.ClaimType 
	LEFT OUTER JOIN CLM_PaymentSummary p on m.AccidentClaimId = p.AccidentClaimId and  m.ClaimId = p.ClaimID  and p.PaymentId = m.PaymentId  
	LEFT OUTER JOIN CLM_ClaimRecovery c on p.AccidentClaimId = c.AccidentClaimId and p.ClaimID = c.ClaimId and p.PaymentId = c.PaymentId   
	WHERE a.AccidentClaimId = @AccidentId and p.ApprovePayment = 'Y' and (a.IsRecoveryBI = 'Y' OR a.IsRecoveryOD = 'Y') and b.ClaimType = 3 and m.MandateRecordNo in --(select mandateid from CLM_MandateSummary ms join 
(select MandateRecordNo from (
select count(MandateId) countrecord,MAX(mandateid) mandateid,MandateRecordNo,AccidentClaimId,ClaimID,ClaimType from CLM_MandateSummary where AccidentClaimId = @AccidentId
GROUP BY MandateRecordNo,AccidentClaimId,ClaimID,ClaimType) tbl where countrecord > 1) --tbl1 
--on ms.AccidentClaimId = tbl1.AccidentClaimId and ms.ClaimID = tbl1.ClaimID and ms.ClaimType = tbl1.ClaimType and ms.MandateRecordNo = tbl1.MandateRecordNo
--group by ms.claimtype,ms.AccidentClaimId,ms.ClaimID)

Union All
	
 SELECT distinct b.ClaimRecordNo ,b.ClaimantName,
	ISNULL(b.ClaimType,0) AS ClaimTypeId,a.AccidentClaimId,a.PolicyId,ISNULL(c.RecoveryId,0) AS RecoveryId,b.ClaimID,p.PaymentId,MandateRecordNo,PaymentRecordNo,m.MandateId,p.ApprovedDate,p.TotalPaymentDue,c.TotalAmt_R,c.RecoverAmt,c.NetAmtRecovered
	FROM   ClaimAccidentDetails a
	LEFT OUTER JOIN CLM_Claims  b  on a.AccidentClaimId = b.AccidentClaimId
	LEFT OUTER JOIN CLM_MandateSummary m on a.AccidentClaimId = m.AccidentClaimId and b.ClaimID = m.ClaimID and b.ClaimType = m.ClaimType 
	LEFT OUTER JOIN CLM_PaymentSummary p on a.AccidentClaimId = p.AccidentClaimId and  b.ClaimId = p.ClaimID  and p.PaymentId = m.PaymentId  
	LEFT OUTER JOIN CLM_ClaimRecovery c on a.AccidentClaimId = c.AccidentClaimId and b.ClaimID = c.ClaimId  and p.PaymentId = c.PaymentId    
	WHERE a.AccidentClaimId = @AccidentId and p.ApprovePayment = 'Y' and (a.IsRecoveryBI = 'Y' OR IsRecoveryOD = 'Y') and b.ClaimType = 3 and m.MandateRecordNo not in (select MandateRecordNo from (
select count(MandateId) countrecord,MandateRecordNo from CLM_MandateSummary where AccidentClaimId = @AccidentId  and ClaimType = 3
GROUP BY MandateRecordNo,AccidentClaimId,ClaimID,ClaimType) tbl where countrecord > 1)

) tbl1
	
	
	select ROW_NUMBER() OVER(PARTITION BY T.ClaimTypeId ORDER BY RecoveryId) RecordNumber, * into #TMP1 from #TMP	T
	
	 SELECT  *,lk.Description AS ClaimTypeDesc,lk.Lookupdesc AS ClaimTypeCode 
	 FROM #TMP1 Recoverylst (NOLOCK) 
	 LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = Recoverylst.ClaimTypeId AND lk.Category = 'ClaimType' 
	 WHERE AccidentClaimId = @AccidentId ORDER BY ClaimTypeId,ApprovedDate 
END


