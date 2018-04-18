IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_STATISTICS_RISK_INSURANCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_RISK_INSURANCE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================  
-- Author:  <ATUL KUMAR SINGH>  
-- Create date: <2011-03-30>  
-- Description: < Quadr-324 pStatistic of Group Life - Premium report to be specified by Ebix LatAm>  
-- ===============================================  
  
--  *********************  MODIFICATION HISTORY   *********************  
-- MODIFIED BY :  
-- MODIFIED DATE :  
-- DESCRIPTION :  
--   ************************************************************  
--  *********************  EXECUTION PATH   *********************  
-- exec [PROC_SUSEP_STATISTICS_RISK_INSURANCE] '2011-05-24 17:27:56.360'  
-- exec [PROC_SUSEP_STATISTICS_RISK_INSURANCE] '2011-04-24 17:27:56.360','050452011040982000016'  
--  
--   ************************************************************  
  
-- DROP PROC [PROC_SUSEP_STATISTICS_RISK_INSURANCE]  
-- =============================================  
CREATE PROCEDURE [dbo].[PROC_SUSEP_STATISTICS_RISK_INSURANCE]  
 @FROM_DATETIME  DATETIME,
 @TO_DATETIME  DATETIME
 --@POLICY_NUMBER VARCHAR(21)=NULL  
AS  
BEGIN    
 SET NOCOUNT ON; 
 
 SELECT * INTO #TEMP FROM [VW_SUSEP_FORM_272_324]    
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
 DECLARE @MONTH VARCHAR(2)    
 DECLARE @YEAR VARCHAR(4)    
 SET @MONTH= DATEPART(MM,@FROM_DATETIME)    
     
 DECLARE @LAST_DAY DATETIME    
 SET @LAST_DAY=(SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@FROM_DATETIME)+1,0)))    
    
 SET @YEAR=(SELECT DATEPART(YY,@FROM_DATETIME))    
    
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
  ,ORIGINAL_RISK_PREMIUM DECIMAL(29,2)    
  ,SECONDARY_EFFECTIVE_DATE DATETIME    
  ,SECONDARY_EXPIRY_DATE DATETIME    
  ,SECONDARY_ISSUE_DATE DATETIME    
  ,SECONDARY_RISK_PREMIUM DECIMAL(29,2)    
  ,SUSEP_CODE_OF_INSURER VARCHAR(5)    
  ,NO_OF_REGISTRIES VARCHAR(4)    
  ,POLICY_NUMBER VARCHAR(21)    
  ,POLICY_ID INT    
  ,POLICY_VERSION_ID INT    
  ,TRAN_TYPE VARCHAR(5)    
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
  
     
  --,FINAL_PPR.POLICY_ID    
  --,FINAL_PPR.POLICY_VERSION_ID    
  --,FINAL_PPR.TRAN_TYPE    
 FROM     
 (    
  SELECT    
    -----------------------  FOR TRANSACTION TYPE    
     CASE  
    WHEN VWS.PROCESS_ID IN(25,18) THEN '0007'  
    WHEN VWS.PROCESS_ID IN(14,37) AND VWS.TOTAL_TRAN_PREMIUM > 0 THEN '0008'  
    WHEN VWS.PROCESS_ID IN(14,37) AND VWS.TOTAL_TRAN_PREMIUM < 0 THEN '0009'  
    WHEN VWS.PROCESS_ID = 12 THEN '0010'  
    ELSE '0009'  
    END AS TRANSACTION_TYPE   
  --CASE     
  -- WHEN PPR.PROCESS_ID IN (25,18) THEN '0007'    
  -- WHEN PPR.PROCESS_ID=14 AND APIPD.TOTAL_TRAN_PREMIUM>0 THEN '0008'    
  -- WHEN PPR.PROCESS_ID=14 AND APIPD.TOTAL_TRAN_PREMIUM<0 THEN '0009'    
  -- WHEN PPR.PROCESS_ID=12 THEN '0010'    
  -- ELSE  '0009'    
  --END AS TRANSACTION_TYPE    
  -----------------------  FOR PREMIUM TYPE    
    ----------- FOR NEW BUSINESS OR RENEWAL    
    ,CASE  
    WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0995'  
    WHEN (VWS.CO_INSURANCE = '14549' AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0986'  
    WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0995'                
    WHEN (VWS.CO_INSURANCE = '14548'  AND VWS.LEADER_FOLLOWER = '14549'AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '0987'  
    WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0991'  
    WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0990'  
    WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID IN(25,18,14,37) AND (VWS.TOTAL_TRAN_PREMIUM > 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '0408'  
        
    --CASE     
    --    WHEN ( ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID IN (25 ,14)    
    --  AND (MRCL_1.COM_TYPE='14694' or MRCL_1.COM_TYPE='')    
    --  ) THEN '0991'    
    --   WHEN ( ISNULL(PRI.COMPANY_ID,0)<>0  AND PPR.PROCESS_ID IN (25 ,14)    
    --  AND MRCL_1.COM_TYPE='14552'    
    --  ) THEN '0990'    
    --   WHEN (ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID IN (25 ,14)    
    --  AND MRCL_1.COM_TYPE='14553'    
    --  ) THEN '0408'    
          
    --  WHEN (PCPL.CO_INSURANCE=14547 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID in (25,18,14) AND (APIPD.TOTAL_TRAN_PREMIUM>0.00)) THEN '0995' -- 25-Commit New Business 5- Renewal    
    --   WHEN (PCPL.CO_INSURANCE=14549 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID in (25,18,14) AND (APIPD.TOTAL_TRAN_PREMIUM>0.00)) THEN '0986'     
    --   WHEN (PCPL.CO_INSURANCE=14548 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID in (25,18,14) AND (APIPD.TOTAL_TRAN_PREMIUM>0.00)) THEN '0987'     
         
    ----------- FOR NEW CANCEL POLICY     
      WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5130'  
      WHEN (VWS.CO_INSURANCE = '14549' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5092'  
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5130'                         
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14549' AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5386'  
      WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5387'  
      WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5388'  
      WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID = 12 AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5389'  
      
      --WHEN (PCPL.CO_INSURANCE in (14547,14549,14548) and ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=12    
      --AND (MRCL_1.COM_TYPE='14694' or MRCL_1.COM_TYPE='')    
      --)    
      -- THEN '5387'    
      -- WHEN (ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=12     
      --AND MRCL_1.COM_TYPE='14552'     
      --)    
      -- THEN '5388'    
      -- WHEN (ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=12     
      --AND MRCL_1.COM_TYPE=14553 ) THEN '5389'    
      -- WHEN (PCPL.CO_INSURANCE=14547 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID =12) THEN '5130' -- 12-Cancel Policy    
      -- WHEN (PCPL.CO_INSURANCE=14549 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID=12) THEN '5092'     
      -- WHEN (PCPL.CO_INSURANCE=14548 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID=12) THEN '5386'     
           
    ----------- FOR REFUND    
      WHEN (VWS.CO_INSURANCE = '14547' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5131'  
      WHEN (VWS.CO_INSURANCE = '14549' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5093'  
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14548' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5131'                
      WHEN (VWS.CO_INSURANCE = '14548' AND VWS.LEADER_FOLLOWER = '14549' AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) = 0) THEN '5382'                  
      WHEN ((VWS.COM_TYPE = '14694' OR VWS.COM_TYPE = '' ) AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5383'  
      WHEN ((VWS.COM_TYPE = '14552') AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5384'  
      WHEN ((VWS.COM_TYPE = '14553') AND VWS.PROCESS_ID IN(14,37) AND (VWS.TOTAL_TRAN_PREMIUM < 0.00) AND ISNULL(VWS.COMPANY_ID,0) <> 0) THEN '5385'  
        
      --  WHEN (ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=14    
      --AND (MRCL_1.COM_TYPE='14694' or MRCL_1.COM_TYPE='')    
      --AND (APIPD.TOTAL_TRAN_PREMIUM<0)) THEN '5383'    
      -- WHEN ( ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=14     
      --AND MRCL_1.COM_TYPE='14552'     
      -- AND (APIPD.TOTAL_TRAN_PREMIUM<0)) THEN '5384'    
      -- WHEN (ISNULL(PRI.COMPANY_ID,0)<>0 AND PPR.PROCESS_ID=14     
      --AND MRCL_1.COM_TYPE='14553'    
      -- AND (APIPD.TOTAL_TRAN_PREMIUM<0)) THEN '5385'    
      --WHEN (PCPL.CO_INSURANCE=14547 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID =14 )    
      -- THEN '5131' -- 14-ENDORSEMNET    
      -- WHEN (PCPL.CO_INSURANCE=14549 AND ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID=14 ) THEN '5093'     
      -- WHEN (PCPL.CO_INSURANCE=14548 and ISNULL(PRI.COMPANY_ID,0)=0 AND PPR.PROCESS_ID=14 ) THEN '5382'     
         
     
     
    END AS PREMIUM_TYPE    
    
  ,VWS.SUSEP_LOB_CODE    
  
  ,CASE WHEN (VWS.PROCESS_ID IN(25,18)) THEN VWS.PPR_EFFECTIVE_DATETIME   
  WHEN (VWS.PROCESS_ID IN (14,37)) THEN VWS.PPR_1_EFFECTIVE_DATETIME   
  WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME   
  END AS ORIGINAL_EFFECTIVE_DATE    
  
  ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.POLICY_EXPIRATION_DATE  
  WHEN (VWS.PROCESS_ID IN (14,37)) THEN VWS.POL_VER_EXPIRATION_DATE   
  WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME   
  END    
   AS ORIGINAL_EXPIRY_DATE    
  
  --,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.COMPLETED_DATETIME   
  --WHEN (VWS.PROCESS_ID = 14) THEN VWS.PPR_1_EFFECTIVE_DATETIME    
  --WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME   
  --END    
  -- AS ORIGINAL_ISSUE_DATE 
  ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.COMPLETED_DATETIME        
	WHEN (VWS.PROCESS_ID IN (37,14,12)) THEN VWS.PPR_1_COMPLETED_DATETIME 
   END          
   AS ORIGINAL_ISSUE_DATE
      
  ,CASE WHEN VWS.TRAN_TYPE = 'NBS' THEN ISNULL(VWS.APIPD_TOTAL_PREMIUM ,0)
   ELSE ISNULL(VWS.APIPD_1_TOTAL_PREMIUM,0) 
   END AS ORIGINAL_PREMIUM  
 
  ,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.PPR_EFFECTIVE_DATETIME   
  WHEN (VWS.PROCESS_ID IN (14,37)) THEN VWS.PPR_EFFECTIVE_DATETIME   
  WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME   
 ELSE VWS.POLICY_EFFECTIVE_DATE   
 END    
   AS SECONDARY_EFFECTIVE_DATE 
      
  --,CASE WHEN (VWS.PROCESS_ID IN (25,18)) THEN VWS.POLICY_EXPIRATION_DATE    
  --WHEN (VWS.PROCESS_ID = 14) THEN VWS.[EXPIRY_DATE]  
  --WHEN (VWS.PROCESS_ID = 12) THEN VWS.PPR_EFFECTIVE_DATETIME  
  -- END    
  -- AS SECONDARY_EXPIRY_DATE  
  
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
    
  
  --,CASE WHEN VWS.PROCESS_ID IN(14,18,12,25) THEN VWS.PPR_EFFECTIVE_DATETIME   
  --END    
  --AS SECONDARY_ISSUE_DATE
  
  ,CASE WHEN VWS.PROCESS_ID IN(18,25) THEN VWS.COMPLETED_DATETIME      
  WHEN VWS.PROCESS_ID IN(14,12,37) THEN VWS.COMPLETED_DATETIME 
  END          
  AS SECONDARY_ISSUE_DATE
      
  --,ISNULL(VWS.TOTAL_TRAN_PREMIUM ,0) AS SECONDARY_PREMIUM    
   ,ABS(ISNULL(VWS.APIPD_TOTAL_PREMIUM ,0)) AS SECONDARY_PREMIUM
 
  --,CASE WHEN (ISNULL(VWS.COMPANY_ID,0)<>0) THEN  ISNULL(VWS.SUSEP_NUM,'05045')  
  --WHEN (VWS.CO_INSURANCE=14547) THEN  ISNULL(VWS.SUSEP_NUM,'05045')  
  --WHEN (VWS.CO_INSURANCE=14548) THEN  ISNULL(VWS.SUSEP_NUM,'05045')  
  --WHEN (VWS.CO_INSURANCE=14549) THEN  ISNULL(VWS.SUSEP_NUM,'05045')  
  --END     
  -- AS SUSEP_OF_INSURERE 
  ,CASE WHEN (ISNULL(VWS.COMPANY_ID,0) = 0) THEN ISNULL(VWS.SUSEP_NUM,'05045')            
   ELSE  ISNULL(VWS.MRCL_1_SUSEP_NUM,'05045')                         
      END                           
   AS SUSEP_OF_INSURERE   
    ,VWS.POLICY_NUMBER   
/* ---- MLM.SUSEP_LOB_CODE    
 -- ,case when PPR.PROCESS_ID IN (25,18) THEN PPR.EFFECTIVE_DATETIME    
 --  WHEN (PPR.PROCESS_ID IN (14) )THEN PPR_1.EFFECTIVE_DATETIME    
 --  WHEN PPR.PROCESS_ID IN (12)  THEN PPR.EFFECTIVE_DATETIME    
 --  END    
 --  AS ORIGINAL_EFFECTIVE_DATE    
 -- ,case when PPR.PROCESS_ID IN (25,18) THEN PCPL.POLICY_EXPIRATION_DATE    
 --  WHEN (PPR.PROCESS_ID IN (14) )THEN PCPL_1.POL_VER_EXPIRATION_DATE    
 --  WHEN PPR.PROCESS_ID IN (12)  THEN PPR.EFFECTIVE_DATETIME    
 --  END    
 --  AS ORIGINAL_EXPIRY_DATE    
 -- ,case when PPR.PROCESS_ID IN (25,18) THEN PPR.COMPLETED_DATETIME    
 --  WHEN PPR.PROCESS_ID IN (14) THEN PPR_1.EFFECTIVE_DATETIME    
 --  WHEN PPR.PROCESS_ID IN (12)  THEN PPR.EFFECTIVE_DATETIME    
    
 --  END    
 --  AS ORIGINAL_ISSUE_DATE    
 -- ,CASE WHEN APIPD.TRAN_TYPE='NBS' THEN ISNULL(APIPD.TOTAL_PREMIUM,0) ELSE ISNULL(APIPD_1.TOTAL_PREMIUM,0) END AS ORIGINAL_PREMIUM    
 -- ,CASE WHEN PPR.PROCESS_ID IN  (25,18) THEN PPR.EFFECTIVE_DATETIME    
 --  WHEN PPR.PROCESS_ID IN (14) THEN PPR.EFFECTIVE_DATETIME    
 --  WHEN PPR.PROCESS_ID IN (12)  THEN PPR.EFFECTIVE_DATETIME    
 --  ELSE PCPL.POLICY_EFFECTIVE_DATE    
 --  END    
 --  AS SECONDARY_EFFECTIVE_DATE    
 -- ,case when PPR.PROCESS_ID IN (25,18) THEN PCPL.POLICY_EXPIRATION_DATE    
 --  WHEN PPR.PROCESS_ID IN (14) THEN PPR.[EXPIRY_DATE]    
 --  WHEN PPR.PROCESS_ID IN (12)  THEN PPR.EFFECTIVE_DATETIME    
       
 --  END    
 --  AS SECONDARY_EXPIRY_DATE    
 -- ,CASE WHEN PPR.PROCESS_ID IN (14,18,12,25) THEN PPR.EFFECTIVE_DATETIME    
 --  --WHEN PPR.PROCESS_ID IN (25) THEN PCPL.POL_VER_EXPIRATION_DATE    
     
     
 -- END    
 -- AS SECONDARY_ISSUE_DATE    
 -- ,ISNULL(APIPD.TOTAL_TRAN_PREMIUM,0) AS SECONDARY_PREMIUM    
    
 -- ,CASE  WHEN (ISNULL(PRI.COMPANY_ID,0)<>0) THEN  ISNULL(MRCL_1.SUSEP_NUM,'05045')    
 --    WHEN (PCPL.CO_INSURANCE=14547) THEN  ISNULL(MRCL.SUSEP_NUM,'05045')    
 --    WHEN (PCPL.CO_INSURANCE=14548) THEN  ISNULL(MRCL.SUSEP_NUM,'05045')     
 --    WHEN (PCPL.CO_INSURANCE=14549) THEN  ISNULL(MRCL.SUSEP_NUM,'05045')     
        
 --  END     
 --  AS SUSEP_OF_INSURERE    
 -- --,COUNT(1) AS NO_OF_REGISTRIES    
 -- ,PCPL.POLICY_NUMBER    
  --,PCPL.POLICY_ID    
  --,PCPL.POLICY_VERSION_ID    
  --,APIPD.TRAN_TYPE    
  --SELECT count(*)   */ 
  FROM  #TEMP VWS WITH(NOLOCK)   
  /* 
  --(    
  --SELECT CUSTOMER_ID,NEW_POLICY_ID,NEW_POLICY_VERSION_ID,PROCESS_ID,EFFECTIVE_DATETIME,[EXPIRY_DATE],COMPLETED_DATETIME    
  --FROM POL_POLICY_PROCESS (NOLOCK)       
    
  --WHERE PROCESS_ID in (25,12,14,18)     
  --) PPR    
  --JOIN    
  --POL_CUSTOMER_POLICY_LIST (NOLOCK) PCPL    
  --ON PPR.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PPR.NEW_POLICY_ID=PCPL.POLICY_ID     
  -- AND PPR.NEW_POLICY_VERSION_ID= PCPL.POLICY_VERSION_ID    
  --LEFT OUTER JOIN    
  --MNT_LOB_MASTER (NOLOCK) MLM    
  --ON MLM.LOB_ID=PCPL.POLICY_LOB    
  --LEFT OUTER JOIN    
  --POL_CO_INSURANCE (NOLOCK) PCI    
  --ON (PCPL.CUSTOMER_ID=PCI.CUSTOMER_ID AND PCPL.POLICY_ID=PCI.POLICY_ID AND PCPL.POLICY_VERSION_ID=PCI.POLICY_VERSION_ID)    
  --LEFT OUTER JOIN    
  --MNT_REIN_COMAPANY_LIST (NOLOCK) MRCL    
    
  --ON MRCL.REIN_COMAPANY_ID=PCI.COMPANY_ID    
    
  --LEFT OUTER JOIN    
    
  --POL_REINSURANCE_INFO (NOLOCK) PRI    
  --ON (PCPL.CUSTOMER_ID=PRI.CUSTOMER_ID AND PCPL.POLICY_ID=PRI.POLICY_ID AND PCPL.POLICY_VERSION_ID=PRI.POLICY_VERSION_ID)    
    
  --LEFT OUTER JOIN    
    
  --ACT_POLICY_INSTALL_PLAN_DATA (NOLOCK) APIPD    
  --ON (PCPL.CUSTOMER_ID=APIPD.CUSTOMER_ID AND PCPL.POLICY_ID=APIPD.POLICY_ID AND PCPL.POLICY_VERSION_ID=APIPD.POLICY_VERSION_ID )    
    
  -- LEFT OUTER JOIN    
  --ACT_POLICY_INSTALL_PLAN_DATA (NOLOCK) APIPD_1    
  --ON (APIPD.CUSTOMER_ID=APIPD_1.CUSTOMER_ID AND APIPD.POLICY_ID=APIPD_1.POLICY_ID AND APIPD.POLICY_VERSION_ID=(APIPD_1.POLICY_VERSION_ID+1) )    
    
    
    
  --LEFT OUTER JOIN     
  --MNT_REIN_COMAPANY_LIST (NOLOCK) MRCL_1    
  --ON  PRI.COMPANY_ID=MRCL_1.REIN_COMAPANY_ID    
    
  --LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST (NOLOCK) PCPL_1    
  --ON (PCPL.CUSTOMER_ID=PCPL_1.CUSTOMER_ID AND PCPL.POLICY_ID=PCPL_1.POLICY_ID AND PCPL.POLICY_VERSION_ID=(PCPL_1.POLICY_VERSION_ID+1) )    
    
  --LEFT OUTER JOIN    
  --POL_POLICY_PROCESS (NOLOCK) PPR_1    
  --ON  (PCPL.CUSTOMER_ID=PPR_1.CUSTOMER_ID AND PCPL.POLICY_ID=PPR_1.NEW_POLICY_ID AND PCPL.POLICY_VERSION_ID=(PPR_1.NEW_POLICY_VERSION_ID+1) )    
   */   
      
  WHERE    
    ------------------------- FOR POLICY NUMBER OPTIONAL PARAMETER      
    --(@POLICY_NUMBER IS NULL  or VWS.POLICY_NUMBER=@POLICY_NUMBER)    
    ------------------------- FOR DATE FILTER    
    /*AND (Convert (DECIMAL,@MONTH)=DATEPART(MM, CASE WHEN VWS.POLICY_EFFECTIVE_DATE>VWS.PPR_EFFECTIVE_DATETIME THEN VWS.POLICY_EFFECTIVE_DATE    
     ELSE VWS.PPR_EFFECTIVE_DATETIME END)     
    AND  DATEPART(YY,CASE WHEN VWS.POLICY_EFFECTIVE_DATE>VWS.PPR_EFFECTIVE_DATETIME THEN VWS.POLICY_EFFECTIVE_DATE    
     ELSE VWS.PPR_EFFECTIVE_DATETIME END)=CONVERT(DECIMAL,@YEAR))*/
	 VWS.COMPLETED_DATETIME BETWEEN @FROM_DATETIME AND @TO_DATETIME
	 --AND MONTH(VWS.COMPLETED_DATETIME) = (Convert (DECIMAL,@MONTH))
  --  AND YEAR(VWS.COMPLETED_DATETIME) =  (CONVERT(DECIMAL,@YEAR))    
    AND VWS.IS_ACTIVE='Y'    
    AND VWS.PROCESS_ID in (25,12,14,18,37)    
  --  AND (
		--	(VWS.SUSEP_LOB_CODE   like '09%' AND  RIGHT(VWS.SUSEP_LOB_CODE,2) NOT IN ('83', '86','94')   ) 
		--	OR (VWS.SUSEP_LOB_CODE  like '13%' AND RIGHT(VWS.SUSEP_LOB_CODE,2) NOT IN ('83', '86','91','92') )    
		--)
     AND ((LEFT(VWS.SUSEP_LOB_CODE,2) <> '09' AND RIGHT(VWS.SUSEP_LOB_CODE,2) NOT IN ('83', '86','94')  )
    OR (LEFT(VWS.SUSEP_LOB_CODE,2) <> '13' AND RIGHT(VWS.SUSEP_LOB_CODE,2) NOT IN ('83', '86','91','92')  )
    )   
    --AND (VWS.LEADER_FOLLOWER=CASE WHEN VWS.CO_INSURANCE=14548 THEn 14548 ELSE 14549 END or VWS.CO_INSURANCE=14547)    
    --AND VWS.APIPD_TOTAL_PREMIUM is not null
	 AND ISNULL(VWS.APIPD_TOTAL_PREMIUM,0) <> 0                
    AND  ISNULL(VWS.TOTAL_TRAN_PREMIUM,0) <> 0    
    
  ) FINAL_PPR    
  GROUP BY FINAL_PPR.TRANSACTION_TYPE,FINAL_PPR.PREMIUM_TYPE,FINAL_PPR.SUSEP_LOB_CODE    
    ,FINAL_PPR.ORIGINAL_EFFECTIVE_DATE,FINAL_PPR.ORIGINAL_EXPIRY_DATE,FINAL_PPR.ORIGINAL_ISSUE_DATE    
    ,FINAL_PPR.ORIGINAL_PREMIUM,FINAL_PPR.SECONDARY_EFFECTIVE_DATE,FINAL_PPR.SECONDARY_EXPIRY_DATE,FINAL_PPR.SECONDARY_ISSUE_DATE    
    ,FINAL_PPR.SECONDARY_PREMIUM,FINAL_PPR.SUSEP_LOB_CODE,FINAL_PPR.SUSEP_OF_INSURERE    
    ,FINAL_PPR.POLICY_NUMBER    
    --,FINAL_PPR.POLICY_ID,FINAL_PPR.POLICY_VERSION_ID,FINAL_PPR.TRAN_TYPE    
     
    
  
       --------------------  FINAL OUTPUT    
   SELECT     
    
 RIGHT('000000' +CONVERT(VARCHAR,SEQUENCE_NO),6) AS [ESRSEQ]    
  ,'05045' AS [ENTCODIGO]    
  ,CONVERT(VARCHAR(8),@LAST_DAY,112) AS [MRFMESANO]    
  ,324 AS [QUAID]    
  ,TRANSACTION_TYPE AS [TPMOID]    
  ,PREMIUM_TYPE AS [CMPID]     
  ,RIGHT('0000'+SUSEP_LINE_OF_BUSINESS,4)  as [RAMCODIGO]    
  ,CONVERT(VARCHAR,ORIGINAL_EFFECTIVE_DATE,112)    
    AS [ESPDATAINICIORO]    
  ,CONVERT(VARCHAR,ORIGINAL_EXPIRY_DATE,112)    
    AS [ESPDATAFIMRO]    
  ,CONVERT(VARCHAR,ORIGINAL_ISSUE_DATE,112)    
   AS [ESPDATAEMISSRO]    
  ,CASE WHEN ORIGINAL_RISK_PREMIUM>=0.00    
   THEN    
    REPLACE(RIGHT('0000000000'+CONVERT(VARCHAR(26),ORIGINAL_RISK_PREMIUM),13),'.',',')    
    ELSE    
     REPLACE('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,ORIGINAL_RISK_PREMIUM),2,LEN(ORIGINAL_RISK_PREMIUM)),12),'.',',')    
  END AS ESPVALORMOVRO    
      
  ,CONVERT(VARCHAR,SECONDARY_EFFECTIVE_DATE,112)    
    AS [ESPDATAINICIORD]    
     
  ,CONVERT(VARCHAR,SECONDARY_EXPIRY_DATE,112)    
    AS [ESPDATAFIMRD]    
      
  ,CONVERT(VARCHAR,SECONDARY_ISSUE_DATE,112)    
    AS [ESPDATAEMISSRD]    
      
  ,CASE WHEN SECONDARY_RISK_PREMIUM>=0.00    
   THEN    
     REPLACE(RIGHT('0000000000'+CONVERT(VARCHAR(26),SECONDARY_RISK_PREMIUM),13),'.',',')    
    ELSE    
       REPLACE('-'+RIGHT('0000000000' +SUBSTRING(CONVERT(VARCHAR,SECONDARY_RISK_PREMIUM),2,LEN(ORIGINAL_RISK_PREMIUM)),12),'.',',')    
  END AS [ESPVALORMOVRD]    
      
  ,RIGHT('00000'+SUSEP_CODE_OF_INSURER,5) AS  [ESPCODCESS]    
  ,RIGHT('0000'+NO_OF_REGISTRIES,4)  AS [ESPFREQ]    
  ,POLICY_NUMBER AS [APOLICE]    
  --,POLICY_ID    
  --,POLICY_VERSION_ID    
  --,TRAN_TYPE    
    
 FROM #TEMPTABLE (NOLOCK) WHERE PREMIUM_TYPE IS NOT NULL
 --select * from    #TEMPTABLE (NOLOCK)
    
    
DROP TABLE #TEMPTABLE
DROP TABLE #TEMP    
END  
GO

