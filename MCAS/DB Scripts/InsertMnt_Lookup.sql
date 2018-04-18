
IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='UserRole' or Lookupvalue='CO')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('CO','Claim Officers','Claim Officers','UserRole','Y')
    END
IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='UserRole' AND Lookupvalue='SP')
   BEGIN
     Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('SP','Supervisors','Supervisors','UserRole','Y')
   END
IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='UserRole' AND Lookupvalue='COSP')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('COSP','Claim Officers & Supervisors','Claim Officers & Supervisors','UserRole','Y')
   END
IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='UserRole' AND Lookupvalue='NON')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('NON','None','None','UserRole','Y')
   END


