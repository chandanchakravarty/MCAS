IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'DriverMobileNo')
BEGIN
ALTER TABLE ClaimAccidentDetails ALTER COLUMN DriverMobileNo [numeric](18, 0) NULL
END