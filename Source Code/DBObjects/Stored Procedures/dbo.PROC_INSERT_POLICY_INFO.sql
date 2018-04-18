IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_POLICY_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_POLICY_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC PROC_INSERT_POLICY_INFO 
--select * from importnew_Policy_info
CREATE PROC [dbo].[PROC_INSERT_POLICY_INFO]        
AS        
BEGIN   TRAN     
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
				
	   			
	   IF(    @ENDORSEMENT_NUMBER = 0 OR @ENDORSEMENT_NO -@ENDORSEMENT_NUMBER =1 ) --CHECK FOR LAST ENDORSEMENT NO AVAILABLE OR ENDORSE INSERT IN SEQUENCE 
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
       
	   SET @COUNT = @COUNT -1         
	   SET @COUNTER = @COUNTER +1 
              
  END--1 END OF MAIN LOOP
    
    
--END

  print 'exist FROM POL_CUSTOMER_POLICY_LIST'  
  
  
   		  
		  
		  
 ------------------POL_APPLICANT_LIST-------------------------------
 
    
			DECLARE    
		   --Values of Martial Status  
		   @Divorced nvarchar(2) ='D',  
		   @Married nvarchar(2)='M',  
		   @Separated nvarchar(2)='P',  
		   @Single nvarchar(2)='S',  
		   @Widowed nvarchar(2)='W',  
		     
		    
		   --Values of gender  
			@Female nvarchar(2) = 'F',--'9814',  
			@Male nvarchar(2) = 'M', --'9813'    
		      
		            
		   @APPLICANT_ID int,         
		   @LAST_UPDATED_TIME datetime,                  
		   @IS_PRIMARY_APPLICANT int,                  
		   @COMMISSION_PERCENT decimal(8,4),                  
		   @FEES_PERCENT decimal(8,4) ,  
		  --@APPLICANT_ID int,              
		  --@CUSTOMER_ID int,               
		  @TITLE nvarchar(20),              
		  @SUFFIX nvarchar(10),              
		  @FIRST_NAME nvarchar (200),              
		  @MIDDLE_NAME nvarchar (100),              
		  @LAST_NAME nvarchar (100),              
		  @ADDRESS1 nvarchar (300),              
		  @ADDRESS2 nvarchar (200),              
		  @CITY nvarchar (140),              
		  @COUNTRY nvarchar (20),              
		  @STATE nvarchar (20),              
		  @ZIP_CODE nvarchar (40),              
		  @PHONE nvarchar (40),              
		  @EMAIL nvarchar (100),              
		  --@IS_ACTIVE nchar,              
		  --@CREATED_BY int,               
		  --@MODIFIED_BY int,               
		  --@CREATED_DATETIME datetime,               
		  --@LAST_UPDATED_TIME datetime,              
		  @CO_APPLI_OCCU int,              
		  @CO_APPLI_EMPL_NAME nvarchar (150),              
		  @CO_APPLI_EMPL_ADDRESS nvarchar (300),              
		  @CO_APPLI_YEARS_WITH_CURR_EMPL real,              
		  @CO_APPL_YEAR_CURR_OCCU real,               
		  @CO_APPL_MARITAL_STATUS nchar,               
		  @CO_APPL_DOB datetime,              
		  @CO_APPL_SSN_NO nvarchar (88),              
		  --@IS_PRIMARY_APPLICANT int,               
		  @DESC_CO_APPLI_OCCU nvarchar (4000),              
		  @BUSINESS_PHONE nvarchar (40),              
		  @MOBILE nvarchar (40),              
		  @EXT nvarchar (12),              
		  @CO_APPLI_EMPL_CITY nvarchar (140),              
		  @CO_APPLI_EMPL_COUNTRY nvarchar (20),              
		  @CO_APPLI_EMPL_STATE nvarchar (20),              
		  @CO_APPLI_EMPL_ZIP_CODE nvarchar (24),              
		  @CO_APPLI_EMPL_PHONE nvarchar (40),              
		  @CO_APPLI_EMPL_EMAIL nvarchar (100),              
		  @CO_APPLI_EMPL_ADDRESS1 nvarchar (300),              
		  @PER_CUST_MOBILE nvarchar (30),              
		  @EMP_EXT nvarchar (12),              
		  @CO_APPL_GENDER nvarchar (40),              
		  @CO_APPL_RELATIONSHIP nvarchar (50),              
		  @POSITION int,               
		  @CONTACT_CODE nvarchar (40),              
		  @ID_TYPE int,               
		  @ID_TYPE_NUMBER nvarchar (40),              
		  @NUMBER nvarchar (40),              
		  @COMPLIMENT nvarchar (40),              
		  @DISTRICT nvarchar (40),              
		  @NOTE nvarchar (500),              
		  @REGIONAL_IDENTIFICATION nvarchar(40),              
		  @REG_ID_ISSUE datetime,              
		  @ORIGINAL_ISSUE nvarchar (40),              
		  @FAX nvarchar (40),              
		  @CPF_CNPJ nvarchar(40),              
		  @APPLICANT_TYPE int, 
		  --@COMMISSION_PERCENT decimal(8,4),                  
		  --@FEES_PERCENT decimal(8,4),
		  @PRO_LABORE_PERCENT decimal(8,4)
		  
		  
		  SET @APPLICANT_ID=0                                
		 SET @IS_PRIMARY_APPLICANT=0          
	                               
		 SET @LAST_UPDATED_TIME =NULL                  
		 SET @COMMISSION_PERCENT =0.0000                  
		 SET @FEES_PERCENT = 0.0000 
		 
		 --PRINT @POLICY_NUMBER
		 --PRINT 'POLICY_NUMBER--#TEMP_POL_APPLICANT_LISTS'
		 
		  -----TEMP TABLE FOR CO-APPLICANT-------  
   
			 CREATE TABLE #TEMP_POL_APPLICANT_LISTS  
			 (  
			 ID INT IDENTITY(1,1),  
			 PolicyNO VARCHAR(MAX),  
			 EndorsementNO VARCHAR(MAX),  
			 APPLICANT_TYPE VARCHAR(MAX),  
			 CONTACT_CODE VARCHAR(MAX),  
			 FIRST_NAME VARCHAR(MAX),  
			 MIDDLE_NAME VARCHAR(MAX),  
			 LAST_NAME VARCHAR(MAX),  
			 ZIP_CODE VARCHAR(MAX),  
			 [ADDRESS] VARCHAR(MAX),  
			 NUMBER VARCHAR(MAX),  
			 COMPLEMENT VARCHAR(MAX),  
			 DISTRICT VARCHAR(MAX),  
			 CITY VARCHAR(MAX),  
			 COUNTRY VARCHAR(MAX),  
			 [State] VARCHAR(MAX),  
			 CPF_CNPJ VARCHAR(MAX),  
			 [HOME PHONE] VARCHAR(MAX),  
			 BUSINESS_PHONE VARCHAR(MAX),  
			 EXT VARCHAR(MAX),  
			 [MOBILE NUMBER]  VARCHAR(MAX),  
			 FAX VARCHAR(MAX),  
			 EMAIL VARCHAR(MAX),  
			 REGIONAL_IDENTIFICATION VARCHAR(MAX),  
			 REG_ID_ISSUE VARCHAR(MAX),  
			 ORIGINAL_ISSUE VARCHAR(MAX),  
			 POSITION VARCHAR(MAX),  
			 DOB VARCHAR(MAX),  
			 [Marital Status] VARCHAR(MAX),   
			 Gender VARCHAR(MAX),  
			 Remarks VARCHAR(MAX),  
			 TOTAL_COMMISSION_PERCENT VARCHAR(MAX),  
			 [TOTAL PRO LABORE PERCENT] VARCHAR(MAX)  
			 )  
		 
		  
		  --INSERT IN APPLICANT IN TEMP TABLE   
		  INSERT INTO #TEMP_POL_APPLICANT_LISTS  
		   (  
			PolicyNO,   
		   EndorsementNO ,  
		   APPLICANT_TYPE ,  
		   CONTACT_CODE ,  
		   FIRST_NAME ,  
		   MIDDLE_NAME ,  
		   LAST_NAME ,  
		   ZIP_CODE ,  
		   [ADDRESS] ,  
		   NUMBER ,  
		   COMPLEMENT ,  
		   DISTRICT ,  
		   CITY ,  
		   COUNTRY ,  
		   [State] ,  
		   CPF_CNPJ ,  
		   [HOME PHONE] ,  
		   BUSINESS_PHONE ,  
		   EXT ,  
		   [MOBILE NUMBER],  
		   FAX ,  
		   EMAIL ,  
		   REGIONAL_IDENTIFICATION ,  
		   REG_ID_ISSUE ,  
		   ORIGINAL_ISSUE ,  
		   POSITION ,  
		   DOB ,  
		   [Marital Status] ,   
		   Gender ,  
		   Remarks ,  
		   TOTAL_COMMISSION_PERCENT,  
		   [TOTAL PRO LABORE PERCENT] 
		   )   
			SELECT   
		   PolicyNo ,  
		   EndorsementNo ,  
		   APPLICANT_TYPE ,  
		   CONTACT_CODE ,  
		   FIRST_NAME ,  
		   MIDDLE_NAME,  
		   LAST_NAME ,  
		   ZIP_CODE ,  
		   [ADDRESS] ,  
		   NUMBER ,  
		   COMPLEMENT ,  
		   DISTRICT ,  
		   CITY ,  
		   COUNTRY ,  
		   [State] ,  
		   CPF_CNPJ ,  
		   [HOME PHONE] ,  
		   BUSINESS_PHONE ,  
		   EXT ,  
		   [MOBILE NUMBER ]  ,  
		   FAX ,  
		   EMAIL ,  
		   REGIONAL_IDENTIFICATION,  
		   REG_ID_ISSUE ,  
		   ORIGINAL_ISSUE ,  
		   POSITION ,  
		   DOB ,  
		   [Marital Status] ,   
		   Gender ,  
		   Remarks ,  
		   TOTAL_COMMISSION_PERCENT,   
		   [TOTAL PRO LABORE PERCENT]  
		     
		  FROM  ImportNew_CoApplicant --WHERE PolicyNo =@POLICY_NUMBER  
		  
		  
		  DECLARE @ROW_ID INT =1       
          DECLARE @MAXCOUNT INT = 0
          
          SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_APPLICANT_LISTS --WHERE PolicyNo =@POLICY_NUMBER  
          
           WHILE @MAXCOUNT > 0  
           BEGIN --4 
              print 'test1'
             --SET  VARIABLE TO ENTER IN LST_APPLICANT_LIST AGAINST POLICY NO  
             
			SELECT @POLICYNO = PolicyNo,  
			@EndorsementNo = EndorsementNo,  
			@APPLICANT_TYPE = CASE WHEN ISNULL(APPLICANT_TYPE,0)<>'' THEN CONVERT(INT,APPLICANT_TYPE) END,  
			@CONTACT_CODE= CONTACT_CODE,  
			@FIRST_NAME= FIRST_NAME,  
			@MIDDLE_NAME= MIDDLE_NAME,  
			@LAST_NAME = LAST_NAME,  
			@ZIP_CODE = ZIP_CODE,  
			@ADDRESS1 = [ADDRESS],  
			@NUMBER = NUMBER,  
			@COMPLIMENT = COMPLEMENT,  
			@DISTRICT = DISTRICT,  
			@CITY= CITY,  
			@COUNTRY = COUNTRY,  
			@STATE = [State],  
			@CPF_CNPJ = CPF_CNPJ,  
			@PHONE = [HOME PHONE],  
			@BUSINESS_PHONE=BUSINESS_PHONE,  
			@EXT = EXT,  
			@MOBILE =[MOBILE NUMBER],   
			@FAX= FAX,  
			@EMAIL= EMAIL,  
			@REGIONAL_IDENTIFICATION=REGIONAL_IDENTIFICATION,  
			@REG_ID_ISSUE = CASE  WHEN ISNULL(REG_ID_ISSUE,'')<>'' THEN        
			CONVERT(DATETIME,REG_ID_ISSUE,101) END,   --REG_ID_ISSUE,  
			@ORIGINAL_ISSUE = ORIGINAL_ISSUE,  
			@POSITION =POSITION,  
			@CO_APPL_DOB= CASE  WHEN ISNULL(DOB,'')<>'' THEN        
			CONVERT(DATETIME,DOB,103) END,        
			@CO_APPL_MARITAL_STATUS = CASE WHEN [Marital Status]='5932' THEN @Divorced    
					 WHEN [Marital Status]='5936' THEN @Widowed    
					 WHEN [Marital Status]='5935'  THEN  @Single   
					 WHEN [Marital Status]='5934' THEN @Separated  
					 WHEN [Marital Status]= '5933' THEN @Married  
					 End,  
			@CO_APPL_GENDER = CASE WHEN Gender = '9814' THEN @Female  
				   WHEN Gender = '9813' THEN @Male END,  
			--Remarks,  
			@COMMISSION_PERCENT = CASE WHEN ISNULL(TOTAL_COMMISSION_PERCENT,0)<>'' THEN CONVERT(decimal(8,4),TOTAL_COMMISSION_PERCENT) END,  
			--TOTAL_FEE_PERCENT 
			 @PRO_LABORE_PERCENT = CASE WHEN ISNULL([TOTAL PRO LABORE PERCENT],0)<>'' THEN CONVERT(decimal(8,4),[TOTAL PRO LABORE PERCENT]) END   --    
				   FROM #TEMP_POL_APPLICANT_LISTS  WHERE ID =@ROW_ID  
				   
				   
	   
				   
				   
			--GET CUSTOMER_ID AND POLIYC_ID        
			 SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
				FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo	
                  
               
              SET @IS_PRIMARY_APPLICANT =0  
                
               
               
               print 'test2'
               print @CUSTOMER_ID 
               print 'customer'
               print @CPF_CNPJ 
               print 'cpf'                
			   print @POLICYNO 
			   print  'POLICYNO'
			   print @ROW_ID
			   print 'ROW_ID '
           
               SELECT @APPLICANT_ID = ISNULL(MAX(APPLICANT_ID),0)+1 FROM CLT_APPLICANT_LIST 
               
               --IF APPLICANT IS NOT EXISTS IN CLT_APPLICANT INSERT FIRST IN CLT_APPLICANT 
               --THEN INSERT IN POL_APPLICANT_LIST
               IF NOT EXISTS(SELECT CUSTOMER_ID FROM CLT_APPLICANT_LIST WHERE CUSTOMER_ID =@CUSTOMER_ID AND CPF_CNPJ =@CPF_CNPJ) 
               BEGIN--5
                print 'record inserted in [CLT_APPLICANT_LIST]' 
					INSERT INTO [CLT_APPLICANT_LIST]              
					  ([APPLICANT_ID]              
					  ,[CUSTOMER_ID]              
					  ,[TITLE]              
					  ,[SUFFIX]              
					  ,[FIRST_NAME]              
					  ,[MIDDLE_NAME]              
					  ,[LAST_NAME]              
					  ,[ADDRESS1]              
					  ,[ADDRESS2]              
					  ,[CITY]              
					  ,[COUNTRY]              
					  ,[STATE]              
					  ,[ZIP_CODE]              
					  ,[PHONE]              
					  ,[EMAIL]              
					  ,[IS_ACTIVE]              
					  ,[CREATED_BY]              
					  ,[MODIFIED_BY]              
					  ,[CREATED_DATETIME]              
					  ,[LAST_UPDATED_TIME]              
					  ,[CO_APPLI_OCCU]              
					  ,[CO_APPLI_EMPL_NAME]              
					  ,[CO_APPLI_EMPL_ADDRESS]              
					  ,[CO_APPLI_YEARS_WITH_CURR_EMPL]              
					  ,[CO_APPL_YEAR_CURR_OCCU]              
					  ,[CO_APPL_MARITAL_STATUS]              
					  ,[CO_APPL_DOB]              
					  ,[CO_APPL_SSN_NO]              
					  ,[IS_PRIMARY_APPLICANT]              
					  ,[DESC_CO_APPLI_OCCU]              
					  ,[BUSINESS_PHONE]              
					  ,[MOBILE]              
					  ,[EXT]              
					  ,[CO_APPLI_EMPL_CITY]              
					  ,[CO_APPLI_EMPL_COUNTRY]              
					  ,[CO_APPLI_EMPL_STATE]              
					  ,[CO_APPLI_EMPL_ZIP_CODE]              
					  ,[CO_APPLI_EMPL_PHONE]              
					  ,[CO_APPLI_EMPL_EMAIL]              
					  ,[CO_APPLI_EMPL_ADDRESS1]              
					  ,[PER_CUST_MOBILE]             
					  ,[EMP_EXT]              
					  ,[CO_APPL_GENDER]              
					  ,[CO_APPL_RELATIONSHIP]              
					  ,[POSITION]              
					  ,[CONTACT_CODE]              
					  ,[ID_TYPE]              
					  ,[ID_TYPE_NUMBER]              
					  ,[NUMBER]              
					  ,[COMPLIMENT]              
					  ,[DISTRICT]              
					  ,[NOTE]              
					  ,[REGIONAL_IDENTIFICATION]              
					  ,[REG_ID_ISSUE]              
					  ,[ORIGINAL_ISSUE]              
					  ,[FAX]              
					  ,[CPF_CNPJ]              
					  ,[APPLICANT_TYPE])              
				   VALUES              
					( @APPLICANT_ID,              
				   @CUSTOMER_ID,               
				   @TITLE,               
				   @SUFFIX,              
				   @FIRST_NAME ,              
				   @MIDDLE_NAME ,              
				   @LAST_NAME ,              
				   @ADDRESS1 ,              
				   @ADDRESS2 ,              
				   @CITY ,              
				   @COUNTRY ,              
				   @STATE ,               
				   @ZIP_CODE ,              
				   @PHONE ,              
				   @EMAIL ,              
				   @IS_ACTIVE ,               
				  @CREATED_BY ,               
				  @MODIFIED_BY ,              
				  @CREATED_DATETIME ,              
				   @LAST_UPDATED_TIME ,              
				   @CO_APPLI_OCCU ,               
				   @CO_APPLI_EMPL_NAME ,              
				   @CO_APPLI_EMPL_ADDRESS ,              
				   @CO_APPLI_YEARS_WITH_CURR_EMPL ,              
				   @CO_APPL_YEAR_CURR_OCCU ,              
				   @CO_APPL_MARITAL_STATUS ,              
				   @CO_APPL_DOB ,              
				   @CO_APPL_SSN_NO ,               
				   @IS_PRIMARY_APPLICANT ,     
				   @DESC_CO_APPLI_OCCU ,              
				   @BUSINESS_PHONE ,              
				   @MOBILE,              
				   @EXT,              
				   @CO_APPLI_EMPL_CITY,              
				   @CO_APPLI_EMPL_COUNTRY,              
				   @CO_APPLI_EMPL_STATE,              
				   @CO_APPLI_EMPL_ZIP_CODE,              
				   @CO_APPLI_EMPL_PHONE,              
				   @CO_APPLI_EMPL_EMAIL,              
				   @CO_APPLI_EMPL_ADDRESS1,              
				  @PER_CUST_MOBILE ,              
				  @EMP_EXT ,              
				  @CO_APPL_GENDER ,              
				   @CO_APPL_RELATIONSHIP,              
				   @POSITION,              
				   @CONTACT_CODE ,              
				   @ID_TYPE ,              
				   @ID_TYPE_NUMBER ,              
				  @NUMBER ,              
				  @COMPLIMENT ,              
				  @DISTRICT ,              
				  @NOTE ,              
				  @REGIONAL_IDENTIFICATION ,              
				  @REG_ID_ISSUE ,              
				  @ORIGINAL_ISSUE ,              
				   @FAX ,              
				  @CPF_CNPJ,               
				   @APPLICANT_TYPE ) 
					
					IF @@ERROR<>0    
					ROLLBACK TRAN 
					
					--GET APPLICANT ID 
					SELECT @APPLICANT_ID = APPLICANT_ID                                 
					FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CPF_CNPJ = @CPF_CNPJ 
					print @APPLICANT_ID
					print 'APPLICANT_ID insert in pol_applicant'
					IF(@APPLICANT_ID >0 AND @CUSTOMER_ID > 1)  
					BEGIN  --6   
						--CHECK TO MAKE PRIMARY APPLICANT
						 IF NOT EXISTS(SELECT APPLICANT_ID FROM POL_APPLICANT_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND
													POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID and IS_PRIMARY_APPLICANT = 1)
					  		 SET @IS_PRIMARY_APPLICANT = 1  
						  ELSE  
							   SET @IS_PRIMARY_APPLICANT =0 						
						  
						   print 'record inserted in POL_APPLICANT_LIST' 
								INSERT INTO POL_APPLICANT_LIST                                                                
							(                                                                                                                              
							 POLICY_ID,                  
							 POLICY_VERSION_ID,                  
							 CUSTOMER_ID,                   
							 APPLICANT_ID,                  
							 CREATED_BY,                  
							 MODIFIED_BY,                  
							 CREATED_DATETIME,                  
							 LAST_UPDATED_TIME,                  
							 IS_PRIMARY_APPLICANT,                  
							 COMMISSION_PERCENT,                  
							 FEES_PERCENT,
							 PRO_LABORE_PERCENT                                    
							)                       
							  VALUES                                                                                                                             
						   ( 
							 @POLICY_ID,                  
							 @POLICY_VERSION_ID,                  
							 @CUSTOMER_ID,                  
							 @APPLICANT_ID,                
							 @CREATED_BY,                  
							 @MODIFIED_BY,                   
							 @CREATED_DATETIME,                  
							 @LAST_UPDATED_TIME,                  
							 @IS_PRIMARY_APPLICANT,                  
							 @COMMISSION_PERCENT,                  
							 @FEES_PERCENT,
							 @PRO_LABORE_PERCENT                  
							)    
	     
						   IF @@ERROR<>0    
						   ROLLBACK TRAN  
					        
					END --6 END FOR INSERT IN PO_APPLICANT_LIST   
               
               END--5
		   
		       ELSE ---IF EXISTS IN CLT THEN INSERT DIRECTLY IN POL_APPLICANT LIST
		       BEGIN--7
				--GET APPLICANT ID 
					SELECT @APPLICANT_ID = APPLICANT_ID                                 
					FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CPF_CNPJ = @CPF_CNPJ 
					
					
					--CHECK TO MAKE PRIMARY APPLICANT
					 IF NOT EXISTS(SELECT APPLICANT_ID FROM POL_APPLICANT_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND
												POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID and IS_PRIMARY_APPLICANT = 1)
				  		 SET @IS_PRIMARY_APPLICANT = 1  
					  ELSE  
						   SET @IS_PRIMARY_APPLICANT =0
					print 'Record inserted into POL_APPLICANT_LIST without insert into clt_applicant_list'
					
					IF(@APPLICANT_ID >0 AND @CUSTOMER_ID > 1)  
					BEGIN  --8    
						INSERT INTO POL_APPLICANT_LIST                                                                
						(                                                                                                                              
						 POLICY_ID,                  
						 POLICY_VERSION_ID,                  
						 CUSTOMER_ID,                   
						 APPLICANT_ID,                  
						 CREATED_BY,                  
						 MODIFIED_BY,                  
						 CREATED_DATETIME,                  
						 LAST_UPDATED_TIME,                  
						 IS_PRIMARY_APPLICANT,                  
						 COMMISSION_PERCENT,                  
						 FEES_PERCENT,
						 PRO_LABORE_PERCENT                                    
						)                       
						  VALUES                                                                                                                             
					   ( 
						 @POLICY_ID,                  
						 @POLICY_VERSION_ID,                  
						 @CUSTOMER_ID,                  
						 @APPLICANT_ID,                
						 @CREATED_BY,                  
						 @MODIFIED_BY,                   
						 @CREATED_DATETIME,                  
						 @LAST_UPDATED_TIME,                  
						 @IS_PRIMARY_APPLICANT,                  
						 @COMMISSION_PERCENT,                  
						 @FEES_PERCENT,
						 @PRO_LABORE_PERCENT                  
						)    
     
					   IF @@ERROR<>0    
					   ROLLBACK TRAN  
					        
					   END --8 END FOR INSERT IN PO_APPLICANT_LIST 
		       END--7
		       
		     
		          
           SET @MAXCOUNT =@MAXCOUNT -1             
          SET @ROW_ID = @ROW_ID +1       
       END--4 END OF LOOP
        
      
  print 'exit from applicant' 
  -------      
           
           
------------------------INSERT INTO REMUNERATION------------------------------------          
 --1. WE HAVE INSERT ALL RECORDS INTO TEMP TABLE FROM DUMP TABLE        
   --2. WE HAVE TAKE A LOOP UPTO THE TOTAL NO. OF RECORDS OF TEMP TABLE\        
   --3. PICK ONE POLICY NO AND GET CUSTOMER_ID         
   --4. FETCH VALUES FOR THIS POLICY NO FROM DUMP TABLE THEN INSERT INTO OUR SYSTEM        
        
   DECLARE                  
   @REMUNERATION_ID int,                  
   --@CUSTOMER_ID int,                  
   --@POLICY_ID int,                  
   --@POLICY_VERSION_ID int,                  
   @BROKER_ID int,                  
   --@COMMISSION_PERCENT numeric(8,0),                  
   @COMMISSION_TYPE int,                  
   --@IS_ACTIVE nchar,                  
   --@CREATED_BY int,                  
   --@CREATED_DATETIME datetime,                  
   --@MODIFIED_BY int,                  
   --@LAST_UPDATED_DATETIME datetime,                  
   @BRANCH numeric(8,0),                  
   @NAME nvarchar(200),                  
   @AMOUNT decimal(12,2),                  
   @LEADER int,                  
   @RISK_ID int,                  
   @CO_APPLICANT_ID int,           
   --@PolicyNo VARCHAR(MAX),        
   --@EndorsementNo VARCHAR(MAX),          
   @BROKER_NAME VARCHAR(MAX)        
          
            
           
   CREATE TABLE #TEMP_REMUNERATION        
  (        
   ID INT IDENTITY(1,1),        
   PolicyNo VARCHAR(MAX),        
   EndorsementNo VARCHAR(MAX),        
   CoApplicantID VARCHAR(MAX),        
   COMMISSION_TYPE VARCHAR(MAX),        
   BROKER_ID VARCHAR(MAX),        
   BRANCH_NUMBER VARCHAR(MAX),        
   LEADER VARCHAR(MAX),        
   BROKER_NAME VARCHAR(MAX),        
   COMMISSION_PERCENT VARCHAR(MAX)        
  )        
   INSERT INTO #TEMP_REMUNERATION        
  (        
   PolicyNo,        
   EndorsementNo,        
   CoApplicantID,        
   COMMISSION_TYPE,        
   BROKER_ID,        
   BRANCH_NUMBER,        
   LEADER ,        
   BROKER_NAME,        
   COMMISSION_PERCENT        
  )         
        
  SELECT         
  POLICYNO,EndorsementNO,CoApplicantID,[COMMISSION_TYPE],[BROKER_ID],        
  [BRANCH NUMBER],[LEADER],[BROKER NAME],[COMMISSION_PERCENT]          
  FROM IMPORTNEW_REMUNERATION        
          
          
           
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_REMUNERATION         
   SET @ROW_ID=1        
   SET @RISK_ID =0      
           
   --SELECT CONVERT(DECIMAL(8,0),  SUBSTRING([BRANCH NUMBER],1,LEN([BRANCH NUMBER]))) FROM  Import_remuneration        
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
 SELECT @PolicyNo = PolicyNo,        
    @EndorsementNo = EndorsementNo,        
       @CO_APPLICANT_ID = CASE WHEN ISNULL(CoApplicantID,'')<>'' THEN CONVERT(INT, SUBSTRING(CoApplicantID,1,4)) END,        
       @COMMISSION_TYPE =  CASE WHEN ISNULL(COMMISSION_TYPE,'')<>'' THEN CONVERT(INT,SUBSTRING(COMMISSION_TYPE,LEN(COMMISSION_TYPE)-1,4)) END,        
       @BROKER_ID =  CASE WHEN ISNULL(BROKER_ID,'')<>'' THEN CONVERT(INT,SUBSTRING(BROKER_ID,LEN(BROKER_ID)-1,4)) END,         
       --@BRANCH= CASE WHEN ISNULL(BRANCH_NUMBER,'')<>'' THEN CONVERT(NUMERIC(8,0),BRANCH_NUMBER) END,         
       @LEADER =  CASE WHEN ISNULL(LEADER,'') = 'Y' THEN 10963     
        WHEN ISNULL(LEADER,'') = 'N' THEN 10964   END,     
       --BROKER_NAME,        
       @COMMISSION_PERCENT =  CASE WHEN ISNULL(COMMISSION_PERCENT,'')<>'' THEN CONVERT(DECIMAL(8,4),COMMISSION_PERCENT) END                
    FROM #TEMP_REMUNERATION WHERE ID=@ROW_ID        
            
    --GET CUSTOMER_ID AND POLIYC_ID        
    SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
     FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
             
    --SET REMUNERATION ID        
    SELECT @REMUNERATION_ID = ISNULL(MAX(REMUNERATION_ID),0)+1 FROM POL_REMUNERATION WITH (NOLOCK)                  
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID         
          
   --SET  OTHER FIELD              
    SET  @IS_ACTIVE='Y'                  
    SET  @CREATED_BY=NULL                  
    SET  @CREATED_DATETIME='2010-10-27 10:08:08.433'                  
    SET  @MODIFIED_BY=198                  
    SET  @LAST_UPDATED_DATETIME='2010-10-27 10:08:08.433'          
    SET  @NAME='A 1-1-0- (Active)'                 
    SET  @AMOUNT=NULL          
    SET  @RISK_ID=1                  
          
      IF(@CUSTOMER_ID > 0 AND @POLICY_ID > 0)        
      BEGIN        
    INSERT INTO [POL_REMUNERATION]                  
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
       ,[MODIFIED_BY]                  
       ,[LAST_UPDATED_DATETIME]                  
       ,[BRANCH]                  
       ,[NAME]                  
       ,[AMOUNT]                  
       ,[LEADER]                  
       ,[RISK_ID]                  
       ,[CO_APPLICANT_ID])                  
    VALUES                  
     ( @REMUNERATION_ID                  
    ,@CUSTOMER_ID                  
    ,@POLICY_ID                  
    ,@POLICY_VERSION_ID                  
    ,@BROKER_ID                  
    ,@COMMISSION_PERCENT                  
    ,@COMMISSION_TYPE                  
    ,@IS_ACTIVE                  
    ,@CREATED_BY                  
    ,@CREATED_DATETIME                  
    ,@MODIFIED_BY                  
    ,@LAST_UPDATED_DATETIME                  
    ,@BRANCH                  
    ,@NAME                  
    ,@AMOUNT                  
    ,@LEADER                  
    ,@RISK_ID                  
    ,@CO_APPLICANT_ID                             
    )         
       
     IF @@ERROR<>0    
 ROLLBACK TRAN       
   END  --CHECK CUSTOMER_ID AND POLICY ID VALUES          
   SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--END OF LOOP FOR INSERTING IN POL_REMUNERATION         
         
     
     
  
 ------------------------INSERT INTO POL_REINSURANCE_INFO------------------------------------     
 DECLARE     
   @REINSURANCE_ID int,     
 --@COMPANY_ID int ,    
 --@CUSTOMER_ID int,     
 --@POLICY_ID int,     
 --@POLICY_VERSION_ID smallint,     
 @CONTRACT_FACULTATIVE int,     
 @CONTRACT int,     
 @REINSURANCE_CEDED decimal (18,2),    
 @REINSURANCE_COMMISSION decimal (18,2)    
 --@IS_ACTIVE nchar(2),     
 --@CREATED_BY int,     
 --@CREATED_DATETIME datetime,     
 --@MODIFIED_BY int,     
 --@LAST_UPDATED_DATETIME datetime     
             
               
               
  SET  @REINSURANCE_ID =377              
  SET  @COMPANY_ID = 64              
  --SET  @CUSTOMER_ID = 2525              
  --SET  @POLICY_ID = 1              
  --SET  @POLICY_VERSION_ID =1              
  SET  @CONTRACT_FACULTATIVE = 14627                
  SET  @CONTRACT = 64              
  SET  @REINSURANCE_CEDED =12.00              
  SET  @REINSURANCE_COMMISSION =1.00              
  SET @IS_ACTIVE ='Y'    
  SET @CREATED_BY =198     
  --SET @CREATED_DATETIME     
  --SET @MODIFIED_BY     
  --SET @LAST_UPDATED_DATETIME    
      
  CREATE TABLE #TEMP_POL_REINSURANCE_INFO        
   (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),        
    EndorsementNo VARCHAR(MAX),            
    [Reinsurance Type] VARCHAR(MAX),     
    ReinsuranceContractCode VARCHAR(MAX),     
    ReinsurerId VARCHAR(MAX),    
    ReinsuranceShareRate VARCHAR(MAX),    
    ReinsuranceFee VARCHAR(MAX)    
        
   )        
    INSERT INTO #TEMP_POL_REINSURANCE_INFO        
   (           
    PolicyNo, EndorsementNo,[Reinsurance Type],    
    ReinsuranceContractCode,ReinsurerId,ReinsuranceShareRate,ReinsuranceFee           
   )     
   SELECT PolicyNo, EndorsementNo,[Reinsurance Type],    
    ReinsuranceContractCode,ReinsurerId,ReinsuranceShareRate,ReinsuranceFee    
 FROM  IMPORTNEW_Reinsurance    
     
 SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_REINSURANCE_INFO        
    SET @ROW_ID=1        
    
  SET @RISK_ID =0   
          
   WHILE @MAXCOUNT >0        
   BEGIN          
    SELECT @PolicyNo= POLICYNO,        
           @EndorsementNo = EndorsementNo,                  
           --ReinsuranceContract code               
           @COMPANY_ID =  CASE when ISNULL(ReinsurerId,'') <>'' THEN CONVERT(INT,ReinsurerId) END, --ReinsurerId, -- DOUBT    
           @REINSURANCE_CEDED = CASE  when ISNULL(ReinsuranceShareRate,'')<>'' THEN CONVERT(DECIMAL(18,2),ReinsuranceShareRate) END,    
           @REINSURANCE_COMMISSION  = CASE  when ISNULL(ReinsuranceFee,'')<>'' THEN CONVERT(DECIMAL(18,2),ReinsuranceFee) END    
     FROM #TEMP_POL_REINSURANCE_INFO WHERE ID = @ROW_ID    
         
     --GET CUSTOMER_ID AND POLIYC_ID FOR ABOVE POLICYNO        
    SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
     FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo      
         
         
     SELECT @REINSURANCE_ID  = ISNULL(MAX(REINSURANCE_ID),0)+1 FROM POL_REINSURANCE_INFO WHERE    
     CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
         
  INSERT INTO [POL_REINSURANCE_INFO]    
           ([REINSURANCE_ID]    
           ,[COMPANY_ID]    
           ,[CUSTOMER_ID]    
           ,[POLICY_ID]    
           ,[POLICY_VERSION_ID]    
           ,[CONTRACT_FACULTATIVE]    
           ,[CONTRACT]    
           ,[REINSURANCE_CEDED]    
           ,[REINSURANCE_COMMISSION]    
           ,[IS_ACTIVE]    
           ,[CREATED_BY]    
           ,[CREATED_DATETIME]    
           ,[MODIFIED_BY]    
           ,[LAST_UPDATED_DATETIME])    
     VALUES     
     (  @REINSURANCE_ID,    
    @COMPANY_ID,    
    @CUSTOMER_ID,    
    @POLICY_ID,    
    @POLICY_VERSION_ID,    
    @CONTRACT_FACULTATIVE,    
    @CONTRACT,    
    @REINSURANCE_CEDED,    
    @REINSURANCE_COMMISSION,    
    @IS_ACTIVE,    
    @CREATED_BY,    
    @CREATED_DATETIME,    
    @MODIFIED_BY,    
    @LAST_UPDATED_DATETIME           
             
     )  
       
     IF @@ERROR<>0    
     ROLLBACK TRAN        
         
   SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--END OF LOOP FOR POL_CO_INSURANCE       
  
  
  
  /*   
   --------------INSERT INTO POL_CO_INSURANCE------------------------    
  DECLARE             
 @COINSURANCE_ID int,             
 --@COMPANY_ID int ,            
 --@CUSTOMER_ID int,             
 --@POLICY_ID int,             
 --@POLICY_VERSION_ID int,             
 @CO_INSURER_NAME nvarchar (100),            
 @LEADER_FOLLOWER int,             
 @COINSURANCE_PERCENT decimal (18,2),            
 @COINSURANCE_FEE decimal (18,2),            
 @BROKER_COMMISSION decimal (18,2),            
 @TRANSACTION_ID nvarchar (50),            
 @LEADER_POLICY_NUMBER nvarchar (50),            
 --@IS_ACTIVE nchar (2),            
 --@CREATED_BY int,             
 --@CREATED_DATETIME datetime,        
 --@MODIFIED_BY int,             
 --@LAST_UPDATED_DATETIME datetime,             
 @BRANCH_COINSURANCE_ID int             
             
             
  SET  @COINSURANCE_ID =377            
  SET  @COMPANY_ID = 64            
  --SET  @CUSTOMER_ID = 2525            
  --SET  @POLICY_ID = 1            
  --SET  @POLICY_VERSION_ID =1            
  SET  @CO_INSURER_NAME = NULL              
  SET  @LEADER_FOLLOWER = 14548            
  SET  @COINSURANCE_PERCENT =NULL            
  SET  @COINSURANCE_FEE =NULL            
  SET  @BROKER_COMMISSION =NULL            
  SET  @TRANSACTION_ID =NULL            
  SET  @LEADER_POLICY_NUMBER =NULL            
  SET  @IS_ACTIVE ='Y'            
  SET  @CREATED_BY =NULL            
  SET  @CREATED_DATETIME ='2010-10-27 18:59:22.290'            
  SET  @MODIFIED_BY =NULL            
  SET  @LAST_UPDATED_DATETIME =NULL            
  SET  @BRANCH_COINSURANCE_ID =NULL    
     
  CREATE TABLE #TEMP_POL_CO_INSURANCE        
   (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),        
    EndorsementNo VARCHAR(MAX),        
    CoinsuranceType VARCHAR(MAX),        
 BranchCoinsurerId VARCHAR(MAX),        
 CoinsurerId  VARCHAR(MAX),        
 CoinsurerShareRate VARCHAR(MAX),        
 CoinsuranceFee VARCHAR(MAX)        
     
   )        
    INSERT INTO #TEMP_POL_CO_INSURANCE        
   (           
    PolicyNo, EndorsementNo,CoinsuranceType, BranchCoinsurerId, CoinsurerId ,  
    CoinsurerShareRate,  CoinsuranceFee          
   )         
        
    SELECT PolicyNo,EndorsementNo,CoinsuranceType,BranchCoinsurerId,CoinsurerId,  
    CoinsurerShareRate,CoinsuranceFee                        
    FROM  ImportNew_Policy_Info   
   
    SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_CO_INSURANCE        
    SET @ROW_ID=1        
           
    WHILE @MAXCOUNT >0        
    BEGIN    
     
    SELECT      
    @POLICYNO =   PolicyNo,  
    @EndorsementNo = EndorsementNo ,  
    @LEADER_FOLLOWER = CASE WHEN ISNULL(CoinsuranceType,0)<>'' THEN CONVERT(INT,CoinsuranceType) END,  
    @BRANCH_COINSURANCE_ID = CASE WHEN ISNULL(BranchCoinsurerId,0)<>'' THEN CONVERT(INT,BranchCoinsurerId) END,  
    @COINSURANCE_ID = CASE WHEN ISNULL(CoinsurerId,0)<>'' THEN CONVERT(INT,CoinsurerId) END,    
    @COINSURANCE_PERCENT = CASE WHEN ISNULL(CoinsurerShareRate,0)<>'' THEN CONVERT(DECIMAL,CoinsurerShareRate) END,  
    @COINSURANCE_FEE = CASE WHEN ISNULL(CoinsuranceFee,0)<>'' THEN CONVERT(DECIMAL,CoinsuranceFee) END      
    FROM #TEMP_POL_CO_INSURANCE WHERE ID=@ROW_ID  
        
     
     
     
     
     
    --GET CUSTOMER_ID AND POLIYC_ID FOR ABOVE POLICYNO        
    SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
     FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo     
       
    INSERT INTO [POL_CO_INSURANCE]            
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
           ,[MODIFIED_BY]            
           ,[LAST_UPDATED_DATETIME]            
           ,[BRANCH_COINSURANCE_ID])            
     VALUES            
           (@COINSURANCE_ID            
   ,@COMPANY_ID            
   ,@CUSTOMER_ID            
   ,@POLICY_ID            
   ,@POLICY_VERSION_ID            
   ,@CO_INSURER_NAME            
   ,@LEADER_FOLLOWER            
   ,@COINSURANCE_PERCENT            
   ,@COINSURANCE_FEE            
   ,@BROKER_COMMISSION            
   ,@TRANSACTION_ID            
   ,@LEADER_POLICY_NUMBER            
   ,@IS_ACTIVE            
   ,@CREATED_BY            
   ,@CREATED_DATETIME            
   ,@MODIFIED_BY            
   ,@LAST_UPDATED_DATETIME            
   ,@BRANCH_COINSURANCE_ID            
   )  
      
    IF @@ERROR<>0    
    ROLLBACK TRAN  
      
   SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
 END--END OF LOOP   
      
  */    
  
   SET @RISK_ID =1   
       
   --SELECT * FROM POL_CO_INSURANCE        
           
--------------INSERT INTO DISCOUNT_SURCHARGE------------------------        
     Declare                  
    --@CUSTOMER_ID int,                   
    --@POLICY_ID int,                   
    --@POLICY_VERSION_ID int,                  
    --@RISK_ID int,                  
    @DISCOUNT_ROW_ID int,                  
    @DISCOUNT_ID int,                  
    @PERCENTAGE decimal (12,4)                  
    --@IS_ACTIVE nchar (2),                  
    --@CREATED_BY int,                   
    --@CREATED_DATETIME datetime,                  
    --@MODIFIED_BY int ,                  
    --@LAST_UPDATED_DATETIME datetime        
        
        
     CREATE TABLE #TEMP_DISCOUNT_SURCHARGE        
   (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),        
    EndorsementNo VARCHAR(MAX),        
    Item VARCHAR(MAX),        
    DiscountID VARCHAR(MAX),        
    DiscountRate VARCHAR(MAX)        
   )        
    INSERT INTO #TEMP_DISCOUNT_SURCHARGE        
   (           
    PolicyNo, EndorsementNo,Item, DiscountID, DiscountRate           
   )         
        
    SELECT PolicyNo,EndorsementNo,Item,DiscountID,DiscountRate                    
    FROM  ImportNew_DiscountSurcharge        
            
           
    SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_DISCOUNT_SURCHARGE        
    SET @ROW_ID=1        
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT @PolicyNo= POLICYNO,        
     @RISK_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,        
     @DISCOUNT_ID =  CASE WHEN ISNULL(DISCOUNTID,0)<>'' THEN CONVERT(INT,DISCOUNTID) END,        
     @PERCENTAGE= CASE WHEN ISNULL(DiscountRate,0)<>'' THEN CONVERT(NUMERIC(12,4),DiscountRate) END        
     FROM #TEMP_DISCOUNT_SURCHARGE  WHERE ID = @ROW_ID      
             
    --SET  OTHER FIELD              
     SET  @IS_ACTIVE='Y'                  
     SET  @CREATED_BY=NULL                  
     SET  @CREATED_DATETIME='2010-10-27 10:08:08.433'                  
     SET  @MODIFIED_BY=198                  
     SET  @LAST_UPDATED_DATETIME=GETDATE()          
             
            
    --GET CUSTOMER_ID AND POLIYC_ID FOR ABOVE POLICYNO        
    SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
     FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo      
         
         
     SELECT @DISCOUNT_ROW_ID  = ISNULL(MAX(DISCOUNT_ROW_ID),0)+1 FROM POL_DISCOUNT_SURCHARGE WHERE    
     CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
             
  IF(@CUSTOMER_ID > 0 AND @POLICY_ID > 0)        
   BEGIN          
     INSERT INTO [POL_DISCOUNT_SURCHARGE]                  
     (    [CUSTOMER_ID]                  
      ,[POLICY_ID]                  
      ,[POLICY_VERSION_ID]                  
      ,[RISK_ID]                  
      ,[DISCOUNT_ROW_ID]                  
      ,[DISCOUNT_ID]                  
      ,[PERCENTAGE]                  
      ,[IS_ACTIVE]                  
      ,[CREATED_BY]                  
      ,[CREATED_DATETIME]            
      ,[MODIFIED_BY]                  
      ,[LAST_UPDATED_DATETIME]        
    )                  
          VALUES                  
    ( @CUSTOMER_ID,                   
    @POLICY_ID,                   
    @POLICY_VERSION_ID,                  
    @RISK_ID ,                  
    @DISCOUNT_ROW_ID,                  
    @DISCOUNT_ID,                  
    @PERCENTAGE,                  
    @IS_ACTIVE,                  
    @CREATED_BY,                   
    @CREATED_DATETIME,                  
    @MODIFIED_BY,                  
    @LAST_UPDATED_DATETIME                  
     )  
      IF @@ERROR<>0    
      ROLLBACK TRAN        
     END  --CHECK CUSTOMER_ID AND POLICY ID VALUES          
             
   SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
 END--END OF LOOP FOR INSERTING IN POL_DISCOUNT_SURCHARGE         
       

           
------------------------INSERT INTO LOCATION------------------------------------                  
  DECLARE                   
   --@CUSTOMER_ID int,                  
   --@POLICY_ID int,                  
   --@POLICY_VERSION_ID smallint,                  
   @LOCATION_ID smallint,                  
   @LOC_NUM int,                   
   @IS_PRIMARY nchar,        
                  
   @LOC_ADD1 nvarchar(150),                  
   @LOC_ADD2 nvarchar (150),                  
   @LOC_CITY nvarchar (150),                  
   @LOC_COUNTY nvarchar (150),                  
   @LOC_STATE nvarchar (10),                  
   @LOC_ZIP nvarchar (22),                  
   @LOC_COUNTRY nvarchar (10),                  
   @PHONE_NUMBER nvarchar (40),                  
   @FAX_NUMBER nvarchar (40),                  
   @DEDUCTIBLE nvarchar (40),                  
   @NAMED_PERILL int,                   
   @DESCRIPTION varchar (1000),                  
   --@IS_ACTIVE nchar,                   
   --@CREATED_BY int ,                  
   --@CREATED_DATETIME datetime ,                  
   --@MODIFIED_BY int ,                  
   --@LAST_UPDATED_DATETIME datetime ,                  
   @LOC_TERRITORY nvarchar (10),                  
   @LOCATION_TYPE int ,                  
   @RENTED_WEEKLY varchar (10),                  
   @WEEKS_RENTED varchar (10),                  
   @LOSSREPORT_ORDER int ,                  
   @LOSSREPORT_DATETIME datetime,                   
   @REPORT_STATUS char (1),            
   @CAL_NUM nvarchar (40),                  
   --@NAME nvarchar (60),                  
   --@NUMBER nvarchar (40),                  
   --@DISTRICT nvarchar (100),                  
   @OCCUPIED int,                  
   --@EXT nvarchar (20),                  
   @CATEGORY nvarchar (40),                  
   @ACTIVITY_TYPE int ,                  
   @CONSTRUCTION int ,                  
   @SOURCE_LOCATION_ID int ,                  
   @IS_BILLING nchar (2)        
   --SET @CUSTOMER_ID = 2525                   
   --SET @POLICY_ID = 1                  
   --SET @POLICY_VERSION_ID =1                  
   --SET @LOCATION_ID = 4                  
   SET @LOC_NUM = 4                  
   SET @IS_PRIMARY =NULL         
   --SET FROM DUMP TABLE                   
   --SET @LOC_ADD1 ='RUA DIREITA'        
                     
   SET @LOC_ADD2 =NULL                  
   --SET @LOC_CITY ='SAO PAULO'                  
   SET @LOC_COUNTY = NULL                  
   --SET @LOC_STATE ='71'                  
  -- SET @LOC_ZIP='1002000'                  
   --SET @LOC_COUNTRY='5'                  
   SET @PHONE_NUMBER=NULL                  
   SET @FAX_NUMBER=NULL                  
   SET @DEDUCTIBLE=NULL               
   SET @NAMED_PERILL=NULL                  
   SET @DESCRIPTION=NULL                   
   SET @IS_ACTIVE='Y'                  
   SET @CREATED_BY=198                  
   SET @CREATED_DATETIME='2010-10-27 12:01:25.053'                  
   SET @MODIFIED_BY=NULL                  
   SET @LAST_UPDATED_DATETIME=GETDATE()                  
   SET @LOC_TERRITORY=NULL                  
   SET @LOCATION_TYPE=0                   
   SET @RENTED_WEEKLY=NULL                  
   SET @WEEKS_RENTED=NULL                  
   SET @LOSSREPORT_ORDER=NULL                  
   SET @LOSSREPORT_DATETIME=NULL                  
   SET @REPORT_STATUS=NULL               
   SET @CAL_NUM='Calculation'                  
   SET @NAME='Building Name'                  
   SET @NUMBER='12345'        
   --SET FROM DUMP TABLE        
   --SET @DISTRICT='SE'        
   --SET @OCCUPIED=0                  
   SET @EXT=NULL                  
   SET @CATEGORY=NULL         
   --SET FROM DUMP TABLE                  
   --SET @ACTIVITY_TYPE=0                  
   SET @CONSTRUCTION=0                  
   SET @SOURCE_LOCATION_ID=2                   
   SET @IS_BILLING='N'          
           
   ------TEMP TABLE FOR POL_LOCATIONS -----------        
  CREATE TABLE #TEMP_POL_LOCATION        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),  
   LocationAddress VARCHAR(MAX),        
   LocationNumber VARCHAR(MAX),        
   LocationComplement VARCHAR(MAX),        
   LocationDistrict VARCHAR(MAX),        
   LocationCity VARCHAR(MAX),        
   LocationState VARCHAR(MAX),        
   LocationPostalCode VARCHAR(MAX),        
   LocationCountry VARCHAR(MAX),        
   LocationContructionType VARCHAR(MAX),        
   LocationActivity VARCHAR(MAX),        
   LocationOccupiedAs VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_POL_LOCATION        
  (        
   POLICYNO,ENDORSEMENTNO,ITEM, LocationAddress,LocationNumber,LocationComplement,        
   LocationDistrict,LocationCity,LocationState,        
   LocationPostalCode,LocationCountry,LocationContructionType,        
   LocationActivity,LocationOccupiedAs        
  )         
        
  SELECT PolicyNo,EndorsementNo,Item,LocationAddress,LocationNumber,LocationComplement,LocationDistrict,LocationCity,        
   LocationState,LocationPostalCode,LocationCountry,LocationContructionType,LocationActivity,LocationOccupiedAs        
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
           
   --DECLARE @ROW_ID INT        
   --DECLARE @MAXCOUNT INT        
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_LOCATION  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @RISK_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END ,   
    @LOC_ADD1 = LocationAddress, --SUBSTRING(LocationAddress,1,40),            
    @NUMBER= LocationNumber, --SUBSTRING(LocationNumber,1,40),        
    @LOC_ADD2= LocationComplement, --SUBSTRING(LocationComplement,1,150),        
    @DISTRICT= LocationDistrict, --SUBSTRING(LocationDistrict,1,100),        
    @LOC_CITY = LocationCity, --SUBSTRING(LocationCity,1,150),        
    @LOC_STATE =CASE WHEN ISNULL(LocationState,'')<>'' THEN CONVERT(INT,LocationState) END, --SUBSTRING(LocationState,1,10),        
    @LOC_ZIP=LocationPostalCode, --SUBSTRING(LocationPostalCode,1,22),        
    @LOC_COUNTRY= CASE WHEN ISNULL(LocationCountry,'')<>'' THEN CONVERT(INT,LocationCountry) END, --SUBSTRING(LocationCountry,1,10),        
    @CONSTRUCTION =CASE WHEN ISNULL(LocationContructionType,'')<>'' THEN CONVERT(INT,LocationContructionType) END, --CONVERT(INT,LocationContructionType),        
    @ACTIVITY_TYPE= CASE WHEN ISNULL(LocationActivity,'')<>'' THEN CONVERT(INT,LocationActivity) END, --CONVERT(INT,LocationActivity),        
    @OCCUPIED=CASE WHEN ISNULL(LocationOccupiedAs,'')<>'' THEN CONVERT(INT,LocationOccupiedAs) END  --CONVERT(INT,LocationOccupiedAs)         
    FROM #TEMP_POL_LOCATION WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
            
            
    --CONSIDER MULTIPLE FOR POLICY         
    --IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID)        
    IF(@CUSTOMER_ID >0 )        
    BEGIN        
     INSERT INTO [POL_LOCATIONS]                  
         ([CUSTOMER_ID]                  
         ,[POLICY_ID]                  
         ,[POLICY_VERSION_ID]                  
         ,[LOCATION_ID]                  
         ,[LOC_NUM]                  
         ,[IS_PRIMARY]                  
         ,[LOC_ADD1]                  
         ,[LOC_ADD2]                  
         ,[LOC_CITY]                  
         ,[LOC_COUNTY]                  
         ,[LOC_STATE]                  
         ,[LOC_ZIP]                  
         ,[LOC_COUNTRY]                  
         ,[PHONE_NUMBER]               ,[FAX_NUMBER]                  
         ,[DEDUCTIBLE]                  
         ,[NAMED_PERILL]                  
         ,[DESCRIPTION]                  
         ,[IS_ACTIVE]                  
         ,[CREATED_BY]                  
         ,[CREATED_DATETIME]                  
         ,[MODIFIED_BY]                  
         ,[LAST_UPDATED_DATETIME]                  
         ,[LOC_TERRITORY]                  
         ,[LOCATION_TYPE]                  
         ,[RENTED_WEEKLY]                  
         ,[WEEKS_RENTED]                  
         ,[LOSSREPORT_ORDER]                  
         ,[LOSSREPORT_DATETIME]                  
         ,[REPORT_STATUS]                  
         ,[CAL_NUM]                  
         ,[NAME]                  
         ,[NUMBER]                  
         ,[DISTRICT]                  
         ,[OCCUPIED]                  
         ,[EXT]                  
         ,[CATEGORY]                  
         ,[ACTIVITY_TYPE]                  
         ,[CONSTRUCTION]                  
         ,[SOURCE_LOCATION_ID]                  
         ,[IS_BILLING])                  
      VALUES                  
         ( @CUSTOMER_ID ,                   
       @POLICY_ID  ,                  
       @POLICY_VERSION_ID ,                  
       @RISK_ID, --@LOCATION_ID  ,                  
       @LOC_NUM,                  
       @IS_PRIMARY ,                  
       @LOC_ADD1,                   
       @LOC_ADD2 ,                  
       @LOC_CITY,                  
       @LOC_COUNTRY,                  
       @LOC_STATE,                  
       @LOC_ZIP,                  
       @LOC_COUNTRY,                  
       @PHONE_NUMBER,                  
       @FAX_NUMBER,                  
       @DEDUCTIBLE,                  
       @NAMED_PERILL,                  
       @DESCRIPTION,                  
       @IS_ACTIVE,                  
       @CREATED_BY,                  
       @CREATED_DATETIME,                  
       @MODIFIED_BY,                  
       @LAST_UPDATED_DATETIME,                  
       @LOC_TERRITORY,                  
       @LOCATION_TYPE,                  
       @RENTED_WEEKLY,                  
       @WEEKS_RENTED,                  
       @LOSSREPORT_ORDER,                  
       @LOSSREPORT_DATETIME,                  
       @REPORT_STATUS,                  
       @CAL_NUM,                  
       @NAME,                  
       @NUMBER,                  
       @DISTRICT,                  
       @OCCUPIED,                  
       @EXT,                  
       @CATEGORY,                  
       @ACTIVITY_TYPE,                  
       @CONSTRUCTION,                  
                            
       @SOURCE_LOCATION_ID,                  
       @IS_BILLING)  
        
      IF @@ERROR<>0    
      ROLLBACK TRAN           
    END        
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--END OF LOOP FOR POL_LOCATION INSERT FOR INDIVIDUAL CUSTOMER        
        
          
           
            
        
------Risks Details------------------



           
----------------------TEMP TABLE FOR POL_COMMODITY_INFO ---------------------------------      
             
  CREATE TABLE #TEMP_POL_COMMODITY_INFO        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),  
   ITEM VARCHAR(MAX),
   TransportationInsuredObject VARCHAR(MAX),        
   TransportationConveyancetype VARCHAR(MAX),         
   TransportationDepartingDate VARCHAR(MAX),        
   TransportationOriginCountry VARCHAR(MAX),         
   TransportationOriginState VARCHAR(MAX),         
   TransportationOriginCity VARCHAR(MAX),         
   TransportationDestinationCountry VARCHAR(MAX),         
   TransportationDestinationState VARCHAR(MAX),         
   TransportationDestinationCity VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_POL_COMMODITY_INFO        
  (        
   POLICYNO,  
   ENDORSEMENTNO,  
   ITEM,
   TransportationInsuredObject,         
   TransportationConveyancetype,         
   TransportationDepartingDate ,        
   TransportationOriginCountry ,         
   TransportationOriginState ,         
   TransportationOriginCity ,         
   TransportationDestinationCountry,         
   TransportationDestinationState ,         
   TransportationDestinationCity         
  )        
  SELECT PolicyNo,  
   EndorsementNo,  
   Item,
   TransportationInsuredObject,        
   TransportationConveyancetype,         
   TransportationDepartingDate ,        
   TransportationOriginCountry ,         
   TransportationOriginState ,         
   TransportationOriginCity ,         
   TransportationDestinationCountry,         
   TransportationDestinationState ,         
   TransportationDestinationCity        
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
          
          
  --DECLARE @ROW_ID INT        
   --DECLARE @MAXCOUNT INT        
   DECLARE
    --@CUSTOMER_ID	int,
	--@POLICY_ID	int,
	--@POLICY_VERSION_ID	smallint,
	@COMMODITY_ID	int,
	@COMMODITY_NUMBER	numeric(10,0),
	@COMMODITY	nvarchar(1000),
	@CONVEYANCE	nvarchar(1000),	
	@SUM_INSURED	numeric(18,2),
	@TransportationDepartingDate DATETIME, 
	@ARRIVAL_DATE	datetime,
	@TransportationOriginCountry SMALLINT,  
	@TransportationOriginState SMALLINT, 
	@TransportationOriginCity NVARCHAR (200), 
	@TransportationDestinationCountry SMALLINT,   
	@TransportationDestinationState SMALLINT, 
	@TransportationDestinationCity NVARCHAR (200),
	@REMARKS	nvarchar(1000),
	--@IS_ACTIVE	char,
	--@CREATED_BY	int,
	--@CREATED_DATETIME	datetime,
	--@MODIFIED_BY	int,
	--@LAST_UPDATED_DATETIME	datetime,
	@TransportationConveyancetype INT
	--@CO_APPLICANT_ID	int
	
	
	--Set @CUSTOMER_ID = 
	--Set @POLICY_ID = 
	--Set @POLICY_VERSION_ID = 
	Set @COMMODITY_ID = 1
	Set @COMMODITY_NUMBER = 0
	Set @COMMODITY = ''
	Set @CONVEYANCE = ''
	Set @SUM_INSURED =0 
	Set @TransportationDepartingDate ='2010-10-27 12:01:25.053' 
	Set @ARRIVAL_DATE ='2010-10-27 12:01:25.053' 
	Set @TransportationOriginCountry =5 
	Set @TransportationOriginState = 71
	Set @TransportationOriginCity = 'fgh'
	Set @TransportationDestinationCountry = 5
	Set @TransportationDestinationState = 71
	Set @TransportationDestinationCity ='fhg' 
	Set @REMARKS = ''
	Set @IS_ACTIVE = 'Y'
	Set @CREATED_BY = 198
	Set @CREATED_DATETIME = '2010-10-27 12:01:25.053'
	Set @MODIFIED_BY = null
	Set @LAST_UPDATED_DATETIME =GETDATE() 
	Set @TransportationConveyancetype = 1
	Set @CO_APPLICANT_ID = 0

	
   
    SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_COMMODITY_INFO  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
          
           
   SET @ROW_ID=1        
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
     SELECT         
      @PolicyNo = PolicyNo,  
      @EndorsementNo = EndorsementNo,  
      @COMMODITY_ID =   CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,      
      @COMMODITY_NUMBER=CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,
      @COMMODITY = TransportationInsuredObject,
      @TransportationConveyancetype = CASE WHEN ISNULL(TransportationConveyancetype,0)<>'' THEN CONVERT(INT,TransportationConveyancetype) END,        
      @TransportationDepartingDate = CASE  WHEN ISNULL(TransportationDepartingDate,'')<>'' THEN        
                      CONVERT(DATETIME,TransportationDepartingDate,103) END,--TransportationDepartingDate,        
      @TransportationOriginCountry =  CASE WHEN ISNULL(TransportationOriginCountry,0)<>'' THEN CONVERT(INT,TransportationOriginCountry) END, --TransportationOriginCountry,         
      @TransportationOriginState = CASE WHEN ISNULL(TransportationOriginState,0)<>'' THEN CONVERT(INT,TransportationOriginState) END, --,         
      @TransportationOriginCity =TransportationOriginCity,        
      @TransportationDestinationCountry =CASE WHEN ISNULL(TransportationDestinationCountry,0)<>'' THEN CONVERT(INT,TransportationDestinationCountry) END, --,         
      @TransportationDestinationState =  CASE WHEN ISNULL(TransportationDestinationState,0)<>'' THEN CONVERT(INT,TransportationDestinationState) END, --,         
      @TransportationDestinationCity =TransportationDestinationCity,        
      @TransportationOriginCountry = CASE WHEN ISNULL(TransportationOriginCountry,0)<>'' THEN CONVERT(INT,TransportationOriginCountry) END, --,         
      @TransportationOriginState =CASE WHEN ISNULL(TransportationOriginState,0)<>'' THEN CONVERT(INT,TransportationOriginState) END, --,         
      @TransportationOriginCity = TransportationOriginCity,        
      @TransportationDestinationCountry = CASE WHEN ISNULL(TransportationDestinationCountry,0)<>'' THEN CONVERT(INT,TransportationDestinationCountry) END,--,         
      @TransportationDestinationState = CASE WHEN ISNULL(TransportationDestinationState,0)<>'' THEN CONVERT(INT,TransportationDestinationState) END, --,         
      @TransportationDestinationCity =TransportationDestinationCity        
            
    FROM #TEMP_POL_COMMODITY_INFO WHERE ID=@ROW_ID        
            
    --GET CUSTOMER_ID AND POLIYC_ID        
    SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
     --SELECT @COMMODITY_ID = ISNULL(MAX(COMMODITY_ID),0)+1 FROM POL_COMMODITY_INFO WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
    --CONSIDER MULTIPLE        
    --IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID)        
     IF(@POLICY_LOB ='20' AND @POLICY_LOB = '23')-- CHECKING PRODUCT ID WHICH DONT HAVE LOCATION        
     BEGIN        
	INSERT INTO [POL_COMMODITY_INFO]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[COMMODITY_ID]
           ,[COMMODITY_NUMBER]
           ,[COMMODITY]
           ,[CONVEYANCE]
           ,[SUM_INSURED]
           ,[DEPARTING_DATE]
           ,[ARRIVAL_DATE]
           ,[ORIGIN_COUNTRY]
           ,[ORIGIN_STATE]
           ,[ORIGIN_CITY]
           ,[DESTINATION_COUNTRY]
           ,[DESTINATION_STATE]
           ,[DESTINATION_CITY]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME]
           ,[CONVEYANCE_TYPE]
           ,[CO_APPLICANT_ID])
     VALUES               
     (
      @CUSTOMER_ID,
		@POLICY_ID,
	@POLICY_VERSION_ID,
	@COMMODITY_ID,
	@COMMODITY_NUMBER,
	@COMMODITY,
	@CONVEYANCE,	
	@SUM_INSURED,
	@TransportationDepartingDate, 
	@ARRIVAL_DATE,
	@TransportationOriginCountry ,  
	@TransportationOriginState , 
	@TransportationOriginCity , 
	@TransportationDestinationCountry ,   
	@TransportationDestinationState , 
	@TransportationDestinationCity,
	@REMARKS,
	@IS_ACTIVE	,
	@CREATED_BY	,
	@CREATED_DATETIME,
	@MODIFIED_BY	,
	@LAST_UPDATED_DATETIME,
	@TransportationConveyancetype,
	@CO_APPLICANT_ID	               
         
     )   
   IF @@ERROR<>0    
   ROLLBACK TRAN          
    END -- CHECK OF CUSTOMER WITH POLIYC ID IN POL_LOCATIONS        
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--END OF LOOP FOR POL_COMMODITY_INFO INSERT FOR INDIVIDUAL CUSTOMER        
         
      
          
   ------TEMP TABLE FOR POL_CIVIL_TRANSPORT_VEHICLES -----------          
  CREATE TABLE #TEMP_POL_CIVIL_TRANSPORT_VEHICLES        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),    
   EndorsementNo VARCHAR(MAX),        
   ITEM VARCHAR(MAX),
   VehicleChassis VARCHAR(MAX),     
   InsuredEffectiveDate VARCHAR(MAX),        
   InsuredExpireDate VARCHAR(MAX),        
   VehicleModel VARCHAR(MAX),        
   VehicleYear VARCHAR(MAX),        
   VehiclePlate VARCHAR(MAX),        
   VehicleId VARCHAR(MAX),        
   VehiclePassengerCapacity VARCHAR(MAX),        
   VehicleCategory VARCHAR(MAX),    
   FIPECODE  VARCHAR(MAX)       
        
  )        
  INSERT INTO #TEMP_POL_CIVIL_TRANSPORT_VEHICLES        
  (        
   POLICYNO,    
   EndorsementNo,          
   ITEM,
   VehicleChassis,  
   InsuredEffectiveDate,        
   InsuredExpireDate ,        
   VehicleModel ,        
   VehicleYear ,        
   VehiclePlate,        
   VehicleId ,        
   VehiclePassengerCapacity,        
   VehicleCategory,    
   FIPECODE        
  )        
  SELECT PolicyNo,    
   EndorsementNo,  
   ITEM,
   VehicleChassis,          
   InsuredEffectiveDate,        
   InsuredExpireDate ,        
   VehicleModel ,        
   VehicleYear ,        
   VehiclePlate,        
   VehicleId ,        
   VehiclePassengerCapacity,        
   VehicleCategory,    
   [FIPE Code]       
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
          
          
  --DECLARE @ROW_ID INT        
   --DECLARE @MAXCOUNT INT        
   DECLARE 
    --@CUSTOMER_ID	int,	
	--@POLICY_ID	int,	
	--@POLICY_VERSION_ID	smallint,	
	@VehicleId INT,  --@VEHICLE_ID	int,	
	@CLIENT_ORDER NUMERIC(10,0),   --@CLIENT_ORDER	numeric	(10,0),
	@VEHICLE_NUMBER	numeric	(10,0),
	@VehicleYear NUMERIC(18,0),  --@MANUFACTURED_YEAR	numeric	(18,0),
	@FIPECODE NVARCHAR(14),  --@FIPE_CODE	nvarchar	(14),
	--@CATEGORY	int,	
	@CAPACITY NVARCHAR(6), --@CAPACITY	nvarchar	(6),
	 @MAKE_MODEL NVARCHAR(100),  --@MAKE_MODEL	nvarchar	(100),
	 @VehiclePlate NVARCHAR(14), --@LICENSE_PLATE	nvarchar	(14),
	@CHASSIS	nvarchar	(50),
	@MANDATORY_DEDUCTIBLE	numeric	(18,0),
	@FACULTATIVE_DEDUCTIBLE	numeric	(18,0),
	@SUB_BRANCH	int,	
	 @InsuredEffectiveDate DATETIME, --@RISK_EFFECTIVE_DATE	datetime,	
	 @InsuredExpireDate DATETIME, --@RISK_EXPIRE_DATE	datetime,	
	@REGION	int,	
	@COV_GROUP_CODE	nvarchar	(30),
	@FINANCE_ADJUSTMENT	nvarchar	(100),
	@REFERENCE_PROPOSASL	nvarchar	(100),
	--@REMARKS	nvarchar	(1000),
	--@IS_ACTIVE	nchar	(2),
	--@CREATED_BY	int,	
	--@CREATED_DATETIME	datetime,	
	--@MODIFIED_BY	int,	
	--@LAST_UPDATED_DATETIME	datetime,	
	@VEHICLE_PLAN_ID	int,	
	@VEHICLE_MAKE	nvarchar	(100)
	--@CO_APPLICANT_ID	int	

   
 --   SET @CUSTOMER_ID = 
	--SET @POLICY_ID = 
	--SET @POLICY_VERSION_ID = 
	--SET @VEHICLE_ID = 
	SET @CLIENT_ORDER = 0
	SET @VEHICLE_NUMBER = 0
	SET @VehicleYear = 0
	SET @FIPECODE = ''
	SET @CATEGORY = ''
	SET @CAPACITY = ''
	SET @MAKE_MODEL = ''
	SET @VehiclePlate = ''
	SET @CHASSIS ='' 
	SET @MANDATORY_DEDUCTIBLE = 0
	SET @FACULTATIVE_DEDUCTIBLE = 0 
	SET @SUB_BRANCH = 0
	SET @InsuredEffectiveDate = '2010-10-27 10:08:07.233'
	SET @InsuredExpireDate = '2010-10-27 10:08:07.233'
	SET @REGION = 0
	SET @COV_GROUP_CODE = ''
	SET @FINANCE_ADJUSTMENT = ''
	SET @REFERENCE_PROPOSASL = ''
	SET @REMARKS ='' 
	SET @IS_ACTIVE = 'Y'
	SET @CREATED_BY=198                
	SET @CREATED_DATETIME='2010-10-27 10:08:07.233'                
	SET @MODIFIED_BY=NULL                
	SET @LAST_UPDATED_DATETIME=GETDATE() 
	SET @VEHICLE_PLAN_ID =0 
	SET @VEHICLE_MAKE = ''
	SET @CO_APPLICANT_ID = 0

    
                     
           
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_CIVIL_TRANSPORT_VEHICLES  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
     SELECT     
		@PolicyNo = POLICYNO,    
		@EndorsementNo = EndorsementNo,  
		@RISK_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,--vehicleId
		@CLIENT_ORDER = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,
		@CHASSIS = VehicleChassis,    
		@InsuredEffectiveDate = CASE  WHEN ISNULL(InsuredEffectiveDate,'')<>'' THEN          
		CONVERT(DATETIME,InsuredEffectiveDate,103)  END,        
		@InsuredExpireDate = CASE  WHEN ISNULL(InsuredExpireDate,'')<>'' THEN          
		CONVERT(DATETIME,InsuredExpireDate,103)  END,          
	    @MAKE_MODEL =VehicleModel,        
	    @VehicleYear =VehicleYear,        
	    @VehiclePlate =VehiclePlate,            
	     @CAPACITY = VehiclePassengerCapacity,        
	    @CATEGORY =VehicleCategory,    
	    @FIPECODE = FIPECODE    
	 FROM #TEMP_POL_CIVIL_TRANSPORT_VEHICLES WHERE ID=@ROW_ID        
          
   --GET CUSTOMER_ID AND POLIYC_ID        
   SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
			@POLICY_LOB =POLICY_LOB
   FROM POL_CUSTOMER_POLICY_LIST 
   WHERE POLICY_NUMBER = @PolicyNo        
         
   SET @IS_ACTIVE ='Y'    
   SET @CLIENT_ORDER =1        
   --SELECT @VehicleId = ISNULL(MAX(COMMODITY_ID),0)+1 FROM POL_COMMODITY_INFO WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
          
    --print @PolicyNo        
    --print @customer_id    
    --print @POLICY_LOB    
    --print @InsuredEffectiveDate    
   --IF NOT EXISTS(SELECT CUSTOMER_ID FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID)        
   IF(@POLICY_LOB ='17' OR @POLICY_LOB ='18')        
   BEGIN        
            
    INSERT INTO [POL_CIVIL_TRANSPORT_VEHICLES]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[VEHICLE_ID]
           ,[CLIENT_ORDER]
           ,[VEHICLE_NUMBER]
           ,[MANUFACTURED_YEAR]
           ,[FIPE_CODE]
           ,[CATEGORY]
           ,[CAPACITY]
           ,[MAKE_MODEL]
           ,[LICENSE_PLATE]
           ,[CHASSIS]
           ,[MANDATORY_DEDUCTIBLE]
           ,[FACULTATIVE_DEDUCTIBLE]
           ,[SUB_BRANCH]
           ,[RISK_EFFECTIVE_DATE]
           ,[RISK_EXPIRE_DATE]
           ,[REGION]
           ,[COV_GROUP_CODE]
           ,[FINANCE_ADJUSTMENT]
           ,[REFERENCE_PROPOSASL]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME]
           ,[VEHICLE_PLAN_ID]
           ,[VEHICLE_MAKE]
           ,[CO_APPLICANT_ID])
     VALUES 
		( @CUSTOMER_ID
		,@POLICY_ID
		,@POLICY_VERSION_ID
		,@RISK_ID
		,@CLIENT_ORDER
		,@VEHICLE_NUMBER
		,@VehicleYear
		,@FIPECODE
		,@CATEGORY
		,@CAPACITY
		,@MAKE_MODEL
		,@VehiclePlate
		,@CHASSIS
		,@MANDATORY_DEDUCTIBLE
		,@FACULTATIVE_DEDUCTIBLE
		,@SUB_BRANCH
		,@InsuredEffectiveDate
		,@InsuredExpireDate
		,@REGION
		,@COV_GROUP_CODE
		,@FINANCE_ADJUSTMENT
		,@REFERENCE_PROPOSASL
		,@REMARKS
		,@IS_ACTIVE
		,@CREATED_BY
		,@CREATED_DATETIME
		,@MODIFIED_BY
		,@LAST_UPDATED_DATETIME
		,@VEHICLE_PLAN_ID
		,@VEHICLE_MAKE
		,@CO_APPLICANT_ID
		)
	        
    
  IF @@ERROR<>0    
  ROLLBACK TRAN          
    END -- CHECK OF CUSTOMER WITH POLIYC ID IN POL_LOCATIONS        
            
         
       SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1       
           
   END--END OF LOOP FOR POL_CIVIL_TRANSPORT_VEHICLES */ 

   
----------------------INSERT INTO POL_PERILS----------------------------    

 CREATE TABLE #TEMP_POL_PERILS        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),  
   LocationAddress VARCHAR(MAX),        
   LocationNumber VARCHAR(MAX),        
   LocationComplement VARCHAR(MAX),        
   LocationDistrict VARCHAR(MAX),        
   LocationCity VARCHAR(MAX),        
   LocationState VARCHAR(MAX),        
   LocationPostalCode VARCHAR(MAX),        
   LocationCountry VARCHAR(MAX),        
   LocationContructionType VARCHAR(MAX),        
   LocationActivity VARCHAR(MAX),        
   LocationOccupiedAs VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_POL_PERILS        
  (        
   POLICYNO,ENDORSEMENTNO,ITEM, LocationAddress,LocationNumber,LocationComplement,        
   LocationDistrict,LocationCity,LocationState,        
   LocationPostalCode,LocationCountry,LocationContructionType,        
   LocationActivity,LocationOccupiedAs        
  )         
        
  SELECT PolicyNo,EndorsementNo,Item,LocationAddress,LocationNumber,LocationComplement,LocationDistrict,LocationCity,        
   LocationState,LocationPostalCode,LocationCountry,LocationContructionType,LocationActivity,LocationOccupiedAs        
  FROM ImportNew_InsuredObject WITH (NOLOCK)   


      
  DECLARE           
    --@CUSTOMER_ID int,           
    --@POLICY_ID int,           
    --@POLICY_VERSION_ID smallint,           
    @PERIL_ID smallint,           
    @CALCULATION_NUMBER int,           
    @LOCATION int,           
    @ADDRESS nvarchar (160),          
    --@NUMBER nvarchar 12          
    @COMPLEMENT nvarchar (40),          
    --@CITY nvarchar (60),          
    --@COUNTRY nvarchar (10),          
   -- @STATE nvarchar (10),    
    @ZIP nvarchar (22),          
    @TELEPHONE nvarchar (24),          
    @EXTENTION nvarchar (12),          
    --@FAX nvarchar (24),          
    --@CATEGORY nvarchar (12),          
    @ATIV_CONTROL int,           
    @LOC nvarchar (8),          
    @LOCALIZATION nvarchar (2),          
    @OCCUPANCY nvarchar (16),          
    --@CONSTRUCTION nvarchar (16),          
    --@LOC_CITY nvarchar (80),          
    @CONSTRUCTION_TYPE nvarchar (22),          
    --@ACTIVITY_TYPE nvarchar (100),          
    @RISK_TYPE nvarchar (100),          
    @VR numeric (12,0),          
    @LMI numeric (12,0),          
    @BUILDING int ,          
    @MMU numeric (12,0),          
    @MMP numeric (12,0),          
    @MRI numeric (12,0),          
    @TYPE int,           
    @LOSS numeric (12,0),          
    @LOYALTY numeric (12,0),          
    @PERC_LOYALTY numeric (12,0),          
    @DEDUCTIBLE_OPTION int,           
    @MULTIPLE_DEDUCTIBLE nvarchar (16),          
    @E_FIRE int,           
    @S_FIXED_FOAM int,           
    @S_FIXED_INSERT_GAS int ,          
    @CAR_COMBAT int,           
    @S_DETECT_ALARM int,           
    @S_FIRE_UNIT int,           
    @S_FOAM_PER_MANUAL int,           
    @S_MANUAL_INERT_GAS int,           
    @S_SEMI_HOSES int,           
    @HYDRANTS int,           
    @SHOWERS int,           
    @SHOWER_CLASSIFICATION nvarchar (200),          
    @FIRE_CORPS nvarchar (200),          
    @PUNCTUATION_QUEST int,           
    @DMP numeric (12,0),          
    @EXPLOSION_DEGREE nvarchar (2),          
    @PR_LIQUID int,           
    @COD_ATIV_DRAFTS nvarchar (174),          
    @OCCUPATION_TEXT nvarchar (100),          
    @ASSIST24 int,           
    @LMRA numeric (12,0),          
    @AGGRAVATION_RCG_AIR numeric (12,0),          
    @EXPLOSION_DESC numeric (12,0),          
    @PROTECTIVE_DESC numeric (12,0),          
    @LMI_DESC numeric (12,0),          
    @LOSS_DESC numeric (12,0),          
    @QUESTIONNAIRE_DESC numeric (12,0),          
    @DEDUCTIBLE_DESC numeric (12,0),          
    @GROUPING_DESC numeric (12,0),          
    @LOC_FLOATING nvarchar (8),          
    @ADJUSTABLE numeric (12,0),          
    --@IS_ACTIVE nchar (2),          
    --@CREATED_BY int,           
    --@CREATED_DATETIME datetime           
    --@MODIFIED_BY int           
    --@LAST_UPDATED_DATETIME datetime           
    @CORRAL_SYSTEM nvarchar (40),          
    @RAWVALUES nvarchar (40),          
    --@REMARKS nvarchar (1000),          
    @PARKING_SPACES nvarchar (40),          
    @CLAIM_RATIO decimal (12,2),          
    @RAW_MATERIAL_VALUE nvarchar (40),          
    @CONTENT_VALUE nvarchar (40),          
    @BONUS decimal (12,2)          
              
              
    --SET  @CUSTOMER_ID = 2525          
    --SET  @POLICY_ID = 1          
    --SET  @POLICY_VERSION_ID =1          
    SET  @PERIL_ID = 1          
    SET  @CALCULATION_NUMBER = NULL          
    SET  @LOCATION =4          
    SET  @ADDRESS =NULL          
    SET  @NUMBER =NULL          
    SET  @COMPLEMENT =NULL          
    SET  @CITY =NULL          
    SET  @COUNTRY =NULL          
    SET  @STATE =NULL          
    SET  @ZIP =NULL          
    SET  @TELEPHONE =NULL          
    SET  @EXTENTION =NULL          
    SET  @FAX =NULL          
    SET  @CATEGORY =''          
    SET  @ATIV_CONTROL =NULL          
    SET  @LOC =NULL          
    SET  @LOCALIZATION =NULL          
    SET  @OCCUPANCY =''          
    SET  @CONSTRUCTION =''          
    SET  @LOC_CITY =NULL          
    SET  @CONSTRUCTION_TYPE =NULL          
    SET  @ACTIVITY_TYPE =''          
    SET  @RISK_TYPE =NULL          
    SET  @VR =NULL          
    SET  @LMI =NULL          
    SET  @BUILDING =500000          
    SET  @MMU =NULL          
    SET  @MMP =NULL          
    SET  @MRI =NULL          
    SET  @TYPE =NULL          
    SET  @LOSS =NULL          
    SET  @LOYALTY =NULL          
    SET  @PERC_LOYALTY =NULL          
    SET  @DEDUCTIBLE_OPTION =NULL          
    SET  @MULTIPLE_DEDUCTIBLE =''          
    SET  @E_FIRE =NULL          
    SET  @S_FIXED_FOAM =NULL          
    SET  @S_FIXED_INSERT_GAS =NULL          
    SET  @CAR_COMBAT =NULL          
    SET  @S_DETECT_ALARM =NULL          
    SET  @S_FIRE_UNIT =NULL          
    SET  @S_FOAM_PER_MANUAL =NULL          
    SET  @S_MANUAL_INERT_GAS =NULL          
    SET  @S_SEMI_HOSES =NULL          
    SET  @HYDRANTS =NULL          
    SET  @SHOWERS =NULL          
    SET  @SHOWER_CLASSIFICATION =NULL          
    SET  @FIRE_CORPS =NULL          
    SET  @PUNCTUATION_QUEST =NULL          
    SET  @DMP =NULL          
    SET  @EXPLOSION_DEGREE =NULL          
    SET  @PR_LIQUID =NULL          
    SET  @COD_ATIV_DRAFTS =NULL          
    SET  @OCCUPATION_TEXT =NULL          
    SET  @ASSIST24 = 10964          
    SET  @LMRA =NULL          
    SET  @AGGRAVATION_RCG_AIR =NULL          
    SET  @EXPLOSION_DESC =NULL          
    SET  @PROTECTIVE_DESC =NULL          
    SET  @LMI_DESC =NULL          
    SET  @LOSS_DESC =NULL          
    SET  @QUESTIONNAIRE_DESC =NULL          
    SET  @DEDUCTIBLE_DESC =NULL          
    SET  @GROUPING_DESC =NULL          
    SET  @LOC_FLOATING =NULL          
    SET  @ADJUSTABLE =NULL          
    SET  @IS_ACTIVE ='Y'          
    SET  @CREATED_BY =198          
    SET  @CREATED_DATETIME ='2010-10-27 00:00:00.000'          
    SET  @MODIFIED_BY =198          
    SET  @LAST_UPDATED_DATETIME ='2010-10-27 00:00:00.000'          
    SET  @CORRAL_SYSTEM =NULL          
    SET  @RAWVALUES ='Y'          
    SET  @REMARKS =''          
    SET  @PARKING_SPACES =23          
    SET  @CLAIM_RATIO =NULL          
    SET  @RAW_MATERIAL_VALUE =23            
    SET  @CONTENT_VALUE =100000          
    SET  @BONUS =1.00          
     
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_PERILS  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @LOC = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END ,   
    @ADDRESS = LocationAddress, --SUBSTRING(LocationAddress,1,40),            
    @CALCULATION_NUMBER= CASE WHEN ISNULL(LocationNumber,0)<>'' THEN CONVERT(INT,LocationNumber) END, --SUBSTRING(LocationNumber,1,40),        
    @COMPLEMENT= LocationComplement, --SUBSTRING(LocationComplement,1,150),        
    --@DISTRICT= LocationDistrict, --SUBSTRING(LocationDistrict,1,100),        
    @CITY = LocationCity, --SUBSTRING(LocationCity,1,150),        
    @STATE =LocationState, --SUBSTRING(LocationState,1,10),        
    @ZIP=LocationPostalCode, --SUBSTRING(LocationPostalCode,1,22),        
    @COUNTRY= LocationCountry, --SUBSTRING(LocationCountry,1,10),        
    @CONSTRUCTION_TYPE=CASE WHEN ISNULL(LocationContructionType,'')<>'' THEN CONVERT(INT,LocationContructionType) END, --CONVERT(INT,LocationContructionType),        
    @ACTIVITY_TYPE= CASE WHEN ISNULL(LocationActivity,'')<>'' THEN CONVERT(INT,LocationActivity) END, --CONVERT(INT,LocationActivity),        
    @OCCUPANCY=CASE WHEN ISNULL(LocationOccupiedAs,'')<>'' THEN CONVERT(INT,LocationOccupiedAs) END  --CONVERT(INT,LocationOccupiedAs)         
    FROM #TEMP_POL_PERILS WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
            
            
     IF(@POLICY_LOB ='9' AND @CUSTOMER_ID >0)   --CHECK LOB FOR POL_PERILS     
		BEGIN --2       
			    
			  INSERT INTO [POL_PERILS]          
			 ([CUSTOMER_ID]          
			 ,[POLICY_ID]          
			 ,[POLICY_VERSION_ID]          
			 ,[PERIL_ID]          
			 ,[CALCULATION_NUMBER]          
			 ,[LOCATION]          
			 ,[ADDRESS]          
			 ,[NUMBER]          
			 ,[COMPLEMENT]          
			 ,[CITY]          
			 ,[COUNTRY]          
			 ,[STATE]          
			 ,[ZIP]          
			 ,[TELEPHONE]          
			 ,[EXTENTION]          
			 ,[FAX]          
			 ,[CATEGORY]          
			 ,[ATIV_CONTROL]          
			 ,[LOC]          
			 ,[LOCALIZATION]          
			 ,[OCCUPANCY]          
			 ,[CONSTRUCTION]          
			 ,[LOC_CITY]          
			 ,[CONSTRUCTION_TYPE]          
		   ,[ACTIVITY_TYPE]          
			 ,[RISK_TYPE]          
			 ,[VR]          
			 ,[LMI]          
			 ,[BUILDING]          
			 ,[MMU]          
			 ,[MMP]          
			 ,[MRI]          
			 ,[TYPE]          
			 ,[LOSS]          
			 ,[LOYALTY]          
			 ,[PERC_LOYALTY]          
			 ,[DEDUCTIBLE_OPTION]          
			 ,[MULTIPLE_DEDUCTIBLE]          
			 ,[E_FIRE]          
			 ,[S_FIXED_FOAM]          
			 ,[S_FIXED_INSERT_GAS]          
			 ,[CAR_COMBAT]          
			 ,[S_DETECT_ALARM]          
			 ,[S_FIRE_UNIT]          
			 ,[S_FOAM_PER_MANUAL]          
			 ,[S_MANUAL_INERT_GAS]          
			 ,[S_SEMI_HOSES]          
			 ,[HYDRANTS]          
			 ,[SHOWERS]          
			 ,[SHOWER_CLASSIFICATION]          
			 ,[FIRE_CORPS]          
			 ,[PUNCTUATION_QUEST]          
			 ,[DMP]          
			 ,[EXPLOSION_DEGREE]          
			 ,[PR_LIQUID]          
			 ,[COD_ATIV_DRAFTS]          
			 ,[OCCUPATION_TEXT]          
			 ,[ASSIST24]          
			 ,[LMRA]          
			 ,[AGGRAVATION_RCG_AIR]          
			 ,[EXPLOSION_DESC]          
			 ,[PROTECTIVE_DESC]          
			 ,[LMI_DESC]          
			 ,[LOSS_DESC]          
			 ,[QUESTIONNAIRE_DESC]          
			 ,[DEDUCTIBLE_DESC]          
			 ,[GROUPING_DESC]          
			 ,[LOC_FLOATING]          
			 ,[ADJUSTABLE]          
			 ,[IS_ACTIVE]          
			 ,[CREATED_BY]          
			 ,[CREATED_DATETIME]          
			 ,[MODIFIED_BY]          
			 ,[LAST_UPDATED_DATETIME]          
			 ,[CORRAL_SYSTEM]          
			 ,[RAWVALUES]          
			 ,[REMARKS]          
			 ,[PARKING_SPACES]          
			 ,[CLAIM_RATIO]          
			 ,[RAW_MATERIAL_VALUE]          
			 ,[CONTENT_VALUE]          
			 ,[BONUS])          
			 VALUES          
			 ( @CUSTOMER_ID          
		   ,@POLICY_ID          
		   ,@POLICY_VERSION_ID          
		   ,@PERIL_ID          
		   ,@CALCULATION_NUMBER          
		   ,@LOCATION          
		   ,@ADDRESS          
		   ,@NUMBER          
		   ,@COMPLEMENT          
		   ,@CITY          
		   ,@COUNTRY          
		   ,@STATE          
		   ,@ZIP          
		   ,@TELEPHONE          
		   ,@EXTENTION          
		   ,@FAX          
		   ,@CATEGORY          
		   ,@ATIV_CONTROL          
		   ,@LOC          
		   ,@LOCALIZATION          
		   ,@OCCUPANCY          
		   ,@CONSTRUCTION          
		   ,@LOC_CITY          
		   ,@CONSTRUCTION_TYPE          
		   ,@ACTIVITY_TYPE          
		   ,@RISK_TYPE          
		   ,@VR          
		   ,@LMI          
		   ,@BUILDING          
		   ,@MMU          
		   ,@MMP          
		   ,@MRI          
		   ,@TYPE          
		   ,@LOSS          
		   ,@LOYALTY          
		   ,@PERC_LOYALTY          
		   ,@DEDUCTIBLE_OPTION          
		   ,@MULTIPLE_DEDUCTIBLE          
		   ,@E_FIRE          
		   ,@S_FIXED_FOAM          
		   ,@S_FIXED_INSERT_GAS          
		   ,@CAR_COMBAT          
		   ,@S_DETECT_ALARM          
		   ,@S_FIRE_UNIT          
		   ,@S_FOAM_PER_MANUAL          
		   ,@S_MANUAL_INERT_GAS          
		   ,@S_SEMI_HOSES          
		   ,@HYDRANTS          
		   ,@SHOWERS          
		   ,@SHOWER_CLASSIFICATION          
		   ,@FIRE_CORPS          
		   ,@PUNCTUATION_QUEST          
		   ,@DMP          
		   ,@EXPLOSION_DEGREE          
		   ,@PR_LIQUID          
		   ,@COD_ATIV_DRAFTS          
		   ,@OCCUPATION_TEXT          
		   ,@ASSIST24          
		   ,@LMRA          
		   ,@AGGRAVATION_RCG_AIR          
		   ,@EXPLOSION_DESC          
		   ,@PROTECTIVE_DESC          
		   ,@LMI_DESC          
		   ,@LOSS_DESC          
		   ,@QUESTIONNAIRE_DESC          
		   ,@DEDUCTIBLE_DESC          
		   ,@GROUPING_DESC          
		   ,@LOC_FLOATING          
		   ,@ADJUSTABLE          
		   ,@IS_ACTIVE          
		   ,@CREATED_BY          
		   ,@CREATED_DATETIME          
		   ,@MODIFIED_BY          
		   ,@LAST_UPDATED_DATETIME          
		   ,@CORRAL_SYSTEM          
		   ,@RAWVALUES          
		   ,@REMARKS          
		   ,@PARKING_SPACES          
		   ,@CLAIM_RATIO          
		   ,@RAW_MATERIAL_VALUE          
		   ,@CONTENT_VALUE          
		   ,@BONUS          
		   )   
		        
			  IF @@ERROR<>0    
			  ROLLBACK TRAN 
			
		END	--2       
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--1 END OF LOOP  
   
   ----------------------INSERT INTO POL_PRODUCT_LOCATION_INFO----------------------------    

 CREATE TABLE #TEMP_POL_PRODUCT_LOCATION_INFO        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),  
   --LocationNumber VARCHAR(MAX),          
   LocationContructionType VARCHAR(MAX),        
   LocationActivity VARCHAR(MAX),        
   LocationOccupiedAs VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_POL_PRODUCT_LOCATION_INFO        
  (        
   POLICYNO,ENDORSEMENTNO,ITEM,LocationContructionType,        
   LocationActivity,LocationOccupiedAs        
  )         
        
  SELECT PolicyNo,EndorsementNo,Item,LocationContructionType,LocationActivity,LocationOccupiedAs        
  FROM ImportNew_InsuredObject WITH (NOLOCK)   


      
  DECLARE           
	--@CUSTOMER_ID	int,	
	--@POLICY_ID	int,	
	--@POLICY_VERSION_ID	smallint,	
	@PRODUCT_RISK_ID	int,	
--	@LOCATION	int,	
	@VALUE_AT_RISK	decimal	(12,2),
	@BUILDING_VALUE	decimal	(12,2),
	@CONTENTS_VALUE	decimal	(12,2),
--	@RAW_MATERIAL_VALUE	decimal	(12,0),
	@CONTENTS_RAW_VALUES	decimal	(12,2),
	@MRI_VALUE	decimal	(12,2),
	@MAXIMUM_LIMIT	decimal	(12,2),
	@POSSIBLE_MAX_LOSS	decimal	(12,2),
	--@MULTIPLE_DEDUCTIBLE	int,	
	--@PARKING_SPACES	int,	
	--@ACTIVITY_TYPE	int,	
	@OCCUPIED_AS	int,	
	--@CONSTRUCTION	nvarchar	(100),
	@RUBRICA	nvarchar	(12)
	--@ASSIST24	int,	
	--@REMARKS	nvarchar	(1000),
	--@IS_ACTIVE	nchar	(2),
	--@CREATED_BY	int,	
	--@CREATED_DATETIME	datetime,	
	--@MODIFIED_BY	int,	
	--@LAST_UPDATED_DATETIME	datetime,	
	--@CLAIM_RATIO	decimal	(12,2),
	--@BONUS	decimal	(12,2),
	--@CO_APPLICANT_ID	int	

    
    --SET @CUSTOMER_ID = 
	--SET @POLICY_ID = 
	--SET @POLICY_VERSION_ID = 
	--SET @PRODUCT_RISK_ID = 
	SET @LOCATION = 0 
	SET @VALUE_AT_RISK = 0
	SET @BUILDING_VALUE = 0
	SET @CONTENTS_VALUE = 0
	SET @RAW_MATERIAL_VALUE = ''
	SET @CONTENTS_RAW_VALUES = 0
	SET @MRI_VALUE = 0.00
	SET @MAXIMUM_LIMIT = 0
	SET @POSSIBLE_MAX_LOSS = 0.00
	SET @MULTIPLE_DEDUCTIBLE = ''
	SET @PARKING_SPACES = ''
	SET @ACTIVITY_TYPE = 0
	SET @OCCUPIED_AS = 0
	SET @CONSTRUCTION = 0
	SET @RUBRICA ='' 
	SET @ASSIST24 = 0
	SET @REMARKS = null
	SET @IS_ACTIVE = 'Y'
	SET @CREATED_BY = 198 
	SET @CREATED_DATETIME ='2010-10-27 00:00:00.000' 
	SET @MODIFIED_BY = NULL
	SET @LAST_UPDATED_DATETIME = GETDATE()
	SET @CLAIM_RATIO = 0.00
	SET @BONUS = 0.00
	SET @CO_APPLICANT_ID = 0
    
     
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_PRODUCT_LOCATION_INFO  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @LOCATION = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END, 
    @PRODUCT_RISK_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END,      
    @CONSTRUCTION_TYPE=CASE WHEN ISNULL(LocationContructionType,'')<>'' THEN CONVERT(INT,LocationContructionType) END, --CONVERT(INT,LocationContructionType),        
    @ACTIVITY_TYPE= CASE WHEN ISNULL(LocationActivity,0)<>'' THEN CONVERT(INT,LocationActivity) END, --CONVERT(INT,LocationActivity),        
    @OCCUPANCY=CASE WHEN ISNULL(LocationOccupiedAs,'')<>'' THEN CONVERT(INT,LocationOccupiedAs) END  --CONVERT(INT,LocationOccupiedAs)         
    FROM #TEMP_POL_PRODUCT_LOCATION_INFO WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID                 
           
 
     IF((@POLICY_LOB ='10' OR @POLICY_LOB ='11' OR @POLICY_LOB ='12'
		OR @POLICY_LOB ='14' OR @POLICY_LOB ='16' OR @POLICY_LOB ='19') AND @CUSTOMER_ID > 0)    --CEHCK LOB FOR POL_PRODUCT_LOCATION_INFO    
		BEGIN --2 
		INSERT INTO [POL_PRODUCT_LOCATION_INFO]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[PRODUCT_RISK_ID]
           ,[LOCATION]
           ,[VALUE_AT_RISK]
           ,[BUILDING_VALUE]
           ,[CONTENTS_VALUE]
           ,[RAW_MATERIAL_VALUE]
           ,[CONTENTS_RAW_VALUES]
           ,[MRI_VALUE]
           ,[MAXIMUM_LIMIT]
           ,[POSSIBLE_MAX_LOSS]
           ,[MULTIPLE_DEDUCTIBLE]
           ,[PARKING_SPACES]
           ,[ACTIVITY_TYPE]
           ,[OCCUPIED_AS]
           ,[CONSTRUCTION]
           ,[RUBRICA]
           ,[ASSIST24]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME]
           ,[CLAIM_RATIO]
           ,[BONUS]
           ,[CO_APPLICANT_ID])
     VALUES
           (@CUSTOMER_ID
			,@POLICY_ID
			,@POLICY_VERSION_ID
			,@PRODUCT_RISK_ID
			,@LOCATION
			,@VALUE_AT_RISK
			,@BUILDING_VALUE
			,@CONTENTS_VALUE
			,@RAW_MATERIAL_VALUE
			,@CONTENTS_RAW_VALUES
			,@MRI_VALUE
			,@MAXIMUM_LIMIT
			,@POSSIBLE_MAX_LOSS
			,@MULTIPLE_DEDUCTIBLE
			,@PARKING_SPACES
			,@ACTIVITY_TYPE
			,@OCCUPIED_AS
			,@CONSTRUCTION
			,@RUBRICA
			,@ASSIST24
			,@REMARKS
			,@IS_ACTIVE
			,@CREATED_BY
			,@CREATED_DATETIME
			,@MODIFIED_BY
			,@LAST_UPDATED_DATETIME
			,@CLAIM_RATIO
			,@BONUS
			,@CO_APPLICANT_ID
			)
		        
			  IF @@ERROR<>0    
			  ROLLBACK TRAN  
		END	--2       
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
   END--1 END OF LOOP 
     
----------TEMP TABLE FOR POL_MARITIME -----------                  
  DECLARE                   
    --@CUSTOMER_ID  int,
	--@POLICY_ID  int,
	--@POLICY_VERSION_ID  smallint,
	@MARITIME_ID  int,
	@VESSEL_NUMBER  numeric(10,0),
	@NAME_OF_VESSEL  nvarchar(140),
	@TYPE_OF_VESSEL  nvarchar(400),
	@MANUFACTURE_YEAR  int,
	@MANUFACTURER  nvarchar(100),
	@BUILDER  nvarchar(100),
	--@CONSTRUCTION  nvarchar(200),
	@PROPULSION  nvarchar(40),
	@CLASSIFICATION  nvarchar(100),
	@LOCAL_OPERATION  nvarchar(200),
	@LIMIT_NAVIGATION  nvarchar(200),
	@PORT_REGISTRATION  nvarchar(100),
	@REGISTRATION_NUMBER  nvarchar(100),
	@TIE_NUMBER  nvarchar(100),
	@VESSEL_ACTION_NAUTICO_CLUB  int,
	@NAME_OF_CLUB  nvarchar(140),
	@LOCAL_CLUB  nvarchar(200),
	@NUMBER_OF_CREW  int,
	@NUMBER_OF_PASSENGER  int
	--@REMARKS  nvarchar(1000),
	--@IS_ACTIVE  nvarchar(4),
	--@CREATED_BY  int,
	--@CREATED_DATETIME  datetime,
	--@LAST_UPDATED_DATETIME  datetime,
	--@MODIFIED_BY  int,
	--@CO_APPLICANT_ID  int

 --   SET @ CUSTOMER_ID  =  
	--SET @ POLICY_ID  =  
	--SET @ POLICY_VERSION_ID  =  
	--SET @ MARITIME_ID  =  
	--SET @CUSTOMER_ID  =  
	--SET @POLICY_ID  =  
	--SET @POLICY_VERSION_ID  =  
	--SET @MARITIME_ID  =  
	
	--SET @VESSEL_NUMBER  = 0	  
	--SET @NAME_OF_VESSEL  =''  
	--SET @TYPE_OF_VESSEL  = '' 
	--SET @MANUFACTURE_YEAR  = 0 
	SET @MANUFACTURER  = '' 
	SET @BUILDER  =  ''
	SET @CONSTRUCTION  = 0 
	SET @PROPULSION  = '' 
	SET @CLASSIFICATION  = '' 
	SET @LOCAL_OPERATION  = '' 
	SET @LIMIT_NAVIGATION  = '' 
	SET @PORT_REGISTRATION  = '' 
	SET @REGISTRATION_NUMBER  = '' 
	SET @TIE_NUMBER  =  ''
	SET @VESSEL_ACTION_NAUTICO_CLUB  = 0 
	SET @NAME_OF_CLUB  = '' 
	SET @LOCAL_CLUB  = '' 
	SET @NUMBER_OF_CREW  =  0
	SET @NUMBER_OF_PASSENGER  = 0 
	SET @REMARKS  =  ''
	SET @IS_ACTIVE  =  'Y'
	SET @CREATED_BY  = 198 
	SET @CREATED_DATETIME  = '2010-10-27 00:00:00.000'  
	SET @LAST_UPDATED_DATETIME  =  GETDATE()
	SET @MODIFIED_BY  = NULL 
	SET @CO_APPLICANT_ID  = 0 

   
           
      
  CREATE TABLE #TEMP_POL_MARITIME        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),            
   LocationContructionType VARCHAR(MAX),        
   [Vessel #] VARCHAR(MAX),
   [Name of Vessel] VARCHAR(MAX),
   [Type of Vessel] VARCHAR(MAX),
   [Manufacture Year] VARCHAR(MAX)

          
  )        
  INSERT INTO #TEMP_POL_MARITIME        
  (        
   POLICYNO,ENDORSEMENTNO,ITEM,LocationContructionType,        
   [Vessel #],[Name of Vessel] ,
   [Type of Vessel],[Manufacture Year]
  )         
        
  SELECT PolicyNo,EndorsementNo,Item,LocationContructionType,
   [Vessel #],[Name of Vessel],
   [Type of Vessel],[Manufacture Year]      
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
           
   --DECLARE @ROW_ID INT        
   --DECLARE @MAXCOUNT INT        
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_MARITIME  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @MARITIME_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END ,
    @CONSTRUCTION =CASE WHEN ISNULL(LocationContructionType,'')<>'' THEN CONVERT(INT,LocationContructionType) END, --CONVERT(INT,LocationContructionType),        
    @VESSEL_NUMBER = CASE WHEN ISNULL([Vessel #],0)<>'' THEN CONVERT(NUMERIC,[Vessel #]) END,
    @NAME_OF_VESSEL= [Name of Vessel],    
	@TYPE_OF_VESSEL = [Type of Vessel],
    @MANUFACTURE_YEAR = [Manufacture Year] 
    FROM #TEMP_POL_MARITIME WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
            
            
    
    IF( @POLICY_LOB ='13' AND @CUSTOMER_ID > 0)    --CEHCK LOB FOR POL_MARITIME          
    BEGIN        
     INSERT INTO [POL_MARITIME]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[MARITIME_ID]
           ,[VESSEL_NUMBER]
           ,[NAME_OF_VESSEL]
           ,[TYPE_OF_VESSEL]
           ,[MANUFACTURE_YEAR]
           ,[MANUFACTURER]
           ,[BUILDER]
           ,[CONSTRUCTION]
           ,[PROPULSION]
           ,[CLASSIFICATION]
           ,[LOCAL_OPERATION]
           ,[LIMIT_NAVIGATION]
           ,[PORT_REGISTRATION]
           ,[REGISTRATION_NUMBER]
           ,[TIE_NUMBER]
           ,[VESSEL_ACTION_NAUTICO_CLUB]
           ,[NAME_OF_CLUB]
           ,[LOCAL_CLUB]
           ,[NUMBER_OF_CREW]
           ,[NUMBER_OF_PASSENGER]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[LAST_UPDATED_DATETIME]
           ,[MODIFIED_BY]
           ,[CO_APPLICANT_ID])
     VALUES    
          (  @CUSTOMER_ID
			, @POLICY_ID
			, @POLICY_VERSION_ID
			, @MARITIME_ID
			, @VESSEL_NUMBER
			, @NAME_OF_VESSEL
			, @TYPE_OF_VESSEL
			, @MANUFACTURE_YEAR
			, @MANUFACTURER
			, @BUILDER
			, @CONSTRUCTION
			, @PROPULSION
			, @CLASSIFICATION
			, @LOCAL_OPERATION
			, @LIMIT_NAVIGATION
			, @PORT_REGISTRATION
			, @REGISTRATION_NUMBER
			, @TIE_NUMBER
			, @VESSEL_ACTION_NAUTICO_CLUB
			, @NAME_OF_CLUB
			, @LOCAL_CLUB
			, @NUMBER_OF_CREW
			, @NUMBER_OF_PASSENGER
			, @REMARKS
			, @IS_ACTIVE
			, @CREATED_BY
			, @CREATED_DATETIME
			, @LAST_UPDATED_DATETIME
			, @MODIFIED_BY
			, @CO_APPLICANT_ID
			)  
        
      IF @@ERROR<>0    
      ROLLBACK TRAN           
    END        
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
 END--END OF LOOP FOR [POL_MARITIME]  
 
 
 ----------TEMP TABLE FOR POL_PERSONAL_ACCIDENT_INFO ----------- 
 DECLARE
	@PERSONAL_INFO_ID int,
	--@POLICY_ID int,
	--@POLICY_VERSION_ID smallint,
	--@CUSTOMER_ID int,
	--@APPLICANT_ID int,
	@INDIVIDUAL_NAME nvarchar	(60),
	@CODE nvarchar(20),
	@POSITION_ID int,
	@CPF_NUM nvarchar(28),
	--@STATE_ID int,
	--@COUNTRY_ID int,
	@DATE_OF_BIRTH date,
	@GENDER int,
	@REG_IDEN nvarchar(24),
	@REG_ID_ISSUES date,
	@REG_ID_ORG nvarchar(100)
	--@REMARKS nvarchar,
	--@IS_ACTIVE nchar,
	--@CREATED_BY int,
	--@CREATED_DATETIME datetime,
	--@MODIFIED_BY int,
	--@LAST_UPDATED_DATETIME datetime,

	----SET @PERSONAL_INFO_ID =  
	--SET @POLICY_ID =  
	--SET @POLICY_VERSION_ID =  
	--SET @CUSTOMER_ID =  
	SET @APPLICANT_ID = 0 
	SET @INDIVIDUAL_NAME = '' 
	SET @CODE =  ''
	SET @POSITION_ID =  0
	SET @CPF_NUM = '' 
	SET @STATE_ID = 71 
	SET @COUNTRY_ID = 5 
	SET @DATE_OF_BIRTH =  '1981-10-27 00:00:00.000'
	SET @GENDER =  6615
	SET @REG_IDEN = 'TD312' 
	SET @REG_ID_ISSUES ='2010-09-08'  
	SET @REG_ID_ORG = '11111' 
	SET @REMARKS =  ''
	SET @IS_ACTIVE =  'Y'
	SET @CREATED_BY =  198
	SET @CREATED_DATETIME ='2010-10-27 00:00:00.000'  
	SET @MODIFIED_BY =  NULL
	SET @LAST_UPDATED_DATETIME =GETDATE()  

 
 
 
        
  CREATE TABLE #TEMP_POL_PERSONAL_ACCIDENT_INFO        
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),            
   INDIVIDUAL_NAME VARCHAR(MAX),
   CODE VARCHAR(MAX),
   POSITION_ID VARCHAR(MAX),
   CPF_NUM VARCHAR(MAX),
   STATE_ID VARCHAR(MAX),
   COUNTRY_ID VARCHAR(MAX),  
   REG_IDEN VARCHAR(MAX),
   REG_ID_ISSUES VARCHAR(MAX),
   REG_ID_ORG VARCHAR(MAX),          
  )        
  INSERT INTO #TEMP_POL_PERSONAL_ACCIDENT_INFO        
  (        
   POLICYNO,ENDORSEMENTNO,ITEM,INDIVIDUAL_NAME,
   CODE ,
   POSITION_ID,
   CPF_NUM ,
   STATE_ID,
   COUNTRY_ID,  
   REG_IDEN ,
   REG_ID_ISSUES,
   REG_ID_ORG 
  )         
        
  SELECT   
   POLICYNO,ENDORSEMENTNO,ITEM,InsuredName,
   CODE ,POSITION_ID,InsuredId ,LocationState,LocationCountry,
   REG_IDEN ,REG_ID_ISSUES,REG_ID_ORG
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
            
    
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_PERSONAL_ACCIDENT_INFO  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @PERSONAL_INFO_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END ,
    @INDIVIDUAL_NAME = INDIVIDUAL_NAME,
    @CODE = CODE,
    @POSITION_ID= CASE WHEN ISNULL(POSITION_ID,0)<>'' THEN CONVERT(INT,POSITION_ID) END ,    
	@CPF_NUM = CPF_NUM,
	@STATE_ID = CASE WHEN ISNULL(STATE_ID,0)<>'' THEN CONVERT(INT,STATE_ID) END ,    
	@COUNTRY_ID = CASE WHEN ISNULL(COUNTRY_ID,0)<>'' THEN CONVERT(INT,COUNTRY_ID) END ,      
	@REG_IDEN =REG_IDEN ,
	@REG_ID_ISSUES = CASE  WHEN ISNULL(REG_ID_ISSUES,'')<>'' THEN        
			  CONVERT(DATETIME,REG_ID_ISSUES,103) END,        
	@REG_ID_ORG = REG_ID_ORG 
    FROM #TEMP_POL_PERSONAL_ACCIDENT_INFO WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
            
            
    
    IF((@POLICY_LOB ='15' OR @POLICY_LOB ='21') AND @CUSTOMER_ID > 0)    --CEHCK LOB FOR POL_PERSONAL_ACCIDENT_INFO          
    BEGIN        
     INSERT INTO [POL_PERSONAL_ACCIDENT_INFO]
           ([PERSONAL_INFO_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[CUSTOMER_ID]
           ,[APPLICANT_ID]
           ,[INDIVIDUAL_NAME]
           ,[CODE]
           ,[POSITION_ID]
           ,[CPF_NUM]
           ,[STATE_ID]
           ,[COUNTRY_ID]
           ,[DATE_OF_BIRTH]
           ,[GENDER]
           ,[REG_IDEN]
           ,[REG_ID_ISSUES]
           ,[REG_ID_ORG]
           ,[REMARKS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME])
      VALUES
       (
         @PERSONAL_INFO_ID
		,@POLICY_ID
		,@POLICY_VERSION_ID
		,@CUSTOMER_ID
		,@APPLICANT_ID
		,@INDIVIDUAL_NAME
		,@CODE
		,@POSITION_ID
		,@CPF_NUM
		,@STATE_ID
		,@COUNTRY_ID
		,@DATE_OF_BIRTH
		,@GENDER
		,@REG_IDEN
		,@REG_ID_ISSUES
		,@REG_ID_ORG
		,@REMARKS
		,@IS_ACTIVE
		,@CREATED_BY
		,@CREATED_DATETIME
		,@MODIFIED_BY
		,@LAST_UPDATED_DATETIME
       )
           
          
      IF @@ERROR<>0    
      ROLLBACK TRAN           
    END        
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
 END--END OF LOOP 
            
  
----------TEMP TABLE FOR POL_PASSENGERS_PERSONAL_ACCIDENT_INFO ----------- 
 DECLARE
	--@CUSTOMER_ID	int,
	--@POLICY_ID	int,
	--@POLICY_VERSION_ID	smallint,
	@PERSONAL_ACCIDENT_ID	int,
	@START_DATE	datetime,
	@END_DATE	datetime,
	@NUMBER_OF_PASSENGERS	numeric(10,0)
	--@IS_ACTIVE	nchar(2),
	--@CREATED_BY	int,
	--@CREATED_DATETIME	datetime,
	--@MODIFIED_BY	int,
	--@LAST_UPDATED_DATETIME	datetime,
	--@CO_APPLICANT_ID	int,


	--SET @CUSTOMER_ID= 
	--@POLICY_ID
	--@POLICY_VERSION_ID
	SET @PERSONAL_ACCIDENT_ID =1
	SET @START_DATE ='2010-10-27 00:00:00.000'
	SET @END_DATE= '2010-10-27 00:00:00.000'
	SET @NUMBER_OF_PASSENGERS=0
	SET @IS_ACTIVE='Y'
	SET @CREATED_BY=198
	SET @CREATED_DATETIME ='2010-10-27 00:00:00.000'
	SET @MODIFIED_BY =NULL
	SET @LAST_UPDATED_DATETIME =GETDATE()
	SET @CO_APPLICANT_ID= 0
        
  CREATE TABLE #TEMP_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO       
  (        
   ID INT IDENTITY(1,1),        
   POLICYNO VARCHAR(MAX),  
   ENDORSEMENTNO VARCHAR(MAX),       
   ITEM VARCHAR(MAX),            
   [Insured Effective Date] VARCHAR(MAX),
   [Insured Expire Date] VARCHAR(MAX),
   [Number of Passengers] VARCHAR(MAX)
       
  )        
  INSERT INTO #TEMP_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO      
  (        
   POLICYNO,ENDORSEMENTNO,ITEM,
   [Insured Effective Date],
   [Insured Expire Date],
   [Number of Passengers]
  )         
        
  SELECT   
   POLICYNO,ENDORSEMENTNO,ITEM,
   [Insured Effective Date],
   --[Insured Expire Date],
   CASE  WHEN ISNULL([Insured Expire Date],'')<>'' THEN        
			  CONVERT(DATETIME,[Insured Expire Date],103) END,
   [Number of Passengers]
  FROM ImportNew_InsuredObject WITH (NOLOCK)         
            
    
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1    
           
   WHILE @MAXCOUNT >0        
   BEGIN          
           
    SELECT         
    @PolicyNo = POLICYNO,  
    @EndorsementNo = EndorsementNo,  
    @PERSONAL_ACCIDENT_ID = CASE WHEN ISNULL(ITEM,0)<>'' THEN CONVERT(INT,ITEM) END ,
	@START_DATE= CASE  WHEN ISNULL([Insured Effective Date],'')<>'' THEN        
			  CONVERT(DATETIME,[Insured Effective Date],103) END,
			   
    @END_DATE= CASE  WHEN ISNULL([Insured Expire Date],'')<>'' THEN        
			  CONVERT(DATETIME,[Insured Expire Date],103) END,
    
    @NUMBER_OF_PASSENGERS = CASE WHEN ISNULL([Number of Passengers],0)<>'' THEN CONVERT(NUMERIC,[Number of Passengers]) END  
    
    FROM #TEMP_POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE ID=@ROW_ID        
            
            
     --GET CUSTOMER_ID AND POLIYC_ID        
     SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID,        
     @POLICY_LOB =POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
    --SELECT @LOCATION_ID =ISNULL(MAX(LOCATION_ID),0)+1 FROM POL_LOCATIONS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID        
            
    
    IF(@POLICY_LOB ='22' AND @CUSTOMER_ID > 0)    --CEHCK LOB FOR POL_PASSENGERS_PERSONAL_ACCIDENT_INFO          
    BEGIN        
     INSERT INTO [POL_PASSENGERS_PERSONAL_ACCIDENT_INFO]
           ([CUSTOMER_ID]
           ,[POLICY_ID]
           ,[POLICY_VERSION_ID]
           ,[PERSONAL_ACCIDENT_ID]
           ,[START_DATE]
           ,[END_DATE]
           ,[NUMBER_OF_PASSENGERS]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[MODIFIED_BY]
           ,[LAST_UPDATED_DATETIME]
           ,[CO_APPLICANT_ID])
     VALUES
      (  @CUSTOMER_ID
		,@POLICY_ID
		,@POLICY_VERSION_ID
		,@PERSONAL_ACCIDENT_ID
		,@START_DATE
		,@END_DATE
		,@NUMBER_OF_PASSENGERS
		,@IS_ACTIVE
		,@CREATED_BY
		,@CREATED_DATETIME
		,@MODIFIED_BY
		,@LAST_UPDATED_DATETIME
		,@CO_APPLICANT_ID

	  )
          
      IF @@ERROR<>0    
      ROLLBACK TRAN           
    END        
            
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
           
           
 END--END OF LOOP FOR [POL_MARITIME]    
     
                     
-----------------------------TEMP TABLE FOR POL_PRODUCT_COVERAGES-----------        
          
  DECLARE                   
     --@CUSTOMER_ID int,                   
     --@POLICY_ID int,                   
     --@POLICY_VERSION_ID smallint,                   
     --@RISK_ID smallint,                   
     @COVERAGE_ID int,                   
     @COVERAGE_CODE_ID int,                   
     @RI_APPLIES nchar (2),                  
     @LIMIT_OVERRIDE nvarchar (10),                 
     @LIMIT_1 decimal (18,0),                  
     @LIMIT_1_TYPE nvarchar (10),                  
     @LIMIT_2 decimal (18,0),                  
     @LIMIT_2_TYPE nvarchar (10),                  
     @LIMIT1_AMOUNT_TEXT nvarchar (200),                  
     @LIMIT2_AMOUNT_TEXT nvarchar (200),                  
     @DEDUCT_OVERRIDE nchar (2),                  
     @DEDUCTIBLE_1 decimal (18,0),                  
     @DEDUCTIBLE_1_TYPE nvarchar (12),                  
     @DEDUCTIBLE_2 decimal (18,0),                  
     @DEDUCTIBLE_2_TYPE nvarchar (10),                
     @MINIMUM_DEDUCTIBLE decimal (18,0),                  
     @DEDUCTIBLE1_AMOUNT_TEXT nvarchar (1000),                  
     @DEDUCTIBLE2_AMOUNT_TEXT nvarchar (2000),                  
     @DEDUCTIBLE_REDUCES nvarchar (2),                  
     @INITIAL_RATE decimal (8,4),                  
     @FINAL_RATE decimal (8,4),                  
     @AVERAGE_RATE nchar (2),                  
     @WRITTEN_PREMIUM decimal (18,2),                  
     @FULL_TERM_PREMIUM decimal (18,2),                  
     @IS_SYSTEM_COVERAGE nchar (2),                  
     @LIMIT_ID int,          
     @DEDUC_ID int ,                  
     @ADD_INFORMATION nvarchar (40),                  
     --@CREATED_BY int,                   
     --@CREATED_DATETIME datetime,                   
     --@MODIFIED_BY int,                   
     --@LAST_UPDATED_DATETIME datetime,                   
     @INDEMNITY_PERIOD int                   
              
              
               
         
     --SET  @CUSTOMER_ID = 2525                  
     --SET  @POLICY_ID = 1                  
     --SET  @POLICY_VERSION_ID = 1                  
     SET  @RISK_ID = 1                  
     SET  @COVERAGE_ID = 1                  
     SET  @COVERAGE_CODE_ID = 1086                  
     SET  @RI_APPLIES = 'N'                  
     SET  @LIMIT_OVERRIDE =NULL                   
     SET  @LIMIT_1 = 5000               
     SET  @LIMIT_1_TYPE = NULL                  
     SET  @LIMIT_2 = NULL                  
     SET  @LIMIT_2_TYPE =NULL                   
     SET  @LIMIT1_AMOUNT_TEXT = NULL                  
     SET  @LIMIT2_AMOUNT_TEXT = NULL                  
     SET  @DEDUCT_OVERRIDE = NULL                  
     SET  @DEDUCTIBLE_1 = NULL                  
     SET  @DEDUCTIBLE_1_TYPE =14573                   
     SET  @DEDUCTIBLE_2 = NULL                  
     SET  @DEDUCTIBLE_2_TYPE =NULL                   
     SET  @MINIMUM_DEDUCTIBLE = NULL                  
     SET  @DEDUCTIBLE1_AMOUNT_TEXT =NULL                   
     SET  @DEDUCTIBLE2_AMOUNT_TEXT = ''                  
     SET  @DEDUCTIBLE_REDUCES = 'N'                  
     SET  @INITIAL_RATE =NULL                   
     SET  @FINAL_RATE = NULL                  
     SET  @AVERAGE_RATE ='N'                    
     SET  @WRITTEN_PREMIUM =5000.00                  
     SET  @FULL_TERM_PREMIUM = NULL                  
     SET  @IS_SYSTEM_COVERAGE = NULL                  
     SET  @LIMIT_ID = NULL                  
     SET  @DEDUC_ID = NULL                  
     SET  @ADD_INFORMATION = NULL                  
     SET  @CREATED_BY = 198                     
     SET  @CREATED_DATETIME = '2010-10-27 15:42:38.703'                  
     SET  @MODIFIED_BY = 198                  
     SET  @LAST_UPDATED_DATETIME ='2010-10-27 00:00:00.000'                   
     SET  @INDEMNITY_PERIOD = 4          
                                
  CREATE TABLE #TEMP_POL_PRODUCT_COVERAGE        
  (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),    
    item varchar(max),        
    EndorsementNo VARCHAR(MAX),         
    [COVERAGE CODE] VARCHAR(MAX),        
    [COVERAGE NAME]  VARCHAR(MAX),        
    [SUM INSURED]  VARCHAR(MAX),        
    RATE     VARCHAR(MAX),        
    PREMIUM  VARCHAR(MAX),        
     [DeductibleType] VARCHAR(MAX),        
   [DeductibleValue] VARCHAR(MAX),        
   [DEDUCTIBLEMINAMOUNT] VARCHAR(MAX),        
    DeducbtibleDescripition  VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_POL_PRODUCT_COVERAGE        
 (        
  POLICYNO,        
  EndorsementNo,    
  item,         
  [COVERAGE CODE],        
  [COVERAGE NAME],        
  [SUM INSURED],        
  RATE ,        
   PREMIUM,        
   [DeductibleType],         
   [DeductibleValue],        
   [DEDUCTIBLEMINAMOUNT],        
   DeducbtibleDescripition        
 )        
  SELECT POLICYNO,        
   EndorsementNo,    
   item,         
   [COVERAGE CODE],        
   [COVERAGE NAME],        
   [SUM INSURED],        
   RATE ,        
   PREMIUM,        
   [DeductibleType],         
   [DeductibleValue],        
   [DEDUCTIBLEMINAMOUNT],        
   DeducbtibleDescripition        
  FROM ImportNew_Coverage WITH (NOLOCK)         
         
          
          
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_POL_PRODUCT_COVERAGE  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
   WHILE @MAXCOUNT >0        
   BEGIN              
     --ITEM/RISK NEED TO CLEARIFY         
             
      SELECT @PolicyNo = POLICYNO,        
       @POLICY_DISP_VERSION = EndorsementNo,    
       @RISK_ID =  CASE WHEN ISNULL(item,0)<>'' THEN  CONVERT(INT,item) END,       
       @COVERAGE_CODE_ID= CASE WHEN ISNULL([COVERAGE CODE],0)<>'' THEN  CONVERT(INT,[COVERAGE CODE]) END,        
       @LIMIT_1 =  CASE WHEN ISNULL([SUM INSURED],0)<>'' THEN  CONVERT(DECIMAL(18,0),[SUM INSURED]) END,    
       @INITIAL_RATE = CASE WHEN ISNULL(RATE,0)<>'' THEN  CONVERT(DECIMAL(18,0),RATE) END,       
       @WRITTEN_PREMIUM =  CASE WHEN ISNULL(PREMIUM,0)<>'' THEN  CONVERT(DECIMAL(18,0),PREMIUM) END,      
       @DEDUCTIBLE_1_TYPE = [DeductibleType],      
       @DEDUCTIBLE_1 = CASE WHEN ISNULL([DeductibleValue],0)<>'' THEN  CONVERT(DECIMAL(18,0),[DeductibleValue]) END,    
       @MINIMUM_DEDUCTIBLE = CASE WHEN ISNULL([DEDUCTIBLEMINAMOUNT],0)<>'' THEN  CONVERT(DECIMAL(18,0),[DEDUCTIBLEMINAMOUNT]) END,        
       @DEDUCTIBLE1_AMOUNT_TEXT =DeducbtibleDescripition        
      FROM  #TEMP_POL_PRODUCT_COVERAGE WHERE ID = @ROW_ID        
            
   --GET CUSTOMER_ID AND POLIYC_ID        
   SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
    FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
            
            
    SELECT @COVERAGE_ID = ISNULL(MAX(COVERAGE_ID),0)+1 FROM [POL_PRODUCT_COVERAGES] WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID   
    AND RISK_ID = @RISK_ID       
          
       
   IF(@CUSTOMER_ID > 0)        
   BEGIN         
    INSERT INTO [POL_PRODUCT_COVERAGES]          
    (            
     [CUSTOMER_ID],[POLICY_ID],[POLICY_VERSION_ID] ,[RISK_ID],[COVERAGE_ID]                  
       ,[COVERAGE_CODE_ID] ,[RI_APPLIES]    ,[LIMIT_OVERRIDE]     ,[LIMIT_1]                  
       ,[LIMIT_1_TYPE]    ,[LIMIT_2]    ,[LIMIT_2_TYPE]    ,[LIMIT1_AMOUNT_TEXT]                  
       ,[LIMIT2_AMOUNT_TEXT]    ,[DEDUCT_OVERRIDE]    ,[DEDUCTIBLE_1]    ,[DEDUCTIBLE_1_TYPE]                  
       ,[DEDUCTIBLE_2]     ,[DEDUCTIBLE_2_TYPE]    ,[MINIMUM_DEDUCTIBLE]        
       ,[DEDUCTIBLE1_AMOUNT_TEXT]                  
       ,[DEDUCTIBLE2_AMOUNT_TEXT] ,[DEDUCTIBLE_REDUCES]    ,[INITIAL_RATE]    ,[FINAL_RATE]                  
       ,[AVERAGE_RATE]    ,[WRITTEN_PREMIUM]    ,[FULL_TERM_PREMIUM]    ,[IS_SYSTEM_COVERAGE]                  
       ,[LIMIT_ID]    ,[DEDUC_ID]    ,[ADD_INFORMATION]    ,[CREATED_BY]    ,[CREATED_DATETIME]                  
       ,[MODIFIED_BY]    ,[LAST_UPDATED_DATETIME]    ,[INDEMNITY_PERIOD]        
     )                   
    VALUES         
    (                 
      @CUSTOMER_ID    ,@POLICY_ID    ,@POLICY_VERSION_ID    ,@RISK_ID    ,@COVERAGE_ID                  
     ,@COVERAGE_CODE_ID    ,@RI_APPLIES    ,@LIMIT_OVERRIDE    ,@LIMIT_1    ,@LIMIT_1_TYPE                  
     ,@LIMIT_2    ,@LIMIT_2_TYPE    ,@LIMIT1_AMOUNT_TEXT    ,@LIMIT2_AMOUNT_TEXT    ,@DEDUCT_OVERRIDE                       ,@DEDUCTIBLE_1    ,@DEDUCTIBLE_1_TYPE    ,@DEDUCTIBLE_2    ,@DEDUCTIBLE_2_TYPE    ,@MINIMUM_DEDUCTIBLE                  
     ,@DEDUCTIBLE1_AMOUNT_TEXT      
       ,@DEDUCTIBLE2_AMOUNT_TEXT    ,@DEDUCTIBLE_REDUCES    ,@INITIAL_RATE                  
     ,@FINAL_RATE    ,@AVERAGE_RATE    ,@WRITTEN_PREMIUM    ,@FULL_TERM_PREMIUM    ,@IS_SYSTEM_COVERAGE                  
     ,@LIMIT_ID    ,@DEDUC_ID    ,@ADD_INFORMATION    ,@CREATED_BY    ,@CREATED_DATETIME    ,@MODIFIED_BY                  
     ,@LAST_UPDATED_DATETIME    ,@INDEMNITY_PERIOD              
    )   
  IF @@ERROR<>0    
  ROLLBACK TRAN        
   END        
           
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
   END -- FOR POL_PRODUCT_COVERAGE        
        
      
  -----------------------------TEMP TABLE FOR [ACT_POLICY_INSTALL_PLAN_DATA]-----------          
  DECLARE             
   --@POLICY_ID int,              
   --@POLICY_VERSION_ID int,            
   --@CUSTOMER_ID int,            
   @PLAN_ID int,            
   --@APP_ID int,            
   --@APP_VERSION_ID int,            
   @PLAN_DESCRIPTION nvarchar (70),                        
   @PLAN_TYPE nvarchar (20),                        
   @NO_OF_PAYMENTS smallint,            
   @MONTHS_BETWEEN smallint,            
   @PERCENT_BREAKDOWN1 decimal (10,4),            
   @PERCENT_BREAKDOWN2 decimal (10,4),            
   @PERCENT_BREAKDOWN3 decimal (10,4),            
   @PERCENT_BREAKDOWN4 decimal (10,4),            
   @PERCENT_BREAKDOWN5 decimal (10,4),            
   @PERCENT_BREAKDOWN6 decimal (10,4),            
   @PERCENT_BREAKDOWN7 decimal (10,4),            
   @PERCENT_BREAKDOWN8 decimal (10,4),            
   @PERCENT_BREAKDOWN9 decimal (10,4),            
   @PERCENT_BREAKDOWN10 decimal (10,4),            
   @PERCENT_BREAKDOWN11 decimal (10,4),            
   @PERCENT_BREAKDOWN12 decimal (10,4),            
   @MODE_OF_DOWN_PAYMENT int,            
   @INSTALLMENTS_IN_DOWN_PAYMENT int,            
   @MODE_OF_PAYMENT int,            
   --@CURRENT_TERM smallint,            
   @IS_ACTIVE_PLAN char(1),                        
   @TOTAL_PREMIUM decimal (12,2),            
   @TOTAL_INTEREST_AMOUNT decimal(12,2),            
   @TOTAL_FEES decimal (12,2),            
   @TOTAL_TAXES decimal (12,2),            
   @TOTAL_AMOUNT decimal (12,2),            
   @TRAN_TYPE nvarchar (20),            
   @TOTAL_TRAN_PREMIUM decimal (12,2),            
   @TOTAL_TRAN_INTEREST_AMOUNT decimal(12,2),            
   @TOTAL_TRAN_FEES decimal (12,2),            
   @TOTAL_TRAN_TAXES decimal (12,2),            
   @TOTAL_TRAN_AMOUNT decimal (12,2),            
   --@CREATED_BY int,             
   --@CREATED_DATETIME datetime,            
   --@MODIFIED_BY int,              
   --@LAST_UPDATED_DATETIME datetime,                         
   @TOTAL_CHANGE_INFORCE_PRM decimal (18,2),            
   @PRM_DIST_TYPE int,            
   @TOTAL_INFO_PRM int,            
   @TOTAL_STATE_FEES decimal(12, 2),            
   @TOTAL_TRAN_STATE_FEES decimal (12,2)            
   --@CO_APPLICANT_ID int               
               
               
    --SET @POLICY_ID =1            
    --SET @POLICY_VERSION_ID =1            
    --SET @CUSTOMER_ID =2220            
    SET @PLAN_ID = 55            
    --SET @APP_ID = 1            
    SET @APP_VERSION_ID =1            
    SET @PLAN_DESCRIPTION ='0 Down payment'            
    SET @PLAN_TYPE = NULL            
    SET @NO_OF_PAYMENTS =6            
    SET @MONTHS_BETWEEN =0            
    SET @PERCENT_BREAKDOWN1 =16.6650            
    SET @PERCENT_BREAKDOWN2 =16.6670            
    SET @PERCENT_BREAKDOWN3 =16.6670            
    SET @PERCENT_BREAKDOWN4 =16.6670            
    SET @PERCENT_BREAKDOWN5 =16.6670            
    SET @PERCENT_BREAKDOWN6 =16.6670            
    SET @PERCENT_BREAKDOWN7 =0.0000            
    SET @PERCENT_BREAKDOWN8 =0.0000            
    SET @PERCENT_BREAKDOWN9 =0.0000            
    SET @PERCENT_BREAKDOWN10 =0.0000            
    SET @PERCENT_BREAKDOWN11 =0.0000            
    SET @PERCENT_BREAKDOWN12 =0.0000            
    SET @MODE_OF_DOWN_PAYMENT =11974            
    SET @INSTALLMENTS_IN_DOWN_PAYMENT =0            
    SET @MODE_OF_PAYMENT =0            
    SET @CURRENT_TERM =1            
    SET @IS_ACTIVE_PLAN ='Y'            
    SET @TOTAL_PREMIUM =40000.00            
    SET @TOTAL_INTEREST_AMOUNT =0.00            
    SET @TOTAL_FEES =0.00            
    SET @TOTAL_TAXES =0.00            
    SET @TOTAL_AMOUNT =40000.00            
    SET @TRAN_TYPE ='NBS'            
    SET @TOTAL_TRAN_PREMIUM =40000.00            
    SET @TOTAL_TRAN_INTEREST_AMOUNT =0.00            
    SET @TOTAL_TRAN_FEES =0.00            
    SET @TOTAL_TRAN_TAXES =0.00            
    SET @TOTAL_TRAN_AMOUNT =40000.00            
    SET @CREATED_BY =198            
    SET @CREATED_DATETIME ='2010-10-29 12:53:30.000'            
    SET @MODIFIED_BY =NULL            
    SET @LAST_UPDATED_DATETIME =GETDATE()            
    SET @TOTAL_CHANGE_INFORCE_PRM =40000.00            
    SET @PRM_DIST_TYPE =NULL            
    SET @TOTAL_INFO_PRM =40000            
    SET @TOTAL_STATE_FEES =0.00            
    SET @TOTAL_TRAN_STATE_FEES =0.00            
    SET @CO_APPLICANT_ID =0            
    SET @CREATED_BY=198                
    SET @CREATED_DATETIME='2010-10-27 10:08:07.233'                
    SET @MODIFIED_BY=NULL                
    SET @LAST_UPDATED_DATETIME=GETDATE()                
           
         
    CREATE TABLE #TEMP_ACT_POLICY_INSTALL_PLAN_DATA        
  (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),        
    EndorsementNo VARCHAR(MAX),        
    NetPremium VARCHAR(MAX),       
    IssuanceCost VARCHAR(MAX),       
   Tax VARCHAR(MAX),       
   Insterest VARCHAR(MAX),       
   GrossPremium VARCHAR(MAX),       
   PaymentMethod VARCHAR(MAX)      
        
  )        
  INSERT INTO #TEMP_ACT_POLICY_INSTALL_PLAN_DATA        
  (          
    PolicyNo,        
    EndorsementNo,        
    NetPremium ,       
    IssuanceCost,       
    Tax,       
   Insterest,       
   GrossPremium ,       
   PaymentMethod      
  )        
   SELECT       
    PolicyNo,        
    EndorsementNo,        
    NetPremium ,       
    IssuanceCost,       
    Tax,       
    Insterest,       
   GrossPremium ,       
   PaymentMethod      
   FROM  Importnew_Policy_Info WITH (NOLOCK)  WHERE CONVERT(INT,TransactionType) <>19      
         
   SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_ACT_POLICY_INSTALL_PLAN_DATA  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1        
           
   WHILE @MAXCOUNT >0        
   BEGIN        
         
    SELECT  @PolicyNo = PolicyNo,        
      @EndorsementNo = EndorsementNo,        
      @TOTAL_PREMIUM =  CASE WHEN ISNULL(NetPremium,'')<>'' THEN  CONVERT(DECIMAL(12,2),NetPremium) END,       
      @TOTAL_TAXES = CASE WHEN ISNULL(Tax,'')<>'' THEN  CONVERT(DECIMAL(12,2),Tax) END,     
      @TOTAL_INTEREST_AMOUNT = CASE WHEN ISNULL(Insterest,'')<>'' THEN  CONVERT(DECIMAL(12,2),Insterest) END,       
      @TOTAL_AMOUNT = CASE WHEN ISNULL(GrossPremium,'')<>'' THEN  CONVERT(DECIMAL(12,2),GrossPremium) END,     
      @MODE_OF_DOWN_PAYMENT = CASE WHEN ISNULL(PaymentMethod,'')<>'' THEN  CONVERT(INT,PaymentMethod) END,      
      @MODE_OF_PAYMENT =CASE WHEN ISNULL(PaymentMethod,'')<>'' THEN  CONVERT(INT,PaymentMethod) END        
          
  FROM    #TEMP_ACT_POLICY_INSTALL_PLAN_DATA  WHERE ID=@ROW_ID    
           
  --GET CUSTOMER_ID AND POLIYC_ID        
  SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
  FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo       
      
   IF(  @CUSTOMER_ID>0)    
   BEGIN       
   IF NOT EXISTS (SELECT CUSTOMER_ID FROM ACT_POLICY_INSTALL_PLAN_DATA WHERE CUSTOMER_ID =@CUSTOMER_ID AND @POLICY_ID=POLICY_ID)         
   BEGIN       
    INSERT INTO [ACT_POLICY_INSTALL_PLAN_DATA]            
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
    ,[TOTAL_FEES]            
    ,[TOTAL_TAXES]            
    ,[TOTAL_AMOUNT]            
    ,[TRAN_TYPE]            
    ,[TOTAL_TRAN_PREMIUM]            
    ,[TOTAL_TRAN_INTEREST_AMOUNT]            
    ,[TOTAL_TRAN_FEES]            
    ,[TOTAL_TRAN_TAXES]            
    ,[TOTAL_TRAN_AMOUNT]            
    ,[CREATED_BY]            
    ,[CREATED_DATETIME]            
    ,[MODIFIED_BY]            
    ,[LAST_UPDATED_DATETIME]            
    ,[TOTAL_CHANGE_INFORCE_PRM]            
    ,[PRM_DIST_TYPE]            
    ,[TOTAL_INFO_PRM]            
    ,[TOTAL_STATE_FEES]            
    ,[TOTAL_TRAN_STATE_FEES]            
    ,[CO_APPLICANT_ID])            
    VALUES            
    ( @POLICY_ID            
  ,@POLICY_VERSION_ID            
  ,@CUSTOMER_ID            
  ,@PLAN_ID            
  ,@APP_ID            
  ,@APP_VERSION_ID            
  ,@PLAN_DESCRIPTION            
  ,@PLAN_TYPE            
  ,@NO_OF_PAYMENTS            
  ,@MONTHS_BETWEEN            
  ,@PERCENT_BREAKDOWN1            
  ,@PERCENT_BREAKDOWN2            
  ,@PERCENT_BREAKDOWN3            
  ,@PERCENT_BREAKDOWN4            
  ,@PERCENT_BREAKDOWN5            
  ,@PERCENT_BREAKDOWN6            
  ,@PERCENT_BREAKDOWN7            
  ,@PERCENT_BREAKDOWN8            
  ,@PERCENT_BREAKDOWN9            
  ,@PERCENT_BREAKDOWN10            
  ,@PERCENT_BREAKDOWN11            
  ,@PERCENT_BREAKDOWN12            
  ,@MODE_OF_DOWN_PAYMENT            
  ,@INSTALLMENTS_IN_DOWN_PAYMENT            
  ,@MODE_OF_PAYMENT            
  ,@CURRENT_TERM            
  ,@IS_ACTIVE_PLAN            
  ,@TOTAL_PREMIUM            
  ,@TOTAL_INTEREST_AMOUNT            
  ,@TOTAL_FEES            
  ,@TOTAL_TAXES            
  ,@TOTAL_AMOUNT            
  ,@TRAN_TYPE            
  ,@TOTAL_TRAN_PREMIUM            
  ,@TOTAL_TRAN_INTEREST_AMOUNT            
  ,@TOTAL_TRAN_FEES            
  ,@TOTAL_TRAN_TAXES            
  ,@TOTAL_TRAN_AMOUNT            
  ,@CREATED_BY            
  ,@CREATED_DATETIME            
  ,@MODIFIED_BY            
  ,@LAST_UPDATED_DATETIME            
  ,@TOTAL_CHANGE_INFORCE_PRM            
  ,@PRM_DIST_TYPE            
  ,@TOTAL_INFO_PRM            
  ,@TOTAL_STATE_FEES            
  ,@TOTAL_TRAN_STATE_FEES            
  ,@CO_APPLICANT_ID            
  )   
  IF @@ERROR<>0    
  ROLLBACK TRAN        
   END  --CHECK ON ACT_POLICY_INSTALL_PLAN_DATA    
   END  -- CHECK ON CUSTOMER_ID      
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
  END -- FOR INSERT IN [ACT_POLICY_INSTALL_PLAN_DATA]        
         
     
         
         
-----------------------------TEMP TABLE FOR ACT_POLICY_INSTALLMENT_DETAILS-----------        
        
DECLARE                   
    --@POLICY_ID int                   
    --@POLICY_VERSION_ID int                   
    --@CUSTOMER_ID int                   
    --@APP_ID int                   
    --@APP_VERSION_ID int                   
    @INSTALLMENT_AMOUNT decimal (25,2),                  
    @INSTALLMENT_EFFECTIVE_DATE datetime,                   
    @RELEASED_STATUS char (1),                  
    @NEWROW_ID int,                   
    @INSTALLMENT_NO int,                   
    --@RISK_ID int,                   
    --@RISK_TYPE varchar (15),                  
    @PAYMENT_MODE int,                   
    --@CURRENT_TERM smallint,                   
    @PERCENTAG_OF_PREMIUM decimal (9,4),                  
    @INTEREST_AMOUNT decimal (12,2),                  
    @FEE decimal (12,2),                  
    @TAXES decimal (12,2),                  
    @TOTAL decimal (12,2),                  
    @TRAN_INTEREST_AMOUNT decimal (12,2),                  
    @TRAN_FEE decimal (12,2),                  
    @TRAN_TAXES decimal (12,2),                  
    @TRAN_TOTAL decimal (12,2),                  
    @BOLETO_NO nvarchar (200),                  
    @IS_BOLETO_GENRATED nchar (2),                  
    --@CREATED_BY int,                  
    --@CREATED_DATETIME datetime,                   
    --@MODIFIED_BY int,                   
    --@LAST_UPDATED_DATETIME datetime,                   
    @TRAN_PREMIUM_AMOUNT decimal (12,2)                  
                      
                      
    --SET  @POLICY_ID = 1                 
    --SET  @POLICY_VERSION_ID =1                  
   -- SET  @CUSTOMER_ID = 2525                 
    --SET  @APP_ID =  1                
    SET  @APP_VERSION_ID = 1                 
    SET  @INSTALLMENT_AMOUNT =  6666.00                
    SET  @INSTALLMENT_EFFECTIVE_DATE =  '2010-10-27 00:00:00.000'                
    SET  @RELEASED_STATUS = 'N'                 
    SET  @NEWROW_ID =1                  
    --SET  @INSTALLMENT_NO =                  
    SET  @RISK_ID = 1                 
    SET  @RISK_TYPE = 'ARPERIL'                 
    SET  @PAYMENT_MODE = 11974                 
    SET  @CURRENT_TERM =  1                
    SET  @PERCENTAG_OF_PREMIUM = 16.6650                 
    SET  @INTEREST_AMOUNT = 10.00                 
    SET  @FEE =    100.00            
    SET  @TAXES =  0.00                
    SET  @TOTAL =  6666.00               
    SET  @TRAN_INTEREST_AMOUNT = 0.00                 
    SET  @TRAN_FEE =  0.00                
    SET  @TRAN_TAXES =   0.00               
    SET  @TRAN_TOTAL = 0.00                 
    SET  @BOLETO_NO =  NULL                
    SET  @IS_BOLETO_GENRATED = NULL                 
    SET  @CREATED_BY = 198      
    SET  @CREATED_DATETIME = '2010-10-29 12:53:30.000'                 
    SET  @MODIFIED_BY = NULL                 
    SET  @LAST_UPDATED_DATETIME = NULL                 
    SET  @TRAN_PREMIUM_AMOUNT =  0.00              
                  
                  
    SET  @INSTALLMENT_NO = 1              
    SET  @PERCENTAG_OF_PREMIUM = 16.6650          
            
 CREATE TABLE #TEMP_ACT_POLICY_INSTALLMENT_DETAILS        
  (        
    ID INT IDENTITY(1,1),        
    PolicyNo VARCHAR(MAX),        
    EndorsementNo VARCHAR(MAX),        
    InstallmentNo VARCHAR(MAX),        
    BarCodeNumber VARCHAR(MAX),        
    InvoiceNumber VARCHAR(MAX),        
    InstallmentIssuanceDate VARCHAR(MAX),        
    InstallmentDueDate VARCHAR(MAX),        
    InstallmentAmount VARCHAR(MAX),        
    InstallmentIssuanceCost VARCHAR(MAX),        
    InstallmentTax VARCHAR(MAX),        
    InstallmentInsterest VARCHAR(MAX),        
    InstallmentStatus VARCHAR(MAX)        
  )        
  INSERT INTO #TEMP_ACT_POLICY_INSTALLMENT_DETAILS        
  (        
   PolicyNo,        
   EndorsementNo,        
   InstallmentNo,        
   BarCodeNumber,        
   InvoiceNumber,        
   InstallmentIssuanceDate,        
   InstallmentDueDate,        
   InstallmentAmount,        
   InstallmentIssuanceCost,        
   InstallmentTax,        
   InstallmentInsterest,        
   InstallmentStatus        
  )        
   SELECT PolicyNo,        
    EndorsementNo,        
    InstallmentNo,        
    BarCodeNumber,        
    InvoiceNumber,        
    InstallmentIssuanceDate,        
    InstallmentDueDate,        
    InstallmentAmount,        
    InstallmentIssuanceCost,        
    InstallmentTax,        
    InstallmentInsterest,        
    InstallmentStatus        
 FROM ImportNew_BillingPlan WITH (NOLOCK)         
          
  SELECT @MAXCOUNT = COUNT(*) FROM #TEMP_ACT_POLICY_INSTALLMENT_DETAILS  --WHERE InsuredName=@CUSTOMER_NAME AND InsuredId=@CUSTOMER_ID        
   SET @ROW_ID=1      
       
    SET IDENTITY_INSERT [ACT_POLICY_INSTALLMENT_DETAILS] ON      
           
   WHILE @MAXCOUNT >0        
   BEGIN  --1            
     --ITEM/RISK NEED TO CLEARIFY         
            
      SELECT @PolicyNo = POLICYNO,        
       @POLICY_DISP_VERSION = EndorsementNo,                 
       --CO-APPLICANT/BARCODENUMBER/INVOICENUMER NEED TO CLEARIFY/ROWID                
       @INSTALLMENT_NO = CASE WHEN ISNULL(InstallmentNo,'')<>'' THEN  CONVERT(INT,InstallmentNo) END,        
       @CREATED_DATETIME =  CASE  WHEN ISNULL(InstallmentIssuanceDate,'')<>'' THEN        
          CONVERT(DATETIME,InstallmentIssuanceDate,103) END,    --InstallmentIssuanceDate,        
       @INSTALLMENT_EFFECTIVE_DATE = CASE  WHEN ISNULL(InstallmentDueDate,'')<>'' THEN        
          CONVERT(DATETIME,InstallmentDueDate,103) END,--InstallmentDueDate,        
       @INSTALLMENT_AMOUNT = CASE WHEN ISNULL(InstallmentAmount,'')<>'' THEN  CONVERT(decimal (12,2),InstallmentAmount) END,    
       @TAXES = CASE WHEN ISNULL(InstallmentTax,'')<>'' THEN  CONVERT(decimal (12,2),InstallmentTax) END,    
       @RELEASED_STATUS =   CASE WHEN ISNULL(InstallmentStatus,'')='1' THEN  'Y'    
         WHEN ISNULL(InstallmentStatus,'')='0' THEN  'N'    
         ELSE 'N' END --InstallmentStatus        
           
      FROM #TEMP_ACT_POLICY_INSTALLMENT_DETAILS WHERE ID = @ROW_ID        
            
   --GET CUSTOMER_ID AND POLIYC_ID        
   SELECT @CUSTOMER_ID=CUSTOMER_ID,@POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID        
    FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNo        
          
    SELECT @NEWROW_ID = ISNULL(MAX(ROW_ID),0)+1 FROM  ACT_POLICY_INSTALLMENT_DETAILS     
          
    SET @APP_ID =   @POLICY_ID      
     --SELECT * FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID =2187 AND POLICY_ID = 2        
           
            
	   IF(@CUSTOMER_ID > 0)        
	   BEGIN         
		  INSERT INTO [ACT_POLICY_INSTALLMENT_DETAILS]                 
		   (        
			[POLICY_ID]     ,[POLICY_VERSION_ID]    ,[CUSTOMER_ID]    ,[APP_ID]                  
			,[APP_VERSION_ID]    ,[INSTALLMENT_AMOUNT]    ,[INSTALLMENT_EFFECTIVE_DATE]    ,[RELEASED_STATUS]                  
			,[ROW_ID]    ,[INSTALLMENT_NO]    ,[RISK_ID]    ,[RISK_TYPE]    ,[PAYMENT_MODE]                  
			,[CURRENT_TERM]    ,[PERCENTAG_OF_PREMIUM]    ,[INTEREST_AMOUNT]    ,[FEE]                  
			,[TAXES]    ,[TOTAL]    ,[TRAN_INTEREST_AMOUNT]    ,[TRAN_FEE]    ,[TRAN_TAXES]    ,[TRAN_TOTAL]                  
			,[BOLETO_NO]    ,[IS_BOLETO_GENRATED]    ,[CREATED_BY]    ,[CREATED_DATETIME]    ,[MODIFIED_BY]                  
			,[LAST_UPDATED_DATETIME]    ,[TRAN_PREMIUM_AMOUNT]        
		  )                  
		   VALUES                  
		   (         
			@POLICY_ID    ,@POLICY_VERSION_ID    ,@CUSTOMER_ID     ,@APP_ID    ,@APP_VERSION_ID                  
		   ,@INSTALLMENT_AMOUNT    ,@INSTALLMENT_EFFECTIVE_DATE    ,@RELEASED_STATUS    ,@NEWROW_ID --@ROW_ID                  
		   ,@INSTALLMENT_NO    ,@RISK_ID    ,@RISK_TYPE    ,@PAYMENT_MODE    ,@CURRENT_TERM                  
		   ,@PERCENTAG_OF_PREMIUM    ,@INTEREST_AMOUNT    ,@FEE    ,@TAXES    ,@INSTALLMENT_AMOUNT+@INTEREST_AMOUNT+@FEE+@TAXES    
		   ,@TRAN_INTEREST_AMOUNT                   
		   ,@TRAN_FEE    ,@TRAN_TAXES    ,@TRAN_TOTAL   ,@BOLETO_NO    ,@IS_BOLETO_GENRATED    ,@CREATED_BY                  
		   ,@CREATED_DATETIME    ,@MODIFIED_BY  ,@LAST_UPDATED_DATETIME    ,@TRAN_PREMIUM_AMOUNT        
		   )          
		  IF @@ERROR<>0    
		  ROLLBACK TRAN         
	   END        
           
    SET @MAXCOUNT =@MAXCOUNT-1        
    SET @ROW_ID = @ROW_ID + 1        
   END --1 FOR POL_PRODUCT_COVERAGE        
      
   SET IDENTITY_INSERT [ACT_POLICY_INSTALLMENT_DETAILS] off 
                     
      
    DROP TABLE #TEMP_POL_APPLICANT_LISTS  
          
COMMIT TRAN
GO

