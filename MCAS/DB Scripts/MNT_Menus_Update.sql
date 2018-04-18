    IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId=131)
     BEGIN
           Update MNT_Menus set AdminDisplayText='Claims',DisplayTitle='Claims' where MenuId=131
     END
    IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId=241)
     BEGIN
           update MNT_Menus set IsActive='N' where MenuId=241
     END
    IF EXISTS(SELECT * FROM MNT_Menus WHERE  MenuId IN  (201,202,203,204))
     BEGIN
           update MNT_Menus set IsActive='Y' where MenuId IN  (201,202,203,204)
     END
    IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId IN  (202,203,204))
     BEGIN
           update MNT_Menus set SubMenu='Y' where MenuId IN  (202,203,204)
     END
