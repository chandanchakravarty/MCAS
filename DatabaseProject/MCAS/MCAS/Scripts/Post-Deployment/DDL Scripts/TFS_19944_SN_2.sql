IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'PrftShrTxTlCases') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add PrftShrTxTlCases numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ReserveId') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add ReserveId int
END