

/****** Object:  StoredProcedure [dbo].[Proc_ProcessTACFilesSchedule]    Script Date: 02/26/2015 14:35:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProcessTACFilesSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProcessTACFilesSchedule]
GO


/****** Object:  StoredProcedure [dbo].[Proc_ProcessTACFilesSchedule]    Script Date: 02/26/2015 14:35:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*************************
Created by: Pravesh K Chandel
Created Date: 9 Feb 2015
Purpose	: Process uploaded CSV Files which are scheduled
drop proc [Proc_ProcessTACFilesSchedule]
**************************/

CREATE PROCEDURE [dbo].[Proc_ProcessTACFilesSchedule] 
(
@User  nvarchar(20) ='System',
@FileServerPath nvarchar(100)=null
)
AS
BEGIN
declare @FileRefNo nvarchar(50)
declare @UploadType nvarchar(50)
declare @JobId int
if (@FileServerPath is null)
select @FileServerPath = isnull(FileServerPath,'') from MNT_SYS_PARAMS
--select * from MNT_UploadFileSchedule
--where Is_Processed ='N' and Is_Active='Y'
--and ScheduleStartDateTime <=GETDATE()


select top 1 @JobId=JobId,@FileRefNo = FileRefNo 
from MNT_UploadFileSchedule 
where Is_Processed ='N' and Is_Active='Y'
and ScheduleStartDateTime <=GETDATE()
and ISNULL(Attempts,0) <=2
order by ScheduleStartDateTime 

update MNT_UploadFileSchedule set JobStartDateTime=GETDATE() where JobId=@JobId 
select @UploadType = UploadType from MNT_FileUpload (nolock) where FileRefNo=@FileRefNo
						
begin try
declare @Result varchar(20)
if (@UploadType in ('T_REP','T_IP','T_BUS','CLM','CLM_STD_CD'))
begin
	update MNT_UploadFileSchedule set JobStartDateTime=GETDATE(),Status='InProgress' where JobId=@JobId 
	-- process uploaded Files
	exec Proc_ProcessTACFiles @FileServerPath,@FileRefNo,@User ,@Result out

	select @Result

	if(@Result='Failed')
		begin
		select 'Job File Procesing Failed'
		update MNT_UploadFileSchedule set Is_Processed='Y', JobEndDateTime=GETDATE(), Status = 'Failed',ErrorDesc='Job File Procesing Failed' + ERROR_MESSAGE()  where JobId=@JobId 
		end
	else if(@Result='NoFilesToProcess')
		begin
		select 'Job Files not exits for TAC'
		update MNT_UploadFileSchedule set Is_Processed='Y', JobEndDateTime=GETDATE(), Status = 'Failed' ,ErrorDesc='Job Files not exits for TAC' where JobId=@JobId 
		end
	else 
		begin
			-- process upoaded Files Data
			if(@UploadType in ('T_REP','T_IP','T_BUS'))
			begin
				exec Proc_ProcessUploadedData @FileRefNo,@User
			end
			if(@UploadType in ('CLM','CLM_STD_CD'))
			begin
				-- process files data
				exec Proc_ProcessUploadedClaimData @FileRefNo,@User
				----- COnvert files record into reported claim
				exec Proc_CreateReportedClaimFromUpload @FileRefNo,@User
			end
			select 'Job File Procesing passed'
			update MNT_UploadFileSchedule set Is_Processed='Y', JobEndDateTime=GETDATE(), Status = 'Completed'  where JobId=@JobId 
		end
		

end	

end try
	begin catch
		insert into STG_FileUpload_ERRORS
		(
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
		  SELECT @FileRefNo,
		  ERROR_NUMBER() AS ErrorNumber
		 ,ERROR_SEVERITY() AS ErrorSeverity
		 ,ERROR_STATE() AS ErrorState
		 ,ERROR_PROCEDURE() AS ErrorProcedure
		 ,ERROR_LINE() AS ErrorLine
		 ,ERROR_MESSAGE() AS ErrorMessage
		 ,getdate()
		 ,@User
		 update MNT_UploadFileSchedule set Is_Processed='Y', JobEndDateTime=GETDATE(), HasError='Y',ErrorDesc='Error while processing job.'+ERROR_MESSAGE(), Status = 'Failed'  where JobId=@JobId 
		 
	end catch	
	
update MNT_UploadFileSchedule  set Attempts= ISNULL(Attempts,0)+1 where JobId=@JobId 
		
--select * from MNT_UploadFileSchedule --where JobId =@JobId
--select * from STG_FileUpload_ERRORS where FileRefNo=@FileRefNo
--select * from MNT_FileUpload (nolock) 
--						where FileRefNo=@FileRefNo
--select * from STG_UploadedFileData where FileRefNo=@FileRefNo
						
END








GO


