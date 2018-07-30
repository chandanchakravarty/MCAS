IF EXISTS(SELECT * FROM sys.columns
WHERE Name = N'Description' AND OBJECT_ID = OBJECT_ID(N'TM_CodeMaster'))
BEGIN
exec sp_rename 'TM_CodeMaster.Description', 'OrganizationId', 'COLUMN';
END  


