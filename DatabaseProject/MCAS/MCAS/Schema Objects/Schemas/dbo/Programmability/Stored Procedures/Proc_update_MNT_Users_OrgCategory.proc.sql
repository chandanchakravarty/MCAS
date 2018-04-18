CREATE PROCEDURE [dbo].[Proc_update_MNT_Users_OrgCategory]
	@p_UserId [varchar](20),
	@p_OrgCategory [nvarchar](200)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
UPDATE [dbo].MNT_Users SET OrgCategory=@p_OrgCategory Where UserId=@p_UserId 
END


