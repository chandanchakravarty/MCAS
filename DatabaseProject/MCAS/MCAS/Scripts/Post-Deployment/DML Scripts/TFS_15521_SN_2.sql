IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=303)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimLogRequest',ProductName='CLM_LogRequest' where MenuId=303
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=133)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimNotesEditor',ProductName='CLM_Notes' where MenuId=133
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=134)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.TaskEditor',ProductName='CLM_ClaimTask' where MenuId=134
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=135)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAttachmentsEditor',ProductName='MNT_AttachmentList' where MenuId=135
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=136)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor',ProductName='TODODIARYLISTs' where MenuId=136
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=283)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.DiaryTaskEditor',ProductName='Claim_ReAssignmentDairy' where MenuId=283
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=132)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ServiceProvider',ProductName='CLM_ServiceProvider' where MenuId=132
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId in(206,207,208))
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimAccident',ProductName='ClaimAccidentDetails' where MenuId in(206,207,208) 
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=131)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimEditor',ProductName='CLM_Claims' where MenuId =131
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=294)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimRecoveryProcessing.ClaimRecoveryEditor',ProductName='CLM_ClaimRecovery' where MenuId =294
END
IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=137)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimReserve',ProductName='CLM_ReserveDetails' where MenuId =137
END

IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=138)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimMandate',ProductName='CLM_MandateDetails' where MenuId =138
END
IF EXISTS (SELECT 1 FROM [MNT_Menus]  WHERE  MenuId=139)
BEGIN
update MNT_Menus set VirtualSource='MCAS.Web.Objects.Resources.ClaimProcessing.ClaimInfoPayment',ProductName='CLM_PaymentDetails' where MenuId =139
END
------ Insert Record In SVC_TRANSACTION_LOG_FIELDS
  
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS  WHERE  FIELD_NAME='Hospital_Id' and TABLE_NAME='MNT_Hospital' and VALUE_ID='Id' and VALUE_DESC1='HospitalName')
BEGIN
Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
            values('Hospital_Id','MNT_Hospital','Id','HospitalName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS  WHERE  FIELD_NAME='AttachEntityType' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc')
BEGIN
Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
            values('AttachEntityType','MNT_Lookups','Lookupvalue','Lookupdesc') 
END
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS  WHERE  FIELD_NAME='Interchange' and TABLE_NAME='MNT_InterChange' and VALUE_ID='Id' and VALUE_DESC1='InterchangeName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('Interchange','MNT_InterChange','Id','InterchangeName') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='OperatingHours' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='OperatingHours'and VALUE_ID1='Category')
BEGIN            
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('OperatingHours','MNT_Lookups','Lookupvalue','Lookupdesc','OperatingHours','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='FinalLiability' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='InvestigationResult' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('FinalLiability','MNT_Lookups','Lookupvalue','Lookupdesc','InvestigationResult','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='InvestigationResult' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc')
BEGIN 
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('InvestigationResult','MNT_Lookups','Lookupvalue','Lookupdesc') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='PromtDetails' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='TASKTYPE' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('PromtDetails','MNT_Lookups','Lookupvalue','Lookupdesc','TASKTYPE','Category') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ClaimsOfficer' and TABLE_NAME='MNT_Lookups' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN            
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ClaimsOfficer','MNT_Users','SNo','UserDispName')
END 

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ListTypeID' and TABLE_NAME='TODODIARYLISTTYPES' and VALUE_ID='TYPEID' and VALUE_DESC1='TYPEDESC')
BEGIN                      
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ListTypeID','TODODIARYLISTTYPES','TYPEID','TYPEDESC')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ToUserId' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ToUserId','MNT_Users','SNo','UserDispName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='FromUserId' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN            
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('FromUserId','MNT_Users','SNo','UserDispName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='EscalationTo' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('EscalationTo','MNT_Users','SNo','UserDispName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ReAssignTo' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ReAssignTo','MNT_Users','SNo','UserDispName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ClaimantNames' and TABLE_NAME='CLM_Claims' and VALUE_ID='ClaimID')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ClaimantNames','CLM_Claims','ClaimID','case when ClaimType=1 then '''+'OD'+'''  
                                                 when ClaimType=2 then '''+'PD'+'''  
                                                 when ClaimType=3 then '''+'BI'+'''   
                                                 End  
                                                 +substring(ClaimRecordNo,CHARINDEX(''-'',ClaimRecordNo)+1,len(ClaimRecordNo))
                                                 +''/''+replace(ClaimantName,'' '','''')'+'as ClaimantName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS  WHERE  FIELD_NAME='ClaimantName' and TABLE_NAME='CLM_Claims' and VALUE_ID='ClaimID')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('ClaimantName','CLM_Claims','ClaimID','case when ClaimType=1 then '''+'OD'+'''  
                                                 when ClaimType=2 then '''+'PD'+'''  
                                                 when ClaimType=3 then '''+'BI'+'''   
                                                 End  
                                                 +substring(ClaimRecordNo,CHARINDEX(''-'',ClaimRecordNo)+1,len(ClaimRecordNo))
                                                 +''/''+replace(ClaimantName,'' '','''')'+'as ClaimantName')
 END 
  
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='ClaimTypeId' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='ClaimType' and VALUE_ID1='Category')
BEGIN  
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('ClaimTypeId','MNT_Lookups','Lookupvalue','Lookupdesc','ClaimType','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
WHERE  FIELD_NAME='PartyTypeId' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='PartyTypeList' and VALUE_ID1='Category' )
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('PartyTypeId','MNT_Lookups','Lookupvalue','Lookupdesc','PartyTypeList','Category') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS 
WHERE  FIELD_NAME='StatusId' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='StatusList' and VALUE_ID1='Category')
BEGIN          
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('StatusId','MNT_Lookups','Lookupvalue','Lookupdesc','StatusList','Category') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
WHERE  FIELD_NAME='claimantNameId' and TABLE_NAME='CLM_Claims' and VALUE_ID='ClaimID' and VALUE_DESC1='ClaimantName')
BEGIN 
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('claimantNameId','CLM_Claims','ClaimID','ClaimantName')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
  WHERE  FIELD_NAME='CountryId' and TABLE_NAME='MNT_Country' and VALUE_ID='CountryShortCode' and VALUE_DESC1='CountryName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('CountryId','MNT_Country','CountryShortCode','CountryName') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS 
 WHERE  FIELD_NAME='AccidentCause' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='AccidentCause' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('AccidentCause','MNT_Lookups','Lookupvalue','Lookupdesc','AccidentCause','Category')
END 

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS 
 WHERE  FIELD_NAME='CaseStatus' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='CaseStatus' and VALUE_ID1='Category')
BEGIN      
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('CaseStatus','MNT_Lookups','Lookupvalue','Lookupdesc','CaseStatus','Category')  
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
  WHERE  FIELD_NAME='CaseCategory' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='CaseCategory' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('CaseCategory','MNT_Lookups','Lookupvalue','Lookupdesc','CaseCategory','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
  WHERE  FIELD_NAME='ClaimantStatus' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='ClaimantStatus' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('ClaimantStatus','MNT_Lookups','Lookupvalue','Lookupdesc','ClaimantStatus','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS
  WHERE  FIELD_NAME='MP' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='MP' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('MP','MNT_Lookups','Lookupvalue','Lookupdesc','MP','Category')
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='RecoverFrom' and TABLE_NAME='MNT_Cedant' and VALUE_ID='CedantId' and VALUE_DESC1='CedantName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('RecoverFrom','MNT_Cedant','CedantId','CedantName') 
END
---
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='AssignedTo' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('AssignedTo','MNT_Users','SNo','UserDispName') 
END

IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='InvestigationResult' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='InvestigationResult' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('InvestigationResult','MNT_Lookups','Lookupvalue','Lookupdesc','InvestigationResult','Category')
END
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='InformSafetytoreviewfindings' and TABLE_NAME='MNT_Lookups' and VALUE_ID='Lookupvalue' and VALUE_DESC1='Lookupdesc' and VALUE_DESC2='InformSafetytoreviewfindings' and VALUE_ID1='Category')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1,VALUE_DESC2,VALUE_ID1)
 values('InformSafetytoreviewfindings','MNT_Lookups','Lookupvalue','Lookupdesc','InformSafetytoreviewfindings','Category')
END
 
IF NOT EXISTS (SELECT 1 FROM SVC_TRANSACTION_LOG_FIELDS WHERE  FIELD_NAME='SupervisorAssignto' and TABLE_NAME='MNT_Users' and VALUE_ID='SNo' and VALUE_DESC1='UserDispName')
BEGIN
 Insert Into SVC_TRANSACTION_LOG_FIELDS(FIELD_NAME,TABLE_NAME,VALUE_ID,VALUE_DESC1)
 values('SupervisorAssignto','MNT_Users','SNo','UserDispName') 
END




