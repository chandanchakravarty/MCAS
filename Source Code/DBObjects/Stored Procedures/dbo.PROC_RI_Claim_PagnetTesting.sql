IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_RI_Claim_PagnetTesting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_RI_Claim_PagnetTesting]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC PROC_RI_Claim    
CREATE PROC PROC_RI_Claim_PagnetTesting           
AS            
BEGIN      
      
      
SELECT ROW_NUMBER()  OVER (ORDER BY [Payment Description],[Sequence_of_record_0]) as  [Sequence of record],  *      
INTO #TEMP_TABLE      
 FROM (      
       
--RI_CLAIM QUERY       
SELECT '05' AS [Interface code],      
C_RSRV.CLAIM_ID AS CLAIM_ID,      
C_RSRV.ACTIVITY_ID AS ACTIVITY_ID,      
C_RSRV.COMP_ID AS COMP_ID,      
'CLM_ACTIVITY_CO_RI_BREAKDOWN' AS TABLE_NAME,      
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID,C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID,C_RSRV.COMP_ID ) AS [Sequence_of_record_0],        
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
CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION        
         
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH           
             
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END  ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],         
        
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Account number],          
            
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END AS [Beneficiary Bank Account type],       
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
  NULL AS [Código SUSEP],       
  NULL AS [No do Funcionário],        
  NULL AS [Cód da Filial],        
  NULL AS [Cód do Centro de Custo],        
  NULL AS [Data de Nascimento],        
  'J' AS   [Payee Party Type],         
  '0' AS [Foreign2], -- REPEAT        
  '064' AS [Payee Class],        
  MR_LST.REIN_COMAPANY_NAME AS [Payee Name],        
  MR_LST.CARRIER_CNPJ AS [Payee ID (CPF or CNPJ)],        
  '' AS [Payee foreign ID],       
          
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],        
          
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Payee Bank Branch],--BRACH           
             
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],         
        
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END       
  ELSE '' END AS [Payee Bank Account no.],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Account Verifier digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END  AS [Payee Bank Account Type],         
  '1' AS [Payee Bank Account Currency],        
  C_RSRV.[PAYMENT ID] AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?        
  '00001' AS [Carrier Code],      
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED        
  C_RSRV.ADJUSTER_CODE AS [Carrier Policy Branch Code],       
  C_RSRV.[EVENT CODE], -- nEED TO DISCUSS       
            
  CASE WHEN C_RSRV.COMP_TYPE = 'CO'  THEN '070'--CO       
    WHEN C_RSRV.COMP_TYPE ='RI' THEN '090' --RI      
   END AS [Operation Type],      
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN '001' -- aLLOW EFT        
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'      
   ELSE '005' END AS [Payment Method], -- aLLOW EFT        
  '' AS [Document number],        
  '' AS [Document number - serial number],        
          
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],      
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],        
         
  '1' AS [Policy Currency],        
  NULL AS [Exchange rate],        
  --CASE WHEN  C_RSRV.PAYMENT_AMOUNT < 0 THEN '-'          
  --      ELSE '' END AS [Co amount sign],      
  NULL AS [Co amount sign],        
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Co amount], -- TOTAL_DUE        
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
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Valor Isento CSLL/COFINS/PIS],  --      
  NULL AS [Sinal do Valor Tributável],  --83      
  NULL AS [Valor Tributável CSLL/COFINS/PIS],        
  NULL AS [Sinal do Valor IR],         
  NULL AS [Valor IR],        
  NULL AS [Sinal do Valor ISS],        
  NULL AS [Valor ISS],        
  NULL AS [Sinal do Valor Desconto],        
  NULL AS [Valor Desconto],        
  NULL AS [Sinal do Valor Multa],        
  NULL AS [Valor Multa],          
  CASE --WHEN   C_RSRV.[EVENT CODE] ='01005' THEN 'Indenização por pedido normal para aceite CO'      
  --  WHEN   C_RSRV.[EVENT CODE] ='01011' THEN 'Indenização por reivindicação legal para aceite CO'      
  --  WHEN   C_RSRV.[EVENT CODE] ='01012' THEN 'Despesa com pedido normal de CO aceite'      
  --  WHEN   C_RSRV.[EVENT CODE] ='01014' THEN 'Despesa com ação judicial para o CO aceite'      
  --  WHEN   C_RSRV.[EVENT CODE] ='01017' THEN 'Professional custa serviço para reivindicar normal para o CO aceite'      
  --  WHEN   C_RSRV.[EVENT CODE] ='01019' THEN 'Professional custa serviço de reivindicação legal para o CO aceite'      
    WHEN   C_RSRV.[EVENT CODE] ='01080' THEN 'Sub-rogação de crédito normal para cosseguro cedido'      
    WHEN   C_RSRV.[EVENT CODE] ='01085' THEN 'Sub-rogação legal para a pretensão de cosseguro cedido'      
    WHEN   C_RSRV.[EVENT CODE] ='01090' THEN 'Salvamento de transferência de crédito normal para cosseguro cedido'      
    WHEN   C_RSRV.[EVENT CODE] ='01095' THEN 'Salvamento de transferência de ação por cosseguro cedido'      
  END       
   AS [Payment Description], --NEED TO DISCUSS       
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
   AS [Policy Branch Code], -- NOT CLEAR        
  NULL AS [Profit Center code], --Profit Center code NOT CLEAR        
  NULL AS [A_DEFINIR1],        
  NULL AS [A_DEFINIR2],        
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],        
  NULL AS [A_DEFINIR3], --           
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)         
  ELSE '' END AS [Apólice],               
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)         
  ELSE '' END AS [Apólice (cont)],          
  P_ENDOR.ENDORSEMENT_NO  AS [Endosso],           
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
  NULL AS [Exchange rate2],      
  NULL AS INCONSISTENCY_1,      
  NULL AS INCONSISTENCY_2,      
  NULL AS INCONSISTENCY_3,      
  NULL AS INCONSISTENCY_4,      
  NULL AS INCONSISTENCY_5      
  FROM (SELECT TMP1.CLAIM_ID,TMP1.ACTIVITY_ID,      
  TMP1.COMP_ID,TMP1.COMP_TYPE,TMP1.[EVENT CODE],      
  CAST(TMP1.CLAIM_ID as varchar) + 'sep' + CAST(TMP1.ACTIVITY_ID as varchar)      
  + 'sep' + CAST(TMP1.COMP_ID as varchar)   AS [PAYMENT ID],      
  SUM(PAYMENT_AMOUNT) as PAYMENT_AMOUNT,      
  TMP1.CUSTOMER_ID,           
  TMP1.POLICY_ID,      
  TMP1.POLICY_VERSION_ID,       
  TMP1.ADJUSTER_CODE      
   FROM      
    (SELECT       
    TMP.CLAIM_ID,TMP.ACTIVITY_ID,      
    TMP.COMP_ID,TMP.COMP_TYPE,      
    C_ACT.IS_LEGAL,      
    CO_INSURANCE_TYPE,      
    TMP.COVERAGE_ID,      
    CASE --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549       
        --AND  TMP.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
        --THEN '01005'      
      --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549       
      --AND  TMP.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
      -- THEN '01011'      
            
      --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549         
      -- AND  TMP.COVERAGE_ID =50017       
      -- THEN '01012'      
             
      --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549         
      -- AND TMP.COVERAGE_ID =50017       
      -- THEN '01014'       
            
      --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE =14549         
      -- AND TMP.COVERAGE_ID =50018       
      -- THEN '01017'      
            
      --WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549          
      -- AND TMP.COVERAGE_ID =50018       
      -- THEN '01019'      
             
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE= 14548         
       AND TMP.COVERAGE_ID =50021       
       THEN '01080'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE=14548          
       AND TMP.COVERAGE_ID =50021       
       THEN '01085'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14548         
       AND TMP.COVERAGE_ID =50019       
       THEN '01090'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14548          
       AND TMP.COVERAGE_ID =50019       
       THEN '01095' END             
      AS [EVENT CODE],      
      C_INFO.CUSTOMER_ID,           
      C_INFO.POLICY_ID,      
      C_INFO.POLICY_VERSION_ID,      
      MUL.ADJUSTER_CODE,                       
      TMP.PAYMENT_AMOUNT       
      FROM      
       (SELECT CAR.CLAIM_ID,CAR.ACTIVITY_ID,CACRB.COMP_ID,COMP_TYPE,      
         CPC.COVERAGE_CODE_ID AS  COVERAGE_ID ,       
         (CACRB.PAYMENT_AMT) AS PAYMENT_AMOUNT      
        FROM        
          CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)      
           INNER JOIN CLM_PRODUCT_COVERAGES CPC       
              ON  CPC.CLAIM_ID = CAR.CLAIM_ID      
              AND CPC.CLAIM_COV_ID = CAR.COVERAGE_ID      
          INNER JOIN CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB WITH(NOLOCK)       
             ON CAR.CLAIM_ID= CACRB.CLAIM_ID       
             AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID       
             AND CAR.RESERVE_ID = CACRB.RESERVE_ID      
        WHERE  ISNULL(CACRB.IS_COMMISSION_PROCESS,'N')<>'Y'       
           AND CACRB.PAYMENT_AMT > 0                                             
      ) TMP      
     INNER JOIN CLM_ACTIVITY C_ACT WITH(NOLOCK)      
       ON C_ACT.CLAIM_ID =  TMP.CLAIM_ID      
       AND C_ACT.ACTIVITY_ID =  TMP.ACTIVITY_ID             
     INNER JOIN CLM_CLAIM_INFO C_INFO WITH(NOLOCK)         
       ON C_INFO.CLAIM_ID = TMP.CLAIM_ID      
     INNER JOIN CLM_ADJUSTER CA WITH(NOLOCK)      
      ON  CA.ADJUSTER_ID = C_INFO.ADJUSTER_ID      
     INNER JOIN MNT_USER_LIST MUL WITH(NOLOCK)      
      ON MUL.USER_ID = CA.USER_ID        
              
     ) TMP1      
    GROUP BY      
    TMP1.CLAIM_ID,TMP1.ACTIVITY_ID,      
    TMP1.COMP_ID,TMP1.COMP_TYPE,TMP1.ADJUSTER_CODE,TMP1.[EVENT CODE],TMP1.CUSTOMER_ID,           
     TMP1.POLICY_ID,      
     TMP1.POLICY_VERSION_ID )  AS C_RSRV      
INNER JOIN CLM_PARTIES C_PT WITH(NOLOCK)      
  ON C_RSRV.CLAIM_ID = C_PT.CLAIM_ID      
INNER JOIN MNT_REIN_COMAPANY_LIST MR_LST WITH(NOLOCK)      
  ON MR_LST.REIN_COMAPANY_ID = C_PT.SOURCE_PARTY_ID    --JOIN ON COMPANY ID            
INNER JOIN CLM_PAYEE C_P  WITH(NOLOCK)        
  ON C_RSRV.CLAIM_ID = C_P.CLAIM_ID         
  AND  C_P.ACTIVITY_ID = C_RSRV.ACTIVITY_ID       
  AND C_P.PARTY_ID = C_PT.PARTY_ID      
INNER JOIN  POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)         
  ON C_LST.CUSTOMER_ID = C_RSRV.CUSTOMER_ID          
  AND C_LST.POLICY_ID = C_RSRV.POLICY_ID          
  AND C_LST.POLICY_VERSION_ID = C_RSRV.POLICY_VERSION_ID       
  AND C_LST.CO_INSURANCE IN (14549,14548)  --IS NOT NULL      
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)         
  ON P_ENDOR.CUSTOMER_ID = C_LST.CUSTOMER_ID          
  AND P_ENDOR.POLICY_ID = C_LST.POLICY_ID          
  AND P_ENDOR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID       
INNER JOIN  mnt_div_list D_LST   WITH(NOLOCK)       
  ON D_LST.DIV_ID = C_LST.DIV_ID       
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)             
  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB      
        
 -- ) AS RI_CLAIM      
 --WHERE [EVENT CODE] IS NOT NULL       
       
 UNION      
       
 SELECT '05' AS [Interface code],      
C_RSRV.CLAIM_ID AS CLAIM_ID,      
C_RSRV.ACTIVITY_ID AS ACTIVITY_ID,      
MR_LST.REIN_COMAPANY_ID AS COMP_ID,      
'CLM_ACTIVITY_RESERVE' AS TABLE_NAME,      
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID,C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID,MR_LST.REIN_COMAPANY_ID ) AS [Sequence_of_record_0],        
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
CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION        
         
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH           
             
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END  ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],         
        
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Account number],          
            
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END AS [Beneficiary Bank Account type],       
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
  NULL AS [Código SUSEP],       
  NULL AS [No do Funcionário],        
  NULL AS [Cód da Filial],        
  NULL AS [Cód do Centro de Custo],        
  NULL AS [Data de Nascimento],        
  'J' AS   [Payee Party Type],         
  '0' AS [Foreign2], -- REPEAT        
  '064' AS [Payee Class],        
  MR_LST.REIN_COMAPANY_NAME AS [Payee Name],        
  MR_LST.CARRIER_CNPJ AS [Payee ID (CPF or CNPJ)],        
  '' AS [Payee foreign ID],       
          
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],        
          
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Payee Bank Branch],--BRACH           
             
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],         
        
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END       
  ELSE '' END AS [Payee Bank Account no.],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Account Verifier digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END  AS [Payee Bank Account Type],         
  '1' AS [Payee Bank Account Currency],        
  --C_RSRV.[PAYMENT ID] AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?        
   CAST(C_RSRV.CLAIM_ID as varchar) + 'sep' + CAST(C_RSRV.ACTIVITY_ID as varchar)      
  + 'sep' + CAST(MR_LST.REIN_COMAPANY_ID as varchar)   AS [PAYMENT ID],      
  '00001' AS [Carrier Code],      
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED        
  CA.ADJUSTER_CODE AS [Carrier Policy Branch Code],       
        
  CASE WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549       
        AND  CPC.COVERAGE_CODE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
THEN '01005'      
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549       
      AND  CPC.COVERAGE_CODE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
       THEN '01011'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549         
       AND  CPC.COVERAGE_CODE_ID =50017       
       THEN '01012'      
             
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549         
       AND CPC.COVERAGE_CODE_ID =50017       
       THEN '01014'       
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE =14549         
       AND CPC.COVERAGE_CODE_ID =50018       
       THEN '01017'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549          
       AND CPC.COVERAGE_CODE_ID =50018       
       THEN '01019'      
     END AS [EVENT CODE],      
        
  --C_RSRV.[EVENT CODE], -- nEED TO DISCUSS       
  '070' AS  [Operation Type],      
          
  --CASE WHEN C_RSRV.COMP_TYPE = 'CO'  THEN '070'--CO       
  --  WHEN C_RSRV.COMP_TYPE ='RI' THEN '090' --RI      
  -- END AS [Operation Type],      
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN '001' -- aLLOW EFT        
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'      
   ELSE '005' END AS [Payment Method], -- aLLOW EFT        
  '' AS [Document number],        
  '' AS [Document number - serial number],        
          
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],      
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],        
         
  '1' AS [Policy Currency],        
  NULL AS [Exchange rate],        
  --CASE WHEN  C_RSRV.PAYMENT_AMOUNT < 0 THEN '-'          
  --      ELSE '' END AS [Co amount sign],      
  NULL AS [Co amount sign],        
  ABS(C_RSRV.PAYMENT_AMOUNT * C_PT.PARTY_PERCENTAGE/100) AS [Co amount], -- TOTAL_DUE        
  NULL AS [Sinal do Valor Isento IR],        
  ABS(C_RSRV.PAYMENT_AMOUNT * C_PT.PARTY_PERCENTAGE/100) AS [Valor Isento IR],        
  NULL AS [Sinal do Valor Tributável IR],        
  NULL AS [Valor Tributável IR],        
  NULL AS [Sinal do Valor Isento ISS],        
  ABS(C_RSRV.PAYMENT_AMOUNT * C_PT.PARTY_PERCENTAGE/100) AS [Valor Isento ISS],        
  NULL AS [Sinal do Valor Tributável ISS],        
  NULL AS [Valor Tributável ISS],        
  NULL AS [Sinal do Valor Isento INSS],        
  ABS(C_RSRV.PAYMENT_AMOUNT * C_PT.PARTY_PERCENTAGE/100) AS [Valor Isento INSS],        
  NULL AS [Sinal do Valor Tributável INSS],        
  NULL AS [Valor Tributável INSS],        
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],        
  ABS(C_RSRV.PAYMENT_AMOUNT * C_PT.PARTY_PERCENTAGE/100) AS [Valor Isento CSLL/COFINS/PIS],  --      
  NULL AS [Sinal do Valor Tributável],  --83      
  NULL AS [Valor Tributável CSLL/COFINS/PIS],        
  NULL AS [Sinal do Valor IR],         
  NULL AS [Valor IR],        
  NULL AS [Sinal do Valor ISS],        
  NULL AS [Valor ISS],        
  NULL AS [Sinal do Valor Desconto],        
  NULL AS [Valor Desconto],        
  NULL AS [Sinal do Valor Multa],        
  NULL AS [Valor Multa],        
  CASE WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549       
        AND  CPC.COVERAGE_CODE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
        THEN 'Indenização por pedido normal para aceite CO' --'01005'      
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549       
      AND  CPC.COVERAGE_CODE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
         THEN 'Indenização por reivindicação legal para aceite CO'--'01011'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549         
       AND  CPC.COVERAGE_CODE_ID =50017       
       THEN 'Despesa com pedido normal de CO aceite' --'01012'      
             
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549         
       AND CPC.COVERAGE_CODE_ID =50017       
       THEN 'Despesa com ação judicial para o CO aceite'  --'01014'       
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE =14549         
       AND CPC.COVERAGE_CODE_ID =50018       
       THEN 'Professional custa serviço para reivindicar normal para o CO aceite' --'01017'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549          
       AND CPC.COVERAGE_CODE_ID =50018       
       THEN 'Professional custa serviço de reivindicação legal para o CO aceite'  --'01019'      
     END AS [Payment Description],      
          
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
   AS [Policy Branch Code], -- NOT CLEAR        
  NULL AS [Profit Center code], --Profit Center code NOT CLEAR        
  NULL AS [A_DEFINIR1],        
  NULL AS [A_DEFINIR2],        
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],        
  NULL AS [A_DEFINIR3], --           
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)         
  ELSE '' END AS [Apólice],               
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)         
  ELSE '' END AS [Apólice (cont)],          
  P_ENDOR.ENDORSEMENT_NO  AS [Endosso],           
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
  NULL AS [Exchange rate2],      
  NULL AS INCONSISTENCY_1,      
  NULL AS INCONSISTENCY_2,      
  NULL AS INCONSISTENCY_3,      
  NULL AS INCONSISTENCY_4,      
  NULL AS INCONSISTENCY_5      
        
        
  FROM CLM_ACTIVITY_RESERVE C_RSRV  WITH(NOLOCK)       
INNER JOIN CLM_PRODUCT_COVERAGES CPC WITH(NOLOCK)        
 ON C_RSRV.CLAIM_ID = CPC.CLAIM_ID        
 AND C_RSRV.COVERAGE_ID = CPC.CLAIM_COV_ID          
INNER JOIN CLM_PARTIES C_PT WITH(NOLOCK)        
 ON C_RSRV.CLAIM_ID = C_PT.CLAIM_ID  and  C_PT.PARTY_TYPE_ID = 618         
INNER JOIN CLM_ACTIVITY C_ACT WITH(NOLOCK)        
  ON C_ACT.CLAIM_ID =  C_RSRV.CLAIM_ID        
  AND C_ACT.ACTIVITY_ID =  C_RSRV.ACTIVITY_ID        
INNER JOIN CLM_CLAIM_INFO C_INFO  WITH(NOLOCK)          
 ON C_INFO.CLAIM_ID = C_PT.CLAIM_ID        
INNER JOIN MNT_REIN_COMAPANY_LIST  MR_LST  WITH(NOLOCK)      
  ON MR_LST.REIN_COMAPANY_ID = C_PT.SOURCE_PARTY_ID            
INNER JOIN  POL_CUSTOMER_POLICY_LIST C_LST  WITH(NOLOCK)          
 ON C_LST.CUSTOMER_ID = C_INFO.CUSTOMER_ID            
 AND C_LST.POLICY_ID = C_INFO.POLICY_ID            
 AND C_LST.POLICY_VERSION_ID = C_INFO.POLICY_VERSION_ID         
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR  WITH(NOLOCK)           
  ON P_ENDOR.CUSTOMER_ID = C_LST.CUSTOMER_ID            
  AND P_ENDOR.POLICY_ID = C_LST.POLICY_ID            
  AND P_ENDOR.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID            
INNER JOIN  mnt_div_list D_LST WITH(NOLOCK)           
  ON D_LST.DIV_ID = C_LST.DIV_ID        
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)               
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB       
INNER JOIN CLM_ADJUSTER CA WITH(NOLOCK)        
  ON  CA.ADJUSTER_ID = C_INFO.ADJUSTER_ID        
INNER JOIN MNT_USER_LIST MUL WITH(NOLOCK)        
  ON MUL.USER_ID = CA.USER_ID             
 WHERE C_LST.CO_INSURANCE =14549       
 AND  C_RSRV.PAYMENT_AMOUNT <> 0      
 AND ISNULL(C_RSRV.IS_COMMISSION_PROCESS,'N')<>'Y'       
       
       
       
       
 UNION -- COI QUERY        
       
      
--SELECT ROW_NUMBER() OVER (ORDER BY [Sequence of record_0]) AS [Sequence of record],*  FROM(      
       
SELECT '05' AS [Interface code],      
NULL AS CLAIM_ID,      
NULL AS ACTIVITY_ID,      
NULL AS COMP_ID,      
'ACT_COI_STATEMENT_DETAILED' AS TABLE_NAME,        
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID ) AS [Sequence_of_record_0],        
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
 CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION        
       
 CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
 CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH           
             
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN           
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],         
          
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END  AS [Beneficiary Bank Account number],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
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
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN         
   MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],        
         
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Payee Bank Branch],--BRACH           
             
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN           
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],         
          
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END AS [Payee Bank Account no.],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN          
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Account Verifier digit],        
        
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END AS [Payee Bank Account Type],        
  '1' AS [Payee Bank Account Currency],        
  CAST(O_ITMS.ROW_ID AS VARCHAR(25)) AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?        
  '00001' AS [Carrier Code],      
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED       
   '00030' AS [Carrier Policy Branch Code],      
   CASE WHEN C_LST.CO_INSURANCE = 14549 THEN '01025' --FOLLOWER      
  WHEN C_LST.CO_INSURANCE = 14548 THEN '01075' --LEADER      
   ELSE '' END   AS [EVENT CODE],       
         
  '070' AS [Operation Type],      
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN '001' -- aLLOW EFT        
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'        
  ELSE '005' END AS [Payment Method],       
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
  'Pagamento CO' AS [Payment Description],       
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END      
   AS [Policy Branch Code],       
  NULL AS [Profit Center code],       
  NULL AS [A_DEFINIR1],        
  NULL AS [A_DEFINIR2],        
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],        
  NULL AS [A_DEFINIR3], --           
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)         
  ELSE '' END AS [Apólice], --C_LST              
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)         
  ELSE '' END AS [Apólice (cont)],          
  P_ENDOR.ENDORSEMENT_NO AS [Endosso],           
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
  NULL AS [Exchange rate2],      
  NULL AS INCONSISTENCY_1,      
  NULL AS INCONSISTENCY_2,      
  NULL AS INCONSISTENCY_3,      
  NULL AS INCONSISTENCY_4,      
  NULL AS INCONSISTENCY_5        
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
WHERE --ISNULL(O_ITMS.IS_COMMISSION_PROCESS,'N')<> 'Y' AND 
  C_LST.CO_INSURANCE IN (14549,14548)      
  AND O_ITMS.COMMISSION_AMOUNT < 0      
  --)  CO WHERE [EVENT CODE] IS NOT NULL        
        
        
   UNION --RI QUERY      
        
--SELECT ROW_NUMBER() OVER (ORDER BY [Sequence of record_0]) AS [Sequence of record],*  FROM (       
       
SELECT '05' AS [Interface code],      
NULL AS CLAIM_ID,      
NULL AS ACTIVITY_ID,      
NULL AS COMP_ID,      
'POL_REINSURANCE_BREAKDOWN_DETAILS' AS TABLE_NAME,        
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID) AS [Sequence_of_record_0],        
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
       
 CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
 MR_LST.BANK_NUMBER ELSE '' END AS [Beneficiary Bank Number], --PUT CONDITION        
         
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Branch],--BRACH           
             
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END  ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],         
        
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Account number],          
            
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
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
        
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  MR_LST.BANK_BRANCH_NUMBER ELSE '' END AS [Payee Bank Number],        
          
   CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,1,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)-1)           
  ELSE MR_LST.BANK_BRANCH_NUMBER          
  END ELSE '' END AS [Payee Bank Branch],--BRACH           
             
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER) > 0 THEN SUBSTRING(MR_LST.BANK_BRANCH_NUMBER,CHARINDEX('-' ,MR_LST.BANK_BRANCH_NUMBER)+1,1)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Branch Verifier Digit],         
        
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END       
  ELSE '' END AS [Payee Bank Account no.],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Account Verifier digit],        
        
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END      
   AS [Payee Bank Account Type],        
  '1' AS [Payee Bank Account Currency],        
 CAST(O_ITMS.IDEN_ROW_ID AS VARCHAR(25)) AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?        
  '00001' AS [Carrier Code],        
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END      
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED       
 '00030' AS [Carrier Policy Branch Code],      
  CASE WHEN MR_LST.COM_TYPE = '14694'  THEN  '01045'      
    WHEN MR_LST.COM_TYPE = '14553' THEN  '01050'      
    WHEN MR_LST.COM_TYPE ='14552' THEN '01055'       
    ELSE '' END   AS [EVENT CODE], -- nEED TO DISCUSS        
  '090' AS [Operation Type],      
        
  CASE WHEN MR_LST.PAYMENT_METHOD IN (13097,14909) THEN '001'      
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'       
   ELSE '005' END AS [Payment Method], -- aLLOW EFT        
  '' AS [Document number],        
  '' AS [Document number - serial number],        
          
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],      
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],        
         
  '1' AS [Policy Currency],        
  '' AS [Exchange rate],        
  --CASE WHEN  O_ITMS.COMM_AMOUNT < 0 THEN '-'          
  --      ELSE '' END AS [Co amount sign],        
  NULL AS [Co amount sign],      
  O_ITMS.COMM_AMOUNT AS [Co amount], -- TOTAL_DUE        
  NULL AS [Sinal do Valor Isento IR],        
  ABS(O_ITMS.COMM_AMOUNT) AS [Valor Isento IR],        
  NULL AS [Sinal do Valor Tributável IR],        
  NULL AS [Valor Tributável IR],        
  NULL AS [Sinal do Valor Isento ISS],        
  ABS(O_ITMS.COMM_AMOUNT) AS [Valor Isento ISS],        
  NULL AS [Sinal do Valor Tributável ISS],        
  NULL AS [Valor Tributável ISS],        
  NULL AS [Sinal do Valor Isento INSS],        
  ABS(O_ITMS.COMM_AMOUNT) AS [Valor Isento INSS],        
  NULL AS [Sinal do Valor Tributável INSS],        
  NULL AS [Valor Tributável INSS],        
  NULL AS [Sinal do Valor Isento CSLL/COFINS/PIS],        
  ABS(O_ITMS.COMM_AMOUNT) AS [Valor Isento CSLL/COFINS/PIS],        
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
  'Pagamento RI' AS [Payment Description], --NEED TO DISCUSS        
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END      
   AS [Policy Branch Code], -- NOT CLEAR        
  ''  AS [Profit Center code], --Profit Center code NOT CLEAR        
  NULL AS [A_DEFINIR1],        
  NULL AS [A_DEFINIR2],        
  L_LST.SUSEP_LOB_CODE AS [Policy Accounting LOB],        
  NULL AS [A_DEFINIR3], --           
  --NULL AS [Apólice],               
  --NULL AS [Apólice (cont)],          
  --NULL AS [Endosso],       
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,1,15)         
  ELSE '' END AS [Apólice],               
  CASE WHEN ISNULL(C_LST.POLICY_NUMBER,'')<> '' THEN SUBSTRING(C_LST.POLICY_NUMBER,16,6)         
  ELSE '' END AS [Apólice (cont)],          
  P_ENDOR.ENDORSEMENT_NO  AS [Endosso],           
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
  NULL AS [Exchange rate2],      
  NULL AS INCONSISTENCY_1,      
  NULL AS INCONSISTENCY_2,      
  NULL AS INCONSISTENCY_3,      
  NULL AS INCONSISTENCY_4,      
  NULL AS INCONSISTENCY_5        
 FROM POL_REINSURANCE_BREAKDOWN_DETAILS  O_ITMS        
           
INNER JOIN POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)        
   ON O_ITMS.CUSTOMER_ID = C_LST.CUSTOMER_ID        
   AND O_ITMS.POLICY_ID = C_LST.POLICY_ID         
   AND O_ITMS.POLICY_VERSION_ID = C_LST.POLICY_VERSION_ID       
           
INNER JOIN MNT_REIN_COMAPANY_LIST MR_LST  WITH(NOLOCK)      
 ON MR_LST.REIN_COMAPANY_ID = O_ITMS.MAJOR_PARTICIPANT --PR_LST.COMPANY_ID       
INNER JOIN MNT_DIV_LIST D_LST   WITH(NOLOCK)      
 ON D_LST.DIV_ID = C_LST.DIV_ID         
INNER JOIN MNT_LOB_MASTER L_LST WITH(NOLOCK)          
 ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB         
LEFT JOIN POL_POLICY_ENDORSEMENTS P_ENDOR WITH(NOLOCK)          
 ON P_ENDOR.CUSTOMER_ID = O_ITMS.CUSTOMER_ID          
    AND P_ENDOR.POLICY_ID = O_ITMS.POLICY_ID          
    AND P_ENDOR.POLICY_VERSION_ID = O_ITMS.POLICY_VERSION_ID          
WHERE --ISNULL(O_ITMS.IS_COMMISSION_PROCESS,'N')<> 'Y' AND  
O_ITMS.COMM_AMOUNT > 0      
AND MR_LST.COM_TYPE IN ('14552','14553','14694')      
      
) CO_RI WHERE [EVENT CODE] IS NOT NULL      
      
      
      
      
      
 -----------------------PAGNET_EXPORT_TABLE-----------------      
 INSERT into [PAGNET_EXPORT_RECORD]      
    (      
  INTERFACE_CODE       
  ,[SEQUENCE_OF_RECORD]   --SEQUENCE_OF_RECORD       
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
          
          
    SELECT          [Interface code]         --1      
 ,[Sequence of record]         --2      
 ,[Beneficiary Type]         --3      
 ,[FOREIGN]         --4      
 ,[Beneficiary Class]         --5      
 ,[Beneficiary name]         --6      
 ,[Beneficiary ID]         --7      
 ,[Beneficiary foreign ID]         --8      
 ,[Beneficiary Address (street name)]         --9      
 ,[Beneficiary Address (number)]         --10      
 ,[Beneficiary Address (complement)]         --11      
 ,[Beneficiary Address (district)]         --12      
 ,[Beneficiary Address (state)]         --13      
 ,[Beneficiary Address (city)]         --14      
 ,[Beneficiary Address (zip code)]         --15      
 ,[Beneficiary e-mail address]         --16      
 ,[Beneficiary Bank Number]         --17      
 ,[Beneficiary Bank Branch]         --18      
 ,[Beneficiary Bank Branch Verifier Digit]         --19      
 --,[Beneficiary Bank Account number]         --20      
 ,[Beneficiary Bank Account Verifier Digit]         --21      
 ,[Beneficiary Bank Account type]         --22      
 ,[Beneficiary Bank Account Currency]         --23      
 ,[Cód Tributação IRRF]         --24      
 ,[Natureza do Rendimento]         --25      
 , [Calcula ISS]  --[Calcula ISS ?]         --26      
 , [Calcula INSS]  --[Calcula INSS ?]         --27      
 ,[Cód Tributação INSS]         --28      
 ,[Cód Tributação CSLL]         --29      
 ,[Cód Tributação COFINS]         --30      
 ,[Cód Tributação PIS]         --31      
 ,[No de Dependentes]         --32      
 ,[No PIS]         --33      
 ,[Inscrição Municipal]         --34      
 ,[Número interno do corretor]         --35      
 ,[CBO (Classific Brasileira Ocupação)]         --36      
 ,[Código SUSEP]         --37      
 ,[No do Funcionário]         --38      
 ,[Cód da Filial]         --39      
 ,[Cód do Centro de Custo]         --40      
 ,[Data de Nascimento]         --41      
 ,[Payee Party Type]         --42      
 ,[Foreign2]         --43      
 ,[Payee Class]         --44      
 ,[Payee Name]         --45      
 ,[Payee ID (CPF or CNPJ)]         --46      
 ,[Payee foreign ID]         --47      
 ,[Payee Bank Number]         --48      
 ,[Payee Bank Branch]         --49      
 ,[Payee Bank Branch Verifier Digit]         --50      
 --,[Payee Bank Account no.]         --51      
 ,[Payee Bank Account Verifier digit]         --52      
 ,[Payee Bank Account Type]         --53      
 ,[Payee Bank Account Currency]         --54      
 ,[PAYMENT ID]         --55      
 ,[Carrier Code]         --56      
 ,[Carrier Policy Branch Code]         --57      
 ,[EVENT CODE]         --58      
 ,[Operation Type]         --59      
 ,[Payment Method]         --60      
 ,[Document number]         --61      
 ,[Document number - serial number]         --62      
 ,[Invoice issuance date]         --63      
 ,[Invoice due date]         --64      
 ,[Policy Currency] --65      
 ,[Exchange rate]         --66      
 ,[Co amount sign]         --67      
 ,[Co amount]         --68      
 ,[Sinal do Valor Isento IR]         --69      
 ,[Valor Isento IR]         --70      
 ,[Sinal do Valor Tributável IR]         --71      
 ,[Valor Tributável IR]         --72      
 ,[Sinal do Valor Isento ISS]         --73      
 ,[Valor Isento ISS]         --74      
 ,[Sinal do Valor Tributável ISS]         --75      
 ,[Valor Tributável ISS]         --76      
 ,[Sinal do Valor Isento INSS]         --77      
 ,[Valor Isento INSS]         --78      
 ,[Sinal do Valor Tributável INSS]         --79      
 ,[Valor Tributável INSS]         --80      
 ,[Sinal do Valor Isento CSLL/COFINS/PIS]         --81      
 ,[Valor Isento CSLL/COFINS/PIS]         --82      
 ,[Sinal do Valor Tributável]         --83      
 ,[Valor Tributável CSLL/COFINS/PIS]         --84      
 ,[Sinal do Valor IR]         --85      
 ,[Valor IR]         --86      
 ,[Sinal do Valor ISS]         --87      
 ,[Valor ISS]         --88      
 ,[Sinal do Valor Desconto]         --89      
 ,[Valor Desconto]         --90      
 ,[Sinal do Valor Multa]         --91      
 ,[Valor Multa]         --92      
 ,Cast([Payment Description] as varchar(60))         --93      
 ,[Policy Branch Code]         --94      
 ,[Profit Center code]         --95      
 ,[A_DEFINIR1]         --96      
 ,[A_DEFINIR2]         --97      
 ,[Policy Accounting LOB]         --98      
 ,[A_DEFINIR3]         --99      
 ,[Apólice]         --100      
 ,[Apólice (cont)]         --101      
 ,[Endosso]         --102      
 ,[Parcela]         --103      
 ,[A_DEFINIR4]         --104      
 ,[A_DEFINIR5]         --105      
 ,[Data de quitação da parcela]         --106      
 ,[Tomador/ Descrição]         --107      
 ,[Prêmio]         --108      
 ,[% Comissão]         --109      
 ,[A_DEFINIR6]         --110      
 ,[A_DEFINIR7]         --111      
 ,[A_DEFINIR8]         --112      
 ,[Tipo de Movimento]         --113      
 ,[Data do Pagamento]         --114      
 ,[Sinal do Valor Pago]         --115      
 ,[Valor Pago]         --116      
 ,[Moeda do pagamento]         --117      
 ,[Código do Banco]         --118      
 ,[No da Agência]         --119      
 ,[No da Conta Corrente]         --120      
 ,[No do Cheque]         --121      
 ,[Sinal do Valor IR_2]         --122      
 ,[Valor IR_2]         --123      
 ,[Sinal do Valor ISS_2]         --124      
 ,[Valor ISS_2]         --125      
 ,[Sinal do Valor INSS]         --126      
 ,[Valor INSS]         --127      
 ,[Sinal do Valor CSLL]         --128      
 ,[Valor CSLL]         --129      
 ,[Sinal do Valor COFINS]         --130      
 ,[Valor COFINS]         --131      
 ,[Sinal do Valor PIS]         --132      
 ,[Valor PIS]         --133      
 ,[Occurrence code]         --134      
 ,[Cheque cancellation reason]  --135      
 ,[Payment method_2]         --136      
 ,[Carrier Bank Number]         --137      
 ,[Carrier Bank Branch number]         --138      
 ,[Carrier Bank Account number]         --139      
  ,[Exchange rate2]         --140      
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
      
      
      
      
      
      
      
      
      
      
      
      
      
      
 -- ) RI WHERE [EVENT CODE] IS NOT NULL        
      
      
      
/*SELECT ROW_NUMBER()  OVER (ORDER BY [Sequence of record_2]) as [Sequence of record],  * FROM        
(SELECT '05' AS [Interface code],      
C_RSRV.CLAIM_ID AS CLAIM_ID,      
C_RSRV.ACTIVITY_ID AS ACTIVITY_ID,      
C_RSRV.COMP_ID AS COMP_ID,      
ROW_NUMBER() OVER (ORDER BY C_LST.customer_id,C_LST.POLICY_ID,C_LST.POLICY_VERSION_ID,C_RSRV.CLAIM_ID,C_RSRV.ACTIVITY_ID,C_RSRV.COMP_ID ) AS [Sequence of record_2],        
 'J' AS [Beneficiary Type],        
 '0' AS [FOREIGN],        
 '064' AS [Beneficiary Class],         MR_LST.REIN_COMAPANY_NAME   AS [Beneficiary name],        
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
  END  ELSE '' END AS [Beneficiary Bank Branch Verifier Digit],         
        
   CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN        
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,1,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)-1)           
  ELSE MR_LST.REIN_COMAPANY_ACC_NUMBER          
  END ELSE '' END AS [Beneficiary Bank Account number],          
            
   CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Beneficiary Bank Account Verifier Digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END AS [Beneficiary Bank Account type],       
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
  NULL AS [Código SUSEP],       
  NULL AS [No do Funcionário],        
  NULL AS [Cód da Filial],        
  NULL AS [Cód do Centro de Custo],        
  NULL AS [Data de Nascimento],        
  'J' AS   [Payee Party Type],         
  '0' AS [Foreign2], -- REPEAT        
  '064' AS [Payee Class],        
  MR_LST.REIN_COMAPANY_NAME AS [Payee Name],        
  MR_LST.CARRIER_CNPJ AS [Payee ID (CPF or CNPJ)],        
  '' AS [Payee foreign ID],       
          
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
  END       
  ELSE '' END AS [Payee Bank Account no.],          
            
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN      
  CASE WHEN CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER) > 0 THEN SUBSTRING(MR_LST.REIN_COMAPANY_ACC_NUMBER,CHARINDEX('-' ,MR_LST.REIN_COMAPANY_ACC_NUMBER)+1,2)           
  ELSE ''          
  END ELSE '' END AS [Payee Bank Account Verifier digit],      
  CASE WHEN MR_LST.BANK_ACCOUNT_TYPE = 14703 THEN '01'      
    WHEN MR_LST.BANK_ACCOUNT_TYPE = 14704 THEN '02'      
    ELSE '' END  AS [Payee Bank Account Type],         
  '1' AS [Payee Bank Account Currency],        
  C_RSRV.[PAYMENT ID] AS [PAYMENT ID], --NEED TO ASK FOR WHICH VALUE?        
  '00001' AS [Carrier Code],      
  --CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
  -- AS [Carrier Policy Branch Code], -- NOT CLEAR TO BE DISCUSSED        
  C_RSRV.ADJUSTER_CODE AS [Carrier Policy Branch Code],       
  C_RSRV.[EVENT CODE], -- nEED TO DISCUSS       
            
  CASE WHEN C_RSRV.COMP_TYPE = 'CO'  THEN '070'--CO       
    WHEN C_RSRV.COMP_TYPE ='RI' THEN '090' --RI      
   END AS [Operation Type],      
  CASE WHEN MR_LST.PAYMENT_METHOD = 13097 THEN '001' -- aLLOW EFT        
    WHEN MR_LST.PAYMENT_METHOD IN (13096,13098) THEN '005'      
   ELSE '' END AS [Payment Method], -- aLLOW EFT        
  '' AS [Document number],        
  '' AS [Document number - serial number],        
          
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice issuance date],      
 REPLACE(CONVERT(VARCHAR,GETDATE(), 111),'/','') AS [Invoice due date],        
         
  '1' AS [Policy Currency],        
  NULL AS [Exchange rate],        
  --CASE WHEN  C_RSRV.PAYMENT_AMOUNT < 0 THEN '-'          
  --      ELSE '' END AS [Co amount sign],      
  NULL AS [Co amount sign],        
  ABS(C_RSRV.PAYMENT_AMOUNT) AS [Co amount], -- TOTAL_DUE        
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
  CASE WHEN   C_RSRV.[EVENT CODE] ='01005' THEN 'Indemnity for normal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01011' THEN 'Indemnity for legal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01012' THEN 'Expense for normal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01014' THEN 'Expense for legal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01017' THEN 'Professional service expense for normal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01019' THEN 'Professional service expense for legal claim for accepted CO'      
    WHEN   C_RSRV.[EVENT CODE] ='01080' THEN 'Subrogation for normal claim for ceded coinsurance'      
    WHEN   C_RSRV.[EVENT CODE] ='01085' THEN 'Subrogation for legal claim for ceded coinsurance'      
    WHEN   C_RSRV.[EVENT CODE] ='01090' THEN 'Salvage transfer for normal claim for ceded coinsurance'      
    WHEN   C_RSRV.[EVENT CODE] ='01095' THEN 'Salvage transfer for legal claim for ceded coinsurance'      
  END       
   AS [Payment Description], --NEED TO DISCUSS       
  CASE WHEN LEN(D_LST.BRANCH_CODE) =1 THEN '0'+D_LST.BRANCH_CODE ELSE D_LST.BRANCH_CODE END        
   AS [Policy Branch Code], -- NOT CLEAR        
  NULL AS [Profit Center code], --Profit Center code NOT CLEAR        
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
  FROM (SELECT TMP1.CLAIM_ID,TMP1.ACTIVITY_ID,      
  TMP1.COMP_ID,TMP1.COMP_TYPE,TMP1.[EVENT CODE],      
  CAST(TMP1.CLAIM_ID as varchar) + 'sep' + CAST(TMP1.ACTIVITY_ID as varchar)      
  + 'sep' + CAST(TMP1.COMP_ID as varchar)   AS [PAYMENT ID],      
  SUM(PAYMENT_AMOUNT) as PAYMENT_AMOUNT,      
  TMP1.CUSTOMER_ID,           
  TMP1.POLICY_ID,      
  TMP1.POLICY_VERSION_ID,       
  TMP1.ADJUSTER_CODE      
   FROM      
    (SELECT       
    TMP.CLAIM_ID,TMP.ACTIVITY_ID,      
    TMP.COMP_ID,TMP.COMP_TYPE,      
    C_ACT.IS_LEGAL,      
    CO_INSURANCE_TYPE,      
    TMP.COVERAGE_ID,      
    CASE WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549       
        AND  TMP.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
        THEN '01005'      
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549       
      AND  TMP.COVERAGE_ID NOT IN (50017,50018,50019,50020,50021,50022)      
       THEN '01011'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14549         
       AND  TMP.COVERAGE_ID =50017       
       THEN '01012'      
             
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549         
       AND TMP.COVERAGE_ID =50017   
       THEN '01014'       
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE =14549         
       AND TMP.COVERAGE_ID =50018       
       THEN '01017'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14549          
       AND TMP.COVERAGE_ID =50018       
       THEN '01019'      
             
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE= 14548         
       AND TMP.COVERAGE_ID =50021       
       THEN '01080'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE=14548          
       AND TMP.COVERAGE_ID =50021       
       THEN '01085'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='N' AND  CO_INSURANCE_TYPE = 14548         
       AND TMP.COVERAGE_ID =50019       
       THEN '01090'      
            
      WHEN ISNULL(C_ACT.IS_LEGAL,'N') ='Y' AND  CO_INSURANCE_TYPE = 14548          
       AND TMP.COVERAGE_ID =50019       
       THEN '01095' END             
      AS [EVENT CODE],      
      C_INFO.CUSTOMER_ID,           
      C_INFO.POLICY_ID,      
      C_INFO.POLICY_VERSION_ID,      
      MUL.ADJUSTER_CODE,                       
      TMP.PAYMENT_AMOUNT       
      FROM      
       (SELECT CAR.CLAIM_ID,CAR.ACTIVITY_ID,CACRB.COMP_ID,COMP_TYPE,      
         CPC.COVERAGE_CODE_ID AS  COVERAGE_ID ,       
         (CACRB.PAYMENT_AMT) AS PAYMENT_AMOUNT      
        FROM        
          CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)      
           INNER JOIN CLM_PRODUCT_COVERAGES CPC       
              ON  CPC.CLAIM_ID = CAR.CLAIM_ID      
              AND CPC.CLAIM_COV_ID = CAR.COVERAGE_ID      
          INNER JOIN CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB WITH(NOLOCK)       
             ON CAR.CLAIM_ID= CACRB.CLAIM_ID       
             AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID       
             AND CAR.RESERVE_ID = CACRB.RESERVE_ID      
        WHERE  ISNULL(CACRB.IS_COMMISSION_PROCESS,'N')<>'Y'       
           AND CACRB.PAYMENT_AMT > 0                                             
      ) TMP      
     INNER JOIN CLM_ACTIVITY C_ACT WITH(NOLOCK)      
       ON C_ACT.CLAIM_ID =  TMP.CLAIM_ID      
       AND C_ACT.ACTIVITY_ID =  TMP.ACTIVITY_ID             
     INNER JOIN CLM_CLAIM_INFO C_INFO WITH(NOLOCK)         
       ON C_INFO.CLAIM_ID = TMP.CLAIM_ID      
     INNER JOIN CLM_ADJUSTER CA WITH(NOLOCK)      
      ON  CA.ADJUSTER_ID = C_INFO.ADJUSTER_ID      
     INNER JOIN MNT_USER_LIST MUL WITH(NOLOCK)      
      ON MUL.USER_ID = CA.USER_ID        
              
     ) TMP1      
    GROUP BY      
    TMP1.CLAIM_ID,TMP1.ACTIVITY_ID,      
    TMP1.COMP_ID,TMP1.COMP_TYPE,TMP1.ADJUSTER_CODE,TMP1.[EVENT CODE],TMP1.CUSTOMER_ID,           
     TMP1.POLICY_ID,      
     TMP1.POLICY_VERSION_ID )  AS C_RSRV      
INNER JOIN CLM_PARTIES C_PT WITH(NOLOCK)      
  ON C_RSRV.CLAIM_ID = C_PT.CLAIM_ID      
INNER JOIN MNT_REIN_COMAPANY_LIST MR_LST WITH(NOLOCK)      
  ON MR_LST.REIN_COMAPANY_ID = C_PT.SOURCE_PARTY_ID    --JOIN ON COMPANY ID            
INNER JOIN CLM_PAYEE C_P  WITH(NOLOCK)        
  ON C_RSRV.CLAIM_ID = C_P.CLAIM_ID         
  AND  C_P.ACTIVITY_ID = C_RSRV.ACTIVITY_ID       
  AND C_P.PARTY_ID = C_PT.PARTY_ID      
INNER JOIN  POL_CUSTOMER_POLICY_LIST C_LST WITH(NOLOCK)         
  ON C_LST.CUSTOMER_ID = C_RSRV.CUSTOMER_ID          
  AND C_LST.POLICY_ID = C_RSRV.POLICY_ID          
  AND C_LST.POLICY_VERSION_ID = C_RSRV.POLICY_VERSION_ID       
  AND C_LST.CO_INSURANCE IN (14549,14548)  --IS NOT NULL      
INNER JOIN  mnt_div_list D_LST   WITH(NOLOCK)       
  ON D_LST.DIV_ID = C_LST.DIV_ID       
INNER JOIN MNT_LOB_MASTER L_LST  WITH(NOLOCK)             
  ON CAST(L_LST.LOB_ID AS VARCHAR) = C_LST.POLICY_LOB      
        
  ) AS RI_CLAIM      
 WHERE [EVENT CODE] IS NOT NULL */      
        
          
END

GO

