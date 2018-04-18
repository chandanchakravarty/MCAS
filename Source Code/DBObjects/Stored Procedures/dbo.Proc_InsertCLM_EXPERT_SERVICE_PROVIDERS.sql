IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                  
                                                  
Proc Name       : Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS                                  
Created by      : Sumit Chhabra                                  
Date            : 21/04/2006                                                  
Purpose         : Insert of Expert Service Providers data in CLM_EXPERT_SERVICE_PROVIDERS                                  
Revison History :                                                  
Used In                   : Wolverine                                                  
------------------------------------------------------------                                                  
Date     Review By          Comments                                                  
------   ------------       -------------------------*/                                                  
--DROP PROC dbo.Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS                                                 
CREATE PROC [dbo].[Proc_InsertCLM_EXPERT_SERVICE_PROVIDERS]                                                 
(                                                  
 @EXPERT_SERVICE_ID int output,                            
 @EXPERT_SERVICE_TYPE int,                            
 @EXPERT_SERVICE_NAME varchar(50),                            
 @EXPERT_SERVICE_ADDRESS1 varchar(75),                            
 @EXPERT_SERVICE_ADDRESS2 varchar(75),                            
 @EXPERT_SERVICE_CITY varchar(25),                            
 @EXPERT_SERVICE_STATE int,                            
 @EXPERT_SERVICE_ZIP varchar(11),                      
 @EXPERT_SERVICE_VENDOR_CODE varchar(50),                            
 @EXPERT_SERVICE_CONTACT_NAME varchar(50),                            
 @EXPERT_SERVICE_CONTACT_PHONE varchar(50),                            
 @EXPERT_SERVICE_CONTACT_EMAIL varchar(50),                            
 @EXPERT_SERVICE_FEDRERAL_ID varchar(250),                            
 @EXPERT_SERVICE_1099_PROCESSING_OPTION int,                            
 @CREATED_BY int,                            
 @CREATED_DATETIME datetime,                        
 @EXPERT_SERVICE_COUNTRY nchar(1),                  
 @EXPERT_SERVICE_MASTER_VENDOR_CODE  varchar(50),          
 @EXPERT_SERVICE_TYPE_DESC varchar(50),          
 @PARTY_DETAIL int,          
 @AGE smallint,          
 @EXTENT_OF_INJURY varchar(200),          
 @OTHER_DETAILS varchar(500),          
 @BANK_NAME varchar(100),          
 @ACCOUNT_NUMBER varchar(20),          
 @ACCOUNT_NAME varchar(100),          
 @EXPERT_SERVICE_CONTACT_PHONE_EXT varchar(5),          
 @EXPERT_SERVICE_CONTACT_FAX varchar(50),          
 @PARENT_ADJUSTER int,        
 @MAIL_1099_ADD1 nvarchar(280),        
 @MAIL_1099_ADD2 nvarchar(280),        
 @MAIL_1099_CITY nvarchar(160),        
 @MAIL_1099_STATE nvarchar(20),        
 @MAIL_1099_COUNTRY nvarchar(20),        
 @MAIL_1099_ZIP  varchar(11),        
 @MAIL_1099_NAME  varchar(75),        
 @W9_FORM  nvarchar(10) ,
 @REGIONAL_IDENTIFICATION NVARCHAR(40),
 @DATE_OF_BIRTH DATETIME,
 @CPF NVARCHAR(40),
 @REG_ID_ISSUE_DATE DATETIME,
 @ACTIVITY INT,
 @REG_ID_ISSUE NVARCHAR(40),   
 @REQ_SPECIAL_HANDLING int = null        
)                                                  
AS                                                  
BEGIN                                             
                             
                     
 IF NOT EXISTS (SELECT EXPERT_SERVICE_VENDOR_CODE FROM CLM_EXPERT_SERVICE_PROVIDERS WHERE                     
  EXPERT_SERVICE_VENDOR_CODE = LTRIM(RTRIM(@EXPERT_SERVICE_VENDOR_CODE)))                    
 BEGIN                    
                    
  SELECT @EXPERT_SERVICE_ID=ISNULL(MAX(EXPERT_SERVICE_ID),0)+1 FROM CLM_EXPERT_SERVICE_PROVIDERS                            
                             
  INSERT INTO CLM_EXPERT_SERVICE_PROVIDERS                            
  (                            
  EXPERT_SERVICE_ID,                            
  EXPERT_SERVICE_TYPE,                            
  EXPERT_SERVICE_NAME,                            
  EXPERT_SERVICE_ADDRESS1,                          
  EXPERT_SERVICE_ADDRESS2,                            
  EXPERT_SERVICE_CITY,                            
  EXPERT_SERVICE_STATE,     
  EXPERT_SERVICE_ZIP,                            
  EXPERT_SERVICE_VENDOR_CODE,                            
  EXPERT_SERVICE_CONTACT_NAME,                
  EXPERT_SERVICE_CONTACT_PHONE,                            
  EXPERT_SERVICE_CONTACT_EMAIL,                            
  EXPERT_SERVICE_FEDRERAL_ID,                            
  EXPERT_SERVICE_1099_PROCESSING_OPTION,                           
  CREATED_BY,                            
  CREATED_DATETIME,                          
  IS_ACTIVE,                        
  EXPERT_SERVICE_COUNTRY,                  
  EXPERT_SERVICE_MASTER_VENDOR_CODE,          
  EXPERT_SERVICE_TYPE_DESC,          
  PARTY_DETAIL,          
  AGE,          
  EXTENT_OF_INJURY,          
  OTHER_DETAILS,          
  BANK_NAME,          
  ACCOUNT_NUMBER,          
  ACCOUNT_NAME,          
  EXPERT_SERVICE_CONTACT_PHONE_EXT,          
  EXPERT_SERVICE_CONTACT_FAX,          
  PARENT_ADJUSTER,        
  MAIL_1099_ADD1,        
  MAIL_1099_ADD2,        
  MAIL_1099_CITY,        
  MAIL_1099_STATE,        
  MAIL_1099_COUNTRY,        
  MAIL_1099_ZIP,          
  MAIL_1099_NAME,          
  W9_FORM ,  
  REQ_SPECIAL_HANDLING,
 REGIONAL_IDENTIFICATION,
 DATE_OF_BIRTH,
 CPF,
 REG_ID_ISSUE_DATE ,
 ACTIVITY,
 REG_ID_ISSUE           
  )                            
  VALUES                            
  (                            
  @EXPERT_SERVICE_ID,            
  @EXPERT_SERVICE_TYPE,                            
  @EXPERT_SERVICE_NAME,                            
  @EXPERT_SERVICE_ADDRESS1,                            
  @EXPERT_SERVICE_ADDRESS2,                            
  @EXPERT_SERVICE_CITY,                            
  @EXPERT_SERVICE_STATE,                            
  @EXPERT_SERVICE_ZIP,                            
  @EXPERT_SERVICE_VENDOR_CODE,                            
  @EXPERT_SERVICE_CONTACT_NAME,                            
  @EXPERT_SERVICE_CONTACT_PHONE,                            
  @EXPERT_SERVICE_CONTACT_EMAIL,                            
  @EXPERT_SERVICE_FEDRERAL_ID,                            
  @EXPERT_SERVICE_1099_PROCESSING_OPTION,                            
  @CREATED_BY,                         
  @CREATED_DATETIME,                          
  'Y',                        
  @EXPERT_SERVICE_COUNTRY,             
  @EXPERT_SERVICE_MASTER_VENDOR_CODE,          
  @EXPERT_SERVICE_TYPE_DESC,          
  @PARTY_DETAIL,          
  @AGE,          
  @EXTENT_OF_INJURY,          
  @OTHER_DETAILS,          
  @BANK_NAME,          
  @ACCOUNT_NUMBER,          
  @ACCOUNT_NAME,          
  @EXPERT_SERVICE_CONTACT_PHONE_EXT,          
  @EXPERT_SERVICE_CONTACT_FAX,          
  @PARENT_ADJUSTER,        
  @MAIL_1099_ADD1,        
  @MAIL_1099_ADD2,        
  @MAIL_1099_CITY,        
  @MAIL_1099_STATE,        
  @MAIL_1099_COUNTRY,        
  @MAIL_1099_ZIP,          
  @MAIL_1099_NAME,          
  @W9_FORM    ,  
  @REQ_SPECIAL_HANDLING,
  @REGIONAL_IDENTIFICATION,
 @DATE_OF_BIRTH,
 @CPF,
 @REG_ID_ISSUE_DATE ,
 @ACTIVITY,
 @REG_ID_ISSUE       
          
  )           
  RETURN 1                           
END ELSE                    
BEGIN                    
 RETURN -2                    
END                    
END                                        
          
          
          
          
          
        
  
GO

