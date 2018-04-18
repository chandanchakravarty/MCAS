ALTER PROC [dbo].[Proc_GetCustomerInfo]                           
(                            
 @CustomerID  int                            
)                            
AS                            
BEGIN                            
                            
SELECT                   
CUSTOMER_FIRST_NAME,                  
ISNULL(CUSTOMER_MIDDLE_NAME,'') AS CUSTOMER_MIDDLE_NAME,                  
ISNULL(CUSTOMER_LAST_NAME,'') AS CUSTOMER_LAST_NAME,       
ISNULL(CUSTOMER_SUFFIX,'') AS CUSTOMER_SUFFIX,  -- Added for Itrack Issue 6165 on 27 July 2009              
CUSTOMER_ADDRESS1,                  
CUSTOMER_ADDRESS2,                  
CUSTOMER_CITY,                  
CUSTOMER_COUNTRY,                  
CUSTOMER_STATE,                  
CUSTOMER_ZIP,                  
CUSTOMER_EXT,                  
CUSTOMER_HOME_PHONE,                
--CUSTOMER_MOBILE, --fetch mobile data from PER_CUST_MOBILE for personal customer            
CASE CUSTOMER_TYPE WHEN 11110 THEN PER_CUST_MOBILE ELSE ''  END CUSTOMER_MOBILE,            
CUSTOMER_FAX,                  
convert(varchar,DATE_OF_BIRTH,101) DATE_OF_BIRTH,                  
CLT.MARITAL_STATUS,                  
SSN_NO,            
APPLICANT_OCCU AS OCCUPATION, -- Added for Itrack Issue 6165 on 27 July 2009           
EMPLOYER_NAME,                  
--EMPLOYER_ADDRESS,                
CUSTOMER_BUSINESS_PHONE,                
CUSTOMER_TYPE,              
IsNull(CLT.CUSTOMER_AGENCY_ID,0) CUSTOMER_AGENCY_ID,                            
CLT.CUSTOMER_STATE,                    
CLT.IS_ACTIVE,                  
CLT.CUSTOMER_CODE, CLT.GENDER,            
ISNULL(EMPLOYER_ADD1,'') + ' ' + ISNULL(EMPLOYER_ADD2,'') AS EMPLOYER_ADDRESS                 
       FROM CLT_CUSTOMER_LIST  CLT           
--FROM CLT_CUSTOMER_LIST  CLT ,  MNT_AGENCY_LIST MAL   --EDIT DONE BY AVIJIT ON 13/02/2012 FOR SINGAPORE PROJECT              
--where MAL.AGENCY_ID=CLT.CUSTOMER_AGENCY_ID and          --EDIT DONE BY AVIJIT ON 13/02/2012 FOR SINGAPORE PROJECT                     

WHERE CLT.CUSTOMER_ID  =  @CustomerID               
                            
                            
END 