CREATE PROCEDURE [dbo].[Proc_ProcessUploadedClaimData]  
 @FileRefNo [varchar](20),  
 @User [nvarchar](20) = 'System'  
AS  
BEGIN  
  
declare @FailedCount int,  
  @SuccessCount int,  
  @HistoryId_CLAIM int,  
  @HistoryId_STD_CD int  
    
    
select @HistoryId_CLAIM = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='CLM'  
select @HistoryId_STD_CD = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='CLM_STD_CD'  
  
  
begin try  
  -- CLAIM and Standard Code File Data match   
    
  -- matching of OperatingHours  
  declare @MisMatchTable_CLAIM_AND_STD_CODE as table (CLAIM_ID int,MisMatchMessage nvarchar(500))  
  insert into @MisMatchTable_CLAIM_AND_STD_CODE(CLAIM_ID,MisMatchMessage)  
     
  select CLM.ID,'Code for Operating Hours not exists in standard code file.;' as ErrorMessage   
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  where CLM.FileRefNo = @FileRefNo and isnull(CLM.IsProcessed,0) =0  and CLM.HistoryIds=@HistoryId_CLAIM  
  and OperatingHours not in  
  (  
  select STD_CODE from STG_UPLOAD_STANDARDS_CODES STD (nolock)  
  where STD.FileRefNo=@FileRefNo  
  and STD.STD_CODE_TYPE ='13'  --Col A  13 (Operating Hours)  
  and isnull(STD.IsProcessed,0) =0   
  and STD.HistoryId=@HistoryId_STD_CD  
  )  
  -- matching of Final Finding   
  insert into @MisMatchTable_CLAIM_AND_STD_CODE(CLAIM_ID,MisMatchMessage)  
    
  select  CLM.ID,'Code for Final Finding not exists in standard code file.;' as ErrorMessage    
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  where CLM.FileRefNo = @FileRefNo and isnull(CLM.IsProcessed,0) =0  and CLM.HistoryIds=@HistoryId_CLAIM  
  and CLM.FinalFinding not in  
  (  
  select STD_CODE from STG_UPLOAD_STANDARDS_CODES STD (nolock)  
  where STD.FileRefNo=@FileRefNo  
  and STD.STD_CODE_TYPE ='21'  --Col A  21 (Final Finding)  
  and isnull(STD.IsProcessed,0) =0   
  and STD.HistoryId=@HistoryId_STD_CD  
  )  
    
  -- matching of Duty IO  
  insert into @MisMatchTable_CLAIM_AND_STD_CODE(CLAIM_ID,MisMatchMessage)  
  
  select  CLM.ID,'Code for Duty IO not exists in standard code file.;' as ErrorMessage    
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  where CLM.FileRefNo = @FileRefNo and isnull(CLM.IsProcessed,0) =0  and CLM.HistoryIds=@HistoryId_CLAIM  
  and CLM.DutyIO not in  
  (  
  select STD_CODE from STG_UPLOAD_STANDARDS_CODES STD (nolock)  
  where STD.FileRefNo=@FileRefNo  
  and STD.STD_CODE_TYPE ='22'  --Col A  22 (Duty IO)   
  and isnull(STD.IsProcessed,0) =0   
  and STD.HistoryId=@HistoryId_STD_CD  
  )  
  -- matching of Bus Number  
  insert into @MisMatchTable_CLAIM_AND_STD_CODE(CLAIM_ID,MisMatchMessage)  
  
  select  CLM.ID,'Bus Number does not exists in Vehicle Master.;' as ErrorMessage    
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  where CLM.FileRefNo = @FileRefNo and isnull(CLM.IsProcessed,0) =0  and CLM.HistoryIds=@HistoryId_CLAIM  
  and CLM.BusNumber not in  
  (  
  select distinct VehicleRegNo from MNT_VehicleListingMaster STD (nolock)  
  )  
  -- matching of Bus captain  
  insert into @MisMatchTable_CLAIM_AND_STD_CODE(CLAIM_ID,MisMatchMessage)  
    
  select  CLM.ID,'Bus Captain does not exists in Bus Captain Master.;' as ErrorMessage    
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  where CLM.FileRefNo = @FileRefNo and isnull(CLM.IsProcessed,0) =0  and CLM.HistoryIds=@HistoryId_CLAIM  
  and CLM.StaffNo not in  
  (  
  select distinct BusCaptainCode from MNT_BusCaptain (nolock)  
  )  
    
  -- Group by Data using column CLAIM_ID and XML PATH  
  declare @MisMatchTable_CLAIM_AND_STD_CODE_FINAL as table (CLAIM_ID int,MisMatchMessage nvarchar(500))  
  insert into @MisMatchTable_CLAIM_AND_STD_CODE_FINAL(CLAIM_ID,MisMatchMessage)  
    
  SELECT CLAIM_ID,  
  STUFF((  
   SELECT ' ' + MisMatchMessage   
   FROM @MisMatchTable_CLAIM_AND_STD_CODE  
   WHERE (CLAIM_ID = T2.CLAIM_ID)  
   FOR XML PATH (''))  
   ,1,1,'') AS ErrorMessage  
  FROM @MisMatchTable_CLAIM_AND_STD_CODE T2  
  GROUP BY CLAIM_ID  
  delete from @MisMatchTable_CLAIM_AND_STD_CODE  
   
  --select * from @MisMatchTable_CLAIM_AND_STD_CODE_FINAL  
  
 if exists(select CLAIM_ID from @MisMatchTable_CLAIM_AND_STD_CODE_FINAL )   
 begin  
  -- update failed record in CLAIM file missedMatch in Standard Code file  
    
  update CLM set  
  CLM.IsProcessed=1,  
  CLM.HasError='Y',  
  CLM.ErrorMessage= ERR.MisMatchMessage,  
  CLM.Status = 'Failed'  
  from STG_CLAIM_FILE_DATA CLM (nolock)  
  join @MisMatchTable_CLAIM_AND_STD_CODE_FINAL ERR   
  on ERR.CLAIM_ID=CLM.ID  
    
  select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
   from STG_CLAIM_FILE_DATA CLM (nolock) where CLM.FileRefNo =@FileRefNo and CLM.HistoryIds=@HistoryId_CLAIM  
  
  update MNT_FileUpload set LastModifiedDateTime=GETDATE(),ModifiedBy=@User, Status ='Failed', HasError='Y'  
  ,ErrorMessage = 'Claim file missmatch from Standard Code file.'   where FileRefNo =@FileRefNo --and UploadType='CLM'  
    
  update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount where FileRefNo =@FileRefNo and UploadType='CLM'  
    
  set @FailedCount = @@ROWCOUNT  
  
  return  
 end   
end try  
begin catch  
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
   ,'Error while validating Processing Files Data.' + ERROR_MESSAGE()  AS ErrorMessage  
   ,getdate()  
   ,@User  
    update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(), HasError='Y', ErrorMessage = ERROR_MESSAGE()  where FileRefNo =@FileRefNo  
  
    select @SuccessCount = COUNT(case when HasError='N' then 1 else null end), @FailedCount = COUNT(*)  from STG_CLAIM_FILE_DATA CLM (nolock) where CLM.FileRefNo =@FileRefNo and CLM.HistoryIds=@HistoryId_CLAIM  
    update MNT_FileUpload set FailedRecords= ABS(@SuccessCount-@FailedCount), SuccessRecords= @SuccessCount   where FileRefNo =@FileRefNo and UploadType='CLM'  
  
    select @SuccessCount = COUNT(case when HasError='N' then 1 else null end), @FailedCount = COUNT(*)  from STG_UPLOAD_STANDARDS_CODES (nolock) where FileRefNo =@FileRefNo and HistoryId=@HistoryId_STD_CD  
    update MNT_FileUpload set FailedRecords= ABS(@SuccessCount-@FailedCount), SuccessRecords= @SuccessCount   where FileRefNo =@FileRefNo and UploadType='CLM_STD_CD'  
end catch  
begin try  
  
--------update/insert lookup value from Standard code file for operating hours  
  --select LookupCode from MNT_Lookups (nolock) where Category ='OperatingHours'  
   if exists(  
     select STD_CODE from STG_UPLOAD_STANDARDS_CODES (nolock) std   
     where std.FileRefNo = @FileRefNo and std.STD_CODE_TYPE ='13' and HistoryId = @HistoryId_STD_CD  
     and std.STD_CODE not in   
     (  
     select LookupCode from MNT_Lookups (nolock) where Category ='OperatingHours' and ltrim(rtrim(Lookupdesc))= ltrim(rtrim(std.Description))  
     )  
    )  
   begin  
    declare @maxLookupvalue varchar(20)  
    select @maxLookupvalue = max(Lookupvalue) from MNT_Lookups (nolock) where Category ='OperatingHours'   
    insert into MNT_Lookups  
    (  
    Lookupvalue,  
    Lookupdesc,  
    Description,  
    Category,  
    IsActive,  
    lookupCode,  
    CreateDate,  
    CreateBy  
    )  
    select @maxLookupvalue + ROW_NUMBER() over(order by STD_CODE asc) as Lookupvalue,  
    [Description] as Lookupdesc, [Description] as Description,'OperatingHours' as Category,'Y' as Active,STD_CODE as lookupCode, GETDATE() as CreateDate,'System' as CreateBy  
    from STG_UPLOAD_STANDARDS_CODES (nolock) std   
    where std.FileRefNo = @FileRefNo and std.STD_CODE_TYPE ='13' and HistoryId = @HistoryId_STD_CD  
    and std.STD_CODE not in   
    (  
    select LookupCode from MNT_Lookups (nolock) where Category ='OperatingHours' and ltrim(rtrim(Lookupdesc))= ltrim(rtrim(std.Description))  
    )  
   end  
   --select * from MNT_Lookups (nolock) where Category ='OperatingHours'  
 if exists( select CLM.IpNumber  
    from STG_CLAIM_FILE_DATA CLM (nolock)  
    where CLM.FileRefNo = @FileRefNo  
    and isnull(CLM.HasError,'')<>'Y'  
    and isnull(CLM.IsProcessed,0) =0  
    and CLM.HistoryIds=@HistoryId_CLAIM  
  )  
  begin  
   insert into STG_UploadedFileData  
   (  
   IpNumber,  
   ReportDate,  
   AccidentDateTime,  
   PlaceOfAccident,  
   OperatingHours,  
   DamageToBus,  
   FactsOfIncident,  
   ReportNo,  
   DutyIO,  
   FileRefNo,  
   FileRefId,  
   FinalLiabilityDate,  
   FinalFinding,  
   BusNumber,  
   ServiceNo,  
   FinalLiability,  
   InitialLiability,  
   CollisionType, 
   DistrictCode,
    RoadName,
    SeriousInjury,
    MinorInjury,
    Fatal,
   --BOIResults,  
   StaffNo,  
   CreatedDate,  
   CreatedBy,  
   IsActive,  
   IsProcessed,  
   HistoryIds,  
   Status  
   )  
  
   select   
   CLM.IpNumber,  
   CLM.ReportDate,  
   CLM.AccidentDateTime,  
   CLM.PlaceOfAccident,  
   CLM.OperatingHours,  
   CLM.DamageToBus,  
   CLM.FactsOfIncident,  
   CLM.ReportNo,  
   CLM.DutyIO,  
   CLM.FileRefNo,  
   CLM.FileRefId,  
   CLM.FinalLiabilityDate,  
   CLM.FinalFinding,     
  
   CLM.BusNumber as BusNumber,  
   CLM.ServiceNo as ServiceNo,  
   CLM.FinalLiability as FinalLiability,  
   CLM.InitialLiability,  
   CLM.CollisionType,  
   CLM.DistrictCode,
   CLM.RoadName,
   CLM.SeriousInjury,
   CLM.MinorInjury,
   CLM.Fatal,
   --CLM.BOIResults as BOIResults,  
   CLM.StaffNo as StaffNo,  
   getdate() as CreatedDate,  
   @User as CreatedBy,  
   'Y' as IsActive,  
   0 as IsProcessed,  
   'CLM^' + cast(@HistoryId_CLAIM as varchar) + '~STD^' + cast(@HistoryId_STD_CD as varchar) as HistoryIds  
   ,'Ready'  
   from STG_CLAIM_FILE_DATA CLM (nolock)  
  
     where CLM.FileRefNo = @FileRefNo  
     and isnull(CLM.HasError,'')<>'Y'  
     and isnull(CLM.IsProcessed,0) =0  
     and CLM.HistoryIds=@HistoryId_CLAIM  
     
   set @FailedCount = @@ROWCOUNT  
    
   Update CLM  
   set CLM.IsProcessed=1,  
   CLM.hasError='N',  
   CLM.Status ='Success'  
   from STG_CLAIM_FILE_DATA CLM (nolock)  
       
     where CLM.FileRefNo = @FileRefNo  
     and isnull(CLM.HasError,'')<>'Y'  
     and isnull(CLM.IsProcessed,0) =0  
     and CLM.HistoryIds=@HistoryId_CLAIM  
       
     
   --Update STD  
   --set STD.IsProcessed=1,  
   --STD.hasError='N'  
   ----STD.Status ='Success'  
   --from STG_UPLOAD_STANDARDS_CODES STD (nolock)  
  
   --  where STD.FileRefNo = @FileRefNo  
   --  and isnull(STD.HasError,'')<>'Y'  
   --  and isnull(STD.IsProcessed,0) =0  
   --  and STD.HistoryId=@HistoryId_STD_CD  
   
   -- update File Table  
   update MNT_FileUpload set Status ='Passed', HasError='N'  where FileRefNo =@FileRefNo  
   select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
     from STG_CLAIM_FILE_DATA CLM (nolock) where CLM.FileRefNo =@FileRefNo and CLM.HistoryIds=@HistoryId_CLAIM  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount, HasError='N'  where FileRefNo =@FileRefNo and UploadType='CLM'  
 end   
 -- select 'pass'   
 if exists( select CLM.ID  
    from STG_UPLOAD_STANDARDS_CODES CLM (nolock)  
    where CLM.FileRefNo = @FileRefNo  
    and isnull(CLM.HasError,'')<>'Y'  
    and isnull(CLM.IsProcessed,0) =0  
    and CLM.HistoryId=@HistoryId_STD_CD  
  )  
  BEGIN  
   select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
     from STG_UPLOAD_STANDARDS_CODES CLM (nolock) where CLM.FileRefNo =@FileRefNo and CLM.HistoryId=@HistoryId_STD_CD  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount,Status ='Passed', HasError='N'  where FileRefNo =@FileRefNo and UploadType='CLM_STD_CD'  
  END  
    
  Update STD  
   set STD.IsProcessed=1,  
   STD.hasError='N'  
   --STD.Status ='Success'  
   from STG_UPLOAD_STANDARDS_CODES STD (nolock)  
  
     where STD.FileRefNo = @FileRefNo  
     and isnull(STD.HasError,'')<>'Y'  
     and isnull(STD.IsProcessed,0) =0  
     and STD.HistoryId=@HistoryId_STD_CD  
end try  
begin catch  
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
   ,'Error while inserting Claim data of Processed Files.' + ERROR_MESSAGE()  AS ErrorMessage  
   ,getdate()  
   ,@User  
     
   update MNT_FileUpload set Status ='Failed', HasError='Y',ErrorMessage='Error while inserting Claim data of Processed Files.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo --and UploadType='CLM'  
      select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
     from STG_CLAIM_FILE_DATA CLM (nolock) where CLM.FileRefNo =@FileRefNo and CLM.HistoryIds=@HistoryId_CLAIM  
  update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount  where FileRefNo =@FileRefNo --and UploadType='CLM'  
     
end catch   
END  
  
--GO  
  
--declare @FileRefNo varchar(20) ='CLM-111'  
--update STG_CLAIM_FILE_DATA set IsProcessed=0 where FileRefNo = @FileRefNo  
--update STG_UPLOAD_STANDARDS_CODES set IsProcessed=0 where FileRefNo = @FileRefNo  
  
--exec [Proc_ProcessUploadedClaimData] @FileRefNo,'System'  
  
--select * from MNT_FileUpload where FileRefNo =@FileRefNo   
--select * from STG_UploadedFileData where FileRefNo = @FileRefNo   
  
--select * from STG_CLAIM_FILE_DATA where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'  
--select * from STG_CLAIM_FILE_DATA where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'  
  
--select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'  
--select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'  
  
  
--rollback tran  
  
  
  
  