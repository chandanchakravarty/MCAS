

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus')
BEGIN
update MNT_Menus set SubMenu='N' where MenuId=217
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus')
BEGIN
update MNT_Menus set SubMenu='Y' where MenuId =218
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus')
BEGIN
update MNT_Menus set SubMenu='Y' where MenuId =219
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Menus')
BEGIN
update MNT_Menus set SubMenu='Y' where MenuId =276
END