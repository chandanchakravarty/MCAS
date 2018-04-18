IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE LookupID=431)
BEGIN
 update MNT_Lookups set lookupCode='281' where LookupID=431 
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE LookupID=430)
BEGIN
update MNT_Lookups set lookupCode='285' where LookupID=430
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE LookupID=434 )
BEGIN
update MNT_Lookups set lookupCode='287' where LookupID=434  
END
IF EXISTS (SELECT 1 FROM [MNT_Lookups]  WHERE LookupID=435)
BEGIN
 update MNT_Lookups set lookupCode='290' where LookupID=435
END




