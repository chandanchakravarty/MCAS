IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_SUSEP_REPORT_270_271_322_323]'))
DROP VIEW [dbo].[VW_SUSEP_REPORT_270_271_322_323]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================              
-- Author:  <SHIKHA CHOURASIYA>              
-- Create date: <2011-25-07>              
-- Description: < Quadro 270,322,271,323 - Claims statistics. >              
-- ===============================================              
--DROP VIEW VW_SUSEP_REPORT_270_271_322_323                
CREATE VIEW [dbo].[VW_SUSEP_REPORT_270_271_322_323]                     
AS                      
SELECT CA.ACTION_ON_PAYMENT,                      
--CRL.CLAIM_ID AS CRL_CLAIM_ID,                      
CCI.CO_INSURANCE_TYPE ,                      
CPC.IS_RISK_COVERAGE ,                      
--PEI.COMPANY_ID ,                
CP.SOURCE_PARTY_ID ,  
CP.SOURCE_PARTY_TYPE_ID, --ADDED BY SHIKHA ON 08/17/2011             
CP.PARTY_TYPE_ID,                  
CA.IS_LEGAL ,                      
CPC.COVERAGE_CODE_ID AS 'COV_CODE' ,-- Relook                    
MRCL.COM_TYPE ,                      
MLM.SUSEP_LOB_CODE ,                      
PCPL.POLICY_EFFECTIVE_DATE AS EFFECTIVE_DATETIME , -- changes                      
PCPL.POLICY_EXPIRATION_DATE ,                     
CCI.LOSS_DATE ,                      
CCI.CREATED_DATETIME ,                      
CA.RESERVE_AMOUNT ,                      
CCI.FIRST_NOTICE_OF_LOSS ,                      
MRCL.SUSEP_NUM AS MRCL_SUSEP_NUM ,                      
--MRCL_1.SUSEP_NUM AS MRCL_1_SUSEP_NUM ,                      
CCI.CLAIM_ID AS CCI_CLAIM_ID,                      
CCI.CLAIM_NUMBER ,                      
CPC.LIMIT_1 ,                      
CA.EXPENSES ,                    
PCPL.CO_INSURANCE ,                    
CA.IS_ACTIVE AS CA_IS_ACTIVE,                    
--MLM.IS_ACTIVE AS MLM_IS_ACTIVE,                    
CCI.CLAIM_STATUS ,          
CAR.RECOVERY_AMOUNT,          
CAR.TOTAL_RECOVERY_AMOUNT,          
CAR.PAYMENT_AMOUNT,          
CAR.TOTAL_PAYMENT_AMOUNT,          
CAR.CO_RESERVE,          
CAR.CO_RESERVE_TRAN,          
CAR.RI_RESERVE,          
CAR.RI_RESERVE_TRAN,          
CAR.OUTSTANDING,          
CAR.OUTSTANDING_TRAN,          
CAR.PREV_OUTSTANDING,          
CAR.RESERVE_ID,                  
PCPL.POLICY_CURRENCY,                    
--CA.CLAIM_ID AS CA_CLAIM_ID  ,                    
CCI.OUTSTANDING_RESERVE ,                    
CA.[RECOVERY] ,                    
CA.PAYMENT_AMOUNT AS CA_PAYMENT_AMOUNT,          
CCI.CLAIM_STATUS_UNDER,        
CCI.OFFCIAL_CLAIM_NUMBER,
CACR.PAYMENT_AMT AS CO_RI_PEYMENT_AMT,
CACR.RECOVERY_AMT AS CO_RI_RECOVERY_AMT,
CACR.COMP_TYPE,
CA.ACTIVITY_ID   --added by shikha 06/09/2011                       
FROM CLM_CLAIM_INFO  (NOLOCK) AS CCI                         
LEFT OUTER JOIN CLM_ACTIVITY (NOLOCK) AS CA  ON  CCI.CLAIM_ID=CA.CLAIM_ID                        
--LEFT OUTER JOIN CLM_REOPEN_CLAIM (NOLOCK) AS CRL  ON  CRL.CLAIM_ID=CA.CLAIM_ID                        
JOIN POL_CUSTOMER_POLICY_LIST (NOLOCK) AS PCPL ON (PCPL.CUSTOMER_ID=CCI.CUSTOMER_ID AND PCPL.POLICY_ID=CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID)                        
LEFT OUTER JOIN MNT_LOB_MASTER (NOLOCK) AS MLM  ON MLM.LOB_ID=CCI.LOB_ID                        
LEFT OUTER JOIN CLM_ACTIVITY_RESERVE (NOLOCK) CAR ON CAR.CLAIM_ID=CA.CLAIM_ID AND CAR.ACTIVITY_ID=CA.ACTIVITY_ID            
LEFT OUTER JOIN CLM_PRODUCT_COVERAGES (NOLOCK) AS CPC  ON CPC.CLAIM_ID=CCI.CLAIM_ID AND CPC.CLAIM_COV_ID=CAR.COVERAGE_ID                         
LEFT OUTER JOIN MNT_COVERAGE_EXTRA (NOLOCK) AS MCE ON MCE.COV_ID=CPC.COVERAGE_CODE_ID                             
LEFT OUTER JOIN CLM_PARTIES CP ON CCI.CLAIM_ID = CP.CLAIM_ID AND PARTY_TYPE_ID IN (618,619)              
LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST (NOLOCK) MRCL ON MRCL.REIN_COMAPANY_ID=CP.SOURCE_PARTY_ID  
LEFT OUTER JOIN CLM_ACTIVITY_CO_RI_BREAKDOWN CACR ON CACR.CLAIM_ID=CA.CLAIM_ID AND CACR.ACTIVITY_ID=CA.ACTIVITY_ID             
WHERE CA.IS_ACTIVE='Y' AND CA.ACTIVITY_STATUS=11801 AND  IS_VOIDED_REVERSED_ACTIVITY IS NULL  
GO

