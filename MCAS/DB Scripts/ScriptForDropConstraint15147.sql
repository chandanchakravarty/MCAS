IF EXISTS (SELECT * 
  FROM sys.foreign_keys  where name = 'FK_ClaimAccidentDetails_MNT_InsruanceM')
Begin
  ALTER TABLE [ClaimAccidentDetails] DROP CONSTRAINT [FK_ClaimAccidentDetails_MNT_InsruanceM]
End

