
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_POLICY_RISK_DISCOUNT_VALIDATE_DATA]    Script Date: 09/16/2011 12:13:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]
GO


GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]    Script Date: 09/16/2011 12:13:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<ATUL KUMAR SINGH>
-- Create date: <2011-09-16>
-- Description:	<validate file data [file 'DISCOUNT']>
-- =============================================
--  UPDATE HISTORY
-- =============================================
-- Author:		
-- update date: 
-- Description:	
-- =============================================

CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA] 

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
      
DECLARE @DISCOUNT_TYPE INT




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
	            
      FROM MIG_IL_POLICY_DISCOUNTS_DETAILS WITH(NOLOCK)  
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
				@DISCOUNT_TYPE			=[TYPE]
				
				
				
	FROM		MIG_IL_POLICY_DISCOUNTS_DETAILS WITH (NOLOCK)
	WHERE		IMPORT_REQUEST_ID	=	@IMPORT_REQUEST_ID 
	AND			IMPORT_SERIAL_NO	=	@IMPORT_SERIAL_NO 
	AND			IS_PROCESSED		=	'N'

		
			  ------------------------------------  VALIDATION OF DISCOUNT_TYPE
			  IF NOT EXISTS (SELECT 1 FROM MNT_DISCOUNT_SURCHARGE WITH (NOLOCK) WHERE DISCOUNT_ID=@DISCOUNT_TYPE AND IS_ACTIVE='Y')
				SET @ERROR_NO=107						-- WRONG DISCOUNT_TYPE 
			   
			  
			

			  

			  

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
							 @ERROR_SOURCE_TYPE     = 'DISR'     -- DISCOUNT AT POLICY RISK LEVEL
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


