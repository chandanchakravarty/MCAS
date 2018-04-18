IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'VehicleRegnNo')
     BEGIN
         ALTER TABLE CLM_Claims ALTER COLUMN VehicleRegnNo nvarchar(10) null
     END