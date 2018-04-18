IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOldSchItemCovgForClaims]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOldSchItemCovgForClaims]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--DROP proc dbo.Proc_GetOldSchItemCovgForClaims  
--go
                  
CREATE proc dbo.Proc_GetOldSchItemCovgForClaims                          
 @CLAIM_ID INTEGER,
 @TRANSACTION_ID INT                  
AS                          
BEGIN                          

--DECLARE @TRANSACTION_ID INT

IF EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID)-- AND (VEHICLE_ID=0 OR VEHICLE_ID IS NULL))              
BEGIN              
 --SELECT @TRANSACTION_ID= ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y';

 SELECT                           
 POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID AS ITEM_DETAIL_ID_TOTAL,                           
 SUM (ISNULL(ITEM_INSURING_VALUE,0)) AS CATEGORY_TOTAL                          
 INTO #TMP_SCH_ITEMS_CATEGORY1_TOTAL1                          
 FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                          
 INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS                          
 ON                           
     POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID                          
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.POL_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID                           
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.POL_VERSION_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID                           
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID                           
 INNER JOIN CLM_CLAIM_INFO                           
 ON                           
     CLM_CLAIM_INFO.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID AND                                       
     CLM_CLAIM_INFO.POLICY_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID AND                                       
     CLM_CLAIM_INFO.POLICY_VERSION_ID=POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID                           
 WHERE CLM_CLAIM_INFO.CLAIM_ID=@CLAIM_ID                           
 GROUP BY POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID                           
                          
                           
 SELECT POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID, COV_DES AS CATEGORY_DESC,                           
 --POL_HOME_OWNER_SCH_ITEMS_CVGS.DEDUCTIBLE, MNT_COVERAGE_RANGES.LIMIT_DEDUC_ID,                          
 MNT_COVERAGE_RANGES.LIMIT_DEDUC_AMOUNT, CATEGORY_TOTAL                          
 INTO #TMP_SCH_ITEMS_CATEGORY1                          
 FROM POL_HOME_OWNER_SCH_ITEMS_CVGS                          
 INNER JOIN MNT_COVERAGE_RANGES                          
 ON POL_HOME_OWNER_SCH_ITEMS_CVGS.DEDUCTIBLE=MNT_COVERAGE_RANGES.LIMIT_DEDUC_ID                          
 INNER JOIN MNT_COVERAGE                           
 ON POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID=MNT_COVERAGE.COV_ID                          
 INNER JOIN #TMP_SCH_ITEMS_CATEGORY1_TOTAL1                          
 ON #TMP_SCH_ITEMS_CATEGORY1_TOTAL1.ITEM_DETAIL_ID_TOTAL=POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID                          
 INNER JOIN CLM_CLAIM_INFO                           
 ON  CLM_CLAIM_INFO.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID AND                                       
     CLM_CLAIM_INFO.POLICY_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID AND                                       
     CLM_CLAIM_INFO.POLICY_VERSION_ID=POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID                           
 WHERE CLM_CLAIM_INFO.CLAIM_ID=@CLAIM_ID                           
                           
 SELECT POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID, CATEGORY_DESC,                     
 LIMIT_DEDUC_AMOUNT, OUTSTANDING,REINSURANCE_CARRIER,RI_RESERVE,RESERVE_ID,                            
 CATEGORY_TOTAL,                  
 ITEM_DETAIL_ID ,                          
 ITEM_DESCRIPTION,                          
 ITEM_INSURING_VALUE,--,MLV.LOOKUP_VALUE_DESC AS  REINSURANCECARRIER
 PDI.DWELLING_ID AS DWELLING_ID,POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID AS ACTUAL_RISK_ID,'SCH' AS ACTUAL_RISK_TYPE                     
 FROM #TMP_SCH_ITEMS_CATEGORY1                          
 INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS                          
 ON POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID =#TMP_SCH_ITEMS_CATEGORY1.ITEM_ID                          
 INNER JOIN POL_HOME_OWNER_SCH_ITEMS_CVGS                          
 ON                           
     POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID                          
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.POL_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID                           
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.POL_VERSION_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID                           
     AND POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID                           
 INNER JOIN CLM_CLAIM_INFO                           
 ON  CLM_CLAIM_INFO.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID AND                                       
     CLM_CLAIM_INFO.POLICY_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID AND                                       
     CLM_CLAIM_INFO.POLICY_VERSION_ID=POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID                 
 JOIN CLM_ACTIVITY_RESERVE                
 ON                 
 CLM_ACTIVITY_RESERVE.COVERAGE_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.ITEM_ID AND                
 CLM_ACTIVITY_RESERVE.CLAIM_ID = CLM_CLAIM_INFO.CLAIM_ID  
LEFT OUTER JOIN POL_DWELLINGS_INFO PDI WITH(NOLOCK)               
  ON  PDI.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID AND                           
     PDI.POLICY_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID AND                           
     PDI.POLICY_VERSION_ID=POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID 
 --LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON  
 --MLV.LOOKUP_UNIQUE_ID = CLM_ACTIVITY_RESERVE.REINSURANCE_CARRIER  
 WHERE CLM_CLAIM_INFO.CLAIM_ID=@CLAIM_ID --AND MLV.IS_ACTIVE='Y' 
--Added by Asfa (22-Oct-2007) - To fetch the last reserve info.
 AND CLM_ACTIVITY_RESERVE.TRANSACTION_ID= CAST(@TRANSACTION_ID AS VARCHAR(50))  
 ORDER BY POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID                           
          
          
END             
END 

--go
--exec Proc_GetOldSchItemCovgForClaims 3506,1
--rollback tran

GO

