IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerGenInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerGenInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc Proc_InsertCustomerGenInfo_ACORD      
/*----------------------------------------------------------                  
Proc Name    : dbo.Proc_InsertCustomer                  
Created by   : Pradeep                  
Date         : 2 Aug,2005                 
Purpose     : Insert the record into Customers Table                
Revison History :              
Used In  :   Wolverine                     
 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       -------------------------*/                 
            
CREATE     PROC dbo.Proc_InsertCustomerGenInfo_ACORD              
(              
 @CUSTOMER_ID     int  OUTPUT,              
 @CUSTOMER_CODE    nvarchar(10) ,              
 @CUSTOMER_TYPE    nvarchar(6)  ,              
 @CUSTOMER_PARENT   int   ,              
 @CUSTOMER_SUFFIX    nvarchar(5)  ,              
 @CUSTOMER_FIRST_NAME   nvarchar(25)  ,              
 @CUSTOMER_MIDDLE_NAME   nvarchar(10)  ,              
 @CUSTOMER_LAST_NAME   nvarchar(25)  ,              
 @CUSTOMER_ADDRESS1   nvarchar(150)  ,              
 @CUSTOMER_ADDRESS2   nvarchar(150)  ,              
 @SSN_NO   nvarchar(70)  ,        
 @CUSTOMER_CITY    varchar(70)  ,                
 @CUSTOMER_COUNTRY   nvarchar(10)  ,                
 @CUSTOMER_STATE    nvarchar(10)  ,              
 --@CUSTOMER_ZIP    nvarchar(11)  ,        
 @CUSTOMER_ZIP    varchar(11)  ,              
 @CUSTOMER_BUSINESS_TYPE   nvarchar(20)  ,              
 @CUSTOMER_BUSINESS_DESC   nvarchar(1000)  ,              
 @CUSTOMER_CONTACT_NAME   nvarchar(35)  ,              
 @CUSTOMER_BUSINESS_PHONE  nvarchar(15)  ,              
 @CUSTOMER_EXT    nvarchar(6)  ,              
 @CUSTOMER_HOME_PHONE   nvarchar(15)  ,              
 @CUSTOMER_MOBILE   nvarchar(15)  ,              
 @CUSTOMER_FAX    nvarchar(15)  ,              
 @CUSTOMER_PAGER_NO   nvarchar(15)  ,              
 @CUSTOMER_Email    nvarchar(50)  ,              
 @CUSTOMER_WEBSITE   nvarchar(150)  ,              
 @CUSTOMER_INSURANCE_SCORE  numeric  ,              
 @CUSTOMER_INSURANCE_RECEIVED_DATE datetime  ,              
 @CUSTOMER_REASON_CODE   nvarchar(10)  ,              
 @CUSTOMER_REASON_CODE2   smallint  ,              
 @CUSTOMER_REASON_CODE3   smallint  ,              
 @CUSTOMER_REASON_CODE4   smallint  ,              
              
               
 @CustomerProducerId   NVARCHAR(30),                
             
 @CustomerAccountExecutiveId  NVARCHAR(30),                
             
 @CustomerCsr    NVARCHAR(30),                
 @CustomerReferredBy   NVARCHAR(25),                
               
               
 @PREFIX     int,              
 @CREATED_BY    int  ,              
 @CREATED_DATETIME   datetime ,              
 @CUSTOMER_AGENCY_ID   int,              
 @LAST_INSURANCE_SCORE_FETCHED   datetime,
 @GENDER   nvarchar(10),
 @MARITAL_STATUS  nvarchar(10),
 @DATE_OF_BIRTH   datetime,
 @APPLICANT_OCCU  NVARCHAR(40)
)              
AS              
BEGIN              
 DECLARE @STATE_ID Int              
 DECLARE @MESSAGE NVarChar(100)               
              
 EXECUTE @STATE_ID = Proc_GetSTATE_ID 1,@CUSTOMER_STATE              
   
        
 IF @@ERROR <> 0              
 BEGIN              
  RAISERROR ('Unable to retrieve the State of the Customer', 16, 1)              
  RETURN               
 END 
CREATE TABLE #TEMPAPPOCC
(
	[LOOKUP_VALUE_DESC] NVARCHAR(100),
	[LOOKUP_UNIQUE_ID] INT
)  
INSERT INTO #TEMPAPPOCC
EXECUTE Proc_GetLookupDescFromAcordCodes '%OCC',@APPLICANT_OCCU 
IF ((SELECT COUNT(*) FROM #TEMPAPPOCC) > 0)
BEGIN
	SELECT @APPLICANT_OCCU= CONVERT(NVARCHAR(100),LOOKUP_UNIQUE_ID) FROM #TEMPAPPOCC
END
ELSE
BEGIN
	SET @APPLICANT_OCCU=NULL
END
DROP TABLE #TEMPAPPOCC          
               
 /*              
 if Exists              
 (              
  SELECT CUSTOMER_CODE FROM CLT_CUSTOMER_LIST               
  WHERE CUSTOMER_CODE = @CUSTOMER_CODE              
 )              
 BEGIN              
                
  SET @MESSAGE = 'The Customer Code ' + @CUSTOMER_CODE + 'already exists.'              
                
  RAISERROR (@MESSAGE , 16, 1)              
  RETURN               
 END              
 ELSE           
 */              
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
    SSN_NO,  
          
    CUSTOMER_COUNTRY,              
    CUSTOMER_STATE,              
    CUSTOMER_ZIP,              
    CUSTOMER_BUSINESS_TYPE,              
    CUSTOMER_BUSINESS_DESC,              
    CUSTOMER_CONTACT_NAME,              
  CUSTOMER_BUSINESS_PHONE,              
    CUSTOMER_EXT,              
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
               
    --CUSTOMER_PRODUCER_ID,              
    --CUSTOMER_ACCOUNT_EXECUTIVE_ID,              
    --CUSTOMER_CSR,              
    --CUSTOMER_LATE_CHARGES,             
    --CUSTOMER_LATE_NOTICES,              
    --CUSTOMER_SEND_STATEMENT,              
    --CUSTOMER_RECEIVABLE_DUE_DAYS,              
    --CUSTOMER_REFERRED_BY,              
    CUSTOMER_AGENCY_ID,              
    PREFIX,              
    CREATED_BY,              
    CREATED_DATETIME,              
    IS_ACTIVE,              
    LAST_INSURANCE_SCORE_FETCHED,
	GENDER,
	MARITAL_STATUS,
	DATE_OF_BIRTH,
    APPLICANT_OCCU
)              
  VALUES(              
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
    @SSN_NO,         
    @CUSTOMER_COUNTRY,              
    @STATE_ID,              
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
    @CUSTOMER_REASON_CODE2,              
    @CUSTOMER_REASON_CODE3,              
    @CUSTOMER_REASON_CODE4,              
               
    --@CustomerProducerId   ,              
                
    --@CustomerAccountExecutiveId  ,              
                
    --@CustomerCsr    ,              
    --@CustomerReferredBy   ,              
               
    @CUSTOMER_AGENCY_ID,              
    @PREFIX,               
    @CREATED_BY,              
    @CREATED_DATETIME, 
    'Y',              
    @LAST_INSURANCE_SCORE_FETCHED,
    @GENDER,
    @MARITAL_STATUS,
    @DATE_OF_BIRTH,
    @APPLICANT_OCCU
   )              
              
 IF @@ERROR <> 0              
  BEGIN              
    RAISERROR ('Unable to Insert Customer', 16, 1)              
    RETURN               
  END              
            
  SELECT @CUSTOMER_ID = Max(CUSTOMER_ID)               
  FROM CLT_CUSTOMER_LIST              
               
  declare @APPLICANT_ID int            
 select @APPLICANT_ID=isnull(Max(APPLICANT_ID),0)+1 from CLT_APPLICANT_LIST            
   
 --- Added by mohit             
 -- Inserting customer to clt_customer_list             
            
 INSERT INTO CLT_APPLICANT_LIST            
 (            
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
CO_APPL_SSN_NO,
  IS_PRIMARY_APPLICANT,        
  IS_ACTIVE,          
  CREATED_BY,          
  CREATED_DATETIME,
  CO_APPL_MARITAL_STATUS,
	CO_APPL_DOB,
	CO_APPL_GENDER,
  CO_APPLI_OCCU
         
 )            
 VALUES            
 (            
   @APPLICANT_ID,  @CUSTOMER_ID, @PREFIX, @CUSTOMER_SUFFIX, @CUSTOMER_FIRST_NAME, @CUSTOMER_MIDDLE_NAME,            
   @CUSTOMER_LAST_NAME,  @CUSTOMER_ADDRESS1,  @CUSTOMER_ADDRESS2,  @CUSTOMER_CITY,  @CUSTOMER_COUNTRY,            
   @STATE_ID,  @CUSTOMER_ZIP,  @CUSTOMER_HOME_PHONE, @CUSTOMER_Email,@SSN_NO,1, 'Y',  @CREATED_BY, @CREATED_DATETIME,
	@MARITAL_STATUS,@DATE_OF_BIRTH,@GENDER,@APPLICANT_OCCU            
 )             
              
 END              
END              
              
              
            
          
          
          
        
      
    
  








GO

