  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_LookupsMaster' and column_name = 'LookupMasterDesc')
     BEGIN
         ALTER TABLE MNT_LookupsMaster ADD  [LookupMasterDesc] [nvarchar](100) NULL
     END