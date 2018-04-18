CREATE PROCEDURE [dbo].[Proc_CreateReportedClaimFromUpload]  
 @FileRefNo [varchar](20),  
 @User [nvarchar](20) = 'System'  
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
  --Added by Shikha For Client Issue
  @DriverDateJoined varchar(20),
  @DriverDateResigned varchar(20),
  @Organization varchar(10),  
  @OrganizationType varchar(10),  
  @OrganizationId int,  
  @InitialLiability nvarchar(20),  
  @CollisionType nvarchar(10),  
  @CollisionTypeDesc nvarchar(500),  
  @DistrictCode nvarchar(10),
  @DistrictCodeDesc nvarchar(100),
  @RoadName nvarchar(100),
  @SeriousInjury nvarchar(50),
  @MinorInjury nvarchar(50),
  @Fatal nvarchar(50),
    
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
--   BusNumber,ServiceNo,FinalLiability,StaffNo,FileRefId,Status,DutyIO,HistoryIds,*  
--   from STG_UploadedFileData where FileRefNo = @FileRefNo and IsProcessed=0 and [Status] = 'Ready'  
     
 DECLARE CR_UPLOAD_CLAIM CURSOR FOR    
   select ID as STG_Uploaded_ID, IpNumber,ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,FinalLiabilityDate,FinalFinding,ReportNo,BusNumber,ServiceNo,FinalLiability,StaffNo,FileRefId,Status,DutyIO,HistoryIds,InitialLiability,CollisionType,DistrictCode,
   RoadName,SeriousInjury,MinorInjury,Fatal
   from STG_UploadedFileData where FileRefNo = @FileRefNo and IsProcessed=0 and [Status] = 'Ready' and HistoryIds =@FileHistoryIds  
     
 OPEN CR_UPLOAD_CLAIM    
  FETCH NEXT  FROM CR_UPLOAD_CLAIM into @STG_Uploaded_ID,@IpNumber,@ReportDate,@AccidentDateTime,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@FinalLiabilityDate,@FinalFinding,@ReportNo,  
   @BusNumber,@ServiceNo,@FinalLiability,@StaffNo,@FileRefId,@Status,@DutyIO,@HistoryIds,@InitialLiability,@CollisionType,@DistrictCode,@RoadName,
   @SeriousInjury,@MinorInjury,@Fatal
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
 BEGIN TRY   
  -----@AccidentDateTime validation  
  begin try  
   --set @tempAccidentTimePart = convert(nvarchar,cast(@AccidentDateTime as datetime),108)  
   --Edited by Shikha for time format in HH:MM
   set @tempAccidentTimePart = Convert(varchar(5),cast(@AccidentDateTime as datetime),114)
   set @AccidentDateTime = convert(nvarchar,cast(@AccidentDateTime as datetime),101)  
   select @DD =DAY(@AccidentDateTime),@MM = MONTH(@AccidentDateTime),@YYYY=YEAR(@AccidentDateTime)  
   set @AccidentDateTime =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
  end try  
  begin catch  
   set @DD = SUBSTRING(@AccidentDateTime,1,CHARINDEX('/',@AccidentDateTime,1)-1)  
   set @tempDateTime = SUBSTRING(@AccidentDateTime,CHARINDEX('/',@AccidentDateTime,1)+1,LEN(@AccidentDateTime))    
   set @MM = SUBSTRING(@tempDateTime,1,CHARINDEX('/',@tempDateTime,1)-1)  
   set @tempDateTime = SUBSTRING(@tempDateTime,CHARINDEX('/',@tempDateTime,1)+1,LEN(@tempDateTime))   
   set @YYYY = SUBSTRING(@tempDateTime,1,CHARINDEX(' ',@tempDateTime,1)-1)  
   set @tempAccidentTimePart = SUBSTRING(@tempDateTime,CHARINDEX(' ',@tempDateTime,1)+1,LEN(@tempDateTime))   
   set @AccidentDateTime =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
  end catch  
  -------@ReportDate Validation  
  begin try  
   set @ReportDate = convert(nvarchar,cast(@ReportDate as datetime),101)  
   select @DD =DAY(@ReportDate),@MM = MONTH(@ReportDate),@YYYY=YEAR(@ReportDate)  
   set @ReportDate =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
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
   set @ReportDate =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
  end catch  
  --select @ReportDate  
  -----------@FinalLiabilityDate validation  
  --select @FinalLiabilityDate as FinalLiabilityDate  
  begin try  
   set @FinalLiabilityDate = convert(nvarchar,cast(@FinalLiabilityDate as datetime),101)  
   select @DD =DAY(@FinalLiabilityDate),@MM = MONTH(@FinalLiabilityDate),@YYYY=YEAR(@FinalLiabilityDate)  
   set @FinalLiabilityDate =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
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
   set @FinalLiabilityDate =CAST( @MM +'/' + @DD + '/' + @YYYY as datetime)  
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
    
  -------@fetching Bus Captain data from from Bus Caption Master for given StaffNO  -- Alter Query by Shikha for Client Issue - 02-06-2017
  if exists(select * from MNT_BusCaptain where BusCaptainCode = @StaffNo)  
  begin  
  --select @DriverEmployeeNo = cast(TranId as nvarchar), @DriverName = BusCaptainName,@DriverNRICNo = NRICPassportNo, @DriverMobileNo = ContactNo, @DriverDateJoined = DateJoined, @DriverDateResigned = DateResigned from MNT_BusCaptain where  BusCaptainCode = @StaffNo  
  select @DriverEmployeeNo = cast(BusCaptainCode as nvarchar), @DriverName = BusCaptainName,@DriverNRICNo = NRICPassportNo, @DriverMobileNo = ContactNo, @DriverDateJoined = DateJoined, @DriverDateResigned = DateResigned from MNT_BusCaptain where  BusCaptainCode = @StaffNo  
  end  
  else  
  select @DriverEmployeeNo = '',@DriverName='',@DriverNRICNo='',@DriverMobileNo='0',@DriverDateJoined = null,  @DriverDateResigned = null  
  --geting Operating hours lookup value id    
  select @OperatingHours = Lookupvalue from MNT_Lookups (nolock) where Category ='OperatingHours' and LookupCode=@OperatingHours  
  --geting DutyIO Name  
  select @DutyIOName = [Description] from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo = @FileRefNo and HistoryId=@HistoryId_STD_CD and STD_CODE_TYPE ='22' and STD_CODE = @DutyIO  
  --geting final Finding Description  
  select @FinalFinding = [Description] from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo = @FileRefNo and HistoryId=@HistoryId_STD_CD and STD_CODE_TYPE ='21' and STD_CODE = @FinalFinding  
  --geting Collision Type Description  
  --SELECT @CollisionTypeDesc=[Description] from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo = @FileRefNo and HistoryId=@HistoryId_STD_CD and STD_CODE_TYPE ='18' and STD_CODE = @CollisionType    
  --SELECT @CollisionTypeDesc=[Description] from MNT_Lookups (nolock) where Category ='CollisionType' AND rtrim(ltrim(Lookupvalue))=RTRIM(ltrim(@CollisionType))   
    
  --Getting Full value for Initial Liability.  
  --SET @InitialLiability=CASE   
  -- WHEN @InitialLiability='FAF' THEN 'Fully at Fault'  
  -- WHEN @InitialLiability='PAF' THEN 'Partial at Fault'  
  -- WHEN @InitialLiability='NAF' THEN 'Not at Fault'  
  -- END  
  SELECT @InitialLiability=Lookupvalue   
  FROM MNT_Lookups   
  WHERE Category ='InvestigationResult' AND LTRIM (RTRIM(lookupCode))=RTRIM (LTRIM(@InitialLiability))  
    
  -- check for duplicity on Report No., IP No. and/or Vehicle Number to detect for similar entry  
  set @AccidentClaimId=null  
  select @AccidentClaimId = AccidentClaimId from  ClaimAccidentDetails where rtrim(ltrim(IPNo))= LTRIM(rtrim(@IpNumber)) and LTRIM(rtrim(isnull(UploadReportNo,''))) = LTRIM(rtrim(@ReportNo)) and IsReported =1  
  if(@AccidentClaimId is null)  
   select @AccidentClaimId = AccidentClaimId from  ClaimAccidentDetails where rtrim(ltrim(IPNo))= LTRIM(rtrim(@IpNumber)) and LTRIM(rtrim(isnull(VehicleNo,''))) = LTRIM(rtrim(@BusNumber)) and IsReported =1  
    
  if (@AccidentClaimId is null) -- new entry  
    begin    
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
   InitialLiability,  
   CollisionType,  
   DutyIO,  
   Make,  
   ModelNo,  
   DriverEmployeeNo,  
   DriverName,  
   DriverNRICNo,  
   DriverMobileNo, 
   DateJoined,
   DateResigned,  
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
   ReportedRefId,  
   UploadReportNo,
   District,
   RoadName,
   MinorInjury,
   SeriousInjury,
   Fatal  
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
   @InitialLiability as InitialLiability,  
   @CollisionType as CollisionType,  
   @DutyIOName as DutyIO,  
   @Make as Malke,  
   @Model as ModelNo,  
   @DriverEmployeeNo as DriverEmployeeNo,  
   @DriverName as DriverName,  
   @DriverNRICNo as DriverNRICNo ,  
   @DriverMobileNo as DriverMobileNo,  
   @DriverDateJoined as  DateJoined,
   @DriverDateResigned as DateResigned,
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
   @STG_Uploaded_ID,  
   ltrim(rtrim(@ReportNo)),
   @DistrictCode,
   @RoadName,
   @MinorInjury,
   @SeriousInjury,
   @Fatal  
  
  set @AccidentClaimId = @@IDENTITY  
    
  --update ClaimAccidentDetails set ClaimNo = @ClaimNo where AccidentClaimId = @AccidentClaimId  
  --exec Proc_GetClaimNo @AccidentClaimId  
  exec Proc_SetClaimsNo @AccidentClaimId ,'','CLM'  
  select @ClaimNo = claimno from ClaimAccidentDetails (nolock) where AccidentClaimId = @AccidentClaimId  
  if(@ClaimNo is null)  
  begin  
   select 'Claim Number could not be generated.'  
  end   
  else  
  begin  
   update ClaimAccidentDetails set IsComplete=1 where AccidentClaimId = @AccidentClaimId  
  end  
  update STG_UploadedFileData set IsProcessed=1 , [Status] = 'Converted',ExceptionDetails='New entry Uploaded with Claim No. ' + @ClaimNo +';' where FileRefNo = @FileRefNo and  ID = @STG_Uploaded_ID  
  end  
  else  -- update case  
  begin  
      select @ClaimNo = ClaimNo from  ClaimAccidentDetails where AccidentClaimId = @AccidentClaimId  
     
   update ClaimAccidentDetails set  
   BusServiceNo = @ServiceNo,  
   VehicleNo  = @BusNumber,  
   AccidentDate = cast(@AccidentDateTime as datetime),  
   AccidentTime = rtrim(ltrim(@tempAccidentTimePart)),  
   ReportedDate = @ReportDate,  
   Facts   = @FactsOfIncident,  
   Damages   = @DamageToBus,  
   DateofFinding = @FinalLiabilityDate,  
   Results   = @FinalFinding,  
   FinalLiability = @FinalLiability,  
   InitialLiability=@InitialLiability,   
   CollisionType =@CollisionType,  
   DutyIO   = @DutyIOName,  
   Make   = @Make,  
   ModelNo   = @Model,  
   DriverEmployeeNo= @DriverEmployeeNo,  
   DriverName  = @DriverName,  
   DriverNRICNo = @DriverNRICNo,  
   DriverMobileNo = @DriverMobileNo,  
   DateJoined = @DriverDateJoined,
   DateResigned = @DriverDateResigned,
   Organization = @OrganizationId,  
   ModifiedDate = GETDATE(),  
   ModifiedBy  = @User,  
   AccidentLoc  = @PlaceOfAccident,  
   OperatingHours = @OperatingHours,
   District = @DistrictCode,
   RoadName = @RoadName,
   MinorInjury = @MinorInjury,
   SeriousInjury = @SeriousInjury,
   Fatal = @Fatal  
   --1 as IsReported,  
   --@STG_Uploaded_ID  
   --1 as IsComplete,  
     
   where AccidentClaimId = @AccidentClaimId  
   update STG_UploadedFileData set IsProcessed=1 , [Status] = 'Converted',ExceptionDetails='Duplicate entry Updated with Claim No. ' + @ClaimNo + ';' where FileRefNo = @FileRefNo and  ID = @STG_Uploaded_ID  
  end   
     
       
 END TRY  
 BEGIN CATCH  
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
   ,'Error while creating Reported Claim for report No. ' + @ReportNo + ' and IP Number ' +  @IpNumber + ERROR_MESSAGE()  AS ErrorMessage  
   ,getdate()  
   ,@User  
     
   update STG_UploadedFileData set IsProcessed=1,Status='Failed',ExceptionDetails='Error while creating Reported Claim for report No. ' + @ReportNo + ' and IP Number ' +  @IpNumber + ERROR_MESSAGE()  where FileRefNo = @FileRefNo  and ID = @STG_Uploaded_ID
  
  
  
  
  
  
  
  
  
 --and HistoryIds=@HistoryId_CLAIM  
   --select * from STG_UploadedFileData where FileRefNo = @FileRefNo and [Status]='Failed'  
    update MNT_FileUpload set HasError='Y', ErrorMessage = isnull(ErrorMessage,'') + 'Error while creating Reported Claim for report No. ' + @ReportNo + ' and IP Number ' +  @IpNumber + ERROR_MESSAGE() + ';'  where FileRefNo =@FileRefNo  
  
 END CATCH  
      
 FETCH NEXT  FROM CR_UPLOAD_CLAIM into @STG_Uploaded_ID,@IpNumber,@ReportDate,@AccidentDateTime,@PlaceOfAccident,@OperatingHours,@DamageToBus,@FactsOfIncident,@FinalLiabilityDate,@FinalFinding,@ReportNo,  
   @BusNumber,@ServiceNo,@FinalLiability,@StaffNo,@FileRefId,@Status,@DutyIO,@HistoryIds,@InitialLiability,@CollisionType,@DistrictCode,@RoadName,
   @SeriousInjury,@MinorInjury,@Fatal 
  END  
  
 select @SuccessCount = COUNT( case when [Status] = 'Converted' OR [Status]='Ready' then 1 else null end), @FailedCount = COUNT( case when [Status] = 'Failed' then 1 else null end)   
 from STG_UploadedFileData where IsProcessed=1 and FileRefNo = @FileRefNo --and  ID = @STG_Uploaded_ID  
   
 -- Success or Failed both has been considered. so that it will be available in download file.  
 if(@SuccessCount > 0 or @FailedCount > 0)   
  update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(), Status='Success', SuccessRecords = @SuccessCount, FailedRecords = @FailedCount  where FileRefNo =@FileRefNo  and UploadType='CLM'  
   
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
  select @SuccessCount = COUNT( case when [Status] = 'Converted' OR [Status]='Ready' then 1 else null end), @FailedCount = COUNT( case when [Status] = 'Failed' then 1 else null end)   
  from STG_UploadedFileData where IsProcessed=1 and FileRefNo = @FileRefNo --and  ID = @STG_Uploaded_ID  
    
  update MNT_FileUpload set SuccessRecords = @SuccessCount, FailedRecords = @FailedCount  where FileRefNo =@FileRefNo  and UploadType='CLM'  
  update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(),HasError='Y', ErrorMessage = 'Error while creating Reported Claim.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo  
      
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
  
  
  
  
  
  
  
  