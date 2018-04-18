CREATE PROCEDURE [dbo].[Proc_TacFileSaveUpdate]
	@p_FileName1 [nvarchar](100),
	@p_FileType1 [nvarchar](10),
	@p_FileName2 [nvarchar](100),
	@p_FileType2 [nvarchar](10),
	@p_FileName3 [nvarchar](100),
	@p_FileType3 [nvarchar](10),
	@p_ScheduleStartDateTime [datetime],
	@p_Upload [nvarchar](10),
	@p_UploadType [nvarchar](10),
	@p_UploadPath [nvarchar](100),
	@p_CreatedBy [varchar](50),
	@p_OrganizationType [varchar](100),
	@p_OrganizationName [varchar](100),
	@FileId [int],
	@FileRefNo [varchar](50)
AS
SET FMTONLY OFF;                                      
  BEGIN                                      
  declare @match int,
		  @T_IP_FileId INT,
		  @T_REP_FileId int,
		  @T_BUS_FileId int,
		  @HistoryID int,
		  @CLM_FileId int,
		  @CLM_STD_CD_FileId int
	 	  
  select @match=count(*) from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo                                  
if(@match>0)                                    
BEGIN  
	 IF ltrim(rtrim(@p_FileName1)) != ''                                
		BEGIN                                
        update MNT_UploadFileSchedule set  FileRefNo=@FileRefNo,ScheduleStartDateTime= @p_ScheduleStartDateTime, [Status]='Incomplete' , Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy=@p_CreatedBy,CreatedDate=GETDATE() where FileRefNo=@FileRefNo   

	    END                                   
	IF @p_Upload = 'TAC'  
    BEGIN    
    select @T_IP_FileId=FileId from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo and UploadType='T_IP'
    select @T_REP_FileId=FileId from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo and UploadType='T_REP'
    select @T_BUS_FileId=FileId from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo and UploadType='T_BUS'
                                 
      IF ltrim(rtrim(@p_FileName1)) != ''   
      BEGIN                                      
      
	      INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date,ScheduleStartDateTime)           
		  select fileid, FileRefNo, @p_FileName1, Processed_Date,@p_ScheduleStartDateTime from MNT_FileUpload (nolock) where FileId=@T_IP_FileId
		  SELECT @HistoryID=@@IDENTITY    

	       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName1,FileType=@p_FileType1,
	        --UploadType=@p_UploadType,
	        UploadPath=@p_UploadPath, UploadHistoryId = @HistoryID,
			UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,                
			OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@T_IP_FileId                                   
      
           --update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@T_IP_FileId  and ID= @HistoryID      
          
      END                                    
      IF ltrim(rtrim(@p_FileName2)) != ''                                      
      BEGIN                                      
	      
      INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date,ScheduleStartDateTime)          
	  select fileid, FileRefNo, @p_FileName2, Processed_Date,@p_ScheduleStartDateTime from MNT_FileUpload (nolock) where FileId=@T_REP_FileId
	  SELECT @HistoryID=@@IDENTITY    

       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName2,FileType=@p_FileType2,
       --UploadType=@p_UploadType,
       UploadPath=@p_UploadPath, UploadHistoryId = @HistoryID,
       UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,
       OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@T_REP_FileId                                  
      
       --update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@T_REP_FileId and ID= @HistoryID                    
      END           
  
IF ltrim(rtrim(@p_FileName3)) != ''               
      BEGIN
      
      INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date,ScheduleStartDateTime)          
	  select fileid, FileRefNo, @p_FileName3, Processed_Date,@p_ScheduleStartDateTime from MNT_FileUpload (nolock) where FileId=@T_BUS_FileId
	  SELECT @HistoryID=@@IDENTITY   
	                                        
       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName3,FileType=@p_FileType3,
       --UploadType=@p_UploadType,
       UploadPath=@p_UploadPath, UploadHistoryId = @HistoryID,
       UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,
       OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@T_BUS_FileId                                 
        
       --update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@T_BUS_FileId  and ID= @HistoryID                                   
      END                                          
     END                             
	IF @p_Upload = 'CLM'                                
    BEGIN 
		select @CLM_FileId=FileId from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo and UploadType='CLM'
		select @CLM_STD_CD_FileId=FileId from mnt_fileupload (NOLOCK) where FileRefNo=@FileRefNo and UploadType='CLM_STD_CD'
                                   
      IF ltrim(rtrim(@p_FileName1)) != ''                                
      BEGIN
			INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date,ScheduleStartDateTime)          
			select fileid, FileRefNo, @p_FileName1, Processed_Date,@p_ScheduleStartDateTime from MNT_FileUpload (nolock) where FileId=@CLM_FileId
			SELECT @HistoryID=@@IDENTITY  
	                                
        update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName1,FileType=@p_FileType1,
        --UploadType=@p_UploadType,
        UploadPath=@p_UploadPath, UploadHistoryId = @HistoryID,
        UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,
        OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@CLM_FileId                              

       -- update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@CLM_FileId
                                       
      END                        
       IF ltrim(rtrim(@p_FileName2)) != ''                                
      BEGIN  
			INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date,ScheduleStartDateTime)          
			select fileid, FileRefNo, @p_FileName2, Processed_Date,@p_ScheduleStartDateTime from MNT_FileUpload (nolock) where FileId=@CLM_STD_CD_FileId
			SELECT @HistoryID=@@IDENTITY  
			                            
        update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName2,FileType=@p_FileType2,
        --UploadType=@p_UploadType, 
        UploadPath=@p_UploadPath, UploadHistoryId = @HistoryID,
        UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,
        OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@CLM_STD_CD_FileId                              

        --update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@CLM_STD_CD_FileId                               
      END                              
    END                            
	END                                  
END

