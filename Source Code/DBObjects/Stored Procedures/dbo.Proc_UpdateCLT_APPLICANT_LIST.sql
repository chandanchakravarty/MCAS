IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLT_APPLICANT_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLT_APPLICANT_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

              
--drop PROC  [dbo].[Proc_UpdateCLT_APPLICANT_LIST]
              
CREATE PROC [dbo].[Proc_UpdateCLT_APPLICANT_LIST]                          
(                          
 @APPLICANT_ID   int OUT,                          
 @CUSTOMER_ID   int,                          
 @TITLE   nvarchar(10),                          
 @SUFFIX     nvarchar(5),                          
 @FIRST_NAME     nvarchar(200),                          
 @MIDDLE_NAME     nvarchar(10),                          
 @LAST_NAME     nvarchar(25),                          
 @ADDRESS1     nvarchar(150),                          
 @ADDRESS2     nvarchar(100),                          
 @CITY     nvarchar(70),                          
 @COUNTRY    nvarchar(10),                          
 @STATE     nvarchar(10),                          
 @ZIP_CODE     nvarchar(20),                          
 @PHONE     nvarchar(20),                    
-- Mobile, Business_ph, ext included by swastika on 21st Mar'06 for Gen Iss# 2367                    
 @MOBILE    nvarchar(20),                    
 @BUSINESS_PHONE nvarchar(20),                    
 @EXT nvarchar(5),                          
 @EMAIL     nvarchar(50),                          
 @IS_ACTIVE nchar(1),                          
 @MODIFIED_BY int,                          
 @CO_APPLI_OCCU nvarchar(25),                          
 @CO_APPLI_EMPL_NAME nvarchar(75),                          
 @CO_APPLI_EMPL_ADDRESS nvarchar(75),                    
 @CO_APPLI_EMPL_ADDRESS1 nvarchar(75),                    
-- <START> Swastika                    
 @CO_APPLI_EMPL_CITY     nvarchar(70),                          
 @CO_APPLI_EMPL_COUNTRY     nvarchar(10),                          
 @CO_APPLI_EMPL_STATE     nvarchar(10),                    
 @CO_APPLI_EMPL_ZIP_CODE     nvarchar(12),                          
 @CO_APPLI_EMPL_PHONE     nvarchar(20),                     
 @CO_APPLI_EMPL_EMAIL     nvarchar(50),                    
 @CO_APPL_RELATIONSHIP nvarchar(25),                  
 @CO_APPL_GENDER     nvarchar(20),                  
--<END>                          
 @CO_APPLI_YEARS_WITH_CURR_EMPL real,                          
 @CO_APPL_YEAR_CURR_OCCU real,                          
 @CO_APPL_MARITAL_STATUS nchar(1),                          
 @CO_APPL_DOB datetime,                          
 @CO_APPL_SSN_NO nvarchar(44),                        
 @DESC_CO_APPLI_OCCU nvarchar(200),                    
 @APPLICATION_FLAG INT=0,            
 --Added By Lalit            
@POSITION int,                
@CONTACT_CODE nvarchar(40),                
@ID_TYPE int,                
@ID_TYPE_NUMBER nvarchar(40),                
@NUMBER nvarchar(40),                
@COMPLIMENT nvarchar(40),                
@DISTRICT nvarchar(40),                
@NOTE nvarchar(250),              
@REGIONAL_IDENTIFICATION  nvarchar(40),              
@REG_ID_ISSUE datetime,              
@ORIGINAL_ISSUE  nvarchar(40),              
@FAX nvarchar(40),              
@CPF_CNPJ nvarchar(40),            
@APPLICANT_TYPE int ,
@ACCOUNT_TYPE int,
@ACCOUNT_NUMBER NVARCHAR(20),
@BANK_NAME NVARCHAR(100),
@BANK_NUMBER NVARCHAR(20),
@BANK_BRANCH NVARCHAR(20)            
             
                           
)                          
AS                          
BEGIN                      
	IF EXISTS(SELECT CONTACT_CODE FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CONTACT_CODE=@CONTACT_CODE AND APPLICANT_ID <> @APPLICANT_ID)                 
		BEGIN
			 SET @APPLICANT_ID = -1
		END
	ELSE
			BEGIN   
					UPDATE  CLT_APPLICANT_LIST                          
					SET                          
					TITLE  = @TITLE,                          
					SUFFIX  =  @SUFFIX,                          
					FIRST_NAME  =  @FIRST_NAME,                          
					MIDDLE_NAME  =  @MIDDLE_NAME,                          
					LAST_NAME  =  @LAST_NAME,                          
					ADDRESS1  =  @ADDRESS1,                          
					ADDRESS2  = @ADDRESS2,                          
					CITY  =  @CITY,                          
					COUNTRY  =  @COUNTRY,                          
					STATE  =  @STATE,                          
					ZIP_CODE  = @ZIP_CODE,                          
					PHONE  =  @PHONE,                    
					MOBILE = @MOBILE,                    
					BUSINESS_PHONE = @BUSINESS_PHONE,                    
					EXT = @EXT,                          
					EMAIL  =  @EMAIL,                          
					IS_ACTIVE=@IS_ACTIVE,                          
					MODIFIED_BY=@MODIFIED_BY,                          
					LAST_UPDATED_TIME=GETDATE(),                          
					CO_APPLI_OCCU=@CO_APPLI_OCCU,                           
					CO_APPLI_EMPL_NAME=@CO_APPLI_EMPL_NAME,                           
					CO_APPLI_EMPL_ADDRESS=@CO_APPLI_EMPL_ADDRESS,                    
					CO_APPLI_EMPL_ADDRESS1=@CO_APPLI_EMPL_ADDRESS1,
					ACCOUNT_TYPE= @ACCOUNT_TYPE,
				    ACCOUNT_NUMBER=@ACCOUNT_NUMBER,
				    BANK_NUMBER=@BANK_NUMBER,
				    BANK_NAME=@BANK_NAME,
				    BANK_BRANCH=@BANK_BRANCH,                        
					                    
					CO_APPLI_EMPL_CITY=@CO_APPLI_EMPL_CITY,                    
					CO_APPLI_EMPL_COUNTRY=@CO_APPLI_EMPL_COUNTRY,                     
					CO_APPLI_EMPL_STATE=@CO_APPLI_EMPL_STATE,                    
					CO_APPLI_EMPL_ZIP_CODE=@CO_APPLI_EMPL_ZIP_CODE,                    
					CO_APPLI_EMPL_PHONE=@CO_APPLI_EMPL_PHONE,                    
					CO_APPLI_EMPL_EMAIL=@CO_APPLI_EMPL_EMAIL,                    
					--ADDED BY NEERAJ SINGH.                     
					CO_APPL_RELATIONSHIP=@CO_APPL_RELATIONSHIP,                  
					CO_APPL_GENDER=@CO_APPL_GENDER,                           
					CO_APPLI_YEARS_WITH_CURR_EMPL=@CO_APPLI_YEARS_WITH_CURR_EMPL,                          
					CO_APPL_YEAR_CURR_OCCU=@CO_APPL_YEAR_CURR_OCCU,                          
					CO_APPL_MARITAL_STATUS=@CO_APPL_MARITAL_STATUS,                          
					CO_APPL_DOB=@CO_APPL_DOB,                          
					CO_APPL_SSN_NO=@CO_APPL_SSN_NO,                        
					DESC_CO_APPLI_OCCU=@DESC_CO_APPLI_OCCU,            
					POSITION=@POSITION,                
					CONTACT_CODE=@CONTACT_CODE,                
					ID_TYPE=@ID_TYPE,                
					ID_TYPE_NUMBER=@ID_TYPE_NUMBER,            
					NUMBER=@NUMBER,                
					COMPLIMENT=@COMPLIMENT,                
					DISTRICT=@DISTRICT,                
					NOTE=@NOTE,              
					REGIONAL_IDENTIFICATION=@REGIONAL_IDENTIFICATION,              
					REG_ID_ISSUE=@REG_ID_ISSUE,              
					ORIGINAL_ISSUE=@ORIGINAL_ISSUE,              
					FAX=@FAX,              
					CPF_CNPJ=@CPF_CNPJ,        
					APPLICANT_TYPE=@APPLICANT_TYPE            
					            
					WHERE  APPLICANT_ID = @APPLICANT_ID                          
					                          
					--SELECT * FROM CLT_APPLICANT_LIST                          
					                          
					--ADDED BY MOHIT.                          
					--UPDATING THE CLT_CUSTOMER_LIST WHILE UPDATING THE DEFAULT CUSTOMER OF TYPE PERSONAL.                          
					DECLARE @CUSTTYPE INT                          
					DECLARE @ISPRIMARY INT                          
					--FINDING THE TYPE OF CUSTOMER                          
					SELECT @CUSTTYPE=CUSTOMER_TYPE FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID                          
					SELECT @ISPRIMARY=IS_PRIMARY_APPLICANT                           
					FROM CLT_APPLICANT_LIST                           
					WHERE APPLICANT_ID=@APPLICANT_ID AND CUSTOMER_ID=@CUSTOMER_ID                          
					-- CHECK IF APPLICANT IS OF PERSONAL TYPE AND IS PRIMARY APPLICANT                          
					IF (@CUSTTYPE = 11110 AND @ISPRIMARY = 1)                          
					BEGIN                          
						UPDATE CLT_CUSTOMER_LIST  SET                          
						PREFIX=@TITLE,                       
						CUSTOMER_SUFFIX=@SUFFIX,                          
						CUSTOMER_FIRST_NAME=@FIRST_NAME,                          
						CUSTOMER_MIDDLE_NAME=@MIDDLE_NAME,                          
						CUSTOMER_LAST_NAME=@LAST_NAME,                          
						CUSTOMER_ADDRESS1=@ADDRESS1,                          
						CUSTOMER_ADDRESS2=@ADDRESS2,                          
						CUSTOMER_CITY=@CITY,                          
						CUSTOMER_COUNTRY=@COUNTRY,                          
						CUSTOMER_STATE=@STATE,                          
						CUSTOMER_ZIP=@ZIP_CODE,                          
						CUSTOMER_BUSINESS_PHONE=@PHONE,                          
						CUSTOMER_EMAIL=@EMAIL,                          
						MARITAL_STATUS=@CO_APPL_MARITAL_STATUS,                          
						SSN_NO=@CO_APPL_SSN_NO,                          
						DATE_OF_BIRTH=@CO_APPL_DOB,
						            
						                         
						MODIFIED_BY=@MODIFIED_BY,                          
						LAST_UPDATED_DATETIME=GETDATE(),                     
						                         
						APPLICANT_OCCU=@CO_APPLI_OCCU,                          
						EMPLOYER_NAME=@CO_APPLI_EMPL_NAME,                          
						EMPLOYER_ADDRESS=@CO_APPLI_EMPL_ADDRESS,                          
						YEARS_WITH_CURR_EMPL=@CO_APPLI_YEARS_WITH_CURR_EMPL,                      
						DESC_APPLICANT_OCCU=@DESC_CO_APPLI_OCCU,                    
						 -- ADDED BY MOHIT ON 4/11/2005                         
						--<START> : ADDED BY SWASTIKA ON 14TH APR'06 // IF PRIMARY APPLICANT IS MODIFIED,ALL (PERSONAL) CUSTOMER INFO IS UPDATED                    
						EMPLOYER_CITY=@CO_APPLI_EMPL_CITY,                    
						EMPLOYER_COUNTRY=@CO_APPLI_EMPL_COUNTRY,                    
						EMPLOYER_STATE=@CO_APPLI_EMPL_STATE,                    
						EMPLOYER_ZIPCODE=@CO_APPLI_EMPL_ZIP_CODE,                    
						EMPLOYER_HOMEPHONE=@CO_APPLI_EMPL_PHONE,            
						EMPLOYER_EMAIL=@CO_APPLI_EMPL_EMAIL,                    
						--ADDED BY SIBIN FOR ITRACK ISSUE 5130 ON 29 DEC 08              
						PER_CUST_MOBILE =@MOBILE,              
						CUSTOMER_HOME_PHONE=@PHONE,
						NUMBER=@NUMBER,

						CPF_CNPJ=@CPF_CNPJ,
						DISTRICT=@DISTRICT,
						REGIONAL_IDENTIFICATION=@REGIONAL_IDENTIFICATION,
						REG_ID_ISSUE=@REG_ID_ISSUE,
						ORIGINAL_ISSUE=@REG_ID_ISSUE,
						GENDER=@CO_APPL_GENDER,
						CUSTOMER_CODE=@CONTACT_CODE  
						WHERE CUSTOMER_ID=@CUSTOMER_ID     
					END                          
	END
END         

               
                    
                    
GO

