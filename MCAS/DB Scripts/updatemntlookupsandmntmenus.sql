update MNT_Lookups set Lookupdesc='Mandate Approval' where Category='Taskselection' and Lookupvalue=5
update MNT_Lookups set Lookupdesc='Payment Approval' where Category='Taskselection' and Lookupvalue=6


update MNT_Menus set IsActive='N' where MenuId =200