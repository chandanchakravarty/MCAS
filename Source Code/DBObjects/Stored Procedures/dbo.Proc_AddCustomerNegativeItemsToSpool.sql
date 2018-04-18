IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddCustomerNegativeItemsToSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddCustomerNegativeItemsToSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                    
Proc Name       : dbo.Proc_AddCustomerNegativeItemsToSpool
Created by      : Ravindra                                                                  
Date            : 06-02-2008
Purpose         : To add custmoer negative items to be reconciled in message queue
Revison History :                                                                    
Used In         : Wolverine                
------------------------------------------------------------                                                                    
Date     Review By          Comments                                                                    
------   ------------       -------------------------                                                                   
*/                                                          
-- drop proc  Proc_AddCustomerNegativeItemsToSpool  
CREATE PROCEDURE [dbo].[Proc_AddCustomerNegativeItemsToSpool]
AS
BEGIN 

	--Ravindra(11/23/2009): If negative item is other than deposit, reconcile only if positive premium item exists
	INSERT INTO ACT_CUSTOMER_NEGATIVE_ITEM_SPOOL
	(
		CUSTOMER_OPEN_ITEM_ID	, CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID , TOTAL_PAID , 
		TOTAL_DUE , ITEM_TRAN_CODE  ,ITEM_TRAN_CODE_TYPE , ITEM_STATUS , UPDATED_FROM
	)
	SELECT 	OI.IDEN_ROW_ID ,OI.CUSTOMER_ID,OI.POLICY_ID ,
	OI.POLICY_VERSION_ID ,ISNULL(OI.TOTAL_PAID,0),ISNULL(OI.TOTAL_DUE,0),
	OI.ITEM_TRAN_CODE 	,OI.ITEM_TRAN_CODE_TYPE  , OI.ITEM_STATUS , OI.UPDATED_FROM	
	FROM ACT_CUSTOMER_OPEN_ITEMS OI
	WHERE OI.UPDATED_FROM <> 'D' 
	AND
	(
		(ISNULL(OI.TOTAL_DUE,0) < 0 AND  ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID,0))
			OR
		(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0) < 0)
	)
	AND CAST(CONVERT(VARCHAR,OI.SOURCE_EFF_DATE,101)  AS DATETIME ) <= 
	CAST(CONVERT(VARCHAR, GETDATE() ,101)  AS DATETIME ) 	
	AND EXISTS 
	(
		SELECT OIIN.IDEN_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS OIIN
		WHERE OIIN.CUSTOMER_ID	= OI.CUSTOMER_ID 
		AND OIIN.POLICY_ID		= OI.POLICY_ID 
		AND ISNULL(OIIN.TOTAL_DUE,0) - ISNULL(OIIN.TOTAL_PAID,0) > 0 
		AND OIIN.ITEM_TRAN_CODE_TYPE <> 'FEES'
	)
	AND NOT EXISTS
	( 
		SELECT IDEN_SPOOL_ID FROM 	ACT_CUSTOMER_NEGATIVE_ITEM_SPOOL 
		WHERE CUSTOMER_OPEN_ITEM_ID = OI.IDEN_ROW_ID
	)
	ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID  

	--Ravindra(11/23/2009): If negative item is deposit it can be reconcilled with Fees 
	INSERT INTO ACT_CUSTOMER_NEGATIVE_ITEM_SPOOL
	(
		CUSTOMER_OPEN_ITEM_ID	, CUSTOMER_ID , POLICY_ID , POLICY_VERSION_ID , TOTAL_PAID , 
		TOTAL_DUE , ITEM_TRAN_CODE  ,ITEM_TRAN_CODE_TYPE , ITEM_STATUS , UPDATED_FROM
	)
	SELECT 	OI.IDEN_ROW_ID ,OI.CUSTOMER_ID,OI.POLICY_ID ,
	OI.POLICY_VERSION_ID ,ISNULL(OI.TOTAL_PAID,0),ISNULL(OI.TOTAL_DUE,0),
	OI.ITEM_TRAN_CODE 	,OI.ITEM_TRAN_CODE_TYPE  , OI.ITEM_STATUS , OI.UPDATED_FROM	
	FROM ACT_CUSTOMER_OPEN_ITEMS OI
	WHERE OI.UPDATED_FROM = 'D' 
	AND
	(
		(ISNULL(OI.TOTAL_DUE,0) < 0 AND  ISNULL(OI.TOTAL_DUE,0)  <> ISNULL(OI.TOTAL_PAID,0))
			OR
		(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0) < 0)
	)
	AND CAST(CONVERT(VARCHAR,OI.SOURCE_EFF_DATE,101)  AS DATETIME ) <= 
	CAST(CONVERT(VARCHAR, GETDATE() ,101)  AS DATETIME ) 	
	AND EXISTS 
	(
		SELECT OIIN.IDEN_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS OIIN
		WHERE OIIN.CUSTOMER_ID	= OI.CUSTOMER_ID 
		AND OIIN.POLICY_ID		= OI.POLICY_ID 
		AND ISNULL(OIIN.TOTAL_DUE,0) - ISNULL(OIIN.TOTAL_PAID,0) > 0 
	)
	AND NOT EXISTS
	( 
		SELECT IDEN_SPOOL_ID FROM 	ACT_CUSTOMER_NEGATIVE_ITEM_SPOOL 
		WHERE CUSTOMER_OPEN_ITEM_ID = OI.IDEN_ROW_ID
	)
	ORDER BY SOURCE_EFF_DATE, IDEN_ROW_ID  
END







GO

