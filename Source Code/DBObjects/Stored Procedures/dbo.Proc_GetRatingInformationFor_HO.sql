IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationFor_HO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationFor_HO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                      
Proc Name           : Dbo.Proc_GetRatingInformationFor_HO                                                                             
Created by          : praveen singh                                                                                     
Date                : 08/02/2006                                                                                      
Purpose             : To get the information for creating the input xml for Homeownwrs                                                                                      
Revison History     :                                                                                      
Used In             : Wolverine                                                                                      
------------------------------------------------------------                                                                                      
Date     Review By          Comments                                                                                      
22-FEB-07  ASFA PRAVEEN  EarthQuakeZone info is added     
------   ------------       -------------------------*/               

-- DROP PROC dbo.Proc_GetRatingInformationFor_HO 1258,56,1,2                                     
                                                                                                                                                                                                 
create PROC dbo.Proc_GetRatingInformationFor_HO                                      
(                                                                                          
@CUSTOMERID     INT,                                                                                          
@APPID     INT,                                                                                          
@APPVERSIONID   INT,                                                                                          
@DWELLINGID     INT                                                                                          
)                                                                                          
AS                                                                                          
                                                                                          
BEGIN                                                                                          
SET QUOTED_IDENTIFIER OFF                                                                                          
                                                                                        
-- DELARATION OF ALL VARIABLES / GENERAL VARIABLES /BASIC POLICY PAGE--                 
DECLARE         @LOB_ID                       nvarchar(20)                                                                                       
DECLARE         @STATENAME                    nvarchar(20)                                                                                        
DECLARE         @STATE_ID                     nvarchar(20)                                                                                          
DECLARE         @QUOTEEFFDATE                 nvarchar(20)                                                                                          
DECLARE         @QUOTEEXPDATE                 nvarchar(20)                                                                                         
DECLARE         @TERMFACTOR                   nvarchar(20)                                                                               
DECLARE         @SEASONALSECONDARY            nvarchar(20)                                  
DECLARE         @WOLVERINEINSURESPRIMARY   nvarchar(20)    
DECLARE         @PRODUCTNAME        nvarchar(20)                                                                                          
DECLARE         @PRODUCT_PREMIER nvarchar(20)                                                                                          
DECLARE         @REPLACEMENTCOSTFACTOR    nvarchar(25)                  
DECLARE         @DWELLING_LIMITS    decimal(20)                                  
DECLARE         @PROTECTIONCLASS              nvarchar(20)  
DECLARE         @FIREPROTECTIONCLASS          nvarchar(20)                                                                                           
DECLARE     	@DISTANCET_FIRESTATION      nvarchar(20)                          
DECLARE         @FEET2HYDRANT                 nvarchar(20)                                                                           
DECLARE         @DEDUCTIBLE                   nvarchar(20)                                                                                          
DECLARE         @EXTERIOR_CONSTRUCTION        nvarchar(20)                     
DECLARE    		@EXTERIOR_CONSTRUCTION_DESC   NVARCHAR(250)                                    
DECLARE         @EXTERIOR_CONSTRUCTION_F_M    nvarchar(20)                                                   
DECLARE         @DOC                          nvarchar(20)                                                       
DECLARE         @AGEOFHOME                    nvarchar(20)                                                       
DECLARE         @NUMBEROFFAMILIES             nvarchar(20)                                                                                          
DECLARE         @NUMBEROFUNITS                nvarchar(20)                                                                                          
DECLARE         @PERSONALLIABILITY_LIMIT      nvarchar(20)                                                                                          
DECLARE         @PERSONALLIABILITY_PREMIUM    nvarchar(20)                                                                                          
DECLARE         @PERSONALLIABILITY_DEDUCTIBLE decimal                                                                                
DECLARE         @INSURANCESCOREDIS            nvarchar(20)                                                                      
DECLARE         @MEDICALPAYMENTSTOOTHERS_LIMIT          nvarchar(20)                                                                                
DECLARE         @MEDICALPAYMENTSTOOTHERS_PREMIUM        nvarchar(20)                                                                
DECLARE         @FORM_CODE                       nvarchar(20)                                                                             
DECLARE         @PRIORLOSSSURCHARGE                     nvarchar(1)                                                                            
DECLARE         @HO20                        nvarchar(20)                                                                               
DECLARE         @HO21                        nvarchar(1)                                                                                      
DECLARE         @HO22                        nvarchar(1)                                                                                     
DECLARE         @HO23               nvarchar(1)                         
DECLARE         @HO24               nvarchar(1)                                                                                       
DECLARE			@HO25                        nvarchar(1)                                                                                          
DECLARE         @HO34                        nvarchar(1)                                                                
DECLARE			@HO11                        nvarchar(1)                                                              
DECLARE        @HO32  nvarchar(1)             
DECLARE  @HO277               nvarchar(1)                                                               
DECLARE         @HO455                       nvarchar(1)                                                                                          
DECLARE         @HO327                       nvarchar(10)                                                                                     
DECLARE         @HO33         nvarchar(20)                                                                                     
DECLARE         @HO315                     nvarchar(1)                                       
DECLARE         @HO9                         nvarchar(1)                            
DECLARE         @HO287                       nvarchar(1)                                                                    
DECLARE         @HO200                       nvarchar(1)                
DECLARE         @HO64RENTERDELUXE            nvarchar(1)                                                            
DECLARE         @HO66CONDOMINIUMDELUXE       nvarchar(1)    
DECLARE         @HO96FINALVALUE              nvarchar(20)                                                                                          
DECLARE         @HO96INCLUDE                 nvarchar(20)                                                                                          
DECLARE         @HO96ADDITIONAL              nvarchar(20)                                                
DECLARE         @HO48INCLUDE                 nvarchar(20)                                                                                          
DECLARE         @HO48ADDITIONAL              decimal                                                                   
DECLARE         @HO40ADDITIONAL              nvarchar(20)                                                                                          
DECLARE         @HO42              nvarchar(20)                                                              
DECLARE         @REPAIRCOSTINCLUDE           nvarchar(20)                                                                                          
DECLARE         @REPAIRCOSTADDITIONAL        nvarchar(20)                                                                                         
DECLARE         @PERSONALPROPERTYINCREASEDLIMITADDITIONAL  nvarchar(20)                                                                                          
DECLARE         @PERSONALPROPERTYAWAYADDITIONAL            nvarchar(20)                                                                                        
DECLARE         @UNSCHEDULEDJEWELRYADDITIONAL              nvarchar(20)                                                                                    
DECLARE         @MONEYADDITIONAL                   nvarchar(20)                                                              
DECLARE         @SECURITIESADDITIONAL              nvarchar(20)                           
DECLARE         @SILVERWAREADDITIONAL              nvarchar(20)                                                    
DECLARE         @FIREARMSADDITIONAL                nvarchar(20)                                                                  
DECLARE         @HO312ADDITIONAL                   nvarchar(20)                                       
DECLARE         @ADDITIONALLIVINGEXPENSEINCLUDE    nvarchar(20)                                              
DECLARE         @ADDITIONALLIVINGEXPENSEADDITIONAL nvarchar(20)                                    
DECLARE         @HO53INCLUDE                       nvarchar(20)     
DECLARE         @HO53ADDITIONAL                    nvarchar(20) 
DECLARE         @HO35INCLUDE              nvarchar(20)                                        
DECLARE         @HO35ADDITIONAL                    nvarchar(20)       
DECLARE         @SPECIFICSTRUCTURESINCLUDE         nvarchar(20)       
DECLARE         @SPECIFICSTRUCTURESADDITIONAL      nvarchar(20)    
    
--LIABILITY OPTIONS--                                                                                              
                                                                              
DECLARE         @OCCUPIED_INSURED                 nvarchar(20)                                                                                          
DECLARE         @RESIDENCE_PREMISES               nvarchar(20)                                                      
DECLARE         @OTHER_LOC_1FAMILY                nvarchar(20)                                                                                          
DECLARE         @OTHER_LOC_2FAMILY                nvarchar(20)                                                                                          
DECLARE         @ONPREMISES_HO42                  nvarchar(20)                                                                                          
DECLARE         @LOCATED_OTH_STRUCTURE            nvarchar(20)                                                                              
DECLARE         @INSTRUCTIONONLY_HO42             nvarchar(20)                                                                                          
DECLARE         @OFF_PREMISES_HO43                nvarchar(20)                                                                                     
DECLARE         @PIP_HO82                    nvarchar(20)                                                                                          
DECLARE         @RESIDENCE_EMP_NUMBER             nvarchar(20)                                                                                          
DECLARE         @CLERICAL_OFFICE_HO71             nvarchar(20)                                                                                          
DECLARE         @SALESMEN_INC_INSTALLATION        nvarchar(20)                                                                                          
DECLARE         @TEACHER_ATHELETIC                nvarchar(20)                                                                          
DECLARE         @TEACHER_NOC                      nvarchar(20)                                                                                          
DECLARE         @INCIDENTAL_FARMING_HO72          nvarchar(20)                                                                                          
DECLARE         @OTH_LOC_OPR_EMPL_HO73            nvarchar(20)                                                
DECLARE         @OTH_LOC_OPR_OTHERS_HO73          nvarchar(20)                                                                                        
                                                               
 --CREDIT AND CHARGES--                                                            
                                                                                        
DECLARE         @LOSSFREE                    nvarchar(20)                                                                                          
DECLARE         @NOTLOSSFREE                 nvarchar(20)                                                                                          
DECLARE			@VALUEDCUSTOMER              nvarchar(20)                                                                                   
DECLARE         @MULTIPLEPOLICYFACTOR        nvarchar(20)                                                        
DECLARE         @NONSMOKER      nvarchar(20)                           
DECLARE         @EXPERIENCE    nvarchar(20)                                                                                          
DECLARE         @CONSTRUCTIONCREDIT          nvarchar(20)              
DECLARE         @REDUCTION_IN_COVERAGE_C     nvarchar(20)             
DECLARE         @N0_LOCAL_ALARM              nvarchar(20)                                                           
                
--PROTECTIVE DEVICE                                                                       
                
DECLARE         @BURGLER_ALERT_POLICE        nvarchar(20)                                                                                       
DECLARE         @FIRE_ALARM_FIREDEPT         nvarchar(20)                                 
DECLARE         @BURGLAR                     nvarchar(20)                                                                                          
DECLARE         @BURGLAR_ACORD               nvarchar(20)                                                               
DECLARE         @CENTRAL_FIRE                nvarchar(20)                    
DECLARE         @INSURANCESCORE              nvarchar(20)                                                    
DECLARE         @WOODSTOVE_SURCHARGE         nvarchar(20)                                                                                          
DECLARE         @PRIOR_LOSS_SURCHARGE        nvarchar(20)                                                                                          
DECLARE         @DOGSURCHARGE                nvarchar(20)                                                                                          
DECLARE         @DOGFACTOR                   nvarchar(20)                                                                                          
                                                                                        
 --INLAND MARINE--        
                                                                       
DECLARE         @SCH_BICYCLE_DED             decimal(20)                                                                                          
DECLARE         @SCH_BICYCLE_AMOUNT          decimal(20)                                                                                          
DECLARE         @SCH_CAMERA_DED             decimal(20)                                                                                          
DECLARE         @SCH_CAMERA_AMOUNT           decimal(20)                                                                 
DECLARE         @SCH_CELL_DED                decimal(20)                                                                                    
DECLARE         @SCH_CELL_AMOUNT             decimal(20)                                                                   
DECLARE         @SCH_FURS_DED                decimal(20)                                                  
DECLARE         @SCH_FURS_AMOUNT             decimal(20)                                                                                          
DECLARE         @SCH_GUNS_DED                decimal(20)                                                                                      
DECLARE         @SCH_GUNS_AMOUNT             decimal(20)                                                                                          
DECLARE         @SCH_GOLF_DED                decimal(20)                      
DECLARE         @SCH_GOLF_AMOUNT             decimal(20)                               
DECLARE         @SCH_JWELERY_DED             decimal(20)                                                     
DECLARE         @SCH_JWELERY_AMOUNT     decimal(20)                       
DECLARE         @SCH_MUSICAL_DED             decimal(20)                                                                                          
DECLARE			@SCH_MUSICAL_AMOUNT          decimal(20)              
DECLARE         @SCH_PERSCOMP_DED            decimal(20)                                 
DECLARE         @SCH_PERSCOMP_AMOUNT        decimal(20)                                  
DECLARE         @SCH_SILVER_DED              decimal(20)                   
DECLARE         @SCH_SILVER_AMOUNT           decimal(20)                               
DECLARE         @SCH_STAMPS_DED              decimal(20)                                     
DECLARE  @SCH_STAMPS_AMOUNT           decimal(20)                                                                                          
DECLARE         @SCH_RARECOINS_DED           decimal(20)                                            
DECLARE         @SCH_RARECOINS_AMOUNT       decimal(20)                                                                                          
DECLARE         @SCH_FINEARTS_WO_BREAK_DED   decimal(20)                                                                                          
DECLARE         @SCH_FINEARTS_WO_BREAK_AMOUNT   decimal(20)                                                                                          
DECLARE         @SCH_FINEARTS_BREAK_DED         decimal(20)                                                                                          
DECLARE         @SCH_FINEARTS_BREAK_AMOUNT      decimal(20)                             
                  
-- NEW INLAND MARINE OPTIONS --                                                                               
DECLARE         @SCH_HANDICAP_ELECTRONICS_DED       decimal(20)                     
DECLARE         @SCH_HANDICAP_ELECTRONICS_AMOUNT       decimal(20)                     
DECLARE         @SCH_HEARING_AIDS_DED       decimal(20)                     
DECLARE         @SCH_HEARING_AIDS_AMOUNT        decimal(20)                     
DECLARE         @SCH_INSULIN_PUMPS_DED      decimal(20)                     
DECLARE         @SCH_INSULIN_PUMPS_AMOUNT      decimal(20)                     
DECLARE         @SCH_MART_KAY_DED         decimal(20)                     
DECLARE         @SCH_MART_KAY_AMOUNT       decimal(20)                     
DECLARE         @SCH_PERSONAL_COMPUTERS_LAPTOP_DED     decimal(20)                     
DECLARE         @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT   decimal(20)                     
DECLARE         @SCH_SALESMAN_SUPPLIES_DED       decimal(20)                     
DECLARE         @SCH_SALESMAN_SUPPLIES_AMOUNT      decimal(20)                     
DECLARE         @SCH_SCUBA_DRIVING_DED       decimal(20)                     
DECLARE         @SCH_SCUBA_DRIVING_AMOUNT        decimal(20)                     
DECLARE         @SCH_SNOW_SKIES_DED        decimal(20)                     
DECLARE         @SCH_SNOW_SKIES_AMOUNT   decimal(20)                     
DECLARE         @SCH_TACK_SADDLE_DED        decimal(20)                     
DECLARE         @SCH_TACK_SADDLE_AMOUNT        decimal(20)                     
DECLARE         @SCH_TOOLS_PREMISES_DED        decimal(20)                     
DECLARE         @SCH_TOOLS_PREMISES_AMOUNT       decimal(20)                    
DECLARE         @SCH_TOOLS_BUSINESS_DED       decimal(20)                     
DECLARE         @SCH_TOOLS_BUSINESS_AMOUNT       decimal(20)                    
DECLARE         @SCH_TRACTORS_DED       decimal(20)                     
DECLARE         @SCH_TRACTORS_AMOUNT     decimal(20)                    
DECLARE         @SCH_TRAIN_COLLECTIONS_DED       decimal(20)                     
DECLARE         @SCH_TRAIN_COLLECTIONS_AMOUNT       decimal(20)                    
DECLARE         @SCH_WHEELCHAIRS_DED       decimal(20)                     
DECLARE         @SCH_WHEELCHAIRS_AMOUNT        decimal(20)     
                   
-------  Rest   ---------------           
                                                                                        
DECLARE         @TERRITORYCODES            nvarchar(20)           
DECLARE         @COVERAGEVALUE          decimal(20)                                                                                          
DECLARE         @TEMPERATURE              nvarchar(20)      
DECLARE         @SMOKE                    nvarchar(20)             
DECLARE         @DWELLING                 nvarchar(20)                                                                                          
DECLARE         @YEARS                    nvarchar(20)                                                                      
DECLARE         @CHIMNEYSTOVE             nvarchar(20)                                                                                          
DECLARE         @PREMIUMGROUP             nvarchar(20)                                                                                          
DECLARE         @PERSONALPROPERTY_LIMIT   nvarchar(20)                                                                                          
DECLARE         @LOSSOFUSE_LIMIT          nvarchar(20)                                                                                          
DECLARE         @COVERAGEFACTOR           nvarchar(20)                                                                                        
DECLARE         @BASEPREMIUM              nvarchar(20)                                                                                          
DECLARE         @CLAIMS                   nvarchar(20)          
DECLARE         @AMOUNT                   nvarchar(20)                                                         
DECLARE         @NOPETS                 nvarchar(20)                                                      
DECLARE         @POLICYTYPE               int                                                                            
DECLARE         @UNDERCONAPP              nvarchar(20)                                                     
DECLARE         @VALUESCUSTOMERAPP        nvarchar(20)                                                        
DECLARE         @INSUREWITHWOL            smallint                               
DECLARE         @BREEDOFDOG               nvarchar(20)                                                        
DECLARE         @TERRITORYNAME            nvarchar(20)                                                        
DECLARE         @TERRITORYCOUNTY          nvarchar(20)                                                        
DECLARE         @ISACTIVE                 nvarchar(2)                                                  
DECLARE         @EARTHQUAKEZONE           int
DECLARE         @MINESUBSIDENCEADDITIONAL nvarchar(20)                                                 
---GET APP_EFFECTIVE_DATE---                
DECLARE 		@APPDATE DATETIME                
DECLARE 		@INCEPTIONDATE VARCHAR(100)                
DECLARE 		@APP_NUMBER VARCHAR(20)                
DECLARE 		@APP_VERSION VARCHAR(20)                
DECLARE			@SUBPROTDIS NVARCHAR(20) 
DECLARE			@YEARSCONTINSURED NVARCHAR(20)
DECLARE			@YEARSCONTINSUREDWITHWOLVERINE NVARCHAR(20)
DECLARE			@TOTALLOSS NVARCHAR(20)                 
DECLARE			@HO493 CHAR(1)    
SET				@HO493='N'     
DECLARE			@SCH_CAMERA_PROF_DED NVARCHAR(20)
DECLARE			@SCH_CAMERA_PROF_AMOUNT NVARCHAR(40)
DECLARE			@SCH_MUSICAL_REMUN_DED NVARCHAR(20)
DECLARE			@SCH_MUSICAL_REMUN_AMOUNT NVARCHAR(40)                           
SELECT                                                                                         
 @LOB_ID    =    APP_LOB ,                                           
 @TERMFACTOR   =   ISNULL(APP_TERMS,'12'),                                                      
 @QUOTEEFFDATE  =    ISNULL(CONVERT(Varchar(20),APP_EFFECTIVE_DATE,101),''),                                               
 @APPDATE  =    APP_EXPIRATION_DATE,                       
 @POLICYTYPE =  isnull(POLICY_TYPE,''),                
 @INCEPTIONDATE = ISNULL(CONVERT(Varchar(10),APP_INCEPTION_DATE,101),''),                
 @APP_NUMBER = APP_NUMBER,                
 @APP_VERSION = APP_VERSION,      
 @STATENAME =  UPPER(ISNULL(STATE_NAME,'')),               
 @STATE_ID =  UPPER(ISNULL(APP_LIST.STATE_ID,'0')),    
 @INSURANCESCORE=  case isnull(APPLY_INSURANCE_SCORE,-1)     --INSURANCESCORE          
 when -1 then '100'        
 when  -2 then 'NOHITNOSCORE'    
 else convert(nvarchar(20),APPLY_INSURANCE_SCORE) end ,     
 @INSURANCESCOREDIS= case isnull(APPLY_INSURANCE_SCORE,-1)     --INSURANCESCOREDIS FOR DISCOUNTS AND SURCHAGES          
 when -1 then 'N'    
 when  -2 then 'N'         
 else cast(APPLY_INSURANCE_SCORE as nvarchar(100)) end           
 FROM APP_LIST  WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST  WITH (NOLOCK) ON  MNT_COUNTRY_STATE_LIST.STATE_ID=APP_LIST.STATE_ID                                                                                         
 WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                                              
    
--This surcharge is applied only on New Business, if the risk has had one loss exceeding $2,500 within the three years preceding                                                    
--only In case of New Buisness                                                    
IF EXISTS (SELECT CUSTOMER_ID from APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMERID                                                     
            AND LOB='1' AND AMOUNT_PAID > 2500 AND              
            OCCURENCE_DATE  > DATEADD(YEAR,-3,@QUOTEEFFDATE))                                                    
 SET @PRIORLOSSSURCHARGE='Y'                                                    
ELSE                                                    
 SET @PRIORLOSSSURCHARGE='N'                                            
                
--BASIC POLICY PAGE                                                                   
                                                                               
--**********************************************************************************************************                                                             
---------GET PRODUCT NAME AND POLICY FROM THE CODE                
DECLARE @POLICY_CODE VARCHAR(100)                                                                                         
SELECT    
 @POLICY_CODE    = UPPER(ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,''))                                                                                     
FROM                                                                                          
 APP_LIST WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES  WITH (NOLOCK) ON APP_LIST.POLICY_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                          
WHERE                                                                                           
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                
      
SET @PRODUCTNAME = dbo.piece(@POLICY_CODE,'^',1)                                                
SET @PRODUCT_PREMIER = dbo.piece(@POLICY_CODE,'^',2)   
--IF(@PRODUCT_PREMIER='REPLACE')
--BEGIN
--	set @PRODUCT_PREMIER='Replacement'
--END                   
                        
----------FORM_CODE                
                            
DECLARE  @FCVAR      NVARCHAR(6)                                                                                
DECLARE  @FIRST_CHAR  NVARCHAR(2)                                                   
DECLARE  @LOOKUP_FRAME_OR_MASONRY    NVARCHAR(10)                                                                                          
DECLARE  @NO_OF_FAMILIES         NVARCHAR(2)                                          
           
SELECT                        
 @EXTERIOR_CONSTRUCTION = EXTERIOR_CONSTRUCTION     
FROM    
 APP_HOME_RATING_INFO WITH (NOLOCK)                 
WHERE            
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID =@DWELLINGID                           
                                                           
IF(@EXTERIOR_CONSTRUCTION IS NULL)                                                                               
 BEGIN                     
  SET @EXTERIOR_CONSTRUCTION =''                                                
  SET @LOOKUP_FRAME_OR_MASONRY=''                                                                                   
  SET @EXTERIOR_CONSTRUCTION_DESC =''                                                   
 END                                                                                          
ELSE                                                                                          
BEGIN                                                                                          
-----NEW                                       
SELECT                                                                                      
  @LOOKUP_FRAME_OR_MASONRY     =  MNT_LOOKUP_VALUES.LOOKUP_FRAME_OR_MASONRY, --Picking Code from the LOOKUP                                                                   
  @EXTERIOR_CONSTRUCTION_DESC  =  MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC                
FROM                                                                                     
 APP_HOME_RATING_INFO WITH (NOLOCK) INNER  JOIN MNT_LOOKUP_VALUES  WITH (NOLOCK)                           
 ON APP_HOME_RATING_INFO.EXTERIOR_CONSTRUCTION = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                      
WHERE                    
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DWELLING_ID =@DWELLINGID                                                                                   
END    
--------------FORM_CODE--------                                              
DECLARE @FIRE_STATION_DIST int                                              
DECLARE @HYDRANT_DIST varchar(20)                                              
    
SELECT                                        
@PROTECTIONCLASS = ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0'),    
@FIRE_STATION_DIST = ISNULL(FIRE_STATION_DIST,0),                         
@HYDRANT_DIST = ISNULL(HYDRANT_DIST,'0')                                                                  
FROM                                                              
 APP_HOME_RATING_INFO WITH (NOLOCK) INNER  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                                                                                    
 ON APP_HOME_RATING_INFO.PROT_CLASS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                      
WHERE                                                                                     
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DWELLING_ID=@DWELLINGID                                       
    
EXECUTE @FORM_CODE = Proc_GetProtectionClass @PROTECTIONCLASS,@FIRE_STATION_DIST,@HYDRANT_DIST,'HOME','RATES' /**RATES IF CALLED FROM RATES*/                                              
  if (@FORM_CODE < 9)
begin
set @FIREPROTECTIONCLASS = '0' + @FORM_CODE  
end
else
begin
set @FIREPROTECTIONCLASS =  @FORM_CODE  
end
IF(LEN(@FORM_CODE)=1)                                      
BEGIN                                      
 SET @FORM_CODE= '0' + @FORM_CODE + @LOOKUP_FRAME_OR_MASONRY   
END           
ELSE   
BEGIN                                      
 SET @FORM_CODE= @FORM_CODE + @LOOKUP_FRAME_OR_MASONRY                                       
END                                      
-------------------------------                                          
                                             
       
----   GET SEASONAL          
DECLARE @PRIMARYLOC nvarchar(5)    
DECLARE @ZIPCODE   VARCHAR(100)                                                                                     
DECLARE @LOCATIONID  NVARCHAR(20)                                                                      
SELECT  @PRIMARYLOC = ISNULL(LOCATION_TYPE,0),    
 @WOLVERINEINSURESPRIMARY= isnull(IS_PRIMARY,'N'), 
 @LOCATIONID   = APP_DWELLINGS_INFO.LOCATION_ID,                                 
 @DOC      = CONVERT(VARCHAR(10),APP_DWELLINGS_INFO.YEAR_BUILT,101),                                                                             
 @AGEOFHOME=convert(nvarchar(20),DATEDIFF(YEAR,convert(nvarchar(20),ISNULL(APP_DWELLINGS_INFO.YEAR_BUILT,0)),@QUOTEEFFDATE)),                                                                                       
 @REPLACEMENTCOSTFACTOR = APP_DWELLINGS_INFO.REPLACEMENT_COST,                                                    
 @ISACTIVE       = ISNULL(APP_DWELLINGS_INFO.IS_ACTIVE,'N')            
    
FROM APP_DWELLINGS_INFO WITH (NOLOCK)                                                                           
LEFT OUTER JOIN  APP_LOCATIONS WITH (NOLOCK) ON                                                                             
 APP_LOCATIONS.LOCATION_ID=APP_DWELLINGS_INFO.LOCATION_ID                                                                            
 AND APP_LOCATIONS.CUSTOMER_ID=APP_DWELLINGS_INFO.CUSTOMER_ID                                                                            
 AND APP_LOCATIONS.APP_ID=APP_DWELLINGS_INFO.APP_ID                                                                            
 AND APP_LOCATIONS.APP_VERSION_ID=APP_DWELLINGS_INFO.APP_VERSION_ID                                                                            
                                                                            
WHERE                                                                                     
APP_DWELLINGS_INFO.CUSTOMER_ID =@CUSTOMERID AND APP_DWELLINGS_INFO.APP_ID=@APPID AND APP_DWELLINGS_INFO.APP_VERSION_ID=@APPVERSIONID AND             
APP_DWELLINGS_INFO.DWELLING_ID=@DWELLINGID        
    
if (@PRIMARYLOC=11812)    
 SET @SEASONALSECONDARY = 'N'                                                         
ELSE                                                                                      
 SET @SEASONALSECONDARY = 'Y'                                                                     
    
IF (@LOCATIONID = '')                                                                    
BEGIN                                                                        
 SET @ZIPCODE=''                                                        
END                                                                         
ELSE                                                                        
BEGIN                    
 SELECT                                                                
  @ZIPCODE = ISNULL(LOC_ZIP,'')                                                               
 FROM                                                                          
  APP_LOCATIONS  WITH (NOLOCK)                                                                       
 WHERE                               
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and LOCATION_ID = @LOCATIONID           
 END                               
                                                
IF ( @ZIPCODE !='')                                                 
BEGIN        
SELECT                                                                                      
  @TERRITORYCODES = ISNULL(TERR,''),                                     
  @TERRITORYNAME  = ISNULL(CITY,''),                                                      
  @TERRITORYCOUNTY= ISNULL(COUNTY,''),      
  @EARTHQUAKEZONE = ISNULL(EARTHQUAKE_ZONE, '')                                                                        
 FROM                                                               
  MNT_TERRITORY_CODES WITH (NOLOCK)                                                                             
 WHERE                                                
   ZIP = (substring(ltrim(rtrim(ISNULL(@ZIPCODE,''))),1,5))  AND  LOBID=@LOB_ID 
   AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                                   
                     
 ----                                  
END                                                        
ELSE                                                                         
 BEGIN                                                                                            
  SET @TERRITORYCODES = ''                                                                                     
 END                                                                                     
                                                                                    
IF @STATE_ID=14           --  forcely converting DEFAULT @EARTHQUAKEZONE=5  in case of INDIANA for earthquake HO-315                                                                                    
BEGIN                                                                                    
 IF @EARTHQUAKEZONE <=0 or @EARTHQUAKEZONE > 5                                                                             
  SET @EARTHQUAKEZONE=5            
END                                     
    
IF @STATE_ID=22           --  forcely converting @EARTHQUAKEZONE=1 in case of mischigan for earthquake HO-315       
BEGIN       
  SET @EARTHQUAKEZONE=1       
END                                                                                    
                                                         
---------NO OF FAMILIES                
                                                                                         
SELECT                                                                                         
 @NO_OF_FAMILIES    = ISNULL(NO_OF_FAMILIES,'0'),                 
 @FEET2HYDRANT      =  isnull(LOOKUP_VALUE_DESC,'0'),            
 @NUMBEROFUNITS  = ISNULL(NEED_OF_UNITS,'0')            
FROM                                                                                         
 APP_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                
 ON APP_HOME_RATING_INFO.HYDRANT_DIST = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                 
WHERE                                                                                          
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DWELLING_ID = @DWELLINGID                 
    
-----GENERAL INFO    
DECLARE @THREEYEARLESSDATE DATETIME
DECLARE @THREEYEARDAYS INT                                                                                     
SET @THREEYEARDAYS=0

SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@QUOTEEFFDATE)
SET @THREEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@QUOTEEFFDATE)
                                                                                           
SELECT                                                
@DOGFACTOR  = ISNULL(NO_of_PETS,'0')---,     ANIMALS_EXO_PETS_HISTORY field stores 0 and 1                 
 --@BREEDOFDOG = ISNULL(MNT.LOOKUP_VALUE_DESC,'')        
       
FROM                                                                                    
 APP_HOME_OWNER_GEN_INFO WITH (NOLOCK) --INNER JOIN MNT_LOOKUP_VALUES  MNT WITH (NOLOCK) ON APP_HOME_OWNER_GEN_INFO.OTHER_DESCRIPTION=MNT.LOOKUP_UNIQUE_ID                
WHERE           
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                               
                                 
IF (@DOGFACTOR != 0)                                                                                 
 BEGIN         
  SET @DOGFACTOR = 'Y'                                                                                        
 END                                                                                 
ELSE    
 BEGIN                                                                              
  SET @DOGFACTOR = 'N'                                                               
 END              
    
------  PRIOR LOSS INFO 
IF  @STATE_ID=14  
BEGIN                                  
IF EXISTS(SELECT * FROM APP_PRIOR_LOSS_INFO APLI WITH (NOLOCK) INNER JOIN APP_LIST AL ON APLI.CUSTOMER_ID=AL.CUSTOMER_ID WHERE  APLI.CUSTOMER_ID= @CUSTOMERID  
AND AL.APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
AND APLI.LOB=@LOB_ID and APLI.AMOUNT_PAID > 2500 AND  
(DATEDIFF(DAY,APLI.OCCURENCE_DATE,AL.APP_EFFECTIVE_DATE)<=@THREEYEARDAYS))  
BEGIN                                                                                  
 SET  @PRIOR_LOSS_SURCHARGE='Y'                                                                                  
END                                                                                  
ELSE                                                                                  
BEGIN                                                                                  
 SET  @PRIOR_LOSS_SURCHARGE='N'                                                                 
END    
END                                                                                
   
IF  @STATE_ID=22  
BEGIN                                                                      
IF EXISTS(SELECT * FROM APP_PRIOR_LOSS_INFO APLI WITH (NOLOCK) INNER JOIN APP_LIST AL ON APLI.CUSTOMER_ID=AL.CUSTOMER_ID WHERE  APLI.CUSTOMER_ID= @CUSTOMERID  
AND AL.APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   
AND APLI.LOB=@LOB_ID and (DATEDIFF(DAY,APLI.OCCURENCE_DATE,AL.APP_EFFECTIVE_DATE)<=@THREEYEARDAYS))                                                                               
BEGIN                                                                                  
 SET  @PRIOR_LOSS_SURCHARGE='Y'                                                                                  
END                                                                                  
ELSE                                                                                  
BEGIN                                                                                  
 SET  @PRIOR_LOSS_SURCHARGE='N'                                                                 
END    
END                                                                                
                                                                     
----------------------------------------------- BASIC POLICY PAGE END-----------------------------------------                
--- COVERAGE A - DWELLING           
--- COVERAGE B - OTHER STRUCTURES                                                                                          
--- COVERAGE C - UNSCGEDULED PERSONAL PROPERTY INCLUDED                         
--- COVERAGE D - LOSS OF USE  INCLUDED                                                                             
--- COVERAGE F - MEDICAL PAYMENTS TO OTHERS                                                                       
                                                                        
----COVERAGE A - DWELL                
----FOR POLICY TYPE HO4(11405 AND 11406) AND HO6(11195 AND 11196) THE MAIN COVERAGE IS C:                              
-- 7  EBUSPP COVERAGE C - UNSCHEDULED PERSONAL PROPERTY 22                              
-- 136 EBUSPP COVERAGE C - UNSCHEDULED PERSONAL PROPERTY 14                               
                       
IF @STATE_ID=14 --INDIANA                                                                                   
BEGIN                                    
 IF(@POLICYTYPE = 11195 OR @POLICYTYPE = 11196)                                   
 BEGIN                                                              
  SELECT                         
   @DWELLING_LIMITS =  ISNULL(LIMIT_1,'0.00')                  -- CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                         
  FROM                                                               
   APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                           
  WHERE                               
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 136                 
   AND DWELLING_ID = @DWELLINGID                   
 END                              
 ELSE                              
 BEGIN                              
   SELECT                 
   @DWELLING_LIMITS = ISNULL(LIMIT_1,'0.00')                  -- CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                         
  FROM                                                                                           
   APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                                      
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 134                 
  AND DWELLING_ID = @DWELLINGID                                                                                            
 END                              
END                   
     
IF @STATE_ID = 22                                                                                    
BEGIN                                    
 IF(@POLICYTYPE = 11405 OR @POLICYTYPE = 11406)                                 
 BEGIN                 
  SELECT                                                                                      
   @DWELLING_LIMITS = ISNULL(LIMIT_1,'0.00')   ---  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                          
  FROM                                                       
   APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                           
  WHERE                                
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 7 
   AND DWELLING_ID = @DWELLINGID                                                      
 END                             
 ELSE             
 BEGIN                              
  SELECT                                                                                      
   @DWELLING_LIMITS = ISNULL(LIMIT_1,'0.00')  ---  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                         
  FROM                                                                                           
   APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                           
  WHERE          
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 3                  
   AND DWELLING_ID = @DWELLINGID                  
 END                       
END                                                                         
  set @DWELLING_LIMITS= convert(DECIMAL(18,2),@DWELLING_LIMITS,0)                                               
IF(@DWELLING_LIMITS IS NOT NULL)         
 BEGIN                                                                                          
  SET @COVERAGEVALUE = (@DWELLING_LIMITS / 1000)                                                                 
 END                                   
ELSE                                                                   
 BEGIN                                       
  SET @COVERAGEVALUE = 0                                                                                      
 END                                                             
---------------------------------------------------------------------------------------                    
                  
---------- SPECIFIC STRUCTURES AWAY FROM PREMESIS                
IF @STATE_ID=14 
BEGIN                                
 SELECT                   
  @SPECIFICSTRUCTURESADDITIONAL       = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                          
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                           
 WHERE                                                                                            
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 172 AND                
  DWELLING_ID = @DWELLINGID                                                                                          
END                                                                                    
    
IF @STATE_ID=22                                 
BEGIN                                                 
 SELECT                                                        
   @SPECIFICSTRUCTURESADDITIONAL       = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,'0.00'))                                                            
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 257                                                                                          
  AND DWELLING_ID = @DWELLINGID                 
END                   
    
    
---COVERAGE B - OTHER STRUCTURES                                                                                         
 
IF @STATE_ID=14                       
BEGIN                                                                                    
 SELECT                                         
 @REPAIRCOSTADDITIONAL   = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                                                                                          
 FROM                                                              
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                            
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 152                
  AND DWELLING_ID = @DWELLINGID                                                                            
END                                                                                    
    
IF @STATE_ID=22                                                                                    
BEGIN                                                                                     
 SELECT         
  @REPAIRCOSTADDITIONAL       = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,'0.00'))                                                              
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                        
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 69                 
  AND DWELLING_ID = @DWELLINGID                                                         
                                                                                    
END    
    
---COVERAGE C - UNSCHEDULED PERSONAL PROPERTY                                                                 
     
                                
IF @STATE_ID = 14                 
BEGIN    
	IF(@POLICYTYPE = 11196)    
	BEGIN                                                                                     
		 SELECT                                                                                            
		  @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                                                                                      
		  @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))      
		 FROM                                                                                           
		  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
		 WHERE                                          
		  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 134                 
		AND DWELLING_ID = @DWELLINGID                                          
	END    
	--STARTS 24 sep 2009
	--HO-4 Tenants
	ELSE IF(@POLICYTYPE = 11195)    
	 BEGIN    
		 SELECT  
		  @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                                                                                      
		  @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                        
		 FROM                                                             
		  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
		 WHERE                                          
		  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
				APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 164
				AND DWELLING_ID = @DWELLINGID                                                    
	END    
	--END
	ELSE     
	 BEGIN    
		 SELECT  
		  @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                                                                                      
		  @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                        
		 FROM                                                             
		  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
		 WHERE                                          
		  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 136                 
		 AND DWELLING_ID = @DWELLINGID                                                    
	END    
END    


IF @STATE_ID = 22    
BEGIN    
	 IF(@POLICYTYPE = 11406)               
	BEGIN                                                                                     
		 SELECT                                       
		 @PERSONALPROPERTY_LIMIT     = ISNULL(LIMIT_1,'0.00'),                                                                                      
		 @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                
		 FROM                                                                      
		 APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                
		 WHERE                                                
		 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 3                
		 AND DWELLING_ID = @DWELLINGID                                                                                          
	END   
	--STARTS 24 sep 2009
	--HO-4 Tenants
	ELSE IF(@POLICYTYPE = 11405)    
	 BEGIN    
		 SELECT  
		  @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                                                                              
		  @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                        
		 FROM                                                             
		  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
		 WHERE                                          
		  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
				APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 91
				AND DWELLING_ID = @DWELLINGID                                                    
	END    
	--END                                                                          
	ELSE    
	BEGIN                                                                                     
	    SELECT                                       
		@PERSONALPROPERTY_LIMIT     = ISNULL(LIMIT_1,'0.00'),                                                                                      
		@PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                          
		FROM                                        
		APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                
		WHERE                                                
		CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 7                
		AND DWELLING_ID = @DWELLINGID                                                                                          
	END                                                                             
END                                                              
--DECLARE @DLIMIT DECIMAL                                                                
--SET @DLIMIT=CONVERT(DECIMAL(18),@PERSONALPROPERTY_LIMIT,0)                        
--                                                                                  
--set @PERSONALPROPERTYINCREASEDLIMITINCLUDE  =  @PERSONALPROPERTY_LIMIT                          
                              
                                                            
          
-----  COVERAGE D - LOSS OF USE                                                         
                                                                 
IF @STATE_ID = 14                                               
BEGIN                                                           
 SELECT                                                                                            
  @LOSSOFUSE_LIMIT  = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18)) ,                                                            
  @ADDITIONALLIVINGEXPENSEADDITIONAL  =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2)) 
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                
 WHERE                                                                        
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 137                 
  AND DWELLING_ID = @DWELLINGID                                        
END     
      
IF @STATE_ID = 22                                                                                    
BEGIN                                                             
 SELECT                                                                                            
  @LOSSOFUSE_LIMIT =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18)) ,                                                            
  @ADDITIONALLIVINGEXPENSEADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                    
 FROM                                                                
APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                       
 WHERE               
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 8                
  AND DWELLING_ID = @DWELLINGID                                                                                         
END    
/*                                                                                         
-----------------------------------------------------------------------------------------------   
IF @STATE_ID = 14                                                            
BEGIN                                                                                     
 SELECT                                       
  @PERSONALLIABILITY_LIMIT = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                   
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 170                
  AND DWELLING_ID = @DWELLINGID                                                                          
END                                                                                       
                                              
IF @STATE_ID = 22                                   
BEGIN                                                                              
 SELECT                          
  @PERSONALLIABILITY_LIMIT = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                   
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                 
 WHERE                                                                                        
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 10                
  AND DWELLING_ID = @DWELLINGID   
END 


----   COVERAGE F - MEDICAL PAYMENT EACH PERSON  
IF @STATE_ID = 14            
BEGIN                                           
 SELECT                                                                                     
  @MEDICALPAYMENTSTOOTHERS_LIMIT =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                    
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
 WHERE                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 171                 
  AND DWELLING_ID = @DWELLINGID                                                                                   
END                                                                                    
                                  
IF @STATE_ID = 22                                                       
BEGIN                                         
 SELECT                                       
  @MEDICALPAYMENTSTOOTHERS_LIMIT =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                             
 FROM                                                       
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                       
 WHERE                                                     
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 13                 
  AND DWELLING_ID = @DWELLINGID                                                   
END  
*/   

-----------------------
--PERSONAL LIABILITY should hold 'EFH' if 'Extended From Home' otherwise should hold Limit_1 Value - Asfa Praveen 03/July/2007  
----COVERAGE E - PERSONAL LIABILITY                                                                   
  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES   
	WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  
	AND COVERAGE_CODE_ID in(170, 10) AND DWELLING_ID = @DWELLINGID                                          
       	AND  LIMIT_ID IN('1293', '1294'))  
   BEGIN  
      SET @PERSONALLIABILITY_LIMIT= 'EFH'     
   END  
ELSE  
   BEGIN  
	 SELECT @PERSONALLIABILITY_LIMIT=CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))
	 FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)   
	 WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                          
	        AND  COVERAGE_CODE_ID IN(170, 10) AND DWELLING_ID = @DWELLINGID     
   END  

--MEDICAL PAYMENT should hold 'EFH' if 'Extended From Home' otherwise should hold Limit_1 Value - Asfa Praveen 03/July/2007  
--COVERAGE F - MEDICAL PAYMENT EACH PERSON
  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES   
	WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  
	AND COVERAGE_CODE_ID in(171, 13) AND DWELLING_ID = @DWELLINGID 
       	AND  LIMIT_ID IN('1410', '1411'))  
   BEGIN  
      SET @MEDICALPAYMENTSTOOTHERS_LIMIT= 'EFH'     
   END  
ELSE  
   BEGIN  
	 SELECT @MEDICALPAYMENTSTOOTHERS_LIMIT=CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))
	 FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)   
	 WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                          
	        AND  COVERAGE_CODE_ID IN(171, 13) AND DWELLING_ID = @DWELLINGID     
   END  

-------------------DEDUCTIBLE All Peril Deductible-------------------------                                
---844 NULL APD All Peril Deductible 14                                                       
---845 NULL APD All Peril Deductible 22                                
IF @STATE_ID = 14                                                               
BEGIN                                 
 SELECT                                               
  @DEDUCTIBLE= ISNULL(DEDUCTIBLE,0)                                                                              
 FROM                                                                         
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)             
WHERE           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 844                 
  AND DWELLING_ID = @DWELLINGID                           
END                                                                                       
IF @STATE_ID = 22                                                                                     
BEGIN                                                                    
 SELECT                                                             
   @DEDUCTIBLE= ISNULL(DEDUCTIBLE,0)                                                                  
 FROM 
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                 
 WHERE                                   
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 845                 
  AND DWELLING_ID = @DWELLINGID                                                                                 
END                                                                             
    
--- Other Structures - Rented to Others (HO-40)                                                                                          
                                                  
IF @STATE_ID=14                                                                                 
BEGIN                    
SELECT                                                                                        
 @HO40ADDITIONAL= convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))        
FROM                                                                                           
 APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                        
WHERE                                                                                     
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 148                
 AND DWELLING_ID = @DWELLINGID                                                                                     
END                                                                                    
                                                                    
IF @STATE_ID=22                                  
BEGIN                                                                    
SELECT                                                                                        
 @HO40ADDITIONAL= convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,'0.00'))                                                                               
FROM                                                                             
 APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                          
WHERE                                                                                           
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 62                
 AND DWELLING_ID = @DWELLINGID                         
END                  
    
---  PREFERRED PLUS V.I.P. COVERAGE (HO-21)     
IF @STATE_ID=14                                                                                          
BEGIN           
 SET @HO21 = 'N'   --HO-21 NOT APPLICABLE FOR INDIANA ON ANY PRODUCT                    
END                                                                                    
IF @STATE_ID=22             
BEGIN                                                                
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 53 AND DWELLING_ID = @DWELLINGID )                                       
 BEGIN                                                                                          
  SET @HO21 = 'Y'                                                                                          
 END                         
ELSE                                                      
 BEGIN                                                                           
  SET @HO21 = 'N'                 
 END                                                                                          
END              
----  PREFERRED PLUS V.I.P. COVERAGE (HO-23)  ONLY IN MISCIGAN STATE                                       
IF @STATE_ID=22                                                                         
BEGIN                                                                                           
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                  
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID =55 AND DWELLING_ID = @DWELLINGID)                                               
BEGIN                                  
  SET @HO23 = 'Y'                                                                                          
 END                                                                                    
ELSE                                                                                          
 BEGIN                                                                                          
 SET @HO23= 'N'                                                                                         
 END                                          
END                                                          
ELSE                                                          
BEGIN                                                          
 SET @HO23='N'                                                          
end                  
----END Preferred Plus V.I.P. Coverage (HO-23)                                                                                                   
                                             
---- PREMIER V.I.P. (HO-25)----------------                                  
-----NOT APPLICABLE FOR INDIANA ON ANY PRODUCT                    
IF @STATE_ID=22                        
BEGIN                 
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)   
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID =196 AND DWELLING_ID = @DWELLINGID)                 
BEGIN                                                               
 IF (@PRODUCTNAME='HO-3' OR @PRODUCTNAME='HO-5') AND @PRODUCT_PREMIER='PREMIER'   ---- this condition is implemented temporarly                                                                                   
   SET @HO25 = 'Y'       
 ELSE                                                                                  
   SET @HO25= 'N'                                           
 END                                                                                          
ELSE                                                                                          
BEGIN                                                                             
  SET @HO25= 'N'                              
 END                      
END                
------------------------EBP22 PREFERRED PLUS V.I.P.(HO-22) 14 142-----------------------                    
IF @STATE_ID=14                                                
BEGIN                                                                                           
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID =142 AND DWELLING_ID = @DWELLINGID)                
 BEGIN                                                                   
     SET @HO22 = 'Y'                                                                                     
 END               
ELSE                                                                                        
 BEGIN                                                                                          
   SET @HO22= 'N'              
 END                                                                                          
END                     
---H0-22 Not Applicable for MICHIGAN                    
IF @STATE_ID=22                                                                                    
BEGIN                    
   SET @HO22= 'N'                     
END                    
IF  @HO22 IS NULL                    
BEGIN                      
   SET @HO22= 'N'    
END                    
-------------END---Preferred Plus V.I.P.(HO-22)   -------------------------------------------                    
                    
-----------EBP24 Premier V.I.P.(HO-24) 14 143----------------------                     
IF @STATE_ID=14                                                                                    
BEGIN                                                                       
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID =143 AND DWELLING_ID = @DWELLINGID)                                                                       
 BEGIN                 
     SET @HO24 = 'Y'                                                                                     
 END                                                                                 
ELSE             
 BEGIN                                     
   SET @HO24 = 'N'                                    
 END                                                                                         
END                      
---H0-24 Not Applicable for MICHIGAN                    
IF @STATE_ID=22                                                                                    
BEGIN                     
   SET @HO24 = 'N'                     
END                    
IF  @HO24 IS NULL                    
BEGIN   
   SET @HO24 = 'N'                        
END                    
----------------END Premier V.I.P.(HO-24)-----------------------                    
                
                                      
---REPLACEMENT COST PERSONAL PROPERTY (HO-34)                                                              
                                                                                          
IF @STATE_ID=14                 
BEGIN                                                                                    
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)  WHERE                  
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND                
COVERAGE_CODE_ID = 140 AND DWELLING_ID = @DWELLINGID)                                                                    
                    
 BEGIN             
  SET @HO34 = 'Y'                                                                                          
 END                                                                                          
ELSE                                                                                          
 BEGIN                                                                                          
  SET @HO34 = 'N'         
 END                                                                                 
END                                                                                    
    
IF @STATE_ID=22                                                                                    
BEGIN                            
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 33 AND DWELLING_ID = @DWELLINGID)                                     
                
 BEGIN                                                                           
  SET @HO34 = 'Y'                                               
 END                                   
ELSE    
 BEGIN                                                                                          
  SET @HO34 = 'N'                                                                                          
 END                       
END                                                                                    
    
------  PREFERRED PLUS COVERAGE (HO-20)                                                             
                                                                                    
                                                                                         
IF @STATE_ID=14                        
BEGIN                                                                         
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 141 AND DWELLING_ID = @DWELLINGID)                                                                      
 BEGIN                                                                           
  SET @HO20 = 'Y'                                                                                   
 END                                                                        
ELSE                                                     
BEGIN                    
  SET @HO20 = 'N'                                                                     
 END                                                                                          
END                                                                                    
                                              
                
IF @STATE_ID=22                                                
BEGIN                                                                                    
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID        
 AND COVERAGE_CODE_ID = 37 AND DWELLING_ID = @DWELLINGID)                                                                       
 BEGIN                                                                              
  SET @HO20 = 'Y'                                                                           
 END                                                                                       
ELSE                                                                                          
 BEGIN                                                                                          
  SET @HO20 = 'N'                                                                                          
 END                                                                          
END                                            
    
----- WATERBED LIABILITY - HO-4 OR HO-6 (HO-200)                                                                                    
    
IF @STATE_ID=14                                    
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 812 AND DWELLING_ID = @DWELLINGID)      
 BEGIN                         
  SET @HO200 = 'Y'                                                                                          
 END                                                                                          
ELSE                                                                                          
 BEGIN                      
  SET @HO200 = 'N'                                                                                          
END                                                                                          
END                                                                                    
IF @STATE_ID=22                                                 
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
AND COVERAGE_CODE_ID = 813 AND DWELLING_ID = @DWELLINGID)                                                   
 BEGIN                                                       
  SET @HO200 = 'Y'                               
 END                                                             
ELSE                                                                                          
 BEGIN                                                
  SET @HO200 = 'N'                                              
 END                  
END                                                                                    
      
---ORDINANCE OR LAW COVERAGE FORMS (HO-277)                                                                                      
                                       
IF @STATE_ID=14              
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 156 AND DWELLING_ID = @DWELLINGID)                
 BEGIN       
  SET @HO277 = 'Y'                                
 END                                                                                          
ELSE                                                                                          
 BEGIN                                                                                          
  SET @HO277 = 'N'                                                                                        
 END                                       
END                                                                                    
IF @STATE_ID=22                                                            
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 79 AND DWELLING_ID = @DWELLINGID)                                                            
 BEGIN                                
  SET @HO277 = 'Y'                                                                                          
 END                                                         
ELSE    
 BEGIN                                                                                          
  SET @HO277 = 'N'                                                                                          
 END         
END                                                                                    
---IDENTITY FRAUD EXPENSE COVERAGE (HO-455)                                                               
IF @STATE_ID=14                                                                                    
BEGIN                                                                             
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 158 AND DWELLING_ID = @DWELLINGID)         
 BEGIN                                       
  SET @HO455 = 'Y'                                             
 END                 
ELSE                                                                                     
 BEGIN                                                               
SET @HO455 = 'N'                                                             
 END            
END                                                                                    
IF @STATE_ID=22                                                                
BEGIN                          
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 84 AND DWELLING_ID = @DWELLINGID)                
 BEGIN                                             
  SET @HO455 = 'Y'                                                                                          
 END                      
ELSE                                             
 BEGIN                                                                                          
  SET @HO455 = 'N'                   
 END                                                                                          
END                                                                       
----- WATER BACKUP AND SUMP PUMP OVERFLOW (HO-327)                                                                                     
--- This is to be modified                                                                              
IF @STATE_ID=14                                                                                      
BEGIN                                                                             
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 197 AND DWELLING_ID = @DWELLINGID)                                    
                
 BEGIN                                                                           
 SELECT @HO327 = ISNULL(CONVERT(VARCHAR(10),ADS.DEDUCTIBLE_1),0)  FROM                                                               
    APP_DWELLING_SECTION_COVERAGES ADS WITH (NOLOCK)                                                               
    WHERE ADS.CUSTOMER_ID=@CUSTOMERID AND ADS.APP_ID=@APPID AND ADS.APP_VERSION_ID=@APPVERSIONID AND ADS.COVERAGE_CODE_ID=197                                                              
    AND ADS.DWELLING_ID = @DWELLINGID     
    
                                                                                
 END                                          
ELSE             
 BEGIN                                                                                            
  SET @HO327 = '0'                                                                                            
 END                                                                                      
END                               
IF @STATE_ID=22                                                                                      
BEGIN       
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 198 AND DWELLING_ID = @DWELLINGID)       
 BEGIN                                                                                            
    SELECT @HO327 = ISNULL(CONVERT(VARCHAR(10),ADS.DEDUCTIBLE_1),0)  FROM         
    APP_DWELLING_SECTION_COVERAGES ADS WITH (NOLOCK)                                                               
    WHERE ADS.CUSTOMER_ID=@CUSTOMERID AND ADS.APP_ID=@APPID AND ADS.APP_VERSION_ID=@APPVERSIONID AND ADS.COVERAGE_CODE_ID=198 AND ADS.DWELLING_ID = @DWELLINGID                                                              
 END 
ELSE                                       
 BEGIN                                                                                            
  SET @HO327 = '0'                                               
 END                                                                                            
END                                                                                      
IF @HO327 IS NULL                        
BEGIN                        
SET @HO327=''                        
END                        
---CONDO - UNIT OWNERS RENTAL TO OTHERS (HO-33)                      
IF @STATE_ID=14                                                                                    
BEGIN                                                    
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 161 AND DWELLING_ID = @DWELLINGID)                                                                      
 BEGIN                                                                                          
  SELECT  @HO33= LIMIT1_AMOUNT_TEXT FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 161 AND DWELLING_ID = @DWELLINGID                          
 END                                   
ELSE                                                                                          
 BEGIN                                                
  SET @HO33 = 'N'                                                                            
 END                                                                    
END                                                                                    
IF @STATE_ID=22                                                                                    
BEGIN                                                  
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                  
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 88 AND DWELLING_ID = @DWELLINGID)                          
 BEGIN                                                    
SELECT  @HO33= LIMIT1_AMOUNT_TEXT FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 88                
 AND DWELLING_ID = @DWELLINGID                                                                           
 END                                                                          
ELSE   
 BEGIN                                                                                          
  SET @HO33 = 'N'                                     
 END                                                                        
END                                           
---EARTHQUAKE (HO-315)                                       
IF @STATE_ID=14                                                                                    
BEGIN                 
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                  
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID       
AND (COVERAGE_CODE_ID = 157 or COVERAGE_CODE_ID = 906 or COVERAGE_CODE_ID = 904) AND DWELLING_ID = @DWELLINGID)                                         
 BEGIN                                                  
  SET @HO315 = 'Y'                                                          
 END                                                                                          
ELSE                                                        
 BEGIN                                          
  SET @HO315 = 'N'                           
 END               
END                          
IF @STATE_ID=22                                                                                    
BEGIN                                              
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND (COVERAGE_CODE_ID = 80 or COVERAGE_CODE_ID = 907 or COVERAGE_CODE_ID = 905) AND DWELLING_ID = @DWELLINGID  )                                         
 BEGIN                
  SET @HO315 = 'Y'                                                                                          
 END                
ELSE                                                                                          
 BEGIN                                                        
  SET @HO315 = 'N'             
 END                                                                                   
END                                                               
                                                                              
---COLLAPSE FROM SUB-SURFACE WATER (HO-9)                                       
--- APPLICABLE ONLY IN INDIANA STATE                                                                                    
                                                               
IF @STATE_ID=22                                                                
  SET @HO9 = 'N'                                                              
IF @STATE_ID=14                                                                                    
BEGIN     
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 814 AND DWELLING_ID = @DWELLINGID)                                                     
 BEGIN                    
  SET @HO9 = 'Y'                                                                                          
 END                                         
ELSE                                                      
 BEGIN                                                                      
  SET @HO9 = 'N'              
 END                                                                                          
END                                                              
------ MINE SUBSIDENCE COVERAGE (HO-287)                        
IF @STATE_ID=14                                                                               
BEGIN                                                                                 
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 169 AND DWELLING_ID = @DWELLINGID)                
BEGIN                                                                                          
  SET @HO287 = 'Y'                                                           
 END                               
ELSE                                                                               
 BEGIN                     
  SET @HO287 = 'N'                                                                                    
 END 
SELECT @MINESUBSIDENCEADDITIONAL = ISNULL(DEDUCTIBLE_1,'0') FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 169 AND DWELLING_ID = @DWELLINGID
                                                                       
END                                      
IF @STATE_ID=22                                                                                    
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 112 AND DWELLING_ID = @DWELLINGID)                                                                      
BEGIN                                                                                    
  SET @HO287 = 'Y'                               
 END                                                                                          
ELSE                                                
 BEGIN                  
  SET @HO287 = 'N'                                         
 END                                                             
END                
-----------------HO-64 Renter Delux Endorsement-------------                
--92 EBRDC Renters Deluxe Coverage (HO-64) 22            
--165 EBRDC Renters Deluxe Coverage (HO-64) 14                 
IF @STATE_ID=22                                                                                    
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 92 AND DWELLING_ID = @DWELLINGID)                                                                      
BEGIN                                        
  SET @HO64RENTERDELUXE = 'Y'                               
 END                                                                     
ELSE                                                
 BEGIN                                               
  SET @HO64RENTERDELUXE = 'N'                                         
 END   
END         
IF @STATE_ID=14                                                                                    
BEGIN                                                                                 
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 165 AND DWELLING_ID = @DWELLINGID)                                            
BEGIN                 
  SET @HO64RENTERDELUXE = 'Y'             
 END                                           
ELSE                                                
 BEGIN                                               
  SET @HO64RENTERDELUXE = 'N'                           
 END                                                             
END      
    
if(@HO64RENTERDELUXE = 'Y')    
begin    
set @PRODUCT_PREMIER = 'DELUXE'    
end      
    
    
    
------------------------------------------------------------------------    
-----------------HO-64 Condominium Deluxe Coverage (HO-66)-------------                
--93 EBRDC Condominium Deluxe Coverage (HO-66) 22                 
--166 EBRDC Condominium Deluxe Coverage (HO-66) 14                 
IF @STATE_ID=22                                                           
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 93 AND DWELLING_ID = @DWELLINGID)                             
BEGIN                                                                                    
  SET @HO66CONDOMINIUMDELUXE = 'Y'                               
 END                     
ELSE              
 BEGIN                                               
  SET @HO66CONDOMINIUMDELUXE = 'N'                                         
 END                                        
END         
IF @STATE_ID=14                                                                                    
BEGIN                                                                                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE         
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND COVERAGE_CODE_ID = 166 AND DWELLING_ID = @DWELLINGID)                                                                      
BEGIN                                                                                    
  SET @HO66CONDOMINIUMDELUXE = 'Y'                               
 END                                                                                          
ELSE                                                
 BEGIN                                               
  SET @HO66CONDOMINIUMDELUXE = 'N'                                         
 END                                                             
END      
    
if(@HO66CONDOMINIUMDELUXE = 'Y')    
begin    
set @PRODUCT_PREMIER = 'DELUXE'    
end      
       
    
------------------------------------------------------------------------    
------ INCREASED FIRE DEPT. SERVICE CHARGE (HO-96)                               
IF @STATE_ID=14                                                                            
BEGIN                                                                                            
 SELECT                                                                                        
  @HO96INCLUDE =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                                                                      
  @HO96ADDITIONAL =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                      
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                           
 WHERE                             
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 145                   
  AND DWELLING_ID = @DWELLINGID                                                                        
END                                                                                    
IF @STATE_ID=22                                     
BEGIN                                                                                            
 SELECT                                                                              
  @HO96INCLUDE =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                                                             
  @HO96ADDITIONAL =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                 
 FROM                                                          
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                         
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 57                   
  AND DWELLING_ID = @DWELLINGID                                                                                  
END               
IF @HO96INCLUDE IS NOT NULL                                    
 BEGIN                                                                                    
  SET @HO96FINALVALUE =  CAST(CAST(@HO96INCLUDE AS decimal) + CAST(@HO96ADDITIONAL  AS decimal) AS decimal(18,2))                                           
 END                                                                                    
ELSE                                                                                    
 BEGIN                                                                               
  SET @HO96FINALVALUE = 0.00                                                                                    
 END                             
----- LOSS ASSESSMENT COVERAGE (HO-35)                                                                                          
IF @STATE_ID=14                                                
BEGIN                                                                                     
 SELECT                                                               
  @HO35INCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                    
  @HO35ADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                                      
 FROM                                                       
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 162                  
  AND DWELLING_ID = @DWELLINGID                
END          
------------                                                                                    
IF @STATE_ID=22                                            
BEGIN                
 SELECT                                                                                      
  @HO35INCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                     
  @HO35ADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')          
 FROM                                                         
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 89                
  AND DWELLING_ID = @DWELLINGID                                                                                          
END                
IF @POLICYTYPE <> 11196 and @POLICYTYPE <> 11246 and @POLICYTYPE <> 11406 and @POLICYTYPE <> 11408                            
 SET @HO35INCLUDE = '0'                                                                
---COVERAGE C INCREASED SPECIAL LIMITS (HO-65 OR HO-211)- MONEY                                       
IF @STATE_ID=14                                                                           
BEGIN                                   
 SELECT                                                                             
   @MONEYADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')                                     
 FROM           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)    
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 188            
  AND DWELLING_ID = @DWELLINGID                                                                
END                                                                                    
IF @STATE_ID=22                                    
BEGIN                                                                       
 SELECT                                                                                
  @MONEYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')              
 FROM                          
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                
 WHERE                                                                                
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 189                  
  AND DWELLING_ID = @DWELLINGID                                                                                 
END                                         
    
--COVERAGE C INCREASED SPECIAL LIMITS (HO-65 OR HO-211)- SECURITIES                                                                                          
IF @STATE_ID=14                                                                                        
BEGIN                                                                                    
 SELECT    
  @SECURITIESADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                       
 FROM          
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                            
 WHERE          
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 190                 
AND DWELLING_ID = @DWELLINGID                                                                                        
END                                                                                    
IF @STATE_ID=22                        
BEGIN                             
 SELECT                                                                                      
  @SECURITIESADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                        
 FROM     
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                        
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 191                   
AND DWELLING_ID = @DWELLINGID                                                                                       
END                                                                  
    
---COVERAGE C INCREASED SPECIAL LIMITS (HO-65 OR HO-211)- SILVERWARE                          
IF @STATE_ID=14                       
BEGIN                                      
 SELECT                                                                                     
  @SILVERWAREADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                      
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                              
 WHERE                                              
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 192                
AND DWELLING_ID = @DWELLINGID                                                    
END                                                      
IF @STATE_ID=22                                                                                       
BEGIN                                                                     
 SELECT                                                        
 @SILVERWAREADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                               
 FROM                                                                           
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                             
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 193                 
AND DWELLING_ID = @DWELLINGID                                                                                         
END                                                                                     
    
IF @POLICYTYPE <> 11196 and @POLICYTYPE <> 11246 and @POLICYTYPE <> 11406 and @POLICYTYPE <> 11408                                                                
 SET @HO35INCLUDE = '0'                                       
--- Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms                                                                                          
IF @STATE_ID = 14                            
BEGIN                                                                          
 SELECT                                                                                      
   @FIREARMSADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                      
 FROM                                                                      
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                           
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 194                
  AND DWELLING_ID = @DWELLINGID                                  
END                                                                    
IF @STATE_ID = 22                                                      
BEGIN                                                                                     
 SELECT           
   @FIREARMSADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                    
 FROM                
  APP_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                       
 WHERE    
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 195                
  AND DWELLING_ID = @DWELLINGID                                                                                          
END                                  
                                                      
---COVERAGE C INCREASED SPECIAL LIMITS (HO-65 OR HO-211)- UNSCHEDULED JEWELRY & FURS                                                                 
IF @STATE_ID = 14                                                                                     
BEGIN                 
 SELECT                                                                                      
  @UNSCHEDULEDJEWELRYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                      
 FROM                                                                
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                         
 WHERE                       
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 154                 
  AND DWELLING_ID = @DWELLINGID                                             
END     
IF @STATE_ID = 22                                                                                     
BEGIN                                                                                      
 SELECT                                                                                      
  @UNSCHEDULEDJEWELRYADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))            
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                    
 WHERE                                                                       
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 74                    
 AND DWELLING_ID = @DWELLINGID                                                         
END                                                                                    
                                                                               
--- PERSONAL PROPERTY COVERAGE C INCREASED LIMITS AWAY FROM PREMISES (HO-50)                                                                                          
IF @STATE_ID = 14                                                                                     
BEGIN                                                                                          
 SELECT                                                                                      
   @PERSONALPROPERTYAWAYADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                                                                                     
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                          
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 150                 
  AND DWELLING_ID = @DWELLINGID                                                                                         
END                                                                            
IF @STATE_ID = 22                                      
BEGIN                                                  
 SELECT                                                                                      
  @PERSONALPROPERTYAWAYADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))        
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                       
 WHERE                              
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 66                    
  AND DWELLING_ID = @DWELLINGID                                                                                      
END                                                                                    
                                                                                
---BUSINESS PROPERTY INCREASED LIMITS (HO-312)                                     
IF @STATE_ID = 14                                                        
BEGIN                                                                                          
 SELECT                                                                                      
  @HO312ADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                  
 FROM       
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 941                
  AND DWELLING_ID = @DWELLINGID                                                                 
END     
     
IF @STATE_ID = 22                                                                                     
BEGIN    
 SELECT                                                                                      
  @HO312ADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                                                                                     
 FROM           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                               
 WHERE         
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 942                    
  AND DWELLING_ID = @DWELLINGID                                          
END                                                                                    
                                             
IF EXISTS ( SELECT  COVERAGE_CODE_ID FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                            
 WHERE                                      
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID IN (831,832) AND DWELLING_ID = @DWELLINGID)                                                                          
  SELECT  @REDUCTION_IN_COVERAGE_C  =  LIMIT_1 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                         
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID IN (831,832)                
  AND DWELLING_ID = @DWELLINGID                                                    
ELSE                                                                          
  SET @REDUCTION_IN_COVERAGE_C='N'                                                                        
---     CREDIT CARD AND DEPOSITORS FORGERY (HO-53)                                                                                    
IF @STATE_ID = 14      
BEGIN                                                            
 SELECT                                                                                      
  @HO53INCLUDE = convert(nvarchar(20),ISNULL(LIMIT_1,0.00)),                                                                                          
  @HO53ADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                                                                                     
 FROM                                                                       
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                            
 WHERE             
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 153                  
  AND DWELLING_ID = @DWELLINGID                                       
END                                                                                    
IF @STATE_ID = 22                                                 
BEGIN                               
 SELECT                                                                                      
  @HO53INCLUDE =convert(nvarchar(20),ISNULL(LIMIT_1,0.00)),  
  @HO53ADDITIONAL = convert(nvarchar(20),ISNULL(DEDUCTIBLE_1,0.00))                                                 
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                
 WHERE                                                        
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 73                   
  AND DWELLING_ID = @DWELLINGID                                                             
END                     
---Expanded Replacement (HO-11)                                                                                          
IF @STATE_ID = 14                                                                                     
BEGIN                                                                                        
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)          
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 144 AND DWELLING_ID = @DWELLINGID)                             
  BEGIN                                  
   SET @HO11 = 'Y'                                                                                     
  END                                                                                          
 ELSE                                                                                          
  BEGIN                                                                                          
   SET @HO11 = 'N'                                                                                  
  END                                                                                          
END                                                                                    
IF @STATE_ID = 22                                              
BEGIN                                                                                        
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 56 AND DWELLING_ID = @DWELLINGID)                  
 BEGIN                                                                                          
   SET @HO11 = 'Y'                                                                           
  END                                                                                          
 ELSE                                
  BEGIN                                                           
   SET @HO11 = 'N'                                                                                 
  END                                                  
END                                                                                    
----- UNIT OWNERS COVERAGE A SPECIAL COVERAGE (HO-32)    
                                                                                   
IF @STATE_ID = 14              
BEGIN               
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)           
WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 160 AND DWELLING_ID = @DWELLINGID)                                                                    
 BEGIN 
   SET @HO32 = 'Y'                                                                                          
  END                                                                                          
 ELSE                                                                                          
  BEGIN                                        
   SET @HO32 = 'N'                                                                   
  END           
END                                                                                    
IF @STATE_ID = 22                
BEGIN                                    
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
 WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND COVERAGE_CODE_ID = 87 AND DWELLING_ID = @DWELLINGID)                             
 BEGIN                                                                                          
   SET @HO32 = 'Y'                                           
  END                                                   
 ELSE                                                          
  BEGIN                                                                                          
   SET @HO32 = 'N'                                                                   
 END                                                      
END    

	-- HO-493                                      

	IF EXISTS (SELECT * FROM APP_DWELLING_SECTION_COVERAGES ADSC WITH (NOLOCK) INNER JOIN MNT_COVERAGE MC
				ON ADSC.COVERAGE_CODE_ID = MC.COV_ID
			   WHERE  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
				AND COV_CODE = 'HO493' AND DWELLING_ID = @DWELLINGID)
				BEGIN
					SET @HO493='Y'
				END
		
--------------------------------END OF SECTION - I------------------------------------------                                                                                          
--------------------------------------- START OF INLAND MARINE--------------------------------------                
----BICYCLES                                
                                                   
IF @STATE_ID=14                                                       
BEGIN                                       
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 846 AND IS_ACTIVE='Y')                                 
  BEGIN                           
   SELECT @SCH_BICYCLE_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 846                                                       
	AND IS_ACTIVE='Y'


  
  
     
    
   SELECT @SCH_BICYCLE_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID                 
   IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID                 
   AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID =  846 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_BICYCLE_AMOUNT  =  0.0                          
   SELECT @SCH_BICYCLE_DED     =  0.0                          
  END                        
END                             
IF @STATE_ID=22                                                                                  
BEGIN                                                                                  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 874 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_BICYCLE_AMOUNT  =            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 874                                                        
		AND IS_ACTIVE='Y'


  
  
  
     
     
SELECT @SCH_BICYCLE_DED =  LIMIT_DEDUC_AMOUNT FROM                     
  MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 874 AND IS_ACTIVE='Y')                















 


  
  
         
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_BICYCLE_AMOUNT  =  0.0                          
   SELECT @SCH_BICYCLE_DED     =  0.0             
  END                     
END                                                                                  
    
---CAMERAS                                                        
                                                                              
IF @STATE_ID=14           
BEGIN                                                                                             
    
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 847 AND IS_ACTIVE='Y')                                            
  BEGIN    
   SELECT @SCH_CAMERA_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 847                                                       	AND IS_ACTIVE='Y' 


  
   
    
     
   SELECT @SCH_CAMERA_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM                     
  MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 847 AND IS_ACTIVE='Y')                

















 

  
  
         
  END                          
 ELSE                  
BEGIN                          
   SELECT @SCH_CAMERA_AMOUNT  =  0.0                          
   SELECT @SCH_CAMERA_DED     =  0.0                          
  END                          
END                                   
IF @STATE_ID=22                                                                                  
BEGIN            
                           
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 875 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_CAMERA_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
	 AND IS_ACTIVE='Y' AND ITEM_ID = 875                                                        

  
  
    
      
   SELECT @SCH_CAMERA_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM                     
  MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 875 AND IS_ACTIVE='Y')                





 













  
  
         
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_CAMERA_AMOUNT  =  0.0                          
   SELECT @SCH_CAMERA_DED     =  0.0              
  END                                                                                        
END                                                                             
    
--- CELLULAR PHONES (HO-900)    NOT FOUND IN THE LIST                                                                                          
                                                           
IF @STATE_ID=14                                                                                  
BEGIN                                                                                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 848 AND IS_ACTIVE='Y')    
BEGIN                       
SELECT @SCH_CELL_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 848                                            
		AND IS_ACTIVE='Y'
     
SELECT @SCH_CELL_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 848     
AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_CELL_AMOUNT  =  0.0    
   SELECT @SCH_CELL_DED     =  0.0   
  END                              
END                                                
IF @STATE_ID=22                                                                                  
BEGIN                                                                                
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 876 AND IS_ACTIVE='Y')                                           
  BEGIN                          
   SELECT @SCH_CELL_AMOUNT  =                
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 876
       AND IS_ACTIVE='Y'                                  
SELECT @SCH_CELL_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN 
(SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 876     
AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_CELL_AMOUNT  =  0.0                          
   SELECT @SCH_CELL_DED     =  0.0                          
  END                          
END                             
    
--- FUR                                                                                          
IF @STATE_ID=14                                                                      
BEGIN                        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 851 AND IS_ACTIVE='Y')                                            
  BEGIN       
   SELECT @SCH_FURS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 851                                                        
		AND IS_ACTIVE='Y'


  
  
   
    
SELECT @SCH_FURS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  
IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 851     
AND IS_ACTIVE='Y')         
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_FURS_AMOUNT  =  0.0                          
   SELECT @SCH_FURS_DED     =  0.0                          
  END                                                                                           
END  
    
IF @STATE_ID=22                                       
BEGIN                                           
                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 879 AND IS_ACTIVE='Y')                                            
  BEGIN                          
	SELECT @SCH_FURS_AMOUNT  =                            
	SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 879                                                       
		AND IS_ACTIVE='Y'
	

  
  
  
     
                           
	SELECT @SCH_FURS_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 879     
	AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_FURS_AMOUNT  =  0.0                          
   SELECT @SCH_FURS_DED     =  0.0                       
  END                                                                                          
END                                                                        
     
IF @SCH_FURS_DED is NULL                                                                                    
BEGIN                                                                                    
 SET @SCH_FURS_AMOUNT=0.00                                                                                    
END     
                                                                                  
IF @SCH_FURS_DED is NULL                                                                                    
BEGIN                                                                                    
 SET @SCH_FURS_DED=0.00                                                                                    
END                                                               
--- GUNS                                                           
IF @STATE_ID=14                                                                                  
BEGIN             
                            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 853 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_GUNS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 853              
	AND IS_ACTIVE='Y'
  
  
    
                           
   SELECT @SCH_GUNS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 853     
  AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN        
   SELECT @SCH_GUNS_AMOUNT  =  0.0                          
   SELECT @SCH_GUNS_DED     =  0.0                          
  END                                                                                        
END                                                                            
IF @STATE_ID=22                                                               
BEGIN                                                                               
                        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 881 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_GUNS_AMOUNT  =                
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 881                                
	AND IS_ACTIVE='Y'
                           
   SELECT @SCH_GUNS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 881     
   AND IS_ACTIVE='Y')                          
  END                       
 ELSE                          
  BEGIN 
   SELECT @SCH_GUNS_AMOUNT  =  0.0                          
   SELECT @SCH_GUNS_DED     =  0.0                          
  END              
END                   
--- GOLF                                                                
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 852 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_GOLF_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 852                                                        
	AND IS_ACTIVE='Y'


  
  
    
    
   SELECT @SCH_GOLF_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
	CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 852     
   AND IS_ACTIVE='Y')                          
  END           
 ELSE                          
  BEGIN                          
   SELECT @SCH_GOLF_AMOUNT  =  0.0                          
   SELECT @SCH_GOLF_DED     =  0.0                          
  END                              
END                                                             
IF @STATE_ID=22                                  
BEGIN                                                                                 
                           
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 880 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_GOLF_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 880                                                       
	AND IS_ACTIVE='Y'


 
  
  
  
     
	SELECT @SCH_GOLF_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN 
	(SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 880     
	AND IS_ACTIVE='Y')    
  END       
 ELSE                          
  BEGIN                          
   SELECT @SCH_GOLF_AMOUNT  =  0.0                          
   SELECT @SCH_GOLF_DED     =  0.0      
  END                                                                                          
END                                                                                  
---JWELERY                                                                                                  
IF @STATE_ID=14                      
BEGIN                                                                                               
                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 857 AND IS_ACTIVE='Y')                          
  BEGIN                          
	SELECT @SCH_JWELERY_AMOUNT  =                            
	SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 857                                 
	AND IS_ACTIVE='Y'
	                      
	SELECT @SCH_JWELERY_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
	APP_VERSION_ID = @APPVERSIONID AND ITEM_ID =   857 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_JWELERY_AMOUNT  =  0.0                          
   SELECT @SCH_JWELERY_DED     =  0.0                          
  END          
END                                                                                  
                                                                                  
IF @STATE_ID=22                                                                 
BEGIN                                                                
                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 885 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_JWELERY_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 885                                                        
	AND IS_ACTIVE='Y'


  
  
    
      
	SELECT @SCH_JWELERY_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
	APP_VERSION_ID = @APPVERSIONID AND ITEM_ID =      885 AND IS_ACTIVE='Y')                          
  END                          
 ELSE              
  BEGIN                          
   SELECT @SCH_JWELERY_AMOUNT  =  0.0                          
   SELECT @SCH_JWELERY_DED     =  0.0                          
  END                               
END                                                                       
--- MUSICAL                                                                                                 
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 859 AND IS_ACTIVE='Y')                                            
  BEGIN                          
	SELECT @SCH_MUSICAL_AMOUNT  =                            
	SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 859                               
	AND IS_ACTIVE='Y'
     
	SELECT @SCH_MUSICAL_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
	APP_VERSION_ID = @APPVERSIONID   AND ITEM_ID = 859 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_MUSICAL_AMOUNT  =  0.0                          
   SELECT @SCH_MUSICAL_DED     =  0.0                          
  END                                     
END                                    
                                                                                  
IF @STATE_ID=22                                 
BEGIN                                    
                       
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 887 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_MUSICAL_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 887                               
	AND IS_ACTIVE='Y'
               
	SELECT @SCH_MUSICAL_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN 
	(SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND ITEM_ID =  887 AND IS_ACTIVE='Y')                         
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_MUSICAL_AMOUNT  =  0.0                          
   SELECT @SCH_MUSICAL_DED     =  0.0                          
  END                       
END                                                                                  
                                                        
--- PERSONAL COMPUTER                            
IF @STATE_ID=14                                                                                  
BEGIN              
                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 860 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_PERSCOMP_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 860                
	AND IS_ACTIVE='Y'
                           
   SELECT @SCH_PERSCOMP_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN
   (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
    AND ITEM_ID = 860 AND IS_ACTIVE='Y')                          
  END              
 ELSE                          
  BEGIN                          
   SELECT @SCH_PERSCOMP_AMOUNT  =  0.0            
   SELECT @SCH_PERSCOMP_DED     =  0.0                          
  END                          
                                                                                         
END                                                                                  
                                 
IF @STATE_ID=22                                                                                  
BEGIN 
                            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 888 AND IS_ACTIVE='Y')                                          
  BEGIN                          
   SELECT @SCH_PERSCOMP_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 888                                                       
   AND IS_ACTIVE='Y'



 
  
   
              
   SELECT @SCH_PERSCOMP_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE
	CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
	AND ITEM_ID = 888 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_PERSCOMP_AMOUNT  =  0.0                 
   SELECT @SCH_PERSCOMP_DED     =  0.0                          
  END                                                                                       
END         
    
---SILVER         
                                                                           
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
                            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 865 AND IS_ACTIVE='Y')                                            
  BEGIN                          
 SELECT @SCH_SILVER_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 865                                                        
	AND IS_ACTIVE='Y'


  
  
   
     
   SELECT @SCH_SILVER_DED = isnull( LIMIT_DEDUC_AMOUNT,0)                     
  FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 865 AND IS_ACTIVE='Y')                         














 



  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_SILVER_AMOUNT  =  0.0                          
   SELECT @SCH_SILVER_DED     =  0.0                          
  END                                                                                      
END                                                              
             
IF @STATE_ID=22  
BEGIN                                                                            
                              
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 893 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SILVER_AMOUNT  =                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 893                                                   	
	AND IS_ACTIVE='Y'


  
  
    
        
   SELECT @SCH_SILVER_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM                     
  MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 893 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_SILVER_AMOUNT  =  0.0  
   SELECT @SCH_SILVER_DED     =  0.0                          
  END              
END                         
                                   
---STAMPS                        
IF @STATE_ID=14                                                     
BEGIN                                                                                  
                                     
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 867 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_STAMPS_AMOUNT  =           
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 867                                                        
	AND IS_ACTIVE='Y'


 
   
   
            
   SELECT @SCH_STAMPS_DED = isnull(LIMIT_DEDUC_AMOUNT,0)                     
  FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 867 AND IS_ACTIVE='Y')                         



















 


  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_STAMPS_AMOUNT  =  0.0                          
   SELECT @SCH_STAMPS_DED     =  0.0                          
  END                          
END                                                                                  
    
IF @STATE_ID=22                                                                  
BEGIN                                                                               
                                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 895 AND IS_ACTIVE='Y')                                            
  BEGIN      
   SELECT @SCH_STAMPS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 895                                                       
	AND IS_ACTIVE='Y'
 

  
  
    
    
   SELECT @SCH_STAMPS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0)                     
  FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 895 AND IS_ACTIVE='Y')                          


















  END                          
 ELSE                
  BEGIN                          
   SELECT @SCH_STAMPS_AMOUNT  =  0.0                          
   SELECT @SCH_STAMPS_DED     =  0.0                          
  END                          
END                                    
                                       
---RARE COINS                                                     
                                                                                        
IF @STATE_ID=14                                          
BEGIN                                                                                                 
                                                                
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 862 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_RARECOINS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 862                                                       
	AND IS_ACTIVE='Y'



   
   
   
      
   SELECT @SCH_RARECOINS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID      
   = 862 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_RARECOINS_AMOUNT  =  0.0      
   SELECT @SCH_RARECOINS_DED     =  0.0                          
  END                          
END                                              
                        
                                                                                  
IF @STATE_ID = 22                                                                                  
BEGIN                                                                                                 
                          
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 890 AND IS_ACTIVE='Y')                                 
  BEGIN                          
   SELECT @SCH_RARECOINS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 890                                                        
	AND IS_ACTIVE='Y'


  
  
    
      
   SELECT @SCH_RARECOINS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN 
(SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID =    890 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_RARECOINS_AMOUNT  =  0.0                          
   SELECT @SCH_RARECOINS_DED     =  0.0       
  END                                   
END                                                                                  
    
----FINEARTS WITHOUT BREAK                                                                                          
                                                                                  
IF @STATE_ID=14      
BEGIN                                                                                  
                                                             
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 850 AND IS_ACTIVE='Y')                     
  BEGIN                          
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 850                                                        
	AND IS_ACTIVE='Y'


  
  
   
       
   SELECT @SCH_FINEARTS_WO_BREAK_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
APP_VERSION_ID = @APPVERSIONID    
  AND ITEM_ID = 850 AND IS_ACTIVE='Y')                     
  END                          
 ELSE                          
  BEGIN      
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =  0.0                          
   SELECT @SCH_FINEARTS_WO_BREAK_DED     =  0.0                          
  END                      
END                                                                                  
    
IF @STATE_ID = 22                                                                                  
BEGIN                                                                                  
                                                                                        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 878 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 878                                                        
	AND IS_ACTIVE='Y'


  
  
    
      
   SELECT @SCH_FINEARTS_WO_BREAK_DED =  isnull(LIMIT_DEDUC_AMOUNT,0)                     
  FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 878 AND IS_ACTIVE='Y')                         


















 



  END       
 ELSE                          
  BEGIN                          
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =  0.0                          
   SELECT @SCH_FINEARTS_WO_BREAK_DED     =  0.0                          
  END                          
END                                               
                   
--- FINEARTS WITH BREAK                                                     
                                          
IF @STATE_ID=14                    
BEGIN                                                                                                       
                          
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 849 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 849                                                       
	AND IS_ACTIVE='Y'
 

  
  
    
     
   SELECT @SCH_FINEARTS_BREAK_DED =  isnull(LIMIT_DEDUC_AMOUNT,0)                     
  FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 849 AND IS_ACTIVE='Y')                         




 














  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =  0.0                          
   SELECT @SCH_FINEARTS_BREAK_DED     =  0.0                          
  END                                                                                         
END                                         
                                                                  
IF @STATE_ID=22        
BEGIN                                        
                                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 877 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 877                                                        
	AND IS_ACTIVE='Y'


  
  
    
     
   SELECT @SCH_FINEARTS_BREAK_DED =  isnull(LIMIT_DEDUC_AMOUNT,0)                     
 FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 877 AND IS_ACTIVE='Y')                          






















  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =  0.0                           
   SELECT @SCH_FINEARTS_BREAK_DED     =  0.0  
  END                          
END                                                                                  
                                             
----- HANDICAP ELECTRONICS                            
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 854 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 854                                                        
	AND IS_ACTIVE='Y'


  
  
    
     
       
                    
   SELECT @SCH_HANDICAP_ELECTRONICS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
APP_VERSION_ID = @APPVERSIONID       
   AND ITEM_ID = 854 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =  0.0                          
   SELECT @SCH_HANDICAP_ELECTRONICS_DED     =  0.0                          
  END                          
                                                                                         
END                     
             
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 882 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 882                                          
	AND IS_ACTIVE='Y'
    
                            
   SELECT @SCH_HANDICAP_ELECTRONICS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
APP_VERSION_ID = @APPVERSIONID       
   AND ITEM_ID = 882 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT =  0.0                          
   SELECT @SCH_HANDICAP_ELECTRONICS_DED     =  0.0       
  END       
END                                                                                  
     
--- SCHEDULED HEARING AIDS                  
IF @STATE_ID=14                                                                                  
BEGIN                           
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 855 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_HEARING_AIDS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 855                                                        
	AND IS_ACTIVE='Y'


  
  
    
     
   SELECT @SCH_HEARING_AIDS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID =




















 



  
  
@APPVERSIONID               
      
AND ITEM_ID = 855 AND IS_ACTIVE='Y')                          
  END                          
 ELSE               
  BEGIN                          
   SELECT @SCH_HEARING_AIDS_AMOUNT  =  0.0                          
   SELECT @SCH_HEARING_AIDS_DED     =  0.0                          
  END                          
                                                                         
END                                                                                  
                                           
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 883 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_HEARING_AIDS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 883                   
		AND IS_ACTIVE='Y'
                             
   SELECT @SCH_HEARING_AIDS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID =





















   
  
@APPVERSIONID               
      
AND ITEM_ID = 883 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_HEARING_AIDS_AMOUNT =  0.0                          
   SELECT @SCH_HEARING_AIDS_DED     =  0.0                          
  END                                                                                       
END                                       
    
--- SCHEDULED insulin pumps                  
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 856 AND IS_ACTIVE='Y')                
  BEGIN                          
   SELECT @SCH_INSULIN_PUMPS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 856                                                        
	AND IS_ACTIVE='Y'
	


 
   
    
     
   SELECT @SCH_INSULIN_PUMPS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID 














=





  
  
 @APPVERSIONID              
  AND ITEM_ID = 856 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_INSULIN_PUMPS_AMOUNT  =  0.0                          
   SELECT @SCH_INSULIN_PUMPS_DED     =  0.0                          
  END                          
                                                                                
END                                                                  
                                                                                     
IF @STATE_ID=22                                                                                  
BEGIN                              
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 884 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_INSULIN_PUMPS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 884                                                        
	AND IS_ACTIVE='Y'


  
  
    
      
   SELECT @SCH_INSULIN_PUMPS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
APP_VERSION_ID = @APPVERSIONID              
      
       
AND ITEM_ID = 884 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
 BEGIN                          
   SELECT @SCH_INSULIN_PUMPS_AMOUNT =  0.0                          
   SELECT @SCH_INSULIN_PUMPS_DED     =  0.0                          
  END                                                                                       
END                                                              
    
--- SCHEDULED Mart KAY                  
IF @STATE_ID=14                                                 
BEGIN                              
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 858 AND IS_ACTIVE='Y')           
  BEGIN     
   SELECT @SCH_MART_KAY_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)
	 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 858                                                       
	AND IS_ACTIVE='Y'



   
  
    
      
                      
   SELECT @SCH_MART_KAY_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND 
ITEM_ID =       858 AND IS_ACTIVE='Y')                          
  END                           
ELSE                          
  BEGIN                          
   SELECT @SCH_MART_KAY_AMOUNT  =  0.0                          
   SELECT @SCH_MART_KAY_DED     =  0.0                          
  END                          
                   
END                                                                                  
                                                                                     
IF @STATE_ID=22                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 886 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_MART_KAY_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 886                                                        
	AND IS_ACTIVE='Y'


  
  
    
                      
   SELECT @SCH_MART_KAY_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
	CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
	AND ITEM_ID = 886 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_MART_KAY_AMOUNT =  0.0                          
   SELECT @SCH_MART_KAY_DED     =  0.0                          
  END                                                              
END                                                                                  
--- SCHEDULED Personal Laptop                  
IF @STATE_ID=14                                                                                  
BEGIN                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 861 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 861                                                        
	AND IS_ACTIVE='Y'


  
  
    
                         
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID        


















 



  
  
        
AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 861 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                    
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =  0.0                          
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED     =  0.0         
  END                          
                                                                     
END                                                                                  
                                                                       
IF @STATE_ID=22                                                                   
BEGIN                                                                        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 889 AND IS_ACTIVE='Y')                                            
  BEGIN          
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 889                                                        
	AND IS_ACTIVE='Y'


 
   
    
                       
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID        












 







  
  
        
AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 889 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                         
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT =  0.0                          
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED  =  0.0                          
  END        
END               
         
--- SCHEDULED SALESMAN SUPPLY                  
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 863 AND IS_ACTIVE='Y')                                            
  BEGIN              
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 863              
AND IS_ACTIVE='Y'
                           
   SELECT @SCH_SALESMAN_SUPPLIES_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
	APP_VERSION_ID = @APPVERSIONID          
   AND ITEM_ID = 863 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                     
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT  =  0.0                          
   SELECT @SCH_SALESMAN_SUPPLIES_DED     =  0.0                          
  END                          
                                                                                         
END                                                        
                                                                                     
IF @STATE_ID=22                                                                            
BEGIN                  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 891 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 891                                      
	AND IS_ACTIVE='Y'
        
   SELECT @SCH_SALESMAN_SUPPLIES_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND 
	APP_VERSION_ID = @APPVERSIONID         
   AND ITEM_ID = 891 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT =  0.0                          
   SELECT @SCH_SALESMAN_SUPPLIES_DED     =  0.0                    
  END                                                                                
END                                                                                  
       --- SCHEDULED scuba diving                  
IF @STATE_ID=14                             
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 864 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SCUBA_DRIVING_AMOUNT  =                        
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
 WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 864                                                       
AND IS_ACTIVE='Y'
 

  
  
    
   SELECT @SCH_SCUBA_DRIVING_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID =@APPVERSIONID              
   AND ITEM_ID = 864 AND IS_ACTIVE='Y')                
  END                          
 ELSE                          
  BEGIN                      
   SELECT @SCH_SCUBA_DRIVING_AMOUNT  =  0.0                          
   SELECT @SCH_SCUBA_DRIVING_DED     =  0.0                          
  END                          
END                                         
                                                                 
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 892 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SCUBA_DRIVING_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 892        
	AND IS_ACTIVE='Y'
                            
   SELECT @SCH_SCUBA_DRIVING_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID 






=@APPVERSIONID              
   AND ITEM_ID = 892 AND IS_ACTIVE='Y')                          
  END              
 ELSE                          
  BEGIN                        
   SELECT @SCH_SCUBA_DRIVING_AMOUNT =  0.0                          
   SELECT @SCH_SCUBA_DRIVING_DED     =  0.0                          
  END                                         
END                                                              
    
--- SCHEDULED Snow Sky                  
IF @STATE_ID=14                         
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 866 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SNOW_SKIES_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 866                                   
AND IS_ACTIVE='Y'
                           
   SELECT @SCH_SNOW_SKIES_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND ITEM_ID = 866 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_SNOW_SKIES_AMOUNT  =  0.0                          
   SELECT @SCH_SNOW_SKIES_DED     =  0.0                          
  END                          
                                               
END         
                                                        
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 894 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_SNOW_SKIES_AMOUNT   =                        
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 894                                                       
AND IS_ACTIVE='Y'
    
                             
   SELECT @SCH_SNOW_SKIES_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID     
   = 894 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_SNOW_SKIES_AMOUNT =  0.0                
   SELECT @SCH_SNOW_SKIES_DED     =  0.0                          
  END                                         
END      
    
--- SCHEDULED Tack Saddle                  
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 868 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TACK_SADDLE_AMOUNT  = 
         SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 868                                                  
AND IS_ACTIVE='Y'
      
                           
   SELECT @SCH_TACK_SADDLE_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) 
   WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID    
   = 868 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TACK_SADDLE_AMOUNT  =  0.0                          
   SELECT @SCH_TACK_SADDLE_DED     =  0.0                          
  END  
             
END                                                                                  
                                                          
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 896 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TACK_SADDLE_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 896          
   AND IS_ACTIVE='Y'
                           
   SELECT @SCH_TACK_SADDLE_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
   AND ITEM_ID= 896 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TACK_SADDLE_AMOUNT =  0.0                          
   SELECT @SCH_TACK_SADDLE_DED     =  0.0                          
  END                                                                                       
END                                                         
    
--- SCHEDULED TOOLS (PREMESIS)                  
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 869 AND IS_ACTIVE='Y')                          
  BEGIN                          
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 869                                                        
	AND IS_ACTIVE='Y'
  
     
   SELECT @SCH_TOOLS_PREMISES_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID             
  AND ITEM_ID = 869 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =  0.0                          
  SELECT @SCH_TOOLS_PREMISES_DED     =  0.0                 
  END                       
 END                  
                                                                                     
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 897 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TOOLS_PREMISES_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 897                                                        
AND IS_ACTIVE='Y'
   SELECT @SCH_TOOLS_PREMISES_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID






= @APPVERSIONID             
  AND ITEM_ID = 897 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =  0.0                          
   SELECT @SCH_TOOLS_PREMISES_DED     =  0.0                          
  END                                                                           
END                                                                                  
    
--- SCHEDULED TOOLS BUSINESS                   
IF @STATE_ID=14                                                                                  
BEGIN                                                                       
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 870 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 870                                                     
 	AND IS_ACTIVE='Y'
      
   SELECT @SCH_TOOLS_BUSINESS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID                  
IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 870 AND IS_ACTIVE='Y')                     
  END                          
 ELSE                          
  BEGIN                          
SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =  0.0                          
   SELECT @SCH_TOOLS_BUSINESS_DED     =  0.0                          
  END                          
 END                                                                                  
                
IF @STATE_ID=22                                                                                  
BEGIN  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 898 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT   =                        
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 898                                            
	AND IS_ACTIVE='Y'
      
   SELECT @SCH_TOOLS_BUSINESS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                 
AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 898 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =  0.0                          
   SELECT @SCH_TOOLS_BUSINESS_DED     =  0.0                          
  END                                                                                       
END                                            
    
--- SCHEDULED tractors                  
IF @STATE_ID=14                                                                                  
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 871 AND IS_ACTIVE='Y')              
  BEGIN                          
   SELECT @SCH_TRACTORS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 871  AND IS_ACTIVE='Y'                                                          
   SELECT @SCH_TRACTORS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                 
AND ITEM_ID =871 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TRACTORS_AMOUNT  =  0.0                          
   SELECT @SCH_TRACTORS_DED     =  0.0                          
  END                          
                                                                      
END                                                                                  
                                                                                     
IF @STATE_ID=22                                                                                  
BEGIN    
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 899 AND IS_ACTIVE='Y')                                     
  BEGIN                    
   SELECT @SCH_TRACTORS_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 899      
   AND IS_ACTIVE='Y'
    
                           
   SELECT @SCH_TRACTORS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) 
  WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS 
  WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
  AND ITEM_ID = 899 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TRACTORS_AMOUNT  =  0.0                          
   SELECT @SCH_TRACTORS_DED     =  0.0                          
  END                                                             
END                                                  
 --- SCHEDULED train collections                  
IF @STATE_ID=14                                            
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 872 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 872              
   AND IS_ACTIVE='Y'                        
   SELECT @SCH_TRAIN_COLLECTIONS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 872 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                 
  BEGIN                          
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =  0.0                          
   SELECT @SCH_TRAIN_COLLECTIONS_DED     =  0.0                          
 END                          
                        
END                             
                                 
IF @STATE_ID=22                                                                                  
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 900 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT   =                
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 900                                                          AND IS_ACTIVE='Y'
                          
   SELECT @SCH_TRAIN_COLLECTIONS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                








AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 900 AND IS_ACTIVE='Y')                    
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =  0.0                          
 SELECT @SCH_TRAIN_COLLECTIONS_DED     =  0.0                          
  END                                                                                       
END                                                                                  
--- SCHEDULED wheel chair                  
IF @STATE_ID=14                                                                          
BEGIN                                                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 873 AND IS_ACTIVE='Y')                                            
  BEGIN        
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)
   WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 873                                                       
   AND IS_ACTIVE='Y'
   SELECT @SCH_WHEELCHAIRS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE 
   CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID= 873 AND IS_ACTIVE='Y')                          
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =  0.0                          
   SELECT @SCH_WHEELCHAIRS_DED     =  0.0                          
  END                          
                                                               
END                                                                                  
                                                                                     
IF @STATE_ID=22                                             
BEGIN                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 901 AND IS_ACTIVE='Y')                                            
  BEGIN                          
   SELECT @SCH_WHEELCHAIRS_AMOUNT   =                            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID = 901                                              
 AND IS_ACTIVE='Y'
               
   SELECT @SCH_WHEELCHAIRS_DED =  isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) 
WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
AND ITEM_ID = 901 AND IS_ACTIVE='Y')              
  END                          
 ELSE                          
  BEGIN                          
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =  0.0                          
   SELECT @SCH_WHEELCHAIRS_DED    =  0.0                          
  END                                                                                       
END                                                                                  
     
	-- CAMERA PROFESSIONAL USE
IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND ITEM_ID IN (10028,10027) AND IS_ACTIVE='Y')
	BEGIN
		SELECT @SCH_CAMERA_PROF_AMOUNT =    SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
		WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (10028,10027) AND IS_ACTIVE='Y'

		SELECT @SCH_CAMERA_PROF_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) 
		WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
		AND ITEM_ID IN (10028,10027) AND IS_ACTIVE='Y')
	END

	-- MUSICAL RENUMERATION

IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND ITEM_ID IN (10028,10029) AND IS_ACTIVE='Y')
	BEGIN
		SELECT @SCH_MUSICAL_REMUN_AMOUNT =    SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM APP_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) 
		WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND ITEM_ID IN (10030,10029) AND IS_ACTIVE='Y'

		SELECT @SCH_MUSICAL_REMUN_DED = isnull(LIMIT_DEDUC_AMOUNT,0) FROM MNT_COVERAGE_RANGES WITH (NOLOCK) 
		WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM APP_HOME_OWNER_SCH_ITEMS_CVGS WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID 
		AND ITEM_ID IN (10030,10029) AND IS_ACTIVE='Y')
	END
            
--------------------------------------- END OF INLAND MARINE  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





                                           
--------------------------------------------------START SECTION - II  --------------------------------------------------------------------------------------------------------------------                                                                     





                 
-- ******************************  Liability Options start  ***********************************************                                                                                        
-----------------------------------------------------------------------------------------------------------                                        
                                                                                     
--Liability Options--                                
    
---- HO-42 coverage    
                                                                                     
IF @STATE_ID=14   
 BEGIN                                                                               
 if exists (SELECT Customer_id FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE     
 COVERAGE_CODE_ID = 266 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND DWELLING_ID = @DWELLINGID)    
 BEGIN                                
   SET @HO42 = 'Y'                                                                                          
  END                                                                                 
 ELSE                                                                                          
  BEGIN        
   SET @HO42 = 'N'                                                                                          
  END      
END                                                                                    
IF @STATE_ID=22                                                                                       
BEGIN    
 if exists (SELECT Customer_id FROM APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE     
 COVERAGE_CODE_ID = 267 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
 AND DWELLING_ID = @DWELLINGID)    
BEGIN                                                                                          
   SET @HO42 = 'Y'          
  END                                                                                 
 ELSE            
  BEGIN                                                         
   SET @HO42 = 'N'                                                                                          
  END      
END     
    
---- ADDITIONAL PREMISES (NUMBER OF PREMISES) -OCCUPIED BY INSURED                                                         
    --(Removed Coverage Type Check for COVERAGE_CODE_ID = 258 ,259 ) Praven kasana Itrack 6840
IF @STATE_ID=14                                                     
BEGIN                                             
 SELECT                                       
  @OCCUPIED_INSURED = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                         
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                          
  COVERAGE_CODE_ID = 258 
	--AND COVERAGE_TYPE='S2' 
	AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
  AND DWELLING_ID = @DWELLINGID                   
END                                                                                    
                                                                                    
IF @STATE_ID=22                                                                                       
BEGIN                                                      
 SELECT                                                  
  @OCCUPIED_INSURED = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                     
 FROM                    
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
 WHERE                                                   
  COVERAGE_CODE_ID = 259 
	--AND COVERAGE_TYPE='S2' 
	AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
  AND DWELLING_ID = @DWELLINGID                
END       
-----  ADDITIONAL PREMISES (NUMBER OF PREMISES) -RESIDENCE PREMISES - RENTED TO OTHERS                   
IF @STATE_ID=14                                                                                      
BEGIN                                       
 SELECT                                                                                      
  @RESIDENCE_PREMISES = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                           
 FROM                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                         
 WHERE                                                
  COVERAGE_CODE_ID = 260 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                  
  AND DWELLING_ID = @DWELLINGID                                                                                          
END                                                                   
                                                                   
IF @STATE_ID=22                                                                                       
BEGIN                                
 SELECT                                                                                      
  @RESIDENCE_PREMISES = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                               
 FROM                                     
  APP_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                              
 WHERE                                                                                           
  COVERAGE_CODE_ID = 261 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                  
  AND DWELLING_ID = @DWELLINGID                                                      
END                                                                                   
                                                                   
-----  ADDITIONAL PREMISES (NUMBER OF PREMISES) -OTHER LOCATION -RENTED TO OTHERS (1 FAMILY)                                                          
         
IF @STATE_ID=14                                                                                       
BEGIN      
	IF EXISTS( SELECT CUSTOMER_ID FROM   APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
	WHERE                                                                                           
	COVERAGE_CODE_ID = 947 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                   
	AND DWELLING_ID = @DWELLINGID )    
	--SET @OTHER_LOC_1FAMILY ='1'                                                                       
	--ITrack # 6178 -28 July 2008 -Manoj Rathore
	SELECT @OTHER_LOC_1FAMILY=convert(varchar(10),LIMIT_1) FROM   APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
	WHERE                                                                                           
	COVERAGE_CODE_ID = 947 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                   
	AND DWELLING_ID = @DWELLINGID
END                                                                      
                                                                                    
IF @STATE_ID=22                                                     
BEGIN                                                                  
	IF EXISTS(SELECT CUSTOMER_ID FROM                                                                        
	APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
	WHERE                                            
	COVERAGE_CODE_ID = 948 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
	AND DWELLING_ID = @DWELLINGID)    
	--SET @OTHER_LOC_1FAMILY ='1'  
	--ITrack # 6178 -28 July 2008 -Manoj Rathore
	SELECT @OTHER_LOC_1FAMILY=convert(varchar(10),LIMIT_1) FROM   APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
	WHERE                                                                                           
	COVERAGE_CODE_ID = 948 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                   
	AND DWELLING_ID = @DWELLINGID                                                                           
END 
                                                                                   
IF @OTHER_LOC_1FAMILY IS NULL                                                                                    
BEGIN                            
SET @OTHER_LOC_1FAMILY = 0.00                                   
END                   
-----  ADDITIONAL PREMISES (NUMBER OF PREMISES) -OTHER LOCATION -RENTED TO OTHERS (2 FAMILY)                                                                                    
    
    
IF @STATE_ID=14                                                                                       
BEGIN       
	IF EXISTS(SELECT CUSTOMER_ID  FROM APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                      
	WHERE                                                                                           
	COVERAGE_CODE_ID = 949 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
	AND DWELLING_ID = @DWELLINGID)    
	--SET @OTHER_LOC_2FAMILY = '1'
	--ITrack # 6178 -28 July 09 Manoj Rathore
	SELECT @OTHER_LOC_2FAMILY=CONVERT(VARCHAR(10),LIMIT_1)  FROM APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
	WHERE                                                                                           
	COVERAGE_CODE_ID = 949 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
	AND DWELLING_ID = @DWELLINGID
	                                                 
END                                                                                    
IF @STATE_ID=22                                                                                       
BEGIN                                   
	IF EXISTS(SELECT  CUSTOMER_ID FROM                                 
	APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                   
	WHERE                                                                                     
	COVERAGE_CODE_ID = 950 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
	AND DWELLING_ID = @DWELLINGID )    
	--SET @OTHER_LOC_2FAMILY = '1'                                                                        
	--ITrack # 6178 -28 July 09 Manoj Rathore
	SELECT @OTHER_LOC_2FAMILY=CONVERT(VARCHAR(10),LIMIT_1)  FROM APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                            
	WHERE                                                                                           
	COVERAGE_CODE_ID = 950 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                
	AND DWELLING_ID = @DWELLINGID
END                                                                                    
IF @OTHER_LOC_2FAMILY IS NULL                                                                                    
BEGIN                                              
 SET @OTHER_LOC_2FAMILY = 0.00                                   
END                                                                                    
-----  INCIDENTAL OFFICE , PRIVATE SCHOOL OR STUDIO - ON PREMISES (HO-42)                                                                                    
IF @STATE_ID = 14                                                                                     
BEGIN                                         
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  COVERAGE_CODE_ID = 266 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                             
  BEGIN                                                    
   SET @ONPREMISES_HO42 = 'Y'                                                                                          
  END                                                                             
 ELSE     
 BEGIN                                                                                     
   SET @ONPREMISES_HO42 = 'N'                                                                                          
  END                                                      
END                       
IF @STATE_ID = 22                                                                   
BEGIN          
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE COVERAGE_CODE_ID = 267 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                 
AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                      
 BEGIN                                                                                          
   SET @ONPREMISES_HO42 = 'Y'                                                                                          
  END                                                                                 
 ELSE                                                                                          
  BEGIN                                                         
   SET @ONPREMISES_HO42 = 'N'                                                                                     
  END                                       
END  
    
------- INCIDENTAL OFFICE , PRIVATE SCHOOL OR STUDIO - LOCATED IN OTHER STRUCTURE                                                                                     
    
IF @STATE_ID = 14                                         
BEGIN                                                                                    
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  COVERAGE_CODE_ID = 268 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                 
AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                             
BEGIN             
   SET @LOCATED_OTH_STRUCTURE = 'Y'                                                      
  END                                                           
 ELSE                                                                                          
BEGIN                                                                                          
   SET @LOCATED_OTH_STRUCTURE = 'N'                          
  END           
END                                                                                    
IF @STATE_ID = 22                                                         
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 269                
 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                 
AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                               
 BEGIN                                                                
   SET @LOCATED_OTH_STRUCTURE = 'Y'                     
  END                              
 ELSE                                                      
  BEGIN                                                  
   SET @LOCATED_OTH_STRUCTURE = 'N'                                                                                          
  END                                                                        
END                                                                                    
----- INCIDENTAL OFFICE , PRIVATE SCHOOL OR STUDIO - INSTRUCTION ONLY (HO-42)                                                                                    
IF @STATE_ID = 14                                                                                     
BEGIN                                                    
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  COVERAGE_CODE_ID = 270 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                
 BEGIN                                                                                          
   SET @INSTRUCTIONONLY_HO42 = 'Y'                                                                                          
  END                                                                                           
 ELSE                                                                                          
  BEGIN                                                                                          
   SET @INSTRUCTIONONLY_HO42 = 'N'                                                   
  END                                                
END                                                          
                                                                                    
IF @STATE_ID = 22                                                        
BEGIN                              
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE COVERAGE_CODE_ID = 271 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                     
 BEGIN                                                          
   SET @INSTRUCTIONONLY_HO42 = 'Y'                                                                                        
  END                                                                                          
 ELSE                          
  BEGIN        
   SET @INSTRUCTIONONLY_HO42 = 'N'                                                                                          
  END                                                                                          
END                                                                                    
------ INCIDENTAL OFFICE , PRIVATE SCHOOL OR STUDIO - OFF PREMISES (HO-43)                                
IF @STATE_ID = 14                     
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                  
COVERAGE_CODE_ID = 272 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND              
APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                        
 BEGIN                             
   SET @OFF_PREMISES_HO43 = 'Y'                                                                                        
  END                 
 ELSE                                                                                
  BEGIN                                                                                          
   SET @OFF_PREMISES_HO43 = 'N'                                                                                          
 END                                                                                          
END         
IF @STATE_ID = 22                                                                
BEGIN          
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE COVERAGE_CODE_ID = 273 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                               
 BEGIN       
   SET @OFF_PREMISES_HO43 = 'Y'   
  END                                                                                          
 ELSE                                                                                          
  BEGIN                                                                                          
   SET @OFF_PREMISES_HO43 = 'N'                                      
  END                                           
END                                                                        
------ PERSONAL INJURY (HO-82)               
IF @STATE_ID = 14                            
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE  COVERAGE_CODE_ID = 274 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                
 AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                              
 BEGIN                                                                                          
   SET @PIP_HO82 = 'Y'                                                                                          
  END                               
 ELSE                                               
  BEGIN                                                                                          
   SET @PIP_HO82 = 'N'                   
  END                                      
END                                                                                    
                                                                                  
IF @STATE_ID = 22                                                                   
BEGIN                                                                                      
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE COVERAGE_CODE_ID = 275 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                
 AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)    
 BEGIN                                                      
   SET @PIP_HO82 = 'Y'                                
  END                                                     
 ELSE                                                                                          
  BEGIN                                                       
   SET @PIP_HO82 = 'N'                                                  
  END                                                                             
END                                                                                    
                            
-----  RESIDENCE EMPLOYEES (NUMBER)                                                                                     
    
IF @STATE_ID=14                                                                                       
BEGIN                                  
 SELECT                                                                                      
  @RESIDENCE_EMP_NUMBER = convert(nvarchar(20),ISNULL(LIMIT_1,'0'))                                                               
 FROM                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                   
 WHERE                                                                                           
  COVERAGE_CODE_ID = 276 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID  AND DWELLING_ID = @DWELLINGID                     
END                                   
                                                                         
IF @STATE_ID=22                                                                                       
BEGIN                                                                      
 SELECT             
  @RESIDENCE_EMP_NUMBER = convert(nvarchar(20),ISNULL(LIMIT_1,'0'))             
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                  
 WHERE                                                                                           
  COVERAGE_CODE_ID = 277 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID                            
END                                                   
    
------ BUSINESS PURSUITS(HO-71)CLERICAL OFFICE EMPLOYEE, SALESMEN, COLLECTORS(NO INSTALLATION ,DEMO OR SERVICE)                                                                               
IF @STATE_ID = 14                                                                                     
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE  COVERAGE_CODE_ID = 278 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND                 
APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                  
 BEGIN                                                                                          
   SET @CLERICAL_OFFICE_HO71 = 'Y'                                                        
  END                                                                                          
 ELSE                                   
  BEGIN                                                                                          
   SET @CLERICAL_OFFICE_HO71 = 'N'                                                                       
  END      
 END            
                            
IF @STATE_ID = 22                                                                                     
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                 
WHERE COVERAGE_CODE_ID = 279 AND COVERAGE_TYPE='S2' AND                
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)            
 BEGIN                                                        
   SET @CLERICAL_OFFICE_HO71 = 'Y'                                                    
  END                                                     
 ELSE                                                           
  BEGIN                                                                                          
   SET @CLERICAL_OFFICE_HO71 = 'N'               
  END                                                                        
END                                                                                    
                                                                       
------- BUSINESS PURSUITS(HO-71)SALESMEN, COLLECTORS OR MESSENGERS(INCLUDING INSTALLATION, DEMO OR SERVICE)                                                
IF @STATE_ID = 14                                                    
BEGIN                                                                     
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
  WHERE COVERAGE_CODE_ID = 280 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND                
  APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                              
 BEGIN                             
   SET @SALESMEN_INC_INSTALLATION = 'Y'                                                                                          
  END                                                                      
 ELSE                                                                                          
  BEGIN             
   SET @SALESMEN_INC_INSTALLATION = 'N'                                                   
  END                                               
END                                           
                                                                   
IF @STATE_ID = 22                                                                                     
BEGIN                                                                                      
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE COVERAGE_CODE_ID = 281 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                
 AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                 
 BEGIN                                                
   SET @SALESMEN_INC_INSTALLATION = 'Y'                                                                                          
  END                         
 ELSE                                                            
  BEGIN                                                                                          
   SET @SALESMEN_INC_INSTALLATION = 'N'                                                                                          
  END         
END                                     
                                                           
                                                                            
------- BUSINESS PURSUITS(HO-71)SALESMEN, COLLECTORS OR MESSENGERS(INCLUDING INSTALLATION, DEMO OR SERVICE)        
IF @STATE_ID = 14                     
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                 
 COVERAGE_CODE_ID = 282 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID                 
AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                              
 BEGIN                  
   SET @TEACHER_ATHELETIC = 'Y'                                     
  END                                                                        
 ELSE        
  BEGIN                                                                                          
   SET @TEACHER_ATHELETIC = 'N'                               
  END                                                                                          
END                       
                
                
IF @STATE_ID = 22                                                                                     
BEGIN                                                               
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
WHERE COVERAGE_CODE_ID = 283 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND                 
APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                               
BEGIN                                
   SET @TEACHER_ATHELETIC = 'Y'                                           
  END                                                                                          
 ELSE                     
  BEGIN                                                                                          
   SET @TEACHER_ATHELETIC = 'N'                                                                                 
  END                             
END                                                                         
                                                                      -------BUSINESS PURSUITS(HO-71)TEACHERS-NOC (EXCL. CORPORAL PUNISHMENT)          
IF @STATE_ID = 14                       
BEGIN        
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                  
WHERE  COVERAGE_CODE_ID = 284 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID                 
AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                            
BEGIN                                                                              
   SET @TEACHER_NOC = 'Y'                                                                           
  END                                                                                        
 ELSE                                            
  BEGIN                                                                                          
   SET @TEACHER_NOC = 'N'                                                                                          
  END                                                                     
END                
                
                                                                                    
IF @STATE_ID = 22                                                     
BEGIN                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                
 WHERE COVERAGE_CODE_ID = 285 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND                 
APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                                              
BEGIN                       
   SET @TEACHER_NOC = 'Y'                                                                            
  END                                                                                          
 ELSE                                                                             
BEGIN          
   SET @TEACHER_NOC = 'N'      
  END                                                                                         
END                                                                                    
                           
                                                                                
-------FARM LIABILITY (NUMBER OF LOCATIONS) INCIDENTAL FARMING ON PREMISES(HO-72)                                                                                     
IF @STATE_ID = 14                 
BEGIN                                                                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
WHERE  COVERAGE_CODE_ID = 286  AND CUSTOMER_ID = @CUSTOMERID AND                
 APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                               
BEGIN                                                                                          
   SET @INCIDENTAL_FARMING_HO72 = 'Y'                                                                            
  END                                                    
 ELSE                                                                                   
  BEGIN                                          
   SET @INCIDENTAL_FARMING_HO72 = 'N'                                                              
END                                                                                          
END                 
    
IF @STATE_ID = 22                                                            
BEGIN                                           
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                 
 WHERE COVERAGE_CODE_ID = 287 AND CUSTOMER_ID = @CUSTOMERID AND                 
APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID = @DWELLINGID)                       
BEGIN                                                                                          
   SET @INCIDENTAL_FARMING_HO72 = 'Y'                                                                                          
  END                                                                                          
 ELSE                  
  BEGIN                                                                                       
   SET @INCIDENTAL_FARMING_HO72 = 'N'                                                             
  END                            
END                                                   
-----   FARM LIABILITY (NUMBER OF LOCATIONS) OWNED FARMS OPERATED BY INSURED'S EMPLOYEES(HO-73)                                                                                      
IF @STATE_ID=14                                                                                    
BEGIN                                                                                     
SELECT                                                                 
  @OTH_LOC_OPR_EMPL_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                    
 FROM                                                  
  APP_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                       
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 288                 
AND DWELLING_ID = @DWELLINGID                                                   
END                                                                                    
---          
IF @STATE_ID=22                                                      
BEGIN               
 SELECT    
  @OTH_LOC_OPR_EMPL_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                       
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                           
 WHERE                    
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 289                 
 AND DWELLING_ID = @DWELLINGID                                                                          
END                
                                                                          
------  FARM LIABILITY (NUMBER OF LOCATIONS) OWNED FARMS RENTED TO OTHERS(HO-73)                                    
                                                                                 
IF @STATE_ID=14                                                       
BEGIN                                                                                     
 SELECT                                                                                      
  @OTH_LOC_OPR_OTHERS_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                      
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                      
 WHERE                                                    
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 290                  
  AND DWELLING_ID = @DWELLINGID                                                                                    
END                                                                                    
---                                   
IF @STATE_ID=22                                                                                    
BEGIN                             
 SELECT                                                                                    
  @OTH_LOC_OPR_OTHERS_HO73 =CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                    
 FROM                                                                                  
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 291                
  AND DWELLING_ID = @DWELLINGID                                                       
END                                      
   
---------------------  HO 48                 
IF @STATE_ID=14                                                                                    
BEGIN                                                                                     
  SELECT                            
  @HO48INCLUDE = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)),                
  @HO48ADDITIONAL = ISNULL(DEDUCTIBLE_1 ,0.00)             
  FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                                     
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 135                 
  AND DWELLING_ID = @DWELLINGID                                                                               
END                                                                                    
---                                                                     
IF @STATE_ID=22                                                                                    
BEGIN                          
 SELECT                                                  
  @HO48INCLUDE =CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                
  @HO48ADDITIONAL = ISNULL(DEDUCTIBLE_1 ,0.00)                   
 FROM                                                                                           
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 5                  
  AND DWELLING_ID = @DWELLINGID                         
END                
 ---------------------  HO 48       Additional from sattelite covg         
IF @STATE_ID=14                                                                                    
BEGIN                                          
  SELECT @HO48ADDITIONAL = isnull(@HO48ADDITIONAL,0.00) + ISNULL(DEDUCTIBLE_1 ,0.00)                
 FROM                    
  APP_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                         
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 742                 
  AND DWELLING_ID = @DWELLINGID                                                                               
END                                                   
---                                                                                    
IF @STATE_ID=22                                                                                    
BEGIN                                     
 SELECT         
   @HO48ADDITIONAL = isnull(@HO48ADDITIONAL,0.00) +  ISNULL(DEDUCTIBLE_1 ,0.00)                                                                                   
 FROM          
  APP_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                          
 WHERE                                                                                           
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND COVERAGE_CODE_ID = 908                  
  AND DWELLING_ID = @DWELLINGID                                                                                            
END          
    
-----------------------------------------------------------------------------------------------------------------------------                                                                   
-- ***************************************** Liability Options  
 END                        
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


















   
  
  
  
    
-----------------------------------------------END OF SECTION- II  --------------------------------------------------------------------------------------------------------------------                                                                        


















  
  
    
    
-------------------------------------------------- START -----------------------------------------------------------------------                                                                                      
    
--- NONSMOKER  and MULTIPLEPOLICYFACTOR                                
                           
SELECT @NONSMOKER = CASE ISNULL(NON_SMOKER_CREDIT,'0')                                                                                        
 WHEN '1' THEN 'Y'                                                                                   
 ELSE 'N'                                                                                        
 END,                             
 @MULTIPLEPOLICYFACTOR =CASE ISNULL(MULTI_POLICY_DISC_APPLIED,'N')                                                             
 WHEN '1' THEN 'Y'                                                                                        
 ELSE 'N'                          
 END,                                                                     
 @INSUREWITHWOL=ISNULL(YEARS_INSU_WOL,0)                       
FROM                                                                                         
 APP_HOME_OWNER_GEN_INFO  WITH (NOLOCK)                                                         
WHERE                                      
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                                                     
    
--This is given once two years are completed, at the third renewal         
IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE  CUSTOMER_ID = @CUSTOMERID AND LOB=1 and (DATEDIFF(day ,OCCURENCE_DATE ,@QUOTEEFFDATE)< 365))                  
 BEGIN                  
  SET @LOSSFREE = 'N'    
 SET @NOTLOSSFREE='Y'               
 END                  
ELSE                 
 BEGIN                  
  SET @LOSSFREE = 'Y'     
 SET @NOTLOSSFREE='N'                 
 END                                                               
--This is given once two years are completed, at the third renewal              
IF   @INSUREWITHWOL > 2                                                                          
 SET @VALUEDCUSTOMER = 'Y'                                                                          
ELSE                                                                          
 SET @VALUEDCUSTOMER = 'N'                                                     
    
-- For Michigan Policy Lookup Values                                                  
-- Applicable to all HO Products                                       
-- In case of HO-4, only applicable in case if Covg C is 15000 or more.                                                                
Declare @D_LIMIT INT  
  
 SET @D_LIMIT = CONVERT(INT, @PERSONALPROPERTY_LIMIT)  
                        
IF (@POLICYTYPE =  11405 OR @POLICYTYPE= 11195 )  
 begin   
  if (@MULTIPLEPOLICYFACTOR='Y')  
   begin  
    if (@D_LIMIT < 15000)  
     begin  
       SET @MULTIPLEPOLICYFACTOR='N'                                                                 
    end                                          
   end  
 end  
/*                                                                            
DWELLING UNDERCONSTRUCTION IS NOT AVAILABLE FOR HO-4 OR HO-6.                                 
                                                          
*/                                        
IF @POLICYTYPE=11405 OR @POLICYTYPE=11406 OR @POLICYTYPE=11195 OR @POLICYTYPE=11196                                                                        
 SET @UNDERCONAPP='N'                                                
ELSE                                                                            
 SET @UNDERCONAPP='Y'                                                                            
                                                                              
/***********AGE OF HOME CREDIT                             
     INDIANA - ONLY ON HO-2,HO-3 AND HO-5                                            
            MICHIGAN -ALL                                              
          
11193-HO-2 REPAIR COST                                                                              
11194-HO-3 REPAIR COST        
11195-HO-4                                                                              
11245-HO-4 DELUXE                                                  
11196-HO-6                                                                              
11246-HO-6 DELUXE                                                                
 APPLICABLE TO ONLY H0-6 AND H0-6 DELUX                     
***********/                                                                                
IF @POLICYTYPE != 11196 and @POLICYTYPE != 11246 and @POLICYTYPE !=11406 and @POLICYTYPE != 11408                                                                
 SET @HO35INCLUDE='0'                                                                       
--------------------------------- END -----------------------------------------------                
--- EXPRIENCE---                                     
DECLARE @DATE_OF_BIRTH  varchar(100)                                                    
                                         
     SELECT                                                                                           
            @DATE_OF_BIRTH = ISNULL(CONVERT(Varchar(10),CLT.CO_APPL_DOB,101),'')                                                                
     FROM                                                                                           
              CLT_APPLICANT_LIST CLT  WITH (NOLOCK) INNER JOIN APP_APPLICANT_LIST APL WITH (NOLOCK)                                                                
               ON CLT.APPLICANT_ID=APL.APPLICANT_ID                                                                          
                  WHERE                                                                          
                       APL.CUSTOMER_ID = @CUSTOMERID AND                                                 
                       APL.APP_ID=@APPID AND                                                                
                       APL.APP_VERSION_ID=@APPVERSIONID AND                       
                       APL.IS_PRIMARY_APPLICANT=1                                                           
     
--When  age is 35.5('35.something') we consider it 36                
                                                
IF @DATE_OF_BIRTH <> ''                                                                                       
 BEGIN              
  SELECT @EXPERIENCE= datediff(day, @DATE_OF_BIRTH, @QUOTEEFFDATE)                  
 /*(case                                                 
  WHEN (datepart(m, @DATE_OF_BIRTH) > datepart(m, @QUOTEEFFDATE)) OR                                                
   (datepart(m, @DATE_OF_BIRTH) = datepart(m, @QUOTEEFFDATE) AND                                                
    datepart(d, @DATE_OF_BIRTH) > datepart(d, @QUOTEEFFDATE))                 
  THEN 1                                                
  WHEN (datepart(m, @DATE_OF_BIRTH) < datepart(m, @QUOTEEFFDATE)OR        
  (datepart(m, @DATE_OF_BIRTH) = datepart(m, @QUOTEEFFDATE) AND                                                
            datepart(d, @DATE_OF_BIRTH) < datepart(d, @QUOTEEFFDATE)))                                                 
   THEN -1            
  ELSE 0                                                
 end)*/                                                 
END                                                   
---HOME RATING INFO       
declare @ONEALARM nvarchar(20)     
declare @CENT_ST_BURG_FIRE nvarchar(20)    
declare @DIR_FIRE_AND_POLICE nvarchar(20)                                               
SELECT                                                     
@CONSTRUCTIONCREDIT =  CASE ISNULL(IS_UNDER_CONSTRUCTION,'0')             
 WHEN '1' THEN 'Y'                                                                                        
 ELSE 'N'                                 
 END,                                                                                   
@BURGLAR=ISNULL(CENT_ST_BURG,'N'),                                                                              
@CENTRAL_FIRE=ISNULL(CENT_ST_FIRE,'N'),    
@DIR_FIRE_AND_POLICE=ISNULL(DIR_FIRE_AND_POLICE,'N'),                                                                              
@BURGLER_ALERT_POLICE=ISNULL(DIR_POLICE,'N'),                                                                              
@FIRE_ALARM_FIREDEPT=ISNULL(DIR_FIRE,'N'),                                                                              
@ONEALARM = isnull(LOC_FIRE_GAS,'N'),    
@CENT_ST_BURG_FIRE = isnull(CENT_ST_BURG_FIRE,'N'),    
@N0_LOCAL_ALARM  =  ISNULL(TWO_MORE_FIRE,'0'),    -- number of local alarms should be taken from this field 'NUM_LOC_ALARMS_APPLIES' after screen level changes            
@DISTANCET_FIRESTATION  = convert(nvarchar(20),ISNULL(FIRE_STATION_DIST,0)),    
@NUMBEROFFAMILIES      =  convert(nvarchar(20),ISNULL(NO_OF_FAMILIES,0))                                                                          
FROM                                                                                         
 APP_HOME_RATING_INFO  WITH (NOLOCK)                            
WHERE                                                                                          
  CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID  AND DWELLING_ID=@DWELLINGID      
    
-- need to be changed later    
if (@ONEALARM = 'Y')    
 set @N0_LOCAL_ALARM='1'    
if(@N0_LOCAL_ALARM='Y')    
 set @N0_LOCAL_ALARM='2'     
                                             
if ISNULL(@BURGLAR,'') = ''  
  SET @BURGLAR='N'                                           
if ISNULL(@CENTRAL_FIRE,'')=''                                                                   
  SET @CENTRAL_FIRE='N'                               
if ISNULL(@BURGLER_ALERT_POLICE,'')=''                                                                              
  SET @BURGLER_ALERT_POLICE='N'                                                                              
if ISNULL(@FIRE_ALARM_FIREDEPT,'')=''                                        
  SET @FIRE_ALARM_FIREDEPT='N'                                                                              
if ISNULL(@BURGLAR,'')=''                                                                              
  SET @BURGLAR='N'                                                 
if ISNULL(@CONSTRUCTIONCREDIT,'')=''  OR  @UNDERCONAPP= 'N'                                                                            
  SET @CONSTRUCTIONCREDIT='N'                                                                              
    
if (@CENT_ST_BURG_FIRE = 'Y')    
begin     
set  @BURGLAR='Y'    
 set  @CENTRAL_FIRE='Y'    
end 
    
    
if (@DIR_FIRE_AND_POLICE = 'Y')    
begin    
 set  @BURGLER_ALERT_POLICE='Y'    
 set  @FIRE_ALARM_FIREDEPT='Y'    
end    
                                             
    
----  @WOODSTOVE_SURCHARGE                                                                      
              
/******************************  Credit - Seasonal / Secondary, Wolverine Insures Primary   */    
DECLARE @WOODSTOVE_SURCHARGE_UNDERWRITING NVARCHAR(20)    
DECLARE @WOODSTOVE_SURCHARGE_RATING_INF NVARCHAR(20)                                                   
SELECT                                             
 @WOODSTOVE_SURCHARGE_UNDERWRITING  =  case  ISNULL(ANY_HEATING_SOURCE,'0')    
    WHEN '1' THEN 'Y'                                                                                        
    ELSE 'N'                                                                                        
END,    
   @NOPETS = convert(nvarchar(20),isnull(NO_OF_PETS,0))                     
FROM                                                                                         
 APP_HOME_OWNER_GEN_INFO WITH (NOLOCK)                                                                     
WHERE                                                                         
 CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                        
  
if exists(select Customer_id from app_HOME_RATING_INFO where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and ((PRIMARY_HEAT_TYPE=11807 or PRIMARY_HEAT_TYPE=6224 or PRIMARY_HEAT_TYPE=6223) or    
 (SECONDARY_HEAT_TYPE=11807 or SECONDARY_HEAT_TYPE=6224 or SECONDARY_HEAT_TYPE=6223)))    
    
set @WOODSTOVE_SURCHARGE_RATING_INF = 'Y'    
                                 
 if(@WOODSTOVE_SURCHARGE_RATING_INF = 'Y' or @WOODSTOVE_SURCHARGE_UNDERWRITING = 'Y')                                          
set  @WOODSTOVE_SURCHARGE ='Y'    
else    
set  @WOODSTOVE_SURCHARGE ='N'    
-- ******************************   ***********************************************   
DECLARE @NONWEATHERLOSS SMALLINT
DECLARE @WEATHERLOSS SMALLINT
SET @NONWEATHERLOSS=0                                                                                     
SET @WEATHERLOSS=0
-- YEARS INSURED WITH WOLVERINE
SELECT  @YEARSCONTINSURED= YEARS_INSU,  @YEARSCONTINSUREDWITHWOLVERINE=YEARS_INSU_WOL--, 
	--	@NONWEATHERLOSS= NON_WEATHER_CLAIMS, @WEATHERLOSS=WEATHER_CLAIMS
FROM  APP_HOME_OWNER_GEN_INFO 
WHERE CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID   

-- PRIOR LOSS INFO
/*
SELECT  @TOTALLOSS = COUNT(LOSS_ID) FROM APP_PRIOR_LOSS_INFO 
WHERE CUSTOMER_ID =  @CUSTOMERID  AND LOB='1' AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE  

SELECT  @WEATHERLOSS = COUNT(PRIOR_LOSS_ID)  FROM PRIOR_LOSS_HOME WHERE CUSTOMER_ID =  @CUSTOMERID  --AND LOB='1' 
		AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE  AND WEATHER_RELATED_LOSS =10963
*/
SELECT @WEATHERLOSS = COUNT(APLI.LOSS_ID) FROM APP_PRIOR_LOSS_INFO APLI INNER JOIN PRIOR_LOSS_HOME PLH
							ON APLI.CUSTOMER_ID=PLH.CUSTOMER_ID	AND APLI.LOSS_ID=PLH.LOSS_ID
							WHERE APLI.CUSTOMER_ID= @CUSTOMERID  AND LOB='1' 
							AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE  AND ISNULL(WEATHER_RELATED_LOSS,10964) =10963

SELECT @NONWEATHERLOSS = COUNT(APLI.LOSS_ID) FROM APP_PRIOR_LOSS_INFO APLI INNER JOIN PRIOR_LOSS_HOME PLH
							ON APLI.CUSTOMER_ID=PLH.CUSTOMER_ID	AND APLI.LOSS_ID=PLH.LOSS_ID
							WHERE APLI.CUSTOMER_ID= @CUSTOMERID  AND LOB='1' 
							AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE  AND ISNULL(WEATHER_RELATED_LOSS,10964) =10964

 IF(@WEATHERLOSS=1)
	BEGIN
		SET @WEATHERLOSS=0
	END
 ELSE IF(@WEATHERLOSS>1)
	BEGIN
		SET @WEATHERLOSS=@WEATHERLOSS - 1
	END

SET @TOTALLOSS = @WEATHERLOSS + @NONWEATHERLOSS

	-- SUBURBAN CLASS DISCOUNT
IF  EXISTS(SELECT * FROM APP_HOME_RATING_INFO   WITH(NOLOCK)                                 
		WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID 
		AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963')
	BEGIN
		SET @SUBPROTDIS ='Y'
	END	
                     
------------------------------------------RETURN VALUES-------------------------------------------                                                                                       
BEGIN                                                                                         
SELECT                                                        
-- BASIC POLICY PAGE                                                    
	@LOB_ID     		AS  LOB_ID,                              
	''          		AS   POLICY_ID,                                 
	@STATENAME      	AS    STATENAME,                                                        
	'Y'            		AS    NEWBUSINESSFACTOR,  -- SINCE IT WILL ALWAYS BE NEW BUSINESS WHEN IT IS AN APPLICATION                                                  
	@QUOTEEFFDATE     	AS    QUOTEEFFDATE,                      
	                                                                  
	@TERMFACTOR      	AS   TERMFACTOR,                
	@APP_NUMBER  		AS APP_NUMBER,                    
	@APP_VERSION  		AS APP_VERSION,                       
	@SEASONALSECONDARY      AS  SEASONALSECONDARY,                                                                                        
	@WOLVERINEINSURESPRIMARY  	AS  WOLVERINEINSURESPRIMARY,                                                                                        
	@PRODUCTNAME         		AS PRODUCTNAME,           
	@PRODUCT_PREMIER      		AS PRODUCT_PREMIER,                  
	CONVERT(decimal(18),isnull(@REPLACEMENTCOSTFACTOR,'0'))  AS REPLACEMENTCOSTFACTOR,                                                                                        
	@DWELLING_LIMITS      		AS DWELLING_LIMITS,   
	@FIREPROTECTIONCLASS		as FIREPROTECTIONCLASS,          
	isnull(@DISTANCET_FIRESTATION,'0') AS  DISTANCET_FIRESTATION,                                                 
	isnull(@FEET2HYDRANT,'0')     	   AS  FEET2HYDRANT,                                 
	isnull(@DEDUCTIBLE,'0')     	   AS  DEDUCTIBLE,                
	@EXTERIOR_CONSTRUCTION    	   AS  EXTERIOR_CONSTRUCTION,                      
	@EXTERIOR_CONSTRUCTION_DESC  	   AS  EXTERIOR_CONSTRUCTION_DESC,                                                                                    
	@LOOKUP_FRAME_OR_MASONRY           AS  EXTERIOR_CONSTRUCTION_F_M,                 
	@DOC        AS  DOC,                                                                                    
	@AGEOFHOME  AS  AGEOFHOME,                 
	isnull(@NUMBEROFFAMILIES,0)     AS  NUMBEROFFAMILIES,                                                                
	isnull(@NUMBEROFUNITS,0)        AS  NUMBEROFUNITS,                    
	isnull(@PERSONALLIABILITY_LIMIT,0)  AS  PERSONALLIABILITY_LIMIT,                                                         
	isnull(@MEDICALPAYMENTSTOOTHERS_LIMIT,0)  AS MEDICALPAYMENTSTOOTHERS_LIMIT,                        
	@FORM_CODE      AS FORM_CODE,                    
	--Property Page--                                                                       
	isnull(@HO20,'N')           AS HO20,                                                                                          
	isnull(@HO21,'N')           AS HO21,                     
	isnull(@HO25,'N')           AS HO25,                                        
	isnull(@HO23,'N')      AS HO23,                                                                                        
	isnull(@HO22,'N')     AS HO22,                      
	isnull(@HO24,'N')     AS HO24,            
	isnull( @HO34,'N')           AS HO34,                                                                                       
	isnull(@HO11,'N')           AS HO11,                                                                                      
	isnull(@HO32,'N')           AS HO32,                                                                                      
	isnull(@HO277,'N')          AS HO277,                                                                  
	isnull(@HO455,'N')          AS HO455,                                           
	isnull(@HO327,'N')          AS HO327,                                                                                      
	isnull(@HO33 ,'N')     AS HO33,                                                                             
	isnull(@HO315,'N')        AS HO315,                                                                                      
	isnull(@HO9 ,'N')  AS HO9,                                   
	isnull(@HO287 ,'N')     AS HO287,                      
	isnull(@HO96FINALVALUE,'N')            AS HO96FINALVALUE,                                                                                          
	CONVERT(DECIMAL(18),@HO96ADDITIONAL)           AS HO96ADDITIONAL,                                                                                          
	CONVERT(DECIMAL(18),isnull(@HO48INCLUDE,'0.00'))   AS HO48INCLUDE,                                                                      
	isnull(@HO48ADDITIONAL,0.00)          AS HO48ADDITIONAL,                                                                             
	isnull(@HO40ADDITIONAL,'N')            AS HO40ADDITIONAL,                        
	@HO42            AS HO42,                   
	isnull(@REPAIRCOSTADDITIONAL,'0')           AS REPAIRCOSTADDITIONAL,     -- 0 is to be removed                                                                                     
	isnull(@PERSONALPROPERTY_LIMIT,'0')      AS PERSONALPROPERTYINCREASEDLIMITINCLUDE,                                                                                          
	isnull(@PERSONALPROPERTYINCREASEDLIMITADDITIONAL,'0')   AS PERSONALPROPERTYINCREASEDLIMITADDITIONAL,                               
	isnull(@PERSONALPROPERTYAWAYADDITIONAL,'0')        AS PERSONALPROPERTYAWAYADDITIONAL,                                                                                          
	isnull(@UNSCHEDULEDJEWELRYADDITIONAL,'0')          AS UNSCHEDULEDJEWELRYADDITIONAL,                 
	isnull(@MONEYADDITIONAL,'0')      AS MONEYADDITIONAL,                                                   
	isnull(@SECURITIESADDITIONAL,'0') AS SECURITIESADDITIONAL,                                 
	isnull(@SILVERWAREADDITIONAL,'0') AS SILVERWAREADDITIONAL,                                                                                      
	isnull(@FIREARMSADDITIONAL,'0')   AS FIREARMSADDITIONAL,                 
	isnull(@HO312ADDITIONAL,'0')     AS HO312ADDITIONAL,                                              
	isnull(@ADDITIONALLIVINGEXPENSEADDITIONAL,'0')     AS ADDITIONALLIVINGEXPENSEADDITIONAL,                                       
	isnull(@LOSSOFUSE_LIMIT,0)     AS ADDITIONALLIVINGEXPENSEINCLUDE      ,                                                                                  
	isnull(@HO53INCLUDE,'0') AS HO53INCLUDE,                                                
	isnull(@HO53ADDITIONAL,'0')             AS HO53ADDITIONAL,                                                                                          
	isnull(@HO35INCLUDE,'0')             AS HO35INCLUDE,                                                                     
	isnull(@HO35ADDITIONAL,'0')          AS HO35ADDITIONAL, 
	ISNULL(@HO493,'N')					 AS HO493,                                                                                         
	isnull(@SPECIFICSTRUCTURESADDITIONAL,'0')   AS SPECIFICSTRUCTURESADDITIONAL,     
	                                                                    
	--LIABILITY OPTIONS--                                                                                          
	isnull(@OCCUPIED_INSURED,'0')            AS OCCUPIED_INSURED,                                                                                            
	isnull(@RESIDENCE_PREMISES,'0')          AS RESIDENCE_PREMISES,              
	isnull(@OTHER_LOC_1FAMILY,'0')           AS OTHER_LOC_1FAMILY,                                                                 
	isnull(@OTHER_LOC_2FAMILY,'0')          AS OTHER_LOC_2FAMILY,                                                                                          
	isnull(@ONPREMISES_HO42,'0')       AS ONPREMISES_HO42,                                                       
	isnull(@LOCATED_OTH_STRUCTURE,'0')       AS LOCATED_OTH_STRUCTURE,       
	isnull(@INSTRUCTIONONLY_HO42,'0')        AS INSTRUCTIONONLY_HO42,                                 
	isnull(@OFF_PREMISES_HO43,'0')           AS OFF_PREMISES_HO43,                                                      
	isnull(@PIP_HO82,'0')               AS PIP_HO82,                                                                                    
	isnull(@HO200,'0')                AS HO200,                
	isnull(@HO64RENTERDELUXE,'N')  AS HO64RENTERDELUXE,     
	isnull(@HO66CONDOMINIUMDELUXE ,'N') as HO66CONDOMINIUMDELUXE,              
	isnull(@RESIDENCE_EMP_NUMBER,'0')        AS RESIDENCE_EMP_NUMBER,                                                                                          
	isnull(@CLERICAL_OFFICE_HO71,'0')        AS CLERICAL_OFFICE_HO71,                                                                                          
	isnull(@SALESMEN_INC_INSTALLATION,'0')   AS SALESMEN_INC_INSTALLATION,                                                                                          
	isnull(@TEACHER_ATHELETIC,'0')           AS TEACHER_ATHELETIC,                                                                                          
	isnull(@TEACHER_NOC,'0')              AS TEACHER_NOC,                                                                                          
	isnull(@INCIDENTAL_FARMING_HO72,'0')     AS INCIDENTAL_FARMING_HO72,                                                                                          
	isnull(@OTH_LOC_OPR_EMPL_HO73,'0')     AS OTH_LOC_OPR_EMPL_HO73,                
	CONVERT(VARCHAR(20),CONVERT(DECIMAL(18),@OTH_LOC_OPR_OTHERS_HO73))   AS OTH_LOC_OPR_OTHERS_HO73,                                                                                         
	-- CREDIT AND CHARGES --                                                                                
	isnull(@LOSSFREE,'0')        AS LOSSFREE,             -- discounts for Renewal                                                                                 
	isnull(@NOTLOSSFREE,'0')           AS NOTLOSSFREE,        -- discountsfor Renewal                             
	isnull(@VALUEDCUSTOMER,'0')              AS VALUEDCUSTOMER,       -- discountsfor Renewal                                                                                           
	isnull(@MULTIPLEPOLICYFACTOR,'0')   AS MULTIPLEPOLICYFACTOR,                                                                                          
	isnull(@NONSMOKER,'0')             AS NONSMOKER,                                                    
	isnull(@EXPERIENCE,'0')        AS EXPERIENCE,                                                                                          
	isnull(@CONSTRUCTIONCREDIT,'0')      AS CONSTRUCTIONCREDIT,                                                                                            
	isnull(@REDUCTION_IN_COVERAGE_C,'N')     AS REDUCTION_IN_COVERAGE_C,    --for nidhi      
	isnull(@N0_LOCAL_ALARM,'0')              AS N0_LOCAL_ALARM,                                   
	isnull(@BURGLER_ALERT_POLICE,'0')   AS BURGLER_ALERT_POLICE,                                                                                          
	isnull(@FIRE_ALARM_FIREDEPT,'0')         AS FIRE_ALARM_FIREDEPT,                                                    
	isnull(@BURGLAR,'0')      AS BURGLAR,                                                    
	isnull(@CENTRAL_FIRE,'0')           AS CENTRAL_FIRE,                           
	isnull(@INSURANCESCORE,'0')         AS INSURANCESCORE,                        
	isnull(@INSURANCESCOREDIS,'0')   AS INSURANCESCOREDIS,                                                               
	isnull(@WOODSTOVE_SURCHARGE,'0')    AS WOODSTOVE_SURCHARGE,            
	isnull(@PRIOR_LOSS_SURCHARGE,'0')        AS PRIOR_LOSS_SURCHARGE,                    
	isnull(@NOPETS,'0') AS DOGSURCHARGE,                                                                  
	isnull(@DOGFACTOR,'0')     AS DOGFACTOR,          
	--Inland Marine--                                                                                              
	isnull(@SCH_BICYCLE_DED,'0')           AS SCH_BICYCLE_DED,                                                                                     
	isnull(@SCH_BICYCLE_AMOUNT,'0')       AS SCH_BICYCLE_AMOUNT,                                                                 
	isnull(@SCH_CAMERA_DED,'0')           AS SCH_CAMERA_DED,                                                                                          
	isnull(@SCH_CAMERA_AMOUNT,'0')        AS SCH_CAMERA_AMOUNT,       
	isnull(@SCH_CELL_DED,'0')              AS SCH_CELL_DED,                                                                                          
	isnull(@SCH_CELL_AMOUNT,'0')           AS SCH_CELL_AMOUNT, 
	isnull(@SCH_FURS_DED,'0')              AS SCH_FURS_DED,                                                                        
	isnull(@SCH_FURS_AMOUNT,'0')           AS SCH_FURS_AMOUNT,                                                                                          
	isnull(@SCH_GUNS_DED,'0')              AS SCH_GUNS_DED,                                                                                          
	isnull(@SCH_GUNS_AMOUNT,'0')           AS SCH_GUNS_AMOUNT,                                                                                          
	isnull(@SCH_GOLF_DED,'0')             AS SCH_GOLF_DED,                                                                                          
	isnull(@SCH_GOLF_AMOUNT,'0')           AS SCH_GOLF_AMOUNT,                                       
	isnull(@SCH_JWELERY_DED,'0')           AS SCH_JWELERY_DED,                                                                                          
	isnull(@SCH_JWELERY_AMOUNT,'0')       AS SCH_JWELERY_AMOUNT,                                                                                     
	isnull(@SCH_MUSICAL_DED,'0')          AS SCH_MUSICAL_DED,                                                                             
	isnull(@SCH_MUSICAL_AMOUNT,'0')        AS SCH_MUSICAL_AMOUNT,                                                                                          
	isnull(@SCH_PERSCOMP_DED,'0')          AS SCH_PERSCOMP_DED,                                   
	isnull(@SCH_PERSCOMP_AMOUNT,'0')       AS SCH_PERSCOMP_AMOUNT,                                                                                          
	isnull(@SCH_SILVER_DED,'0')      AS SCH_SILVER_DED,                                                     
	isnull(@SCH_SILVER_AMOUNT,'0')        AS SCH_SILVER_AMOUNT,                 
	isnull(@SCH_STAMPS_DED,'0')            AS SCH_STAMPS_DED,                                                                  
	isnull(@SCH_STAMPS_AMOUNT,'0')         AS SCH_STAMPS_AMOUNT,              
	isnull(@SCH_RARECOINS_DED,'0')     AS SCH_RARECOINS_DED,                                                                                          	
	isnull(@SCH_RARECOINS_AMOUNT,'0')     AS SCH_RARECOINS_AMOUNT,                
	isnull(@SCH_FINEARTS_WO_BREAK_DED,'0')       AS SCH_FINEARTS_WO_BREAK_DED,                                            
	isnull(@SCH_FINEARTS_WO_BREAK_AMOUNT,'0')    AS SCH_FINEARTS_WO_BREAK_AMOUNT,                                                                                          
	isnull(@SCH_FINEARTS_BREAK_DED,'0')          AS SCH_FINEARTS_BREAK_DED,                                                                                          
	isnull(@SCH_FINEARTS_BREAK_AMOUNT,'0')       AS SCH_FINEARTS_BREAK_AMOUNT,                                   
	isnull(@SCH_HANDICAP_ELECTRONICS_DED,'0')    AS SCH_HANDICAP_ELECTRONICS_DED,                        
	isnull(@SCH_HANDICAP_ELECTRONICS_AMOUNT,'0') AS SCH_HANDICAP_ELECTRONICS_AMOUNT,                  
	isnull(@SCH_HEARING_AIDS_DED,'0')   AS  SCH_HEARING_AIDS_DED,                  
	isnull(@SCH_HEARING_AIDS_AMOUNT,'0')   AS SCH_HEARING_AIDS_AMOUNT,                  
	isnull(@SCH_INSULIN_PUMPS_DED,'0')    AS   SCH_INSULIN_PUMPS_DED,                  
	isnull(@SCH_INSULIN_PUMPS_AMOUNT ,'0')   AS SCH_INSULIN_PUMPS_AMOUNT,    
	isnull(@SCH_MART_KAY_DED,'0')     AS   SCH_MART_KAY_DED,                  
	isnull(@SCH_MART_KAY_AMOUNT,'0')    AS   SCH_MART_KAY_AMOUNT,                  
	isnull(@SCH_PERSONAL_COMPUTERS_LAPTOP_DED,'0')  AS  SCH_PERSONAL_COMPUTERS_LAPTOP_DED,                    
	isnull(@SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT,'0') AS SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT,                    
	isnull(@SCH_SALESMAN_SUPPLIES_DED,'0')      AS SCH_SALESMAN_SUPPLIES_DED,                  
	isnull(@SCH_SALESMAN_SUPPLIES_AMOUNT,'0')     AS SCH_SALESMAN_SUPPLIES_AMOUNT,                  
	isnull(@SCH_SCUBA_DRIVING_DED,'0')    AS SCH_SCUBA_DRIVING_DED,                  
	isnull(@SCH_SCUBA_DRIVING_AMOUNT,'0')    AS SCH_SCUBA_DRIVING_AMOUNT,                  
	isnull(@SCH_SNOW_SKIES_DED,'0')        AS  SCH_SNOW_SKIES_DED,                  
	isnull(@SCH_SNOW_SKIES_AMOUNT,'0')       AS  SCH_SNOW_SKIES_AMOUNT,                    
	isnull(@SCH_TACK_SADDLE_DED,'0')   AS SCH_TACK_SADDLE_DED,                           
	isnull(@SCH_TACK_SADDLE_AMOUNT,'0')   AS SCH_TACK_SADDLE_AMOUNT,                         
	isnull(@SCH_TOOLS_PREMISES_DED,'0')      AS SCH_TOOLS_PREMISES_DED,                  
	isnull(@SCH_TOOLS_PREMISES_AMOUNT,'0')       AS SCH_TOOLS_PREMISES_AMOUNT,                  
	isnull(@SCH_TOOLS_BUSINESS_DED,'0')     AS SCH_TOOLS_BUSINESS_DED,                   
	isnull(@SCH_TOOLS_BUSINESS_AMOUNT,'0')      AS SCH_TOOLS_BUSINESS_AMOUNT ,                       
	isnull(@SCH_TRACTORS_DED,'0')      AS  SCH_TRACTORS_DED,                    
	isnull(@SCH_TRACTORS_AMOUNT,'0')     AS SCH_TRACTORS_AMOUNT,                  
	isnull(@SCH_TRAIN_COLLECTIONS_DED,'0')   AS SCH_TRAIN_COLLECTIONS_DED,                  
	isnull(@SCH_TRAIN_COLLECTIONS_AMOUNT,'0')    AS SCH_TRAIN_COLLECTIONS_AMOUNT,                  
	isnull(@SCH_WHEELCHAIRS_DED,'0')    AS SCH_WHEELCHAIRS_DED,                   
	isnull(@SCH_WHEELCHAIRS_AMOUNT,'0')    AS   SCH_WHEELCHAIRS_AMOUNT,
	isnull(@MINESUBSIDENCEADDITIONAL,'0')	AS	MINESUBSIDENCE_ADDITIONAL,  
	isnull(@SCH_CAMERA_PROF_AMOUNT,'0')    as     SCH_CAMERA_PROF_AMOUNT,
	isnull(@SCH_CAMERA_PROF_DED,'0')	as		SCH_CAMERA_PROF_DED,  
	isnull(@SCH_MUSICAL_REMUN_AMOUNT,'0') as	SCH_MUSICAL_REMUN_AMOUNT, 
	isnull(@SCH_MUSICAL_REMUN_DED,'0')	as		SCH_MUSICAL_REMUN_DED,             
	@TERRITORYCODES        AS TERRITORYCODES,          
	@EARTHQUAKEZONE   AS  EARTHQUAKEZONE,        
	isnull(@COVERAGEVALUE,'0')         AS COVERAGEVALUE,                                                                                          
	@PRODUCT_PREMIER       AS PREMIUMGROUP ,              
	@POLICYTYPE                AS POLICYTYPE   ,                                                       
	--Hard Coded To be removed                                                                            
	isnull(@PRIORLOSSSURCHARGE,'0')  AS PRIORLOSSSURCHARGE,                                                      
	@TERRITORYNAME    AS TERRITORYNAME,                                                      
	@TERRITORYCOUNTY   AS TERRITORYCOUNTY,                                                      
	ISNULL(@BREEDOFDOG,'')        AS BREEDOFDOG        ,                                                  
	@ISACTIVE        AS ISACTIVE,
	@SUBPROTDIS		AS SUBPROTDIS,
	ISNULL(@YEARSCONTINSURED,'0')  AS YEARSCONTINSURED,
	ISNULL(@YEARSCONTINSUREDWITHWOLVERINE,'0')	AS YEARSCONTINSUREDWITHWOLVERINE,
	@TOTALLOSS	AS	TOTALLOSS
 END          
 SET    quoted_identifier ON               

GO

