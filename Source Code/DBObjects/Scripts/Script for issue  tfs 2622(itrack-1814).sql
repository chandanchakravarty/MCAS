If exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=558)
Begin
Update MNT_MENU_LIST_MULTILINGUAL
set MENU_NAME='Limite de Retenção'
where MENU_ID=558
End

If exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=263)
Begin
Update MNT_MENU_LIST_MULTILINGUAL
set MENU_NAME='Informações de Seguradoras'
where MENU_ID=263
End


If exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=264)
Begin
Update MNT_MENU_LIST_MULTILINGUAL
set MENU_NAME='Configurações Principais'
where MENU_ID=264
End




