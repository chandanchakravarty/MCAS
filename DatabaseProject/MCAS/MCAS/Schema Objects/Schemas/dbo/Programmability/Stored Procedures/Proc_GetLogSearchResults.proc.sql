CREATE PROCEDURE [dbo].[Proc_GetLogSearchResults]
	@ClaimNo [nvarchar](100) = null,
	@ClaimRecordNo [nvarchar](50) = null,
	@LogRefNo [nvarchar](15) = null,
	@Hospital_Id [int] = 0
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;              
BEGIN                 
                
Select   temp.AccidentClaimId,temp.MandateId,temp.LogId,temp.ClaimID,temp.MandateRecordNo,temp.SubClaimNo,temp.ClaimNo,upper(temp.LogRefNo)LogRefNo,temp.HospitalName,temp.LOGAmount,temp.IsVoid             
from             
 (                     
  Select t1.AccidentClaimId,t1.MandateId,clog.LogId,t1.ClaimID,t1.MandateRecordNo,clm.ClaimRecordNo as SubClaimNo,    
  acc.ClaimNo,upper(clog.LogRefNo)LogRefNo,hosp.HospitalName,t1.MovementMandateSP LOGAmount,clog.IsVoid,clog.Hospital_Id                              
             
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
        inner join ClaimAccidentDetails acc  on t1.AccidentClaimId=acc.AccidentClaimId          
     where acc.IsComplete=2     
   )temp where        
            (@ClaimNo IS NULL OR (temp.ClaimNo  like '%'+@ClaimNo+'%'))                
        and(@ClaimRecordNo IS NULL OR (temp.SubClaimNo like '%'+@ClaimRecordNo+'%'))                
        and(@LogRefNo IS NULL OR (temp.LogRefNo  like '%'+@LogRefNo+'%'))                
        and(@Hospital_Id=0 OR (temp.Hospital_Id=@Hospital_Id))                
                
END 


