IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_AttachmentList' and column_name = 'AttachType')
BEGIN
ALTER TABLE MNT_AttachmentList ALTER COLUMN AttachType [nvarchar](50) NULL
END