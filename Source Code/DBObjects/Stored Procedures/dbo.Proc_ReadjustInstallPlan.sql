IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ReadjustInstallPlan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ReadjustInstallPlan]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name       : dbo.Proc_ReadjustInstallPlan
Created by      : Ravindra 
Date            : 12-19-2006
Purpose         : To Re Adjust installment plan for the selected policy
Revison History :        
Used In         :            

-----------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop PROC dbo.Proc_ReadjustInstallPlan
CREATE PROC dbo.Proc_ReadjustInstallPlan
(
@CUSTOMER_ID Int,
@POLICY_ID Int,
@POLICY_VERSION_ID Int,
@NEW_PLAN_ID Int
)
AS        
BEGIN        

DECLARE @IS_HOME_EMP  Char(1)
DECLARE @POLICY_EFFECTIVE_DATE DateTime
DECLARE @POLICY_STATUS    Varchar(20)
DECLARE @CURRENT_TERM SmallInt
DECLARE @POLICY_TERMS SmallInt
DECLARE @APP_ID Int, @APP_VERSION_ID Int,@SELECTED_DOWN_PAYMENT_MODE Int

	      
SELECT  @POLICY_EFFECTIVE_DATE=POLICY_EFFECTIVE_DATE   ,   
	@POLICY_STATUS   = POLICY_STATUS ,
	@CURRENT_TERM 	 = CURRENT_TERM	,
	@POLICY_TERMS    = APP_TERMS,
	@APP_ID 	 = @APP_ID,
	@APP_VERSION_ID  = @APP_VERSION_ID,
	@SELECTED_DOWN_PAYMENT_MODE = DOWN_PAY_MODE,
	@IS_HOME_EMP = CASE ISNULL(IS_HOME_EMP, 0) WHEN 1 THEN 'Y' ELSE 'N' END

FROM POL_CUSTOMER_POLICY_LIST       
WHERE  CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID       

/****************************************
Conditions to be added in which the new plan can not be assigned
*************************************************************/

-- Make Old Plan IN-ACTIVE

UPDATE  ACT_POLICY_INSTALL_PLAN_DATA SET IS_ACTIVE_PLAN = 'N' 
WHERE   CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND CURRENT_TERM = @CURRENT_TERM

-- Fetch Details of new Plan 
DECLARE @IDEN_PLAN_ID SMALLINT, @PLAN_DESCRIPTION NVARCHAR(70),
	@PLAN_TYPE NCHAR(2), @NO_OF_PAYMENTS SMALLINT, @MONTHS_BETWEEN SMALLINT, 
	@PERCENT_BREAKDOWN1 DECIMAL(10,4), @PERCENT_BREAKDOWN2 DECIMAL(10,4), 
	@PERCENT_BREAKDOWN3 DECIMAL(10,4), @PERCENT_BREAKDOWN4 DECIMAL(10,4),
	@PERCENT_BREAKDOWN5 DECIMAL(10,4), @PERCENT_BREAKDOWN6 DECIMAL(10,4),
	@PERCENT_BREAKDOWN7 DECIMAL(10,4), @PERCENT_BREAKDOWN8 DECIMAL(10,4),
	@PERCENT_BREAKDOWN9 DECIMAL(10,4), @PERCENT_BREAKDOWN10 DECIMAL(10,4), 
	@PERCENT_BREAKDOWN11 DECIMAL(10,4), @PERCENT_BREAKDOWN12 DECIMAL(10,4),
	@PLAN_PAYMENT_MODE Int,@NO_INS_DOWNPAY INt

			
SELECT 	@IDEN_PLAN_ID = IDEN_PLAN_ID, @PLAN_DESCRIPTION = PLAN_DESCRIPTION,
	@PLAN_TYPE = PLAN_TYPE, @NO_OF_PAYMENTS = NO_OF_PAYMENTS, @MONTHS_BETWEEN = MONTHS_BETWEEN, 
	@PERCENT_BREAKDOWN1 = PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2 = PERCENT_BREAKDOWN2, 
	@PERCENT_BREAKDOWN3 = PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4 = PERCENT_BREAKDOWN4,
	@PERCENT_BREAKDOWN5 = PERCENT_BREAKDOWN5, @PERCENT_BREAKDOWN6 = PERCENT_BREAKDOWN6,
	@PERCENT_BREAKDOWN7 = PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8 = PERCENT_BREAKDOWN8, 
	@PERCENT_BREAKDOWN9 = PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10 = PERCENT_BREAKDOWN10, 
	@PERCENT_BREAKDOWN11 = PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12 = PERCENT_BREAKDOWN12,
	@PLAN_PAYMENT_MODE  = PLAN_PAYMENT_MODE,
	@NO_INS_DOWNPAY   = NO_INS_DOWNPAY
FROM ACT_INSTALL_PLAN_DETAIL
WHERE IDEN_PLAN_ID = @NEW_PLAN_ID 


--- Inserting New Plan data in the table 
INSERT INTO ACT_POLICY_INSTALL_PLAN_DATA
(
APP_ID, APP_VERSION_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, PLAN_ID, PLAN_DESCRIPTION, 
PLAN_TYPE, NO_OF_PAYMENTS, MONTHS_BETWEEN, 
PERCENT_BREAKDOWN1, PERCENT_BREAKDOWN2, PERCENT_BREAKDOWN3, PERCENT_BREAKDOWN4, PERCENT_BREAKDOWN5,
PERCENT_BREAKDOWN6, PERCENT_BREAKDOWN7, PERCENT_BREAKDOWN8, PERCENT_BREAKDOWN9, PERCENT_BREAKDOWN10, 
PERCENT_BREAKDOWN11, PERCENT_BREAKDOWN12,
MODE_OF_DOWN_PAYMENT,INSTALLMENTS_IN_DOWN_PAYMENT,MODE_OF_PAYMENT,CURRENT_TERM,IS_ACTIVE_PLAN

)
VALUES
(
@APP_ID, @APP_VERSION_ID, @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @IDEN_PLAN_ID, @PLAN_DESCRIPTION,
@PLAN_TYPE, @NO_OF_PAYMENTS, @MONTHS_BETWEEN, 
@PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2, @PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4, @PERCENT_BREAKDOWN5,
@PERCENT_BREAKDOWN6, @PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8, @PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10, 
@PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12,
@SELECTED_DOWN_PAYMENT_MODE,@NO_INS_DOWNPAY,@PLAN_PAYMENT_MODE,@CURRENT_TERM,'Y'
)



DECLARE @PREMIUM_AMOUNT  Decimal(18,2)

SELECT @PREMIUM_AMOUNT   = PREMIUM_AMOUNT
FROM ACT_PREMIUM_PROCESS_DETAILS with(nolock)
WHERE CUSTOMER_ID  = @CUSTOMER_ID
AND POLICY_ID      = @POLICY_ID 
AND POLICY_VERSION_ID = @POLICY_VERSION_ID

CREATE TABLE #TMP_INSTALL_PLAN
(
	[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                                              
	[AMOUNT]			DECIMAL(25,2),
	[INSTALLMENT_EFFECTIVE_DATE]	DATETIME,
	[RELEASED_STATUS]		Char(1),
	[INSTALLMENT_NO]		Int
)


--Inserting the record in to temp table
DECLARE @INSTALLMENT_EFFECTIVE_DATE DATETIME, @INSTALLMENT_AMOUNT DECIMAL(20,2)
DECLARE @CTR SMALLINT
SET @CTR = 1;
SET @INSTALLMENT_EFFECTIVE_DATE = @POLICY_EFFECTIVE_DATE

WHILE @CTR <= @NO_OF_PAYMENTS		--Looping as number of times as there are installments
BEGIN
	
	IF @CTR > 1
		SELECT @INSTALLMENT_EFFECTIVE_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @INSTALLMENT_EFFECTIVE_DATE )

	SELECT @INSTALLMENT_AMOUNT =
		CASE @CTR
			WHEN 1 THEN
				ROUND((@PERCENT_BREAKDOWN1/100) * @PREMIUM_AMOUNT, 2)
			WHEN 2 THEN
				ROUND((@PERCENT_BREAKDOWN2/100) * @PREMIUM_AMOUNT, 2)
			WHEN 3 THEN
				ROUND((@PERCENT_BREAKDOWN3/100) * @PREMIUM_AMOUNT, 2)
			WHEN 4 THEN
				ROUND((@PERCENT_BREAKDOWN4/100) * @PREMIUM_AMOUNT, 2)
			WHEN 5 THEN
				ROUND((@PERCENT_BREAKDOWN5/100) * @PREMIUM_AMOUNT, 2)
			WHEN 6 THEN
				ROUND((@PERCENT_BREAKDOWN6/100) * @PREMIUM_AMOUNT, 2)
			WHEN 7 THEN
				ROUND((@PERCENT_BREAKDOWN7/100) * @PREMIUM_AMOUNT, 2)
			WHEN 8 THEN
				ROUND((@PERCENT_BREAKDOWN8/100) * @PREMIUM_AMOUNT, 2)
			WHEN 9 THEN
				ROUND((@PERCENT_BREAKDOWN9/100) * @PREMIUM_AMOUNT, 2)
			WHEN 10 THEN
				ROUND((@PERCENT_BREAKDOWN10/100) * @PREMIUM_AMOUNT, 2)
			WHEN 11 THEN
				ROUND((@PERCENT_BREAKDOWN11/100) * @PREMIUM_AMOUNT, 2)
			WHEN 12 THEN
				ROUND((@PERCENT_BREAKDOWN12/100) * @PREMIUM_AMOUNT, 2)
		END
	
	--Inserting the installment record in temporary table, 
	INSERT INTO #TMP_INSTALL_PLAN
	(
		AMOUNT,
		INSTALLMENT_EFFECTIVE_DATE,
		INSTALLMENT_NO
	)
	VALUES
	(	
		@INSTALLMENT_AMOUNT,
		@INSTALLMENT_EFFECTIVE_DATE,
		@CTR
	)
	
	SELECT @CTR = @CTR + 1
END

DECLARE @UNREALESED_AMOUNT Decimal(18,2),
	@REMAINING_AMOUNT Decimal(18,2),
	@NEXT_DUE_INSTALLMENT_NO Int,
	@NEXT_DUE_INSTALLMENT_EFF_DATE DateTime

-- Fetching Date & Ins No of next due installment
SELECT  @NEXT_DUE_INSTALLMENT_NO  = MIN(INSTALLMENT_NO),
	@NEXT_DUE_INSTALLMENT_EFF_DATE  = MIN(INSTALLMENT_EFFECTIVE_DATE)
FROM ACT_POLICY_INSTALLMENT_DETAILS 
WHERE   CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND CURRENT_TERM = @CURRENT_TERM
	AND ISNULL(RELEASED_STATUS, ' ') = 'N'

SELECT @UNREALESED_AMOUNT  = SUM(INSTALLMENT_AMOUNT) 
FROM ACT_POLICY_INSTALLMENT_DETAILS 
WHERE   CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND CURRENT_TERM = @CURRENT_TERM
	AND ISNULL(RELEASED_STATUS, '') = 'N'


CREATE TABLE #FINAL_INSTALL_PLAN
(
	[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,                                                              
	[AMOUNT]			DECIMAL(25,2),
	[INSTALLMENT_EFFECTIVE_DATE]	DATETIME,
	[RELEASED_STATUS]		Char(1),
	[INSTALLMENT_NO]		Int
)


DECLARE curTEMP_INSTALLMENTS CURSOR   
LOCAL FORWARD_ONLY STATIC  
FOR SELECT AMOUNT,INSTALLMENT_EFFECTIVE_DATE,INSTALLMENT_NO  
FROM #TMP_INSTALL_PLAN   
order by INSTALLMENT_NO DESC


DECLARE @AMT Decimal(18,2),
	@INSTAL_EFFECTIVE_DATE DateTime,
	@INSTAL_NO  Int

SET @REMAINING_AMOUNT = @UNREALESED_AMOUNT

OPEN curTEMP_INSTALLMENTS  

WHILE 1=1  
BEGIN   
	--Fetching the values from cursor  
	FETCH NEXT FROM curTEMP_INSTALLMENTS INTO @AMT , @INSTAL_EFFECTIVE_DATE , @INSTAL_NO  
	
	IF @@FETCH_STATUS <> 0  
		BREAK;  

	IF @AMT > @REMAINING_AMOUNT
	BEGIN
		IF(@INSTAL_EFFECTIVE_DATE > @NEXT_DUE_INSTALLMENT_EFF_DATE )
		BEGIN 
				INSERT INTO #FINAL_INSTALL_PLAN (AMOUNT ,INSTALLMENT_EFFECTIVE_DATE ,INSTALLMENT_NO)
				VALUES(@REMAINING_AMOUNT , @INSTAL_EFFECTIVE_DATE , @INSTAL_NO  )
		END
		BREAK;
	END
	INSERT INTO #FINAL_INSTALL_PLAN (AMOUNT ,INSTALLMENT_EFFECTIVE_DATE ,INSTALLMENT_NO)
		VALUES(@AMT , @INSTAL_EFFECTIVE_DATE , @INSTAL_NO  )
	SET @REMAINING_AMOUNT = @REMAINING_AMOUNT - @AMT 
END

CLOSE curTEMP_INSTALLMENTS  
DEALLOCATE curTEMP_INSTALLMENTS  


select @UNREALESED_AMOUNT ,@NEXT_DUE_INSTALLMENT_EFF_DATE ,@NEXT_DUE_INSTALLMENT_NO 
SELECT * FROM #TMP_INSTALL_PLAN order by INSTALLMENT_NO desc

SELECT @REMAINING_AMOUNT
SELECT * from #FINAL_INSTALL_PLAN




DROP TABLE #TMP_INSTALL_PLAN

END



GO

