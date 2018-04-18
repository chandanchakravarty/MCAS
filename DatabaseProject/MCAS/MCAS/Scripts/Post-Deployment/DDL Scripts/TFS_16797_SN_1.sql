IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimTask' and column_name = 'TaskModifiedDate')
     BEGIN
         ALTER TABLE dbo.CLM_ClaimTask ADD TaskModifiedDate datetime  NULL
     END