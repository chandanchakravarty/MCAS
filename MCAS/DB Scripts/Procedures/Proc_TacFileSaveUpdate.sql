

/****** Object:  StoredProcedure [dbo].[Proc_TacFileSaveUpdate]    Script Date: 03/18/2015 11:10:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_TacFileSaveUpdate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_TacFileSaveUpdate]
GO



/****** Object:  StoredProcedure [dbo].[Proc_TacFileSaveUpdate]    Script Date: 03/18/2015 11:10:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Proc_TacFileSaveUpdate]                                   
@p_FileName1 nvarchar(100),                                      
@p_FileType1 nvarchar(10),                                      
@p_FileName2 nvarchar(100),                                      
@p_FileType2 nvarchar(10),                                      
@p_FileName3 nvarchar(100),                                      
@p_FileType3 nvarchar(10),                                      
@p_ScheduleStartDateTime datetime,       
@p_Upload nvarchar(10),                                     
@p_UploadType nvarchar(10),                                      
@p_UploadPath nvarchar(100),                                      
@p_CreatedBy varchar(50),                                      
@p_OrganizationType varchar(100),                                      
@p_OrganizationName varchar(100),                                  
@FileId int,                                  
@FileRefNo varchar(50)                    
                                     
AS                                      
  SET FMTONLY OFF;                                      
  BEGIN                                      
  declare @match int                                  
  select @match=count(*) from mnt_fileupload where FileRefNo=@FileRefNo                                  
                                    
  if(@match>0)                                    
  BEGIN                                  
                                 
       IF @p_Upload = 'TAC'                 
     IF @p_FileName1 != ' '                                
    BEGIN                                
      --INSERT INTO MNT_UploadFileSchedule (FileRefNo, ScheduleStartDateTime, [Status], Is_Processed, Is_Active, HasError, CreatedBy, CreatedDate)                                
      --  VALUES (@FileRefNo, @p_ScheduleStartDateTime, 'Incomplete', 'N', 'Y', 'N', @p_CreatedBy, GETDATE())   
        update MNT_UploadFileSchedule set  FileRefNo=@FileRefNo,ScheduleStartDateTime= @p_ScheduleStartDateTime, [Status]='Incomplete' , Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy=@p_CreatedBy,CreatedDate=GETDATE() where FileRefNo=@FileRefNo                        
    END                                
                                       
    BEGIN                                      
      IF @p_FileName1 != ' '                                      
      BEGIN                                      
                                            
                                               
       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName1,FileType=@p_FileType1,UploadType=@p_UploadType,UploadPath=@p_UploadPath, UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy,                
                            
OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@FileId                                   
                                             
      END                                 
      BEGIN                                   
           update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@FileId        
                                         
                                           
                                          
      END                                    
      IF @p_FileName2 != ' '                                      
      BEGIN                                      
                                                 
       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName2,FileType=@p_FileType2,UploadType=@p_UploadType,UploadPath=@p_UploadPath, UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy                          
                            
                              
                                
,OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@FileId                                  
                                             
      END                                  
      BEGIN                                   
                            
       update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@FileId                   
                                          
      END                                      
             
      IF @p_FileName3 != ' '                      
      BEGIN                                      
                               
       update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName3,FileType=@p_FileType3,UploadType=@p_UploadType,UploadPath=@p_UploadPath, UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy                          
                            
                              
                                
,OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@FileId                                  
                                             
      END                                 
      BEGIN                                   
                                         
        update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@FileId                                    
                        
      END                                          
     END                             
      IF @p_Upload = 'CLM'                                
    BEGIN                                
      IF @p_FileName1 != ' '                                
      BEGIN                              
        update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName1,FileType=@p_FileType1,UploadType=@p_UploadType,UploadPath=@p_UploadPath, UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy                      
                        
                          
,                            
OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@FileId                              
      END                                
      BEGIN                               
                                    
        update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@FileId                               
                               
      END                        
       IF @p_FileName2 != ' '                                
      BEGIN                              
        update MNT_FileUpload set FileRefNo=@FileRefNo,FileName=@p_FileName2,FileType=@p_FileType2,UploadType=@p_UploadType,UploadPath=@p_UploadPath, UploadedDate=GETDATE(),[Status]='Incomplete',Is_Processed='N',Is_Active='Y',HasError='N',CreatedBy= @p_CreatedBy                      
                        
                          
,                            
OrganizationType= @p_OrganizationType, OrganizationName= @p_OrganizationName where FileId=@FileId                              
      END                                
      BEGIN                               
                                    
        update MNT_FileUploadHistory set ScheduleStartDateTime=@p_ScheduleStartDateTime where FileId=@FileId                               
                               
      END                              
    END                            
  END                                  
  END 

GO


