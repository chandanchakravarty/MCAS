IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'clm_claims' and column_name = 'ConfirmedAmount')
BEGIN
ALTER TABLE clm_claims ALTER COLUMN ConfirmedAmount [numeric](18, 2) NULL
END