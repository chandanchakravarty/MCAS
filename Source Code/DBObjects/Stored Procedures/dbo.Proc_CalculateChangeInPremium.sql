IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CalculateChangeInPremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CalculateChangeInPremium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------------------  
Procedure Name	: dbo.Proc_CalculateChangeInPremium  
Created By  	: Ravindra
Created Date 	: 2-12-2007 
Purpose   		: Calculate Pro Rata based Endorsement Premium

Revison History :      
Modified By 	: Praveen Kasana
Modified On		: 14 Sep 2009
Purpose			: 
				: Round Issue (@CHANGE_PREMIUM_AMOUNT) 
          
Purpose  
-----------------------------------------------------------------------*/  
-- drop proc dbo.Proc_CalculateChangeInPremium
CREATE PROCEDURE dbo.Proc_CalculateChangeInPremium
(  
	@CUSTOMER_ID    			INT,  	--Id of customer whose premium policy will be posted  
	@POLICY_ID    				INT,  	--Policy identification number  
	@POLICY_VERSION_ID  		INT,   	--Policy version identification number  
	@GROSS_PREMIUM_AMOUNT 		DECIMAL(25,8),  --Premium amount,  
	@MCCA_FEES					decimal(25,8),		-- MCCA fees amount
	@OTHER_FEES					decimal(25,8),		-- Other fees
	@FEES_TO_REVERSE			decimal(25,2),
	@CHANGE_PREMIUM_AMOUNT		decimal(25,8) OUTPUT,		
	@CHANGE_MCCA_FEES			Decimal(25,2) OUTPUT,
	@CHANGE_OTHER_FEES			Decimal(25,2) OUTPUT,
	@CHANGE_FEES_TO_REVERSE		Decimal(25,2) OUTPUT,
	@CALLED_FROM varchar(8) = null
)  
AS
BEGIN 
	DECLARE @POLICY_EFFECTIVE_DATE DATETIME,  
		@POLICY_EXPIRATION_DATE DATETIME,  
		@POL_VER_EFFECTIVE_DATE DateTime,
		@POL_VER_EXPIRATION_DATE DateTime
	
	SELECT  @POLICY_EFFECTIVE_DATE 	 = APP_EFFECTIVE_DATE,  
		@POLICY_EXPIRATION_DATE  = APP_EXPIRATION_DATE,  
		@POL_VER_EFFECTIVE_DATE	 = POL_VER_EFFECTIVE_DATE,
		@POL_VER_EXPIRATION_DATE = POL_VER_EXPIRATION_DATE
	FROM POL_CUSTOMER_POLICY_LIST   
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID  


	/*Calculating the pro-rated change in premium as per change effective date and policy efective date*/  
	DECLARE @TOTAL_DAYS INT  
	SET @TOTAL_DAYS = DATEDIFF(DAY, @POLICY_EFFECTIVE_DATE, @POLICY_EXPIRATION_DATE)  

	--Modified Praveen Kasana in 14 Sep 2009 
	IF(@CALLED_FROM = 'TOTAL')
		SELECT @CHANGE_PREMIUM_AMOUNT =	  ROUND(	ROUND((( @GROSS_PREMIUM_AMOUNT * DATEDIFF ( DAY, @POL_VER_EFFECTIVE_DATE, @POL_VER_EXPIRATION_DATE ) ) / @TOTAL_DAYS ),2) ,0)
	ELSE
		SELECT @CHANGE_PREMIUM_AMOUNT =  ROUND((( @GROSS_PREMIUM_AMOUNT * DATEDIFF ( DAY, @POL_VER_EFFECTIVE_DATE, @POL_VER_EXPIRATION_DATE ) ) / @TOTAL_DAYS ),0)  
		
	
	--- Calculating Pro Rata Changes in Fees
	
	SET @CHANGE_MCCA_FEES =  ROUND((( @MCCA_FEES * DATEDIFF ( DAY, @POL_VER_EFFECTIVE_DATE, @POL_VER_EXPIRATION_DATE ) ) / @TOTAL_DAYS ),0)  
	
	SET @CHANGE_OTHER_FEES =  ROUND((( @OTHER_FEES * DATEDIFF (  DAY, @POL_VER_EFFECTIVE_DATE, @POL_VER_EXPIRATION_DATE ) ) / @TOTAL_DAYS ),0)  

	SET @CHANGE_FEES_TO_REVERSE = ROUND((( @FEES_TO_REVERSE * DATEDIFF (  DAY, @POL_VER_EFFECTIVE_DATE, @POL_VER_EXPIRATION_DATE ) ) / @TOTAL_DAYS ),0)  
END








GO

