BEGIN TRAN

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'Organization')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN Organization int NULL
END

ROLLBACK