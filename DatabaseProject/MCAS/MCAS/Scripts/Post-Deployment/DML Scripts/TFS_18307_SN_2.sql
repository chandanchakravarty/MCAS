IF NOT EXISTS (SELECT
    1
  FROM dbo.[MNT_Lookups]
  WHERE [Category] = 'UserRole'
  AND [Lookupvalue] = 'Ad')
BEGIN
  INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [CreateBy], [lookupCode]
  , [DisplayOrder],[CreateDate])
    VALUES ('Ad', 'Admin', 'Admin', 'UserRole', 'Y', 'pravesh', '5', '5',GETDATE())
END

UPDATE MNT_Lookups
SET [lookupCode] = '1',
    [DisplayOrder] = '1'
WHERE [Category] = 'UserRole'
AND [Lookupvalue] = 'CO'
UPDATE MNT_Lookups
SET [lookupCode] = '2',
    [DisplayOrder] = '2'
WHERE [Category] = 'UserRole'
AND [Lookupvalue] = 'SP'

UPDATE MNT_Lookups
SET [lookupCode] = '3',
    [DisplayOrder] = '3'
WHERE [Category] = 'UserRole'
AND [Lookupvalue] = 'COSP'

UPDATE MNT_Lookups
SET [lookupCode] = '4',
    [DisplayOrder] = '5'
WHERE [Category] = 'UserRole'
AND [Lookupvalue] = 'NON'