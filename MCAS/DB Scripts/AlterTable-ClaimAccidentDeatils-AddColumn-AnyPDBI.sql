IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'TPClaimentStatus') 

BEGIN 

ALTER TABLE ClaimAccidentDetails 

add TPClaimentStatus nvarchar(1) null 

END 


