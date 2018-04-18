CREATE PROCEDURE [dbo].[Proc_GetLogsDetails]
	@LogId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;        
BEGIN            
Select temp.AccidentClaimId,temp.LogId,temp.ClaimID,temp.SubClaimNo,temp.ClaimNo,temp.LogRefNo,temp.HospitalName,temp.LOGAmount,temp.IsVoid 
from 
 (         
  select acc.AccidentClaimId,clog.LogId,clm.ClaimID,clm.ClaimRecordNo as SubClaimNo,acc.ClaimNo,clog.LogRefNo,hosp.HospitalName,clog.LOGAmount,clog.IsVoid  
  from ClaimAccidentDetails acc (nolock)  
  join CLM_Claims clm (nolock) on clm.AccidentClaimId = acc.AccidentClaimId and clm.ClaimType=3  
  left join CLM_LogRequest clog (nolock) on clog.AccidentClaimId = acc.AccidentClaimId  
  and clog.ClaimID = clm.ClaimID  
  left join MNT_Hospital hosp (nolock) on hosp.Id = clog.Hospital_Id  
  where acc.IsComplete=2   
 )temp ,CLM_MandateDetails man , CLM_MandateSummary manSumry where temp.AccidentClaimId=manSumry.AccidentClaimId and temp.ClaimID=man.ClaimID       
                               and manSumry.ClaimType=3 and manSumry.ApproveRecommedations='Y'      
                               and (man.MovementMandateSP!=0.00 and man.MovementMandateSP is not null) 
                               and man.MandateId=manSumry.MandateId
                               and temp.LogId=@LogId 
END


