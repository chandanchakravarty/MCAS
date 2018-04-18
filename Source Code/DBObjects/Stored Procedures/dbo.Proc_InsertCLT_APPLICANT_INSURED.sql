IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLT_APPLICANT_INSURED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED]  
CREATE PROCEDURE [dbo].[Proc_InsertCLT_APPLICANT_INSURED]     --Proc_InsertCLT_APPLICANT_INSURED                     
(                          
 @APPLICANT_ID    int output,                          
 @CUSTOMER_ID   int,                          
 @TITLE     nvarchar(10),                          
 @SUFFIX     nvarchar(5),                          
 @FIRST_NAME     nvarchar(200),                          
 @MIDDLE_NAME     nvarchar(10),                          
 @LAST_NAME     nvarchar(25),                          
 @ADDRESS1     nvarchar(150),                          
 @ADDRESS2     nvarchar(100),                          
 @CITY     nvarchar(70),                          
 @COUNTRY     nvarchar(10),                          
 @STATE     nvarchar(10),                    
                          
 @ZIP_CODE     nvarchar(20),                          
 @PHONE     nvarchar(20),                    
-- Mobile, Business_ph, ext included by swastika on 21st Mar'06 for Gen Iss# 2367                    
 @MOBILE nvarchar(20),                    
 @BUSINESS_PHONE nvarchar(20),                    
 @EXT nvarchar(5),                          
 @EMAIL     nvarchar(50),                          
 @IS_ACTIVE nchar(1),                          
 @CREATED_BY int,                          
 @CO_APPLI_OCCU nvarchar(25),                          
 @CO_APPLI_EMPL_NAME nvarchar(75),                          
 @CO_APPLI_EMPL_ADDRESS nvarchar(75),                    
 @CO_APPLI_EMPL_ADDRESS1 nvarchar(75),                    
 -- <START> Fields included by swastika on 10th Apr'06 for Gen Iss# 2367                    
 @CO_APPLI_EMPL_CITY     nvarchar(70),                          
 @CO_APPLI_EMPL_COUNTRY     nvarchar(10),                          
 @CO_APPLI_EMPL_STATE     nvarchar(10),                    
 @CO_APPLI_EMPL_ZIP_CODE     nvarchar(12),                          
 @CO_APPLI_EMPL_PHONE     nvarchar(20),                     
 @CO_APPLI_EMPL_EMAIL     nvarchar(50),                   
                  
 -- <START> Fields included by Neeraj Singh on 10th Apr'06 for Gen Iss# 09                   
 @CO_APPL_RELATIONSHIP nvarchar(25),                   
 @CO_APPL_GENDER nvarchar(20),                   
--<END>                    
 @CO_APPLI_YEARS_WITH_CURR_EMPL real,                          
 @CO_APPL_YEAR_CURR_OCCU real,                          
 @CO_APPL_MARITAL_STATUS nchar(1),                          
 @CO_APPL_DOB datetime,                          
 @CO_APPL_SSN_NO nvarchar(44),                        
 @DESC_CO_APPLI_OCCU nvarchar(200),                    
 @APPLICATION_FLAG int=0,              
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
@APPLICANT_TYPE int,
@ACCOUNT_TYPE int,
@ACCOUNT_NUMBER NVARCHAR(20),
@BANK_NAME NVARCHAR(100),
@BANK_NUMBER NVARCHAR(20),
@BANK_BRANCH NVARCHAR(20)              
                            
)                          
AS                          
	BEGIN     
	IF EXISTS(SELECT CONTACT_CODE FROM CLT_APPLICANT_LIST WITH(NOLOCK) WHERE CONTACT_CODE=@CONTACT_CODE)                 
		BEGIN
			 SET @APPLICANT_ID = -1
		END
	ELSE
		BEGIN            
			SELECT @APPLICANT_ID=ISNULL(MAX(APPLICANT_ID),0)+1 FROM CLT_APPLICANT_LIST                         
			                          
			INSERT INTO CLT_APPLICANT_LIST                          
			(                          
			APPLICANT_ID,CUSTOMER_ID,TITLE,SUFFIX,FIRST_NAME,MIDDLE_NAME,                          
			LAST_NAME,ADDRESS1,ADDRESS2,CITY,COUNTRY,STATE,ZIP_CODE,PHONE,MOBILE,BUSINESS_PHONE,EXT,EMAIL,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                          
			CO_APPLI_OCCU, CO_APPLI_EMPL_NAME ,CO_APPLI_EMPL_ADDRESS,CO_APPLI_EMPL_ADDRESS1,CO_APPLI_EMPL_CITY,CO_APPLI_EMPL_COUNTRY,CO_APPLI_EMPL_STATE,CO_APPLI_EMPL_ZIP_CODE,                    
			CO_APPLI_EMPL_PHONE,CO_APPLI_EMPL_EMAIL,CO_APPLI_YEARS_WITH_CURR_EMPL,CO_APPL_YEAR_CURR_OCCU,                          
			CO_APPL_MARITAL_STATUS,CO_APPL_DOB,CO_APPL_SSN_NO,IS_PRIMARY_APPLICANT,DESC_CO_APPLI_OCCU,CO_APPL_RELATIONSHIP,CO_APPL_GENDER,              
			POSITION,              
			CONTACT_CODE,              
			ID_TYPE,              
			ID_TYPE_NUMBER,              
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
            BANK_BRANCH              
			               
			                          
			)                          
			VALUES                          
			(                          
			@APPLICANT_ID,@CUSTOMER_ID,@TITLE,@SUFFIX,@FIRST_NAME,@MIDDLE_NAME,@LAST_NAME,@ADDRESS1,@ADDRESS2,@CITY,@COUNTRY,@STATE,@ZIP_CODE,                          
			@PHONE,@MOBILE,@BUSINESS_PHONE,@EXT,@EMAIL,@IS_ACTIVE,@CREATED_BY,GETDATE(),@CO_APPLI_OCCU, @CO_APPLI_EMPL_NAME ,@CO_APPLI_EMPL_ADDRESS ,@CO_APPLI_EMPL_ADDRESS1,@CO_APPLI_EMPL_CITY,                    
			@CO_APPLI_EMPL_COUNTRY,@CO_APPLI_EMPL_STATE,@CO_APPLI_EMPL_ZIP_CODE,@CO_APPLI_EMPL_PHONE,@CO_APPLI_EMPL_EMAIL,@CO_APPLI_YEARS_WITH_CURR_EMPL,                          
			@CO_APPL_YEAR_CURR_OCCU,@CO_APPL_MARITAL_STATUS,@CO_APPL_DOB,@CO_APPL_SSN_NO,0,@DESC_CO_APPLI_OCCU,@CO_APPL_RELATIONSHIP,@CO_APPL_GENDER,              
			@POSITION,              
			@CONTACT_CODE,              
			@ID_TYPE,              
			@ID_TYPE_NUMBER,              
			@NUMBER,              
			@COMPLIMENT,              
			@DISTRICT,              
			@NOTE,            
			@REGIONAL_IDENTIFICATION,            
			@REG_ID_ISSUE,            
			@ORIGINAL_ISSUE,            
			@FAX ,            
			@CPF_CNPJ,        
			@APPLICANT_TYPE,
			@ACCOUNT_TYPE,
            @ACCOUNT_NUMBER,
            @BANK_NAME,
            @BANK_NUMBER,
            @BANK_BRANCH         
			                            
			)                   
			END      
END                          
                    
                    
                    
                    
                    
GO

