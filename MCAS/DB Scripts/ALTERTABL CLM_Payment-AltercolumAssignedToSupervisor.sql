--AltertableAssignedToSupervisor

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Payment' and column_name = 'AssignedToSupervisor ')
     BEGIN
         ALTER TABLE CLM_Payment ALTER COLUMN AssignedToSupervisor [nvarchar](max) NULL
     END