IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAgencyInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAgencyInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                    
Proc Name      : dbo.Proc_FetchAgencyInfo                    
Created by       : Anurag Verma                    
Date             : 5/9/2005                    
Purpose       : retrieving data from MNT_AGENCY_LIST                    
Revison History :           
Modify by       : Pravesh                    
Date             : 17/11/2006                    
Purpose       : Fetch 2 More Fields TERMINATION_DATE_RENEW and TERMINATION_NOTICE        
                 
Used In        : Wolverine                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--drop proc dbo.Proc_FetchAgencyInfo  28         
CREATE PROC [dbo].[Proc_FetchAgencyInfo]                   
@AGENCY_ID INT ,
@LANG_ID INT                   
AS                    
 declare @CARRIER_CODE nvarchar(20)                    
BEGIN   
  SELECT @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'') FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)  
INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID=SYSP.SYS_CARRIER_ID    

            
SELECT ISNULL(AGENCY.AGENCY_ID,0) AGENCY_ID,ISNULL(AGENCY.AGENCY_CODE,'') AGENCY_CODE,              
ISNULL(AGENCY.AGENCY_COMBINED_CODE,'') AGENCY_COMBINED_CODE,                    
ISNULL(AGENCY.AGENCY_DISPLAY_NAME,'') AGENCY_DISPLAY_NAME,ISNULL(AGENCY.AGENCY_DBA,'') AGENCY_DBA,ISNULL(AGENCYNAME,0) AGENCYNAME,              
ISNULL(AGENCY.AGENCY_ADD1,'')+' '+ISNULL(AGENCY.AGENCY_ADD2,'')                    
 AS ADDRESS,ISNULL(AGENCY.AGENCY_CITY,'') AGENCY_CITY,ISNULL(AGENCY.AGENCY_STATE,0) AGENCY_STATE,                    
ISNULL(AGENCY.AGENCY_ZIP,0) AGENCY_ZIP,ISNULL(AGENCY.AGENCY_COUNTRY,0) AGENCY_COUNTRY,                    
ISNULL(AGENCY.AGENCY_PHONE,'') AGENCY_PHONE,ISNULL(AGENCY.AGENCY_EXT,'') AGENCY_EXT,                    
ISNULL(AGENCY.AGENCY_FAX,0) AGENCY_FAX,ISNULL(AGENCY.AGENCY_SPEED_DIAL,0) AGENCY_SPEED_DIAL,              
ISNULL(AGENCY.AGENCY_EMAIL,'') AGENCY_EMAIL,ISNULL(AGENCY.AGENCY_WEBSITE,'') AGENCY_WEBSITE,              
ISNULL(AGENCY.AGENCY_PAYMENT_METHOD,0) AGENCY_PAYMENT_METHOD,ISNULL(AGENCY.AGENCY_COMMISSION,0.0) AGENCY_COMMISSION,              
ISNULL(AGENCY.AGENCY_BILL_TYPE,0) AGENCY_BILL_TYPE,ISNULL(AGENCY.AGENCY_SIGNATURES,0) AGENCY_SIGNATURES,              
AGENCY.IS_ACTIVE,AGENCY.LAST_UPDATED_DATETIME,                    
ISNULL(AGENCY.AGENCY_ADD1,'') AGENCY_ADD1,ISNULL(AGENCY.AGENCY_ADD2,'') AGENCY_ADD2,                    
AGENCY.AGENCY_LIC_NUM AGENCY_LIC_NUM,                    
                    
ISNULL(PRINCIPAL_CONTACT,'') PRINCIPAL_CONTACT,                    
ISNULL(OTHER_CONTACT,'') OTHER_CONTACT,                    
ISNULL(FEDERAL_ID,'') FEDERAL_ID,                    
--CONVERT(VARCHAR,ORIGINAL_CONTRACT_DATE,101) ORIGINAL_CONTRACT_DATE,    
ORIGINAL_CONTRACT_DATE,          
--CONVERT(VARCHAR,CURRENT_CONTRACT_DATE,101) CURRENT_CONTRACT_DATE,    
CURRENT_CONTRACT_DATE,                 
--ISNULL(UNDERWRITER_ASSIGNED_AGENCY,'') UNDERWRITER_ASSIGNED_AGENCY,                    
ISNULL(BANK_ACCOUNT_NUMBER,'') BANK_ACCOUNT_NUMBER,                    
ISNULL(ROUTING_NUMBER,'') ROUTING_NUMBER,              
ISNULL(BANK_ACCOUNT_NUMBER1,'') BANK_ACCOUNT_NUMBER1,                    
ISNULL(ROUTING_NUMBER1,'') ROUTING_NUMBER1,              
ISNULL(BANK_NAME,'') BANK_NAME,                    
ISNULL(BANK_BRANCH,'') BANK_BRANCH,                    
--NEW FIELDS                    
ISNULL(M_AGENCY_ADD_1,'') M_AGENCY_ADD_1,                    
ISNULL(M_AGENCY_ADD_2,'') M_AGENCY_ADD_2,                    
ISNULL(M_AGENCY_CITY,'') M_AGENCY_CITY,                    
ISNULL(M_AGENCY_COUNTRY,'') M_AGENCY_COUNTRY,                    
ISNULL(M_AGENCY_STATE,'') M_AGENCY_STATE,                    
ISNULL(M_AGENCY_ZIP,'') M_AGENCY_ZIP,                    
--ISNULL(M_AGENCY_PHONE,'') M_AGENCY_PHONE,                    
--ISNULL(M_AGENCY_EXT,'') M_AGENCY_EXT,                    
--ISNULL(M_AGENCY_FAX,'') M_AGENCY_FAX,                    
'AGENCY_ID='+cast(AGENCY_ID as varchar(8000)) as UniqueGrdId,           
-- new fields                  
--CONVERT(VARCHAR,TERMINATION_DATE,101) TERMINATION_DATE,   
TERMINATION_DATE,               
   isnull(TERMINATION_REASON,'')TERMINATION_REASON ,              
isnull(NOTES,'')NOTES,              
NUM_AGENCY_CODE,                 
--CONVERT(VARCHAR,TERMINATION_DATE_RENEW,101) TERMINATION_DATE_RENEW,   
TERMINATION_DATE_RENEW,              
TERMINATION_NOTICE,    
INCORPORATED_LICENSE,      
ISNULL(ALLOWS_EFT,0) ALLOWS_EFT  ,  
ISNULL(ALLOWS_CUSTOMER_SWEEP,0) ALLOWS_CUSTOMER_SWEEP  , 
ISNULL(agency.REQ_SPECIAL_HANDLING,'10694')  AS REQ_SPECIAL_HANDLING,
ACCOUNT_TYPE  ,  
ACCOUNT_TYPE_2 ,  
PROCESS_1099 ,  
BANK_NAME_2,  
BANK_BRANCH_2,  
REVERIFIED_AC1,  
REVERIFIED_AC2,  
CONVERT(VARCHAR,ACCOUNT_VERIFIED_DATE1,101) AS ACCOUNT_VERIFIED_DATE1,  
CONVERT(VARCHAR,ACCOUNT_VERIFIED_DATE2,101) AS ACCOUNT_VERIFIED_DATE2,  
CASE WHEN CONVERT(VARCHAR,ACCOUNT_ISVERIFIED1) = '10964' THEN CASE WHEN @LANG_ID=2 THEN 'Não' ELSE 'No' END
 WHEN CONVERT(VARCHAR(10),ACCOUNT_ISVERIFIED1) = '10963' THEN CASE WHEN @LANG_ID=2 THEN 'Sim' ELSE 'Yes' END ELSE   
CASE WHEN @LANG_ID=2 THEN 'Não' ELSE 'No' END END AS ACCOUNT_ISVERIFIED1,  
CASE WHEN CONVERT(VARCHAR,ACCOUNT_ISVERIFIED2) = '10964' THEN CASE WHEN @LANG_ID=2 THEN 'Não' ELSE 'No' END  
 WHEN CONVERT(VARCHAR(10),ACCOUNT_ISVERIFIED2) = '10963' THEN CASE WHEN @LANG_ID=2 THEN 'Sim' ELSE 'Yes' END ELSE   
CASE WHEN @LANG_ID=2 THEN 'Não' ELSE 'No' END END AS ACCOUNT_ISVERIFIED2,
ISNULL(SUSEP_NUMBER,'') SUSEP_NUMBER, 
 BROKER_CURRENCY,
 AGENCY_TYPE_ID ,
 BROKER_TYPE,
     BROKER_CPF_CNPJ,
  --CONVERT(VARCHAR, BROKER_DATE_OF_BIRTH,101) AS BROKER_DATE_OF_BIRTH,
  BROKER_DATE_OF_BIRTH,
    BROKER_REGIONAL_ID,
    REGIONAL_ID_ISSUANCE,
  -- CONVERT(VARCHAR, REGIONAL_ID_ISSUE_DATE,101) AS REGIONAL_ID_ISSUE_DATE,
  REGIONAL_ID_ISSUE_DATE,
    MARITAL_STATUS,
    GENDER,
    BROKER_BANK_NUMBER,
    DISTRICT,
    NUMBER
    


FROM mnt_agency_list agency WITH(NOLOCK)                    
WHERE agency_code <>@CARRIER_CODE                    
AND AGENCY_ID=@AGENCY_ID                    
END      
  


GO

