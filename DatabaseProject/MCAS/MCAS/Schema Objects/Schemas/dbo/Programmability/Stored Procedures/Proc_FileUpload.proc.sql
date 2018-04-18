CREATE PROCEDURE [dbo].[Proc_FileUpload]
	@FileId [int] = null,
	@FileName [nvarchar](100),
	@FileType [nvarchar](10),
	@ScheduleStartDateTime [datetime],
	@UploadType [nvarchar](10),
	@UploadPath [nvarchar](100),
	@CreatedBy [varchar](50),
	@OrganizationType [varchar](100) = null,
	@OrganizationName [varchar](100) = null,
	@FileRefNo [varchar](20) output
WITH EXECUTE AS CALLER
AS
BEGIN  
  
if(@FileId is null)  
 BEGIN
  
select @FileId = isnull(MAX(FileId),0) + 1 from MNT_FileUpload (nolock)  
   
 IF @UploadType = 'TAC'  
      SET @FileRefNo = 'TAC-' + @FileId   
    ELSE  
      SET @FileRefNo = 'CLM-' + @FileId  
  
    IF (@ScheduleStartDateTime != null)  
    BEGIN  
      INSERT INTO MNT_UploadFileSchedule (FileRefNo, ScheduleStartDateTime, [Status], Is_Processed, Is_Active, HasError, CreatedBy, CreatedDate)  
        VALUES (@FileRefNo, @ScheduleStartDateTime, 'Incomplete', 'N', 'Y', 'N', @CreatedBy, GETDATE())  
    END  
      
  BEGIN
 INSERT INTO MNT_FileUpload   
 (  
 FileId,  
 FileRefNo,   
 FileName,   
 FileType,   
 UploadType,   
 UploadPath,   
 UploadedDate,   
 [Status],   
 Is_Processed,   
 Is_Active,   
 HasError,   
 CreatedBy,   
 OrganizationType,   
 OrganizationName  
 )  
 VALUES  
 (  
 @FileId,  
 @FileRefNo,   
 @FileName,   
 @FileType,   
 'CLM',   
 @UploadPath,   
 GETDATE(),   
 'Incomplete',   
 'N',   
 'Y',   
 'N',   
 @CreatedBy,   
 @OrganizationType,   
 @OrganizationName  
 )  
 
 SELECT @FileRefNo = FileRefNo FROM MNT_FileUpload WHERE FileId=@FileId
  END
 END  
            
END


