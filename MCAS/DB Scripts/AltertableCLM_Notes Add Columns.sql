IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'ModifiedBy') 

BEGIN 

ALTER TABLE CLM_Notes 

add ModifiedBy nvarchar(25) 

END 



IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'CreatedDate') 

BEGIN 

ALTER TABLE CLM_Notes 

add CreatedDate datetime 

END 



IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'CreatedBy') 

BEGIN 

ALTER TABLE CLM_Notes 

add CreatedBy nvarchar(25) 

END



IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'ModifiedDate') 

BEGIN 

ALTER TABLE CLM_Notes 

add ModifiedDate datetime 

END 