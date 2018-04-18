IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Mandate' and column_name = 'ClaimantName')
     BEGIN
         ALTER TABLE CLM_Mandate ALTER COLUMN [ClaimantName] nvarchar(250) NULL
     END