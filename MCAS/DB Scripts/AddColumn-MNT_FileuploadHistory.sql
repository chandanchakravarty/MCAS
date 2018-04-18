IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_FileUploadHistory]'
  AND [COLUMN_NAME] = 'ScheduleStartDateTime')
BEGIN
  ALTER TABLE [dbo].MNT_FileUploadHistory ADD [ScheduleStartDateTime] datetime null
END