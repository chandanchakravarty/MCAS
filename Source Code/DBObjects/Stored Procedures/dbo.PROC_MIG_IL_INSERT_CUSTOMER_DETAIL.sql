IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_CUSTOMER_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_CUSTOMER_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_CUSTOMER_DETAIL]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 18 Aug 2011                                                          
Purpose               : CREATE NEW CUSTOMER
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_INSERT_CUSTOMER_DETAIL]    1                                               
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].PROC_MIG_IL_INSERT_CUSTOMER_DETAIL   
      
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
     DECLARE @APPLICANT_ID INT
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT 
     DECLARE @LOOP_CUST_CODE NVARCHAR(50)   
     DECLARE @LOOP_CUST_SEQL INT 
     DECLARE @IMPORT_FILE_NAME NVARCHAR(50)   
     DECLARE @IMPORT_CUST_FILE_TYPE INT = 14936 -- FOR CUSTOMER FILE TYPE
     DECLARE @COUNTER INT  =1
     DECLARE @BANK_NAME NVARCHAR(200)  
     DECLARE @BANK_NUMBER NVARCHAR(100)  
     DECLARE @BANK_BRANCH NVARCHAR(200)  
     DECLARE @ACCOUNT_NUMBER NVARCHAR(100)  
     DECLARE @ACCOUNT_TYPE INT
	 DECLARE @TempValue  VARCHAR(3)= '@T@'

     
     
     CREATE TABLE #TEMP_CUST
     (
	   ID INT IDENTITY(1,1),
       IMPORT_SERIAL_NO BIGINT,
       CUST_CODE NVARCHAR(50) NULL,
       CUSTOMER_SEQUENTIAL	INT NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_CUST
     (
      IMPORT_SERIAL_NO,
      CUST_CODE,
      CUSTOMER_SEQUENTIAL
     )
     (
      SELECT IMPORT_SERIAL_NO,
             CUSTOMER_CODE,
             CUSTOMER_SEQUENTIAL
      FROM MIG_IL_CUSTOMER_DETAILS WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID AND HAS_ERRORS=0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT=COUNT(ID) 
    FROM   #TEMP_CUST WITH(NOLOCK)

   ------------------------------------      
   -- GET IMPORT FILE NAME
   ------------------------------------   
    IF(@MAX_RECORD_COUNT>0)
    BEGIN
      SELECT  @IMPORT_FILE_NAME=SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9)
      FROM MIG_IL_IMPORT_REQUEST_FILES WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND 
            IMPORT_FILE_TYPE  = @IMPORT_CUST_FILE_TYPE
      
    END 
    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
       SET @CUSTOMER_ID=0
       SET @LOOP_CUST_SEQL=0
       
      
       SELECT @LOOP_CUST_CODE   = CUST_CODE,
              @IMPORT_SERIAL_NO = IMPORT_SERIAL_NO ,
              @LOOP_CUST_SEQL   = CUSTOMER_SEQUENTIAL
       FROM #TEMP_CUST WITH(NOLOCK)
       WHERE ID=@COUNTER
       
       
     
       ------------------------------------      
	   -- IF CUSTOMER ALREADY EXISTS	   
	   ------------------------------------  
       IF EXISTS(SELECT * FROM CLT_CUSTOMER_LIST  WITH(NOLOCK) WHERE CUSTOMER_CODE=@LOOP_CUST_CODE)
       BEGIN
       
		   UPDATE MIG_IL_CUSTOMER_DETAILS
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
				 @ERROR_TYPES           = 48,  --CUSTOMER ALREADY EXISTS         
				 @ACTUAL_RECORD_DATA    = '' , 
				 @ERROR_MODE            = 'VE',  -- VALIDATION ERROR      
				 @ERROR_SOURCE_TYPE     = 'CUST'   

       END 
       ELSE
       BEGIN
       
         ------------------------------------      
		 -- CREATE NEW CUSTOMER
		 ------------------------------------  
		
		 
		 INSERT INTO [dbo].[CLT_CUSTOMER_LIST]
           (
           -- [CUSTOMER_ID]                       
            [CUSTOMER_CODE]                
           ,[CUSTOMER_TYPE]				
           ,[PREFIX]					
           ,[CUSTOMER_FIRST_NAME]		
           ,[CUSTOMER_ADDRESS1]		
           ,[CUSTOMER_ADDRESS2]			
           ,[CUSTOMER_CITY]				
           ,[CUSTOMER_COUNTRY]			
           ,[CUSTOMER_STATE]			
           ,[CUSTOMER_ZIP]				
           ,[CUSTOMER_BUSINESS_DESC]	
           ,[CUSTOMER_CONTACT_NAME]	  --
           ,[CUSTOMER_BUSINESS_PHONE]	
           ,[CUSTOMER_EXT]				
           ,[CUSTOMER_HOME_PHONE]		
           ,[CUSTOMER_MOBILE]			
           ,[CUSTOMER_FAX]				
           ,[CUSTOMER_Email]			
           ,[CUSTOMER_WEBSITE]			
           ,[IS_ACTIVE]					
           ,[CREATED_DATETIME]			
           ,[MARITAL_STATUS]			
										
										
										
										
										
           ,[DATE_OF_BIRTH]				
           ,[GENDER]					
           ,[CPF_CNPJ]					
           ,[NUMBER]					
           ,[COMPLIMENT]				
           ,[DISTRICT]					
           ,[BROKER]
           ,[CUSTOMER_AGENCY_ID]					
           ,[MAIN_TITLE]
           ,[MAIN_CPF_CNPJ]				
           ,[MAIN_ADDRESS]				
           ,[MAIN_NUMBER]				
           ,[MAIN_COMPLIMENT]			
           ,[MAIN_DISTRICT]				
           ,[REGIONAL_IDENTIFICATION]	
           ,[REG_ID_ISSUE]				
           ,[ORIGINAL_ISSUE]			
           ,[MAIN_CITY]					
           ,[MAIN_STATE]				
           ,[MAIN_COUNTRY]				
           ,[MAIN_ZIPCODE]				
           ,[MAIN_FIRST_NAME]			           	
           ,[ID_TYPE]					
           ,[MONTHLY_INCOME]			
           ,[AMOUNT_TYPE]				
           ,[CADEMP]					
           ,[NET_ASSETS_AMOUNT]
           ,[NATIONALITY]
           ,[EMAIL_ADDRESS]
           ,[REGIONAL_IDENTIFICATION_TYPE]
           ,[IS_POLITICALLY_EXPOSED] 
           ,INITIAL_LOAD_FLAG
            ,MAIN_NOTE
           ,MAIN_CONTACT_CODE
           
           )         

           (
           
           
            SELECT
                 --  @CUSTOMER_ID 
                 CUSTOMER_CODE
                  ,CUST_TYPE
                  ,TITLE
                  ,CUSTOMER_NAME
                  ,[ADDRESS]
                  ,[COMPLIMENT]
                  ,CITY
                  ,COUNTRY
                  ,[STATE]
                  ,[ZIP CODE]
                  ,BUSINESS_DESC
                  ,CONTACT_CODE
                  ,[dbo].fun_FormatPhoneText(BUSINESS_PHONE)
                  ,EXT
                  ,[dbo].fun_FormatPhoneText(HOME_PHONE)
                  ,[dbo].fun_FormatPhoneText(MOBILE)
                  ,[dbo].fun_FormatPhoneText(FAX)
                  ,EMAIL_ADDRESS
                  ,WEBSITE
                  ,CASE WHEN [STATUS]='N' THEN 'N' ELSE 'Y' END
                  ,GETDATE() --CREATED DATE
                  ,CASE WHEN MARITAL_STATUS=5932 THEN 'D' --Divorced
						WHEN MARITAL_STATUS=5933 THEN 'M'--Married
						WHEN MARITAL_STATUS=5934 THEN 'P'--Separated
						WHEN MARITAL_STATUS=5935 THEN 'S'--Single
						WHEN MARITAL_STATUS=5936 THEN 'W'--Widowed
					END
				   ,CASE WHEN CUST_TYPE= 11110 -- FOR PERONAL 
				    THEN CAST (SUBSTRING([DATE OF BIRTH],1,4)+'/'+SUBSTRING([DATE OF BIRTH],5,2)+'/'+SUBSTRING([DATE OF BIRTH],7,2) AS DATE) -- DOB 
				    ELSE CAST (SUBSTRING([CREATION DATE],1,4)+'/'+SUBSTRING([CREATION DATE],5,2)+'/'+SUBSTRING([CREATION DATE],7,2) AS DATE) -- CREATION DATE 
				    END
                   ,CASE WHEN GENDER=9813 THEN 'M' ELSE 'F' END

                   ,CASE WHEN CUST_TYPE= 11110 -- FOR PERONAL 
                         THEN  SUBSTRING(CPF_CNPJ,1,3)+'.'+SUBSTRING(CPF_CNPJ,4,3)+'.'+SUBSTRING(CPF_CNPJ,7,3)+'-'+SUBSTRING(CPF_CNPJ,10,2) -- CPF FORMATTING
                         ELSE  SUBSTRING(CPF_CNPJ,1,2)+'.'+SUBSTRING(CPF_CNPJ,3,3)+'.'+SUBSTRING(CPF_CNPJ,6,3)+'/'+SUBSTRING(CPF_CNPJ,9,4)+'-'+SUBSTRING(CPF_CNPJ,13,2)---- CNPJ FORMATTING
                    END    
                   ,NUMBER
                   ,COMPLIMENT
                   ,DISTRICT
                   ,[BROKER]
                   ,[BROKER]
                   ,TITLE_1
                   ,CASE WHEN CPF IS NOT NULL THEN SUBSTRING(CPF,1,3)+'.'+SUBSTRING(CPF,4,3)+'.'+SUBSTRING(CPF,7,3)+'-'+SUBSTRING(CPF,10,2) -- CPF FORMATTING               
                    ELSE CPF END
                   ,ADDRESS1
                   ,NUMBER1
                   ,COMPLIMENT1
                   ,DISTRICT1
                   ,REG_IDENTIFICATION
                   ,CASE WHEN REG_ID_ISSUE != @TempValue THEN CAST (SUBSTRING(REG_ID_ISSUE,1,4)+'/'+SUBSTRING(REG_ID_ISSUE,5,2)+'/'+SUBSTRING(REG_ID_ISSUE,7,2) AS DATE) ELSE NULL END -- REG_ID_ISSUE ,
                   ,ORIGINAL_ISSUE
                   ,CITY1
                   ,[STATE1]
                   ,[COUNTRY1]
                   ,ZIP_CODE1
                   ,FIRST_NAME1
                   ,ID_TYPE
				   ,TOTAL_ASSETS_AMOUNT--MONTHLY_INCOME
                   ,AMOUNT_TYPE
                   ,CADEMP_NUMBER
                   ,NET_ASSETS_AMOUNT
                   ,NATIONALITY
                   ,EMAIL_ADDRESS1
                   ,REG_ID_TYPE
                   ,POLITICALLY_EXPOSED
                   ,'Y' --INITIAL_LOAD_FLAG
                   ,REMARKS
                   ,CONTACT_CODE
                   
            FROM  MIG_IL_CUSTOMER_DETAILS
            WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
                  IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO   
            
           )
           
         SELECT @CUSTOMER_ID=SCOPE_IDENTITY()  
         
         SELECT @BANK_NAME       = BANK_NAME,
                @BANK_NUMBER     = BANK_NUMBER,
                @BANK_BRANCH     = BANK_BRANCH,
                @ACCOUNT_NUMBER  = ACCOUNT_NUMBER,
                @ACCOUNT_TYPE    = ACCOUNT_TYPE
         FROM  MIG_IL_CUSTOMER_DETAILS
         WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
               IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO   
        
         
         SELECT @APPLICANT_ID=ISNULL(MAX(APPLICANT_ID),0)+1 FROM CLT_APPLICANT_LIST   WITH(NOLOCK) 
            
         ------------------------------------      
		 -- CREATE NEW APPLICANT
		 ------------------------------------  
         INSERT INTO CLT_APPLICANT_LIST                                                      
			 (                                                      
			  APPLICANT_TYPE,  
			  APPLICANT_ID,  
			  CUSTOMER_ID,  
			  TITLE,  
			  SUFFIX,  
			  FIRST_NAME,  
			  MIDDLE_NAME,  
			  LAST_NAME,  
			  ADDRESS1,  
			  ADDRESS2,  
			  CITY,  
			  COUNTRY,  
			  STATE,    
			  ZIP_CODE,  
			  PHONE,  
			  EMAIL,  
			  CO_APPL_MARITAL_STATUS,  
			  CO_APPL_SSN_NO,  
			  CO_APPL_DOB,  
			  IS_ACTIVE,  
			  CREATED_BY,  
			  CREATED_DATETIME,    
			  CO_APPLI_OCCU,  
			  CO_APPLI_EMPL_NAME,  
			  CO_APPLI_EMPL_ADDRESS,  
			  CO_APPLI_EMPL_ADDRESS1,  
			  CO_APPLI_YEARS_WITH_CURR_EMPL,    
			  CO_APPL_YEAR_CURR_OCCU,  
			  IS_PRIMARY_APPLICANT,  
			  DESC_CO_APPLI_OCCU,  
			  CO_APPLI_EMPL_CITY,  
			  CO_APPLI_EMPL_COUNTRY,    
			  CO_APPLI_EMPL_STATE,  
			  CO_APPLI_EMPL_ZIP_CODE,  
			  CO_APPLI_EMPL_PHONE,  
			  CO_APPLI_EMPL_EMAIL,  
			  MOBILE,                            
			  EMP_EXT,  
			  EXT,  
			  CO_APPL_GENDER,  
			  NUMBER,  
			  POSITION,  
			  DISTRICT,  
			  REG_ID_ISSUE,  
			  ORIGINAL_ISSUE,  
			  REGIONAL_IDENTIFICATION,  
			  NOTE,  
			  CONTACT_CODE,    
			  CPF_CNPJ,  
			  BUSINESS_PHONE,  
			  FAX  ,
			  BANK_NAME,
			  BANK_NUMBER,
			  BANK_BRANCH,
			  ACCOUNT_NUMBER,
			  ACCOUNT_TYPE   
			  )                               
			  (  
			  SELECT                                                     
			  CUSTOMER_TYPE,  
			  @APPLICANT_ID,  
			  @CUSTOMER_ID,  
			  PREFIX,    
			  CUSTOMER_SUFFIX,  
			  CUSTOMER_FIRST_NAME,  
			  null,  
			  null,    
			  CUSTOMER_ADDRESS1,  
			  CUSTOMER_ADDRESS2,  
			  CUSTOMER_CITY,  
			  CUSTOMER_COUNTRY,  
			  CUSTOMER_STATE,    
			  CUSTOMER_ZIP,  
			  CUSTOMER_HOME_PHONE,  
			  CUSTOMER_Email,  
			  MARITAL_STATUS,  
			  null,  
			  DATE_OF_BIRTH,  
			 'Y',  
			  CREATED_BY,    
			  CREATED_DATETIME,  
			  null,  
			  null,  
			  null,  
			  null,  
			  null,  
			  null,  
			  1,  
			  null,  
			  null,  
			  null,  
			  null,  
			  null,  
			 null,    
			  null,  
			  CUSTOMER_MOBILE,  
			  EMP_EXT,  
			  CUSTOMER_EXT,  
			  GENDER,  
			  NUMBER,  
			  MAIN_POSITION,  
			  DISTRICT,    
			  REG_ID_ISSUE,  
			  ORIGINAL_ISSUE,  
			  REGIONAL_IDENTIFICATION,  
			  MAIN_NOTE,  
			  CUSTOMER_CODE ,    
			  CPF_CNPJ,  
			  CUSTOMER_BUSINESS_PHONE,  
			  CUSTOMER_FAX ,
			  @BANK_NAME,
			  @BANK_NUMBER,
			  @BANK_BRANCH,
			  @ACCOUNT_NUMBER,
			  @ACCOUNT_TYPE 
			 FROM  [CLT_CUSTOMER_LIST]
			 WHERE CUSTOMER_ID=@CUSTOMER_ID
			       
			)    
			
            ------------------------------------      
		    -- UPDATE IMPORT DETAILS
		    ------------------------------------  
			 			 
			 UPDATE MIG_IL_CUSTOMER_DETAILS
			 SET    CUSTOMER_ID  = @CUSTOMER_ID,
			        IS_PROCESSED = 'Y'
			 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
			        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			        
			EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]     
				@IMPORT_REQUEST_ID 	    = @IMPORT_REQUEST_ID,
				@IMPORT_SERIAL_NO    	= @IMPORT_SERIAL_NO,	
				@CUSTOMER_ID		  	= @CUSTOMER_ID	,
				@POLICY_ID		  		= NULL,
				@POLICY_VERSION_ID 		= NULL,
				@IS_ACTIVE		  		= 'Y',
				@IS_PROCESSED	  		= 'Y',
				@FILE_TYPE		  		= @IMPORT_CUST_FILE_TYPE,
				@FILE_NAME              = @IMPORT_FILE_NAME,
				@CUSTOMER_SEQUENTIAL    = @LOOP_CUST_SEQL,
				@POLICY_SEQUENTIAL      = NULL,
				@ENDORSEMENT_SEQUENTIAL = NULL
			   
			
       
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














  
  
  
  
  
  
  
