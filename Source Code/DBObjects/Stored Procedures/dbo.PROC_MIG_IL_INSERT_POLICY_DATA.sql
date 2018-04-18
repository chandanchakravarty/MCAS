/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_DATA]    Script Date: 12/02/2011 16:07:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_POLICY_DATA]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_DATA]
GO


/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_POLICY_DATA]    Script Date: 12/02/2011 16:07:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================          
-- Author:  <ATUL KUMAR SINGH>          
-- Create date: <2011-08-23>          
-- Description: <insert data in all ebix respective tables from Initial Load table for file '011800001APOLICE.xlsx'>          
-- DROP proc [PROC_MIG_IL_INSERT_POLICY_DATA] 1841
-- =============================================          
CREATE PROCEDURE [dbo].[PROC_MIG_IL_INSERT_POLICY_DATA]           
          
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
          
          
DECLARE @CUSTOMER_CODE VARCHAR(20)          
DECLARE @CUSTOMER_ID INT          
DECLARE @POLICY_ID INT          
DECLARE @POLICY_VERSION_ID INT          
DECLARE @PROCESS_TYPE INT  
DECLARE @POLICY_NUMBER NVARCHAR(21)          
DECLARE @ENDORSEMENT_NUMBER INT          
DECLARE @APP_ID INT     --FOR APP_ID                  
DECLARE @APP_VERSION_ID INT   --FOR APP_VERSION_ID                  
DECLARE @POLICY_DISP_VERSION NVARCHAR(6)='1.0' --FOR POLICY_DISP_VERSION                  
DECLARE @APP_VERSION NVARCHAR(6)='1.0'--FOR APP VERSION             
          
DECLARE @CREATED_BY INT=3 -- ADMINSTRATOR          
DECLARE @ENDORSEMENT_STATUS VARCHAR(5)='COMP'  -- COMPLETE          
DECLARE @PREVIOUS_POLICY_STATUS NVARCHAR(20)            
DECLARE @CURRENT_POLICY_STATUS NVARCHAR(20)  
          
DECLARE @LOOP_POLICY_NUMBER NVARCHAR(50)               
DECLARE @LOOP_POLICY_SEQUANCE_NO INT              
DECLARE @LOOP_END_SEQUANCE_NO INT              
DECLARE @LOOP_IMPORT_SERIAL_NO INT         
DECLARE @CSR INT       
DECLARE @UNDERWRITER INT  
DECLARE @PRODUCER INT  
DECLARE @COUNTER INT  =1            
DECLARE @MAX_RECORD_COUNT INT             
DECLARE @ERROR_NO INT=0            
DECLARE @IMPORT_SERIAL_NO INT           
DECLARE @LOOP_POLICY_LOB INT      
DECLARE @LOOP_AGENCY_ID INT       
DECLARE @LOOP_AGENCY_CODE INT      
DECLARE @EFFETIVE_DATE DATETIME
DECLARE @EXPIRY_DATE DATETIME
          
DECLARE @DIV_CODE VARCHAR(10)           
DECLARE @DIV_ID INT          
DECLARE @DEPT_ID INT           
DECLARE @PC_ID INT           
DECLARE @NEW_VERSION_ID INT   
          
DECLARE @IMPORT_FILE_NAME NVARCHAR(50)             
DECLARE @IMPORT_POLICY_FILE_TYPE INT = 14939 -- FOR POLICY FILE TYPE          
DECLARE @APP_NUMBER NVARCHAR(100)    
DECLARE @PROCESS_ID INT    

DECLARE @CANCELLATION_OPTION INT                        
DECLARE @CANCELLATION_TYPE	 INT     
DECLARE @CANCELLATION_EFFECTIVE_DATE NVARCHAR(10)  
DECLARE @CANCELLATION_EFFECTIVE_HH  INT
DECLARE @CANCELLATION_EFFECTIVE_MM  INT
DECLARE @CANCELLATION_EFFECTIVE_AM_PM INT        
DECLARE @POL_LEVEL_COMM_APPLIES NVARCHAR(10) 
DECLARE @REASON				 INT                    
DECLARE @POL_LEVEL_COMMISSION DECIMAL(18,2)    

DECLARE @OTHER_REASON		 NVARCHAR(500)
DECLARE @RETURN_PREMIUM		 DECIMAL(13)                              
DECLARE @PAST_DUE_PREMIUM	 DECIMAL(13)                             
DECLARE @ENDORSEMENT_NO		 INT   

DECLARE @ROW_ID INT
DECLARE @REQUESTED_BY INT

DECLARE @CREATED_DATE DATETIME
DECLARE @IS_VALID     CHAR(1) 
DECLARE @ERROR_TYPES    NVARCHAR(MAX) 
DECLARE @LOB_ID INT

DECLARE	@PROCESS_STATUS NVARCHAR(20)	
DECLARE @ENDORSEMENT_TYPE INT 		
DECLARE @ENDORSEMENT_OPTION	INT
DECLARE @END_APPL_NUMBER NVARCHAR(50)	
DECLARE @COMMENTS NVARCHAR(500)	

	    
 ----------------------------------------------------------------------------------------         
          
BEGIN TRY          


SELECT  @CREATED_DATE =GETDATE()

-------------------------------- CREATE TEMP TABLE FOR THOSE RECORDS NEEDS TO BE PROCESSED          
-----------------------------------------------------------------------------------------          
    CREATE TABLE #TEMP_POLICY          
     (          
		ID INT IDENTITY(1,1),          
		POLICY_SEQUANCE_NO INT,          
		END_SEQUANCE_NO INT,         
		IMPORT_SERIAL_NO BIGINT,          
		POLICY_NUMBER NVARCHAR(50) NULL,      
		LOB INT NULL,      
		AGENCY_CODE NVARCHAR(20)   NULL,
		PROCESS_TYPE INT NULL   
     )          
        
        
        
  INSERT INTO #TEMP_POLICY       
     (          
		POLICY_SEQUANCE_NO ,          
		END_SEQUANCE_NO ,         
		IMPORT_SERIAL_NO,          
		POLICY_NUMBER,      
		LOB,      
		AGENCY_CODE,
		PROCESS_TYPE  
              
     )          
     (          
      SELECT POLICY_SEQUENCE_NO,        
			ENDORSEMENT_SEQUENTIAL_NO,        
			IMPORT_SERIAL_NO,        
			POLICY_NUMBER,      
			PRODUCT,      
			[BROKER]   ,
			PROCESS_TYPE     
                     
      FROM MIG_IL_POLICY_DETAILS WITH(NOLOCK)          
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
    
   ------------------------------------                
   -- FETCH ALL ACTIVE AGENCY   
   ------------------------------------   
     SELECT  CAST(AGENCY_CODE AS INT)AS AGENCY_CODE, AGENCY_ID
				 INTO #TempAgencyList
				 FROM MNT_AGENCY_LIST 
				 WHERE AGENCY_CODE NOT IN (SELECT REIN_COMAPANY_CODE FROM MNT_REIN_COMAPANY_LIST WITH(NOLOCK)) AND
				       IS_ACTIVE='Y'
				 
          
  WHILE(@COUNTER<=@MAX_RECORD_COUNT)            
  BEGIN          
    SET @ERROR_NO=0            
    SET @POLICY_ID=0          
    SET @CUSTOMER_ID=0          
    SET @POLICY_VERSION_ID=0          
    SET @LOOP_AGENCY_ID=0      
    SET @LOOP_POLICY_LOB=0      
    SET @LOOP_AGENCY_CODE=''
          
      SELECT @LOOP_POLICY_NUMBER    = POLICY_NUMBER,            
       @IMPORT_SERIAL_NO   = IMPORT_SERIAL_NO ,            
       @LOOP_POLICY_SEQUANCE_NO = POLICY_SEQUANCE_NO  ,          
       @LOOP_END_SEQUANCE_NO = END_SEQUANCE_NO,            
       @LOOP_AGENCY_CODE  = CAST(AGENCY_CODE  AS INT)  ,      
       @LOOP_POLICY_LOB  = LOB ,
       @PROCESS_TYPE     = PROCESS_TYPE     
     FROM   #TEMP_POLICY (NOLOCK) WHERE ID   = @COUNTER               
       
                
     -------------------------------- FETCH CUSTOMER_ID ASSUMING THAT CUSTOMER CODE IS UNIQUE          
-----------------------------------------------------------------------------------------          
          
 --SELECT CUSTOMER CODE, POLICY_NUMBER, ENDORSEMENT_NUMBER FROM SOURCE TABLE          
 SELECT  @CUSTOMER_CODE  = CUSTOMER_CODE,          
    @POLICY_NUMBER  = POLICY_NUMBER,          
    @ENDORSEMENT_NUMBER = ENDORSEMENT_NUMBER,          
    @DIV_CODE   = DIVDEPTPC ,
    @APP_NUMBER = APPLICATION_NUMBER         
 FROM  MIG_IL_POLICY_DETAILS WITH (NOLOCK)          
 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID           
 AND   IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO           
 AND   IS_PROCESSED  = 'N'          

  IF(@ERROR_NO>0)            
      BEGIN            
                   
      UPDATE MIG_IL_CONTACT_DETAILS        
SET    HAS_ERRORS=1                  
      WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND             
       IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO             
          
          
         EXEC  [PROC_MIG_IL_INSERT_IMPORT_ERROR_DETAILS]                             
        @IMPORT_REQUEST_ID     = @IMPORT_REQUEST_ID,                  
        @IMPORT_SERIAL_NO    = @IMPORT_SERIAL_NO  ,                      
        @ERROR_SOURCE_FILE     = ''     ,                  
        @ERROR_SOURCE_COLUMN   = ''     ,                  
        @ERROR_SOURCE_COLUMN_VALUE= '' ,                  
        @ERROR_ROW_NUMBER      = @IMPORT_SERIAL_NO   ,                    
        @ERROR_TYPES           = @ERROR_NO,               
        @ACTUAL_RECORD_DATA    = '' ,                  
        @ERROR_MODE            = 'VE',  -- VALIDATION ERROR                  
        @ERROR_SOURCE_TYPE     = 'POLC' 
    END 
    ELSE            
       BEGIN          
  
   --SELECT CUSTOMER id FROM CUSTOMER TABLE          
 SELECT TOP 1  @CUSTOMER_ID= CUSTOMER_ID FROM CLT_CUSTOMER_LIST WITH (NOLOCK) WHERE CUSTOMER_CODE=@CUSTOMER_CODE          
-----------------------------------------------------------------------------------------          
         
-------------------------------------------------------  FETCH DIV DEPT PC FROM DIV ID          
          
  SELECT  @DIV_ID=DIV_ID FROM MNT_DIV_LIST (NOLOCK) WHERE DIV_CODE=@DIV_CODE AND IS_ACTIVE='Y' 
  IF(@DIV_ID IS NULL OR @DIV_ID=0)
  BEGIN
  
   SELECT  @DIV_ID=DIV_ID FROM MNT_DIV_LIST (NOLOCK) WHERE DIV_CODE=CAST(@DIV_CODE AS INT) AND IS_ACTIVE='Y' 
   
  END
           
  SELECT TOP 1 @DEPT_ID=DEPT_ID FROM MNT_DIV_DEPT_MAPPING (NOLOCK) WHERE DIV_ID=@DIV_ID          
  SELECT  TOP 1 @PC_ID  =PC_ID FROM MNT_DEPT_PC_MAPPING (NOLOCK) WHERE DEPT_ID=@DEPT_ID 
           
  SELECT top 1 @LOOP_AGENCY_ID=AGENCY_ID 
  FROM #TempAgencyList WITH(NOLOCK) 
  WHERE AGENCY_CODE=@LOOP_AGENCY_CODE  
       
-------------------------------- FETCH POLICY_ID, POLICY_VERSION_ID          
-----------------------------------------------------------------------------------------          
 -- FIND MAX NUMBER OF POLICY_ID IN SYSTEM FOR CUSTOMER_ID          
 -- IF THERE IS NO EXISTING POLICY FOR THIS CUSTOMER IT WILL START FROM 1 OTHERWISE MAX(POLICY-ID) + 1          
 SELECT  @POLICY_ID  = ISNULL(MAX(POLICY_ID),0)+1,          
    @APP_ID   = (ISNULL(MAX(APP_ID),0)+1)                      
                       
 FROM  POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)           
 WHERE  CUSTOMER_ID  = @CUSTOMER_ID          
          
        
 IF( @PROCESS_TYPE =1) -- SET POLICY_VERSION_ID IF ENDORSEMENT_NUMBER=0      
 BEGIN    
		 SET @POLICY_VERSION_ID=1          
  
        -- SET APPLICATION NUMBER  
       -- SET @APP_NUMBER=(SELECT dbo.func_GENERATE_APP_NUMBER_MIG(@LOOP_POLICY_LOB,@LOOP_AGENCY_ID))       
          
          
          
          
          
			-------------------------------- INSERT INTO POL_CUSTOMER_POLICY_LIST          
			 INSERT INTO [dbo].[POL_CUSTOMER_POLICY_LIST]          
					   ([CUSTOMER_ID]          
					   ,[POLICY_ID]          
					   ,[POLICY_VERSION_ID]          
					   ,[APP_ID]          
					   ,[APP_VERSION_ID]          
					   ,[POLICY_TYPE]   -- 0          
					   --,[POLICY_NUMBER]          
					   ,[POLICY_DISP_VERSION]          
					   ,[POLICY_STATUS]            
					   ,[POLICY_LOB]          
					   ,[POLICY_SUBLOB]          
					   ,[POLICY_DESCRIPTION]          
					   ,[ACCOUNT_EXEC]   -- NULL          
					   ,[CSR]   --            
					   ,[UNDERWRITER]              
			          
					   ,[PROCESS_STATUS]          
					   ,[IS_UNDER_CONFIRMATION] -- NULL          
					   ,[LAST_PROCESS]   -- NULL          
					   ,[LAST_PROCESS_COMPLETED] -- NULL          
					   ,[IS_ACTIVE]    -- 'Y'          
					   ,[CREATED_BY]    -- HAVE TO DECIDE          
					   ,[CREATED_DATETIME]  -- GETDATE() HAVE TO DECIDE          
					   ,[MODIFIED_BY]    -- NULL          
					   ,[LAST_UPDATED_DATETIME] --NULL          
					   ,[POLICY_ACCOUNT_STATUS] -- NULL          
					   ,[AGENCY_ID]    -- IN COMMUNICATION          
					   ,[PARENT_APP_VERSION_ID] -- 0          
					   ,[APP_STATUS]    -- COMPLETE           
					   ,[APP_NUMBER]    -- Nro. Proposta do Endosso          
			          
					   ,[APP_VERSION]    -- POLICY_VERSION_ID           
					   ,[APP_TERMS]    -- Vigência (em dias)  
			          
					  ,[APP_INCEPTION_DATE]  -- Data da Proposta          
			          
					   ,[APP_EFFECTIVE_DATE]  --Inicio da Vigência     
			         
					   ,[APP_EXPIRATION_DATE]  --Final da Vigência          
			          
					   ,[IS_UNDER_REVIEW]   -- NULL          
					   ,[COUNTRY_ID]    -- 5          
					   ,[STATE_ID]  -- 0          
					   ,[DIV_ID]     -- clearyfication          
					   ,[DEPT_ID]     -- clearyfication          
					   ,[PC_ID]     -- clearyfication          
					   ,[BILL_TYPE]    -- Tipo de Cobrança          
			          
					   ,[COMPLETE_APP]   -- BLANK          
					   ,[INSTALL_PLAN_ID]   -- Plano de Cobrança          
					   ,[CHARGE_OFF_PRMIUM]  -- NULL          
					   ,[RECEIVED_PRMIUM]   -- 0.00          
					   ,[PROXY_SIGN_OBTAINED]  -- 0          
					   ,[SHOW_QUOTE]    -- NULL          
					   ,[APP_VERIFICATION_XML] -- NULL          
					   ,[YEAR_AT_CURR_RESI]  -- 0     
					   ,[YEARS_AT_PREV_ADD]  -- null          
					   ,[POLICY_TERMS]   -- Vigência (em dias)          
					   ,[POLICY_EFFECTIVE_DATE] --          
					   ,[POLICY_EXPIRATION_DATE]          
					   ,[POLICY_STATUS_CODE]  -- NULL          
					   ,[SEND_RENEWAL_DIARY_REM] -- NULL          
					   ,[TO_BE_AUTO_RENEWED]  -- NULL          
					   ,[POLICY_PREMIUM_XML]  -- NULL          
					   ,[MVR_WIN_SERVICE]   -- NULL          
					   ,[ALL_DATA_VALID]   -- NULL          
					   ,[PIC_OF_LOC]    -- NULL          
					   ,[PROPRTY_INSP_CREDIT]  -- 0          
					   ,[BILL_TYPE_ID]   --8460          
					   ,[IS_HOME_EMP]    -- 0          
					   ,[RULE_INPUT_XML]   -- NULL          
					   ,[POL_VER_EFFECTIVE_DATE]          
					   ,[POL_VER_EXPIRATION_DATE]          
					   ,[APPLY_INSURANCE_SCORE] -- -1          
					   ,[DWELLING_ID]    -- NULL          
					   ,[ADD_INT_ID]    -- NULL          
					   ,[PRODUCER]          
					   ,[DOWN_PAY_MODE]          
					   ,[CURRENT_TERM]   --1          
					   ,[NOT_RENEW]    -- NULL          
					   ,[NOT_RENEW_REASON]  -- NULL          
					   ,[REFER_UNDERWRITER]  -- NULL          
					   ,[REFERAL_INSTRUCTIONS] -- NULL          
					   ,[REINS_SPECIAL_ACPT]  -- NULL          
					   ,[FROM_AS400]    -- 'I'          
					   ,[CUSTOMER_REASON_CODE] -- NULL          
					   ,[CUSTOMER_REASON_CODE2] -- NULL          
					   ,[CUSTOMER_REASON_CODE3] -- NULL          
					   ,[CUSTOMER_REASON_CODE4] -- NULL          
					   ,[IS_REWRITE_POLICY]  --NULL           
					   ,[IS_YEAR_WITH_WOL_UPDATED]-- NULL          
					   ,[POLICY_CURRENCY]          
					   ,[POLICY_LEVEL_COMISSION]          
					   ,[BILLTO]          
					   ,[PAYOR]     -- 14542          
					   ,[CO_INSURANCE]          
					   ,[CONTACT_PERSON]   -- 0          
					   ,[TRANSACTION_TYPE]          
					   ,[PREFERENCE_DAY]   --0          
					   ,[BROKER_REQUEST_NO]  -- 0  plz suggest          
					   ,[POLICY_LEVEL_COMM_APPLIES]          
					   ,[BROKER_COMM_FIRST_INSTM] -- NULL          
					   ,[OLD_POLICY_NUMBER]  -- NULL          
					   ,[APP_SUBMITTED_DATE]          
					   ,[POLICY_VERIFY_DIGIT]  -- NULL          
					   ,[SUSEP_LOB_CODE]          
			            
					   ,IL_POLICY_NUMBER              
					   ,IL_APPLICATION_NUMBER        
					   )          
			             
				SELECT      @CUSTOMER_ID          
					,@POLICY_ID          
					,@POLICY_VERSION_ID          
					,@APP_ID          
					,@POLICY_VERSION_ID--@APP_VERSION_ID          
					,0          
					--,POLICY_NUMBER          
					,@POLICY_DISP_VERSION          
					,null--POLICY_STATUS          
					,PRODUCT          
					,LINE_OF_BUSINESS          
					,COMMENTS          
					,NULL          
					,MUL.[USER_ID]  -- CSR NEED TO IMPLEMENT          
					,ISNULL(UNDERWRITER,0)
				    ,NULL-- PROCESS STATUS          
					,NULL          
					,NULL       
					,NULL          
					,'Y'          
					,dbo.fun_GetDefaultUserID()          
					,GETDATE()  
					,NULL          
					,NULL          
					,NULL          
					,@LOOP_AGENCY_ID -- AGENCY ID           
					,0          
					,'APPLICATION'          
			        , @APP_NUMBER        
					,@APP_VERSION          
					,TERMS_IN_DAYS          
					,CONVERT(DATETIME,(LEFT(INCEPTION_DATE ,4)+'-'+SUBSTRING(INCEPTION_DATE ,5,2)+'-'+RIGHT(INCEPTION_DATE ,2)))--INCEPTION_DATE           
					,CONVERT(DATETIME,(LEFT(EFFECTIVE_DATE ,4)+'-'+SUBSTRING(EFFECTIVE_DATE ,5,2)+'-'+RIGHT(EFFECTIVE_DATE ,2)))--EFFECTIVE_DATE           
					,CONVERT(DATETIME,(LEFT(EXPIRATION_DATE ,4)+'-'+SUBSTRING(EXPIRATION_DATE ,5,2)+'-'+RIGHT(EXPIRATION_DATE ,2)))--EXPIRATION_DATE           
					,NULL          
					,5          
					,0          
					,@DIV_ID -- div ID          
					,@DEPT_ID -- dept ID          
					,@PC_ID -- pc ID          
					,BILL_TYPE          
					,''          
					,BILLING_PLAN          
					,NULL          
					,0.00          
					,0          
					,NULL          
					,NULL          
					,0          
					,NULL          
					,TERMS_IN_DAYS          
					,CONVERT(DATETIME,(LEFT(EFFECTIVE_DATE,4)+'-'+SUBSTRING(EFFECTIVE_DATE,5,2)+'-'+RIGHT(EFFECTIVE_DATE,2)))--EFFECTIVE_DATE          
					,CONVERT(DATETIME,(LEFT(EXPIRATION_DATE,4)+'-'+SUBSTRING(EXPIRATION_DATE,5,2)+'-'+RIGHT(EXPIRATION_DATE,2)))--EXPIRATION_DATE          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,0          
					,8460          
					,0          
					,NULL          
					,CONVERT(DATETIME,(LEFT(EFFECTIVE_DATE,4)+'-'+SUBSTRING(EFFECTIVE_DATE,5,2)+'-'+RIGHT(EFFECTIVE_DATE,2)))          
					,CONVERT(DATETIME,(LEFT(EXPIRATION_DATE,4)+'-'+SUBSTRING(EXPIRATION_DATE,5,2)+'-'+RIGHT(EXPIRATION_DATE,2)))          
			                  
					,-1          
					,NULL          
					,NULL          
					,PRODUCER          
					,DOWN_PAYMENT_MODE          
					,1          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,'I'          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,NULL          
					,POLICY_CURRENCY          
					,CASE WHEN (RTRIM(LTRIM(POLICY_LEVEL_COMMISSION_APPLIES))='Y') THEN POLICY_LEVEL_COMMISSION ELSE NULL --- (CONVERT(FLOAT,SUBSTRING(RTRIM(LTRIM(POLICY_LEVEL_COMMISSION)),0,CHARINDEX('.',RTRIM(LTRIM(POLICY_LEVEL_COMMISSION))))))/100         
			         END         
			         -- CONVERSION APPLIED COZ, IN SOURCE EXCEL FILE THIS FIELD IS TREATED AS DOUBLE        
					,BILL_TO          
					,14542          
					,CO_INSURANCE          
					,0          
					,CASE WHEN LTRIM(RTRIM(TRANSACTION_TYPE))=12 THEN 14559  -- SINGLE POLICY          
					   WHEN LTRIM(RTRIM(TRANSACTION_TYPE))=13 THEN 14679  --Fronting Premium Policy          
					   WHEN LTRIM(RTRIM(TRANSACTION_TYPE))=14 THEN 14560  --OPEN Policy          
					   WHEN LTRIM(RTRIM(TRANSACTION_TYPE))=15 THEN 14561  --Adjustable Policy          
					 END          
					,0          
						,0          
					,POLICY_LEVEL_COMMISSION_APPLIES          
					,NULL          
					,NULL          
					,CONVERT(DATETIME,(LEFT(ENDORSEMENT_EFFECTIVE_DATE,4)+'-'+SUBSTRING(ENDORSEMENT_EFFECTIVE_DATE,5,2)+'-'+RIGHT(ENDORSEMENT_EFFECTIVE_DATE,2)))  -- APP SUBMITTED DATE (NEED TO WORK)          
					,NULL  
					,MLM.SUSEP_LOB_CODE          
					,POLICY_NUMBER          
					,APPLICATION_NUMBER          
			 FROM      MIG_IL_POLICY_DETAILS MIPD WITH (NOLOCK)           
			 LEFT OUTER JOIN    MNT_LOB_MASTER MLM WITH (NOLOCK)      
			 ON       MIPD.PRODUCT = MLM.LOB_ID          
			 LEFT OUTER JOIN    MNT_USER_LIST (NOLOCK) MUL          
			 ON       MUL.CARRIER_CSR_ID=MIPD.CSRNAME          
			 WHERE      IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID          
			 AND       IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO           
			 AND       IS_PROCESSED  = 'N'           
			 AND       MLM.IS_ACTIVE='Y'          
          
END  --- END OF IF( @PROCESS_TYPE =1 AND @ENDORSEMENT_NUMBER=0)  

 --========================================================
 -- FOR POLICY ENDORSEMENT AND POLICY CANCELLATION
 --========================================================   
 ELSE    
 BEGIN     
 
     SET @NEW_VERSION_ID=0
     
    --============================================================
    --  ~~~~~~~~~~~~~~ PROCESS TYPE ~~~~~~~~~~~~~~~~
    --============================================================
    
    --------------------------------------------------------------
	--  INTIAL LOAD DOMAIN    ==  SYSTEM ID
	--------------------------------------------------------------
	--1	NEW NBS				  == 24	NEW BUSINESS COMMIT IN PROGRESS
	--2	CANCEL POLICY         == 2  POLICY CANCELLATION IN PROGRESS
	--3	ENDORSEMENT           == 3	Endorsement IN PROGRESS
	--4	RENEWAL POLICY        == 5	Renewal
	--5	UNDO ENDORSEMENT      == 35	UnDo Endorsement
	--6	REWRITE POLICY        == 31	Rewrite
	
	
     ------------------------------------------------------------
     -- GET POLICY DETAILS
     ------------------------------------------------------------  
     SELECT @CUSTOMER_ID	   	   = CUSTOMER_ID, 
			@POLICY_ID		   	   = POLICY_ID, 
			@POLICY_VERSION_ID 	   = POLICY_VERSION_ID
     FROM MIG_IL_IMPORT_SUMMARY (NOLOCK) 
     WHERE [FILE_NAME]			   = @IMPORT_FILE_NAME AND 
			FILE_TYPE			   = @IMPORT_POLICY_FILE_TYPE AND
            POLICY_SEQUENTIAL	   = @LOOP_POLICY_SEQUANCE_NO  AND   
            ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO AND  
            IS_ACTIVE			   = 'Y'  
       
     
 --   select 
	--@CUSTOMER_ID	   as 	 '@CUSTOMER_ID	   ',
	--@POLICY_ID		   	as '@POLICY_ID'		,   
	--@POLICY_VERSION_ID 	 as '@POLICY_VERSION_ID '
    
    
    
     ------------------------------------------------------------
     -- CHECK MAX POLICY VERSION ID
     ------------------------------------------------------------  
     SELECT @POLICY_VERSION_ID = MAX(POLICY_VERSION_ID)
     FROM  POL_CUSTOMER_POLICY_LIST 
     WHERE CUSTOMER_ID = @CUSTOMER_ID AND
           POLICY_ID   = @POLICY_ID
       
       
       
       
       
     		          
	 SELECT @EFFETIVE_DATE   =  CASE  --WHEN @PROCESS_TYPE =1  -- NEW POLICY COMMIT
									  --THEN CONVERT(DATETIME,(LEFT(EFFECTIVE_DATE,4)+'-'+SUBSTRING(EFFECTIVE_DATE,5,2)+'-'+RIGHT(EFFECTIVE_DATE,2)))  
									  WHEN @PROCESS_TYPE =2  -- POLICY CANCELLATION EFFECTIVE DATE
									  THEN CONVERT(DATETIME,(LEFT(CANCELLATION_EFFECTIVE_DATE,4)+'-'+SUBSTRING(CANCELLATION_EFFECTIVE_DATE,5,2)+'-'+RIGHT(CANCELLATION_EFFECTIVE_DATE,2))) 
									  WHEN @PROCESS_TYPE =3  -- ENDORSEMENT
									  THEN CONVERT(DATETIME,(LEFT(ENDORSEMENT_EFFECTIVE_DATE,4)+'-'+SUBSTRING(ENDORSEMENT_EFFECTIVE_DATE,5,2)+'-'+RIGHT(ENDORSEMENT_EFFECTIVE_DATE,2)))  
									  ELSE NULL END,
								      
			 @EXPIRY_DATE     =  CASE --WHEN @PROCESS_TYPE =1  -- NEW POLICY COMMIT
									  --THEN CONVERT(DATETIME,(LEFT(EXPIRATION_DATE,4)+'-'+SUBSTRING(EXPIRATION_DATE,5,2)+'-'+RIGHT(EXPIRATION_DATE,2)))  
									  WHEN @PROCESS_TYPE =2  -- POLICY CANCELLATION EXPIRATION_ DATE
									  THEN CONVERT(DATETIME,(LEFT(EXPIRATION_DATE,4)+'-'+SUBSTRING(EXPIRATION_DATE,5,2)+'-'+RIGHT(EXPIRATION_DATE,2))) 
									  WHEN @PROCESS_TYPE =3  -- ENDORSEMENT
									 THEN CONVERT(DATETIME,(LEFT(ENDORSEMENT_EXPIRY_DATE,4)+'-'+SUBSTRING(ENDORSEMENT_EXPIRY_DATE,5,2)+'-'+RIGHT(ENDORSEMENT_EXPIRY_DATE,2)))  
									  ELSE NULL END,
             @CANCELLATION_OPTION = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_OPTION ELSE NULL END,
             @CANCELLATION_TYPE	  = CASE WHEN @PROCESS_TYPE=2 THEN POLICY_CANCELLATION_TYPE ELSE NULL END,
             @REASON			  = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_REASON ELSE NULL END,
             @OTHER_REASON		  = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_REASON_DESC ELSE NULL END,
             @RETURN_PREMIUM	  = CASE WHEN @PROCESS_TYPE=2 THEN RETURN_PREMIUM ELSE NULL END,		
             
             @CANCELLATION_EFFECTIVE_AM_PM = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_EFFECTIVE_AM_PM ELSE NULL END,
             --@CANCELLATION_EFFECTIVE_DATE  = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_EFFECTIVE_DATE ELSE NULL END,
             @CANCELLATION_EFFECTIVE_HH	   = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_EFFECTIVE_HH ELSE NULL END,
             @CANCELLATION_EFFECTIVE_MM	   = CASE WHEN @PROCESS_TYPE=2 THEN CANCELLATION_EFFECTIVE_MM ELSE NULL END,
             @REQUESTED_BY				   = CANCELLATION_REQUESTED_BY,
             @POL_LEVEL_COMMISSION		   = POLICY_LEVEL_COMMISSION,
             @POL_LEVEL_COMM_APPLIES       = POLICY_LEVEL_COMMISSION_APPLIES,
             @CSR						   = CSRNAME,
             @PRODUCER					   = PRODUCER,
             @UNDERWRITER				   = UNDERWRITER ,
             @ENDORSEMENT_TYPE			   = ENDORSEMENT_TYPE,
             @END_APPL_NUMBER			   = END_APPLICATION_NUMBER,
             @COMMENTS					   = COMMENTS,
             @ENDORSEMENT_NUMBER		   = ENDORSEMENT_NUMBER
	  FROM   MIG_IL_POLICY_DETAILS 
	  WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND
			 IMPORT_SERIAL_NO  = @IMPORT_SERIAL_NO
			 
	 --select POLICY_CANCELLATION_TYPE from  	MIG_IL_POLICY_DETAILS where IMPORT_REQUEST_ID=	1834
	 				 
      ------------------------------------------
	  -- FOR NEW BUSINESS COMMIT
	  ------------------------------------------ 
      IF(@PROCESS_TYPE=1)
         BEGIN
         
          SET @PROCESS_ID=24
          SET @CURRENT_POLICY_STATUS  = NULL
          SET @PREVIOUS_POLICY_STATUS ='NBUS'
          SET @NEW_VERSION_ID         = @POLICY_VERSION_ID
          
          ------------------------------------------------------
          -- UPDATE POLICY DETAIL 
          ------------------------------------------------------
          UPDATE POL_CUSTOMER_POLICY_LIST
          SET   POLICY_STATUS	    = @CURRENT_POLICY_STATUS	
		  WHERE CUSTOMER_ID			= @CUSTOMER_ID AND 
				POLICY_ID			= @POLICY_ID AND
				@POLICY_VERSION_ID	= @POLICY_VERSION_ID
         
         END  
       ELSE
         BEGIN  
    
    
	    SET @LOB_ID=0       
	    SET @IS_VALID=''	
		SET	@ERROR_TYPES=''
		--SET @CUSTOMER_ID =0	  
		--SET @POLICY_ID	=0
		--SET @POLICY_VERSION_ID=0
		
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
		
				
	  IF(@IS_VALID='Y')
		  BEGIN
			 ------------------------------------------------------------
			 -- CREATE NEW VERSION OF POLICY
			 ------------------------------------------------------------
			 EXEC [Proc_PolicyCreateNewVersion]                                          
				 @CUSTOMER_ID					= @CUSTOMER_ID,                                                                
				 @POLICY_ID						= @POLICY_ID,                                                             
				 @POLICY_VERSION_ID				= @POLICY_VERSION_ID,                                                                
				 @CREATED_BY					= @CREATED_BY,            
				 @NEW_VERSION					= @NEW_VERSION_ID OUTPUT,                            		                    
				 @RENEWAL						= 0 ,   -- In case  of Renewal 1 will be passed in case of Rewrite 3 will be passed else 0                    
				 @NEW_DISP_VERSION				= NULL  ,                  
				 @TRAN_DESC						= NULL,                    
				 @INVALID_COVERAGE				= NULL ,              
				 @COVERAGE_BASE_CHANGED_BOAT_ID = NULL,  
				 @NEW_DISP_VERSION_REWRITABLE   = NULL,
				 @CALLED_FROM					= NULL 

--select @NEW_VERSION_ID as '@NEW_VERSION_ID'

			 IF(@NEW_VERSION_ID IS NOT NULL AND @NEW_VERSION_ID>0)
				 BEGIN
				 
					 ------------------------------------------------------------
					 -- POLICY CANCELLATION
					 ------------------------------------------------------------
					 IF(@PROCESS_TYPE=2)
					 BEGIN
			          
			          SET @PROCESS_ID=2
			          SET @PREVIOUS_POLICY_STATUS='NORMAL' 
			          SET @CURRENT_POLICY_STATUS='CANCEL' -- POLICY CANCELLATION IN PROGRESS
			          SET @PROCESS_STATUS=''
			          
			          ------------------------------------------------------
					  -- UPDATE POLICY DETAIL OF NEW VERSION
					  ------------------------------------------------------
					  UPDATE POL_CUSTOMER_POLICY_LIST
					  SET   POLICY_STATUS	        = @CURRENT_POLICY_STATUS,
					        POL_VER_EFFECTIVE_DATE  = @EFFETIVE_DATE ,
					        POL_VER_EXPIRATION_DATE = @EXPIRY_DATE		
					  WHERE CUSTOMER_ID				= @CUSTOMER_ID AND 
							POLICY_ID				= @POLICY_ID AND
							POLICY_VERSION_ID		= @NEW_VERSION_ID
			   
					 END -- END OF IF(@PROCESS_TYPE=2)
					 ------------------------------------------------------------
					 -- POLICY ENDOESEMENT
					 ------------------------------------------------------------
					 IF(@PROCESS_TYPE=3) 
					 BEGIN
					 
					      SET @PROCESS_ID = 3
					      SET @PREVIOUS_POLICY_STATUS ='NORMAL' 
			              SET @CURRENT_POLICY_STATUS  ='UENDRS' -- POLICY ENDORSEMENT IN PROGRESS
			              SET @PROCESS_STATUS         ='PENDING'
			              
			              -- APP STATUS= 'COMPLETE'
			              -- POLICY STATUS=UENDRS
			              --
			          SET @POL_LEVEL_COMM_APPLIES= CASE WHEN @POL_LEVEL_COMM_APPLIES NOT IN ('Y','N') THEN NULL ELSE  @POL_LEVEL_COMM_APPLIES END
			          SET @POL_LEVEL_COMMISSION = CASE WHEN @POL_LEVEL_COMM_APPLIES !='Y' THEN NULL ELSE  @POL_LEVEL_COMMISSION END
			             
			          UPDATE POL_CUSTOMER_POLICY_LIST
					  SET    POLICY_STATUS			   = @CURRENT_POLICY_STATUS,
					         APP_STATUS				   = 'COMPLETE',
					         POL_VER_EFFECTIVE_DATE    = @EFFETIVE_DATE ,
					         POL_VER_EXPIRATION_DATE   = @EXPIRY_DATE		,					       
					         PRODUCER        		   = CASE WHEN @PRODUCER IS NULL OR  @PRODUCER=0 THEN 	PRODUCER ELSE   @PRODUCER END,      
					         POLICY_LEVEL_COMM_APPLIES = CASE WHEN @POL_LEVEL_COMM_APPLIES IS NULL THEN POLICY_LEVEL_COMM_APPLIES ELSE @POL_LEVEL_COMM_APPLIES END ,
					         UNDERWRITER               = CASE WHEN @UNDERWRITER IS NULL OR  @UNDERWRITER=0 THEN UNDERWRITER ELSE   @UNDERWRITER END,
					         POLICY_LEVEL_COMISSION	   = CASE WHEN @UNDERWRITER IS NULL THEN POLICY_LEVEL_COMISSION ELSE   @POL_LEVEL_COMMISSION END      		        
					  WHERE  CUSTOMER_ID			   = @CUSTOMER_ID AND  
							 POLICY_ID				   = @POLICY_ID AND
							 POLICY_VERSION_ID		   = @NEW_VERSION_ID
							
					 END
			     
				 END -- END OF 	 IF(@NEW_VERSION_ID IS NOT NULL AND @NEW_VERSION_ID>0)
		    
           
			    --select @CANCELLATION_OPTION,@CANCELLATION_TYPE
          
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
				 @NEW_POLICY_VERSION_ID			= @NEW_VERSION_ID,                                
				 @POLICY_PREVIOUS_STATUS		= @PREVIOUS_POLICY_STATUS,                                 
				 @POLICY_CURRENT_STATUS		    = @CURRENT_POLICY_STATUS,                                
				 @PROCESS_STATUS				= @PROCESS_STATUS,
				 @CREATED_BY					= @CREATED_BY,                            
				 @CREATED_DATETIME				= @CREATED_DATE,                           
				 @COMPLETED_BY					= @CREATED_BY,                           
				 @COMPLETED_DATETIME			= @CREATED_DATE,                           
				 @COMMENTS						= @COMMENTS,                           
				 @PRINT_COMMENTS				= '',                           
				 @REQUESTED_BY					= @REQUESTED_BY,                           
				 @EFFECTIVE_DATETIME			= @EFFETIVE_DATE,                           
				 @EXPIRY_DATE					= @EXPIRY_DATE,                           
				 @CANCELLATION_OPTION			= @CANCELLATION_OPTION,                           
				 @CANCELLATION_TYPE				= @CANCELLATION_TYPE,         
				 @REASON						= @REASON,                           
				 @OTHER_REASON					= @OTHER_REASON,                           
				 @RETURN_PREMIUM				= @RETURN_PREMIUM,        
				 @PAST_DUE_PREMIUM				= null,                              
				 @ENDORSEMENT_NO				= @ENDORSEMENT_NUMBER,                              
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
				 @ENDORSEMENT_TYPE				= @ENDORSEMENT_TYPE,  
				 @ENDORSEMENT_OPTION			= null,  
				 @SOURCE_VERSION_ID				= null,  
				 @ENDORSEMENT_RE_ISSUE			= null,  
				 @CO_APPLICANT_ID				= null
				
				------------------------------------------------------------------ 
				-- FOR ENDORSEMENT PROCESS UPDATE ENDORSEMENT APPLICATION  NUMBER
				------------------------------------------------------------------
				IF( @PROCESS_TYPE=3) 
				 BEGIN
				 
				  UPDATE  POL_POLICY_PROCESS
				  SET     COINSURANCE_NUMBER = @END_APPL_NUMBER
				  WHERE   CUSTOMER_ID    = @CUSTOMER_ID   AND  
						  POLICY_ID      = @POLICY_ID     AND                          
					      POLICY_VERSION_ID = @POLICY_VERSION_ID AND
					      ROW_ID         = @ROW_ID                             
					    
				  
				 END
				
				
				 SET @POLICY_VERSION_ID=@NEW_VERSION_ID
				 
				 
--select  @CANCELLATION_OPTION,        
--		@CANCELLATION_TYPE 
		
--		SELECT * FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
				 
				 
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
        END
          
      
  
 END          
  
  ---------------------------------------------------------- UPDATE TABLE MIG_IL_POLICY_DETAILS          
-------------------------------------------------------------------------------------------------          
 
  END --IF( @PROCESS_TYPE =1 AND @ENDORSEMENT_NUMBER=0)            
       
 IF(@ERROR_NO=0 )
 BEGIN      
 
		 UPDATE  MIG_IL_POLICY_DETAILS           
		 SET    POLICY_ID   = @POLICY_ID          
			,POLICY_VERSION_ID = @POLICY_VERSION_ID          
			,CUSTOMER_ID  = @CUSTOMER_ID          
			,LOB_ID    = PRODUCT          
			,IS_PROCESSED  = 'Y'          
			,CREATED_DATETIME   = GETDATE()          
		 WHERE  IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID           
		 AND   IMPORT_SERIAL_NO = @IMPORT_SERIAL_NO                  
		       
		       
		 -- INSERT RECORD FOR NBS AND POLICY ENDORSEMENT ONLY         
		 IF @PROCESS_TYPE IN (1,2,3)   
		 BEGIN
			 ---------------------------------------------          
			 -- ADD DETAILS IN SUMMARY TABLE          
			 ---------------------------------------------          
			 EXEC [PROC_MIG_IL_INSERT_IMPORT_SUMMARY]               
				@IMPORT_REQUEST_ID      = @IMPORT_REQUEST_ID,          
				@IMPORT_SERIAL_NO       = @IMPORT_SERIAL_NO,       
				@CUSTOMER_ID      = @CUSTOMER_ID ,          
				@POLICY_ID              = @POLICY_ID,          
				@POLICY_VERSION_ID      = @POLICY_VERSION_ID,          
				@IS_ACTIVE              = 'Y',          
				@IS_PROCESSED       = 'Y',          
				@FILE_TYPE              = @IMPORT_POLICY_FILE_TYPE,          
				@FILE_NAME              = @IMPORT_FILE_NAME,          
				@CUSTOMER_SEQUENTIAL    = NULL,          
				@POLICY_SEQUENTIAL      = @LOOP_POLICY_SEQUANCE_NO,          
				@ENDORSEMENT_SEQUENTIAL = @LOOP_END_SEQUANCE_NO,  
				@IMPORT_SEQUENTIAL      = NULL,  
				@IMPORT_SEQUENTIAL2     = NULL,  
				@LOB_ID					= @LOOP_POLICY_LOB ,   
				@IMPORTED_RECORD_ID     = NULL   ,
				@PROCESS_TYPE           = @PROCESS_TYPE    
	    END    
    END		                 
   SET @COUNTER+=1            
  END    
 
  END -- WHILE(@COUNTER<=@MAX_RECORD_COUNT)     
          
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
