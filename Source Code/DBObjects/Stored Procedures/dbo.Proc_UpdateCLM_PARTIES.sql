IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_PARTIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_PARTIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_UpdateCLM_PARTIES                                  
Created by      : Sumit Chhabra                                                                
Date            : 28/04/2006                                                                  
Purpose         : Insert data in CLM_PARTIES table for claim parties screen                                              
Created by      : Sumit Chhabra                                                                 
Revison History :                                                                  
Used In        : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/                                                                  
--DROP PROC dbo.Proc_UpdateCLM_PARTIES                                  
CREATE PROC [dbo].[Proc_UpdateCLM_PARTIES]                                  
@PARTY_ID int,                                  
@CLAIM_ID int,                                  
@NAME varchar(75),-- column width changed to 75 Itrack Issue 5301 by Sibin on 16 Jan 09                                  
@ADDRESS1 varchar(50),                                  
@ADDRESS2 varchar(50),                                  
@CITY varchar(60),                                  
@STATE int,                                  
@ZIP varchar(11),                                  
@CONTACT_PHONE varchar(15),                                  
@CONTACT_EMAIL varchar(50),                                  
@OTHER_DETAILS varchar(500),                                  
@MODIFIED_BY int,                                  
@LAST_UPDATED_DATETIME datetime,                                  
@PARTY_TYPE_ID int,                                  
@COUNTRY int,                                
@PARTY_DETAIL INT,                                
@AGE SMALLINT,                                
@EXTENT_OF_INJURY VARCHAR(200),                            
@REFERENCE varchar(30),                          
@BANK_NAME varchar(100),                          
@AGENCY_BANK varchar(100),    --Added by Santosh Kumar Gautam on 16 Nov 2010      
@ACCOUNT_NUMBER varchar(200),     -- Changed by Aditya for tfs bug # 2522     
@ACCOUNT_NAME varchar(100),                        
@CONTACT_PHONE_EXT varchar(5),                        
@CONTACT_FAX varchar(15),                      
@PARTY_TYPE_DESC varchar(50),                    
@PARENT_ADJUSTER int,                
@FEDRERAL_ID varchar(50),                
@PROCESSING_OPTION_1099 INT = NULL,                
@MASTER_VENDOR_CODE VARCHAR(50) = NULL,                
@VENDOR_CODE VARCHAR(50) = NULL,                
@CONTACT_NAME VARCHAR(50) = NULL,                
@EXPERT_SERVICE_TYPE INT = NULL,                
@EXPERT_SERVICE_TYPE_DESC VARCHAR(50) =  NULL,                
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
 @MAIL_1099_ADD1 nvarchar(280)=NULL,              
 @MAIL_1099_ADD2 nvarchar(280)=NULL,              
 @MAIL_1099_CITY nvarchar(160)=NULL,              
 @MAIL_1099_STATE nvarchar(20)=NULL,              
 @MAIL_1099_COUNTRY nvarchar(20)=NULL,              
 @MAIL_1099_ZIP  varchar(11),          
 @MAIL_1099_NAME  varchar(75),            
 @W9_FORM  nvarchar(10),      
 @ACCOUNT_TYPE  INT , -- Added by Santosh Kumar Gautam on 05 Jan 2011       
 @DISTRICT nvarchar(50)=NULL, -- Added by Shubhanshu Pandey on 13 Jun 2011           
 @REGIONAL_ID nvarchar(50)=NULL, -- Added by Santosh Kumar Gautam on 20 Jan 2011            
 @REGIONAL_ID_ISSUANCE nvarchar(50)=NULL,      
 @REGIONAL_ID_ISSUE_DATE datetime =null,      
 @MARITAL_STATUS int =null,      
 @GENDER int =null ,      
 @BANK_BRANCH nvarchar(20)=NULL,      
 @BANK_NUMBER nvarchar(10)=NULL ,                       
 @PARTY_TYPE INT =NULL       ,      
 @PAYMENT_METHOD INT =NULL,      
 @PARTY_CPF_CNPJ nvarchar(20) =NULL,      
 @IS_BENEFICIARY CHAR(2) = 'Y'      
      
AS                                                                  
BEGIN                      
                
DECLARE @RETURN_VALUE INT                
SET @RETURN_VALUE = 1                
IF (@VENDOR_CODE ='') SET @VENDOR_CODE=NULL              
 --IF EXISTS(SELECT VENDOR_CODE FROM CLM_PARTIES                 
 -- WHERE VENDOR_CODE=@VENDOR_CODE AND   CLAIM_ID=@CLAIM_ID AND PARTY_ID<>@PARTY_ID  )                
 --BEGIN                
 --SET @RETURN_VALUE = -2                
 --RETURN @RETURN_VALUE                
 --END                
                                   
 update CLM_PARTIES                                   
 set                                               
  NAME=@NAME,                                  
  ADDRESS1=@ADDRESS1,                                  
  ADDRESS2=@ADDRESS2,                                  
  CITY=@CITY,                             
  STATE=@STATE,                                  
  ZIP=@ZIP,                                  
  CONTACT_PHONE=@CONTACT_PHONE,                                  
  CONTACT_EMAIL=@CONTACT_EMAIL,                                  
  OTHER_DETAILS=@OTHER_DETAILS,                                  
  MODIFIED_BY=@MODIFIED_BY,                                  
  LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,                                  
  PARTY_TYPE_ID=@PARTY_TYPE_ID,                                  
  COUNTRY=@COUNTRY  ,                                
  PARTY_DETAIL=@PARTY_DETAIL,                                
  AGE=@AGE,                                
  EXTENT_OF_INJURY=@EXTENT_OF_INJURY,                            
  REFERENCE=@REFERENCE,                          
 BANK_NAME=@BANK_NAME,         
 AGENCY_BANK=@AGENCY_BANK , --Added by Santosh Kumar Gautam on 16 Nov 2010                       
 ACCOUNT_NUMBER=@ACCOUNT_NUMBER,                          
 ACCOUNT_NAME=@ACCOUNT_NAME,                        
CONTACT_PHONE_EXT=@CONTACT_PHONE_EXT,                        
CONTACT_FAX=@CONTACT_FAX,                      
PARTY_TYPE_DESC = @PARTY_TYPE_DESC,                    
PARENT_ADJUSTER = @PARENT_ADJUSTER,                
FEDRERAL_ID = @FEDRERAL_ID,                
PROCESSING_OPTION_1099 = @PROCESSING_OPTION_1099,                
MASTER_VENDOR_CODE=@MASTER_VENDOR_CODE,                
VENDOR_CODE = @VENDOR_CODE,                
CONTACT_NAME = @CONTACT_NAME,                
EXPERT_SERVICE_TYPE = @EXPERT_SERVICE_TYPE,                
EXPERT_SERVICE_TYPE_DESC = @EXPERT_SERVICE_TYPE_DESC,                
SUB_ADJUSTER=@SUB_ADJUSTER,                
SA_ADDRESS1=@SA_ADDRESS1,                
SA_ADDRESS2=@SA_ADDRESS2,                
SA_CITY=@SA_CITY,                
SA_COUNTRY=@SA_COUNTRY,                
SA_STATE=@SA_STATE,                
SA_ZIPCODE=@SA_ZIPCODE,                
SA_PHONE=@SA_PHONE,                
SA_FAX=@SA_FAX,                
SUB_ADJUSTER_CONTACT_NAME=@SUB_ADJUSTER_CONTACT_NAME,            
  MAIL_1099_ADD1 = @MAIL_1099_ADD1,              
  MAIL_1099_ADD2 = @MAIL_1099_ADD2,              
  MAIL_1099_CITY = @MAIL_1099_CITY,              
  MAIL_1099_STATE = @MAIL_1099_STATE,              
  MAIL_1099_COUNTRY = @MAIL_1099_COUNTRY,              
  MAIL_1099_ZIP = @MAIL_1099_ZIP,                
  MAIL_1099_NAME = @MAIL_1099_NAME,              
  W9_FORM = @W9_FORM  ,      
  ACCOUNT_TYPE=@ACCOUNT_TYPE ,      
  DISTRICT = @DISTRICT,           
REGIONAL_ID=@REGIONAL_ID,      
REGIONAL_ID_ISSUANCE=@REGIONAL_ID_ISSUANCE,      
REGIONAL_ID_ISSUE_DATE=@REGIONAL_ID_ISSUE_DATE,      
MARITAL_STATUS=@MARITAL_STATUS,      
GENDER=@GENDER,      
BANK_BRANCH=@BANK_BRANCH,      
BANK_NUMBER=@BANK_NUMBER,      
PARTY_TYPE=@PARTY_TYPE,      
PAYMENT_METHOD =@PAYMENT_METHOD,      
PARTY_CPF_CNPJ=  @PARTY_CPF_CNPJ,      
IS_BENEFICIARY = @IS_BENEFICIARY       
      
 WHERE                                  
  PARTY_ID = @PARTY_ID    AND                              
  CLAIM_ID = @CLAIM_ID                  
                            
 RETURN @RETURN_VALUE                
END 
GO

