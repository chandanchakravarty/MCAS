  
 /*----------------------------------------------------------                
Proc Name        : dbo.Proc_FetchReinsurerInfo                
Created by       : Priya Arora               
Date             : Jan 06,2006             
Purpose          : retrieving data from MNT_REIN_COMPANY_LIST                
Revison History  :                
Used In         : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/            
-- MNT_REIN_GETXML_REINSURER 18        
-- DROP PROC dbo.MNT_REIN_GETXML_REINSURER   
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_GETXML_REINSURER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_GETXML_REINSURER]
GO                 
CREATE PROC [dbo].[MNT_REIN_GETXML_REINSURER]               
@REIN_COMPANY_ID INT,  
@LANG_ID INT=NULL                
AS                
                
BEGIN                
SELECT         
        
ISNULL(REIN_COMAPANY_ID,0) REIN_COMPANY_ID,        
ISNULL(REIN_COMAPANY_CODE,'') REIN_COMAPANY_CODE,        
ISNULL(REIN_COMAPANY_NAME,'') REIN_COMAPANY_NAME,        
ISNULL(REIN_COMPANY_TYPE,'') REIN_COMAPANY_TYPE,        
        
ISNULL(REIN_COMAPANY_ADD1,'') REIN_COMAPANY_ADD1,        
ISNULL(REIN_COMAPANY_ADD2,'') REIN_COMAPANY_ADD2,              
    
ISNULL(REIN_COMAPANY_CITY,'') REIN_COMAPANY_CITY,        
ISNULL(REIN_COMAPANY_COUNTRY,'') REIN_COMAPANY_COUNTRY,        
ISNULL(REIN_COMAPANY_STATE,'') REIN_COMAPANY_STATE,                
ISNULL(REIN_COMAPANY_ZIP,'') REIN_COMAPANY_ZIP,        
                
ISNULL(REIN_COMAPANY_PHONE,'') REIN_COMAPANY_PHONE,        
ISNULL(REIN_COMAPANY_EXT,0) REIN_COMAPANY_EXT,                
ISNULL(REIN_COMAPANY_FAX,0) REIN_COMAPANY_FAX,        
       
ISNULL(REIN_COMAPANY_MOBILE,'') REIN_COMAPANY_MOBILE,        
ISNULL(REIN_COMAPANY_EMAIL,'') REIN_COMAPANY_EMAIL,                
ISNULL(REIN_COMAPANY_NOTE,'') REIN_COMAPANY_NOTE,        
ISNULL(REIN_COMAPANY_ACC_NUMBER,0) REIN_COMAPANY_ACC_NUMBER,                
                
ISNULL(M_REIN_COMPANY_ADD_1,'') M_REIN_COMPANY_ADD_1,                
ISNULL(M_RREIN_COMPANY_ADD_2,'') M_RREIN_COMPANY_ADD_2,                
ISNULL(M_REIN_COMPANY_CITY,'') M_REIN_COMPANY_CITY,                
ISNULL(M_REIN_COMPANY_COUNTRY,'') M_REIN_COMPANY_COUNTRY,                
ISNULL(M_REIN_COMPANY_STATE,'') M_REIN_COMPANY_STATE,                
ISNULL(M_REIN_COMPANY_ZIP,'') M_REIN_COMPANY_ZIP,                
ISNULL(M_REIN_COMPANY_PHONE,'') M_REIN_COMPANY_PHONE,                
ISNULL(M_REIN_COMPANY_EXT,'') M_REIN_COMPANY_EXT,                
ISNULL(M_REIN_COMPANY_FAX,'') M_REIN_COMPANY_FAX,                
ISNULL(REIN_COMPANY_WEBSITE,'') REIN_COMPANY_WEBSITE,            
ISNULL(REIN_COMPANY_IS_BROKER,'') REIN_COMPANY_IS_BROKER,            
ISNULL(PRINCIPAL_CONTACT,'') PRINCIPAL_CONTACT,            
ISNULL(OTHER_CONTACT,'') OTHER_CONTACT,            
ISNULL(FEDERAL_ID,'') FEDERAL_ID,            
       
ISNULL(REIN_COMPANY_SPEED_DIAL,'') REIN_COMPANY_SPEED_DIAL,        
            
           
        
        
--'AGENCY_ID='+cast(AGENCY_ID as varchar(8000)) as UniqueGrdId,              
          
CONVERT(VARCHAR,TERMINATION_DATE,CASE WHEN @LANG_ID=3 THEN 103 ELSE 101 END) TERMINATION_DATE,            
CONVERT(VARCHAR,EFFECTIVE_DATE,CASE WHEN @LANG_ID=3 THEN 103 ELSE 101 END) EFFECTIVE_DATE,        
isnull(TERMINATION_REASON,'')TERMINATION_REASON  ,        
isnull(DOMICILED_STATE,'')DOMICILED_STATE  ,        
isnull(NAIC_CODE,'')NAIC_CODE  ,        
isnull(AM_BEST_RATING,'')AM_BEST_RATING  ,        
isnull(COMMENTS,'')COMMENTS  ,        
        
IS_ACTIVE ,    
isnull(SUSEP_NUM,'')SUSEP_NUM ,    
isnull(COM_TYPE,'')COM_TYPE  ,  
 ISNULL(DISTRICT,'') DISTRICT,  
 ISNULL(BANK_NUMBER,'') BANK_NUMBER,  
 ISNULL(BANK_BRANCH_NUMBER,'') BANK_BRANCH_NUMBER,  
 ISNULL(CARRIER_CNPJ,'') CARRIER_CNPJ,  
 ISNULL(BANK_ACCOUNT_TYPE,'') BANK_ACCOUNT_TYPE,  
 ISNULL(PAYMENT_METHOD,'') PAYMENT_METHOD,  
  ISNULL(AGENCY_CLASSIFICATION,'') AGENCY_CLASSIFICATION,  
 ISNULL(RISK_CLASSIFICATION,'') RISK_CLASSIFICATION  
      
            
FROM MNT_REIN_COMAPANY_LIST        
--WHERE agency_code not in ('W001')                 
where REIN_COMAPANY_ID=@REIN_COMPANY_ID                
END       
      