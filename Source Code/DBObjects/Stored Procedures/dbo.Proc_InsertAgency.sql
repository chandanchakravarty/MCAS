IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


   
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
/*----------------------------------------------------------                                
Proc Name     : dbo.Proc_InsertAgency                                
Created by      : Anurag Verma                                
Date            : 14/03/2005                                
Purpose         : To insert the data into Agency table.                                
Revison History :                                
Used In         : Wolverine                         
                      
Modified by:                       
RPSINGH                       
11 May 2006                      
Do not check unique code for blank code (Itrack 1010)                      
                        
Modified by:  Pravesh                
date       : 17 Nov 2006                      
Purpose : add 2 More Parameters TERMINATION_DATE_RENEW datetime, @TERMINATION_NOTICE char(1)                 
                               
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--drop proc dbo.Proc_InsertAgency                      
CREATE PROC [dbo].[Proc_InsertAgency]                                
(                                
                               
  @AGENCY_CODE     nchar(8),                                
  @AGENCY_COMBINED_CODE nvarchar(10),                      
  @AGENCY_DISPLAY_NAME     nvarchar(140),                                
  @AGENCY_DBA      nvarchar(100),                      
  @AGENCY_ADD1     nvarchar(140),                                
  @AGENCY_ADD2     nvarchar(140),                                
  @AGENCY_CITY     nvarchar(80),                                
  @AGENCY_STATE    nvarchar(10),                                
  @AGENCY_ZIP      nvarchar(20),                                
  @AGENCY_COUNTRY  nvarchar(10),                                
  @AGENCY_PHONE    nvarchar(40),                                
  @AGENCY_EXT      nvarchar(80),                                
  @AGENCY_FAX      nvarchar(40),                      
  @AGENCY_SPEED_DIAL    int,                               
  @AGENCY_EMAIL    nvarchar(100),                                
  @CREATED_BY      int,                                
  @CREATED_DATETIME datetime,                                
  @AGENCY_ID       int output,                                
  @AGENCY_WEBSITE  varchar(200),                          
  @AGENCY_PAYMENT_METHOD varchar(2),                          
  @AGENCY_BILL_TYPE char(10),                          
  @AGENCY_LIC_NUM int,                          
                            
  @PRINCIPAL_CONTACT varchar(50),                          
  @OTHER_CONTACT VARCHAR (50),                          
  @FEDERAL_ID VARCHAR(50),                          
  @ORIGINAL_CONTRACT_DATE DATETIME,                          
--@UNDERWRITER_ASSIGNED_AGENCY VARCHAR(250),                          
  @BANK_ACCOUNT_NUMBER VARCHAR(100),                          
  @ROUTING_NUMBER VARCHAR(100),                      
  @BANK_ACCOUNT_NUMBER1 VARCHAR(100),                          
  @ROUTING_NUMBER1 VARCHAR(100),                      
  @BANK_NAME VARCHAR(20),                            
  @BANK_BRANCH VARCHAR(20),           
  @BANK_NAME_2 VARCHAR(20),                          
  @BANK_BRANCH_2 VARCHAR(20),
                           
                          
--new fields                           
                          
  @M_AGENCY_ADD_1 nvarchar(70) ,                          
  @M_AGENCY_ADD_2 nvarchar(70),                          
  @M_AGENCY_CITY  nvarchar(40),                          
  @M_AGENCY_COUNTRY nvarchar(5),                          
  @M_AGENCY_STATE nvarchar(5),                          
  @M_AGENCY_ZIP varchar(11),                          
  @M_AGENCY_PHONE nvarchar(20)=null,                          
  @M_AGENCY_EXT nvarchar(5)=null,                          
 @M_AGENCY_FAX nvarchar(20)=null,                        
-- New fields added by mohit on 2/11/2005                        
  @TERMINATION_DATE datetime,                        
  @TERMINATION_REASON varchar(75),                        
  @NOTES varchar(200),                      
  @NUM_AGENCY_CODE int,                 
  @AgencyName nvarchar(20),                    
  @CURRENT_CONTRACT_DATE datetime,                    
--end                    
  @TERMINATION_DATE_RENEW datetime,                        
  @TERMINATION_NOTICE char(1),              
  @INCORPORATED_LICENSE char(1),              
  @ALLOWS_EFT int,                
  @ACCOUNT_TYPE varchar(4),          
  @ACCOUNT_TYPE_2 varchar(4) ,        
  @PROCESS_1099 int ,        
  @ALLOWS_CUSTOMER_SWEEP int = null    ,        
  @REVERIFIED_AC1 int = null,              
  @REVERIFIED_AC2 int = null,  
  @REQ_SPECIAL_HANDLING int = null ,  
  --Added by pradeep kushwaha  
  @SUSEP_NUMBER nvarchar(20) =null, 
  @BROKER_CURRENCY int,
  @AGENCY_TYPE_ID int ,
  @BROKER_TYPE int  ,  
 	@BROKER_CPF_CNPJ nvarchar(40),
	@BROKER_DATE_OF_BIRTH datetime,
	@BROKER_REGIONAL_ID nvarchar(40),
	@REGIONAL_ID_ISSUANCE nvarchar(40),
	@REGIONAL_ID_ISSUE_DATE datetime,
	@MARITAL_STATUS int=null,
	@GENDER int=null,
	@BROKER_BANK_NUMBER nvarchar(20),
	@DISTRICT nvarchar(50),
	@NUMBER nvarchar(40)
          
)                                
AS                                
BEGIN                              
 /*Check for Unique Code of Agency  */                              
 Declare @Count numeric                              
 Declare @NCount numeric                           
/*Check for Unique Code of Agency  */                                               
--Condition added by RPSINGH - 11 may 2006                       
  IF  LTRIM(RTRIM(@AGENCY_CODE)) <> ''                      
   Begin                      
    SELECT @Count = Count(@AGENCY_CODE)FROM MNT_Agency_LIST WHERE Agency_CODE = @AGENCY_CODE                      
   End                      
  else                      
   Begin                      
    SELECT @Count = 0                      
   End                      
                       
 IF @Count >= 1                               
 BEGIN                                      
   SELECT @AGENCY_ID = -1                            
 END                         
 ELSE                             
                      
  BEGIN                             
   INSERT INTO MNT_Agency_LIST                                
   (                               
                                
  Agency_CODE,                      
  Agency_COMBINED_CODE,                          
  Agency_Display_NAME,                      
  Agency_DBA,                           
  Agency_ADD1,                                
  Agency_ADD2,                           
  Agency_CITY,                            
  Agency_STATE,                                
  Agency_ZIP,                              
  Agency_COUNTRY,                             
  Agency_PHONE,                                
  Agency_EXT,                            
  Agency_FAX,                              
  Agency_SPEED_DIAL,                      
  Agency_EMAIL,                                
  CREATED_BY,                           
  CREATED_DATETIME,                          
  Agency_Website,                          
  agency_payment_method,                          
  agency_bill_type,                          
  AGENCY_LIC_NUM,                          
  IS_ACTIVE,                          
  PRINCIPAL_CONTACT,                          
  OTHER_CONTACT,                          
  FEDERAL_ID,                          
  ORIGINAL_CONTRACT_DATE,                          
  --UNDERWRITER_ASSIGNED_AGENCY,                      
  BANK_ACCOUNT_NUMBER,                          
  ROUTING_NUMBER,                          
  BANK_ACCOUNT_NUMBER1,                          
  ROUTING_NUMBER1,                      
  BANK_NAME,                          
  BANK_BRANCH,          
  BANK_NAME_2,                          
  BANK_BRANCH_2,                          
                          
  --new fields            
  M_AGENCY_ADD_1,                          
  M_AGENCY_ADD_2,                          
  M_AGENCY_CITY,                          
  M_AGENCY_COUNTRY,                          
  M_AGENCY_STATE,                          
  M_AGENCY_ZIP,                          
  M_AGENCY_PHONE,                          
  M_AGENCY_EXT,                          
  M_AGENCY_FAX,                        
  TERMINATION_DATE,                        
  TERMINATION_REASON,                      
  NOTES,                      
  NUM_AGENCY_CODE,                    
  AgencyName,                    
  CURRENT_CONTRACT_DATE,                       
  TERMINATION_DATE_RENEW,                        
  TERMINATION_NOTICE,         
  INCORPORATED_LICENSE,             
  ALLOWS_EFT,            
  ACCOUNT_TYPE,          
  ACCOUNT_TYPE_2 ,        
  PROCESS_1099 ,         
  ALLOWS_CUSTOMER_SWEEP ,        
  REVERIFIED_AC1,        
  REVERIFIED_AC2,  
  REQ_SPECIAL_HANDLING,  
  SUSEP_NUMBER ,       
  BROKER_CURRENCY,
  AGENCY_TYPE_ID ,
  BROKER_TYPE , 
    BROKER_CPF_CNPJ,
    BROKER_DATE_OF_BIRTH,
    BROKER_REGIONAL_ID,
    REGIONAL_ID_ISSUANCE,
    REGIONAL_ID_ISSUE_DATE,
    MARITAL_STATUS,
    GENDER,
    BROKER_BANK_NUMBER,
    DISTRICT,
    NUMBER
       
   )                                
   VALUES                                
   (                                
  @AGENCY_CODE,                      
  @AGENCY_COMBINED_CODE,                            
  @AGENCY_DISPLAY_NAME,                      
  @AGENCY_DBA,                            
  @AGENCY_ADD1,                                
  @AGENCY_ADD2,                           
  @AGENCY_CITY,                        
  @AGENCY_STATE,                                
  @AGENCY_ZIP,                             
  @AGENCY_COUNTRY,                           
  @AGENCY_PHONE,               
  @AGENCY_EXT,                            
  @AGENCY_FAX,                             
  @AGENCY_SPEED_DIAL,                      
  @AGENCY_EMAIL,                                
  @CREATED_BY,                           
  @CREATED_DATETIME,                          
  @AGENCY_WEBSITE,                 
  @AGENCY_PAYMENT_METHOD,                          
  @AGENCY_BILL_TYPE,                          
  @AGENCY_LIC_NUM,                          
  'Y',                          
  @PRINCIPAL_CONTACT,                          
  @OTHER_CONTACT,                          
  @FEDERAL_ID,                          
  @ORIGINAL_CONTRACT_DATE ,                          
  --@UNDERWRITER_ASSIGNED_AGENCY,                          
  @BANK_ACCOUNT_NUMBER,                          
  @ROUTING_NUMBER,                      
  @BANK_ACCOUNT_NUMBER1,                          
  @ROUTING_NUMBER1,                      
  @BANK_NAME,                          
  @BANK_BRANCH,           
  @BANK_NAME_2,                          
  @BANK_BRANCH_2,                 
                          
  --new fields                        
  @M_AGENCY_ADD_1,                          
  @M_AGENCY_ADD_2,                          
  @M_AGENCY_CITY,                          
  @M_AGENCY_COUNTRY,                          
  @M_AGENCY_STATE,                          
  @M_AGENCY_ZIP,                          
  @M_AGENCY_PHONE ,                          
  @M_AGENCY_EXT,                          
  @M_AGENCY_FAX,                        
  @TERMINATION_DATE,                        
  @TERMINATION_REASON,                      
  @NOTES,                      
  @NUM_AGENCY_CODE,                    
  @AgencyName,                    
  @CURRENT_CONTRACT_DATE,                          
  @TERMINATION_DATE_RENEW,                        
  @TERMINATION_NOTICE,        
  @INCORPORATED_LICENSE,              
  @ALLOWS_EFT,            
  @ACCOUNT_TYPE,          
  @ACCOUNT_TYPE_2,        
  @PROCESS_1099 ,        
  @ALLOWS_CUSTOMER_SWEEP,        
  @REVERIFIED_AC1,        
  @REVERIFIED_AC2,  
  @REQ_SPECIAL_HANDLING,  
  @SUSEP_NUMBER,
  @BROKER_CURRENCY,
  @AGENCY_TYPE_ID ,
  @BROKER_TYPE ,
	  @BROKER_CPF_CNPJ,
	@BROKER_DATE_OF_BIRTH,
	@BROKER_REGIONAL_ID,
	@REGIONAL_ID_ISSUANCE,
	@REGIONAL_ID_ISSUE_DATE,
	@MARITAL_STATUS,
	@GENDER,
	@BROKER_BANK_NUMBER,
	@DISTRICT,
	@NUMBER
             
 )                                
    SELECT @Agency_ID = Max(Agency_ID) FROM MNT_Agency_LIST                            
     END                              
                              
END                        
        
        
        
        
        
        
        
        
        
      
  

GO

