IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE Category='TaskSelection' and Lookupdesc='Task')
BEGIN
 update MNT_Lookups set lookupCode='285' WHERE Category='TaskSelection' and Lookupdesc='Task'
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE Category='TaskSelection' and Lookupdesc='Dairy')
BEGIN
update MNT_Lookups set lookupCode='281' WHERE Category='TaskSelection' and Lookupdesc='Dairy'
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE Category='TaskSelection' and Lookupdesc='Mandate')
BEGIN
update MNT_Lookups set lookupCode='287' WHERE Category='TaskSelection' and Lookupdesc='Mandate' 
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE Category='TaskSelection' and Lookupdesc='Payment')
BEGIN
 update MNT_Lookups set lookupCode='290' WHERE Category='TaskSelection' and Lookupdesc='Payment'
END