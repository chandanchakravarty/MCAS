IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactvateRealEstateLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactvateRealEstateLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
CREATE BY 		: Vijay Joshi
CREATED DATE	: 20 July 2005
Purpose			: To implement the activate deactivate functionlity in real estate locaton
*/
CREATE PROCEDURE Proc_ActivateDeactvateRealEstateLocation
(
	@CUSTOMER_ID 	INT,
	@APP_ID			INT,
	@APP_VERSION_ID	INT,
	@LOCATION_ID	INT,
	@STATUS			CHAR(1)
)
AS
BEGIN
	UPDATE APP_UMBRELLA_REAL_ESTATE_LOCATION

	SET IS_ACTIVE = @STATUS
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID 
END



GO

