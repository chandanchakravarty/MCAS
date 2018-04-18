IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'Initial') 
BEGIN
ALTER TABLE MNT_OrgCountry ADD Initial nvarchar(10)
END
