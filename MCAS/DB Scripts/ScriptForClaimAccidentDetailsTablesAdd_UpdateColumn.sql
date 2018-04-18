IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'ODStatus') 
BEGIN 
ALTER TABLE ClaimAccidentDetails 
add ODStatus varchar(1) 
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'BusServiceNo')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN BusServiceNo nvarchar(8)
END


IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'Organization')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN Organization nvarchar(80)
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'InvestStatus')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN InvestStatus nvarchar(100)
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'Results')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN Results nvarchar(100)
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'DutyIO')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN DutyIO nvarchar(250)
END