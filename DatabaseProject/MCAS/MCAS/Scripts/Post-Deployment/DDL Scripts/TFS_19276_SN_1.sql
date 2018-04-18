IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'CDGIClaimRef') 
BEGIN
Alter table ClaimAccidentDetails Add  CDGIClaimRef varchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'OwnerName') 
BEGIN
Alter table ClaimAccidentDetails Add  OwnerName varchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'OwnInsurer') 
BEGIN
Alter table ClaimAccidentDetails Add  OwnInsurer varchar(100)
END