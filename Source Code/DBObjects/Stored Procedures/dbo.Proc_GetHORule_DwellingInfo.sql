IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_DwellingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_DwellingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                                                                                      
Proc Name                : Dbo.Proc_GetHORule_DwellingInfo 935,141,1,1,'d'                                                                                                                                                        
Created by               : Ashwani                                                                                                                                                                      
Date                     : 01 Dec.,2005                                                                                                                      
Purpose                  : To get the Dwelling Info for HO rules                                                                                                                      
Revison History          :                                                                                                                                                                      
Used In                  : Wolverine                                                                                                                                                                      
------------------------------------------------------------                                                                                                                                                                      
Date     Review By          Comments                                                                                                                                                                      
------   ------------       -------------------------*/                       
-- drop proc dbo.Proc_GetHORule_DwellingInfo 1692,76,1,1,''                         
CREATE  proc dbo.Proc_GetHORule_DwellingInfo                                                                                             
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
 -- DECLARE @INTDWELLING_NUMBER  int                                                                                                  
 DECLARE @DWELLING_NUMBER  INT                                                                                                
 DECLARE @INTLOCATION_ID INT                                                                                                  
 DECLARE @LOCATION_ID CHAR           
 DECLARE @YEAR_BUILT INT            
 DECLARE @INTREPLACEMENT_COST DECIMAL                                     
 DECLARE @REPLACEMENT_COST CHAR                                  
 DECLARE @DECMARKET_VALUE DECIMAL                                  
 DECLARE @MARKET_VALUE CHAR                                                                           
 DECLARE @INTBUILDING_TYPE INT                                                                    
 DECLARE @BUILDING_TYPE CHAR                                                                         
 DECLARE @OCCUPANCY CHAR                            
 DECLARE @LOCATION_IS INT                                 
                     
 DECLARE @INTOCCUPANCY INT                            
 DECLARE @LOCATION_TYPE INT                                                 
                                             
--Other Structures:                               
 /*            
  DECLARE @DETACHED_OTHER_STRUCTURES CHAR                                                  
  DECLARE @INTDETACHED_OTHER_STRUCTURES INT                                                  
  DECLARE @PREMISES_LOCATION CHAR                                                  
  DECLARE @INTPREMISES_LOCATION INT            
 */                                                  
                                                                                                  
IF EXISTS (SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO                                                                         
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                                                                  
BEGIN                                                                                                   
 SELECT              
 @DWELLING_NUMBER=ISNULL(DWELLING_NUMBER,0),            
 @INTLOCATION_ID=ISNULL(LOCATION_ID,0),            
 @YEAR_BUILT=ISNULL(YEAR_BUILT,0),                                                                                                  
 @INTREPLACEMENT_COST=ISNULL(REPLACEMENT_COST,0),            
 @DECMARKET_VALUE=ISNULL(MARKET_VALUE,0),            
 @INTBUILDING_TYPE=ISNULL(BUILDING_TYPE,0),            
 @INTOCCUPANCY=ISNULL(OCCUPANCY,-1) /*,@INTDETACHED_OTHER_STRUCTURES=ISNULL(DETACHED_OTHER_STRUCTURES,-1),@INTPREMISES_LOCATION=ISNULL(PREMISES_LOCATION,-1)*/                                          
 FROM APP_DWELLINGS_INFO                                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                                                                                          
END                                                                                                 
ELSE                                                                                                  
BEGIN                                                                           
 set @DWELLING_NUMBER =0                                                                                                  
 set @LOCATION_ID =''                                                                                                  
 set @YEAR_BUILT =0                                                                                                  
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
END                                            
----------------------------------------8961 --BLANK           
          
DECLARE @LOC_TYPE int                                   
SELECT @LOC_TYPE=LOCATION_TYPE FROM APP_LOCATIONS   WITH(NOLOCK)                                
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID            
              
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
 DECLARE @POLICY_TYPE INT                                                                      
 DECLARE @STATE_ID INT   
 DECLARE @DATE_APP_EFFECTIVE_DATE DATETIME --Added by Charles on 18-Dec-09 for Itrack 6681
 SELECT @POLICY_TYPE=POLICY_TYPE,@STATE_ID=ISNULL(STATE_ID,0), @DATE_APP_EFFECTIVE_DATE= CAST(CONVERT(VARCHAR,APP_EFFECTIVE_DATE,101) AS DATETIME) --@DATE_APP_EFFECTIVE_DATE Added by Charles on 18-Dec-09 for Itrack 6681                                                                  
 FROM APP_LIST WITH(NOLOCK)                                                                                              
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                            
                          
                          
----IF Location Type ------------------------------------------                          
                          
 SELECT @LOCATION_TYPE=LOCATION_TYPE                           
 FROM APP_LOCATIONS                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                          
                                                                                          
-- Repair Cost Policy :Removed repair cost policy rule as the field is common                                                                                              
 /*                                
 DECLARE @REPAIR_COST_POLICY CHAR                                                   
 IF(@POLICY_TYPE=11193 OR @POLICY_TYPE=11403 OR @POLICY_TYPE=11194 OR @POLICY_TYPE=11404)          
 BEGIN                   
  IF(@INTREPLACEMENT_COST > @DECMARKET_VALUE)                                                                                          
   BEGIN                             
   SET @REPAIR_COST_POLICY='Y'                     
END                                                                                         
  ELSE                             
   BEGIN                                    
   SET @REPAIR_COST_POLICY='N'                                                                              
   END                                                                                          
 END                               
 ELSE                                                       
  BEGIN                                             
  SET @REPAIR_COST_POLICY='N'                                                       
  END             
 */                                                                                          
                                                    
------------------------------------------------------------------------------                                                                                         
--(HO2, HO3, HO5) when year of construction is pre 1940, require market value be input.                                                 
--All Replacement cost and premier Programs(Ho-2,HO-3,Ho-5 Replacement and HO-3,HO-5 Premier )                                                
--Market value is mandatory only for dwellings prior to 1940                                                                   
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
 IF(@DECMARKET_VALUE=0 AND (@POLICY_TYPE=11195 OR @POLICY_TYPE=11405 OR @POLICY_TYPE=11245 OR @POLICY_TYPE=11207 OR @POLICY_TYPE=11246 OR @POLICY_TYPE=11208                                                                     
 OR @POLICY_TYPE=11196 OR @POLICY_TYPE=11406))   
 BEGIN                                                           
 SET @MARKET_VALUE='N'                                                                    
 END                                                        
----------------------------------------------------------------------------------------------------------------------                                                                    
--  Indiana HO-2/HO-3 Repair, we need Market value                                                
--All Repair cost Programs(HO-2,HO-3 Repar cost)             
--Market value is always mandatory field irrespective of year                                                                     
 IF((@DECMARKET_VALUE=0) AND (@POLICY_TYPE=11193 OR @POLICY_TYPE=11194 OR @POLICY_TYPE=11404 OR @POLICY_TYPE=11403))                                  
 BEGIN                                                                     
 SET @MARKET_VALUE=''                                                   
 END                                                                    
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
 end */             
                                                                                
DECLARE @HO5_REP_COST_COVA CHAR                                                                                              
DECLARE @INTCOVERAGEA DECIMAL                        
SELECT  @INTCOVERAGEA = (CASE  WHEN M.COV_CODE ='DWELL' THEN ISNULL(COV_INFO.LIMIT_1,-1) END)             
FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                      
INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                      
AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                                      
AND (COV_INFO.APP_ID = @APPID)                                                                       
AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                       
AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='DWELL'                                              
                                           
/* select  @INTCOVERAGEA =isnull(DWELLING_LIMIT,0)                                                                                                             
 from  APP_DWELLING_COVERAGE                                                                       
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID   */                                    
                                                                                              
                                                               
 SELECT @INTREPLACEMENT_COST=ISNULL(REPLACEMENT_COST,0)                                                                                  
 FROM APP_DWELLINGS_INFO                                                                                   
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID  

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
--SELECT @GF_REP_COST,@STATE_ID,@DATE_APP_EFFECTIVE_DATE 

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
IF(@INTLOCATION_ID=0)                                                                             
 BEGIN                        
 SET @LOCATION_ID =''                                                            
 END                                                   
ELSE                                                                                                
 BEGIN                                                                                                 
 SET @LOCATION_ID ='N'                                                                                                  
 END                                                                                                 
--                                     
--Check for Repair Cost Policy :                                   
IF(@POLICY_TYPE<>11193 AND @POLICY_TYPE<>11194 AND @POLICY_TYPE<>11404 AND @POLICY_TYPE<>11403)                                                                    
BEGIN                                                                     
 IF(@INTREPLACEMENT_COST=0)                                                                                       
  BEGIN                                                                                                 
  SET @REPLACEMENT_COST =''                                                                                                  
  END                                                                                                 
 ELSE                                                                                                
  BEGIN                                                                                                 
  SET @REPLACEMENT_COST ='N'                                                                                                  
  END                               
END                                  
ELSE                                  
BEGIN          
SET @REPLACEMENT_COST ='N'                                                                                                  
END                                         
--------------------                                                                  
---------------------------------Indiana Only -                                                               
--HO-2, HO-3 & HO-5 Policies only Dwelling Details Year Built Field is prior to 1950                                                                   
--then look at the Insurance Score on the Client Details Tab - if this amount is under 600 REJECT RULE                                                                  
                                                                  
DECLARE @INSURANCE_SCORE INT                                                            
DECLARE @PRIOR_YEAR_BUILT CHAR(1)                             
SELECT @INSURANCE_SCORE=CUSTOMER_INSURANCE_SCORE FROM CLT_CUSTOMER_LIST                                                                   
WHERE CUSTOMER_ID=@CUSTOMERID                           
                                                                  
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
--------------------                                                                 
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
SELECT @EFF_DATE = YEAR(APP_EFFECTIVE_DATE)                                                                             
FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                                                 
                              
                            
---get roofing update status from Rating info                                                                
       SELECT @INTROOF_UPDATE =ISNULL(ROOFING_RENOVATION,8924) ,@INTWIRING_UPDATE=ISNULL(WIRING_RENOVATION,8924),                
       @INTPLUMBING_UPDATE=ISNULL(PLUMBING_RENOVATION,8924),@INTHEATING_UPDATE=ISNULL(HEATING_RENOVATION,8924)                                                           
       FROM APP_HOME_RATING_INFO WITH(NOLOCK)                 
       WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID              
  --IF(@POLICY_TYPE!=11149 AND @POLICY_TYPE!=11401) --Not applicable for HO-5 replacement                
  -- BEGIN         
        
SET @ROOF_UPDATE_YEAR='N'           
        
IF(@POLICY_TYPE!=11409 AND @POLICY_TYPE!=11410) --Not applicable for Premier Policies --ITRACK 6679, CHARLES, 26-NOV-09        
BEGIN                         
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO WITH(NOLOCK)                                                                                 
 WHERE(@EFF_DATE-YEAR_BUILT)>25 AND CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID)                                                                 
 BEGIN                    
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_RATING_INFO WITH(NOLOCK)                                                                                  
  WHERE ((@EFF_DATE-ROOFING_UPDATE_YEAR)>10 or (@EFF_DATE-WIRING_UPDATE_YEAR)>10 or (@EFF_DATE-PLUMBING_UPDATE_YEAR) >10 or (@EFF_DATE-HEATING_UPDATE_YEAR)>10 ) AND                              
  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID )                                                              
   BEGIN                                                                
  SET @ROOF_UPDATE_YEAR='Y'                                                                
   END          
 END                                                                                                                     
END           
               
  --ELSE                
  --BEGIN                
  --   SET @ROOF_UPDATE_YEAR='N'                                                                
  --END                
                 
 -- IF(@POLICY_TYPE!=11149 AND @POLICY_TYPE!=11401) --Not applicable for HO5 replacement                
 -- BEGIN            
             
SET @ROOF_UPDATE='N'         
        
IF(@POLICY_TYPE!=11409 AND @POLICY_TYPE!=11410) --Not applicable for Premier Policies --ITRACK 6679, CHARLES, 26-NOV-09        
BEGIN                                                               
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO WITH(NOLOCK)                                                                                  
 WHERE(@EFF_DATE-YEAR_BUILT)>25 AND CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID)                                                                 
 IF(@INTROOF_UPDATE=8924 OR   @INTWIRING_UPDATE=8924 OR  @INTPLUMBING_UPDATE=8924 OR  @INTHEATING_UPDATE=8924 )                
 BEGIN                                                                
 SET @ROOF_UPDATE='Y'                                                                
 END                                                                  
END                  
        
--Added by Charles on 15-Dec-09 for Itrack 6679    
IF @ROOF_UPDATE='Y' AND @ROOF_UPDATE_YEAR='Y'      
 BEGIN      
 SET @ROOF_UPDATE='N'      
 END --Added till here        
    
--Added by Charles on 26-Nov-09 for Itrack 6679          
DECLARE @ROOF_UPDATE_PREMIER CHAR                                                                
DECLARE @ROOF_UPDATE_YEAR_PREMIER CHAR         
        
SET @ROOF_UPDATE_PREMIER = 'N'        
SET @ROOF_UPDATE_YEAR_PREMIER = 'N'        
        
IF(@POLICY_TYPE = 11409 OR @POLICY_TYPE = 11410) --Premier Policies         
BEGIN         
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLINGS_INFO WITH(NOLOCK) WHERE(@EFF_DATE-YEAR_BUILT)>25         
 AND CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID)        
 BEGIN         
  IF(@INTROOF_UPDATE = 8924 OR @INTWIRING_UPDATE = 8924 OR @INTPLUMBING_UPDATE = 8924 OR @INTHEATING_UPDATE = 8924)        
  BEGIN        
   SET @ROOF_UPDATE_PREMIER = 'Y'        
  END        
          
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_HOME_RATING_INFO WITH(NOLOCK)                                                                                  
  WHERE ((@EFF_DATE-ROOFING_UPDATE_YEAR)>10 OR (@EFF_DATE-WIRING_UPDATE_YEAR)>10 OR (@EFF_DATE-PLUMBING_UPDATE_YEAR) >10 OR (@EFF_DATE-HEATING_UPDATE_YEAR)>10)                              
     AND CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                              
     BEGIN                                                                
   SET @ROOF_UPDATE_YEAR_PREMIER='Y'                                                                
     END        
 END        
       
 IF @ROOF_UPDATE_PREMIER='Y' AND @ROOF_UPDATE_YEAR_PREMIER='Y'      
 BEGIN      
 SET @ROOF_UPDATE_PREMIER='N'      
 END      
END                               
--Added till here        
                                                                                                          
---------------------------------------------------------"Location Details : 13 June 2006                 
--If Location is Seasonal Look at the Dwelling Info Tab                
--If seasonal,# of weeks rented Field                  
--If this number is greater than 4 Refer to underwriter"                                                              
                
                
                
Declare @INTLOCATION_TYPE INT                                                              
Declare @LOCATIONID int                                          
Declare @INTNO_WEEKS_RENTED int                                                              
Declare @NO_WEEKS_RENTED CHAR                                                              
--Get the location ID                                                              
SELECT @LOCATIONID = LOCATION_ID,                                                              
       @INTNO_WEEKS_RENTED = ISNULL(NO_WEEKS_RENTED,0) FROM APP_DWELLINGS_INFO                                                                                                                     
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID                                                              
--Get the  location type                           
SELECT @INTLOCATION_TYPE =ISNULL(LOCATION_TYPE,0) FROM APP_LOCATIONS                                                               
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and LOCATION_ID=@LOCATIONID                                                              
                      
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
                                                    
DECLARE @REPLACEMENT_COST_YEAR CHAR                                                          
IF(@POLICY_TYPE=11192 OR @POLICY_TYPE=11148 OR @POLICY_TYPE=11149 OR                                                    
@POLICY_TYPE=11400 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11402)                          
 BEGIN                                                          
  IF(@YEAR_BUILT<1940)                                                          
  BEGIN                              
    IF(@DECMARKET_VALUE < (@INTREPLACEMENT_COST * 0.80))                                                          
     BEGIN                                                          
     SET @REPLACEMENT_COST_YEAR='Y'                                                          
     END                                  
   ELSE                                                          
     BEGIN                                                        
     SET @REPLACEMENT_COST_YEAR='N'                                                          
     END                                                          
  END                                           
 ELSE                                                          
  BEGIN                                                          
  SET @REPLACEMENT_COST_YEAR='N'                                                          
  END                                                           
END                                                          
ELSE                                                          
BEGIN                                                          
SET @REPLACEMENT_COST_YEAR='N'                                                     
END                                                          
                                                    
--HO-3 regular in MI the minimum amount of $50,000             
--HO-3 Repair Cost minimum of $50,000 :Modified 19 June 2006                                                      
--11400 : HO-3 Replacement                                                
--11148 : HO-3 Replacement                                              
                                            
--11404 : HO-3 Repair Cost                        
--11194 : HO-3 Repair Cost                                            
Declare @REPLACEMENT_COST_HO3 char                          
Declare @REPLACEMENT_COST_HO3SECONDARY char             
            
--Itrack No. 3281 3 Jan 2008            
DECLARE @APP_EFFECTIVE_DATE VARCHAR(20),            
 @EFFECTIVE_DATE VARCHAR(20)            
 SET @EFFECTIVE_DATE='01/01/2008'            
 SELECT @APP_EFFECTIVE_DATE=ISNULL(DATEDIFF(DAY,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101),@EFFECTIVE_DATE),'') FROM APP_LIST  WITH(NOLOCK)                                                                                 
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                     
                                                      
--if(@STATE_ID=22)--MI ,11812-->Primary                                                      
--begin                             
 IF(@APP_EFFECTIVE_DATE <= 0) -- Itrack No 3281            
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
---IF Location Type Secondany=11813 and Seasonal =11814                          
IF(@APP_EFFECTIVE_DATE <= 0) -- Itrack No 3281            
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
                                                     
--end                                                      
--else                              
--begin                                                      
--   set @REPLACEMENT_COST_HO3='N'                                                      
--end                                                   
                                                  
----------------------------------OTHER STRUCTURES:                                                  
 /*            
  IF(@INTDETACHED_OTHER_STRUCTURES=-1)                                                  
  BEGIN                                                  
  SET @DETACHED_OTHER_STRUCTURES=''                                                  
  END                       
  IF(@INTDETACHED_OTHER_STRUCTURES = 1 AND @INTPREMISES_LOCATION=-1)                                                  
  BEGIN                          
  SET @PREMISES_LOCATION=''                                                  
  END             
 */                            
----------------------------END OTHER STRUCTURES--------                                              
                                              
----------------------------START-----                    
--"Dwelling under Construction field is only mandatory,                                                 
--IF house built within the last 2 years :Modified :19 June 2006                                               
/*DECLARE @HOME_YAER_DIFF INT                    
                                         
 DECLARE @DWELL_UNDER_CONSTRUCTION CHAR                                              
                            
 SELECT @HOME_YAER_DIFF = (@EFF_DATE-YEAR_BUILT)  FROM APP_DWELLINGS_INFO                                                        
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                                
                       
 IF NOT EXISTS (SELECT IS_UNDER_CONSTRUCTION FROM APP_HOME_RATING_INFO                                                                                                  
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
  SELECT @DWELL_UNDER_CONSTRUCTION = ISNULL(IS_UNDER_CONSTRUCTION,'') FROM APP_HOME_RATING_INFO                                                                                                  
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
*/             
/*            
IF "Do you want to add a boat with this Homeowner policy"? is 'Yes' and No boat added with policy .            
            
 DECLARE @IS_BOAT_ATCH_WITH_HOMEOWNER   VARCHAR(50)             
 SELECT  @IS_BOAT_ATCH_WITH_HOMEOWNER =ISNULL(BOAT_WITH_HOMEOWNER,'')  FROM APP_HOME_OWNER_GEN_INFO                                                                       
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID            
 IF(@IS_BOAT_ATCH_WITH_HOMEOWNER='1')                                              
 BEGIN                                               
 SET @IS_BOAT_ATCH_WITH_HOMEOWNER='Y'                                              
 END                                    
 ELSE IF(@IS_BOAT_ATCH_WITH_HOMEOWNER='0')                                              
 BEGIN                                               
 SET @IS_BOAT_ATCH_WITH_HOMEOWNER='N'                                              
 END */            
              
--Added by Charles on 27-Nov-09 for Itrack 6681             
DECLARE @SOLID_FUEL_DEVICE CHAR  
SET @SOLID_FUEL_DEVICE = 'N'  
  
IF EXISTS(SELECT CUSTOMER_ID FROM APP_OTHER_STRUCTURE_DWELLING WITH(NOLOCK) WHERE ISNULL(SOLID_FUEL_DEVICE,'10964')='10963'  
AND CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID AND ISNULL(IS_ACTIVE,'N') = 'Y')  
BEGIN  
 SET @SOLID_FUEL_DEVICE = 'Y'  
END  
--Added till here       
            
-----------------------------------------                                      
SELECT                           
 @DWELLING_NUMBER as DWELLING_NUMBER,                                                                                                  
 @LOCATION_ID as LOCATION_ID,                                                                                        
 @YEAR_BUILT as YEAR_BUILT,                                                                             
 @REPLACEMENT_COST as REPLACEMENT_COST ,                                                     
 -- Rules                                                             
 @HO5_REP_COST_COVA as HO5_REP_COST_COVA, -- HO-5                                           
 @MARKET_VALUE as MARKET_VALUE,                                                             
 --@REPAIR_COST_POLICY as REPAIR_COST_POLICY, -- Repair Cost Policy                                                                                                           
 @BUILDING_TYPE as BUILDING_TYPE,                                                                        
 @OCCUPANCY as OCCUPANCY,                                  
 --@REPAIRCOST_MARKETVALUE as REPAIRCOST_MARKETVALUE  ,                                                                  
 @PRIOR_YEAR_BUILT as PRIOR_YEAR_BUILT ,            
 @ROOF_UPDATE as ROOF_UPDATE,                      
 @ROOF_UPDATE_YEAR as ROOF_UPDATE_YEAR ,                                                              
 @NO_WEEKS_RENTED as NO_WEEKS_RENTED  ,                                    
 --Replacement cost                                                          
 @REPLACEMENT_COST_YEAR as REPLACEMENT_COST_YEAR,                                                   
 @REPLACEMENT_COST_HO3 as REPLACEMENT_COST_HO3 ,                          
 @REPLACEMENT_COST_HO3SECONDARY as REPLACEMENT_COST_HO3SECONDARY,        
 @ROOF_UPDATE_PREMIER AS ROOF_UPDATE_PREMIER, --Added by Charles on 26-Nov-09 for Itrack 6679          
 @ROOF_UPDATE_YEAR_PREMIER AS ROOF_UPDATE_YEAR_PREMIER, --Added by Charles on 26-Nov-09 for Itrack 6679          
 @SOLID_FUEL_DEVICE AS SOLID_FUEL_DEVICE,  --Added by Charles on 27-Nov-09 for Itrack 6681             
 @HO5_REP_COST_COVA_NEW AS HO5_REP_COST_COVA_NEW --Added by Charles on 18-Dec-09 for Itrack 6681
           
 -- @IS_BOAT_ATCH_WITH_HOMEOWNER AS IS_BOAT_ATCH_WITH_HOMEOWNER               
 ---                                                  
 -- @DETACHED_OTHER_STRUCTURES as DETACHED_OTHER_STRUCTURES,                                                  
 -- @PREMISES_LOCATION as PREMISES_LOCATION,                                       
 -- @DWELL_UNDER_CONSTRUCTION as DWELL_UNDER_CONSTRUCTION             
                
END                
            
            
            
             
GO

