IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'Clm_claims' and column_name = 'VehicleRegnNo') 
BEGIN
 Alter Table Clm_claims  Alter Column VehicleRegnNo  nvarchar(10) NULL
END