IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'mnt_motor_make' and  column_name = 'MakeName')
BEGIN 
 ALTER TABLE mnt_motor_make ALTER COLUMN  MakeName nvarchar(100)
END


--select * from mnt_motor_make