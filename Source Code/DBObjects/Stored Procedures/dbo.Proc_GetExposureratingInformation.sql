IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetExposureratingInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetExposureratingInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetExposureratingInformation
Created by         : Priya
Date               : 19/05/2005
Purpose            : To get details from APP_GENERAL_EXPOSURE_RATING
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_GetExposureratingInformation
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID	smallint
       
)

AS
BEGIN
	SELECT CUSTOMER_ID, APP_ID, APP_VERSION_ID,EXPOSURE_RATING_ID,
               EXPOSURE,ADDITIONAL_EXPOSURE,RATING_BASE,
		RATE
	              
		
	FROM APP_GENERAL_EXPOSURE_RATING
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID

END




GO

