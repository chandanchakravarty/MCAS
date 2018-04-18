if exists(select * from MNT_Lookups where Category='Trancomponent' and Lookupvalue='LME' and DisplayOrder=4)
Begin
update MNT_Lookups set Lookupdesc = 'LOG Medical Expenses' where Category='Trancomponent' and Lookupvalue='LME' and DisplayOrder=4
End