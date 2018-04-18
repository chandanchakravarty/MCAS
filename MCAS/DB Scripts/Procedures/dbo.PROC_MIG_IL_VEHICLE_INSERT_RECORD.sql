IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VEHICLE_INSERT_RECORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_INSERT_RECORD]
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO      
       
 /*----------------------------------------------------------                                                                        
Proc Name             :    [dbo].[PROC_MIG_IL_VEHICLE_INSERT_RECORD]                                                                 
Created by            :   yogender singh                                                              
Date                  :   07/10/2014                                                             
Purpose               :  To uplaod VEHICLE Detail(SSIS PACKAGE)                 
Revison History       :                                                                        
Used In               : SSIS
------------------------------------------------------------                                                                        
Date     Review By          Comments                                           
                                  
drop Proc PROC_MIG_IL_VEHICLE_INSERT_RECORD                                                               
------   ------------       -------------------------*/                                                                        
                                      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_INSERT_RECORD]                                     
@UPLOAD_FILE_ID INT,
@TotalRecords INT                                       
AS                                            
BEGIN                    
IF EXISTS(Select top 1 1 from MNT_VEHICLELISTINGUPLOAD WHERE UploadFileId=@UPLOAD_FILE_ID)--Status is Inprog since job can take a time 
																							--or can be re-run by user (to prevant same file)
BEGIN 
UPDATE MNT_VEHICLELISTINGUPLOAD SET [STATUS]='INPRG',TotalRecords=@TotalRecords WHERE UploadFileId=@UPLOAD_FILE_ID
END
--Checking If final Table (MNT_VEHICLE_LIST) Has same record then we will update that records bases on VEHICLE_REGISTRATION_NO. 

 UPDATE MIVDL                                        
 SET IMPORT_ACTION='UPDT'                                     
 FROM MIG_IL_VEHICLE_DETAIL MIVDL WITH(NOLOCK)INNER JOIN                                        
  MNT_VEHICLE_LIST  MVL WITH(NOLOCK) ON  MIVDL.VEHICLE_REGISTRATION_NO=MVL.VEHICLE_REGISTRATION_NO                                      
 WHERE MIVDL.UPLOAD_FILE_ID=@UPLOAD_FILE_ID  

--Updating Staging table if there are  duplicate Records

 UPDATE  MK        
 SET IMPORT_ACTION=CASE WHEN T.UPLOAD_SERIAL_NO=MK.UPLOAD_SERIAL_NO THEN  IMPORT_ACTION ELSE 'UPDT' END          
 FROM MIG_IL_VEHICLE_DETAIL MK INNER JOIN        
 (           
  SELECT VEHICLE_REGISTRATION_NO, MIN(UPLOAD_SERIAL_NO) AS UPLOAD_SERIAL_NO        
  FROM MIG_IL_VEHICLE_DETAIL        
  WHERE UPLOAD_FILE_ID=@UPLOAD_FILE_ID AND ISNULL(IMPORT_ACTION,'')!='UPDT'        
  GROUP BY VEHICLE_REGISTRATION_NO      
  HAVING COUNT(VEHICLE_REGISTRATION_NO)>1          
 )T ON MK. UPLOAD_FILE_ID=@UPLOAD_FILE_ID  AND  ISNULL(IMPORT_ACTION,'')!='UPDT'        
 AND MK.VEHICLE_REGISTRATION_NO=T.VEHICLE_REGISTRATION_NO 
 WHERE MK.UPLOAD_FILE_ID=@UPLOAD_FILE_ID       
        
   
  --Insert Data Which are not duplicate 
 INSERT INTO MNT_VEHICLE_LIST
 (
 VEHICLE_REGISTRATION_NO,VEHICLE_MAKE,VEHICLE_MODEL,VEHICLE_CLASS,MODEL_DESCRIPTION,BUS_CAPTAIN
,IS_ACTIVE,CREATE_BY,CREATE_DATE
 )
Select VEHICLE_REGISTRATION_NO,VEHICLE_MAKE,VEHICLE_MODEL,VEHICLE_CLASS,MODEL_DESCRIPTION,BUS_CAPTAIN,'Y',NULL,NULL
from MIG_IL_VEHICLE_DETAIL With(NOLOCK) WHERE UPLOAD_FILE_ID=@UPLOAD_FILE_ID AND ISNULL(IMPORT_ACTION,'')<>'UPDT'
                                         

--Updating Duplicate Data 
UPDATE  MVLL
SET 
MVLL.VEHICLE_MAKE=MIVDD.VEHICLE_MAKE,
MVLL.VEHICLE_MODEL= MIVDD.VEHICLE_MODEL,   
MVLL.VEHICLE_CLASS=MIVDD.VEHICLE_CLASS,
MVLL.MODEL_DESCRIPTION=MIVDD.MODEL_DESCRIPTION,
MVLL.BUS_CAPTAIN=MIVDD.BUS_CAPTAIN
FROM
  MNT_VEHICLE_LIST MVLL WiTH (NOLOCK) INNER JOIN MIG_IL_VEHICLE_DETAIL MIVDD ON  
  MVLL.VEHICLE_REGISTRATION_NO=MIVDD.VEHICLE_REGISTRATION_NO
  WHERE MIVDD.UPLOAD_FILE_ID=@UPLOAD_FILE_ID AND MIVDD.IMPORT_ACTION='UPDT'
   AND MIVDD.UPLOAD_SERIAL_NO in( select MAX(UPLOAD_SERIAL_NO) from MIG_IL_VEHICLE_DETAIL
   WHERE  UPLOAD_FILE_ID=@UPLOAD_FILE_ID AND IMPORT_ACTION='UPDT'
   group BY VEHICLE_REGISTRATION_NO)
   --MAX(UPLOAD_SERIAL_NO) IS apply to update last duplicate record having same  VEHICLE_REGISTRATION_NO
  

  END 

   

            
            
            
            
            
            
            
            
          