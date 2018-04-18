----Script For Menu  for itrack-1405
If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=533)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relatório de Emissão' where MENU_ID=533



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=530)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relatório de Resseguro e Cosseguro' where MENU_ID=530


If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=536)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relatório SUSEP' where MENU_ID=536



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=537)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Gestão de Interface' where MENU_ID=537



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=395)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Log de Transações' where MENU_ID=395