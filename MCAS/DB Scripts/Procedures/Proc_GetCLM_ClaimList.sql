IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ClaimList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ClaimList]
GO

CREATE Proc [dbo].[Proc_GetCLM_ClaimList]
(
@AccidentId nvarchar(100)
)
as
BEGIN
SET FMTONLY OFF;
select row_number() over(partition by ClaimType order by ClaimID) as RecordNumber,ClaimRecordNo,PolicyId,AccidentClaimId,ClaimID,ClaimantName,
ClaimType , lk.Description as ClaimTypeDesc,lk.Lookupdesc  as ClaimTypeCode
from CLM_Claims claim (nolock)
left join MNT_Lookups lk (nolock) on lk.lookupvalue=claim.ClaimType and lk.Category ='ClaimType' 
where AccidentClaimId=@AccidentId and claim.IsActive='Y' order by ClaimType
END


GO


