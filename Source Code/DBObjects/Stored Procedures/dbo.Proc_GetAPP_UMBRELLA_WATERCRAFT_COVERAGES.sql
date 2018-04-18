IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES

/*                      
----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES                      
Created by      : Pradeep                        
Date            : 10/25/2005                          
Purpose         : Selects records from Umbrella Watercrafts                          
Revison History :                          
Used In         : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------                         
*/                      
                      
CREATE            PROCEDURE Proc_GetAPP_UMBRELLA_WATERCRAFT_COVERAGES            
(                      
 @CUSTOMER_ID int,                      
 @APP_ID int,                      
 @APP_VERSION_ID smallint,                      
 @VEHICLE_ID smallint,                    
 @APP_TYPE Char(1)                    
)                      
                      
As                      
                      
                    
DECLARE @STATEID SmallInt                      
DECLARE @LOBID NVarCHar(5)                      
                    
--N for New Business, R for renewal                    
--DECLARE @APP_TYPE Char(1)                    
                    
--SET @APP_TYPE = 'N'                    
                      
SELECT @STATEID = STATE_ID,                      
 @LOBID = APP_LOB                      
FROM APP_LIST                      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
 APP_ID = @APP_ID AND                      
 APP_VERSION_ID = @APP_VERSION_ID                      
            
SET @LOBID = 4            
                
---For renewal                    
DECLARE @APP_COVERAGE_COUNT int                    
DECLARE @PREV_APP_VERSION_ID smallint                    
                    
                    
                    
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
 [IS_MANDATORY] Char(1) ,      
[IS_LIMIT_APPLICABLE] NChar(1),      
 [IS_DEDUCT_APPLICABLE] NChar(1) ,  
 [SIGNATURE_OBTAINED] NChar(1)                           
)                      
                    
IF ( @APP_TYPE = 'R')                    
BEGIN                    
 SELECT @PREV_APP_VERSION_ID = MAX(APP_VERSION_ID)                    
 FROM APP_LIST                    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                      
   APP_ID = @APP_ID AND                      
   APP_VERSION_ID < @APP_VERSION_ID                    
                     
 /*    
 IF ( @PREV_APP_VERSION_ID IS NULL )                    
 BEGIN                    
  --RAISERROR('Previous application not found',16,1)                    
 END                    
 */                    
      
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
 SIGNATURE_OBTAINED                       
                       
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
   NULL ,      
  C.ISLIMITAPPLICABLE,      
   C.ISDEDUCTAPPLICABLE  ,  
 NULL                         
 FROM MNT_COVERAGE C                    
 WHERE STATE_ID = @STATEID AND                      
  LOB_ID = @LOBID AND                      
  C.IS_ACTIVE = 'Y' AND                      
 PURPOSE IN (2, 3) --Purpose should either new business or both                      
 AND C.COV_ID NOT  IN                    
 (                    
  SELECT C.COV_ID                      
  FROM MNT_COVERAGE C                    
  INNER JOIN APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  AVC ON                    
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                    
  CUSTOMER_ID = @CUSTOMER_ID AND                      
   APP_ID = @APP_ID AND                      
   APP_VERSION_ID = @PREV_APP_VERSION_ID AND                      
   BOAT_ID = @VEHICLE_ID                     
 )                    
 UNION                    
 SELECT                     
   C.COV_ID,                      
   C.COV_CODE,                      
   C.COV_DES,                    
   NULL,                      
   AVC.LIMIT_1,                
   AVC.LIMIT1_AMOUNT_TEXT,                      
   AVC.LIMIT_2,                 
    AVC.LIMIT2_AMOUNT_TEXT,                           
   NULL,                      
   AVC.DEDUCTIBLE_1,                  
   AVC.DEDUCTIBLE1_AMOUNT_TEXT,                    
   AVC.DEDUCTIBLE_2,                        
      AVC.DEDUCTIBLE2_AMOUNT_TEXT,                    
   NULL,                      
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
   C.ISDEDUCTAPPLICABLE ,  
 AVC.SIGNATURE_OBTAINED                 
 FROM MNT_COVERAGE C                    
 INNER JOIN APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  AVC ON                    
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                    
  CUSTOMER_ID = @CUSTOMER_ID AND                      
   APP_ID = @APP_ID AND       
   APP_VERSION_ID = @PREV_APP_VERSION_ID AND                      
   BOAT_ID = @VEHICLE_ID                    
 WHERE STATE_ID = @STATEID AND                      
  LOB_ID = @LOBID AND                      
  C.IS_ACTIVE = 'Y' AND                      
 PURPOSE IN (2 , 3) --Purpose should either new business or both                      
                     
                     
END                    
           
--For New Business                    
IF ( @APP_TYPE = 'N')                    
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
 SIGNATURE_OBTAINED                                 
 )                      
 SELECT                     
   C.COV_ID,                      
   C.COV_CODE,                      
   C.COV_DES,                    
   NULL,                      
   AVC.LIMIT_1,                 
   AVC.LIMIT1_AMOUNT_TEXT,                      
   AVC.LIMIT_2,                   
   AVC.LIMIT2_AMOUNT_TEXT,                         
   NULL,                      
   AVC.DEDUCTIBLE_1,                      
   AVC.DEDUCTIBLE1_AMOUNT_TEXT,                       
   AVC.DEDUCTIBLE_2,                   
   AVC.DEDUCTIBLE2_AMOUNT_TEXT,                      
   NULL,                      
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
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'')  ,      
C.ISLIMITAPPLICABLE,      
   C.ISDEDUCTAPPLICABLE ,  
NULL                           
 FROM MNT_COVERAGE C                    
 LEFT OUTER JOIN APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  AVC ON                    
  C.COV_ID = AVC.COVERAGE_CODE_ID AND                    
  CUSTOMER_ID = @CUSTOMER_ID AND                      
   APP_ID = @APP_ID AND                      
   APP_VERSION_ID = @APP_VERSION_ID AND                      
   BOAT_ID = @VEHICLE_ID                    
 WHERE STATE_ID = @STATEID AND                      
  LOB_ID = @LOBID AND                      
  C.IS_ACTIVE = 'Y' AND                      
  PURPOSE IN (1 , 3) --Purpose should either new business or both                         
                      
END                    
                    
--Table 1                    
SELECT * FROM #COVERAGES                    
                    
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
 WHERE R.IS_ACTIVE = 1  
                 
--Table 3                  
--Get the State for the application                  
EXEC Proc_GetApplicationState @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID                  

--Table 4  
--Get the Watercraft info from APP_WATERCRAFT_INFO  
SELECT INSURING_VALUE  
FROM APP_WATERCRAFT_INFO  
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                      
   APP_ID = @APP_ID AND                      
   APP_VERSION_ID = @APP_VERSION_ID  AND  
   BOAT_ID = @VEHICLE_ID   
                  
DROP TABLE #COVERAGES                      
                      
                      
                      
                    
                       
                      
                      
                      
                    
                    
                    
                  
                  
                
              
            
          
        
      
    
  



GO

