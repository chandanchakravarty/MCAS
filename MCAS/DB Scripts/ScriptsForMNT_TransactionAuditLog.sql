IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_TransactionAuditLog' AND [COLUMN_NAME] = 'TansDescription')
BEGIN
ALTER TABLE [dbo].[MNT_TransactionAuditLog] ADD [TansDescription] [nvarchar](400) NULL
END
 
IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_TransactionAuditLog' AND [COLUMN_NAME] = 'ClaimID')
BEGIN
ALTER TABLE [dbo].[MNT_TransactionAuditLog] ADD [ClaimID] [int] NULL
END