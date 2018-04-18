IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_SINAV_CLAIM_MOVEMENT_OF_DIRECT_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_SINAV_CLAIM_MOVEMENT_OF_DIRECT_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                              
Proc Name             : Dbo.[PROC_SUSEP_SINAV_CLAIM_MOVEMENT_OF_DIRECT_POLICIES]        --FIXED                                                      
Created by            : SANTOSH KUMAR GAUTAM                                                    
Date                  : 12 JULY 2011                                                            
Purpose               : TO CREATE SUSEP REPORT FOR CLAIM   
Revison History       :                                                              
Used In               : CLAIM MODULE 
Itrack				  : 1074 TFS Bug # 43          
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc [PROC_SUSEP_SINAV_CLAIM_MOVEMENT_OF_DIRECT_POLICIES] '2011-08-12 18:42:38.793'                                       
------   ------------       -------------------------*/                                                              
                            
CREATE PROCEDURE [dbo].[PROC_SUSEP_SINAV_CLAIM_MOVEMENT_OF_DIRECT_POLICIES]      
@DATETIME DATETIME                                 
AS                                  
BEGIN                           
       
 DECLARE @MONTH VARCHAR(2)      
 DECLARE @YEAR VARCHAR(4)    
    
 SET @MONTH= MONTH(@DATETIME)    
 SET @YEAR=YEAR(@DATETIME)  
   
 IF(LEN(@MONTH)<2)  
  SET @MONTH='0'+@MONTH  

DECLARE @TEMPTABLE TABLE           
 (                  
  SEQUENCIA INT                  
  ,COD_CIA VARCHAR(5)                  
  ,DT_BASE VARCHAR(6)                  
  ,TIPO_MOV INT                  
  ,COD_RAMO NVARCHAR(4)               
  ,NUM_SIN NVARCHAR(20)               
  ,NUM_APOL NVARCHAR(20)                   
  ,NUM_END NVARCHAR(20)              
  ,CPF_SEG NVARCHAR(14)                    
  ,QTD_SEG INT                
  ,CPF_BEN NVARCHAR(15)                  
  ,QTD_BEN INT 
  ,DT_REG  NVARCHAR(10)                  
  ,DT_AVISO NVARCHAR(10)                    
  ,DT_OCOR NVARCHAR(10)                 
  ,VR_COS_CED DECIMAL(16,2)               
  ,VR_MOV NVARCHAR(16)               
  ,DT_MOV NVARCHAR(8)                   
  ,TP_SIN NVARCHAR(3)              
                        
 )                  
 INSERT INTO @TEMPTABLE       
SELECT   
  
ROW_NUMBER() OVER(ORDER BY ACT.ACTIVITY_ID ASC)   
                                      AS SEQUENCIA,-- CHANGED BY KULDEEP FROM SEQUENCE TO SEQUENCIA AS PER ITRACK 1074 NOTE OF 24-AUG-11--SEQUANCE_NUMBER,  
--Modified by Pradeep As per the itrack-1074-note-13 by Monica
'504-5'          AS COD_CIA,--COMPANY_CODE, --ALIANCA DA BAHIA SUSEP CODE  
(@YEAR+@MONTH)         AS DT_BASE,--BASE_DATE,  
---------------------------------------------------  
--- TYPE OF TRANSACTION  
---------------------------------------------------  
 --Code commented by pradeep on 02-09-2011
--CASE WHEN ACT.ACTION_ON_PAYMENT = 165 THEN 201 -- FOR CREATE RESERVE ACTIVITY =>( NEW CLAIM REGISTERED)  
--  WHEN ACT.ACTION_ON_PAYMENT = 168 THEN 214 -- FOR REOPEN RESERVE ACTIVITY =>( CLAIM REOPEN  )  
--  WHEN ACT.ACTION_ON_PAYMENT = 167 AND CLM.CLAIM_STATUS_UNDER=114305 --CODE FOR CLOSED WITHOUR IDEMNITY(Encerrado sem Indenização)  
--           THEN 211 -- FOR CANCELLATION RESERVE ACTIVITY =>( CLAIM CLOSE )  
--  ELSE                                  204 -- FOR ANY OTHER TYPE ACTIVITY =>( CLAIM REAPPRAISAL)  
--END           AS TIPO_MOV,--TRANSACTION_TYPE,  
--Commented till here 
 --Added by pradeep for itrack-1074 note 13 by Monica
--CASE 
--  WHEN ACT.ACTION_ON_PAYMENT =165 THEN 201 -- FOR CREATE RESERVE ACTIVITY =>( NEW CLAIM REGISTERED)  
--  --WHEN ACT.ACTION_ON_PAYMENT =166 AND (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)=0 AND ABS(TEMP_RESERVE.RESERVE)>0) THEN 201 --Itmeans reserve reopen. 
--  WHEN ACT.ACTION_ON_PAYMENT =166 AND (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)<>0  AND ABS(TEMP_RESERVE.RESERVE)>0) THEN 204 --It means change reserve. 
--  WHEN ACT.ACTION_ON_PAYMENT =167 AND (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)>0 AND ABS(TEMP_RESERVE.RESERVE)=0) THEN 211 --It means reserve closure. 
--  WHEN ACT.ACTION_ON_PAYMENT =168 AND (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)=0 AND ABS(TEMP_RESERVE.RESERVE)>0) THEN 214 --Itmeans reserve reopen. 
--  ELSE 
--	CASE 
--	WHEN (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)=0 AND ABS(TEMP_RESERVE.RESERVE)>0) THEN 214 
--	WHEN (ABS(TEMP_RESERVE.PREVIOUS_RESERVE)>0 AND ABS(TEMP_RESERVE.RESERVE)=0) THEN 211 
--	END -- FOR ANY OTHER TYPE ACTIVITY =>( CLAIM REAPPRAISAL)  
--END           AS TIPO_MOV,--TRANSACTION_TYPE,  
CASE 
  WHEN ACT.ACTION_ON_PAYMENT =165 THEN 201 -- FOR CREATE RESERVE ACTIVITY =>( NEW CLAIM REGISTERED)  
  WHEN ACT.ACTION_ON_PAYMENT =166 THEN 
  CASE WHEN  TEMP_RESERVE.CLAIM_RESERVE_AMOUNT=0 THEN 211 ELSE 204 END --204 --It means change reserve. 
  WHEN ACT.ACTION_ON_PAYMENT =167 THEN 211 --It means reserve closure. 
  WHEN ACT.ACTION_ON_PAYMENT =168 THEN 214 --Itmeans reserve reopen. 
  ELSE 0
END AS TIPO_MOV,--TRANSACTION_TYPE, 
--Added till here itrack-  -1074  
RIGHT('0000' + ISNULL(CAST(RTRIM(LOB.SUSEP_LOB_CODE) AS VARCHAR),''),4)  AS COD_RAMO,--SUSEP_LOB_CODE,  
--Modified by Pradeep As per the itrack-1074-note-13 by Monica
RIGHT('                    '+ CAST(REPLACE(ISNULL(CLM.OFFCIAL_CLAIM_NUMBER,''),'-','') AS VARCHAR),20)              AS NUM_SIN,--CLAIM_NUMBER, --Modified by Pradeep As per the itrack-1074-note-15 by Paula
RIGHT('                    '+ CAST(POL.POLICY_NUMBER AS VARCHAR),20)            AS NUM_APOL,--POLICY_NUMBER,---MODIFIED BY SHUBHANSHU ON 09/08/2011 FOR ITRACK 1074(NOTE 4)  
--Modified till here 
RIGHT('                    '+ CAST(PPE.ENDORSEMENT_NO AS VARCHAR),20)        AS NUM_END,--ENDORSEMENT_NO,  
RIGHT('              '+ CAST(REPLACE(REPLACE(REPLACE(CLM_P.PARTY_CPF_CNPJ,'.',''),'-',''),'/','') AS VARCHAR),14) AS CPF_SEG,--Modified by Pradeep As per the itrack-1074-note-15 by Paula

--CLM_P.PARTY_CPF_CNPJ      AS CPF_SEG,--INSURED_CPF_CNPJ,  
---------------------------------------------------  
--- CALCULATE NUMBER OF INSURED PERSON'S IN POLICY  
---------------------------------------------------  

(SELECT COUNT(CP.PARTY_TYPE_ID)  
 FROM CLM_PARTIES CP WITH(NOLOCK)   
 WHERE CP.CLAIM_ID = CLM.CLAIM_ID  AND  
      CP.PARTY_TYPE_ID IN (10,114)  
 ) AS QTD_SEG,--NUMBER_OF_INSURED,  
ISNULL(RIGHT('000000000000000' + CAST(CLM_B.PARTY_CPF_CNPJ AS VARCHAR),15),'')  AS CPF_BEN,--BENF_CNPJ,  
---------------------------------------------------  
--- CALCULATE BENEFICIARY COUNT OF ACTIVITY  
---------------------------------------------------  
--Modified by Pradeep as per the itrack 1074 note 13 by Monica
(SELECT COUNT(TP.PARTY_ID)  
 FROM CLM_PAYEE TP  WITH(NOLOCK)  
 WHERE TP.CLAIM_ID = ACT.CLAIM_ID AND   
      TP.ACTIVITY_ID= ACT.ACTIVITY_ID  
 ) AS QTD_BEN,--NUMBER_OF_BENEFICIRY,  
 --Modified till here 
---------------------------------------------------  
--- REGISTRATION DATE ( LOSS DATE)  
---------------------------------------------------             
CAST(YEAR(CLM.LOSS_DATE) AS VARCHAR(4))+       -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM.LOSS_DATE) AS VARCHAR(2)),2)+     -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM.LOSS_DATE)AS VARCHAR(2)),2)         -- FOR DAY PART  
           AS DT_REG,--REGISTRATION_DATE,  
---------------------------------------------------  
--- NOTIFICATION DATE ( FNOL DATE)  
---------------------------------------------------   
CAST(YEAR(CLM.FIRST_NOTICE_OF_LOSS) AS VARCHAR(4))+      -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM.FIRST_NOTICE_OF_LOSS) AS VARCHAR(2)),2)+ -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM.FIRST_NOTICE_OF_LOSS)AS VARCHAR(2)),2)     -- FOR DAY PART  
           AS DT_AVISO,--NOTIFICATION_DATE,  
---------------------------------------------------  
--- OCCURRENCE DATE ( LOSS DATE)  
---------------------------------------------------               
CAST(YEAR(CLM.LOSS_DATE) AS VARCHAR(4))+        -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM.LOSS_DATE) AS VARCHAR(2)),2)+     -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM.LOSS_DATE)AS VARCHAR(2)),2)      -- FOR DAY PART  
           AS DT_OCOR,--OCCURRENCE_DATE,  
             
-------------------------------------------------------------------------------  
---- RECOVERABLE AMOUNT FROM ALL FOLLOWER PARTIES           
-------------------------------------------------------------------------------  
--Added by pradeep for itrack-1074 note 13 by Monica
--RIGHT('0000000000000000' + CAST(ABS(TEMP_RESERVE.COI_RESERVE) AS VARCHAR), 16) AS VR_COS_CED,--COI_PORTION_AMT,
CAST((CASE   
WHEN ACT.ACTION_ON_PAYMENT =165        THEN TEMP_RESERVE.RESERVE -- FOR RESERVE CREATION ACTIVITY  
WHEN ACT.ACTION_ON_PAYMENT =167     THEN ABS(ACT.RESERVE_AMOUNT) -- FOR CLOSE RESERVE ACTIVITY --Modified by Pradeep as per the itrack-1074 note - 15 by Paula -Retrieve amount with no signal. 
WHEN ACT.ACTION_ON_PAYMENT IN(166,168) THEN (CASE    
             WHEN TEMP_RESERVE.PREVIOUS_RESERVE=TEMP_RESERVE.RESERVE  
             THEN 0.00             
                         WHEN TEMP_RESERVE.PREVIOUS_RESERVE>TEMP_RESERVE.RESERVE                                                         
             THEN  ABS(TEMP_RESERVE.PREVIOUS_RESERVE-TEMP_RESERVE.RESERVE) --Modified by Pradeep as per the itrack-1074 note - 15 by Paula -Retrieve amount with no signal.
             ELSE  ABS(TEMP_RESERVE.RESERVE-TEMP_RESERVE.PREVIOUS_RESERVE)   
              
            END   
                                             )   
ELSE 0.00 END)*(
CASE WHEN (SELECT sum(PARTY_PERCENTAGE)  FROM CLM_PARTIES WITH(NOLOCK) WHERE PARTY_TYPE_ID=618 AND SOURCE_PARTY_ID<>1 AND SOURCE_PARTY_TYPE_ID=14549 AND CLAIM_ID=CLM.CLAIM_ID)>0
THEN (SELECT sum(PARTY_PERCENTAGE)  FROM CLM_PARTIES WITH(NOLOCK) WHERE PARTY_TYPE_ID=618 AND SOURCE_PARTY_ID<>1 AND SOURCE_PARTY_TYPE_ID=14549 AND CLAIM_ID=CLM.CLAIM_ID)
ELSE 0.00 END 
)*0.01 as decimal(16,2)) AS VR_COS_CED,
--Added till here 
---------------------------------------------------------------  
--- TRANSACTION RESERVE AMOUNT ( LOSS DATE)  
--- IF RESERVE DECREASED THEN PUT '-' AS FIRST DIGIT IN AMOUNT   
--- IF RESERVE INCREASED THEN PUT '0' AS FIRST DIGIT IN AMOUNT   
---------------------------------------------------------------  
--CASE   
--WHEN ACT.ACTION_ON_PAYMENT =165        THEN RIGHT('0000000000000000' + REPLACE(CAST(TEMP_RESERVE.RESERVE AS VARCHAR(30)),'.',','),16) -- FOR RESERVE CREATION ACTIVITY  
--WHEN ACT.ACTION_ON_PAYMENT =167        THEN '-'+RIGHT('000000000000000' + REPLACE(CAST(ABS(ACT.RESERVE_AMOUNT) AS VARCHAR(30)),'.',','),15) -- FOR CLOSE RESERVE ACTIVITY  
--WHEN ACT.ACTION_ON_PAYMENT IN(166,168) THEN (CASE    
--             WHEN TEMP_RESERVE.PREVIOUS_RESERVE=TEMP_RESERVE.RESERVE  
--             THEN '0000000000000,00'             
--                         WHEN TEMP_RESERVE.PREVIOUS_RESERVE>TEMP_RESERVE.RESERVE                                                         
--             THEN  '-'+RIGHT('000000000000000' + REPLACE(CAST(ABS(TEMP_RESERVE.PREVIOUS_RESERVE-TEMP_RESERVE.RESERVE) AS VARCHAR(30)),'.',','),15)  
--             ELSE RIGHT('0000000000000000' + REPLACE(CAST(ABS(TEMP_RESERVE.RESERVE-TEMP_RESERVE.PREVIOUS_RESERVE) AS VARCHAR(30)),'.',','),16)  
              
--            END   
--                                             )   
--ELSE '0000000000000,00' END            AS VR_MOV,--TRANSACTION_AMT,  
CASE   
WHEN ACT.ACTION_ON_PAYMENT =165        THEN TEMP_RESERVE.RESERVE -- FOR RESERVE CREATION ACTIVITY  
WHEN ACT.ACTION_ON_PAYMENT =167     THEN ABS(ACT.RESERVE_AMOUNT) -- FOR CLOSE RESERVE ACTIVITY  --Modified by Pradeep as per the itrack-1074 note - 15 by Paula -Retrieve amount with no signal.
WHEN ACT.ACTION_ON_PAYMENT IN(166,168) THEN (CASE    
             WHEN TEMP_RESERVE.PREVIOUS_RESERVE=TEMP_RESERVE.RESERVE  
             THEN 0.00            
                         WHEN TEMP_RESERVE.PREVIOUS_RESERVE>TEMP_RESERVE.RESERVE                                                         
             THEN  ABS(TEMP_RESERVE.PREVIOUS_RESERVE-TEMP_RESERVE.RESERVE)--Modified by Pradeep as per the itrack-1074 note - 15 by Paula -Retrieve amount with no signal.
             ELSE ABS(TEMP_RESERVE.RESERVE-TEMP_RESERVE.PREVIOUS_RESERVE) 
              
            END   
                                             )   
ELSE 0.00 END            AS VR_MOV,--TRANSACTION_AMT,  
  
---------------------------------------------------  
---- TRANSACTION DATE (ACTIVITY COMPLETED DATE)          
---------------------------------------------------  
CAST(YEAR(ACT.COMPLETED_DATE) AS VARCHAR(4))+    -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(ACT.COMPLETED_DATE) AS VARCHAR(2)),2)+ -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(ACT.COMPLETED_DATE)AS VARCHAR(2)),2)     -- FOR DAY PART  
           AS DT_MOV,--TRANSACTION_DATE,  
  
---------------------------------------------------  
-- CLAIM TYPE        
-- 01: ADMINISTRATIVE INDEMINITY  
-- 02: ADMINISTRATIVE EXPENSES  
-- 03: LEGAL INDEMINITY  
-- 04: LEGAL EXPENSES  
  
-- CHECK IF RESERVE/PAYMENT/RECOVERY DONE AGAINST POLICY COVERAGES THEN IDENMINTY ESLE EXPENSES  
---------------------------------------------------  
CASE WHEN TEMP_RESERVE.IS_RISK_COVERAGE='Y' AND ACT.IS_LEGAL='N' THEN '01' --- ADMINISTRATIVE INDEMINITY  
  WHEN TEMP_RESERVE.IS_RISK_COVERAGE='N' AND ACT.IS_LEGAL='N' THEN '02' --- ADMINISTRATIVE EXPENSES  
     WHEN TEMP_RESERVE.IS_RISK_COVERAGE='Y' AND ACT.IS_LEGAL='Y' THEN '03' --- LEGAL INDEMINITY  
     WHEN TEMP_RESERVE.IS_RISK_COVERAGE='N' AND ACT.IS_LEGAL='Y' THEN '04' --- LEGAL EXPENSES  
END AS TP_SIN--CLAIM_TYPE  
  
FROM   CLM_CLAIM_INFO    AS CLM   WITH (NOLOCK) INNER JOIN  
    CLM_ACTIVITY      AS ACT   WITH (NOLOCK) ON CLM.CLAIM_ID=ACT.CLAIM_ID AND ACTIVITY_STATUS=11801  LEFT OUTER JOIN--COMPLETED ACTIVITY   
       POL_CUSTOMER_POLICY_LIST AS POL   WITH (NOLOCK) ON CLM.CUSTOMER_ID=POL.CUSTOMER_ID AND CLM.POLICY_ID=POL.POLICY_ID AND CLM.POLICY_VERSION_ID=POL.POLICY_VERSION_ID LEFT OUTER JOIN  
     MNT_LOB_MASTER           AS LOB   WITH (NOLOCK) ON CAST(POL.POLICY_LOB AS INT)=LOB.LOB_ID  LEFT OUTER JOIN  
       POL_POLICY_ENDORSEMENTS  AS PPE   WITH (NOLOCK) ON PPE.CUSTOMER_ID=POL.CUSTOMER_ID AND PPE.POLICY_ID=POL.POLICY_ID AND PPE.POLICY_VERSION_ID=POL.POLICY_VERSION_ID AND PPE.ENDORSEMENT_STATUS='COM' LEFT OUTER JOIN  
       CLM_PARTIES              AS CLM_P WITH (NOLOCK) ON CLM_P.CLAIM_ID=CLM.CLAIM_ID AND CLM_P.PARTY_TYPE_ID=10  LEFT OUTER JOIN  
       CLM_PAYEE    AS PAYEE WITH (NOLOCK) ON PAYEE.CLAIM_ID=ACT.CLAIM_ID AND PAYEE.ACTIVITY_ID=ACT.ACTIVITY_ID  LEFT OUTER JOIN  
       CLM_PARTIES              AS CLM_B WITH (NOLOCK) ON CLM_B.CLAIM_ID=PAYEE.CLAIM_ID AND PAYEE.PARTY_ID=CLM_B.PARTY_ID     
    INNER JOIN   
      (  
            SELECT TCAR.CLAIM_ID,TCAR.ACTIVITY_ID, TCPC.IS_RISK_COVERAGE, SUM( TCAR.OUTSTANDING)AS RESERVE,SUM(TCAR.PREV_OUTSTANDING) AS PREVIOUS_RESERVE, SUM(TCAR.CO_RESERVE) AS COI_RESERVE,TCA.CLAIM_RESERVE_AMOUNT   
      FROM  CLM_CLAIM_INFO  TCLM WITH (NOLOCK) INNER JOIN  
      CLM_ACTIVITY   TCA  WITH (NOLOCK) ON TCLM.CLAIM_ID=TCA.CLAIM_ID AND TCA.ACTIVITY_STATUS=11801  
                    AND TCA.IS_ACTIVE='Y'AND TCA.IS_VOIDED_REVERSED_ACTIVITY IS NULL  AND  					
					 TCA.ACTION_ON_PAYMENT IN (165,166,167,168) LEFT OUTER JOIN  
      CLM_ACTIVITY_RESERVE  TCAR WITH (NOLOCK) ON TCAR.CLAIM_ID=TCA.CLAIM_ID AND TCAR.ACTIVITY_ID=TCA.ACTIVITY_ID LEFT OUTER JOIN  
      CLM_PRODUCT_COVERAGES TCPC WITH (NOLOCK) ON TCAR.CLAIM_ID=TCPC.CLAIM_ID AND TCAR.COVERAGE_ID=TCPC.CLAIM_COV_ID  
   WHERE TCLM.CO_INSURANCE_TYPE IN(14547,14548) AND TCLM.CLAIM_STATUS = 11739 AND -- OPEN CLAIMS  
      TCLM.OFFCIAL_CLAIM_NUMBER IS NOT NULL AND   
	  --Modified by pradeep on 30-Aug-2011 as per the itrack-1074 note-12 point(1 and 2)
		TCAR.OUTSTANDING<>TCAR.PREV_OUTSTANDING AND 
		TCPC.COVERAGE_CODE_ID NOT IN (50019,50021) AND --Modified by Pradeep on 05-Sep-2011 as per the itrack-1074 note-14 
	  --Modified till here 
      YEAR(TCA.COMPLETED_DATE)=@YEAR AND MONTH(TCA.COMPLETED_DATE)=@MONTH  
   GROUP BY TCAR.CLAIM_ID,TCAR.ACTIVITY_ID, TCPC.IS_RISK_COVERAGE,TCA.CLAIM_RESERVE_AMOUNT            
   HAVING (SUM( TCAR.OUTSTANDING)+SUM(TCAR.PREV_OUTSTANDING) )>0     
           
      )TEMP_RESERVE   
      ON TEMP_RESERVE.CLAIM_ID=ACT.CLAIM_ID AND TEMP_RESERVE.ACTIVITY_ID=ACT.ACTIVITY_ID   
WHERE CLM.CO_INSURANCE_TYPE IN(14547,14548) AND  -- FOR LEADER AND DIRECT  
      CLM.CLAIM_STATUS = 11739 AND -- OPEN CLAIMS  
      CLM.OFFCIAL_CLAIM_NUMBER IS NOT NULL   
   AND YEAR(ACT.COMPLETED_DATE)=@YEAR AND MONTH(ACT.COMPLETED_DATE)=@MONTH  
   AND ACT.ACTION_ON_PAYMENT IN (165,166,167,168) --MODIFIED BY SHUBHANSHU ON 09/08/2011 FOR ITRACK 1074(NOTE 6)  
      ORDER BY CLM.CLAIM_ID, ACT.ACTIVITY_ID  
	  
SELECT 
   SEQUENCIA ,
   COD_CIA,DT_BASE,
   ISNULL(TIPO_MOV,0) AS TIPO_MOV
  ,COD_RAMO,NUM_SIN ,NUM_APOL
  ,ISNULL(NUM_END,'') AS NUM_END
  ,ISNULL(CPF_SEG,'') AS CPF_SEG,
   ISNULL(QTD_SEG,0) AS  QTD_SEG,
  CPF_BEN ,
  ISNULL(QTD_BEN,0) AS QTD_BEN
  ,DT_REG  ,DT_AVISO,DT_OCOR 
  ,ISNULL(VR_COS_CED,0.00)AS VR_COS_CED
  ,ISNULL(VR_MOV,0.00)AS VR_MOV
  ,DT_MOV 
  ,ISNULL( CAST(TP_SIN AS INT),0) AS  TP_SIN
 FROM @TEMPTABLE  
END     
GO



