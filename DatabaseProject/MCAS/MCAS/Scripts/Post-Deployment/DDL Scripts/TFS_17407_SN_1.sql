-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TransactionAuditLog' and column_name = 'CustomInfo')
BEGIN
    ALTER TABLE MNT_TransactionAuditLog  ADD CustomInfo nvarchar(250)
END
