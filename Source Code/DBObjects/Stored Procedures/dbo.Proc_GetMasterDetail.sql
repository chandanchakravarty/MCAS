    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_GetMasterDetail]              
Created by      : SNEHA          
Date            : 24/10/2011                      
Purpose         :INSERT RECORDS IN MNT_MASTER_DETAIL TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_GetMasterDetail]        
      
*/  

CREATE PROC dbo.Proc_GetMasterDetail  
(  
@TYPE_UNIQUE_ID INT
)  
AS  
BEGIN  
        
 SELECT   TYPE_UNIQUE_ID,TYPE_ID,TYPE_CODE,TYPE_NAME,ADDRESS,ADDRESS1,CITY,COUNTRY,TEL_NO_OFF,MOBILE_NO,E_MAIL,GST,CONTACT_PERSON,PROVINCE,POST_CODE,TEL_NO_RES,FAX_NO,GST_REG_NO,WITHHOLDING_TAX,STATUS,SOLICITOR_TYPE,PRIVATE_E_MAIL,SURVEYOR_SOURCE,CLASSIFICATION,MEMO,IS_ACTIVE
 FROM MNT_MASTER_DETAIL WITH(NOLOCK)   
 WHERE TYPE_UNIQUE_ID=@TYPE_UNIQUE_ID             
  
END  
         