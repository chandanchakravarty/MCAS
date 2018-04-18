

/****** Object:  StoredProcedure [dbo].[Proc_ExportProcessedDataClaimFiles]    Script Date: 02/26/2015 14:39:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ExportProcessedDataClaimFiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ExportProcessedDataClaimFiles]
GO


/****** Object:  StoredProcedure [dbo].[Proc_ExportProcessedDataClaimFiles]    Script Date: 02/26/2015 14:39:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc [Proc_ExportProcessedDataClaimFiles]
--GO

/*************************
Created by: Pravesh K Chandel
Created Date: 25 Feb 2015
Purpose	: Export Processed uploaded data of CSV Files into Claim Files to download.
drop proc [Proc_ExportProcessedDataClaimFiles]
**************************/

CREATE PROCEDURE [dbo].[Proc_ExportProcessedDataClaimFiles] 
(
@FileRefNo varchar(20)
)
AS
BEGIN

declare @FileId int,
		--@FileRefNo nvarchar(50),
		@FileName nvarchar(100),
		@FileType nvarchar(10),
		@UploadType nvarchar(10),
		@UploadPath nvarchar(200),
		@Status varchar(20),
		@TotalCount int,
		@ErrorFile nvarchar(100),
		@FileServerPath varchar(50)=null,
		
		@UploadHistoryIds varchar(50)
		
		
declare @FilePath varchar(500)=@FileServerPath + @FileName
declare @mySql nvarchar(2000)
select @FileType = UploadType from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo
if(@FileType in ('CLM','CLM_STD_CD'))
begin
	select @UploadHistoryIds = 'CLM^' + cast(UploadHistoryId as varchar) from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo and UploadType='CLM'
	select @UploadHistoryIds = @UploadHistoryIds + '~STD^' + cast(UploadHistoryId as varchar) from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo and UploadType='CLM_STD_CD'

	select 
	isnull(ReportNo,'') as ReportNo,
	isnull(ReportDate,'') as ReportDate,
	isnull(IpNumber,'') as IpNumber,
	isnull(AccidentDateTime,'') as AccidentDateTime,
	isnull(PlaceOfAccident,'') as PlaceOfAccident,
	isnull(STG.OperatingHours,'') as OperatingHours,
	 isnull(STG.DutyIO,'') as DutyIO,
	isnull(DamageToBus,'') as DamageToBus,
	isnull(FactsOfIncident,'') as FactsOfIncident,
	 isnull(BusNumber,'') as BusNumber,
	isnull(ServiceNo,'') as ServiceNo,
	isnull(STG.FinalLiability,'') as FinalLiability,
	isnull(StaffNo,'') as StaffNo,
	isnull(FinalLiabilityDate,'') as FinalLiabilityDate,
	isnull(FinalFinding,'') as FinalFinding,
	'Claim - ' + CLM.ClaimNo + ' - generated for this data row' as ClaimNumber
	  from STG_UploadedFileData STG (nolock)
	  left join ClaimAccidentDetails CLM (nolock) on CLM.IsReported=1 and CLM.ReportedRefId = STG.ID
	  where isnull(ReportNo,'')<>'' and FileRefNo =@FileRefNo  
	   and STG.HistoryIds = @UploadHistoryIds 
   
end
else
begin
	select @UploadHistoryIds = 'REP^' + cast(UploadHistoryId as varchar) from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo and UploadType='T_REP'
	select @UploadHistoryIds = @UploadHistoryIds + '~BUS^' + cast(UploadHistoryId as varchar) from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo and UploadType='T_BUS'
	select @UploadHistoryIds = @UploadHistoryIds + '~IP^' + cast(UploadHistoryId as varchar) from MNT_FileUpload FU (nolock) where FU.FileRefNo = @FileRefNo and UploadType='T_IP'


	select 
	isnull(ReportNo,'') as ReportNo,
	isnull(ReportDate,'') as ReportDate,
	isnull(IpNumber,'') as IpNumber,
	isnull(AccidentDateTime,'') as AccidentDateTime,
	isnull(PlaceOfAccident,'') as PlaceOfAccident,
	isnull(OperatingHours,'') as OperatingHours,
	 isnull(DutyIO,'') as DutyIO,
	isnull(DamageToBus,'') as DamageToBus,
	isnull(FactsOfIncident,'') as FactsOfIncident,
	 isnull(BusNumber,'') as BusNumber,
	isnull(ServiceNo,'') as ServiceNo,
	isnull(FinalLiability,'') as FinalLiability,
	isnull(StaffNo,'') as StaffNo,
	isnull(FinalLiabilityDate,'') as FinalLiabilityDate,
	isnull(FinalFinding,'') as FinalFinding
	  from STG_UploadedFileData STG (nolock)
	  where isnull(ReportNo,'')<>'' and FileRefNo =@FileRefNo  
	   and STG.HistoryIds = @UploadHistoryIds 
end

END

--go
--exec [dbo].[Proc_ExportProcessedDataClaimFiles] 'TAC-109'

--rollback tran


GO