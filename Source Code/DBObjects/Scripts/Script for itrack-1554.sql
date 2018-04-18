
-----The itrack-1554

If exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=123)
begin
update MNT_MENU_LIST_MULTILINGUAL
set MENU_NAME='Gestão de Interfaces'
where MENU_ID=123
End

----Menu Name  to be changed

IF EXISTS(Select * from MNT_MENU_LIST_MULTILINGUAL  where MENU_ID=21)
 BEGIN
 Update MNT_MENU_LIST_MULTILINGUAL
 set MENU_NAME='Adicionar/Editar Tipo de Usuário' where MENU_ID=21
 END