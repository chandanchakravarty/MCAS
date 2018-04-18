/****** Object:  StoredProcedure [dbo].[Proc_CreateReportedClaimFromUpload]    Script Date: 03/12/2015 15:48:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CreateReportedClaimFromUpload]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CreateReportedClaimFromUpload]
GO

/****** Object:  StoredProcedure [dbo].[Proc_CreateReportedClaimFromUpload]    Script Date: 03/12/2015 15:48:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc [Proc_CreateReportedClaimFromUpload]
--GO

/*************************
Created by: Pravesh K Chandel
Created Date: 2 March 2015
Purpose	: Convert uploaded Claim Data to create new Reported Claim
drop proc [Proc_CreateReportedClaimFromUpload]
**************************/

CREATE PROCEDURE [dbo].[Proc_CreateReportedClaimFromUpload] 
(
@FileRefNo varchar(20),
@User  nvarchar(20) ='System'
)
AS
BEGIN

declare @FailedCount int,
		@SuccessCount int,
		@HistoryId_CLAIM varchar(20),
		@HistoryId_STD_CD  varchar(20),
		@STG_Uploaded_ID int,
		
		@AccidentClaimId int,

		@IpNumber varchar(20),
		@ReportDate varchar(20),
		@AccidentDateTime varchar(20),
		@PlaceOfAccident nvarchar(1000),
		@OperatingHours varchar(20),
		@DamageToBus nvarchar(max),
		@FactsOfIncident nvarchar(max),
		@FinalLiabilityDate varchar(20),
		@FinalFinding varchar(100),
		@ReportNo varchar(50),
		@BusNumber varchar(50),
		@ServiceNo varchar(20),
		@FinalLiability varchar(50),
		@StaffNo varchar(30),
		@FileRefId int,
		@Status varchar(10),
		@DutyIO varchar(10),
		@HistoryIds varchar(20),
		@DutyIOName nvarchar(100),
		
		@ClaimNo nvarchar(20),
		@Make nvarchar(50),
		@Model nvarchar(50),
		@DriverEmployeeNo nvarchar(50),
		@DriverName nvarchar(200),
		@DriverNRICNo nvarchar(20),
		@DriverMobileNo nvarchar(18),
		@Organization varchar(10),
		@OrganizationType varchar(10),
		@OrganizationId int,
		
		@DD varchar(2),
		@MM varchar(2),
		@YYYY varchar(4),
		@HHMM varchar(10),
		@tempDateTime varchar(20),
		@tempAccidentTimePart varchar(20)

select @HistoryId_CLAIM = UploadHistoryId,@OrganizationType=OrganizationType,@Organization = OrganizationName from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='CLM'
select @HistoryId_STD_CD = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='CLM_STD_CD'
SELECT @OrganizationId = Id from MNT_OrgCountry where CountryOrgazinationCode=@Organization
declare @FileHistoryIds varchar(50) = 'CLM^' + @HistoryId_CLAIM + '~STD^' + @HistoryId_STD_CD

begin try
--select IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FinalLiabilityDate,FinalFinding,ReportNo,
--			BusNumber,ServiceNo,FinalLiability,StaffNo,FileRefId,Status,DutyIO,HistoryIds,*
--			from STG_UploadedFileData where FileRefNo = @FileRefNo and IsProcessed=0 and [Status] = 'Ready'
			
	DECLARE CR_UPLOAD_CLAIM CURSOR FOR  
		 select ID as STG_Uploaded_ID, IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FinalLiabilityDate,FinalFinding,ReportNo,
			BusNumber,ServiceNo,FinalLiability,StaffNo,FileRefId,Status,DutyIO,HistoryIds
			from STG_UploadedFileData where FileRefNo = @FileRefNo and IsProcessed=0 and [Status] = 'Ready' and HistoryIds =@FileHistoryIds
		 
	OPEN CR_UPLOAD_CLAIM  
	 FETCH NEXT  FROM CR_UPLOAD_CLAIM into @STG_Uploaded_ID,@IpNumber,@ReportDate,@AccidentDateTime,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@FinalLiabilityDate,@FinalFinding,@ReportNo,
			@BusNumber,@ServiceNo,@FinalLiability,@StaffNo,@FileRefId,@Status,@DutyIO,@HistoryIds
	  WHILE @@FETCH_STATUS = 0  
	  BEGIN 
		-- CLAIM and Standard Code File Data match	
		 --select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo = @FileRefNo 
		 --select @STG_Uploaded_ID,@IpNumber,@ReportDate,@AccidentDateTime,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@FinalLiabilityDate,@FinalFinding,@ReportNo,
			--@BusNumber,@ServiceNo,@FinalLiability,@StaffNo,@FileRefId,@Status,@DutyIO,@HistoryIds
		
		--select 
		--PolicyId,IPNo,ClaimNo,BusServiceNo,VehicleNo,AccidentDate,AccidentTime,ReportedDate,Facts,Damages,DateofFinding,InvestStatus,Results,BOIResults,FinalLiability,DutyIO,
		--Make,ModelNo,DriverEmployeeNo,DriverName,DriverNRICNo,DriverMobileNo,InitialEstimate,Organization,LossTypeCode,LossNatureCode,BusCaptainFault,TempClaimNo,
		--CreatedDate,CreatedBy,AccidentLoc,IsComplete,OperatingHours,IsReported
		--from ClaimAccidentDetails
		
		-----@AccidentDateTime validation
		begin try
			set @tempAccidentTimePart = convert(nvarchar,cast(@AccidentDateTime as datetime),108)
			set @AccidentDateTime = convert(nvarchar,cast(@AccidentDateTime as datetime),101)
			select @DD =DAY(@AccidentDateTime),@MM = MONTH(@AccidentDateTime),@YYYY=YEAR(@AccidentDateTime)
			set @AccidentDateTime =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end try
		begin catch
			set @DD = SUBSTRING(@AccidentDateTime,1,CHARINDEX('/',@AccidentDateTime,1)-1)
			set @tempDateTime = SUBSTRING(@AccidentDateTime,CHARINDEX('/',@AccidentDateTime,1)+1,LEN(@AccidentDateTime))		
			set @MM = SUBSTRING(@tempDateTime,1,CHARINDEX('/',@tempDateTime,1)-1)
			set @tempDateTime = SUBSTRING(@tempDateTime,CHARINDEX('/',@tempDateTime,1)+1,LEN(@tempDateTime))	
			set @YYYY = SUBSTRING(@tempDateTime,1,CHARINDEX(' ',@tempDateTime,1)-1)
			set @tempAccidentTimePart = SUBSTRING(@tempDateTime,CHARINDEX(' ',@tempDateTime,1)+1,LEN(@tempDateTime))	
			set @AccidentDateTime =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end catch
		-------@ReportDate Validation
		begin try
			set @ReportDate = convert(nvarchar,cast(@ReportDate as datetime),101)
			select @DD =DAY(@ReportDate),@MM = MONTH(@ReportDate),@YYYY=YEAR(@ReportDate)
			set @ReportDate =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end try
		begin catch
			set @DD = SUBSTRING(@ReportDate,1,CHARINDEX('/',@ReportDate,1)-1)
			set @tempDateTime = SUBSTRING(@ReportDate,CHARINDEX('/',@ReportDate,1)+1,LEN(@ReportDate))		
			set @MM = SUBSTRING(@tempDateTime,1,CHARINDEX('/',@tempDateTime,1)-1)
			set @tempDateTime = SUBSTRING(@tempDateTime,CHARINDEX('/',@tempDateTime,1)+1,LEN(@tempDateTime))	
			--select @tempDateTime, CHARINDEX(' ',@tempDateTime,1)-1
			if CHARINDEX(' ',@tempDateTime,1)-1>-1
			set @YYYY = SUBSTRING(@tempDateTime,1,CHARINDEX(' ',@tempDateTime,1)-1)
			else
			set @YYYY = @tempDateTime
			set @ReportDate =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end catch
		--select @ReportDate
		-----------@FinalLiabilityDate validation
		--select @FinalLiabilityDate as FinalLiabilityDate
		begin try
			set @FinalLiabilityDate = convert(nvarchar,cast(@FinalLiabilityDate as datetime),101)
			select @DD =DAY(@FinalLiabilityDate),@MM = MONTH(@FinalLiabilityDate),@YYYY=YEAR(@FinalLiabilityDate)
			set @FinalLiabilityDate =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end try
		begin catch
			--select @FinalLiabilityDate
			set @DD = SUBSTRING(@FinalLiabilityDate,1,CHARINDEX('/',@FinalLiabilityDate,1)-1)
			set @tempDateTime = SUBSTRING(@FinalLiabilityDate,CHARINDEX('/',@FinalLiabilityDate,1)+1,LEN(@FinalLiabilityDate))		
			set @MM = SUBSTRING(@tempDateTime,1,CHARINDEX('/',@tempDateTime,1)-1)
			set @tempDateTime = SUBSTRING(@tempDateTime,CHARINDEX('/',@tempDateTime,1)+1,LEN(@tempDateTime))	
			--select @tempDateTime, CHARINDEX(' ',@tempDateTime,1)-1
			if CHARINDEX(' ',@tempDateTime,1)-1>-1
			set @YYYY = SUBSTRING(@tempDateTime,1,CHARINDEX(' ',@tempDateTime,1)-1)
			else
			set @YYYY = @tempDateTime
			set @FinalLiabilityDate =CAST(	@MM +'/' + @DD + '/' + @YYYY as datetime)
		end catch
		-------@FinalLiability validation
		select @FinalLiability = Lookupvalue from MNT_Lookups(nolock) where Category = 'InvestigationResult' and lookupCode=@FinalLiability
		if ISNUMERIC(@FinalLiability)<>1
			set @FinalLiability =0
			
		-------@fetching Make Model from Vehicle Master for given bus number
		if exists(select * from MNT_VehicleListingMaster (nolock) where VehicleRegNo = @BusNumber)
		begin
		select @Make = VehicleMakeCode,@Model= VehicleModelCode from MNT_VehicleListingMaster (nolock) where VehicleRegNo = @BusNumber
		end
		else
		select @Make = '',@Model= ''
		
		-------@fetching Bus Captain data from from Bus Caption Master for given StaffNO
		if exists(select * from MNT_BusCaptain where BusCaptainCode = @StaffNo)
		begin
		select @DriverEmployeeNo = cast(TranId as nvarchar), @DriverName = BusCaptainName,@DriverNRICNo = NRICPassportNo, @DriverMobileNo = ContactNo from MNT_BusCaptain where  BusCaptainCode = @StaffNo
		end
		else
		select @DriverEmployeeNo = '',@DriverName='',@DriverNRICNo='',@DriverMobileNo='0'
		--geting Operating hours lookup value id		
		select @OperatingHours = Lookupvalue from MNT_Lookups (nolock) where Category ='OperatingHours' and LookupCode=@OperatingHours
		--geting DutyIO Name
		select @DutyIOName = [Description] from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo = @FileRefNo and HistoryId=@HistoryId_STD_CD and STD_CODE_TYPE ='22' and STD_CODE = @DutyIO
		--geting final Finding Description
		select @FinalFinding = [Description] from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo = @FileRefNo and HistoryId=@HistoryId_STD_CD and STD_CODE_TYPE ='21' and STD_CODE = @FinalFinding
		

			insert into ClaimAccidentDetails
			(
			PolicyId,
			IPNo,
			ClaimNo,
			BusServiceNo,
			VehicleNo,
			AccidentDate,
			AccidentTime,
			ReportedDate,
			Facts,
			Damages,
			DateofFinding,
			InvestStatus,
			Results,
			BOIResults,
			FinalLiability,
			DutyIO,
			Make,
			ModelNo,
			DriverEmployeeNo,
			DriverName,
			DriverNRICNo,
			DriverMobileNo,
			InitialEstimate,
			Organization,
			LossTypeCode,
			LossNatureCode,
			BusCaptainFault,
			TempClaimNo,
			CreatedDate,
			CreatedBy,
			AccidentLoc,
			IsComplete,
			OperatingHours,
			IsReported,
			ReportedRefId
			)
		select 
			0 as PolicyId,
			@IpNumber,
			@ClaimNo,
			@ServiceNo as BusServiceNo,
			@BusNumber as VehicleNo,
			cast(@AccidentDateTime as datetime) as AccidentDate,
			 rtrim(ltrim(@tempAccidentTimePart))as AccidentTime,
			@ReportDate,
			@FactsOfIncident as Facts,
			@DamageToBus as Damages,
			@FinalLiabilityDate as DateofFinding,
			null as InvestStatus,
			@FinalFinding as Results,
			'' as BOIResults,
			@FinalLiability as FinalLiability,
			@DutyIOName as DutyIO,
			@Make as Malke,
			@Model as ModelNo,
			@DriverEmployeeNo as DriverEmployeeNo,
			@DriverName as DriverName,
			@DriverNRICNo as DriverNRICNo ,
			@DriverMobileNo as DriverMobileNo,
			null as InitialEstimate,
			@OrganizationId as Organization,
			null as LossTypeCode,
			null as LossNatureCode,
			null as BusCaptainFault,
			'' as TempClaimNo,
			GETDATE() as CreatedDate,
			@User as CreatedBy,
			@PlaceOfAccident as AccidentLoc,
			1 as IsComplete,
			@OperatingHours as OperatingHours,
			1 as IsReported,
			@STG_Uploaded_ID
			

		set @AccidentClaimId = @@IDENTITY
		
		--update ClaimAccidentDetails set ClaimNo = @ClaimNo where AccidentClaimId = @AccidentClaimId
		exec Proc_GetClaimNo @AccidentClaimId
		select @ClaimNo = claimno from ClaimAccidentDetails (nolock) where AccidentClaimId = @AccidentClaimId
		if(@ClaimNo is null)
		begin
			select 'Claim Number could not be generated.'
		end	
		else
		begin
			update ClaimAccidentDetails set IsComplete=1 where AccidentClaimId = @AccidentClaimId
		end
			
		update STG_UploadedFileData set IsProcessed=1 , [Status] = 'Converted' where FileRefNo = @FileRefNo and  ID = @STG_Uploaded_ID
			 
	FETCH NEXT  FROM CR_UPLOAD_CLAIM into @STG_Uploaded_ID,@IpNumber,@ReportDate,@AccidentDateTime,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@FinalLiabilityDate,@FinalFinding,@ReportNo,
			@BusNumber,@ServiceNo,@FinalLiability,@StaffNo,@FileRefId,@Status,@DutyIO,@HistoryIds
  END
 CLOSE CR_UPLOAD_CLAIM  
 DEALLOCATE CR_UPLOAD_CLAIM  
 
end try
begin catch

--Error while creating Reported Claim.Invalid length parameter passed to the LEFT or SUBSTRING function.

SELECT 0,@FileRefNo,
		  ERROR_NUMBER() AS ErrorNumber
		 ,ERROR_SEVERITY() AS ErrorSeverity
		 ,ERROR_STATE() AS ErrorState
		 ,ERROR_PROCEDURE() AS ErrorProcedure
		 ,ERROR_LINE() AS ErrorLine
		 ,'Error while creating Reported Claim.' + ERROR_MESSAGE()  AS ErrorMessage
		 ,getdate()
		 ,@User
		 
		insert into STG_FileUpload_ERRORS
		(
			FileId,
			FileRefNo,
			ErrorNumber,
			ErrorSeverity,
			ErrorState,
			ErrorProcedure,
			ErrorLine,
			ErrorMessage,
			createDateTime,
			createBy
		)
		  SELECT 0,@FileRefNo,
		  ERROR_NUMBER() AS ErrorNumber
		 ,ERROR_SEVERITY() AS ErrorSeverity
		 ,ERROR_STATE() AS ErrorState
		 ,ERROR_PROCEDURE() AS ErrorProcedure
		 ,ERROR_LINE() AS ErrorLine
		 ,'Error while creating Reported Claim.' + ERROR_MESSAGE()  AS ErrorMessage
		 ,getdate()
		 ,@User
		 
		 update STG_UploadedFileData set IsProcessed=1,Status='Failed',ExceptionDetails='Error while creating Reported Claim. ' + ERROR_MESSAGE()  where FileRefNo = @FileRefNo  and ID = @STG_Uploaded_ID --and HistoryIds=@HistoryId_CLAIM
		 --select * from STG_UploadedFileData where FileRefNo = @FileRefNo and [Status]='Failed'

		  update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(), HasError='Y', ErrorMessage = 'Error while creating Reported Claim.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo
		  
		CLOSE CR_UPLOAD_CLAIM  
		DEALLOCATE CR_UPLOAD_CLAIM 
 
end catch

END

--GO
--declare @FileRefNo varchar(20) ='CLM-40'
--exec [Proc_CreateReportedClaimFromUpload] @FileRefNo,'System'

--select * from STG_UploadedFileData where FileRefNo = 'CLM-40' 
--select * from ClaimAccidentDetails where IsReported=1

--select * from MNT_FileUpload where FileRefNo =@FileRefNo 
--select * from STG_UploadedFileData where FileRefNo = @FileRefNo --and [Status]='Failed'
--/*
--select * from STG_CLAIM_FILE_DATA where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'
--select * from STG_CLAIM_FILE_DATA where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'

--select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'
--select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'
--*/

--rollback tran





GO


