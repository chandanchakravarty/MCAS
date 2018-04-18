-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'DistrictCode') 
BEGIN 
Alter Table STG_UploadedFileData
Add DistrictCode nvarchar(10) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'RoadName') 
BEGIN 
Alter Table STG_UploadedFileData
Add RoadName nvarchar(max) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'SeriousInjury') 
BEGIN 
Alter Table STG_UploadedFileData
Add SeriousInjury nvarchar(50) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'MinorInjury') 
BEGIN 
Alter Table STG_UploadedFileData
Add MinorInjury nvarchar(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'Fatal') 
BEGIN 
Alter Table STG_UploadedFileData
Add Fatal nvarchar(50) 
END