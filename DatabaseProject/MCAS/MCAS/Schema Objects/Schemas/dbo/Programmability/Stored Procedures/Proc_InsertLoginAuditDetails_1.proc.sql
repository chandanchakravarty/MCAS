CREATE Procedure [dbo].[Proc_InsertLoginAuditDetails]
(
 @userId nvarchar(50),
 @loggedInUserId nvarchar(50),
 @loggedInUserName nvarchar(50),
 @loggedInBranch nvarchar(50),
 @loggedInCountryId nvarchar(50),
 @loggedInCountryName nvarchar(50),
 @sNo nvarchar(max) out
)
As
   
     INSERT INTO LoginAuditLog(UserId,LoggedInUserId,LoggedInUserName,LoggedInBranch,loggedInCountryId,LoggedInCountryName,LogInTime)
     VALUES(@userId,@loggedInUserId,@loggedInUserName,@loggedInBranch,@loggedInCountryId,@loggedInCountryName,GETDATE())
     SELECT @sNo= SCOPE_IDENTITY()
GO


