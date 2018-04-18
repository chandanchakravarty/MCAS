IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='Task')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('1','Task','Task','TaskSelection','Y')
    END
    
    IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='Dairy')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('2','Dairy','Dairy','TaskSelection','Y')
    END
     IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='ReassignedDiary')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('3','ReassignedDiary','ReassignedDiary','TaskSelection','Y')
    END
    
   IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='Escalation')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('4','Escalation','Escalation','TaskSelection','Y')
    END

	IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='Mandate')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('5','Mandate','Mandate','TaskSelection','Y')
    END
	IF NOT EXISTS(SELECT * FROM MNT_Lookups where Category='TaskSelection' and Lookupdesc='Payment')
   BEGIN
      Insert into MNT_Lookups(Lookupvalue,Lookupdesc,Description,Category,IsActive)
             values ('6','Payment','Payment','TaskSelection','Y')
    END