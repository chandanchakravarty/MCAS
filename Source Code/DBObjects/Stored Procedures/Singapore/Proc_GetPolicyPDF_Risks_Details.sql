  
 /*                                                                              
 ----------------------------------------------------------                                                                                  
 Proc Name       : Proc_GetPolicyPDF_Risks_Details                                                                      
 Created by      : Naveen Pujari                                                                             
 Date            : january-3-2011                
 Purpose         : Selects records from POL_PRODUCT_COVERAGES  For Policy              
 Revison History :                                                                                  
 Used In         : EbixAdvantage       
 Modified by     :      
 Modification Date: january-3-2011      
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
  --DROP  Proc_GetPolicyPDF_Risks_Details 2885,575,2,9,2,0  
  Proc_GetPolicyPDF_Risks_Details 2885 ,561, 1 ,15,1,0  
  
 */         
 --drop proc [Proc_GetPolicyPDF_Risks_Details]   
 CREATE  PROC [dbo].[Proc_GetPolicyPDF_Risks_Details]   
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
  DECLARE @INCLUDE TABLE (TotalInsuredObjI NVARCHAR(20),TotalSumInsuredI NVARCHAR(20),TotalPremiumI NVARCHAR(20))        
  DECLARE @EXCULDE TABLE (TotalInsuredObjE NVARCHAR(20),TotalSumInsuredE NVARCHAR(20),TotalPremiumE NVARCHAR(20))        
  DECLARE @TOTALINSURED TABLE(TotalInsuredObjT NVARCHAR(20),TotalSumInsuredT NVARCHAR(20),TotalPremiumT NVARCHAR(20))        
  DECLARE @INCLUDEEXCULDE TABLE(TotalInsuredObjT NVARCHAR(20),TotalSumInsuredT NVARCHAR(20),TotalPremiumT NVARCHAR(20),TotalInsuredObjI NVARCHAR(20),TotalSumInsuredI NVARCHAR(20),TotalPremiumI NVARCHAR(20),TotalInsuredObjE NVARCHAR(20),TotalSumInsuredE NV
ARCHAR(20),TotalPremiumE NVARCHAR(20))        
  DECLARE @VALUE_AT_RISK1 TABLE (RISK_ID1 INT,VALUE_AT_RISK1 DECIMAL(18,2),CUSTOMER_ID1 INT,POLICY_ID1 INT,POLICY_VERSION_ID1 INT)  
  DECLARE @VALUE_AT_RISK2 TABLE (RISK_ID2 INT,VALUE_AT_RISK2 DECIMAL(18,2),CUSTOMER_ID2 INT,POLICY_ID2 INT,POLICY_VERSION_ID2 INT)  
  DECLARE @FINAL_VALUE_AT_RISK TABLE (RISK_ID INT,VALUE_AT_RISK1 DECIMAL(18,2),VALUE_AT_RISK2 DECIMAL(18,2),FINAL_VALUE_AT_RISK DECIMAL(18,2),CUSTOMER_ID1 INT,POLICY_ID1 INT,POLICY_VERSION_ID1 INT,CUSTOMER_ID2 INT,POLICY_ID2 INT,POLICY_VERSION_ID2 INT)  
  DECLARE @MAX_LIMIT_VALUE_AT_RISK TABLE(RISK_ID INT,VALUE_AT_RISK DECIMAL(18,2),MAX_LIMIT DECIMAL(18,2),ACTIVITY_TYPE int,REMARKS VARCHAR(4000),CLASS_FIELD int)  
  DECLARE @PROCESS_ID INT  
  DECLARE @CO_APPLICANT_ID INT  
  SELECT @PROCESS_ID = PROCESS_ID, @CO_APPLICANT_ID = CO_APPLICANT_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND NEW_POLICY_VERSION_ID  = @POLICY_VERSION_ID  
  AND PROCESS_STATUS <>'ROLLBACK'     
    
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
 FINAL_RATE DECIMAL(25,4),        
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
   
   
   
-- Added for testing by Rajan  
 --**************************** Code by Pawan for location and discount only chnage - Starts*********************************  
 --****** LOCATION changed **************  
    if (@PROCESS_ID in (3, 14) AND @LOBID IN (10,11,12,14,16,19,25,27,32))  
    BEGIN          
   insert into @POLICY_COVERAGE (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, RISK_ID, COVERAGE_ID, COVERAGE_CODE_ID)    
   select NEW.CUSTOMER_ID,NEW.POLICY_ID,NEW.POLICY_VERSION_ID,NEW.PRODUCT_RISK_ID,-1,-1 FROM   
  (  
   select loc.LOC_NUM,loc.LOC_ADD1,loc.LOC_ADD2,loc.LOC_CITY,loc.LOC_COUNTY,loc.LOC_STATE,loc.LOC_ZIP,loc.LOC_COUNTRY,loc.NAME,loc.NUMBER,loc.DISTRICT,loc.LOCATION_ID,  
   loc.CUSTOMER_ID,loc.POLICY_ID,loc.POLICY_VERSION_ID,risk.PRODUCT_RISK_ID  
   from POL_PRODUCT_LOCATION_INFO risk WITH(NOLOCK)   
   left outer join  POL_LOCATIONS loc WITH(NOLOCK)   
   on risk.CUSTOMER_ID=loc.CUSTOMER_ID and risk.POLICY_ID=loc.POLICY_ID and risk.POLICY_VERSION_ID=loc.POLICY_VERSION_ID and risk.LOCATION=loc.LOCATION_ID  
   where risk.CUSTOMER_ID = @CUSTOMER_ID and risk.POLICY_ID = @POLICY_ID and risk.Policy_version_id=@Policy_version_id  
  ) NEW  
  inner join   
  (  
   select loc.LOC_NUM,loc.LOC_ADD1,loc.LOC_ADD2,loc.LOC_CITY,loc.LOC_COUNTY,loc.LOC_STATE,loc.LOC_ZIP,loc.LOC_COUNTRY,loc.NAME,loc.NUMBER,loc.DISTRICT,loc.LOCATION_ID,  
   loc.CUSTOMER_ID,loc.POLICY_ID,loc.POLICY_VERSION_ID,risk.PRODUCT_RISK_ID  
   from POL_PRODUCT_LOCATION_INFO risk WITH(NOLOCK)   
   left outer join  POL_LOCATIONS loc WITH(NOLOCK)   
   on risk.CUSTOMER_ID=loc.CUSTOMER_ID and risk.POLICY_ID=loc.POLICY_ID and risk.POLICY_VERSION_ID=loc.POLICY_VERSION_ID and risk.LOCATION=loc.LOCATION_ID  
   where risk.CUSTOMER_ID = @CUSTOMER_ID and risk.POLICY_ID = @POLICY_ID and risk.Policy_version_id = @Policy_version_id-1  
  ) OLD  
  on new.CUSTOMER_ID = old.customer_id AND new.POLICY_ID=old.POLICY_ID and new.LOCATION_ID=old.LOCATION_ID  
  
  Where   
  (ISNULL(new.LOC_num,'') <> ISNULL(old.LOC_num,'') OR    
  ISNULL(new.LOC_ADD1,'') <> ISNULL(old.LOC_ADD1,'') OR    
  ISNULL(new.LOC_ADD2,'') <> ISNULL(old.LOC_ADD2,'') OR    
  ISNULL(new.NAME,'') <> ISNULL(old.name,'') OR   
  ISNULL(new.NUMBER,0) <> ISNULL(old.NUMBER,0) OR    
  ISNULL(new.DISTRICT,'') <> ISNULL(old.DISTRICT,'') OR    
  ISNULL(new.LOC_CITY ,'') <> ISNULL(old.LOC_CITY,'') OR    
  ISNULL(new.LOC_ZIP ,'') <> ISNULL(old.LOC_ZIP,'') OR    
  ISNULL(new.LOC_COUNTRY ,'') <> ISNULL(old.LOC_COUNTRY,'') OR   
  ISNULL(new.LOC_STATE,0) <> ISNULL(old.LOC_STATE,0))    
  AND NEW.PRODUCT_RISK_ID not in (select risk_id from @POLICY_COVERAGE)   
 END  
 --added by Pravesh on 25 June 11 for two more Product (0167,0196)  
  if (@PROCESS_ID in (3, 14) AND @LOBID IN (9,26))  
    BEGIN          
   insert into @POLICY_COVERAGE (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, RISK_ID, COVERAGE_ID, COVERAGE_CODE_ID)    
   select NEW.CUSTOMER_ID,NEW.POLICY_ID,NEW.POLICY_VERSION_ID,NEW.PERIL_ID,-1,-1 FROM   
  (  
   select loc.LOC_NUM,loc.LOC_ADD1,loc.LOC_ADD2,loc.LOC_CITY,loc.LOC_COUNTY,loc.LOC_STATE,loc.LOC_ZIP,loc.LOC_COUNTRY,loc.NAME,loc.NUMBER,loc.DISTRICT,loc.LOCATION_ID,  
   loc.CUSTOMER_ID,loc.POLICY_ID,loc.POLICY_VERSION_ID,risk.PERIL_ID  
   from POL_PERILS risk WITH(NOLOCK)   
   left outer join  POL_LOCATIONS loc WITH(NOLOCK)   
   on risk.CUSTOMER_ID=loc.CUSTOMER_ID and risk.POLICY_ID=loc.POLICY_ID and risk.POLICY_VERSION_ID=loc.POLICY_VERSION_ID and risk.LOCATION=loc.LOCATION_ID  
   where risk.CUSTOMER_ID = @CUSTOMER_ID and risk.POLICY_ID = @POLICY_ID and risk.Policy_version_id=@Policy_version_id  
  ) NEW  
  inner join   
  (  
   select loc.LOC_NUM,loc.LOC_ADD1,loc.LOC_ADD2,loc.LOC_CITY,loc.LOC_COUNTY,loc.LOC_STATE,loc.LOC_ZIP,loc.LOC_COUNTRY,loc.NAME,loc.NUMBER,loc.DISTRICT,loc.LOCATION_ID,  
   loc.CUSTOMER_ID,loc.POLICY_ID,loc.POLICY_VERSION_ID,risk.PERIL_ID  
   from POL_PERILS risk WITH(NOLOCK)   
   left outer join  POL_LOCATIONS loc WITH(NOLOCK)   
   on risk.CUSTOMER_ID=loc.CUSTOMER_ID and risk.POLICY_ID=loc.POLICY_ID and risk.POLICY_VERSION_ID=loc.POLICY_VERSION_ID and risk.LOCATION=loc.LOCATION_ID  
   where risk.CUSTOMER_ID = @CUSTOMER_ID and risk.POLICY_ID = @POLICY_ID and risk.Policy_version_id = @Policy_version_id-1  
  ) OLD  
  on new.CUSTOMER_ID = old.customer_id AND new.POLICY_ID=old.POLICY_ID and new.LOCATION_ID=old.LOCATION_ID  
  
  Where   
  (ISNULL(new.LOC_num,'') <> ISNULL(old.LOC_num,'') OR    
  ISNULL(new.LOC_ADD1,'') <> ISNULL(old.LOC_ADD1,'') OR    
  ISNULL(new.LOC_ADD2,'') <> ISNULL(old.LOC_ADD2,'') OR    
  ISNULL(new.NAME,'') <> ISNULL(old.name,'') OR   
  ISNULL(new.NUMBER,0) <> ISNULL(old.NUMBER,0) OR    
  ISNULL(new.DISTRICT,'') <> ISNULL(old.DISTRICT,'') OR    
  ISNULL(new.LOC_CITY ,'') <> ISNULL(old.LOC_CITY,'') OR    
  ISNULL(new.LOC_ZIP ,'') <> ISNULL(old.LOC_ZIP,'') OR    
  ISNULL(new.LOC_COUNTRY ,'') <> ISNULL(old.LOC_COUNTRY,'') OR   
  ISNULL(new.LOC_STATE,0) <> ISNULL(old.LOC_STATE,0))    
  AND NEW.PERIL_ID not in (select risk_id from @POLICY_COVERAGE)   
 END  
 -- added by pravesh end here  
 --****** DISCOUNTS - Changed or added **************  
 if (@PROCESS_ID in (3, 14))  
 BEGIN  
  insert into @POLICY_COVERAGE (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, RISK_ID, COVERAGE_ID, COVERAGE_CODE_ID)   
  select NEW.CUSTOMER_ID,NEW.POLICY_ID,NEW.POLICY_VERSION_ID,NEW.RISK_ID,-1,-1 from  
  (  
   select * from POL_DISCOUNT_SURCHARGE WITH(NOLOCK)  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID   
  ) new  
  left outer join   
  (  
   select * from POL_DISCOUNT_SURCHARGE WITH(NOLOCK)  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID-1   
  ) old  
  on new.CUSTOMER_ID = old.customer_id AND new.POLICY_ID=old.POLICY_ID and new.risk_id=old.risk_id and new.DISCOUNT_ID =old.DISCOUNT_ID  
  where old.DISCOUNT_ID is NULL  or new.PERCENTAGE<>old.PERCENTAGE  
  AND NEW.RISK_ID not in (select risk_id from @POLICY_COVERAGE)  
   END   
--**************************** Code by Pawan for location and discount only chnage - ENDS*********************************   
 -- End by Rajan  
   
   
   
 INSERT INTO @RISK_COV (RISK_ID, COVERAGE_CODE_ID)         
  SELECT RISK_ID,COVERAGE_CODE_ID FROM @POLICY_COVERAGE   
      
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
               
               
    
  INSERT INTO @MAX_LIMIT_VALUE_AT_RISK(RISK_ID,VALUE_AT_RISK,MAX_LIMIT,ACTIVITY_TYPE,REMARKS,CLASS_FIELD)  
  SELECT PERIL_ID,VR,LMI,ACTIVITY_TYPE,REMARKS,ACTIVITY_TYPE FROM POL_PERILS  WITH(NOLOCK)   
  WHERE CUSTOMER_ID = @CUSTOMER_ID   
  AND POLICY_ID = @POLICY_ID   
  AND POLICY_VERSION_ID =(select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock) where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <> 'ROLLBACK')  
         
                
                
        SELECT * into #tempPOL_PERILS FROM POL_PERILS WHERE 1=2        
        ALTER TABlE #tempPOL_PERILS        
        ADD ID INT IDENTITY(1,1)        
       --  select * from #tempPOL_PERILS         
        INSERT INTO #tempPOL_PERILS SELECT         
    @CUSTOMER_ID ,   
    @POLICY_ID ,   
    @POLICY_VERSION_ID ,             
    TEMP.RISK_ID,        
    RISK_INFO.CALCULATION_NUMBER,             
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
                         
   SELECT                 
      RISKS.PERIL_ID as RISK_ID,              
      ISNULL(RISKS.LOCATION,0) as LOCATION_ID,           
      ISNULL(RISKS.LOCATION_NUMBER,0) AS LOCATION_NUMBER,         
      ISNULL(RISKS.ITEM_NUMBER,0) AS ITEM_NUMBER,          
      ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID),0) as RISK_PREMIUM,            
      ISNULL(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,        
      dbo.fun_FormatCurrency(isnull(RISKS.VR,0),@LANG_ID) as VALUE_AT_RISK ,               
      dbo.fun_FormatCurrency(isnull(RISKS.LMI,0),@LANG_ID) AS MAXIMUM_LIMIT,          
      ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'') AS APPLICABLE_LOCATIONS ,           
      ISNULL(UPPER(MNT_ACT_MST.ACTIVITY_DESC),'')   AS  ACTIVITY_DESC   ,    
      isnull(RISKS.ACTUAL_INSURED_OBJECT,'') as ACTUAL_INSURED_OBJECT,   
      ISNULL(dbo.fun_FormatCurrency(PRE_RISK.VALUE_AT_RISK,@LANG_ID),0) AS PRE_VALUE_AT_RISK,  
      ISNULL(dbo.fun_FormatCurrency(PRE_RISK.MAX_LIMIT,@LANG_ID),0) AS PRE_MAX_LIMIT,   
      ISNULL(PRE_RISK.VALUE_AT_RISK,0) AS PRE_VALUE_AT_RISK_C,  
      ISNULL(PRE_RISK.MAX_LIMIT,0) AS PRE_MAX_LIMIT_C,  
      ISNULL(RISKS.VR,0) AS CURRENT_VALUE_AT_RISK_C,  
      ISNULL(RISKS.LMI,0) AS CURRENT_MAX_LIMIT_C,    
      ISNULL(PRE_RISK.ACTIVITY_TYPE,'') AS PRE_ACTIVITY_TYPE,  
      ISNULL(RISKS.ACTIVITY_TYPE,0) AS CURRENT_ACTIVITY_TYPE,  
      ISNULL(PRE_RISK.REMARKS,'') AS PRE_REMARKS,  
      ISNULL(RISKS.REMARKS,'') AS CURRENT_REMARKS,    
      (ISNULL(POL_LOCA.LOC_ADD1,'') +', '+        
     +ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'')  + ISNULL(' - '+LOC_ADD2,'')              
     +ISNULL(' - '+DISTRICT,'')           
     +ISNULL(' - '+POL_LOCA.LOC_CITY,'')+'/'+ ISNULL(MNT_CSL.STATE_CODE,''))  AS LOCATION,          
        
      COVERAGE.IS_ACTIVE,        
      Case POL_COV.COVERAGE_CODE_ID when -1  then -1 else COVERAGE.COV_ID end AS COVERAGE_CODE_ID,                 
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
      THEN  ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' ,'')        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575          
         THEN        
           CASE         
     WHEN (POL_COV.DEDUCTIBLE_1 IS NOT NULL AND POL_COV.DEDUCTIBLE_1>0) or (POL_COV.MINIMUM_DEDUCTIBLE IS NOT NULL AND POL_COV.MINIMUM_DEDUCTIBLE>0)        
     THEN    
     ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) AS NVARCHAR(10)) ,'')        
        ELSE   ''        
     END        
              
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10)),'')        
      else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'          
      end as DEDUCTIBLE_1,        
              
                 
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573          
      THEN  UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
                                                                                    -- RIGHT(REPLICATE('0', 8-LEN(ENDORSEMENT_NO))        
      THEN          
      UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
       WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN 'HORAS'        
      else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'          
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
                    
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV   WITH(NOLOCK)   ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML   WITH(NOLOCK)   ON         
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
                  
    
     LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT_CSL WITH(NOLOCK) ON        
     MNT_CSL.STATE_ID=POL_LOCA.LOC_STATE    AND MNT_CSL.COUNTRY_ID = POL_LOCA.LOC_COUNTRY            
           
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
  left outer join             
   (           
   select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)          
    inner join POL_PRODUCT_COVERAGES p   WITH(NOLOCK) on k.COV_ID=p.COVERAGE_CODE_ID            
    WHERE          
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI        
    from POL_PRODUCT_COVERAGES l          
  JOIn MNT_COVERAGE  a ON l.COVERAGE_CODE_ID = a.COV_ID          
  where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID =  @POLICY_VERSION_ID           
    )          
  PSB           
  on RISKS.PERIL_ID=PSB.RISK_ID          
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID       
    LEFT OUTER JOIN @MAX_LIMIT_VALUE_AT_RISK PRE_RISK  
  ON   RISKS.PERIL_ID=PRE_RISK.RISK_ID      
  WHERE                     
  RISKS.CUSTOMER_ID =  @CUSTOMER_ID             
  AND RISKS.POLICY_ID = @POLICY_ID          
  AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID             
  ORDER BY  RISK_ID, IS_MAIN desc ,COV_DES asc              
  FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')           
     SET @RESULT_COUNT  = @@ROWCOUNT        
                 
        DROP TABLE #tempPOL_PERILS        
          
        SELECT   
       
    ISNULL(CURRENT_VERSION.PERIL_ID, 0) AS RISK_ID,  
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
     
   FROM POL_PERILS  CURRENT_VERSION WITH(NOLOCK)   
    LEFT OUTER JOIN(  
    SELECT * FROM POL_PERILS  PREV_VERSION WITH(NOLOCK)   
    WHERE PREV_VERSION.CUSTOMER_ID  = @CUSTOMER_ID   
    AND PREV_VERSION.POLICY_ID = @POLICY_ID AND PREV_VERSION.POLICY_VERSION_ID = (select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock) where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION
_ID and PROCESS_STATUS <> 'ROLLBACK'))PREV_VERSION ON  
    PREV_VERSION.PERIL_ID = CURRENT_VERSION.PERIL_ID  
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
       
    JOIN MNT_COUNTRY_STATE_LIST MNT_CSL   WITH(NOLOCK)   ON MNT_CSL.COUNTRY_ID = CURRENT_LOCATION.LOC_COUNTRY AND CURRENT_LOCATION.LOC_STATE=MNT_CSL.STATE_ID   
      
    WHERE CURRENT_VERSION.CUSTOMER_ID  = @CUSTOMER_ID   
    AND CURRENT_VERSION.POLICY_ID = @POLICY_ID AND CURRENT_VERSION.POLICY_VERSION_ID = @POLICY_VERSION_ID  
     FOR XML AUTO,ELEMENTS,ROOT('RISK_LOCATION')  
                
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
        CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE   
               FROM POL_DISCOUNT_SURCHARGE DISCOUNT WITH(NOLOCK)   
      LEFT OUTER JOIN (  
      SELECT * FROM POL_DISCOUNT_SURCHARGE PREV_DISCOUNT WITH(NOLOCK) WHERE   
      PREV_DISCOUNT.CUSTOMER_ID  = @CUSTOMER_ID and PREV_DISCOUNT.POLICY_ID = @POLICY_ID and PREV_DISCOUNT.POLICY_VERSION_ID = (select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock)    where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLI
CY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK' )  
      ) PREV_DISCOUNT  
      ON   
      PREV_DISCOUNT.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND   
      PREV_DISCOUNT.POLICY_ID = DISCOUNT.POLICY_ID  
      AND   
      PREV_DISCOUNT.RISK_ID = DISCOUNT.RISK_ID  
      AND PREV_DISCOUNT.DISCOUNT_ID=DISCOUNT.DISCOUNT_ID  
        
              LEFT OUTER JOIN  POL_PRODUCT_LOCATION_INFO  RISK  WITH(NOLOCK)        
              ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID        
              LEFT OUTER  JOIN        
              MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR   WITH(NOLOCK) ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID        
              WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID        
              FOR XML AUTO,ELEMENTS,  ROOT('DISCOUNTS')   
    END  
   ELSE  
   BEGIN        
           
   select RISK_ID,ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,        
   CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE  from POL_DISCOUNT_SURCHARGE DISCOUNT   WITH(NOLOCK)   LEFT OUTER JOIN          
   POL_PRODUCT_LOCATION_INFO  RISK    WITH(NOLOCK)          
   ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID        
   LEFT OUTER  JOIN        
              
   MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR   WITH(NOLOCK)    ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID        
           
   WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID        
   FOR XML AUTO,ELEMENTS,ROOT('DISCOUNTS')         
    END  
      
                 
                 
   END  --end named Perils              
                 
   ELSE IF  (@LOBID IN (10,11,12,14,16,19,25,27,32))  --For Comprehensive Condominium ,Comprehensive Company ,General Civil Liability  ,Diversified Risks ,Robbery                 
   BEGIN          
                
       
  INSERT INTO @MAX_LIMIT_VALUE_AT_RISK(RISK_ID,VALUE_AT_RISK,MAX_LIMIT,ACTIVITY_TYPE,REMARKS,CLASS_FIELD)  
  SELECT PRODUCT_RISK_ID,VALUE_AT_RISK,MAXIMUM_LIMIT,ACTIVITY_TYPE,REMARKS,CLASS_FIELD FROM POL_PRODUCT_LOCATION_INFO   
  WHERE CUSTOMER_ID = @CUSTOMER_ID   
  AND POLICY_ID = @POLICY_ID   
  AND POLICY_VERSION_ID =(select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock) where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <> 'ROLLBACK')  
  
        SELECT * into #tempPOL_PRODUCT_LOCATION_INFO FROM POL_PRODUCT_LOCATION_INFO   WITH(NOLOCK)   WHERE 1=2        
        ALTER TABlE #tempPOL_PRODUCT_LOCATION_INFO        
        ADD ID INT IDENTITY(1,1)        
          
       INSERT INTO #tempPOL_PRODUCT_LOCATION_INFO SELECT          
           @CUSTOMER_ID , @POLICY_ID , @POLICY_VERSION_ID ,             
           TEMP.RISK_ID,          
           ISNULL(RISK_INFO.LOCATION,0), ---modified by Lalit June 27 2011.itrack # 1309.handel not null columns       
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
           ISNULL(RISK_INFO.IS_ACTIVE,'Y'),            
           ISNULL(RISK_INFO.CREATED_BY,0),        
           ISNULL(RISK_INFO.CREATED_DATETIME,GETDATE()),        
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
   POL_PRODUCT_LOCATION_INFO RISK_INFO    WITH(NOLOCK)  ON RISK_INFO.PRODUCT_RISK_ID= TEMP.RISK_ID            
   AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID            
          
       -- SELECT * FROM #tempRISK     
         
  SELECT            
     RISKS.PRODUCT_RISK_ID as RISK_ID,           
     ISNULL(RISKS.LOCATION,0) AS LOCATION_ID,         
     ISNULL(RISKS.LOCATION_NUMBER,0) AS LOCATION_NUMBER,         
     ISNULL(POL_LOCA.LOC_NUM ,'')AS  LOCATION_NUM,        
     ISNULL(RISKS.ITEM_NUMBER,0) AS ITEM_NUMBER,        
     ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID),0) as RISK_PREMIUM,              
     isnull(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,         
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
     ISNULL(RISKS.REMARKS,'') AS CURRENT_REMARKS,  
       ISNULL(PRE_RISK.CLASS_FIELD,'') AS PRE_CLASS_FIELD,  
       ISNULL(RISKS.CLASS_FIELD,'') AS CURRENT_CLASS_FIELD,  
      
    (ISNULL(POL_LOCA.LOC_ADD1,'') +', '+        
     +ISNULL(Convert(varchar(10), POL_LOCA.NUMBER),'')  + ISNULL(' - '+LOC_ADD2,'')              
     +ISNULL(' - '+DISTRICT,'')                
     +ISNULL(' - '+POL_LOCA.LOC_CITY,'')+'/'+ ISNULL(MNT_CSL.STATE_CODE,''))  AS LOCATION,    
      
         
             
      COVERAGE.IS_ACTIVE,              
     Case POL_COV.COVERAGE_CODE_ID when -1  then -1 else COVERAGE.COV_ID end AS COVERAGE_CODE_ID,         
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
      CASE WHEN  (POL_COV.LIMIT_1/PSB.BASIC_SI)*100 = 100 THEN 0  ELSE (POL_COV.LIMIT_1/PSB.BASIC_SI)*100  END        
      else  0 end        
     else        
     0        
     end        
             
             
     ,@LANG_ID        
        
     )       ,0) as PsB,        
             
           
     ISNULL(dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,           
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,              
     CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573          
    THEN  ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) +'%'       
   WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
           THEN  ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0)+'%'        
    WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575          
    THEN        
     CASE         
     WHEN (POL_COV.DEDUCTIBLE_1 IS NOT NULL AND POL_COV.DEDUCTIBLE_1>0) or (POL_COV.MINIMUM_DEDUCTIBLE IS NOT NULL AND POL_COV.MINIMUM_DEDUCTIBLE>0)        
     THEN    
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)        
        ELSE ''  
              
     END        
              
 WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10)),0)        
        else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'  
      end as DEDUCTIBLE_1,        
              
                 
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573          
      THEN  UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
                                                                                       
      THEN          
      UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
       WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN 'HORAS'        
      else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'    
              
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
             
         
             
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV   WITH(NOLOCK) ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML   WITH(NOLOCK)   ON           
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
             
    LEFT OUTER JOIN   MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LVM  WITH(NOLOCK) ON MNT_LVM.LOOKUP_UNIQUE_ID =RISKS.CLASS_FIELD        
           
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
     MNT_CSL.COUNTRY_ID = POL_LOCA.LOC_COUNTRY AND MNT_CSL.STATE_ID=POL_LOCA.LOC_STATE        
               
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
          
Left outer join             
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
   ORDER BY  RISK_ID, IS_MAIN desc ,COV_DES asc        
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
           CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE   
               FROM POL_DISCOUNT_SURCHARGE DISCOUNT WITH(NOLOCK)   
      LEFT OUTER JOIN (  
      SELECT * FROM POL_DISCOUNT_SURCHARGE PREV_DISCOUNT WITH(NOLOCK) WHERE   
      PREV_DISCOUNT.CUSTOMER_ID  = @CUSTOMER_ID and PREV_DISCOUNT.POLICY_ID = @POLICY_ID and PREV_DISCOUNT.POLICY_VERSION_ID = (select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock)    where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLI
CY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK' )  
      ) PREV_DISCOUNT  
      ON   
      PREV_DISCOUNT.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND   
      PREV_DISCOUNT.POLICY_ID = DISCOUNT.POLICY_ID  
      AND   
      --PREV_DISCOUNT.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND  
      PREV_DISCOUNT.RISK_ID = DISCOUNT.RISK_ID  
      AND PREV_DISCOUNT.DISCOUNT_ID=DISCOUNT.DISCOUNT_ID  
        
              LEFT OUTER JOIN  POL_PRODUCT_LOCATION_INFO  RISK   WITH(NOLOCK)          
              ON RISK.CUSTOMER_ID = DISCOUNT.CUSTOMER_ID AND RISK.POLICY_ID = DISCOUNT.POLICY_ID AND RISK.POLICY_VERSION_ID = DISCOUNT.POLICY_VERSION_ID AND RISK.PRODUCT_RISK_ID = DISCOUNT.RISK_ID        
              LEFT OUTER  JOIN        
              MNT_DISCOUNT_SURCHARGE MNT_DIS_SUR  ON DISCOUNT.DISCOUNT_ID=MNT_DIS_SUR.DISCOUNT_ID        
              WHERE DISCOUNT.CUSTOMER_ID = @CUSTOMER_ID AND DISCOUNT.POLICY_ID = @POLICY_ID AND DISCOUNT.POLICY_VERSION_ID = @POLICY_VERSION_ID        
              FOR XML AUTO,ELEMENTS,  ROOT('DISCOUNTS')   
    END  
   ELSE  
   BEGIN        
           
   select RISK_ID,ISNULL(MNT_DIS_SUR.DISCOUNT_DESCRIPTION,'DISCOUNT') AS DISCOUNT_DESCRIPTION,        
   CAST(dbo.fun_FormatCurrency(DISCOUNT.PERCENTAGE,@LANG_ID) as nvarchar(50)) +'%' AS PERCENTAGE   
   from POL_DISCOUNT_SURCHARGE DISCOUNT  WITH(NOLOCK)  LEFT OUTER JOIN          
   POL_PRODUCT_LOCATION_INFO  RISK   WITH(NOLOCK)         
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
    AND PREV_VERSION.POLICY_ID = @POLICY_ID AND PREV_VERSION.POLICY_VERSION_ID = (select POLICY_VERSION_ID from POL_POLICY_PROCESS with(nolock) where NEW_CUSTOMER_ID = @CUSTOMER_ID and NEW_POLICY_ID = @POLICY_ID and NEW_POLICY_VERSION_ID = @POLICY_VERSION
_ID and PROCESS_STATUS <> 'ROLLBACK'))PREV_VERSION ON  
    PREV_VERSION.PRODUCT_RISK_ID = CURRENT_VERSION.PRODUCT_RISK_ID  
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
       
    JOIN MNT_COUNTRY_STATE_LIST MNT_CSL ON MNT_CSL.COUNTRY_ID = CURRENT_LOCATION.LOC_COUNTRY AND CURRENT_LOCATION.LOC_STATE=MNT_CSL.STATE_ID   
      
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
            
      isnull(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,         
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
                
                     
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV  with(nolock) ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML  with(nolock) ON         
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
    inner join POL_PRODUCT_COVERAGES p with(nolock) on k.COV_ID=p.COVERAGE_CODE_ID            
WHERE          
    CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_MAIN = 1 and p.RISK_ID= l.RISK_ID) BASIC_SI            from POL_PRODUCT_COVERAGES l          
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
             
      isnull(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,            
                
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
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'          ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍN
IMO DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')          
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574                                                                                               
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIM
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')          
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
               
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV with(nolock) ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML with(nolock) ON         
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
    inner join POL_PRODUCT_COVERAGES p with(nolock) on k.COV_ID=p.COVERAGE_CODE_ID            
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
     ISNULL(RISK_INFO.DATE_OF_BIRTH,''),        
     ISNULL(RISK_INFO.GENDER,0),        
     ISNULL(RISK_INFO.REG_IDEN,0),        
     ISNULL(RISK_INFO.REG_ID_ISSUES,''),        
     ISNULL(RISK_INFO.REG_ID_ORG,0),        
     ISNULL(RISK_INFO.REMARKS,''),        
     ISNULL(RISK_INFO.IS_ACTIVE,0),        
     ISNULL(RISK_INFO.CREATED_BY,0),        
     ISNULL(RISK_INFO.CREATED_DATETIME,''),        
     ISNULL(RISK_INFO.MODIFIED_BY,0),        
     ISNULL(RISK_INFO.LAST_UPDATED_DATETIME,''),        
     ISNULL(RISK_INFO.IS_SPOUSE_OR_CHILD,0),        
     ISNULL(RISK_INFO.MAIN_INSURED,0),        
     ISNULL(RISK_INFO.CITY_OF_BIRTH ,''),        
     ISNULL(RISK_INFO.ORIGINAL_VERSION_ID,0),        
     ISNULL(RISK_INFO.CO_RISK_ID,0),  
     ISNULL(RISK_INFO.MARITAL_STATUS,'')      
            
           
                     
    FROM            
       @RISK_COV TEMP             
                          
      LEFT OUTER JOIN             
   POL_PERSONAL_ACCIDENT_INFO RISK_INFO with(nolock) ON RISK_INFO.PERSONAL_INFO_ID= TEMP.RISK_ID            
   AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID            
                        
   ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID         
               
  SELECT                 
     RISKS.PERSONAL_INFO_ID  as RISK_ID,              
  0 as LOCATION_ID,         
  ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,           
  isnull(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,         
  dbo.fun_FormatCurrency(0,@LANG_ID) as VALUE_AT_RISK ,            
        dbo.fun_FormatCurrency(0,@LANG_ID) AS MAXIMUM_LIMIT,            
  0 AS LOCATION_NUMBER ,  
     ISNULL(RISKS.INDIVIDUAL_NAME,'') AS INDIVIDUAL_NAME,  
  RISKS.CITY_OF_BIRTH + '/' + MCSL.STATE_CODE + ' ' + '-' + ' ' + convert(varchar(8),RISKS.DATE_OF_BIRTH,3) as City_State_DOB,  
  ISNULL(MAM.ACTIVITY_DESC,'') as Position,  
  ISNULL(RISKS.CPF_NUM,'') AS CPF,  
  case when MLVM.LOOKUP_VALUE_DESC != '' and MLVM1.LOOKUP_VALUE_DESC = '' then isnull(MLVM.LOOKUP_VALUE_DESC,'')  
  when MLVM1.LOOKUP_VALUE_DESC != '' and MLVM.LOOKUP_VALUE_DESC = '' then isnull(MLVM1.LOOKUP_VALUE_DESC,'') else  
  isnull(MLVM.LOOKUP_VALUE_DESC,'') + ' ' + '-' + ' ' + isnull(MLVM1.LOOKUP_VALUE_DESC,'') end AS MARITAL_GENDER,  
  isnull((MLVM1.LOOKUP_VALUE_DESC),'') AS GENDER,  
  ISNULL(cal.IS_PRIMARY_APPLICANT,0) as IsApplicant,   
  COVERAGE.IS_ACTIVE,          
  COVERAGE.COV_ID AS COVERAGE_CODE_ID,  
  case when @lANG_ID = 2            
  then  ISNULL(MNT_COV_MULTI.COV_DES,'')            
  else            
  COVERAGE.COV_DES            
  end  as          
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
     ISNULL( dbo.fun_FormatCurrency(POL_COV.LIMIT_1,@LANG_ID),0) LIMIT_1,          
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0) MINIMUM_DEDUCTIBLE,    
     ISNULL(dbo.fun_FormatCurrency(POL_COV.WRITTEN_PREMIUM,@LANG_ID),0) COV_PREMIUM,            
          
        CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573          
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'        ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIM
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')          
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
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
     ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT,  
     ISNULL(dbo.fun_FormatCurrency_Final_Rate(POL_COV.FINAL_RATE,@LANG_ID),0) AS FINAL_RATE  
     FROM         
     #tempPOL_PERSONAL_ACCIDENT_INFO RISKS         
     LEFT OUTER JOIN CLT_APPLICANT_LIST CAL with(nolock) ON RISKS.CUSTOMER_ID = CAL.CUSTOMER_ID AND RISKS.APPLICANT_ID = CAL.APPLICANT_ID    
     left outer join MNT_COUNTRY_STATE_LIST MCSL  with(nolock) on  RISKS.STATE_ID = MCSL.STATE_ID    
     left outer join MNT_ACTIVITY_MASTER MAM  with(nolock) on  RISKS.POSITION_ID = MAM.ACTIVITY_ID   
     left outer join MNT_LOOKUP_VALUES MLV with(nolock) on RISKS.MARITAL_STATUS = MLV.LOOKUP_VALUE_CODE  and MLV.LOOKUP_ID = 721  
     left outer join MNT_LOOKUP_VALUES MLV1  with(nolock) on RISKS.GENDER  = MLV1.LOOKUP_UNIQUE_ID and MLV1.LOOKUP_ID = 846  
     left outer join MNT_LOOKUP_VALUES_MULTILINGUAL  MLVM with(nolock) on  MLVM.LOOKUP_UNIQUE_ID = MLV.LOOKUP_UNIQUE_ID and MLVM.LANG_ID = @LANG_ID   
     left outer join MNT_LOOKUP_VALUES_MULTILINGUAL  MLVM1 with(nolock) on MLVM1.LOOKUP_UNIQUE_ID = MLV1.LOOKUP_UNIQUE_ID and MLVM1.LANG_ID = @LANG_ID    
       
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
        MNT_COV_MULTI.LANG_ID = @LANG_ID              
               
               
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
                 
 left outer join             
   (           
 select l.RISK_ID,l.COVERAGE_CODE_ID,a.COV_DES,(select ISNULL(MAX(p.LIMIT_1),0) from MNT_COVERAGE  k with(nolock)          
    inner join POL_PRODUCT_COVERAGES p with(nolock) on k.COV_ID=p.COVERAGE_CODE_ID            
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
      
    select PB.BENEFICIARY_NAME,PB.BENEFICIARY_SHARE,PB.BENEFICIARY_RELATION,PB.RISK_ID  from POL_BENEFICIARY   PB with(nolock)  
    where PB.CUSTOMER_ID = @CUSTOMER_ID  and PB.POLICY_ID = @POLICY_ID  and PB.POLICY_VERSION_ID = @POLICY_VERSION_ID     
  FOR XML AUTO,ELEMENTS,ROOT('BENEFICIARIOS')    
    
  SELECT dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID)as total_Premium_of_risk,RISK_ID  FROM POL_PRODUCT_COVERAGES  with(nolock) where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID group by RISK_ID
            
     FOR XML AUTO,ELEMENTS,ROOT('SUMOFRISKS')   
            
    ------------------------------            
IF( @PROCESS_ID= 14 or @PROCESS_ID= 3)          
BEGIN       
    
   
 -- Written By Rajan and Pawan, Last Month Case  
  
  select   
      --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  case when CAST(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT.RISK_ID ELSE NULL END)as varchar)='0' then ''  
  else  
   CAST(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT.RISK_ID ELSE NULL END)as varchar)  
  end as TotalInsuredObjTAPC,   
  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT.LIMIT_1 ELSE NULL END),@LANG_ID), '') as TotalSumInsuredTAPC,  
  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT.WRITTEN_PREMIUM ELSE NULL END),@LANG_ID), '') as TotalPremiumTAPC,  
  case when CAST(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993')  THEN TOTALINSUREDT.RISK_ID ELSE NULL END) as varchar) ='0' then ''  
  else  
  CAST(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT.RISK_ID ELSE NULL END) as varchar)  
  end as TotalInsuredObjTVG,   
  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT.LIMIT_1 ELSE NULL END),@LANG_ID), '') as TotalSumInsuredTVG,  
  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT.WRITTEN_PREMIUM ELSE NULL END),@LANG_ID), '') as TotalPremiumTVG  
  from POL_PRODUCT_COVERAGES TOTALINSUREDT with(nolock)  
  INNER JOIN POL_PERSONAL_ACCIDENT_INFO PPAI  with(nolock) ON  
  TOTALINSUREDT.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  TOTALINSUREDT.POLICY_ID = PPAI.POLICY_ID and  
  TOTALINSUREDT.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  TOTALINSUREDT.RISK_ID = PPAI.PERSONAL_INFO_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL  with(nolock) ON TOTALINSUREDT.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM  with(nolock) ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK) ON PCPL.CUSTOMER_ID=TOTALINSUREDT.CUSTOMER_ID AND PCPL.POLICY_ID=TOTALINSUREDT.POLICY_ID AND PCPL.CUSTOMER_ID=TOTALINSUREDT.CUSTOMER_ID  
  where TOTALINSUREDT.CUSTOMER_ID = @CUSTOMER_ID and TOTALINSUREDT.POLICY_ID = @POLICY_ID and TOTALINSUREDT.POLICY_VERSION_ID = (select POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND N
EW_POLICY_VERSION_ID  = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK') and   
  PPAI.APPLICANT_ID = @CO_APPLICANT_ID and PPAI.IS_ACTIVE = 'Y'  
  FOR XML AUTO,ELEMENTS,ROOT('TOTALINSURED')     
    
   
 -- Written By Rajan and Pawan, Exclusion Case  
  select   
   --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN EXCLUSOESOBJ.RISK_ID ELSE NULL END)as varchar) ='0' then ''  
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN EXCLUSOESOBJ.RISK_ID ELSE NULL END)as varchar)  
  end as TotalInsuredObjEAPC,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN EXCLUSOESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredEAPC,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN EXCLUSOESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumEAPC,  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN EXCLUSOESOBJ.RISK_ID  ELSE NULL END) as varchar) ='0' then ''  
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN EXCLUSOESOBJ.RISK_ID  ELSE NULL END) as varchar)  
  end as TotalInsuredObjEVG,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN EXCLUSOESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredEVG,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN EXCLUSOESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumEVG  
  from POL_PRODUCT_COVERAGES EXCLUSOESOBJ with(nolock)  
  INNER JOIN POL_PERSONAL_ACCIDENT_INFO PPAI with(nolock) ON  
  EXCLUSOESOBJ.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  EXCLUSOESOBJ.POLICY_ID = PPAI.POLICY_ID and  
  EXCLUSOESOBJ.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  EXCLUSOESOBJ.RISK_ID = PPAI.PERSONAL_INFO_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL ON EXCLUSOESOBJ.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST   PCPL   WITH(NOLOCK) ON  PCPL.CUSTOMER_ID=EXCLUSOESOBJ.CUSTOMER_ID AND PCPL.POLICY_ID=EXCLUSOESOBJ.POLICY_ID AND PCPL.POLICY_VERSION_ID=EXCLUSOESOBJ.POLICY_VERSION_ID   
      where EXCLUSOESOBJ.CUSTOMER_ID = @CUSTOMER_ID and EXCLUSOESOBJ.POLICY_ID = @POLICY_ID and EXCLUSOESOBJ.POLICY_VERSION_ID = (select POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID =@POLICY_ID AND 
NEW_POLICY_VERSION_ID  = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK') and   
  PPAI.APPLICANT_ID = @CO_APPLICANT_ID and PPAI.IS_ACTIVE = 'Y'  
  and RISK_ID in (  
      select PERSONAL_INFO_ID from POL_PERSONAL_ACCIDENT_INFO  with(nolock) where CUSTOMER_ID = @CUSTOMER_ID and policy_id = @POLICY_ID   
      and POLICY_VERSION_ID = @POLICY_VERSION_ID and IS_ACTIVE <> 'Y' and APPLICANT_ID = @CO_APPLICANT_ID  
  )  
    FOR XML AUTO,ELEMENTS,ROOT('EXCLUSOES')  
  
  
    
 -- Written By Rajan, Inclusion Case -- modified by Pravesh  
  select   
   --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  case   
   when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END)as varchar) ='0' then ''   
   else cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END)as varchar)  
  end as TotalInsuredObjIAPC,   
    
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUSÕESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredIAPC,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUSÕESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumIAPC,  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END) as varchar)='0' then ''  
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END) as varchar)  
  end  as TotalInsuredObjIVG,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUSÕESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredIVG,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUSÕESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumIVG  
  from POL_PRODUCT_COVERAGES INCLUSÕESOBJ with(nolock)  
  INNER JOIN POL_PERSONAL_ACCIDENT_INFO PPAI with(nolock) ON  
  INCLUSÕESOBJ.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  INCLUSÕESOBJ.POLICY_ID = PPAI.POLICY_ID and  
  INCLUSÕESOBJ.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  INCLUSÕESOBJ.RISK_ID = PPAI.PERSONAL_INFO_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL with(nolock)ON INCLUSÕESOBJ.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM  with(nolock) ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST   PCPL   WITH(NOLOCK) ON  PCPL.CUSTOMER_ID=INCLUSÕESOBJ.CUSTOMER_ID AND PCPL.POLICY_ID=INCLUSÕESOBJ.POLICY_ID AND PCPL.POLICY_VERSION_ID=INCLUSÕESOBJ.POLICY_VERSION_ID   
  where INCLUSÕESOBJ.CUSTOMER_ID = @CUSTOMER_ID and INCLUSÕESOBJ.POLICY_ID = @POLICY_ID and INCLUSÕESOBJ.POLICY_VERSION_ID = @POLICY_VERSION_ID and   
  PPAI.APPLICANT_ID = @CO_APPLICANT_ID AND PPAI.IS_ACTIVE = 'Y'  
  and RISK_ID not in (  
  select RISK_ID from POL_PRODUCT_COVERAGES PPCIN with(nolock) where PPCIN.CUSTOMER_ID = @CUSTOMER_ID and PPCIN.POLICY_ID = @POLICY_ID and PPCIN.POLICY_VERSION_ID = (select POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOM
ER_ID AND POLICY_ID =@POLICY_ID AND NEW_POLICY_VERSION_ID  = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK')  
  )  
    FOR XML AUTO,ELEMENTS,ROOT('INCLUSÕES')  
         
         
    -- Written By Rajan, Inclusion Case - modified by Pravesh  
      
  select   
   --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUDEEXCULDE.RISK_ID  ELSE NULL END)as varchar) ='0' then ''  
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUDEEXCULDE.RISK_ID  ELSE NULL END)as varchar)   
  end as FinalInsuredObjAPC,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUDEEXCULDE.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as FinalSumInsuredObjAPC,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE NOT in ('0993') THEN INCLUDEEXCULDE.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as FinalPremiumInsuredObjAPC,  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUDEEXCULDE.RISK_ID  ELSE NULL END) as varchar) ='0' then ''   
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUDEEXCULDE.RISK_ID  ELSE NULL END) as varchar)  
  end  
  as FinalInsuredObjVG,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUDEEXCULDE.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as FinalSumInsuredObjVG,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  in ('0993') THEN INCLUDEEXCULDE.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as FinalPremiumInsuredObjVG  
  from POL_PRODUCT_COVERAGES  as INCLUDEEXCULDE  with(nolock)  
  INNER JOIN POL_PERSONAL_ACCIDENT_INFO PPAI  with(nolock) ON  
  INCLUDEEXCULDE.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  INCLUDEEXCULDE.POLICY_ID = PPAI.POLICY_ID and  
  INCLUDEEXCULDE.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  INCLUDEEXCULDE.RISK_ID = PPAI.PERSONAL_INFO_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL ON INCLUDEEXCULDE.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST   PCPL   WITH(NOLOCK) ON  PCPL.CUSTOMER_ID=INCLUDEEXCULDE.CUSTOMER_ID AND PCPL.POLICY_ID=INCLUDEEXCULDE.POLICY_ID AND PCPL.POLICY_VERSION_ID=INCLUDEEXCULDE.POLICY_VERSION_ID  
  where INCLUDEEXCULDE.CUSTOMER_ID = @CUSTOMER_ID and INCLUDEEXCULDE.POLICY_ID = @POLICY_ID and INCLUDEEXCULDE.POLICY_VERSION_ID = (@POLICY_VERSION_ID) and   
  PPAI.APPLICANT_ID = @CO_APPLICANT_ID and PPAI.IS_ACTIVE = 'Y'  
  FOR XML AUTO,ELEMENTS,ROOT('FinalInsured')     
    
  --Added by Abhishek Goel iTrack 1236 on dated 30/06/2010  
    
  select   
   --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  '' as  TotalInsuredObjTAPC,   
  case when sum(TOTALINSUREDT_CURRENT.LIMIT_1) > sum(TOTALINSUREDT_PREVIOUS.LIMIT_1) then '' else ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT_CURRENT.LIMIT_1 ELSE NULL END) - SUM(CASE WHEN PCPL.SUSE
P_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT_PREVIOUS.LIMIT_1 ELSE NULL END),@LANG_ID), '')  end as TotalSumInsuredTAPC,  
  case when sum(TOTALINSUREDT_CURRENT.WRITTEN_PREMIUM) > sum(TOTALINSUREDT_PREVIOUS.WRITTEN_PREMIUM) then '' else  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT_CURRENT.WRITTEN_PREMIUM ELSE NULL END)-
 SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT in ('0993')  THEN TOTALINSUREDT_PREVIOUS.WRITTEN_PREMIUM ELSE NULL END),@LANG_ID), '') end as TotalPremiumTAPC,  
  '' as TotalInsuredObjTVG,   
  case when sum(TOTALINSUREDT_CURRENT.LIMIT_1) > sum(TOTALINSUREDT_PREVIOUS.LIMIT_1) then '' else  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT_CURRENT.LIMIT_1 ELSE NULL END) - SUM(CASE WHEN PCPL.SUSEP_LOB
_CODE in ('0993') THEN TOTALINSUREDT_PREVIOUS.LIMIT_1 ELSE NULL END),@LANG_ID), '') end as TotalSumInsuredTVG,  
  case when sum(TOTALINSUREDT_CURRENT.WRITTEN_PREMIUM) > sum(TOTALINSUREDT_PREVIOUS.WRITTEN_PREMIUM) then '' else  ISNULL(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT_CURRENT.WRITTEN_PREMIUM ELSE NULL END) - SUM(
CASE WHEN PCPL.SUSEP_LOB_CODE in ('0993') THEN TOTALINSUREDT_PREVIOUS.WRITTEN_PREMIUM ELSE NULL END),@LANG_ID), '') end as TotalPremiumTVG  
  from POL_PRODUCT_COVERAGES TOTALINSUREDT_CURRENT WITH(NOLOCK)  
  JOIN  
  (  
  select   
  *  
  from POL_PRODUCT_COVERAGES TOTALINSUREDT_PREVIOUS WITH(NOLOCK)  
  where TOTALINSUREDT_PREVIOUS.CUSTOMER_ID = @CUSTOMER_ID  and TOTALINSUREDT_PREVIOUS.POLICY_ID = @POLICY_ID  and TOTALINSUREDT_PREVIOUS.POLICY_VERSION_ID = (@POLICY_VERSION_ID - 1)  
  ) TOTALINSUREDT_PREVIOUS  
  ON  
  TOTALINSUREDT_CURRENT.CUSTOMER_ID = TOTALINSUREDT_PREVIOUS.CUSTOMER_ID AND  
  TOTALINSUREDT_CURRENT.POLICY_ID = TOTALINSUREDT_PREVIOUS.POLICY_ID AND  
  --TOTALINSUREDT_CURRENT.POLICY_VERSION_ID = TOTALINSUREDT_PREVIOUS.POLICY_VERSION_ID AND   
  TOTALINSUREDT_CURRENT.RISK_ID = TOTALINSUREDT_PREVIOUS.RISK_ID AND   
  TOTALINSUREDT_CURRENT.COVERAGE_ID = TOTALINSUREDT_PREVIOUS.COVERAGE_ID  
  
  INNER JOIN POL_PERSONAL_ACCIDENT_INFO PPAI ON  
  TOTALINSUREDT_CURRENT.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  TOTALINSUREDT_CURRENT.POLICY_ID = PPAI.POLICY_ID and  
  TOTALINSUREDT_CURRENT.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  TOTALINSUREDT_CURRENT.RISK_ID = PPAI.PERSONAL_INFO_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL  with(nolock) ON TOTALINSUREDT_CURRENT.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM  with(nolock) ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST   PCPL   WITH(NOLOCK) ON  PCPL.CUSTOMER_ID=TOTALINSUREDT_PREVIOUS.CUSTOMER_ID AND PCPL.POLICY_ID=TOTALINSUREDT_PREVIOUS.POLICY_ID AND PCPL.POLICY_VERSION_ID=TOTALINSUREDT_PREVIOUS.POLICY_VERSION_ID     
  where TOTALINSUREDT_CURRENT.CUSTOMER_ID = @CUSTOMER_ID and TOTALINSUREDT_CURRENT.POLICY_ID = @POLICY_ID and TOTALINSUREDT_CURRENT.POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  PPAI.APPLICANT_ID = @CO_APPLICANT_ID and PPAI.IS_ACTIVE = 'Y'  
  
  FOR XML AUTO,ELEMENTS,ROOT('TOTALINSUREDUCED')     
        
          
End      
    --- IF finishes for LOB ID 34 and Endorsement   
--------------------      
            
 END     --END National & international cargo transport                                   
 ELSE IF   (@LOBID in (17,18,28,29,31))   --For Facultative Liability AND Civil Liability Transportation              
              
           BEGIN            
                                
                
        SELECT * into #tempPOL_CIVIL_TRANSPORT_VEHICLES FROM POL_CIVIL_TRANSPORT_VEHICLES WHERE 1 = 2        
        
        ALTER TABlE #tempPOL_CIVIL_TRANSPORT_VEHICLES        
        ADD ID INT IDENTITY(1,1)        
                   
          INSERT INTO #tempPOL_CIVIL_TRANSPORT_VEHICLES SELECT         
             @CUSTOMER_ID ,   
             @POLICY_ID ,  
             @POLICY_VERSION_ID ,             
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
     POL_CIVIL_TRANSPORT_VEHICLES RISK_INFO  with(nolock) ON RISK_INFO.VEHICLE_ID= TEMP.RISK_ID            
     AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID            
                          
        ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID         
                   
                   
                   
                
    SELECT                 
       RISKS.VEHICLE_ID as RISK_ID,              
       0 as LOCATION_ID,           
       ISNULL(dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,           
       ISNULL(REPLACE(RISKS.REMARKS,char(13),'~'),'')  AS REMARKS ,              
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
       (ISNULL(RISKS.MAKE_MODEL,'')+'-'+ISNULL(CAST(RISKS.VEHICLE_NUMBER AS NVARCHAR(50)),''))  AS LOCATION ,                  
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
    THEN  ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) +'%'       
   WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
             THEN  ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0)+'%'        
    WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575          
    THEN        
     CASE         
     WHEN (POL_COV.DEDUCTIBLE_1 IS NOT NULL AND POL_COV.DEDUCTIBLE_1>0) or (POL_COV.MINIMUM_DEDUCTIBLE IS NOT NULL AND POL_COV.MINIMUM_DEDUCTIBLE>0)        
     THEN    
     ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)        
        ELSE ''  
              
     END        
              
  WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10)),0)        
        else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'  
      end as DEDUCTIBLE_1,        
              
                 
      CASE WHEN POL_COV.DEDUCTIBLE_1_TYPE=14573          
      THEN  UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DOS')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14574          
      THEN          
      UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIMO DE' +' '+'R$ '+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50))        
       WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN 'HORAS'        
      else dbo.fun_FormatCurrency('0',@LANG_ID) + '%'    
              
      end as DEDUCTIBLE_1_Text,          
              
     ISNULL(POL_COV.DEDUCTIBLE2_AMOUNT_TEXT,'') AS DEDUCTIBLE2_AMOUNT_TEXT,        
        
         
  CASE     
  WHEN ISNULL(POL_COV.INDEMNITY_PERIOD,0) > 0 THEN  CAST(ISNULL('PI= '+ CAST(POL_COV.INDEMNITY_PERIOD AS VARCHAR(20)) ,'')AS VARCHAR(20) )    
  ELSE '' END AS INDEMNITY_PERIOD ,         
     COVERAGE.IS_MAIN AS IS_MAIN   ,        
    case when  COVERAGE.IS_MAIN=1        
     then 'Básica'        
     when COVERAGE.IS_MAIN=0        
     then  'Adicional'        
     end as  IS_MAIN_Text,        
      ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT,  
      ISNULL(MCAL.ACC_LOB_ID,0) AS ACC_LOB_ID,  
      ISNULL(COVERAGE.FORM_NUMBER,'') AS FORMNUMBER,  
      ISNULL(COVERAGE.CARRIER_COV_CODE,'') AS CARRIER_COV_CODE       
              
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
                
                
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV with(nolock) ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML  with(nolock) ON         
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
                   
     LEFT OUTER JOIN                          
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON             
            
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID     
      LEFT OUTER JOIN MNT_COVERAGE_ACCOUNTING_LOB MCAL  
      ON MCAL.COV_ID = COVERAGE.COV_ID   
      LEFT OUTER JOIN MNT_LOB_MASTER MLM  
      ON MLM.LOB_ID = COVERAGE.LOB_ID              
      LEFT OUTER JOIN                    
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON                
      RISKS.VEHICLE_ID = POL_LOCA.LOCATION_ID  AND              
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND              
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND              
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID              
   LEFT OUTER JOIN                            
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                      
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND              
        MNT_COV_MULTI.LANG_ID = @LANG_ID            
                    
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
    inner join POL_PRODUCT_COVERAGES p with(nolock) on k.COV_ID=p.COVERAGE_CODE_ID            
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
        
            SELECT                 
       ISNULL(RISKS.COV_GROUP_CODE,'') AS COV_GROUP_CODE,          
        COVERAGE.COV_ID AS COVERAGE_CODE_ID,            
        ISNULL(MNT_COV_MULTI.COV_DES,COVERAGE.COV_DES) AS COV_DES    ,        
        COVERAGE.COMPONENT_CODE AS COMPONENT_CODE,           
     COVERAGE.IS_MAIN AS IS_MAIN   ,        
    case when  COVERAGE.IS_MAIN=1        
     then 'Básica'        
     when COVERAGE.IS_MAIN=0        
     then  'Adicional'        
     end as  IS_MAIN_Text,        
      ISNULL(MCAL.ACC_LOB_ID,0) AS ACC_LOB_ID,  
      ISNULL(COVERAGE.FORM_NUMBER,'') AS FORMNUMBER,  
      ISNULL(COVERAGE.CARRIER_COV_CODE,'') AS CARRIER_COV_CODE,  
      isnull(RISKS.VEHICLE_ID,'') as RISK_ID       
              
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
                
                
     LEFT OUTER JOIN MNT_LOOKUP_VALUES MNT_LV with(nolock) ON MNT_LV.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
     LEFT OUTER JOIN  MNT_LOOKUP_VALUES_MULTILINGUAL MNT_LML  with(nolock) ON         
     MNT_LML.LOOKUP_UNIQUE_ID=POL_COV.DEDUCTIBLE_1_TYPE        
                   
     LEFT OUTER JOIN                          
     MNT_COVERAGE COVERAGE   WITH(NOLOCK) ON             
            
      POL_COV.COVERAGE_CODE_ID=COVERAGE.COV_ID     
      LEFT OUTER JOIN MNT_COVERAGE_ACCOUNTING_LOB MCAL  
      ON MCAL.COV_ID = COVERAGE.COV_ID   
      LEFT OUTER JOIN MNT_LOB_MASTER MLM  
      ON MLM.LOB_ID = COVERAGE.LOB_ID              
      LEFT OUTER JOIN                    
      POL_LOCATIONS POL_LOCA WITH(NOLOCK) ON                
      RISKS.VEHICLE_ID = POL_LOCA.LOCATION_ID  AND              
      RISKS.CUSTOMER_ID = POL_LOCA.CUSTOMER_ID AND              
      RISKS.POLICY_ID = POL_LOCA.POLICY_ID AND              
      RISKS.POLICY_VERSION_ID = POL_LOCA.POLICY_VERSION_ID              
   LEFT OUTER JOIN                            
        MNT_COVERAGE_MULTILINGUAL MNT_COV_MULTI WITH(NOLOCK) ON                      
        MNT_COV_MULTI.COV_ID = COVERAGE.COV_ID AND              
        MNT_COV_MULTI.LANG_ID = @LANG_ID            
                    
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
    inner join POL_PRODUCT_COVERAGES p with(nolock) on k.COV_ID=p.COVERAGE_CODE_ID            
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
    ORDER BY RISK_ID ASC   FOR XML AUTO,ELEMENTS,ROOT('COVERAGES')         
            
                 
  DROP TABLE #tempPOL_CIVIL_TRANSPORT_VEHICLES          
  SELECT dbo.fun_FormatCurrency(SUM(LIMIT_1),@LANG_ID) as Total_Sum_Insured, dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID) as Total_Written_Premium FROM POL_PRODUCT_COVERAGES where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_V
ERSION_ID =@POLICY_VERSION_ID            
  FOR XML AUTO,ELEMENTS,ROOT('SUMOFRISKS')        
                 
           END     --END Facultative Liability and Civil Liability Transportation              
              
                         
 ELSE IF (@LOBID=22 )  --For Personal Accident for Passengers              
 BEGIN               
           
        SELECT * into #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WHERE 1 = 2        
        
        ALTER TABlE #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO        
        ADD ID INT IDENTITY(1,1)        
                   
          INSERT INTO #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO SELECT         
           @CUSTOMER_ID,   
           @POLICY_ID ,   
           @POLICY_VERSION_ID ,             
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
     POL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISK_INFO with(nolock) ON RISK_INFO.PERSONAL_ACCIDENT_ID= TEMP.RISK_ID            
     AND RISK_INFO.CUSTOMER_ID = @CUSTOMER_ID AND RISK_INFO.POLICY_ID = @POLICY_ID AND RISK_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID            
     ORDER BY TEMP.RISK_ID ASc, TEMP.COVERAGE_CODE_ID          
          
   SELECT                 
     RISKS.PERSONAL_ACCIDENT_ID as RISK_ID,              
      0 as LOCATION_ID,             
           
      ISNULL( dbo.fun_FormatCurrency(RISK_PREMIUM,@LANG_ID) , 0 )as RISK_PREMIUM,    
      ISNULL(RISKS.NUMBER_OF_PASSENGERS,0) as NUMBER_OF_PASSENGERS ,              
             
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
                                                                                       
      THEN  ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))+'%' +'         ', 15-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +UPPER(REPLACE(MNT_LML.LOOKUP_VALUE_DESC,'%','DA')) +', '+'MÍNIM
  
    
      
O DE' +' '+'R$'+''+ CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.MINIMUM_DEDUCTIBLE,@LANG_ID),0)  AS NVARCHAR(50)),'')          
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14575          
      THEN  'R$'+ ''+ ISNULL(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS  NVARCHAR(50)) ,'')          
      WHEN POL_COV.DEDUCTIBLE_1_TYPE=14576          
      THEN ISNULL(LEFT(CAST(ISNULL(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID),0) AS NVARCHAR(10))  +'            ', 17-LEN(dbo.fun_FormatCurrency(POL_COV.DEDUCTIBLE_1,@LANG_ID))) +'HORAS','')          
              
      end as DEDUCTIBLE_1,          
                
     ISNULL(POL_COV.INDEMNITY_PERIOD,0)AS INDEMNITY_PERIOD ,            
     COVERAGE.IS_MAIN AS IS_MAIN ,         
             
     case when  COVERAGE.IS_MAIN=@LANG_ID       
     then 'Basica'        
     when COVERAGE.IS_MAIN=0        
     then  'Additional'        
     end as  IS_MAIN_Text,        
      ISNULL(dbo.fun_FormatCurrency(POL_COV.PREV_LIMIT,@LANG_ID),0)  AS PREV_LIMIT    ,  
      ISNULL(dbo.fun_FormatCurrency(POL_COV.WRITTEN_PREMIUM,@LANG_ID),0)  AS COVERAGE_PREMIUM  
       
              
     FROM         
     #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO RISKS         
        
       JOIN             
                   
       @POLICY_COVERAGE POL_COV --WITH(NOLOCK)        
        ON                               
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
  on RISKS.PERSONAL_ACCIDENT_ID=PSB.RISK_ID          
  and  COVERAGE.COV_ID =PSB.COVERAGE_CODE_ID          
   WHERE                     
    RISKS.CUSTOMER_ID = @CUSTOMER_ID AND RISKS.POLICY_ID = @POLICY_ID AND RISKS.POLICY_VERSION_ID = @POLICY_VERSION_ID                 
    ORDER BY RISK_ID ASC     
    FOR XML AUTO,ELEMENTS,ROOT('RISKINFO')    
       
      
            
      SET @RESULT_COUNT  = @@ROWCOUNT          
      DROP TABLE #tempPOL_PASSENGERS_PERSONAL_ACCIDENT_INFO    
        
    SELECT dbo.fun_FormatCurrency(SUM(WRITTEN_PREMIUM),@LANG_ID)as total_Premium_of_risk,RISK_ID  FROM POL_PRODUCT_COVERAGES with(nolock)  where CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID group by RISK_
ID            
     FOR XML AUTO,ELEMENTS,ROOT('SUMOFRISKS')             
------------------------------            
      
if( @PROCESS_ID= 14 or @PROCESS_ID= 3)          
begin       
          
 -- Written By Rajan, Inclusion Case -- modified by Pravesh  
  --did changes to fetch SUSEP_LOB_CODE from  policy, tfs id 427  
  select   
  case when cast(SUM(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE IN ('0520','0982') THEN PPAI.NUMBER_OF_PASSENGERS ELSE NULL END)as varchar) ='0' then ''   
  else  
   cast(SUM(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE IN ('0520','0982') THEN PPAI.NUMBER_OF_PASSENGERS  ELSE NULL END)as varchar)  
  end  as TotalInsuredObjIAPC,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE IN ('0520','0982') THEN INCLUSÕESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredIAPC,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE IN ('0520','0982') THEN INCLUSÕESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumIAPC,  
  case when cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  NOT IN ('0520','0982') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END) as varchar) ='0' then ''  
  else  
  cast(COUNT(DISTINCT CASE WHEN PCPL.SUSEP_LOB_CODE  NOT IN ('0520','0982') THEN INCLUSÕESOBJ.RISK_ID  ELSE NULL END) as varchar)  
  end  
   as TotalInsuredObjIVG,   
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT IN ('0520','0982') THEN INCLUSÕESOBJ.LIMIT_1  ELSE NULL END),@LANG_ID), '')  as TotalSumInsuredIVG,  
  isnull(dbo.fun_FormatCurrency(SUM(CASE WHEN PCPL.SUSEP_LOB_CODE  NOT IN ('0520','0982') THEN INCLUSÕESOBJ.WRITTEN_PREMIUM  ELSE NULL END),@LANG_ID), '')  as TotalPremiumIVG  
  from POL_PRODUCT_COVERAGES INCLUSÕESOBJ  
  INNER JOIN POL_PASSENGERS_PERSONAL_ACCIDENT_INFO PPAI ON  
  INCLUSÕESOBJ.CUSTOMER_ID = PPAI.CUSTOMER_ID and  
  INCLUSÕESOBJ.POLICY_ID = PPAI.POLICY_ID and  
  INCLUSÕESOBJ.POLICY_VERSION_ID = PPAI.POLICY_VERSION_ID and  
  INCLUSÕESOBJ.RISK_ID = PPAI.PERSONAL_ACCIDENT_ID  
  LEFT OUTER JOIN [MNT_COVERAGE_ACCOUNTING_LOB ] MCAL WITH(NOLOCK) ON INCLUSÕESOBJ.COVERAGE_CODE_ID = MCAL.COV_ID   
  LEFT OUTER JOIN MNT_LOB_MASTER MLM  WITH(NOLOCK) ON MCAL.ACC_LOB_ID = MLM.LOB_ID  
  LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST   PCPL   WITH(NOLOCK) ON  PCPL.CUSTOMER_ID=INCLUSÕESOBJ.CUSTOMER_ID AND PCPL.POLICY_ID=INCLUSÕESOBJ.POLICY_ID AND PCPL.POLICY_VERSION_ID=INCLUSÕESOBJ.POLICY_VERSION_ID  
  where INCLUSÕESOBJ.CUSTOMER_ID = @CUSTOMER_ID and INCLUSÕESOBJ.POLICY_ID = @POLICY_ID and INCLUSÕESOBJ.POLICY_VERSION_ID = @POLICY_VERSION_ID and   
  PPAI.CO_APPLICANT_ID = @CO_APPLICANT_ID and PPAI.IS_ACTIVE = 'Y'  
  and RISK_ID not in (  
  select RISK_ID from POL_PRODUCT_COVERAGES PPCIN WITH(NOLOCK) where PPCIN.CUSTOMER_ID = @CUSTOMER_ID and PPCIN.POLICY_ID = @POLICY_ID and PPCIN.POLICY_VERSION_ID = (select POLICY_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOM
ER_ID AND POLICY_ID =@POLICY_ID AND NEW_POLICY_VERSION_ID  = @POLICY_VERSION_ID AND PROCESS_STATUS <>'ROLLBACK')  
  )  
    FOR XML AUTO,ELEMENTS,ROOT('INCLUSÕES')  
  
    
End      
      
--------------------      
      
      
            
           END        
                           
 END           
                           
  
--EXEC  [Proc_GetPolicyPDF_Risks_Details] 28025 ,171, 3,11,2 ,0   
  