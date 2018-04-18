CREATE PROCEDURE [dbo].[Proc_GetCLM_ClaimNo]
	@ClaimType [nvarchar](100),
	@AccidentClaimId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
select COUNT(*) as ClaimID from CLM_Claims  where ClaimType=@ClaimType and AccidentClaimId=@AccidentClaimId order by ClaimID desc  
END


