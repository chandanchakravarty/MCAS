IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'FinalLiability')
     BEGIN
         ALTER TABLE dbo.ClaimAccidentDetails ALTER COLUMN FinalLiability int
     END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'Facts')
     BEGIN
         ALTER TABLE dbo.ClaimAccidentDetails ALTER COLUMN Facts [nvarchar](max) NULL
     END