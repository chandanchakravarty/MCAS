IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStatus_ACT_CHECK_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStatus_ACT_CHECK_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetStatus_ACT_CHECK_INFORMATION
Created by      : Ajit Singh Chahal
Rule			: a check can be committed only if fully distributed.
Date            : 6/28/2005
Purpose    		: Returns status of check
Revison History :
Used In 		: Wolverine
Status values	: N-Not distributed, D-Distributed, C-Committed
Check Types : 
2472 - Agency Commission Checks
2474 - Premium Refund Checks for Return Premium Payment
9935 - Premium Refund Checks for Over Payment
9936 - Premium Refund Checks for Suspense Amount
9937 - Claims Checks
9938 - Vendor Checks
9940 - Miscellaneous (Other) Checks
9945 - Reinsurance Premium Checks

------------------------------------------------------------
Proc_GetStatus_ACT_CHECK_INFORMATION 386
------   ------------       -------------------------*/
-- drop proc dbo.Proc_GetStatus_ACT_CHECK_INFORMATION
CREATE PROC dbo.Proc_GetStatus_ACT_CHECK_INFORMATION
(
@CHECK_ID     int
)
AS
BEGIN
DECLARE @RETVALUE VARCHAR(5) ,
@ISCOMMITED CHAR(1),
@REMAININGAMOUNT DECIMAL(18,2)

SELECT @REMAININGAMOUNT=(CHK.CHECK_AMOUNT - 
ISNULL((SELECT SUM(DISTRIBUTION_AMOUNT) FROM ACT_DISTRIBUTION_DETAILS (NOLOCK)
WHERE GROUP_TYPE = 'CHQ' AND GROUP_ID = CHK.CHECK_ID),0)) 
FROM ACT_CHECK_INFORMATION CHK (NOLOCK) WHERE CHECK_ID = @CHECK_ID

 --CHECK TYPE IS NOT OF 'OTHER TYPE' AND OF 'TAX' 9939
IF NOT EXISTS (SELECT CHECK_ID FROM ACT_CHECK_INFORMATION (NOLOCK) 
--WHERE CHECK_ID = @CHECK_ID AND CHECK_TYPE IN  (9940,9941,9942,9943,9939)) 
--WHERE CHECK_ID = @CHECK_ID AND CHECK_TYPE IN  (2472,2474,9935,9936,9937,9938,9940,9945))
WHERE CHECK_ID = @CHECK_ID AND CHECK_TYPE IN  (9940,9945))
BEGIN
	SELECT @ISCOMMITED=IS_COMMITED FROM  ACT_CHECK_INFORMATION (NOLOCK) WHERE CHECK_ID = @CHECK_ID
		IF @ISCOMMITED='Y'
	         BEGIN
			SET @RETVALUE = 'C'
		 END
		ELSE
			SET @RETVALUE = 'D'
END
ELSE 
BEGIN
	IF @REMAININGAMOUNT=0 --CHECK IS TO BE DISTRIBUTED ONLY IN OTHER & TAX CASE I.E. IF CHECK TYPE IN ONE OF 9940,9941,9942,9943,9939
	BEGIN
		SELECT @ISCOMMITED=IS_COMMITED FROM  ACT_CHECK_INFORMATION (NOLOCK) WHERE CHECK_ID = @CHECK_ID
		IF @ISCOMMITED='Y'
	         BEGIN
			SET @RETVALUE = 'C'
		 END
		ELSE
				SET @RETVALUE = 'D'
	END
	ELSE
		SET @RETVALUE = 'N'
	END
SELECT @RETVALUE AS STATUS 
END














GO

