IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'ModifiedBy') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add ModifiedBy nvarchar(25) 

END 

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'CreatedDate') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add CreatedDate datetime 

END 

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'CreatedBy') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add CreatedBy nvarchar(25) 

END

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'ModifiedDate') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add ModifiedDate datetime 

END 