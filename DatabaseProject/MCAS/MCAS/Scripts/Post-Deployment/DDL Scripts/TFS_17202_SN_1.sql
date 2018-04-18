IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ServiceProvider' and column_name = 'AppointedDate')
     BEGIN
         ALTER TABLE CLM_ServiceProvider ALTER COLUMN AppointedDate datetime  NULL
     END