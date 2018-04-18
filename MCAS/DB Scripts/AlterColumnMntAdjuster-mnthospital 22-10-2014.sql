IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'mnt_adjusters' and column_name = 'City') 
BEGIN 
ALTER TABLE mnt_adjusters 
ALTER COLUMN City NVARCHAR(100) NULL
END




IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'mnt_adjusters' and column_name = 'Province') 
BEGIN 
ALTER TABLE mnt_adjusters 
ALTER COLUMN Province NVARCHAR(100) NULL
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'City') 
BEGIN 
ALTER TABLE MNT_Hospital 
ALTER COLUMN City NVARCHAR(100) NULL
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'State') 
BEGIN 
ALTER TABLE MNT_Hospital 
ALTER COLUMN State NVARCHAR(100) NULL
END


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'Remarks') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ALTER COLUMN Remarks NVARCHAR(1000) NULL
END