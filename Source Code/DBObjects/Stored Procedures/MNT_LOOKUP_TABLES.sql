IF NOT EXISTS (select * from MNT_LOOKUP_TABLES WHERE LOOKUP_ID= 1447)
BEGIN
INSERT INTO	MNT_LOOKUP_TABLES
           (LOOKUP_ID
           ,LOOKUP_NAME
           ,LOOKUP_DESC)
     VALUES
           (1447
           ,'RFMAT'
           ,'Building Roofing Material'
           )

END
GO
IF NOT EXISTS (select * from MNT_LOOKUP_TABLES WHERE LOOKUP_ID= 1448)
BEGIN
INSERT INTO	MNT_LOOKUP_TABLES
           (LOOKUP_ID
           ,LOOKUP_NAME
           ,LOOKUP_DESC)
     VALUES
           (1448
           ,'WTRPIP'
           ,'Types of Water Pipes'
           )

END
GO
IF NOT EXISTS (select * from MNT_LOOKUP_TABLES WHERE LOOKUP_ID= 1449)
BEGIN
INSERT INTO	MNT_LOOKUP_TABLES
           (LOOKUP_ID
           ,LOOKUP_NAME
           ,LOOKUP_DESC)
     VALUES
           (1449
           ,'TYPSYS'
           ,'Types Of System'
           )

END
GO
IF NOT EXISTS (select * from MNT_LOOKUP_TABLES WHERE LOOKUP_ID= 1450)
BEGIN
INSERT INTO	MNT_LOOKUP_TABLES
           (LOOKUP_ID
           ,LOOKUP_NAME
           ,LOOKUP_DESC)
     VALUES
           (1450
           ,'TYPFEL'
           ,'Types of Fuels'
           )

END
GO

