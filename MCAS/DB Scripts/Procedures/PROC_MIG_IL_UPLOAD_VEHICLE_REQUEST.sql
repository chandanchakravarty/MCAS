IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
       
 /*----------------------------------------------------------                                                                        
Proc Name             :    [dbo].[PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST]                                                                 
Created by            :   yogender singh                                                              
Date                  :   07/10/2014                                                             
Purpose               :  To uplaod VEHICLE Detail(SSIS PACKAGE)                 
Revison History       :                                                                        
Used In               : SSIS
------------------------------------------------------------                                                                        
Date     Review By          Comments                                           
                                  
drop Proc PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST                                                               
------   ------------       -------------------------*/                                                                        
                                      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_UPLOAD_VEHICLE_REQUEST]                                     
                                           
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



            
            
            
            
            
            
            
            
          