IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_PAGNET_RefundData]'))
DROP VIEW [dbo].[VW_PAGNET_RefundData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW VW_PAGNET_RefundData
AS
SELECT * FROM 
(  
SELECT    
'4' AS [Interface code],   
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID ) AS [Sequence of record],      
CASE WHEN ISNULL (CA_LST.APPLICANT_TYPE,'11110')='11110' THEN 'F'      
WHEN ISNULL(CA_LST.APPLICANT_TYPE,'11110') ='11109' THEN 'J'
WHEN ISNULL(CA_LST.APPLICANT_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],      
'0' AS [FOREIGN],      
'064' AS [Beneficiary Class],
 RTRIM(FIRST_NAME+' '+ CASE WHEN  len(ISNULL(MIDDLE_NAME,''))<1 THEN ''						
		ELSE MIDDLE_NAME +' ' END  +  ISNULL(LAST_NAME,'')) AS [Beneficiary name],      
 CA_LST.CPF_CNPJ AS [Beneficiary ID],      
 '' AS [Beneficiary foreign ID],      
 CA_LST.ADDRESS1  AS [Beneficiary Address (street name)],      
 CA_LST.NUMBER AS [Beneficiary Address (number)],      
 CA_LST.ADDRESS2 AS [Beneficiary Address (complement)],      
 CA_LST.DISTRICT AS [Beneficiary Address (district)],      
 MCSL.STATE_CODE AS [Beneficiary Address (state)],      
 CA_LST.CITY AS [Beneficiary Address (city)],      
 REPLACE(CA_LST.ZIP_CODE,'-','') AS [Beneficiary Address (zip code)],      
 CA_LST.EMAIL AS [Beneficiary e-mail address],      
  
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CA_LST.BANK_NUMBER 
  -- ELSE '' END
  --AS [Beneficiary Bank Number],
  NULL AS [Beneficiary Bank Number],
  
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CASE WHEN CHARINDEX('-' ,CA_LST.BANK_BRANCH) > 0 THEN SUBSTRING(CA_LST.BANK_BRANCH,1,CHARINDEX('-' ,CA_LST.BANK_BRANCH)-1)     
  --ELSE CA_LST.BANK_BRANCH    
  --END ELSE '' END AS [Beneficiary Bank Branch],--BRACH         
   NULL AS [Beneficiary Bank Branch],
      
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CASE WHEN CHARINDEX('-' ,CA_LST.BANK_BRANCH) > 0 THEN SUBSTRING(CA_LST.BANK_BRANCH,CHARINDEX('-' ,CA_LST.BANK_BRANCH)+1,1)     
  --ELSE ''    
  --END ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],  
   NULL AS [Beneficiary Bank Branch Verifier Digit],    
      
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CASE WHEN CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(CA_LST.ACCOUNT_NUMBER,1,CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER)-1)     
  --ELSE CA_LST.ACCOUNT_NUMBER    
  --END ELSE '' END AS [Beneficiary Bank Account number],   
   NULL  AS [Beneficiary Bank Account number],
  
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CASE WHEN CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(CA_LST.ACCOUNT_NUMBER,CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER)+1,2)     
  --ELSE '' END
  --ELSE '' END  AS [Beneficiary Bank Account Verifier Digit],     
   NULL  AS [Beneficiary Bank Account Verifier Digit],
  
  CASE WHEN  CA_LST.ACCOUNT_TYPE = 14703 THEN  '01'
	   WHEN  CA_LST.ACCOUNT_TYPE = 14704 THEN  '02'
	   ELSE ''
	   END AS [Beneficiary Bank Account type],--      
 '1' AS [Beneficiary Bank Account Currency],      
  NULL AS [Cód Tributação IRRF],      
  NULL AS [Natureza do Rendimento],      
  NULL AS [Calcula ISS ?],      
  NULL AS [Calcula INSS ?],      
  NULL AS [Cód Tributação INSS],      
  NULL AS [Cód Tributação CSLL],      
  NULL AS [Cód Tributação COFINS],      
  NULL AS [Cód Tributação PIS],      
  NULL AS [No de Dependentes],      
  NULL AS [No PIS],      
  NULL AS [Inscrição Municipal],      
  NULL AS [Número interno do corretor],      
  NULL AS [CBO (Classific Brasileira Ocupação)],      
  NULL AS [Código SUSEP],      
  NULL AS [No do Funcionário],      
  NULL AS [Cód da Filial],      
  NULL AS [Cód do Centro de Custo], 
 REPLACE(CONVERT(VARCHAR,CA_LST.CO_APPL_DOB , 111),'/','') AS [DATE OF BIRTH], 
      
  CASE WHEN ISNULL (CA_LST.APPLICANT_TYPE,'11110')='11110' THEN 'F'      
  WHEN ISNULL(CA_LST.APPLICANT_TYPE,'11110') ='11109' THEN 'J'     
  WHEN ISNULL(CA_LST.APPLICANT_TYPE,'11110') ='14725' THEN 'G'  END AS   [Payee Party Type], -- nOTE --1 NOT CLEAR      
  '0' AS [Foreign2], -- REPEAT      
  '064' AS [Payee Class],
  RTRIM(FIRST_NAME+' '+ CASE WHEN  len(ISNULL(MIDDLE_NAME,''))<1 THEN ''						
		ELSE MIDDLE_NAME +' ' END  +  ISNULL(LAST_NAME,'')) AS [Payee Name],     
  CA_LST.CPF_CNPJ AS [Payee ID (CPF or CNPJ)],      
  '' AS [Payee foreign ID],
  
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN        
  --CA_LST.BANK_NUMBER 
  --ELSE '' END AS [Payee Bank Number],
  NULL AS [Payee Bank Number],
  
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN   
  --CASE WHEN CHARINDEX('-' ,CA_LST.BANK_BRANCH) > 0 THEN SUBSTRING(CA_LST.BANK_BRANCH,1,CHARINDEX('-' ,CA_LST.BANK_BRANCH)-1)     
  --ELSE CA_LST.BANK_BRANCH    
  --END
  --ELSE '' END AS [Payee Bank Branch],--BRACH       
   NULL AS [Payee Bank Branch],
      
  -- CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN     
  --CASE WHEN CHARINDEX('-' ,CA_LST.BANK_BRANCH) > 0 THEN SUBSTRING(CA_LST.BANK_BRANCH,CHARINDEX('-' ,CA_LST.BANK_BRANCH)+1,1)     
  --ELSE '' END
  --ELSE ''END AS [Payee Bank Branch Verifier Digit],      
  NULL AS [Payee Bank Branch Verifier Digit],
     
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN      
  --CASE WHEN CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(CA_LST.ACCOUNT_NUMBER,1,CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER)-1)     
  --ELSE CA_LST.ACCOUNT_NUMBER    
  --END 
  --ELSE '' END AS [Payee Bank Account no.],    
  NULL AS [Payee Bank Account no.], 
   
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN
  --CASE WHEN CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER) > 0 THEN SUBSTRING(CA_LST.ACCOUNT_NUMBER,CHARINDEX('-' ,CA_LST.ACCOUNT_NUMBER)+1,2)     
  --ELSE ''    
  --END
  --ELSE '' END AS [Payee Bank Account Verifier digit],  
   NULL AS [Payee Bank Account Verifier digit],    
    
  CASE WHEN  CA_LST.ACCOUNT_TYPE = 14703 THEN  '01'
	   WHEN  CA_LST.ACCOUNT_TYPE = 14704 THEN  '02'
	   ELSE ''
	   END AS [Payee Bank Account Type],    
  
  '1' AS [Payee Bank Account Currency],      
  O_ITEM.IDEN_ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?      
  '00001' AS [Carrier Code],  
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    
  --  AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED
  '00030' AS [Carrier Policy Branch Code],
  CASE WHEN  O_ITEM.ITEM_TRAN_CODE = 'REFP' THEN '01060' --for a policy/ endorsement/ boleto cancelled 
		WHEN O_ITEM.ITEM_TRAN_CODE  IN ( 'CANP', 'CANC', 'CANI', 'CANF', 'CANT',
										'RENP','RENC','RENI','RENF','RENT','NBSP'
										,'NBSC','NBSI','NBSF','NBST') THEN '01020' -- ENDORSEMENT /CANCELATION 
  END AS [EVENT CODE],         
  '008' AS [Operation Type],        
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN '001'
  --     --WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11972 THEN '005'
  --     ELSE '005'  END AS [Payment Method], -- aLLOW EFT PUT CONDITION  
  '005'   AS [Payment Method],     
  P_ENDOR.ENDORSEMENT_NO AS [Document number],      
  '' AS [Série da Nota Fiscal],      
      
  REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],    
       
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date],     
     
 '1' AS [Policy Currency],      
 NULL AS [Exchange rate],        
 --CASE WHEN  O_ITEM.TOTAL_DUE < 0 THEN '-'    
 --        ELSE '' END AS [Refund amount sign],    
  NULL AS [Refund amount sign], 
  ISNULL(O_ITEM.TOTAL_DUE,0) * -1  AS [Refund amount], --which amount      
  NULL AS [Sinal do Valor Isento IR],      
  ABS(O_ITEM.TOTAL_DUE) AS [Valor Isento IR],      
  NULL AS [Sinal do Valor Tributável IR],      
  NULL AS [Valor Tributável IR],      
  NULL AS [Sinal do Valor Isento ISS],      
  ABS(O_ITEM.TOTAL_DUE) AS [Valor Isento ISS],      
  NULL AS [Sinal do Valor Tributável ISS],      
  NULL AS [Valor Tributável ISS],      
  NULL AS [Sinal do Valor Isento INSS],      
  ABS(O_ITEM.TOTAL_DUE) AS [Valor Isento INSS],      
  NULL AS [Sinal do Valor Tributável INSS],      
  NULL AS [Valor Tributável INSS],      
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],      
  ABS(O_ITEM.TOTAL_DUE) AS [Valor Isento CSLL/COFINS/PIS],      
  NULL AS [Sinal do Valor Tributável CSLL/COFINS/PIS],      
  NULL AS [Valor Tributável CSLL/COFINS/PIS],      
  NULL AS [Sinal do Valor IR],      
  NULL AS [Valor IR],      
  NULL AS [Sinal do Valor ISS],      
  NULL AS [Valor ISS],      
  NULL AS [Sinal do Valor Desconto],      
  NULL AS [Valor Desconto],      
  NULL AS [Sinal do Valor Multa],      
  NULL AS [Valor Multa],
  'Refund Premium to Insured' AS [REFUND PAYMENT DESCRIPTION],
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END      
    AS [Policy Branch Code],      
   '' AS [Profit Center code],      
   NULL AS [A_DEFINIR1],      
   NULL AS [A_DEFINIR2],      
   L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],      
   NULL AS [A_DEFINIR3],      
   NULL AS [Apólice],      
   NULL AS [Apólice (cont)],      
   P_ENDOR.ENDORSEMENT_NO AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?      
    NULL AS [Parcela],              NULL AS [A_DEFINIR4],      
    NULL AS [A_DEFINIR5],      
    NULL AS [Data de quitação da parcela],      
    NULL AS [Tomador/ Descrição],      
    NULL AS [Prêmio],      
    NULL AS [% Comissão],      
    NULL AS [A_DEFINIR6],      
    NULL AS [A_DEFINIR7],      
    NULL AS [A_DEFINIR8],      
    'P' AS [Tipo de Movimento],      
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
    NULL AS [Cod Ocorrencia],      
    NULL AS [Motivo do Cancelamento do Cheque],      
    NULL AS [Forma de Pagamento],      
    NULL AS [Código do Banco Pagador],      
    NULL AS [No da Agência_2],      
    NULL AS [No da Conta Corrente_2],      
    NULL AS [Taxa conversão]        
       
FROM ACT_CUSTOMER_OPEN_ITEMS O_ITEM WITH(NOLOCK)    
INNER JOIN POL_APPLICANT_LIST CP_LST  WITH(NOLOCK)      
  ON CP_LST.CUSTOMER_ID = O_ITEM.CUSTOMER_ID  
  AND CP_LST.POLICY_ID = O_ITEM.POLICY_ID  
  AND CP_LST.POLICY_VERSION_ID = O_ITEM.POLICY_VERSION_ID  
  AND CP_LST.IS_PRIMARY_APPLICANT = 1  
        
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)    
  ON C_LST.CUSTOMER_ID = O_ITEM.CUSTOMER_ID    
   AND C_LST.POLICY_ID = O_ITEM.POLICY_ID    
   AND C_LST.POLICY_VERSION_ID  = O_ITEM.POLICY_VERSION_ID    
INNER JOIN CLT_APPLICANT_LIST CA_LST  WITH(NOLOCK)
ON CA_LST.APPLICANT_ID = CP_LST.APPLICANT_ID    
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)   
  ON P_ENDOR.CUSTOMER_ID = O_ITEM.CUSTOMER_ID    
  AND P_ENDOR.POLICY_ID = O_ITEM.POLICY_ID    
  AND P_ENDOR.POLICY_VERSION_ID = O_ITEM.POLICY_VERSION_ID    
INNER JOIN MNT_DIV_LIST D_LST   WITH(NOLOCK)  
  ON D_LST.DIV_ID = C_LST.DIV_ID 
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)   
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB    
--INNER JOIN ACT_POLICY_INSTALL_PLAN_DATA I_DATA  WITH(NOLOCK)   
--    ON I_DATA.CUSTOMER_ID = C_LST.CUSTOMER_ID    
--     AND I_DATA.POLICY_ID = C_LST.POLICY_ID    
--     AND I_DATA.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID 
  INNER JOIN MNT_COUNTRY_STATE_LIST MCSL
   ON MCSL.STATE_ID = CAST(CA_LST.[STATE] AS SMALLINT)   
 WHERE  ISNULL(O_ITEM.IS_COMMISSION_PROCESS,'N') <>'Y'
  AND O_ITEM.UPDATED_FROM IN ('P','D') 
  AND O_ITEM.ITEM_STATUS ='RP'    
  AND O_ITEM.ITEM_TRAN_CODE IN ( 'CANP', 'CANC', 'CANI', 'CANF', 'CANT',
								  'RENP','RENC','RENI','RENF','RENT','NBSP',
								  'NBSC','NBSI','NBSF','NBST')
								  
 AND O_ITEM.TOTAL_DUE < 0
) REFUND WHERE [EVENT CODE] IS NOT NULL
GO

