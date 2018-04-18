IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_RatingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_RatingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--
 /* ----------------------------------------------------------                                                                                                                                                                          
Proc Name                : Dbo.Proc_GetHORule_RatingInfo 1355,7,1,1,'d'                                                                                                                                                            
Created by               : Ashwani                                                                                                                                                                          
Date                     : 01 Dec.,2005                                                                                                                          
Purpose                  : To get the rating detail for HO rules                                                                                                                          
Revison History          :                                                                                                                                                                          
Used In                  : Wolverine                                                                                                                                                                          
        
Reviewed By : Anurag Verma        
Reviewed On : 06-07-2007        
------------------------------------------------------------                                                                                                                                                                          
Date     Review By          Comments                                                                                                                                                                          
------   ------------       -------------------------*/                                
-- DROP PROC dbo.Proc_GetHORule_RatingInfo 1692,72,1,1,''                               
CREATE proc dbo.Proc_GetHORule_RatingInfo                                                  
(                                                                                                                                                   
                                                                                                                   
 @CUSTOMERID    int,                                                                                                                                                                          
 @APPID    int,                                                                                                                                                                          
 @APPVERSIONID   int,                                                                                                                          
 @DWELLINGID int,                                                                                                                                      
 @DESC varchar(10)                                                                                                                                                            
)                                                                                                                                                                          
AS      
BEGIN                                                                                                                             
 -- Mandatory                                                    
  DECLARE @REALHYDRANT_DIST REAL   
  DECLARE @HYDRANT_DIST CHAR          
  DECLARE @REALFIRE_STATION_DIST REAL                             
  DECLARE @FIRE_STATION_DIST CHAR                          
  DECLARE @IS_UNDER_CONSTRUCTION CHAR                                                                                               
  DECLARE @PROT_CLASS NVARCHAR(50)                                                
  DECLARE @INTNO_OF_AMPS INT                                            
  DECLARE @NO_OF_AMPS CHAR                                                                   
  DECLARE @CIRCUIT_BREAKERS NVARCHAR(5)                                
  DECLARE @INTCIRCUIT_BREAKERS INT                                  
  DECLARE @INTEXTERIOR_CONSTRUCTION INT                                                                                  
  DECLARE @EXTERIOR_CONSTRUCTION CHAR                                                                                 
  -- RULE                                                           
  DECLARE @INTNO_OF_FAMILIES  INT                                                                                                    
  DECLARE @NO_OF_FAMILIES CHAR                                                                                                                    
  DECLARE @INTROOF_TYPE INT                                                                                                             
  DECLARE @ROOF_TYPE CHAR                                                                                    
  DECLARE @IS_RECORD_EXIST CHAR          
  DECLARE @ALARM_CERT_ATTACHED nvarchar(60)           
  DECLARE @CENT_ST_BURG_FIRE CHAR    
--Added by Charles on 20-Oct-09 for Itrack 6586    
  DECLARE @DIR_FIRE_AND_POLICE CHAR(1)    
  DECLARE @DIR_FIRE CHAR(1)                    
  DECLARE @DIR_POLICE CHAR(1)                      
  DECLARE @CENT_ST_FIRE VARCHAR(1)                    
  DECLARE @CENT_ST_BURG VARCHAR(1)                            
--Added till here                                 
  --BUILDING CONTRACTOR                                                                              
  DECLARE @INTIS_SUPERVISED INT                                                                               
  DECLARE @IS_SUPERVISED CHAR(1)                       
  DECLARE @NEED_OF_UNITS VARCHAR(10) 
	DECLARE @SUBURBAN_CLASS VARCHAR(100) 
	SET @SUBURBAN_CLASS='N' 
  DECLARE @LOCATION_TEN_HOME CHAR(1)
SET @LOCATION_TEN_HOME='N'       
  SET @NO_OF_FAMILIES='N'                                                                                      
IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_RATING_INFO   WITH(NOLOCK)                                                                                             
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                                                                                          
BEGIN                                                                                                                           
  SET @IS_RECORD_EXIST='Y'                  
  SELECT       
 @REALHYDRANT_DIST=ISNULL(HYDRANT_DIST,-1),      
 @REALFIRE_STATION_DIST=ISNULL(FIRE_STATION_DIST,-1),      
 @IS_UNDER_CONSTRUCTION= ISNULL(IS_UNDER_CONSTRUCTION,''),      
 @PROT_CLASS=ISNULL(PROT_CLASS,''),      
 @INTNO_OF_AMPS=ISNULL(NO_OF_AMPS,-1),      
 @INTCIRCUIT_BREAKERS= ISNULL(CIRCUIT_BREAKERS,''),      
 @INTEXTERIOR_CONSTRUCTION=ISNULL(EXTERIOR_CONSTRUCTION,-1),   
 @INTNO_OF_FAMILIES    =ISNULL(NO_OF_FAMILIES,-1),      
 @INTROOF_TYPE=ISNULL(ROOF_TYPE,-1) ,                                                                              
 @INTIS_SUPERVISED     =ISNULL(IS_SUPERVISED,0) ,      
 @NEED_OF_UNITS=ISNULL(NEED_OF_UNITS,''),          
 @ALARM_CERT_ATTACHED = ISNULL(ALARM_CERT_ATTACHED,''),          
 @CENT_ST_BURG_FIRE   = ISNULL(CENT_ST_BURG_FIRE,''),    
--Added by Charles on 20-Oct-09 for Itrack 6586                      
  @DIR_FIRE_AND_POLICE = ISNULL(DIR_FIRE_AND_POLICE,''),                    
  @DIR_FIRE = ISNULL(DIR_FIRE,''),                    
  @DIR_POLICE = ISNULL(DIR_POLICE,''),                    
  @CENT_ST_FIRE = ISNULL(CENT_ST_FIRE,''),                    
  @CENT_ST_BURG = ISNULL(CENT_ST_BURG,'')                                 
--Added till here                                                                                                          
  FROM APP_HOME_RATING_INFO     WITH(NOLOCK)                                                                                                                      
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID       
END      
 DECLARE @POLICY_TYPE INT                                    
 SELECT @POLICY_TYPE=POLICY_TYPE FROM APP_LIST   WITH(NOLOCK)                                                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID           
 -- Mandatory check for Roof Type If Policy Type is HO-3 Replacement or HO-5 Replacement or HO-3 Repair            
 -- HO5 and HO6 Premier Types        
 IF(@POLICY_TYPE=11148 OR @POLICY_TYPE=11400             
 OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11149              
 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11194           
 OR @POLICY_TYPE=11404 OR @POLICY_TYPE=11410       
 OR @POLICY_TYPE = 11192 OR @POLICY_TYPE=11402)                              
 BEGIN       
 IF(@INTROOF_TYPE=9964)  --Flat Build                                                  
 BEGIN                                      
  SET @ROOF_TYPE='Y'        
 END                                                                                          
 ELSE IF(@INTROOF_TYPE<>0)                                                                                   
 BEGIN                                                                                                             
  SET @ROOF_TYPE='N'        
 END                                                                                                             
END                                                                                                     
ELSE                                                                                              
BEGIN                                                                                                             
 SET @ROOF_TYPE='N'        
END      
---================================      
IF(@INTROOF_TYPE=-1 or @INTROOF_TYPE = 0 OR @INTROOF_TYPE IS NULL )                                                                                                            
 BEGIN                                                                                                             
 SET @ROOF_TYPE=''        
 END                                                                                                       
---=================================                                                                                    
 IF(@REALHYDRANT_DIST=-1 OR @REALHYDRANT_DIST IS NULL)                                                                                                              
  BEGIN                                                                                                           
  SET @HYDRANT_DIST=''                                                                          
  END                                                                                                               
 ELSE IF(@REALHYDRANT_DIST=11555.0)                     
  BEGIN                 
  SET @HYDRANT_DIST='N'               
  END                                              
 ELSE IF(@REALHYDRANT_DIST=11556.0)                                                                                                              
  BEGIN                                                                                                               
  SET @HYDRANT_DIST='Y'                           
  END                    
--                                                                 
 IF(@REALFIRE_STATION_DIST =-1 OR @REALFIRE_STATION_DIST IS NULL)                        
  BEGIN                                                                                           
  SET @FIRE_STATION_DIST=''                                                                                                      
  END                                     
 ELSE                 
  BEGIN                     
  SET @FIRE_STATION_DIST='N'                                                                                                               
  END                                                                                                    
--                                                                                                       
 IF(@INTNO_OF_AMPS =-1)                                    
  BEGIN                                                                                                               
  SET @NO_OF_AMPS=''                                                                                      
  END                                                                                    
 ELSE IF(@INTNO_OF_AMPS<100)                                                                                                              
  BEGIN                                                                                       
  SET @NO_OF_AMPS='Y'                                                           
  END                                                                                                               
 ELSE IF(@INTNO_OF_AMPS<>0)                                                                    
  BEGIN                                                                                                               
  SET @NO_OF_AMPS='N'                                                                       
  END                                                                                                               
--                                                                       
 IF(@INTEXTERIOR_CONSTRUCTION=-1 OR @INTEXTERIOR_CONSTRUCTION IS NULL  OR @INTEXTERIOR_CONSTRUCTION=0)                                                                                                                     
  BEGIN                                                                                                                         
  SET @EXTERIOR_CONSTRUCTION=''                                                                                                         
  END                                                                                                                         
 ELSE                                                                           
  BEGIN                                                                                                                          
  SET  @EXTERIOR_CONSTRUCTION='N'                                                                                                                        
  END                                     
 -- Primary and Secondary Heat Type                                                                        
 DECLARE @PRIMARY_HEAT_TYPE CHAR    
 IF EXISTS (SELECT  PRIMARY_HEAT_TYPE FROM APP_HOME_RATING_INFO  WITH(NOLOCK)            
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                  
      AND DWELLING_ID=@DWELLINGID AND (PRIMARY_HEAT_TYPE=6223 OR PRIMARY_HEAT_TYPE=6224                                                                          
--INSERTED NEW PRIMARY TYPES                                                                          
      OR PRIMARY_HEAT_TYPE=6212 OR PRIMARY_HEAT_TYPE=6213                              
      OR PRIMARY_HEAT_TYPE=11806 OR PRIMARY_HEAT_TYPE=11807 OR PRIMARY_HEAT_TYPE=11808))                                                                         
 BEGIN                                                                                                                       
 SET  @PRIMARY_HEAT_TYPE='Y'                
 END                                                
 ELSE                            
 BEGIN                                                                          
 SET @PRIMARY_HEAT_TYPE='N'                                                                                                                      
 END                                               
                                                                                                          
 DECLARE @SECONDARY_HEAT_TYPE  CHAR                                                                                                
 IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  APP_HOME_RATING_INFO  WITH(NOLOCK)                                                                                                                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                               
      AND DWELLING_ID=@DWELLINGID AND (SECONDARY_HEAT_TYPE=6223 OR SECONDARY_HEAT_TYPE=6224                         
 OR SECONDARY_HEAT_TYPE=6212 OR SECONDARY_HEAT_TYPE=6213                         
        OR SECONDARY_HEAT_TYPE=11806 OR SECONDARY_HEAT_TYPE=11807 OR SECONDARY_HEAT_TYPE=11808                          
))                                                                                   
 BEGIN                                                                                                                       
 SET  @SECONDARY_HEAT_TYPE='Y'                                                                                         
 END                                                             
 ELSE                                                                                                                      
 BEGIN                                            
 SET @SECONDARY_HEAT_TYPE='N'                                                                                         
 END                                                                                                                       
                                                                                                               
-- No of Families                                                           
                                                                               
IF(@POLICY_TYPE=11402 OR @POLICY_TYPE=11400 OR @POLICY_TYPE=11401            
 OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11192 OR @POLICY_TYPE=11148             
 OR @POLICY_TYPE=11149 OR @POLICY_TYPE=11194 OR @POLICY_TYPE=11404            
 OR @POLICY_TYPE=11193 OR @POLICY_TYPE=11403 OR @POLICY_TYPE =11410 )                                                                                                           
            
 BEGIN                    
  IF(@INTNO_OF_FAMILIES>2 )                              
   BEGIN                          
    SET @NO_OF_FAMILIES='Y'         
   END                          
  ELSE IF(@INTNO_OF_FAMILIES=-1 OR @INTNO_OF_FAMILIES IS NULL OR @INTNO_OF_FAMILIES=0  )                     
   BEGIN                                                         
     SET @NO_OF_FAMILIES=''                    
   END                             
  ELSE IF(@INTNO_OF_FAMILIES<>0)                                                                                         
   BEGIN                                                                                                     
     SET @NO_OF_FAMILIES='N'                                   
   END                                                                                                        
 END                                         
--NO of families                   
/*IF(@POLICY_TYPE !=11195 OR @POLICY_TYPE!=11196)                   
BEGIN                   
if(@INTNO_OF_FAMILIES=-1 )                           
 begin                            
   set @NO_OF_FAMILIES=''                     
 end                   
END*/                                                                                                                    
           
-- HO-5                                                                                             
 declare @INTYEAR_BUILT int                                                                                                                  
 declare @YEAR_BUILT char                                           
                                                                                                            
 SELECT @INTYEAR_BUILT=YEAR_BUILT                                                                                                                  
 from   APP_DWELLINGS_INFO    WITH(NOLOCK)                                                                                                
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID                                                                                                                          
                                                                                              
declare @intWIRING_UPDATE_YEAR int   --2.A.2.c                                        
declare @WIRING_UPDATE_YEAR char  --2.A.2.c                                                                               
declare @DIFFWIRING_UPDATE_YEAR   int                                                                                                                   
                                                                                                                       
declare @intPLUMBING_UPDATE_YEAR int   --2.A.2.c                                                                   
declare @PLUMBING_UPDATE_YEAR char   --2.A.2.c                                                                                                                              
declare @DIFFPLUMBING_UPDATE_YEAR   int                                                                                                                  
                                        
declare @intHEATING_UPDATE_YEAR int   --2.A.2.c                                                                                                                     
declare @HEATING_UPDATE_YEAR char  --2.A.2.c                                                                      
declare @DIFFHEATING_UPDATE_YEAR   int                                                                                    
     
declare @intROOFING_UPDATE_YEAR int  --2.A.2.c                                                
declare @ROOFING_UPDATE_YEAR char  --2.A.2.c                                            
declare @DIFFROOFING_UPDATE_YEAR   int                                                                                                         
declare @HO5_YEAR_UPDATE char            
declare @EFF_DATE INT             
DECLARE @WIRING_RENOVATION INT          
declare @PLUMBING_RENOVATION INT          
declare @HEATING_RENOVATION INT           
DECLARE @ROOFING_RENOVATION INT          
--DEFAULT            
 SET @HO5_YEAR_UPDATE='N'           
SELECT @EFF_DATE = YEAR(APP_EFFECTIVE_DATE)                                                                                           
 FROM APP_LIST WITH(NOLOCK)                                    
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID            
 SELECT                                                                                                                          
 @INTWIRING_UPDATE_YEAR=ISNULL(WIRING_UPDATE_YEAR,0),@INTPLUMBING_UPDATE_YEAR=ISNULL(PLUMBING_UPDATE_YEAR,0),                     
 @INTHEATING_UPDATE_YEAR=ISNULL(HEATING_UPDATE_YEAR,0), @INTROOFING_UPDATE_YEAR =ISNULL(ROOFING_UPDATE_YEAR,0),          
 @WIRING_RENOVATION = WIRING_RENOVATION ,@PLUMBING_RENOVATION = PLUMBING_RENOVATION ,@HEATING_RENOVATION = HEATING_RENOVATION           
 ,@ROOFING_RENOVATION = ROOFING_RENOVATION                                                                                             
 FROM APP_HOME_RATING_INFO    WITH(NOLOCK)                                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID            
 SET @DIFFWIRING_UPDATE_YEAR   =(@EFF_DATE  - @INTWIRING_UPDATE_YEAR)                                                                                                                                
 SET @DIFFPLUMBING_UPDATE_YEAR =(@EFF_DATE  - @INTPLUMBING_UPDATE_YEAR)                                                                                                                                 
 SET @DIFFHEATING_UPDATE_YEAR  =(@EFF_DATE  - @INTHEATING_UPDATE_YEAR)                                                                                                         
 SET @DIFFROOFING_UPDATE_YEAR  =(@EFF_DATE  - @INTROOFING_UPDATE_YEAR)          
--              
SET @WIRING_UPDATE_YEAR ='N'      
SET @PLUMBING_UPDATE_YEAR='N'      
SET @HEATING_UPDATE_YEAR ='N'      
SET @ROOFING_UPDATE_YEAR ='N'       
          
IF(@WIRING_RENOVATION!=8924 or @PLUMBING_RENOVATION!=8924 or @HEATING_RENOVATION!=8924 or @ROOFING_RENOVATION!=8924)          
BEGIN         
                                                                                                        
 IF(@DIFFWIRING_UPDATE_YEAR>10)                                                                                                                  
 BEGIN                                                                                                                   
 SET @WIRING_UPDATE_YEAR='Y'                                                                                                                  
 END                                                                                                  
 -- ELSE IF(@INTWIRING_UPDATE_YEAR<>0)                                                                                                         
 --  BEGIN                                                                                                                   
 --  SET @WIRING_UPDATE_YEAR='N'                                                                              
 --  END                                                                                                                 
 ELSE IF(@INTWIRING_UPDATE_YEAR=0)                                                                                                                  
 BEGIN                                                         
 SET @WIRING_UPDATE_YEAR=''                                                        
 END                                                                                       
 --------------------                                                                      
 IF(@DIFFPLUMBING_UPDATE_YEAR >10)                                                                                                                  
 BEGIN                                  
 SET @PLUMBING_UPDATE_YEAR='Y'                                                                                                                  
 END                                                                
 -- ELSE IF(@INTPLUMBING_UPDATE_YEAR<>0)                                                                           
 --  BEGIN                                                                       
 --  SET @PLUMBING_UPDATE_YEAR='N'           
 --  END                                                                       
 ELSE IF(@INTPLUMBING_UPDATE_YEAR=0)                                                                         
 BEGIN                      
 SET @PLUMBING_UPDATE_YEAR=''                                                                                                                  
 END                               
 ----------------------                            
 IF(@DIFFHEATING_UPDATE_YEAR>10)                                                                          
 BEGIN                                   
 SET @HEATING_UPDATE_YEAR='Y'                                                                                                                  
 END                                                               
 -- ELSE IF(@INTHEATING_UPDATE_YEAR<>0)                                                         
 --  BEGIN                                                              
 --  SET @HEATING_UPDATE_YEAR='N'                        
 --  END                                                                                                                   
 ELSE IF(@INTHEATING_UPDATE_YEAR=0)                                                           
 BEGIN                                                                                                                   
 SET @HEATING_UPDATE_YEAR=''                                                                                                                  
 END                                                                                                  
 -----------------------                                                                                                                
 IF(@DIFFROOFING_UPDATE_YEAR>10)                                                                                                              
 BEGIN                                                                                                                   
 SET @ROOFING_UPDATE_YEAR='Y'                                                                     
 END                                                                                                                   
 -- ELSE IF(@INTROOFING_UPDATE_YEAR<>0)                                                                                                                  
 --  BEGIN                                                                                                                   
 --  SET @ROOFING_UPDATE_YEAR='N'                                                   
 --  END                                                                                           
 ELSE IF(@INTROOFING_UPDATE_YEAR=0)                                                                                            
 BEGIN            
 SET @ROOFING_UPDATE_YEAR=''                                                        
 END           
END        
                                           
  -- Only for HO-5                                                                                             
 DECLARE @INTPOLICY_TYPE int                   
 SELECT @INTPOLICY_TYPE=ISNULL(POLICY_TYPE,0)                                   
 FROM APP_LIST     WITH(NOLOCK)                                                                                                             
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                     
 --HO-5 replacement,HO-5 Premiem and HO-3 Repair          
 /*          
11404 HO-3 Repair          
11410 HO-5 Premier          
11401 HO-5 Replacement          
11194 HO-3 Repair          
11149 HO-5 Replacement              
*/                
  IF(@INTPOLICY_TYPE=11149 OR @INTPOLICY_TYPE=11410  OR @INTPOLICY_TYPE=11401 or  @INTPOLICY_TYPE=11194   OR @INTPOLICY_TYPE=11404)       
  BEGIN                                                                                        
 IF(@INTYEAR_BUILT<1950)                                                                                                          
 BEGIN             
   IF(@WIRING_RENOVATION!=8924 or @PLUMBING_RENOVATION!=8924 or @HEATING_RENOVATION!=8924 or @ROOFING_RENOVATION!=8924)          
   BEGIN                                     
  IF(@WIRING_UPDATE_YEAR='Y' OR @PLUMBING_UPDATE_YEAR='Y' OR @HEATING_UPDATE_YEAR='Y' OR @ROOFING_UPDATE_YEAR='Y')                                                       
  BEGIN                                                    
  SET @HO5_YEAR_UPDATE='Y'                                                                                                                  
  END                                                                              
  ELSE                                                       
  BEGIN                                                                                                                   
  SET @HO5_YEAR_UPDATE='N'                                                                                       
  END            
   END                                                                                        
 END                                                                        
 ELSE                                                                                                    
 BEGIN                                                                                                                   
 SET @HO5_YEAR_UPDATE='N'                                                                         
 END                                                                                                                 
 END                                                                                                 
--  ELSE                                                  
--  BEGIN                                                                                                                 
--   SET @HO5_YEAR_UPDATE='N'                                                                      
--   SET @WIRING_UPDATE_YEAR='N'                                                                                                                  
--   SET @PLUMBING_UPDATE_YEAR='N'                                                                     
--   SET @HEATING_UPDATE_YEAR='N'                                                                                                           
--   SET @ROOFING_UPDATE_YEAR='N'                                                                         
--  END                                                                                     
                                                             
---                                        
 IF(@INTYEAR_BUILT=0)                               
   BEGIN                                        
   SET @YEAR_BUILT=''                                                                                                                  
   END       
 ELSE                                                                                                                  
   BEGIN                                                                                                                   
   SET @YEAR_BUILT='N'                                                          
   END                                                         
--                                                                
--"Dwelling under Construction field is only mandatory,                                                         
--if house built within the last 2 years :Modified :19 June 1006                        
/*Declare @HOME_YAER_DIFF int             
declare @APP_EFFECTIVE_DATE varchar(20)                            
                                                              
SELECT @APP_EFFECTIVE_DATE=isnull(convert(varchar(20),APP_EFFECTIVE_DATE),'')  FROM  APP_LIST                                                                                
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                            
                                                  Select @HOME_YAER_DIFF = (year(@APP_EFFECTIVE_DATE)-YEAR_BUI          
LT)  FROM APP_DWELLINGS_INFO                                                    
where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID                                                                  
                                                              
 if(@HOME_YAER_DIFF <= 2)                                                                
begin                                                         
 if(@IS_UNDER_CONSTRUCTION='')                                                              
  begin                                                              
   set @IS_UNDER_CONSTRUCTION=''                                                                
  end                                                              
end  */                                                                                                       
                                                              
-- If Yes to Is Construction supervised by Licensed Building Contractor and policy term under Appl/POl main page is 1 yr then it should not be referred                              
DECLARE @APP_TERMS  NVARCHAR(10)                              
SELECT @APP_TERMS=APP_TERMS FROM APP_LIST  WITH(NOLOCK)                              
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                            
Declare @UNDER_CONSTRUCTION char                                
--                                                                                  
IF (@APP_TERMS='6')                              
BEGIN                               
  IF(@IS_UNDER_CONSTRUCTION='1')                                                                                                          
    BEGIN                                                                     
    SET @UNDER_CONSTRUCTION='Y'       -----REF              
    END                                                                                                      
  ELSE                           
    BEGIN                                                              
    SET @UNDER_CONSTRUCTION='N'                                 
    END                                 
END                 
ELSE                              
BEGIN                              
SET @UNDER_CONSTRUCTION='N'                              
END                              
         
                           
----------Building contractor       
IF(@INTIS_SUPERVISED=0)                                                                              
BEGIN                                                                              
  IF(@IS_UNDER_CONSTRUCTION='1')                                                        
  BEGIN                                                        
  SET @IS_SUPERVISED='Y'     --REJECT IF NO                                                                 
  END                             
    ELSE                                                        
  BEGIN                 
  SET @IS_SUPERVISED='N'                                                                  
  END                                                        
                                          
END                             
ELSE                                                                        
  BEGIN                              
  SET @IS_SUPERVISED='N'                                                                              
  END                                                                            
                                                 
----------end building contractor                         
-- Mandatory check for 'If HO-4/HO-6, #units/apts ' when Policy Type HO-4/HO-6 .                                                        
SELECT @POLICY_TYPE=POLICY_TYPE                                                                      
FROM   APP_LIST   WITH(NOLOCK)                       
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                      
IF (@POLICY_TYPE=11195 OR @POLICY_TYPE=11196 or @POLICY_TYPE=11405 OR @POLICY_TYPE=11406)                       
BEGIN                      
 IF(@NEED_OF_UNITS='')                      
  BEGIN                      
  SET @NEED_OF_UNITS=''                      
  END                       
 ELSE                      
  BEGIN                      
  SET @NEED_OF_UNITS='N'                      
  END                      
END                       
ELSE                      
 BEGIN                      
 SET @NEED_OF_UNITS='N'                      
 END                        
                                         
-----------------------------------                                                    
----------------------------START-----                                                       
--"Dwelling under Construction field is only mandatory,                                                                     
--if house built within the last 2 years :Modified :19 June 2006                                                           
DECLARE @HOME_YAER_DIFF INT                         
DECLARE @DWELL_UNDER_CONSTRUCTION CHAR                   
SELECT @HOME_YAER_DIFF = (@EFF_DATE-YEAR_BUILT)                                     
FROM APP_DWELLINGS_INFO WITH(NOLOCK)                                                                    
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                                            
                         
IF NOT EXISTS (SELECT IS_UNDER_CONSTRUCTION FROM APP_HOME_RATING_INFO  WITH(NOLOCK)                                                                                                             
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                                                                                              
BEGIN                                  
  IF(@HOME_YAER_DIFF <= 2)                                                                    
  BEGIN                                                       
  SET @DWELL_UNDER_CONSTRUCTION=''                                    
  END                                          
   ELSE                                                          
  BEGIN                                                          
  SET @DWELL_UNDER_CONSTRUCTION='N'                                                             
  END                                                           
END                                                           
ELSE                                   
BEGIN                                                          
 ---IF EXISTS THEN CHECK FOR UNDER CONSTRUCTION FIELD IF IT IS NOT FIELD THEN PROMT THE MANDATORY MESSAGE                                                          
 SELECT @DWELL_UNDER_CONSTRUCTION = ISNULL(IS_UNDER_CONSTRUCTION,'') FROM APP_HOME_RATING_INFO   WITH(NOLOCK)                                 
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                         
 IF(@HOME_YAER_DIFF <= 2)                           
 BEGIN                                                                    
  IF(@DWELL_UNDER_CONSTRUCTION='')                           
  BEGIN                                       
  SET @DWELL_UNDER_CONSTRUCTION=''                                                          
  END                                                             
         ELSE                                                               
  BEGIN                                                                  
  SET @DWELL_UNDER_CONSTRUCTION='N'                                       
  END                                                             
 END                                                          
  ELSE                                       
  BEGIN                        
  SET @DWELL_UNDER_CONSTRUCTION='N'                                                                    
  END                   
                                                                 
END                          
--CIRCUIT BREAKERS                              
IF( @INTCIRCUIT_BREAKERS =0)                                    
  BEGIN                                              
   SET @CIRCUIT_BREAKERS = ''                                           
  END                                    
ELSE IF( @INTCIRCUIT_BREAKERS !=10963 )                                    
  BEGIN                                    
   SET @CIRCUIT_BREAKERS = 'Y'                                     
  END                                    
ELSE                                     
  BEGIN                                    
   SET @CIRCUIT_BREAKERS='N'                                                    
  END          
/* Itrack - 1901          
Rating Info Tab - Protective Devices           
- If there is a check mark beside - Central Alarm Fire and Burglary           
- Then look at Alarm Certificate Field           
- If yes - all is in order and no action is required    
- if No then refer to underwriters          
*/          
DECLARE @CENT_BURG_FIRE_ALARM_CERT_ATTACHED CHAR          
IF (@CENT_ST_BURG_FIRE = 'Y' )           
BEGIN          
  IF(@ALARM_CERT_ATTACHED ='10964' or @ALARM_CERT_ATTACHED ='')          
   BEGIN           
    SET @CENT_BURG_FIRE_ALARM_CERT_ATTACHED='Y'          
   END          
  ELSE           
   BEGIN           
    SET @CENT_BURG_FIRE_ALARM_CERT_ATTACHED='N'          
   END          
END            
ELSE           
BEGIN           
 SET @CENT_BURG_FIRE_ALARM_CERT_ATTACHED='N'          
END       
    
-- Added by Charles on 20-Oct-09 for Itrack 6586         
DECLARE @PROT_DEVC_ALARM_CERT_ATTACHED CHAR    
SET @PROT_DEVC_ALARM_CERT_ATTACHED='N'     
IF       
(( @CENT_ST_BURG = 'Y' OR @CENT_ST_BURG_FIRE ='Y' OR               
 (@DIR_FIRE_AND_POLICE = 'Y' OR @DIR_FIRE= 'Y' OR @DIR_POLICE='Y' OR @CENT_ST_FIRE='Y'))    
  AND (@ALARM_CERT_ATTACHED='10964'))        
 BEGIN                      
  SET @PROT_DEVC_ALARM_CERT_ATTACHED='Y'                                          
 END                      
ELSE                      
 BEGIN                      
  SET @PROT_DEVC_ALARM_CERT_ATTACHED='N'                                             
 END       
--Added till here  mand
IF EXISTS(SELECT * FROM APP_HOME_RATING_INFO   WITH(NOLOCK)                                 
		WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID 
		AND  SUBURBAN_CLASS='Y' AND ISNULL(LOCATED_IN_SUBDIVISION,'')='')
	BEGIN
		SET @LOCATION_TEN_HOME=''
	END 
DECLARE @SUBURBANREFERVALUE NVARCHAR(200)
SET @SUBURBANREFERVALUE=''
IF  EXISTS(SELECT * FROM APP_HOME_RATING_INFO   WITH(NOLOCK) inner join APP_LIST WITH(NOLOCK)
		ON  APP_HOME_RATING_INFO.CUSTOMER_ID = APP_LIST.CUSTOMER_ID AND APP_HOME_RATING_INFO.APP_ID = APP_LIST.APP_ID 
			AND APP_HOME_RATING_INFO.APP_VERSION_ID = APP_LIST.APP_VERSION_ID
		WHERE APP_LIST.CUSTOMER_ID=@CUSTOMERID AND APP_LIST.APP_ID= @APPID AND APP_LIST.APP_VERSION_ID = @APPVERSIONID AND APP_HOME_RATING_INFO.DWELLING_ID=@DWELLINGID 
		AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963' and POLICY_TYPE NOT IN (11195,11196,11405,11406,11403,11404,11193,11194))
	BEGIN
		SET @LOCATION_TEN_HOME = 'Y'
		SET @SUBURBAN_CLASS='Y'
		-- REFERAL FOR FIRE PROTECTION CLASS
		IF EXISTS(SELECT 1 FROM                                                              
				APP_HOME_RATING_INFO WITH (NOLOCK) INNER  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                                                                                    
				ON APP_HOME_RATING_INFO.PROT_CLASS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                      
			WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID 
				AND (ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0')<>'8B' AND ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0')<>'09'))
				BEGIN
					SET @SUBURBANREFERVALUE='PROT_CLASS'
				END 
		-- REFREAL FOR MARKET VALUE AND REPLACMENT VALUE LESS THAN 200000
		IF EXISTS (SELECT 1 FROM APP_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID 
					AND DWELLING_ID=@DWELLINGID AND REPLACEMENT_COST<200000 AND MARKET_VALUE <200000)

				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'RPMR200'
				END

		-- REFERAL FOR MARKET VALUE AND REPLACEMENT VALUE NOT EQUAL

				IF EXISTS (SELECT 1 FROM APP_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID 
					AND DWELLING_ID=@DWELLINGID AND ISNULL(REPLACEMENT_COST,0)<>ISNULL(MARKET_VALUE,0))
				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'RPMRNOTEQ'
				END

		-- REFERAL FOR YEAR BUILT NOT WITHIN THE LAST 20 YEARS
				
				IF EXISTS (SELECT 1 FROM APP_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID 
					AND DWELLING_ID=@DWELLINGID AND (@EFF_DATE-ISNULL(APP_DWELLINGS_INFO.YEAR_BUILT,0)) >20)--convert(nvarchar(20),DATEDIFF(YEAR,convert(nvarchar(20),ISNULL(APP_DWELLINGS_INFO.YEAR_BUILT,0)),@EFF_DATE))>20)
				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'DWELL20'
				END

		-- REFERAL FOR # OF MILES FROM FIRE STATION MUST BE 5 OR LESS

			IF EXISTS (SELECT 1 FROM APP_HOME_RATING_INFO WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID 
						AND DWELLING_ID=@DWELLINGID AND FIRE_STATION_DIST>5)
				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'MILES5'
				END				

		-- REFERAL FOR DWELLING CONTAINS A SOLID FUEL HEATING DEVISE

			IF EXISTS(SELECT 1 FROM APP_HOME_OWNER_SOLID_FUEL WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID and ISNULL(IS_ACTIVE,'N')='Y')

				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'SLDHEATDEV'
				END

		-- REFERAL FOR RESIDENCE IS A MODULAR OR MANUFACTURE HOME

			IF EXISTS(SELECT 1 FROM APP_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID 
				AND APP_VERSION_ID = @APPVERSIONID  AND MODULAR_MANUFACTURED_HOME=1)
				BEGIN
					SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'MODMANHOME'
				END
			if(@SUBURBANREFERVALUE<>'')
				SET  @SUBURBAN_CLASS = @SUBURBAN_CLASS+'~'+ @SUBURBANREFERVALUE
	END
                                         
----------------------------END--------             
--------------------------------                                                                                  
SELECT                                                                                                                          
-- Mandatory                                                                                                                    
 @HYDRANT_DIST as HYDRANT_DIST,                                                                                                                       
 @FIRE_STATION_DIST as FIRE_STATION_DIST,                                                                                                                          
 @UNDER_CONSTRUCTION as UNDER_CONSTRUCTION,    
 @PROT_CLASS as PROT_CLASS,                           
 @NO_OF_AMPS as NO_OF_AMPS,                                                                                                     
 @CIRCUIT_BREAKERS as CIRCUIT_BREAKERS,                                                                                                                          
 @EXTERIOR_CONSTRUCTION as EXTERIOR_CONSTRUCTION ,                                                                                                                    
 @PRIMARY_HEAT_TYPE as PRIMARY_HEAT_TYPE ,                                                                                                                                  
 @SECONDARY_HEAT_TYPE as SECONDARY_HEAT_TYPE,                                                                                                                    
 --Rules             
 @NO_OF_FAMILIES   as NO_OF_FAMILIES,        
 -- HO-5                                                  
 @WIRING_UPDATE_YEAR as WIRING_UPDATE_YEAR ,                                 
 @PLUMBING_UPDATE_YEAR as PLUMBING_UPDATE_YEAR,                                                                                                   
 @HEATING_UPDATE_YEAR as HEATING_UPDATE_YEAR,                                                                   
 @ROOFING_UPDATE_YEAR as ROOFING_UPDATE_YEAR,                                                       
 @HO5_YEAR_UPDATE as HO5_YEAR_UPDATE, -- HO-5                                                                                           
 --HO-3 or HO -5                                                                                                         
 @ROOF_TYPE as ROOF_TYPE,                                                                     
 @IS_RECORD_EXIST as IS_RECORD_EXIST  ,                                                               
 @IS_SUPERVISED as IS_SUPERVISED,                         
 @DWELL_UNDER_CONSTRUCTION as DWELL_UNDER_CONSTRUCTION ,                      
 @NEED_OF_UNITS AS NEED_OF_UNITS ,          
 @CENT_BURG_FIRE_ALARM_CERT_ATTACHED AS CENT_BURG_FIRE_ALARM_CERT_ATTACHED,    
 @PROT_DEVC_ALARM_CERT_ATTACHED AS PROT_DEVC_ALARM_CERT_ATTACHED, --Added by Charles on 20-Oct-09 for Itrack 6586    
 @SUBURBAN_CLASS AS SUBURBAN_CLASS,
 @LOCATION_TEN_HOME AS LOCATION_TEN_HOME
END 




GO

