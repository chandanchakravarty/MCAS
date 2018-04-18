  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'ClaimantNames')
     BEGIN
         ALTER TABLE CLM_Notes ADD  ClaimantNames NVARCHAR(255)
     END