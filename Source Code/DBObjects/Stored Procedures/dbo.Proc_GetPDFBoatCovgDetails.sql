IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFBoatCovgDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFBoatCovgDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[Proc_GetPDFBoatCovgDetails]                                      
(                                                
 @CUSTOMERID   int,                                                
 @POLID                int,                                                
 @VERSIONID   int,                                                
 @BOATID   int,                                                
 @CALLEDFROM  VARCHAR(20)                                                
)                                                
AS
BEGIN 
DECLARE @STATE_ID smallINT

 IF (@CALLEDFROM='APPLICATION')                                                
 BEGIN                                                
                                                
  SELECT                                                 
 EA.FORM_NUMBER FORM_NUMBER, EA.EDITION_DATE,                                                
 --CASE WHEN DATEPART(YY,MC.EDITION_DATE)='1900' THEN 'A' ELSE MC.EDITION_DATE END EDITION_DATE,                                                
 COV_DES,COV_CODE,                                                
 --CASE MC.COV_CODE when 'WP865' THEN '' when 'EBSMECE' THEN 'N/A' WHEN 'OP400' THEN 'N/A' ELSE                                         
 --ISNULL(CAST(LIMIT_1 AS VARCHAR),'') AS LIMIT_1,                                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,LIMIT_1),1) AS LIMIT_1, LIMIT1_AMOUNT_TEXT, DEDUCTIBLE1_AMOUNT_TEXT,                              
 ISNULL('$' + CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'') DEDUCTIBLE_1, ED.ENDORS_PRINT                                                
 ,MC.COMPONENT_CODE,'C' Type,ED.ENDORS_ASSOC_COVERAGE, EA.ATTACH_FILE, ED.PRINT_ORDER, MC.RANK AS RANK                                               
 FROM  APP_WATERCRAFT_COVERAGE_INFO CI                                                
 INNER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                
 LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED ON MC.COV_ID=ED.SELECT_COVERAGE                                            
 LEFT OUTER JOIN APP_WATERCRAFT_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
 AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                                      
 AND DE.APP_VERSION_ID=@VERSIONID AND DE.BOAT_ID=@BOATID                                            
 LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
 ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
 WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID AND CI.APP_VERSION_ID=@VERSIONID AND CI.BOAT_ID=@BOATID --and ci.is_active='Y'                                                
 AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'   OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND           
 (SELECT COUNT(MED.SELECT_COVERAGE) FROM MNT_ENDORSMENT_DETAILS MED INNER JOIN APP_WATERCRAFT_ENDORSEMENTS ADE           
 ON MED.ENDORSMENT_ID=ADE.ENDORSEMENT_ID              
 AND ADE.CUSTOMER_ID=@CUSTOMERID AND ADE.APP_ID=@POLID                                
 AND ADE.APP_VERSION_ID=@VERSIONID AND ADE.BOAT_ID=@BOATID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE) =0))                  
                           
  UNION                                                
  SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Outboard Motor 1' COV_DES,'OUTBOARD1' COV_CODE,                                
 --ISNULL(CAST(INSURING_VALUE AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,INSURING_VALUE),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                               
        
 '' DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                   
 FROM APP_WATERCRAFT_ENGINE_INFO                                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND ENGINE_ID=1 AND ASSOCIATED_BOAT=@BOATID and APP_WATERCRAFT_ENGINE_INFO.is_active='Y'                                                
      
  UNION             
                                         
 SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Outboard Motor 2' COV_DES,'OUTBOARD2' COV_CODE,                                
 --ISNULL(CAST(INSURING_VALUE AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,INSURING_VALUE),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                               
 '' DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE ,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                    
 FROM APP_WATERCRAFT_ENGINE_INFO                                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND ENGINE_ID=2 AND ASSOCIATED_BOAT=@BOATID and APP_WATERCRAFT_ENGINE_INFO.is_active='Y'                                                
  UNION                                           
  SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Portable Accessories' COV_DES,'PORTACCESS' COV_CODE,                                
 --ISNULL(CAST(SUM(INSURED_VALUE) AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,SUM(INSURED_VALUE)),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                              
                  
--Ravindra(06-19-2008): Ravindra Added ISNULL to EQUIP_AMOUNT
 CASE WHEN (SELECT COUNT(DISTINCT ISNULL(EQUIP_AMOUNT,0)) FROM APP_WATERCRAFT_EQUIP_DETAILLS                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID and APP_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                                
 ) > 1 THEN 'Various' ELSE                  
 ISNULL('$' + CAST((SELECT DISTINCT isnull(EQUIP_AMOUNT,0) FROM APP_WATERCRAFT_EQUIP_DETAILLS                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID and APP_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                                
 ) AS VARCHAR),'') END DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE ,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                   
 FROM APP_WATERCRAFT_EQUIP_DETAILLS                                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID and APP_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                                
 UNION                                                
 SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Trailer' COV_DES,'TRAILER' COV_CODE,                                
 -- ISNULL(CAST(SUM(INSURED_VALUE) AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,SUM(INSURED_VALUE)),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                               
                            
 ISNULL('$' + CONVERT(VARCHAR,CONVERT(MONEY,SUM(TRAILER_DED)),1),'') AS DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE ,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                    
 FROM APP_WATERCRAFT_TRAILER_INFO                          
   WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND ASSOCIATED_BOAT = @BOATID and APP_WATERCRAFT_TRAILER_INFO.is_active='Y'                        
  UNION                
  SELECT                                                 
 EA.FORM_NUMBER FORM_NUMBER, EA.EDITION_DATE,                   
 ED.DESCRIPTION AS COV_DES,COV_CODE,                                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,LIMIT_1),1) AS LIMIT_1, CI.LIMIT1_AMOUNT_TEXT, CI.DEDUCTIBLE1_AMOUNT_TEXT,                              
 ISNULL('$' + CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'') DEDUCTIBLE_1, ED.ENDORS_PRINT                                                
 ,MC.COMPONENT_CODE,'C' Type,ED.ENDORS_ASSOC_COVERAGE, EA.ATTACH_FILE, ED.PRINT_ORDER, MC.RANK AS RANK                                
 FROM APP_WATERCRAFT_ENDORSEMENTS WE                                                
 INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID AND ED.LOB_ID=4                            
 LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                                             
 ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=WE.EDITION_DATE                                            
 LEFT OUTER JOIN APP_WATERCRAFT_COVERAGE_INFO CI ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                                               
 AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID                                      
 AND CI.APP_VERSION_ID=@VERSIONID AND CI.BOAT_ID=@BOATID                                             
 LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                       
 WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.APP_ID=@POLID AND WE.APP_VERSION_ID=@VERSIONID AND WE.BOAT_ID = @BOATID --and we.is_active='Y'                                                
                                                  
  order by rank                        
                        
 SELECT     
 CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
    WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END     AS COVERAGE_PREMIUM,      
 --convert(nvarchar(100),convert(decimal(18,0),C1.WRITTEN_PREM))+'.00' as WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 C1.COMPONENT_CODE,P1.RISK_ID, M.COV_CODE          
                     
 FROM APP_WATERCRAFT_COVERAGE_INFO C                                 
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1                                 
 ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.APP_ID = P1.APP_ID                                 
 AND C.APP_VERSION_ID = P1.APP_VERSION_ID                                 
 LEFT OUTER JOIN MNT_COVERAGE M ON M.COV_ID = C.COVERAGE_CODE_ID                                 
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1                                 
 ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID 
 AND M.COV_ID=C1.COMP_EXT                                              
 WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.APP_ID=@POLID AND C.APP_VERSION_ID=@VERSIONID AND C.BOAT_ID=@BOATID                                                  
 AND P1.RISK_ID = @BOATID AND C1.COMPONENT_CODE IS NOT NULL                   
                      
 UNION      
      
 SELECT     
 CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
    WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END     AS COVERAGE_PREMIUM,      
 --convert(nvarchar(100),convert(decimal(18,0),C1.WRITTEN_PREM))+'.00' as WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 C1.COMPONENT_CODE,P1.RISK_ID, '' AS COV_CODE              
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M ON      
 M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID       
 AND P1.RISK_ID = @BOATID AND M.COV_CODE IS NULL --C1.COMPONENT_CODE IN ('SUMTOTAL')      
      
-- Schedule equipment      
SELECT     
 CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
    WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END     AS COVERAGE_PREMIUM,      
 -- convert(nvarchar(100),convert(decimal(18,0),C1.WRITTEN_PREM))+'.00' as WRITTEN_PREMIUM,       
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 P1.RISK_TYPE, P1.RISK_ID              
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
 ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M ON      
 M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID  AND P1.RISK_ID = @BOATID     
 AND M.COV_CODE IS NULL and C1.COMPONENT_CODE IN ('SUMTOTAL_S','SUMTOTAL')      

-- Unattached premium  
SELECT  @STATE_ID=STATE_ID FROM APP_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID= @VERSIONID                                             
SELECT     
CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
 WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END       
 AS COVERAGE_PREMIUM,      
 --CONVERT(NVARCHAR(100),CONVERT(DECIMAL(18,0),C1.WRITTEN_PREM))+'.00' AS WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 P1.RISK_TYPE, P1.RISK_ID               
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock) ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M ON M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID AND P1.RISK_ID = @BOATID       
 and C1.COMPONENT_CODE IN ('BOAT_UNATTACH_PREMIUM') AND   M.STATE_ID=@STATE_ID      
 END                                                
 ELSE IF (@CALLEDFROM='POLICY')                                                
 BEGIN
SELECT @STATE_ID=STATE_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID
  SELECT                                                 
 EA.FORM_NUMBER FORM_NUMBER, EA.EDITION_DATE,                            
 -- CASE WHEN DATEPART(YY,MC.EDITION_DATE)='1900' THEN 'A' ELSE MC.EDITION_DATE END EDITION_DATE,                                                
 COV_DES,COV_CODE,                                                
 --  CASE MC.COV_CODE when 'WP865' THEN '' when 'EBIUE' THEN 'N/A' when 'EBSMECE' THEN 'N/A' WHEN 'OP400' THEN 'N/A' WHEN 'EBSMWL' THEN 'N/A' ELSE                                         
 --ISNULL(CAST(LIMIT_1 AS VARCHAR),'') AS LIMIT_1,                                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,LIMIT_1),1) AS LIMIT_1, LIMIT1_AMOUNT_TEXT, DEDUCTIBLE1_AMOUNT_TEXT,                              
 ISNULL('$' + CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'') DEDUCTIBLE_1, ED.ENDORS_PRINT                                                
 ,MC.COMPONENT_CODE,'C' Type,ED.ENDORS_ASSOC_COVERAGE, EA.ATTACH_FILE, ED.PRINT_ORDER, MC.RANK AS RANK                                                 
 FROM  POL_WATERCRAFT_COVERAGE_INFO CI WITH(NOLOCK)                                              
 INNER JOIN MNT_COVERAGE MC WITH(NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                
 LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED WITH(NOLOCK) ON MC.COV_ID=ED.SELECT_COVERAGE                                            
 LEFT OUTER JOIN POL_WATERCRAFT_ENDORSEMENTS DE WITH(NOLOCK) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                                            
 AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                                      
 AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.BOAT_ID=@BOATID                                            
 LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH(NOLOCK)                                
 ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                                            
 WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.BOAT_ID=@BOATID --and ci.is_active='Y'                                                
 AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'   OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND           
 (SELECT COUNT(MED.SELECT_COVERAGE) FROM MNT_ENDORSMENT_DETAILS MED WITH(NOLOCK) INNER JOIN POL_WATERCRAFT_ENDORSEMENTS PDE WITH(NOLOCK) ON           
 MED.ENDORSMENT_ID=PDE.ENDORSEMENT_ID               
 AND PDE.CUSTOMER_ID=@CUSTOMERID AND PDE.POLICY_ID=@POLID                      
 AND PDE.POLICY_VERSION_ID=@VERSIONID AND PDE.BOAT_ID=@BOATID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE) =0))                  
                           
  UNION                                  
  SELECT                      
 '' FORM_NUMBER,'' EDITION_DATE,'Outboard Motor 1' COV_DES,'OUTBOARD1' COV_CODE,                                
 --ISNULL(CAST(INSURING_VALUE AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,INSURING_VALUE),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                              
 '' DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                    
 FROM POL_WATERCRAFT_ENGINE_INFO WITH(NOLOCK)                                       
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND ENGINE_ID=1 AND ASSOCIATED_BOAT=@BOATID and POL_WATERCRAFT_ENGINE_INFO.is_active='Y'                                                
  UNION                                                
  SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Outboard Motor 2' COV_DES,'OUTBOARD2' COV_CODE,      --ISNULL(CAST(INSURING_VALUE AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,INSURING_VALUE),1) AS LIMIT_1, '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                              
                         
 '' DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE ,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                                    
 FROM POL_WATERCRAFT_ENGINE_INFO WITH(NOLOCK)                                                
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND ENGINE_ID=2 AND ASSOCIATED_BOAT=@BOATID and POL_WATERCRAFT_ENGINE_INFO.is_active='Y'                                                
  UNION                                                
  SELECT                                                 
 '' FORM_NUMBER,'' EDITION_DATE,'Portable Accessories' COV_DES,'PORTACCESS' COV_CODE,                                
 --ISNULL(CAST(SUM(INSURED_VALUE) AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,SUM(INSURED_VALUE)),1) AS LIMIT_1,      
 '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,                              
--Ravindra(06-19-2008): Ravindra Added ISNULL to EQUIP_AMOUNT 
CASE       
 WHEN (SELECT COUNT(DISTINCT ISNULL(EQUIP_AMOUNT,0)) FROM POL_WATERCRAFT_EQUIP_DETAILLS WITH(NOLOCK)                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID and POL_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                                
 ) > 1 THEN 'Various' ELSE ISNULL('$' + CAST((SELECT DISTINCT isnull(EQUIP_AMOUNT,0) FROM POL_WATERCRAFT_EQUIP_DETAILLS  WITH(NOLOCK)                  
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID and POL_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                                
 ) AS VARCHAR),'') END DEDUCTIBLE_1, '' ENDORS_PRINT                                                
 ,'' COMPONENT_CODE ,'' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                 
  FROM POL_WATERCRAFT_EQUIP_DETAILLS  WITH(NOLOCK)                                               
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID and POL_WATERCRAFT_EQUIP_DETAILLS.is_active='Y'                                     
  UNION                                                
  SELECT      
 '' FORM_NUMBER,'' EDITION_DATE,'Trailer' COV_DES,'TRAILER' COV_CODE,                                
 --ISNULL(CAST(SUM(INSURED_VALUE) AS VARCHAR),'') LIMIT_1,                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,SUM(INSURED_VALUE)),1) AS LIMIT_1,      
  '' AS LIMIT1_AMOUNT_TEXT, '' DEDUCTIBLE1_AMOUNT_TEXT,              
 '' DEDUCTIBLE_1, '' ENDORS_PRINT ,'' COMPONENT_CODE ,      
 '' Type,'' ENDORS_ASSOC_COVERAGE,'' ATTACH_FILE, NULL AS PRINT_ORDER, 0 AS RANK                                            
 FROM POL_WATERCRAFT_TRAILER_INFO WITH(NOLOCK)                                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND ASSOCIATED_BOAT = @BOATID and POL_WATERCRAFT_TRAILER_INFO.is_active='Y'                                                
  UNION                                                
  SELECT                                                 
 EA.FORM_NUMBER FORM_NUMBER, EA.EDITION_DATE,                                               
 ED.DESCRIPTION AS COV_DES,COV_CODE,                                                
 '$' + CONVERT(VARCHAR,CONVERT(MONEY,LIMIT_1),1) AS LIMIT_1, CI.LIMIT1_AMOUNT_TEXT, CI.DEDUCTIBLE1_AMOUNT_TEXT,                              
 ISNULL('$' + CONVERT(VARCHAR,CONVERT(MONEY,DEDUCTIBLE_1),1),'') DEDUCTIBLE_1, ED.ENDORS_PRINT                                                
 ,MC.COMPONENT_CODE,'C' Type,ED.ENDORS_ASSOC_COVERAGE, EA.ATTACH_FILE, ED.PRINT_ORDER, MC.RANK AS RANK                                                  
 FROM POL_WATERCRAFT_ENDORSEMENTS WE WITH(NOLOCK)                                                
 INNER JOIN MNT_ENDORSMENT_DETAILS ED WITH(NOLOCK) ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID AND ED.LOB_ID=4                            
 LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA WITH(NOLOCK)                                             
 ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=WE.EDITION_DATE                                            
 LEFT OUTER JOIN POL_WATERCRAFT_COVERAGE_INFO CI WITH(NOLOCK) ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                                               
 AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID                                      
 AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.BOAT_ID=@BOATID                                             
 LEFT OUTER JOIN MNT_COVERAGE MC WITH(NOLOCK) ON MC.COV_ID=CI.COVERAGE_CODE_ID                                                       
 WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.POLICY_ID=@POLID AND WE.POLICY_VERSION_ID=@VERSIONID AND WE.BOAT_ID = @BOATID --and we.is_active='Y'                                                
                      
  ORDER BY RANK                             
                        
 SELECT     
 CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
    WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END     AS COVERAGE_PREMIUM,      
 --convert(nvarchar(100),convert(decimal(18,0),C1.WRITTEN_PREM))+'.00' as WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
      
 C1.COMPONENT_CODE,P1.RISK_ID, M.COV_CODE       
 FROM POL_WATERCRAFT_COVERAGE_INFO C WITH(NOLOCK)                                 
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1 WITH(NOLOCK)                                 
 ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.POLICY_ID = P1.POLICY_ID                                 
 AND C.POLICY_VERSION_ID = P1.POLICY_VERSION_ID                                 
 LEFT OUTER JOIN MNT_COVERAGE M WITH(NOLOCK) ON M.COV_ID = C.COVERAGE_CODE_ID    
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1 WITH(NOLOCK)                                 
 ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID 
AND M.COV_ID=C1.COMP_EXT                                         
 WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.POLICY_ID=@POLID AND C.POLICY_VERSION_ID=@VERSIONID AND C.BOAT_ID=@BOATID                                                  
 AND P1.RISK_ID = @BOATID AND C1.COMPONENT_CODE IS NOT NULL                                 
  UNION                                           
 SELECT     
CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
    WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END     AS COVERAGE_PREMIUM,      
-- CONVERT(NVARCHAR(100),CONVERT(DECIMAL(18,0),C1.WRITTEN_PREM))+'.00' AS WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 C1.COMPONENT_CODE,P1.RISK_ID, '' AS COV_CODE      
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock) ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) ON M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID       
 AND P1.RISK_ID = @BOATID AND M.COV_CODE IS NULL -- C1.COMPONENT_CODE IN ('SUMTOTAL')      
      
-- Schdedule Equipment      
SELECT     
CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
 WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0         
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END        
 AS COVERAGE_PREMIUM,      
 --CONVERT(NVARCHAR(100),CONVERT(DECIMAL(18,0),C1.WRITTEN_PREM))+'.00' AS WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 P1.RISK_TYPE, P1.RISK_ID               
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock) ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) ON M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID AND P1.RISK_ID = @BOATID       
 AND M.COV_CODE IS NULL and C1.COMPONENT_CODE IN ('SUMTOTAL_S','SUMTOTAL','BOAT_UNATTACH_PREMIUM','BOAT_UNATTACH_INCLUDE')      
       
-- Unattached premium
SELECT     
CASE        
    WHEN M.SHOW_ACT_PREMIUM='10963'         
      THEN         
      CASE         
       WHEN C1.COMP_ACT_PREMIUM ='0.00' OR LTRIM(RTRIM(C1.COMP_ACT_PREMIUM)) =''        
        THEN C1.PREMIUM        
      ELSE        
        CONVERT(nvarchar(100),convert(DECIMAL(18,0),C1.COMP_ACT_PREMIUM)) + '.00'         
      END        
 WHEN M.SHOW_ACT_PREMIUM IS NULL        
     THEN         
      CASE         
       WHEN ISNUMERIC(C1.PREMIUM) = 0  
        THEN C1.PREMIUM         
       ELSE         
        C1.PREMIUM + '.00'         
      END         
  END        
 AS COVERAGE_PREMIUM,      
 --CONVERT(NVARCHAR(100),CONVERT(DECIMAL(18,0),C1.WRITTEN_PREM))+'.00' AS WRITTEN_PREMIUM,      
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END  AS WRITTEN_PREMIUM,      
 P1.RISK_TYPE, P1.RISK_ID               
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock) ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) ON M.COMPONENT_CODE = C1.COMPONENT_CODE      
 WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID AND P1.RISK_ID = @BOATID       
 and C1.COMPONENT_CODE IN ('BOAT_UNATTACH_PREMIUM') AND M.STATE_ID=@STATE_ID           
       
      
      
 END       
END

GO

