 IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
 WHERE TABLE_NAME = 'MNT_PROFIT_CENTER_LIST' AND COLUMN_NAME = 'PC_NAME')
 BEGIN
     ALTER TABLE MNT_PROFIT_CENTER_LIST  
	 ALTER COLUMN PC_NAME NVARCHAR(200) NULL
 END
 go
