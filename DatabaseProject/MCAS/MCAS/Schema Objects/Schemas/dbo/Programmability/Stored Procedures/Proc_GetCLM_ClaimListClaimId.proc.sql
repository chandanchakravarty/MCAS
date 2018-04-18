CREATE PROCEDURE [dbo].[Proc_GetCLM_ClaimListClaimId]
	@ClaimID [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
SELECT * FROM [CLM_Claims] where [ClaimID]=@ClaimID
END


