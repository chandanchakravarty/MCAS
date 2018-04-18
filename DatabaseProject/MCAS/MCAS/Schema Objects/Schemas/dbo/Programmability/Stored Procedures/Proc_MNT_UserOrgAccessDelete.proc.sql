CREATE PROCEDURE [dbo].[Proc_MNT_UserOrgAccessDelete]
	@p_UserId [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
IF EXISTS (SELECT OrgCode,OrgName FROM [MNT_UserOrgAccess] WHERE UserId = @p_UserId)  
DELETE FROM MNT_UserOrgAccess WHERE UserId = @p_UserId 
END


