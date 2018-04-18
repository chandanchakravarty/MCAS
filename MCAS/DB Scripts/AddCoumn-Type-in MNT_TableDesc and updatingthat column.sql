IF NOT EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_TableDesc' AND [COLUMN_NAME] = 'Type')
BEGIN
ALTER TABLE [dbo].[MNT_TableDesc] ADD Type nvarchar(20)
END






---After creating Update Column

IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_InsruanceM') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_InsruanceM' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_BranchLogin') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_BranchLogin' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_SYS_PARAMS') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_SYS_PARAMS' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_MODULE_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_MODULE_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'ADMIN_DocumentNos_________Generic') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'ADMIN_DocumentNos_________Generic' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Menus_Multilingual') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Menus_Multilingual' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_DIARY_DETAILS') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_DIARY_DETAILS' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'VISION_TransRefNos1') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'VISION_TransRefNos1' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'POL_EMAIL_SPOOL') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'POL_EMAIL_SPOOL' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_LOB_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_LOB_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Adjusters_old') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_Adjusters_old' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_VehicleListingMaster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_VehicleListingMaster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TClaim_Registration') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TClaim_Registration' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ProductClass') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ProductClass' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Lookups') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Lookups' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_ReAssignmentDairy') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_ReAssignmentDairy' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PROCESS_TRIGGER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PROCESS_TRIGGER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Branch') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Branch' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TEST_COM_NO') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TEST_COM_NO' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Adjusters') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Adjusters' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Department') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Department' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Claim') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_Claim' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'mnt_menu_backUp') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'mnt_menu_backUp' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_City') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_City' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_RiskType') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_RiskType' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_LossNature') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_LossNature' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_GroupsMaster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_GroupsMaster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyClients') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyClients' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'ClaimAccidentDetails') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'ClaimAccidentDetails' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'RumahAmanItemProposalTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'RumahAmanItemProposalTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_CurrencyM') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_CurrencyM' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'FIN_ClaimLedger') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'FIN_ClaimLedger' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Document') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Document' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_CurrencyTxn') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_CurrencyTxn' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Log') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Log' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_CastropheMaster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_CastropheMaster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_ExpReserve') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_ExpReserve' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Recovery') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Recovery' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_ClaimRecovery') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_ClaimRecovery' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_OrgCountry') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_OrgCountry' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Users') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Users' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_DepotMaster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_DepotMaster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_UserDetails') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_UserDetails' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Document_Transaction') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Document_Transaction' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Cedant') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Cedant' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Payment') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Payment' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_ExpReserve_Log') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_ExpReserve_Log' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_LossType') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_LossType' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TODODIARYLISTTYPES_MULTILINGUAL') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TODODIARYLISTTYPES_MULTILINGUAL' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ClaimOfficerDetail') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ClaimOfficerDetail' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Reserve') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Reserve' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Claimant_Log') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Claimant_Log' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Country') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Country' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PRINT_JOBS') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PRINT_JOBS' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TODODIARYLISTTYPES') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TODODIARYLISTTYPES' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Products') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Products' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyDetailTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyDetailTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyClassRegTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyClassRegTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ProductsMap') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_ProductsMap' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_ClaimReserve') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_ClaimReserve' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_NoAutoGenerator') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_NoAutoGenerator' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_ClaimPayment') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_ClaimPayment' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TODODIARYLIST') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TODODIARYLIST' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_TableDesc') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_TableDesc' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Vision_TransRefNos') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Vision_TransRefNos' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_PasswordSetup') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_PasswordSetup' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'EXCEPTIONLOG') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'EXCEPTIONLOG' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_UserPasswordHistory') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_UserPasswordHistory' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TrackModifiedPassword') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TrackModifiedPassword' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PasswordSetup') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PasswordSetup' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_Notes') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_Notes' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TClaim_ExpReserve') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TClaim_ExpReserve' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_Transactions') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_Transactions' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_DIARY_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_DIARY_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_RegistrationHistory') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_RegistrationHistory' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_UserCountry') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_UserCountry' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TClaim_Claimant') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TClaim_Claimant' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_Claims') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_Claims' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'UserCountryProductLog') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'UserCountryProductLog' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Claimant') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Claimant' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_UserBranches') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_UserBranches' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ClaimReOpened') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ClaimReOpened' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_PACKAGE_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_PACKAGE_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ClaimClosed') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ClaimClosed' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'UserBranchAuditLog') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'UserBranchAuditLog' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyClassRiskItemTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyClassRiskItemTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyRegTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyRegTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_EMAIL') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_EMAIL' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_AttachmentList') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_AttachmentList' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_MOTOR_MODEL') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_MOTOR_MODEL' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ZoneMaster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_ZoneMaster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_GENERATE_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_GENERATE_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Motor_Body') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Motor_Body' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ClaimExpense') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ClaimExpense' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_PACKAGE_TEMPLATES') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_PACKAGE_TEMPLATES' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_CountryIncorporation') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_CountryIncorporation' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_GST') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_GST' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_CountryGrouping') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_CountryGrouping' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_ClaimTask') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_ClaimTask' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Registration') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Registration' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_TEMPLATE_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_TEMPLATE_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_BranchType') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_BranchType' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_TRIGGER_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_TRIGGER_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_TEMPLATE_FORMAT_MASTER') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_TEMPLATE_FORMAT_MASTER' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Motor_Class') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Motor_Class' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Province') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_Province' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Menus') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Menus' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_BusCaptain') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_BusCaptain' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Region') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_Region' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_QuotationNoSetup') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_QuotationNoSetup' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_UserCountryProducts') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_UserCountryProducts' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Motor_Make') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_Motor_Make' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_ProductsCountry') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_ProductsCountry' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_ThirdParty') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'CLM_ThirdParty' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_VehicleListingUpload') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_VehicleListingUpload' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'Claim_Adjuster') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'Claim_Adjuster' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_TransactionAuditLog') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_TransactionAuditLog' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_GroupPermission') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'System Admin' WHERE [TableName] = 'MNT_GroupPermission' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'PolicyAmountTrx') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'PolicyAmountTrx' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'MNT_Broker') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'MNT_Broker' END
IF EXISTS (
SELECT 1
FROM [MNT_TableDesc] WHERE [TableName] = 'TODODIARYLISTs') BEGIN UPDATE [MNT_TableDesc] SET [Type] = 'Claims' WHERE [TableName] = 'TODODIARYLISTs' END




