/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST]    Script Date: 12/02/2011 16:59:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST]
GO
 

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST]    Script Date: 12/02/2011 16:59:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------                                                          
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_IMPORT_REQUEST]                                                         
Created by            : Santosh Kumar Gautam                                                         
Date                  : 19 Aug 2011                                                        
Purpose               : TO INSERT IMPORT REQUEST DETAILS         
Revison History       :                                                          
Used In               : INITIAL LOAD             
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc [PROC_MIG_IL_INSERT_IMPORT_REQUEST]                                                 
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
                        
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST]              
                    
    @IMPORT_REQUEST_ID        INT  OUT ,    
    @REQUEST_DESC             nvarchar(256) ,              
    @CREATED_BY               INT,                                    
    @CREATED_DATETIME         DATETIME      
AS                          
BEGIN                       
                  
                   
                  
            SELECT @IMPORT_REQUEST_ID=(ISNULL(MAX([IMPORT_REQUEST_ID]),0)+1)  FROM [dbo].[MIG_IL_IMPORT_REQUEST]  WITH(NOLOCK)                            
                    
            INSERT INTO [MIG_IL_IMPORT_REQUEST]          
            (          
				[IMPORT_REQUEST_ID]    
			   ,[REQUEST_STATUS]   
			   ,[SUBMITTED_DATE]                
			   ,[SUBMITTED_BY]                  
			   ,[IS_PROCESSED]                      
			   ,[HAS_ERRORS]                    
			   ,[IS_ACTIVE]               
			   ,[IS_DELETED]              
			   ,[CREATED_BY]              
			   ,[CREATED_DATETIME]        
			   ,[REQUEST_DESC]            
            )          
			VALUES          
			(          
				 @IMPORT_REQUEST_ID                      
				,'NSTART' -- NOT STARTED    
				,GETDATE()            
				,@CREATED_BY             
				,'N'           
				,'N'            
				,'Y'                
				,'N'                     
				,@CREATED_BY              
				,@CREATED_DATETIME        
				,@REQUEST_DESC        
			            
			)          
                 
END 





GO
