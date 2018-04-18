IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_RatingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_RatingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                                                    
Proc Name                : Dbo.Proc_GetRDRule_RatingInfo  1151,634,1,1,'S'                                                                                                                                
Created by               : Ashwani                                                                                                                                    
Date                     : 12 Dec.,2005                                                                                    
Purpose                  : To get the rating detail for RD rules                                                                                    
Revison History          :                                            
MODIFIED BY				:PRAVESH K CHANDEL
DATE					:19 MAY
PURPOSE					: AD MANDATORY RULE FOR CONSTACTION START DATE AND iS SUPERVISED                                                                                        
Used In                  : Wolverine                                                                                                                                    
------------------------------------------------------------                                                                                                                                    
Date     Review By          Comments                                                                                                                                    
------   ------------       -------------------------*/              
-- DROP PROC dbo.Proc_GetRDRule_RatingInfo 892,1,1,1,''                                                                                                            
CREATE proc [dbo].[Proc_GetRDRule_RatingInfo]                                                                                    
(                                                                                                                                    
@CUSTOMERID    int,                                                                                                                                    
@APPID    int,                                                                                                                                    
@APPVERSIONID   int,                                                                                    
@DWELLINGID int,                                                                                                
@DESC varchar(10)                                                                                                                      
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

 declare @WIRING_RENOVATION int                                                       
 declare @WIRING_UPDATE_YEAR int          
 declare @CHRWIRING_UPDATE_YEAR char                                           
          
 declare @PLUMBING_RENOVATION int                                                       
 declare @PLUMBING_UPDATE_YEAR int                                                       
 declare @CHRPLUMBING_UPDATE_YEAR char                
 declare @HEATING_RENOVATION int                                        
 declare @HEATING_UPDATE_YEAR int                                                       
 declare @CHRHEATING_UPDATE_YEAR char
 declare @ROOFING_RENOVATION int                         
 declare @ROOFING_UPDATE_YEAR int                                                     
 declare @CHRROOFING_UPDATE_YEAR char                                              
 declare @INTNO_OF_AMPS int                                                                          
 declare @NO_OF_AMPS char                                                       
 declare @CIRCUIT_BREAKERS nvarchar(5)                                                                                    
 declare @INTEXTERIOR_CONSTRUCTION int                                                                                    
 declare @EXTERIOR_CONSTRUCTION char                                                      
 declare @EXTERIOR_OTHER_DESC varchar(250)                                                      
 declare @intROOFTYPE int                                                      
-- declare @ROOF_OTHER_DESC nvarchar(150)                                                         
 declare @PRIMARY_HEAT_TYPE int                                                       
-- declare @PRIMARY_HEAT_OTHER_DESC nvarchar(250)                                                      
 declare @SECONDARY_HEAT_TYPE int                                                       
-- declare @SECONDARY_HEAT_OTHER_DESC nvarchar(250)                                                      
-- Rules                                                   
 declare @INTNO_OF_FAMILIES  int                                                                      
 declare @NO_OF_FAMILIES char    
 declare @IS_RECORD_EXIST char 
 declare @IS_SUPERVISED char  
declare @UNDER_CONSTRUCTION_DATE char
declare @UNDER_CONSTRUCTION_START_DATE datetime
                                                  
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_RATING_INFO     WITH(NOLOCK)                                                                                                  
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                                                    
 BEGIN                             
	 SET @IS_RECORD_EXIST='Y'                                                                                   
	                                    
	  SELECT @REALHYDRANT_DIST=isnull(HYDRANT_DIST,0),@REALFIRE_STATION_DIST=isnull(FIRE_STATION_DIST,0),@IS_UNDER_CONSTRUCTION=                                                                                    
	 isnull(IS_UNDER_CONSTRUCTION,''),@PROT_CLASS=isnull(PROT_CLASS,''),@INTNO_OF_AMPS=isnull(NO_OF_AMPS,-1),@CIRCUIT_BREAKERS=                                                                                    
	 isnull(CIRCUIT_BREAKERS,''),@INTEXTERIOR_CONSTRUCTION=isnull(EXTERIOR_CONSTRUCTION,0),                                       
	 @WIRING_RENOVATION=isnull(WIRING_RENOVATION,0),@WIRING_UPDATE_YEAR=isnull(WIRING_UPDATE_YEAR,0),                                                      
	 @PLUMBING_RENOVATION=isnull(PLUMBING_RENOVATION,0),@PLUMBING_UPDATE_YEAR=isnull(PLUMBING_UPDATE_YEAR,0),                                                      
	 @HEATING_RENOVATION=isnull(HEATING_RENOVATION,0),@HEATING_UPDATE_YEAR=isnull(HEATING_UPDATE_YEAR,0),                                                
	 @ROOFING_RENOVATION=isnull(ROOFING_RENOVATION,0),@ROOFING_UPDATE_YEAR=isnull(ROOFING_UPDATE_YEAR,0),                   
	 @EXTERIOR_OTHER_DESC=isnull(EXTERIOR_OTHER_DESC,''),@intROOFTYPE=isnull(ROOF_TYPE,0),                                                      
	 @PRIMARY_HEAT_TYPE=isnull(PRIMARY_HEAT_TYPE,0),                                                     
	 @SECONDARY_HEAT_TYPE=isnull(SECONDARY_HEAT_TYPE,0),                                                  
	 @INTNO_OF_FAMILIES=isnull(NO_OF_FAMILIES,0) ,
	 @IS_SUPERVISED=isnull(IS_SUPERVISED,'') ,
	@UNDER_CONSTRUCTION_START_DATE = isnull(DWELLING_CONST_DATE,'')
	 FROM APP_HOME_RATING_INFO  WITH(NOLOCK)                                                                                  
	 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                     
	 END          
ELSE         
	 BEGIN        
	 SET @IS_UNDER_CONSTRUCTION=''        
	 SET @PROT_CLASS=''        
	 SET @NO_OF_FAMILIES=''        
	 SET @EXTERIOR_OTHER_DESC=''        
	 SET @NO_OF_AMPS=''        
	 SET @IS_RECORD_EXIST=''        
	 END                       
-----------------------  
print @IS_SUPERVISED
IF(@ROOFING_RENOVATION=8923 OR @ROOFING_RENOVATION=8922)                                                  
BEGIN                                       
	IF(@ROOFING_UPDATE_YEAR=0)                                      
		BEGIN                                       
			SET  @CHRROOFING_UPDATE_YEAR=''                                      
		END                                      
	ELSE                                      
		BEGIN                                       
			SET  @CHRROOFING_UPDATE_YEAR='N'                                      
		END                                       
END                                       
ELSE                                      
BEGIN                                       
	 SET  @CHRROOFING_UPDATE_YEAR='N'                                      
END                                        
---                                      
IF(@WIRING_RENOVATION=8923 OR @WIRING_RENOVATION=8922)                                                  
BEGIN                                       
	IF(@WIRING_UPDATE_YEAR=0)                                      
		BEGIN                                       
			SET  @CHRWIRING_UPDATE_YEAR=''                                      
		END                                      
	ELSE                                      
		BEGIN                                       
			SET  @CHRWIRING_UPDATE_YEAR='N'                                      
		END                                       
END                                       
ELSE                                      
BEGIN                                       
	SET  @CHRWIRING_UPDATE_YEAR='N'                                      
END                                        
--                                      
---                                      
IF(@PLUMBING_RENOVATION=8923 OR @PLUMBING_RENOVATION=8922)                                                  
BEGIN                                       
	IF(@PLUMBING_UPDATE_YEAR=0)                                      
		BEGIN                                       
			SET  @CHRPLUMBING_UPDATE_YEAR=''                                      
		END                                      
	ELSE                                 
		BEGIN        
			SET  @CHRPLUMBING_UPDATE_YEAR='N'                                      
		END                                
END                                       
ELSE                                      
BEGIN                      
	SET  @CHRPLUMBING_UPDATE_YEAR='N'                                      
END                                       
--   
IF(@HEATING_RENOVATION=8923 OR @HEATING_RENOVATION=8922)                                                  
BEGIN                                    
	IF(@HEATING_UPDATE_YEAR=0)                                      
		BEGIN   
			SET  @CHRHEATING_UPDATE_YEAR=''                 
		END                      
	ELSE                                      
		BEGIN                                       
			SET  @CHRHEATING_UPDATE_YEAR='N'                                      
		END                                       
END                                       
ELSE                                      
BEGIN                                       
 	SET  @CHRHEATING_UPDATE_YEAR='N'                                      
END                             
--                                                  
IF(@INTNO_OF_FAMILIES>2)                
	BEGIN                                                                   
		SET @NO_OF_FAMILIES='Y'                                                                           
	END                                                                          
ELSE IF(@INTNO_OF_FAMILIES<>0)                                                                          
	BEGIN                                                                           
		SET @NO_OF_FAMILIES='N'                                     
	END                                                                           
ELSE IF(@INTNO_OF_FAMILIES=0)                                                                          
	BEGIN                                                                           
		SET @NO_OF_FAMILIES=''                                                                          
	END                                                   
--                                                      
 IF(@REALHYDRANT_DIST=0)                                                        
	 BEGIN                                                       
		 SET @HYDRANT_DIST=''                                           
	 END                                                       
 ELSE                                                      
	BEGIN                                                       
		SET @HYDRANT_DIST='N'                                  
	END                                                       
--                                                      
 IF(@REALFIRE_STATION_DIST=0)                                                        
	 BEGIN                                                       
		 SET @FIRE_STATION_DIST=''                                                      
	 END                                                       
 ELSE                                 
	BEGIN                                                       
		SET @FIRE_STATION_DIST='N'                                            
	END                                                        
 --                                                                 
IF(@INTNO_OF_AMPS=-1)                                                        
	BEGIN                                                       
		SET @NO_OF_AMPS=''                                                      
	END                         
ELSE IF(@INTNO_OF_AMPS <100)                                                      
	BEGIN                                                       
		SET @NO_OF_AMPS='Y'                               
	END         
ELSE IF(@INTNO_OF_AMPS >100)                                                  
	BEGIN                                                 
		SET @NO_OF_AMPS='N'                                                
	END                                                 
--                                            
IF(@INTEXTERIOR_CONSTRUCTION =0)                                            
	BEGIN                                             
		SET @EXTERIOR_CONSTRUCTION=''                             
	END      
ELSE                                             
	BEGIN                                             
		SET @EXTERIOR_CONSTRUCTION='N'                                
	END                     
                                                     
--                   
IF(@INTEXTERIOR_CONSTRUCTION =0)                                            
	BEGIN                                             
		SET @EXTERIOR_OTHER_DESC=''                                      
	END                                    
ELSE IF(@INTEXTERIOR_CONSTRUCTION !=11436)                                    
	BEGIN                                             
		SET @EXTERIOR_OTHER_DESC='N'                                      
	END                     
--                    
                                 
-----------------ROOF UPDATE YEAR---                                
--If Policy Type is DP3- Repair                                
--Then look at the Dwelling Details tab - Year Built Field - If prior to 1940                                  
--Then look at the Rating Info tab - If any of the following fields are greater than 10 Submit (Refer)                                
  -- Wiring Update Year                                 
  -- Plumbing Update Year                                 
  -- Electrical Update Year                    
  -- Roof Update Year   
--WIRING_UPDATE_YEAR,PLUMBING_UPDATE_YEAR,HEATING_UPDATE_YEAR,ROOFING_UPDATE_YEAR  
                             
--Get Year                                
 DECLARE @INTYEAR_BUILT INT                                
 SELECT @INTYEAR_BUILT=ISNULL(YEAR_BUILT,0) FROM APP_DWELLINGS_INFO WITH(NOLOCK)                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                                                        
--GET POLICY                                
 DECLARE @INTPOLICY_TYPE INT                                                          
 SELECT @INTPOLICY_TYPE=ISNULL(POLICY_TYPE,0)                                                          
 FROM APP_LIST   WITH(NOLOCK)                                                        
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                    

 declare @DIFFWIRING_UPDATE_YEAR   int  
 declare @DIFFPLUMBING_UPDATE_YEAR int
 declare @DIFFHEATING_UPDATE_YEAR  int
 declare @DIFFROOFING_UPDATE_YEAR  int
 
 IF(@WIRING_RENOVATION!=8924 or @PLUMBING_RENOVATION!=8924 or @HEATING_RENOVATION!=8924 or @ROOFING_RENOVATION!=8924)
 BEGIN 
	--GET DATE                                 
	DECLARE @EFF_DATE INT                                                         
 	SELECT @EFF_DATE = YEAR(APP_EFFECTIVE_DATE)                                                                         
 	FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  
	SET @DIFFWIRING_UPDATE_YEAR   =(@EFF_DATE  - @WIRING_UPDATE_YEAR)                                                                                                                      
	SET @DIFFPLUMBING_UPDATE_YEAR =(@EFF_DATE  - @PLUMBING_UPDATE_YEAR)                                                                                                                       
	SET @DIFFHEATING_UPDATE_YEAR  =(@EFF_DATE  - @HEATING_UPDATE_YEAR)                                                                                               
	SET @DIFFROOFING_UPDATE_YEAR  =(@EFF_DATE  - @ROOFING_UPDATE_YEAR)  
                                                                                                    
	IF(@DIFFWIRING_UPDATE_YEAR>10)                                                                                                        
		 BEGIN                                   
			 SET @CHRWIRING_UPDATE_YEAR='Y'                                                                 
		 END                                                                    
	-- ELSE IF(@INTWIRING_UPDATE_YEAR<>0)                                                                                               
	--  BEGIN                                                                                                         
	--  SET @WIRING_UPDATE_YEAR='N'                                                                    
	--  END                                                                                                         
	ELSE IF(@WIRING_UPDATE_YEAR=0)                                                                                                        
		 BEGIN                                                                                                         
			 SET @CHRWIRING_UPDATE_YEAR=''                                                                    
		 END                                                                                            
	--                     
	                                                                                                   
	 IF(@DIFFPLUMBING_UPDATE_YEAR >10)                                                                                                        
		BEGIN                                          
			SET @CHRPLUMBING_UPDATE_YEAR='Y'                                                                                                        
		END                                                      
	-- ELSE IF(@INTPLUMBING_UPDATE_YEAR<>0)                                                                 
	--  BEGIN                                                             
	--  SET @PLUMBING_UPDATE_YEAR='N'                                                                                                
	--  END                                                             
	 ELSE IF(@PLUMBING_UPDATE_YEAR=0)                                                                       
		 BEGIN                      
			 SET @CHRPLUMBING_UPDATE_YEAR=''                                                                                                        
		 END                    
	                               
	--                   
	IF(@DIFFHEATING_UPDATE_YEAR>10)                                                                     
		 BEGIN                                                                                           
			 SET @CHRHEATING_UPDATE_YEAR='Y'                                                                                                        
		 END                                                     
	-- ELSE IF(@INTHEATING_UPDATE_YEAR<>0)                                               
	--  BEGIN                                                    
	--  SET @HEATING_UPDATE_YEAR='N'              
	--  END                                                                                                         
	ELSE IF(@HEATING_UPDATE_YEAR=0)                                                 
		 BEGIN                                                                                                         
			 SET @CHRHEATING_UPDATE_YEAR=''                                                                                                        
		 END                                                                                        
	--                                                                                                      
	IF(@DIFFROOFING_UPDATE_YEAR>10)                                                                                                    
		 BEGIN                                                                                                         
			 SET @CHRROOFING_UPDATE_YEAR='Y'               
		 END        
	-- ELSE IF(@INTROOFING_UPDATE_YEAR<>0)                                
	--  BEGIN                                                                             
	--  SET @ROOFING_UPDATE_YEAR='N'                                                                                                        
	--  END                                                                                 
	ELSE IF(@ROOFING_UPDATE_YEAR=0)                                                                                   
		 BEGIN                                                                                                         
			 SET @CHRROOFING_UPDATE_YEAR=''                                                                                                        
		 END 
END

DECLARE @UPDATE_YEAR_DP3REPAIR CHAR                                
IF(@INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11482)                                
BEGIN                                
	 IF(@INTYEAR_BUILT < 1940)                                
	 BEGIN                                
		IF(@WIRING_RENOVATION!=8924  and  @PLUMBING_RENOVATION!=8924  and @HEATING_RENOVATION!=8924 and  @ROOFING_RENOVATION!=8924)        
		BEGIN                                
			IF(@CHRWIRING_UPDATE_YEAR='Y' OR @CHRPLUMBING_UPDATE_YEAR='Y' OR @CHRHEATING_UPDATE_YEAR='Y' OR @CHRROOFING_UPDATE_YEAR='Y')                                
				BEGIN                                
					SET @UPDATE_YEAR_DP3REPAIR='Y'                                
				END                                
			ELSE                                
				BEGIN                                
					SET @UPDATE_YEAR_DP3REPAIR='N'                                
				END                                
		END                                
	   	ELSE                                
		BEGIN                                
			SET @UPDATE_YEAR_DP3REPAIR='N'                        
		END                        
	 END                                
	 ELSE                                
		BEGIN                                
			SET @UPDATE_YEAR_DP3REPAIR='N'                                
		END                                
END                                
ELSE                                
	BEGIN                                
		SET @UPDATE_YEAR_DP3REPAIR='N'                
	END 

DECLARE @ROOF_UPDATE_RENOVATION CHAR
SET @ROOF_UPDATE_RENOVATION ='N'
IF(@INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11482)                                
BEGIN                                
	 IF(@INTYEAR_BUILT < 1940)                                
	 BEGIN 
		IF(@WIRING_RENOVATION=8924  OR  @PLUMBING_RENOVATION=8924  OR @HEATING_RENOVATION=8924 OR  @ROOFING_RENOVATION=8924 )        
		BEGIN 
			SET @ROOF_UPDATE_RENOVATION ='Y'
		END 	
		ELSE
		BEGIN 
			SET @ROOF_UPDATE_RENOVATION ='N'
		END
 
	 END
	 ELSE
	 BEGIN 
			SET @ROOF_UPDATE_RENOVATION ='N'
	 END
END   
                  
-- Also check Rating Info Tab if Roof Type Field is Flat Build up - Decline
-- 11481 - DP-3 Replacement
-- 11482 - DP-3 Repair(IN) 
-- 11292 - DP-3 Repair (MI)
-- 11291 - DP-3 Replacement
-- 11458 - DP-3 Premier                             
DECLARE @ROOF_TYPE_DP3_REPAIR CHAR  
DECLARE @ROOFTYPE CHAR                               
IF(@INTPOLICY_TYPE=11482 or @INTPOLICY_TYPE=11292 or @INTPOLICY_TYPE=11481 or @INTPOLICY_TYPE=11291 or @INTPOLICY_TYPE=11458)                                
BEGIN                                
    IF(@INTROOFTYPE=9964)--Flat Buildup                                
	      BEGIN                       
	     	 SET @ROOF_TYPE_DP3_REPAIR='Y'                                
	      END 
	else
		BEGIN                       
			 SET @ROOF_TYPE_DP3_REPAIR='N'                                
		END
END                                
ELSE        
	   BEGIN                                
	 	  SET @ROOF_TYPE_DP3_REPAIR='N'  
	   END        
------------------------------------------------------------------------------------------------- 
/*DECLARE @ROOFTYPE CHAR                      
IF(@INTPOLICY_TYPE=11291 OR @INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11481 OR @INTPOLICY_TYPE=11482 OR @INTPOLICY_TYPE=11458)
BEGIN                      
	IF(@INTROOFTYPE=-1)        
		BEGIN                      
		SET @ROOFTYPE=''         
		END        
	ELSE                      
		BEGIN                                 
		SET @ROOFTYPE='N'                                  
		END        
END  */                             
 -- PRIMARY AND SECONDARY HEAT TYPE                                                                                                
                                                                                            
 DECLARE @PRIMARY_HEATING_TYPE CHAR                                                                                                       
 IF EXISTS (SELECT  PRIMARY_HEAT_TYPE FROM  APP_HOME_RATING_INFO   WITH(NOLOCK)                                                                                         
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                       
      AND DWELLING_ID=@DWELLINGID AND (PRIMARY_HEAT_TYPE=6213 OR PRIMARY_HEAT_TYPE=6212                                                          
      OR PRIMARY_HEAT_TYPE=6224 OR PRIMARY_HEAT_TYPE=6223                          
      OR PRIMARY_HEAT_TYPE=11806 OR PRIMARY_HEAT_TYPE=11807 OR PRIMARY_HEAT_TYPE=11808))                                                         
	   BEGIN                                                                                                 
	 	  SET  @PRIMARY_HEATING_TYPE='Y'                                                                                                      
	   END                                                                                                       
 ELSE                                                                                                      
	   BEGIN                                                                       
		   SET @PRIMARY_HEATING_TYPE='N'                                                                                                     
	   END                                                                    
                                                                                                 
    DECLARE @SECONDARY_HEATING_TYPE  CHAR                                                                                    
    IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  APP_HOME_RATING_INFO  WITH(NOLOCK)                                                                                          
    WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                                       
    AND DWELLING_ID=@DWELLINGID AND (SECONDARY_HEAT_TYPE=6213 OR SECONDARY_HEAT_TYPE=6212                                                          
    OR SECONDARY_HEAT_TYPE=6224 OR SECONDARY_HEAT_TYPE=6223                                                         
    OR SECONDARY_HEAT_TYPE=11806 OR SECONDARY_HEAT_TYPE=11807 OR SECONDARY_HEAT_TYPE=11808))                                                                  
	BEGIN                              
		SET  @SECONDARY_HEATING_TYPE='Y'                                                                                                      
	END                                                                                                       
    ELSE                             
	BEGIN                                       
		SET @SECONDARY_HEATING_TYPE='N'                                                                         
	END                                  
---------------------END HEATING      
-------------------CIRCUIT_BREAKERS-----------------                                
  IF(@CIRCUIT_BREAKERS='10964')                                
	BEGIN                                
		SET @CIRCUIT_BREAKERS='Y'                                
	END                            
    ELSE IF(@CIRCUIT_BREAKERS='0')                    
	BEGIN                    
		SET @CIRCUIT_BREAKERS=''                    
	END                    
-------------------END CIRCUIT_BREAKERS------------                                
                            
------------------                            
--Rating Info tab - if Yes to Building Under Construction (Builder Risk) --Make sure that the Policy Term under Application/Policy Details is showing 1 year  --If not - put a message and allow the user the change the term                            
DECLARE @UNDER_CONSTRUCTION_POLICY_TERM CHAR                            
DECLARE @APP_TERMS NVARCHAR(5)                             
SELECT @APP_TERMS=isnull(APP_TERMS,'') FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                             
                            
IF (@IS_UNDER_CONSTRUCTION='1')                            
BEGIN                            
   IF(@APP_TERMS='6')                            
     BEGIN                            
   	  SET @UNDER_CONSTRUCTION_POLICY_TERM='' --6 MONTHS (MANDATORY DATA)                            
     END                            
   ELSE                            
     BEGIN                            
   	  SET @UNDER_CONSTRUCTION_POLICY_TERM='N' --1 YEAR                            
     END                            
END                            
ELSE                            
  BEGIN                            
 	 SET @UNDER_CONSTRUCTION_POLICY_TERM='N' --1 YEAR                 
  END                            
                             
------------------ 
/*
When you answer 'yes' to dwelling under construction @ rating info tab, the field regarding
'SUPERVISED by licensed contractor' and 'date construction started' should be mandatory. 
When 'no' to 'supervised by licensed contractor', should REFER to underwriter
*/
 declare @UNDER_CONSTRUCTION_POLICYTERM char
 SET @UNDER_CONSTRUCTION_POLICYTERM='N' 
 SET @UNDER_CONSTRUCTION_DATE='N'                          
 IF (@IS_UNDER_CONSTRUCTION='1')                            
 BEGIN 
	IF(@IS_SUPERVISED='0')
	BEGIN 	
		SET @UNDER_CONSTRUCTION_POLICYTERM='Y'
	END
	ELSE IF (@IS_SUPERVISED='')
		BEGIN
			SET @UNDER_CONSTRUCTION_POLICYTERM=''
		END
	ELSE
	BEGIN 	
		SET @UNDER_CONSTRUCTION_POLICYTERM='N'
	END
  
	/*IF (@UNDER_CONSTRUCTION_START_DATE='')
	BEGIN
		SET @UNDER_CONSTRUCTION_DATE=''
	END*/
 END 
----------------                          
--Rating Info tab - Is Dwelling Under Construction                           
--If yes - Risk is Declined                           
DECLARE @DWELL_UNDER_CONSTRUCTION CHAR                          
IF(@INTPOLICY_TYPE=11458)     
BEGIN                          
  IF(@IS_UNDER_CONSTRUCTION='1')                          
   BEGIN          
  	 SET @DWELL_UNDER_CONSTRUCTION='Y'                          
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
------------------                        
-------------------------------------------DP3 REPLACEMENT-----------------                        
/*Dwelling Details tab - Replacement Value can not be greater than the Market Value by 20%          
Is greater than Submit                          
If equal or less move to next condition                   
                        
Rating Info tab - Roof Type                         
If roof type is Flat - Risk is declined                         
If roof in not flat  move to next condition*/                         
declare @DECMARKET_VALUE decimal                             
declare @INTREPLACEMENT_COST decimal                        
declare @MARKET_VALUE_DP3_REPLACEMENT char                         
                        
SELECT  @INTREPLACEMENT_COST=isnull(REPLACEMENT_COST,0),                        
 @DECMARKET_VALUE=isnull(MARKET_VALUE,-1) FROM APP_DWELLINGS_INFO   WITH(NOLOCK)                      
  WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID                
--STEP1:                         
  IF(@DECMARKET_VALUE < = (@INTREPLACEMENT_COST * .80))                                      
	   BEGIN                                      
	  	 SET @MARKET_VALUE_DP3_REPLACEMENT='Y' --REFER                                      
	   END                                      
  ELSE                                      
	   BEGIN                                      
	  	 SET @MARKET_VALUE_DP3_REPLACEMENT='N'                                      
	   END                            
                        
--STEP2                        
DECLARE @ROOF_TYPE_DP3_REPLACEMENT CHAR           
IF (@MARKET_VALUE_DP3_REPLACEMENT='N')                        
BEGIN                        
	 IF(@INTPOLICY_TYPE=11291 or @INTPOLICY_TYPE=11481 or @INTPOLICY_TYPE=11400 )                                
	 BEGIN                                
	         IF(@intROOFTYPE=9964)--Flat Buildup                                
		       BEGIN                                
			       SET @ROOF_TYPE_DP3_REPLACEMENT='Y'                                
		       END                                
	         ELSE                                
		       BEGIN                                
			       SET @ROOF_TYPE_DP3_REPLACEMENT='N'                                
		       END                                
	 END               
	 ELSE                                
	 BEGIN                                
	 	SET @ROOF_TYPE_DP3_REPLACEMENT='N'                                
	 END                         
END                        
ELSE                        
 BEGIN          
	 SET @ROOF_TYPE_DP3_REPLACEMENT='Y'                        
 END  
--====================== Roof type Mandetory 

IF(@INTROOFTYPE=-1 or @INTROOFTYPE=0)                               
	      BEGIN                                
	     	 SET @ROOFTYPE=''                                
	      END                         
-------------------------------------------DP3 REPLACEMENT-----------------                
SELECT                                                 
-- Mandatory                                                                                    
 @HYDRANT_DIST as HYDRANT_DIST,                                                     
 @FIRE_STATION_DIST as FIRE_STATION_DIST,                                                                     
 @IS_UNDER_CONSTRUCTION as IS_UNDER_CONSTRUCTION,                                     
 @PROT_CLASS as PROT_CLASS,                                        
 @CHRWIRING_UPDATE_YEAR as WIRING_UPDATE_YEAR,                                      
 @CHRPLUMBING_UPDATE_YEAR as PLUMBING_UPDATE_YEAR,      
 @CHRHEATING_UPDATE_YEAR as HEATING_UPDATE_YEAR,                                                      
 @CHRROOFING_UPDATE_YEAR  as ROOFING_UPDATE_YEAR,                                                      
 @NO_OF_AMPS as NO_OF_AMPS, 
 @ROOFTYPE as ROOFTYPE ,                                                      
 @CIRCUIT_BREAKERS as CIRCUIT_BREAKERS,               
 @EXTERIOR_CONSTRUCTION as EXTERIOR_CONSTRUCTION,                                                      
 @EXTERIOR_OTHER_DESC as EXTERIOR_OTHER_DESC,                                                      
-- @ROOF_OTHER_DESC as ROOF_OTHER_DESC ,                            
-- @PRIMARY_HEAT_OTHER_DESC as PRIMARY_HEAT_OTHER_DESC,                                           
-- @SECONDARY_HEAT_OTHER_DESC as SECONDARY_HEAT_OTHER_DESC,                                                  
 --rules                                                  
 @NO_OF_FAMILIES as NO_OF_FAMILIES ,                                    
 @IS_RECORD_EXIST as IS_RECORD_EXIST,                                
 @UPDATE_YEAR_DP3REPAIR AS UPDATE_YEAR_DP3REPAIR,                                
 @ROOF_TYPE_DP3_REPAIR AS ROOF_TYPE_DP3_REPAIR,                                
 @PRIMARY_HEATING_TYPE AS PRIMARY_HEATING_TYPE,                             
 @SECONDARY_HEATING_TYPE AS SECONDARY_HEATING_TYPE,                            
 @UNDER_CONSTRUCTION_POLICY_TERM AS UNDER_CONSTRUCTION_POLICY_TERM ,                          
 @DWELL_UNDER_CONSTRUCTION AS DWELL_UNDER_CONSTRUCTION,
 @UNDER_CONSTRUCTION_POLICYTERM as UNDER_CONSTRUCTION_POLICYTERM ,                       
 --DP3 REPLACEMENT                        
 --@ROOF_TYPE_DP3_REPLACEMENT AS ROOF_TYPE_DP3_REPLACEMENT,                      
 
 @ROOF_UPDATE_RENOVATION AS ROOF_UPDATE_RENOVATION  ,
 @UNDER_CONSTRUCTION_DATE  AS   UNDER_CONSTRUCTION_DATE
END










GO

