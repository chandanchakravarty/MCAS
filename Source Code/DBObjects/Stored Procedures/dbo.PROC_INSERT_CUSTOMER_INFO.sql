IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_CUSTOMER_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_CUSTOMER_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC PROC_INSERT_CUSTOMER_INFO    
CREATE PROC [dbo].[PROC_INSERT_CUSTOMER_INFO]    
AS    
BEGIN    
--------------------------------------------INSERT INTO CLT_CUSTOMER_LIST--------------------                
 DECLARE       
  @CUSTOMER_CODE nvarchar(20),                
  @CUSTOMER_TYPE int,                
  @CUSTOMER_PARENT int,                
  @CUSTOMER_SUFFIX nvarchar(10),                
  @CUSTOMER_FIRST_NAME nvarchar(200),                
  @CUSTOMER_MIDDLE_NAME nvarchar(200),                
  @CUSTOMER_LAST_NAME nvarchar(200),                
  @CUSTOMER_ADDRESS1 nvarchar(300),                
  @CUSTOMER_ADDRESS2 nvarchar(400),                
  @CUSTOMER_CITY varchar(70),                
  @CUSTOMER_COUNTRY nvarchar(20),                 
  @CUSTOMER_STATE nvarchar(40),                
  @CUSTOMER_ZIP nvarchar(40),                
  @CUSTOMER_BUSINESS_TYPE nvarchar(40),                
  @CUSTOMER_BUSINESS_DESC nvarchar(2000),                
  @CUSTOMER_CONTACT_NAME nvarchar(70),                
  @CUSTOMER_BUSINESS_PHONE nvarchar(30),                
  @CUSTOMER_EXT nvarchar(12),                
  @CUSTOMER_HOME_PHONE nvarchar(30),                
  @CUSTOMER_MOBILE nvarchar(30),                
  @CUSTOMER_FAX nvarchar(30),                
  @CUSTOMER_PAGER_NO nvarchar(30),                
  @CUSTOMER_Email nvarchar(100),                
  @CUSTOMER_WEBSITE nvarchar(300),                
  @CUSTOMER_INSURANCE_SCORE int,                
  @CUSTOMER_INSURANCE_RECEIVED_DATE datetime,                
  @CUSTOMER_REASON_CODE nvarchar(20),                
  @CUSTOMER_LATE_CHARGES nchar,                
  @CUSTOMER_LATE_NOTICES nchar,                
  @CUSTOMER_SEND_STATEMENT nchar,                
  @CUSTOMER_RECEIVABLE_DUE_DAYS int,                
  @CUSTOMER_AGENCY_ID smallint,                
  @IS_ACTIVE nchar,                
  @CREATED_BY int,                
  @CREATED_DATETIME datetime,                
  @MODIFIED_BY int,                
  @LAST_UPDATED_DATETIME datetime,                
  @CUSTOMER_ATTENTION_NOTE nvarchar(1000),                
  @PREFIX int,                
  @CUSTOMER_REASON_CODE2 nvarchar(20),                
  @CUSTOMER_REASON_CODE3 nvarchar(20),                
  @CUSTOMER_REASON_CODE4 nvarchar(20),                
  @ATTENTION_NOTE_UPDATED datetime,                
  @IS_HOME_EMPLOYEE nvarchar(4000),                
  @LAST_INSURANCE_SCORE_FETCHED datetime,                
  @APPLICANT_OCCU int,                
  @EMPLOYER_NAME nvarchar(150),                
  @EMPLOYER_ADDRESS nvarchar(150),                
  @YEARS_WITH_CURR_EMPL real,                
  @SSN_NO nvarchar(88),                
  @MARITAL_STATUS nchar,                
  @DATE_OF_BIRTH datetime,                
  @DESC_APPLICANT_OCCU nvarchar(400),                
  @LAST_MVR_SCORE_FETCHED datetime,                
  @EMPLOYER_ADD1 nvarchar(300),                
  @EMPLOYER_ADD2 nvarchar(300),                
  @EMPLOYER_CITY nvarchar(140),                
  @EMPLOYER_COUNTRY nvarchar(20),                
  @EMPLOYER_STATE nvarchar(20),                
  @EMPLOYER_ZIPCODE nvarchar(22),                
  @EMPLOYER_HOMEPHONE nvarchar(30),                
  @EMPLOYER_EMAIL nvarchar(100),                
  @YEARS_WITH_CURR_OCCU real,                
  @GENDER nchar,                
  @PER_CUST_MOBILE nvarchar(30),                
  @EMP_EXT nvarchar(12),                
  @PRIORINFO_ORDERED datetime,                
  @CPF_CNPJ nvarchar(40),                
  @NUMBER nvarchar(40),                
  @COMPLIMENT nvarchar(40),       
  @DISTRICT nvarchar(40),                
  @BROKER int,                
  @MAIN_TITLE int,                
  @MAIN_POSITION int,                
  @MAIN_CPF_CNPJ nvarchar(40),                
  @MAIN_ADDRESS nvarchar(40),                
  @MAIN_NUMBER nvarchar(40),                
  @MAIN_COMPLIMENT nvarchar(40),                
  @MAIN_DISTRICT nvarchar(40),                
  @MAIN_NOTE nvarchar(500),                
  @MAIN_CONTACT_CODE nvarchar(40),                
  @REGIONAL_IDENTIFICATION nvarchar(40),                
  @REG_ID_ISSUE datetime,                
  @ORIGINAL_ISSUE nvarchar(40),                
  @MAIN_CITY nvarchar(140),                
  @MAIN_STATE int,                
  @MAIN_COUNTRY int,                
  @MAIN_ZIPCODE nvarchar(40),                
  @MAIN_FIRST_NAME nvarchar(200),                
  @MAIN_MIDDLE_NAME nvarchar(200),                
  @MAIN_LAST_NAME nvarchar(200)    
      
  SET  @CUSTOMER_CODE ='SanGarg43'                
  SET @CUSTOMER_TYPE= 11110                
  SET @CUSTOMER_PARENT= NULL                
  SET  @CUSTOMER_SUFFIX = NULL                
  SET @CUSTOMER_FIRST_NAME = 'Sanjay'                
  SET @CUSTOMER_MIDDLE_NAME = 'Kumar'                
  SET @CUSTOMER_LAST_NAME = 'Garg'                
  SET @CUSTOMER_ADDRESS1 ='RUA ARNALDO AUGUSTO DE SA'                
  SET @CUSTOMER_ADDRESS2 =''                
  SET @CUSTOMER_CITY ='SAO PAULO'                
  SET @CUSTOMER_COUNTRY ='5'                
  SET @CUSTOMER_STATE ='84'                
  SET @CUSTOMER_ZIP ='4323160'                
  SET @CUSTOMER_BUSINESS_TYPE =''                
  SET @CUSTOMER_BUSINESS_DESC= ''                
  SET @CUSTOMER_CONTACT_NAME = ''                
  SET @CUSTOMER_BUSINESS_PHONE= ''                
  SET @CUSTOMER_EXT = ''                
  SET @CUSTOMER_HOME_PHONE = ''                
  SET @CUSTOMER_MOBILE = ''                
  SET @CUSTOMER_FAX = ''                
  SET @CUSTOMER_PAGER_NO =NULL                
  SET @CUSTOMER_Email = ''                
  SET @CUSTOMER_WEBSITE = ''                
  SET @CUSTOMER_INSURANCE_SCORE = -1                
  SET @CUSTOMER_INSURANCE_RECEIVED_DATE = NULL                
  SET @CUSTOMER_REASON_CODE = NULL                
  SET @CUSTOMER_LATE_CHARGES = NULL                
  SET @CUSTOMER_LATE_NOTICES = NULL                
  SET @CUSTOMER_SEND_STATEMENT = NULL                
  SET @CUSTOMER_RECEIVABLE_DUE_DAYS = NULL                
  SET @CUSTOMER_AGENCY_ID =83                
  SET @IS_ACTIVE ='Y'                
  SET @CREATED_BY =198                
  SET @CREATED_DATETIME ='2010-10-21 13:01:32.390'                
  SET @MODIFIED_BY = NULL                
  SET @LAST_UPDATED_DATETIME = NULL                
  SET @CUSTOMER_ATTENTION_NOTE = NULL                
  SET @PREFIX =0                
  SET @CUSTOMER_REASON_CODE2 = NULL                
  SET @CUSTOMER_REASON_CODE3 = NULL                
  SET @CUSTOMER_REASON_CODE4 = NULL                
  SET @ATTENTION_NOTE_UPDATED = NULL                
  SET @IS_HOME_EMPLOYEE = NULL                
  SET @LAST_INSURANCE_SCORE_FETCHED = NULL                
  SET @APPLICANT_OCCU = NULL                
  SET @EMPLOYER_NAME = NULL                
  SET @EMPLOYER_ADDRESS = NULL                
  SET @YEARS_WITH_CURR_EMPL = NULL                
  SET @SSN_NO = NULL                
  SET @MARITAL_STATUS ='M'                
  SET @DATE_OF_BIRTH ='1981-05-09 00:00:00.000'                
  SET @DESC_APPLICANT_OCCU = NULL                
  SET @LAST_MVR_SCORE_FETCHED = NULL                
  SET @EMPLOYER_ADD1 = NULL                
  SET @EMPLOYER_ADD2 = NULL                
  SET @EMPLOYER_CITY = NULL                
  SET @EMPLOYER_COUNTRY = NULL                
  SET @EMPLOYER_STATE = NULL                
  SET @EMPLOYER_ZIPCODE = NULL                
  SET @EMPLOYER_HOMEPHONE = NULL                
  SET @EMPLOYER_EMAIL = NULL                
  SET @YEARS_WITH_CURR_OCCU = NULL                
  SET @GENDER ='M'                
  SET @PER_CUST_MOBILE = NULL                
  SET @EMP_EXT = NULL           
  SET @PRIORINFO_ORDERED = NULL                
  SET @CPF_CNPJ ='747.227.464-00'   
  --HARD CODED               
  SET @NUMBER ='123456789'                
  SET @COMPLIMENT = NULL                
  SET @DISTRICT =''                
  SET @BROKER = NULL                
  SET @MAIN_TITLE =0                
  SET @MAIN_POSITION =246                
  SET @MAIN_CPF_CNPJ ='747.227.464-00'                
  SET @MAIN_ADDRESS =''                
  SET @MAIN_NUMBER =''                
  SET @MAIN_COMPLIMENT =''                
  SET @MAIN_DISTRICT  =''                
  SET @MAIN_NOTE  =''                
  SET @MAIN_CONTACT_CODE ='Ankgoel40'                
  SET @REGIONAL_IDENTIFICATION ='Regional Ide'                
  SET @REG_ID_ISSUE ='2010-10-21 00:00:00.000'                
  SET @ORIGINAL_ISSUE ='10/21/2010'                
  SET @MAIN_CITY =''                
  SET @MAIN_STATE =88                
  SET @MAIN_COUNTRY =5                
  SET @MAIN_ZIPCODE =''                
  SET @MAIN_FIRST_NAME =''                
  SET @MAIN_MIDDLE_NAME =''                
  SET @MAIN_LAST_NAME =''                  
    
 CREATE TABLE #TEMP_CUSTOMER_LIST    
 (    
 ID INT IDENTITY(1,1),    
 CustomerType INT,    
 InsuredName VARCHAR(MAX),    
 [Address] VARCHAR(MAX),    
 Number nvarchar(40),    
 Complement  nvarchar(40),    
 District  nvarchar(40),    
 City  nvarchar(140),    
 [State]  nvarchar(40),    
 PostalCode VARCHAR(20),    
 Country VARCHAR(MAX),    
 CPF_CNPJ VARCHAR(MAX)    
 )    
    
 --DROP TABLE #TEMP_CUSTOMER_LIST    
 INSERT INTO #TEMP_CUSTOMER_LIST    
 (CustomerType,InsuredName ,[Address] ,Number ,Complement ,District ,City,    
 [State] ,PostalCode,Country ,CPF_CNPJ)     
    
 SELECT CustomerType,FIRST_NAME ,[Address] ,Number ,Complement ,    
  District ,City,[State] ,PostalCode,Country,[Individual/Company Id]     
  FROM [Importnew_Policy_Info]    
      
  DECLARE @COUNT INT,@COUNTER INT    
  SELECT  @COUNT =COUNT(*) FROM #TEMP_CUSTOMER_LIST    
  SET @COUNTER=1    
      
  ----RUNNING LOOP FOR INSERTING RECORDS-------------    
  WHILE @COUNT > 0    
  BEGIN     
  --CONDITION FOR CHECKING CUSTOMER ALREADY EXIST OR NOT---     
      
      
  SELECT @CUSTOMER_TYPE=CustomerType,    
  @CUSTOMER_FIRST_NAME= InsuredName,    
  @CUSTOMER_ADDRESS1=[Address],    
  --@NUMBER =Number,     
  @COMPLIMENT = Complement,    
  @DISTRICT =District ,    
  @CUSTOMER_CITY= CITY,    
  @CUSTOMER_STATE = (SELECT  STATE_ID FROM MNT_COUNTRY_STATE_LIST WHERE   
   COUNTRY_ID = CONVERT(INT,ISNULL(Country,0)) AND STATE_CODE =[State]),     
  @CUSTOMER_ZIP =  PostalCode, --,    
  @CUSTOMER_COUNTRY = Country, --     
  @CPF_CNPJ =CPF_CNPJ     
  FROM #TEMP_CUSTOMER_LIST      
  WHERE ID=@COUNTER    
      
      
  IF NOT EXISTS( SELECT CUSTOMER_ID FROM CLT_CUSTOMER_LIST WHERE CPF_CNPJ = @CPF_CNPJ)    
  INSERT INTO [CLT_CUSTOMER_LIST]                
      ([CUSTOMER_CODE]                
      ,[CUSTOMER_TYPE]                
      ,[CUSTOMER_PARENT]                
      ,[CUSTOMER_SUFFIX]                
      ,[CUSTOMER_FIRST_NAME]                
      ,[CUSTOMER_MIDDLE_NAME]                
   ,[CUSTOMER_LAST_NAME]                
      ,[CUSTOMER_ADDRESS1]                
      ,[CUSTOMER_ADDRESS2]                
      ,[CUSTOMER_CITY]                
      ,[CUSTOMER_COUNTRY]                
      ,[CUSTOMER_STATE]                
      ,[CUSTOMER_ZIP]                
      ,[CUSTOMER_BUSINESS_TYPE]                
      ,[CUSTOMER_BUSINESS_DESC]                
      ,[CUSTOMER_CONTACT_NAME]                
      ,[CUSTOMER_BUSINESS_PHONE]                
      ,[CUSTOMER_EXT]                
      ,[CUSTOMER_HOME_PHONE]                
      ,[CUSTOMER_MOBILE]                
      ,[CUSTOMER_FAX]                
      ,[CUSTOMER_PAGER_NO]                
      ,[CUSTOMER_Email]                
      ,[CUSTOMER_WEBSITE]                
      ,[CUSTOMER_INSURANCE_SCORE]                
      ,[CUSTOMER_INSURANCE_RECEIVED_DATE]                
      ,[CUSTOMER_REASON_CODE]                
      ,[CUSTOMER_LATE_CHARGES]                
      ,[CUSTOMER_LATE_NOTICES]                
      ,[CUSTOMER_SEND_STATEMENT]                
      ,[CUSTOMER_RECEIVABLE_DUE_DAYS]                
      ,[CUSTOMER_AGENCY_ID]                
      ,[IS_ACTIVE]                
      ,[CREATED_BY]                
      ,[CREATED_DATETIME]                
      ,[MODIFIED_BY]                
      ,[LAST_UPDATED_DATETIME]                
      ,[CUSTOMER_ATTENTION_NOTE]                
      ,[PREFIX]                
      ,[CUSTOMER_REASON_CODE2]                
  ,[CUSTOMER_REASON_CODE3]                
      ,[CUSTOMER_REASON_CODE4]                
      ,[ATTENTION_NOTE_UPDATED]                
      ,[IS_HOME_EMPLOYEE]                
      ,[LAST_INSURANCE_SCORE_FETCHED]                
      ,[APPLICANT_OCCU]                
      ,[EMPLOYER_NAME]                
      ,[EMPLOYER_ADDRESS]                
      ,[YEARS_WITH_CURR_EMPL]                
      ,[SSN_NO]                
      ,[MARITAL_STATUS]                
      ,[DATE_OF_BIRTH]                
      ,[DESC_APPLICANT_OCCU]                
      ,[LAST_MVR_SCORE_FETCHED]                
      ,[EMPLOYER_ADD1]                
      ,[EMPLOYER_ADD2]                
      ,[EMPLOYER_CITY]                
      ,[EMPLOYER_COUNTRY]                
      ,[EMPLOYER_STATE]                
      ,[EMPLOYER_ZIPCODE]                
      ,[EMPLOYER_HOMEPHONE]                
      ,[EMPLOYER_EMAIL]                
      ,[YEARS_WITH_CURR_OCCU]                
      ,[GENDER]                
      ,[PER_CUST_MOBILE]                
      ,[EMP_EXT]                
      ,[PRIORINFO_ORDERED]                
      ,[CPF_CNPJ]                
      ,[NUMBER]                
      ,[COMPLIMENT]                
      ,[DISTRICT]                
      ,[BROKER]                
      ,[MAIN_TITLE]                
      ,[MAIN_POSITION]                
      ,[MAIN_CPF_CNPJ]                
      ,[MAIN_ADDRESS]                
      ,[MAIN_NUMBER]                
      ,[MAIN_COMPLIMENT]                
      ,[MAIN_DISTRICT]                
      ,[MAIN_NOTE]                
      ,[MAIN_CONTACT_CODE]                
      ,[REGIONAL_IDENTIFICATION]                
      ,[REG_ID_ISSUE]                
      ,[ORIGINAL_ISSUE]                
      ,[MAIN_CITY]                
      ,[MAIN_STATE]                
      ,[MAIN_COUNTRY]                
      ,[MAIN_ZIPCODE]                
      ,[MAIN_FIRST_NAME]                
      ,[MAIN_MIDDLE_NAME]                
      ,[MAIN_LAST_NAME])                
   VALUES                
      -- @CUSTOMER_ID,                
    (@CUSTOMER_CODE,                
    @CUSTOMER_TYPE,                
    @CUSTOMER_PARENT,                
    @CUSTOMER_SUFFIX,                
    @CUSTOMER_FIRST_NAME,               
    @CUSTOMER_MIDDLE_NAME,                
    @CUSTOMER_LAST_NAME,                
    @CUSTOMER_ADDRESS1,                
    @CUSTOMER_ADDRESS2,                
    @CUSTOMER_CITY,                
    @CUSTOMER_COUNTRY,                
    @CUSTOMER_STATE,                
    @CUSTOMER_ZIP,                
    @CUSTOMER_BUSINESS_TYPE,                
    @CUSTOMER_BUSINESS_DESC,                
    @CUSTOMER_CONTACT_NAME,                
    @CUSTOMER_BUSINESS_PHONE,                
    @CUSTOMER_EXT,                
    @CUSTOMER_HOME_PHONE,                
    @CUSTOMER_MOBILE,                
    @CUSTOMER_FAX,                
    @CUSTOMER_PAGER_NO,                
    @CUSTOMER_Email,                
    @CUSTOMER_WEBSITE,                
    @CUSTOMER_INSURANCE_SCORE,                
    @CUSTOMER_INSURANCE_RECEIVED_DATE,                
    @CUSTOMER_REASON_CODE,                
    @CUSTOMER_LATE_CHARGES,                
    @CUSTOMER_LATE_NOTICES,                
    @CUSTOMER_SEND_STATEMENT,                
    @CUSTOMER_RECEIVABLE_DUE_DAYS,                
    @CUSTOMER_AGENCY_ID,                
    @IS_ACTIVE,                
    @CREATED_BY,                
    @CREATED_DATETIME,                
    @MODIFIED_BY,                
    @LAST_UPDATED_DATETIME,                
    @CUSTOMER_ATTENTION_NOTE,                
    @PREFIX,                
    @CUSTOMER_REASON_CODE2,                
    @CUSTOMER_REASON_CODE3,                
    @CUSTOMER_REASON_CODE4,                
    @ATTENTION_NOTE_UPDATED,                
    @IS_HOME_EMPLOYEE,                
    @LAST_INSURANCE_SCORE_FETCHED,                
    @APPLICANT_OCCU,                
    @EMPLOYER_NAME,                
    @EMPLOYER_ADDRESS,                
    @YEARS_WITH_CURR_EMPL,                
  @SSN_NO,                
    @MARITAL_STATUS,                
    @DATE_OF_BIRTH,                
    @DESC_APPLICANT_OCCU,                
    @LAST_MVR_SCORE_FETCHED,                
    @EMPLOYER_ADD1,                
    @EMPLOYER_ADD2,                
    @EMPLOYER_CITY,                
    @EMPLOYER_COUNTRY,                
    @EMPLOYER_STATE,                
    @EMPLOYER_ZIPCODE,                
    @EMPLOYER_HOMEPHONE,                
    @EMPLOYER_EMAIL ,                
    @YEARS_WITH_CURR_OCCU ,                
    @GENDER ,                
    @PER_CUST_MOBILE ,                
    @EMP_EXT,                
    @PRIORINFO_ORDERED,                
    @CPF_CNPJ,                
    @NUMBER ,                
    @COMPLIMENT ,                
    @DISTRICT,                
    @BROKER ,                
    @MAIN_TITLE,                
    @MAIN_POSITION,                
    @MAIN_CPF_CNPJ,                
    @MAIN_ADDRESS,                
    @MAIN_NUMBER,                
    @MAIN_COMPLIMENT,                
    @MAIN_DISTRICT ,                
    @MAIN_NOTE ,                
    @MAIN_CONTACT_CODE,                
    @REGIONAL_IDENTIFICATION,                
    @REG_ID_ISSUE,                
    @ORIGINAL_ISSUE,                
    @MAIN_CITY ,                
    @MAIN_STATE ,                
    @MAIN_COUNTRY ,                
    @MAIN_ZIPCODE ,                
    @MAIN_FIRST_NAME,                
    @MAIN_MIDDLE_NAME,                
    @MAIN_LAST_NAME )      
        
        
    DECLARE         
 @APPLICANT_ID int,    
 @CUSTOMER_ID int    
     
     
  SELECT @CUSTOMER_ID =  SCOPE_IDENTITY() --isnull(Max(@CUSTOMER_ID),0) FROM CLT_CUSTOMER_LIST     
     
    SELECT @APPLICANT_ID = isnull(Max(APPLICANT_ID),0)+1 FROM CLT_APPLICANT_LIST     
        
    PRINT @CUSTOMER_ID    
    PRINT @APPLICANT_ID    
    IF(@CUSTOMER_ID > 0)    
   BEGIN    
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
   FAX           
   )                                                      
  VALUES                                                       
   (                                                            
   @CUSTOMER_TYPE,        
   @APPLICANT_ID,        
   @CUSTOMER_ID,        
   @PREFIX,          
   @CUSTOMER_SUFFIX,        
   @CUSTOMER_FIRST_NAME,        
   null,        
   null,          
   @CUSTOMER_ADDRESS1,        
   @CUSTOMER_ADDRESS2,        
   @CUSTOMER_CITY,        
   @CUSTOMER_COUNTRY,        
   @CUSTOMER_STATE,          
   @CUSTOMER_ZIP,        
   @CUSTOMER_HOME_PHONE,        
   @CUSTOMER_Email,        
   null,        
   null,        
   null,        
   'Y',        
   @CREATED_BY,          
   @CREATED_DATETIME,        
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
   @CUSTOMER_MOBILE,        
   @EMP_EXT,        
   @CUSTOMER_EXT,        
   @GENDER,        
   @NUMBER,        
   @MAIN_POSITION,        
   @DISTRICT,          
   @REG_ID_ISSUE,        
   @ORIGINAL_ISSUE,        
   @REGIONAL_IDENTIFICATION,        
   @MAIN_NOTE,        
   @CUSTOMER_CODE ,          
   @CPF_CNPJ,        
   @CUSTOMER_BUSINESS_PHONE,        
   @CUSTOMER_FAX          
    )     
           
    END    
    SET @COUNTER = @COUNTER +1    
  SET @COUNT =@COUNT-1    
 END -- End of clt_customer_list    
        
        
        
        
     
      
END    
      
      
      
      
GO

