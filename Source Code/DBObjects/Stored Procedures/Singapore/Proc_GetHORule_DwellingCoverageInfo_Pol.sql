 /* ----------------------------------------------------------                                                                                            
Proc Name                : Dbo.Proc_GetHORule_DwellingCoverageInfo_Pol 1374,8,1,1,'W001'                                                                                     
Created by               : Ashwani                                                                                            
Date                     : 02 Mar 2006                                          
Purpose                  : To get the dwelling coverage detail for HO policy rules                                            
Revison History          :                                                                                            
Used In                  : Wolverine                                                                                            
------------------------------------------------------------                                                                                            
Date     Review By          Comments                                                                                            
------   ------------       -------------------------*/      
--Proc_GetHORule_DwellingCoverageInfo_Pol 1426,28,1,1,'w001'                                                                                            
-- DROP PROC dbo.Proc_GetHORule_DwellingCoverageInfo_Pol                                    
alter  proc dbo.Proc_GetHORule_DwellingCoverageInfo_Pol                                      
(                                                                                            
@CUSTOMER_ID    int,                                                                                            
@POLICY_ID    int,                                                                                            
@POLICY_VERSION_ID   int,                                            
@DWELLING_ID int ,      
@USER varchar(50)   ---Added to Check the Wolv User                                                        
)                                                                                            
AS                                                                                                
BEGIN                                               
 -- Mandatory                                              
 declare @DECDWELLING_LIMIT decimal                                          
 declare @DWELLING_LIMIT char  -- HO-6                                         
 declare @PERSONAL_LIAB_LIMIT varchar(50)                                          
 declare @MED_PAY_EACH_PERSON varchar(50)                                        
 --Rule                                       
 declare @DECPERSONAL_PROP_LIMIT decimal -- HO-6                                      
 declare @PERSONAL_PROP_LIMIT char                                      
 declare @COVAC_HO6 char --HO-6                             
 declare @INTREPLACEMENT_COST int             
                            
                                          
--------------                  
-- Policy Type                                      
                                      
 DECLARE @INTPOLICY_TYPE INT     
 DECLARE @DATE_APP_EFFECTIVE_DATE DATETIME --Added by Charles on 18-Dec-09 for Itrack 6681  
 DECLARE @STATE_ID SMALLINT --Added by Charles on 18-Dec-09 for Itrack 6681                                   
                                      
 SELECT @INTPOLICY_TYPE=ISNULL(POLICY_TYPE,0),  
 @DATE_APP_EFFECTIVE_DATE= CAST(CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS DATETIME), @STATE_ID=ISNULL(STATE_ID,0) --@DATE_APP_EFFECTIVE_DATE, @STATE_ID Added by Charles on 18-Dec-09 for Itrack 6681                                                         
                                              
 FROM POL_CUSTOMER_POLICY_LIST    with(nolock)                                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                    
                  
--------------                                           
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES                                                               
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID)                                            
 BEGIN                                             
 -------------------------------------------------------------------                                  
---------DWELL                                  
Select  @DECDWELLING_LIMIT = (CASE  WHEN M.COV_CODE ='DWELL' THEN ISNULL(COV_INFO.LIMIT_1,-1) end)                                  
from POL_DWELLING_SECTION_COVERAGES COV_INFO                     
INNER join mnt_coverage M on m.cov_id = cov_info.coverage_code_id                
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                       
 AND (COV_INFO.POLICY_ID = @POLICY_ID)                                                   
 AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)          
 AND (COV_INFO.DWELLING_ID = @DWELLING_ID) where M.COV_CODE ='DWELL'                                  
---END DWELL                                       
                                  
                                  
                                  
                                  
---------PL                                  
--SELECT  @PERSONAL_LIAB_LIMIT = (CASE  WHEN M.COV_CODE ='PL' THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')  END)                                  
--FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                                  
--INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                  
-- AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                  
--    AND (COV_INFO.POLICY_ID = @POLICY_ID)                                   
--    AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                   
--    AND (COV_INFO.DWELLING_ID = @DWELLING_ID) WHERE M.COV_CODE ='PL'        
      
SELECT  @PERSONAL_LIAB_LIMIT = (CASE  WHEN COV_INFO.LIMIT_1 is NOT NULL       
  THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')       
  ELSE ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT1_AMOUNT_TEXT),'')  END)      
FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                              
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                              
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                  
 AND (COV_INFO.POLICY_ID = @POLICY_ID)                                   
 AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                   
 AND (COV_INFO.DWELLING_ID = @DWELLING_ID)      
  WHERE M.COV_CODE ='PL'                                 
------END PL                                        
                                  
---------MEDPM                                  
--SELECT  @MED_PAY_EACH_PERSON = (CASE  WHEN M.COV_CODE ='MEDPM' THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')  END)                                  
--FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                                  
--INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                  
-- AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                  
--    AND (COV_INFO.POLICY_ID = @POLICY_ID)                                                   
--    AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                   
--    AND (COV_INFO.DWELLING_ID = @DWELLING_ID) WHERE M.COV_CODE ='MEDPM'                                  
------END MEDPM                                  
      
SELECT  @MED_PAY_EACH_PERSON = (CASE  WHEN COV_INFO.LIMIT_1 is NOT NULL       
  THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')       
  ELSE ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT1_AMOUNT_TEXT),'')  END)      
FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                              
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                              
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                  
 AND (COV_INFO.POLICY_ID = @POLICY_ID)                                                   
 AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                   
 AND (COV_INFO.DWELLING_ID = @DWELLING_ID)      
WHERE M.COV_CODE ='MEDPM'          
---------EBUSPP COVERAGE C - UNSCHEDULED PERSONAL PROPERTY 2800000                                  
                                  
SELECT  @DECPERSONAL_PROP_LIMIT = (CASE  WHEN M.COV_CODE ='EBUSPP' THEN ISNULL(COV_INFO.LIMIT_1,-1) END)                                  
FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                                  
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                  
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                  
 AND (COV_INFO.POLICY_ID = @POLICY_ID)                                                   
 AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                   
 AND (COV_INFO.DWELLING_ID = @DWELLING_ID) WHERE M.COV_CODE ='EBUSPP'                                  
                                  
------END MEDPM                       
                  
----FOR POLICY TYPE HO4(11405 AND 11406) AND HO6(11195 AND 11196) THE MAIN COVERAGE IS C:                   
IF(@INTPOLICY_TYPE=11405 OR @INTPOLICY_TYPE=11406 OR @INTPOLICY_TYPE =11195 OR @INTPOLICY_TYPE=11196)                  
    set @DECDWELLING_LIMIT = @DECPERSONAL_PROP_LIMIT       
-------------------------------------------------------------------                                  
END                                                          
ELSE                                                          
BEGIN                                                           
 SET @DWELLING_LIMIT =''                                                          
 SET @PERSONAL_LIAB_LIMIT =''                                                          
 SET @MED_PAY_EACH_PERSON =''                                                     
 SET @DECDWELLING_LIMIT=-1                               
END                                                           
                                                    
-- Policy Type                     
---------------------------------------------------------------------------------                          
                          
SELECT @INTREPLACEMENT_COST=ISNULL(REPLACEMENT_COST,0)                                      
FROM POL_DWELLINGS_INFO                                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID                           
      
IF(@INTPOLICY_TYPE=11192 OR @INTPOLICY_TYPE=11148 OR @INTPOLICY_TYPE=11400 OR @INTPOLICY_TYPE=11402)                     
--IF(@INTPOLICY_TYPE=11193 OR @INTPOLICY_TYPE=11194 OR @INTPOLICY_TYPE=11403 OR @INTPOLICY_TYPE=11404)-- HO-2 & HO-3                                      
BEGIN                                       
 DECLARE @INTCOVERAGE_A_HO2_HO3 INT                                      
 DECLARE @REPLACEMENT_COST_POLICY_HO2_HO3 CHAR       
                                       
 SET @INTCOVERAGE_A_HO2_HO3 =(0.8*@INTREPLACEMENT_COST)                                                  
                                        
 IF (@DECDWELLING_LIMIT < @INTCOVERAGE_A_HO2_HO3)                                                      
  BEGIN                                                                     
  SET @REPLACEMENT_COST_POLICY_HO2_HO3='Y'                                                                    
  END      
 ELSE                                                                    
  BEGIN                                          
  SET @REPLACEMENT_COST_POLICY_HO2_HO3='N'                                                                    
  END                                                        
END                                       
ELSE                                      
BEGIN                                       
SET @REPLACEMENT_COST_POLICY_HO2_HO3='N'        
END                                       
--       
                                   
 --HO -5 only                                       
DECLARE @REPLACEMENT_COST_POLICY_HO5 CHAR                                      
                               
IF(@INTPOLICY_TYPE=11149 OR @INTPOLICY_TYPE=11401)                                      
BEGIN                                      
 IF (@DECDWELLING_LIMIT < @INTREPLACEMENT_COST )                                                                    
  BEGIN                                                                     
  SET @REPLACEMENT_COST_POLICY_HO5='Y'                                                                    
  END                                                                     
 ELSE                                                                    
  BEGIN                                                                     
  SET @REPLACEMENT_COST_POLICY_HO5='N'                                                                    
  END                                       
END                                       
ELSE                                      
BEGIN                                       
SET @REPLACEMENT_COST_POLICY_HO5='N'                                       
END                                             
                                      
                                      
                                       
-- HO-6                                      
IF(@INTPOLICY_TYPE=11196 OR @INTPOLICY_TYPE=11406)                                       
BEGIN                                       
 DECLARE @INTMINCOVC DECIMAL                                      
 -- FOR HO6 (10% OF COVG C)                                                     
 SET @INTMINCOVC=(0.1 * @DECPERSONAL_PROP_LIMIT)                                    
                          
  IF(@DECDWELLING_LIMIT< @INTMINCOVC OR @DECDWELLING_LIMIT<2000)                                            
   BEGIN                                                     
   SET @COVAC_HO6='Y'                                                    
   END                                              
  ELSE                                                    
   BEGIN                                                     
   SET @COVAC_HO6='N'                                                    
   END                                          
END                                      
ELSE                                       
BEGIN                                       
SET  @COVAC_HO6='N'                                      
SET @PERSONAL_PROP_LIMIT='N'                                      
END                                          
                                      
--=======================================================                                      
 IF(@DECPERSONAL_PROP_LIMIT=-1)                                            
 BEGIN                                       
 SET @PERSONAL_PROP_LIMIT=''                                      
 END                                       
 ELSE                                      
 BEGIN                                       
 SET @PERSONAL_PROP_LIMIT='N'                                      
 END                                       
--======================================================      
                  
IF(@DECDWELLING_LIMIT > 400000)                                          
 BEGIN                                           
 SET @DWELLING_LIMIT='Y'                                          
 END                 
ELSE IF(@DECDWELLING_LIMIT=-1)                                         
 BEGIN                                           
 SET @DWELLING_LIMIT=''                                          
 END                                          
ELSE IF(@DECDWELLING_LIMIT<>0)                                         
BEGIN                                           
SET @DWELLING_LIMIT='N'                                          
END                                          
-------------------------------------------------------------------------------------------------------------------                                          
--Coverage A in case of HO2, 3, 5 regular and premier cannot be greater than 100% of Replacement Cost                              
-- 11192,11402  -- HO-2, 11148,11400,11409 -- HO-3, 11149,11401,11410 -- HO-5         
DECLARE @COVA_NOT_REPCOST CHAR                 
                  
IF(@INTPOLICY_TYPE=11192 OR @INTPOLICY_TYPE=11402 OR @INTPOLICY_TYPE=11148 OR @INTPOLICY_TYPE=11400 OR @INTPOLICY_TYPE=11149 OR @INTPOLICY_TYPE=11401 OR @INTPOLICY_TYPE=11409 OR @INTPOLICY_TYPE=11410)                                       
BEGIN                               
 IF(@DECDWELLING_LIMIT>@INTREPLACEMENT_COST)                              
  BEGIN                               
  SET @COVA_NOT_REPCOST='Y'                              
  END                               
 ELSE                           
  BEGIN                               
  SET @COVA_NOT_REPCOST='N'                              
  END                               
END                     
ELSE                              
BEGIN                               
SET @COVA_NOT_REPCOST='N'                              
END                               
--                              
IF(@COVA_NOT_REPCOST='Y')                              
BEGIN                               
SET @COVA_NOT_REPCOST=''                               
END                               
                      
                              
-------------------------------------------------------------------------------------------------------------------                                          
--Coverage A in case of HO2, 3 Repair must be equal to 100% of Market Value                              
                              
 DECLARE @COVA_NOT_MAKVALUE CHAR                              
                              
 -- MARKET VALUE                              
 DECLARE @DECMARKET_VALUE DECIMAL                              
                              
 SELECT @DECMARKET_VALUE=ISNULL(MARKET_VALUE,0)                
 FROM POL_DWELLINGS_INFO                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                
                              
-- HO-2 Repair Cost - 11193,11403,HO-3 Repair Cost - 11194,11404                              
IF(@INTPOLICY_TYPE=11193 OR @INTPOLICY_TYPE=11403 OR @INTPOLICY_TYPE=11194 OR @INTPOLICY_TYPE=11404)                              
BEGIN                               
 IF(@DECDWELLING_LIMIT <> @DECMARKET_VALUE)                              
  BEGIN                               
  SET @COVA_NOT_MAKVALUE ='Y'                              
  END                               
 ELSE                              
  BEGIN                               
  SET @COVA_NOT_MAKVALUE ='N'                              
  END                               
END                               
ELSE                              
BEGIN                               
SET @COVA_NOT_MAKVALUE ='N'                              
END                               
---                              
 if(@COVA_NOT_MAKVALUE='Y')                              
 begin                               
 set @COVA_NOT_MAKVALUE=''                              
 end                               
                              
-------------------------------------------------------------------------------------------------------------------                                          
--Coverage C in case of HO4, 6 Reg and Delux cannot be greater than 100% of Replacement Cost                            
-- HO-4 11195,11405,HO-4 Deluxe 11245,11407, HO-6 Deluxe 11246,11408, HO-6 11196,11406                            
                            
 DECLARE  @COVC_NOT_REPCOST CHAR                            
                             
IF(@INTPOLICY_TYPE=11195 OR @INTPOLICY_TYPE=11405 OR @INTPOLICY_TYPE=11245 OR @INTPOLICY_TYPE=11407 OR @INTPOLICY_TYPE=11246 OR @INTPOLICY_TYPE=11408 OR @INTPOLICY_TYPE=11196 OR @INTPOLICY_TYPE=11406)                              
BEGIN                             
 IF(@DECPERSONAL_PROP_LIMIT > @INTREPLACEMENT_COST)                            
 BEGIN                             
 SET @COVC_NOT_REPCOST='Y'                        
 END                   
 ELSE                            
 BEGIN                             
 SET @COVC_NOT_REPCOST='N'                          
 END                            
END                            
ELSE                            
BEGIN                             
SET @COVC_NOT_REPCOST='N'                            
END                            
           
                       
                          
 ----------                            
 if(@COVC_NOT_REPCOST='Y')                            
 begin                             
   set @COVC_NOT_REPCOST=''                            
 end                                        
                        
-----------------------------------------------------------------------------------------------------        -- Coverage A is not mandatory in case of HO-4=11195, Ho-4 Deluxe= 11245                        
-- Modified on 27 Feb. 2006                        
IF(@INTPOLICY_TYPE=11195 OR @INTPOLICY_TYPE=11245 OR @INTPOLICY_TYPE=11405 OR @INTPOLICY_TYPE=11407)                        
BEGIN                         
IF(@DECDWELLING_LIMIT IS NULL)                     
BEGIN                        
SET @DWELLING_LIMIT='N'                        
END                          
END       -----------------                    
---------CHECK FOR THE POLICY TYPE : 22 June 2006                           
Declare @DWELLING_LIMIT_COVA char                          
IF(@INTPOLICY_TYPE!=11195 AND @INTPOLICY_TYPE !=11245 AND @INTPOLICY_TYPE!=11196 AND                          
@INTPOLICY_TYPE !=11246 AND @INTPOLICY_TYPE!=11405  AND @INTPOLICY_TYPE !=11407 AND @DWELLING_LIMIT='' )                          
BEGIN                          
SET @DWELLING_LIMIT_COVA = ''                          
END                          
ELSE                          
BEGIN                          
SET @DWELLING_LIMIT_COVA='N'                          
END                          
                    
-----------------                 
-------------------Modified 19 June 2006                                  
--Built on a Continuous Foundation If No - Reject                                   
--If yes - Then check the Coverage/Limits Tab on HO2-hO3- and H05 only                                  
--If Coverage A- Building is less than $75,000 Reject                                  
--11148 HO-3^REPLACE HO-3 Replacement                                  
--11149 HO-5^REPLACE HO-5 Replacement                                  
--11192 HO-2^REPLACE HO-2 Replacement                                  
--11400 HO-3^REPLACE HO-3 Replacement                                  
--11401 HO-5^REPLACE HO-5 Replacement                                  
--11402 HO-2^REPLACE HO-2 Replacement        
/*      
11148 HO-3^REPLACE HO-3 Replacement      
11149 HO-5^REPLACE HO-5 Replacement      
11192 HO-2^REPLACE HO-2 Replacement      
11193 HO-2^REPAIR HO-2 Repair      
11194 HO-3^REPAIR HO-3 Repair      
-----------------------------------------------------      
11400 HO-3^REPLACE HO-3 Replacement      
11401 HO-5^REPLACE HO-5 Replacement      
11402 HO-2^REPLACE HO-2 Replacement      
11403 HO-2^REPAIR HO-2 Repair      
11404 HO-3^REPAIR HO-3 Repair      
------------------------------------------------------      
11409 HO-3^PREMIER HO-3 Premier      
11410 HO-5^PREMIER HO-5 Premier*/                                     
DECLARE @POLICY_EFFECTIVE_DATE VARCHAR(20),      
 @EFFECTIVE_DATE VARCHAR(20)      
SET @EFFECTIVE_DATE='01/01/2008'      
SELECT @POLICY_EFFECTIVE_DATE=ISNULL(DATEDIFF(DAY,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101),@EFFECTIVE_DATE),'') FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)                                                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                  
                                  
DECLARE @INTBUILT_ON_CONTINUOUS_FOUNDATION INT                                  
DECLARE @BUILT_ON_CONTINUOUS_FOUNDATION_COV CHAR                                  
SELECT @INTBUILT_ON_CONTINUOUS_FOUNDATION = ISNULL(BUILT_ON_CONTINUOUS_FOUNDATION,0)                                   
    FROM POL_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                   
      
IF(@POLICY_EFFECTIVE_DATE <= 0)      
  BEGIN       
 IF(@INTBUILT_ON_CONTINUOUS_FOUNDATION=1)                                 
  BEGIN                                  
    IF(@INTPOLICY_TYPE=11148 OR @INTPOLICY_TYPE=11149 OR @INTPOLICY_TYPE=11192 OR                             
      @INTPOLICY_TYPE=11400 OR @INTPOLICY_TYPE=11401 OR @INTPOLICY_TYPE=11402 OR       
      @INTPOLICY_TYPE=11193 OR @INTPOLICY_TYPE=11194 OR @INTPOLICY_TYPE = 11403 OR      
      @INTPOLICY_TYPE=11404 OR @INTPOLICY_TYPE=11409 OR @INTPOLICY_TYPE=11410)                             
      BEGIN                                  
   IF(@DECDWELLING_LIMIT<75000)                                  
   BEGIN                                  
   SET @BUILT_ON_CONTINUOUS_FOUNDATION_COV='Y'                                  
   END        
   ELSE                               
   BEGIN                                  
   SET @BUILT_ON_CONTINUOUS_FOUNDATION_COV='N'                                  
   END                           
  END                                  
  ELSE                                  
  BEGIN                                  
  SET @BUILT_ON_CONTINUOUS_FOUNDATION_COV='N'                                  
  END                        
  END                                  
 ELSE                   
 BEGIN                                  
 SET @BUILT_ON_CONTINUOUS_FOUNDATION_COV='N'                                  
 END       
 END      
 ELSE                   
 BEGIN                                  
 SET @BUILT_ON_CONTINUOUS_FOUNDATION_COV='N'                                  
 END                                   
----=============END                                     
------------------If policy type is HO-3 Premier                                
--Then look at the Dwelling Details Tab Market Value and Replacement Value must                                 
--be equal or greater than $175,000                                 
--if Not -risk is declined If $175,00 or greater look at the Coverage/Limits tab Coverage A                      
--Removed Market Value Check  : 29 June 2006                               
DECLARE @REPLACEMENT_COST_HO3_HO5_PREMIER CHAR      
IF(@POLICY_EFFECTIVE_DATE <= 0)      
  BEGIN                                 
 IF(@INTPOLICY_TYPE=11409)   -- Removed @INTPOLICY_TYPE=11410, Charles (30-Nov-09), Itrack 6681                               
 BEGIN                                
  IF(@INTREPLACEMENT_COST>=175000)                                
  BEGIN                                
       IF(@DECDWELLING_LIMIT=@INTREPLACEMENT_COST)                                
        BEGIN                                
        SET @REPLACEMENT_COST_HO3_HO5_PREMIER='Y' --OK                                
        END                                
       ELSE                                
    BEGIN                                
        SET @REPLACEMENT_COST_HO3_HO5_PREMIER = 'N' --REJECT                                
        END                                
  END                                
  ELSE                                
  BEGIN                                
  SET @REPLACEMENT_COST_HO3_HO5_PREMIER='N' --REJECT                                
  END                                 
 END                                
 ELSE                                
  BEGIN                                
  SET @REPLACEMENT_COST_HO3_HO5_PREMIER='Y' --OK                                
  END      
  END                                
  ELSE                                
  BEGIN                                
  SET @REPLACEMENT_COST_HO3_HO5_PREMIER='Y' --OK                                
  END        
    
--Added by Charles on 18-Dec-09 for Itrack 6681  
DECLARE @GF_REP_COST CHAR(2)  
DECLARE @REPLACEMENT_COST_HO5_PREMIER_NEW CHAR  
SET @GF_REP_COST='-1'  
SET @REPLACEMENT_COST_HO5_PREMIER_NEW='Y' --OK  
  
IF @STATE_ID = 14  
BEGIN  
 IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'01/01/2000',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'10/14/2008',101) AS DATETIME))  
    BEGIN  
  SET @GF_REP_COST = 'Y'  
 END  
 ELSE IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'10/15/2008',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'12/31/9999',101) AS DATETIME))  
 BEGIN  
  SET @GF_REP_COST = 'N'  
 END   
END  
ELSE IF @STATE_ID = 22  
BEGIN  
 IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'01/01/2000',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'01/31/2009',101) AS DATETIME))  
    BEGIN  
  SET @GF_REP_COST = 'Y'  
 END  
 ELSE IF( @DATE_APP_EFFECTIVE_DATE >= CAST(CONVERT(VARCHAR,'02/01/2009',101) AS DATETIME) AND @DATE_APP_EFFECTIVE_DATE <= CAST(CONVERT(VARCHAR,'12/31/9999',101) AS DATETIME))  
 BEGIN  
  SET @GF_REP_COST = 'N'  
 END   
END   
  
IF(@POLICY_EFFECTIVE_DATE <= 0)           
BEGIN                               
  IF(@INTPOLICY_TYPE=11410 AND @GF_REP_COST='N')                           
  BEGIN                                
    IF(@INTREPLACEMENT_COST>=200000)                                
    BEGIN                                
      IF(@DECDWELLING_LIMIT=@INTREPLACEMENT_COST)                                
      BEGIN                                
     SET @REPLACEMENT_COST_HO5_PREMIER_NEW='Y' --OK                                
      END                                
      ELSE                                
      BEGIN                                
     SET @REPLACEMENT_COST_HO5_PREMIER_NEW = 'N' --REJECT                                
      END                                
    END                                
    ELSE                                
    BEGIN                                
   SET @REPLACEMENT_COST_HO5_PREMIER_NEW='N' --REJECT                                
    END                                 
  END                                
  ELSE                                
  BEGIN                                
  SET @REPLACEMENT_COST_HO5_PREMIER_NEW='Y' --OK                                
  END           
END  
--Added till here  
  
--Added by Charles on 30-Nov-09 for Itrack 6681        
--POLICY TYPE IS HO-5 PREMIER                              
DECLARE @REPLACEMENT_COST_HO5_PREMIER CHAR  
SET @REPLACEMENT_COST_HO5_PREMIER='Y' --OK                                
          
IF(@POLICY_EFFECTIVE_DATE <= 0)           
BEGIN                               
  IF(@INTPOLICY_TYPE=11410 AND @GF_REP_COST='Y') --@GF_REP_COST Added by Charles on 18-Dec-09 for Itrack 6681                           
  BEGIN                                
    IF(@INTREPLACEMENT_COST>=175000)                                
    BEGIN                                
      IF(@DECDWELLING_LIMIT=@INTREPLACEMENT_COST)                                
      BEGIN                                
     SET @REPLACEMENT_COST_HO5_PREMIER='Y' --OK           
      END                                
      ELSE                                
      BEGIN                                
     SET @REPLACEMENT_COST_HO5_PREMIER = 'N' --REJECT                                
      END                                
    END                                
    ELSE                                
    BEGIN                                
   SET @REPLACEMENT_COST_HO5_PREMIER='N' --REJECT                                
    END                                 
  END                                
  ELSE                                
  BEGIN                                
  SET @REPLACEMENT_COST_HO5_PREMIER='Y' --OK                                
  END           
END                                        
--Added till here                    
    
---======================START=========GRANDFTAHERED COVERGAES==========================                  
DECLARE @POLICY_EFF_DATE datetime                                            
SELECT @POLICY_EFF_DATE = APP_EFFECTIVE_DATE                                             
FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=POLICY_VERSION_ID                                               
                                          
                                      
SELECT MNTC.COV_DES as COVERAGE_DES                    
from POL_DWELLING_SECTION_COVERAGES AVC                                            
INNER JOIN MNT_COVERAGE MNTC on MNTC.COV_ID = AVC.COVERAGE_CODE_ID                                            
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID                                                           
AND NOT( @POLICY_EFF_DATE  BETWEEN MNTC.EFFECTIVE_FROM_DATE                                            
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))                        
                  
--Check for WOL and AGENCY USER                    
--W001 -CARRIERSYSTEMID --W001 TO BE CHECKED WITH XML(DYNAMICALLY)                      
DECLARE @WOLVERINE_USER VARCHAR(50)                   
IF(@USER='w001')                     
 BEGIN                    
  SET @WOLVERINE_USER='Y'  --WOL USER                    
 END                    
ELSE                    
 BEGIN                    
  SET @WOLVERINE_USER='N'  --AGENCY USER                    
 END                    
----=======================END========GRANDFTAHERED COVERGAES==========================           
-----------------------------------------GRANDFATHER LIMIT -------------------------------------------------------------         
SELECT  MNT.COV_DES AS LIMIT_DESCRIPTION,              
CASE MNT.LIMIT_TYPE              
  WHEN 2 THEN ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  +   ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')               
  + '/'  +    ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')               
 ELSE              
       ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')               
 +         ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')               
END AS LIMIT,             
AVC.COVERAGE_CODE_ID             
FROM POL_DWELLING_SECTION_COVERAGES AVC                                              
INNER JOIN MNT_COVERAGE_RANGES MNTC  ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID  AND  AVC.LIMIT_ID= MNTC.LIMIT_DEDUC_ID                                          
INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID               
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID                                  
AND LIMIT_DEDUC_TYPE='LIMIT'              
AND NOT( @POLICY_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1') AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')              
)                
   
            
----------------------------------------- END GRANFATHER LIMIT ---------------------------------------------------------                               
            
--========================================= GRANDFATHER Additional ====================================================            
            
 SELECT  MNT.COV_DES AS DEDUCTIBLE_DESCRIPTION,            
 CASE MNT.DEDUCTIBLE_TYPE            
 WHEN 2 THEN   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')             
 + '/' + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')             
 ELSE            
    ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')             
         + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')             
 END AS DEDUCTIBLE,            
 AVC.COVERAGE_CODE_ID              
 FROM POL_DWELLING_SECTION_COVERAGES AVC                                            
 INNER JOIN MNT_COVERAGE_RANGES MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID  AND  AVC.DEDUC_ID= MNTC.LIMIT_DEDUC_ID                                        
 INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID             
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID                                                             
 AND LIMIT_DEDUC_TYPE='DEDUCT'            
 AND NOT(@POLICY_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1') AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')            
 )            
--========================================= END GRANDFATHER DEDUCTIBLE ===============================================            
                 
                          
---- Deductible             
SELECT  MNT.COV_DES AS ADDDEDUCTIBLE_DESCRIPTION,              
 CASE MNT.LIMIT_TYPE              
 WHEN 2 THEN  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')               
 + '/'   +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')               
 ELSE              
   ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')               
  +  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')               
END AS LIMIT,             
AVC.COVERAGE_CODE_ID             
FROM POL_DWELLING_SECTION_COVERAGES AVC                                              
INNER JOIN MNT_COVERAGE_RANGES MNTC ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID AND  AVC.ADDDEDUCTIBLE_ID= MNTC.LIMIT_DEDUC_ID                       
INNER JOIN MNT_COVERAGE MNT ON AVC.COVERAGE_CODE_ID = MNT.COV_ID               
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID                                                             
AND LIMIT_DEDUC_TYPE= 'ADDDED' AND MNT.COV_ID= AVC.COVERAGE_CODE_ID           
AND NOT( @POLICY_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1') AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')              
)                                  
         
--IF 1. Additional Premises (Number of Premises) - Residence Premises - Rented to Others (HO-40)          
---  2. Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Family)         
---  3. Additional Premises (Number of Premises) -Other Location -Rented to Others (2 Family)                              if the total number exceeds 4         
--- IF the total number exceeds 4 Risk is declined        
--- COVERAGE_CODE_ID= 260 -- Additional Premises (Number of Premises) - Residence Premises - Rented to Others (HO-40)        
--- COVERAGE_CODE_ID= 262 -- Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Family)         
        
DECLARE @ADDITIONAL_PREMISES_COV_DESC CHAR        
        
DECLARE @LIMIT_1 DECIMAL         
DECLARE @LIMIT_2 DECIMAL        
DECLARE @LIMIT_3 DECIMAL         
SET @LIMIT_1 = 0        
SET @LIMIT_2 = 0        
SET @LIMIT_3 = 0        
      
SELECT @LIMIT_1= isnull(LIMIT_1,0)  FROM POL_DWELLING_SECTION_COVERAGES         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID         
AND COVERAGE_CODE_ID= 260        
      
SELECT @LIMIT_2= isnull(LIMIT_1,0) FROM POL_DWELLING_SECTION_COVERAGES         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID         
AND COVERAGE_CODE_ID= 262        
SELECT @LIMIT_3= isnull(LIMIT_1,0) FROM POL_DWELLING_SECTION_COVERAGES         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =POLICY_VERSION_ID  AND DWELLING_ID=@DWELLING_ID         
AND COVERAGE_CODE_ID= 264        
         
IF((@LIMIT_1 + @LIMIT_2 + @LIMIT_3) > 4)         
 BEGIN        
  SET @ADDITIONAL_PREMISES_COV_DESC='Y'        
 END         
ELSE         
 BEGIN        
   SET @ADDITIONAL_PREMISES_COV_DESC='N'        
 END         
       
/*      
IF SOME COVERAGES/LIMITS/DEDUCTIBLES/ENDORSEMENTS ARE THERE WHICH THE RENEWED      
VERSION IS NOT ELIGIBLE TO OPT FOR,THESE COVERAGES/LIMITS/ENDORSEMENTS ARE NOT      
COPIED TO THIS RENEWED VERSION.IN THIS CASE IF USE/EOD PROCESS COMMITS(Refer) THE       
RENEWAL PROCESS NOT READJUSTING THE COVERAGES, PROCESS SHOULD NOT COMMIT ,SHOULD REFER      
*/       
 DECLARE @ALL_DATA_VALID INT      
 DECLARE @COPY_COVERAGE_AT_RENEWAL CHAR      
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID        
 AND   ALL_DATA_VALID=2 )      
 BEGIN       
 SET @COPY_COVERAGE_AT_RENEWAL ='Y'      
 END                
---------------------------------------------------------------------------------------------                                                    
                                                                        
SELECT                                            
-- Mandatory                                            
  --isnull(@DWELLING_LIMIT,'' )as  DWELLING_LIMIT,                                            
  --isnull(@PERSONAL_LIAB_LIMIT,'') as PERSONAL_LIAB_LIMIT,                                            
  --isnull(@MED_PAY_EACH_PERSON,'') as MED_PAY_EACH_PERSON ,                                      
  -- Rule                                      
  @PERSONAL_PROP_LIMIT as PERSONAL_PROP_LIMIT,    --HO-6                                    
  @COVAC_HO6 as COVAC_HO6, -- HO-6                  
  @REPLACEMENT_COST_POLICY_HO2_HO3 as REPLACEMENT_COST_POLICY_HO2_HO3, -- HO-2 & HO-3             
  @REPLACEMENT_COST_POLICY_HO5 as REPLACEMENT_COST_POLICY_HO5, -- HO-5                                      
  @COVA_NOT_REPCOST as  COVA_NOT_REPCOST,                              
  @COVA_NOT_MAKVALUE as COVA_NOT_MAKVALUE,                            
  @COVC_NOT_REPCOST as COVC_NOT_REPCOST,               
 ----                   
  --@DWELLING_LIMIT_COVA as DWELLING_LIMIT_COVA ,                
  @BUILT_ON_CONTINUOUS_FOUNDATION_COV as BUILT_ON_CONTINUOUS_FOUNDATION_COV ,              
  @REPLACEMENT_COST_HO3_HO5_PREMIER as  REPLACEMENT_COST_HO3_HO5_PREMIER ,            
  @WOLVERINE_USER AS WOLVERINE_USER ,        
  @ADDITIONAL_PREMISES_COV_DESC AS ADDITIONAL_PREMISES_COV_DESC ,      
  @COPY_COVERAGE_AT_RENEWAL AS COPY_COVERAGE_AT_RENEWAL,    
 @REPLACEMENT_COST_HO5_PREMIER AS REPLACEMENT_COST_HO5_PREMIER, --Added by Charles on 30-Nov-09 for Itrack 6681          
 @REPLACEMENT_COST_HO5_PREMIER_NEW AS REPLACEMENT_COST_HO5_PREMIER_NEW --Added by Charles on 18-Dec-09 for Itrack 6681  
  
               
END      
      