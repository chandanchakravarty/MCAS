  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_GroupsMaster' and column_name = 'RoleCode')
     BEGIN
         ALTER TABLE MNT_GroupsMaster ADD  RoleCode VARCHAR(5)
     END