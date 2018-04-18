
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]
GO
 

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 15 SEPT 2011                                                      
Purpose               : VALIDATE CONTACT
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]  22
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CONTACT_DETAIL]   
      
 @IMPORT_REQUEST_ID INT      
   
AS                                
BEGIN                         
    
 SET NOCOUNT ON;    
 
 -- VARIABLES TO HOLD THE EXCEPTION GENERATED VALUES

DECLARE @ERROR_NUMBER    INT
DECLARE @ERROR_SEVERITY  INT
DECLARE @ERROR_STATE     INT
DECLARE @ERROR_PROCEDURE VARCHAR(512)
DECLARE @ERROR_LINE  	 INT
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)

 
 BEGIN TRY
    

    
     

     DECLARE @ERROR_NO INT=0
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT 
     DECLARE @LOOP_CONTACT_CODE NVARCHAR(50)   
     DECLARE @COUNTER INT  =1
     
     DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
	 DECLARE @IMPORT_FILE_TYPE INT = 14938 -- FOR CONTACT FILE TYPE 
	 DECLARE @LOOP_CUST_SEQUANCE_NO   INT  
	 DECLARE @LOOP_IMPORT_SEQUANCE_NO INT 
			

     
     
     CREATE TABLE #TEMP_CONTACT
     (
	   ID INT IDENTITY(1,1),	  
       IMPORT_SERIAL_NO BIGINT,
       CONTACT_CODE NVARCHAR(50) NULL,
       CUST_SEQUANCE_NO INT NULL,
       CONTACT_SEQUANCE_NO INT NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_CONTACT
     (      
      IMPORT_SERIAL_NO,
      CONTACT_CODE,
      CUST_SEQUANCE_NO,
      CONTACT_SEQUANCE_NO
     )
     (
      SELECT IMPORT_SERIAL_NO,
             CONTACT_CODE,
             CUSTOMER_SEQUENTIAL,
             CONTACT_SEQUENTIAL
      FROM MIG_IL_CONTACT_DETAILS WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT = COUNT(ID) 
    FROM   #TEMP_CONTACT 

   ------------------------------------              
   -- GET IMPORT FILE NAME        
   ------------------------------------           
    IF(@MAX_RECORD_COUNT>0)        
    BEGIN        
            
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)        
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)        
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND         
            IMPORT_FILE_TYPE  = @IMPORT_FILE_TYPE        
              
    END       
    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
      
       SET @ERROR_NO=0
       
       SELECT @LOOP_CONTACT_CODE       = CONTACT_CODE,
              @IMPORT_SERIAL_NO		   = IMPORT_SERIAL_NO   ,
              @LOOP_CUST_SEQUANCE_NO   = CUST_SEQUANCE_NO,
              @LOOP_IMPORT_SEQUANCE_NO = CONTACT_SEQUANCE_NO         
       FROM   #TEMP_CONTACT WHERE ID   = @COUNTER      
       
     
       -------------------------------------------      
	   -- CHECK THAT RECORD ALREADY PROCESSED  
	   -------------------------------------------      
       IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
                        AND CUSTOMER_SEQUENTIAL= @LOOP_CUST_SEQUANCE_NO AND
                            IMPORT_SEQUENTIAL  = @LOOP_IMPORT_SEQUANCE_NO AND
                         IS_ACTIVE='Y'
                     )          
        SET @ERROR_NO=66      -- THIS RECORD ALREADY PROCESSED
       ELSE IF EXISTS(SELECT * FROM MNT_CONTACT_LIST WITH(NOLOCK) WHERE  CONTACT_CODE=@LOOP_CONTACT_CODE)
        SET @ERROR_NO =52 ---Contact code is already exists.
        
       IF(@ERROR_NO>0)
       BEGIN
       
		   UPDATE MIG_IL_CONTACT_DETAILS
		   SET    HAS_ERRORS=1      
		   WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
		          IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO      
	     
		  EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                 
				 @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,      
				 @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,          
				 @ERROR_SOURCE_FILE     = ''     ,      
				 @ERROR_SOURCE_COLUMN   = ''     ,      
				 @ERROR_SOURCE_COLUMN_VALUE= '' ,      
				 @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,        
				 @ERROR_TYPES           = @ERROR_NO,   
				 @ACTUAL_RECORD_DATA    = '' ,      
				 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR      
				 @ERROR_SOURCE_TYPE     = 'CONT'   
				 

       END 
      
       
         SET @COUNTER+=1
    
    END -- END OF WHILE LOOP
  
 
 
		
	     
					   
	END TRY
	BEGIN CATCH
	
	SELECT 
			 @ERROR_NUMBER    = ERROR_NUMBER(),
			 @ERROR_SEVERITY  = ERROR_SEVERITY(),
			 @ERROR_STATE     = ERROR_STATE(),
			 @ERROR_PROCEDURE = ERROR_PROCEDURE(),
			 @ERROR_LINE	  = ERROR_LINE(),
			 @ERROR_MESSAGE   = ERROR_MESSAGE()
     
  -- CREATING LOG OF EXCEPTION 
  EXEC [PROC_MIG_INSERT_ERROR_LOG]  
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID
 ,@IMPORT_SERIAL_NO		= 0
 ,@ERROR_NUMBER    		= @ERROR_NUMBER
 ,@ERROR_SEVERITY  		= @ERROR_SEVERITY
 ,@ERROR_STATE     	    = @ERROR_STATE
 ,@ERROR_PROCEDURE 		= @ERROR_PROCEDURE
 ,@ERROR_LINE  	   		= @ERROR_LINE
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE
 ,@INITIAL_LOAD_FLAG    = 'Y'
  
	
 END CATCH
		  
  
  
            
END 
 
GO