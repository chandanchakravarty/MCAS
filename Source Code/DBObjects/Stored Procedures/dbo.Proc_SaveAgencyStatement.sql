IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveAgencyStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveAgencyStatement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc dbo.Proc_SaveAgencyStatement  
--go 

/*---------------------------------------------------------------------------    
CREATE BY  : Praven Kasana    
CREATE DATETIME : 14 Nov 2006 12.54.00 PM    
PURPOSE   : To Fetch and Commit the agency statement details of specified agency of specified month    
REVIOSN HISTORY    
Revised By  Date  Reason    

----------------------------------------------------------------------------*/    
--drop proc dbo.Proc_SaveAgencyStatement  
CREATE  PROC [dbo].[Proc_SaveAgencyStatement]    
(    
 @MONTH  INT = null,    
 @YEAR  INT = null,  
 @COMM_TYPE VARCHAR(20) = null,  --Regular//Additional//Property Inspection//Complete    
 @RETVALUE INT OUTPUT --CHECK TO SEE IF RECORD EXISTS OR NOT   
)    
AS    
BEGIN    
 

SET NOCOUNT ON 
DECLARE @LAST_DAY_OF_MONTH Datetime 

--Ravindra(03-03-2009): As we are advancing records to next month for rounding adjustments
--there could be records for this month already
-- Ravindra(07-24-2008) : If already processed stop further processing
--IF EXISTS ( SELECT ROW_ID FROM ACT_AGENCY_STATEMENT_DETAILED WHERE MONTH_NUMBER = @MONTH 
--			AND MONTH_YEAR = @YEAR AND COMM_TYPE =  @COMM_TYPE 
--			AND ISNULL(NOT_COUNTED_RECEIVABLE,0) = 0 ) 
--BEGIN 
--	SET @RETVALUE = -2 
--	RETURN @RETVALUE 
--END
	
IF(@MONTH = 12)
BEGIN 
	SET @LAST_DAY_OF_MONTH = CAST( CONVERT(VARCHAR, @YEAR+1) +'-' + CONVERT(VARCHAR,1) + '-' + '1' AS DATETIME)
	SET @LAST_DAY_OF_MONTH = DATEADD(dd,-1 , @LAST_DAY_OF_MONTH )
END
ELSE
BEGIN 
	SET @LAST_DAY_OF_MONTH = CAST( CONVERT(VARCHAR, @YEAR) +'-' + CONVERT(VARCHAR,@MONTH + 1) + '-' + '1' AS DATETIME)
	SET @LAST_DAY_OF_MONTH = DATEADD(dd, -1 , @LAST_DAY_OF_MONTH)
END
 
--========== FETCHING THE PROCESSED RECORDS==========  
/* drop the temporary table if already exists */  
if Object_id('tempdb..##TEMP_ACT_AGENCY_STATEMENT') IS NOT NULL  
	DROP TABLE ##TEMP_ACT_AGENCY_STATEMENT  
/*Create table and fetch record */  
	
EXEC Proc_ProcessAgencyStatement @MONTH,@YEAR,@COMM_TYPE  
  
IF EXISTS(SELECT MONTH_NUMBER FROM ##TEMP_ACT_AGENCY_STATEMENT) 
BEGIN 

	INSERT INTO ACT_AGENCY_STATEMENT_DETAILED    
	(    
	MONTH_NUMBER, MONTH_YEAR, AGENCY_ID, POLICY_ID, POLICY_VERSION_ID, CUSTOMER_ID, SOURCE_EFF_DATE,     
	PREMIUM_AMOUNT, COMMISSION_RATE ,    
	TOTAL_PAID, SOURCE_ROW_ID, TRAN_TYPE, AMOUNT_FOR_CALCULATION,     
	COMMISSION_AMOUNT,    
	DUE_AMOUNT, PAYMENT_STATUS, IS_TEMP_ENTRY, IN_RECON, AMT_IN_RECON,  
	CUSTOMER_OPEN_ITEM_ID,AGENCY_OPEN_ITEM_ID,TRAN_CODE,COMM_TYPE ,BILL_TYPE ,
	STAT_FEES_FOR_CALCULATION  , CSR_ID
	)   
	SELECT   
	MONTH_NUMBER, MONTH_YEAR, AGENCY_ID, POLICY_ID, POLICY_VERSION_ID , CUSTOMER_ID, SOURCE_EFF_DATE,         
	PREMIUM_AMOUNT,COMMISSION_RATE,      
	TOTAL_PAID, SOURCE_ROW_ID, TRAN_TYPE, AMOUNT_FOR_CALCULATION ,      
	COMMISSION_AMOUNT,      
	DUE_AMOUNT, PAYMENT_STATUS, IS_TEMP_ENTRY, IN_RECON, AMT_IN_RECON,      
	CUSTOMER_OPEN_ITEM_ID ,AGENCY_OPEN_ITEM_ID,TRAN_CODE,COMM_TYPE ,BILL_TYPE   , 
	STAT_FEES_FOR_CALCULATION , CSR_ID
	FROM ##TEMP_ACT_AGENCY_STATEMENT  

	IF(@COMM_TYPE = 'ADC')  --ADDITIONAL COMMISION
	BEGIN
		--Updating the PROCESSED_AMOUNT_FOR_AGENCY field    
		UPDATE ACT_CUSTOMER_OPEN_ITEMS     
		SET ACT_CUSTOMER_OPEN_ITEMS.PROCESSED_AMOUNT_FOR_ADDITIONAL = ACT_CUSTOMER_OPEN_ITEMS.TOTAL_PAID     
		FROM POL_CUSTOMER_POLICY_LIST PCL     
		WHERE     
		PCL.CUSTOMER_ID = ACT_CUSTOMER_OPEN_ITEMS.CUSTOMER_ID     
		AND PCL.POLICY_ID = ACT_CUSTOMER_OPEN_ITEMS.POLICY_ID     
		AND PCL.POLICY_VERSION_ID = ACT_CUSTOMER_OPEN_ITEMS.POLICY_VERSION_ID     
		AND ISNULL(ACT_CUSTOMER_OPEN_ITEMS.TOTAL_DUE,0) <> ISNULL(ACT_CUSTOMER_OPEN_ITEMS.PROCESSED_AMOUNT_FOR_ADDITIONAL,0)    
		AND ACT_CUSTOMER_OPEN_ITEMS .BILL_CODE = 'DB'    
		--AND (MONTH(PCL.APP_EFFECTIVE_DATE) <= @MONTH AND YEAR(PCL.APP_EFFECTIVE_DATE) <= @YEAR)    
		AND DATEDIFF(DD,PCL.APP_EFFECTIVE_DATE , @LAST_DAY_OF_MONTH) >= 0 	 
		--====END processing=======================  
	END
	ELSE IF(@COMM_TYPE = 'REG') 	---Regular Commission
	BEGIN
		--Updating the PROCESSED_AMOUNT_FOR_AGENCY field    
		UPDATE ACT_CUSTOMER_OPEN_ITEMS 
		SET ACT_CUSTOMER_OPEN_ITEMS.PROCESSED_AMOUNT_FOR_AGENCY = ACT_CUSTOMER_OPEN_ITEMS.TOTAL_PAID     
		FROM POL_CUSTOMER_POLICY_LIST PCL     
		WHERE     
		PCL.CUSTOMER_ID = ACT_CUSTOMER_OPEN_ITEMS.CUSTOMER_ID
		AND PCL.POLICY_ID = ACT_CUSTOMER_OPEN_ITEMS.POLICY_ID
		AND PCL.POLICY_VERSION_ID = ACT_CUSTOMER_OPEN_ITEMS.POLICY_VERSION_ID
		AND ISNULL(ACT_CUSTOMER_OPEN_ITEMS.TOTAL_DUE,0) <> ISNULL(ACT_CUSTOMER_OPEN_ITEMS.PROCESSED_AMOUNT_FOR_AGENCY,0)
		AND ACT_CUSTOMER_OPEN_ITEMS .BILL_CODE = 'DB'
		--AND (MONTH(PCL.APP_EFFECTIVE_DATE) <= @MONTH AND YEAR(PCL.APP_EFFECTIVE_DATE) <= @YEAR)    
		AND DATEDIFF(DD,PCL.APP_EFFECTIVE_DATE , @LAST_DAY_OF_MONTH) >= 0 	 
		--====END processing=======================  
	END

	-- Insert record in Agency Statemnt Final with grouping

	exec ProcInsertFinalAgencyStatement  @MONTH , @YEAR , @COMM_TYPE 

END
ELSE
BEGIN
	SET @RETVALUE = -1 --NO RECORDS AVAILABLE FOR THIS COMMISSION
END
  
  
DROP TABLE ##TEMP_ACT_AGENCY_STATEMENT  
RETURN @RETVALUE 
END  

--
--go 
--declare @r int 
--exec Proc_SaveAgencyStatement 7 , 2008,'REG',@R out
--
--rollback tran
--





GO

