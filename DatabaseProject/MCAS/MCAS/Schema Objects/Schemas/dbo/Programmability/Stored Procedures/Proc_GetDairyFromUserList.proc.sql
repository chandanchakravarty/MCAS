CREATE PROCEDURE [dbo].[Proc_GetDairyFromUserList]
	@UserId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;    
select  a.DairyFromUser,a.DairyId from Claim_ReAssignmentDairy a inner join MNT_Users u on a.ReAssignTo = u.SNo inner join ClaimAccidentDetails c on c.AccidentClaimId =a.AccidentClaimId where u.UserId=@UserId  
  
END


