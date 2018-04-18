IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'EmailAddress2') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD EmailAddress2 VARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'OffNo2') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD OffNo2 NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'MobileNo2') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD MobileNo2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'Fax2') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD Fax2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'WorkShopType') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD WorkShopType NVARCHAR(2) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'EffectiveFrom') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD EffectiveFrom datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'EffectiveTo') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD EffectiveTo datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'FirstContactPersonName') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD FirstContactPersonName NVARCHAR(250) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'SecondContactPersonName') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD SecondContactPersonName NVARCHAR(250) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_OrgCountry' and column_name = 'InsurerType') 
BEGIN 
ALTER TABLE MNT_OrgCountry 
ADD InsurerType NVARCHAR(2) 
END


