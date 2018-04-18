
update MNT_Menus set Hyp_Link_Address = '/ClaimProcessing/ClaimAccidentEditorNew?policyId=0&claimMode=New' where TId = 27 and MenuId = 206

IF EXISTS (SELECT * 
  FROM sys.foreign_keys  where name = 'FK_CLM_Claims_MNT_InsruanceM_PolicyId')
Begin
  ALTER TABLE [CLM_Claims] DROP CONSTRAINT [FK_CLM_Claims_MNT_InsruanceM_PolicyId]
End
