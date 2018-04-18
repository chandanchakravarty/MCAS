IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ServiceProvider' and column_name = 'VehNo') 
BEGIN
Alter table CLM_ServiceProvider Add  VehNo varchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ServiceProvider' and column_name = 'TPVehNo') 
BEGIN
Alter table CLM_ServiceProvider Add  TPVehNo varchar(100)
END