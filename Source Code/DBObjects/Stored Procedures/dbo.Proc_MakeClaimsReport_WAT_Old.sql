IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_WAT_Old]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_WAT_Old]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_MakeClaimsReport_WAT_Old]                                                
@STATE_ID varchar(50),                                               
@MONTH int,                                               
@YEAR int                            
AS                                                                                
BEGIN                 
                
 DECLARE @STATE_CODE VARCHAR(10)                
                
 SET @STATE_CODE = (SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID = @STATE_ID)                
                
 ----------START: Create Table for grouping Activities FINAL OUTPUT                
-- DROP TABLE  #TMP_ACTIVITY_FINAL                
                
CREATE TABLE #TMP_ACTIVITY_FINAL                
(LOB VARCHAR(100), STATE VARCHAR(10), COV_DES VARCHAR(100), AMOUNT DECIMAL(18,2),                
DETAIL_TYPE_DESCRIPTION VARCHAR(100), ACTV_MONTH INT, ACTV_YEAR INT)                
                
----------START: Create Table for grouping Activities             
          
----- Start : Incurred Losses Logic                
INSERT INTO #TMP_ACTIVITY_FINAL          
                  
SELECT   DISTINCT 'Boat' AS LOB,                            
@STATE_CODE AS STATE,                       
'Boat' AS COV_DES, SUM(LOSSES_INCURRED) AS AMOUNT,               
'Incurred Loss' DETAIL_TYPE_DESCRIPTION,  PAID_LOSS_INCURRED.MONTH_NUMBER ACTV_MONTH, PAID_LOSS_INCURRED.YEAR_NUMBER ACTV_YEAR                                               
FROM (            
SELECT PAID_LOSS.STATE_ID,PAID_LOSS.AGENCY_ID,PAID_LOSS.LOB_ID,BE.YEAR_NUMBER ,BE.MONTH_NUMBER,            
(ISNULL(END_RESERVE,0) + ISNULL(PAID_LOSS.LOSS_PAID ,0))- ISNULL(BEGIN_RESERVE,0) AS LOSSES_INCURRED,            
 PAID_LOSS.LOSS_PAID, BE.CLAIM_ID            
FROM VW_BEGINING_ENDING_RESERVE BE             
INNER JOIN (            
SELECT PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB AS LOB_ID,ACTV_YEAR,ACTV_MONTH,            
ISNULL(SUM(CASE WHEN ACTION_ON_PAYMENT IN (180,181,176,177) THEN ISNULL(AMOUNT,0) ELSE 0 END),0) -            
ISNULL(SUM(CASE WHEN ACTION_ON_PAYMENT IN (63,64,70,188,240,242,243,244,245) THEN ISNULL(AMOUNT,0) ELSE 0 END),0)  AS LOSS_PAID            
FROM VW_CLAIM_TRANSACTION_DETAIL VWCLM            
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID=VWCLM.CUSTOMER_ID AND PCPL.POLICY_ID=VWCLM.POLICY_ID             
AND PCPL.POLICY_VERSION_ID=VWCLM.POLICY_VERSION_ID            
WHERE (ACTV_MONTH <= @MONTH AND ACTV_YEAR = @YEAR  )            
GROUP BY PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB ,ACTV_YEAR,ACTV_MONTH            
) PAID_LOSS ON PAID_LOSS.STATE_ID=BE.STATE_ID AND PAID_LOSS.AGENCY_ID=BE.AGENCY_ID            
AND PAID_LOSS.LOB_ID=BE.LOB_ID AND PAID_LOSS.ACTV_YEAR=BE.YEAR_NUMBER AND PAID_LOSS.ACTV_MONTH=BE.MONTH_NUMBER            
WHERE (BE.MONTH_NUMBER <= @MONTH AND BE.YEAR_NUMBER = @YEAR )            
) PAID_LOSS_INCURRED                             
                           
WHERE PAID_LOSS_INCURRED.STATE_ID = @STATE_ID AND PAID_LOSS_INCURRED.LOB_ID = '4' AND PAID_LOSS_INCURRED.MONTH_NUMBER <= @MONTH AND PAID_LOSS_INCURRED.YEAR_NUMBER = @YEAR                 
GROUP BY PAID_LOSS_INCURRED.MONTH_NUMBER, PAID_LOSS_INCURRED.YEAR_NUMBER             
----- End : Incurred Losses Logic           
                  
-- SELECT * FROM CLM_TYPE_DETAIL                
-- DROP TABLE  #TMP_ACTIVITY                
                
           
----------END: Create Table for grouping Activities                
                
INSERT INTO #TMP_ACTIVITY_FINAL                 
                
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE, 'Boat' AS COV_DES,              
 SUM(  CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT                
ELSE  VW.AMOUNT END) AS AMOUNT,               
--CASE WHEN     
TMP.ACTIVITY_DESC AS    
--IS NOT NULL THEN TMP.ACTIVITY_DESC ELSE VW.DETAIL_TYPE_DESCRIPTION END                
DETAIL_TYPE_DESCRIPTION,  VW.ACTV_MONTH, VW.ACTV_YEAR                              
FROM VW_CLAIM_TRANSACTION_DETAIL VW                   
LEFT OUTER JOIN CLM_CLAIM_TRANCACTIONS TMP ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT                
                  
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '4' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                            
AND TMP.FACTOR IS NOT NULL               
              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR,-- VW.DETAIL_TYPE_DESCRIPTION,     
TMP.ACTIVITY_DESC                                                              
                
--DROP TABLE #TMP_ACTIVITY                
----------END: Create Table for grouping Activities FINAL OUTPUT                
              
                             
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE,                   
'Boat' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,                   
'Written Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                      
AS ACTV_YEAR                      
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '4' AND        
 AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                        
AND AEP.COVERAGEID IN ('20007','20008')                      
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                      
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID) = '4'        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                      
                      
UNION                      
                                                                         
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE,  'Boat' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,                  
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                      
AS ACTV_YEAR                      
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '4' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                        
AND AEP.COVERAGEID NOT IN ('20007','20008')                      
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                      
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID) = '4'        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                      
                      
UNION                      
                                                                         
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE,  'Boat' COV_DES, SUM(AEP.EARNED_PREMIUM) AS AMOUNT,                   
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                      
AS ACTV_YEAR                      
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '4' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                        
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                      
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID) = '4'        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                      
                      
UNION                      
                                                                         
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE,  'Boat' COV_DES, SUM(AEP.ENDING_UNEARNED) AS AMOUNT,                   
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                      
AS ACTV_YEAR                      
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '4' AND        
 AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                        
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                      
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID) = '4'        
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                      
                      
UNION                      
                      
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE,  'Boat' COV_DES, SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,                   
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                      
AS ACTV_YEAR                      
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '4' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                        
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                      
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID) = '4'        
                      
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                      
                      
UNION                      
SELECT LOB , STATE , COV_DES , SUM(AMOUNT) ,                
DETAIL_TYPE_DESCRIPTION , ACTV_MONTH , ACTV_YEAR                 
                
 FROM #TMP_ACTIVITY_FINAL                
GROUP BY LOB , STATE , COV_DES , DETAIL_TYPE_DESCRIPTION , ACTV_MONTH , ACTV_YEAR                 
              
UNION              
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE, 'Boat' AS COV_DES,              
 SUM(BEGIN_RESERVE) AS AMOUNT,               
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                              
FROM VW_BEGINING_ENDING_RESERVE VW                   
                 
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '4' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                              
                
UNION              
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE, 'Boat' AS COV_DES,              
 SUM(END_RESERVE) AS AMOUNT,               
'Ending Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                              
FROM VW_BEGINING_ENDING_RESERVE VW                   
                 
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '4' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                              
                
UNION              
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE, 'Boat' AS COV_DES,              
 SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT,               
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                              
FROM VW_BEGINING_ENDING_RESERVE VW                   
                 
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '4' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                              
                
UNION              
SELECT DISTINCT 'Boat' AS Lob, @STATE_CODE AS STATE, 'Boat' AS COV_DES,              
 SUM(END_RESERVE_REINSURANCE) AS AMOUNT,               
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR     
FROM VW_BEGINING_ENDING_RESERVE VW                   
                 
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '4' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                                                              
                      
ORDER BY ACTV_MONTH, ACTV_YEAR                  
              
DROP TABLE  #TMP_ACTIVITY_FINAL                
                  
END                              
                          
                      
                    
        


GO

