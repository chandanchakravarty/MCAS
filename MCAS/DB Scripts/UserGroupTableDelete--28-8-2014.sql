IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupsMaster')

BEGIN

 Delete MNT_GroupsMaster

END




IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupPermission')

BEGIN

 Delete MNT_GroupPermission

END