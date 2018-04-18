IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SUSEP_FORM_51_REINSURANCE_OUTWARDS_TRANSACTIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SUSEP_FORM_51_REINSURANCE_OUTWARDS_TRANSACTIONS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[PROC_SUSEP_FORM_51_REINSURANCE_OUTWARDS_TRANSACTIONS] --'08/13/2011','88998201180981000017'          
@DATETIME DATETIME = NULL,          
@POLICY_NUMBER VARCHAR(22)= NULL          
AS          
BEGIN              
         
DECLARE @MONTH INT,        
@DATE DATETIME        
        
        
SET @MONTH= MONTH(DATEADD(MONTH,-1,@DATETIME))             
SET @DATE = DATEADD(MONTH, -1, @DATETIME)        
        
--select MONTH(@DATE)        
SELECT                 
 --REFERENCE_MONTH--          
 CONVERT(VARCHAR,@DATE,103)  AS [mrfMesAno],          
           
 --GROUP_LINE_CODE---              
 SUBSTRING(ISNULL(MLM.SUSEP_LOB_CODE,''),1,2) AS [Grupo de Ramos],          
            
 ----INSUSRANCE_LINE_CODE---               
 SUBSTRING(ISNULL(MLM.SUSEP_LOB_CODE,''),3,4) AS [Ramo de Seguro],          
            
 -----CONTRACT_NO----       
  CASE WHEN PRBD.CONTRACT_NUMBER='FACULTATIVE' THEN PCPL.POLICY_NUMBER  -- PRBD.CONTRACT_NUMBER   --CONTRACT          
    WHEN LEN(ISNULL(PRBD.CONTRACT_NUMBER,''))> 0 AND PRBD.CONTRACT_NUMBER <>'FACULTATIVE' THEN PRBD.CONTRACT_NUMBER      
      ELSE ''      --FACULATIVE          
      END AS  [Contrato],        
           
 --CASE WHEN PRI.CONTRACT_FACULTATIVE=14627 THEN  PRBD.CONTRACT_NUMBER   --CONTRACT          
 --     WHEN PRI.CONTRACT_FACULTATIVE=14628 THEN PCPL.POLICY_NUMBER      --FACULATIVE          
 --     END AS   CONTRACT_NO,          
 ------RE-INSURER NAME--------          
 MRCL.REIN_COMAPANY_NAME AS  [Resseguradora],          
           
 ---TYPE OF CONTRACT---          
 --MRC.CONTRACT_DESC AS TYPE_OF_CONTRACT,      
       
 CASE WHEN PRBD.CONTRACT_NUMBER='FACULTATIVE' THEN 'FACULTATIVO'   --CONTRACT          
   WHEN LEN(ISNULL(PRBD.CONTRACT_NUMBER,''))> 0 AND PRBD.CONTRACT_NUMBER <>'FACULTATIVE' THEN MRC.CONTRACT_DESC      
      ELSE ''      --FACULATIVE          
      END   AS [Modalidade],      
         
 --CASE WHEN PRI.CONTRACT_FACULTATIVE=14627 THEN  MRC.CONTRACT_DESC  --CONTRACT          
 -- WHEN PRI.CONTRACT_FACULTATIVE=14628 THEN  'FACULTATIVO'    --FACULATIVE          
 -- END AS TYPE_OF_CONTRACT,         
           
 ----REINSURANCE_PREMIUM------          
 --dbo.fun_FormatCurrency(CAST(SUM(ISNULL(PRBD.TOTAL_INS_VALUE,0)*ISNULL(PRBD.RETENTION_PER,0)/100 ) AS  DECIMAL(13,2)),2) AS REINSURANCE_PREMIUM,                
-- dbo.fun_FormatCurrency(  
 CAST(SUM(ISNULL(PRBD.REIN_PREMIUM,0)) AS  DECIMAL(13,2))  
 --,2)   
 AS [Cessões],          
           
 ------REINSURER RECOVERABLE CLAIM ----          
 --'0,00' AS REINSURER_RECOVERABLE_CLAIM,          
           
 --dbo.fun_FormatCurrency(  
 ISNULL((SELECT  TEMP.RESERVE_AMT          
        FROM (          
        SELECT  SUM(ISNULL(RESERVE_AMT,0.00)) AS RESERVE_AMT, COMP_ID--,CLAIM_ID          
        FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB2 WITH(NOLOCK)          
        WHERE   COMP_TYPE ='RI'  AND ACTIVITY_ID =(SELECT MAX(ACTIVITY_ID) FROM           
             CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB3 WITH(NOLOCK)           
          INNER JOIN CLM_CLAIM_INFO CCI WITH(NOLOCK)          
         ON     CCI.CLAIM_ID = CACRB3.CLAIM_ID                     
         WHERE  CACRB3.COMP_ID =MRCL.REIN_COMAPANY_ID  
   AND  CCI.CUSTOMER_ID = PCPL.CUSTOMER_ID                      
             AND CCI.POLICY_ID = PCPL.POLICY_ID                      
             AND CCI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID           
    AND CACRB3.COMP_ID=CACRB2.COMP_ID          
             AND  CACRB3.CLAIM_ID = CACRB2.CLAIM_ID          
             AND CACRB3.ACTIVITY_ID = CACRB2.ACTIVITY_ID          
             AND CACRB3.RESERVE_ID = CACRB2.RESERVE_ID          
             AND CACRB3.COMP_TYPE = CACRB2.COMP_TYPE          
            AND CACRB3.COMP_TYPE ='RI'          
                              
        GROUP BY CACRB3.COMP_ID,CACRB3.CLAIM_ID           
            )          
         GROUP BY COMP_ID--,CLAIM_ID   
         ) AS TEMP           
         WHERE TEMP.COMP_ID = MRCL.REIN_COMAPANY_ID),0.00)  
         --,2)  
          AS [Recuperações] ,  
            
     
           
 -------REISURANCE_COMMISSION---------          
-- dbo.fun_FormatCurrency(CAST(SUM(ISNULL(MRC.COMMISSION,0)) AS  DECIMAL(13,2)),2) AS [Comissões de resseguro],          
           
           
           
 --dbo.fun_FormatCurrency(  
 CAST(SUM(ISNULL(PRBD.COMM_AMOUNT,0)) AS  DECIMAL(13,2))  
 --,2)  
  AS [Comissões de resseguro],          
           
 -------BROKERAGE_COMMISSION----------          
 --dbo.fun_FormatCurrency(  
 CAST(0 AS DECIMAL(13,2))  
 --,2)   
  AS [Comissões de corretagem],          
           
 --------PROFIT SHARING AMOUNT-------          
 --dbo.fun_FormatCurrency(  
 CAST(0 AS DECIMAL(13,2))  
 --,2)   
AS [Participações no lucro],          
           
 --OTHER AMOUNT---          
 --dbo.fun_FormatCurrency(  
 CAST(0 AS DECIMAL(13,2))  
 --,2)  
  AS [Outros]           
           
   INTO #TEMP        
         
        
 FROM POL_CUSTOMER_POLICY_LIST PCPL              
INNER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK)              
   ON PCPL.POLICY_LOB = MLM.LOB_ID              
INNER JOIN POL_REINSURANCE_BREAKDOWN_DETAILS PRBD WITH(NOLOCK)              
   ON PRBD.CUSTOMER_ID = PCPL.CUSTOMER_ID              
   AND PRBD.POLICY_ID = PCPL.POLICY_ID              
   AND PRBD.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID         
INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)              
  ON PPP.PROCESS_ID = PRBD.PROCESS_ID              
  AND PPP.CUSTOMER_ID = PRBD.CUSTOMER_ID              
  AND PPP.POLICY_ID = PRBD.POLICY_ID              
  AND PPP.NEW_POLICY_VERSION_ID = PRBD.POLICY_VERSION_ID         
LEFT JOIN MNT_REINSURANCE_CONTRACT MRC WITH(NOLOCK)          
   ON  MRC.CONTRACT_NUMBER = PRBD.CONTRACT_NUMBER         
INNER JOIN POL_REINSURANCE_INFO PRI WITH(NOLOCK)        
 ON PRI.CUSTOMER_ID = PCPL.CUSTOMER_ID              
   AND PRI.POLICY_ID = PCPL.POLICY_ID              
   AND PRI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID           
INNER JOIN MNT_REIN_COMAPANY_LIST MRCL WITH(NOLOCK)        
   ON MRCL.REIN_COMAPANY_ID = PRI.COMPANY_ID        
         
 WHERE        
 MONTH(PPP.COMPLETED_DATETIME) = MONTH(@DATE) AND        
 YEAR(PPP.COMPLETED_DATETIME) = YEAR(@DATE) AND        
     (@POLICY_NUMBER IS NULL OR PCPL.POLICY_NUMBER = @POLICY_NUMBER)         
          
 GROUP BY  MLM.SUSEP_LOB_CODE,    
 --PRI.CONTRACT_FACULTATIVE,    
 PRBD.CONTRACT_NUMBER,    
 PCPL.POLICY_NUMBER,    
 --CASE when (PRI.CONTRACT_FACULTATIVE=14627) THEN PRBD.CONTRACT_NUMBER        
 --WHEN (PRI.CONTRACT_FACULTATIVE=14628) THEN PCPL.POLICY_NUMBER END,        
 MRCL.REIN_COMAPANY_NAME,        
 MRCL.REIN_COMAPANY_ID,        
 MRC.CONTRACT_DESC,        
 PCPL.CUSTOMER_ID,        
 PCPL.POLICY_ID,        
 PCPL.POLICY_VERSION_ID,    
 PRBD.CONTRACT_NUMBER       
 --PRI.CONTRACT_FACULTATIVE        
  ORDER by [Grupo de Ramos],[Ramo de Seguro],[mrfMesAno]   
    
  SELECT [mrfMesAno],[Grupo de Ramos],[Ramo de Seguro],
  ISNULL([Contrato],'') AS [Contrato] ,  
 [Resseguradora],  
  [Modalidade],  
  dbo.fun_FormatCurrency(SUM([Cessões]),2) AS [Cessões],  
  dbo.fun_FormatCurrency(SUM(Recuperações),2)   AS [Recuperações]
  ,dbo.fun_FormatCurrency(SUM([Comissões de resseguro]),2)   AS [Comissões de resseguro]
  ,dbo.fun_FormatCurrency(SUM([Comissões de corretagem]),2) AS [Comissões de corretagem],  
  dbo.fun_FormatCurrency(SUM([Participações no lucro]),2) AS [Participações no lucro],  
  dbo.fun_FormatCurrency(SUM([Outros]),2) AS [Outros]  
   FROM  
    
#TEMP  
 GROUP BY [mrfMesAno], [Grupo de Ramos],[Ramo de Seguro],[Resseguradora],[Contrato],[Modalidade]  
 ,[Comissões de corretagem]   
   
 DROP TABLE #TEMP      
               
END     
GO

