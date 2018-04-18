IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXmlCLT_APPLICANT_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXmlCLT_APPLICANT_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*                
Reviewed By : Anurag Verma                
Reviewed On : 12-07-2007                
*/                
                
--Drop Proc dbo.Proc_GetXmlCLT_APPLICANT_LIST 1135                    
CREATE PROCEDURE [dbo].[Proc_GetXmlCLT_APPLICANT_LIST]                                          
(                                          
 @APPLICANT_ID     int                                           
)                                          
AS                                          
BEGIN                                          
SELECT           
CAL.APPLICANT_ID,    
CAL.APPLICANT_TYPE,                                          
CAL.TITLE,                                          
CAL.SUFFIX,                                          
CAL.FIRST_NAME,                                          
CAL.MIDDLE_NAME,                                          
CAL.LAST_NAME,                                          
CAL.ADDRESS1,                                          
CAL.ADDRESS2,                                          
CAL.CITY,                                          
CAL.COUNTRY,                                          
CAL.STATE,                                          
CAL.ZIP_CODE,                                          
CAL.PHONE,                                        
CAL.MOBILE as CMOBILE,                                 
CAL.BUSINESS_PHONE,        
CAL.EXT as APPL_EXT,                                      
--CAL.EMP_EXT AS EXT,                                        
CAL.EMAIL,                                          
CAL.IS_ACTIVE,                                          
CAL.CO_APPLI_OCCU,                                           
CAL.CO_APPLI_EMPL_NAME ,                                          
CAL.CO_APPLI_EMPL_ADDRESS ,                                        
CAL.CO_APPLI_EMPL_ADDRESS1,                                        
CAL.CO_APPLI_EMPL_CITY ,                                        
CAL.CO_APPLI_EMPL_COUNTRY ,                                        
CAL.CO_APPLI_EMPL_STATE ,                                        
CAL.CO_APPLI_EMPL_ZIP_CODE ,                                        
CAL.CO_APPLI_EMPL_PHONE ,                                        
CAL.CO_APPLI_EMPL_EMAIL ,                                          
CAL.CO_APPLI_YEARS_WITH_CURR_EMPL,                                          
CAL.CO_APPL_YEAR_CURR_OCCU,                                          
CAL.CO_APPL_MARITAL_STATUS,      
CAL.POSITION,        
CAL.CONTACT_CODE,        
CAL.ID_TYPE,        
CAL.ID_TYPE_NUMBER,        
CAL.NUMBER,        
CAL.COMPLIMENT,        
CAL.DISTRICT,        
CAL.NOTE ,      
CAL.REGIONAL_IDENTIFICATION,      
CAL.REG_ID_ISSUE,      
CAL.ORIGINAL_ISSUE,
CAL.ACCOUNT_NUMBER,
CAL.ACCOUNT_TYPE,
CAL.BANK_BRANCH,
CAL.BANK_NAME,
CAL.BANK_NUMBER,      
      
CAL.FAX,      
CAL.CPF_CNPJ,     
    
      
                                      
--convert(varchar, CAL.CO_APPL_DOB,101) as CO_APPL_DOB ,  
CO_APPL_DOB ,                                         
ISNULL(CO_APPL_SSN_NO,'') AS CO_APPL_SSN_NO,                                          
case CAL.is_active when 'Y' then 'Active' else 'Inactive' end APPLICANT_STATUS,                                          
case IS_PRIMARY_APPLICANT when 1 then 'Yes' else 'No' end PRIMARY_APPLICANT,                                        
CAL.DESC_CO_APPLI_OCCU,                                     
--CCL.GENDER as CO_APPL_GENDER,                                
CAL.CO_APPL_GENDER as CO_APPL_GENDER,                                
CAL.CO_APPL_RELATIONSHIP,                              
--CCL.PER_CUST_MOBILE as MOBILE                   
--CAL.EMP_EXT as EXT,                   
--IF CAL.MOBILE IS NOT NULL              
CCL.CUSTOMER_FIRST_NAME,              
CCL.CUSTOMER_MIDDLE_NAME,              
CCL.CUSTOMER_LAST_NAME,              
CCL.CUSTOMER_CODE,          
CASE WHEN CAL.EXT IS NULL THEN CAL.EMP_EXT ELSE CAL.EXT END AS EXT,                  
--CASE WHEN CCL.CUSTOMER_TYPE = '11109' THEN CAL.EMP_EXT ELSE CAL.EXT END AS EXT,                            
--CASE WHEN CCL.CUSTOMER_TYPE = '11109' THEN CAL.MOBILE ELSE CAL.PER_CUST_MOBILE END AS MOBILE          
CAL.MOBILE AS MOBILE ,
CAL.CREATED_DATETIME
           
from  CLT_APPLICANT_LIST CAL  LEFT OUTER JOIN   CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = CAL.CUSTOMER_ID                    
where  APPLICANT_ID = @APPLICANT_ID                                          
END                 
          
          
GO

