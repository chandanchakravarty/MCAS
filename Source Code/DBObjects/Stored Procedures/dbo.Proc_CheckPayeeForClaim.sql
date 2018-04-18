IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPayeeForClaim]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPayeeForClaim]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------
Proc Name       : dbo.Proc_CheckPayeeForClaim
Created by      : Sumit Chhabra
Date            : 04/25/2007
Purpose        : Checks for the existence of payee for an activity
Revison History :
Used In  : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC dbo.Proc_CheckPayeeForClaim
CREATE PROC dbo.Proc_CheckPayeeForClaim
(
 @CLAIM_ID     int,
 @ACTIVITY_ID int
)
AS
BEGIN

DECLARE @ACTIVITY_REASON INT
DECLARE @RECOVERY_ACTIVITY INT
SET @RECOVERY_ACTIVITY = 11776

SELECT @ACTIVITY_REASON = ACTIVITY_REASON FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID

	IF(@ACTIVITY_REASON=@RECOVERY_ACTIVITY)
	BEGIN
		IF EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY_RECOVERY_PAYER WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)
			RETURN 1
		ELSE
			RETURN 0
	END
	ELSE
	BEGIN
		IF EXISTS(SELECT CLAIM_ID FROM CLM_PAYEE WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID)
			RETURN 1
		ELSE
			RETURN 0
	END
END






GO

