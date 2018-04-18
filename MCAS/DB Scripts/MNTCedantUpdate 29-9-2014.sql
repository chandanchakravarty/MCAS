IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'State') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD State VARCHAR(100) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'FirstContactPersonName') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD FirstContactPersonName VARCHAR(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EmailAddress1') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD EmailAddress1 VARCHAR(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'OfficeNo1') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD OfficeNo1 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'MobileNo1') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD MobileNo1 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'FaxNo1') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD FaxNo1 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'SecondContactPersonName') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD SecondContactPersonName VARCHAR(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EmailAddress2') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD EmailAddress2 VARCHAR(100) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'OfficeNo2') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD OfficeNo2 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'MobileNo2') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD MobileNo2 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'FaxNo2') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD FaxNo2 VARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'InsurerType') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD InsurerType VARCHAR(5) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EffectiveFrom') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD EffectiveFrom datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Cedant' and column_name = 'EffectiveTo') 
BEGIN 
ALTER TABLE MNT_Cedant 
ADD EffectiveTo datetime
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_Cedant' AND [COLUMN_NAME] = 'Remarks')
BEGIN
  ALTER TABLE MNT_Cedant
    ALTER COLUMN Remarks VARCHAR(800) 
END





--select * from mnt_cedant
