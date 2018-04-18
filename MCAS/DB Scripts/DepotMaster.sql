IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and  column_name = 'Address')
BEGIN 
 ALTER TABLE MNT_DepotMaster ALTER COLUMN  Address nvarchar(200)
END