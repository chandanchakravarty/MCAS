IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'Lookupvalue')
     BEGIN
         ALTER TABLE dbo.MNT_Lookups ALTER COLUMN Lookupvalue [varchar](20) NULL
     END
GO

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE TId=139 and TabId='S_ADMN' and [MenuId] = 297)
BEGIN
  Update MNT_Menus set GroupType='A' where TId=139 and TabId='S_ADMN' and MenuId=297
END