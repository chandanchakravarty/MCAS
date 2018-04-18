IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'POL_EMAIL_SPOOL' and column_name = 'EmailSubject') 

BEGIN 

ALTER TABLE POL_EMAIL_SPOOL 

add EmailSubject nvarchar(400) 

END 