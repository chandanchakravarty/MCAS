   IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId=241)
     BEGIN
           update MNT_Menus set AdminDisplayText='Lawyer Master Details',IsActive='Y' where MenuId=241
     END
