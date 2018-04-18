CREATE Procedure [dbo].[Proc_UnLinkReportedUnReportedClaim]
(
	@ReportedAccidentClaimid int
--@UnReportedAccidentClaimid int
)
AS
begin
-- By Pravesh K Chandel-
--UnLinking  of Reported Claim and Un reported Claim.

declare @mXml xml
DECLARE @handle INT  
DECLARE @PrepareXmlStatus INT  

BEGIN TRANSACTION;
BEGIN TRY 
--set @AccidentClaimId=220
	DELETE FROM CLM_PaymentDetails WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_PaymentSummary WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_MandateDetails WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_MandateSummary WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_ReserveDetails WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_ReserveSummary WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM MNT_AttachmentList WHERE AccidentId=@ReportedAccidentClaimid
	DELETE FROM TODODIARYLIST WHERE AccidentId=@ReportedAccidentClaimid
	DELETE FROM Claim_ReAssignmentDairy WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_ClaimTask WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM CLM_Notes WHERE AccidentId=@ReportedAccidentClaimid
	DELETE FROM CLM_ServiceProvider WHERE AccidentId=@ReportedAccidentClaimid
	DELETE FROM CLM_Claims WHERE AccidentClaimId=@ReportedAccidentClaimid
	DELETE FROM ClaimAccidentDetails WHERE AccidentClaimId=@ReportedAccidentClaimid

 
--Table ClaimAccidentDetails
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='ClaimAccidentDetails'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT ClaimAccidentDetails ON
	INSERT INTO  ClaimAccidentDetails(AccidentClaimId,PolicyId,IPNo,ClaimNo,BusServiceNo,VehicleNo,AccidentDate,AccidentTime,ReportedDate,Facts,Damages,DateofFinding,
	InvestStatus,Results,BOIResults,FinalLiability,DutyIO,Make,ModelNo,DriverEmployeeNo,DriverName,DriverNRICNo,DriverMobileNo,InitialEstimate,
	InsurerClaim,MandateReqd,Organization,AccidentImage,LossTypeCode,LossNatureCode,TPClaimentStatus,TimePeriod,BusCaptainFault,ODAssignmentTranId,
	TPAssignmentTranId,TempClaimNo,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate,IsRecoveryOD,AccidentLoc,ODStatus,IsRecoveryBI,IsComplete,
	OperatingHours,IsReported,ReportedRefId,DateJoined,DateResigned,Interchange,UploadReportNo,IsReadOnly,LinkedAccidentClaimId,InitialLiability,
	CollisionType)
	select *
		from(
		SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
		with(
		[AccidentClaimId] [int],
		[PolicyId] [int] ,
		[IPNo] [nvarchar](10) ,
		[ClaimNo] [nvarchar](100) ,
		[BusServiceNo] [nvarchar](8) ,
		[VehicleNo] [nvarchar](10) ,
		[AccidentDate] [datetime]  ,
		[AccidentTime] [nvarchar](20)  ,
		[ReportedDate] [datetime]  ,
		[Facts] [nvarchar](max) ,
		[Damages] [nvarchar](500) ,
		[DateofFinding] [datetime] ,
		[InvestStatus] [nvarchar](100) ,
		[Results] [nvarchar](100) ,
		[BOIResults] [nvarchar](100) ,
		[FinalLiability] [int] ,
		[DutyIO] [nvarchar](250) ,
		[Make] [nvarchar](50) ,
		[ModelNo] [nvarchar](50) ,
		[DriverEmployeeNo] [nvarchar](50) ,
		[DriverName] [nvarchar](200) ,
		[DriverNRICNo] [nvarchar](20) ,
		[DriverMobileNo] [numeric](18, 0) ,
		[InitialEstimate] [nvarchar](100) ,
		[InsurerClaim] [nvarchar](100) ,
		[MandateReqd] [nvarchar](100) ,
		[Organization] [int] ,
		[AccidentImage] [nvarchar](100) ,
		[LossTypeCode] [nvarchar](10) ,
		[LossNatureCode] [nvarchar](10) ,
		[TPClaimentStatus] [varchar](1) ,
		[TimePeriod] [int] ,
		[BusCaptainFault] [varchar](1) ,
		[ODAssignmentTranId] [nvarchar](200) ,
		[TPAssignmentTranId] [nvarchar](200) ,
		[TempClaimNo] [nvarchar](100) ,
		[CreatedDate] [datetime] ,
		[CreatedBy] [nvarchar](25) ,
		[ModifiedBy] [nvarchar](25) ,
		[ModifiedDate] [datetime] ,
		[IsRecoveryOD] [varchar](1) ,
		[AccidentLoc] [nvarchar](250) ,
		[ODStatus] [varchar](1) ,
		[IsRecoveryBI] [varchar](1) ,
		[IsComplete] [int] ,
		[OperatingHours] [int] ,
		[IsReported] [bit] ,
		[ReportedRefId] [int] ,
		[DateJoined] [datetime] ,
		[DateResigned] [datetime] ,
		[Interchange] [int] ,
		[UploadReportNo] [nvarchar](50) ,
		[IsReadOnly] [bit] ,
		[LinkedAccidentClaimId] [int],
		[InitialLiability] [nvarchar](40),
		[CollisionType] [nvarchar](500)
		)
	  )tmp
	SET IDENTITY_INSERT ClaimAccidentDetails OFF	
	EXEC sp_xml_removedocument @handle 

--Table CLM_Claims
--SELECT * FROM CLM_Claims where AccidentClaimId =184	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_Claims'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_Claims ON
	INSERT INTO CLM_Claims(ClaimID,AccidentClaimId,PolicyId,ClaimType,ClaimDate,ClaimOfficer,FinalSettleDate,ClaimStatus,TimeBarDate,
	CaseCategory,CaseStatus,DriversLiability,ClaimAmount,PaidDate,BalanceLOG,LOGAmount,LOURate,LOUDays,ReportSentInsurer,ReferredInsurers,
	InformInsurer,ExcessRecoveredDate,WritIssuedDate,WritNo,SenstiveCase,MPLetter,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,
	ReserveCurr,ReserveExRate,ReserveAmt,ExpensesCurr,ExpensesExRate,TotalReserve,AdjusterAppointed,LawyerAppointed,SurveyorAppointed,
	DepotWorkshop,ExpensesAmt,PayableTo,ClaimAmountCurr,ClaimAmtPayout,ClaimAmtPayoutExRate,ExpensesAmount,ReserveAmount,RecordDeletionDate,
	RecordDeletionReason,ClaimantName,ClaimantNRICPPNO,ClaimantDOB,ClaimantGender,ClaimantType,ClaimantAddress1,ClaimantAddress2,
	ClaimantAddress3,PostalCode,ClaimantContactNo,ClaimantEmail,VehicleRegnNo,VehicleMake,VehicleModel,Isclaimantaninfant,InfantName,
	InfantDOB,InfantGender,ClaimRecordNo,ClaimsOfficer,DriverLiablity,AccidentCause,ClaimantStatus,SensitiveCase,ReopenedDate,
	RecordReopenedReason,RecordCancellationDate,RecordCancellationReason,MP,Constituency,DateOfMpLetter,SeverityReferenceNo,
	ReportSentToInsurer,ReferredToInsurers,InformedInsurerOfSettlement,ReferredToInsurersB,DateReferredToInsurersB,AccidentId,ConfirmedAmount)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
		[ClaimID] [int],
		[AccidentClaimId] [int],
		[PolicyId] [int],
		[ClaimType] [int],
		[ClaimDate] [datetime],
		[ClaimOfficer] [varchar](20),
		[FinalSettleDate] [datetime],
		[ClaimStatus] [varchar](10),
		[TimeBarDate] [datetime],
		[CaseCategory] [nvarchar](30),
		[CaseStatus] [nvarchar](50),
		[DriversLiability] [numeric](18, 0),
		[ClaimAmount] [decimal](18, 9),
		[PaidDate] [datetime],
		[BalanceLOG] [numeric](18, 0),
		[LOGAmount] [numeric](18, 0),
		[LOURate] [numeric](18, 0),
		[LOUDays] [numeric](18, 0),
		[ReportSentInsurer] [datetime],
		[ReferredInsurers] [datetime],
		[InformInsurer] [datetime],
		[ExcessRecoveredDate] [datetime],
		[WritIssuedDate] [datetime],
		[WritNo] [nvarchar](50),
		[SenstiveCase] [datetime],
		[MPLetter] [datetime],
		[IsActive] [varchar](1),
		[CreatedBy] [varchar](50),
		[CreatedDate] [datetime],
		[ModifiedBy] [varchar](50),
		[ModifiedDate] [datetime],
		[ReserveCurr] [varchar](20),
		[ReserveExRate] [decimal](18, 9),
		[ReserveAmt] [decimal](18, 9),
		[ExpensesCurr] [varchar](20),
		[ExpensesExRate] [decimal](18, 9),
		[TotalReserve] [decimal](18, 9),
		[AdjusterAppointed] [int],
		[LawyerAppointed] [int],
		[SurveyorAppointed] [int],
		[DepotWorkshop] [int],
		[ExpensesAmt] [decimal](18, 9),
		[PayableTo] [int],
		[ClaimAmountCurr] [varchar](20),
		[ClaimAmtPayout] [decimal](18, 9),
		[ClaimAmtPayoutExRate] [decimal](18, 9),
		[ExpensesAmount] [decimal](18, 9),
		[ReserveAmount] [decimal](18, 9),
		[RecordDeletionDate] [datetime],
		[RecordDeletionReason] [nvarchar](100),
		[ClaimantName] [nvarchar](250),
		[ClaimantNRICPPNO] [nvarchar](250),
		[ClaimantDOB] [datetime],
		[ClaimantGender] [nvarchar](10),
		[ClaimantType] [int],
		[ClaimantAddress1] [nvarchar](250),
		[ClaimantAddress2] [nvarchar](250),
		[ClaimantAddress3] [nvarchar](250),
		[PostalCode] [nvarchar](250),
		[ClaimantContactNo] [nvarchar](20),
		[ClaimantEmail] [nvarchar](100),
		[VehicleRegnNo] [nvarchar](8),
		[VehicleMake] [nvarchar](150),
		[VehicleModel] [nvarchar](150),
		[Isclaimantaninfant] [nvarchar](5),
		[InfantName] [nvarchar](250),
		[InfantDOB] [datetime],
		[InfantGender] [nvarchar](10),
		[ClaimRecordNo] [nvarchar](50),
		[ClaimsOfficer] [int],
		[DriverLiablity] [nvarchar](250),
		[AccidentCause] [nvarchar](100),
		[ClaimantStatus] [nvarchar](10),
		[SensitiveCase] [datetime],
		[ReopenedDate] [datetime],
		[RecordReopenedReason] [nvarchar](150),
		[RecordCancellationDate] [datetime],
		[RecordCancellationReason] [nvarchar](150),
		[MP] [nvarchar](250),
		[Constituency] [nvarchar](250),
		[DateOfMpLetter] [datetime],
		[SeverityReferenceNo] [nvarchar](50),
		[ReportSentToInsurer] [datetime],
		[ReferredToInsurers] [nvarchar](250),
		[InformedInsurerOfSettlement] [nvarchar](250),
		[ReferredToInsurersB] [bit],
		[DateReferredToInsurersB] [datetime],
		[AccidentId] [int],
		[ConfirmedAmount] [numeric](18, 0)
		)
	)tmp

	SET IDENTITY_INSERT CLM_Claims OFF
	EXEC sp_xml_removedocument @handle 
 
--Table CLM_ServiceProvider
--SELECT * FROM CLM_ServiceProvider	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_ServiceProvider'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_ServiceProvider ON
	INSERT INTO CLM_ServiceProvider(ServiceProviderId,ClaimTypeId,ClaimantNameId,PartyTypeId,ServiceProviderTypeId,CompanyNameId,
	AppointedDate,Address1,Address2,Address3,City,State,CountryId,PostalCode,Reference,ContactPersonName,EmailAddress,OfficeNo,Mobile,
	Fax,ContactPersonName2nd,EmailAddress2nd,OfficeNo2nd,Mobile2nd,Fax2nd,StatusId,Remarks,Createdby,Createddate,Modifiedby,Modifieddate,
	AccidentId,PolicyId,IsActive,ClaimRecordNo)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[ServiceProviderId] [int],
			[ClaimTypeId] [int],
			[ClaimantNameId] [int],
			[PartyTypeId] [int],
			[ServiceProviderTypeId] [int],
			[CompanyNameId] [int],
			[AppointedDate] [datetime],
			[Address1] [nvarchar](100),
			[Address2] [nvarchar](100),
			[Address3] [nvarchar](100),
			[City] [nvarchar](100),
			[State] [nvarchar](100),
			[CountryId] [nvarchar](5),
			[PostalCode] [nvarchar](30),
			[Reference] [nvarchar](150),
			[ContactPersonName] [nvarchar](250),
			[EmailAddress] [nvarchar](100),
			[OfficeNo] [nvarchar](30),
			[Mobile] [nvarchar](30),
			[Fax] [nvarchar](30),
			[ContactPersonName2nd] [nvarchar](250),
			[EmailAddress2nd] [nvarchar](100),
			[OfficeNo2nd] [nvarchar](30),
			[Mobile2nd] [nvarchar](30),
			[Fax2nd] [nvarchar](30),
			[StatusId] [int],
			[Remarks] [nvarchar](800),
			[Createdby] [nvarchar](max),
			[Createddate] [datetime],
			[Modifiedby] [nvarchar](max),
			[Modifieddate] [datetime],
			[AccidentId] [int],
			[PolicyId] [int],
			[IsActive] [char](1),
			[ClaimRecordNo] [nvarchar](50)
		)
	)tmp

	SET IDENTITY_INSERT CLM_ServiceProvider OFF
	EXEC sp_xml_removedocument @handle 

--Table from CLM_Notes
--SELECT * FROM CLM_Notes

		
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_Notes'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_Notes ON
	INSERT INTO CLM_Notes(NoteId,PolicyId,ClaimId,NoteCode,NoteDate,NoteTime,ImageCode,ImageId,NotesDescription,ModifiedBy,
	CreatedDate,CreatedBy,ModifiedDate,URL_PATH,AccidentId,ClaimantNames)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[NoteId] [int],
			[PolicyId] [int],
			[ClaimId] [int],
			[NoteCode] [nvarchar](100),
			[NoteDate] [datetime],
			[NoteTime] [nvarchar](20),
			[ImageCode] [nvarchar](50),
			[ImageId] [nvarchar](200),
			[NotesDescription] [nvarchar](max),
			[ModifiedBy] [nvarchar](25),
			[CreatedDate] [datetime],
			[CreatedBy] [nvarchar](25),
			[ModifiedDate] [datetime],
			[URL_PATH] [nvarchar](200),
			[AccidentId] [int],
			[ClaimantNames] [nvarchar](255)
		)
	)tmp

	SET IDENTITY_INSERT CLM_Notes OFF
	EXEC sp_xml_removedocument @handle 
	
--Table CLM_ClaimTask
--SELECT * FROM CLM_ClaimTask	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_ClaimTask'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_ClaimTask ON
	INSERT INTO CLM_ClaimTask(Id,TaskNo,ClaimID,ActionDue,CloseDate,PromtDetails,isApprove,ApproveDate,ApproveBy,ModifiedDate,
	CreatedDate,CreatedBy,ModifiedBy,Remarks,AccidentId,AccidentClaimId,ClaimantNames,ClaimsOfficer)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[Id] [int],
			[TaskNo] [int],
			[ClaimID] [int],
			[ActionDue] [datetime],
			[CloseDate] [datetime],
			[PromtDetails] [nvarchar](100),
			[isApprove] [int],
			[ApproveDate] [datetime],
			[ApproveBy] [nvarchar](25),
			[ModifiedDate] [datetime],
			[CreatedDate] [datetime],
			[CreatedBy] [nvarchar](25),
			[ModifiedBy] [nvarchar](25),
			[Remarks] [nvarchar](500),
			[AccidentId] [int],
			[AccidentClaimId] [int],
			[ClaimantNames] [nvarchar](255),
			[ClaimsOfficer] [int]
		)
	)tmp

	SET IDENTITY_INSERT CLM_ClaimTask OFF
	EXEC sp_xml_removedocument @handle 

--Table from Claim_ReAssignmentDairy
--SELECT * FROM Claim_ReAssignmentDairy	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='Claim_ReAssignmentDairy'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT Claim_ReAssignmentDairy ON
	INSERT INTO Claim_ReAssignmentDairy(Id,DairyId,TypeOfAssignment,ReAssignTo,DairyFromUser,ReAssignDateFrom,ReAssignDateTo,Remark,
	EmailId,Status,IsActive,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate,ClaimId,AccidentClaimId)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[Id] [int],
			[DairyId] [nvarchar](100),
			[TypeOfAssignment] [char](1),
			[ReAssignTo] [nvarchar](50),
			[DairyFromUser] [nvarchar](50),
			[ReAssignDateFrom] [datetime],
			[ReAssignDateTo] [datetime],
			[Remark] [nvarchar](4000),
			[EmailId] [nvarchar](50),
			[Status] [char](1),
			[IsActive] [char](1),
			[CreatedDate] [datetime],
			[CreatedBy] [nvarchar](25),
			[ModifiedBy] [nvarchar](25),
			[ModifiedDate] [datetime],
			[ClaimId] [int],
			[AccidentClaimId] [int]
		)
	)tmp

	SET IDENTITY_INSERT Claim_ReAssignmentDairy OFF
	EXEC sp_xml_removedocument @handle 

--Table  TODODIARYLIST 
--SELECT * FROM TODODIARYLIST
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='TODODIARYLIST'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT TODODIARYLIST ON
	INSERT INTO TODODIARYLIST(LISTID,RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,
	PRIORITY,TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,CUSTOMER_ID,
	APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,RULES_VERIFIED,PROCESS_ROW_ID,MODULE_ID,INSURERNAME,POLICYNO,CLAIMNO,ExpectedPaymentDate,
	ReminderBeforeCompletion,Escalation,EscalationTo,EmailBody,ModifiedBy,CreatedDate,CreatedBy,ModifiedDate,UserId,ReassignedDiary,
	ReassignedDiaryDate,AccidentId,MovementType,ParentId,IsActive)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[LISTID] [bigint],
			[RECBYSYSTEM] [nchar](1),
			[RECDATE] [datetime],
			[FOLLOWUPDATE] [datetime],
			[LISTTYPEID] [int],
			[POLICYBROKERID] [numeric](8, 0),
			[SUBJECTLINE] [nvarchar](512),
			[LISTOPEN] [nchar](1),
			[SYSTEMFOLLOWUPID] [numeric](18, 0),
			[PRIORITY] [nchar](1),
			[TOUSERID] [numeric](18, 0),
			[FROMUSERID] [numeric](18, 0),
			[STARTTIME] [datetime],
			[ENDTIME] [datetime],
			[NOTE] [nvarchar](2000),
			[PROPOSALVERSION] [numeric](18, 0),
			[QUOTEID] [numeric](18, 0),
			[CLAIMID] [int],
			[CLAIMMOVEMENTID] [numeric](18, 0),
			[TOENTITYID] [numeric](10, 0),
			[FROMENTITYID] [int],
			[CUSTOMER_ID] [int],
			[APP_ID] [int],
			[APP_VERSION_ID] [smallint],
			[POLICY_ID] [int],
			[POLICY_VERSION_ID] [smallint],
			[RULES_VERIFIED] [smallint],
			[PROCESS_ROW_ID] [int],
			[MODULE_ID] [int],
			[INSURERNAME] [varchar](50),
			[POLICYNO] [varchar](50),
			[CLAIMNO] [varchar](50),
			[ExpectedPaymentDate] [datetime],
			[ReminderBeforeCompletion] [int],
			[Escalation] [varchar](1),
			[EscalationTo] [nvarchar](100),
			[EmailBody] [nvarchar](max),
			[ModifiedBy] [nvarchar](25),
			[CreatedDate] [datetime],
			[CreatedBy] [nvarchar](25),
			[ModifiedDate] [datetime],
			[UserId] [int],
			[ReassignedDiary] [nvarchar](4),
			[ReassignedDiaryDate] [datetime],
			[AccidentId] [int],
			[MovementType] [nvarchar](50),
			[ParentId] [int],
			[IsActive] [char](1)
		)
	)tmp

	SET IDENTITY_INSERT TODODIARYLIST OFF
	EXEC sp_xml_removedocument @handle 

--Table MNT_AttachmentList
--SELECT * FROM MNT_AttachmentList
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='MNT_AttachmentList'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT MNT_AttachmentList ON
	INSERT INTO MNT_AttachmentList(AttachId,AttachLoc,AttachEntId,AttachFileName,AttachDateTime,AttachFileDesc,AttachPolicyId,ClaimantName,
	AttachFileType,AttachEntityType,IsActive,AttachType,FilePath,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ClaimID,AccidentId)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[AttachId] [int],
			[AttachLoc] [nchar](2),
			[AttachEntId] [int],
			[AttachFileName] [nvarchar](255),
			[AttachDateTime] [datetime],
			[AttachFileDesc] [nvarchar](255),
			[AttachPolicyId] [int],
			[ClaimantName] [nvarchar](255),
			[AttachFileType] [nvarchar](5),
			[AttachEntityType] [varchar](25),
			[IsActive] [char](1),
			[AttachType] [int],
			[FilePath] [nvarchar](200),
			[CreatedBy] [varchar](50),
			[CreatedDate] [datetime],
			[ModifiedBy] [varchar](50),
			[ModifiedDate] [datetime],
			[ClaimID] [int],
			[AccidentId] [int]
		)
	)tmp

	SET IDENTITY_INSERT MNT_AttachmentList OFF
	EXEC sp_xml_removedocument @handle 

--Table CLM_ReserveSummary
--SELECT * FROM CLM_ReserveSummary	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_ReserveSummary'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_ReserveSummary ON
	INSERT INTO CLM_ReserveSummary(ReserveId,AccidentClaimId,ClaimID,ClaimType,MovementType,InitialReserve,PreReserve,MovementReserve,
	CurrentReserve,PaymentId,Createdby,Createddate,Modifiedby,Modifieddate,IsActive)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[ReserveId] [int],
			[AccidentClaimId] [int],
			[ClaimID] [int],
			[ClaimType] [int],
			[MovementType] [nvarchar](2),
			[InitialReserve] [numeric](18, 2),
			[PreReserve] [numeric](18, 2),
			[MovementReserve] [numeric](18, 2),
			[CurrentReserve] [numeric](18, 2),
			[PaymentId] [int],
			[Createdby] [nvarchar](max),
			[Createddate] [datetime],
			[Modifiedby] [nvarchar](max),
			[Modifieddate] [datetime],
			[IsActive] [char](1)
		)
	)tmp

	SET IDENTITY_INSERT CLM_ReserveSummary OFF
	EXEC sp_xml_removedocument @handle 

--Table CLM_ReserveDetails
--SELECT * FROM CLM_ReserveDetails	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_ReserveDetails'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_ReserveDetails ON
	INSERT INTO CLM_ReserveDetails(ReserveDetailID,ReserveId,CmpCode,InitialReserve,PreReserve,MovementReserve,CurrentReserve,PaymentId,
	InitialNoofdays,MovementNoofdays,CurrentNoofdays,InitialRateperday,MovementlRateperday,CurrentRateperday,Createdby,Createddate,
	Modifiedby,Modifieddate,IsActive,AccidentClaimId,ClaimID,MovementType)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[ReserveDetailID] [int],
			[ReserveId] [int],
			[CmpCode] [nchar](15),
			[InitialReserve] [numeric](18, 2),
			[PreReserve] [numeric](18, 2),
			[MovementReserve] [numeric](18, 2),
			[CurrentReserve] [numeric](18, 2),
			[PaymentId] [int],
			[InitialNoofdays] [nvarchar](10),
			[MovementNoofdays] [nvarchar](10),
			[CurrentNoofdays] [nvarchar](10),
			[InitialRateperday] [nvarchar](10),
			[MovementlRateperday] [nvarchar](10),
			[CurrentRateperday] [nvarchar](10),
			[Createdby] [nvarchar](max),
			[Createddate] [datetime],
			[Modifiedby] [nvarchar](max),
			[Modifieddate] [datetime],
			[IsActive] [char](1),
			[AccidentClaimId] [int],
			[ClaimID] [int],
			[MovementType] [char](1)
		)
	)tmp

	SET IDENTITY_INSERT CLM_ReserveDetails OFF
	EXEC sp_xml_removedocument @handle


--Table CLM_MandateSummary
--SELECT * FROM CLM_MandateSummary	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_MandateSummary'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_MandateSummary ON
	INSERT INTO CLM_MandateSummary(MandateId,AccidentClaimId,ReserveId,ClaimID,ClaimType,MovementType,AssignedTo,InvestigationResult,
	Scenario,Evidence,RelatedFacts,COAssessment,SupervisorAssignto,ApproveRecommedations,SupervisorRemarks,PreMandate,MovementMandate,
	CurrentMandate,PaymentId,InformSafetytoreviewfindings,MandateRecordNo,Createdby,Createddate,Modifiedby,Modifieddate,IsActive,
	PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[MandateId] [int],
			[AccidentClaimId] [int],
			[ReserveId] [int],
			[ClaimID] [int],
			[ClaimType] [int],
			[MovementType] [nvarchar](2),
			[AssignedTo] [int],
			[InvestigationResult] [int],
			[Scenario] [nvarchar](max),
			[Evidence] [nvarchar](max),
			[RelatedFacts] [nvarchar](max),
			[COAssessment] [nvarchar](max),
			[SupervisorAssignto] [int],
			[ApproveRecommedations] [nvarchar](2),
			[SupervisorRemarks] [nvarchar](max),
			[PreMandate] [numeric](18, 2),
			[MovementMandate] [numeric](18, 2),
			[CurrentMandate] [numeric](18, 2),
			[PaymentId] [int],
			[InformSafetytoreviewfindings] [nvarchar](10),
			[MandateRecordNo] [nvarchar](100),
			[Createdby] [nvarchar](max),
			[Createddate] [datetime],
			[Modifiedby] [nvarchar](max),
			[Modifieddate] [datetime],
			[IsActive] [char](1),
			[PreMandateSP] [numeric](18, 2),
			[MovementMandateSP] [numeric](18, 2),
			[CurrentMandateSP] [numeric](18, 2),
			[PreviousOffers] [numeric](18, 2),
			[TPCounterOffer] [numeric](18, 2)
		)
	)tmp

	SET IDENTITY_INSERT CLM_MandateSummary OFF
	EXEC sp_xml_removedocument @handle 

--Table CLM_MandateDetails
--SELECT * FROM CLM_MandateDetails	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_MandateDetails'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_MandateDetails ON
	INSERT INTO CLM_MandateDetails(MandateDetailId,MandateId,CmpCode,PreMandate,MovementMandate,CurrentMandate,PaymentId,Createdby,
	Createddate,Modifiedby,Modifieddate,IsActive,AccidentClaimId,ClaimID,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,
	TPCounterOffer,ReserveId,MovementType)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[MandateDetailId] [int],
			[MandateId] [int],
			[CmpCode] [nchar](15),
			[PreMandate] [numeric](18, 2),
			[MovementMandate] [numeric](18, 2),
			[CurrentMandate] [numeric](18, 2),
			[PaymentId] [int],
			[Createdby] [nvarchar](max),
			[Createddate] [datetime],
			[Modifiedby] [nvarchar](max),
			[Modifieddate] [datetime],
			[IsActive] [char](1),
			[AccidentClaimId] [int],
			[ClaimID] [int],
			[PreMandateSP] [numeric](18, 2),
			[MovementMandateSP] [numeric](18, 2),
			[CurrentMandateSP] [numeric](18, 2),
			[PreviousOffers] [numeric](18, 2),
			[TPCounterOffer] [numeric](18, 2),
			[ReserveId] [int],
			[MovementType] [char](1)
		)
	)tmp

	SET IDENTITY_INSERT CLM_MandateDetails OFF
	EXEC sp_xml_removedocument @handle 
	
--Table CLM_PaymentSummary
--SELECT * FROM CLM_PaymentSummary	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_PaymentSummary'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_PaymentSummary ON
	INSERT INTO CLM_PaymentSummary(PaymentId,AccidentClaimId,PolicyId,Payee,AssignedToSupervisor,TotalPaymentDue,TotalAmountMandate,
	Createddate,Modifieddate,PaymentRequestDate,PaymentDueDate,CreatedBy,Modifiedby,AssignedTo,ClaimantName,PaymentRecordNo,
	ClaimType,IsActive,ClaimID,Address,Address1,Address2,PostalCodes,CoRemarks,ApprovePayment,SupervisorRemarks,ApprovedDate,
	MovementType,MandateId,ReserveId)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
			[PaymentId] [int],
			[AccidentClaimId] [int],
			[PolicyId] [int],
			[Payee] [int],
			[AssignedToSupervisor] [nvarchar](max),
			[TotalPaymentDue] [numeric](18, 2),
			[TotalAmountMandate] [numeric](18, 2),
			[Createddate] [datetime],
			[Modifieddate] [datetime],
			[PaymentRequestDate] [datetime],
			[PaymentDueDate] [datetime],
			[CreatedBy] [nvarchar](100),
			[Modifiedby] [nvarchar](100),
			[AssignedTo] [nvarchar](max),
			[ClaimantName] [nvarchar](max),
			[PaymentRecordNo] [nvarchar](max),
			[ClaimType] [int],
			[IsActive] [char](1),
			[ClaimID] [int],
			[Address] [nvarchar](max),
			[Address1] [nvarchar](max),
			[Address2] [nvarchar](max),
			[PostalCodes] [nvarchar](max),
			[CoRemarks] [nvarchar](max),
			[ApprovePayment] [nvarchar](max),
			[SupervisorRemarks] [nvarchar](max),
			[ApprovedDate] [datetime],
			[MovementType] [nvarchar](50),
			[MandateId] [int],
			[ReserveId] [int]
		)
	)tmp

	SET IDENTITY_INSERT CLM_PaymentSummary OFF
	EXEC sp_xml_removedocument @handle 

--Table CLM_PaymentDetails
--SELECT * FROM CLM_PaymentDetails	
	
	select @mXml = Tabledata from CLM_ReportedClaimBackup where AccidentClaimId = @ReportedAccidentClaimid and tableName='CLM_PaymentDetails'
	EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

	SET IDENTITY_INSERT CLM_PaymentDetails ON
	INSERT INTO CLM_PaymentDetails(PaymentDetailID,CmpCode,TotalPaymentDue,TotalAmountMandate,Createdby,Createddate,Modifiedby,
	Modifieddate,IsActive,AccidentClaimId,ReserveId,MandateId,PaymentId,ClaimId)
	select *
	from(
	SELECT  * FROM    OPENXML(@handle, '/rows/row', 2)  
	with(
		[PaymentDetailID] [int],
		[CmpCode] [nchar](15),
		[TotalPaymentDue] [numeric](18, 2),
		[TotalAmountMandate] [numeric](18, 2),
		[Createdby] [nvarchar](max),
		[Createddate] [datetime],
		[Modifiedby] [nvarchar](max),
		[Modifieddate] [datetime],
		[IsActive] [char](1),
		[AccidentClaimId] [int],
		[ReserveId] [int],
		[MandateId] [int],
		[PaymentId] [int],
		[ClaimId] [int]
		)
	)tmp

	SET IDENTITY_INSERT CLM_PaymentDetails OFF
	EXEC sp_xml_removedocument @handle 
	
--Now Mark Unreported as Unread
	UPDATE ClaimAccidentDetails 
	SET IsReadOnly=null,LinkedAccidentClaimId=null 
	WHERE LinkedAccidentClaimId=@ReportedAccidentClaimid

END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
       -- SELECT 
       -- ERROR_NUMBER() AS ErrorNumber
       --,ERROR_MESSAGE() AS ErrorMessage;
END CATCH

IF @@TRANCOUNT > 0
BEGIN
    COMMIT TRANSACTION;
    --RETURN 1;
    select 1;    
END
ELSE
BEGIN
	--RETURN 0;
	select 0;
END
end


