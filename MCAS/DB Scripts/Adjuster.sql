IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and  column_name = 'Address1')
BEGIN 
 ALTER TABLE MNT_Adjusters ALTER COLUMN  Address1 nvarchar(210)
END

------------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and  column_name = 'Address2')
BEGIN 
 ALTER TABLE MNT_Adjusters ALTER COLUMN  Address2 nvarchar(210)
END

-------------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and  column_name = 'Address3')
BEGIN 
 ALTER TABLE MNT_Adjusters ALTER COLUMN  Address3 nvarchar(210)
END


