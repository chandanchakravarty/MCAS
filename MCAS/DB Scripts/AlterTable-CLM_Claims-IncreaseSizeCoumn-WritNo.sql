  IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WritNo')
     BEGIN
         ALTER TABLE CLM_Claims ALTER COLUMN WritNo NVARCHAR(50)
     END