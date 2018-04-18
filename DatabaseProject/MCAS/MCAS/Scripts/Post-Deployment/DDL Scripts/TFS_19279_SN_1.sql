IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimTask' and column_name = 'Remarks') 
BEGIN
Alter table CLM_ClaimTask ALTER COLUMN Remarks nvarchar(MAX)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_AttachmentList' and column_name = 'AttachFileDesc') 
BEGIN
Alter table MNT_AttachmentList ALTER COLUMN AttachFileDesc nvarchar(MAX)
END