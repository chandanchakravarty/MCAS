IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--DROP PROC [Proc_GetCustomerDetails]  28114

CREATE PROC [dbo].[Proc_GetCustomerDetails]                                              
(                                                      
 @CustomerID  int                                 
)                                       
AS                                                      
BEGIN                                                      
                    
DECLARE @HAS_APPLICATIONS Char(1)                  
                    
IF EXISTS ( SELECT CUSTOMER_ID FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CustomerID )                     
BEGIN                     
 SET @HAS_APPLICATIONS  = 'Y'                    
END                                      
ELSE                    
BEGIN                     
 SET @HAS_APPLICATIONS  = 'N'                    
END                                      
                                   
SELECT  CLT.CUSTOMER_ID, CLT.CUSTOMER_CODE,                                                      
 ISNULL(CLT.CUSTOMER_TYPE,'') CUSTOMER_TYPE,                                                      
 ISNULL(CLT.CUSTOMER_PARENT,0) AS CUSTOMER_PARENT,                                                      
 ISNULL(CLT.CUSTOMER_FIRST_NAME,'')CUSTOMER_FIRST_NAME,                                                      
 ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') CUSTOMER_MIDDLE_NAME,                                                      
 ISNULL(CLT.CUSTOMER_LAST_NAME,'') CUSTOMER_LAST_NAME,                                   
 ISNULL(CLT.CUSTOMER_SUFFIX,'') CUSTOMER_SUFFIX,                                                      
 ISNULL(CLT.CUSTOMER_ADDRESS1,'') CUSTOMER_ADDRESS1,ISNULL(CLT.CUSTOMER_ADDRESS2,'') CUSTOMER_ADDRESS2,                                                      
 ISNULL(CLT.CUSTOMER_CITY,'') CUSTOMER_CITY,ISNULL(CLT.CUSTOMER_COUNTRY,'') CUSTOMER_COUNTRY,ISNULL(CLT.CUSTOMER_STATE,'') CUSTOMER_STATE,ISNULL(MNT_COUNTRY_LIST.COUNTRY_NAME,'') CUSTOMER_COUNTRY_NAME,                                                    
ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'') CUSTOMER_STATE_NAME,                                                      
ISNULL(MNT_COUNTRY_STATE_LIST.STATE_CODE,'') as CUSTOMER_STATE_CODE,                                                    
 ISNULL(CLT.CUSTOMER_ZIP,'') CUSTOMER_ZIP,                                                  
ISNULL(CLT.CUSTOMER_BUSINESS_TYPE,'') CUSTOMER_BUSINESS_TYPE,                                                  
--ISNULL(BUS_TYPE.LOOKUP_VALUE_DESC,'') as CUSTOMER_BUSINESS_TYPE_NAME,                        
ISNULL(BUS_TYPE.ACTIVITY_DESC,'') as CUSTOMER_BUSINESS_TYPE_NAME,                                                 
ISNULL(CLT.CUSTOMER_BUSINESS_DESC,'') CUSTOMER_BUSINESS_DESC,                                                      
 ISNULL(CLT.CUSTOMER_CONTACT_NAME,'') CUSTOMER_CONTACT_NAME,                                                      
 ISNULL(CLT.CUSTOMER_BUSINESS_PHONE,'') CUSTOMER_BUSINESS_PHONE,ISNULL(CLT.CUSTOMER_EXT,'') CUSTOMER_EXT,                          
ISNULL(CLT.CUSTOMER_HOME_PHONE,'') CUSTOMER_HOME_PHONE,                                                      
 ISNULL(CLT.CUSTOMER_MOBILE,'') CUSTOMER_MOBILE,ISNULL(CLT.CUSTOMER_FAX,'') CUSTOMER_FAX,ISNULL(CLT.CUSTOMER_PAGER_NO,'') CUSTOMER_PAGER_NO,                                                      
 ISNULL(CLT.CUSTOMER_Email,'') CUSTOMER_EMAIL,                                                      
 ISNULL(CLT.CUSTOMER_WEBSITE,'') CUSTOMER_WEBSITE,ltrim(rtrim(ISNULL(CLT.CUSTOMER_REASON_CODE,''))) CUSTOMER_REASON_CODE,ISNULL(CLT.IS_ACTIVE,'') IS_ACTIVE,                                                      
 CLT.CUSTOMER_INSURANCE_RECEIVED_DATE,                                              
 CLT.CUSTOMER_INSURANCE_SCORE,CLT.CREATED_BY,                                        
ISNULL(CLT.PREFIX,'')PREFIX ,                                  
 ISNULL(CLT.PER_CUST_MOBILE,'') PER_CUST_MOBILE,ISNULL(CLT.EMP_EXT,'') EMP_EXT,                                        
CLT.CREATED_DATETIME,CLT.MODIFIED_BY,                                
 CLT.LAST_UPDATED_DATETIME , IsNull(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'') AS CUSTOMER_TYPE_DESC,                                                      
 IsNull(CUSTOMER_PREFIX_LOOKUP.LOOKUP_VALUE_DESC,'') As PREFIX_DESC,ltrim(rtrim(ISNULL(CLT.CUSTOMER_REASON_CODE2,0))) CUSTOMER_REASON_CODE2,                                                      
 ltrim(rtrim(ISNULL(CLT.CUSTOMER_REASON_CODE3,0))) CUSTOMER_REASON_CODE3,ltrim(rtrim(ISNULL(CLT.CUSTOMER_REASON_CODE4,0))) CUSTOMER_REASON_CODE4 ,                                                      
 --ISNULL(CLT.CUSTOMER_PRODUCER_ID,'') CUSTOMER_PRODUCER_ID,ISNULL(CLT.CUSTOMER_ACCOUNT_EXECUTIVE_ID,'') CUSTOMER_ACCOUNT_EXECUTIVE_ID,ISNULL(CLT.CUSTOMER_CSR,'') CUSTOMER_CSR, ISNULL(CLT.CUSTOMER_REFERRED_BY,'') CUSTOMER_REFERRED_BY,                    
  
     
     
        
           
---Added by Lalit March 12,2010          
  ISNULL(CLT.CPF_CNPJ,'') CPF_CNPJ,          
  ISNULL(CLT.NUMBER,'') NUMBER,          
          
  ISNULL(CLT.DISTRICT,'') DISTRICT,          
        
  ISNULL(CLT.MAIN_TITLE,0) MAIN_TITLE,          
  ISNULL(CLT.MAIN_POSITION,0) MAIN_POSITION,          
  ISNULL(CLT.MAIN_CPF_CNPJ,'') MAIN_CPF_CNPJ,          
  ISNULL(CLT.MAIN_ADDRESS,'') MAIN_ADDRESS,          
  ISNULL(CLT.MAIN_NUMBER,'') MAIN_NUMBER,          
  ISNULL(CLT.MAIN_COMPLIMENT,'') MAIN_COMPLIMENT,          
  ISNULL(CLT.MAIN_DISTRICT,'') MAIN_DISTRICT,          
  ISNULL(CLT.MAIN_NOTE,'') MAIN_NOTE,          
  ISNULL(CLT.MAIN_CONTACT_CODE,'') MAIN_CONTACT_CODE,        
  ISNULL(CLT.REGIONAL_IDENTIFICATION,'')REGIONAL_IDENTIFICATION,        
  CLT.REG_ID_ISSUE,        
  ISNULL(CLT.ORIGINAL_ISSUE,'') ORIGINAL_ISSUE,        
  ISNULL(CLT.MAIN_ZIPCODE ,'')MAIN_ZIPCODE,      
  ISNULL(CLT.MAIN_CITY ,'')MAIN_CITY,      
  ISNULL(CLT. MAIN_COUNTRY ,'')MAIN_COUNTRY, ISNULL(CNTRY.COUNTRY_NAME,'') MAIN_COUNTRY_NAME,      
  ISNULL(CLT.MAIN_STATE ,'') MAIN_STATE, ISNULL(ST.STATE_NAME,'') MAIN_STATE_NAME,    
  ISNULL(CLT.MAIN_FIRST_NAME ,'') MAIN_FIRST_NAME,    
  ISNULL(CLT.MAIN_MIDDLE_NAME ,'') MAIN_MIDDLE_NAME,    
  ISNULL(CLT.MAIN_LAST_NAME ,'') MAIN_LAST_NAME,    
  ISNULL(CLT.ID_TYPE ,'') ID_TYPE,   
  --convert(varchar,(CLT.MONTHLY_INCOME))
  ISNULL(CLT.MONTHLY_INCOME,0) MONTHLY_INCOME, 
  ISNULL(CLT.AMOUNT_TYPE ,'') AMOUNT_TYPE,
  ISNULL(CLT.CADEMP ,'') CADEMP, 
  --convert(varchar,(CLT.NET_ASSETS_AMOUNT)) 
 ISNULL(CLT.NET_ASSETS_AMOUNT, 0) NET_ASSETS_AMOUNT,
  ISNULL(CLT.NATIONALITY ,'') NATIONALITY, 
  ISNULL(CLT.EMAIL_ADDRESS ,'') EMAIL_ADDRESS,
  ISNULL(CLT.REGIONAL_IDENTIFICATION_TYPE ,'') REGIONAL_IDENTIFICATION_TYPE,
  ISNULL(CLT.IS_POLITICALLY_EXPOSED ,'') IS_POLITICALLY_EXPOSED, 
  ISNULL(CAL.BANK_BRANCH ,'') BANK_BRANCH, 
  ISNULL(CAL.BANK_NAME ,'') BANK_NAME,
  ISNULL(CAL.BANK_NUMBER ,'') BANK_NUMBER,
  ISNULL(CAL.ACCOUNT_NUMBER ,'') ACCOUNT_NUMBER,
  ISNULL(CAL.ACCOUNT_TYPE ,'') ACCOUNT_TYPE,
  
       
  --ISNULL(CLT.CUSTOMER_COUNTRY,'') CUSTOMER_COUNTRY,ISNULL(CLT.CUSTOMER_STATE,'') CUSTOMER_STATE,ISNULL(MNT_COUNTRY_LIST.COUNTRY_NAME,'') CUSTOMER_COUNTRY_NAME,                                                    
-----End            
              
                    
                          
IsNull(CLT.CUSTOMER_AGENCY_ID,0) CUSTOMER_AGENCY_ID,                                                      
IsNull(PC.CUSTOMER_FIRST_NAME,'') +                                                
CASE ISNULL(PC.CUSTOMER_MIDDLE_NAME ,'') WHEN '' THEN '' ELSE ' ' + ISNULL(PC.CUSTOMER_MIDDLE_NAME,'') END +                                 
' ' + IsNull(PC.CUSTOMER_LAST_NAME,'') CUSTOMER_PARENT_TEXT ,                                                      
 case          
  when (CLT.CUSTOMER_ATTENTION_NOTE is null) then '0'                                
  when (CLT.CUSTOMER_ATTENTION_NOTE='') then '0'                                                       
  else CLT.CUSTOMER_ATTENTION_NOTE                                                      
 end CUSTOMER_ATTENTION_NOTE,                              
 CLT.LAST_INSURANCE_SCORE_FETCHED,                                                    
 CASE             
 when CLT.LAST_INSURANCE_SCORE_FETCHED is null then -1                                                         
 when CLT.LAST_INSURANCE_SCORE_FETCHED is not null  then DATEDIFF(MM,CLT.LAST_INSURANCE_SCORE_FETCHED,GETDATE())                                                      
 end                                                      
 DT_LAST_INSURANCE_SCORE_FETCHED                                                      
 ,AGENCY_DISPLAY_NAME ,                                                
CLT.APPLICANT_OCCU,CLT.EMPLOYER_NAME,CLT.EMPLOYER_ADDRESS,CLT.YEARS_WITH_CURR_EMPL,CLT.MARITAL_STATUS,CLT.SSN_NO,
--convert(varchar,CLT.DATE_OF_BIRTH,101) as DATE_OF_BIRTH,  
CLT.DATE_OF_BIRTH,                                          
CLT.IS_ACTIVE as IS_ACTIVE,CLT.DESC_APPLICANT_OCCU,                                     
ISNULL(CLT.EMPLOYER_ADD1,'')  EMPLOYER_ADD1,                                    
ISNULL(CLT.EMPLOYER_ADD2,'')  EMPLOYER_ADD2,                                    
ISNULL(CLT.EMPLOYER_CITY,'')  EMPLOYER_CITY,                                    
ISNULL(CLT.EMPLOYER_COUNTRY,'')  EMPLOYER_COUNTRY,                                    
ISNULL(CLT.EMPLOYER_STATE,'')  EMPLOYER_STATE,                                    
ISNULL(CLT.EMPLOYER_ZIPCODE,'')  EMPLOYER_ZIPCODE,                                    
ISNULL(CLT.EMPLOYER_HOMEPHONE,'')  EMPLOYER_HOMEPHONE,                           
ISNULL(CLT.PER_CUST_MOBILE,'')  PER_CUST_MOBILE,                                   
ISNULL(CLT.EMPLOYER_EMAIL,'')  EMPLOYER_EMAIL,                                    
CLT.YEARS_WITH_CURR_OCCU,CLT.GENDER,                                
ISNULL(CLT.IS_ACTIVE,'') IS_CUSTOMER_ACTIVE  ,                        
ISNULL (mal.AGENCY_ID,'') AS AGENCY_ID ,--aDDED 17 APRIL 2007                        
CASE MAL.IS_ACTIVE                        
 WHEN 'Y' THEN   ISNULL(MAL.AGENCY_DISPLAY_NAME,'') +'-'+ RTRIM(ISNULL (MAL.AGENCY_CODE,'')) +'-'+ cast(isnull(MAL.NUM_AGENCY_CODE,'') as varchar) + '- (Active)'                        
 WHEN 'N' THEN   ISNULL(MAL.AGENCY_DISPLAY_NAME,'') +'-'+ RTRIM(ISNULL (MAL.AGENCY_CODE,'')) +'-'+ cast(isnull(MAL.NUM_AGENCY_CODE,'') as varchar) + '- (Inactive)'                        
END AS AGENCY_NAME_ACTIVE_STATUS   ,                     
@HAS_APPLICATIONS AS HAS_APPLICATIONS,                  
--Added by Charles on 18-Aug-09 for Customer Page Optimization                  
CASE                   
WHEN  ISNULL(DATEDIFF(DAY,MAL.TERMINATION_DATE, GETDATE()),0)> 0 THEN 'Y'                  
ELSE 'N'                  
END AS IS_TERMINATED                   
--Added till here                    
                           
--ISNULL(CLT.ALT_CUSTOMER_ADDRESS1,'') ALT_CUSTOMER_ADDRESS1,ISNULL(CLT.ALT_CUSTOMER_ADDRESS2,'') ALT_CUSTOMER_ADDRESS2,                                                      
 --ISNULL(CLT.ALT_CUSTOMER_CITY,'') ALT_CUSTOMER_CITY,ISNULL(CLT.ALT_CUSTOMER_COUNTRY,'') ALT_CUSTOMER_COUNTRY,CLT.ALT_CUSTOMER_STATE,                                                    
 --ISNULL(CLT.ALT_CUSTOMER_ZIP,'') ALT_CUSTOMER_ZIP                                
                                                              
FROM  CLT_CUSTOMER_LIST CLT WITH(NOLOCK)                                                     
 LEFT OUTER JOIN MNT_COUNTRY_LIST ON CLT.CUSTOMER_COUNTRY = MNT_COUNTRY_LIST.COUNTRY_ID                                              
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST ON CLT.CUSTOMER_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID        
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST as ST ON CLT.MAIN_STATE= ST.STATE_ID      
  LEFT OUTER JOIN MNT_COUNTRY_LIST as CNTRY ON CLT.MAIN_COUNTRY = CNTRY.COUNTRY_ID      
                   
 LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP ON                                                   
 CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                                      
 LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_PREFIX_LOOKUP ON                                                   
 CUSTOMER_PREFIX_LOOKUP.LOOKUP_UNIQUE_ID = CLT.PREFIX                                                    
LEFT JOIN MNT_ACTIVITY_MASTER  BUS_TYPE ON                       
--LEFT JOIN MNT_LOOKUP_VALUES BUS_TYPE ON                                                   
-- BUS_TYPE.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_BUSINESS_TYPE                          
BUS_TYPE.ACTIVITY_ID = CLT.CUSTOMER_BUSINESS_TYPE                                                    
 LEFT JOIN CLT_CUSTOMER_LIST PC ON                                                       
   CLT.CUSTOMER_PARENT = PC.CUSTOMER_ID AND                                                      
   CLT.CUSTOMER_ID  =  @CustomerID                                    
 LEFT OUTER JOIN MNT_AGENCY_LIST MAL ON                                                   
 MAL.AGENCY_ID=CLT.CUSTOMER_AGENCY_ID  
 INNER JOIN CLT_APPLICANT_LIST CAL ON 
 CAL.CUSTOMER_ID = CLT.CUSTOMER_ID
 AND IS_PRIMARY_APPLICANT = 1                                                        
WHERE CLT.CUSTOMER_ID  =  @CustomerID  


SELECT COUNT(*)as TOTPOLICY FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CustomerID AND IS_ACTIVE='Y'

SELECT count(*) INDIVIDUAL_CONTACT_ID FROM MNT_CONTACT_LIST  with(nolock) WHERE INDIVIDUAL_CONTACT_ID = @CustomerID  AND IS_ACTIVE = 'Y'

                                                                                                           
END 

GO

