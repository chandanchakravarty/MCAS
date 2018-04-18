/*----------------------------------------------------------                                                                    
Proc Name           : Dbo.Proc_GetPolicyRatingInformationFor_HO  746,79,1,1                                                             
Created by          : shafi                                                                    
Date                : 08/02/2006                                                                    
Purpose             : To get the information for creating the input xml for Homeownwrs                                                                    
Revison History     :                                                                    
Used In             : Wolverine                                                   
                                                            
------------------------------------------------------------                                                                    
Date     Review By          Comments                                                                    
------   ------------       -------------------------*/                                                                    
 --Drop PROC Proc_GetPolicyRatingInformationFor_HO                                                      
  /*----------------------------------------------------------                                                                                          
Proc Name           : Dbo.Proc_GetPolicyRatingInformationFor_HO  817,29,2,1                                                                               
Created by          : shafi                                                                                          
Date                : 08/02/2006                                                                                          
Purpose             : To get the information for creating the input xml for Homeownwrs                                                                                          
Revison History     :                                                                                          
Used In             : Wolverine                                                                         
                                                                                  
------------------------------------------------------------                                                                                          
Date     Review By          Comments                                                                                          
------   ------------       -------------------------*/                                                                                          
 --                   
-- DROP PROC dbo.Proc_GetPolicyRatingInformationFor_HO                                                                                         
ALTER  PROC [dbo].[Proc_GetPolicyRatingInformationFor_HO] --28924,1 , 2, 1                                               
(                                                                                                
@CUSTOMERID      INT,                                                                                          
@POLICYID       INT,                                                                                          
@POLICYVERSIONID    INT,                                                                                          
@DWELLINGID      INT                                                                                                 
)                                                                                                
AS                                                                                                
                                                                                                
BEGIN                                                                                                
SET QUOTED_IDENTIFIER OFF                      
                                     
                       
-- GENERAL VARIABLES                                                                                              
DECLARE         @LOB_ID              nvarchar(20)                                           
DECLARE         @STATENAME                 nvarchar(20)                                                                                              
DECLARE         @STATE_ID                 nvarchar(20)                                                                                          
DECLARE         @QUOTEEFFDATE                   nvarchar(20)                                                                                                
DECLARE         @QUOTEEXPDATE                   nvarchar(20)                                                                                               
DECLARE         @TERMFACTOR                     nvarchar(20)                                                                                               
DECLARE         @SEASONALSECONDARY              nvarchar(20)                                                                                                
DECLARE         @WOLVERINEINSURESPRIMARY        nvarchar(20)                                                                                                
DECLARE         @PRODUCTNAME                    nvarchar(20)                                    
DECLARE         @PRODUCT_PREMIER                nvarchar(20)                                          
DECLARE         @REPLACEMENTCOSTFACTOR          nvarchar(25)                                                   
DECLARE         @DWELLING_LIMITS          decimal(20)                                            
DECLARE         @PROTECTIONCLASS                nvarchar(20)            
DECLARE         @FIREPROTECTIONCLASS           nvarchar(20)                                                                    
DECLARE         @DISTANCET_FIRESTATION          nvarchar(20)                                                                                        
DECLARE         @FEET2HYDRANT       nvarchar(20)                                                                       
DECLARE         @DEDUCTIBLE                     nvarchar(20)                                                                                                
DECLARE         @EXTERIOR_CONSTRUCTION       nvarchar(20)                                        
DECLARE   @EXTERIOR_CONSTRUCTION_DESC   nvarchar(250)                                                                                                
DECLARE         @EXTERIOR_CONSTRUCTION_F_M      nvarchar(20)                                                             
DECLARE         @DOC                            nvarchar(20)                                                                   
DECLARE   @AGEOFHOME                      nvarchar(20)                                                       
DECLARE         @NUMBEROFFAMILIES               nvarchar(20)                                                                                                
DECLARE         @NUMBEROFUNITS                  nvarchar(20)                                                                                                
DECLARE         @PERSONALLIABILITY_LIMIT        nvarchar(20)                                                                                                
DECLARE         @PERSONALLIABILITY_PREMIUM      nvarchar(20)                                                                                                
DECLARE         @PERSONALLIABILITY_DEDUCTIBLE   nvarchar(20)                                   
DECLARE         @INSURANCESCOREDIS              nvarchar(20)                                                                            
                                                                                          
DECLARE         @MEDICALPAYMENTSTOOTHERS_LIMIT          nvarchar(20)                         
DECLARE         @MEDICALPAYMENTSTOOTHERS_PREMIUM        nvarchar(20)                                                 
DECLARE         @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE     nvarchar(20)                                                                                                                            
DECLARE         @FORM_CODE                       nvarchar(20)                                                                                
DECLARE         @PRIORLOSSSURCHARGE              nvarchar(1)         
DECLARE         @HO20                          nvarchar(20)                                                                                     
DECLARE         @HO21                         nvarchar(1)                                                                                            
DECLARE         @HO23     nvarchar(1)                                                   
DECLARE         @HO22             nvarchar(1)                                                 
DECLARE         @HO24            nvarchar(1)                                                                                          
DECLARE         @HO25                          nvarchar(1)                                                                                                
DECLARE         @HO34                          nvarchar(1)                                                                                            
DECLARE         @HO11                          nvarchar(1)                                                                                                
DECLARE         @HO32                          nvarchar(1)                                                                                            
DECLARE   @HO277              nvarchar(1)                                                                       
DECLARE         @HO455                   nvarchar(1)                                                                                                
DECLARE         @HO327                        nvarchar(10)      
DECLARE         @HO33                       nvarchar(20)                                                         
DECLARE         @HO315      nvarchar(1)                                                                                              
DECLARE   @HO9                           nvarchar(1)                                                                                              
DECLARE         @HO287       nvarchar(1)                                                                                             
DECLARE         @HO200                         nvarchar(1)                                                                                             
DECLARE         @HO64RENTERDELUXE            nvarchar(1)                                    
                                    
DECLARE         @HO96FINALVALUE                nvarchar(20)                                                                                                
DECLARE   @HO96INCLUDE                   nvarchar(20)                                                                       
DECLARE         @HO96ADDITIONAL                nvarchar(20)                                                
DECLARE         @HO48INCLUDE                   nvarchar(20)                                                                             
DECLARE         @HO48ADDITIONAL                decimal                                                                                          
DECLARE   @HO40INCLUDE                   nvarchar(20)                                                                                                
DECLARE         @HO40ADDITIONAL                nvarchar(20)                                                                    
DECLARE         @HO42ADDITIONAL                nvarchar(20)                                                                                                
DECLARE     @REPAIRCOSTINCLUDE             nvarchar(20)        
DECLARE   @REPAIRCOSTADDITIONAL          nvarchar(20)                                                                 
                  
DECLARE  @PERSONALPROPERTYINCREASEDLIMITINCLUDE      nvarchar(20)       
DECLARE @PERSONALPROPERTYINCREASEDLIMITADDITIONAL   nvarchar(20)                                         
DECLARE         @PERSONALPROPERTYAWAYINCLUDE              nvarchar(20)                                                                                                
DECLARE         @PERSONALPROPERTYAWAYADDITIONAL             nvarchar(20)                                                                    
DECLARE         @UNSCHEDULEDJEWELRYINCLUDE                  nvarchar(20)                                                                                                
DECLARE         @UNSCHEDULEDJEWELRYADDITIONAL               nvarchar(20)                                                                                              
DECLARE         @MONEYINCLUDE                         nvarchar(20)                                                                                                
DECLARE         @MONEYADDITIONAL                      nvarchar(20)                                                                                             
DECLARE         @SECURITIESINCLUDE                    nvarchar(20)                                                                                                
DECLARE         @SECURITIESADDITIONAL                 nvarchar(20)                                                                                                
DECLARE   @SILVERWAREINCLUDE                    nvarchar(20)                                       
DECLARE         @SILVERWAREADDITIONAL                 nvarchar(20)                                                                                                
DECLARE         @FIREARMSINCLUDE             nvarchar(20)                                                                                                
DECLARE         @FIREARMSADDITIONAL                   nvarchar(20)                                                                                                
DECLARE         @HO312INCLUDE                         nvarchar(20)                                                                                            
DECLARE         @HO312ADDITIONAL                      nvarchar(20)                                                               
DECLARE         @ADDITIONALLIVINGEXPENSEINCLUDE       nvarchar(20)                                                                                                
DECLARE         @ADDITIONALLIVINGEXPENSEADDITIONAL    nvarchar(20)                                                                                                
DECLARE         @HO53INCLUDE                nvarchar(20)                                                                                                
DECLARE         @HO53ADDITIONAL                       nvarchar(20)                                                                 
DECLARE         @HO35INCLUDE               nvarchar(20)                                                                  
DECLARE         @HO35ADDITIONAL                       nvarchar(20)                                                                                                
DECLARE         @SPECIFICSTRUCTURESINCLUDE            nvarchar(20)                                            
DECLARE         @SPECIFICSTRUCTURESADDITIONAL         nvarchar(20)                                                                                               
DECLARE        @HO66CONDOMINIUMDELUXE          nvarchar(1)         
DECLARE   @HO493 CHAR(1)          
SET    @HO493='N'                                                                                            
--LIABILITY OPTIONS--                                                                                                                                                                                           
DECLARE         @OCCUPIED_INSURED                  nvarchar(20)                                                                   
DECLARE         @RESIDENCE_PREMISES nvarchar(20)               
DECLARE         @OTHER_LOC_1FAMILY   nvarchar(20)                      
DECLARE         @OTHER_LOC_2FAMILY       nvarchar(20)                                                                                                
DECLARE         @ONPREMISES_HO42                   nvarchar(20)                                                                                                
DECLARE         @LOCATED_OTH_STRUCTURE             nvarchar(20)                                                                                                
DECLARE         @INSTRUCTIONONLY_HO42              nvarchar(20)                                                                                                
DECLARE         @OFF_PREMISES_HO43                 nvarchar(20)                                                                                           
DECLARE         @PIP_HO82                          nvarchar(20)                                                                                                
DECLARE         @RESIDENCE_EMP_NUMBER              nvarchar(20)                                                                                                
DECLARE         @CLERICAL_OFFICE_HO71              nvarchar(20)                                                                                                
DECLARE         @SALESMEN_INC_INSTALLATION         nvarchar(20)                                                                                                
DECLARE         @TEACHER_ATHELETIC                 nvarchar(20)                                                                                                
DECLARE         @TEACHER_NOC                       nvarchar(20)                                                            
DECLARE         @INCIDENTAL_FARMING_HO72           nvarchar(20)                                                                                                
DECLARE         @OTH_LOC_OPR_EMPL_HO73    nvarchar(20)                                                                                                
DECLARE         @OTH_LOC_OPR_OTHERS_HO73           nvarchar(20)                                                                      
 DECLARE        @HO42       nvarchar(20)                                                   
 --CREDIT AND CHARGES--                                                                                                                 
                                                                                              
DECLARE   @LOSSFREE                      nvarchar(20)                                                                                                
DECLARE   @NOTLOSSFREE                   nvarchar(20)                                                                       
DECLARE         @VALUEDCUSTOMER                nvarchar(20)                                                                          
DECLARE         @MULTIPLEPOLICYFACTOR          nvarchar(20)                                  
DECLARE         @NONSMOKER                     nvarchar(20)                                                                                                
DECLARE         @EXPERIENCE                    nvarchar(20)                                                                   
                                                               
DECLARE         @CONSTRUCTIONCREDIT            nvarchar(20)                                                                                                
DECLARE         @REDUCTION_IN_COVERAGE_C       nvarchar(20)                                                                                                
DECLARE         @N0_LOCAL_ALARM                nvarchar(20)                                                                                     
                                    
--PROTECTIVE DEVICE                                                               
                                    
DECLARE         @BURGLER_ALERT_POLICE       nvarchar(20)        
DECLARE         @FIRE_ALARM_FIREDEPT     nvarchar(20)             
DECLARE         @BURGLAR                  nvarchar(20)                     
DECLARE         @BURGLAR_ACORD            nvarchar(20)                                   
DECLARE         @CENTRAL_FIRE                  nvarchar(20)                                             
DECLARE         @CENT_ST_BURG_FIRE             nvarchar(20)                                                                                     
DECLARE         @DIR_FIRE_AND_POLICE           nvarchar(20)                                                                   
DECLARE         @LOC_FIRE_GAS                  nvarchar(20)                                                         
DECLARE         @TWO_MORE_FIRE             nvarchar(20)                                                                                     
                                             
                                                                                               
DECLARE         @INSURANCESCORE               nvarchar(20)                                                                                                
DECLARE         @WOODSTOVE_SURCHARGE          nvarchar(20)                                                                                                
DECLARE         @PRIOR_LOSS_SURCHARGE         nvarchar(20)                                                                                                
DECLARE         @DOGSURCHARGE                 nvarchar(20)                                                                                                
DECLARE         @DOGFACTOR                    nvarchar(20)                                                                                                
                         
 --INLAND MARINE--                                                                                                    
                                                                                              
DECLARE         @SCH_BICYCLE_DED               decimal(20)                                                
DECLARE         @SCH_BICYCLE_AMOUNT            decimal(20)                                                                                                
DECLARE         @SCH_CAMERA_DED       decimal(20)                                                                                                
DECLARE         @SCH_CAMERA_AMOUNT             decimal(20)                                                                                                
DECLARE         @SCH_CELL_DED                  decimal(20)                                                                                                
DECLARE         @SCH_CELL_AMOUNT               decimal(20)                                                                          
DECLARE   @SCH_FURS_DED                  decimal(20)                                                                                                
DECLARE         @SCH_FURS_AMOUNT               decimal(20)                                                                                 
DECLARE         @SCH_GUNS_DED                  decimal(20)                                                                             
DECLARE         @SCH_GUNS_AMOUNT               decimal(20)                               
DECLARE         @SCH_GOLF_DED                  decimal(20)                                                                                       
DECLARE         @SCH_GOLF_AMOUNT               decimal(20)                                                                                                
DECLARE         @SCH_JWELERY_DED               decimal(20)                                                               
DECLARE         @SCH_JWELERY_AMOUNT            decimal(20)          
DECLARE         @SCH_MUSICAL_DED               decimal(20)                                       
DECLARE         @SCH_MUSICAL_AMOUNT            decimal(20)                                    
DECLARE         @SCH_PERSCOMP_DED              decimal(20)                        
DECLARE         @SCH_PERSCOMP_AMOUNT           decimal(20)                                                                     
DECLARE         @SCH_SILVER_DED                decimal(20)                            
DECLARE         @SCH_SILVER_AMOUNT             decimal(20)                                                                                                
DECLARE         @SCH_STAMPS_DED                decimal(20)                                                                 
DECLARE         @SCH_STAMPS_AMOUNT             decimal(20)                                                                                                
DECLARE         @SCH_RARECOINS_DED        decimal(20)                                                                          
DECLARE         @SCH_RARECOINS_AMOUNT          decimal(20)                                                                                            
DECLARE         @SCH_FINEARTS_WO_BREAK_DED     decimal(20)                                                                                                
DECLARE         @SCH_FINEARTS_WO_BREAK_AMOUNT    decimal(20)                                               
DECLARE         @SCH_FINEARTS_BREAK_DED          decimal(20)                                                                     
DECLARE         @SCH_FINEARTS_BREAK_AMOUNT       decimal(20)                                                                                               
DECLARE         @SCH_HANDICAP_ELECTRONICS_DED    decimal(20)                                    
DECLARE         @SCH_HANDICAP_ELECTRONICS_AMOUNT        decimal(20)                                           
DECLARE         @SCH_HEARING_AIDS_DED         decimal(20)                       
DECLARE         @SCH_HEARING_AIDS_AMOUNT         decimal(20)                                           
DECLARE         @SCH_INSULIN_PUMPS_DED          decimal(20)                                           
DECLARE         @SCH_INSULIN_PUMPS_AMOUNT        decimal(20)                                           
DECLARE         @SCH_MART_KAY_DED           decimal(20)                                           
DECLARE         @SCH_MART_KAY_AMOUNT         decimal(20)                                           
DECLARE         @SCH_PERSONAL_COMPUTERS_LAPTOP_DED      decimal(20)                                           
DECLARE         @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT   decimal(20)                                           
DECLARE         @SCH_SALESMAN_SUPPLIES_DED        decimal(20)                                           
DECLARE         @SCH_SALESMAN_SUPPLIES_AMOUNT       decimal(20)                                           
DECLARE         @SCH_SCUBA_DRIVING_DED         decimal(20)                                           
DECLARE         @SCH_SCUBA_DRIVING_AMOUNT         decimal(20)                                           
DECLARE         @SCH_SNOW_SKIES_DED          decimal(20)                                           
DECLARE         @SCH_SNOW_SKIES_AMOUNT    decimal(20)                                           
DECLARE         @SCH_TACK_SADDLE_DED          decimal(20)                                           
DECLARE         @SCH_TACK_SADDLE_AMOUNT          decimal(20)                                           
DECLARE         @SCH_TOOLS_PREMISES_DED          decimal(20)                                           
DECLARE         @SCH_TOOLS_PREMISES_AMOUNT        decimal(20)                                    
DECLARE         @SCH_TOOLS_BUSINESS_DED         decimal(20)        
DECLARE         @SCH_TOOLS_BUSINESS_AMOUNT        decimal(20)          
DECLARE         @SCH_TRACTORS_DED       decimal(20)                                           
DECLARE         @SCH_TRACTORS_AMOUNT         decimal(20)                                          
DECLARE         @SCH_TRAIN_COLLECTIONS_DED        decimal(20)                                     
DECLARE         @SCH_TRAIN_COLLECTIONS_AMOUNT        decimal(20)             
DECLARE         @SCH_WHEELCHAIRS_DED         decimal(20)                             
DECLARE         @SCH_WHEELCHAIRS_AMOUNT          decimal(20)                                
                                                             
-------  Rest   ---------------        
                                
DECLARE         @TERRITORYCODES             nvarchar(20)                                               
DECLARE         @EARTHQUAKEZONE            nvarchar(20)                                                         
DECLARE         @COVERAGEVALUE              decimal(20)                                                                                                
DECLARE         @TEMPERATURE                nvarchar(20)                                                             
DECLARE         @SMOKE                      nvarchar(20)                                                                                                
DECLARE         @DWELLING                 nvarchar(20)                                                                                                
DECLARE         @YEARS                      nvarchar(20)                                                                                                
DECLARE   @CHIMNEYSTOVE               nvarchar(20)                                                                                                
DECLARE         @PREMIUMGROUP               nvarchar(20)                                                             
DECLARE         @OTHERSTRUCTURES_LIMIT      nvarchar(20)                                                                                                
DECLARE         @PERSONALPROPERTY_LIMIT     nvarchar(20)                                                     
DECLARE         @LOSSOFUSE_LIMIT            nvarchar(20)                                                                 
DECLARE         @COVERAGEFACTOR             nvarchar(20)                                                        
DECLARE         @BASEPREMIUM                nvarchar(20)                                                                                                
DECLARE         @CLAIMS                     nvarchar(20)                                                        
DECLARE         @AMOUNT                     nvarchar(20)                                                                         
DECLARE         @NOPETS               int 
DECLARE         @AGEHOMECREDIT              nvarchar(20)                                                                                       
DECLARE         @POLICYTYPE                 INT                                      
DECLARE         @UNDERCONAPP                nvarchar(20)                                                        
DECLARE         @VALUESCUSTOMERAPP          nvarchar(20)                                                                      
DECLARE         @INSUREWITHWOL              SMALLINT                                                             
DECLARE         @BREEDOFDOG                 nvarchar(20)                                                              
DECLARE         @TERRITORYNAME              nvarchar(20)                                                              
DECLARE         @TERRITORYCOUNTY    nvarchar(20)                                                              
DECLARE         @ISACTIVE                nvarchar(2)                                   
DECLARE   @APPDATE      DATETIME                                                               
DECLARE   @POLICYSTATUS     VARCHAR(30)                             
DECLARE   @NEWBUSINESSFACTOR    CHAR(10)        
DECLARE   @INCEPTIONDATE     VARCHAR(100)               
DECLARE   @POL_NUMBER      VARCHAR(20)              
DECLARE   @POL_VERSION VARCHAR(20)     
DECLARE   @MINESUBSIDENCEADDITIONAL    VARCHAR(20)      
DECLARE   @SUBPROTDIS VARCHAR(20)                               
DECLARE   @YEARSCONTINSURED NVARCHAR(20)      
DECLARE   @YEARSCONTINSUREDWITHWOLVERINE NVARCHAR(20)      
DECLARE   @TOTALLOSS NVARCHAR(20)       
DECLARE   @SCH_CAMERA_PROF_DED NVARCHAR(20)      
DECLARE   @SCH_CAMERA_PROF_AMOUNT NVARCHAR(40)      
DECLARE   @SCH_MUSICAL_REMUN_DED NVARCHAR(20)      
DECLARE   @SCH_MUSICAL_REMUN_AMOUNT NVARCHAR(40)                                      
SELECT                                     
 @LOB_ID         =   POLICY_LOB ,                                                                        
 @TERMFACTOR   =   ISNULL(APP_TERMS,'12'),                                                                                               
 @QUOTEEFFDATE   =   ISNULL(APP_EFFECTIVE_DATE,'02/14/2012'),                                                                                                 
 @APPDATE   =   APP_EXPIRATION_DATE,                                  
 @POLICYSTATUS   =   POLICY_STATUS  ,                              
 @POLICYTYPE   =   ISNULL(POLICY_TYPE,11193),                                     
 @INCEPTIONDATE  =   ISNULL(CONVERT(VARCHAR(10),APP_INCEPTION_DATE,101),''),                                    
 @POL_NUMBER            =   POLICY_NUMBER,                                    
 @POL_VERSION  =   POLICY_DISP_VERSION,                      
 @STATENAME= UPPER(ISNULL(STATE_NAME,'')),                                                                                             
 @STATE_ID= UPPER(ISNULL(POL_CUSTOMER_POLICY_LIST.STATE_ID,'0'))        
         
FROM                                                                                               
 POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST  WITH (NOLOCK) ON  MNT_COUNTRY_STATE_LIST.STATE_ID=22                                                
WHERE    CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                                                                
                                                         
--IF POLICY IN UNDER RENEWAL THEN SET NEW NEWBUSINESSFACTOR TO 'N' ELSE 'Y'                                                            
-- POLICY ID 18- Renewal Committed                                    
-- POLICY ID 5- Renewal In Progress                                    
IF EXISTS (SELECT * FROM POL_POLICY_PROCESS  WITH (NOLOCK)                                   
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND PROCESS_ID IN (18,5))                                                           
 SET @NEWBUSINESSFACTOR='REN'                                                            
ELSE                                                       
 SET @NEWBUSINESSFACTOR= 'Y'                                                            
                                                           
IF EXISTS (SELECT CUSTOMER_ID from APP_PRIOR_LOSS_INFO WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID                                                             
            AND LOB='1' AND AMOUNT_PAID > 2500 AND  OCCURENCE_DATE                                                   
              > (DATEADD(YEAR,-3,@QUOTEEFFDATE)) )                                                            
 SET @PRIORLOSSSURCHARGE='Y'                                                            
ELSE                                                            
 SET @PRIORLOSSSURCHARGE='N'                                                  
            
        
        
-- Insurance score         
DECLARE @PROCCESSID INT        
DECLARE @CACELATION_TYPE INT        
DECLARE @PROCESS_STATUS nvarchar(40)        
SELECT @PROCCESSID=PROCESS_ID,        
    @CACELATION_TYPE=CANCELLATION_TYPE,        
       @PROCESS_STATUS=PROCESS_STATUS        
FROM POL_POLICY_PROCESS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@POLICYVERSIONID         
        
IF((@PROCCESSID=4 OR @PROCCESSID=16) AND  @CACELATION_TYPE=14244 AND @PROCESS_STATUS != 'ROLLBACK')        
BEGIN        
 SELECT @INSURANCESCORE =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                            
         WHEN -1 THEN '100'           
         WHEN  -2 THEN 'NOHITNOSCORE'                 
       ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END         
--     @INSURANCESCOREDIS =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                        
--         WHEN -1 THEN '100'                
--         WHEN  -2 THEN 'NOHITNOSCORE'                 
--       ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END         
   FROM   CLT_CUSTOMER_LIST  WITH (NOLOCK)                                                           
   WHERE  CUSTOMER_ID =@CUSTOMERID        
END        
ELSE        
BEGIN        
SELECT        
@INSURANCESCORE=  case isnull(APPLY_INSURANCE_SCORE,-1)     --INSURANCESCORE                            
 when -1 then '100'                          
 when  -2 then 'NOHITNOSCORE'                      
 else convert(nvarchar(20),APPLY_INSURANCE_SCORE) end                        
-- @INSURANCESCOREDIS= case isnull(APPLY_INSURANCE_SCORE,-1)     --INSURANCESCOREDIS FOR DISCOUNTS AND SURCHAGES                            
-- when -1 then 'N'                          
-- when  -2 then 'N'                           
-- else cast(APPLY_INSURANCE_SCORE as nvarchar(100)) end                             
FROM                                                                                               
 POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)        
WHERE    CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                                                                
 END                          
-- BASIC POLICY PAGE                                                                         
----- Territory Code                                                                                          
--  **********************************************************************************************************                                                                                              
----PRODUCT NAME                                                                       
DECLARE @POLICY_CODE VARCHAR(100)                                                                                                 
SELECT                                                          
 @POLICY_CODE    = UPPER(ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,''))                                                
FROM                                                                                                
 POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) INNER JOIN MNT_LOOKUP_VALUES  WITH (NOLOCK) ON POL_CUSTOMER_POLICY_LIST.POLICY_TYPE = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                              
  
WHERE                             
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                                                   
                                                                                                  
-----------GET THE POLICY FROM THE CODE----------------                                                                        
                                                     
SET @PRODUCTNAME = dbo.piece(@POLICY_CODE,'^',1)                                                                        
SET @PRODUCT_PREMIER = dbo.piece(@POLICY_CODE,'^',2)                                                                   
       
--IF(@PRODUCT_PREMIER='REPLACE')      
--BEGIN      
-- set @PRODUCT_PREMIER='Replacement'      
--END                                                         
------------------------------------------------------------------------------                        
-- FORM_CODE                                                 
DECLARE @FCVAR   NVARCHAR(6)                                    
DECLARE @FIRST_CHAR   NVARCHAR(2)                                                     
DECLARE  @LOOKUP_FRAME_OR_MASONRY   NVARCHAR(10)                   
DECLARE  @NO_OF_FAMILIES        NVARCHAR(2)                                                                   
                                                     
SELECT                                  
 @EXTERIOR_CONSTRUCTION=EXTERIOR_CONSTRUCTION                                                   
FROM       
 POL_HOME_RATING_INFO WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND DWELLING_ID =@DWELLINGID                                                                                            
 

                                                                                              
IF(@EXTERIOR_CONSTRUCTION IS NULL)                                                                            
 BEGIN                                                                                                
  SET @EXTERIOR_CONSTRUCTION =''                                                                                                
  SET @LOOKUP_FRAME_OR_MASONRY=''                                                                                                   
  SET @EXTERIOR_CONSTRUCTION_DESC =''                                                                     
 END                     
ELSE                                                                   
BEGIN                                                         
-------                       
-----NEW                                                             
SELECT                                                                                       
  @LOOKUP_FRAME_OR_MASONRY = MNT_LOOKUP_VALUES.LOOKUP_FRAME_OR_MASONRY, --Picking Code from the LOOKUP                                                                                         
  @EXTERIOR_CONSTRUCTION_DESC = MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC                                        
FROM                                                                                                           
 POL_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES  WITH (NOLOCK)                                                                   
 ON POL_HOME_RATING_INFO.EXTERIOR_CONSTRUCTION = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                                            
WHERE                                                                                                           
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                        
--------------FORM_CODE--------                                                            
DECLARE @FIRE_STATION_DIST int                                          
DECLARE @HYDRANT_DIST varchar(20)                                                            
                      
SELECT                                                    
@PROTECTIONCLASS = ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0'),                      
@FIRE_STATION_DIST = ISNULL(FIRE_STATION_DIST,0),                                                   
@HYDRANT_DIST = ISNULL(HYDRANT_DIST,'0')                                                             
FROM                                                                                       
 POL_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                                                                                      
 ON POL_HOME_RATING_INFO.PROT_CLASS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                     
WHERE                     
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND DWELLING_ID =@DWELLINGID         
                      
EXECUTE @FORM_CODE = Proc_GetProtectionClass @PROTECTIONCLASS,@FIRE_STATION_DIST,@HYDRANT_DIST,'HOME','RATES' /**RATES IF CALLED FROM RATES*/                                
SET @PROTECTIONCLASS = '0' + @FORM_CODE  /*Protection class depends on the rated class not the one that is selected*/                                                                    
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
END                    
                      
----   GET SEASONAL                         
DECLARE @LOCATIONID NVARCHAR(20)                                                                                       
DECLARE @PRIMARYLOC nvarchar(5)                                                                                                    
SELECT  @PRIMARYLOC = ISNULL(LOCATION_TYPE,0) ,                      
 @WOLVERINEINSURESPRIMARY= isnull(IS_PRIMARY,'N'),                      
 @LOCATIONID = POL_DWELLINGS_INFO.LOCATION_ID,                                                                                                  
 @DOC     =  CONVERT(VARCHAR(10),POL_DWELLINGS_INFO.YEAR_BUILT,101),                                                                                   
 @AGEOFHOME=convert(nvarchar(20),DATEDIFF(YEAR,convert(nvarchar(20),ISNULL(POL_DWELLINGS_INFO.YEAR_BUILT,0)),@QUOTEEFFDATE)),                        
 @REPLACEMENTCOSTFACTOR = POL_DWELLINGS_INFO.REPLACEMENT_COST ,                                                            
 @ISACTIVE      = ISNULL(POL_DWELLINGS_INFO.IS_ACTIVE,'N')                                     
FROM POL_DWELLINGS_INFO WITH (NOLOCK)                                                                                 
LEFT OUTER JOIN  POL_LOCATIONS WITH (NOLOCK) on                                                                                   
                                 POL_LOCATIONS.LOCATION_ID=POL_DWELLINGS_INFO.LOCATION_ID                                                                                  
     AND POL_LOCATIONS.CUSTOMER_ID=POL_DWELLINGS_INFO.CUSTOMER_ID                                                                                  
                             AND POL_LOCATIONS.POLICY_ID=POL_DWELLINGS_INFO.POLICY_ID                    
                         AND POL_LOCATIONS.POLICY_VERSION_ID=POL_DWELLINGS_INFO.POLICY_VERSION_ID                                                          
                                                  
WHERE                                                                                           
POL_DWELLINGS_INFO.CUSTOMER_ID =@CUSTOMERID AND POL_DWELLINGS_INFO.POLICY_ID=@POLICYID AND POL_DWELLINGS_INFO.POLICY_VERSION_ID=@POLICYVERSIONID AND                    
POL_DWELLINGS_INFO.DWELLING_ID=@DWELLINGID                                                                                         
                                                                     
IF  @PRIMARYLOC = 11812 --Unique code for Primary location                                               
 SET @SEASONALSECONDARY = 'N'             ELSE                                               
 SET @SEASONALSECONDARY = 'Y'                    
       
IF @WOLVERINEINSURESPRIMARY is null                            
 set @WOLVERINEINSURESPRIMARY ='N'                                                          
 ------------------------------check                                                            
--DEDUCTIBLE AND ZIP-CODE         
                                                            
DECLARE @ZIPCODE    nvarchar(10)                                      
IF (@LOCATIONID = '')                                        
 BEGIN                                                                 
 SET @ZIPCODE=''                                                         
 END                                                              
ELSE                                                          
BEGIN                                                        
 SELECT                                                                                     
  @ZIPCODE = LOC_ZIP                                                              
  FROM                       
  POL_LOCATIONS  WITH (NOLOCK)                                                                                                 
 WHERE                                                        
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and LOCATION_ID = @LOCATIONID                                                                                                  
END                                                                                               
----------------check end                                                                                            
IF ( @ZIPCODE !='')                                                                              
BEGIN                                                                                                 
 SELECT                                                                                                 
  @TERRITORYCODES = ISNULL(TERR ,''),                                           
  @EARTHQUAKEZONE  = ISNULL(EARTHQUAKE_ZONE,''),                                                               
  @TERRITORYNAME  = ISNULL(CITY,''),                                                              
  @TERRITORYCOUNTY= ISNULL(COUNTY,'')                                                                                       
 FROM                                                                                          
  MNT_TERRITORY_CODES WITH (NOLOCK)                                                                                                    
 WHERE                                 
   ZIP = (substring(ltrim(rtrim(ISNULL(@ZIPCODE,''))),1,5)) AND  LOBID=@LOB_ID         
   AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                                                               
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
--NO OF FAMILIES                                        
SELECT                                                                                               
 @NO_OF_FAMILIES = ISNULL(NO_OF_FAMILIES,'0'),                                                                                          
 @FEET2HYDRANT      =  LOOKUP_VALUE_DESC    ,                                    
 @NUMBEROFUNITS = isnull(NEED_OF_UNITS,'0')                                                                               
FROM                                                      
 POL_HOME_RATING_INFO WITH (NOLOCK) inner  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                        
 ON POL_HOME_RATING_INFO.HYDRANT_DIST = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                              
WHERE                                                                                               
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                    
 AND DWELLING_ID = @DWELLINGID                                                                                    
                      
SELECT                                                                          
 @DOGFACTOR = ISNULL(ANIMALS_EXO_PETS_HISTORY,'0') ,          --- ANIMALS_EXO_PETS_HISTORY field stores 0 and 1                                                                                         
 @NOPETS    = ISNULL(NO_OF_PETS,0)--,                                                                                    
 --@BREEDOFDOG = ISNULL(MNT.LOOKUP_VALUE_DESC,'')                                                                                 
FROM                                                  
 POL_HOME_OWNER_GEN_INFO WITH (NOLOCK) --INNER JOIN MNT_LOOKUP_VALUES  MNT WITH (NOLOCK) ON    POL_HOME_OWNER_GEN_INFO.OTHER_DESCRIPTION=MNT.LOOKUP_UNIQUE_ID                                                            
 WHERE                                                                                   
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                                               
                                                                                
IF (@NOPETS != 0)                                                            
 BEGIN                                                               
  SET @DOGFACTOR = 'Y'                                                                                                
 END                                         
ELSE                                                                                        
 BEGIN                                                                                      
  SET @DOGFACTOR = 'N'                                   
 END         
      
DECLARE @THREEYEARLESSDATE DATETIME      
DECLARE @THREEYEARDAYS INT      
SET @THREEYEARDAYS=0      
SET @THREEYEARLESSDATE = DATEADD(YEAR,-3,@QUOTEEFFDATE)      
SET @THREEYEARDAYS = DATEDIFF(DAY,@THREEYEARLESSDATE,@QUOTEEFFDATE)                                                                                   
------  PRIOR LOSS INFO                                                                                        
IF  @STATE_ID=14          
BEGIN                                                                              
IF EXISTS(SELECT * FROM APP_PRIOR_LOSS_INFO APLI WITH (NOLOCK) INNER JOIN pol_customer_policy_list AL WITH (NOLOCK) ON APLI.CUSTOMER_ID=AL.CUSTOMER_ID WHERE  APLI.CUSTOMER_ID= @CUSTOMERID          
AND AL.POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID           
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
IF EXISTS(SELECT * FROM APP_PRIOR_LOSS_INFO APLI WITH (NOLOCK) INNER JOIN pol_customer_policy_list AL WITH (NOLOCK) ON APLI.CUSTOMER_ID=AL.CUSTOMER_ID WHERE  APLI.CUSTOMER_ID= @CUSTOMERID          
AND AL.POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID           
AND APLI.LOB=@LOB_ID and (DATEDIFF(DAY,APLI.OCCURENCE_DATE,AL.APP_EFFECTIVE_DATE)<=@THREEYEARDAYS))                                                       
BEGIN                                                                    
 SET  @PRIOR_LOSS_SURCHARGE='Y'                                                                                          
END                                                                                          
ELSE                                                                                          
BEGIN                                                                                          
 SET  @PRIOR_LOSS_SURCHARGE='N'                                                                         
END            
END                          
                  
---------------------------------------------------- BASIC POLICY PAGE END----------------------------------------------------------------------------------------------                                                                                      
  
               
------------------------------------------------------------------------------------------------                                                                         
--- COVERAGE A - DWELLING                                                                                                
--- COVERAGE B - OTHER STRUCTURES                                  
--- COVERAGE C - UNSCGEDULED PERSONAL PROPERTY INCLUDED                                                                                          
--- COVERAGE D - LOSS OF USE  INCLUDED                                      
--- COVERAGE F - MEDICAL PAYMENTS TO OTHERS                                                                                                
----- Coverage A - Dwelling                                                        
----FOR POLICY TYPE HO4(11405 AND 11196 ) AND HO6(11195 AND 11406 ) THE MAIN COVERAGE IS C:                                                  
---7  EBUSPP Coverage C - Unscheduled Personal Property 22                       
--136 EBUSPP Coverage C - Unscheduled Personal Property 14                 
                       
IF @STATE_ID=14                                              
BEGIN                                                       
  IF(@POLICYTYPE = 11195 OR @POLICYTYPE = 11196)                                                       
    BEGIN               
   SELECT                         
    @DWELLING_LIMITS =  ISNULL(LIMIT_1,'0.00')                   -- CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                               
   FROM                                                       
    POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
   WHERE                                                                                                 
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 136                                    
 AND DWELLING_ID = @DWELLINGID                                       
  END                                                  
ELSE                           
 BEGIN                                                  
   SELECT                       
    @DWELLING_LIMITS =  ISNULL(LIMIT_1,'0.00')                   -- CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                            
   FROM                                                                                                 
POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                    
   WHERE                                                         
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 134                                                                                       
    AND DWELLING_ID = @DWELLINGID                                                  
 END                                                  
END                                                                            
                                           
IF @STATE_ID = 22                                                                                         
BEGIN                                                      
 IF(@POLICYTYPE = 11406 OR @POLICYTYPE = 11405)                                                       
 BEGIN                                                      
  SELECT                                                                                    
   @DWELLING_LIMITS = ISNULL(LIMIT_1,'0.00')    ---  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                                
  FROM                                                                         
   POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                   
  WHERE                                                                                                 
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 7                                                                         
 AND DWELLING_ID = @DWELLINGID                                       
 END                                                  
 ELSE                      
 BEGIN                                                  
  SELECT                                                                                            
   @DWELLING_LIMITS = ISNULL(LIMIT_1,'0.00')    ---  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                        
  FROM                                                                         
   POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                 
  WHERE                   
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 3                                              
   AND DWELLING_ID = @DWELLINGID                                                      
 END           
                                                                                        
END                        
                                                     
SET @DWELLING_LIMITS= convert(DECIMAL(18,2),@DWELLING_LIMITS,0)                                                                                             
                        
                                              
IF(@DWELLING_LIMITS IS NOT NULL)                                                                                                
 BEGIN                                                                                           
  SET @COVERAGEVALUE = (@DWELLING_LIMITS / 1000)        
                                                                           
 END                                                                          
ELSE                                                                      
 BEGIN                                                    
  SET @COVERAGEVALUE = 0                                          
 END                              
                                    
                                                                  
---COVERAGE B - OTHER STRUCTURES             
                                                                                          
                                                                                     
IF @STATE_ID=14                                                                                          
BEGIN                                                                                           
 SELECT                                                                     
  @OTHERSTRUCTURES_LIMIT  =  CAST(ISNULL(LIMIT_1,'0.00') as DECIMAL(18,2)),                                                                                          
  --@HO48ADDITIONAL         =  ISNULL(DEDUCTIBLE_1 ,'0.00')    ,                                          
 @REPAIRCOSTADDITIONAL       = ISNULL(DEDUCTIBLE_1,'0.00')                                                 
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                
 WHERE                                                                                                  
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 152                                    
  AND DWELLING_ID = @DWELLINGID                                                         
END                                                                                          
                                                                      
                                                                                         
IF @STATE_ID=22                                                                                          
BEGIN                                                                                           
 SELECT                                                           
  @OTHERSTRUCTURES_LIMIT = CAST(ISNULL(LIMIT_1,'0.00') as DECIMAL(18,2)) ,                                                                                                
  --@HO48ADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')  ,                                          
@REPAIRCOSTADDITIONAL       = ISNULL(DEDUCTIBLE_1,'0.00')                                                                     
 FROM                                                               
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                              
 WHERE                        
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 69                                                                                               
AND DWELLING_ID = @DWELLINGID                                      
                                                                                             
END                                                               
                                    
                              
IF @OTHERSTRUCTURES_LIMIT is null                              
 set @OTHERSTRUCTURES_LIMIT=0                                                                
------------                                        
                                        
-- Specific Structures Away from premesis                                        
IF @STATE_ID=14                                                                     
BEGIN                                                           
 SELECT       
  @SPECIFICSTRUCTURESADDITIONAL       = ISNULL(DEDUCTIBLE_1,'0.00')                                               
 FROM                                                            
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                           
 WHERE                    
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 172                                          
 AND DWELLING_ID = @DWELLINGID                                     
END                                                                                                          
                     
                                                                                                          
IF @STATE_ID=22                                                                                                          
BEGIN                                                                               
 SELECT                                                                              
   @SPECIFICSTRUCTURESADDITIONAL       = ISNULL(DEDUCTIBLE_1,'0.00')                                                                          
 FROM                                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                                
 WHERE                                                                                              
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 257                                                                                                  
AND DWELLING_ID = @DWELLINGID                                     
                                                                                                
END                           
                                        
                                  
IF @SPECIFICSTRUCTURESADDITIONAL is null                              
 set @SPECIFICSTRUCTURESADDITIONAL =0                              
                                                                                     
---COVERAGE C - UNSCGEDULED PERSONAL PROPERTY                               
                                                                                             
IF @STATE_ID = 14                  
BEGIN                
 IF(@POLICYTYPE = 11196)                                
 BEGIN               
   SELECT                                   
    @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                           
    @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                              
   FROM                                                              
    POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                  
   WHERE                            
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 134                                    
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
    POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                 
  WHERE           
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
   AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 164                                    
   AND DWELLING_ID = @DWELLINGID                                                         
 END          
 --END                                                                                        
 ELSE                       
 BEGIN                
  SELECT                           
    @PERSONALPROPERTY_LIMIT = ISNULL(LIMIT_1,'0.00'),                                                       
    @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                              
   FROM                                          
    POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                             
   WHERE                                                                                                 
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 136                                    
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
    POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)             
   WHERE                                 
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 3                                         
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
    POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                 
  WHERE                                                                                                 
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
   AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 91                                    
   AND DWELLING_ID = @DWELLINGID                                                         
 END          
 --END           
 ELSE           
 BEGIN                
    SELECT                                                                                        
    @PERSONALPROPERTY_LIMIT     = ISNULL(LIMIT_1,'0.00'),                       
    @PERSONALPROPERTYINCREASEDLIMITADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                                                                
   FROM                            
    POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                              
   WHERE                                             
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 7             
   AND DWELLING_ID = @DWELLINGID                                   
 END                  
END                                                                  
                                                           
DECLARE @DLIMIT DECIMAL                                                           
SET @DLIMIT=CONVERT(DECIMAL(18),@PERSONALPROPERTY_LIMIT,0)                                                 
--                                                                                                        
SET @PERSONALPROPERTYINCREASEDLIMITINCLUDE  =  @PERSONALPROPERTY_LIMIT        
                                                                                      
                                                                                           
                                                                        
-----  Coverage D - Loss of USE                                                                                          
                                                                                         
IF @STATE_ID = 14                  
BEGIN                                                            
 SELECT                                                                                                  
  @LOSSOFUSE_LIMIT = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                                                                          
  @ADDITIONALLIVINGEXPENSEADDITIONAL =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                                         
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                      
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 137                                                                                              
   AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                                                                          
IF @STATE_ID = 22                                         
BEGIN                                                                                           
 SELECT                                                                
  @LOSSOFUSE_LIMIT =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                                                                          
  @ADDITIONALLIVINGEXPENSEADDITIONAL = CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                
 FROM               
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                
 WHERE                                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 8                                    
 AND DWELLING_ID = @DWELLINGID                     
END                                                                                                   
                                                                      
                                                                                             
                                                                                               
                                                               
          
IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES           
 WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
 AND COVERAGE_CODE_ID in(170, 10) AND DWELLING_ID = @DWELLINGID                                                  
        AND  LIMIT_ID IN('1293', '1294'))          
   BEGIN          
      SET @PERSONALLIABILITY_LIMIT= 'EFH'             
   END          
ELSE          
   BEGIN          
  SELECT @PERSONALLIABILITY_LIMIT=CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)),                                                          
   @PERSONALLIABILITY_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'0.00')        
  FROM POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)           
  WHERE  CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                  
      AND  COVERAGE_CODE_ID IN(170, 10) AND DWELLING_ID = @DWELLINGID             
   END          
        
--MEDICAL PAYMENT should hold 'EFH' if 'Extended From Home' otherwise should hold Limit_1 Value - Asfa Praveen 03/July/2007          
--COVERAGE F - MEDICAL PAYMENT EACH PERSON        
          
IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES           
 WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
 AND COVERAGE_CODE_ID in(171, 13) AND DWELLING_ID = @DWELLINGID                                                
        AND  LIMIT_ID IN('1410', '1411'))          
   BEGIN          
    SET @MEDICALPAYMENTSTOOTHERS_LIMIT= 'EFH'             
   END          
ELSE          
   BEGIN          
  SELECT @MEDICALPAYMENTSTOOTHERS_LIMIT=CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)),           
  @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'0.00')          
 FROM POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)           
  WHERE  CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                              
         AND  COVERAGE_CODE_ID IN(171, 13) AND DWELLING_ID = @DWELLINGID             
   END          
-------------------------------------------------------------------------------------                           
                                          
                                       
 ---------------------------------------DEDUCTIBLE APD---------------------------------                                                
---844 NULL APD All Peril Deductible 14                                                          
---845 NULL APD All Peril Deductible 22                                      
                                                       
IF @STATE_ID = 14                                          
BEGIN                  
 SELECT                                                                                                           
  @DEDUCTIBLE= ISNULL(DEDUCTIBLE,0)                                                                                                    
 FROM                                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                                 
 WHERE                                                                                                                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 844                                                                                            
AND DWELLING_ID = @DWELLINGID                                     
END                      
                                                                         
IF @STATE_ID = 22                                              
BEGIN                                                                                                           
 SELECT                      
   @DEDUCTIBLE= ISNULL(DEDUCTIBLE,0)                                    
 FROM                                                                                             
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                    
 WHERE                                                                                                              
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 845                                                      
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                                             
-------------------------------------------------------------------------------------                                                    
----------------------------------------------------------------------                                                    
                                               
                                                                                            
                                                              
IF @STATE_ID=14                                                                                          
BEGIN                                                                                                
SELECT                                                                                              
 @HO40INCLUDE =   CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                       
 @HO40ADDITIONAL= ISNULL(DEDUCTIBLE_1,'0.00')                                                                                            
FROM                                       
 POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                           
WHERE                                      
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 148                                         
AND DWELLING_ID = @DWELLINGID                                     
END                                                                
                                                                                               
IF @STATE_ID=22                 
BEGIN                                                                                                
SELECT                                                               
 @HO40INCLUDE =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2) ),                              
 @HO40ADDITIONAL= ISNULL(DEDUCTIBLE_1,'0.00')                                                            
FROM                                                                                        
 POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
WHERE                                                                                                 
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 62                                                                      
AND DWELLING_ID = @DWELLINGID                                
END                           
                                                                                       
IF @HO40INCLUDE IS NULL                                          BEGIN                                                                                           
SET @HO40INCLUDE =0.00                                                        
END                                                
                   IF @HO40ADDITIONAL IS NULL                                               
BEGIN                                                                                           
SET @HO40ADDITIONAL =0.00                                                                             
END                          
                                                 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------      
                                                                                     
                                                                                                
IF @STATE_ID=14                                                                                      
BEGIN                                        
 SET @HO21 = 'N'   --HO-21 NOT APPLICABLE FOR INDIANA ON ANY PRODUCT                                        
END                                        
                                                           
                                                                                  
IF @STATE_ID=22                                                                                                
BEGIN                                                                                  
                  
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                                     
POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 53 AND DWELLING_ID = @DWELLINGID)                               
                                               
 BEGIN                                                                                                
  SET @HO21 = 'Y'                       
 END                                       
ELSE                                                                                                
 BEGIN                                                                                 
  SET @HO21 = 'N'                                             
 END                 
                                                                 
END                                  
                                                                               
                                                  
                           
IF @STATE_ID=22                                                                                                
                                                          
BEGIN                                                  
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                      
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID =55 AND DWELLING_ID = @DWELLINGID )                                                                               
                                     
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
                                                                                         
                                   
                                                                                          
                                                                                          
       
IF @STATE_ID=22                                                                                             
                                             
BEGIN                                                            
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
 WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                                    
 POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID =196 AND DWELLING_ID = @DWELLINGID )                                                                           
 BEGIN                                                                                                
 IF (@PRODUCTNAME='HO-3' OR @PRODUCTNAME='HO-5') AND @PRODUCT_PREMIER='Premier'   ---- this condition is implemented temporarly                                                                                         
   SET @HO25 = 'Y'                     
 ELSE                                          
   SET @HO25= 'N'                  
 END                                                                                                
ELSE                                                                                                
 BEGIN                                                                                                
  SET @HO25= 'N'                                                                       
 END                                                                                              
END                                 
------------------END  Premier V.I.P. (HO-25)                                           
                                   
                                        
------------------------EBP22 Preferred Plus V.I.P.(HO-22) 14 142-----------------------                                        
IF @STATE_ID=14                                                                                         
                                                                                          
BEGIN                          
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                      
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID =142 AND DWELLING_ID = @DWELLINGID )                                    
 BEGIN                                                   
   SET @HO22='Y'                                        
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
                                        
-------------END---Preferred Plus V.I.P.(HO-22)   -------------------------------                                        
-----------EBP24 Premier V.I.P.(HO-24) 14 143----------------------                                         
IF @STATE_ID=14                                                
BEGIN                                                                                                               
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                   
 WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID =143 AND DWELLING_ID = @DWELLINGID )                                                                    
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
                                          
                                        
                                            
----------------------------------------------------------------------                                                  
                             
                                                                                                
---Replacement Cost Personal Property (HO-34)                     
                                                                         
IF @STATE_ID=14                                                                                          
BEGIN                                     
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                    
  WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 140 AND DWELLING_ID = @DWELLINGID )                                                                             
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                                     
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 33 AND DWELLING_ID = @DWELLINGID )                                                                
                                    
 BEGIN                                                                                 
  SET @HO34 = 'Y'                       
 END                                                                                                
ELSE                                                                                                
 BEGIN                                     
  SET @HO34 = 'N'                                                                                                
 END                                                                                        
END                                                                                          
                                                                                   
         
------  Preferred Plus Coverage (HO-20)                 
                                                                                          
                                                                                
IF @STATE_ID=14                                                                                          
BEGIN                                                                                          
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)        
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                    
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 141 AND DWELLING_ID = @DWELLINGID )                                
                                          
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 37 AND DWELLING_ID = @DWELLINGID )                                                                          
 BEGIN                                                                  
  SET @HO20 = 'Y'                                                                                 
 END                                                                       
ELSE        
 BEGIN                                                        
  SET @HO20 = 'N'                                         
 END                                                                                                
END                         
                                             
----- Waterbed Liability - HO-4 or HO-6 (HO-200)                                            
                                                                         
                                       
IF @STATE_ID=14                                                        
BEGIN           
      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 812 AND DWELLING_ID = @DWELLINGID )                                                                              
 BEGIN                                                                                                
  SET @HO200 = 'Y'                                               
 END                                                                                      
ELSE                                                                                                
 BEGIN       SET @HO200 = 'N'                                                                                
 END                                                                                                
END                                   
                                                                                          
      
IF @STATE_ID=22                                                                                          
BEGIN                                                                                            
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                    
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 813 AND DWELLING_ID = @DWELLINGID )                                    
                                    
 BEGIN                                                                                                
  SET @HO200 = 'Y'                                                     
 END                                                                            
ELSE                                 
 BEGIN         
  SET @HO200 = 'N'                                                         
 END                                                                                                
END                        
                                                                                               
                                               
                                                                                          
                         
---Ordinance or Law Coverage Forms (HO-277)                                                                                                
                                                             
IF @STATE_ID=14                                                                            
BEGIN                   
                                                                                                
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 156 AND DWELLING_ID = @DWELLINGID )                                                                           
                          
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                    
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 79 AND DWELLING_ID = @DWELLINGID )                                                                
 BEGIN                                                      
  SET @HO277 = 'Y'                                                                                                
 END                                                                           
ELSE                                                                                                
 BEGIN                                                                                                
  SET @HO277 = 'N'                                                                               
 END                                                                                                
END                                                  
                
                                                                              
---Identity Fraud Expense Coverage (HO-455)                                                                                                
IF @STATE_ID=14                                           
BEGIN                                                          
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 158 AND DWELLING_ID = @DWELLINGID )                                                                
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                                     
CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 84 AND DWELLING_ID = @DWELLINGID )             
 BEGIN                                                                    
  SET @HO455 = 'Y'                          
 END                                                          
ELSE                                    
 BEGIN                                        
  SET @HO455 = 'N'                                                                      
 END                                                                        
END                                                    
                                                                 
                                        
----- Water Backup and Sump Pump Overflow (HO-327)                                    
--- This is to be modified                                                                                                                                                                                 
IF @STATE_ID=14                                   
BEGIN                                                                                   
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                                     
POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 197 AND DWELLING_ID = @DWELLINGID )                                                                              
                                      
                                        
                                          
                                            
 BEGIN                                                                                 
  SELECT @HO327 = ISNULL(CONVERT(VARCHAR(10),ADS.DEDUCTIBLE_1),0)  FROM                    
    POL_DWELLING_SECTION_COVERAGES ADS WITH (NOLOCK)                                                          
    WHERE ADS.Customer_ID=@CUSTOMERID and ADS.POLICY_ID=@POLICYID and ADS.POLICY_Version_ID=@POLICYVERSIONID AND ADS.COVERAGE_CODE_ID=197                                    
    AND ADS.DWELLING_ID = @DWELLINGID                                                                     
                                                                                                 
 END                                      
ELSE                                              
 BEGIN                                                          
  SET @HO327 = '0'                                          
 END                                                                         
END                                                                                            
                                           
                                                                           
IF @STATE_ID=22                                         
BEGIN                                                     
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID         
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 198 AND DWELLING_ID = @DWELLINGID )                                                     
                                       
 BEGIN                                                           
SELECT @HO327 = ISNULL(CONVERT(VARCHAR(10),ADS.DEDUCTIBLE_1),0)  FROM                               
    POL_DWELLING_SECTION_COVERAGES ADS WITH (NOLOCK)                                                                   
    WHERE ADS.Customer_ID=@CUSTOMERID and ADS.POLICY_ID=@POLICYID and ADS.POLICY_Version_ID=@POLICYVERSIONID AND ADS.COVERAGE_CODE_ID=198                                     
 AND ADS.DWELLING_ID = @DWELLINGID                                                                    
                                                                                         
 END                 
ELSE                                                                                                  
 BEGIN                                                   
  SET @HO327 = 'N'        END                                      
END                          
                                 
IF @HO327 IS NULL                  
BEGIN                                            
SET  @HO327=''                                            
END        
                                                                                    
                                                                                                
---Condo - Unit Owners Rental to Others (HO-33)                 
                                                                                           
IF @STATE_ID=14                
BEGIN                                                                                               
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 161 AND DWELLING_ID = @DWELLINGID )                                                    
                                           
 BEGIN                                                                                                
  SELECT  @HO33= LIMIT1_AMOUNT_TEXT FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 161 AND DWELLING_ID = @DWELLINGID                                                                            
                                                                                      
 END                        
ELSE                                                                                   
 BEGIN                                                                                                
  SET @HO33 = 'N'                                                                                                
 END                  
END                                        
                                                        
                                                                                          
IF @STATE_ID=22                        
BEGIN                                                                 
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                         
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                 
 AND COVERAGE_CODE_ID = 88 AND DWELLING_ID = @DWELLINGID )                                                                               
                                                  
 BEGIN                                            
SELECT  @HO33= LIMIT1_AMOUNT_TEXT FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                    
 WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
AND COVERAGE_CODE_ID = 88 AND DWELLING_ID = @DWELLINGID                                                         
                                      
 END                                                                                         
ELSE                                                                                                
 BEGIN                                                                                               
  SET @HO33 = 'N'                                          
 END                                                                              
END                                                                                          
           
                                                                                
---Earthquake (HO-315)                                                                                                
                                                   
IF @STATE_ID=14          
BEGIN                        
    IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND (COVERAGE_CODE_ID = 157 or COVERAGE_CODE_ID = 906 or COVERAGE_CODE_ID = 904)AND DWELLING_ID = @DWELLINGID )                                                    
                                       
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND (COVERAGE_CODE_ID = 80 or COVERAGE_CODE_ID = 907 or COVERAGE_CODE_ID = 905) AND DWELLING_ID = @DWELLINGID )                                                                               
                                       
 BEGIN                 
  SET @HO315 = 'Y'                                                                                                
 END                                                       
ELSE                                                                                                
 BEGIN                                                      
  SET @HO315 = 'N'                       
 END                                                                                         
END            
                                                                                          
                                                     
                                                          
                                                                  
---Collapse From Sub-Surface Water (HO-9)                                                                                            
--- POLlicable only in INDIANA state                                             
                                                                     
IF @STATE_ID=22                                                                      
  SET @HO9 = 'N'                                       
                                                                                           
                                                     
IF @STATE_ID=14                                     
BEGIN                                            
                      
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                
AND COVERAGE_CODE_ID = 814 AND DWELLING_ID = @DWELLINGID )         
 BEGIN                                                                                                
  SET @HO9 = 'Y'                                                                                                
 END                                     
ELSE                                                                                                
 BEGIN                                                                                                
  SET @HO9 = 'N'                                                            
 END        
END                           
                                       
                                                       
                                                                                          
------ Mine Subsidence Coverage (HO-287)                                                                                          
                                            
                                                                  
IF @STATE_ID=14                                                                                          
BEGIN                         
                                                          
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 169 AND DWELLING_ID = @DWELLINGID )                                            
                                    
 BEGIN                                         
  SET @HO287 = 'Y'                                                                                                
 END                   
ELSE                                                                                           
 BEGIN                              
  SET @HO287 = 'N'                                                         
 END         
SELECT  @MINESUBSIDENCEADDITIONAL =DEDUCTIBLE_1  FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 169 AND DWELLING_ID = @DWELLINGID                                                                                               
END                                                                                          
                                                                                          
IF @STATE_ID=22                               
BEGIN                                                                                            
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                   
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                                     
POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 112 AND DWELLING_ID = @DWELLINGID )                                     
                                        
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                                     
POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 92 AND DWELLING_ID = @DWELLINGID )                                                                              
                                    
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND                    
POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 165 AND DWELLING_ID = @DWELLINGID )                                                                     
                                        
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
                
                
                
                
-----------------HO-64 Condominium Deluxe Coverage (HO-66)-------------                   
--93 EBRDC Condominium Deluxe Coverage (HO-66) 22                             
--166 EBRDC Condominium Deluxe Coverage (HO-66) 14                             
IF @STATE_ID=22                                                                                   
BEGIN                           
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                             
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                           
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
IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                             
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID               
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
                                      
-------------------------------                                                                                      
                                                                              
------ Increased Fire Dept. Service Charge (HO-96)                                                                                                
                                  
IF @STATE_ID=14                          
BEGIN                                                        
 SELECT                                                                                          
  @HO96INCLUDE =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                                                                            
  @HO96ADDITIONAL =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                                        
            
 FROM                                                   
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                        
 WHERE                                                                      
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND                                     
COVERAGE_CODE_ID = 145 AND DWELLING_ID = @DWELLINGID                                                         
END                                                                                           
      
                                                                                             
IF @STATE_ID=22                      
BEGIN                                                                                                  
 SELECT                                                                                            
  @HO96INCLUDE =  CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)) ,                                   
  @HO96ADDITIONAL =  CAST(ISNULL(DEDUCTIBLE_1,'0.00') AS DECIMAL(18,2))                      
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                      
 WHERE                                                        
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 57                                    
AND DWELLING_ID = @DWELLINGID                                                                                                 
END                                                                         
                                                                     
                                                                                          
IF @HO96INCLUDE IS NULL                                                       
BEGIN                                                                                          
 SET @HO96INCLUDE=0.00                   
END                                                    
                                                                                 
IF @HO96ADDITIONAL IS   NULL           
BEGIN                                                      
 SET @HO96ADDITIONAL=0.00                                                                                          
END                                                                                    
                                                                                       
IF @HO96INCLUDE IS NOT NULL                                                                                          
 BEGIN                                                                                          
  SET @HO96FINALVALUE =  CAST(CAST(@HO96INCLUDE AS decimal) + CAST(@HO96ADDITIONAL  AS decimal) AS decimal(18,2))                                                       
                
 END                                                             
ELSE                                                       
 BEGIN                                                  
  SET @HO96FINALVALUE = 0.00                                                                                          
 END                                                                         
                                                                                           
                                                                                          
                                                           
                                                 
                                                                 
----- Loss Assessment Coverage (HO-35)                                                            
                                                                   
IF @STATE_ID=14                                                                                          
BEGIN                                                                          
 SELECT                                                         
  @HO35INCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                          
  @HO35ADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                 
                                                             
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                             
 WHERE                                                                              
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 162                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
---                                   
IF @STATE_ID=22                                           
BEGIN                                                                                           
 SELECT                                                        
  @HO35INCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                     
  @HO35ADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')                                     
                                                              
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                
 WHERE                                                                  
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 89               
AND DWELLING_ID = @DWELLINGID                              
END                                                                        
                                                                                          
                                                                                                
                                                            
IF @HO35INCLUDE IS  NULL                          
BEGIN                                                   
 SET @HO35INCLUDE=0.00                                                            
END                                                                                          
IF @HO35ADDITIONAL IS   NULL            
BEGIN                                                                
 SET @HO35ADDITIONAL=0.00                                                                              
END                                          
           
IF @POLICYTYPE <> 11196 and @POLICYTYPE <> 11246 and @POLICYTYPE <> 11406 and @POLICYTYPE <> 11408                                           
 SET @HO35INCLUDE = '0'                                                                     
                                                                                       
                                                                                 
---Coverage C Increased Special Limits (HO-65 or HO-211)- Money                                                                                                
                                                                                                
IF @STATE_ID=14                                
BEGIN                                            
 SELECT                                                     
  @MONEYINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                            
  @MONEYADDITIONAL =  ISNULL(DEDUCTIBLE_1,'0.00')                                                                                          
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                   
 WHERE                                              
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 188                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                                                                        
----                                                                        
IF @STATE_ID=22                    
BEGIN                                                                       
 SELECT                                                                                           
  @MONEYINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                          
  @MONEYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                    
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                                                         
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 189                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                   
                            
                   
IF @MONEYINCLUDE IS  NULL                                                      BEGIN                                                                     
 SET @MONEYINCLUDE=0.00                                                                                          
END                                                                                          
IF @MONEYADDITIONAL IS  NULL                                                                                          
BEGIN                    
SET @MONEYADDITIONAL=0.00                           
END                                                           
                                                             
                                                                                                
--Coverage C Increased Special Limits (HO-65 or HO-211)- Securities                                           
                                                       
                                               
IF @STATE_ID=14                                                              
BEGIN                                                                                          
 SELECT                                                                  
  @SECURITIESINCLUDE =ISNULL(LIMIT_1,'0.00'),                                                  
  @SECURITIESADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                             
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                    
 WHERE                                                                                                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 190                                                                                               
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                                                                             
                                                     
                                                
IF @STATE_ID=22                                   BEGIN                                                                     
 SELECT                                                                                            
  @SECURITIESINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                   
  @SECURITIESADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                               
 FROM          
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                  
 WHERE                                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 191                                                                
AND DWELLING_ID = @DWELLINGID                       
END                                                                                             
                                                                                          
                                                                                          
                                                                                          
IF @SECURITIESINCLUDE IS  NULL                            
BEGIN                                                                                          
 SET @SECURITIESINCLUDE=0.00           
END                                                                                          
IF @SECURITIESADDITIONAL IS  NULL                   
BEGIN                                                                 
 SET @SECURITIESADDITIONAL=0.00                                                                                          
END                                  
                                                        
                                                                                          
            
                               
---Coverage C Increased Special Limits (HO-65 or HO-211)- Silverware                                                                                                
IF @STATE_ID=14                                        
BEGIN                                                                                                
 SELECT                         
  @SILVERWAREINCLUDE =ISNULL(LIMIT_1,'0.00'),                                                                                               
  @SILVERWAREADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                               
 FROM                         
  POL_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                            
 WHERE                                                                                
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 192                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                  
                                                                                            
IF @STATE_ID=22                                                                                             
BEGIN                                                                                                
 SELECT                                                                              
  @SILVERWAREINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                   
  @SILVERWAREADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')         
 FROM                                                                                 
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                        
 WHERE                          
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 193                                                                           
AND DWELLING_ID = @DWELLINGID                                     
END                                      
                                                           
                                                                  
                                                                                          
                                                                                          
IF @SILVERWAREINCLUDE IS  NULL                                                                                          
BEGIN                   
 SET @SILVERWAREINCLUDE=0.00                                                               
END                                                              
IF @SILVERWAREADDITIONAL IS  NULL              
BEGIN                                          
 SET @SILVERWAREADDITIONAL=0.00                                   
END                                                                        
IF @POLICYTYPE <> 11196 and @POLICYTYPE <> 11246 and @POLICYTYPE <> 11406 and @POLICYTYPE <> 11408                                                                      
 SET @HO35INCLUDE = '0'                                                                                            
                               
---   Coverage C Increased Special Limits (HO-65 or HO-211)- Firearms                                
                                          
IF @STATE_ID = 14                                     
BEGIN                                    
SELECT                                                                                            
  @FIREARMSINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                           
  @FIREARMSADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                          
 FROM          
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                               
 WHERE                        
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 194                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
       
IF @STATE_ID = 22       
BEGIN                                                                                 
 SELECT                                                                               
  @FIREARMSINCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                                
  @FIREARMSADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                          
 FROM                                                                         
  POL_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                             
 WHERE                                                                           
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 195                                   
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                
                                                                                          
                                                                              
                                                                                        
IF @FIREARMSINCLUDE IS  NULL                                                                          
BEGIN                                                                                          
 SET @FIREARMSINCLUDE=0.00                                                                                          
END                                                  
IF @FIREARMSADDITIONAL IS  NULL                  
BEGIN                                                                                    
 SET @FIREARMSADDITIONAL=0.00                                                   
END                                                                                          
                                                                                               
                                                                                              
---Coverage C Increased Special Limits (HO-65 or HO-211)- Unscheduled Jewelry & Furs                                                                                                
                   
IF @STATE_ID = 14                                                                                           
BEGIN                   SELECT                                                                                            
  @UNSCHEDULEDJEWELRYINCLUDE = ISNULL(LIMIT_1,'0.00'),                         
  @UNSCHEDULEDJEWELRYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                     
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                              
 WHERE                                             
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 154                                                                                                
AND DWELLING_ID = @DWELLINGID                            
END                                                    
                                     
IF @STATE_ID = 22   
BEGIN                                             
 SELECT                              
  @UNSCHEDULEDJEWELRYINCLUDE = ISNULL(LIMIT_1,'0.00'),                             
  @UNSCHEDULEDJEWELRYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')         
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                              
 WHERE                                                                             
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 74                                                                                                
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                    
                                                  
IF @UNSCHEDULEDJEWELRYINCLUDE IS  NULL                                                                                          
BEGIN                         
 SET @UNSCHEDULEDJEWELRYINCLUDE=0.00                                                                                          
END                                                                                          
IF @UNSCHEDULEDJEWELRYADDITIONAL IS  NULL                                                                              
BEGIN                                                               
 SET @UNSCHEDULEDJEWELRYADDITIONAL=0.00                           
END                                                                                          
             
         
                                                                                                
--- Personal Property Coverage C Increased Limits Away from Premises (HO-50)                                                                           
                                            
IF @STATE_ID = 14                                                                         
BEGIN                   
 SELECT                               
  @PERSONALPROPERTYAWAYINCLUDE = ISNULL(LIMIT_1,'0.00'),                                          
  @PERSONALPROPERTYAWAYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                           
 FROM                                                             
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                              
 WHERE                             
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 150                                                                           
AND DWELLING_ID = @DWELLINGID                         
END                                                                                          
                                                                                          
IF @STATE_ID = 22                                                                         
BEGIN                                    
 SELECT                                                                                            
  @PERSONALPROPERTYAWAYINCLUDE = ISNULL(LIMIT_1,'0.00') ,                         
  @PERSONALPROPERTYAWAYADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                        
 FROM                                       
  POL_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                        
 WHERE                                                                                  
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 66                                                            
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                                    
                                                                                                
                                              
                                                                                          
IF @PERSONALPROPERTYAWAYINCLUDE IS  NULL                                                          
BEGIN                                                                                          
 SET @PERSONALPROPERTYAWAYINCLUDE=0.00                                                               
END                                                                                          
IF @PERSONALPROPERTYAWAYADDITIONAL IS  NULL                                    
BEGIN                   
 SET @PERSONALPROPERTYAWAYADDITIONAL=0.00                                    
END                                                                                          
                                               
                                                                                          
                                                                                          
---Business Property Increased Limits (HO-312)                                                                                                
IF @STATE_ID = 14                                                                                         
BEGIN                                                                                                
 SELECT            
  @HO312INCLUDE = ISNULL(LIMIT_1,'0.00')                
 FROM             
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                             
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 146                                                     
AND DWELLING_ID = @DWELLINGID                                     
 SELECT                                                                                            
  @HO312ADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                             
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                                                      
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 941                                                     
AND DWELLING_ID = @DWELLINGID                                     
                
END         
                        
                                                                                          
IF @STATE_ID = 22                           
BEGIN                                     
 SELECT                                                                                            
  @HO312INCLUDE =ISNULL(LIMIT_1,'0.00')                                              
  FROM                                       
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                  
 WHERE                                                                                                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 58                                                                                                
AND DWELLING_ID = @DWELLINGID                 
                
 SELECT                                                                                            
  @HO312ADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                       
 FROM                                       
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                    
 WHERE                                                                                                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 942                                                                                         
AND DWELLING_ID = @DWELLINGID                                        
END                                            
                    
IF @HO312INCLUDE IS NULL                                             
BEGIN                                                                                           
 SET @HO312INCLUDE=0.00                                              
END                                                        
IF @HO312ADDITIONAL IS NULL                                                                                          
BEGIN                                       
 SET @HO312ADDITIONAL=0.00                                      
END                                                                                          
                                                                    
                                                                                
IF EXISTS ( SELECT  COVERAGE_CODE_ID FROM                                                             
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                              
 WHERE                    
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID           
AND COVERAGE_CODE_ID IN (831,832) AND DWELLING_ID = @DWELLINGID )                                                      
                                    
SELECT    @REDUCTION_IN_COVERAGE_C = LIMIT_1 FROM                                                       
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK) WHERE                                                                                                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID IN (831,832)                                    
AND DWELLING_ID = @DWELLINGID                                                             
ELSE                                                                                
  SET @REDUCTION_IN_COVERAGE_C='N'                                                                                
                
                     
               
---     Credit Card and Depositors Forgery (HO-53)                                                                                          
                                                                                          
IF @STATE_ID = 14                                                          
BEGIN                              
 SELECT                                    
  @HO53INCLUDE = ISNULL(LIMIT_1,'0.00'),                                                                                                
  @HO53ADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                                                                                           
 FROM                                                                                     
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)     WHERE                                       
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 153                                    
AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                                                                          
IF @STATE_ID = 22                                                
BEGIN                              
 SELECT                                                                                            
  @HO53INCLUDE =ISNULL(LIMIT_1,'0.00'),                          
  @HO53ADDITIONAL = ISNULL(DEDUCTIBLE_1,'0.00')                     
 FROM                                                                                     
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                
 WHERE                                                  
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 73                                                                                             
AND DWELLING_ID = @DWELLINGID                                     
END                                                          
                                                         
                                                                                       
                                                                                               
                                                         
IF @HO53INCLUDE IS NULL        
BEGIN                                                                            
 SET @HO53INCLUDE=0.00                                                                     
END                
IF @HO53ADDITIONAL IS NULL                                                                        
BEGIN                             
 SET @HO53ADDITIONAL=0.00                                                                                          
END                                                                             
                                                                                          
                                                                                          
                          
---Expanded Replacement (HO-11)                                                                     
                                                                                          
                                                                                          
IF @STATE_ID = 14                                                                                           
BEGIN                                                            
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                    
 WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 144 AND DWELLING_ID = @DWELLINGID )                                                                            
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK) WHERE                                     
CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                    
 AND COVERAGE_CODE_ID = 56 AND DWELLING_ID = @DWELLINGID )                                                                            
 BEGIN                             
   SET @HO11 = 'Y'                            
  END                  
 ELSE                                                                                                
  BEGIN                                
   SET @HO11 = 'N'                                                                  
  END                                                                                                
END                                                                                          
                                                                                                          
                                                                                          
----- Unit Owners Coverage A Special Coverage (HO-32)                                                                                          
                                                                                    
                    
IF @STATE_ID = 14                                                         
BEGIN                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                     
 WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                 
AND COVERAGE_CODE_ID = 160 AND DWELLING_ID = @DWELLINGID )                                                                            
                                      
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)  WHERE                                     
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 87 AND DWELLING_ID = @DWELLINGID )                                                                    
  BEGIN                                                          
   SET @HO32 = 'Y'                           
  END                                                                 
 ELSE                                                                                                
  BEGIN                                                                                                
   SET @HO32 = 'N'                                                                             
  END                   
END                                                                                          
                                                                                          
  /*                                      
               
---Increased Credit Card (HO-53)                                                             
IF @STATE_ID = 14                                                 
BEGIN                                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLID AND POL_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID = 153)                                   
  BEGIN                                      
   SET @HO53 = 'Y'                                                            
  END                                                                              
 ELSE                                                                                                
  BEGIN         
   SET @HO53 = 'N'                               
  END                                                                                                
END                                                                                          
           
IF @STATE_ID = 22                          
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLID AND POL_VERSION_ID = @POLVERSIONID AND COVERAGE_CODE_ID = 73)      
  BEGIN                                                
   SET @HO53 = 'Y'                                                                                                
  END                                                                                                
 ELSE                                                                        
  BEGIN                  SET @HO53 = 'N'                                                           
  END                                                                       
END                                                                     
  */                                                                                        
                                                
             
                       
---Dwelling Under Construction (HO-14) ,  COV_ID=159, COV_CODE=EBDUC                                                                                          
                                                    
                                                                                          
IF @STATE_ID = 14                                                                                           
BEGIN                                              
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                          
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                     
AND COVERAGE_CODE_ID = 159 AND DWELLING_ID = @DWELLINGID )                                                                             
                                                   
  BEGIN                                                                                                
SET @DWELLING= 'Y'                                                       
  END                                                             
 ELSE                                                                              
  BEGIN                          
  SET @DWELLING = 'N'                                                                                          
  END                                           
END                                                                                       
                                                        
IF @STATE_ID = 22                       
BEGIN                                                                      
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                     
WHERE  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                       
AND COVERAGE_CODE_ID = 86 AND DWELLING_ID = @DWELLINGID )                                                    
                             
  BEGIN                                                                                                
  SET @DWELLING= 'Y'                                                                                                
  END                                                                                               
 ELSE           
  BEGIN                                              
  SET @DWELLING = 'N'                                                                                   
  END                                                                                 
END                              
             
-- HO-493                                                 
      
 IF EXISTS (SELECT 1 FROM POL_DWELLING_SECTION_COVERAGES PDSC WITH (NOLOCK) INNER JOIN MNT_COVERAGE MC      
    ON PDSC.COVERAGE_CODE_ID = MC.COV_ID      
      WHERE   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                       
    AND DWELLING_ID = @DWELLINGID AND COV_CODE = 'HO493' AND DWELLING_ID = @DWELLINGID)      
    BEGIN      
     SET @HO493='Y'      
    END      
                                          
------------------------------------------END OF SECTION - I----------------------------------------------------------------------------------------------------------------                                                                                  
  
    
      
      
      
      
      
      
      
       
       
                                                                
--------------------------------------- START OF INLAND MARINE  ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
    
      
      
      
      
      
      
      
      
      
      
                                                   
---BICYCLES                   
                       
IF @STATE_ID=14                                                       
BEGIN                                                        
 /*SELECT                                                                                                 
  @SCH_BICYCLE_AMOUNT  =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                
  @SCH_BICYCLE_DED     =  ISNULL(DEDUCTIBLE ,'0.00')                                                               
 FROM                                             
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                                                              
 WHERE                                           
  CATEGORY = 222 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 846 AND IS_ACTIVE='Y')      
               
  BEGIN                                                
   SELECT @SCH_BICYCLE_AMOUNT  =                                        
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
 WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 846                       
        AND IS_ACTIVE='Y'          
                    
             
                                       
   SELECT @SCH_BICYCLE_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND          
 ITEM_ID = 846 AND IS_ACTIVE='Y')                                                
  END                               
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_BICYCLE_AMOUNT  =  0.0                                                
   SELECT @SCH_BICYCLE_DED     =  0.0                                         
END                 
END                                      
IF @STATE_ID=22                                                                         
BEGIN                                                                                
 /*SELECT                                                    
  @SCH_BICYCLE_AMOUNT  =  ISNULL(AMOUNT_OF_INSURANCE,'0') ,         
  @SCH_BICYCLE_DED     =  ISNULL(DEDUCTIBLE ,'0')                                                 
 FROM                                                                         
  POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)                                                                                                
 WHERE                                                                                                 
  CATEGORY = 223 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                     
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 874 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_BICYCLE_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 874                                          
    AND IS_ACTIVE='Y'      
        
   SELECT @SCH_BICYCLE_DED = LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND          
 ITEM_ID = 874 AND IS_ACTIVE='Y')                                              
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_BICYCLE_AMOUNT  =  0.0                                                
   SELECT @SCH_BICYCLE_DED     =  0.0                                                
END                                                
END                                                                                        
IF @SCH_BICYCLE_AMOUNT is NULL                                                              
 BEGIN                                                                          
  SET @SCH_BICYCLE_AMOUNT=0.00                                             
 END                                                                               
          
IF @SCH_BICYCLE_DED is NULL                                                                                          
 BEGIN                                                                                          
  SET @SCH_BICYCLE_DED=0.00                           
 END                            
---CAMERAS                                           
IF @STATE_ID=14                                                                  
BEGIN                                                                                                   
 /*SELECT       
 @SCH_CAMERA_AMOUNT  =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                                     
  @SCH_CAMERA_DED     =  ISNULL(DEDUCTIBLE,'0.00')                   
 FROM                                                        
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                                 
 WHERE                                                                                        
  ITEM_ID = 847  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 847 AND IS_ACTIVE='Y')      
  BEGIN                                               
   SELECT @SCH_CAMERA_AMOUNT  =                       
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID and IS_ACTIVE='Y' AND ITEM_ID = 847     SELECT @SCH_CAMERA_DED =  
  
    
      
      
      
      
       
      
       
LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE      
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND  ITEM_ID = 847 AND IS_ACTIVE='Y')                                                
  END             
 ELSE                                                
  BEGIN               
   SELECT @SCH_CAMERA_AMOUNT  =  0.0                                  
   SELECT @SCH_CAMERA_DED     =  0.0                                        
END                                                
                                                
END                                                                                        
                                                            
                                                    
IF @STATE_ID=22                                                                                        
BEGIN                                                                                                   
 /*SELECT         
@SCH_CAMERA_AMOUNT  =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                               
  @SCH_CAMERA_DED     =  ISNULL(DEDUCTIBLE,'0.00')                   
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                                                  
 WHERE                                                                                                 
  ITEM_ID = 847+28  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 875 AND IS_ACTIVE='Y')   BEGIN                                  
  
    
      
      
      
      
      
      
      
              
   SELECT @SCH_CAMERA_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND IS_ACTIVE='Y' AND ITEM_ID = 875      
   SELECT @SCH_CAMERA_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 875 AND IS_ACTIVE='Y')                                              
  END                                                
 ELSE                                      
  BEGIN                                                
   SELECT @SCH_CAMERA_AMOUNT  =  0.0                           
   SELECT @SCH_CAMERA_DED     =  0.0                                                
END                                    
END       
IF @SCH_CAMERA_AMOUNT is NULL                                                                                          
BEGIN                 
  SET @SCH_CAMERA_AMOUNT=0.00    
END                                                            
                         
IF @SCH_CAMERA_DED is NULL                                                                                          
BEGIN                               
 SET  @SCH_CAMERA_DED=0.00                                                                                            
END                                                                                           
--- Cellular Phones (HO-900)    NOT FOUND IN THE LIST                                                                  
                                                        
IF @STATE_ID=14                                                       
BEGIN                                                 
 /*SELECT                                                                       
  @SCH_CELL_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                                                               
  @SCH_CELL_DED    =  ISNULL(DEDUCTIBLE,'0.00')                    
                                                                                               
 FROM                                                                                              
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                                                  
 WHERE                                   
  ITEM_ID = 848 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 848 AND IS_ACTIVE='Y')                       
  BEGIN                       
   SELECT @SCH_CELL_AMOUNT  =                                   
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 848                              
   AND IS_ACTIVE='Y'                               
          
SELECT @SCH_CELL_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                             
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 848 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                            
   SELECT @SCH_CELL_AMOUNT  =  0.0                                                
   SELECT @SCH_CELL_DED     =  0.0                                                
END                  
END                    
                      
IF @STATE_ID=22                                                                                        
BEGIN                                                                                
 /*SELECT                                                                                                 
  @SCH_CELL_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                       
  @SCH_CELL_DED    =  ISNULL(DEDUCTIBLE,'0.00')                                             
                       
 FROM                         POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                      
 WHERE                                      
 ITEM_ID = 876 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                        
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 876 AND IS_ACTIVE='Y')                                  
  BEGIN                                                
   SELECT @SCH_CELL_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 876                                                       
   AND IS_ACTIVE='Y'      
      
   SELECT @SCH_CELL_DED =  LIMIT_DEDUC_AMOUNT FROM                                         
  MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND       
 POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 876 AND IS_ACTIVE='Y')              
                             
  END        
ELSE                                                
  BEGIN                                                
   SELECT @SCH_CELL_AMOUNT  =  0.0                                                
 SELECT @SCH_CELL_DED   =  0.0                                                
END                                                  
END                                                                                        
IF @SCH_CELL_AMOUNT is NULL                        
 BEGIN                                                                                          
  SET @SCH_CELL_AMOUNT=0.00                             
 END                                            
                                                                          
IF @SCH_CELL_DED is NULL                                                                                          
 BEGIN                                                                
  SET @SCH_CELL_DED=0.00                                                                                          
 END                                                    
--- FUR                
IF @STATE_ID=14                                                                                        
BEGIN                                                                                                             
 /*SELECT                                                                                                 
  @SCH_FURS_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                                                                                               
  @SCH_FURS_DED    =  ISNULL(DEDUCTIBLE,'0.00')                                                                                                
                                                                           
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                            
 WHERE                                                                                                 
  ITEM_ID = 851  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                    
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 851 AND IS_ACTIVE='Y')      
  BEGIN               
   SELECT @SCH_FURS_AMOUNT  =                       
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 851                                                     AND IS_ACTIVE='Y'       
         
   SELECT @SCH_FURS_DED =  LIMIT_DEDUC_AMOUNT                                    
  FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 851 AND
   
   
      
      
      
      
      
      
      
 IS_ACTIVE='Y')                        
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_FURS_AMOUNT  =  0.0                                                
   SELECT @SCH_FURS_DED     =  0.0                                                
END                                                   
END                                
                                                                                        
IF @STATE_ID=22         
BEGIN                              
  /*SELECT                                                        
 @SCH_FURS_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00') ,                         
  @SCH_FURS_DED    =  ISNULL(DEDUCTIBLE,'0.00')                                                                                              
                                                           
 FROM                                  
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                                       
 WHERE                                                                         
  ITEM_ID = 879  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 879 AND IS_ACTIVE='Y')   BEGIN                                  
  
    
      
      
       
       
      
      
       
               
   SELECT @SCH_FURS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 879                                                        
 AND IS_ACTIVE='Y'                             
   SELECT @SCH_FURS_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 879 AND IS_ACTIVE='Y')                          
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
                                                                                   
                                                                                              
--- Guns                                                           
                                                                
IF @STATE_ID=14              
BEGIN                                                                                   
 /*SELECT                                 
  @SCH_GUNS_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                   
  @SCH_GUNS_DED    =  ISNULL(DEDUCTIBLE,'0.00')       
                         
 FROM                               POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                                                    
 WHERE           
  ITEM_ID = 853 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 853 AND IS_ACTIVE='Y')                                          
 
    
      
       
      
      
       
       
      
       
  BEGIN                                                
   SELECT @SCH_GUNS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 853                                                        
   AND IS_ACTIVE='Y'      
        
SELECT @SCH_GUNS_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 853 AND IS_ACTIVE='Y')                          
  END                                                
  ELSE                               
  BEGIN                                                
   SELECT @SCH_GUNS_AMOUNT  =  0.0                                                
   SELECT @SCH_GUNS_DED     =  0.0                                                
  END                                              
END                                                                         
                                     
IF @STATE_ID=22                                        
BEGIN                                                       
/*SELECT                                                                                                 
  @SCH_GUNS_AMOUNT =  ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                                               
  @SCH_GUNS_DED    =  ISNULL(DEDUCTIBLE,'0.00')                                                                                                
                                                          
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                     
 WHERE                
  ITEM_ID = 881 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/            
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 881 AND IS_ACTIVE='Y')   BEGIN                                  
  
    
      
      
      
      
      
       
       
              
   SELECT @SCH_GUNS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 881                                                        
   AND IS_ACTIVE='Y'      
        
   SELECT @SCH_GUNS_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 881 AND IS_ACTIVE='Y')                         
  END                                            
  ELSE                                                
  BEGIN               
   SELECT @SCH_GUNS_AMOUNT  =  0.0                               
   SELECT @SCH_GUNS_DED     =  0.0                                     
  END                                                  
END                         
IF @SCH_GUNS_AMOUNT is NULL                                             
BEGIN                                                                                          
 SET @SCH_GUNS_AMOUNT=0.00                                                                                          
END                                                                                          
                                                                                          
IF @SCH_GUNS_DED is NULL                                                                                          
BEGIN             
 SET @SCH_GUNS_DED=0.00                                                                                     
END                                                                      
                                                   
--- GOLF                                                                           
                                                                                   
                                                                                        
IF @STATE_ID=14                                                   
BEGIN                                                                                        
 /*SELECT                                                                 
  @SCH_GOLF_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                     
  @SCH_GOLF_DED    = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
              
 FROM                                                                               
POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                                                              
 WHERE                                                                                                 
  ITEM_ID = 852  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 852 AND IS_ACTIVE='Y')      
  BEGIN                                   
   SELECT @SCH_GOLF_AMOUNT  =              
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 852                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_GOLF_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND       
 POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 852 AND IS_ACTIVE='Y')                          
  END                                  
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_GOLF_AMOUNT  =  0.0                
   SELECT @SCH_GOLF_DED     =  0.0                                                
  END                            
END                                                                        
                                                                                                
IF @STATE_ID=22                                                      
BEGIN                     
 /*SELECT                                
  @SCH_GOLF_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                               
  @SCH_GOLF_DED    = ISNULL(DEDUCTIBLE,'0.00')         
                                                                                         
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                       
 WHERE                                                                                                 
  ITEM_ID = 880  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 880 AND IS_ACTIVE='Y')   BEGIN                                  
 
     
      
      
       
      
       
      
      
              
   SELECT @SCH_GOLF_AMOUNT  =                 
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 880                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_GOLF_DED =  LIMIT_DEDUC_AMOUNT                                         
 FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 880 AND IS_ACTIVE='Y')                          
  END                                                
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_GOLF_AMOUNT  =  0.0                                                
   SELECT @SCH_GOLF_DED     =  0.0                     
END                                                  
END                                                
                                                                                                
                                                                                        
IF @SCH_GOLF_AMOUNT is NULL                             
BEGIN                                                                                          
 SET @SCH_GOLF_AMOUNT=0.00                                                                   
END                                                                    
                                                             
IF @SCH_GOLF_DED is NULL                                                                   
BEGIN                                                                                          
 SET @SCH_GOLF_DED=0.00                                                            
END        
                                                                                          
                                                                                          
---JWELERY                                             
IF @STATE_ID=14                  
BEGIN                                                 
 /*SELECT                                                                 
  @SCH_JWELERY_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),            
  @SCH_JWELERY_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
 FROM                                         
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)            
 WHERE                         
  ITEM_ID = 857  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 857 AND IS_ACTIVE='Y')      
  BEGIN                        
   SELECT @SCH_JWELERY_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 857                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_JWELERY_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND  ITEM_ID = 857 AND IS_ACTIVE='Y')                                                
  END                                                
  ELSE                                                
  BEGIN        
   SELECT @SCH_JWELERY_AMOUNT  =  0.0                                                
   SELECT @SCH_JWELERY_DED     =  0.0                                                
  END                                                  
END                                                                                
                                                                                        
IF @STATE_ID=22                                            
BEGIN                                                                                       
 /*SELECT                                                                                                 
  @SCH_JWELERY_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                                     
  @SCH_JWELERY_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
 FROM                                        
  POL_HOME_OWNER_SCH_ITEMS_CVGS    WITH (NOLOCK)                                                                                             
 WHERE               
  ITEM_ID = 885  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 885 AND IS_ACTIVE='Y')   BEGIN                                 
   
   
      
       
      
       
      
      
      
               
   SELECT @SCH_JWELERY_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 885                                             
   AND IS_ACTIVE='Y'      
   SELECT @SCH_JWELERY_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND       
 POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 885 AND IS_ACTIVE='Y')              
  END                                                
  ELSE                                                
  BEGIN                                      
   SELECT @SCH_JWELERY_AMOUNT  =  0.0                                                
   SELECT @SCH_JWELERY_DED     =  0.0                 
  END                                                                         
END                                                      
                                                                                               
                                                                                        
IF @SCH_JWELERY_AMOUNT is NULL                                                  
BEGIN                                             
 SET @SCH_JWELERY_AMOUNT=0                            
END                                                                                          
               
IF @SCH_JWELERY_DED is NULL                                                                           
BEGIN                                                                                          
 SET @SCH_JWELERY_DED=0                                                   
END                                                                                            
                                                        
--- MUSICAL                                                                                                       
IF @STATE_ID=14                                         
BEGIN                                                             
                                                                  
/*SELECT                                                                                                 
  @SCH_MUSICAL_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0'),                             
  @SCH_MUSICAL_DED     = ISNULL(DEDUCTIBLE,'0')                        
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS    WITH (NOLOCK)                                                        
 WHERE                                                                                                 
  ITEM_ID = 859  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 859 AND IS_ACTIVE='Y')                                        
  BEGIN                                                
   SELECT @SCH_MUSICAL_AMOUNT  =                                                  
 SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
  WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 859                                                        
  AND IS_ACTIVE='Y'      
   SELECT @SCH_MUSICAL_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND          
    ITEM_ID = 859 AND IS_ACTIVE='Y')                                                
  END                                                
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_MUSICAL_AMOUNT  =  0.0                
   SELECT @SCH_MUSICAL_DED    =  0.0                                 
  END                                                                                                     
END                                                                                        
                                       
IF @STATE_ID=22         
BEGIN                                                            
 /*SELECT                                                                        
  @SCH_MUSICAL_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0'),                                                                                              
  @SCH_MUSICAL_DED     = ISNULL(DEDUCTIBLE,'0')                       
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                            
 WHERE                                            
  ITEM_ID = 887  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 887 AND IS_ACTIVE='Y')      
  BEGIN                   
   SELECT @SCH_MUSICAL_AMOUNT  =                       
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 887                                                     
   AND IS_ACTIVE='Y'      
   SELECT @SCH_MUSICAL_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)       
 WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 887 AND IS_ACTIVE='Y')                                                
  END                                                
  ELSE                                                
  BEGIN              
   SELECT @SCH_MUSICAL_AMOUNT  =  0.0                     
   SELECT @SCH_MUSICAL_DED     =  0.0                                                
  END                                                                               
END                                                                                        
                                                       
IF @SCH_MUSICAL_AMOUNT is NULL                                   
BEGIN                                                  
 SET @SCH_MUSICAL_AMOUNT = 0                                                                                          
END                                                                                          
                                                    
IF @SCH_MUSICAL_DED is NULL       
BEGIN                                                                                          
 SET @SCH_MUSICAL_DED=0                                                                                  
END                                                                                            
                         
--- PERSONAL COMPUTER Desktop                                                                                               
IF @STATE_ID=14                           
BEGIN         
 /*SELECT                                                                                
  @SCH_PERSCOMP_AMOUNT    =  ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                                               
  @SCH_PERSCOMP_DED  =  ISNULL(DEDUCTIBLE,'0.00')                   
 FROM                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                            
 WHERE                                                                                                 
  ITEM_ID = 860  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 860 AND IS_ACTIVE='Y')                          
  BEGIN                                                
   SELECT @SCH_PERSCOMP_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 860                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_PERSCOMP_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 860 AND IS_ACTIVE='Y')                          
  END                                
  ELSE              
 BEGIN                                                
   SELECT @SCH_PERSCOMP_AMOUNT  =  0.0                                                
   SELECT @SCH_PERSCOMP_DED     =  0.0                                                
  END                                                       
                                                                                                
END                             
                            
IF @STATE_ID=22                                                                                 
BEGIN                                                                       
 /*SELECT                                                                                                 
  @SCH_PERSCOMP_AMOUNT    =  ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                      
  @SCH_PERSCOMP_DED  =  ISNULL(DEDUCTIBLE,'0.00')                                                                                 
 FROM                    
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                                              
 WHERE                                                                                                 
  ITEM_ID = 888  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                           
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 888 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_PERSCOMP_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 888                                                        
   AND IS_ACTIVE='Y'      
SELECT @SCH_PERSCOMP_DED = LIMIT_DEDUC_AMOUNT                                         
 FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 888 AND IS_ACTIVE='Y')                          
  END                                                
  ELSE                                                
  BEGIN                                  
   SELECT @SCH_PERSCOMP_AMOUNT  =  0.0                                                
   SELECT @SCH_PERSCOMP_DED     =  0.0                                                
  END                                               
END                                                                                     
IF @SCH_PERSCOMP_AMOUNT is NULL                                                                   
BEGIN                                                                   
 SET @SCH_PERSCOMP_AMOUNT = 0.00                           
END                                                                                
          
IF @SCH_PERSCOMP_DED is NULL                                                                                          
BEGIN                                                                                  
 SET @SCH_PERSCOMP_DED = 0.00                                              
END                                                                                            
                                                                                              
---SILVER                               
                                            
IF @STATE_ID=14                                                                                        
BEGIN                                                        
 /*SELECT                                              
  @SCH_SILVER_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                 
  @SCH_SILVER_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
                                                                                  
 FROM                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                                 
 WHERE                                                                                                 
  ITEM_ID = 865 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 865 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_SILVER_AMOUNT  =                                
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 865                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_SILVER_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 865 AND IS_ACTIVE='Y')                                        
  END                                                
  ELSE                                            
  BEGIN                                                
   SELECT @SCH_SILVER_AMOUNT  =  0.0                                                
SELECT @SCH_SILVER_DED     =  0.0                             
  END                                                                                                 
END                                                                                        
                                                    
IF @STATE_ID=22                                                                       
BEGIN                                         
 /*SELECT                                       
  @SCH_SILVER_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                     
  @SCH_SILVER_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
                                                                                         
 FROM                                                                                                 
POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)                                           
 WHERE            
  ITEM_ID = 893 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 893 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_SILVER_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 893                                                        
   AND IS_ACTIVE='Y'      
        
   SELECT @SCH_SILVER_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND       
 POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 893 AND IS_ACTIVE='Y')                       
  END                                                
  ELSE                                   
  BEGIN                                                
   SELECT @SCH_SILVER_AMOUNT  =  0.0                                                
   SELECT @SCH_SILVER_DED     =  0.0                                
  END                                                     
END                                                                        
IF @SCH_SILVER_AMOUNT is NULL                                                                                          
 BEGIN                                                                                     
  SET @SCH_SILVER_AMOUNT = 0.00                                                                                          
 END                                                                                          
                                                              
IF @SCH_SILVER_DED is NULL                                           
 BEGIN                                                                                          
  SET @SCH_SILVER_DED = 0.00                         
 END                                                                                            
                                                                                          
                                       
---STAMPS                                                                                                
IF @STATE_ID=14                                       
BEGIN             
/*SELECT                                            
  @SCH_STAMPS_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                             
  @SCH_STAMPS_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                          
 FROM             
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                                   
 WHERE                                                                                                 
  ITEM_ID = 867  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                             
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 867 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_STAMPS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 867                                                      
   AND IS_ACTIVE='Y'      
        
   SELECT @SCH_STAMPS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 867 AND IS_ACTIVE='Y')                                                
  END                                                
  ELSE                                             
  BEGIN                              
   SELECT @SCH_STAMPS_AMOUNT  =  0.0                                                
   SELECT @SCH_STAMPS_DED     =  0.0                                                
  END                                                      
                                                                                               
END                                                                                        
                                                                                        
                                                                             
IF @STATE_ID=22                                                                                        
BEGIN                                                    
 /*SELECT                                                                           
  @SCH_STAMPS_AMOUNT  = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),        
  @SCH_STAMPS_DED     = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
 FROM                                                   
  POL_HOME_OWNER_SCH_ITEMS_CVGS   WITH (NOLOCK)                                     
 WHERE                                                                                           
  ITEM_ID = 895  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 895 AND IS_ACTIVE='Y')      
  BEGIN                                                 
   SELECT @SCH_STAMPS_AMOUNT  =                                       
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 895                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_STAMPS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND  ITEM_ID = 895 AND IS_ACTIVE='Y')                              
  END                     
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_STAMPS_AMOUNT  =  0.0                                                
   SELECT @SCH_STAMPS_DED     =  0.0                                             
  END                                                 
END                                                                                        
IF @SCH_STAMPS_AMOUNT is NULL                                                   
BEGIN                                                            
 SET @SCH_STAMPS_AMOUNT = 0.00                            
END                                                                                          
                  
IF @SCH_STAMPS_DED is NULL                     
BEGIN                                                                                          
 SET @SCH_STAMPS_DED = 0.00                                                                                          
END                                                                                            
                             
         
                                                                              
---RARE COINS                                                                                                                
                                                                                              
IF @STATE_ID=14                                                                                 
BEGIN                 
 /*SELECT                                          
  @SCH_RARECOINS_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                             
  @SCH_RARECOINS_DED = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
 FROM                                                                                         
 POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                         
 WHERE                                                                               ITEM_ID = 862  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                    
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 862 AND IS_ACTIVE='Y')                                         
  
    
      
      
      
      
      
      
      
       
  BEGIN                                  
   SELECT @SCH_RARECOINS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 862                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_RARECOINS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE      
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID  AND ITEM_ID = 862 AND IS_ACTIVE='Y')                               
  END                                                
  ELSE                                     
  BEGIN                                            
   SELECT @SCH_RARECOINS_AMOUNT  =  0.0                                                
   SELECT @SCH_RARECOINS_DED     =  0.0                                  
  END                                                          
                         
END                                           
                                                                                            
                                                                 
IF @STATE_ID = 22                                                                                        
BEGIN                                                                                       
 /*SELECT                                          
  @SCH_RARECOINS_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                                      
  @SCH_RARECOINS_DED = ISNULL(DEDUCTIBLE,'0.00')                                                                                                
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                        
 WHERE                                        
 ITEM_ID = 890  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 890 AND IS_ACTIVE='Y')                                          
  
    
      
      
       
       
      
       
       
       
                
  BEGIN                                 
   SELECT @SCH_RARECOINS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)      
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 890                                                        
   AND IS_ACTIVE='Y'      
      
        
                                                 
   SELECT @SCH_RARECOINS_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID       
AND ITEM_ID = 890 AND IS_ACTIVE='Y')                          
  END                                                
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_RARECOINS_AMOUNT  =  0.0                        
   SELECT @SCH_RARECOINS_DED     =  0.0                                                
  END                               
END                                                                                        
                                                                                
                                                                                        
                                                                                          
IF @SCH_RARECOINS_AMOUNT is NULL                                                                                   
BEGIN          
 SET @SCH_RARECOINS_AMOUNT = 0.00                                                                          
END                                                                        
                                                                                   
IF @SCH_RARECOINS_DED is NULL           
BEGIN                                  
 SET @SCH_RARECOINS_DED = 0.00                                                                               
END                                                                                                                       
                                         
               
----FINEARTS WITHOUT BREAK                                                                   
                                                                                        
IF @STATE_ID=14                                                                                        
BEGIN                                                             
 /*SELECT                     
  @SCH_FINEARTS_WO_BREAK_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0.00'),                                                                                               
  @SCH_FINEARTS_WO_BREAK_DED    = ISNULL(DEDUCTIBLE,'0.00')                                                                                         
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK)                                                 
 WHERE                          
  ITEM_ID = 850  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS  WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 850 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
 WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 850                                                        
 AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_FINEARTS_WO_BREAK_DED =  LIMIT_DEDUC_AMOUNT                                         
   FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID        
 IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID      
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
/* SELECT                                                
  @SCH_FINEARTS_WO_BREAK_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0'),                                                                                               
  @SCH_FINEARTS_WO_BREAK_DED    = ISNULL(DEDUCTIBLE,'0')                                          
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)                 
 WHERE                                                                                                 
 ITEM_ID = 878  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID*/                                                
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 878 AND IS_ACTIVE='Y')                                          
 
  BEGIN                          
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 878                                                       
   AND IS_ACTIVE='Y'      
       
        
   SELECT @SCH_FINEARTS_WO_BREAK_DED =  LIMIT_DEDUC_AMOUNT                                         
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 878 AND IS_ACTIVE='Y')                          
  END              
  ELSE                                                
  BEGIN              
   SELECT @SCH_FINEARTS_WO_BREAK_AMOUNT  =  0.0                                                
   SELECT @SCH_FINEARTS_WO_BREAK_DED     =  0.0                                                
  END                                                                 
END                  
                                                                                               
                                                      
IF @SCH_FINEARTS_WO_BREAK_AMOUNT is NULL                                 
BEGIN                                                                                          
 SET @SCH_FINEARTS_WO_BREAK_AMOUNT = 0                                                                              
END                                      
                                                                                        
IF @SCH_FINEARTS_WO_BREAK_DED is NULL                       
BEGIN                            
 SET @SCH_FINEARTS_WO_BREAK_DED = 0                                                                       
END                                                                                            
                                                                                          
                                                                                          
           
--- FINEARTS WITH BREAK                                                                                                 
                                                                                          
IF @STATE_ID=14       
BEGIN                                                                                                             
 /*SELECT                                                                          
  @SCH_FINEARTS_BREAK_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0'),                                                                                              
  @SCH_FINEARTS_BREAK_DED    = ISNULL(DEDUCTIBLE,'0')                                                                                                
 FROM                                               
  POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)                                                                                                
 WHERE           
  ITEM_ID = 849  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 849 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =                    
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 849          
   AND IS_ACTIVE='Y'      
      
   SELECT @SCH_FINEARTS_BREAK_DED =  LIMIT_DEDUC_AMOUNT                                        
FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 849 AND IS_ACTIVE='Y')                          
  END                 
  ELSE                                                
  BEGIN                                                
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =  0.0                                                
   SELECT @SCH_FINEARTS_BREAK_DED     =  0.0                                                
  END                                                                                                
END                                                  
                                                                                        
IF @STATE_ID=22                                                                                  
BEGIN                                                                                                             
/* SELECT                                        
  @SCH_FINEARTS_BREAK_AMOUNT = ISNULL(AMOUNT_OF_INSURANCE,'0'),                                                                                              
  @SCH_FINEARTS_BREAK_DED    = ISNULL(DEDUCTIBLE,'0')                                                           
 FROM                                                                                                 
  POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)                    
 WHERE          
  ITEM_ID = 877  AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID */                                                 
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 877 AND IS_ACTIVE='Y')      
  BEGIN                              
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 877                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_FINEARTS_BREAK_DED =  LIMIT_DEDUC_AMOUNT                                        
 FROM MNT_COVERAGE_RANGES WITH (NOLOCK)  WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 877 AND
  
    
      
      
      
      
      
      
      
 IS_ACTIVE='Y')      
  END        
  ELSE                                          
  BEGIN                                                
   SELECT @SCH_FINEARTS_BREAK_AMOUNT  =  0.0                                
   SELECT @SCH_FINEARTS_BREAK_DED     =  0.0                                                
  END                   
END                                                                                     
                                                                                        
IF @SCH_FINEARTS_BREAK_AMOUNT is NULL                                                                                          
BEGIN                                                          
 SET @SCH_FINEARTS_BREAK_AMOUNT = 0                                                       
END                                                                                                                                                                  
IF @SCH_FINEARTS_BREAK_DED is NULL                                
BEGIN                                                                                          
 SET @SCH_FINEARTS_BREAK_DED = 0                                                                           
END                                                                                            
----- new inland marine                                        
                
--- HANDICAP ELECTRONICS                            
IF @STATE_ID=14                                                                              
BEGIN                                                                            
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 854 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 854                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_HANDICAP_ELECTRONICS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES   WITH (NOLOCK)                                    
 WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 854 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =  0.0                                                
   SELECT @SCH_HANDICAP_ELECTRONICS_DED     =  0.0                           
  END                                                
                     
END                                        
                                                      
IF @STATE_ID=22                                                                                       
BEGIN                                                                               
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 882 AND IS_ACTIVE='Y')   BEGIN                                  
  
   
       
      
       
       
      
       
       
              
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 882                                                        
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_HANDICAP_ELECTRONICS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES  WITH (NOLOCK)                                     
 WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 882 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_HANDICAP_ELECTRONICS_AMOUNT =  0.0                               
   SELECT @SCH_HANDICAP_ELECTRONICS_DED     =  0.0                                                
  END                                                                                                             
END                                                                                                        
IF @SCH_HANDICAP_ELECTRONICS_AMOUNT is NULL                                                                  
BEGIN                                                     
 SET @SCH_HANDICAP_ELECTRONICS_AMOUNT = 0.00           
END                                                                                                          
                                                                                                        
IF @SCH_HANDICAP_ELECTRONICS_DED is NULL                                
BEGIN                     
 SET @SCH_HANDICAP_ELECTRONICS_DED = 0.00                     
END                       
                                  
--- SCHEDULED HEARING AIDS                                        
IF @STATE_ID=14                                       
BEGIN                                                
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 855 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_HEARING_AIDS_AMOUNT  =                    
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 855                
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_HEARING_AIDS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE                     
 FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 855 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_HEARING_AIDS_AMOUNT  =  0.0                                      
   SELECT @SCH_HEARING_AIDS_DED     =  0.0           
  END                   
      
END                                                                                                        
                                                                                              
IF @STATE_ID=22                                                                                                        
BEGIN                                                                                               
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 883 AND IS_ACTIVE='Y')   BEGIN                                  
  
    
     
      
      
       
       
      
      
               
   SELECT @SCH_HEARING_AIDS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 883                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_HEARING_AIDS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                      
  (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 883 AND IS_ACTIVE='Y')                                                
  END                                 
 ELSE                                                
  BEGIN                                                
 SELECT @SCH_HEARING_AIDS_AMOUNT =  0.0                                                
   SELECT @SCH_HEARING_AIDS_DED   =  0.0       
  END                                                        
END                                                                                                        
                                           
IF @SCH_HEARING_AIDS_AMOUNT is NULL                                                                                                          
BEGIN                                 
 SET @SCH_HEARING_AIDS_AMOUNT = 0.00                                                      
END                                                                                                          
                          
IF @SCH_HEARING_AIDS_DED is NULL       
BEGIN                                                                  
 SET @SCH_HEARING_AIDS_DED = 0.00             
END                                         
                                   
                                        
--- SCHEDULED insulin pumps                                 
IF @STATE_ID=14                                                               
BEGIN                                                    
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 856 AND IS_ACTIVE='Y')      
  BEGIN                                    
  SELECT @SCH_INSULIN_PUMPS_AMOUNT  =                                                  
  SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
  WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 856                              
  AND IS_ACTIVE='Y'                              
   SELECT @SCH_INSULIN_PUMPS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES  WITH (NOLOCK)                                     
 WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 856 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_INSULIN_PUMPS_AMOUNT  =  0.0             
   SELECT @SCH_INSULIN_PUMPS_DED     =  0.0                              
  END                                                
                                                                                                    
END                                                                                                        
                                                                                                           
IF @STATE_ID=22                                           
BEGIN                                                                          
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 884 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_INSULIN_PUMPS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 884                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_INSULIN_PUMPS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 884 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_INSULIN_PUMPS_AMOUNT =  0.0                                                
   SELECT @SCH_INSULIN_PUMPS_DED     =  0.0                                                
  END                                                                                    
END                       
                                                  
                                                                                                        
IF @SCH_INSULIN_PUMPS_AMOUNT is NULL                                                                                      
BEGIN                      
 SET @SCH_INSULIN_PUMPS_AMOUNT = 0.00                                                       
END                                                                                                          
                                                                    
IF @SCH_INSULIN_PUMPS_DED is NULL                                                                                                          
BEGIN                                                                                                          
 SET @SCH_INSULIN_PUMPS_DED = 0.00                                                           
END                                           
                                        
                      
--- SCHEDULED Mart KAY                                        
IF @STATE_ID=14                                                                                                        
BEGIN                                                                                                        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 858 AND IS_ACTIVE='Y')                              
  BEGIN                                                
   SELECT @SCH_MART_KAY_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 858                       
   AND IS_ACTIVE='Y'                         
   SELECT @SCH_MART_KAY_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 858 AND IS_ACTIVE='Y')                                                
  END                                           
 ELSE                                                
  BEGIN                     
 SELECT @SCH_MART_KAY_AMOUNT  =  0.0                                                
   SELECT @SCH_MART_KAY_DED     =  0.0                                                
  END                                                
                                                                                                               
END                                                                            
                                
IF @STATE_ID=22                                                                                                        
BEGIN                                                                           
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 886 AND IS_ACTIVE='Y')      
  BEGIN                                      
   SELECT @SCH_MART_KAY_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 886                                                       
   AND IS_ACTIVE='Y'                               
   SELECT @SCH_MART_KAY_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN         
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 886 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN           
   SELECT @SCH_MART_KAY_AMOUNT =  0.0                     
   SELECT @SCH_MART_KAY_DED     =  0.0                                                
  END                                                                                                             
END                                                       
                                                  
                                            
IF @SCH_MART_KAY_AMOUNT is NULL                                                                                                          
BEGIN                                                                           
 SET @SCH_MART_KAY_AMOUNT = 0.00                                                               
END                                                 
                                                                                                        
IF @SCH_MART_KAY_DED is NULL                                            
BEGIN                                                                                                          
 SET @SCH_MART_KAY_DED = 0.00           
END                                           
                                        
                     
--- SCHEDULED Personal Laptop                                     
IF @STATE_ID=14                                                                                                        
BEGIN                                         
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 861 AND IS_ACTIVE='Y')      
  BEGIN                       
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 861                                                        
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 861 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =  0.0                                                
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED     =  0.0                                                
  END                                                
                                                  
END                                 
                                                         
IF @STATE_ID=22                                                                                
BEGIN                                                                    
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 889 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 889   AND IS_ACTIVE='Y'      
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK)       
 WHERE LIMIT_DEDUC_ID IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 889 AND IS_ACTIVE='Y')                          
  
    
      
      
      
      
      
      
      
                      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT =  0.0                                                
   SELECT @SCH_PERSONAL_COMPUTERS_LAPTOP_DED     =  0.0                                                
  END                                                   
END                                                                                                        
                                                  
                                                                        
IF @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT  is NULL                                      
BEGIN                                               
 SET @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT = 0.00                                                               
END                                                                                                          
                                                            
IF @SCH_PERSONAL_COMPUTERS_LAPTOP_DED is NULL                                                                                                          
BEGIN                                                                                                          
 SET @SCH_PERSONAL_COMPUTERS_LAPTOP_DED = 0.00                                       
END                                           
                               
                                        
--- SCHEDULED SALESMAN SUPPLY        
IF @STATE_ID=14                                                 
BEGIN                                                                                                        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 863 AND IS_ACTIVE='Y')      
BEGIN                                      
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 863  AND IS_ACTIVE='Y'      
   SELECT @SCH_SALESMAN_SUPPLIES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                     
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 863 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT  =  0.0                  
   SELECT @SCH_SALESMAN_SUPPLIES_DED     =  0.0                                                
  END      
              
END       
                                                                                             
IF @STATE_ID=22                                                                                                        
BEGIN                                                                                               
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 891 AND IS_ACTIVE='Y')      
  BEGIN                              
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 891   AND IS_ACTIVE='Y'      
   SELECT @SCH_SALESMAN_SUPPLIES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 891 AND IS_ACTIVE='Y')                                                
  END                       
 ELSE                                                
  BEGIN                                     
   SELECT @SCH_SALESMAN_SUPPLIES_AMOUNT =  0.0                                                
   SELECT @SCH_SALESMAN_SUPPLIES_DED     =  0.0                                                
  END                                                                                     
END                                                                      
                                            
                                                                                
IF @SCH_SALESMAN_SUPPLIES_AMOUNT  is NULL                                                                                                          
BEGIN             
 SET @SCH_SALESMAN_SUPPLIES_AMOUNT = 0.00                                                               
END                                                                                                          
                                                                                                        
IF @SCH_SALESMAN_SUPPLIES_DED is NULL                                                                                                          
BEGIN                 
 SET @SCH_SALESMAN_SUPPLIES_DED = 0.00                                                                                                          
END       
                                        
--- SCHEDULED scuba diving                                        
IF @STATE_ID=14                   
BEGIN                                                                                                        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 864 AND IS_ACTIVE='Y')      
  BEGIN                                              
   SELECT @SCH_SCUBA_DRIVING_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 864                                                       
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_SCUBA_DRIVING_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
(SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 864 AND IS_ACTIVE='Y')                                                
  END                              
 ELSE                    
  BEGIN                               
   SELECT @SCH_SCUBA_DRIVING_AMOUNT  =  0.0                                                
   SELECT @SCH_SCUBA_DRIVING_DED     =  0.0                                                
  END                          
                                                                                                               
END                                                            
                                                                                 
IF @STATE_ID=22                                                                                                
BEGIN                                                                                               
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 892 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_SCUBA_DRIVING_AMOUNT   =       
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 892                                                        
   AND IS_ACTIVE='Y'        
   SELECT @SCH_SCUBA_DRIVING_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID           
  
   
       
       
      
      
      
       
       
       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 892 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                 
  BEGIN                                                
   SELECT @SCH_SCUBA_DRIVING_AMOUNT =  0.0                                                
   SELECT @SCH_SCUBA_DRIVING_DED     =  0.0                                                
  END                            
END                                                               
                                                
                                      
IF @SCH_SCUBA_DRIVING_AMOUNT  is NULL                                                                                                          
BEGIN                                             
 SET @SCH_SCUBA_DRIVING_AMOUNT = 0.00                                                               
END           
            
IF @SCH_SCUBA_DRIVING_DED is NULL                                                                  
BEGIN                                              
 SET @SCH_SCUBA_DRIVING_DED = 0.00         
END                                  
                                        
                                        
--- SCHEDULED Snow Sky                                        
IF @STATE_ID=14        
BEGIN                                                             
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 866 AND IS_ACTIVE='Y')   BEGIN                                 
 
     
      
       
       
      
       
       
      
               
  SELECT @SCH_SNOW_SKIES_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 866                                                        
   AND IS_ACTIVE='Y'      
      
        
   SELECT @SCH_SNOW_SKIES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK)       
WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID           
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
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 894 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_SNOW_SKIES_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 894                                                        
   AND IS_ACTIVE='Y'      
   SELECT @SCH_SNOW_SKIES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE      
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID           
AND ITEM_ID = 894 AND IS_ACTIVE='Y')                                                
  END                                      
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_SNOW_SKIES_AMOUNT =  0.0                                                
   SELECT @SCH_SNOW_SKIES_DED     =  0.0                              
  END              
END                                                          
                                           
                                               
IF @SCH_SNOW_SKIES_AMOUNT  is NULL                                                                                                          
BEGIN                       
 SET @SCH_SNOW_SKIES_AMOUNT = 0.00                        
END                                                                                                          
                           
IF @SCH_SNOW_SKIES_DED is NULL               
BEGIN                                                                                                          
 SET @SCH_SNOW_SKIES_DED = 0.00                                         
END                                               
                                        
                      
--- SCHEDULED Tack Saddle                                        
IF @STATE_ID=14                                                                                                        
BEGIN                               
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 868 AND IS_ACTIVE='Y')      
  BEGIN                                                
  SELECT @SCH_TACK_SADDLE_AMOUNT  =                                                  
  SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
  WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 868                                                     
  AND IS_ACTIVE='Y'               
   SELECT @SCH_TACK_SADDLE_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID      
   AND ITEM_ID = 868 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                       
  BEGIN                                                
   SELECT @SCH_TACK_SADDLE_AMOUNT  =  0.0                                                
   SELECT @SCH_TACK_SADDLE_DED     =  0.0                                                
  END                                                                                                   
END                          
IF @STATE_ID=22                                                                         
BEGIN                                                    
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 896 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_TACK_SADDLE_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 896                                                        
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_TACK_SADDLE_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE      
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
 AND ITEM_ID = 896 AND IS_ACTIVE='Y')                                                
  END                                        
 ELSE                                                
  BEGIN                   
   SELECT @SCH_TACK_SADDLE_AMOUNT =  0.0                                 
   SELECT @SCH_TACK_SADDLE_DED     =  0.0                                                
  END                                                                                                             
END                                            
IF @SCH_TACK_SADDLE_AMOUNT  is NULL                                                                                                          
BEGIN                                                      
 SET @SCH_TACK_SADDLE_AMOUNT = 0.00                                                               
END                                                                                                      
                                                                                                        
IF @SCH_TACK_SADDLE_DED is NULL                                            
BEGIN                                                         
 SET @SCH_TACK_SADDLE_DED = 0.00                                                                                                
END                                              
                                        
                                        
                                        
--- SCHEDULED TOOLS (PREMESIS)                                        
IF @STATE_ID=14                     
BEGIN                                                                                                        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 869 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =            
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
  WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 869                                                        
  AND IS_ACTIVE='Y'      
   SELECT @SCH_TOOLS_PREMISES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID                                    
 AND POLICY_ID = @POLICYID                                     
AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 869 AND IS_ACTIVE='Y')                                           
  END                                                
 ELSE                                                
  BEGIN                                    
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =  0.0                                                
   SELECT @SCH_TOOLS_PREMISES_DED     =  0.0               
  END                                                   
END                                                                                                        
                                                                                                           
IF @STATE_ID=22                                                    
BEGIN                                                                    
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 897 AND IS_ACTIVE='Y')                         
  BEGIN                                                
   SELECT @SCH_TOOLS_PREMISES_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 897                                                       
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_TOOLS_PREMISES_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID                                   
 
 AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 897 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TOOLS_PREMISES_AMOUNT  =  0.0                                          
   SELECT @SCH_TOOLS_PREMISES_DED     =  0.0                                                
  END                                                                                                             
END                                                                                                        
                                            
           
IF @SCH_TOOLS_PREMISES_AMOUNT  is NULL                                         
BEGIN                                                                  
 SET @SCH_TOOLS_PREMISES_AMOUNT = 0.00                                                               
END                                                                             
                                                                                                        
IF @SCH_TOOLS_PREMISES_DED is NULL                         
BEGIN                                                                         
 SET @SCH_TOOLS_PREMISES_DED = 0.00                                                                                                          
END                     
                                        
--- SCHEDULED TOOLS BUSINESS                                         
IF @STATE_ID=14                                                        
BEGIN                                                                                                        
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 870 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =                 
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 870                                                        
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_TOOLS_BUSINESS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID       
 AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 870 AND IS_ACTIVE='Y')      
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =  0.0                                                
   SELECT @SCH_TOOLS_BUSINESS_DED     =  0.0                                    
  END                                                                                            
END                                       
                        
IF @STATE_ID=22                         
BEGIN                                                                   
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 898 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 898                                                        
   AND IS_ACTIVE='Y'                          
   SELECT @SCH_TOOLS_BUSINESS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
 (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 898 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TOOLS_BUSINESS_AMOUNT  =  0.0                                                
   SELECT @SCH_TOOLS_BUSINESS_DED     =  0.0                      
  END                                                                 
END                                 
IF @SCH_TOOLS_BUSINESS_AMOUNT  is NULL                                                    
BEGIN                                                             
 SET @SCH_TOOLS_BUSINESS_AMOUNT = 0.00                                                               
END                                                                                                      
                                          
IF @SCH_TOOLS_BUSINESS_DED is NULL                                                                           
BEGIN                                                                                                          
 SET @SCH_TOOLS_BUSINESS_DED = 0.00                                                                                                  
END                                         
                                        
--- SCHEDULED tractors                                        
IF @STATE_ID=14                                                                                                        
BEGIN          
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 871 AND IS_ACTIVE='Y')      
  BEGIN                                
   SELECT @SCH_TRACTORS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 871               
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_TRACTORS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
 (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 871 AND IS_ACTIVE='Y')                                                
  END        
                                              
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TRACTORS_AMOUNT  =  0.0                                                
   SELECT @SCH_TRACTORS_DED     =  0.0                                                
  END                                                
                      
END                                  
                                                                    
IF @STATE_ID=22                                                                                                        
BEGIN                                                                                               
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 899 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_TRACTORS_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 899       
   AND IS_ACTIVE='Y'           
  SELECT @SCH_TRACTORS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
 (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 899 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TRACTORS_AMOUNT  =  0.0                                         
   SELECT @SCH_TRACTORS_DED     =  0.0                                                
  END                                     
END                                                                                                        
                                                  
       
IF @SCH_TRACTORS_AMOUNT  is NULL              
BEGIN                                                                               
 SET @SCH_TRACTORS_AMOUNT = 0.00                                          
END                                                                                                          
                                                                
IF @SCH_TRACTORS_DED is NULL                                                                                                        
BEGIN                                                                                                          
 SET @SCH_TRACTORS_DED = 0.00                                                                                                          
END                                        
                     
                                        
--- SCHEDULED train collections                                        
IF @STATE_ID=14                                                                                                        
BEGIN                       
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 872 AND IS_ACTIVE='Y')      
  BEGIN                     
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =        
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 872                                                        
   AND IS_ACTIVE='Y'                             
   SELECT @SCH_TRAIN_COLLECTIONS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                      
  (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 872 AND IS_ACTIVE='Y')                                  
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =  0.0                            
   SELECT @SCH_TRAIN_COLLECTIONS_DED     =  0.0                                                
  END                                                
                                                                                                
END                                       
                                              
IF @STATE_ID=22                                                                                                        
BEGIN                                                                                         
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 900 AND IS_ACTIVE='Y')                       
  BEGIN          SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 900                                                  AND IS_ACTIVE='Y'                                
   SELECT @SCH_TRAIN_COLLECTIONS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN                                       
 (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 900 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_TRAIN_COLLECTIONS_AMOUNT  =  0.0                                                
   SELECT @SCH_TRAIN_COLLECTIONS_DED     =  0.0                                                
  END                                                         
END                   
                
                                                  
IF @SCH_TRAIN_COLLECTIONS_AMOUNT  is NULL                                                                                                          
BEGIN                                                                                                        
 SET @SCH_TRAIN_COLLECTIONS_AMOUNT = 0.00                                                               
END                               
                                                                                                        
IF @SCH_TRAIN_COLLECTIONS_DED is NULL                           
BEGIN                                            
 SET @SCH_TRAIN_COLLECTIONS_DED = 0.00                                                                     
END                         
               
--- SCHEDULED wheel chair                                        
IF @STATE_ID=14                                                                          
BEGIN                              
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 873 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 873                                                        
  AND IS_ACTIVE='Y'                              
   SELECT @SCH_WHEELCHAIRS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID               
AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                      
 AND ITEM_ID = 873 AND IS_ACTIVE='Y')                    
  END                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =  0.0                                                
   SELECT @SCH_WHEELCHAIRS_DED     =  0.0                    
  END                           
                                              
END                                                                                                        
                                                                  
IF @STATE_ID=22                                                                                                        
BEGIN                                                   
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 901 AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_WHEELCHAIRS_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID = 901                     
 AND IS_ACTIVE='Y'      
        
   SELECT @SCH_WHEELCHAIRS_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
   AND ITEM_ID = 901 AND IS_ACTIVE='Y')                                                
  END                                                
 ELSE                                                
  BEGIN                                                
   SELECT @SCH_WHEELCHAIRS_AMOUNT  =  0.0                                                
   SELECT @SCH_WHEELCHAIRS_DED    =  0.0                                                
  END                                                            
END                                                                                                        
                             
                                                                                                        
IF @SCH_WHEELCHAIRS_AMOUNT  is NULL                               
BEGIN                                       
 SET @SCH_WHEELCHAIRS_AMOUNT = 0.00                                                               
END                           
                                                                                                        
IF @SCH_WHEELCHAIRS_DED is NULL                                                                                                          
BEGIN   
 SET @SCH_WHEELCHAIRS_DED = 0.00             
END                                     
  -- CAMERA PROFESSIONAL USE      
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID in (10028,10027) AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_CAMERA_PROF_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID  in (10028,10027)                      
 AND IS_ACTIVE='Y'      
        
   SELECT @SCH_CAMERA_PROF_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
   AND ITEM_ID  in (10028,10027)  AND IS_ACTIVE='Y')                                                
  END       
             
      
 -- MUSICAL RENUMERATION         
IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND ITEM_ID IN (10030,10029) AND IS_ACTIVE='Y')      
  BEGIN                                                
   SELECT @SCH_MUSICAL_REMUN_AMOUNT   =                                                  
   SUM(ISNULL(ITEM_INSURING_VALUE,0)) FROM POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS WITH (NOLOCK)       
   WHERE CUSTOMER_ID = @CUSTOMERID AND POL_ID = @POLICYID AND POL_VERSION_ID = @POLICYVERSIONID AND ITEM_ID  IN (10030,10029)                      
 AND IS_ACTIVE='Y'      
        
   SELECT @SCH_MUSICAL_REMUN_DED =  LIMIT_DEDUC_AMOUNT FROM MNT_COVERAGE_RANGES WITH (NOLOCK) WHERE LIMIT_DEDUC_ID  IN (SELECT DEDUCTIBLE FROM POL_HOME_OWNER_SCH_ITEMS_CVGS WITH (NOLOCK) WHERE       
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID          
   AND ITEM_ID  IN (10030,10029)  AND IS_ACTIVE='Y')                                                
  END                                                     
--------------------------------------- END OF INLAND MARINE  ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
      
      
      
      
      
      
 --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
      
      
      
      
      
      
      
--------------------------------------------------START SECTION - II  --------------------------------------------------------------------------------------------------------------------                                                                     
  
    
      
      
      
      
      
      
      
      
-- ******************************  Liability Options start  ***********************************************                                                                                              
-----------------------------------------------------------------------------------------------------------                                        
--Liability Options--                                                                                                
---- Additional Premises (Number of Premises) -Occupied by Insured                                                                                           
  --(Removed Coverage Type check  COVERAGE_TYPE='S2' - Praveen kasana                                                       
IF @STATE_ID=14                                                                        
BEGIN              
 SELECT                                      
  @OCCUPIED_INSURED = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                           
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                
 WHERE                                                                                 
  COVERAGE_CODE_ID = 258       
 --AND COVERAGE_TYPE='S2'       
 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                               
END                                                                      
                                                                                          
IF @STATE_ID=22                                                                                             
BEGIN                                                 
 SELECT                     
  @OCCUPIED_INSURED = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                              
 FROM                                                                        
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                           
 WHERE                                                                  
  COVERAGE_CODE_ID = 259       
 --AND COVERAGE_TYPE='S2'       
 AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                         
END                                                                                          
                                                                                     
                                                                                      
                                                 
IF @OCCUPIED_INSURED IS NULL                                                                                          
BEGIN                                                 
SET @OCCUPIED_INSURED = 0.00                                       
END                                 
                                 
                                                                                          
-----  Additional Premises (Number of Premises) -Residence Premises - Rented to Others                                                                                           
IF @STATE_ID=14              
BEGIN                                                                                          
 SELECT                         
  @RESIDENCE_PREMISES = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                   
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                
 WHERE                                                        
  COVERAGE_CODE_ID = 260 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                                                  
END                                                                         
                                                      
IF @STATE_ID=22                                                                   
BEGIN                                            
 SELECT                                                                                            
  @RESIDENCE_PREMISES = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                     
 FROM                                             
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                                              
 WHERE                                                
COVERAGE_CODE_ID = 261 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                                                  
END                                                                           
                                                             
                                  
                                                                  
IF @RESIDENCE_PREMISES IS NULL                                                  
BEGIN                                                                              
SET @RESIDENCE_PREMISES = 0.00                                                                           
END                                                                                          
                           
                                                                                         
                                                                                  
-----  Additional Premises (Number of Premises) -Other Location -Rented to Others (1 Family)                                                                                
                                                                   
IF @STATE_ID=14                                                                                         
BEGIN                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                   
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 947 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)                
 --SET @OTHER_LOC_1FAMILY ='1'        
 --ITrack # 6178 - 29 July 09 -Manoj Rathore                                                
 SELECT @OTHER_LOC_1FAMILY =CONVERT(VARCHAR(10),LIMIT_1) FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 947 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID      
END                                                              
                                                                                          
IF @STATE_ID=22                
BEGIN                                                    
 IF EXISTS(SELECT CUSTOMER_ID  FROM                 
 POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                             
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 948 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)                                                              
 --SET @OTHER_LOC_1FAMILY ='1'       
 --ITrack # 6178 - 29 July 09 -Manoj Rathore                                                                                             
 SELECT @OTHER_LOC_1FAMILY =CONVERT(VARCHAR(10),LIMIT_1) FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                   
 WHERE           
 COVERAGE_CODE_ID = 948 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID      
                       
END                                                         
                                     
IF @OTHER_LOC_1FAMILY IS NULL                                          
BEGIN                                                              
 SET @OTHER_LOC_1FAMILY=0.00                                                                              
END                                                                                          
                                                                         
                                                   
                                                                                          
                                                                                    
-----  Additional Premises (Number of Premises) -Other Location -Rented to Others (2 Family)                                                                   
                                                                                          
                                                                                           
IF @STATE_ID=14                                            
BEGIN                    
 if exists(SELECT Customer_id FROM   POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                        
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 949 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID )                
 --set @OTHER_LOC_2FAMILY = '1'                
 --ITrack # 6178 -28 July 09 Manoj Rathore      
 SELECT @OTHER_LOC_2FAMILY=CONVERT(VARCHAR(10),LIMIT_1)  FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 949 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                      
       
END                                                                                          
                                 
IF @STATE_ID=22                                                      
BEGIN           
 if exists(SELECT Customer_id FROM                                                                                         
 POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                      
 WHERE                           
 COVERAGE_CODE_ID = 950 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID )                
 --set @OTHER_LOC_2FAMILY = '1'                                                                                         
 --ITrack # 6178 -28 July 09 Manoj Rathore      
 SELECT @OTHER_LOC_2FAMILY=CONVERT(VARCHAR(10),LIMIT_1)  FROM POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                                                                                 
 COVERAGE_CODE_ID = 950 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                      
       
END                                                                                          
                 
IF @OTHER_LOC_2FAMILY IS NULL                                                      
BEGIN                                                                                           
 SET @OTHER_LOC_2FAMILY = 0.00                                                                                          
END                                                         
                      
---- HO-42 coverage                                                                           
                                                                                                       
IF @STATE_ID=14                                      
 BEGIN                                
 if exists (SELECT Customer_id FROM POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                       
 COVERAGE_CODE_ID = 266 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                      
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
 if exists (SELECT Customer_id FROM POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE                       
 COVERAGE_CODE_ID = 267 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                                  
 AND DWELLING_ID = @DWELLINGID)                      
BEGIN           
   SET @HO42 = 'Y'                                                                                                            
  END                                                                        
 ELSE                              
  BEGIN                                                      
   SET @HO42 = 'N'                                                                                                            
  END                        
END                       
                      
                                    
-----  Incidental Office , Private School or Studio - On Premises (HO-42)                       
                                       
IF @STATE_ID = 14                                                                
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 266 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 267 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
 BEGIN                                                                                                
   SET @ONPREMISES_HO42 = 'Y'                          
  END                                                                                                
 ELSE                                                                                                
  BEGIN                                            
   SET @ONPREMISES_HO42 = 'N'                                                                                                
  END                                      
END                                                                                          
                                                                                  
                                                             
------- Incidental Office , Private School or Studio - Located in Other Structure                                                                                           
                                                                                          
                                                                                          
IF @STATE_ID = 14                           
BEGIN                
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 268 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                           
   SET @LOCATED_OTH_STRUCTURE = 'Y'                             
  END                                
 ELSE                                                                                        BEGIN               
   SET @LOCATED_OTH_STRUCTURE = 'N'                                                        
  END                                                                                                
END                                                                                          
                                                                                          
IF @STATE_ID = 22                               
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 269 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                                             
   SET @LOCATED_OTH_STRUCTURE = 'Y'                                                                                                
  END                                            
 ELSE                                    
  BEGIN                                                                                                
   SET @LOCATED_OTH_STRUCTURE = 'N'                       
  END                  
END                                                                                          
            ------ Incidental Office , Private School or Studio - Instruction Only (HO-42)                                                                                          
                                                
IF @STATE_ID = 14                                                                                      
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 270 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)   BEGIN         
  
                                                                                 
   SET @INSTRUCTIONONLY_HO42 = 'Y'                                                                    
  END           
 ELSE                                                         
  BEGIN                                         
   SET @INSTRUCTIONONLY_HO42 = 'N'                  
  END         
END                                                                                          
                          
IF @STATE_ID = 22                   
BEGIN                       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 271 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                         
   SET @INSTRUCTIONONLY_HO42 = 'Y'                                                      
  END                                                                                                
 ELSE                                                                                                
  BEGIN                                                                                                
   SET @INSTRUCTIONONLY_HO42 = 'N'                                 
  END                                                                                                
END                                                                                          
------ Incidental Office , Private School or Studio - Off Premises (HO-43)                                                                                          
                                                                                          
IF @STATE_ID = 14         
BEGIN                                                
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 272 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 273 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)   BEGIN          
   
   
      
       
       
      
       
      
      
                                                                                     
   SET @OFF_PREMISES_HO43 = 'Y'                      
  END                   
 ELSE                                                                          
  BEGIN                          
   SET @OFF_PREMISES_HO43 = 'N'                                   
  END          
END                                                                              
------ Personal Injury (HO-82)                                                                                           
IF @STATE_ID = 14                                                                           
BEGIN                                                 
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 274 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 275 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                                             
   SET @PIP_HO82 = 'Y'                         
  END                                                            
 ELSE                                                                                                
  BEGIN                                                             
   SET @PIP_HO82 = 'N'                                                                    
  END                                                                                                
END                                                                         
                                                                                          
                                                                                          
-----  Residence Employees (number)                                                      
                  
                                                                          IF @STATE_ID=14           
BEGIN                                                           
 SELECT                                                              
  @RESIDENCE_EMP_NUMBER = ISNULL(LIMIT_1,'0')             
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                           
 WHERE                                                                                                 
  COVERAGE_CODE_ID = 276 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                        
END                                                                                          
            IF @STATE_ID=22                                                                                             
BEGIN                                                                                        
 SELECT                                                                               
  @RESIDENCE_EMP_NUMBER = ISNULL(LIMIT_1,'0')                   
 FROM                
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                               
 WHERE                                                               
  COVERAGE_CODE_ID = 277 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                             
END                                                                                          
                                                                                          
                                                              
IF @RESIDENCE_EMP_NUMBER IS NULL                                
BEGIN                        
 SET @RESIDENCE_EMP_NUMBER = 0                                                                               
END                                                                                          
         
                                                                                          
------ Business Pursuits(HO-71)Clerical Office Employee, Salesmen, Collectors(No Installation ,Demo or Service)                                                          
IF @STATE_ID = 14                                                                                           
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 278 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)       
  BEGIN                                  
   SET @CLERICAL_OFFICE_HO71 = 'Y'                               
  END                                                                                                
 ELSE                              
  BEGIN                                                                           
   SET @CLERICAL_OFFICE_HO71 = 'N'                                                                                  
  END                      END            
                                                                                          
IF @STATE_ID = 22                                                                                           
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)WHERE COVERAGE_CODE_ID = 279 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                                                
   SET @CLERICAL_OFFICE_HO71 = 'Y'                                                
  END                                                                                                
 ELSE                                
  BEGIN                              
   SET @CLERICAL_OFFICE_HO71 = 'N'                                                                         
END                                    
END                                                                   
------- Business Pursuits(HO-71)Salesmen, Collectors or Messengers(Including Installation, Demo or Service)                                                    
IF @STATE_ID = 14                                                                               
BEGIN       
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)  WHERE COVERAGE_CODE_ID = 280 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)   BEGIN         
  

   SET @SALESMEN_INC_INSTALLATION = 'Y'                                                                                   
  END                                                                                             
 ELSE                                                                                                
  BEGIN                                                                                
   SET @SALESMEN_INC_INSTALLATION = 'N'                                                                                    
  END                                                                                                
END                                                                   
                                                
IF @STATE_ID = 22                                                                                           
BEGIN                                
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 281 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                  
   SET @SALESMEN_INC_INSTALLATION = 'Y'                                                                                                
  END                                                       
ELSE                                                                
  BEGIN                                             
   SET @SALESMEN_INC_INSTALLATION = 'N'                  
  END                                                                                     
END                                                                                          
------- Business Pursuits(HO-71)Salesmen, Collectors or Messengers(Including Installation, Demo or Service)                                                   
IF @STATE_ID = 14            
BEGIN                                                   
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE  COVERAGE_CODE_ID = 282 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)   BEGIN         
   
    

 
                         
   SET @TEACHER_ATHELETIC = 'Y'                                                                                                
  END                          
 ELSE                                                           
  BEGIN                                                                                                
   SET @TEACHER_ATHELETIC = 'N'                                                                                                
  END                      
END                                                   
                                                                                          
IF @STATE_ID = 22                                                         
BEGIN                                
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 283 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                  
   SET @TEACHER_ATHELETIC = 'Y'                                                                                                
  END                                                                                 
 ELSE                                                                                  
  BEGIN                                 
   SET @TEACHER_ATHELETIC = 'N'                              
  END                                                                                                
END                                                                                          
 -------Business Pursuits(HO-71)Teachers-NOC (Excl. Corporal Punishment)                                                                      
IF @STATE_ID = 14                                                                                           
BEGIN                                                                                             
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)  WHERE  COVERAGE_CODE_ID = 284 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK) WHERE COVERAGE_CODE_ID = 285 AND COVERAGE_TYPE='S2' AND CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID)      
  BEGIN                                                                                                
   SET @TEACHER_NOC = 'Y'                                                    
  END                                                                                                
 ELSE                              
  BEGIN                                                                                                
   SET @TEACHER_NOC = 'N'                                              
  END                   
END                              
                                                                                  
-------Farm Liability (number of locations) Incidental Farming on Premises(HO-72)                                                                                           
IF @STATE_ID = 14                                        
BEGIN                                                                 
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)         
   WHERE  COVERAGE_CODE_ID = 286         
--   AND COVERAGE_TYPE='S2'          
   AND CUSTOMER_ID = @CUSTOMERID         
   AND POLICY_ID = @POLICYID         
   AND POLICY_VERSION_ID = @POLICYVERSIONID        
   AND DWELLING_ID = @DWELLINGID)                           
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
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)          
    WHERE COVERAGE_CODE_ID = 287         
--    AND COVERAGE_TYPE='S2'         
    AND CUSTOMER_ID = @CUSTOMERID         
    AND POLICY_ID = @POLICYID         
    AND POLICY_VERSION_ID = @POLICYVERSIONID        
    AND DWELLING_ID = @DWELLINGID)        
BEGIN                                                                      
   SET @INCIDENTAL_FARMING_HO72 = 'Y'                                                                                                
  END                                                    
 ELSE                                                           
  BEGIN                                                           
   SET @INCIDENTAL_FARMING_HO72 = 'N'                                              
  END                            
END                                                  
 -----   Farm Liability (number of locations) Owned Farms Operated by Insured's Employees(HO-73)                              
                                                               
IF @STATE_ID=14                                                                                          
BEGIN                                                                                           
        SELECT                                                                                            
  @OTH_LOC_OPR_EMPL_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                   
 FROM                                                                 
  POL_DWELLING_SECTION_COVERAGES    WITH (NOLOCK)                                                                                             
 WHERE                                                         
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 288                                                                          
 AND DWELLING_ID = @DWELLINGID                                     
END                               
---                                                                                          
IF @STATE_ID=22                                                              
BEGIN           
 SELECT          
  @OTH_LOC_OPR_EMPL_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                    
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                                                                                
 WHERE                              
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 289                                    
 AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
                                 
                                                                                          
IF @OTH_LOC_OPR_EMPL_HO73 IS NULL                                           
 BEGIN                                                                                          
  SET @OTH_LOC_OPR_EMPL_HO73 = 0.00                                                          
 END                                           
                                                                                          
                    
------  Farm Liability (number of locations) Owned Farms Rented to Others(HO-73)                                                                                           
                                                                                       
IF @STATE_ID=14                                                                                          
BEGIN                                             
        SELECT                                          
  @OTH_LOC_OPR_OTHERS_HO73 = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))                                                                                          
 FROM                                         
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                              
 WHERE                                                                
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 290       
   AND DWELLING_ID = @DWELLINGID                                     
END                                                                                          
---                                
IF @STATE_ID=22                                           
BEGIN                                                                                           
 SELECT                                               
  @OTH_LOC_OPR_OTHERS_HO73 =CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))        
 FROM                                                                                                 
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                
 WHERE                                                                                                 
CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID = 291                                                                                                
   AND DWELLING_ID = @DWELLINGID              
END                  
                                                                           
                         
                                                                                    
IF @OTH_LOC_OPR_OTHERS_HO73 IS NULL                                 
 BEGIN                  
  SET @OTH_LOC_OPR_OTHERS_HO73 = 0.00                                                                                          
 END                            
                                                                                
                                         
-----------------------------HO 38 PK                                    
IF @STATE_ID=14                                                                                                        
BEGIN                                
  SELECT                                                                                                          
  @HO48INCLUDE = CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2)),                                    
   @HO48ADDITIONAL = ISNULL(DEDUCTIBLE_1 ,0.00)                                                           
 FROM                                                                                
POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                                                             
 WHERE                 
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and  COVERAGE_CODE_ID = 135                                    
  AND DWELLING_ID = @DWELLINGID                                                                    
END                                                                                                        
---                                                                                                   
IF @STATE_ID=22                                                                          
BEGIN                                                          
 SELECT                                                                                  
  @HO48INCLUDE =CAST(ISNULL(LIMIT_1,'0.00') AS DECIMAL(18,2))  ,                                    
   @HO48ADDITIONAL = ISNULL(DEDUCTIBLE_1 ,0.00)                                                                                                       
 FROM                       
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)                                   
 WHERE                                                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and  COVERAGE_CODE_ID = 5                                    
  AND DWELLING_ID = @DWELLINGID                                                    
END                                        
                                    
                              
-- For satellite dishes                          
IF @STATE_ID=14                                                 
BEGIN           
  SELECT @HO48ADDITIONAL = isnull(@HO48ADDITIONAL,0.00) + ISNULL(DEDUCTIBLE_1 ,0.00)                                  
 FROM                               
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)      
 WHERE                                                                                                             
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and COVERAGE_CODE_ID = 742                                   
  AND DWELLING_ID = @DWELLINGID                                                        
END                    
---                                          
IF @STATE_ID=22                                       
BEGIN                     
 SELECT                           
   @HO48ADDITIONAL = @HO48ADDITIONAL +  ISNULL(DEDUCTIBLE_1 ,0.00)                                                                                                     
 FROM                                                                                                         
  POL_DWELLING_SECTION_COVERAGES WITH (NOLOCK)              
 WHERE                                                                                                             
   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and COVERAGE_CODE_ID = 908                                    
  AND DWELLING_ID = @DWELLINGID                                                                                                              
END                           
                          
                          
                                                  
--------------------------------- START ------------------------------------                
                                                                                              
                                                
--- NONSMOKER  and MULTIPLEPOLICYFACTOR                                                                                 
      SET @MULTIPLEPOLICYFACTOR='N'                                                
                 SELECT @NONSMOKER = CASE ISNULL(NON_SMOKER_CREDIT,'0')                                                                                               
 WHEN '1' THEN 'Y'                     
 ELSE 'N'                    
 END,                                                                     
 @MULTIPLEPOLICYFACTOR =CASE ISNULL(MULTI_POLICY_DISC_APPLIED,'N')                                                     
 WHEN '1' THEN 'Y'                                                
 ELSE 'N'                         
 END,                                                                                   
 --@LOSSFREE = CASE ISNULL(IS_LOSS_FREE_12_MONTHS,'0')                                        
 --WHEN '1' then 'Y'                                                                                            
--else 'N'                                                     
--END,                            
@INSUREWITHWOL=ISNULL(YEARS_INSU_WOL,0)                         
                                                                              
--@EXPERIENCE=ISNULL(EXP_AGE_CREDIT,'0')                                                                                            
                                                                        
FROM                                                                                               
 POL_HOME_OWNER_GEN_INFO  WITH (NOLOCK)                                                                     
WHERE                                                                                 
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                                                                      
                                    
                                    
---------------------LOSS FREE                                    
if @POLICYSTATUS='RENEWED'                                                             
begin                                    
 SET @NEWBUSINESSFACTOR='REN'                          
end                                    
IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH (NOLOCK)  WHERE  CUSTOMER_ID = @CUSTOMERID AND LOB=1 and (DATEDIFF(day ,OCCURENCE_DATE ,@QUOTEEFFDATE)< 365) )                             
BEGIN                                    
 SET @LOSSFREE = 'N'                                
 SET @NOTLOSSFREE='Y'                              
END                                    
ELSE                                    
BEGIN                                    
 SET @LOSSFREE = 'Y'                             
 SET @NOTLOSSFREE='N'                                   
END        
                                    
                    
----------------END LOSS FREE                                                           
                                                            
--This is given once two years are completed, at the third renewal                                     
IF   @INSUREWITHWOL > 2                                                                                
 SET @VALUESCUSTOMERAPP = 'Y'                                
ELSE                                                          
 SET @VALUESCUSTOMERAPP = 'N'           
 SET @VALUEDCUSTOMER  = @VALUESCUSTOMERAPP                                                                                                  
                                                                           
-- For Michigan Policy Lookup Values                                                                                 
-- Applicable to all HO Products                                                                      
-- In case of HO-4, only applicable in case if Covg C is 15000 or more.                
                                      
IF ((@POLICYTYPE =  11405 OR @POLICYTYPE= 11195 )  AND (@MULTIPLEPOLICYFACTOR='Y') AND (@DLIMIT < 15000))            
   SET @MULTIPLEPOLICYFACTOR='N'                                                     
                                                                    IF ISNULL(@MULTIPLEPOLICYFACTOR,'')=''                                                                      
 SET @MULTIPLEPOLICYFACTOR='N'                                                                                  
                                                                         
/*        
Dwelling underconstruction is not available for HO-4 or HO-6.                                                                                  
                                                                                  
11405">HO-4                         
11406">HO-6                                                                                  
11195">HO-4                                                                                  
11196">HO-6                                                                                  
                                                                                    
*/                                                        
IF @POLICYTYPE=11405 OR @POLICYTYPE=11406 OR @POLICYTYPE=11195 OR @POLICYTYPE=11196                                                                                  
 SET @UNDERCONAPP='N'                                                                                   
                                
ELSE                   
 SET @UNDERCONAPP='Y'                                                                                  
                                                                       
/***********Age of Home Credit                                    
       INDIANA - Only on HO-2,HO-3 and HO-5                                                                                    
            MICHIGAN -ALL                                                                                    
                                                                                    
11193-HO-2 Repair Cost                                                                                  
11194-HO-3 Repair Cost                                                                                    
11195-HO-4                                                           
11245-HO-4 Deluxe                           
11196-HO-6                                                                                    
11246-HO-6 Deluxe                                                                                    
                  
applicable to only H0-6 and h0-6 Delux                                                                                    
***********/                                                                                      
IF @POLICYTYPE != 11196 and @POLICYTYPE != 11246 and @POLICYTYPE !=11406 and @POLICYTYPE != 11408                                                                      
 SET @HO35INCLUDE='0'                            
                                                                         
IF ISNULL(@POLICYTYPE,'') = '' OR @POLICYTYPE = 11193 OR @POLICYTYPE = 11194 OR @POLICYTYPE=11245 OR @POLICYTYPE=11246                                                                    
   OR @POLICYTYPE=11195 OR @POLICYTYPE=11196 AND @AGEOFHOME <> '0'                                                                              
   SET @AGEHOMECREDIT = 'N'                                                                                    
ELSE                                                                                    
   SET @AGEHOMECREDIT = 'Y'                    
--------------------------------- END  Praveen KASANA  -----------------------------------------------------------------------                                                                                            
--- EXPRIENCE  ---                                    
                                                                                   
DECLARE @DATE_OF_BIRTH   varchar(100)                                         
                                                                           
SELECT                                                                                         
 @DATE_OF_BIRTH = ISNULL(CONVERT(Varchar(10),CLT.CO_APPL_DOB,101),'')                                                                                    
                                             
 FROM                                                                               
   CLT_APPLICANT_LIST CLT WITH (NOLOCK) INNER JOIN POL_APPLICANT_LIST APL  WITH (NOLOCK)                                                                            
      ON CLT.APPLICANT_ID=APL.APPLICANT_ID                                                       
       WHERE  APL.POLICY_ID=@POLICYID AND                                                       
              APL.POLICY_VERSION_ID=@POLICYVERSIONID AND                                                        
              APL.CUSTOMER_ID = @CUSTOMERID AND                                              
              APL.IS_PRIMARY_APPLICANT=1                                                        
                                                                
                                              
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
SELECT                                                              
 @CONSTRUCTIONCREDIT =  CASE ISNULL(IS_UNDER_CONSTRUCTION,'0')               
  WHEN '1' THEN 'Y'                                     
  ELSE 'N'                                      
  END,           
  @N0_LOCAL_ALARM  =   ISNULL(NUM_LOC_ALARMS_APPLIES,'0'),                                                 
 @BURGLAR=ISNULL(CENT_ST_BURG,'N'),                                                                                    
 @CENTRAL_FIRE=ISNULL(CENT_ST_FIRE,'N'),                      
 @BURGLER_ALERT_POLICE=ISNULL(DIR_POLICE,'N'),                                                                                  
 @FIRE_ALARM_FIREDEPT=ISNULL(DIR_FIRE,'N'),                       
 @CENT_ST_BURG_FIRE=ISNULL(CENT_ST_BURG_FIRE,'N'),                                                                                
 @DIR_FIRE_AND_POLICE=ISNULL(DIR_FIRE_AND_POLICE,'N'),                                                                      
 @LOC_FIRE_GAS=ISNULL(LOC_FIRE_GAS,'N'),                                                       
 @TWO_MORE_FIRE=ISNULL(TWO_MORE_FIRE,'N'),                  
 @DISTANCET_FIRESTATION     =  ISNULL(FIRE_STATION_DIST,0) ,                                                                                               
 @NUMBEROFFAMILIES      =  ISNULL(NO_OF_FAMILIES,0)                                     
FROM                                                                               
 POL_HOME_RATING_INFO  WITH (NOLOCK)                                                                                            
WHERE                                                                                         
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID  AND DWELLING_ID=@DWELLINGID                             
-- need to be changed after screen level changes       
                     
if (@LOC_FIRE_GAS = 'Y')                      
 set @N0_LOCAL_ALARM='1'                      
if(@TWO_MORE_FIRE='Y')                      
 set @N0_LOCAL_ALARM='2'                       
                                           
                                    
if ISNULL(@BURGLAR,'') = ''                                                                                    
  SET @BURGLAR='N'                                                                                    
if ISNULL(@CENTRAL_FIRE,'')=''                                                           
  SET @CENTRAL_FIRE='N'                                           
if ISNULL(@BURGLER_ALERT_POLICE,'')=''                                                                           
  SET @BURGLER_ALERT_POLICE='N'                                                                                    
if ISNULL(@FIRE_ALARM_FIREDEPT,'')=''                                                                 
  SET @FIRE_ALARM_FIREDEPT='N'                                             
if ISNULL(@CENT_ST_BURG_FIRE,'')=''                                                                                    
  SET @CENT_ST_BURG_FIRE='N'                                                                                    
if ISNULL(@DIR_FIRE_AND_POLICE,'')=''                                                   
  SET @DIR_FIRE_AND_POLICE='N'                           
if ISNULL(@LOC_FIRE_GAS,'')=''                                                                                    
  SET @LOC_FIRE_GAS='N'                                                                                    
if ISNULL(@TWO_MORE_FIRE,'')=''                                                      
  SET @TWO_MORE_FIRE='N'                                                                                    
if ISNULL(@BURGLAR,'')=''                                               
  SET @BURGLAR='N'                                                                                    
if ISNULL(@CONSTRUCTIONCREDIT,'')=''  OR  @UNDERCONAPP= 'N'                                                                                  
  SET @CONSTRUCTIONCREDIT='N'                   
                         
if (@CENT_ST_BURG_FIRE = 'Y')          Begin                      
 set  @BURGLAR='Y'                      
 set  @CENTRAL_FIRE='Y'                     
End                      
if (@DIR_FIRE_AND_POLICE = 'Y')                      
Begin                      
 set  @BURGLER_ALERT_POLICE='Y'                      
 set  @FIRE_ALARM_FIREDEPT='Y'                      
End                                                                                     
                                                                                        
----  @WOODSTOVE_SURCHARGE                 
DECLARE @WOODSTOVE_SURCHARGE_UNDERWRITING NVARCHAR(20)                
DECLARE @WOODSTOVE_SURCHARGE_RATING_INF NVARCHAR(20)                                                                 
SELECT                                                                                           
 @WOODSTOVE_SURCHARGE_UNDERWRITING  =  case  ISNULL(ANY_HEATING_SOURCE,'0')                                                                                              
    WHEN '1' THEN 'Y'                   
    ELSE 'N'                                                                                              
    END                                                      
FROM                                                                                 
 POL_HOME_OWNER_GEN_INFO WITH (NOLOCK)                         
WHERE             
 CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID                                              
                                                                    
if exists(select Customer_id from POL_HOME_RATING_INFO WITH (NOLOCK) where CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID and ((PRIMARY_HEAT_TYPE=11807 or PRIMARY_HEAT_TYPE=6224 or PRIMARY_HEAT_TYPE=6223) or 
  
    
      
      
      
      
      
      
      
(SECONDARY_HEAT_TYPE=11807 or SECONDARY_HEAT_TYPE=6224 or SECONDARY_HEAT_TYPE=6223)))                
                
set @WOODSTOVE_SURCHARGE_RATING_INF = 'Y'                
                              
 if(@WOODSTOVE_SURCHARGE_RATING_INF = 'Y' or @WOODSTOVE_SURCHARGE_UNDERWRITING = 'Y')                                                      
set  @WOODSTOVE_SURCHARGE ='Y'                
else                
set  @WOODSTOVE_SURCHARGE ='N'                                     
-- ******************************  Liability Options start  ***********************************************                                                                                              
                                                
--TRUNCATE THE DECIMAL SPACE BY SHAFI                                                                                       
                                              
SET @LOSSOFUSE_LIMIT=CONVERT(DECIMAL(18),@LOSSOFUSE_LIMIT)                                                                      
                                                              
IF @HO40INCLUDE='0.00' OR @HO40INCLUDE=0.00                                                                      
 SET @HO40INCLUDE=''                                                                      
                                                                                              
 -- SUBURBAN CLASS DISCOUNT      
IF  EXISTS(SELECT * FROM POL_HOME_RATING_INFO   WITH(NOLOCK)                                       
  WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID and DWELLING_ID=@DWELLINGID      
  AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963')      
 BEGIN      
  SET @SUBPROTDIS='Y'      
 END      
 -- YEARS INSURED      
      
DECLARE @NONWEATHERLOSS SMALLINT      
DECLARE @WEATHERLOSS SMALLINT      
DECLARE @CLMWEATHERLOSS SMALLINT      
DECLARE @CLMNONWEATHERLOSS SMALLINT      
DECLARE @CLMLOSSES INT      
DECLARE @WEATHPRIORLOSS INT      
DECLARE @NONWEATHPRIORLOSS INT      
SET @CLMLOSSES=0      
SET @NONWEATHERLOSS=0                                                                                           
SET @WEATHERLOSS=0      
SET @CLMWEATHERLOSS=0      
SET @CLMNONWEATHERLOSS=0      
SET @WEATHPRIORLOSS=0      
SET @NONWEATHPRIORLOSS=0      
      
SELECT @YEARSCONTINSURED=YEARS_INSU , @YEARSCONTINSUREDWITHWOLVERINE=YEARS_INSU_WOL--,       
  --@NONWEATHERLOSS= NON_WEATHER_CLAIMS, @WEATHERLOSS=WEATHER_CLAIMS       
FROM POL_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND  POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID       
      
SELECT @WEATHPRIORLOSS = COUNT(PLH.CUSTOMER_ID) FROM PRIOR_LOSS_HOME PLH      
 INNER JOIN APP_PRIOR_LOSS_INFO APLI       
 ON PLH.CUSTOMER_ID=APLI.CUSTOMER_ID      
 AND PLH.LOSS_ID=APLI.LOSS_ID      
 WHERE PLH.CUSTOMER_ID=@CUSTOMERID AND LOB='1'       
 AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE        
 AND ISNULL(WEATHER_RELATED_LOSS,10964)=10963      
      
SELECT @NONWEATHPRIORLOSS = COUNT(PLH.CUSTOMER_ID) FROM PRIOR_LOSS_HOME PLH      
 INNER JOIN APP_PRIOR_LOSS_INFO APLI       
 ON PLH.CUSTOMER_ID=APLI.CUSTOMER_ID      
 AND PLH.LOSS_ID=APLI.LOSS_ID      
 WHERE PLH.CUSTOMER_ID=@CUSTOMERID AND LOB='1'       
 AND OCCURENCE_DATE BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE        
 AND ISNULL(WEATHER_RELATED_LOSS,10964)=10964      
       
SELECT @CLMLOSSES=COUNT(CCI.CLAIM_ID)  FROM CLM_OCCURRENCE_DETAIL COD     
  INNER JOIN  CLM_CLAIM_INFO CCI      
  ON COD.CLAIM_ID=CCI.CLAIM_ID      
 WHERE CCI.CUSTOMER_ID = @CUSTOMERID AND LOB_ID='1' AND LOSS_DATE       
 BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE AND ISNULL(WEATHER_RELATED_LOSS,10964)=10964      
      
SELECT @CLMWEATHERLOSS = COUNT(COD.CLAIM_ID) FROM CLM_OCCURRENCE_DETAIL COD      
  INNER JOIN CLM_CLAIM_INFO CCI      
  ON COD.CLAIM_ID=CCI.CLAIM_ID      
  WHERE CCI.CUSTOMER_ID = @CUSTOMERID AND LOB_ID='1' AND LOSS_DATE       
  BETWEEN DATEADD(YEAR,-3,@QUOTEEFFDATE) AND @QUOTEEFFDATE AND ISNULL(WEATHER_RELATED_LOSS,10964)=10963      
       
SET @WEATHERLOSS = @CLMWEATHERLOSS + @WEATHPRIORLOSS      
SET @NONWEATHERLOSS = @CLMLOSSES + @NONWEATHPRIORLOSS      
 -- PRIOR LOSS INFO      
IF(@WEATHERLOSS=1)      
 BEGIN      
  SET @WEATHERLOSS=0      
 END      
 ELSE IF(@WEATHERLOSS>1)      
 BEGIN      
  SET @WEATHERLOSS=@WEATHERLOSS - 1      
 END      
       
SET @TOTALLOSS = @WEATHERLOSS + @NONWEATHERLOSS      
      
-- **************************************************************************************************************                                                                                              
                                      
END                                                                                    
 -- return values                                                                                          
BEGIN                                                                           
                                                             
SELECT                                                                                                
                                                                             
  --- @PERSONALPROPERTYINCREASEDLIMITADDITIONAL  as test ,                            
 @LOB_ID    as LOB_ID,                                                                                              
 ''     as POLICY_ID,                                                                                              
                                                                                   
-- Basic Policy Page                                                                                               
                    
 @STATENAME       as  STATENAME,                                                               
 RTRIM(LTRIM(@NEWBUSINESSFACTOR))        as  NEWBUSINESSFACTOR,                                                                 
 @QUOTEEFFDATE      as  QUOTEEFFDATE,                                                                                           
 @QUOTEEXPDATE       as  QUOTEEXPDATE,                                    
 @POL_NUMBER  AS POL_NUMBER,                                    
 @POL_VERSION  AS POL_VERSION,                                                                                       
 @TERMFACTOR      as TERMFACTOR,                                                                                              
 @SEASONALSECONDARY     as SEASONALSECONDARY,                                                                                         
 @WOLVERINEINSURESPRIMARY  as WOLVERINEINSURESPRIMARY,                                                                                              
 'HO-2'       as PRODUCTNAME,                                                                                
 'REPLACE'    as PRODUCT_PREMIER,                                                                                          
 CONVERT(decimal(18),50000)  as REPLACEMENTCOSTFACTOR,                                                    
 isnull(@DWELLING_LIMITS,'50000')     as DWELLING_LIMITS,             
'01' as FIREPROTECTIONCLASS,                                                         
 '01'     as PROTECTIONCLASS,                                                                                              
 5  as DISTANCET_FIRESTATION,                                                                
 '1000 OR LESS'    AS FEET2HYDRANT,                         
 500    AS DEDUCTIBLE,                                
 11852    as EXTERIOR_CONSTRUCTION,                                      
'ALUMINIUM' AS EXTERIOR_CONSTRUCTION_DESC,                  
 'F'  as EXTERIOR_CONSTRUCTION_F_M,                                                  
 @DOC        as DOC,                                                              
@AGEOFHOME       as AGEOFHOME,                                                                                              
 1     as NUMBEROFFAMILIES,                                                                                              
 0      as NUMBEROFUNITS,                                                                                              
 isnull(@PERSONALLIABILITY_LIMIT,100000.00) AS PERSONALLIABILITY_LIMIT,                                                             
 '0'     AS PERSONALLIABILITY_PREMIUM,              
 isnull(@PERSONALLIABILITY_DEDUCTIBLE,0)  as PERSONALLIABILITY_DEDUCTIBLE,                    
                                                                                          
 isnull(@MEDICALPAYMENTSTOOTHERS_LIMIT,1000.00) as MEDICALPAYMENTSTOOTHERS_LIMIT,                                                                         
 '0'   as MEDICALPAYMENTSTOOTHERS_PREMIUM,                                                                                              
 isnull(@MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE,0)    as MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE,                                                                '01F'       as FORM_CODE,                                                                         
                                  
 --Property Page--                                                                                                
                                                                                              
@HO20           AS HO20,                                          
@HO21           AS HO21,                                                                                                
@HO25        AS HO25,                                                               
@HO23      AS HO23,                                         
@HO22      AS HO22,                                          
@HO24      AS HO24,                                   
@HO34           AS HO34,                                                                                              
@HO11           AS HO11,                                                                                            
@HO32           AS HO32,                                                                                            
@HO277          AS HO277,                                                                                                
@HO455          AS HO455,                
@HO327          AS HO327,                                                                                            
@HO33       AS HO33,                                                                                          
@HO315          AS HO315,                                                                                            
@HO9            AS HO9,                                                                                            
@HO287          AS HO287,                                                 
@HO42  as HO42,                                                                                      
@HO96FINALVALUE         AS HO96FINALVALUE,                                                                                      
CONVERT(DECIMAL(18),@HO96INCLUDE)            AS HO96INCLUDE,                                                                                
CONVERT(DECIMAL(18),@HO96ADDITIONAL)          AS HO96ADDITIONAL,                                                                     
CONVERT(DECIMAL(18),isnull(@HO48INCLUDE,'0.00')) AS HO48INCLUDE,                          
isnull(@HO48ADDITIONAL,'0.00') AS HO48ADDITIONAL,                                                              
@HO40INCLUDE  AS HO40INCLUDE,                      
@HO40ADDITIONAL         AS HO40ADDITIONAL,                                                                                                
'0.00'  AS HO42ADDITIONAL,                                                       
--''            AS REPAIRCOSTINCLUDE,    -- Not Required               
                  
CONVERT(DECIMAL(18),@PERSONALPROPERTYINCREASEDLIMITINCLUDE)     AS PERSONALPROPERTYINCREASEDLIMITINCLUDE,                                                                                                
CONVERT(DECIMAL(18),ISNULL(@PERSONALPROPERTYINCREASEDLIMITADDITIONAL,'0.00')) AS PERSONALPROPERTYINCREASEDLIMITADDITIONAL,                  
@PERSONALPROPERTYAWAYINCLUDE         AS PERSONALPROPERTYAWAYINCLUDE,                                                                                
@PERSONALPROPERTYAWAYADDITIONAL      AS PERSONALPROPERTYAWAYADDITIONAL,                                                  
@UNSCHEDULEDJEWELRYINCLUDE           AS UNSCHEDULEDJEWELRYINCLUDE,                                                                                                
@UNSCHEDULEDJEWELRYADDITIONAL        AS UNSCHEDULEDJEWELRYADDITIONAL,                      
@REPAIRCOSTADDITIONAL   AS REPAIRCOSTADDITIONAL,                                                                                             
@MONEYINCLUDE         AS MONEYINCLUDE,                                                                                         
@MONEYADDITIONAL      AS MONEYADDITIONAL,                                                    
@SECURITIESINCLUDE    AS SECURITIESINCLUDE,                                                                                                
@SECURITIESADDITIONAL AS SECURITIESADDITIONAL,                                                    
@SILVERWAREINCLUDE    AS SILVERWAREINCLUDE,                                                                                                
@SILVERWAREADDITIONAL AS SILVERWAREADDITIONAL,                                                                      
@FIREARMSINCLUDE      AS FIREARMSINCLUDE,                    
@FIREARMSADDITIONAL   AS FIREARMSADDITIONAL,                                                           
@HO312INCLUDE         AS HO312INCLUDE,                                                                                                
@HO312ADDITIONAL      AS HO312ADDITIONAL,                                                                   
@LOSSOFUSE_LIMIT      AS LOSSOFUSE_LIMIT,                                         
@ADDITIONALLIVINGEXPENSEADDITIONAL     AS ADDITIONALLIVINGEXPENSEADDITIONAL,               
@LOSSOFUSE_LIMIT     AS ADDITIONALLIVINGEXPENSEINCLUDE      ,                                                                                   
                                                                     
--Substring(convert(varchar(30),@HO53INCLUDE,0),0,charindex('.',convert(varchar(30),@HO53INCLUDE,0)))   AS HO53INCLUDE,                                                                            
                   
@HO53INCLUDE              AS HO53INCLUDE,                                                                                             
@HO53ADDITIONAL           AS HO53ADDITIONAL,                                                           
@HO35INCLUDE            AS HO35INCLUDE,                                                                              
@HO35ADDITIONAL         AS HO35ADDITIONAL,                                            
--''             AS SPECIFICSTRUCTURESINCLUDE,  -- Not Required                                                              
@SPECIFICSTRUCTURESADDITIONAL             AS SPECIFICSTRUCTURESADDITIONAL,      --     this value is used for testing purpose it is used in step 42         
 ISNULL(@HO493,'N') AS HO493,      
                                   
--Liability Options--                                                       
                                                                             
                             
   @OCCUPIED_INSURED  AS OCCUPIED_INSURED,                
   @RESIDENCE_PREMISES          AS RESIDENCE_PREMISES,                                                           
   @OTHER_LOC_1FAMILY          AS OTHER_LOC_1FAMILY,                                       
   @OTHER_LOC_2FAMILY           AS OTHER_LOC_2FAMILY,                                                                                                
   @ONPREMISES_HO42           AS ONPREMISES_HO42,                                           
   @LOCATED_OTH_STRUCTURE       AS LOCATED_OTH_STRUCTURE,                                                                                                
   @INSTRUCTIONONLY_HO42        AS INSTRUCTIONONLY_HO42,                                                                             
   @OFF_PREMISES_HO43           AS OFF_PREMISES_HO43,                                                                     
   @PIP_HO82              AS PIP_HO82,                                                                         
   @HO200               AS HO200,                                    
   @HO64RENTERDELUXE   AS HO64RENTERDELUXE,                                                                                          
     isnull(@HO66CONDOMINIUMDELUXE ,'N') as HO66CONDOMINIUMDELUXE,                                                                                           
   @RESIDENCE_EMP_NUMBER        AS RESIDENCE_EMP_NUMBER,                                                                
   @CLERICAL_OFFICE_HO71        AS CLERICAL_OFFICE_HO71,                              
   @SALESMEN_INC_INSTALLATION   AS SALESMEN_INC_INSTALLATION,                                                    
   @TEACHER_ATHELETIC         AS TEACHER_ATHELETIC,                                                                                 
   @TEACHER_NOC             AS TEACHER_NOC,                                                                                    
   @INCIDENTAL_FARMING_HO72     AS INCIDENTAL_FARMING_HO72,                                   
   @OTH_LOC_OPR_EMPL_HO73       AS OTH_LOC_OPR_EMPL_HO73,                                                                     
   CONVERT(VARCHAR(20),CONVERT(DECIMAL(18),@OTH_LOC_OPR_OTHERS_HO73))             AS OTH_LOC_OPR_OTHERS_HO73,                                                                                               
--   @OTH_LOC_OPR_OTHERS_HO73    AS OTH_LOC_OPR_OTHERS_HO73,                                                                                                
                                                   
                                                                                 
                                                                                          
 -- Credit and Charges --                                                   
                                          
   @LOSSFREE      AS LOSSFREE,             -- discounts for Renewal                                                                                                
   @NOTLOSSFREE          AS NOTLOSSFREE,        -- discountsfor Renewal                                      
   @VALUEDCUSTOMER                AS VALUEDCUSTOMER,       -- discountsfor Renewal                                                                                                 
   @MULTIPLEPOLICYFACTOR   AS MULTIPLEPOLICYFACTOR,                                                                            
   @NONSMOKER            AS NONSMOKER,                                                                         
   @EXPERIENCE      AS EXPERIENCE,                                              
   @CONSTRUCTIONCREDIT     AS CONSTRUCTIONCREDIT,                                                        
   ISNULL(@REDUCTION_IN_COVERAGE_C,'N')     AS REDUCTION_IN_COVERAGE_C,    --for nidhi                                                                                               
   @N0_LOCAL_ALARM              AS N0_LOCAL_ALARM,                                                           
   @BURGLER_ALERT_POLICE        AS BURGLER_ALERT_POLICE,                                                                   
   @FIRE_ALARM_FIREDEPT         AS FIRE_ALARM_FIREDEPT,          
@BURGLAR      AS BURGLAR,       
   --'51'           AS BURGLAR_ACORD,    -- NOT REQUIRED                                                           
   @CENTRAL_FIRE           AS CENTRAL_FIRE,                                                                                     
   @CENT_ST_BURG_FIRE      AS CENT_ST_BURG_FIRE,                                                                                        
   @DIR_FIRE_AND_POLICE    AS DIR_FIRE_AND_POLICE,                                                   
   @LOC_FIRE_GAS           AS LOC_FIRE_GAS,                                                  
   @TWO_MORE_FIRE          AS TWO_MORE_FIRE,                                                                        
   @INSURANCESCORE         AS INSURANCESCORE,                                                                        
   @INSURANCESCOREDIS   AS INSURANCESCOREDIS,                                                                                             
   @WOODSTOVE_SURCHARGE    AS WOODSTOVE_SURCHARGE,                                                                 
   @PRIOR_LOSS_SURCHARGE        AS PRIOR_LOSS_SURCHARGE,                                                                                             
   CONVERT(VARCHAR(10),@NOPETS) AS DOGSURCHARGE, --for nidhi                             
   @DOGFACTOR           AS DOGFACTOR,                                                                
                                                         
 --Inland Marine--                  
                                     
   @SCH_BICYCLE_DED           AS SCH_BICYCLE_DED,                                                                        
   @SCH_BICYCLE_AMOUNT       AS SCH_BICYCLE_AMOUNT,                                                                                                
   @SCH_CAMERA_DED           AS SCH_CAMERA_DED,                                                                         
   @SCH_CAMERA_AMOUNT        AS SCH_CAMERA_AMOUNT,                                                                                                
   @SCH_CELL_DED              AS SCH_CELL_DED,                                                                         
   @SCH_CELL_AMOUNT           AS SCH_CELL_AMOUNT,                     
   @SCH_FURS_DED              AS SCH_FURS_DED,                 
   @SCH_FURS_AMOUNT           AS SCH_FURS_AMOUNT,                                                                                                
   @SCH_GUNS_DED   AS SCH_GUNS_DED,                                                                 
   @SCH_GUNS_AMOUNT           AS SCH_GUNS_AMOUNT,                                               
   @SCH_GOLF_DED             AS SCH_GOLF_DED,                                                                                
   @SCH_GOLF_AMOUNT           AS SCH_GOLF_AMOUNT,                                                                                                
   @SCH_JWELERY_DED           AS SCH_JWELERY_DED,                                                                                                
   @SCH_JWELERY_AMOUNT       AS SCH_JWELERY_AMOUNT,                                                                                        
   @SCH_MUSICAL_DED          AS SCH_MUSICAL_DED,                                                                                   
   @SCH_MUSICAL_AMOUNT        AS SCH_MUSICAL_AMOUNT,                                                      
   @SCH_PERSCOMP_DED          AS SCH_PERSCOMP_DED,                        
   @SCH_PERSCOMP_AMOUNT       AS SCH_PERSCOMP_AMOUNT,                        
   @SCH_SILVER_DED            AS SCH_SILVER_DED,           
   @SCH_SILVER_AMOUNT        AS SCH_SILVER_AMOUNT,                                         
   @SCH_STAMPS_DED           AS SCH_STAMPS_DED,                        
   @SCH_STAMPS_AMOUNT        AS SCH_STAMPS_AMOUNT,                                 
   @SCH_RARECOINS_DED        AS SCH_RARECOINS_DED,                                             
   @SCH_RARECOINS_AMOUNT     AS SCH_RARECOINS_AMOUNT,                                                
   @SCH_FINEARTS_WO_BREAK_DED       AS SCH_FINEARTS_WO_BREAK_DED,                                                                                                
   @SCH_FINEARTS_WO_BREAK_AMOUNT    AS SCH_FINEARTS_WO_BREAK_AMOUNT,                                       
   @SCH_FINEARTS_BREAK_DED          AS SCH_FINEARTS_BREAK_DED,                                                                                                
   @SCH_FINEARTS_BREAK_AMOUNT       AS SCH_FINEARTS_BREAK_AMOUNT,                                                                           
    --Inland Marine--            
                                                                                 
   @SCH_BICYCLE_DED          AS SCH_BICYCLE_DED,        
   @SCH_BICYCLE_AMOUNT      AS SCH_BICYCLE_AMOUNT,                                                                    
   @SCH_CAMERA_DED          AS SCH_CAMERA_DED,                                                                          
   @SCH_CAMERA_AMOUNT       AS SCH_CAMERA_AMOUNT,                    
   @SCH_CELL_DED             AS SCH_CELL_DED,                              
   @SCH_CELL_AMOUNT          AS SCH_CELL_AMOUNT,                                                
   @SCH_FURS_DED             AS SCH_FURS_DED,                                                                                              
   @SCH_FURS_AMOUNT          AS SCH_FURS_AMOUNT,                                         
   @SCH_GUNS_DED             AS SCH_GUNS_DED,                                            
   @SCH_GUNS_AMOUNT          AS SCH_GUNS_AMOUNT,                                                                          
   @SCH_GOLF_DED            AS SCH_GOLF_DED,                                                  
   @SCH_GOLF_AMOUNT          AS SCH_GOLF_AMOUNT,                                                                                 
   @SCH_JWELERY_DED          AS SCH_JWELERY_DED,                                                             
   @SCH_JWELERY_AMOUNT      AS SCH_JWELERY_AMOUNT,                                                
   @SCH_MUSICAL_DED         AS SCH_MUSICAL_DED,                             
   @SCH_MUSICAL_AMOUNT       AS SCH_MUSICAL_AMOUNT,                                                       
   @SCH_PERSCOMP_DED         AS SCH_PERSCOMP_DED,                                                                                                                
   @SCH_PERSCOMP_AMOUNT      AS SCH_PERSCOMP_AMOUNT,                                                                                                                
   @SCH_SILVER_DED           AS SCH_SILVER_DED,           
   @SCH_SILVER_AMOUNT       AS SCH_SILVER_AMOUNT,                                                                                 
   @SCH_STAMPS_DED           AS SCH_STAMPS_DED,                                                                                      
   @SCH_STAMPS_AMOUNT        AS SCH_STAMPS_AMOUNT,                                                                                                                
   @SCH_RARECOINS_DED    AS SCH_RARECOINS_DED,                                      
   @SCH_RARECOINS_AMOUNT     AS SCH_RARECOINS_AMOUNT,                                                       
   @SCH_FINEARTS_WO_BREAK_DED      AS SCH_FINEARTS_WO_BREAK_DED,         
   @SCH_FINEARTS_WO_BREAK_AMOUNT    AS SCH_FINEARTS_WO_BREAK_AMOUNT,                                                                                                                
   @SCH_FINEARTS_BREAK_DED AS SCH_FINEARTS_BREAK_DED,                                                                                    
@SCH_FINEARTS_BREAK_AMOUNT       AS SCH_FINEARTS_BREAK_AMOUNT,                                                                                                                
   @SCH_HANDICAP_ELECTRONICS_DED      as SCH_HANDICAP_ELECTRONICS_DED,                                              
 @SCH_HANDICAP_ELECTRONICS_AMOUNT     as SCH_HANDICAP_ELECTRONICS_AMOUNT,                                        
         @SCH_HEARING_AIDS_DED as  SCH_HEARING_AIDS_DED,                                        
           @SCH_HEARING_AIDS_AMOUNT as SCH_HEARING_AIDS_AMOUNT,                                        
@SCH_INSULIN_PUMPS_DED  as   SCH_INSULIN_PUMPS_DED,                                        
           @SCH_INSULIN_PUMPS_AMOUNT  as SCH_INSULIN_PUMPS_AMOUNT,                                        
  @SCH_MART_KAY_DED  as   SCH_MART_KAY_DED,                                        
       @SCH_MART_KAY_AMOUNT  as SCH_MART_KAY_AMOUNT,                                        
           @SCH_PERSONAL_COMPUTERS_LAPTOP_DED as  SCH_PERSONAL_COMPUTERS_LAPTOP_DED,                                          
           @SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT    as SCH_PERSONAL_COMPUTERS_LAPTOP_AMOUNT,                                          
           @SCH_SALESMAN_SUPPLIES_DED     as SCH_SALESMAN_SUPPLIES_DED,                                        
@SCH_SALESMAN_SUPPLIES_AMOUNT    as SCH_SALESMAN_SUPPLIES_AMOUNT,                                        
           @SCH_SCUBA_DRIVING_DED  as SCH_SCUBA_DRIVING_DED,                                        
           @SCH_SCUBA_DRIVING_AMOUNT  as SCH_SCUBA_DRIVING_AMOUNT,                                        
    @SCH_SNOW_SKIES_DED      as  SCH_SNOW_SKIES_DED,                                        
          @SCH_SNOW_SKIES_AMOUNT      as  SCH_SNOW_SKIES_AMOUNT,                                          
   @SCH_TACK_SADDLE_DED as SCH_TACK_SADDLE_DED,                                                 
   @SCH_TACK_SADDLE_AMOUNT as SCH_TACK_SADDLE_AMOUNT,                                         
          @SCH_TOOLS_PREMISES_DED    as SCH_TOOLS_PREMISES_DED,                                        
          @SCH_TOOLS_PREMISES_AMOUNT      as SCH_TOOLS_PREMISES_AMOUNT,                                         
          @SCH_TOOLS_BUSINESS_DED   as SCH_TOOLS_BUSINESS_DED,                                         
          @SCH_TOOLS_BUSINESS_AMOUNT     as SCH_TOOLS_BUSINESS_AMOUNT ,                                       
   @SCH_TRACTORS_DED    as  SCH_TRACTORS_DED,                                          
           @SCH_TRACTORS_AMOUNT   as SCH_TRACTORS_AMOUNT,                                        
           @SCH_TRAIN_COLLECTIONS_DED as SCH_TRAIN_COLLECTIONS_DED,                                        
           @SCH_TRAIN_COLLECTIONS_AMOUNT   as SCH_TRAIN_COLLECTIONS_AMOUNT,                                        
          @SCH_WHEELCHAIRS_DED  as SCH_WHEELCHAIRS_DED,                  
          @SCH_WHEELCHAIRS_AMOUNT   as   SCH_WHEELCHAIRS_AMOUNT,        
 isnull(@SCH_CAMERA_PROF_AMOUNT,'0')    as     SCH_CAMERA_PROF_AMOUNT,      
 isnull(@SCH_CAMERA_PROF_DED,'0') as  SCH_CAMERA_PROF_DED,        
 isnull(@SCH_MUSICAL_REMUN_AMOUNT,'0') as SCH_MUSICAL_REMUN_AMOUNT,       
 isnull(@SCH_MUSICAL_REMUN_DED,'0') as  SCH_MUSICAL_REMUN_DED,       
 isnull(@MINESUBSIDENCEADDITIONAL,'0') AS MINESUBSIDENCE_ADDITIONAL,                                                                   
 --Rest--                     
                                                                                
   @TERRITORYCODES       AS TERRITORYCODES,                                                @EARTHQUAKEZONE AS  EARTHQUAKEZONE,                                     
 @COVERAGEVALUE        AS COVERAGEVALUE,                                                                                     
   --'0'                 AS TEMPERATURE,      -- Not Required                                                                    
 --'0'      AS SMOKE,   -- Not Required                                                                                                 
   @DWELLING            AS DWELLING,                                         
   '0'              AS YEARS,                                                                                     
   --'N'             AS CHIMNEYSTOVE,     -- Not Required                                                              
   --'0'             AS TERRITORYCODES1,  -- Not Required                                                         
   @PRODUCT_PREMIER      AS PREMIUMGROUP ,    -- ( Fetching values from Sub LOB ID)                                      
   @OTHERSTRUCTURES_LIMIT      AS OTHERSTRUCTURES_LIMIT,         
   @PERSONALPROPERTY_LIMIT     AS PERSONALPROPERTY_LIMIT  ,                              
   @AGEHOMECREDIT             AS AGEHOMECREDIT ,                                                                                  
   @POLICYTYPE               AS POLICYTYPE   ,                                    
   @VALUESCUSTOMERAPP        AS VALUESCUSTOMERAPP,                                                                                
  --Hard Coded To be removed                                                                                  
    @PRIORLOSSSURCHARGE                         AS PRIORLOSSSURCHARGE,                                                            
    @TERRITORYNAME  AS TERRITORYNAME,                                                            
    @TERRITORYCOUNTY AS TERRITORYCOUNTY,                             
    @BREEDOFDOG      AS BREEDOFDOG,                           
    @ISACTIVE       AS   ISACTIVE,      
 ISNULL(@SUBPROTDIS,'N')  AS SUBPROTDIS,      
 ISNULL(@YEARSCONTINSURED,'0')  AS YEARSCONTINSURED,      
 ISNULL(@YEARSCONTINSUREDWITHWOLVERINE,'0') AS YEARSCONTINSUREDWITHWOLVERINE,      
 @TOTALLOSS AS TOTALLOSS                                                          
                                                                                        
   ---'0'        AS LOSSOFUSE_LIMIT,                                                                                          
   --'0'             AS COVERAGEFACTOR,    -- Not Required                                                                                                
   --'0'             AS BASEPREMIUM,       -- Not Required                                                    
   --'N'             AS CLAIMS,            -- Not Required                                                                                      
   --'0'             AS AMOUNT ,           -- Not Required                                                                        
                                                                                             
--@COVERAGEVALUE      AS COVERAGEVALUE                                        
--SELECT                                                                                                
--@COVERAGEVALUE        AS @COVERAGEVALUE                                                                                                  
                                                                                                
 END                             
SET    quoted_identifier ON 