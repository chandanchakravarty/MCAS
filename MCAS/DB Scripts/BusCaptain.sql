IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_BusCaptain' and  column_name = 'Nationality')

BEGIN

 ALTER TABLE MNT_BusCaptain

add Nationality nvarchar(100)

END

-------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_BusCaptain' and  column_name = 'BusCaptainName')
BEGIN 
 ALTER TABLE MNT_BusCaptain ALTER COLUMN  BusCaptainName nvarchar(200)
END

------------------------------------------------------------

