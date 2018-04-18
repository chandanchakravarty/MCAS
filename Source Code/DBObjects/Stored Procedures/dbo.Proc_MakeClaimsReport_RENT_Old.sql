IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_RENT_Old]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_RENT_Old]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_MakeClaimsReport_RENT_Old]                                                  
@STATE_ID varchar(50),                                                 
@MONTH int,                                                 
@YEAR int                              
AS                                                                                  
BEGIN                                 
DECLARE @FIRE_FACTOR DECIMAL(18,2),                            
 @EC_FACTOR DECIMAL(18,2) ,                    
 @CLAIM_DATE DATETIME,                    
 @STATE_CODE VARCHAR(10)                    
                    
 SET @CLAIM_DATE = CONVERT(DATETIME, CAST(@MONTH AS VARCHAR(2)) + '/' + '01' + '/' + CAST(@YEAR AS VARCHAR(4)))                    
                    
 SET @STATE_CODE = (SELECT STATE_CODE FROM MNT_COUNTRY_STATE_LIST WHERE STATE_ID = @STATE_ID)                    
                    
 SET @FIRE_FACTOR = 0.84                             
 SET @EC_FACTOR = 0.16                            
                        
                            
IF EXISTS(SELECT * FROM MNT_CLAIM_FIREEC_PERCENT WHERE STATE_ID = @STATE_ID AND LOB_ID = '6' AND LOB_SUB_CATEGORY = 'Fire' AND EFF_FROM_DATETIME <= @CLAIM_DATE AND EFF_TO_DATETIME > @CLAIM_DATE)                        
BEGIN                            
 SET @FIRE_FACTOR = (SELECT PERCENTAGE/100.0 FROM MNT_CLAIM_FIREEC_PERCENT WHERE STATE_ID = @STATE_ID AND LOB_ID = '6' AND LOB_SUB_CATEGORY = 'Fire' AND EFF_FROM_DATETIME <= @CLAIM_DATE AND EFF_TO_DATETIME > @CLAIM_DATE)                          
END                
                
IF EXISTS(SELECT * FROM MNT_CLAIM_FIREEC_PERCENT WHERE STATE_ID = @STATE_ID AND LOB_ID = '6' AND LOB_SUB_CATEGORY = 'EC' AND EFF_FROM_DATETIME <= @CLAIM_DATE AND EFF_TO_DATETIME > @CLAIM_DATE)                        
BEGIN                            
 SET @EC_FACTOR = (SELECT PERCENTAGE/100.0 FROM MNT_CLAIM_FIREEC_PERCENT WHERE STATE_ID = @STATE_ID AND LOB_ID = '6' AND LOB_SUB_CATEGORY = 'EC' AND EFF_FROM_DATETIME <= @CLAIM_DATE AND EFF_TO_DATETIME > @CLAIM_DATE)                              
                           
   -- print @FIRE_FACTOR                        
   -- print @EC_FACTOR                    
   --  print  @CLAIM_DATE                       
END                            
----------START: Create Table for grouping Activities FINAL OUTPUT                
-- DROP TABLE  #TMP_ACTIVITY_FINAL                
                
CREATE TABLE #TMP_ACTIVITY_FINAL                
(LOB VARCHAR(100), STATE VARCHAR(10), COV_DES VARCHAR(100), AMOUNT DECIMAL(18,2),                
DETAIL_TYPE_DESCRIPTION VARCHAR(100), ACTV_MONTH INT, ACTV_YEAR INT)                
          
----------START: Create Table for grouping Activities           
        
----- Start : Incurred Losses Logic                
INSERT INTO #TMP_ACTIVITY_FINAL          
                  
SELECT   DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')                    
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                       
THEN 'EC'                  
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'                            
END) AS COV_DES, SUM(LOSSES_INCURRED) AS AMOUNT,               
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
FROM VW_CLAIM_TRANSACTION_DETAIL VWCLM INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID=VWCLM.CUSTOMER_ID AND PCPL.POLICY_ID=VWCLM.POLICY_ID             
AND PCPL.POLICY_VERSION_ID=VWCLM.POLICY_VERSION_ID          
          
WHERE (ACTV_MONTH <= @MONTH AND ACTV_YEAR = @YEAR )            
GROUP BY PCPL.STATE_ID,VWCLM.AGENCY_ID,PCPL.POLICY_LOB ,ACTV_YEAR,ACTV_MONTH            
) PAID_LOSS ON PAID_LOSS.STATE_ID=BE.STATE_ID AND PAID_LOSS.AGENCY_ID=BE.AGENCY_ID            
AND PAID_LOSS.LOB_ID=BE.LOB_ID AND PAID_LOSS.ACTV_YEAR=BE.YEAR_NUMBER AND PAID_LOSS.ACTV_MONTH=BE.MONTH_NUMBER            
WHERE (BE.MONTH_NUMBER <= @MONTH AND BE.YEAR_NUMBER = @YEAR )            
) PAID_LOSS_INCURRED                             
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON PAID_LOSS_INCURRED.CLAIM_ID = COD.CLAIM_ID                            
                           
WHERE PAID_LOSS_INCURRED.STATE_ID = @STATE_ID AND PAID_LOSS_INCURRED.LOB_ID = '6' AND PAID_LOSS_INCURRED.MONTH_NUMBER <= @MONTH AND PAID_LOSS_INCURRED.YEAR_NUMBER = @YEAR                 
GROUP BY PAID_LOSS_INCURRED.MONTH_NUMBER, PAID_LOSS_INCURRED.YEAR_NUMBER, COD.LOSS_TYPE              
----- End : Incurred Losses Logic           
              
-- SELECT * FROM CLM_TYPE_DETAIL                
-- DROP TABLE  #TMP_ACTIVITY                
                
       
----------END: Create Table for grouping Activities                
                
INSERT INTO #TMP_ACTIVITY_FINAL                 
                
SELECT DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                    
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')             
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                    
THEN 'EC'                      
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'               
END) AS COV_DES, SUM(                
CASE WHEN TMP.FACTOR IS NOT NULL THEN TMP.FACTOR * VW.AMOUNT                
ELSE                
VW.AMOUNT END) AS AMOUNT, --CASE WHEN TMP.ACTIVITY_DESC IS NOT NULL THEN     
TMP.ACTIVITY_DESC AS    
--ELSE VW.DETAIL_TYPE_DESCRIPTION END                
DETAIL_TYPE_DESCRIPTION,  VW.ACTV_MONTH, VW.ACTV_YEAR                                
FROM VW_CLAIM_TRANSACTION_DETAIL VW                             
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                            
LEFT OUTER JOIN CLM_CLAIM_TRANCACTIONS TMP ON TMP.ACTION_ON_PAYMENT = VW.ACTION_ON_PAYMENT                
                           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '6' AND VW.ACTV_MONTH <= @MONTH AND VW.ACTV_YEAR = @YEAR                 
AND TMP.FACTOR IS NOT NULL                             
GROUP BY VW.ACTV_MONTH, VW.ACTV_YEAR, --VW.DETAIL_TYPE_DESCRIPTION,     
COD.LOSS_TYPE, TMP.ACTIVITY_DESC                                                              
                
--DROP TABLE #TMP_ACTIVITY                
----------END: Create Table for grouping Activities FINAL OUTPUT                
                        
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'Fire' AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)*@FIRE_FACTOR AS AMOUNT,                           
'Written Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,                             
 AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP        
                         
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')          
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'Fire' AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)*@FIRE_FACTOR AS AMOUNT,                            
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                            
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                     
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                             
'Fire' AS COV_DES, SUM(AEP.EARNED_PREMIUM)*@FIRE_FACTOR AS AMOUNT,                            
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP        
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'Fire' AS COV_DES, SUM(AEP.ENDING_UNEARNED)*@FIRE_FACTOR AS AMOUNT,                           
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION,  AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                            
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                             
'Fire' AS COV_DES, SUM(AEP.INFORCE_PREMIUM)*@FIRE_FACTOR AS AMOUNT,                           
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND        
 AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                          
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'EC' AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)*@EC_FACTOR AS AMOUNT,                           
'Written Statistical Fees' AS DETAIL_TYPE_DESCRIPTION,  AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND AEP.COVERAGEID IN ('20007','20008','20009','20010','20011')                    
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'EC' AS COV_DES, SUM(AEP.WRITTEN_PREMIUM)*@EC_FACTOR AS AMOUNT,                           
'Written Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND AEP.COVERAGEID NOT IN ('20007','20008','20009','20010','20011')                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                                               
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                             
'EC' AS COV_DES, SUM(AEP.EARNED_PREMIUM)*@EC_FACTOR AS AMOUNT,                           
'Earned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                        
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                             
'EC' AS COV_DES, SUM(AEP.ENDING_UNEARNED)*@EC_FACTOR AS AMOUNT,                           
'Unearned Premium' AS DETAIL_TYPE_DESCRIPTION, AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP         
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'                     
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                            
SELECT DISTINCT 'Rental' AS LOB, @STATE_CODE AS STATE,                           
'EC' AS COV_DES, SUM(AEP.INFORCE_PREMIUM)*@EC_FACTOR AS AMOUNT,                           
'Inforce Premium' AS DETAIL_TYPE_DESCRIPTION,  AEP.MONTH_NUMBER AS ACTV_MONTH, AEP.YEAR_NUMBER                            
AS ACTV_YEAR                            
 FROM ACT_EARNED_PREMIUM AEP        
        
WHERE --PCPL.POLICY_LOB = '6' AND         
AEP.STATE_ID = @STATE_ID AND AEP.MONTH_NUMBER <= @MONTH AND AEP.YEAR_NUMBER = @YEAR                              
AND (SELECT TOP 1 PCPL.POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST PCPL                            
WHERE AEP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND AEP.POLICY_ID = PCPL.POLICY_ID                            
   ) = '6'         
GROUP BY AEP.MONTH_NUMBER, AEP.YEAR_NUMBER                            
                            
UNION                            
                                                                               
SELECT LOB , STATE , COV_DES , SUM(AMOUNT) ,                
DETAIL_TYPE_DESCRIPTION , ACTV_MONTH , ACTV_YEAR                 
                
 FROM #TMP_ACTIVITY_FINAL                
GROUP BY LOB , STATE , COV_DES , DETAIL_TYPE_DESCRIPTION , ACTV_MONTH , ACTV_YEAR                 
               
UNION              
              
SELECT DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                       
THEN 'EC'                      
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'                            
END) AS COV_DES, SUM(BEGIN_RESERVE) AS AMOUNT,               
'Beginning Reserve' DETAIL_TYPE_DESCRIPTION,  VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                
FROM VW_BEGINING_ENDING_RESERVE VW                             
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                            
                           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '6' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE              
                
UNION              
              
SELECT DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')            
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                       
THEN 'EC'                      
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'                            
END) AS COV_DES, SUM(END_RESERVE) AS AMOUNT,               
'Ending Reserve' DETAIL_TYPE_DESCRIPTION,  VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                
FROM VW_BEGINING_ENDING_RESERVE VW                             
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                            
                           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '6' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE              
                
UNION              
              
SELECT DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                       
THEN 'EC'                      
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'                            
END) AS COV_DES, SUM(BEGIN_RESERVE_REINSURANCE) AS AMOUNT,               
'Beginning Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION,  VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                
FROM VW_BEGINING_ENDING_RESERVE VW                             
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                            
                           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '6' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE              
                
UNION              
              
SELECT DISTINCT 'Rental' AS LOB,                            
@STATE_CODE AS STATE,                       
(CASE WHEN (COD.LOSS_TYPE + ',' LIKE '%32,%') OR (COD.LOSS_TYPE + ',' LIKE '%37,%') THEN 'Fire'                       
WHEN (COD.LOSS_TYPE + ',' LIKE '%26,%') OR (COD.LOSS_TYPE + ',' LIKE '%27,%')                    
OR (COD.LOSS_TYPE + ',' LIKE '%30,%') OR (COD.LOSS_TYPE + ',' LIKE '%31,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%33,%') OR (COD.LOSS_TYPE + ',' LIKE '%34,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%35,%') OR (COD.LOSS_TYPE + ',' LIKE '%36,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%38,%') OR (COD.LOSS_TYPE + ',' LIKE '%39,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%40,%') OR (COD.LOSS_TYPE + ',' LIKE '%41,%')                     OR (COD.LOSS_TYPE + ',' LIKE '%42,%') OR (COD.LOSS_TYPE + ',' LIKE '%43,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%44,%') OR (COD.LOSS_TYPE + ',' LIKE '%45,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%50,%') OR (COD.LOSS_TYPE + ',' LIKE '%56,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%58,%') OR (COD.LOSS_TYPE + ',' LIKE '%59,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%60,%') OR (COD.LOSS_TYPE + ',' LIKE '%62,%') OR (COD.LOSS_TYPE + ',' LIKE '%230,%')                       
THEN 'EC'                  
WHEN (COD.LOSS_TYPE + ',' LIKE '%51,%') OR (COD.LOSS_TYPE + ',' LIKE '%52,%')                       
OR (COD.LOSS_TYPE + ',' LIKE '%53,%') OR (COD.LOSS_TYPE + ',' LIKE '%55,%')                    
THEN 'Liability'                     
ELSE 'EC'                            
END) AS COV_DES, SUM(END_RESERVE_REINSURANCE) AS AMOUNT,               
'Ending Reinsurance Reserve' DETAIL_TYPE_DESCRIPTION,  VW.MONTH_NUMBER ACTV_MONTH, VW.YEAR_NUMBER ACTV_YEAR                                
FROM VW_BEGINING_ENDING_RESERVE VW          
LEFT JOIN CLM_OCCURRENCE_DETAIL COD ON VW.CLAIM_ID = COD.CLAIM_ID                            
                           
WHERE VW.STATE_ID = @STATE_ID AND VW.LOB_ID = '6' AND VW.MONTH_NUMBER <= @MONTH AND VW.YEAR_NUMBER = @YEAR                 
GROUP BY VW.MONTH_NUMBER, VW.YEAR_NUMBER, COD.LOSS_TYPE              
          
          
ORDER BY ACTV_MONTH, ACTV_YEAR                            
                    
DROP TABLE  #TMP_ACTIVITY_FINAL                
                            
END                                
                      
                    
                  
                  
                  
                


GO

