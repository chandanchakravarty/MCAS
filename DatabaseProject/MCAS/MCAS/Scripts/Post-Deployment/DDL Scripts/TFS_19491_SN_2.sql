IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name IN ('Lookupvalue','Lookupdesc','IsActive','Description','Category')) 
BEGIN
INSERT INTO MNT_Lookups (Lookupvalue,Lookupdesc,IsActive,Description,Category)
VALUES ('4','Recovery Claim','Y','Recovery','ClaimType')
END
