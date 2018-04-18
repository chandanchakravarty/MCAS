IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_R_COMP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_R_COMP]
GO

SET ANSI_NULLS ON
GO
-- DROP PROCEDURE [dbo].[PROC_SUSEP_R_COMP]  '08/31/2011','89982011330118000016'    
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PROC_SUSEP_R_COMP]        
@DATETIME DATETIME =NULL,          
@POLICY_NUMBER VARCHAR(30) = NULL          
AS           
BEGIN   
           
SELECT COD_SEG, PROCESSO, TIPO,   CLASSE,  APOLICE , ENDOSSO ,COD_END, ITEM,          
  COBERTURA,UF,INICIO_VIG, FIM_VIG,TIPO_FRANQ, VAL_FRANQ,IMP_SEG,         
   RIGHT(SUBSTRING(CAST(ROUND(ABS(ISNULL(PREMIO,0))        
    ,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ABS(ISNULL(PREMIO,0)),0,1)AS VARCHAR),1) ),9)  as PREMIO         
   ,RIGHT(SUBSTRING(CAST(ROUND(ABS(ISNULL(CORRETAGEM,0))        
    ,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ABS(ISNULL(CORRETAGEM,0)),0,1)AS VARCHAR),1) ),7)  as CORRETAGEM  
   ,          
  PERC_DESC                         
FROM          
(          
SELECT                                         
VW.CUSTOMER_ID                                        
,VW.POLICY_ID                                        
,VW.POLICY_VERSION_ID,              
--RISK_ID              
              
-------REPORT FIELDS--------              
 VW.SUSEP_INSURER_CODE AS COD_SEG              
               
, ISNULL(VW.SUSEP_PROCESS_NUMBER,'') AS PROCESSO              
--------Type              
,ISNULL(CASE WHEN  PRLI.CLASS_FIELD = 14730 THEN '1'              
   WHEN  PRLI.CLASS_FIELD = 14731 THEN '1'              
   WHEN  PRLI.CLASS_FIELD = 14732 THEN '1'              
   WHEN  PRLI.CLASS_FIELD = 14733 THEN '1'              
   WHEN  PRLI.CLASS_FIELD = 14734 THEN '1'              
   WHEN  PRLI.CLASS_FIELD = 14735 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14736 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14737 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14738 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14739 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14740 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14741 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14742 THEN '2'              
   WHEN  PRLI.CLASS_FIELD = 14743 THEN '3'              
   WHEN  PRLI.CLASS_FIELD = 14744 THEN '3'              
   WHEN  PRLI.CLASS_FIELD = 14745 THEN '3'              
   WHEN  PRLI.CLASS_FIELD = 14746 THEN '3'              
   WHEN  PRLI.CLASS_FIELD = 14747 THEN '3'              
 ELSE NULL              
 END,'') AS TIPO --CASE              
               
               
              
--CLASS              
, ISNULL(CASE WHEN  PRLI.CLASS_FIELD = 14730 THEN '01'              
    WHEN  PRLI.CLASS_FIELD = 14731 THEN '02'              
    WHEN  PRLI.CLASS_FIELD = 14732 THEN '03'              
    WHEN  PRLI.CLASS_FIELD = 14733 THEN '04'              
    WHEN  PRLI.CLASS_FIELD = 14734 THEN '99'              
    WHEN  PRLI.CLASS_FIELD = 14735 THEN '01'              
    WHEN  PRLI.CLASS_FIELD = 14736 THEN '02'              
    WHEN  PRLI.CLASS_FIELD = 14737 THEN '03'              
    WHEN  PRLI.CLASS_FIELD = 14738 THEN '04'              
    WHEN  PRLI.CLASS_FIELD = 14739 THEN '05'              
    WHEN  PRLI.CLASS_FIELD = 14740 THEN '06'              
    WHEN  PRLI.CLASS_FIELD = 14741 THEN '07'              
    WHEN  PRLI.CLASS_FIELD = 14742 THEN '99'              
    WHEN  PRLI.CLASS_FIELD = 14743 THEN '01'              
    WHEN  PRLI.CLASS_FIELD = 14744 THEN '02'              
    WHEN  PRLI.CLASS_FIELD = 14745 THEN '03'              
    WHEN  PRLI.CLASS_FIELD = 14746 THEN '04'              
    WHEN  PRLI.CLASS_FIELD = 14747 THEN '05'              
 ELSE NULL              
 END,'') AS  CLASSE --CASE              
              
,RIGHT('00000000000000000000'+ CONVERT(VARCHAR, VW.POLICY_NUMBER),20) AS APOLICE          
              
,RIGHT('000000000'+ CONVERT(VARCHAR, VW.ENDORSEMENT_NO),10) AS ENDOSSO          
              
--,VW.ENDORSEMENT_TYPE AS COD_END --CASE              
,CASE  WHEN VW.PROCESS_ID =12 THEN '3' ---CANCELLATION OF POLICY/COVERAGE OR ITEM EXCLUSION NEED TO IMPLEMENT           
    WHEN VW.PROCESS_ID =37  THEN '4'  --VW.ENDORSEMENT_TYPE =14685  --Cancelation Endorsement              
             
   WHEN VW.ENDORSEMENT_TYPE =14684 THEN   --Refund Endorsement          
    CASE WHEN (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_4 WITH(NOLOCK)           
     WHERE PPC_4.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_4.POLICY_ID = PCPL.POLICY_ID          
      AND PPC_4.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  ) <          
   (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)           
     WHERE PPC_3.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_3.POLICY_ID = PCPL.POLICY_ID          
      AND PPC_3.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID-1  )             
              
  THEN '3'            
                   
   ELSE '2' END                                         
   WHEN VW.ENDORSEMENT_TYPE =14682 THEN --Additional Endorsement           
 CASE WHEN (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_4 WITH(NOLOCK)           
    WHERE PPC_4.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_4.POLICY_ID = PCPL.POLICY_ID          
     AND PPC_4.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  ) >          
   (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)           
    WHERE PPC_3.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_3.POLICY_ID = PCPL.POLICY_ID          
     AND PPC_3.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID-1  )           
   THEN  '1'           
   ELSE '2'          
  END                                   
                                    
                               
   ELSE '0'                                    
  END AS COD_END              
             
--,RIGHT('000000'+CONVERT(VARCHAR, isnull(PRLI.ITEM_NUMBER, '')),6)  AS ITEM 
,RIGHT('000000'+CONVERT(VARCHAR, isnull(PRLI.LOCATION_NUMBER, '')),6)  AS ITEM             
            
            
,MC.SUSEP_COV_CODE AS COBERTURA --CASE         
--,CASE WHEN  VW.COVERAGE_CODE_ID  ='4184' THEN '10'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4185' THEN '20'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4186' THEN '30'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4187' THEN '40'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4188' THEN '50'          
--   WHEN  VW.COVERAGE_CODE_ID  = '4189' THEN '60'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4190' THEN '70'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4191' THEN '80'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4192' THEN '90'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4193' THEN '100'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4194' THEN '110'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4195' THEN '120'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4196' THEN '130'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4197' THEN '140'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4198' THEN '150'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4199' THEN '160'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4200' THEN '170'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4201' THEN '180'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4202' THEN '190'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4203' THEN '200'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4204' THEN '210'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4205' THEN '220'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4206' THEN '230'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4207' THEN '240'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4208' THEN '250'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4209' THEN '260'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4210' THEN '270'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4211' THEN '280'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4212' THEN '290'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4213' THEN '300'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4214' THEN '310'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4215' THEN '320'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4216' THEN '330'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4217' THEN '340'          
--   WHEN  VW.COVERAGE_CODE_ID  ='4218' THEN '999'          
--   ELSE ''             
--  END AS COBERTURA          
            
,VW.STATE_CODE  AS UF           
          
,LEFT(REPLACE(CONVERT(varchar,VW.POLICY_EFFECTIVE_DATE,111),'/',''),8)  AS INICIO_VIG          
           
,LEFT(REPLACE(CONVERT(varchar,VW.POLICY_EXPIRATION_DATE,111),'/',''),8) AS FIM_VIG            
               
--,CASE WHEN VW.DEDUCTIBLE_1_TYPE =14573 THEN '1'              
--   WHEN VW.DEDUCTIBLE_1_TYPE =14574 THEN '2'              
--   WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN '3'              
--   WHEN VW.DEDUCTIBLE_1_TYPE =14576 THEN '5'              
--   WHEN ISNULL(VW.DEDUCTIBLE_1_TYPE,'') ='' THEN '9'      
--   WHEN ISNULL(VW.DEDUCTIBLE_1,0) =0 THEN '9'              
--   ELSE '9'              
--   END AS TIPO_FRANQ  --CASE  

,CASE WHEN  VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574)       
     AND  ( LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    )      
     AND  ISNULL(VW.DEDUCTIBLE_1,0) = 0      
     AND ISNULL(VW.MINIMUM_DEDUCTIBLE,0)=0 THEN '9'       
   WHEN VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574) AND      
    (      
   ( LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) > 0                                 
    OR LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) > 0      
   )      
   AND       
   ISNULL(VW.DEDUCTIBLE_1,0) = 0           
   AND ISNULL(VW.MINIMUM_DEDUCTIBLE,0) = 0      
    ) THEN '5'       
   WHEN VW.DEDUCTIBLE_1_TYPE =14573 --% Of Loss    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '1'      
   WHEN VW.DEDUCTIBLE_1_TYPE =14574 -- % Of SI    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '2'                                            
   WHEN VW.DEDUCTIBLE_1_TYPE =14575 --Flat    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '3'      
   WHEN VW.DEDUCTIBLE_1_TYPE =14576 --# Of Hours    
     AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '5'    
        
  WHEN VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574) --% Of Loss    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) > 0                                 
      or LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) > 0      
    ) THEN '5'      
        
                  
        ELSE   '9' END  AS TIPO_FRANQ        
              
--,RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.DEDUCTIBLE_1),''),'.',''),9)  AS VAL_FRANQ            
--,CASE WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.MINIMUM_DEDUCTIBLE),''),'.',''),9)      
--      ELSE RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.DEDUCTIBLE_1),''),'.',''),9)      
--      END AS VAL_FRANQ  
      ,ISNULL(
      CAST(
			(CASE WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN      
			   --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.MINIMUM_DEDUCTIBLE AS INT),0)),9)     
				 RIGHT(SUBSTRING(CAST(ROUND(VW.MINIMUM_DEDUCTIBLE,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.MINIMUM_DEDUCTIBLE,0,1) AS VARCHAR),1)),9)         
			  ELSE     
			  --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.DEDUCTIBLE_1 AS INT),0)),9)     
			  RIGHT(SUBSTRING(CAST(ROUND(VW.DEDUCTIBLE_1,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.DEDUCTIBLE_1,0,1) AS VARCHAR),1) ),9)          
			  --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.DEDUCTIBLE_1 AS INT),0)),9)           
			  END)
		   AS VARCHAR),'0'
		  ) 
       AS VAL_FRANQ          
          
              
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.LIMIT_1),''),'.',''),11) AS IMP_SEG   
, ISNULL(RIGHT(SUBSTRING(CAST(ROUND(VW.LIMIT_1,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.LIMIT_1,0,1) AS VARCHAR),1) ),11),'') AS IMP_SEG                 
          
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,ABS(ISNULL(VW.WRITTEN_PREMIUM,0))),''),'.',''),11) AS PREMIO 
 --, RIGHT(SUBSTRING(CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1)AS VARCHAR),1) ),9) AS PREMIO                       
 ,CASE WHEN VW.PROCESS_ID = 37 THEN        
  CASE WHEN (SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_4 WITH (NOLOCK)          
      WHERE  PPC_4.CUSTOMER_ID = PPP.CUSTOMER_ID AND PPC_4.POLICY_ID = PPP.POLICY_ID        
      AND PPC_4.POLICY_VERSION_ID =  PPP.NEW_POLICY_VERSION_ID --CAST(dbo.Piece(PPP.LAST_REVERT_BACK,'^',3) AS SMALLINT  ))        
      ) > 0    AND ((SELECT SUM(ABS(ISNULL(APID.INSTALLMENT_AMOUNT,0))) from ACT_POLICY_INSTALLMENT_DETAILS apid WITH(NOLOCK) WHERE apid.CUSTOMER_ID        
            = PCPL.CUSTOMER_ID AND apid.POLICY_ID = PCPL.POLICY_ID AND         
             apid.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND RELEASED_STATUS ='N'         
             AND APID.INSTALLMENT_AMOUNT <0 )        
            ) > 0        
        THEN        
              
                
     (SELECT SUM(ABS(ISNULL(APID.INSTALLMENT_AMOUNT,0))) from ACT_POLICY_INSTALLMENT_DETAILS apid WITH(NOLOCK) WHERE apid.CUSTOMER_ID        
         = PCPL.CUSTOMER_ID AND apid.POLICY_ID = PCPL.POLICY_ID AND         
          apid.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND RELEASED_STATUS ='N'         
          AND APID.INSTALLMENT_AMOUNT <0         
      )/        
       (SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_4 WITH (NOLOCK)          
        WHERE  PPC_4.CUSTOMER_ID = PPP.CUSTOMER_ID AND PPC_4.POLICY_ID = PPP.POLICY_ID        
          AND PPC_4.POLICY_VERSION_ID =  PPP.NEW_POLICY_VERSION_ID --CAST(dbo.Piece(PPP.LAST_REVERT_BACK,'^',3) AS SMALLINT  ))        
       ) * ISNULL(VW.WRITTEN_PREMIUM,0        
     )        
              
     ELSE 0        
    END          
             
             
         
    ELSE ISNULL(VW.WRITTEN_PREMIUM,0)        
    END AS PREMIO
          
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,ABS(ISNULL(VW.COMMISSION,0))),''),'.',''),11) AS CORRETAGEM 
          
,RIGHT(SUBSTRING(CAST(ROUND(VW.COMMISSION,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.COMMISSION,0,1)AS VARCHAR),1) ),7) AS CORRETAGEM           
--,RIGHT('00000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.PERC_DISCOUNT),''),'.',''),5) AS PERC_DESC      
,RIGHT(CONVERT(VARCHAR,ISNULL(VW.PERC_DISCOUNT ,0) ),5) AS PERC_DESC        
  --,VW.PERC_DISCOUNT            
 FROM POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)                                  
 INNER JOIN POL_PRODUCT_LOCATION_INFO PRLI WITH(NOLOCK)                                  
 ON PCPL.CUSTOMER_ID  = PRLI.CUSTOMER_ID                                    
 AND PCPL.POLICY_ID   = PRLI.POLICY_ID                                    
 AND PCPL.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID                                 
INNER JOIN [VW_SUSEP_RCOMP_AND_RRURAL] VW                                    
 ON PRLI.CUSTOMER_ID  = VW.CUSTOMER_ID                                    
 AND PRLI.POLICY_ID   = VW.POLICY_ID                                    
 AND PRLI.POLICY_VERSION_ID = VW.POLICY_VERSION_ID                                    
 AND PRLI.LOCATION   = VW.LOCATION_ID                                    
 --AND PRLI.ITEM_NUMBER  = VW.RISK_ID                        
 AND PRLI.PRODUCT_RISK_ID =  VW.RISK_ID            
INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)             
 ON PCPL.CUSTOMER_ID  = PPP.CUSTOMER_ID                                    
 AND PCPL.POLICY_ID   = PPP.POLICY_ID                                    
 AND PCPL.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID            
 AND PPP.PROCESS_STATUS ='COMPLETE'            
                                    
INNER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK)                                     
 ON MLM.LOB_ID    = VW.POLICY_LOB            
LEFT JOIN MNT_COVERAGE MC WITH(NOLOCK)            
 ON MC.COV_ID = VW.COVERAGE_CODE_ID              
                                    
                                       
   WHERE (@POLICY_NUMBER IS  NULL OR VW.POLICY_NUMBER = @POLICY_NUMBER )                
    --VW.POLICY_NUMBER =    '88998201180114000122'                         
   AND YEAR(VW.COMPLETED_DATETIME) = YEAR(@DATETIME)                                       
   AND MLM.SUSEP_LOB_CODE IN ('0116','0118','0114')                
   AND VW.WRITTEN_PREMIUM = CASE WHEN VW.PROCESS_ID IN (14,25,18) AND VW.WRITTEN_PREMIUM<>0 THEN  VW.WRITTEN_PREMIUM            
         WHEN VW.PROCESS_ID IN (37)  THEN  VW.WRITTEN_PREMIUM            
          END              
 ---------------------------------------------------------------------------------------------------           
 -------------------------------------------PREMIUM REFUN IN POLICY CANCELLED----------------------------------------          
-----------------------------------------------------------------------------------------------------    
 UNION          
          
SELECT                                     
VW.CUSTOMER_ID                                    
,VW.POLICY_ID                                    
,VW.POLICY_VERSION_ID,                                    
--RISK_ID                                    
                                    
-------REPORT FIELDS--------                                    
 VW.SUSEP_INSURER_CODE AS COD_SEG                                    
                                     
, ISNULL(VW.SUSEP_PROCESS_NUMBER,'') AS PROCESSO                                    
--------Type                                    
,ISNULL(CASE WHEN  PRLI.CLASS_FIELD = 14730 THEN '1'                                    
   WHEN  PRLI.CLASS_FIELD = 14731 THEN '1'                                    
   WHEN  PRLI.CLASS_FIELD = 14732 THEN '1'                                    
   WHEN  PRLI.CLASS_FIELD = 14733 THEN '1'                                    
   WHEN  PRLI.CLASS_FIELD = 14734 THEN '1'                                    
   WHEN  PRLI.CLASS_FIELD = 14735 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14736 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14737 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14738 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14739 THEN '2'                 
WHEN  PRLI.CLASS_FIELD = 14740 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14741 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14742 THEN '2'                                    
   WHEN  PRLI.CLASS_FIELD = 14743 THEN '3'                                    
   WHEN  PRLI.CLASS_FIELD = 14744 THEN '3'                                    
   WHEN  PRLI.CLASS_FIELD = 14745 THEN '3'                                    
   WHEN  PRLI.CLASS_FIELD = 14746 THEN '3'                                    
   WHEN  PRLI.CLASS_FIELD = 14747 THEN '3'           
 ELSE NULL                                    
 END,'') AS TIPO --CASE                                    
                                     
                                     
                                    
--CLASS                                    
, ISNULL(CASE  WHEN  PRLI.CLASS_FIELD = 14730 THEN '01'                                    
    WHEN  PRLI.CLASS_FIELD = 14731 THEN '02'                                    
    WHEN  PRLI.CLASS_FIELD = 14732 THEN '03'                                    
    WHEN  PRLI.CLASS_FIELD = 14733 THEN '04'                                    
    WHEN  PRLI.CLASS_FIELD = 14734 THEN '99'                                    
    WHEN  PRLI.CLASS_FIELD = 14735 THEN '01'                                    
    WHEN  PRLI.CLASS_FIELD = 14736 THEN '02'                                    
    WHEN  PRLI.CLASS_FIELD = 14737 THEN '03'                                    
    WHEN  PRLI.CLASS_FIELD = 14738 THEN '04'                                    
    WHEN  PRLI.CLASS_FIELD = 14739 THEN '05'                                    
    WHEN  PRLI.CLASS_FIELD = 14740 THEN '06'                                    
    WHEN  PRLI.CLASS_FIELD = 14741 THEN '07'                                    
    WHEN  PRLI.CLASS_FIELD = 14742 THEN '99'                                    
    WHEN  PRLI.CLASS_FIELD = 14743 THEN '01'                                    
    WHEN  PRLI.CLASS_FIELD = 14744 THEN '02'                                    
    WHEN  PRLI.CLASS_FIELD = 14745 THEN '03'                                    
    WHEN  PRLI.CLASS_FIELD = 14746 THEN '04'                                    
    WHEN  PRLI.CLASS_FIELD = 14747 THEN '05'            
 ELSE NULL                                    
 END,'') AS  CLASSE --CASE                                    
                                    
,RIGHT('00000000000000000000'+ CONVERT(VARCHAR, VW.POLICY_NUMBER),20) AS APOLICE                                
                                    
,RIGHT('000000000'+ CONVERT(VARCHAR, VW.ENDORSEMENT_NO),10) AS ENDOSSO                                
                                    
--,VW.ENDORSEMENT_TYPE AS COD_END --CASE                                    
,CASE  WHEN VW.PROCESS_ID =12 THEN '3' ---CANCELLATION OF POLICY/COVERAGE OR ITEM EXCLUSION NEED TO IMPLEMENT           
    WHEN VW.PROCESS_ID =37  THEN '4'  --VW.ENDORSEMENT_TYPE =14685  --Cancelation Endorsement              
             
   WHEN VW.ENDORSEMENT_TYPE =14684 THEN   --Refund Endorsement          
    CASE WHEN (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_4 WITH(NOLOCK)           
     WHERE PPC_4.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_4.POLICY_ID = PCPL.POLICY_ID          
      AND PPC_4.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  ) <          
   (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)           
     WHERE PPC_3.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_3.POLICY_ID = PCPL.POLICY_ID          
      AND PPC_3.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID-1  )             
              
  THEN '3'            
                   
   ELSE '2' END                                         
   WHEN VW.ENDORSEMENT_TYPE =14682 THEN --Additional Endorsement           
 CASE WHEN (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_4 WITH(NOLOCK)           
    WHERE PPC_4.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_4.POLICY_ID = PCPL.POLICY_ID          
     AND PPC_4.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  ) >          
   (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)           
    WHERE PPC_3.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPC_3.POLICY_ID = PCPL.POLICY_ID          
     AND PPC_3.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID-1  )           
   THEN  '1'           
   ELSE '2'          
  END                                   
                                    
                               
   ELSE '0'                                    
  END AS COD_END                                     
                                   
--,RIGHT('000000'+CONVERT(VARCHAR, isnull(PRLI.ITEM_NUMBER, '')),6)  AS ITEM                       
,RIGHT('000000'+CONVERT(VARCHAR, isnull(PRLI.LOCATION_NUMBER, '')),6)  AS ITEM                                   
                                  
                                  
,MC.SUSEP_COV_CODE AS COBERTURA --CASE          
                                  
,VW.STATE_CODE  AS UF                                 
          
,LEFT(REPLACE(CONVERT(varchar,VW.POLICY_EFFECTIVE_DATE,111),'/',''),8)  AS INICIO_VIG                                
                                 
,LEFT(REPLACE(CONVERT(varchar,VW.POLICY_EXPIRATION_DATE,111),'/',''),8) AS FIM_VIG                                  
                                     
--,CASE WHEN VW.DEDUCTIBLE_1_TYPE =14573 THEN '1'                                    
--   WHEN VW.DEDUCTIBLE_1_TYPE =14574 THEN '2'                                    
--   WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN '3'                  
--   WHEN VW.DEDUCTIBLE_1_TYPE =14576 THEN '5'                                    
--   WHEN ISNULL(VW.DEDUCTIBLE_1_TYPE,'') ='' THEN '9'                            
--   WHEN ISNULL(VW.DEDUCTIBLE_1,0) =0 THEN '9'                                    
--   ELSE '9'                                    
--   END AS TIPO_FRANQ  --CASE                        
                      
,CASE WHEN  VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574)       
     AND  ( LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    )      
     AND  ISNULL(VW.DEDUCTIBLE_1,0) = 0      
     AND ISNULL(VW.MINIMUM_DEDUCTIBLE,0)=0 THEN '9'       
   WHEN VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574) AND      
    (      
   ( LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) > 0                                 
    OR LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) > 0      
   )      
   AND       
   ISNULL(VW.DEDUCTIBLE_1,0) = 0           
   AND ISNULL(VW.MINIMUM_DEDUCTIBLE,0) = 0      
    ) THEN '5'       
   WHEN VW.DEDUCTIBLE_1_TYPE =14573 --% Of Loss    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '1'      
   WHEN VW.DEDUCTIBLE_1_TYPE =14574 -- % Of SI    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '2'                                            
   WHEN VW.DEDUCTIBLE_1_TYPE =14575 --Flat    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '3'      
   WHEN VW.DEDUCTIBLE_1_TYPE =14576 --# Of Hours    
     AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) = 0                                 
      AND LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) = 0      
    ) THEN '5'    
        
  WHEN VW.DEDUCTIBLE_1_TYPE IN (14575,14573,14576,14574) --% Of Loss    
   AND (ISNULL(VW.DEDUCTIBLE_1,0) > 0 OR  ISNULL(VW.MINIMUM_DEDUCTIBLE,0)>0)     
   AND (LEN(ISNULL(VW.DEDUCTIBLE1_AMOUNT_TEXT,'')) > 0                                 
      or LEN(ISNULL(VW.DEDUCTIBLE2_AMOUNT_TEXT,'')) > 0      
    ) THEN '5'      
        
                  
        ELSE   '9' END  AS TIPO_FRANQ                                       
                                    
--,RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.DEDUCTIBLE_1),''),'.',''),9)  AS VAL_FRANQ                                  
--,CASE WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.MINIMUM_DEDUCTIBLE),''),'.',''),9)                            
--      ELSE RIGHT('000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.DEDUCTIBLE_1),''),'.',''),9)                            
--      END AS VAL_FRANQ                        
      ,ISNULL(                
      CAST(                
   (CASE WHEN VW.DEDUCTIBLE_1_TYPE =14575 THEN                      
      --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.MINIMUM_DEDUCTIBLE AS INT),0)),9)                     
     RIGHT(SUBSTRING(CAST(ROUND(VW.MINIMUM_DEDUCTIBLE,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.MINIMUM_DEDUCTIBLE,0,1) AS VARCHAR),1)),9)                         
     ELSE                     
     --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.DEDUCTIBLE_1 AS INT),0)),9)                     
     RIGHT(SUBSTRING(CAST(ROUND(VW.DEDUCTIBLE_1,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.DEDUCTIBLE_1,0,1) AS VARCHAR),1) ),9)                          
     --RIGHT(CONVERT(VARCHAR,ISNULL(CAST(VW.DEDUCTIBLE_1 AS INT),0)),9)                           
     END)                
 AS VARCHAR),'0'                
    )                 
       AS VAL_FRANQ                                  
                                
                                    
 , ISNULL(RIGHT(SUBSTRING(CAST(ROUND(VW.LIMIT_1,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(VW.LIMIT_1,0,1) AS VARCHAR),1) ),11),'') AS IMP_SEG                                 
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.LIMIT_1),''),'.',''),11) AS IMP_SEG                         
--,RIGHT(CONVERT(VARCHAR,ISNULL(CAST(ROUND(VW.LIMIT_1,0,1) AS INT),0)),11) AS IMP_SEG                                   
                                
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,ABS(ISNULL(VW.WRITTEN_PREMIUM,0))),''),'.',''),11) AS PREMIO                       
 --,RIGHT(CONVERT(VARCHAR,ISNULL(CAST(ROUND(VW.WRITTEN_PREMIUM,0) AS INT) ,0)),11) AS PREMIO                                     
 -- /* , CASE WHEN  VW.PROCESS_ID = 12  AND VW.RETURN_PREMIUM > 0 THEN  --0            
 --  --RIGHT(SUBSTRING(CAST(ROUND(ISNULL(VW.RETURN_PREMIUM,0),0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1)AS VARCHAR),1) ),9)             
             
 --RIGHT(SUBSTRING(CAST(ROUND(          
 --  (ISNULL(VW.RETURN_PREMIUM,0)/          
 --   (SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PRC_2 WITH(NOLOCK) WHERE           
 --    PRC_2.CUSTOMER_ID  = VW.CUSTOMER_ID                                    
 --    AND PRC_2.POLICY_ID   = VW.POLICY_ID                                    
 --    AND PRC_2.POLICY_VERSION_ID = VW.POLICY_VERSION_ID))          
               
 --,0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1)AS VARCHAR),1) ),9)                 
            
 -- WHEN  VW.PROCESS_ID = 12  AND VW.RETURN_PREMIUM = 0 THEN             
 --  '0'            
 -- ELSE  ISNULL(VW.WRITTEN_PREMIUM,0)          
 -- --RIGHT(SUBSTRING(CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ISNULL(VW.WRITTEN_PREMIUM,0),0,1)AS VARCHAR),1) ),9)             
                
 --         END AS PREMIO */          
 ,CASE WHEN PPP.RETURN_PREMIUM <>0 THEN        
 --PPP.RETURN_PREMIUM          
 --(SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)         
 --  WHERE PPC_3.CUSTOMER_ID =PRLI.CUSTOMER_ID AND PPC_3.POLICY_ID = PRLI.POLICY_ID        
 --  AND PPC_3.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID-1 )        
         
 CASE WHEN  (SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)         
   WHERE PPC_3.CUSTOMER_ID =PRLI.CUSTOMER_ID AND PPC_3.POLICY_ID = PRLI.POLICY_ID        
   AND PPC_3.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID-1 ) > 0 THEN        
       (        
   ISNULL(PPP.RETURN_PREMIUM,0) /(SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)         
   WHERE PPC_3.CUSTOMER_ID =PRLI.CUSTOMER_ID AND PPC_3.POLICY_ID = PRLI.POLICY_ID        
   AND PPC_3.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID-1 )        
  ) * ISNULL(VW_2.WRITTEN_PREMIUM,0)        
          
    ELSE 0        
           
    END        
            
 --ELSE 0        
 END AS PREMIO          
            
            
                  
--,RIGHT('00000000000'+REPLACE(ISNULL(CONVERT(VARCHAR,ABS(ISNULL(VW.COMMISSION,0))),''),'.',''),11) AS CORRETAGEM                                   
--,RIGHT(SUBSTRING(CAST(ROUND(ISNULL(VW.COMMISSION,0),0,1) AS VARCHAR),0, CHARINDEX('.', CAST(ROUND(ISNULL(VW.COMMISSION,0),0,1)AS VARCHAR),1) ),7) AS CORRETAGEM                     
--,RIGHT(CONVERT(VARCHAR,ISNULL(CAST(ROUND(VW.COMMISSION,0) AS INT),0)),11) AS CORRETAGEM                     
,CASE WHEN PPP.RETURN_PREMIUM <>0 THEN   
 CASE WHEN  (SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)         
  WHERE PPC_3.CUSTOMER_ID =PRLI.CUSTOMER_ID AND PPC_3.POLICY_ID = PRLI.POLICY_ID        
  AND PPC_3.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID-1 ) > 0 THEN        
     (        
      ISNULL(PPP.RETURN_PREMIUM,0)* ISNULL(VW_2.COMMISSION_PERC,0) /(SELECT SUM(ISNULL(WRITTEN_PREMIUM,0)) FROM POL_PRODUCT_COVERAGES PPC_3 WITH(NOLOCK)         
      WHERE PPC_3.CUSTOMER_ID =PRLI.CUSTOMER_ID AND PPC_3.POLICY_ID = PRLI.POLICY_ID        
      AND PPC_3.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID-1 )        
     ) * ISNULL(VW_2.WRITTEN_PREMIUM,0)        
          
    ELSE 0        
           
    END        
            
 --ELSE 0        
 END AS CORRETAGEM                                   
--,RIGHT('00000'+REPLACE(ISNULL(CONVERT(VARCHAR,VW.PERC_DISCOUNT),''),'.',''),5) AS PERC_DESC                            
,RIGHT(CONVERT(VARCHAR,ISNULL(VW.PERC_DISCOUNT ,0) ),5) AS PERC_DESC          
          
--,(SELECT COUNT(*) FROM POL_PRODUCT_COVERAGES PRC_2 WITH(NOLOCK) WHERE           
--    PRC_2.CUSTOMER_ID  = VW.CUSTOMER_ID                                    
--    AND PRC_2.POLICY_ID   = VW.POLICY_ID                                    
--    AND PRC_2.POLICY_VERSION_ID = VW.POLICY_VERSION_ID          
--    --AND PRC_2.RISK_ID = VW.RISK_ID           
-- )   AS TT_COUNT_COV                           
  --,VW.PERC_DISCOUNT                                  
 FROM POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)                                
 INNER JOIN POL_PRODUCT_LOCATION_INFO PRLI WITH(NOLOCK)                                
 ON PCPL.CUSTOMER_ID  = PRLI.CUSTOMER_ID                                  
 AND PCPL.POLICY_ID   = PRLI.POLICY_ID                                  
 AND PCPL.POLICY_VERSION_ID = PRLI.POLICY_VERSION_ID                               
INNER JOIN [VW_SUSEP_RCOMP_AND_RRURAL] VW                                  
 ON PRLI.CUSTOMER_ID  = VW.CUSTOMER_ID                                  
 AND PRLI.POLICY_ID   = VW.POLICY_ID                                  
 AND PRLI.POLICY_VERSION_ID = VW.POLICY_VERSION_ID                                  
 AND PRLI.LOCATION   = VW.LOCATION_ID                                  
 --AND PRLI.ITEM_NUMBER  = VW.RISK_ID                      
 AND PRLI.PRODUCT_RISK_ID =  VW.RISK_ID         
LEFT  JOIN [VW_SUSEP_RCOMP_AND_RRURAL] VW_2                                
 ON VW.CUSTOMER_ID  = VW_2.CUSTOMER_ID                                
 AND VW.POLICY_ID   = VW_2.POLICY_ID                                
 AND VW.POLICY_VERSION_ID-1  = VW_2.POLICY_VERSION_ID       
 AND VW.COVERAGE_CODE_ID = VW_2.COVERAGE_CODE_ID 
 AND VW.RISK_ID = VW_2.RISK_ID                             
 --AND PRLI.LOCATION   = VW.LOCATION_ID                                
 --AND PRLI.ITEM_NUMBER  = VW.RISK_ID                    
 --AND PRLI.PRODUCT_RISK_ID =  VW_2.RISK_ID      
 AND VW.PROCESS_ID  =12            
INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)           
 ON PCPL.CUSTOMER_ID  = PPP.CUSTOMER_ID                                  
 AND PCPL.POLICY_ID   = PPP.POLICY_ID                                  
 AND PCPL.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID          
 AND PPP.PROCESS_STATUS ='COMPLETE'          
                                  
INNER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK)                                   
 ON MLM.LOB_ID    = VW.POLICY_LOB          
LEFT JOIN MNT_COVERAGE MC WITH(NOLOCK)          
 ON MC.COV_ID = VW.COVERAGE_CODE_ID         
                                
                                   
   WHERE (@POLICY_NUMBER IS  NULL OR VW.POLICY_NUMBER = @POLICY_NUMBER )            
    --VW.POLICY_NUMBER =    '88998201180114000122'      
   AND YEAR(VW.COMPLETED_DATETIME) = YEAR(@DATETIME)          
                    
   AND MLM.SUSEP_LOB_CODE IN ('0116','0118','0114')            
  AND PPP.Process_id IN (12)          
   --AND CASE WHEN    vw.Process_id IN (14,25,18) THEN  VW.WRITTEN_PREMIUM  > 0          
   --    WHEN    vw.Process_id IN (37) THEN  VW.WRITTEN_PREMIUM >= 0          
   --    END                     
    --AND VW.WRITTEN_PREMIUM = CASE WHEN VW.PROCESS_ID IN (14,25,18) AND VW.WRITTEN_PREMIUM<>0 THEN  VW.WRITTEN_PREMIUM          
    --     WHEN VW.PROCESS_ID IN (37)  THEN  VW.WRITTEN_PREMIUM          
    --      END           
     -- and vw.POLICY_VERSION_ID =1           
                                    
             
    --ORDER BY VW.CUSTOMER_ID,VW.POLICY_ID,VW.POLICY_VERSION_ID                                           
   ) TABLE_1           
   ORDER BY TABLE_1.CUSTOMER_ID,TABLE_1.POLICY_ID,TABLE_1.POLICY_VERSION_ID             
END   
GO

