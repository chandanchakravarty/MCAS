IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPaymentPlanFor_TotalPremium]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPaymentPlanFor_TotalPremium]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create proc [dbo].[Proc_GetPaymentPlanFor_TotalPremium]  
(
	@TOTALPREMIUM INT,
	@POLICYTERM INT,
	@POLICYEFFFDATE datetime
)
AS
BEGIN
DECLARE @INSTALLMENTDESC nvarchar(max)--varchar(8000) 
SET @INSTALLMENTDESC=''
	CREATE TABLE #TMP_APPLICABLEPLAN
	(	
		IDENT_COL 			[int] IDENTITY(1,1) NOT NULL ,      
		INSTALL_PLAN_ID 			SMALLINT,
		BILLING_PLAN 				VARCHAR(100),
		APPLABLE_POLTERM         			INT,
		IS_ACTIVE 			Varchar(20)
	)
	
	-- get Applicable Billing Plan
	INSERT INTO #TMP_APPLICABLEPLAN
	SELECT         
		INST_PLAN.IDEN_PLAN_ID ,        
		ISNULL(INST_PLAN.PLAN_DESCRIPTION,' ') + ' (' + ISNULL(INST_PLAN.PLAN_CODE,' ') + ')' 
			+ ' - ' + ISNULL(LOOKUP.LOOKUP_VALUE_DESC,'') BILLING_PLAN, 
		isnull(APPLABLE_POLTERM,'-1') ,
 		/*MODE_OF_DOWNPAY AS MODE_OF_DOWNPAY,
 		MODE_OF_DOWNPAY1 AS MODE_OF_DOWNPAY1,
 		MODE_OF_DOWNPAY2 AS MODE_OF_DOWNPAY2,
		mnt.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC,
		mnt1.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC1,
		mnt2.LOOKUP_VALUE_DESC LOOKUP_VALUE_DESC2,
		NULL AS MISSING_COL,INST_PLAN.DEFAULT_PLAN,*/
		INST_PLAN.IS_ACTIVE		
		FROM ACT_INSTALL_PLAN_DETAIL INST_PLAN
		LEFT JOIN MNT_LOOKUP_VALUES LOOKUP  --Changed by Ravindra(08-16-2007) From Inner To Left
		ON INST_PLAN.PLAN_PAYMENT_MODE = LOOKUP.LOOKUP_UNIQUE_ID 
-- 		LEFT JOIN MNT_LOOKUP_VALUES MNT ON MNT.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY
-- 		LEFT JOIN MNT_LOOKUP_VALUES MNT1 ON MNT1.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY1         
-- 		LEFT JOIN MNT_LOOKUP_VALUES MNT2 ON MNT2.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY2
		LEFT JOIN MNT_LOOKUP_VALUES MNT ON MNT.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY 
		LEFT JOIN MNT_LOOKUP_VALUES MNT1 ON MNT1.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY1       
		LEFT JOIN MNT_LOOKUP_VALUES MNT2 ON MNT2.LOOKUP_UNIQUE_ID=INST_PLAN.MODE_OF_DOWNPAY2  
	
		WHERE (INST_PLAN.APPLABLE_POLTERM = @POLICYTERM OR  INST_PLAN.APPLABLE_POLTERM  = 0)
		AND ISNULL(INST_PLAN.IS_ACTIVE,'N') = 'Y'
		ORDER BY BILLING_PLAN   


-- get instalment info group by instalment effective date
CREATE TABLE #TMP_INSTALMENTPLANDESC
	(	
		IDENT_COL 			[int] IDENTITY(1,1) NOT NULL ,      
		COUNTNODE 			INT,
		NO_OF_PAYMENTS      SMALLINT,
	    INSTALLMENT_AMOUNT DECIMAL(20,2),
        INSTALLMENT_FEES   decimal(18,2),
        INSTALLMENT_EFFECTIVE_DATE DATETIME
	)
DECLARE @IDEN_PLAN_ID SMALLINT, @PLAN_DESCRIPTION NVARCHAR(70),@PLAN_CODE NVARCHAR(70),
				@PLAN_TYPE NCHAR(2), @NO_OF_PAYMENTS SMALLINT, @MONTHS_BETWEEN SMALLINT, 
				@PERCENT_BREAKDOWN1 DECIMAL(10,4), @PERCENT_BREAKDOWN2 DECIMAL(10,4), 
				@PERCENT_BREAKDOWN3 DECIMAL(10,4), @PERCENT_BREAKDOWN4 DECIMAL(10,4),
				@PERCENT_BREAKDOWN5 DECIMAL(10,4), @PERCENT_BREAKDOWN6 DECIMAL(10,4),
				@PERCENT_BREAKDOWN7 DECIMAL(10,4), @PERCENT_BREAKDOWN8 DECIMAL(10,4),
				@PERCENT_BREAKDOWN9 DECIMAL(10,4), @PERCENT_BREAKDOWN10 DECIMAL(10,4), 
				@PERCENT_BREAKDOWN11 DECIMAL(10,4), @PERCENT_BREAKDOWN12 DECIMAL(10,4),
				@PLAN_PAYMENT_MODE Int,@NO_INS_DOWNPAY INt,@PLAN_PAYMENT_MODE_DESC NVARCHAR(100),
				@DUE_DATE  Datetime
DECLARE @INSTALLMENT SMALLINT
CREATE TABLE #TMP_APPLICABLEPLANDESC
	(	
		IDENT_COL 			[int] IDENTITY(1,1) NOT NULL ,      
		PAYMENT_PLAN 			nvarchar(max)
	)
declare @INSTALLMENT_FEES decimal(18,2)
DECLARE @IDENT_COLMN INT
SET @IDENT_COLMN=1
DECLARE @EFT INT	
SET @EFT   = 11973
WHILE (1= 1)  
	BEGIN     
		IF NOT EXISTS (SELECT IDENT_COL FROM #TMP_APPLICABLEPLAN  WITH(NOLOCK) WHERE IDENT_COL = @IDENT_COLMN)     
			BEGIN     
				BREAK    
			END 

			SELECT  @INSTALLMENT = INSTALL_PLAN_ID    
			FROM #TMP_APPLICABLEPLAN  WITH(NOLOCK)    
			WHERE IDENT_COL = @IDENT_COLMN 
		--- Instalment Fees
	
			SELECT @INSTALLMENT_FEES = ISNULL(INSTALLMENT_FEES, 0)
			FROM ACT_INSTALL_PLAN_DETAIL	WITH(NOLOCK)
			WHERE IDEN_PLAN_ID = @INSTALLMENT


		--Getting the details of installments
				SELECT 
					@IDEN_PLAN_ID = IDEN_PLAN_ID, @PLAN_DESCRIPTION = PLAN_DESCRIPTION,@PLAN_CODE = PLAN_CODE,
					@PLAN_TYPE = PLAN_TYPE, @NO_OF_PAYMENTS = NO_OF_PAYMENTS, @MONTHS_BETWEEN = MONTHS_BETWEEN, 
					@PERCENT_BREAKDOWN1 = PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2 = PERCENT_BREAKDOWN2, 
					@PERCENT_BREAKDOWN3 = PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4 = PERCENT_BREAKDOWN4,
					@PERCENT_BREAKDOWN5 = PERCENT_BREAKDOWN5, @PERCENT_BREAKDOWN6 = PERCENT_BREAKDOWN6,
					@PERCENT_BREAKDOWN7 = PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8 = PERCENT_BREAKDOWN8, 
					@PERCENT_BREAKDOWN9 = PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10 = PERCENT_BREAKDOWN10, 
					@PERCENT_BREAKDOWN11 = PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12 = PERCENT_BREAKDOWN12,
					@PLAN_PAYMENT_MODE  = PLAN_PAYMENT_MODE,@PLAN_PAYMENT_MODE_DESC = LOOKUP.LOOKUP_VALUE_DESC,
					@NO_INS_DOWNPAY   = NO_INS_DOWNPAY
				FROM ACT_INSTALL_PLAN_DETAIL	WITH(NOLOCK)
				LEFT JOIN MNT_LOOKUP_VALUES LOOKUP  --Changed by Ravindra(08-16-2007) From Inner To Left
				ON ACT_INSTALL_PLAN_DETAIL.PLAN_PAYMENT_MODE = LOOKUP.LOOKUP_UNIQUE_ID 
				WHERE IDEN_PLAN_ID = @INSTALLMENT



			--Getting the record for Installment
			DECLARE @INSTALLMENT_EFFVE_DATE DATETIME, @INST_AMOUNT DECIMAL(20,2), @NO_PAYMENTS SMALLINT
			DECLARE @INSTALLMENT_EFFECTIVE_DATE DATETIME, @INSTALLMENT_AMOUNT DECIMAL(20,2)
			DECLARE @CTR SMALLINT
			DECLARE @INSTALLMENTTOTAL DECIMAL(20,2)
			SET @INSTALLMENTTOTAL=0
			DECLARE @IS_DOWN_PAYMENT SmallINt,
				@MODE_OF_PAYMENT Int,
				@PERCENTAG_OF_PREMIUM  Decimal(9,4)
			DECLARE @COUNTNODE INT
			SET @COUNTNODE =0
			SET @CTR = 1;
			SET @IS_DOWN_PAYMENT   = 1
			DECLARE @CNTR INT
			SET @CNTR=0			
			DECLARE @DISCOUNT_APPLICABLE SmallInt
			DECLARE @DISCOUNT_AMOUNT DECIMAL(20,8)
			DECLARE @TOTAL_INSTALLMENT_AMT DECIMAL(20,8)
			DECLARE @TOTAL_DISCOUNT_AMT DECIMAL(20,8)
			DECLARE @DIFFERENCE DECIMAL(20,8),
					@CALCULATED_INS_EFF_DATE Datetime
			DECLARE @COUNTERDOWNPAYMENT INT
			SET @COUNTERDOWNPAYMENT=0
			SET @INSTALLMENT_EFFECTIVE_DATE = @POLICYEFFFDATE
			SET @CALCULATED_INS_EFF_DATE = @POLICYEFFFDATE
			DECLARE @NUM_PAYMENT SMALLINT
			SET @NUM_PAYMENT=0
--SELECT @NO_INS_DOWNPAY,@IS_DOWN_PAYMENT
			IF (@NO_INS_DOWNPAY  >= @IS_DOWN_PAYMENT)
				BEGIN
					-- REVISED NUMBER OF PAYMENTS					
					SET @NUM_PAYMENT = @NO_OF_PAYMENTS - @NO_INS_DOWNPAY + 1
				END
			ELSE
				BEGIN
					SET @NUM_PAYMENT=@NO_OF_PAYMENTS
				END
			SET @DISCOUNT_APPLICABLE  = 0
			SET @TOTAL_INSTALLMENT_AMT = 0
			SET @TOTAL_DISCOUNT_AMT  = 0

			WHILE @CTR <= @NO_OF_PAYMENTS		--Looping as number of times as there are installments
			BEGIN
					IF @CTR > 1		--Calculating the istallment effective date
						SET @CALCULATED_INS_EFF_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @CALCULATED_INS_EFF_DATE )
						--SELECT @INSTALLMENT_EFFECTIVE_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @INSTALLMENT_EFFECTIVE_DATE )
			
					IF @IS_DOWN_PAYMENT  > @NO_INS_DOWNPAY
					BEGIN 
						SET @MODE_OF_PAYMENT =   @PLAN_PAYMENT_MODE
					END
--					ELSE
--					BEGIN 
--						SET @MODE_OF_PAYMENT =   @SELECTED_DOWN_PAYMENT_MODE
--					END 
									
					-- Ravindra(06-11-2007): If Installment is Part of DownPayment will be billed with 
					-- first installment i.e. due date will be same as policy effective date
									
					IF @NO_INS_DOWNPAY  <= @IS_DOWN_PAYMENT
					BEGIN 
						SET @INSTALLMENT_EFFECTIVE_DATE = @CALCULATED_INS_EFF_DATE
					END
					ELSE
					BEGIN 
						IF(@COUNTERDOWNPAYMENT < @NO_INS_DOWNPAY)
							BEGIN
								SET @INSTALLMENT_EFFECTIVE_DATE=@POLICYEFFFDATE
							END
						ELSE
							BEGIN
								SET @INSTALLMENT_EFFECTIVE_DATE = @CALCULATED_INS_EFF_DATE
							END
					END
					DECLARE @EFT_TENTATIVE_DATE DATETIME
					SET @EFT_TENTATIVE_DATE=@POLICYEFFFDATE
					-- In Case of EFT mode of payment
					-- if EFT Tentative date is with in 5 days of actual due date then due date will
					-- EFT Tentetive day will be date in same month
					-- else transaction eill be EFT one month prior
					IF(@MODE_OF_PAYMENT = @EFT AND @EFT_TENTATIVE_DATE <> 0)
					BEGIN 
						DECLARE @DD SmallInt,
							@MM SmallInt,
							@YYYY SmallInt
						SET @DD = DATEPART(dd,@INSTALLMENT_EFFECTIVE_DATE)
							SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)
							SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)

						-- If month is Feb then date will be updated to 28 if it is 29
						IF(@MM = 2 AND @DD >=29)
						BEGIN 
							SET @DD = 28
						END
						-- by Pravesh on 17 aug 2007 if @MM=0 then
						IF (@MM = 0) 
						BEGIN 
							SET @MM = 12
							SET @YYYY = @YYYY-1
						END
						--end here 
						-- If month is of 30 days and @DD=31 then date will be updated to 30 if it is 31 --by pravesh on 6 aug 2007
						IF((@MM = 4 or @MM = 6 or @MM = 9 or @MM = 11) and @DD =31)
						BEGIN 
							SET @DD = 30
						END
			
						SET @DUE_DATE = CAST(
						( 
						CONVERT(VARCHAR,@YYYY)
						+ '-'
						+ CONVERT(VARCHAR,@MM) 
						+ '-'
						+ CONVERT(VARCHAR, @DD) 
						)
						AS DATETIME)

						IF( @DUE_DATE < CAST(CONVERT(VARCHAR,@POLICYEFFFDATE,101) AS DATETIME))
						BEGIN 
							SET @DUE_DATE = @POLICYEFFFDATE 
						END	

					END
					ELSE
					BEGIN
						SET @DUE_DATE = @INSTALLMENT_EFFECTIVE_DATE
					END 
		
					SELECT @PERCENTAG_OF_PREMIUM =
						CASE @CTR
							WHEN 1 THEN
								@PERCENT_BREAKDOWN1
							WHEN 2 THEN
								@PERCENT_BREAKDOWN2					
							WHEN 3 THEN
								 @PERCENT_BREAKDOWN3
							WHEN 4 THEN
								 @PERCENT_BREAKDOWN4
							WHEN 5 THEN
								 @PERCENT_BREAKDOWN5
							WHEN 6 THEN
								 @PERCENT_BREAKDOWN6
							WHEN 7 THEN
								@PERCENT_BREAKDOWN7
							WHEN 8 THEN
								@PERCENT_BREAKDOWN8
							WHEN 9 THEN
								@PERCENT_BREAKDOWN9
							WHEN 10 THEN
								@PERCENT_BREAKDOWN10
							WHEN 11 THEN
								@PERCENT_BREAKDOWN11
							WHEN 12 THEN
								@PERCENT_BREAKDOWN12
						END		
					SET @INSTALLMENT_AMOUNT = ROUND((@PERCENTAG_OF_PREMIUM/100) * @TOTALPREMIUM, 2)
					SET @COUNTNODE=@COUNTNODE+1
					SET @COUNTERDOWNPAYMENT=@COUNTERDOWNPAYMENT+1
				SELECT @CTR = @CTR + 1	
						if(@COUNTERDOWNPAYMENT<= @NO_INS_DOWNPAY)
							BEGIN
									IF(@CNTR=0)
										BEGIN
											SET @CNTR=@CNTR+1
											SET @INSTALLMENTTOTAL =@INSTALLMENTTOTAL+@INSTALLMENT_AMOUNT+@INSTALLMENT_FEES
										END
									ELSE
										BEGIN
											SET @INSTALLMENTTOTAL =@INSTALLMENTTOTAL+@INSTALLMENT_AMOUNT
										END
							END
						ELSE
							BEGIN
									SET @INSTALLMENTTOTAL =@INSTALLMENTTOTAL+@INSTALLMENT_AMOUNT+@INSTALLMENT_FEES
							END

				-- INSERT IN TO #TMP_INSTALMENTPLANDESC 
				INSERT INTO #TMP_INSTALMENTPLANDESC
				VALUES (@COUNTNODE,@NUM_PAYMENT,@INSTALLMENT_AMOUNT,@INSTALLMENT_FEES,@DUE_DATE)	
			END
				SET @CNTR=0
-- create temp table to stroe payment plan
			CREATE TABLE #TMP_INSTALMENTDESC
				(	
					IDENT_COL 			[int] IDENTITY(1,1) NOT NULL ,      
					[INSTALMENT_DESC] 			Nvarchar(MAX),
					[INSTALMENT_DESC1] 			Nvarchar(MAX),
					[INSTALMENT_DESC2] 			Nvarchar(MAX),
					[INSTALMENT_DESC3] 			Nvarchar(MAX)
				)		
	INSERT INTO #TMP_INSTALMENTDESC
	 select '<InstallmentInfo>'+'<NumPayments>'+ convert(nvarchar(100),NO_OF_PAYMENTS)+'</NumPayments>', '<DepositAmt>'+ convert(nvarchar(100),SUM(INSTALLMENT_AMOUNT))+'</DepositAmt>',
			'<com.brics_Installmenteffdate>'+convert(nvarchar(100),INSTALLMENT_EFFECTIVE_DATE) +'</com.brics_Installmenteffdate>','<InstallmentFeeAmt>'+convert(nvarchar(100),@INSTALLMENT_FEES)+'</InstallmentFeeAmt>'+'</InstallmentInfo>' 
			FROM #TMP_INSTALMENTPLANDESC GROUP BY INSTALLMENT_EFFECTIVE_DATE,NO_OF_PAYMENTS--,INSTALLMENT_AMOUNT
		SET @IDENT_COLMN = @IDENT_COLMN + 1
--SELECT * FROM #TMP_INSTALMENTDESC
DELETE FROM #TMP_INSTALMENTPLANDESC
DECLARE @IDENT_COLN INT
DECLARE @TEMPINST VARCHAR(MAX)
DECLARE @TEMPINST1 VARCHAR(MAX)
DECLARE @TEMPINST2 VARCHAR(MAX)
DECLARE @TEMPINST3 VARCHAR(MAX)
SET @TEMPINST=''
SET @TEMPINST1=''
SET @TEMPINST2=''
SET @TEMPINST3=''
SET @IDENT_COLN=1
WHILE(1=1)
	BEGIN
		IF NOT EXISTS (SELECT IDENT_COL FROM #TMP_INSTALMENTDESC  WITH(NOLOCK) WHERE IDENT_COL = @IDENT_COLN)     
			BEGIN     
				BREAK    
			END 
			SELECT @INSTALLMENTDESC=INSTALMENT_DESC,@TEMPINST1=INSTALMENT_DESC1,
					@TEMPINST2=INSTALMENT_DESC2,@TEMPINST3=INSTALMENT_DESC3 FROM #TMP_INSTALMENTDESC WHERE IDENT_COL = @IDENT_COLN

			/*SET @INSTALLMENTDESC = @INSTALLMENTDESC + '<InstallmentInfo_'+convert(nvarchar(100),@COUNTNODE)+'>'+'<NumPayments>'+  convert(nvarchar(100),@NO_PAYMENTS)+'</NumPayments>'+'<DepositAmt>'+ convert(nvarchar(100),SUM(@INST_AMOUNT)) +'</DepositAmt>' 
				+'<InstallmentFeeAmt>'+convert(nvarchar(100),@INSTALLMENT_FEES)+'</InstallmentFeeAmt>'
				+'<com.brics_Installmenteffdate>'+convert(nvarchar(100),@INSTALLMENT_EFFVE_DATE,107) +'</com.brics_Installmenteffdate>'
				+'</InstallmentInfo_'+convert(nvarchar(100),@COUNTNODE)+'>'*/ 
		SET @TEMPINST = @TEMPINST + @INSTALLMENTDESC + @TEMPINST1 + @TEMPINST2 + @TEMPINST3
		--DELETE FROM #TMP_INSTALMENTDESC	WHERE IDENT_COL = @IDENT_COLN
		SET @IDENT_COLN=@IDENT_COLN + 1
	END

DROP TABLE #TMP_INSTALMENTDESC
			INSERT INTO #TMP_APPLICABLEPLANDESC			
			select '<PaymentOption>'+'<Description>'+convert(nvarchar(100),@PLAN_PAYMENT_MODE_DESC)+'</Description>'
					+'<PaymentPlanCd>'+convert(nvarchar(100),@PLAN_CODE)+'</PaymentPlanCd>'+'<com.brics_PaymentPlan>'
					+convert(nvarchar(100),@PLAN_DESCRIPTION)+'</com.brics_PaymentPlan>'+'<com.brics_TotalOfPayments>'
					+convert(nvarchar(100),round(@INSTALLMENTTOTAL,0))+'</com.brics_TotalOfPayments>'+@TEMPINST+'</PaymentOption>'  
			
		SET @COUNTNODE =0
	
		END

SELECT * FROM #TMP_APPLICABLEPLANDESC ORDER BY IDENT_COL
DROP TABLE #TMP_APPLICABLEPLAN
DROP TABLE #TMP_APPLICABLEPLANDESC
DROP TABLE #TMP_INSTALMENTPLANDESC
END


GO

