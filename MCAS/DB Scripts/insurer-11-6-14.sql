IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and  column_name = 'CedantName')
BEGIN 
 ALTER TABLE MNT_Cedant ALTER COLUMN  CedantName nvarchar(200)
END