IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'IPNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN IPNo [nvarchar](10) NULL
END