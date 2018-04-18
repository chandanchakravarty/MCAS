--DBscript


IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Reserve' AND [COLUMN_NAME] = 'MovementType')
BEGIN
  ALTER TABLE [dbo].[CLM_Reserve] ADD MovementType nvarchar(50) NULL
END

IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Mandate' AND [COLUMN_NAME] = 'MovementType')
BEGIN
  ALTER TABLE [dbo].[CLM_Mandate] ADD MovementType nvarchar(50) NULL
END





