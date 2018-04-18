CREATE PROCEDURE [dbo].[Proc_UpdateFolderPath]
	@FileRefNo [varchar](50),
	@Filepath [nvarchar](200)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN  
update MNT_FileUpload set UploadPath=@Filepath where FileRefNo=@FileRefNo
END


