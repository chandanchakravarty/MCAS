IF NOT EXISTS (SELECT
    11
  FROM [MNT_Lookups]
  WHERE [Category] = 'Trancomponent'
  AND Lookupvalue = 'Labl')
BEGIN

  INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('Labl', 'Liability', 'Liability', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 15)
END
           
GO


