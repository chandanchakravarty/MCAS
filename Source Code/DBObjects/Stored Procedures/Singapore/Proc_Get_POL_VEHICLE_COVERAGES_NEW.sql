                                          
CREATE PROCEDURE [dbo].[Proc_Get_POL_VEHICLE_COVERAGES_NEW]                                
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
 DECLARE @APP_EFFECTIVE_DATE DateTime                          
 DECLARE @APP_INCEPTION_DATE DateTime                                                                   
 DECLARE @POLICY_STATUS NVARCHAR(20)                                                       
                                                                       
 SELECT @STATEID = STATE_ID,        
  @POLICY_STATUS = POLICY_STATUS,                                                                      
  @LOBID = POLICY_LOB,                      
  @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  ,                      
  @APP_INCEPTION_DATE = APP_INCEPTION_DATE                                                                    
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
  [INCLUDED]     VARCHAR(20),                                                    
 [IS_LIMIT_APPLICABLE] NChar(1),                                                            
 [IS_DEDUCT_APPLICABLE] NChar(1) ,                                                  
 [SIGNATURE_OBTAINED]  NChar(1) ,                                                
 [RANK] Decimal(7,2),                                              
 [COVERAGE_TYPE] NChar(10),                                            
 [VEHICLE_COVERAGE_CODE_ID] Int,                                
 [IS_ACTIVE] NChar(1),                          
 [LIMIT_ID] Int,                          
 [DEDUC_ID] Int,        
 [EFFECTIVE_FROM_DATE] datetime,          
 [EFFECTIVE_TO_DATE] datetime ,        
  [ISADDDEDUCTIBLE_APP]   NChar(1),        
  [ADD_INFORMATION]  varchar(50)                                                                             
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
  FROM POL_CUSTOMER_POLICY_LIST                      
   WHERE CUSTOMER_ID = @CUSTOMER_ID         
  AND POLICY_ID = @POL_ID    AND STATE_ID=@STATEID                     
                      
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
 AND STATE_ID = @STATEID         
 AND COV_ID NOT IN (SELECT COV_ID FROM @TEMP_COV)                      
         
 --Coverage ranges                      
 INSERT INTO @TEMP_COV_RANGES                        
 SELECT LIMIT_DEDUC_ID FROM MNT_COVERAGE_RANGES R                      
 INNER JOIN MNT_COVERAGE C ON R.COV_ID = C.COV_ID                      
 WHERE @APP_EFF_DATE BETWEEN R.EFFECTIVE_FROM_DATE AND ISNULL(R.EFFECTIVE_TO_DATE,'3000-01-01 16:50:49.333')        
 AND ISNULL(R.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFF_DATE                                 
 AND C.LOB_ID = @LOBID         
 AND C.STATE_ID = @STATEID         
 AND R.LIMIT_DEDUC_ID NOT IN (SELECT LIMIT_DEDUC_ID FROM @TEMP_COV_RANGES)                      
                     
 SET @IDENT_COL = @IDENT_COL + 1                      
END  --- End While Loop                    
        
        
--Insert coverages which were opted in any previous version though not applicable to that version        
 INSERT INTO @TEMP_COV         
 (                        
      COV_ID        
 )          
 SELECT DISTINCT COVERAGE_CODE_ID  FROM POL_VEHICLE_COVERAGES PV        
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID         
 WHERE        
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID = @POL_ID          
  AND  COVERAGE_CODE_ID  IS NOT NULL        
  AND M.STATE_ID=@STATEID        
        
 INSERT INTO @TEMP_COV_RANGES                          
 (        
   LIMIT_DEDUC_ID        
 )        
 SELECT DISTINCT LIMIT_ID  FROM POL_VEHICLE_COVERAGES PV        
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID         
 WHERE        
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID = @POL_ID          
  AND  LIMIT_ID  IS NOT NULL        
  AND M.STATE_ID=@STATEID        
        
 INSERT INTO @TEMP_COV_RANGES                          
 (        
   LIMIT_DEDUC_ID        
 )        
 SELECT DISTINCT DEDUC_ID  FROM POL_VEHICLE_COVERAGES PV        
 INNER JOIN MNT_COVERAGE M ON M.COV_ID=PV.COVERAGE_CODE_ID         
 WHERE        
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID = @POL_ID          
  AND  DEDUC_ID  IS NOT NULL        
  AND M.STATE_ID=@STATEID        
        
-------------------------------------------------------------------                    
                    
             
--If new business select all coverages including Grandfathered        
IF @POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'        
 BEGIN        
 INSERT INTO #COVERAGES                                                                      
 (                                                                      
 COV_ID,                                                       
 COV_CODE,         COV_DESC,                                          
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
 C.EFFECTIVE_TO_DATE  as EFFECTIVE_TO_DATE,        
  C.ISADDDEDUCTIBLE_APP,        
  AVC.ADD_INFORMATION            
 FROM MNT_COVERAGE C                                                       
 LEFT OUTER JOIN POL_VEHICLE_COVERAGES  AVC ON                                                                    
 C.COV_ID = AVC.COVERAGE_CODE_ID         
 AND CUSTOMER_ID = @CUSTOMER_ID        
 AND POLICY_ID = @POL_ID         
 AND POLICY_VERSION_ID = @POL_VERSION_ID                                                                       
 AND VEHICLE_ID = @VEHICLE_ID                                                                    
 WHERE STATE_ID = @STATEID         
 AND LOB_ID = @LOBID         
 AND PURPOSE IN (1 , 3) --Purpose should either new business or both           
 AND C.COV_ID  IN                             
 (                       
   SELECT COV_ID                           
   FROM MNT_COVERAGE  C1                         
    WHERE   ISNULL(C1.DISABLED_DATE,'3000-03-16 16:59:06.630') >= @APP_EFFECTIVE_DATE          
  AND C1.IS_ACTIVE = 'Y'                 
  )         
 OR  AVC.COVERAGE_CODE_ID IS NOT NULL              
 END                
        
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
                      
--Table 0                                                                    
 SELECT * FROM #COVERAGES                                                  
 ORDER BY RANK                                                       
                                          
 --Table 1                                                                   
 --Get Coverage ranges          
--If new Business select all including  grandfathered but not disabled                                                                  
IF @POLICY_STATUS = 'UISSUE' or @POLICY_STATUS = 'SUSPENDED'        
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
                      
                              
                      
                      
 --DROP TABLE #TEMP_APP_LIST                        
 --DROP TABLE #TEMP_COV                      
                       
                                                              
DROP TABLE #COVERAGES                                      
 --Table 2                                                                  
 --Get the State for the Application                                            
 SELECT CS.STATE_ID,        
substring(upper(CS.STATE_NAME),1,1) + substring(lower(CS.STATE_NAME),2,len(CS.STATE_NAME)-1) as STATE_NAME,        
A.POLICY_LOB as LOB_ID,APP_EFFECTIVE_DATE,ALL_DATA_VALID, YEAR(A.APP_EFFECTIVE_DATE ) as APP_YEAR                                  
 FROM POL_CUSTOMER_POLICY_LIST A                       
 INNER JOIN MNT_COUNTRY_STATE_LIST CS ON               
  A.STATE_ID = CS.STATE_ID                             
 WHERE  A.CUSTOMER_ID=@CUSTOMER_ID    AND         
        A.POLICY_ID=@POL_ID     AND                                  
        A.POLICY_VERSION_ID=@POL_VERSION_ID --AND                                  
   --CS.COUNTRY_ID = 1                        
                                         
 --Table 3                                  
 --Get row count of coverages          
                                  
 SELECT *, APP_USE_VEHICLE_ID AS USE_VEHICLE,APP_VEHICLE_PERTYPE_ID as VEHICLE_TYPE_PER,APP_VEHICLE_COMTYPE_ID as VEHICLE_TYPE_COM,'POL' AS CALLEDFROM        
   FROM                                   
  POL_VEHICLES            
 WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                                                          
    POLICY_ID = @POL_ID AND                                 
    POLICY_VERSION_ID = @POL_VERSION_ID                                                                  
    AND  VEHICLE_ID = @VEHICLE_ID          
--TABLE 4        
SELECT convert(int,SUM(ITEM_VALUE)) as SUM_MIS,count(customer_id) as MIS_COUNT FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES         
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                          
    POLICY_ID = @POL_ID AND                                 
    POLICY_VERSION_ID = @POL_VERSION_ID                                                                  
    AND  VEHICLE_ID = @VEHICLE_ID  AND ITEM_VALUE!=0         
        
DECLARE @NO_CLAIMS INT,@NO_PRIOR_LOSS INT        
SELECT @NO_PRIOR_LOSS = COUNT(CUSTOMER_ID)  FROM APP_PRIOR_LOSS_INFO        
 WHERE CUSTOMER_ID=@CUSTOMER_ID         
 --AND LOSS_TYPE=9765         
    AND ISNULL(CLAIMS_TYPE,0)=14234        
 --AND OCCURENCE_DATE >= DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)           
    AND OCCURENCE_DATE >= DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE)           
SELECT @NO_CLAIMS=COUNT(C.CUSTOMER_ID)        
 FROM CLM_CLAIM_INFO C WITH (NOLOCK)                    
 WHERE C.CUSTOMER_ID=@CUSTOMER_ID        
 AND ISNULL(C.IS_ACTIVE,'Y')='Y'         
 AND ISNULL(PINK_SLIP_TYPE_LIST,'') like '%13005%'  -- at fault        
 --AND LOSS_DATE >=DATEADD(DD,-3*365 ,@APP_EFFECTIVE_DATE)           
   AND LOSS_DATE >=DATEADD(DD,-DATEDIFF(DAY,DATEADD(YEAR,-3,@APP_EFFECTIVE_DATE),@APP_EFFECTIVE_DATE) ,@APP_EFFECTIVE_DATE)           
        
        
--TABLE 5        
SELECT @NO_CLAIMS+@NO_PRIOR_LOSS AS NO_CLAIMS        
        
DECLARE @UNDER_25_AGE SMALLINT        
SET @UNDER_25_AGE=0        
        
IF EXISTS        
( SELECT DRIVER_DOB FROM POL_DRIVER_DETAILS AD WITH(NOLOCK)        
-- INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ASV WITH(NOLOCK) ON ASV.CUSTOMER_ID=AD.CUSTOMER_ID AND ASV.POLICY_ID=AD.POLICY_ID         
-- AND ASV.POLICY_VERSION_ID=AD.POLICY_VERSION_ID AND ASV.DRIVER_ID=AD.DRIVER_ID         
 --AND ASV.APP_VEHICLE_PRIN_OCC_ID<>'11931'         
-- AND ASV.VEHICLE_ID=@VEHICLE_ID        
 WHERE AD.CUSTOMER_ID=@CUSTOMER_ID AND AD.POLICY_ID=@POL_ID AND AD.POLICY_VERSION_ID=@POL_VERSION_ID        
 AND DBO.GETAGE(DRIVER_DOB ,@APP_EFFECTIVE_DATE) < 25        
)        
SET @UNDER_25_AGE=1        
--Table 6        
SELECT ISNULL(APPLY_PERS_UMB_POL,0) AS UMB_POL,@UNDER_25_AGE AS UNDER_25_AGE FROM POL_AUTO_GEN_INFO WHERE         
 CUSTOMER_ID = @CUSTOMER_ID AND                                                          
    POLICY_ID = @POL_ID AND                                 
    POLICY_VERSION_ID = @POL_VERSION_ID                                    
                                                                      
                                                                      
END                                                                      
                                                                    
                                                     
        
        
        
        
        
        
        
        