IF EXISTS (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
 WHERE TABLE_NAME = 'MNT_HOLDER_INTEREST_LIST' AND COLUMN_NAME = 'HOLDER_NAME')
 BEGIN
     ALTER TABLE MNT_HOLDER_INTEREST_LIST  
	 ALTER COLUMN HOLDER_NAME NVARCHAR(200) NULL
 END
 go
 
EXEC   sp_addextendedproperty 'Caption', 'Name', 'user', dbo, 'table', 'MNT_HOLDER_INTEREST_LIST', 'column', HOLDER_NAME

EXEC   sp_addextendedproperty 'Description', 'This column stores the Interest Holder Name', 'user', dbo, 'table', 'MNT_HOLDER_INTEREST_LIST', 'column', HOLDER_NAME
