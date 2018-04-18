     IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_LogRequest' and column_name = 'IsVoid')
     BEGIN
         ALTER TABLE CLM_LogRequest ADD IsVoid bit NOT NULL DEFAULT(0)
     END
      