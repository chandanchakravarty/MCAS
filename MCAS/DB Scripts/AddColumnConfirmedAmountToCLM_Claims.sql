IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ConfirmedAmount')
BEGIN
ALTER TABLE CLM_Claims ADD ConfirmedAmount numeric(18,0) NULL
END


