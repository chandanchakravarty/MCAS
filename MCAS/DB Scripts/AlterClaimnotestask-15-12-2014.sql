IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Notes' AND [COLUMN_NAME] = 'AccidentId')
BEGIN
ALTER TABLE [dbo].CLM_Notes ADD AccidentId int null
END





IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_ClaimTask' AND [COLUMN_NAME] = 'AccidentId')
BEGIN
ALTER TABLE [dbo].CLM_ClaimTask ADD AccidentId int null
END



IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_AttachmentList' AND [COLUMN_NAME] = 'AccidentId')
BEGIN
ALTER TABLE [dbo].MNT_AttachmentList ADD AccidentId int null
END

IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'TODODIARYLIST' AND [COLUMN_NAME] = 'AccidentId')
BEGIN
ALTER TABLE [dbo].TODODIARYLIST ADD AccidentId int null
END


--select * from TODODIARYLIST