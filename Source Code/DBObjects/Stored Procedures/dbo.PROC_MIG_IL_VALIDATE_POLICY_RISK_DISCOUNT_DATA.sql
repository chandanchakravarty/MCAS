/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]    Script Date: 12/02/2011 17:54:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]    Script Date: 12/02/2011 17:54:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                
Proc Name             : Dbo.[[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]]                                                            
Created by            : Santosh kr Gautam
Date                  : 14 oct 2011                                                    
Purpose               : Validate Risk Disount
Revison History       :                                                                
Used In               : INITIAL LOAD                   
------------------------------------------------------------                                                                
Date     Review By          Comments                                   
                          
drop Proc [PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA]    1                                                   
------   ------------       -------------------------*/   

CREATE PROCEDURE [dbo].[PROC_MIG_IL_VALIDATE_POLICY_RISK_DISCOUNT_DATA] 

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
      
DECLARE @DISCOUNT_TYPE INT
DECLARE @DISCOUNT_PERCENT DECIMAL(18,2)

--===================================================   
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)           
DECLARE @IMPORT_FILE_TYPE INT = 14968 -- FOR POLICY FILE TYPE 
DECLARE @LOOP_POLICY_SEQUANCE_NO INT  
DECLARE @LOOP_END_SEQUANCE_NO INT   
DECLARE @LOOP_RISK_SEQUANCE_NO INT        
DECLARE @LOOP_DISCOUNT_SEQUANCE_NO INT        
                      
                       
--=================================================== 


BEGIN TRY
-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED ---------------------------------------------------------------
  
     CREATE TABLE #TEMP_POLICY  
     (  
			ID INT IDENTITY(1,1),  
		    IMPORT_SERIAL_NO BIGINT,  
		    [TYPE] INT,
			[PERCENT] [DECIMAL] (18,2),
			POLICY_SEQUENCE_NO INT NULL,
			POLICY_END_NO  INT NULL,
			DISCOUNT_SEQUENCE INT NULL,
			RISK_SEQUENCE INT NULL,
     )  



	 INSERT INTO #TEMP_POLICY  
     (  
		    IMPORT_SERIAL_NO,
		    [TYPE],
		    [PERCENT],
		    POLICY_SEQUENCE_NO,
			POLICY_END_NO,
			DISCOUNT_SEQUENCE,
			RISK_SEQUENCE
     )  
    (
      SELECT 		
			IMPORT_SERIAL_NO,
	        [TYPE],
		    [PERCENT],
		    POLICY_SEQUENTIAL,
			ENDORSEMENT_SEQUENTIAL  ,
			DISCOUNT_SURCHARGE_RISK_SEQUENTIAL,
			RISK_LOCATION_SEQUENTIAL  
      FROM MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS WITH(NOLOCK)  
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0  
     )
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
					@IMPORT_SERIAL_NO		=  [IMPORT_SERIAL_NO],
					@DISCOUNT_TYPE			=  [TYPE],
					@DISCOUNT_PERCENT		=  [PERCENT],
					@LOOP_POLICY_SEQUANCE_NO  = POLICY_SEQUENCE_NO,
					@LOOP_END_SEQUANCE_NO     = POLICY_END_NO,
					@LOOP_RISK_SEQUANCE_NO    = RISK_SEQUENCE,
					@LOOP_DISCOUNT_SEQUANCE_NO= DISCOUNT_SEQUENCE 
				FROM
					#TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER    


			  --===================================================     
          
				 IF EXISTS (SELECT 1 FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) WHERE [FILE_NAME]=@IMPORT_FILE_NAME AND FILE_TYPE=@IMPORT_FILE_TYPE 
									AND POLICY_SEQUENTIAL= @LOOP_POLICY_SEQUANCE_NO AND 
										ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO AND
										IMPORT_SEQUENTIAL      = @LOOP_RISK_SEQUANCE_NO AND
										IMPORT_SEQUENTIAL2     = @LOOP_DISCOUNT_SEQUANCE_NO AND
										IS_ACTIVE='Y'
								 )          
				  SET @ERROR_NO=66      -- This record already processed.
		    
			 --=================================================== 				
				
			   ------------------------------------  VALIDATION OF DISCOUNT_PERCENT
			  IF((@DISCOUNT_PERCENT>100.00) OR (@DISCOUNT_PERCENT<0))
				SET @ERROR_NO=106						-- INVALID DISCOUNT PERCENT(>100)	
				
			  ------------------------------------  VALIDATION OF DISCOUNT_TYPE
			  ELSE IF NOT EXISTS (SELECT 1 FROM MNT_DISCOUNT_SURCHARGE WITH (NOLOCK) WHERE DISCOUNT_ID=@DISCOUNT_TYPE and [LEVEL]=14705 AND IS_ACTIVE='Y')
				SET @ERROR_NO=104						-- WRONG RISK DISCOUNT TYPE 
			   

			   IF(@ERROR_NO>0)  
			   BEGIN  
         
					 UPDATE MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS  
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
							 @ERROR_SOURCE_TYPE     = 'RDSC'     -- DISCOUNT AT POLICY RISK LEVEL
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
