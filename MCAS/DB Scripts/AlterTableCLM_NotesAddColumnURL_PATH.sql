IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Notes' and column_name = 'URL_PATH') 

BEGIN 

ALTER TABLE CLM_Notes 

add URL_PATH nvarchar(200) 

END 
