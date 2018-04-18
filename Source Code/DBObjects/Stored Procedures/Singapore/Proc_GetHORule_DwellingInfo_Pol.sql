/* ----------------------------------------------------------                                                                                                                          
Proc Name                : Dbo.Proc_GetHORule_DwellingInfo_Pol                                                                                                                        
Created by               : Ashwani                                                                                                                          
Date                     : 02 Mar 2006                                                                     
Purpose                  : To get the Dwelling detail for HO policy rules                                                                          
Revison History          :                                                                                                                          
Used In                  : Wolverine                                                                                                                          
------------------------------------------------------------                                                                                                                          
Date     Review By          Comments                                                                                                                          
------   ------------       -------------------------*/                         
--drop proc dbo.Proc_GetHORule_DwellingInfo_Pol 1692,1,1,1                                                                                                                        
alter proc dbo.Proc_GetHORule_DwellingInfo_Pol                                                                      
(                                                                                                                          
@CUSTOMER_ID    int,                                                                                                                          
@POLICY_ID    int,                                                                                                                          
@POLICY_VERSION_ID   int,                                                                          
@DWELLING_ID int                                                                                      
)                                                                                                                          
as                                                                                                                              
begin                                                                             
 -- Mandatory                                                                            
-- declare @INTDWELLING_NUMBER  int                                                                          
 declare @DWELLING_NUMBER  int                                                                        
 declare @INTLOCATION_ID int                                                                          
 declare @LOCATION_ID char                                                                        
 declare @YEAR_BUILT int                                                                          
 declare @INTREPLACEMENT_COST decimal                                                                          
 declare @REPLACEMENT_COST char                                                                    
 declare @DECMARKET_VALUE decimal                                                                  
 declare @MARKET_VALUE char                                                                  
 declare @INTBUILDING_TYPE int                                                                
 declare @BUILDING_TYPE char                                                 
 declare @OCCUPANCY char                                                               
 declare @INTOCCUPANCY int                       
 declare @LOCATION_TYPE int                                        
                                        
--Other Structures:                                                
/* declare @DETACHED_OTHER_STRUCTURES char                                                
 declare @INTDETACHED_OTHER_STRUCTURES int                                                
 declare @PREMISES_LOCATION char                                                
 declare @INTPREMISES_LOCATION int */                                           
                           
                             
 if exists (select CUSTOMER_ID from POL_DWELLINGS_INFO     with(nolock)                                                                                        
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID)                                                                          
 begin                                                             
  select   @DWELLING_NUMBER=isnull(DWELLING_NUMBER,0),@INTLOCATION_ID=isnull(LOCATION_ID,0),@YEAR_BUILT=isnull(YEAR_BUILT,0),                                                                          
   @INTREPLACEMENT_COST=isnull(REPLACEMENT_COST,0),@DECMARKET_VALUE=isnull(MARKET_VALUE,0),@INTBUILDING_TYPE=isnull(BUILDING_TYPE,0)                                                                          
   ,@INTOCCUPANCY=isnull(OCCUPANCY,-1)/*,@INTDETACHED_OTHER_STRUCTURES=ISNULL(DETACHED_OTHER_STRUCTURES,-1),@INTPREMISES_LOCATION=isnull(PREMISES_LOCATION,-1)          */                                      
 from POL_DWELLINGS_INFO  with(nolock)                                                                        
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                                    
 end                                                                          
else                                                                          
begin                                                                           
 set @DWELLING_NUMBER =''                                                                          
 set @LOCATION_ID =''                                                                          
 set @YEAR_BUILT =''                                                                          
 set @REPLACEMENT_COST =''                                                                   
 set @MARKET_VALUE=''                                                                
 set @BUILDING_TYPE=''                                                  
 set @OCCUPANCY=''                                                
 set @INTOCCUPANCY=-1                                         
--Modified on : 22 June 2006                                                
 --set @DETACHED_OTHER_STRUCTURES=''                                                
 --set @INTDETACHED_OTHER_STRUCTURES=-1                                                
 --set @PREMISES_LOCATION=''                                                
 --set @INTPREMISES_LOCATION=-1                                                                        
end                                                   
                                                                
----------------------------------------                                                
              
DECLARE @LOC_TYPE int                                       
SELECT @LOC_TYPE=LOCATION_TYPE FROM POL_LOCATIONS   WITH(NOLOCK)                                    
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                
                  
IF(@INTOCCUPANCY=8961 OR @INTOCCUPANCY=-1 OR @INTOCCUPANCY=0 or @INTOCCUPANCY= '')                                       
  BEGIN                      
  SET @OCCUPANCY=''                       
  END       
ELSE                                                                    
BEGIN                                   
  --5625 UNOCCUPIED        
  --8964 VACANT                            
  IF((@INTOCCUPANCY = 5625 OR @INTOCCUPANCY = 8964) and @LOC_TYPE != 11814 )                                
  BEGIN              
  SET @OCCUPANCY='Y'                                   
  END                                
  ELSE                                
  BEGIN                                
  SET @OCCUPANCY='N'                                
  END                       
END                                            
----------------------------------------              
                                                        
IF(@INTBUILDING_TYPE=10571 OR @INTBUILDING_TYPE=11494)                                                                       
BEGIN                                                   
SET @BUILDING_TYPE='Y'                                                                
END                                                                 
ELSE  IF(@INTBUILDING_TYPE<>10571)                                                                       
BEGIN                                                                 
SET @BUILDING_TYPE='N'                                                                
END                                                               
IF(@INTBUILDING_TYPE=0 OR @INTBUILDING_TYPE =-1)                                         
BEGIN                                                                                           
SET @BUILDING_TYPE=''                                                                                        
END                                                             
                    
-- HO-5 Rule                                                                      
                            
 declare @POLICY_TYPE int                                    
 declare @STATE_ID int        
 DECLARE @DATE_APP_EFFECTIVE_DATE DATETIME --Added by Charles on 18-Dec-09 for Itrack 6681              
 select @POLICY_TYPE=POLICY_TYPE,@STATE_ID=isnull(STATE_ID,0) , @DATE_APP_EFFECTIVE_DATE= CAST(CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS DATETIME) --@DATE_APP_EFFECTIVE_DATE Added by Charles on 18-Dec-09 for Itrack 6681                                  
  
                                    
 from POL_CUSTOMER_POLICY_LIST  with(nolock)                                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                                                                        
                          
select @LOCATION_TYPE=LOCATION_TYPE                         
 from POL_LOCATIONS                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                                                              
-- Repair Cost Policy                                                                     
 /*declare @REPAIR_COST_POLICY char                                                                  
                                                                    
 if(@POLICY_TYPE=11193 or @POLICY_TYPE=11403 or @POLICY_TYPE=11194 or @POLICY_TYPE=11404)                                                   
 begin                                                                   
 if(@INTREPLACEMENT_COST > @DECMARKET_VALUE)                                                                  
 begin                                                                   
  set @REPAIR_COST_POLICY='Y'                                                             
 end                                                                 
 else                                                                  
 begin                                                                   
  set @REPAIR_COST_POLICY='N'                                                     
 end                                               
 end                                                                   
 else                                                                  
 begin                
  set @REPAIR_COST_POLICY='N'                                                                   
 end */                                                                  
-----------------------------------------------------------                                                                  
--(HO2, HO3, HO5) when year of construction is pre 1940, require market value be input.                                          
              
SET @MARKET_VALUE ='N'                                             
 IF(@YEAR_BUILT<1940)                                          
 BEGIN                         
  IF(@POLICY_TYPE=11192 OR @POLICY_TYPE=11402 OR @POLICY_TYPE=11148 OR @POLICY_TYPE=11400 OR                                                    
  @POLICY_TYPE=11149 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11410)                                    
   BEGIN                                           
   IF(@DECMARKET_VALUE=0)                                                                        
  BEGIN                                                                       
   SET @MARKET_VALUE =''                                                                        
  END                                                                    
   END                                          
 END                                           
                                          
----------------------------------------------------------------------------------------------------------------------                                          
--We don't need market value for either HO-4 or HO-6( including Deluxe)                                          
 if(@DECMARKET_VALUE=0 and (@POLICY_TYPE=11195 or @POLICY_TYPE=11405 or @POLICY_TYPE=11245 or @POLICY_TYPE=11207 or @POLICY_TYPE=11246 or @POLICY_TYPE=11208                                
 or @POLICY_TYPE=11196 or @POLICY_TYPE=11406))                                          
 begin                                           
 set @MARKET_VALUE='N'                                          
 end                                           
----------------------------------------------------------------------------------------------------------------------                                          
--  Indiana HO-2/HO-3 Repair, we need Market value                        
 if((@DECMARKET_VALUE=0) and (@POLICY_TYPE=11193 or @POLICY_TYPE=11194 or @POLICY_TYPE=11404 or @POLICY_TYPE=11403))                                          
 begin                                           
  set @MARKET_VALUE=''                                          
 end                                          
-----------------------------------------------------------------------------------------------------------------                                          
-- In case of HO-2/HO-3 repair-cost Reapir Cost and Market value should be same.                                    
                                          
 /*declare @REPAIRCOST_MARKETVALUE char                                          
 set @REPAIRCOST_MARKETVALUE='N'                                          
                                          
 if(@POLICY_TYPE=11193 or @POLICY_TYPE=11194 or @POLICY_TYPE=11404 or @POLICY_TYPE=11403)                                          
 begin                                           
 if(@INTREPLACEMENT_COST!=@DECMARKET_VALUE)                                          
 begin                                           
  set @REPAIRCOST_MARKETVALUE=''                                          
    end                                           
 end*/                                              
-----------------------------------------------------------------------------------------------------------------        
 declare @HO5_REP_COST_COVA char                                                                      
 declare @INTCOVERAGEA decimal                
                
 SELECT  @INTCOVERAGEA = (CASE  WHEN M.COV_CODE ='DWELL' THEN ISNULL(COV_INFO.LIMIT_1,-1) END)                   
 FROM POL_DWELLING_SECTION_COVERAGES COV_INFO                                                    
 INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                    
 AND (COV_INFO.CUSTOMER_ID = @CUSTOMER_ID)                                                                    
 AND (COV_INFO.POLICY_ID = @POLICY_ID)                                                                     
 AND (COV_INFO.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                                     
 AND (COV_INFO.DWELLING_ID = @DWELLING_ID) WHERE M.COV_CODE ='DWELL'                                                                                                                                        
 /*select  @INTCOVERAGEA =isnull(DWELLING_LIMIT,0)                                                                                     
 from  POL_DWELLING_COVERAGE  with(nolock)                                                                                   
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID   */                                                                      
                                                                      
                                                  
 select @INTREPLACEMENT_COST=isnull(REPLACEMENT_COST,0)                          
 from POL_DWELLINGS_INFO  with(nolock)                                                         
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID                                                    
      
--Added by Charles on 18-Dec-09 for Itrack 6681    
DECLARE @GF_REP_COST CHAR(2)    
DECLARE @HO5_REP_COST_COVA_NEW CHAR    
SET @GF_REP_COST='-1'    
SET @HO5_REP_COST_COVA_NEW='N'    
    
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
    
IF((@POLICY_TYPE=11401 OR @POLICY_TYPE=11149) AND @GF_REP_COST = 'N')                                                                                                  
BEGIN                                                          
  IF(@INTCOVERAGEA >= 200000)                                                   
  BEGIN                                                                                                   
  IF(@INTCOVERAGEA=@INTREPLACEMENT_COST)                                                   
  BEGIN                                                                         
   SET @HO5_REP_COST_COVA_NEW='N'                        
  END                                                                                                   
  ELSE                                   
  BEGIN    
   SET @HO5_REP_COST_COVA_NEW='Y'                                            
  END                         
  END                                                                                                   
  ELSE                                           
  BEGIN                                       
 SET @HO5_REP_COST_COVA_NEW='Y'                                           
  END                                                
END                                                                        
--Added till here                                                
      
SET @HO5_REP_COST_COVA='N'                                                                        
                                                
IF((@POLICY_TYPE=11401 OR @POLICY_TYPE=11149) AND @GF_REP_COST = 'Y') --@GF_REP_COST Added by Charles on 18-Dec-09 for Itrack 6681                                                                                                 
BEGIN                                                          
  IF(@INTCOVERAGEA >= 125000)                                                   
  BEGIN                                                                                                   
  IF(@INTCOVERAGEA=@INTREPLACEMENT_COST)                                                   
  BEGIN                                                                         
   SET @HO5_REP_COST_COVA='N'                        
  END                                                                                                   
  ELSE                                   
  BEGIN                                                                            
   SET @HO5_REP_COST_COVA='Y'                                            
  END                         
  END                                                                                                   
  ELSE                                           
  BEGIN                                       
 SET @HO5_REP_COST_COVA='Y'                                           
  END                                                                          
END                                  
                                                                   
                                                 
--                                                  
 if(@INTLOCATION_ID=0)                                                                          
 begin                                                                         
  set @LOCATION_ID =''                                                                          
 end                                                                         
 else                                                                        
 begin                                                                         
   set @LOCATION_ID ='N'                                             
 end                                                                         
--                         
--Check for Repair Cost Policy :                               
if(@POLICY_TYPE<>11193 and @POLICY_TYPE<>11194 and @POLICY_TYPE<>11404 and @POLICY_TYPE<>11403)                                                                
begin                                 
                                                                       
 if(@INTREPLACEMENT_COST=0)                                                                                   
 begin                                                                                             
  set @REPLACEMENT_COST =0                                 
 end                     
 else                                                                                            
 begin                                                                                             
   set @REPLACEMENT_COST ='N'                                                                                              
 end                                
                              
end                              
else                              
begin                              
set @REPLACEMENT_COST ='N'                                                                     
end                                         
-----------                                        
-----------------------------------/*                                                              
-- Dwelling Details :13 June 2006                                         
-- Take the year of the effective date of the policy - Application Details - Effective Date                                
-- then subtract the Year Built from the Dwelling Details Tab - if greater than 25                                                               
-- look at the Rating Info tab if there is not date in the Roof Update field then Refer */                                                              
DECLARE @EFF_DATE int                                                                     
DECLARE @INTROOF_UPDATE int                 
DECLARE @INTWIRING_UPDATE INT                 
DECLARE @INTPLUMBING_UPDATE INT                       
DECLARE @INTHEATING_UPDATE INT                                                                          
DECLARE @ROOF_UPDATE char                                                              
DECLARE @ROOF_UPDATE_YEAR char                                                              
--get date                                                              
SELECT @EFF_DATE = YEAR(POLICY_EFFECTIVE_DATE)                                                                             
FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                     
                                                              
---get roofing update status from Rating info                                                              
SELECT @INTROOF_UPDATE =ISNULL(ROOFING_RENOVATION,8924) ,@INTWIRING_UPDATE=ISNULL(WIRING_RENOVATION,8924),                    
       @INTPLUMBING_UPDATE=ISNULL(PLUMBING_RENOVATION,8924),@INTHEATING_UPDATE=ISNULL(HEATING_RENOVATION,8924)                                                        
       FROM POL_HOME_RATING_INFO WITH(NOLOCK)  WHERE                                                               
CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DWELLING_ID=@DWELLING_ID                                                                
                
--IF(@POLICY_TYPE!=11149 AND @POLICY_TYPE!=11401) --Not applicable for HO5 replacement                
--BEGIN                  
            
 SET @ROOF_UPDATE_YEAR='N'              
            
IF(@POLICY_TYPE!=11409 AND @POLICY_TYPE!=11410) --Not applicable for Premier Policies --ITRACK 6679, CHARLES, 26-NOV-09            
BEGIN                                                                                                             
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLINGS_INFO with(nolock)                                                     
 WHERE(@EFF_DATE-YEAR_BUILT)>25 AND                                                               
 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DWELLING_ID=@DWELLING_ID)                                                               
 BEGIN                                                              
   IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_RATING_INFO                                                                                 
   WHERE((@EFF_DATE-ROOFING_UPDATE_YEAR)>10 or (@EFF_DATE-WIRING_UPDATE_YEAR)>10 or (@EFF_DATE-PLUMBING_UPDATE_YEAR) >10 or (@EFF_DATE-HEATING_UPDATE_YEAR)>10 ) AND                        
   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID )                                                               
   BEGIN                                         
    SET @ROOF_UPDATE_YEAR='Y'                                                              
   END                                                                                                             
 END                                                                                                               
END                
--ELSE                
--BEGIN                
-- SET @ROOF_UPDATE_YEAR='N'                                                              
--END                
                
--IF(@POLICY_TYPE!=11149 AND @POLICY_TYPE!=11401) --Not applicable for HO5 replacement                
--BEGIN                
 SET @ROOF_UPDATE='N'               
            
IF(@POLICY_TYPE!=11409 AND @POLICY_TYPE!=11410) --Not applicable for Premier Policies --ITRACK 6679, CHARLES, 26-NOV-09            
BEGIN                                                  
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLINGS_INFO with(nolock)                                                          
 WHERE(@EFF_DATE-YEAR_BUILT)>25 AND                                                               
 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DWELLING_ID=@DWELLING_ID)                                                               
 IF(@INTROOF_UPDATE=8924 or   @INTWIRING_UPDATE=8924 or  @INTPLUMBING_UPDATE=8924 or  @INTHEATING_UPDATE=8924 )                
  BEGIN                                                              
   SET @ROOF_UPDATE='Y'                                                              
  END                                  
END                
            
--Added by Charles on 15-Dec-09 for Itrack 6679        
IF @ROOF_UPDATE='Y' AND @ROOF_UPDATE_YEAR='Y'          
 BEGIN          
 SET @ROOF_UPDATE='N'          
 END        
        
 DECLARE @ISRENEWEDPOLICY CHAR                
 SET @ISRENEWEDPOLICY = 'N'                
                  
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID                   
    AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_ID IN (5,18))                
 BEGIN                
 SET @ISRENEWEDPOLICY = 'Y'                 
 END         
--Added till here        
        
--Added by Charles on 26-Nov-09 for Itrack 6679              
DECLARE @ROOF_UPDATE_PREMIER CHAR                                                                    
DECLARE @ROOF_UPDATE_YEAR_PREMIER CHAR             
            
SET @ROOF_UPDATE_PREMIER = 'N'            
SET @ROOF_UPDATE_YEAR_PREMIER = 'N'            
            
IF(@POLICY_TYPE = 11409 OR @POLICY_TYPE = 11410) --Premier Policies             
BEGIN             
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLINGS_INFO WITH(NOLOCK) WHERE(@EFF_DATE-YEAR_BUILT)>25             
 AND CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and DWELLING_ID=@DWELLING_ID)            
 BEGIN             
  IF(@INTROOF_UPDATE = 8924 OR @INTWIRING_UPDATE = 8924 OR @INTPLUMBING_UPDATE = 8924 OR @INTHEATING_UPDATE = 8924)            
  BEGIN            
   SET @ROOF_UPDATE_PREMIER = 'Y'            
  END            
              
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_HOME_RATING_INFO WITH(NOLOCK)                                                                                      
  WHERE ((@EFF_DATE-ROOFING_UPDATE_YEAR)>10 OR (@EFF_DATE-WIRING_UPDATE_YEAR)>10 OR (@EFF_DATE-PLUMBING_UPDATE_YEAR) >10 OR (@EFF_DATE-HEATING_UPDATE_YEAR)>10)                                  
     AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID)                                                                  
     BEGIN                                                                    
   SET @ROOF_UPDATE_YEAR_PREMIER='Y'                                                                    
     END            
 END          
        
--Added by Charles on 15-Dec-09 for Itrack 6679        
 IF @ISRENEWEDPOLICY = 'Y'        
 BEGIN        
 SET @ROOF_UPDATE_PREMIER='N'         
 SET @ROOF_UPDATE_YEAR_PREMIER='N'         
 END --Added till here        
          
 IF @ROOF_UPDATE_PREMIER='Y' AND @ROOF_UPDATE_YEAR_PREMIER='Y'          
 BEGIN          
 SET @ROOF_UPDATE_PREMIER='N'          
 END             
END                                   
--Added till here                                  
                                           
-----------------------"Location Details : 13 June 2006 --If Location is Seasonal Look at the Dwelling Info Tab --If seasonal,# of weeks rented Field  --If this number is greater than 4 Refer to underwriter"         
      
        
             
              
Declare @INTLOCATION_TYPE INT            
Declare @LOCATIONID int                                       
Declare @INTNO_WEEKS_RENTED int                                                            
Declare @NO_WEEKS_RENTED CHAR                                                            
--Get the location ID                          
SELECT @LOCATIONID = LOCATION_ID,                                                            
       @INTNO_WEEKS_RENTED = ISNULL(NO_WEEKS_RENTED,0) FROM POL_DWELLINGS_INFO                                                                                                                   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID                                                            
--GET THE  LOCATION TYPE                                                            
SELECT @INTLOCATION_TYPE =ISNULL(LOCATION_TYPE,0) FROM POL_LOCATIONS                                                             
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND LOCATION_ID=@LOCATIONID                                                          
                                                            
IF (@INTLOCATION_TYPE =11814) --SEASONAL                        
BEGIN                                              
   IF(@INTNO_WEEKS_RENTED>4)                                                           
  BEGIN                                         
  SET @NO_WEEKS_RENTED ='Y'                                                        
  END                                                            
   ELSE                             
  BEGIN                                                            
  SET @NO_WEEKS_RENTED ='N'                                                            
  END                                                            
END                                                            
ELSE                             
 BEGIN                                                            
 SET @NO_WEEKS_RENTED ='N'                                                            
 END                                                            
                                                         
----------------------- END LOCATION   -----------------                                               
                                                    
--------------------------"Application Details :14 june 2006                                                      
 --On Policy types HO-2, HO-3 & HO-5 Replacement Policies Dwelling Info tab                                                       
 --If Year built is prior to 1940 then check                                                        
 --If Market Value is less than 80% of the Replacement Cost field :REFER"                                                        
                                                      
--11192 HO-2 Replacement                                                        
--11148 HO-3 Replacement                                                        
--11149 HO-5 Replacement                                                    
                                                  
--11400 HO-2 Replacement                                                    
--11401 HO-3 Replacement                                                      
--11402 HO-5 Replacement                             
                
Declare @REPLACEMENT_COST_YEAR char                                                        
if(@POLICY_TYPE=11192 or @POLICY_TYPE=11148 or @POLICY_TYPE=11149 or                                                  
  @POLICY_TYPE=11400 or @POLICY_TYPE=11401 or @POLICY_TYPE=11402)                            
begin                                                        
if(@YEAR_BUILT<1940)                                                        
 begin                                                        
  if(@DECMARKET_VALUE < (@INTREPLACEMENT_COST * 0.80))                            
    begin                                                        
      set @REPLACEMENT_COST_YEAR='Y'                                                        
    end                                            
    else                                             
    begin                                                        
      set @REPLACEMENT_COST_YEAR='N'                                                        
    end                                                        
 end                                          
 else                                                    
 begin                                                        
      set @REPLACEMENT_COST_YEAR='N'                                                        
 end                                                         
end                                                        
else                                                        
begin                                  
 set @REPLACEMENT_COST_YEAR='N'                                              
end                                           
                        
--HO-3 regular in MI the minimum amount of $50,000                                                     
--HO-3 Repair Cost minimum of $50,000 :Modified 19 June 2006                                                    
--11400 : HO-3 Replacement                                              
--11148 : HO-3 Replacement                                            
                                          
--11404 : HO-3 Repair Cost                                                
--11194 : HO-3 Repair Cost                                          
Declare @REPLACEMENT_COST_HO3 char                        
Declare @REPLACEMENT_COST_HO3SECONDARY char              
                
DECLARE @POLICY_EFFECTIVE_DATE VARCHAR(20),              
 @EFFECTIVE_DATE VARCHAR(20)              
SET @EFFECTIVE_DATE='01/01/2008'              
SELECT @POLICY_EFFECTIVE_DATE=ISNULL(DATEDIFF(DAY,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101),@EFFECTIVE_DATE),'') FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                   
                                                    
--if(@STATE_ID=22)--MI ,11812-->Primary                   
--begin                           
  IF(@POLICY_EFFECTIVE_DATE <=0) --Itrack No.3281              
  BEGIN                                                
 IF(@POLICY_TYPE=11400 OR @POLICY_TYPE=11148 )                                                        
 BEGIN                                                      
  IF((@INTREPLACEMENT_COST < 50000 OR @INTCOVERAGEA < 50000)  AND @LOCATION_TYPE='11812')                    
  BEGIN                              
  SET @REPLACEMENT_COST_HO3='Y'                                
  END                                                    
  ELSE                                                     
  BEGIN                                                    
  SET @REPLACEMENT_COST_HO3='N'       
  END                                   
 END                                                 
 ELSE                                                    
 BEGIN                                                    
 SET @REPLACEMENT_COST_HO3='N'                                                    
 END                
 END                                                 
 ELSE                                                    
BEGIN                                                    
 SET @REPLACEMENT_COST_HO3='N'                                                    
 END                        
 ---if Location Type Secondany=11813 and Seasonal =11814                        
 IF(@POLICY_EFFECTIVE_DATE <=0)               
 BEGIN                                                  
 IF(@POLICY_TYPE=11400 OR @POLICY_TYPE=11148 )                                                        
 BEGIN                                     
  IF((@INTREPLACEMENT_COST < 40000 OR @INTCOVERAGEA < 40000)  AND (@LOCATION_TYPE='11813' OR @LOCATION_TYPE='11814'))                                        
  BEGIN                                                    
  SET @REPLACEMENT_COST_HO3SECONDARY='Y'                                                    
  END                                                    
  ELSE                                                     
  BEGIN             
  SET @REPLACEMENT_COST_HO3SECONDARY='N'                                         
  END                                                    
 END                                                 
 ELSE                                                    
 BEGIN                                                    
 SET @REPLACEMENT_COST_HO3SECONDARY='N'                                                    
 END                    
   END                                                 
 ELSE                                                    
 BEGIN                                                    
 SET @REPLACEMENT_COST_HO3SECONDARY='N'                                                    
 END                 
---------------------------------Indiana Only -                                          
--HO-2, HO-3 & HO-5 Policies only Dwelling Details Year Built Field is prior to 1950                                     
--then look at the Insurance Score on the Client Details Tab - if this amount is under 600 REJECT RULE                                                              
                                                              
DECLARE @INSURANCE_SCORE INT                 
DECLARE @PRIOR_YEAR_BUILT CHAR(1)                                                              
SELECT @INSURANCE_SCORE=CUSTOMER_INSURANCE_SCORE FROM CLT_CUSTOMER_LIST                                                               
WHERE CUSTOMER_ID=@CUSTOMER_ID                                                               
                                                              
IF(@STATE_ID=14)                                                              
BEGIN                                          
     IF(@POLICY_TYPE=11192 OR @POLICY_TYPE=11193 OR @POLICY_TYPE=11194 OR                                         
      @POLICY_TYPE=11148 OR @POLICY_TYPE=11149)                                                              
      BEGIN                                                              
      IF(@YEAR_BUILT<1950)                                                                
       BEGIN                                            
                 IF(@INSURANCE_SCORE<600)                                    
      BEGIN                                                              
      SET @PRIOR_YEAR_BUILT='Y'                                                              
      END                                                              
                 ELSE                        
      BEGIN                                                              
      SET @PRIOR_YEAR_BUILT='N'                         
      END                                                                
       END                                                              
       ELSE              
   BEGIN                                                               
   SET @PRIOR_YEAR_BUILT='N'                                                              
   END                                                              
     END                       
     ELSE                                                              
 BEGIN                 
 SET @PRIOR_YEAR_BUILT='N'                                                              
 END                                                          
END                                                              
ELSE                                                     
BEGIN                  
SET @PRIOR_YEAR_BUILT='N'                                                              
END          
      
 --Added by Charles on 27-Nov-09 for Itrack 6681             
DECLARE @SOLID_FUEL_DEVICE CHAR      
SET @SOLID_FUEL_DEVICE = 'N'      
      
IF EXISTS(SELECT CUSTOMER_ID FROM POL_OTHER_STRUCTURE_DWELLING WITH(NOLOCK) WHERE ISNULL(SOLID_FUEL_DEVICE,'10964')='10963'      
AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')      
BEGIN      
 SET @SOLID_FUEL_DEVICE = 'Y'      
END      
--Added till here                                             
--------------------  --------------------                    
select                                                                          
@DWELLING_NUMBER as DWELLING_NUMBER,                                                                          
  @LOCATION_ID as LOCATION_ID,                                                                          
  @YEAR_BUILT as YEAR_BUILT,                                                                          
  @REPLACEMENT_COST as REPLACEMENT_COST ,                                                                       
-- Rules                                                                    
 @HO5_REP_COST_COVA as HO5_REP_COST_COVA, -- HO-5                                        
 @MARKET_VALUE as MARKET_VALUE,                                                           
-- @REPAIR_COST_POLICY as REPAIR_COST_POLICY, -- Repair Cost Policy                                                                                   
 @BUILDING_TYPE as BUILDING_TYPE,                                                
 @OCCUPANCY as OCCUPANCY,                                         
 --@REPAIRCOST_MARKETVALUE as REPAIRCOST_MARKETVALUE                                        
---Other Structures                                                
  --@DETACHED_OTHER_STRUCTURES as DETACHED_OTHER_STRUCTURES,                                                
  --@PREMISES_LOCATION as PREMISES_LOCATION ,                                        
  --@DWELL_UNDER_CONSTRUCTION as DWELL_UNDER_CONSTRUCTION                       
   @ROOF_UPDATE AS ROOF_UPDATE ,                      
   @ROOF_UPDATE_YEAR AS ROOF_UPDATE_YEAR ,                      
--------                      
   @REPLACEMENT_COST_YEAR AS REPLACEMENT_COST_YEAR,                      
   @REPLACEMENT_COST_HO3 AS REPLACEMENT_COST_HO3,                      
   @REPLACEMENT_COST_HO3SECONDARY AS REPLACEMENT_COST_HO3SECONDARY                      
,  @NO_WEEKS_RENTED as NO_WEEKS_RENTED,                  
   @PRIOR_YEAR_BUILT AS PRIOR_YEAR_BUILT,            
 @ROOF_UPDATE_PREMIER AS ROOF_UPDATE_PREMIER, --Added by Charles on 26-Nov-09 for Itrack 6679              
 @ROOF_UPDATE_YEAR_PREMIER AS ROOF_UPDATE_YEAR_PREMIER, --Added by Charles on 26-Nov-09 for Itrack 6679                                     
  @SOLID_FUEL_DEVICE AS SOLID_FUEL_DEVICE , --Added by Charles on 27-Nov-09 for Itrack 6681                                            
  @HO5_REP_COST_COVA_NEW AS HO5_REP_COST_COVA_NEW --Added by Charles on 18-Dec-09 for Itrack 6681    
                                                             
end 