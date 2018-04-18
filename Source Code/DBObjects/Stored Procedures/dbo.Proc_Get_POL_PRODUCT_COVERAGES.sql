IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_PRODUCT_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_PRODUCT_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 --BEGIN TRAN        
 --DROP PROc dbo.Proc_Get_POL_PRODUCT_COVERAGES        
 --GO  
/*                                                                  
----------------------------------------------------------                                                                      
Proc Name       : dbo.Proc_Get_POL_PRODUCT_COVERAGES                                                                  
Created by      : Pravesh K Chandel                                                                  
Date            : March -29-2010    
Purpose         : Selects records from POL_PRODUCT_COVERAGES    
Revison History :                                                                      
Used In         : EbixAdvantage    
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                      
------   ------------       -------------------------                                                                     
*/                                                                  
-- drop proc dbo.Proc_Get_POL_PRODUCT_COVERAGES   2885,95,1,1,'',2                                                    
CREATE PROCEDURE [dbo].[Proc_Get_POL_PRODUCT_COVERAGES]  --2126,50,1,1,'N',1                             
(                                                                    
 @CUSTOMER_ID int,                                                                    
 @POL_ID int,                                                                    
 @POL_VERSION_ID smallint,                                                                    
 @RISK_ID smallint,                                                                  
 @POL_TYPE Char(1),        
 @LANG_ID SMALLINT       
)    
    
As                                                                    
BEGIN                                
 DECLARE @LOBID NVarCHar(5)                                                                    
 DECLARE @APP_EFFECTIVE_DATE DateTime                        
 DECLARE @APP_INCEPTION_DATE DateTime                                                                 
 DECLARE @POLICY_STATUS NVARCHAR(20)                                                     
  DECLARE @SUBLOBID NVarCHar(5)       
 SELECT @POLICY_STATUS = POLICY_STATUS,                                                                    
  @LOBID = POLICY_LOB,     
  @SUBLOBID= POLICY_SUBLOB,                   
  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  ,                    
  @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                                  
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                                                               
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                                    
  POLICY_ID = @POL_ID AND                                                                    
  POLICY_VERSION_ID = @POL_VERSION_ID                                                                    
     
 CREATE TABLE #COVERAGES         
 (          
 [IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                                                    
 [COV_ID] [int] NOT NULL ,                                
 [COV_CODE] VarChar(10) NOT NULL ,   
 [COV_REF_CODE] VarChar(10),                                      
 [COV_DESC] nVarChar(1000), 
 [INDEMNITY_PERIOD]  INT,    
 [LIMIT_OVERRIDE] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                                                    
 [LIMIT_1] [decimal](18,2) NULL ,                                                                    
 [LIMIT1_AMOUNT_TEXT] NVarChar(100),                                                                 
 [LIMIT_2] [decimal](18,2) NULL ,                                                                    
 [LIMIT2_AMOUNT_TEXT] NVarChar(100),           
 [DEDUCT_OVERRIDE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,                                               
 [DEDUCTIBLE_1] [decimal](18,2) NULL ,                                               
 [DEDUCTIBLE1_AMOUNT_TEXT] NVarChar(100),                                                                       
 [DEDUCTIBLE_2] [decimal](18,2) NULL ,                                                               
 [DEDUCTIBLE2_AMOUNT_TEXT] NVarChar(4000),      
 [IS_SYSTEM_COVERAGE] [nchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,                                                                    
 [COVERAGE_ID] int,                                                                  
 [LIMIT_TYPE] NChar(1),                                                                  
 [DEDUCTIBLE_TYPE] NChar(1),                                                                     
 [LIMIT_1_DISPLAY] NVarChar(100) ,                                                              
 [LIMIT_2_DISPLAY] NVarChar(100) ,                      
 [DEDUCTIBLE_1_DISPLAY] NVarChar(500) ,                                                              
 [DEDUCTIBLE_2_DISPLAY] NVarChar(1000),                                                       
 [IS_MANDATORY] Char(1),         
 [INCLUDED]     VARCHAR(20),                                                  
 [IS_LIMIT_APPLICABLE] NChar(1),                                                          
 [IS_DEDUCT_APPLICABLE] NChar(1) ,        
 [RANK] Decimal(7,2),                                            
 [COVERAGE_TYPE] NChar(10),                                          
 [VEHICLE_COVERAGE_CODE_ID] Int,                              
 [IS_ACTIVE] NChar(1),                        
 [LIMIT_ID] Int,                        
 [DEDUC_ID] Int,      
 [EFFECTIVE_FROM_DATE] datetime,        
 [EFFECTIVE_TO_DATE] datetime ,      
  [ISADDDEDUCTIBLE_APP]   NChar(1),      
  [ADD_INFORMATION]  varchar(50),      
  [DEDUCTIBLE_1_TYPE] int,        
 [RI_APPLIES] char(1),        
 [MINIMUM_DEDUCTIBLE] [decimal](18,2) NULL ,              
 [DEDUCTIBLE_REDUCES]char(1),        
 [INITIAL_RATE] [decimal](8,4) NULL ,         
 [FINAL_RATE]   [decimal](8,4) NULL ,         
 [AVERAGE_RATE]  char(1),        
 [WRITTEN_PREMIUM]   [decimal](25,4) NULL ,
 [IS_MAIN]	 nchar	(2) null                                                                         
)                                                                 
    
 DECLARE @IDENT_COL INT                    
 SET  @IDENT_COL = 1                    
 --Holds effective dates of all versions of current policy                    
 DECLARE @TEMP_APP_LIST TABLE                    
 (                    
 IDENT_COL INT IDENTITY (1,1),                    
 APP_EFFECTIVE_DATE DATETIME,                    
 POLICY_VERSION_ID INT                    
 )                    
                   
 --Holds coverages applicable to each version of this policy                    
 DECLARE @TEMP_COV TABLE                    
 (                    
 COV_ID INT                    
 )                    
                     
 DECLARE @TEMP_COV_RANGES TABLE                   
 (             
 LIMIT_DEDUC_ID INT                    
 )                    
                    
 -- Insert APP_EFFECTIVE_DATE of all versions of this policy in temporary table      
 INSERT INTO @TEMP_APP_LIST                    
 (                    
 APP_EFFECTIVE_DATE,                    
 POLICY_VERSION_ID                    
 )                  
 SELECT APP_EFFECTIVE_DATE,POLICY_VERSION_ID            
  FROM POL_CUSTOMER_POLICY_LIST with(nolock)                   
   WHERE CUSTOMER_ID = @CUSTOMER_ID       
  AND POLICY_ID = @POL_ID          
        
                    
 --Get all versions while renewing                    
 DECLARE @APP_EFF_DATE DateTime                    
 DECLARE @COV_ID Int                    
 DECLARE @CURRENT_VERSION_ID Int             
 DECLARE @END_EFFECTIVE_DATE DateTime          
                    
 WHILE 1 = 1                    
 BEGIN                    
 IF NOT EXISTS                    
   (                    
    SELECT IDENT_COL FROM @TEMP_APP_LIST                   
    WHERE IDENT_COL = @IDENT_COL        
        )                    
   BEGIN                    
     BREAK                    
   END                    
                      
  SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE,                
   @CURRENT_VERSION_ID = POLiCY_VERSION_ID            
   FROM @TEMP_APP_LIST                    
 WHERE IDENT_COL = @IDENT_COL                    
    
 /*                    
 Insert into temp table the list of all coverages which where available in all versions                    
 and which has date range between each of the effective date.                    
 Also get coverages which were applicable during any endorsement process                    
 */                    
 INSERT INTO @TEMP_COV                    
 SELECT COV_ID FROM MNT_COVERAGE                    
 WHERE @APP_EFF_DATE BETWEEN EFFECTIVE_FROM_DATE AND ISNULL(EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')       
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE                  
 AND LOB_ID = @LOBID       
 AND SUB_LOB_ID = CAST(@SUBLOBID   AS INT)    
 AND COV_ID NOT IN (SELECT COV_ID FROM @TEMP_COV)                    
       
 --Coverage ranges                    
 INSERT INTO @TEMP_COV_RANGES                      
 SELECT LIMIT_DEDUC_ID FROM MNT_COVERAGE_RANGES R                    
 INNER JOIN MNT_COVERAGE C ON R.COV_ID = C.COV_ID                    
 WHERE @APP_EFF_DATE BETWEEN R.EFFECTIVE_FROM_DATE AND ISNULL(R.EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')      
 AND ISNULL(R.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE                               
 AND C.LOB_ID = @LOBID       
 AND C.SUB_LOB_ID = CAST(@SUBLOBID   AS INT)    
 --AND C.STATE_ID = @STATEID       
 AND R.LIMIT_DEDUC_ID NOT IN (SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES)                    
                   
 SET @IDENT_COL = @IDENT_COL + 1                    
END  --- End While Loop                  
      
      
--Insert coverages which were opted in any previous version though not applicable to that version      
 INSERT INTO @TEMP_COV       
 (                      
      COV_ID      
 )         
 SELECT DISTINCT COVERAGE_CODE_ID  FROM POL_PRODUCT_COVERAGES PV      
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
 WHERE      
  CUSTOMER_ID = @CUSTOMER_ID         
  AND POLICY_ID = @POL_ID        
  AND  COVERAGE_CODE_ID  IS NOT NULL      
  --AND M.STATE_ID=@STATEID      
      
 INSERT INTO @TEMP_COV_RANGES                        
 (      
   LIMIT_DEDUC_ID      
 )      
 SELECT DISTINCT LIMIT_ID  FROM POL_PRODUCT_COVERAGES PV      
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
 WHERE      
  CUSTOMER_ID = @CUSTOMER_ID         
  AND POLICY_ID = @POL_ID        
  AND  LIMIT_ID  IS NOT NULL      
  --AND M.STATE_ID=@STATEID      
      
 INSERT INTO @TEMP_COV_RANGES                        
 (      
   LIMIT_DEDUC_ID      
 )      
 SELECT DISTINCT DEDUC_ID  FROM POL_PRODUCT_COVERAGES PV      
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
 WHERE      
  CUSTOMER_ID = @CUSTOMER_ID         
  AND POLICY_ID = @POL_ID        
  AND  DEDUC_ID  IS NOT NULL      
  --AND M.STATE_ID=@STATEID      
-------------------------------------------------------------------                  
--If new business select all coverages including Grandfathered      
IF 1=1 --@POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'      
 BEGIN      
 INSERT INTO #COVERAGES                                                                    
 (                                                                    
 COV_ID,     
 COV_CODE, 
 COV_REF_CODE,
 COV_DESC,    
 INDEMNITY_PERIOD,                                        
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
  INCLUDED,                                                            
 LIMIT_1_DISPLAY  ,                                                              
 LIMIT_2_DISPLAY ,                                                              
 DEDUCTIBLE_1_DISPLAY  ,                                                              
 DEDUCTIBLE_2_DISPLAY,                                                          
 IS_LIMIT_APPLICABLE,                                                          
 IS_DEDUCT_APPLICABLE ,                                                       
 --SIGNATURE_OBTAINED,                                              
 RANK,                                            
 COVERAGE_TYPE ,                          
 VEHICLE_COVERAGE_CODE_ID,                          
 IS_ACTIVE,                        
 LIMIT_ID,                        
 DEDUC_ID,      
 EFFECTIVE_FROM_DATE,        
 EFFECTIVE_TO_DATE ,      
  ISADDDEDUCTIBLE_APP,      
   AVC.ADD_INFORMATION    ,        
DEDUCTIBLE_1_TYPE,          
RI_APPLIES ,        
MINIMUM_DEDUCTIBLE,        
DEDUCTIBLE_REDUCES,        
INITIAL_RATE,        
FINAL_RATE,        
AVERAGE_RATE,        
WRITTEN_PREMIUM  ,
IS_MAIN                                                                                             
 )    
 SELECT                                                                   
 C.COV_ID,                                       
 C.COV_CODE, 
 C.COV_REF_CODE,                                                                   
 ISNULL(COV_MULTI.COV_DES,C.COV_DES) AS COV_DES,    
 AVC.INDEMNITY_PERIOD,                                                                  
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
  ISNULL(REPLACE(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,                          
 ISNULL(REPLACE(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +     
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                   
 ISNULL(REPLACE(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +     
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                              
 ISNULL(REPLACE(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +     
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                    
 ISNULL(REPLACE(CONVERT(VARCHAR(30),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +     
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,                                                        
 C.ISLIMITAPPLICABLE,                                                          
 C.ISDEDUCTAPPLICABLE  ,                                                  
-- AVC.SIGNATURE_OBTAINED  ,                  
 C.RANK,                    
 C.COVERAGE_TYPE,         
 AVC.COVERAGE_CODE_ID,                          
 AVC.IS_ACTIVE,                        
 AVC.LIMIT_ID,                        
 AVC.DEDUC_ID,      
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,        
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE,      
  C.ISADDDEDUCTIBLE_APP,      
  AVC.ADD_INFORMATION    ,       
AVC.DEDUCTIBLE_1_TYPE,          
AVC.RI_APPLIES ,        
AVC.MINIMUM_DEDUCTIBLE,        
AVC.DEDUCTIBLE_REDUCES,        
AVC.INITIAL_RATE,        
AVC.FINAL_RATE,        
AVC.AVERAGE_RATE,     
AVC.WRITTEN_PREMIUM,
C.IS_MAIN          
 FROM MNT_COVERAGE C                                                     
 LEFT OUTER JOIN POL_PRODUCT_COVERAGES  AVC ON                                                                  
 C.COV_ID = AVC.COVERAGE_CODE_ID       
    AND CUSTOMER_ID = @CUSTOMER_ID      
 AND POLICY_ID = @POL_ID       
 AND POLICY_VERSION_ID = @POL_VERSION_ID                                                                     
 AND RISK_ID = @RISK_ID              
 LEFT OUTER JOIN  MNT_COVERAGE_MULTILINGUAL COV_MULTI ON       
 COV_MULTI.COV_ID = c.COV_ID AND      
 COV_MULTI.LANG_ID = @LANG_ID      
 WHERE       
 --STATE_ID = @STATEID AND       
 LOB_ID = @LOBID     
 AND C.SUB_LOB_ID = cast(@SUBLOBID  as int)    
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both         
 AND C.COV_ID  IN                           
 (                     
   SELECT COV_ID                         
   FROM MNT_COVERAGE  C1                       
    WHERE   --ISNULL(C1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE  -- condition changed for Brazil implementation       
    ISNULL(C1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE        
  AND C1.IS_ACTIVE = 'Y'    
  AND C1.LOB_ID		= C.LOB_ID 
  AND C1.SUB_LOB_ID = C.SUB_LOB_ID         
  )       
 OR  AVC.COVERAGE_CODE_ID IS NOT NULL            
 END               
/*      
-- In case of renewal Select all active coverages and       
-- coverages which are visible in any previous version of policy and are not disabled      
ELSE IF @POLICY_STATUS = 'URENEW' OR @POLICY_STATUS = 'UCORUSER'   OR @POLICY_STATUS='RSUSPENSE'      
 BEGIN      
 --select * from #TEMP_COV                      
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
  INCLUDED,                 
 LIMIT_1_DISPLAY  ,                                                                
 LIMIT_2_DISPLAY ,                                                                
 DEDUCTIBLE_1_DISPLAY  ,                                                                
 DEDUCTIBLE_2_DISPLAY,                                              
 IS_LIMIT_APPLICABLE,           
 IS_DEDUCT_APPLICABLE ,                                                        
 SIGNATURE_OBTAINED,                                                
 RANK,            
 COVERAGE_TYPE ,        
 VEHICLE_COVERAGE_CODE_ID,                            
 IS_ACTIVE,                          
 LIMIT_ID,        
 DEDUC_ID,        
 EFFECTIVE_FROM_DATE,          
 EFFECTIVE_TO_DATE ,      
  ISADDDEDUCTIBLE_APP,      
  ADD_INFORMATION                                                                                                
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
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,                        
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +              
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,                                                          
 C.ISLIMITAPPLICABLE,                                                            
 C.ISDEDUCTAPPLICABLE  ,                                                    
 AVC.SIGNATURE_OBTAINED  ,                                                
 C.RANK,                                              
 C.COVERAGE_TYPE,                     
 AVC.COVERAGE_CODE_ID,                            
 C.IS_ACTIVE,                          
 AVC.LIMIT_ID,                          
 AVC.DEDUC_ID,        
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,          
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE ,      
  C.ISADDDEDUCTIBLE_APP,      
 AVC.ADD_INFORMATION           
 FROM MNT_COVERAGE C                                                                    
 LEFT OUTER JOIN POL_VEHICLE_COVERAGES  AVC ON                                                                    
 C.COV_ID = AVC.COVERAGE_CODE_ID AND       
 CUSTOMER_ID = @CUSTOMER_ID AND                                 
 POLICY_ID = @POL_ID AND                    
 POLICY_VERSION_ID = @POL_VERSION_ID             
 AND VEHICLE_ID = @VEHICLE_ID                
 WHERE STATE_ID = @STATEID       
 AND LOB_ID = @LOBID      
 AND PURPOSE IN (1 , 3)--Purpose should either new business or both                       
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE      
 AND C.COV_ID  IN                             
 (                       
 SELECT DISTINCT COV_ID FROM @TEMP_COV                      
 )       
 OR                      
 AVC.COVERAGE_CODE_ID IS NOT NULL             
 END      
-- Endorsement Case        
-- Fetch All Active Coverages + Those Grandfathered(not disabled) but available in any previous version      
--     + Those disabled but available in base version of this policy      
ELSE IF @POLICY_STATUS = 'UENDRS'      
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
  INCLUDED, 
 LIMIT_1_DISPLAY  ,                                     
 LIMIT_2_DISPLAY ,                                                                
 DEDUCTIBLE_1_DISPLAY  ,                                                                
 DEDUCTIBLE_2_DISPLAY,                                                            
 IS_LIMIT_APPLICABLE,                                                            
 IS_DEDUCT_APPLICABLE ,                                                         
 SIGNATURE_OBTAINED,                                                
 RANK,                                              
 COVERAGE_TYPE ,                            
 VEHICLE_COVERAGE_CODE_ID,                            
 IS_ACTIVE,                          
 LIMIT_ID,                          
 DEDUC_ID,        
 EFFECTIVE_FROM_DATE,    
 EFFECTIVE_TO_DATE ,      
  ISADDDEDUCTIBLE_APP ,      
  ADD_INFORMATION                                                                                                 
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
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,                          
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                                        
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                                                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                                                 
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,                                                          
 C.ISLIMITAPPLICABLE,                                                            
 C.ISDEDUCTAPPLICABLE  ,                                                    
 AVC.SIGNATURE_OBTAINED  ,                                                
 C.RANK,                                              
 C.COVERAGE_TYPE,                            
 AVC.COVERAGE_CODE_ID,                            
 C.IS_ACTIVE,                          
 AVC.LIMIT_ID,                          
 AVC.DEDUC_ID,        
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,          
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE,      
  C.ISADDDEDUCTIBLE_APP ,      
  AVC.ADD_INFORMATION       
 FROM MNT_COVERAGE C                                                                    
 LEFT OUTER JOIN POL_VEHICLE_COVERAGES  AVC ON                                                                    
 C.COV_ID = AVC.COVERAGE_CODE_ID AND                                                                    
 CUSTOMER_ID = @CUSTOMER_ID AND                                                          
 POLICY_ID = @POL_ID AND                                                                      
 POLICY_VERSION_ID = @POL_VERSION_ID                                                                       
 AND VEHICLE_ID = @VEHICLE_ID                                                                    
 WHERE STATE_ID = @STATEID       
 AND LOB_ID = @LOBID       
 AND C.IS_ACTIVE='Y'      
 AND       
 (      
  C.COV_ID  IN                             
  (                       
   SELECT DISTINCT COV_ID FROM @TEMP_COV                      
  )       
 )      
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE      
 OR                      
 AVC.COVERAGE_CODE_ID IS NOT NULL        
 OR      
 C.COV_ID IN      
 (      
  SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES PV      
  INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
  WHERE       
  CUSTOMER_ID= @CUSTOMER_ID       
  AND POLICY_ID=@POL_ID       
  AND M.STATE_ID=@STATEID      
  AND POLICY_VERSION_ID IN       
  (      
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS       
   WHERE       
   CUSTOMER_ID= @CUSTOMER_ID       
   AND POLICY_ID=@POL_ID       
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit      
   AND PROCESS_STATUS ='COMPLETE'      
   AND NEW_POLICY_VERSION_ID IN       
    (      
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS      
     WHERE      
     CUSTOMER_ID= @CUSTOMER_ID      
     AND POLICY_ID=@POL_ID      
 AND PROCESS_ID IN (25,18)      
     AND PROCESS_STATUS ='COMPLETE'      
     )      
      
      
  )      
 )            
      
      
 END      
--If Policy is Active display only coverages which are relevant      
ELSE      
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
  INCLUDED,                                                               
 LIMIT_1_DISPLAY  ,                                                                
 LIMIT_2_DISPLAY ,                                                                
 DEDUCTIBLE_1_DISPLAY  ,                                                                
 DEDUCTIBLE_2_DISPLAY,                                                      
 IS_LIMIT_APPLICABLE,                  
 IS_DEDUCT_APPLICABLE ,                                                         
 SIGNATURE_OBTAINED,                                                
 RANK,                                              
 COVERAGE_TYPE ,                            
 VEHICLE_COVERAGE_CODE_ID,                            
 IS_ACTIVE,                          
 LIMIT_ID,                          
 DEDUC_ID,        
 EFFECTIVE_FROM_DATE,          
 EFFECTIVE_TO_DATE,      
  ISADDDEDUCTIBLE_APP,      
  ADD_INFORMATION                                                                                                  
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
  ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'Included') as INCLUDED_TEXT,               
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_1),1),'.00',''),'') +                
 ' ' + ISNULL(AVC.LIMIT1_AMOUNT_TEXT,'') as Limit_1_Display,                 
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.LIMIT_2),1),'.00',''),'') +                                                           
 ' ' + ISNULL(AVC.LIMIT2_AMOUNT_TEXT,'') as Limit_2_Display,                      
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_1),1),'.00',''),'') +           
 ' ' + ISNULL(AVC.DEDUCTIBLE1_AMOUNT_TEXT,'') as Limit_1_Display,                                                                
 ISNULL(REPLACE(CONVERT(VARCHAR(20),CONVERT(MONEY,AVC.DEDUCTIBLE_2),1),'.00',''),'') +                                 
 ' ' + ISNULL(AVC.DEDUCTIBLE2_AMOUNT_TEXT,'') as Limit_2_Display,                                                          
 C.ISLIMITAPPLICABLE,                                                            
 C.ISDEDUCTAPPLICABLE  ,                                                    
 AVC.SIGNATURE_OBTAINED  ,                                                
 C.RANK,                                              
 C.COVERAGE_TYPE,                            
 AVC.COVERAGE_CODE_ID,                            
 C.IS_ACTIVE,                          
 AVC.LIMIT_ID,                          
 AVC.DEDUC_ID,        
 C.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,          
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE ,      
  C.ISADDDEDUCTIBLE_APP,      
  AVC.ADD_INFORMATION           
 FROM MNT_COVERAGE C       
 LEFT OUTER JOIN POL_VEHICLE_COVERAGES  AVC ON                                                                    
 C.COV_ID = AVC.COVERAGE_CODE_ID AND    
 CUSTOMER_ID = @CUSTOMER_ID AND                                                          
 POLICY_ID = @POL_ID AND                                                                      
 POLICY_VERSION_ID = @POL_VERSION_ID                                                                       
 AND VEHICLE_ID = @VEHICLE_ID                                                                    
 WHERE STATE_ID = @STATEID AND                                                                  
 LOB_ID = @LOBID AND                                             
 PURPOSE IN (1 , 3) AND--Purpose should either new business or both                                                                         
 C.COV_ID  IN                             
 (         
  SELECT C.COV_ID                         
   FROM MNT_COVERAGE C                        
   WHERE @APP_EFFECTIVE_DATE BETWEEN C.EFFECTIVE_FROM_DATE AND ISNULL(C.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                       
   AND @APP_EFFECTIVE_DATE <= ISNULL(C.DISABLED_DATE,'3000-03-16 16:59:06.630')      
   AND C.STATE_ID=@STATEID      
 ) OR                      
 AVC.COVERAGE_CODE_ID IS NOT NULL              
 END      
  */                  
--Table 0                                                                  
 SELECT * FROM #COVERAGES                                                
 ORDER BY (CASE WHEN ISNULL(COVERAGE_ID,0)=0 THEN 0 ELSE 1 END) DESC,IS_MAIN DESC,COV_DESC asc--, --RANK                                                  
                                               
 --Table 1                                                                 
 --Get Coverage ranges        
--If new Business select all including  grandfathered but not disabled                                                                
IF 1=1 --@POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'      
 BEGIN      
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
 R.IS_DEFAULT,      
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,        
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                    
 FROM MNT_COVERAGE_RANGES R                                                                   
 INNER JOIN #COVERAGES C ON                                                 
 C.COV_ID = R.COV_ID                                                
 WHERE R.IS_ACTIVE = 1                          
 AND R.LIMIT_DEDUC_ID  IN                         
 (                    
  SELECT LIMIT_DEDUC_ID                      
  FROM MNT_COVERAGE_RANGES R1                      
  WHERE R1.LIMIT_DEDUC_TYPE = 'Limit' AND                      
  R1.COV_ID =  C.COV_ID  AND                
  ( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )                
  OR       
  ( C.LIMIT_ID = LIMIT_DEDUC_ID )                
        
  UNION                      
        
  SELECT LIMIT_DEDUC_ID                      
  FROM MNT_COVERAGE_RANGES R1                      
  WHERE R1.LIMIT_DEDUC_TYPE = 'Deduct' AND                      
  R1.COV_ID =  C.COV_ID     AND                
  ( ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE )                
  OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )                     
       
 )                    
 --ORDER BY R.RANK                                    
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                         
  R.LIMIT_DEDUC_ID       
 END      
 /*      
--For renewal select all effective ranges & grandfathered ragnes which are available in previous      
--version and which are not disabled      
ELSE IF @POLICY_STATUS = 'URENEW'  OR @POLICY_STATUS = 'UCORUSER'   OR @POLICY_STATUS='RSUSPENSE'      
 BEGIN      
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
 R.IS_DEFAULT,      
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,        
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                    
 FROM MNT_COVERAGE_RANGES R                                                                   
 INNER JOIN #COVERAGES C ON                                                 
 C.COV_ID = R.COV_ID                                                
 WHERE R.IS_ACTIVE = 1                          
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE      
 AND R.LIMIT_DEDUC_ID  IN                         
 (                    
 SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES                  
 UNION                  
 SELECT C.LIMIT_ID                   
 WHERE C.LIMIT_ID IS NOT NULL                   
 UNION                  
 SELECT C.DEDUC_ID                  
 WHERE C.DEDUC_ID IS NOT NULL                   
       
 )                    
 --ORDER BY R.RANK                                    
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                         
  R.LIMIT_DEDUC_ID       
 END      
-- Endorsment Case      
ELSE IF @POLICY_STATUS = 'UENDRS'      
 BEGIN      
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
 R.IS_DEFAULT,      
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,        
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                           
 FROM MNT_COVERAGE_RANGES R           
 INNER JOIN #COVERAGES C ON                                                 
 C.COV_ID = R.COV_ID                                                
 WHERE R.IS_ACTIVE = 1       
 AND ISNULL(DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE      
 AND R.LIMIT_DEDUC_ID  IN                         
 (                    
  SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES                  
  UNION                  
  SELECT C.LIMIT_ID                   
  WHERE C.LIMIT_ID IS NOT NULL                   
  UNION                  
  SELECT C.DEDUC_ID                  
  WHERE C.DEDUC_ID IS NOT NULL                   
       
 )         
 OR  R.LIMIT_DEDUC_ID  IN        
 (      
  SELECT  LIMIT_ID FROM POL_VEHICLE_COVERAGES PV      
  INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
  WHERE       
  CUSTOMER_ID= @CUSTOMER_ID       
  AND POLICY_ID=@POL_ID       
  AND M.STATE_ID=@STATEID      
  AND POLICY_VERSION_ID IN       
  (      
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS       
   WHERE       
   CUSTOMER_ID= @CUSTOMER_ID       
   AND POLICY_ID=@POL_ID       
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit      
   AND PROCESS_STATUS ='COMPLETE'      
   AND NEW_POLICY_VERSION_ID IN       
    (      
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS      
     WHERE      
     CUSTOMER_ID= @CUSTOMER_ID      
     AND POLICY_ID=@POL_ID      
     AND PROCESS_ID IN (25,18)      
     AND PROCESS_STATUS ='COMPLETE'      
     )      
      
      
  )      
  UNION      
  SELECT  DEDUC_ID FROM POL_VEHICLE_COVERAGES PV      
  INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID       
  WHERE       
  CUSTOMER_ID= @CUSTOMER_ID       
  AND POLICY_ID=@POL_ID       
  AND M.STATE_ID=@STATEID      
  AND POLICY_VERSION_ID IN       
  (      
   SELECT NEW_POLICY_VERSION_ID FROM POL_POLICY_PROCESS       
   WHERE       
   CUSTOMER_ID= @CUSTOMER_ID       
   AND POLICY_ID=@POL_ID       
   AND PROCESS_ID IN (25,18)   -- New Business Commit & Renewal Commit      
   AND PROCESS_STATUS ='COMPLETE'      
   AND NEW_POLICY_VERSION_ID IN       
    (      
     SELECT MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS      
     WHERE      
     CUSTOMER_ID= @CUSTOMER_ID      
     AND POLICY_ID=@POL_ID      
     AND PROCESS_ID IN (25,18)      
     AND PROCESS_STATUS ='COMPLETE'      
     )      
      
      
  )      
      
        
      
 )      
                 
 --ORDER BY R.RANK                                    
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                         
  R.LIMIT_DEDUC_ID       
 END      
      
-- In case of Active or in active policy display only Effective coverages ranges      
ELSE       
 BEGIN      
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
 R.IS_DEFAULT,      
 R.EFFECTIVE_FROM_DATE as EFFECTIVE_FROM_DATE,        
 R.EFFECTIVE_TO_DATE   as EFFECTIVE_TO_DATE                                                                    
 FROM MNT_COVERAGE_RANGES R                                                                   
 INNER JOIN #COVERAGES C ON                                                 
 C.COV_ID = R.COV_ID                                                
 WHERE R.IS_ACTIVE = 1           
 AND R.LIMIT_DEDUC_ID  IN                         
 (                    
  SELECT LIMIT_DEDUC_ID                      
  FROM MNT_COVERAGE_RANGES R1                      
  WHERE  R1.LIMIT_DEDUC_TYPE = 'Limit'      
  AND   R1.COV_ID =  C.COV_ID           
  AND                
  (                 
   @APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND      
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')            
   AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')      
  )                
  OR ( C.LIMIT_ID = LIMIT_DEDUC_ID )                
       
  UNION                      
        
  SELECT LIMIT_DEDUC_ID                      
  FROM MNT_COVERAGE_RANGES R1                      
  WHERE                  
  R1.LIMIT_DEDUC_TYPE = 'Deduct'      
  AND R1.COV_ID =  C.COV_ID          
  AND                
  (                 
   @APP_EFFECTIVE_DATE BETWEEN R1.EFFECTIVE_FROM_DATE AND      
     ISNULL(R1.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                  
   AND @APP_EFFECTIVE_DATE <= ISNULL(R1.DISABLED_DATE,'3000-03-16 16:59:06.630')      
  )                
  OR ( C.DEDUC_ID = LIMIT_DEDUC_ID )          
 )                    
 --ORDER BY R.RANK                                    
 ORDER BY R.LIMIT_DEDUC_AMOUNT,R.COV_ID,                                                         
  R.LIMIT_DEDUC_ID       
 END      
                    
   */                         
                    
                    
 --DROP TABLE #TEMP_APP_LIST                      
 --DROP TABLE #TEMP_COV                    
                     
                                                            
DROP TABLE #COVERAGES                              
 --Table 2                                                                
 --Get the State for the Application                                          
 SELECT CS.STATE_ID,      
substring(upper(CS.STATE_NAME),1,1) + substring(lower(CS.STATE_NAME),2,len(CS.STATE_NAME)-1) as STATE_NAME,      
A.POLICY_LOB as LOB_ID,APP_EFFECTIVE_DATE,ALL_DATA_VALID, YEAR(A.APP_EFFECTIVE_DATE ) as APP_YEAR      
 FROM POL_CUSTOMER_POLICY_LIST A                     
 left outer JOIN MNT_COUNTRY_STATE_LIST CS ON             
  A.STATE_ID = CS.STATE_ID                     
 WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND       
        A.POLICY_ID=@POL_ID     AND                                
        A.POLICY_VERSION_ID=@POL_VERSION_ID      
        --AND CS.COUNTRY_ID = 1                      
 ----table 3   deductible type        
   --SELECT 1 as DEDUCT_TYPE_ID,'% of Loss' DEDUCT_TYPE_DESC ,null as EFFECTIVE_FROM_DATE, null EFFECTIVE_TO_DATE        
   --union        
   --SELECT 2 as DEDUCT_TYPE_ID,'% of SI' DEDUCT_TYPE_DESC ,null as EFFECTIVE_FROM_DATE, null EFFECTIVE_TO_DATE        
   --union        
   --SELECT 3 as DEDUCT_TYPE_ID,'Flat' DEDUCT_TYPE_DESC ,null as EFFECTIVE_FROM_DATE, null EFFECTIVE_TO_DATE        
   --union        
   --SELECT 4 as DEDUCT_TYPE_ID,'# of Hours' DEDUCT_TYPE_DESC ,null as EFFECTIVE_FROM_DATE, null EFFECTIVE_TO_DATE        
   SELECT MLV1.LOOKUP_UNIQUE_ID  as DEDUCT_TYPE_ID,isnull(MLV2.LOOKUP_VALUE_DESC, MLV1.LOOKUP_VALUE_DESC) as DEDUCT_TYPE_DESC, null as EFFECTIVE_FROM_DATE, null EFFECTIVE_TO_DATE         
   from MNT_LOOKUP_VALUES MLV1 with(nolock)
   left join MNT_LOOKUP_VALUES_MULTILINGUAL MLV2 with(nolock)
   on MLV1.LOOKUP_UNIQUE_ID=MLV2.LOOKUP_UNIQUE_ID and MLV2.LANG_ID=@LANG_ID
   where MLV1.LOOKUP_ID=1379      
                                                                         
END    

 --GO        
 --EXEC dbo.Proc_Get_POL_PRODUCT_COVERAGES   28241,143,4,1,'',2   
 --ROLLBACK TRAN        
       
GO

