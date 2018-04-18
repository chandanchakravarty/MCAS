/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]    Script Date: 12/02/2011 17:08:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]                                                      
Created by            : Santosh Kumar Gautam                                                           
Date                  : 15 Sep 2011                                                     
Purpose               : VALIDATE CO-APPLICANT DETAILS
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]  22
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_CO_APPLICANT_DETAIL]   
      
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
    

    
     
     DECLARE @CUSTOMER_ID INT
     DECLARE @ERROR_NO INT=0
     DECLARE @APPLICANT_ID INT
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT 
     DECLARE @LOOP_COAPP_CODE NVARCHAR(50)       
  
  
  
	 DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
	 DECLARE @IMPORT_FILE_TYPE INT = 14937 -- FOR CO-APPLICANT FILE TYPE 
	 DECLARE @LOOP_CUST_SEQUANCE_NO   INT  
	 DECLARE @LOOP_COAPP_SEQUANCE_NO   INT  
	 DECLARE @LOOP_IMPORT_SEQUANCE_NO INT                         
	                       
	                       
     DECLARE @COUNTER INT  =1   
     
     CREATE TABLE #TEMP_COAPP
     (
	   ID INT IDENTITY(1,1),	   
       IMPORT_SERIAL_NO BIGINT,      
       COAPP_CODE NVARCHAR(50) NULL,
       CUST_SEQUANCE_NO INT NULL,
       COAPP_SEQUANCE_NO INT NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_COAPP
     (     
      IMPORT_SERIAL_NO,     
      COAPP_CODE,
      CUST_SEQUANCE_NO,
      COAPP_SEQUANCE_NO
     )
     (
      SELECT IMPORT_SERIAL_NO,             
             COAPPLICANT_CODE,
             CUSTOMER_SEQUENTIAL,
             COAPPLICANT_SEQUENTIAL
      FROM MIG_IL_CO_APPLICANT_DETAILS
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT = COUNT(ID) 
    FROM   #TEMP_COAPP 

    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
       SET @CUSTOMER_ID=0
       SET @ERROR_NO=0
       
       SELECT @LOOP_COAPP_CODE		 = COAPP_CODE,
              @IMPORT_SERIAL_NO		 = IMPORT_SERIAL_NO ,
              @LOOP_CUST_SEQUANCE_NO = CUST_SEQUANCE_NO,
              @LOOP_COAPP_SEQUANCE_NO = COAPP_SEQUANCE_NO 
       FROM   #TEMP_COAPP WHERE ID   = @COUNTER      
       
       -------------------------------------------      
	   -- CHECK THAT RECORD ALREADY PROCESSED  
	   -------------------------------------------      
       IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
                        AND CUSTOMER_SEQUENTIAL= @LOOP_CUST_SEQUANCE_NO
                        AND IMPORT_SEQUENTIAL  = @LOOP_COAPP_SEQUANCE_NO
                        AND IS_ACTIVE='Y'
                     )          
       SET @ERROR_NO=66      -- THIS RECORD ALREADY PROCESSED
      
      ELSE IF EXISTS(SELECT * FROM CLT_APPLICANT_LIST WHERE  CONTACT_CODE=@LOOP_COAPP_CODE)
       SET @ERROR_NO =49 --Co-applicant already exists.
        
        
     
       IF(@ERROR_NO>0)
       BEGIN
       
		   UPDATE MIG_IL_CO_APPLICANT_DETAILS
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
				 @ERROR_SOURCE_TYPE     = 'CAPP'   
				 

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
