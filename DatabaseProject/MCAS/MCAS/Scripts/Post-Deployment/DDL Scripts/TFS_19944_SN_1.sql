IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'InvoiceAmount') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add InvoiceAmount numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LossofRental') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add LossofRental numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Others1') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Others1 numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LossofIncome') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add LossofIncome numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LegalFee') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add LegalFee numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'SurveyFee') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add SurveyFee numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TPGIAFee') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TPGIAFee numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Others2') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Others2 numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LTAFee') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add LTAFee numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CELossofUse') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CELossofUse numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'MedicalExpenses') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add MedicalExpenses numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CarRental') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CarRental numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CarCourtesy') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CarCourtesy numeric(18,2)
END