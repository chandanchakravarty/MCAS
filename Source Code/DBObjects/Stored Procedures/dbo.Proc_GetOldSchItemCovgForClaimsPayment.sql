IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOldSchItemCovgForClaimsPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOldSchItemCovgForClaimsPayment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop PROC dbo.Proc_GetOldSchItemCovgForClaimsPayment 
--go

CREATE PROC dbo.Proc_GetOldSchItemCovgForClaimsPayment      
 @CLAIM_ID INT,      
 @ACTIVITY_ID INT      
AS                                
                                
BEGIN                                
IF EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID) --AND (VEHICLE_ID=0 OR VEHICLE_ID IS NULL)                    
BEGIN    
                
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
LIMIT_DEDUC_AMOUNT, OUTSTANDING,REINSURANCE_CARRIER,RI_RESERVE,CAP.RESERVE_ID,CAP.PAYMENT_AMOUNT,
 ISNULL(CAP.PAYMENT_ID,0) AS PAYMENT_ID,
 CATEGORY_TOTAL,                     
 ITEM_DETAIL_ID ,                                
 ITEM_DESCRIPTION,                                
 ITEM_INSURING_VALUE,--,MLV.LOOKUP_VALUE_DESC
 --Done for Itrack Issue 6635 on 28 Oct 09   
 MRCL.REIN_COMAPANY_NAME AS  REINSURANCECARRIER,
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
 --Done for Itrack Issue 6635 on 28 Oct 09
 LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST MRCL ON  
 MRCL.REIN_COMAPANY_ID = CLM_ACTIVITY_RESERVE.REINSURANCE_CARRIER  
 --LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON  
 --MLV.LOOKUP_UNIQUE_ID = CLM_ACTIVITY_RESERVE.REINSURANCE_CARRIER        
 LEFT OUTER JOIN CLM_ACTIVITY_PAYMENT CAP ON      
 CAP.CLAIM_ID = CLM_ACTIVITY_RESERVE.CLAIM_ID AND      
 CAP.RESERVE_ID = CLM_ACTIVITY_RESERVE.RESERVE_ID      
 
LEFT OUTER JOIN POL_DWELLINGS_INFO PDI WITH(NOLOCK)               
  ON  PDI.CUSTOMER_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.CUSTOMER_ID AND                           
     PDI.POLICY_ID = POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_ID AND                           
     PDI.POLICY_VERSION_ID=POL_HOME_OWNER_SCH_ITEMS_CVGS.POLICY_VERSION_ID

 WHERE CAP.CLAIM_ID=@CLAIM_ID AND CAP.ACTIVITY_ID=@ACTIVITY_ID --AND  MLV.IS_ACTIVE='Y'                                 
 ORDER BY POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS.ITEM_ID                                 
                
                
END            
ELSE
	SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND (VEHICLE_ID=0 OR VEHICLE_ID IS NULL)
END

--go
--exec Proc_GetOldSchItemCovgForClaimsPayment 2986,4
--rollback tran

GO

