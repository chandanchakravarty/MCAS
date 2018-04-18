IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'VehicleNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN VehicleNo nvarchar(10) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'Make')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN Make nvarchar(50) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'ModelNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN ModelNo nvarchar(50) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'DriverEmployeeNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN DriverEmployeeNo nvarchar(50) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'DriverName')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN DriverName nvarchar(200) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'DriverNRICNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN DriverNRICNo nvarchar(20) NULL
END