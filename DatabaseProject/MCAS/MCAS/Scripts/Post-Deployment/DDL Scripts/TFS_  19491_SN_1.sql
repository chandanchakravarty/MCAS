IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_LookupsMaster' and column_name IN ('LookupCategoryCode','LookupCategoryDesc','IsActive','IsCommonMaster')) 
BEGIN
INSERT INTO MNT_LookupsMaster (LookupCategoryCode,LookupCategoryDesc,IsActive,IsCommonMaster)
VALUES ('NatureOfAcc','NatureOfAcc','Y','Y')
END


