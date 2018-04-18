IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_EXPENSE_HOME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_EXPENSE_HOME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN	
--DROP PROC Proc_GetCLM_ACTIVITY_EXPENSE_HOME
--GO
/*----------------------------------------------------------                                                                                                                                                              
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_EXPENSE_HOME                                                                                                                                                  
Created by      : Sumit Chhabra                                                                                                                                                            
Date            : 27/07/2006                                                                                                                                                              
Purpose         : Fetch data from CLM_ACTIVITY_EXPENSE table for home claim reserve screen                                                                                                                                          
Created by      : Sumit Chhabra                                                                                                                                                             
Revison History :                                                                                                                                                              
Used In        : Wolverine                                                                                                                                                              
------------------------------------------------------------                                                                                                                                                              
Date     Review By          Comments                                                                                                                                                              
------   ------------       -------------------------*/                                                                                                                                                              
--DROP PROC Proc_GetCLM_ACTIVITY_EXPENSE_HOME              
CREATE PROC dbo.Proc_GetCLM_ACTIVITY_EXPENSE_HOME                                                                                                                                                    
@CLAIM_ID int,            
@ACTIVITY_ID int                                                                                          
AS                                                                                                                                                              
BEGIN                                                                                    
                    
DECLARE @SECTION1_COVERAGES VARCHAR(5)                    
DECLARE @SECTION2_COVERAGES VARCHAR(5);           
SET @SECTION1_COVERAGES = 'S1'                                                                  
SET @SECTION2_COVERAGES = 'S2'                                                                  
                    
--Section I Coverages                    
SELECT DISTINCT
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                      
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                    
  CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,PVC.DWELLING_ID,MC.COV_ID,                    
  MC.COV_CODE, COV_DES AS COV_DESC,        
 --PVC.DWELLING_ID AS DWELLING,        
  ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')            
  + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,         
 CAP.ACTION_ON_PAYMENT AS EXPENSE_ACTION_ON_PAYMENT ,CAP.EXPENSE_ID,ISNULL(CAP.PAYMENT_METHOD,'') AS EXPENSE_PAYMENT_METHOD, ISNULL(CAP.CHECK_NUMBER,'') AS CHECK_NUMBER ,CAP.ADDITIONAL_EXPENSE,CAP.PAYMENT_AMOUNT,CAP.DRACCTS,CAP.CRACCTS,  
  
  CASE MC.LIMIT_TYPE                      
   WHEN 2 THEN  SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, +                     
   CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                      
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'')) + '/' +                      
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                       
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))                      
  ELSE                     
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                     
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,''))                      
  END AS LIMIT ,                     
 CASE MC.DEDUCTIBLE_TYPE                      
  WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' ' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                      
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0))  +                      
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                      
  ELSE                     
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) +  ' ' +                     
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                      
  END AS DEDUCTIBLE,                    
  MC.COVERAGE_TYPE,                     
  --Done for Itrack Issue 7663
  --ISNULL(PVC.DEDUCTIBLE_TEXT,'') ELSE ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(PVC.DEDUCTIBLE_TEXT,'')                                                       
  CASE MC.COV_ID WHEN 169 THEN ISNULL(PVC.DEDUCTIBLE_TEXT,'') ELSE ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(PVC.DEDUCTIBLE_TEXT,'') END AS DEDUCTIBLE2, MC.RANK,
  CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CAR.VEHICLE_ID 
 FROM                     
  CLM_CLAIM_INFO CCI                     
 JOIN                     
 CLM_ACTIVITY_EXPENSE CAP            
 ON            
  CAP.CLAIM_ID = CCI.CLAIM_ID            
 JOIN            
  CLM_ACTIVITY_RESERVE CAR                      
 ON                     
  CAP.CLAIM_ID = CAR.CLAIM_ID AND            
 CAP.RESERVE_ID = CAR.RESERVE_ID            
 JOIN                     
  POL_DWELLING_SECTION_COVERAGES PVC                     
 ON                     
  CCI.POLICY_ID = PVC.POLICY_ID AND  CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND                      
  CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND                      
  CAR.VEHICLE_ID = PVC.DWELLING_ID                      
 JOIN                     
  MNT_COVERAGE MC                     
 ON                     
  MC.COV_ID = CAR.COVERAGE_ID                      
 LEFT JOIN                     
  POL_DWELLINGS_INFO PDI                     
 ON                     
  PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND PDI.POLICY_ID=CCI.POLICY_ID AND  PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                     
  PDI.DWELLING_ID = PVC.DWELLING_ID                      
 LEFT JOIN                     
  CLM_INSURED_LOCATION CIV                     
 ON       
  CCI.CLAIM_ID = CIV.CLAIM_ID AND CIV.POLICY_LOCATION_ID=PDI.LOCATION_ID                      
 LEFT OUTER JOIN                     
  MNT_LOOKUP_VALUES MLV           
 ON                     
  MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER           
 LEFT OUTER JOIN          
POL_LOCATIONS PL          
 ON          
 PL.CUSTOMER_ID=CCI.CUSTOMER_ID AND PL.POLICY_ID=CCI.POLICY_ID AND  PL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                       
  PL.LOCATION_ID = CIV.POLICY_LOCATION_ID          
 LEFT OUTER JOIN          
 MNT_COUNTRY_STATE_LIST MCSL         
 ON          
 PL.LOC_STATE = MCSL.STATE_ID       
 WHERE                      
  CAP.CLAIM_ID=@CLAIM_ID AND MC.COVERAGE_TYPE= CAST(@SECTION1_COVERAGES AS VARCHAR) AND CIV.CLAIM_ID=@CLAIM_ID             
 AND CAP.ACTIVITY_ID = @ACTIVITY_ID                    
 ORDER BY                     
   CAR.VEHICLE_ID , CAP.ACTUAL_RISK_TYPE, CAP.ACTUAL_RISK_ID , MC.RANK,CAR.RESERVE_ID              
                    
                    
--Section II Coverages                    
 SELECT DISTINCT                     
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                      
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                    
  CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,PVC.DWELLING_ID,MC.COV_ID,                    
  MC.COV_CODE, COV_DES AS COV_DESC,        
 --PVC.DWELLING_ID AS DWELLING,        
 ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')            
  + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,         
 CAP.ACTION_ON_PAYMENT AS EXPENSE_ACTION_ON_PAYMENT, CAP.EXPENSE_ID,CAP.ADDITIONAL_EXPENSE,CAP.PAYMENT_AMOUNT,CAP.DRACCTS,CAP.CRACCTS,  
  CASE MC.LIMIT_TYPE                      
   WHEN 2 THEN  SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, +                     
   CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                      
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,'')) + '/' +                      
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                       
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))                      
  ELSE                     
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                     
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,''))                      
  END AS LIMIT ,                     
 CASE MC.DEDUCTIBLE_TYPE                      
  WHEN 2 THEN SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' ' + CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                      
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0))  +                      
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                      
  ELSE                     
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) +  ' ' +                     
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                      
END AS DEDUCTIBLE,                    
  MC.COVERAGE_TYPE,                     
  ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(PVC.DEDUCTIBLE_TEXT,'') AS DEDUCTIBLE2,MC.RANK,
 CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CAR.VEHICLE_ID                      
 FROM                     
  CLM_CLAIM_INFO CCI            
 JOIN                     
 CLM_ACTIVITY_EXPENSE CAP            
 ON            
  CAP.CLAIM_ID = CCI.CLAIM_ID            
 JOIN            
  CLM_ACTIVITY_RESERVE CAR                      
 ON                     
  CAP.CLAIM_ID = CAR.CLAIM_ID AND         
 CAP.RESERVE_ID = CAR.RESERVE_ID                     
 JOIN                     
  POL_DWELLING_SECTION_COVERAGES PVC                   
 ON                     
  CCI.POLICY_ID = PVC.POLICY_ID AND  CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND                      
  CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND                     
  CAR.VEHICLE_ID = PVC.DWELLING_ID                      
 JOIN                     
  MNT_COVERAGE MC    
 ON                     
  MC.COV_ID = CAR.COVERAGE_ID                      
 LEFT JOIN                     
  POL_DWELLINGS_INFO PDI                     
 ON                     
  PDI.CUSTOMER_ID=CCI.CUSTOMER_ID AND PDI.POLICY_ID=CCI.POLICY_ID AND  PDI.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                     
  PDI.DWELLING_ID = PVC.DWELLING_ID                      
 LEFT JOIN                     
  CLM_INSURED_LOCATION CIV                     
 ON       
  CCI.CLAIM_ID = CIV.CLAIM_ID AND CIV.POLICY_LOCATION_ID=PDI.LOCATION_ID                      
 LEFT OUTER JOIN                     
  MNT_LOOKUP_VALUES MLV                      
 ON                     
  MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER            
LEFT OUTER JOIN          
 POL_LOCATIONS PL          
 ON          
 PL.CUSTOMER_ID=CCI.CUSTOMER_ID AND PL.POLICY_ID=CCI.POLICY_ID AND  PL.POLICY_VERSION_ID=CCI.POLICY_VERSION_ID AND                       
  PL.LOCATION_ID = CIV.POLICY_LOCATION_ID          
 LEFT OUTER JOIN          
 MNT_COUNTRY_STATE_LIST MCSL          
 ON          
 PL.LOC_STATE = MCSL.STATE_ID                    
 WHERE                      
  CAP.CLAIM_ID=@CLAIM_ID AND MC.COVERAGE_TYPE= CAST(@SECTION2_COVERAGES AS VARCHAR) AND             
 CIV.CLAIM_ID=@CLAIM_ID  AND CAP.ACTIVITY_ID = @ACTIVITY_ID            
 ORDER BY CAR.VEHICLE_ID , CAP.ACTUAL_RISK_TYPE, CAP.ACTUAL_RISK_ID , MC.RANK,CAR.RESERVE_ID                     
                    
--Scheduled Item Coverages                    
/* SELECT                  
  P.ITEM_ID AS COV_ID,MC.COV_DES AS COV_DESC,MR.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,                  
  CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,CAP.ACTION_ON_PAYMENT, CAP.EXPENSE_ID,CAP.ADDITIONAL_EXPENSE,                 
   CAR.REINSURANCE_CARRIER,CAP.PAYMENT_AMOUNT,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                  
  ISNULL(SUM(D.ITEM_INSURING_VALUE),0) AS SCHEDULED_ITEM_COVERAGE_AMOUNT          
 FROM                              
 CLM_ACTIVITY_EXPENSE CAP            
 LEFT JOIN                   
  CLM_CLAIM_INFO CCI                   
 ON                  
  CAP.CLAIM_ID = CCI.CLAIM_ID              
 JOIN            
  CLM_ACTIVITY_RESERVE CAR                      
 ON                     
  CAP.CLAIM_ID = CAR.CLAIM_ID AND            
 CAP.RESERVE_ID = CAR.RESERVE_ID                   
 LEFT JOIN                   
  POL_HOME_OWNER_SCH_ITEMS_CVGS P                  
 ON                   
  P.CUSTOMER_ID = CCI.CUSTOMER_ID AND P.POLICY_ID = CCI.POLICY_ID AND P.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID AND                   
  P.ITEM_ID = CAR.COVERAGE_ID                  
 LEFT JOIN                   
  MNT_COVERAGE MC                   
 ON                  
  CAR.COVERAGE_ID = MC.COV_ID                  
 LEFT JOIN                   
  MNT_COVERAGE_RANGES MR                   
 ON                  
  MR.COV_ID = CAR.COVERAGE_ID AND                  
  P.DEDUCTIBLE = MR.LIMIT_DEDUC_ID                  
 LEFT JOIN                   
  POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS D                  
 ON                   
  CCI.CUSTOMER_ID = D.CUSTOMER_ID AND CCI.POLICY_ID = D.POL_ID AND CCI.POLICY_VERSION_ID = D.POL_VERSION_ID AND                   
  CAR.COVERAGE_ID = D.ITEM_ID               
 LEFT OUTER JOIN                     
  MNT_LOOKUP_VALUES MLV                  
 ON                     
  MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER                 
 WHERE    
  CAP.CLAIM_ID=@CLAIM_ID AND ISNULL(D.IS_ACTIVE,'N')='Y' AND CAR.VEHICLE_ID=0            
 AND CAP.ACTIVITY_ID = @ACTIVITY_ID 
--vehicle_id will be 0 in case of scheduled item        
 GROUP BY                         
  P.ITEM_ID,MC.COV_DES,MR.LIMIT_DEDUC_AMOUNT,                  
  CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                  
  CAR.REINSURANCE_CARRIER,CAP.ACTION_ON_PAYMENT,CAP.EXPENSE_ID,CAP.ADDITIONAL_EXPENSE,CAP.PAYMENT_AMOUNT,MLV.LOOKUP_VALUE_DESC       
ORDER BY MC.COV_DES    
*/                
                    
--Home Watercraft Coverages                    
 SELECT DISTINCT                
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                  
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,CAP.DRACCTS,CAP.CRACCTS,  
  --PVC.BOAT_ID AS DWELLING_ID,
  CASE ISNULL(PVC.BOAT_ID,'') WHEN '' THEN CAR.VEHICLE_ID ELSE PVC.BOAT_ID END AS DWELLING_ID,
  --MC.COV_ID,
  CASE ISNULL(MC.COV_ID,'') WHEN '' THEN MCE.COV_ID ELSE MC.COV_ID END AS COV_ID,
   MC.COV_CODE,
  --COV_DES AS COV_DESC, 
  CASE WHEN CAR.COVERAGE_ID = 20001 THEN 'Section 1 - Covered Property Damage - Actual Cash Value' 
  WHEN CAR.COVERAGE_ID = 20002 THEN 'Section 1 - Covered Property Damage - Actual Cash Value'
  WHEN CAR.COVERAGE_ID = 20003 THEN 'Section 1 - Covered Property Damage - Actual Cash Value'
  ELSE MC.COV_DES END AS COV_DESC, 
  CAP.ACTION_ON_PAYMENT AS EXPENSE_ACTION_ON_PAYMENT, CAP.EXPENSE_ID,CAP.ADDITIONAL_EXPENSE,    
 CAP.PAYMENT_AMOUNT,            
  (CAST(CIV.YEAR AS VARCHAR)+'-'+CAST(CIV.MAKE AS VARCHAR)+'-'+CAST(CIV.MODEL AS VARCHAR)+'-'+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS DWELLING,                
  CASE MC.LIMIT_TYPE  WHEN 2 THEN                  
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, + CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                  
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))            + '/' +                  
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                   
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))                  
  ELSE                 
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                 
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT1_AMOUNT_TEXT,''))  END AS LIMIT ,                 
   CASE MC.DEDUCTIBLE_TYPE  WHEN 2 THEN                 
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' '+                 
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,'')) + '/' +                  
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_2),1),0))  +                  
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE2_AMOUNT_TEXT,''))                  
  ELSE                 
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.DEDUCTIBLE_1),1),0)) + ' '+                 
   CONVERT(VARCHAR(20),ISNULL(PVC.DEDUCTIBLE1_AMOUNT_TEXT,''))                  
  END AS DEDUCTIBLE,                  
  --MC.COVERAGE_TYPE
  CASE ISNULL(MC.COVERAGE_TYPE,'') WHEN '' THEN MCE.COVERAGE_TYPE ELSE MC.COVERAGE_TYPE END AS COVERAGE_TYPE,
  CASE ISNULL(MC.RANK,'0.0') WHEN '0.0' THEN MCE.RANK ELSE MC.RANK END AS RANK,
  CAP.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,
  CAP.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CAR.VEHICLE_ID                                          
 FROM            
 CLM_ACTIVITY_EXPENSE CAP            
 LEFT OUTER JOIN                   
  CLM_CLAIM_INFO CCI                   
 ON                  
  CAP.CLAIM_ID = CCI.CLAIM_ID              
 LEFT OUTER JOIN            
  CLM_ACTIVITY_RESERVE CAR                      
 ON                     
  CAP.CLAIM_ID = CAR.CLAIM_ID AND            
 CAP.RESERVE_ID = CAR.RESERVE_ID                 
 LEFT OUTER JOIN                 
  POL_WATERCRAFT_COVERAGE_INFO PVC         
 ON                 
  CCI.POLICY_ID = PVC.POLICY_ID AND  CCI.POLICY_VERSION_ID = PVC.POLICY_VERSION_ID AND                  
  CCI.CUSTOMER_ID = PVC.CUSTOMER_ID AND CAR.COVERAGE_ID=PVC.COVERAGE_CODE_ID AND                  
  CAR.VEHICLE_ID = PVC.BOAT_ID       
 LEFT OUTER JOIN                 
  MNT_COVERAGE MC                 
 ON                 
  MC.COV_ID = CAR.COVERAGE_ID                  
 LEFT                 
  OUTER JOIN CLM_INSURED_BOAT CIV                 
 ON                 
  CAR.VEHICLE_ID = CIV.POLICY_BOAT_ID                 
LEFT                 
  OUTER JOIN MNT_LOOKUP_VALUES MLV                  
 ON                 
  MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER 
 LEFT OUTER JOIN  MNT_COVERAGE_EXTRA MCE ON           
 MCE.COV_ID = CAR.COVERAGE_ID
 AND MCE.COV_ID IN (20001,20002,20003)                 
 WHERE                  
  CAP.CLAIM_ID=@CLAIM_ID AND  CIV.CLAIM_ID=@CLAIM_ID  AND CAP.ACTIVITY_ID = @ACTIVITY_ID
  AND (ISNULL(MC.COVERAGE_TYPE,'') = 'RL'  OR ISNULL(MCE.COVERAGE_TYPE,'') = 'RL')            
 ORDER BY                 
   CAR.RESERVE_ID, CAR.VEHICLE_ID , CAP.ACTUAL_RISK_TYPE, CAP.ACTUAL_RISK_ID                 
  
exec Proc_GetOldSchItemCovgForClaimsExpense @CLAIM_ID,@ACTIVITY_ID
EXEC Proc_GetOldWaterEquipCovgForClaimsExpense @CLAIM_ID,@ACTIVITY_ID    
EXEC Proc_GetOldRecVehCovgForClaimsExpense @CLAIM_ID,@ACTIVITY_ID

END

--GO
--exec Proc_GetCLM_ACTIVITY_EXPENSE_HOME 2989,4
--ROLLBACK Tran

GO

