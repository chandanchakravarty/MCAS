IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'TempClaimNo')
BEGIN
ALTER TABLE [dbo].[ClaimAccidentDetails] ADD TempClaimNo nvarchar(100)
END