IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'mnt_cedant' and column_name = 'City') 
BEGIN 
ALTER TABLE mnt_cedant 
ALTER COLUMN City NVARCHAR(50) NULL
END




IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'mnt_cedant' and column_name = 'Province') 
BEGIN 
ALTER TABLE mnt_cedant 
ALTER COLUMN Province NVARCHAR(50) NULL
END