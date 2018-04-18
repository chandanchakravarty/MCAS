CREATE PROCEDURE [dbo].[PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST]
WITH EXECUTE AS CALLER
AS
BEGIN                      
                            
              
   -- IF ALREADY ETL IS RUNNING THEN REURN NULL                      
   IF EXISTS(SELECT UploadFileId FROM [MNT_VEHICLELISTINGUPLOAD]  WITH(NOLOCK) WHERE [STATUS]='INPRG'  AND IS_ACTIVE='Y'  )                   
     BEGIN                    
          SELECT   0 AS UPLOAD_FILE_ID,              
                     '' AS UPLOAD_FILE_NAME              
                          
                               
        RETURN                     
     END                
                         
   IF EXISTS                          
   (                         
    SELECT M.UploadFileId                      
    FROM   [MNT_VEHICLELISTINGUPLOAD] M WITH(NOLOCK)                        
    WHERE  [STATUS] ='INQUEUE' AND [IS_PROCESSED]='N'  AND [IS_ACTIVE]   ='Y'                          
    )                          
   BEGIN                 
                 
              
     -- SELECT ALL RECORDS                 
     SELECT top 1 UploadFileId AS UPLOAD_FILE_ID, UploadFileName AS UPLOAD_FILE_NAME            
                FROM  [MNT_VEHICLELISTINGUPLOAD]             
  WHERE  [STATUS]='INQUEUE' AND [IS_PROCESSED]='N' AND  [IS_ACTIVE]   ='Y'    
     Order By UploadFileId                         
    END                          
 ELSE    -- IF NO FILES EXISTS                            
   SELECT    0 AS UPLOAD_FILE_ID,              
            '' AS UPLOAD_FILE_NAME           
                 
                                 
END


