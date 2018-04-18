    
                          
/*----------------------------------------------------------                                                                      
Proc Name             : Dbo.Proc_GetClaimCoveragesReserveDetails                                                                      
Created by            : Santosh Kumar Gautam                                                                     
Date                  : 30 Nov 2010                                                                     
Purpose               : To retrieve the reserve of claim coverages                                                                   
Revison History       :                                                                      
Used In               : To fill dropdown at risk information page.(CLAIM module)                                                                      
------------------------------------------------------------                                                                      
Date     Review By          Comments                                         
                                
drop Proc Proc_GetClaimCoveragesReserveDetails                        
[Proc_GetClaimCoveragesReserveDetails] 1219,0,'NTF',1,3                                                     
------   ------------       -------------------------*/                                                                      
--                                         
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimCoveragesReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimCoveragesReserveDetails]
GO                                     
CREATE PROCEDURE [dbo].[Proc_GetClaimCoveragesReserveDetails]                                          
                                           
 @CLAIM_ID            INT                                  
,@ACTIVITY_ID         INT =0                                     
,@FETCH_MODE          CHAR(3)                        
,@LOB_ID              INT                    
,@LANG_ID             INT=1    
    
AS                                          
BEGIN                    
    
    
DECLARE @NUM_OF_PASS INT=0    
DECLARE @IS_VICTIM_CLAIM CHAR(1)='N'    
    
    
SELECT @IS_VICTIM_CLAIM=CASE WHEN IS_VICTIM_CLAIM=10963 THEN 'Y' ELSE 'N' END FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID    
    
    
  -- SELECT NUMBER OF PASSENGERS FOR THIS CLAIM    
  SELECT @NUM_OF_PASS= PA_NUM_OF_PASS  FROM CLM_INSURED_PRODUCT WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID    
          
----------------------------------------------------------    
-- HERE WE HAVE MINIMUM_DEDUCTIBLE COLUMN IN CLM_PRODICT_COVERAGES, THE VALUE OF THIS COLUMN IN COPY    
-- INTO DEDUCTIBLE_1 COLUMN OF CLM_ACTIVITY_RESERVE TABLE. SO IF ACTIVITY IS NULL THEN WE ARE FETCHING     
-- MINIMUM_DEDUCTIBLE OR DEDUCTIBLE_1    
----------------------------------------------------------    
    
               
                          
-- THIS MEANS THAT RESERVE PAGE IS CALLED FROM ACTIVITY SCREEN                          
-- AND RESERVE PAGE CAN OPEN EITHER FOR NEW ACTIVITY OR FOR EXISTING ACTIVITY                          
-- IN THIS CASE ACTIVITY_ID CANNOT BE 0 OR NULL                          
IF(@FETCH_MODE='ACT')                           
 BEGIN                          
                          
   -- IF ACTIVITY NOT EXISTS IN CLM_ACTIVITY_RESERVE THIS MEANS THAT RESERVE PAGE BEING CALLED FROM ACTIVITY SCREEN FOR NEW ACTIVITY                        
   -- SO COPY THE DETAILS OF LAST COMPLETED ACTIVITY TO CLM_ACTIVITY_RESERVE WITH TRANSACTION COLUMNS WOULD BE 0                        
   -- IF ACTIVITY IS EXISTS IN CLM_ACTIVITY_RESERVE THIS MEANS THAT RESERVE PAGE BEING CALLED FROM ACTIVITY SCREEN FOR AN ACTIVITY WITH                         
   -- UPDATE MODE                        
                          
    IF(NOT EXISTS(SELECT ACTIVITY_ID FROM CLM_ACTIVITY_RESERVE WITH(NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID))                        
    BEGIN                        
           
      -- TO COPY LAST COMPLETED ACTIVTIY RECORDS      
        EXEC [Proc_CopyReserveDetails] @CLAIM_ID,@ACTIVITY_ID,1      
                  
     -- TO CALCULATE BREAKDOWN            
       EXEC Proc_CalculateBreakdown @CLAIM_ID,@ACTIVITY_ID    
                    
      END                 
         
  SELECT  R.ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,R.ACTUAL_RISK_ID AS RISK_ID,R.RESERVE_ID, R.OUTSTANDING,R.RI_RESERVE,R.CO_RESERVE,                 
    R.PAYMENT_AMOUNT,R.PREV_OUTSTANDING, R.RECOVERY_AMOUNT,                        
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,    
    CASE WHEN @LOB_ID=22 THEN ROUND(ISNULL(C.LIMIT_1,0)/@NUM_OF_PASS,1)  ELSE  C.LIMIT_1 END AS LIMIT,    
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE ,    
    R.TOTAL_PAYMENT_AMOUNT ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE,C.DEDUCTIBLE1_AMOUNT_TEXT ,    
    R.DEDUCTIBLE_1   ,R.ADJUSTED_AMOUNT ,R.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE,    
    R.PERSONAL_INJURY,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID,    
    -- TO FETCH WHETHER ANY RESERVE OR RECOVERY OR PAYMENT IS REMAINING OR NOT    
    CASE WHEN     
    EXISTS(    
     SELECT COMP_ID FROM CLM_ACTIVITY_CO_RI_BREAKDOWN     
     WHERE  CLAIM_ID=R.CLAIM_ID      AND ACTIVITY_ID=R.ACTIVITY_ID AND    
         RESERVE_ID=R.RESERVE_ID AND    
        (PAYMENT_AMT <>0 or RESERVE_AMT<>0) AND    
         COMP_TYPE in ('RI','CO')    
      )    
     THEN 'Y' ELSE 'N' END AS IS_RECOVERY_PENDING,    
     @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM ,  'NRML' AS RESERVE_TYPE                                 
  FROM CLM_PRODUCT_COVERAGES C  WITH(NOLOCK)  LEFT OUTER JOIN                                           
    CLM_ACTIVITY_RESERVE R WITH(NOLOCK) ON R.CLAIM_ID=C.CLAIM_ID AND R.COVERAGE_ID=C.CLAIM_COV_ID  LEFT OUTER JOIN                               
    MNT_COVERAGE M WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=M.COV_ID  LEFT OUTER JOIN         
    MNT_COVERAGE_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID LEFT OUTER JOIN                  
    CLM_VICTIM_INFO V WITH(NOLOCK) ON R.CLAIM_ID =V.CLAIM_ID AND R.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'     
  WHERE  (C.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND IS_RISK_COVERAGE='Y' )--AND R.VICTIM_ID!= CASE WHEN @LOB_ID IN(17,18,22) THEN 0 ELSE -1 END )                                           
  UNION                             
  SELECT  R.ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,R.ACTUAL_RISK_ID AS RISK_ID,R.RESERVE_ID, R.OUTSTANDING,R.RI_RESERVE,R.CO_RESERVE,               
    R.PAYMENT_AMOUNT,R.PREV_OUTSTANDING, R.RECOVERY_AMOUNT,                                    
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,C.LIMIT_1 AS LIMIT,C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE   ,    
    R.TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE    ,C.IS_RISK_COVERAGE  ,C.DEDUCTIBLE1_AMOUNT_TEXT  ,    
    R.DEDUCTIBLE_1   ,R.ADJUSTED_AMOUNT       ,R.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE,    
    R.PERSONAL_INJURY ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID ,    
     -- TO FETCH WHETHER ANY RESERVE OR RECOVERY OR PAYMENT IS REMAINING OR NOT    
    CASE WHEN     
    EXISTS(    
     SELECT COMP_ID FROM CLM_ACTIVITY_CO_RI_BREAKDOWN     
     WHERE  CLAIM_ID=R.CLAIM_ID      AND ACTIVITY_ID=R.ACTIVITY_ID AND    
         RESERVE_ID=R.RESERVE_ID AND    
        (PAYMENT_AMT <>0 or RESERVE_AMT<>0) AND    
         COMP_TYPE in ('RI','CO')    
      )    
      THEN 'Y' ELSE 'N' END AS IS_RECOVERY_PENDING ,    
       @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM ,M.RESERVE_TYPE               
  FROM CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                            
    CLM_ACTIVITY_RESERVE R WITH(NOLOCK) ON R.CLAIM_ID=C.CLAIM_ID AND R.COVERAGE_ID=C.CLAIM_COV_ID LEFT OUTER JOIN                               
    MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID LEFT OUTER JOIN        
    MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID  LEFT OUTER JOIN                  
    CLM_VICTIM_INFO V WITH(NOLOCK) ON R.CLAIM_ID =V.CLAIM_ID AND R.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'                       
  WHERE   (C.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND IS_RISK_COVERAGE='N')          
     ORDER BY VICTIM    
                          
                            
                           
  END-- END OF @FETCH_MODE IF STATEMENT                          
 ELSE                          
-- THIS MEANS THAT RESERVE PAGE IS CALLED FROM NOTIFICATION SCREEN                          
-- AND RESERVE PAGE CAN OPEN EITHER FOR NEW ACTIVITY OR FOR EXISTING ACTIVITY                          
-- IN THIS CASE ACTIVITY_ID WOULD BE 0 OR NULL                          
 BEGIN                   
                      
    SELECT TOP 1 @ACTIVITY_ID=ACTIVITY_ID              
    FROM  CLM_ACTIVITY  WITH(NOLOCK)                          
    WHERE ( ACTIVITY_STATUS=11801 -- COMPLETED ACTIVITY                          
   AND CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'               
     )                          
    ORDER BY ACTIVITY_ID DESC                          
                            
    IF (@ACTIVITY_ID IS NOT NULL AND @ACTIVITY_ID>0 )                          
    BEGIN                            
      SELECT  R.ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,R.ACTUAL_RISK_ID AS RISK_ID,R.RESERVE_ID, R.OUTSTANDING,R.RI_RESERVE,R.CO_RESERVE,                  
     R.PAYMENT_AMOUNT,R.PREV_OUTSTANDING, R.RECOVERY_AMOUNT,                                
     ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,    
     CASE WHEN @LOB_ID=22 THEN ROUND(ISNULL(C.LIMIT_1,0)/@NUM_OF_PASS,1)  ELSE  C.LIMIT_1 END AS LIMIT,    
     C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE ,    
     R.TOTAL_PAYMENT_AMOUNT ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE ,C.DEDUCTIBLE1_AMOUNT_TEXT ,    
     R.DEDUCTIBLE_1   ,R.ADJUSTED_AMOUNT     ,R.VICTIM_ID, V.NAME AS VICTIM  ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE,    
     R.PERSONAL_INJURY,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID ,    
      -- TO FETCH WHETHER ANY RESERVE OR RECOVERY OR PAYMENT IS REMAINING OR NOT    
    CASE WHEN     
    EXISTS(    
     SELECT COMP_ID FROM CLM_ACTIVITY_CO_RI_BREAKDOWN     
     WHERE  CLAIM_ID=R.CLAIM_ID      AND ACTIVITY_ID=R.ACTIVITY_ID AND    
         RESERVE_ID=R.RESERVE_ID AND    
        (PAYMENT_AMT <>0 or RESERVE_AMT<>0) AND    
         COMP_TYPE in ('RI','CO')    
      )    
       THEN 'Y' ELSE 'N' END AS IS_RECOVERY_PENDING,    
         @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM ,  'NRML' AS RESERVE_TYPE             
      FROM    CLM_PRODUCT_COVERAGES C WITH(NOLOCK)  LEFT OUTER JOIN                                         
     CLM_ACTIVITY_RESERVE R WITH(NOLOCK) ON R.CLAIM_ID=C.CLAIM_ID AND  R.COVERAGE_ID=C.CLAIM_COV_ID LEFT OUTER JOIN                                  
     MNT_COVERAGE M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID       LEFT OUTER JOIN         
              MNT_COVERAGE_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID  LEFT OUTER JOIN                  
     CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND R.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'     
      WHERE (C.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND IS_RISK_COVERAGE='Y')                                
      UNION                             
      SELECT R.ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,R.ACTUAL_RISK_ID AS RISK_ID,R.RESERVE_ID, R.OUTSTANDING,R.RI_RESERVE,R.CO_RESERVE,                 
    R.PAYMENT_AMOUNT,R.PREV_OUTSTANDING,    R.RECOVERY_AMOUNT,                              
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,C.LIMIT_1 AS LIMIT,C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE    ,    
    R.TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE  ,C.DEDUCTIBLE1_AMOUNT_TEXT ,    
    R.DEDUCTIBLE_1   ,R.ADJUSTED_AMOUNT     ,R.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE,    
    R.PERSONAL_INJURY   ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID ,    
     -- TO FETCH WHETHER ANY RESERVE OR RECOVERY OR PAYMENT IS REMAINING OR NOT    
    CASE WHEN     
    EXISTS(    
     SELECT COMP_ID FROM CLM_ACTIVITY_CO_RI_BREAKDOWN     
     WHERE  CLAIM_ID=R.CLAIM_ID      AND ACTIVITY_ID=R.ACTIVITY_ID AND    
         RESERVE_ID=R.RESERVE_ID AND    
        (PAYMENT_AMT <>0 or RESERVE_AMT<>0) AND    
         COMP_TYPE in ('RI','CO')    
      )    
       THEN 'Y' ELSE 'N' END AS IS_RECOVERY_PENDING   ,    
         @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM      ,M.RESERVE_TYPE                                      
      FROM   CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                           
    CLM_ACTIVITY_RESERVE R WITH(NOLOCK) ON R.CLAIM_ID=C.CLAIM_ID AND R.COVERAGE_ID=C.CLAIM_COV_ID LEFT OUTER JOIN                                  
    MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID LEFT OUTER JOIN           
    MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID LEFT OUTER JOIN                  
    CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND R.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'     
      WHERE  (C.CLAIM_ID=@CLAIM_ID AND R.ACTIVITY_ID=@ACTIVITY_ID AND IS_RISK_COVERAGE='N')                               
      ORDER BY VICTIM    
    END                            
    ELSE                            
                                
    BEGIN      
        
    --------------------------------------------------------------------    
    -- THIS QUERY WILL EXECUTE WHEN USER SET THE RESERVE FIRST TIME                           
    --------------------------------------------------------------------    
        
  -----------------------------------------------------------------------     
  -- 22 : FOR LOB Personal Accident for Passengers     
  --      FOR THIS LOB SUM INSURED IS DIVIDED INTO NUMBER OF PASSENGERS    
  --      IF USER IS NOT ADDED ANY VITIM IN CLAIM THEN SHOW THE COVERAGES WITHOUT GROUPING    
  -----------------------------------------------------------------------        
  IF(@LOB_ID =22 AND @IS_VICTIM_CLAIM='Y')       
    BEGIN       
          
     SELECT   NULL AS ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                              
     NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING,  NULL AS RECOVERY_AMOUNT,                       
     ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,    
     ROUND(ISNULL(C.LIMIT_1,0)/@NUM_OF_PASS,1) AS LIMIT,-- SUM INSURED AMOUNT    
     C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE,      
     0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE ,C.DEDUCTIBLE1_AMOUNT_TEXT ,         
     C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1   ,0 AS ADJUSTED_AMOUNT  ,V.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,      
     'N' AS PERSONAL_INJURY  ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID,'N' AS IS_RECOVERY_PENDING,    
     @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM  ,  'NRML' AS RESERVE_TYPE         
   FROM CLM_VICTIM_INFO V WITH(NOLOCK),     
     CLM_PRODUCT_COVERAGES C WITH(NOLOCK) LEFT OUTER JOIN                                    
     MNT_COVERAGE M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID       LEFT OUTER JOIN           
     MNT_COVERAGE_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID                
   WHERE   V.CLAIM_ID=@CLAIM_ID AND C.CLAIM_ID=@CLAIM_ID AND C.IS_RISK_COVERAGE='Y'    
   UNION    
   SELECT  NULL AS ACTIVITY_ID ,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                              
     NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING, NULL AS RECOVERY_AMOUNT,                     
     ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,C.LIMIT_1 AS LIMIT,C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE ,      
     0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE   ,C.DEDUCTIBLE1_AMOUNT_TEXT ,         
     C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1  ,0 AS ADJUSTED_AMOUNT  ,V.VICTIM_ID, V.NAME AS VICTIM  ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,      
     'N' AS PERSONAL_INJURY ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID,'N' AS IS_RECOVERY_PENDING,    
     @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM   ,M.RESERVE_TYPE      
   ---------------------------------------------------------------    
      -- ADDED BY SANTOSH KR GAUTAM ON 14 JUL 2011 (REF ITRACK:1031)       
      ---------------------------------------------------------------    
      FROM    CLM_VICTIM_INFO V WITH(NOLOCK),     
        CLM_PRODUCT_COVERAGES C WITH(NOLOCK) LEFT OUTER JOIN                                    
        MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID       LEFT OUTER JOIN           
        MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID                
         WHERE   V.CLAIM_ID=@CLAIM_ID AND C.CLAIM_ID=@CLAIM_ID AND C.IS_RISK_COVERAGE='N'      
                    
   --FROM   CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                                    
   --    MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID    LEFT OUTER JOIN      
   --    MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID  LEFT OUTER JOIN                    
   --    CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND C.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'                                   
   --WHERE (C.CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='N')         
   ORDER BY VICTIM               
       
    END    
  ---------------------------------------------------------------    
  -- ADDED BY SANTOSH KR GAUTAM ON 14 JUL 2011 (REF ITRACK:1031)         
  ---------------------------------------------------------------    
  -- FOR LOB : FACULTATIVE LIABILITY    
  --         : CIVIL LIABLITY TRANSPORTATION    
  ---------------------------------------------------------------    
  ELSE IF(@IS_VICTIM_CLAIM='Y' AND @LOB_ID IN(17,18))     
  BEGIN    
       
     SELECT * FROM     
        (    
        SELECT   NULL AS ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                            
    NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING,  NULL AS RECOVERY_AMOUNT,                     
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,CASE WHEN @LOB_ID=22 THEN ROUND(ISNULL(C.LIMIT_1,0)/@NUM_OF_PASS,1)  ELSE  C.LIMIT_1 END AS LIMIT,    
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE,    
    0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE ,C.DEDUCTIBLE1_AMOUNT_TEXT ,       
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1   ,0 AS ADJUSTED_AMOUNT  ,C.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,    
    'N' AS PERSONAL_INJURY  ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID ,'N' AS IS_RECOVERY_PENDING,    
      @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM    ,  'NRML' AS RESERVE_TYPE              
   FROM   CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                                  
    MNT_COVERAGE M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID    LEFT OUTER JOIN    
    MNT_COVERAGE_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID    LEFT OUTER JOIN                  
     CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND C.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'                  
   WHERE (C.CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='Y' AND C.VICTIM_ID= CASE WHEN @IS_VICTIM_CLAIM='Y' AND @LOB_ID IN(17,18,22) THEN V.VICTIM_ID  ELSE C.VICTIM_ID END)                                
   UNION                             
   SELECT NULL AS ACTIVITY_ID ,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                            
      NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING, NULL AS RECOVERY_AMOUNT,                   
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,C.LIMIT_1 AS LIMIT,C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE ,    
    0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE   ,C.DEDUCTIBLE1_AMOUNT_TEXT ,       
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1  ,0 AS ADJUSTED_AMOUNT  ,V.VICTIM_ID, V.NAME AS VICTIM  ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,    
    'N' AS PERSONAL_INJURY ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID,'N' AS IS_RECOVERY_PENDING ,    
      @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM  ,M.RESERVE_TYPE          
  FROM CLM_VICTIM_INFO V WITH(NOLOCK),     
     CLM_PRODUCT_COVERAGES C WITH(NOLOCK) LEFT OUTER JOIN                                    
     MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID       LEFT OUTER JOIN           
     MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID                
     WHERE   V.CLAIM_ID=@CLAIM_ID AND C.CLAIM_ID=@CLAIM_ID AND C.IS_RISK_COVERAGE='N'    
      
     )TEMP    
     WHERE TEMP.VICTIM_ID IN (SELECT VICTIM_ID FROM CLM_PRODUCT_COVERAGES  WHERE CLAIM_ID=@CLAIM_ID )    
  ORDER BY VICTIM          
      
      
  END      
  ELSE    
    BEGIN    
   SELECT NULL AS ACTIVITY_ID,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                            
    NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING,  NULL AS RECOVERY_AMOUNT,                     
    ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,CASE WHEN @LOB_ID=22 THEN ROUND(ISNULL(C.LIMIT_1,0)/@NUM_OF_PASS,1)  ELSE  C.LIMIT_1 END AS LIMIT,    
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE,    
    0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE ,C.DEDUCTIBLE1_AMOUNT_TEXT ,       
    C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1   ,0 AS ADJUSTED_AMOUNT  ,C.VICTIM_ID, V.NAME AS VICTIM ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,    
    'N' AS PERSONAL_INJURY  ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID ,'N' AS IS_RECOVERY_PENDING,    
      @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM    ,  'NRML' AS RESERVE_TYPE              
   FROM   CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                                  
    MNT_COVERAGE M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID    LEFT OUTER JOIN    
    MNT_COVERAGE_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=@LANG_ID    LEFT OUTER JOIN                  
     CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND C.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'                  
   WHERE (C.CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='Y' AND C.VICTIM_ID= CASE WHEN @IS_VICTIM_CLAIM='Y' AND @LOB_ID IN(17,18,22) THEN V.VICTIM_ID  ELSE C.VICTIM_ID END)                                
  -- UNION                             
  -- SELECT NULL AS ACTIVITY_ID ,C.CLAIM_COV_ID AS COVERAGE_ID,C.RISK_ID,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                            
  --    NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING, NULL AS RECOVERY_AMOUNT,                   
  --  ISNULL(N.COV_DES,M.COV_DES)AS COV_DES,C.LIMIT_1 AS LIMIT,C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE ,    
  --  0 AS TOTAL_PAYMENT_AMOUNT  ,C.LIMIT_OVERRIDE ,C.IS_RISK_COVERAGE   ,C.DEDUCTIBLE1_AMOUNT_TEXT ,       
  --  C.MINIMUM_DEDUCTIBLE AS DEDUCTIBLE_1  ,0 AS ADJUSTED_AMOUNT  ,C.VICTIM_ID, V.NAME AS VICTIM  ,C.DEDUCTIBLE_1 AS MAX_DEDUCTIBLE  ,    
  --  'N' AS PERSONAL_INJURY ,C.COVERAGE_CODE_ID AS ACTUAL_COVERAGE_ID,'N' AS IS_RECOVERY_PENDING ,    
  --    0 AS IS_VICTIM_CLAIM  ,M.RESERVE_TYPE            
  -- FROM   CLM_PRODUCT_COVERAGES C  WITH(NOLOCK) LEFT OUTER JOIN                                                  
  --  MNT_COVERAGE_EXTRA M WITH(NOLOCK) ON C.COVERAGE_CODE_ID=COV_ID    LEFT OUTER JOIN    
  --  MNT_COVERAGE_EXTRA_MULTILINGUAL N WITH(NOLOCK)  ON C.COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=3  LEFT OUTER JOIN                  
  --   CLM_VICTIM_INFO V WITH(NOLOCK) ON C.CLAIM_ID =V.CLAIM_ID AND C.VICTIM_ID=V.VICTIM_ID AND V.IS_ACTIVE='Y'                                 
  --WHERE (C.CLAIM_ID=1221 AND IS_RISK_COVERAGE='N')       
  ORDER BY VICTIM      
  --SELECT NULL AS ACTIVITY_ID ,mc.COV_ID AS COVERAGE_ID,1 as RISK_ID,1 AS LIMIT_OVERRIDE, 1 AS IS_RISK_COVERAGE,NULL AS RESERVE_ID, NULL AS OUTSTANDING,NULL AS RI_RESERVE,NULL AS CO_RESERVE,                            
  --    NULL AS PAYMENT_AMOUNT,NULL AS PREV_OUTSTANDING, NULL AS RECOVERY_AMOUNT,                   
  --MC.COV_DES AS COV_DES,MCR.LIMIT_DEDUC_AMOUNT AS LIMIT,MCR.LIMIT_DEDUC_AMOUNT1 AS DEDUCTIBLE ,    
  -- 0 AS TOTAL_PAYMENT_AMOUNT  ,MCR.LIMIT_DEDUC_AMOUNT_TEXT ,       
  --  MCR.LIMIT_DEDUC_AMOUNT1 AS DEDUCTIBLE_1  ,0 AS ADJUSTED_AMOUNT ,MCR.LIMIT_DEDUC_AMOUNT1 AS MAX_DEDUCTIBLE  ,MCR.LIMIT_DEDUC_AMOUNT1_TEXT AS DEDUCTIBLE1_AMOUNT_TEXT,1 AS VICTIM_ID,NULL AS VICTIM,NULL AS RESERVE_TYPE ,    
  --  'N' AS PERSONAL_INJURY ,MC.COV_ID AS ACTUAL_COVERAGE_ID,'N' AS IS_RECOVERY_PENDING ,    
  --   @IS_VICTIM_CLAIM AS IS_VICTIM_CLAIM     from MNT_COVERAGE MC inner join MNT_COVERAGE_RANGES MCR on MC.COV_ID=MCR.COV_ID and  mc.STATE_ID=92 and mc.LOB_ID=38   
  --   --INNER JOIN CLM_INSURED_PRODUCT CIP  
    END                           
                                         
    END                            
                           
 END               
                           
                       
                       
 --- FETCH RISK DETAILS BASED ON THE CLAIM ID (IT IS USED TO SHOW HEADING IN GRID)                      
                      
 IF(@LOB_ID IN (1,9,26,10,11,12,14,16,19,25,27,32))--Comprehensive Condominium/DWELLING/GENERAL CIVIL LIABILITY/Diversified Risks/ RObbery /Comprehensive Company                             
 BEGIN                              
                                
  SELECT (ISNULL(LOCATION_ADDRESS,'')+'-'+ISNULL(LOCATION_COMPLIMENT,'')+'-'+                           
          ISNULL(LOCATION_DISTRICT,'')+'-' +                                
         ISNULL(C.COUNTRY_NAME,'')+'-'+ISNULL(S.STATE_NAME,''))                      
         AS INSURED_OBJECT           
  FROM CLM_INSURED_PRODUCT P WITH(NOLOCK) LEFT OUTER JOIN                      
       MNT_COUNTRY_LIST C WITH(NOLOCK) ON P.COUNTRY1=C.COUNTRY_ID LEFT OUTER JOIN                      
       MNT_COUNTRY_STATE_LIST S WITH(NOLOCK)  ON P.COUNTRY1=S.COUNTRY_ID AND P.STATE1=S.STATE_ID                      
  WHERE (CLAIM_ID=@CLAIM_ID)                          
                       
 END               
                                     
                                
 IF(@LOB_ID=13) -- MARITIME                              
 BEGIN                                        
                           
  SELECT (ISNULL(VESSEL_TYPE,'')+'-'+ISNULL(VESSEL_TYPE,'')+'-'+                                
         ISNULL(CONVERT(VARCHAR(10), [YEAR]),'')+'-' +                                
         ISNULL(VESSEL_MANUFACTURER,''))                      
         AS INSURED_OBJECT                           
  FROM CLM_INSURED_PRODUCT  WITH(NOLOCK)                              
  WHERE (CLAIM_ID=@CLAIM_ID)                          
                                    
  END                                   
                                
---------------------------------------------------------    
 -- 21 : Group Personal Accident for Passenger     
 -- 34 : Group Life     
 -- 15 : Individual Personal Accident    
 -- 33 : Mortgage    
 ---------------------------------------------------------              
 IF(@LOB_ID IN (21,34,15,33))       
 BEGIN                              
                                  
  SELECT ISNULL(INSURED_NAME,'')                             
          AS INSURED_OBJECT                                 
   FROM CLM_INSURED_PRODUCT  WITH(NOLOCK)                              
   WHERE (CLAIM_ID=@CLAIM_ID)                      
                         
  END                                   
                                
 ---------------------------------------------------------    
 -- 17 : Facultative Liability   (THIS IS MASTER POLICY)        
 -- 18 : Civil Liability Transportation   (THIS IS MASTER POLICY)        
 -- 28 : Aeronautic    
 -- 29 : Motor    
 -- 31 : Cargo Transportation Civil Liability    
 ---------------------------------------------------------                    
IF(@LOB_ID IN(17,18,28,29,31,38))                        
 BEGIN                              
                                
  SELECT                    
  (ISNULL(VEHICLE_VIN,'')+'-'+ISNULL(CAST([YEAR] AS NVARCHAR(4)),'')+'-'+                                
   ISNULL( VEHICLE_MAKER,'')+'-' + ISNULL(VEHICLE_MODEL,''))                               
         AS INSURED_OBJECT                                
   FROM CLM_INSURED_PRODUCT   WITH(NOLOCK)                             
   WHERE (CLAIM_ID=@CLAIM_ID)                      
  END                                 
                                     
 ---------------------------------------------------------     
  --  20 : National Cargo Transport    
  --  23 : International Cargo Transport    
 ---------------------------------------------------------                       
 IF(@LOB_ID IN (20,23))                          
   BEGIN                              
                                
   SELECT  (ISNULL(VOYAGE_CONVEYENCE_TYPE,'')+'-'+ISNULL(CITY1,'')+'-'+                      
   ISNULL(P.COUNTRY1,'')+'-'+ISNULL(P.STATE1,''))                         
           AS INSURED_OBJECT                                
   FROM CLM_INSURED_PRODUCT P WITH(NOLOCK)                         
   WHERE (CLAIM_ID=@CLAIM_ID)                      
                        
 END                               
                             
   ---------------------------------------------------------     
  -- 30 : Dpvat(Cat. 3 e 4)    
  -- 36 : DPVAT(Cat.1,2,9 e 10)    
  ---------------------------------------------------------          
  IF(@LOB_ID IN (30,36))        
  BEGIN          
       
   SELECT  (ISNULL(CAST( DP_TICKET_NUMBER AS NVARCHAR(20)),'')+'-'+ISNULL(S.STATE_NAME,''))              
          AS INSURED_OBJECT                                
   FROM CLM_INSURED_PRODUCT P WITH(NOLOCK) LEFT OUTER JOIN                      
       MNT_COUNTRY_STATE_LIST S WITH(NOLOCK)  ON  P.STATE1=S.STATE_ID                             
   WHERE (CLAIM_ID=@CLAIM_ID)                      
       
       
  END    
      
 ---------------------------------------------------------     
  -- 22 : Personal Accident for Passengers     
 ---------------------------------------------------------        
  IF(@LOB_ID =22)       
   BEGIN        
       
   SELECT  (ISNULL(CAST( PA_NUM_OF_PASS AS NVARCHAR(20)),'')+'-'+    
           ISNULL(Convert(varchar,EFFECTIVE_DATE,case when  @LANG_ID =2 then 103 else 101 end) ,'')        
          +'-'+ ISNULL( Convert(varchar,EXPIRE_DATE,case when  @LANG_ID =2 then 103 else 101 end) ,''))    
          AS INSURED_OBJECT                                
   FROM  CLM_INSURED_PRODUCT P WITH(NOLOCK)    
   WHERE (CLAIM_ID=@CLAIM_ID)                 
       
       
   END    
      
  ---------------------------------------------------------     
  -- 35 : Rural Lien    
---------------------------------------------------------         
  IF(@LOB_ID = 35)    
   BEGIN          
     SELECT (ISNULL(CAST(ITEM_NUMBER AS NVARCHAR(20)),'')+'-'+ISNULL(S.STATE_NAME,'')    
             +'-'+ISNULL(CITY1,'') +'-'+ISNULL(CAST( RURAL_INSURED_AREA AS NVARCHAR(20)) ,'')    
             )              
            AS INSURED_OBJECT                                
     FROM   CLM_INSURED_PRODUCT P WITH(NOLOCK) LEFT OUTER JOIN                      
            MNT_COUNTRY_STATE_LIST S WITH(NOLOCK) on  P.STATE1=S.STATE_ID                             
     WHERE  (CLAIM_ID=@CLAIM_ID)                      
    
      
   END    
       
---------------------------------------------------------     
  -- 37 : Renatl Security    
---------------------------------------------------------         
  IF(@LOB_ID = 37)    
   BEGIN      
      SELECT (ISNULL(CAST(ITEM_NUMBER AS NVARCHAR(20)),'')+'-'+ISNULL(ACTUAL_INSURED_OBJECT,'')                
             )              
            AS INSURED_OBJECT                                
     FROM   CLM_INSURED_PRODUCT P WITH(NOLOCK)                       
     WHERE  (CLAIM_ID=@CLAIM_ID)          
      
   END    
                                
  -- SELECT ACTIVITY DETAILS              
                             
    SELECT ACTIVITY_REASON,ACTIVITY_STATUS,ACTION_ON_PAYMENT           
    FROM  CLM_ACTIVITY  WITH(NOLOCK)                          
    WHERE ( CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID )       
    
    --select * from CLM_ACTIVITY where CLAIM_ID = 1219               
                                  
                  
                  
END 