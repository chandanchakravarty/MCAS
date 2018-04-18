-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'INVA')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('INVA', 'Invoice Amount', 'InvoiceAmount', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 30)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CELOR')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CELOR', 'Loss of Rental', 'LossofRental', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 31)
END


IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CELOI')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CELOI', 'Loss of Income', 'LossofIncome', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 32)
END


IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CELOU')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CELOU', 'Loss of Use', 'CELossofUse', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 33)
END


IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CEME')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CEME', 'Medical Expenses', 'MedicalExpenses', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 34)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'OTH1S')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('OTH1S', 'Others (1)', 'OthersCEC', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 35)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'LF')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('LF', 'Legal Fee', 'LegalFee', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 36)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CESF')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CESF', 'Survey Fee', 'CESurveyFee', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 37)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'TPGIA')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('TPGIA', 'TP GIA Fee', 'TPGIAFee', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 38)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'LTA')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('LTA', 'LTA Fee', 'LTAFee', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 39)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CR')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CR', 'Car Rental', 'CarRental', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 40)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'CC')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('CC', 'Car Courtesy', 'CarCourtesy', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 41)
END

IF NOT EXISTS (SELECT 1 FROM [MNT_Lookups] WHERE [Category] = 'Trancomponent' AND Lookupvalue = 'OTH2S')
BEGIN
 INSERT INTO [MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive], [lookupCode], [CreateDate], [CreateBy], [ModifiedBy], [ModifiedDate], [DisplayOrder])
    VALUES ('OTH2S', 'Others (2)', 'OthersCEI', 'Trancomponent', 'Y', NULL, NULL, NULL, NULL, NULL, 42)
END
