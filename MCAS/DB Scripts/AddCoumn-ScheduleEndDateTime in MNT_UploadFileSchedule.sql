
IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_UploadFileSchedule' AND [COLUMN_NAME] = 'JobStartDateTime')
BEGIN
ALTER TABLE [dbo].[MNT_UploadFileSchedule] ADD JobStartDateTime datetime null
END

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_UploadFileSchedule' AND [COLUMN_NAME] = 'JobEndDateTime')
BEGIN
ALTER TABLE [dbo].[MNT_UploadFileSchedule] ADD JobEndDateTime datetime null
END