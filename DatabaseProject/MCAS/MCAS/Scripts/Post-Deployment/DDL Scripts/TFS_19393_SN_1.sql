IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'Initial') 
BEGIN
Alter table MNT_Users Add  Initial varchar(5)
END