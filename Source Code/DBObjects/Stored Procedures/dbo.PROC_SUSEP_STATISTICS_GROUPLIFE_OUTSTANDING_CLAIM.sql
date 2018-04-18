IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================        
-- Author:  <ATUL KUMAR SINGH>        
-- Create date: <2011-17-05>        
-- Description: < Quadro 323 - Claims statistics on pending claims to be settled of group 09 (People). >        
-- ===============================================        
        
--  *********************  MODIFICATION HISTORY   *********************        
-- MODIFIED BY : SHIKHA CHOURASIYA        
-- MODIFIED DATE : 2011-25-07      
-- DESCRIPTION :  ITrack - 1063      
--   ************************************************************        
--  *********************  EXECUTION PATH   *********************        
-- exec [PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM] '2011-08-24 17:27:56.360'        
-- exec [PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM] '2011-04-24 17:27:56.360','88998201150982000095'        
--  exec [PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM] '2011-04-24 17:27:56.360','AD000180'        
--        
--   ************************************************************        
        
-- DROP PROC [PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM]        
CREATE PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_GROUPLIFE_OUTSTANDING_CLAIM]            
 @DATETIME DATETIME,            
 @CLAIM_NUMBER NVARCHAR(21)=NULL            
AS            
BEGIN            
 SET NOCOUNT ON;            
 DECLARE @MONTH VARCHAR(2)            
 DECLARE @YEAR VARCHAR(4)            
 SET @MONTH= DATEPART(MM,@DATETIME)            
             
 DECLARE @LAST_DAY DATETIME            
 SET @LAST_DAY=(SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@DATETIME)+1,0)))            
            
 SET @YEAR=(SELECT DATEPART(YY,@DATETIME))            
             
             
  --------------------------- CREATE  TEMPORARY TABLE FOR HOLDING VALUES            
 CREATE TABLE #TEMPTABLE            
 (            
   SEQUENCE_NO INT identity(1,1)            
  ,CLAIM_TYPE VARCHAR(5)            
  ,INSURENCE_LINE_CODE VARCHAR(4)            
  ,POLICY_EFFECTIVE_DATE DATETIME            
  ,POLICY_EXPIRY_DATE DATETIME            
  ,DATE_OF_LOSS DATETIME            
  ,NOTICE_OF_LOSS_DATE DATETIME            
  ,OUTSTANDING_RESERVE_AMOUNT decimal(25,2)            
  ,RESERVE_AMOUNT_SERCHARGES decimal(25,2)            
  ,SUSEP_CODE VARCHAR(5)            
  ,NO_OF_REGISTRIES VARCHAR(5)            
  ,POLICY_NUMBER VARCHAR(21)            
  ,CLAIM_NUMBER VARCHAR(20)            
 )            
             
 INSERT INTO #TEMPTABLE            
 (             
   CLAIM_TYPE            
  ,INSURENCE_LINE_CODE            
  ,POLICY_EFFECTIVE_DATE             
  ,POLICY_EXPIRY_DATE             
  ,DATE_OF_LOSS             
  ,NOTICE_OF_LOSS_DATE             
  ,OUTSTANDING_RESERVE_AMOUNT             
  ,RESERVE_AMOUNT_SERCHARGES            
  ,SUSEP_CODE            
  ,NO_OF_REGISTRIES             
  ,CLAIM_NUMBER            
 )
 SELECT  CLAIM_TYPE            
   ,INSURENCE_LINE_CODE            
   ,EFFECTIVE_DATE            
   ,[EXPIRY_DATE]            
   ,[DATE_OF_LOSS]            
   ,[RECORD_DATE]            
   ,TBL1.OUTSTANDING_RESERVE_AMOUNT             
   ,TBL1.RESERVE_AMOUNT_SERCHARGES         
   ,SUSEP_CODE            
   ,COUNT(1)AS NUMBER_OF_REGIS            
   ,CLAIM_NUMBER 
 FROM            
 (            
 SELECT             
    CLAIM_TYPE            
   ,INSURENCE_LINE_CODE            
   ,EFFECTIVE_DATE            
   ,[EXPIRY_DATE]            
   ,[DATE_OF_LOSS]            
   ,[RECORD_DATE]  
   ,SUM(TBL.OUTSTANDING_RESERVE_AMOUNT) AS OUTSTANDING_RESERVE_AMOUNT            
   ,SUM(TBL.RESERVE_AMOUNT_SERCHARGES) AS RESERVE_AMOUNT_SERCHARGES           
   ,SUSEP_CODE            
   ,COUNT(1)AS NUMBER_OF_REGIS            
   ,CLAIM_NUMBER            
    FROM            
   (            
    SELECT            
       CASE   -- 5946 Direct - Administrative Indemnification                
  WHEN ((VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='Y'))                
  THEN '5946'                
              --5971-Direct - Juridical  Expenses                    
  WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y')                
  THEN '5971'                
              --5963-Direct - Juridical Indemnification                
  WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y')                
  THEN '5963' --LOSSEXP                
 --5954-Direct - Administrative  Expenses                
  WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='N')                
  THEN '5954'                
   -- INWARD CO INSURENCE                
              --5947-Inward Coinsurance - Administrative Indemnification                
  WHEN ((VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='Y'))                
  THEN '5947'                
              --5954-Inward Coinsurance - Administrative  Expenses                
  WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE ='N')                
  THEN '5954'                
              -- 5963-Inward Coinsurance - Juridical Indemnification                
  WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y')                
  THEN '5963'                
              --5971-Inward Coinsurance - Juridical Expenses                    
  WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y')                
  THEN '5971'                
                       
  -- OUTWARD CO INSURENCE                  
              --5948-Cession Coinsurance - Administrative IndemnifiVWStion                
  WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE='Y')                
  THEN '5948'                
             --5956-Cession Coinsurance - Administrative  Expenses                
  WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'N')                
  THEN '5956'                
              -- 5965-Cession Coinsurance - JuridiVWSl IndemnifiVWStion                
  WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'Y' AND VWS.IS_LEGAL='Y')                
  THEN '5965'                
              --5973-Cession Coinsurance - JuridiVWSl Expenses                 
  WHEN (VWS.CO_INSURANCE_TYPE='14549'  AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y')                
  THEN '5973'                
                         
  -- REINSURENCE                
  --LoVWSl Reinsurer                
              --5949-Cession LoVWSl Reinsurer  - Administrative Indemnification                 
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,14694)=14694 OR VWS.COM_TYPE='') AND VWS.IS_RISK_COVERAGE='Y' )                
  THEN '5949'                
                    
              --5957-Cession LoVWSl Reinsurer - Administrative  Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,14694)=14694 OR VWS.COM_TYPE='') AND VWS.IS_RISK_COVERAGE='N')                
  THEN '5957'                
              --5966-Cession LoVWSl Reinsurer  - Juridical Indemnification                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,14694)=14694 OR VWS.COM_TYPE='') AND  VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y')                
  THEN '5966'                
                    
              --5974-Cession LoVWSl Reinsurer - Juridical Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,14694)=14694 OR VWS.COM_TYPE='') AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y')                
  THEN '5974'                
                    
  --Admitted Reinsurer                  
              --5950-Cession Admitted Reinsurer - Administrative IndemnifiVWStion                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,14694)=14553 OR VWS.COM_TYPE='') AND VWS.IS_RISK_COVERAGE='Y')                
  THEN '5950'                
                    
              --5958-Cession Admitted Reinsurer - Administrative  Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14553 AND VWS.IS_RISK_COVERAGE = 'N')                
  THEN '5958'                
              --5967-Cession Admitted Reinsurer - JuridiVWSl IndemnifiVWStion                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14553 AND VWS.IS_RISK_COVERAGE = 'Y' AND VWS.IS_LEGAL='Y')                
  THEN '5967'                
                    
              --5975-Cession Admitted Reinsurer - JuridiVWSl Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14553 AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y')                
  THEN '5975'                
                    
  --Eventual Reinsurer                  
              --5951-Cession Eventual Reinsurer - Administrative IndemnifiVWStion                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14552 AND VWS.IS_RISK_COVERAGE='Y' )                
  THEN '5951'                
             
              --5959-Cession Eventual Reinsurer - Administrative  Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14552 AND VWS.IS_RISK_COVERAGE='N')                
  THEN '5959'                
              --5968-Cession Eventual  Reinsurer - JuridiVWSl IndemnifiVWStion                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14552 AND  VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y')                
  THEN '5968'                
                    
              --5976-Cession Eventual Reinsurer - JuridiVWSl Expenses                
  WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,14694)=14552 AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y')                
  THEN '5976'             
    END  AS CLAIM_TYPE              
   ,VWS.SUSEP_LOB_CODE AS INSURENCE_LINE_CODE              
   ,VWS.EFFECTIVE_DATETIME AS EFFECTIVE_DATE              
   ,VWS.POLICY_EXPIRATION_DATE AS [EXPIRY_DATE]              
   ,VWS.LOSS_DATE AS [DATE_OF_LOSS]              
   ,VWS.CREATED_DATETIME AS [RECORD_DATE]          
   ,VWS.RESERVE_AMOUNT AS OUTSTANDING_RESERVE_AMOUNT             
   --,TEMP1.RESERVE_AMOUNT AS OUTSTANDING_RESERVE_AMOUNT              
   ,(VWS.RESERVE_AMOUNT* MCRM.RATE * .01) AS RESERVE_AMOUNT_SERCHARGES         
   ,CASE WHEN (VWS.CO_INSURANCE_TYPE = 14547) THEN '05045'            
 ELSE ISNULL(VWS.MRCL_SUSEP_NUM ,'05045')            
 END                
    AS SUSEP_CODE             
   ,VWS.CLAIM_NUMBER          
   FROM VW_SUSEP_REPORT_270_271_322_323 VWS (NOLOCK)          
  --  LEFT OUTER JOIN          
  --  (              
  --SELECT SUM(RESERVE_AMOUNT) as RESERVE_AMOUNT ,A.CLAIM_ID from CLM_ACTIVITY  A               
  --JOIN CLM_CLAIM_INFO B ON A.CLAIM_ID=B.CLAIM_ID              
  --WHERE B.CLAIM_STATUS=11739              
  --GROUP BY  A.CLAIM_ID              
  -- ) TEMP1 ON VWS.CCI_CLAIM_ID=TEMP1.CLAIM_ID          
        
 LEFT OUTER JOIN             
 (            
 SELECT RATE,CURRENCY_ID FROM MNT_CURRENCY_RATE_MASTER (NOLOCK)             
 WHERE RATE_EFFETIVE_TO IN (            
 SELECT MAX(RATE_EFFETIVE_TO) FROm MNT_CURRENCY_RATE_MASTER (NOLOCK)             
 GROUP BY CURRENCY_ID )            
 ) MCRM            
 ON MCRM.CURRENCY_ID=VWS.POLICY_CURRENCY               
                
                
    WHERE  VWS.CA_IS_ACTIVE='Y'         
    AND VWS.CLAIM_STATUS='11739'              
    AND (@CLAIM_NUMBER IS NULL OR VWS.CLAIM_NUMBER=@CLAIM_NUMBER)              
    AND  CONVERT(varchar(6),VWS.CREATED_DATETIME,112)=CONVERT(varchar(6),@DATETIME,112)  AND    
    VWS.OFFCIAL_CLAIM_NUMBER IS NOT NULL AND     
    (VWS.LIMIT_1<>0.00 ) AND VWS.SUSEP_LOB_CODE LIKE '09%'         
          
 ) TBL            
 GROUP BY             
    CLAIM_TYPE            
   ,INSURENCE_LINE_CODE            
   ,EFFECTIVE_DATE            
   ,[EXPIRY_DATE]            
   ,[DATE_OF_LOSS]            
   ,[RECORD_DATE]            
   ,SUSEP_CODE            
   ,CLAIM_NUMBER  
   ) TBL1   
   GROUP BY   CLAIM_TYPE            
   ,INSURENCE_LINE_CODE            
   ,EFFECTIVE_DATE            
   ,[EXPIRY_DATE]            
   ,[DATE_OF_LOSS]            
   ,[RECORD_DATE] 
   ,OUTSTANDING_RESERVE_AMOUNT            
   ,RESERVE_AMOUNT_SERCHARGES                      
   ,SUSEP_CODE            
   ,CLAIM_NUMBER      
               
               
 SELECT RIGHT('000000' +CONVERT(VARCHAR,SEQUENCE_NO),6) AS [ESRSEQ]            
  ,'05045' AS [ENTCODIGO]            
  ,CONVERT(VARCHAR(8),@LAST_DAY,112) AS [MRFMESANO]            
  ,323 AS [QUAID]            
              
  ,CLAIM_TYPE AS [CMPID]             
 ,INSURENCE_LINE_CODE AS [RAMCODIGO]            
  ,CONVERT(VARCHAR,POLICY_EFFECTIVE_DATE,112) AS [ESRDATAINICIO]            
,CONVERT(VARCHAR,POLICY_EXPIRY_DATE,112) AS [ESRDATAFIM]            
  ,CONVERT(VARCHAR,DATE_OF_LOSS,112) AS [ESRDATAOCORR]            
              
  ,CONVERT(VARCHAR,NOTICE_OF_LOSS_DATE,112) AS [ESRDATAVISO]  
    
              
              
  ,CASE WHEN OUTSTANDING_RESERVE_AMOUNT >=0.00            
   THEN            
     REPLACE(RIGHT('0000000000'+CONVERT(VARCHAR(16),OUTSTANDING_RESERVE_AMOUNT),13),'.',',')            
    ELSE             
     REPLACE('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,OUTSTANDING_RESERVE_AMOUNT),2,LEN(OUTSTANDING_RESERVE_AMOUNT)),12),'.',',')            
  END AS [ESRVALORMOV]            
  ,             
  CASE WHEN RESERVE_AMOUNT_SERCHARGES >=0.00            
   THEN            
     ISNULL(REPLACE(RIGHT('0000000000'+CONVERT(VARCHAR(16),RESERVE_AMOUNT_SERCHARGES),13),'.',','),'0000000000,00')            
    ELSE             
     ISNULL(REPLACE('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,RESERVE_AMOUNT_SERCHARGES),2,LEN(RESERVE_AMOUNT_SERCHARGES)),12),'.',',') ,'0000000000,00')           
                 
 END AS [ESLVARORMON]            
  ,          
  RIGHT('00000'+SUSEP_CODE,5) AS [ESLCODCESS]            
  ,RIGHT('0000'+NO_OF_REGISTRIES,4)AS [ESRFREQ]            
FROM #TEMPTABLE (NOLOCK)          
        
DROP TABLE #TEMPTABLE     
END
GO

