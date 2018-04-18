

/****** Object:  StoredProcedure [dbo].[Proc_ProcessTACFiles]    Script Date: 02/27/2015 19:51:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProcessTACFiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProcessTACFiles]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ProcessTACFiles]    Script Date: 02/27/2015 19:51:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc [Proc_ProcessTACFiles]
--GO

/*************************
Created by: Pravesh K Chandel
Created Date: 9 Feb 2015
Purpose	: Process uploaded CSV Files which are scheduled
drop proc [Proc_ProcessTACFiles]
**************************/

CREATE PROCEDURE [dbo].[Proc_ProcessTACFiles] 
(
@FileServerPath varchar(50)=null,
@FileRefNo varchar(20),
@User  nvarchar(20) ='System',
@Result varchar(20) out
)
AS
BEGIN

--if @FileServerPath is null set @FileServerPath ='\\192.168.91.18\'
--set @FileRefNo ='TAC-24'	
declare @FileId int,
		--@FileRefNo nvarchar(50),
		@FileName nvarchar(100),
		@FileType nvarchar(10),
		@UploadType nvarchar(10),
		@UploadPath nvarchar(200),
		@Status varchar(20),
		@TotalCount int,
		@ErrorFile nvarchar(200),
		@HistoryId int
		
declare @FilePath varchar(500)=@FileServerPath + @FileName

declare @mySql nvarchar(1500)

DECLARE CR_UPLOAD CURSOR FOR  
 select FileId,FileRefNo,FileName,FileType,UploadType,UploadPath,Status,UploadHistoryId as HistoryId  from MNT_FileUpload (nolock) 
						where Is_Processed <>'Y' and fileType='csv' and FileRefNo=@FileRefNo
 order by fileRefNo
  
  select @TotalCount = count(FileId) from MNT_FileUpload (nolock) 
  where Is_Processed <>'Y' and fileType='csv' and FileRefNo=@FileRefNo
if(@TotalCount=0)
 set @Result='NoFilesToProcess'
else
 set @Result='Passed'
 
 OPEN CR_UPLOAD  
 FETCH NEXT  FROM CR_UPLOAD into @FileId,@FileRefNo,@FileName,@FileType,@UploadType,@UploadPath ,@Status,@HistoryId 
  WHILE @@FETCH_STATUS = 0  
  BEGIN  
	begin try
			--select @FileId,@FileRefNo,@FileName,@FileType,@UploadType,@UploadPath ,@Status ,@HistoryId
			set @TotalCount =0
			set @FilePath = @FileServerPath + @UploadPath + '\' + @FileName
			--select @FilePath
			set @ErrorFile ='Error_' + @FileRefNo + '_' + CAST(@FileId as varchar) + '.log'
			-- READING FILE
			IF OBJECT_ID('tempdb..##TacCSV') IS NOT NULL DROP TABLE ##TacCSV
			IF OBJECT_ID('tempdb..#TacCsvTemp') IS NOT NULL DROP TABLE #TacCsvTemp
			
			set @mySql ='SELECT * into ##TacCSV
						FROM OPENROWSET (
						''Microsoft.ACE.OLEDB.12.0'',
						''Text;HDR=YES;Database=' + @FileServerPath + @UploadPath +';'',
						''SELECT * from [' + @FileName + ']'')'
			--select @mySql
			execute(@mySql)
			select * into #TacCsvTemp from ##TacCSV
			--select * from #TacCsvTemp
			--select @UploadType
			select @TotalCount = COUNT(*) from #TacCsvTemp
			--select @TotalCount
			if(@TotalCount=0)
			begin
				update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(),HasError='Y',TotalRecords=@TotalCount,[Status]='Failed',ErrorMessage ='No Record found in file.' where FileId=@FileId
				 set @Result='Failed'
			end	
			else
			begin
			-- SELECT COLOUMN
			if(@UploadType='T_REP')
			begin
				set @mySql =
				'insert into STG_TAC_ACC_REP
				(
				FileId,FileRefNo,
				ReportNo,
				ReportDate,
				IpNumber,
				AccidentDateTime,
				PlaceOfAccident,
				OperatingHours,
				DutyIO,
				DamageToBus,
				FactsOfIncident,
				IsActive,
				CreatedDate,
				CreatedBy,
				HistoryId
				)
				select ' + cast(@FileId as varchar) + ' as FileId,' + '''' + @FileRefNo + '''' + ' as FileRefNo,
				[acc_rep_num] AS reportNo,    --A
				[report_date] AS reportDate, --D
				[ip_number] AS IPNumber, --E
				[accident_dt_time] AS DateTimeOfAccident, --F
				[plce_of_acc] AS PlaceOfAccident,--H
				[op_hrs_code] AS OperatingHours, --N
				[dutyio_code] AS DutyIO, --AD
				[damage_to_bus] AS DamageToBus, --AF
				[Facts_txt] AS FactsOfIncident -- AG
				,''Y'' isActive,GETDATE(),' + '''' +  @User + ''''+' 
				,' + cast(@HistoryId as varchar) + '
				from #TacCsvTemp' 
				--select @mySql 
				execute(@mySql) 
				--select * from STG_TAC_ACC_REP where FileRefNo=@FileRefNo and FileId=@FileId
			end
			else if(@UploadType='T_IP')
			begin
				set @mySql =
				'insert into STG_TAC_IP
				(
				FileId,FileRefNo,
				IpNumber,
				AccidentDateTime,
				FinalLiabilityDate,
				FinalFinding,
				PlaceOfAccident,
				OperatingHours,
				DutyIO,
				DamageToBus,
				FactsOfIncident,
				ReportNo,
				IsActive,
				CreatedDate,
				CreatedBy,
				HistoryId
				)
				select ' + cast(@FileId as varchar) + ' as FileId,' + '''' + @FileRefNo + '''' + ' as FileRefNo,
				[IP_NUMBER] AS IPNumber,    --A
				[accident_date] AS DateTimeOfAccident, --D
				[final_find_date] AS FinalLiabilityDate, --J
				[final_cause] AS FinalFindings, --K
				[plce_of_acc] AS PlaceOfAccident,--O
				[op_hrs_code] AS OperatingHours, --U
				[dutyio_code] AS DutyIO, --AO
				[damage_to_bus] AS DamageToBus, --AP
				[Facts_txt] AS FactsOfIncident, -- AQ
				[src_rep_num] AS reportNo -- AU
				,''Y'' isActive,GETDATE(),' + '''' +  @User + ''''+'
				,' + cast(@HistoryId as varchar) + '
				from #TacCsvTemp' 
				--select @mySql
				execute(@mySql) 
				--select * from STG_TAC_IP where FileRefNo=@FileRefNo and FileId=@FileId
			end
			else if(@UploadType='T_BUS')
			begin
				set @mySql =
				'insert into STG_TAC_IP_BUS
				(
				FileId,FileRefNo,
				ReportNo,
				IpNumber,
				BusNumber,
				ServiceNo,
				FinalLiability,
				StaffNo,
				IsActive,
				CreatedDate,
				CreatedBy,
				HistoryId
				)
				select ' + cast(@FileId as varchar) + ' as FileId,' + '''' + @FileRefNo + '''' + ' as FileRefNo,
				[acc_rep_number] AS ReportNo,    --A
				[ip_number] AS IPNumber, --B
				[bus_no] AS BusNumber, --D
				[svc_no] AS ServiceNo, --E
				[final_liab] AS FinalLiability, --L
				[driver_num] AS StaffNo --U
				,''Y'' isActive,GETDATE(),' + '''' +  @User + ''''+'
				,' + cast(@HistoryId as varchar) + '
				from #TacCsvTemp'
				--select @mySql
				execute(@mySql) 
				--select * from STG_TAC_IP_BUS where FileRefNo=@FileRefNo and FileId=@FileId
			end
			else if(@UploadType='CLM')
			begin
				set @mySql =
				'insert into STG_CLAIM_FILE_DATA
				(
				FileRefId,FileRefNo,
				ReportNo,
				ReportDate,
				IpNumber,
				AccidentDateTime,
				PlaceOfAccident,
				OperatingHours,
				DutyIO,
				DamageToBus,
				FactsOfIncident,
				BusNumber,
				ServiceNo,
				FinalLiability,
				StaffNo,
				FinalLiabilityDate,
				FinalFinding,
				IsActive,
				CreatedDate,
				CreatedBy,
				HistoryIds
				)
				select ' + cast(@FileId as varchar) + ' as FileId,' + '''' + @FileRefNo + '''' + ' as FileRefNo,
				[ReportNo] AS reportNo,    --A
				[ReportDate] AS ReportDate, --B
				[IpNumber] AS IPNumber, --C
				[AccidentDateTime] AS DateTimeOfAccident, --D
				[PlaceOfAccident] AS PlaceOfAccident,--E
				[OperatingHours] AS OperatingHours, --F
				[DutyIO] AS DutyIO, --G
				[DamageToBus] AS DamageToBus, --H
				[FactsOfIncident] AS FactsOfIncident, -- I
				[BusNumber] AS BusNumber, -- J
				[ServiceNo] AS ServiceNo, -- K
				[FinalLiability] AS FinalLiability, -- L
				[StaffNo] AS StaffNo, -- M
				[FinalLiabilityDate] AS FinalLiabilityDate, -- N
				[FinalFinding] AS FinalFinding -- O
				,''Y'' isActive,GETDATE(),' + '''' +  @User + ''''+' 
				,' + cast(isnull(@HistoryId,'0') as varchar) + '
				from #TacCsvTemp' 
				--select @mySql 
				execute(@mySql) 
				--select * from STG_CLAIM_FILE_DATA where FileRefNo=@FileRefNo and FileRefId=@FileId
			end
			else if(@UploadType='CLM_STD_CD')
			begin
				set @mySql =
				'insert into STG_UPLOAD_STANDARDS_CODES
				(
				FileId,
				FileRefNo,
				STD_CODE_TYPE,
				STD_CODE,
				Description,
				Active_flag,
				CreatedBy_File,
				CreatedDate_File,
				IsActive,
				CreatedDate,
				CreatedBy,
				HistoryId
				)
				select ' + cast(@FileId as varchar) + ' as FileId,' + '''' + @FileRefNo + '''' + ' as FileRefNo,
				[std_code_type] AS STD_CODE_TYPE,    --A
				[std_code] AS STD_CODE, --B
				[description] AS Description, --C
				[active_flag] AS Active_flag, --D
				[create_by] AS CreatedBy_File,--E
				[create_dt] AS CreatedDate_File --F
				,''Y'' isActive,GETDATE(),' + '''' +  @User + ''''+' 
				,' + cast(isnull(@HistoryId,'0') as varchar) + '
				from #TacCsvTemp' 
				--select @mySql 
				execute(@mySql) 
				--select * from STG_UPLOAD_STANDARDS_CODES where FileRefNo=@FileRefNo and FileId=@FileId
			end
			select @TotalCount = COUNT(*) from #TacCsvTemp
			--select @TotalCount
			update MNT_FileUpload set Is_Processed='Y',Processed_Date=GETDATE(),HasError='N',TotalRecords=@TotalCount where FileId=@FileId
			end
			drop table ##TacCSV
			drop table #TacCsvTemp
	end try
	begin catch
	 SELECT @FileId,@FileRefNo,
		  ERROR_NUMBER() AS ErrorNumber
		 ,ERROR_SEVERITY() AS ErrorSeverity
		 ,ERROR_STATE() AS ErrorState
		 ,ERROR_PROCEDURE() AS ErrorProcedure
		 ,ERROR_LINE() AS ErrorLine
		 ,ERROR_MESSAGE() AS ErrorMessage
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
		
		  SELECT @FileId,@FileRefNo,
		  ERROR_NUMBER() AS ErrorNumber
		 ,ERROR_SEVERITY() AS ErrorSeverity
		 ,ERROR_STATE() AS ErrorState
		 ,ERROR_PROCEDURE() AS ErrorProcedure
		 ,ERROR_LINE() AS ErrorLine
		 ,ERROR_MESSAGE() AS ErrorMessage
		 ,getdate()
		 ,@User
		 
		 update MNT_FileUpload set Is_Processed='Y',[Status]='Failed',Processed_Date=GETDATE(), HasError='Y', ErrorMessage = ERROR_MESSAGE()  where FileId =@FileId 
		  set @Result='Failed'
		  
	end catch		
  
 FETCH NEXT  FROM CR_UPLOAD into @FileId,@FileRefNo,@FileName,@FileType,@UploadType,@UploadPath ,@Status,@HistoryId 
  END
 CLOSE CR_UPLOAD  
 DEALLOCATE CR_UPLOAD  
  
  if not exists(select FileId from MNT_FileUpload where FileRefNo =@FileRefNo and HasError='Y')
  and @Result<>'NoFilesToProcess'
  begin
   update MNT_FileUpload set [Status]= case when @Result<>'Failed' then 'INQUE' else @Result end where FileRefNo =@FileRefNo  
  end
  if exists(select FileId from MNT_FileUpload where FileRefNo =@FileRefNo and HasError='Y')
  begin
   update MNT_FileUpload set [Status]= 'Failed' where FileRefNo =@FileRefNo  
  end
 --select * from MNT_FileUpload where fileREfNo='TAC-107' order by 1 desc
 --select * from STG_UploadedFileData 
 --select * from STG_FileUpload_ERRORS
 --status
 --1. Failed - Failed
 --2.InQue	- File read suceesfully qued for further Processing
END
--go

--declare @mResult varchar(50)
--exec [dbo].[Proc_ProcessTACFiles]
--'\\192.168.91.18\',
--'CLM-111',
--'System',
--@mResult out

--select @mResult
--select * from MNT_FileUpload where fileREfNo='CLM-111' order by 1 desc
----select * from MNT_FileUploadHistory where fileREfNo='TAC-107' order by 1 desc
--select * from STG_CLAIM_FILE_DATA
--select * from STG_UPLOAD_STANDARDS_CODES 
----select * from STG_TAC_ACC_REP where FileRefNo='TAC-107' and HistoryId is not null
----select * from STG_TAC_IP_BUS where fileREfNo='TAC-107' and HistoryId is not null
----select * from STG_TAC_IP where FileRefNo='TAC-107' and HistoryId is not null
----select * from STG_UploadedFileData where fileREfNo='TAC-107' order by 1 desc
--rollback tran








GO


