IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPDFAuto_Coverage_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPDFAuto_Coverage_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc [dbo].[Proc_GetPDFAuto_Coverage_Details]   
--go 
CREATE  PROCEDURE [dbo].[Proc_GetPDFAuto_Coverage_Details]                    
(                                
 @CUSTOMERID   int,                                
 @POLID        int,                                
 @VERSIONID   int,                                
 @VEHICLEID   int,  
 @RISKTYPE nvarchar(20),                                
 @CALLEDFROM  VARCHAR(20)                                
)                                
AS                                
BEGIN     
-- Creat Temp Table and put excluded driver and "Extended Non-Owned Coverage for Named Individual Required" drivers on it  
CREATE TABLE #PEXCEXTEND_DRIVER                    
(     
 [DRIVER_NAME] NVARCHAR(100),  
 [EXCEXTEND_DRIVER] NVARCHAR(40),  
 [DRIVER_DOB] NVARCHAR(100),  
 [SIGNATURE_OBT] NVARCHAR(20)           
  
)   
  
-- Creat Temp Table and put excluded driver message on it  
CREATE TABLE #PEXCEXTEND_DRIVER_message                    
(     
 [DESCRIPTION_MESSAGE] TEXT,  
 [LIMIT_MESSAGE] TEXT,  
 [DEDUCTIBLE_MESSAGE] TEXT              
)                               
 IF (@CALLEDFROM='APPLICATION')                                
 BEGIN                                
  SELECT DISTINCT MC.rank RANK,                                 
  COV_DES,COV_CODE,               
  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,   
 CASE deductible1_amount_text  
  WHEN 'LIMITED'  
   THEN ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
  ELSE  
   ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
  END  
 DEDUCTIBLE_1 ,                  
  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text                                
  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                               
  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
  ED.PRINT_ORDER                                
  FROM  APP_VEHICLE_COVERAGES CI  with(nolock)                                
  INNER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED ON MC.COV_ID=ED.SELECT_COVERAGE                        
   LEFT OUTER JOIN APP_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                            
    AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                    
    AND DE.APP_VERSION_ID=@VERSIONID AND DE.vehicle_ID=@VEHICLEID                          
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID AND CI.APP_VERSION_ID=@VERSIONID AND CI.vehicle_ID=@VEHICLEID                                
  and MC.IS_ACTIVE='Y' AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'                               
  OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND (SELECT MED.SELECT_COVERAGE FROM MNT_ENDORSMENT_DETAILS MED  with(nolock)  INNER JOIN 
    APP_VEHICLE_ENDORSEMENTS ADE  with(nolock)  ON MED.ENDORSMENT_ID=ADE.ENDORSEMENT_ID                            
    AND ADE.CUSTOMER_ID=@CUSTOMERID AND ADE.APP_ID=@POLID                    
    AND ADE.APP_VERSION_ID=@VERSIONID AND ADE.vehicle_ID=@VEHICLEID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE and MED.IS_ACTIVE='Y') IS NULL)) 
           
------------------------------       
UNION          
          
  SELECT DISTINCT MC.rank RANK,                                 
  ED.DESCRIPTION AS COV_DES,COV_CODE,               
  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,   
 CASE deductible1_amount_text  
  WHEN 'LIMITED'  
   THEN ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
  ELSE  
   ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
 END  
 DEDUCTIBLE_1 ,                  
  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text       
  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                                
  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE          
--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                             
  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
  ED.PRINT_ORDER                                
  FROM APP_VEHICLE_ENDORSEMENTS DE   with(nolock)                                
   INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                        
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
   LEFT OUTER JOIN APP_VEHICLE_COVERAGES CI ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                            
    AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.APP_ID=@POLID                    
    AND CI.APP_VERSION_ID=@VERSIONID AND CI.vehicle_ID=@VEHICLEID                          
   LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID AND DE.APP_VERSION_ID=@VERSIONID AND DE.vehicle_ID=@VEHICLEID                                
  and MC.IS_ACTIVE='Y'                                   
/*  UNION                                
                                  
  SELECT ED.rank RANK,DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE, '' LIMIT_1,'' LIMIT_2,'' DEDUCTIBLE_1,'' DEDUCTIBLE_2                                
  ,'' limit1_amount_text,'' limit2_amount_text,'' deductible1_amount_text,'' deductible2_amount_text,                                
   '' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                
  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER,EA.EDITION_DATE                        
--'('  + left(convert(varchar(15),ED.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),ED.EDITION_DATE,1),2) + ')' EDITION_DATE                                
  ,'' limit_id,'' add_information,'' coverage_type, EA.ATTACH_FILE                                
  FROM APP_VEHICLE_ENDORSEMENTS WE                                
  INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                                 
   LEFT OUTER JOIN APP_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                            
    AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.APP_ID=@POLID                    
    AND DE.APP_VERSION_ID=@VERSIONID                           
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.APP_ID=@POLID AND WE.APP_VERSION_ID=@VERSIONID AND WE.vehicle_ID=@VEHICLEID                                
  and ED.IS_ACTIVE='Y'                                
  */                              
  order by rank               
                
--  SELECT CASE WHEN ISNUMERIC(C1.PREMIUM) = 0 THEN C1.PREMIUM ELSE C1.PREMIUM + '.00' END AS COVERAGE_PREMIUM, C1.COMPONENT_CODE, M.COV_CODE--, C.*               
--  FROM  C               
--  INNER JOIN  P               
--  ON C.CUSTOMER_ID = P.CUSTOMER_ID AND               
--  AND  AND C.VEHICLE_ID = P.VEHICLE_ID               
--  AND P.IS_ACTIVE = 'Y'               
--  LEFT OUTER JOIN CLT_PREMIUM_SPLIT P1               
--  ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND               
--  AND               
--  LEFT OUTER JOIN MNT_COVERAGE M ON M.COV_ID = C.COVERAGE_CODE_ID               
-- LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS C1               
--  ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                             
--   WHERE C.CUSTOMER_ID=@CUSTOMERID AND C.APP_ID=@POLID AND C.APP_VERSION_ID=@VERSIONID AND C.vehicle_ID=@VEHICLEID                                
--          AND P1.RISK_ID = @VEHICLEID AND C1.COMPONENT_CODE IS NOT NULL             
--           
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
  END AS COVERAGE_PREMIUM,     
  CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
  END AS WRITTEN_PREMIUM,    
C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID, M.COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*               
--COMP_REMARKS COLUMN  Added By raghav
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 LEFT OUTER JOIN APP_VEHICLE_COVERAGES  C   with(nolock)      
 ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.APP_ID = P1.APP_ID               
 AND C.APP_VERSION_ID = P1.APP_VERSION_ID            
 LEFT OUTER JOIN APP_VEHICLES  P     with(nolock)          
 ON C.CUSTOMER_ID = P.CUSTOMER_ID AND C.APP_ID = P1.APP_ID               
 AND C.APP_VERSION_ID = P1.APP_VERSION_ID  AND C.VEHICLE_ID = P.VEHICLE_ID               
 AND P.IS_ACTIVE = 'Y'               
 LEFT OUTER JOIN MNT_COVERAGE M ON M.COV_ID = C.COVERAGE_CODE_ID        
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)            
 ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                        
               
   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID AND C.VEHICLE_ID=@VEHICLEID                              
       AND P1.RISK_ID = @VEHICLEID AND P1.RISK_TYPE = @RISKTYPE AND C1.COMPONENT_CODE IS NOT NULL      
      
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
  END  AS COVERAGE_PREMIUM,     
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
  END AS WRITTEN_PREMIUM,    
C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID, '' AS COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*               
--COMP_REMARKS COLUMN Added By raghav 
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)         
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) ON      
   M.COMPONENT_CODE = C1.COMPONENT_CODE      
  WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.APP_ID=@POLID AND P1.APP_VERSION_ID=@VERSIONID       
     AND P1.RISK_ID = @VEHICLEID AND P1.RISK_TYPE = @RISKTYPE AND M.COV_CODE IS NULL --C1.COMPONENT_CODE IN ('SUMTOTAL')      
---------------------------------------------------------------------------------  
 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (START)  
---------------------------------------------------------------------------------  
  
  
-- FETCH DB "EXTENDED NON-OWNED COVERAGE FOR NAMED INDIVIDUAL REQUIRED"  
INSERT INTO #PEXCEXTEND_DRIVER  
SELECT ISNULL(ADDS.DRIVER_FNAME,'') + ' ' + ISNULL(ADDS.DRIVER_MNAME,'') + ' ' + ISNULL(ADDS.DRIVER_LNAME,'') , ADDS.EXT_NON_OWN_COVG_INDIVI , CONVERT(NVARCHAR(100),ADDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),ADDS.FORM_F95)   FROM 
--APP_DRIVER_ASSIGNED_VEHICLE ADAV INNER JOIN   
APP_DRIVER_DETAILS ADDS  with(nolock)  --ON ADAV.DRIVER_ID = ADDS.DRIVER_ID AND ADAV.CUSTOMER_ID = ADDS.CUSTOMER_ID AND ADAV.APP_ID = ADDS.APP_ID AND ADAV.APP_VERSION_ID= ADDS.APP_VERSION_ID  
WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@POLID AND ADDS.APP_VERSION_ID=@VERSIONID AND ADDS.EXT_NON_OWN_COVG_INDIVI = 10963  
  
-- FETCH DB EXCLUDED DRIVER  
INSERT INTO #PEXCEXTEND_DRIVER  
SELECT ISNULL(ADDS.DRIVER_FNAME,'') + ' ' + ISNULL(ADDS.DRIVER_MNAME,'') + ' ' + ISNULL(ADDS.DRIVER_LNAME,''), ADDS.DRIVER_DRIV_TYPE , CONVERT(NVARCHAR(100),ADDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),ADDS.FORM_F95) FROM APP_DRIVER_DETAILS ADDS    with(nolock)  
WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@POLID AND ADDS.APP_VERSION_ID=@VERSIONID AND ADDS.DRIVER_DRIV_TYPE = 3477   
-- SELECT FROM TEMP TABLE  
SELECT * FROM #PEXCEXTEND_DRIVER  
  
---------------------------------------------------------------------------------  
 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (END)  
---------------------------------------------------------------------------------  
      
 END                                
 ELSE IF (@CALLEDFROM='POLICY')                                
 BEGIN                                
  SELECT DISTINCT MC.rank RANK,                                
  COV_DES,COV_CODE,                            
  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                           
  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,     
 CASE deductible1_amount_text  
 WHEN 'LIMITED'  
  THEN   ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')  
 ELSE                        
  ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
 END DEDUCTIBLE_1 ,                  
  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text            
  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE               
  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
  ED.PRINT_ORDER                                
  FROM  POL_VEHICLE_COVERAGES  CI with(nolock)                     
  INNER JOIN MNT_COVERAGE  MC with(nolock) ON MC.COV_ID=CI.COVERAGE_CODE_ID                      
   LEFT OUTER JOIN MNT_ENDORSMENT_DETAILS ED ON MC.COV_ID=ED.SELECT_COVERAGE                        
   LEFT OUTER JOIN POL_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                            
    AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                    
    AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.VEHICLE_ID=@VEHICLEID                          
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
  WHERE CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.VEHICLE_ID=@VEHICLEID                                
  and MC.IS_ACTIVE='Y' AND (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') != 'Y'                                  
  OR (ISNULL(ED.ENDORS_ASSOC_COVERAGE,'N') = 'Y' AND (SELECT MED.SELECT_COVERAGE 
FROM MNT_ENDORSMENT_DETAILS MED  with(nolock)  INNER JOIN POL_VEHICLE_ENDORSEMENTS PDE  with(nolock) ON MED.ENDORSMENT_ID=PDE.ENDORSEMENT_ID                            
    AND PDE.CUSTOMER_ID=@CUSTOMERID AND PDE.POLICY_ID=@POLID                    
    AND PDE.POLICY_VERSION_ID=@VERSIONID AND PDE.vehicle_ID=@VEHICLEID WHERE CI.COVERAGE_CODE_ID = MED.SELECT_COVERAGE and MED.IS_ACTIVE='Y') IS NULL))      
             
UNION          
          
  SELECT DISTINCT MC.rank RANK,                                
  ED.DESCRIPTION AS COV_DES,COV_CODE,                            
  ISNULL(CAST(LIMIT_2*1.00 AS VARCHAR),'') AS LIMIT_2,                   
  ISNULL(CAST(LIMIT_1*1.00 AS VARCHAR),'') AS LIMIT_1,    
 CASE deductible1_amount_text  
  WHEN 'LIMITED'  
   THEN ISNULL(CAST(ISNULL(DEDUCTIBLE_1,0)*1.00 AS VARCHAR),'')   
  ELSE   
   ISNULL(CAST(DEDUCTIBLE_1*1.00 AS VARCHAR),'')   
 END DEDUCTIBLE_1 ,                  
  ISNULL(CAST(DEDUCTIBLE_2*1.00 AS VARCHAR),'') DEDUCTIBLE_2                                
  ,limit1_amount_text,limit2_amount_text,deductible1_amount_text,deductible2_amount_text                                
  ,COMPONENT_CODE, ED.ENDORS_PRINT,ltrim(rtrim(MC.COVERAGE_TYPE)) COVERAGE_TYPE                                
  ,'C' Type,ED.ENDORS_ASSOC_COVERAGE,MC.FORM_NUMBER,EA.EDITION_DATE                        
--'('  + left(convert(varchar(15),MC.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),MC.EDITION_DATE,1),2) + ')' EDITION_DATE                                
  ,isnull(limit_id,0) limit_id,isnull(ci.add_information,'') add_information, EA.ATTACH_FILE,        
  ED.PRINT_ORDER              
  FROM  POL_VEHICLE_ENDORSEMENTS  DE with(nolock)                               
   INNER JOIN MNT_ENDORSMENT_DETAILS  ED with(nolock) ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID                        
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE                            
   LEFT OUTER JOIN POL_VEHICLE_COVERAGES CI ON CI.COVERAGE_CODE_ID=ED.SELECT_COVERAGE                            
    AND CI.CUSTOMER_ID=@CUSTOMERID AND CI.POLICY_ID=@POLID                    
    AND CI.POLICY_VERSION_ID=@VERSIONID AND CI.VEHICLE_ID=@VEHICLEID                          
   LEFT OUTER JOIN MNT_COVERAGE MC ON MC.COV_ID=CI.COVERAGE_CODE_ID                                
  WHERE DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID AND DE.POLICY_VERSION_ID=@VERSIONID AND DE.VEHICLE_ID=@VEHICLEID                                
  and MC.IS_ACTIVE='Y'                                  
      
/*  UNION     
                                  
  SELECT ED.rank RANK,DESCRIPTION COV_DES,ENDORSEMENT_CODE COV_CODE, '' LIMIT_1,'' LIMIT_2,'' DEDUCTIBLE_1,'' DEDUCTIBLE_2                                
  ,'' limit1_amount_text,'' limit2_amount_text,'' deductible1_amount_text,'' deductible2_amount_text,                                
   '' COMPONENT_CODE, ISNULL(ENDORS_PRINT,'N') AS ENDORS_PRINT                                
  ,'E' Type,ENDORS_ASSOC_COVERAGE,ED.FORM_NUMBER,EA.EDITION_DATE                    
--'('  + left(convert(varchar(15),ED.EDITION_DATE,1),2) + '\' + right(convert(varchar(15),ED.EDITION_DATE,1),2) + ')' EDITION_DATE                                
  ,'' limit_id,'' add_information,'' coverage_type, EA.ATTACH_FILE                                
  FROM POL_VEHICLE_ENDORSEMENTS WE                                
  INNER JOIN MNT_ENDORSMENT_DETAILS ED ON ED.ENDORSMENT_ID=WE.ENDORSEMENT_ID                           
   LEFT OUTER JOIN POL_VEHICLE_ENDORSEMENTS DE ON ED.ENDORSMENT_ID=DE.ENDORSEMENT_ID          
    AND DE.CUSTOMER_ID=@CUSTOMERID AND DE.POLICY_ID=@POLID                    
    AND DE.POLICY_VERSION_ID=@VERSIONID                           
   LEFT OUTER JOIN MNT_ENDORSEMENT_ATTACHMENT EA                             
      ON ED.ENDORSMENT_ID=EA.ENDORSEMENT_ID AND EA.ENDORSEMENT_ATTACH_ID=DE.EDITION_DATE         
  WHERE WE.CUSTOMER_ID=@CUSTOMERID AND WE.POLICY_ID=@POLID AND WE.POLICY_VERSION_ID=@VERSIONID AND WE.vehicle_ID=@VEHICLEID                                
  and ED.IS_ACTIVE='Y'            */                    
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
  END AS COVERAGE_PREMIUM,    
 CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
  END AS WRITTEN_PREMIUM,    
 C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID,M.COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*   
 --COMP_REMARKS COLUMN Added By raghav 
 FROM CLT_PREMIUM_SPLIT  P1  with(nolock)        
 LEFT OUTER JOIN POL_VEHICLE_COVERAGES  C   with(nolock)      
 ON C.CUSTOMER_ID = P1.CUSTOMER_ID AND C.POLICY_ID = P1.POLICY_ID               
 AND C.POLICY_VERSION_ID = P1.POLICY_VERSION_ID             
 LEFT OUTER JOIN POL_VEHICLES  P     with(nolock)          
 ON C.CUSTOMER_ID = P.CUSTOMER_ID AND C.POLICY_ID = P.POLICY_ID               
 AND C.POLICY_VERSION_ID = P.POLICY_VERSION_ID AND C.VEHICLE_ID = P.VEHICLE_ID               
 AND P.IS_ACTIVE = 'Y'               
 LEFT OUTER JOIN MNT_COVERAGE M ON M.COV_ID = C.COVERAGE_CODE_ID               
 --Table used 1
 LEFT OUTER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)            
 ON M.COMPONENT_CODE = C1.COMPONENT_CODE AND P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID                        
   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID AND (M.COV_ID IS NULL OR ( M.COV_ID IS NOT NULL AND C.VEHICLE_ID=@VEHICLEID))                                
       AND P1.RISK_ID = @VEHICLEID AND P1.RISK_TYPE = @RISKTYPE AND C1.COMPONENT_CODE IS NOT NULL               
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
  END     
    
 AS COVERAGE_PREMIUM,     
     CASE WHEN P1.PROCESS_TYPE = '14'  
 THEN  
    CASE WHEN C1.WRITTEN_PREM IS NULL OR C1.WRITTEN_PREM=''  THEN '0.00'    
         ELSE convert(nvarchar(100),CONVERT(decimal(18,0),C1.WRITTEN_PREM))+'.00' END  
 ELSE ''  
   END AS WRITTEN_PREMIUM,    
C1.COMPONENT_CODE,C1.COM_EXT_AD,P1.RISK_ID, '' AS COV_CODE,ISNULL(C1.COMP_REMARKS,'') AS COMP_REMARKS,C1.COMPONENT_TYPE--, C.*               
--COMP_REMARKS COLUMN Added By raghav 
FROM CLT_PREMIUM_SPLIT  P1  with(nolock)         
 INNER JOIN CLT_PREMIUM_SPLIT_DETAILS  C1   with(nolock)          
  ON P1.UNIQUE_ID = C1.SPLIT_UNIQUE_ID       
 LEFT OUTER JOIN MNT_COVERAGE M with(nolock) 
ON      
   M.COMPONENT_CODE = C1.COMPONENT_CODE      
   WHERE P1.CUSTOMER_ID=@CUSTOMERID AND P1.POLICY_ID=@POLID AND P1.POLICY_VERSION_ID=@VERSIONID       
     AND P1.RISK_ID = @VEHICLEID AND 
     P1.RISK_TYPE = @RISKTYPE AND M.COV_CODE IS NULL -- C1.COMPONENT_CODE IN ('SUMTOTAL')      
      
  
---------------------------------------------------------------------------------  
 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (START)  
---------------------------------------------------------------------------------  
  
-- FETCH DB "EXTENDED NON-OWNED COVERAGE FOR NAMED INDIVIDUAL REQUIRED"  
INSERT INTO #PEXCEXTEND_DRIVER  
SELECT ISNULL(PDDS.DRIVER_FNAME,'') + ' ' + ISNULL(PDDS.DRIVER_MNAME,'') + ' ' + ISNULL(PDDS.DRIVER_LNAME,'') , PDDS.EXT_NON_OWN_COVG_INDIVI , CONVERT(NVARCHAR(100),PDDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),PDDS.FORM_F95)    FROM 
--POL_DRIVER_ASSIGNED_VEHICLE PDAV   
--INNER JOIN   
POL_DRIVER_DETAILS PDDS   
--ON PDAV.DRIVER_ID = PDDS.DRIVER_ID and PDAV.CUSTOMER_ID = PDDS.CUSTOMER_ID AND PDAV.POLICY_ID=PDDS.POLICY_ID AND PDAV.POLICY_VERSION_ID=PDDS.POLICY_VERSION_ID  
 WHERE PDDS.CUSTOMER_ID=@CUSTOMERID AND PDDS.POLICY_ID=@POLID AND PDDS.POLICY_VERSION_ID=@VERSIONID  and  PDDS.EXT_NON_OWN_COVG_INDIVI=10963  
  
-- FETCH DB EXCLUDED DRIVER  
INSERT INTO #PEXCEXTEND_DRIVER  
SELECT ISNULL(PDDS.DRIVER_FNAME,'') + ' ' + ISNULL(PDDS.DRIVER_MNAME,'') + ' ' + ISNULL(PDDS.DRIVER_LNAME,'') , PDDS.DRIVER_DRIV_TYPE , CONVERT(NVARCHAR(100),PDDS.DRIVER_DOB,101),CONVERT(NVARCHAR(20),PDDS.FORM_F95)   FROM POL_DRIVER_DETAILS PDDS    with(
nolock) 
 WHERE PDDS.CUSTOMER_ID=@CUSTOMERID AND PDDS.POLICY_ID=@POLID AND PDDS.POLICY_VERSION_ID=@VERSIONID AND PDDS.DRIVER_DRIV_TYPE=3477  
  
-- SELECT FROM TEMP TABLE  
SELECT * FROM #PEXCEXTEND_DRIVER  
  
---------------------------------------------------------------------------------  
 --         A-95, a-96  DRIVER EXCLUSION DRIVER DETAILS (END)  
---------------------------------------------------------------------------------  
 END   
  --  TEMP TABLE  
  
DECLARE @GARRAGEZIPSTATE NVARCHAR(100)  
IF (@CALLEDFROM='APPLICATION')  
BEGIN   
 SELECT @GARRAGEZIPSTATE =CONVERT(NVARCHAR(100),STATE)  FROM APP_VEHICLES WITH (NOLOCK)  INNER JOIN MNT_TERRITORY_CODES WITH (NOLOCK)  ON   
 SUBSTRING(LTRIM(RTRIM(GRG_ZIP)),1,5) = ZIP WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@POLID AND APP_VERSION_ID=@VERSIONID AND VEHICLE_ID=@VEHICLEID  
END  
ELSE IF(@CALLEDFROM='POLICY')  
BEGIN   
 SELECT   
 @GARRAGEZIPSTATE =CONVERT(NVARCHAR(100),STATE)  FROM POL_VEHICLES WITH (NOLOCK)  INNER JOIN MNT_TERRITORY_CODES WITH (NOLOCK)  ON   
 SUBSTRING(LTRIM(RTRIM(GRG_ZIP)),1,5) = ZIP WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND POLICY_VERSION_ID=@VERSIONID AND VEHICLE_ID=@VEHICLEID  
END  
IF(@GARRAGEZIPSTATE = '22')  
BEGIN   
INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
SELECT '   WARNING - When a named excluded person operates a vehicle all liability coverag','e is void - no one is insured. Owners',' of the vehicle and others'  
  
INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
SELECT '   legally responsible for the acts of the named excluded person remain fully personall','y liable.',''  
END  
  
ELSE IF(@GARRAGEZIPSTATE = '14')  
BEGIN  
INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
SELECT '   WARNING - The excluded person(s), owner and/or registrant will not be covered fo',' r the insurance afforded by the poli','cy when your covered '  
  
INSERT INTO #PEXCEXTEND_DRIVER_MESSAGE  
SELECT '   auto is being used or operated by or at the direction of the excluded driver.','',''  
END  
-- SELECT FROM TEMP TABLE  
SELECT * FROM #PEXCEXTEND_DRIVER_MESSAGE  
  
DROP TABLE #PEXCEXTEND_DRIVER  
DROP TABLE #PEXCEXTEND_DRIVER_MESSAGE                           
END                                



GO

