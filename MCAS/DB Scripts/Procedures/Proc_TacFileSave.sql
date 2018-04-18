

/****** Object:  StoredProcedure [dbo].[Proc_TacFileSave]    Script Date: 03/11/2015 13:12:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_TacFileSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_TacFileSave]
GO



/****** Object:  StoredProcedure [dbo].[Proc_TacFileSave]    Script Date: 03/11/2015 13:12:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_TacFileSave] @p_FileName1 nvarchar(100),          
@p_FileType1 nvarchar(10),          
@p_FileName2 nvarchar(100),          
@p_FileType2 nvarchar(10),          
@p_FileName3 nvarchar(100),          
@p_FileType3 nvarchar(10),          
@p_ScheduleStartDateTime datetime,          
@p_UploadType nvarchar(10),          
@p_UploadPath nvarchar(100),          
@p_CreatedBy varchar(50),          
@p_OrganizationType varchar(100),          
@p_OrganizationName varchar(100)  
         
AS          
  SET FMTONLY OFF;          
  BEGIN          
  DECLARE @FileId int   
  DECLARE @HistoryID int       
  if(@FileId is null)          
 begin          
          
select @FileId = isnull(MAX(FileId),0) + 1 from MNT_FileUpload (nolock)        
    DECLARE @p_FileRefNo nvarchar(50)          
          
    IF @p_UploadType = 'TAC'          
      SET @p_FileRefNo = (SELECT          
        'TAC-' + CAST((ISNULL(MAX(JobId), 0) + 1) AS varchar(max))          
      FROM MNT_UploadFileSchedule)          
    ELSE          
      SET @p_FileRefNo = (SELECT          
        'CLM-' + CAST((ISNULL(MAX(JobId), 0) + 1) AS varchar(max))          
      FROM MNT_UploadFileSchedule)          
          
    IF @p_FileName1 != ' '          
    BEGIN          
      INSERT INTO MNT_UploadFileSchedule (FileRefNo, ScheduleStartDateTime, [Status], Is_Processed, Is_Active, HasError, CreatedBy, CreatedDate)          
        VALUES (@p_FileRefNo, @p_ScheduleStartDateTime, 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, GETDATE())          
    END          
          
          
    IF @p_UploadType = 'TAC'          
    BEGIN          
      IF @p_FileName1 != ' '          
      BEGIN          
        INSERT INTO MNT_FileUpload (FileRefNo, FileName, FileType, UploadType, UploadPath, UploadedDate, [Status], Is_Processed, Is_Active, HasError, CreatedBy, OrganizationType, OrganizationName)          
          VALUES (@p_FileRefNo, @p_FileName1, @p_FileType1, 'T_IP', @p_UploadPath, GETDATE(), 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, @p_OrganizationType, @p_OrganizationName)         
                 
      END          
      BEGIN         
               
        INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date)          
          VALUES (@FileId,@p_FileRefNo, @p_FileName1,GETDATE())  
          SELECT @HistoryID=@@IDENTITY   
            
                
          update MNT_FileUpload set UploadHistoryId=@HistoryID where FileRefNo=@p_FileRefNo and FileId=@FileId       
                
      END    
              
          
      IF @p_FileName2 != ' '          
      BEGIN          
        INSERT INTO MNT_FileUpload (FileRefNo, FileName, FileType, UploadType, UploadPath, UploadedDate, [Status], Is_Processed, Is_Active, HasError, CreatedBy, OrganizationType, OrganizationName)          
          VALUES (@p_FileRefNo, @p_FileName2, @p_FileType2, 'T_REP', @p_UploadPath, GETDATE(), 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, @p_OrganizationType, @p_OrganizationName)          
      END          
       BEGIN         
              
        INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date)          
          VALUES (@FileId+1,@p_FileRefNo, @p_FileName2,GETDATE())  
          SELECT @HistoryID=@@IDENTITY   
                  
         update MNT_FileUpload set UploadHistoryId=@HistoryID where FileRefNo=@p_FileRefNo and FileId=@FileId+1       
      END        
              
      IF @p_FileName3 != ' '          
      BEGIN          
        INSERT INTO MNT_FileUpload (FileRefNo, FileName, FileType, UploadType, UploadPath, UploadedDate, [Status], Is_Processed, Is_Active, HasError, CreatedBy, OrganizationType, OrganizationName)          
          VALUES (@p_FileRefNo, @p_FileName3, @p_FileType3, 'T_BUS', @p_UploadPath, GETDATE(), 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, @p_OrganizationType, @p_OrganizationName)          
      END          
      BEGIN         
               
        INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date)          
          VALUES (@FileId+2,@p_FileRefNo, @p_FileName3,GETDATE())    
          SELECT @HistoryID=@@IDENTITY    
          update MNT_FileUpload set UploadHistoryId=@HistoryID where FileRefNo=@p_FileRefNo and FileId=@FileId+2     
                
      END        
          
    END          
          
    IF @p_UploadType = 'CLM'          
    BEGIN          
      IF @p_FileName1 != ' '          
      BEGIN        
        INSERT INTO MNT_FileUpload (FileRefNo, FileName, FileType, UploadType, UploadPath, UploadedDate, [Status], Is_Processed, Is_Active, HasError, CreatedBy, OrganizationType, OrganizationName)          
          VALUES (@p_FileRefNo, @p_FileName1, @p_FileType1, 'CLM', @p_UploadPath, GETDATE(), 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, @p_OrganizationType, @p_OrganizationName)        
      END          
      BEGIN         
              
        INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date)          
          VALUES (@FileId,@p_FileRefNo, @p_FileName1,GETDATE())    
          SELECT @HistoryID=@@IDENTITY   
          update MNT_FileUpload set UploadHistoryId=@HistoryID where FileRefNo=@p_FileRefNo and FileId=@FileId      
         
      END        
      IF @p_FileName2 != ' '          
      BEGIN        
        INSERT INTO MNT_FileUpload (FileRefNo, FileName, FileType, UploadType, UploadPath, UploadedDate, [Status], Is_Processed, Is_Active, HasError, CreatedBy, OrganizationType, OrganizationName)          
          VALUES (@p_FileRefNo, @p_FileName2, @p_FileType2, 'CLM_STD_CD', @p_UploadPath, GETDATE(), 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, @p_OrganizationType, @p_OrganizationName)        
      END          
      BEGIN         
              
        INSERT INTO MNT_FileUploadHistory (fileid, FileRefNo, FileName, Processed_Date)          
          VALUES (@FileId+1,@p_FileRefNo, @p_FileName1,GETDATE())    
          SELECT @HistoryID=@@IDENTITY   
          update MNT_FileUpload set UploadHistoryId=@HistoryID where FileRefNo=@p_FileRefNo and FileId=@FileId+1      
         
      END        
    END          
  END         
  END 
GO


