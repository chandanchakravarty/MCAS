IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_UMBRELLA_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_UMBRELLA_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*          
----------------------------------------------------------                                                                            
Proc Name       : dbo.Proc_Get_POL_UMBRELLA_COVERAGES                                                                        
Created by      : Pravesh                                                                          
Date            : 10 Oct,2006                                                                            
Purpose         : Selects record from UMBRELLA_Coverages                                                                            
Revison History :                                                                            
Used In         : Wolverine                           
              
Modified By  :               
Modified On :               
Purpose  :               
    
Reviewed By : Anurag Verma    
Reviewed On : 06-07-2007    
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------            
-- Drop proc dbo.Proc_Get_POL_UMBRELLA_COVERAGES                                                                       
*/                                                                        
                                                                        
CREATE PROCEDURE dbo.Proc_Get_POL_UMBRELLA_COVERAGES          
(                                                                        
 @CUSTOMER_ID int,                                                                        
 @POL_ID int,                                                                        
 @POL_VERSION_ID smallint,                                                                        
 @POL_TYPE Char(1)                                                        
)                                                                        
                                                                        
As                                                                        
                                                                        
BEGIN                                    
                                                                      
 DECLARE @STATEID SmallInt                                                                        
 DECLARE @LOBID NVarCHar(5)                                                                        
 DECLARE @APP_EFFECTIVE_DATE DateTime                        
 DECLARE @APP_INCEPTION_DATE DateTime                                                                     
 DECLARE @POLICY_EFFECTIVE_DATE DateTime                        
 DECLARE @POLICY_EXPIRATION_DATE DateTime                                                                                       
           
SELECT @STATEID = STATE_ID,                                                                        
 @LOBID = POLICY_LOB,                        
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,                        
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE,                                                                         
 @POLICY_EFFECTIVE_DATE = POLICY_EFFECTIVE_DATE,                        
 @POLICY_EXPIRATION_DATE = POLICY_EXPIRATION_DATE                                                                        
 FROM POL_CUSTOMER_POLICY_LIST                                                                        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                                        
 POLICY_ID = @POL_ID AND                                                                        
 POLICY_VERSION_ID = @POL_VERSION_ID             
      
      
      
      
                                                                      
CREATE TABLE #COVERAGES                                                              
(                                                                    
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                                 
 [COV_ID] [int] NOT NULL ,                                                
 [COV_CODE] VarChar(10) NOT NULL ,                                  
 [COV_DESC] VarChar(100),                              
 [COVERAGE_ID] int,                                                                      
 [LIMIT_TYPE] NChar(1),                                                         
 [DEDUCTIBLE_TYPE] NChar(1),                                                                         
 [IS_MANDATORY] Char(1),               
 [RANK] Decimal(7,2),                                                
 [COVERAGE_TYPE] NChar(10),                                  
 [VEHICLE_COVERAGE_CODE_ID] Int,                                  
 [IS_ACTIVE] NChar(1) ,                            
 [EFFECTIVE_FROM_DATE] datetime,              
 [EFFECTIVE_TO_DATE] datetime ,              
 [ISADDDEDUCTIBLE_APP]   NChar(1),         
[HAS_MOTORIST_PROTECTION] int,          
  [LOWER_LIMITS] int,        
 [IS_BOAT_EXCLUDED] int,        
 [LOC_EXCLUDED] int,      
 [IS_EXCLUDED] int,      
[IS_RV_EXCLUDED] int,  
[AUTO_LIABILITY] INT      
)                                                                        
  
DECLARE @HAVE_NON_OWNED_AUTO_POL INT,    
@POLICY_LOB INT,    
@AUTO_LIABILITY INT    
SET @HAVE_NON_OWNED_AUTO_POL = (SELECT COUNT(HAVE_NON_OWNED_AUTO_POL) FROM POL_UMBRELLA_GEN_INFO     
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID  
AND HAVE_NON_OWNED_AUTO_POL = 'N')    
    
SET @POLICY_LOB = (SELECT COUNT(POLICY_LOB) FROM POL_UMBRELLA_UNDERLYING_POLICIES    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND POLICY_LOB NOT IN (2,3))    
    
IF( @HAVE_NON_OWNED_AUTO_POL>0 AND  @POLICY_LOB >0)    
 SET @AUTO_LIABILITY = 0    
ELSE    
  SET @AUTO_LIABILITY =1                                                    
                                                                   
INSERT INTO #COVERAGES                                                                        
(                                                                        
 COV_ID,                                                                     
 COV_CODE,                                                                      
 COV_DESC,                                                                        
 COVERAGE_ID,                                                                      
 LIMIT_TYPE,                         
 DEDUCTIBLE_TYPE ,                           
 IS_MANDATORY ,              
 RANK,                                                
 COVERAGE_TYPE ,                                  
 VEHICLE_COVERAGE_CODE_ID,                                  
 IS_ACTIVE,                            
 EFFECTIVE_FROM_DATE,              
 EFFECTIVE_TO_DATE ,              
  ISADDDEDUCTIBLE_APP ,          
 HAS_MOTORIST_PROTECTION,          
 LOWER_LIMITS,        
 IS_BOAT_EXCLUDED,        
 LOC_EXCLUDED,      
IS_EXCLUDED,      
IS_RV_EXCLUDED,  
AUTO_LIABILITY                 
)                                                                        
SELECT                                                                       
 C.COV_ID,                                                                        
 C.COV_CODE,                                                                        
 C.COV_DES + ' (' + ISNULL(C.FORM_NUMBER,'')+' )' AS  COV_DES,                                                                      
 AUC.COVERAGE_ID,                                                                       
 C.LIMIT_TYPE,                                                                      
 C.DEDUCTIBLE_TYPE,                                                                    
 C.IS_MANDATORY ,                
 C.RANK,                 
 C.COVERAGE_TYPE,                                  
 AUC.COVERAGE_CODE_ID,      
 C.IS_ACTIVE,                            
 C.EFFECTIVE_FROM_DATE,              
 C.EFFECTIVE_TO_DATE ,              
  C.ISADDDEDUCTIBLE_APP,        
        
isnull((select count(HAS_MOTORIST_PROTECTION) from  POL_UMBRELLA_UNDERLYING_POLICIES           
 where customer_id=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and isnull(HAS_MOTORIST_PROTECTION,0)=1 ),0)          
HAS_MOTORIST_PROTECTION,          
isnull((select count(LOWER_LIMITS) from  POL_UMBRELLA_UNDERLYING_POLICIES           
 where customer_id=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and isnull(LOWER_LIMITS,0)=1 ),0)          
     LOWER_LIMITS,         
isnull((select count(IS_BOAT_EXCLUDED) from  POL_UMBRELLA_WATERCRAFT_INFO           
 where customer_id=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and isnull(IS_BOAT_EXCLUDED,0)='10963'),0)           
IS_BOAT_EXCLUDED,        
        
isnull((select count(LOC_EXCLUDED) from  POL_UMBRELLA_REAL_ESTATE_LOCATION           
 where customer_id=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and isnull(LOC_EXCLUDED,0)='10963'),0)           
LOC_EXCLUDED,      
      
isnull((select count(IS_EXCLUDED) from  POL_UMBRELLA_VEHICLE_INFO           
 where customer_id=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and isnull(IS_EXCLUDED,0)=1),0)           
IS_EXCLUDED,      
/*case ISNULL((SELECT TOP 1 IS_BOAT_EXCLUDED FROM POL_UMBRELLA_RECREATIONAL_VEHICLES         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID),-1)      
WHEN -1 THEN -1       
ELSE(*/
CASE(SELECT COUNT(CUSTOMER_ID) FROM POL_UMBRELLA_RECREATIONAL_VEHICLES         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND IS_BOAT_EXCLUDED=10963)      
  WHEN 0 THEN 0      
 ELSE 1 END      
-- )      
--END AS      
IS_RV_EXCLUDED,  
@AUTO_LIABILITY  
/*      
isnull((select count(IS_BOAT_EXCLUDED) from  POL_UMBRELLA_RECREATIONAL_VEHICLES           
 where customer_id=@CUSTOMER_ID and POL_id=@POL_ID and POL_version_id=@POL_VERSION_ID and isnull(IS_BOAT_EXCLUDED,0)=1),0)           
IS_BOAT_EXCLUDED*/              
                 
 FROM MNT_COVERAGE C                                
  LEFT OUTER JOIN POL_UMBRELLA_COVERAGES  AUC ON                                                             
  C.COV_ID = AUC.COVERAGE_CODE_ID               
  AND CUSTOMER_ID = @CUSTOMER_ID              
  AND POL_ID = @POL_ID               
  AND POL_VERSION_ID = @POL_VERSION_ID              
             
 WHERE               
  STATE_ID = @STATEID              
  AND LOB_ID = @LOBID               
  AND PURPOSE IN (1 , 3) --purpose should either new business or both                                          
  AND C.COV_ID IN                               
  (                               
  SELECT C.COV_ID                               
  FROM MNT_COVERAGE C                              
  WHERE @APP_EFFECTIVE_DATE BETWEEN  C.EFFECTIVE_FROM_DATE AND                    
   ISNULL(C.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')              
  AND @APP_EFFECTIVE_DATE <= ISNULL(C.DISABLED_DATE,'3000-03-16 16:59:06.630')              
  )              
  OR ( AUC.COVERAGE_CODE_ID IS NOT NULL )                               
              
 --Table 0                                                                      
 SELECT * FROM #COVERAGES                                                    
 ORDER BY RANK                                                         
 DROP TABLE #COVERAGES              
      
                                       
            
END        
      
      



GO

