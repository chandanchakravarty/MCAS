
IF NOT EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Hospital') BEGIN INSERT INTO .[MNT_TableDesc] ([TableName] ,[TableDesc],[Type])
VALUES ('MNT_Hospital' ,
        'Hospital',
        'System Admin') END

		IF NOT EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_Payment') BEGIN INSERT INTO .[MNT_TableDesc] ([TableName] ,[TableDesc],[Type])
VALUES ('CLM_Payment' ,
        'Payment',
        'Claims') END