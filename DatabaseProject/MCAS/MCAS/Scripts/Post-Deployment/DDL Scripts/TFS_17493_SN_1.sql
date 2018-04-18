IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'LODSentdate')
BEGIN
ALTER TABLE CLM_Claims Add [LODSentdate] [datetime] NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'InformedInsurer')
BEGIN
ALTER TABLE CLM_Claims Add [InformedInsurer] [datetime] NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'EZLinkCardNo')
BEGIN
ALTER TABLE CLM_Claims Add [EZLinkCardNo] [char](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'NODNOS ')
BEGIN
ALTER TABLE CLM_Claims Add [NODNOS] [char](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'RecoveredtoDate')
BEGIN
ALTER TABLE CLM_Claims Add [RecoveredtoDate] [decimal](18, 9) NULL
END

