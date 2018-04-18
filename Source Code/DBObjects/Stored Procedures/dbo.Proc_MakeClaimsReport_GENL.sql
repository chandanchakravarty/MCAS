IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_GENL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_GENL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                      
Proc Name       : dbo.Proc_MakeClaimsReport_GENL                                      
Created by      : Asfa Praveen                                                                 
Date            : 20/08/2008                                                                      
Purpose         : Generates sum of Claims amount :- state, lob, month, year wise                  
Revewed by      : Mohit Agarwal                                                                     
Revison History :                                                                      
Used In        : Wolverine                                                                      
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                      
------   ------------       -------------------------*/                                                                      
--DROP PROC dbo.Proc_MakeClaimsReport_GENL '22',9,2008                                     
CREATE PROC [dbo].[Proc_MakeClaimsReport_GENL]                                      
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
(CUSTOMER_ID INT, POLICY_ID INT, POLICY_VERSION_ID INT, LOB_ID nvarchar(10), STATE_ID SMALLINT,      
LOB VARCHAR(100), STATE VARCHAR(10), COV_DES VARCHAR(100), COVERAGE_ID INT, AMOUNT DECIMAL(18,2),            
DETAIL_TYPE_DESCRIPTION VARCHAR(100), ACTV_MONTH INT, ACTV_YEAR INT)            
            
----------START: Create Table for grouping Activities            
-- SELECT * FROM CLM_TYPE_DETAIL            
-- DROP TABLE  #TMP_ACTIVITY            
            
CREATE TABLE #TMP_ACTIVITY            
(ACTIVITY_DESC VARCHAR(300), ACTION_ON_PAYMENT VARCHAR(100), FACTOR DECIMAL(18,2))            
            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense', '175', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense', '207', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense', '254', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense Reinsurance Recovered', '185', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense Reinsurance Recovered', '246', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Adjustment Expense Reinsurance Recovered', '249', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense', '174', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense', '203', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense', '255', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense Reinsurance Recovered', '184', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense Reinsurance Recovered', '247', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Legal Expense Reinsurance Recovered', '251', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Loss Reinsurance Recovered', '182', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Loss Reinsurance Recovered', '179', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Loss Reinsurance Recovered', '252', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense', '173', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense', '204', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense', '241', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense Reinsurance Recovered', '183', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense Reinsurance Recovered', '248', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Other Expense Reinsurance Recovered', '250', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Paid Loss', '180', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Paid Loss', '181', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Paid Loss', '188', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Paid Loss', '240', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Reins Returned Salvage / Subro Expense', '178', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Reins Returned Salvage / Subro Expense', '253', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Salvage Rec''d', '189', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Salvage Expense', '176', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Salvage Expense', '244', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Salvage Expense', '245', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Salvage Expense Reinsurance Recovered', '187', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Subrogation Rec''d', '190', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Subrogation Expense', '177', 1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Subrogation Expense', '242', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Subrogation Expense', '243', -1.0)            
INSERT INTO #TMP_ACTIVITY            
VALUES ('Subrogation Expense Reinsurance Recovered', '187', 1.0)            
----------END: Create Table for grouping Activities            
            
INSERT INTO #TMP_ACTIVITY_FINAL             
            
SELECT DISTINCT VW.CUSTOMER_ID, VW.POLICY_ID, VW.POLICY_VERSION_ID, VW.LOB_ID, VW.STATE_ID,      
'General Liabilty' AS Lob, @STATE_CODE AS STATE, 'General Liabilty' AS COV_DES, VW.COVERAGE_ID,         
 SUM(  CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT            
ELSE  VW.AMOUNT END) AS AMOUNT,           
--CASE WHEN TMP.ACTIVITY_DESC IS NOT NULL THEN 
TMP.ACTIVITY_DESC AS
--ELSE VW.DETAIL_TYPE_DESCRIPTION END            
DETAIL_TYPE_DESCRIPTION,  VW.ACTV_MONTH, VW.ACTV_YEAR                          
FROM VW_CLAIM_TRANSACTION_DETAIL VW               
LEFT OUTER JOIN #TMP_ACTIVITY TMP ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT            
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                        
AND TMP.FACTOR IS NOT NULL           
          
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, --VW.DETAIL_TYPE_DESCRIPTION, 
TMP.ACTIVITY_DESC, VW.COVERAGE_ID,      
VW.CUSTOMER_ID, VW.POLICY_ID, VW.POLICY_VERSION_ID, VW.LOB_ID, VW.STATE_ID      
            
DROP TABLE #TMP_ACTIVITY            
----------END: Create Table for grouping Activities FINAL OUTPUT            
                           
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE,         
'' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,         
'Written Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER            
AS ACTV_YEAR            
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID            
            
WHERE PCPL.POLICY_LOB = '7' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR              
AND AEP.COVERAGEID IN ('20007','20008')            
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER            
            
UNION            
                                                               
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,        
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER            
AS ACTV_YEAR            
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID            
            
WHERE PCPL.POLICY_LOB = '7' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR              
AND AEP.COVERAGEID NOT IN ('20007','20008')            
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER            
            
UNION            
                                                               
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.EARNED_PREMIUM) AS AMOUNT,         
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER            
AS ACTV_YEAR            
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID            
            
WHERE PCPL.POLICY_LOB = '7' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR              
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER            
            
UNION            
                                                               
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.ENDING_UNEARNED - AEP.BEGINNING_UNEARNED) AS AMOUNT,         
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER            
AS ACTV_YEAR            
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID            
            
WHERE PCPL.POLICY_LOB = '7' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR              
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER            
            
UNION            
            
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,         
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER            
AS ACTV_YEAR            
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID            
            
WHERE PCPL.POLICY_LOB = '7' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR              
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER            
            
UNION            
                                                                   
SELECT DISTINCT  'General Liabilty' AS Lob, @STATE_CODE AS STATE, MC.COV_DES                  
, SUM(VW.AMOUNT) AS AMOUNT,  VW.DETAIL_TYPE_DESCRIPTION, VW.ACTV_MONTH, VW.ACTV_YEAR                    
FROM #TMP_ACTIVITY_FINAL VW             
INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                 
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                  
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_DES                                                  
            
  UNION        
SELECT DISTINCT 'General Liability' AS Lob, @STATE_CODE AS STATE, 'General Liability' AS COV_DES,        
 SUM(BEGIN_RESERVE) AS AMOUNT,         
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                        
FROM VW_BEGINING_ENDING_RESERVE VW             
           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR           
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                        
          
UNION        
SELECT DISTINCT 'General Liability' AS Lob, @STATE_CODE AS STATE, 'General Liability' AS COV_DES,        
 SUM(END_RESERVE) AS AMOUNT,         
'Ending Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                        
FROM VW_BEGINING_ENDING_RESERVE VW             
           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR           
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                        
          
UNION        
SELECT DISTINCT 'General Liability' AS Lob, @STATE_CODE AS STATE, 'General Liability' AS COV_DES,        
 SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT,         
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                        
FROM VW_BEGINING_ENDING_RESERVE VW             
           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR           
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                        
          
UNION        
SELECT DISTINCT 'General Liability' AS Lob, @STATE_CODE AS STATE, 'General Liability' AS COV_DES,        
 SUM(END_RESERVE_REINSURANCE) AS AMOUNT,         
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                        
FROM VW_BEGINING_ENDING_RESERVE VW             
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '7' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR           
          
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER     
      
UNION  
  
SELECT   DISTINCT 'General Liability' AS LOB,                    
@STATE_CODE AS STATE,               
'General Liability' AS COV_DES, SUM(LOSSES_INCURRED) AS AMOUNT,       
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
AND PCPL.POLICY_NUMBER=VWCLM.POLICY_NUMBER    
WHERE (ACTV_MONTH <= @MONTH AND ACTV_YEAR = @YEAR  )    
GROUP BY PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB ,ACTV_YEAR,ACTV_MONTH    
) PAID_LOSS ON PAID_LOSS.STATE_ID=BE.STATE_ID AND PAID_LOSS.AGENCY_ID=BE.AGENCY_ID    
AND PAID_LOSS.LOB_ID=BE.LOB_ID AND PAID_LOSS.ACTV_YEAR=BE.YEAR_NUMBER AND PAID_LOSS.ACTV_MONTH=BE.MONTH_NUMBER    
WHERE (BE.MONTH_NUMBER <= @MONTH AND BE.YEAR_NUMBER = @YEAR )    
) PAID_LOSS_INCURRED                     
                   
WHERE PAID_LOSS_INCURRED.STATE_ID = @STATE_ID AND PAID_LOSS_INCURRED.LOB_ID = '7' AND PAID_LOSS_INCURRED.MONTH_NUMBER <= @MONTH AND PAID_LOSS_INCURRED.YEAR_NUMBER = @YEAR         
GROUP BY PAID_LOSS_INCURRED.MONTH_NUMBER, PAID_LOSS_INCURRED.YEAR_NUMBER   
        
ORDER BY ACTV_MONTH, ACTV_YEAR        
      
DROP TABLE #TMP_ACTIVITY_FINAL      
          
END                    
                
            
          
        
        
      
      
GO

