IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'DateJoined')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD DateJoined datetime NULL
END

IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'DateResigned')
BEGIN
  ALTER TABLE [dbo].[ClaimAccidentDetails] ADD DateResigned datetime NULL
END