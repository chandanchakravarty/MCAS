  IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'lookupCode')
     BEGIN
         alter Table MNT_Lookups add lookupCode varchar(10)
     END
	IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'CreateDate')
     BEGIN
         alter Table MNT_Lookups add CreateDate Datetime
     END     

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Lookups' and column_name = 'CreateBy')
     BEGIN
         alter Table MNT_Lookups add CreateBy nvarchar(20)
     END          