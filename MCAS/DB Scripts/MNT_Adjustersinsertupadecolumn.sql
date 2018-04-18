IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'EmailAddress2') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD EmailAddress2 VARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'OffNo2') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD OffNo2 NVARCHAR(20) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'MobileNo2') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD MobileNo2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'Fax2') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD Fax2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'InsurerType') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD InsurerType NVARCHAR(2) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'EffectiveFrom') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD EffectiveFrom datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'EffectiveTo') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD EffectiveTo datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Adjusters' and column_name = 'Remarks') 
BEGIN 
ALTER TABLE MNT_Adjusters 
ADD Remarks NVARCHAR(500) 
END





