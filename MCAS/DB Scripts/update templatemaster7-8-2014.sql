update MNT_TEMPLATE_MASTER
set filename='Redirect claim to  3P'+ char(39) +'s' + ' '+ 'Insurers.pdf'
where template_id=23


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'print_jobs' and  column_name = 'CREATED_BY')
BEGIN 
 ALTER TABLE print_jobs ALTER COLUMN  CREATED_BY nvarchar(50)

END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'print_jobs' and  column_name = 'MODIFIED_BY')
BEGIN 
 ALTER TABLE print_jobs ALTER COLUMN  MODIFIED_BY nvarchar(50)
 
END




