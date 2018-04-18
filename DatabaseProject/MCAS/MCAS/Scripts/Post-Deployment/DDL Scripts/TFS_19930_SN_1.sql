IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_VehicleListingMaster' and column_name = 'Type')
BEGIN
ALTER TABLE MNT_VehicleListingMaster
ALTER COLUMN Type NVARCHAR(100) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_VehicleListingMaster' and column_name = 'Aircon')
BEGIN
ALTER TABLE MNT_VehicleListingMaster
ALTER COLUMN Aircon NVARCHAR(200) NULL
END

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_VehicleListingMaster' and column_name = 'Axle')
BEGIN
ALTER TABLE MNT_VehicleListingMaster
ALTER COLUMN Axle NVARCHAR(200) NULL
END
