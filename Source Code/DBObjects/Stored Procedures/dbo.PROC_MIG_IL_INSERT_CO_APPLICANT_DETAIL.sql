/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]    Script Date: 12/02/2011 16:59:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]    Script Date: 12/02/2011 16:59:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 23 Aug 2011                                                          
Purpose               : CREATE NEW CO-APPLICANT
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]  22
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_CO_APPLICANT_DETAIL]   
      
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
     
     DECLARE @CUSTOMER_ID INT
     DECLARE @ERROR_NO INT=0
     DECLARE @APPLICANT_ID INT
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT 
     DECLARE @LOOP_COAPP_CODE NVARCHAR(50)   
     DECLARE @IMPORT_FILE_NAME NVARCHAR(50)   

     DECLARE @COUNTER INT  =1
     DECLARE @TempValue  VARCHAR(3)= '@T@'
   

     DECLARE @IMPORT_CUST_FILE_TYPE INT  = 14936 -- FOR CUSTOMER FILE TYPE
     DECLARE @IMPORT_COAPP_FILE_TYPE INT = 14937 -- FOR COAPPLICANT FILE TYPE
     DECLARE @LOOP_CUST_SEQUANCE_NO INT  
     DECLARE @LOOP_COAPP_SEQUANCE_NO INT  

	
     
     CREATE TABLE #TEMP_COAPP
     (
	   ID INT IDENTITY(1,1),
	   CUST_SEQUANCE_NO INT,
       IMPORT_SERIAL_NO BIGINT,
       COAPP_CODE NVARCHAR(50) NULL,
       COAPP_SEQUENTIAL	INT NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_COAPP
     (
      CUST_SEQUANCE_NO,
      IMPORT_SERIAL_NO,
      COAPP_CODE,
      COAPP_SEQUENTIAL
     )
     (
      SELECT CUSTOMER_SEQUENTIAL,
             IMPORT_SERIAL_NO,
             CUSTOMER_CODE,
             COAPPLICANT_SEQUENTIAL
      FROM MIG_IL_CO_APPLICANT_DETAILS
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT = COUNT(ID) 
    FROM   #TEMP_COAPP 

   ------------------------------------      
   -- GET FILE NAME
   ------------------------------------   
   IF(@MAX_RECORD_COUNT>0)
   BEGIN
   
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9) 
    FROM  MIG_IL_IMPORT_REQUEST_FILES
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND
          IMPORT_FILE_TYPE   = @IMPORT_COAPP_FILE_TYPE 
   
   END
    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
       SET @CUSTOMER_ID=0
       SET @ERROR_NO=0
       
       SELECT @LOOP_COAPP_CODE		 = COAPP_CODE,
              @IMPORT_SERIAL_NO		 = IMPORT_SERIAL_NO ,
              @LOOP_CUST_SEQUANCE_NO = CUST_SEQUANCE_NO,
              @LOOP_COAPP_SEQUANCE_NO= COAPP_SEQUENTIAL
       FROM   #TEMP_COAPP WHERE ID   = @COUNTER      
       
     
       -----------------------------------------     
	   -- CHECK WHETHER CUSTOMER EXISTS OR NOT
	   -----------------------------------------  
	   SELECT @CUSTOMER_ID = CUSTOMER_ID 
	   FROM   MIG_IL_IMPORT_SUMMARY
	   WHERE  CUSTOMER_SEQUENTIAL = @LOOP_CUST_SEQUANCE_NO AND
	          FILE_TYPE           = @IMPORT_CUST_FILE_TYPE AND
	          [FILE_NAME]         = @IMPORT_FILE_NAME      AND
			  IS_ACTIVE			  = 'Y'
	              
      IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)
        SET @ERROR_NO =50     -- CUSTOMER DOES NOT EXISTS  
      ELSE IF EXISTS(SELECT * FROM CLT_APPLICANT_LIST WHERE CONTACT_CODE=@LOOP_COAPP_CODE)
        SET @ERROR_NO =49     -- COAPPLICANT ALREADY EXISTS         
        
     
       IF(@ERROR_NO>0)
       BEGIN
       
		   UPDATE MIG_IL_CO_APPLICANT_DETAILS
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
				 @ERROR_SOURCE_TYPE     = 'CAPP'   
				 

       END 
       ELSE
       BEGIN
       
        
         SELECT @APPLICANT_ID=ISNULL(MAX(APPLICANT_ID),0)+1 FROM CLT_APPLICANT_LIST   WITH(NOLOCK)    
         ------------------------------------      
		 -- CREATE NEW APPLICANT
		 ------------------------------------  
        INSERT INTO CLT_APPLICANT_LIST                          
			(                          
			APPLICANT_ID,                    
			CUSTOMER_ID,
			TITLE,			
			FIRST_NAME,
			ADDRESS1,
			ADDRESS2,
			CITY,
			COUNTRY,
			[STATE],
			ZIP_CODE,
			PHONE,
			MOBILE,
			BUSINESS_PHONE,
			EXT,
			EMAIL,
			IS_ACTIVE,			    
			CREATED_DATETIME,  
			CO_APPL_MARITAL_STATUS,
			CO_APPL_DOB,--CREATION DATE,
			IS_PRIMARY_APPLICANT,
			CO_APPL_GENDER,  		   
			NUMBER,              
			COMPLIMENT,              
			DISTRICT,              
			NOTE ,            
			REGIONAL_IDENTIFICATION,            
			REG_ID_ISSUE,            
			ORIGINAL_ISSUE,            
			FAX ,            
			CPF_CNPJ,        
			APPLICANT_TYPE,
			ACCOUNT_TYPE,
            ACCOUNT_NUMBER,
            BANK_NAME,
            BANK_NUMBER,
            BANK_BRANCH  ,
            CONTACT_CODE  
			                          
			)                    
			(     
			SELECT                     
			@APPLICANT_ID,
			@CUSTOMER_ID,
			TITLE,
			CUSTOMER_NAME,--@FIRST_NAME,
			[ADDRESS],--,@ADDRESS2,
			COMPLIMENT,
			CITY,
			COUNTRY,
			[STATE],
			[ZIP CODE],                          
			[dbo].fun_FormatPhoneText(HOME_PHONE),
			[dbo].fun_FormatPhoneText(MOBILE),
			[dbo].fun_FormatPhoneText(BUSINESS_PHONE),
			EXT,
			EMAIL_ADDRESS,
			'Y',		
			GETDATE(),	
			CASE WHEN MARITAL_STATUS=5932 THEN 'D' --Divorced
			     WHEN MARITAL_STATUS=5933 THEN 'M'--Married
				 WHEN MARITAL_STATUS=5934 THEN 'P'--Separated
				 WHEN MARITAL_STATUS=5935 THEN 'S'--Single
				 WHEN MARITAL_STATUS=5936 THEN 'W'--Widowed
				END,
			CASE WHEN  [DATE OF BIRTH] = @TempValue AND [CREATION DATE]= @TempValue THEN NULL
			       WHEN CUST_TYPE = 11110 -- FOR PERONAL 
				    THEN CASE WHEN [DATE OF BIRTH]!= @TempValue THEN CAST (SUBSTRING([DATE OF BIRTH],1,4)+'/'+SUBSTRING([DATE OF BIRTH],5,2)+'/'+SUBSTRING([DATE OF BIRTH],7,2) AS DATE) ELSE NULL END-- DOB 
				    ELSE CASE WHEN [CREATION DATE]!= @TempValue THEN CAST (SUBSTRING([CREATION DATE],1,4)+'/'+SUBSTRING([CREATION DATE],5,2)+'/'+SUBSTRING([CREATION DATE],7,2) AS DATE) ELSE NULL END-- CREATION DATE 
				    END, --CREATION DATE	
			0,--IS_PRIMARY_APPLICANT
			CASE WHEN CUST_TYPE = 11110 THEN 
			      CASE WHEN GENDER=9813 THEN 'M' ELSE 'F' END
			      ELSE NULL
			END,           						  
			NUMBER,              
			COMPLIMENT,              
			DISTRICT,              
			REMARKS,            
			REG_IDENTIFICATION,  
            CASE WHEN REG_ID_ISSUE != @TempValue THEN CAST (SUBSTRING(REG_ID_ISSUE,1,4)+'/'+SUBSTRING(REG_ID_ISSUE,5,2)+'/'+SUBSTRING(REG_ID_ISSUE,7,2) AS DATE) ELSE NULL END , -- REG_ID_ISSUE ,,            
			CASE WHEN CUST_TYPE != 11110 -- FOR PERONAL , 
			     THEN ORIGINAL_ISSUE ELSE NULL 
			  END,
			[dbo].fun_FormatPhoneText(FAX),    			        
			CASE WHEN CUST_TYPE= 11110 -- FOR PERONAL 
                         THEN  SUBSTRING(CPF_CNPJ,1,3)+'.'+SUBSTRING(CPF_CNPJ,4,3)+'.'+SUBSTRING(CPF_CNPJ,7,3)+'-'+SUBSTRING(CPF_CNPJ,10,2) -- CPF FORMATTING
                         ELSE  SUBSTRING(CPF_CNPJ,1,2)+'.'+SUBSTRING(CPF_CNPJ,3,3)+'.'+SUBSTRING(CPF_CNPJ,6,3)+'/'+SUBSTRING(CPF_CNPJ,9,4)+'-'+SUBSTRING(CPF_CNPJ,13,2)---- CNPJ FORMATTING
                    END,        
			CUST_TYPE,
			ACCOUNT_TYPE,
            ACCOUNT_NUMBER,
            BANK_NAME,
            BANK_NUMBER,
            BANK_BRANCH  ,
            COAPPLICANT_CODE             
           FROM  MIG_IL_CO_APPLICANT_DETAILS
           WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
                 IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO  
                           
			)                  
			
      			
        ------------------------------------      
	    -- UPDATE IMPORT DETAILS
	    ------------------------------------  
		 			 
		 UPDATE MIG_IL_CO_APPLICANT_DETAILS
		 SET    CUSTOMER_ID  = @CUSTOMER_ID,
		        IS_PROCESSED = 'Y'
		 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
		        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
		
		EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]     
				 @IMPORT_REQUEST_ID 	 = @IMPORT_REQUEST_ID,
				 @IMPORT_SERIAL_NO  	 = @IMPORT_SERIAL_NO,	
				 @CUSTOMER_ID		  	 = @CUSTOMER_ID	,
				 @POLICY_ID		  		 = NULL,
				 @POLICY_VERSION_ID 	 = NULL,
				 @IS_ACTIVE		  		 = 'Y',
				 @IS_PROCESSED	  		 = 'Y',
				 @FILE_TYPE		  		 = @IMPORT_COAPP_FILE_TYPE,
				 @FILE_NAME              = @IMPORT_FILE_NAME,
				 @CUSTOMER_SEQUENTIAL    = @LOOP_CUST_SEQUANCE_NO,
				 @POLICY_SEQUENTIAL      = NULL, 
				 @ENDORSEMENT_SEQUENTIAL = NULL,
				 @IMPORT_SEQUENTIAL      = @LOOP_COAPP_SEQUANCE_NO,
				 @IMPORT_SEQUENTIAL2     = NULL,
				 @LOB_ID		  		 = NULL , 
				 @IMPORTED_RECORD_ID     = @APPLICANT_ID
				 	
       
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
