 /* ----------------------------------------------------------                                                                                                                                          
Proc Name                : Dbo.Proc_GetHORule_RatingInfo_Pol                                                                                                                                        
Created by               : Ashwani                                                                                                                                          
Date                     : 02 Mar 2006                                                                                    
Purpose                  : To get the rating detail for HO policy rules                                                                                          
Revison History          :                                                                                                                                          
Used In                  : Wolverine                                                                                                                                          
          
Reviewed By  : Anurag Verma          
Reviewed On  : 06-07-2007          
------------------------------------------------------------                                                                                                                                          
Date     Review By          Comments                                                                                                                                          
------   ------------       -------------------------*/                                                                                                                                          
-- DROP PROC dbo.Proc_GetHORule_RatingInfo_Pol                                            
ALTER proc dbo.Proc_GetHORule_RatingInfo_Pol                                                                                         
(                                                                                                                   
                                                                                   
@CUSTOMER_ID    int,                                                                                                                                          
@POLICY_ID    int,                                                                                                                                          
@POLICY_VERSION_ID   int,                                                                                          
@DWELLING_ID int                                                                                                      
)                                                                                                                                          
as                                                                                                                                              
begin                                                                                             
 -- Mandatory                                                                                            
 declare @REALHYDRANT_DIST real                                                                                         
 declare @HYDRANT_DIST char                                                                                        
 declare @REALFIRE_STATION_DIST real                                                                                          
 declare @FIRE_STATION_DIST char                                                                                        
 declare @IS_UNDER_CONSTRUCTION char                                                                                          
 declare @PROT_CLASS nvarchar(50)                       
 declare @INTNO_OF_AMPS int               declare @NO_OF_AMPS char                            
 declare @CIRCUIT_BREAKERS nvarchar(5)                                                                                          
 declare @INTEXTERIOR_CONSTRUCTION int                                                  
 declare @EXTERIOR_CONSTRUCTION char                                
 -- Rule                                                     
 declare @INTNO_OF_FAMILIES  int                                         
 declare @NO_OF_FAMILIES char                                            
 declare @INTROOF_TYPE int                                                                         
 declare @ROOF_TYPE char                                                   
 declare @IS_RECORD_EXIST char                                                               
  --BUILDING CONTRACTOR                                                                              
 DECLARE @INTIS_SUPERVISED INT                                                                               
 DECLARE @IS_SUPERVISED CHAR(1)                      
 DECLARE @NEED_OF_UNITS VARCHAR(10)                                                                                         
 DECLARE @ALARM_CERT_ATTACHED nvarchar(60)             
 DECLARE @CENT_ST_BURG_FIRE CHAR       
--Added by Charles on 20-Oct-09 for Itrack 6586      
  DECLARE @DIR_FIRE_AND_POLICE CHAR(1)      
  DECLARE @DIR_FIRE CHAR(1)                      
  DECLARE @DIR_POLICE CHAR(1)                        
  DECLARE @CENT_ST_FIRE VARCHAR(1)                      
  DECLARE @CENT_ST_BURG VARCHAR(1)     
  DECLARE @SUBURBAN_CLASS VARCHAR(100)   
  SET @SUBURBAN_CLASS='N'    
  DECLARE @LOCATION_TEN_HOME CHAR(1)  
SET @LOCATION_TEN_HOME='N'   
 DECLARE @SUBURBAN_RENEWAL_REFERAL CHAR(1)  
 SET @SUBURBAN_RENEWAL_REFERAL='N'  
--Added till here                                                                 
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_HOME_RATING_INFO    WITH(NOLOCK)                                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID)                                                                                          
 BEGIN                                                                                        
 SET @IS_RECORD_EXIST='Y'                                                    
 SELECT @REALHYDRANT_DIST=ISNULL(HYDRANT_DIST,-1),@REALFIRE_STATION_DIST=ISNULL(FIRE_STATION_DIST,-1),@IS_UNDER_CONSTRUCTION=                                                                                          
 ISNULL(IS_UNDER_CONSTRUCTION,''),@PROT_CLASS=ISNULL(PROT_CLASS,''),@INTNO_OF_AMPS=ISNULL(NO_OF_AMPS,-1),@CIRCUIT_BREAKERS=                                                        
 ISNULL(CIRCUIT_BREAKERS,''),@INTEXTERIOR_CONSTRUCTION=ISNULL(EXTERIOR_CONSTRUCTION,-1),                                                          
 @INTNO_OF_FAMILIES=ISNULL(NO_OF_FAMILIES,-1), @INTROOF_TYPE=ISNULL(ROOF_TYPE,''),                            
 @INTIS_SUPERVISED=ISNULL(IS_SUPERVISED,0),@NEED_OF_UNITS=ISNULL(NEED_OF_UNITS,''),            
        @ALARM_CERT_ATTACHED  = ISNULL(ALARM_CERT_ATTACHED,''),            
        @CENT_ST_BURG_FIRE   = ISNULL(CENT_ST_BURG_FIRE,''),      
  --Added by Charles on 20-Oct-09 for Itrack 6586                        
    @DIR_FIRE_AND_POLICE = ISNULL(DIR_FIRE_AND_POLICE,''),                      
    @DIR_FIRE = ISNULL(DIR_FIRE,''),                      
    @DIR_POLICE = ISNULL(DIR_POLICE,''),                      
    @CENT_ST_FIRE = ISNULL(CENT_ST_FIRE,''),                      
    @CENT_ST_BURG = ISNULL(CENT_ST_BURG,'')                                   
  --Added till here                                                                                  
 FROM POL_HOME_RATING_INFO WITH(NOLOCK)                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID       
                                                 
 END                                                                                      
 ELSE                                                                                   
 BEGIN                                                                                           
 SET @HYDRANT_DIST =''                                                                                        
 SET @FIRE_STATION_DIST =''                                                                                          
 SET @IS_UNDER_CONSTRUCTION=''                                                                                          
 SET @PROT_CLASS=''                                                                                          
 SET @NO_OF_AMPS =''                                                                                
 SET @CIRCUIT_BREAKERS =''          
 SET @EXTERIOR_CONSTRUCTION =''           
 SET @INTNO_OF_FAMILIES=-1         
 SET @NO_OF_FAMILIES=''                                         
 SET @ROOF_TYPE=''                           
 SET @REALHYDRANT_DIST=-1                                                            
 SET @REALFIRE_STATION_DIST=-1                                                            
 SET @INTEXTERIOR_CONSTRUCTION=-1                                                                           
 SET @INTROOF_TYPE=-1                                   
 SET @IS_RECORD_EXIST='N'                                     
 SET @INTNO_OF_AMPS=-1                                                       
 END                                                     
                                                                            
 DECLARE @POLICY_TYPE INT                                                      
 SELECT @POLICY_TYPE=POLICY_TYPE FROM POL_CUSTOMER_POLICY_LIST   WITH(NOLOCK)                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
-- HO-3 or HO-5                                                                             
 IF(@POLICY_TYPE=11148 OR @POLICY_TYPE=11400            
 OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11149               
 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11194             
 OR @POLICY_TYPE=11404 OR @POLICY_TYPE=11410        
 OR @POLICY_TYPE=11192 OR  @POLICY_TYPE=11402 )                 
BEGIN                                  
   IF(@INTROOF_TYPE=9964)                                                                            
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
--=======================        
IF(@INTROOF_TYPE='' OR @INTROOF_TYPE =-1 OR @INTROOF_TYPE = 0 or @INTROOF_TYPE IS NULL)                                 
 BEGIN                                                                             
  SET @ROOF_TYPE=''                                 
 END         
-----------------------------------------                                               
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
--------------------------                                                                                        
 IF(@REALFIRE_STATION_DIST =-1 OR @REALFIRE_STATION_DIST IS NULL)                                                                
 BEGIN                                                           
  SET @FIRE_STATION_DIST=''             
 END                                                                                         
 ELSE                                                          
  BEGIN                                                                                         
   SET @FIRE_STATION_DIST='N'                                       
  END                                                                   
--------------------------                                                                                        
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
  IF EXISTS (SELECT  PRIMARY_HEAT_TYPE FROM  POL_HOME_RATING_INFO     WITH(NOLOCK)                          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                       
       AND DWELLING_ID=@DWELLING_ID AND (PRIMARY_HEAT_TYPE=6223 OR PRIMARY_HEAT_TYPE=6224                          
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
 --- Secondary Heat Type                                                                          
 DECLARE @SECONDARY_HEAT_TYPE  CHAR                                                                                       
 IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  POL_HOME_RATING_INFO WITH(NOLOCK)                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                       
      AND DWELLING_ID=@DWELLING_ID AND (SECONDARY_HEAT_TYPE=6223 OR SECONDARY_HEAT_TYPE=6224 OR                    
                                        SECONDARY_HEAT_TYPE=6213 OR SECONDARY_HEAT_TYPE=6212 OR                       
                                        SECONDARY_HEAT_TYPE=11806 OR SECONDARY_HEAT_TYPE=11807 OR                         
     SECONDARY_HEAT_TYPE=11808))                                                                                 
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
 OR @POLICY_TYPE=11193 OR @POLICY_TYPE=11403 OR @POLICY_TYPE=11410)                                                                                       
 BEGIN                         
   IF(@INTNO_OF_FAMILIES>2 )                                                                                    
     BEGIN                                                                                     
      SET @NO_OF_FAMILIES='Y'                                                                           
     END                
   ELSE IF(@INTNO_OF_FAMILIES=-1 OR @INTNO_OF_FAMILIES IS NULL OR @INTNO_OF_FAMILIES=0 )                         
     BEGIN                                                  
      SET @NO_OF_FAMILIES=''        
     END                                           
  ELSE IF(@INTNO_OF_FAMILIES<>0)                                                                               
     BEGIN                                                                     
      SET @NO_OF_FAMILIES='N'                                                                                    
     END                                                           
 END                                                                             
-- HO-5                                                                                   
 DECLARE @INTYEAR_BUILT INT                                                                                  
 DECLARE @YEAR_BUILT CHAR                                                                                  
                                                                            
 SELECT @INTYEAR_BUILT=YEAR_BUILT                                                                 
 FROM   POL_DWELLINGS_INFO   WITH(NOLOCK)                                                                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID                                                                  
                                                              
DECLARE @INTWIRING_UPDATE_YEAR INT   --2.A.2.C                                                                                              
DECLARE @WIRING_UPDATE_YEAR CHAR  --2.A.2.C                                                                                     
DECLARE @DIFFWIRING_UPDATE_YEAR   INT                                                                                   
                                      
DECLARE @INTPLUMBING_UPDATE_YEAR INT   --2.A.2.C              
DECLARE @PLUMBING_UPDATE_YEAR CHAR   --2.A.2.C                                                                                              
DECLARE @DIFFPLUMBING_UPDATE_YEAR   INT                                                                                  
                                                                                  
DECLARE @INTHEATING_UPDATE_YEAR INT   --2.A.2.C                 
DECLARE @HEATING_UPDATE_YEAR CHAR  --2.A.2.C                                                                    
DECLARE @DIFFHEATING_UPDATE_YEAR   INT                                                                                        
                                                  
DECLARE @INTROOFING_UPDATE_YEAR INT  --2.A.2.C                                                                                               
DECLARE @ROOFING_UPDATE_YEAR CHAR  --2.A.2.C                                                                                
DECLARE @DIFFROOFING_UPDATE_YEAR   INT                                                                                        
                                                                                  
DECLARE @HO5_YEAR_UPDATE CHAR             
DECLARE @EFF_DATE INT               
DECLARE @WIRING_RENOVATION INT            
DECLARE @PLUMBING_RENOVATION INT            
DECLARE @HEATING_RENOVATION INT             
DECLARE @ROOFING_RENOVATION INT      
DECLARE @APP_EFF_DATE INT            
--dEFAULT              
 SET @HO5_YEAR_UPDATE='N'                   
 SELECT @EFF_DATE = YEAR(POLICY_EFFECTIVE_DATE),     
  @APP_EFF_DATE = YEAR(APP_EFFECTIVE_DATE)                                                                            
 FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and             
 POLICY_VERSION_ID = @POLICY_VERSION_ID                                                        
 SELECT                                                                                             
 @intWIRING_UPDATE_YEAR=isnull(WIRING_UPDATE_YEAR,-1),@intPLUMBING_UPDATE_YEAR=isnull(PLUMBING_UPDATE_YEAR,-1),                                            
 @intHEATING_UPDATE_YEAR=isnull(HEATING_UPDATE_YEAR,-1), @intROOFING_UPDATE_YEAR = isnull(ROOFING_UPDATE_YEAR,-1) ,     
 @WIRING_RENOVATION = WIRING_RENOVATION ,@PLUMBING_RENOVATION = PLUMBING_RENOVATION ,@HEATING_RENOVATION = HEATING_RENOVATION             
 ,@ROOFING_RENOVATION = ROOFING_RENOVATION                                                                                      
 FROM POL_HOME_RATING_INFO    WITH(NOLOCK)                                                                                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                                   
  
 SET @DIFFWIRING_UPDATE_YEAR   =(@EFF_DATE  - @INTWIRING_UPDATE_YEAR)                                                                                                                                  
 SET @DIFFPLUMBING_UPDATE_YEAR =(@EFF_DATE  - @INTPLUMBING_UPDATE_YEAR)                                   
 SET @DIFFHEATING_UPDATE_YEAR  =(@EFF_DATE  - @INTHEATING_UPDATE_YEAR)                   
 SET @DIFFROOFING_UPDATE_YEAR  =(@EFF_DATE  - @INTROOFING_UPDATE_YEAR)         
               
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
 --   SET @WIRING_UPDATE_YEAR='N'                       
 --  END                                        
 ELSE IF(@INTWIRING_UPDATE_YEAR=-1)                            
 BEGIN                                                                                   
 SET @WIRING_UPDATE_YEAR=''                                                                                  
 END                                                                                   
--                                                                                  
 IF(@DIFFPLUMBING_UPDATE_YEAR>10)                                                                                  
 BEGIN                                                                                   
 SET @PLUMBING_UPDATE_YEAR='Y'                                                                                  
 END                                                                           
 -- else if(@intPLUMBING_UPDATE_YEAR<>0)                                                               
 --  begin                                                                                   
 --   set @PLUMBING_UPDATE_YEAR='N'                                                                                  
 --  end                                                                                   
 ELSE IF(@INTPLUMBING_UPDATE_YEAR=-1)                                                                                  
 BEGIN                                              
 SET @PLUMBING_UPDATE_YEAR=''                                                          
 END                                                            
--      
 IF(@DIFFHEATING_UPDATE_YEAR>10)                                                        
 BEGIN                                             
 SET @HEATING_UPDATE_YEAR='Y'                                         
 END                                                   
 -- ELSE IF(@INTHEATING_UPDATE_YEAR<>0)                                     
 --  BEGIN                    
 --   SET @HEATING_UPDATE_YEAR='N'                                                                          
 --  END                                                                   
 ELSE IF(@INTHEATING_UPDATE_YEAR=-1)                                                                                  
 BEGIN                                                                                   
 SET @HEATING_UPDATE_YEAR=''                                                                                 
 END                                                                                   
--                                                                                  
 IF(@DIFFROOFING_UPDATE_YEAR>10)                                                                                  
 BEGIN                                                                                   
 SET @ROOFING_UPDATE_YEAR='Y'                                                                                  
 END                                                                                   
 -- ELSE IF(@INTROOFING_UPDATE_YEAR<>0)                                                                  
 --  BEGIN                                                                
 --   SET @ROOFING_UPDATE_YEAR='N'                                                                       
 --  END                              
 ELSE IF(@INTROOFING_UPDATE_YEAR=-1)                    
 BEGIN                                                                                   
 SET @ROOFING_UPDATE_YEAR=''                                                                                  
 END          
END                                                      
-- Only for HO-5                                               
 DECLARE @INTPOLICY_TYPE INT                                                     
 SELECT @INTPOLICY_TYPE=ISNULL(POLICY_TYPE,0)                                                                                
 FROM POL_CUSTOMER_POLICY_LIST    WITH(NOLOCK)                     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID              
--HO5 REPLACEMENT AND HO3 REPAIR                
/*            
11404 HO-3 Repair            
11410 HO-5 Premier            
11401 HO-5 Replacement            
11194 HO-3 Repair            
11149 HO-5 Replacement                
*/                                                                                          
  IF(@INTPOLICY_TYPE=11149   OR @INTPOLICY_TYPE=11401 OR @INTPOLICY_TYPE=11194  OR @INTPOLICY_TYPE=11410 OR @INTPOLICY_TYPE=11404)                                                                            
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
--  else                                                   
--  begin                                                        
--   set @HO5_YEAR_UPDATE='N'                                                 
--   set @WIRING_UPDATE_YEAR='N'                                                                                  
--   set @PLUMBING_UPDATE_YEAR='N'                                                            
--   set @HEATING_UPDATE_YEAR='N'                                                                           
--   set @ROOFING_UPDATE_YEAR='N'                                                                                  
--  end                      
---                                                                                      
 if(@INTYEAR_BUILT=0)                                                                                  
 begin                                                                             
 set @YEAR_BUILT=''                                                                                  
 end                                                            
 else                                                                                  
 begin                   
 set @YEAR_BUILT='N'                                
 end                                      
-- If Yes to Is Construction supervised by Licensed Building Contractor and policy term under Appl/POl main page is 1 yr then it should not be referred                              
DECLARE @APP_TERMS  NVARCHAR(10)                              
SELECT @APP_TERMS=APP_TERMS FROM POL_CUSTOMER_POLICY_LIST   WITH(NOLOCK)                            
                              
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                            
Declare @UNDER_CONSTRUCTION char                                
--                                                                                  
IF (@APP_TERMS='6')                              
BEGIN                         
  IF(@IS_UNDER_CONSTRUCTION='1')                                                                            
    BEGIN                                                        
    SET @UNDER_CONSTRUCTION='Y'     -----REF                                              
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
                                         
--                                       
----------------------------START-----                                             
--"Dwelling under Construction field is only mandatory,                                                             
--if house built within the last 2 years :Modified :19 June 1006                                           
                                              
DECLARE @HOME_YAER_DIFF INT                             
SET @HOME_YAER_DIFF=1                                                  
DECLARE @DWELL_UNDER_CONSTRUCTION CHAR                                                  
                               
SELECT @HOME_YAER_DIFF = (@EFF_DATE-YEAR_BUILT)  FROM POL_DWELLINGS_INFO WITH(NOLOCK)                          
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                                       
                                                  
IF NOT EXISTS (SELECT IS_UNDER_CONSTRUCTION FROM POL_HOME_RATING_INFO   WITH(NOLOCK)                                                                                                   
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID)                                                                                                                      
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
 SELECT @DWELL_UNDER_CONSTRUCTION = ISNULL(IS_UNDER_CONSTRUCTION,'') FROM POL_HOME_RATING_INFO WITH(NOLOCK)                                                                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                              
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
-----------------------------                                      
IF @CIRCUIT_BREAKERS =10964                                 
 BEGIN                                        
  SET @CIRCUIT_BREAKERS = 'Y'                                                          
 END                            
ELSE                                
 BEGIN                                
  SET @CIRCUIT_BREAKERS = 'N'                                 
 END                                
----------------------------END--------                      
---------Building contractor    
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
FROM   POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                     
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=POLICY_VERSION_ID                         
                  
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
/* ITRACK - 1901            
RATING INFO TAB - PROTECTIVE DEVICES             
- IF THERE IS A CHECK MARK BESIDE - CENTRAL ALARM FIRE AND BURGLARY             
- THEN LOOK AT ALARM CERTIFICATE FIELD             
- IF YES - ALL IS IN ORDER AND NO ACTION IS REQUIRED             
- IF NO THEN REFER TO UNDERWRITERS          
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
--Added till here   
  
IF EXISTS(SELECT 1 FROM POL_HOME_RATING_INFO   WITH(NOLOCK)                                   
  WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID  
  AND  SUBURBAN_CLASS='Y' AND  ISNULL(LOCATED_IN_SUBDIVISION,'')='')  
 BEGIN  
  SET @LOCATION_TEN_HOME=''  
 END  
DECLARE @SUBURBANREFERVALUE NVARCHAR(200)  
SET @SUBURBANREFERVALUE=''  
IF  EXISTS(SELECT 1 FROM POL_HOME_RATING_INFO PHRI   WITH(NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)  
  ON PHRI.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PHRI.POLICY_ID=PCPL.POLICY_ID AND PHRI.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID  
  WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID and PCPL.POLICY_ID= @POLICY_ID and PCPL.POLICY_VERSION_ID = @POLICY_VERSION_ID and PHRI.DWELLING_ID=@DWELLING_ID  
 AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963' AND PCPL.POLICY_TYPE NOT IN (11195,11196,11405,11406,11403,11404,11193,11194))  
 BEGIN  
  SET @LOCATION_TEN_HOME = 'Y'  
  
  SET @SUBURBAN_CLASS='Y'  
  -- REFERAL FOR FIRE PROTECTION CLASS  
  IF EXISTS(SELECT 1 FROM                                                                
    POL_HOME_RATING_INFO WITH (NOLOCK) INNER  JOIN MNT_LOOKUP_VALUES WITH (NOLOCK)                                                                                      
    ON POL_HOME_RATING_INFO.PROT_CLASS = MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                                                                        
   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID   
    AND (ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0')<>'8B' AND ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE,'0')<>'09'))  
    BEGIN  
     SET @SUBURBANREFERVALUE='PROT_CLASS'  
    END   
  -- REFREAL FOR MARKET VALUE AND REPLACMENT VALUE LESS THAN 200000  
  IF EXISTS (SELECT 1 FROM POL_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
     AND DWELLING_ID=@DWELLING_ID AND REPLACEMENT_COST<200000 AND MARKET_VALUE <200000)  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'RPMR200'  
    END  
  
  -- REFERAL FOR MARKET VALUE AND REPLACEMENT VALUE NOT EQUAL  
  
    IF EXISTS (SELECT 1 FROM  POL_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
     AND DWELLING_ID=@DWELLING_ID AND ISNULL(REPLACEMENT_COST,0)<>ISNULL(MARKET_VALUE,0))  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'RPMRNOTEQ'  
    END  
  
  -- REFERAL FOR YEAR BUILT NOT WITHIN THE LAST 20 YEARS  
   IF EXISTS (SELECT 1 FROM  POL_DWELLINGS_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
     AND DWELLING_ID=@DWELLING_ID AND (@APP_EFF_DATE-ISNULL(POL_DWELLINGS_INFO.YEAR_BUILT,0)>20))--convert(nvarchar(20),DATEDIFF(YEAR,convert(nvarchar(20),ISNULL(POL_DWELLINGS_INFO.YEAR_BUILT,0)),@APP_EFF_DATE))>20)  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'DWELL20'  
    END  
  
  -- REFERAL FOR # OF MILES FROM FIRE STATION MUST BE 5 OR LESS  
  
   IF EXISTS (SELECT 1 FROM POL_HOME_RATING_INFO WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID   
      and DWELLING_ID=@DWELLING_ID AND FIRE_STATION_DIST>5)  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'MILES5'  
    END      
  
  -- REFERAL FOR DWELLING CONTAINS A SOLID FUEL HEATING DEVISE  
  
   IF EXISTS(SELECT 1 FROM POL_HOME_OWNER_SOLID_FUEL WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'N')='Y')  
  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'SLDHEATDEV'  
    END  
  
  -- REFERAL FOR RESIDENCE IS A MODULAR OR MANUFACTURE HOME  
  
   IF EXISTS(SELECT 1 FROM POL_HOME_OWNER_GEN_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID   
    AND POLICY_VERSION_ID = @POLICY_VERSION_ID  AND MODULAR_MANUFACTURED_HOME=1)  
    BEGIN  
     SET @SUBURBANREFERVALUE= @SUBURBANREFERVALUE +'~'+'MODMANHOME'  
    END  
   if(@SUBURBANREFERVALUE<>'')  
    SET  @SUBURBAN_CLASS = @SUBURBAN_CLASS+'~'+ @SUBURBANREFERVALUE  
 END  
  
  
  -- RENEWAL REFFERAL RULE FOR SUBURBAN DISCOUNT  
DECLARE @IS_NEW_BUSINESS INT  
  
SELECT @IS_NEW_BUSINESS=COUNT(NEW_POLICY_VERSION_ID)   FROM   
    POL_POLICY_PROCESS PPPS  WITH (NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPLS  WITH (NOLOCK)  
    ON PPPS.CUSTOMER_ID=PCPLS.CUSTOMER_ID AND   
    PPPS.POLICY_ID=PCPLS.POLICY_ID AND PPPS.NEW_POLICY_VERSION_ID=PCPLS.POLICY_VERSION_ID   
    AND PPPS.PROCESS_STATUS!='ROLLBACK'  
    WHERE PPPS.PROCESS_ID IN (24,25,5,18,31,32)   
    AND ISNULL(REVERT_BACK,'N') = 'N' AND PPPS.CUSTOMER_ID=@CUSTOMER_ID AND PPPS.POLICY_ID=@POLICY_ID   
     
   
DECLARE @SUBURBAN_RENEWAL_PRIOR_TERM_POLICY_VERSION_ID INT  
SELECT  @SUBURBAN_RENEWAL_PRIOR_TERM_POLICY_VERSION_ID=MAX(NEW_POLICY_VERSION_ID)  FROM POL_POLICY_PROCESS PPPS  WITH (NOLOCK) INNER JOIN POL_CUSTOMER_POLICY_LIST PCPLS  WITH (NOLOCK)  
    ON PPPS.CUSTOMER_ID=PCPLS.CUSTOMER_ID AND   
    PPPS.POLICY_ID=PCPLS.POLICY_ID AND PPPS.NEW_POLICY_VERSION_ID=PCPLS.POLICY_VERSION_ID   
    AND PPPS.PROCESS_STATUS!='ROLLBACK'  
    WHERE PPPS.PROCESS_ID IN (25,18,32) and PPPS.PROCESS_STATUS='COMPLETE'-- MIN OF NBS OR RENEWAL  
    AND ISNULL(REVERT_BACK,'N') = 'N' AND PPPS.CUSTOMER_ID=@CUSTOMER_ID AND PPPS.POLICY_ID=@POLICY_ID  
IF(@IS_NEW_BUSINESS=1)  
 BEGIN   
  SET @SUBURBAN_RENEWAL_REFERAL = 'N'  
 END  
ELSE  
BEGIN  
 IF EXISTS(SELECT 1 FROM POL_HOME_RATING_INFO   WITH(NOLOCK)                                   
   WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @SUBURBAN_RENEWAL_PRIOR_TERM_POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID  
   AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963')  
  BEGIN  
   SET @SUBURBAN_RENEWAL_REFERAL = 'Y'  
  END  
 IF(@SUBURBAN_RENEWAL_REFERAL='Y')  
  BEGIN  
   IF EXISTS(SELECT 1 FROM POL_HOME_RATING_INFO   WITH(NOLOCK)                                   
     WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID  
     AND  SUBURBAN_CLASS='Y' AND LOCATED_IN_SUBDIVISION='10963')  
    BEGIN  
     if(@SUBURBAN_RENEWAL_PRIOR_TERM_POLICY_VERSION_ID<>'')  
      begin  
       SET @SUBURBAN_RENEWAL_REFERAL = 'N'  
      end  
    END  
   ELSE  
    BEGIN  
     if(@SUBURBAN_RENEWAL_PRIOR_TERM_POLICY_VERSION_ID<>'')  
      begin  
       SET @SUBURBAN_RENEWAL_REFERAL = 'Y'  
      end  
    END   
  END  
 ELSE   
  BEGIN  
   SET @SUBURBAN_RENEWAL_REFERAL = 'N'   
  END  
  
END  
-----------------------------------     
SELECT                                                                                          
-- Mandatory                                                                                    
 --@HYDRANT_DIST as HYDRANT_DIST,                  
 --@FIRE_STATION_DIST as FIRE_STATION_DIST,                                                                                          
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
 @IS_RECORD_EXIST as IS_RECORD_EXIST,                     
 @DWELL_UNDER_CONSTRUCTION as DWELL_UNDER_CONSTRUCTION,                            
 @IS_SUPERVISED as IS_SUPERVISED,                      
--HO-4 or HO-5                       
 @NEED_OF_UNITS AS NEED_OF_UNITS,            
 @CENT_BURG_FIRE_ALARM_CERT_ATTACHED  AS  CENT_BURG_FIRE_ALARM_CERT_ATTACHED,      
 @PROT_DEVC_ALARM_CERT_ATTACHED AS PROT_DEVC_ALARM_CERT_ATTACHED, --Added by Charles on 20-Oct-09 for Itrack 6586      
 @SUBURBAN_CLASS AS SUBURBAN_CLASS,  
 @LOCATION_TEN_HOME AS LOCATION_TEN_HOME,  
 @SUBURBAN_RENEWAL_REFERAL AS SUBURBAN_RENEWAL_REFERAL  
END        