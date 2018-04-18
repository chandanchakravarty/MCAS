EXEC sp_rename 'CLM_ClaimRecovery.CostofRepairs', 'CostofRepairs_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.LossofUse', 'LossofUse_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.OtherExpences', 'OtherExpences_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.ReportServeyFee', 'ReportServeyFee_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.ReportReserveyFee', 'ReportReserveyFee_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.ReportLTA_GIA_PolicyFee', 'ReportLTA_GIA_PolicyFee_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.TPLawyerCost', 'TPLawyerCost_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.TPLawyerDisbursment', 'TPLawyerDisbursment_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.LeagalLawyerCost', 'LeagalLawyerCost_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.legalLawyerDisbursement', 'legalLawyerDisbursement_R', 'COLUMN'
EXEC sp_rename 'CLM_ClaimRecovery.TotalAmt', 'TotalAmt_R', 'COLUMN'


 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'CostofRepairs_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD CostofRepairs_S decimal(18,2)
 END
 
  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LossofUse_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD LossofUse_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'OtherExpences_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD OtherExpences_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ReportServeyFee_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD ReportServeyFee_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ReportReserveyFee_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD ReportReserveyFee_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'ReportLTA_GIA_PolicyFee_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD ReportLTA_GIA_PolicyFee_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TPLawyerCost_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD TPLawyerCost_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TPLawyerDisbursment_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD TPLawyerDisbursment_S decimal(18,2)
 END

 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'LeagalLawyerCost_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD LeagalLawyerCost_S decimal(18,2)
 END
 
  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'legalLawyerDisbursement_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD legalLawyerDisbursement_S decimal(18,2)
 END
 
  IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ClaimRecovery' and column_name = 'TotalAmt_S')
 BEGIN
     ALTER TABLE CLM_ClaimRecovery ADD TotalAmt_S decimal(18,2)
 END
