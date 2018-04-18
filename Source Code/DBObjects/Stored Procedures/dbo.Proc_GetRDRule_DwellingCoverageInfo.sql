IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_DwellingCoverageInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_DwellingCoverageInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                    
Proc Name                : Dbo.Proc_GetRDRule_DwellingCoverageInfo 935,277,1,1,'s','w001'                                                                                                 
Created by               : Ashwani                                                                                                    
Date                     : 12 Dec.,2005                                                    
Purpose                  : To get the dwelling coverage detail for RD rules                                                    
Revison History          :                                                                                                    
Used In                  : Wolverine                                                                                                    
------------------------------------------------------------                                                                                                    
Date     Review By          Comments   
DROP proc dbo.Proc_GetRDRule_DwellingCoverageInfo                                                                                                  
------   ------------       -------------------------*/                                                                                        
CREATE  proc dbo.Proc_GetRDRule_DwellingCoverageInfo                                                    
(                                                                                                    
	@CUSTOMERID    int,                                                                                                    
	@APPID    int,                                                                                                    
	@APPVERSIONID   int,                                                    
	@DWELLINGID int,                                                                
	@DESC varchar(10),        
	@USER varchar(50) ---Added to Check the Wolv User                                                                                              
)                                                                                                    
AS                                                                                                        
BEGIN                                                       
 	 --Mandatory                                                      
	 DECLARE @DECDWELLING_LIMIT DECIMAL                                      
	 DECLARE @DWELLING_LIMIT VARCHAR(100)    -- COVERAGE A                                     
	                                  
	 DECLARE @PERSONAL_LIAB_LIMIT VARCHAR(100)                                                  
	 DECLARE @MED_PAY_EACH_PERSON VARCHAR(100)     
	 DECLARE @BOATID int                                               
                                                    
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES                                                                       
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID)                                                    
 BEGIN                                   
                                
  /*select @DWELLING_LIMIT= isnull(convert(varchar(25),DWELLING_LIMIT),''),                                
           @PERSONAL_LIAB_LIMIT=isnull(convert(varchar(50),PERSONAL_LIAB_LIMIT),''),                                                    
           @MED_PAY_EACH_PERSON=isnull(convert(varchar(50),MED_PAY_EACH_PERSON),'')                                                   
   from APP_DWELLING_COVERAGE                                                    
   where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID */         
                                 
 ---------DWELL     
 SELECT  @DWELLING_LIMIT = (CASE  WHEN M.COV_CODE ='DWELL' THEN isnull(convert(varchar(25),COV_INFO.LIMIT_1),'') END)              
 FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                             
 INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                
	AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                        
	AND (COV_INFO.APP_ID = @APPID)                                                                 
	AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                 
	AND (COV_INFO.dwelling_id = @DWELLINGID) WHERE M.COV_CODE ='DWELL'                                                
 ---END DWELL                                   
                                                
 ---------PL                           
 SELECT  @PERSONAL_LIAB_LIMIT = (CASE  WHEN M.COV_CODE ='PL' THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')  END)                                                
 FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                
 INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                
     AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                                
     AND (COV_INFO.APP_ID = @APPID)                                                 
     AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                 
     AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='PL'                                                
 ------END PL                                                      
                                                
 ---------MEDPM                                                
 SELECT  @MED_PAY_EACH_PERSON = (CASE  WHEN M.COV_CODE ='MEDPM' THEN ISNULL(CONVERT(VARCHAR(50),COV_INFO.LIMIT_1),'')  END)                                                
 FROM APP_DWELLING_SECTION_COVERAGES COV_INFO                                                
 INNER JOIN MNT_COVERAGE M ON M.COV_ID = COV_INFO.COVERAGE_CODE_ID                                                
     AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                                                                
     AND (COV_INFO.APP_ID = @APPID)                                                                 
     AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                                 
     AND (COV_INFO.DWELLING_ID = @DWELLINGID) WHERE M.COV_CODE ='MEDPM'                                                
 ------END MEDPM                                                                                    
  END                                                    
 ELSE                                                    
 BEGIN                                                     
  SET @DWELLING_LIMIT =''                                                    
  SET @PERSONAL_LIAB_LIMIT =''                                                    
  SET @MED_PAY_EACH_PERSON =''                                          
 END                                             
---------------------------                                          
 IF(@PERSONAL_LIAB_LIMIT='')                                          
  BEGIN                                           
  SET @PERSONAL_LIAB_LIMIT='N'                                          
  END                                           
--------------------------                                          
 IF(@MED_PAY_EACH_PERSON='')                                          
  BEGIN                                           
  SET @MED_PAY_EACH_PERSON='N'                                          
  END                
   
--------------------------------------------------------------------------------------------------                                    
-- DP-2, DP-3 - Both States Covg. A should be at least 80% of Replacement cost.      
-- Covg. A should not exceed 100% of Replacement cost                                    
-- DP-2  11289,11479,DP-3  11291,11481                                     
 DECLARE @INTPOLICY_TYPE INT                                                  
                                     
  SELECT @INTPOLICY_TYPE=ISNULL(POLICY_TYPE,0)                                                  
  FROM APP_LIST                                   
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                     
 -- Cov A  --Modified on 3 July 2006                                
---------DWELL                                                
 Select  @DECDWELLING_LIMIT = (CASE  WHEN M.COV_CODE ='DWELL' THEN ISNULL(COV_INFO.LIMIT_1,-1) end)                                                
 from APP_DWELLING_SECTION_COVERAGES COV_INFO                                 
 INNER join mnt_coverage M on m.cov_id = cov_info.coverage_code_id                                                
     AND (COV_INFO.CUSTOMER_ID = @CUSTOMERID)                               
     AND (COV_INFO.APP_ID = @APPID)                                
     AND (COV_INFO.APP_VERSION_ID = @APPVERSIONID)                                                            
     AND (COV_INFO.dwelling_id = @DWELLINGID)       
 where M.COV_CODE ='DWELL'                                                
---END DWELL                       
  DECLARE @REPLACEMENT_COST DECIMAL -- REPLACEMENT COST        
  SELECT @REPLACEMENT_COST=ISNULL(CONVERT(int,REPLACEMENT_COST),0)                                                  
  FROM APP_DWELLINGS_INFO                      
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                                       
                    
--MARKET VALUE :                     
  DECLARE @MARKET_VALUE DECIMAL -- MARKET VALUE       
  SELECT @MARKET_VALUE=ISNULL(MARKET_VALUE,0)                                                  
  FROM APP_DWELLINGS_INFO                                                   
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                      
--                    
                                     
DECLARE @MIN_COVA_REPCOST CHAR                                    
DECLARE @MINVAL_REPLACEMENT_COST DECIMAL                                  
DECLARE @MAXVAL_REPLACEMENT_COST CHAR                                     
---Removed Mandatory Rule                                    
/* set @MINVAL_REPLACEMENT_COST = (0.8 * @REPLACEMENT_COST)                                    
                                    
IF(@INTPOLICY_TYPE=11289 OR @INTPOLICY_TYPE=11479 OR @INTPOLICY_TYPE=11291 OR @INTPOLICY_TYPE=11481)                                    
BEGIN                                     
IF(@DECDWELLING_LIMIT<@MINVAL_REPLACEMENT_COST )                                     
BEGIN                                
SET @MIN_COVA_REPCOST='Y'                                    
END                                     
ELSE                                     
BEGIN                                     
SET @MIN_COVA_REPCOST='N'                                    
END                                     
END                                     
ELSE                                    
BEGIN                                     
SET @MIN_COVA_REPCOST='N'                                    
END                                     
                            
IF(@MIN_COVA_REPCOST='Y')                                    
BEGIN                                     
SET @MIN_COVA_REPCOST=''        
end   */                                 
---------------------------------------------                                    
-- Covg. A should not exceed 100% of Replacement cost                                    
IF(@INTPOLICY_TYPE=11289 OR @INTPOLICY_TYPE=11479 OR @INTPOLICY_TYPE=11291 OR @INTPOLICY_TYPE=11481)                                    
BEGIN                                     
   IF(@DECDWELLING_LIMIT>@REPLACEMENT_COST )                                     
   BEGIN                                     
   SET @MAXVAL_REPLACEMENT_COST='Y'                                    
   END                                     
  ELSE                                     
   BEGIN                                     
   SET @MAXVAL_REPLACEMENT_COST='N'                                    
   END                                
END                                     
ELSE                                    
 BEGIN                                     
 SET @MAXVAL_REPLACEMENT_COST='N'                                    
 END                                     
                                    
IF(@MAXVAL_REPLACEMENT_COST='Y')                                    
 BEGIN                                     
 SET @MAXVAL_REPLACEMENT_COST=''                                    
 END                                    
---------------------------------------------------------------------------------------------                                    
--DP-2 Repair Cost, Both States                                    
--Covg. A could be Greater than or Equal to Min. covg. limit And not more than Replacement cost.                                    
--(i.e. Between Min. covg. Limit and Replacement cost)                                    
--DP-2 Repair Cost 11290,11480                                    
 --<REMOVED RULE :13 July 2006>                                   
/*declare @MIN_COV_REPCOST_RC  char                                    
                                    
IF(@INTPOLICY_TYPE=11290 OR @INTPOLICY_TYPE=11480)                                    
BEGIN                                     
IF(@DECDWELLING_LIMIT<10000.00)                            
BEGIN                                     
SET @MIN_COV_REPCOST_RC='Y'                                    
END                                     
ELSE                                     
BEGIN                                     
SET @MIN_COV_REPCOST_RC='N'                                    
END                                     
END                                     
ELSE                                    
BEGIN                                     
SET @MIN_COV_REPCOST_RC='N'                                    
END                                     
                            
IF(@MIN_COV_REPCOST_RC='Y')                                    
BEGIN                                     
SET @MIN_COV_REPCOST_RC=''                                    
end */                                   
-------------------------------------------------------------                                    
/*Removed mandatory check as Replacement Cost is removed from the REPAIR COST*/                    
/*declare @MAX_COV_REPCOST_RC  char                                    
                                    
IF(@INTPOLICY_TYPE=11290 OR @INTPOLICY_TYPE=11480)                                    
BEGIN                                     
IF(@DECDWELLING_LIMIT>@REPLACEMENT_COST)                                     
BEGIN                                     
SET @MAX_COV_REPCOST_RC='Y'                                    
END                                     
ELSE                                     
BEGIN                                     
SET @MAX_COV_REPCOST_RC='N'                         
END                                     
END             
ELSE             
BEGIN           
SET @MAX_COV_REPCOST_RC='N'                                    
END                                     
                            
IF(@MAX_COV_REPCOST_RC='Y')                                    
BEGIN                                     
SET @MAX_COV_REPCOST_RC=''       
end */                                   
-----------------------------------------------------------------------------------------------                                    
--DP-3 Repair Cost, Both States                                    
--Covg. A could be Greater than or Equal to Min. covg. limit               
--And could be more than Replacement cost.(i.e From Min. Covg. Limit to any amount)                                    
--DP-3 Repair Cost 11292,11482 
DECLARE @APP_EFFECTIVE_DATE VARCHAR(20),
	@EFFECTIVE_DATE VARCHAR(20)
 SET @EFFECTIVE_DATE='01/01/2008'
 SELECT @APP_EFFECTIVE_DATE=ISNULL(DATEDIFF(DAY,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101),@EFFECTIVE_DATE),'') FROM APP_LIST  WITH(NOLOCK)                                                                     
 	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                   
                                     
DECLARE @MIN_COVA_DP3RC CHAR 
SET @MIN_COVA_DP3RC='N'                                   
/* Commited By manoj AS PER DISCUSSION WITH Praveen kasana on 18 - Feb -2008
IF(@APP_EFFECTIVE_DATE <= 0) 
BEGIN                                   
	IF(@INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11482)                                    
	BEGIN                                     
		IF(75000.00>@DECDWELLING_LIMIT OR 75000 >@DECDWELLING_LIMIT)                             
			   BEGIN                                     
			   	SET @MIN_COVA_DP3RC='Y'                                    
			   END                                     
		ELSE                                     
			   BEGIN                                     
				   SET @MIN_COVA_DP3RC='N'                    
			   END                                     
	END                                     
	ELSE                                    
	BEGIN                                     
		 SET @MIN_COVA_DP3RC='N'                                    
	END 
END
ELSE                                    
	BEGIN                                     
		 SET @MIN_COVA_DP3RC='N'                                    
	END 
*/                                    
                                    
IF(@MIN_COVA_DP3RC='Y')                                    
  BEGIN                                     
  SET @MIN_COVA_DP3RC=''                                    
  END                                    
---------------------------------------------------------------------------------------------                                            
--DP-3 Premier (11458), Michigan (22)                                    
--Covg. A should be 100% of Replacement cost 
 DECLARE @COVA_REPCOST_DP3P CHAR                                     
                                     
 DECLARE @STATE_ID VARCHAR(10)                                    
 SELECT @STATE_ID=ISNULL(CONVERT(VARCHAR(10),STATE_ID),0)                                    
 FROM APP_LIST                                     
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                    
 IF(@STATE_ID='22'  AND @INTPOLICY_TYPE= 11458)                                   
 BEGIN                                     
	 IF(@DECDWELLING_LIMIT<>@REPLACEMENT_COST)                                    
		BEGIN                                     
			SET @COVA_REPCOST_DP3P='Y'                                    
		END                                     
	 ELSE                                    
		BEGIN                                     
			SET @COVA_REPCOST_DP3P='N'                                    
		END                                     
 END                    
 ELSE 
 BEGIN                                     
	SET @COVA_REPCOST_DP3P='N'                                    
 END                                    
 ---                                    
/*if(@COVA_REPCOST_DP3P='Y')                                    
begin                                     
 set @COVA_REPCOST_DP3P=''                                    
end    */                              
                              
----------    
--For Dp-2 and DP-3 Repair Programs :Coverage A is not equal or greater than                               
--Market Value/Repair Cost on the Dwelling Details then Submit                              
--11290,11292,11480,11482                              
DECLARE @COVA_REAPAIR_COST CHAR                              
IF(@INTPOLICY_TYPE=11290 OR @INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11480 OR @INTPOLICY_TYPE=11482)                              
BEGIN                              
	 IF(@DECDWELLING_LIMIT<@MARKET_VALUE)        
		BEGIN                              
			SET @COVA_REAPAIR_COST='Y'                              
		END                              
	 ELSE                              
		BEGIN                              
			SET @COVA_REAPAIR_COST='N'                              
		END                              
END                              
ELSE                              
BEGIN                              
	 SET @COVA_REAPAIR_COST='N'                              
END                           
                              
--if Coverage A Building is greater than $150,000                               
--Submit to underwriters                              
DECLARE @COVA_BUILDING CHAR 
                             
	IF(@INTPOLICY_TYPE=11290 OR @INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11480 OR @INTPOLICY_TYPE=11482)                              
	BEGIN                              
	 	IF(@DECDWELLING_LIMIT>150000)                              
		BEGIN                              
			SET @COVA_BUILDING='Y'                              
		END                              
	        ELSE                              
		BEGIN                              
		 	SET @COVA_BUILDING='N'                              
		END                              
	                               
	END                              
	ELSE                              
	BEGIN                              
		 SET @COVA_BUILDING='N'                              
	END
                             
--If Policy type is DP2-Repair or DP-2 Replacement                               
--Coverage A Building is less than $30,00 -                               
--Put a message that the minimum coverages is $30,000 in order to be able to submit                              
DECLARE @MIN_COVA_BUILDING CHAR  
IF(@APP_EFFECTIVE_DATE <= 0) 
BEGIN                            
	IF(@INTPOLICY_TYPE=11290 OR  @INTPOLICY_TYPE=11480                           
	 OR @INTPOLICY_TYPE=11289 OR @INTPOLICY_TYPE=11479)                              
	BEGIN                              
		 IF(@DECDWELLING_LIMIT<30000)                              
			BEGIN                              
				SET @MIN_COVA_BUILDING='Y'                              
			END                              
		 ELSE                              
			BEGIN                              
				SET @MIN_COVA_BUILDING='N'                              
			END                              
	END                              
	ELSE                              
	BEGIN                              
	 	SET @MIN_COVA_BUILDING='N'                              
	END
END
ELSE                              
	BEGIN                              
	 	SET @MIN_COVA_BUILDING='N'                              
	END                              
                              
----------End DP2 DP3 REPAIR COST                       
                  
                  
-------  
--If Policy type is DP3-Repair or DP-3 Replacement or DP-3 Premier         
-- if Coverage A Building is less than $75,000 - Put a message that the minimum coverages is $75,000 in order to be able to submit                         
--11292,11482,11291,11481,11458                        
DECLARE @COVA_DP3 CHAR 
IF(@APP_EFFECTIVE_DATE <= 0) 
BEGIN                       
	IF(@INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11482 OR                        
	   @INTPOLICY_TYPE=11291 OR @INTPOLICY_TYPE=11481 OR @INTPOLICY_TYPE=11458)                        
	BEGIN                        
		IF(@DECDWELLING_LIMIT<75000)                        
			BEGIN           
				SET @COVA_DP3='Y'               
			END                        
		ELSE                        
			BEGIN                        
				SET @COVA_DP3='N'                        
			END                        
	END                        
	ELSE                        
	BEGIN                        
	 	SET @COVA_DP3='N'                        
	END
END                        
	ELSE                        
	BEGIN                        
	 	SET @COVA_DP3='N'                        
	END                         
                        
--If Policy Type is DP-2 Replacement(11289,11479) or DP-3 Replacement(11291,11481)                         
--If Coverage A - Building is greater than $250,000                        
--Submit to underwriters                          
                        
DECLARE @COVA_DP2DP3 CHAR                        
IF(@INTPOLICY_TYPE=11289 OR @INTPOLICY_TYPE=11479 OR                        
   @INTPOLICY_TYPE=11291 OR @INTPOLICY_TYPE=11481 OR @INTPOLICY_TYPE=11481)                        
BEGIN                        
	IF(@DECDWELLING_LIMIT>250000)                        
		BEGIN                        
			SET @COVA_DP2DP3='Y'                        
		END                        
	ELSE                        
	BEGIN                        
		SET @COVA_DP2DP3='N'                        
	END                        
END                        
ELSE                        
BEGIN                        
 	SET @COVA_DP2DP3='N'          
END                                
                              
-------------------------------------START DP_3 PREMIER PROGRAM--------------------------                              
--Coverage A Building must be equal or greater than the Replacement Field on the Dwelling Details tab                               
--If not equal or greater - submit   
---print @DECDWELLING_LIMIT       
--print @REPLACEMENT_COST                         
DECLARE @COVA_DP3_PREMIUM char                              
IF(@INTPOLICY_TYPE=11458)                              
BEGIN                              
	  IF(@DECDWELLING_LIMIT < @REPLACEMENT_COST)                              
		BEGIN                              
			SET @COVA_DP3_PREMIUM='Y'                              
		END                              
	  ELSE                              
		BEGIN                              
			SET @COVA_DP3_PREMIUM='N'                              
		END                    
END                              
ELSE                              
BEGIN                              
	SET @COVA_DP3_PREMIUM='N'                              
END                    
                
/*Rating Info tab - Roof Type                   
If roof type is Flat - Risk is declined                   
If roof in not flat  move to next condition                   
                  
Coverage/Limits Tab if Coverage A = Building is less than $75,000                   
If Less than $75,000 decline - Not eligible for DP- Premier                   
If $75,000 or greater move to next condition                  
                  
Coverage/Limits Tab if Coverage A = Building is greater than $300,000                  
If greater Submit                    
If under $300,000 then go to next condition*/                  
               
DECLARE @ROOFTYPE INT        
DECLARE @ROOF_TYPE_DP3_REPAIR CHAR                    
                  
SELECT @ROOFTYPE=ISNULL(ROOF_TYPE,0) FROM APP_HOME_RATING_INFO                                                                            
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID=@DWELLINGID                   
                      
IF(@INTPOLICY_TYPE=11458)                        
BEGIN                        
	 IF(@ROOFTYPE=9964)--Flat Buildup                        
		BEGIN                        
			SET @ROOF_TYPE_DP3_REPAIR='Y'                        
		END                        
	 ELSE                        
		BEGIN                        
			SET @ROOF_TYPE_DP3_REPAIR='N'                        
		END                   
END                        
ELSE                        
BEGIN                        
 	SET @ROOF_TYPE_DP3_REPAIR='N'                        
END                              
--Coverage/Limits Tab if Coverage A = Building is less than $75,000                   
--If Less than $75,000 decline - Not eligible for DP- Premier                   
--If $75,000 or greater move to next condition                  
DECLARE @COVA_DP3P CHAR                  
IF(@INTPOLICY_TYPE=11458)                        
BEGIN                    
		  IF(@ROOF_TYPE_DP3_REPAIR='N')                  
		  BEGIN                  
			IF(@DECDWELLING_LIMIT<75000)                  
				    BEGIN                  
				  	  SET @COVA_DP3='Y' --dECLINE                  
				    END                  
			ELSE                  
				    BEGIN                  
					    SET @COVA_DP3='N'                   
				    END                  
		  END                  
		  ELSE                  
		  BEGIN                  
		 	 SET @COVA_DP3='N'                  
		  END        
		 ----                  
		 --Coverage/Limits Tab if Coverage A = Building is greater than $300,000                  
		 --If greater Submit If under $300,000                   
		  IF(@COVA_DP3='N')                  
		  BEGIN                  
			   IF(@DECDWELLING_LIMIT>300000)                  
				   BEGIN                  
				  	 SET @COVA_DP3P='Y'               
				   END                  
			   ELSE                  
				   BEGIN                  
				  	 SET @COVA_DP3P='N'                  
				   END                  
		  END                  
		  ELSE                  
		  BEGIN                  
		 	  SET  @COVA_DP3P='N'                  
		  END        
END                  
ELSE                  
BEGIN                  
SET @COVA_DP3='N'             
SET @COVA_DP3P='N'                  
END                  
                     
-------------------------------END START DP_3 PREMIER PROGRAM-------       
-------MODULAR/MANUFACTURE HOME :7 July 2006                    
 declare @MODULAR_MANUFACTURED_HOME nchar(1)                    
 declare @BUILT_ON_CONTINUOUS_FOUNDATION  nchar(1)                    
                    
SELECT @MODULAR_MANUFACTURED_HOME=ISNULL(MODULAR_MANUFACTURED_HOME,''),@BUILT_ON_CONTINUOUS_FOUNDATION=ISNULL(BUILT_ON_CONTINUOUS_FOUNDATION,'')                    
FROM APP_HOME_OWNER_GEN_INFO  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                    
                    
   IF(@MODULAR_MANUFACTURED_HOME='1')                                                                  
	    BEGIN                                                                                     
		    SET @MODULAR_MANUFACTURED_HOME='Y'                                                                     
	    END                       
   ELSE IF(@MODULAR_MANUFACTURED_HOME='0')                                                                                    
	    BEGIN                                
		    SET @MODULAR_MANUFACTURED_HOME='N'                    
	  END                     
                    
   IF(@BUILT_ON_CONTINUOUS_FOUNDATION='1')                                                                             
	    BEGIN                                                                                     
		    SET @BUILT_ON_CONTINUOUS_FOUNDATION='Y'                                                                     
	    END                                                  
   ELSE IF(@BUILT_ON_CONTINUOUS_FOUNDATION='0')                                                                                    
	    BEGIN                                                                                 
		    SET @BUILT_ON_CONTINUOUS_FOUNDATION='N'                                    
	    END                     
                    
DECLARE @MODULAR_MANU_HOME CHAR                    
IF(@MODULAR_MANUFACTURED_HOME='Y')                    
BEGIN                    
 IF(@BUILT_ON_CONTINUOUS_FOUNDATION='N')                     
	 BEGIN                    
		SET @MODULAR_MANU_HOME='Y' --RISK DECLINE                    
	 END                    
  ELSE                    
         BEGIN                     
        	 SET @MODULAR_MANU_HOME='N'                    
         END                     
END                    
ELSE                    
  BEGIN                    
  SET @MODULAR_MANU_HOME='N'                    
  END                    
                 
DECLARE @COVA_MODULAR_MANU_HOME CHAR                    
      
IF(@MODULAR_MANUFACTURED_HOME='Y')      
BEGIN      
	     IF(@BUILT_ON_CONTINUOUS_FOUNDATION='Y')      
			 BEGIN      
				  IF(@DECDWELLING_LIMIT < 75000)                    
					   BEGIN                    
						   SET @COVA_MODULAR_MANU_HOME='Y' --DECLINE                    
					   END                    
				   ELSE             
					   BEGIN                    
						   SET @COVA_MODULAR_MANU_HOME='N'                    
					   END              
			 END      
	     ELSE       
		 BEGIN      
			 SET @COVA_MODULAR_MANU_HOME='N'      
		 END      
END      
ELSE      
BEGIN      
	 SET @COVA_MODULAR_MANU_HOME='N'      
END      
-------------------------------------------END                     
                    
-----Policy type is DP-2 Repair or DP-3 Repair                     
--If Coverage A Building field is not equal to or greater than Market Value Field n the Dwelling Details tab                             
DECLARE @MAX_COV_DP2DP3_REPAIR  CHAR                                    
                                    
IF(@INTPOLICY_TYPE=11290 OR @INTPOLICY_TYPE=11292 OR @INTPOLICY_TYPE=11480 OR @INTPOLICY_TYPE=11482)                                    
BEGIN                                     
  IF(@DECDWELLING_LIMIT>@MARKET_VALUE)                                     
	   BEGIN                                     
		   SET @MAX_COV_DP2DP3_REPAIR='Y'                                    
	   END          
  ELSE                      
	   BEGIN                    
		   SET @MAX_COV_DP2DP3_REPAIR='N'                    
	   END                    
                     
END                    
-------------END Policy type is DP-2 Repair or DP-3 Repair -------                     
                
                                  
---------START DP3 REPLACEMENT COST-------------                
--Coverage A Building must be equal or greater than the Replacement Field on the Dwelling Details tab                               
--If not equal or greater - submit                               
DECLARE @COVA_DP3_REPLACEMENT CHAR                              
IF(@INTPOLICY_TYPE=11291 or @INTPOLICY_TYPE=11481)                              
BEGIN                              
	IF(@DECDWELLING_LIMIT < @REPLACEMENT_COST )                              
	BEGIN                              
	SET @COVA_DP3_REPLACEMENT='Y'                              
	END   
	ELSE                              
	BEGIN                              
	SET @COVA_DP3_REPLACEMENT='N'           
	END                              
END                              
ELSE                              
	BEGIN                              
	SET @COVA_DP3_REPLACEMENT='N'                              
	END     
--print @COVA_DP3_REPLACEMENT                
---------END DP3 REPLACEMENT COST---------------                                    
----======================START=========GRANDFTAHERED COVERGAES==========================        
DECLARE @APP_EFF_DATE datetime                                  
SELECT @APP_EFF_DATE = APP_EFFECTIVE_DATE                                                                
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                     
                                
                            
SELECT MNTC.COV_DES as COVERAGE_DES          
from APP_DWELLING_SECTION_COVERAGES AVC                                  
INNER JOIN MNT_COVERAGE MNTC on MNTC.COV_ID = AVC.COVERAGE_CODE_ID                           
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID  AND DWELLING_ID=@DWELLINGID                                                 
AND NOT( @APP_EFF_DATE  BETWEEN MNTC.EFFECTIVE_FROM_DATE                                  
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630'))              
        
--Check for WOL and AGENCY USER          
--W001 -CARRIERSYSTEMID --W001 TO BE CHECKED WITH XML(DYNAMICALLY)            
DECLARE @WOLVERINE_USER VARCHAR(50)          
    
	IF(@USER='w001')           
	BEGIN          
	SET @WOLVERINE_USER='Y'  --WOL USER          
	END          
	ELSE          
	BEGIN          
	SET @WOLVERINE_USER='N'  --AGENCY USER          
	END          
----=======================END========GRANDFTAHERED COVERGAES==========================      
    
-----------------------------------------GRANDFATHER LIMIT -------------------------------------------------------------      
SELECT MNT.COV_DES AS LIMIT_DESCRIPTION,      
CASE MNT.LIMIT_TYPE      
 WHEN 2 THEN ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')       
 + '/'  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
 ELSE      
 ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')       
 + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
END AS LIMIT,     
AVC.COVERAGE_CODE_ID     
FROM APP_DWELLING_SECTION_COVERAGES AVC                                      
INNER JOIN MNT_COVERAGE_RANGES MNTC       
ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID     
AND  AVC.LIMIT_ID= MNTC.LIMIT_DEDUC_ID                                  
INNER JOIN MNT_COVERAGE MNT       
ON AVC.COVERAGE_CODE_ID = MNT.COV_ID       
WHERE CUSTOMER_ID=@CUSTOMERID     
and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID       
AND DWELLING_ID = @DWELLINGID    
AND LIMIT_DEDUC_TYPE='LIMIT'      
AND NOT(      
@APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')      
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')      
)        
    
    
----------------------------------------- END GRANFATHER LIMIT ---------------------------------------------------------                       
    
--========================================= GRANDFATHER Additional ====================================================    
    
SELECT MNT.COV_DES AS DEDUCTIBLE_DESCRIPTION,    
 CASE MNT.DEDUCTIBLE_TYPE    
 WHEN 2 THEN     
  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')    
   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')     
  + '/'    
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')     
  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')     
ELSE    
  ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')    
   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')     
  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')     
  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')     
 END AS DEDUCTIBLE,    
 AVC.COVERAGE_CODE_ID      
FROM APP_DWELLING_SECTION_COVERAGES AVC                                    
INNER JOIN MNT_COVERAGE_RANGES MNTC     
ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID      
AND  AVC.DEDUC_ID= MNTC.LIMIT_DEDUC_ID                                
INNER JOIN MNT_COVERAGE MNT     
ON AVC.COVERAGE_CODE_ID = MNT.COV_ID     
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID      
AND DWELLING_ID=@DWELLINGID AND LIMIT_DEDUC_TYPE='DEDUCT'    
AND NOT(    
 @APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')    
 AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')    
 )    
--========================================= END GRANDFATHER DEDUCTIBLE ===============================================    
         
                  
---- Deductible     
SELECT MNT.COV_DES AS ADDDEDUCTIBLE_DESCRIPTION,      
CASE MNT.LIMIT_TYPE      
 WHEN 2 THEN ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')  + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')       
 + '/'  + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'')   + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
 ELSE      
 ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT),'')+ ISNULL(MNTC.LIMIT_DEDUC_AMOUNT_TEXT,'')       
 + ISNULL(CONVERT(VARCHAR,MNTC.LIMIT_DEDUC_AMOUNT1),'') + ISNULL(MNTC.LIMIT_DEDUC_AMOUNT1_TEXT,'')       
END AS LIMIT,     
AVC.COVERAGE_CODE_ID     
FROM APP_DWELLING_SECTION_COVERAGES AVC                                      
INNER JOIN MNT_COVERAGE_RANGES MNTC       
ON MNTC.COV_ID = AVC.COVERAGE_CODE_ID     
AND  AVC.ADDDEDUCTIBLE_ID= MNTC.LIMIT_DEDUC_ID                                  
INNER JOIN MNT_COVERAGE MNT       
ON AVC.COVERAGE_CODE_ID = MNT.COV_ID       
WHERE CUSTOMER_ID=@CUSTOMERID     
and APP_ID=@APPID and APP_VERSION_ID =@APPVERSIONID       
AND DWELLING_ID=@DWELLINGID AND LIMIT_DEDUC_TYPE= 'ADDDED'      
AND NOT(      
@APP_EFF_DATE  BETWEEN ISNULL(MNTC.EFFECTIVE_FROM_DATE,'1950-1-1')      
AND ISNULL(MNTC.EFFECTIVE_TO_DATE,'3000-12-31')      
) 
                         
AND LIMIT_DEDUC_TYPE= 'NOT REQUIRED NOW'   -- ADDED BY PRAVESH ON 25 APRIL AS THIS RULE NOT REQUIRED NOW
---------------------------------------------------------------------------------------------                                            
                                 
SELECT                                                    
	-- Mandatory                                                    
	ISNULL(@DWELLING_LIMIT,'') as  DWELLING_LIMIT,                                                    
	@PERSONAL_LIAB_LIMIT as PERSONAL_LIAB_LIMIT,  --(Coverage E - Personal Liability Each Occurrence)                                      
	@MED_PAY_EACH_PERSON as MED_PAY_EACH_PERSON,   --(Coverage F - Medical Payment Each Person)                                          
	--@MIN_COVA_REPCOST as MIN_COVA_REPCOST, --  DP-2,DP-3 covgerage A must be at least 80% of replacement cost.                                     
	@MAXVAL_REPLACEMENT_COST as  MAXVAL_REPLACEMENT_COST, --DP-2,DP-3 covgerage A can not be more than replacement cost                                    
	--@MAX_COV_REPCOST_RC as MAX_COV_REPCOST_RC, --DP-2 Repair Cost coverage A can not be more than full replacement cost.                                    
	--@MIN_COV_REPCOST_RC as MIN_COV_REPCOST_RC, -- DP-2 Repair Cost requires minimum coverage A of $10000.                                    
	@MIN_COVA_DP3RC as MIN_COVA_DP3RC, -- DP-3 Repair Cost requires minimum coverage A of $75000.                                    
	@COVA_REPCOST_DP3P as COVA_REPCOST_DP3P,-- DP-3 Premier,Michigan requires residence to be insured to 100% of replacement cost.                   
	--------------                              
	@COVA_REAPAIR_COST as COVA_REAPAIR_COST,                       
	@COVA_BUILDING as COVA_BUILDING,                              
	@MIN_COVA_BUILDING as MIN_COVA_BUILDING,                              
	@COVA_DP3_PREMIUM as COVA_DP3_PREMIUM ,                        
	----                        
	@COVA_DP3 as COVA_DP3,                  
	@COVA_DP3P AS COVA_DP3P,                         
	@COVA_DP2DP3 as COVA_DP2DP3,                    
	--MOD HOME COVERAGE A                    
	@COVA_MODULAR_MANU_HOME as COVA_MODULAR_MANU_HOME,                     
	@MAX_COV_DP2DP3_REPAIR as MAX_COV_DP2DP3_REPAIR, --Policy type is DP-2 Repair or DP-3 Repair                 
	@COVA_DP3_REPLACEMENT as COVA_DP3_REPLACEMENT, --Policy DP3 Replacement   
	@WOLVERINE_USER as WOLVERINE_USER                   
                   
END  







GO

