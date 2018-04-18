-- =============================================
-- Script Template
-- =============================================
IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'SessionId')
BEGIN
ALTER TABLE MNT_Users Add [SessionId] nvarchar(200) NULL
END