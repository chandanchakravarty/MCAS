-- Step 1
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name = 'MNT_Users')
BEGIN
update MNT_Users set GroupId=Null
END

-- Step 2
IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_Users' AND [COLUMN_NAME] = 'GroupId')
BEGIN
  ALTER TABLE MNT_Users
    ALTER COLUMN GroupId int
END

-- Step 3
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name = 'MNT_Users')
BEGIN
if EXISTS (select top 1 * from MNT_GroupsMaster where GroupCode='MN')
BEGIN
update MNT_Users set GroupId=(select GroupId from MNT_GroupsMaster where GroupCode='MN') where UserId in ('pravesh','varun','Nishant')
END
if EXISTS (select top 1 * from MNT_GroupsMaster where GroupCode='CO')
BEGIN
update MNT_Users set GroupId=(select GroupId from MNT_GroupsMaster where GroupCode='CO') where UserId in ('neha','sanjay','shikha','ashish')
END
END




