IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckApplicationExistence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckApplicationExistence]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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

CREATE  PROC Dbo.Proc_CheckApplicationExistence
(
	@CUSTOMER_ID Int,
	@APP_NUMBER NVarChar(25),
	@APP_VERSION NVarChar(4),
	@APP_ID int OUTPUT,
	@APP_VERSION_ID smallint output
)

AS
BEGIN

DECLARE @L_APP_ID Int
DECLARE @L_APP_VERSION_ID SmallInt
IF (@APP_NUMBER='')
BEGIN
 SET @APP_NUMBER = NULL
END  
	
	
	SELECT @L_APP_ID = APP_ID,
		@L_APP_VERSION_ID = APP_VERSION_ID
	FROM APP_LIST
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_NUMBER = @APP_NUMBER AND
		APP_VERSION = @APP_VERSION	
	
	IF ( @L_APP_ID IS NULL )
	BEGIN
		SET @APP_ID =  -1
	END
	ELSE
	BEGIN
		SET @APP_ID = @L_APP_ID
		SET @APP_VERSION_ID = @L_APP_VERSION_ID
	END
	

	

END








GO

