IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and  column_name = 'OrgCategory')

BEGIN

 ALTER TABLE MNT_Users

add OrgCategory nvarchar(200)

END


update MNT_Users
set OrgCategory='BU~Bus'