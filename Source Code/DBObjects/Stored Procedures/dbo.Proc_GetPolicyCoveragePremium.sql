IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyCoveragePremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyCoveragePremium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
 /*                                                                                
 ----------------------------------------------------------                                                                                    
 Proc Name       : dbo.Proc_GetPolicyCoveragePremium                                                                               
 Created by      : Lalit Kumar Chauhan                                                                                 
 Date            : April-30-2010                  
 Purpose         : Selects records from POL_PRODUCT_COVERAGES  For Policy                
 Revison History :                                                                                    
 Used In         : EbixAdvantage         
 Modified by     : Praveen Kumar[Add language id]           
 Modification Date: 16/08/2010         
 ------------------------------------------------------------                                                                                    
 Date     Review By          Comments                                                                                    
 ------   ------------       -------------------------                      
 LOBID   DESC              
 1   Homeowners              
 2  Automobile              
 3  Motorcycle              
 4  Watercraft              
 5  Umbrella              
 6  Rental              
 7  General Liability              
 8  Aviation              
 9  All Risks and Named Perils  Done         
 10  Comprehensive Condominium  Done        
 11  Comprehensive Company   Done        
 12  General Civil Liability  Done        
 13  Maritime      Done        
 14  Diversified Risks    Done           
 15  Individual Personal Accident   Done            
 16  Robbery      Done        
 17  Facultative Liability   Done        
 18  Civil Liability Transportation Done             
 19  Dwelling                       Done        
 20  National Cargo Transport  Done        
 21  Group Passenger Personal Accident  Done             
 22  Passenger Personal Accident     Done        
 23  International Cargo Transport   Done             
  sele        
---drop proc  [Proc_GetPolicyCoveragePremium]  2727,185,1,19,2        
          
 */           
 --select * from MNT_COVERAGE_MULTILINGUAL WHERE COV_ID=10057                                                                             
CREATE PROC [dbo].[Proc_GetPolicyCoveragePremium]        
 (               
 @CUSTOMER_ID INT,              
 @POLICY_ID INT,            
 @POLICY_VERSION_ID SMALLINT,              
 @LOBID INT ,        
 @LANG_ID INT=NULL                    
 )                
 AS                
 BEGIN           
                
     IF (@LOBID = 2 OR @LOBID = 3) --for Automobile and Motorcycle        
     BEGIN             
             
        SELECT           
      POL_COV.VEHICLE_ID as RISK_ID,      
      '' as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      MNT_COV.COMPONENT_CODE as COMPONENT_CODE,           
      (ISNULL(POL_RISKINFO.MAKE,'')+ISNULL('-'+POL_RISKINFO.MODEL,'')          
      +ISNULL('-'+POL_RISKINFO.VEHICLE_YEAR,'')        
      +ISNULL('-'+POL_RISKINFO.GRG_ADD1,'')              
      +ISNULL('-'+POL_RISKINFO.GRG_CITY,'')+ISNULL('-'+POL_RISKINFO.GRG_ZIP,''))  AS LOCATION,          
      ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      '' MINIMUM_DEDUCTIBLE ,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1 ,      
     'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC         
      FROM POL_VEHICLE_COVERAGES POL_COV WITH(NOLOCK)            
    LEFT OUTER JOIN              
      POL_VEHICLES POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.VEHICLE_ID=POL_RISKINFO.VEHICLE_ID  AND      
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
    LEFT OUTER JOIN       
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID         
    LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
      MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
      MNT_COV_MULTI.LANG_ID = @LANG_ID        
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
     ORDER BY RISK_ID ASC          
     END        
     ELSE IF (@LOBID = 4)     --Watercraft        
     BEGIN           
             
      SELECT           
      POL_COV.BOAT_ID as RISK_ID,         
      '' as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      MNT_COV.COMPONENT_CODE as COMPONENT_CODE,           
      (ISNULL(CAST(POL_RISKINFO.BOAT_NO AS NVARCHAR(50)),'')+ISNULL('-'+POL_RISKINFO.MAKE,'')          
      +ISNULL('-'+POL_RISKINFO.MODEL,'')        
      +ISNULL('-'+POL_RISKINFO.LOCATION_CITY,'')              
      +ISNULL('-'+POL_RISKINFO.LOCATION_STATE,'')+ISNULL('-'+POL_RISKINFO.LOCATION_ZIP,''))  AS LOCATION,          
      ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      '' MINIMUM_DEDUCTIBLE ,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1 ,       
      'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC       
      FROM POL_WATERCRAFT_COVERAGE_INFO POL_COV WITH(NOLOCK)            
    LEFT OUTER JOIN              
      POL_WATERCRAFT_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.BOAT_ID=POL_RISKINFO.BOAT_ID    AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
            
    LEFT OUTER JOIN                    
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID         
    LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
      MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND         
      MNT_COV_MULTI.LANG_ID = @LANG_ID        
           
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
     ORDER BY RISK_ID ASC         
             
     END        
  ELSE IF (@LOBID = 1 OR @LOBID = 6)  --  for Homeowners and Rental        
     BEGIN           
             
      SELECT           
      POL_COV.DWELLING_ID as RISK_ID ,       
      '' as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      MNT_COV.COMPONENT_CODE as COMPONENT_CODE,           
      (ISNULL(CAST(POL_RISKINFO.DWELLING_NUMBER AS NVARCHAR(50)),'')+ISNULL('-'+CAST(POL_RISKINFO.BUILDING_TYPE AS NVARCHAR(50)),'')          
      +ISNULL('-'+POL_RISKINFO.COMMENTDWELLINGOWNED,'')        
      +ISNULL('-'+POL_RISKINFO.LOCATION_CITY,'')              
      +ISNULL('-'+POL_RISKINFO.LOCATION_STATE,'')+ISNULL('-'+POL_RISKINFO.LOCATION_ZIP,''))  AS LOCATION,          
      ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      '' MINIMUM_DEDUCTIBLE ,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1,        
      'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC      
     FROM POL_DWELLING_SECTION_COVERAGES POL_COV WITH(NOLOCK)            
    LEFT OUTER JOIN              
      POL_DWELLINGS_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.DWELLING_ID=POL_RISKINFO.DWELLING_ID   AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'       
            
    LEFT OUTER JOIN                    
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID         
    LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON           
       MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
       MNT_COV_MULTI.LANG_ID = @LANG_ID       
            
        
             
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
     ORDER BY RISK_ID ASC         
             
     END        
       ELSE IF (@LOBID = 8)     --for Aviation        
     BEGIN           
             
      SELECT           
      POL_COV.VEHICLE_ID as RISK_ID,       
      '' as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      MNT_COV.COMPONENT_CODE as COMPONENT_CODE,           
      (ISNULL('-'+CAST(POL_RISKINFO.INSURED_VEH_NUMBER AS NVARCHAR(10)),'')+ISNULL(POL_RISKINFO.REG_NUMBER,'')+ISNULL('-'+POL_RISKINFO.MODEL,'')          
      +ISNULL('-'+POL_RISKINFO.MAKE,'')        
      +ISNULL('-'+POL_RISKINFO.CREW,'')              
      +ISNULL('-'+POL_RISKINFO.ENGINE_TYPE,''))  AS LOCATION,          
      ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      '' MINIMUM_DEDUCTIBLE ,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
     'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC      
     FROM POL_AVIATION_VEHICLE_COVERAGES POL_COV WITH(NOLOCK)            
    LEFT OUTER JOIN              
      POL_AVIATION_VEHICLES POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.VEHICLE_ID=POL_RISKINFO.VEHICLE_ID  AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
    LEFT OUTER JOIN                    
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID         
    LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
         MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
         MNT_COV_MULTI.LANG_ID = @LANG_ID        
            
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID       
               
     ORDER BY RISK_ID ASC         
             
     END        
             
            
 ELSE IF (@LOBID in (9,26))  ---All Risks and Named Perils          
   BEGIN          
    SELECT           
      POL_COV.RISK_ID as RISK_ID,       
      POL_RISKINFO.LOCATION as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      MNT_COV.COMPONENT_CODE as COMPONENT_CODE,            
      (ISNULL(POL_LOCA.NAME,'')+ISNULL('-'+POL_LOCA.LOC_ADD1,'')          
      +ISNULL('-'+Convert(varchar(20), POL_LOCA.LOC_NUM),'')    
      +ISNULL('-'+LOC_ADD2,'')        
      +ISNULL('-'+DISTRICT,'')          
      +ISNULL('-'+POL_LOCA.LOC_CITY,'')+ISNULL('-'+POL_LOCA.LOC_ZIP,''))  AS LOCATION,          
      ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE ,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
      ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,      
        '' VALUE_AT_RISK,      
        MNT_ACT_MST.ACTIVITY_DESC AS ACTIVITY_DESC,      
        0 AS  MAXIMUM_LIMIT,      
        MNT_COV.IS_MAIN  AS IS_MAIN      
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)            
    LEFT OUTER JOIN              
       POL_PERILS POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.PERIL_ID   AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
    LEFT OUTER JOIN                    
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID          
    LEFT OUTER JOIN           
     POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON          
      POL_RISKINFO.LOCATION = POL_LOCA.LOCATION_ID  AND        
      POL_RISKINFO.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND        
      POL_RISKINFO.POLICY_ID = POL_LOCA.POLICY_ID AND        
      POL_RISKINFO.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID        
    LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
       MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
       MNT_COV_MULTI.LANG_ID = @LANG_ID        
             
       LEFT OUTER JOIN       
      MNT_ACTIVITY_MASTER MNT_ACT_MST WITH(NOLOCK) ON      
     MNT_ACT_MST.ACTIVITY_ID= POL_RISKINFO.ACTIVITY_TYPE      
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
     ORDER BY RISK_ID ASC          
   END  --end named Perils        
           
   ELSE IF (@LOBID IN (10,11,12,14,16,19,25,27,32))  --For Comprehensive Condominium ,Comprehensive Company ,General Civil Liability  ,Diversified Risks ,Robbery           
  BEGIN          
    SELECT           
     POL_COV.RISK_ID as RISK_ID,        
     POL_RISKINFO.LOCATION as LOCATION_ID,       
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
     (ISNULL(POL_LOCA.NAME,'')+'-'+ISNULL(POL_LOCA.LOC_ADD1,'')          
     +ISNULL('-'+Convert(varchar(20), POL_LOCA.LOC_NUM),'')          
     +ISNULL('-'+LOC_ADD2,'')        
     +ISNULL('-'+DISTRICT,'')          
     +ISNULL('-'+POL_LOCA.LOC_CITY,'')+ISNULL('-'+POL_LOCA.LOC_ZIP,''))  AS LOCATION,         
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1    ,      
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,      
     ISNULL(POL_RISKINFO.VALUE_AT_RISK,0) as VALUE_AT_RISK   ,      
     MNT_ACT_MST.ACTIVITY_DESC AS ACTIVITY_DESC ,      
     ISNULL(POL_RISKINFO.MAXIMUM_LIMIT,0) AS  MAXIMUM_LIMIT,      
     MNT_COV.IS_MAIN AS IS_MAIN      
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)            
      
      LEFT OUTER JOIN             
       POL_PRODUCT_LOCATION_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.PRODUCT_RISK_ID  AND      
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
   LEFT OUTER JOIN                    
     MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID          
   LEFT OUTER JOIN              
    POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON          
      POL_RISKINFO.LOCATION = POL_LOCA.LOCATION_ID  AND        
      POL_RISKINFO.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND        
      POL_RISKINFO.POLICY_ID = POL_LOCA.POLICY_ID AND        
      POL_RISKINFO.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID        
   LEFT OUTER JOIN                      
    MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
        MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
        MNT_COV_MULTI.LANG_ID = @LANG_ID       
              
    LEFT OUTER JOIN MNT_ACTIVITY_MASTER MNT_ACT_MST WITH(NOLOCK) ON      
     MNT_ACT_MST.ACTIVITY_ID= POL_RISKINFO.ACTIVITY_TYPE      
   WHERE               
    POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
    ORDER BY RISK_ID ASC          
   END            
           
   ELSE IF (@LOBID=13)   ---For maritime        
   BEGIN          
    SELECT           
      POL_COV.RISK_ID as RISK_ID,        
      0 as LOCATION_ID,          
      POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
      POL_RISKINFO.NAME_OF_VESSEL AS LOCATION ,       
       ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
      MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
      ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
      ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
      ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1   ,      
       ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,      
        0 as VALUE_AT_RISK     ,      
        'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC,      
        0 AS  MAXIMUM_LIMIT,      
        MNT_COV.IS_MAIN AS IS_MAIN      
      FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)         
     LEFT OUTER JOIN              
       POL_MARITIME POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.MARITIME_ID  AND      
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'      
     LEFT OUTER JOIN                    
      MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID        
     LEFT OUTER JOIN                      
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
        MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
        MNT_COV_MULTI.LANG_ID = @LANG_ID        
    WHERE               
     POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
     ORDER BY RISK_ID ASC          
   END  --end meritime        
            
   ELSE IF (@LOBID=20 or @LOBID=23)  --For National & international Carogo transport        
           BEGIN         
    SELECT        
     POL_COV.RISK_ID as RISK_ID,      
     0 as LOCATION_ID,          
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
     POL_RISKINFO.COMMODITY AS LOCATION ,          
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,      
       '' VALUE_AT_RISK,      
        'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC   ,      
       0 AS  MAXIMUM_LIMIT  ,      
       MNT_COV.IS_MAIN AS IS_MAIN      
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)        
    LEFT OUTER JOIN              
     POL_COMMODITY_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.COMMODITY_ID   AND      
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'                
    LEFT OUTER JOIN                    
     MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID        
    LEFT OUTER JOIN                      
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
         MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
         MNT_COV_MULTI.LANG_ID = @LANG_ID        
     WHERE               
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
      ORDER BY RISK_ID ASC          
           END     --END National & international cargo transport        
                   
   ELSE IF (@LOBID in (15,21,33,34))  --For Individual Personal Accident info        
           BEGIN         
    SELECT        
     POL_COV.RISK_ID as RISK_ID,         
     0 as LOCATION_ID,          
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
     POL_RISKINFO.INDIVIDUAL_NAME AS LOCATION ,          
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
      ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,      
       '' VALUE_AT_RISK,      
        'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC ,      
        0 AS  MAXIMUM_LIMIT ,      
        MNT_COV.IS_MAIN AS IS_MAIN        
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)        
    LEFT OUTER JOIN              
       POL_PERSONAL_ACCIDENT_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.PERSONAL_INFO_ID     AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'              
    LEFT OUTER JOIN                    
     MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID        
    LEFT OUTER JOIN                      
     MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
         MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
         MNT_COV_MULTI.LANG_ID = @LANG_ID        
     WHERE               
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
      ORDER BY RISK_ID ASC          
           END     --END National & international cargo transport        
                   
 ELSE IF (@LOBID in (17,18,28,29,31,30,36))  --For Facultative Liability AND Civil Liability Transportation        
      --Dpvat(Cat. 3 e 4)/DPVAT(Cat.1,2,9 e 10)  
           BEGIN         
    SELECT    
     POL_COV.RISK_ID as RISK_ID,       
     0 as LOCATION_ID,          
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,               
        (ISNULL(POL_RISKINFO.MAKE_MODEL,'')+'-'        
     +ISNULL(CAST(POL_RISKINFO.VEHICLE_NUMBER AS NVARCHAR(50)),''))  AS LOCATION ,            
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,         
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
      ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,      
      '' VALUE_AT_RISK,      
      'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC ,      
       0 AS  MAXIMUM_LIMIT  ,      
       MNT_COV.IS_MAIN AS IS_MAIN      
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)        
    LEFT OUTER JOIN              
       POL_CIVIL_TRANSPORT_VEHICLES  POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.VEHICLE_ID      AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'             
    LEFT OUTER JOIN                    
     MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID        
    LEFT OUTER JOIN                      
     MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
   MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
         MNT_COV_MULTI.LANG_ID = @LANG_ID        
     WHERE               
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
      ORDER BY RISK_ID ASC          
           END     --END Facultative Liability and Civil Liability Transportation                               
 ELSE IF (@LOBID=22 )  --For Personal Accident for Passengers        
           BEGIN         
    SELECT   
     POL_COV.RISK_ID as RISK_ID,        
     0 as LOCATION_ID,          
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,          
     (CONVERT(varchar(50),ISNULL(POL_RISKINFO.START_DATE,''),CASE WHEN @LANG_ID = 2 THEN 103 ELSE 101 END)+' '+ CASE WHEN @LANG_ID = 2 THEN 'à' ELSE 'To' END +' '+ CONVERT(varchar(50),ISNULL(POL_RISKINFO.END_DATE,''),CASE WHEN @LANG_ID = 2 THEN 103 ELSE 101 END)+CASE WHEN @LANG_ID = 2 THEN ', Número de passageiros 'ELSE ' # of Passengers' END +'-'+ CAST(ISNULL(POL_RISKINFO.NUMBER_OF_PASSENGERS,0) AS NVARCHAR(50)))  AS LOCATION ,                   
 
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,        
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,            
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,              
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,              
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,        
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,      
      ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,      
        '' VALUE_AT_RISK,      
        'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC  ,      
       0 AS  MAXIMUM_LIMIT,      
       MNT_COV.IS_MAIN AS IS_MAIN      
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)        
    LEFT OUTER JOIN              
       POL_PASSENGERS_PERSONAL_ACCIDENT_INFO POL_RISKINFO WITH(NOLOCK) ON                
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND           
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND          
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND           
       POL_COV.RISK_ID=POL_RISKINFO.PERSONAL_ACCIDENT_ID    AND       
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'               
      LEFT OUTER JOIN                    
       MNT_COVERAGE MNT_COV WITH(NOLOCK) ON              
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID        
      LEFT OUTER JOIN                      
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                
         MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND        
         MNT_COV_MULTI.LANG_ID = @LANG_ID        
     WHERE               
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID           
      ORDER BY RISK_ID ASC          
           END     --END Personal Accident for Passengers                   
 ELSE IF (@LOBID in (35,37) )  --For Rural Lien     and rental surity(New Product) itrack #1140
           BEGIN       
    SELECT    
     POL_COV.RISK_ID as RISK_ID,      
     0 as LOCATION_ID,        
     POL_COV.COVERAGE_CODE_ID as COVERAGE_CODE_ID,        
     (CONVERT(varchar(50),ISNULL(POL_RISKINFO.ITEM_NUMBER,''),101)  
     + CONVERT(varchar(50),ISNULL('-'+ ISNULL(LOKUP_MULTI.LOOKUP_VALUE_DESC,LOKUP.LOOKUP_VALUE_DESC),''),101)  
     + CAST(ISNULL('-'+ISNULL(LOKUP_MULTI.LOOKUP_VALUE_DESC,LOKUP.LOOKUP_VALUE_DESC),0) AS NVARCHAR(50)))   
      AS LOCATION ,                  
     ISNULL(MNT_COV_MULTI.COV_DES,MNT_COV.COV_DES) AS COV_DES,      
     MNT_COV.COMPONENT_CODE AS COMPONENT_CODE,          
     ISNULL(POL_COV.LIMIT_1,0) LIMIT_1,            
     ISNULL(POL_COV.WRITTEN_PREMIUM,0) WRITTEN_PREMIUM,            
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,      
     ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1  ,    
      ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD,    
        '' VALUE_AT_RISK,    
        'DESCRIÇÃO DE ATIVIDADES'   AS  ACTIVITY_DESC  ,    
       0 AS  MAXIMUM_LIMIT,    
       MNT_COV.IS_MAIN AS IS_MAIN    
     FROM POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK)      
    LEFT OUTER JOIN            
       POL_PENHOR_RURAL_INFO POL_RISKINFO WITH(NOLOCK) ON              
       POL_COV.CUSTOMER_ID=POL_RISKINFO.CUSTOMER_ID AND         
       POL_COV.POLICY_ID=POL_RISKINFO.POLICY_ID AND        
       POL_COV.POLICY_VERSION_ID=POL_RISKINFO.POLICY_VERSION_ID AND         
       POL_COV.RISK_ID=POL_RISKINFO.PENHOR_RURAL_ID    AND     
       ISNULL(POL_RISKINFO.IS_ACTIVE,'Y') = 'Y'             
      LEFT OUTER JOIN                  
       MNT_COVERAGE MNT_COV WITH(NOLOCK) ON            
       POL_COV.COVERAGE_CODE_ID=MNT_COV.COV_ID      
      LEFT OUTER JOIN                    
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON              
         MNT_COV_MULTI.COV_ID = MNT_COV.COV_ID AND      
         MNT_COV_MULTI.LANG_ID = @LANG_ID      
      LEFT OUTER JOIN                    
      MNT_LOOKUP_VALUES LOKUP WITH(NOLOCK) ON              
         LOKUP.LOOKUP_UNIQUE_ID = POL_RISKINFO.CULTIVATION   
      LEFT OUTER JOIN     
      MNT_LOOKUP_VALUES_MULTILINGUAL LOKUP_MULTI WITH(NOLOCK) ON              
         LOKUP_MULTI.LOOKUP_UNIQUE_ID = POL_RISKINFO.CULTIVATION AND  
         LOKUP_MULTI.LANG_ID = @LANG_ID  
           
     WHERE             
      POL_RISKINFO.CUSTOMER_ID = @CUSTOMER_ID AND POL_RISKINFO.POLICY_ID = @POLICY_ID AND POL_RISKINFO.POLICY_VERSION_ID = @POLICY_VERSION_ID         
      ORDER BY RISK_ID ASC        
           END     --END Rural Lien   
  
                     
 END              
               
               
               
                 
               
                    
               
               
                   
                   
                 
                 

GO

