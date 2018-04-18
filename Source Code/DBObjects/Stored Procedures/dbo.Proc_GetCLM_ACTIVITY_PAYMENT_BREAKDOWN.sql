IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_PAYMENT_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_PAYMENT_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                              
Proc Name       : dbo.'Proc_GetCLM_ACTIVITY_PAYMENT_BREAKDOWN'                                                                  
Created by      : Sumit Chhabra                                                                            
Date            : 05/06/2006                                                                              
Purpose         : Fetch Claim Payment Breakdown data
Created by      : Sumit Chhabra                                                                             
Revison History :                                                                              
Used In        : Wolverine                                                                              
------------------------------------------------------------                                                                              
Date     Review By          Comments                                                                              
------   ------------       -------------------------*/                                                                              
CREATE PROC dbo.Proc_GetCLM_ACTIVITY_PAYMENT_BREAKDOWN
@CLAIM_ID int,
@ACTIVITY_ID int,
@PAYMENT_BREAKDOWN_ID int
AS                                                                              
BEGIN       

SELECT
		CLAIM_ID,ACTIVITY_ID,PAYMENT_BREAKDOWN_ID,TRANSACTION_CODE,COVERAGE_ID,PAID_AMOUNT,IS_ACTIVE
FROM
	CLM_ACTIVITY_PAYMENT_BREAKDOWN
WHERE
	CLAIM_ID=@CLAIM_ID AND
	ACTIVITY_ID=@ACTIVITY_ID AND
	PAYMENT_BREAKDOWN_ID=@PAYMENT_BREAKDOWN_ID	
END


GO

