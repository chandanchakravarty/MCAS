	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Country' and column_name = 'CountryShortCode')
     BEGIN
         alter Table MNT_Country alter column CountryShortCode varchar(4)
     END