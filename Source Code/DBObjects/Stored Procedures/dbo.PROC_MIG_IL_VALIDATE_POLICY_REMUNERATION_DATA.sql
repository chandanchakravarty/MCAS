
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================    
-- Author:  <ATUL KUMAR SINGH>    
-- Create date: <2011-09-15>    
-- Description: <validate file data [file 'remuneration']>    
-- =============================================    
--  UPDATE HISTORY    
-- =============================================    
-- Author:   Pradeep Kushwaha   
-- update date:  18-OCt-2011   
-- Description:  <validate file data [file 'remuneration']>       
-- =============================================    
-- DROp PROC [PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]     
    
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
    
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)             
DECLARE @IMPORT_FILE_TYPE INT = 14940 -- FOR POLICY Remuneration FILE TYPE      
      
    
    
DECLARE @LOOP_IMPORT_SERIAL_NO INT        
DECLARE @COUNTER INT  =1      
DECLARE @MAX_RECORD_COUNT INT       
DECLARE @ERROR_NO INT=0      
DECLARE @IMPORT_SERIAL_NO INT     
          
DECLARE @COMMISION_TYPE INT     
DECLARE @AGENCY_CODE INT 
DECLARE @COMMISION INT     
DECLARE @LEADER INT     

DECLARE @LOOP_POLICY_SEQUANCE_NO INT      
DECLARE @LOOP_END_SEQUANCE_NO INT      
DECLARE @LOOP_REMUNERATION_SERIAL_NO INT          
    
    
    
   -- validation for country and state    
    
BEGIN TRY    
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED    
-----------------------------------------------------------------------------------------    
      
     CREATE TABLE #TEMP_POLICY      
     (      
	  ID INT IDENTITY(1,1),      
      IMPORT_SERIAL_NO BIGINT,      
      POLICY_SEQUENCE_NO INT,  
	  END_SEQUENCE_NO INT,  
	  REMUNERATION_SEQUENCE_NO  INT ,  
     )      
    
    
    
  INSERT INTO #TEMP_POLICY      
     (      
      IMPORT_SERIAL_NO,
       POLICY_SEQUENCE_NO ,
	  END_SEQUENCE_NO ,  
	  REMUNERATION_SEQUENCE_NO  
     )      
      
      SELECT       
	  IMPORT_SERIAL_NO    ,
      POLICY_SEQUENCE_NO ,
	  END_SEQUENCE_NO ,  
	  REMUNERATION_SEQUENCE_NO  
                 
      FROM MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK)      
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0      
         
------------------------------------------------------------------------------------------    
    
    
   ------------------------------------            
   -- GET MAX RECOUNT COUNT      
   ------------------------------------         
    SELECT @MAX_RECORD_COUNT = COUNT(ID)       
    FROM   #TEMP_POLICY       
    
   ------------------------------------                
   -- FETCH ALL ACTIVE AGENCY   
   ------------------------------------   
     SELECT  CAST(AGENCY_CODE AS INT)AS AGENCY_CODE, AGENCY_ID
				 INTO #TempAgencyList
				 FROM MNT_AGENCY_LIST 
				 WHERE AGENCY_CODE NOT IN (SELECT REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)) AND
				       IS_ACTIVE='Y'
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
    ---------------------------------------------
      
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)      
  BEGIN    
    SET @ERROR_NO=0      
    
      SELECT     
        @IMPORT_SERIAL_NO				= IMPORT_SERIAL_NO , 
       	@LOOP_POLICY_SEQUANCE_NO		= POLICY_SEQUENCE_NO,     
        @LOOP_END_SEQUANCE_NO			= END_SEQUENCE_NO,   
		@LOOP_REMUNERATION_SERIAL_NO	= REMUNERATION_SEQUENCE_NO    
      FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER         
    
    
  SELECT      
    @COMMISION     = COMMISSION_PERCENTAGE,    
    @COMMISION_TYPE    = COMMISION_TYPE,    
    @AGENCY_CODE     = CAST(NAME AS INT),    
    @LEADER      = LEADER    
 FROM  MIG_IL_POLICY_REMUNERATION_DETAILS WITH (NOLOCK)    
 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID     
 AND   IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO     
 AND   IS_PROCESSED  = 'N'    
     
		 ----------------------------  VALIDATION OF ALREADY PROCESSED RECORD  -----------------------------------------  
		IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE   
                        AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO  AND   
                            ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO AND  
                            IMPORT_SEQUENTIAL=@LOOP_REMUNERATION_SERIAL_NO AND --CHANGE  
                            IS_ACTIVE='Y'   
                     )            
		 SET @ERROR_NO=66      -- This record already processed.    
      
       
        
     ------------------------------------  VALIDATION OF COMMISION TYPE    
     IF (@COMMISION_TYPE NOT IN (43,44,45)) --43- commition, 44- Enrolment Fee, 45- Prolabore    
     SET @ERROR_NO=96      -- INVALID COMMISION TYPE    
        
      ------------------------------------  VALIDATION OF BROKER ID    
     IF NOT EXISTS (SELECT 1 FROM #TempAgencyList WITH (NOLOCK) WHERE AGENCY_CODE=@AGENCY_CODE )    
     SET @ERROR_NO=89      -- INVALID BROKER  
        
         
        
      IF(@ERROR_NO>0)      
      BEGIN      
             
      UPDATE MIG_IL_POLICY_REMUNERATION_DETAILS 
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
        @ERROR_SOURCE_TYPE     = 'REMU'         
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
