IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_FILE_TYPES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_FILE_TYPES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
/*----------------------------------------------------------                                                          
Proc Name             : Dbo.[[PROC_MIG_IL_GET_FILE_TYPES]]                                                         
Created by            : Santosh Kr Gautam                                               
Date                  : 17 Aug 2011                                             
Purpose               : TO GET FILE TYPES LIST          
Revison History       :                                                          
Used In               : INITIAL LOAD             
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc [PROC_MIG_IL_GET_FILE_TYPES]   0,0,1                                        
------   ------------       -------------------------*/                                                          
--                             
      
CREATE PROCEDURE [DBO].[PROC_MIG_IL_GET_FILE_TYPES]     
             
  @IMPORT_REQUEST_ID INT ,      
  @FILE_GROUP  INT=NULL,    
  @LANG_ID INT=1      
AS                          
BEGIN     
 -- FETCH FILE GROUP    
	IF(@FILE_GROUP IS NULL OR @FILE_GROUP=0 )   
		SET @FILE_GROUP=1441 --DEFAULT    

	SELECT MLT.LOOKUP_ID ,ISNULL(MLTm.LOOKUP_DESC,MLT.LOOKUP_DESC) AS LOOKUP_DESC    
	FROM  MNT_LOOKUP_TABLES MLT  WITH(NOLOCK) LEFT OUTER JOIN    
	MNT_LOOKUP_TABLES_MULTILINGUAL MLTM WITH(NOLOCK) ON  MLT.LOOKUP_ID=MLTM.LOOKUP_ID AND MLTM.LANG_ID=@LANG_ID  
	WHERE MLT.LOOKUP_ID IN (1441,1442,1443)     
	ORDER BY LOOKUP_ID    
 
	SELECT MLV.LOOKUP_UNIQUE_ID ,    
	ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) LOOKUP_VALUE_DESC     
	FROM   MNT_LOOKUP_VALUES MLV      WITH(NOLOCK) LEFT OUTER JOIN    
	MNT_LOOKUP_VALUES_MULTILINGUAL MLVM WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID=MLVM.LOOKUP_UNIQUE_ID AND LANG_ID=@LANG_ID      
	WHERE LOOKUP_ID=@FILE_GROUP AND MLV.IS_ACTIVE='Y' AND MLV.LOOKUP_UNIQUE_ID NOT IN      
		(       
			SELECT IMPORT_FILE_TYPE FROM MIG_IL_IMPORT_REQUEST_FILES WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID      
		)     
	END   
         
                             
    
  
