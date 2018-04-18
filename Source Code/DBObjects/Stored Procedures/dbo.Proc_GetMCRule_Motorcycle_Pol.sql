IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMCRule_Motorcycle_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMCRule_Motorcycle_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                            
Proc Name                : Dbo.Proc_GetMCRule_Motorcycle_Pol                                                                         
Created by               : Ashwani                                                                            
Date                     : 16 Sep.,2005                                                                          
Purpose                  : To get the Rule Information for Motorcycle                                                              
Revison History          :                                                                            
Used In                  : Wolverine                                                                            
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------                                
drop proc dbo.Proc_GetMCRule_Motorcycle_Pol 1692,6,1,1                              
*/                             
CREATE PROC [dbo].[Proc_GetMCRule_Motorcycle_Pol]                                                                            
(                                                                            
 @CUSTOMER_ID    int,                                                                            
 @POLICY_ID    int,                                                                            
 @POLICY_VERSION_ID   int,                                                              
 @VEHICLE_ID int                                        
                                                           
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
                                                     
IF EXISTS (SELECT CUSTOMER_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID)                                                             
BEGIN                                     
 SELECT @VEHICLE_YEAR=ISNULL(VEHICLE_YEAR,''),@MAKE=ISNULL(UPPER(MAKE),''),@MODEL=ISNULL(UPPER(MODEL),''),                                                                     
 @GRG_ADD1=ISNULL(GRG_ADD1,''),@GRG_CITY=ISNULL(GRG_CITY,''),@GRG_STATE=ISNULL(GRG_STATE,''),@GRG_ZIP=ISNULL(GRG_ZIP,''),                                                                      
 -- @AMOUNT=ISNULL(AMOUNT,0)                               
 @AMOUNT=ISNULL(CONVERT(VARCHAR(30),AMOUNT),'N'),                                        
 @TERRITORY=ISNULL(TERRITORY,''),@MOTORCYCLE_TYPE=ISNULL(CONVERT(VARCHAR(20),MOTORCYCLE_TYPE),''),                               
@VEHICLE_CC=ISNULL(CONVERT(VARCHAR(20),VEHICLE_CC),''), @INTVEHICLE_AGE=ISNULL(VEHICLE_AGE,0),     
@VIN=ISNULL(VIN  ,''), @INTSYMBOL=ISNULL(SYMBOL,0),@CYCL_REGD_ROAD_USE=CYCL_REGD_ROAD_USE                                        
 FROM POL_VEHICLES                                                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID                                                                            
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
--IF(@INTVEHICLE_AGE >'25')                                                              
--BEGIN                                                               
--SET @VEHICLE_AGE='Y'                
--END                                                        
--ELSE                                               
--BEGIN                                                               
--SET @VEHICLE_AGE='N'                
--END         
SET @VEHICLE_AGE='N'  --Added by Charles on 27-Jul-09 for Itrack 5912                                    
-----------------------------------------------------                                                        
IF(@VIN='')                                                  
BEGIN              
SET @VIN='N'               
END      
  
--Added by Charles on 10-Aug-09 for Itrack 6234                                      
--Reject Application if atleast one coverage from (RLCSL ,BISPL, OTC or COLL) is not selected  
-- FOR SUSPENDED  ONLY  
IF((SELECT COMPRH_ONLY FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID) = 10963)  
BEGIN  
  IF EXISTS (SELECT COVERAGE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)                                               
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID                                         
  AND COVERAGE_CODE_ID IN (200,217,201,216) )  
  -- OTC: 200(IN), 217(MI). COLL: 201(IN), 216(MI).  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED_SUS='N'  
  END  
  ELSE  
  BEGIN  
  SET @ATLEAST_ONE_SELECTED_SUS='Y'  
  END   
END  
-- FOR NON SUSPENDED ONLY  
ELSE IF((SELECT COMPRH_ONLY FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID) = 10964)  
BEGIN  
 IF EXISTS (SELECT COVERAGE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)                                               
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID                                         
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
                                       
------------------------------------------------------                                             
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
                                         
-- OTC -- 200,201 -- COLL -- 216,217                                        
DECLARE @OTC_COLL  CHAR                                    
DECLARE @INTYEAR INT                                         
DECLARE @STATE_ID INT                                     
            
--Added by Charles on 21-Jul-09 for Itrack 5912        
 DECLARE @APP_EFFECTIVE_DATE INT                                   
            
 SELECT  @APP_EFFECTIVE_DATE =YEAR(ISNULL(APP_EFFECTIVE_DATE,0))  FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
                                     
 SET @INTYEAR = @APP_EFFECTIVE_DATE-@VEHICLE_YEAR           
--Added till here                                     
            
SET @OTC_COLL='N'                                  
                                
IF(@INTYEAR>25)                                        
BEGIN                                         
                                 
 IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                    
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID                                         
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
          
IF(@VEHICLE_CC<=50 AND @CYCL_REGD_ROAD_USE ='0')                                        
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
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                     
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (131) AND SIGNATURE_OBTAINED='N')                                         
BEGIN                                         
SET @PUNCS='Y'                                        
END                                        
ELSE                                        
BEGIN                                         
SET @PUNCS='N'                                        
END                                         
                                        
--2 Underinsured Motorist (CSL) -- 133                                        
DECLARE @UNCSL CHAR                                        
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (133) AND SIGNATURE_OBTAINED='N')                                         
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
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (199) AND SIGNATURE_OBTAINED='N')                                         
BEGIN                                         
SET @UMPD='Y'               
END                                        
ELSE                                        
BEGIN                          
SET @UMPD='N'                                        
END                                         
                                        
--5 Underinsured Motorist (BI)-- 214                                        
DECLARE @UNDSP CHAR                                        
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (214) AND SIGNATURE_OBTAINED='N')                                         
BEGIN                                         
SET @UNDSP='Y'                                        
END                                        
ELSE                                        
BEGIN                                         
SET @UNDSP='N'                                        
END                                         
                                        
--6 Uninsured Motorist (BI)-- 132                            
DECLARE @PUMSP CHAR                                      
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (132) AND SIGNATURE_OBTAINED='N')                                         
BEGIN                                   
SET @PUMSP='Y'                            
END                         
ELSE                         
BEGIN                                         
SET @PUMSP='N'                                        
END                                         
------- Medical Payments(MEDPM) -- 131                                        
DECLARE @MEDPM CHAR                                        
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (770,769,843) AND SIGNATURE_OBTAINED='N')                                         
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
Ineligible  Tour Bike                     T                                        
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
 SELECT  @STATE_ID = STATE_ID                                   
 FROM POL_CUSTOMER_POLICY_LIST  with(nolock)                                     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                   
 DECLARE @GRG_STATE_RULE CHAR                                    
 IF(@GRG_STATE<>@STATE_ID)                                    
 BEGIN                          
 SET @GRG_STATE_RULE='Y'                                    
 END                                   
 ELSE                                  
 BEGIN                                    
 SET @GRG_STATE_RULE='N'                                    
 END                                     
 --=========================================================================                              
   --Grandfather case for Territory Code                              
--=========================================================================                              
/*DECLARE @APP_EFF_DATE DATETIME                              
SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
                              
SELECT TOP 1 MTC.TERR AS TERRITORY_DESCRIPTION FROM POL_VEHICLES AVC                                                                
 INNER JOIN MNT_TERRITORY_CODES MTC  ON  AVC.GRG_ZIP=MTC.ZIP  AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=3                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                 
 AND NOT(@APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')                                
 AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')                                
 )*/                              
DECLARE @APP_EFF_DATE DATETIME ,                               
@TERR Int,                              
@GRANDFATHER_TERRITORY VARCHAR ,                        
@MOT_AMOUNT NUMERIC(12)                             
                        
SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                           
                        
SELECT @TERR = TERRITORY ,@MOT_AMOUNT=ISNULL(AMOUNT,0) FROM POL_VEHICLES AVC                              
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID                              
                        
IF EXISTS (                               
SELECT MTC.* FROM POL_VEHICLES AVC                                                                
INNER JOIN MNT_TERRITORY_CODES MTC                                
ON  AVC.TERRITORY = MTC.TERR AND SUBSTRING(AVC.GRG_Zip,0,6) = SUBSTRING(MTC.ZIP,0,6) AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=3                              
AND MTC.TERR = @TERR AND @APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')                    
AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')                               
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID                              
)                              
BEGIN                               
SET @GRANDFATHER_TERRITORY='N'                              
END                              
ELSE                              
BEGIN                               
SET @GRANDFATHER_TERRITORY='Y'                              
END                              
                        
                        
                        
--SELECT @DRIVER_DRIV_TYPE                        
--Added  by Pravesh on 27 nov 08 - Rule Moved form Driver level                              
DECLARE @YOUTHFUL_PRIN_DRIVER CHAR                                  
SET @YOUTHFUL_PRIN_DRIVER='N'                                  
--    "11399">Principal - No Points Assigned                              
--    "11398">Principal - Points Assigned                              
--    "11930">Youthful Principal - No Points Assigned                            
                     
--IS_ACTIVE CHECK ADDED BY CHARLES ON 6-Jul-09 FOR ITRACK 6056                      
 IF NOT EXISTS(SELECT ADAV.APP_VEHICLE_PRIN_OCC_ID FROM POL_DRIVER_ASSIGNED_VEHICLE ADAV                            
   INNER JOIN POL_DRIVER_DETAILS AD ON AD.CUSTOMER_ID=ADAV.CUSTOMER_ID                             
   AND AD.POLICY_ID=ADAV.POLICY_ID AND AD.POLICY_VERSION_ID=ADAV.POLICY_VERSION_ID                           
   AND AD.DRIVER_ID=ADAV.DRIVER_ID AND AD.IS_ACTIVE='Y'                                 
   WHERE ADAV.CUSTOMER_ID=@CUSTOMER_ID AND ADAV.POLICY_ID=@POLICY_ID AND ADAV.POLICY_VERSION_ID=@POLICY_VERSION_ID                                  
   AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)AND ADAV.VEHICLE_ID=@VEHICLE_ID)                              
 BEGIN                                   
 SET @YOUTHFUL_PRIN_DRIVER='Y'                                  
 END                              
                         
--SELECT @DRIVER_DRIV_TYPE ,@YOUTHFUL_PRIN_DRIVER                        
-------end here                          
---------------------ADDED TO CHECK IF ANY VEHICLE HAVE MORE THAN ONE PRINCIPAL DRIVERS ASSIGNED ITRACK 5701                        
DECLARE @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER CHAR                        
SET  @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER = 'N'                        
DECLARE @COUNT INT                        
                        
--Added Praveen Kasana --27 May 2009                        
SELECT @COUNT=COUNT(AOPB.APP_VEHICLE_PRIN_OCC_ID) FROM POL_DRIVER_ASSIGNED_VEHICLE AOPB                                
INNER JOIN POL_DRIVER_DETAILS AWDD                                
ON AOPB.CUSTOMER_ID=AWDD.CUSTOMER_ID AND AOPB.POLICY_ID=AWDD.POLICY_ID AND AOPB.POLICY_VERSION_ID=AWDD.POLICY_VERSION_ID                                
and AWDD.DRIVER_ID = AOPB.DRIVER_ID                                
WHERE AOPB.CUSTOMER_ID=@CUSTOMER_ID AND AOPB.POLICY_ID=@POLICY_ID                                 
AND AOPB.POLICY_VERSION_ID =@POLICY_VERSION_ID AND  AOPB.VEHICLE_ID=@VEHICLE_ID                                 
AND AOPB.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)                                
AND IS_ACTIVE='Y'                           
                        
--Commented on 27 May 2009                        
/*SELECT @COUNT=COUNT(*) FROM POL_DRIVER_ASSIGNED_VEHICLE                       
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID AND                         
POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID                        
AND APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)  */                        
                       
IF(@COUNT > 1)                        
BEGIN                        
 SET @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER = 'Y'                        
END                          
                        
-----------------------------------END---------------------------------------                         
--************************** Added by Manoj Rathore on 11 Jun. 2009 Itrack #  5872 ****************************                         
                        
DECLARE @IS_COST_OVER_DEFINED_LIMIT CHAR,@MOT_COST_OVER_DEFINED_LIMIT CHAR                        
SET @MOT_COST_OVER_DEFINED_LIMIT='N'                        
--SELECT @IS_COST_OVER_DEFINED_LIMIT=ISNULL(IS_COST_OVER_DEFINED_LIMIT,'0') FROM POL_AUTO_GEN_INFO                        
--WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                         
                 
IF (@MOT_AMOUNT > 40000 or @MOT_AMOUNT > 40000.00) -- AND @IS_COST_OVER_DEFINED_LIMIT='1')                        
BEGIN                        
 SET @MOT_COST_OVER_DEFINED_LIMIT='Y'                        
END                        
                        
--************************** End Itrack #  5872 ***************************************************************                        
--=========================================================================                                          
 SELECT                                                                                             
   @VEHICLE_YEAR   as VEHICLE_YEAR,                                                                 
   @MAKE  as MAKE,                                                                      
   @MODEL as MODEL,                                           
   @GRG_ADD1 as GRG_ADD1,             
   @GRG_CITY  as GRG_CITY,                                                             
   @GRG_STATE as GRG_STATE,                                                                  
   @GRG_ZIP   as  GRG_ZIP,                                                       
   @TERRITORY as TERRITORY,                                                               
   @AMOUNT as AMOUNT,                                                        
   @MOTORCYCLE_TYPE as MOTORCYCLE_TYPE,                                                      
   @VEHICLE_CC  as  VEHICLE_CC,                     
   @MODEL_NAME as MODEL_NAME,                                                               
   @MAKE_NAME  as MAKE_NAME ,                                                      
   @VEHICLE_AGE as VEHICLE_AGE,     
   @VIN as VIN ,                                            
   @SYMBOL as SYMBOL,                                        
   @OTC_COLL as OTC_COLL,                                        
   @CYCL_REGD_ROAD_USE as CYCL_REGD_ROAD_USE ,                                        
   @PUNCS  as PUNCS,                                        
   @UNCSL as UNCSL,                                        
   @UMPD as UMPD,                                        
   @UNDSP as UNDSP,                                        
   @PUMSP as PUMSP,                                        
   @MEDPM as MEDPM,                                        
   @REG_LIC_STATE as REG_LIC_STATE,                                  
   @GRG_STATE_RULE as GRG_STATE_RULE,                              
   @GRANDFATHER_TERRITORY as GRANDFATHER_TERRITORY  ,                              
   @YOUTHFUL_PRIN_DRIVER as YOUTHFUL_PRIN_DRIVER  ,                        
   @VEHICLE_MORETHANONE_PRINCIPAL_DRIVER AS   VEHICLE_MORETHANONE_PRINCIPAL_DRIVER,                      
   @MOT_COST_OVER_DEFINED_LIMIT AS MOT_COST_OVER_DEFINED_LIMIT,  
   @ATLEAST_ONE_SELECTED AS ATLEAST_ONE_SELECTED,  --Added by Charles on 10-Aug-09 for Itrack 6234                                                                                               
   @ATLEAST_ONE_SELECTED_SUS AS ATLEAST_ONE_SELECTED_SUS --Added by Charles on 13-Nov-09 for Itrack 6234                
END   
  
GO

