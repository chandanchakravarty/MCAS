CREATE PROCEDURE [dbo].[Proc_ProcessUploadedData]  
 @FileRefNo [varchar](20),  
 @User [nvarchar](20) = 'System'  
  
AS  
BEGIN  
  
declare @FailedCount int,  
  @SuccessCount int,  
  @HistoryId_REP int,  
  @HistoryId_BUS int,  
  @HistoryId_IP int  
    
select @HistoryId_REP = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='T_REP'  
select @HistoryId_BUS = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='T_BUS'  
select @HistoryId_IP = UploadHistoryId from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo and UploadType='T_IP'  
  
begin try  
 -- REP and BUS File Data match   
 declare @MisMatchTable_REP_AND_BUS as table (REP_ID int,BUS_ID int,MisMatchMessage nvarchar(500))  
   
 insert into @MisMatchTable_REP_AND_BUS(REP_ID,BUS_ID,MisMatchMessage)  
 select  REP.ID as REP_ID, BUS.ID as BUS_ID,  
  case when BUS.ReportNo is null then 'Report No. does not exists in BUS file.;' else '' end +   
  case when REP.IpNumber<>BUS.IpNumber then 'IpNumber do not Match with BUS file.;' else '' end    
  as MisMatchMessage  
  from STG_TAC_ACC_REP REP (nolock)  
  left join STG_TAC_IP_BUS BUS (nolock)  
  on REP.FileRefNo=BUS.FileRefNo  
  and REP.ReportNo=BUS.ReportNo  
  and BUS.IsProcessed =0 and BUS.HistoryId=@HistoryId_BUS  
  where REP.FileRefNo = @FileRefNo and REP.IsProcessed =0 and REP.HistoryId=@HistoryId_REP  
  and  
  case when BUS.ReportNo is null then 'Report No. does not exists in BUS file.;' else '' end +   
  case when REP.IpNumber<>BUS.IpNumber then 'IpNumber do not Match with BUS file.;' else '' end  <>''  
     
 --select * from @MisMatchTable_REP_AND_BUS where BUS_ID =63099  
 -- report no exists only in IP  
 declare @MisMatchTable_BUS_ONLY as table (BUS_ID int,MisMatchMessage nvarchar(500))  
  
 insert into @MisMatchTable_BUS_ONLY(BUS_ID,MisMatchMessage)  
 select BUS.ID ,'Report No. does not exists in REP file.;' as ErrorMessage  
  from STG_TAC_IP_BUS BUS  (nolock)  
  where BUS.FileRefNo =@FileRefNo and BUS.HistoryId=@HistoryId_BUS  
  and BUS.ID not in  
  (   
  select BUS.ID from STG_TAC_ACC_REP REP (nolock)  
  join STG_TAC_IP_BUS BUS (nolock)  
  on REP.FileRefNo=BUS.FileRefNo and REP.ReportNo=BUS.ReportNo and BUS.IsProcessed =0 and BUS.HistoryId=@HistoryId_BUS  
  where REP.FileRefNo =@FileRefNo and REP.IsProcessed =0 and REP.HistoryId=@HistoryId_REP  
  )  
 --select * from @MisMatchTable_BUS_ONLY where BUS_ID =63099  
 --report no do not match from REP and IP  
 declare @MisMatchTable_REP_AND_IP as table (REP_ID int,IP_ID int,MisMatchMessage nvarchar(500))  
   
 insert into @MisMatchTable_REP_AND_IP(REP_ID,IP_ID,MisMatchMessage)  
 select REP.ID as REP_ID, IP.ID as IP_ID,  
  case when IP.ReportNo is null then 'Report No. does not exists in IP file.;' else '' end +   
  case when REP.IpNumber<>IP.IpNumber then 'IpNumber do not Match.;' else '' end +   
  case when REP.AccidentDateTime<>IP.AccidentDateTime then 'Date of Accident / Time do not Match.;' else '' end  +  
  case when REP.PlaceOfAccident<>IP.PlaceOfAccident then 'Place Of Accident do not Match.;' else '' end +   
  case when REP.OperatingHours<>IP.OperatingHours then 'Operating Hours do not Match.;' else '' end +   
  case when REP.DutyIO<>IP.DutyIO then 'Investigation Officer do not Match.;' else '' end +   
  case when REP.DamageToBus<>IP.DamageToBus then 'Damage To Bus do not Match.;' else '' end +   
  case when REP.FactsOfIncident<>IP.FactsOfIncident then 'Facts Of Incident do not Match.;' else '' end   
  as MisMatchMessage  
   
  from STG_TAC_ACC_REP REP (nolock)  
  left join STG_TAC_IP IP (nolock)  
  on REP.FileRefNo=IP.FileRefNo  
  and REP.ReportNo=IP.ReportNo  
  and IP.IsProcessed =0 and IP.HistoryId=@HistoryId_IP  
  where REP.FileRefNo = @FileRefNo and REP.IsProcessed =0 and REP.HistoryId=@HistoryId_REP  
  and  
  case when IP.ReportNo is null then 'Report No. does not exists in IP file.;' else '' end +   
  case when REP.IpNumber<>IP.IpNumber then 'IpNumber do not Match.;' else '' end +   
  case when REP.AccidentDateTime<>IP.AccidentDateTime then 'Date of Accident / Time do not Match.;' else '' end  +  
  case when REP.PlaceOfAccident<>IP.PlaceOfAccident then 'Place Of Accident do not Match.;' else '' end +   
  case when REP.OperatingHours<>IP.OperatingHours then 'Operating Hours do not Match.;' else '' end +   
  case when REP.DutyIO<>IP.DutyIO then 'Investigation Officer do not Match.;' else '' end +   
  case when REP.DamageToBus<>IP.DamageToBus then 'Damage To Bus do not Match.;' else '' end +   
  case when REP.FactsOfIncident<>IP.FactsOfIncident then 'Facts Of Incident do not Match.;' else '' end <>''  
  
 --select * from @MisMatchTable_REP_AND_IP  
 -- report no exists only in IP  
 declare @MisMatchTable_IP_ONLY as table (IP_ID int,MisMatchMessage nvarchar(500))  
 insert into @MisMatchTable_IP_ONLY(IP_ID,MisMatchMessage)  
 select IP.ID ,'Report No. does not exists in REP file.;' as ErrorMessage  
  from STG_TAC_IP IP  (nolock)  
  where IP.FileRefNo =@FileRefNo  and IP.HistoryId=@HistoryId_IP  
  and IP.ID not in  
  (   
  select IP.ID from STG_TAC_ACC_REP REP (nolock)  
  join STG_TAC_IP IP (nolock)  
  on REP.FileRefNo=IP.FileRefNo and REP.ReportNo=IP.ReportNo and IP.IsProcessed =0  and IP.HistoryId=@HistoryId_IP  
  where REP.FileRefNo =@FileRefNo and REP.IsProcessed =0  and REP.HistoryId=@HistoryId_REP  
  )  
 --select * from @MisMatchTable_IP_ONLY  
 declare @MisMatchTable_IP_AND_REP as table (IP_ID int,MisMatchMessage nvarchar(500))  
   
 insert into @MisMatchTable_IP_AND_REP(IP_ID,MisMatchMessage)  
 select IP.ID,  
  case when IP.ReportNo is null then 'Report No. does not exists in REP file.;' else '' end +   
  case when REP.IpNumber<>IP.IpNumber then 'IpNumber do not Match with REP file.;' else '' end +   
  case when REP.AccidentDateTime<>IP.AccidentDateTime then 'Date of Accident / Time do not Match with REP file.;' else '' end  +  
  case when REP.PlaceOfAccident<>IP.PlaceOfAccident then 'Place Of Accident do not Match with REP file.;' else '' end +   
  case when REP.OperatingHours<>IP.OperatingHours then 'Operating Hours do not Match with REP file.;' else '' end +   
  case when REP.DutyIO<>IP.DutyIO then 'Investigation Officer do not Match with REP file.;' else '' end +   
  case when REP.DamageToBus<>IP.DamageToBus then 'Damage To Bus do not Match with REP file.;' else '' end +   
  case when REP.FactsOfIncident<>IP.FactsOfIncident then 'Facts Of Incident do not Match with REP file.;' else '' end  
  as ErrorMessage  
   
  from STG_TAC_ACC_REP REP  (nolock)  
  left join  STG_TAC_IP IP (nolock)  
  on REP.FileRefNo=IP.FileRefNo  
  and REP.ReportNo=IP.ReportNo and IP.IsProcessed =0 and IP.HistoryId=@HistoryId_IP  
  where REP.FileRefNo =@FileRefNo and REP.IsProcessed =0 and REP.HistoryId=@HistoryId_REP  
  and  
  case when IP.ReportNo is null then 'Report No. does not exists in IP file.;' else '' end +   
  case when REP.IpNumber<>IP.IpNumber then 'IpNumber do not Match with REP file.;' else '' end +   
  case when REP.AccidentDateTime<>IP.AccidentDateTime then 'Date of Accident / Time do not Match with REP file.;' else '' end  +  
  case when REP.PlaceOfAccident<>IP.PlaceOfAccident then 'Place Of Accident do not Match with REP file.;' else '' end +   
  case when REP.OperatingHours<>IP.OperatingHours then 'Operating Hours do not Match with REP file.;' else '' end +   
  case when REP.DutyIO<>IP.DutyIO then 'Investigation Officer do not Match with REP file.;' else '' end +   
  case when REP.DamageToBus<>IP.DamageToBus then 'Damage To Bus do not Match with REP file.;' else '' end +   
  case when REP.FactsOfIncident<>IP.FactsOfIncident then 'Facts Of Incident do not Match with REP file.;' else '' end <>''  
  
 --select * from @MisMatchTable_IP_AND_REP   
   
 --Check If CollisionType exists in Collision Type Master (LookUps)  
 declare @MisMatchTable_IP_CollisionType as table (IP_ID int,MisMatchMessage nvarchar(500))  
   
 INSERT INTO @MisMatchTable_IP_CollisionType(IP_ID,MisMatchMessage)  
 SELECT IP.ID,CASE WHEN ISNULL(IP.CollisionType,'')<>'' AND LKP.LookupID IS NULL   
    THEN 'Collision Type does not exists in Master.;' ELSE '' END AS ErrorMessage  
 FROM STG_TAC_IP AS IP  
 LEFT JOIN MNT_Lookups AS LKP ON IP.CollisionType=LKP.Lookupvalue AND LKP.Category='CollisionType' AND LKP.IsActive='Y'  
 WHERE IP.FileRefNo =@FileRefNo AND IP.IsProcessed =0 AND IP.HistoryId=@HistoryId_IP   
  AND  ISNULL(IP.CollisionType,'')<>'' AND LKP.LookupID  IS NULL  
  
  
   --Check If District Code exists in Distrct Code Master (LookUps) -- Shikha 
 declare @MisMatchTable_IP_DistrictCode as table (IP_ID int,MisMatchMessage nvarchar(500))  
   
 INSERT INTO @MisMatchTable_IP_DistrictCode(IP_ID,MisMatchMessage)  
 SELECT IP.ID,CASE WHEN ISNULL(IP.DistrictCode,'')<>'' AND LKP.LookupID IS NULL   
    THEN 'District Code does not exists in Master.;' ELSE '' END AS ErrorMessage  
 FROM STG_TAC_IP AS IP  
 LEFT JOIN MNT_Lookups AS LKP ON IP.DistrictCode=LKP.Lookupvalue AND LKP.Category='DistrictCode' AND LKP.IsActive='Y'  
 WHERE IP.FileRefNo =@FileRefNo AND IP.IsProcessed =0 AND IP.HistoryId=@HistoryId_IP   
  AND  ISNULL(IP.DistrictCode,'')<>'' AND LKP.LookupID  IS NULL  
   
 
   
 if exists(select REP_ID from @MisMatchTable_REP_AND_BUS )   
  OR exists(select BUS_ID from @MisMatchTable_BUS_ONLY )  
  OR exists(select REP_ID from @MisMatchTable_REP_AND_IP )  
  OR exists(select IP_ID from @MisMatchTable_IP_ONLY )  
  OR exists(select IP_ID from @MisMatchTable_IP_AND_REP )  
  OR exists(select IP_ID from @MisMatchTable_IP_CollisionType)
  Or exists(select IP_ID from @MisMatchTable_IP_DistrictCode)  
 begin  
  -- update failed record in REP file missedMatch in Bus/Table  
  update REP set  
  REP.IsProcessed=1,  
  REP.HasError='Y',  
  REP.ErrorMessage= isnull(REP.ErrorMessage,'') + BUS.MisMatchMessage  
  from STG_TAC_ACC_REP REP (nolock)  
  join @MisMatchTable_REP_AND_BUS BUS   
  on REP.ID=BUS.REP_ID  
  -- update Bus Records does not exists in REP file/Table  
  update BUS set  
  BUS.IsProcessed=1,  
  BUS.HasError='Y',  
  BUS.ErrorMessage= BUSO.MisMatchMessage  
  from STG_TAC_IP_BUS BUS (nolock)  
  join @MisMatchTable_BUS_ONLY BUSO   
  on BUSO.BUS_ID=BUS.ID  
  -- update Bus records does not exists in IP  
  update BUS set  
  BUS.IsProcessed=1,  
  BUS.HasError='Y',  
  BUS.ErrorMessage= isnull(BUS.ErrorMessage,'') + 'Report No. does not exists in IP file.;'  
  from STG_TAC_IP_BUS BUS (nolock)  
  where BUS.FileRefNo =@FileRefNo and BUS.HistoryId=@HistoryId_BUS  
  and  BUS.ReportNo not in  
  (select distinct reportNo from STG_TAC_IP IP  (nolock)  
  where IP.FileRefNo =@FileRefNo and IP.HistoryId=@HistoryId_IP  
  )  
    
  select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
   from STG_TAC_IP_BUS BUS (nolock) where BUS.FileRefNo =@FileRefNo and BUS.HistoryId=@HistoryId_BUS  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount,Status ='Failed', HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_BUS'  
  -- update failed record in REP file/Table  
  update REP set  
  REP.IsProcessed=1,  
  REP.HasError='Y',  
  REP.ErrorMessage= isnull(REP.ErrorMessage,'') + IP.MisMatchMessage  
  from STG_TAC_ACC_REP REP (nolock)  
  join @MisMatchTable_REP_AND_IP IP on REP.ID=IP.REP_ID  
  -- update failed and success records in upload table  
  select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
  from STG_TAC_ACC_REP REP (nolock) where FileRefNo =@FileRefNo and REP.HistoryId=@HistoryId_REP  
  
  update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount,Status ='Failed', HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_REP'  
  -- update failed record in IP file/Table  
  update IP set  
  IP.IsProcessed=1,  
  IP.HasError='Y',  
  IP.ErrorMessage=IPO.MisMatchMessage  
  from STG_TAC_IP IP  (nolock)  
  join @MisMatchTable_IP_ONLY IPO on IPO.IP_ID = IP.ID  
  where IP.FileRefNo =@FileRefNo   
  
  set @FailedCount = @@ROWCOUNT  
  --select @FailedCount  
  -- update failed record in IP file/Table  
  update IP set  
  IP.IsProcessed=1,  
  IP.HasError='Y',  
  IP.ErrorMessage= IPREP.MisMatchMessage  
  from STG_TAC_IP IP (nolock)  
  join @MisMatchTable_IP_AND_REP IPREP on IPREP.IP_ID = IP.ID  
  
  -- update failed record in IP file does not exists in BUS file/Table  
  update IP set  
  IP.IsProcessed=1,  
  IP.HasError='Y',  
  IP.ErrorMessage= isnull(IP.ErrorMessage,'') + 'Report No. does not exists in Bus file.;'  
  from STG_TAC_IP IP (nolock)  
  where IP.FileRefNo =@FileRefNo and IP.HistoryId=@HistoryId_IP  
  and IP.ReportNo not in  
  (select distinct reportNo from STG_TAC_IP_BUS BUS  (nolock)  
  where BUS.FileRefNo =@FileRefNo and BUS.HistoryId=@HistoryId_BUS  
  )  
    
  -- update failed record in IP file if Collision Type not found in master.  
  update IP set  
  IP.IsProcessed=1,  
  IP.HasError='Y',  
  IP.ErrorMessage= IPREP.MisMatchMessage  
  from STG_TAC_IP IP (nolock)  
  join @MisMatchTable_IP_CollisionType IPREP on IPREP.IP_ID = IP.ID  
  
    -- update failed record in IP file if District Code not found in master.  --Shikha
  update IP set  
  IP.IsProcessed=1,  
  IP.HasError='Y',  
  IP.ErrorMessage= IPREP.MisMatchMessage  
  from STG_TAC_IP IP (nolock)  
  join @MisMatchTable_IP_DistrictCode IPREP on IPREP.IP_ID = IP.ID  
    
  -- update failed and success records in upload table  
  select @FailedCount = COUNT( case when HasError='Y' then 1 else null end ),@SuccessCount = COUNT( case when isnull(HasError,'')<>'Y' then 1 else null end )   
  from STG_TAC_IP IP (nolock) where IP.FileRefNo =@FileRefNo  and IP.HistoryId=@HistoryId_IP  
  --select @FailedCount  
  update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=@SuccessCount,Status ='Failed', HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_IP'  
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
     
   update MNT_FileUpload set Is_Processed='Y',LastModifiedDateTime=GETDATE(),ModifiedBy=@User,Status ='Failed', HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo   
     
   update STG_TAC_ACC_REP set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_REP  
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=0, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_REP'  
     
   update STG_TAC_IP set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_IP  
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=0,HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_IP'  
     
   update STG_TAC_IP_BUS set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_BUS   
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount,SuccessRecords=0, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_BUS'  
     
   return  
end catch  
begin try  
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
 InitialLiability,  
 CollisionType,  
 DistrictCode,
 RoadName,
 SeriousInjury,
 MinorInjury,
 Fatal,
 BusNumber,   
 ServiceNo,  
 FinalLiability,  
 BOIResults,  
 StaffNo,  
 CreatedDate,  
 CreatedBy,  
 IsActive,  
 HistoryIds  
 )  
  
 select   
 REP.IpNumber,  
 REP.ReportDate,  
 REP.AccidentDateTime,  
 REP.PlaceOfAccident,  
 REP.OperatingHours,  
 REP.DamageToBus,  
 REP.FactsOfIncident,  
 REP.ReportNo,  
 REP.DutyIO,  
 REP.FileRefNo,  
 REP.FileId,  
 IP.FinalLiabilityDate,  
 IP.FinalFinding,  
 IP.InitialLiability,  
 IP.CollisionType,  
 IP.DistrictCode,
 IP.RoadName,
 IP.SeriousInjury,
 IP.MinorInjury,
 IP.Fatal, 
 BUS.BusNumber as BusNumber,
 BUS.ServiceNo as ServiceNo,  
 BUS.FinalLiability as FinalLiability,  
 BUS.BOIResults as BOIResults,  
 BUS.StaffNo as StaffNo,  
 getdate() as CreatedDate,  
 @User as CreatedBy,  
 'Y' as IsActive,  
 'REP^' + cast(@HistoryId_REP as varchar) + '~BUS^' + cast(@HistoryId_BUS as varchar) + '~IP^' + cast(@HistoryId_IP as varchar) as HistoryIds  
   
 from STG_TAC_ACC_REP REP (nolock)  
    join STG_TAC_IP IP (nolock)  
   on REP.FileRefNo=IP.FileRefNo  
   and REP.ReportNo=IP.ReportNo  
   and isnull(IP.HasError,'')<>'Y'  
   and IP.IsProcessed =0  
   and IP.HistoryId=@HistoryId_IP  
     
   join STG_TAC_IP_BUS BUS (nolock)  
   on REP.FileRefNo=BUS.FileRefNo  
   and REP.ReportNo=BUS.ReportNo  
   and BUS.IsProcessed =0  
   and isnull(BUS.HasError,'')<>'Y'  
   and BUS.HistoryId=@HistoryId_BUS  
  
   where REP.FileRefNo = @FileRefNo  
   and isnull(REP.HasError,'')<>'Y'  
   and REP.IsProcessed =0  
   and REP.HistoryId=@HistoryId_REP  
   
 set @FailedCount = @@ROWCOUNT  
 update MNT_FileUpload set Status ='Success', HasError='N'  where FileRefNo =@FileRefNo  
  
 Update REP  
 set REP.IsProcessed=1,  
 REP.hasError='N'  
 from STG_TAC_ACC_REP REP (nolock)  
   left join STG_TAC_IP IP (nolock)  
   on REP.FileRefNo=IP.FileRefNo  
   and REP.ReportNo=IP.ReportNo  
   and isnull(IP.HasError,'')<>'Y'   
   and IP.IsProcessed =0 and IP.HistoryId=@HistoryId_IP  
   where REP.FileRefNo = @FileRefNo  
   and isnull(REP.HasError,'')<>'Y'  
   and REP.HistoryId=@HistoryId_REP  
     
 Update BUS  
 set BUS.IsProcessed=1,  
 BUS.hasError='N'  
 from STG_TAC_IP_BUS BUS  (nolock)  
   join STG_TAC_ACC_REP REP (nolock)  
   on REP.FileRefNo=BUS.FileRefNo  
   and REP.ReportNo=BUS.ReportNo  
   and BUS.IsProcessed =0  
   and isnull(BUS.HasError,'')<>'Y'  
   and BUS.HistoryId=@HistoryId_BUS  
   where REP.FileRefNo = @FileRefNo  
   and isnull(REP.HasError,'')<>'Y'  
   and REP.HistoryId=@HistoryId_REP  
     
 Update IP  
 set IP.IsProcessed=1,  
 IP.hasError='N'  
 from STG_TAC_IP IP  (nolock)  
   join STG_TAC_ACC_REP REP (nolock)  
   on REP.FileRefNo=IP.FileRefNo  
   and REP.ReportNo=IP.ReportNo  
   and IP.IsProcessed =0  
   and isnull(IP.HasError,'')<>'Y' and IP.HistoryId=@HistoryId_IP  
   where REP.FileRefNo = @FileRefNo  
   and isnull(REP.HasError,'')<>'Y'  
   and REP.HistoryId=@HistoryId_REP  
  
  
   select @SuccessCount = COUNT(case when HasError='N' then 1 else null end),@FailedCount =COUNT(*) from STG_TAC_ACC_REP where FileRefNo =@FileRefNo and HistoryId = @HistoryId_REP  
   update MNT_FileUpload set FailedRecords = abs(@SuccessCount - @FailedCount),SuccessRecords=@SuccessCount, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_REP'  
     
   select @SuccessCount = COUNT(case when HasError='N' then 1 else null end),@FailedCount =COUNT(*) from STG_TAC_IP where FileRefNo =@FileRefNo and HistoryId = @HistoryId_IP  
   update MNT_FileUpload set FailedRecords =  abs(@SuccessCount - @FailedCount),SuccessRecords=@SuccessCount,HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_IP'  
     
   select @SuccessCount = COUNT(case when HasError='N' then 1 else null end),@FailedCount =COUNT(*) from STG_TAC_IP_BUS where FileRefNo =@FileRefNo and HistoryId = @HistoryId_BUS  
   update MNT_FileUpload set FailedRecords =  abs(@SuccessCount - @FailedCount),SuccessRecords=@SuccessCount, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_BUS'  
     
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
   ,'Error while inserting data of Processed Files.' + ERROR_MESSAGE()  AS ErrorMessage  
   ,getdate()  
   ,@User  
     
    update MNT_FileUpload set Is_Processed='Y',LastModifiedDateTime=GETDATE(),ModifiedBy=@User,Status ='Failed', HasError='Y', ErrorMessage = 'Error while inserting data of Processed Files.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo   
  
   update STG_TAC_ACC_REP set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_REP  
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_REP'  
  
   update STG_TAC_IP set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_IP  
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_IP'  
     
   update STG_TAC_IP_BUS set IsProcessed=1, HasError='Y', ErrorMessage = 'Error while validating Processing Files Data.' + ERROR_MESSAGE()  where FileRefNo =@FileRefNo and HistoryId = @HistoryId_BUS   
   set @FailedCount = @@ROWCOUNT  
   update MNT_FileUpload set FailedRecords = @FailedCount, HasError='Y',ModifiedBy=@User ,LastModifiedDateTime=GETDATE() where FileRefNo =@FileRefNo and UploadType='T_BUS'  
  
end catch   
END  
  
--GO  
--declare @FileRefNo varchar(20) ='TAC-109'  
--exec [Proc_ProcessUploadedData] @FileRefNo,'System'  
  
--select * from MNT_FileUpload where FileRefNo =@FileRefNo   
--select * from STG_UploadedFileData where FileRefNo = @FileRefNo  
  
--select 'REP',* from STG_TAC_ACC_REP where FileRefNo = @FileRefNo and ReportNo='E79842014' --IsProcessed=0  
--select 'BUS',* from STG_TAC_IP_BUS where FileRefNo = @FileRefNo and  ReportNo='E79842014' --IsProcessed=0  
--select 'IP',* from STG_TAC_IP where FileRefNo = @FileRefNo and  ReportNo='E79842014' --IsProcessed=0  
  
--select * from STG_TAC_ACC_REP where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'  
--select * from STG_TAC_ACC_REP where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'  
  
--select * from STG_TAC_IP_BUS where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'  
--select * from STG_TAC_IP_BUS where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'  
  
--select * from STG_TAC_IP where FileRefNo = @FileRefNo and isnull(HasError,'')='Y'  
--select * from STG_TAC_IP where FileRefNo = @FileRefNo and isnull(HasError,'')<>'Y'  
  
--rollback tran  
  
--declare @FileRefNo varchar(20) ='TAC-109'  
--select * from MNT_FileUpload where FileRefNo =@FileRefNo   
--select * from STG_UploadedFileData where FileRefNo = @FileRefNo  
  
  
  

