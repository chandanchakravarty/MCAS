IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_CLAIMSREPORT_RENT]'))
DROP VIEW [dbo].[VW_CLAIMSREPORT_RENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop VIEW dbo.VW_CLAIMSREPORT_RENT   
CREATE VIEW dbo.VW_CLAIMSREPORT_RENT                   
AS   
-- 180 - Paid Loss, Partial
-- 181 - Paid Loss, Final
-- 240 - Void - Paid Loss
SELECT  LOB, STATE_ID,LOSS_TYPE,  sum(AMOUNT) as Amount, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR 
from
(  
	SELECT  'Rental' AS LOB, TEMP.STATE_ID ,  
	CASE WHEN (LOSS_TYPE + ',' LIKE '%32,%') OR (LOSS_TYPE + ',' LIKE '%37,%')  
	THEN 'Fire'  
	WHEN (LOSS_TYPE + ',' LIKE '%51,%') OR (LOSS_TYPE + ',' LIKE '%52,%')OR (LOSS_TYPE + ',' LIKE '%53,%') OR (LOSS_TYPE + ',' LIKE '%55,%')                          
	THEN 'Liability'  
	ELSE 'EC'  
	END AS LOSS_TYPE, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR FROM  
	  
	(  
	 SELECT  COD.LOSS_TYPE ,VPLI.STATE_ID,  
	 SUM(VPLI.LOSSES_INCURRED) AS AMOUNT, 'Incurred Loss' DETAIL_TYPE_DESCRIPTION,    
	 VPLI.MONTH_NUMBER ACTV_MONTH, VPLI.YEAR_NUMBER ACTV_YEAR                                                   
	 FROM VW_PAID_LOSS_INCURRED VPLI  
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VPLI.CLAIM_ID = COD.CLAIM_ID                                
	 WHERE VPLI.LOB_ID = '6'   
	 --AND VPLI.MONTH_NUMBER <= @MONTH AND VPLI.YEAR_NUMBER = @YEAR                     
	 GROUP BY VPLI.MONTH_NUMBER, VPLI.YEAR_NUMBER, COD.LOSS_TYPE ,VPLI.STATE_ID                 
	  
	 UNION                     
	  
	 SELECT  COD.LOSS_TYPE, VW.STATE_ID,  
	 SUM( CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT                    
	 ELSE VW.AMOUNT END) AS AMOUNT, --TMP.ACTIVITY_DESC AS DETAIL_TYPE_DESCRIPTION,    
	 CASE WHEN TMP.ACTION_ON_PAYMENT IN (180,181,240) THEN 'Paid Loss'
		  WHEN TMP.ACTION_ON_PAYMENT IN (173,204,241)THEN 'Other Expense'
		  WHEN TMP.ACTION_ON_PAYMENT IN (254,175) THEN 'Adjustment Expense'
		  WHEN TMP.ACTION_ON_PAYMENT IN(189) THEN 'Salvage'
		  WHEN TMP.ACTION_ON_PAYMENT IN(176,244,245) THEN 'Salvage Expense'
		  WHEN TMP.ACTION_ON_PAYMENT IN(190) THEN 'Subrogation'
		  WHEN TMP.ACTION_ON_PAYMENT IN(177,242,243) THEN 'Subrogation Expense'
	 --TMP.ACTIVITY_DESC IN ('Void - Paid Loss','Paid Loss, Final','Paid Loss, Partial')
	 ELSE TMP.ACTIVITY_DESC END AS DETAIL_TYPE_DESCRIPTION,
	 VW.ACTV_MONTH, VW.ACTV_YEAR                                    
	 FROM VW_CLAIM_TRANSACTION_DETAIL VW                                 
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                                
	 INNER JOIN CLM_CLAIM_TRANCACTIONS TMP ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT                    
	 WHERE VW.LOB_ID = '6' --AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                     
	 AND TMP.FACTOR IS NOT NULL                                 
	 GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, COD.LOSS_TYPE, TMP.ACTIVITY_DESC, VW.STATE_ID,TMP.ACTION_ON_PAYMENT                                                                  
	  
	 UNION   
	  
	 SELECT  COD.LOSS_TYPE  ,VW.STATE_ID,                
	 SUM(BEGIN_RESERVE) AS AMOUNT,'Beginning Reserve' DETAIL_TYPE_DESCRIPTION,    
	 VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                    
	 FROM VW_BEGINING_ENDING_RESERVE VW                                 
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                                
	 WHERE VW.LOB_ID = '6' --AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                     
	 GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE, VW.STATE_ID                  
	  
	 UNION                  
	  
	 SELECT COD.LOSS_TYPE, VW.STATE_ID,   
	 SUM(END_RESERVE) AS AMOUNT, 'Ending Reserve' DETAIL_TYPE_DESCRIPTION,    
	 VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                    
	 FROM VW_BEGINING_ENDING_RESERVE VW                                 
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                                
	 WHERE VW.LOB_ID = '6' --AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                     
	 GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE, VW.STATE_ID                  
	  
	 UNION                  
	  
	 SELECT COD.LOSS_TYPE, VW.STATE_ID,   
	 SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT, 'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION,    
	 VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                    
	 FROM VW_BEGINING_ENDING_RESERVE VW                                 
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                  
	 WHERE VW.LOB_ID = '6' --AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                     
	 GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE, VW.STATE_ID                  
	  
	 UNION                  
	  
	 SELECT COD.LOSS_TYPE,VW.STATE_ID,  
	 SUM(END_RESERVE_REINSURANCE) AS AMOUNT,'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION,    
	 VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                    
	 FROM VW_BEGINING_ENDING_RESERVE VW   
	 LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                                
	 WHERE VW.LOB_ID = '6' --AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                     
	 GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE, VW.STATE_ID     
	) AS TEMP  
	GROUP BY LOSS_TYPE,STATE_ID, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR 
) As TEMP1
 GROUP BY LOB, LOSS_TYPE,STATE_ID, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR
  
--**********  
UNION  
--**********  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MLV.LOOKUP_VALUE_DESC AS COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,                                  
'Written Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP     
INNER JOIN MNT_COVERAGE_EXTRA MCE  
ON AEP.COVERAGEID = MCE.COV_ID  
INNER JOIN MNT_LOOKUP_VALUES MLV  
ON MCE.ASLOB = MLV.LOOKUP_UNIQUE_ID               
WHERE              
--AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
    ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,MLV.LOOKUP_VALUE_DESC,AEP.STATE_ID    
                                                                                     
UNION                           
                                                                                     
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MCFP.LOB_SUB_CATEGORY AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)* MCFP.PERCENTAGE/100 AS AMOUNT,                                  
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP   
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID     
INNER JOIN MNT_CLAIM_FIREEC_PERCENT MCFP    
ON MCFP.STATE_ID = AEP.STATE_ID                
WHERE              
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14168' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND  
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,MCFP.PERCENTAGE,MCFP.LOB_SUB_CATEGORY,AEP.STATE_ID                                  
                           
UNION  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
'Other Liability' AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)AS AMOUNT,                                  
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID     
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
    ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID   
                                   
UNION  
       
SELECT 'Rental' AS LOB, AEP.STATE_ID,                          
MCFP.LOB_SUB_CATEGORY AS COV_DES, SUM(AEP.EARNED_PREMIUM)* MCFP.PERCENTAGE/100 AS AMOUNT,                                  
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID    
INNER JOIN MNT_CLAIM_FIREEC_PERCENT MCFP    
ON MCFP.STATE_ID = AEP.STATE_ID                
WHERE              
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14168' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,MCFP.PERCENTAGE,MCFP.LOB_SUB_CATEGORY,AEP.STATE_ID                                  
                                  
UNION  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
'Other Liability' AS COV_DES, SUM(AEP.EARNED_PREMIUM)AS AMOUNT,                                  
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID     
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID                                   
   
UNION  
                                                                                    
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MCFP.LOB_SUB_CATEGORY AS COV_DES, SUM(AEP.ENDING_UNEARNED)* MCFP.PERCENTAGE/100 AS AMOUNT,                                  
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID       
INNER JOIN MNT_CLAIM_FIREEC_PERCENT MCFP    
ON MCFP.STATE_ID = AEP.STATE_ID 
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14168' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR  AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                        
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'   
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,MCFP.PERCENTAGE,MCFP.LOB_SUB_CATEGORY,AEP.STATE_ID                                  
  
UNION  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
'Other Liability' AS COV_DES, SUM(AEP.ENDING_UNEARNED)AS AMOUNT,                                  
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID     
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID   
                                
UNION                                 
                                  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MCFP.LOB_SUB_CATEGORY AS COV_DES, SUM(AEP.INFORCE_PREMIUM)* MCFP.PERCENTAGE/100 AS AMOUNT,                                  
'Inforce Premium ' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID      
INNER JOIN MNT_CLAIM_FIREEC_PERCENT MCFP    
ON MCFP.STATE_ID = AEP.STATE_ID                
WHERE              
--AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14168' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR  AND   
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,MCFP.PERCENTAGE,MCFP.LOB_SUB_CATEGORY ,AEP.STATE_ID                                
  
UNION  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
'Other Liability' AS COV_DES, SUM(AEP.INFORCE_PREMIUM)AS AMOUNT,                                  
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP  
INNER JOIN MNT_COVERAGE MC  
ON AEP.COVERAGEID = MC.COV_ID     
WHERE --PCPL.POLICY_LOB = '6' AND AEP.STATE_ID = @STATE_ID AND   
MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND  
AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID  
  
--Added for Stat Fees by Shikha on 28/03/09.  
  
UNION  
  
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MLV.LOOKUP_VALUE_DESC AS COV_DES, SUM(AEP.EARNED_PREMIUM)AS AMOUNT,                                  
'Earned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
FROM ACT_EARNED_PREMIUM AEP     
INNER JOIN MNT_COVERAGE_EXTRA MCE  
ON AEP.COVERAGEID = MCE.COV_ID  
INNER JOIN MNT_LOOKUP_VALUES MLV  
ON MCE.ASLOB = MLV.LOOKUP_UNIQUE_ID     
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
--MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                                  
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,MLV.LOOKUP_VALUE_DESC                                  
   
UNION  
                                                                                    
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MLV.LOOKUP_VALUE_DESC AS COV_DES, SUM(AEP.ENDING_UNEARNED)AS AMOUNT,                                  
'Unearned Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
--FROM ACT_EARNED_PREMIUM AEP  
--INNER JOIN MNT_COVERAGE MC  
--ON AEP.COVERAGEID = MC.COV_ID    
FROM ACT_EARNED_PREMIUM AEP     
INNER JOIN MNT_COVERAGE_EXTRA MCE  
ON AEP.COVERAGEID = MCE.COV_ID  
INNER JOIN MNT_LOOKUP_VALUES MLV  
ON MCE.ASLOB = MLV.LOOKUP_UNIQUE_ID  
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
--MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                         
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,MLV.LOOKUP_VALUE_DESC  

UNION  
                                                                                    
SELECT 'Rental' AS LOB, AEP.STATE_ID,                                 
MLV.LOOKUP_VALUE_DESC AS COV_DES, SUM(AEP.INFORCE_PREMIUM)AS AMOUNT,                                  
'Inforce Premium Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                                  
AS ACTV_YEAR                                  
--FROM ACT_EARNED_PREMIUM AEP  
--INNER JOIN MNT_COVERAGE MC  
--ON AEP.COVERAGEID = MC.COV_ID    
FROM ACT_EARNED_PREMIUM AEP     
INNER JOIN MNT_COVERAGE_EXTRA MCE  
ON AEP.COVERAGEID = MCE.COV_ID  
INNER JOIN MNT_LOOKUP_VALUES MLV  
ON MCE.ASLOB = MLV.LOOKUP_UNIQUE_ID  
WHERE               
--AEP.STATE_ID = @STATE_ID AND   
--MC.ASLOB = '14171' AND --AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR AND   
AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')                                  
AND     
 (    
   SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                         
   WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                                  
 ) = '6'                           
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER,AEP.STATE_ID,MLV.LOOKUP_VALUE_DESC  

--Proc_MakeClaimsReport_RENT 22,12,2009
















GO

