IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PPNG_REPORT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PPNG_REPORT] 
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[PROC_PPNG_REPORT]   
@FROM_DATETIME DATETIME =NULL,                              
@TO_DATETIME DATETIME =NULL     
-- @DATETIME  DATETIME=null            
-- @To_DATETIME  DATETIME ='09/20/2011'                  
 --@POLICY_NUMBER VARCHAR(21)=NULL                    
AS                    
BEGIN                    
 SET NOCOUNT ON;       
       
 SELECT * INTO #TEMP    
  FROM [VW_REPORT_FORM_PPNG]      
 UPDATE T1      
        
  SET T1.APIPD_TOTAL_PREMIUM =  ISNULL(T1.APIPD_TOTAL_PREMIUM,0) - ISNULL(T2.APIPD_TOTAL_PREMIUM,0)                     
 ,T1.APIPD_1_TOTAL_PREMIUM =  ISNULL(T1.APIPD_1_TOTAL_PREMIUM,0) -                       
         ISNULL(T2.APIPD_1_TOTAL_PREMIUM,0)      
            
  FROM #TEMP T1      
 INNER JOIN #TEMP T2      
 ON T1.CUSTOMER_ID = T2.CUSTOMER_ID AND  T2.POLICY_ID = T1.POLICY_ID       
 AND T2.POLICY_VERSION_ID = T1.POLICY_VERSION_ID      
  WHERE (       
  (T1.CO_INSURANCE IN(14547,14548) AND (T1.LEADER_FOLLOWER IS NULL OR T1.LEADER_FOLLOWER IN (14548)))      
   OR       
   (T1.CO_INSURANCE IN(14549) AND (T1.LEADER_FOLLOWER IS NULL OR T1.LEADER_FOLLOWER IN (14549)))        
  )      
  AND T1.COMPANY_ID =0       
  AND T2.COMPANY_ID <>0      
       
       
       
                    
                     
 --------------------------- SEPRATE DATE AND TIME                    
 --DECLARE @MONTH VARCHAR(2)                    
 --DECLARE @YEAR VARCHAR(4)                    
 --SET @MONTH= DATEPART(MM,@DATETIME)                    
                     
 DECLARE @LAST_DAY DATETIME                    
 --SET @LAST_DAY=(SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@DATETIME)+1,0)))                    
    SET @LAST_DAY = (SELECT CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(DATEADD(mm,1,@TO_DATETIME))),DATEADD(mm,1,@TO_DATETIME)),101))                 
 --SET @YEAR=(SELECT DATEPART(YY,@DATETIME))              
             
                   
                    
     --------------------------- CREATE  TEMPORARY TABLE FOR HOLDING VALUES                    
 CREATE TABLE #TEMPTABLE                    
 (                    
  SEQUENCE_NO INT identity(1,1)                    
                      
  ,TRANSACTION_TYPE VARCHAR(5)                    
  ,PREMIUM_TYPE VARCHAR(5)                    
  ,SUSEP_LINE_OF_BUSINESS VARCHAR(5)                    
                      
  ,ORIGINAL_EFFECTIVE_DATE DATETIME                    
  ,ORIGINAL_EXPIRY_DATE DATETIME                    
  ,ORIGINAL_ISSUE_DATE DATETIME                    
  ,ORIGINAL_RISK_PREMIUM DECIMAL(25,8)                    
  ,SECONDARY_EFFECTIVE_DATE DATETIME                    
  ,SECONDARY_EXPIRY_DATE DATETIME                    
  ,SECONDARY_ISSUE_DATE DATETIME                    
  ,SECONDARY_RISK_PREMIUM DECIMAL(25,8)                    
  ,SUSEP_CODE_OF_INSURER VARCHAR(5)                    
  ,NO_OF_REGISTRIES VARCHAR(4)                    
  ,POLICY_NUMBER VARCHAR(21)    
      
  ,ENDORSEMENT_NUMBER VARCHAR(50)    
  ,UNEARNED_PREMIUM DECIMAL(25,8)    
  ,COMMISSION_AMOUNT DECIMAL(25,8)    
  ,DEFFERED_ACQUISITION_COST DECIMAL(25,8)      
  ,PF  DECIMAL(25,8)                
  --,POLICY_ID INT                    
  --,POLICY_VERSION_ID INT                    
  --,TRAN_TYPE VARCHAR(5)                    
 )                    
                     
 --------------------- INSERTION IN TO TEMPARORY TABLE                    
                    
 INSERT INTO #TEMPTABLE                    
 (                    
                       
  TRANSACTION_TYPE                     
  ,PREMIUM_TYPE                     
  ,SUSEP_LINE_OF_BUSINESS                     
                     
  ,ORIGINAL_EFFECTIVE_DATE           
  ,ORIGINAL_EXPIRY_DATE                     
  ,ORIGINAL_ISSUE_DATE                     
  ,ORIGINAL_RISK_PREMIUM                     
  ,SECONDARY_EFFECTIVE_DATE                    
  ,SECONDARY_EXPIRY_DATE                    
  ,SECONDARY_ISSUE_DATE                     
  ,SECONDARY_RISK_PREMIUM                     
  ,SUSEP_CODE_OF_INSURER                     
  ,NO_OF_REGISTRIES                     
  ,POLICY_NUMBER     
  ,ENDORSEMENT_NUMBER     
  ,UNEARNED_PREMIUM     
  ,COMMISSION_AMOUNT     
  ,DEFFERED_ACQUISITION_COST    
               
  --,POLICY_ID                     
  --,POLICY_VERSION_ID                     
  --,TRAN_TYPE                    
 )                    
                    
 SELECT                     
   FINAL_PPR.TRANSACTION_TYPE                    
  ,FINAL_PPR.PREMIUM_TYPE                    
  ,FINAL_PPR.SUSEP_LOB_CODE                    
  ,FINAL_PPR.ORIGINAL_EFFECTIVE_DATE                    
  ,FINAL_PPR.ORIGINAL_EXPIRY_DATE                    
  ,FINAL_PPR.ORIGINAL_ISSUE_DATE                    
  ,FINAL_PPR.ORIGINAL_PREMIUM                    
  ,FINAL_PPR.SECONDARY_EFFECTIVE_DATE                    
  ,FINAL_PPR.SECONDARY_EXPIRY_DATE                    
  ,FINAL_PPR.SECONDARY_ISSUE_DATE                    
  ,FINAL_PPR.SECONDARY_PREMIUM                    
  ,FINAL_PPR.SUSEP_OF_INSURERE                    
  ,COUNT(1) AS NO_OF_REGISTRIES                    
  ,FINAL_PPR.POLICY_NUMBER    
  ,FINAL_PPR.ENDORSEMENT_NO    
  ,FINAL_PPR.UNEARNED_PREMIUM    
  ,FINAL_PPR.COMMISSION_AMOUNT    
  ,FINAL_PPR.DEFFERED_ACQUISITION_COST    
                 
  --,FINAL_PPR.POLICY_ID                    
  --,FINAL_PPR.POLICY_VERSION_ID                    
  --,FINAL_PPR.TRAN_TYPE                    
 FROM                     
 (                    
  SELECT                    
    -----------------------  FOR TRANSACTION TYPE                  
    CASE                  
    WHEN VWS.PROCESS_ID IN(25,18) THEN '0007'                  
    WHEN VWS.PROCESS_ID IN(14) AND VWS.TOTAL_TRAN_PREMIUM > 0 THEN '0008'                  
    WHEN VWS.PROCESS_ID IN(14,37) AND VWS.TOTAL_TRAN_PREMIUM < 0 THEN '0009'               
    WHEN VWS.PROCESS_ID IN (12,37) THEN '0010'                  
    ELSE '0009'                  
    END AS TRANSACTION_TYPE                    
                 
  --END AS TRANSACTION_TYPE                    
  -----------------------  FOR PREMIUM TYPE                    
    ----------- FOR NEW BUSINESS OR RENEWAL                   
    ,CASE                  
    WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0995'                  
    WHEN (VWS.CO_INSURANCE = '14549'  AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0986'                  
     WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0995'                  
    WHEN (VWS.CO_INSURANCE = '14548'  AND VWS.LEADER_FOLLOWER = '14549'AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0987'                  
    WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0991'                  
    WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0990'                  
    WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0408'                  
                        
                    
                      
    ----------- FOR REFUND                    
                        
      WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5131'                  
      WHEN (VWS.CO_INSURANCE = '14549' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5093'        
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5131'                  
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14549' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5382'                  
      WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5383'                  
      WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5384'                  
      WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5385'                  
                        
                          
                         
    ----------- FOR NEW CANCEL POLICY                     
                      
     WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5130'                  
      WHEN (VWS.CO_INSURANCE = '14549' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5092'         
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5130'                           
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14549' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5386'                  
      WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5387'                  
      WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5388'                  
      WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5389'                  
                      
                        
                      
    END AS PREMIUM_TYPE                    
   ------ SUSEP_LOB_CODE                
   ,VWS.SUSEP_LOB_CODE  AS SUSEP_LOB_CODE                    
       
   -----------ORIGINAL_EFFECTIVE_DATE                 
   ,CASE WHEN (VWS.PROCESS_ID IN(25,18)) THEN VWS.PPR_EFFECTIVE_DATETIME                   
   WHEN (VWS.PROCESS_ID IN  (14,37)) THEN VWS.PPR_1_EFFECTIVE_DATETIME                   
   WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME                   
                     
    END AS ORIGINAL_EFFECTIVE_DATE    
       
  -----ORIGINAL_EXPIRY_DATE                      
                
   ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.POLICY_EXPIRATION_DATE                  
   WHEN (VWS.PROCESS_ID  IN  (14,37)) THEN VWS.POL_VER_EXPIRATION_DATE                   
   WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME                   
                      
    END                    
    AS ORIGINAL_EXPIRY_DATE    
        
   ----ORIGINAL_ISSUE_DATE                   
   ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.COMPLETED_DATETIME                  
   WHEN (VWS.PROCESS_ID IN (37,14,12)) THEN VWS.PPR_1_COMPLETED_DATETIME          
               
                     
    END                    
    AS ORIGINAL_ISSUE_DATE     
                      
   ------ORIGINAL_PREMIUM      
    ,CASE WHEN VWS.PROCESS_ID = 25 THEN ISNULL(VWS.APIPD_TOTAL_PREMIUM ,0)          
    ELSE ISNULL(ABS(VWS.APIPD_1_TOTAL_PREMIUM),0) END AS ORIGINAL_PREMIUM          
              
  ----SECONDARY_EFFECTIVE_DATE    
   ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.PPR_EFFECTIVE_DATETIME                   
   WHEN (VWS.PROCESS_ID IN  (14,37)) THEN VWS.PPR_EFFECTIVE_DATETIME                   
   WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME                   
  ELSE VWS.POLICY_EFFECTIVE_DATE                    
                   
    END                    
    AS SECONDARY_EFFECTIVE_DATE                
             
 ------SECONDARY_EXPIRY_DATE            
  ,CASE WHEN VWS.PROCESS_ID IN(14,37) AND VWS.TOTAL_TRAN_PREMIUM < 0  --IN CASE 0009 THEN ORIGINAL EXP DAT            
  THEN             
              
   VWS.POL_VER_EXPIRATION_DATE            
  ELSE            
     CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.POLICY_EXPIRATION_DATE                    
    WHEN (VWS.PROCESS_ID = 14) THEN VWS.[EXPIRY_DATE]                  
    WHEN (VWS.PROCESS_ID = 37) THEN VWS.PPR_EFFECTIVE_DATETIME                  
    WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME             
     END            
  END  AS SECONDARY_EXPIRY_DATE    
                
 ----SECONDARY_ISSUE_DATE    
   ,CASE WHEN VWS.PROCESS_ID IN(18,25) THEN VWS.COMPLETED_DATETIME                
   WHEN VWS.PROCESS_ID IN(14,12,37) THEN VWS.COMPLETED_DATETIME --VWS.PPR_EFFECTIVE_DATETIME                   
   --CASE WHEN PPR.PROCESS_ID IN (14,18,12,25) THEN PPR.EFFECTIVE_DATETIME                    
    --WHEN PPR.PROCESS_ID IN (25) THEN PCPL.POL_VER_EXPIRATION_DATE                    
                      
                      
   END                    
   AS SECONDARY_ISSUE_DATE      
      
 -------SUSEP_OF_INSURERE                  
   ,ABS(ISNULL(VWS.APIPD_TOTAL_PREMIUM ,0)) AS SECONDARY_PREMIUM           
        
    ,CASE WHEN (ISNULL(VWS.COMPANY_ID,0) = 0) THEN ISNULL(VWS.SUSEP_NUM,'05045')              
    ELSE  ISNULL(VWS.MRCL_1_SUSEP_NUM,'05045')                           
    END                             
   AS SUSEP_OF_INSURERE     
  -----POLICY_NUMBER                   
  ,VWS.POLICY_NUMBER    
  ----    
  ,VWS.ENDORSEMENT_NO    
  -----     
  ,0 AS  UNEARNED_PREMIUM    
  ----    
  ,VWS.BROKER_COMMISSION AS COMMISSION_AMOUNT    
  ----    
  ,0 AS DEFFERED_ACQUISITION_COST     
              
  --PCPL.POLICY_NUMBER                    
  --,VWS.POLICY_ID                    
  --,VWS.POLICY_VERSION_ID                    
  --,APIPD.TRAN_TYPE                    
  --SELECT count(*)                    
  FROM                    
  #TEMP (NOLOCK) VWS      
      
               
                     
                      
  WHERE       
       
     --(MONTH(VWS.POLICY_EFFECTIVE_DATE)= MONTH(@DATETIME)    
     --AND YEAR(VWS.POLICY_EFFECTIVE_DATE) = YEAR(@DATETIME))           
     VWS.POLICY_EFFECTIVE_DATE BETWEEN @FROM_DATETIME AND @TO_DATETIME          
    AND VWS.IS_ACTIVE='Y'                    
    AND VWS.PROCESS_ID in (25,12,14,18,37)                    
    --AND VWS.SUSEP_LOB_CODE NOT  like '09%'                     
    --and VWS.SUSEP_LOB_CODE NOT IN ('1066','0588','0589')    
    AND ISNULL(VWS.APIPD_TOTAL_PREMIUM,0) <> 0                  
    AND  ISNULL(VWS.TOTAL_TRAN_PREMIUM,0) <> 0                  
                    
 ) FINAL_PPR                    
  GROUP BY FINAL_PPR.TRANSACTION_TYPE,FINAL_PPR.PREMIUM_TYPE,FINAL_PPR.SUSEP_LOB_CODE                    
    ,FINAL_PPR.ORIGINAL_EFFECTIVE_DATE,FINAL_PPR.ORIGINAL_EXPIRY_DATE,FINAL_PPR.ORIGINAL_ISSUE_DATE                    
    ,FINAL_PPR.ORIGINAL_PREMIUM,FINAL_PPR.SECONDARY_EFFECTIVE_DATE,FINAL_PPR.SECONDARY_EXPIRY_DATE,FINAL_PPR.SECONDARY_ISSUE_DATE                    
    ,FINAL_PPR.SECONDARY_PREMIUM,FINAL_PPR.SUSEP_LOB_CODE,FINAL_PPR.SUSEP_OF_INSURERE                    
    ,FINAL_PPR.POLICY_NUMBER     
    ,FINAL_PPR.ENDORSEMENT_NO    
    ,FINAL_PPR.UNEARNED_PREMIUM    
    ,FINAL_PPR.COMMISSION_AMOUNT    
    ,FINAL_PPR.DEFFERED_ACQUISITION_COST    
    --,FINAL_PPR.BROKER_COMMISSION                         
    --,FINAL_PPR.POLICY_ID,FINAL_PPR.POLICY_VERSION_ID--,FINAL_PPR.TRAN_TYPE    
  ORDER BY FINAL_PPR.SUSEP_LOB_CODE                     
                     
                    
           
 -------------------UPDATING TEMP TABLE FOR CALCULATION AS PER PF--------    
    --FORMULA    1 - (Reference Date - Effective Date)/(Expire Date - Effective Date)     
 UPDATE #TEMPTABLE     
 SET PF = CASE WHEN  DATEDIFF(DD, SECONDARY_EXPIRY_DATE, SECONDARY_EFFECTIVE_DATE) <> 0 THEN     
    1 -  CAST((DATEDIFF(DD,  SECONDARY_EFFECTIVE_DATE,@TO_DATETIME)) AS DECIMAL) / CAST((DATEDIFF(DD,  SECONDARY_EFFECTIVE_DATE,SECONDARY_EXPIRY_DATE)) AS DECIMAL)    
            
   ELSE 0    
   END    
 UPDATE #TEMPTABLE     
 SET UNEARNED_PREMIUM = ISNULL(PF,0) * ISNULL(SECONDARY_RISK_PREMIUM,0),    
  DEFFERED_ACQUISITION_COST = ISNULL(PF,0) * ISNULL(COMMISSION_AMOUNT,0)     
       
 -- WHERE POLICY_NUMBER =  '050452011200196000121'    
           
           
           
                    
       --------------------  FINAL OUTPUT                    
   SELECT                       
                      
 RIGHT('000000' +CONVERT(VARCHAR,SEQUENCE_NO),6) AS 'ESRSEQ'    
  ,'05045' AS 'ENTCODIGO'                      
  ,CONVERT(VARCHAR(8),@LAST_DAY,112) AS 'MRFMESANO'                      
  ,272 AS 'QUAID'                      
  ,TRANSACTION_TYPE AS 'TPMOID'   ---shubh            
  ,PREMIUM_TYPE AS 'CMPID'                       
  ,RIGHT('0000'+SUSEP_LINE_OF_BUSINESS,4)  as 'RAMCODIGO'                      
  ,CONVERT(VARCHAR,ORIGINAL_EFFECTIVE_DATE,112)                      
    AS 'ESPDATAINICIORO'                      
  ,CONVERT(VARCHAR,ORIGINAL_EXPIRY_DATE,112)                      
    AS 'ESPDATAFIMRO' --sh                     
  ,CONVERT(VARCHAR,ORIGINAL_ISSUE_DATE,112)                      
   AS 'ESPDATAEMISSRO'                      
  ,CASE WHEN ORIGINAL_RISK_PREMIUM>=0.00                      
   THEN                      
    dbo.fun_FormatCurrency(RIGHT('0000000000'+CONVERT(VARCHAR(25),ORIGINAL_RISK_PREMIUM),13),2)                   
    ELSE                      
     dbo.fun_FormatCurrency('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,ORIGINAL_RISK_PREMIUM),2,LEN(ORIGINAL_RISK_PREMIUM)),12),2)                      
  END AS 'ESPVALORMOVRO'    
                        
  ,CONVERT(VARCHAR,SECONDARY_EFFECTIVE_DATE,112)                      
    AS 'ESPDATAINICIORD'                      
                       
  ,CONVERT(VARCHAR,SECONDARY_EXPIRY_DATE,112)                      
    AS 'ESPDATAFIMRD'  ----shubh                    
                        
  ,CONVERT(VARCHAR,SECONDARY_ISSUE_DATE,112)                      
    AS 'ESPDATAEMISSRD'                      
                        
  ,CASE WHEN SECONDARY_RISK_PREMIUM>=0.00                      
   THEN                      
     dbo.fun_FormatCurrency(RIGHT('0000000000'+CONVERT(VARCHAR(25),SECONDARY_RISK_PREMIUM),13),2)                      
    ELSE                      
       dbo.fun_FormatCurrency('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,SECONDARY_RISK_PREMIUM),2,LEN(ORIGINAL_RISK_PREMIUM)),12),2)                      
  END AS 'ESPVALORMOVRD'                      
                        
  ,RIGHT('00000'+SUSEP_CODE_OF_INSURER,5) AS  'ESPCODCESS'                      
  ,RIGHT('0000'+NO_OF_REGISTRIES,4)  AS 'ESPFREQ'                      
  ,CAST(POLICY_NUMBER AS NVARCHAR(50))  AS 'APONUM'      
  ,ISNULL(ENDORSEMENT_NUMBER,'') AS 'ENDNUM'    
  --,ISNULL(UNEARNED_PREMIUM,0.00)  AS PPNG      
 ,CASE WHEN --(CONVERT(VARCHAR(8),@LAST_DAY,112)<CONVERT(VARCHAR(8),SECONDARY_EFFECTIVE_DATE,112))      
 ((DATEDIFF(DD,SECONDARY_EFFECTIVE_DATE,@TO_DATETIME) < 0)) 
  THEN       
   CASE WHEN SECONDARY_RISK_PREMIUM>=0.00                          
    THEN        
     dbo.fun_FormatCurrency(RIGHT('0000000000'+SECONDARY_RISK_PREMIUM,13),2)        
     --CONVERT(VARCHAR,ISNULL(SECONDARY_RISK_PREMIUM,0.00))                                    
    ELSE       
     dbo.fun_FormatCurrency('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,SECONDARY_RISK_PREMIUM),2,LEN(ORIGINAL_RISK_PREMIUM)),12),2)      
     --SUBSTRING(CONVERT(VARCHAR,ISNULL(SECONDARY_RISK_PREMIUM,0.00)),2,LEN(ORIGINAL_RISK_PREMIUM))                                            
  END      
   ELSE      
  dbo.fun_FormatCurrency(RIGHT('0000000000'+ISNULL(UNEARNED_PREMIUM,0.00),13),2)       
   END AS 'PPNG'       
  ,dbo.fun_FormatCurrency(ABS(ISNULL(COMMISSION_AMOUNT,0.00)),2) AS 'COMVALOR'    
  , CASE WHEN --(CONVERT(VARCHAR(8),@LAST_DAY,112)<CONVERT(VARCHAR(8),SECONDARY_EFFECTIVE_DATE,112))      
  ((DATEDIFF(DD,SECONDARY_EFFECTIVE_DATE,@TO_DATETIME) < 0)) 
  THEN       
   dbo.fun_FormatCurrency(ABS(ISNULL(COMMISSION_AMOUNT,0.00)),2)                                        
   ELSE      
  dbo.fun_FormatCurrency(ABS(ISNULL(DEFFERED_ACQUISITION_COST,0.00)),2)       
   END
   AS 'DCD'  
  --,PF     
  --,(DATEDIFF(DD,  SECONDARY_EFFECTIVE_DATE,@FROM_DATETIME))    
  --, (DATEDIFF(DD,  SECONDARY_EFFECTIVE_DATE,SECONDARY_EXPIRY_DATE))         
  --,POLICY_ID                    
  --,POLICY_VERSION_ID                    
  --,TRAN_TYPE       
             
                    
 FROM #TEMPTABLE (NOLOCK) --WHERE POLICY_NUMBER ='050452011200196000120'         
                   
    --WHERE ORIGINAL_EFFECTIVE_DATE IS NOT NULL                
    --AND ORIGINAL_EXPIRY_DATE IS NOT NULL                
    --AND ORIGINAL_ISSUE_DATE IS NOT NULL                
                    
DROP TABLE #TEMPTABLE         
DROP TABLE #TEMP                 
END 