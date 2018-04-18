IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'InputDate') 
BEGIN
Alter table ClaimAccidentDetails Add  InputDate datetime
END