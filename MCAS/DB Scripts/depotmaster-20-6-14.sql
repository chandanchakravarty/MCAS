IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and  column_name = 'ContactPerson')

BEGIN

 ALTER TABLE MNT_DepotMaster add ContactPerson nvarchar(50)

END