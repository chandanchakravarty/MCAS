

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CLM_ClaimRecovery')
DROP TABLE [dbo].[CLM_ClaimRecovery]


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Claim_Recovery')
DROP TABLE dbo.Claim_Recovery
