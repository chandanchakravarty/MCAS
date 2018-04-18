IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                          
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]                                                         
Created by            : Santosh Kumar Gautam                                                         
Date                  : 17 AUG 2011                                                        
Purpose               : TO INSERT IMPORT REQUEST FILE DETAILS  
Revison History       :                                                          
Used In               : INITIAL LOAD             
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc [PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]                                                 
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
                        
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]              
                    
       
 @IMPORT_REQUEST_FILE_ID        INT  OUT,        
 @IMPORT_REQUEST_ID        INT  ,          
 @IMPORT_FILE_NAME         nvarchar(256) ,    
 @DISPLAY_FILE_NAME        nvarchar(512) ,     
 @IMPORT_FILE_PATH         nvarchar(256) ,           
 @IMPORT_FILE_TYPE         INT  ,   
 @IMPORT_FILE_GROUP_TYPE   INT,  
 @FILE_IMPORTED_DATE       datetime,        
 @FILE_IMPORTED_BY         int        
        
AS                          
BEGIN                       
 
	SELECT @IMPORT_REQUEST_FILE_ID=(ISNULL(MAX([IMPORT_REQUEST_FILE_ID]),0)+1)  FROM [dbo].[MIG_IMPORT_REQUEST_FILES]  WITH(NOLOCK)                            
	        
	INSERT INTO [MIG_IL_IMPORT_REQUEST_FILES]          
	(          
	IMPORT_REQUEST_FILE_ID         
	,[IMPORT_REQUEST_ID]                
	,[IMPORT_FILE_NAME]                 
	,[DISPLAY_FILE_NAME]  
	,[IMPORT_FILE_PATH]                 
	,[IMPORT_FILE_TYPE]   
	,[IMPORT_FILE_GROUP_TYPE]     
	,[FILE_IMPORTED_DATE]                 
	,[FILE_IMPORTED_BY]         
	)          
	VALUES          
	(          
	@IMPORT_REQUEST_FILE_ID  
	,@IMPORT_REQUEST_ID              
	,@IMPORT_FILE_NAME   
	,@DISPLAY_FILE_NAME                   
	,@IMPORT_FILE_PATH            
	,@IMPORT_FILE_TYPE     
	,@IMPORT_FILE_GROUP_TYPE     
	,@FILE_IMPORTED_DATE        
	,@FILE_IMPORTED_BY            
	)          
                 
END   
  
  
  
  
  
