IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_HOME_OWNER_SUB_INSU]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_HOME_OWNER_SUB_INSU]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertAPP_HOME_OWNER_SUB_INSU
Created by      : Pradeep
Date            : 5/18/2005
Purpose    	:Inserts a record in APP_HOME_OWNER_SUB_INSU
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE       PROC Dbo.Proc_InsertAPP_HOME_OWNER_SUB_INSU
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@IS_BLANKET_COV     nchar(1),
	@BLANKET_APPLY_TO     nchar(1),
	@LOCATION_ID     smallint,
	@SUB_LOC_ID     smallint,
	@SUBJECT_OF_INSURANCE     int,
	@AMOUNT     decimal(18,2),
	@OTHERS_DESC     nvarchar(510),
	@DEDUCTIBLE     decimal(18,2),
	@FORMS_CONDITIONS_APPLY     nvarchar(255),
	@IS_PROPERTY_EVER_RENTED     nchar(1),
	@KEEP_WHEN_NOT_IN_USE     nchar(1),
	@CREATED_BY     int
)
AS

DECLARE @LOCATIONID Smallint
DECLARE @SUBLOCATIONID smallint

BEGIN
	
	IF EXISTS
	(
		SELECT * FROM APP_HOME_OWNER_SUB_INSU
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID = @SUB_LOC_ID AND
			SUBJECT_OF_INSURANCE = @SUBJECT_OF_INSURANCE	
	)
	BEGIN
		RETURN -1 
	END
	
	--Set null  the relevant fields
	IF @IS_BLANKET_COV = 'Y' AND @BLANKET_APPLY_TO = 'Y'
	BEGIN
		SET @LOCATION_ID = NULL
		SET @SUB_LOC_ID = NULL
		
	END
	
	IF @IS_BLANKET_COV = 'Y' AND @BLANKET_APPLY_TO = 'N'
	BEGIN
		SET @SUB_LOC_ID = NULL
		
	END
	---	

	INSERT INTO APP_HOME_OWNER_SUB_INSU
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		IS_BLANKET_COV,
		BLANKET_APPLY_TO,
		LOCATION_ID,
		SUB_LOC_ID,
		SUBJECT_OF_INSURANCE,
		AMOUNT,
		OTHERS_DESC,
		DEDUCTIBLE,
		FORMS_CONDITIONS_APPLY,
		IS_PROPERTY_EVER_RENTED,
		KEEP_WHEN_NOT_IN_USE,
		IS_ACTIVE,
		CREATED_BY,
		CREATED_DATETIME

	)
	VALUES
	(
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@IS_BLANKET_COV,
		@BLANKET_APPLY_TO,
		@LOCATION_ID,
		@SUB_LOC_ID,
		@SUBJECT_OF_INSURANCE,
		@AMOUNT,
		@OTHERS_DESC,
		@DEDUCTIBLE,
		@FORMS_CONDITIONS_APPLY,
		@IS_PROPERTY_EVER_RENTED,
		@KEEP_WHEN_NOT_IN_USE,
		'Y',
		@CREATED_BY,
		GetDate()		
	)
	
	IF @@ERROR <> 0
	BEGIN
		RETURN -2
	END

	RETURN @@IDENTITY

END










GO

