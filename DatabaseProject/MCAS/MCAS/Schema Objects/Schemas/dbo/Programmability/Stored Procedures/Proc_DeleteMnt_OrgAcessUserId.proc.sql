CREATE PROCEDURE [dbo].[Proc_DeleteMnt_OrgAcessUserId]
	@userID [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;                  
  BEGIN                  
  delete from [MNT_UserOrgAccess] WHERE [UserId]=@userID
  END


