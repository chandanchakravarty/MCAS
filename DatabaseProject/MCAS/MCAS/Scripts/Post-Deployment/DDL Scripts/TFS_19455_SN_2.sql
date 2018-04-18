-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'DistrictCode') 
BEGIN 
Alter Table STG_TAC_IP
Add DistrictCode nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'RoadName') 
BEGIN 
Alter Table STG_TAC_IP
Add RoadName nvarchar(max) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'SeriousInjury') 
BEGIN 
Alter Table STG_TAC_IP
Add SeriousInjury nvarchar(50) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'MinorInjury') 
BEGIN 
Alter Table STG_TAC_IP
Add MinorInjury nvarchar(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'Fatal') 
BEGIN 
Alter Table STG_TAC_IP
Add Fatal nvarchar(50) 
END