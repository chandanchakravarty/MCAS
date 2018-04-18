
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]    Script Date: 12/02/2011 16:59:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]
GO
 

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_IMPORT_REQUEST_FILES]    Script Date: 12/02/2011 16:59:09 ******/
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
            
       
                 
        
          SELECT @IMPORT_REQUEST_FILE_ID=(ISNULL(MAX([IMPORT_REQUEST_FILE_ID]),0)+1)  FROM [dbo].[MIG_IL_IMPORT_REQUEST_FILES]  WITH(NOLOCK)                          
                  
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









GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]    Script Date: 12/02/2011 16:59:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                        
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]                                                      
Created by            : Santosh Kumar Gautam                                                       
Date                  : 17 Aug 2011                                                   
Purpose               : TO INSERT IMPORT REQUEST IN QUQUE FOR PROCESSING      
Revison History       :                                                        
Used In               : INITIAL LOAD           
------------------------------------------------------------                                                        
Date     Review By          Comments                           
                  
drop Proc [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]                                               
------   ------------       -------------------------*/                                                        
--                           
                            
--                         
                      
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_IMPORT_SUMMARY]            
           
   @IMPORT_REQUEST_ID    INT,  
   @IMPORT_SERIAL_NO     BIGINT,  
   @CUSTOMER_ID       INT,  
   @POLICY_ID       INT,  
   @POLICY_VERSION_ID    SMALLINT,  
   @IS_ACTIVE       NCHAR(1),  
   @IS_PROCESSED      CHAR(1),   
   @FILE_TYPE       INT,  
   @FILE_NAME                NVARCHAR(256),  
   @CUSTOMER_SEQUENTIAL      INT,  
   @POLICY_SEQUENTIAL        INT,  
   @ENDORSEMENT_SEQUENTIAL   INT,  
   @IMPORT_SEQUENTIAL        INT,  
   @IMPORT_SEQUENTIAL2       INT,  
   @LOB_ID           INT,  
   @IMPORTED_RECORD_ID       INT,  
   @CLAIM_ID        INT =NULL,  
   @CLAIM_SEQUENTIAL   INT =NULL,
   @PROCESS_TYPE INT = NULL  
  
  
  
AS                        
BEGIN                     
                
       INSERT INTO [dbo].[MIG_IL_IMPORT_SUMMARY]  
           (  
            [IMPORT_REQUEST_ID]                       
           ,[IMPORT_SERIAL_NO]  
           ,[CUSTOMER_ID]  
           ,[POLICY_ID]  
           ,[POLICY_VERSION_ID]  
           ,[IS_ACTIVE]  
           ,[IS_PROCESSED]             
           ,[FILE_TYPE]  
           ,[FILE_NAME]  
           ,[CUSTOMER_SEQUENTIAL]  
           ,[POLICY_SEQUENTIAL]  
           ,[ENDORSEMENT_SEQUENTIAL]  
           ,LOB_ID  
		 ,IMPORT_SEQUENTIAL  
		 ,IMPORTED_RECORD_ID  
		 ,IMPORT_SEQUENTIAL2  
		 ,CLAIM_ID  
		 ,CLAIM_SEQUENTIAL 
		 ,PROCESS_TYPE 
           )  
     VALUES  
           (  
    @IMPORT_REQUEST_ID     
   ,@IMPORT_SERIAL_NO      
   ,@CUSTOMER_ID        
   ,@POLICY_ID        
   ,@POLICY_VERSION_ID     
   ,@IS_ACTIVE        
   ,@IS_PROCESSED       
   ,@FILE_TYPE        
   ,@FILE_NAME                 
   ,@CUSTOMER_SEQUENTIAL       
   ,@POLICY_SEQUENTIAL         
   ,@ENDORSEMENT_SEQUENTIAL    
   ,@LOB_ID  
   ,@IMPORT_SEQUENTIAL  
   ,@IMPORTED_RECORD_ID  
   ,@IMPORT_SEQUENTIAL2  
   ,@CLAIM_ID    
   ,@CLAIM_SEQUENTIAL
   ,@PROCESS_TYPE  
             
           )  
         
                 
         
                
END                            
  
  
  
  
  
  
  
  
            
   
          
          
          
          
  
  
  
  
  
  
  


GO


