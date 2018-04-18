    IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId in (286,287,288,289,290,291,292))
     BEGIN
         update MNT_Menus set IsMenuItem='N' where menuid in (286,287,288,289,290,291,292)
     END