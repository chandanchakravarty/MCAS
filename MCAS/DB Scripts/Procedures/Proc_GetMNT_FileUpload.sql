
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMNT_FileUpload]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMNT_FileUpload]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Proc [dbo].[Proc_GetMNT_FileUpload]
as
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


GO


