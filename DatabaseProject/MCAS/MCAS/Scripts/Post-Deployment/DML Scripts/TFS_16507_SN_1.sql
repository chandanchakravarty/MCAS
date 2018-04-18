IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'UploadReportNo')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD UploadReportNo nvarchar(50) NULL
END



