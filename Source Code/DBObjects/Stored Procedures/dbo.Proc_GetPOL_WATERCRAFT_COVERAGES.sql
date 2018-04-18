IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPOL_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPOL_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc  Proc_GetPOL_WATERCRAFT_COVERAGES                 
                  
/*                                
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetPOL_WATERCRAFT_COVERAGES                                                      
Created by      : shafi                                  
Date            : 14 Feb,2006            
Purpose         : selects record for watercraft coverage                              
Revison History :                                    
Used In         : Wolverine                                    
                            
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                                   
*/                                
                                
CREATE         PROCEDURE Proc_GetPOL_WATERCRAFT_COVERAGES                                  
(                                  
 @CUSTOMER_ID int,                                  
 @POL_ID int,                                  
 @POL_VERSION_ID smallint,                                  
 @WATERCRAFT_ID   smallint,                                
 @POL_TYPE Char(1)                                
)                                  
                                  
As                                  
                                  
                                
DECLARE @STATEID SmallInt                                    
DECLARE @LOBID NVarCHar(5)                                    
                                  
--N for New Business, R for renewal                                  
--DECLARE @APP_TYPE Char(1)                                  
                                  
--SET @APP_TYPE = 'N'                                  
                                    
/*------  Modified BY Anurag Verma on 14/10/2005 --- hardcoded LOBID for watercraft - 4                   */                            
SELECT @STATEID = STATE_ID,                                    
 @LOBID = 4                                    
FROM  POL_CUSTOMER_POLICY_LIST                 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                    
  POLICY_ID = @POL_ID AND                                    
  POLICY_VERSION_ID = @POL_VERSION_ID               
            
            
                                 
                                  
---For renewal                                  
DECLARE @POL_COVERAGE_COUNT int                                  
DECLARE @PREV_POL_VERSION_ID smallint                                  
                                  
print @STATEID                            
print @LOBID                            
                                  
                                  
CREATE TABLE #COVERAGES                                    
(                                    
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                    
 [COV_ID] [int] NOT NULL ,                                    
 [COV_CODE] VarChar(10) NOT NULL ,                                  
 [COV_DESC] VarChar(100),                                      
 [LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                    
 [LIMIT_1] [decimal](18) NULL ,                                    
 [LIMIT1_AMOUNT_TEXT] NVarChar(100),                                 
 [LIMIT_2] [decimal](18) NULL ,                                    
 [LIMIT2_AMOUNT_TEXT] NVarChar(100),                                    
 [DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                    
 [DEDUCTIBLE_1] [decimal](18) NULL ,                                    
 [DEDUCTIBLE1_AMOUNT_TEXT] NVarChar(100),                                       
 [DEDUCTIBLE_2] [decimal](18) NULL ,                               
 [DEDUCTIBLE2_AMOUNT_TEXT] NVarChar(100),                                            
                                 
 [IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,                                    
                                  
 [COVERAGE_ID] int,                                  
 [LIMIT_TYPE] NChar(1),                                  
 [DEDUCTIBLE_TYPE] NChar(1),                                     
 [LIMIT_1_DISPLAY] NVarChar(100) ,                              
[LIMIT_2_DISPLAY] NVarChar(100) ,                              
 [DEDUCTIBLE_1_DISPLAY] NVarChar(100) ,                              
 [DEDUCTIBLE_2_DISPLAY] NVarChar(100),                             
 [IS_MANDATORY] Char(1),                        
 [IS_LIMIT_APPLICABLE] NChar(1),                        
 [IS_DEDUCT_APPLICABLE] NChar(1),                    
  [COVERAGE_TYPE] NChar(10),         
 [SIGNATURE_OBTAINED] NChar(1),    
 [RANK]  INT                               
)                                    
                                  
IF ( @POL_TYPE = 'R')                                  
BEGIN                                  
 SELECT @PREV_POL_VERSION_ID = MAX(POLICY_VERSION_ID)                                  
FROM POL_CUSTOMER_POLICY_LIST                 
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                    
  POLICY_ID = @POL_ID AND                                    
  POLICY_VERSION_ID < @POL_VERSION_ID                                     
                                   
                                  
                                   
 INSERT INTO #COVERAGES                                    
 (                                    
   COV_ID,                                    
   COV_CODE,                                  
   COV_DESC,                                    
   LIMIT_OVERRIDE,                                    
   LIMIT_1,                              
LIMIT1_AMOUNT_TEXT,                                    
   LIMIT_2,                                    
      LIMIT2_AMOUNT_TEXT,                                    
   DEDUCT_OVERRIDE,                                    
   DEDUCTIBLE_1,                              
   DEDUCTIBLE1_AMOUNT_TEXT,                                    
   DEDUCTIBLE_2,                                
   DEDUCTIBLE2_AMOUNT_TEXT,                                    
   IS_SYSTEM_COVERAGE,                                     
   COVERAGE_ID,                                  
   LIMIT_TYPE,                                  
   DEDUCTIBLE_TYPE,                                
   IS_MANDATORY,                              
   LIMIT_1_DISPLAY  ,                              
   LIMIT_2_DISPLAY ,                              
 DEDUCTIBLE_1_DISPLAY  ,                              
 DEDUCTIBLE_2_DISPLAY ,                        
 IS_LIMIT_APPLICABLE,                        
 IS_DEDUCT_APPLICABLE,       
 COVERAGE_TYPE,                           
 SIGNATURE_OBTAINED,    
 RANK                           
                                     
 )                                    
 SELECT                                   
   C.COV_ID,                                    
   C.COV_CODE,                                    
   C.COV_DES,                                  
   NULL , --AVC.LIMIT_OVERRIDE,                                    
   NULL , --AVC.LIMIT_1,                               
   NULL,                                    
   NULL , --AVC.LIMIT_2,                                
   NULL,                              
   NULL, --AVC.DEDUCT_OVERRIDE,                                    
   NULL,--AVC.DEDUCTIBLE_1,                               
   NULL,                                   
   NULL,--AVC.DEDUCTIBLE_2,                                      
   NULL,                               
   NULL,--AVC.IS_SYSTEM_COVERAGE,                                    
   NULL,--AVC.COVERAGE_ID,                                   
   C.LIMIT_TYPE,                                  
   C.DEDUCTIBLE_TYPE,                                
   C.IS_MANDATORY,                    
   NULL,                              
   NULL,                              
   NULL,                              
   NULL  ,                        
   C.ISLIMITAPPLICABLE,                        
   C.ISDEDUCTAPPLICABLE ,        
   C.COVERAGE_TYPE,                    
   NULL,    
   C.RANK                             
 FROM MNT_COVERAGE C                                  
 WHERE STATE_ID = @STATEID AND                                    
  LOB_ID = @LOBID AND                                    
  IS_ACTIVE = 'Y' AND                                    
 PURPOSE IN (2, 3) --Purpose should either new business or both                                    
 AND C.COV_ID NOT  IN                                  
 (                                  
  SELECT C.COV_ID                                    
  FROM MNT_COVERAGE C                                  
  INNER JOIN POL_WATERCRAFT_COVERAGE_INFO  AVC ON                                  
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                                  
  CUSTOMER_ID = @CUSTOMER_ID AND                                    
   POLICY_ID = @POL_ID AND                                    
   POLICY_VERSION_ID = @PREV_POL_VERSION_ID AND                                    
   BOAT_ID = @WATERCRAFT_ID                                   
 )                                  
 UNION                                  
 SELECT                                   
   C.COV_ID,                                    
   C.COV_CODE,                            
   C.COV_DES,                                  
   AVC.LIMIT_OVERRIDE,                                    
   AVC.LIMIT_1,                              
   AVC.LIMIT1_AMOUNT_TEXT,                                    
   AVC.LIMIT_2,                               
    AVC.LIMIT2_AMOUNT_TEXT,                                         
   AVC.DEDUCT_OVERRIDE,                                    
   AVC.DEDUCTIBLE_1,                                
   AVC.DEDUCTIBLE1_AMOUNT_TEXT,                                  
   AVC.DEDUCTIBLE_2,                                      
      AVC.DEDUCTIBLE2_AMOUNT_TEXT,                                  
   AVC.IS_SYSTEM_COVERAGE,                                    
   NULL,                                  
   C.LIMIT_TYPE,                                  
   C.DEDUCTIBLE_TYPE,                                
   C.IS_MANDATORY,                              
   Convert(VarChar(20),AVC.LIMIT_1) +                               
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''),                              
 Convert(VarChar(20),AVC.LIMIT_2) +                               
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''),                               
   Convert(VarChar(20),AVC.DEDUCTIBLE_1) +                               
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''),                               
    Convert(VarChar(20),AVC.DEDUCTIBLE_2) +                               
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') ,                        
  C.ISLIMITAPPLICABLE,                        
   C.ISDEDUCTAPPLICABLE,        
   C.COVERAGE_TYPE,                    
 AVC.SIGNATURE_OBTAINED,    
 C.RANK                               
 FROM MNT_COVERAGE C                                  
 INNER JOIN POL_WATERCRAFT_COVERAGE_INFO  AVC ON                                  
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                                  
  CUSTOMER_ID = @CUSTOMER_ID AND                                    
   POLICY_ID = @POL_ID AND                                    
   POLICY_VERSION_ID = @PREV_POL_VERSION_ID AND                                    
   BOAT_ID = @WATERCRAFT_ID                                  
 WHERE C.STATE_ID = @STATEID AND                                    
  C.LOB_ID = @LOBID AND                                    
  C.IS_ACTIVE = 'Y' AND                                    
 PURPOSE IN (2 , 3) --Purpose should either new business or both                                    
                                   
                                   
END                  
                                  
--For New Business                                  
IF ( @POL_TYPE = 'N')                                  
BEGIN                                  
                                   
 INSERT INTO #COVERAGES                                    
 (                                    
   COV_ID,                                    
   COV_CODE,                                  
   COV_DESC,                
   LIMIT_OVERRIDE,                         
   LIMIT_1,                              
   LIMIT1_AMOUNT_TEXT,                                    
   LIMIT_2,                                 
   LIMIT2_AMOUNT_TEXT,                                       
   DEDUCT_OVERRIDE,                                    
   DEDUCTIBLE_1,                               
   DEDUCTIBLE1_AMOUNT_TEXT,                                     
   DEDUCTIBLE_2,                              
   DEDUCTIBLE2_AMOUNT_TEXT,                
   IS_SYSTEM_COVERAGE,                                     
   COVERAGE_ID,                                  
LIMIT_TYPE,                                  
   DEDUCTIBLE_TYPE ,                                   
   IS_MANDATORY ,                              
   LIMIT_1_DISPLAY  ,                              
   LIMIT_2_DISPLAY ,                              
 DEDUCTIBLE_1_DISPLAY  ,                              
 DEDUCTIBLE_2_DISPLAY,                        
 IS_LIMIT_APPLICABLE,                        
 IS_DEDUCT_APPLICABLE,       
 COVERAGE_TYPE,                   
 SIGNATURE_OBTAINED,    
 RANK                                              
 )                         
 SELECT                                   
   C.COV_ID,                                    
   C.COV_CODE,                                    
   C.COV_DES,                                  
   AVC.LIMIT_OVERRIDE,                                    
   AVC.LIMIT_1,                               
   AVC.LIMIT1_AMOUNT_TEXT,                                    
   AVC.LIMIT_2,                                 
   AVC.LIMIT2_AMOUNT_TEXT,                                       
   AVC.DEDUCT_OVERRIDE,                                    
   AVC.DEDUCTIBLE_1,                                   
   AVC.DEDUCTIBLE1_AMOUNT_TEXT,                                     
   AVC.DEDUCTIBLE_2,                                 
   AVC.DEDUCTIBLE2_AMOUNT_TEXT,                                    
   AVC.IS_SYSTEM_COVERAGE,                                    
   AVC.COVERAGE_ID,                                  
   C.LIMIT_TYPE,                                  
   C.DEDUCTIBLE_TYPE,                                
   C.IS_MANDATORY ,                              
   Convert(VarChar(20),AVC.LIMIT_1) +                               
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''),                 
 Convert(VarChar(20),AVC.LIMIT_2) +                               
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''),                               
   Convert(VarChar(20),AVC.DEDUCTIBLE_1) +                               
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''),                               
    Convert(VarChar(20),AVC.DEDUCTIBLE_2) +                               
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') ,                        
C.ISLIMITAPPLICABLE,                        
   C.ISDEDUCTAPPLICABLE  ,       
   C.COVERAGE_TYPE,                   
 AVC.SIGNATURE_OBTAINED,    
C.RANK                                        
 FROM MNT_COVERAGE C                                  
 LEFT OUTER JOIN POL_WATERCRAFT_COVERAGE_INFO  AVC ON                                  
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                                  
  CUSTOMER_ID = @CUSTOMER_ID AND                                    
   POLICY_ID = @POL_ID AND                   
   POLICY_VERSION_ID = @POL_VERSION_ID AND                                    
   BOAT_ID = @WATERCRAFT_ID                                  
 WHERE C.STATE_ID = @STATEID AND                                    
  C.LOB_ID = @LOBID AND                                    
  --C.IS_ACTIVE = 'Y' AND                                    
  C.PURPOSE IN (1 , 3) AND--Purpose should either new business or both                                                       
   C.COV_ID NOT IN           
   (          
     SELECT C.COV_ID           
     FROM MNT_COVERAGE C          
     WHERE C.IS_ACTIVE = 'N' AND          
     AVC.COVERAGE_CODE_ID IS NULL          
   )               
END                                  
                                  
--Table 1                                  
SELECT * FROM #COVERAGES ORDER BY RANK                    
                                  
--Table 2                                  
--Get Coverage ranges                                  
SELECT  R.COV_ID,                                  
R.LIMIT_DEDUC_ID,                                  
 R.LIMIT_DEDUC_TYPE,                                  
 R.LIMIT_DEDUC_AMOUNT,                                  
 R.LIMIT_DEDUC_AMOUNT1,                               
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT) +                               
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                              
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1) +                               
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,             
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT)  +                               
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') +   '/' +                               
 Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1) +                               
 ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                              
 as SplitAmount,                                
                              
 R.IS_DEFAULT                                  
FROM MNT_COVERAGE_RANGES R                                   
INNER JOIN #COVERAGES C ON                                  
 C.COV_ID = R.COV_ID                     
order by R.Limit_DEDUC_AMOUNT                  
                               
                             
--Table 3                                
--Get the State for the Policy                               
EXEC Proc_GetPolicyState @CUSTOMER_ID,@POL_ID,@POL_VERSION_ID            
                
--Table 4                
--Get the Watercraft info from POL_WATERCRAFT_INFO                
SELECT INSURING_VALUE,              
 TYPE_OF_WATERCRAFT,  
 TYPE,    
YEAR,    
LENGTH ,
BOAT_ID,
MAKE,
MODEL
FROM POL_WATERCRAFT_INFO INNER JOIN MNT_LOOKUP_VALUES   
ON POL_WATERCRAFT_INFO.TYPE_OF_WATERCRAFT=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID               
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                    
   POLICY_ID = @POL_ID AND                                    
   POLICY_VERSION_ID = @POL_VERSION_ID  AND                
   BOAT_ID = @WATERCRAFT_ID                 
                 
                                
DROP TABLE #COVERAGES                          
                          
                        
                      
                
                  
                         
                
              
            
            
          
        
        
      
    
    
    
  



GO

