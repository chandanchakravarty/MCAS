IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Proc_GetCLM_Claim_ClamiantName')AND type in (N'P', N'PC'))
DROP PROCEDURE Proc_GetCLM_Claim_ClamiantName
GO

CREATE PROCEDURE Proc_GetCLM_Claim_ClamiantName
@w_AccidentId int ,
@w_ClaimType int
AS
SET FMTONLY OFF;
BEGIN
select ClaimID,ClaimantName from dbo.CLM_Claims where AccidentClaimId=@w_AccidentId and ClaimType=@w_ClaimType
END

