
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]    Script Date: 12/02/2011 17:08:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]
GO


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]    Script Date: 12/02/2011 17:08:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]                                                          
Created by            : Santosh Kumar Gautam                                                             
Date                  : 11 OCT 2011                                                        
Purpose               : VALIDATE POLICY LOCATION  
Revison History       :                                                              
Used In               : INITIAL LOAD                 
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc [PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]  22  
------   ------------       -------------------------*/     
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_CLAUSE_DATA]   
  
--------------------------------- INPUT PARAMETER  
@IMPORT_REQUEST_ID  INT  
-------------------------------------------------  
    
AS  
BEGIN  
   
-------------------------------- DECLARATION PART  
----------------------------------------------------------------------------------------  
DECLARE @ERROR_NUMBER    INT    
DECLARE @ERROR_SEVERITY  INT    
DECLARE @ERROR_STATE     INT    
DECLARE @ERROR_PROCEDURE VARCHAR(512)    
DECLARE @ERROR_LINE    INT    
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
  
  
DECLARE @LOOP_POLICY_NUMBER NVARCHAR(50)       
DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_IMPORT_SERIAL_NO INT      
DECLARE @COUNTER INT  =1    
DECLARE @MAX_RECORD_COUNT INT     
DECLARE @ERROR_NO INT=0    
DECLARE @IMPORT_SERIAL_NO INT   
DECLARE @POLICY_SEQUENCE_NO INT
DECLARE @END_SEQUENCE_NO INT
DECLARE @CLAUSE_SEQUENCE_NO INT
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
DECLARE @IMPORT_FILE_TYPE INT = 14962 -- FOR CLAUSE FILE TYPE  --CHANGE
  
DECLARE @COUNTRY_ID INT  
DECLARE @STATE_ID INT  
  
  
   -- validation for country and state  
  
BEGIN TRY  
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED  
-----------------------------------------------------------------------------------------  
    
   
      SELECT
		ROW_NUMBER() OVER(ORDER BY IMPORT_SERIAL_NO) as ID,    
		IMPORT_SERIAL_NO,
		POLICY_SEQUENTIAL,
		ENDORSEMENT_SEQUENTIAL,
		CLAUSE_SEQUENTIAL
	INTO
		#TEMP_CLAUSE
      FROM MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)     --CHANGE
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0    
       
------------------------------------------------------------------------------------------  
  
  
  ------------------------------------          
   -- GET MAX RECOUNT COUNT    
   ------------------------------------       
    SELECT @MAX_RECORD_COUNT = COUNT(ID)     
    FROM   #TEMP_CLAUSE 
    
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
  
      SELECT   
       @IMPORT_SERIAL_NO     = IMPORT_SERIAL_NO,             --CHANGE
	   @POLICY_SEQUENCE_NO   = POLICY_SEQUENTIAL,   
	   @END_SEQUENCE_NO      = ENDORSEMENT_SEQUENTIAL, 
	   @CLAUSE_SEQUENCE_NO   = CLAUSE_SEQUENTIAL 
      FROM   #TEMP_CLAUSE (NOLOCK) WHERE ID   = @COUNTER       
  
  
 
	 ------------------------------------  VALIDATION OF ALREADY PROCESSED RECORD  -----------------------------------------
  
	 IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
                        AND POLICY_SEQUENTIAL= @POLICY_SEQUENCE_NO AND 
                            ENDORSEMENT_SEQUENTIAL= @END_SEQUENCE_NO AND
                            IMPORT_SEQUENTIAL=@CLAUSE_SEQUENCE_NO AND --CHANGE
                            IS_ACTIVE='Y' 
                     )          
      SET @ERROR_NO=66      -- This record already processed.  
     
  
      IF(@ERROR_NO>0)    
      BEGIN    
            
      UPDATE MIG_IL_POLICY_CLAUSES_DETAILS   --CHANGE
      SET    HAS_ERRORS=1          
      WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND     
       IMPORT_SERIAL_NO        = @IMPORT_SERIAL_NO     
  
  
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
        @ERROR_SOURCE_TYPE     = 'CLSS'       
    END  
      
  
    SET @COUNTER+=1    
  END  
  
END TRY  
BEGIN CATCH    
     
 SELECT     
    @ERROR_NUMBER    = ERROR_NUMBER(),    
    @ERROR_SEVERITY  = ERROR_SEVERITY(),    
    @ERROR_STATE     = ERROR_STATE(),    
    @ERROR_PROCEDURE = ERROR_PROCEDURE(),    
    @ERROR_LINE   = ERROR_LINE(),    
    @ERROR_MESSAGE   = ERROR_MESSAGE()    
         
  -- CREATING LOG OF EXCEPTION     
  EXEC [PROC_MIG_INSERT_ERROR_LOG]      
  @IMPORT_REQUEST_ID    = @IMPORT_REQUEST_ID    
 ,@IMPORT_SERIAL_NO  = 0    
 ,@ERROR_NUMBER      = @ERROR_NUMBER    
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY    
 ,@ERROR_STATE          = @ERROR_STATE    
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE    
 ,@ERROR_LINE        = @ERROR_LINE    
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE    
 ,@INITIAL_LOAD_FLAG    = 'Y'    
      
     
 END CATCH    
  
   
END  
  
  
  


GO