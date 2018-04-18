CREATE PROCEDURE [dbo].[Proc_GetCLM_Claim_ClamiantName]
	@w_AccidentId [int],
	@w_ClaimType [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
select ClaimID,ClaimantName from dbo.CLM_Claims where AccidentClaimId=@w_AccidentId and ClaimType=@w_ClaimType
END


