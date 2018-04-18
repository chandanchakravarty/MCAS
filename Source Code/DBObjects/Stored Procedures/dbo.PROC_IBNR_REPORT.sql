/*----------------------------------------------------------                                                                    
Proc Name             : Dbo.[PROC_IBNR_REPORT]                                                           
Created by            : PUNEET KUMAR                                          
Date                  : 23 SEPT 2011                                                          
Purpose               :       
Revison History       :                                                                    
Used In               : CLAIM MODULE       
Itrack      : 1664 TFS Bug        
------------------------------------------------------------                                                                    
Date     Review By          Comments                                       
                              
drop Proc [PROC_IBNR_REPORT] '10/01/2011','10/31/2011'                                             
------   ------------       -------------------------*/                                                                    
                                  
CREATE PROCEDURE [dbo].[PROC_IBNR_REPORT]            
@FROM_DATETIME DATETIME =NULL,                          
@TO_DATETIME DATETIME =NULL                                     
AS                                        
BEGIN                                 
             
 --DECLARE @MONTH VARCHAR(2)            
 --DECLARE @YEAR VARCHAR(4)          
          
 --SET @MONTH= MONTH(@DATETIME)          
 --SET @YEAR=YEAR(@DATETIME)        
         
 --IF(LEN(@MONTH)<2)        
 -- SET @MONTH='0'+@MONTH        
      
       
  SELECT      
 CASE WHEN ACT.IS_LEGAL='Y' THEN 'JUD' ELSE 'ADM' END AS 'Tipo de Regulação',      
 CASE WHEN CLM.CO_INSURANCE_TYPE=14549 THEN 'ACE'       
   ELSE 'DIR'           
     END AS 'Tipo Emissão',      
 LOB.SUSEP_LOB_CODE AS 'Cod Ramo',      
 PCUST.POLICY_NUMBER  AS 'Apólice',      
 ISNULL(ENDS.ENDORSEMENT_NO,'')  AS 'Endosso',      
 CLM.CLAIM_NUMBER AS 'Num Aviso',      
  CONVERT( VARCHAR(10),CLM.LOSS_DATE ,103) AS 'Dt. Ocorrência',      
  CONVERT( VARCHAR(10),CLM.FIRST_NOTICE_OF_LOSS ,103) AS 'Dt Aviso',      
  dbo.fun_FormatCurrency(ACT.CLAIM_RESERVE_AMOUNT,2) AS 'Vlr Estimativa',            
  dbo.fun_FormatCurrency(ACT.CO_TOTAL_RESERVE_AMT,2) AS 'Vl Ced Cosseg',            
  dbo.fun_FormatCurrency(ACT.CLAIM_RI_RESERVE,2)     AS 'Vlr Ced Resseg',            
  dbo.fun_FormatCurrency(ABS(SUM(ACT.CLAIM_RESERVE_AMOUNT - (ACT.CLAIM_RI_RESERVE + ACT.CO_TOTAL_RESERVE_AMT))),2) AS 'Vlr Retido'    
  -- dbo.fun_FormatCurrency(ABS(SUM(CAR.OUTSTANDING_TRAN)),2) AS 'Vlr Estimativa',    
  --dbo.fun_FormatCurrency(ABS(SUM(CAR.CO_RESERVE_TRAN)),2) AS 'Vl Ced Cosseg',    
  --dbo.fun_FormatCurrency(ABS(SUM(CAR.RI_RESERVE_TRAN)),2)     AS 'Vlr Ced Resseg',    
  --dbo.fun_FormatCurrency(ABS(SUM(CAR.OUTSTANDING_TRAN) - (SUM(CAR.CO_RESERVE_TRAN) + SUM(CAR.RI_RESERVE_TRAN))),2) AS 'Vlr Retido'   
FROM CLM_CLAIM_INFO CLM WITH(NOLOCK)
LEFT JOIN MNT_LOB_MASTER LOB WITH(NOLOCK) ON LOB.LOB_ID=CAST(ISNULL(CLM.LOB_ID,0) AS INT)         
INNER JOIN CLM_ACTIVITY ACT WITH(NOLOCK) ON ACT.CLAIM_ID=CLM.CLAIM_ID  AND ACT.ACTIVITY_ID = (SELECT MAX(ACTIVITY_ID) FROM CLM_ACTIVITY CA WHERE ACT.CLAIM_ID = CA.CLAIM_ID  AND CA.ACTIVITY_STATUS = '11801')             
--LEFT JOIN CLM_ACTIVITY_RESERVE CAR ON CAR.CLAIM_ID = CLM.CLAIM_ID AND CAR.ACTIVITY_ID = ACT.ACTIVITY_ID         
LEFT JOIN POL_CUSTOMER_POLICY_LIST PCUST WITH(NOLOCK) ON PCUST.CUSTOMER_ID=CLM.CUSTOMER_ID AND PCUST.POLICY_ID=CLM.POLICY_ID AND PCUST.POLICY_VERSION_ID=CLM.POLICY_VERSION_ID         
LEFT OUTER JOIN POL_POLICY_ENDORSEMENTS ENDS WITH(NOLOCK) ON ENDS.CUSTOMER_ID=CLM.CUSTOMER_ID AND ENDS.POLICY_ID=CLM.POLICY_ID AND ENDS.POLICY_VERSION_ID=CLM.POLICY_VERSION_ID     
  
WHERE --YEAR(ACT.COMPLETED_DATE)=@YEAR AND MONTH(ACT.COMPLETED_DATE)=@MONTH  
CLM.CLAIM_STATUS=11739   
AND ACT.IS_ACTIVE='Y'           
AND CLM.FIRST_NOTICE_OF_LOSS BETWEEN @FROM_DATETIME AND @TO_DATETIME    
AND ISNULL(ACT.IS_VOIDED_REVERSED_ACTIVITY,'')=''   
  
   
GROUP BY   ACT.IS_LEGAL,CLM.CO_INSURANCE_TYPE,LOB.SUSEP_LOB_CODE,PCUST.POLICY_NUMBER,ENDS.ENDORSEMENT_NO,CLM.CLAIM_NUMBER,CLM.LOSS_DATE,CLM.FIRST_NOTICE_OF_LOSS,ACT.CLAIM_RESERVE_AMOUNT,ACT.CO_TOTAL_RESERVE_AMT,ACT.CLAIM_RI_RESERVE        
      
HAVING SUM(ACT.CLAIM_RESERVE_AMOUNT) <> 0      
ORDER BY LOB.SUSEP_LOB_CODE  
END 