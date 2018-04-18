--Add Column

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'clm_claims' and  column_name = 'RecordDeletionDate')

BEGIN

 ALTER TABLE clm_claims

add RecordDeletionDate datetime,
RecordDeletionReason nvarchar(100)

END
