IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE
Created by         : Pradeep
Date               : 05/05/2005
Purpose            : To check whether this customer exists
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--DROP PROC Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE
CREATE  PROC dbo.Proc_CheckAPP_DRIVER_DETAILS_EXISTENCE
(
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID Int,
	@DRIVER_FNAME NVarChar(25),
	@DRIVER_LNAME NVarChar(25),
	@DRIVER_MNAME NVarChar(25),
	@DRIVER_ID int OUTPUT
)

AS
BEGIN

	
	
	SELECT @DRIVER_ID = DRIVER_ID
	FROM APP_DRIVER_DETAILS
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		DRIVER_FNAME = RTRIM(LTRIM(@DRIVER_FNAME)) AND
		DRIVER_LNAME = RTRIM(LTRIM(@DRIVER_LNAME)) AND
        DRIVER_MNAME = RTRIM(LTRIM(@DRIVER_MNAME))  
	
	IF ( @DRIVER_ID IS NULL )
	BEGIN
		SET @DRIVER_ID =  -1
	END
	
	

	

END








GO

