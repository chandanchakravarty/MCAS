IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReconcileInstallments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReconcileInstallments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc dbo.Proc_ReconcileInstallments
--go 
 /*----------------------------------------------------------  
Proc Name       : Proc_ReconcileInstallments
Created by      : Ravindra 
Date            : 04-10-2008
Purpose      	: Reconciles negative items at endorsement with postive item with in installment
Revison History :  
Modified By		: 
Modified Date 	: 
Purpose			: 
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       --------------------------------*/  
-- drop proc dbo.Proc_ReconcileInstallments
CREATE PROC [dbo].[Proc_ReconcileInstallments]
(
		@CUSTOMER_ID		Int,
		@POLICY_ID			Int,
		@CURRENT_TERM		Int,
		@IS_FULLPAY			Char(1)
) 
AS 
BEGIN 

CREATE TABLE #TEMP_RECON 
(
	ITEM_REFRENCE_ID Int,
	RECON_AMOUNT	 Decimal(18,2)
)

CREATE TABLE #ITEMS_TO_RECON  
(  
	[IDEN_ROW_ID] [int] IDENTITY(1,1) NOT NULL ,       
	POSITIVE_ITEM_ID int,  
	TOTAL_PAID DECIMAL(18,2),  
	TOTAL_DUE DECIMAL(18,2)  
)  

DECLARE @IDEN_COL				Int,
		@INS_NO					Int,
		@NEGATIVE_ITEM_ID		Int	,
		@NEGATIVE_AMOUNT		Decimal(18,2),
		@INSTALL_OPEN_ITEM_ID	Int ,
		@RISK_ID				Int , 
		@RISK_TYPE				Varchar(20),
		@POLICY_VERSION_ID		Int


CREATE TABLE #NEGATIVE_ITEMS
(
	IDEN_COL		Int Identity(1,1),
	INSTALLMENT_NO	Int,
	OPEN_ITEM_ID	Int ,
	AMOUNT			DECIMAL(18,2), 
	RISK_ID			Int, 
	RISK_TYPE		Varchar(10),
	POLICY_VERSION_ID	Int
)


IF(@IS_FULLPAY <> 'Y')
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT , RISK_ID , RISK_TYPE ,POLICY_VERSION_ID   )
	SELECT OI.IDEN_ROW_ID,INSD.INSTALLMENT_NO ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0)
			, OI.RISK_ID , OI.RISK_TYPE , OI.POLICY_VERSION_ID
	FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
		AND INSD.POLICY_ID = @POLICY_ID 
		AND INSD.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY INSD.INSTALLMENT_NO
END
ELSE
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT, RISK_ID , RISK_TYPE ,POLICY_VERSION_ID )
	SELECT OI.IDEN_ROW_ID, 1 ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0) , OI.RISK_ID , OI.RISK_TYPE , 
			OI.POLICY_VERSION_ID
	FROM ACT_CUSTOMER_OPEN_ITEMS OI
	WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
		AND OI.POLICY_ID = @POLICY_ID 
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	
		AND OI.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY OI.IDEN_ROW_ID
END


SET @IDEN_COL = 1
	
WHILE (1=1) 
BEGIN 

		IF NOT EXISTS(SELECT IDEN_COL FROM #NEGATIVE_ITEMS WHERE IDEN_COL = @IDEN_COL)
		BEGIN 
			BREAK
		END	
		SELECT	@INS_NO					= INSTALLMENT_NO,
				@NEGATIVE_ITEM_ID		= OPEN_ITEM_ID ,
				@NEGATIVE_AMOUNT		= AMOUNT,
				@RISK_ID				= RISK_ID , 
				@RISK_TYPE				= RISK_TYPE ,
				@POLICY_VERSION_ID		= POLICY_VERSION_ID
		FROM #NEGATIVE_ITEMS 
		WHERE IDEN_COL = @IDEN_COL
		
			
		TRUNCATE TABLE #ITEMS_TO_RECON
			
		IF(@IS_FULLPAY <> 'Y')
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
			INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
				ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
				AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
			AND INSD.POLICY_ID = @POLICY_ID 
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND INSD.INSTALLMENT_NO	 =  @INS_NO
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			AND INSD.POLICY_VERSION_ID = @POLICY_VERSION_ID


			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
			INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
				ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
				AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
			AND INSD.POLICY_ID = @POLICY_ID 
			AND INSD.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND INSD.INSTALLMENT_NO	 =  @INS_NO
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			AND INSD.POLICY_VERSION_ID <> @POLICY_VERSION_ID
		END
		ELSE
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM  ACT_CUSTOMER_OPEN_ITEMS OI
			WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
			AND OI.POLICY_ID = @POLICY_ID 
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			AND OI.POLICY_VERSION_ID = @POLICY_VERSION_ID

			
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM  ACT_CUSTOMER_OPEN_ITEMS OI
			WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
			AND OI.POLICY_ID = @POLICY_ID 
			AND OI.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			AND OI.POLICY_VERSION_ID <> @POLICY_VERSION_ID
		END

		DECLARE @IDEN_ROW_ID Int ,
				@RECON_AMOUNT Decimal(18,2),
				@POSITIVE_ITEM_ID Int,
				@TOTAL_PAID_TO_UPDATE Decimal(18,2) ,
				@RECONCILE_TO_UPDATE Decimal(18,2) ,
				@NEGATIVE_APPLIED	Decimal(18,2) 

		SET @NEGATIVE_APPLIED = 0 
		SET @IDEN_ROW_ID  = 1 
		WHILE(1=1) 
		BEGIN 
				IF NOT EXISTS ( SELECT @IDEN_ROW_ID FROM #ITEMS_TO_RECON WHERE IDEN_ROW_ID = @IDEN_ROW_ID) 
				BEGIN 
					BREAK
				END
				
				IF (@NEGATIVE_AMOUNT = 0)  
					break  
	     
				SELECT   
				@RECON_AMOUNT = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID ,0),  
				@POSITIVE_ITEM_ID = POSITIVE_ITEM_ID  
				FROM #ITEMS_TO_RECON  
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID   


				IF @RECON_AMOUNT  >= @NEGATIVE_AMOUNT   * -1
				BEGIN  
 					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_PAID,0) + (@NEGATIVE_AMOUNT * -1),  
					@RECONCILE_TO_UPDATE = @NEGATIVE_AMOUNT * -1  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  
					
					SET @NEGATIVE_APPLIED =  @NEGATIVE_APPLIED +  @NEGATIVE_AMOUNT
					SET @NEGATIVE_AMOUNT = 0  
				END  
				ELSE  
				BEGIN  
					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_DUE,0),  
					@RECONCILE_TO_UPDATE = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0)  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  

					SET @NEGATIVE_APPLIED = @NEGATIVE_APPLIED - @RECON_AMOUNT
					SET @NEGATIVE_AMOUNT = (@NEGATIVE_AMOUNT + @RECON_AMOUNT)   
				END  
			
				UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = @TOTAL_PAID_TO_UPDATE 
				WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  	

				INSERT INTO #TEMP_RECON 
					( ITEM_REFRENCE_ID ,RECON_AMOUNT )
				VALUES (@POSITIVE_ITEM_ID , @RECONCILE_TO_UPDATE )

				SET @IDEN_ROW_ID = @IDEN_ROW_ID + 1 
			
		END
			
		
		UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = ISNULL(TOTAL_PAID,0) + @NEGATIVE_APPLIED
			WHERE IDEN_ROW_ID = @NEGATIVE_ITEM_ID  

		INSERT INTO #TEMP_RECON 
			( ITEM_REFRENCE_ID ,RECON_AMOUNT )
		VALUES (@NEGATIVE_ITEM_ID , @NEGATIVE_APPLIED )
	
		SET @IDEN_COL = @IDEN_COL	+ 1 
END

-- IF there is still balance on negative items reconcile with item in next installment for same risk 

TRUNCATE TABLE #NEGATIVE_ITEMS 
TRUNCATE TABLE #ITEMS_TO_RECON 
 
IF(@IS_FULLPAY <> 'Y')
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT , RISK_ID , RISK_TYPE  )
	SELECT OI.IDEN_ROW_ID,INSD.INSTALLMENT_NO ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0)
			, OI.RISK_ID , OI.RISK_TYPE 
	FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
		AND INSD.POLICY_ID = @POLICY_ID 
		AND INSD.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY INSD.INSTALLMENT_NO
END
ELSE
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT, RISK_ID , RISK_TYPE  )
	SELECT OI.IDEN_ROW_ID, 1 ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0) , OI.RISK_ID , OI.RISK_TYPE 
	FROM ACT_CUSTOMER_OPEN_ITEMS OI
	WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
		AND OI.POLICY_ID = @POLICY_ID 
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	
		AND OI.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY OI.IDEN_ROW_ID
END

SET @IDEN_COL = 1
	
WHILE (1=1) 
BEGIN 

		IF NOT EXISTS(SELECT IDEN_COL FROM #NEGATIVE_ITEMS WHERE IDEN_COL = @IDEN_COL)
		BEGIN 
			BREAK
		END	
		SELECT	@INS_NO					= INSTALLMENT_NO,
				@NEGATIVE_ITEM_ID		= OPEN_ITEM_ID ,
				@NEGATIVE_AMOUNT		= AMOUNT,
				@RISK_ID				= RISK_ID , 
				@RISK_TYPE				= RISK_TYPE 
		FROM #NEGATIVE_ITEMS 
		WHERE IDEN_COL = @IDEN_COL
		
			
		TRUNCATE TABLE #ITEMS_TO_RECON
			
		IF(@IS_FULLPAY <> 'Y')
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
			INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
				ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
				AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
			AND INSD.POLICY_ID = @POLICY_ID 
			AND INSD.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			AND INSD.INSTALLMENT_NO >  @INS_NO
			ORDER BY INSD.INSTALLMENT_NO	  
		END
		ELSE
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM  ACT_CUSTOMER_OPEN_ITEMS OI
			WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
			AND OI.POLICY_ID = @POLICY_ID 
			AND OI.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
		END

		SET @RECON_AMOUNT  = 0 
		SET @POSITIVE_ITEM_ID  = 0 
		SET @TOTAL_PAID_TO_UPDATE  = 0 
		SET @RECONCILE_TO_UPDATE  = 0 
		SET @NEGATIVE_APPLIED	 = 0  

		SET @NEGATIVE_APPLIED = 0 
		SET @IDEN_ROW_ID  = 1 
		WHILE(1=1) 
		BEGIN 
				IF NOT EXISTS ( SELECT @IDEN_ROW_ID FROM #ITEMS_TO_RECON WHERE IDEN_ROW_ID = @IDEN_ROW_ID) 
				BEGIN 
					BREAK
				END
				
				IF (@NEGATIVE_AMOUNT = 0)  
					break  
	     
				SELECT   
				@RECON_AMOUNT = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID ,0),  
				@POSITIVE_ITEM_ID = POSITIVE_ITEM_ID  
				FROM #ITEMS_TO_RECON  
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID   


				IF @RECON_AMOUNT  >= @NEGATIVE_AMOUNT   * -1
				BEGIN  
 					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_PAID,0) + (@NEGATIVE_AMOUNT * -1),  
					@RECONCILE_TO_UPDATE = @NEGATIVE_AMOUNT * -1  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  
					
					SET @NEGATIVE_APPLIED =  @NEGATIVE_APPLIED +  @NEGATIVE_AMOUNT
					SET @NEGATIVE_AMOUNT = 0  
				END  
				ELSE  
				BEGIN  
					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_DUE,0),  
					@RECONCILE_TO_UPDATE = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0)  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  

					SET @NEGATIVE_APPLIED = @NEGATIVE_APPLIED - @RECON_AMOUNT
					SET @NEGATIVE_AMOUNT = (@NEGATIVE_AMOUNT + @RECON_AMOUNT)   
				END  
			
				UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = @TOTAL_PAID_TO_UPDATE 
				WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  	

				INSERT INTO #TEMP_RECON 
					( ITEM_REFRENCE_ID ,RECON_AMOUNT )
				VALUES (@POSITIVE_ITEM_ID , @RECONCILE_TO_UPDATE )

				SET @IDEN_ROW_ID = @IDEN_ROW_ID + 1 
			
		END
			
		
		UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = ISNULL(TOTAL_PAID,0) + @NEGATIVE_APPLIED
			WHERE IDEN_ROW_ID = @NEGATIVE_ITEM_ID  

		INSERT INTO #TEMP_RECON 
			( ITEM_REFRENCE_ID ,RECON_AMOUNT )
		VALUES (@NEGATIVE_ITEM_ID , @NEGATIVE_APPLIED )
	
		SET @IDEN_COL = @IDEN_COL	+ 1 
END

-- End reconciling balance 


--RAvindra(09-15-2009): Do not reconcile with installment lesser than current installmnet, previous installmnets 
-- might be released/billed, if applied with will create issue while changing billing plan as in 6322 and in 
-- EFT/Billing as in 6407
/*
--If balace left reconcie with previous items of same risk


TRUNCATE TABLE #NEGATIVE_ITEMS 
TRUNCATE TABLE #ITEMS_TO_RECON 
 
IF(@IS_FULLPAY <> 'Y')
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT , RISK_ID , RISK_TYPE  )
	SELECT OI.IDEN_ROW_ID,INSD.INSTALLMENT_NO ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0)
			, OI.RISK_ID , OI.RISK_TYPE 
	FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
		AND INSD.POLICY_ID = @POLICY_ID 
		AND INSD.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY INSD.INSTALLMENT_NO
END
ELSE
BEGIN 
	INSERT INTO #NEGATIVE_ITEMS
		(OPEN_ITEM_ID , INSTALLMENT_NO , AMOUNT, RISK_ID , RISK_TYPE  )
	SELECT OI.IDEN_ROW_ID, 1 ,ISNULL(OI.TOTAL_DUE,0 ) -  ISNULL(OI.TOTAL_PAID ,0) , OI.RISK_ID , OI.RISK_TYPE 
	FROM ACT_CUSTOMER_OPEN_ITEMS OI
	WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
		AND OI.POLICY_ID = @POLICY_ID 
		--AND OI.ITEM_TRAN_CODE = 'ENDP'
		AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
	
		AND OI.POLICY_VERSION_ID  IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
										AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
		AND ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID ,0)
		AND ISNULL(OI.TOTAL_DUE,0) < 0 
	ORDER BY OI.IDEN_ROW_ID
END

SET @IDEN_COL = 1
	
WHILE (1=1) 
BEGIN 

		IF NOT EXISTS(SELECT IDEN_COL FROM #NEGATIVE_ITEMS WHERE IDEN_COL = @IDEN_COL)
		BEGIN 
			BREAK
		END	
		SELECT	@INS_NO					= INSTALLMENT_NO,
				@NEGATIVE_ITEM_ID		= OPEN_ITEM_ID ,
				@NEGATIVE_AMOUNT		= AMOUNT,
				@RISK_ID				= RISK_ID , 
				@RISK_TYPE				= RISK_TYPE 
		FROM #NEGATIVE_ITEMS 
		WHERE IDEN_COL = @IDEN_COL
		
			
		TRUNCATE TABLE #ITEMS_TO_RECON
			
		IF(@IS_FULLPAY <> 'Y')
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM ACT_POLICY_INSTALLMENT_DETAILS INSD
			INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
				ON OI.INSTALLMENT_ROW_ID = INSD.ROW_ID
				AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			WHERE   INSD.CUSTOMER_ID = @CUSTOMER_ID 
			AND INSD.POLICY_ID = @POLICY_ID 
			AND INSD.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
			ORDER BY INSD.INSTALLMENT_NO	  
		END
		ELSE
		BEGIN 
			INSERT INTO #ITEMS_TO_RECON
			(  
				POSITIVE_ITEM_ID,  TOTAL_DUE   , TOTAL_PAID  
			)  
			SELECT OI.IDEN_ROW_ID,ISNULL(OI.TOTAL_DUE,0 ) ,  ISNULL(OI.TOTAL_PAID ,0)
			FROM  ACT_CUSTOMER_OPEN_ITEMS OI
			WHERE   OI.CUSTOMER_ID = @CUSTOMER_ID 
			AND OI.POLICY_ID = @POLICY_ID 
			AND OI.POLICY_VERSION_ID  IN  (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID 
											AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM)  
			AND ISNULL(OI.TOTAL_DUE,0)  - ISNULL(OI.TOTAL_PAID ,0) > 0 
			AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
			AND OI.RISK_ID = @RISK_ID 
			AND OI.RISK_TYPE = @RISK_TYPE 
		END

		SET @RECON_AMOUNT  = 0 
		SET @POSITIVE_ITEM_ID  = 0 
		SET @TOTAL_PAID_TO_UPDATE  = 0 
		SET @RECONCILE_TO_UPDATE  = 0 
		SET @NEGATIVE_APPLIED	 = 0  

		SET @NEGATIVE_APPLIED = 0 
		SET @IDEN_ROW_ID  = 1 
		WHILE(1=1) 
		BEGIN 
				IF NOT EXISTS ( SELECT @IDEN_ROW_ID FROM #ITEMS_TO_RECON WHERE IDEN_ROW_ID = @IDEN_ROW_ID) 
				BEGIN 
					BREAK
				END
				
				IF (@NEGATIVE_AMOUNT = 0)  
					break  
	     
				SELECT   
				@RECON_AMOUNT = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID ,0),  
				@POSITIVE_ITEM_ID = POSITIVE_ITEM_ID  
				FROM #ITEMS_TO_RECON  
				WHERE IDEN_ROW_ID = @IDEN_ROW_ID   


				IF @RECON_AMOUNT  >= @NEGATIVE_AMOUNT   * -1
				BEGIN  
 					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_PAID,0) + (@NEGATIVE_AMOUNT * -1),  
					@RECONCILE_TO_UPDATE = @NEGATIVE_AMOUNT * -1  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  
					
					SET @NEGATIVE_APPLIED =  @NEGATIVE_APPLIED +  @NEGATIVE_AMOUNT
					SET @NEGATIVE_AMOUNT = 0  
				END  
				ELSE  
				BEGIN  
					SELECT   
					@TOTAL_PAID_TO_UPDATE = ISNULL(TOTAL_DUE,0),  
					@RECONCILE_TO_UPDATE = ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0)  
					FROM ACT_CUSTOMER_OPEN_ITEMS  
					WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  

					SET @NEGATIVE_APPLIED = @NEGATIVE_APPLIED - @RECON_AMOUNT
					SET @NEGATIVE_AMOUNT = (@NEGATIVE_AMOUNT + @RECON_AMOUNT)   
				END  
			
				UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = @TOTAL_PAID_TO_UPDATE 
				WHERE IDEN_ROW_ID = @POSITIVE_ITEM_ID  	

				INSERT INTO #TEMP_RECON 
					( ITEM_REFRENCE_ID ,RECON_AMOUNT )
				VALUES (@POSITIVE_ITEM_ID , @RECONCILE_TO_UPDATE )

				SET @IDEN_ROW_ID = @IDEN_ROW_ID + 1 
			
		END
			
		
		UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_PAID = ISNULL(TOTAL_PAID,0) + @NEGATIVE_APPLIED
			WHERE IDEN_ROW_ID = @NEGATIVE_ITEM_ID  

		INSERT INTO #TEMP_RECON 
			( ITEM_REFRENCE_ID ,RECON_AMOUNT )
		VALUES (@NEGATIVE_ITEM_ID , @NEGATIVE_APPLIED )
	
		SET @IDEN_COL = @IDEN_COL	+ 1 
END



--end final recocilliation
*/

IF EXISTS ( SELECT ITEM_REFRENCE_ID FROM #TEMP_RECON )
BEGIN 
	-- Create Reconciliation Details for discount adjustment
	DECLARE @GROUP_ID INT  
	SELECT @GROUP_ID = ISNULL(MAX(GROUP_ID),0) + 1 FROM ACT_RECONCILIATION_GROUPS  
		
	INSERT INTO ACT_RECONCILIATION_GROUPS  
	(  
	GROUP_ID, RECON_ENTITY_ID, RECON_ENTITY_TYPE, IS_COMMITTED,  
	DATE_COMMITTED ,COMMITTED_BY,  
	IS_ACTIVE, CREATED_BY, CREATED_DATETIME, MODIFIED_BY, LAST_UPDATED_DATETIME,  
	CD_LINE_ITEM_ID  
	)  
	VALUES(
	@GROUP_ID, @CUSTOMER_ID, 'CUST', 'Y',  
	GETDATE(), NULL,  
	'Y', NULL, GETDATE(), NULL, NULL,  
	NULL
	)

	INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS  
	(  
	GROUP_ID, ITEM_TYPE, ITEM_REFERENCE_ID,   
	SUB_LEDGER_TYPE, RECON_AMOUNT, NOTE, DIV_ID, DEPT_ID, PC_ID  
	)  
	SELECT   
	@GROUP_ID, 'CUST', ITEM_REFRENCE_ID ,  
	NULL, RECON_AMOUNT , NULL, OI.DIV_ID, OI.DEPT_ID, OI.PC_ID  
	FROM   
	#TEMP_RECON TMP
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI 
	ON TMP.ITEM_REFRENCE_ID = OI.IDEN_ROW_ID

END

DROP TABLE #TEMP_RECON
DROP TABLE #ITEMS_TO_RECON
DROP TABLE #NEGATIVE_ITEMS

END


--go 
--
--UPDATE ACT_CUSTOMER_OPEN_ITEMS SET TOTAL_DUE = TOTAL_DUE - 600 WHERE IDEN_ROW_ID = 68819 
--
--exec Proc_ReconcileInstallments 1432 , 89 ,1 , 'N' 
--
--SELECT A.IDEN_ROW_ID,A.TRANS_DESC,A.TOTAL_DUE,A.TOTAL_PAID,A.DUE_DATE,A.POSTING_DATE,
--A.SOURCE_EFF_DATE,MNT.LOOKUP_VALUE_DESC as PaymentMode, 
--A.ITEM_TRAN_CODE_TYPE,A.ITEM_TRAN_CODE,
--ISNULL(B.INSTALLMENT_NO,50) INS_NO,B.INSTALLMENT_NO	 AS INS_NO,B.RELEASED_STATUS,A.RISK_ID,A.RISK_TYPE,
--A.* FROM ACT_CUSTOMER_OPEN_ITEMS A WITH(NOLOCK)
--left JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK) ON A.INSTALLMENT_ROW_ID  = B.ROW_ID
--LEFT JOIN MNT_LOOKUP_VALUES MNT ON 
--B.PAYMENT_MODE = MNT.LOOKUP_UNIQUE_ID 
--WHERE A.CUSTOMER_ID= 1432
--AND A.POLICY_ID = 89
--ORDER BY  ISNULL(B.CURRENT_TERM,50),ISNULL(B.INSTALLMENT_NO,50) ,A.IDEN_ROW_ID ,A.SOURCE_EFF_DATE,A.UPDATED_FROM
--
--
--rollback tran 








GO

