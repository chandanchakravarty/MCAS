CREATE PROCEDURE [dbo].[Proc_GetMNT_FileUpload]
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
SELECT [FileId]
      ,[FileRefNo]
      ,[FileName]
      ,[FileType]
      ,[UploadType]
      ,[UploadPath]
      ,[UploadedDate]
      ,[TotalRecords]
      ,[SuccessRecords]
      ,[FailedRecords]
      ,[Status]
      ,[Is_Processed]
      ,[Processed_Date]
      ,[Is_Active]
      ,[HasError]
      ,[CreatedBy]
  FROM [MNT_FileUpload]
  
END


