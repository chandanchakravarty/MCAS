IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_TEST_RI_CLAIM_PagnetTesting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_TEST_RI_CLAIM_PagnetTesting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 --DROP PROC PROC_TEST_RI_CLAIM
 CREATE PROC PROC_TEST_RI_CLAIM_PagnetTesting
 AS
SELECT * FROM  
(SELECT '05' AS [Interface code],
C_RSRV.CLAIM_ID AS CLAIM_ID,
C_RSRV.ACTIVITY_ID AS ACTIVITY_ID,   
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID ) AS [Sequence of record],  
 'J' AS [Beneficiary Type],  
 '' AS [FOREIGN],  
 '064' AS [Beneficiary Class],  
 MR_LST.REIN_COMAPANY_NAME   AS [Beneficiary name],  
 MR_LST.CARRIER_CNPJ AS [Beneficiary ID],  
  '0' AS [Beneficiary foreign ID],  
 MR_LST.REIN_COMAPANY_ADD1 AS [Beneficiary Address (street name)],  
 MR_LST.REIN_COMAPANY_ADD1 AS [Beneficiary Address (number)], --nEED TO SPLIT FROM ,  
 MR_LST.REIN_COMAPANY_ADD2 AS [Beneficiary Address (complement)],  
 MR_LST.DISTRICT AS [Beneficiary Address (district)],  
 MR_LST.REIN_COMAPANY_STATE AS [Beneficiary Address (state)],  
 MR_LST.REIN_COMAPANY_CITY AS [Beneficiary Address (city)],  
 MR_LST.REIN_COMAPANY_ZIP AS [Beneficiary Address (zip code)],  
 MR_LST.REIN_COMAPANY_EMAIL AS [Beneficiary e-mail address],   
CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN  
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION  
   
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)     
  ELSE MR_LST.BANK_BRANCH_NUMBER    
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH     
       
   CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)     
  ELSE ''    
  END  ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],   
  
   CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN  
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)     
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER    
  END ELSE '' END AS [Beneficiary Bank Account number],    
      
   CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)     
  ELSE ''    
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],  
  MR_LST.REIN_COMPANY_TYPE AS [Beneficiary Bank Account type],--  
  '1' AS [Beneficiary Bank Account Currency],   
  NULL AS [Cód Tributação IRRF],  
  NULL AS [Natureza do Rendimento],  
  NULL AS [Calcula ISS],  
  NULL AS [Calcula INSS],  
  NULL AS [Cód Tributação INSS],  
  NULL AS [Cód Tributação CSLL],  
  NULL AS [Cód Tributação COFINS],  
  NULL AS [Cód Tributação PIS],  
  NULL AS [No de Dependentes],  
  NULL AS [No PIS],  
  NULL AS [Inscrição Municipal],  
  NULL AS [Número interno do corretor],  
  NULL AS [CBO (Classific Brasileira Ocupação)],  
  NULL AS [Código SUSEP], -- BENEFICIARY CLASS IS HARD CODED  
  NULL AS [No do Funcionário],  
  NULL AS [Cód da Filial],  
  NULL AS [Cód do Centro de Custo],  
  NULL AS [Data de Nascimento],  
  'J' AS   [Payee Party Type],   
  '0' AS [Foreign2], -- REPEAT  
  '064' AS [Payee Class],  
  MR_LST.REIN_COMAPANY_NAME AS [Payee Name],  
  MR_LST.CARRIER_CNPJ AS [Payee ID (CPF or CNPJ)],  
  '0' AS [Payee foreign ID], -- DOUBT 
    
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],  
    
   CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)     
  ELSE MR_LST.BANK_BRANCH_NUMBER    
  END ELSE '' END AS [Payee Bank Branch],--BRACH     
       
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)     
  ELSE ''    
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],   
  
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN  
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)     
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER    
  END 
  ELSE '' END AS [Payee Bank Account no.],    
      
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)     
  ELSE ''    
  END ELSE '' END AS [Payee Bank Account Verifier digit],  
  MR_LST.BANK_ACCOUNT_TYPE AS [Payee Bank Account Type],  
  '1' AS [Payee Bank Account Currency],  
  C_RSRV.[PAYMENT ID] AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?  
  '00001' AS [Carrier Code],  
  D_LST.BRANCH_CODE AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED 
	
  CASE WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  C_LST.CO_INSURANCE = 14549 
			AND  C_RSRV.COV_ID=1
			THEN '01005'
			 
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  C_LST.CO_INSURANCE = 14549 
			AND  C_RSRV.COV_ID=1
			 THEN '01011'
			 
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  C_LST.CO_INSURANCE = 14549
			 --AND C_RSRV.IS_RISK_COVERAGE = 'N' 
			 AND  C_RSRV.COV_ID=50017 
			 THEN '01012'
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  C_LST.CO_INSURANCE = 14549
			 --AND C_RSRV.IS_RISK_COVERAGE ='N'
			 AND C_RSRV.COV_ID=50017 
			 THEN '01014'	
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  C_LST.CO_INSURANCE =14549 
			 --AND C_RSRV.IS_RISK_COVERAGE = 'N' 
			 AND C_RSRV.COV_ID=50018 
			 THEN '01017'
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  C_LST.CO_INSURANCE = 14549
			 --AND C_RSRV.IS_RISK_COVERAGE = 'N' 
			 AND C_RSRV.COV_ID=50018 
			 THEN '01019'
				
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  C_LST.CO_INSURANCE = 14548
			 --AND C_RSRV.IS_RISK_COVERAGE = 'N' 
			 AND C_RSRV.COV_ID=50021 
			 THEN '01080'
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  C_LST.CO_INSURANCE =14548
			 --AND C_RSRV.IS_RISK_COVERAGE ='N' 
			 AND C_RSRV.COV_ID=50021 
			 THEN '01085'
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  C_LST.CO_INSURANCE = 14548
			 --AND C_RSRV.IS_RISK_COVERAGE ='N' 
			 AND C_RSRV.COV_ID=50019 
			 THEN '01090'
			
		WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  C_LST.CO_INSURANCE = 14548 
			 --AND C_RSRV.IS_RISK_COVERAGE = 'N' 
			 AND C_RSRV.COV_ID =50019 
			 THEN '01095' END 
			 AS [EVENT CODE], -- nEED TO DISCUSS 
			  
  '070' AS [Operation Type], 
  CASE WHEN MR_LST.PAYMENT_METHOD = 11973 THEN  '001' 
   ELSE '' END AS [Payment Method], -- aLLOW EFT  
  '' AS [Document number],  
  '' AS [Document number - serial number],  
    
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],  
   
  '1' AS [Policy Currency],  
  '' AS [Exchange rate],  
  CASE WHEN  CHARINDEX('-',CONVERT(VARCHAR(MAX),ISNULL(C_RSRV.PAYMENT_AMOUNT,'')))> 0 THEN '-'    
        ELSE '' END AS [Co amount sign],  
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Co amount], -- TOTAL_DUE  
  NULL AS [Sinal do Valor Isento IR],  
  NULL AS [Valor Isento IR],  
  NULL AS [Sinal do Valor Tributável IR],  
  NULL AS [Valor Tributável IR],  
  NULL AS [Sinal do Valor Isento ISS],  
  NULL AS [Valor Isento ISS],  
  NULL AS [Sinal do Valor Tributável ISS],  
  NULL AS [Valor Tributável ISS],  
  NULL AS [Sinal do Valor Isento INSS],  
  NULL AS [Valor Isento INSS],  
  NULL AS [Sinal do Valor Tributável INSS],  
  NULL AS [Valor Tributável INSS],  
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],  
  NULL AS [Valor Isento CSLL/COFINS/PIS],  
  NULL AS [Sinal do Valor Tributável],  
  NULL AS [Valor Tributável CSLL/COFINS/PIS],  
  NULL AS [Sinal do Valor IR],  
  NULL AS [Valor IR],  
  NULL AS [Sinal do Valor ISS],  
  NULL AS [Valor ISS],  
  NULL AS [Sinal do Valor Desconto],  
  NULL AS [Valor Desconto],  
  NULL AS [Sinal do Valor Multa],  
  NULL AS [Valor Multa],  
  '' AS [Payment Description], --NEED TO DISCUSS  
  D_LST.BRANCH_CODE AS [Policy Branch Code], -- NOT CLEAR  
  '' AS [Profit Center code], --Profit Center code NOT CLEAR  
  NULL AS [A_DEFINIR1],  
  NULL AS [A_DEFINIR2],  
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],  
  NULL AS [A_DEFINIR3], --     
  NULL AS [Apólice],         
  NULL AS [Apólice (cont)],    
  NULL AS [Endosso],     
  NULL [Parcela],        
  NULL AS [A_DEFINIR4],   
  NULL AS [A_DEFINIR5],  
  NULL AS [Data de quitação da parcela],  
  NULL AS [Tomador/ Descrição],   
  NULL AS [Prêmio],   
  NULL AS [% Comissão],  
  NULL AS [A_DEFINIR6],  
  NULL AS [A_DEFINIR7],  
  NULL AS [A_DEFINIR8],  
  NULL AS  [Tipo de Movimento],  
  NULL AS [Data do Pagamento],  
  NULL AS [Sinal do Valor Pago],  
  NULL AS [Valor Pago],  
  NULL AS [Moeda do pagamento],  
  NULL AS [Código do Banco],  
  NULL AS [No da Agência],  
  NULL AS [No da Conta Corrente],  
  NULL AS [No do Cheque],  
  NULL AS [Sinal do Valor IR_2],  
  NULL AS [Valor IR_2],  
  NULL AS [Sinal do Valor ISS_2],  
  NULL AS [Valor ISS_2],  
  NULL AS [Sinal do Valor INSS],  
  NULL AS [Valor INSS],  
  NULL AS [Sinal do Valor CSLL],  
  NULL AS [Valor CSLL],  
  NULL AS [Sinal do Valor COFINS],  
  NULL AS [Valor COFINS],  
  NULL AS [Sinal do Valor PIS],  
  NULL AS [Valor PIS],  
  NULL AS [Occurrence code],  
  NULL AS [Cheque cancellation reason],  
  NULL AS [Payment method_2],  
  NULL AS [Carrier Bank Number],  
  NULL AS [Carrier Bank Branch number],  
  NULL AS [Carrier Bank Account number],  
  NULL AS [Exchange rate2]  
  

  FROM  (SELECT SUM(CACRB.PAYMENT_AMT) AS PAYMENT_AMOUNT,
CAR.CLAIM_ID,CAR.ACTIVITY_ID
				--,COV_EXT.cov_id AS cov_id
				,CASE WHEN  CAR.COVERAGE_ID < 50000	THEN 1
				ELSE CASE WHEN CAR.COVERAGE_ID = 50017 THEN 50017
							 WHEN CAR.COVERAGE_ID = 50018 THEN 50018
							 WHEN  CAR.COVERAGE_ID = 50019 THEN 50019
							 WHEN  CAR.COVERAGE_ID = 50021 THEN 50021
							 ELSE 2 END							  
							 END AS COV_ID
				,CAST(CAR.CLAIM_ID as varchar) + CAST(CAR.ACTIVITY_ID as varchar) AS [PAYMENT ID] 
		FROM  CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)
		INNER JOIN CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB 
				ON CAR.CLAIM_ID= CACRB.CLAIM_ID 
					AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID 
					AND CAR.RESERVE_ID = CACRB.RESERVE_ID
		--WHERE  ISNULL(CACRB.IS_COMMISSION_PROCESS,'N')<>'Y' 
	              			   
	    GROUP BY CAR.CLAIM_ID,CAR.ACTIVITY_ID 
	    ,CASE WHEN  CAR.COVERAGE_ID <50000	THEN 1
				ELSE CASE WHEN CAR.COVERAGE_ID = 50017 THEN 50017 --Loss expense
							 WHEN CAR.COVERAGE_ID = 50018 THEN 50018 --Professional
							 WHEN  CAR.COVERAGE_ID = 50019 THEN 50019--salvage
							 WHEN  CAR.COVERAGE_ID = 50021 THEN 50021--subrogation
							 ELSE 2 END 							
							 END)   AS C_RSRV			 	
   				 	
				 	
INNER JOIN CLM_PARTIES C_PT WITH(NOLOCK)
  ON C_RSRV.CLAIM_ID = C_PT.CLAIM_ID
INNER JOIN MNT_REIN_COMAPANY_LIST MR_LST
  ON MR_LST.REIN_COMAPANY_ID = C_PT.SOURCE_PARTY_ID    --JOIN ON COMPANY ID    	 
INNER JOIN CLM_PAYEE C_P  WITH(NOLOCK)  
  ON C_RSRV.CLAIM_ID = C_P.CLAIM_ID   
  AND  C_P.ACTIVITY_ID = C_RSRV.ACTIVITY_ID 
  AND C_P.PARTY_ID = C_PT.PARTY_ID 
INNER JOIN CLM_ACTIVITY C_ACT WITH(NOLOCK)
  ON C_ACT.CLAIM_ID =  C_RSRV.CLAIM_ID
  AND C_ACT.ACTIVITY_ID =  C_RSRV.ACTIVITY_ID
INNER JOIN CLM_CLAIM_INFO C_INFO WITH(NOLOCK)   
  ON C_INFO.CLAIM_ID = C_PT.CLAIM_ID    
INNER JOIN  POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)   
  ON C_LST.CUSTOMER_ID = C_INFO.CUSTOMER_ID    
  AND C_LST.POLICY_ID = C_INFO.POLICY_ID    
  AND C_LST.POLICY_VERSION_ID = C_INFO.POLICY_VERSION_ID 
  AND C_LST.CO_INSURANCE IS NOT NULL
INNER JOIN  mnt_div_list D_LST   WITH(NOLOCK) 
  ON D_LST.DIV_ID = C_LST.DIV_ID    
    
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)       
  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB
      
 WHERE   C_LST.CO_INSURANCE IN (14548,14549) -- FOLLOWER
  AND C_RSRV.COV_ID <> 2
 
  
  ) AS RI_CLAIM
  
  --WHERE [EVENT CODE] IS NOT NULL
  --AND ISNULL([Co amount],0)<>0
  --ORDER BY [EVENT CODE]
    
--END 



GO

