IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


   
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
/*----------------------------------------------------------                                          
Proc Name      : dbo.Proc_UpdateAgency                                          
Created by       : Anurag Verma                                          
Date                   : 10/05/2005                                          
Purpose          : To update the data into Agency table.                                          
Revison History :                                          
Used In          : Wolverine                                      
                              
Modified by:                               
RPSINGH                               
11 May 2006                              
Do not check unique code for blank code (Itrack 1010)                                  
modify by       : Pravesh                                          
Date                   : 17/11/2006                                          
Purpose          : Add Two more Params and Update them (@TERMINATION_DATE_RENEW datetime, @TERMINATION_NOTICE char(1))                
                
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                                          
--drop proc dbo.Proc_UpdateAgency                                       
CREATE PROC dbo.Proc_UpdateAgency                                         
(                                          
  @AGENCY_CODE     nchar(8),                              
  @AGENCY_COMBINED_CODE     nvarchar(10),                                          
  @AGENCY_DISPLAY_NAME     nvarchar(140),                              
  @AGENCY_DBA      nvarchar(100),                       
  @AGENCY_ADD1     nvarchar(140),                                          
  @AGENCY_ADD2     nvarchar(140),                                          
  @AGENCY_CITY     nvarchar(80),                                          
  @AGENCY_STATE    nvarchar(10),                                          
  @AGENCY_ZIP      nvarchar(20),                                          
  @AGENCY_COUNTRY     nvarchar(10),                                          
  @AGENCY_PHONE     nvarchar(40),                                          
  @AGENCY_EXT     nvarchar(80),                                          
  @AGENCY_FAX     nvarchar(40),                                          
  @AGENCY_SPEED_DIAL int,                              
  @AGENCY_EMAIL     nvarchar(100),                                            
  @AGENCY_ID       int,                                          
  @AGENCY_WEBSITE varchar(200),                                    
  @AGENCY_PAYMENT_METHOD varchar(2),                                    
  @AGENCY_BILL_TYPE char(10),                                    
  @AGENCY_LIC_NUM int,                                    
  @MODIFIED_BY int,                                    
  @LAST_UPDATED_DATETIME datetime,                                    
  @PRINCIPAL_CONTACT varchar(50),                                    
  @OTHER_CONTACT VARCHAR (50),                                    
  @FEDERAL_ID VARCHAR(50),                                    
  @ORIGINAL_CONTRACT_DATE DATETIME,                      
  @CURRENT_CONTRACT_DATE DATETIME,                      
--@UNDERWRITER_ASSIGNED_AGENCY VARCHAR(250),                                    
  @BANK_ACCOUNT_NUMBER VARCHAR(100),                                    
  @ROUTING_NUMBER VARCHAR(100),                              
  @BANK_ACCOUNT_NUMBER1 VARCHAR(100),                                    
  @ROUTING_NUMBER1 VARCHAR(100),                              
  @BANK_NAME VARCHAR(20),                                    
  @BANK_BRANCH VARCHAR(20),        
 @BANK_NAME_2 VARCHAR(20),                            
  @BANK_BRANCH_2 VARCHAR(20),
  @AGENCY_TYPE_ID INT,                                     
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
-- new fields added by mohit.                                
  @TERMINATION_DATE datetime,                                
  @TERMINATION_REASON varchar(75),                                     
  @NOTES varchar(200),                              
  @NUM_AGENCY_CODE int,                      
  @AgencyName varchar(20),                      
-- end                                
 @TERMINATION_DATE_RENEW datetime,                   
 @TERMINATION_NOTICE char(1),           
 @INCORPORATED_LICENSE char(1),           
 @ALLOWS_EFT int,              
@ACCOUNT_TYPE varchar(4),        
@ACCOUNT_TYPE_2 varchar(4),        
@PROCESS_1099 int,        
@ALLOWS_CUSTOMER_SWEEP int = null,        
@REVERIFIED_AC1 int = null,        
@REVERIFIED_AC2 int = null  ,  
@REQ_SPECIAL_HANDLING int = null,  
--added by pradeep kushwaha  
@SUSEP_NUMBER nvarchar(20)=null ,       
  @BROKER_CURRENCY int  ,
  @BROKER_TYPE int ,
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
/*Check for Unique Code of Agency  if AGENCY CODE IS NOT BLANK*/                                        
if ltrim(rtrim(@AGENCY_CODE)) <> ''                               
Begin                              
 if exists (SELECT @AGENCY_CODE FROM MNT_Agency_LIST WHERE Agency_CODE = @AGENCY_CODE AND AGENCY_ID<>@AGENCY_ID)                                          
       RETURN   -1                                           
End                              
                              
declare @USER_LIC_NUM int                                    
                            
 SELECT @USER_LIC_NUM=ISNULL(COUNT(USER_ID),0)  FROM MNT_USER_LIST                                 
  WHERE USER_SYSTEM_ID=@AGENCY_CODE AND LIC_BRICS_USER=10963 AND IS_ACTIVE='Y'                                
                                
 IF(@USER_LIC_NUM > ISNULL(@AGENCY_LIC_NUM,0))                                
  RETURN -3                                
             
        
        
--Update Reverify flag on basis of chage        
DECLARE @NEW_ACCOUNT_ISVERIFIED1 INT        
DECLARE @OLD_BANK_ACCOUNT_NUMBER varchar(100)        
DECLARE @OLD_ROUTING_NUMBER  varchar(100)        
        
SET @NEW_ACCOUNT_ISVERIFIED1 = 10964 --NO        
        
SELECT          
@OLD_BANK_ACCOUNT_NUMBER = BANK_ACCOUNT_NUMBER ,        
@OLD_ROUTING_NUMBER = ROUTING_NUMBER         
FROM MNT_AGENCY_LIST with(nolock)        
WHERE AGENCY_ID = @AGENCY_ID            
        
--compare DFI_ACCOUNT_NUMBER and ROUTING_NUMBER        
IF((@OLD_BANK_ACCOUNT_NUMBER <> @BANK_ACCOUNT_NUMBER)        
OR (@OLD_ROUTING_NUMBER <> @ROUTING_NUMBER) )        
        
BEGIN        
 IF (@ALLOWS_EFT = 10963)        
 UPDATE MNT_AGENCY_LIST SET ACCOUNT_ISVERIFIED1 = @NEW_ACCOUNT_ISVERIFIED1 WHERE AGENCY_ID = @AGENCY_ID            
END        
        
--IS VERIFY 2 ACCOUNT        
DECLARE @NEW_ACCOUNT_ISVERIFIED2 INT        
DECLARE @OLD_BANK_ACCOUNT_NUMBER1 varchar(100)        
DECLARE @OLD_ROUTING_NUMBER1  varchar(100)        
        
        
SET @NEW_ACCOUNT_ISVERIFIED2 = 10964 --NO        
        
SELECT          
@OLD_BANK_ACCOUNT_NUMBER1 = BANK_ACCOUNT_NUMBER1 ,        
@OLD_ROUTING_NUMBER1 = ROUTING_NUMBER1         
FROM MNT_AGENCY_LIST with(nolock)        
WHERE AGENCY_ID = @AGENCY_ID            
        
--compare DFI_ACCOUNT_NUMBER and ROUTING_NUMBER        
IF((@OLD_BANK_ACCOUNT_NUMBER1 <> @BANK_ACCOUNT_NUMBER1)        
OR (@OLD_ROUTING_NUMBER1 <> @ROUTING_NUMBER1) )        
        
BEGIN        
 IF (@ALLOWS_CUSTOMER_SWEEP = 10963)        
 UPDATE MNT_AGENCY_LIST SET ACCOUNT_ISVERIFIED2 = @NEW_ACCOUNT_ISVERIFIED2 WHERE AGENCY_ID = @AGENCY_ID            
END        
-------------end                          
--If code reaches  here then udate record                              
                              
BEGIN                                       
  UPDATE MNT_Agency_LIST                                          
  SET                                     
  Agency_CODE=@AGENCY_CODE,                              
  Agency_COMBINED_CODE=@AGENCY_COMBINED_CODE,                                       
  Agency_Display_NAME=@AGENCY_DISPLAY_NAME,                              
  Agency_DBA=@AGENCY_DBA,                        
  Agency_ADD1=@AGENCY_ADD1,                                          
  Agency_ADD2=@AGENCY_ADD2,                                  
  Agency_CITY=@AGENCY_CITY,                                      
  Agency_STATE=@AGENCY_STATE,                                          
  Agency_ZIP=@AGENCY_ZIP,          
  Agency_COUNTRY=@AGENCY_COUNTRY,                                       
  Agency_PHONE=@AGENCY_PHONE,                                          
  Agency_EXT=@AGENCY_EXT,                                      
  Agency_FAX=@AGENCY_FAX,                                        
  Agency_SPEED_DIAL=@AGENCY_SPEED_DIAL,                              
  Agency_EMAIL=@AGENCY_EMAIL,                                                
  Agency_Website=@AGENCY_WEBSITE,                                    
  agency_payment_method=@AGENCY_PAYMENT_METHOD,                                    
  agency_bill_type=@AGENCY_BILL_TYPE,                                    
  AGENCY_LIC_NUM=@AGENCY_LIC_NUM,
  AGENCY_TYPE_ID=@AGENCY_TYPE_ID,                                    
                                      
  PRINCIPAL_CONTACT=@PRINCIPAL_CONTACT,                                    
  OTHER_CONTACT=@OTHER_CONTACT,                                
  FEDERAL_ID=@FEDERAL_ID,                                    
  ORIGINAL_CONTRACT_DATE=@ORIGINAL_CONTRACT_DATE,                       
  CURRENT_CONTRACT_DATE=@CURRENT_CONTRACT_DATE,                                      
  --UNDERWRITER_ASSIGNED_AGENCY=@UNDERWRITER_ASSIGNED_AGENCY,                                    
              
  BANK_ACCOUNT_NUMBER=@BANK_ACCOUNT_NUMBER,                                    
  ROUTING_NUMBER=@ROUTING_NUMBER,                                    
  BANK_ACCOUNT_NUMBER1=@BANK_ACCOUNT_NUMBER1,                                    
  ROUTING_NUMBER1=@ROUTING_NUMBER1,                              
  BANK_NAME=@BANK_NAME,                         
  BANK_BRANCH=@BANK_BRANCH,        
  BANK_NAME_2 =@BANK_NAME_2 ,            
  BANK_BRANCH_2 = @BANK_BRANCH_2 ,                              
            
  --new fields                                    
  M_AGENCY_ADD_1=@M_AGENCY_ADD_1,                                    
  M_AGENCY_ADD_2=@M_AGENCY_ADD_2,                                    
  M_AGENCY_CITY=@M_AGENCY_CITY,                                    
  M_AGENCY_COUNTRY=@M_AGENCY_COUNTRY,                                    
  M_AGENCY_STATE=@M_AGENCY_STATE,                                    
  M_AGENCY_ZIP=@M_AGENCY_ZIP,                      
  M_AGENCY_PHONE=@M_AGENCY_PHONE ,                                    
  M_AGENCY_EXT=@M_AGENCY_EXT,                                    
  M_AGENCY_FAX=@M_AGENCY_FAX,                                
  TERMINATION_DATE= @TERMINATION_DATE,                                
  TERMINATION_REASON=@TERMINATION_REASON,                                 
  NOTES=@NOTES,                              
  NUM_AGENCY_CODE=@NUM_AGENCY_CODE,                       
  AgencyName=@AgencyName,                      
  TERMINATION_DATE_RENEW= @TERMINATION_DATE_RENEW,         
  TERMINATION_NOTICE= @TERMINATION_NOTICE,        
  INCORPORATED_LICENSE=@INCORPORATED_LICENSE,                
  ALLOWS_EFT =@ALLOWS_EFT,          
  ACCOUNT_TYPE=@ACCOUNT_TYPE ,        
  ACCOUNT_TYPE_2 =@ACCOUNT_TYPE_2,        
  PROCESS_1099 =@PROCESS_1099,      
  ALLOWS_CUSTOMER_SWEEP =@ALLOWS_CUSTOMER_SWEEP ,        
  REVERIFIED_AC1 = @REVERIFIED_AC1,  
  REQ_SPECIAL_HANDLING = @REQ_SPECIAL_HANDLING ,         
  REVERIFIED_AC2 = @REVERIFIED_AC2,  
  SUSEP_NUMBER= @SUSEP_NUMBER,  
  BROKER_CURRENCY=@BROKER_CURRENCY  ,
  BROKER_TYPE=@BROKER_TYPE  ,
  BROKER_CPF_CNPJ= @BROKER_CPF_CNPJ,
    BROKER_DATE_OF_BIRTH=@BROKER_DATE_OF_BIRTH,
    BROKER_REGIONAL_ID=@BROKER_REGIONAL_ID,
    REGIONAL_ID_ISSUANCE=@REGIONAL_ID_ISSUANCE,
    REGIONAL_ID_ISSUE_DATE=@REGIONAL_ID_ISSUE_DATE,
    MARITAL_STATUS=@MARITAL_STATUS,
    GENDER=@GENDER,
    BROKER_BANK_NUMBER=	@BROKER_BANK_NUMBER,
    DISTRICT=@DISTRICT,
    NUMBER=@NUMBER           				
	                           
  WHERE                              
  AGENCY_ID=@AGENCY_ID                                             
END                                  
                              
return 1                          
end                                
        
        
        
        
        
        
        
        
        
        
      
        
      
    
  
  
  
  

GO

