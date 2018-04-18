IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForRentalDwelling]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForRentalDwelling]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetRatingInformationForRentalDwelling]                                                                 
(                                                                        
 @CUSTOMERID      INT,                                                                        
 @APPID         INT,                                                                        
 @APPVERSIONID    INT,                                                                        
 @DWELLINGID      INT                                                                        
)                                                                        
AS                                                        
BEGIN                                                                        
                
SET QUOTED_IDENTIFIER OFF                                                                        
                                                                        
DECLARE @LOB_ID          nvarchar(100)                                                                          
DECLARE @POLICY_ID          nvarchar(100)                                                                        
DECLARE @TERRITORYCODES       nvarchar(100)                                                                        
DECLARE @INSURANCESCORE       nvarchar(100)                                                                        
DECLARE @TERRITORYZONE       nvarchar(100)                                                                        
DECLARE @TERRITORYNAME       nvarchar(100)           
DECLARE @EARTHQUAKEZONE      NVARCHAR(100)                                                                         
DECLARE @TERRITORYCOUNTY      nvarchar(100)                                               
                              
DECLARE @STATE_ID     nvarchar(20)                                    
DECLARE @STATENAME         nvarchar(100)                                                                        
DECLARE @NEWBUSINESSFACTOR       nvarchar(100)                                                                        
DECLARE @QUOTEEFFDATE         nvarchar(100)                                                                        
DECLARE @QUOTEEXPDATE        nvarchar(100)                                                                        
DECLARE @TERMFACTOR         nvarchar(100)                                                                        
DECLARE @PRODUCTNAME         nvarchar(100)                                                                        
DECLARE @PRODUCT_PREMIER        nvarchar(100)                                                                        
DECLARE @SEASONALSECONDARY       nvarchar(100)                                                            
DECLARE @REPLACEMENTCOSTFACTOR   nvarchar(100)                                                                        
DECLARE @DWELLING_LIMITS       nvarchar(100)                                                                        
DECLARE @DEDUCTIBLE         nvarchar(100)                                              
DECLARE @PROTECTIONCLASS       nvarchar(100)                      
DECLARE @DISTANCET_FIRESTATION   nvarchar(100)                                                                        
DECLARE @FEET2HYDRANT     nvarchar(100)                     
DECLARE @EXTERIOR_CONSTRUCTION   nvarchar(100)                      
DECLARE @EXTERIOR_CONSTRUCTION_DESC   nvarchar(100)                     
DECLARE @EXTERIOR_CONSTRUCTION_F_M      nvarchar(100)                                                       
DECLARE @DWELL_UND_CONSTRUCTION_DP1143  nvarchar(100)                                                                      
DECLARE @NO_YEARS_WITH_WOLVERINE    nvarchar(100)                      
                                                  
DECLARE @DOC             nvarchar(100)                              
DECLARE @AGEOFHOME          nvarchar(100)                                               
DECLARE @NUMBEROFFAMILIES        nvarchar(100)                                                                      
DECLARE @N0_LOCAL_ALARM        nvarchar(100)                                                                      
DECLARE @LOSSFREE          nvarchar(100)                                                                      
DECLARE @NOTLOSSFREE nvarchar(100)    
DECLARE @MULTIPLEPOLICYFACTOR    nvarchar(100)                                                                   
DECLARE @FORM_CODE          nvarchar(100)                                                                      
DECLARE @COVERAGEVALUE          nvarchar(100)                                                                   
                
--OPTIONAL COVERAGES PAGE--                
DECLARE @PERSONALLIABILITY_LIMIT        nvarchar(100)                                                                      
DECLARE @MEDICALPAYMENTSTOOTHERS_LIMIT     nvarchar(100)                                                                      
DECLARE @EARTHQUAKEDP469           nvarchar(100)                                                                      
DECLARE @INCIDENTALOFFICE    nvarchar(100)                                                                      
DECLARE @MINESUBSIDENCEDP480          nvarchar(100)                
DECLARE @MINESUBSIDENCEDP480_COVG_LIMIT        nvarchar(100)                                                                        
DECLARE @APPURTENANTSTRUCTURES_INCLUDE   nvarchar(100)                                                                      
DECLARE @APPURTENANTSTRUCTURES_ADDITIONAL  nvarchar(100)                                                                      
DECLARE @BUILDINGIMPROVEMENTS_INCLUDE      nvarchar(100)                                                                      
DECLARE @BUILDINGIMPROVEMENTS_ADDITIONAL   nvarchar(100)                                                                      
DECLARE @RENTALVALUE_INCLUDE          nvarchar(100)                                                                      
DECLARE @RENTALVALUE_ADDITIONAL        nvarchar(100)                                                                      
DECLARE @PERSONALPROPERTY_INCLUDE        nvarchar(100)                                                                      
DECLARE @PERSONALPROPERTY_ADDITIONAL       nvarchar(100)                                                                      
DECLARE @CONTENTSINSTORAGE_INCLUDE        nvarchar(100)                                                                      
DECLARE @CONTENTSINSTORAGE_ADDITIONAL     nvarchar(100)                                                                      
DECLARE @TREESLAWNSSHRUBS_INCLUDE        nvarchar(100)                                                          
DECLARE @TREESLAWNSSHRUBS_ADDITIONAL       nvarchar(100)                                                                      
DECLARE @RADIOTV_INCLUDE          nvarchar(100)                                                                      
DECLARE @RADIOTV_ADDITIONAL          nvarchar(100)                                                                      
DECLARE @SATELLITEDISHES_INCLUDE         nvarchar(100)                                                                    
DECLARE @SATELLITEDISHES_ADDITIONAL        nvarchar(100)                                                                      
DECLARE @AWNINGSCANOPIES_INCLUDE         nvarchar(100)                                                                      
DECLARE @AWNINGSCANOPIES_ADDITIONAL    nvarchar(100)                                                                      
DECLARE @FLOATERBUILDINGMATERIALS_INCLUDE       nvarchar(100)  
DECLARE @FLOATERBUILDINGMATERIALS_ADDITIONAL    nvarchar(100)    
DECLARE @FLOATERNONSTRUCTURAL_INCLUDE     nvarchar(100)                      
DECLARE @FLOATERNONSTRUCTURAL_ADDITIONAL   nvarchar(100)                                                         
DECLARE @VALUESCUSTOMERAPP          nvarchar(20)                                                          
DECLARE @INSUREWITHWOL              smallint                                                          
DECLARE @WOODSTOVE_SURCHARGE        nvarchar(20)                 
DECLARE @INCEPTIONDATE     varchar(100)                                                           
DECLARE @MINESUBSIDENCE_ADDITIONAL   varchar(100)    
DECLARE @FIREPROTECTIONCLASS   varchar(100)  
DECLARE @LEADLIABILITY  varchar(100)  
DECLARE @TEMPEXPFEE NVARCHAR(20)                                                                  
-------- START ---------                
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





   
   
      
         
         
            
              
                
DECLARE @APP_NUMBER VARCHAR(20)                
DECLARE @APP_VERSION VARCHAR(20)            
SELECT                                                                         
 @LOB_ID  =  ISNULL(APP_LOB,0) ,                                                                       
 @TERMFACTOR   =  ISNULL(APP_TERMS,''),                
 @QUOTEEFFDATE   =  CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                                                            
 @QUOTEEXPDATE   =  CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101),                
 @INCEPTIONDATE  =  ISNULL(CONVERT(Varchar(10),APP_INCEPTION_DATE,101),''),              
 @APP_NUMBER  = APP_NUMBER,                
 @APP_VERSION  = APP_VERSION                        
FROM                                                 
 APP_LIST  WITH (NOLOCK)                                                                      
WHERE                                                                         
 CUSTOMER_ID = @CUSTOMERID   AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                                      
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 





  
   
       
        
---- INSURANCE SCORE              
SELECT  
 @INSURANCESCORE=  case isnull(APPLY_INSURANCE_SCORE,-1) 
 when -1 then '100'        
 when  -2 then 'NOHITNOSCORE'         
 else convert(nvarchar(20),APPLY_INSURANCE_SCORE) end ,        
  @STATENAME    =  UPPER(ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'')) ,                                                                             
  @STATE_ID = APP_LIST.STATE_ID              
FROM                                                                 
APP_LIST WITH (NOLOCK)                 
 INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK) ON  APP_LIST.STATE_ID=MNT_COUNTRY_STATE_LIST.STATE_ID                                
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                         
                                                     
                                                    
--------------------PRIOR LOSS                               
-- If exists any record having the year difference less than 1 then it is not loss free            
/*IF EXISTS (                     
 SELECT  isnull(DATEDIFF(YEAR,OCCURENCE_DATE,@QUOTEEFFDATE),0)                                            
 FROM  APP_PRIOR_LOSS_INFO  WHERE CUSTOMER_ID=@CUSTOMERID and LOB ='6'--RENTAL              
 GROUP BY OCCURENCE_DATE HAVING isnull(DATEDIFF(YEAR,OCCURENCE_DATE,@QUOTEEFFDATE),0)>0 and isnull(DATEDIFF(YEAR,OCCURENCE_DATE,@QUOTEEFFDATE),0)<1)                                   
 SET @LOSSFREE ='N'                                              
 ELSE                                              
  SET @LOSSFREE ='Y'                    
*/      
-- CHECK FOR PRIOR LOSS IN RENTAL
IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE  CUSTOMER_ID = @CUSTOMERID AND LOB=6 and ((DATEDIFF(day ,OCCURENCE_DATE ,@QUOTEEFFDATE)<= 365) and (DATEDIFF(day ,OCCURENCE_DATE ,@QUOTEEFFDATE))>0))                      
 
 BEGIN                      
  SET @LOSSFREE = 'N'        
  SET @NOTLOSSFREE='Y'                   
 END                      
ELSE                     
 BEGIN                      
  SET @LOSSFREE = 'Y'         
  SET @NOTLOSSFREE='N'                     
 END 

-- CHECK FOR UNDERWRITING QUESTION
IF(@LOSSFREE = 'N')
BEGIN
 IF  ((SELECT VALUED_CUSTOMER_DISCOUNT_OVERRIDE FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID)=1)
	BEGIN
		SET @LOSSFREE = 'Y'          
		SET @NOTLOSSFREE='N' 
	END  
 ELSE                     
	BEGIN                      
		SET @LOSSFREE = 'N'         
		SET @NOTLOSSFREE='Y'                     
	END 
 END   
/*    
--This is given once two years are completed, at the third renewal                  
IF   @INSUREWITHWOL > 2                                                                              
 SET @VALUEDCUSTOMER = 'Y'                                                                              
ELSE                                                                              
 SET @VALUEDCUSTOMER = 'N'                                                         
*/    
                            
    
--THIS IS GIVEN ONCE TWO YEARS ARE COMPLETED, AT THE THIRD RENEWAL            
IF   ISNULL(@NO_YEARS_WITH_WOLVERINE,0)=0 or @NO_YEARS_WITH_WOLVERINE=''                                                        
 SET @INSUREWITHWOL = 0                                                     
                                                  
IF   @NO_YEARS_WITH_WOLVERINE > 2         
 begin                                                                         
  SET @VALUESCUSTOMERAPP = 'Y'             
  set @INSUREWITHWOL = 1                                                                     
 end        
ELSE                                     
 begin        
  SET @VALUESCUSTOMERAPP = 'N'                                                     
         set @INSUREWITHWOL=0        
 end        
---------------------------             
DECLARE @POLICY_CODE VARCHAR(100)                                   
SELECT @POLICY_CODE    = UPPER(ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,''))               
FROM APP_LIST WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES WITH (NOLOCK) ON  APP_LIST.POLICY_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                               
                                                      
-----------GET THE POLICY FROM THE CODE--------                            
SET @PRODUCTNAME = dbo.piece(@POLICY_CODE,'^',1)                                  
SET @PRODUCT_PREMIER = dbo.piece(@POLICY_CODE,'^',2)                                  
                                  
-------------------------------------------------------                                                                    
DECLARE @LOCATIONID NVARCHAR(20)                           
 SELECT                                                                                  
 @LOCATIONID   = LOCATION_ID,                                                                        
 @DOC       =  CONVERT(VARCHAR(10),YEAR_BUILT,101),               
 @AGEOFHOME   =  year(@QUOTEEFFDATE) - YEAR_BUILT  ,                                                                    
 @REPLACEMENTCOSTFACTOR = CONVERT(decimal(18),REPLACEMENT_COST)                                                                       
FROM                                                                                  
 APP_DWELLINGS_INFO  WITH (NOLOCK)                                                                                
WHERE                                                                                   
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                            

IF (@AGEOFHOME < 0)
	BEGIN
	SET @AGEOFHOME=0
	END  
                                                         
IF (@LOCATIONID is null)                                                        
  SET @LOCATIONID = ''                                                          
IF (@DOC is null)                                                       
  SET @DOC=''                                                          
IF (@AGEOFHOME is null)        
  SET @AGEOFHOME=0                                                  
IF (@REPLACEMENTCOSTFACTOR is null)                                                        
  SET @REPLACEMENTCOSTFACTOR='0'                                                          
                  
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------                                                              
DECLARE @ZIPCODE   nvarchar(10)                                                                                         
                
IF (@LOCATIONID = '')                                                   
 SET @ZIPCODE=''                                               
ELSE                                   
 SELECT  @ZIPCODE = LOC_ZIP        
 FROM  APP_LOCATIONS  WITH (NOLOCK)                                                                       
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and LOCATION_ID = @LOCATIONID                                                                        
                                                              
                                                                
IF ( @ZIPCODE !='')                                 
BEGIN                                                                        
 SELECT                                                                
  @TERRITORYCODES = ISNULL(TERR ,''),                                                                  
  @TERRITORYZONE  = ISNULL(ZONE,'0'),                                                    
  @TERRITORYNAME  = ISNULL(CITY,''),                                                    
  @TERRITORYCOUNTY= ISNULL(COUNTY,'')  ,        
 @EARTHQUAKEZONE = isnull(EARTHQUAKE_ZONE,'0')                       
 FROM              
  MNT_TERRITORY_CODES  WITH (NOLOCK)                 
 WHERE                                                      
  ZIP = (SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5))  and STATE=@STATE_ID    and  LOBID=@LOB_ID  
  AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                            
END                                                                        
ELSE                                                                        
 BEGIN                                                                        
  SET @TERRITORYCODES = ''                                                                    
  SET @TERRITORYZONE=''            
  SET @EARTHQUAKEZONE=''                                                                
 END                                                       
                          
                /*                                                   
                                                                
IF @STATE_ID=14           --  forcely converting DEFAULT @TERRITORYZONE=5  in case of INDIANA for earthquake HO-315          
BEGIN                                                                
 IF @TERRITORYZONE <=0 or @TERRITORYZONE > 5  or @TERRITORYZONE=''                                                               
 SET @TERRITORYZONE=5                                                                
END                                                                
IF @STATE_ID=22           --  forcely converting @TERRITORYZONE=1 in case of mischigan for earthquake HO-315                                                                
BEGIN                                                                
 SET @TERRITORYZONE=1                                                                
END                                                                
        */        
----------------------------------------  COVERAGES FOR INDIANA  ----------------------------------------------------------------------------------------------------------------                                                                    
                                                            
IF @STATE_ID=14    -- START OF COVERAGES FOR INDIANA                                                                    
BEGIN                                                                     
                
  ----  COVERAGE - B   COV_ID=774                                                               
 SELECT                                                                                  
  @APPURTENANTSTRUCTURES_INCLUDE     = ISNULL(limit_1,'0.00')                                                                    
                                                                  
                                                                              
 FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                                   
   COVERAGE_CODE_ID = 774 AND CUSTOMER_ID =@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                              
  SELECT             
                                                                      
  @APPURTENANTSTRUCTURES_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                 
                                                                              
 FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)         
 WHERE                                                                                   
   COVERAGE_CODE_ID = 779 AND CUSTOMER_ID =@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                              
                                     
                                                                 
 IF @APPURTENANTSTRUCTURES_INCLUDE IS NULL               
  SET @APPURTENANTSTRUCTURES_INCLUDE = 0.00                                             
                                                                   
 IF @APPURTENANTSTRUCTURES_ADDITIONAL IS NULL                                                                 
  SET @APPURTENANTSTRUCTURES_ADDITIONAL = 0.00                                                                
                                                                 
 -----  PERSONEL PROPERTY  - COVERAGE - C  -  LPP          
   
 SET @PERSONALPROPERTY_ADDITIONAL=0                                                                    
 SELECT                                                                                  
  @PERSONALPROPERTY_INCLUDE     = ISNULL(limit_1,'0.00'),                                                                   
  @PERSONALPROPERTY_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                  
  
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                
 WHERE                                                                                   
   COVERAGE_CODE_ID=775   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                        
 IF @PERSONALPROPERTY_INCLUDE IS NULL           
  SET @PERSONALPROPERTY_INCLUDE = 0.00                                                                
                                                
 IF @PERSONALPROPERTY_ADDITIONAL IS NULL                                                                 
  SET @PERSONALPROPERTY_ADDITIONAL = 0.00                                                              
                                                                     
 -----  RENTAL VALUE - COVERAGE - D  -  RV                                                                       
 SET @RENTALVALUE_ADDITIONAL=0                                                            
 SELECT                                                                    
  @RENTALVALUE_INCLUDE     = ISNULL(limit_1,'0.00'),                                                                    
  @RENTALVALUE_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                    
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                
 WHERE                                   
   COVERAGE_CODE_ID=776   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                           
                                                    
 IF @RENTALVALUE_INCLUDE IS NULL         
  SET @RENTALVALUE_INCLUDE = 0.00                         
                                                                     
 IF @RENTALVALUE_ADDITIONAL IS NULL                                                                 
  SET @RENTALVALUE_ADDITIONAL = 0.00                      

 --  SATELLITE DISHES                           
 SELECT                                                                                  
  @SATELLITEDISHES_INCLUDE     = ISNULL(LIMIT_1,'500.00'),                                                
  @SATELLITEDISHES_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0.00')                                                                    
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                              
 WHERE                                                                      
   COVERAGE_CODE_ID=786   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                        
 IF @SATELLITEDISHES_INCLUDE IS NULL                                                            
  SET @SATELLITEDISHES_INCLUDE = 500.00                                                                    
                                                                     
 IF @SATELLITEDISHES_ADDITIONAL IS NULL                                                                     
  SET @SATELLITEDISHES_ADDITIONAL =0.00                   
         
 -----CONTENTS IN STORAGE                                                                    
 SELECT                                            
  @CONTENTSINSTORAGE_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @CONTENTSINSTORAGE_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                 
 FROM                                                                         
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                   
   COVERAGE_CODE_ID=782   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                 
 IF ISNULL(@CONTENTSINSTORAGE_INCLUDE,'')='' OR @CONTENTSINSTORAGE_INCLUDE='0'                                                    
  SET @CONTENTSINSTORAGE_INCLUDE =''                                                                    
                                                                     
 IF ISNULL(@CONTENTSINSTORAGE_ADDITIONAL,'')=''                                                    
  SET @CONTENTSINSTORAGE_ADDITIONAL= '0'                                                                    
                                                                     
                                                                     
 -- BUILDING IMPROVEMENTS, ALTERATIONS & ADDITIONS                            
                                                                     
 SELECT                                                                                  
  @BUILDINGIMPROVEMENTS_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                
  @BUILDINGIMPROVEMENTS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')              
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                       
 WHERE                                                                                   
   COVERAGE_CODE_ID=780   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                           
              
 IF ISNULL(@BUILDINGIMPROVEMENTS_INCLUDE,'')='' OR @BUILDINGIMPROVEMENTS_INCLUDE='0'                                                    
  SET @BUILDINGIMPROVEMENTS_INCLUDE =''                
                                                                     
 IF ISNULL(@BUILDINGIMPROVEMENTS_ADDITIONAL,'')=''                                                    
  SET @BUILDINGIMPROVEMENTS_ADDITIONAL= '0'               
                                                                     
 ---  TREE SHURBS   - TSPL                                                                    
 SELECT                                                                                  
 @TREESLAWNSSHRUBS_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
 @TREESLAWNSSHRUBS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                     
 FROM                                          
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)           
 WHERE                                                               
   COVERAGE_CODE_ID=784   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID =@DWELLINGID                                                                                  
                                              
 IF ISNULL(@TREESLAWNSSHRUBS_INCLUDE,'')='' OR @TREESLAWNSSHRUBS_INCLUDE='0'      
  SET @TREESLAWNSSHRUBS_INCLUDE =''                                                                    
                                                                     
 IF ISNULL(@TREESLAWNSSHRUBS_ADDITIONAL,'')=''                                                    
  SET @TREESLAWNSSHRUBS_ADDITIONAL= '0'                                                                            
                 
 ----   RADIO & TELEVISION EQUIPMENT                                
 SELECT                                                                                  
  @RADIOTV_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @RADIOTV_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                            
 FROM                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                         
   COVERAGE_CODE_ID=785   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                             
                                                                     
 IF ISNULL(@RADIOTV_INCLUDE,'')='' OR @RADIOTV_INCLUDE='0'                                                              
  SET @RADIOTV_INCLUDE =''                                                                    
                                                                     
 IF ISNULL(@RADIOTV_ADDITIONAL,'')=''                                                    
  SET @RADIOTV_ADDITIONAL= '0'                                                                    
                                                                  
 ----- AWNINGS, CANOPIES OR SIGNS                                                                                 
 SELECT                                                                                  
  @AWNINGSCANOPIES_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                   
  @AWNINGSCANOPIES_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')          
 FROM                                            
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                           
 WHERE                                                                                   
   COVERAGE_CODE_ID=787   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                       
                                                                             
                                                                     
 IF ISNULL(@AWNINGSCANOPIES_INCLUDE,'')='' OR @AWNINGSCANOPIES_INCLUDE='0'                                                 
  SET @AWNINGSCANOPIES_INCLUDE =''                                                                    
                                                                     
 IF ISNULL(@AWNINGSCANOPIES_ADDITIONAL,'')=''                                                    
  SET @AWNINGSCANOPIES_ADDITIONAL= '0'                                                                
                                                                     
                             
 ------   INSTALLATION FLOATER - BUILDING MATERIALS (IF-184)             
 SELECT                                                                                  
  @FLOATERBUILDINGMATERIALS_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @FLOATERBUILDINGMATERIALS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')            
 FROM                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                                   
   COVERAGE_CODE_ID=790   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                             
                                                                     
 IF ISNULL(@FLOATERBUILDINGMATERIALS_INCLUDE,'')='' OR @FLOATERBUILDINGMATERIALS_INCLUDE='0'                                                              
  SET @FLOATERBUILDINGMATERIALS_INCLUDE =''                                                                    
                                                                    
 IF ISNULL(@FLOATERBUILDINGMATERIALS_ADDITIONAL,'')=''                                                    
  SET @FLOATERBUILDINGMATERIALS_ADDITIONAL= '0'                  
           
 ------  INSTALLATION FLOATER - NON-STRUCTURAL EQUIPMENT (IF-184)           
 SELECT                                                                                  
  @FLOATERNONSTRUCTURAL_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @FLOATERNONSTRUCTURAL_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')          
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                               
 WHERE                                                   
   COVERAGE_CODE_ID=791   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID      
                                                                             
 IF ISNULL(@FLOATERNONSTRUCTURAL_INCLUDE,'')='' OR @FLOATERNONSTRUCTURAL_INCLUDE='0'                                                    
  SET @FLOATERNONSTRUCTURAL_INCLUDE =''                                                                 
                                                                             
 IF ISNULL(@FLOATERNONSTRUCTURAL_ADDITIONAL,'')=''                                                                             
  SET @FLOATERNONSTRUCTURAL_ADDITIONAL= '0'                                                                            
                                                         
                                            
 ---- MINE SUBSIDENCE COVERAGE (DP-480)                  
 SET @MINESUBSIDENCEDP480='N'                                                                    
 IF EXISTS                 
 (SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID=792   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID )                                          





  
    
      
         
  SET @MINESUBSIDENCEDP480='Y'                                                                    
 ELSE                                                                 
  SET @MINESUBSIDENCEDP480='N'                  
                                                                   
 SELECT @MINESUBSIDENCE_ADDITIONAL =isnull(DEDUCTIBLE_1,'0') FROM APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK) WHERE COVERAGE_CODE_ID=792   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID    





  
                  
 -----  EARTHQUAKE (DP-469)  -  EDP469          
 IF EXISTS(SELECT * FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID=788   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID)                              



  
    
      
         
  SET @EARTHQUAKEDP469='Y'                                                                    
 ELSE                                                                    
  SET @EARTHQUAKEDP469='N'                                                                    
                                   
 ----- INCIDENTAL OFFICE OCCUPANCY (BY INSURED) --  IOO              
 IF EXISTS(SELECT * FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID=816   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID)                                            


 



  
    
      
         
   SET @INCIDENTALOFFICE='Y'                                                        
 ELSE                                   
  SET @INCIDENTALOFFICE='N'                                      
END                   
                
--------------------------- --  END OF COVERAGES FOR INDIANA             
-----------------------------------------  COVERAGES FOR MICHIGAN  ----------------------------------------------------------------------------------------------------------------                                                                            





 
     
                                         
IF @STATE_ID=22    -- START OF COVERAGES FOR MICHIGAN                                                             
BEGIN                                                                     
 ----  COVERAGE - B   COV_ID=774                                                                        
 SET @APPURTENANTSTRUCTURES_ADDITIONAL=0                                 
                                                                      
 SELECT                                              
  @APPURTENANTSTRUCTURES_INCLUDE     = ISNULL(LIMIT_1,'0.00')                                                         
                                                                    
 FROM                                                    
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                
 WHERE                                                                                   
   COVERAGE_CODE_ID = 794   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID =@DWELLINGID      
 
SELECT                                              
                                                                     
  @APPURTENANTSTRUCTURES_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                      
 FROM                                                    
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                
 WHERE                                                                                   
   COVERAGE_CODE_ID = 799   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID =@DWELLINGID               
                                                                             
 IF  @APPURTENANTSTRUCTURES_INCLUDE IS NULL                                                                 
  SET @APPURTENANTSTRUCTURES_INCLUDE = 0.00                                                               
                                                                 
 IF  @APPURTENANTSTRUCTURES_ADDITIONAL IS NULL                                                                 
  SET @APPURTENANTSTRUCTURES_ADDITIONAL = 0.00                                                                
                                                            
 -----  PERSONEL PROPERTY  - COVERAGE - C  -  LPP                                                                        
                                                                     
 SET @PERSONALPROPERTY_ADDITIONAL=0                                                             
 SELECT                                                                                  
  @PERSONALPROPERTY_INCLUDE     = ISNULL(limit_1,'0.00'),                
  @PERSONALPROPERTY_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                    
 FROM                                                                     
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                
 WHERE                                                                                   
   COVERAGE_CODE_ID=795   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                    
                                                                            
 IF  @PERSONALPROPERTY_INCLUDE IS NULL                                 
  SET @PERSONALPROPERTY_INCLUDE = 0.00            
                                                                 
 IF  @PERSONALPROPERTY_ADDITIONAL IS NULL                                                                 
  SET @PERSONALPROPERTY_ADDITIONAL = 0.00                                                       
                                                  
 -----  RENTAL VALUE - COVERAGE - D  -  RV                                                                       
 SET @RENTALVALUE_ADDITIONAL=0                                                   
 SELECT                                                                                  
  @RENTALVALUE_INCLUDE     = ISNULL(limit_1,'0.00'),                                                             
  @RENTALVALUE_ADDITIONAL  =  ISNULL(DEDUCTIBLE_1,'0.00')                     
 FROM                                                                    
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                            
   COVERAGE_CODE_ID=796   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                  
 IF  @RENTALVALUE_INCLUDE IS NULL                             
  SET @RENTALVALUE_INCLUDE = 0.00                                                            
                                                                 
 IF  @PERSONALPROPERTY_ADDITIONAL IS NULL                       
  SET @RENTALVALUE_ADDITIONAL = 0.00                                                                
                                                                 
 --  SATELLITE DISHES   - SD                                          
 SELECT                                                                                  
  @SATELLITEDISHES_INCLUDE     = ISNULL(LIMIT_1,'500'),                                                         
  @SATELLITEDISHES_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0.00')           
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                             
 WHERE                                                                                   
   COVERAGE_CODE_ID=806   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                   
 IF @SATELLITEDISHES_INCLUDE IS NULL                                                                     
  SET @SATELLITEDISHES_INCLUDE ='500'                                
                                                                     
 IF @SATELLITEDISHES_ADDITIONAL IS NULL                                     
  SET @SATELLITEDISHES_ADDITIONAL ='0.00'      
                                                                     
 -- CONTENTS IN STORAGE    CS                                        
 SELECT                                                             
  @CONTENTSINSTORAGE_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @CONTENTSINSTORAGE_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                  
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                             
 WHERE                                                                                   
   COVERAGE_CODE_ID=802   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                
                                                                             
 IF ISNULL(@CONTENTSINSTORAGE_INCLUDE,'')='' OR @CONTENTSINSTORAGE_INCLUDE='0'                                             
  SET @CONTENTSINSTORAGE_INCLUDE =''                                                                    
                                                                     
 IF ISNULL(@CONTENTSINSTORAGE_ADDITIONAL,'')=''                     
  SET @CONTENTSINSTORAGE_ADDITIONAL= '0'                   
                                                                     
 -- BUILDING IMPROVEMENTS, ALTERATIONS & ADDITIONS --  BIAA              
 SELECT                                   
  @BUILDINGIMPROVEMENTS_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @BUILDINGIMPROVEMENTS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')               
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                                   
   COVERAGE_CODE_ID=800   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                            
                                                                             
                                                  
 IF ISNULL(@BUILDINGIMPROVEMENTS_INCLUDE,'')='' OR @BUILDINGIMPROVEMENTS_INCLUDE='0'                                                    
  SET @BUILDINGIMPROVEMENTS_INCLUDE =''                                                                    
                                                                
 IF ISNULL(@BUILDINGIMPROVEMENTS_ADDITIONAL,'')=''                                                    
  SET @BUILDINGIMPROVEMENTS_ADDITIONAL= '0'                                                      
                                                                 
 ---- MINE SUBSIDENCE COVERAGE (DP-480)                                                                   
 ---  IN MICHIGAN THER IS IS NO MINE SUBSIDENCE COVERAGE SO SET IT ALWAY 'N'                                                              
 SET @MINESUBSIDENCEDP480='N'                                                               
                 
 ---  TREE SHURBS   - TSPL                                          
 SELECT                                                                                  
  @TREESLAWNSSHRUBS_INCLUDE     = ISNULL(LIMIT_1,'0.00'),                                                                    
  @TREESLAWNSSHRUBS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0.00')                                                                 
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                         
 WHERE                                                                                   
   COVERAGE_CODE_ID=804   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                  
                                                                             
                                                                     
 IF @TREESLAWNSSHRUBS_INCLUDE IS NULL                                                                     
   SET @TREESLAWNSSHRUBS_INCLUDE = '0'                                                                    
                                                                     
 IF @TREESLAWNSSHRUBS_ADDITIONAL IS NULL                                                              
  SET @TREESLAWNSSHRUBS_ADDITIONAL = '0'                                                              
                                                                     
 ----   RADIO & TELEVISION EQUIPMENT   -  RTE                                                          
 SELECT                                                                         
  @RADIOTV_INCLUDE  = ISNULL(LIMIT_1,'0'),                                                            
  @RADIOTV_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                                                 
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                 
 WHERE                                           
 COVERAGE_CODE_ID=805   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                       
                                                                             
 IF @RADIOTV_INCLUDE is null                                                  
  SET @RADIOTV_INCLUDE =''                                                           
                                                              
 IF ISNULL(@RADIOTV_ADDITIONAL,'')=''                                                    
  SET @RADIOTV_ADDITIONAL= '0'                                                               
                                          
 ----- AWNINGS, CANOPIES OR SIGNS   -  ACS                                                                    
 SELECT                                                                                  
  @AWNINGSCANOPIES_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @AWNINGSCANOPIES_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                                                
 FROM                                                                       
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                                   
   COVERAGE_CODE_ID=807   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                                                                            
                                                                    
 IF @AWNINGSCANOPIES_INCLUDE is null OR @AWNINGSCANOPIES_INCLUDE='0'                                                
  SET @AWNINGSCANOPIES_INCLUDE =''                                                                    
  
 IF ISNULL(@AWNINGSCANOPIES_ADDITIONAL,'')=''                                                    
  SET @AWNINGSCANOPIES_ADDITIONAL= '0'                                                                    
                         
 ------   INSTALLATION FLOATER - BUILDING MATERIALS (IF-184) ---  IF184                                                                    
 SELECT                                                      
  @FLOATERBUILDINGMATERIALS_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @FLOATERBUILDINGMATERIALS_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')                                                                    
 FROM                                                                                   
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                       
 WHERE                                                                                   
   COVERAGE_CODE_ID=810   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                            
                                                                     
 IF ISNULL(@FLOATERBUILDINGMATERIALS_INCLUDE,'')='' OR @FLOATERBUILDINGMATERIALS_INCLUDE='0'                                                                   
  SET @FLOATERBUILDINGMATERIALS_INCLUDE =''                                                        
                                                                  
 IF ISNULL(@FLOATERBUILDINGMATERIALS_ADDITIONAL,'')=''                            
  SET @FLOATERBUILDINGMATERIALS_ADDITIONAL= '0'                      
 
        
                                                                     
 ------  INSTALLATION FLOATER - NON-STRUCTURAL EQUIPMENT (IF-184)         
 SELECT                                        
  @FLOATERNONSTRUCTURAL_INCLUDE     = ISNULL(LIMIT_1,'0'),                                                                    
  @FLOATERNONSTRUCTURAL_ADDITIONAL  = ISNULL(DEDUCTIBLE_1,'0')             
 FROM                                                                  
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                               
 WHERE                                                                                 
   COVERAGE_CODE_ID=811   AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID                                   
                                                               
 IF ISNULL(@FLOATERNONSTRUCTURAL_INCLUDE,'')='' OR @FLOATERNONSTRUCTURAL_INCLUDE='0'                                                    
  SET @FLOATERNONSTRUCTURAL_INCLUDE =''                                                                 
                          
 IF ISNULL(@FLOATERNONSTRUCTURAL_ADDITIONAL,'')=''                                                                             
  SET @FLOATERNONSTRUCTURAL_ADDITIONAL= '0'                                                        
        
 -----  EARTHQUAKE (DP-469)  -  EDP469                              
 SET @EARTHQUAKEDP469='N'                                                     
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES                 
 WITH (NOLOCK) WHERE COVERAGE_CODE_ID=808   and CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID)                                                             
  SET @EARTHQUAKEDP469='Y'                                                                    
 ELSE                                                                    
  SET @EARTHQUAKEDP469='N'                 
                                             
 ----- INCIDENTAL OFFICE OCCUPANCY (BY INSURED) --  IOO                                                                    
 SET @INCIDENTALOFFICE='N'                                                                    
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID=815                
    AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID)                                                              
  SET @INCIDENTALOFFICE='Y'                                                                    
 ELSE                                         
  SET @INCIDENTALOFFICE='N'     
  
 ----- Limited Lead Liability                                                                    
 SET @LEADLIABILITY='N'    
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID=978                
    AND CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DWELLING_ID =@DWELLINGID)                                                              
  SET @LEADLIABILITY='Y'                                                                    
 ELSE                                         
  SET @LEADLIABILITY='N'               
END                  
-----------------------------------------------END OF COVERAGES FOR MICHIGAN                                                                    
                                                                    
                                                                    
                                                                    
---- ********************************************************************************************                                                                    
/*                                                                    
787 ACS Awnings, Canopies or Signs                                                              
789 BR1143 Builders Risk (DP-1143)                                   
780 BIAA Building Improvements, Alterations & Additions                                                                    
782 CS Contents in Storage                                                              
773 DWELL Coverage A - Dwelling                                                                    
774 OSTR Coverage B - Other Structures                                                                 
779 BOSTR Coverage B - Other Structures Rented to Others                                 
781 AC Coverage C - Additional Contents                                                          
775 LPP Coverage C - Landlords Personal Property                                                                    
783 ARV Coverage D - Additional Rental Value                                                                    
776 RV Coverage D - Rental Value                                                                    
293 PL Coverage E - Personal Liability                                       
777 CSL Coverage E - Personal Liability Each Occurrence                                                                    
778 MEDPM Coverage F - Medical Payment Each Person                                                         
788 EDP469 Earthquake (DP-469)                                                                    
816 IOO Incidental Office Occupancy (By Insured)                                                          
790 IF184 Installation Floater - Building Materials (IF-184)                                                                    
791 IFNSE Installation Floater - Non-Structural Equipment (IF-184)                                     
792 MSC480 Mine Subsidence Coverage (DP-480)                                                                    
785 RTE Radio & Television Equipment                                                                    
786 SD Satellite Dishes                                                                    
784 TSPL Trees, Shrubs, Plants & Lawns                                                                    
                                                                    
*/                                                  
---- ********************************************************************************************                                                                                            
                                 
                                             
--------------------------------------------COVERAGES RENTAL :                
                
---------DWELL                                                    
SELECT  @DWELLING_LIMITS = convert(decimal(18),ISNULL(COV_INFO.LIMIT_1,0))                                                                       
FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                                      
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                                      
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                              
    AND (COV_INFO.APP_ID = @APPID)                                                                                       
    AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                                       
    AND (COV_INFO.dwelling_id = @DWELLINGID) WHERE M.COV_CODE ='DWELL'    
---END DWELL                           
                                    
---------PL                                                 
SELECT  @PERSONALLIABILITY_LIMIT = ISNULL(COV_INFO.LIMIT_1,0)                                                                     
FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                 
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                 
    AND (COV_INFO.APP_ID = @APPID)                
    AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                
    AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='CSL'                                         
------END PL                                       
                                    
---------MEDPM                  
SELECT  @MEDICALPAYMENTSTOOTHERS_LIMIT = ISNULL(COV_INFO.LIMIT_1,0)                                   
FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                                      
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                                 
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                                                      
    AND (COV_INFO.APP_ID = @APPID)                                         
    AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                                       
    AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='MEDPM'                                                         
------END MEDPM                                
                            
---------APDI                                                            
SELECT  @DEDUCTIBLE = ISNULL(COV_INFO.DEDUCTIBLE,0)                                                                    
FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                                      
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                                      
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                               
    AND (COV_INFO.APP_ID = @APPID)                                                          
    AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                                       
    AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='APDI'                                                                      
------END APDI                              
                
-------MINE SUBSIDENCE COV LIMIT                
 IF(@MINESUBSIDENCEDP480='Y')                
                  
  SELECT  @MINESUBSIDENCEDP480_COVG_LIMIT = ISNULL(COV_INFO.DEDUCTIBLE_1,0)                                                                 
  FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                                      
  INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                                 
  AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                                                      
  AND (COV_INFO.APP_ID = @APPID)                                         
  AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                                       
  AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='MSC480'                  
                  
 ELSE             
 SET @MINESUBSIDENCEDP480_COVG_LIMIT =''                
                   
                       
-------------------------------------------END Coverages Rental : Modified on 11 July 2006           
IF(@DWELLING_LIMITS IS NOT  NULL)                                                                        
 BEGIN                                                                        
  SET @COVERAGEVALUE = CAST(@DWELLING_LIMITS AS real) / 1000                                                                    
 END                                                                        
ELSE                                                                        
 BEGIN                                                                        
  SET @COVERAGEVALUE = 0                                                                        
 END                                             
                                                                    
                                          
---APP_DWELLING_COVERAGE - END                                                                    
--------------------------------------------        
    
                             
DECLARE @LOCATION_TYPE INT                            
SELECT @LOCATION_TYPE = ISNULL(LOCATION_TYPE,11848) FROM APP_LOCATIONS                            
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and LOCATION_ID = @LOCATIONID          
                            
IF(@LOCATION_TYPE = 11848) --RENTED                            
BEGIN                            
 SET @SEASONALSECONDARY='N'                            
END                            
ELSE --SEASONAL/SECONDARY                            
BEGIN                            
 SET @SEASONALSECONDARY='Y'                            
END                            
                 
 --END MULTOIPOLICY                                                   
                                                                 
SELECT                                                              
 @N0_LOCAL_ALARM    = ISNULL(NUM_LOC_ALARMS_APPLIES,0),                                               
 @NUMBEROFFAMILIES  = ISNULL(NO_OF_FAMILIES,'0'),                                                                    
                                                                 
 @DISTANCET_FIRESTATION  = ISNULL(FIRE_STATION_DIST,'0'),                                                                    
 @DWELL_UND_CONSTRUCTION_DP1143= CASE ISNULL(IS_UNDER_CONSTRUCTION,'0')                                                                    
  WHEN 1 THEN 'Y'                                                                    
  ELSE 'N'                                                                    
  END                                 
FROM                                                                     
 APP_HOME_RATING_INFO WITH (NOLOCK)                                                               
WHERE                          
 CUSTOMER_ID = @CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID=@DWELLINGID                                                                        
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





  
    
  SELECT    
      @FEET2HYDRANT      =  LOOKUP_VALUE_DESC        
    FROM                                                                     
 APP_HOME_RATING_INFO WITH (NOLOCK) INNER  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                              
 ON APP_HOME_RATING_INFO.HYDRANT_DIST = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                      
WHERE                          
 CUSTOMER_ID = @CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID=@DWELLINGID              
                                                        
----********************** EXTERIOR CONSTRUCTION  ****************----------                                
                                                                    
SELECT                                                                      
 @EXTERIOR_CONSTRUCTION_DESC = ISNULL(UPPER(MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC),'') ,                                                           
 @EXTERIOR_CONSTRUCTION      = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID,                                
 @EXTERIOR_CONSTRUCTION_F_M  = MNT_LOOKUP_VALUES.LOOKUP_FRAME_OR_MASONRY --Picking Code from the LOOKUP           
FROM                    
 APP_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES  WITH (NOLOCK)                                                                   
 ON APP_HOME_RATING_INFO.EXTERIOR_CONSTRUCTION = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                    
WHERE                                      
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DWELLING_ID=@DWELLINGID                                                                       
                
IF(@EXTERIOR_CONSTRUCTION_DESC IS NULL)                      
 SET @EXTERIOR_CONSTRUCTION_DESC=''                                                            
                                                            
 ----******************END**** EXTERIOR CONSTRUCTION  ****************----------                                                                   
                            
--- START - APP_HOME_OWNER_GEN_INFO                                                                
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



 


  
    
      
        
                 
                                                                    
SELECT                 
 @MULTIPLEPOLICYFACTOR = ISNULL(MULTI_POLICY_DISC_APPLIED,'N'),                                                                         
 @NO_YEARS_WITH_WOLVERINE=ISNULL(YEARS_INSU_WOL,'0')   ,                                                        
  @WOODSTOVE_SURCHARGE  =  case  ISNULL(ANY_HEATING_SOURCE,'0')                    
    WHEN '1' THEN 'Y'                                                                      
    ELSE 'N'                                                        
    END                                                             
FROM                                                                     
 APP_HOME_OWNER_GEN_INFO WITH (NOLOCK)                                                             
WHERE                                                              
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                                          
                                                                    
IF ISNULL(@WOODSTOVE_SURCHARGE,'') !='Y'                                                        
 SET @WOODSTOVE_SURCHARGE='N'                 
                
IF @MULTIPLEPOLICYFACTOR = '1'                                  
 SET @MULTIPLEPOLICYFACTOR = 'Y'                                                                  
ELSE                                                                  
 SET @MULTIPLEPOLICYFACTOR = 'N'                                                         
                                                                    
                                                   
---END APP_HOME_OWNER_GEN_INFO                                                                    
                                                               
-------------FORM_CODE-------------                                          
                                                        
DECLARE @FIRE_STATION_DIST int                                          
DECLARE @HYDRANT_DIST varchar(20)                                          
                                          
SELECT          
@PROTECTIONCLASS = ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0'),        
@FIRE_STATION_DIST = ISNULL(FIRE_STATION_DIST,0),                                                                    
@HYDRANT_DIST = ISNULL(HYDRANT_DIST,'0')                                                              
FROM   
 APP_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                                                                    
 ON APP_HOME_RATING_INFO.PROT_CLASS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                      
WHERE                                  
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID=@DWELLINGID                             
  
SET @FIREPROTECTIONCLASS =  @PROTECTIONCLASS   
                                          
EXECUTE @FORM_CODE = Proc_GetProtectionClass @PROTECTIONCLASS,@FIRE_STATION_DIST,@HYDRANT_DIST,'REDW','RATES' /**RATES IF CALLED FROM RATES*/                                          
IF (LEN(@FORM_CODE)=1)                                          
SET @PROTECTIONCLASS = '0' + @FORM_CODE  /*Protection class depends on the rated class not the one that is selected*/                                          
ELSE                                           
SET @PROTECTIONCLASS = @FORM_CODE                                          
                                
SET @FORM_CODE= @FORM_CODE + @EXTERIOR_CONSTRUCTION_F_M --(Format 4M or 4F)   
                                
-----------------END FORM_CODE-------------                          
------------------------------------------------------------------------------------------                                                                                                      
 if ISNULL(@PRODUCT_PREMIER,'')=''                         
       SET @PRODUCT_PREMIER=''   


	SET @TEMPEXPFEE='Y'              
---------------------------RETURN VALUES--------------------------                
SELECT                                                                        
   @LOB_ID AS LOB_ID,                                                            
   @POLICY_ID             AS POLICY_ID,                                                       
   @TERRITORYCODES        AS TERRITORYCODES,                                                      
   @INSURANCESCORE         AS INSURANCESCORE,                                                                        
   @TERRITORYZONE         AS TERRITORYZONE,                                                           
   @TERRITORYNAME         AS TERRITORYNAME,                                                                        
   @TERRITORYCOUNTY        AS TERRITORYCOUNTY,                                                
   @EARTHQUAKEZONE   AS EARTHQUAKEZONE,                          
   @STATENAME                AS  STATENAME,                                                                        
   'Y'                 AS NEWBUSINESSFACTOR,                                                
   @QUOTEEFFDATE             AS QUOTEEFFDATE,                                                                        
   @QUOTEEXPDATE             AS QUOTEEXPDATE,                
   @APP_NUMBER   AS APP_NUMBER,                
   @APP_VERSION   AS APP_VERSION,                       
   @TERMFACTOR               AS TERMFACTOR  ,                                                                    
   @PRODUCTNAME              AS PRODUCTNAME,                         
   @PRODUCT_PREMIER           AS PRODUCT_PREMIER,                                                       
   @SEASONALSECONDARY         AS SEASONALSECONDARY,                                                                        
   @REPLACEMENTCOSTFACTOR       AS REPLACEMENTCOSTFACTOR,                                                                        
   @DWELLING_LIMITS             AS DWELLING_LIMITS,                                                
   @DEDUCTIBLE                  AS DEDUCTIBLE,                                       
   @PROTECTIONCLASS             AS PROTECTIONCLASS,                                                                        
  @DISTANCET_FIRESTATION       AS DISTANCET_FIRESTATION,                                                                        
   @FEET2HYDRANT                AS FEET2HYDRANT,                                                                        
                
   @EXTERIOR_CONSTRUCTION        AS EXTERIOR_CONSTRUCTION,                                         
   @EXTERIOR_CONSTRUCTION_DESC   AS EXTERIOR_CONSTRUCTION_DESC,                                                            
   @EXTERIOR_CONSTRUCTION_F_M    AS EXTERIOR_CONSTRUCTION_F_M,                                                              
   @DWELL_UND_CONSTRUCTION_DP1143   AS DWELL_UND_CONSTRUCTION_DP1143,                                                                        
                
   @DOC                           AS DOC,                 
   @AGEOFHOME                      AS AGEOFHOME,                      
   @NUMBEROFFAMILIES               AS NUMBEROFFAMILIES,                      
   @N0_LOCAL_ALARM                 AS N0_LOCAL_ALARM,                      
   @NO_YEARS_WITH_WOLVERINE        AS NO_YEARS_WITH_WOLVERINE,                      
   @LOSSFREE                       AS LOSSFREE,                      
   @NOTLOSSFREE                       AS NOTLOSSFREE,     
   @MULTIPLEPOLICYFACTOR           AS MULTIPLEPOLICYFACTOR,                                                                        
   @FORM_CODE                    AS FORM_CODE,                                                                  
   @COVERAGEVALUE                 AS COVERAGEVALUE,                                 
                                                                      
   ----  OPTIONAL COVERAGES PAGE  ----                                                                        
                                                
   @PERSONALLIABILITY_LIMIT             AS PERSONALLIABILITY_LIMIT,                                                                        
   @MEDICALPAYMENTSTOOTHERS_LIMIT       AS MEDICALPAYMENTSTOOTHERS_LIMIT,                                                                        
   @EARTHQUAKEDP469         AS EARTHQUAKEDP469,                                                                        
   @INCIDENTALOFFICE                 AS INCIDENTALOFFICE,                     
   @MINESUBSIDENCEDP480                AS MINESUBSIDENCEDP480,                
   @MINESUBSIDENCEDP480_COVG_LIMIT  AS MINESUBSIDENCEDP480_COVG_LIMIT,                                       
   @APPURTENANTSTRUCTURES_INCLUDE       AS APPURTENANTSTRUCTURES_INCLUDE,                                                    
   @APPURTENANTSTRUCTURES_ADDITIONAL    AS APPURTENANTSTRUCTURES_ADDITIONAL,                                                                        
   @BUILDINGIMPROVEMENTS_INCLUDE        AS BUILDINGIMPROVEMENTS_INCLUDE,                                                                        
   @BUILDINGIMPROVEMENTS_ADDITIONAL     AS BUILDINGIMPROVEMENTS_ADDITIONAL,                                                                        
   @RENTALVALUE_INCLUDE                AS RENTALVALUE_INCLUDE,                                      
   @RENTALVALUE_ADDITIONAL              AS RENTALVALUE_ADDITIONAL,                                                            
   @PERSONALPROPERTY_INCLUDE            AS PERSONALPROPERTY_INCLUDE,                                                                        
   @PERSONALPROPERTY_ADDITIONAL         AS PERSONALPROPERTY_ADDITIONAL,                                                                        
   @CONTENTSINSTORAGE_INCLUDE           AS CONTENTSINSTORAGE_INCLUDE,                                                                        
   @CONTENTSINSTORAGE_ADDITIONAL        AS CONTENTSINSTORAGE_ADDITIONAL,                                    
   @TREESLAWNSSHRUBS_INCLUDE            AS TREESLAWNSSHRUBS_INCLUDE,                            
   @TREESLAWNSSHRUBS_ADDITIONAL         AS TREESLAWNSSHRUBS_ADDITIONAL,                                                                        
   @RADIOTV_INCLUDE                AS RADIOTV_INCLUDE,                                                                        
   @RADIOTV_ADDITIONAL              AS RADIOTV_ADDITIONAL,                                              
   @SATELLITEDISHES_INCLUDE             AS SATELLITEDISHES_INCLUDE,                                                            
   @SATELLITEDISHES_ADDITIONAL          AS SATELLITEDISHES_ADDITIONAL,                                                                        
   @AWNINGSCANOPIES_INCLUDE             AS AWNINGSCANOPIES_INCLUDE,                                                                      
   @AWNINGSCANOPIES_ADDITIONAL       AS AWNINGSCANOPIES_ADDITIONAL,                                                 
   @FLOATERBUILDINGMATERIALS_INCLUDE    AS FLOATERBUILDINGMATERIALS_INCLUDE,                                                                        
   @FLOATERBUILDINGMATERIALS_ADDITIONAL AS FLOATERBUILDINGMATERIALS_ADDITIONAL,                                              
   @FLOATERNONSTRUCTURAL_INCLUDE      AS FLOATERNONSTRUCTURAL_INCLUDE,                                                                        
   @FLOATERNONSTRUCTURAL_ADDITIONAL     AS FLOATERNONSTRUCTURAL_ADDITIONAL,                                                           
   @INSUREWITHWOL                       AS INSUREWITHWOL  ,                                                        
   @WOODSTOVE_SURCHARGE                 AS WOODSTOVE_SURCHARGE   ,                                                    
   @VALUESCUSTOMERAPP                 AS VALUESCUSTOMERAPP,                                                              
   @MINESUBSIDENCE_ADDITIONAL       AS MINESUBSIDENCE_ADDITIONAL, 
   @FIREPROTECTIONCLASS  AS FIREPROTECTIONCLASS,  
   @LEADLIABILITY AS LEADLIABILITY,
   @TEMPEXPFEE AS TEMPEXPFEE                                                    
END                                                                   
                



GO

