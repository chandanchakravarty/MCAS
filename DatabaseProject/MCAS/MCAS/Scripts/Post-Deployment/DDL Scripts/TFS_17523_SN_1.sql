IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'UserDispName')
BEGIN
ALTER TABLE MNT_Users ALTER COLUMN UserDispName nvarchar(50) NULL
END
