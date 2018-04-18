IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFireProtInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFireProtInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetFireProtInformation
Created by         : Priya
Date               : 20/05/2005
Purpose            : To get details from APP_HOME_OWNER_FIRE_PROT_CLEAN
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetFireProtInformation
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID	smallint,
        @FUEL_ID       smallint
)

AS
BEGIN
	SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID,FUEL_ID,IS_SMOKE_DETECTOR,IS_PROTECTIVE_MAT_FLOOR,IS_PROTECTIVE_MAT_WALLS,
               PROT_MAT_SPACED,STOVE_SMOKE_PIPE_CLEANED,STOVE_CLEANER,REMARKS
		
	FROM APP_HOME_OWNER_FIRE_PROT_CLEAN
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		FUEL_ID =@FUEL_ID AND
		APP_VERSION_ID = @APP_VERSION_ID

END





GO

