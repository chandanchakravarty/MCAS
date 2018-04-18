IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_Vehcile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_Vehcile]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                        
Proc Name                : Dbo.Proc_GetPPARule_Vehcile                                                                      
Created by               : Ashwani                                                                        
Date                     : 14 Nov.,2005                                        
Purpose                  : To get the Auto detail info for Rules                                        
Revison History          :                                                   
MODIFIED by              : Pravesh K chandel                                                                        
Date                     : 17 july,2008                                        
Purpose                  : Implement Extra equipment/other then collision covg Rules         

MODIFIED by              : Praveen Kasana                                                                        
Date                     : 30 sep,2009                                        
Purpose                  : Implement Itrack 5823                                   
                                                              
                               
Used In                  : Wolverine                                                                        
------------------------------------------------------------                                                                        
Date     Review By          Comments                  
DROP PROC dbo.Proc_GetPPARule_Vehcile 864,327,1,1,'submit'          
------   ------------       -------------------------*/                                                                        
Create proc [dbo].[Proc_GetPPARule_Vehcile]                                                                     
(                                                                        
 @CUSTOMERID    int,           
 @APPID    int,                                                                        
 @APPVERSIONID   int,                                                              
 @VEHICLEID int ,                                  
 @DESC varchar(10)                                          
)                                                                        
AS                                                                            
BEGIN                                                                     
-- Vehicle Info                                                 
 DECLARE @VEHICLE_YEAR VARCHAR(4)                                                
 DECLARE @MAKE NVARCHAR(75)                                                
 DECLARE @MODEL NVARCHAR(75)                                                
 DECLARE @VIN NVARCHAR(75)                                          
 DECLARE @VEHICLE_USE NVARCHAR(5)                                          
 DECLARE @GRG_ADD1 NVARCHAR(70)                                          
 DECLARE @GRG_CITY  NVARCHAR(40)                                          
 DECLARE @GRG_STATE NVARCHAR(5)                                          
 DECLARE @GRG_ZIP VARCHAR(11)                                          
 DECLARE @AMOUNT  DECIMAL                              
 DECLARE @MAKE_NAME CHAR                                      
 DECLARE @USE_VEHICLE NVARCHAR(5)                           
 DECLARE @INTSYMBOL INT                    
 DECLARE @SYMBOL CHAR                
 DECLARE @VEHICLE_TYPE_PER VARCHAR(15)                    
 DECLARE @INTRADIUS_OF_USE INT              
 DECLARE @COMM_RADIUS_OF_USE CHAR              
 DECLARE @VEHICLE_TYPE_COM VARCHAR(10)              
 DECLARE @SNOWPLOW_CONDS VARCHAR(10)              
 DECLARE @CLASS_PER VARCHAR(12)              
 DECLARE @CLASS_COM VARCHAR(12)                 
 DECLARE @REGISTERED_STATE VARCHAR(10)          
 DECLARE @TRANSPORT_CHEMICAL VARCHAR(10)          
 DECLARE @COVERED_BY_WC_INSU VARCHAR(10)          
 DECLARE @AGE VARCHAR(20)          
    DECLARE @ANTIQUE_VECH VARCHAR(20)          
 set @COMM_RADIUS_OF_USE='N'          
 DECLARE @QUOTEEFFECTIVEDATE VARCHAR(20)              
 DECLARE @APP_INCEPTION_DATE VARCHAR(20)          
 DECLARE @MILES_TO_WORK INT          
 DECLARE @IS_SUSPENDED INT        
--ADDED PRAVEEN KUMAR(05-03-2009):ITRACK 5544    
 DECLARE @AUTO_POLICY_NO VARCHAR(12)    
 SET @AUTO_POLICY_NO = 'N'    
 DECLARE @TRAILBLAZER_EXPIRY_DATE DATETIME
 SET @TRAILBLAZER_EXPIRY_DATE = '01/01/2010'
--END              
            
SELECT @QUOTEEFFECTIVEDATE = APP_EFFECTIVE_DATE,@APP_INCEPTION_DATE=APP_INCEPTION_DATE  FROM APP_LIST WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                                          
IF EXISTS (SELECT CUSTOMER_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID)                                                
BEGIN                   
 SELECT @VEHICLE_YEAR=ISNULL(VEHICLE_YEAR,''),@MAKE=ISNULL(UPPER(MAKE),''),@MODEL=ISNULL(UPPER(MODEL),''),@VIN=ISNULL(VIN,''),                                          
 @VEHICLE_USE=ISNULL(VEHICLE_USE,''),                                          
 @GRG_ADD1=ISNULL(GRG_ADD1,''),@GRG_CITY=ISNULL(GRG_CITY,''),@GRG_STATE=ISNULL(GRG_STATE,''),@GRG_ZIP=ISNULL(GRG_ZIP,''),                                          
 @AMOUNT=ISNULL(AMOUNT,-1),@USE_VEHICLE=ISNULL(USE_VEHICLE,''),@INTSYMBOL=ISNULL(SYMBOL,0),@VEHICLE_TYPE_PER=ISNULL(VEHICLE_TYPE_PER,'0'),              
 @INTRADIUS_OF_USE = ISNULL(RADIUS_OF_USE,0),@VEHICLE_TYPE_COM=ISNULL(VEHICLE_TYPE_COM,''),@SNOWPLOW_CONDS=ISNULL(SNOWPLOW_CONDS,''),              
 @CLASS_PER=ISNULL(CLASS_PER,''),@CLASS_COM=ISNULL(CLASS_COM,''),@REGISTERED_STATE=REGISTERED_STATE,          
 @TRANSPORT_CHEMICAL=ISNULL(TRANSPORT_CHEMICAL,''),@COVERED_BY_WC_INSU=ISNULL(COVERED_BY_WC_INSU,''),          
 @AGE=DATEDIFF(YEAR,ISNULL(VEHICLE_YEAR,'0'),@QUOTEEFFECTIVEDATE),          
 @MILES_TO_WORK=isnull(MILES_TO_WORK,0),          
 @IS_SUSPENDED=ISNULL(IS_SUSPENDED,0)           
FROM APP_VEHICLES                                                  
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID                
END                
          
------------------------------------------------------------          
-- if age of vehicle is greater than 15 years then refer it to underwriter          
--          
DECLARE @DATEDIFF INT          
SELECT @DATEDIFF=(DATEDIFF(year,@VEHICLE_YEAR,@QUOTEEFFECTIVEDATE))           
IF(@DATEDIFF>0)          
BEGIN          
 IF(@AGE > 15 and @IS_SUSPENDED=10964)  --Condition added by Sibin for Itrack 5512 on 2 March 09        
  BEGIN          
   SET @ANTIQUE_VECH='Y'          
  END          
 ELSE          
  BEGIN          
   SET @ANTIQUE_VECH='N'          
  END           
END          
ELSE          
 BEGIN          
  SET @ANTIQUE_VECH='N'          
 END            
          
-------rule added by pravesh on 17 sep for GF Antique car Itrack 4522          
--Classic & Antique car - Grandfathered, for existing policies           
--only where the inception date is prior to 01/01/2003          
--11869 -antique car          
declare @ANTIQUE_CLASSIC_CAR CHAR(1)          
SET @ANTIQUE_CLASSIC_CAR='N'          
IF ((@VEHICLE_TYPE_PER='11869' OR @VEHICLE_TYPE_PER='11868' )AND YEAR(@APP_INCEPTION_DATE)>2002 )          
 BEGIN          
  SET @ANTIQUE_CLASSIC_CAR='Y'          
 END          
----------          
------------------------------------------------------------------------------------------              
-- Vehicle Info TabIf Vehicle Use is  commercial Radius of Use?* If number is greater than 150               
-- Refer to underwriters               
 IF(@USE_VEHICLE='11333' AND @INTRADIUS_OF_USE>150)              
 BEGIN                   
 SET @COMM_RADIUS_OF_USE='Y'              
 END              
                    
--------------------------------------------------------------------------------------------               
-- If Vehicle Use is Commercial If Vehicle type is Local Haul  If the  Radius Field is greater than  75 miles               
--Refer to Underwriters              
DECLARE @COMM_LOCALHAUL CHAR              
SET @COMM_LOCALHAUL='N'              
          
IF(@USE_VEHICLE='11333' AND @INTRADIUS_OF_USE>75 AND @VEHICLE_TYPE_COM='11339')              
BEGIN               
SET @COMM_LOCALHAUL='Y'              
END                                      
---------------------------------------------------------------------------------------------              
-- If Vehicle Use is Commercial If Vehicle type is Intermediate Haul then               
-- If the Radius Field  is not with 75 -150 miles Refer to Underwriters               
DECLARE @COMM_INTER_LH CHAR              
SET @COMM_INTER_LH='N'              
          
IF(@USE_VEHICLE='11333' AND @INTRADIUS_OF_USE NOT BETWEEN 75 AND 150 AND @VEHICLE_TYPE_COM='11338')              
BEGIN               
SET @COMM_INTER_LH='Y'              
END                                 
              
---------------------------------------------------------------------------------------------              
-- Vehicle Info Tab If the Model is a Corvette on any of the automobiles in the lister               
-- Look at the Underwriting Question Is multi policy discount applied? If No Refer to Underwriters                
DECLARE @MODEL_NAME_MULTI_DIS CHAR              
SET @MODEL_NAME_MULTI_DIS ='N'              
          
IF EXISTS (SELECT CUSTOMER_ID FROM APP_AUTO_GEN_INFO               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID               
AND @MODEL IN ('CORVETTE','CORVETTE Z06')AND MULTI_POLICY_DISC_APPLIED='0')              
BEGIN               
SET @MODEL_NAME_MULTI_DIS='Y'            
END              
              
                                     
-- -----------------------------------------------                    
--Vehicle Info Tab If the make field for any of the vehicles in the lister are any of the following               
--Delorean, Ferrari, GEM, Lamborghini, Maserati, Rantera or Shelby Cobra              
--Then Refer to Underwriters               
                                
IF(@MAKE='DELOREAN' OR @MAKE='FERRARI' OR @MAKE='GEM' OR @MAKE='LAMBORGHINI'                                    
OR @MAKE='MASERATI' OR  @MAKE='RANTERA' OR  @MAKE='SHELBY COBRA')                                      
BEGIN                                       
SET @MAKE_NAME='Y'                                      
END                                       
ELSE                                      
BEGIN                                  
SET @MAKE_NAME='N'                                      
END                          
                        
              
---Rule Amount > 80000            
                        
declare @VEHICLE_AMOUNT char                        
                  
--check for Personal Vehicle Type                
--Amount field is not mandatory in case of Private Passenger Vehicle - 11334                
IF(@VEHICLE_TYPE_PER=11334 AND (@AMOUNT=-1 OR @AMOUNT =0 OR @AMOUNT IS NULL))                
SET @VEHICLE_AMOUNT='N'                    
ELSE IF(@AMOUNT>80000.00)                        
BEGIN                         
SET @VEHICLE_AMOUNT ='Y'                        
END                         
ELSE IF(@AMOUNT=-1 OR @AMOUNT =0 OR @AMOUNT IS NULL)                        
BEGIN                         
SET @VEHICLE_AMOUNT=''                        
END                       
ELSE                  
SET @VEHICLE_AMOUNT='N'                    
--3) Vehicle Information Tab , If Vehicle type is Personal               
--   If the Amount Field is greater than $80,000 ,then Refer to Underwriters               
IF(@USE_VEHICLE='11332' AND @VEHICLE_AMOUNT='Y')              
BEGIN               
SET @VEHICLE_AMOUNT='Y'              
END               
-------------------------------------------------------------------------------------              
--4) Vehicle Information Tab , If Vehicle type is Commercial               
-- If the Amount Field is greater than $50,000 , then Refer to Underwriters               
DECLARE @VEHICLE_AMOUNT_COM CHAR              
SET @VEHICLE_AMOUNT_COM='N'              
          
IF(@USE_VEHICLE='11333' AND @AMOUNT>50000.00)              
BEGIN               
SET @VEHICLE_AMOUNT_COM='Y'            
END               
----------------------------------------------------------------------------------------------                    
IF(@VEHICLE_TYPE_PER='0' OR @VEHICLE_TYPE_PER='')              
BEGIN               
SET @VEHICLE_TYPE_PER=''              
END               
          
--for symbol                     
IF(@INTSYMBOL>26)                       
BEGIN                     
SET @SYMBOL='Y'                    
END           
ELSE IF(@INTSYMBOL=0 and @VEHICLE_TYPE_PER NOT IN (11868,11869))                    
BEGIN                     
SET @SYMBOL=''                    
END           
ELSE IF(@VEHICLE_TYPE_PER NOT IN (11868,11869)AND @INTSYMBOL=0)          
BEGIN          
SET @SYMBOL='N'                    
END                    
ELSE IF(@INTSYMBOL<>0)                    
BEGIN                     
SET @SYMBOL='N'                    
END                
-----------------------------------------------------------------------------------------------              
/*If State is Indiana On vehicle   coverage tab If No in the Signature Field   for any of these coverages       
  Refer to underwriter               
  Underinsured Motorist (CSL) Uninsured Motorist (CSL)               
  Underinsured Motorist (PD)  Uninsured Motorist (PD)              
  Underinsured Motorist (BI)  Uninsured Motorist (BI)*/               
              
--1. Uninsured Motorist (CSL) -- 9              
DECLARE @PUNCS CHAR              
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                         
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (9) AND SIGNATURE_OBTAINED='N')               
BEGIN               
SET @PUNCS='Y'              
END              
ELSE              
BEGIN               
SET @PUNCS='N'              
END               
              
--2 Underinsured Motorist (CSL) -- 14              
DECLARE @UNCSL CHAR              
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                         
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (14) AND SIGNATURE_OBTAINED='N')               
BEGIN           
SET @UNCSL='Y'              
END              
ELSE              
BEGIN               
SET @UNCSL='N'              
END               
              
--3 Underinsured Motorist (PD) -- Not at Screen               
              
--4 Uninsured Motorist (PD) -- 36              
DECLARE @UMPD CHAR              
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                         
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (36) AND SIGNATURE_OBTAINED='N')               
BEGIN               
SET @UMPD='Y'              
END              
ELSE              
BEGIN               
SET @UMPD='N'              
END               
              
--5 Underinsured Motorist (BI)-- 34              
DECLARE @UNDSP CHAR              
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES           
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (34) AND SIGNATURE_OBTAINED='N')               
BEGIN               
SET @UNDSP='Y'              
END              
ELSE              
BEGIN               
SET @UNDSP='N'              
END               
          
--6 Uninsured Motorist (BI)-- 12              
DECLARE @PUMSP CHAR              
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                         
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (12) AND SIGNATURE_OBTAINED='N')               
BEGIN               
SET @PUMSP='Y'              
END              
ELSE              
BEGIN               
SET @PUMSP='N'              
END                    
-------------------------------------------------------------------------------------------------                  
-- 333. If on the Vehicle Coverage Tab Loan/Lease Gap coverage is checked off for any of the vehicles in the lister               
-- Then this risk is submitted to underwriters                
 declare @LLGC  char              
IF EXISTS (SELECT SIGNATURE_OBTAINED               
FROM APP_VEHICLE_COVERAGES                         
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID  AND COVERAGE_CODE_ID IN (46,249))               
BEGIN               
--SET @LLGC ='Y'    new Itrack 4494 rule change to new rule          
SET @LLGC ='N'          
END              
ELSE              
BEGIN               
SET @LLGC ='N'              
END              
--------------------------------------------------------------------------------------------------------------------              
-- Indiana : Excluded Person(s) Endorsement A-96 If this is checked off on the Vehicle Coverage Tab               
-- then in the verify process make sure we have a least one Excluded driver and               
-- also Sig obtained field for this coverage is "Y" --               
--Changed by Pravesh on 19 sep08 and implement for both state Itrack 4773          
DECLARE @EPENDO_SIGN CHAR              
SET  @EPENDO_SIGN='N' -- DEFAULT              
          
IF ( (   EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES             
    WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID              
    AND COVERAGE_CODE_ID IN (1010,1002) --AND SIGNATURE_OBTAINED='N'          
    )              
  AND            
  NOT EXISTS(SELECT DRIVER_ID FROM APP_DRIVER_DETAILS               
     WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID            
     AND DRIVER_DRIV_TYPE=3477  AND isnull(FORM_F95,0)= 10963 AND isnull(IS_ACTIVE,'Y')='Y')               
  )          
  OR           
       (          
   EXISTS(SELECT DRIVER_ID FROM APP_DRIVER_DETAILS               
     WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID            
     AND DRIVER_DRIV_TYPE=3477 AND isnull(FORM_F95,0)= 10963 AND isnull(IS_ACTIVE,'Y')='Y')              
  AND          
  NOT EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                         
    WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID              
    AND COVERAGE_CODE_ID IN (1010,1002) --AND SIGNATURE_OBTAINED='N'          
    )              
  )          
 )          
BEGIN           
 SET @EPENDO_SIGN='Y'              
END            
IF(@VEHICLE_TYPE_COM IN (11340,11341) OR @VEHICLE_TYPE_PER IN (11337,11870))          
BEGIN           
 SET @EPENDO_SIGN='N'              
END          
----------------------------------------------------               
-- If State is Indiana Then look at the Vehicle Infor Tab for all vehicles on the policy If the Registered State is               
-- other then Indiana on any of the vehicles than Submit               
 DECLARE @STATE_ID VARCHAR(30)              
 DECLARE @REG_STATE CHAR              
 SET @REG_STATE ='N'              
               
              
 SELECT @STATE_ID = CONVERT(VARCHAR(30),STATE_ID) FROM APP_LIST               
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID              
           
 IF (@STATE_ID='14')              
 BEGIN               
 IF EXISTS( SELECT REGISTERED_STATE FROM APP_VEHICLES               
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID               
 AND VEHICLE_ID=@VEHICLEID AND  @REGISTERED_STATE<>'14')              
 BEGIN               
 SET @REG_STATE='Y'              
 END              
 END              
           
           
 IF(@REGISTERED_STATE<>@STATE_ID)              
 BEGIN               
 SET @REG_STATE='Y'              
 END              
-----------------------------------------------------------------------------------------              
-- Vehicle Info Tab Territory Field The territory is based on the Garaging Location Fields               
-- If Class 5C (11357) Then look at the Vehicle Info Tab for Garaging Location / Registered State Field               
-- must equal the State Field on the Application/Policy Details Tab If Not then refer to Underwriters               
-- Class 5C chk removed              
 DECLARE @CREG_STATE CHAR               
 SET @CREG_STATE='N'              
           
 IF((@GRG_STATE<>@REGISTERED_STATE OR @STATE_ID<>@REGISTERED_STATE))              
 BEGIN               
 SET @CREG_STATE='Y'              
 END            
-- Garaging state of all vehicle must be same as of main application state Reject in this case             
 DECLARE @GRG_STATE_RULE CHAR       
 IF(@GRG_STATE<>@STATE_ID)          
 BEGIN          
 SET @GRG_STATE_RULE='Y'          
 END              
              
              
-----------------------------------------------------------------------------------------              
--If State is Michigan               
--If Vehicle Type is Motor Home, Truck or Van Camper               
--If Yes to Is this Vehicle used for Business or Permanent Residence? then Submit to Underwriters               
              
 DECLARE @VEHICLE_TYPE_MHT CHAR              
 SET @VEHICLE_TYPE_MHT = 'N'              
           
 IF(@STATE_ID='22' OR @STATE_ID='14')              
 BEGIN               
 IF EXISTS (SELECT VEHICLE_TYPE_PER FROM APP_VEHICLES               
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID               
 AND VEHICLE_ID = @VEHICLEID AND VEHICLE_TYPE_PER='11336' AND BUSS_PERM_RESI=10963)              
 BEGIN               
 SET @VEHICLE_TYPE_MHT='Y'              
 END              
 END               
-----------------------------------------------------------------------------------------              
              
              
-----------------------------------------------------------------------------------------              
              
-- 4) If Vehicle Use is  commercial , Vehicle Type Is Long Haul Send message in Red "Do not write this type of Risk"              
-- Refer to underwriters if still submitted               
 DECLARE @COMM_LONGHAUL CHAR              
 SET @COMM_LONGHAUL ='N'              
           
 IF(@USE_VEHICLE = '11333' AND @VEHICLE_TYPE_COM='11871') --COMM.              
 BEGIN               
 SET @COMM_LONGHAUL='Y'              
 END              
-----------------------------------------------------------------------------------------              
-- 11272              
-- SNOWPLOW_CONDS --11912              
--Application/Policy Details If State is Indiana Vehicle Info Tab If Use is Snowplowing               
--Then look at the Snowplow conditions If Full time - Refer to Underwriters                
 DECLARE @INSNOW_FULL CHAR              
 SET @INSNOW_FULL='N'              
           
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_VEHICLES               
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID               
 AND VEHICLE_ID = @VEHICLEID AND SNOWPLOW_CONDS='11912'               
 AND VEHICLE_USE='11272' AND (@STATE_ID=14 or @STATE_ID=22 ) )       --add condition for state michigan on 3 july 2008 by pravesh as per itrack 4446          
 BEGIN               
 SET @INSNOW_FULL='Y'              
 END            
-----------------------------------------------------------------------------------------              
-- There should be atleast one "Pricipal" or "Youthful Principal" driver for each vehicle added in application. Else Refer.              
 -- is active check applied itrack 5539             
 DECLARE @YOUTHFUL_PRINC_DRIVER CHAR              
 SET @YOUTHFUL_PRINC_DRIVER='N'              
           
 IF ( NOT EXISTS(SELECT ADAV.APP_VEHICLE_PRIN_OCC_ID               
    FROM APP_DRIVER_ASSIGNED_VEHICLE  ADAV       
 INNER JOIN APP_DRIVER_DETAILS ADDS      
 ON  ADAV.CUSTOMER_ID= ADDS.CUSTOMER_ID       
  AND ADAV.APP_ID=  ADDS.APP_ID      
  AND ADAV.APP_VERSION_ID=ADDS.APP_VERSION_ID      
  AND ADAV.DRIVER_ID=ADDS.DRIVER_ID                 
    WHERE ADAV.CUSTOMER_ID=@CUSTOMERID AND  ADAV.APP_ID=@APPID  AND  ADAV.APP_VERSION_ID=@APPVERSIONID              
  AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929) AND ADDS.IS_ACTIVE='Y' AND ADAV.VEHICLE_ID=@VEHICLEID          
    )          
  AND @VEHICLE_TYPE_PER NOT IN ('11618','11337','11870') AND @VEHICLE_TYPE_COM !='11341'          
  )          
 BEGIN               
 SET @YOUTHFUL_PRINC_DRIVER='Y'              
 END               
-----------------------------------------------------------------------------------------            
--------------------------------------------------------------------------------          
--      Vehicle must have one prin OR youthOccas OR YuthPrin(Start)          
--------------------------------------------------------------------------------          
DECLARE @PRINC_OCCA_DRIVER NVARCHAR(50)          
SET @PRINC_OCCA_DRIVER = 'N'          
IF (EXISTS(SELECT ADAV.APP_VEHICLE_PRIN_OCC_ID                 
  FROM APP_DRIVER_ASSIGNED_VEHICLE ADAV          
  INNER JOIN APP_DRIVER_DETAILS AD ON AD.CUSTOMER_ID=ADAV.CUSTOMER_ID           
  AND AD.APP_ID=ADAV.APP_ID AND AD.APP_VERSION_ID=ADAV.APP_VERSION_ID           
  AND AD.DRIVER_ID=ADAV.DRIVER_ID AND ISNULL(AD.IS_ACTIVE,'Y')='Y'          
          
  WHERE ADAV.CUSTOMER_ID=@CUSTOMERID AND  ADAV.APP_ID=@APPID  AND  ADAV.APP_VERSION_ID=@APPVERSIONID                
  AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929,11928,11927)AND ADAV.VEHICLE_ID=@VEHICLEID          
  )                
 OR @VEHICLE_TYPE_PER IN ('11618','11337','11870') OR @VEHICLE_TYPE_COM ='11341'          
 )          
BEGIN                 
 SET @PRINC_OCCA_DRIVER='Y'                
END            
--------------------------------------------------------------------------------          
--      Vehicle must have one prin, youthOccas,YuthPrin(End)          
--------------------------------------------------------------------------------            
-- For Personal Auto, it is mandatory only in case of 'Private Pasenger' and  'Motor home'.              
--11336- Motor home, Truck or Van Campers ,11334- Private Passenger               
              
 IF(@CLASS_PER='0' AND @USE_VEHICLE='11332' AND (@VEHICLE_TYPE_PER='11336'OR @VEHICLE_TYPE_PER='11334'))              
 SET @CLASS_PER= ''              
ELSE              
 SET @CLASS_PER= 'N'              
              
              
--For Commercial Auto, it is mandatory for all except 'Trailer' -              
 IF(@CLASS_COM='0' AND @USE_VEHICLE='11333' AND @VEHICLE_TYPE_COM<>'11341' )     
 SET @CLASS_COM=''              
 ELSE              
 SET @CLASS_COM='N'              
           
            
--=========================================================================          
  --Grandfather case for Territory Code          
--=========================================================================          
/*DECLARE @APP_EFF_DATE DATETIME          
SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID              
          
SELECT TOP 1 MTC.TERR AS TERRITORY_DESCRIPTION FROM APP_VEHICLES AVC                                            
 INNER JOIN MNT_TERRITORY_CODES MTC  ON  AVC.GRG_ZIP=MTC.ZIP  AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=2          
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID           
 AND NOT(@APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')            
 AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')            
 )*/           
          
 DECLARE @APP_EFF_DATE DATETIME ,           
  @TERR Int,          
  @GRANDFATHER_TERRITORY VARCHAR          
            
 SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID          
          
 SELECT @TERR = TERRITORY FROM APP_VEHICLES AVC          
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID          
          
 IF EXISTS (           
  SELECT MTC.* FROM APP_VEHICLES AVC                                            
  INNER JOIN MNT_TERRITORY_CODES MTC            
  ON  AVC.TERRITORY = MTC.TERR AND SUBSTRING(AVC.GRG_Zip,0,6) = SUBSTRING(MTC.ZIP,0,6) AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=2          
  AND MTC.TERR = @TERR AND @APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')            
  AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')           
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID          
 )          
 BEGIN           
  SET @GRANDFATHER_TERRITORY='N'          
 END          
 ELSE          
 BEGIN           
  SET @GRANDFATHER_TERRITORY='Y'          
 END          
----> Mandatory Vehicle Use           
     IF(@USE_VEHICLE='11333')          
     BEGIN          
 SET @VEHICLE_USE='N'           
     END          
---> In case of  personal          
      IF(@USE_VEHICLE='11332')          
      BEGIN          
 SET @INTRADIUS_OF_USE=1          
 SET @TRANSPORT_CHEMICAL='N'            
      END          
---> If Vehicle Use personal and If commercial than State Not Michigan          
 IF(@USE_VEHICLE='11332' OR (@USE_VEHICLE='11333' AND @STATE_ID!='22' ))          
       BEGIN          
 SET @COVERED_BY_WC_INSU='N'          
 END           
          
-----------------------------------------------------------------------------------------           
--Added by Pravesh on 17 july 2008 as per iTRACK # 4511           
--3- If there is Miscellaneous Equipment and they have not selected Other than Collision on the Vehicle Coverage tab – Put a not in the Verification process           
--      No coverage has been applied to the Miscellaneous Equipment          
--42 OTC  Other than Collision (Comprehensive)          
--123 COMP Other Than Collision (Comprehensive)          
DECLARE @MIS_EQUIP_COUNT INT,@OT_COLL_COUNT INT, @MIS_EQUIP_COV CHAR(1)          
SELECT  @MIS_EQUIP_COUNT=COUNT(*) FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES           
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID AND ITEM_VALUE<>0          
SELECT  @OT_COLL_COUNT=COUNT(*) FROM APP_VEHICLE_COVERAGES           
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID      
 AND COVERAGE_CODE_ID IN (42,123)          
IF (@MIS_EQUIP_COUNT>0 AND @OT_COLL_COUNT=0)          
 SET @MIS_EQUIP_COV='Y'          
ELSE          
 SET @MIS_EQUIP_COV='N'            
----ADDED BY PRAVESH END S HERE       
--added by pravesh on 22 july 2008 Itrack 452          
DECLARE @HELTH_CARE CHAR(1),@ADD_INFORMATION  NVARCHAR(50)--116 PIP COVERAGE          
SET @HELTH_CARE='N'          
IF EXISTS(          
  SELECT CUSTOMER_ID FROM APP_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID           
  AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
  AND COVERAGE_CODE_ID IN (116) AND LIMIT_ID IN (687,688,1374,1375)          
  )          
 BEGIN          
  SELECT @ADD_INFORMATION=ISNULL(ADD_INFORMATION,'') FROM APP_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID           
  AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
  AND COVERAGE_CODE_ID IN (116)           
  IF(@ADD_INFORMATION='')          
   SET @HELTH_CARE=''          
 END          
--end here          
          
--added by pravesh on 22 AUG 2008 Itrack 4494          
DECLARE @LEASED_PURCHASED_DATE DateTIME,@LEASED_PURCHASED  CHAR(1),@ADD_INT_LOAN_LEAN CHAR(1)          
SET @LEASED_PURCHASED='N'          
SET @ADD_INT_LOAN_LEAN='N'          
SELECT @LEASED_PURCHASED_DATE=PURCHASE_DATE FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID           
  AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
IF EXISTS (SELECT SIGNATURE_OBTAINED               
 FROM APP_VEHICLE_COVERAGES                         
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID  AND COVERAGE_CODE_ID IN (46,249)          
 )           
BEGIN          
  IF(@LEASED_PURCHASED_DATE IS NULL)          
  BEGIN          
  SET @LEASED_PURCHASED='Y'          
   END          
  ELSE IF (DATEDIFF(DD,@LEASED_PURCHASED_DATE,@APP_EFF_DATE) > 90)          
  BEGIN          
  SET @LEASED_PURCHASED='Y'          
  END          
     -- NEW RULE AS PER iTRACK 4493          
      IF NOT EXISTS(select CUSTOMER_ID from APP_ADD_OTHER_INT           
  where CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID          
  )          
            SET @ADD_INT_LOAN_LEAN='Y'           
          
          
END          
          
--end here          
--added by pravesh on 29 AUG 2008 Itrack 4649/ 4546          
DECLARE @BI_CSL_EXISTS CHAR(1)          
DECLARE @IS_CSL CHAR(1)          
SET @BI_CSL_EXISTS='N'          
set @IS_CSL='N'          
IF (@STATE_ID=14 AND @VEHICLE_TYPE_PER NOT IN ('11618','11337','11870') AND @VEHICLE_TYPE_COM !='11341' AND @IS_SUSPENDED!=10963)          
BEGIN          
          
  IF EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
    WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
      AND COVERAGE_CODE_ID IN (1)          
   )          
 set @IS_CSL='Y'          
          
   IF NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
    WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
    --AND COVERAGE_CODE_ID IN (36,12,34,9,14)          
      AND COVERAGE_CODE_ID IN (9,12)          
   )          
  begin          
   SET @BI_CSL_EXISTS='Y'          
  end          
  else          
  begin          
     IF (          
    (EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (9) AND ISNULL(LIMIT1_AMOUNT_TEXT,'')<>'Reject')          
    AND           
                  (   
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (14))          
    OR          
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (36))          
      )           
    )          
    OR          
    (EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (12) AND ISNULL(LIMIT1_AMOUNT_TEXT,'')<>'Reject')          
    AND (          
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (34))          
    OR          
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)          
     WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID           
     AND COVERAGE_CODE_ID IN (36))          
     )          
    )          
   )          
   BEGIN          
    SET @BI_CSL_EXISTS='Y'          
   END          
          
  end          
END          
--end here          
-----moved down by pravesh on 17 sep08          
IF(@USE_VEHICLE = '11333') --COMM.              
 BEGIN               
 SET @VEHICLE_TYPE_PER='N'              
 END              
IF(@USE_VEHICLE = '11332') --PER.              
 BEGIN               
 SET @VEHICLE_TYPE_COM='N'              
 END            
          
 --added by pravesh on 24 sep 2008 Itrack 4777          
DECLARE @GOOD_STUDENT_DISCOUNT NVARCHAR(10)          
SET @GOOD_STUDENT_DISCOUNT='N'          
EXEC Proc_GetGoodStudent_App @CUSTOMERID,@APPID,@APPVERSIONID,@VEHICLEID,@GOOD_STUDENT_DISCOUNT OUTPUT          
          
IF (@GOOD_STUDENT_DISCOUNT='FALSE'          
 AND           
  EXISTS(          
   SELECT ADDS.DRIVER_ID FROM  APP_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK)           
   INNER JOIN APP_VEHICLES AV WITH (NOLOCK)                            
   ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                            
    AV.APP_ID=ADDS.APP_ID AND                    
    AV.APP_VERSION_ID = ADDS.APP_VERSION_ID AND                             
    AV.VEHICLE_ID=ADDS.VEHICLE_ID and          
    AV.CLASS_DRIVERID = ADDS.DRIVER_ID                            
   INNER JOIN APP_DRIVER_DETAILS WITH (NOLOCK)                            
   ON ADDS.CUSTOMER_ID=APP_DRIVER_DETAILS.CUSTOMER_ID AND                            
    ADDS.APP_ID=APP_DRIVER_DETAILS.APP_ID AND                            
    ADDS.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID                              
    AND  ADDS.DRIVER_ID=APP_DRIVER_DETAILS.DRIVER_ID                            
   WHERE ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.VEHICLE_ID=@VEHICLEID            
   AND (ISNULL(APP_DRIVER_DETAILS.DRIVER_GOOD_STUDENT,0)=1)           
   )          
 )          
 SET @GOOD_STUDENT_DISCOUNT='Y'          
ELSE          
 SET @GOOD_STUDENT_DISCOUNT='N'          
          
--end here            
-- by Pravesh on 13 Nov 08          
/*=============================================================          
Itrack 4862 Should not be able to commit if more than 1 principal driver on a vehicle           
==============================================================*/           
          
DECLARE @PRINCIPLE_OPERATOR CHAR          
DECLARE @INTCOUNT INT     
SET @PRINCIPLE_OPERATOR='N'          
          
SELECT @INTCOUNT=COUNT(AOPB.APP_VEHICLE_PRIN_OCC_ID) FROM APP_DRIVER_ASSIGNED_VEHICLE AOPB          
INNER JOIN APP_DRIVER_DETAILS AWDD          
ON AOPB.CUSTOMER_ID=AWDD.CUSTOMER_ID AND AOPB.APP_ID=AWDD.APP_ID AND AOPB.APP_VERSION_ID=AWDD.APP_VERSION_ID         
and AWDD.DRIVER_ID = AOPB.DRIVER_ID          
WHERE AOPB.CUSTOMER_ID=@CUSTOMERID AND AOPB.APP_ID=@APPID           
AND AOPB.APP_VERSION_ID =@APPVERSIONID AND  AOPB.VEHICLE_ID=@VEHICLEID           
AND AOPB.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)          
AND IS_ACTIVE='Y'          
          
 IF(@INTCOUNT > 1)          
 BEGIN          
  SET @PRINCIPLE_OPERATOR='Y'          
 END          
-- end here          
---------------------------------------------------------------------          
          
---------------------------------------------------------------------------------------------------          
-- If commercial vehicle and Bi Coverage limit is 300/500000 then policy will be rejected (Start)          
---------------------------------------------------------------------------------------------------          
DECLARE @BICOMNA NVARCHAR(10)          
DECLARE @BISPLITLIMIT NVARCHAR(50)          
SET @BISPLITLIMIT = ''          
SET @BICOMNA = 'N'          
IF(@VEHICLE_TYPE_COM IN ('11338','11339','11340','11341','11871'))          
BEGIN          
 SELECT @BISPLITLIMIT =  Substring(convert(varchar(30),convert(money,isnull(AVC.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(AVC.LIMIT_1,0)),1),0))                                    
    +'/'                                        
    + Substring(convert(varchar(30),convert(money,isnull(AVC.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(AVC.LIMIT_2,0)),1),0))                                        
    FROM APP_VEHICLE_COVERAGES AVC INNER JOIN           
 MNT_COVERAGE MC ON           
 AVC.COVERAGE_CODE_ID = MC.COV_ID          
          
 WHERE AVC.CUSTOMER_ID=@CUSTOMERID AND AVC.APP_ID=@APPID AND AVC.APP_VERSION_ID=@APPVERSIONID AND AVC.VEHICLE_ID=@VEHICLEID AND MC.COV_CODE ='BISPL'          
END          
IF(@BISPLITLIMIT = '300/500,000' OR @BISPLITLIMIT = '300,000/500,000')          
BEGIN          
 SET @BICOMNA = 'Y'          
END          
          
---------------------------------------------------------------------------------------------------          
-- If commercial vehicle and Bi Coverage limit is 300/500000 then policy will be rejected (End)          
---------------------------------------------------------------------------------------------------          
          
--Rule added by Pravesh on 27 Nov 08 as per Itrack 5057          
declare @BEYOND_50_MILES CHAR          
SET @BEYOND_50_MILES ='N'          
IF(@MILES_TO_WORK>50 and @USE_VEHICLE='11332' AND @STATE_ID='22')          
 SET @BEYOND_50_MILES ='Y'          
-- eND hERE          
          
-- This Rule Moved From Driver Level to Vehicle LEvel on 27 Jan 2009 Itrack 4771 by Pravesh          
/*                                    
If Extended Non- Owned Coverage for Named Insured is checked off, then when doing the verify make sure that                                     
Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                                     
that have a yes in the Field Extended Non Owned Coverages Required.                       
If there is a yes in the Field Extended Non Owned Coverages Required on the                                     
Drivers/Household members tab                                     
Then make sure  Extended Non- Owned Coverage for Named Insured is checked off */                       
                        
declare @ENO  char                   
set @ENO='N'                                 
              
declare @ADD_INFORMATION1 varchar(20)                           
                    
select @ADD_INFORMATION1=isnull(ADD_INFORMATION,'0')                                    
  from APP_VEHICLE_COVERAGES COV                                    
   where  COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID            
   AND  COV.COVERAGE_CODE_ID IN (52,254)                                     
   AND  COV.VEHICLE_ID = @VEHICLEID          
          
 /* AND  COV.VEHICLE_ID IN              
  (SELECT P.VEHICLE_ID FROM APP_DRIVER_DETAILS D   with(nolock)                
   INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P with(nolock)                                   
   ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                  
   WHERE D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID= @DRIVERID              
  )            
   */                                 
-- Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                            
-- that have a yes in the Field Extended Non Owned Coverages Required                            
declare @intCount1 int                
select @intCount1= count(isnull(Driver_ID,0)) from APP_DRIVER_DETAILS                                  
where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID            
 AND  EXT_NON_OWN_COVG_INDIVI='10963'                                    
--                                     
if(CAST(@ADD_INFORMATION1 AS NUMERIC)<>@intCount1)                   
 set @ENO='Y'                                    
else                                    
 set @ENO='N'                  
-- end here          
        
        
          
---------Added TrailBlazr Discount : Praveen Kasana          
DECLARE @QUALIFIESTRAIBLAZERPROGRAM VARCHAR(22)  
SET @QUALIFIESTRAIBLAZERPROGRAM='N'       
 -- Itrack 6828 Trailbalzer 
	IF(@QUOTEEFFECTIVEDATE<@TRAILBLAZER_EXPIRY_DATE )
		BEGIN
			EXECUTE Proc_CheckTrailBlazerEligibilityRule @CUSTOMERID,@APPID,@APPVERSIONID,@VEHICLEID,'APP',@QUALIFIESTRAIBLAZERPROGRAM OUT          
		END
------------------------------          
    
--------------------------ADDED AUTO POLICY NUMBER:PRAVEEN KUMAR(05-03-2009):ITRACK 5544    
    
DECLARE @MULTI_CAR NVARCHAR(5)    
SELECT @AUTO_POLICY_NO = ISNULL(AUTO_POL_NO,''),@MULTI_CAR = ISNULL(MULTI_CAR,'') FROM APP_VEHICLES     
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID    
    
IF(@MULTI_CAR = '11920' AND (@AUTO_POLICY_NO = '' OR @AUTO_POLICY_NO IS NULL))    
BEGIN    
 SET @AUTO_POLICY_NO = ''    
END    
    
ELSE    
BEGIN    
 SET @AUTO_POLICY_NO = 'N'    
END    
    
----------------------------------END------------------    

------------------------------Itrack 5832--REFERAL RULE IMPLEMENTATION--Praveen
DECLARE @UTILITY_TRAILER INT	
	SET @UTILITY_TRAILER = 11337
DECLARE @CAMPER_TRAVEL_TRAILER INT 
	SET @CAMPER_TRAVEL_TRAILER = 11870
DECLARE @NOT_APPLICABLE INT 
	SET @NOT_APPLICABLE = 11918

DECLARE @TRAILER INT 
	SET @TRAILER = 11341
DECLARE @TRAILER_INTERMITTENT INT
	SET @TRAILER_INTERMITTENT = 11340


DECLARE @TOTAL_VEHICLE_COUNT INT
DECLARE @PERSVEHICLEIDS INT                                                                                       
	SET @PERSVEHICLEIDS=0    
DECLARE @COMSVEHICLEIDS INT    
	SET @COMSVEHICLEIDS=0    

SELECT @PERSVEHICLEIDS = COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH (NOLOCK) 
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE = 'Y' 
AND USE_VEHICLE =11332 and VEHICLE_TYPE_PER !=@CAMPER_TRAVEL_TRAILER and VEHICLE_TYPE_PER != @UTILITY_TRAILER    

SELECT @COMSVEHICLEIDS = COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH (NOLOCK) 
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE = 'Y'  
AND USE_VEHICLE =11333 and VEHICLE_TYPE_COM !=@TRAILER and VEHICLE_TYPE_COM != @TRAILER_INTERMITTENT    

SET @TOTAL_VEHICLE_COUNT = @PERSVEHICLEIDS + @COMSVEHICLEIDS    



DECLARE @MULTI_CAR_PPA int
DECLARE @MULTI_CAR_COM int
DECLARE @MULTICAR_DISCOUNT_ELIGIBLE NVARCHAR(5)

SELECT @MULTI_CAR_PPA = ISNULL(MULTI_CAR,0) FROM APP_VEHICLES WITH (NOLOCK)
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID
AND VEHICLE_ID=@VEHICLEID  AND IS_ACTIVE = 'Y'
AND USE_VEHICLE =11332 AND VEHICLE_TYPE_PER !=@CAMPER_TRAVEL_TRAILER and VEHICLE_TYPE_PER != @UTILITY_TRAILER


SELECT @MULTI_CAR_COM = ISNULL(MULTI_CAR,0) FROM APP_VEHICLES WITH (NOLOCK)
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID 
AND VEHICLE_ID=@VEHICLEID AND IS_ACTIVE = 'Y'
AND USE_VEHICLE =11333 and VEHICLE_TYPE_COM !=@TRAILER and VEHICLE_TYPE_COM != @TRAILER_INTERMITTENT

SET @MULTICAR_DISCOUNT_ELIGIBLE  = 'N'

IF(@TOTAL_VEHICLE_COUNT > 1)
BEGIN
	IF(@MULTI_CAR_PPA = @NOT_APPLICABLE or @MULTI_CAR_COM = @NOT_APPLICABLE)
	BEGIN
		SET @MULTICAR_DISCOUNT_ELIGIBLE = 'Y'
	END
	ELSE
	BEGIN
		SET @MULTICAR_DISCOUNT_ELIGIBLE = 'N'
	END
END


------------------------------------------------END itrack 5832
          
          
 SELECT                                                                 
 @VEHICLE_YEAR AS VEHICLE_YEAR,                                                
 @MAKE AS MAKE,                                                
 @MODEL AS MODEL,                                                
 @VIN AS VIN,          
 @ANTIQUE_VECH AS ANTIQUE_VECH,                                
 @VEHICLE_USE AS VEHICLE_USE,              
 @VEHICLE_TYPE_PER AS VEHICLE_TYPE_PER,              
 @GRG_ADD1 AS GRG_ADD1,                                          
 @GRG_CITY  AS GRG_CITY,                                          
 @GRG_STATE AS GRG_STATE,                                          
 @GRG_ZIP AS GRG_ZIP,                        
 @AMOUNT AS  AMOUNT ,                                      
 @MAKE_NAME AS MAKE_NAME ,                   
 @USE_VEHICLE AS USE_VEHICLE,                        
 @VEHICLE_AMOUNT AS VEHICLE_AMOUNT,                      
 @SYMBOL AS SYMBOL,           
 @BICOMNA AS BICOMNA,             
 @PUNCS AS PUNCS,              
 @UNCSL AS UNCSL,               
 @UMPD  AS  UMPD,              
 @UNDSP AS UNDSP,              
 @PUMSP AS PUMSP,            
 @LLGC AS LLGC,              
 @EPENDO_SIGN AS EPENDO_SIGN   ,              
 @REG_STATE AS REG_STATE ,              
 @VEHICLE_TYPE_MHT AS VEHICLE_TYPE_MHT ,              
 @COMM_RADIUS_OF_USE AS COMM_RADIUS_OF_USE,              
 @COMM_LOCALHAUL  AS COMM_LOCALHAUL,            
 @COMM_INTER_LH AS COMM_INTER_LH,              
 @COMM_LONGHAUL AS COMM_LONGHAUL,              
 @VEHICLE_AMOUNT_COM AS VEHICLE_AMOUNT_COM,              
 @INSNOW_FULL AS INSNOW_FULL,              
 @CREG_STATE AS CREG_STATE,              
 @MODEL_NAME_MULTI_DIS AS MODEL_NAME_MULTI_DIS,              
 @YOUTHFUL_PRINC_DRIVER AS YOUTHFUL_PRINC_DRIVER,              
 @CLASS_COM AS CLASS_COM,              
 @CLASS_PER AS CLASS_PER,              
 @VEHICLE_TYPE_COM AS VEHICLE_TYPE_COM ,          
 @GRG_STATE_RULE  AS  GRG_STATE_RULE,          
 @GRANDFATHER_TERRITORY AS GRANDFATHER_TERRITORY,          
 @INTRADIUS_OF_USE AS RADIUS_OF_USE ,          
 @TRANSPORT_CHEMICAL AS TRANSPORT_CHEMICAL,          
 @COVERED_BY_WC_INSU   AS COVERED_BY_WC_INSU,          
 @MIS_EQUIP_COV AS MIS_EQUIP_COV,          
 @HELTH_CARE as HELTH_CARE,          
 @OT_COLL_COUNT as OT_COLL_COUNT,          
 @PRINC_OCCA_DRIVER AS PRINC_OCCA_DRIVER,          
 @LEASED_PURCHASED AS LEASED_PURCHASED,          
 @ADD_INT_LOAN_LEAN  AS ADD_INT_LOAN_LEAN,          
 @BI_CSL_EXISTS AS BI_CSL_EXISTS,          
 @IS_CSL as IS_CSL,          
 @ANTIQUE_CLASSIC_CAR AS ANTIQUE_CLASSIC_CAR,          
 @GOOD_STUDENT_DISCOUNT as GOOD_STUDENT_DISCOUNT,          
 @PRINCIPLE_OPERATOR as PRINCIPLE_OPERATOR,          
 @BEYOND_50_MILES as BEYOND_50_MILES,          
 @ENO as ENO,          
 @QUALIFIESTRAIBLAZERPROGRAM AS QUALIFIESTRAIBLAZERPROGRAM ,    
 @AUTO_POLICY_NO AS AUTO_POL_NO,
 @MULTICAR_DISCOUNT_ELIGIBLE AS MULTICAR_DISCOUNT_ELIGIBLE          
END             

--

GO

