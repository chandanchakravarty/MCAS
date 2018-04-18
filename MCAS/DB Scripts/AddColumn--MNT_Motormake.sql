IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_Motor_Make]'
  AND [COLUMN_NAME] = 'CreatedBy')
BEGIN
  ALTER TABLE [dbo].MNT_Motor_Make ADD [CreatedBy] nvarchar(50) null
END



IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_Motor_Make]'
  AND [COLUMN_NAME] = 'CreatedDate')
BEGIN
  ALTER TABLE [dbo].MNT_Motor_Make ADD [CreatedDate] datetime null
END

IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_Motor_Make]'
  AND [COLUMN_NAME] = 'ModifiedBy')
BEGIN
  ALTER TABLE [dbo].MNT_Motor_Make ADD [ModifiedBy] nvarchar(50) null
END



IF NOT EXISTS (SELECT TOP 1
    1
  FROM INFORMATION_SCHEMA.COLUMNS
  WHERE [TABLE_NAME] = '[MNT_Motor_Make]'
  AND [COLUMN_NAME] = 'ModifiedDate')
BEGIN
  ALTER TABLE [dbo].MNT_Motor_Make ADD [ModifiedDate] datetime null
END
