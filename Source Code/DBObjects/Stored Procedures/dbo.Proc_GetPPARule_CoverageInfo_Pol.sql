IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_CoverageInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_CoverageInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                              
Proc Name                : Dbo.Proc_GetPPARule_CoverageInfo_Pol 1954,77,2,1,'w001'                  
Created by               : Manoj Rathore                                                                                                               
Date                     : 10 Apr. 2007                  
Purpose                  : To get the auto coverage detail for rules                                                              
Revison History          :                                                                                                              
Used In                  : Wolverine                                                                                                              
------------------------------------------------------------                                                                                                              
Date     Review By          Comments                                                                                                              
------   ------------       -------------------------*/                                                                                                              
--DROP PROC PROC_GETPPARULE_COVERAGEINFO_POL                                                       
CREATE proc [dbo].[Proc_GetPPARule_CoverageInfo_Pol]                                                              
(                                                                                                              
 @CUSTOMER_ID     INT,                                                                                                              
 @POLICY_ID      INT,                                                                                                              
 @POLICY_VERSION_ID    INT,                                                              
 @VEHICLE_ID   INT ,            
 @USER    VARCHAR(20)                                                                                           
)                                                                                                              
AS                                                                                                                  
BEGIN                                                                 
                                 
----======================START=========GRANDFTAHERED COVERGAES==========================                      
 DECLARE @APP_EFF_DATE datetime            
 --declare @USER varchar(50)          
 DECLARE @STATE_ID SMALLINT     --Added by Charles on 29-Dec-09 for Itrack 6830                                                                            
                   
 SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE,        
 @STATE_ID = STATE_ID    --Added by Charles on 29-Dec-09 for Itrack 6830                                                                                  
 FROM POL_CUSTOMER_POLICY_LIST                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                   
                                              
                                          
 SELECT MNTC.COV_DES AS COVERAGE_DES                        
 FROM POL_VEHICLE_COVERAGES AVC                                                
 INNER JOIN MNT_COVERAGE MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID                                                
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID                                                               
  AND NOT(@APP_EFF_DATE BETWEEN MNTC.EFFECTIVE_FROM_DATE AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))                            
                      
--Check for WOL and AGENCY USER                        
--W001 -CARRIERSYSTEMID --W001 TO BE CHECKED WITH XML(DYNAMICALLY)                          
 DECLARE @WOLVERINE_USER VARCHAR(50)                       
                   
 IF(upper(@USER)='W001')                         
 BEGIN                        
  SET @WOLVERINE_USER='Y'  --WOL USER                        
 END                        
 ELSE                        
 BEGIN                        
  SET @WOLVERINE_USER='N'  --AGENCY USER                        
 END                   
----=======================END========GRANDFTAHERED COVERGAES==========================        ----======================= GRANDFATHER LIMIT ========================================                
 SELECT MNT.COV_DES AS LIMIT_DESCRIPTION,             
 CASE MNT.LIMIT_TYPE                  
 WHEN 2 THEN  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')                   
 + '/'  +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')                   
 ELSE                  
   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')                   
 +   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')                   
 END AS LIMIT, AVC.COVERAGE_CODE_ID                 
 FROM POL_VEHICLE_COVERAGES AVC                                                  
 INNER JOIN MNT_COVERAGE_RANGES MNTC  ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID AND  AVC.LIMIT_ID= MNTC.LIMIT_DEDUC_ID                                              
 INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID               
 AND  VEHICLE_ID=@VEHICLE_ID AND LIMIT_DEDUC_TYPE='LIMIT'                  
 AND NOT( @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')                  
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')                  
)                    
----============================ END LIMIT  ==========================================                  
--========================================= GRANDFATHER DEDUCTIBLE ====================================================                
                
 SELECT MNT.COV_DES AS DEDUCTIBLE_DESCRIPTION,                
 CASE MNT.DEDUCTIBLE_TYPE                
 WHEN 2 THEN                 
   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/'                
  +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')                 
 ELSE                
   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')                 
  +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')                 
 END AS DEDUCTIBLE,                
 AVC.COVERAGE_CODE_ID                  
 FROM POL_VEHICLE_COVERAGES AVC                                                
 INNER JOIN MNT_COVERAGE_RANGES MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID AND  AVC.DEDUC_ID= MNTC.LIMIT_DEDUC_ID                                            
 INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID                 
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                
 AND  VEHICLE_ID=@VEHICLE_ID  AND LIMIT_DEDUC_TYPE='DEDUCT'                
 AND NOT( @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')                
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')                
 )                
--========================================= END GRANDFATHER DEDUCTIBLE ===============================================                    
/*              
IF SOME COVERAGES/LIMITS/DEDUCTIBLES/ENDORSEMENTS ARE THERE WHICH THE RENEWED              
VERSION IS NOT ELIGIBLE TO OPT FOR,THESE COVERAGES/LIMITS/ENDORSEMENTS ARE NOT              
COPIED TO THIS RENEWED VERSION.IN THIS CASE IF USE/EOD PROCESS COMMITS(Refer) THE               
RENEWAL PROCESS NOT READJUSTING THE COVERAGES, PROCESS SHOULD NOT COMMIT ,SHOULD REFER              
*/               
 DECLARE @ALL_DATA_VALID INT              
 DECLARE @COPY_COVERAGE_AT_RENEWAL CHAR              
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST               
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID                
 AND   ALL_DATA_VALID=2 )              
 BEGIN               
 SET @COPY_COVERAGE_AT_RENEWAL ='Y'              
 END             
 else             
 BEGIN               
 SET @COPY_COVERAGE_AT_RENEWAL ='N'              
 END             
-----------------------------------------------------------------------------------------------------------------            
 --Added by Charles on 29-Dec-09 for Itrack 6830        
/*Look at Vehicle Coverages tab 1st vehicle                                           
  Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits) to determine the Prior BI/CSL Limit                                    
  If vehicle coverage is suspended then look to see if there are any other vehicles on the policy with Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits)                                     
  If one or all vehicles have suspended coverage then refer to underwriters - must have an underwriting tier in order to process the policy                                   
       
 DECLARE @UNDERWRITING_TIER CHAR        
 DECLARE @IS_RENEWAL CHAR        
 SET @UNDERWRITING_TIER ='N'        
 SET @IS_RENEWAL = 'N'        
         
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID           
           AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID  <= @POLICY_VERSION_ID AND PROCESS_ID = 5)        
 BEGIN        
 SET @IS_RENEWAL = 'Y'        
 END        
        
 IF @STATE_ID = '14' AND @IS_RENEWAL='Y' AND NOT EXISTS        
 (        
   SELECT TOP 1 VEH.CUSTOMER_ID         
      FROM POL_VEHICLES VEH        
      LEFT JOIN  POL_VEHICLE_COVERAGES COV        
      LEFT JOIN MNT_COVERAGE MNT_COV        
      ON MNT_COV.COV_ID = COV.COVERAGE_CODE_ID        
      ON VEH.CUSTOMER_ID = COV.CUSTOMER_ID        
      AND VEH.POLICY_ID  = COV.POLICY_ID        
      AND VEH.POLICY_VERSION_ID = COV.POLICY_VERSION_ID         
      WHERE COV.CUSTOMER_ID = @CUSTOMER_ID         
      AND COV.POLICY_ID = @POLICY_ID         
      AND COV.POLICY_VERSION_ID = @POLICY_VERSION_ID        
      AND MNT_COV.COV_CODE IN ('BISPL','SLL')        
      AND ISNULL(VEH.IS_SUSPENDED,'') != 10963 --SUSPENDED_VEHILCE     
      AND ISNULL(VEH.IS_ACTIVE,'')='Y'       
      ORDER BY VEH.VEHICLE_ID        
    )        
   BEGIN        
  SET @UNDERWRITING_TIER='Y'        
   END        
   */        
 --Added till here         
SELECT @WOLVERINE_USER  as WOLVERINE_USER ,            
       @COPY_COVERAGE_AT_RENEWAL AS COPY_COVERAGE_AT_RENEWAL         
       --@UNDERWRITING_TIER AS UNDERWRITING_TIER  --Added by Charles on 29-Dec-09 for Itrack 6830                                              
                     
END 
GO

