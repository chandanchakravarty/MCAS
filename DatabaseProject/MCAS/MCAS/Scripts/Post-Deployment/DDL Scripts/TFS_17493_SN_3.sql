-- =============================================
-- Script Template
-- =============================================
IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'DateofNoticetoSafety')
BEGIN
ALTER TABLE CLM_MandateSummary Add [DateofNoticetoSafety] [datetime] NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'InformedInsurer')
BEGIN
ALTER TABLE CLM_MandateSummary Add [InformedInsurer] [datetime] NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'EZLinkCardNo')
BEGIN
ALTER TABLE CLM_MandateSummary Add [EZLinkCardNo] [varchar](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'ODStatus')
BEGIN
ALTER TABLE CLM_MandateSummary Add [ODStatus] [varchar](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_MandateSummary' and column_name = 'RecoverableFromInsurerBI')
BEGIN
ALTER TABLE CLM_MandateSummary Add [RecoverableFromInsurerBI] [varchar](1) NULL
END

