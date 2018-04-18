IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomerInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                  
Proc Name    : dbo.Proc_UpdateCustomer                                                  
Created by   : Shrikant Bhatt                                                  
Date         : 5 May.,2005                                                 
Purpose     : Insert the record into Customers Table                                                
Revison History :                                              
Used In  :   Wolverine                                                     
 ------------------------------------------------------------                                                              
Date     Review By          Comments                                                            
MODIFIED BY : Vijay                                              
MODIFY ON  : 23 May,2005                                              
PURPOSE  : To avoid duplcate customer code                                                   
                                              
MODIFIED BY : Vijay                                              
MODIFY ON  : 2 June,2005                                              
PURPOSE  : To increase length of city                                                 
                                              
MODIFIED BY     : MOHIT                                              
MODIFIED ON     : 7/10/2005                                              
PURPOSE         : UPDATING CUSTOMER DATA CLT_APPLICANT_LIST TABLE.                                             
                                            
Modified By     : Mohit                                            
Modified On     : 4/11/2005                                            
Purpopse        : adding field "DESC_APPLICANT_OCCU" in update statement.                                            
Modified By     : Sumit Chhabra                                          
Modified On     : 11/11/2005                                            
Purpopse        : Check for duplicate customer code has been done away with                                          
                                
Modified By     : Pravesh Chandel                                            
Modified On     : 22 Nov 2006                                            
Purpopse        : to update insurance score in app list and policy list.                       
              
Modified By     : Pravesh Chandel                                            
Modified On     : 28 July 2009                                            
Purpopse        : to update HOme Coverages if Occupation changes (Itrack 6179)            
                                         
Review by Anurag Verma on 18-06-2007                    
------   ------------       -------------------------*/                                             
--drop proc dbo.Proc_UpdateCustomerInfo                                                  
CREATE PROC [dbo].[Proc_UpdateCustomerInfo]                                              
(                                                
 @CUSTOMER_ID     int ,                                              
 @CUSTOMER_CODE    nvarchar(10) ,                                              
 @CUSTOMER_TYPE    nvarchar(6)  ,                                              
 @CUSTOMER_PARENT   int   ,                                              
 @CUSTOMER_SUFFIX   nvarchar(5)  ,                                              
 @CUSTOMER_FIRST_NAME   nvarchar(200)  ,                                              
 @CUSTOMER_MIDDLE_NAME   nvarchar(10)  ,                                              
 @CUSTOMER_LAST_NAME   nvarchar(25)  ,                                              
 @CUSTOMER_ADDRESS1   nvarchar(150)  ,                                              
 @CUSTOMER_ADDRESS2   nvarchar(150)  ,                                              
 @CUSTOMER_CITY    nvarchar(70)  ,     
 @CUSTOMER_COUNTRY   nvarchar(35)  ,                                                
 @CUSTOMER_STATE   nvarchar(20)  ,                                              
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
 @CUSTOMER_REASON_CODE   nvarchar (10)  ,                                         
 @CUSTOMER_REASON_CODE2    nvarchar (10)  ,                                              
 @CUSTOMER_REASON_CODE3    nvarchar (10)  ,                                           
 @CUSTOMER_REASON_CODE4    nvarchar (10)  ,                                              
--@CustomerProducerId   NVARCHAR(30),                            
--@CustomerLateCharges   NVARCHAR(30),                                                
--@CustomerLateNotices   NVARCHAR(1),                                                
--@CustomerAccountExecutiveId  NVARCHAR(30),                                                
--@CustomerSendStatement  NVARCHAR(1),                                                
--@CustomerReceivableDueDays  NVARCHAR(4),                                                
--@CustomerCsr    NVARCHAR(30),                                                
--@CustomerReferredBy   NVARCHAR(25),                               
 @CUSTOMER_AGENCY_ID   int,                                              
                                              
 @PREFIX     int ,                                      
 @PER_CUST_MOBILE nvarchar(15),                                              
 @MODIFIED_BY    int  ,                                              
 @MODIFIED_DATETIME   datetime ,                                              
@APPLICANT_OCCU INT =NULL,                                              
@EMPLOYER_NAME nvarchar(75) =null,                                              
@EMPLOYER_ADDRESS nvarchar(75)=null,                                              
@YEARS_WITH_CURR_EMPL real =null,                                              
@SSN_NO nvarchar(44) =null,                                              
@MARITAL_STATUS nchar(1) =null,                                              
@DATE_OF_BIRTH datetime =null,                                            
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
@GENDER nchar(1),  --Added By Lalit            
          
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
@ID_TYPE NVARCHAR(200),
@MONTHLY_INCOME DECIMAL(10,2),
@AMOUNT_TYPE INT,
@CADEMP NVARCHAR(200),
@NET_ASSETS_AMOUNT DECIMAL(10,2),
@NATIONALITY NVARCHAR(100),
@EMAIL_ADDRESS NVARCHAR(100),   
@REGIONAL_IDENTIFICATION_TYPE NVARCHAR(40),
@IS_POLITICALLY_EXPOSED CHAR(2),
@BANK_NAME NVARCHAR(100),
@BANK_NUMBER NVARCHAR(5),
@BANK_BRANCH NVARCHAR(10),
@ACCOUNT_NUMBER NVARCHAR(20),
@ACCOUNT_TYPE INT  ,
@New_Cust_Id int =null out
                                   
--@ALT_CUSTOMER_ADDRESS1   nvarchar(150)  ,                                              
--@ALT_CUSTOMER_ADDRESS2   nvarchar(150)  ,                                              
--@ALT_CUSTOMER_CITY    nvarchar(70)  ,                                              
--@ALT_CUSTOMER_COUNTRY   nvarchar(40)  ,                                                
--@ALT_CUSTOMER_STATE    nvarchar(40)  ,                                              
--@ALT_CUSTOMER_ZIP    nvarchar(12)                                                 
)                                              
AS                                           
BEGIN                                        
 /*Checking for the duplicacy of code */                                          
 /*Nov 11,2005:Sumit Chhabra:Check for duplicate customer code has been done away with   */                                        
--  IF (@CUSTOMER_TYPE <>'11110' )
--  BEGIN                        
-- IF NOT EXISTS (SELECT INDIVIDUAL_CONTACT_ID FROM MNT_CONTACT_LIST  with(nolock) WHERE INDIVIDUAL_CONTACT_ID = @CUSTOMER_ID  AND IS_ACTIVE = 'Y')
-- BEGIN
--  SET @New_Cust_Id = -2
--return
-- END 
-- END 
                  
IF EXISTS(SELECT CUSTOMER_CODE FROM CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_CODE = @CUSTOMER_CODE AND CUSTOMER_ID <> @CUSTOMER_ID)
BEGIN
 SET @New_Cust_Id = -1
   RETURN
END
ELSE 
 BEGIN                                             
  /*Updating the record*/                                               
  UPDATE CLT_CUSTOMER_LIST                                              
  SET                                                  
   CUSTOMER_CODE    =@CUSTOMER_CODE,                                              
   CUSTOMER_TYPE    =@CUSTOMER_TYPE,                                            
   CUSTOMER_PARENT    =@CUSTOMER_PARENT,                                              
   CUSTOMER_SUFFIX    =@CUSTOMER_SUFFIX,                                              
   CUSTOMER_FIRST_NAME   =@CUSTOMER_FIRST_NAME,                       
   CUSTOMER_MIDDLE_NAME   =@CUSTOMER_MIDDLE_NAME,                                              
   CUSTOMER_LAST_NAME   =@CUSTOMER_LAST_NAME,                                              
   CUSTOMER_ADDRESS1   =@CUSTOMER_ADDRESS1,                                              
   CUSTOMER_ADDRESS2   =@CUSTOMER_ADDRESS2,                                              
   CUSTOMER_CITY    =@CUSTOMER_CITY ,                                              
   CUSTOMER_COUNTRY   =@CUSTOMER_COUNTRY,                                              
   CUSTOMER_STATE    =@CUSTOMER_STATE,                                               
CUSTOMER_ZIP    =@CUSTOMER_ZIP,                                               
   CUSTOMER_BUSINESS_TYPE   =@CUSTOMER_BUSINESS_TYPE,                                              
   CUSTOMER_BUSINESS_DESC   =@CUSTOMER_BUSINESS_DESC,                                               
   CUSTOMER_CONTACT_NAME   =@CUSTOMER_CONTACT_NAME,                                               
   CUSTOMER_BUSINESS_PHONE   =@CUSTOMER_BUSINESS_PHONE,                                              
   CUSTOMER_EXT    =@CUSTOMER_EXT,                                      
   EMP_EXT    =@EMP_EXT,                                                 
   CUSTOMER_HOME_PHONE   =@CUSTOMER_HOME_PHONE,                                                
   CUSTOMER_MOBILE  =@CUSTOMER_MOBILE,                                                
   CUSTOMER_FAX    =@CUSTOMER_FAX,                                                 
   CUSTOMER_PAGER_NO   =@CUSTOMER_PAGER_NO,                                                
   CUSTOMER_Email    =@CUSTOMER_Email,                                                
   CUSTOMER_WEBSITE   =@CUSTOMER_WEBSITE,                                         
   CUSTOMER_INSURANCE_SCORE  =@CUSTOMER_INSURANCE_SCORE,                                               
   CUSTOMER_INSURANCE_RECEIVED_DATE =@CUSTOMER_INSURANCE_RECEIVED_DATE,                    
   LAST_INSURANCE_SCORE_FETCHED  = @CUSTOMER_INSURANCE_RECEIVED_DATE,                                             
   CUSTOMER_REASON_CODE   =@CUSTOMER_REASON_CODE,                                          
   CUSTOMER_REASON_CODE2   =@CUSTOMER_REASON_CODE2,                                              
   CUSTOMER_REASON_CODE3   =@CUSTOMER_REASON_CODE3,                                              
   CUSTOMER_REASON_CODE4   =@CUSTOMER_REASON_CODE4,                                              
--CUSTOMER_PRODUCER_ID=@CustomerProducerId,                                              
--CUSTOMER_ACCOUNT_EXECUTIVE_ID=@CustomerAccountExecutiveId,                                              
--CUSTOMER_CSR =@CustomerCsr,                                              
--CUSTOMER_LATE_CHARGES,                                              
--CUSTOMER_LATE_NOTICES,                                              
--CUSTOMER_SEND_STATEMENT,                                              
--CUSTOMER_RECEIVABLE_DUE_DAYS,                                              
--CUSTOMER_REFERRED_BY=@CustomerReferredBy,                                        
CUSTOMER_AGENCY_ID=@CUSTOMER_AGENCY_ID,                                              
   PREFIX     =@PREFIX,                                      
   PER_CUST_MOBILE =@PER_CUST_MOBILE,                                               
   MODIFIED_BY    =@MODIFIED_BY,                                               
   LAST_UPDATED_DATETIME   =@MODIFIED_DATETIME,                                              
APPLICANT_OCCU=@APPLICANT_OCCU,                                              
EMPLOYER_NAME=@EMPLOYER_NAME,                                              
EMPLOYER_ADDRESS =@EMPLOYER_ADDRESS,                                              
YEARS_WITH_CURR_EMPL=@YEARS_WITH_CURR_EMPL,                                              
SSN_NO =@SSN_NO,                                              
MARITAL_STATUS =@MARITAL_STATUS ,                                              
DATE_OF_BIRTH=@DATE_OF_BIRTH,                                            
DESC_APPLICANT_OCCU=@DESC_APPLICANT_OCCU,                                        
EMPLOYER_ADD1 = @EMPLOYER_ADD1,                                        
EMPLOYER_ADD2 = @EMPLOYER_ADD2,                                        
EMPLOYER_CITY = @EMPLOYER_CITY,                                        
EMPLOYER_COUNTRY = @EMPLOYER_COUNTRY,                                   
EMPLOYER_STATE = @EMPLOYER_STATE,                                        
EMPLOYER_ZIPCODE = @EMPLOYER_ZIPCODE,                                        
EMPLOYER_HOMEPHONE = @EMPLOYER_HOMEPHONE,                                        
EMPLOYER_EMAIL = @EMPLOYER_EMAIL,                                        
YEARS_WITH_CURR_OCCU = @YEARS_WITH_CURR_OCCU,                     
GENDER = @GENDER,            
CPF_CNPJ=@CPF_CNPJ,                    
NUMBER=@NUMBER,                    
        
DISTRICT=@DISTRICT,                    
         
MAIN_TITLE=@MAIN_TITLE,                     
MAIN_POSITION=@MAIN_POSITION,                     
MAIN_CPF_CNPJ=@MAIN_CPF_CNPJ,                     
MAIN_ADDRESS=@MAIN_ADDRESS,               
MAIN_NUMBER=@MAIN_NUMBER,                     
MAIN_COMPLIMENT=@MAIN_COMPLIMENT,                     
MAIN_DISTRICT=@MAIN_DISTRICT,                     
MAIN_NOTE=@MAIN_NOTE,                  
MAIN_CONTACT_CODE=@MAIN_CONTACT_CODE,                
REGIONAL_IDENTIFICATION=@REGIONAL_IDENTIFICATION,                
REG_ID_ISSUE=@REG_ID_ISSUE,                
ORIGINAL_ISSUE=@ORIGINAL_ISSUE,              
MAIN_ZIPCODE=@MAIN_ZIPCODE ,              
MAIN_CITY =@MAIN_CITY,              
MAIN_COUNTRY=@MAIN_COUNTRY ,              
MAIN_STATE=@MAIN_STATE ,          
MAIN_FIRST_NAME=@MAIN_FIRST_NAME,          
MAIN_MIDDLE_NAME=@MAIN_MIDDLE_NAME,          
MAIN_LAST_NAME=@MAIN_LAST_NAME,
ID_TYPE = @ID_TYPE,
MONTHLY_INCOME=@MONTHLY_INCOME,
AMOUNT_TYPE=@AMOUNT_TYPE,
CADEMP=@CADEMP,
NET_ASSETS_AMOUNT=@NET_ASSETS_AMOUNT,
NATIONALITY=@NATIONALITY,
EMAIL_ADDRESS=@EMAIL_ADDRESS,   
REGIONAL_IDENTIFICATION_TYPE=@REGIONAL_IDENTIFICATION_TYPE,
IS_POLITICALLY_EXPOSED = @IS_POLITICALLY_EXPOSED              
                                 
--ALT_CUSTOMER_ADDRESS1   =@ALT_CUSTOMER_ADDRESS1,                                              
--   ALT_CUSTOMER_ADDRESS2   =@ALT_CUSTOMER_ADDRESS2,                                              
 --  ALT_CUSTOMER_CITY    =@ALT_CUSTOMER_CITY ,                                              
  -- ALT_CUSTOMER_COUNTRY   =@ALT_CUSTOMER_COUNTRY,                                              
  -- ALT_CUSTOMER_STATE    =@ALT_CUSTOMER_STATE,                                               
  -- ALT_CUSTOMER_ZIP    =@ALT_CUSTOMER_ZIP                                       
WHERE CUSTOMER_ID = @CUSTOMER_ID                                                
                                              
                                        
--Added by Mohit on 7/10/2005                                              
--updatating clt_applicant_list.                                              
-- updation is only for personal and onlt for the primary applicant which is default customer.                                              
-- we can't change primary status of personal type user.                              
--11110                                                 
                                               
                                               
--if(@CUSTOMER_TYPE = 11110)                                              
--begin                                               
                                               
-- declare @APPLICANT_ID int                                                
-- select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST                                                 
DECLARE @COUNT INT                                              
                                              
--FINDING WHETHER CUSTOMER EXISTS IN THE CLT_APPLICANT_LIST.                                              
SELECT @COUNT=COUNT(*) FROM clt_applicant_list WITH(NOLOCK) where CUSTOMER_ID=@CUSTOMER_ID AND IS_PRIMARY_APPLICANT=1                                              
                                              
IF (@COUNT > 0)                                     
BEGIN                                   
--ADDED BY PRAVESH ON 29 JULY 09              
DECLARE @OLD_CO_APPLI_OCCU NVARCHAR(8),@APPLICANT_ID INT              
SELECT @OLD_CO_APPLI_OCCU=ISNULL(CO_APPLI_OCCU,''),@APPLICANT_ID=APPLICANT_ID FROM CLT_APPLICANT_LIST   WITH(NOLOCK)            
WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_PRIMARY_APPLICANT=1                                                
-- END HERE              
UPDATE CLT_APPLICANT_LIST                                              
SET      
                                           
TITLE=@PREFIX,SUFFIX=@CUSTOMER_SUFFIX,                                              
FIRST_NAME=@CUSTOMER_FIRST_NAME,                                              
MIDDLE_NAME=@CUSTOMER_MIDDLE_NAME,                                              
LAST_NAME=@CUSTOMER_LAST_NAME,                                              
ADDRESS1=@CUSTOMER_ADDRESS1,                                              
ADDRESS2=@CUSTOMER_ADDRESS2,                                              
CITY=@CUSTOMER_CITY,                                              
COUNTRY=@CUSTOMER_COUNTRY,                                              
STATE=@CUSTOMER_STATE,                      
ZIP_CODE=@CUSTOMER_ZIP,PHONE=@CUSTOMER_HOME_PHONE,                                              
EMAIL=@CUSTOMER_Email,                                              
CO_APPL_MARITAL_STATUS=@MARITAL_STATUS,                                              
CO_APPL_SSN_NO=@SSN_NO,                                              
CO_APPL_DOB=@DATE_OF_BIRTH,                                              
MODIFIED_BY=@MODIFIED_BY,                                               
LAST_UPDATED_TIME=@MODIFIED_DATETIME,                                              
CO_APPLI_OCCU=@APPLICANT_OCCU,                                              
CO_APPLI_EMPL_NAME=@EMPLOYER_NAME,                                              
CO_APPLI_EMPL_ADDRESS=@EMPLOYER_ADD1,                   
CO_APPLI_EMPL_ADDRESS1=@EMPLOYER_ADD2,                                              
CO_APPLI_YEARS_WITH_CURR_EMPL=@YEARS_WITH_CURR_EMPL,                                            
DESC_CO_APPLI_OCCU=@DESC_APPLICANT_OCCU,  -- added by mohit on 4/11/2005                                            
CO_APPL_YEAR_CURR_OCCU=@YEARS_WITH_CURR_OCCU, --Added by Sumit on 10/04/2006                      
CO_APPLI_EMPL_CITY = @EMPLOYER_CITY ,                                      
CO_APPLI_EMPL_COUNTRY = @EMPLOYER_COUNTRY,                                      
CO_APPLI_EMPL_STATE = @EMPLOYER_STATE,                                      
CO_APPLI_EMPL_ZIP_CODE = @EMPLOYER_ZIPCODE,                                      
CO_APPLI_EMPL_PHONE = @EMPLOYER_HOMEPHONE,                                      
CO_APPLI_EMPL_EMAIL = @EMPLOYER_EMAIL,      
MOBILE =@PER_CUST_MOBILE,                                      
EMP_EXT    =@EMP_EXT ,              
CO_APPL_GENDER = @GENDER,      
NUMBER=@NUMBER, --Added By Lalit April 07,2010      
DISTRICT=@DISTRICT,      
APPLICANT_TYPE=@CUSTOMER_TYPE,      
POSITION=@MAIN_POSITION,      
REG_ID_ISSUE=@REG_ID_ISSUE,      
ORIGINAL_ISSUE=@ORIGINAL_ISSUE,      
REGIONAL_IDENTIFICATION=@REGIONAL_IDENTIFICATION,      
NOTE=@MAIN_NOTE,    
CONTACT_CODE=@CUSTOMER_CODE ,
BANK_NAME = @BANK_NAME,
BANK_NUMBER = @BANK_NUMBER,
BANK_BRANCH = @BANK_BRANCH,
ACCOUNT_NUMBER = @ACCOUNT_NUMBER,
ACCOUNT_TYPE = @ACCOUNT_TYPE        
  --ADDED BY PRAVEEN KUMAR(17-03-2009):ITRACK 5580                                     
WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_PRIMARY_APPLICANT=1                                          
              
-- added by pravesh k Chandel on  28 july 09 Itrack 6179 ( UPDATE HOME COVG              
if (ISNULL(@OLD_CO_APPLI_OCCU,'') <> @APPLICANT_OCCU)              
 BEGIN              
 -- updating app  coverages              
 EXEC dbo.PROC_UPDATE_HOME_COVERAGE_BY_APPLICANT @CUSTOMER_ID ,@APPLICANT_ID              
 -- updating pol coverages              
 EXEC dbo.PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT @CUSTOMER_ID ,@APPLICANT_ID              
 END              
---- END HERE              
END                                  
--END                                              
 /*  comment by pravesh as this code moved to as proc                             
---- BY PRAVESH  UPDATE APPLY INSURANCE SCORE IN APP LIST FOR IMCOMPLETE APPLICATION                                
IF EXISTS (SELECT APP_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND UPPER(APP_STATUS)='INCOMPLETE')                                 
 BEGIN                                
 UPDATE APP_LIST SET APPLY_INSURANCE_SCORE = @CUSTOMER_INSURANCE_SCORE WHERE CUSTOMER_ID=@CUSTOMER_ID AND UPPER(APP_STATUS)='INCOMPLETE'                                
 END                                
--FOR POLICY                                
IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND UPPER(POLICY_STATUS)='SUSPENDED')                                
 BEGIN                                
 UPDATE POL_CUSTOMER_POLICY_LIST SET APPLY_INSURANCE_SCORE = @CUSTOMER_INSURANCE_SCORE WHERE CUSTOMER_ID=@CUSTOMER_ID AND UPPER(POLICY_STATUS)='SUSPENDED'                                
 END                                
IF EXISTS (                                
 SELECT PLL.POLICY_ID FROM POL_CUSTOMER_POLICY_LIST PLL    
 INNER JOIN POL_POLICY_PROCESS PLP ON PLL.CUSTOMER_ID=PLP.CUSTOMER_ID                                
 AND PLL.POLICY_ID=PLP.POLICY_ID AND PLL.POLICY_VERSION_ID=PLP.POLICY_VERSION_ID                                
 WHERE PLL.CUSTOMER_ID=@CUSTOMER_ID                                 
        )                                
 BEGIN                                
 UPDATE POL_CUSTOMER_POLICY_LIST SET APPLY_INSURANCE_SCORE = @CUSTOMER_INSURANCE_SCORE                                 
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID IN                                 
   (                                
   SELECT PLL.POLICY_ID FROM POL_CUSTOMER_POLICY_LIST PLL                                
   INNER JOIN POL_POLICY_PROCESS PLP ON PLL.CUSTOMER_ID=PLP.CUSTOMER_ID                       
   AND PLL.POLICY_ID=PLP.POLICY_ID AND PLL.POLICY_VERSION_ID=PLP.POLICY_VERSION_ID                                
   WHERE PLL.CUSTOMER_ID=@CUSTOMER_ID and PLP.PROCESS_ID IN(4,5,19,24)                                
   )                                
   AND POLICY_VERSION_ID IN                                
   (                                
   SELECT PLL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST PLL                                
   INNER JOIN POL_POLICY_PROCESS PLP ON PLL.CUSTOMER_ID=PLP.CUSTOMER_ID                                
   AND PLL.POLICY_ID=PLP.POLICY_ID AND PLL.POLICY_VERSION_ID=PLP.POLICY_VERSION_ID               
   WHERE PLL.CUSTOMER_ID=@CUSTOMER_ID and PLP.PROCESS_ID IN(4,5,19,24)                                
   )                                
 END                                  
*/                            
execute Proc_UpdateCustomer_InsuranceScore @CUSTOMER_ID, @CUSTOMER_INSURANCE_SCORE                            
                          
/*if (@CUSTOMER_INSURANCE_SCORE= '-1')                          
begin                                           
 update POL_CUSTOMER_POLICY_LIST                          
 set APPLY_INSURANCE_SCORE = '-1'                          
 where POLICY_STATUS <> 'NORMAL'                          
                                
end     */   
SET @New_Cust_Id = 2                   
      END                  
 
 
END                 
GO

