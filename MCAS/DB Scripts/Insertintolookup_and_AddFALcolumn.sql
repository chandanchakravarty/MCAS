IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='MA' and Category='FALCat') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive]) VALUES (N'MA', N'Mandate Approval', N'Mandate Approval', N'FALCat',N'Y')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PA' and Category='FALCat') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive]) VALUES (N'PA', N'Payment Approval', N'Payment Approval', N'FALCat',N'Y')
END 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FAL' and column_name = 'UnlimitedAmt') 
BEGIN 
ALTER TABLE MNT_FAL
ADD UnlimitedAmt varchar(1) NOT NULL DEFAULT('N')
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FAL' and column_name = 'CreatedBy') 
BEGIN 
ALTER TABLE MNT_FAL
ADD CreatedBy varchar(100) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FAL' and column_name = 'CreatedDate') 
BEGIN 
ALTER TABLE MNT_FAL
ADD CreatedDate DATETIME NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FAL' and column_name = 'ModifiedBy') 
BEGIN 
ALTER TABLE MNT_FAL
ADD ModifiedBy varchar(100) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FAL' and column_name = 'ModifiedDate') 
BEGIN 
ALTER TABLE MNT_FAL
ADD ModifiedDate DATETIME NULL
END
