IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SALRESAV_DATA_OF_SALVAGE_REIMBURSEMENT_OF_ACCEPTED_POLICIES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SALRESAV_DATA_OF_SALVAGE_REIMBURSEMENT_OF_ACCEPTED_POLICIES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_SALRESAV_DATA_OF_SALVAGE_REIMBURSEMENT_OF_ACCEPTED_POLICIES]                                                            
Created by            : SANTOSH KUMAR GAUTAM                                                  
Date                  : 22 JULY 2011                                                          
Purpose               : TO CREATE SUSEP REPORT FOR CLAIM 
Revison History       :                                                            
Used In               : CLAIM MODULE           
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_SALRESAV_DATA_OF_SALVAGE_REIMBURSEMENT_OF_ACCEPTED_POLICIES] '2011-06-12 18:42:38.793'                                     
------   ------------       -------------------------*/                                                            
                          
CREATE PROCEDURE [dbo].[PROC_SALRESAV_DATA_OF_SALVAGE_REIMBURSEMENT_OF_ACCEPTED_POLICIES]    
  
@DATETIME					DATETIME
                               
AS                                
BEGIN                         
    

 DECLARE @MONTH VARCHAR(2)    
 DECLARE @YEAR VARCHAR(4)  
  
 SET @MONTH= MONTH(@DATETIME)  
 SET @YEAR=YEAR(@DATETIME)
 
 IF(LEN(@MONTH)<2)
  SET @MONTH='0'+@MONTH
 



SELECT   
--ACT.CLAIM_ID,  
--ACT.ACTIVITY_ID,  
--ACT.ACTION_ON_PAYMENT,  
--CLM_C.COVERAGE_CODE_ID  
--,  
  
RIGHT('0000000000'+CAST(ROW_NUMBER() OVER(ORDER BY ACT.CLAIM_ID, ACT.ACTIVITY_ID ASC)AS VARCHAR(10)),10)   
                                      AS SEQUENCIA,--SEQUANCE_NUMBER,  
'504-5'           AS COD_CIA, --COMPANY_CODE, --ALIANCA DA BAHIA SUSEP CODE  
(@YEAR+@MONTH)         AS DT_BASE, --BASE_DATE,  
---------------------------------------------------  
--- TYPE OF TRANSACTION  
---------------------------------------------------  
/*CASE WHEN ACT.ACTION_ON_PAYMENT = 165 AND CLM_C.COVERAGE_CODE_ID=50019 THEN 202 --NEW RESERVE  
  WHEN ACT.ACTION_ON_PAYMENT = 165 AND CLM_C.COVERAGE_CODE_ID=50021 THEN 203 --NEW RESERVE  
    
  WHEN ACT.ACTION_ON_PAYMENT = 167 AND CLM_C.COVERAGE_CODE_ID=50019 THEN 212 --FOR CLOSE RESERVE  
  WHEN ACT.ACTION_ON_PAYMENT = 167 AND CLM_C.COVERAGE_CODE_ID=50021 THEN 213 --FOR CLOSE RESERVE  
    
  WHEN ACT.ACTION_ON_PAYMENT = 168 AND CLM_C.COVERAGE_CODE_ID=50019 THEN 215 --FOR REOPEN RESERVE  
  WHEN ACT.ACTION_ON_PAYMENT = 168 AND CLM_C.COVERAGE_CODE_ID=50021 THEN 216 --FOR REOPEN RESERVE  
    
  WHEN ACT.ACTION_ON_PAYMENT IN(166,180,181) AND CLM_C.COVERAGE_CODE_ID=50019 THEN 205 --FOR PAYMENT, CHANGE RESERVE  
  WHEN ACT.ACTION_ON_PAYMENT IN(166,180,181) AND CLM_C.COVERAGE_CODE_ID=50021 THEN 206 --FOR PAYMENT,CHANGE RESERVE  
    
  WHEN ACT.ACTION_ON_PAYMENT IN(190,192) AND CLM_C.COVERAGE_CODE_ID=50019 THEN 209 --FOR RECOVERY  
  WHEN ACT.ACTION_ON_PAYMENT IN(190,192) AND CLM_C.COVERAGE_CODE_ID=50021 THEN 210 --FOR RECOVERY   
           
  ELSE 205 -- FOR ANY OTHER TYPE ACTIVITY   
END           AS TIPO_MOV, --TRANSACTION_TYPE, */


CASE 
  WHEN ACT.ACTION_ON_PAYMENT = 166 THEN
	 CASE        
 --CREATION        
  WHEN  CLM_R.PREV_OUTSTANDING = 0.00 AND CLM_R.OUTSTANDING > 0.00 AND  CLM_C.COVERAGE_CODE_ID IN (50019) THEN '202'        
  WHEN      CLM_R.PREV_OUTSTANDING = 0.00 AND CLM_R.OUTSTANDING > 0.00 AND  CLM_C.COVERAGE_CODE_ID IN (50021) THEN '203'        
 --CLOSE         
  WHEN (SELECT SUM(CAR2.PREV_OUTSTANDING)             
    FROM CLM_ACTIVITY_RESERVE CAR2             
    WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) > 0.00          
     AND  CLM_C.COVERAGE_CODE_ID IN (50019)           
                
   AND (SELECT SUM(CAR2.OUTSTANDING)  FROM CLM_ACTIVITY_RESERVE CAR2             
    WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) = 0.00         
       AND  CLM_C.COVERAGE_CODE_ID IN (50019) THEN '212'  --CLOSE        
     WHEN (SELECT SUM(CAR2.PREV_OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) > 0.00 AND  CLM_C.COVERAGE_CODE_ID IN (50021)            
        AND (SELECT SUM(CAR2.OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) = 0.00  AND  CLM_C.COVERAGE_CODE_ID IN (50021)          
         THEN '213'  --CLOSE        
    --REOPEN        
  WHEN (SELECT SUM(CAR2.PREV_OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID)  = 0.00  AND  CLM_C.COVERAGE_CODE_ID IN (50019)              
        AND (SELECT SUM( CAR2.OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) > 0.00  AND  CLM_C.COVERAGE_CODE_ID IN (50019)          
         THEN '215' --REOPEN          
    WHEN (SELECT SUM(CAR2.PREV_OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID)  = 0.00  AND  CLM_C.COVERAGE_CODE_ID IN (50021)           
        AND (SELECT SUM( CAR2.OUTSTANDING)             
         FROM CLM_ACTIVITY_RESERVE CAR2             
         WHERE CAR2.CLAIM_ID = CLM_R.CLAIM_ID AND CAR2.ACTIVITY_ID = CLM_R.ACTIVITY_ID) > 0.00 AND  CLM_C.COVERAGE_CODE_ID IN (50021)        
         THEN '216' --REOPEN     
    WHEN    CLM_C.COVERAGE_CODE_ID IN (50019)    THEN  '205'    
    WHEN    CLM_C.COVERAGE_CODE_ID IN (50021)    THEN  '206'    
                                
 END  
  WHEN ACT.ACTION_ON_PAYMENT = 165 AND CLM_C.COVERAGE_CODE_ID In (50019) THEN 202 --NEW RESERVE        
  WHEN ACT.ACTION_ON_PAYMENT = 165 AND CLM_C.COVERAGE_CODE_ID IN (50021) THEN 203 --NEW RESERVE        
          
  WHEN ACT.ACTION_ON_PAYMENT = 167 AND CLM_C.COVERAGE_CODE_ID In (50019) THEN 212 --FOR CLOSE RESERVE        
  WHEN ACT.ACTION_ON_PAYMENT = 167 AND CLM_C.COVERAGE_CODE_ID IN (50021) THEN 213 --FOR CLOSE RESERVE        
          
  WHEN ACT.ACTION_ON_PAYMENT = 168 AND CLM_C.COVERAGE_CODE_ID In (50019) THEN 215 --FOR REOPEN RESERVE        
  WHEN ACT.ACTION_ON_PAYMENT = 168 AND CLM_C.COVERAGE_CODE_ID IN (50021) THEN 216 --FOR REOPEN RESERVE        
          
  WHEN ACT.ACTION_ON_PAYMENT IN(166,180,181) AND CLM_C.COVERAGE_CODE_ID In (50019) THEN 205 --FOR PAYMENT, CHANGE RESERVE        
  WHEN ACT.ACTION_ON_PAYMENT IN(166,180,181) AND CLM_C.COVERAGE_CODE_ID IN (50021) THEN 206 --FOR PAYMENT,CHANGE RESERVE        
          
  WHEN ACT.ACTION_ON_PAYMENT IN(190,192) AND CLM_C.COVERAGE_CODE_ID In (50019) THEN 209 --FOR RECOVERY        
  WHEN ACT.ACTION_ON_PAYMENT IN(190,192) AND CLM_C.COVERAGE_CODE_ID IN (50021) THEN 210 --FOR RECOVERY         
                 
  ELSE 205 -- FOR ANY OTHER TYPE ACTIVITY         
END           AS TIPO_MOV, 
  
--LOB.SUSEP_LOB_CODE                    AS COD_RAMO, --SUSEP_LOB_CODE,
ISNULL(MLM2.SUSEP_LOB_CODE,'') AS COD_RAMO, 
LEFT(ISNULL(COMP.REIN_COMAPANY_CODE,''),3) +'-' + RIGHT(ISNULL(COMP.REIN_COMAPANY_CODE,''),1)      AS COD_COSS, -- LEADER COMPANY CODE    
RIGHT(ISNULL(CLM_CO.LEADER_CLAIM_NUMBER,''),20)            AS NUM_SIN, --CLAIM_NUMBER,       
ISNULL(RIGHT(CONVERT(VARCHAR,CLM_CO.LEADER_POLICY_NUMBER),20),'')           AS NUM_APOL, --POLICY_NUMBER,   
RIGHT(ISNULL(CLM_CO.LEADER_ENDORSEMENT_NUMBER,''),20)      AS NUM_END, --ENDORSEMENT_NO,  
  
---------------------------------------------------  
--- REGISTRATION DATE ( LOSS DATE)  
---------------------------------------------------             
ISNULL(CAST(YEAR(CLM_CO.CLAIM_REGISTRATION_DATE) AS VARCHAR(4))+    -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM_CO.CLAIM_REGISTRATION_DATE) AS VARCHAR(2)),2)+ -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM_CO.CLAIM_REGISTRATION_DATE)AS VARCHAR(2)),2),'')     -- FOR DAY PART  
           AS DT_REG, --REGISTRATION_DATE,  
---------------------------------------------------  
--- NOTIFICATION DATE ( FNOL DATE)  
---------------------------------------------------   
CAST(YEAR(CLM.FIRST_NOTICE_OF_LOSS) AS VARCHAR(4))+        -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM.FIRST_NOTICE_OF_LOSS) AS VARCHAR(2)),2)+   -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM.FIRST_NOTICE_OF_LOSS)AS VARCHAR(2)),2)       -- FOR DAY PART  
           AS DT_AVISO, --NOTIFICATION_DATE,  
---------------------------------------------------  
--- OCCURRENCE DATE ( LOSS DATE)  
---------------------------------------------------               
CAST(YEAR(CLM.LOSS_DATE) AS VARCHAR(4))+          -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(CLM.LOSS_DATE) AS VARCHAR(2)),2)+       -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(CLM.LOSS_DATE)AS VARCHAR(2)),2)        -- FOR DAY PART  
           AS DT_OCOR, --OCCURRENCE_DATE,  
             
-------------------------------------------------------------------------------  
---- NOTE : HERE POLICY IS DIRECT TYPE SO POLICY CANNOT HAVE COINSURANCE PARTY              
-------------------------------------------------------------------------------  
--CAST(ACT.CO_TRAN_RESERVE_AMT AS VARCHAR(30))   AS COI_PORTION_AMT, --COMMENT BY KULDEEP AS PER ITRACK 1249  
---------------------------------------------------------------  
--- TRANSACTION RESERVE AMOUNT ( LOSS DATE)  
--- IF RESERVE DECREASED THEN PUT '-' AS FIRST DIGIT IN AMOUNT   
--- IF RESERVE INCREASED THEN PUT '0' AS FIRST DIGIT IN AMOUNT   
---------------------------------------------------------------  
CASE               
WHEN ACT.ACTION_ON_PAYMENT =165  THEN  ABS(CLM_R.OUTSTANDING_TRAN)     --RIGHT('0000000000000000' +REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),16) -- FOR RESERVE CREATION ACTIVITY              
WHEN ACT.ACTION_ON_PAYMENT =167        THEN ABS(ACT.RESERVE_AMOUNT)  --'-'+RIGHT('000000000000000'+REPLACE(CAST(ABS(ACT.RESERVE_AMOUNT) AS VARCHAR(30)),'.',''),15) -- FOR CLOSE RESERVE ACTIVITY              
WHEN ACT.ACTION_ON_PAYMENT IN (190,192)        
 THEN CLM_R.RECOVERY_AMOUNT        
   WHEN ACT.ACTION_ON_PAYMENT IN(166,168,180,181)--IN(166,168,180,181,190,192)   
--WHEN ACT.ACTION_ON_PAYMENT IN(166,168,180,181,190,192)   
THEN (CASE              
                  WHEN CLM_R.OUTSTANDING_TRAN IS NULL THEN 0  --'0000000000000000' --FOR CASE WHEN CHANGE RESERVE ACTIVITY COMPLETED WITHOUT ANY CHANGE               
                  WHEN (CLM_R.PREV_OUTSTANDING-CLM_R.OUTSTANDING) =0              
                  THEN ABS(CLM_R.OUTSTANDING_TRAN) -- RIGHT('0000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),16)              
                              WHEN (CLM_R.PREV_OUTSTANDING-CLM_R.OUTSTANDING) >0                                                                       
                  THEN ABS(CLM_R.OUTSTANDING_TRAN) -- '-'+RIGHT('000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),15)              
                  ELSE ABS(CLM_R.OUTSTANDING_TRAN)  --'0'+RIGHT('000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),15)              
                 END               
                                             )               
ELSE 0 END          AS VR_MOV, --TRANSACTION_AMT,  
  
---------------------------------------------------  
---- TRANSACTION DATE (ACTIVITY COMPLETED DATE)          
---------------------------------------------------  
CAST(YEAR(ACT.COMPLETED_DATE) AS VARCHAR(4))+    -- FOR YEAR PART  
RIGHT('0'+CAST(MONTH(ACT.COMPLETED_DATE) AS VARCHAR(2)),2)+ -- FOR MONTH PART  
RIGHT('0'+CAST(DAY(ACT.COMPLETED_DATE)AS VARCHAR(2)),2)     -- FOR DAY PART  
                AS DT_MOV, --TRANSACTION_DATE,  
---------------------------------------------------  
-- CLAIM TYPE        
-- 01: ADMINISTRATIVE INDEMINITY  
-- 02: ADMINISTRATIVE EXPENSES  
-- 03: LEGAL INDEMINITY  
-- 04: LEGAL EXPENSES  
  
-- CHECK IF RESERVE/PAYMENT/RECOVERY DONE AGAINST POLICY COVERAGES THEN IDENMINTY ESLE EXPENSES  
---------------------------------------------------  
/*CASE WHEN ACT.ACTION_ON_PAYMENT=167  THEN '00' --- FOR CLOSE RESERVE ACTIVITY  
     WHEN (CLM_C.IS_RISK_COVERAGE='Y' OR CLM_C.IS_RISK_COVERAGE IS NULL) AND ACT.IS_LEGAL='N' THEN '01' --- ADMINISTRATIVE INDEMINITY  
  WHEN (CLM_C.IS_RISK_COVERAGE='N' OR CLM_C.IS_RISK_COVERAGE IS NULL) AND ACT.IS_LEGAL='N' THEN '02' --- ADMINISTRATIVE EXPENSES  
     WHEN (CLM_C.IS_RISK_COVERAGE='Y' OR CLM_C.IS_RISK_COVERAGE IS NULL) AND ACT.IS_LEGAL='Y' THEN '03' --- LEGAL INDEMINITY  
     WHEN (CLM_C.IS_RISK_COVERAGE='N' OR CLM_C.IS_RISK_COVERAGE IS NULL) AND ACT.IS_LEGAL='Y' THEN '04' --- LEGAL EXPENSES  
END AS TP_SIN --CLAIM_TYPE  */
/*CASE WHEN ACT.ACTION_ON_PAYMENT=167  THEN '00' --- FOR CLOSE RESERVE ACTIVITY        
     WHEN CLM_C.COVERAGE_CODE_ID IN (50019,50021) AND ACT.IS_LEGAL='N' THEN '01' --- ADMINISTRATIVE INDEMINITY        
     WHEN CLM_C.COVERAGE_CODE_ID IN (50020,50022)  AND ACT.IS_LEGAL='N' THEN '02' --- ADMINISTRATIVE EXPENSES        
     WHEN CLM_C.COVERAGE_CODE_ID IN (50019,50021) AND ACT.IS_LEGAL='Y' THEN '03' --- LEGAL INDEMINITY        
     WHEN CLM_C.COVERAGE_CODE_ID IN (50020,50022) AND ACT.IS_LEGAL='Y' THEN '04' --- LEGAL EXPENSES        
END AS TP_SIN*/
CASE  WHEN ACT.IS_LEGAL='Y' THEN '03' --- ADMINISTRATIVE INDEMINITY              
     WHEN ACT.IS_LEGAL='N' THEN '01'              
END AS TP_SIN 

FROM   CLM_CLAIM_INFO    AS CLM   WITH (NOLOCK) INNER JOIN  
       CLM_ACTIVITY      AS ACT   WITH (NOLOCK) ON CLM.CLAIM_ID=ACT.CLAIM_ID AND ACTIVITY_STATUS=11801   LEFT OUTER JOIN--COMPLETED ACTIVITY   
       MNT_LOB_MASTER           AS LOB   WITH (NOLOCK) ON CAST(CLM.LOB_ID AS INT)=LOB.LOB_ID  LEFT OUTER JOIN         
       CLM_PARTIES              AS CLM_P WITH (NOLOCK) ON CLM_P.CLAIM_ID=CLM.CLAIM_ID AND CLM_P.PARTY_TYPE_ID=10  LEFT OUTER JOIN  
       CLM_PAYEE    AS PAYEE WITH (NOLOCK) ON PAYEE.CLAIM_ID=ACT.CLAIM_ID AND PAYEE.ACTIVITY_ID=ACT.ACTIVITY_ID  LEFT OUTER JOIN  
       CLM_PARTIES              AS CLM_B WITH (NOLOCK) ON CLM_B.CLAIM_ID=PAYEE.CLAIM_ID AND PAYEE.PARTY_ID=CLM_B.PARTY_ID  LEFT OUTER JOIN  
       CLM_PARTIES              AS PARTY WITH (NOLOCK) ON PARTY.CLAIM_ID=CLM.CLAIM_ID AND PARTY.SOURCE_PARTY_TYPE_ID=14548 LEFT OUTER JOIN -- PARTY SHOULD BE LEADER ( 14548 :LEADER ,14549 :FOLLOWER)   
       MNT_REIN_COMAPANY_LIST   AS COMP  WITH (NOLOCK) ON COMP.REIN_COMAPANY_ID=PARTY.SOURCE_PARTY_ID  LEFT OUTER JOIN  
       CLM_CO_INSURANCE         AS CLM_CO WITH (NOLOCK) ON CLM_CO.CLAIM_ID=CLM.CLAIM_ID LEFT OUTER JOIN  
       CLM_ACTIVITY_RESERVE     AS CLM_R WITH (NOLOCK) ON CLM_R.CLAIM_ID=ACT.CLAIM_ID AND CLM_R.ACTIVITY_ID=ACT.ACTIVITY_ID LEFT OUTER JOIN  
      CLM_PRODUCT_COVERAGES    AS CLM_C WITH (NOLOCK)  ON CLM_R.CLAIM_ID=CLM_C.CLAIM_ID AND CLM_R.COVERAGE_ID=CLM_C.CLAIM_COV_ID AND CLM_C.COVERAGE_CODE_ID IN (50019,50021)                        
	  LEFT OUTER JOIN      
	[MNT_COVERAGE_ACCOUNTING_LOB] MCAL (NOLOCK) ON CLM_C.COVERAGE_CODE_ID = MCAL.COV_ID      
	LEFT OUTER JOIN       
	MNT_LOB_MASTER MLM2 (NOLOCK) ON MLM2.LOB_ID = MCAL.ACC_LOB_ID 
WHERE CLM.CO_INSURANCE_TYPE =14549 AND  -- FOR FOLLOWER  
      CLM.CLAIM_STATUS = 11739 AND -- OPEN CLAIMS   
      CLM_C.COVERAGE_CODE_ID IN (50019,50021)  AND 
      ISNULL(CLM.OFFCIAL_CLAIM_NUMBER,'') <>''  AND
      YEAR(ACT.COMPLETED_DATE)=YEAR(@DATETIME) AND MONTH(ACT.COMPLETED_DATE)=MONTH(@DATETIME) 
      AND ISNULL( CASE   
			WHEN ACT.ACTION_ON_PAYMENT =165        THEN  CLM_R.OUTSTANDING_TRAN --RIGHT('0000000000000000' +REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),16) -- FOR RESERVE CREATION ACTIVITY  
			WHEN ACT.ACTION_ON_PAYMENT =167        THEN	 ACT.RESERVE_AMOUNT						--'-'+RIGHT('000000000000000'+REPLACE(CAST(ABS(ACT.RESERVE_AMOUNT) AS VARCHAR(30)),'.',''),15) -- FOR CLOSE RESERVE ACTIVITY  
			 WHEN ACT.ACTION_ON_PAYMENT IN (190,192)        
			 THEN CLM_R.RECOVERY_AMOUNT  
			  -- WHEN ACT.ACTION_ON_PAYMENT IN(166,168,180,181,190,192)  
			  WHEN ACT.ACTION_ON_PAYMENT IN(166,168,180,181)  
				THEN (CASE  
					  WHEN CLM_R.OUTSTANDING_TRAN IS NULL THEN  0 --'0000000000000000' --FOR CASE WHEN CHANGE RESERVE ACTIVITY COMPLETED WITHOUT ANY CHANGE   
					  WHEN (CLM_R.PREV_OUTSTANDING-CLM_R.OUTSTANDING) =0  
							THEN CLM_R.OUTSTANDING_TRAN  --RIGHT('0000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),16)  
					  WHEN (CLM_R.PREV_OUTSTANDING-CLM_R.OUTSTANDING) >0                                                           
							THEN	CLM_R.OUTSTANDING_TRAN  --THEN  '-'+RIGHT('000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),15)  
					  ELSE  CLM_R.OUTSTANDING_TRAN --'0'+RIGHT('000000000000000'+REPLACE(CAST(ABS(CLM_R.OUTSTANDING_TRAN) AS VARCHAR(30)),'.',''),15)  
					 END )   
			ELSE 0 END,0)<>0  
ORDER BY ACT.CLAIM_ID, ACT.ACTIVITY_ID

                      
END   









GO

