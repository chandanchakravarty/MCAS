CREATE PROCEDURE [dbo].[Proc_GetLogslist]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;          
BEGIN          
Select t1.AccidentClaimId,t1.MandateId,clog.LogId,t1.ClaimID,t1.MandateRecordNo,clm.ClaimRecordNo as SubClaimNo,    
acc.ClaimNo,upper(clog.LogRefNo)LogRefNo,hosp.HospitalName,t1.MovementMandateSP LOGAmount,clog.IsVoid           
             
  from     
   (    
     Select distinct man.AccidentClaimId,man.ClaimID,manDtls.MandateId,man.MandateRecordNo,manDtls.MovementMandateSP   
         from CLM_MandateSummary man ,CLM_MandateDetails manDtls    
         where(    
               manDtls.MovementMandateSP!=0.00 and manDtls.MovementMandateSP is not null and manDtls.MovementMandateSP>=0       
              )    
         and man.AccidentClaimId=manDtls.AccidentClaimId     
         and   man.ClaimID=manDtls.ClaimID    
         and   man.MandateId=manDtls.MandateId    
         and   man.ApproveRecommedations='Y'    
         and   man.ClaimType=3    
         and   manDtls.CmpCode='LME'    
    )t1 left outer join  CLM_Claims clm (nolock) on clm.AccidentClaimId = t1.AccidentClaimId and clm.ClaimID=t1.ClaimID  and clm.ClaimType=3       
        left outer join CLM_LogRequest clog (nolock) on clog.AccidentClaimId = t1.AccidentClaimId  and clog.MandateId=t1.MandateId    
        left outer join MNT_Hospital hosp (nolock) on hosp.Id = clog.Hospital_Id      
        left outer join ClaimAccidentDetails acc  on t1.AccidentClaimId=acc.AccidentClaimId          
     where acc.IsComplete=2                        
END


