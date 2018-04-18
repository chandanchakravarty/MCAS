IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomerInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomerInfo_ACORD]
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
MODIFIED BY : Pradeep    
MODIFY ON  : 2 Aug,2005    
PURPOSE  : To avoid duplcate customer code         
    
MODIFIED BY : Vijay    
MODIFY ON  : 2 June,2005    
PURPOSE  : To increase length of city        
------   ------------       -------------------------*/       
CREATE   PROC dbo.Proc_UpdateCustomerInfo_ACORD    
(      
 @CUSTOMER_ID     int ,    
 @CUSTOMER_CODE    nvarchar(10) ,    
 @CUSTOMER_TYPE    nvarchar(6)  ,    
 @CUSTOMER_PARENT   int   ,    
 @CUSTOMER_SUFFIX   nvarchar(5)  ,    
 @CUSTOMER_FIRST_NAME   nvarchar(25)  ,    
 @CUSTOMER_MIDDLE_NAME   nvarchar(10)  ,    
 @CUSTOMER_LAST_NAME   nvarchar(25)  ,    
 @CUSTOMER_ADDRESS1   nvarchar(150)  ,    
 @CUSTOMER_ADDRESS2   nvarchar(150)  ,    
 @CUSTOMER_CITY    nvarchar(70)  ,    
 @CUSTOMER_COUNTRY   nvarchar(10)  ,      
 @CUSTOMER_STATE    nvarchar(10)  ,    
 @CUSTOMER_ZIP    nvarchar(12)  ,    
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
 @CUSTOMER_REASON_CODE   nvarchar (10)  ,    
 @CUSTOMER_REASON_CODE2   smallint  ,    
 @CUSTOMER_REASON_CODE3   smallint  ,    
 @CUSTOMER_REASON_CODE4   smallint  ,    
@CustomerProducerId   NVARCHAR(30),      
--@CustomerLateCharges   NVARCHAR(30),      
--@CustomerLateNotices   NVARCHAR(1),      
@CustomerAccountExecutiveId  NVARCHAR(30),      
--@CustomerSendStatement  NVARCHAR(1),      
--@CustomerReceivableDueDays  NVARCHAR(4),      
@CustomerCsr    NVARCHAR(30),      
@CustomerReferredBy   NVARCHAR(25),      
@CUSTOMER_AGENCY_ID   int,    
    
 @PREFIX     int ,    
 @MODIFIED_BY    int  ,    
 @MODIFIED_DATETIME   datetime,
 @GENDER   nvarchar(10),
 @MARITAL_STATUS  nvarchar(10),
 @DATE_OF_BIRTH   datetime,
 @APPLICANT_OCCU  NVARCHAR(40)      
)    
AS       
BEGIN      
     
 DECLARE @STATE_ID Int    
     
 EXECUTE @STATE_ID = Proc_GetSTATE_ID 1,@CUSTOMER_STATE    
    
     
 /*Checking for the duplicacy of code */    
 if Not Exists(SELECT CUSTOMER_CODE     
   FROM CLT_CUSTOMER_LIST     
   WHERE CUSTOMER_CODE = @CUSTOMER_CODE AND    
    CUSTOMER_ID <> @CUSTOMER_ID)    
 BEGIN     
  /*Updating the record*/ 
	RETURN 1
	/*    
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
   --CUSTOMER_STATE    =@STATE_ID,     
   CUSTOMER_ZIP    =@CUSTOMER_ZIP,     
   CUSTOMER_BUSINESS_TYPE   =@CUSTOMER_BUSINESS_TYPE,    
   CUSTOMER_BUSINESS_DESC   =@CUSTOMER_BUSINESS_DESC,     
   CUSTOMER_CONTACT_NAME   =@CUSTOMER_CONTACT_NAME,     
   CUSTOMER_BUSINESS_PHONE   =@CUSTOMER_BUSINESS_PHONE,    
   CUSTOMER_EXT    =@CUSTOMER_EXT,       
   CUSTOMER_HOME_PHONE   =@CUSTOMER_HOME_PHONE,     
   CUSTOMER_MOBILE    =@CUSTOMER_MOBILE,      
   CUSTOMER_FAX    =@CUSTOMER_FAX,       
   CUSTOMER_PAGER_NO   =@CUSTOMER_PAGER_NO,      
   CUSTOMER_Email    =@CUSTOMER_Email,      
   CUSTOMER_WEBSITE   =@CUSTOMER_WEBSITE,      
   CUSTOMER_INSURANCE_SCORE  =@CUSTOMER_INSURANCE_SCORE,     
   CUSTOMER_INSURANCE_RECEIVED_DATE =@CUSTOMER_INSURANCE_RECEIVED_DATE,    
   CUSTOMER_REASON_CODE   =@CUSTOMER_REASON_CODE,    
   CUSTOMER_REASON_CODE2   =@CUSTOMER_REASON_CODE2,    
   CUSTOMER_REASON_CODE3   =@CUSTOMER_REASON_CODE3,    
   CUSTOMER_REASON_CODE4   =@CUSTOMER_REASON_CODE4,    
CUSTOMER_AGENCY_ID=@CUSTOMER_AGENCY_ID,    
   PREFIX     =@PREFIX,     
   MODIFIED_BY    =@MODIFIED_BY,     
   LAST_UPDATED_DATETIME   =@MODIFIED_DATETIME    
   WHERE CUSTOMER_ID = @CUSTOMER_ID      
	*/
 END    
      
END    
  
  
  





GO

