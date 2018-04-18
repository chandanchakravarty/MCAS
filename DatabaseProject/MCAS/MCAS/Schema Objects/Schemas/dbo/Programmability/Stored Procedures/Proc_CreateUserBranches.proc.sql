CREATE PROCEDURE [dbo].[Proc_CreateUserBranches]
	@UserId [varchar](100),
	@BranchCode [varchar](50),
	@LoginId [varchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN	
	INSERT INTO MNT_UserBranches (UserId, BranchCode) 
				VALUES (@UserId,@BranchCode)
END


