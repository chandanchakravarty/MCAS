CREATE Procedure [dbo].[Proc_LinkReportedUnReportedClaim]
(
@ReportedAccidentClaimid int,
@UnReportedAccidentClaimid int
)
as
begin
-- By Pravesh K Chandel-
--Linking  of Reported Claim and Un reported Claim.
--Table ClaimAccidentDetails


BEGIN TRANSACTION;

EXEC Proc_CreateClaimBackup @ReportedAccidentClaimid

--select * from ClaimAccidentDetails where AccidentClaimId = @ReportedAccidentClaimid

BEGIN TRY 

UPDATE c 
	SET 	
	--c.IPNo= (CASE WHEN COALESCE(c.IPNo,'')='' THEN u.IPNo ELSE c.IPNo END),	
	--c.BusServiceNo= (CASE WHEN COALESCE(c.BusServiceNo,'')='' THEN u.BusServiceNo ELSE c.BusServiceNo END),
	--c.VehicleNo= (CASE WHEN COALESCE(c.VehicleNo,'')='' THEN u.VehicleNo ELSE c.VehicleNo END),
	--c.AccidentDate= (CASE WHEN COALESCE(c.AccidentDate,'')='' THEN u.AccidentDate ELSE c.AccidentDate END),
	--c.AccidentTime= (CASE WHEN COALESCE(c.AccidentTime,'')='' THEN u.AccidentTime ELSE c.AccidentTime END),
	--c.ReportedDate= (CASE WHEN COALESCE(c.ReportedDate,'')='' THEN u.ReportedDate ELSE c.ReportedDate END),
	c.Facts= (CASE WHEN COALESCE(c.Facts,'')='' THEN u.Facts ELSE c.Facts END),
	--c.Damages= (CASE WHEN COALESCE(c.Damages,'')='' THEN u.Damages ELSE c.Damages END),
	--c.DateofFinding= (CASE WHEN COALESCE(c.DateofFinding,'')='' THEN u.DateofFinding ELSE c.DateofFinding END),
	c.InvestStatus= (CASE WHEN COALESCE(c.InvestStatus,'')='' THEN u.InvestStatus ELSE c.InvestStatus END),
	c.Results= (CASE WHEN COALESCE(c.Results,'')='' THEN u.Results ELSE c.Results END),
	c.BOIResults= (CASE WHEN COALESCE(c.BOIResults,'')='' THEN u.BOIResults ELSE c.BOIResults END),
	--c.FinalLiability= (CASE WHEN COALESCE(c.FinalLiability,'')='' THEN u.FinalLiability ELSE c.FinalLiability END),
	--c.DutyIO= (CASE WHEN COALESCE(c.DutyIO,'')='' THEN u.DutyIO ELSE c.DutyIO END),
	--c.Make= (CASE WHEN COALESCE(c.Make,'')='' THEN u.Make ELSE c.Make END),
	--c.ModelNo= (CASE WHEN COALESCE(c.ModelNo,'')='' THEN u.ModelNo ELSE c.ModelNo END),
	--c.DriverEmployeeNo= (CASE WHEN COALESCE(c.DriverEmployeeNo,'')='' THEN u.DriverEmployeeNo ELSE c.DriverEmployeeNo END),
	--c.DriverName= (CASE WHEN COALESCE(c.DriverName,'')='' THEN u.DriverName ELSE c.DriverName END),
	--c.DriverNRICNo= (CASE WHEN COALESCE(c.DriverNRICNo,'')='' THEN u.DriverNRICNo ELSE c.DriverNRICNo END),
	--c.DriverMobileNo= (CASE WHEN COALESCE(c.DriverMobileNo,'')='' THEN u.DriverMobileNo ELSE c.DriverMobileNo END),
	c.InitialEstimate= (CASE WHEN COALESCE(c.InitialEstimate,'')='' THEN u.InitialEstimate ELSE c.InitialEstimate END),
	c.InsurerClaim= (CASE WHEN COALESCE(c.InsurerClaim,'')='' THEN u.InsurerClaim ELSE c.InsurerClaim END),
	c.MandateReqd= (CASE WHEN COALESCE(c.MandateReqd,'')='' THEN u.MandateReqd ELSE c.MandateReqd END),
	c.Organization= (CASE WHEN COALESCE(c.Organization,'')='' THEN u.Organization ELSE c.Organization END),
	c.AccidentImage= (CASE WHEN COALESCE(c.AccidentImage,'')='' THEN u.AccidentImage ELSE c.AccidentImage END),
	c.LossTypeCode= (CASE WHEN COALESCE(c.LossTypeCode,'')='' THEN u.LossTypeCode ELSE c.LossTypeCode END),
	c.LossNatureCode= (CASE WHEN COALESCE(c.LossNatureCode,'')='' THEN u.LossNatureCode ELSE c.LossNatureCode END),
	c.TPClaimentStatus= (CASE WHEN COALESCE(c.TPClaimentStatus,'')='' THEN u.TPClaimentStatus ELSE c.TPClaimentStatus END),
	c.TimePeriod= (CASE WHEN COALESCE(c.TimePeriod,'')='' THEN u.TimePeriod ELSE c.TimePeriod END),
	c.BusCaptainFault= (CASE WHEN COALESCE(c.BusCaptainFault,'')='' THEN u.BusCaptainFault ELSE c.BusCaptainFault END),
	c.ODAssignmentTranId= (CASE WHEN COALESCE(c.ODAssignmentTranId,'')='' THEN u.ODAssignmentTranId ELSE c.ODAssignmentTranId END),
	c.TPAssignmentTranId= (CASE WHEN COALESCE(c.TPAssignmentTranId,'')='' THEN u.TPAssignmentTranId ELSE c.TPAssignmentTranId END),
	c.TempClaimNo= (CASE WHEN COALESCE(c.TempClaimNo,'')='' THEN u.TempClaimNo ELSE c.TempClaimNo END),	
	c.ModifiedDate= GETDATE(),
	c.IsRecoveryOD= (CASE WHEN COALESCE(c.IsRecoveryOD,'')='' THEN u.IsRecoveryOD ELSE c.IsRecoveryOD END),
	--c.AccidentLoc= (CASE WHEN COALESCE(c.AccidentLoc,'')='' THEN u.AccidentLoc ELSE c.AccidentLoc END),
	c.ODStatus= (CASE WHEN COALESCE(c.ODStatus,'')='' THEN u.ODStatus ELSE c.ODStatus END),
	c.IsRecoveryBI= (CASE WHEN COALESCE(c.IsRecoveryBI,'')='' THEN u.IsRecoveryBI ELSE c.IsRecoveryBI END),	
	--c.OperatingHours= (CASE WHEN COALESCE(c.OperatingHours,'')='' THEN u.OperatingHours ELSE c.OperatingHours END),
	c.DateJoined= (CASE WHEN COALESCE(c.DateJoined,'')='' THEN u.DateJoined ELSE c.DateJoined END),
	c.DateResigned= (CASE WHEN COALESCE(c.DateResigned,'')='' THEN u.DateResigned ELSE c.DateResigned END),
	c.Interchange= (CASE WHEN COALESCE(c.Interchange,'')='' THEN u.Interchange ELSE c.Interchange END),
	c.InitialLiability=(CASE WHEN COALESCE(c.InitialLiability,'')='' THEN u.InitialLiability ELSE c.InitialLiability END),
	c.CollisionType=(CASE WHEN COALESCE(c.CollisionType,'')='' THEN u.CollisionType ELSE c.CollisionType END)
	FROM ClaimAccidentDetails c INNER JOIN ClaimAccidentDetails u
	on c.AccidentClaimId = @ReportedAccidentClaimid
	AND u.AccidentClaimId=@UnReportedAccidentClaimid

--Table CLM_Claims

--select * from CLM_Claims where AccidentClaimId = @ReportedAccidentClaimid

UPDATE a 
	SET			
		a.ClaimType= (CASE WHEN COALESCE(a.ClaimType,'')='' THEN b.ClaimType ELSE a.ClaimType END),
		a.ClaimDate= (CASE WHEN COALESCE(a.ClaimDate,'')='' THEN b.ClaimDate ELSE a.ClaimDate END),
		a.FinalSettleDate= (CASE WHEN COALESCE(a.FinalSettleDate,'')='' THEN b.FinalSettleDate ELSE a.FinalSettleDate END),
		a.ClaimStatus= (CASE WHEN COALESCE(a.ClaimStatus,'')='' THEN b.ClaimStatus ELSE a.ClaimStatus END),
		a.TimeBarDate= (CASE WHEN COALESCE(a.TimeBarDate,'')='' THEN b.TimeBarDate ELSE a.TimeBarDate END),
		a.CaseCategory= (CASE WHEN COALESCE(a.CaseCategory,'')='' THEN b.CaseCategory ELSE a.CaseCategory END),
		a.CaseStatus= (CASE WHEN COALESCE(a.CaseStatus,'')='' THEN b.CaseStatus ELSE a.CaseStatus END),
		a.ClaimAmount= (CASE WHEN COALESCE(a.ClaimAmount,'')='' THEN b.ClaimAmount ELSE a.ClaimAmount END),
		a.ExcessRecoveredDate= (CASE WHEN COALESCE(a.ExcessRecoveredDate,'')='' THEN b.ExcessRecoveredDate ELSE a.ExcessRecoveredDate END),
		a.WritIssuedDate= (CASE WHEN COALESCE(a.WritIssuedDate,'')='' THEN b.WritIssuedDate ELSE a.WritIssuedDate END),
		a.WritNo= (CASE WHEN COALESCE(a.WritNo,'')='' THEN b.WritNo ELSE a.WritNo END),
		a.SenstiveCase= (CASE WHEN COALESCE(a.SenstiveCase,'')='' THEN b.SenstiveCase ELSE a.SenstiveCase END),
		a.MPLetter= (CASE WHEN COALESCE(a.MPLetter,'')='' THEN b.MPLetter ELSE a.MPLetter END),
		a.IsActive= (CASE WHEN COALESCE(a.IsActive,'')='' THEN b.IsActive ELSE a.IsActive END),				
		a.ModifiedDate= GETDATE(),
		a.AdjusterAppointed= (CASE WHEN COALESCE(a.AdjusterAppointed,'')='' THEN b.AdjusterAppointed ELSE a.AdjusterAppointed END),
		a.LawyerAppointed= (CASE WHEN COALESCE(a.LawyerAppointed,'')='' THEN b.LawyerAppointed ELSE a.LawyerAppointed END),
		a.SurveyorAppointed= (CASE WHEN COALESCE(a.SurveyorAppointed,'')='' THEN b.SurveyorAppointed ELSE a.SurveyorAppointed END),
		a.DepotWorkshop= (CASE WHEN COALESCE(a.DepotWorkshop,'')='' THEN b.DepotWorkshop ELSE a.DepotWorkshop END),
		a.ClaimantName= (CASE WHEN COALESCE(a.ClaimantName,'')='' THEN b.ClaimantName ELSE a.ClaimantName END),
		a.ClaimantNRICPPNO= (CASE WHEN COALESCE(a.ClaimantNRICPPNO,'')='' THEN b.ClaimantNRICPPNO ELSE a.ClaimantNRICPPNO END),
		a.ClaimantDOB= (CASE WHEN COALESCE(a.ClaimantDOB,'')='' THEN b.ClaimantDOB ELSE a.ClaimantDOB END),
		a.ClaimantGender= (CASE WHEN COALESCE(a.ClaimantGender,'')='' THEN b.ClaimantGender ELSE a.ClaimantGender END),
		a.ClaimantType= (CASE WHEN COALESCE(a.ClaimantType,'')='' THEN b.ClaimantType ELSE a.ClaimantType END),
		a.ClaimantAddress1= (CASE WHEN COALESCE(a.ClaimantAddress1,'')='' THEN b.ClaimantAddress1 ELSE a.ClaimantAddress1 END),
		a.ClaimantAddress2= (CASE WHEN COALESCE(a.ClaimantAddress2,'')='' THEN b.ClaimantAddress2 ELSE a.ClaimantAddress2 END),
		a.ClaimantAddress3= (CASE WHEN COALESCE(a.ClaimantAddress3,'')='' THEN b.ClaimantAddress3 ELSE a.ClaimantAddress3 END),
		a.PostalCode= (CASE WHEN COALESCE(a.PostalCode,'')='' THEN b.PostalCode ELSE a.PostalCode END),
		a.ClaimantContactNo= (CASE WHEN COALESCE(a.ClaimantContactNo,'')='' THEN b.ClaimantContactNo ELSE a.ClaimantContactNo END),
		a.ClaimantEmail= (CASE WHEN COALESCE(a.ClaimantEmail,'')='' THEN b.ClaimantEmail ELSE a.ClaimantEmail END),
		a.VehicleRegnNo= (CASE WHEN COALESCE(a.VehicleRegnNo,'')='' THEN b.VehicleRegnNo ELSE a.VehicleRegnNo END),
		a.VehicleMake= (CASE WHEN COALESCE(a.VehicleMake,'')='' THEN b.VehicleMake ELSE a.VehicleMake END),
		a.VehicleModel= (CASE WHEN COALESCE(a.VehicleModel,'')='' THEN b.VehicleModel ELSE a.VehicleModel END),
		a.Isclaimantaninfant= (CASE WHEN COALESCE(a.Isclaimantaninfant,'')='' THEN b.Isclaimantaninfant ELSE a.Isclaimantaninfant END),
		a.InfantName= (CASE WHEN COALESCE(a.InfantName,'')='' THEN b.InfantName ELSE a.InfantName END),
		a.InfantDOB= (CASE WHEN COALESCE(a.InfantDOB,'')='' THEN b.InfantDOB ELSE a.InfantDOB END),
		a.InfantGender= (CASE WHEN COALESCE(a.InfantGender,'')='' THEN b.InfantGender ELSE a.InfantGender END),
		a.ClaimRecordNo= (CASE WHEN COALESCE(a.ClaimRecordNo,'')='' THEN b.ClaimRecordNo ELSE a.ClaimRecordNo END),
		a.ClaimsOfficer= (CASE WHEN COALESCE(a.ClaimsOfficer,'')='' THEN b.ClaimsOfficer ELSE a.ClaimsOfficer END),
		a.DriverLiablity= (CASE WHEN COALESCE(a.DriverLiablity,'')='' THEN b.DriverLiablity ELSE a.DriverLiablity END),
		a.AccidentCause= (CASE WHEN COALESCE(a.AccidentCause,'')='' THEN b.AccidentCause ELSE a.AccidentCause END),
		a.ClaimantStatus= (CASE WHEN COALESCE(a.ClaimantStatus,'')='' THEN b.ClaimantStatus ELSE a.ClaimantStatus END),
		a.SensitiveCase= (CASE WHEN COALESCE(a.SensitiveCase,'')='' THEN b.SensitiveCase ELSE a.SensitiveCase END),
		a.ReopenedDate= (CASE WHEN COALESCE(a.ReopenedDate,'')='' THEN b.ReopenedDate ELSE a.ReopenedDate END),
		a.RecordReopenedReason= (CASE WHEN COALESCE(a.RecordReopenedReason,'')='' THEN b.RecordReopenedReason ELSE a.RecordReopenedReason END),
		a.RecordCancellationDate= (CASE WHEN COALESCE(a.RecordCancellationDate,'')='' THEN b.RecordCancellationDate ELSE a.RecordCancellationDate END),
		a.RecordCancellationReason= (CASE WHEN COALESCE(a.RecordCancellationReason,'')='' THEN b.RecordCancellationReason ELSE a.RecordCancellationReason END),
		a.MP= (CASE WHEN COALESCE(a.MP,'')='' THEN b.MP ELSE a.MP END),
		a.Constituency= (CASE WHEN COALESCE(a.Constituency,'')='' THEN b.Constituency ELSE a.Constituency END),
		a.DateOfMpLetter= (CASE WHEN COALESCE(a.DateOfMpLetter,'')='' THEN b.DateOfMpLetter ELSE a.DateOfMpLetter END),
		a.SeverityReferenceNo= (CASE WHEN COALESCE(a.SeverityReferenceNo,'')='' THEN b.SeverityReferenceNo ELSE a.SeverityReferenceNo END),
		a.ReportSentToInsurer= (CASE WHEN COALESCE(a.ReportSentToInsurer,'')='' THEN b.ReportSentToInsurer ELSE a.ReportSentToInsurer END),
		a.InformedInsurerOfSettlement= (CASE WHEN COALESCE(a.InformedInsurerOfSettlement,'')='' THEN b.InformedInsurerOfSettlement ELSE a.InformedInsurerOfSettlement END),
		a.ReferredToInsurersB= (CASE WHEN COALESCE(a.ReferredToInsurersB,'')='' THEN b.ReferredToInsurersB ELSE a.ReferredToInsurersB END),
		a.DateReferredToInsurersB= (CASE WHEN COALESCE(a.DateReferredToInsurersB,'')='' THEN b.DateReferredToInsurersB ELSE a.DateReferredToInsurersB END),		
		a.ConfirmedAmount= (CASE WHEN COALESCE(a.ConfirmedAmount,'')='' THEN b.ConfirmedAmount ELSE a.ConfirmedAmount END)
	
	FROM CLM_Claims a 
		INNER JOIN CLM_Claims b
	ON a.AccidentClaimId= @ReportedAccidentClaimid
		AND b.AccidentClaimId= @UnReportedAccidentClaimid
		AND ISNULL(a.ClaimType,'') =ISNULL(b.ClaimType,'')
		AND ISNULL(a.ClaimDate,'') =ISNULL(b.ClaimDate,'')
		AND ISNULL (a.ClaimantName,'') = ISNULL(b.ClaimantName,'')
		AND ISNULL (a.ClaimantNRICPPNO,'') =ISNULL(b.ClaimantNRICPPNO,'')
		AND ISNULL(a.ClaimantDOB,'') =ISNULL(b.ClaimantDOB,'')
		AND ISNULL(a.ClaimantType,'') =ISNULL(b.ClaimantType,'')

INSERT INTO CLM_Claims(AccidentClaimId,PolicyId,ClaimType,ClaimDate,FinalSettleDate,ClaimStatus,TimeBarDate,CaseCategory,CaseStatus,ClaimAmount,ExcessRecoveredDate,WritIssuedDate,WritNo,SenstiveCase,MPLetter,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,AdjusterAppointed,LawyerAppointed,SurveyorAppointed,DepotWorkshop,ClaimantName,ClaimantNRICPPNO,ClaimantDOB,ClaimantGender,ClaimantType,ClaimantAddress1,ClaimantAddress2,ClaimantAddress3,PostalCode,ClaimantContactNo,ClaimantEmail,VehicleRegnNo,VehicleMake,VehicleModel,Isclaimantaninfant,InfantName,InfantDOB,InfantGender,ClaimRecordNo,ClaimsOfficer,DriverLiablity,AccidentCause,ClaimantStatus,SensitiveCase,ReopenedDate,RecordReopenedReason,RecordCancellationDate,RecordCancellationReason,MP,Constituency,DateOfMpLetter,SeverityReferenceNo,
ReportSentToInsurer,InformedInsurerOfSettlement,ReferredToInsurersB,DateReferredToInsurersB,AccidentId,ConfirmedAmount)
	SELECT @ReportedAccidentClaimid,PolicyId,ClaimType,ClaimDate,FinalSettleDate,ClaimStatus,TimeBarDate,CaseCategory,CaseStatus,ClaimAmount,ExcessRecoveredDate,WritIssuedDate,WritNo,SenstiveCase,MPLetter,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,AdjusterAppointed,LawyerAppointed,SurveyorAppointed,DepotWorkshop,ClaimantName,ClaimantNRICPPNO,ClaimantDOB,ClaimantGender,ClaimantType,ClaimantAddress1,ClaimantAddress2,ClaimantAddress3,PostalCode,ClaimantContactNo,ClaimantEmail,VehicleRegnNo,VehicleMake,VehicleModel,Isclaimantaninfant,InfantName,InfantDOB,InfantGender,ClaimRecordNo,ClaimsOfficer,DriverLiablity,AccidentCause,ClaimantStatus,SensitiveCase,ReopenedDate,RecordReopenedReason,RecordCancellationDate,RecordCancellationReason,MP,Constituency,DateOfMpLetter,SeverityReferenceNo,ReportSentToInsurer,InformedInsurerOfSettlement,ReferredToInsurersB,DateReferredToInsurersB,AccidentId,ConfirmedAmount 
	FROM CLM_Claims 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND ClaimID NOT IN 
	(
		SELECT b.ClaimID
		FROM CLM_Claims a 
			INNER JOIN CLM_Claims b
		ON a.AccidentClaimId= @ReportedAccidentClaimid
			AND b.AccidentClaimId= @UnReportedAccidentClaimid
			AND ISNULL(a.ClaimType,'') =ISNULL(b.ClaimType,'')
			AND ISNULL(a.ClaimDate,'') =ISNULL(b.ClaimDate,'')
			AND ISNULL (a.ClaimantName,'') = ISNULL(b.ClaimantName,'')
			AND ISNULL (a.ClaimantNRICPPNO,'') =ISNULL(b.ClaimantNRICPPNO,'')
			AND ISNULL(a.ClaimantDOB,'') =ISNULL(b.ClaimantDOB,'')
			AND ISNULL(a.ClaimantType,'') =ISNULL(b.ClaimantType,'')
	)
	

--Table CLM_ServiceProvider
 
 --select * from CLM_ServiceProvider
 
UPDATE d 
	SET	
		d.ClaimTypeId= (CASE WHEN COALESCE(d.ClaimTypeId,'')='' THEN e.ClaimTypeId ELSE d.ClaimTypeId END),
		d.ClaimantNameId= (CASE WHEN COALESCE(d.ClaimantNameId,'')='' THEN e.ClaimantNameId ELSE d.ClaimantNameId END),
		d.PartyTypeId= (CASE WHEN COALESCE(d.PartyTypeId,'')='' THEN e.PartyTypeId ELSE d.PartyTypeId END),
		d.ServiceProviderTypeId= (CASE WHEN COALESCE(d.ServiceProviderTypeId,'')='' THEN e.ServiceProviderTypeId ELSE d.ServiceProviderTypeId END),
		d.CompanyNameId= (CASE WHEN COALESCE(d.CompanyNameId,'')='' THEN e.CompanyNameId ELSE d.CompanyNameId END),
		d.AppointedDate= (CASE WHEN COALESCE(d.AppointedDate,'')='' THEN e.AppointedDate ELSE d.AppointedDate END),
		d.Address1= (CASE WHEN COALESCE(d.Address1,'')='' THEN e.Address1 ELSE d.Address1 END),
		d.Address2= (CASE WHEN COALESCE(d.Address2,'')='' THEN e.Address2 ELSE d.Address2 END),
		d.Address3= (CASE WHEN COALESCE(d.Address3,'')='' THEN e.Address3 ELSE d.Address3 END),
		d.City= (CASE WHEN COALESCE(d.City,'')='' THEN e.City ELSE d.City END),
		d.State= (CASE WHEN COALESCE(d.State,'')='' THEN e.State ELSE d.State END),
		d.CountryId= (CASE WHEN COALESCE(d.CountryId,'')='' THEN e.CountryId ELSE d.CountryId END),
		d.PostalCode= (CASE WHEN COALESCE(d.PostalCode,'')='' THEN e.PostalCode ELSE d.PostalCode END),
		d.Reference= (CASE WHEN COALESCE(d.Reference,'')='' THEN e.Reference ELSE d.Reference END),
		d.ContactPersonName= (CASE WHEN COALESCE(d.ContactPersonName,'')='' THEN e.ContactPersonName ELSE d.ContactPersonName END),
		d.EmailAddress= (CASE WHEN COALESCE(d.EmailAddress,'')='' THEN e.EmailAddress ELSE d.EmailAddress END),
		d.OfficeNo= (CASE WHEN COALESCE(d.OfficeNo,'')='' THEN e.OfficeNo ELSE d.OfficeNo END),
		d.Mobile= (CASE WHEN COALESCE(d.Mobile,'')='' THEN e.Mobile ELSE d.Mobile END),
		d.Fax= (CASE WHEN COALESCE(d.Fax,'')='' THEN e.Fax ELSE d.Fax END),
		d.ContactPersonName2nd= (CASE WHEN COALESCE(d.ContactPersonName2nd,'')='' THEN e.ContactPersonName2nd ELSE d.ContactPersonName2nd END),
		d.EmailAddress2nd= (CASE WHEN COALESCE(d.EmailAddress2nd,'')='' THEN e.EmailAddress2nd ELSE d.EmailAddress2nd END),
		d.OfficeNo2nd= (CASE WHEN COALESCE(d.OfficeNo2nd,'')='' THEN e.OfficeNo2nd ELSE d.OfficeNo2nd END),
		d.Mobile2nd= (CASE WHEN COALESCE(d.Mobile2nd,'')='' THEN e.Mobile2nd ELSE d.Mobile2nd END),
		d.Fax2nd= (CASE WHEN COALESCE(d.Fax2nd,'')='' THEN e.Fax2nd ELSE d.Fax2nd END),
		d.StatusId= (CASE WHEN COALESCE(d.StatusId,'')='' THEN e.StatusId ELSE d.StatusId END),
		d.Remarks= (CASE WHEN COALESCE(d.Remarks,'')='' THEN e.Remarks ELSE d.Remarks END),				
		d.Modifieddate= GETDATE(),		
		d.IsActive= (CASE WHEN COALESCE(d.IsActive,'')='' THEN e.IsActive ELSE d.IsActive END),
		d.ClaimRecordNo= (CASE WHEN COALESCE(d.ClaimRecordNo,'')='' THEN e.ClaimRecordNo ELSE d.ClaimRecordNo END)
	FROM  CLM_ServiceProvider d 
	INNER JOIN CLM_ServiceProvider e
	ON d.AccidentId= @ReportedAccidentClaimid
	AND e.AccidentId= @UnReportedAccidentClaimid
	AND d.ClaimTypeId =e.ClaimTypeId
	AND d.ClaimantNameId=e.ClaimantNameId
	AND d.PartyTypeId =e.PartyTypeId 
	AND d.CompanyNameId=e.CompanyNameId 
	AND d.AppointedDate =e.AppointedDate  
	
	--FOR INSERT IF NOT EXISTS
	
	INSERT INTO CLM_ServiceProvider(ClaimTypeId,ClaimantNameId,PartyTypeId,ServiceProviderTypeId,CompanyNameId,AppointedDate,Address1,
	Address2,Address3,City,State,CountryId,PostalCode,Reference,ContactPersonName,EmailAddress,OfficeNo,Mobile,Fax,ContactPersonName2nd,
	EmailAddress2nd,OfficeNo2nd,Mobile2nd,Fax2nd,StatusId,Remarks,Createdby,Createddate,Modifiedby,Modifieddate,AccidentId,PolicyId,
	IsActive,ClaimRecordNo)
	SELECT ClaimTypeId,ClaimantNameId,PartyTypeId,ServiceProviderTypeId,CompanyNameId,AppointedDate,Address1,
	Address2,Address3,City,State,CountryId,PostalCode,Reference,ContactPersonName,EmailAddress,OfficeNo,Mobile,Fax,ContactPersonName2nd,
	EmailAddress2nd,OfficeNo2nd,Mobile2nd,Fax2nd,StatusId,Remarks,Createdby,Createddate,Modifiedby,Modifieddate,@ReportedAccidentClaimid,
	PolicyId,IsActive,ClaimRecordNo 
	FROM CLM_ServiceProvider 
	WHERE AccidentId=@UnReportedAccidentClaimid AND ServiceProviderId NOT IN 
	(
		SELECT e.ServiceProviderId
		FROM  CLM_ServiceProvider d 
			INNER JOIN CLM_ServiceProvider e
		ON d.AccidentId= @ReportedAccidentClaimid
			AND e.AccidentId= @UnReportedAccidentClaimid
			AND d.ClaimTypeId =e.ClaimTypeId
			AND d.ClaimantNameId=e.ClaimantNameId
			AND d.PartyTypeId =e.PartyTypeId 
			AND d.CompanyNameId=e.CompanyNameId 
			AND d.AppointedDate =e.AppointedDate  
	)
	
--Table from CLM_Notes

--select * from CLM_Notes

UPDATE f 
	SET	
		f.PolicyId=(CASE WHEN COALESCE(f.PolicyId,'')='' THEN g.PolicyId ELSE f.PolicyId END),		
		f.NoteCode=(CASE WHEN COALESCE(f.NoteCode,'')='' THEN g.NoteCode ELSE f.NoteCode END),
		f.NoteDate=(CASE WHEN COALESCE(f.NoteDate,'')='' THEN g.NoteDate ELSE f.NoteDate END),
		f.NoteTime=(CASE WHEN COALESCE(f.NoteTime,'')='' THEN g.NoteTime ELSE f.NoteTime END),
		f.ImageCode=(CASE WHEN COALESCE(f.ImageCode,'')='' THEN g.ImageCode ELSE f.ImageCode END),
		f.ImageId=(CASE WHEN COALESCE(f.ImageId,'')='' THEN g.ImageId ELSE f.ImageId END),
		f.NotesDescription=(CASE WHEN COALESCE(f.NotesDescription,'')='' THEN g.NotesDescription ELSE f.NotesDescription END),
		f.ModifiedDate=GETDATE(),
		f.URL_PATH=(CASE WHEN COALESCE(f.URL_PATH,'')='' THEN g.URL_PATH ELSE f.URL_PATH END),		
		f.ClaimantNames=(CASE WHEN COALESCE(f.ClaimantNames,'')='' THEN g.ClaimantNames ELSE f.ClaimantNames END)
	FROM CLM_Notes f 
		INNER JOIN CLM_Notes g
	ON f.AccidentId =@ReportedAccidentClaimid
		AND g.AccidentId =@UnReportedAccidentClaimid
		AND f.ClaimId =g.ClaimId
		AND f.NoteDate =g.NoteDate
		AND ISNULL(f.ClaimantNames,'') =ISNULL(g.ClaimantNames,'')
		
	--FOR INSERT IF NOT EXISTS
	INSERT INTO CLM_Notes(PolicyId,ClaimId,NoteCode,NoteDate,NoteTime,ImageCode,ImageId,NotesDescription,ModifiedBy,
							CreatedDate,CreatedBy,ModifiedDate,URL_PATH,AccidentId,ClaimantNames)
	SELECT PolicyId,ClaimId,NoteCode,NoteDate,NoteTime,ImageCode,ImageId,NotesDescription,ModifiedBy,
							CreatedDate,CreatedBy,ModifiedDate,URL_PATH,@ReportedAccidentClaimid,ClaimantNames 
	FROM CLM_Notes 
	WHERE AccidentId=@UnReportedAccidentClaimid AND NoteId NOT IN 
	(	
		SELECT g.NoteId
		FROM CLM_Notes f 
			INNER JOIN CLM_Notes g
		ON f.AccidentId =@ReportedAccidentClaimid
			AND g.AccidentId =@UnReportedAccidentClaimid
			AND f.ClaimId =g.ClaimId
			AND f.NoteDate =g.NoteDate
			AND ISNULL(f.ClaimantNames,'') =ISNULL(g.ClaimantNames,'')
	)

--Table CLM_ClaimTask

--select * from CLM_ClaimTask

UPDATE h 
	SET	
		h.TaskNo= (CASE WHEN COALESCE(h.TaskNo,'')='' THEN i.TaskNo ELSE h.TaskNo END),		
		h.ActionDue= (CASE WHEN COALESCE(h.ActionDue,'')='' THEN i.ActionDue ELSE h.ActionDue END),
		h.CloseDate= (CASE WHEN COALESCE(h.CloseDate,'')='' THEN i.CloseDate ELSE h.CloseDate END),
		h.PromtDetails= (CASE WHEN COALESCE(h.PromtDetails,'')='' THEN i.PromtDetails ELSE h.PromtDetails END),
		h.isApprove= (CASE WHEN COALESCE(h.isApprove,'')='' THEN i.isApprove ELSE h.isApprove END),
		h.ApproveDate= (CASE WHEN COALESCE(h.ApproveDate,'')='' THEN i.ApproveDate ELSE h.ApproveDate END),
		h.ApproveBy= (CASE WHEN COALESCE(h.ApproveBy,'')='' THEN i.ApproveBy ELSE h.ApproveBy END),
		h.ModifiedDate=GETDATE(),				
		h.Remarks= (CASE WHEN COALESCE(h.Remarks,'')='' THEN i.Remarks ELSE h.Remarks END),		
		h.ClaimantNames= (CASE WHEN COALESCE(h.ClaimantNames,'')='' THEN i.ClaimantNames ELSE h.ClaimantNames END),
		h.ClaimsOfficer= (CASE WHEN COALESCE(h.ClaimsOfficer,'')='' THEN i.ClaimsOfficer ELSE h.ClaimsOfficer END)
	FROM CLM_ClaimTask h 
		INNER JOIN CLM_ClaimTask i
	ON h.AccidentClaimId =@ReportedAccidentClaimid
		AND i.AccidentClaimId =@UnReportedAccidentClaimid
		AND ISNULL(h.ClaimID,'') =ISNULL(i.ClaimID,'')
		AND ISNULL(h.ActionDue,'') =ISNULL(i.ActionDue,'')
		AND ISNULL(h.PromtDetails,'') =ISNULL(i.PromtDetails,'')
		AND ISNULL(h.ClaimantNames,'')=ISNULL(i.ClaimantNames,'')
		AND ISNULL(h.ClaimsOfficer,'') =ISNULL(i.ClaimsOfficer,'')
		
		
	--INSERT IF NOT EXSITS
	INSERT INTO CLM_ClaimTask(TaskNo,ClaimID,ActionDue,CloseDate,PromtDetails,isApprove,ApproveDate,ApproveBy,ModifiedDate,
							CreatedDate,CreatedBy,ModifiedBy,Remarks,AccidentId,AccidentClaimId,ClaimantNames,ClaimsOfficer)
	SELECT TaskNo,ClaimID,ActionDue,CloseDate,PromtDetails,isApprove,ApproveDate,ApproveBy,ModifiedDate,
							CreatedDate,CreatedBy,ModifiedBy,Remarks,AccidentId,@ReportedAccidentClaimid,ClaimantNames,ClaimsOfficer
	FROM CLM_ClaimTask 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND Id NOT IN 
	(
		SELECT i.Id
		FROM CLM_ClaimTask h 
			INNER JOIN CLM_ClaimTask i
		ON h.AccidentClaimId =@ReportedAccidentClaimid
			AND i.AccidentClaimId =@UnReportedAccidentClaimid
			AND ISNULL(h.ClaimID,'') =ISNULL(i.ClaimID,'')
			AND ISNULL(h.ActionDue,'') =ISNULL(i.ActionDue,'')
			AND ISNULL(h.PromtDetails,'') =ISNULL(i.PromtDetails,'')
			AND ISNULL(h.ClaimantNames,'')=ISNULL(i.ClaimantNames,'')
			AND ISNULL(h.ClaimsOfficer,'') =ISNULL(i.ClaimsOfficer,'')
	)

--Table from Claim_ReAssignmentDairy

--select * from Claim_ReAssignmentDairy 

UPDATE j 
	SET	
		j.DairyId=(CASE WHEN COALESCE(j.DairyId,'')='' THEN k.DairyId ELSE j.DairyId END),
		j.TypeOfAssignment=(CASE WHEN COALESCE(j.TypeOfAssignment,'')='' THEN k.TypeOfAssignment ELSE j.TypeOfAssignment END),
		j.ReAssignTo=(CASE WHEN COALESCE(j.ReAssignTo,'')='' THEN k.ReAssignTo ELSE j.ReAssignTo END),
		j.DairyFromUser=(CASE WHEN COALESCE(j.DairyFromUser,'')='' THEN k.DairyFromUser ELSE j.DairyFromUser END),
		j.ReAssignDateFrom=(CASE WHEN COALESCE(j.ReAssignDateFrom,'')='' THEN k.ReAssignDateFrom ELSE j.ReAssignDateFrom END),
		j.ReAssignDateTo=(CASE WHEN COALESCE(j.ReAssignDateTo,'')='' THEN k.ReAssignDateTo ELSE j.ReAssignDateTo END),
		j.Remark=(CASE WHEN COALESCE(j.Remark,'')='' THEN k.Remark ELSE j.Remark END),
		j.EmailId=(CASE WHEN COALESCE(j.EmailId,'')='' THEN k.EmailId ELSE j.EmailId END),
		j.Status=(CASE WHEN COALESCE(j.Status,'')='' THEN k.Status ELSE j.Status END),
		j.IsActive=(CASE WHEN COALESCE(j.IsActive,'')='' THEN k.IsActive ELSE j.IsActive END),				
		j.ModifiedDate=GETDATE()
	FROM Claim_ReAssignmentDairy j 
		INNER JOIN Claim_ReAssignmentDairy k
	ON j.AccidentClaimId=@ReportedAccidentClaimid
		AND k.AccidentClaimId = @UnReportedAccidentClaimid
		AND ISNULL(j.TypeOfAssignment,'')=ISNULL(k.TypeOfAssignment,'')
		AND ISNULL(j.ReAssignTo,'')=ISNULL(k.ReAssignTo,'')
		AND ISNULL(j.DairyFromUser,'') =ISNULL(k.DairyFromUser,'')
		AND ISNULL(j.ClaimId,'') =ISNULL(k.ClaimId,'')
		
	--INSERT IF NOT EXISTS
	INSERT INTO Claim_ReAssignmentDairy(DairyId,TypeOfAssignment,ReAssignTo,DairyFromUser,ReAssignDateFrom,ReAssignDateTo,Remark,
								EmailId,Status,IsActive,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate,ClaimId,AccidentClaimId)
	SELECT DairyId,TypeOfAssignment,ReAssignTo,DairyFromUser,ReAssignDateFrom,ReAssignDateTo,Remark,
								EmailId,Status,IsActive,CreatedDate,CreatedBy,ModifiedBy,ModifiedDate,ClaimId,@ReportedAccidentClaimid 
	FROM Claim_ReAssignmentDairy 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND Id NOT IN 
	(
		SELECT k.Id
		FROM Claim_ReAssignmentDairy j 
			INNER JOIN Claim_ReAssignmentDairy k
		ON j.AccidentClaimId=@ReportedAccidentClaimid
			AND k.AccidentClaimId = @UnReportedAccidentClaimid
			AND ISNULL(j.TypeOfAssignment,'')=ISNULL(k.TypeOfAssignment,'')
			AND ISNULL(j.ReAssignTo,'')=ISNULL(k.ReAssignTo,'')
			AND ISNULL(j.DairyFromUser,'') =ISNULL(k.DairyFromUser,'')
			AND ISNULL(j.ClaimId,'') =ISNULL(k.ClaimId,'')
	)
	


--Table  TODODIARYLIST 
--select * from TODODIARYLIST where AccidentId in(184,186)

UPDATE l
	SET	
		l.RECBYSYSTEM=(CASE WHEN COALESCE(l.RECBYSYSTEM,'')='' THEN m.RECBYSYSTEM ELSE l.RECBYSYSTEM END),
		l.RECDATE=(CASE WHEN COALESCE(l.RECDATE,'')='' THEN m.RECDATE ELSE l.RECDATE END),
		l.FOLLOWUPDATE=(CASE WHEN COALESCE(l.FOLLOWUPDATE,'')='' THEN m.FOLLOWUPDATE ELSE l.FOLLOWUPDATE END),
		l.LISTTYPEID=(CASE WHEN COALESCE(l.LISTTYPEID,'')='' THEN m.LISTTYPEID ELSE l.LISTTYPEID END),
		l.POLICYBROKERID=(CASE WHEN COALESCE(l.POLICYBROKERID,'')='' THEN m.POLICYBROKERID ELSE l.POLICYBROKERID END),
		l.SUBJECTLINE=(CASE WHEN COALESCE(l.SUBJECTLINE,'')='' THEN m.SUBJECTLINE ELSE l.SUBJECTLINE END),
		l.LISTOPEN=(CASE WHEN COALESCE(l.LISTOPEN,'')='' THEN m.LISTOPEN ELSE l.LISTOPEN END),
		l.SYSTEMFOLLOWUPID=(CASE WHEN COALESCE(l.SYSTEMFOLLOWUPID,'')='' THEN m.SYSTEMFOLLOWUPID ELSE l.SYSTEMFOLLOWUPID END),
		l.PRIORITY=(CASE WHEN COALESCE(l.PRIORITY,'')='' THEN m.PRIORITY ELSE l.PRIORITY END),
		l.TOUSERID=(CASE WHEN COALESCE(l.TOUSERID,'')='' THEN m.TOUSERID ELSE l.TOUSERID END),
		l.FROMUSERID=(CASE WHEN COALESCE(l.FROMUSERID,'')='' THEN m.FROMUSERID ELSE l.FROMUSERID END),
		l.STARTTIME=(CASE WHEN COALESCE(l.STARTTIME,'')='' THEN m.STARTTIME ELSE l.STARTTIME END),
		l.ENDTIME=(CASE WHEN COALESCE(l.ENDTIME,'')='' THEN m.ENDTIME ELSE l.ENDTIME END),
		l.NOTE=(CASE WHEN COALESCE(l.NOTE,'')='' THEN m.NOTE ELSE l.NOTE END),
		l.PROPOSALVERSION=(CASE WHEN COALESCE(l.PROPOSALVERSION,'')='' THEN m.PROPOSALVERSION ELSE l.PROPOSALVERSION END),
		l.QUOTEID=(CASE WHEN COALESCE(l.QUOTEID,'')='' THEN m.QUOTEID ELSE l.QUOTEID END),		
		l.CLAIMMOVEMENTID=(CASE WHEN COALESCE(l.CLAIMMOVEMENTID,'')='' THEN m.CLAIMMOVEMENTID ELSE l.CLAIMMOVEMENTID END),
		l.TOENTITYID=(CASE WHEN COALESCE(l.TOENTITYID,'')='' THEN m.TOENTITYID ELSE l.TOENTITYID END),
		l.FROMENTITYID=(CASE WHEN COALESCE(l.FROMENTITYID,'')='' THEN m.FROMENTITYID ELSE l.FROMENTITYID END),
		l.CUSTOMER_ID=(CASE WHEN COALESCE(l.CUSTOMER_ID,'')='' THEN m.CUSTOMER_ID ELSE l.CUSTOMER_ID END),
		l.APP_ID=(CASE WHEN COALESCE(l.APP_ID,'')='' THEN m.APP_ID ELSE l.APP_ID END),
		l.APP_VERSION_ID=(CASE WHEN COALESCE(l.APP_VERSION_ID,'')='' THEN m.APP_VERSION_ID ELSE l.APP_VERSION_ID END),		
		l.POLICY_VERSION_ID=(CASE WHEN COALESCE(l.POLICY_VERSION_ID,'')='' THEN m.POLICY_VERSION_ID ELSE l.POLICY_VERSION_ID END),
		l.RULES_VERIFIED=(CASE WHEN COALESCE(l.RULES_VERIFIED,'')='' THEN m.RULES_VERIFIED ELSE l.RULES_VERIFIED END),
		l.PROCESS_ROW_ID=(CASE WHEN COALESCE(l.PROCESS_ROW_ID,'')='' THEN m.PROCESS_ROW_ID ELSE l.PROCESS_ROW_ID END),
		l.MODULE_ID=(CASE WHEN COALESCE(l.MODULE_ID,'')='' THEN m.MODULE_ID ELSE l.MODULE_ID END),
		l.INSURERNAME=(CASE WHEN COALESCE(l.INSURERNAME,'')='' THEN m.INSURERNAME ELSE l.INSURERNAME END),				
		l.ExpectedPaymentDate=(CASE WHEN COALESCE(l.ExpectedPaymentDate,'')='' THEN m.ExpectedPaymentDate ELSE l.ExpectedPaymentDate END),
		l.ReminderBeforeCompletion=(CASE WHEN COALESCE(l.ReminderBeforeCompletion,'')='' THEN m.ReminderBeforeCompletion ELSE l.ReminderBeforeCompletion END),
		l.Escalation=(CASE WHEN COALESCE(l.Escalation,'')='' THEN m.Escalation ELSE l.Escalation END),
		l.EscalationTo=(CASE WHEN COALESCE(l.EscalationTo,'')='' THEN m.EscalationTo ELSE l.EscalationTo END),
		l.EmailBody=(CASE WHEN COALESCE(l.EmailBody,'')='' THEN m.EmailBody ELSE l.EmailBody END),
		l.ModifiedDate=GETDATE(),
		l.UserId=(CASE WHEN COALESCE(l.UserId,'')='' THEN m.UserId ELSE l.UserId END),
		l.ReassignedDiary=(CASE WHEN COALESCE(l.ReassignedDiary,'')='' THEN m.ReassignedDiary ELSE l.ReassignedDiary END),
		l.ReassignedDiaryDate=(CASE WHEN COALESCE(l.ReassignedDiaryDate,'')='' THEN m.ReassignedDiaryDate ELSE l.ReassignedDiaryDate END),		
		l.MovementType=(CASE WHEN COALESCE(l.MovementType,'')='' THEN m.MovementType ELSE l.MovementType END),
		l.ParentId=(CASE WHEN COALESCE(l.ParentId,'')='' THEN m.ParentId ELSE l.ParentId END),
		l.IsActive=(CASE WHEN COALESCE(l.IsActive,'')='' THEN m.IsActive ELSE l.IsActive END)
	FROM TODODIARYLIST l 
		INNER JOIN TODODIARYLIST m
	ON l.AccidentId =@ReportedAccidentClaimid
		AND m.AccidentId =@UnReportedAccidentClaimid
		AND l.LISTTYPEID=m.LISTTYPEID 
		AND l.TOUSERID =m.TOUSERID
		AND l.FROMUSERID =m.FROMUSERID
		AND ISNULL(l.CLAIMID,'') =ISNULL(m.CLAIMID,'') 
		AND ISNULL(l.MovementType,'') =ISNULL(m.MovementType,'') 
		AND ISNULL(l.STARTTIME,'') =ISNULL(m.STARTTIME,'')
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO TODODIARYLIST(RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,
	TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,CUSTOMER_ID,
	APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,RULES_VERIFIED,PROCESS_ROW_ID,MODULE_ID,INSURERNAME,POLICYNO,CLAIMNO,
	ExpectedPaymentDate,ReminderBeforeCompletion,Escalation,EscalationTo,EmailBody,ModifiedBy,CreatedDate,CreatedBy,ModifiedDate,
	UserId,ReassignedDiary,ReassignedDiaryDate,AccidentId,MovementType,ParentId,IsActive)
	SELECT RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,
	TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,CUSTOMER_ID,
	APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID,RULES_VERIFIED,PROCESS_ROW_ID,MODULE_ID,INSURERNAME,POLICYNO,CLAIMNO,
	ExpectedPaymentDate,ReminderBeforeCompletion,Escalation,EscalationTo,EmailBody,ModifiedBy,CreatedDate,CreatedBy,ModifiedDate,
	UserId,ReassignedDiary,ReassignedDiaryDate,@ReportedAccidentClaimid,MovementType,ParentId,IsActive
	FROM TODODIARYLIST 
	WHERE AccidentId=@UnReportedAccidentClaimid AND LISTID NOT IN 
	(
		SELECT m.LISTID
		FROM TODODIARYLIST l 
			INNER JOIN TODODIARYLIST m
		ON l.AccidentId =@ReportedAccidentClaimid
				AND m.AccidentId =@UnReportedAccidentClaimid
				AND l.LISTTYPEID=m.LISTTYPEID 
				AND l.TOUSERID =m.TOUSERID
				AND l.FROMUSERID =m.FROMUSERID
				AND ISNULL(l.CLAIMID,'') =ISNULL(m.CLAIMID,'') 
				AND ISNULL(l.MovementType,'') =ISNULL(m.MovementType,'') 
				AND ISNULL(l.STARTTIME,'') =ISNULL(m.STARTTIME,'')
	)


--Table MNT_AttachmentList

--select * from MNT_AttachmentList 

UPDATE n
	SET	
		n.AttachLoc=(CASE WHEN COALESCE(n.AttachLoc,'')='' THEN o.AttachLoc ELSE n.AttachLoc END),
		n.AttachEntId=(CASE WHEN COALESCE(n.AttachEntId,'')='' THEN o.AttachEntId ELSE n.AttachEntId END),
		n.AttachFileName=(CASE WHEN COALESCE(n.AttachFileName,'')='' THEN o.AttachFileName ELSE n.AttachFileName END),
		n.AttachDateTime=(CASE WHEN COALESCE(n.AttachDateTime,'')='' THEN o.AttachDateTime ELSE n.AttachDateTime END),
		n.AttachFileDesc=(CASE WHEN COALESCE(n.AttachFileDesc,'')='' THEN o.AttachFileDesc ELSE n.AttachFileDesc END),
		n.AttachPolicyId=(CASE WHEN COALESCE(n.AttachPolicyId,'')='' THEN o.AttachPolicyId ELSE n.AttachPolicyId END),
		n.ClaimantName=(CASE WHEN COALESCE(n.ClaimantName,'')='' THEN o.ClaimantName ELSE n.ClaimantName END),
		n.AttachFileType=(CASE WHEN COALESCE(n.AttachFileType,'')='' THEN o.AttachFileType ELSE n.AttachFileType END),
		n.AttachEntityType=(CASE WHEN COALESCE(n.AttachEntityType,'')='' THEN o.AttachEntityType ELSE n.AttachEntityType END),
		n.IsActive=(CASE WHEN COALESCE(n.IsActive,'')='' THEN o.IsActive ELSE n.IsActive END),
		n.AttachType=(CASE WHEN COALESCE(n.AttachType,'')='' THEN o.AttachType ELSE n.AttachType END),
		n.FilePath=(CASE WHEN COALESCE(n.FilePath,'')='' THEN o.FilePath ELSE n.FilePath END),		
		n.ModifiedDate=GETDATE()		
	FROM MNT_AttachmentList n 
		INNER JOIN MNT_AttachmentList o
	ON n.AccidentId =@ReportedAccidentClaimid
		AND o.AccidentId =@UnReportedAccidentClaimid
		AND ISNULL(n.AttachEntId,'')=ISNULL(o.AttachEntId,'') 
		AND ISNULL(n.AttachFileName,'')=ISNULL(o.AttachFileName,'') 
		AND ISNULL(n.AttachFileType,'') =ISNULL(o.AttachFileType,'') 
		AND ISNULL(n.AttachEntityType,'')=ISNULL(o.AttachEntityType,'') 
		AND ISNULL(n.ClaimID,'') =ISNULL(o.ClaimID,'')  
		
	--INSERT IF NOT EXISTS
	INSERT INTO MNT_AttachmentList(AttachLoc,AttachEntId,AttachFileName,AttachDateTime,AttachFileDesc,AttachPolicyId,ClaimantName,
		AttachFileType,AttachEntityType,IsActive,AttachType,FilePath,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ClaimID,AccidentId)
	SELECT AttachLoc,AttachEntId,AttachFileName,AttachDateTime,AttachFileDesc,AttachPolicyId,ClaimantName,
		AttachFileType,AttachEntityType,IsActive,AttachType,FilePath,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,
		ClaimID,@ReportedAccidentClaimid
	FROM MNT_AttachmentList 
	WHERE AccidentId=@UnReportedAccidentClaimid AND AttachId NOT IN 
	(
		SELECT o.AttachId
		FROM MNT_AttachmentList n 
			INNER JOIN MNT_AttachmentList o
		ON n.AccidentId =@ReportedAccidentClaimid
			AND o.AccidentId =@UnReportedAccidentClaimid
			AND ISNULL(n.AttachEntId,'')=ISNULL(o.AttachEntId,'') 
			AND ISNULL(n.AttachFileName,'')=ISNULL(o.AttachFileName,'') 
			AND ISNULL(n.AttachFileType,'') =ISNULL(o.AttachFileType,'') 
			AND ISNULL(n.AttachEntityType,'')=ISNULL(o.AttachEntityType,'') 
			AND ISNULL(n.ClaimID,'') =ISNULL(o.ClaimID,'') 
	)

--Table CLM_ReserveSummary
--select * from CLM_ReserveSummary

UPDATE p
	SET	
		p.ClaimType=(CASE WHEN COALESCE(p.ClaimType,'')='' THEN q.ClaimType ELSE p.ClaimType END),
		p.MovementType=(CASE WHEN COALESCE(p.MovementType,'')='' THEN q.MovementType ELSE p.MovementType END),
		p.InitialReserve=(CASE WHEN COALESCE(p.InitialReserve,'')='' THEN q.InitialReserve ELSE p.InitialReserve END),
		p.PreReserve=(CASE WHEN COALESCE(p.PreReserve,'')='' THEN q.PreReserve ELSE p.PreReserve END),
		p.MovementReserve=(CASE WHEN COALESCE(p.MovementReserve,'')='' THEN q.MovementReserve ELSE p.MovementReserve END),
		p.CurrentReserve=(CASE WHEN COALESCE(p.CurrentReserve,'')='' THEN q.CurrentReserve ELSE p.CurrentReserve END),		
		p.Modifieddate=GETDATE(),
		p.IsActive=(CASE WHEN COALESCE(p.IsActive,'')='' THEN q.IsActive ELSE p.IsActive END)	
	FROM CLM_ReserveSummary p 
		INNER JOIN CLM_ReserveSummary q
	ON p.AccidentClaimId =@ReportedAccidentClaimid
		AND q.AccidentClaimId =@UnReportedAccidentClaimid
		AND p.ClaimID =q.ClaimID 
		AND p.ClaimType =q.ClaimType 
		AND ISNULL(p.MovementType,'') =ISNULL(q.MovementType,'') 
		AND ISNULL(p.PaymentId,'') =ISNULL(q.PaymentId,'')
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO CLM_ReserveSummary(AccidentClaimId,ClaimID,ClaimType,MovementType,InitialReserve,PreReserve,MovementReserve,
						CurrentReserve,PaymentId,Createdby,Createddate,Modifiedby,Modifieddate,IsActive)
	SELECT @ReportedAccidentClaimid,ClaimID,ClaimType,MovementType,InitialReserve,PreReserve,MovementReserve,
						CurrentReserve,PaymentId,Createdby,Createddate,Modifiedby,Modifieddate,IsActive
	FROM CLM_ReserveSummary 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND ReserveId NOT IN 
	(
		SELECT q.ReserveId
		FROM CLM_ReserveSummary p 
			INNER JOIN CLM_ReserveSummary q
		ON p.AccidentClaimId =@ReportedAccidentClaimid
			AND q.AccidentClaimId =@UnReportedAccidentClaimid
			AND p.ClaimID =q.ClaimID 
			AND p.ClaimType =q.ClaimType 
			AND ISNULL(p.MovementType,'') =ISNULL(q.MovementType,'') 
			AND ISNULL(p.PaymentId,'') =ISNULL(q.PaymentId,'')
	)


--Table CLM_ReserveDetails

--select * from CLM_ReserveDetails

UPDATE r
	SET			
		r.CmpCode=(CASE WHEN COALESCE(r.CmpCode,'')='' THEN s.CmpCode ELSE r.CmpCode END),
		r.InitialReserve=(CASE WHEN COALESCE(r.InitialReserve,'')='' THEN s.InitialReserve ELSE r.InitialReserve END),
		r.PreReserve=(CASE WHEN COALESCE(r.PreReserve,'')='' THEN s.PreReserve ELSE r.PreReserve END),
		r.MovementReserve=(CASE WHEN COALESCE(r.MovementReserve,'')='' THEN s.MovementReserve ELSE r.MovementReserve END),
		r.CurrentReserve=(CASE WHEN COALESCE(r.CurrentReserve,'')='' THEN s.CurrentReserve ELSE r.CurrentReserve END),		
		r.InitialNoofdays=(CASE WHEN COALESCE(r.InitialNoofdays,'')='' THEN s.InitialNoofdays ELSE r.InitialNoofdays END),
		r.MovementNoofdays=(CASE WHEN COALESCE(r.MovementNoofdays,'')='' THEN s.MovementNoofdays ELSE r.MovementNoofdays END),
		r.CurrentNoofdays=(CASE WHEN COALESCE(r.CurrentNoofdays,'')='' THEN s.CurrentNoofdays ELSE r.CurrentNoofdays END),
		r.InitialRateperday=(CASE WHEN COALESCE(r.InitialRateperday,'')='' THEN s.InitialRateperday ELSE r.InitialRateperday END),
		r.MovementlRateperday=(CASE WHEN COALESCE(r.MovementlRateperday,'')='' THEN s.MovementlRateperday ELSE r.MovementlRateperday END),
		r.CurrentRateperday=(CASE WHEN COALESCE(r.CurrentRateperday,'')='' THEN s.CurrentRateperday ELSE r.CurrentRateperday END),				
		r.Modifieddate=GETDATE(),
		r.IsActive=(CASE WHEN COALESCE(r.IsActive,'')='' THEN s.IsActive ELSE r.IsActive END)		
	FROM CLM_ReserveDetails r 
		INNER JOIN CLM_ReserveDetails s
	ON r.AccidentClaimId =@ReportedAccidentClaimid
		AND s.AccidentClaimId =@UnReportedAccidentClaimid
		AND r.ReserveId =s.ReserveId 
		AND r.CmpCode =s.CmpCode 
		AND ISNULL(r.PaymentId,'') =ISNULL(s.PaymentId,'')
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO CLM_ReserveDetails(ReserveId,CmpCode,InitialReserve,PreReserve,MovementReserve,CurrentReserve,PaymentId,InitialNoofdays,
			MovementNoofdays,CurrentNoofdays,InitialRateperday,MovementlRateperday,CurrentRateperday,Createdby,Createddate,Modifiedby,
			Modifieddate,IsActive,AccidentClaimId)
	SELECT ReserveId,CmpCode,InitialReserve,PreReserve,MovementReserve,CurrentReserve,PaymentId,InitialNoofdays,
			MovementNoofdays,CurrentNoofdays,InitialRateperday,MovementlRateperday,CurrentRateperday,Createdby,Createddate,Modifiedby,
			Modifieddate,IsActive,@ReportedAccidentClaimid
	FROM CLM_ReserveDetails 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND ReserveDetailID NOT IN 
	(
		SELECT s.ReserveDetailID
		FROM CLM_ReserveDetails r 
			INNER JOIN CLM_ReserveDetails s
		ON r.AccidentClaimId =@ReportedAccidentClaimid
			AND s.AccidentClaimId =@UnReportedAccidentClaimid
			AND r.ReserveId =s.ReserveId 
			AND r.CmpCode =s.CmpCode 
			AND ISNULL(r.PaymentId,'') =ISNULL(s.PaymentId,'')
	)

--Table CLM_MandateSummary

--select * from CLM_MandateSummary

UPDATE t
	SET	
		t.ClaimType=(CASE WHEN COALESCE(t.ClaimType,'')='' THEN u.ClaimType ELSE t.ClaimType END),
		t.MovementType=(CASE WHEN COALESCE(t.MovementType,'')='' THEN u.MovementType ELSE t.MovementType END),
		t.AssignedTo=(CASE WHEN COALESCE(t.AssignedTo,'')='' THEN u.AssignedTo ELSE t.AssignedTo END),
		t.InvestigationResult=(CASE WHEN COALESCE(t.InvestigationResult,'')='' THEN u.InvestigationResult ELSE t.InvestigationResult END),
		t.Scenario=(CASE WHEN COALESCE(t.Scenario,'')='' THEN u.Scenario ELSE t.Scenario END),
		t.Evidence=(CASE WHEN COALESCE(t.Evidence,'')='' THEN u.Evidence ELSE t.Evidence END),
		t.RelatedFacts=(CASE WHEN COALESCE(t.RelatedFacts,'')='' THEN u.RelatedFacts ELSE t.RelatedFacts END),
		t.COAssessment=(CASE WHEN COALESCE(t.COAssessment,'')='' THEN u.COAssessment ELSE t.COAssessment END),
		t.SupervisorAssignto=(CASE WHEN COALESCE(t.SupervisorAssignto,'')='' THEN u.SupervisorAssignto ELSE t.SupervisorAssignto END),
		t.ApproveRecommedations=(CASE WHEN COALESCE(t.ApproveRecommedations,'')='' THEN u.ApproveRecommedations ELSE t.ApproveRecommedations END),
		t.SupervisorRemarks=(CASE WHEN COALESCE(t.SupervisorRemarks,'')='' THEN u.SupervisorRemarks ELSE t.SupervisorRemarks END),
		t.PreMandate=(CASE WHEN COALESCE(t.PreMandate,'')='' THEN u.PreMandate ELSE t.PreMandate END),
		t.MovementMandate=(CASE WHEN COALESCE(t.MovementMandate,'')='' THEN u.MovementMandate ELSE t.MovementMandate END),
		t.CurrentMandate=(CASE WHEN COALESCE(t.CurrentMandate,'')='' THEN u.CurrentMandate ELSE t.CurrentMandate END),		
		t.InformSafetytoreviewfindings=(CASE WHEN COALESCE(t.InformSafetytoreviewfindings,'')='' THEN u.InformSafetytoreviewfindings ELSE t.InformSafetytoreviewfindings END),
		t.MandateRecordNo=(CASE WHEN COALESCE(t.MandateRecordNo,'')='' THEN u.MandateRecordNo ELSE t.MandateRecordNo END),		
		t.Modifieddate=GETDATE(),
		t.IsActive=(CASE WHEN COALESCE(t.IsActive,'')='' THEN u.IsActive ELSE t.IsActive END),
		t.PreMandateSP=(CASE WHEN COALESCE(t.PreMandateSP,'')='' THEN u.PreMandateSP ELSE t.PreMandateSP END),
		t.MovementMandateSP=(CASE WHEN COALESCE(t.MovementMandateSP,'')='' THEN u.MovementMandateSP ELSE t.MovementMandateSP END),
		t.CurrentMandateSP=(CASE WHEN COALESCE(t.CurrentMandateSP,'')='' THEN u.CurrentMandateSP ELSE t.CurrentMandateSP END),
		t.PreviousOffers=(CASE WHEN COALESCE(t.PreviousOffers,'')='' THEN u.PreviousOffers ELSE t.PreviousOffers END),
		t.TPCounterOffer=(CASE WHEN COALESCE(t.TPCounterOffer,'')='' THEN u.TPCounterOffer ELSE t.TPCounterOffer END)
	FROM CLM_MandateSummary t 
		INNER JOIN CLM_MandateSummary u
	ON t.AccidentClaimId =@ReportedAccidentClaimid
		AND u.AccidentClaimId =@UnReportedAccidentClaimid
		AND t.ReserveId=u.ReserveId 
		AND t.ClaimID =u.ClaimID 
		AND t.ClaimType =u.ClaimType 
		AND ISNULL(t.MovementType,'') =ISNULL(u.MovementType,'') 
		AND ISNULL(t.AssignedTo,'') =ISNULL(t.AssignedTo,'')
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO  CLM_MandateSummary(AccidentClaimId,ReserveId,ClaimID,ClaimType,MovementType,AssignedTo,InvestigationResult,Scenario,
		Evidence,RelatedFacts,COAssessment,SupervisorAssignto,ApproveRecommedations,SupervisorRemarks,PreMandate,MovementMandate,CurrentMandate,
		PaymentId,InformSafetytoreviewfindings,MandateRecordNo,Createdby,Createddate,Modifiedby,Modifieddate,IsActive,PreMandateSP,
		MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer)
	SELECT @ReportedAccidentClaimid,ReserveId,ClaimID,ClaimType,MovementType,AssignedTo,InvestigationResult,Scenario,
		Evidence,RelatedFacts,COAssessment,SupervisorAssignto,ApproveRecommedations,SupervisorRemarks,PreMandate,MovementMandate,CurrentMandate,
		PaymentId,InformSafetytoreviewfindings,MandateRecordNo,Createdby,Createddate,Modifiedby,Modifieddate,IsActive,PreMandateSP,
		MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer
	FROM CLM_MandateSummary 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND MandateId NOT IN 
	(
		SELECT u.MandateId
		FROM CLM_MandateSummary t 
			INNER JOIN CLM_MandateSummary u
		ON t.AccidentClaimId =@ReportedAccidentClaimid
			AND u.AccidentClaimId =@UnReportedAccidentClaimid
			AND t.ReserveId=u.ReserveId 
			AND t.ClaimID =u.ClaimID 
			AND t.ClaimType =u.ClaimType 
			AND ISNULL(t.MovementType,'') =ISNULL(u.MovementType,'') 
			AND ISNULL(t.AssignedTo,'') =ISNULL(t.AssignedTo,'')	
	)

--Table CLM_MandateDetails

--select * from CLM_MandateDetails

UPDATE v
	SET			
		v.CmpCode=(CASE WHEN COALESCE(v.CmpCode,'')='' THEN w.CmpCode ELSE v.CmpCode END),
		v.PreMandate=(CASE WHEN COALESCE(v.PreMandate,'')='' THEN w.PreMandate ELSE v.PreMandate END),
		v.MovementMandate=(CASE WHEN COALESCE(v.MovementMandate,'')='' THEN w.MovementMandate ELSE v.MovementMandate END),
		v.CurrentMandate=(CASE WHEN COALESCE(v.CurrentMandate,'')='' THEN w.CurrentMandate ELSE v.CurrentMandate END),		
		v.Modifieddate=GETDATE(),
		v.IsActive=(CASE WHEN COALESCE(v.IsActive,'')='' THEN w.IsActive ELSE v.IsActive END),				
		v.PreMandateSP=(CASE WHEN COALESCE(v.PreMandateSP,'')='' THEN w.PreMandateSP ELSE v.PreMandateSP END),
		v.MovementMandateSP=(CASE WHEN COALESCE(v.MovementMandateSP,'')='' THEN w.MovementMandateSP ELSE v.MovementMandateSP END),
		v.CurrentMandateSP=(CASE WHEN COALESCE(v.CurrentMandateSP,'')='' THEN w.CurrentMandateSP ELSE v.CurrentMandateSP END),
		v.PreviousOffers=(CASE WHEN COALESCE(v.PreviousOffers,'')='' THEN w.PreviousOffers ELSE v.PreviousOffers END),
		v.TPCounterOffer=(CASE WHEN COALESCE(v.TPCounterOffer,'')='' THEN w.TPCounterOffer ELSE v.TPCounterOffer END)		
	FROM CLM_MandateDetails v 
		INNER JOIN CLM_MandateDetails w
	ON v.AccidentClaimId =@ReportedAccidentClaimid
		AND w.AccidentClaimId =@UnReportedAccidentClaimid
		AND v.MandateId=w.MandateId 
		AND v.CmpCode =w.CmpCode 
		AND ISNULL(v.PaymentId,'') =ISNULL(w.PaymentId,'') 
		AND ISNULL(v.ClaimID,'') =ISNULL(w.ClaimID,'')
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO CLM_MandateDetails(MandateId,CmpCode,PreMandate,MovementMandate,CurrentMandate,PaymentId,Createdby,Createddate,Modifiedby,
				Modifieddate,IsActive,AccidentClaimId,ClaimID,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer)
	SELECT MandateId,CmpCode,PreMandate,MovementMandate,CurrentMandate,PaymentId,Createdby,Createddate,Modifiedby,
				Modifieddate,IsActive,@ReportedAccidentClaimid,ClaimID,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,
				TPCounterOffer
	FROM CLM_MandateDetails 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND MandateDetailId NOT IN 
	(
		SELECT w.MandateDetailId
		FROM CLM_MandateDetails v 
			INNER JOIN CLM_MandateDetails w
		ON v.AccidentClaimId =@ReportedAccidentClaimid
			AND w.AccidentClaimId =@UnReportedAccidentClaimid
			AND v.MandateId=w.MandateId 
			AND v.CmpCode =w.CmpCode 
			AND ISNULL(v.PaymentId,'') =ISNULL(w.PaymentId,'') 
			AND ISNULL(v.ClaimID,'') =ISNULL(w.ClaimID,'')
	)

--Table CLM_PaymentSummary

--select * from CLM_PaymentSummary

UPDATE x
	SET	
		x.Payee=(CASE WHEN COALESCE(x.Payee,'')='' THEN y.Payee ELSE x.Payee END),
		x.AssignedToSupervisor=(CASE WHEN COALESCE(x.AssignedToSupervisor,'')='' THEN y.AssignedToSupervisor ELSE x.AssignedToSupervisor END),
		x.TotalPaymentDue=(CASE WHEN COALESCE(x.TotalPaymentDue,'')='' THEN y.TotalPaymentDue ELSE x.TotalPaymentDue END),
		x.TotalAmountMandate=(CASE WHEN COALESCE(x.TotalAmountMandate,'')='' THEN y.TotalAmountMandate ELSE x.TotalAmountMandate END),		
		x.Modifieddate=GETDATE(),
		x.PaymentRequestDate=(CASE WHEN COALESCE(x.PaymentRequestDate,'')='' THEN y.PaymentRequestDate ELSE x.PaymentRequestDate END),
		x.PaymentDueDate=(CASE WHEN COALESCE(x.PaymentDueDate,'')='' THEN y.PaymentDueDate ELSE x.PaymentDueDate END),
		x.AssignedTo=(CASE WHEN COALESCE(x.AssignedTo,'')='' THEN y.AssignedTo ELSE x.AssignedTo END),
		x.ClaimantName=(CASE WHEN COALESCE(x.ClaimantName,'')='' THEN y.ClaimantName ELSE x.ClaimantName END),
		x.PaymentRecordNo=(CASE WHEN COALESCE(x.PaymentRecordNo,'')='' THEN y.PaymentRecordNo ELSE x.PaymentRecordNo END),
		x.ClaimType=(CASE WHEN COALESCE(x.ClaimType,'')='' THEN y.ClaimType ELSE x.ClaimType END),
		x.IsActive=(CASE WHEN COALESCE(x.IsActive,'')='' THEN y.IsActive ELSE x.IsActive END),
		x.Address=(CASE WHEN COALESCE(x.Address,'')='' THEN y.Address ELSE x.Address END),
		x.Address1=(CASE WHEN COALESCE(x.Address1,'')='' THEN y.Address1 ELSE x.Address1 END),
		x.Address2=(CASE WHEN COALESCE(x.Address2,'')='' THEN y.Address2 ELSE x.Address2 END),
		x.PostalCodes=(CASE WHEN COALESCE(x.PostalCodes,'')='' THEN y.PostalCodes ELSE x.PostalCodes END),
		x.CoRemarks=(CASE WHEN COALESCE(x.CoRemarks,'')='' THEN y.CoRemarks ELSE x.CoRemarks END),
		x.ApprovePayment=(CASE WHEN COALESCE(x.ApprovePayment,'')='' THEN y.ApprovePayment ELSE x.ApprovePayment END),
		x.SupervisorRemarks=(CASE WHEN COALESCE(x.SupervisorRemarks,'')='' THEN y.SupervisorRemarks ELSE x.SupervisorRemarks END),
		x.ApprovedDate=(CASE WHEN COALESCE(x.ApprovedDate,'')='' THEN y.ApprovedDate ELSE x.ApprovedDate END),
		x.MovementType=(CASE WHEN COALESCE(x.MovementType,'')='' THEN y.MovementType ELSE x.MovementType END)		
	FROM CLM_PaymentSummary x 
		INNER JOIN CLM_PaymentSummary y
	ON x.AccidentClaimId =@ReportedAccidentClaimid
		AND y.AccidentClaimId =@UnReportedAccidentClaimid
		AND ISNULL(x.MandateId,'') =ISNULL(y.MandateId,'') 
		AND ISNULL(x.ReserveId,'') =ISNULL(y.ReserveId,'') 
		AND ISNULL(x.MovementType,'') =ISNULL(y.MovementType,'')
		AND ISNULL(x.ClaimID,'') =ISNULL(y.ClaimID,'') 
		AND ISNULL(x.ClaimType,'') =ISNULL(y.ClaimType,'') 
		
	--INSERT IF NOT EXISTS
	
	INSERT INTO CLM_PaymentSummary(AccidentClaimId,PolicyId,Payee,AssignedToSupervisor,TotalPaymentDue,TotalAmountMandate,
			Createddate,Modifieddate,PaymentRequestDate,PaymentDueDate,CreatedBy,Modifiedby,AssignedTo,ClaimantName,PaymentRecordNo,
			ClaimType,IsActive,ClaimID,Address,Address1,Address2,PostalCodes,CoRemarks,ApprovePayment,SupervisorRemarks,ApprovedDate,
			MovementType,MandateId,ReserveId)
	SELECT @ReportedAccidentClaimid,PolicyId,Payee,AssignedToSupervisor,TotalPaymentDue,TotalAmountMandate,
			Createddate,Modifieddate,PaymentRequestDate,PaymentDueDate,CreatedBy,Modifiedby,AssignedTo,ClaimantName,PaymentRecordNo,
			ClaimType,IsActive,ClaimID,Address,Address1,Address2,PostalCodes,CoRemarks,ApprovePayment,SupervisorRemarks,ApprovedDate,
			MovementType,MandateId,ReserveId 
	FROM CLM_PaymentSummary 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND PaymentId NOT IN 
	(
		SELECT y.PaymentId
		FROM CLM_PaymentSummary x 
			INNER JOIN CLM_PaymentSummary y
		ON x.AccidentClaimId =@ReportedAccidentClaimid
			AND y.AccidentClaimId =@UnReportedAccidentClaimid
			AND ISNULL(x.MandateId,'') =ISNULL(y.MandateId,'') 
			AND ISNULL(x.ReserveId,'') =ISNULL(y.ReserveId,'') 
			AND ISNULL(x.MovementType,'') =ISNULL(y.MovementType,'')
			AND ISNULL(x.ClaimID,'') =ISNULL(y.ClaimID,'') 
			AND ISNULL(x.ClaimType,'') =ISNULL(y.ClaimType,'') 
	)

--Table CLM_PaymentDetails

--select * from CLM_PaymentDetails

UPDATE z
	SET
		z.CmpCode=(CASE WHEN COALESCE(z.CmpCode,'')='' THEN a.CmpCode ELSE z.CmpCode END),
		z.TotalPaymentDue=(CASE WHEN COALESCE(z.TotalPaymentDue,'')='' THEN a.TotalPaymentDue ELSE z.TotalPaymentDue END),
		z.TotalAmountMandate=(CASE WHEN COALESCE(z.TotalAmountMandate,'')='' THEN a.TotalAmountMandate ELSE z.TotalAmountMandate END),
		z.Createdby=(CASE WHEN COALESCE(z.Createdby,'')='' THEN a.Createdby ELSE z.Createdby END),
		z.Createddate=(CASE WHEN COALESCE(z.Createddate,'')='' THEN a.Createddate ELSE z.Createddate END),
		z.Modifiedby=(CASE WHEN COALESCE(z.Modifiedby,'')='' THEN a.Modifiedby ELSE z.Modifiedby END),
		z.Modifieddate=(CASE WHEN COALESCE(z.Modifieddate,'')='' THEN a.Modifieddate ELSE z.Modifieddate END),
		z.IsActive=(CASE WHEN COALESCE(z.IsActive,'')='' THEN a.IsActive ELSE z.IsActive END)		
	FROM CLM_PaymentDetails z 
		INNER JOIN CLM_PaymentDetails a
	ON z.AccidentClaimId =@ReportedAccidentClaimid
		AND a.AccidentClaimId = @UnReportedAccidentClaimid
		AND z.CmpCode =a.CmpCode 
		AND z.ReserveId =a.ReserveId 
		AND z.MandateId =a.MandateId 
		AND z.PaymentId =a.PaymentId
		
	--INSERT IF NOT EXISTS
	INSERT INTO CLM_PaymentDetails(CmpCode,TotalPaymentDue,TotalAmountMandate,Createdby,Createddate,Modifiedby,Modifieddate,
									IsActive,AccidentClaimId,ReserveId,MandateId,PaymentId)
	SELECT CmpCode,TotalPaymentDue,TotalAmountMandate,Createdby,Createddate,Modifiedby,Modifieddate,
									IsActive,@ReportedAccidentClaimid,ReserveId,MandateId,PaymentId
	FROM CLM_PaymentDetails 
	WHERE AccidentClaimId=@UnReportedAccidentClaimid AND PaymentDetailID NOT IN 
	(
		SELECT a.PaymentDetailID
		FROM CLM_PaymentDetails z 
			INNER JOIN CLM_PaymentDetails a
		ON z.AccidentClaimId =@ReportedAccidentClaimid
			AND a.AccidentClaimId = @UnReportedAccidentClaimid
			AND z.CmpCode =a.CmpCode 
			AND z.ReserveId =a.ReserveId 
			AND z.MandateId =a.MandateId 
			AND z.PaymentId =a.PaymentId
	)
	
	
	--NOW UPDATING IsReadOnly && LinkedClaimId
	
	UPDATE ClaimAccidentDetails 
	SET IsReadOnly=1, LinkedAccidentClaimId=@ReportedAccidentClaimid
	WHERE AccidentClaimId=@UnReportedAccidentClaimid
	
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
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
GO