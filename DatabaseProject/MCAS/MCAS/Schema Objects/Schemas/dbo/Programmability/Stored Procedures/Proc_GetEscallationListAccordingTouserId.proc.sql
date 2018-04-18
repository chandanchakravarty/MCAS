CREATE PROCEDURE [dbo].[Proc_GetEscallationListAccordingTouserId]
	@UserId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
SELECT x.* FROM TODODIARYLIST x inner join MNT_Users y on x.EscalationTo = CAST( y.SNo AS nvarchar) inner join ClaimAccidentDetails c on c.AccidentClaimId=x.AccidentId where y.UserId=@UserId and y.IsActive='Y'


