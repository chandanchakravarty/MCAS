-- =============================================
-- Script Template
-- =============================================

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'lookupCode')
BEGIN

ALTER TABLE MNT_Lookups
ALTER COLUMN lookupCode varchar(500)

END
