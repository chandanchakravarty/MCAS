IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TEMPLATE_MASTER' and column_name = 'Is_Header') 
BEGIN 
ALTER TABLE MNT_TEMPLATE_MASTER ADD Is_Header Char(1) Null
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TEMPLATE_MASTER' and column_name = 'ParantId') 
BEGIN
Alter table MNT_TEMPLATE_MASTER Add  ParantId int NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TEMPLATE_MASTER' and column_name = 'ScreenId') 
BEGIN
Alter table MNT_TEMPLATE_MASTER Add  ScreenId int NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TEMPLATE_MASTER' and column_name = 'Id') 
BEGIN
Alter table MNT_TEMPLATE_MASTER Add  Id int NOT NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_TEMPLATE_MASTER' and column_name = 'OutPutFormat') 
BEGIN
Alter table MNT_TEMPLATE_MASTER Add  OutPutFormat varchar(50) NULL
END