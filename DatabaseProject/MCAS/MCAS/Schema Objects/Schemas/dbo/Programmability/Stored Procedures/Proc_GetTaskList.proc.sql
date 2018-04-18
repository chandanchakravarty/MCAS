CREATE PROCEDURE [dbo].[Proc_GetTaskList]
	@AccidentClaimId [int]
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
SELECT Id,TaskNo,ActionDue,CloseDate,ct.TaskModifiedDate,PromtDetails,  
ct.AccidentClaimId,ct.ClaimID ,mu.UserDispName as ClaimOfficer,replace(cc.ClaimRecordNo,'-','')  +'/'+ cc.ClaimantName as ClaimantNames  
FROM CLM_ClaimTask ct
left outer join MNT_Users mu on mu.SNo=ct.ClaimsOfficer
left outer join CLM_Claims cc on  ct.AccidentClaimId = cc.AccidentClaimId and ct.ClaimID = cc.ClaimID
where  ct.AccidentClaimId = @AccidentClaimId  
END


