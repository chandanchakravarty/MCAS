IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and  column_name = 'Address2')
BEGIN 
 ALTER TABLE MNT_Cedant ALTER COLUMN  Address2 nvarchar(200)
END




IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and  column_name = 'Address3')
BEGIN 
 ALTER TABLE MNT_Cedant ALTER COLUMN  Address3 nvarchar(200)
END



IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and  column_name = 'Address')
BEGIN 
 ALTER TABLE MNT_Cedant ALTER COLUMN  Address nvarchar(200)
END