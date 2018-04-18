IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '100')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurer',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Cedant/CedantIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '100'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '101')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Listing Upload',
      [Hyp_Link_Address] = 'Masters/VehicleUploadIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '101'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '102')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Nature of Loss',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/LossNatureMasterList',
      [IsExists] = 'Y'
  WHERE [menuid] = '102'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '103')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Type of Loss',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/Index',
      [IsExists] = 'Y'
  WHERE [menuid] = '103'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '104')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Main Class of Business',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ProductBusiness/ProductBusinessIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '104'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '105')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Sub-Class',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ProductBusiness/SubClassIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '105'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '106')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Depot Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/DepotMasterIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '106'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '107')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Surveyor Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/AdjusterMasters/SurveyorIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '107'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '108')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Adjuster Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/AdjusterMasters/AdjusterIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '108'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '109')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Lawyer Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/AdjusterMasters/SolicitorIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '109'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '110')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Expense Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/ClaimExpenseIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '110'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '111')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Status Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-status-master.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '111'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '112')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Currency Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/CurrencyMasterIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '112'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '113')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Exchange Rate Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/ExchangeIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '113'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '114')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'User Admin',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/UserAdmin/Index',
      [IsExists] = 'Y'
  WHERE [menuid] = '114'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '115')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Country Master',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/CountryMaster/Index',
      [IsExists] = 'Y'
  WHERE [menuid] = '115'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '116')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary and Follow-Up',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,diary-and-follow-up-master.html',
      [IsExists] = 'N'
  WHERE [menuid] = '116'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '117')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Type',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,vehicle-type.html',
      [IsExists] = 'N'
  WHERE [menuid] = '117'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '118')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make and Model',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleIndex',
      [IsExists] = 'N'
  WHERE [menuid] = '118'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '119')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Bus Captain Listing',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleBusCaptainIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '119'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '120')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Class  ',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleClassIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '120'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '121')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '121'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '122')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Model',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/VModelIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '122'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '123')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurance Policy',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/InsuranceMaster/InsurancePolicyMasterIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '123'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '124')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'GST Setting',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/GSTIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '124'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '125')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Organization Country',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/CountryMaster/OrgCountryIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '125'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '126')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Officer Duty',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/ClaimOfficerDutyIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '126'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '127')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'LOU Rate',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Masters/LOUIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '127'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '128')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Hospital Information ',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/HospitalIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '128'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '129')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Re Assignment Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReAssignmentEditorP',
      [IsExists] = 'Y'
  WHERE [menuid] = '129'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '130')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Accident',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAccidentEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '130'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '131')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Own Damage',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/OwnDamage',
      [IsExists] = 'Y'
  WHERE [menuid] = '131'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '132')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'PD/BI Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ThirdPartyEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '132'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '133')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = ' Notes Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimNotesEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '133'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '134')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Tasks Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TaskEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '134'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '135')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Mandate',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimMandateReqEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '135'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '136')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Attachments Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '136'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '137')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/DiaryTaskEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '137'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '138')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Reserve Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimReserveEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '138'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '139')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Payment Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/PaymentEditorNew',
      [IsExists] = 'Y'
  WHERE [menuid] = '139'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '140')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Transactions History',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TransactionEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '140'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '200')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Dashboard',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Home/Index',
      [IsExists] = 'Y'
  WHERE [menuid] = '200'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '201')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary Listing',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,#',
      [IsExists] = 'Y'
  WHERE [menuid] = '201'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '202')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diaried items',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Home/MenuList',
      [IsExists] = 'Y'
  WHERE [menuid] = '202'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '203')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Assigned Tasks',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,assigned-tasks.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '203'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '204')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Escalation Tasks',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,escalation-tasks.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '204'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '205')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Registration',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,#',
      [IsExists] = 'Y'
  WHERE [menuid] = '205'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '206')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'New Claims Registration',
      [Hyp_Link_Address] = 'ClaimRegistration,ClaimProcessing',
      [IsExists] = 'Y'
  WHERE [menuid] = '206'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '207')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Incomplete Claims Registration',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimRegistration?claimMode=Incomplete',
      [IsExists] = 'Y'
  WHERE [menuid] = '207'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '208')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Adjustments',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimRegistration?claimMode=Adjustment',
      [IsExists] = 'Y'
  WHERE [menuid] = '208'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '209')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Enquiry',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimEnquiry?claimMode=Enquiry',
      [IsExists] = 'Y'
  WHERE [menuid] = '209'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '210')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,#',
      [IsExists] = 'Y'
  WHERE [menuid] = '210'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '211')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment Processing',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-payment-processing.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '211'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '212')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Incomplete Claims Payment Registration',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,incomplete-claims-payment-registration.html',
      [IsExists] = 'N'
  WHERE [menuid] = '212'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '213')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Payment Document Enquiry',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-payment-document-enquiry.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '213'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '214')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-recovery.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '214'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '215')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery Processing',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-recovery-processing.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '215'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '216')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Recovery',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,claims-recovery.html',
      [IsExists] = 'Y'
  WHERE [menuid] = '216'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '217')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Enquiry',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,#',
      [IsExists] = 'Y'
  WHERE [menuid] = '217'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '218')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimEnquiry?claimMode=Enquiry',
      [IsExists] = 'Y'
  WHERE [menuid] = '218'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '219')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Documents Printed',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/ClaimDocumentsPrintedIndex?claimMode=EnqDocPrinted',
      [IsExists] = 'Y'
  WHERE [menuid] = '219'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '220')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Hospital Information Editor',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimMasters/HospitalEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '220'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '221')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '221'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '222')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder2',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '222'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '223')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder3',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '223'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '224')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Folder4',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '224'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '225')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Attachments List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsList',
      [IsExists] = 'Y'
  WHERE [menuid] = '225'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '226')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'PD/BI List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ThirdPartyList',
      [IsExists] = 'Y'
  WHERE [menuid] = '226'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '227')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Notes List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimNotesList',
      [IsExists] = 'Y'
  WHERE [menuid] = '227'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '228')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Tasks List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TaskIndex',
      [IsExists] = 'Y'
  WHERE [menuid] = '228'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '229')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Mandate',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimMandateList',
      [IsExists] = 'Y'
  WHERE [menuid] = '229'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '230')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Diary List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ToDoList',
      [IsExists] = 'Y'
  WHERE [menuid] = '230'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '231')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurer Details',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/Cedant/CedantEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '231'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '232')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Nature of Loss Details',
      [Hyp_Link_Address] = 'ClaimMasters/LossNatureMasterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '232'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '233')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Type of Loss Details',
      [Hyp_Link_Address] = 'ClaimMasters/Create',
      [IsExists] = 'Y'
  WHERE [menuid] = '233'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '234')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Main Class of Business Details',
      [Hyp_Link_Address] = 'ProductBusiness/ProductBusinessEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '234'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '235')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Sub Class Details',
      [Hyp_Link_Address] = 'ProductBusiness/SubClassEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '235'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '236')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Depot Master Details',
      [Hyp_Link_Address] = 'Masters/DepotMasterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '236'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '237')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Reserve List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReverseChange',
      [IsExists] = 'Y'
  WHERE [menuid] = '237'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '238')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Surveyor Master Details',
      [Hyp_Link_Address] = 'AdjusterMasters/SurveyorEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '238'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '239')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Payment List',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimPayment',
      [IsExists] = 'Y'
  WHERE [menuid] = '239'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '240')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Adjuster Master Details',
      [Hyp_Link_Address] = 'AdjusterMasters/AdjusterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '240'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '241')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Solicitor Master Details',
      [Hyp_Link_Address] = 'AdjusterMasters/SolicitorEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '241'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '242')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Expense Master Details',
      [Hyp_Link_Address] = 'ClaimMasters/ClaimExpenseEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '242'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '243')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claims Close Reason Master Details',
      [Hyp_Link_Address] = 'ClaimMasters/ClaimCloseEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '243'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '244')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Currency Master Details',
      [Hyp_Link_Address] = 'ClaimMasters/CurrencyMasterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '244'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '245')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Exchange Rate Master Details',
      [Hyp_Link_Address] = 'ClaimMasters/ExchangeEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '245'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '246')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Country Master Details',
      [Hyp_Link_Address] = 'CountryMaster/CountryEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '246'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '247')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Class Details',
      [Hyp_Link_Address] = 'Masters/VehicleClassEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '247'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '248')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Make Details',
      [Hyp_Link_Address] = 'Masters/VehicleMaster',
      [IsExists] = 'Y'
  WHERE [menuid] = '248'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '249')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Vehicle Model Details',
      [Hyp_Link_Address] = 'Masters/VehicleMaster',
      [IsExists] = 'Y'
  WHERE [menuid] = '249'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '250')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Insurance Policy Details',
      [Hyp_Link_Address] = 'InsuranceMaster/InsurancePolicyMasterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '250'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '251')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'GST Setting Details',
      [Hyp_Link_Address] = 'InsuranceMaster/InsurancePolicyMasterEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '251'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '252')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Bus Captain Listing Details',
      [Hyp_Link_Address] = 'Masters/VehicleBusCaptainEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '252'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '253')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Organization Country Details',
      [Hyp_Link_Address] = 'CountryMaster/OrgCountryEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '253'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '254')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'Claim Officer Duty Details',
      [Hyp_Link_Address] = 'Masters/ClaimOfficerDutyEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '254'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '255')
BEGIN
  UPDATE [MNT_Menus]
  SET [DisplayTitle] = 'LOU Rate Details',
      [Hyp_Link_Address] = 'Masters/LOUEditor',
      [IsExists] = 'Y'
  WHERE [menuid] = '255'
END