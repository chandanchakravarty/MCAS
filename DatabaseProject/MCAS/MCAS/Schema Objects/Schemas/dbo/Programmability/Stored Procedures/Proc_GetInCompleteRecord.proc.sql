CREATE PROCEDURE [dbo].[Proc_GetInCompleteRecord]
WITH EXECUTE AS CALLER
AS
BEGIN                      
   -- IF NO FILES EXISTS                       
   IF NOT EXISTS(
                  SELECT FileId 
                  FROM MNT_FileUpload FU INNER JOIN MNT_UploadFileSchedule UFS 
                         ON FU.FileRefNo=UFS.FileRefNo
                         AND UFS.STATUS='Incomplete'  
                         AND UFS.IS_ACTIVE='Y' 
                         AND UFS.Is_processed='N'
                         AND UFS.FileRefNo LIKE 'TAC%' 
                 )                   
     BEGIN                    
          SELECT FileId AS FileId, FileName ,UploadPath,UploadType    
                 FROM dbo.MNT_FileUpload WHERE 1=2           
        RETURN                     
     END                
   ELSE
     BEGIN
      -- SELECT RECORDS                 
           SELECT TOP 3 FileId AS FileId,FileName ,UploadPath,UploadType  
           FROM MNT_FileUpload FU INNER JOIN MNT_UploadFileSchedule UFS 
                         ON FU.FileRefNo=UFS.FileRefNo
                         AND UFS.STATUS='Incomplete'  
                         AND UFS.IS_ACTIVE='Y' 
                         AND UFS.Is_processed='N' 
                         AND UFS.FileRefNo LIKE 'TAC%' 
           ORDER BY FileId 
       -- CHANGE STATUS AFTER SELECT RECORD     
           UPDATE MNT_UploadFileSchedule  
           SET STATUS='Inprocess' 
           WHERE  MNT_UploadFileSchedule.STATUS='Incomplete'  
                  AND MNT_UploadFileSchedule.IS_ACTIVE='Y' 
                  AND MNT_UploadFileSchedule.Is_processed='N'  
                  AND MNT_UploadFileSchedule.FileRefNo LIKE 'TAC%' 
     END                          
 END


