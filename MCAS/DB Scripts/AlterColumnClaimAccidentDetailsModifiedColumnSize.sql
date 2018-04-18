IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'IPNo')
BEGIN
update ClaimAccidentDetails set IPNo=case when len(IPNo)>10 then SUBSTRING(convert(varchar,IPNo),1,10) else IPNo end 
END
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'IPNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN [IPNo] nvarchar(10) NOT NULL
END



IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[ClaimAccidentDetails]'
  AND [COLUMN_NAME] = 'ReportedRefId')
BEGIN
  ALTER TABLE [dbo].ClaimAccidentDetails ADD [ReportedRefId] int
END