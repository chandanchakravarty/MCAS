IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PropertyInflationAdjustment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PropertyInflationAdjustment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--
--drop proc   dbo.Proc_PropertyInflationAdjustment    
--
--go 

/*============================================================================================  
Proc Name       : dbo.Proc_PropertyInflationAdjustment    
Created by      : Ashwani     
Date            : 30-10-2006  
Purpose       : To adjust the replacement cost/market value in case of renewal policy      
Revison History :      
modified by :Pravesh K. Chandel  
purpose  : optomisation and Adjust coverage A for both Repair and Replacement  
  
modified by :Pravesh K. Chandel  
Modified Date : 5 july 2007  
purpose  : -- Round to the nearest $1,000 to Market Value/Replacement Cost and Cov A  
Used In    : Wolverine      

modified by :Pravesh K. Chandel  
Modified Date : 04 Feb 2008  
purpose  : -- Change  New Replcaement/market Value calculation Logic from just multiplied to fator to Percentage of Factor

Modified by		: Ravindra	Gupta 
Modified Date	: 02-20-2008
purpose			: Revert back the changes made by Prevesh on Feb 4 as Suggested by Bill
Used In    : Wolverine      
  
  
===============================================================================================  
Date     Review By          Comments      
====== ==============  =========================================================================  
drop proc   dbo.Proc_PropertyInflationAdjustment      
*/  
    
CREATE PROC [dbo].[Proc_PropertyInflationAdjustment]                                                   
(                                            
		@CUSTOMER_ID INT,                                            
		@POLICY_ID  INT,                                            
		@POLICY_VERSION_ID SMALLINT,  
		@DWELLING_ID INT,  
		@TRAN_DESC VARCHAR(500) OUTPUT,  
		@LOB_ID NVARCHAR(5) OUTPUT                                               
)                                            
AS                                            
BEGIN  
-- BEGIN TRAN    
-- 1: Homeowners  UPDATE IF POLICY_TYPE IS HO-2 ,HO-3 OR  HO-5   
-- IN   
-- replacement : 11148,11149,11192   
-- repair      : 11193,11194   
-- MI   
-- replacement : 11400,11401,11402  
-- repair      : 11403,11404  
-- premium     : 11409,11410  
  
-- 2: Rental  UPDATE IF POLICY_TYPE IS DP-2 ,DP-3   
-- IN   11480 - DP-2 Repair,11479-  DP-2 Replacement ,11482-  DP-3 Repair  ,11481-  DP-3 Replacement    
-- MI   11290  DP-2 Repair,11289  DP-2 Replacement,11458  DP-3 Premier,11292  DP-3 Repair,11291  DP-3 Replacement    
  
-- 2. UPDATE ONLY IF APP_TERMS IS 06 MONTHS AND POL_EFF_DATE IS BETWEEN 01 JAN TO 31 JUNE  
/* POLICY TYPE INCLUDED  
11148 HO-3 Replacement  
11192 HO-2 Replacement  
11289 DP-2 Replacement  
11291 DP-3 Replacement  
11400 HO-3 Replacement  
11402 HO-2 Replacement  
11479 DP-2 Replacement  
11481 DP-3 Replacement  
  
11149 HO-5 Replacement  
11401 HO-5 Replacement  
  
11193 HO-2 Repair  
11194 HO-3 Repair  
11290 DP-2 Repair  
11292 DP-3 Repair  
11403 HO-2 Repair  
11404 HO-3 Repair  
11480 DP-2 Repair  
11482 DP-3 Repair  
  
11409 HO-3 Premier  
11410 HO-5 Premier  
11458 DP-3 Premier  
*/  
  
DECLARE @POLICY_TYPE NVARCHAR(12)  
DECLARE @APP_TERMS NVARCHAR(5)  
DECLARE @APP_EFFECTIVE_DATE_MONTH INT   
DECLARE @POLICY_LOB NVARCHAR(5)  
DECLARE @APP_EFFECTIVE_DATE DATETIME  
DECLARE @FROM_AS400 CHAR(1)  
DECLARE @RENEWAL_COUNT SMALLINT
DECLARE @RENEWAL_COMMIT SMALLINT
SET @RENEWAL_COUNT=0
SET @FROM_AS400='N'
--18          Commit Renewal Process  
SET @RENEWAL_COMMIT = 18
SELECT @POLICY_TYPE=ISNULL(POLICY_TYPE,''),@APP_TERMS=ISNULL(APP_TERMS,''),@APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,  
		@APP_EFFECTIVE_DATE_MONTH=MONTH(APP_EFFECTIVE_DATE),@POLICY_LOB=isnull(POLICY_LOB,''),
		@FROM_AS400=ISNULL(FROM_AS400,'N')
		FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
 
SELECT @FROM_AS400=ISNULL(FROM_AS400,'N')
		FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK)
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID --AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
		AND ISNULL(FROM_AS400,'N')='Y'

SELECT  @RENEWAL_COUNT =COUNT(PROCESS_ID) FROM POL_POLICY_PROCESS WITH(NOLOCK)
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=@RENEWAL_COMMIT
		AND ISNULL(REVERT_BACK,'N')<>'Y'

SET @LOB_ID=@POLICY_LOB  
declare @REPAIR_FLAG char(1)  
declare @REPLACEMENT_FLAG char(1)  
declare @PREMIER_FLAG char(1)  

set @REPLACEMENT_FLAG='N'  
set @REPAIR_FLAG='N'  
set @PREMIER_FLAG='N'  

if (@POLICY_TYPE= 11148 or @POLICY_TYPE=11192 or @POLICY_TYPE=11289 or @POLICY_TYPE=11291 or @POLICY_TYPE=11400    
	or @POLICY_TYPE=11402 or @POLICY_TYPE=11479 or @POLICY_TYPE=11481 or @POLICY_TYPE=11149 or @POLICY_TYPE=11401)  
	set @REPLACEMENT_FLAG='Y'  

if (@POLICY_TYPE=11193 or @POLICY_TYPE=11194 or @POLICY_TYPE=11290 or @POLICY_TYPE=11292 or @POLICY_TYPE=11403   
	or @POLICY_TYPE=11404 or @POLICY_TYPE=11480 or @POLICY_TYPE=11482)   
	set @REPAIR_FLAG='Y'  

if (@POLICY_TYPE=11409 or @POLICY_TYPE=11410 or @POLICY_TYPE=11458)   
	set @PREMIER_FLAG='Y'  

 /*  
  IF(@POLICY_TYPE=11193 OR @POLICY_TYPE=11192 OR @POLICY_TYPE=11194 OR @POLICY_TYPE=11148 OR @POLICY_TYPE=11149  
     OR @POLICY_TYPE=11400 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11402 OR @POLICY_TYPE=11403 OR @POLICY_TYPE=11404  
     OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11410 OR   
     @POLICY_TYPE=11480 OR @POLICY_TYPE=11479 OR @POLICY_TYPE=11482 OR @POLICY_TYPE=11481 OR @POLICY_TYPE=11290  
     OR @POLICY_TYPE=11289 OR @POLICY_TYPE=11458 OR @POLICY_TYPE=11292 OR  @POLICY_TYPE=11291)  
 */  

IF (@REPLACEMENT_FLAG='Y' OR @REPAIR_FLAG='Y' or @PREMIER_FLAG='Y' )  
BEGIN 
	-- NO INFLATION ON FIRST RENEWAL OF AS400 POLICY ADDED BY PRAVESH ON 15 APRIL 2008
	IF (@RENEWAL_COUNT=0 AND @FROM_AS400='Y') 
 	BEGIN   
		RETURN  
	END  
	IF(@APP_TERMS=6 AND (@APP_EFFECTIVE_DATE_MONTH NOT BETWEEN 1 AND 6))  
	BEGIN   
		RETURN  
	END    

	DECLARE @MARKET_VALUE DECIMAL(18,2)  
	DECLARE @REPLACEMENT_COST DECIMAL(18,2)  
	DECLARE @LOCATION_ID INT  

	SELECT @REPLACEMENT_COST=REPLACEMENT_COST,@MARKET_VALUE=MARKET_VALUE,  
		   @LOCATION_ID=LOCATION_ID  
	FROM POL_DWELLINGS_INFO  
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
	AND DWELLING_ID=@DWELLING_ID  

	-- FETCH LOC_ZIP,LOC_STATE FROM POL_LOCATIONS  
	DECLARE @LOC_ZIP NVARCHAR(3)  
	DECLARE @LOC_STATE NVARCHAR(5)  

	--SELECT @LOC_ZIP=LOC_ZIP,@LOC_STATE=LOC_STATE   
	SELECT @LOC_ZIP=substring(LOC_ZIP,1,3),@LOC_STATE=LOC_STATE  --changed by pravesh only left 3 dugit are used for inflation guard  
	FROM POL_LOCATIONS  
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
	AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOCATION_ID=@LOCATION_ID  
 
	-- FETCH FACTOR FROM INFLATION_COST_FACTORS   
	DECLARE @FACTOR DECIMAL(12,3)  
	SELECT @FACTOR=FACTOR FROM INFLATION_COST_FACTORS   
	WHERE ZIP_CODE=@LOC_ZIP AND STATE_ID=@LOC_STATE   
	AND LOB_ID=@POLICY_LOB  -- only for LOB Base  
   AND @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31')  


	IF (@FACTOR is null) --no factor for corresponding LOB/Home and Rental  
	begin  
		SELECT @FACTOR=FACTOR FROM INFLATION_COST_FACTORS   
		WHERE ZIP_CODE=@LOC_ZIP AND STATE_ID=@LOC_STATE   
		AND  LOB_ID = 0  AND @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31')  
	end  
  
	---if no factor found take Average of State   
	IF (@FACTOR is null)  
	begin   
		SELECT @FACTOR=avg(FACTOR) FROM INFLATION_COST_FACTORS   
		WHERE  STATE_ID=@LOC_STATE   
		AND LOB_ID=@POLICY_LOB AND @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31')  
	  
		if (@FACTOR is null)  
		begin  
			SELECT @FACTOR=avg(FACTOR) FROM INFLATION_COST_FACTORS   
			WHERE  STATE_ID=@LOC_STATE   
			AND LOB_ID = 0  AND @APP_EFFECTIVE_DATE BETWEEN EFFECTIVE_DATE AND ISNULL(EXPIRY_DATE,'3000-12-31')  
		end  
		if (@FACTOR is null) --if no factor found no adjustment  
		begin  
			SET @TRAN_DESC = 'No Property Inflation adjustment has been done. As Inflation factor not found.;'   
			return  
		end  
	end   

	-- UPDATE REPLACEMENT_COST AND MARKET_VALUE IN  POL_DWELLINGS_INFO  
	DECLARE @NEW_MARKET_VALUE DECIMAL(18,2)  
	DECLARE @NEW_REPLACEMENT_COST DECIMAL(18,2)   
	/*  
	IF(@POLICY_TYPE=11193 OR @POLICY_TYPE=11194 or @POLICY_TYPE=11403 OR @POLICY_TYPE=11404 OR   
	@POLICY_TYPE=11480 OR @POLICY_TYPE=11482 or @POLICY_TYPE=11290 OR @POLICY_TYPE=11292 )-- REPAIR_COST  
	*/  
	IF (@REPAIR_FLAG='Y')  
	BEGIN   
		SET @NEW_MARKET_VALUE =  @MARKET_VALUE * @FACTOR    
		
		--change by pravesh on 4 feb 2008
		--SET @NEW_MARKET_VALUE =  @MARKET_VALUE+ (@MARKET_VALUE * @FACTOR /100)  

		IF(@NEW_MARKET_VALUE-@MARKET_VALUE<1000)  
			SET @NEW_MARKET_VALUE = (@MARKET_VALUE + 1000)  

--		ELSE  
--  	  		SET @NEW_MARKET_VALUE =  @MARKET_VALUE+ (@MARKET_VALUE * @FACTOR /100)  
	
		-- Round to the nearest $1,000  
		--  SET @NEW_MARKET_VALUE = (@NEW_MARKET_VALUE/1000) * 1000 
		SET @NEW_MARKET_VALUE = round(cast((cast(@NEW_MARKET_VALUE as numeric)/ 1000) as numeric),0)*1000 
		UPDATE POL_DWELLINGS_INFO        
		SET MARKET_VALUE=@NEW_MARKET_VALUE,INFLATION_FACTOR=@FACTOR  

		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID   
		SET @TRAN_DESC = 'Market Value has been adjusted from ' + CONVERT(VARCHAR(100),cast(@MARKET_VALUE as numeric)) + ' to ' + CONVERT(VARCHAR(100),cast(@NEW_MARKET_VALUE as numeric)) + '.;'     

   
    END   
	ELSE -- REPLACEMENT COST  
    BEGIN   
		SET @NEW_REPLACEMENT_COST = @REPLACEMENT_COST * @FACTOR   

		--change by pravesh on 4 feb 2008
		--SET @NEW_REPLACEMENT_COST = @REPLACEMENT_COST+ (@REPLACEMENT_COST * @FACTOR/100)   

		IF(@NEW_REPLACEMENT_COST-@REPLACEMENT_COST<1000)  
			SET @NEW_REPLACEMENT_COST = (@REPLACEMENT_COST + 1000)  

--		ELSE  
--		SET @NEW_REPLACEMENT_COST = @REPLACEMENT_COST+ (@REPLACEMENT_COST * @FACTOR/100)

		-- Round to the nearest $1,000  
		SET @NEW_REPLACEMENT_COST = round(cast((cast(@NEW_REPLACEMENT_COST as numeric)/ 1000) as numeric),0)*1000
		UPDATE POL_DWELLINGS_INFO        
		SET REPLACEMENT_COST=@NEW_REPLACEMENT_COST,INFLATION_FACTOR=@FACTOR       
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID    
		SET @TRAN_DESC = 'Replacement Cost has been adjusted from ' + convert(varchar(100),cast(@REPLACEMENT_COST as numeric) ) + ' to ' + convert(varchar(100),cast(@NEW_REPLACEMENT_COST as numeric)) + '.;'   
  
    END  
    
	-- FOR HO ,IN CASE OF HO-2 & HO-3 REPLACEMENT COST , UPDATE THE VALUE OF COVERAGE A BY THE SAME FACTOR  
	-- IN : 11192 - HO-2 REPLACEMENT,11148 - HO-3 REPLACEMENT  
	-- MI : 11402 - HO-2 REPLACEMENT, 11400 - HO-3 REPLACEMENT  

	-- FOR RD, IN CASE OF DP-2 & DP-3 REPLACEMENT COST , UPDATE THE VALUE OF COVERAGE A BY THE SAME FACTOR  
	-- IN   11479-  DP-2 Replacement ,11481-  DP-3 Replacement    
	-- MI   11289  DP-2 Replacement,11291  DP-3 Replacement    
	/*  
	11193 1195 HO-2 Repair  
	11194 1195 HO-3 Repair  
	11403 1216 HO-2 Repair  
	11404 1216 HO-3 Repair  
	11480 1223 DP-2 Repair  
	11482 1223 DP-3 Repair  
	11290 1203 DP-2 Repair  
	11292 1203 DP-3 Repair  
	*/  
	/*  
	IF(@POLICY_TYPE=11192 OR @POLICY_TYPE=11148 or @POLICY_TYPE=11402 OR @POLICY_TYPE=11400 OR   
	@POLICY_TYPE=11479 OR @POLICY_TYPE=11481 or @POLICY_TYPE=11289 OR @POLICY_TYPE=11291  
	or @POLICY_TYPE=11193 OR @POLICY_TYPE=11194 or @POLICY_TYPE=11403 OR @POLICY_TYPE=11404  
	or @POLICY_TYPE=11480 OR @POLICY_TYPE=11482 or @POLICY_TYPE=11290 OR @POLICY_TYPE=11292  
	)*/  

	IF(@REPLACEMENT_FLAG='Y' OR @REPAIR_FLAG='Y' OR   @PREMIER_FLAG='Y')  
	BEGIN   
     
		DECLARE @LIMIT_1 DECIMAL(18,0)  

		SELECT @LIMIT_1=LIMIT_1   
		FROM POL_DWELLING_SECTION_COVERAGES  
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID  
		AND COVERAGE_CODE_ID IN (3,134,773,793)      

		DECLARE @NEW_LIMIT_1 DECIMAL(18,0)  
		SET @NEW_LIMIT_1 = (@LIMIT_1 * @FACTOR)  
		--SET @NEW_LIMIT_1 = @LIMIT_1 + (@LIMIT_1 * @FACTOR/100)  
		
		IF(@NEW_LIMIT_1-@LIMIT_1<1000)  
			SET @NEW_LIMIT_1 = (@LIMIT_1 + 1000)
     
		-- Round to the nearest $1,000  
		-- SET @NEW_LIMIT_1 = (@NEW_LIMIT_1/1000)* 1000  
		SET @NEW_LIMIT_1 = round(cast((cast(@NEW_LIMIT_1 as numeric)/ 1000) as numeric),0)*1000
		UPDATE  POL_DWELLING_SECTION_COVERAGES  
		SET  LIMIT_1  = @NEW_LIMIT_1  
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DWELLING_ID=@DWELLING_ID  
		AND COVERAGE_CODE_ID IN (3,134,773,793)  -- Cov_ID 3 MI 134 IN  
		SET @TRAN_DESC = @TRAN_DESC + ' Coverage A has been adjusted from '+ convert(varchar(100), @LIMIT_1) + ' to '+ convert(varchar(100),@NEW_LIMIT_1) + '.;'  
		END   
        
  --  END   
     END  
END  
  
--
-- 
--go 
--
--declare @T varchar(500)
--exec Proc_PropertyInflationAdjustment    2078,	1	,4  ,1 , @t out,  6 
--
--select @t 
----select * from pol_customer_policy_list where policy_number = 'R8961700'
--rollback tran 


GO

