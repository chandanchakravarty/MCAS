IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEscallationListAccordingTouserId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEscallationListAccordingTouserId]
GO

CREATE Procedure [dbo].[Proc_GetEscallationListAccordingTouserId]
(
 @UserId nvarchar(100)
)
As
SET FMTONLY OFF;
SELECT x.* FROM TODODIARYLIST x inner join MNT_Users y on x.EscalationTo = CAST( y.SNo AS nvarchar) inner join ClaimAccidentDetails c on c.AccidentClaimId=x.AccidentId where y.UserId=@UserId and y.IsActive='Y'

GO





