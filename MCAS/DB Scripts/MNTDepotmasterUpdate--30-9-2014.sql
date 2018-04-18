IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'EmailAddress2') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD EmailAddress2 VARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'OffNo2') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD OffNo2 NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'MobileNo2') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD MobileNo2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'Fax2') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD Fax2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'WorkShopType') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD WorkShopType NVARCHAR(2) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'EffectiveFrom') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD EffectiveFrom datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'EffectiveTo') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD EffectiveTo datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'Remarks') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD Remarks NVARCHAR(800) 
END
