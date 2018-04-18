IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_RESERVE_HOME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_RESERVE_HOME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_HOME
--go
/*----------------------------------------------------------                                                                                                                                                                          
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_RESERVE_HOME                                                                                                                                                              
Created by      : Sumit Chhabra                                                                                                                                                                        
Date            : 27/07/2006                                                                                                                                                                          
Purpose         : Fetch data from CLM_ACTIVITY_RESERVE table for home claim reserve screen                                                                                                                                                      
Created by      : Sumit Chhabra                                                                                                                                                                         
Revison History :                                                                                                                                                                          
Used In        : Wolverine                                                                                                                                                                          
------------------------------------------------------------                                                                                                                                                                          
Date     Review By          Comments                                                                                                                                                                          
------   ------------       -------------------------*/                                                                                                                                                                          
-- drop PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_HOME                                         
CREATE PROC [dbo].[Proc_GetCLM_ACTIVITY_RESERVE_HOME]                                                                   (                                                                                           
  @CLAIM_ID int    ,                                                                                      
  @ACTIVITY_ID int=null                
)                                                                                                      
AS                                                                                                                                                                          
BEGIN                                                                                                
                                
DECLARE @SECTION1_COVERAGES VARCHAR(5)                                
DECLARE @SECTION2_COVERAGES VARCHAR(5);                  
DECLARE @TRANSACTION_ID INT              
DECLARE @ACTIVTY_ID INT                
                                
SET @SECTION1_COVERAGES = 'S1'                                                                              
SET @SECTION2_COVERAGES = 'S2'                      
                  
 IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID)                  
 RETURN -1                  
                                
 IF(@ACTIVITY_ID IS NOT NULL)         
 BEGIN             
  SELECT @TRANSACTION_ID= ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND  IS_ACTIVE='Y' AND ACTIVITY_ID=@ACTIVITY_ID;             
END              
ELSE           
BEGIN           
--Added for Itrack Issue 5548 on 18 June 2009            
SELECT @ACTIVTY_ID=ISNULL(MAX(CR.ACTIVITY_ID),0) FROM CLM_ACTIVITY CA    
INNER JOIN CLM_ACTIVITY_RESERVE CR     
ON CA.ACTIVITY_ID = CASE CR.ACTIVITY_ID  WHEN 0 THEN 1 ELSE CR.ACTIVITY_ID  END 
AND CA.CLAIM_ID=CR.CLAIM_ID   
WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_STATUS =11801     
  
--This is done as in CLM_ACTIVITY table activity_id is set as 1 for a 'New Reserve' but in CLM_ACTIVITY_RESERVE table activity id is set to 0 for a new reserve. So we are not able to get correct transaction for a New Reserve   
IF(@ACTIVTY_ID = 1)  
  SET @ACTIVTY_ID = 0  
    
SELECT @TRANSACTION_ID = ISNULL(MAX(TRANSACTION_ID),0) FROM CLM_ACTIVITY_RESERVE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID = @ACTIVTY_ID AND IS_ACTIVE='Y';              
 END     
     
--Section I Coverages                      
 SELECT                                 
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                                  
  ISNULL(CAR.ACTION_ON_PAYMENT,'') AS CLM_RESERVE_ACTION_ON_PAYMENT,CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,                  
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                   
  CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,PVC.DWELLING_ID,MC.COV_ID,                                
  MC.COV_CODE, COV_DES AS COV_DESC,                    
 ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')                 
 + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,                                
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
  CASE MC.COV_ID WHEN 169 THEN ISNULL(PVC.DEDUCTIBLE_TEXT,'') ELSE ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(PVC.DEDUCTIBLE_TEXT,'') END AS DEDUCTIBLE2,
  MC.RANK,CAR.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,CAR.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE                   
 FROM                                 
  CLM_CLAIM_INFO CCI                                 
 JOIN                                 
  CLM_ACTIVITY_RESERVE CAR                      
 ON                                 
  CCI.CLAIM_ID = CAR.CLAIM_ID                                  
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
   --Done for Itrack Issue 6892 on 20 Jan 2010
	 AND CIV.IS_ACTIVE= 'Y'                               
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
  CCI.CLAIM_ID=@CLAIM_ID AND MC.COVERAGE_TYPE= CAST(@SECTION1_COVERAGES AS VARCHAR) AND CIV.CLAIM_ID=@CLAIM_ID                                 
-- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.                  
 AND CAR.TRANSACTION_ID= CAST(@TRANSACTION_ID AS VARCHAR(50))                    
 ORDER BY  CAR.VEHICLE_ID, CAR.ACTUAL_RISK_ID , CAR.ACTUAL_RISK_TYPE , MC.RANK,CAR.RESERVE_ID                                 
                        
                       
--Section II Coverages                                
 SELECT                                 
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                                  
  ISNULL(CAR.ACTION_ON_PAYMENT,'') AS CLM_RESERVE_ACTION_ON_PAYMENT,CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,                  
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                                
  CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,PVC.DWELLING_ID,MC.COV_ID,                                
  MC.COV_CODE, COV_DES AS COV_DESC,                    
 ISNULL(CAST(PDI.DWELLING_NUMBER AS VARCHAR),'') + '-' + ISNULL(PL.LOC_ADD1,'')  + '-' +  ISNULL(PL.LOC_ADD2,'')                      
 + '-' +  ISNULL(PL.LOC_CITY,'')  + '-' +  ISNULL(MCSL.STATE_NAME,'') + '-' + ISNULL(PL.LOC_ZIP,'') AS DWELLING,                             
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
  ISNULL(CAST(PVC.DEDUCTIBLE AS VARCHAR(10)),'') + ISNULL(PVC.DEDUCTIBLE_TEXT,'') AS DEDUCTIBLE2,
  MC.RANK,CAR.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,CAR.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE                                 
 FROM                                 
  CLM_CLAIM_INFO CCI                                 
 JOIN                                 
  CLM_ACTIVITY_RESERVE CAR                                  
 ON                                 
  CCI.CLAIM_ID = CAR.CLAIM_ID                                  
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
	--Done for Itrack Issue 6892 on 21 Jan 2010
	 AND CIV.IS_ACTIVE= 'Y'                                 
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
  CCI.CLAIM_ID=@CLAIM_ID AND MC.COVERAGE_TYPE= CAST(@SECTION2_COVERAGES AS VARCHAR) AND CIV.CLAIM_ID=@CLAIM_ID                   
-- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.                  
 AND CAR.TRANSACTION_ID= CAST(@TRANSACTION_ID AS VARCHAR(50))                    
ORDER BY CAR.VEHICLE_ID, CAR.ACTUAL_RISK_ID , CAR.ACTUAL_RISK_TYPE , MC.RANK,CAR.RESERVE_ID                           
                                
                                               
--Home Watercraft Coverages                                
 SELECT                             
  CAR.PRIMARY_EXCESS,CAR.ATTACHMENT_POINT,CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                              
  ISNULL(CAR.ACTION_ON_PAYMENT,'') AS CLM_RESERVE_ACTION_ON_PAYMENT,CAR.CRACCTS AS CLM_RESERVE_CRACCTS,CAR.DRACCTS AS CLM_RESERVE_DRACCTS,                  
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,CAR.MCCA_ATTACHMENT_POINT,CAR.MCCA_APPLIES,                          
  CASE ISNULL(PVC.BOAT_ID,'') WHEN '' THEN CAR.VEHICLE_ID ELSE PVC.BOAT_ID END AS DWELLING_ID,--MC.COV_ID AS COV_ID,
  CASE ISNULL(MC.COV_ID,'') WHEN '' THEN MCE.COV_ID ELSE MC.COV_ID END AS COV_ID,
  --COV_DES AS COV_DESC, 
  CASE WHEN CAR.COVERAGE_ID = 20001 THEN 'Section 1 - Covered Property Damage - Actual Cash Value' 
  WHEN CAR.COVERAGE_ID = 20002 THEN 'Section 1 - Covered Property Damage - Actual Cash Value'
  WHEN CAR.COVERAGE_ID = 20003 THEN 'Section 1 - Covered Property Damage - Actual Cash Value'
  ELSE MC.COV_DES END AS COV_DESC,                         
  (CAST(CIV.YEAR AS VARCHAR)+'-'+CAST(CIV.MAKE AS VARCHAR)+'-'+CAST(CIV.MODEL AS VARCHAR)+'-'+CAST(CIV.SERIAL_NUMBER AS VARCHAR)) AS DWELLING,                            
  CASE MC.LIMIT_TYPE  WHEN 2 THEN                              
   SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0, + CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,PVC.LIMIT_1),1),0)) +                              
   CONVERT(VARCHAR(20),ISNULL(PVC.LIMIT2_AMOUNT_TEXT,''))        + '/' +                              
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
 CASE ISNULL(MC.COVERAGE_TYPE,'') WHEN '' THEN MCE.COVERAGE_TYPE ELSE MC.COVERAGE_TYPE END AS COVERAGE_TYPE,
 CASE ISNULL(MC.RANK,'0.0') WHEN '0.0' THEN MCE.RANK ELSE MC.RANK END AS RANK,
 CAR.ACTUAL_RISK_ID AS ACTUAL_RISK_ID,
 CAR.ACTUAL_RISK_TYPE AS ACTUAL_RISK_TYPE,CIV.BOAT_ID AS CLAIM_BOAT_ID                       
 FROM                             
  CLM_CLAIM_INFO CCI                             
 LEFT OUTER JOIN                             
  CLM_ACTIVITY_RESERVE CAR                              
 ON                             
  CCI.CLAIM_ID = CAR.CLAIM_ID                              
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
  OUTER JOIN MNT_LOOKUP_VALUES MLV                               ON                             
  MLV.LOOKUP_UNIQUE_ID = CAR.REINSURANCE_CARRIER  
 LEFT OUTER JOIN  MNT_COVERAGE_EXTRA MCE ON           
 MCE.COV_ID = CAR.COVERAGE_ID
 AND MCE.COV_ID IN (20001,20002,20003)              
 WHERE                              
  CCI.CLAIM_ID=@CLAIM_ID AND  CIV.CLAIM_ID=@CLAIM_ID                            
-- Added by Asfa (12-Oct-2007) - To fetch the last reserve info.                  
 AND CAR.TRANSACTION_ID= CAST(@TRANSACTION_ID AS VARCHAR(50))       
 AND (ISNULL(MC.COVERAGE_TYPE,'') = 'RL'  OR ISNULL(MCE.COVERAGE_TYPE,'') = 'RL')          
 ORDER BY CAR.RESERVE_ID,CAR.VEHICLE_ID ,CAR.ACTUAL_RISK_ID ,  MCE.COV_ID,CAR.ACTUAL_RISK_TYPE                                          
   
                 
--Scheduled Item Coverages                          
exec Proc_GetOldSchItemCovgForClaims @CLAIM_ID,@TRANSACTION_ID              

exec Proc_GetOldWaterEquipCovgForClaims @CLAIM_ID,@TRANSACTION_ID

EXEC Proc_GetOldRecVehCovgForClaims @CLAIM_ID,@TRANSACTION_ID
/*                  
 SELECT                              
  P.ITEM_ID AS COV_ID,MC.COV_DES AS COV_DESC,MR.LIMIT_DEDUC_AMOUNT AS DEDUCTIBLE,         
  CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                              
   CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC AS REINSURANCECARRIER,                              
  ISNULL(SUM(D.ITEM_INSURING_VALUE),0) AS SCHEDULED_ITEM_COVERAGE_AMOUNT                               
 FROM                               
  CLM_ACTIVITY_RESERVE CAR                               
 LEFT JOIN                               
  CLM_CLAIM_INFO CCI                               
 ON                              
  CAR.CLAIM_ID = CCI.CLAIM_ID                            LEFT JOIN                               
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
  CAR.CLAIM_ID=@CLAIM_ID AND ISNULL(D.IS_ACTIVE,'N')='Y' AND CAR.VEHICLE_ID=0--vehicle_id will be 0 in case of scheduled item                              
 GROUP BY                                     
  P.ITEM_ID,MC.COV_DES,MR.LIMIT_DEDUC_AMOUNT,                              
  CAR.OUTSTANDING,CAR.RI_RESERVE,CAR.CLAIM_ID,CAR.RESERVE_ID,                              
  CAR.REINSURANCE_CARRIER,MLV.LOOKUP_VALUE_DESC                              
 ORDER BY MC.COV_DES                           
*/                  
 
                          
END

--go
--exec Proc_GetCLM_ACTIVITY_RESERVE_HOME  3506

--rollback tran

GO

