IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ClaimListClaimId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ClaimListClaimId]
GO

CREATE Proc [dbo].[Proc_GetCLM_ClaimListClaimId]
(
@ClaimID nvarchar(100)
)
as
BEGIN
SET FMTONLY OFF;
SELECT * FROM [CLM_Claims] where [ClaimID]=@ClaimID
END

GO


