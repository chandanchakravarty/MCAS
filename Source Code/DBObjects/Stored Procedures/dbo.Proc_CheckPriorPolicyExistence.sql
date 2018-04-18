IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckPriorPolicyExistence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckPriorPolicyExistence]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO







/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckApplicationExistence
Created by         : Pradeep
Date               : 05/05/2005
Purpose            : To check whether this customer exists
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_CheckPriorPolicyExistence
(
	@CUSTOMER_ID Int,
	@OLD_POLICY_NUMBER NVarChar(75),
	@CARRIER NVarChar(100),
	@LOB NVarChar(5),
	@APP_PRIOR_CARRIER_INFO_ID smallint output
)

AS
BEGIN
		
DECLARE @L_ID SmallInt

	
	
	SELECT @L_ID = APP_PRIOR_CARRIER_INFO_ID
	FROM APP_PRIOR_CARRIER_INFO
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		OLD_POLICY_NUMBER = @OLD_POLICY_NUMBER
	
	IF ( @L_ID IS NULL )
	BEGIN
		SET @APP_PRIOR_CARRIER_INFO_ID =  -1
		RETURN
	END
	ELSE
	BEGIN
		SET @APP_PRIOR_CARRIER_INFO_ID = @L_ID
		RETURN
	END
	

	

END






GO

