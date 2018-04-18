



IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'EmailId') 

BEGIN 

ALTER TABLE MNT_Users
ADD EmailId nvarchar(100) null

END 