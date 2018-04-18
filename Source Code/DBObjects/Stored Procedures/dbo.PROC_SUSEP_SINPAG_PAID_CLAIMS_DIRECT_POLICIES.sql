IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_SINPAG_PAID_CLAIMS_DIRECT_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_SINPAG_PAID_CLAIMS_DIRECT_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
  
-- =============================================          
-- Author:  <SHIKHA CHOURASIYA>          
-- Create date: <08/10/2011>    
-- Modify date : <08/19/2011>          
-- Description: <SINPAG Paid Claims of Direct  ITrack - 1079 - TFS Bug # 204>          
-- DROP PROC [PROC_SUSEP_SINPAG_PAID_CLAIMS_DIRECT_POLICIES]          
-- EXEC [PROC_SUSEP_SINPAG_PAID_CLAIMS_DIRECT_POLICIES] '06/10/2011','SK000250'        
         
-- =============================================         
          
CREATE PROCEDURE  [dbo].[PROC_SUSEP_SINPAG_PAID_CLAIMS_DIRECT_POLICIES]             
(          
 @DATETIME DATETIME,            
 @CLAIM_NUMBER NVARCHAR(21)=NULL          
)                  
AS           
          
 DECLARE @MONTH VARCHAR(2)            
 DECLARE @YEAR VARCHAR(4)           
 DECLARE @TEMP NVARCHAR(500)          
 SET @MONTH= DATEPART(MM, @DATETIME)           
 SET @YEAR=(SELECT DATEPART(YYYY, @DATETIME))            
 BEGIN          
 DECLARE @TEMPTABLE TABLE           
 (          
   SEQUENCE_NO INT IDENTITY(1,1),          
   COMPANY_CODE VARCHAR(50),          
   REFERENCE_MONTH VARCHAR(8),          
   TYPE_OF_TRANSACTION NVARCHAR(10),          
   SUSEP_LOB_CODE VARCHAR(5),          
   OFFCIAL_CLAIM_NUMBER NVARCHAR(100),          
   POLICY_NUMBER NVARCHAR(150),          
   ENDORSEMENT_NO VARCHAR(20),          
   INSURED_CPF NVARCHAR(40),          
   NUMBER_OF_INSURED VARCHAR(20),          
   BENEFICIARY_CPF NVARCHAR(40),          
   NUMBER_OF_BENEFICIARY VARCHAR(20),          
   CLAIM_REGISTRATION_DATE NVARCHAR(10),          
   FIRST_NOTICE_OF_LOSS NVARCHAR(10),          
   LOSS_DATE NVARCHAR(10),          
   COINSURENCE_AMT NVARCHAR(100),        
   AMOUNT_PAYMENT NVARCHAR(100),         
   TRANSACTION_DATE VARCHAR(20),        
   CLAIM_TYPE VARCHAR(2),         
   MODE_OF_PAYMENT NVARCHAR(10),          
   BANK_NUMBER_LEADER NVARCHAR(40),          
   BANK_BRANCH_LEADER NVARCHAR(40),          
   ACCOUNT_NUMBER_LEADER NVARCHAR(40),          
   NUM_TRANSACAO VARCHAR(20),         
   BANK_NUMBER NVARCHAR(40),          
   BANK_BRANCH NVARCHAR(40),            
   ACCOUNT_NUMBER NVARCHAR(40)           
 )          
INSERT INTO @TEMPTABLE            
            
   SELECT  DISTINCT         
   -------ALIANÇA DA BAHIA SUSEP CODE          
   '504-5' AS COMPANY_CODE, --Modified as per the itrack-1079 notes 16 - point 35          
             
   -------REFERENCE MONTH OF THE REPORT.          
   CASE           
   WHEN @MONTH IN (1,2,3,4,5,6,7,8,9) THEN  @YEAR + '0' + @MONTH           
   WHEN @MONTH IN (10,11,12) THEN  @YEAR + @MONTH          
   END AS REFERENCE_MONTH,          
             
   --------TYPE OF TRANSACTION  207 - PARTIAL SETTLEMENT AND 208 - TOTAL SETTLEMENT          
   CASE WHEN CA.ACTION_ON_PAYMENT = '181' THEN '208'          
        WHEN CA.ACTION_ON_PAYMENT = '180' THEN '207'          
   END AS TYPE_OF_TRANSACTION,          
             
   -----------SUSEP LOB CODE         
   ( SELECT TOP 1 MLM.SUSEP_LOB_CODE FROM CLM_ACTIVITY_RESERVE TCAR WITH(NOLOCK) LEFT OUTER JOIN         
                   CLM_PRODUCT_COVERAGES TCPC WITH(NOLOCK) ON  TCAR.CLAIM_ID=TCPC.CLAIM_ID AND TCAR.COVERAGE_ID=TCPC.CLAIM_COV_ID INNER JOIN         
                   MNT_COVERAGE MC WITH(NOLOCK) ON TCPC.COVERAGE_CODE_ID = MC.COV_ID INNER JOIN        
                   MNT_LOB_MASTER MLM WITH(NOLOCK) ON MC.LOB_ID = MLM.LOB_ID         
             WHERE TCAR.CLAIM_ID=CA.CLAIM_ID AND TCAR.ACTIVITY_ID=CA.ACTIVITY_ID --AND PAYMENT_AMOUNT<>0     
             AND CA.ACTIVITY_STATUS = '11801'        
                   )        
            
   --SELECT TOP 1 MLM.SUSEP_LOB_CODE FROM CLM_PRODUCT_COVERAGES CPV         
   --LEFT JOIN MNT_COVERAGE MC ON CPV.COVERAGE_CODE_ID = MC.COV_ID  AND IS_RISK_COVERAGE = 'Y' AND CPV.CLAIM_ID = VW.CLAIM_ID         
   --INNER JOIN MNT_LOB_MASTER MLM ON MC.LOB_ID = MLM.LOB_ID)         
   ,        
             
   -----------CLAIM NUMBER - OFFICIAL NUMBER          
   VW.OFFCIAL_CLAIM_NUMBER,           
             
   -------SEQUENCE NUMBER OF POLICY.          
   VW.FULL_POLICY_NUMBER,   
             
   ----------NUMBER OF ENDORSEMENT.     
   --CASE WHEN ISNULL(VW.ENDORSEMENT_NO,'') = '' THEN '00000000000000000000'    
   --   ELSE VW.ENDORSEMENT_NO END ENDORSEMENT_NO,       
   --ISNULL(VW.ENDORSEMENT_NO,0),           
   VW.ENDORSEMENT_NO,          
   -----------INSURED PARTY'S CPF/CNPJ.          
   --CPP.PARTY_CPF_CNPJ,       
   CASE WHEN ISNULL(VWS.RISK_CO_APP_CPF_CNPJ,'') = '' THEN '00000000000'        
 ELSE VWS.RISK_CO_APP_CPF_CNPJ END AS RISK_CO_APP_CPF_CNPJ     
   ,        
             
   -----------NUMBER OF INSURED PARTIES.          
   (SELECT           
    COUNT(CP.PARTY_TYPE_ID)           
    FROM           
    CLM_PARTIES CP WITH(NOLOCK)          
    WHERE           
    CP.PARTY_TYPE_ID IN (10,114) AND           
    CP.CLAIM_ID  = VW.CLAIM_ID            
   ) AS NUMBER_OF_INSURED,          
             
  -------BENEFICIARY'S /CLAIMANT'S CPF/CNPJ      
  --Modified by Pradeep - as per the itrack- 1079 - note 15 point(25)
  CASE WHEN ISNULL(CPP1.PARTY_CPF_CNPJ,'') = '' THEN ''        
  ELSE CPP1.PARTY_CPF_CNPJ END AS PARTY_CPF_CNPJ,            
            
  ---------NUMBER OF BENEFICIARIES.         
  (SELECT COUNT(TP.PARTY_ID)          
 FROM CLM_PAYEE TP  WITH(NOLOCK)          
 WHERE TP.CLAIM_ID = CA.CLAIM_ID AND           
      TP.ACTIVITY_ID= CA.ACTIVITY_ID          
 ) AS NUMBER_OF_BENEFICIARY,          
            
            
  -----------THE DATE WHEN THE CLAIM CASE WAS REGISTERED IN SYSTEM.          
  VW.CLAIM_REGISTRATION_DATE_1,          
            
  -----------THE DATE WHEN CLAIMANT REPORT THE CLAIM. USE THE IRT NOTICE OF LOSS.          
  VW.FIRST_NOTICE_OF_LOSS,           
            
  ------------ACCIDENT OCCURRENCE DATE.DATE OF LOSS.          
  VW.LOSS_DATE,           
            
  -----------CEDED COINSURANCE AMOUNT - REGARDING TO THE SHARE OF COINSURANCE COMPANY OF TOTAL AMOUNT.          
  (SELECT SUM(PAYMENT_AMT)               
   FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CA1 WITH(NOLOCK)          
   WHERE CA1.CLAIM_ID = VW.CLAIM_ID AND CA1.ACTIVITY_ID = CA.ACTIVITY_ID           
  AND CA.ACTIVITY_STATUS = 11801 AND CA1.IS_ACTIVE='Y' AND COMP_TYPE = 'CO'      
   ) AS COINSURENCE_AMT,          
          
    ----------AMOUNT OF PAYMENTS MADE TO CLAIMANT.          
  (ISNULL(CA.PAYMENT_AMOUNT,0)+ISNULL(CA.EXPENSES,0)) AS AMOUNT_PAYMENT,          
          
          
    ---------TRANSACTION DATE.          
   CONVERT(VARCHAR(10),CA.ACTIVITY_DATE,112) AS TRANSACTION_DATE,         
           
       ---------------------------------------------------          
  -- CLAIM TYPE                
  -- 01: ADMINISTRATIVE INDEMINITY          
  -- 02: ADMINISTRATIVE EXPENSES          
  -- 03: LEGAL INDEMINITY          
  -- 04: LEGAL EXPENSES          
          
  -- CHECK IF RESERVE/PAYMENT/RECOVERY DONE AGAINST POLICY COVERAGES THEN IDENMINTY ESLE EXPENSES          
  ---------------------------------------------------          
   CASE WHEN (SELECT TOP 1 IS_RISK_COVERAGE           
     FROM  CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK) INNER JOIN           
     CLM_PRODUCT_COVERAGES CPC WITH(NOLOCK) ON CAR.COVERAGE_ID = CPC.CLAIM_COV_ID          
     WHERE CAR.CLAIM_ID =CA.CLAIM_ID AND CAR.ACTIVITY_ID=CA.ACTIVITY_ID          
     AND (CAR.PAYMENT_AMOUNT          
       )<>0          
    )='Y'           
             
  THEN           
     (CASE WHEN CA.IS_LEGAL='Y' THEN '03' --- LEGAL INDEMINITY          
       ELSE '01' END                    --- ADMINISTRATIVE INDEMINITY          
     )          
  ELSE          
     (CASE WHEN CA.IS_LEGAL='Y' THEN '04' --- LEGAL EXPENSES          
       ELSE '02' END                    --- ADMINISTRATIVE EXPENSES          
     )          
  END AS CLAIM_TYPE ,        
            
  -----------PAYMENT MODE : FOR CHEQUE : 14711 ; FOR BANK TRANSFER : 14707          
  CASE MLV1.LOOKUP_UNIQUE_ID           
    WHEN 14711 THEN '2'          
    WHEN 14707 THEN '3'          
  END AS MODE_OF_PAYMENT,          
             
  ------------BANK NUMBER OF ALIANÇA DA BAHIA.       
  PRF.CARRIER_BANK_NUMBER  AS BANK_NUMBER_LEADER,         
  --CPP2.BANK_NUMBER AS BANK_NUMBER_LEADER,            
            
  ----------BRANCH NUMBER OF BANK OF ALIANÇA DA BAHIA.      
  PRF.CARRIER_BANK_BRANCH_NUMBER AS BANK_BRANCH_LEADER,         
  --CPP2.BANK_BRANCH AS BANK_BRANCH_LEADER,            
            
  -----------ACCOUNT NUMBER OF ALIANÇA DA BAHIA.      
  PRF.CARRIER_BANK_ACCOUNT_NUMBER AS ACCOUNT_NUMBER_LEADER,          
  --CPP2.ACCOUNT_NUMBER AS ACCOUNT_NUMBER_LEADER,          
          
               
   ------------PAYMENT TRANSACTION NUMBER.       
   PRF.CHEQUE_NUMBER,        
   --'0' AS PAYMENT_TRAN_NUMBER,            
            
  ----------BANK NUMBER OF CLAIMANT(IN CASE OF BANK TRANSFER).          
  --CASE WHEN MLV1.LOOKUP_UNIQUE_ID = '14711' THEN '001'         
  --  ELSE CPP3.BANK_NUMBER END AS BANK_NUMBER,        
   CPP3.BANK_NUMBER,         
  ----------BRANCH NUMBER OF BANK OF CLAIMANT(IN CASE OF BANK TRANSFER).           
  --CASE WHEN MLV1.LOOKUP_UNIQUE_ID = '14711' THEN '34290'         
  --  ELSE CPP3.BANK_BRANCH END AS BANK_BRANCH,           
   CPP3.BANK_BRANCH,         
  -----------ACCOUNT NUMBER OF CLAIMANT(IN CASE OF BANK TRANSFER).          
  --CASE WHEN MLV1.LOOKUP_UNIQUE_ID = '14711' THEN '1500791'         
  --  ELSE ISNULL(CPP3.ACCOUNT_NUMBER,'') END AS ACCOUNT_NUMBER       
    CPP3.ACCOUNT_NUMBER        
 FROM           
  [VW_SUSEP_FORM_SINPENDAC_SINPAGAC] AS VW          
 --  CLM_CLAIM_INFO CCI WITH(NOLOCK)          
 INNER JOIN          
  CLM_ACTIVITY CA WITH(NOLOCK) ON (CA.CLAIM_ID = VW.CLAIM_ID            
    AND CA.ACTION_ON_PAYMENT IN (180,181) AND CA.ACTIVITY_STATUS = '11801') --For payment partial = 180, payment full = 181          
 INNER JOIN          
  CLM_PAYEE PAYEE WITH(NOLOCK) ON (CA.CLAIM_ID = PAYEE.CLAIM_ID          
    AND PAYEE.ACTIVITY_ID=CA.ACTIVITY_ID)          
  LEFT OUTER JOIN           
  MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON (MLV.LOOKUP_UNIQUE_ID = CA.COI_TRAN_TYPE)       
  LEFT OUTER JOIN CLM_INSURED_PRODUCT  CIP WITH(NOLOCK) ON CIP.CLAIM_ID = VW.CLAIM_ID       
  LEFT OUTER JOIN  [VW_POL_PRODUCT_RISK_DETAILS] VWS  WITH(NOLOCK) ON VW.CUSTOMER_ID = VWS.CUSTOMER_ID AND VW.POLICY_ID = VWS.POLICY_ID AND VW.POLICY_VERSION_ID = VWS.POLICY_VERSION_ID AND CIP.POL_RISK_ID = VWS.RISK_ID          
  -- LEFT OUTER JOIN           
  --CLM_PARTIES CPP WITH(NOLOCK) ON (CPP.CLAIM_ID = CA.CLAIM_ID AND          
  --         CPP.PARTY_ID = PAYEE.PARTY_ID AND           
  --         CPP.PARTY_TYPE_ID = 10) --PARTY_TYPE_ID = 10 : For insured          
   INNER JOIN           
  CLM_PARTIES CPP1 WITH(NOLOCK) ON (CPP1.PARTY_ID = PAYEE.PARTY_ID AND           
            CPP1.CLAIM_ID = PAYEE.CLAIM_ID)          
   LEFT OUTER JOIN           
  MNT_LOOKUP_VALUES MLV1 WITH(NOLOCK) ON (MLV1.LOOKUP_UNIQUE_ID = PAYEE.PAYMENT_METHOD)                    
   LEFT OUTER JOIN           
  CLM_PARTIES CPP3 WITH(NOLOCK) ON (CPP3.PARTY_ID = PAYEE.PAYEE_PARTY_ID AND --CPP3.PAYMENT_METHOD = MLV1.LOOKUP_UNIQUE_ID AND          
            CPP3.CLAIM_ID = CA.CLAIM_ID AND           
            MLV1.LOOKUP_UNIQUE_ID = 14707) --14707 : For bank Transfer       
 -- LEFT OUTER JOIN CLM_ACTIVITY_RESERVE CAR WITH(NOLOCK) ON CA.CLAIM_ID = CAR.CLAIM_ID AND CA.ACTIVITY_ID = CAR.ACTIVITY_ID      
  LEFT OUTER JOIN PAGNET_RETURN_FILE PRF WITH(NOLOCK) ON CA.CLAIM_ID = dbo.Piece(REPLACE(PAYMENT_ID,'sep','~'),'~',1) AND CA.ACTIVITY_ID = dbo.Piece(REPLACE(PAYMENT_ID,'sep','~'),'~',2)        
 AND EVENT_CODE IN ('01013','01015','01018','01021','01000','01010','01005','01011','01012','01014','01017','01019','01080','01085','01090','01095') AND OCCURRENCE_CODE <>'999' AND PAYMENT_ID LIKE '%sep%'                   
            
 WHERE          
   CONVERT(varchar(6),VW.CREATED_DATETIME,112) = CONVERT(varchar(6),@DATETIME,112) AND          
   (@CLAIM_NUMBER IS NULL OR VW.CLAIM_NUMBER = @CLAIM_NUMBER)          
   AND VW.CO_INSURANCE IN (14547,14548) AND VW.OFFCIAL_CLAIM_NUMBER IS NOT NULL        
            
   SELECT  
   --Modified by Pradeep for itrack-1079 note(15)- point-33
    --RIGHT('0000000000' +CONVERT(VARCHAR,SEQUENCE_NO),10) AS SEQUENCIA,          
	    SEQUENCE_NO AS SEQUENCIA,
    --Modified till here
    COMPANY_CODE AS COD_CIA,          
	REFERENCE_MONTH AS DT_BASE,          
    TYPE_OF_TRANSACTION AS TIPO_MOV,          
    SUSEP_LOB_CODE AS COD_RAMO,          
    --RIGHT('00000000000000000000'+REPLACE(OFFCIAL_CLAIM_NUMBER,'-',''),20) AS NUM_SIN, 
    REPLACE(OFFCIAL_CLAIM_NUMBER,'-','') AS NUM_SIN,        
    RIGHT('                    '+ISNULL(POLICY_NUMBER,''),20) NUM_APOL,--Modified by Pradeep for itrack-1079 note(15)- point-22
   -- RIGHT('00000000000000000000'+ISNULL(ENDORSEMENT_NO,''),20) AS CUST_NUM_END,  
    ISNULL(ENDORSEMENT_NO,'') AS NUM_END,       
    --RIGHT('00000000000000'+REPLACE(REPLACE(REPLACE(INSURED_CPF,'.',''),'-',''),'/',''),14) AS CPF_SEG,  
    REPLACE(REPLACE(REPLACE(INSURED_CPF,'.',''),'-',''),'/','') CPF_SEG,      
	--Modified by Pradeep for itrack-1079 note(15)- point-32
    --RIGHT('0000'+NUMBER_OF_INSURED,4) AS QTD_SEG,          
	NUMBER_OF_INSURED AS QTD_SEG,
	--Modified till here 

    --RIGHT('000000000000000'+REPLACE(REPLACE(REPLACE(BENEFICIARY_CPF,'.',''),'-',''),'/',''),15) AS RE_CPF_BEN,  
    REPLACE(REPLACE(REPLACE(ISNULL(BENEFICIARY_CPF,''),'.',''),'-',''),'/','')  AS CPF_BEN,        
    --RIGHT('0000'+NUMBER_OF_BENEFICIARY,4) AS QTD_BEN, 
    NUMBER_OF_BENEFICIARY  AS QTD_BEN,        
    CLAIM_REGISTRATION_DATE AS DT_REG,          
    FIRST_NOTICE_OF_LOSS AS DT_AVISO,          
    LOSS_DATE AS DT_OCOR,          
    --REPLACE(RIGHT('0000000000000000'+ISNULL(CONVERT(VARCHAR(16),COINSURENCE_AMT),''),16),'.','') AS RVR_COS_CED,
    ISNULL(CONVERT(VARCHAR(16),isnull(COINSURENCE_AMT,'0.00')),0) AS VR_COS_CED, ----Modified by Pradeep for itrack-1079 note(15)- point-27       
    --REPLACE(RIGHT('0000000000000000'+CONVERT(VARCHAR(16),AMOUNT_PAYMENT),16),'.','') AS REVR_MOV,  
    ISNULL(CONVERT(VARCHAR(16),ISNULL(AMOUNT_PAYMENT,'0.00')),0) AS VR_MOV,----Modified by Pradeep for itrack-1079 note(15)- point-28             
    TRANSACTION_DATE AS DT_MOV,        
    CLAIM_TYPE AS TP_SIN,         
    MODE_OF_PAYMENT AS TIPO_REC,          
    --ISNULL(BANK_NUMBER_LEADER,'') AS NUM_BAN_SEG,          
    --ISNULL(BANK_BRANCH_LEADER,'') AS NUM_AGE_SEG,          
    --ISNULL(ACCOUNT_NUMBER_LEADER,'') AS NUM_CON_SEG,          
    --ISNULL(NUM_TRANSACAO,'') AS NUM_TRANSACAO,         
    --ISNULL(BANK_NUMBER,'') AS NUM_BAN,          
    --ISNULL(BANK_BRANCH,'') AS NUM_AGE,            
    --ISNULL(RIGHT('00000000000000000000'+ACCOUNT_NUMBER,20),'') AS NUM_CON    
    --Modified by Pradeep- to change the column level- as per the itrack-1079 note-15 -point (34)
    RIGHT(ISNULL(BANK_NUMBER_LEADER,0),4) AS NUM_BAN_SEG,          
    RIGHT('     '+ISNULL(BANK_BRANCH_LEADER,''),5) AS NUM_AGE_SEG,          
    RIGHT('                    '+ISNULL(ACCOUNT_NUMBER_LEADER,''),20) AS NUM_CON_SEG,          
    RIGHT('                    '+ISNULL(NUM_TRANSACAO,''),20) AS NUM_TRANSACAO,         
    RIGHT(ISNULL(BANK_NUMBER,0),4) AS NUM_BAN,          
    RIGHT('     '+ISNULL(BANK_BRANCH,''),5) AS NUM_AGE,            
    RIGHT('                    '+ISNULL(ACCOUNT_NUMBER,''),20) AS NUM_CON 
	--Modified till here 
         
   FROM @TEMPTABLE           
END    
GO



