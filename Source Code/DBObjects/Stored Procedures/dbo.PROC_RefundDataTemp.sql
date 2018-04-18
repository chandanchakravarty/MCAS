IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_RefundDataTemp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_RefundDataTemp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc PROC_RefundData      
      
CREATE PROC PROC_RefundDataTemp        
AS              
BEGIN          
         
        
SELECT TOP 10 ROW_NUMBER() OVER (ORDER BY  TABLE_NAME,[Sequence_of_record_0]) AS [Sequence of record],        
 * INTO #TEMP_TABLE FROM         
(          
SELECT            
'4' AS [Interface code],         
'ACT_CUSTOMER_OPEN_ITEMS' AS TABLE_NAME,           
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID ) AS [Sequence_of_record_0],              
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
  '01060' AS [EVENT CODE],        
  /*CASE WHEN  O_ITEM.ITEM_TRAN_CODE = 'REFP' THEN '01060' --for a policy/ endorsement/ boleto cancelled         
  WHEN O_ITEM.ITEM_TRAN_CODE  IN ( 'CANP', 'CANC', 'CANI', 'CANF', 'CANT',        
          'RENP','RENC','RENI','RENF','RENT','NBSP'        
          ,'NBSC','NBSI','NBSF','NBST') THEN '01020' -- ENDORSEMENT /CANCELATION         
  END AS [EVENT CODE],*/                 
  '008' AS [Operation Type],                
  --CASE WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11973 THEN '001'        
  --  --WHEN ISNULL(I_DATA.MODE_OF_PAYMENT,0) = 11972 THEN '005'        
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
  --(ISNULL(O_ITEM.TOTAL_DUE,0) - ISNULL(O_ITEM.TOTAL_PAID,0)) * -1  AS [Refund amount], --which amount         
  ABS(ISNULL(O_ITEM.TOTAL_DUE,0)) AS [Refund amount],             
  NULL AS [Sinal do Valor Isento IR],              
  --(ISNULL(O_ITEM.TOTAL_DUE,0) - ISNULL(O_ITEM.TOTAL_PAID,0)) * -1 AS [Valor Isento IR],              
  ABS(ISNULL(O_ITEM.TOTAL_DUE,0)) AS [Valor Isento IR],              
  NULL AS [Sinal do Valor Tributável IR],              
  NULL AS [Valor Tributável IR],              
  NULL AS [Sinal do Valor Isento ISS],              
  --(ISNULL(O_ITEM.TOTAL_DUE,0) - ISNULL(O_ITEM.TOTAL_PAID,0)) * -1 AS [Valor Isento ISS],              
  ABS(ISNULL(O_ITEM.TOTAL_DUE,0)) AS [Valor Isento ISS],              
  NULL AS [Sinal do Valor Tributável ISS],              
  NULL AS [Valor Tributável ISS],              
  NULL AS [Sinal do Valor Isento INSS],              
  --(ISNULL(O_ITEM.TOTAL_DUE,0) - ISNULL(O_ITEM.TOTAL_PAID,0)) * -1 AS [Valor Isento INSS],           
  ABS(ISNULL(O_ITEM.TOTAL_DUE,0)) AS [Valor Isento INSS],             
  NULL AS [Sinal do Valor Tributável INSS],              
  NULL AS [Valor Tributável INSS],              
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],              
  --(ISNULL(O_ITEM.TOTAL_DUE,0) - ISNULL(O_ITEM.TOTAL_PAID,0)) * -1 AS [Valor Isento CSLL/COFINS/PIS],          
  ABS(ISNULL(O_ITEM.TOTAL_DUE,0)) AS [Valor Isento CSLL/COFINS/PIS],            
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
  'Reembolso ao Segurado Premium' AS [REFUND PAYMENT DESCRIPTION],        
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END              
    AS [Policy Branch Code],              
   '' AS [Profit Center code],              
   NULL AS [A_DEFINIR1],              
   NULL AS [A_DEFINIR2],              
   L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],              
   NULL AS [A_DEFINIR3],         
                
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)           
  ELSE '' END AS [Apólice],         
               
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)           
  ELSE '' END  AS [Apólice (cont)],              
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
    NULL AS [Valor ISS_2],                NULL AS [Sinal do Valor INSS],              
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
    NULL AS [Taxa conversão],        
    NULL AS INCONSISTENCY_1,        
  NULL AS INCONSISTENCY_2,        
  NULL AS INCONSISTENCY_3,        
  NULL AS INCONSISTENCY_4,        
  NULL AS INCONSISTENCY_5                
               
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
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL        
   ON MCSL.STATE_ID = CAST(CA_LST.[STATE] AS SMALLINT)           
 WHERE  --ISNULL(O_ITEM.IS_COMMISSION_PROCESS,'N') <>'Y' AND       
   O_ITEM.UPDATED_FROM = 'D'         
  AND O_ITEM.ITEM_STATUS ='RP'            
  --AND O_ITEM.ITEM_TRAN_CODE IN ( 'CANP', 'CANC', 'CANI', 'CANF', 'CANT',        
  --        'RENP','RENC','RENI','RENF','RENT','NBSP',        
  --        'NBSC','NBSI','NBSF','NBST')        
                  
 --AND (O_ITEM.TOTAL_DUE - O_ITEM.TOTAL_PAID) < 0        
 AND (O_ITEM.TOTAL_DUE) < 0        
         
 UNION        
         
 SELECT            
'4' AS [Interface code],        
'ACT_POLICY_INSTALLMENT_DETAILS' AS TABLE_NAME,           
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID ) AS [Sequence_of_record_0],              
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
  O_ITEM.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?              
  '00001' AS [Carrier Code],        
            
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END            
  --  AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED        
  '00030' AS [Carrier Policy Branch Code],        
          
  '01020' AS [EVENT CODE],        
 /* CASE WHEN  O_ITEM.ITEM_TRAN_CODE = 'REFP' THEN '01060' --for a policy/ endorsement/ boleto cancelled         
  WHEN O_ITEM.ITEM_TRAN_CODE  IN ( 'CANP', 'CANC', 'CANI', 'CANF', 'CANT',        
          'RENP','RENC','RENI','RENF','RENT','NBSP'        
          ,'NBSC','NBSI','NBSF','NBST') THEN '01020' -- ENDORSEMENT /CANCELATION         
  END AS [EVENT CODE], */                
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
  ISNULL(O_ITEM.TOTAL,0) * -1  AS [Refund amount], --which amount              
  NULL AS [Sinal do Valor Isento IR],              
  ABS(O_ITEM.TOTAL) AS [Valor Isento IR],              
  NULL AS [Sinal do Valor Tributável IR],              
  NULL AS [Valor Tributável IR],              
  NULL AS [Sinal do Valor Isento ISS],              
  ABS(O_ITEM.TOTAL) AS [Valor Isento ISS],              
  NULL AS [Sinal do Valor Tributável ISS],              
  NULL AS [Valor Tributável ISS],              
  NULL AS [Sinal do Valor Isento INSS],              
  ABS(O_ITEM.TOTAL) AS [Valor Isento INSS],              
  NULL AS [Sinal do Valor Tributável INSS],              
  NULL AS [Valor Tributável INSS],              
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],              
  ABS(O_ITEM.TOTAL) AS [Valor Isento CSLL/COFINS/PIS],              
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
  'Reembolso ao Segurado Premium' AS [REFUND PAYMENT DESCRIPTION],        
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END              
    AS [Policy Branch Code],              
   '' AS [Profit Center code],              
   NULL AS [A_DEFINIR1],              
   NULL AS [A_DEFINIR2],              
   L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],              
   NULL AS [A_DEFINIR3],         
                
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)           
  ELSE '' END AS [Apólice],         
               
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)           
  ELSE '' END  AS [Apólice (cont)],              
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
    NULL AS [Taxa conversão],        
    NULL AS INCONSISTENCY_1,        
  NULL AS INCONSISTENCY_2,        
  NULL AS INCONSISTENCY_3,        
  NULL AS INCONSISTENCY_4,        
  NULL AS INCONSISTENCY_5                
               
FROM ACT_POLICY_INSTALLMENT_DETAILS O_ITEM WITH(NOLOCK)            
INNER JOIN POL_APPLICANT_LIST CP_LST  WITH(NOLOCK)              
  ON CP_LST.CUSTOMER_ID = O_ITEM.CUSTOMER_ID          
  AND CP_LST.POLICY_ID = O_ITEM.POLICY_ID          
  AND CP_LST.POLICY_VERSION_ID = O_ITEM.POLICY_VERSION_ID          
  --AND CP_LST.IS_PRIMARY_APPLICANT = 1          
                
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
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL        
  ON MCSL.STATE_ID = CAST(CA_LST.[STATE] AS SMALLINT)         
          
  WHERE  --ISNULL(O_ITEM.IS_COMMISSION_PROCESS,'N') <>'Y' AND         
 C_LST.CO_INSURANCE <> 14549 AND O_ITEM.RELEASED_STATUS ='N'        
 AND TOTAL < 0        
         
         
) REFUND WHERE [EVENT CODE] IS NOT NULL        
         
         
    INSERT into [PAGNET_EXPORT_RECORD]        
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
 ,[Calcula ISS ?]        
 ,[Calcula INSS ?]        
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
 ,[DATE OF BIRTH]        
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
 ,[Série da Nota Fiscal]        
 ,[Invoice issuance date]        
 ,[Invoice due date]        
 ,[Policy Currency]        
 ,[Exchange rate]        
 ,[Refund amount sign]        
 ,[Refund amount]        
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
 ,[Sinal do Valor Tributável CSLL/COFINS/PIS]        
 ,[Valor Tributável CSLL/COFINS/PIS]        
 ,[Sinal do Valor IR]        
 ,[Valor IR]        
 ,[Sinal do Valor ISS]        
 ,[Valor ISS]        
 ,[Sinal do Valor Desconto]        
 ,[Valor Desconto]        
 ,[Sinal do Valor Multa]        
 ,[Valor Multa]        
 ,[REFUND PAYMENT DESCRIPTION]        
 ,[Policy Branch Code]        
 ,[Profit Center code]        
 ,[A_DEFINIR1]        
 ,[A_DEFINIR2]        
 ,[Policy Accounting LOB]        
 ,[A_DEFINIR3]        
 ,[Apólice]        
 ,[Apólice (cont)]        
 ,[Endorsement number]        
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
 ,[Cod Ocorrencia]        
 ,[Motivo do Cancelamento do Cheque]        
 ,[Forma de Pagamento]        
 ,[Código do Banco Pagador]        
 ,[No da Agência_2]        
 ,[No da Conta Corrente_2]        
    ,[Taxa conversão]        
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
      -- WHERE PAYMENT_ID NOT IN(SELECT PAYMENT_ID FROM PAGNET_EXPORT_TABLE )        
              
          
         
              
END
GO

