CREATE PROCEDURE [dbo].[Proc_UpdateClaimAccDetClaimNo]
	@p_ClaimNo [nvarchar](100),
	@w_AccidentClaimId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  

BEGIN
UPDATE [dbo].ClaimAccidentDetails SET ClaimNo=@p_ClaimNo 
	WHERE AccidentClaimId=@w_AccidentClaimId
END


