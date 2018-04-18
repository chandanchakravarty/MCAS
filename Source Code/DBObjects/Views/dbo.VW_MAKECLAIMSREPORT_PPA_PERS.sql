IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_MAKECLAIMSREPORT_PPA_PERS]'))
DROP VIEW [dbo].[VW_MAKECLAIMSREPORT_PPA_PERS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP VIEW VW_MakeClaimsReport_PPA_PERS
--GO
/*----------------------------------------------------------                                                                            
View Name       : VW_MakeClaimsReport_PPA_PERS                                            
Created by      : Shikha Dixit                                                                       
Date            : 01/04/2009                                                                            
Purpose         : Generates sum of Claims amount :- state, lob, month, year wise                        
Used In        : Wolverine                                                                            
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------*/                                                                            
--DROP VIEW VW_MakeClaimsReport_PPA_PERS
CREATE VIEW [dbo].[VW_MAKECLAIMSREPORT_PPA_PERS]
AS
-- 180 - Paid Loss, Partial
-- 181 - Paid Loss, Final
-- 240 - Void - Paid Loss
-- 175 - Adjuster Expense

WITH 
VEHICLE_PURPOSE
AS
(
	SELECT CLAIM_ID, PURPOSE_OF_USE
	FROM CLM_INSURED_VEHICLE CIV
	WHERE INSURED_VEHICLE_ID = (SELECT MIN(INSURED_VEHICLE_ID) FROM CLM_INSURED_VEHICLE WHERE CLAIM_ID = CIV.CLAIM_ID)
),
CLAIM_REPORT
AS
(
SELECT 'Personal Auto' AS LOB,VW.STATE_ID AS STATE_ID,VW.COVERAGE_ID,
SUM(CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT ELSE  VW.AMOUNT END) AS AMOUNT,
CASE WHEN VW.ACTION_ON_PAYMENT IN (180,181,240) THEN 'Paid Loss'
	 WHEN VW.ACTION_ON_PAYMENT IN (254,175) THEN 'Adjustment Expense'
	 WHEN VW.ACTION_ON_PAYMENT IN (173,204,241)THEN 'Other Expense'
	 WHEN VW.ACTION_ON_PAYMENT IN(189) THEN 'Salvage'
	 WHEN VW.ACTION_ON_PAYMENT IN(176,244,245) THEN 'Salvage Expense'
	 WHEN VW.ACTION_ON_PAYMENT IN(190) THEN 'Subrogation'
	 WHEN VW.ACTION_ON_PAYMENT IN(177,242,243) THEN 'Subrogation Expense'
	ELSE VW.DETAIL_TYPE_DESCRIPTION END AS DETAIL_TYPE_DESCRIPTION,VW.ACTV_MONTH, VW.ACTV_YEAR                      
FROM VW_CLAIM_TRANSACTION_DETAIL VW
INNER JOIN VEHICLE_PURPOSE VP
ON VW.CLAIM_ID = VP.CLAIM_ID
--INNER JOIN CLM_INSURED_VEHICLE CIV
--ON CIV.INSURED_VEHICLE_ID = CASE WHEN VW.RISK_ID = 0 THEN 1 ELSE VW.RISK_ID END
--AND CIV.CLAIM_ID = VW.CLAIM_ID
--ON VW.RISK_ID = CIV.INSURED_VEHICLE_ID
--AND VW.CLAIM_ID = CIV.CLAIM_ID              
INNER JOIN CLM_CLAIM_TRANCACTIONS TMP
ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT  
WHERE VW.LOB_ID = '2' AND VP.PURPOSE_OF_USE = '11332' 
--AND VW.COVERAGE_ID IN (2,114,1,9,113,119,4,36,115,118,6,12,120,14,34,121,304,38,122,42,123,44,124,45,125,1006)            
GROUP BY VW.LOB_ID, VW.STATE_ID,VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION,
VW.COVERAGE_ID,VW.ACTION_ON_PAYMENT                                               
      
--**********
   UNION
--**********            

SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE_ID,AEP.COVERAGEID,
SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,'Written Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV 
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
WHERE PCPL.POLICY_LOB = '2' AND AEP.COVERAGEID IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID            
              
UNION              
                                         
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,
SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,'Written Premium' AS DETAIL_TYPE_DESCRIPTION, 
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID              
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID  
INNER JOIN POL_VEHICLES PV 
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
WHERE PCPL.POLICY_LOB = '2'AND AEP.COVERAGEID NOT IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID, AEP.COVERAGEID            
              
UNION              
                                                     
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,SUM(AEP.EARNED_PREMIUM) AS AMOUNT,
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH,
AEP.YEAR_NUMBER AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID              
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID  
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID 
WHERE PCPL.POLICY_LOB = '2' AND AEP.COVERAGEID NOT IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID, AEP.COVERAGEID             
              
UNION              
                                                                 
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,
SUM(AEP.ENDING_UNEARNED) AS AMOUNT,
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, 
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL          
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID 
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID               
INNER JOIN POL_VEHICLES PV 
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID 
WHERE PCPL.POLICY_LOB = '2'AND AEP.COVERAGEID NOT IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID ,AEP.COVERAGEID           

UNION
--Added by Shikha on 28/03/09
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID, AEP.COVERAGEID,SUM(AEP.EARNED_PREMIUM) AS AMOUNT,               
'Earned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, 
AEP.YEAR_NUMBER AS ACTV_YEAR                  
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                  
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                     
INNER JOIN POL_VEHICLES PV 
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                  
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID                 
WHERE PCPL.POLICY_LOB = '2' AND PV.APP_USE_VEHICLE_ID = '11332'  AND AEP.COVERAGEID IN ('20007','20009')               
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,AEP.COVERAGEID   
              
UNION              

SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID,  AEP.COVERAGEID,
SUM(AEP.ENDING_UNEARNED) AS AMOUNT,
'Unearned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR                  
FROM ACT_EARNED_PREMIUM AEP
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL 
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID 
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                 
INNER JOIN POL_VEHICLES PV 
ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                  
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID                 
WHERE PCPL.POLICY_LOB = '2' AND PV.APP_USE_VEHICLE_ID = '11332' AND AEP.COVERAGEID IN ('20007','20009') 
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,AEP.COVERAGEID 
--End of Addition            
                  
UNION
              
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE,AEP.COVERAGEID,SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,           
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL    
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID              
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID   AND AEP.RISK_ID = PV.VEHICLE_ID           
WHERE PCPL.POLICY_LOB = '2' AND AEP.COVERAGEID NOT IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID            

UNION
              
SELECT DISTINCT  'Personal Auto' AS Lob, AEP.STATE_ID AS STATE,AEP.COVERAGEID,SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,           
'Inforce Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER              
AS ACTV_YEAR              
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID              
AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
WHERE PCPL.POLICY_LOB = '2' AND AEP.COVERAGEID IN ('20007','20009') AND PV.APP_USE_VEHICLE_ID = '11332'             
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID  
              
UNION 
         
SELECT DISTINCT 'Personal Auto' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID, SUM(BEGIN_RESERVE) AS AMOUNT,           
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                          
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW           
WHERE VW.LOB_ID = '2' AND VW.PURPOSE_OF_USE = '11332'
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID,VW.COVERAGE_ID                                       
            
UNION          
SELECT DISTINCT 'Personal Auto' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID,SUM(END_RESERVE) AS AMOUNT,           
'Ending Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                          
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
WHERE VW.LOB_ID = '2' AND VW.PURPOSE_OF_USE = '11332'
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID ,VW.COVERAGE_ID        
            
UNION          
SELECT DISTINCT 'Personal Auto' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID, SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT,           
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                          
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
WHERE VW.LOB_ID = '2' AND VW.PURPOSE_OF_USE = '11332'
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER,VW.STATE_ID,VW.COVERAGE_ID                                          
            
UNION        
SELECT DISTINCT 'Personal Auto' AS Lob, VW.STATE_ID AS STATE,VW.COVERAGE_ID, SUM(END_RESERVE_REINSURANCE) AS AMOUNT,           
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                          
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
WHERE VW.LOB_ID = '2'AND VW.PURPOSE_OF_USE = '11332'
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID,VW.COVERAGE_ID    
       
UNION    
    
SELECT   DISTINCT 'Personal Auto' AS LOB, VPLC.STATE_ID AS STATE,VPLC.COVERAGE_ID,
SUM(LOSSES_INCURRED) AS AMOUNT,'Incurred Loss' DETAIL_TYPE_DESCRIPTION, VPLC.MONTH_NUMBER ACTV_MONTH,
VPLC.YEAR_NUMBER ACTV_YEAR                                         
FROM VW_PAID_LOSS_INCURRED_BY_COVERAGE VPLC
INNER JOIN CLM_INSURED_VEHICLE CIV
ON VPLC.CLAIM_ID = CIV.CLAIM_ID 
WHERE VPLC.LOB_ID = '2' AND CIV.PURPOSE_OF_USE = '11332'
GROUP BY VPLC.MONTH_NUMBER, VPLC.YEAR_NUMBER, VPLC.COVERAGE_ID,VPLC.STATE_ID      
)

SELECT * FROM CLAIM_REPORT 
--ORDER BY ACTV_MONTH, ACTV_YEAR              
            
--DROP TABLE #TEMP_CLM_COVERAGE_PPA_Pers            
--DROP TABLE  #TMP_ACTIVITY_FINAL             
--END                      
 
--GO
--SELECT * FROM  VW_MakeClaimsReport_PPA_PERS WHERE ACTV_MONTH = 6 AND ACTV_YEAR = 2009 ORDER BY DETAIL_TYPE_DESCRIPTION
--ROLLBACK TRAN
            























GO

