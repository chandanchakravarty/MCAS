CREATE PROCEDURE [dbo].[Proc_UpdateMnt_OrgAcessUserId]
	@OlduserID [nvarchar](50),
	@NewuserID [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;                  
  BEGIN                  
  UPDATE [MNT_UserOrgAccess] SET [UserId] = @NewuserID WHERE [UserId]=@OlduserID
  END


