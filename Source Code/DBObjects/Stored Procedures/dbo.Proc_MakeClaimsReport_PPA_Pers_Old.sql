IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_PPA_Pers_Old]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_PPA_Pers_Old]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--
--drop proc dbo.Proc_MakeClaimsReport_PPA_Pers
--go 

/*----------------------------------------------------------                                                                          
Proc Name       : dbo.Proc_MakeClaimsReport_PPA_Pers                                          
Created by      : Asfa Praveen                                                                     
Date            : 20/08/2008                                                                          
Purpose         : Generates sum of Claims amount :- state, lob, month, year wise                      
Revewed by      : Mohit Agarwal                                                                         
Revison History :                                                                          
Used In        : Wolverine                                                                          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/                                                                          
--DROP PROC dbo.Proc_MakeClaimsReport_PPA_Pers '14',4,2009                                         
CREATE PROC [dbo].[Proc_MakeClaimsReport_PPA_Pers_Old]                                          
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
'Auto' AS Lob, @STATE_CODE AS STATE, 'Auto' AS COV_DES, VW.COVERAGE_ID,             
 SUM(  CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT                
ELSE  VW.AMOUNT END) AS AMOUNT,               
--CASE WHEN TMP.ACTIVITY_DESC IS NOT NULL THEN     
TMP.ACTIVITY_DESC AS    
--ELSE VW.DETAIL_TYPE_DESCRIPTION END                
DETAIL_TYPE_DESCRIPTION,  VW.ACTV_MONTH, VW.ACTV_YEAR                              
FROM VW_CLAIM_TRANSACTION_DETAIL VW                   
LEFT OUTER JOIN #TMP_ACTIVITY TMP ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT                
                  
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                            
AND TMP.FACTOR IS NOT NULL               
              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, --VW.DETAIL_TYPE_DESCRIPTION,     
TMP.ACTIVITY_DESC, VW.COVERAGE_ID,          
VW.CUSTOMER_ID, VW.POLICY_ID, VW.POLICY_VERSION_ID, VW.LOB_ID, VW.STATE_ID          
                
DROP TABLE #TMP_ACTIVITY                
----------END: Create Table for grouping Activities FINAL OUTPUT                
                 
--------- Temporary Table Coverage work ------              
CREATE TABLE #TEMP_CLM_COVERAGE_PPA_Pers              
( DETAIL_TYPE_DESCRIPTION VARCHAR(200), COV_DES VARCHAR(200),              
AMOUNT DECIMAL(18,2), ACTV_MONTH INT , ACTV_YEAR INT              
)                
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'BI'         
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID       
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN 
(
SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
UNION    
SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA 
)MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID         
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (2,114)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'CSL'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN 
(
SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
UNION    
SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA 
)MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (1,9,113,119)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'PD'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (4,36,115,118)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'MP'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
         
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (6)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'UM/BI'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (12,120)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'UIM'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (14,34,121,304)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'Collision'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (38,122)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'Comp'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (42,123)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                                  
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'Road Service'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID             
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (44,124)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'Rental Reimbursement'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID    
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.                     
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (45,125)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
              
              
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, 'PIP'                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
--INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID     
--CHANGED BY SHIKHA 28/03/09.                  
INNER JOIN (SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE     
   UNION    
   SELECT COV_ID,STATE_ID,LOB_ID,ASLOB FROM MNT_COVERAGE_EXTRA )MC    
ON MC.COV_ID = VW.COVERAGE_ID     
--END.                    
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' AND VW.COVERAGE_ID IN (1006)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_ID                                                      
             
/* test query           
INSERT INTO #TEMP_CLM_COVERAGE_PPA_Pers               
 SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, MC.COV_DES                   
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR       
FROM #TMP_ACTIVITY_FINAL VW                 
INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' --AND VW.COVERAGE_ID IN (1006)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_DES                                                      
   */           
--------- Temporary Table Coverage work ------              
                 
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE,             
'' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,             
'Written Statistical Fees' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
WHERE PCPL.POLICY_LOB = '2' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                  
AND AEP.COVERAGEID IN ('20007','20008') AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                
                
UNION                
                                                                   
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE, '' COV_DES, SUM(AEP.WRITTEN_PREMIUM) AS AMOUNT,             
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
                
WHERE PCPL.POLICY_LOB = '2' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                  
AND AEP.COVERAGEID NOT IN ('20007','20008')   AND PV.APP_USE_VEHICLE_ID = '11332'  
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                
                
UNION                
                                                       
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.EARNED_PREMIUM) AS AMOUNT,             
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
      
WHERE PCPL.POLICY_LOB = '2' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                  
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                
                
UNION                
                                                                   
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.ENDING_UNEARNED - AEP.BEGINNING_UNEARNED) AS AMOUNT,             
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL            
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
                
WHERE PCPL.POLICY_LOB = '2' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                  
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                
                
UNION                
                
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE,  '' COV_DES, SUM(AEP.INFORCE_PREMIUM) AS AMOUNT,             
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                
AS ACTV_YEAR                
 FROM ACT_EARNED_PREMIUM AEP INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
                
WHERE PCPL.POLICY_LOB = '2' AND AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                  
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                
                
UNION                
                                                                       
SELECT DISTINCT  'Auto' AS Lob, @STATE_CODE AS STATE,             
 COV_DES, AMOUNT,             
DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR     FROM #TEMP_CLM_COVERAGE_PPA_Pers              
/*              
UNION              
              
SELECT DISTINCT VW.DETAIL_TYPE_DESCRIPTION, MC.COV_DES                      
, SUM(VW.AMOUNT) AS AMOUNT , VW.ACTV_MONTH, VW.ACTV_YEAR                        
FROM #TMP_ACTIVITY_FINAL VW                 
INNER JOIN MNT_COVERAGE MC ON MC.COV_ID = VW.COVERAGE_ID                     
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = VW.CUSTOMER_ID AND PV.POLICY_ID = VW.POLICY_ID                
  AND PV.POLICY_VERSION_ID = VW.POLICY_VERSION_ID               
              
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                      
 AND PV.APP_USE_VEHICLE_ID = '11332' --AND VW.COVERAGE_ID IN (2,114)              
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, VW.DETAIL_TYPE_DESCRIPTION, MC.COV_DES                
*/              
        
UNION            
SELECT DISTINCT 'Auto' AS Lob, @STATE_CODE AS STATE, 'Auto' AS COV_DES,            
 SUM(BEGIN_RESERVE) AS AMOUNT,             
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                            
FROM VW_BEGINING_ENDING_RESERVE VW                 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON VW.POLICY_NUMBER = PCPL.POLICY_NUMBER AND VW.POLICY_ID = PCPL.POLICY_ID         
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
               
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR               
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                            
              
UNION            
SELECT DISTINCT 'Auto' AS Lob, @STATE_CODE AS STATE, 'Auto' AS COV_DES,            
 SUM(END_RESERVE) AS AMOUNT,             
'Ending Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                            
FROM VW_BEGINING_ENDING_RESERVE VW                 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON VW.POLICY_NUMBER = PCPL.POLICY_NUMBER AND VW.POLICY_ID = PCPL.POLICY_ID         
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
               
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR               
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                            
              
UNION            
SELECT DISTINCT 'Auto' AS Lob, @STATE_CODE AS STATE, 'Auto' AS COV_DES,            
 SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT,             
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                            
FROM VW_BEGINING_ENDING_RESERVE VW                 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON VW.POLICY_NUMBER = PCPL.POLICY_NUMBER AND VW.POLICY_ID = PCPL.POLICY_ID      
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
               
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR               
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER                                            
              
UNION            
SELECT DISTINCT 'Auto' AS Lob, @STATE_CODE AS STATE, 'Auto' AS COV_DES,            
 SUM(END_RESERVE_REINSURANCE) AS AMOUNT,             
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION, VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                            
FROM VW_BEGINING_ENDING_RESERVE VW                 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL                
ON VW.POLICY_NUMBER = PCPL.POLICY_NUMBER AND VW.POLICY_ID = PCPL.POLICY_ID         
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PV.POLICY_ID = PCPL.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID               
               
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '2' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR               
 AND PV.APP_USE_VEHICLE_ID = '11332'               
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER         
              
UNION      
      
SELECT DISTINCT 'Auto' AS LOB,                    
@STATE_CODE AS STATE,                   
'Auto' AS COV_DES, SUM(LOSSES_INCURRED) AS AMOUNT,           
'Incurred Loss' DETAIL_TYPE_DESCRIPTION,  PAID_LOSS_INCURRED.MONTH_NUMBER ACTV_MONTH, PAID_LOSS_INCURRED.YEAR_NUMBER ACTV_YEAR                                           
FROM (        
SELECT PAID_LOSS.CUSTOMER_ID, PAID_LOSS.POLICY_ID, PAID_LOSS.POLICY_VERSION_ID,      
PAID_LOSS.STATE_ID,PAID_LOSS.AGENCY_ID,PAID_LOSS.LOB_ID,BE.YEAR_NUMBER ,BE.MONTH_NUMBER,        
(ISNULL(END_RESERVE,0) + ISNULL(PAID_LOSS.LOSS_PAID ,0))- ISNULL(BEGIN_RESERVE,0) AS LOSSES_INCURRED,        
 PAID_LOSS.LOSS_PAID, BE.CLAIM_ID        
FROM VW_BEGINING_ENDING_RESERVE BE         
INNER JOIN (        
SELECT PCPL.CUSTOMER_ID, PCPL.POLICY_ID, PCPL.POLICY_VERSION_ID,      
PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB AS LOB_ID,ACTV_YEAR,ACTV_MONTH,        
ISNULL(SUM(CASE WHEN ACTION_ON_PAYMENT IN (180,181,176,177) THEN ISNULL(AMOUNT,0) ELSE 0 END),0) -        
ISNULL(SUM(CASE WHEN ACTION_ON_PAYMENT IN (63,64,70,188,240,242,243,244,245) THEN ISNULL(AMOUNT,0) ELSE 0 END),0)  AS LOSS_PAID        
FROM VW_CLAIM_TRANSACTION_DETAIL VWCLM        
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID=VWCLM.CUSTOMER_ID AND PCPL.POLICY_ID=VWCLM.POLICY_ID         
AND PCPL.POLICY_NUMBER=VWCLM.POLICY_NUMBER        
WHERE (ACTV_MONTH <= @MONTH AND ACTV_YEAR = @YEAR  )        
GROUP BY PCPL.CUSTOMER_ID, PCPL.POLICY_ID, PCPL.POLICY_VERSION_ID,      
PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB ,ACTV_YEAR,ACTV_MONTH        
) PAID_LOSS ON PAID_LOSS.STATE_ID=BE.STATE_ID AND PAID_LOSS.AGENCY_ID=BE.AGENCY_ID        AND PAID_LOSS.LOB_ID=BE.LOB_ID AND PAID_LOSS.ACTV_YEAR=BE.YEAR_NUMBER AND PAID_LOSS.ACTV_MONTH=BE.MONTH_NUMBER        
WHERE (BE.MONTH_NUMBER <= @MONTH AND BE.YEAR_NUMBER = @YEAR )        
) PAID_LOSS_INCURRED                         
                       
INNER JOIN POL_VEHICLES PV ON PV.CUSTOMER_ID = PAID_LOSS_INCURRED.CUSTOMER_ID AND PV.POLICY_ID = PAID_LOSS_INCURRED.POLICY_ID                
  AND PV.POLICY_VERSION_ID = PAID_LOSS_INCURRED.POLICY_VERSION_ID               
WHERE PAID_LOSS_INCURRED.STATE_ID = @STATE_ID AND PAID_LOSS_INCURRED.LOB_ID = '2' AND PAID_LOSS_INCURRED.MONTH_NUMBER <= @MONTH AND PAID_LOSS_INCURRED.YEAR_NUMBER = @YEAR             
 AND PV.APP_USE_VEHICLE_ID = '11332'               
      
GROUP BY PAID_LOSS_INCURRED.MONTH_NUMBER, PAID_LOSS_INCURRED.YEAR_NUMBER       
      
ORDER BY ACTV_MONTH, ACTV_YEAR                
--select * from #TEMP_CLM_COVERAGE_PPA_Pers             
DROP TABLE #TEMP_CLM_COVERAGE_PPA_Pers              
DROP TABLE  #TMP_ACTIVITY_FINAL               
END
--go
--
--exec Proc_MakeClaimsReport_PPA_Pers '14',9,2008 
--
--rollback tran                        
--                    
              
            
            
          




GO

