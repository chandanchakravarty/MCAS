IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_COMMISSION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_COMMISSION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC PROC_COMMISSION      
AS      
BEGIN  




SELECT ROW_NUMBER()  OVER (ORDER BY [Payment Description],[Sequence of record_0]) as  [Sequence of record],  *                                                          
INTO #TEMP_TABLE                                                          
 FROM (     
                                     
--SELECT * INTO  #TEMP_TABLE FROM                                          
--( SELECT ROW_NUMBER() OVER (ORDER BY [Sequence of record_0]) AS [Sequence of record],*                                           
-- FROM                                          
--(    
 SELECT '01' AS [Interface code],                                            
--'1' AS [Sequence of record],                                            
ROW_NUMBER() OVER ( ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence of record_0],                                            
 CASE WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11110' THEN 'F'                                             
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J'                                          
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],                                            
 '0' AS [FOREIGN],                                            
 '004' AS [Beneficiary Class],                                            
 A_LST.AGENCY_DISPLAY_NAME AS [Beneficiary name],                                            
 A_LST.BROKER_CPF_CNPJ AS [Beneficiary ID],                                            
 '' AS [Beneficiary foreign ID],                                            
 A_LST.AGENCY_ADD1  AS [Beneficiary Address (street name)],                                            
 A_LST.NUMBER AS [Beneficiary Address (number)],                                            
 A_LST.AGENCY_ADD2 AS [Beneficiary Address (complement)],                                            
 A_LST.DISTRICT AS [Beneficiary Address (district)],                                            
 MCSL.STATE_CODE AS [Beneficiary Address (state)],                                            
 A_LST.AGENCY_CITY AS [Beneficiary Address (city)],                                            
 REPLACE(A_LST.AGENCY_ZIP,'-','') AS [Beneficiary Address (zip code)],                                            
 A_LST.AGENCY_EMAIL AS [Beneficiary e-mail address],                                          
                                           
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
 A_LST.BROKER_BANK_NUMBER                                            
 ELSE ''END AS [Beneficiary Bank Number], --PUT CONDITION                                            
                                           
 CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                            
   CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                               
   ELSE A_LST.BANK_BRANCH                                              
   END                                           
 ELSE ''END AS [Beneficiary Bank Branch],--BRACH                                   
                                              
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                       
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                            
      --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                    
  ELSE '' END                                              
  ELSE ''END AS [Beneficiary Bank Branch Verifier Digit],                                             
                                          
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN   A_LST.BANK_ACCOUNT_NUMBER                                          
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                             
  ----itrack 1750                                          
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                                 
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                          
  ELSE ''END AS [Beneficiary Bank Account number],                                              
                                                
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                          
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
   --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                                  
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE ''                                              
  END                                          
  ELSE ''END AS [Beneficiary Bank Account Verifier Digit],                                            
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Beneficiary Bank Account type],--                                            
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
  WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS   [Payee Party Type],                                             
  '0' AS [Foreign2], -- REPEAT                             
  '004' AS [Payee Class],  --Handled at front end as per sheet                                          
  A_LST.AGENCY_DISPLAY_NAME AS [Payee Name],                                            
  A_LST.BROKER_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],                        
  '' AS [Payee foreign ID], -- DOUBT                                            
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                            
  A_LST.BROKER_BANK_NUMBER                                           
  END AS [Payee Bank Number],                                            
                                             
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                    
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                                           
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                                       
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                                           
  ELSE A_LST.BANK_BRANCH                                      
  END                                           
  ELSE '' END AS [Payee Bank Branch],--BRACH                                               
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                               
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                      
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                                             
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                                  
 ELSE ''END                                              
   ELSE '' END AS [Payee Bank Branch Verifier Digit],                                             
                   
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN  A_LST.BANK_ACCOUNT_NUMBER                                           
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                           
  ELSE '' END AS [Payee Bank Account no.],                                              
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                           
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                         
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE '' END                                          
  ELSE '' END AS [Payee Bank Account Verifier digit],                                           
                                             
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Payee Bank Account Type],                                            
  '1' AS [Payee Bank Account Currency],                                            
  A_smt_det.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?                                            
  '00001' AS [Carrier Code],                                            
 -- CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED            
  '00030' AS [Carrier Policy Branch Code],                                          
  CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN '01030'                                           
       WHEN A_smt_det.COMM_TYPE = 'PRLBR' THEN '01035'                                           
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN '01040' END  AS [EVENT CODE],                                            
  '060' AS [Operation Type],                                            
   CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN '001'                                             
        WHEN ISNULL(A_LST.ALLOWS_EFT,0) = 10964 THEN '005'                                           
        ELSE '005' END AS [Payment Method], -- aLLOW EFT                                            
  '' AS [Document number],                                           
  '' AS [Document number - serial number],                                        
                                              
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],                                           
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date],                                            
                                             
  '1' AS [Policy Currency],                                            
  '' AS [Exchange rate],                                            
  --CASE WHEN  ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 < 0 THEN '-'                                              
  --      ELSE '' END AS [Commission amount sign],                                          
  NULL AS [Commission amount sign],                                            
  --ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 AS [Commission amount], -- TOTAL_DUE      
      
    CASE WHEN  (C_LST.POLICY_LEVEL_COMISSION>0 AND POLICY_LEVEL_COMM_APPLIES='Y')  
    THEN           
    ABS(ISNULL(((A_smt_det.AMOUNT_FOR_CALCULATION+ACT_PID.INTEREST_AMOUNT)*(C_LST.POLICY_LEVEL_COMISSION))/100,0)*(PR.COMMISSION_PERCENT)/100 )                                            
  ELSE  
    ABS(ISNULL(((A_smt_det.AMOUNT_FOR_CALCULATION+ACT_PID.INTEREST_AMOUNT)*(PR.COMMISSION_PERCENT))/100,0) )                                            
 END  AS [Commission amount],   
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
  --changes by naveen, changes by naveen, itrack 1750, refer sheet v12,                            
  --CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Agente de Pagamento de Comissão'  --'Broker Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Prolabore Pagamento de Comissão'   --'Prolabore Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Taxa de inscrição' END AS [Payment Description],                                          
    CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Pagamento de comissão de corretagem'  --'Broker Commission Payment'                                           
       WHEN  A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Pagamento de pró-labore'   --'Prolabore Commission Payment'                                       
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Pagamento de agenciamento' END AS [Payment Description],                                          
                                     
                                                 
 CASE WHEN LEN(D_LST.BRANCH_CODE)=1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END  AS [Policy Branch Code], -- NOT CLEAR                                            
  '' AS [Profit Center code], --Profit Center AS PER DISCUSSION                                           
  NULL AS [A_DEFINIR1],                                            
  NULL AS [A_DEFINIR2],                                           
 --tfs id-2246,modified by naveen                                          
  case when   L_LST.SUSEP_LOB_CODE='0520'                                          
  then '0982'  else                                                
  L_LST.SUSEP_LOB_CODE end AS [Policy Accounting LOB],                                           
  NULL AS [A_DEFINIR3], --                                             
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)                                             
  ELSE '' END  AS [Policy number (first 15 digits)],                              
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)                                             
  ELSE '' END  AS [Policy Number (remaining digits)],                                            
                                              
  P_ENDOR.ENDORSEMENT_NO  AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?                                            
  A_smt_det.INSTALLMENT_NO AS [Installment number], --NOT CLEAR                                            
  NULL AS [A_DEFINIR4],                                             
  NULL AS [A_DEFINIR5],                                          
  CONVERT(VARCHAR,A_smt_det.PAYMENT_DATE, 103) AS [Installment payment date],                                            
  RTRIM(C_APP_LST.FIRST_NAME+' '+ CASE WHEN  len(ISNULL(C_APP_LST.MIDDLE_NAME,''))<1  THEN ''                   
     ELSE C_APP_LST.MIDDLE_NAME +' '    END  +  ISNULL(C_APP_LST.LAST_NAME,'')) AS [Applicant/ Co Applicant name],                                            
  A_smt_det.AMOUNT_FOR_CALCULATION AS [Premium amount],                                            
   --A_smt_det.COMMISSION_RATE AS [Commission percentage],                                  
   --Modified by naveen , itrack 1750 , calcuated amouint based on the new requirement issue no 12 ,Comissão_Commission                           
   cast((ABS(A_smt_det.COMMISSION_AMOUNT)/A_smt_det.AMOUNT_FOR_CALCULATION)*100 as decimal(25,4)) AS [Commission percentage],                                     
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
  NULL AS [No da Conta Corrente],       
  NULL AS [Cheque number],                                            
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
 FROM ACT_AGENCY_STATEMENT_DETAILED A_smt_det  WITH(NOLOCK)                                          
INNER JOIN  MNT_AGENCY_LIST A_LST  WITH(NOLOCK)                                           
   ON A_smt_det.AGENCY_ID = A_LST.AGENCY_ID                                               
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK)                                           
   ON --C_LST.AGENCY_ID = A_smt_det.AGENCY_ID  AND                                           
    A_smt_det.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
   AND A_smt_det.POLICY_ID = C_LST.POLICY_ID                                             
   AND A_smt_det.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                          
   --AS PER ITRACK 1662----                                          
                                  
INNER JOIN MNT_DIV_LIST D_LST WITH(NOLOCK)                                            
 ON D_LST.DIV_ID = C_LST.DIV_ID                              
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)                                              
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB                                             
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)                                              
 ON P_ENDOR.CUSTOMER_ID = A_smt_det.CUSTOMER_ID                                              
    AND P_ENDOR.POLICY_ID = A_smt_det.POLICY_ID                                              
    AND P_ENDOR.POLICY_VERSION_ID = A_smt_det.POLICY_VERSION_ID                                                 
--uncommented by naveen          
                                     
--    AND AP_LST.IS_PRIMARY_APPLICANT = 1                                            
INNER  JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI WITH(NOLOCK)                           ON ACOI.CUSTOMER_ID = C_LST.CUSTOMER_ID                                
    AND ACOI.POLICY_ID = C_LST.POLICY_ID                                              
    AND ACOI.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                        
    AND ACOI.IDEN_ROW_ID =  A_smt_det.CUSTOMER_OPEN_ITEM_ID                                         
INNER JOIN CLT_APPLICANT_LIST C_APP_LST  WITH(NOLOCK)                                           
    ON C_APP_LST.APPLICANT_ID = ACOI.PAYOR_ID            
    AND  C_APP_LST.CUSTOMER_ID = ACOI.CUSTOMER_ID                                     
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK)                                          
  --ON CAST(A_LST.AGENCY_STATE AS SMALLINT) = MCSL.STATE_ID                          
  -- Changed for itrack #1750 issue 9                                          
 ON (A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5)) or  A_LST.AGENCY_STATE  = MCSL.STATE_CODE)                                 
 --A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5))                                            
 AND A_LST.AGENCY_COUNTRY = MCSL.COUNTRY_ID                          
 --ADDED BY NAVEEN, ITRACK 1750, SHEET V18, POINT 15                        
 INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS ACT_PID                        
 ON ACT_PID.CUSTOMER_ID = C_LST.CUSTOMER_ID                 
  AND ACT_PID.POLICY_ID = C_LST.POLICY_ID                                             
  AND ACT_PID.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                 
  AND ACT_PID.INSTALLMENT_NO   =A_smt_det.INSTALLMENT_NO                     
 and ACT_PID.RELEASED_STATUS='Y'           
 INNER JOIN POL_APPLICANT_LIST AP_LST WITH(NOLOCK)                                            
   ON AP_LST.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
    AND AP_LST.POLICY_ID = C_LST.POLICY_ID                                            
    AND AP_LST.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID            
    AND  AP_LST.APPLICANT_ID   =  ACT_PID.CO_APPLICANT_ID         
    AND  AP_LST.APPLICANT_ID   =  C_APP_LST.APPLICANT_ID         
           
   INNER JOIN POL_REMUNERATION PR WITH(NOLOCK)                                          
 ON PR.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND PR.POLICY_ID = C_LST.POLICY_ID                                               
    AND PR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                            
    AND PR.BROKER_ID = A_smt_det.AGENCY_ID         
    AND PR.CO_APPLICANT_ID = AP_LST.APPLICANT_ID        
    AND PR.COMMISSION_TYPE = 43                            
   --TILL HERE                                
   WHERE  ISNULL(A_smt_det.IS_COMMISSION_PROCESS,'N') <>'Y'                                               
        AND A_LST.AGENCY_TYPE_ID IN (14701,14702)                                          
        AND  A_smt_det.COMMISSION_AMOUNT < 0                                          
        AND A_smt_det.COMM_TYPE = 'COMM'                                       
        --CONDITION IS BASED ON #ITRACK 1284                                          
        AND CONVERT(date,A_LST.TERMINATION_DATE,101) >= convert(date,GETDATE(),101)           
        AND C_LST.TRANSACTION_TYPE<>14560          
        
       UNION          
       --to get  COMMISSION_PERCENT          
       SELECT '01' AS [Interface code],                                            
--'1' AS [Sequence of record],                                            
ROW_NUMBER() OVER ( ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence of record_0],                                            
 CASE WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11110' THEN 'F'                                             
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J'                                          
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],                                            
 '0' AS [FOREIGN],                                            
 '004' AS [Beneficiary Class],                                            
 A_LST.AGENCY_DISPLAY_NAME AS [Beneficiary name],                                            
 A_LST.BROKER_CPF_CNPJ AS [Beneficiary ID],                                            
 '' AS [Beneficiary foreign ID],                                            
 A_LST.AGENCY_ADD1  AS [Beneficiary Address (street name)],                                            
 A_LST.NUMBER AS [Beneficiary Address (number)],                                            
 A_LST.AGENCY_ADD2 AS [Beneficiary Address (complement)],                                            
 A_LST.DISTRICT AS [Beneficiary Address (district)],                                            
 MCSL.STATE_CODE AS [Beneficiary Address (state)],                                            
 A_LST.AGENCY_CITY AS [Beneficiary Address (city)],                                            
 REPLACE(A_LST.AGENCY_ZIP,'-','') AS [Beneficiary Address (zip code)],                                            
 A_LST.AGENCY_EMAIL AS [Beneficiary e-mail address],                                          
                                           
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
 A_LST.BROKER_BANK_NUMBER                                            
 ELSE ''END AS [Beneficiary Bank Number], --PUT CONDITION                                            
                                           
 CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                            
   CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                               
   ELSE A_LST.BANK_BRANCH                                              
   END                                           
 ELSE ''END AS [Beneficiary Bank Branch],--BRACH                                   
                                              
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                            
      --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                    
  ELSE '' END                                              
  ELSE ''END AS [Beneficiary Bank Branch Verifier Digit],                                             
                                          
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN   A_LST.BANK_ACCOUNT_NUMBER                                          
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                             
  ----itrack 1750                                          
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                                 
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                          
  ELSE ''END AS [Beneficiary Bank Account number],                                              
                                                
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                          
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
   --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                                  
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE ''                                              
  END                                          
  ELSE ''END AS [Beneficiary Bank Account Verifier Digit],                                            
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Beneficiary Bank Account type],--                                            
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
  WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS   [Payee Party Type],                                             
  '0' AS [Foreign2], -- REPEAT                             
  '004' AS [Payee Class],  --Handled at front end as per sheet                                          
  A_LST.AGENCY_DISPLAY_NAME AS [Payee Name],                                            
  A_LST.BROKER_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],                        
  '' AS [Payee foreign ID], -- DOUBT                                            
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                            
  A_LST.BROKER_BANK_NUMBER                                           
  END AS [Payee Bank Number],                                            
                                             
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                    
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                                           
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                                       
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                                           
  ELSE A_LST.BANK_BRANCH                                      
  END                                           
  ELSE '' END AS [Payee Bank Branch],--BRACH                                               
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                               
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                      
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                                             
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                                  
  ELSE ''END                                              
   ELSE '' END AS [Payee Bank Branch Verifier Digit],                                             
                   
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN  A_LST.BANK_ACCOUNT_NUMBER                                           
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                           
  ELSE '' END AS [Payee Bank Account no.],                                              
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                           
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                         
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE '' END                                          
  ELSE '' END AS [Payee Bank Account Verifier digit],                                           
                                             
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Payee Bank Account Type],                                            
  '1' AS [Payee Bank Account Currency],                                            
  A_smt_det.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?                                            
  '00001' AS [Carrier Code],                                            
 -- CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED            
  '00030' AS [Carrier Policy Branch Code],                                          
  CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN '01030'                                           
       WHEN A_smt_det.COMM_TYPE = 'PRLBR' THEN '01035'                                           
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN '01040' END  AS [EVENT CODE],                                            
  '060' AS [Operation Type],                                            
   CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN '001'                                             
        WHEN ISNULL(A_LST.ALLOWS_EFT,0) = 10964 THEN '005'                                           
        ELSE '005' END AS [Payment Method], -- aLLOW EFT                                            
  '' AS [Document number],                                            
  '' AS [Document number - serial number],                                        
                                              
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],                                           
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date],                                            
                                             
  '1' AS [Policy Currency],                                            
  '' AS [Exchange rate],                                            
  --CASE WHEN  ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 < 0 THEN '-'                                              
  --      ELSE '' END AS [Commission amount sign],                                          
  NULL AS [Commission amount sign],                                            
  --ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 AS [Commission amount], -- TOTAL_DUE                        
  ABS(ISNULL(((A_smt_det.AMOUNT_FOR_CALCULATION+ACT_PID.INTEREST_AMOUNT)*(AP_LST.COMMISSION_PERCENT))/100,0)) AS [Commission amount],                                            
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
  --changes by naveen, changes by naveen, itrack 1750, refer sheet v12,                            
  --CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Agente de Pagamento de Comissão'  --'Broker Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Prolabore Pagamento de Comissão'   --'Prolabore Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Taxa de inscrição' END AS [Payment Description],                                          
    CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Pagamento de comissão de corretagem'  --'Broker Commission Payment'                                           
       WHEN  A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Pagamento de pró-labore'   --'Prolabore Commission Payment'                                       
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Pagamento de agenciamento' END AS [Payment Description],                                          
                                     
                                                 
 CASE WHEN LEN(D_LST.BRANCH_CODE)=1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END  AS [Policy Branch Code], -- NOT CLEAR                                            
  '' AS [Profit Center code], --Profit Center AS PER DISCUSSION                                           
  NULL AS [A_DEFINIR1],                                            
  NULL AS [A_DEFINIR2],                                           
 --tfs id-2246,modified by naveen                                          
  case when   L_LST.SUSEP_LOB_CODE='0520'                                          
  then '0982'  else                                                
  L_LST.SUSEP_LOB_CODE end AS [Policy Accounting LOB],                                           
  NULL AS [A_DEFINIR3], --                                             
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)                                             
  ELSE '' END  AS [Policy number (first 15 digits)],                              
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)                                             
  ELSE '' END  AS [Policy Number (remaining digits)],                                            
                                              
  P_ENDOR.ENDORSEMENT_NO  AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?                                            
  A_smt_det.INSTALLMENT_NO AS [Installment number], --NOT CLEAR                                            
  NULL AS [A_DEFINIR4],                                             
  NULL AS [A_DEFINIR5],                                          
  CONVERT(VARCHAR,A_smt_det.PAYMENT_DATE, 103) AS [Installment payment date],                                            
  RTRIM(C_APP_LST.FIRST_NAME+' '+ CASE WHEN  len(ISNULL(C_APP_LST.MIDDLE_NAME,''))<1  THEN ''                   
     ELSE C_APP_LST.MIDDLE_NAME +' '    END  +  ISNULL(C_APP_LST.LAST_NAME,'')) AS [Applicant/ Co Applicant name],                                            
  A_smt_det.AMOUNT_FOR_CALCULATION AS [Premium amount],                                            
   --A_smt_det.COMMISSION_RATE AS [Commission percentage],                                  
   --Modified by naveen , itrack 1750 , calcuated amouint based on the new requirement issue no 12 ,Comissão_Commission                           
   cast((ABS(A_smt_det.COMMISSION_AMOUNT)/A_smt_det.AMOUNT_FOR_CALCULATION)*100 as decimal(25,4)) AS [Commission percentage],                                     
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
  NULL AS [No da Conta Corrente],                                            
  NULL AS [Cheque number],                                            
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
 FROM ACT_AGENCY_STATEMENT_DETAILED A_smt_det  WITH(NOLOCK)                                          
INNER JOIN  MNT_AGENCY_LIST A_LST  WITH(NOLOCK)                                           
   ON A_smt_det.AGENCY_ID = A_LST.AGENCY_ID                                               
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK)                                           
   ON --C_LST.AGENCY_ID = A_smt_det.AGENCY_ID  AND                                           
    A_smt_det.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
   AND A_smt_det.POLICY_ID = C_LST.POLICY_ID                                             
   AND A_smt_det.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                          
   --AS PER ITRACK 1662----                                          
                                  
INNER JOIN MNT_DIV_LIST D_LST WITH(NOLOCK)                                            
 ON D_LST.DIV_ID = C_LST.DIV_ID                              
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)                                              
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB                                             
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)                                              
 ON P_ENDOR.CUSTOMER_ID = A_smt_det.CUSTOMER_ID                                              
    AND P_ENDOR.POLICY_ID = A_smt_det.POLICY_ID                                              
    AND P_ENDOR.POLICY_VERSION_ID = A_smt_det.POLICY_VERSION_ID                                                 
--uncommented by naveen          
                                     
--    AND AP_LST.IS_PRIMARY_APPLICANT = 1                                            
INNER  JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI WITH(NOLOCK)                           ON ACOI.CUSTOMER_ID = C_LST.CUSTOMER_ID                                
    AND ACOI.POLICY_ID = C_LST.POLICY_ID                                              
    AND ACOI.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                        
    AND ACOI.IDEN_ROW_ID =  A_smt_det.CUSTOMER_OPEN_ITEM_ID                                         
INNER JOIN CLT_APPLICANT_LIST C_APP_LST  WITH(NOLOCK)                                           
    ON C_APP_LST.APPLICANT_ID = ACOI.PAYOR_ID            
    AND  C_APP_LST.CUSTOMER_ID = ACOI.CUSTOMER_ID                                     
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK)                                          
  --ON CAST(A_LST.AGENCY_STATE AS SMALLINT) = MCSL.STATE_ID                          
  -- Changed for itrack #1750 issue 9                                          
 ON (A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5)) or  A_LST.AGENCY_STATE  = MCSL.STATE_CODE)                                 
 --A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5))                                            
 AND A_LST.AGENCY_COUNTRY = MCSL.COUNTRY_ID                          
 --ADDED BY NAVEEN, ITRACK 1750, SHEET V18, POINT 15                        
 INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS ACT_PID                        
 ON ACT_PID.CUSTOMER_ID = C_LST.CUSTOMER_ID                 
  AND ACT_PID.POLICY_ID = C_LST.POLICY_ID                                             
  AND ACT_PID.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                 
  AND ACT_PID.INSTALLMENT_NO   =A_smt_det.INSTALLMENT_NO                     
 and ACT_PID.RELEASED_STATUS='Y'           
 INNER JOIN POL_APPLICANT_LIST AP_LST WITH(NOLOCK)                                            
   ON AP_LST.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
    AND AP_LST.POLICY_ID = C_LST.POLICY_ID                                            
    AND AP_LST.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID            
    AND  AP_LST.APPLICANT_ID   =  ACT_PID.CO_APPLICANT_ID         
    AND  AP_LST.APPLICANT_ID   =  C_APP_LST.APPLICANT_ID         
           
   INNER JOIN POL_REMUNERATION PR WITH(NOLOCK)                                          
 ON PR.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND PR.POLICY_ID = C_LST.POLICY_ID                                               
    AND PR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                            
    AND PR.BROKER_ID = A_smt_det.AGENCY_ID         
    AND PR.CO_APPLICANT_ID = AP_LST.APPLICANT_ID        
    AND PR.COMMISSION_TYPE = 43                            
   --TILL HERE                                
   WHERE  ISNULL(A_smt_det.IS_COMMISSION_PROCESS,'N') <>'Y'                                               
        AND A_LST.AGENCY_TYPE_ID IN (14701,14702)                                          
        AND  A_smt_det.COMMISSION_AMOUNT < 0                                          
        AND A_smt_det.COMM_TYPE = 'COMM'                                       
        --CONDITION IS BASED ON #ITRACK 1284                                          
        AND CONVERT(date,A_LST.TERMINATION_DATE,101) >= convert(date,GETDATE(),101)           
        AND C_LST.TRANSACTION_TYPE=14560                      
     UNION          
     --to get prolabour          
     SELECT '01' AS [Interface code],                                            
--'1' AS [Sequence of record],                                            
ROW_NUMBER() OVER ( ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence of record_0],                                            
 CASE WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11110' THEN 'F'                                             
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J'                                          
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],                                            
 '0' AS [FOREIGN],                                            
 '004' AS [Beneficiary Class],                                            
 A_LST.AGENCY_DISPLAY_NAME AS [Beneficiary name],                                            
 A_LST.BROKER_CPF_CNPJ AS [Beneficiary ID],                                            
 '' AS [Beneficiary foreign ID],                                            
 A_LST.AGENCY_ADD1  AS [Beneficiary Address (street name)],                                            
 A_LST.NUMBER AS [Beneficiary Address (number)],                                            
 A_LST.AGENCY_ADD2 AS [Beneficiary Address (complement)],                                            
 A_LST.DISTRICT AS [Beneficiary Address (district)],                                            
 MCSL.STATE_CODE AS [Beneficiary Address (state)],                                            
 A_LST.AGENCY_CITY AS [Beneficiary Address (city)],                                            
 REPLACE(A_LST.AGENCY_ZIP,'-','') AS [Beneficiary Address (zip code)],                                            
 A_LST.AGENCY_EMAIL AS [Beneficiary e-mail address],                                          
                                           
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
 A_LST.BROKER_BANK_NUMBER                                            
 ELSE ''END AS [Beneficiary Bank Number], --PUT CONDITION                                            
                                           
 CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                            
   CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                               
   ELSE A_LST.BANK_BRANCH                                              
   END                                           
 ELSE ''END AS [Beneficiary Bank Branch],--BRACH                                            
                                      
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                            
      --itrack 1750            
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                    
  ELSE '' END                                              
  ELSE ''END AS [Beneficiary Bank Branch Verifier Digit],                                             
                                          
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN   A_LST.BANK_ACCOUNT_NUMBER                                          
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                             
  ----itrack 1750                                          
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                           
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                          
  ELSE ''END AS [Beneficiary Bank Account number],                                              
                                                
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                          
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
   --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                                  
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE ''                                     
  END                                          
  ELSE ''END AS [Beneficiary Bank Account Verifier Digit],                                            
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Beneficiary Bank Account type],--                                            
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
  WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS   [Payee Party Type],                                             
  '0' AS [Foreign2], -- REPEAT                             
  '004' AS [Payee Class],  --Handled at front end as per sheet                                          
  A_LST.AGENCY_DISPLAY_NAME AS [Payee Name],                                            
  A_LST.BROKER_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],                        
  '' AS [Payee foreign ID], -- DOUBT                                            
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                            
  A_LST.BROKER_BANK_NUMBER                                           
  END AS [Payee Bank Number],                                            
                                             
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) = 10963 THEN                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                    
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                                           
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                                       
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                                           
  ELSE A_LST.BANK_BRANCH                                      
  END                                           
  ELSE '' END AS [Payee Bank Branch],--BRACH                                               
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                               
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                      
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                                             
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                            
  ELSE ''END                                              
   ELSE '' END AS [Payee Bank Branch Verifier Digit],                                             
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN  A_LST.BANK_ACCOUNT_NUMBER                                           
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                           
  ELSE '' END AS [Payee Bank Account no.],                                              
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                           
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                         
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE '' END                                          
  ELSE '' END AS [Payee Bank Account Verifier digit],                                           
                                             
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Payee Bank Account Type],                                            
  '1' AS [Payee Bank Account Currency],                                            
  A_smt_det.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?                                            
  '00001' AS [Carrier Code],                                            
 -- CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED                                           
  '00030' AS [Carrier Policy Branch Code],                                          
  CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN '01030'                                           
       WHEN A_smt_det.COMM_TYPE = 'PRLBR' THEN '01035'                                           
       WHEN A_smt_det.COMM_TYPE = 'ENFEE' THEN '01040' END  AS [EVENT CODE],                                            
  '060' AS [Operation Type],                                            
   CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN '001'                                             
        WHEN ISNULL(A_LST.ALLOWS_EFT,0) = 10964 THEN '005'                                           
        ELSE '005' END AS [Payment Method], -- aLLOW EFT                                            
  '' AS [Document number],                                            
  '' AS [Document number - serial number],                                        
                                              
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],                                           
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date],                                            
                                             
  '1' AS [Policy Currency],                                            
  '' AS [Exchange rate],                                            
  --CASE WHEN  ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 < 0 THEN '-'                                              
  --      ELSE '' END AS [Commission amount sign],                                          
  NULL AS [Commission amount sign],                                            
  --ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 AS [Commission amount], -- TOTAL_DUE                        
  ABS(ISNULL(((A_smt_det.AMOUNT_FOR_CALCULATION+ACT_PID.INTEREST_AMOUNT)*(AP_LST.PRO_LABORE_PERCENT))/100,0)) AS [Commission amount],                                            
  NULL AS [Sinal do Valor Isento IR],                                            
  NULL AS [Valor Isento IR],                                            
  NULL AS [Sinal do Valor Tributável IR],                                            
  NULL AS [Valor Tributável IR],                                            
  NULL AS [Sinal do Valor Isento ISS],                                            
  NULL AS [Valor Isento ISS],                                            
  NULL AS [Sinal do Valor Tributável ISS],                                            
  NULL AS [Valor Tributável ISS],                                            
  NULL AS [Sinal do Valor Isento INSS],                                           NULL AS [Valor Isento INSS],                                            
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
  --changes by naveen, changes by naveen, itrack 1750, refer sheet v12,                            
  --CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Agente de Pagamento de Comissão'  --'Broker Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Prolabore Pagamento de Comissão'   --'Prolabore Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Taxa de inscrição' END AS [Payment Description],                                          
    CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Pagamento de comissão de corretagem'  --'Broker Commission Payment'                                           
       WHEN  A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Pagamento de pró-labore'   --'Prolabore Commission Payment'                                       
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Pagamento de agenciamento' END AS [Payment Description],                                          
                                     
                                                 
 CASE WHEN LEN(D_LST.BRANCH_CODE)=1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END  AS [Policy Branch Code], -- NOT CLEAR                                            
  '' AS [Profit Center code], --Profit Center AS PER DISCUSSION                                           
  NULL AS [A_DEFINIR1],                                            
  NULL AS [A_DEFINIR2],                                           
 --tfs id-2246,modified by naveen                                          
  case when   L_LST.SUSEP_LOB_CODE='0520'                                          
  then '0982'  else                                                
  L_LST.SUSEP_LOB_CODE end AS [Policy Accounting LOB],                                           
  NULL AS [A_DEFINIR3], --                                             
                                              
 CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)                                             
  ELSE '' END  AS [Policy number (first 15 digits)],                                             
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)                                             
  ELSE '' END  AS [Policy Number (remaining digits)],                                            
                                              
  P_ENDOR.ENDORSEMENT_NO  AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?                                            
  A_smt_det.INSTALLMENT_NO AS [Installment number], --NOT CLEAR                                            
  NULL AS [A_DEFINIR4],                                             
  NULL AS [A_DEFINIR5],                                          
  CONVERT(VARCHAR,A_smt_det.PAYMENT_DATE, 103) AS [Installment payment date],                                            
  RTRIM(C_APP_LST.FIRST_NAME+' '+ CASE WHEN  len(ISNULL(C_APP_LST.MIDDLE_NAME,''))<1  THEN ''                   
     ELSE C_APP_LST.MIDDLE_NAME +' '    END  +  ISNULL(C_APP_LST.LAST_NAME,'')) AS [Applicant/ Co Applicant name],                                            
  A_smt_det.AMOUNT_FOR_CALCULATION AS [Premium amount],                                            
   --A_smt_det.COMMISSION_RATE AS [Commission percentage],                                  
   --Modified by naveen , itrack 1750 , calcuated amouint based on the new requirement issue no 12 ,Comissão_Commission                           
   cast((ABS(A_smt_det.COMMISSION_AMOUNT)/A_smt_det.AMOUNT_FOR_CALCULATION)*100 as decimal(25,4)) AS [Commission percentage],                                     
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
  NULL AS [No da Conta Corrente],                                            
  NULL AS [Cheque number],                                            
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
                                           
 FROM ACT_AGENCY_STATEMENT_DETAILED A_smt_det  WITH(NOLOCK)                                          
INNER JOIN  MNT_AGENCY_LIST A_LST  WITH(NOLOCK)                                           
   ON A_smt_det.AGENCY_ID = A_LST.AGENCY_ID                                               
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK)                                           
   ON --C_LST.AGENCY_ID = A_smt_det.AGENCY_ID  AND                                           
    A_smt_det.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
   AND A_smt_det.POLICY_ID = C_LST.POLICY_ID                                             
   AND A_smt_det.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                          
   --AS PER ITRACK 1662----                                          
                                  
INNER JOIN MNT_DIV_LIST D_LST WITH(NOLOCK)                                            
 ON D_LST.DIV_ID = C_LST.DIV_ID                              
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)                                              
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB                                             
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)                                              
 ON P_ENDOR.CUSTOMER_ID = A_smt_det.CUSTOMER_ID                                              
    AND P_ENDOR.POLICY_ID = A_smt_det.POLICY_ID                                              
    AND P_ENDOR.POLICY_VERSION_ID = A_smt_det.POLICY_VERSION_ID                                                 
--uncommented by naveen          
                                     
--    AND AP_LST.IS_PRIMARY_APPLICANT = 1                                            
INNER  JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI WITH(NOLOCK)                           ON ACOI.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND ACOI.POLICY_ID = C_LST.POLICY_ID                                              
    AND ACOI.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                        
    AND ACOI.IDEN_ROW_ID =  A_smt_det.CUSTOMER_OPEN_ITEM_ID                                         
INNER JOIN CLT_APPLICANT_LIST C_APP_LST  WITH(NOLOCK)                                           
    ON C_APP_LST.APPLICANT_ID = ACOI.PAYOR_ID            
    AND  C_APP_LST.CUSTOMER_ID = ACOI.CUSTOMER_ID                   
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK)                                          
  --ON CAST(A_LST.AGENCY_STATE AS SMALLINT) = MCSL.STATE_ID                          
  -- Changed for itrack #1750 issue 9                                          
 ON (A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5)) or  A_LST.AGENCY_STATE  = MCSL.STATE_CODE)                                 
 --A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5))                                            
 AND A_LST.AGENCY_COUNTRY = MCSL.COUNTRY_ID                          
 --ADDED BY NAVEEN, ITRACK 1750, SHEET V18, POINT 15                        
 INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS ACT_PID                        
 ON ACT_PID.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
  AND ACT_PID.POLICY_ID = C_LST.POLICY_ID                                             
  AND ACT_PID.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                 
  AND ACT_PID.INSTALLMENT_NO   =A_smt_det.INSTALLMENT_NO                     
 and ACT_PID.RELEASED_STATUS='Y'           
 INNER JOIN POL_APPLICANT_LIST AP_LST WITH(NOLOCK)                                            
   ON AP_LST.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
    AND AP_LST.POLICY_ID = C_LST.POLICY_ID                                            
    AND AP_LST.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID            
    AND  AP_LST.APPLICANT_ID   =  ACT_PID.CO_APPLICANT_ID         
    AND  AP_LST.APPLICANT_ID   =  C_APP_LST.APPLICANT_ID         
        
   INNER JOIN POL_REMUNERATION PR WITH(NOLOCK)                                          
 ON PR.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND PR.POLICY_ID = C_LST.POLICY_ID                                    
    AND PR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                            
    AND PR.BROKER_ID = A_smt_det.AGENCY_ID         
    AND PR.CO_APPLICANT_ID = AP_LST.APPLICANT_ID        
    AND PR.COMMISSION_TYPE = 45                                  
   --TILL HERE                                
   WHERE  ISNULL(A_smt_det.IS_COMMISSION_PROCESS,'N') <>'Y'                                     
        AND A_LST.AGENCY_TYPE_ID IN (14701,14702)                                          
        AND  A_smt_det.COMMISSION_AMOUNT < 0                                          
      AND A_smt_det.COMM_TYPE = 'PRLBR'                                          
        --CONDITION IS BASED ON #ITRACK 1284                                          
        AND CONVERT(date,A_LST.TERMINATION_DATE,101) >= convert(date,GETDATE(),101)                       
      AND C_LST.TRANSACTION_TYPE=14560          
               
     UNION          
     --to get enrollmetnt fees.          
     SELECT '01' AS [Interface code],                                            
--'1' AS [Sequence of record],                                            
ROW_NUMBER() OVER ( ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence of record_0],                                            
 CASE WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11110' THEN 'F'                                             
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='11109' THEN 'J'                                          
     WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS [Beneficiary Type],                                            
 '0' AS [FOREIGN],                                            
 '004' AS [Beneficiary Class],                                            
 A_LST.AGENCY_DISPLAY_NAME AS [Beneficiary name],                                            
 A_LST.BROKER_CPF_CNPJ AS [Beneficiary ID],                                            
 '' AS [Beneficiary foreign ID],                                            
 A_LST.AGENCY_ADD1  AS [Beneficiary Address (street name)],                                            
 A_LST.NUMBER AS [Beneficiary Address (number)],                                     
 A_LST.AGENCY_ADD2 AS [Beneficiary Address (complement)],                                            
 A_LST.DISTRICT AS [Beneficiary Address (district)],                                            
 MCSL.STATE_CODE AS [Beneficiary Address (state)],                       
 A_LST.AGENCY_CITY AS [Beneficiary Address (city)],                                            
 REPLACE(A_LST.AGENCY_ZIP,'-','') AS [Beneficiary Address (zip code)],                                            
 A_LST.AGENCY_EMAIL AS [Beneficiary e-mail address],                                          
                                           
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
 A_LST.BROKER_BANK_NUMBER                                            
 ELSE ''END AS [Beneficiary Bank Number], --PUT CONDITION                                            
                                           
 CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                            
   CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                           
   WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                               
   ELSE A_LST.BANK_BRANCH                                              
   END                                           
 ELSE ''END AS [Beneficiary Bank Branch],--BRACH                                            
                                              
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                            
      --itrack 1750                  
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                             
       WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                    
  ELSE '' END                                              
  ELSE ''END AS [Beneficiary Bank Branch Verifier Digit],                                             
                                          
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN   A_LST.BANK_ACCOUNT_NUMBER                                          
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                             
  ----itrack 1750                                          
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                                 
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                            
  ELSE ''END AS [Beneficiary Bank Account number],                                              
                                                
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                          
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
   --itrack 1750                                          
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                                  
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE ''                                              
  END                                          
  ELSE ''END AS [Beneficiary Bank Account Verifier Digit],                                            
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
    ELSE ''                                          
    END AS [Beneficiary Bank Account type],--                                            
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
  WHEN ISNULL(A_LST.BROKER_TYPE,'11110') ='14725' THEN 'G' END AS   [Payee Party Type],                                             
  '0' AS [Foreign2], -- REPEAT                             
  '004' AS [Payee Class],  --Handled at front end as per sheet                                          
  A_LST.AGENCY_DISPLAY_NAME AS [Payee Name],                                            
  A_LST.BROKER_CPF_CNPJ AS [Payee ID (CPF or CNPJ)],                        
  '' AS [Payee foreign ID], -- DOUBT                                            
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                            
  A_LST.BROKER_BANK_NUMBER                                           
  END AS [Payee Bank Number],                                            
                                             
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('-' ,A_LST.BANK_BRANCH)-1)                                    
    WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('/' ,A_LST.BANK_BRANCH)-1)                                                           
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX(',' ,A_LST.BANK_BRANCH)-1)                                                       
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,1,CHARINDEX('.' ,A_LST.BANK_BRANCH)-1)                                                   
  ELSE A_LST.BANK_BRANCH                                      
  END                                           
  ELSE '' END AS [Payee Bank Branch],--BRACH                                               
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                               
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('-' ,A_LST.BANK_BRANCH)+1,1)                                           
   WHEN CHARINDEX('/' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('/' ,A_LST.BANK_BRANCH)+1,1)                                      
    WHEN CHARINDEX(',' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX(',' ,A_LST.BANK_BRANCH)+1,1)                                                             
    WHEN CHARINDEX('.' ,A_LST.BANK_BRANCH) > 0 THEN SUBSTRING(A_LST.BANK_BRANCH,CHARINDEX('.' ,A_LST.BANK_BRANCH)+1,1)                                                  
  ELSE ''END                                              
   ELSE '' END AS [Payee Bank Branch Verifier Digit],                                             
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN  A_LST.BANK_ACCOUNT_NUMBER                                           
  --CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                            
  --WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,1,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)-1)                                               
  --ELSE A_LST.BANK_ACCOUNT_NUMBER                                              
  --END                                           
  ELSE '' END AS [Payee Bank Account no.],                                              
                                            
  CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN                                              
  CASE WHEN CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('-' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                           
    WHEN CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('/' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                            
    WHEN CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX(',' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)          
    WHEN CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER) > 0 THEN SUBSTRING(A_LST.BANK_ACCOUNT_NUMBER,CHARINDEX('.' ,A_LST.BANK_ACCOUNT_NUMBER)+1,2)                                                                              
  ELSE '' END                                          
  ELSE '' END AS [Payee Bank Account Verifier digit],                                           
                                             
  CASE WHEN  A_LST.ACCOUNT_TYPE = '100' THEN  '01'                                          
    WHEN  A_LST.ACCOUNT_TYPE = '101' THEN  '02'                                          
   ELSE ''                                          
    END AS [Payee Bank Account Type],                                            
  '1' AS [Payee Bank Account Currency],                                            
  A_smt_det.ROW_ID AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?                                            
  '00001' AS [Carrier Code],                                            
 -- CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END    AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED                                           
  '00030' AS [Carrier Policy Branch Code],                                          
  CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN '01030'                                           
       WHEN  A_smt_det.COMM_TYPE = 'PRLBR' THEN '01035'                                           
       WHEN A_smt_det.COMM_TYPE = 'ENFEE' THEN '01040' END  AS [EVENT CODE],                                            
  '060' AS [Operation Type],                                            
   CASE WHEN ISNULL(A_LST.ALLOWS_EFT,0) =  10963 THEN '001'                                             
        WHEN ISNULL(A_LST.ALLOWS_EFT,0) = 10964 THEN '005'                                           
        ELSE '005' END AS [Payment Method], -- aLLOW EFT                                            
  '' AS [Document number],                                            
  '' AS [Document number - serial number],                                        
                                              
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice issuance date],                                           
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','')  AS [Invoice due date],                                            
                       
  '1' AS [Policy Currency],                                            
  '' AS [Exchange rate],                                            
  --CASE WHEN  ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 < 0 THEN '-'                                              
  --      ELSE '' END AS [Commission amount sign],                                          
  NULL AS [Commission amount sign],                                            
  --ISNULL(A_smt_det.COMMISSION_AMOUNT,0) * -1 AS [Commission amount], -- TOTAL_DUE                        
  ABS(ISNULL(((A_smt_det.AMOUNT_FOR_CALCULATION+ACT_PID.INTEREST_AMOUNT)*(AP_LST.FEES_PERCENT))/100,0)) AS [Commission amount],                                            
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
  --changes by naveen, changes by naveen, itrack 1750, refer sheet v12,                            
  --CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Agente de Pagamento de Comissão'  --'Broker Commission Payment'                                           
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Prolabore Pagamento de Comissão'   --'Prolabore Commission Payment'                  
  --     WHEN A_LST.AGENCY_TYPE_ID = 14702 AND A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Taxa de inscrição' END AS [Payment Description],                                          
    CASE WHEN  A_LST.AGENCY_TYPE_ID = 14701 AND A_smt_det.COMM_TYPE = 'COMM' THEN 'Pagamento de comissão de corretagem'  --'Broker Commission Payment'                                           
       WHEN  A_smt_det.COMM_TYPE = 'PRLBR' THEN 'Pagamento de pró-labore'   --'Prolabore Commission Payment'                                       
       WHEN  A_smt_det.COMM_TYPE = 'ENFEE' THEN 'Pagamento de agenciamento' END AS [Payment Description],                                          
                                     
                                                 
 CASE WHEN LEN(D_LST.BRANCH_CODE)=1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END  AS [Policy Branch Code], -- NOT CLEAR                                            
  '' AS [Profit Center code], --Profit Center AS PER DISCUSSION                                           
  NULL AS [A_DEFINIR1],                                            
  NULL AS [A_DEFINIR2],                                           
 --tfs id-2246,modified by naveen                                          
  case when   L_LST.SUSEP_LOB_CODE='0520'                                          
  then '0982'  else              
  L_LST.SUSEP_LOB_CODE end AS [Policy Accounting LOB],                                           
  NULL AS [A_DEFINIR3], --                                             
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)                                             
  ELSE '' END  AS [Policy number (first 15 digits)],                                             
                                              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)                                             
  ELSE '' END  AS [Policy Number (remaining digits)],                                            
                                              
  P_ENDOR.ENDORSEMENT_NO  AS [Endorsement number], -- NEED TO CLEAR FOR WHICH VALUE?                                            
  A_smt_det.INSTALLMENT_NO AS [Installment number], --NOT CLEAR                                            
  NULL AS [A_DEFINIR4],                                             
  NULL AS [A_DEFINIR5],                                          
  CONVERT(VARCHAR,A_smt_det.PAYMENT_DATE, 103) AS [Installment payment date],                                            
  RTRIM(C_APP_LST.FIRST_NAME+' '+ CASE WHEN  len(ISNULL(C_APP_LST.MIDDLE_NAME,''))<1  THEN ''                   
     ELSE C_APP_LST.MIDDLE_NAME +' '    END  +  ISNULL(C_APP_LST.LAST_NAME,'')) AS [Applicant/ Co Applicant name],                                            
  A_smt_det.AMOUNT_FOR_CALCULATION AS [Premium amount],                                            
   --A_smt_det.COMMISSION_RATE AS [Commission percentage],                                  
   --Modified by naveen , itrack 1750 , calcuated amouint based on the new requirement issue no 12 ,Comissão_Commission                           
   cast((ABS(A_smt_det.COMMISSION_AMOUNT)/A_smt_det.AMOUNT_FOR_CALCULATION)*100 as decimal(25,4)) AS [Commission percentage],                                     
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
  NULL AS [No da Conta Corrente],                                            
  NULL AS [Cheque number],           
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
         
 FROM ACT_AGENCY_STATEMENT_DETAILED A_smt_det  WITH(NOLOCK)                                          
INNER JOIN  MNT_AGENCY_LIST A_LST  WITH(NOLOCK)                                           
   ON A_smt_det.AGENCY_ID = A_LST.AGENCY_ID                                               
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK)                                           
   ON --C_LST.AGENCY_ID = A_smt_det.AGENCY_ID  AND                                           
    A_smt_det.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
   AND A_smt_det.POLICY_ID = C_LST.POLICY_ID                                             
   AND A_smt_det.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                          
   --AS PER ITRACK 1662----                                          
                                  
INNER JOIN MNT_DIV_LIST D_LST WITH(NOLOCK)                                            
 ON D_LST.DIV_ID = C_LST.DIV_ID                              
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)                                              
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB                                             
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)                                              
 ON P_ENDOR.CUSTOMER_ID = A_smt_det.CUSTOMER_ID                                              
    AND P_ENDOR.POLICY_ID = A_smt_det.POLICY_ID                                              
    AND P_ENDOR.POLICY_VERSION_ID = A_smt_det.POLICY_VERSION_ID                                                 
--uncommented by naveen          
                                     
--    AND AP_LST.IS_PRIMARY_APPLICANT = 1                                            
INNER  JOIN ACT_CUSTOMER_OPEN_ITEMS ACOI WITH(NOLOCK)                                  
   ON ACOI.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND ACOI.POLICY_ID = C_LST.POLICY_ID                                              
    AND ACOI.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                                        
    AND ACOI.IDEN_ROW_ID =  A_smt_det.CUSTOMER_OPEN_ITEM_ID                                         
INNER JOIN CLT_APPLICANT_LIST C_APP_LST  WITH(NOLOCK)                                           
    ON C_APP_LST.APPLICANT_ID = ACOI.PAYOR_ID            
    AND  C_APP_LST.CUSTOMER_ID = ACOI.CUSTOMER_ID                                     
INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH(NOLOCK)                                          
  --ON CAST(A_LST.AGENCY_STATE AS SMALLINT) = MCSL.STATE_ID                          
  -- Changed for itrack #1750 issue 9                                          
 ON (A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5)) or  A_LST.AGENCY_STATE  = MCSL.STATE_CODE)                                 
 --A_LST.AGENCY_STATE  = CAST(MCSL.STATE_ID AS NVARCHAR(5))                                            
 AND A_LST.AGENCY_COUNTRY = MCSL.COUNTRY_ID                          
 --ADDED BY NAVEEN, ITRACK 1750, SHEET V18, POINT 15                        
 INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS ACT_PID                        
 ON ACT_PID.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
  AND ACT_PID.POLICY_ID = C_LST.POLICY_ID                                             
  AND ACT_PID.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                 
  AND ACT_PID.INSTALLMENT_NO   =A_smt_det.INSTALLMENT_NO                     
 and ACT_PID.RELEASED_STATUS='Y'           
 INNER JOIN POL_APPLICANT_LIST AP_LST WITH(NOLOCK)                                            
   ON AP_LST.CUSTOMER_ID = C_LST.CUSTOMER_ID                                            
    AND AP_LST.POLICY_ID = C_LST.POLICY_ID                                            
    AND AP_LST.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID            
    AND  AP_LST.APPLICANT_ID   =  ACT_PID.CO_APPLICANT_ID         
    AND  AP_LST.APPLICANT_ID   =  C_APP_LST.APPLICANT_ID         
        
   INNER JOIN POL_REMUNERATION PR WITH(NOLOCK)                                          
 ON PR.CUSTOMER_ID = C_LST.CUSTOMER_ID                                              
    AND PR.POLICY_ID = C_LST.POLICY_ID                                               
    AND PR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID                            
    AND PR.BROKER_ID = A_smt_det.AGENCY_ID         
    AND PR.CO_APPLICANT_ID = AP_LST.APPLICANT_ID        
    AND PR.COMMISSION_TYPE = 44                               
   --TILL HERE                                
   WHERE  ISNULL(A_smt_det.IS_COMMISSION_PROCESS,'N') <>'Y'                                               
        AND A_LST.AGENCY_TYPE_ID IN (14701,14702)                                          
        AND  A_smt_det.COMMISSION_AMOUNT < 0                                          
        AND A_smt_det.COMM_TYPE ='ENFEE'                                         
        --CONDITION IS BASED ON #ITRACK 1284                                          
        AND CONVERT(date,A_LST.TERMINATION_DATE,101) >= convert(date,GETDATE(),101)                       
       AND C_LST.TRANSACTION_TYPE=14560          
                 
       ) AS COMMISSION                                          
                                            
  WHERE [EVENT CODE] IS NOT NULL                                           
                                            
                                            
  --------------------PAGNET_EXPORT_TABLE----------                                          
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
 ,[Beneficiary Bank Branch Verifier Digit]                                   --,[Beneficiary Bank Account number]                           
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
 ,[Broker SUSEP Code]                                          
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
 ,substring(cast([Commission amount] as varchar),0, CHARINDEX('.',cast([Commission amount] as varchar))+3)                                
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
 ,[Payment Description]                                          
 ,[Policy Branch Code]                                          
 ,[Profit Center code]                                          
 ,[A_DEFINIR1]                      
 ,[A_DEFINIR2]                                         
 ,[Policy Accounting LOB]                                          
 ,[A_DEFINIR3]                                          
 ,[Policy number (first 15 digits)]                                          
 ,[Policy Number (remaining digits)]                                          
 ,[Endorsement number]                                          
 ,[Installment number]                                          
 ,[A_DEFINIR4]                                          
 ,[A_DEFINIR5]                                          
 ,[Installment payment date]                                          
 ,[Applicant/ Co Applicant name]                                          
 ,[Premium amount]                                          
 ,[Commission percentage]                                          
 ,[A_DEFINIR6]                                          
 ,[A_DEFINIR7]                                          
 ,[A_DEFINIR8]                                          
 ,[Movement type]                                          
 ,[Commission Payment date]                                          
 ,[Commission Paid Amount sign]                                          
 ,[Commission paid amount]                                          
 ,[Payment currency]                                      
 ,[Bank account number]                                          
 ,[Bank branch code]                                   
 ,[No da Conta Corrente]                                          
 ,[Cheque number]                                          
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
   INCONSISTENCY_2,                            INCONSISTENCY_3,                                          
  INCONSISTENCY_4,                                          
   INCONSISTENCY_5,                                          
   'Record Generated'                                          
     FROM                                               
                                              
                                              
      #TEMP_TABLE                                          
             
     SELECT * FROM #TEMP_TABLE           
                        
    END 
  
      
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  

GO

