IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_DwellingInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_DwellingInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                                                    
Proc Name                : Dbo.Proc_GetRDRule_DwellingInfo_Pol                                                                                                  
Created by               : Ashwani                                                                                                    
Date                     : 02 Mar. 2006                                             
Purpose                  : To get the Dwelling detail for RD policy rules                                                    
Revison History          :                                                                                                    
Used In                  : Wolverine                                                                                                    
------------------------------------------------------------                                                                                                    
Date     Review By          Comments                                                                                                    
------   ------------       -------------------------*/                                                                                                    
--drop proc  dbo.Proc_GetRDRule_DwellingInfo_Pol             
CREATE proc dbo.Proc_GetRDRule_DwellingInfo_Pol                                                    
(                                                                                                    
	@CUSTOMER_ID    int,                                                                                                    
	@POLICY_ID    int,                                                                                                    
	@POLICY_VERSION_ID   int,                                                    
	@DWELLING_ID int                                                                
)                                                                                                    
AS                                                                                                        
BEGIN                                                       
 -- Mandatory                                                      
	 DECLARE @INTDWELLING_NUMBER  INT                                                    
	 DECLARE @DWELLING_NUMBER  INT                                                  
	 DECLARE @INTLOCATION_ID INT                                                    
	 DECLARE @LOCATION_ID CHAR                                                  
	 DECLARE @INTYEAR_BUILT INT                           
	 DECLARE @YEAR_BUILT CHAR                          
	 DECLARE @INTREPLACEMENT_COST DECIMAL                                                    
	 DECLARE @REPLACEMENT_COST CHAR                                              
	                                
	 DECLARE @INTBUILDING_TYPE INT                                          
	 DECLARE @BUILDING_TYPE CHAR                                         
	 --RULES                            
	 DECLARE @INTOCCUPANCY INT                               
	 DECLARE @OCCUPANCY CHAR                              
	 DECLARE @DECMARKET_VALUE DECIMAL                             
	 DECLARE @MARKET_VALUE CHAR      
                                                
                                                     
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_DWELLINGS_INFO  WITH(NOLOCK)                                                                     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID)                                                    
 BEGIN                       
	SELECT   @INTDWELLING_NUMBER=ISNULL(DWELLING_NUMBER,0),@INTLOCATION_ID=ISNULL(LOCATION_ID,0),@INTYEAR_BUILT=ISNULL(YEAR_BUILT,0),                                          
		 @INTREPLACEMENT_COST=ISNULL(REPLACEMENT_COST,0),@INTOCCUPANCY=ISNULL(OCCUPANCY,-1),@DECMARKET_VALUE=ISNULL(MARKET_VALUE,-1)                                     
		,@INTBUILDING_TYPE = ISNULL(BUILDING_TYPE,-1)
	FROM POL_DWELLINGS_INFO  WITH(NOLOCK)             
 	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID                                                  
 END                                                    
 ELSE                                                   
 BEGIN                                                     
	SET @DWELLING_NUMBER =''                                                    
	SET @LOCATION_ID =''                   
	SET @YEAR_BUILT =''                                                    
	SET @REPLACEMENT_COST =''                           
	SET @MARKET_VALUE=''                               
	SET @OCCUPANCY=''   
	SET @BUILDING_TYPE=''                       
 END                                       
-------------------------------          
 IF(@INTOCCUPANCY=8962)                                                
	 BEGIN                                                 
	 SET @OCCUPANCY='Y'                                                
	 END                                                 
 ELSE IF(@INTOCCUPANCY=8961 OR @INTOCCUPANCY=0 OR @INTOCCUPANCY=-1)                                                 
	 BEGIN                                                 
	 SET @OCCUPANCY=''                                                
	 END          
 ELSE IF(@INTOCCUPANCY<>8961)                                                
	 BEGIN                                                 
	 SET @OCCUPANCY='N'                                                
	 END                                                 
                    
-------------------------------                       
 /*
	IF(@DECMARKET_VALUE=-1)                                  
	BEGIN                               
	SET @MARKET_VALUE=''                              
	END                               
	ELSE                              
	BEGIN                               
	SET @MARKET_VALUE='N'                              
	END  
*/  
------------------------ 
IF(@INTYEAR_BUILT<1940)
BEGIN                                                              
	 IF(@DECMARKET_VALUE=-1)                                                      
		 BEGIN                                                   
			 SET @MARKET_VALUE=''                         
		 END                                                   
	 ELSE                                                  
		 BEGIN                                                   
			 SET @MARKET_VALUE='N'                                                  
		 END 
END                              
                         
-------------------------------                  
 IF(@INTLOCATION_ID=0)                                    
  BEGIN                                 
  SET @LOCATION_ID=''                                
  END                                 
 ELSE                                
  BEGIN                                 
  SET @LOCATION_ID='N'                                
  END                                 
--=============--------------
IF(@INTBUILDING_TYPE=0 OR @INTBUILDING_TYPE=-1 )
BEGIN 
	SET @BUILDING_TYPE=''
END                              
----------------------------------------------------------------------------------------                                                     
/* IF(@INTYEAR_BUILT=0)                                    
  BEGIN                                 
  SET @YEAR_BUILT=''                                
  END                                 
 ELSE                                
  BEGIN     
  SET @YEAR_BUILT='N'                                
  END   */                          
------------------------------------------------------------------------------------------                          
-- Indiana Only                           
 DECLARE  @STATE_ID INT                           
 DECLARE @POLICY_TYPE INT                        
                        
 SELECT @STATE_ID=STATE_ID,@POLICY_TYPE=ISNULL(POLICY_TYPE,0)FROM  POL_CUSTOMER_POLICY_LIST   with(nolock)                                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND IS_ACTIVE='Y'          
 DECLARE @NUM_CUSTOMER_INSURANCE_SCORE NUMERIC                             
 DECLARE @CUSTOMER_INSURANCE_SCORE CHAR                            
                             
 SELECT @NUM_CUSTOMER_INSURANCE_SCORE=ISNULL(CUSTOMER_INSURANCE_SCORE,-1) FROM CLT_CUSTOMER_LIST     WITH(NOLOCK)                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE='Y'           
 DECLARE @IN_YEAR_INSURANCESCORE CHAR            
-----------------------------------------                           
IF(@STATE_ID=14 AND (@NUM_CUSTOMER_INSURANCE_SCORE<600 AND @NUM_CUSTOMER_INSURANCE_SCORE >=0 )AND @INTYEAR_BUILT<1950 )                          
BEGIN                           
SET @IN_YEAR_INSURANCESCORE ='Y'                          
END                           
ELSE              
BEGIN                           
SET @IN_YEAR_INSURANCESCORE='N'                          
END                   
-----------------------------------------                  
 IF(@INTREPLACEMENT_COST=0)                          
 BEGIN                           
 SET @REPLACEMENT_COST=''                          
 END                           
 ELSE IF(@INTREPLACEMENT_COST <>0)                          
 BEGIN                           
 SET @REPLACEMENT_COST='N'                          
 END                      
-----------------------------------------                  
-- DP-3 should only come up when the replacement cost is less than $75,000.                  
                  
--  declare @DP3_REPLACEMENT_COST char                  
--                   
--  if((@POLICY_TYPE=11481 or @POLICY_TYPE=11291 or @POLICY_TYPE=11292 or @POLICY_TYPE=11482 ) and @INTREPLACEMENT_COST < 75000 and (@STATE_ID=14 or @STATE_ID=22))                  
--  begin                   
--  set @DP3_REPLACEMENT_COST='Y'                  
--  end                  
--  else                  
--  begin                   
--  set @DP3_REPLACEMENT_COST='N'                   
--  end                   
---------------------------------------------------------------------------------                  
-- DP-2 should only come up when the replacement cost is less than $30,000.                  
-- Itrack No. 3281 on 3rd Jan 2008
DECLARE @POLICY_EFFECTIVE_DATE VARCHAR(20),
	@EFFECTIVE_DATE VARCHAR(20)
SET @EFFECTIVE_DATE='01/01/2008'
SELECT @POLICY_EFFECTIVE_DATE=ISNULL(DATEDIFF(DAY,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101),@EFFECTIVE_DATE),'') FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)                                                                     
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                   
 declare @DP2_REPLACEMENT_COST char 
 IF(@POLICY_EFFECTIVE_DATE <= 0)
 BEGIN
	 IF((@POLICY_TYPE=11479 OR @POLICY_TYPE=11289 OR @POLICY_TYPE=11291 OR @POLICY_TYPE=11481)
	   AND @INTREPLACEMENT_COST < 30000 AND (@STATE_ID=14 OR @STATE_ID=22))                  
		  BEGIN                   
			  SET @DP2_REPLACEMENT_COST='Y'                  
		  END                  
	 ELSE                  
		  BEGIN                   
			  SET @DP2_REPLACEMENT_COST='N'                   
		  END  
 END                  
	 ELSE                  
		  BEGIN                   
			  SET @DP2_REPLACEMENT_COST='N'                   
		  END  
               
---------------------------------------------------------------                  
-- In case of DP-2 Repair cost and  DP-3 Repair cost , Market Value and Repair cost should default to be the same                  
                  
/* declare  @MARKET_VS_REPCOST char                  
 set @MARKET_VS_REPCOST = 'N'                  
                  
 if((@POLICY_TYPE=11292 or @POLICY_TYPE=11482 or @POLICY_TYPE=11290 or @POLICY_TYPE=11480) and (@INTREPLACEMENT_COST!=@DECMARKET_VALUE) and (@STATE_ID=14 or @STATE_ID=22))                  
 begin                   
 set @MARKET_VS_REPCOST=''                  
 end   */              
                
--------Check for the policy types(REPAIR COST):----                              
IF(@POLICY_TYPE=11290 OR @POLICY_TYPE=11480 OR @POLICY_TYPE=11292 OR @POLICY_TYPE=11482)                              
	 BEGIN                              
		 SET @REPLACEMENT_COST='Y' --IF REPAIR COST POLICY THEN NOT MANDATORY FIELD                              
	 END           
ELSE                  
	  BEGIN                   
		  SET @REPLACEMENT_COST='N'                   
	  END                    
     ----------------------------------DP3 PREMIER PROGRAM--------------------                                  
--Dwelling Details Tab - Year Built                                   
--If prior to 1970 - Risk is Declined - Provide Note that Year Built is showing prior to 1970 and not Eligible for the Premier Program                                   
--If 1970 or later move to the next condition --DP3 PREMIER 11458                                  
                                 
DECLARE @ELIGIBLE_YEAR CHAR                           
DECLARE @MARKET_VALUE_DP3P CHAR                                  
IF(@POLICY_TYPE=11458)                                  
BEGIN                                  
	IF(@INTYEAR_BUILT<1970)                                  
		BEGIN                                  
			SET @ELIGIBLE_YEAR='Y'                                  
		END                                  
	ELSE                                  
		BEGIN                                  
			SET @ELIGIBLE_YEAR='N'                                  
		END                                  
 --Dwelling Details tab - Replacement Value can not be greater than the Market Value by 20%                                
 --Is greater than Submit If equal or less move to next condition                 
	IF(@DECMARKET_VALUE < = (@INTREPLACEMENT_COST * .80))                                
		BEGIN                                
			SET @MARKET_VALUE_DP3P='Y' --REFER                                
		END                                
	ELSE                                
		BEGIN                                
			  SET @MARKET_VALUE_DP3P='N'                                
		END                                 
END                                  
ELSE                                  
BEGIN                                  
  SET @ELIGIBLE_YEAR='N'                
  SET @MARKET_VALUE_DP3P='N'                                
END                    
-------------------------DP3 REPLACEMENT COST(11291,11481)----------                    
--If Policy Type is DP-3 Replacement                     
--Dwelling Details Tab - Year Built Field If year Built is prior to 1940 - submit                     
--If built in 1940 0r later move to next condition           
DECLARE @ELIGIBLE_YEAR_DP3 CHAR                   
DECLARE @MARKET_VALUE_DP3_REPLACEMENT CHAR                       
IF((@POLICY_TYPE=11291 or @POLICY_TYPE=11481 or @POLICY_TYPE=11289 or @POLICY_TYPE=11292) and @DECMARKET_VALUE!=-1)                        
BEGIN                
	IF(@INTYEAR_BUILT<1940)                        
		BEGIN                        
			SET @ELIGIBLE_YEAR_DP3='N'                        
		END                        
	ELSE                        
		BEGIN       
			SET @ELIGIBLE_YEAR_DP3='N'                        
		END  
 --Dwelling Details tab - Replacement Value can not be greater than the Market Value by 20%                                
 --Is greater than Submit If equal or less move to next condition              
 --declare @INTREPLACEMENT_COST int 100000              
 --declare @DECMARKET_VALUE int 80090               
	IF(@DECMARKET_VALUE < = (@INTREPLACEMENT_COST * .80))                                
		BEGIN                                
		SET @MARKET_VALUE_DP3_REPLACEMENT='Y' --REFER                                
		END                                
	ELSE                       
		BEGIN                                
		SET @MARKET_VALUE_DP3_REPLACEMENT='N'                                
		END                                 
                    
END                        
ELSE                        
  BEGIN                        
  SET @ELIGIBLE_YEAR_DP3='N'                  
  SET @MARKET_VALUE_DP3_REPLACEMENT ='N'                       
  END                    

/*If Policy Type is DP2 Replacement, DP-3 Replacement or DP-3 Premier                       
then check the following                       
If Coverage  A Building Limit is not equal to the Replacement Value Field on the Dwelling Details tab and/ or                        
The Market Value is less than 80% of the Replacement Value on the Dwelling Details tab then                    
and prior to 1940 and Market value does not null 
Risk is Declined :Modified on 7 July 2006*/               
                       
DECLARE @REPLACEMENT_COST_PREMIER CHAR                      
IF((@POLICY_TYPE=11289 OR @POLICY_TYPE=11479 OR @POLICY_TYPE=11291 OR @POLICY_TYPE=11481 OR @POLICY_TYPE=11458)
AND @INTYEAR_BUILT<1940 AND  @DECMARKET_VALUE!=-1)                      
BEGIN                 
	IF(@DECMARKET_VALUE < (@INTREPLACEMENT_COST * 0.80))                                          
		BEGIN                                          
			SET @REPLACEMENT_COST_PREMIER='Y'                           
		END                                          
	ELSE                                          
		BEGIN                
			SET @REPLACEMENT_COST_PREMIER='N'                                          
		END                              
END                      
ELSE                      
BEGIN                      
	SET @REPLACEMENT_COST_PREMIER='N'                      
END                  
-------------------------                              
SELECT                                                    
  @INTDWELLING_NUMBER as DWELLING_NUMBER,                                                    
  @LOCATION_ID as LOCATION_ID,                                                    
  @INTYEAR_BUILT as YEAR_BUILT,                                                    
  @REPLACEMENT_COST as REPLACEMENT_COST,                              
  -- Rules                          
  @OCCUPANCY as OCCUPANCY ,
  @BUILDING_TYPE AS BUILDING_TYPE,                            
  @MARKET_VALUE as MARKET_VALUE,                          
  @IN_YEAR_INSURANCESCORE as IN_YEAR_INSURANCESCORE,                  
  @DP2_REPLACEMENT_COST as DP2_REPLACEMENT_COST ,                 
  -- @DP3_REPLACEMENT_COST as DP3_REPLACEMENT_COST                   
  -- @MARKET_VS_REPCOST as MARKET_VS_REPCOST          
  @ELIGIBLE_YEAR_DP3 AS ELIGIBLE_YEAR_DP3,          
  @MARKET_VALUE_DP3P as MARKET_VALUE_DP3P,          
  @ELIGIBLE_YEAR as ELIGIBLE_YEAR,
  @REPLACEMENT_COST_PREMIER AS REPLACEMENT_COST_PREMIER,
  @MARKET_VALUE_DP3_REPLACEMENT AS MARKET_VALUE_DP3_REPLACEMENT                                                    
END









GO

