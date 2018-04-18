IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLogSearchResults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLogSearchResults]
GO

CREATE Procedure [dbo].[Proc_GetLogSearchResults]      
(      
 @ClaimNo nvarchar(100)=null,      
 @ClaimRecordNo nvarchar(50)= null,      
 @LogRefNo nvarchar(15)=null,      
 @Hospital_Id int=0      
)              
As              
         
SET FMTONLY OFF;              
BEGIN       
      
Select distinct temp.AccidentClaimId,temp.LogId,temp.ClaimID,temp.SubClaimNo,temp.ClaimNo,temp.LogRefNo,temp.HospitalName,temp.LOGAmount,temp.IsVoid  
from   
 (           
  select acc.AccidentClaimId,clog.LogId,clm.ClaimID,clm.ClaimRecordNo as SubClaimNo,acc.ClaimNo,clog.LogRefNo,hosp.HospitalName,clog.LOGAmount,clog.IsVoid ,clog.Hospital_Id   
  from ClaimAccidentDetails acc (nolock)    
  join CLM_Claims clm (nolock) on clm.AccidentClaimId = acc.AccidentClaimId and clm.ClaimType=3    
  left join CLM_LogRequest clog (nolock) on clog.AccidentClaimId = acc.AccidentClaimId    
  and clog.ClaimID = clm.ClaimID    
  left join MNT_Hospital hosp (nolock) on hosp.Id = clog.Hospital_Id    
  where acc.IsComplete=2     
 )temp , CLM_Mandate man where temp.AccidentClaimId=man.AccidentId and temp.ClaimID=man.ClaimID and man.ClaimType=3   
        and man.ApproveRecommedations='Y' and (man.LogMedicalExpenses_S!=0.00 and man.LogMedicalExpenses_S is not null)       
        and(@ClaimNo IS NULL OR (temp.ClaimNo  like '%'+@ClaimNo+'%'))      
        and(@ClaimRecordNo IS NULL OR (temp.SubClaimNo like '%'+@ClaimRecordNo+'%'))      
        and(@LogRefNo IS NULL OR (temp.LogRefNo  like '%'+@LogRefNo+'%'))      
        and(@Hospital_Id=0 OR (temp.Hospital_Id=@Hospital_Id))      
      
END 



