/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_INSERT_CONTACT_DETAIL]                                                        
Created by            : Santosh Kumar Gautam                                                           
Date                  : 25 Aug 2011                                                          
Purpose               : CREATE NEW CONTACT
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_INSERT_CONTACT_DETAIL]  22
------   ------------       -------------------------*/                                                             
--                               
       
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_CONTACT_DETAIL]   
      
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
     DECLARE @CONTACT_ID INT
     DECLARE @IMPORT_SERIAL_NO INT   
     DECLARE @MAX_RECORD_COUNT INT 
     DECLARE @LOOP_CONTACT_CODE NVARCHAR(50)   
     DECLARE @LOOP_CUST_SEQUANCE_NO INT  
     DECLARE @COUNTER INT  =1
     DECLARE @TempValue VARCHAR(3) = '@T@'
       
     DECLARE @IMPORT_FILE_NAME NVARCHAR(50)   
     DECLARE @IMPORT_CUST_FILE_TYPE INT = 14936 -- FOR CUSTOMER FILE TYPE
			

     
     
     CREATE TABLE #TEMP_CONTACT
     (
	   ID INT IDENTITY(1,1),
	   CUST_SEQUANCE_NO INT,
       IMPORT_SERIAL_NO BIGINT,
       CONTACT_CODE NVARCHAR(50) NULL
     )
     
   ------------------------------------      
   -- INSERT RECORD IN TEMP TABLE
   ------------------------------------   
     INSERT INTO #TEMP_CONTACT
     (
      CUST_SEQUANCE_NO,
      IMPORT_SERIAL_NO,
      CONTACT_CODE
     )
     (
      SELECT CUSTOMER_SEQUENTIAL,
             IMPORT_SERIAL_NO,
             CONTACT_CODE
      FROM MIG_IL_CONTACT_DETAILS WITH(NOLOCK)
      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS = 0
     )

   ------------------------------------      
   -- GET MAX RECOUNT COUNT
   ------------------------------------   
    SELECT @MAX_RECORD_COUNT = COUNT(ID) 
    FROM   #TEMP_CONTACT 

   ------------------------------------      
   -- GET FILE NAME
   ------------------------------------   
   IF(@MAX_RECORD_COUNT>0)
   BEGIN
   
    SELECT @IMPORT_FILE_NAME = SUBSTRING(ISNULL(DISPLAY_FILE_NAME,''),1,9) 
    FROM  MIG_IL_IMPORT_REQUEST_FILES
    WHERE IMPORT_REQUEST_ID  = @IMPORT_REQUEST_ID AND
          IMPORT_FILE_TYPE   = 14938  -- FOR CONTACT FILE TYPE
   
   END
    
    WHILE(@COUNTER<=@MAX_RECORD_COUNT)
    BEGIN
       
       SET @CUSTOMER_ID=0
       SET @ERROR_NO=0
       
       SELECT @LOOP_CONTACT_CODE    = CONTACT_CODE,
              @IMPORT_SERIAL_NO		 = IMPORT_SERIAL_NO ,
              @LOOP_CUST_SEQUANCE_NO = CUST_SEQUANCE_NO
       FROM   #TEMP_CONTACT WHERE ID   = @COUNTER      
       
     
       ------------------------------------      
	   -- GET CUSTOMER ID
	   ------------------------------------ 
	   SELECT @CUSTOMER_ID = CUSTOMER_ID 
	   FROM   MIG_IL_IMPORT_SUMMARY
	   WHERE  CUSTOMER_SEQUENTIAL = @LOOP_CUST_SEQUANCE_NO AND
	          FILE_TYPE           = @IMPORT_CUST_FILE_TYPE AND
	          [FILE_NAME]         = @IMPORT_FILE_NAME      AND
	          IS_ACTIVE			  = 'Y'
	  
      IF(@CUSTOMER_ID IS NULL OR @CUSTOMER_ID='' OR @CUSTOMER_ID=0)
        SET @ERROR_NO =50 -- CUSTOMER DOES NOT EXISTS         
      ELSE IF EXISTS(SELECT * FROM MNT_CONTACT_LIST WITH(NOLOCK) WHERE  CONTACT_CODE=@LOOP_CONTACT_CODE)
        SET @ERROR_NO =52 -- CONTACT ALREADY EXISTS     
        
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
				 @ERROR_SOURCE_TYPE     = 'CONT'   
				 

       END 
       ELSE
       BEGIN
       
        
      --   SELECT @CONTACT_ID=ISNULL(MAX(CONTACT_ID),0)+1 FROM MNT_CONTACT_LIST   WITH(NOLOCK)    
         ------------------------------------      
		 -- CREATE NEW CONTACT
		 ------------------------------------  
        	INSERT INTO MNT_CONTACT_LIST    
					(    
					CONTACT_CODE,                
					CONTACT_TYPE_ID,    
					CONTACT_SALUTATION,    
					CONTACT_POS,    
					INDIVIDUAL_CONTACT_ID,    
					CONTACT_FNAME,    					
					CONTACT_ADD1,    
					CONTACT_ADD2,    
					CONTACT_CITY,    
					CONTACT_STATE,    
					CONTACT_ZIP,    
					CONTACT_COUNTRY,    
					CONTACT_BUSINESS_PHONE,    
					CONTACT_EXT,    
					CONTACT_FAX,    
					CONTACT_MOBILE,    
					CONTACT_EMAIL,    
					CONTACT_PAGER,    
					CONTACT_HOME_PHONE,    
					CONTACT_TOLL_FREE,    
					CONTACT_NOTE,    
					CONTACT_AGENCY_ID,    
					IS_ACTIVE,    					
					CREATED_DATETIME,    					
					DATE_OF_BIRTH,
                    CPF_CNPJ,
                    REGIONAL_IDENTIFICATION,
                    REG_ID_ISSUE_DATE,
                    ACTIVITY,
                    NUMBER,
                    DISTRICT,
                    REG_ID_ISSUE,
                    REGIONAL_ID_TYPE,
                    NATIONALITY       
					)    
					
					(    
					SELECT
					 CONTACT_CODE,    
					2,--CONTACT_TYPE_ID,    
					TITLE,    
					CONTARCT_POSITION,    
					@CUSTOMER_ID,--@INDIVIDUAL_CONTACT_ID,    
					FIRST_NAME,    					   
					ADDRESS1,    
					ADDRESS2,    
					CITY,    
					STATE,    
					[ZIP CODE],    
					COUNTRY,    
					dbo.fun_FormatPhoneText(BUSINESS_PHONE),    
					SUBSTRING(CONTACT_PHONE_EXT,1,5),    
					dbo.fun_FormatPhoneText(FAX),    
					dbo.fun_FormatPhoneText(MOBILE),    
					EMAIL_ADDRESS,    
					PAGER,    
					dbo.fun_FormatPhoneText(HOME_PHONE),    
					TOLL_FREE_NO,    
					NOTE,    
					0,--AGENCY_ID,    
					'Y',    					
					GETDATE(),    					
					CASE WHEN [DATE OF BIRTH] IS NOT NULL AND [DATE OF BIRTH]!=@TempValue   THEN CAST (SUBSTRING([DATE OF BIRTH],1,4)+'/'+SUBSTRING([DATE OF BIRTH],5,2)+'/'+SUBSTRING([DATE OF BIRTH],7,2) AS DATE) -- DOB ,
					     ELSE NULL END,
                    CASE WHEN CPF_CNPJ IS NOT NULL THEN SUBSTRING(CPF_CNPJ,1,3)+'.'+SUBSTRING(CPF_CNPJ,4,3)+'.'+SUBSTRING(CPF_CNPJ,7,3)+'-'+SUBSTRING(CPF_CNPJ,10,2) -- CPF FORMATTING
                         ELSE CPF_CNPJ END
                    ,
                    REG_ID,
                	CASE WHEN REG_ID_ISSUE_DATE IS NOT NULL AND REG_ID_ISSUE_DATE!=@TempValue   THEN CAST (SUBSTRING(REG_ID_ISSUE_DATE,1,4)+'/'+SUBSTRING(REG_ID_ISSUE_DATE,5,2)+'/'+SUBSTRING(REG_ID_ISSUE_DATE,7,2) AS DATE) -- DOB ,
				     ELSE NULL END,
                    ACTIVITY,
                    NUMBER,
                    DISTRICT,
                    NULL,--REG_ID_ISSUE,
                    REG_ID_ISSUE_TYPE,
                    NATIONALITY
                     FROM  MIG_IL_CONTACT_DETAILS
					 WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
						   IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO      
					)    
				
			SELECT @CONTACT_ID = SCOPE_IDENTITY()   
		      			
        ------------------------------------      
	    -- UPDATE IMPORT DETAILS
	    ------------------------------------  
		 			 
		 UPDATE MIG_IL_CONTACT_DETAILS
		 SET    CONTACT_ID   = @CONTACT_ID,
		        CUSTOMER_ID  = @CUSTOMER_ID,
		        IS_PROCESSED = 'Y'
		 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
		        IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			
       
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



















