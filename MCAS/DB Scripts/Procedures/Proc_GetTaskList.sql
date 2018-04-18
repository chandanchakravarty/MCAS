IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTaskList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTaskList]
GO

create Proc [dbo].[Proc_GetTaskList]
(
@AccidentClaimId int
)
as
BEGIN
SET FMTONLY OFF;
SELECT * FROM [dbo].[CLM_ClaimTask] where [AccidentClaimId] = @AccidentClaimId
END

GO


