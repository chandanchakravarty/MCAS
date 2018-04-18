--Againcheckin for crach on local uat-tfs16806

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TransactionAuditLog' and column_name = 'IsValidXml')
     BEGIN
         ALTER TABLE dbo.MNT_TransactionAuditLog ADD IsValidXml bit  NULL DEFAULT(0)
     END
