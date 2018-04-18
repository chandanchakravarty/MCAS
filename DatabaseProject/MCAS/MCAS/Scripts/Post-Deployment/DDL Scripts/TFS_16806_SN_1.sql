

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimTask' and column_name = 'Remarks')
BEGIN
ALTER TABLE CLM_ClaimTask ALTER COLUMN Remarks nvarchar(1000) NULL
END