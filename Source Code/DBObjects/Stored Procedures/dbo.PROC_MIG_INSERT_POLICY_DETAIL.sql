IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_INSERT_POLICY_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_INSERT_POLICY_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*-- =============================================        
 AUTHOR:  ATUL KUMAR SINGH        
 CRAETE DATE: 2011-05-11        
 DESCRIPTION: CREATE NEW POLICY/LAUNCH NEW ENDORSEMENT
 IN-SCOPE:         
   INSERT CUSTOMER        
   INSERT CO-APPLICANT        
   INSERT POLICY INFORMATION        
   INSERT REMUNERATION        
   INSERT BILLING INFO DATA        
   INSERT ENDORSEMENTS        
        
  OUT-SCOPE        
   POLICY RICK INFO           
   POLICY COVERAGE INFO        
        
CLIENT ASSUMPTION        
  1: THERE BE ALWAYS SINGLE BROKER FOR A POLICY        
          
 INTERNAL ASSUMPTIONS         
        
 1:  WE DO NOT HAVE ANY INFO REGARDING CUSTOMER TYPE, WE CALCULATE IT FROM LENGTH OF CPF_VNPJ NUBER        
  IF LENGHT IS !$ THEN IT IS COMMERCIAL OTHERWISE PERSONAL        
  WE ARE NOT COVERING GOVERNMENT AS A CUSTOMER TYPE        
-- =============================================*/        
-- DROP proc [PROC_MIG_INSERT_POLICY_DETAIL] 566,1
        
CREATE PROCEDURE [dbo].[PROC_MIG_INSERT_POLICY_DETAIL]        
        
 ----------------------------------------  INPUT PARAMETER      
 @INPUT_REQUEST_ID INT,        
 @INPUT_SERIAL_ID INT        
         
        
AS        
BEGIN        
 SET NOCOUNT ON;        
    
    
  ------------------------***  START OF DECLARTION AREA *** -----------------------------------  
    
           
	 DECLARE @CUST_ID INT    --FOR CUSTOMER_ID        
     DECLARE @CUST_CODE NVARCHAR(6)  --FOR CUSTOMER_CODE         
     DECLARE @FNAME NVARCHAR(150)  --FOR CUSTOMER_FNAME         
     DECLARE @COUNT INT     --FOR COUNT VARIABLE        
     DECLARE @POLICY_NUMBER NVARCHAR(200) --FOR POLICY_NO        
            
     DECLARE @REMUNERATION_ID INT=1  --FOR REMUNERATION_ID        
            
     DECLARE @ENDORSEMENT_NUMBER_VAR INT --FOR ENDORSEMENT NUMBER        
            
     DECLARE @POLICY_ID INT    --FOR POLICY_ID        
	 DECLARE @POLICY_VERSION_ID INT  --FOR POLICY_VERSION_ID        
	 DECLARE @APP_ID INT     --FOR APP_ID        
	 DECLARE @APP_VERSION_ID INT   --FOR APP_VERSION_ID        
	 DECLARE @POLICY_DISP_VERSION NVARCHAR(6)='1.0' --FOR POLICY_DISP_VERSION        
	 DECLARE @APP_VERSION NVARCHAR(6)='1.0'--FOR APP VERSION        
	         
	         
	 DECLARE @POLICY_LOB   INT  --FOR POLICY LOB        
	 DECLARE @POLICY_SUB_LOB   INT  --FOR POLICY SUBLOB        
	        
	         
	 DECLARE @CSR  INT=565   
	 DECLARE @UNDERWRITER  INT=null        
	 DECLARE @CREATED_BY  INT=3        
	 DECLARE @INSTALL_PLAN_ID INT        
	 DECLARE @DOWN_PAYMENT_MODE INT =14558        
	 DECLARE @APPLICATION_STATUS NVARCHAR(20) ='Application'        
	 DECLARE @POLICY_STATUS NVARCHAR(20)        
	 DECLARE @CURRENT_TERM INT=1        
	 DECLARE @PAYMENT_MODE INT=11972        
	 DECLARE @RISK_TYPE NVARCHAR(20)='ARPERIL'     
	 DECLARE @DEFAULT NVARCHAR(80)= 'CO Aceito'      
     DECLARE @DEFAULT_DATE NVARCHAR(80)= '1900-01-01'     
	 DECLARE @DEFAULT_COUNTRY INT= 5   
	 DECLARE @DEFAULT_STATE INT= 87  
	 DECLARE @GENDER INT=  6615    
	 DECLARE @DEFAULT_ZIP_CODE INT=00000000  
         
	 DECLARE @COMPANY_CODE INT=1        
	 DECLARE @TOTAL_NO_OF_INSTALLMENTS INT  
	         
	      
	 DECLARE @TRAN_TYPE NVARCHAR(10)='NBS',@NET_PREMIUM FLOAT=0.0, @INTEREST FLOAT=0.0, @TAX FLOAT=0.0, @TOTAL_CHANGE_INFORCE_PRM FLOAT=0.0,@TOTAL_INFO_PRM FLOAT=0.0,@FEES FLOAT=0.0,@TOTAL_PREMIUM FLOAT=0.0        
	 DECLARE @NET_PREMIUM_TOTAL FLOAT=0.0, @INTEREST_TOTAL FLOAT=0.0, @TAX_TOTAL FLOAT=0.0,@FEES_TOTAL FLOAT=0.0         
	 DECLARE @TOATL_NO_OF_INSTALLMENT INT        
	        
	 DECLARE @DIV_ID INT=30  -- HARD CODED COZ, WE DO NOT HAVE ANY INFORMATION ABOUT LEADER BRANCH        
	 DECLARE @LEADER_ENDORSEMENT_NUMBER NVARCHAR(10)    
	   
	 DECLARE @ERROR_NUMBER    INT    
	 DECLARE @ERROR_SEVERITY  INT    
	 DECLARE @ERROR_STATE     INT    
	 DECLARE @ERROR_PROCEDURE VARCHAR(512)    
	 DECLARE @ERROR_LINE    INT    
	 DECLARE @ERROR_MESSAGE   NVARCHAR(MAX)    
	  
	 DECLARE @APP_NUMBER VARCHAR(20)        
	 DECLARE @NBS_POLICY_NUMBER  NVARCHAR(21)  
	 DECLARE @NBS_APP_NUMBER  NVARCHAR(21)   
	 DECLARE @AGENCY_ID INT=3670   
	 DECLARE  @CPF_CNPJ NVARCHAR(50)          
	 DECLARE  @OLD_APPLICANT_ID NVARCHAR(50)
   
 ------------------------***  END OF DECLARTION AREA *** -----------------------------------  
          
          
          
    ------------------------***  SET VALUE IN VARIABLES *** -----------------------------------    
         
	 SET @POLICY_ID=1        
	 SET @POLICY_VERSION_ID=1        
	 SET @APP_ID =1        
	 SET @APP_VERSION_ID =1       
   
  ------------------------***  end SET VALUE IN VARIABLES *** -----------------------------------    
    
  
  
  
 BEGIN TRY      
         
 ----------------------------------- TRANSFER DATA FROM MAIN TABLE TO TEMP TABLE FOR OPTIMIZATION PURPOSE    
   
       
	  SELECT		* INTO		#MIG_CUSTOMER_POLICY_LIST         
	  FROM						MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
	  WHERE						IMPORT_SERIAL_NO	 =	@INPUT_SERIAL_ID        
	  AND						IMPORT_REQUEST_ID    =  @INPUT_REQUEST_ID        
    
  
  -------------------------------- ***  SETTING OF LOB AND SUBLOB *** --------------------------------------  
        
	  SELECT		  TOP 1 
								@POLICY_LOB=MLM.LOB_ID,@POLICY_SUB_LOB=MSLM.SUB_LOB_ID FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK) MCPL        
	  LEFT OUTER JOIN         
	  MIG_POLICY_LOB_MAPPING (NOLOCK) MPLM        
	  ON MPLM.LEADER_SUSEP_LOB_CODE=MCPL.POLICY_LOB AND MPLM.LEADER_SUSEP_SUB_LOB_CODE=MCPL.POLICY_SUBLOB        
	  LEFT OUTER JOIN        
	  MNT_LOB_MASTER (NOLOCK) MLM        
	  ON CONVERT(INT,MLM.SUSEP_LOB_CODE)=MPLM.ALBA_SUSEP_LOB_CODE        
	  LEFT OUTER JOIN MNT_SUB_LOB_MASTER (NOLOCK) MSLM        
	  ON (MSLM.SUSEP_CODE=MPLM.ALBA_SUSEP_SUB_LOB_CODE AND MLM.LOB_ID=MSLM.LOB_ID)        
	  WHERE MLM.IS_ACTIVE='Y'        
          
    
    -------------------------------- ***  SETTING OF AGENCY_ID *** --------------------------------------    
         
  SELECT TOP 1   @AGENCY_ID=  ISNULL(MAL.AGENCY_ID,3670)  
  FROM    #MIG_CUSTOMER_POLICY_LIST (NOLOCK) MCPL        
  LEFT OUTER JOIN MNT_AGENCY_LIST (NOLOCK) MAL        
  ON     ISNULL(MAL.SUSEP_NUMBER,0) = CAST(MCPL.BROKER_CODE AS NVARCHAR(20))
         
    
 -------------------------------- ***  SETTING CPF AND APPLICANT FIRST NAME *** --------------------------------------    
         
 SELECT  @FNAME=LTRIM(RTRIM(APPLICANT_NAME)),@CPF_CNPJ=LTRIM(RTRIM(CPF_CNPJ))        
    ,@ENDORSEMENT_NUMBER_VAR=(CONVERT(INT,LEADER_ENDORSEMENT_NUMBER)+1)        
    ,@LEADER_ENDORSEMENT_NUMBER=CONVERT(INT,LEADER_ENDORSEMENT_NUMBER)  
    ,@POLICY_NUMBER=LEADER_POLICY_NUMBER        
 FROM  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)         
   
   
          
 /* CHECK IF POLICY NUMBER ALREADY EXISTS WITH SAME ENDORSEMENT NUMBER        
           SCENERIO 1: IF POLICY NUMBER DOES NOT EXISTS         
       THEN PROCESS POLICY        
     SCENERIO 2: IF POLICY NUMBER ALREADY EXISTS WITH SAME ENDORSEMENT NUMBER (NO MATTER COMMITED OR NOT)        
       THEN RETURN -2        
     SCENERIO 3: IF POLICY NUMBER ALREADY EXISTS WITH PREVOIUS ENDORSEMENT NUMBER        
       THEN PROCESS RECORD RETURN 1 AFTER SUCCESS        
     SCENERIO 4: IF POLICY NUMBER ALREADY EXISTS WITH ADVANCE VERSION OF ENDORSEMENT NUMBER (NO MATTER COMMITED OR NOT)        
       THEN RETURN -3          
     SCENERIO 5: IF IMMEDIATE PREVOUS VERSION IS COMMITED THEN DATA PROCESS OTHERWISE        
        RETURN -4               
 */        
         
        CREATE TABLE #BILLING_DETAILS_TEMP        
           (        
     INSTALLMENT_NO INT         
    ,LEADER_POLICY_NUMBER INT        
    ,LEADER_ENDORSEMENT_NUMBER INT        
    ,INSTALLMENT_AMOUNT DECIMAL(15,4)
    ,DISCOUNT_AMOUNT_PER_INSTALLMENT DECIMAL(15,4)      
    ,INTREST_AMOUNT_PER_INSTALLMENT DECIMAL(15,4)        
    ,INSTALLMENT_DUE_DATE DATETIME        
           )        
                   
           INSERT INTO #BILLING_DETAILS_TEMP        
           ( INSTALLMENT_NO        
     ,LEADER_POLICY_NUMBER         
    ,LEADER_ENDORSEMENT_NUMBER         
    ,INSTALLMENT_AMOUNT         
    ,INTREST_AMOUNT_PER_INSTALLMENT  
    ,DISCOUNT_AMOUNT_PER_INSTALLMENT      
    ,INSTALLMENT_DUE_DATE        
           )        
                   
           SELECT   1        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT1        
     ,INTREST_AMOUNT_PER_INSTALLMENT1    
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT1          
     ,INSTALLMENT_DUE_DATE1        
           FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)         
                
           UNION ALL        
           SELECT   2        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT2        
     ,INTREST_AMOUNT_PER_INSTALLMENT2        
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT2
     ,INSTALLMENT_DUE_DATE2        
           FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT 3        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT3        
     ,INTREST_AMOUNT_PER_INSTALLMENT3  
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT3      
     ,INSTALLMENT_DUE_DATE3        
          FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT   4        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT4        
     ,INTREST_AMOUNT_PER_INSTALLMENT4 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT4       
     ,INSTALLMENT_DUE_DATE4        
           FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
            UNION ALL        
   SELECT  5        
     ,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT5        
     ,INTREST_AMOUNT_PER_INSTALLMENT5 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT5       
     ,INSTALLMENT_DUE_DATE5        
         FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT   6        
     ,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT6        
     ,INTREST_AMOUNT_PER_INSTALLMENT6 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT6       
     ,INSTALLMENT_DUE_DATE6        
          FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
            UNION ALL        
            SELECT   7        
     ,LEADER_POLICY_NUMBER     
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT7        
     ,INTREST_AMOUNT_PER_INSTALLMENT7 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT7       
     ,INSTALLMENT_DUE_DATE7        
         FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT    8        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT8        
     ,INTREST_AMOUNT_PER_INSTALLMENT8 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT8       
     ,INSTALLMENT_DUE_DATE8        
       FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT    9        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT9        
     ,INTREST_AMOUNT_PER_INSTALLMENT9 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT9       
     ,INSTALLMENT_DUE_DATE9        
        FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT    10        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT10        
     ,INTREST_AMOUNT_PER_INSTALLMENT10
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT10        
  ,INSTALLMENT_DUE_DATE10        
          FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT    11        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT11        
     ,INTREST_AMOUNT_PER_INSTALLMENT11 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT11       
     ,INSTALLMENT_DUE_DATE11        
          FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           UNION ALL        
           SELECT    12        
     ,LEADER_POLICY_NUMBER        
     ,LEADER_ENDORSEMENT_NUMBER        
     ,INSTALLMENT_AMOUNT12        
     ,INTREST_AMOUNT_PER_INSTALLMENT12 
     ,DISCOUNT_AMOUNT_PER_INSTALLMENT12       
     ,INSTALLMENT_DUE_DATE12        
          FROM      #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
         
  
        
   
   SELECT    @TOTAL_NO_OF_INSTALLMENTS= COUNT(1)  
      FROM      #BILLING_DETAILS_TEMP (NOLOCK)        
      WHERE  INSTALLMENT_AMOUNT<>0.00  
        
      ------------------   SELECT BILLING PLAN  
      
      ------------------ TRANSACTION TYPE
      
      DECLARE @TRANSACTION_TYPE INT
      SELECT @TRANSACTION_TYPE=TRANSACTION_TYPE FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK)
      
         
			IF(@TRANSACTION_TYPE=14 )  
				BEGIN  
				SET @INSTALL_PLAN_ID=76   
				 
				END  
			ELSE  
			    BEGIN  
			     SELECT @INSTALL_PLAN_ID=IDEN_PLAN_ID from ACT_INSTALL_PLAN_DETAIL (NOLOCK) WHERE NO_OF_PAYMENTS=@TOTAL_NO_OF_INSTALLMENTS  
				 AND PLAN_CODE like '0%'   
				 END  
        
        
         
         
         
 SET @POLICY_ID=1        
        
 SET @APP_ID =1        
 SET @APP_VERSION_ID =@ENDORSEMENT_NUMBER_VAR        
 SET @POLICY_DISP_VERSION =1.0        
         
         
          
  --IF((SELECT TRANSACTION_TYPE FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK))=14) --14560        
  --SET @INSTALL_PLAN_ID=84        
  --ELSE        
  --SET @INSTALL_PLAN_ID=80        
          
  SET   @CUST_CODE=(SELECT LEFT(@FNAME,4)+CONVERT(NVARCHAR,CAST(RAND() *99 AS INT)))        
           
          
          
          
         
          
 --***************************************************************************        
 -- INSERT INTO CLT_CUSTOMER_LIST TABLE        
 --***************************************************************************        
         
 --         
 --BEGIN TRY        
 --BEGIN TRAN T1        
         
 --********* CHECK FOR CUSTOMER EXISTENCE *********        
 --  a): CHECK WITH CPF_CNPJ NO IN [CLT_CUSTOMER_LIST]        
 --  b): IF EXISTS DO NOT INSERT CUSTOMER IN CLT_CULSTOMER_LIST        
 --  b.1) FIND OUT THE MAXIMUM POLICY_ID FOR THIS CUSTOMER        
 --  b.2) INCREASE MAX POLICY_ID WITH 1        
 --  b.3) FIND OUT THE MAXIMUM APP_ID FOR THIS CUSTOMER         
 --  b.4) INCREASE MAX APP_ID WITH 1        
 --***********************END*************************        
         
         
         
 IF  EXISTS (        
     SELECT 1 FROM [CLT_CUSTOMER_LIST] (NOLOCK)     clt   
     join POL_CUSTOMER_POLICY_LIST (nolock) pcpl  
     on clt.CUSTOMER_ID=pcpl.CUSTOMER_ID  
     WHERE    
       
     CPF_CNPJ   = @CPF_CNPJ    
    AND CUSTOMER_FIRST_NAME  =@FNAME      
    )        
  BEGIN        
    
  SELECT TOP 1 @CUST_ID=customer_id FROM [CLT_CUSTOMER_LIST] (NOLOCK)        
     WHERE  ltrim(rtrim(CPF_CNPJ)) =ltrim(rtrim(@CPF_CNPJ)) AND ltrim(rtrim(CUSTOMER_FIRST_NAME))=ltrim(rtrim(@FNAME))  
     
   SELECT  @POLICY_ID=(ISNULL(MAX(POLICY_ID),0)+1) , @APP_ID=(ISNULL(MAX(APP_ID),0)+1)         
   FROM  POL_CUSTOMER_POLICY_LIST (NOLOCK)        
   WHERE  CUSTOMER_ID=@CUST_ID     
   
  END        
         
 ELSE        
  BEGIN        
  INSERT INTO [dbo].[CLT_CUSTOMER_LIST]        
      (        
      [CUSTOMER_CODE]        
      ,CUSTOMER_TYPE        
      --,[CUSTOMER_PARENT]        
      --,[CUSTOMER_SUFFIX]        
      ,[CUSTOMER_FIRST_NAME]        
      --,[CUSTOMER_MIDDLE_NAME]        
      --,[CUSTOMER_LAST_NAME]        
      --,[CUSTOMER_ADDRESS1]        
      --,[CUSTOMER_ADDRESS2]        
      --,[CUSTOMER_CITY]        
      --,[CUSTOMER_COUNTRY]        
      --,[CUSTOMER_STATE]        
      --,[CUSTOMER_ZIP]        
      --,[CUSTOMER_BUSINESS_TYPE]        
     -- ,[CUSTOMER_BUSINESS_DESC]        
     -- ,[CUSTOMER_CONTACT_NAME]        
      --,[CUSTOMER_BUSINESS_PHONE]        
      --,[CUSTOMER_EXT]        
      --,[CUSTOMER_HOME_PHONE]        
      --,[CUSTOMER_MOBILE]        
      --,[CUSTOMER_FAX]        
      --,[CUSTOMER_PAGER_NO]        
      --,[CUSTOMER_Email]        
     -- ,[CUSTOMER_WEBSITE]        
     -- ,[CUSTOMER_INSURANCE_SCORE]        
      --,[CUSTOMER_INSURANCE_RECEIVED_DATE]        
      --,[CUSTOMER_REASON_CODE]        
      --,[CUSTOMER_LATE_CHARGES]        
     -- ,[CUSTOMER_LATE_NOTICES]        
     -- ,[CUSTOMER_SEND_STATEMENT]        
      --,[CUSTOMER_RECEIVABLE_DUE_DAYS]        
      ,[CUSTOMER_AGENCY_ID]        
      ,[IS_ACTIVE]        
      ,[CREATED_BY]        
      ,[CREATED_DATETIME]        
     -- ,[MODIFIED_BY]        
      --,[LAST_UPDATED_DATETIME]        
      --,[CUSTOMER_ATTENTION_NOTE]        
      --,[PREFIX]        
      --,[CUSTOMER_REASON_CODE2]        
      --,[CUSTOMER_REASON_CODE3]        
      --,[CUSTOMER_REASON_CODE4]        
      --,[ATTENTION_NOTE_UPDATED]        
     -- ,[IS_HOME_EMPLOYEE]        
      --,[LAST_INSURANCE_SCORE_FETCHED]        
      --,[APPLICANT_OCCU]        
     -- ,[EMPLOYER_NAME]        
     -- ,[EMPLOYER_ADDRESS]        
     -- ,[YEARS_WITH_CURR_EMPL]        
     -- ,[SSN_NO]        
    --  ,[MARITAL_STATUS]        
    --,[DATE_OF_BIRTH]        
      --,[DESC_APPLICANT_OCCU]        
      --,[LAST_MVR_SCORE_FETCHED]        
      --,[EMPLOYER_ADD1]        
      --,[EMPLOYER_ADD2]        
      --,[EMPLOYER_CITY]        
      --,[EMPLOYER_COUNTRY]        
      --,[EMPLOYER_STATE]        
      --,[EMPLOYER_ZIPCODE]        
      --,[EMPLOYER_HOMEPHONE]        
      --,[EMPLOYER_EMAIL]        
      --,[YEARS_WITH_CURR_OCCU]        
      --,[GENDER]        
      --,[PER_CUST_MOBILE]        
      --,[EMP_EXT]        
      --,[PRIORINFO_ORDERED]        
      ,[CPF_CNPJ]        
      --,[NUMBER]        
      --,[COMPLIMENT]        
      --,[DISTRICT]        
      --,[BROKER]        
      --,[MAIN_TITLE]        
      --,[MAIN_POSITION]        
      --,[MAIN_CPF_CNPJ]        
      --,[MAIN_ADDRESS]        
      --,[MAIN_NUMBER]        
      --,[MAIN_COMPLIMENT]        
      --,[MAIN_DISTRICT]        
      --,[MAIN_NOTE]        
      --,[MAIN_CONTACT_CODE]        
      --,[REGIONAL_IDENTIFICATION]        
      --,[REG_ID_ISSUE]        
      --,[ORIGINAL_ISSUE]        
     -- ,[MAIN_CITY]        
     -- ,[MAIN_STATE]        
     -- ,[MAIN_COUNTRY]        
     -- ,[MAIN_ZIPCODE]        
     -- ,[MAIN_FIRST_NAME]        
     -- ,[MAIN_MIDDLE_NAME]        
     -- ,[MAIN_LAST_NAME]     
      ,ACC_COI_FLAG      
      )        
          
  SELECT @CUST_CODE        
   ,CASE WHEN  LEN(LTRIM(RTRIM(@CPF_CNPJ)))>11        
      THEN  '11109'        
     ELSE   '11110'        
    END             
      ,@FNAME        
      ,@AGENCY_ID        
      ,'Y'        
      ,@CREATED_BY        
      ,GETDATE()        
      ,@CPF_CNPJ        
      ,'A'  
            
            
          
          
             
  END      
      
   IF(@CUST_ID IS NULL)       
   BEGIN  
  SET @CUST_ID=(SELECT MAX(CUSTOMER_ID) FROM dbo.CLT_CUSTOMER_LIST (NOLOCK))        
  END  
   
   
  DECLARE @APPLICANT_ID INT        
  IF(@LEADER_ENDORSEMENT_NUMBER=0)  
  begin  
  SELECT @APPLICANT_ID=(ISNULL(MAX(APPLICANT_ID),0)+1) FROM CLT_APPLICANT_LIST (NOLOCK)        
  end  
  --else   
  --begin  
  --SELECT @APPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST (NOLOCK)        
  --end  
  DECLARE @OLD_POLICY_VERSION INT     
   IF(@LEADER_ENDORSEMENT_NUMBER>0)  
  BEGIN  
   
    SELECT   
    @POLICY_ID=MAX(P.POLICY_ID),  
    @POLICY_VERSION_ID= MAX( P.POLICY_VERSION_ID) ,  
    @CUST_ID = MAX( P.CUSTOMER_ID),  
    @POLICY_DISP_VERSION= MAX(P.POLICY_DISP_VERSION),  
    @UNDERWRITER=MAX(P.UNDERWRITER)  
      
             FROM  POL_CO_INSURANCE  C INNER JOIN      
                   POL_CUSTOMER_POLICY_LIST P ON C.CUSTOMER_ID=P.CUSTOMER_ID AND C.POLICY_ID=P.POLICY_ID AND C.POLICY_VERSION_ID=P.POLICY_VERSION_ID      
             WHERE LEADER_POLICY_NUMBER=@POLICY_NUMBER AND C.LEADER_FOLLOWER=14548  
             
     ---------------------------  LAUNCH ENDORSEMNET----------------------------  
   
   
     exec [Proc_PolicyCreateNewVersion] @CUST_ID,@POLICY_ID,@POLICY_VERSION_ID,@CREATED_BY,NULL,0,NULL,NULL,NUll,NULL,NULL,NULL  
   
     ------*********************************************************--------------  
      
     ----------------------SET APPLICANT ID-----------------------------------------------------
     SELECT @APPLICANT_ID=APPLICANT_ID FROM POL_APPLICANT_LIST (NOLOCK) WHERE CUSTOMER_ID=@CUST_ID 
     AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
     ------------------------------------------------------------------------------   
            
             SELECT @NBS_APP_NUMBER=APP_NUMBER,@NBS_POLICY_NUMBER=POLICY_NUMBER  
             FROM    
                   POL_CUSTOMER_POLICY_LIST  (NOLOCK)  
             WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
             SET @OLD_POLICY_VERSION=@POLICY_VERSION_ID  
             SET @POLICY_VERSION_ID=(@POLICY_VERSION_ID+1)  
             SET @POLICY_DISP_VERSION=(CAST(@POLICY_DISP_VERSION as float)+0.1)  
               
               
              
   
   
  END       
  ELSE  
  BEGIN          
    INSERT INTO [dbo].[CLT_APPLICANT_LIST]        
        (        
      [APPLICANT_ID]        
        ,[CUSTOMER_ID]        
        --,[TITLE]        
        --,[SUFFIX]        
        ,[FIRST_NAME]        
        --,[MIDDLE_NAME]        
        --,[LAST_NAME]        
        ,[ADDRESS1]        
       ,[ADDRESS2]        
        --,[CITY]     
        ,[COUNTRY]        
        ,[STATE]        
        ,[ZIP_CODE]        
        --,[PHONE]        
        --,[EMAIL]        
        ,[IS_ACTIVE]        
        ,[CREATED_BY]        
        --,[MODIFIED_BY]        
        ,[CREATED_DATETIME]        
        --,[LAST_UPDATED_TIME]        
        --,[CO_APPLI_OCCU]        
        --,[CO_APPLI_EMPL_NAME]        
        --,[CO_APPLI_EMPL_ADDRESS]        
        --,[CO_APPLI_YEARS_WITH_CURR_EMPL]        
        --,[CO_APPL_YEAR_CURR_OCCU]        
        --,[CO_APPL_MARITAL_STATUS]        
        ,[CO_APPL_DOB]        
        --,[CO_APPL_SSN_NO]        
        ,[IS_PRIMARY_APPLICANT]        
        --,[DESC_CO_APPLI_OCCU]        
        --,[BUSINESS_PHONE]        
        --,[MOBILE]        
        --,[EXT]        
        --,[CO_APPLI_EMPL_CITY]        
--,[CO_APPLI_EMPL_COUNTRY]        
        --,[CO_APPLI_EMPL_STATE]        
        --,[CO_APPLI_EMPL_ZIP_CODE]        
        --,[CO_APPLI_EMPL_PHONE]        
        --,[CO_APPLI_EMPL_EMAIL]        
        --,[CO_APPLI_EMPL_ADDRESS1]        
        --,[PER_CUST_MOBILE]        
        --,[EMP_EXT]        
        ,[CO_APPL_GENDER]        
        --,[CO_APPL_RELATIONSHIP]        
        --,[POSITION]        
        ,[CONTACT_CODE]        
        --,[ID_TYPE]        
        --,[ID_TYPE_NUMBER]        
       -- ,[NUMBER]        
        ,[COMPLIMENT]        
        --,[DISTRICT]        
        --,[NOTE]        
        ,[REGIONAL_IDENTIFICATION]        
        ,[REG_ID_ISSUE]        
        ,[ORIGINAL_ISSUE]        
        --,[FAX]        
        ,[CPF_CNPJ]        
        ,[APPLICANT_TYPE]        
        --,[BANK_NAME]        
        --,[BANK_NUMBER]        
        --,[BANK_BRANCH]        
        --,[ACCOUNT_NUMBER]        
        --,[ACCOUNT_TYPE]        
        )        
     SELECT         
      @APPLICANT_ID        
      ,@CUST_ID        
      ,@FNAME   
       ,@DEFAULT     
       ,@DEFAULT  
        --,[CITY]        
        ,@DEFAULT_COUNTRY  
        ,@DEFAULT_STATE  
        ,@DEFAULT_ZIP_CODE        
      ,'Y'        
        ,@CREATED_BY        
        ,GETDATE()     
        ,@DEFAULT_DATE     
        ,1       
        ,@GENDER  
        ,@CUST_CODE  
        ,@DEFAULT  
        ,@DEFAULT  
        ,@DEFAULT_DATE  
        ,@DEFAULT  
          
        ,@CPF_CNPJ        
                
    ,CASE WHEN  LEN(LTRIM(RTRIM(@CPF_CNPJ)))>11        
      THEN  '11109'        
     ELSE   '11110'        
    END        
      
  END  
              
        
         
 DECLARE @DEPT_ID INT        
 DECLARE @PC_ID INT        
 DECLARE @CARRIER_BRANCH_ID VARCHAR(30)='30'  
         
    SELECT @DIV_ID=DIV_ID FROM MNT_DIV_LIST (NOLOCK)  
 WHERE DIV_CODE=@CARRIER_BRANCH_ID  
   
 SELECT @DEPT_ID=DEPT_ID FROM MNT_DIV_DEPT_MAPPING (NOLOCK)        
 WHERE DIV_ID=@DIV_ID        
         
 SELECT @PC_ID=PC_ID FROM MNT_DEPT_PC_MAPPING (NOLOCK)        
 WHERE DEPT_ID=@DEPT_ID        
  
 -----------------------*****  SET APPLICATION NUMBER  ***-----------------------------  
         
 SET @APP_NUMBER=(SELECT dbo.func_GENERATE_APP_NUMBER_MIG(@POLICY_LOB,@AGENCY_ID))       
   
 -----------------------------------------------------------------------------------------  
 -----------------------  INSERT INTO  [POL_CUSTOMER_POLICY_LIST] ------------------------  
 -----------------------------------------------------------------------------------------  
  
 IF(@LEADER_ENDORSEMENT_NUMBER=0)  
 BEGIN  

  INSERT INTO [dbo].[POL_CUSTOMER_POLICY_LIST]        
           ([CUSTOMER_ID]         
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[APP_ID]        
           ,[APP_VERSION_ID]        
           ,[POLICY_TYPE]        
           ,[POLICY_NUMBER]        
           ,[POLICY_DISP_VERSION]        
           ,[POLICY_STATUS]        
           ,[POLICY_LOB]        
           ,[POLICY_SUBLOB]        
           ,[POLICY_DESCRIPTION]        
           --,[ACCOUNT_EXEC]        
           ,[CSR]                       
           ,[UNDERWRITER]        
           --,[PROCESS_STATUS]        
           --,[IS_UNDER_CONFIRMATION]        
           --,[LAST_PROCESS]        
          -- ,[LAST_PROCESS_COMPLETED]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           --,[MODIFIED_BY]        
           --,[LAST_UPDATED_DATETIME]        
           --,[POLICY_ACCOUNT_STATUS]        
           ,[AGENCY_ID]                --
           ,[PARENT_APP_VERSION_ID]        
           ,[APP_STATUS]        
           ,[APP_NUMBER]        
           ,[APP_VERSION]        
          ,[APP_TERMS]        
          ,[APP_INCEPTION_DATE]        
          ,[APP_EFFECTIVE_DATE]        
          ,[APP_EXPIRATION_DATE]        
          -- ,[IS_UNDER_REVIEW]        
           ,[COUNTRY_ID]        
           ,[STATE_ID]        
         ,[DIV_ID]        
         ,[DEPT_ID]        
          ,[PC_ID]        
           ,[BILL_TYPE]        
           ,[COMPLETE_APP]        
          ,[INSTALL_PLAN_ID]        
           --,[CHARGE_OFF_PRMIUM]        
           ,[RECEIVED_PRMIUM]        
           ,[PROXY_SIGN_OBTAINED]        
           --,[SHOW_QUOTE]        
           --,[APP_VERIFICATION_XML]        
           ,[YEAR_AT_CURR_RESI]        
           --,[YEARS_AT_PREV_ADD]        
     --,[POLICY_TERMS]  
      ,[POLICY_EFFECTIVE_DATE]        
      ,[POLICY_EXPIRATION_DATE]        
           --,[POLICY_STATUS_CODE]        
           --,[SEND_RENEWAL_DIARY_REM]        
           --,[TO_BE_AUTO_RENEWED]        
           --,[POLICY_PREMIUM_XML]        
           --,[MVR_WIN_SERVICE]        
           --,[ALL_DATA_VALID]        
           --,[PIC_OF_LOC]        
           ,[PROPRTY_INSP_CREDIT]        
           ,[BILL_TYPE_ID]        
           ,[IS_HOME_EMP]        
           --,[RULE_INPUT_XML]        
           ,[POL_VER_EFFECTIVE_DATE]        
           ,[POL_VER_EXPIRATION_DATE]        
           ,[APPLY_INSURANCE_SCORE]        
           --,[DWELLING_ID]        
           --,[ADD_INT_ID]        
           ,[PRODUCER]        
          ,[DOWN_PAY_MODE]        
          ,[CURRENT_TERM]        
           --,[NOT_RENEW]        
           --,[NOT_RENEW_REASON]        
           --,[REFER_UNDERWRITER]        
           --,[REFERAL_INSTRUCTIONS]        
           --,[REINS_SPECIAL_ACPT]        
          ,[FROM_AS400]        
           --,[CUSTOMER_REASON_CODE]        
           --,[CUSTOMER_REASON_CODE2]        
           --,[CUSTOMER_REASON_CODE3]        
           --,[CUSTOMER_REASON_CODE4]        
           --,[IS_REWRITE_POLICY]        
           --,[IS_YEAR_WITH_WOL_UPDATED]        
           ,[POLICY_CURRENCY]        
           ,[POLICY_LEVEL_COMISSION]        
           --,[BILLTO]        
           ,[PAYOR]        
          ,[CO_INSURANCE]        
           ,[CONTACT_PERSON]        
          ,[TRANSACTION_TYPE]        
           ,[PREFERENCE_DAY]        
           ,[BROKER_REQUEST_NO]        
          ,[POLICY_LEVEL_COMM_APPLIES]        
           --,[BROKER_COMM_FIRST_INSTM]        
         -- ,[OLD_POLICY_NUMBER]        
           ,[APP_SUBMITTED_DATE]        
           --,[POLICY_VERIFY_DIGIT]       
            
             
           )        
        SELECT                        
    @CUST_ID,                 
    @POLICY_ID,        
    @POLICY_VERSION_ID,        
    @APP_ID,        
    @APP_VERSION_ID,     
    0     
    ,@NBS_POLICY_NUMBER      
    ,@POLICY_DISP_VERSION     
	,NULL   	-- POLICY_STATUS  				--,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
													--THEn 'UISSUE'    
													--ELSE 'UENDRS'   
													--END      
    ,@POLICY_LOB        
    ,@POLICY_SUB_LOB     
    ,POLICY_SPECIFICATION     
    ,@CSR        
    ,0,											--@UNDERWRITER,        
    'Y',        
    @CREATED_BY ,        
    GETDATE(),                
    @AGENCY_ID,                
    0,      -- DEFAULT VALUE  --PARENT_APP_NO      
    CASE WHEN  @LEADER_ENDORSEMENT_NUMBER=0    --APPLICATION_STATUS
    THEN @APPLICATION_STATUS  
    ELSE 'COMPLETE'  
    END 
  
    ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  ---APP_NUMBER  
    THEn @APP_NUMBER  
    ELSE @NBS_APP_NUMBER  
    END,   
    --,        
     @APP_VERSION,        --APP_VERSION
   DATEDIFF(DD,ENDORSEMENT_EFFECTIVE_DATE,ENDORSEMENT_EXPIRE_DATE),       --[APP_TERMS]         
    ENDORSEMENT_COMMIT_DATE,											 -- [APP_INCEPTION_DATE]     
    ENDORSEMENT_EFFECTIVE_DATE,											--  [APP_EFFECTIVE_DATE]     
    ENDORSEMENT_EXPIRE_DATE,											--  [APP_EXPIRATION_DATE]    
    1,       -- Assuming default    -- ,[COUNTRY_ID]            
    0,       -- Assuming default     ,[STATE_ID]            
     @DIV_ID,                       --[DIV_ID]          
     @DEPT_ID,						--[DEPT_ID]        
     @PC_ID,						--,[PC_ID]        
     'DB',      --BILL TYPE  Assumin  g Default  
      '',		-- COMPLETED_APP     
     @INSTALL_PLAN_ID,				--[INSTALL_PLAN_ID]       
     0.00,      -- Assuming default     --[RECEIVED_PRMIUM]          
     0,      -- Assuming default		 --[PROXY_SIGN_OBTAINED]     
     0,      -- Assuming default        --[YEAR_AT_CURR_RESI]   
    -- 0,      -- Assuming default  
     --DATEDIFF(DD,A.EFFECTIVE_DATE,A.EXPIRE_DATE),        
    ENDORSEMENT_EFFECTIVE_DATE,   --[POLICY_EFFECTIVE_DATE]           
    ENDORSEMENT_EXPIRE_DATE,  --  --[POLICY_EXPIRATION_DATE]  
    0,					--[PROPRTY_INSP_CREDIT]         
    8460,       -- Assuming default				  --[BILL_TYPE_ID]       
    0,			-- Assuming default				  --[IS_HOME_EMP]        
             
      ENDORSEMENT_EFFECTIVE_DATE,        --[POL_VER_EFFECTIVE_DATE]       
    ENDORSEMENT_EXPIRE_DATE,			 --[POL_VER_EXPIRATION_DATE]      
    -1,      -- Assuming default		 [APPLY_INSURANCE_SCORE]        
      0,								--[PRODUCER]       
     @DOWN_PAYMENT_MODE,         --[DOWN_PAY_MODE] 
     @CURRENT_TERM,				-- [CURRENT_TERM]  
     'A',  -- HERE   A MEANS THIS RECORD IS COPIED FROM ACCEPTED COINSURANCE  , 
     2,								--	[POLICY_CURRENCY]               						
     (ISNULL((ISNULL(TOTAL_COMMISSION_PERCENTAGE,0)-ISNULL(COINSURANCE_COMMISSION,0)),0)),     --				[POLICY_LEVEL_COMISSION]   
    14542,      -- Assuming default                  --,[PAYOR]        
    14549,--CO InSURENCE TYPE						--  [CO_INSURANCE]        
    0,       -- Assuming default					--  ,[CONTACT_PERSON]        
    CASE WHEN (TRANSACTION_TYPE=12)  THEn '14559'    -- [TRANSACTION_TYPE]           
    WHEN (TRANSACTION_TYPE=14) THEN '14560'			  
    WHEN (TRANSACTION_TYPE=15) THEN '14561'			     
    WHEN (TRANSACTION_TYPE=13) THEN '14679'			     
    ELSE '14559'        
    END        
    ,0       -- Assuming default             ,[PREFERENCE_DAY]        
    ,0			--[BROKER_REQUEST_NO]		 ,[BROKER_REQUEST_NO]     
      ,CASE WHEN (ISNULL((ISNULL(TOTAL_COMMISSION_PERCENTAGE,0)-ISNULL(COINSURANCE_COMMISSION,0)),0))>0.00  -- [POLICY_LEVEL_COMM_APPLIES]  
    THEN 'Y'      
    ELSE 'N'  
    END    
    --, @POLICY_NUMBER        
    
   ,NULL -- [APP_SUBMITTED_DATE]           
     
            
  FROM    #MIG_CUSTOMER_POLICY_LIST (NOLOCK)         
    
    END     
    ELSE  
    BEGIN  
  UPDATE		[dbo].[POL_CUSTOMER_POLICY_LIST]        
  SET			[POLICY_DESCRIPTION]		=		POLICY_SPECIFICATION,          
                [POL_VER_EFFECTIVE_DATE]	=		ENDORSEMENT_EFFECTIVE_DATE,  
				[POL_VER_EXPIRATION_DATE]   =		ENDORSEMENT_EXPIRE_DATE,        
				[FROM_AS400]				=		'A',        
				[POLICY_LEVEL_COMISSION]	=		(ISNULL((ISNULL(TOTAL_COMMISSION_PERCENTAGE,0)-ISNULL(COINSURANCE_COMMISSION,0)),0)),            
				[POLICY_LEVEL_COMM_APPLIES] =		CASE WHEN (ISNULL((ISNULL(TOTAL_COMMISSION_PERCENTAGE,0)-ISNULL(COINSURANCE_COMMISSION,0)),0))>0.00  
													THEN 'Y'    ELSE 'N' END,
				POLICY_STATUS				=		'UENDRS',    
				APP_STATUS					=		'COMPLETE'
            
        FROM  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)    
        WHERE  [POL_CUSTOMER_POLICY_LIST].CUSTOMER_ID   = @CUST_ID   
        AND   [POL_CUSTOMER_POLICY_LIST].POLICY_ID   = @POLICY_ID  
        AND   [POL_CUSTOMER_POLICY_LIST].POLICY_VERSION_ID = @POLICY_VERSION_ID  
     END  
             
          
    ---------------------------***  FOR UNDERWRITER   ***-------------------------------------  
    ------------------------------------------------------------------------------------------  
  IF(@LEADER_ENDORSEMENT_NUMBER=0)  
  BEGIN  
  EXEC [Proc_AssignUnderwriterToCustomer] @CUST_ID,@APP_ID,1,@POLICY_LOB,NULL,NULL,NULL,NULL,NULL,NULL  
  END  
   
   
   
       
 ---------------------------------------------------------------------------------------------  
 -----------------------------------*** INSERT INTO POL_POLICY_PROCESS  ***-------------------  
 ---------------------------------------------------------------------------------------------  
           
           
      DECLARE @ROW_ID INT  
      SELECT @ROW_ID =(ISNULL(MAX(ROW_ID),0)+1) FROM [POL_POLICY_PROCESS] (NOLOCK)  
      WHERE CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID-1  
    
    IF(@LEADER_ENDORSEMENT_NUMBER>0)    
   BEGIN
	INSERT INTO [dbo].[POL_POLICY_PROCESS]  
           ([CUSTOMER_ID]  
           ,[POLICY_ID]  
           ,[POLICY_VERSION_ID]  
           ,[ROW_ID]  
           ,[PROCESS_ID]  
           ,[PROCESS_TYPE] -- BLANK ''  
           ,[NEW_CUSTOMER_ID]  
           ,[NEW_POLICY_ID]  
           ,[NEW_POLICY_VERSION_ID]  
           ,[POLICY_PREVIOUS_STATUS] -- NORMAL AND SUSPENDED  
           ,[POLICY_CURRENT_STATUS] --NORMAL  
           ,[PROCESS_STATUS]  -- PENDING  
           ,[CREATED_BY]  
           ,[CREATED_DATETIME]  
           ,[COMPLETED_BY]  
           ,[COMPLETED_DATETIME]  
           ,[COMMENTS]-- ''  
           ,[PRINT_COMMENTS] --''  
           ,[REQUESTED_BY] -- 0  
           ,[EFFECTIVE_DATETIME] -- FROM FILE  
           ,[EXPIRY_DATE] -- FROM FILE  
           ,[CANCELLATION_OPTION] -- 0 if it is not a cancellation process if cancellation then it is Calculatable (11996)  
           ,[CANCELLATION_TYPE] -- 0 if it is not a cancellation process if cancellation then it is Calculatable (11996)  
           ,[REASON] -- 0 if it is not a cancellation process if cancellation then it is Calculatable (11551)  
           ,[OTHER_REASON]  -- o  
           ,[RETURN_PREMIUM] -- 0, Only in cancellation it needs to be calculated  
           ,[PAST_DUE_PREMIUM] -- 0.00  
           ,[ENDORSEMENT_NO] ---- @ENDORSEMENT_NO -- FILE END NUMBER  
           ,[PROPERTY_INSPECTION_CREDIT]  -- '' in case of NBS OTHER 'N'  
           ,[POLICY_TERMS] -- 0  
           ,[NEW_POLICY_TERM_EFFECTIVE_DATE]-- NULL  
           ,[NEW_POLICY_TERM_EXPIRATION_DATE] -- NULL  
           ,[DIARY_LIST_ID] -- NULL  
           ,[RETURN_PREMIUM_AMOUNT] -- NULL  
           ,[RETURN_MCCA_FEE_AMOUNT] -- NULL  
           ,[RETURN_OTHER_FEE_AMOUNT] -- NULL  
           ,[PRINTING_OPTIONS] -- 0  
           ,[INSURED] -- 11983 IN CASE OF NBS other 0  
           ,[SEND_INSURED_COPY_TO]  -- 13035 IN CASE OF CANCELLATION OTHER 11980  
           ,[AUTO_ID_CARD]  -- 0  
           ,[NO_COPIES] -- 0  
           ,[STD_LETTER_REQD] -- 0  
           ,[CUSTOM_LETTER_REQD] -- 0  
           ,[ADD_INT]   -- 13035 in CASE OF CANCELATION OTHER 11980  
           ,[ADD_INT_ID] -- blank ''  
           ,[SEND_ALL] -- 1  
           ,[AGENCY_PRINT] -- 13035 in CASE OF CANCELATION OTHER 11980  
           ,[OTHER_RES_DATE_CD] -- ''  
           ,[OTHER_RES_DATE] -- NULL  
           ,[INTERNAL_CHANGE] --  (in progress NULL) (OTHER -0)  
           ,[APPLY_REINSTATE_FEE]--  (in progress NULL) (OTHER -0)  
           ,[ANOTHER_AGENCY]  --(in progress NULL) (OTHER -0)  
           ,[CFD_AMT]  --(in progress NULL) (OTHER -0)  
           ,[SAME_AGENCY]  --(in progress NULL) (OTHER -0)  
           ,[ADVERSE_LETTER_REQD] --NBS -0, NED- 10964, In PROGRESS- NULL  
           ,[DUE_DATE] -- NULL ( ONLY IN CASE OF CANCELLATION THERE IS DUE DATE)  
           ,[CANCELLATION_NOTICE_SENT]-- NULL  
           ,[REVERT_BACK]-- NULL  
           ,[LAST_REVERT_BACK]-- NULL  
           ,[INCLUDE_REASON_DESC]-- NULL  
           ,[WRITTEN_OFF_PREMIUM]-- NULL  
           ,[COINSURANCE_NUMBER] -- '' other NULL  
           ,[ENDORSEMENT_TYPE] -- NEED TO MAP NOT DONE (FOR NBS 0, END 14683, )  
           ,[ENDORSEMENT_OPTION] -- 0 if commited then NULL  
           ,[SOURCE_VERSION_ID] -- 0 if commited then NULL  
           ,[CO_APPLICANT_ID] -- NULL  
           ,[ENDORSEMENT_RE_ISSUE] --NULL  
           )  
  SELECT   
   @CUST_ID  
   ,@POLICY_ID  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
    THEN @POLICY_VERSION_ID  
    ELSE  
   @OLD_POLICY_VERSION  
   END  
   ,@ROW_ID-- ROW ID  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
    THEN 24   
    WHEN @LEADER_ENDORSEMENT_NUMBER>0   
    THEn 3          -- END IN PROGRESS  
    END  
   ,''            -- FROM FILE  
   ,@CUST_ID  
   ,@POLICY_ID  
     
   , CASE  WHEN @LEADER_ENDORSEMENT_NUMBER=0  
     THEn @POLICY_VERSION_ID  
     ELSE  
       @POLICY_VERSION_ID  
       END  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0   -- POLICY_PREVIOUS STATUS  
     THEn 'Suspended'  
      WHEN @LEADER_ENDORSEMENT_NUMBER>0  
     THEn 'NORMAL'  
     END  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  -- POLICY_CUURRENT_STATUS  
     THEn ''  
     WHEN @LEADER_ENDORSEMENT_NUMBER>0  
     THEn 'UENDRS'  
     END  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  -- PROCES  STATUS  
     THEn 'PENDING'  
     WHEN @LEADER_ENDORSEMENT_NUMBER>0  
     THEn 'PENDING'  
     END  
   ,@CREATED_BY  
   ,GETDATE()  
   ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  --COMPLETED BY  
     THEn 0  
     ELSE @CREATED_BY  
     END   -- COMPLETED BY  
   ,NULL--CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEn NULL  END    
   ,''   --[COMMENTS]-- ''  
   ,''   --[PRINT_COMMENTS]  
   ,0   --[REQUESTED_BY]  
   --,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
   --  THEn ENDORSEMENT_EFFECTIVE_DATE --[EFFECTIVE_DATETIME]  
   --   ELSE ENDORSEMENT_EFFECTIVE_DATE    
   -- END  
   ,ENDORSEMENT_EFFECTIVE_DATE
   ,ENDORSEMENT_EXPIRE_DATE--CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEn NULL --[EXPIRY_DATE]   
   , 0  
          --[CANCELLATION_OPTION] -----------  
   , 0  
        --[CANCELLATION_TYPE]-----------  
   , 0  
       --REASON-- if it is not a cancellat  
   ,  ''   
       -- [OTHER_REASON]  
   , 0.00   
         --    ,[RETURN_PREMIUM] -- 0,   
    ,0.00   
         --[PAST_DUE_PREMIUM] -- 0.00  
    ,@LEADER_ENDORSEMENT_NUMBER   --[ENDORSEMENT_NO] ---- @ENDORSEMENT_NO -- FILE END NUMBER  
    ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
     THEn ''  
     WHEN @LEADER_ENDORSEMENT_NUMBER>0  
     THEn 'N'  
     END   --[PROPERTY_INSPECTION_CREDIT]  -- '' in case of NBS OTHER 'N'  
      
    ,0  --POICY TERM  
    ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
     THEn NULL--CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  THEn NULL END   --[NEW_POLICY_TERM_EFFECTIVE_DATE]-- NULL  
     ELSE ENDORSEMENT_EFFECTIVE_DATE  
     END  
             ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
     THEn NULL--CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  THEn NULL END   --[NEW_POLICY_TERM_EXPIRATION_DATE]-- NULL  
     ELSE ENDORSEMENT_EXPIRE_DATE  
     END  
             ,NULL         --[DIARY_LIST_ID] -- NULL  
           ,NULL         --[RETURN_PREMIUM_AMOUNT] -- NULL  
           ,NULL         --[RETURN_MCCA_FEE_AMOUNT] -- NULL  
           ,NULL         --[RETURN_OTHER_FEE_AMOUNT] -- NULL  
           ,0          --[PRINTING_OPTIONS] -- 0  
           ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEN 11980           
   ELSE 11984  
   END           --[INSURED] -- 11983 IN CASE OF NBS other 0  
           ,0  
     --[SEND_INSURED_COPY_TO]  -- 13035 IN CASE OF CANCELLATION OTHER 11980 NEED TO WORK  
           ,11980          --[AUTO_ID_CARD]  -- 0  
           ,0          --,[NO_COPIES] -- 0  
           ,0          --[STD_LETTER_REQD] -- 0  
           ,0          --[CUSTOM_LETTER_REQD] -- 0  
           ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEN 11980           
   ELSE 11981 END         --[ADD_INT]   -- 13035 in CASE OF CANCELATION OTHER 11980  
           ,''          --[ADD_INT_ID] -- blank ''  
   ,0          --[SEND_ALL] -- 1  
           ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEN 11980           
   ELSE 11984 END   
             
                   --[AGENCY_PRINT] -- 13035 in CASE OF CANCELATION OTHER 11980  
           ,''          --[OTHER_RES_DATE_CD] -- ''  
           ,NULL         --[OTHER_RES_DATE] -- NULL  
           ,NULL         --[INTERNAL_CHANGE] --  (in progress NULL) (OTHER -0)  
           ,NULL         --[APPLY_REINSTATE_FEE]--  (in progress NULL) (OTHER -0)  
           ,NULL         --[ANOTHER_AGENCY]  --(in progress NULL) (OTHER -0)  
           ,NULL         --[CFD_AMT]  --(in progress NULL) (OTHER -0)  
           ,NULL         --[SAME_AGENCY]  --(in progress NULL) (OTHER -0)  
           ,NULL         --[ADVERSE_LETTER_REQD] --NBS -0, NED- 10964, In PROGRESS- NULL  
           ,NULL         --[DUE_DATE] -- NULL ( ONLY IN CASE OF CANCELLATION THERE IS DUE DATE)  
           ,NULL         --[CANCELLATION_NOTICE_SENT]-- NULL  
           ,NULL         --[REVERT_BACK]-- NULL  
           ,NULL         --[LAST_REVERT_BACK]-- NULL  
           ,NULL         --[INCLUDE_REASON_DESC]-- NULL  
           ,NULL         --[WRITTEN_OFF_PREMIUM]-- NULL  
           ,NULL         --[COINSURANCE_NUMBER] -- '' other NULL  
           ,  
           CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
           THEN 0  
           WHEN TRANSACTION_TYPE=21  
   THEN 14682  
     WHEN TRANSACTION_TYPE=22  
   THEN 14683  
     WHEN TRANSACTION_TYPE=23  
   THEN 14684  
     WHEN TRANSACTION_TYPE=24  
   THEN 11619  
     WHEN TRANSACTION_TYPE=25  
   THEN 11619  
   WHEN TRANSACTION_TYPE=26  
   THEN 14687  
   WHEN TRANSACTION_TYPE=27  
   THEN 14688  
   WHEN TRANSACTION_TYPE=28  
   THEN 14689  
   WHEN TRANSACTION_TYPE=31  
   THEN 14690  
   WHEN TRANSACTION_TYPE=32  
   THEN 14691  
   ELSE  
    11619  
    END  
            --[ENDORSEMENT_TYPE] -- NEED TO MAP NOT DONE (FOR NBS 0, END 14683, )  
           ,0         --[ENDORSEMENT_OPTION] -- 0 if commited then NULL  
           ,0         --[SOURCE_VERSION_ID] -- 0 if commited then NULL  
           ,CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0  
     THEn 0  
     WHEN @LEADER_ENDORSEMENT_NUMBER>0  
     THEn 1  
     END         --[CO_APPLICANT_ID] -- NULL  
           ,0         --[ENDORSEMENT_RE_ISSUE] --NULL  
   FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK)  
    END  
          
          
          
 ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** INSERT INTO [POL_APPLICANT_LIST] ***------------------  
 ---------------------------------------------------------------------------------------------       
    
        
    DECLARE @EFFECTIVE_DATE DATETIME        
         
      
  IF(@LEADER_ENDORSEMENT_NUMBER=0)     
  BEGIN  
    INSERT INTO [dbo].[POL_APPLICANT_LIST]        
        ([POLICY_ID]        
        ,[POLICY_VERSION_ID]        
        ,[CUSTOMER_ID]        
        ,[APPLICANT_ID]        
        ,[CREATED_BY]        
        --,[MODIFIED_BY]        
        ,[CREATED_DATETIME]        
  --,[LAST_UPDATED_TIME]        
        ,[IS_PRIMARY_APPLICANT]        
        ,[COMMISSION_PERCENT]        
        --,[FEES_PERCENT]        
        --,[PRO_LABORE_PERCENT]        
        )        
           
    SELECT  @POLICY_ID,        
      @POLICY_VERSION_ID,        
      @CUST_ID,        
      @APPLICANT_ID,        
      @CREATED_BY,        
      GETDATE(),        
      1,
      CASE WHEN (@TRANSACTION_TYPE=14 AND @LEADER_ENDORSEMENT_NUMBER=0)
		THEN 100
	  ELSE
		0
	   END		
      
              
  END  
  --ELSE  
  --BEGIN  
  --UPDATE  POL_APPLICANT_LIST  
  --   SET  APPLICANT_ID   = @APPLICANT_ID,  
  --  IS_PRIMARY_APPLICANT = 1  
  --WHERE  CUSTOMER_ID   = @CUST_ID   
  --   AND  POLICY_ID    = @POLICY_ID  
  --   AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID  
  --END  
              
              
 ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** INSERT INTO [POL_POLICY_ENDORSEMENTS] ***-------------  
  --------------------------------  *** INSERT INTO [POL_POLICY_ENDORSEMENTS_DETAILS] ***------  
 ---------------------------------------------------------------------------------------------        
          
  IF(@LEADER_ENDORSEMENT_NUMBER>0)         
   BEGIN        
    select 'POL_POLICY_ENDORSEMENTS'
    INSERT INTO  POL_POLICY_ENDORSEMENTS        
          (        
           POLICY_ID,        
           POLICY_VERSION_ID,        
           CUSTOMER_ID,        
           ENDORSEMENT_NO,        
           ENDORSEMENT_DATE,        
           IS_ACTIVE,        
           CREATED_BY,        
           CREATED_DATETIME ,       
           --MODIFIED_BY,        
           --LAST_UPDATED_DATETIME,        
          ENDORSEMENT_STATUS        
           --PROCESS_TYPE,        
           --END_VERIFY_DIGIT        
            ,CO_ENDORSEMENT_NO          
          )        
       SELECT           
           @POLICY_ID,        
           @POLICY_VERSION_ID,        
           @CUST_ID,        
           @LEADER_ENDORSEMENT_NUMBER,        
           ENDORSEMENT_EFFECTIVE_DATE,  
           'Y',        
           @CREATED_BY,        
           GETDATE() ,       
          'OPEN'      
           --A.COMPLEMENT,        
           --A.DISTRICT,        
           --A.CITY,        
           --A.COUNTRY,        
           --A.[STATE],        
           --A.ZIP_CODE,   
           , @LEADER_ENDORSEMENT_NUMBER      
       FROM  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)   --1        
     
     
    INSERT INTO [dbo].[POL_POLICY_ENDORSEMENTS_DETAILS]        
       ([POLICY_ID]        
       ,[POLICY_VERSION_ID]        
       ,[CUSTOMER_ID]        
       ,[ENDORSEMENT_NO]        
       ,[ENDORSEMENT_DETAIL_ID]        
       ,[ENDORSEMENT_DATE]        
       ,[ENDORSEMENT_TYPE]        
       --,[ENDORSEMENT_DESC]        
       --,[REMARKS]        
       ,[IS_ACTIVE]        
       ,[CREATED_BY]        
       ,[CREATED_DATETIME]        
       --,[MODIFIED_BY]        
       --,[LAST_UPDATED_DATETIME]        
       --,[TRANS_ID]        
       )        
       SELECT         
       @POLICY_ID,        
       @POLICY_VERSION_ID,        
       @CUST_ID,        
       @LEADER_ENDORSEMENT_NUMBER, -- CHANGE i-TRACK 1148 (INTERNAL) Logged by Surya 22-07-2011       
       @LEADER_ENDORSEMENT_NUMBER, -- CHANGE i-TRACK 1148 (INTERNAL) Logged by Surya 22-07-2011              
       ENDORSEMENT_EFFECTIVE_DATE        
       ,11619        
       ,'Y',        
       @CREATED_BY,        
       GETDATE()        
               
       FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK)          
    END           
    
    
 ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** INSERT INTO [POL_REMUNERATION] ***--------------------  
 ---------------------------------------------------------------------------------------------  
   
  IF(@LEADER_ENDORSEMENT_NUMBER=0)      
  BEGIN  
  SELECT   @REMUNERATION_ID=(ISNULL(REMUNERATION_ID,0)+1)  FROM POL_REMUNERATION (NOLOCK)          
  WHERE   CUSTOMER_ID=@CUST_ID AND POLICY_ID=@POLICY_ID     
    
       
    INSERT INTO [dbo].[POL_REMUNERATION]        
     ([REMUNERATION_ID]        
     ,[CUSTOMER_ID]        
     ,[POLICY_ID]        
     ,[POLICY_VERSION_ID]        
     ,[BROKER_ID]        
     ,[COMMISSION_PERCENT]        
     ,[COMMISSION_TYPE]        
     ,[IS_ACTIVE]        
     ,[CREATED_BY]        
     ,[CREATED_DATETIME]        
     --,[MODIFIED_BY]        
     --,[LAST_UPDATED_DATETIME]        
     --,[BRANCH]        
     --,[AMOUNT]        
     ,[LEADER]        
    -- ,[NAME]        
     --,[RISK_ID]        
     ,[CO_APPLICANT_ID]        
     )        
     SELECT        
   @REMUNERATION_ID        
   ,@CUST_ID        
   ,@POLICY_ID        
   ,@POLICY_VERSION_ID        
   ,@AGENCY_ID        
  -- ,ISNULL((ISNULL(TOTAL_COMMISSION_PERCENTAGE,0)-ISNULL(COINSURANCE_COMMISSION,0)),0)        
  ,100  
   ,43        
   ,'Y'        
   ,@CREATED_BY        
   ,GETDATE()        
   ,'10963'     
   ,@APPLICANT_ID     
           
   FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
     
   END  
     
   ELSE  
   BEGIN  
     
     
 UPDATE  POL_REMUNERATION  
 SET   BROKER_ID   = @AGENCY_ID
     
    FROM     #MIG_CUSTOMER_POLICY_LIST  
 WHERE  POL_REMUNERATION.CUSTOMER_ID   = @CUST_ID  
 AND   POL_REMUNERATION.POLICY_ID   = @POLICY_ID  
 AND   POL_REMUNERATION.POLICY_VERSION_ID = @POLICY_VERSION_ID      
      
   END  
             
           
            
          
                    
  ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** INSERT INTO [POL_REMUNERATION] ***--------------------  
 ---------------------------------------------------------------------------------------------            
             
  DECLARE @TOTAL_INSTALLMENT_AMOUNT  DECIMAL(15,4)               
  SELECT @TOTAL_INSTALLMENT_AMOUNT=SUM(INSTALLMENT_AMOUNT1        
          +INSTALLMENT_AMOUNT2        
          +INSTALLMENT_AMOUNT3        
          +INSTALLMENT_AMOUNT4        
          +INSTALLMENT_AMOUNT5        
          +INSTALLMENT_AMOUNT6        
          +INSTALLMENT_AMOUNT7        
          +INSTALLMENT_AMOUNT8        
          +INSTALLMENT_AMOUNT9        
          +INSTALLMENT_AMOUNT10        
          +INSTALLMENT_AMOUNT11        
          +INSTALLMENT_AMOUNT12)        
                  
           from  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)         
                   
           
  DECLARE @TOTAL_INTREST_AMOUNT  DECIMAL(15,4)               
  SELECT @TOTAL_INTREST_AMOUNT =SUM(INTREST_AMOUNT_PER_INSTALLMENT1        
          +INTREST_AMOUNT_PER_INSTALLMENT2        
          +INTREST_AMOUNT_PER_INSTALLMENT3        
          +INTREST_AMOUNT_PER_INSTALLMENT4        
          +INTREST_AMOUNT_PER_INSTALLMENT5        
          +INTREST_AMOUNT_PER_INSTALLMENT6        
          +INTREST_AMOUNT_PER_INSTALLMENT7        
          +INTREST_AMOUNT_PER_INSTALLMENT8        
          +INTREST_AMOUNT_PER_INSTALLMENT9        
          +INTREST_AMOUNT_PER_INSTALLMENT10        
          +INTREST_AMOUNT_PER_INSTALLMENT11        
          +INTREST_AMOUNT_PER_INSTALLMENT12        
          )        
                  
           from  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
          
  DECLARE @TOTAL_DISCOUNT_AMOUNT  DECIMAL(15,4)               
  SELECT @TOTAL_DISCOUNT_AMOUNT=SUM(DISCOUNT_AMOUNT_PER_INSTALLMENT1        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT2        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT3        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT4        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT5        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT6        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT7        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT8        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT9        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT10        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT11        
          +DISCOUNT_AMOUNT_PER_INSTALLMENT12        
          )        
                  
           from  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
                   
      DECLARE @TOTAL_COMMISSION_AMOUNT  DECIMAL(15,4)               
     SELECT @TOTAL_COMMISSION_AMOUNT=SUM(COMMISSION_AMOUNT_PER_INSTALLMENT1        
          +COMMISSION_AMOUNT_PER_INSTALLMENT2        
          +COMMISSION_AMOUNT_PER_INSTALLMENT3        
          +COMMISSION_AMOUNT_PER_INSTALLMENT4        
          +COMMISSION_AMOUNT_PER_INSTALLMENT5        
          +COMMISSION_AMOUNT_PER_INSTALLMENT6        
          +COMMISSION_AMOUNT_PER_INSTALLMENT7        
          +COMMISSION_AMOUNT_PER_INSTALLMENT8        
          +COMMISSION_AMOUNT_PER_INSTALLMENT9        
          +COMMISSION_AMOUNT_PER_INSTALLMENT10        
          +COMMISSION_AMOUNT_PER_INSTALLMENT11        
          +COMMISSION_AMOUNT_PER_INSTALLMENT12        
          )        
                  
           from  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
               
           
   
     SELECT @NET_PREMIUM=isnull(INSTALLMENT_AMOUNT,0),@INTEREST=isnull(INTREST_AMOUNT_PER_INSTALLMENT,0),@TOTAL_CHANGE_INFORCE_PRM=isnull(INSTALLMENT_AMOUNT,0),@TOTAL_INFO_PRM=isnull(INSTALLMENT_AMOUNT,0)        
    ,@TOTAL_PREMIUM=ISNULL(A.INSTALLMENT_AMOUNT,0)--+isnull(INTREST_AMOUNT_PER_INSTALLMENT,0)+isnull(COMMISSION_AMOUNT_P,0)
    FROM #BILLING_DETAILS_TEMP (NOLOCK) A    
            
 if(@LEADER_ENDORSEMENT_NUMBER=0)        
 BEGIN        
  SET  @TRAN_TYPE='NBS'        
  --SELECT @NET_PREMIUM=isnull(INSTALLMENT_AMOUNT,0),@INTEREST=isnull(INTREST_AMOUNT_PER_INSTALLMENT,0),@TOTAL_CHANGE_INFORCE_PRM=isnull(INSTALLMENT_AMOUNT,0),@TOTAL_INFO_PRM=isnull(INSTALLMENT_AMOUNT,0)        
  --  ,@TOTAL_PREMIUM=ISNULL(A.INSTALLMENT_AMOUNT,0)        
  --FROM #BILLING_DETAILS_TEMP (NOLOCK) A        
         
          
          
 END        
 ELSE        
 BEGIN        
  SET  @TRAN_TYPE='END'        
  --SELECT @NET_PREMIUM=ABS(isnull(A.INSTALLMENT_AMOUNT,0)-@NET_PREMIUM),@INTEREST=ABS(isnull(A.INTREST_AMOUNT_PER_INSTALLMENT,0)-@INTEREST),        
  --@TOTAL_CHANGE_INFORCE_PRM=ABS(A.INSTALLMENT_AMOUNT-@TOTAL_CHANGE_INFORCE_PRM),@TOTAL_INFO_PRM=ABS(A.INSTALLMENT_AMOUNT-@TOTAL_INFO_PRM),        
  --@TOTAL_PREMIUM=(ISNULL(A.INSTALLMENT_AMOUNT,0)+@TOTAL_PREMIUM)        
        
  -- FROM #BILLING_DETAILS_TEMP (NOLOCK) A        
         
 END        
         
             
    INSERT INTO [dbo].[ACT_POLICY_INSTALL_PLAN_DATA]        
           ([POLICY_ID]        
   ,[POLICY_VERSION_ID]        
           ,[CUSTOMER_ID]        
           ,[PLAN_ID]        
           ,[APP_ID]        
           ,[APP_VERSION_ID]        
           ,[PLAN_DESCRIPTION]        
           ,[PLAN_TYPE]        
           ,[NO_OF_PAYMENTS]        
           ,[MONTHS_BETWEEN]        
           ,[PERCENT_BREAKDOWN1]        
           ,[PERCENT_BREAKDOWN2]        
           ,[PERCENT_BREAKDOWN3]        
           ,[PERCENT_BREAKDOWN4]        
           ,[PERCENT_BREAKDOWN5]        
           ,[PERCENT_BREAKDOWN6]        
           ,[PERCENT_BREAKDOWN7]        
           ,[PERCENT_BREAKDOWN8]        
           ,[PERCENT_BREAKDOWN9]        
           ,[PERCENT_BREAKDOWN10]        
           ,[PERCENT_BREAKDOWN11]        
           ,[PERCENT_BREAKDOWN12]        
           ,[MODE_OF_DOWN_PAYMENT]        
           ,[INSTALLMENTS_IN_DOWN_PAYMENT]        
           ,[MODE_OF_PAYMENT]        
           ,[CURRENT_TERM]        
           ,[IS_ACTIVE_PLAN]        
           ,[TOTAL_PREMIUM]        
           ,[TOTAL_INTEREST_AMOUNT]        
           --,[TOTAL_FEES]        
           --,[TOTAL_TAXES]        
           ,[TOTAL_AMOUNT]        
           ,[TRAN_TYPE]        
           ,[TOTAL_TRAN_PREMIUM]        
           ,[TOTAL_TRAN_INTEREST_AMOUNT]        
           --,[TOTAL_TRAN_FEES]        
           --,[TOTAL_TRAN_TAXES]        
           ,[TOTAL_TRAN_AMOUNT]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           --,[MODIFIED_BY]        
           --,[LAST_UPDATED_DATETIME]        
         --  ,[TOTAL_CHANGE_INFORCE_PRM]        
           --,[PRM_DIST_TYPE]        
           ,[TOTAL_INFO_PRM]        
           --,[TOTAL_STATE_FEES]        
           --,[TOTAL_TRAN_STATE_FEES]        
           --,[CO_APPLICANT_ID]        
           )        
                   
       SELECT        
   @POLICY_ID,        
   @POLICY_VERSION_ID,        
   @CUST_ID,        
   @INSTALL_PLAN_ID,        
   @APP_ID,        
   @APP_VERSION_ID,        
   C.PLAN_DESCRIPTION,        
   C.PLAN_TYPE,        
   C.NO_OF_PAYMENTS,        
   C.MONTHS_BETWEEN,        
   C.PERCENT_BREAKDOWN1,        
   C.PERCENT_BREAKDOWN2,        
   C.PERCENT_BREAKDOWN3,        
   C.PERCENT_BREAKDOWN4,        
   C.PERCENT_BREAKDOWN5,        
   C.PERCENT_BREAKDOWN6,        
   C.PERCENT_BREAKDOWN7,        
   C.PERCENT_BREAKDOWN8,        
   C.PERCENT_BREAKDOWN9,        
   C.PERCENT_BREAKDOWN10,        
   C.PERCENT_BREAKDOWN11,        
   C.PERCENT_BREAKDOWN12,        
   C.NO_INS_DOWNPAY,        
   @DOWN_PAYMENT_MODE,        
           
   @PAYMENT_MODE,  
   @CURRENT_TERM,        
   'Y',        
   ISNULL(@TOTAL_INSTALLMENT_AMOUNT,0),        
           
   isnull(@TOTAL_INTREST_AMOUNT,0),        
           
           
        -- FEES        
   ISNULL(@TOTAL_INSTALLMENT_AMOUNT,0),        
   @TRAN_TYPE,        
    ISNULL(@TOTAL_INSTALLMENT_AMOUNT,0),   --,[TOTAL_TRAN_PREMIUM]        
   isnull(@TOTAL_INTREST_AMOUNT,0),         
           --,[TOTAL_TRAN_TAXES]        
   --(isnull(@NET_PREMIUM,0.00)+isnull(@INTEREST,0.00)+isnull(@TAX,0.00)+ISNULL(@FEES,0.00)),     --  ,[TOTAL_TRAN_AMOUNT]        
    ISNULL(@TOTAL_INSTALLMENT_AMOUNT,0),        
   @CREATED_BY,        
   GETDATE(),        
             
   --@TOTAL_CHANGE_INFORCE_PRM,   --,[TOTAL_CHANGE_INFORCE_PRM]        
   --CASE WHEN (@ENDORSEMENT_NUMBER_VAR<>0)        
   --THEN   @END_DISTRIBUTION_TYPE         
   --ELSE NULL        
   --END,        
    ISNULL(@TOTAL_INSTALLMENT_AMOUNT,0)    --,[TOTAL_INFO_PRM]        
              
  FROM #MIG_CUSTOMER_POLICY_LIST  (NOLOCK) A        
  LEFT OUTER JOIN ACT_INSTALL_PLAN_DETAIL (NOLOCK) C        
  ON C.IDEN_PLAN_ID=@INSTALL_PLAN_ID        
         
          
         
         
        
  --select * from #BILLING_DETAILS_TEMP (NOLOCK)        
          
  DECLARE @CURRENT_INSTALLMNET_NO INT        
  --SELECT @CURRENT_INSTALLMNET_NO=NO_OF_INSTALLMENTS FROM #BILLING_DETAILS_TEMP (NOLOCK)        
  --WHERE LEADER_POLICY_NUMBER=@POLICY_NUMBER AND LEADER_ENDORSEMENT_NUMBER=@ENDORSEMENT_NUMBER_VAR        
          
   INSERT INTO [dbo].[ACT_POLICY_INSTALLMENT_DETAILS]        
           ([POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[CUSTOMER_ID]        
           ,[APP_ID]        
           ,[APP_VERSION_ID]        
           ,[INSTALLMENT_AMOUNT]        
           ,[INSTALLMENT_EFFECTIVE_DATE]        
           ,[RELEASED_STATUS]        
           ,[INSTALLMENT_NO]        
           --,[RISK_ID]        
           ,[RISK_TYPE]        
           ,[PAYMENT_MODE]        
           ,[CURRENT_TERM]        
           --,[PERCENTAG_OF_PREMIUM]        
           ,[INTEREST_AMOUNT]        
           --,[FEE]        
   --,[TAXES]        
           ,[TOTAL]        
           ,[TRAN_INTEREST_AMOUNT]        
           --,[TRAN_FEE]        
           --,[TRAN_TAXES]        
           ,[TRAN_TOTAL]        
           --,[BOLETO_NO]        
           --,[IS_BOLETO_GENRATED]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           --,[MODIFIED_BY]        
           --,[LAST_UPDATED_DATETIME]        
           ,[TRAN_PREMIUM_AMOUNT]        
           ,[CO_APPLICANT_ID]        
           --,[PAID_AMOUNT]        
           --,[RECEIVED_AMOUNT]        
           --,[RECEIVED_DATE]        
           ,INSTALLMENT_EXPIRE_DATE
           ,ACC_CO_DISCOUNT        
           )        
        
  SELECT @POLICY_ID        
            , @POLICY_VERSION_ID        
            ,@CUST_ID        
            ,@APP_ID        
            ,@APP_VERSION_ID        
            ,D.INSTALLMENT_AMOUNT        
            ,D.INSTALLMENT_DUE_DATE        
            ,'N'        
            ,D.INSTALLMENT_NO        
            ,@RISK_TYPE        
   ,@PAYMENT_MODE        
   ,@CURRENT_TERM        -- CUURRENT TERM
            ,D.INTREST_AMOUNT_PER_INSTALLMENT          -- [INTEREST_AMOUNT]
            ,(ISNULL(D.INSTALLMENT_AMOUNT,0.00)+ISNULL(D.INTREST_AMOUNT_PER_INSTALLMENT,0.00))   -- TOTAL
   ,D.INTREST_AMOUNT_PER_INSTALLMENT				   -- [TRAN_INTEREST_AMOUNT]	
   ,(ISNULL(D.INSTALLMENT_AMOUNT,0.00)+ISNULL(D.INTREST_AMOUNT_PER_INSTALLMENT,0.00))        -- [TRAN_TOTAL]
   ,@CREATED_BY        
   ,GETDATE()        
   ,D.INSTALLMENT_AMOUNT				-- [TRAN_PREMIUM_AMOUNT]
   ,@APPLICANT_ID						-- [CO_APPLICANT_ID]
   ,D.INSTALLMENT_DUE_DATE				-- [INSTALLMENT_EXPIRE_DATE]
   ,DISCOUNT_AMOUNT_PER_INSTALLMENT		--	ACC_CO_DISCOUNT
  FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK) A        
  JOIN        
  #BILLING_DETAILS_TEMP (NOLOCK) D        
  ON A.LEADER_POLICY_NUMBER=D.LEADER_POLICY_NUMBER AND A.LEADER_ENDORSEMENT_NUMBER=D.LEADER_ENDORSEMENT_NUMBER        
  WHERE D.INSTALLMENT_AMOUNT<>0.00        
          
  DECLARE @COINSURANCE_ID INT        
  SELECT @COINSURANCE_ID=ISNULL(MAX(COINSURANCE_ID),1) FROM POL_CO_INSURANCE (NOLOCK)        
      
      
 ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** INSERT INTO [POL_CO_INSURANCE] ***--------------------  
 ---------------------------------------------------------------------------------------------     
    
  IF(@LEADER_ENDORSEMENT_NUMBER=0)        
  BEGIN  
  
  
  INSERT INTO [dbo].[POL_CO_INSURANCE]        
           ([COINSURANCE_ID]        
           ,[COMPANY_ID]        
           ,[CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[CO_INSURER_NAME]        
           ,[LEADER_FOLLOWER]        
           ,[COINSURANCE_PERCENT]        
           ,[COINSURANCE_FEE]        
           ,[BROKER_COMMISSION]        
           ,[TRANSACTION_ID]        
           ,[LEADER_POLICY_NUMBER]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
           ,[CREATED_DATETIME]        
           --,[MODIFIED_BY]        
           --,[LAST_UPDATED_DATETIME]        
           --,[BRANCH_COINSURANCE_ID]        
           --,[ENDORSEMENT_POLICY_NUMBER]        
           )        
         
   SELECT         
    @COINSURANCE_ID        
    ,1        
    ,@CUST_ID        
    ,@POLICY_ID        
    ,@POLICY_VERSION_ID        
    ,''        
    ,14549						--  FOLLOWER
    ,COINSURENCE_SHARE_PERCENTAGE        
    ,TOTAL_COMMISSION_PERCENTAGE        
    ,0.00        
    ,COINSURENCE_TRANSACTION_ID        
    ,LEADER_POLICY_NUMBER        
    ,'Y'        
    ,@CREATED_BY        
    ,GETDATE()        
   FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK)        
           
  INSERT INTO [dbo].[POL_CO_INSURANCE]        
           ([COINSURANCE_ID]        
           ,[COMPANY_ID]        
           ,[CUSTOMER_ID]        
           ,[POLICY_ID]        
           ,[POLICY_VERSION_ID]        
           ,[CO_INSURER_NAME]        
           ,[LEADER_FOLLOWER]        
           ,[COINSURANCE_PERCENT]        
           ,[COINSURANCE_FEE]        
           ,[BROKER_COMMISSION]        
           --,[TRANSACTION_ID]        
          ,[LEADER_POLICY_NUMBER]        
           ,[IS_ACTIVE]        
           ,[CREATED_BY]        
,[CREATED_DATETIME]        
           --,[MODIFIED_BY]        
           --,[LAST_UPDATED_DATETIME]        
           --,[BRANCH_COINSURANCE_ID]        
           --,[ENDORSEMENT_POLICY_NUMBER]        
           )        
         
   SELECT         
    (@COINSURANCE_ID+1)        
    ,ISNULL(B.REIN_COMAPANY_ID,125)        
    ,@CUST_ID        
    ,@POLICY_ID        
    ,@POLICY_VERSION_ID        
    ,''        
    ,14548				--  LEADER
    , (100 - COINSURENCE_SHARE_PERCENTAGE )       
    ,0.00        
    ,0.00        
    ,LEADER_POLICY_NUMBER        
    ,'Y'        
    ,@CREATED_BY        
    ,GETDATE()        
   FROM #MIG_CUSTOMER_POLICY_LIST (NOLOCK) A        
   LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST (NOLOCK) B        
   ON A.SUSEP_LEADER_CODE=B.SUSEP_NUM    
     
  END      
    
  ELSE  
  BEGIN  
  UPDATE  [POL_CO_INSURANCE]  
  SET	  COINSURANCE_PERCENT		=	COINSURENCE_SHARE_PERCENTAGE,  
		  COINSURANCE_FEE			=	TOTAL_COMMISSION_PERCENTAGE,  
		  TRANSACTION_ID			=	COINSURENCE_TRANSACTION_ID,
		  ENDORSEMENT_POLICY_NUMBER	=	RIGHT('000000'+@LEADER_ENDORSEMENT_NUMBER,6)  
       
  FROM  #MIG_CUSTOMER_POLICY_LIST (NOLOCK)  
  WHERE  LEADER_FOLLOWER   = 14549  
  AND   [POL_CO_INSURANCE].CUSTOMER_ID    = @CUST_ID  
  AND   [POL_CO_INSURANCE].POLICY_ID    = @POLICY_ID  
  AND   [POL_CO_INSURANCE].POLICY_VERSION_ID  = @POLICY_VERSION_ID  
   
  END  
           
    
 ---------------------------------------------------------------------------------------------  
 ---------------------------------  *** UPDATE MIG_CUSTOMER_POLICY_LIST ***--------------------  
 ---------------------------------------------------------------------------------------------    
          
   UPDATE MIG_CUSTOMER_POLICY_LIST  
   SET            
  ALBA_POLICY_NUMBER=CASE WHEN @LEADER_ENDORSEMENT_NUMBER=0 THEN @APP_NUMBER ELSE @NBS_APP_NUMBER END,  
  ALBA_ENDORSEMENT_NO=@LEADER_ENDORSEMENT_NUMBER,  
  CUSTOMER_ID=@CUST_ID,  
  POLICY_ID=@POLICY_ID,  
  POLICY_VERSION_ID=@POLICY_VERSION_ID  
 WHERE IMPORT_REQUEST_ID=@INPUT_REQUEST_ID AND IMPORT_SERIAL_NO=@INPUT_SERIAL_ID  
   
    
  DROP TABLE #MIG_CUSTOMER_POLICY_LIST        
  DROP TABLE #BILLING_DETAILS_TEMP     
    
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
  @IMPORT_REQUEST_ID    = @INPUT_REQUEST_ID    
 ,@IMPORT_SERIAL_NO  = @INPUT_SERIAL_ID    
 ,@ERROR_NUMBER      = @ERROR_NUMBER    
 ,@ERROR_SEVERITY    = @ERROR_SEVERITY    
 ,@ERROR_STATE          = @ERROR_STATE    
 ,@ERROR_PROCEDURE   = @ERROR_PROCEDURE    
 ,@ERROR_LINE        = @ERROR_LINE    
 ,@ERROR_MESSAGE        = @ERROR_MESSAGE    
      
     
         
     
 END CATCH      
END   
  
  
  
  
  


GO

