----Script For Menu  for itrack-1405
If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=533)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relat�rio de Emiss�o' where MENU_ID=533



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=530)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relat�rio de Resseguro e Cosseguro' where MENU_ID=530


If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=536)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Relat�rio SUSEP' where MENU_ID=536



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=537)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Gest�o de Interface' where MENU_ID=537



If Exists(Select * from MNT_MENU_LIST_MULTILINGUAL where MENU_ID=395)
Update MNT_MENU_LIST_MULTILINGUAL  
set MENU_NAME='Log de Transa��es' where MENU_ID=395