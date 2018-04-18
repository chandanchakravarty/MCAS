-- =============================================
-- Script Template
-- =============================================


IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'IsMigrated')
BEGIN

ALTER TABLE ClaimAccidentDetails
ADD IsMigrated varchar(1) NULL
END
