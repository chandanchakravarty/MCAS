IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_TEST_INSERT_POLICY_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_TEST_INSERT_POLICY_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PROC_TEST_INSERT_POLICY_INFO]        
AS        
BEGIN   
--POL_CUSTOMER_POLICY_LIST        
 --DROP TABLE #TEMP_CUSTOMER_POLICY_LT        
         
 DECLARE        
  @CUSTOMER_ID INT,        
  @CUSTOMER_NAME VARCHAR(MAX),        
  @COMPANY_ID VARCHAR(MAX),        
  @POLICY_ID int,                 
  @POLICY_VERSION_ID smallint,          
                   
  @APP_ID int,--WILL BE POLICY ID           
                   
  @APP_VERSION_ID smallint,                 
  @POLICY_TYPE nvarchar (24),                
  @POLICY_NUMBER nvarchar (150),                
  @POLICY_DISP_VERSION nvarchar (912),                
  @POLICY_STATUS varchar (20),                
  @POLICY_LOB nvarchar (10),                
  @POLICY_SUBLOB nvarchar (10),                
  @POLICY_DESCRIPTION varchar (255),                
  @ACCOUNT_EXEC int,                 
  @CSR int,                 
  @UNDERWRITER int ,                
  @PROCESS_STATUS smallint ,                
  @IS_UNDER_CONFIRMATION nchar (2),                
  @LAST_PROCESS nvarchar (20),                
  @LAST_PROCESS_COMPLETED datetime,                 
  @IS_ACTIVE nchar (2),                
  @CREATED_BY int ,                
  @CREATED_DATETIME datetime ,                
  @MODIFIED_BY int ,                
  @LAST_UPDATED_DATETIME datetime ,                
  @POLICY_ACCOUNT_STATUS int ,                
  @AGENCY_ID smallint ,                
  @PARENT_APP_VERSION_ID smallint ,                
  @APP_STATUS nvarchar (50),                
  @APP_NUMBER nvarchar (150),                
  @APP_VERSION nvarchar (8),                
  @APP_TERMS nvarchar (10),                
  @APP_INCEPTION_DATE datetime,            
  @APP_EFFECTIVE_DATE datetime,                 
  @APP_EXPIRATION_DATE datetime ,                
  @IS_UNDER_REVIEW nchar (2),                
  @COUNTRY_ID int ,                
  @STATE_ID smallint ,                
  @DIV_ID smallint ,                
  @DEPT_ID smallint,                 
  @PC_ID smallint ,                
  @BILL_TYPE char (2),                
  @COMPLETE_APP char(1),                
  @INSTALL_PLAN_ID int ,                
  @CHARGE_OFF_PRMIUM varchar (5),                
  @RECEIVED_PRMIUM decimal (18,2),                
  @PROXY_SIGN_OBTAINED int ,                
  @SHOW_QUOTE nchar (2),                
  @APP_VERIFICATION_XML nvarchar(max),                 
  @YEAR_AT_CURR_RESI real,                 
  @YEARS_AT_PREV_ADD varchar (250),                
  @POLICY_TERMS nvarchar (10),                
  @POLICY_EFFECTIVE_DATE datetime,                 
  @POLICY_EXPIRATION_DATE datetime,                 
  @POLICY_STATUS_CODE varchar (6),                
  @SEND_RENEWAL_DIARY_REM char (1),                
  @TO_BE_AUTO_RENEWED smallint,                
  @POLICY_PREMIUM_XML nvarchar(max),                
  @MVR_WIN_SERVICE char (1),                
  @ALL_DATA_VALID int ,                
  @PIC_OF_LOC int ,                
  @PROPRTY_INSP_CREDIT int ,                
  @BILL_TYPE_ID int ,                
  @IS_HOME_EMP bit ,                
  @RULE_INPUT_XML nvarchar(max),                
  @POL_VER_EFFECTIVE_DATE datetime,                 
  @POL_VER_EXPIRATION_DATE datetime,                 
  @APPLY_INSURANCE_SCORE int,                 
  @DWELLING_ID smallint,                 
  @ADD_INT_ID int,                 
  @PRODUCER int,                 
  @DOWN_PAY_MODE int,                 
  @CURRENT_TERM smallint,                 
  @NOT_RENEW char (1),                
  @NOT_RENEW_REASON int,                 
  @REFER_UNDERWRITER char (1),                
  @REFERAL_INSTRUCTIONS nvarchar (2000),                
  @REINS_SPECIAL_ACPT int,                
  @FROM_AS400 char,                 
  @CUSTOMER_REASON_CODE nvarchar (20),                
  @CUSTOMER_REASON_CODE2 nvarchar (20),                
  @CUSTOMER_REASON_CODE3 nvarchar (20),                
  @CUSTOMER_REASON_CODE4 nvarchar (20),                
  @IS_REWRITE_POLICY char (1),                
  @IS_YEAR_WITH_WOL_UPDATED char (1),                
  @POLICY_CURRENCY int,                 
  @POLICY_LEVEL_COMISSION decimal (18,2),                
  @BILLTO nvarchar (100),                
  @PAYOR int,       
  @CO_INSURANCE int ,                
  @CONTACT_PERSON int ,                
  @TRANSACTION_TYPE int ,                
  @PREFERENCE_DAY smallint,                 
  @BROKER_REQUEST_NO nvarchar (100),                
  @POLICY_LEVEL_COMM_APPLIES nchar (2),                
  @BROKER_COMM_FIRST_INSTM nchar (2)          
         
              
            
   --SET @CUSTOMER_ID =  2222          
  --SET @POLICY_ID =1         
   SET @APP_VERSION_ID =1                
  SET @POLICY_VERSION_ID =1            
  SET @POLICY_TYPE =NULL          
   --SET FROM DUMP TABLE          
               
  --SET @POLICY_NUMBER = '889982010a10196000288'                
 -- SET @POLICY_DISP_VERSION = 1.0                
  SET @POLICY_STATUS ='Suspended'        
  --SET FROM DUMP TABLE                
  --SET @POLICY_LOB = 9                
  --SET @POLICY_SUBLOB =1                
  SET @POLICY_DESCRIPTION=NULL                
  SET @ACCOUNT_EXEC=NULL                
  SET @CSR=61                
  SET @UNDERWRITER=241                
  SET @PROCESS_STATUS=NULL                
  SET @IS_UNDER_CONFIRMATION=NULL                
  SET @LAST_PROCESS=NULL                
  SET @LAST_PROCESS_COMPLETED=NULL  
                
  --HARD CODE VALUES
  SET @IS_ACTIVE='Y'                
  SET @CREATED_BY=198                  
  SET @CREATED_DATETIME='2010-10-27 10:08:07.233'                
  SET @MODIFIED_BY=NULL                
  SET @LAST_UPDATED_DATETIME=GETDATE()  
  SET @POLICY_CURRENCY=2   
   
                 
  SET @POLICY_ACCOUNT_STATUS=NULL                
  SET @AGENCY_ID=83                
  SET @PARENT_APP_VERSION_ID=0                
  SET @APP_STATUS='COMPLETE'         
   --SET FROM DUMP TABLE               
  --SET @APP_NUMBER='P8998343APP'                
  SET @APP_VERSION='1.0'                
  SET @APP_TERMS='1'                
  SET @APP_INCEPTION_DATE='2010-10-27 00:00:00.000'                
  SET @APP_EFFECTIVE_DATE='2010-10-27 00:00:00.000'                
  SET @APP_EXPIRATION_DATE='2010-10-28 00:00:00.000'                
  SET @IS_UNDER_REVIEW=NULL                
  SET @COUNTRY_ID =1                 
  SET @STATE_ID = 0                
  SET @DIV_ID =291                
  SET @DEPT_ID = 161                
  SET @PC_ID = 200                
  SET @BILL_TYPE ='DB'                
  SET @COMPLETE_APP=''                
  SET @INSTALL_PLAN_ID=55                
  SET @CHARGE_OFF_PRMIUM =''                
  SET @RECEIVED_PRMIUM =0.00                
  SET @PROXY_SIGN_OBTAINED=0                
  SET @SHOW_QUOTE=NULL                
  SET @APP_VERIFICATION_XML=NULL                
  SET @YEAR_AT_CURR_RESI=0                
  SET @YEARS_AT_PREV_ADD=''                
  SET @POLICY_TERMS=NULL          
   --SET FROM DUMP TABLE              
  --SET @POLICY_EFFECTIVE_DATE='2010-10-27 00:00:00.000'                
 -- SET @POLICY_EXPIRATION_DATE='2010-10-27 00:00:00.000'                
  SET @POLICY_STATUS_CODE=NULL                
  SET @SEND_RENEWAL_DIARY_REM=NULL                
  SET @TO_BE_AUTO_RENEWED=NULL                
  SET @POLICY_PREMIUM_XML=NULL                
  SET @MVR_WIN_SERVICE=NULL                
  SET @ALL_DATA_VALID=NULL                
  SET @PIC_OF_LOC=NULL                
  SET @PROPRTY_INSP_CREDIT=0                
  SET @BILL_TYPE_ID=8460                
  SET @IS_HOME_EMP=0                
  SET @RULE_INPUT_XML=NULL                
  SET @POL_VER_EFFECTIVE_DATE='2010-10-27 00:00:00.000'                
  SET @POL_VER_EXPIRATION_DATE='2010-10-28 00:00:00.000'                
  SET @APPLY_INSURANCE_SCORE=-1                
  SET @DWELLING_ID=NULL                
  SET @ADD_INT_ID=NULL                
  SET @PRODUCER=0                
  SET @DOWN_PAY_MODE=11974                
  SET @CURRENT_TERM=1                
  SET @NOT_RENEW=NULL                
  SET @NOT_RENEW_REASON=NULL                
  SET @REFER_UNDERWRITER=NULL            
  SET @REFERAL_INSTRUCTIONS=NULL                
  SET @REINS_SPECIAL_ACPT=NULL                
  SET @FROM_AS400='Y'                
  SET @CUSTOMER_REASON_CODE=''                
  SET @CUSTOMER_REASON_CODE2=''                
  SET @CUSTOMER_REASON_CODE3=''                
  SET @CUSTOMER_REASON_CODE4=''                
  SET @IS_REWRITE_POLICY=NULL                
  SET @IS_YEAR_WITH_WOL_UPDATED=NULL        
              
             
  SET @POLICY_LEVEL_COMISSION=0.00                
  SET @BILLTO=''                
  SET @PAYOR=14542                
  SET @CO_INSURANCE=14548                
  SET @CONTACT_PERSON=0          
  --SET FROM DUMP TABLE             
 -- SET @TRANSACTION_TYPE=0                
  SET @PREFERENCE_DAY=0                
  SET @BROKER_REQUEST_NO=''                
  SET @POLICY_LEVEL_COMM_APPLIES='N'                
  SET @BROKER_COMM_FIRST_INSTM='N'          
          
  ----Temp table for inserting record for pol_customer_policy_list        
         
  CREATE TABLE #TEMP_CUSTOMER_POLICY_LIST        
  (        
  ID INT IDENTITY(1,1),        
  SUSEPLoB VARCHAR(max),        
  SubLoB  VARCHAR(max),        
  PolicyNo VARCHAR(max),
  [Carrier Branch ID] VARCHAR(MAX),
  ProductCode VARCHAR(MAX),        
  EndorsementNo VARCHAR(max),        
  TransactionType VARCHAR(max),        
  ApplicationNo VARCHAR(max),        
  ApplicationDate VARCHAR(max),        
  [Effective DATE] VARCHAR(max),        
  [Expire DATE] VARCHAR(max),        
  Currency VARCHAR(max),
  CompanyId  VARCHAR(max),
  Policy_Number VARCHAR(max)      
  )        
    -- drop table  #TEMP_CUSTOMER_POLICY_LIST    
  INSERT INTO #TEMP_CUSTOMER_POLICY_LIST        
 (SUSEPLoB ,        
  SubLoB  ,        
  PolicyNo ,
  [Carrier Branch ID],
  ProductCode,        
  EndorsementNo,        
  TransactionType,        
  ApplicationNo ,        
  ApplicationDate ,        
  [Effective DATE],        
  [Expire DATE] ,        
  Currency,
  CompanyId
    )         
        
 SELECT SUSEPLoB ,        
  SubLoB  ,        
  PolicyNo ,
  [Carrier Branch ID],
  ProductCode,  -- iTS CONTAIN POLICY_LOB      
  EndorsementNo,        
  TransactionType,        
  ApplicationNo ,        
  ApplicationDate ,        
  [Effective DATE],        
  [Expire DATE] ,        
  Currency,
  [Individual/Company Id]        
  FROM [Importnew_Policy_Info] WITH (NOLOCK) 
  WHERE CONVERT(INT,TransactionType)in (12,13,14,15) -- All transaction except 99
  
  If not EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.columns 
    WHERE TABLE_NAME = 'Importnew_Policy_Info' AND COLUMN_NAME = 'POLICY_NUMBER')
	BEGIN
		ALTER TABLE [Importnew_Policy_Info] 
		 ADD POLICY_NUMBER  varchar(max)
		 
	END 
	
   DECLARE @COUNT INT,@COUNTER INT,   
   @POLICYNO VARCHAR(MAX),  
   @TEMP_DIV_ID INT,
   @TEMP_BRANCH_ID VARCHAR(MAX)
  
         
  SELECT  @COUNT =COUNT(*) FROM #TEMP_CUSTOMER_POLICY_LIST    
   WHERE CONVERT(INT,TransactionType) in (12,13,14,15) -- All transaction accept 99
  SET @COUNTER=1        
  
          
  ----RUNNING LOOP FOR UPDATING #TEMP_CUSTOMER_POLICY_LIST FOR GENERATING POLICY_NUMBER
  ----- for TransactionType in ('12','13','14','15')-------------        
          
  WHILE @COUNT > 0        
  BEGIN  --1   
	          
	  SELECT @POLICY_LOB =   ProductCode,--  CASE WHEN ISNULL(ProductCode,'')<>'' THEN CONVERT(INT,ProductCode) END,
			 @POLICY_NUMBER = PolicyNo,
			 @POLICY_EFFECTIVE_DATE= CASE  WHEN ISNULL([Effective DATE],'')<>'' THEN        
			 CONVERT(DATETIME,[Effective DATE],101) END,
	         @TEMP_BRANCH_ID  =   [Carrier Branch ID] 
	   FROM #TEMP_CUSTOMER_POLICY_LIST        
		 WHERE ID=@COUNTER   AND  CONVERT(INT,TransactionType) <> 99 
		 
		 
		 
		 
		 SELECT @TEMP_DIV_ID = DIV_ID FROM MNT_DIV_LIST WHERE BRANCH_CODE = @TEMP_BRANCH_ID 
		 
		-- print @POLICY_LOB 
		-- print 'POLICY_LOB' 
		--print @TEMP_BRANCH_ID
		--print 'TEMP_BRANCH_ID'
		-- print @POLICY_NUMBER
		--  print 'POLICY_NUMBER'
		 
		-- PRINT @TEMP_DIV_ID
		-- PRINT 'TEMP_DIV_ID'
		-- PRINT @POLICY_EFFECTIVE_DATE
		-- PRINT '@POLICY_EFFECTIVE_DATE'
		
		 --print [dbo].[func_GenerateAppPolNumber](@POLICY_LOB,@TEMP_DIV_ID,@POLICY_EFFECTIVE_DATE,'Pol', @POLICY_NUMBER)
		 
		 UPDATE IMP
	     SET IMP.POLICY_NUMBER = [dbo].[func_GenerateAppPolNumber](@POLICY_LOB,@TEMP_DIV_ID,@POLICY_EFFECTIVE_DATE,'Pol', @POLICY_NUMBER)     
	     
	     FROM #TEMP_CUSTOMER_POLICY_LIST   TEMP_POL
	     INNER JOIN [Importnew_Policy_Info] AS IMP	     
	     ON IMP.POLICYNO = TEMP_POL.POLICYNO
	     AND IMP.TransactionType = TEMP_POL.TransactionType
	     AND IMP.[Individual/Company Id] = TEMP_POL.CompanyId		 
		 WHERE TEMP_POL.ID=@COUNTER
		 
		 UPDATE #TEMP_CUSTOMER_POLICY_LIST
		 SET POLICY_NUMBER = [dbo].[func_GenerateAppPolNumber](@POLICY_LOB,@TEMP_DIV_ID,@POLICY_EFFECTIVE_DATE,'Pol', @POLICY_NUMBER)
		 WHERE ID=@COUNTER
		 
		 
		 print [dbo].[func_GenerateAppPolNumber](@POLICY_LOB,@TEMP_DIV_ID,@POLICY_EFFECTIVE_DATE,'Pol', @POLICY_NUMBER)
	    
	   SET @COUNT = @COUNT -1         
	   SET @COUNTER = @COUNTER +1 
              
  END--1 END OF MAIN LOOP
    
  DECLARE  @EndorsementNo VARCHAR(MAX)  
  

         
  SELECT  @COUNT =COUNT(*) FROM #TEMP_CUSTOMER_POLICY_LIST 
  WHERE CONVERT(INT,TransactionType)in (12,13,14,15) -- All transaction accept 99        
  SET @COUNTER=1        
          
  ----RUNNING LOOP FOR INSERTING RECORDS-------------        
  WHILE @COUNT > 0        
  BEGIN  --1       
          
	  --CONDITION FOR CHECKING POLICY ALREADY EXIST OR NOT---          
	  --IF NOT EXIST THEN INSERT POLICY AND TAKE REF OF CUSTOMER        
	  --1.  check policy exist or not IN POL_CUSTOMR_POLICY_LIST        
	  --2.  if not exist pick that policy and keep the customer name and company id        
	  --3.  with that customer name and company id pick customer id from customer table if customer id         
		 --is not available then  return         
	  ---4 check for that customer id any policy available in that way generate policy id.        
	   
	          
	  SELECT @POLICY_LOB = CASE WHEN ISNULL(ProductCode,'')<>'' THEN CONVERT(INT,ProductCode) END,        
	   @POLICY_SUBLOB = CASE WHEN ISNULL(SubLoB,'')<>'' THEN CONVERT(INT,SubLoB) END ,        
	   @POLICY_NUMBER = POLICY_NUMBER,
	   @POLICYNO = policyno,        
	   --@POLICY_DISP_VERSION=EndorsementNo,        
	   @TRANSACTION_TYPE= CASE WHEN ISNULL(TransactionType,'')<>'' THEN CONVERT(INT,TransactionType) END,        
	   @APP_NUMBER=ApplicationNo,        
	   --@CREATED_DATETIME= CASE  WHEN ISNULL(ApplicationDate,'')<>'' THEN        
			 -- CONVERT(DATETIME,SUBSTRING(ApplicationDate,1,2)+'/'+SUBSTRING(ApplicationDate,3,2)+'/'+SUBSTRING(ApplicationDate,5,4),101) END,        
	   @POLICY_EFFECTIVE_DATE= CASE  WHEN ISNULL([Effective DATE],'')<>'' THEN        
				CONVERT(DATETIME,[Effective DATE],101) END,        
	   @POLICY_EXPIRATION_DATE=CASE  WHEN ISNULL([Expire DATE],'')<>'' THEN        
			 CONVERT(DATETIME,[Expire DATE],101) END        
	   --@POLICY_CURRENCY=CASE WHEN ISNULL(Currency,'')<>'' THEN CONVERT(INT,Currency) END   --Currency        
	   FROM #TEMP_CUSTOMER_POLICY_LIST        
		 WHERE ID=@COUNTER  
	             
	 --CHECK POLICY IS ALREADY EXIST OR NOT         
	 IF NOT EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) WHERE POLICY_NUMBER = @POLICY_NUMBER)        
	 BEGIN  --2      
	 --PRINT   @POLICY_NUMBER         
	    
	  --PICK CUSTOMER NAME AND CPF_CNPJ             
	  SELECT --@CUSTOMER_NAME = InsuredName,
				@COMPANY_ID =[Individual/Company Id]   FROM [Importnew_Policy_Info] WITH (NOLOCK)  WHERE PolicyNo = @POLICYNO        
				
		--print 	@COMPANY_ID
		--print 'COMPANY_ID'	
	  --PICK CUSTOMER ID        
		  SELECT @CUSTOMER_ID = CUSTOMER_ID FROM CLT_CUSTOMER_LIST  WITH (NOLOCK) WHERE 
		  --CUSTOMER_FIRST_NAME = @CUSTOMER_NAME 	  AND 
		  CPF_CNPJ = @COMPANY_ID        
	          
	         --print @CUSTOMER_ID
	         --print 'CUSTOMER_ID'
	         
			  --GENERATE POLICY ID IF CUSTOMER IS AVAILABLE         
			  IF EXISTS(SELECT CUSTOMER_ID FROM CLT_CUSTOMER_LIST WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID)        
			  BEGIN --3         
				SELECT @POLICY_ID = ISNULL(MAX(POLICY_ID),0)+1 FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)           
				WHERE CUSTOMER_ID =@CUSTOMER_ID               
			              
				--SET APP ID  AS POLICY ID          
				SET @APP_ID =@POLICY_ID            
				--PRINT   'TEST' 
				--PRINT @CUSTOMER_ID
				--print 'CUSTOMER_ID'         
			            
				INSERT INTO [POL_CUSTOMER_POLICY_LIST]                  
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
				   ,[ACCOUNT_EXEC]                  
				   ,[CSR]                  
				   ,[UNDERWRITER]                  
				   ,[PROCESS_STATUS]                  
				   ,[IS_UNDER_CONFIRMATION]                  
				   ,[LAST_PROCESS]                  
				   ,[LAST_PROCESS_COMPLETED]                  
				   ,[IS_ACTIVE]                  
				   ,[CREATED_BY]              
				   ,[CREATED_DATETIME]                  
				   ,[MODIFIED_BY]                  
				   ,[LAST_UPDATED_DATETIME]                  
				   ,[POLICY_ACCOUNT_STATUS]                  
				   ,[AGENCY_ID]                  
				   ,[PARENT_APP_VERSION_ID]                  
				   ,[APP_STATUS]                  
				   ,[APP_NUMBER]                  
				   ,[APP_VERSION]                  
				   ,[APP_TERMS]                  
				   ,[APP_INCEPTION_DATE]                  
				   ,[APP_EFFECTIVE_DATE]                  
				   ,[APP_EXPIRATION_DATE]                  
				   ,[IS_UNDER_REVIEW]                  
				   ,[COUNTRY_ID]                  
				   ,[STATE_ID]                  
				   ,[DIV_ID]                  
				   ,[DEPT_ID]                  
				   ,[PC_ID]                  
				   ,[BILL_TYPE]                  
				   ,[COMPLETE_APP]                  
				   ,[INSTALL_PLAN_ID]                  
				   ,[CHARGE_OFF_PRMIUM]                  
				   ,[RECEIVED_PRMIUM]                  
				   ,[PROXY_SIGN_OBTAINED]                  
				   ,[SHOW_QUOTE]                  
				   ,[APP_VERIFICATION_XML]                  
				   ,[YEAR_AT_CURR_RESI]                  
				   ,[YEARS_AT_PREV_ADD]                  
				   ,[POLICY_TERMS]                  
				   ,[POLICY_EFFECTIVE_DATE]                  
				   ,[POLICY_EXPIRATION_DATE]                  
				   ,[POLICY_STATUS_CODE]                  
				   ,[SEND_RENEWAL_DIARY_REM]                  
				   ,[TO_BE_AUTO_RENEWED]                  
				   ,[POLICY_PREMIUM_XML]                  
				   ,[MVR_WIN_SERVICE]                  
				   ,[ALL_DATA_VALID]                  
				   ,[PIC_OF_LOC]                  
				   ,[PROPRTY_INSP_CREDIT]                  
				   ,[BILL_TYPE_ID]                  
				   ,[IS_HOME_EMP]                  
				   ,[RULE_INPUT_XML]                  
				   ,[POL_VER_EFFECTIVE_DATE]                  
				   ,[POL_VER_EXPIRATION_DATE]                  
				   ,[APPLY_INSURANCE_SCORE]                  
				   ,[DWELLING_ID]                  
				   ,[ADD_INT_ID]                  
				   ,[PRODUCER]                  
				   ,[DOWN_PAY_MODE]                  
				   ,[CURRENT_TERM]                  
				   ,[NOT_RENEW]                  
				   ,[NOT_RENEW_REASON]                  
				   ,[REFER_UNDERWRITER]                  
				   ,[REFERAL_INSTRUCTIONS]                  
				   ,[REINS_SPECIAL_ACPT]                  
				   ,[FROM_AS400]                  
				   ,[CUSTOMER_REASON_CODE]       
				   ,[CUSTOMER_REASON_CODE2]                  
				   ,[CUSTOMER_REASON_CODE3]                  
				   ,[CUSTOMER_REASON_CODE4]                  
				   ,[IS_REWRITE_POLICY]                  
				   ,[IS_YEAR_WITH_WOL_UPDATED]                  
				   ,[POLICY_CURRENCY]                  
				   ,[POLICY_LEVEL_COMISSION]                  
				   ,[BILLTO]                  
				   ,[PAYOR]                  
				   ,[CO_INSURANCE]                  
				   ,[CONTACT_PERSON]                  
				   ,[TRANSACTION_TYPE]                  
				   ,[PREFERENCE_DAY]                  
				   ,[BROKER_REQUEST_NO]                  
				   ,[POLICY_LEVEL_COMM_APPLIES]                  
				   ,[BROKER_COMM_FIRST_INSTM])                  
				 VALUES                  
				  (@CUSTOMER_ID                  
				 ,@POLICY_ID                   
				 ,@POLICY_VERSION_ID                  
				 ,@APP_ID                  
				 ,@APP_VERSION_ID                   
				 ,@POLICY_TYPE                     
				 ,@POLICY_NUMBER                  
				 ,@POLICY_DISP_VERSION                  
				 ,@POLICY_STATUS                      
				 ,@POLICY_LOB                      
				 ,@POLICY_SUBLOB                  
				 ,@POLICY_DESCRIPTION                  
				 ,@ACCOUNT_EXEC                     
				 ,@CSR                  
				 ,@UNDERWRITER                  
				 ,@PROCESS_STATUS                  
				 ,@IS_UNDER_CONFIRMATION                  
				 ,@LAST_PROCESS                    
				 ,@LAST_PROCESS_COMPLETED                  
				 ,@IS_ACTIVE                   
				 ,@CREATED_BY                  
				 ,@CREATED_DATETIME                  
				 ,@MODIFIED_BY                     
				 ,@LAST_UPDATED_DATETIME                  
				 ,@POLICY_ACCOUNT_STATUS                  
				 ,@AGENCY_ID                  
				 ,@PARENT_APP_VERSION_ID                  
				 ,@APP_STATUS                  
				 ,@APP_NUMBER                  
				 ,@APP_VERSION                  
				 ,@APP_TERMS                  
				 ,@POLICY_EFFECTIVE_DATE --,@APP_INCEPTION_DATE                  
				 ,@POLICY_EFFECTIVE_DATE   --,@APP_EFFECTIVE_DATE                  
				 ,@POLICY_EXPIRATION_DATE  --,@APP_EXPIRATION_DATE                  
				 ,@IS_UNDER_REVIEW                  
				 ,@COUNTRY_ID              
				 ,@STATE_ID             
				 ,@DIV_ID                   
				 ,@DEPT_ID                  
				 ,@PC_ID                   
				 ,@BILL_TYPE                  
				 ,@COMPLETE_APP                  
				 ,@INSTALL_PLAN_ID                  
				 ,@CHARGE_OFF_PRMIUM                  
				 ,@RECEIVED_PRMIUM                   
				 ,@PROXY_SIGN_OBTAINED                  
				 ,@SHOW_QUOTE                  
				 ,@APP_VERIFICATION_XML                  
				 ,@YEAR_AT_CURR_RESI                  
				 ,@YEARS_AT_PREV_ADD                  
				 ,@POLICY_TERMS                     
				 ,@POLICY_EFFECTIVE_DATE                    
				 ,@POLICY_EXPIRATION_DATE                     
				 ,@POLICY_STATUS_CODE                    
				 ,@SEND_RENEWAL_DIARY_REM                    
				 ,@TO_BE_AUTO_RENEWED                     
				 ,@POLICY_PREMIUM_XML                    
				 ,@MVR_WIN_SERVICE                    
				 ,@ALL_DATA_VALID                    
				 ,@PIC_OF_LOC                    
				 ,@PROPRTY_INSP_CREDIT                     
				 ,@BILL_TYPE_ID                  
				 ,@IS_HOME_EMP                  
				 ,@RULE_INPUT_XML                  
				 ,@POL_VER_EFFECTIVE_DATE                  
				 ,@POL_VER_EXPIRATION_DATE                  
				 ,@APPLY_INSURANCE_SCORE                  
				 ,@DWELLING_ID                  
				 ,@ADD_INT_ID                  
				 ,@PRODUCER                  
				 ,@DOWN_PAY_MODE                  
				 ,@CURRENT_TERM                  
				 ,@NOT_RENEW                  
				 ,@NOT_RENEW_REASON                  
				 ,@REFER_UNDERWRITER                  
				 ,@REFERAL_INSTRUCTIONS                  
				 ,@REINS_SPECIAL_ACPT                  
				 ,@FROM_AS400                  
				 ,@CUSTOMER_REASON_CODE                  
				 ,@CUSTOMER_REASON_CODE2                  
				 ,@CUSTOMER_REASON_CODE3                  
				 ,@CUSTOMER_REASON_CODE4                  
				 ,@IS_REWRITE_POLICY                  
				 ,@IS_YEAR_WITH_WOL_UPDATED                  
				 ,@POLICY_CURRENCY                  
				 ,@POLICY_LEVEL_COMISSION          
				 ,@BILLTO                  
				 ,@PAYOR                  
				 ,@CO_INSURANCE                  
				 ,@CONTACT_PERSON                  
				 ,@TRANSACTION_TYPE                  
				 ,@PREFERENCE_DAY                  
				 ,@BROKER_REQUEST_NO                  
				 ,@POLICY_LEVEL_COMM_APPLIES                  
				 ,@BROKER_COMM_FIRST_INSTM )         
			      
				IF @@ERROR<>0    
				ROLLBACK TRAN  
			  
			    --print 'insert into'   
			   END----3 END OF IF OF CHECK CUSTOMER IN CLT_CUSTOMER_LIST
			   
			   
			   
	  END---2 -END OF BEGING OF CHECK POLICY EXIST IN POL_CUSTOMER_POLICY _LIST         
	  
	  SET @COMPANY_ID= ''
	  SET @CUSTOMER_ID = 0
	   SET @COUNT = @COUNT -1         
	   SET @COUNTER = @COUNTER +1 
	   
		    
              
END--1 END OF MAIN LOOP 

SET @COMPANY_ID= ''
SET @CUSTOMER_ID = 0


-------------------------------------------------------------------------------
--------------------------For Transaction 99-----------------------------------
-------------------------------------------------------------------------------
DECLARE
--POLICY_ID	int	
--POLICY_VERSION_ID	int	
--CUSTOMER_ID	int	
@ENDORSEMENT_NO	int,	
@ENDORSEMENT_DATE	datetime,	
--@IS_ACTIVE	char	(1),
--@CREATED_BY	smallint,	
--@CREATED_DATETIME	datetime,	
--@MODIFIED_BY	smallint,	
--@LAST_UPDATED_DATETIME	datetime,	
@ENDORSEMENT_STATUS	varchar	(6),
@PROCESS_TYPE	nvarchar	(100)



SET @POLICY_ID = 1
SET @POLICY_VERSION_ID = 1
SET @CUSTOMER_ID =1
SET @ENDORSEMENT_NO =0
SET @ENDORSEMENT_DATE = '2010-10-27 00:00:00.000'
SET @IS_ACTIVE ='Y'
SET @CREATED_BY = 198
SET @CREATED_DATETIME ='2010-10-27 00:00:00.000'
SET @MODIFIED_BY = NULL
SET @LAST_UPDATED_DATETIME = GETDATE()
SET @ENDORSEMENT_STATUS ='COM'
SET @PROCESS_TYPE = NULL

DECLARE @PrvTotCOUNT int =0
 
 --COUNT TOTAL NO OF RECORDS EXCEPT 99 TRNAXACTION
 SELECT @PrvTotCOUNT = COUNT(*) FROM #TEMP_CUSTOMER_POLICY_LIST


INSERT INTO #TEMP_CUSTOMER_POLICY_LIST        
 (SUSEPLoB ,        
  SubLoB  ,        
  PolicyNo ,
  [Carrier Branch ID],
  ProductCode,        
  EndorsementNo,        
  TransactionType,        
  ApplicationNo ,        
  ApplicationDate ,        
  [Effective DATE],        
  [Expire DATE] ,        
  Currency,
  CompanyId
    )         
        
 SELECT SUSEPLoB ,        
  SubLoB  ,        
  PolicyNo ,
  [Carrier Branch ID],
  ProductCode,  -- iTS CONTAIN POLICY_LOB      
  EndorsementNo,        
  TransactionType,        
  ApplicationNo ,        
  ApplicationDate ,        
  [Effective DATE],        
  [Expire DATE] ,        
  Currency,
  [Individual/Company Id]        
  FROM [Importnew_Policy_Info] WITH (NOLOCK) 
  WHERE CONVERT(INT,TransactionType)= '99' -- All transaction except 99
  ORDER BY PolicyNo, CONVERT(INT,EndorsementNo)
  
 
   --DECLARE @COUNT INT,@COUNTER INT,   
   --@POLICYNO VARCHAR(MAX),  
   --@TEMP_DIV_ID INT,
   --@TEMP_BRANCH_ID VARCHAR(MAX)
  
         
  SELECT  @COUNT =COUNT(*) FROM #TEMP_CUSTOMER_POLICY_LIST    
   WHERE CONVERT(INT,TransactionType) =99 -- All transaction except 99
  SET @COUNTER=1        
  
          
  ----RUNNING LOOP FOR UPDATING #TEMP_CUSTOMER_POLICY_LIST FOR GENERATING POLICY_NUMBER
  ----- for TransactionType 99 -------------
  
 DECLARE @ENDORSEMENT_NUMBER INT = 0       
          
  WHILE @COUNT > 0        
  BEGIN  --1
	SET  @POLICY_NUMBER = ''
	SET @CUSTOMER_ID =0
	SET @POLICY_ID=0
	SET @POLICY_VERSION_ID =0 
	SET @ENDORSEMENT_NUMBER =0
	SET @ENDORSEMENT_NO =0
	SET @POLICYNO =''
	
  --GET NEW POLICY NUMBER for inserting in pol_policy_endorsement
  SELECT @POLICY_NUMBER = POLICY_NUMBER  FROM #TEMP_CUSTOMER_POLICY_LIST
   WHERE TransactionType <>'99' AND PolicyNo = 
   (SELECT   PolicyNo			
	     FROM #TEMP_CUSTOMER_POLICY_LIST        
		 WHERE ID= @PrvTotCOUNT + @COUNTER   AND  CONVERT(INT,TransactionType) = 99 )
		 
   --GET CUSTOMER_ID,POLICY_ID AND VERSION ID  for inserting in pol_policy_endorsement      
	 IF  EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) WHERE POLICY_NUMBER = @POLICY_NUMBER)        
	 BEGIN       
		PRINT   @POLICY_NUMBER 
		
	    SELECT @CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID, @POLICY_VERSION_ID =  MAX(ISNULL(POLICY_VERSION_ID,0))+1 
	     FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) WHERE POLICY_NUMBER = @POLICY_NUMBER
	      GROUP BY CUSTOMER_ID,POLICY_ID
	 
	        
	 END	
	 
	 --SET OTHER VARIABLE FROM TEMP TABLE FOR 99  
		SELECT @POLICY_LOB = CASE WHEN ISNULL(ProductCode,'')<>'' THEN CONVERT(INT,ProductCode) END,        
	   @POLICY_SUBLOB = CASE WHEN ISNULL(SubLoB,'')<>'' THEN CONVERT(INT,SubLoB) END ,        
	  -- @POLICY_NUMBER = POLICY_NUMBER,
	   @POLICYNO = policyno,        
	   @ENDORSEMENT_NO=CONVERT(INT,ISNULL(EndorsementNo,0)),        --FOR POL_POLICY_ENDORSEMENT
	   @TRANSACTION_TYPE= CASE WHEN ISNULL(TransactionType,'')<>'' THEN CONVERT(INT,TransactionType) END,        
	   @APP_NUMBER=ApplicationNo,        
	   --@CREATED_DATETIME= CASE  WHEN ISNULL(ApplicationDate,'')<>'' THEN        
			 -- CONVERT(DATETIME,SUBSTRING(ApplicationDate,1,2)+'/'+SUBSTRING(ApplicationDate,3,2)+'/'+SUBSTRING(ApplicationDate,5,4),101) END,        
	   @POLICY_EFFECTIVE_DATE= CASE  WHEN ISNULL([Effective DATE],'')<>'' THEN        
				CONVERT(DATETIME,[Effective DATE],101) END,        
	   @POLICY_EXPIRATION_DATE=CASE  WHEN ISNULL([Expire DATE],'')<>'' THEN        
			 CONVERT(DATETIME,[Expire DATE],101) END        
	   --@POLICY_CURRENCY=CASE WHEN ISNULL(Currency,'')<>'' THEN CONVERT(INT,Currency) END   --Currency        
	   FROM #TEMP_CUSTOMER_POLICY_LIST        
		 WHERE ID= @PrvTotCOUNT + @COUNTER AND  CONVERT(INT,TransactionType) = 99
		 
       -- GET LAST ENDORSEMENT NO FOR THE POLICY NUMBER
       SELECT @ENDORSEMENT_NUMBER = ISNULL(max(ENDORSEMENT_NO),0) FROM POL_POLICY_ENDORSEMENTS WHERE CUSTOMER_ID =@CUSTOMER_ID
				AND  POLICY_ID = @POLICY_ID --AND POLICY_VERSION_ID = @POLICY_VERSION_ID
				
	   
	   
	   IF(@CUSTOMER_ID > 0 AND @POLICY_ID > 0)	
	   BEGIN		
		   IF(@ENDORSEMENT_NUMBER = 0 OR @ENDORSEMENT_NO -@ENDORSEMENT_NUMBER =1 ) --CHECK FOR LAST ENDORSEMENT NO AVAILABLE OR ENDORSE INSERT IN SEQUENCE
			 
				BEGIN
			    
			    
				print @POLICYNO
				print 'old policy number'
				print @POLICY_NUMBER
				print '@POLICY_NUMBER'
				print 'entry of 99 in POL_CUSTOMER_POLICY_LIST'
				 --MAKE ENTRY HERE
			     
				 --INSERT INOT [POL_CUSTOMER_POLICY_LIST] FOR ENDORSEMENT
     			INSERT INTO [POL_CUSTOMER_POLICY_LIST]                  
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
						   ,[ACCOUNT_EXEC]                  
						   ,[CSR]                  
						   ,[UNDERWRITER]                  
						   ,[PROCESS_STATUS]                  
						   ,[IS_UNDER_CONFIRMATION]                  
						   ,[LAST_PROCESS]                  
						   ,[LAST_PROCESS_COMPLETED]                  
						   ,[IS_ACTIVE]                  
						   ,[CREATED_BY]              
						   ,[CREATED_DATETIME]                  
						   ,[MODIFIED_BY]                  
						   ,[LAST_UPDATED_DATETIME]                  
						   ,[POLICY_ACCOUNT_STATUS]                  
						   ,[AGENCY_ID]                  
						   ,[PARENT_APP_VERSION_ID]                  
						   ,[APP_STATUS]                  
						   ,[APP_NUMBER]                  
						   ,[APP_VERSION]                  
						   ,[APP_TERMS]                  
						   ,[APP_INCEPTION_DATE]                  
						   ,[APP_EFFECTIVE_DATE]                  
						   ,[APP_EXPIRATION_DATE]                  
						   ,[IS_UNDER_REVIEW]                  
						   ,[COUNTRY_ID]                  
						   ,[STATE_ID]                  
						   ,[DIV_ID]                  
						   ,[DEPT_ID]                  
						   ,[PC_ID]                  
						   ,[BILL_TYPE]                  
						   ,[COMPLETE_APP]                  
						   ,[INSTALL_PLAN_ID]                  
						   ,[CHARGE_OFF_PRMIUM]                  
						   ,[RECEIVED_PRMIUM]                  
						   ,[PROXY_SIGN_OBTAINED]                  
						   ,[SHOW_QUOTE]                  
						   ,[APP_VERIFICATION_XML]                  
						   ,[YEAR_AT_CURR_RESI]                  
						   ,[YEARS_AT_PREV_ADD]                  
						   ,[POLICY_TERMS]                  
						   ,[POLICY_EFFECTIVE_DATE]                  
						   ,[POLICY_EXPIRATION_DATE]                  
						   ,[POLICY_STATUS_CODE]                  
						   ,[SEND_RENEWAL_DIARY_REM]                  
						   ,[TO_BE_AUTO_RENEWED]                  
						   ,[POLICY_PREMIUM_XML]                  
						   ,[MVR_WIN_SERVICE]                  
						   ,[ALL_DATA_VALID]                  
						   ,[PIC_OF_LOC]                  
						   ,[PROPRTY_INSP_CREDIT]                  
						   ,[BILL_TYPE_ID]                  
						   ,[IS_HOME_EMP]                  
						   ,[RULE_INPUT_XML]                  
						   ,[POL_VER_EFFECTIVE_DATE]                  
						   ,[POL_VER_EXPIRATION_DATE]                  
						   ,[APPLY_INSURANCE_SCORE]                  
						   ,[DWELLING_ID]                  
						   ,[ADD_INT_ID]                  
						   ,[PRODUCER]                  
						   ,[DOWN_PAY_MODE]                  
						   ,[CURRENT_TERM]                  
						   ,[NOT_RENEW]                  
						   ,[NOT_RENEW_REASON]                  
						   ,[REFER_UNDERWRITER]                  
						   ,[REFERAL_INSTRUCTIONS]                  
						   ,[REINS_SPECIAL_ACPT]                  
						   ,[FROM_AS400]                  
						   ,[CUSTOMER_REASON_CODE]       
						   ,[CUSTOMER_REASON_CODE2]                  
						   ,[CUSTOMER_REASON_CODE3]                  
						   ,[CUSTOMER_REASON_CODE4]                  
						   ,[IS_REWRITE_POLICY]                  
						   ,[IS_YEAR_WITH_WOL_UPDATED]                  
						   ,[POLICY_CURRENCY]                  
						   ,[POLICY_LEVEL_COMISSION]                  
						   ,[BILLTO]                  
						   ,[PAYOR]                  
						   ,[CO_INSURANCE]                  
						   ,[CONTACT_PERSON]                  
						   ,[TRANSACTION_TYPE]                  
						   ,[PREFERENCE_DAY]                  
						   ,[BROKER_REQUEST_NO]                  
						   ,[POLICY_LEVEL_COMM_APPLIES]                  
						   ,[BROKER_COMM_FIRST_INSTM])                  
						 VALUES                  
						  (@CUSTOMER_ID                  
						 ,@POLICY_ID                   
						 ,@POLICY_VERSION_ID                  
						 ,@APP_ID                  
						 ,@APP_VERSION_ID                   
						 ,@POLICY_TYPE                     
						 ,@POLICY_NUMBER                  
						 ,@POLICY_DISP_VERSION                  
						 ,@POLICY_STATUS                      
						 ,@POLICY_LOB                      
						 ,@POLICY_SUBLOB                  
						 ,@POLICY_DESCRIPTION                  
						 ,@ACCOUNT_EXEC                     
						 ,@CSR                  
						 ,@UNDERWRITER                  
						 ,@PROCESS_STATUS                  
						 ,@IS_UNDER_CONFIRMATION                  
						 ,@LAST_PROCESS                    
						 ,@LAST_PROCESS_COMPLETED                  
						 ,@IS_ACTIVE                   
						 ,@CREATED_BY                  
						 ,@CREATED_DATETIME                  
						 ,@MODIFIED_BY                     
						 ,@LAST_UPDATED_DATETIME                  
						 ,@POLICY_ACCOUNT_STATUS                  
						 ,@AGENCY_ID                  
						 ,@PARENT_APP_VERSION_ID                  
						 ,@APP_STATUS                  
						 ,@APP_NUMBER                  
						 ,@APP_VERSION                  
						 ,@APP_TERMS                  
						 ,@POLICY_EFFECTIVE_DATE --,@APP_INCEPTION_DATE                  
						 ,@POLICY_EFFECTIVE_DATE   --,@APP_EFFECTIVE_DATE                  
						 ,@POLICY_EXPIRATION_DATE  --,@APP_EXPIRATION_DATE                  
						 ,@IS_UNDER_REVIEW                  
						 ,@COUNTRY_ID              
						 ,@STATE_ID             
						 ,@DIV_ID                   
						 ,@DEPT_ID                  
						 ,@PC_ID                   
						 ,@BILL_TYPE                  
						 ,@COMPLETE_APP                  
						 ,@INSTALL_PLAN_ID                  
						 ,@CHARGE_OFF_PRMIUM                  
						 ,@RECEIVED_PRMIUM                   
						 ,@PROXY_SIGN_OBTAINED                  
						 ,@SHOW_QUOTE                  
						 ,@APP_VERIFICATION_XML                  
						 ,@YEAR_AT_CURR_RESI                  
						 ,@YEARS_AT_PREV_ADD                  
						 ,@POLICY_TERMS                     
						 ,@POLICY_EFFECTIVE_DATE                    
						 ,@POLICY_EXPIRATION_DATE                     
						 ,@POLICY_STATUS_CODE                    
						 ,@SEND_RENEWAL_DIARY_REM                    
						 ,@TO_BE_AUTO_RENEWED                     
						 ,@POLICY_PREMIUM_XML                    
						 ,@MVR_WIN_SERVICE                    
						 ,@ALL_DATA_VALID                    
						 ,@PIC_OF_LOC                    
						 ,@PROPRTY_INSP_CREDIT                     
						 ,@BILL_TYPE_ID                  
						 ,@IS_HOME_EMP                  
						 ,@RULE_INPUT_XML                  
						 ,@POL_VER_EFFECTIVE_DATE                  
						 ,@POL_VER_EXPIRATION_DATE                  
						 ,@APPLY_INSURANCE_SCORE                  
						 ,@DWELLING_ID                  
						 ,@ADD_INT_ID                  
						 ,@PRODUCER                  
						 ,@DOWN_PAY_MODE                  
						 ,@CURRENT_TERM                  
						 ,@NOT_RENEW                  
						 ,@NOT_RENEW_REASON                  
						 ,@REFER_UNDERWRITER                  
						 ,@REFERAL_INSTRUCTIONS                  
						 ,@REINS_SPECIAL_ACPT                  
						 ,@FROM_AS400                  
						 ,@CUSTOMER_REASON_CODE                  
						 ,@CUSTOMER_REASON_CODE2                  
						 ,@CUSTOMER_REASON_CODE3                  
						 ,@CUSTOMER_REASON_CODE4                  
						 ,@IS_REWRITE_POLICY                  
						 ,@IS_YEAR_WITH_WOL_UPDATED                  
						 ,@POLICY_CURRENCY                  
						 ,@POLICY_LEVEL_COMISSION          
						 ,@BILLTO                  
						 ,@PAYOR                  
						 ,@CO_INSURANCE                  
						 ,@CONTACT_PERSON                  
						 ,@TRANSACTION_TYPE                  
						 ,@PREFERENCE_DAY                  
						 ,@BROKER_REQUEST_NO                  
						 ,@POLICY_LEVEL_COMM_APPLIES                  
						 ,@BROKER_COMM_FIRST_INSTM )  
		  --   	IF @@ERROR<>0    
				--ROLLBACK TRAN 	 
				print @ENDORSEMENT_NO
				print '[ENDORSEMENT_NO]'
				
				print 'entry of 99 in [POL_POLICY_ENDORSEMENTS]'
				
			   --INSERT INTO [POL_POLICY_ENDORSEMENTS]
				 INSERT INTO [POL_POLICY_ENDORSEMENTS]
				   (
					[POLICY_ID]
					,[POLICY_VERSION_ID]
					,[CUSTOMER_ID]
					,[ENDORSEMENT_NO]
					,[ENDORSEMENT_DATE]
					,[IS_ACTIVE]
					,[CREATED_BY]
					,[CREATED_DATETIME]
					,[MODIFIED_BY]
					,[LAST_UPDATED_DATETIME]
					,[ENDORSEMENT_STATUS]
					,[PROCESS_TYPE]
				   )
				   VALUES
				   (
					 @POLICY_ID,
					 @POLICY_VERSION_ID,
					 @CUSTOMER_ID,
					 @ENDORSEMENT_NO,
					 @ENDORSEMENT_DATE,
					 @IS_ACTIVE,
					 @CREATED_BY,
					 @CREATED_DATETIME,
					 @MODIFIED_BY,
					 @LAST_UPDATED_DATETIME,
					 @ENDORSEMENT_STATUS,
					 @PROCESS_TYPE ) 
		           
			  --     IF @@ERROR<>0    
					--ROLLBACK TRAN
				END
		   ELSE  			
				BEGIN
				
				 print @POLICYNO
				 print 'old policy number'
				 print @POLICY_NUMBER
				 print '@POLICY_NUMBER'
				 PRINT @ENDORSEMENT_NO
				 PRINT 'ENDORSEMENT_NO'
				 PRINT 'ENDORSEMENT IN NOT IN SEQUENCE. ITS AN INVALID ENTRY'
				 
				 INSERT INTO ENDORSEMENT_NOT_COPIED
				 (POLICYNO,POLICY_NUMBER,EndorsementNo) VALUES
				  (@POLICYNO,@POLICY_NUMBER,@ENDORSEMENT_NO)
				 
				 
				END 	
       END
	   SET @COUNT = @COUNT -1         
	   SET @COUNTER = @COUNTER +1 
              
  END--1 END OF MAIN LOOP
    
    
END
GO

