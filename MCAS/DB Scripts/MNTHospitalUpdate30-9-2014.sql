
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'HospitalAddress2') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD HospitalAddress2 NVARCHAR(300) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'HospitalAddress3') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD HospitalAddress3 NVARCHAR(300) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'City') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD City NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'State') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD State NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'Country') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD Country NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'PostalCode') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD PostalCode NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'FirstContactPersonName') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD FirstContactPersonName NVARCHAR(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'MobileNo1') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD MobileNo1 NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'SecondContactPersonName') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD SecondContactPersonName NVARCHAR(250) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'EmailAddress2') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD EmailAddress2 NVARCHAR(30) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'OffNo2') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD OffNo2 NVARCHAR(30) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'MobileNo2') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD MobileNo2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'Fax2') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD Fax2 NVARCHAR(50) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'HospitalType') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD HospitalType NVARCHAR(2) 
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'EffectiveFrom') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD EffectiveFrom datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'EffectiveTo') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD EffectiveTo datetime
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'Remarks') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD Remarks NVARCHAR(800) 
END


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Hospital' and column_name = 'Status') 
BEGIN 
ALTER TABLE MNT_Hospital 
ADD Status NVARCHAR(10) 
END