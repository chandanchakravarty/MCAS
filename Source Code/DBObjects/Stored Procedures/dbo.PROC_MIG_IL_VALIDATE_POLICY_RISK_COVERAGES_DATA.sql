
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kr Kushwaha>
-- Create date: <17-10-2011>
-- Description:	Validate covarage data
-- =============================================
--  UPDATE HISTORY
-- =============================================
-- Author:		
-- update date: 
-- Description:	
-- =============================================
--drop Proc [PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA]    1 
CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_COVERAGES_DATA] 

--------------------------------- INPUT PARAMETER ---------------------------------------
@IMPORT_REQUEST_ID		INT
-------------------------------------------------
		
AS
BEGIN
	
------------------------------------------ DECLARATION PART --------------------------------------------------
DECLARE @ERROR_NUMBER    INT  
DECLARE @ERROR_SEVERITY  INT  
DECLARE @ERROR_STATE     INT  
DECLARE @ERROR_PROCEDURE VARCHAR(512)  
DECLARE @ERROR_LINE		 INT  
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)  



DECLARE @LOOP_IMPORT_SERIAL_NO INT    
DECLARE @COUNTER INT  =1  
DECLARE @MAX_RECORD_COUNT INT   
DECLARE @ERROR_NO INT=0  
DECLARE @IMPORT_SERIAL_NO INT 
      
DECLARE @COVERAGE INT
DECLARE @INITIAL_RATE DECIMAL(18,4)
DECLARE @FINAL_RATE   DECIMAL(18,4)


--===================================================   
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
DECLARE @IMPORT_FILE_TYPE INT = 14968 -- FOR COVERAGE FILE TYPE 
DECLARE @LOOP_POLICY_SEQUANCE_NO INT  
DECLARE @LOOP_END_SEQUANCE_NO INT                         
DECLARE @LOOP_RISK_SEQUANCE_NO INT 
DECLARE @LOOP_COVERAGE_SEQUANCE_NO INT
      
DECLARE @DEDUCTIBLE_TYPE INT                       
DECLARE @COVERAGE_ID INT                       
 
--=================================================== 

BEGIN TRY
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED ---------------------------------------------------------------
  
     CREATE TABLE #TEMP_COVERAGE
     (  
			ID INT IDENTITY(1,1),  
		    IMPORT_SERIAL_NO BIGINT,  
		    COVERAGE_ID INT,
			INITIAL_RATE DECIMAL(18,4),
			FINAL_RATE   DECIMAL(18,4),
			POLICY_SEQUENCE_NO INT NULL,
			POLICY_END_NO  INT NULL,
			RISK_SEQUANCE_NO INT,
			COVERAGE_SEQUANCE_NO INT 
			
     )  

	 INSERT INTO #TEMP_COVERAGE  
     (  
		    IMPORT_SERIAL_NO,
		    COVERAGE_ID,
			INITIAL_RATE, 
			FINAL_RATE,
			POLICY_SEQUENCE_NO,
			POLICY_END_NO,
			RISK_SEQUANCE_NO,
			COVERAGE_SEQUANCE_NO			 
		    
     )  
  
      SELECT 		
			IMPORT_SERIAL_NO,
	        COVERAGE,
	        INITIAL_RATE, 
			FINAL_RATE,
			POLICY_SEQUENTIAL,
			ENDORSEMENT_SEQUENTIAL,
			RISK_LOCATION_SEQUENTIAL,
			COVERAGE_SEQUENTIAL 
      FROM MIG_IL_POLICY_RISK_COVERAGES_DETAILS WITH(NOLOCK)  
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0  
     
	------------------------------------------------------------------------------------------


    ------------------------------------        
    -- GET MAX RECOUNT COUNT  
    ------------------------------------     
    SELECT @MAX_RECORD_COUNT = COUNT(ID)   
    FROM   #TEMP_COVERAGE   
    
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
					@IMPORT_SERIAL_NO			=  [IMPORT_SERIAL_NO],
					@COVERAGE_ID					=  COVERAGE_ID,
					@INITIAL_RATE				=  INITIAL_RATE, 
					@FINAL_RATE					=  FINAL_RATE, 
					@LOOP_POLICY_SEQUANCE_NO  	= POLICY_SEQUENCE_NO,
					@LOOP_END_SEQUANCE_NO     	= POLICY_END_NO,
					@LOOP_RISK_SEQUANCE_NO		= RISK_SEQUANCE_NO,
					@LOOP_COVERAGE_SEQUANCE_NO	= COVERAGE_SEQUANCE_NO
					
				FROM
					#TEMP_COVERAGE (NOLOCK) WHERE ID   = @COUNTER   
					
			  --===================================================     
          
				 IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
									AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO AND 
										ENDORSEMENT_SEQUENTIAL= @LOOP_END_SEQUANCE_NO AND
										IMPORT_SEQUENTIAL= @LOOP_RISK_SEQUANCE_NO AND  -- RISK
										IMPORT_SEQUENTIAL2= @LOOP_COVERAGE_SEQUANCE_NO AND  --cov
										IS_ACTIVE='Y'
								 )          
				  SET @ERROR_NO=66      -- This record already processed.
		    
			 --=================================================== 
		
		-- RECORD IF PROCESSGIN FOR ENDORSEMENT THEN SKIP DATA VALIDATION
				
		
			  ------------------------------------  VALIDATION OF INITIAL_RATE -------------------------------------------
			  IF(@INITIAL_RATE IS NOT NULL AND (@INITIAL_RATE>100.00) OR (@INITIAL_RATE<0))
				SET @ERROR_NO=129  --Initial rate should be in rage of 0 - 100.
			
			  ------------------------------------  VALIDATION OF FINAL_RATE -------------------------------------------
			  ELSE IF(@FINAL_RATE IS NOT NULL AND  (@FINAL_RATE>100.00) OR (@FINAL_RATE<0))
				SET @ERROR_NO=130 --Final rate should be in rage of 0 - 100.
			
			  ------------------------------------  VALIDATION OF COVERAGE ------------------------------------------------------------------
			  -- ELSE IF NOT EXISTS (SELECT 1 FROM MNT_COVERAGE WITH (NOLOCK) WHERE COV_CODE=CAST(@COVERAGE as NVARCHAR(20)) AND IS_ACTIVE='Y')
				--SET @ERROR_NO=215						-- WRONG COVERAGE 
				
			    ------------------------------------  VALIDATION OF COVERAGE_ID
			  ELSE IF @COVERAGE_ID IS NOT NULL AND @COVERAGE_ID >0 AND NOT EXISTS (SELECT 1 FROM MNT_COVERAGE WITH (NOLOCK) WHERE COV_ID=@COVERAGE_ID AND IS_ACTIVE='Y')
				SET @ERROR_NO=215	-- WRONG COVERAGE_ID 
			  
			  ELSE IF (@DEDUCTIBLE_TYPE IS NOT NULL AND @DEDUCTIBLE_TYPE>0 AND @DEDUCTIBLE_TYPE NOT IN (SELECT LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES WITH(NOLOCK) WHERE LOOKUP_UNIQUE_ID IN (14573,14574,14575,14576) AND IS_ACTIVE='Y'))
				SET @ERROR_NO=131	--Deductible Type does not exists	
	
		
		      IF(@ERROR_NO>0)  
			   BEGIN  
         
					 UPDATE MIG_IL_POLICY_RISK_COVERAGES_DETAILS  
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
							 @ERROR_SOURCE_TYPE     = 'PCOV'     -- DISCOUNT AT POLICY RISK LEVEL
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
 ,@ERROR_MESSAGE   = @ERROR_MESSAGE  
 ,@INITIAL_LOAD_FLAG    = 'Y'  
    
   
 END CATCH  

	
END
 
GO
