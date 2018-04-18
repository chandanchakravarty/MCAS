
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]    Script Date: 09/15/2011 15:26:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]
GO


GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA]    Script Date: 09/15/2011 15:26:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<ATUL KUMAR SINGH>
-- Create date: <2011-09-15>
-- Description:	<validate file data [file 'location']>
-- =============================================
--  UPDATE HISTORY
-- =============================================
-- Author:		
-- update date: 
-- Description:	
-- =============================================

CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_LOCATION_DATA] 

--------------------------------- INPUT PARAMETER
@IMPORT_REQUEST_ID		INT
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
      

DECLARE @COUNTRY_ID INT
DECLARE @STATE_ID INT


	  -- validation for country and state

BEGIN TRY
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED
-----------------------------------------------------------------------------------------
  
     CREATE TABLE #TEMP_POLICY  
     (  
			ID INT IDENTITY(1,1),  
		    IMPORT_SERIAL_NO BIGINT,  
		  
     )  



	 INSERT INTO #TEMP_POLICY  
     (  
		    IMPORT_SERIAL_NO
     )  
  
      SELECT 		
			IMPORT_SERIAL_NO
	            
      FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK)  
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0  
     
------------------------------------------------------------------------------------------


  ------------------------------------        
   -- GET MAX RECOUNT COUNT  
   ------------------------------------     
    SELECT @MAX_RECORD_COUNT = COUNT(ID)   
    FROM   #TEMP_POLICY   


	 WHILE(@COUNTER<=@MAX_RECORD_COUNT)  
	 BEGIN
			 SET @ERROR_NO=0  

			   SELECT 
					  @IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO 
			   FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER     


	 SELECT		
				@COUNTRY_ID					=	COUNTRY,
				@STATE_ID					=	[STATE]

	FROM		MIG_IL_POLICY_LOCATION_DETAILS WITH (NOLOCK)
	WHERE		IMPORT_REQUEST_ID	=	@IMPORT_REQUEST_ID 
	AND			IMPORT_SERIAL_NO	=	@IMPORT_SERIAL_NO 
	AND			IS_PROCESSED		=	'N'

			  ------------------------------------  VALIDATION OF COUNTRY

			  IF NOT EXISTS (SELECT 1 FROM MNT_COUNTRY_LIST (NOLOCK) WHERE COUNTRY_ID=@COUNTRY_ID AND IS_ACTIVE='Y')
			   SET @ERROR_NO=90						--WRONG COUNTRY ID
			  ELSE
			  BEGIN
				
			  ------------------------------------  VALIDATION OF STATE
			  IF NOT EXISTS (SELECT 1 FROM MNT_COUNTRY_STATE_LIST (NOLOCK) WHERE COUNTRY_ID=@COUNTRY_ID AND STATE_ID=@STATE_ID AND IS_ACTIVE='Y')
			  SET @ERROR_NO=91						-- WRONG STATE ID
			  
			  END	

			  

			  

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
							 @ERROR_SOURCE_TYPE     = 'LOC'     
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


