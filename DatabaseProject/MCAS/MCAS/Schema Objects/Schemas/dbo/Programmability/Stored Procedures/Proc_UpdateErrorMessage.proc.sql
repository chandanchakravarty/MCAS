CREATE PROCEDURE [dbo].[Proc_UpdateErrorMessage]
	@FileId [int],
	@Errors [nvarchar](max)
WITH EXECUTE AS CALLER
AS
update MNT_FileUpload
       set ErrorMessage=@Errors,Status='Fail',HasError='Y'
       where FileId=@FileId


