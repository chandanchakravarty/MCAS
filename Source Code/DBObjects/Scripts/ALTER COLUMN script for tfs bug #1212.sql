IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
 WHERE TABLE_NAME = 'POL_LOCATIONS' AND COLUMN_NAME = 'NAME')
 BEGIN
     ALTER TABLE POL_LOCATIONS  
	 ALTER COLUMN NAME NVARCHAR(100) NULL
 END
 go
 
 IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
 WHERE TABLE_NAME = 'POL_LOCATIONS' AND COLUMN_NAME = 'LOC_ADD1')
 BEGIN
     ALTER TABLE POL_LOCATIONS  
	 ALTER COLUMN LOC_ADD1 NVARCHAR(100) NULL
 END
 go
 

 