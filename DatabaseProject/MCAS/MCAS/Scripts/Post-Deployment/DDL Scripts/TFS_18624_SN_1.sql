IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'CountryOrgazinationCode')
     BEGIN
         ALTER TABLE MNT_OrgCountry ALTER COLUMN CountryOrgazinationCode nvarchar(5) not null
     END
