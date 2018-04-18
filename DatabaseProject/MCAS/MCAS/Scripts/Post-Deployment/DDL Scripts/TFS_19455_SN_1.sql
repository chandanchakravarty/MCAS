-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'District') 
BEGIN 
Alter Table ClaimAccidentDetails
Add District nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'RoadName') 
BEGIN 
Alter Table ClaimAccidentDetails
Add RoadName nvarchar(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'MinorInjury') 
BEGIN 
Alter Table ClaimAccidentDetails
Add MinorInjury nvarchar(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'SeriousInjury') 
BEGIN 
Alter Table ClaimAccidentDetails
Add SeriousInjury nvarchar(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'Fatal') 
BEGIN 
Alter Table ClaimAccidentDetails
Add Fatal nvarchar(50) 
END
