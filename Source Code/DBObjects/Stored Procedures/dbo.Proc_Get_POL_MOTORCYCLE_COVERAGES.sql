IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_MOTORCYCLE_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_MOTORCYCLE_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc Proc_Get_POL_MOTORCYCLE_COVERAGES                  
/*                                          
----------------------------------------------------------                                              
Proc Name       : dbo.Proc_Get_POL_MOTORCYCLE_COVERAGES                                          
Created by      : Pradeep                                            
Date            : 26 May,2005                                              
Purpose         : Selects records from POL_VEHICLE_COVERAGES                                              
Revison History :                                              
Used In         : Wolverine                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------                                             
*/                                          
                                          
CREATE PROCEDURE Proc_Get_POL_MOTORCYCLE_COVERAGES    
(                                          
 @CUSTOMER_ID int,                                          
 @POL_ID int,                                          
 @POL_VERSION_ID smallint,                                          
 @VEHICLE_ID smallint,                                        
 @POL_TYPE Char(1)                          
                                    
)                                          
                                          
As                                          
                                          
BEGIN      
                                        
 DECLARE @STATEID SmallInt                                          
 DECLARE @LOBID NVarCHar(5)                                          
                                         
 --N for New Business, R for renewal                                        
 --DECLARE @POL_TYPE Char(1)                                        
                                         
 --SET @POL_TYPE = 'N'                                        
                                           
 SELECT @STATEID = STATE_ID,                                          
  @LOBID = POLICY_LOB                                          
 FROM POL_CUSTOMER_POLICY_LIST                                          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                          
  POLICY_ID = @POL_ID AND                                          
  POLICY_VERSION_ID = @POL_VERSION_ID                                          
                                         
 ---For renewal                                        
 DECLARE @POL_COVERAGE_COUNT int                                        
 DECLARE @PREV_POL_VERSION_ID smallint                                        
                                         
                                         
                                         
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
  [IS_DEDUCT_APPLICABLE] NChar(1) ,                      
  [SIGNATURE_OBTAINED]  NChar(1) ,                    
  [RANK] Decimal(7,2),                  
  [COVERAGE_TYPE] NChar(10)                
                             
 )                                          
                                         
                     
                                     
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
  IS_DEDUCT_APPLICABLE ,                             
  SIGNATURE_OBTAINED,                    
  RANK,                  
  COVERAGE_TYPE                                                        
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
   ISNULL(Convert(VarChar(20),AVC.LIMIT_1),'') +                        
  ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,''),                                  
  ISNULL(Convert(VarChar(20),AVC.LIMIT_2),'') +                                   
  ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,''),                                   
    ISNULL(Convert(VarChar(20),AVC.DEDUCTIBLE_1),'') +                                   
  ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,''),               
     ISNULL(Convert(VarChar(20),AVC.DEDUCTIBLE_2),'') +                                   
  ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'')  ,                             
 C.ISLIMITAPPLICABLE,                                
    C.ISDEDUCTAPPLICABLE  ,                        
  AVC.SIGNATURE_OBTAINED  ,                    
  C.RANK,                  
  C.COVERAGE_TYPE                                            
  FROM MNT_COVERAGE C                                        
  LEFT OUTER JOIN POL_VEHICLE_COVERAGES  AVC ON                                        
   C.COV_ID = AVC.COVERAGE_CODE_ID AND                                        
   CUSTOMER_ID = @CUSTOMER_ID AND                              
    POLICY_ID = @POL_ID AND                                          
    POLICY_VERSION_ID = @POL_VERSION_ID AND                                          
    VEHICLE_ID = @VEHICLE_ID                                        
  WHERE STATE_ID = @STATEID AND                                          
   LOB_ID = @LOBID AND                 
   IS_ACTIVE = 'Y' AND                                          
   PURPOSE IN (1 , 3) --Purpose should either new business or both                                             
                                           
 END                                        
                                         
 --Table 1                                        
 SELECT * FROM #COVERAGES                      
 ORDER BY RANK                           
                                         
 --Table 2                                        
 --Get Coverage ranges                                        
 SELECT  R.COV_ID,                                        
  R.LIMIT_DEDUC_ID,                                        
  R.LIMIT_DEDUC_TYPE,                                        
  R.LIMIT_DEDUC_AMOUNT,                                        
  R.LIMIT_DEDUC_AMOUNT1,                                
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display,                                    
   ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display,                                     
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT),1),'.00',''),'') +                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') + '/' +                                     
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,R.LIMIT_DEDUC_AMOUNT1),1),'.00',''),'') +                                     
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                    
  as SplitAmount,                                      
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'') +                                   
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') as Limit_1_Display_Orig,                                  
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +             
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'') as Limit_2_Display_Orig,                                   
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT),'')  +                                   
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT_TEXT,'') +  '/' +                                   
  ISNULL(Convert(VarChar(20),R.LIMIT_DEDUC_AMOUNT1),'') +                                   
  ' ' + ISNULL(R.LIMIT_DEDUC_AMOUNT1_TEXT,'')                                  
  as SplitAmount_Orig,                                   
  R.IS_DEFAULT                                        
 FROM MNT_COVERAGE_RANGES R                                         
 INNER JOIN #COVERAGES C ON                       
  C.COV_ID = R.COV_ID                      
 WHERE R.IS_ACTIVE = 1                                        
 ORDER BY R.RANK          
             
 DROP TABLE #COVERAGES                 
                                       
 --Table 3                                      
 --Get the State for the POLlication                                      
 SELECT CS.STATE_ID,      
       CS.STATE_NAME       
FROM POL_CUSTOMER_POLICY_LIST A      
INNER JOIN MNT_COUNTRY_STATE_LIST CS ON      
 A.STATE_ID = CS.STATE_ID      
WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND      
               A.POLICY_ID=@POL_ID     AND      
               A.POLICY_VERSION_ID=@POL_VERSION_ID AND      
  CS.COUNTRY_ID = 1      
             
 --Table 4        
 --Get row count of coverages        
 SELECT COUNT(*) as ROW_COUNT        
 FROM POL_VEHICLE_COVERAGES        
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                              
    POLICY_ID = @POL_ID AND                                          
    POLICY_VERSION_ID = @POL_VERSION_ID AND                                          
    VEHICLE_ID = @VEHICLE_ID         
                                          
                                          
END                                          
                                        
                                           
                
                                          
                                          
                                        
                                        
                                        
                                      
                                      
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
  



GO

