IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus' and column_name = 'VirtualSource')
     BEGIN
         ALTER TABLE dbo.MNT_Menus ALTER COLUMN VirtualSource [varchar](100) NULL
     END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus' and column_name = 'ProductName')
     BEGIN
         ALTER TABLE dbo.MNT_Menus ALTER COLUMN ProductName [varchar](50) NULL
     END