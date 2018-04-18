If Exists(select * from MNT_MENU_LIST where MENU_ID='553' and PARENT_ID='261')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where  MENU_ID='553' and PARENT_ID='261'
End
