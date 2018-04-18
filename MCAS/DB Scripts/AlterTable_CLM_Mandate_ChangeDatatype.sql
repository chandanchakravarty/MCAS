  IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Mandate' and column_name = 'Evidence')
     BEGIN
         ALTER TABLE dbo.CLM_Mandate ALTER COLUMN Evidence NVARCHAR(max)
     END