IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyFireProtInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyFireProtInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetPolicyFireProtInformation
Created by         : Anurag Verma
Date               : 18/11/2005
Purpose            : To get details from POL_HOME_OWNER_FIRE_PROT_CLEAN
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetPolicyFireProtInformation
(
	@CUSTOMER_ID int,
	@POL_ID int,
	@POL_VERSION_ID	smallint,
        @FUEL_ID       smallint
)

AS
BEGIN
	SELECT CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,FUEL_ID,IS_SMOKE_DETECTOR,IS_PROTECTIVE_MAT_FLOOR,IS_PROTECTIVE_MAT_WALLS,
               PROT_MAT_SPACED,STOVE_SMOKE_PIPE_CLEANED,STOVE_CLEANER,REMARKS
		
	FROM POL_HOME_OWNER_FIRE_PROT_CLEAN
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		POLICY_ID = @POL_ID AND
		FUEL_ID =@FUEL_ID AND
		POLICY_VERSION_ID = @POL_VERSION_ID

END







GO

