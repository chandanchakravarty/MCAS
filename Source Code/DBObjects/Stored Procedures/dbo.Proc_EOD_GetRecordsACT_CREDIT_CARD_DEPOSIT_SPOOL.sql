IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.[Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]
Created by       : Praveen Kasana
Date             : 07-09-2008
Purpose      	 : Will fetch records to Credit Card Sweep from Spool Table   
				 : Which are Failed in BRICS	
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.[Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]
CREATE PROC [dbo].[Proc_EOD_GetRecordsACT_CREDIT_CARD_DEPOSIT_SPOOL]
AS
BEGIN 

SELECT  SPOOL.IDEN_ROW_ID AS SPOOL_RECORD_ID,
	SPOOL.ENTITY_ID,
	SPOOL.POLICY_NUMBER,
	SPOOL.POLICY_ID,
	ISNULL(CUST.CUSTOMER_FIRST_NAME , '') 
		+ ISNULL(CUST.CUSTOMER_MIDDLE_NAME,'')
		+ ISNULL(CUST.CUSTOMER_LAST_NAME,'') As CUSTOMER_NAME,
	SPOOL.POLICY_VERSION_ID,
	SPOOL.TRANSACTION_AMOUNT,
	ISNULL(SPOOL.PROCESSED,'N') AS PROCESSED_STATUS, 
	SPOOL.PROCESSED_DATETIME, 
	SPOOL.PAY_PAL_REF_ID AS CARD_REFERENCE_ID  
FROM ACT_CREDIT_CARD_DEPOSIT_SPOOL SPOOL WITH(NOLOCK)
INNER JOIN CLT_CUSTOMER_LIST CUST WITH(NOLOCK)
	ON SPOOL.ENTITY_ID = CUST.CUSTOMER_ID 	
WHERE  ISNULL(SPOOL.PROCESSED,'N') IN  ('N') 
ORDER BY SPOOL.IDEN_ROW_ID


END





GO

