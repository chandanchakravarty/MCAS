
update MNT_Menus set Hyp_Link_Address = '/UserAdmin/UserGroupsIndex' where MenuId = 114

  

EXEC sp_rename 'MNT_Users.PaymentLimit', 'FAL_OD', 'COLUMN'



EXEC sp_rename 'MNT_Users.CreditNoteLimit', 'FAL_PDBI', 'COLUMN'

