IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupPermission' and 
column_name = 'Read')

BEGIN

 ALTER TABLE MNT_GroupPermission

add [Read] bit,
[Write] bit,
[Delete] bit,
[SplPermission] bit

END

