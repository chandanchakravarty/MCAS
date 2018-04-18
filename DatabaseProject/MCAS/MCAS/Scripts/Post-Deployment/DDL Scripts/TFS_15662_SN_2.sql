IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_ProductClass' and column_name = 'ClassDesc')
     BEGIN
         ALTER TABLE dbo.MNT_ProductClass ALTER COLUMN ClassDesc [nvarchar](100) NULL
     END