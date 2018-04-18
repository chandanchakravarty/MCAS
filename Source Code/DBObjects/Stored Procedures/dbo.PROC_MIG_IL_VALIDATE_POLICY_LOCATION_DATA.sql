/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]
GO
 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]                                                          
Created by            : Santosh Kumar Gautam                                                             
Date                  : 15 SEPT 2011                                                        
Purpose               : VALIDATE POLICY LOCATION  
Revison History       :         
Modified by           : Pradeep  Kushwaha
Date                  : OCT 18 2011
Purpose               :  VALIDATE POLICY LOCATION                                                
Used In               : INITIAL LOAD                 
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA  712  
------   ------------       -------------------------*/     
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]   
  
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
DECLARE @LOOP_LOCATION_SERIAL_NO INT  
 
    
DECLARE @COUNTER INT  =1    
DECLARE @MAX_RECORD_COUNT INT     
DECLARE @ERROR_NO INT=0    
DECLARE @IMPORT_SERIAL_NO INT   
        
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)         
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE      
DECLARE @IMPORT_LOCATION_FILE_TYPE INT = 14960 -- FOR POLICY LOCATION   

DECLARE @STATE_ID INT  
DECLARE @ACTIVITY_TYPE INT 
DECLARE @OCCUPIED_AS INT 
DECLARE @CONSTRUCTION INT   
  
   -- validation for country and state  
  
BEGIN TRY  
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED  
-----------------------------------------------------------------------------------------  
    
     CREATE TABLE #TEMP_POLICY    
     (    
      ID INT IDENTITY(1,1),    
      IMPORT_SERIAL_NO INT NULL,  
      LOCATION_STATE INT NULL,  
      POLICY_SEQUENCE_NO INT,  
	  END_SEQUENCE_NO INT,  
	  LOCATION_SEQUENCE_NO  INT ,
	  ACTIVITY_TYPES INT, 
	  OCCUPIED_AS INT,
	  CONSTRUCTION INT
     )    
  
  
  
  INSERT INTO #TEMP_POLICY    
     (    
      IMPORT_SERIAL_NO ,
      LOCATION_STATE, 
      POLICY_SEQUENCE_NO,
	  END_SEQUENCE_NO,
	  LOCATION_SEQUENCE_NO,
	  ACTIVITY_TYPES,
	  OCCUPIED_AS,
	  CONSTRUCTION
     )    
    (
      SELECT     
      IMPORT_SERIAL_NO  ,
      [STATE]      ,
      POLICY_SEQUENCE_NO,
	  END_SEQUENCE_NO,
	  LOCATION_SEQUENCE_NO,
	  ACTIVITY_TYPES,
	  OCCUPIED_AS,
	  CONSTRUCTION
      FROM MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK)    
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0    
     )  
------------------------------------------------------------------------------------------  
  
  
  ------------------------------------          
   -- GET MAX RECOUNT COUNT    
   ------------------------------------       
    SELECT @MAX_RECORD_COUNT = COUNT(ID)     
    FROM   #TEMP_POLICY     
   ------------------------------------                
   -- GET IMPORT FILE NAME          
   ------------------------------------             
    IF(@MAX_RECORD_COUNT>0)          
    BEGIN          
              
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)          
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)          
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND           
            IMPORT_FILE_TYPE  = @IMPORT_POLICY_FILE_TYPE          
       
    END               
    ---------------------------------------------
 
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)    
  BEGIN  
    SET @ERROR_NO=0    
	SET @ACTIVITY_TYPE=0 
	SET @OCCUPIED_AS=0 
	SET @CONSTRUCTION =0
      SELECT   
        @IMPORT_SERIAL_NO			= IMPORT_SERIAL_NO  ,
        @STATE_ID					= LOCATION_STATE,
       	@LOOP_POLICY_SEQUANCE_NO	= POLICY_SEQUENCE_NO,     
        @LOOP_END_SEQUANCE_NO       = END_SEQUENCE_NO,   
		@LOOP_LOCATION_SERIAL_NO    = LOCATION_SEQUENCE_NO, 
		@ACTIVITY_TYPE				=ACTIVITY_TYPES,
		@OCCUPIED_AS				=OCCUPIED_AS,
		@CONSTRUCTION				=CONSTRUCTION     
      FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER       
  
      ------------------------------------  VALIDATION OF ALREADY PROCESSED RECORD  -----------------------------------------  
		IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_POLICY_FILE_TYPE   
                        AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO  AND   
                            ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO AND  
                            IMPORT_SEQUENTIAL=@LOOP_LOCATION_SERIAL_NO AND --CHANGE  
                            IS_ACTIVE='Y'   
                     )            
		 SET @ERROR_NO=66      -- This record already processed.    
       
     ------------------------------------  VALIDATION OF STATE  
		IF NOT EXISTS (SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=5 AND STATE_ID=@STATE_ID )  
			 SET @ERROR_NO=92      -- WRONG STATE ID  
		ELSE IF(@OCCUPIED_AS IS NOT NULL AND @OCCUPIED_AS >0 )
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM MNT_OCCUPIED_MASTER NOLOCK WHERE OCCUPIED_ID=@OCCUPIED_AS)	
			SET @ERROR_NO =114 --  Occupied as does not exists.
		END	
		ELSE IF(@ACTIVITY_TYPE IS NOT NULL AND @ACTIVITY_TYPE >0 )
		BEGIN
		 IF NOT EXISTS(SELECT 1 FROM MNT_ACTIVITY_MASTER NOLOCK WHERE ACTIVITY_ID=@ACTIVITY_TYPE)
			SET @ERROR_NO =112 --  Activity type  does not exists.
		END	
		ELSE IF(@CONSTRUCTION IS NOT NULL AND @CONSTRUCTION >0)
		BEGIN
			IF NOT EXISTS(SELECT 1 FROM MNT_LOOKUP_VALUES NOLOCK WHERE LOOKUP_UNIQUE_ID =@CONSTRUCTION )
				SET @ERROR_NO =113 --  Construction does not exists. 
		END	
   
 
      IF(@ERROR_NO>0)    
      BEGIN    
           
      UPDATE MIG_IL_POLICY_LOCATION_DETAILS    
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
        @ERROR_SOURCE_TYPE     = 'LOCA'       
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
