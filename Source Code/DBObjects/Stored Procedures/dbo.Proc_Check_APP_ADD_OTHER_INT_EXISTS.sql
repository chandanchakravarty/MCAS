IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Check_APP_ADD_OTHER_INT_EXISTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Check_APP_ADD_OTHER_INT_EXISTS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO







/*----------------------------------------------------------
Proc Name          : Dbo.Proc_Check_APP_ADD_OTHER_INT_EXISTS
Created by         : Pradeep
Date               : 28/07/2005
Purpose            : To check whether this holder exists
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_Check_APP_ADD_OTHER_INT_EXISTS
(
	@CUSTOMER_ID Int,
	@APP_ID smallint,
	@APP_VERSION_ID smallint,
	@VEHICLE_ID int,
	@HOLDER_NAME NVarChar(70),
	@ADD_INT_ID int OUTPUT
)

AS
BEGIN
	
	SELECT @ADD_INT_ID = ADD_INT_ID
	FROM APP_ADD_OTHER_INT
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		VEHICLE_ID  = @VEHICLE_ID AND
		HOLDER_NAME = @HOLDER_NAME	
	
	IF ( @ADD_INT_ID IS NULL )
	BEGIN
		SET @ADD_INT_ID =  -1
	END
	
	RETURN

	

END






GO

