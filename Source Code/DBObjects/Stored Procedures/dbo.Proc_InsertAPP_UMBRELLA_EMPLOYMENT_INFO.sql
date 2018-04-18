IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_UMBRELLA_EMPLOYMENT_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_UMBRELLA_EMPLOYMENT_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertAPP_UMBRELLA_EMPLOYMENT_INFO
Created by      : Pradeep
Date            : 5/30/2005
Purpose    	:Inserts record in Employment
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/


CREATE   PROC Dbo.Proc_InsertAPP_UMBRELLA_EMPLOYMENT_INFO
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID smallint,
	@APP_OCCUPATION smallint,
	@CO_APP_OCCUPATION smallint
)

AS
BEGIN
	
	IF EXISTS
	(
		SELECT * FROM APP_UMBRELLA_EMPLOYMENT_INFO
		WHERE
		CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID
	)
	BEGIN
		RETURN -1
	END

	INSERT INTO APP_UMBRELLA_EMPLOYMENT_INFO
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		APP_OCCUPATION,
		CO_APP_OCCUPATION
	)
	VALUES
	(
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@APP_OCCUPATION,
		@CO_APP_OCCUPATION
	)

	RETURN 1

END






GO

