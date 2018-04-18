
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'City')
BEGIN
ALTER TABLE CLM_Claims
ALTER COLUMN City nvarchar(250)
END



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'State')
BEGIN

ALTER TABLE CLM_Claims
ALTER COLUMN State nvarchar(250)
END


