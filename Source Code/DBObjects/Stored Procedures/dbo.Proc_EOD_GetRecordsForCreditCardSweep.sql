IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_EOD_GetRecordsForCreditCardSweep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_EOD_GetRecordsForCreditCardSweep]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_EOD_GetRecordsForCreditCardSweep
Created by       : Ravinda Gupta 
Date             : 06-12-2007
Purpose      	 : Will fetch records to Credit Card Sweep from Spool Table
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_EOD_GetRecordsForCreditCardSweep
CREATE PROC [dbo].[Proc_EOD_GetRecordsForCreditCardSweep]
AS
BEGIN 

--- will have records of customers for EFT
SELECT  SPOOL.IDEN_ROW_ID AS SPOOL_RECORD_ID,
	SPOOL.ENTITY_ID, 
	SPOOL.POLICY_NUMBER, 
	SPOOL.POLICY_ID, 
	ISNULL(CUST.CUSTOMER_FIRST_NAME , '') 
		+ ISNULL(CUST.CUSTOMER_MIDDLE_NAME,'')
		+ ISNULL(CUST.CUSTOMER_LAST_NAME,'') As CUSTOMER_NAME,
	SPOOL.POLICY_VERSION_ID,
	SPOOL.TRANSACTION_AMOUNT, 
	SPOOL.CREATED_DATETIME, 
	ISNULL(SPOOL.PROCESSED,'N') AS PROCESSED_STATUS, 
	SPOOL.PROCESSED_DATETIME, 
--	CC.CARD_TYPE AS CARD_TYPE,
--	CC.CARD_HOLDER_NAME AS CARD_HOLDER_NAME , 
--	CC.CARD_DATE_VALID_TO AS CARD_EXPIRY_DATE,
--	CC.CARD_CVV_NUMBER AS CVV_NUMBER ,
--	CC.CARD_NO AS CARD_NUMBER
	CC.PAY_PAL_REF_ID AS CARD_REFERENCE_ID  
FROM EOD_CREDIT_CARD_SPOOL SPOOL WITH(NOLOCK)
LEFT  JOIN ACT_POL_CREDIT_CARD_DETAILS CC  WITH(NOLOCK)
	ON SPOOL.ENTITY_ID = CC.CUSTOMER_ID 
	AND SPOOL.POLICY_ID = CC.POLICY_ID 	
	AND SPOOL.POLICY_VERSION_ID = CC.POLICY_VERSION_ID
INNER JOIN CLT_CUSTOMER_LIST CUST WITH(NOLOCK)
	ON SPOOL.ENTITY_ID = CUST.CUSTOMER_ID 	
WHERE   ISNULL(SPOOL.ENTITY_TYPE,'CUST') = 'CUST'
	AND ISNULL(SPOOL.PROCESSED,'N') IN  ('N','F') 
ORDER BY SPOOL.IDEN_ROW_ID


END





GO

