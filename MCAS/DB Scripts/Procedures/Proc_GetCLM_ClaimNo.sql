IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ClaimNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ClaimNo]
GO

CREATE Proc [dbo].[Proc_GetCLM_ClaimNo]  
(  
@ClaimType nvarchar(100),
@AccidentClaimId  nvarchar(100)
)  
as  
BEGIN  
SET FMTONLY OFF;  
select COUNT(*) as ClaimID from CLM_Claims  where ClaimType=@ClaimType and AccidentClaimId=@AccidentClaimId order by ClaimID desc  
END 

GO





