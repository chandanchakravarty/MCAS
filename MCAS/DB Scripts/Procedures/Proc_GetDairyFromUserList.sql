IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDairyFromUserList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDairyFromUserList]
GO

create Proc [dbo].[Proc_GetDairyFromUserList]  
(  
@UserId nvarchar(100)  
)  
as  
BEGIN  
SET FMTONLY OFF;    
select  a.DairyFromUser,a.DairyId from Claim_ReAssignmentDairy a inner join MNT_Users u on a.ReAssignTo = u.SNo inner join ClaimAccidentDetails c on c.AccidentClaimId =a.AccidentClaimId where u.UserId=@UserId  
  
END  
GO


