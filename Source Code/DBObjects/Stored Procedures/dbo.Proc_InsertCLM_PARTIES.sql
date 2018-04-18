IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_PARTIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_PARTIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name       : dbo.Proc_InsertCLM_PARTIES                            
Created by      : Sumit Chhabra                                                          
Date            : 28/04/2006                                                            
Purpose         : Insert data in CLM_PARTIES table for claim parties screen                                        
Created by      : Sumit Chhabra                                                           
Revison History :                                                            
Used In        : Wolverine                                                            
------------------------------------------------------------                                                            
Date     Review By          Comments                                                            
------   ------------       -------------------------*/                                                            
--DROP PROC dbo.Proc_InsertCLM_PARTIES                            
CREATE PROC [dbo].[Proc_InsertCLM_PARTIES]                            
@PARTY_ID int output,                            
@CLAIM_ID int,                            
@NAME varchar(75), -- column width changed to 75 Itrack Issue 5301 by Sibin on 16 Jan 09                           
@ADDRESS1 varchar(50),                            
@ADDRESS2 varchar(50),                            
@CITY varchar(60),                            
@STATE int,                            
@ZIP varchar(11),                            
@CONTACT_PHONE varchar(15),                            
@CONTACT_EMAIL varchar(50),                            
@OTHER_DETAILS varchar(500),                            
@CREATED_BY int,                            
@CREATED_DATETIME datetime,                            
@PARTY_TYPE_ID int,                            
@COUNTRY int ,                          
@PARTY_DETAIL INT,                          
@AGE SMALLINT,                          
@EXTENT_OF_INJURY VARCHAR(200),                      
@REFERENCE VARCHAR(30),                    
@BANK_NAME varchar(100),          
@AGENCY_BANK  nvarchar(100),  -- Added by Santosh Kumar Gautam on 16 Nov 2010         
@ACCOUNT_TYPE  INT,  -- Added by Santosh Kumar Gautam on 05 Jan 2011    
@ACCOUNT_NUMBER varchar(200),                    
@ACCOUNT_NAME varchar(100),          -- Changed by Aditya for tfs bug # 2522         
@CONTACT_PHONE_EXT varchar(5),                  
@CONTACT_FAX varchar(15),                
@PARTY_TYPE_DESC varchar(50),              
@PARENT_ADJUSTER int,          
@FEDRERAL_ID varchar(50),          
@PROCESSING_OPTION_1099 INT = NULL,          
@MASTER_VENDOR_CODE VARCHAR(50) = NULL,          
@VENDOR_CODE VARCHAR(50) = NULL,          
@CONTACT_NAME varchar(50) = null,          
@EXPERT_SERVICE_TYPE INT = NULL,          
@EXPERT_SERVICE_TYPE_DESC VARCHAR(50) = NULL,          
@SUB_ADJUSTER VARCHAR(35)=NULL,          
@SA_ADDRESS1 VARCHAR(75)=NULL,          
@SA_ADDRESS2 VARCHAR(75)=NULL,          
@SA_CITY VARCHAR(75)=NULL,          
@SA_COUNTRY VARCHAR(2)=NULL,          
@SA_STATE INT=NULL,          
@SA_ZIPCODE VARCHAR(11)=NULL,          
@SA_PHONE VARCHAR(15)=NULL,          
@SA_FAX VARCHAR(15)=NULL,          
@SUB_ADJUSTER_CONTACT_NAME VARCHAR(50)=NULL,          
@PROP_DAMAGED_ID int =null,        
 @MAIL_1099_ADD1 nvarchar(280)=NULL,            
 @MAIL_1099_ADD2 nvarchar(280)=NULL,            
 @MAIL_1099_CITY nvarchar(160)=NULL,            
 @MAIL_1099_STATE nvarchar(20)=NULL,            
 @MAIL_1099_COUNTRY nvarchar(20)=NULL,            
 @MAIL_1099_ZIP  varchar(11)  =NULL,          
 @MAIL_1099_NAME  varchar(75) =NULL,        
 @W9_FORM  nvarchar(10) =NULL,  
 @DISTRICT  nvarchar(50)=NULL, -- Added by Shubhanshu Pandey on 13 Jun 2011            
  @REGIONAL_ID nvarchar(50)=NULL,
 @REGIONAL_ID_ISSUANCE nvarchar(50)=NULL,
 @REGIONAL_ID_ISSUE_DATE datetime =null,
 @MARITAL_STATUS int =null,
 @GENDER int =null ,
 @BANK_BRANCH nvarchar(20)=NULL,
 @BANK_NUMBER nvarchar(10)=NULL ,
 @PARTY_TYPE INT =NULL   ,
 @PAYMENT_METHOD INT =NULL,
 @PARTY_CPF_CNPJ nvarchar(20) =NULL,
 @IS_BENEFICIARY CHAR(2) = 'Y'

                   
AS                                                            
BEGIN                                                            
 SELECT                                         
  @PARTY_ID = ISNULL(MAX(PARTY_ID),0)+1                                         
 FROM                                         
  CLM_PARTIES                           
 WHERE                        
  CLAIM_ID=@CLAIM_ID             
 DECLARE @RETURN_VALUE int          
 SET @RETURN_VALUE = 1          
 
 
 --===============================================================
 -- IF INSURED PARTY IS ALREADY EXISTS THEN RETURN
 --===============================================================
 IF (@PARTY_TYPE_ID=10 AND EXISTS(SELECT CLAIM_ID FROM CLM_PARTIES WHERE  CLAIM_ID=@CLAIM_ID AND PARTY_TYPE_ID=10))
  BEGIN
	SET @RETURN_VALUE = -3          
    RETURN @RETURN_VALUE          
  END          
                                        
 INSERT INTO CLM_PARTIES                            
 (                           
  PARTY_ID,                            
  CLAIM_ID,                            
  NAME,                            
  ADDRESS1,                            
  ADDRESS2,                            
  CITY,                            
  STATE,                        ZIP,                            
  CONTACT_PHONE,                            
  CONTACT_EMAIL,                            
  OTHER_DETAILS,                            
  CREATED_BY,                            
  CREATED_DATETIME,                        
  PARTY_TYPE_ID,                            
  COUNTRY,                            
  IS_ACTIVE,                            
  PARTY_DETAIL,                          
  AGE,                          
  EXTENT_OF_INJURY,                      
  REFERENCE,                    
 BANK_NAME,        
  AGENCY_BANK,         -- Added by Santosh Kumar Gautam on 16 Nov 2010              
 ACCOUNT_NUMBER,                    
 ACCOUNT_NAME,                  
 CONTACT_PHONE_EXT,                  
CONTACT_FAX,                
PARTY_TYPE_DESC,              
PARENT_ADJUSTER,          
FEDRERAL_ID,          
PROCESSING_OPTION_1099,          
MASTER_VENDOR_CODE,          
VENDOR_CODE,          
CONTACT_NAME,          
EXPERT_SERVICE_TYPE,          
EXPERT_SERVICE_TYPE_DESC,          
SUB_ADJUSTER,          
SA_ADDRESS1,          
SA_ADDRESS2,          
SA_CITY,          
SA_COUNTRY,          
SA_STATE,          
SA_ZIPCODE,          
SA_PHONE,          
SA_FAX,          
SUB_ADJUSTER_CONTACT_NAME,          
PROP_DAMAGED_ID,        
  MAIL_1099_ADD1,          
  MAIL_1099_ADD2,          
  MAIL_1099_CITY,          
  MAIL_1099_STATE,          
  MAIL_1099_COUNTRY,          
  MAIL_1099_ZIP,        
  MAIL_1099_NAME,          
  W9_FORM    ,
  ACCOUNT_TYPE,
  DISTRICT,
  REGIONAL_ID,
  REGIONAL_ID_ISSUANCE,
  REGIONAL_ID_ISSUE_DATE,
  MARITAL_STATUS,
  GENDER,
  BANK_BRANCH,
  BANK_NUMBER,
  PARTY_TYPE,
  PAYMENT_METHOD,
  PARTY_CPF_CNPJ,
  IS_BENEFICIARY
          
 )                                        
 VALUES                                        
 (                                        
  @PARTY_ID,                            
  @CLAIM_ID,                  
  @NAME,                            
  @ADDRESS1,                            
  @ADDRESS2,                     
  @CITY,                            
  @STATE,                            
  @ZIP,                            
  @CONTACT_PHONE,                            
  @CONTACT_EMAIL,                            
  @OTHER_DETAILS,                            
  @CREATED_BY,                            
  @CREATED_DATETIME,                            
  @PARTY_TYPE_ID,                  
  @COUNTRY,                            
  'Y',                          
  @PARTY_DETAIL,                          
  @AGE,                          
  @EXTENT_OF_INJURY,                      
  @REFERENCE,                    
 @BANK_NAME,     
 @AGENCY_BANK,  -- Added by Santosh Kumar Gautam on 16 Nov 2010                        
 @ACCOUNT_NUMBER,                    
 @ACCOUNT_NAME,                  
 @CONTACT_PHONE_EXT,                  
 @CONTACT_FAX,                
@PARTY_TYPE_DESC,              
@PARENT_ADJUSTER,          
@FEDRERAL_ID,          
@PROCESSING_OPTION_1099,          
@MASTER_VENDOR_CODE,          
@VENDOR_CODE,          
@CONTACT_NAME,          
@EXPERT_SERVICE_TYPE,          
@EXPERT_SERVICE_TYPE_DESC,          
@SUB_ADJUSTER,          
@SA_ADDRESS1,          
@SA_ADDRESS2,          
@SA_CITY,          
@SA_COUNTRY,          
@SA_STATE,          
@SA_ZIPCODE,          
@SA_PHONE,          
@SA_FAX,          
@SUB_ADJUSTER_CONTACT_NAME ,          
@PROP_DAMAGED_ID,        
  @MAIL_1099_ADD1,          
  @MAIL_1099_ADD2,          
  @MAIL_1099_CITY,          
  @MAIL_1099_STATE,          
  @MAIL_1099_COUNTRY,          
  @MAIL_1099_ZIP,      
  @MAIL_1099_NAME,          
  @W9_FORM   ,
  @ACCOUNT_TYPE ,
  @DISTRICT,
@REGIONAL_ID,
@REGIONAL_ID_ISSUANCE,
@REGIONAL_ID_ISSUE_DATE,
@MARITAL_STATUS,
@GENDER,
@BANK_BRANCH,
@BANK_NUMBER,
@PARTY_TYPE,
@PAYMENT_METHOD,
@PARTY_CPF_CNPJ,
@IS_BENEFICIARY
         
             
 )                              
 RETURN @RETURN_VALUE          
                                    
END          
   
  
  

GO

