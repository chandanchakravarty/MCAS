IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_STATISTICS_OUTSTANDING_CLAIM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_OUTSTANDING_CLAIM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================            
-- Author:  <ATUL KUMAR SINGH>            
-- Create date: <2011-17-05>            
-- Description: < Quadro 321 - Claims statistics on pending claims to be settled. >            
-- ===============================================            
            
--  *********************  MODIFIVWSTION HISTORY   *********************            
-- MODIFIED BY : <SHIKHA CHOURASIYA>            
-- MODIFIED DATE : <2011-25-07>           
-- DESCRIPTION : ITrack - 1060           
--   ************************************************************            
--  *********************  EXECUTION PATH   *********************            
-- exec [PROC_SUSEP_STATISTICS_OUTSTANDING_CLAIM] '09/30/2011','SH000007'                  
--   ************************************************************            
            
 --DROP PROC [PROC_SUSEP_STATISTICS_OUTSTANDING_CLAIM]              
CREATE PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_OUTSTANDING_CLAIM]                  
 @DATETIME DATETIME ,                  
 @CLAIM_NUMBER NVARCHAR(21)=NULL                  
AS                  
BEGIN                  
 SET NOCOUNT ON;                  
 DECLARE @MONTH VARCHAR(2)                  
 DECLARE @YEAR VARCHAR(4)                  
 SET @MONTH= DATEPART(MM,@DATETIME)                  
                   
 DECLARE @LAST_DAY DATETIME                  
 SET @LAST_DAY=(SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@DATETIME)+1,0)))                  
                  
 SET @YEAR=(SELECT DATEPART(YY,@DATETIME)) ;                 
                   
 /*                  
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
 )    */
 WITH TEMPTABLE
 AS
 (              
   SELECT  CLAIM_TYPE                
   ,INSURENCE_LINE_CODE                
   ,POLICY_EFFECTIVE_DATE                
   ,POLICY_EXPIRY_DATE                
   ,[DATE_OF_LOSS]                
   ,NOTICE_OF_LOSS_DATE                
   ,TBL1.OUTSTANDING_RESERVE_AMOUNT                 
   ,TBL1.RESERVE_AMOUNT_SERCHARGES             
   ,SUSEP_CODE                
   ,COUNT(1)AS NO_OF_REGISTRIES                
   ,CLAIM_NUMBER     
 FROM                
 (                     
 SELECT         
 PAYMENT_TYPE,          
   CLAIM_TYPE                  
   ,INSURENCE_LINE_CODE                  
   ,POLICY_EFFECTIVE_DATE                  
   ,POLICY_EXPIRY_DATE                  
   ,[DATE_OF_LOSS]                  
   ,[NOTICE_OF_LOSS_DATE] 
   ,TBL.OUTSTANDING_RESERVE_AMOUNT                 
   --,SUM(TBL.OUTSTANDING_RESERVE_AMOUNT) AS OUTSTANDING_RESERVE_AMOUNT                
   ,SUM(TBL.RESERVE_AMOUNT_SERCHARGES) AS RESERVE_AMOUNT_SERCHARGES               
   ,SUSEP_CODE                  
   ,COUNT(1)AS NUMBER_OF_REGIS                  
   ,TBL.CLAIM_NUMBER                  
    FROM                  
   (              
    SELECT     COV_CODE, 
       CASE WHEN COV_CODE IN (50017,50018,50019,50020,50021,50022) THEN 'Expense' ELSE 'Payment' END AS PAYMENT_TYPE,
  CASE   -- 5946 Direct - Administrative Indemnification                  
     
     WHEN   (VWS.CO_INSURANCE_TYPE in(14547,14548) AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618 and VWS.SOURCE_PARTY_ID=1)
     THEN '5946'
     
     
     WHEN   (VWS.CO_INSURANCE_TYPE =14548 AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618 and VWS.SOURCE_PARTY_TYPE_ID=14549)
     THEN '5948'
    
     
     WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL AND VWS.PARTY_TYPE_ID = 619 AND VWS.IS_RISK_COVERAGE='Y' )                  
     THEN '5949'                  
	 
	 WHEN   (VWS.CO_INSURANCE_TYPE in(14547,14548) AND VWS.IS_RISK_COVERAGE='N' AND VWS.PARTY_TYPE_ID = 618 and VWS.SOURCE_PARTY_ID=1)
     THEN '5954'
     
     WHEN   (VWS.CO_INSURANCE_TYPE =14548 AND VWS.IS_RISK_COVERAGE='N' AND VWS.PARTY_TYPE_ID = 618 and VWS.SOURCE_PARTY_TYPE_ID=14549)
     THEN '5956' 
     
     WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL AND VWS.PARTY_TYPE_ID = 619 AND VWS.IS_RISK_COVERAGE='N' )  -- AND (ISNULL(VWS.COM_TYPE,'14694')='14694' OR VWS.COM_TYPE='') CHANGES BY SHIKHA                
     THEN '5957' 
     
     WHEN   (VWS.CO_INSURANCE_TYPE =14549 AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618)
     THEN '5947'
         
	 WHEN   (VWS.CO_INSURANCE_TYPE =14549 AND VWS.IS_RISK_COVERAGE='N' AND VWS.PARTY_TYPE_ID = 618)
     THEN '5955'    
     END AS CLAIM_TYPE
 --      CASE   -- 5946 Direct - Administrative Indemnification                  
 -- WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618)                  
 -- THEN '5946'                  
 --             --5971-Direct - Juridical  Expenses                      
 -- WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618)                  
 -- THEN '5971'                  
 --             --5963-Direct - Juridical Indemnification               
 -- WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618)                  
 -- THEN '5963' --LOSSEXP                  
 ----5954-Direct - Administrative  Expenses                  
 -- WHEN (VWS.CO_INSURANCE_TYPE='14547' AND VWS.IS_RISK_COVERAGE='N' AND VWS.PARTY_TYPE_ID = 618)                  
 -- THEN '5954' 
  
 --  -- CESSION CO INSURENCE                  
 --             --5948-CESSION Coinsurance - Administrative Indemnification                  
 -- WHEN ((VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549'))                  
 -- THEN '5947'   
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14548' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548' ) --AND VWS.ACTION_ON_PAYMENT in (165,166,180,190,181,240,192,167,168))                  
 -- THEN '5948'                 
 --             --5956-CESSION Coinsurance - Administrative  Expenses                  
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE ='N' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5955'  
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE ='N' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5956'                  
 --             -- 5965-CESSION Coinsurance - Juridical Indemnification                  
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5964'  
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5965'                  
 --             --5973-CESSION Coinsurance - Juridical Expenses                      
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5972'   
 -- WHEN (VWS.CO_INSURANCE_TYPE = '14548' AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5973'                 
                         
 -- -- INWARD CO INSURENCE                    
 --             --5947-Inward Coinsurance - Administrative Indemnification                  
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618  AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                   
 -- THEN '5948'  
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')  
 -- THEN '5947'                  
 --            --5955-Inward Coinsurance - Administrative  Expenses                  
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5956'   
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5955'                 
 --             -- 5964-Inward Coinsurance - JuridiVWSl Indemnification             
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5965'  
 -- WHEN (VWS.CO_INSURANCE_TYPE ='14549' AND VWS.IS_RISK_COVERAGE = 'Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5964'                  
 --             --5972-Inward Coinsurance - JuridiVWSl Expenses                   
 -- WHEN (VWS.CO_INSURANCE_TYPE='14549'  AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14548')                  
 -- THEN '5973'  
 -- WHEN (VWS.CO_INSURANCE_TYPE='14549'  AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_TYPE_ID = '14549')                  
 -- THEN '5972'                 
      
 -- -- REINSURENCE                  
 -- --LoVWSl Reinsurer                  
 --             --5949-Cession Local Reinsurer  - Administrative Indemnification                   
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,'14694')='14694' OR VWS.COM_TYPE='' AND VWS.PARTY_TYPE_ID = 619) AND VWS.IS_RISK_COVERAGE='Y' )                  
 -- THEN '5949'                  
                      
 --             --5957-Cession Local Reinsurer - Administrative  Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,'14694')='14694' OR VWS.COM_TYPE='' AND VWS.PARTY_TYPE_ID = 619) AND VWS.IS_RISK_COVERAGE='N')                  
 -- THEN '5957'                  
 --             --5966-Cession Local Reinsurer  - Juridical Indemnification                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,'14694')='14694' OR VWS.COM_TYPE='' AND VWS.PARTY_TYPE_ID = 619) AND  VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y')                  
 -- THEN '5966'                  
                      
 --             --5974-Cession Local Reinsurer - Juridical Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,'14694')='14694' OR VWS.COM_TYPE='' AND VWS.PARTY_TYPE_ID = 619) AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y')                  
 -- THEN '5974'                  
                      
 -- --Admitted Reinsurer                    
 --             --5950-Cession Admitted Reinsurer - Administrative IndemnifiVWStion                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND (ISNULL(VWS.COM_TYPE,'14694')='14553' OR VWS.COM_TYPE='' AND VWS.PARTY_TYPE_ID = 619) AND VWS.IS_RISK_COVERAGE='Y')                  
 -- THEN '5950'                  
                      
 --             --5958-Cession Admitted Reinsurer - Administrative  Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14553' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5958'                  
 --             --5967-Cession Admitted Reinsurer - JuridiVWSl IndemnifiVWStion           
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14553' AND VWS.IS_RISK_COVERAGE = 'Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5967'                  
                      
 --             --5975-Cession Admitted Reinsurer - JuridiVWSl Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14553' AND VWS.IS_RISK_COVERAGE = 'N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5975'                  
                      
 -- --Eventual Reinsurer                    
 --             --5951-Cession Eventual Reinsurer - Administrative IndemnifiVWStion                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14552' AND VWS.IS_RISK_COVERAGE='Y' AND VWS.PARTY_TYPE_ID = 619 )                  
 -- THEN '5951'                  
               
 --             --5959-Cession Eventual Reinsurer - Administrative  Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14552' AND VWS.IS_RISK_COVERAGE='N' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5959'                  
 --             --5968-Cession Eventual  Reinsurer - JuridiVWSl IndemnifiVWStion                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14552' AND  VWS.IS_RISK_COVERAGE='Y' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5968'                  
                      
 --             --5976-Cession Eventual Reinsurer - JuridiVWSl Expenses                  
 -- WHEN (VWS.SOURCE_PARTY_ID IS NOT NULL  AND ISNULL(VWS.COM_TYPE,'14694')='14552' AND VWS.IS_RISK_COVERAGE='N' AND VWS.IS_LEGAL='Y' AND VWS.PARTY_TYPE_ID = 619)                  
 -- THEN '5976'                  
 --   END AS CLAIM_TYPE                  
   ,case when VWS.SUSEP_LOB_CODE ='0520' then '0982' else VWS.SUSEP_LOB_CODE end AS INSURENCE_LINE_CODE                
   ,VWS.EFFECTIVE_DATETIME AS POLICY_EFFECTIVE_DATE                  
   ,VWS.POLICY_EXPIRATION_DATE AS [POLICY_EXPIRY_DATE]                  
   ,VWS.LOSS_DATE AS [DATE_OF_LOSS]                  
   ,VWS.FIRST_NOTICE_OF_LOSS AS [NOTICE_OF_LOSS_DATE]         
   --Commented by pradeep    
   --,CASE WHEN COV_CODE IN (50017,50018,50019,50020,50021,50022) THEN 
			--CASE 
			--	 WHEN  VWS.PARTY_TYPE_ID = 619 THEN RI_EXPENSES_AMT
			--	 ELSE
			--	 EXPENSES_AMT - RI_EXPENSES_AMT
			-- END 
		 --ELSE
		 --	CASE 
			--	 WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID = 1 THEN TEMP.RESERVE_AMOUNT - (TEMP.CO_RESERVER_AMT + TEMP.RI_RESERVE_AMT + EXPENSES_AMT )
			--	 WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID != 1  THEN TEMP.CO_RESERVER_AMT
			--	 WHEN  VWS.PARTY_TYPE_ID = 619 THEN TEMP.RI_RESERVE_AMT 
			-- END 
		 --END  AS OUTSTANDING_RESERVE_AMOUNT
   
   ,CASE 
		WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID = 1 AND VWS.IS_RISK_COVERAGE = 'N'  THEN  TEMP1.RESERVE_AMOUNT - (TEMP1.CO_RESERVER_AMT + TEMP1.RI_RESERVE_AMT)
		WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID != 1 AND VWS.IS_RISK_COVERAGE = 'N' THEN TEMP1.CO_RESERVER_AMT
		WHEN  VWS.PARTY_TYPE_ID = 619 AND VWS.IS_RISK_COVERAGE = 'N' THEN TEMP1.RI_RESERVE_AMT 
	    WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID = 1 THEN  TEMP.RESERVE_AMOUNT - (TEMP.CO_RESERVER_AMT + TEMP.RI_RESERVE_AMT)
		WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID != 1 THEN TEMP.CO_RESERVER_AMT
		WHEN  VWS.PARTY_TYPE_ID = 619 THEN TEMP.RI_RESERVE_AMT 
	END AS OUTSTANDING_RESERVE_AMOUNT
	,0 AS RESERVE_AMOUNT_SERCHARGES
  -- ,CASE
		--WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID = 1 AND VWS.IS_RISK_COVERAGE = 'N'  THEN  ((TEMP1.RESERVE_AMOUNT - (TEMP1.CO_RESERVER_AMT + TEMP1.RI_RESERVE_AMT)) *  MCRM.RATE * .01)
		--WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID != 1 AND VWS.IS_RISK_COVERAGE = 'N' THEN (TEMP1.CO_RESERVER_AMT * MCRM.RATE * .01)
		--WHEN  VWS.PARTY_TYPE_ID = 619 AND VWS.IS_RISK_COVERAGE = 'N' THEN (TEMP1.RI_RESERVE_AMT * MCRM.RATE * .01) 
		--WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID = 1 THEN ((TEMP.RESERVE_AMOUNT - (TEMP.CO_RESERVER_AMT + TEMP.RI_RESERVE_AMT)) *  MCRM.RATE * .01)
  --      WHEN  VWS.PARTY_TYPE_ID = 618 AND VWS.SOURCE_PARTY_ID != 1  THEN (TEMP.CO_RESERVER_AMT * MCRM.RATE * .01)
  --      WHEN  VWS.PARTY_TYPE_ID = 619 THEN (TEMP.RI_RESERVE_AMT * MCRM.RATE * .01)
  --  END AS RESERVE_AMOUNT_SERCHARGES            
   ,CASE WHEN (VWS.CO_INSURANCE_TYPE = 14547 and VWS.PARTY_TYPE_ID<>619) THEN '05045'
    WHEN (VWS.CO_INSURANCE_TYPE = 14549 AND VWS.PARTY_TYPE_ID<>619) THEN (SELECT LEADER_SUSEP_CODE FROM CLM_CO_INSURANCE CCII WHERE CCII.CLAIM_ID = VWS.CCI_CLAIM_ID)            
   ELSE ISNULL(VWS.MRCL_SUSEP_NUM ,'05045')              
   END                  
    AS SUSEP_CODE                 
   ,VWS.CLAIM_NUMBER                  
  
  
 FROM VW_SUSEP_REPORT_270_271_322_323 VWS (NOLOCK)                
    LEFT OUTER JOIN                  
     (  
		
		SELECT SUM(OUTSTANDING_TRAN) AS RESERVE_AMOUNT,SUM(A.RI_RESERVE_TRAN) AS RI_RESERVE_AMT,SUM(CO_RESERVE_TRAN) AS CO_RESERVER_AMT, A.CLAIM_ID 
		FROM CLM_ACTIVITY (NOLOCK) A1
		JOIN CLM_ACTIVITY_RESERVE (NOLOCK) A ON A1.CLAIM_ID = A.CLAIM_ID AND A1.ACTIVITY_ID = A.ACTIVITY_ID 
		JOIN CLM_PRODUCT_COVERAGES (NOLOCK) B ON A.CLAIM_ID = B.CLAIM_ID AND A.COVERAGE_ID = B.CLAIM_COV_ID 
		WHERE  IS_RISK_COVERAGE = 'Y' AND A1.ACTIVITY_STATUS = '11801'
		GROUP BY  A.CLAIM_ID 
	 ) TEMP ON VWS.CCI_CLAIM_ID = TEMP.CLAIM_ID    
  LEFT OUTER JOIN                  
     (  
		SELECT SUM(OUTSTANDING_TRAN) AS RESERVE_AMOUNT,SUM(A.RI_RESERVE_TRAN) AS RI_RESERVE_AMT,SUM(CO_RESERVE_TRAN) AS CO_RESERVER_AMT, A.CLAIM_ID 
		FROM CLM_ACTIVITY (NOLOCK) A1
		JOIN CLM_ACTIVITY_RESERVE (NOLOCK) A ON A1.CLAIM_ID = A.CLAIM_ID AND A1.ACTIVITY_ID = A.ACTIVITY_ID 
		JOIN CLM_PRODUCT_COVERAGES (NOLOCK) B ON A.CLAIM_ID = B.CLAIM_ID AND A.COVERAGE_ID = B.CLAIM_COV_ID 
		WHERE  IS_RISK_COVERAGE = 'N' AND A1.ACTIVITY_STATUS = '11801' AND COVERAGE_CODE_ID IN (50017,50018,50020,50022)
		GROUP BY  A.CLAIM_ID 
      ) TEMP1 ON VWS.CCI_CLAIM_ID = TEMP1.CLAIM_ID                 
 LEFT OUTER JOIN                   
 (                  
 SELECT RATE,CURRENCY_ID FROM MNT_CURRENCY_RATE_MASTER (NOLOCK)                   
 WHERE RATE_EFFETIVE_TO IN (                  
 SELECT MAX(RATE_EFFETIVE_TO) FROm MNT_CURRENCY_RATE_MASTER (NOLOCK)                   
 GROUP BY CURRENCY_ID                  
 )                  
 ) MCRM ON MCRM.CURRENCY_ID=VWS.POLICY_CURRENCY                      
    WHERE             
  VWS.CA_IS_ACTIVE='Y' AND             
  VWS.CLAIM_STATUS='11739' AND             
  (@CLAIM_NUMBER IS NULL OR VWS.CLAIM_NUMBER=@CLAIM_NUMBER) AND        
  CONVERT(varchar(6),VWS.CREATED_DATETIME,112)=CONVERT(varchar(6),@datetime,112)  AND      
   VWS.OFFCIAL_CLAIM_NUMBER IS NOT NULL AND        
  VWS.SUSEP_LOB_CODE NOT  LIKE '09%'  AND             
  VWS.SUSEP_LOB_CODE NOT IN ('1066','0588','0589')
  AND CASE WHEN CO_INSURANCE_TYPE='14549' THEN ISNULL(VWS.SOURCE_PARTY_TYPE_ID,'0')   
		   ELSE 1  
	  END  
			<>   
	  CASE WHEN CO_INSURANCE_TYPE='14549' THEN '14548'   
		   ELSE 2  
	  END                
) TBL 
 
 GROUP BY                   
 PAYMENT_TYPE,                    
   CLAIM_TYPE                  
   ,INSURENCE_LINE_CODE                  
   ,POLICY_EFFECTIVE_DATE                  
   ,POLICY_EXPIRY_DATE                  
   ,[DATE_OF_LOSS] 
   ,OUTSTANDING_RESERVE_AMOUNT                 
   ,[NOTICE_OF_LOSS_DATE]                                  
   ,SUSEP_CODE                  
   ,TBL.CLAIM_NUMBER                  
   ) TBL1       
   GROUP BY   CLAIM_TYPE                
   ,INSURENCE_LINE_CODE                
   ,POLICY_EFFECTIVE_DATE                
   ,POLICY_EXPIRY_DATE                
   ,[DATE_OF_LOSS]                
   ,[NOTICE_OF_LOSS_DATE]     
   ,OUTSTANDING_RESERVE_AMOUNT                
   ,RESERVE_AMOUNT_SERCHARGES                          
   ,SUSEP_CODE                
   ,CLAIM_NUMBER 
   ),
   
   FINALTABLE AS
   (                   
                  
 SELECT --RIGHT('000000' +CONVERT(VARCHAR,SEQUENCE_NO),6) AS [ESLSEQ]  
 RIGHT('000000' +CONVERT(VARCHAR,ROW_NUMBER() OVER( ORDER BY  CLAIM_TYPE)),6) AS [ESLSEQ]                  
  ,'05045' AS [ENTCODIGO]                  
  ,CONVERT(VARCHAR(8),@LAST_DAY,112) AS [MRFMESANO]                  
  ,271 AS [QUAID]                  
  ,CLAIM_TYPE AS [CMPID]              
  ,INSURENCE_LINE_CODE  AS [RAMCODIGO]                  
  ,CONVERT(VARCHAR,POLICY_EFFECTIVE_DATE,112) AS [ESLDATAINICIO]                  
  ,CONVERT(VARCHAR,POLICY_EXPIRY_DATE,112) AS [ESLDATAFIM]                  
  ,CONVERT(VARCHAR,DATE_OF_LOSS,112) AS [ESLDATAOCORR]                  
  ,CONVERT(VARCHAR,NOTICE_OF_LOSS_DATE,112) AS [ESLDATAVISO]                  
  ,CASE WHEN OUTSTANDING_RESERVE_AMOUNT >=0.00              
   THEN                  
     RIGHT('0000000000'+REPLACE(CONVERT(VARCHAR(16),OUTSTANDING_RESERVE_AMOUNT),'.',','),13)                  
    ELSE                   
     RIGHT('0000000000' +ISNULL(REPLACE(SUBSTRING(CONVERT(VARCHAR,OUTSTANDING_RESERVE_AMOUNT),2,LEN(OUTSTANDING_RESERVE_AMOUNT)),'.',','),''),13)                  
  END AS [ESLVALORMOV]                  
  ,CASE WHEN RESERVE_AMOUNT_SERCHARGES >=0.00                  
   THEN                  
     REPLACE(CAST(RIGHT('0000000000'+ISNULL(REPLACE(CAST(RESERVE_AMOUNT_SERCHARGES AS DECIMAL(10,2)),'.',','),''),13) AS VARCHAR(50)),'0000000000,00','')           
	 --'0000000000'+CAST(RESERVE_AMOUNT_SERCHARGES AS DECIMAL(11,2)),13)
    ELSE                   
     REPLACE(CAST(RIGHT('0000000000' +ISNULL(REPLACE(SUBSTRING(CONVERT(VARCHAR,RESERVE_AMOUNT_SERCHARGES),2,LEN(RESERVE_AMOUNT_SERCHARGES)),'.',','),''),13) AS VARCHAR(50)),'0000000000,00','')                  
   END AS [ESLVALORMON]                  
  ,RIGHT('00000'+SUSEP_CODE,5) AS [ESLCODCESS]                  
  ,RIGHT('0000'+NO_OF_REGISTRIES,4)AS [ESLFREQ]                  
  FROM TEMPTABLE --(NOLOCK)  
  WHERE OUTSTANDING_RESERVE_AMOUNT <> 0.00
  )                
  SELECT * FROM FINALTABLE            
  --DROP TABLE #TEMPTABLE         
END     
GO

