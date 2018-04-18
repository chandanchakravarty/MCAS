IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_MakeClaimsReport_MOT]'))
DROP VIEW [dbo].[VW_MakeClaimsReport_MOT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP VIEW VW_MakeClaimsReport_MOT
CREATE VIEW VW_MakeClaimsReport_MOT
AS
WITH
TEMP1 
AS
(
	SELECT PL.POLICY_NUMBER, PL.CUSTOMER_ID , PL.POLICY_ID, PL.POLICY_VERSION_ID,PVC.VEHICLE_ID, PVC.COVERAGE_CODE_ID,
	CASE WHEN 		(
					SELECT COUNT(P.CUSTOMER_ID)FROM POL_VEHICLE_COVERAGES P
					WHERE P.CUSTOMER_ID = PL.CUSTOMER_ID AND P.POLICY_ID = PL.POLICY_ID AND 
					P.POLICY_VERSION_ID = PL.POLICY_VERSION_ID AND 
					P.COVERAGE_CODE_ID IN (200,216)
					)>0
	THEN 'YES' ELSE 'NO' END 
	AS IS_COLLISION
	FROM POL_CUSTOMER_POLICY_LIST PL
	INNER JOIN POL_VEHICLE_COVERAGES PVC
	ON PL.CUSTOMER_ID = PVC.CUSTOMER_ID
	AND PL.POLICY_ID = PVC.POLICY_ID
	AND PL.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID
	WHERE PL.POLICY_LOB = 3	--AND PL.CUSTOMER_ID = 3520 AND PL.POLICY_ID = 1
) ,

CLAIM_REPORT
AS
(
	SELECT 'Motorcycle' AS LOB,VW.STATE_ID AS STATE_ID,VW.COVERAGE_ID,
	SUM(CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT ELSE  VW.AMOUNT END) AS AMOUNT,
	CASE WHEN VW.ACTION_ON_PAYMENT IN (180,181,240) THEN 'Paid Loss'
		 WHEN VW.ACTION_ON_PAYMENT IN (254,175) THEN 'Adjustment Expense'
		 WHEN VW.ACTION_ON_PAYMENT IN (173,204,241)THEN 'Other Expense'
		 WHEN VW.ACTION_ON_PAYMENT IN(189) THEN 'Salvage'
		 WHEN VW.ACTION_ON_PAYMENT IN(176,244,245) THEN 'Salvage Expense'
		 WHEN VW.ACTION_ON_PAYMENT IN(190) THEN 'Subrogation'
		 WHEN VW.ACTION_ON_PAYMENT IN(177,242,243) THEN 'Subrogation Expense'
	ELSE VW.DETAIL_TYPE_DESCRIPTION END AS DETAIL_TYPE_DESCRIPTION,VW.ACTV_MONTH, VW.ACTV_YEAR,
	COD.LOSS_TYPE AS 'LOSS TYPE',T.IS_COLLISION
	FROM VW_CLAIM_TRANSACTION_DETAIL VW
	INNER JOIN CLM_CLAIM_TRANCACTIONS TMP
	ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT  
	LEFT JOIN CLM_OCCURRENCE_DETAIL COD
	ON COD.CLAIM_ID = VW.CLAIM_ID
	LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = VW.CUSTOMER_ID
	AND T.POLICY_ID = VW.POLICY_ID
	and t.coverage_code_id = vw.coverage_id
	AND T.POLICY_VERSION_ID = VW.POLICY_VERSION_ID
	AND T.VEHICLE_ID = VW.RISK_ID
	WHERE VW.LOB_ID = '3' --AND VW.CUSTOMER_ID = 3520 AND VW.POLICY_ID = 1
	--AND VW.COVERAGE_ID IN (127,207,126,131,206,211,128,208,769,770,843,132,212,199,133,214,200,216,201,217,202,218)            
	GROUP BY VW.LOB_ID, VW.STATE_ID,VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION,VW.COVERAGE_ID,
	COD.LOSS_TYPE,VW.ACTION_ON_PAYMENT,T.IS_COLLISION

--**********
   UNION
--**********            

SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE_ID,AEP.COVERAGEID,
SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,'Written Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID IN ('20007','20008')    
--	AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID,T.IS_COLLISION

UNION              
                                         
SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,
SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,'Written Premium' AS DETAIL_TYPE_DESCRIPTION, 
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID              
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID  
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3'AND AEP.COVERAGEID NOT IN ('20007','20008')            
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID, AEP.COVERAGEID,T.IS_COLLISION 

UNION              
                                                     
SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,SUM(AEP.EARNED_PREMIUM) AS AMOUNT,
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH,
AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID              
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID  
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID 
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID NOT IN ('20007','20008')         
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520          
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID, AEP.COVERAGEID,T.IS_COLLISION

UNION

SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID, AEP.COVERAGEID,SUM(AEP.EARNED_PREMIUM) AS AMOUNT,               
'Earned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, 
AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                  
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                     
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                  
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID                 
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID IN ('20007','20008')               
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520          
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,AEP.COVERAGEID,T.IS_COLLISION

UNION              
                                                                 
SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE, AEP.COVERAGEID,
SUM(AEP.ENDING_UNEARNED) AS AMOUNT,
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, 
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL          
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID 
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID               
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID 
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID 
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3'AND AEP.COVERAGEID NOT IN ('20007','20008')     
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID ,AEP.COVERAGEID, T.IS_COLLISION

UNION              

SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID,  AEP.COVERAGEID,
SUM(AEP.ENDING_UNEARNED) AS AMOUNT,
'Unearned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,
AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                  
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID 
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                     
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                  
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID                 
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID IN ('20007','20008') 
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520          
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,AEP.COVERAGEID, T.IS_COLLISION

UNION
              
SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE,AEP.COVERAGEID,SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,           
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER AS ACTV_YEAR,
NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL    
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID              
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID   AND AEP.RISK_ID = PV.VEHICLE_ID           
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID NOT IN ('20007','20008')
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520          
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID, T.IS_COLLISION
              
UNION
              
SELECT DISTINCT  'Motorcycle' AS Lob, AEP.STATE_ID AS STATE,AEP.COVERAGEID,SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,           
'Inforce Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER              
AS ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM ACT_EARNED_PREMIUM AEP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL              
	ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID
	AND AEP.VERSION_FOR_RISK = PCPL.POLICY_VERSION_ID                
INNER JOIN POL_VEHICLES PV 
	ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID              
	AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND AEP.RISK_ID = PV.VEHICLE_ID             
LEFT JOIN TEMP1 T
	ON T.CUSTOMER_ID = AEP.CUSTOMER_ID
	AND T.POLICY_ID = AEP.POLICY_ID
	AND T.POLICY_VERSION_ID = AEP.VERSION_FOR_RISK
	AND T.COVERAGE_CODE_ID = AEP.COVERAGEID
	AND T.VEHICLE_ID = PV.VEHICLE_ID
WHERE PCPL.POLICY_LOB = '3' AND AEP.COVERAGEID IN ('20007','20008')
--AND AEP.POLICY_ID= 1 AND AEP.CUSTOMER_ID = 3520          
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER ,AEP.STATE_ID,AEP.COVERAGEID,T.IS_COLLISION

UNION 
         
SELECT DISTINCT 'Motorcycle' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID, BEGIN_RESERVE AS AMOUNT,           
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR,NULL AS 'LOSS TYPE',
T.IS_COLLISION
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW 
LEFT JOIN TEMP1 T
	ON T.POLICY_ID = VW.POLICY_ID
	AND T.POLICY_NUMBER = VW.POLICY_NUMBER
	AND T.COVERAGE_CODE_ID = VW.COVERAGE_ID
	AND T.VEHICLE_ID = VW.POLICY_RISK_ID
WHERE VW.LOB_ID = '3' AND VW.BEGIN_RESERVE <> 0.00
-- AND VW.POLICY_NUMBER = 'C1000004'        
GROUP BY VW.BEGIN_RESERVE,VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID,VW.COVERAGE_ID,T.IS_COLLISION
            
UNION          
SELECT DISTINCT 'Motorcycle' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID,END_RESERVE AS AMOUNT,           
'Ending Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR,NULL AS 'LOSS TYPE',
T.IS_COLLISION
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
LEFT JOIN TEMP1 T
	ON T.POLICY_ID = VW.POLICY_ID
	AND T.POLICY_NUMBER = VW.POLICY_NUMBER
	AND T.COVERAGE_CODE_ID = VW.COVERAGE_ID
	AND T.VEHICLE_ID = VW.POLICY_RISK_ID
WHERE VW.LOB_ID = '3' AND VW.END_RESERVE <> 0.00
-- AND VW.POLICY_NUMBER = 'C1000004'       
GROUP BY VW.END_RESERVE,VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID ,VW.COVERAGE_ID, T.IS_COLLISION

UNION          
SELECT DISTINCT 'Motorcycle' AS Lob, VW.STATE_ID AS STATE, VW.COVERAGE_ID, BEGIN_RESERVE_REINSURANCE AS AMOUNT,           
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR,
NULL AS 'LOSS TYPE',T.IS_COLLISION                    
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
LEFT JOIN TEMP1 T
	ON T.POLICY_ID = VW.POLICY_ID
	AND T.POLICY_NUMBER = VW.POLICY_NUMBER
	AND T.COVERAGE_CODE_ID = VW.COVERAGE_ID
	AND T.VEHICLE_ID = VW.POLICY_RISK_ID
WHERE VW.LOB_ID = '3' AND VW.BEGIN_RESERVE_REINSURANCE <> 0.00
-- AND VW.POLICY_NUMBER = 'C1000004'       
GROUP BY VW.BEGIN_RESERVE_REINSURANCE,VW.MONTH_NUMBER, VW.YEAR_NUMBER,VW.STATE_ID,VW.COVERAGE_ID,T.IS_COLLISION
            
UNION          
SELECT DISTINCT 'Motorcycle' AS Lob, VW.STATE_ID AS STATE,VW.COVERAGE_ID, END_RESERVE_REINSURANCE AS AMOUNT,           
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR,
NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM VW_BEGINING_ENDING_RESERVE_BY_COVERAGE VW               
LEFT JOIN TEMP1 T
	ON T.POLICY_ID = VW.POLICY_ID
	AND T.POLICY_NUMBER = VW.POLICY_NUMBER
	AND T.COVERAGE_CODE_ID = VW.COVERAGE_ID
	AND T.VEHICLE_ID = VW.POLICY_RISK_ID
	AND VW.END_RESERVE_REINSURANCE <> 0.00
WHERE VW.LOB_ID = '3'
-- AND VW.POLICY_NUMBER = 'C1000004'       
GROUP BY VW.END_RESERVE_REINSURANCE,VW.MONTH_NUMBER, VW.YEAR_NUMBER ,VW.STATE_ID,VW.COVERAGE_ID, T.IS_COLLISION

UNION    
    
SELECT   DISTINCT 'Motorcycle' AS LOB, VPLC.STATE_ID AS STATE,VPLC.COVERAGE_ID,
SUM(LOSSES_INCURRED) AS AMOUNT,'Incurred Loss' DETAIL_TYPE_DESCRIPTION, VPLC.MONTH_NUMBER ACTV_MONTH,
VPLC.YEAR_NUMBER ACTV_YEAR,NULL AS 'LOSS TYPE',T.IS_COLLISION
FROM VW_PAID_LOSS_INCURRED_BY_COVERAGE VPLC
INNER JOIN CLM_INSURED_VEHICLE CIV
ON VPLC.CLAIM_ID = CIV.CLAIM_ID 
INNER JOIN CLM_CLAIM_INFO CCI
ON VPLC.CLAIM_ID = CCI.CLAIM_ID
INNER JOIN TEMP1 T
ON T.CUSTOMER_ID = CCI.CUSTOMER_ID
AND T.POLICY_ID = CCI.POLICY_ID
AND T.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
AND T.COVERAGE_CODE_ID = VPLC.COVERAGE_ID
WHERE VPLC.LOB_ID = '3'
--AND CCI.POLICY_ID= 7 AND CCI.CUSTOMER_ID = 1710        
GROUP BY VPLC.MONTH_NUMBER, VPLC.YEAR_NUMBER, VPLC.COVERAGE_ID,VPLC.STATE_ID,T.IS_COLLISION
)


SELECT * FROM CLAIM_REPORT 
--where amount <> 0 AND  ACTV_MONTH = 7 AND ACTV_YEAR = 2009
--ORDER BY ACTV_MONTH,ACTV_YEAR









GO

