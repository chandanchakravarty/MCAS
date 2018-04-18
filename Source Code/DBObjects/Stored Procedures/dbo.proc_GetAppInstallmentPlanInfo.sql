IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetAppInstallmentPlanInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetAppInstallmentPlanInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.proc_GetAppInstallmentPlanInfo
--go
/*----------------------------------------------------------                
Proc Name       : dbo.proc_GetAppInstallmentPlanInfo                
Created by      : Ravindra         
Date            : 12-26-2006        
Purpose         : To Re Adjust installment plan for the selected Application        
Revison History :                
Used In         :                    
      
Reviewed By : Anurag verma      
Reviewed On : 12-07-2007      
        
exec proc_GetAppInstallmentPlanInfo   1426,145,1      
-----------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
        
         
-- drop proc dbo.proc_GetAppInstallmentPlanInfo                 
CREATE PROCEDURE dbo.proc_GetAppInstallmentPlanInfo                
(                
 @CUSTOMER_ID int,              
 @APP_ID int,              
 @APP_VERSION_ID int              
)                
AS                
BEGIN                
         
DECLARE @INSURED_BILL Int,        
		@FULL_PAY_PLAN_ID  Int,        
		@INSTALL_PLAN_ID Int,
		@BILL_TYPE_ID Int,
		@APP_EFFECTIVE_DATE DateTime,      
		@EFT_TENTATIVE_DATE INT,      
		@APP_TERMS SmallInt,      
		@BILL_TYPE Char(2),      
		@NO_OF_PAYMENTS Int,        
		@PLAN_PAYMENT_MODE Int,        
		@NO_INS_DOWNPAY Int,        
		@MODE_OF_DOWNPAY Int,        
		@MONTHS_BETWEEN SMALLINT,        
		@PERCENT_BREAKDOWN1 DECIMAL(10,4), @PERCENT_BREAKDOWN2 DECIMAL(10,4),         
		@PERCENT_BREAKDOWN3 DECIMAL(10,4), @PERCENT_BREAKDOWN4 DECIMAL(10,4),        
		@PERCENT_BREAKDOWN5 DECIMAL(10,4), @PERCENT_BREAKDOWN6 DECIMAL(10,4),        
		@PERCENT_BREAKDOWN7 DECIMAL(10,4), @PERCENT_BREAKDOWN8 DECIMAL(10,4),        
		@PERCENT_BREAKDOWN9 DECIMAL(10,4), @PERCENT_BREAKDOWN10 DECIMAL(10,4),         
		@PERCENT_BREAKDOWN11 DECIMAL(10,4), @PERCENT_BREAKDOWN12 DECIMAL(10,4)        
        
        
SET @INSURED_BILL = 8460              

SELECT @FULL_PAY_PLAN_ID = ISNULL(IDEN_PLAN_ID,0)          
FROM ACT_INSTALL_PLAN_DETAIL  with(nolock)      
WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1        

SELECT @EFT_TENTATIVE_DATE  = isnull(EFT_TENTATIVE_DATE,'') FROM ACT_APP_EFT_CUST_INFO  (NOLOCK)      
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID              

    
SELECT  @BILL_TYPE  = ISNULL(BILL_TYPE,''),      
		@INSTALL_PLAN_ID = ISNULL(INSTALL_PLAN_ID,0),
		@BILL_TYPE_ID = ISNULL(BILL_TYPE_ID,0),        
		@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE   ,           
		@APP_TERMS    = ISNULL(APP_TERMS,0),        
		@MODE_OF_DOWNPAY = ISNULL(DOWN_PAY_MODE,0)        
FROM APP_LIST  with(nolock)      
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND              
APP_ID = @APP_ID AND              
APP_VERSION_ID = @APP_VERSION_ID               
        
        
SELECT  @NO_OF_PAYMENTS = NO_OF_PAYMENTS ,        
		@PLAN_PAYMENT_MODE = PLAN_PAYMENT_MODE,        
		@NO_INS_DOWNPAY = NO_INS_DOWNPAY,        
		--@MODE_OF_DOWNPAY = MODE_OF_DOWNPAY,        
		@MONTHS_BETWEEN = MONTHS_BETWEEN,        
		@PERCENT_BREAKDOWN1 = PERCENT_BREAKDOWN1, @PERCENT_BREAKDOWN2 = PERCENT_BREAKDOWN2,         
		@PERCENT_BREAKDOWN3 = PERCENT_BREAKDOWN3, @PERCENT_BREAKDOWN4 = PERCENT_BREAKDOWN4,        
		@PERCENT_BREAKDOWN5 = PERCENT_BREAKDOWN5, @PERCENT_BREAKDOWN6 = PERCENT_BREAKDOWN6,        
		@PERCENT_BREAKDOWN7 = PERCENT_BREAKDOWN7, @PERCENT_BREAKDOWN8 = PERCENT_BREAKDOWN8,         
		@PERCENT_BREAKDOWN9 = PERCENT_BREAKDOWN9, @PERCENT_BREAKDOWN10 = PERCENT_BREAKDOWN10,         
		@PERCENT_BREAKDOWN11 = PERCENT_BREAKDOWN11, @PERCENT_BREAKDOWN12 = PERCENT_BREAKDOWN12        
FROM ACT_INSTALL_PLAN_DETAIL  with(nolock)      
WHERE IDEN_PLAN_ID = @INSTALL_PLAN_ID        
        
-- Result Set 1        
SELECT  @BILL_TYPE as BILL_TYPE,        
CASE @INSTALL_PLAN_ID WHEN @FULL_PAY_PLAN_ID THEN 'FULLPAY'   
ELSE 'INSTALLMENT' END  INSTALLMENT_PLAN ,        
@APP_EFFECTIVE_DATE As APP_EFFECTIVE_DATE,      
@APP_TERMS AS APP_TERMS,        
@INSTALL_PLAN_ID AS INSTALL_PLAN_ID,
@BILL_TYPE_ID AS BILL_TYPE_ID,       
@MODE_OF_DOWNPAY as MODE_OF_DOWNPAY ,@PLAN_PAYMENT_MODE AS PLAN_PAYMENT_MODE        


CREATE TABLE #TEMP_INSTALLMENT_DETAILS        
(        
	INSTALL_PLAN_ID Int,
	BILL_TYPE_ID Int,        
	INSTALLMENT_NO Int,        
	RELEASED_STATUS Char(2),        
	INSTALLMENT_EFFECTIVE_DATE DateTime,        
	PAYMENT_MODE Int ,        
	PERCENTAG_OF_PREMIUM Decimal(9,4)        
)        

DECLARE @INSTALLMENT_EFFECTIVE_DATE DATETIME  ,      
		@CTR SMALLINT ,       
		@IS_DOWN_PAYMENT  SmallInt ,      
		@MODE_OF_PAYMENT Int,        
		@PERCENTAG_OF_PREMIUM Decimal(9,4) ,      
		@DUE_DATE DATETIME     ,
		@CALCULATED_INS_EFF_DATE Datetime
   
SET @CTR = 1        
SET @IS_DOWN_PAYMENT   = 1        
SET @INSTALLMENT_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE        
SET @CALCULATED_INS_EFF_DATE  = @APP_EFFECTIVE_DATE    


         
        
WHILE @CTR <= @NO_OF_PAYMENTS  --Looping as number of times as there are installments        
BEGIN        
	IF @CTR > 1  --Calculating the istallment effective date        
		SELECT @CALCULATED_INS_EFF_DATE = DATEADD(MONTH, @MONTHS_BETWEEN, @INSTALLMENT_EFFECTIVE_DATE )      

    
	IF @IS_DOWN_PAYMENT  > @NO_INS_DOWNPAY        
	BEGIN         
		SET @MODE_OF_PAYMENT =   @PLAN_PAYMENT_MODE       
		SET @INSTALLMENT_EFFECTIVE_DATE = @CALCULATED_INS_EFF_DATE     
	END        
	ELSE        
	BEGIN         
		SET @MODE_OF_PAYMENT =   @MODE_OF_DOWNPAY        
		SET @INSTALLMENT_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE     
	END         
      
 SELECT @PERCENTAG_OF_PREMIUM =  CASE @CTR        
		WHEN 1 THEN   @PERCENT_BREAKDOWN1        
		WHEN 2 THEN  @PERCENT_BREAKDOWN2             
		WHEN 3 THEN   @PERCENT_BREAKDOWN3        
		WHEN 4 THEN   @PERCENT_BREAKDOWN4        
		WHEN 5 THEN   @PERCENT_BREAKDOWN5        
		WHEN 6 THEN   @PERCENT_BREAKDOWN6        
		WHEN 7 THEN   @PERCENT_BREAKDOWN7        
		WHEN 8 THEN   @PERCENT_BREAKDOWN8        
		WHEN 9 THEN   @PERCENT_BREAKDOWN9        
		WHEN 10 THEN  @PERCENT_BREAKDOWN10        
		WHEN 11 THEN  @PERCENT_BREAKDOWN11        
		WHEN 12 THEN  @PERCENT_BREAKDOWN12   END        
          

	IF @IS_DOWN_PAYMENT  <= @NO_INS_DOWNPAY
	BEGIN 
		SET @INSTALLMENT_EFFECTIVE_DATE = @APP_EFFECTIVE_DATE
	END
	ELSE
	BEGIN 
		SET @INSTALLMENT_EFFECTIVE_DATE = @CALCULATED_INS_EFF_DATE
	END

	IF(@MODE_OF_PAYMENT = 11973 AND @EFT_TENTATIVE_DATE <> 0)
	BEGIN 
		DECLARE @DD SmallInt,
				@MM SmallInt,
				@YYYY SmallInt


		IF(@EFT_TENTATIVE_DATE - DATEPART(DD,@INSTALLMENT_EFFECTIVE_DATE)  > 5 )
		--AND DATEPART(DD,@INSTALLMENT_EFFECTIVE_DATE)  - @EFT_TENTATIVE_DATE < 0
		BEGIN 

			SET @DD = @EFT_TENTATIVE_DATE
			SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)-1
			SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)
		END
		ELSE
		BEGIN 
			SET @DD = @EFT_TENTATIVE_DATE
			SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)
			SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)
		END

		-- If month is Feb then date will be updated to 28 if it is 29
		IF(@MM = 2 AND @DD >=29)
		BEGIN 
			SET @DD = 28
		END
		IF (@MM = 0) 
		BEGIN 
			SET @MM = 12
			SET @YYYY = @YYYY-1
		END


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

		IF( @DUE_DATE < CAST(CONVERT(VARCHAR,@APP_EFFECTIVE_DATE,101) AS DATETIME))
		BEGIN 
			SET @DUE_DATE = @APP_EFFECTIVE_DATE 
		END	

	END
	ELSE
	BEGIN
		SET @DUE_DATE = @INSTALLMENT_EFFECTIVE_DATE
		----Added on (30 Oct 2009) Praveen Kasana Itrack 6675 --Check Installments
		SET @DD = DATEPART(DD,@APP_EFFECTIVE_DATE)
		SET @MM = DATEPART(MM,@INSTALLMENT_EFFECTIVE_DATE)
		SET @YYYY = DATEPART(YYYY,@INSTALLMENT_EFFECTIVE_DATE)
		-- If month is Feb then date will be updated to 28 if it is 29
		IF(@MM = 2 AND @DD >=29)
		BEGIN 
			SET @DD = 28
		END
		IF (@MM = 0) 
		BEGIN 
			SET @MM = 12
			SET @YYYY = @YYYY-1
		END


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

		IF( @DUE_DATE < CAST(CONVERT(VARCHAR,@APP_EFFECTIVE_DATE,101) AS DATETIME))
		BEGIN 
			SET @DUE_DATE = @APP_EFFECTIVE_DATE 
		END	
		-------end Itrack 6675
	END 

	INSERT INTO #TEMP_INSTALLMENT_DETAILS        
	(        
		INSTALL_PLAN_ID,
		BILL_TYPE_ID,        
		INSTALLMENT_NO ,        
		RELEASED_STATUS ,        
		INSTALLMENT_EFFECTIVE_DATE ,        
		PAYMENT_MODE ,        
		PERCENTAG_OF_PREMIUM        
	)        
	VALUES        
	(        
		@INSTALL_PLAN_ID,
		@BILL_TYPE_ID,
		@CTR,        
		'N',        
		@DUE_DATE,        
		@MODE_OF_PAYMENT,        
		@PERCENTAG_OF_PREMIUM        
	)        
	SET @CTR = @CTR + 1        
	SET @IS_DOWN_PAYMENT = @IS_DOWN_PAYMENT + 1        
      
END         
        
-- resultset 2         
SELECT         
      
CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101) AS INSTALLMENT_EFFECTIVE_DATE,    
CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101) AS INSTALLMENT_DUE_DATE,   --Added Itrack #4068
DATEPART(D,CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101)) AS DAY_PART,        
--ISNULL(CAST(DATEPART(MM,CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101)) AS VARCHAR) + '/' + cast(@EFT_TENTATIVE_DATE as varchar) + '/' + CAST(DATEPART(YY,CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101)) AS VARCHAR),CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101)
--) as INSTALLMENT_PROCESSING_DATE,  
CONVERT(CHAR,INSTALLMENT_EFFECTIVE_DATE,101) AS INSTALLMENT_PROCESSING_DATE ,
INSTALLMENT_NO,              
MLV.LOOKUP_VALUE_DESC AS PAYMENT_MODE,              
CASE RELEASED_STATUS WHEN 'Y' THEN 'Yes' ELSE 'No' END RELEASED_STATUS,              
INSTALL_DETAILS.PERCENTAG_OF_PREMIUM AS PERCENTAG_OF_PREMIUM,        
'Not Calculated' AS INSTALLMENT_AMOUNT        
FROM               
#TEMP_INSTALLMENT_DETAILS INSTALL_DETAILS   with(nolock)            
LEFT OUTER JOIN        
MNT_LOOKUP_VALUES MLV         
ON INSTALL_DETAILS.PAYMENT_MODE = MLV.LOOKUP_UNIQUE_ID        
ORDER BY   
INSTALLMENT_NO         
      
         
DROP TABLE #TEMP_INSTALLMENT_DETAILS 
        
END                
              



--go
--
--exec dbo.proc_GetAppInstallmentPlanInfo 1068,58,1
--
--rollback tran
GO

