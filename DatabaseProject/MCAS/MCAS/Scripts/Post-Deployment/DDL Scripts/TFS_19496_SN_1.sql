-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CreditDebitNoteAmount') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CreditDebitNoteAmount numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CreditDebitNoteNo') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CreditDebitNoteNo nvarchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Insurer') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Insurer numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'OwnerHirer') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add OwnerHirer numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ThirdParty') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add ThirdParty numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Contractor') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Contractor numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'NetCostofRepairs') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add NetCostofRepairs numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TotalClaimReceipts') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TotalClaimReceipts numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TClmExpensesClaimant') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TClmExpensesClaimant numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TClmExpensesIncidental') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TClmExpensesIncidental numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TotalClaimExpenses') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TotalClaimExpenses numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'NetClaimRecovery') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add NetClaimRecovery numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ProfitSharingForTaxi') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add ProfitSharingForTaxi numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'OverUnderClaimRecovery') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add OverUnderClaimRecovery numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ShareNetClmRecovery') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add ShareNetClmRecovery numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LessCDGEAdminFee') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add LessCDGEAdminFee numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CDGE1Or3') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CDGE1Or3 numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Taxi2Or3') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Taxi2Or3 numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'GeneralRemarks') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add GeneralRemarks nvarchar(MAX)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'SORASerialNo') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add SORASerialNo nvarchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'SORASOCRA') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add SORASOCRA nvarchar(100)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Adjustment') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Adjustment numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'Compensation') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add Compensation numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CDGECost') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add CDGECost numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TPReceipt') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add TPReceipt numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'NetCDGEClaimExpenses') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add NetCDGEClaimExpenses numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ClaimableReceipt') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add ClaimableReceipt numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'NetClientReceipt') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add NetClientReceipt numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'OverUnderClientRecovery') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add OverUnderClientRecovery numeric(18,2)
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'NetClientRecovery') 
BEGIN 
Alter Table CLM_ClaimRecovery
Add NetClientRecovery numeric(18,2)
END