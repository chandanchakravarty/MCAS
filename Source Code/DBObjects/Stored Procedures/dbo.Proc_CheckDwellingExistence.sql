IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckDwellingExistence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckDwellingExistence]
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
CREATE  PROC Dbo.Proc_CheckDwellingExistence
(
	@CUSTOMER_ID Int,
	@APP_ID int,	
	@APP_VERSION_ID smallint,
	@LOCATION_ID smallint,
	@SUB_LOC_ID smallint
)

AS
BEGIN

DECLARE @DWELLING_ID smallint	
	
	SELECT @DWELLING_ID = DWELLING_ID
	FROM APP_DWELLINGS_INFO
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID AND	
		SUB_LOC_ID = @SUB_LOC_ID
	
	IF ( @DWELLING_ID IS NULL )
	BEGIN
		RETURN  -1
	END
	ELSE
	BEGIN
		RETURN @DWELLING_ID
	END
	

	

END






GO

