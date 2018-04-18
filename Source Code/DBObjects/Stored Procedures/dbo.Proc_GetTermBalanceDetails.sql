IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTermBalanceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTermBalanceDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name       : Proc_GetTermBalanceDetails                                         
Created by      : Mohit Agarwal                                      
Date            : 17-09-2008                                      
Purpose      : To fetch term balance of (a) policy                                     
Revison History :                                          
Used In   :                                           
Modified By :                                  
Date        :                                  
Purpose     :                                             
                          
------------------------------------------------------------                                          
Date        Review By          Comments                                          
------   ------------       -------------------------*/                                 
--drop proc dbo.Proc_GetTermBalanceDetails                                          
CREATE  PROC [dbo].[Proc_GetTermBalanceDetails]                                          
AS
BEGIN                                         

	WITH


	TERM_BALANCE AS 
	(
		SELECT CPL.CUSTOMER_ID , CPL.POLICY_ID , CPL.CURRENT_TERM ,
		SUM( ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID , 0) ) AS BALANCE 
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
		-- Added by Shikha Dixit against itrack# 5710.
		AND CPL.POLICY_VERSION_ID >= 
		( 
			SELECT MIN(PPPIN.NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS PPPIN
			WHERE PPPIN.CUSTOMER_ID = CPL.CUSTOMER_ID 
			AND PPPIN.POLICY_ID		= CPL.POLICY_ID 
			AND PROCESS_ID	IN (25,18)  
			AND PROCESS_STATUS		='COMPLETE'-- MIN OF NBS OR RENEWAL
			AND ISNULL(REVERT_BACK,'N') = 'N'
		) 
		GROUP BY CPL.CUSTOMER_ID , CPL.POLICY_ID , CPL.CURRENT_TERM
	) ,



	LATEST_VERSION AS 
	(	
		SELECT 	PCPL.CUSTOMER_ID,	PCPL.POLICY_ID,	MAX(PCPL.POLICY_VERSION_ID) AS POLICY_VERSION_ID
		FROM POL_CUSTOMER_POLICY_LIST AS PCPL
		GROUP BY 	PCPL.CUSTOMER_ID, PCPL.POLICY_ID
	)


	SELECT ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' +
		(CASE WHEN ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') = '' THEN ''
		ELSE CCL.CUSTOMER_MIDDLE_NAME + ' ' END ) + ISNULL(CCL.CUSTOMER_LAST_NAME,'')	AS CUSTOMER_NAME, 
		CPL.POLICY_NUMBER , CPL.APP_EFFECTIVE_DATE AS POLICY_EFFECTIVE_DATE, CPL.APP_EXPIRATION_DATE AS POLICY_EXPIRY_DATE,
		STATUS.POLICY_DESCRIPTION AS POLICY_CURRENT_STATUS, CURR_BAL.BALANCE AS CURRENT_TERM_BALANCE ,
		ISNULL(PREV_BAL.BALANCE ,0) AS PREVIOUS_TERM_BALANCE 
	FROM POL_CUSTOMER_POLICY_LIST CPL 
	INNER JOIN LATEST_VERSION CPL2
		ON CPL.CUSTOMER_ID = CPL2.CUSTOMER_ID 
		AND CPL.POLICY_ID = CPL2.POLICY_ID 
		AND CPL.POLICY_VERSION_ID = CPL2.POLICY_VERSION_ID
	INNER JOIN TERM_BALANCE CURR_BAL
		ON CPL.CUSTOMER_ID = CURR_BAL.CUSTOMER_ID 
		AND CPL.POLICY_ID = CURR_BAL.POLICY_ID 
		AND CPL.CURRENT_TERM = CURR_BAL.CURRENT_TERM 
	LEFT JOIN TERM_BALANCE PREV_BAL
		ON CPL.CUSTOMER_ID = PREV_BAL.CUSTOMER_ID 
		AND CPL.POLICY_ID = PREV_BAL.POLICY_ID 
		AND CPL.CURRENT_TERM - 1  = PREV_BAL.CURRENT_TERM 
	INNER JOIN CLT_CUSTOMER_LIST CCL
		ON CPL.CUSTOMER_ID = CCL.CUSTOMER_ID
	INNER JOIN POL_POLICY_STATUS_MASTER  STATUS
		ON CPL.POLICY_STATUS = STATUS.POLICY_STATUS_CODE

	WHERE ISNULL(PREV_BAL.BALANCE ,0)  < 0 


END







GO

