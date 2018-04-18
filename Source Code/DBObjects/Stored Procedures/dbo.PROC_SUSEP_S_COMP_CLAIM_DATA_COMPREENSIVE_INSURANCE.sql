IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_S_COMP_CLAIM_DATA_COMPREENSIVE_INSURANCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_S_COMP_CLAIM_DATA_COMPREENSIVE_INSURANCE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-----------------------------------------------------------------------------------------------------------------------------
		----itrack 1244(356)
-----------------------------------------------------------------------------------------------------------------------------

--DROP PROC [dbo].[PROC_SUSEP_S_COMP_CLAIM_DATA_COMPREENSIVE_INSURANCE] 
CREATE PROCEDURE [dbo].[PROC_SUSEP_S_COMP_CLAIM_DATA_COMPREENSIVE_INSURANCE]   --FIXED     
      
@DATETIME DATETIME = NULL  
                                   
AS                                    
BEGIN                             
    IF(@DATETIME = NULL)  
  IF(@DATETIME IS NULL)  
  BEGIN  
   SELECT @DATETIME = GETDATE()  
  END  
   --print convert(varchar(10),@BASE_DATE, 101) 
   
SELECT 
   aa.COD_SEG,  
  ISNULL(aa.TIPO,'') AS TIPO,  
  ISNULL(aa.CLASSE,'') AS CLASSE,  
  RIGHT(ISNULL(aa.APOLICE,''),20) AS APOLICE,  
  RIGHT(aa.ENDOSSO,10) AS ENDOSSO,  
  ISNULL(aa.ITEM,'') AS ITEM,--SH  
  aa.COBERTURA,  
  LEFT(ISNULL(aa.UF,''),2) AS UF,  
  ISNULL(aa.VAL_FRANQ, '') AS VAL_FRANQ,--SH  
  SUM(CAST(ISNULL(aa.INDENIZ,0) AS DECIMAL(11,0))) AS INDENIZ,  	
  ISNULL(aa.D_AVISO,'') D_AVISO,  
  ISNULL(aa.D_LIQ,'') D_LIQ,  
  ISNULL(aa.D_OCORR,'') D_OCORR  
FROM
   (        
SELECT 
		COD_SEG,
		ISNULL(TIPO,'') AS TIPO,
		ISNULL(CLASSE,'') AS CLASSE,
		RIGHT(ISNULL(APOLICE,''),20) AS APOLICE,
		RIGHT(ENDOSSO,10) AS ENDOSSO,
		ISNULL(ITEM,'') AS ITEM,--SH
		COBERTURA,
		LEFT(ISNULL(UF,''),2) AS UF,
		ISNULL(VAL_FRANQ, '') AS VAL_FRANQ,--SH
		CAST(ISNULL(INDENIZ,0) AS DECIMAL(11,0)) AS INDENIZ,
		ISNULL(D_AVISO,'') D_AVISO,
		ISNULL(D_LIQ,'') D_LIQ,
		ISNULL(D_OCORR,'') D_OCORR
		
    FROM 
(SELECT   
  
  ---------------------------------------------------------------------------------------------------------------    
   ---CODE_SEG  
  ---------------------------------------------------------------------------------------------------------------       
  '504-5' AS COD_SEG, --ALIANCA DA BAHIA SUSEP CODE   
  ---------------------------------------------------------------------------------------------------------------    
   ---TIPO   
  ---------------------------------------------------------------------------------------------------------------                
   
  (SELECT     --SHUBH  
   CASE       
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14730  THEN '1' --DWELLING      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14732  THEN '1'      
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14731  THEN '1'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14733  THEN '1'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14734  THEN '1'      
          
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14735  THEN '2' --COMPREHENSIVE CONDOMINIUM      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14736  THEN '2'     
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14737  THEN '2'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14738  THEN '2'      
    WHEN M.LOOKUP_VALUE_CODE = '05' AND M.LOOKUP_UNIQUE_ID = 14739  THEN '2'      
    WHEN M.LOOKUP_VALUE_CODE = '06' AND M.LOOKUP_UNIQUE_ID = 14740  THEN '2'      
    WHEN M.LOOKUP_VALUE_CODE = '07' AND M.LOOKUP_UNIQUE_ID = 14741  THEN '2'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14742  THEN '2'      
          
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14743  THEN '3' --COMPREHENSIVE COMPANY      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14744  THEN '3'      
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14745  THEN '3'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14746  THEN '3'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14747  THEN '3'      
   END       
   FROM       
	POL_PRODUCT_LOCATION_INFO P WITH(NOLOCK)       
  LEFT OUTER JOIN       
	MNT_LOOKUP_VALUES M WITH(NOLOCK) ON (P.CLASS_FIELD = M.LOOKUP_UNIQUE_ID)      
  WHERE       
			CIP.POL_RISK_ID= P.PRODUCT_RISK_ID AND         
            CCI.CUSTOMER_ID = P.CUSTOMER_ID AND         
            CCI.POLICY_ID = P.POLICY_ID AND         
            CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID) AS TIPO,  
            
 (SELECT 
	CASE       
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14730  THEN '01' --DWELLING      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14732  THEN '02'      
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14731  THEN '03'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14733  THEN '04'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14734  THEN '99'      
          
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14735  THEN '01' --COMPREHENSIVE CONDOMINIUM      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14736  THEN '02'     
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14737  THEN '03'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14738  THEN '04'      
    WHEN M.LOOKUP_VALUE_CODE = '05' AND M.LOOKUP_UNIQUE_ID = 14739  THEN '05'      
    WHEN M.LOOKUP_VALUE_CODE = '06' AND M.LOOKUP_UNIQUE_ID = 14740  THEN '06'      
    WHEN M.LOOKUP_VALUE_CODE = '07' AND M.LOOKUP_UNIQUE_ID = 14741  THEN '07'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14742  THEN '99'      
          
    WHEN M.LOOKUP_VALUE_CODE = '01' AND M.LOOKUP_UNIQUE_ID = 14743  THEN '01' --COMPREHENSIVE COMPANY      
    WHEN M.LOOKUP_VALUE_CODE = '02' AND M.LOOKUP_UNIQUE_ID = 14744  THEN '02'      
    WHEN M.LOOKUP_VALUE_CODE = '03' AND M.LOOKUP_UNIQUE_ID = 14745  THEN '03'      
    WHEN M.LOOKUP_VALUE_CODE = '04' AND M.LOOKUP_UNIQUE_ID = 14746  THEN '04'      
    WHEN M.LOOKUP_VALUE_CODE = '99' AND M.LOOKUP_UNIQUE_ID = 14747  THEN '99'      
   END    
  FROM       
	POL_PRODUCT_LOCATION_INFO P WITH(NOLOCK)       
  LEFT OUTER JOIN       
	MNT_LOOKUP_VALUES M WITH(NOLOCK) ON (P.CLASS_FIELD = M.LOOKUP_UNIQUE_ID)      
  WHERE       
			CIP.POL_RISK_ID= P.PRODUCT_RISK_ID AND         
            CCI.CUSTOMER_ID = P.CUSTOMER_ID AND         
            CCI.POLICY_ID = P.POLICY_ID AND         
            CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID) AS CLASSE,
  ---------------------------------------------------------------------------------------------------------------    
   ---POLICY NUMBER  
  ---------------------------------------------------------------------------------------------------------------     
  PCPL.POLICY_NUMBER AS APOLICE,  
  ---------------------------------------------------------------------------------------------------------------    
   ---ENDOSSO  
  ---------------------------------------------------------------------------------------------------------------     
  ISNULL(PPE.ENDORSEMENT_NO ,'') AS ENDOSSO,  
    
    -----------------------------------------------------------------------------------------------------  
   --FILL WITH IDENTIFICATION ITEM OF RISK IN CASE OF COLLECTIVE POLICY,   
   --ELSE FILL WITH THE VALUE "000001". THE NUMBER SHOULD BE ALLIGNED TO RIGHT,   
   --AND COMPLETED WITH ZEROS TO LEFT.  
     -----------------------------------------------------------------------------------------------------  
     --(SELECT CASE WHEN CIP.ITEM_NUMBER > 1 THEN CIP.ITEM_NUMBER  ELSE 0 END AS ITEM_NUMBER              
     --    FROM VW_POL_PRODUCT_RISK_DETAILS P WITH(NOLOCK)            
     --       WHERE CIP.POL_RISK_ID= P.RISK_ID AND             
     --       CCI.CUSTOMER_ID = P.CUSTOMER_ID AND             
     --       CCI.POLICY_ID = P.POLICY_ID AND             
     --       CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID) AS ITEM, 
	 
	 (SELECT P.ITEM_NUMBER      
            FROM     
            POL_PRODUCT_LOCATION_INFO P WITH(NOLOCK)    
            WHERE CIP.POL_RISK_ID= P.PRODUCT_RISK_ID AND     
            CCI.CUSTOMER_ID = P.CUSTOMER_ID AND     
            CCI.POLICY_ID = P.POLICY_ID AND     
            CCI.POLICY_VERSION_ID=P.POLICY_VERSION_ID) AS ITEM, 
             
                       
        ---------------FILL WITH COVERAGE CODE CONTRACTED  
    0 AS COBERTURA, --SHUBH 
        
         
   ----------------FILL WITH STATE ABBREVIATION CODE OF LOCAL OF RISK.  
   MCSL1.STATE_CODE AS UF,  
        
     --------------------FILL WITH TOTAL VALUE (IN R$) OF PARTICIPATION OF INSURED IN LOSSES.  
   --  (SELECT SUM(ISNULL(PPC.DEDUCTIBLE_1 ,0)) FROM POL_PRODUCT_COVERAGES PPC WITH(NOLOCK)  
	  --WHERE PPC.CUSTOMER_ID = PCPL.CUSTOMER_ID   
   --  AND PPC.POLICY_ID = PCPL.POLICY_ID   
   --     AND PPC.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID) 
   CAST(CPC.DEDUCTIBLE_1 AS NVARCHAR(100)) AS VAL_FRANQ,  
           
   -------------------------------------------------------------------------------------------------------  
   --FILL WITH TOTAL VALUE OF INDEMINITY PAID IN COVERAGE.   
   --FOR CASE OF WARNING CLAIM AND NOT PAID, THE INSURER SHOULD BE INFORM TH AMONT OF THIS INDEMINITY.  
   -------------------------------------------------------------------------------------------------------  
   (SELECT SUM(ISNULL(CLM_R.PAYMENT_AMOUNT,0))          
		 FROM             
			CLM_ACTIVITY_RESERVE CLM_R WITH(NOLOCK)   
			LEFT OUTER JOIN CLM_PRODUCT_COVERAGES CPR ON CLM_R.CLAIM_ID = CPR.CLAIM_ID AND CLM_R.COVERAGE_ID = CPR.CLAIM_COV_ID AND CPR.IS_RISK_COVERAGE = 'Y'    
				WHERE CLM_R.CLAIM_ID = CCI.CLAIM_ID AND CLM_R.COVERAGE_ID = CPC.CLAIM_COV_ID  AND  CLM_R.ACTIVITY_ID =CAR.ACTIVITY_ID      
				)AS INDENIZ, 
    
     ------------------------------------------------------------------------------------------------------     
   ---DATE OF WARNING.  
  ------------------------------------------------------------------------------------------------------      
  CAST(YEAR(CCI.FIRST_NOTICE_OF_LOSS) AS VARCHAR(4))+      -- FOR YEAR PART    
  RIGHT('0'+CAST(MONTH(CCI.FIRST_NOTICE_OF_LOSS) AS VARCHAR(2)),2)+ -- FOR MONTH PART    
  RIGHT('0'+CAST(DAY(CCI.FIRST_NOTICE_OF_LOSS)AS VARCHAR(2)),2)     -- FOR DAY PART    
     AS D_AVISO,  
             
     ------------------FILL WITH THE DATE OF LIQUIDATION OF THE CLAIMYYYYMMDD. 

  --CONVERT(VARCHAR(10),CASE WHEN CA.ACTION_ON_PAYMENT = 181 THEN CONVERT(VARCHAR(8),CA.ACTIVITY_DATE,112)
		--WHEN CA.ACTION_ON_PAYMENT = 180 THEN CONVERT(VARCHAR(8),(SELECT TOP 1 CA.ACTIVITY_DATE   
		--									   FROM CLM_ACTIVITY CA WITH(NOLOCK)   
		--									   WHERE ACTION_ON_PAYMENT = 180  
		--										  AND CA.CLAIM_ID = CCI.CLAIM_ID  
		--									   ORDER BY ACTIVITY_ID),112)
		--ELSE '00000000'
  -- END,112) AS D_LIQ,

   CONVERT(VARCHAR(10),(SELECT TOP 1 CA.ACTIVITY_DATE     
					  FROM CLM_ACTIVITY CA WITH(NOLOCK)     
					  WHERE CA.CLAIM_ID = CCI.CLAIM_ID    
					  ORDER BY ACTIVITY_ID),112) AS D_LIQ,
   ---------------------------------------------------------------------------------------------------------------    
   ---OCCURRENCE DATE ( LOSS DATE)    
  ---------------------------------------------------------------------------------------------------------------                
  CAST(YEAR(CCI.LOSS_DATE) AS VARCHAR(4))+          -- FOR YEAR PART    
  RIGHT('0'+CAST(MONTH(CCI.LOSS_DATE) AS VARCHAR(2)),2)+       -- FOR MONTH PART    
  RIGHT('0'+CAST(DAY(CCI.LOSS_DATE)AS VARCHAR(2)),2)        -- FOR DAY PART    
  AS D_OCORR  
   
FROM CLM_CLAIM_INFO CCI WITH (NOLOCK)           
		LEFT OUTER JOIN CLM_ACTIVITY  CA WITH (NOLOCK) ON CA.CLAIM_ID = CCI.CLAIM_ID AND CA.ACTION_ON_PAYMENT IN (180,181)      
		LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR WITH (NOLOCK) ON CA.CLAIM_ID = CAR.CLAIM_ID AND CA.ACTIVITY_ID = CAR.ACTIVITY_ID 
		INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL (NOLOCK) ON PCPL.CUSTOMER_ID = CCI.CUSTOMER_ID AND PCPL.POLICY_ID = CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID                   
		LEFT OUTER JOIN POL_POLICY_ENDORSEMENTS PPE WITH(NOLOCK) ON PPE.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPE.POLICY_ID = PCPL.POLICY_ID AND PPE.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID                       
		INNER JOIN CLM_INSURED_PRODUCT CIP WITH(NOLOCK) ON CCI.CLAIM_ID = CIP.CLAIM_ID         
		INNER JOIN CLM_PRODUCT_COVERAGES CPC WITH(NOLOCK) ON CCI.CLAIM_ID = CPC.CLAIM_ID AND CAR.COVERAGE_ID = CPC.CLAIM_COV_ID AND IS_RISK_COVERAGE='Y'   
		LEFT OUTER JOIN MNT_COVERAGE MC WITH(NOLOCK) ON CPC.COVERAGE_CODE_ID = MC.COV_ID   
		LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL1 WITH(NOLOCK) ON CIP.COUNTRY1 = MCSL1.COUNTRY_ID AND (CIP.STATE1 = CONVERT(VARCHAR ,MCSL1.STATE_ID) OR CIP.STATE1 = CONVERT(VARCHAR ,MCSL1.STATE_CODE))         
		INNER JOIN MNT_DIV_LIST MDL WITH(NOLOCK) ON (MDL.DIV_ID = PCPL.DIV_ID)       
		INNER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK) ON (MLM.LOB_ID = CCI.LOB_ID)     
  
WHERE  CA.IS_ACTIVE = 'Y'
				AND CCI.CLAIM_STATUS = 11739  -- OPEN CLAIMS      
				AND MLM.SUSEP_LOB_CODE IN (0114,1116,0118)    
				AND CCI.LOSS_DATE BETWEEN CONVERT(DATE,'01'+'/'+'01'+'/'+ CONVERT(VARCHAR,YEAR(@DATETIME)-1))     
				AND CONVERT(DATE,'12'+'/'+'31'+'/'+ CONVERT(VARCHAR,YEAR(@DATETIME)-1)) 
				AND CCI.FIRST_NOTICE_OF_LOSS <=  CONVERT(DATE,'02'+'/'+'28'+'/'+ CONVERT(VARCHAR,YEAR(@DATETIME)))
				AND CAR.PAYMENT_AMOUNT <> 0
		  ) AS TEMP  
  
 ) aa 
GROUP BY  
aa.COD_SEG,  
aa.TIPO,
aa.CLASSE,
aa.APOLICE,  
aa.ENDOSSO,
aa.ITEM,
aa.COBERTURA,  
aa.UF, 
aa.VAL_FRANQ,
aa.D_AVISO,
aa.D_LIQ,
aa.D_OCORR
END   
  
GO

