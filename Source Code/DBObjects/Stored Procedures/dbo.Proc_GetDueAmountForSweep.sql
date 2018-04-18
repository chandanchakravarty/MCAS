IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDueAmountForSweep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDueAmountForSweep]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc dbo.Proc_GetDueAmountForSweep
--go 
/*----------------------------------------------------------      
Proc Name          : dbo.Proc_GetDueAmountForSweep
Created by         : Ravindra Gupta	       
Date               : 11/18/2009      
Purpose            : To fetch Minimum for EFT/Credit Card sweep
Revison History    :      
Used In            : Wolverine        
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/     
-- drop proc dbo.[Proc_GetDueAmountForSweep]    
CREATE  PROCEDURE [dbo].[Proc_GetDueAmountForSweep]    
(      
		@CUSTOMER_ID	Int ,    
		@POLICY_ID		Int ,
		@CURRENT_TERM	Int ,
		@AS_ON_DATE		Datetime, 
		@SWEEP_AMOUNT	Decimal(18,2)   out 
)      
AS     
BEGIN    
	    
	DECLARE @AMOUNT_DUE_AS_ON_DATE		Decimal(18,2),    
			@INF_FEES_DUE_AS_ON_DATE    Decimal(18,2) , 
			@MINIMUM_DUE				Decimal(18,2),    
			@TOTAL_DUE					Decimal(18,2) , 
			@TOTAL_PREMIUM_DUE			Decimal(18,2)

	    
	-- Calculate Amount dues as on date (Other than installment fees)
	SELECT @AMOUNT_DUE_AS_ON_DATE = ISNULL(SUM(ISNULL( OI.TOTAL_DUE ,0)),0)    
			- ISNULL(SUM(ISNULl(OI.TOTAL_PAID,0)),0)                        
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)                        
	WHERE  CAST(CONVERT(VARCHAR, ISNULL(OI.SWEEP_DATE,OI.DUE_DATE ) ,101) AS Datetime) <= @AS_ON_DATE                        
	AND OI.CUSTOMER_ID = @CUSTOMER_ID                        
	AND OI.POLICY_ID  = @POLICY_ID                        
	AND ( OI.ITEM_TRAN_CODE <> 'INSF'  OR UPDATED_FROM = 'F' ) 
	

	SELECT  @INF_FEES_DUE_AS_ON_DATE = ISNULL( OPI.TOTAL_DUE ,0)	- ISNULl(OPI.TOTAL_PAID,0)
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

	                      
	SELECT @TOTAL_PREMIUM_DUE = ISNULL(SUM(ISNULL( OI.TOTAL_DUE ,0)),0) - ISNULL(SUM(ISNULl(OI.TOTAL_PAID,0)),0)    
	FROM ACT_CUSTOMER_OPEN_ITEMS OI with (nolock)        
	WHERE OI.CUSTOMER_ID = @CUSTOMER_ID                        
	AND OI.POLICY_ID  = @POLICY_ID                        
	AND ( OI.ITEM_TRAN_CODE_TYPE <> 'FEES' OR UPDATED_FROM =  'F'   )
	GROUP BY OI.CUSTOMER_ID , OI.POLICY_ID       
	  
	SET @TOTAL_DUE	=  @TOTAL_PREMIUM_DUE 

	-- If it is first installment do not include service fee in Tota Due  for successive installments Fess will be billed     
	IF NOT EXISTS 
			(
				SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS  with (nolock)     
				WHERE CUSTOMER_ID = @CUSTOMER_ID    AND POLICY_ID  = @POLICY_ID     AND CURRENT_TERM = @CURRENT_TERM    
				AND ISNULL(RELEASED_STATUS,'N') = 'N'    AND INSTALLMENT_NO = 1
			)    
	BEGIN     
		SET @TOTAL_DUE	= @TOTAL_DUE  + ISNULL(@INF_FEES_DUE_AS_ON_DATE,0)     
	END    
	  
	SET @MINIMUM_DUE = @AMOUNT_DUE_AS_ON_DATE + ISNULL(@INF_FEES_DUE_AS_ON_DATE,0)


	IF(@MINIMUM_DUE > @TOTAL_DUE)
		SET @MINIMUM_DUE = @TOTAL_DUE
	
	
	SET @SWEEP_AMOUNT = @MINIMUM_DUE
END    

--go 
--
--DECLARE @S Decimal(18,2) 
--
--exec Proc_GetDueAmountForSweep     1958 , 2 ,1, '11/19/2009', @S Out
--SELECT @S
--
--rollback tran 

GO

