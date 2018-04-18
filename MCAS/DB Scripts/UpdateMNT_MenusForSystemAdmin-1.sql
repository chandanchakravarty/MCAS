IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '100')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurer',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Cedant/CedantIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '100'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '101')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Listing Upload',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/VehicleUploadIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '101'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '102')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Nature of Loss',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/LossNatureMasterList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '102'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '103')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Type of Loss',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/Index',
      [IsActive] = 'Y'
  WHERE [MenuId] = '103'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '104')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Main Class of Business',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ProductBusiness/ProductBusinessIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '104'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '105')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Sub-Class',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ProductBusiness/SubClassIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '105'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '106')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Depot Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/DepotMasterIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '106'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '107')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Surveyor Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/AdjusterMasters/SurveyorIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '107'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '108')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Adjuster Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/AdjusterMasters/AdjusterIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '108'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '109')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Solicitor  Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/AdjusterMasters/SolicitorIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '109'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '110')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Expense Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimExpenseIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '110'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '111')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Status Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'claims-status-master.html',
      [IsActive] = 'N'
  WHERE [MenuId] = '111'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '112')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Currency Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/CurrencyMasterIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '112'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '113')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Exchange Rate Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/ExchangeIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '113'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '114')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'User Admin',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/UserAdmin/Index',
      [IsActive] = 'Y'
  WHERE [MenuId] = '114'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '115')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Country Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/CountryMaster/Index',
      [IsActive] = 'Y'
  WHERE [MenuId] = '115'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '116')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary and Follow-Up',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'diary-and-follow-up-master.html',
      [IsActive] = 'N'
  WHERE [MenuId] = '116'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '117')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Type',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'vehicle-type.html',
      [IsActive] = 'N'
  WHERE [MenuId] = '117'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '118')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make and Model',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'Masters/VehicleIndex',
      [IsActive] = 'N'
  WHERE [MenuId] = '118'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '119')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Bus Captain Listing',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/VehicleBusCaptainIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '119'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '120')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Class ',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/VehicleClassIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '120'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '121')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/VehicleIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '121'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '122')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Model',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/VModelIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '122'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '123')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurance Policy',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '123'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '124')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'GST Setting',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/GSTIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '124'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '125')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Organization Country',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/CountryMaster/OrgCountryIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '125'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '126')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Officer Duty',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/ClaimOfficerDutyIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '126'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '127')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'LOU Rate',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/Masters/LOUIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '127'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '128')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Hospital Information ',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/HospitalIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '128'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '129')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Re Assignment Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReAssignmentEditorP',
      [IsActive] = 'Y'
  WHERE [MenuId] = '129'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '130')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Accident',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAccidentEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '130'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '131')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Own Damage',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/OwnDamage',
      [IsActive] = 'Y'
  WHERE [MenuId] = '131'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '132')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'PD/BI Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ThirdPartyEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '132'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '133')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = ' Notes Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimNotesEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '133'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '134')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Tasks Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TaskEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '134'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '135')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Mandate',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimMandateReqEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '135'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '136')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Attachments Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '136'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '137')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/DiaryTaskEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '137'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '138')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Reserve Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimReserveEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '138'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '139')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Payment Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/PaymentEditorNew',
      [IsActive] = 'Y'
  WHERE [MenuId] = '139'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '140')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Transactions History',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TransactionEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '140'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '200')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Dashboard',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Home/Index',
      [IsActive] = 'Y'
  WHERE [MenuId] = '200'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '201')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary Listing',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,#',
      [IsActive] = 'Y'
  WHERE [MenuId] = '201'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '202')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diaried items',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Home/MenuList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '202'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '203')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Assigned Tasks',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,assigned-tasks.html',
      [IsActive] = 'Y'
  WHERE [MenuId] = '203'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '204')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Escalation Tasks',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,escalation-tasks.html',
      [IsActive] = 'Y'
  WHERE [MenuId] = '204'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '205')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Registration',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/#',
      [IsActive] = 'N'
  WHERE [MenuId] = '205'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '206')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'New Claims Registration',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=New',
      [IsActive] = 'Y'
  WHERE [MenuId] = '206'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '207')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Incomplete Claims Registration',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=Incomplete',
      [IsActive] = 'Y'
  WHERE [MenuId] = '207'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '208')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Adjustments',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=Adjustment',
      [IsActive] = 'Y'
  WHERE [MenuId] = '208'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '209')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Enquiry',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=RegEnquiry',
      [IsActive] = 'Y'
  WHERE [MenuId] = '209'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '210')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/#',
      [IsActive] = 'N'
  WHERE [MenuId] = '210'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '211')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment Processing',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment',
      [IsActive] = 'Y'
  WHERE [MenuId] = '211'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '212')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Incomplete Claims Payment Registration',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimProcessing/incomplete-claims-payment-registration.html',
      [IsActive] = 'N'
  WHERE [MenuId] = '212'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '213')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment Document Enquiry',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/claims-payment-document-enquiry.html',
      [IsActive] = 'Y'
  WHERE [MenuId] = '213'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '214')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'ClaimRecoveryProcessing/claims-recovery.html',
      [IsActive] = 'Y'
  WHERE [MenuId] = '214'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '215')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery Processing',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery',
      [IsActive] = 'Y'
  WHERE [MenuId] = '215'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '216')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/claims-recovery.html',
      [IsActive] = 'N'
  WHERE [MenuId] = '216'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '217')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Enquiry',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = 'N
',
      [IsActive] = 'Y'
  WHERE [MenuId] = '217'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '218')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimEnquiry?claimMode=Enquiry',
      [IsActive] = 'Y'
  WHERE [MenuId] = '218'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '219')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Documents Printed',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimDocumentsPrintedIndex?claimMode=EnqDocPrinted',
      [IsActive] = 'Y'
  WHERE [MenuId] = '219'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '220')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Hospital Information Editor',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/HospitalEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '220'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '221')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder1',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '221'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '222')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder2',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '222'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '223')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder3',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '223'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '224')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder4',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '224'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '225')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Attachments List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '225'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '226')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'PD/BI List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ThirdPartyList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '226'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '227')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Notes List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimNotesList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '227'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '228')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Tasks List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TaskIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '228'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '229')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Mandate',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimMandateList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '229'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '230')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ToDoList',
      [IsActive] = 'Y'
  WHERE [MenuId] = '230'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '231')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurer Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Cedant/CedantEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '231'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '232')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Nature of Loss Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/LossNatureMasterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '232'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '233')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Type of Loss Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/Create',
      [IsActive] = 'Y'
  WHERE [MenuId] = '233'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '234')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Main Class of Business Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ProductBusiness/ProductBusinessEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '234'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '235')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Sub Class Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ProductBusiness/SubClassEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '235'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '236')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Depot Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/DepotMasterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '236'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '237')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Reserve List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReverseChange',
      [IsActive] = 'Y'
  WHERE [MenuId] = '237'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '238')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Surveyor Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/AdjusterMasters/SurveyorEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '238'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '239')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Payment List',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimPayment',
      [IsActive] = 'Y'
  WHERE [MenuId] = '239'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '240')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Adjuster Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/AdjusterMasters/AdjusterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '240'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '241')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Solicitor Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/AdjusterMasters/SolicitorEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '241'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '242')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Expense Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimExpenseEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '242'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '243')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Close Reason<br>Master',
      [IsMenuItem] = 'Y',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimCloseIndex',
      [IsActive] = 'Y'
  WHERE [MenuId] = '243'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '244')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Currency Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/CurrencyMasterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '244'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '245')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Exchange Rate Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/ClaimMasters/ExchangeEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '245'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '246')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Country Master Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/CountryMaster/CountryEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '246'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '247')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Class Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/VehicleClassEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '247'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '248')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/VehicleMaster',
      [IsActive] = 'Y'
  WHERE [MenuId] = '248'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '249')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Model Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/VehicleMaster',
      [IsActive] = 'Y'
  WHERE [MenuId] = '249'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '250')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurance Policy Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '250'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '251')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'GST Setting Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '251'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '252')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Bus Captain Listing Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/VehicleBusCaptainEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '252'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '253')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Organization Country Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/CountryMaster/OrgCountryEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '253'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '254')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Officer Duty Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/ClaimOfficerDutyEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '254'
END
IF EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = '255')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'LOU Rate Details',
      [IsMenuItem] = 'N',
      [Hyp_Link_Address] = '/Masters/LOUEditor',
      [IsActive] = 'Y'
  WHERE [MenuId] = '255'
END