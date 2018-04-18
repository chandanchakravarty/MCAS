-- =============================================
-- Script Template
-- =============================================
IF Not EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_orgname' AND object_id = OBJECT_ID('MNT_UserOrgAccess')) 
BEGIN
CREATE INDEX idx_orgname
ON MNT_UserOrgAccess (OrgName);
END

IF Not EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_orgcode' AND object_id = OBJECT_ID('MNT_UserOrgAccess')) 
BEGIN
CREATE INDEX idx_orgcode
ON MNT_UserOrgAccess (OrgCode);
END