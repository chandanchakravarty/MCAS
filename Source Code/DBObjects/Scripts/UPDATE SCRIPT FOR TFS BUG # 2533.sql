IF EXISTS (SELECT MENU_ID FROM MNT_MENU_LIST WHERE MENU_ID = 328)
BEGIN
update mnt_menu_list set is_active ='Y' where MENU_ID = 328
END