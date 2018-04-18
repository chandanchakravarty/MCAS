IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAutoIDCardUpdated]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAutoIDCardUpdated]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE Proc_GetAutoIDCardUpdated
(
	@CUSTOMER_ID	int,
	@APP_ID		int,
	@APP_VERSION_ID	int,
	@VEHICLE_ID		int
)
AS
BEGIN
	select 	isnull(IS_UPDATED,0) from APP_AUTO_ID_CARD_INFO
	where	CUSTOMER_ID	=	@CUSTOMER_ID AND
		APP_ID		=	@APP_ID AND
		APP_VERSION_ID	=	@APP_VERSION_ID AND
		VEHICLE_ID	= 	@VEHICLE_ID
END




GO

