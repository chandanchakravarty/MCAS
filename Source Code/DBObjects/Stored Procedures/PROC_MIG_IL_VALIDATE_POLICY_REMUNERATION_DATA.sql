
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]    Script Date: 09/15/2011 16:40:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]
GO


GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA]    Script Date: 09/15/2011 16:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<ATUL KUMAR SINGH>
-- Create date: <2011-09-15>
-- Description:	<validate file data [file 'remuneration']>
-- =============================================
--  UPDATE HISTORY
-- =============================================
-- Author:		
-- update date: 
-- Description:	
-- =============================================

CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_REMUNERATION_DATA] 

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


  


DECLARE @LOOP_IMPORT_SERIAL_NO INT    
DECLARE @COUNTER INT  =1  
DECLARE @MAX_RECORD_COUNT INT   
DECLARE @ERROR_NO INT=0  
DECLARE @IMPORT_SERIAL_NO INT 
      
DECLARE @COMMISION_TYPE INT 
DECLARE @BROKER_ID INT 
DECLARE @COMMISION INT 
DECLARE @LEADER INT 




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
	            
      FROM MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK)  
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
				@COMMISION					=	COMMISSION_PERCENTAGE,
				@COMMISION_TYPE				=	COMMISION_TYPE,
				@BROKER_ID					=	BROKER_AGENCY,
				@LEADER						=	LEADER
	FROM		MIG_IL_POLICY_REMUNERATION_DETAILS WITH (NOLOCK)
	WHERE		IMPORT_REQUEST_ID	=	@IMPORT_REQUEST_ID 
	AND			IMPORT_SERIAL_NO	=	@IMPORT_SERIAL_NO 
	AND			IS_PROCESSED		=	'N'

			  ------------------------------------  VALIDATION OF COMMISSION PERCENTAGE

			  IF (@COMMISION<>'' OR @COMMISION IS NOT NULL)
			   SET @ERROR_NO=100						--WRONG COMMITION
			  ELSE
			  BEGIN
				
			  ------------------------------------  VALIDATION OF COMMISION TYPE
			  IF (@COMMISION_TYPE in (43,44,45)) --43- commition, 44- Enrolment Fee, 45- Prolabore
				SET @ERROR_NO=101						-- WRONG COMMISION TYPE
				
			   ------------------------------------  VALIDATION OF BROKER ID
			  IF EXISTS (SELECT 1 FROM MNT_AGENCY_LIST WITH (NOLOCK) WHERE AGENCY_CODE=@BROKER_ID AND IS_ACTIVE=1)
				SET @ERROR_NO=102						-- WRONG BROKER CODE
			  
			   
			  
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
							 @ERROR_SOURCE_TYPE     = 'REC'     
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


