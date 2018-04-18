IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'IsReported')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD IsReported [bit] NULL
END