
  IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TransactionAuditLog' and column_name = 'ChangedColumns')
     BEGIN
         ALTER TABLE MNT_TransactionAuditLog ALTER COLUMN ChangedColumns NVARCHAR(MAX)
     END