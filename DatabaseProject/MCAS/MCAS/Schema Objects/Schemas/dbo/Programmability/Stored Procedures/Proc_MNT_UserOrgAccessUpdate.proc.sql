CREATE PROCEDURE [dbo].[Proc_MNT_UserOrgAccessUpdate]
	@p_UserId [nvarchar](50),
	@p_OrgCode [nvarchar](50),
	@p_OrgName [nvarchar](100),
	@p_ModifiedDate [datetime],
	@p_ModifiedBy [nvarchar](50),
	@w_UserId [nvarchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
UPDATE [dbo].MNT_UserOrgAccess SET UserId=@p_UserId,OrgCode=@p_OrgCode,OrgName=@p_OrgName,ModifiedDate=@p_ModifiedDate,ModifiedBy=@p_ModifiedBy 
	WHERE UserId=@w_UserId
END


