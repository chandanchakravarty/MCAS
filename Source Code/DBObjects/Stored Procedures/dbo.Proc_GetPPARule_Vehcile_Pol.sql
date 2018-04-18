IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_Vehcile_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_Vehcile_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                  
Proc Name                : Dbo.Proc_GetPPARule_Vehcile_POL                                                                                
Created by               : Ashwani                                                                                  
Date                     : 01 Mar. 2006                                          
Purpose                  : To get the Auto detail for Policy                                                  
Revison History          :              
            
MODIFIED by              : Pravesh K chandel                                                                          
Date                     : 17 july,2008                                          
Purpose                  : Implement Extra equipment/other then collision covg Rules           
    
MODIFIED by              : Praveen Kasana                                                                            
Date                     : 30 sep,2009                                            
Purpose                  : Implement Itrack 5823                                        
                                                                                
Used In                  : Wolverine                                                                                  
Reviewed By : Anurag Verma            
Reviewed On : 12-07-2007            
------------------------------------------------------------                                                                                  
Date     Review By          Comments                                                                                  
------   ------------       -------------------------*/                                                                                  
-- drop proc dbo.Proc_GetPPARule_Vehcile_Pol  1455,2,1 ,2                                                                             
create proc [dbo].[Proc_GetPPARule_Vehcile_Pol]                                                                                
(                                                                                  
 @CUSTOMER_ID    int,                                                                                  
 @POLICY_ID    int,                                                                                  
 @POLICY_VERSION_ID   int,                                                                        
 @VEHICLE_ID int                                                                              
)                                                                                  
AS                                                                                      
BEGIN                                                                               
-- Vehicle Info                                                           
 DECLARE @VEHICLE_YEAR VARCHAR(4)                                                          
 DECLARE @MAKE NVARCHAR(75)                                                          
 DECLARE @MODEL NVARCHAR(75)                                                          
 DECLARE @VIN NVARCHAR(75)                                                    
 DECLARE @VEHICLE_USE NVARCHAR(5)                                                    
 DECLARE @INSURED_VEH_NUMBER SMALLINT                                                    
 DECLARE @GRG_ADD1 NVARCHAR(70)                                                    
 DECLARE @GRG_CITY  NVARCHAR(40)                                                    
 DECLARE @GRG_STATE NVARCHAR(5)                                                    
 DECLARE @GRG_ZIP VARCHAR(11)                                                    
 --DECLARE @TERRITORY VARCHAR(5)                                                    
 DECLARE @AMOUNT  DECIMAL                  
 DECLARE @MODEL_NAME CHAR         
 DECLARE @MAKE_NAME CHAR                                                
 DECLARE @USE_VEHICLE NVARCHAR(5)                                     
 DECLARE @INTSYMBOL INT                              
 DECLARE @SYMBOL CHAR                      
 DECLARE @APP_VEHICLE_PERTYPE_ID VARCHAR(15)                    
 DECLARE @CLASS_PER VARCHAR(12)         
 DECLARE @CLASS_COM VARCHAR(12)                  
 DECLARE @APP_VEHICLE_COMTYPE_ID VARCHAR(15)                
 DECLARE @COMM_RADIUS_OF_USE CHAR               
 DECLARE @INTRADIUS_OF_USE  INT             
 DECLARE @REGISTERED_STATE VARCHAR(10)            
 DECLARE @TRANSPORT_CHEMICAL VARCHAR(10)            
 DECLARE @COVERED_BY_WC_INSU VARCHAR(10)            
 DECLARE @SNOWPLOW_CONDS VARCHAR(10)             
 DECLARE @AGE VARCHAR(20)            
    DECLARE @ANTIQUE_VECH VARCHAR(20)            
 SET @COMM_RADIUS_OF_USE='N'             
 DECLARE @QUOTEEFFECTIVEDATE VARCHAR(20)             
 DECLARE @APP_INCEPTION_DATE VARCHAR(20)            
 DECLARE @MILES_TO_WORK INT             
 DECLARE @IS_SUSPENDED INT            
 DECLARE @TRAILBLAZER_EXPIRY_DATE DATETIME
 SET @TRAILBLAZER_EXPIRY_DATE = '01/01/2010'  
 --Added by Charles on 20-Nov-09 for Itrack 6592  
-- DECLARE @ISRENEWEDPOLICY CHAR  
-- DECLARE @PRIOR_LOSS CHAR  
-- SET @ISRENEWEDPOLICY = 'N'  
-- SET @PRIOR_LOSS = 'N'  
--       
-- IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID     
--    AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_ID = 5)  
-- BEGIN  
-- SET @ISRENEWEDPOLICY = 'Y'   
-- END    
--  
-- IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID)  
-- BEGIN  
-- SET @PRIOR_LOSS = 'Y'  
-- END  
 --Added till here           
            
 SELECT @QUOTEEFFECTIVEDATE = APP_EFFECTIVE_DATE ,@APP_INCEPTION_DATE=APP_INCEPTION_DATE            
  FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                    
            
IF EXISTS (SELECT CUSTOMER_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID)                                                          
BEGIN                                                     
 SELECT @VEHICLE_YEAR=ISNULL(VEHICLE_YEAR,''),@MAKE=ISNULL(UPPER(MAKE),''),@MODEL=ISNULL(UPPER(MODEL),''),            
 @VIN=ISNULL(VIN,''),                   
 @VEHICLE_USE=ISNULL(APP_USE_VEHICLE_ID,''),                            
 @INSURED_VEH_NUMBER=ISNULL(INSURED_VEH_NUMBER,''),                         
 @GRG_ADD1=ISNULL(GRG_ADD1,''),@GRG_CITY=ISNULL(GRG_CITY,''),@GRG_STATE=ISNULL(GRG_STATE,''),            
 @GRG_ZIP=ISNULL(GRG_ZIP,''),                                                    
 @AMOUNT=ISNULL(AMOUNT,-1),@USE_VEHICLE=ISNULL(APP_USE_VEHICLE_ID,''),@INTSYMBOL=ISNULL(SYMBOL,0),                      
 @APP_VEHICLE_PERTYPE_ID=ISNULL(APP_VEHICLE_PERTYPE_ID,'0'),@CLASS_PER=ISNULL(APP_VEHICLE_PERCLASS_ID,''),            
 @CLASS_COM=ISNULL(APP_VEHICLE_COMCLASS_ID,''),                  
 @APP_VEHICLE_COMTYPE_ID=ISNULL(APP_VEHICLE_COMTYPE_ID,'0'),@INTRADIUS_OF_USE = ISNULL(RADIUS_OF_USE,0),            
 @SNOWPLOW_CONDS=ISNULL(SNOWPLOW_CONDS,''),@REGISTERED_STATE=REGISTERED_STATE,            
 @TRANSPORT_CHEMICAL=ISNULL(TRANSPORT_CHEMICAL,''),@COVERED_BY_WC_INSU=ISNULL(COVERED_BY_WC_INSU,''),            
 @AGE=DATEDIFF(YEAR,ISNULL(VEHICLE_YEAR,'0'),@QUOTEEFFECTIVEDATE),            
 @MILES_TO_WORK=isnull(MILES_TO_WORK,0) ,            
 @IS_SUSPENDED =isnull(IS_SUSPENDED,0)                 
                                                 
FROM POL_VEHICLES                                 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID                    
END                                                                                               
------------------------------------------------------------            
-- if age of vehicle is greater than 15 years then refer it to underwriter            
            
DECLARE @DATEDIFF INT            
SELECT @DATEDIFF=(DATEDIFF(year,@VEHICLE_YEAR,@QUOTEEFFECTIVEDATE))             
IF(@DATEDIFF>0)            
BEGIN            
 IF(@AGE > 15 AND @IS_SUSPENDED=10964)-- AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y'))  --Condition added by Sibin for Itrack 5512 on 2 March 09          
  BEGIN     -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592        
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
IF ((@APP_VEHICLE_PERTYPE_ID='11869' OR @APP_VEHICLE_PERTYPE_ID='11868' )AND YEAR(@APP_INCEPTION_DATE)>2002 )            
 BEGIN            
  SET @ANTIQUE_CLASSIC_CAR='Y'            
 END            
----------            
              
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
            
IF(@USE_VEHICLE='11333' AND @INTRADIUS_OF_USE>75 AND @APP_VEHICLE_COMTYPE_ID='11339')                
 BEGIN                 
 SET @COMM_LOCALHAUL='Y'                
 END                                                  
--                                                 
IF(@MODEL='CORVETTE' OR @MODEL='CORVETTE' OR @MODEL='C')                    
 BEGIN                                       
 SET @MODEL_NAME='Y'                                              
 END                         
ELSE                                 
 BEGIN                                                 
 SET @MODEL_NAME='N'                                                
 END                                                 
--                          
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
---------------------------------------------------------------------------------------------------------------                                  
---Rule Amount > 80000                                  
                                  
declare @VEHICLE_AMOUNT char                                  
--check for Personal Vehicle Type                      
--Amount field is not mandatory in case of Private Passenger Vehicle - 11334                      
if(@APP_VEHICLE_PERTYPE_ID=11334 AND (@Amount='-1' or @Amount ='0' or @Amount is null))                      
  set @VEHICLE_AMOUNT='N'                                
else if(@USE_VEHICLE='11332' AND @AMOUNT>'80000.00')              
begin             
 set @VEHICLE_AMOUNT ='Y'                                  
end                                   
else if(@Amount=-1 or @Amount =0 or @Amount is null)                            
begin                             
  set @VEHICLE_AMOUNT=''                            
end                    
else                                  
begin                                   
 set @VEHICLE_AMOUNT='N'                                  
end                               
----------------------------------------------------------------------------------------------             
--4) Vehicle Information Tab , If Vehicle type is Commercial                 
-- If the Amount Field is greater than $50,000 , then Refer to Underwriters                 
DECLARE @VEHICLE_AMOUNT_COM CHAR                
SET @VEHICLE_AMOUNT_COM='N'                
            
IF(@USE_VEHICLE='11333' AND @AMOUNT>50000.00)                
BEGIN                 
SET @VEHICLE_AMOUNT_COM='Y'                
END                 
----------------------------------------------------------------------------------------------                                 
IF(@APP_VEHICLE_PERTYPE_ID='0' OR @APP_VEHICLE_PERTYPE_ID = '')                  
BEGIN                   
 SET @APP_VEHICLE_PERTYPE_ID=''                  
END                   
                  
--FOR SYMBOL          
IF(@INTSYMBOL>26)                                 
 BEGIN                               
 SET @SYMBOL='Y'                              
 END                               
ELSE IF(@INTSYMBOL=0 and @APP_VEHICLE_PERTYPE_ID NOT IN (11868,11869))                              
 BEGIN                               
 SET @SYMBOL=''                              
 END             
ELSE IF(@INTSYMBOL=0 and @APP_VEHICLE_PERTYPE_ID IN (11868,11869))         
 BEGIN            
 SET @SYMBOL='N'              
 END                              
ELSE IF(@INTSYMBOL<>0)                              
 BEGIN                               
 SET @SYMBOL='N'                              
 END                               
-------------------------------------------------------------------------------------------------                     
                  
                  
-----------------------------------------------------------------------------------------------                  
/*If State is Indiana On vehicle   coverage tab If No in the Signature Field   for any of these coverages                   
  Refer to underwriter                   
  Underinsured Motorist (CSL) Uninsured Motorist (CSL)                   
  Underinsured Motorist (PD)  Uninsured Motorist (PD)                  
  Underinsured Motorist (BI)  Uninsured Motorist (BI)*/                   
                  
--1. Uninsured Motorist (CSL) -- 9                  
DECLARE @PUNCS CHAR                  
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                        
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (9) AND SIGNATURE_OBTAINED='N')                   
 BEGIN                   
 SET @PUNCS='Y'             
 END                  
ELSE                  
 BEGIN               
 SET @PUNCS='N'                  
 END                   
                  
--2 Underinsured Motorist (CSL) -- 14                  
DECLARE @UNCSL CHAR                  
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                             
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (14) AND SIGNATURE_OBTAINED='N')                   
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
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                             
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (36) AND SIGNATURE_OBTAINED='N')                   
 BEGIN                   
 SET @UMPD='Y'                  
 END                  
ELSE                  
 BEGIN                   
 SET @UMPD='N'                  
 END                   
                  
--5 Underinsured Motorist (BI)-- 34                  
DECLARE @UNDSP CHAR                  
IF EXISTS (SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                          
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID AND COVERAGE_CODE_ID IN (34) AND SIGNATURE_OBTAINED='N')                   
BEGIN                   
SET @UNDSP='Y'                  
END                  
ELSE                  
BEGIN                   
SET @UNDSP='N'                  
END                   
                  
--6 Uninsured Motorist (BI)-- 12                  
declare @PUMSP char                  
 if exists (select SIGNATURE_OBTAINED from POL_VEHICLE_COVERAGES                             
   where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID and COVERAGE_CODE_ID in (12) and SIGNATURE_OBTAINED='N')                   
begin                   
 set @PUMSP='Y'                  
end                  
else                  
begin                   
 set @PUMSP='N'                  
end                        
-------------------------------------------------------------------------------------------------                      
-- 333. If on the Vehicle Coverage Tab Loan/Lease Gap coverage is checked off for any of the vehicles in the lister                   
-- Then this risk is submitted to underwriters                    
 declare @LLGC  char                  
 if exists (select SIGNATURE_OBTAINED                   
  from POL_VEHICLE_COVERAGES                             
  where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID  and COVERAGE_CODE_ID in (46,249))                   
 begin                   
--  set @LLGC ='Y'      new Itrack 4494 rule change to new rule            
 set @LLGC ='N'             
 end                  
 else                  
 begin                   
  set @LLGC ='N'                  
 end                  
-------------------------------------------------------------------------------------------------                
/*                  
If Extended Non- Owned Coverage for Named Insured is checked off, then when doing the verify make sure that                   
Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers             
that have a yes in the Field Extended Non Owned Coverages Required.                  
If there is a yes in the Field Extended Non Owned Coverages Required on the                   
Drivers/Household members tab                   
Then make sure  Extended Non- Owned Coverage for Named Insured is checked off */                  
declare @ENO  char                  
declare @LIMIT_1 int                  
                  
if exists (select SIGNATURE_OBTAINED from POL_VEHICLE_COVERAGES                             
 where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID and COVERAGE_CODE_ID in (52,254))                   
begin                   
 select  @LIMIT_1=LIMIT_1 from POL_VEHICLE_COVERAGES                   
 where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID  and COVERAGE_CODE_ID in (52,254)                  
end                   
-- Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                   
-- that have a yes in the Field Extended Non Owned Coverages Required                   
                  
declare @intCount int                   
                  
if exists (select CUSTOMER_ID from POL_DRIVER_DETAILS                
 where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID)                  
begin                   
select @intCount= count(isnull(Driver_ID,0)) from POL_DRIVER_DETAILS                    
 where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID   and VEHICLE_ID=@VEHICLE_ID and EXT_NON_OWN_COVG_INDIVI='10963'                  
end                   
--                   
if(@LIMIT_1<>@intCount)                  
 set @ENO='Y'                  
else                  
 set @ENO='N'                  
--------------------------------------------------------------------------------------------------------------------                  
--------------------------------------------------------------------------------------------------------------------                  
-- Indiana : Excluded Person(s) Endorsement A-96 If this is checked off on the Vehicle Coverage Tab                   
-- then in the verify process make sure we have a least one Excluded driver and                   
-- also Sig obtained field for this coverage is "Yes" -- Refer to UW                 
             
--Changed by Pravesh on 19 sep08 and implement for both state Itrack 4773                  
DECLARE @EPENDO_SIGN CHAR                  
 SET  @EPENDO_SIGN='N' -- DEFAULT                  
                  
IF ( (EXISTS(SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                             
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID                  
   AND COVERAGE_CODE_ID IN (1010,1002) --AND SIGNATURE_OBTAINED='Y'            
   )                   
   AND            
  NOT EXISTS( SELECT DRIVER_ID FROM POL_DRIVER_DETAILS                   
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID              
     AND DRIVER_DRIV_TYPE=3477  AND isnull(FORM_F95,0)= 10963 AND isnull(IS_ACTIVE,'Y')='Y')            
 )                  
 OR             
 (            
  EXISTS( SELECT DRIVER_ID FROM POL_DRIVER_DETAILS                   
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID              
     AND DRIVER_DRIV_TYPE=3477 AND isnull(FORM_F95,0)= 10963 AND isnull(IS_ACTIVE,'Y')='Y')            
  AND            
              
  NOT EXISTS(SELECT SIGNATURE_OBTAINED FROM POL_VEHICLE_COVERAGES                             
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND VEHICLE_ID=@VEHICLE_ID                  
   AND COVERAGE_CODE_ID IN (1010,1002) --AND SIGNATURE_OBTAINED='Y'            
   )                   
 )            
  )            
 BEGIN                   
  SET @EPENDO_SIGN='Y'                  
 END               
            
IF(@APP_VEHICLE_COMTYPE_ID IN (11340,11341) OR @APP_VEHICLE_PERTYPE_ID IN (11337,11870))            
BEGIN            
  SET @EPENDO_SIGN='N'             
END               
----------------------------------------------------                  
-- If State is Indiana Then look at the Vehicle Infor Tab for all vehicles on the policy If the Registered State is                   
-- other then Indiana on any of the vehicles than Submit                   
 declare @STATE_ID varchar(30)                  
declare @REG_STATE char                  
 set @REG_STATE ='N'                  
                   
                  
SELECT @STATE_ID = CONVERT(VARCHAR(30),STATE_ID) FROM POL_CUSTOMER_POLICY_LIST   with(nolock) --by pravesh                
WHERE CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                  
                  
 if (@STATE_ID='14')                  
 begin                   
 if exists( select REGISTERED_STATE from POL_VEHICLES                   
   where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                  
   and VEHICLE_ID=@VEHICLE_ID and  REGISTERED_STATE<>'14')                  
 begin                   
set @REG_STATE='Y'                  
 end                  
 end                  
-----------------------------------------------------------------------------------------                  
--If State is Michigan                   
--If Vehicle Type is Motor Home, Truck or Van Camper                   
--If Yes to Is this Vehicle used for Business or Permanent Residence? then Submit to Underwriters                   
                  
 declare @VEHICLE_TYPE_MHT char                 
 set @VEHICLE_TYPE_MHT = 'N'                  
                  
if(@STATE_ID='22')                  
begin               
 if exists (select APP_VEHICLE_PERTYPE_ID from POL_VEHICLES                   
  where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                    
    and VEHICLE_ID=@VEHICLE_ID and APP_VEHICLE_PERTYPE_ID='11336' and BUSS_PERM_RESI=10963)                  
 begin                   
  set @VEHICLE_TYPE_MHT='Y'                  
 end                  
end                 
            
-----------14 may 2007            
-- 4) If Vehicle Use is  commercial , Vehicle Type Is Long Haul Send message in Red "Do not write this type of Risk"                  
-- Refer to underwriters if still submitted                   
declare @COMM_LONGHAUL char                  
set @COMM_LONGHAUL ='N'                  
                  
if(@USE_VEHICLE = '11333' and @APP_VEHICLE_COMTYPE_ID='11871') --Comm.                  
begin                   
 set @COMM_LONGHAUL='Y'                  
end              
            
-- If Vehicle Use is Commercial If Vehicle type is Intermediate Haul then                   
-- If the Radius Field  is not with 75 -150 miles Refer to Underwriters                   
 declare @COMM_INTER_LH char                  
 set @COMM_INTER_LH='N'           
                  
if(@USE_VEHICLE='11333' and @intRADIUS_OF_USE not between 75 and 150 and @APP_VEHICLE_COMTYPE_ID='11338')                  
begin                   
 set @COMM_INTER_LH='Y'                  
end                 
            
-- Vehicle Info Tab If the Model is a Corvette on any of the automobiles in the lister                 
-- Look at the Underwriting Question Is multi policy discount applied? If No Refer to Underwriters                  
declare @MODEL_NAME_MULTI_DIS char                
set @MODEL_NAME_MULTI_DIS ='N'                
                
if exists (select CUSTOMER_ID from POL_AUTO_GEN_INFO                 
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                   
  and @MODEL in ('CORVETTE','CORVETTE Z06')and MULTI_POLICY_DISC_APPLIED='0')                
begin                 
  set @MODEL_NAME_MULTI_DIS='Y'                    
end             
            
            
-- There should be atleast one "Pricipal" or "Youthful Principal" driver for each vehicle added in application. Else Refer.                  
                  
declare @YOUTHFUL_PRINC_DRIVER char             
set @YOUTHFUL_PRINC_DRIVER='N'                  
                  
if (not exists(select PDAV.APP_VEHICLE_PRIN_OCC_ID                   
    from POL_DRIVER_ASSIGNED_VEHICLE PDAV INNER JOIN POL_DRIVER_DETAILS PDDS          
 ON PDAV.CUSTOMER_ID=PDDS.CUSTOMER_ID          
  AND PDAV.POLICY_ID=PDDS.POLICY_ID          
  AND PDAV.POLICY_VERSION_ID=PDDS.POLICY_VERSION_ID          
  AND PDAV.DRIVER_ID=PDDS.DRIVER_ID           
    where PDAV.CUSTOMER_ID=@CUSTOMER_ID and  PDAV.POLICY_ID=@POLICY_ID  and  PDAV.POLICY_VERSION_ID=@POLICY_VERSION_ID                  
    and PDAV.APP_VEHICLE_PRIN_OCC_ID in (11399,11398,11930,11929)and PDAV.VEHICLE_ID=@VEHICLE_ID AND PDDS.IS_ACTIVE='Y'           
     )                
  AND @APP_VEHICLE_PERTYPE_ID NOT IN ('11618','11337','11870') AND @APP_VEHICLE_COMTYPE_ID !='11341'            
 )            
            
begin                   
 set @YOUTHFUL_PRINC_DRIVER='Y'                  
end              
             
--------------------------------------------------------------------------------            
--      Vehicle must have one prin OR youthOccas OR YuthPrin(Start)            
--------------------------------------------------------------------------------            
DECLARE @PRINC_OCCA_DRIVER NVARCHAR(50)            
SET @PRINC_OCCA_DRIVER = 'N'            
IF ( EXISTS(SELECT ADAV.APP_VEHICLE_PRIN_OCC_ID                   
   FROM POL_DRIVER_ASSIGNED_VEHICLE  ADAV            
   INNER JOIN POL_DRIVER_DETAILS AD ON AD.CUSTOMER_ID=ADAV.CUSTOMER_ID             
   AND AD.POLICY_ID=ADAV.POLICY_ID AND AD.POLICY_VERSION_ID=ADAV.POLICY_VERSION_ID             
   AND AD.DRIVER_ID=ADAV.DRIVER_ID AND ISNULL(AD.IS_ACTIVE,'Y')='Y'            
                 
   WHERE ADAV.CUSTOMER_ID=@CUSTOMER_ID AND  ADAV.POLICY_ID=@POLICY_ID  AND  ADAV.POLICY_VERSION_ID=@POLICY_VERSION_ID                  
   AND ADAV.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929,11928,11927)AND ADAV.VEHICLE_ID=@VEHICLE_ID            
   )                  
 OR @APP_VEHICLE_PERTYPE_ID IN ('11618','11337','11870') OR @APP_VEHICLE_COMTYPE_ID ='11341'            
 )            
BEGIN                   
 SET @PRINC_OCCA_DRIVER='Y'                  
END              
--------------------------------------------------------------------------------            
--      Vehicle must have one prin, youthOccas,YuthPrin(End)            
--------------------------------------------------------------------------------            
            
----------              
--------------------------------------------------                  
-- For Personal Auto, it is mandatory only in case of 'Private Pasenger' and  'Motor home'.                  
--11336- Motor home, Truck or Van Campers ,11334- Private Passenger                   
                  
if(@CLASS_PER='0' and @USE_VEHICLE='11332' and (@APP_VEHICLE_PERTYPE_ID='11336'or @APP_VEHICLE_PERTYPE_ID='11334'))                  
begin                   
 set @CLASS_PER=''                  
end                  
else                  
 set @CLASS_PER='N'                  
----              
                  
--For Commercial Auto, it is mandatory for all except 'Trailer' -             
               
if(@CLASS_COM='0' and @USE_VEHICLE='11333' and @APP_VEHICLE_COMTYPE_ID<>'11341' )                  
 set @CLASS_COM=''                  
else                  
 set @CLASS_COM='N'                  
-------------------------------------                  
            
DECLARE @VEHICLE_TYPE_PER VARCHAR(10),@VEHICLE_TYPE_COM VARCHAR(10)            
SET @VEHICLE_TYPE_PER =@APP_VEHICLE_PERTYPE_ID            
SET @VEHICLE_TYPE_COM = @APP_VEHICLE_COMTYPE_ID                
             
if(@USE_VEHICLE='11333')--Commercial                  
 set @APP_VEHICLE_PERTYPE_ID='N'                  
                  
if(@USE_VEHICLE='11332')--Personal                  
 set @APP_VEHICLE_COMTYPE_ID='N'               
-----------------------------------------------------------------------------------------                
-- Vehicle Info Tab Territory Field The territory is based on the Garaging Location Fields                 
-- If Class 5C (11357) Then look at the Vehicle Info Tab for Garaging Location / Registered State Field                 
-- must equal the State Field on the Application/Policy Details Tab If Not then refer to Underwriters                 
-- Class 5C chk removed                
declare @CREG_STATE char                 
set @CREG_STATE='N'                
                
if((@GRG_STATE<>@REGISTERED_STATE or @STATE_ID<>@REGISTERED_STATE))                
begin                 
 set @CREG_STATE='Y'                
end                 
--------------------------------------------             
-- Garaging state of all vehicle must be same as of main application state Reject in this case                 
DECLARE @GRG_STATE_RULE CHAR              
 IF(@GRG_STATE<>@STATE_ID)              
 BEGIN              
SET @GRG_STATE_RULE='Y'              
 END                 
-----------------------------------------------------------------------------------------             
-- 11272                
-- SNOWPLOW_CONDS --11912                
--Application/Policy Details If State is Indiana Vehicle Info Tab If Use is Snowplowing                 
--Then look at the Snowplow conditions If Full time - Refer to Underwriters                  
DECLARE @INSNOW_FULL CHAR                
SET @INSNOW_FULL='N'            
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_VEHICLES                 
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                 
 AND VEHICLE_ID = @VEHICLE_ID AND SNOWPLOW_CONDS='11912'                 
 AND VEHICLE_USE='11272' AND (@STATE_ID=14 or @STATE_ID=22))     --add condition for state michigan on 3 july 2008 by pravesh as per itrack 4446            
 BEGIN                 
 SET @INSNOW_FULL='Y'                
 END             
             
--=========================================================================            
  --Grandfather case for Territory Code            
--=========================================================================            
/*DECLARE @APP_EFF_DATE DATETIME            
SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID            
            
SELECT TOP 1 MTC.TERR AS TERRITORY_DESCRIPTION FROM POL_VEHICLES AVC                                              
 INNER JOIN MNT_TERRITORY_CODES MTC  ON  AVC.GRG_ZIP=MTC.ZIP  AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=2            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID            
 AND NOT(@APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')              
 AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')              
 ) */            
             
 DECLARE @APP_EFF_DATE DATETIME ,             
  @TERR Int,            
  @GRANDFATHER_TERRITORY VARCHAR            
              
 SELECT @APP_EFF_DATE=APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID            
            
 SELECT @TERR = TERRITORY FROM POL_VEHICLES AVC            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID             
            
 IF EXISTS (        
  SELECT MTC.* FROM POL_VEHICLES AVC                                              
  INNER JOIN MNT_TERRITORY_CODES MTC              
  ON  AVC.TERRITORY = MTC.TERR AND SUBSTRING(AVC.GRG_Zip,0,6) = SUBSTRING(MTC.ZIP,0,6) AND   AVC.GRG_STATE=MTC.STATE AND MTC.LOBID=2            
  AND MTC.TERR = @TERR AND @APP_EFF_DATE  BETWEEN ISNULL(MTC.EFFECTIVE_FROM_DATE,'1950-1-1')              
  AND ISNULL(MTC.EFFECTIVE_TO_DATE,'3000-12-31')             
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
 )            
 BEGIN             
  SET @GRANDFATHER_TERRITORY='N' END            
 ELSE            
 BEGIN             
  SET @GRANDFATHER_TERRITORY='Y'            
 END            
----> Mandatory Vehicle Use Only for Personal Type            
     IF(@USE_VEHICLE='11333')            
     BEGIN            
 SET @VEHICLE_USE='N'             
     END            
---> In case of  personal            
      IF(@USE_VEHICLE='11332')            
      BEGIN            
 SET @INTRADIUS_OF_USE=1            
 SET @TRANSPORT_CHEMICAL='N'            
 --SET @COVERED_BY_WC_INSU='N'              
      END            
---> If Vehicle Use personal and If commercial than State Not Michigan            
 IF(@USE_VEHICLE='11332' OR (@USE_VEHICLE='11333' AND @STATE_ID!='22' ))            
       BEGIN            
 SET @COVERED_BY_WC_INSU='N'            
 END            
--========================================================================               
--Added by Pravesh on 17 july 2008 as per iTRACK # 4511             
--3- If there is Miscellaneous Equipment and they have not selected Other than Collision on the Vehicle Coverage tab – Put a not in the Verification process             
--      No coverage has been applied to the Miscellaneous Equipment            
--42 OTC  Other than Collision (Comprehensive)            
--123 COMP Other Than Collision (Comprehensive)            
DECLARE @MIS_EQUIP_COUNT INT,@OT_COLL_COUNT INT, @MIS_EQUIP_COV CHAR(1)            
SELECT  @MIS_EQUIP_COUNT=COUNT(*) FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID AND ITEM_VALUE<>0            
SELECT  @OT_COLL_COUNT=COUNT(*) FROM POL_VEHICLE_COVERAGES             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
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
  SELECT CUSTOMER_ID FROM POL_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID              
  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
  AND COVERAGE_CODE_ID IN (116) AND LIMIT_ID IN (687,688,1374,1375)            
  )            
 BEGIN            
  SELECT @ADD_INFORMATION=ISNULL(ADD_INFORMATION,'') FROM POL_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID              
  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
  AND COVERAGE_CODE_ID IN (116)             
  IF(@ADD_INFORMATION='')            
   SET @HELTH_CARE=''            
 END            
--end here            
--added by pravesh on 22 AUG 2008 Itrack 4494            
DECLARE @LEASED_PURCHASED_DATE DateTIME,@LEASED_PURCHASED  CHAR(1),@ADD_INT_LOAN_LEAN CHAR(1)            
SET @LEASED_PURCHASED='N'            
SET @ADD_INT_LOAN_LEAN='N'            
SELECT @LEASED_PURCHASED_DATE=PURCHASE_DATE FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID             
  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID             
IF EXISTS (SELECT SIGNATURE_OBTAINED                 
 FROM POL_VEHICLE_COVERAGES                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID  AND COVERAGE_CODE_ID IN (46,249)            
 )             
BEGIN            
  IF((@LEASED_PURCHASED_DATE IS NULL))-- AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y'))            
  BEGIN   -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592                
  SET @LEASED_PURCHASED='Y'            
   END            
  ELSE IF ((DATEDIFF(DD,@LEASED_PURCHASED_DATE,@APP_EFF_DATE) > 90))-- AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y'))            
  BEGIN   -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592                
  SET @LEASED_PURCHASED='Y'            
  END            
     -- NEW RULE AS PER iTRACK 4493            
      IF NOT EXISTS(select CUSTOMER_ID from POL_ADD_OTHER_INT             
  where CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID             
  )            
            SET @ADD_INT_LOAN_LEAN='Y'             
END            
--end here            
 --added by pravesh on 29 AUG 2008 Itrack 4649/ 4496            
DECLARE @BI_CSL_EXISTS CHAR(1)            
DECLARE @IS_CSL CHAR(1)            
SET @BI_CSL_EXISTS='N'            
set @IS_CSL='N'            
            
IF (@STATE_ID=14 AND @VEHICLE_TYPE_PER NOT IN ('11618','11337','11870') AND @VEHICLE_TYPE_COM !='11341' and @IS_SUSPENDED!=10963)            
BEGIN            
 IF EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
      AND COVERAGE_CODE_ID IN (1)            
   )            
 set @IS_CSL='Y'            
            
   IF NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
    --AND COVERAGE_CODE_ID IN (36,12,34,9,14)            
      AND COVERAGE_CODE_ID IN (9,12)            
   )            
  begin            
   SET @BI_CSL_EXISTS='Y'            
  end            
  else            
  begin            
     IF (            
    (EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
     AND COVERAGE_CODE_ID IN (9) AND ISNULL(LIMIT1_AMOUNT_TEXT,'')<>'Reject')            
    AND             
                  (            
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
     AND COVERAGE_CODE_ID IN (14))            
    OR            
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
     AND COVERAGE_CODE_ID IN (36))            
      )             
    )            
    OR            
    (EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
     AND COVERAGE_CODE_ID IN (12) AND ISNULL(LIMIT1_AMOUNT_TEXT,'')<>'Reject')            
    AND (            
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
     AND COVERAGE_CODE_ID IN (34))            
    OR            
    NOT EXISTS(SELECT  COVERAGE_CODE_ID FROM POL_VEHICLE_COVERAGES WITH(NOLOCK)            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VEHICLE_ID=@VEHICLE_ID            
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
            
 --added by pravesh on 24 sep 2008 Itrack 4777            
DECLARE @GOOD_STUDENT_DISCOUNT NVARCHAR(10)            
SET @GOOD_STUDENT_DISCOUNT='N'            
EXEC Proc_GetGoodStudent_Pol @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@VEHICLE_ID,@GOOD_STUDENT_DISCOUNT OUTPUT            
            
IF (@GOOD_STUDENT_DISCOUNT='FALSE'            
 AND             
  EXISTS(            
   SELECT ADDS.DRIVER_ID FROM  POL_DRIVER_ASSIGNED_VEHICLE ADDS WITH (NOLOCK)             
   INNER JOIN POL_VEHICLES AV WITH (NOLOCK)                              
   ON AV.CUSTOMER_ID=ADDS.CUSTOMER_ID AND                              
    AV.POLICY_ID=ADDS.POLICY_ID AND                      
    AV.POLICY_VERSION_ID = ADDS.POLICY_VERSION_ID AND                               
    AV.VEHICLE_ID=ADDS.VEHICLE_ID and            
    AV.CLASS_DRIVERID = ADDS.DRIVER_ID                              
   INNER JOIN POL_DRIVER_DETAILS WITH (NOLOCK)                              
   ON ADDS.CUSTOMER_ID=POL_DRIVER_DETAILS.CUSTOMER_ID AND                              
    ADDS.POLICY_ID=POL_DRIVER_DETAILS.POLICY_ID AND                              
    ADDS.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID                    
    AND  ADDS.DRIVER_ID=POL_DRIVER_DETAILS.DRIVER_ID                              
   WHERE ADDS.CUSTOMER_ID=@CUSTOMER_ID AND ADDS.POLICY_ID=@POLICY_ID AND ADDS.POLICY_VERSION_ID=@POLICY_VERSION_ID AND ADDS.VEHICLE_ID=@VEHICLE_ID              
   AND (ISNULL(POL_DRIVER_DETAILS.DRIVER_GOOD_STUDENT,0)=1)             
   )            
 )            
 SET @GOOD_STUDENT_DISCOUNT='Y'            
ELSE            
 SET @GOOD_STUDENT_DISCOUNT='N'            
            
--end here              
--Rule added by Pravesh on 30 Sep 20008 Itrack 4822            
--We are looking for a referral on the 1st renewal of all Motorhome polices that have been converted             
--If policy has a vehicle type of Motorhome and this is on a converted policy from AS400 then when processing the renewal the underwriters will get a Referral             
--Message - Compare Deductibles with the AS400 -             
--11336 1209 3 MH Motor home, Truck or Van Campers            
DECLARE @HOME_AS400_RENEWAL CHAR(1),@RENEW_COUNT INT            
SET @HOME_AS400_RENEWAL='N'            
IF (@VEHICLE_TYPE_PER='11336'             
 AND EXISTS(            
  SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock)             
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID              
  AND  POLICY_VERSION_ID=(SELECT MIN(POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID)            
  AND ISNULL(FROM_AS400,'')='Y'            
  )            
 )            
BEGIN            
 SELECT @RENEW_COUNT=COUNT(CUSTOMER_ID) FROM POL_POLICY_PROCESS WITH(NOLOCK)             
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID              
  AND  (POLICY_VERSION_ID=@POLICY_VERSION_ID OR NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID )            
  AND PROCESS_ID IN (5,18)            
 IF(@RENEW_COUNT=1)            
  SET @HOME_AS400_RENEWAL='Y'            
            
END            
--End Here            
-- by Pravesh on 13 Nov 08            
/*=============================================================            
Itrack 4862 Should not be able to commit if more than 1 principal driver on a vehicle             
==============================================================*/             
            
DECLARE @PRINCIPLE_OPERATOR CHAR            
DECLARE @INTCOUNT1 INT             
SET @PRINCIPLE_OPERATOR='N'            
            
SELECT @INTCOUNT1=COUNT(AOPB.APP_VEHICLE_PRIN_OCC_ID) FROM POL_DRIVER_ASSIGNED_VEHICLE AOPB            
INNER JOIN POL_DRIVER_DETAILS AWDD            
ON AOPB.CUSTOMER_ID=AWDD.CUSTOMER_ID AND AOPB.POLICY_ID=AWDD.POLICY_ID AND AOPB.POLICY_VERSION_ID=AWDD.POLICY_VERSION_ID            
and AWDD.DRIVER_ID = AOPB.DRIVER_ID            
WHERE AOPB.CUSTOMER_ID=@CUSTOMER_ID AND AOPB.POLICY_ID=@POLICY_ID             
AND AOPB.POLICY_VERSION_ID =@POLICY_VERSION_ID AND  AOPB.VEHICLE_ID=@VEHICLE_ID             
AND AOPB.APP_VEHICLE_PRIN_OCC_ID IN (11399,11398,11930,11929)            
AND IS_ACTIVE='Y'          
            
 IF(@INTCOUNT1 > 1)            
 BEGIN            
  SET @PRINCIPLE_OPERATOR='Y'            
 END            
-- end here            
--Rule added by Pravesh on 27 Nov 08 as per Itrack 5057            
declare @BEYOND_50_MILES CHAR            
SET @BEYOND_50_MILES ='N'            
IF(@MILES_TO_WORK>50 and @USE_VEHICLE='11332' AND @STATE_ID='22')            
 SET @BEYOND_50_MILES ='Y'            
-- eND hERE            
            
--------------------------------------------------------------------             
---------------------------------------------------------------------------------------------------            
-- If commercial vehicle and Bi Coverage limit is 300/500000 then policy will be rejected (Start)            
---------------------------------------------------------------------------------------------------            
DECLARE @BICOMNA NVARCHAR(10)            
DECLARE @BISPLITLIMIT NVARCHAR(50)            
SET @BISPLITLIMIT = ''            
SET @BICOMNA = 'N'            
IF(@VEHICLE_TYPE_COM IN ('11338','11339','11340','11341','11871'))            
BEGIN            
 SELECT @BISPLITLIMIT =  Substring(convert(varchar(30),convert(money,isnull(PVC.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(PVC.LIMIT_1,0)),1),0))                                      
    +'/'                                          
    + Substring(convert(varchar(30),convert(money,isnull(PVC.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(PVC.LIMIT_2,0)),1),0))                                          
    FROM POL_VEHICLE_COVERAGES PVC INNER JOIN             
 MNT_COVERAGE MC ON             
 PVC.COVERAGE_CODE_ID = MC.COV_ID            
            
 WHERE PVC.CUSTOMER_ID=@CUSTOMER_ID AND PVC.POLICY_ID=@POLICY_ID AND PVC.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PVC.VEHICLE_ID=@VEHICLE_ID AND MC.COV_CODE ='BISPL'            
END            
IF(@BISPLITLIMIT = '300/500,000' OR @BISPLITLIMIT = '300,000/500,000')            
BEGIN            
 SET @BICOMNA = 'Y'            
END            
            
---------------------------------------------------------------------------------------------------            
-- If commercial vehicle and Bi Coverage limit is 300/500000 then policy will be rejected (End)            
---------------------------------------------------------------------------------------------------               
            
            
-- This Rule Moved From Driver Level to Vehicle LEvel on 27 Jan 2009 Itrack 4771 by Pravesh            
/*                                      
If Extended Non- Owned Coverage for Named Insured is checked off, then when doing the verify make sure that                     
Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                                       
that have a yes in the Field Extended Non Owned Coverages Required.              
If there is a yes in the Field Extended Non Owned Coverages Required on the                                       
Drivers/Household members tab                                       
Then make sure  Extended Non- Owned Coverage for Named Insured is checked off */                         
                          
--declare @ENO  char                     
set @ENO='N'                                   
declare @ADD_INFORMATION1 varchar(20)                                      
select @ADD_INFORMATION1=isnull(ADD_INFORMATION,'0')                                      
  from POL_VEHICLE_COVERAGES COV                                      
   where  COV.CUSTOMER_ID=@CUSTOMER_ID AND  COV.POLICY_ID=@POLICY_ID  AND  COV.POLICY_VERSION_ID=@POLICY_VERSION_ID                  
   AND  COV.COVERAGE_CODE_ID IN (52,254)                                       
   AND  COV.VEHICLE_ID = @VEHICLE_ID            
-- Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                              
-- that have a yes in the Field Extended Non Owned Coverages Required                              
declare @intCount2 int                  
select @intCount2= count(isnull(Driver_ID,0)) from POL_DRIVER_DETAILS                                    
where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID              
 AND  isnull(EXT_NON_OWN_COVG_INDIVI,'0')='10963'                                  
--                                       
if(CAST(@ADD_INFORMATION1 AS NUMERIC)<>@intCount2)                        
 set @ENO='Y'                                      
else                                      
 set @ENO='N'                    
-- end here       
            
---------Added TrailBlazr Discount : Praveen Kasana            
DECLARE @QUALIFIESTRAIBLAZERPROGRAM VARCHAR(22) 
SET @QUALIFIESTRAIBLAZERPROGRAM = 'N' 
	IF(@QUOTEEFFECTIVEDATE<@TRAILBLAZER_EXPIRY_DATE)
		BEGIN          
			EXECUTE Proc_CheckTrailBlazerEligibilityRule @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@VEHICLE_ID,'POL',@QUALIFIESTRAIBLAZERPROGRAM OUT            
		END
          
--------------------------ADDED AUTO POLICY NUMBER:PRAVEEN KUMAR(05-03-2009):ITRACK 5544        
DECLARE @AUTO_POLICY_NO VARCHAR(12)        
 SET @AUTO_POLICY_NO = 'N'        
DECLARE @MULTI_CAR NVARCHAR(5)        
SELECT @AUTO_POLICY_NO = ISNULL(AUTO_POL_NO,''),@MULTI_CAR = ISNULL(MULTI_CAR,'') FROM POL_VEHICLES         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID        
        
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
    
SELECT @PERSVEHICLEIDS = COUNT(VEHICLE_ID) FROM POL_VEHICLES WITH (NOLOCK)     
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID and IS_ACTIVE = 'Y'     
AND APP_USE_VEHICLE_ID =11332 and APP_VEHICLE_PERTYPE_ID !=@CAMPER_TRAVEL_TRAILER and APP_VEHICLE_PERTYPE_ID != @UTILITY_TRAILER        
    
SELECT @COMSVEHICLEIDS = COUNT(VEHICLE_ID) FROM POL_VEHICLES WITH (NOLOCK)     
WHERe CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID and IS_ACTIVE = 'Y'      
AND APP_USE_VEHICLE_ID =11333 and APP_VEHICLE_COMTYPE_ID !=@TRAILER and APP_VEHICLE_COMTYPE_ID != @TRAILER_INTERMITTENT        
    
SET @TOTAL_VEHICLE_COUNT = @PERSVEHICLEIDS + @COMSVEHICLEIDS    
    
DECLARE @MULTI_CAR_PPA int    
DECLARE @MULTI_CAR_COM int    
DECLARE @MULTICAR_DISCOUNT_ELIGIBLE NVARCHAR(5)    
    
SELECT @MULTI_CAR_PPA = ISNULL(MULTI_CAR,0) FROM POL_VEHICLES WITH (NOLOCK)    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID    
AND VEHICLE_ID=@VEHICLE_ID  AND IS_ACTIVE = 'Y'    
AND APP_USE_VEHICLE_ID =11332 AND APP_VEHICLE_PERTYPE_ID !=@CAMPER_TRAVEL_TRAILER AND APP_VEHICLE_PERTYPE_ID != @UTILITY_TRAILER    
    
    
SELECT @MULTI_CAR_COM = ISNULL(MULTI_CAR,0) FROM POL_VEHICLES WITH (NOLOCK)    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID    
AND VEHICLE_ID=@VEHICLE_ID AND IS_ACTIVE = 'Y'    
AND APP_USE_VEHICLE_ID =11333 and APP_VEHICLE_COMTYPE_ID !=@TRAILER AND APP_VEHICLE_COMTYPE_ID != @TRAILER_INTERMITTENT    
    
SET @MULTICAR_DISCOUNT_ELIGIBLE  = 'N'    
    
IF(@TOTAL_VEHICLE_COUNT > 1)    
BEGIN    
 IF(@MULTI_CAR_PPA = @NOT_APPLICABLE OR @MULTI_CAR_COM = @NOT_APPLICABLE)    
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
 @VEHICLE_YEAR as VEHICLE_YEAR,                                                          
 @MAKE as MAKE,                          
 @MODEL as MODEL,                                                          
 @VIN as VIN,            
 @ANTIQUE_VECH AS ANTIQUE_VECH,                               
 @VEHICLE_USE as VEHICLE_USE,                        
 --@INSURED_VEH_NUMBER as INSURED_VEH_NUMBER,                                                    
 @GRG_ADD1 as GRG_ADD1,                                                    
 @GRG_CITY  as GRG_CITY,                     
 @GRG_STATE as GRG_STATE,                                                    
 @GRG_ZIP as GRG_ZIP,                                                    
 --@TERRITORY as TERRITORY,                                                    
 @AMOUNT as  AMOUNT ,                                    
 @MODEL_NAME as MODEL_NAME,                           
 @MAKE_NAME as MAKE_NAME ,                                            
 @USE_VEHICLE as USE_VEHICLE,                                  
 @VEHICLE_AMOUNT as VEHICLE_AMOUNT,                                
 @SYMBOL as SYMBOL,                  
 @APP_VEHICLE_PERTYPE_ID as VEHICLE_TYPE_PER,                                                     
 @PUNCS as PUNCS,      
 @BICOMNA AS BICOMNA,                  
 @UNCSL as UNCSL,                   
 @UMPD  as  UMPD,                  
 @UNDSP as UNDSP,                  
 @PUMSP as PUMSP,                  
 @LLGC as LLGC,                  
 @EPENDO_SIGN as EPENDO_SIGN,                  
 @REG_STATE AS REG_STATE,                  
 @VEHICLE_TYPE_MHT as VEHICLE_TYPE_MHT,                  
 @CLASS_COM as CLASS_COM,                  
 @CLASS_PER as CLASS_PER,                  
 @APP_VEHICLE_COMTYPE_ID as VEHICLE_TYPE_COM ,             
 @INSNOW_FULL as INSNOW_FULL,             
 @CREG_STATE AS CREG_STATE,            
 @COMM_RADIUS_OF_USE as COMM_RADIUS_OF_USE ,            
 @GRG_STATE_RULE  as  GRG_STATE_RULE,            
 @YOUTHFUL_PRINC_DRIVER as YOUTHFUL_PRINC_DRIVER,            
 @COMM_LOCALHAUL  as COMM_LOCALHAUL,            
 @COMM_LONGHAUL as COMM_LONGHAUL,            
 @VEHICLE_AMOUNT_COM AS VEHICLE_AMOUNT_COM,                
 @COMM_INTER_LH as COMM_INTER_LH  ,            
 @MODEL_NAME_MULTI_DIS as MODEL_NAME_MULTI_DIS,            
 @GRANDFATHER_TERRITORY AS GRANDFATHER_TERRITORY,            
 @INTRADIUS_OF_USE AS RADIUS_OF_USE ,            
 @TRANSPORT_CHEMICAL AS TRANSPORT_CHEMICAL,            
 @COVERED_BY_WC_INSU   AS COVERED_BY_WC_INSU,            
 @MIS_EQUIP_COV AS MIS_EQUIP_COV,            
 @HELTH_CARE as HELTH_CARE,           @OT_COLL_COUNT as OT_COLL_COUNT,            
 @PRINC_OCCA_DRIVER AS PRINC_OCCA_DRIVER,            
 @LEASED_PURCHASED AS LEASED_PURCHASED,            
 @ADD_INT_LOAN_LEAN  AS ADD_INT_LOAN_LEAN,            
 @BI_CSL_EXISTS AS BI_CSL_EXISTS,            
 @IS_CSL as IS_CSL,            
 @ANTIQUE_CLASSIC_CAR as ANTIQUE_CLASSIC_CAR,            
 @GOOD_STUDENT_DISCOUNT AS GOOD_STUDENT_DISCOUNT,            
 @HOME_AS400_RENEWAL AS HOME_AS400_RENEWAL,            
 @PRINCIPLE_OPERATOR as PRINCIPLE_OPERATOR,            
 @BEYOND_50_MILES as BEYOND_50_MILES,            
 @ENO as ENO,            
 @QUALIFIESTRAIBLAZERPROGRAM AS QUALIFIESTRAIBLAZERPROGRAM ,        
 @AUTO_POLICY_NO AS AUTO_POL_NO,    
 @MULTICAR_DISCOUNT_ELIGIBLE AS MULTICAR_DISCOUNT_ELIGIBLE               
 END 


GO

