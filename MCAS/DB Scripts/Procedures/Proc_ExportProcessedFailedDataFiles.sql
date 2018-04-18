/****** Object:  StoredProcedure [dbo].[Proc_ExportProcessedDataClaimFiles]    Script Date: 02/27/2015 19:51:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ExportProcessedDataClaimFiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ExportProcessedDataClaimFiles]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ExportProcessedDataClaimFiles]    Script Date: 02/27/2015 19:51:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
--begin tran  
--drop proc [Proc_ExportProcessedFailedDataFiles]  
--GO  
  
/*************************  
Created by: Pravesh K Chandel  
Created Date: 25 Feb 2015  
Purpose : Export Processed and failed uploaded data of CSV Files into log Files to download.  
drop proc [Proc_ExportProcessedFailedDataFiles]  
**************************/  
  
CREATE PROCEDURE [dbo].[Proc_ExportProcessedFailedDataFiles]   
(  
@FileRefNo varchar(20),  
@FileType varchar(20) =null 
)  
AS  
BEGIN  
 declare @rowCount int;
--if @FileType='I_IP'  
--begin  
with  I_IP as
(
 select   
 IpNumber,  
 AccidentDateTime,  
 FinalLiabilityDate,  
 FinalFinding,  
 PlaceOfAccident,  
 OperatingHours,  
 DamageToBus,  
 FactsOfIncident,  
 ReportNo,  
 DutyIO,  
 IP.HasError,  
 IP.ErrorMessage,  
 ERR.ErrorMessage as FileError  
 from STG_TAC_IP IP (nolock)  
 join MNT_FileUpload FU (nolock)on FU.FileRefNo = IP.FileRefNo and FU.FileId = IP.FileId   
 left join STG_FileUpload_ERRORS ERR (nolock) on ERR.FileId = IP.FileId and ERR.FileRefNo = IP.FileRefNo  
 where IP.FileRefNo =@FileRefNo   
 and ISNULL(IP.hasError,'')='Y'   
 and FU.UploadHistoryId=IP.HistoryId  
 ),
--end  
--if @FileType='I_BUS'  
--begin  
I_BUS as
(
 select   
 IpNumber,  
 BusNumber,  
 ServiceNo,  
 FinalLiability,  
 BOIResults,  
 StaffNo,  
 ReportNo,  
 BUS.HasError,  
 BUS.ErrorMessage,  
 ERR.ErrorMessage as FileError  
 from STG_TAC_IP_BUS  BUS (nolock)  
 join MNT_FileUpload FU (nolock)on FU.FileRefNo = BUS.FileRefNo and FU.FileId = BUS.FileId   
 left join STG_FileUpload_ERRORS ERR (nolock) on ERR.FileId = BUS.FileId and ERR.FileRefNo = BUS.FileRefNo  
 where BUS.FileRefNo =@FileRefNo   
 and ISNULL(BUS.hasError,'')='Y'   
 and FU.UploadHistoryId=BUS.HistoryId  
),
--end  
--if @FileType='I_REP'  
--begin  
I_REP as
(
 select   
 IpNumber,  
 ReportDate,  
 AccidentDateTime,  
 PlaceOfAccident,  
 OperatingHours,  
 DamageToBus,  
 FactsOfIncident,  
 ReportNo,  
 DutyIO,  
 REP.HasError,  
 REP.ErrorMessage,  
 ERR.ErrorMessage as FileError  
 from STG_TAC_ACC_REP  REP (nolock)  
 join MNT_FileUpload FU (nolock)on FU.FileRefNo = REP.FileRefNo and FU.FileId = REP.FileId   
 left join STG_FileUpload_ERRORS ERR (nolock) on ERR.FileId = REP.FileId and ERR.FileRefNo = REP.FileRefNo  
 where REP.FileRefNo =@FileRefNo   
 and ISNULL(REP.hasError,'')='Y'   
 and FU.UploadHistoryId=REP.HistoryId  
 ),
 I_CLAIM as
(
 select   
 IpNumber,  
 ReportDate,  
 AccidentDateTime,  
 PlaceOfAccident,  
 OperatingHours,  
 DamageToBus,  
 FactsOfIncident,  
 ReportNo,  
 DutyIO, 
 FinalLiabilityDate,
 FinalFinding,
 BusNumber,   
 ServiceNo,   
 FinalLiability,  
 StaffNo,
 CLM.HasError,  
 CLM.ErrorMessage,  
 ERR.ErrorMessage as FileError  
 from STG_CLAIM_FILE_DATA  CLM (nolock)  
 join MNT_FileUpload FU (nolock)on FU.FileRefNo = CLM.FileRefNo and FU.FileId = CLM.FileRefId   
 left join STG_FileUpload_ERRORS ERR (nolock) on ERR.FileId = CLM.FileRefId and ERR.FileRefNo = CLM.FileRefNo  
 where CLM.FileRefNo =@FileRefNo   
 and ISNULL(CLM.hasError,'')='Y'   
 and FU.UploadHistoryId=CLM.HistoryIds  
 ),
--end  
ErrorReport as
(
select 'TAC_REP' as FileType, ReportNo,IpNumber, ReportDate,AccidentDateTime,PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,DutyIO,
 null as FinalLiabilityDate,null as FinalFinding, null as BusNumber, null as ServiceNo,null as FinalLiability, null as StaffNo,
 HasError,ErrorMessage,FileError from I_REP

union all

select 'TAC_IP' as FileType,ReportNo,IpNumber,null as ReportDate,AccidentDateTime, PlaceOfAccident,OperatingHours,DamageToBus,FactsOfIncident,DutyIO,
FinalLiabilityDate,FinalFinding,  null as BusNumber, null as ServiceNo,null as FinalLiability, null as StaffNo,
 HasError, ErrorMessage, FileError  from I_IP

union all

select 'TAC_BUS' as FileType,ReportNo,IpNumber,null as ReportDate,null as AccidentDateTime, null as PlaceOfAccident, null as OperatingHours, null as DamageToBus, null as FactsOfIncident,null as DutyIO,
null as FinalLiabilityDate,null as FinalFinding,  BusNumber,   ServiceNo,   FinalLiability,  StaffNo,
   HasError,  ErrorMessage, FileError  from I_BUS  
union all

select 'TAC_CLAIM' as FileType,ReportNo,IpNumber,ReportDate,AccidentDateTime, PlaceOfAccident, OperatingHours, DamageToBus, FactsOfIncident,DutyIO,
FinalLiabilityDate,FinalFinding,  BusNumber,   ServiceNo,   FinalLiability,  StaffNo,
   HasError,  ErrorMessage, FileError  from I_CLAIM  
   
   
)

select * into #FinalReport from ErrorReport

select @rowCount = COUNT(*) from #FinalReport
if(@rowCount>0)
begin
select * from #FinalReport
end
else
begin
 select FileRefNo,FileName,UploadedDate,TotalRecords,SuccessRecords,FailedRecords,Status,Processed_Date,HasError,OrganizationType,OrganizationName,ErrorMessage
 from MNT_FileUpload where FileRefNo=@FileRefNo
end
 drop table #FinalReport

END  
  
--go  
--exec [dbo].[Proc_ExportProcessedFailedDataFiles] 'TAC-1','I_REP'  
  
--rollback tran  












GO


