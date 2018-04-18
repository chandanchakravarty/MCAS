IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_ENROLLMENT_FEE_PagnetTesting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_ENROLLMENT_FEE_PagnetTesting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
--drop PROC PROC_ENROLLMENT_FEE 
CREATE PROC PROC_ENROLLMENT_FEE_PagnetTesting      
AS      
BEGIN  
SELECT '01' AS [Interface code],  
--'1' AS [Sequence of record],  
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID ) AS [Sequence of record],  
 CASE WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11110' THEN 'F'   
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J' END AS [Beneficiary Type],  
 '0' AS [FOREIGN],  
 '004' AS [Beneficiary Class],  
 A_LST.AGENCY_DISPLAY_NAME AS [Beneficiary name],  
 A_LST.BROKER_CPF_CNPJ AS [Beneficiary ID],  
 '0' AS [Beneficiary foreign ID],  
 A_LST.AGENCY_ADD1  AS [Beneficiary Address (street name)],  
 A_LST.NUMBER AS [Beneficiary Address (number)],  
 A_LST.AGENCY_ADD2 AS [Beneficiary Address (complement)],  
 A_LST.DISTRICT AS [Beneficiary Address (district)],  
 A_LST.AGENCY_STATE AS [Beneficiary Address (state)],  
 A_LST.AGENCY_CITY AS [Beneficiary Address (city)],  
 A_LST.AGENCY_ZIP AS [Beneficiary Address (zip code)],  
 A_LST.AGENCY_EMAIL AS [Beneficiary e-mail address],   
 A_LST.BROKER_BANK_NUMBER AS [Beneficiary Bank Number], --PUT CONDITION  
   
 CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)     
  ELSE A_LST.BANK_BRANCH    
  END AS [Beneficiary Bank Branch],--BRACH     
       
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)     
  ELSE ''    
  END AS [Beneficiary Bank Branch Verifier Digit],   
    
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)     
  ELSE A_LST.BANK_ACCOUNT_NUMBER    
  END AS [Beneficiary Bank Account number],    
      
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)     
  ELSE ''    
  END AS [Beneficiary Bank Account Verifier Digit],  
  A_LST.ACCOUNT_TYPE AS [Beneficiary Bank Account type],--  
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
  A_LST.SUSEP_NUMBER AS [Broker SUSEP Code], -- BENEFICIARY CLASS IS HARD CODED  
  NULL AS [No do Funcionário],  
  NULL AS [Cód da Filial],  
  NULL AS [Cód do Centro de Custo],  
  NULL AS [Data de Nascimento],  
  CASE WHEN ISNULL (A_LST.BROKER_TYPE,'11110')='11110' THEN 'F'      
  WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J'     
  END AS   [Payee Party Type],   
  '0' AS [Foreign2], -- REPEAT  
  '004' AS [Payee Class],  
  A_LST.AGENCY_DISPLAY_NAME AS [Payee Name],  
  A_LST.BROKER_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],  
  '0' AS [Payee foreign ID], -- DOUBT   
  A_LST.BROKER_BANK_NUMBER AS [Payee Bank Number],  
    
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)     
  ELSE A_LST.BANK_BRANCH    
  END AS [Payee Bank Branch],--BRACH     
       
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)     
  ELSE ''    
  END AS [Payee Bank Branch Verifier Digit],   
    
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)     
  ELSE A_LST.BANK_ACCOUNT_NUMBER    
  END AS [Payee Bank Account no.],    
      
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)     
  ELSE ''    
  END AS [Payee Bank Account Verifier digit],  
  A_LST.ACCOUNT_TYPE AS [Payee Bank Account Type],  
  '1' AS [Payee Bank Account Currency],  
  A_smt_det.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?  
  '00001' AS [Carrier Code],  
  D_LST.BRANCH_CODE AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED  
  '01040' AS [EVENT CODE],  
  '060' AS [Operation Type],  
   CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN '001'   
        ELSE '' END AS [Payment Method], -- aLLOW EFT  
  ' ' AS [Document number],  
  ' ' AS [Document number - serial number],  
    
 CAST(YEAR(GETDATE()) AS VARCHAR) +    
 CASE WHEN LEN (CAST(MONTH(GETDATE()) AS VARCHAR)) < 2 THEN '0' +CAST(MONTH(GETDATE()) AS VARCHAR)     
 ELSE CAST(MONTH(GETDATE()) AS VARCHAR)    
 END + CASE WHEN LEN(CONVERT(VARCHAR,DATEPART(DD,GETDATE()))) < 2 THEN '0' + CONVERT(VARCHAR,DATEPART(DD,GETDATE()))    
 ELSE CONVERT(VARCHAR,DATEPART(DD,GETDATE()))    
 END AS [Invoice issuance date],    
       
 CAST(YEAR(GETDATE()) AS VARCHAR) +    
 CASE WHEN LEN (CAST(MONTH(GETDATE()) AS VARCHAR)) < 2 THEN '0' +CAST(MONTH(GETDATE()) AS VARCHAR)     
 ELSE CAST(MONTH(GETDATE()) AS VARCHAR)    
 END + CASE WHEN LEN(CONVERT(VARCHAR,DATEPART(DD,GETDATE()))) < 2 THEN '0' + CONVERT(VARCHAR,DATEPART(DD,GETDATE()))    
 ELSE CONVERT(VARCHAR,DATEPART(DD,GETDATE()))    
 END AS [Invoice due date],  
   
  '1' AS [Policy Currency],  
  ' ' AS [Exchange rate],  
  CASE WHEN  CHARINDEX('-',CONVERT(VARCHAR(MAX),ISNULL(A_smt_det.COMMISSION_AMOUNT,'')))> 0 THEN '-'    
        ELSE '' END AS [Commission amount sign],  
  ABS(A_smt_det.COMMISSION_AMOUNT) AS [Commission amount], -- TOTAL_DUE  
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
  'Broker Commission Payment' AS [Payment Description], --NEED TO DISCUSS  
  D_LST.BRANCH_CODE AS [Policy Branch Code], -- NOT CLEAR  
  P_LST.PC_NAME AS [Profit Center code], --Profit Center code NOT CLEAR  
  NULL AS [A_DEFINIR1],  
  NULL AS [A_DEFINIR2],  
  L_LST.SUSEP_LOB_ID AS [Policy Accounting LOB],  
  NULL AS [A_DEFINIR3], --   
    
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)   
  ELSE '' END  AS [Policy number (first 15 digits)],   
    
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)   
  ELSE '' END  AS [Policy Number (remaining digits)],  
    
  P_ENDOR.ENDORSEMENT_NO  AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?  
  '1' AS [Installment number], --NOT CLEAR  
  NULL AS [A_DEFINIR4],   
  NULL AS [A_DEFINIR5],  
  CAST(YEAR(A_smt_det.PAYMENT_DATE) AS VARCHAR) +      
 CASE WHEN LEN (CAST(MONTH(A_smt_det.PAYMENT_DATE) AS VARCHAR)) < 2 THEN '0' +CAST(MONTH(A_smt_det.PAYMENT_DATE) AS VARCHAR)       
 ELSE CAST(MONTH(A_smt_det.PAYMENT_DATE) AS VARCHAR)      
 END + CASE WHEN LEN(CONVERT(VARCHAR,DATEPART(DD,A_smt_det.PAYMENT_DATE))) < 2 THEN '0' + CONVERT(VARCHAR,DATEPART(DD,A_smt_det.PAYMENT_DATE))      
 ELSE CONVERT(VARCHAR,DATEPART(DD,A_smt_det.PAYMENT_DATE))      
 END AS [Installment payment date],  
  C_APP_LST.FIRST_NAME + C_APP_LST.MIDDLE_NAME + C_APP_LST.LAST_NAME  
  AS [Applicant/ Co Applicant name],   
  A_smt_det.PREMIUM_AMOUNT AS [Premium amount],  
  A_smt_det.COMMISSION_RATE AS [Commission percentage],  
  NULL AS [A_DEFINIR6],  
  NULL AS [A_DEFINIR7],  
  NULL AS [A_DEFINIR8],  
  'P' AS  [Movement type],  
  NULL AS [Commission Payment date],  
  NULL AS [Commission Paid Amount sign],  
  NULL AS [Commission paid amount],  
  NULL AS [Payment currency],  
  NULL AS [Bank account number],  
  NULL AS [Bank branch code],  
  NULL AS [Bank account number],  
  NULL AS [Cheque number],  
  NULL AS [Sinal do Valor IR],  
  NULL AS [Valor IR],  
  NULL AS [Sinal do Valor ISS],  
  NULL AS [Valor ISS],  
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
  NULL AS [Payment method],  
  NULL AS [Carrier Bank Number],  
  NULL AS [Carrier Bank Branch number],  
  NULL AS [Carrier Bank Account number],  
  NULL AS [Exchange rate2]  
 FROM ACT_AGENCY_STATEMENT_DETAILED A_smt_det  WITH(NOLOCK)
INNER JOIN  MNT_AGENCY_LIST A_LST  WITH(NOLOCK) 
   ON A_smt_det.AGENCY_ID = A_LST.AGENCY_ID     
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK) 
   ON C_LST.AGENCY_ID = A_smt_det.AGENCY_ID   
   AND A_smt_det.CUSTOMER_ID = C_LST.CUSTOMER_ID  
   AND A_smt_det.POLICY_ID = C_LST.POLICY_ID   
   AND A_smt_det.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID  
INNER JOIN MNT_DIV_LIST D_LST WITH(NOLOCK)  
 ON D_LST.DIV_ID = C_LST.DIV_ID  
INNER JOIN mnt_profit_center_list P_LST WITH(NOLOCK)      
   ON C_LST.PC_ID = P_LST.PC_ID    
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)    
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB   
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)    
 ON P_ENDOR.CUSTOMER_ID = A_smt_det.CUSTOMER_ID    
    AND P_ENDOR.POLICY_ID = A_smt_det.POLICY_ID    
    AND P_ENDOR.POLICY_VERSION_ID = A_smt_det.POLICY_VERSION_ID       
INNER JOIN POL_APPLICANT_LIST AP_LST WITH(NOLOCK)  
   ON AP_LST.CUSTOMER_ID = C_LST.CUSTOMER_ID  
    AND AP_LST.POLICY_ID = C_LST.POLICY_ID  
    AND AP_LST.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID  
    AND AP_LST.IS_PRIMARY_APPLICANT = 1  
INNER JOIN CLT_APPLICANT_LIST C_APP_LST  WITH(NOLOCK) 
    ON C_APP_LST.APPLICANT_ID = AP_LST.APPLICANT_ID 
   WHERE  --ISNULL(A_smt_det.IS_COMMISSION_PROCESS,'N') <>'Y'     
        --AND A_LST.AGENCY_TYPE_ID = 14701
     --AND
      A_smt_det.COMM_TYPE = 'PRLBR'
   
  
    END 
    
   

GO

