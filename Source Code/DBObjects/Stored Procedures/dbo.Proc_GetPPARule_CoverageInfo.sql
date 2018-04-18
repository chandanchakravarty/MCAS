IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_CoverageInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_CoverageInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /* ----------------------------------------------------------                                                                                                    
Proc Name                : Dbo.Proc_GetPPARule_CoverageInfo 1692,361,1,1,'','w001'        
Created by               : Ashwani                                                                                                    
Date                     : 24 Aug.  2006        
Purpose                  : To get the auto coverage detail for rules                                                    
Revison History          :                                                                                                    
Used In                  : Wolverine                                                                                                    
------------------------------------------------------------                                                                                                    
Date     Review By          Comments                                                                                                    
------   ------------       -------------------------*/                                                                                                    
--drop proc dbo.Proc_GetPPARule_CoverageInfo                                             
CREATE proc dbo.Proc_GetPPARule_CoverageInfo                                                    
(                                                                                                    
 @CUSTOMERID     INT,                                                                                                    
 @APPID      INT,                                                                                                    
 @APPVERSIONID    INT,                                                    
 @VEHICLEID   INT,                                                                
 @DESC   VARCHAR(10),            
 @USER    VARCHAR(50)   ---Added to Check the Wolv User                                                                                           
)                                                                                                    
AS                                                                                                        
BEGIN                                                       
                       
----======================START=========GRANDFTAHERED COVERGAES==========================            
 DECLARE @APP_EFF_DATE datetime   
 DECLARE @STATE_ID SMALLINT     --Added by Charles on 29-Dec-09 for Itrack 6830                                
         
 SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE ,  
 @STATE_ID = STATE_ID    --Added by Charles on 29-Dec-09 for Itrack 6830                                   
 FROM APP_LIST         
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=APP_VERSION_ID                                         
                                    
                                
 SELECT MNTC.COV_DES AS COVERAGE_DES              
 FROM APP_VEHICLE_COVERAGES AVC                                      
 INNER JOIN MNT_COVERAGE MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID                                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID =@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID                                                     
  AND NOT(@APP_EFF_DATE BETWEEN MNTC.EFFECTIVE_FROM_DATE AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))                  
            
--Check for WOL and AGENCY USER              
--W001 -CARRIERSYSTEMID --W001 TO BE CHECKED WITH XML(DYNAMICALLY)                
 DECLARE @WOLVERINE_USER VARCHAR(50)             
         
 IF(@USER='w001')               
 BEGIN              
 SET @WOLVERINE_USER='Y'  --WOL USER              
 END              
 ELSE              
 BEGIN              
 SET @WOLVERINE_USER='N'  --AGENCY USER              
 END         
----=======================END========GRANDFTAHERED COVERGAES==========================      
----======================= GRANDFATHER LIMIT ========================================      
SELECT MNT.COV_DES AS LIMIT_DESCRIPTION,        
CASE MNT.LIMIT_TYPE        
WHEN 2 THEN      ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')    + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')         
 + '/'  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')         
ELSE        
  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')         
 +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')         
END AS LIMIT, AVC.COVERAGE_CODE_ID       
FROM APP_VEHICLE_COVERAGES AVC                                        
INNER JOIN MNT_COVERAGE_RANGES MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID AND  AVC.LIMIT_ID= MNTC.LIMIT_DEDUC_ID                                    
INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID         
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID         
AND  VEHICLE_ID=@VEHICLEID AND LIMIT_DEDUC_TYPE='LIMIT'        
AND NOT( @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')     
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')        
)          
----============================ END LIMIT  ==========================================        
--========================================= GRANDFATHER DEDUCTIBLE ====================================================      
      
SELECT MNT.COV_DES AS DEDUCTIBLE_DESCRIPTION,      
 CASE MNT.DEDUCTIBLE_TYPE      
 WHEN 2 THEN       
    ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')    + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')  + '/'      
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
 ELSE      
    ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')    + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')       
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
 END AS DEDUCTIBLE, AVC.COVERAGE_CODE_ID        
 FROM APP_VEHICLE_COVERAGES AVC                                      
 INNER JOIN MNT_COVERAGE_RANGES MNTC  ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID  AND  AVC.DEDUC_ID= MNTC.LIMIT_DEDUC_ID                                  
 INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID       
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID         
 AND  VEHICLE_ID=@VEHICLEID  AND LIMIT_DEDUC_TYPE='DEDUCT'      
 AND NOT(@APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')      
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')      
 )      
--========================================= END GRANDFATHER DEDUCTIBLE ===============================================     
 --Added by Charles on 29-Dec-09 for Itrack 6830  
 /*DECLARE @UNDERWRITING_TIER CHAR  
 SET @UNDERWRITING_TIER ='N' */ 
 --Added till here  
 SELECT @WOLVERINE_USER  as WOLVERINE_USER  
 --@UNDERWRITING_TIER AS UNDERWRITING_TIER    --Added by Charles on 29-Dec-09 for Itrack 6830  
                                         
                                                    
END      
GO

