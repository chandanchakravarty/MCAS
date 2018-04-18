-- Apply it in Local UAT
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name = 'MNT_Users')
BEGIN
delete MNT_Users where SNo in (203,204)
END

-- Apply it in both local as well as main UAT.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name = 'MNT_Users')
BEGIN
ALTER TABLE MNT_Users ADD CONSTRAINT IX_MNT_Users_UserId UNIQUE(UserId)
END

-- Apply it in both local as well as main UAT.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES where table_name = 'MNT_Department')
BEGIN
ALTER TABLE MNT_Department ADD CONSTRAINT IX_MNT_Department_DeptName UNIQUE(DeptName)
END



