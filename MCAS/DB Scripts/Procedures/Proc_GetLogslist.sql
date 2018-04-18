/****** Object:  StoredProcedure [dbo].[Proc_GetLogslist]    Script Date: 02/24/2015 14:03:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLogslist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLogslist]
GO
CREATE Procedure [dbo].[Proc_GetLogslist]  
As  
  
SET FMTONLY OFF;  
BEGIN      
  Select distinct temp.AccidentClaimId,temp.LogId,temp.ClaimID,temp.SubClaimNo,temp.ClaimNo,temp.LogRefNo,temp.HospitalName,temp.LOGAmount,temp.IsVoid 
from 
 (         
  select acc.AccidentClaimId,clog.LogId,clm.ClaimID,clm.ClaimRecordNo as SubClaimNo,acc.ClaimNo,clog.LogRefNo,hosp.HospitalName,clog.LOGAmount,clog.IsVoid  
  from ClaimAccidentDetails acc (nolock)  
  join CLM_Claims clm (nolock) on clm.AccidentClaimId = acc.AccidentClaimId and clm.ClaimType=3  
  left join CLM_LogRequest clog (nolock) on clog.AccidentClaimId = acc.AccidentClaimId  
  and clog.ClaimID = clm.ClaimID  
  left join MNT_Hospital hosp (nolock) on hosp.Id = clog.Hospital_Id  
  where acc.IsComplete=2   
 )temp , CLM_Mandate man where temp.AccidentClaimId=man.AccidentId and temp.ClaimID=man.ClaimID 
                               and man.ClaimType=3 and man.ApproveRecommedations='Y'
                               and (man.LogMedicalExpenses_S!=0.00 and man.LogMedicalExpenses_S is not null)   
END 
GO


