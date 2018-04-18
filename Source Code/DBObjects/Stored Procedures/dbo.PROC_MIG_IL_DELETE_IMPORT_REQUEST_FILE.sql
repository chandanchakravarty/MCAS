
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]
GO


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                  
Proc Name             : Dbo.[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]                                                  
Created by            : Santosh Kumar Gautam                                                 
Date                  : 19 Aug 2011                                         
Purpose               : TO DELETE FILE OF CORRESPONDING IMPORT REQUEST
Revison History       :                                                  
Used In               : INITIAL LOAD     
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc [PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]                                         
------   ------------       -------------------------*/                                                  
--                     
                      
--                   
                
CREATE PROCEDURE [dbo].[PROC_MIG_IL_DELETE_IMPORT_REQUEST_FILE]               
 @IMPORT_REQUEST_ID        INT,    
 @IMPORT_REQUEST_FILE_ID        INT   
   
AS                      
BEGIN               
          
   DELETE FROM MIG_IL_IMPORT_REQUEST_FILES   
   WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND   
         IMPORT_REQUEST_FILE_ID=@IMPORT_REQUEST_FILE_ID             
         
END                      
  
GO