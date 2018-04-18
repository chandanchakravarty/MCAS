IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VEHICLE_Error_Satus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_Error_Satus]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
       
 /*----------------------------------------------------------                                                                        
Proc Name             :    [dbo].[PROC_MIG_IL_VEHICLE_Error_Satus]                                                                 
Created by            :   yogender singh                                                              
Date                  :   07/16/2014                                                             
Purpose               :  To Update upload VEHICLE Detail(SSIS PACKAGE)                 
Revison History       :                                                                        
Used In               : SSIS
------------------------------------------------------------                                                                        
Date     Review By          Comments                                           
                                  
drop PROC_MIG_IL_VEHICLE_Error_Satus                                                               
------   ------------       -------------------------*/                                                                        
                                      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_Error_Satus]                                     
@UPLOAD_FILE_ID INT,
@HASERROR VARCHAR(50)                                       
AS                                            
BEGIN                    
UPDATE MNT_VEHICLELISTINGUPLOAD SET [STATUS]='COMP',IS_PROCESSED='Y',HasError=@HASERROR WHERE UploadFileId=@UPLOAD_FILE_ID
END 


   

            
            
            
            
            
            
            
            
          