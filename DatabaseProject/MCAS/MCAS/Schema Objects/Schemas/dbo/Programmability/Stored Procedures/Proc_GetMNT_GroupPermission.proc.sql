CREATE PROCEDURE [dbo].[Proc_GetMNT_GroupPermission]
	@GroupId [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
SELECT [GroupPermissionId]
      ,[GroupId]
      ,[MenuId]
      ,[Status]
      ,[RowId]
      ,[Read]
      ,[Write]
      ,[Delete]
      ,[SplPermission]
  FROM [MNT_GroupPermission] where [GroupId] =@GroupId order by 1 desc

END


