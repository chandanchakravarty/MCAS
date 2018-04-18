IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

                  
                                   
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                                       
/*----------------------------------------------------------                                                          
Proc Name    : dbo.Proc_InsertCustomer                                                          
Created by   : VIJAY                                                          
Date         : 24 Mar.,2005                                                         
Purpose     : Insert the record into Customers Table                                                        
                                        
Used In  :     Ebix Advantage                                                         
Modified By  : Mohit Gupta.                                                    
Modified On  : 6/10/2005                                                    
Purpose      : Check if user is of type personal then only add it in CLT_APPLICANT_LIST.                                                  
                                                
Modified By  : Mohit Gupta.                                                    
Modified On  : 4/11/2005                                                    
Purpose      : Adding field "DESC_APPLICANT_OCCU" in insert statement.                                                
                                              
Modified By  : Sumit Chhabra                                              
Modified On  : 11/11/2005                                                    
Purpose      : Duplicacy of Customer Code has been done away with                                              
                                              
                                                         
 ------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                    
                                                           
------   ------------       -------------------------*/                                                         
--drop proc dbo.Proc_InsertCustomerGenInfo                                                                  
CREATE   PROC [dbo].[Proc_InsertCustomerGenInfo]                                                      
(                                                      
 @CUSTOMER_ID     int  OUTPUT,                                                      
 @CUSTOMER_CODE    nvarchar(10) ,                                                      
 @CUSTOMER_TYPE    int  ,                                                      
 @CUSTOMER_PARENT   int   ,                                                      
 @CUSTOMER_SUFFIX    nvarchar(5)  ,                                                      
 @CUSTOMER_FIRST_NAME   nvarchar(200)  ,                                                      
 @CUSTOMER_MIDDLE_NAME   nvarchar(10)  ,                                                      
 @CUSTOMER_LAST_NAME   nvarchar(25)  ,                                                      
 @CUSTOMER_ADDRESS1   nvarchar(150)  ,                                                      
 @CUSTOMER_ADDRESS2   nvarchar(150)  ,                                                      
 @CUSTOMER_CITY    nvarchar(70)  ,                                                      
 @CUSTOMER_COUNTRY   nvarchar(35)  ,                                                        
 @CUSTOMER_STATE    nvarchar(20)  ,                                                      
 @CUSTOMER_ZIP    nvarchar(20)  ,                                                      
 @CUSTOMER_BUSINESS_TYPE   nvarchar(20)  ,                                                      
 @CUSTOMER_BUSINESS_DESC   nvarchar(1000)  ,                   
 @CUSTOMER_CONTACT_NAME   nvarchar(35)  ,                      
 @CUSTOMER_BUSINESS_PHONE  nvarchar(15)  ,                                                      
 @CUSTOMER_EXT    nvarchar(6)  ,                                        
 @EMP_EXT    nvarchar(6)  ,                                    
 @CUSTOMER_HOME_PHONE   nvarchar(15)  ,                     
 @CUSTOMER_MOBILE   nvarchar(15)  ,                                                      
 @CUSTOMER_FAX    nvarchar(15)  ,                                                      
 @CUSTOMER_PAGER_NO   nvarchar(15)  ,                                              
 @CUSTOMER_Email    nvarchar(50)  ,                                                      
 @CUSTOMER_WEBSITE   nvarchar(150)  ,                                                      
 @CUSTOMER_INSURANCE_SCORE  int  ,                                                 
 @CUSTOMER_INSURANCE_RECEIVED_DATE datetime  ,                                                      
 @CUSTOMER_REASON_CODE   nvarchar(10)  ,                                                      
 @CUSTOMER_REASON_CODE2   nvarchar(10) ,                                                   
 @CUSTOMER_REASON_CODE3   nvarchar(10)  ,                                                      
 @CUSTOMER_REASON_CODE4   nvarchar(10) ,                                                      
                                                      
                                                       
 --@CustomerProducerId   NVARCHAR(30),                                                        
 --@CustomerLateCharges   NVARCHAR(30),                                                        
 --@CustomerLateNotices   NVARCHAR(1),                                                        
 --@CustomerAccountExecutiveId  NVARCHAR(30),                                                        
 --@CustomerSendStatement  NVARCHAR(1),                                                        
 --@CustomerReceivableDueDays  NVARCHAR(4),                                                        
 --@CustomerCsr    NVARCHAR(30),                                                        
 --@CustomerReferredBy   NVARCHAR(25),                                                        
                                                       
                                                       
 @PREFIX     int,                                        
 @PER_CUST_MOBILE   nvarchar(15),                                                      
 @CREATED_BY    int  ,                                                      
 @CREATED_DATETIME   datetime ,                                                      
 @Cust_Id     int   OUTPUT,                                                      
 @CUSTOMER_AGENCY_ID   int,                                                      
 @LAST_INSURANCE_SCORE_FETCHED   datetime,                                                    
@APPLICANT_OCCU INT =NULL,                                                    
@EMPLOYER_NAME nvarchar(75) =null,                                                    
@EMPLOYER_ADDRESS nvarchar(75)=null,                                                    
@YEARS_WITH_CURR_EMPL real =null,                                                    
@SSN_NO nvarchar(44) =null,                                            
@MARITAL_STATUS nchar(1) =null,                                                    
@DATE_OF_BIRTH datetime =null,                                                    
@Co_Appl_Marital_Status nchar(1)=null,                                                    
@Co_Appl_SSN_No nvarchar(44)=null,                                                    
@Co_Appl_DOB datetime=null,                                                
@DESC_APPLICANT_OCCU varchar(200),                                            
@EMPLOYER_ADD1 nvarchar(150),                                            
@EMPLOYER_ADD2 nvarchar(150),                                            
@EMPLOYER_CITY varchar(70),                            
@EMPLOYER_COUNTRY  nvarchar(10),                                            
@EMPLOYER_STATE  nvarchar(10),                  
@EMPLOYER_ZIPCODE  varchar(11),                                            
@EMPLOYER_HOMEPHONE  nvarchar(15),                                    
@EMPLOYER_EMAIL  nvarchar(50),                                            
@YEARS_WITH_CURR_OCCU real,                                        
@GENDER nchar(1),                          
@CPF_CNPJ nvarchar(20),                          
@NUMBER nvarchar(20),                          
                        
@DISTRICT nvarchar(20),                          
                        
@MAIN_TITLE nvarchar(20),                  
@MAIN_POSITION nvarchar(20),                          
@MAIN_CPF_CNPJ nvarchar(20),                          
@MAIN_ADDRESS nvarchar(20),                          
@MAIN_NUMBER nvarchar(20),                          
@MAIN_COMPLIMENT nvarchar(20),                          
@MAIN_DISTRICT nvarchar(20),                          
@MAIN_NOTE  nvarchar(250),                        
@MAIN_CONTACT_CODE nvarchar(20),                      
@REGIONAL_IDENTIFICATION nvarchar(20),                      
@REG_ID_ISSUE datetime,                      
@ORIGINAL_ISSUE nvarchar(20),                    
@MAIN_ZIPCODE nvarchar(20),                    
@MAIN_CITY  nvarchar(70),                    
@MAIN_COUNTRY int,                    
@MAIN_STATE int,                  
@MAIN_FIRST_NAME nvarchar(20),                  
@MAIN_MIDDLE_NAME nvarchar(20),                  
@MAIN_LAST_NAME nvarchar(20),
@ID_TYPE NVARCHAR(100),
@MONTHLY_INCOME DECIMAL(10,2),
@AMOUNT_TYPE INT,
@CADEMP NVARCHAR(100),
@NET_ASSETS_AMOUNT DECIMAL(10,2),
@NATIONALITY NVARCHAR(100),
@EMAIL_ADDRESS NVARCHAR(100),   
@REGIONAL_IDENTIFICATION_TYPE NVARCHAR(40),
@IS_POLITICALLY_EXPOSED CHAR(2),
@BANK_NAME NVARCHAR(100),
@BANK_NUMBER NVARCHAR(40),
@BANK_BRANCH NVARCHAR(40),
@ACCOUNT_NUMBER NVARCHAR(40),
@ACCOUNT_TYPE INT  

                     
                    
                      
                    
                     
/*@ALT_CUSTOMER_ADDRESS1   nvarchar(150)  ,                                                      
 @ALT_CUSTOMER_ADDRESS2   nvarchar(150)  ,                        
 @ALT_CUSTOMER_CITY   nvarchar(70)  ,                                                      
 @ALT_CUSTOMER_COUNTRY   nvarchar(10)  ,                                                        
 @ALT_CUSTOMER_STATE    nvarchar(10)  ,                                                      
 @ALT_CUSTOMER_ZIP    nvarchar(11)  */                                                   
)                                                      
AS                                                
BEGIN                                                      
/*Nov 11,2005:Sumit Chhabra:Duplicacy of Customer Code has been done away with */                                              
 if Exists(SELECT CUSTOMER_CODE FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_CODE = LTRIM(RTRIM(@CUSTOMER_CODE)))                                                      
 BEGIN                                                      
  SELECT @CUSTOMER_ID = -1                                                      
 END                                                      
 ELSE                                                      
		 BEGIN                                                     
			INSERT INTO CLT_CUSTOMER_LIST                                                      
			(                                                      
			  CUSTOMER_TYPE,                           
			  CUSTOMER_CODE,                                                      
			  CUSTOMER_PARENT,                                                      
			  CUSTOMER_SUFFIX,                                                    
			  CUSTOMER_FIRST_NAME,                                                      
			  CUSTOMER_MIDDLE_NAME,                                                      
			  CUSTOMER_LAST_NAME,                                                      
			  CUSTOMER_ADDRESS1,                                                      
			  CUSTOMER_ADDRESS2,                                                      
			  CUSTOMER_CITY,                                                      
			  CUSTOMER_COUNTRY,                                                      
			  CUSTOMER_STATE,          
			  CUSTOMER_ZIP,                                                      
			  CUSTOMER_BUSINESS_TYPE,                                                      
			  CUSTOMER_BUSINESS_DESC,                                                      
			  CUSTOMER_CONTACT_NAME,                                             
			  CUSTOMER_BUSINESS_PHONE,                                                      
			  CUSTOMER_EXT,                                        
			  EMP_EXT,                                                      
			  CUSTOMER_HOME_PHONE,                                                 
			 CUSTOMER_MOBILE,                                                      
			  CUSTOMER_FAX,                                                      
			  CUSTOMER_PAGER_NO,                                                      
			  CUSTOMER_Email,                                                      
			  CUSTOMER_WEBSITE,                                         
			  CUSTOMER_INSURANCE_SCORE,                                                      
			  CUSTOMER_INSURANCE_RECEIVED_DATE,                                                      
			  CUSTOMER_REASON_CODE,                                                      
			  CUSTOMER_REASON_CODE2,              
			  CUSTOMER_REASON_CODE3,                                                      
			  CUSTOMER_REASON_CODE4,                                               
			  CUSTOMER_AGENCY_ID,                                                      
			  PREFIX,                                     
			  PER_CUST_MOBILE,                                                      
			  CREATED_BY,                                         
			  CREATED_DATETIME,                                                      
			  IS_ACTIVE,                                                      
			  LAST_INSURANCE_SCORE_FETCHED,                                                    
			  APPLICANT_OCCU,                                                    
			  EMPLOYER_NAME,                                                    
			  EMPLOYER_ADDRESS ,                 
			  YEARS_WITH_CURR_EMPL,                                                    
			  SSN_NO ,                                        
			  MARITAL_STATUS ,                                                    
			  DATE_OF_BIRTH,                                                
			  DESC_APPLICANT_OCCU,                                            
			  EMPLOYER_ADD1,                                            
			  EMPLOYER_ADD2,                                            
			  EMPLOYER_CITY,                                        
			  EMPLOYER_COUNTRY,                                            
			  EMPLOYER_STATE,                                            
			  EMPLOYER_ZIPCODE,                                        
			  EMPLOYER_HOMEPHONE,                                            
			  EMPLOYER_EMAIL,                                            
			  YEARS_WITH_CURR_OCCU,                                        
			  GENDER,                          
			  CPF_CNPJ,                          
			  NUMBER,                  
			  DISTRICT,                     
			  MAIN_TITLE,                           
			  MAIN_POSITION,                           
			  MAIN_CPF_CNPJ,                           
			  MAIN_ADDRESS,                           
			  MAIN_NUMBER,                           
			  MAIN_COMPLIMENT,                           
			  MAIN_DISTRICT,                           
			  MAIN_NOTE,                        
			  MAIN_CONTACT_CODE,                      
			  REGIONAL_IDENTIFICATION,                      
			  REG_ID_ISSUE,                      
			  ORIGINAL_ISSUE,                    
			  MAIN_ZIPCODE ,                    
			  MAIN_CITY ,                    
			  MAIN_COUNTRY ,                    
			  MAIN_STATE,                    
			  MAIN_FIRST_NAME,                  
			  MAIN_MIDDLE_NAME,                  
			  MAIN_LAST_NAME,
			  ID_TYPE,
			  MONTHLY_INCOME,
			  AMOUNT_TYPE,
			  CADEMP,                  
			  NET_ASSETS_AMOUNT,
			  NATIONALITY,
			  EMAIL_ADDRESS ,
			  REGIONAL_IDENTIFICATION_TYPE,
			  IS_POLITICALLY_EXPOSED
			                                      
			  --ALT_CUSTOMER_ADDRESS1,                                                    
			  --ALT_CUSTOMER_ADDRESS2,                                                      
			 -- ALT_CUSTOMER_CITY,                  
			  --ALT_CUSTOMER_COUNTRY,                                                      
			  --ALT_CUSTOMER_STATE,                                                      
			 -- ALT_CUSTOMER_ZIP                                          
			)                                                      
			VALUES                                            
			(                                                       
			  @CUSTOMER_TYPE,                                                      
			  @CUSTOMER_CODE,                                                      
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
			  @EMP_EXT,                                                      
			  @CUSTOMER_HOME_PHONE,    
			  @CUSTOMER_MOBILE,                                                      
			  @CUSTOMER_FAX,                                                      
			  @CUSTOMER_PAGER_NO,                                                      
			  @CUSTOMER_Email,                                                      
			  @CUSTOMER_WEBSITE,                                                      
			  @CUSTOMER_INSURANCE_SCORE,                                                      
			  @CUSTOMER_INSURANCE_RECEIVED_DATE,                                                      
			@CUSTOMER_REASON_CODE,                                                      
			  @CUSTOMER_REASON_CODE2,                                                      
			  @CUSTOMER_REASON_CODE3,                                                      
			  @CUSTOMER_REASON_CODE4,                                            
			  @CUSTOMER_AGENCY_ID,                                                      
			  @PREFIX,                                        
			  @PER_CUST_MOBILE,                                                       
			  @CREATED_BY,                                                      
			  @CREATED_DATETIME,                                        
			  'Y',                                                      
			  @CUSTOMER_INSURANCE_RECEIVED_DATE ,    
			  @APPLICANT_OCCU,                                                    
			  @EMPLOYER_NAME,                                                    
			  @EMPLOYER_ADDRESS,                                                    
			  @YEARS_WITH_CURR_EMPL,                                                    
			  @SSN_NO ,                                                      
			  @MARITAL_STATUS,                                                    
			  @DATE_OF_BIRTH,                                                
			  @DESC_APPLICANT_OCCU,                                            
			  @EMPLOYER_ADD1,                                            
			  @EMPLOYER_ADD2,                                            
			  @EMPLOYER_CITY,                                            
			  @EMPLOYER_COUNTRY,                                            
			  @EMPLOYER_STATE,                                            
			  @EMPLOYER_ZIPCODE,                                            
			  @EMPLOYER_HOMEPHONE,                        
			  @EMPLOYER_EMAIL,                                            
			  @YEARS_WITH_CURR_OCCU,                                        
			  @GENDER,                          
			  @CPF_CNPJ,                          
			  @NUMBER,                          
			                           
			  @DISTRICT,                          
			                           
			  @MAIN_TITLE,                           
			  @MAIN_POSITION,                           
			  @MAIN_CPF_CNPJ,                           
			  @MAIN_ADDRESS,                           
			  @MAIN_NUMBER,                           
			  @MAIN_COMPLIMENT,                           
			  @MAIN_DISTRICT,                          
			  @MAIN_NOTE,                        
			  @MAIN_CONTACT_CODE,                      
			  @REGIONAL_IDENTIFICATION,                      
			  @REG_ID_ISSUE,                      
			  @ORIGINAL_ISSUE,                    
			  @MAIN_ZIPCODE ,                    
			  @MAIN_CITY ,                   
			  @MAIN_COUNTRY ,                    
			  @MAIN_STATE ,                  
			  @MAIN_FIRST_NAME,                  
			  @MAIN_MIDDLE_NAME,                  
			  @MAIN_LAST_NAME,
			  @ID_TYPE,
			  @MONTHLY_INCOME,
			  @AMOUNT_TYPE,
			  @CADEMP,                  
			  @NET_ASSETS_AMOUNT,
			  @NATIONALITY,
			  @EMAIL_ADDRESS ,
			  @REGIONAL_IDENTIFICATION_TYPE,
			  @IS_POLITICALLY_EXPOSED
			                   
			                                           
			 -- @ALT_CUSTOMER_ADDRESS1,                                                      
			 -- @ALT_CUSTOMER_ADDRESS2,                                                      
			 -- @ALT_CUSTOMER_CITY,                                                      
			 -- @ALT_CUSTOMER_COUNTRY,                                                      
			 -- @ALT_CUSTOMER_STATE,                                                      
			 -- @ALT_CUSTOMER_ZIP                             
			)                                                      
			                                                      
			  SELECT @CUSTOMER_ID = Max(CUSTOMER_ID) FROM CLT_CUSTOMER_LIST WITH(NOLOCK)                                                 
			  SELECT @Cust_Id= Max(CUSTOMER_ID) FROM CLT_CUSTOMER_LIST     WITH(NOLOCK)                                               
			                                                      
			                                                      
			                                                     
			                                                      
			 --- Added by mohit                                                       
			 -- Inserting customer to clt_customer_list                                                     
			 -- if customer is of type personal then only entry for that is to be made in CLT_APPLICANT_LIST.                                                     
			 --11110                                                       
			                                                     
			  declare @APPLICANT_ID int                                               
			  if(@CUSTOMER_TYPE = 11110)                                                    
			  begin                                                 
			 select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST  WITH(NOLOCK)                               
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
			  CREATED_DATETIME ,                                                
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
			  FAX ,
			  BANK_NAME,
			  BANK_NUMBER,
			  BANK_BRANCH,
			  ACCOUNT_NUMBER,
			  ACCOUNT_TYPE       
			          
			  )                                                
			 VALUES                                                 
			  (                                                      
			   @CUSTOMER_TYPE,  
			   @APPLICANT_ID,  
			   @CUSTOMER_ID,  
			   @PREFIX,  
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
			   @CUSTOMER_HOME_PHONE,  
			   @CUSTOMER_Email,  
			   @MARITAL_STATUS,  
			   @SSN_NO,  
			   @DATE_OF_BIRTH,  
			   'Y',  
			   @CREATED_BY,  
			   @CREATED_DATETIME  ,                                                    
			   @APPLICANT_OCCU,  
			   @EMPLOYER_NAME,  
			   @EMPLOYER_ADD1,  
			   @EMPLOYER_ADD2,  
			   @YEARS_WITH_CURR_EMPL,  
			   @YEARS_WITH_CURR_OCCU,  
			   1,  
			   @DESC_APPLICANT_OCCU,                                        
			   @EMPLOYER_CITY,  
			   @EMPLOYER_COUNTRY,  
			   @EMPLOYER_STATE,  
			   @EMPLOYER_ZIPCODE,  
			   @EMPLOYER_HOMEPHONE,  
			   @EMPLOYER_EMAIL,    
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
			   @CUSTOMER_FAX ,
			    @BANK_NAME,
				@BANK_NUMBER,
				@BANK_BRANCH,
				@ACCOUNT_NUMBER,
				@ACCOUNT_TYPE     
                     
			             
			)                               
			 end                                     
			                        
			 if(@CUSTOMER_TYPE = 11109 or @CUSTOMER_TYPE= 14725) --commercial or government               
			 begin                                                     
			                                                     
			                                                      
			 select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST   WITH(NOLOCK)                                                    
			                                                  
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
			  @MARITAL_STATUS,  
			  null,  
			  @DATE_OF_BIRTH,  
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
			  @CUSTOMER_FAX ,
			  @BANK_NAME,
				@BANK_NUMBER,
				@BANK_BRANCH,
				 @ACCOUNT_NUMBER,
				@ACCOUNT_TYPE         
			)                                                    
			 END                                                   
			END
 END 
GO

