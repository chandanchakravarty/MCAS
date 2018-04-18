  IF EXISTS(SELECT * FROM MNT_Menus WHERE MenuId=102)
     BEGIN
         UPDATE MNT_Menus SET IsActive='N' WHERE MenuId=102
     END
     
   IF EXISTS(SELECT * FROM MNT_Lookups WHERE LookupID=102)
     BEGIN
         UPDATE MNT_Lookups SET Lookupdesc='Inactive' WHERE LookupID=102
     END
     
                          
   