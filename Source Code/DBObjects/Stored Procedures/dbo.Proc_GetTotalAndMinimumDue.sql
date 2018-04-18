IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTotalAndMinimumDue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTotalAndMinimumDue]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--	drop proc dbo.Proc_GetTotalAndMinimumDue    
--go 
/*----------------------------------------------------------      
Proc Name          : dbo.Proc_GetTotalAndMinimumDue    
Created by         :        
Date               :       
Purpose            : To fetch Minimum and Total Due of a customer as per Premium Notice Logic    
						Ref : EBX-DV25-->SDLC-->Design-->Accounting-->    
						Changes-->EBX-DV25 - Billing Premium Notice    
Revison History    :      
Used In            :   Wolverine        
exec Proc_GetTotalAndMinimumDue 603,8,1 , null  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/     
-- drop proc dbo.[Proc_GetTotalAndMinimumDue]    
CREATE  PROCEDURE [dbo].[Proc_GetTotalAndMinimumDue]    
(      
		@CUSTOMER_ID   Int ,    
		@POLICY_ID    Int ,    
		@POLICY_VERSION_ID  SmallInt,    
		@AS_ON_DATE    Datetime = null    
)      
AS     
BEGIN    

	DECLARE @CURRENT_TERM Int,  
			@agency_id int    
	 
	SELECT @CURRENT_TERM = CURRENT_TERM      
	FROM POL_CUSTOMER_POLICY_LIST with (nolock)    
	WHERE CUSTOMER_ID = @CUSTOMER_ID     
	AND POLICY_ID  = @POLICY_ID     
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID    

	IF(@AS_ON_DATE IS NULL)     
		SET @AS_ON_DATE = CAST(CONVERT(VARCHAR,GETDATE(),101) AS Datetime)    
	    
	    
	DECLARE @PREMIUM_DUE_AS_ON_DATE Decimal(18,2),    
			@FEES_DUE_AS_ON_DATE Decimal(18,2),    
			@TOTAL_PREMIUM_DUE  Decimal(18,2),    
			@FEES_INS_DUE_AS_ON_DATE    Decimal(18,2) , 
			@TOTAL_DUE		Decimal(18,2) 

	SET  @PREMIUM_DUE_AS_ON_DATE = 0     
	SET  @FEES_DUE_AS_ON_DATE = 0     
	SET  @TOTAL_PREMIUM_DUE  = 0  
	SET  @FEES_INS_DUE_AS_ON_DATE  = 0      
	    
	-- Calculate Premium Due as on date
	SELECT @PREMIUM_DUE_AS_ON_DATE = ISNULL(SUM(ISNULL( OI.TOTAL_DUE ,0)),0)    
			- ISNULL(SUM(ISNULl(OI.TOTAL_PAID,0)),0)                        
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)                        
	WHERE  CAST(CONVERT(VARCHAR,OI.DUE_DATE,101) AS Datetime) <= @AS_ON_DATE                        
	AND OI.CUSTOMER_ID = @CUSTOMER_ID                        
	AND OI.POLICY_ID  = @POLICY_ID                        
	AND OI.ITEM_TRAN_CODE_TYPE <> 'FEES'                        
	GROUP BY OI.CUSTOMER_ID , OI.POLICY_ID           
	    
	-- Calculate fees dues as on date (Other than installment fees)
	SELECT @FEES_DUE_AS_ON_DATE = ISNULL(SUM(ISNULL( OI.TOTAL_DUE ,0)),0)    
			- ISNULL(SUM(ISNULl(OI.TOTAL_PAID,0)),0)                        
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)                        
	WHERE  CAST(CONVERT(VARCHAR,OI.DUE_DATE,101) AS Datetime) <= @AS_ON_DATE                        
	AND OI.CUSTOMER_ID = @CUSTOMER_ID                        
	AND OI.POLICY_ID  = @POLICY_ID                        
	AND OI.ITEM_TRAN_CODE_TYPE = 'FEES'    
	AND OI.ITEM_TRAN_CODE <> 'INSF'                      
	GROUP BY OI.CUSTOMER_ID , OI.POLICY_ID  


--	-- Calculate Installment fee due for first premium item due included in Minimum due
--	SELECT  @FEES_INS_DUE_AS_ON_DATE = ISNULL( OPI.TOTAL_DUE ,0)	- ISNULl(OPI.TOTAL_PAID,0)
--	FROM ACT_CUSTOMER_OPEN_ITEMS OPI with (nolock)    
--	WHERE OPI.IDEN_ROW_ID = 
--	(
--		SELECT	 TOP 1 OI.IDEN_ROW_ID
--		FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)
--		INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD WITH(NOLOCK) 
--		ON OI.INSTALLMENT_ROW_ID  = INSD.ROW_ID
--		WHERE	OI.CUSTOMER_ID= @CUSTOMER_ID
--		AND		OI.POLICY_ID= @POLICY_ID
--		AND		CAST(CONVERT(VARCHAR,OI.DUE_DATE,101) AS Datetime) <= @AS_ON_DATE 
--		AND		ISNULL(OI.TOTAL_DUE,0) <> ISNULL(OI.TOTAL_PAID,0) 
--		AND		OI.UPDATED_FROM = 'P' 
--		AND     OI.ITEM_TRAN_CODE = 'INSF'
--		ORDER BY ISNULL(INSD.CURRENT_TERM,50),ISNULL(INSD.INSTALLMENT_NO,50) ,OI.IDEN_ROW_ID ,OI.SOURCE_EFF_DATE
--	)


	SELECT  @FEES_INS_DUE_AS_ON_DATE = ISNULL( OPI.TOTAL_DUE ,0)	- ISNULl(OPI.TOTAL_PAID,0)
	FROM ACT_CUSTOMER_OPEN_ITEMS OPI with (nolock)    
	WHERE OPI.IDEN_ROW_ID = 
	(
		SELECT	 TOP 1 OI.IDEN_ROW_ID
		FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)
		INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD WITH(NOLOCK) 
		ON OI.INSTALLMENT_ROW_ID  = INSD.ROW_ID
		WHERE	OI.CUSTOMER_ID= @CUSTOMER_ID
		AND		OI.POLICY_ID= @POLICY_ID
		AND		ISNULL(OI.TOTAL_DUE,0) <> ISNULL(OI.TOTAL_PAID,0) 
		AND		OI.UPDATED_FROM = 'P' 
		AND     OI.ITEM_TRAN_CODE = 'INSF'
		ORDER BY ISNULL(INSD.CURRENT_TERM,50),ISNULL(INSD.INSTALLMENT_NO,50) ,OI.IDEN_ROW_ID ,OI.SOURCE_EFF_DATE
	)

	                      
	SELECT @TOTAL_PREMIUM_DUE = ISNULL(SUM(ISNULL( OI.TOTAL_DUE ,0)),0)    
			- ISNULL(SUM(ISNULl(OI.TOTAL_PAID,0)),0)    
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)        
	WHERE OI.CUSTOMER_ID = @CUSTOMER_ID                        
	AND OI.POLICY_ID  = @POLICY_ID                        
	AND OI.ITEM_TRAN_CODE_TYPE <> 'FEES'                        
	GROUP BY OI.CUSTOMER_ID , OI.POLICY_ID       
	  
	SET @TOTAL_DUE	=  @TOTAL_PREMIUM_DUE 

	-- If it is first installment do not include service fee in Tota Due    
	-- for successive installments Fess will be billed     
	IF NOT EXISTS (SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS  with (nolock)     
	WHERE CUSTOMER_ID = @CUSTOMER_ID     
	AND POLICY_ID  = @POLICY_ID     
	AND CURRENT_TERM = @CURRENT_TERM    
	AND ISNULL(RELEASED_STATUS,'N') = 'N'    
	AND INSTALLMENT_NO = 1)    
	BEGIN     
		--SET @TOTAL_PREMIUM_DUE = @TOTAL_PREMIUM_DUE + @FEES_INS_DUE_AS_ON_DATE     
		SET @TOTAL_DUE	= @TOTAL_DUE  + @FEES_INS_DUE_AS_ON_DATE     
	END    
	  
	-- select @agency_id = agency_id from ACT_CUSTOMER_OPEN_ITEMS with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
	select @agency_id = agency_id from POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  
	declare @AGENCYCODE varchar(20)  

	select @AGENCYCODE = agency_code from mnt_agency_list with(nolock) where agency_id=@agency_id  

	
	---ITRACK 6367
	--IF MINIMUM DUE IS GREATER THAN TOTAL DUE THAN SET MINIMUM DUE EQUAL TO TOTAL DUE
	DECLARE @MINIMUM_DUE DECIMAL(18,2)
	DECLARE @TOTAL_DUE_POLICY DECIMAL(18,2)

	SET @MINIMUM_DUE = 0
	SET @TOTAL_DUE_POLICY = 0

	SET @MINIMUM_DUE = @PREMIUM_DUE_AS_ON_DATE + @FEES_INS_DUE_AS_ON_DATE + @FEES_DUE_AS_ON_DATE
	SET @TOTAL_DUE_POLICY = @TOTAL_DUE + @FEES_DUE_AS_ON_DATE

	IF(@MINIMUM_DUE > @TOTAL_DUE_POLICY)
		SET @MINIMUM_DUE = @TOTAL_DUE_POLICY
	
	--END ITRACK 6367

	SELECT  
			--@PREMIUM_DUE_AS_ON_DATE + @FEES_INS_DUE_AS_ON_DATE + @FEES_DUE_AS_ON_DATE AS MINIMUM_DUE ,     
			--@TOTAL_DUE + @FEES_DUE_AS_ON_DATE  AS TOTAL_DUE  ,
			@MINIMUM_DUE AS MINIMUM_DUE,
			@TOTAL_DUE_POLICY AS TOTAL_DUE,
			@AGENCY_ID AS AGENCY_ID,  
			@AGENCYCODE as AGENCYCODE ,   
			@PREMIUM_DUE_AS_ON_DATE AS PREM ,  @FEES_DUE_AS_ON_DATE AS FEE , @FEES_INS_DUE_AS_ON_DATE as FIRST_INS_FEE,  
			@TOTAL_PREMIUM_DUE AS TOTAL_PREMIUM_DUE
END    

--go 
------
--exec Proc_GetTotalAndMinimumDue     1955 , 2 ,4, null
------
--rollback tran 








GO

