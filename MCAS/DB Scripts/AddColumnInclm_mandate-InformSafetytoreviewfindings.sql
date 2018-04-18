IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Mandate' AND [COLUMN_NAME] = 'InformSafetytoreviewfindings')
BEGIN
  ALTER TABLE [dbo].CLM_Mandate ADD [InformSafetytoreviewfindings] nvarchar(10) null
END