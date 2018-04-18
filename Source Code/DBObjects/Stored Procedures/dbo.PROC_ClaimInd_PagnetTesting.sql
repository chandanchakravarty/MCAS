IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ClaimInd_PagnetTesting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ClaimInd_PagnetTesting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC PROC_ClaimInd_PagnetTesting
AS          
BEGIN  

SELECT * INTO #TEMP_TABLE  FROM
(SELECT ROW_NUMBER() OVER (ORDER BY [Sequence of record_0]) AS [Sequence of record],*
 FROM     
(SELECT '03' AS [Interface code],
C_RSRV.CLAIM_ID AS CLAIM_ID,
C_RSRV.ACTIVITY_ID AS ACTIVITY_ID,
--ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence of record],  
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID,C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID) AS [Sequence of record_0],     
 CASE WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='11110' THEN 'F'       
     WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='11109' THEN 'J'
     WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],      
 '0' AS [FOREIGN],      
 CASE WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='14725' THEN '032'
            ELSE '064' END AS [Beneficiary Class],       
 C_PT.NAME AS [Beneficiary name],      
 C_PT.PARTY_CPF_CNPJ AS [Beneficiary ID],      
 '' AS [Beneficiary foreign ID],      
 C_P.ADDRESS1  AS [Beneficiary Address (street name)],      
 C_PT.NUMBER AS [Beneficiary Address (number)],      
 C_P.ADDRESS2 AS [Beneficiary Address (complement)],      
 C_PT.DISTRICT AS [Beneficiary Address (district)],      
 MCSL.STATE_CODE AS [Beneficiary Address (state)],      
 C_P.CITY  AS [Beneficiary Address (city)],      
 REPLACE(C_P.ZIP,'-','') AS [Beneficiary Address (zip code)],      
 C_PT.CONTACT_EMAIL AS [Beneficiary e-mail address],
 CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN       
 C_PT.BANK_NUMBER 
 ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION      
 
 CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN       
 CASE WHEN CHARINDEX('-' ,C_PT.BANK_BRANCH) > 0 THEN SUBSTRING(C_PT.BANK_BRANCH,1,CHARINDEX('-' ,C_PT.BANK_BRANCH)-1)         
  ELSE C_PT.BANK_BRANCH        
  END ELSE '' END  AS [Beneficiary Bank Branch],--BRACH         
  
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN            
  CASE WHEN CHARINDEX('-' ,C_PT.BANK_BRANCH) > 0 THEN SUBSTRING(C_PT.BANK_BRANCH,CHARINDEX('-' ,C_PT.BANK_BRANCH)+1,1)         
  ELSE ''        
  END ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],       
        
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(C_PT.ACCOUNT_NUMBER ,1,CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER)-1)         
  ELSE C_PT.ACCOUNT_NUMBER        
  END ELSE '' END AS [Beneficiary Bank Account number],        
          
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(C_PT.ACCOUNT_NUMBER,CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER)+1,2)         
  ELSE ''        
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],      
  CASE WHEN C_PT.ACCOUNT_TYPE = 14703 THEN '01'
	   WHEN C_PT.ACCOUNT_TYPE = 14704 THEN '02'
	   ELSE '' END AS [Beneficiary Bank Account type],--      
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
  CASE WHEN ISNULL (C_PT.PARTY_TYPE,'11110')='11110' THEN 'F'          
  WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='11109' THEN 'J'
  WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='14725' THEN 'G'         
  END AS   [Payee Party Type],       
  '0' AS [Foreign2], -- REPEAT      
 CASE WHEN ISNULL(C_PT.PARTY_TYPE,'11110') ='14725' THEN '032'
            ELSE '064' END AS [Payee Class],      
  C_PT.NAME AS [Payee Name],      
  C_PT.PARTY_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],      
  '' AS [Payee foreign ID], -- DOUBT 
  
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN         
  C_PT.BANK_NUMBER ELSE '' END AS [Payee Bank Number],        
  --C_PT.BANK_NUMBER AS [Payee Bank Number],
      
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.BANK_BRANCH) > 0 THEN SUBSTRING(C_PT.BANK_BRANCH,1,CHARINDEX('-' ,C_PT.BANK_BRANCH)-1)         
  ELSE C_PT.BANK_BRANCH        
  END ELSE '' END AS [Payee Bank Branch],--BRACH         
           
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.BANK_BRANCH) > 0 THEN SUBSTRING(C_PT.BANK_BRANCH,CHARINDEX('-' ,C_PT.BANK_BRANCH)+1,1)         
  ELSE ''        
  END ELSE '' END  AS [Payee Bank Branch Verifier Digit],       
        
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(C_PT.ACCOUNT_NUMBER,1,CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER)-1)         
  ELSE C_PT.ACCOUNT_NUMBER        
  END ELSE '' END AS [Payee Bank Account no.],        
          
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN   
  CASE WHEN CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(C_PT.ACCOUNT_NUMBER,CHARINDEX('-' ,C_PT.ACCOUNT_NUMBER)+1,2)         
  ELSE ''        
  END ELSE '' END AS [Payee Bank Account Verifier digit],      
  CASE WHEN C_PT.ACCOUNT_TYPE = 14703 THEN '01'
	   WHEN C_PT.ACCOUNT_TYPE = 14704 THEN '02'
	   ELSE '' END AS [Payee Bank Account Type],      
  '1' AS [Payee Bank Account Currency],      
  C_RSRV.[PAYMENT ID] AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?      
  '00001' AS [Carrier Code],
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END      
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED
  MUL.ADJUSTER_CODE AS [Carrier Policy Branch Code],   
  CASE WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'Y'  THEN '01010'
	   WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'N'  THEN '01000'
  ELSE '' END  AS [EVENT CODE], --NOT CLEAR
  CASE WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'Y'  THEN '130'
	   WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'N'  THEN '120'
  ELSE '' END AS [Operation Type],
  CASE WHEN C_P.PAYMENT_METHOD IN (14707,14709) THEN  '001'
	   WHEN C_P.PAYMENT_METHOD IN (14708,147011,14710) THEN  '005'
	   WHEN C_P.PAYMENT_METHOD = 14712 THEN  '006'	
	   ELSE '005' END AS [Payment Method],           
  C_P.INVOICE_NUMBER AS [Document number],      
  '' AS [Document number - serial number],
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],  
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date], 
  '1' AS [Policy Currency],      
  '' AS [Exchange rate],      
  --CASE WHEN  CHARINDEX('-',CONVERT(VARCHAR(MAX),ISNULL(C_RSRV.PAYMENT_AMOUNT,'')))> 0 THEN '-'        
  --      ELSE '' END AS [Commission amount sign],    
  NULL AS [Commission amount sign],   
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Commission amount], -- TOTAL_DUE      
  NULL AS [Sinal do Valor Isento IR],      
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Valor Isento IR],      
  NULL AS [Sinal do Valor Tributável IR],      
  NULL AS [Valor Tributável IR],      
  NULL AS [Sinal do Valor Isento ISS],      
 ABS(C_RSRV.PAYMENT_AMOUNT) AS [Valor Isento ISS],      
  NULL AS [Sinal do Valor Tributável ISS],      
  NULL AS [Valor Tributável ISS],      
  NULL AS [Sinal do Valor Isento INSS],      
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Valor Isento INSS],      
  NULL AS [Sinal do Valor Tributável INSS],      
  NULL AS [Valor Tributável INSS],      
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],      
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Valor Isento CSLL/COFINS/PIS],      
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
  --CASE WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'Y'  THEN 'indenização por direito legal para o líder e políticas directo'  --'01010'
	 --  WHEN     ISNULL(C_ACT.IS_LEGAL,'N') = 'N'  THEN 'indenização por um crédito normal para o líder e políticas directo' --'01000'
  --ELSE '' END  AS [Expense payment description],    
  'Pedido de pagamento de Indenização' AS [Expense payment description], --NEED TO DISCUSS 
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END     
   AS [Policy Branch Code], -- NOT CLEAR      
  '' AS [Profit Center code], --Profit Center code NOT CLEAR      
  NULL AS [A_DEFINIR1],      
  NULL AS [A_DEFINIR2],      
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],      
  NULL AS [A_DEFINIR3], -- 
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)   
  ELSE '' END AS [Apólice], 
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)   
  ELSE '' END AS [Apólice (cont)],       
  P_ENDOR.ENDORSEMENT_NO AS [Endosso], --P_ENDOR.ENDORSEMENT_NO 
  NULL AS [Parcela], 
  NULL AS [A_DEFINIR4],       
  NULL AS [A_DEFINIR5],      
  NULL AS [Data de quitação da parcela],
  NULL AS [Tomador/ Descrição],          
  NULL AS [Prêmio],      
  NULL AS [% Comissão],      
  NULL AS [A_DEFINIR6],      
  NULL AS [A_DEFINIR7],      
  NULL AS [A_DEFINIR8],      
  'P' AS  [Tipo de Movimento],      
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
  NULL AS [Exchange rate2],
  NULL AS INCONSISTENCY_1,
  NULL AS INCONSISTENCY_2,
  NULL AS INCONSISTENCY_3,
  NULL AS INCONSISTENCY_4,
  NULL AS INCONSISTENCY_5
  FROM  (SELECT SUM(PAYMENT_AMOUNT) AS PAYMENT_AMOUNT,CLM_ACTIVITY_RESERVE.CLAIM_ID,ACTIVITY_ID,				
				CAST(CLM_ACTIVITY_RESERVE.CLAIM_ID as varchar) +'sep'+ CAST(ACTIVITY_ID as varchar)
				 AS [PAYMENT ID] FROM  CLM_ACTIVITY_RESERVE  WITH(NOLOCK)
				 INNER JOIN CLM_PRODUCT_COVERAGES WITH(NOLOCK)
				 ON CLM_ACTIVITY_RESERVE.CLAIM_ID = CLM_PRODUCT_COVERAGES.CLAIM_ID				 
		 WHERE  CLM_PRODUCT_COVERAGES.IS_RISK_COVERAGE = 'Y'AND
				 CLM_PRODUCT_COVERAGES.CLAIM_COV_ID = CLM_ACTIVITY_RESERVE.COVERAGE_ID
				  --AND ISNULL(CLM_ACTIVITY_RESERVE.IS_COMMISSION_PROCESS,'N')<>'Y' 
		GROUP BY CLM_ACTIVITY_RESERVE.CLAIM_ID,ACTIVITY_ID) 	AS C_RSRV
INNER JOIN CLM_PARTIES C_PT WITH(NOLOCK)
  ON C_RSRV.CLAIM_ID = C_PT.CLAIM_ID  	 
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
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)   
  ON P_ENDOR.CUSTOMER_ID = C_LST.CUSTOMER_ID    
  AND P_ENDOR.POLICY_ID = C_LST.POLICY_ID    
  AND P_ENDOR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID  
INNER JOIN  mnt_div_list D_LST   WITH(NOLOCK) 
  ON D_LST.DIV_ID = C_LST.DIV_ID
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)       
  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL
  ON C_P.[STATE]  = MCSL.STATE_ID  
  AND C_P.COUNTRY  = MCSL.COUNTRY_ID
INNER JOIN CLM_ADJUSTER CA WITH(NOLOCK)
  ON  CA.ADJUSTER_ID = C_INFO.ADJUSTER_ID
INNER JOIN MNT_USER_LIST MUL WITH(NOLOCK)
  ON MUL.USER_ID = CA.USER_ID
  
  --CHANGES AS PER ITRACK-1145 
 --WHERE C_LST.CO_INSURANCE NOT IN (14548,14549)--WITHOUT COINSURANCE
 WHERE C_LST.CO_INSURANCE NOT IN (14549)
) AS CLAIM_Ind
  
  WHERE [EVENT CODE] IS NOT NULL
  AND ISNULL([Commission amount],0) > 0
  --ORDER BY [EVENT CODE]      
 --WHERE C_ACT.IS_LEGAL IS NOT NULL
 ) TABLE_2
 
 -----------------------PAGNET_EXPORT_TABLE-----------------
 INSERT into PAGNET_EXPORT_RECORD
    (
		INTERFACE_CODE	
		,SEQUENCE_OF_RECORD	
		,BENEFICIARY_TYPE	
		,[FOREIGN]	
		,BENEFICIARY_CLASS
		,BENEFICIARY_NAME
		,BENEFICIARY_ID	
		,BENEFICIARY_FOREIGN_ID	
		,BENEFICIARY_ADDRESS_STREET_NAME	
		,BENEFICIARY_ADDRESS_NUMBER
		,BENEFICIARY_ADDRESS_COMPLEMENT
		,BENEFICIARY_ADDRESS_DISTRICT	
		,BENEFICIARY_ADDRESS_STATE	
		,BENEFICIARY_ADDRESS_CITY	
		,BENEFICIARY_ADDRESS_ZIP_CODE	
		,BENEFICIARY_EMAIL_ADDRESS	
		,BENEFICIARY_BANK_NUMBER	
		,BENEFICIARY_BANK_BRANCH	
		,BENEFICIARY_BANK_BRANCH_VERIFIER_DIGIT
		--,BENEFICIARY_BANK_ACCOUNT_NUMBER	
		,BENEFICIARY_BANK_ACCOUNT_VERIFIER_DIGIT
		,BENEFICIARY_BANK_ACCOUNT_TYPE	
		,BENEFICIARY_BANK_ACCOUNT_CURRENCY
		,CÓD_TRIBUTAÇÃO_IRRF	
		,NATUREZA_DO_RENDIMENTO	
		,[CALCULA_ISS_?]	
		,[CALCULA_INSS_?]	
		,CÓD_TRIBUTAÇÃO_INSS	
		,CÓD_TRIBUTAÇÃO_CSLL	
		,CÓD_TRIBUTAÇÃO_COFINS	
		,CÓD_TRIBUTAÇÃO_PIS	
		,NO_DE_DEPENDENTES	
		,NO_PIS 
		,INSCRIÇÃO_MUNICIPAL
		,NÚMERO_INTERNO_DO_CORRETOR
		,CBO_CLASSIFIC_BRASILEIRA_OCUPAÇÃO
		,CÓDIGO_SUSEP	
		,NO_DO_FUNCIONÁRIO
		,CÓD_DA_FILIAL	
		,CÓD_DO_CENTRO_DE_CUSTO
		,DATE_OF_BIRTH	
		,PAYEE_PARTY_TYPE
		,FOREIGN2	
		,PAYEE_CLASS
		,PAYEE_NAME	
		,PAYEE_ID_CPF_CNPJ	
		,PAYEE_FOREIGN_ID	
		,PAYEE_BANK_NUMBER	
		,PAYEE_BANK_BRANCH	
		,PAYEE_BANK_BRANCH_VERIFIER_DIGIT
		--,PAYEE_BANK_ACCOUNT_NO	
		,PAYEE_BANK_ACCOUNT_VERIFIER_DIGIT
		,PAYEE_BANK_ACCOUNT_TYPE	
		,PAYEE_BANK_ACCOUNT_CURRENCY
		,PAYMENT_ID	
		,CARRIER_CODE
		,CARRIER_POLICY_BRANCH_CODE
		,EVENT_CODE	
		,OPERATION_TYPE	
		,PAYMENT_METHOD	
		,DOCUMENT_NUMBER
		,SÉRIE_DA_NOTA_FISCAL
		,INVOICE_ISSUANCE_DATE
		,INVOICE_DUE_DATE	
		,POLICY_CURRENCY	
		,EXCHANGE_RATE	
		,REFUND_AMOUNT_SIGN
		,REFUND_AMOUNT	
		,SINAL_DO_VALOR_ISENTO_IR	
		,VALOR_ISENTO_IR	
		,SINAL_DO_VALOR_TRIBUTÁVEL_IR
		,VALOR_TRIBUTÁVEL_IR	
		,SINAL_DO_VALOR_ISENTO_ISS
		,VALOR_ISENTO_ISS	
		,SINAL_DO_VALOR_TRIBUTÁVEL_ISS
		,VALOR_TRIBUTÁVEL_ISS	
		,SINAL_DO_VALOR_ISENTO_INSS
		,VALOR_ISENTO_INSS	
		,SINAL_DO_VALOR_TRIBUTÁVEL_INSS
		,VALOR_TRIBUTÁVEL_INSS	
		,SINAL_DO_VALOR_ISENTO_CSLL_COFINS_PIS	
		,VALOR_ISENTO_CSLL_COFINS_PIS	
		,SINAL_DO_VALOR_TRIBUTÁVEL_CSLL_COFINS_PIS
		,VALOR_TRIBUTÁVEL_CSLL_COFINS_PIS	
		,SINAL_DO_VALOR_IR	
		,VALOR_IR	
		,SINAL_DO_VALOR_ISS
		,VALOR_ISS	
		,SINAL_DO_VALOR_DESCONTO
		,VALOR_DESCONTO	
		,SINAL_DO_VALOR_MULTA
		,VALOR_MULTA	
		,REFUND_PAYMENT_DESCRIPTION	
		,POLICY_BRANCH_CODE	
		,PROFIT_CENTER_CODE	
		,A_DEFINIR1	
		,A_DEFINIR2	
		,POLICY_ACCOUNTING_LOB
		,A_DEFINIR3	
		,APÓLICE	
		,APÓLICE_CONT
		,ENDORSEMENT_NUMBER
		,PARCELA	
		,A_DEFINIR4	
		,A_DEFINIR5	
		,DATA_DE_QUITAÇÃO_DA_PARCELA
		,TOMADOR_DESCRIÇÃO
		,PRÊMIO		
		,[%_COMISSÃO]		
		,A_DEFINIR6		
		,A_DEFINIR7		
		,A_DEFINIR8		
		,TIPO_DE_MOVIMENTO	
		,DATA_DO_PAGAMENTO		
		,SINAL_DO_VALOR_PAGO		
		,VALOR_PAGO		
		,MOEDA_DO_PAGAMENTO		
		,CÓDIGO_DO_BANCO		
		,NO_DA_AGÊNCIA		
		,NO_DA_CONTA_CORRENTE		
		,NO_DO_CHEQUE		
		,SINAL_DO_VALOR_IR_2		
		,VALOR_IR_2		
		,SINAL_DO_VALOR_ISS_2		
		,VALOR_ISS_2		
		,SINAL_DO_VALOR_INSS		
		,VALOR_INSS		
		,SINAL_DO_VALOR_CSLL		
		,VALOR_CSLL		
		,SINAL_DO_VALOR_COFINS		
		,VALOR_COFINS		
		,SINAL_DO_VALOR_PIS		
		,VALOR_PIS		
		,COD_OCORRENCIA		
		,MOTIVO_DO_CANCELAMENTO_DO_CHEQUE		
		,FORMA_DE_PAGAMENTO		
		,CÓDIGO_DO_BANCO_PAGADOR		
		,NO_DA_AGÊNCIA_2		
		,NO_DA_CONTA_CORRENTE_2		
		,TAXA_CONVERSÃO		
		-- ,FILE_NAMES VARCHAR(500), 
		,CREATE_DATETIME,
		INCONSISTENCY_1,
	  INCONSISTENCY_2,
	 INCONSISTENCY_3,
	 INCONSISTENCY_4,
	  INCONSISTENCY_5,
	  RETURN_STATUS    
    )
    
    
    SELECT      
	 [Interface code]
	,[Sequence of record]
	,[Beneficiary Type]
	,[FOREIGN]
	,[Beneficiary Class]
	,[Beneficiary name]
	,[Beneficiary ID]
	,[Beneficiary foreign ID]
	,[Beneficiary Address (street name)]
	,[Beneficiary Address (number)]
	,[Beneficiary Address (complement)]
	,[Beneficiary Address (district)]
	,[Beneficiary Address (state)]
	,[Beneficiary Address (city)]
	,[Beneficiary Address (zip code)]
	,[Beneficiary e-mail address]
	,[Beneficiary Bank Number]
	,[Beneficiary Bank Branch]
	,[Beneficiary Bank Branch Verifier Digit]
	--,[Beneficiary Bank Account number]
	,[Beneficiary Bank Account Verifier Digit]
	,[Beneficiary Bank Account type]
	,[Beneficiary Bank Account Currency]
	,[Cód Tributação IRRF]
	,[Natureza do Rendimento]
	,[Calcula ISS]      
     ,[Calcula INSS]
	,[Cód Tributação INSS]
	,[Cód Tributação CSLL]
	,[Cód Tributação COFINS]
	,[Cód Tributação PIS]
	,[No de Dependentes]
	,[No PIS]
	,[Inscrição Municipal]
	,[Número interno do corretor]
	,[CBO (Classific Brasileira Ocupação)]
	,[Código SUSEP]
	,[No do Funcionário]
	,[Cód da Filial]
	,[Cód do Centro de Custo]
	,[Data de Nascimento]
	,[Payee Party Type]
	,[Foreign2]
	,[Payee Class]
	,[Payee Name]
	,[Payee ID (CPF or CNPJ)]
	,[Payee foreign ID]
	,[Payee Bank Number]
	,[Payee Bank Branch]
	,[Payee Bank Branch Verifier Digit]
	--,[Payee Bank Account no.]
	,[Payee Bank Account Verifier digit]
	,[Payee Bank Account Type]
	,[Payee Bank Account Currency]
	,[PAYMENT ID]
	,[Carrier Code]
	,[Carrier Policy Branch Code]
	,[EVENT CODE]
	,[Operation Type]
	,[Payment Method]
	,[Document number]
	,[Document number - serial number]
	,[Invoice issuance date]
	,[Invoice due date]
	,[Policy Currency]
	,[Exchange rate]
	,[Commission amount sign]   
	,[Commission amount]
	,[Sinal do Valor Isento IR]
	,[Valor Isento IR]
	,[Sinal do Valor Tributável IR]
	,[Valor Tributável IR]
	,[Sinal do Valor Isento ISS]
	,[Valor Isento ISS]
	,[Sinal do Valor Tributável ISS]
	,[Valor Tributável ISS]
	,[Sinal do Valor Isento INSS]
	,[Valor Isento INSS]
	,[Sinal do Valor Tributável INSS]
	,[Valor Tributável INSS]
	,[Sinal do Valor Isento CSLL/COFINS/PIS]
	,[Valor Isento CSLL/COFINS/PIS]
	,[Sinal do Valor Tributável]
	,[Valor Tributável CSLL/COFINS/PIS]
	,[Sinal do Valor IR]
	,[Valor IR]
	,[Sinal do Valor ISS]
	,[Valor ISS]
	,[Sinal do Valor Desconto]
	,[Valor Desconto]
	,[Sinal do Valor Multa]
	,[Valor Multa]
	,[Expense payment description]
	,[Policy Branch Code]
	,[Profit Center code]
	,[A_DEFINIR1]
	,[A_DEFINIR2]
	,[Policy Accounting LOB]
	,[A_DEFINIR3]
	,[Apólice]
	,[Apólice (cont)]
	,[Endosso]
	,[Parcela]
	,[A_DEFINIR4]
	,[A_DEFINIR5]
	,[Data de quitação da parcela]
	,[Tomador/ Descrição]
	,[Prêmio]
	,[% Comissão]
	,[A_DEFINIR6]
	,[A_DEFINIR7]
	,[A_DEFINIR8]
	,[Tipo de Movimento]
	,[Data do Pagamento]
	,[Sinal do Valor Pago]
	,[Valor Pago]
	,[Moeda do pagamento]
	,[Código do Banco]
	,[No da Agência]
	,[No da Conta Corrente]
	,[No do Cheque]
	,[Sinal do Valor IR_2]
	,[Valor IR_2]
	,[Sinal do Valor ISS_2]
	,[Valor ISS_2]
	,[Sinal do Valor INSS]
	,[Valor INSS]
	,[Sinal do Valor CSLL]
	,[Valor CSLL]
	,[Sinal do Valor COFINS]
	,[Valor COFINS]
	,[Sinal do Valor PIS]
	,[Valor PIS]
	,[Occurrence code]      
  ,[Cheque cancellation reason]      
  ,[Payment method_2]      
  ,[Carrier Bank Number]      
  ,[Carrier Bank Branch number]     
  ,[Carrier Bank Account number]      
  ,[Exchange rate2] 
    
    ,GETDATE(),
		INCONSISTENCY_1,
	  INCONSISTENCY_2,
	 INCONSISTENCY_3,
	 INCONSISTENCY_4,
	  INCONSISTENCY_5,  
	'Record Generated'
     FROM     
    
    
      #TEMP_TABLE
      
     SELECT * FROM #TEMP_TABLE
           
END  


 

GO

