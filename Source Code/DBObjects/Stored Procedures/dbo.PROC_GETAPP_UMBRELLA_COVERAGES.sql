IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETAPP_UMBRELLA_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETAPP_UMBRELLA_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*                                                                    
----------------------------------------------------------                                                                        
PROC NAME       : DBO.PROC_GETAPP_UMBRELLA_COVERAGES                                                                    
CREATED BY      : PRAVESH                                                                      
DATE            : 10 OCT,2006                                                                        
PURPOSE         : SELECTS RECORD FROM UMBRELLA_COVERAGES                                                                        
REVISON HISTORY :                                                                        
USED IN         : WOLVERINE                       
          
MODIFIED BY  :           
MODIFIED ON :           
PURPOSE  :           
  
REVIEWED BY : ANURAG VERMA  
REVIEWED ON : 06-07-2007  
------------------------------------------------------------                                                                        
DATE     REVIEW BY          COMMENTS                                                                        
------   ------------       -------------------------              
DROP PROC  DBO.PROC_GETAPP_UMBRELLA_COVERAGES 1082,11,1,'N'                                                             
*/                                                                    
                                                                    
CREATE PROCEDURE DBO.PROC_GETAPP_UMBRELLA_COVERAGES          
(                                                                    
 @CUSTOMER_ID INT,                                                                    
 @APP_ID INT,                                                                    
 @APP_VERSION_ID SMALLINT,                                                                    
 @APP_TYPE CHAR(1)                                                    
)                                                                    
                                                                    
AS                                                                    
                                                                    
BEGIN                                
                                                                  
 DECLARE @STATEID SMALLINT                                                                    
 DECLARE @LOBID NVARCHAR(5)                                                                    
 DECLARE @APP_EFFECTIVE_DATE DATETIME                    
 DECLARE @APP_INCEPTION_DATE DATETIME                                                                 
                                                                     
 SELECT @STATEID = STATE_ID,                                                                    
 @LOBID = APP_LOB,                    
 @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE,                    
 @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                                     
 FROM APP_LIST                                                                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                                    
 APP_ID = @APP_ID AND                                                                    
 APP_VERSION_ID = @APP_VERSION_ID                                                                    
                                                                  
CREATE TABLE #COVERAGES                                                          
(                                                                    
 [IDENT_COL] [INT] IDENTITY(1,1) NOT NULL ,                                                
 [COV_ID] [INT] NOT NULL ,                                            
 [COV_CODE] VARCHAR(10) NOT NULL ,                                                            
 [COV_DESC] VARCHAR(300),                          
 [COVERAGE_ID] INT,                              
 [LIMIT_TYPE] NCHAR(1),                                                                  
 [DEDUCTIBLE_TYPE] NCHAR(1),       
 [IS_MANDATORY] CHAR(1),           
 [RANK] DECIMAL(7,2),                                            
 [COVERAGE_TYPE] NCHAR(10),                              
 [VEHICLE_COVERAGE_CODE_ID] INT,                              
 [IS_ACTIVE] NCHAR(1) ,                        
 [EFFECTIVE_FROM_DATE] DATETIME,          
 [EFFECTIVE_TO_DATE] DATETIME ,          
  [ISADDDEDUCTIBLE_APP]   NCHAR(1),         
  [HAS_MOTORIST_PROTECTION] INT,        
  [LOWER_LIMITS] INT,      
 [IS_BOAT_EXCLUDED] INT,      
 [LOC_EXCLUDED] INT,    
 [IS_EXCLUDED] INT,    
 [IS_RV_EXCLUDED] INT,    
 [AUTO_LIABILITY] INT    
)                                                                    
                                               
DECLARE @HAVE_NON_OWNED_AUTO_POL INT,
@POLICY_LOB INT,
@AUTO_LIABILITY INT
SET @HAVE_NON_OWNED_AUTO_POL = (SELECT COUNT(HAVE_NON_OWNED_AUTO_POL) FROM APP_UMBRELLA_GEN_INFO 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 
AND HAVE_NON_OWNED_AUTO_POL = 'N')

SET @POLICY_LOB = (SELECT COUNT(POLICY_LOB) FROM APP_UMBRELLA_UNDERLYING_POLICIES
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID and POLICY_LOB not in (2,3))

IF( @HAVE_NON_OWNED_AUTO_POL>0 AND  @POLICY_LOB >0)
	SET @AUTO_LIABILITY = 1
ELSE
	 SET @AUTO_LIABILITY =0
                                                           
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
 ISADDDEDUCTIBLE_APP  ,        
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
  C.ISADDDEDUCTIBLE_APP  ,        
ISNULL((SELECT COUNT(HAS_MOTORIST_PROTECTION) FROM  APP_UMBRELLA_UNDERLYING_POLICIES         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(HAS_MOTORIST_PROTECTION,0)=1 ),0)        
HAS_MOTORIST_PROTECTION,        
ISNULL((SELECT COUNT(LOWER_LIMITS) FROM  APP_UMBRELLA_UNDERLYING_POLICIES         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(LOWER_LIMITS,0)=1 ),0)        
     LOWER_LIMITS,       
ISNULL((SELECT COUNT(IS_BOAT_EXCLUDED) FROM  APP_UMBRELLA_WATERCRAFT_INFO         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(IS_BOAT_EXCLUDED,0)='10963'),0)         
IS_BOAT_EXCLUDED,      
      
ISNULL((SELECT COUNT(LOC_EXCLUDED) FROM  APP_UMBRELLA_REAL_ESTATE_LOCATION         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(LOC_EXCLUDED,0)='10963'),0)         
LOC_EXCLUDED,    
    
ISNULL((SELECT COUNT(IS_EXCLUDED) FROM  APP_UMBRELLA_VEHICLE_INFO         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(IS_EXCLUDED,0)=1),0)         
IS_EXCLUDED, 


    
--CASE ISNULL((SELECT TOP 1 IS_BOAT_EXCLUDED FROM APP_UMBRELLA_RECREATIONAL_VEHICLES       
-- WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID),-1)    
--WHEN -1 THEN -1     
--ELSE(
CASE(SELECT COUNT(CUSTOMER_ID) FROM APP_UMBRELLA_RECREATIONAL_VEHICLES       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND IS_BOAT_EXCLUDED=10963)    
  WHEN 0 THEN 0     
 ELSE 1 END    
-- )    
--END    
IS_RV_EXCLUDED,

@AUTO_LIABILITY 
/*    
ISNULL((SELECT COUNT(IS_BOAT_EXCLUDED) FROM  APP_UMBRELLA_RECREATIONAL_VEHICLES         
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND ISNULL(IS_BOAT_EXCLUDED,0)=1),0)         
IS_BOAT_EXCLUDED*/    
     
 FROM MNT_COVERAGE C                            
  LEFT OUTER JOIN         
 APP_UMBRELLA_COVERAGES  AUC ON                                                         
  C.COV_ID = AUC.COVERAGE_CODE_ID           
  AND CUSTOMER_ID = @CUSTOMER_ID          
  AND APP_ID = @APP_ID       
  AND APP_VERSION_ID = @APP_VERSION_ID          
          
 WHERE           
  STATE_ID = @STATEID          
  AND LOB_ID = @LOBID           
  AND PURPOSE IN (1 , 3) --PURPOSE SHOULD EITHER NEW BUSINESS OR BOTH          
  AND C.COV_ID IN                           
  (                           
  SELECT C.COV_ID                           
  FROM MNT_COVERAGE C                          
  WHERE @APP_EFFECTIVE_DATE BETWEEN  C.EFFECTIVE_FROM_DATE AND                
   ISNULL(C.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')          
  AND @APP_EFFECTIVE_DATE <= ISNULL(C.DISABLED_DATE,'3000-03-16 16:59:06.630')          
  )          
  OR ( AUC.COVERAGE_CODE_ID IS NOT NULL )                           
          
 --TABLE 0                                                                  
 SELECT * FROM #COVERAGES                                                
 ORDER BY RANK                                                     
 DROP TABLE #COVERAGES                                           
        
END  



GO

