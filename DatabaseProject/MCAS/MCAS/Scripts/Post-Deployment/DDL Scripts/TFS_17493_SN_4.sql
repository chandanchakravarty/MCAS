-- =============================================
-- Script Template
-- =============================================
IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_PaymentSummary' and column_name = 'DateofNoticetoSafety')
BEGIN
ALTER TABLE CLM_PaymentSummary Add [DateofNoticetoSafety] [datetime] NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_PaymentSummary' and column_name = 'InformSafetytoreviewfindings')
BEGIN
ALTER TABLE CLM_PaymentSummary Add [InformSafetytoreviewfindings] [nvarchar](10) NULL
END


IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_PaymentSummary' and column_name = 'EZLinkCardNo')
BEGIN
ALTER TABLE CLM_PaymentSummary Add [EZLinkCardNo] [varchar](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_PaymentSummary' and column_name = 'ODStatus')
BEGIN
ALTER TABLE CLM_PaymentSummary Add [ODStatus] [varchar](1) NULL
END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_PaymentSummary' and column_name = 'RecoverableFromInsurerBI')
BEGIN
ALTER TABLE CLM_PaymentSummary Add [RecoverableFromInsurerBI] [varchar](1) NULL
END

