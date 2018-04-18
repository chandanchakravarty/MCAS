IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMCRule_Motorcycle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMCRule_Motorcycle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                        
Proc Name                : Dbo.Proc_GetMCRule_Motorcycle 1676,3,1,1,'a'                                                                     
Created by               : Ashwani                                                                        
Date                     : 16 Sep.,2005                                                                      
Purpose                  : To get the Rule Information for Motorcycle                                                          
Revison History          :                                                                        
Used In                  : Wolverine                                                                        
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/                                
--drop proc dbo.Proc_GetMCRule_Motorcycle 1707,16,1,1,'SUBMIT'                            
CREATE PROC [dbo].[Proc_GetMCRule_Motorcycle]                                                                    
(                                                                        
 @CUSTOMERID    INT,                                                                        
 @APPID    INT,                                                                        
 @APPVERSIONID   INT,                                                          
 @VEHICLEID INT   ,                                                  
 @DESC VARCHAR(10)                                                       
)                                                                        
AS                                                     
BEGIN                                                   
 -- Vehicle Info                                                                         
  declare @VEHICLE_YEAR varchar(4)                                                                    
  declare @MAKE nvarchar(75)                                                                        
  declare @MODEL nvarchar(75)                                                        
  declare @GRG_ADD1 nvarchar(70)                                                                  
  declare @GRG_CITY  nvarchar(40)                                                                  
  declare @GRG_STATE nvarchar(5)                                                                  
  declare @GRG_ZIP varchar(11)                                                                  
  declare @TERRITORY varchar(5)                                                                  
  declare @AMOUNT  varchar(30)                                                         
  declare @MOTORCYCLE_TYPE varchar(20)                              
  declare @VEHICLE_CC varchar(20)                                                                     
  declare @MODEL_NAME char                                                              
  declare @MAKE_NAME char                                                   
  declare @INTVEHICLE_AGE int                                                        
  declare @VEHICLE_AGE char                                                         
  declare @VIN nvarchar(75)                                        
  declare @SYMBOL char                                      
  declare @INTSYMBOL int                                      
  declare @CYCL_REGD_ROAD_USE char                                     
  declare @REGISTERED_STATE varchar(10)   
  
  declare @ATLEAST_ONE_SELECTED char --Added by Charles on 10-Aug-09 for Itrack 6234                         
  declare @ATLEAST_ONE_SELECTED_SUS char --Added by Charles on 13-Nov-09 for Itrack 6234   
   SET @ATLEAST_ONE_SELECTED='N'
   SET @ATLEAST_ONE_SELECTED_SUS='N' --Added by Charles on 13-Nov-09 for Itrack 6234 
                                               
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID)                                                                        
 BEGIN                                                 
 SELECT @VEHICLE_YEAR=ISNULL(VEHICLE_YEAR,''),@MAKE=ISNULL(UPPER(MAKE),''),@MODEL=ISNULL(UPPER(MODEL),''),                                         
 @GRG_ADD1=ISNULL(GRG_ADD1,''),@GRG_CITY=ISNULL(GRG_CITY,''),@GRG_STATE=ISNULL(GRG_STATE,''),@GRG_ZIP=ISNULL(GRG_ZIP,''),                                     
 -- @AMOUNT=ISNULL(AMOUNT,0)                                    
 @AMOUNT=ISNULL(CONVERT(VARCHAR(30),AMOUNT),'N'),                                    
 @TERRITORY=ISNULL(TERRITORY,''),@MOTORCYCLE_TYPE=ISNULL(CONVERT(VARCHAR(20),MOTORCYCLE_TYPE),''),                    
 @VEHICLE_CC=ISNULL(CONVERT(VARCHAR(20),VEHICLE_CC),''), @INTVEHICLE_AGE=ISNULL(VEHICLE_AGE,0),      
@VIN=ISNULL(VIN  ,''), @INTSYMBOL=ISNULL(SYMBOL,0),@CYCL_REGD_ROAD_USE= CYCL_REGD_ROAD_USE                                    
 FROM APP_VEHICLES                                            
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID                                                    
 END                                                                         
 ELSE                                               
 BEGIN                                             
 SET @VEHICLE_YEAR =''                                             
 SET @MAKE =''                                                                        
 SET @MODEL =''                                                                        
 SET @GRG_ADD1 =''                                                                  
 SET @GRG_CITY =''                                                                   
 SET @GRG_STATE =''                                                                
 SET @GRG_ZIP=''                                                  
 SET @TERRITORY =''                                                                  
 SET @AMOUNT ='N'                                                              
 SET @MODEL_NAME=''                                                               
 SET @MAKE_NAME=''                                                   
 SET @VEHICLE_CC=''                                                  
 SET @VEHICLE_AGE=''                                                   
 SET @INTVEHICLE_AGE=0                                               
 SET @VIN=''                                          
 SET @SYMBOL=''                                        
 SET @INTSYMBOL=0                                                
 SET @CYCL_REGD_ROAD_USE=''                                    
 END                                                      
                                                   
-- Commented by Charles on 24-Jul-09 for Itrack 5912                                             
-- IF(@INTVEHICLE_AGE >'25')                                                          
-- BEGIN                                                           
-- SET @VEHICLE_AGE='Y'               
-- END                                                    
-- ELSE                                           
-- BEGIN                                                           
-- SET @VEHICLE_AGE='N'               
-- END     
SET @VEHICLE_AGE='N'  --Added by Charles on 27-Jul-09 for Itrack 5912                                                                                       
-----------------------------------------------------                                                    
 IF(@VIN='')                                              
 BEGIN                                               
 SET @VIN='N'                                              
 END  
  
--Added by Charles on 10-Aug-09 for Itrack 6234                                      
--Reject Application if atleast one coverage from (RLCSL ,BISPL, OTC or COLL) is not selected  
-- For Suspended Vehicle  
IF((SELECT COMPRH_ONLY FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID) = 10963)  
BEGIN  
  IF EXISTS (SELECT COVERAGE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)                                               
  WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID                                     
  AND COVERAGE_CODE_ID IN (200,217,201,216))  
  -- OTC: 200(IN), 217(MI). COLL: 201(IN), 216(MI).  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED_SUS='N'  
  END  
  ELSE  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED_SUS='Y'  
  END  
END  
-- FOR NON SUSPNEDED VEHICLE  
ELSE IF((SELECT COMPRH_ONLY FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID) = 10964)  
BEGIN  
 IF EXISTS (SELECT COVERAGE_ID FROM APP_VEHICLE_COVERAGES WITH(NOLOCK)                                               
  WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID                                     
  AND COVERAGE_CODE_ID IN (126,206,127,207,128,208))  
  -- RLCSL: 126(IN), 206(MI). BISPL: 127(IN), 207(MI). PD: 128(IN), 208(MI).  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED='N'  
  END  
  ELSE  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED='Y'  
  END  
END  
--Added till here   
                                      
--for symbol                                       
 IF(@INTSYMBOL>9)                                         
 BEGIN                                       
 SET @SYMBOL='Y'                                      
 END                                       
 ELSE IF(@INTSYMBOL=0)                                      
 BEGIN            
 SET @SYMBOL=''                                 
 END                                       
 ELSE IF(@INTSYMBOL<>0)                                      
 BEGIN                                       
 SET @SYMBOL='N'                                      
 END                                       
-------------------------------------------------------                                      
/*Take the effective date of the policy minus the Year field on the Motorcycle                                    
  Info Tab if greater than 25 then Collision and comprehensive coverages should be disabled.                                     
  User can not take these 2 and dependant coverages */                                    
 DECLARE @APP_EFFECTIVE_DATE INT                                    
 DECLARE @INTYEAR INT                                     
 DECLARE @STATE_ID INT                       
                                         
 SELECT  @APP_EFFECTIVE_DATE =YEAR(APP_EFFECTIVE_DATE) ,                                    
 @STATE_ID = STATE_ID FROM APP_LIST                                     
 WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                                    
                                     
 SET @INTYEAR = @APP_EFFECTIVE_DATE-@VEHICLE_YEAR                                    
                                    
                                    
-- OTC -- 200,201 -- COLL -- 216,217                                    
DECLARE @OTC_COLL  CHAR                                    
SET @OTC_COLL='N'            
                               
IF(@INTYEAR>25)                             
BEGIN                                   
                              
 IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
 WHERE  CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID                                     
 AND COVERAGE_CODE_ID IN (200,201,216,217) )                                     
 BEGIN                                     
 SET @OTC_COLL='Y'           
 END                                    
 ELSE                                    
 BEGIN                                     
 SET @OTC_COLL='N'            
 END                                     
END           
                                 
--------------------------------                                    
/* Motorcycle Info Tab If CC's Field is less than 50 and if NO to Road Use then refer to Underwriter                                  
 for eligibility Message should read "Not eligible for Motorcycle Program  refer to Homeowners for eligibility" */                                    
                                    
IF(@VEHICLE_CC<=50 AND @CYCL_REGD_ROAD_USE ='0' )                                    
BEGIN                                     
SET @CYCL_REGD_ROAD_USE='Y'                                    
END                            
                            
--ELSE IF ADDED BY PRAVEEN KUMAR(11-12-2008):ITRACK 5121)                     
ELSE IF(@VEHICLE_CC<=50 AND (@CYCL_REGD_ROAD_USE is null or @CYCL_REGD_ROAD_USE=''))                            
BEGIN                             
 SET @CYCL_REGD_ROAD_USE=''                            
END                             
-------END PRAVEEN KUMAR-----                             
ELSE                            
 BEGIN                                     
SET @CYCL_REGD_ROAD_USE='N'                                    
END                            
--------------------------------------------------------------------                                    
-----------------------------------------------------------------------------------------------                                    
/*                                     
  If State is Indiana On vehicle   coverage tab If No in the Signature Field   for any of these coverages                                     
  Refer to underwriter                                     
  Underinsured Motorist (CSL) Uninsured Motorist (CSL)          
  Underinsured Motorist (PD)  Uninsured Motorist (PD)                                    
Underinsured Motorist (BI)  Uninsured Motorist (BI)*/                                     
                                    
 --1. Uninsured Motorist (CSL) -- 131                                    
DECLARE @PUNCS CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (131) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                             
SET @PUNCS='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @PUNCS='N'                                    
END                                     
                                    
--2 Underinsured Motorist (CSL) -- 133                                    
DECLARE @UNCSL CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (133) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                                     
SET @UNCSL='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @UNCSL='N'                                    
END                                     
                                    
--3 Underinsured Motorist (PD) -- Not at Screen                                     
                                    
--4 Uninsured Motorist (PD) -- 199                                    
DECLARE @UMPD CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (199) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                                     
SET @UMPD='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @UMPD='N'                                    
END                                     
                   
--5 Underinsured Motorist (BI)-- 214                      
DECLARE @UNDSP CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (214) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                                     
SET @UNDSP='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @UNDSP='N'                                    
END                                     
                                    
--6 Uninsured Motorist (BI)-- 132                                    
DECLARE @PUMSP CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                               
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (132) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                                     
SET @PUMSP='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @PUMSP='N'         
END                                     
------- Medical Payments(MEDPM) -- 131                                    
DECLARE @MEDPM CHAR                                    
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM APP_VEHICLE_COVERAGES                                             
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND VEHICLE_ID=@VEHICLEID AND COVERAGE_CODE_ID IN (770,769,843) AND SIGNATURE_OBTAINED='N')                                     
BEGIN                                     
SET @MEDPM='Y'                                    
END                                    
ELSE                                    
BEGIN                                     
SET @MEDPM='N'                                    
END                                    
---------------------------------------------------------------------------------------------------               
                                    
/* Application/Policy Details State Field If State Field above is not equal to the State Licensed Field on the                                     
   Vehicle Info Tab for all vehicles Refer to underwriters  */                                    
DECLARE @REG_LIC_STATE CHAR                                     
SET @REG_LIC_STATE ='N'                                    
                              
IF(@REGISTERED_STATE<>@STATE_ID)                                    
BEGIN                        
SET @REG_LIC_STATE='Y'                                    
END                                    
-----------------------------------------------------------------------------                                    
/* Vehicle Info Tab Motorcycle  Type System Generate based on Yr/Make/Model                                     
Table Options All Other Standard Bike            A Extra Hazard  (Scooter & Dual Purpose)    E                                    
Gold Wing (All models)                    G Sports Bike (Racing or High Performance)  H                         
Ineligible  Tour Bike                                 T                                    
If Ineligible refer to underwriters */                                    
                                    
IF(@MOTORCYCLE_TYPE='11426')                                    
BEGIN                                     
SET @MOTORCYCLE_TYPE='Y'                                    
END                                     
ELSE                                    
BEGIN                                     
SET @MOTORCYCLE_TYPE='N'                                    
END                                     
------------------------------------------------------------------------------                                  
-- Garaging state of all motorcycles must be same as of main application state.Reject in this case                                 
DECLARE @GRG_STATE_RULE CHAR              
 IF(@GRG_STATE<>@STATE_ID)                                  
 BEGIN                                  
 SET @GRG_STATE_RULE='Y'                     END                                 
 ELSE                                
 BEGIN                                  
 SET @GRG_STATE_RULE='N'                                  
 END                                 
--=========================================================================                              
   --Grandfather case for Territory Code                              
--=========================================================================                              
/*DECLARE @APP_EFF_DATE DATETIME                              
SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                              
                              
SELECT  MTC.TERR AS TERRITORY_DESCRIPTION FROM APP_VEHICLES AVC                                                                
 INNER JOIN MNT_TERRITORY_CODES MTC  ON  AVC.GRG_ZIP=MTC.ZIP  AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=3                              
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND VEHICLE_ID=@VEHICLEID                               
 AND NOT(@APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')                                
 AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')                                
 )*/                              
 DECLARE @APP_EFF_DATE DATETIME ,                          
  @TERR Int,                              
  @GRANDFATHER_TERRITORY VARCHAR ,                        
  @MOT_AMOUNT NUMERIC(12)                            
                                
 SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                              
                              
 SELECT @TERR = TERRITORY ,@MOT_AMOUNT=ISNULL(AMOUNT,0)FROM APP_VEHICLES AVC                              
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND VEHICLE_ID=@VEHICLEID                              
                              
 IF EXISTS (                               
  SELECT MTC.* FROM APP_VEHICLES AVC                                                                
  INNER JOIN MNT_TERRITORY_CODES MTC                                
  ON  AVC.TERRITORY = MTC.TERR AND SUBSTRING(AVC.GRG_Zip,0,6) = SUBSTRING(MTC.ZIP,0,6) AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=3                              
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
                        
                              
--Added  by Pravesh on 27 nov 08 - Rule Moved form Driver level                              
DECLARE @YOUTHFUL_PRIN_DRIVER CHAR                      
SET @YOUTHFUL_PRIN_DRIVER='N'                                  
--    "11399">Principal - No Points Assigned                              
--    "11398">Principal - Points Assigned                              
--    "11930">Youthful Principal - No Points Assigned                     
                  
--IS_ACTIVE check  added by Charles on 6-Jul-09 for Itrack 6056                  
 IF ( NOT EXISTS(SELECT ADAV.APP_VEHICLE_PRIN_OCC_ID FROM APP_DRIVER_ASSIGNED_VEHICLE  ADAV                 
 INNER JOIN APP_DRIVER_DETAILS ADDS                            
 ON  ADAV.CUSTOMER_ID= ADDS.CUSTOMER_ID AND ADAV.APP_ID=ADDS.APP_ID AND ADAV.APP_VERSION_ID=ADDS.APP_VERSION_ID                            
 AND ADAV.DRIVER_ID=ADDS.DRIVER_ID                                       
 WHERE ADAV.CUSTOMER_ID=@CUSTOMERID AND ADAV.APP_ID=@APPID AND ADAV.APP_VERSION_ID=@APPVERSIONID                                    
 AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929) AND ADDS.IS_ACTIVE='Y'                   
 AND ADAV.VEHICLE_ID=@VEHICLEID))                         
 BEGIN                       
 SET @YOUTHFUL_PRIN_DRIVER='Y'                                  
 END                            
-------end here                      
                      
---------------------ADDED TO CHECK IF ANY VEHICLE HAVE MORE THAN ONE PRINCIPAL DRIVERS ASSIGNED ITRACK 5701                        
DECLARE @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER CHAR                        
SET  @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER = 'N'                        
DECLARE @COUNT INT                        
                
---Added on May 27 2009 --Praveen kasana                        
SELECT @COUNT=COUNT(AOPB.APP_VEHICLE_PRIN_OCC_ID) FROM APP_DRIVER_ASSIGNED_VEHICLE AOPB                                  
INNER JOIN APP_DRIVER_DETAILS AWDD                    
ON AOPB.CUSTOMER_ID=AWDD.CUSTOMER_ID AND AOPB.APP_ID=AWDD.APP_ID AND AOPB.APP_VERSION_ID=AWDD.APP_VERSION_ID                                 
and AWDD.DRIVER_ID = AOPB.DRIVER_ID                                  
WHERE AOPB.CUSTOMER_ID=@CUSTOMERID AND AOPB.APP_ID=@APPID                                   
AND AOPB.APP_VERSION_ID =@APPVERSIONID AND  AOPB.VEHICLE_ID=@VEHICLEID                                   
AND AOPB.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)                                  
AND IS_ACTIVE='Y'                          
                   
--Commented Praveen kasana Check -No Isactive Check                        
/*SELECT @COUNT=COUNT(*) FROM                         
APP_DRIVER_ASSIGNED_VEHICLE with(nolock)                        
WHERE                         
CUSTOMER_ID=@CUSTOMERID and                         
APP_ID=@APPID AND                          
APP_VERSION_ID=@APPVERSIONID AND                         
VEHICLE_ID=@VEHICLEID                        
AND APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)  */                        
                        
--select * from mnt_lookup_values where lookup_unique_id in (11399,11398,11930,11929)                          
                      
IF(@COUNT > 1)                        
BEGIN                        
 SET @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER = 'Y'                        
END                      
                      
-----------------------------------END---------------------------------------                           
--************************** Added by Manoj Rathore on 11 Jun. 2009 Itrack #  5872 ****************************                         
                        
DECLARE @IS_COST_OVER_DEFINED_LIMIT CHAR,@MOT_COST_OVER_DEFINED_LIMIT CHAR                        
SET @MOT_COST_OVER_DEFINED_LIMIT='N'                        
--SELECT @IS_COST_OVER_DEFINED_LIMIT=ISNULL(IS_COST_OVER_DEFINED_LIMIT,'0') FROM APP_AUTO_GEN_INFO                        
--WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                         
                
 IF ((@MOT_AMOUNT > 40000 or @MOT_AMOUNT > 40000.00)) ---AND @IS_COST_OVER_DEFINED_LIMIT='1')                        
 BEGIN                        
 SET @MOT_COST_OVER_DEFINED_LIMIT='Y'                        
 END                    
                  
                     
                        
--************************** End Itrack #  5872 ***************************************************************                 --=========================================================================                                    
SELECT                                                       
 @VEHICLE_YEAR   AS VEHICLE_YEAR,                                                             
 @MAKE  AS MAKE,                                                     
 @MODEL AS MODEL,                                                  
 @GRG_ADD1 AS GRG_ADD1,                                                  
 @GRG_CITY  AS GRG_CITY,                          
@GRG_STATE AS GRG_STATE,                                            
 @GRG_ZIP   AS  GRG_ZIP,                                                   
 @TERRITORY AS TERRITORY,                                                           
 @AMOUNT AS AMOUNT,                                                    
 @MOTORCYCLE_TYPE AS MOTORCYCLE_TYPE,                                                  
 @VEHICLE_CC  AS  VEHICLE_CC,                                                                
 @MODEL_NAME AS MODEL_NAME,                                                           
 @MAKE_NAME  AS MAKE_NAME ,                                                  
 @VEHICLE_AGE AS VEHICLE_AGE,        
 @VIN AS VIN ,                                        
 @SYMBOL AS SYMBOL,                                    
 @OTC_COLL AS OTC_COLL,                                    
 @CYCL_REGD_ROAD_USE AS CYCL_REGD_ROAD_USE ,                                    
 @PUNCS  AS PUNCS,                                    
 @UNCSL AS UNCSL,                                    
 @UMPD AS UMPD,                                    
 @UNDSP AS UNDSP,                                    
 @PUMSP AS PUMSP,                                    
 @MEDPM AS MEDPM,                                    
 @REG_LIC_STATE AS REG_LIC_STATE,                                
 @GRG_STATE_RULE AS GRG_STATE_RULE,                                    
 @GRANDFATHER_TERRITORY AS GRANDFATHER_TERRITORY   ,                              
 @YOUTHFUL_PRIN_DRIVER as YOUTHFUL_PRIN_DRIVER  ,                        
 @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER AS   VEHICLE_MORETHANONE_PRINCIPAL_DRIVER,                        
 @MOT_COST_OVER_DEFINED_LIMIT AS MOT_COST_OVER_DEFINED_LIMIT,  
 @ATLEAST_ONE_SELECTED AS ATLEAST_ONE_SELECTED,  --Added by Charles on 10-Aug-09 for Itrack 6234        
 @ATLEAST_ONE_SELECTED_SUS AS ATLEAST_ONE_SELECTED_SUS --Added by Charles on 13-Nov-09 for Itrack 6234                
END   
GO

