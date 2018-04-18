 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                              
PROC NAME             : DBO.[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]                                                         
CREATED BY            : SANTOSH KR GAUTAM                                           
DATE                  : 16 NOV 2011                                                            
PURPOSE               : COMMIT POLICY
MODIFIED BY           : PRADEEP KUSHWAHA
DATE                  : 30 NOV 2011                                                            
PURPOSE               : COMMIT POLICY ENDORESMENT  
REVISON HISTORY       :                                                              
USED IN               : INITIAL LOAD                 
------------------------------------------------------------                                                              
DATE     REVIEW BY          COMMENTS                                 
    
drop proc [PROC_MIG_IL_EXECUTE_POLICY_COMMIT]   1760                    
------   ------------       -------------------------*/               
CREATE PROCEDURE [dbo].[PROC_MIG_IL_EXECUTE_POLICY_COMMIT]           
          
--------------------------------- INPUT PARAMETER          
@IMPORT_REQUEST_ID  INT          
-------------------------------------------------          
            
AS          
BEGIN        
        
DECLARE @ERROR_NUMBER    INT            
DECLARE @ERROR_SEVERITY  INT            
DECLARE @ERROR_STATE     INT            
DECLARE @ERROR_PROCEDURE VARCHAR(512)            
DECLARE @ERROR_LINE    INT            
DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)            
          
    

          
BEGIN TRY          

DECLARE @CUSTOMER_ID INT  
DECLARE @LOB_ID INT 
DECLARE @POLICY_ID INT          
DECLARE @POLICY_VERSION_ID INT   
DECLARE @IMPORT_SERIAL_NO INT   
       
DECLARE @PROCESS_TYPE INT  
DECLARE @POLICY_NUMBER NVARCHAR(21)          
DECLARE @ENDORSEMENT_NUMBER INT          
DECLARE @MAX_RECORD_COUNT INT 
DECLARE @COUNTER INT =1
DECLARE @IS_VALID     CHAR(1) 
DECLARE @ERROR_TYPES    NVARCHAR(MAX) 
DECLARE @COMMIT_DATE    DATETIME
DECLARE @TEMP_VALE VARCHAR(4) ='@T@' 
DECLARE	@PROCESS_ID INT = NULL
DECLARE	@CREATED_BY INT = NULL
DECLARE	@CREATED_DATE DATETIME = NULL

DECLARE	@ENDORSEMENT_TYPE INT = 0
DECLARE	@CO_APPLICANT_ID INT
DECLARE	@ENDORSEMENT_NO INT = 0
DECLARE	@POLICY_STATUS NVARCHAR(10)
DECLARE	@IL_PROCESS_TYPE INT = NULL
DECLARE @PREVIOUS_POLICY_STATUS NVARCHAR(20)            
DECLARE @CURRENT_POLICY_STATUS NVARCHAR(20)  
DECLARE @ROW_ID INT
DECLARE @REQUESTED_BY INT
DECLARE @EFFETIVE_DATE DATETIME
DECLARE @EXPIRY_DATE DATETIME
DECLARE @CO_APPLICANT_CODE NVARCHAR(20)	
 
	--============================================================
    --  ~~~~~~~~~~~~~~ PROCESS TYPE ~~~~~~~~~~~~~~~~
    --============================================================
    
    --------------------------------------------------------------
	--  INTIAL LOAD DOMAIN    ==  SYSTEM ID
	--------------------------------------------------------------
	--1	NEW NBS				  == 25	NEW BUSINESS COMMIT 
	--2	CANCEL POLICY         == 2  POLICY CANCELLATION IN PROGRESS
	--3	ENDORSEMENT           == 3	Endorsement IN PROGRESS
	--4	RENEWAL POLICY        == 5	Renewal
	--5	UNDO ENDORSEMENT      == 35	UnDo Endorsement
	--6	REWRITE POLICY        == 31	Rewrite
	

	SELECT @CREATED_BY   = dbo.fun_GetDefaultUserID()
	SELECT @CREATED_DATE = GETDATE()
	SELECT @REQUESTED_BY = @CREATED_BY	
		   
    CREATE TABLE #TEMP_POLICY          
     (          
		ID INT IDENTITY(1,1),          
		CUST_ID INT,          
		POLICY_ID INT,         
		POLICY_VERSION_ID INT,
		LOB_ID  INT,
		IMPORT_SERIAL_NO INT,
		PROCESS_TYPE INT
		
     )          
        
        
        
  INSERT INTO #TEMP_POLICY          
     (          
		CUST_ID ,          
		POLICY_ID ,         
		POLICY_VERSION_ID ,
		LOB_ID,
		IMPORT_SERIAL_NO,		
		PROCESS_TYPE
              
     )          
     (          
      SELECT CUSTOMER_ID,        
			POLICY_ID,        
			POLICY_VERSION_ID,
			LOB_ID,
			IMPORT_SERIAL_NO,
			PROCESS_TYPE
	  FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK)          
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0 AND HAS_COMMITED_ERROR=0      
     )          

   ------------------------------------                  
   -- GET MAX RECOUNT COUNT            
   ------------------------------------               
    SELECT @MAX_RECORD_COUNT = COUNT(ID)             
    FROM   #TEMP_POLICY             
          
	WHILE(@COUNTER<=@MAX_RECORD_COUNT)            
	BEGIN   
	          
		SET @POLICY_ID=NULL        
		SET @CUSTOMER_ID=NULL          
		SET @POLICY_VERSION_ID=NULL  
		SET @LOB_ID=0       
	    SET @IS_VALID=''	
		SET	@ERROR_TYPES=''
		SET @CO_APPLICANT_CODE=''
		SET @ENDORSEMENT_NO=0
		
	     SELECT @POLICY_ID          = POLICY_ID,
	            @CUSTOMER_ID        = CUST_ID,
	            @POLICY_VERSION_ID  = POLICY_VERSION_ID,
	            @LOB_ID			    = LOB_ID,
	            @IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO,
	            @IL_PROCESS_TYPE    = PROCESS_TYPE
	     FROM #TEMP_POLICY WHERE ID = @COUNTER
	     
	     
	     SELECT * FROM  #TEMP_POLICY
	    
	  --  select @IL_PROCESS_TYPE as '@PROCESS_TYPE' 
	   ------------------------------------------
	   -- FOR NEW BUSINESS COMMIT
	   ------------------------------------------ 
       IF(@IL_PROCESS_TYPE=1)
         BEGIN
         
          SET @PROCESS_ID=25
          SET @CURRENT_POLICY_STATUS  = 'Suspended'
          SET @PREVIOUS_POLICY_STATUS ='NORMAL'
          
            SELECT @EFFETIVE_DATE = CONVERT(DATETIME,(LEFT(EFFECTIVE_DATE,4)+'-'+SUBSTRING(EFFECTIVE_DATE,5,2)+'-'+RIGHT(EFFECTIVE_DATE,2))), 
       			   @EXPIRY_DATE   = CONVERT(DATETIME,(LEFT(EXPIRATION_DATE,4)+'-'+SUBSTRING(EXPIRATION_DATE,5,2)+'-'+RIGHT(EXPIRATION_DATE,2)))  
	        FROM   MIG_IL_POLICY_DETAILS 
			WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
				   IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			 
          ------------------------------------------------------
          -- UPDATE POLICY DETAIL 
          ------------------------------------------------------
          UPDATE POL_CUSTOMER_POLICY_LIST
          SET   POLICY_STATUS	    = 'NORMAL'	
		  WHERE CUSTOMER_ID			= @CUSTOMER_ID AND 
				POLICY_ID			= @POLICY_ID AND
				@POLICY_VERSION_ID	= @POLICY_VERSION_ID
			
				
			 ------------------------------------------------------------
			 -- INSERT RECORD IN POL POLICY PROCESS
			 ------------------------------------------------------------
			 EXEC  dbo.Proc_InsertPOL_POLICY_PROCESS    
			 @CUSTOMER_ID					= @CUSTOMER_ID ,               
			 @POLICY_ID						= @POLICY_ID,                       
			 @POLICY_VERSION_ID				= @POLICY_VERSION_ID,             
			 @ROW_ID						= @ROW_ID  OUTPUT,             
			 @PROCESS_ID			        = @PROCESS_ID,    
			 @PROCESS_TYPE					= '',   -- relook      
			 @NEW_CUSTOMER_ID				= @CUSTOMER_ID,             
			 @NEW_POLICY_ID					= @POLICY_ID,                                
			 @NEW_POLICY_VERSION_ID			= @POLICY_VERSION_ID,                                
			 @POLICY_PREVIOUS_STATUS		= @PREVIOUS_POLICY_STATUS,                                 
			 @POLICY_CURRENT_STATUS		    = @CURRENT_POLICY_STATUS,                                
			 @PROCESS_STATUS				='',     --relook                            
			 @CREATED_BY					= @CREATED_BY,                            
			 @CREATED_DATETIME				= @CREATED_DATE,                           
			 @COMPLETED_BY					= @CREATED_BY,                           
			 @COMPLETED_DATETIME			= @CREATED_DATE,                           
			 @COMMENTS						= '',                           
			 @PRINT_COMMENTS				='',                           
			 @REQUESTED_BY					= @REQUESTED_BY,                           
			 @EFFECTIVE_DATETIME			= @EFFETIVE_DATE,                           
			 @EXPIRY_DATE					= @EXPIRY_DATE,                           
			 @CANCELLATION_OPTION			= null,                           
			 @CANCELLATION_TYPE				= null,                   
			 @REASON						= null, 
			 @OTHER_REASON					= null,                           
			 @RETURN_PREMIUM				= null,                              
			 @PAST_DUE_PREMIUM				= null,                              
			 @ENDORSEMENT_NO				= null,                              
			 @PROPERTY_INSPECTION_CREDIT    = null,                              
			 @POLICY_TERMS					= null ,-- relook                            
			 @NEW_POLICY_TERM_EFFECTIVE_DATE  = @EFFETIVE_DATE,                            
			 @NEW_POLICY_TERM_EXPIRATION_DATE = @EXPIRY_DATE,                          
			 @DIARY_LIST_ID					= null,               
			 @PRINTING_OPTIONS				= null,                     
			 @INSURED						= null,                 
			 @SEND_INSURED_COPY_TO			= null,                    
			 @AUTO_ID_CARD	                = null,  
			 @NO_COPIES						= null,         
			 @STD_LETTER_REQD				= null,                  
			 @CUSTOM_LETTER_REQD			= null,                
			 @SEND_ALL						= null,           
			 @ADD_INT						= null,     
			 @ADD_INT_ID					= null,            
			 @AGENCY_PRINT					= null,     
			 @OTHER_RES_DATE				= null,        
			 @OTHER_RES_DATE_CD				= null,    
			 @DUE_DATE						= null,  
			 @INCLUDE_REASON_DESC			= null,    
			 @ENDORSEMENT_TYPE				= null,  
			 @ENDORSEMENT_OPTION			= null,  
			 @SOURCE_VERSION_ID				= null,  
			 @ENDORSEMENT_RE_ISSUE			= null,  
			 @CO_APPLICANT_ID				= null
			 			
         
         
        -- select * from POL_POLICY_PROCESS  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID
         
         END  
         --------------------------------------------------
		 -- FOR POLICY CANCELLATION OR ENDORSEMENT COMMIT
		 -------------------------------------------------- 
         ELSE
         BEGIN
           
           --- GET MAXIUM POLICY VERSION ID TO VERIF RULES
           SELECT @POLICY_VERSION_ID=MAX(POLICY_VERSION_ID) 
           FROM POL_CUSTOMER_POLICY_LIST (NOLOCK)
           WHERE CUSTOMER_ID = @CUSTOMER_ID AND
                 POLICY_ID   = @POLICY_ID
         
         
         END -- END OF IF(@PROCESS_TYPE=1)
         
         
         	

	     ----------------------------------------
	     -- VERIFY POLICY DETAILS
	     ---------------------------------------
	     EXECUTE PROC_MIG_IL_VERIFY_POLICY_RULES
				@CUSTOMER_ID	   = @CUSTOMER_ID,
				@POLICY_ID		   = @POLICY_ID,
				@POLICY_VERSION_ID = @POLICY_VERSION_ID,
				@LOB_ID			   = @LOB_ID,
				@IS_VALID		   = @IS_VALID OUT,
				@ERROR_TYPES	   = @ERROR_TYPES OUT
		
		
		 ----------------------------------------
	     -- IF RULES VERIFIED SUCCESSFULLY
	     ---------------------------------------
		IF(@IS_VALID='Y')
		  BEGIN
		  
				SELECT				
				@COMMIT_DATE         = CASE WHEN COMMIT_DATE IS NULL OR COMMIT_DATE =@TEMP_VALE OR COMMIT_DATE='0' THEN NULL 
									   ELSE CAST (SUBSTRING(COMMIT_DATE,1,4)+'/'+SUBSTRING(COMMIT_DATE,5,2)+'/'+SUBSTRING(COMMIT_DATE,7,2) AS DATE) END,
				@ENDORSEMENT_NO		= CAST(ENDORSEMENT_NUMBER AS INT), --WE ARE CONSIDARING ENDORESMENT NO FROM INITIAL LOAD GIVEN FILE 		ADDED BY PRADEEP			   
				@CO_APPLICANT_CODE  = COAPPLICANT_CODE,
				@POLICY_NUMBER		= POLICY_NUMBER
		        FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK)
			    WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
		              IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
		              
		        --GET THE APPLICANT ID FOR MASTER POLICY OR NORMAL POLICY -- ADDED BY PRADEEP 
		        IF EXISTS(SELECT 1 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND TRANSACTION_TYPE=14560) --MASTER POLICY
		        BEGIN
					SELECT @CO_APPLICANT_ID=PAL.APPLICANT_ID
					FROM CLT_APPLICANT_LIST CAL WITH(NOLOCK) INNER JOIN
					POL_APPLICANT_LIST PAL (NOLOCK) ON 
							CAL.CUSTOMER_ID		 =	PAL.CUSTOMER_ID AND
							CAL.APPLICANT_ID	 =	PAL.APPLICANT_ID
					WHERE	PAL.CUSTOMER_ID		 =  @CUSTOMER_ID AND 
							PAL.POLICY_ID		 =  @POLICY_ID AND 
							PAL.POLICY_VERSION_ID=  @POLICY_VERSION_ID AND
							CAL.CONTACT_CODE	 =	@CO_APPLICANT_CODE
		        END
		        ELSE
		        BEGIN
					SELECT @CO_APPLICANT_ID=APPLICANT_ID
					FROM POL_APPLICANT_LIST (NOLOCK) 
					WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND IS_PRIMARY_APPLICANT = 1
		        END
		     ----------------------------   
		     --- FOR POLICY COMMIT   
		     ----------------------------
		     IF(@IL_PROCESS_TYPE=1) 
		        SET @PROCESS_ID=25 -- COMMIT NEW BUSSINES
		     ----------------------------
		     --  FOR POLICY CANCELLATION
		     ----------------------------
		     ELSE IF(@IL_PROCESS_TYPE=2)    
		        SET @PROCESS_ID=12 	 -- COMMIT POLICY CANCELLATION
		     ----------------------------   
		     --  FOR ENDORSEMENT COMMIT 
		     ----------------------------  	 
			 ELSE IF(@IL_PROCESS_TYPE=3)    
		        SET @PROCESS_ID=14 
		   
		    --=================================================
		    -- COMMIT POLICY/ENDORSEMENT/POLICY CANCELLATION
		    --=================================================
		    EXEC [PROC_MIG_IL_POLICY_COMMITT]
				 @CUSTOMER_ID			= @CUSTOMER_ID,            
				 @POLICY_ID				= @POLICY_ID,
				 @POLICY_VERSION_ID		= @POLICY_VERSION_ID,
				 @COMMIT_DATE			= @COMMIT_DATE,				 
				 @PROCESS_ID			= @PROCESS_ID,
				 @CREATED_BY			= @CREATED_BY,
				 @CREATED_DATE		    = @CREATED_DATE, 
				 @ENDORSEMENT_TYPE      = 0,
				 @CO_APPLICANT_ID		= @CO_APPLICANT_ID,
				 @ENDORSEMENT_NO		= @ENDORSEMENT_NO
				 
			--====================================================
			-- UPDATE POLICY NUMBER - FROM INITIAL LOAD GIVEN POLICY FILE -- ADDED BY PRADEEP
			--====================================================
			IF(@PROCESS_ID=25) -- COMMIT NEW BUSSINES
			BEGIN
				UPDATE POL_CUSTOMER_POLICY_LIST 
				SET 
					POLICY_NUMBER	=	@POLICY_NUMBER
				WHERE CUSTOMER_ID	=	@CUSTOMER_ID	AND 
					  POLICY_ID		=	@POLICY_ID		AND
				POLICY_VERSION_ID	=	@POLICY_VERSION_ID
				 	
			END
			
		  END
		ELSE
		  BEGIN
		  
		  -----------------------------------------------------------        
		  -- INSERT ERROR DETAILS  
		  -----------------------------------------------------------   
			 UPDATE MIG_IL_POLICY_DETAILS   -- CHANGE
			 SET    HAS_COMMITED_ERROR=1        
			 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND   
					IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO        
		        
			EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                   
			 @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,        
			 @IMPORT_SERIAL_NO      = @IMPORT_SERIAL_NO  ,            
			 @ERROR_SOURCE_FILE     = ''     ,        
			 @ERROR_SOURCE_COLUMN   = ''     ,        
			 @ERROR_SOURCE_COLUMN_VALUE= '' ,        
			 @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,          
			 @ERROR_TYPES           = @ERROR_TYPES,     
			 @ACTUAL_RECORD_DATA    = '' ,        
			 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR        
			 @ERROR_SOURCE_TYPE     = 'POLC'     -- CHANGE
			 
		  END  -- END IF(@IS_VALID='Y')
		 
				
				
	     
	     
	    SET @COUNTER+=1      
	     
	END  -- END OF WHILE       
   
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