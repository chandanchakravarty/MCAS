IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerRecordsToReconcile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerRecordsToReconcile]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc  dbo.Proc_GetCustomerRecordsToReconcile
--go 
/********************************************************************
Created By 		: Vijay Joshi
Created Date	: 7, March 2006
Purpose			: To get the records, which should be reconciled by any deposit
*********************************************************************/
-- drop proc  dbo.Proc_GetCustomerRecordsToReconcile
CREATE PROC [dbo].[Proc_GetCustomerRecordsToReconcile]
(
	@CD_LINE_ITEM_ID	INT ,
	@CALLED_FROM 		SmallInt = NULL, -- 1 for Deposit 2 for Policy Process
	@INSF_DETAILS		VARCHAR(800) OUTPUT	--Added for saving INSF fee details.
)
AS
BEGIN


DECLARE  @RECORDS_TO_RECONCILE TABLE
(
	IDEN_ROW_ID			INT,
	TOTAL_DUE			DECIMAL(18,2),
	RECONCILE_AMOUNT	DECIMAL(18,2),
	UPDATED_FROM			Char(1), 
	ITEM_TRAN_CODE		Varchar(5) ,
	ITEM_TRAN_CODE_TYPE	Varchar(5)
)

DECLARE @INSTALL_PLAN_ID int, @RECEIPT_AMOUNT decimal(20,2), @POLICY_ID INT, @POLICY_VERSION_ID INT, @CUSTOMER_ID INT
DECLARE @ITEM_TRAN_CODE_INSTALLMENT_FEES VARCHAR(5)

--Initialising the values
SELECT @ITEM_TRAN_CODE_INSTALLMENT_FEES = 'INSF'


--Variables for retreiving values from cursor
Declare @IDEN_ROW_ID			Int,
		@AMOUNT_TO_APPLY		DECIMAL(20,2), 
		@TOTAL_PAID				DECIMAL(20,2),
		@RECONCILED				BIT, 
		@ITEM_TRAN_CODE			VARCHAR(5), 
		@SOURCE_EFF_DATE		DATETIME,
		@INTALLMENT_RECONCILED	BIT,
		@TOTAL_DUE				decimal(20,2),
		@UPDATED_FROM			Char(1), 
		@ITEM_TRAN_CODE_TYPE	Varchar(5)

IF(@CALLED_FROM = 1 )
BEGIN 
	SELECT 
		@INSTALL_PLAN_ID 	= PCPL.INSTALL_PLAN_ID,
		@RECEIPT_AMOUNT 	= RECEIPT_AMOUNT,
		@POLICY_ID		= CDLI.POLICY_ID,
		@POLICY_VERSION_ID	= CDLI.POLICY_VERSION_ID,
		@CUSTOMER_ID		= CDLI.CUSTOMER_ID
	FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI
	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON CDLI.POLICY_ID = PCPL.POLICY_ID AND CDLI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND CDLI.CUSTOMER_ID = PCPL.CUSTOMER_ID
	WHERE CDLI.CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
END
ELSE
BEGIN 
	SELECT 
		@INSTALL_PLAN_ID 	= PCPL.INSTALL_PLAN_ID,
		@RECEIPT_AMOUNT 	= (ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)) * -1 ,
		@POLICY_ID			= CDLI.POLICY_ID,
		@POLICY_VERSION_ID	= CDLI.POLICY_VERSION_ID,
		@CUSTOMER_ID		= CDLI.CUSTOMER_ID
	FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON OI.CUSTOMER_ID = CDLI.CUSTOMER_ID 
	AND OI.POLICY_ID = CDLI.POLICY_ID 
	AND OI.UPDATED_FROM = 'D' 
	AND OI.SOURCE_ROW_ID = CDLI.CD_LINE_ITEM_ID
	LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL 
	ON CDLI.POLICY_ID = PCPL.POLICY_ID AND CDLI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID AND CDLI.CUSTOMER_ID = PCPL.CUSTOMER_ID
	WHERE CDLI.CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
END


--Ravindra(03-03-2008) if No Positive items pending exit reconciliation
--Ravindra(02-02-2009) : Reconciliation should be there if postive items exists balance could be negative
--IF
--	(
--	SELECT SUM(ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID ,0) )
--	FROM ACT_CUSTOMER_OPEN_ITEMS 
--	WHERE POLICY_ID = @POLICY_ID 
--	AND CUSTOMER_ID = @CUSTOMER_ID
--	AND UPDATED_FROM <> 'D'
--	) <= 0 
--BEGIN 
--	RETURN
--END

IF NOT EXISTS ( 
				SELECT IDEN_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS 
				WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
				AND ISNULL(TOTAL_DUE,0) > 0 AND ISNULL(TOTAL_DUE,0) <> ISNULL(TOTAL_PAID,0) 
				)
BEGIN 
	RETURN
END

/*CODE FOR CHECKING FESS AND APPLY OF THAT FESS WILL COME HERE*/	

DECLARE @FeesOpenItems TABLE 
(
	IDEN_COL				Int Identity(1,1),
	IDEN_ROW_ID				Int, 
	TOTAL_DUE				Decimal(18,2), 
	TOTAL_PAID				Decimal(18,2), 
	SOURCE_EFF_DATE			Datetime, 
	ITEM_TRAN_CODE			VArchar(10),
	ITEM_TRAN_CODE_TYPE		VArchar(10),
	UPDATED_FROM			Char(1)
)

INSERT INTO @FeesOpenItems
SELECT 
	ISNULL(IDEN_ROW_ID,0), ISNULL(TOTAL_DUE,0), ISNULL(TOTAL_PAID ,0), 
	SOURCE_EFF_DATE, ITEM_TRAN_CODE, ITEM_TRAN_CODE_TYPE , UPDATED_FROM 
	FROM ACT_CUSTOMER_OPEN_ITEMS 
	WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
	AND ((ISNULL(TOTAL_PAID,0) < ISNULL(TOTAL_DUE,0)) or (ISNULL(TOTAL_PAID,0) > ISNULL(TOTAL_DUE,0)) )
	--AND NOT (UPDATED_FROM = 'D' OR  ISNULL(SOURCE_ROW_ID,0) = @CD_LINE_ITEM_ID)
	AND UPDATED_FROM <> 'D'
	AND ITEM_TRAN_CODE_TYPE = 'FEES'
	AND ITEM_TRAN_CODE <> 'INSF'
	ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID

DECLARE @IDEN_COL Int 
SET @IDEN_COL = 1 

SET @AMOUNT_TO_APPLY = @RECEIPT_AMOUNT	--This much is the amount we need to reconcile

--OPEN Cur_FeesOpenItems
--Looping in each Fees open items and reconcilling each item one by one
WHILE 1=1
BEGIN

	IF NOT EXISTS (SELECT IDEN_COL FROM @FeesOpenItems WHERE IDEN_COL = @IDEN_COL ) 
	BEGIN 
		BREAK
	END
		
	SELECT 
	@IDEN_ROW_ID			= IDEN_ROW_ID, 
	@TOTAL_DUE				= TOTAL_DUE, 
	@TOTAL_PAID				= TOTAL_PAID, 
	@SOURCE_EFF_DATE		= SOURCE_EFF_DATE, 
	@ITEM_TRAN_CODE			= ITEM_TRAN_CODE,
	@ITEM_TRAN_CODE_TYPE	= ITEM_TRAN_CODE_TYPE , 
	@UPDATED_FROM			= UPDATED_FROM  
	FROM @FeesOpenItems WHERE IDEN_COL = @IDEN_COL 

	SET @IDEN_COL = @IDEN_COL + 1 

	IF @TOTAL_DUE - @TOTAL_PAID < @AMOUNT_TO_APPLY
	BEGIN
		INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT , 
						ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
		VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @TOTAL_DUE - @TOTAL_PAID, 
						@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )	
		--Updating the amount to reconcile variable			
		SET @AMOUNT_TO_APPLY = @AMOUNT_TO_APPLY - (ISNULL(@TOTAL_DUE,0) - ISNULL(@TOTAL_PAID,0))		
			
	END
	ELSE
	BEGIN
		INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT,
						ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
		VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @AMOUNT_TO_APPLY,
						@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )			
		
		--All amount has been reconcile, hence setting it to 0
		SET @AMOUNT_TO_APPLY = 0
		--Whole amount has been applied, hence exiting from while loop
		BREAK
	END
END		

-- Balance Reciept Amount after applying to fees 
IF ( @AMOUNT_TO_APPLY <> 0)
BEGIN 
	SET  @RECEIPT_AMOUNT = @AMOUNT_TO_APPLY 
	
	/*********************************************/
	--Checking whether the policy is installment policy or not
	if ISNULL(@INSTALL_PLAN_ID,0) = 0
	BEGIN
	
		--Not a installment policy
		SET @TOTAL_DUE = 0
		
		IF EXISTS(SELECT UPDATED_FROM 
					FROM ACT_CUSTOMER_OPEN_ITEMS 
					WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
					AND IsNull(TOTAL_PAID,0) < ISNULL(TOTAL_DUE,0))
		BEGIN
			
			/*Some pending amount exist on this policy, hence applying that amount*/
			SELECT @TOTAL_DUE = ISNULL(SUM(ISNULL(TOTAL_DUE, 0) - ISNULL(TOTAL_PAID,0)),0)
			FROM ACT_CUSTOMER_OPEN_ITEMS 
			WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
				AND UPDATED_FROM <> 'D' 
				--AND ISNULL(SOURCE_ROW_ID,0) <> @CD_LINE_ITEM_ID
		
			
			if @TOTAL_DUE <= @RECEIPT_AMOUNT
			BEGIN
				
				--Applying the whole amount
				INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT , 
								ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
				SELECT IDEN_ROW_ID, TOTAL_DUE, ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0) , 
								ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM 
				FROM ACT_CUSTOMER_OPEN_ITEMS
				WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
				AND UPDATED_FROM <> 'D'

			END
			ELSE
			BEGIN

				DECLARE @OpenItems TABLE 
				(
					IDEN_COL				Int Identity(1,1),
					IDEN_ROW_ID				Int, 
					TOTAL_DUE				Decimal(18,2), 
					TOTAL_PAID				Decimal(18,2), 
					SOURCE_EFF_DATE			Datetime, 
					ITEM_TRAN_CODE			VArchar(10),
					ITEM_TRAN_CODE_TYPE		VArchar(10),
					UPDATED_FROM			Char(1)
				)
				
				INSERT INTO  @OpenItems
				SELECT ISNULL(IDEN_ROW_ID,0), ISNULL(TOTAL_DUE,0), ISNULL(TOTAL_PAID ,0), 
						SOURCE_EFF_DATE, ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM 
					FROM ACT_CUSTOMER_OPEN_ITEMS 
					WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
						AND ISNULL(TOTAL_PAID,0) < ISNULL(TOTAL_DUE,0) 
						--AND NOT (UPDATED_FROM = 'D' OR ISNULL(SOURCE_ROW_ID,0) = @CD_LINE_ITEM_ID)
						AND UPDATED_FROM <> 'D'
						ORDER BY SOURCE_EFF_DATE
				
				SET @IDEN_COL = 1

				SET @AMOUNT_TO_APPLY = @RECEIPT_AMOUNT	--This much is the amount we need to reconcile
				
				--Looping in each  open items and reconcilling each item one by one
				while 1=1
				BEGIN
			
					IF NOT EXISTS (SELECT IDEN_COL FROM @OpenItems WHERE IDEN_COL = @IDEN_COL ) 
					BEGIN 
						BREAK
					END

					SELECT 
					@IDEN_ROW_ID			= IDEN_ROW_ID, 
					@TOTAL_DUE				= TOTAL_DUE, 
					@TOTAL_PAID				= TOTAL_PAID, 
					@SOURCE_EFF_DATE		= SOURCE_EFF_DATE, 
					@ITEM_TRAN_CODE			= ITEM_TRAN_CODE,
					@ITEM_TRAN_CODE_TYPE	= ITEM_TRAN_CODE_TYPE , 
					@UPDATED_FROM			= UPDATED_FROM  
					FROM @OpenItems WHERE IDEN_COL = @IDEN_COL 

					SET @IDEN_COL = @IDEN_COL + 1 
				
					if @TOTAL_DUE - @TOTAL_PAID < @AMOUNT_TO_APPLY
					BEGIN
						--Whole amount(total due) should be reconciled
						INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT , 
									ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
						VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @TOTAL_DUE - @TOTAL_PAID ,
									@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )
									
						--Updating the amount to reconcile variable			
						SET @AMOUNT_TO_APPLY = @AMOUNT_TO_APPLY - (ISNULL(@TOTAL_DUE,0) - ISNULL(@TOTAL_PAID,0))
					END
					ELSE
					BEGIN
						--whole remaning amount should be reconciled
						INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT, 
										ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
						VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @AMOUNT_TO_APPLY ,
										@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )
	
						--All amount has been reconcile, hence setting it to 0
						SET @AMOUNT_TO_APPLY = 0
				
						--Whole amount has been applied, hence exiting from while loop
						break
					END
				END		--End while loop
	
			END
		END
	END
	ELSE
	BEGIN --Policy is a installment policy
	
	
		SET @AMOUNT_TO_APPLY = @RECEIPT_AMOUNT	--This much is the amount we need to reconcile

		DECLARE @TOTAL_INS_DUE Decimal(18,2)
		DECLARE @IS_FIRST_INSTALLMENT Bit
		DECLARE @NEXT_DUE_INS_NO Int
		DECLARE @PENDING_INSTALLMENTS SmallInt, 
				@CURRENT_TERM	SmallInt, 
				@DEPOSIT_ITEM_ID		Int

		--Ravindra(02-26-2009): In case if first payment paid instalment in part and then there is another 
		--payment which is paying policy in full. System will not charge installment fees on second payment 
		--because @NEXT_DUE_INS_NO  will be one as per following query. 
		--A fees should be charged because this is second payment on policy
--		SELECT TOP 1 @NEXT_DUE_INS_NO = INSD.INSTALLMENT_NO
--		FROM ACT_CUSTOMER_OPEN_ITEMS OI 
--		INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD 
--		ON OI.INSTALLMENT_ROW_ID  = INSD.ROW_ID 
--		WHERE OI.POLICY_ID = @POLICY_ID AND OI.CUSTOMER_ID = @CUSTOMER_ID
--		AND ISNULL(OI.TOTAL_PAID ,0)  <> ISNULL(OI.TOTAL_DUE,0)
--		ORDER BY INSD.INSTALLMENT_NO

		--Ravindra(08-26-2009): Change logic to consider only lowest unpaid term. 
		--FIx for iTRack 6184

		SELECT @DEPOSIT_ITEM_ID = IDEN_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS WHERE UPDATED_FROM = 'D' 
		AND SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 

		SELECT @CURRENT_TERM = MIN (CURRENT_TERM ) FROM 
		(
			SELECT CPL.CURRENT_TERM ,SUM( ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID , 0) ) AS BALANCE 
			FROM ACT_CUSTOMER_OPEN_ITEMS OI
			INNER JOIN POL_CUSTOMER_POLICY_LIST CPL 
				ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID 
				AND OI.POLICY_ID = CPL.POLICY_ID 
				AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID 
			WHERE
			(
			OI.UPDATED_FROM IN ('C','D','F','J')
			OR	(
					OI.UPDATED_FROM IN ('P') AND OI.ITEM_TRAN_CODE_TYPE = 'PREM'
				)
			)
			AND CPL.POLICY_VERSION_ID >= 
			( 
				SELECT MIN(PPPIN.NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS PPPIN
				WHERE PPPIN.CUSTOMER_ID = CPL.CUSTOMER_ID 
				AND PPPIN.POLICY_ID		= CPL.POLICY_ID 
				AND PROCESS_ID	IN (24,25,18,5)   
				AND PROCESS_STATUS		IN ('PENDING','COMPLETE') -- MIN OF NBS OR RENEWAL
				AND ISNULL(REVERT_BACK,'N') = 'N'
			) 
		
			--RAvindra(11/12/2009) Do not consider this Deposit Item in Calculating Balance

			AND OI.IDEN_ROW_ID <> ISNULL(@DEPOSIT_ITEM_ID ,0) 

			AND CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID 
			GROUP BY CPL.CURRENT_TERM  
			HAVING SUM( ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID , 0) )  > 0 
		)POLICY_BALANCE

		SELECT TOP 1 @NEXT_DUE_INS_NO = INSTALLMENT_NO
		FROM 
		(
			SELECT INSD.INSTALLMENT_NO , SUM(ISNULL(OI.TOTAL_DUE,0)) AS TOTAL_DUE , 
			SUM(ISNULL(OI.TOTAL_PAID,0)) AS TOTAL_PAID
			FROM ACT_CUSTOMER_OPEN_ITEMS OI 
			INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD 
			ON OI.INSTALLMENT_ROW_ID  = INSD.ROW_ID 
			WHERE OI.POLICY_ID = @POLICY_ID AND OI.CUSTOMER_ID = @CUSTOMER_ID
			AND OI.POLICY_VERSION_ID IN 
			(
				SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL 
				WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID 
				AND CPL.CURRENT_TERM = @CURRENT_TERM
			)
			GROUP BY INSD.INSTALLMENT_NO 
		)AS TEMP
		WHERE TOTAL_DUE <> TOTAL_PAID AND TOTAL_PAID = 0 
		ORDER BY INSTALLMENT_NO


		IF ( ISNULL(@NEXT_DUE_INS_NO,100) > 1  ) 
		BEGIN 
			SET @IS_FIRST_INSTALLMENT = 0 
		END			
		ELSE
		BEGIN 
			SET @IS_FIRST_INSTALLMENT = 1
		END


--		SELECT @PENDING_INSTALLMENTS = COUNT(*) 
--		FROM 
--		(
--			SELECT DISTINCT INSD.INSTALLMENT_NO
--			FROM ACT_CUSTOMER_OPEN_ITEMS OI 
--			INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD 
--			ON OI.INSTALLMENT_ROW_ID  = INSD.ROW_ID 
--			WHERE OI.POLICY_ID = @POLICY_ID AND OI.CUSTOMER_ID = @CUSTOMER_ID
--			AND ISNULL(OI.TOTAL_PAID ,0)  <> ISNULL(OI.TOTAL_DUE,0)
--		)PENDING_INSTALLMENTS
--
--		IF (  @PENDING_INSTALLMENTS > 1  ) 
--		BEGIN 
--			SET @IS_LAST_INSTALLMENT = 0 
--		END			
--		ELSE
--		BEGIN 
--			SET @IS_LAST_INSTALLMENT = 1
--		END

		SELECT @TOTAL_INS_DUE = SUM(ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID ,0)) 
		FROM ACT_CUSTOMER_OPEN_ITEMS 
		WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
		--AND NOT (UPDATED_FROM = 'D' OR  ISNULL(SOURCE_ROW_ID,0) = @CD_LINE_ITEM_ID)
		AND UPDATED_FROM <> 'D'
		AND ITEM_TRAN_CODE <> 'NSFF'
		AND ITEM_TRAN_CODE <> 'RENSF'
		AND ITEM_TRAN_CODE <> 'LF'
		AND ITEM_TRAN_CODE <> 'INSF'
		
		-- If Deposit is Paying all installments  in full update all ISF Fees to Zero	
		IF(@TOTAL_INS_DUE <= @AMOUNT_TO_APPLY AND @IS_FIRST_INSTALLMENT =  1 )
		BEGIN 
			SET @INTALLMENT_RECONCILED = 1
		END
		ELSE
		BEGIN 
			SET @INTALLMENT_RECONCILED = 0
		END
	
		DECLARE @InsOpenItems TABLE 
		(
			IDEN_COL				Int Identity(1,1),
			IDEN_ROW_ID				Int, 
			TOTAL_DUE				Decimal(18,2), 
			TOTAL_PAID				Decimal(18,2), 
			SOURCE_EFF_DATE			Datetime, 
			ITEM_TRAN_CODE			VArchar(10),
			ITEM_TRAN_CODE_TYPE		VArchar(10),
			UPDATED_FROM			Char(1)
		)

		INSERT INTO @InsOpenItems
		SELECT 
				ISNULL(IDEN_ROW_ID,0), ISNULL(TOTAL_DUE,0), ISNULL(TOTAL_PAID ,0), 
				SOURCE_EFF_DATE, ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM 
			FROM ACT_CUSTOMER_OPEN_ITEMS 
			WHERE POLICY_ID = @POLICY_ID AND CUSTOMER_ID = @CUSTOMER_ID
			--AND ISNULL(TOTAL_PAID,0) < ISNULL(TOTAL_DUE,0) 
			AND ((ISNULL(TOTAL_PAID,0) < ISNULL(TOTAL_DUE,0)) or (ISNULL(TOTAL_PAID,0) > ISNULL(TOTAL_DUE,0)) )
			--AND NOT (UPDATED_FROM = 'D' OR  ISNULL(SOURCE_ROW_ID,0) = @CD_LINE_ITEM_ID)
			AND UPDATED_FROM <> 'D'
			AND ITEM_TRAN_CODE <> 'NSFF'
			AND ITEM_TRAN_CODE <> 'RENSF'
			AND ITEM_TRAN_CODE <> 'LF'
			ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID

		SET @IDEN_COL = 1 

		--Looping in each installments open items and reconcilling each item one by one
		while 1=1
		BEGIN
			IF NOT EXISTS (SELECT IDEN_COL FROM @InsOpenItems WHERE IDEN_COL = @IDEN_COL ) 
			BEGIN 
				BREAK
			END

			SELECT 
			@IDEN_ROW_ID			= IDEN_ROW_ID, 
			@TOTAL_DUE				= TOTAL_DUE, 
			@TOTAL_PAID				= TOTAL_PAID, 
			@SOURCE_EFF_DATE		= SOURCE_EFF_DATE, 
			@ITEM_TRAN_CODE			= ITEM_TRAN_CODE,
			@ITEM_TRAN_CODE_TYPE	= ITEM_TRAN_CODE_TYPE , 
			@UPDATED_FROM			= UPDATED_FROM  
			FROM @InsOpenItems WHERE IDEN_COL = @IDEN_COL 

			SET @IDEN_COL = @IDEN_COL + 1 
			SET @RECONCILED = 1	--This flag will be set to 1 if there is some reconcilition
	
			IF @ITEM_TRAN_CODE = @ITEM_TRAN_CODE_INSTALLMENT_FEES 
			BEGIN
				--If current transaction is of installment fees, 
				--we will reconcile only first  installment fees
				IF @INTALLMENT_RECONCILED = 1 
				BEGIN
					
					DECLARE @TOTAL_OF_NEXT_INS Decimal(20,8)
						
					SELECT @TOTAL_OF_NEXT_INS = SUM(ISNULL(TOTAL_DUE,0) - ISNULL(TOTAL_PAID,0))
					FROM ACT_CUSTOMER_OPEN_ITEMS with(nolock)
					WHERE INSTALLMENT_ROW_ID IN 
					(SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS with(nolock) WHERE INSTALLMENT_NO IN 
					(SELECT INSTALLMENT_NO FROM ACT_POLICY_INSTALLMENT_DETAILS INS with(nolock) 
					INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OP ON OP.INSTALLMENT_ROW_ID = INS.ROW_ID 
					WHERE OP.IDEN_ROW_ID = @IDEN_ROW_ID)
					AND CUSTOMER_ID = @CUSTOMER_ID
					AND POLICY_ID = @POLICY_ID)
					AND ITEM_TRAN_CODE_TYPE <> 'FEES'
					-- There cant't be reconciled records for an installment before Ins Fees records
					-- AND IDEN_ROW_ID NOT IN (SELECT IDEN_ROW_ID FROM #RECORDS_TO_RECONCILE with(nolock))
				
					IF( ISNULL( @TOTAL_OF_NEXT_INS,0) <=  @AMOUNT_TO_APPLY)
					BEGIN
						--IF installment fees transaction is already reconciled, 
						-- and next installment will be fully paid after this reconciliation 
						--then changing the next installment
						--fees due amount to zero and continuing the loop
						--Added by Shikha against itrack# 4914.
						SET @INSF_DETAILS = CONVERT(VARCHAR,@IDEN_ROW_ID)  + '^' + CONVERT(VARCHAR,ISNULL(@TOTAL_DUE,0)) 
											 + ',' + ISNULL(@INSF_DETAILS,'') 
						--End of addition.
						--Ravindra(12/16/2009): Moved to Post and Reconcile Customer deposit
--						UPDATE ACT_CUSTOMER_OPEN_ITEMS 
--						SET TOTAL_DUE = ISNULL(TOTAL_PAID,0)
--						WHERE IDEN_ROW_ID = @IDEN_ROW_ID
		
						CONTINUE		
					END
					-- Ravindra (09-06-2007) If next installment can't be paid full then the remaining amount 
					-- will be adjusted against the next installment's premium (iTrack 2426)
					-- Previously it is being applied against Fees of next due installment first.
					-- So if next installment is going to be partially paid ignore fees item of this installment
					ELSE
					BEGIN 
						CONTINUE
					END		
				END
				ELSE
					SET @INTALLMENT_RECONCILED = 1 --Setting the installment fees reconciled flag on
			END
	
			
	
			if @TOTAL_DUE - @TOTAL_PAID < @AMOUNT_TO_APPLY
			BEGIN
				INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT , 
						ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
				VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @TOTAL_DUE - @TOTAL_PAID , 
						@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )			
				
				--- Added By Ravindra 
				-- If there is any negative balance reconcile it
				IF ISNULL(@TOTAL_DUE,0) < ISNULL(@TOTAL_PAID,0) 
				BEGIN
					DECLARE @NEGATIVE_DUE Decimal(20,8)
					SET @NEGATIVE_DUE = ISNULL(@TOTAL_DUE,0) - ISNULL(@TOTAL_PAID,0) 
					SET @AMOUNT_TO_APPLY = @AMOUNT_TO_APPLY - @NEGATIVE_DUE
				END
				ELSE
				BEGIN
					--Updating the amount to reconcile variable			
					SET @AMOUNT_TO_APPLY = @AMOUNT_TO_APPLY - (ISNULL(@TOTAL_DUE,0) - ISNULL(@TOTAL_PAID,0))
				END
			END
			ELSE
			BEGIN
				
	
				INSERT INTO @RECORDS_TO_RECONCILE ( IDEN_ROW_ID, TOTAL_DUE, RECONCILE_AMOUNT , 
							ITEM_TRAN_CODE , ITEM_TRAN_CODE_TYPE , UPDATED_FROM )
				VALUES (@IDEN_ROW_ID, @TOTAL_DUE, @AMOUNT_TO_APPLY , 
							@ITEM_TRAN_CODE , @ITEM_TRAN_CODE_TYPE , @UPDATED_FROM )			
				
-- 				IF @TOTAL_DUE - @TOTAL_PAID = @AMOUNT_TO_APPLY AND @INTALLMENT_RECONCILED = 1 
-- 					AND ISNULL(@ITEM_TRAN_CODE,'') <> ISNULL(@ITEM_TRAN_CODE_INSTALLMENT_FEES,'')
-- 				BEGIN
-- 					--If reconciling whole amount for this open item
-- 					--updating its fees due to zero
-- 					UPDATE ACT_CUSTOMER_OPEN_ITEMS
-- 					SET TOTAL_DUE = ISNULL(TOTAL_PAID,0)
-- 					WHERE INSTALLMENT_ROW_ID = 
-- 					(SELECT TOP 1 INSTALLMENT_ROW_ID 
-- 					FROM ACT_CUSTOMER_OPEN_ITEMS 
-- 					WHERE IDEN_ROW_ID = @IDEN_ROW_ID)
-- 					AND IDEN_ROW_ID <> @IDEN_ROW_ID
-- 	
-- 				END
				
				--All amount has been reconcile, hence setting it to 0
				SET @AMOUNT_TO_APPLY = 0
	
	
				--Whole amount has been applied, hence exiting from while loop
				break
			END
		END		--End while loop
	
	END
END

SELECT * FROM @RECORDS_TO_RECONCILE

END

 
 
--
--go 
--DECLARE @INSF_DETAILS Varchar(8000)
--exec Proc_GetCustomerRecordsToReconcile  5362	, 1 , @INSF_DETAILS out 
--
--
--SELECT A.IDEN_ROW_ID,B.ROW_ID, A.TRANS_DESC,A.TOTAL_DUE,A.TOTAL_PAID,A.DUE_DATE,A.SWEEP_DATE,
--A.SOURCE_EFF_DATE,MNT.LOOKUP_VALUE_DESC as PaymentMode, 
--A.ITEM_TRAN_CODE_TYPE,A.ITEM_TRAN_CODE,
--ISNULL(B.INSTALLMENT_NO,50) INS_NO,B.INSTALLMENT_NO	 AS INS_NO,B.RELEASED_STATUS,A.RISK_ID,A.RISK_TYPE,
--A.* FROM ACT_CUSTOMER_OPEN_ITEMS A WITH(NOLOCK)
--left JOIN ACT_POLICY_INSTALLMENT_DETAILS B WITH(NOLOCK) ON A.INSTALLMENT_ROW_ID  = B.ROW_ID
--LEFT JOIN MNT_LOOKUP_VALUES MNT ON 
--B.PAYMENT_MODE = MNT.LOOKUP_UNIQUE_ID 
--WHERE A.CUSTOMER_ID= 1577
--AND A.POLICY_ID= 23
--ORDER BY ISNULL(B.INSTALLMENT_NO,50) ,A.IDEN_ROW_ID ,A.SOURCE_EFF_DATE,A.UPDATED_FROM
--
--
--rollback tran 











GO

