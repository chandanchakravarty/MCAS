
--Menu Script For itrack-1618

If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=523)
Begin 
Update MNT_MENU_LIST_MULTILINGUAL
set MENU_NAME='Taxas da Moeda' where MENU_ID=523
End
