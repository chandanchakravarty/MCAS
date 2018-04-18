IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_AttachmentList' and column_name = 'AttachFileType')
BEGIN
ALTER TABLE MNT_AttachmentList ALTER COLUMN AttachFileType [nvarchar](15) NULL
END