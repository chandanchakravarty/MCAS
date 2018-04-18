IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CalculateEquityCancellationDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CalculateEquityCancellationDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- begin tran 
-- drop proc Dbo.Proc_CalculateEquityCancellationDate
-- go
/*----------------------------------------------------------                
 Proc Name       : Dbo.Proc_CalculateEquityCancellationDate
 Created by      : Ravindra 
 Date            : 12-30-2006
 Purpose         : Calculates  the Effective Date of cancellation when method is equity
 Revison History :                
 Used In     	 : Wolverine   (Cancellation Process)             

------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop proc Dbo.Proc_CalculateEquityCancellationDate
CREATE PROC [dbo].[Proc_CalculateEquityCancellationDate]
(                
	 @CUSTOMER_ID  INT,          
	 @POLICY_ID  INT,        
	 @POLICY_VERSION_ID INT,      
	 @CHANGE_EFFECTIVE_DATE Datetime Out
)                
AS               
BEGIN 

DECLARE @TOTAL_GROSS_PREMIUM 	Decimal(18,2) ,        
	@CURRENT_TERM  		Int,
	@POLICY_EFFECTIVE_DATE	Datetime,
	@POLICY_EXPIRATION_DATE Datetime,
	@INSTALL_PLAN_ID 	Int,
	@LOB_ID 		Int,
	@SUB_LOB_ID 		Smallint,
	@STATE_ID 		SmallInt,
	@EARNED_PREMIUM 	Decimal(18,2),
	@MINIMUM_PREMIUM 	Decimal(18,2),
	@TOTAL_UNPAID		Decimal(18,2)

-- Fetch Policy Level Details
SELECT  @CURRENT_TERM = CURRENT_TERM ,
	@POLICY_EFFECTIVE_DATE  = APP_EFFECTIVE_DATE,
	@POLICY_EXPIRATION_DATE	= APP_EXPIRATION_DATE,
	@INSTALL_PLAN_ID = INSTALL_PLAN_ID,
	@LOB_ID  = POLICY_LOB , 
	@SUB_LOB_ID = ISNULL(POLICY_SUBLOB ,0),
	@STATE_ID = STATE_ID
FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) 
WHERE   CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID

SET @MINIMUM_PREMIUM  = 0 
SELECT @MINIMUM_PREMIUM = ISNULL(PREMIUM_AMT , 0)
FROM ACT_MINIMUM_PREM_CANCEL 
WHERE 	LOB_ID = @LOB_ID
	AND ISNULL(SUB_LOB_ID,0) = @SUB_LOB_ID
	AND STATE_ID = @STATE_ID
	AND ISNULL(IS_ACTIVE,'Y') = 'Y'
	AND @CHANGE_EFFECTIVE_DATE BETWEEN EFFECTIVE_FROM_DATE AND EFFECTIVE_TO_DATE

SET @TOTAL_GROSS_PREMIUM = 0
-- Changed by Ravindra(08-24-2007) For calculating cancellation equity date enforce premium need to be considered
-- instead of written premium

SELECT @TOTAL_GROSS_PREMIUM = SUM(PPDD.INFORCE_PREMIUM)  
FROM ACT_PREMIUM_PROCESS_SUB_DETAILS PPDD
WHERE 	PPDD.CUSTOMER_ID = @CUSTOMER_ID 
	AND PPDD.POLICY_ID =	@POLICY_ID	
	AND PPDD.POLICY_VERSION_ID IN (SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID 
							AND CPL.POLICY_ID = @POLICY_ID AND CPL.CURRENT_TERM = @CURRENT_TERM ) 

-- Temporary Code to be removed due the implementation change policies creadted before this change will not 
-- have premium data in ACT_PREMIUM_PROCESS_SUB_DETAILS and hence total premium is required to be pulled 
-- from main premium process table
IF ( @TOTAL_GROSS_PREMIUM IS NULL)
BEGIN 
	SET @TOTAL_GROSS_PREMIUM = 0
	CREATE TABLE #POSTED_PREMIUM
	(	
		[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,
		[POSTED_PREMIUM_XML] VARCHAR(4000),
		[CURRENT_TERM] Int ,
		POLICY_VERSION_ID Int
	)

	INSERT INTO #POSTED_PREMIUM
	(
		POSTED_PREMIUM_XML ,
		CURRENT_TERM ,
		POLICY_VERSION_ID
	)
	SELECT  PPD.POSTED_PREMIUM_XML , PCL.CURRENT_TERM ,PPD.POLICY_VERSION_ID
	FROM ACT_PREMIUM_PROCESS_DETAILS PPD WITH(NOLOCK) 
	INNER JOIN POL_CUSTOMER_POLICY_LIST PCL WITH(NOLOCK) 
	ON 	PPD.CUSTOMER_ID = PCL.CUSTOMER_ID
		AND PPD.POLICY_ID = PCL.POLICY_ID 
	WHERE 	PCL.CUSTOMER_ID = @CUSTOMER_ID 
		AND PCL.POLICY_ID = @POLICY_ID 
		AND PCL.POLICY_VERSION_ID = @POLICY_VERSION_ID
		AND PCL.CURRENT_TERM = @CURRENT_TERM

	DECLARE @POSTED_PREMIUM_XML VARCHAR(4000)
	DECLARE @IDENT_COL Int

	SET @IDENT_COL = 1

	WHILE 1 = 1              
	BEGIN  
		IF NOT EXISTS              
		(              
		SELECT IDENT_COL FROM #POSTED_PREMIUM             
		WHERE IDENT_COL = @IDENT_COL              
		)      
		BEGIN            
			BREAK              
		END              

		SELECT @POSTED_PREMIUM_XML = POSTED_PREMIUM_XML 
		FROM #POSTED_PREMIUM
		WHERE IDENT_COL = @IDENT_COL              

		DECLARE @IDOC INT         
		--CREATE AN INTERNAL REPRESENTATION OF THE XML DOCUMENT.        
		EXEC SP_XML_PREPAREDOCUMENT @IDOC OUTPUT, @POSTED_PREMIUM_XML
	        
		-- EXECUTE A SELECT STATEMENT THAT USES THE OPENXML ROWSET PROVIDER.        
		
		DECLARE @NET_PREMIUM VARCHAR(40) ,       
			@GROSS_PREMIUM VARCHAR(40),        
			@MCCA_FEES  VARCHAR(40)  ,    
			@OTHER_FEES VARCHAR(40)      
	        

		SELECT  @NET_PREMIUM = NETPREMIUM ,        
			@GROSS_PREMIUM = GROSSPREMIUM,         
			@MCCA_FEES = MCCAFEES ,        
			@OTHER_FEES = OTHERFEES        
		FROM   OPENXML (@IDOC, '/PREMIUM',2 )        
			   WITH(        
			  NETPREMIUM   VARCHAR(40),         
			  GROSSPREMIUM VARCHAR(40),        
			  MCCAFEES     VARCHAR(40),        
			  OTHERFEES    VARCHAR(40)        
			 )        
		
		EXEC SP_XML_REMOVEDOCUMENT @IDOC

		SET @TOTAL_GROSS_PREMIUM = @TOTAL_GROSS_PREMIUM  + ISNULL(CONVERT(Decimal , @GROSS_PREMIUM ), 0)          
		SET @IDENT_COL = @IDENT_COL + 1
	END

	DROP TABLE #POSTED_PREMIUM
END

-- Changed by Ravindra: ref Track 3083 and Calculation Logic mailed by Sally on 12-12-2007
-- To calulate Equity Date calculate  unpaid written premium the calulate unpaid days using Inforce Premium 
-- Substract these days from Policy Expiry Date result will be the Equity date
-- Calculate Paid Premium

-- SELECT @TOTAL_PAID = ISNULL(SUM(ISNULL(OI.TOTAL_PAID,0)),0) 
-- FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK) 
-- WHERE 	OI.CUSTOMER_ID = @CUSTOMER_ID 
-- 	AND OI.POLICY_ID = @POLICY_ID 
-- 	AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
-- 	AND OI.POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
-- 				WHERE CUSTOMER_ID = @CUSTOMER_ID 
-- 					AND POLICY_ID = @POLICY_ID 
-- 					AND CURRENT_TERM = @CURRENT_TERM)
-- 
-- 
-- DECLARE @TOTAL_DAYS INT  ,
-- 	@CALCULATIVE_EARNED_DAYS Int
-- 
-- SET 	@CALCULATIVE_EARNED_DAYS  = 0
-- 
-- 
-- SET @TOTAL_DAYS = DATEDIFF(DAY, @POLICY_EFFECTIVE_DATE, @POLICY_EXPIRATION_DATE)  
-- 
-- SELECT @CALCULATIVE_EARNED_DAYS   =  ROUND((@TOTAL_DAYS * @TOTAL_PAID )/ @TOTAL_GROSS_PREMIUM ,0)
-- 
-- SET @CHANGE_EFFECTIVE_DATE  = DATEADD(DD,@CALCULATIVE_EARNED_DAYS,@POLICY_EFFECTIVE_DATE)

SELECT @TOTAL_UNPAID = ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0) 
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK) 
WHERE 	OI.CUSTOMER_ID = @CUSTOMER_ID 
	AND OI.POLICY_ID = @POLICY_ID 
	AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	AND OI.POLICY_VERSION_ID IN 	(
					SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
					WHERE CUSTOMER_ID = @CUSTOMER_ID 
					AND POLICY_ID = @POLICY_ID 
					AND CURRENT_TERM = @CURRENT_TERM
					)

-- Ravindra ( 12-17-2007) : Add up DB type JEs to unpaid premium . 

SELECT @TOTAL_UNPAID = @TOTAL_UNPAID + ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0) ),0) 
FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK) 
WHERE 	OI.CUSTOMER_ID = @CUSTOMER_ID 
	AND OI.POLICY_ID = @POLICY_ID 
	AND OI.ITEM_TRAN_CODE_TYPE = 'JE'
	AND OI.ITEM_TRAN_CODE = 'JE'
	AND OI.POLICY_VERSION_ID IN 	(
					SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)
					WHERE CUSTOMER_ID = @CUSTOMER_ID 
					AND POLICY_ID = @POLICY_ID 
					AND CURRENT_TERM = @CURRENT_TERM
					)
	AND  ISNULL(OI.TOTAL_DUE,0) >0

DECLARE @TOTAL_DAYS INT  ,
	@UNPAID_DAYS Int

SET 	@UNPAID_DAYS = 0


SET @TOTAL_DAYS = DATEDIFF(DAY, @POLICY_EFFECTIVE_DATE, @POLICY_EXPIRATION_DATE)  

SELECT @UNPAID_DAYS   =  ROUND((@TOTAL_DAYS * @TOTAL_UNPAID )/ @TOTAL_GROSS_PREMIUM ,0)

--Ravindra(08-04-2008) : Changed as per iTrack 4461

SET @UNPAID_DAYS    = @UNPAID_DAYS    + 1

SET @CHANGE_EFFECTIVE_DATE  = DATEADD(DD,@UNPAID_DAYS * - 1,@POLICY_EXPIRATION_DATE)


END

-- go
-- 
-- Declare @d datetime 
-- exec Proc_CalculateEquityCancellationDate 1320 , 202 ,1,@d out 
-- select @D
-- 
-- rollback tran
-- 
-- 
-- 
-- 
-- 













GO

