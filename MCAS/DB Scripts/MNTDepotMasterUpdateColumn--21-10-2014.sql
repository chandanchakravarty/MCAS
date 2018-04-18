IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'City') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ALTER COLUMN City VARCHAR(50) NULL
END




IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'City') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ALTER COLUMN City [nvarchar](50) NULL
END