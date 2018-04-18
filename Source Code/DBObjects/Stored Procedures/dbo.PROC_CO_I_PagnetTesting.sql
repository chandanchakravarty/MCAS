IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CO_I_PagnetTesting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CO_I_PagnetTesting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
--drop proc PROC_CO_I    
CREATE PROC [dbo].[PROC_CO_I_PagnetTesting]       
AS        
BEGIN TRAN  
SELECT ROW_NUMBER() OVER (ORDER BY [Sequence of record_0]) AS [Sequence of record],*  FROM(   
SELECT '05' AS [Interface code],    
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID ) AS [Sequence of record_0],    
 'J' AS [Beneficiary Type],    
 '0' AS [FOREIGN],    
 '064' AS [Beneficiary Class],    
 MR_LST.REIN_COMAPANY_NAME   AS [Beneficiary name],    
 MR_LST.CARRIER_CNPJ AS [Beneficiary ID],    
  '' AS [Beneficiary foreign ID],    
 MR_LST.REIN_COMAPANY_ADD1 AS [Beneficiary Address (street name)],    
 CASE WHEN len(MR_LST.REIN_COMAPANY_ADD1)-len(replace(MR_LST.REIN_COMAPANY_ADD1,',','')) > 0 THEN   
  CASE WHEN ISNUMERIC(DBO.Piece(MR_LST.REIN_COMAPANY_ADD1,',',len(MR_LST.REIN_COMAPANY_ADD1)-len(replace(MR_LST.REIN_COMAPANY_ADD1,',',''))+1)) =1   
  THEN  
  DBO.Piece(MR_LST.REIN_COMAPANY_ADD1,',',len(MR_LST.REIN_COMAPANY_ADD1)-len(replace(MR_LST.REIN_COMAPANY_ADD1,',',''))+1)  
  END  END AS [Beneficiary Address (number)], --nEED TO SPLIT FROM ,    
 MR_LST.REIN_COMAPANY_ADD2 AS [Beneficiary Address (complement)],    
 MR_LST.DISTRICT AS [Beneficiary Address (district)],    
 MR_LST.REIN_COMAPANY_STATE AS [Beneficiary Address (state)],    
 MR_LST.REIN_COMAPANY_CITY AS [Beneficiary Address (city)],    
 REPLACE(MR_LST.REIN_COMAPANY_ZIP,'-','') AS [Beneficiary Address (zip code)],    
 MR_LST.REIN_COMAPANY_EMAIL AS [Beneficiary e-mail address],     
 CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN  
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION    
   
 CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN    
 CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)       
  ELSE MR_LST.BANK_BRANCH_NUMBER      
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH       
         
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN       
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)       
  ELSE ''      
  END ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],     
      
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN    
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)       
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER      
  END ELSE '' END  AS [Beneficiary Bank Account number],      
        
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN  
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)       
  ELSE ''      
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],    
    
 CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'  
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'  
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
  'J' AS   [Payee Party Type],     
  '0' AS [Foreign2], -- REPEAT    
  '064' AS [Payee Class],    
  MR_LST.REIN_COMAPANY_NAME AS [Payee Name],    
  MR_LST.CARRIER_CNPJ AS [Payee ID (CPF or CNPJ)],    
  '' AS [Payee foreign ID], -- DOUBT   
   CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN     
   MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],    
     
   CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN    
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)       
  ELSE MR_LST.BANK_BRANCH_NUMBER      
  END ELSE '' END AS [Payee Bank Branch],--BRACH       
         
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN       
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)       
  ELSE ''      
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],     
      
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN    
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)       
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER      
  END ELSE '' END AS [Payee Bank Account no.],      
        
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)       
  ELSE ''      
  END ELSE '' END AS [Payee Bank Account Verifier digit],    
    
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'  
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'  
    ELSE '' END AS [Payee Bank Account Type],    
  '1' AS [Payee Bank Account Currency],    
  O_ITMS.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?    
  '00001' AS [Carrier Code],  
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED   
   '00030' AS [Carrier Policy Branch Code],  
   CASE WHEN C_LST.CO_INSURANCE = 14549 THEN '01025' --FOLLOWER  
  WHEN C_LST.CO_INSURANCE = 14548 THEN '01075' --LEADER  
   ELSE '' END   AS [EVENT CODE],   
     
  '070' AS [Operation Type],  
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN '001' -- aLLOW EFT    
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'    
  ELSE '' END AS [Payment Method],   
  NULL AS [Document number],    
  NULL AS [Document number - serial number],  
  REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],  
  REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],   
  '1' AS [Policy Currency],    
  NULL AS [Exchange rate],    
  --CASE WHEN  CHARINDEX('-',CONVERT(VARCHAR(MAX),ISNULL(COMMISSION_AMOUNT,'')))> 0 THEN '-'      
  --      ELSE '' END AS [Co amount sign],   
  NULL  AS [Co amount sign],   
  ISNULL(O_ITMS.COMMISSION_AMOUNT,0) * -1  AS [Co amount], -- TOTAL_DUE    
  NULL AS [Sinal do Valor Isento IR],    
  ABS(O_ITMS.COMMISSION_AMOUNT) AS [Valor Isento IR],    
  NULL AS [Sinal do Valor Tributável IR],    
  NULL AS [Valor Tributável IR],    
  NULL AS [Sinal do Valor Isento ISS],    
  ABS(O_ITMS.COMMISSION_AMOUNT) AS [Valor Isento ISS],    
  NULL AS [Sinal do Valor Tributável ISS],    
  NULL AS [Valor Tributável ISS],    
  NULL AS [Sinal do Valor Isento INSS],    
  ABS(O_ITMS.COMMISSION_AMOUNT) AS [Valor Isento INSS],    
  NULL AS [Sinal do Valor Tributável INSS],    
  NULL AS [Valor Tributável INSS],    
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],    
  ABS(O_ITMS.COMMISSION_AMOUNT) AS [Valor Isento CSLL/COFINS/PIS],    
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
  'CO Payment' AS [Payment Description],   
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END  
   AS [Policy Branch Code],   
  NULL AS [Profit Center code],   
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
  NULL AS [Exchange rate2]    
 FROM ACT_COI_STATEMENT_DETAILED  O_ITMS  
INNER JOIN POL_CUSTOMER_POLICY_LIST  C_LST WITH(NOLOCK)    
   ON O_ITMS.CUSTOMER_ID = C_LST.CUSTOMER_ID    
   AND O_ITMS.POLICY_ID = C_LST.POLICY_ID     
   AND O_ITMS.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID         
INNER JOIN POL_CO_INSURANCE PR_LST WITH(NOLOCK)    
   ON O_ITMS.CUSTOMER_ID = PR_LST.CUSTOMER_ID    
   AND O_ITMS.POLICY_ID = PR_LST.POLICY_ID     
   AND O_ITMS.POLICY_VERSION_ID = PR_LST.POLICY_VERSION_ID       
   AND O_ITMS.COMPANY_ID = PR_LST.COMPANY_ID         
INNER JOIN MNT_REIN_COMAPANY_LIST  MR_LST  WITH(NOLOCK)  
  ON MR_LST.REIN_COMAPANY_ID = PR_LST.COMPANY_ID    
INNER JOIN MNT_DIV_LIST D_LST     
  ON D_LST.DIV_ID = C_LST.DIV_ID  
INNER JOIN MNT_LOB_MASTER L_LST       
  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB     
LEFT JOIN POL_POLICY_ENDORSEMENTS  P_ENDOR    WITH(NOLOCK)   
  ON P_ENDOR.CUSTOMER_ID = O_ITMS.CUSTOMER_ID      
    AND P_ENDOR.POLICY_ID = O_ITMS.POLICY_ID      
    AND P_ENDOR.POLICY_VERSION_ID = O_ITMS.POLICY_VERSION_ID      
WHERE --ISNULL(O_ITMS.IS_COMMISSION_PROCESS,'N')<> 'Y'  AND 
C_LST.CO_INSURANCE IN (14549,14548)  
  AND O_ITMS.COMMISSION_AMOUNT < 0  
  )  CO WHERE [EVENT CODE] IS NOT NULL    
  
ROLLBACK TRAN      
 

GO

