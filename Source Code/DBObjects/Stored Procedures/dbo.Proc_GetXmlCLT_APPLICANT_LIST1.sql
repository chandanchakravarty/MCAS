IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXmlCLT_APPLICANT_LIST1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXmlCLT_APPLICANT_LIST1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name    : dbo.Proc_GetXmlCLT_APPLICANT_LIST                
Created by   : -            
Date         : -                
Purpose      : -             
  
Modified  by : Swastika Gaur  
Date         : 11th Apr,06  
Purpose      : Addition of Co-Applicant's Employer's fields  
  
Revision History :              
Used In  :   Wolverine                     
 ------------------------------------------------------------                              
Date     Review By          Comments                            
                   
------   ------------       -------------------------*/ 
--DROP PROC Proc_GetXmlCLT_APPLICANT_LIST  
CREATE   PROCEDURE Proc_GetXmlCLT_APPLICANT_LIST1    
(    
 @APPLICANT_ID     int     
)    
AS    
BEGIN    
select APPLICANT_ID,    
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
MOBILE,  
BUSINESS_PHONE,  
EXT,  
EMAIL,    
IS_ACTIVE,    
CO_APPLI_OCCU,     
CO_APPLI_EMPL_NAME ,    
CO_APPLI_EMPL_ADDRESS ,  
CO_APPLI_EMPL_ADDRESS1,  
CO_APPLI_EMPL_CITY ,  
CO_APPLI_EMPL_COUNTRY ,  
CO_APPLI_EMPL_STATE ,  
CO_APPLI_EMPL_ZIP_CODE ,  
CO_APPLI_EMPL_PHONE ,  
CO_APPLI_EMPL_EMAIL ,    
CO_APPLI_YEARS_WITH_CURR_EMPL,    
CO_APPL_YEAR_CURR_OCCU,    
CO_APPL_MARITAL_STATUS,

convert(varchar, CO_APPL_DOB,101) as CO_APPL_DOB ,    
CO_APPL_SSN_NO,    
case is_active when 'Y' then 'Active' else 'Inactive' end APPLICANT_STATUS,    
case IS_PRIMARY_APPLICANT when 1 then 'Yes' else 'No' end PRIMARY_APPLICANT,  
DESC_CO_APPLI_OCCU 
--CLT.GENDER GENDER    
from  CLT_APPLICANT_LIST 
--INNER JOIN CLT_CUSTOMER_LIST CLT ON CLT.CUSTOMER_ID = CLT_APPLICANT_LIST.CUSTOMER_ID 
where  APPLICANT_ID = @APPLICANT_ID  

END  

GO

