IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
       
 /*----------------------------------------------------------                                                                        
Proc Name             :    [dbo].[PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST]                                                                 
Created by            :   yogender singh                                                              
Date                  :   07/16/2014                                                             
Purpose               :  To Update upload VEHICLE Detail(SSIS PACKAGE)                 
Revison History       :                                                                        
Used In               : SSIS
------------------------------------------------------------                                                                        
Date     Review By          Comments                                           
                                  
drop PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST                                                               
------   ------------       -------------------------*/                                                                        
                                      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST]                                     
@UPLOAD_FILE_ID INT                                       
AS                                            
BEGIN  
declare @SuccessedRecords INT
declare @FailedRecords INT 
select @SuccessedRecords=COUNT(UPLOAD_FILE_ID) from MIG_IL_VEHICLE_DETAIL  where UPLOAD_FILE_ID= @UPLOAD_FILE_ID
select @FailedRecords=(TotalRecords-@SuccessedRecords) from  MNT_VEHICLELISTINGUPLOAD where UploadFileId= @UPLOAD_FILE_ID
                 
UPDATE MNT_VEHICLELISTINGUPLOAD SET [STATUS]='COMP',IS_PROCESSED='Y',PROCESSED_DATE=GETDATE(), UplodedSuccess=@SuccessedRecords,
UploadedFailed=@FailedRecords
WHERE UploadFileId=@UPLOAD_FILE_ID
END 

   

            
            
            
            
            
            
            
            
          