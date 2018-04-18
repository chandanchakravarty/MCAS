IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_VehicleListingMaster]'
  AND [COLUMN_NAME] = 'IS_ACTIVE')
BEGIN
  ALTER TABLE [dbo].MNT_VehicleListingMaster ADD [IS_ACTIVE] nvarchar(10) null
END


IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_VehicleListingMaster]'
  AND [COLUMN_NAME] = 'CreatedBy')
BEGIN
  ALTER TABLE [dbo].MNT_VehicleListingMaster ADD [CreatedBy] nvarchar(50) null
END



IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_VehicleListingMaster]'
  AND [COLUMN_NAME] = 'CreatedBy')
BEGIN
  ALTER TABLE [dbo].MNT_VehicleListingMaster ADD [CreatedDate] datetime null
END

IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_VehicleListingMaster]'
  AND [COLUMN_NAME] = 'ModifiedBy')
BEGIN
  ALTER TABLE [dbo].MNT_VehicleListingMaster ADD [ModifiedBy] nvarchar(50) null
END



IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_VehicleListingMaster]'
  AND [COLUMN_NAME] = 'ModifiedDate')
BEGIN
  ALTER TABLE [dbo].MNT_VehicleListingMaster ADD [ModifiedDate] datetime null
END
