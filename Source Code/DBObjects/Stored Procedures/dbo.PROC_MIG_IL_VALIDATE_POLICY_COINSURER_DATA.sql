
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]    Script Date: 12/02/2011 17:08:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 22 SEPT 2011                                                      
Purpose               : VALIDATE CONTACT
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]  273
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_COINSURER_DATA]   
      
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
     DECLARE @LOOP_COINSURANCE_NAME INT
     DECLARE @LOOP_LEADER_FOLLOWER INT
     DECLARE @COUNTER INT  =1
  
			

     
     
     CREATE TABLE #TEMP_COINSURER
     (
	   ID INT IDENTITY(1,1),	  
       IMPORT_SERIAL_NO BIGINT,
       COINSURANCE_NAME INT NULL,
       LEADER_FOLLOWER INT NULL,
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_COINSURER
     (      
      IMPORT_SERIAL_NO,
      COINSURANCE_NAME,
      LEADER_FOLLOWER
     )
     (
      SELECT IMPORT_SERIAL_NO,
             COINSURANCE_NAME,
             LEADER_FOLLOWER
      FROM MIG_IL_POLICY_COINSURER_DETAILS WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT = COUNT(ID) 
    FROM   #TEMP_COINSURER 

    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
      
       SET @ERROR_NO=0
       SET @LOOP_LEADER_FOLLOWER=0
       
       SELECT @LOOP_COINSURANCE_NAME = COINSURANCE_NAME,
              @IMPORT_SERIAL_NO	     = IMPORT_SERIAL_NO,
              @LOOP_LEADER_FOLLOWER  = LEADER_FOLLOWER            
       FROM   #TEMP_COINSURER WHERE ID   = @COUNTER      
       
    
    
       ------------------------------------      
	   -- VALIDATE COINSURANCE COMPANY
	   ------------------------------------    
       IF NOT EXISTS(SELECT * FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK) WHERE  REIN_COMAPANY_ID=@LOOP_COINSURANCE_NAME)
        SET @ERROR_NO =55 --Coinsurance name does not exists        
       ------------------------------------      
	   -- VALIDATE LEADER/FOLLOWER CODES
	   ------------------------------------    
       ELSE IF(@LOOP_LEADER_FOLLOWER NOT IN(14548,14549))      
        SET @ERROR_NO =62 --Invalid code for Leader/Follower 
      
        
        
        select @ERROR_NO
        
       IF(@ERROR_NO>0)
       BEGIN
       
      
       
		   UPDATE MIG_IL_POLICY_COINSURER_DETAILS
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
				 @ERROR_SOURCE_TYPE     = 'COIS'   
				 

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