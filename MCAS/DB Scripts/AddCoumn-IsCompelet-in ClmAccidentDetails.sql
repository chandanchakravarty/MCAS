IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'IsComplete')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD IsComplete int NULL
END

IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'OperatingHours')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD OperatingHours int NULL
END