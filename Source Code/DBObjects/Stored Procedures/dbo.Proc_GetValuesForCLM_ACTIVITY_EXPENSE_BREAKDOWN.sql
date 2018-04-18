IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesForCLM_ACTIVITY_EXPENSE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesForCLM_ACTIVITY_EXPENSE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetValuesForCLM_ACTIVITY_EXPENSE_BREAKDOWN
Created by      : Vijay Arora
Date            : 6/6/2006
Purpose    	: To get the values from table named CLM_ACTIVITY_EXPENSE_BREAKDOWN
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetValuesForCLM_ACTIVITY_EXPENSE_BREAKDOWN
(
	@CLAIM_ID     int,
	@EXPENSE_ID     int,
	@EXPENSE_BREAKDOWN_ID     int,
	@ACTIVITY_ID int
)
AS
BEGIN
SELECT CLAIM_ID,
EXPENSE_ID,
EXPENSE_BREAKDOWN_ID,
TRANSACTION_CODE,
COVERAGE_ID,
PAID_AMOUNT
from  CLM_ACTIVITY_EXPENSE_BREAKDOWN
WHERE CLAIM_ID = @CLAIM_ID AND EXPENSE_ID = @EXPENSE_ID AND EXPENSE_BREAKDOWN_ID = @EXPENSE_BREAKDOWN_ID
AND ACTIVITY_ID = @ACTIVITY_ID
END


GO

