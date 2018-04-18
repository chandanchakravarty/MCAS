IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyPDF_Risks_Details_Abhi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyPDF_Risks_Details_Abhi]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

	CREATE  PROC [dbo].[Proc_GetPolicyPDF_Risks_Details_Abhi] 
 (                   
 @CUSTOMER_ID INT,                  
 @POLICY_ID INT,                
 @POLICY_VERSION_ID INT,                  
 @LOBID INT ,            
 @LANG_ID INT=NULL ,       
 @RESULT_COUNT INT OUTPUT      
                      
 )                    
 AS                    
 BEGIN               
      
       
  DECLARE @COVERAGES TABLE (COVERAGE_CODE_ID INT)          
  DECLARE @RISK TABLE (RISK_ID INT)          
  DECLARE @RISK_COV TABLE (RISK_ID INT, COVERAGE_CODE_ID INT)          
  DECLARE @INCLUDE TABLE (TotalInsuredObjI VARCHAR(20),TotalSumInsuredI VARCHAR(20),TotalPremiumI VARCHAR(20))      
  DECLARE @EXCULDE TABLE (TotalInsuredObjE VARCHAR(20),TotalSumInsuredE VARCHAR(20),TotalPremiumE VARCHAR(20))      
  DECLARE @TOTALINSURED TABLE(TotalInsuredObjT VARCHAR(20),TotalSumInsuredT VARCHAR(20),TotalPremiumT VARCHAR(20))      
  DECLARE @INCLUDEEXCULDE TABLE(TotalInsuredObjT VARCHAR(20),TotalSumInsuredT VARCHAR(20),TotalPremiumT VARCHAR(20),TotalInsuredObjI VARCHAR(20),TotalSumInsuredI VARCHAR(20),TotalPremiumI VARCHAR(20),TotalInsuredObjE VARCHAR(20),TotalSumInsuredE VARCHAR(20),TotalPremiumE VARCHAR(20))      
  DECLARE @VALUE_AT_RISK1 TABLE (RISK_ID1 INT,VALUE_AT_RISK1 DECIMAL(18,2),CUSTOMER_ID1 INT,POLICY_ID1 INT,POLICY_VERSION_ID1 INT)
  DECLARE @VALUE_AT_RISK2 TABLE (RISK_ID2 INT,VALUE_AT_RISK2 DECIMAL(18,2),CUSTOMER_ID2 INT,POLICY_ID2 INT,POLICY_VERSION_ID2 INT)
  DECLARE @FINAL_VALUE_AT_RISK TABLE (RISK_ID INT,VALUE_AT_RISK1 DECIMAL(18,2),VALUE_AT_RISK2 DECIMAL(18,2),FINAL_VALUE_AT_RISK DECIMAL(18,2),CUSTOMER_ID1 INT,POLICY_ID1 INT,POLICY_VERSION_ID1 INT,CUSTOMER_ID2 INT,POLICY_ID2 INT,POLICY_VERSION_ID2 INT)
  DECLARE @MAX_LIMIT_VALUE_AT_RISK TABLE(RISK_ID INT,VALUE_AT_RISK DECIMAL(18,2),MAX_LIMIT DECIMAL(18,2),ACTIVITY_TYPE int,REMARKS VARCHAR(4000))
  DECLARE @PROCESS_ID INT
  SELECT @PROCESS_ID = PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND NEW_POLICY_VERSION_ID  = @POLICY_VERSION_ID
  AND UPPER(PROCESS_STATUS) <>'ROLLBACK'   
  
  DECLARE @IncludeCount NVARCHAR(5)           
  DECLARE @IncludeSumInsured NVARCHAR(5)            
  DECLARE @IncludePremium NVARCHAR(5)       
  DECLARE @ExcludeCount NVARCHAR(5)           
  DECLARE @ExcludeSumInsured NVARCHAR(5)            
  DECLARE @ExcludePremium NVARCHAR(5)            
       
 DECLARE  @POLICY_COVERAGE TABLE      
 (      
 ID INT IDENTITY(1,1),      
 CUSTOMER_ID INT,      
 POLICY_ID INT,      
 POLICY_VERSION_ID INT,      
 RISK_ID INT,      
 COVERAGE_ID INT,      
 COVERAGE_CODE_ID INT,      
 RI_APPLIES NCHAR(50),      
 LIMIT_OVERRIDE NVARCHAR(500),      
 LIMIT_1 DECIMAL(25,2),      
 LIMIT_1_TYPE NVARCHAR(500),      
 LIMIT_2 DECIMAL(25,2),      
 LIMIT_2_TYPE NVARCHAR(500),      
 LIMIT1_AMOUNT_TEXT NVARCHAR(500),      
 LIMIT2_AMOUNT_TEXT NVARCHAR(500),      
 DEDUCT_OVERRIDE NCHAR(500),      
 DEDUCTIBLE_1 DECIMAL(25,2),      
 DEDUCTIBLE_1_TYPE NVARCHAR(500),      
 DEDUCTIBLE_2 DECIMAL(25,2),      
 DEDUCTIBLE_2_TYPE NVARCHAR(500),      
 MINIMUM_DEDUCTIBLE DECIMAL(25,2),      
 DEDUCTIBLE1_AMOUNT_TEXT NVARCHAR(500),      
 DEDUCTIBLE2_AMOUNT_TEXT NVARCHAR(500),      
 DEDUCTIBLE_REDUCES NVARCHAR(500),      
 INITIAL_RATE DECIMAL(25,2),      
 FINAL_RATE DECIMAL(25,2),      
 AVERAGE_RATE NCHAR(50),      
 WRITTEN_PREMIUM DECIMAL(25,2),      
 FULL_TERM_PREMIUM DECIMAL(25,2),      
 IS_SYSTEM_COVERAGE NCHAR(50),      
 LIMIT_ID INT,      
 DEDUC_ID INT,      
 ADD_INFORMATION NVARCHAR(500),      
 CREATED_BY INT,      
 CREATED_DATETIME DATETIME,      
 MODIFIED_BY INT,      
 LAST_UPDATED_DATETIME DATETIME,      
 INDEMNITY_PERIOD INT,      
 CHANGE_INFORCE_PREM DECIMAL(25,2),      
 PREV_LIMIT DECIMAL(25,2)      
      
 )      
       
INSERT INTO @POLICY_COVERAGE      
 EXEC PROC_RiskCoverage @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID  
 
 INSERT INTO @RISK_COV (RISK_ID, COVERAGE_CODE_ID)       
  SELECT RISK_ID,COVERAGE_CODE_ID FROM @POLICY_COVERAGE 
  
  --SELECT * FROM @RISK_COV   
  
     IF (@LOBID = 2 OR @LOBID = 3) --for Automobile and Motorcycle            
     BEGIN                 
                 
        SELECT               
      POL_COV.VEHICLE_ID as RISK_ID,          
      0 as LOCATION_ID,              
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
      0 as LOCATION_ID,              
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
      0 as LOCATION_ID,              
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
      ISNULL(POL_COV.DEDUCTIBLE_1,0) DEDUCTIBLE_1            
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
 ELSE IF(@LOBID in (9,26))    ---All Risks and Named Perils              
   BEGIN             
             
            
            
  --INSERT INTO @RISK(RISK_ID)          
  --SELECT DISTINCT RISK_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @COVERAGES(COVERAGE_CODE_ID)          
  --SELECT DISTINCT COVERAGE_CODE_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
     
  --SELECT RISK_ID, COVERAGE_CODE_ID          
  --FROM @RISK, @COVERAGES          
  --ORDER BY RISK_ID          
              
              
        SELECT * into #tempPOL_PERILS FROM POL_PERILS WHERE 1=2      
        ALTER TABlE #tempPOL_PERILS      
        ADD ID INT IDENTITY(1,1)      
       --  select * from #tempPOL_PERILS       
        INSERT INTO #tempPOL_PERILS SELECT       
        @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID ,           
         TEMP.RISK_ID,      
         RISK_INFO.CALCULATION_NUMBER,      
    --RISK_INFO.LOCATION,      
    isnull( RISK_INFO.LOCATION,0),      
    RISK_INFO.ADDRESS,      
    isnull( RISK_INFO.NUMBER,0),      
    RISK_INFO.COMPLEMENT,      
    RISK_INFO.CITY,      
    RISK_INFO.COUNTRY,      
    RISK_INFO.STATE,      
    RISK_INFO.ZIP,      
    RISK_INFO.TELEPHONE,      
    RISK_INFO.EXTENTION,      
    RISK_INFO.FAX,      
    RISK_INFO.CATEGORY,      
    RISK_INFO.ATIV_CONTROL,      
    RISK_INFO.LOC,      
    RISK_INFO.LOCALIZATION,      
    RISK_INFO.OCCUPANCY,      
    RISK_INFO.CONSTRUCTION,      
    RISK_INFO.LOC_CITY,      
    RISK_INFO.CONSTRUCTION_TYPE,      
    RISK_INFO.ACTIVITY_TYPE,      
    RISK_INFO.RISK_TYPE,      
    RISK_INFO.VR,      
    RISK_INFO.LMI,      
    RISK_INFO.BUILDING,      
    RISK_INFO.MMU,      
    RISK_INFO.MMP,      
    RISK_INFO.MRI,      
    RISK_INFO.TYPE,      
    RISK_INFO.LOSS,      
    RISK_INFO.LOYALTY,      
    RISK_INFO.PERC_LOYALTY,      
    RISK_INFO.DEDUCTIBLE_OPTION,      
    RISK_INFO.MULTIPLE_DEDUCTIBLE,      
    RISK_INFO.E_FIRE,      
    RISK_INFO.S_FIXED_FOAM,      
    RISK_INFO.S_FIXED_INSERT_GAS,      
    RISK_INFO.CAR_COMBAT,      
    RISK_INFO.S_DETECT_ALARM,      
    RISK_INFO.S_FIRE_UNIT,      
    RISK_INFO.S_FOAM_PER_MANUAL,      
    RISK_INFO.S_MANUAL_INERT_GAS,      
    RISK_INFO.S_SEMI_HOSES,      
    RISK_INFO.HYDRANTS,      
    RISK_INFO.SHOWERS,      
    RISK_INFO.SHOWER_CLASSIFICATION,      
    RISK_INFO.FIRE_CORPS,      
    RISK_INFO.PUNCTUATION_QUEST,      
    RISK_INFO.DMP,      
    RISK_INFO.EXPLOSION_DEGREE,      
    RISK_INFO.PR_LIQUID,      
    RISK_INFO.COD_ATIV_DRAFTS,      
    RISK_INFO.OCCUPATION_TEXT,      
    RISK_INFO.ASSIST24,      
    RISK_INFO.LMRA,      
    RISK_INFO.AGGRAVATION_RCG_AIR,      
    RISK_INFO.EXPLOSION_DESC,      
    RISK_INFO.PROTECTIVE_DESC,      
    RISK_INFO.LMI_DESC,      
    RISK_INFO.LOSS_DESC,      
    RISK_INFO.QUESTIONNAIRE_DESC,      
    RISK_INFO.DEDUCTIBLE_DESC,      
    RISK_INFO.GROUPING_DESC,      
    RISK_INFO.LOC_FLOATING,      
    RISK_INFO.ADJUSTABLE,      
    RISK_INFO.IS_ACTIVE,      
    RISK_INFO.CREATED_BY,      
    RISK_INFO.CREATED_DATETIME,      
    RISK_INFO.MODIFIED_BY,      
    LAST_UPDATED_DATETIME,      
    RISK_INFO.CORRAL_SYSTEM,      
    RISK_INFO.RAWVALUES,      
    RISK_INFO.REMARKS,      
    RISK_INFO.PARKING_SPACES,      
    RISK_INFO.CLAIM_RATIO,      
    RISK_INFO.RAW_MATERIAL_VALUE,      
    RISK_INFO.CONTENT_VALUE,      
    RISK_INFO.BONUS,      
    RISK_INFO.CO_APPLICANT_ID,      
    RISK_INFO.LOCATION_NUMBER,      
    RISK_INFO.ITEM_NUMBER,      
    RISK_INFO.ACTUAL_INSURED_OBJECT,      
    RISK_INFO.ORIGINAL_VERSION_ID,      
    RISK_INFO.CO_RISK_ID      
                         
    FROM          
       @RISK_COV TEMP               
    LEFT OUTER JOIN          
    POL_PERILS RISK_INFO  ON RISK_INFO.PERIL_ID= TEMP.RISK_ID          
    AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID          
                       
   ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID          
             
  --SELECT * FROM POL_PERILS WHERE CUSTOMER_ID = 28025 AND POLICY_ID = 174         
             
       
   SELECT               
      RISKS.PERIL_ID as RISK_ID,            
      ISNULL(RISKS.LOCATION,0) as LOCATION_ID,         
      ISNULL(RISKS.LOCATION_NUMBER,0) AS LOCATION_NUMBER,       
      ISNULL(RISKS.ITEM_NUMBER,0) AS ITEM_NUMBER,        
      ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID),0) as RISK_PREMIUM,          
            
      ISNULL(RISKS.REMARKS,'') AS REMARKS,        
            
      dbo.fun_FormatCurrency(RISKS.VR,@LANG_ID) as VALUE_AT_RISK ,             
      dbo.fun_FormatCurrency(RISKS.BUILDING,@LANG_ID) AS MAXIMUM_LIMIT,        
      ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'') AS APPLICABLE_LOCATIONS ,         
       ISNULL(UPPER(MNT_ACT_MST.ACTIVITY_DESC),'')   AS  ACTIVITY_DESC   ,        
       ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'') AS LOCATION_NUMBER ,           
      (ISNULL(POL_LOCA.LOC_ADD1,'') +', '+      
     +ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'')  + ISNULL(' - '+LOC_ADD2,'')            
     +ISNULL(' - '+DISTRICT,'')         
     +ISNULL(' - '+POL_LOCA.LOC_CITY,'')+'/'+ ISNULL(MNT_CSL.STATE_CODE,''))  AS LOCATION,           
      
      COVERAGE.IS_ACTIVE,      
      COVERAGE.COV_ID AS COVERAGE_CODE_ID,         
      case when @LANG_ID = 2          
      then  ISNULL(MNT_COV_MULTI.COV_DES,'')          
      else          
      COVERAGE.COV_DES          
      end          
      COV_DES,            
      COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,      
            
       ISNULL( dbo.fun_FormatCurrency(       
      case when IS_MAIN <> '1' Then      
      case when BASIC_SI>0       
      then      
      CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100      
     THEN 0       
     ELSE      
     (POL_COV.LIMIT_1/PSB.BASIC_SI)*100      
     END      
     else      
     0      
     end      
     else      
     0      
     end      
           
           
     ,@LANG_ID      
      
     )      
     ,0) as PsB,       
      ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,                  
      ISNULL( dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,            
           
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%','')      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' ,'')      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
         THEN      
           CASE       
     WHEN (POL_COV.DEDUCTIBLE_1 IS NOT NULL AND POL_COV.DEDUCTIBLE_1>0) or (POL_COV.MINIMUM_DEDUCTIBLE IS NOT NULL AND POL_COV.MINIMUM_DEDUCTIBLE>0)      
     THEN   --'R$ '+ ''+ ISNULL(CAST(ISNULL(DBO.FUN_FORMATCURRENCY(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
     ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) AS NVARCHAR(10)) ,'')      
        ELSE   ''      
     END      
            
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10)),'')      
            
      end as DEDUCTIBLE_1,      
            
               
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                    -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN        
      UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))      
       WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN 'HORAS'      
            
      end as DEDUCTIBLE_1_Text,      
             
      ISNULL(POL_COV.DEDUCTIBLE2_AMOUNT_TEXT,'') AS DEDUCTIBLE2_AMOUNT_TEXT,        
              
    CASE   
  WHEN ISNULL(POL_COV.INDEMNITY_PERIOD,0) > 0 THEN  CAST(ISNULL('PI= '+ CAST(POL_COV.INDEMNITY_PERIOD AS VARCHAR(20)) ,'')AS VARCHAR(20) )  
  ELSE '' END AS INDEMNITY_PERIOD , 
             
      COVERAGE.IS_MAIN AS IS_MAIN   ,      
          
          
     case when  COVERAGE.IS_MAIN=1      
     then 'Basica'      
     when COVERAGE.IS_MAIN=0      
     then  'Additional'      
     end as  IS_MAIN_Text,      
     ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT       
     FROM       
     #tempPOL_PERILS RISKS       
      
       JOIN           
                 
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)      
        ON                    
               
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.PERIL_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'          
       and RISKS.id=POL_COV.ID      
                  
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE          
                 
     LEFT OUTER JOIN                        
              
      MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
  RISKS.LOCATION = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
                
        LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID =@LANG_ID        
                  
        LEFT OUTER JOIN             
        MNT_ACTIVITY_MASTER MNT_ACT_MST WITH(NOLOCK) ON            
        MNT_ACT_MST.ACTIVITY_ID= RISKS.ACTIVITY_TYPE           
                
                
         --LEFT OUTER JOIN          
         --MNT_LOOKUP_VALUES MNT_LOOK_VALUE WITH(NOLOCK) ON        
         --MNT_LOOK_VALUE.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
                 
     LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT_CSL WITH(NOLOCK) ON      
     MNT_CSL.STATE_ID=POL_LOCA.LOC_STATE      
         
 LEFT OUTER JOIN           
 (          
  select CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
  sum(WRITTEN_PREMIUM) as RISK_PREMIUM          
  from  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
  where           
   CUSTOMER_ID = @CUSTOMER_ID           
   AND POLICY_ID =@POLICY_ID           
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
  group by CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
  ) RP          
  on RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
  and RP.POLICY_ID=RISKS.POLICY_ID          
  and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
  and RP.RISK_ID = POL_COV.RISK_ID          
  join           
   (         
   select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
    )        
  PSB         
  on RISKS.PERIL_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
  WHERE                   
  RISKS.CUSTOMER_ID =  @CUSTOMER_ID           
  AND RISKS.POLICY_ID = @POLICY_ID        
  AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID           
  ORDER BY RISK_ID ASC            
  FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')         
     SET @RESULT_COUNT  = @@ROWCOUNT      
               
        DROP TABLE #tempPOL_PERILS      
              
   select RISK_ID,ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,      
   CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE  from POL_DISCOUNT_SURCHARGE DISCOUNT LEFT OUTER JOIN        
   POL_PRODUCT_LOCATION_INFO  RISK       
         
 ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID      
   LEFT OUTER  JOIN      
            
   MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR  ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID      
         
   WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID      
   FOR XML AUTO,ELEMENTS,ROOT('DISCOUNTS')       
               
               
   END  --end named Perils            
               
   ELSE IF  (@LOBID IN (10,11,12,14,16,19,25,27,32))  --For Comprehensive Condominium ,Comprehensive Company ,General Civil Liability  ,Diversified Risks ,Robbery               
   BEGIN        
              
        --declare temp table for cross join risk table       
              
            
            
  --INSERT INTO @RISK(RISK_ID)          
  --SELECT DISTINCT RISK_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @COVERAGES(COVERAGE_CODE_ID)          
  --SELECT DISTINCT COVERAGE_CODE_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @RISK_COV (RISK_ID, COVERAGE_CODE_ID)          
  --SELECT RISK_ID, COVERAGE_CODE_ID          
  --FROM @RISK, @COVERAGES          
  --ORDER BY RISK_ID    
  
  --declare @Previous_Value_at_risk int
  --set @Previous_Value_at_risk = (select VALUE_AT_RISK from POL_PRODUCT_LOCATION_INFO where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID - 1)
  --declare @Previous_Max_Limit int
  --set @Previous_Max_Limit = (select MAXIMUM_LIMIT from POL_PRODUCT_LOCATION_INFO where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID - 1)
  
  
  --INSERT INTO @VALUE_AT_RISK1(RISK_ID1,VALUE_AT_RISK1,CUSTOMER_ID1,POLICY_ID1,POLICY_VERSION_ID1)
  --SELECT  PRODUCT_RISK_ID,VALUE_AT_RISK,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID FROM  POL_PRODUCT_LOCATION_INFO where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID - 1
  
  
  --INSERT INTO @VALUE_AT_RISK2(RISK_ID2,VALUE_AT_RISK2,CUSTOMER_ID2,POLICY_ID2,POLICY_VERSION_ID2)
  --SELECT PRODUCT_RISK_ID,VALUE_AT_RISK,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID FROM  POL_PRODUCT_LOCATION_INFO where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID
  
      
  --INSERT INTO @FINAL_VALUE_AT_RISK(RISK_ID,VALUE_AT_RISK1, VALUE_AT_RISK2,FINAL_VALUE_AT_RISK)          
  --SELECT A.RISK_ID1,VALUE_AT_RISK1, VALUE_AT_RISK2,CASE WHEN VALUE_AT_RISK1 = VALUE_AT_RISK2 THEN 0  ELSE VALUE_AT_RISK2 END AS VALUE_AT_RISK         
  --FROM @VALUE_AT_RISK1 AS A, @VALUE_AT_RISK2
  
  --select * from @FINAL_VALUE_AT_RISK
  
  --select * from(SELECT * FROM @VALUE_AT_RISK1 a
  --union 
  --select * from @VALUE_AT_RISK2 b) ValueRisk --where ValueRisk.CUSTOMER_ID1 = 28025 and ValueRisk.POLICY_ID1 = 116 and ValueRisk.POLICY_VERSION_ID1 = 4 
  
  INSERT INTO @MAX_LIMIT_VALUE_AT_RISK(RISK_ID,VALUE_AT_RISK,MAX_LIMIT,ACTIVITY_TYPE,REMARKS)
  SELECT PRODUCT_RISK_ID,VALUE_AT_RISK,MAXIMUM_LIMIT,ACTIVITY_TYPE,REMARKS FROM POL_PRODUCT_LOCATION_INFO 
  WHERE CUSTOMER_ID = @CUSTOMER_ID 
  AND POLICY_ID = @POLICY_ID 
  AND POLICY_VERSION_ID =(select POLICY_VERSION_ID from POL_POLICY_PROCESS where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <> 'ROLLBACK')

  --SELECT * FROM @MAX_LIMIT_VALUE_AT_RISK
  
  

        SELECT * into #tempPOL_PRODUCT_LOCATION_INFO FROM POL_PRODUCT_LOCATION_INFO WHERE 1=2      
        ALTER TABlE #tempPOL_PRODUCT_LOCATION_INFO      
        ADD ID INT IDENTITY(1,1)      
              
       -- FROM       
       INSERT INTO #tempPOL_PRODUCT_LOCATION_INFO SELECT        
           @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID ,           
           TEMP.RISK_ID,        
           RISK_INFO.LOCATION,      
           RISK_INFO.VALUE_AT_RISK,      
           RISK_INFO.BUILDING_VALUE,          
           RISK_INFO.CONTENTS_VALUE,      
           RISK_INFO.RAW_MATERIAL_VALUE,      
           RISK_INFO.CONTENTS_RAW_VALUES ,         
           RISK_INFO.MRI_VALUE,      
           RISK_INFO.MAXIMUM_LIMIT,          
           RISK_INFO.POSSIBLE_MAX_LOSS,      
           RISK_INFO.MULTIPLE_DEDUCTIBLE,      
           RISK_INFO.PARKING_SPACES,      
           RISK_INFO.ACTIVITY_TYPE,          
           RISK_INFO.OCCUPIED_AS,      
           RISK_INFO.CONSTRUCTION,      
           RISK_INFO.RUBRICA,      
           RISK_INFO.ASSIST24,      
           RISK_INFO.REMARKS,      
           RISK_INFO.IS_ACTIVE,          
           RISK_INFO.CREATED_BY,      
           RISK_INFO.CREATED_DATETIME,      
           RISK_INFO.MODIFIED_BY,      
           RISK_INFO.LAST_UPDATED_DATETIME,      
           RISK_INFO.CLAIM_RATIO,            
           RISK_INFO.BONUS,      
           RISK_INFO.CO_APPLICANT_ID,      
           RISK_INFO.CLASS_FIELD,      
           RISK_INFO.LOCATION_NUMBER,      
           RISK_INFO.ITEM_NUMBER,      
           RISK_INFO.ACTUAL_INSURED_OBJECT,      
           RISK_INFO.ORIGINAL_VERSION_ID,      
           RISK_INFO.PORTABLE_EQUIPMENT ,      
           RISK_INFO.CO_RISK_ID      
                
                
   FROM          
       @RISK_COV TEMP           
           
                      
      LEFT OUTER JOIN           
   POL_PRODUCT_LOCATION_INFO RISK_INFO  ON RISK_INFO.PRODUCT_RISK_ID= TEMP.RISK_ID          
   AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID          
        
       -- SELECT * FROM #tempRISK   
       
          
      
 SELECT          
  RISKS.PRODUCT_RISK_ID as RISK_ID,         
     ISNULL(RISKS.LOCATION,0) AS LOCATION_ID,       
     ISNULL(RISKS.LOCATION_NUMBER,0) AS LOCATION_NUMBER,       
     ISNULL(POL_LOCA.LOC_NUM ,'')AS  LOCATION_NUM,      
     ISNULL(RISKS.ITEM_NUMBER,0) AS ITEM_NUMBER,      
     ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID),0) as RISK_PREMIUM,            
     ISNULL(RISKS.REMARKS,'') AS REMARKS ,       
     dbo.fun_FormatCurrency(RISKS.VALUE_AT_RISK,@LANG_ID) as VALUE_AT_RISK ,        
      isnull(MNT_LVM.LOOKUP_VALUE_DESC,'') as CLASS_FIELD,       
     dbo.fun_FormatCurrency(RISKS.MAXIMUM_LIMIT,@LANG_ID) AS MAXIMUM_LIMIT,         
     ISNULL(UPPER(MNT_ACT_MST.ACTIVITY_DESC),'')   AS  ACTIVITY_DESC   ,      
     ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'') AS APPLICABLE_LOCATIONS ,        
     RISKS.ACTUAL_INSURED_OBJECT as ACTUAL_INSURED_OBJECT, 
       ISNULL(dbo.fun_FormatCurrency(PRE_RISK.VALUE_AT_RISK,@LANG_ID),0) AS PRE_VALUE_AT_RISK,
     ISNULL(dbo.fun_FormatCurrency(PRE_RISK.MAX_LIMIT,@LANG_ID),0) AS PRE_MAX_LIMIT, 
     ISNULL(PRE_RISK.VALUE_AT_RISK,0) AS PRE_VALUE_AT_RISK_C,
     ISNULL(PRE_RISK.MAX_LIMIT,0) AS PRE_MAX_LIMIT_C,
     ISNULL(RISKS.VALUE_AT_RISK,0) AS CURRENT_VALUE_AT_RISK_C,
     ISNULL(RISKS.MAXIMUM_LIMIT,0) AS CURRENT_MAX_LIMIT_C,
     ISNULL(PRE_RISK.ACTIVITY_TYPE,'') AS PRE_ACTIVITY_TYPE,
     ISNULL(RISKS.ACTIVITY_TYPE,0) AS CURRENT_ACTIVITY_TYPE,
     ISNULL(PRE_RISK.REMARKS,'') AS PRE_REMARKS,
                  
    (ISNULL(POL_LOCA.LOC_ADD1,'') +', '+      
     +ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'')  + ISNULL(' - '+LOC_ADD2,'')            
     +ISNULL(' - '+DISTRICT,'')              
     +ISNULL(' - '+POL_LOCA.LOC_CITY,'')+'/'+ ISNULL(MNT_CSL.STATE_CODE,''))  AS LOCATION,  
    
       
           
      COVERAGE.IS_ACTIVE,            
      COVERAGE.COV_ID AS COVERAGE_CODE_ID,          
     case when @lANG_ID = 2          
     then  ISNULL(UPPER(MNT_COV_MULTI.COV_DES),'')          
     else          
      COVERAGE.COV_DES          
     end          
     COV_DES,            
     COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,       
      ISNULL( dbo.fun_FormatCurrency(       
    case when IS_MAIN <> '1' Then      
      case when BASIC_SI>0       
      then      
      CASE WHEN  POL_COV.LIMIT_1/PSB.BASIC_SI = 100 THEN 0  ELSE POL_COV.LIMIT_1/PSB.BASIC_SI   END      
      else  0 end      
     else      
     0      
     end      
           
           
     ,@LANG_ID      
      
     )       ,0) as PsB,      
           
         
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,         
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,            
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%','')      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
        -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' ,'')      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
         THEN      
           CASE       
     WHEN (POL_COV.DEDUCTIBLE_1 IS NOT NULL AND POL_COV.DEDUCTIBLE_1>0) or (POL_COV.MINIMUM_DEDUCTIBLE IS NOT NULL AND POL_COV.MINIMUM_DEDUCTIBLE>0)      
     THEN   --'R$ '+ ''+ ISNULL(CAST(ISNULL(DBO.FUN_FORMATCURRENCY(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
     ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) AS NVARCHAR(10)) ,'')      
        ELSE   ''      
     END      
            
 WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10)),'')      
            
      end as DEDUCTIBLE_1,      
            
               
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))      
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN        
      UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))      
       WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN 'HORAS'      
            
      end as DEDUCTIBLE_1_Text,       
             
     ISNULL(POL_COV.DEDUCTIBLE2_AMOUNT_TEXT,'') AS DEDUCTIBLE2_AMOUNT_TEXT,      
      
       
  CASE   
  WHEN ISNULL(POL_COV.INDEMNITY_PERIOD,0) > 0 THEN  CAST(ISNULL('PI= '+ CAST(POL_COV.INDEMNITY_PERIOD AS VARCHAR(20)) ,'')AS VARCHAR(20) )  
  ELSE '' END AS INDEMNITY_PERIOD ,  
            
     COVERAGE.IS_MAIN AS IS_MAIN  ,      
     case when  COVERAGE.IS_MAIN=1      
     then 'Basica'      
     when COVERAGE.IS_MAIN=0      
     then  'Additional'      
     end as  IS_MAIN_Text,      
   ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT
 
            
     FROM       
     #tempPOL_PRODUCT_LOCATION_INFO RISKS       
      
       JOIN           
                 
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)      
        ON                    
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.PRODUCT_RISK_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'          
       and POL_COV.ID = RISKS.ID      
           
       
           
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
           
    left outer join   MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LVM ON MNT_LVM.LOOKUP_UNIQUE_ID =RISKS.CLASS_FIELD      
         
     LEFT OUTER JOIN                        
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
          
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.LOCATION = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
    LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID = @LANG_ID        
                  
    LEFT OUTER JOIN MNT_ACTIVITY_MASTER MNT_ACT_MST WITH(NOLOCK) ON          
     MNT_ACT_MST.ACTIVITY_ID= RISKS.ACTIVITY_TYPE          
           
     LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT_CSL WITH(NOLOCK) ON      
     MNT_CSL.STATE_ID=POL_LOCA.LOC_STATE      
             
     LEFT OUTER JOIN           
 (SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
 SUM(WRITTEN_PREMIUM) as RISK_PREMIUM          
 FROM  @POLICY_COVERAGE --WITH(NOLOCK)          
 WHERE           
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID =@POLICY_ID           
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
 ) RP          
 ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
 and RP.POLICY_ID=RISKS.POLICY_ID          
 and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
 and RP.RISK_ID = POL_COV.RISK_ID          
        
join           
   (   select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join @POLICY_COVERAGE p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from @POLICY_COVERAGE l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
    )        
  PSB         
  on RISKS.PRODUCT_RISK_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
  LEFT OUTER JOIN @MAX_LIMIT_VALUE_AT_RISK PRE_RISK
  ON   RISKS.PRODUCT_RISK_ID=PRE_RISK.RISK_ID          
        
   WHERE                   
   RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID =@POLICY_VERSION_ID              
   ORDER BY  RISK_ID, IS_MAIN desc      
  FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')      
      
   SET @RESULT_COUNT  = @@ROWCOUNT      
       
   DROP TABLE #tempPOL_PRODUCT_LOCATION_INFO      
         
       
       
       IF(@PROCESS_ID IN (3,14))
    BEGIN
    
    
              SELECT 
               DISCOUNT.RISK_ID,
               ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,
               ISNULL(DISCOUNT.PERCENTAGE,0) AS CURRENT_PERCENTAGE,
		       ISNULL(PREV_DISCOUNT.PERCENTAGE,0) AS PREV_PERCENTAGE,
               CASE 
					WHEN(ISNULL(DISCOUNT.PERCENTAGE,0) <> ISNULL(PREV_DISCOUNT.PERCENTAGE,0)) 
					THEN 1 ELSE 0 END IS_PRINT,
				--OTHER Table column
		      --ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,  
	          CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE 
               FROM POL_DISCOUNT_SURCHARGE DISCOUNT WITH(NOLOCK) 
			   LEFT OUTER JOIN (
			   SELECT * FROM POL_DISCOUNT_SURCHARGE PREV_DISCOUNT WITH(NOLOCK) WHERE 
			   PREV_DISCOUNT.CUSTOMER_ID  = @CUSTOMER_ID and PREV_DISCOUNT.POLICY_ID = @POLICY_ID and PREV_DISCOUNT.POLICY_VERSION_ID = (select POLICY_VERSION_ID from POL_POLICY_PROCESS where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID)
			   ) PREV_DISCOUNT
			   ON 
			   PREV_DISCOUNT.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND 
			   PREV_DISCOUNT.POLICY_ID = DISCOUNT.POLICY_ID
			   AND 
			   --PREV_DISCOUNT.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND
			   PREV_DISCOUNT.RISK_ID = DISCOUNT.RISK_ID
			   
              LEFT OUTER JOIN  POL_PRODUCT_LOCATION_INFO  RISK        
              ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID      
              LEFT OUTER  JOIN      
              MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR  ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID      
              WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID      
              FOR XML AUTO,ELEMENTS,  ROOT('DISCOUNTS') 
    END
   ELSE
   BEGIN      
         
   select RISK_ID,ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,      
   CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE  from POL_DISCOUNT_SURCHARGE DISCOUNT LEFT OUTER JOIN        
   POL_PRODUCT_LOCATION_INFO  RISK        
   ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID      
   LEFT OUTER  JOIN      
            
   MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR  ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID      
         
   WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID      
   FOR XML AUTO,ELEMENTS,ROOT('DISCOUNTS')       
    END
    
    
    
  SELECT dbo.fun_FormatCurrency(SUM(VALUE_AT_RISK),@LANG_ID) as Total_Value_at_Risk FROM POL_PRODUCT_LOCATION_INFO where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
  FOR XML AUTO,ELEMENTS,ROOT('SUMOFRISKS')       
        

    SELECT 
     
	   ISNULL(CURRENT_VERSION.PRODUCT_RISK_ID, 0) AS RISK_ID,
	   ISNULL(CURRENT_LOCATION.LOC_ADD1,'') AS CURRENT_LOC_ADD1,
	   ISNULL(PREV_LOCATION.LOC_ADD1,'') AS PREV_LOC_ADD1, 
	   ISNULL(CURRENT_LOCATION.LOC_ADD2,'')  AS CURRENT_LOC_ADD2,
	   ISNULL(PREV_LOCATION.LOC_ADD2,'')  AS PREV_LOC_ADD2,
	   ISNULL(CURRENT_LOCATION.NUMBER,0) AS CURRENT_NUMBER,
	   ISNULL(PREV_LOCATION.NUMBER,0)AS PREV_NUMBER,
	   ISNULL(CURRENT_LOCATION.DISTRICT,'') CURRENT_DISTRICT,
	   ISNULL(PREV_LOCATION.DISTRICT,'') PREV_DISTRICT,
	   ISNULL(CURRENT_LOCATION.LOC_CITY ,'') AS CURRENT_LOC_CITY,
       ISNULL(PREV_LOCATION.LOC_CITY,'') PREV_LOC_CITY,
	   ISNULL(CURRENT_LOCATION.LOC_STATE,0) CURRENT_LOC_STATE,
	   ISNULL(PREV_LOCATION.LOC_STATE,0) AS PREV_LOC_STATE,
	  CASE WHEN (
	   ISNULL(CURRENT_LOCATION.LOC_ADD1,'') <> ISNULL(PREV_LOCATION.LOC_ADD1,'') OR
	   ISNULL(CURRENT_LOCATION.LOC_ADD2,'') <> ISNULL(PREV_LOCATION.LOC_ADD2,'') OR
	   ISNULL(CURRENT_LOCATION.NUMBER,0) <> ISNULL(PREV_LOCATION.NUMBER,0) OR
	   ISNULL(CURRENT_LOCATION.DISTRICT,'') <> ISNULL(PREV_LOCATION.DISTRICT,'') OR
	   ISNULL(CURRENT_LOCATION.LOC_CITY ,'') <> ISNULL(PREV_LOCATION.LOC_CITY,'') OR
	   ISNULL(CURRENT_LOCATION.LOC_STATE,0) <> ISNULL(PREV_LOCATION.LOC_STATE,0) )
	  THEN 
     (ISNULL(CURRENT_LOCATION.LOC_ADD1,'') +', '+      
      +ISNULL(Convert(varchar(10), CURRENT_LOCATION.NUMBER),'')  + ISNULL(' - '+CURRENT_LOCATION.LOC_ADD2,'')            
      +ISNULL(' - '+CURRENT_LOCATION.DISTRICT,'')              
      +ISNULL(' - '+CURRENT_LOCATION.LOC_CITY,'')+'/'+ ISNULL(MNT_CSL.STATE_CODE,'')) ELSE '' END  RISK_LOCATION
   
   FROM POL_PRODUCT_LOCATION_INFO  CURRENT_VERSION WITH(NOLOCK) 
    LEFT OUTER JOIN(
    SELECT * FROM POL_PRODUCT_LOCATION_INFO  PREV_VERSION WITH(NOLOCK) 
    WHERE PREV_VERSION.CUSTOMER_ID  = @CUSTOMER_ID 
    AND PREV_VERSION.POLICY_ID = @POLICY_ID AND PREV_VERSION.POLICY_VERSION_ID = @POLICY_VERSION_ID -1)PREV_VERSION ON
    PREV_VERSION.PRODUCT_RISK_ID = CURRENT_VERSION.PRODUCT_RISK_ID
    --AND PREV_VERSION.PRODUCT_RISK_ID = CURRENT_VERSION.PRODUCT_RISK_ID
    AND PREV_VERSION.CUSTOMER_ID = CURRENT_VERSION.CUSTOMER_ID
    AND PREV_VERSION.POLICY_ID = CURRENT_VERSION.POLICY_ID
    left outer JOIN 
    POL_LOCATIONS  CURRENT_LOCATION  WITH(NOLOCK) ON 
      CURRENT_VERSION.LOCATION = CURRENT_LOCATION.LOCATION_ID  AND            
      CURRENT_VERSION.CUSTOMER_ID = CURRENT_LOCATION.CUSTOMER_ID AND            
      CURRENT_VERSION.POLICY_ID = CURRENT_LOCATION.POLICY_ID AND            
      CURRENT_VERSION.POLICY_VERSION_ID = CURRENT_LOCATION.POLICY_VERSION_ID 
    left outer JOIN 
    POL_LOCATIONS  PREV_LOCATION  WITH(NOLOCK) ON 
      PREV_VERSION.LOCATION = PREV_LOCATION.LOCATION_ID  AND            
      PREV_VERSION.CUSTOMER_ID = PREV_LOCATION.CUSTOMER_ID AND            
      PREV_VERSION.POLICY_ID = PREV_LOCATION.POLICY_ID AND            
      PREV_VERSION.POLICY_VERSION_ID = PREV_LOCATION.POLICY_VERSION_ID 
     
    JOIN MNT_COUNTRY_STATE_LIST MNT_CSL ON CURRENT_LOCATION.LOC_STATE=MNT_CSL.STATE_ID 
    
    WHERE CURRENT_VERSION.CUSTOMER_ID  = @CUSTOMER_ID 
    AND CURRENT_VERSION.POLICY_ID = @POLICY_ID AND CURRENT_VERSION.POLICY_VERSION_ID = @POLICY_VERSION_ID
     FOR XML AUTO,ELEMENTS,ROOT('RISK_LOCATION')
  
  

         END
        
             
               
   ELSE IF (@LOBID=13)   ---For maritime            
   BEGIN          
               
           
   SELECT               
      POL_COV.RISK_ID as RISK_ID,            
      RISKS.MARITIME_ID as LOCATION_ID,           
      ISNULL( dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,         
          
      ISNULL(RISKS.REMARKS,''), REMARKS,        
   COVERAGE.IS_ACTIVE,      
              
      dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,            
      dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,          
            
     ISNULL(RISKS.NAME_OF_VESSEL,'') AS LOCATION ,         
        0  AS LOCATION_NUMBER ,        
     COVERAGE.COV_ID AS COVERAGE_CODE_ID,          
     case when @lANG_ID =2          
     then  ISNULL(MNT_COV_MULTI.COV_DES,'')          
     else          
      COVERAGE.COV_DES          
     end          
     COV_DES,            
     COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,                
       ISNULL( dbo.fun_FormatCurrency(       
            
      case when BASIC_SI>0      
      then      
      CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100      
     THEN 0       
     ELSE      
     (POL_COV.LIMIT_1/PSB.BASIC_SI)*100      
     END      
     else      
     0      
     end      
     ,@LANG_ID      
           
           
     )      
           
           
     ,0) as PsB,       
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,                  
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,           
           
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍ
  
    
NIMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIM
  
    
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')        
            
      end as DEDUCTIBLE_1,          
        
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,          
     COVERAGE.IS_MAIN AS IS_MAIN          
     FROM POL_MARITIME RISKS WITH(NOLOCK)                
          
       LEFT OUTER JOIN           
                 
       POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK) ON                    
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.MARITIME_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'          
              
                   
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
                 
         
     LEFT OUTER JOIN                
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
          
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.MARITIME_ID = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
   LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID = @LANG_ID          
                
     LEFT OUTER JOIN           
 (          
 SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
 SUM(WRITTEN_PREMIUM) AS RISK_PREMIUM          
 FROM  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
 WHERE           
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID =@POLICY_ID           
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
 ) RP          
 ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
 and RP.POLICY_ID=RISKS.POLICY_ID          
 and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
 and RP.RISK_ID = POL_COV.RISK_ID          
        
        
join           
   (         
select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
)        
  PSB         
  on RISKS.MARITIME_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
   WHERE                   
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID =@POLICY_VERSION_ID               
    ORDER BY RISK_ID ASC    FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')        
  SET @RESULT_COUNT  = @@ROWCOUNT      
           
   END  --end meritime            
                
   ELSE IF (@LOBID=20 or @LOBID=23)  --For National & international Carogo transport            
           BEGIN             
        
    SELECT               
      POL_COV.RISK_ID as RISK_ID,            
      RISKS.COMMODITY_ID as LOCATION_ID,        
              
      ISNULL( dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) ,0)as RISK_PREMIUM,         
           
      ISNULL(RISKS.REMARKS,'') AS REMARKS ,          
              
        COVERAGE.IS_ACTIVE,      
      dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,            
      dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,        
               
           
      ISNULL( RISKS.COMMODITY,'') AS LOCATION,          
       0 AS LOCATION_NUMBER ,        
      COVERAGE.COV_ID AS COVERAGE_CODE_ID,          
     case when @lANG_ID = 2          
     then  ISNULL(MNT_COV_MULTI.COV_DES,'')          
     else          
      COVERAGE.COV_DES          
     end          
     COV_DES,            
     COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,        
      ISNULL( dbo.fun_FormatCurrency(       
            
      case when BASIC_SI>0      
      then      
      CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100      
     THEN 0       
     ELSE      
     (POL_COV.LIMIT_1/PSB.BASIC_SI)*100      
     END      
     else      
     0      
     end      
           
           
     ,@LANG_ID      
           
           
           
           
     )      
           
           
     ,0) as PsB,        
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,                  
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,            
        
     CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')        
            
      end as DEDUCTIBLE_1,        
  
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,          
     COVERAGE.IS_MAIN AS IS_MAIN          
     FROM POL_COMMODITY_INFO RISKS WITH(NOLOCK)                
          
       LEFT OUTER JOIN           
                 
       POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK) ON                    
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.COMMODITY_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'          
             
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
             
                 
      LEFT OUTER JOIN                        
      MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.COMMODITY_ID = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
   LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID = 1           
                
              
       LEFT OUTER JOIN           
 (          
 SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
 SUM(WRITTEN_PREMIUM) AS RISK_PREMIUM          
 FROM  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
 WHERE           
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID =@POLICY_ID           
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
 ) RP          
 ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
 and RP.POLICY_ID=RISKS.POLICY_ID          
 and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
 and RP.RISK_ID = POL_COV.RISK_ID          
         
join           
   (         
 select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
)        
  PSB         
  on RISKS.COMMODITY_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
   WHERE                   
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID               
    ORDER BY RISK_ID ASC    FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')       
    SET @RESULT_COUNT  = @@ROWCOUNT           
           END     --END National & international cargo transport            
                       
   ELSE IF (@LOBID in (15,21,30,33,34)) --For Individual Personal Accident info            
   BEGIN             
            
          
        SELECT * into #tempPOL_PERSONAL_ACCIDENT_INFO FROM POL_PERSONAL_ACCIDENT_INFO WHERE 1=2      
        ALTER TABlE #tempPOL_PERSONAL_ACCIDENT_INFO      
        ADD ID INT IDENTITY(1,1)      
         
   INSERT INTO #tempPOL_PERSONAL_ACCIDENT_INFO      
        
   SELECT           
   TEMP.RISK_ID,         
            @POLICY_ID ,       
            @POLICY_VERSION_ID ,          
            @CUSTOMER_ID ,        
   RISK_INFO.APPLICANT_ID,      
   ISNULL(RISK_INFO.INDIVIDUAL_NAME,''),      
   ISNULL(RISK_INFO.CODE,''),      
   ISNULL(RISK_INFO.POSITION_ID,0),      
   ISNULL(RISK_INFO.CPF_NUM,''),      
   ISNULL(RISK_INFO.STATE_ID,''),      
   ISNULL(RISK_INFO.COUNTRY_ID,0),      
   RISK_INFO.DATE_OF_BIRTH,      
   ISNULL(RISK_INFO.GENDER,0),      
   ISNULL(RISK_INFO.REG_IDEN,0),      
   ISNULL(RISK_INFO.REG_ID_ISSUES,''),      
   ISNULL(RISK_INFO.REG_ID_ORG,0),      
   ISNULL(RISK_INFO.REMARKS,''),      
   ISNULL(RISK_INFO.IS_ACTIVE,0),      
   ISNULL(RISK_INFO.CREATED_BY,0),      
   RISK_INFO.CREATED_DATETIME,      
   ISNULL(RISK_INFO.MODIFIED_BY,0),      
   RISK_INFO.LAST_UPDATED_DATETIME,      
   ISNULL(RISK_INFO.IS_SPOUSE_OR_CHILD,0),      
   ISNULL(RISK_INFO.MAIN_INSURED,0),      
   RISK_INFO.CITY_OF_BIRTH,      
   ISNULL(RISK_INFO.ORIGINAL_VERSION_ID,0),      
   ISNULL(RISK_INFO.CO_RISK_ID,0)
   --ISNULL(CAL.CO_APPL_GENDER,''),
  -- ISNULL(CAL.CO_APPL_MARITAL_STATUS,'')    
        
         
                   
    FROM          
       @RISK_COV TEMP           
                        
      LEFT OUTER JOIN           
   POL_PERSONAL_ACCIDENT_INFO RISK_INFO  ON RISK_INFO.PERSONAL_INFO_ID= TEMP.RISK_ID          
   AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID       
   --LEFT OUTER JOIN CLT_APPLICANT_LIST CAL ON RISK_INFO.CUSTOMER_ID = CAL.CUSTOMER_ID AND RISK_INFO.APPLICANT_ID = CAL.APPLICANT_ID   
     
                      
   ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID       
         
   --select * from #tempPOL_PERSONAL_ACCIDENT_INFO      
        
  SELECT               
		RISKS.PERSONAL_INFO_ID  as RISK_ID,            
		0 as LOCATION_ID,       
		ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,         
		ISNULL(RISKS.REMARKS,'') AS REMARKS,        
		dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,          
        dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,          
		0 AS LOCATION_NUMBER ,
		ISNULL(RISKS.INDIVIDUAL_NAME,'') AS INDIVIDUAL_NAME,
		RISKS.CITY_OF_BIRTH + '/' + cast(RISKS.STATE_ID as varchar(20)) + '-' + CAST(RISKS.DATE_OF_BIRTH AS VARCHAR(20)) as City_State_DOB,
		ISNULL(RISKS.POSITION_ID,0) as Position,
		ISNULL(RISKS.CPF_NUM,'') AS CPF,
		(CAL.CO_APPL_MARITAL_STATUS + '-' + CAL.CO_APPL_GENDER) AS MARITAL_GENDER,
		
		COVERAGE.IS_ACTIVE,        
		COVERAGE.COV_ID AS COVERAGE_CODE_ID,
		case when @lANG_ID = 2          
		then  ISNULL(MNT_COV_MULTI.COV_DES,'')          
		else          
		COVERAGE.COV_DES          
		end          
		COV_DES,            
		COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,      
		ISNULL( dbo.fun_FormatCurrency(  case when BASIC_SI>0      
		then      
		CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100      
		THEN 0       
		ELSE      
		(POL_COV.LIMIT_1/PSB.BASIC_SI)*100      
		END      
		else      
		0      
		end      
		,@LANG_ID      
               
         )      
       ,0) as PsB,                     
     ISNULL( dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,        
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,            
        
        CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')        
            
      end as DEDUCTIBLE_1,        
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,          
     COVERAGE.IS_MAIN AS IS_MAIN   ,       
    case when  COVERAGE.IS_MAIN=1      
     then 'Basica'      
     when COVERAGE.IS_MAIN=0      
     then  'Additional'      
     end as  IS_MAIN_Text,      
     ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT,
     (POL_COV.LIMIT_1 * POL_COV.FINAL_RATE) AS TAXAS_ANUAIS
          
            
     FROM       
     #tempPOL_PERSONAL_ACCIDENT_INFO RISKS 
           
      LEFT OUTER JOIN CLT_APPLICANT_LIST CAL ON RISKS.CUSTOMER_ID = CAL.CUSTOMER_ID AND RISKS.APPLICANT_ID = CAL.APPLICANT_ID   
       JOIN           
                 
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)      
        ON                         
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.PERSONAL_INFO_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'          
       and POL_COV.ID = RISKS.ID      
              
           LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
                 
     LEFT OUTER JOIN                        
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
          
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.PERSONAL_INFO_ID = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
   LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID = 1           
             
             
    LEFT OUTER JOIN           
   (          
   SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
   SUM(WRITTEN_PREMIUM) AS RISK_PREMIUM          
   FROM  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
   WHERE           
    CUSTOMER_ID = @CUSTOMER_ID           
    AND POLICY_ID =@POLICY_ID           
    AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
   GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
   ) RP          
   ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
   and RP.POLICY_ID=RISKS.POLICY_ID          
   and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
   and RP.RISK_ID = POL_COV.RISK_ID          
           
               
 join           
   (         
 select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
)        
  PSB         
  on RISKS.PERSONAL_INFO_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
         
   WHERE                   
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID             
    ORDER BY RISK_ID ASC  FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')         
    SET @RESULT_COUNT  = @@ROWCOUNT      
          
          
    DROP TABLE #tempPOL_PERSONAL_ACCIDENT_INFO   
    
	select PB.BENEFICIARY_NAME,PB.BENEFICIARY_SHARE,PB.BENEFICIARY_RELATION  from POL_BENEFICIARY  PB
	left outer join POL_PERSONAL_ACCIDENT_INFO  PPAI with(Nolock) on
	PPAI.CUSTOMER_ID = PB.CUSTOMER_ID and
	PPAI.POLICY_ID = PB.POLICY_ID and
	PPAI.POLICY_VERSION_ID = PB.POLICY_VERSION_ID 
	left outer join POL_CUSTOMER_POLICY_LIST PCPL with(Nolock) on
	PCPL.CUSTOMER_ID = PPAI.CUSTOMER_ID and
	PCPL.POLICY_ID = PPAI.POLICY_ID and
	PCPL.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID 

	where PCPL.CUSTOMER_ID = @CUSTOMER_ID and PCPL.POLICY_ID = @POLICY_ID  and PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID  
	
	 FOR XML AUTO,ELEMENTS,ROOT('BENEFICIARIOS') 

    
    
    
    
       
          
    ------------------------------          
	declare @Pol_Status_21 varchar(20)          
              
    select @Pol_Status_21 = PROCESS_ID FROM POL_CUSTOMER_POLICY_LIST  POL  WITH(NOLOCK)                          
    LEFT OUTER JOIN POL_POLICY_PROCESS PPP ON PPP.CUSTOMER_ID=POL.CUSTOMER_ID                  
    AND PPP.POLICY_ID=POL.POLICY_ID                   
    AND PPP.NEW_POLICY_VERSION_ID=POL.POLICY_VERSION_ID      
    where POL.CUSTOMER_ID=@CUSTOMER_ID  AND POL.POLICY_ID=@POLICY_ID   AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID and PPP.PROCESS_STATUS <> 'ROLLBACK'         
    
if( @Pol_Status_21= 14 or @Pol_Status_21= 3)        
begin     
 --################################################################################################   
    
declare @POL_VERSION_21 INT    
declare @MAX_RISK_21 INT    
declare @ID_21 INT    
select @POL_VERSION_21 = MAX(POLICY_VERSION_ID) from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID    
select @MAX_RISK_21 = MAX(RISK_ID) from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID    
    
SET @ID_21 = 1    
--PRINT @POL_VERSION    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#TempA_21]') AND type in (N'U'))    
drop table #TempA_21    
select * into #TempA_21 from    
(select * from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POL_VERSION_21 - 1) A    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#TempB_21]') AND type in (N'U'))    
drop table #TempB_21    
select * into #TempB_21 from     
(select * from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POL_VERSION_21) B    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#EXCLUDE_21]') AND type in (N'U'))    
DROP table #EXCLUDE_21    
select * into #EXCLUDE_21 from    
(select * from POL_PRODUCT_COVERAGES    
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID) x    
    
Truncate table #EXCLUDE_21    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#INCLUDE_21]') AND type in (N'U'))    
DROP table #INCLUDE_21    
select * into #INCLUDE_21 from    
(select * from POL_PRODUCT_COVERAGES    
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID) x    
    
Truncate table #INCLUDE_21    
    
while (@ID_21<@MAX_RISK_21 + 1)    
BEGIN    
    
 INSERT INTO #EXCLUDE_21    
    
 select * from #TempA_21 where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID =  @POL_VERSION_21 - 1 and RISK_ID = @ID_21    
 and COVERAGE_CODE_ID not in    
 (select COVERAGE_CODE_ID from #TempB_21 where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID = @POL_VERSION_21  and RISK_ID = @ID_21)    
     
    
 INSERT INTO #INCLUDE_21    
    
 select * from #TempB_21 where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID =  @POL_VERSION_21 and RISK_ID = @ID_21    
 and COVERAGE_CODE_ID not in    
 (select COVERAGE_CODE_ID from #TempA_21 where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID = @POL_VERSION_21 - 1  and RISK_ID = @ID_21)    
    
 SET @ID_21 = @ID_21 + 1    
END    
--##########################################################################################################    
    
select count(*) as TotalInsuredObjT, dbo.fun_FormatCurrency(SUM(LIMIT_1),@LANG_ID) as TotalSumInsuredT,dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID) as TotalPremiumT from POL_PRODUCT_COVERAGES as TOTALINSUREDT where CUSTOMER_ID = @CUSTOMER_ID and 
 
 POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID      
FOR XML AUTO,ELEMENTS,ROOT('TOTALINSURED')      
    
SELECT count(*) as TotalInsuredObjE,dbo.fun_FormatCurrency(SUM(CAST(LIMIT_1 as decimal(18,2))),@LANG_ID) as TotalSumInsuredE,dbo.fun_FormatCurrency(SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))),@LANG_ID) as TotalPremiumE FROM #EXCLUDE_21 as EXCLUSOESOBJ    
 
FOR XML AUTO,ELEMENTS,ROOT('EXCLUSOES')     
    
SELECT count(*) as TotalInsuredObjI,dbo.fun_FormatCurrency(SUM(CAST(LIMIT_1 as decimal(18,2))),@LANG_ID) as TotalSumInsuredI,dbo.fun_FormatCurrency(SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))),@LANG_ID) as TotalPremiumI FROM #INCLUDE_21 as INCLUSÕESOBJ    
FOR XML AUTO,ELEMENTS,ROOT('INCLUSÕES')      
    
insert into @TOTALINSURED(TotalInsuredObjT,TotalSumInsuredT,TotalPremiumT)      
  select count(*) as TotalInsuredObjT,SUM(LIMIT_1) as TotalSumInsuredT,SUM(WRITTEN_PREMIUM) as TotalPremiumT from POL_PRODUCT_COVERAGES as INCLUSÕESOBJ where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID 
  
    
     
           
  insert into @INCLUDE (TotalInsuredObjI,TotalSumInsuredI,TotalPremiumI)      
 SELECT count(*) as TotalInsuredObjI,SUM(CAST(LIMIT_1 as decimal(18,2))) as TotalSumInsuredI,SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))) as TotalPremiumI FROM #INCLUDE_21        
            
   insert into @EXCULDE  (TotalInsuredObjE,TotalSumInsuredE,TotalPremiumE)      
 SELECT count(*) as TotalInsuredObjE,SUM(CAST(LIMIT_1 as decimal(18,2))) as TotalSumInsuredE,SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))) as TotalPremiumE FROM #EXCLUDE_21    
   --FOR XML AUTO,ELEMENTS,ROOT('EXCLUSOES')        
            
   insert into @INCLUDEEXCULDE(TotalInsuredObjT,TotalSumInsuredT,TotalPremiumT,TotalInsuredObjI,TotalSumInsuredI,TotalPremiumI,TotalInsuredObjE,TotalSumInsuredE,TotalPremiumE)      
   select ISNULL(TotalInsuredObjT,'0'),ISNULL(TotalSumInsuredT,'0'),ISNULL(TotalPremiumT,'0'),ISNULL(TotalInsuredObjI,'0'),ISNULL(TotalSumInsuredI,'0'),ISNULL(TotalPremiumI,'0'),ISNULL(TotalInsuredObjE,'0'),ISNULL(TotalSumInsuredE,'0'),ISNULL(TotalPremiumE,'0')      
   from @TOTALINSURED,@INCLUDE,@EXCULDE   
            
  -- SELECT --cast(TotalInsuredObjT as int) + cast(TotalInsuredObjI as int)- cast(TotalInsuredObjE as int) as FinalInsuredObj,      
  -- dbo.fun_FormatCurrency(cast(ISNULL(TotalSumInsuredT,'0') as decimal(18,2)) + cast(ISNULL(TotalSumInsuredI,'0') as decimal(18,2))- cast(ISNULL(TotalSumInsuredE,'0') as decimal(18,2)),@LANG_ID) as FinalSumInsuredObj--,      
  ---- dbo.fun_FormatCurrency(cast(TotalPremiumT as decimal(10,5)) + cast(TotalPremiumI as decimal(10,5))- cast(TotalPremiumE as decimal(10,5)),@LANG_ID) as FinalPremiumInsuredObj,*      
  -- from @INCLUDEEXCULDE      
  -- FOR XML AUTO,ELEMENTS,ROOT('FinalInsured')   
    
    SELECT cast(TotalInsuredObjT as int) + cast(TotalInsuredObjI as int)- cast(TotalInsuredObjE as int) as FinalInsuredObj,      
   dbo.fun_FormatCurrency(cast(TotalSumInsuredT as decimal(18,2)) + cast(TotalSumInsuredI as decimal(18,2))- cast(TotalSumInsuredE as decimal(18,2)),@LANG_ID) as FinalSumInsuredObj,      
   dbo.fun_FormatCurrency(cast(TotalPremiumT as decimal(18,2)) + cast(TotalPremiumI as decimal(18,2))- cast(TotalPremiumE as decimal(18,2)),@LANG_ID) as FinalPremiumInsuredObj,*      
   from @INCLUDEEXCULDE      
   FOR XML AUTO,ELEMENTS,ROOT('FinalInsured')   
     
  -- select * from @INCLUDEEXCULDE  
    
    
    
    
End    
    
--------------------    
          
             
           
           
           
           
          
           END     --END National & international cargo transport                                 
 ELSE IF   (@LOBID in (17,18,28,29,31))   --For Facultative Liability AND Civil Liability Transportation            
            
           BEGIN          
                 
                 
  --      INSERT INTO @RISK(RISK_ID)          
  --SELECT DISTINCT RISK_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @COVERAGES(COVERAGE_CODE_ID)          
  --SELECT DISTINCT COVERAGE_CODE_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @RISK_COV (RISK_ID, COVERAGE_CODE_ID)          
  --SELECT RISK_ID, COVERAGE_CODE_ID          
  --FROM @RISK, @COVERAGES          
  --ORDER BY RISK_ID          
              
              
        SELECT * into #tempPOL_CIVIL_TRANSPORT_VEHICLES FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE 1 = 2      
      
        ALTER TABlE #tempPOL_CIVIL_TRANSPORT_VEHICLES      
        ADD ID INT IDENTITY(1,1)      
                 
          INSERT INTO #tempPOL_CIVIL_TRANSPORT_VEHICLES SELECT       
           @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID ,           
           TEMP.RISK_ID,       
           RISK_INFO.CLIENT_ORDER,      
     RISK_INFO.VEHICLE_NUMBER,      
     ISNULL(RISK_INFO.MANUFACTURED_YEAR,0),      
     RISK_INFO.FIPE_CODE,      
     RISK_INFO.CATEGORY,      
     RISK_INFO.CAPACITY,      
     RISK_INFO.MAKE_MODEL,      
     LICENSE_PLATE,      
     RISK_INFO.CHASSIS,      
     RISK_INFO.MANDATORY_DEDUCTIBLE,      
     RISK_INFO.FACULTATIVE_DEDUCTIBLE,      
     RISK_INFO.SUB_BRANCH,      
     ISNULL(RISK_INFO.RISK_EFFECTIVE_DATE,'01/01/1990'),      
     ISNULL(RISK_INFO.RISK_EXPIRE_DATE,'01/01/1990'),      
     RISK_INFO.REGION,      
     RISK_INFO.COV_GROUP_CODE,      
     RISK_INFO.FINANCE_ADJUSTMENT,      
     RISK_INFO.REFERENCE_PROPOSASL,      
     RISK_INFO.REMARKS,      
     ISNULL(RISK_INFO.IS_ACTIVE,'Y'),      
     ISNULL(RISK_INFO.CREATED_BY,0),      
     ISNULL(RISK_INFO.CREATED_DATETIME,GETDATE()),      
     RISK_INFO.MODIFIED_BY,      
     RISK_INFO.LAST_UPDATED_DATETIME,      
     RISK_INFO.VEHICLE_PLAN_ID,      
     RISK_INFO.VEHICLE_MAKE,      
     RISK_INFO.CO_APPLICANT_ID,      
     RISK_INFO.TICKET_NUMBER,      
     RISK_INFO.STATE_ID,      
     RISK_INFO.ZIP_CODE,      
     RISK_INFO.ORIGINAL_VERSION_ID  ,      
     CO_RISK_ID                  
    FROM          
   @RISK_COV TEMP           
   LEFT OUTER JOIN           
     POL_CIVIL_TRANSPORT_VEHICLES RISK_INFO  ON RISK_INFO.VEHICLE_ID= TEMP.RISK_ID          
     AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID          
                        
        ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID       
                 
                 
                 
              
    SELECT               
       RISKS.VEHICLE_ID as RISK_ID,            
       0 as LOCATION_ID,         
           
       ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,         
            
       ISNULL(RISKS.REMARKS,'') AS REMARKS,        
              
       ISNULL(RISKS.VEHICLE_NUMBER,0) AS VEHICLE_NUMBER  ,          
       ISNULL(RISKS.MAKE_MODEL,'')  AS MAKE_MODEL,        
       ISNULL(RISKS.MANUFACTURED_YEAR,0)  AS MANUFACTURED_YEAR,        
       ISNULL(RISKS.LICENSE_PLATE,'')  AS LICENSE_PLATE,        
       ISNULL(RISKS.CHASSIS,'')  AS CHASSIS,        
       ISNULL(RISKS.CLIENT_ORDER,0)  AS CLIENT_ORDER,        
       ISNULL(RISKS.CAPACITY,'')  AS CAPACITY,        
       ISNULL(RISKS.COV_GROUP_CODE,'') AS COV_GROUP_CODE,        
               
               
       dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,          
       dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,          
         
         0  AS LOCATION_NUMBER ,      
         ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'') AS APPLICABLE_LOCATIONS ,      
    (ISNULL(RISKS.MAKE_MODEL,'')+'-'            
     +ISNULL(CAST(RISKS.VEHICLE_NUMBER AS NVARCHAR(50)),''))  AS LOCATION ,                
       COVERAGE.IS_ACTIVE,      
   COVERAGE.COV_ID AS COVERAGE_CODE_ID,          
     ISNULL(MNT_COV_MULTI.COV_DES,COVERAGE.COV_DES) AS COV_DES    ,      
     COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,        
           
     ISNULL( dbo.fun_FormatCurrency(         
      case when IS_MAIN = '1' Then 0 else        
        
     (POL_COV.LIMIT_1/PSB.BASIC_SI)*100        
     END        
     ,@LANG_ID        
        
     )        
     ,0) as PsB,              
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,                  
     ISNULL( dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,            
        
             
        CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍ
N  
    
IMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIM
  
    
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')        
            
      end as DEDUCTIBLE_1,        
            
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,          
     COVERAGE.IS_MAIN AS IS_MAIN   ,      
    case when  COVERAGE.IS_MAIN=1      
     then 'Basica'      
     when COVERAGE.IS_MAIN=0      
     then  'Additional'      
     end as  IS_MAIN_Text,      
      ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT       
            
     FROM       
     #tempPOL_CIVIL_TRANSPORT_VEHICLES RISKS       
      
       JOIN           
                 
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)      
        ON                             
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.VEHICLE_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'        
        and POL_COV.ID = RISKS.ID       
              
              
          LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
                 
     LEFT OUTER JOIN                        
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
          
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.VEHICLE_ID = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
   LEFT OUTER JOIN                          
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
        MNT_COV_MULTI.LANG_ID = 2           
                  
     LEFT OUTER JOIN          (          
 SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
 SUM(WRITTEN_PREMIUM) AS RISK_PREMIUM          
 FROM  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
 WHERE           
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID =@POLICY_ID           
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
 ) RP          
 ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
 and RP.POLICY_ID=RISKS.POLICY_ID          
 and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
 and RP.RISK_ID = POL_COV.RISK_ID          
        
        
join           
   (         
 select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
)        
  PSB         
  on RISKS.VEHICLE_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
   WHERE                   
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID              
    ORDER BY RISK_ID ASC   FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')       
          
      SET @RESULT_COUNT  = @@ROWCOUNT        
          
               
  DROP TABLE #tempPOL_CIVIL_TRANSPORT_VEHICLES        
  SELECT dbo.fun_FormatCurrency(SUM(LIMIT_1),@LANG_ID) as Total_Sum_Insured, dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID) as Total_Written_Premium FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
  FOR XML AUTO,ELEMENTS,ROOT('SUMOFRISKS')      
               
           END     --END Facultative Liability and Civil Liability Transportation            
            
                       
 ELSE IF (@LOBID=22 )  --For Personal Accident for Passengers            
           BEGIN             
         
  -- INSERT INTO @RISK(RISK_ID)          
  --SELECT DISTINCT RISK_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @COVERAGES(COVERAGE_CODE_ID)          
  --SELECT DISTINCT COVERAGE_CODE_ID FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID          
            
  --INSERT INTO @RISK_COV (RISK_ID, COVERAGE_CODE_ID)          
  --SELECT RISK_ID, COVERAGE_CODE_ID          
  --FROM @RISK, @COVERAGES          
  --ORDER BY RISK_ID          
              
              
        SELECT * into #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE 1 = 2      
      
        ALTER TABlE #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO      
        ADD ID INT IDENTITY(1,1)      
                 
          INSERT INTO #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO SELECT       
            @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID ,           
            TEMP.RISK_ID,       
            RISK_INFO.START_DATE,      
   RISK_INFO.END_DATE,      
   ISNULL(RISK_INFO.NUMBER_OF_PASSENGERS,0),      
   RISK_INFO.IS_ACTIVE,      
   RISK_INFO.CREATED_BY,      
   RISK_INFO.CREATED_DATETIME,      
   RISK_INFO.MODIFIED_BY,      
   RISK_INFO.LAST_UPDATED_DATETIME,      
   RISK_INFO.CO_APPLICANT_ID,      
   RISK_INFO.ORIGINAL_VERSION_ID,      
   RISK_INFO.RISK_ORIGINAL_ENDORSEMENT_NO,      
   RISK_INFO.CO_RISK_ID                  
    FROM          
   @RISK_COV TEMP           
   LEFT OUTER JOIN           
     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK_INFO  ON RISK_INFO.PERSONAL_ACCIDENT_ID= TEMP.RISK_ID          
     AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID          
                       
        ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID       
         
         
         
         
   SELECT               
     RISKS.PERSONAL_ACCIDENT_ID as RISK_ID,            
      0 as LOCATION_ID,           
         
      ISNULL( dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,         
           
        '' as REMARKS,         
          COVERAGE.IS_ACTIVE,      
        
      dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,          
      dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,          
            
     (CONVERT(varchar(50),ISNULL(RISKS.START_DATE,''),101)+ ' To '+CONVERT(varchar(50),ISNULL(RISKS.END_DATE,''),101)+ ', # of Passengers '+CAST(ISNULL(RISKS.NUMBER_OF_PASSENGERS,0) AS NVARCHAR(50)))  AS LOCATION ,                        
      0 AS LOCATION_NUMBER ,        
      COVERAGE.COV_ID AS COVERAGE_CODE_ID,          
     case when @lANG_ID = 2          
     then  ISNULL(MNT_COV_MULTI.COV_DES,'')          
     else          
      COVERAGE.COV_DES          
     end          
     COV_DES,            
     COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,         
     ISNULL( dbo.fun_FormatCurrency(       
            
      case when BASIC_SI>0      
      then      
      CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100      
     THEN 0       
     ELSE      
     (POL_COV.LIMIT_1/PSB.BASIC_SI)*100      
     END      
     else      
     0      
     end      
           
           
     ,@LANG_ID      
           
           
           
           
     )      
           
           
     ,0) as PsB,          
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,                  
     ISNULL(POL_COV.MINIMUM_DEDUCTIBLE,0) MINIMUM_DEDUCTIBLE,            
        
             
       CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573        
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍN
  
    
IMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574        
                                                                                      -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))      
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIM
  
    
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575        
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576        
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')        
            
      end as DEDUCTIBLE_1,        
              
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,          
     COVERAGE.IS_MAIN AS IS_MAIN ,       
           
       case when  COVERAGE.IS_MAIN=1      
     then 'Basica'      
     when COVERAGE.IS_MAIN=0      
     then  'Additional'      
     end as  IS_MAIN_Text,      
      ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT       
            
     FROM       
     #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISKS       
      
       JOIN           
                 
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)      
        ON                             
     --FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISKS WITH(NOLOCK)                
          
     --  LEFT OUTER JOIN           
                 
     --  POL_PRODUCT_COVERAGES POL_COV WITH(NOLOCK) ON                    
       POL_COV.CUSTOMER_ID=RISKS.CUSTOMER_ID AND               
       POL_COV.POLICY_ID=RISKS.POLICY_ID AND              
       POL_COV.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID AND               
       POL_COV.RISK_ID=RISKS.PERSONAL_ACCIDENT_ID  AND          
       ISNULL(RISKS.IS_ACTIVE,'Y') = 'Y'       
        and POL_COV.ID = RISKS.ID         
              
             LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML ON       
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE      
                 
     LEFT OUTER JOIN                        
      MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON           
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID              
      LEFT OUTER JOIN                  
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON              
      RISKS.PERSONAL_ACCIDENT_ID = POL_LOCA.LOCATION_ID  AND            
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND            
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND            
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID            
      LEFT OUTER JOIN                          
      MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                    
      MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND            
      MNT_COV_MULTI.LANG_ID = 1           
            
            
     LEFT OUTER JOIN           
 (          
 SELECT CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,          
 SUM(WRITTEN_PREMIUM) AS RISK_PREMIUM          
 FROM  POL_PRODUCT_COVERAGES WITH(NOLOCK)          
 WHERE           
  CUSTOMER_ID = @CUSTOMER_ID           
  AND POLICY_ID =@POLICY_ID           
  AND POLICY_VERSION_ID = @POLICY_VERSION_ID           
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID          
 ) RP          
 ON RP.CUSTOMER_ID=RISKS.CUSTOMER_ID          
 and RP.POLICY_ID=RISKS.POLICY_ID          
 and RP.POLICY_VERSION_ID=RISKS.POLICY_VERSION_ID          
 and RP.RISK_ID = POL_COV.RISK_ID          
        
 join           
   (         
select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)        
    inner join POL_PRODUCT_COVERAGES p on k.COV_ID=p.COVERAGE_CODE_ID          
    WHERE        
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI      
    from POL_PRODUCT_COVERAGES l        
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID        
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID         
)        
  PSB         
  on RISKS.PERSONAL_ACCIDENT_ID=PSB.RISK_ID        
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID        
   WHERE                   
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID               
    ORDER BY RISK_ID ASC    FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')        
          
      SET @RESULT_COUNT  = @@ROWCOUNT        
      DROP TABLE #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO      
------------------------------          
declare @Pol_Status_22 varchar(20)          
              
    select @Pol_Status_22 = PROCESS_ID FROM POL_CUSTOMER_POLICY_LIST  POL  WITH(NOLOCK)                          
    LEFT OUTER JOIN POL_POLICY_PROCESS PPP ON PPP.CUSTOMER_ID=POL.CUSTOMER_ID                  
    AND PPP.POLICY_ID=POL.POLICY_ID                   
    AND PPP.NEW_POLICY_VERSION_ID=POL.POLICY_VERSION_ID      
    where POL.CUSTOMER_ID=@CUSTOMER_ID  AND POL.POLICY_ID=@POLICY_ID   AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID and PPP.PROCESS_STATUS <> 'ROLLBACK'        
    
if( @Pol_Status_22= 14 or @Pol_Status_22= 3)        
begin     
    
    
declare @POL_VERSION INT    
declare @MAX_RISK INT    
declare @ID INT    
select @POL_VERSION = MAX(POLICY_VERSION_ID) from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID    
select @MAX_RISK = MAX(RISK_ID) from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID    
    
SET @ID = 1    
--PRINT @POL_VERSION    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#TempA]') AND type in (N'U'))    
drop table #TempA    
select * into #TempA from    
(select * from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POL_VERSION - 1) A    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#TempB]') AND type in (N'U'))    
drop table #TempB    
select * into #TempB from     
(select * from POL_PRODUCT_COVERAGES     
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POL_VERSION) B    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#EXCLUDE]') AND type in (N'U'))    
DROP table #EXCLUDE    
select * into #EXCLUDE from    
(select * from POL_PRODUCT_COVERAGES    
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID) x    
    
Truncate table #EXCLUDE    
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[#INCLUDE]') AND type in (N'U'))    
DROP table #INCLUDE    
select * into #INCLUDE from    
(select * from POL_PRODUCT_COVERAGES    
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID) x    
    
Truncate table #INCLUDE    
    
while (@ID<@MAX_RISK + 1)    
BEGIN    
    
 INSERT INTO #EXCLUDE    
    
 select * from #TempA where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID =  @POL_VERSION - 1 and RISK_ID = @ID    
 and COVERAGE_CODE_ID not in    
 (select COVERAGE_CODE_ID from #TempB where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID = @POL_VERSION  and RISK_ID = @ID)    
     
    
 INSERT INTO #INCLUDE    
    
 select * from #TempB where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID =  @POL_VERSION and RISK_ID = @ID    
 and COVERAGE_CODE_ID not in    
 (select COVERAGE_CODE_ID from #TempA where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
 and POLICY_VERSION_ID = @POL_VERSION - 1  and RISK_ID = @ID)    
     
 SET @ID = @ID + 1    
END    
    
    
select count(*) as TotalInsuredObjT, dbo.fun_FormatCurrency(SUM(LIMIT_1),@LANG_ID) as TotalSumInsuredT,dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID) as TotalPremiumT from POL_PRODUCT_COVERAGES as TOTALINSUREDT where CUSTOMER_ID = @CUSTOMER_ID and 
  
POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID      
FOR XML AUTO,ELEMENTS,ROOT('TOTALINSURED')      
    
SELECT count(*) as TotalInsuredObjE,dbo.fun_FormatCurrency(SUM(CAST(LIMIT_1 as decimal(18,2))),@LANG_ID) as TotalSumInsuredE,dbo.fun_FormatCurrency(SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))),@LANG_ID) as TotalPremiumE FROM #EXCLUDE as EXCLUSOESOBJ     
FOR XML AUTO,ELEMENTS,ROOT('EXCLUSOES')     
    
SELECT count(*) as TotalInsuredObjI,dbo.fun_FormatCurrency(SUM(CAST(LIMIT_1 as decimal(18,2))),@LANG_ID) as TotalSumInsuredI,dbo.fun_FormatCurrency(SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))),@LANG_ID) as TotalPremiumI FROM #INCLUDE as INCLUSÕESOBJ    
FOR XML AUTO,ELEMENTS,ROOT('INCLUSÕES')      
    
insert into @TOTALINSURED(TotalInsuredObjT,TotalSumInsuredT,TotalPremiumT)      
  select count(*) as TotalInsuredObjT,SUM(LIMIT_1) as TotalSumInsuredT,SUM(WRITTEN_PREMIUM) as TotalPremiumT from POL_PRODUCT_COVERAGES as INCLUSÕESOBJ where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID 
  
    
     
           
  insert into @INCLUDE (TotalInsuredObjI,TotalSumInsuredI,TotalPremiumI)      
 SELECT count(*) as TotalInsuredObjI,SUM(CAST(LIMIT_1 as decimal(18,2))) as TotalSumInsuredI,SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))) as TotalPremiumI FROM #INCLUDE        
            
   insert into @EXCULDE  (TotalInsuredObjE,TotalSumInsuredE,TotalPremiumE)      
 SELECT count(*) as TotalInsuredObjE,SUM(CAST(LIMIT_1 as decimal(18,2))) as TotalSumInsuredE,SUM(CAST(WRITTEN_PREMIUM as decimal(18,2))) as TotalPremiumE FROM #EXCLUDE    
   --FOR XML AUTO,ELEMENTS,ROOT('EXCLUSOES')        
            
    insert into @INCLUDEEXCULDE(TotalInsuredObjT,TotalSumInsuredT,TotalPremiumT,TotalInsuredObjI,TotalSumInsuredI,TotalPremiumI,TotalInsuredObjE,TotalSumInsuredE,TotalPremiumE)      
   select ISNULL(TotalInsuredObjT,'0'),ISNULL(TotalSumInsuredT,'0'),ISNULL(TotalPremiumT,'0'),ISNULL(TotalInsuredObjI,'0'),ISNULL(TotalSumInsuredI,'0'),ISNULL(TotalPremiumI,'0'),ISNULL(TotalInsuredObjE,'0'),ISNULL(TotalSumInsuredE,'0'),ISNULL(TotalPremiumE,'0')      
   from @TOTALINSURED,@INCLUDE,@EXCULDE            
  
    
    SELECT cast(TotalInsuredObjT as int) + cast(TotalInsuredObjI as int)- cast(TotalInsuredObjE as int) as FinalInsuredObj,      
   dbo.fun_FormatCurrency(cast(TotalSumInsuredT as decimal(18,2)) + cast(TotalSumInsuredI as decimal(18,2))- cast(TotalSumInsuredE as decimal(18,2)),@LANG_ID) as FinalSumInsuredObj,      
   dbo.fun_FormatCurrency(cast(TotalPremiumT as decimal(18,2)) + cast(TotalPremiumI as decimal(18,2))- cast(TotalPremiumE as decimal(18,2)),@LANG_ID) as FinalPremiumInsuredObj,*      
   from @INCLUDEEXCULDE      
   FOR XML AUTO,ELEMENTS,ROOT('FinalInsured')   
     
  -- select * from @INCLUDEEXCULDE  
    
    
End    
    
--------------------    
    
    
          
           END     --END Personal Accident for Passengers            
                         
 END         
                         

GO

