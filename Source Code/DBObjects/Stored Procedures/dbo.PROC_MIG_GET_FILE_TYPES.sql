IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_GET_FILE_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_GET_FILE_TYPES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                      
Proc Name             : Dbo.[PROC_MIG_GET_FILE_TYPES]                                                     
Created by            : Aditya Goel                                                    
Date                  : 11 May 2011                                                    
Purpose               : TO GET FILE TYPES LIST      
Revison History       :                                                      
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD         
------------------------------------------------------------                                                      
Date     Review By          Comments                         
                
drop Proc [PROC_MIG_GET_FILE_TYPES]                                       
------   ------------       -------------------------*/                                                      
--                         
  
CREATE PROCEDURE [DBO].[PROC_MIG_GET_FILE_TYPES]     
         
  @IMPORT_REQUEST_ID INT   ,  
  @LANG_ID INT=null  
AS                      
BEGIN                  
          
SELECT ISNULL(MLVM.LOOKUP_UNIQUE_ID,MLV.LOOKUP_UNIQUE_ID)LOOKUP_UNIQUE_ID,ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)LOOKUP_VALUE_DESC FROM MNT_LOOKUP_VALUES MLV WITH(NOLOCK)  
LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID=MLVM.LOOKUP_UNIQUE_ID AND LANG_ID=@LANG_ID  
 WHERE LOOKUP_ID=1435 AND MLV.IS_ACTIVE='Y' AND ISNULL(MLVM.LOOKUP_UNIQUE_ID,MLV.LOOKUP_UNIQUE_ID) NOT IN  
 (   
   SELECT IMPORT_FILE_TYPE FROM MIG_IMPORT_REQUEST_FILES WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID  
  )  
    
END                          

GO

