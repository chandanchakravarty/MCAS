
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST_FILES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST_FILES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                          
Proc Name             : Dbo.[PROC_MIG_IL_GET_IMPORT_REQUEST_FILES]                                                         
Created by            : Santosh Kumar Gautam                                                         
Date                  : 17 Aug 2011                                                        
Purpose               : TO INSERT IMPORT REQUEST FILE DETAILS          
Revison History       :                                                          
Used In               : INITIAL LOAD             
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc [PROC_MIG_IL_GET_IMPORT_REQUEST_FILES]   274 ,1                                              
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
                        
CREATE PROCEDURE [dbo].[PROC_MIG_IL_GET_IMPORT_REQUEST_FILES]              
             
    @IMPORT_REQUEST_ID        INT,       
    @LANG_ID                  INT              
AS                          
BEGIN                       
                  
      IF EXISTS(SELECT  IMPORT_REQUEST_ID FROM MIG_IL_IMPORT_REQUEST_FILES WHERE IMPORT_REQUEST_ID= @IMPORT_REQUEST_ID)  
      BEGIN        
        SELECT MIR.IMPORT_REQUEST_ID,          
               MIRF.IMPORT_REQUEST_FILE_ID,          
               MIRF.DISPLAY_FILE_NAME ,   
               MIR.REQUEST_STATUS,         
              CASE WHEN @LANG_ID=1 THEN CONVERT(varchar(10),MIRF.FILE_IMPORTED_DATE,101)       
                   ELSE CONVERT(varchar(10),MIRF.FILE_IMPORTED_DATE,103) END      
               AS FILE_IMPORTED_DATE,        
               ISNULL(U.USER_FNAME,'')+' '+ISNULL(U.USER_LNAME,'') AS FILE_IMPORTED_BY,              
               ISNULL(MLVM.LOOKUP_VALUE_DESC, MLV.LOOKUP_VALUE_DESC)  AS 'IMPORT_FILE_TYPE',  
               ISNULL(MLTM.LOOKUP_DESC, MLT.LOOKUP_DESC)  AS 'IMPORT_FILE_GROUP_TYPE'  ,  
               IMPORT_FILE_TYPE AS   FILE_TYPE   
                         
        FROM MIG_IL_IMPORT_REQUEST   AS MIR  WITH(NOLOCK) LEFT OUTER JOIN          
             [MIG_IL_IMPORT_REQUEST_FILES]  AS MIRF WITH(NOLOCK) ON MIR.IMPORT_REQUEST_ID=MIRF.IMPORT_REQUEST_ID  LEFT OUTER JOIN     
             MNT_LOOKUP_TABLES    AS MLT  WITH(NOLOCK) ON MLT.LOOKUP_ID=MIRF.IMPORT_FILE_GROUP_TYPE LEFT OUTER JOIN     
             MNT_LOOKUP_TABLES_MULTILINGUAL AS MLTM WITH(NOLOCK) ON MLTM.LOOKUP_ID=MLT.LOOKUP_ID AND MLTM.LANG_ID=@LANG_ID LEFT  OUTER JOIN         
             MNT_LOOKUP_VALUES    AS MLV  WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID=MIRF.IMPORT_FILE_TYPE  LEFT OUTER JOIN          
             MNT_LOOKUP_VALUES_MULTILINGUAL AS MLVM WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID =MLVM.LOOKUP_UNIQUE_ID   AND MLVM.LANG_ID=@LANG_ID LEFT OUTER JOIN  
             MNT_USER_LIST        AS U    WITH(NOLOCK) ON MIRF.FILE_IMPORTED_BY=U.USER_ID                      
        WHERE MIR.IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID          
      END             
                
           
                  
END   
  
  
  
  
  
  







