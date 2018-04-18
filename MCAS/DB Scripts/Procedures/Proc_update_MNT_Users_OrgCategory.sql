IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Proc_update_MNT_Users_OrgCategory')AND type in (N'P', N'PC'))
DROP PROCEDURE Proc_update_MNT_Users_OrgCategory
GO
CREATE PROCEDURE Proc_update_MNT_Users_OrgCategory
@p_UserId varchar(20),
@p_OrgCategory nvarchar(200)
AS
SET FMTONLY OFF;
BEGIN
UPDATE [dbo].MNT_Users SET OrgCategory=@p_OrgCategory Where UserId=@p_UserId
END
GO