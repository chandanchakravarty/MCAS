--sp_helptext PROC_GET_ACCUMULATED_POLICY_DETAILS

 /*----------------------------------------------------------              
Proc Name       : dbo.PROC_GET_ACCUMULATED_POLICY_DETAILS              
Created by      : Kuldeep Saxena     
Date            : 13/03/2012            
Purpose         : To fetch record For accumulated policies and their details  
Revison History :              
Used In         : Ebix Advantage web        
          
------------------------------------------------------------              
Date     Review By          Comments              
--Drop Proc dbo.PROC_GET_RI_CONTRACT_INFO  28325,147,1         
------   ------------       -------------------------*/                 
ALTER PROCEDURE [dbo].[PROC_GET_ACCUMULATED_POLICY_DETAILS]                                      
(      
@ACC_REF NVARCHAR(25)    
)    
AS  
BEGIN           
select   
DISTINCT(PCPL.POLICY_NUMBER) as 'Policy',  
NULL AS 'Ent No',  
PDI.DWELLING_ID AS 'Risk No',  
CCL.CUSTOMER_FIRST_NAME as 'Insured Name',  
PCPL.POLICY_EXPIRATION_DATE as 'Expiry Date',  
PAD.TOTAL_SUM_INSURED as 'Sum Insured',  
PAD.FACULTATIVE_RI as 'Fac',  
pad.GROSS_RETAINED_SUM_INSURED as 'Gross Retained',  
PAD.OWN_RETENTION as 'Own Retained',  
PAD.QUOTA_SHARE as 'Quota Share',  
PAD.FIRST_SURPLUS as 'Surplus1',  
ISNULL(PPP.POLICY_CURRENT_STATUS,'') as 'Status'  
from   
POL_CUSTOMER_POLICY_LIST PCPL   
LEFT JOIN   
POL_DWELLINGS_INFO PDI ON PCPL.CUSTOMER_ID=PDI.CUSTOMER_ID AND PCPL.POLICY_ID=PDI.POLICY_ID 
AND PCPL.POLICY_VERSION_ID=PDI.POLICY_VERSION_ID  
INNER JOIN  
CLT_CUSTOMER_LIST CCL ON PCPL.CUSTOMER_ID=CCL.CUSTOMER_ID   
INNER JOIN  
POL_ACCUMULATION_DETAILS PAD ON PCPL.CUSTOMER_ID=PAD.CUSTOMER_ID AND PCPL.POLICY_ID=PAD.POLICY_ID 
AND PCPL.POLICY_VERSION_ID=PAD.POLICY_VERSION_ID  
INNER JOIN 
POL_POLICY_PROCESS PPP ON PCPL.POLICY_ID=PPP.POLICY_ID AND PCPL.POLICY_VERSION_ID=PPP.POLICY_VERSION_ID 
AND PCPL.CUSTOMER_ID=PPP.CUSTOMER_ID AND PPP.POLICY_CURRENT_STATUS='NORMAL'
where PAD.ACC_REF_NO=@ACC_REF AND PPP.PROCESS_ID IN (25, 14, 18)
END  