IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_ERROR_LOG]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_ERROR_LOG]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.PROC_MIG_INSERT_ERROR_LOG                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 03 June 2011                                                          
Purpose               : TO ERROR LOG DETAILS
Revison History       :                                                            
Used In               : ACCEPETED COINSURANCE DATA INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc PROC_MIG_INSERT_ERROR_LOG       174,'1000023'
                                      
------   ------------       -------------------------*/                                                            
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_ERROR_LOG]  

  @IMPORT_REQUEST_ID    BIGINT
 ,@IMPORT_SERIAL_NO		BIGINT
 ,@ERROR_NUMBER    		INT
 ,@ERROR_SEVERITY  		INT
 ,@ERROR_STATE     		INT
 ,@ERROR_PROCEDURE 		VARCHAR(512)
 ,@ERROR_LINE  	   		INT
 ,@ERROR_MESSAGE        NVARCHAR(MAX)
 ,@INITIAL_LOAD_FLAG    NCHAR(1)='N'
                                 
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
 
		 INSERT INTO MIG_ERROR_LOG
		 (	
			IMPORT_REQUEST_ID, 
			IMPORT_SERIAL_NO,
			ERROR_DATETIME,
			[ERROR_NUMBER],
			[ERROR_SEVERITY],
			[ERROR_STATE],
			ERROR_PROC_NAME,
			[ERROR_LINE],
			[ERROR_MESSAGE],
			INITIAL_LOAD_FLAG
		)
		VALUES
		(
			 @IMPORT_REQUEST_ID
			,@IMPORT_SERIAL_NO	
			,GETDATE()
			,@ERROR_NUMBER    	
			,@ERROR_SEVERITY  	
			,@ERROR_STATE     	
			,@ERROR_PROCEDURE 	
			,@ERROR_LINE  	   	
			,@ERROR_MESSAGE 
			,@INITIAL_LOAD_FLAG   
		  
		)
 
 

 END



GO

