IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertRealEstateSubLoc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertRealEstateSubLoc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name        : dbo.SubLocation
Created by       : Priya
Date             : 5/30/2005
Purpose    	  :To insert values in APP_UMBRELLA_REAL_ESTATE_SUB_LOC
Revison History :
Used In 	      : Wolverine
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertRealEstateSubLoc
(
	@CUSTOMER_ID 	    int,
	@APP_ID     	    int,
	@APP_VERSION_ID     smallint,
	@LOCATION_ID        smallint,
	@SUB_LOC_ID         smallint	OUTPUT,
	@SUB_LOC_NUMBER     int,
	@SUB_LOC_TYPE       nchar(30),
	@SUB_LOC_DESC        nvarchar(200),
	@SUB_LOC_CITY_LIMITS    int,
	@SUB_LOC_INTEREST	int,
	@SUB_LOC_OCCUPIED_PERCENT real,
	@SUB_LOC_OCC_DESC     	nvarchar(510),
	@CREATED_BY     	int,
	@CREATED_DATETIME     	datetime
)
AS
BEGIN
	/*Checking the duplicate sub location number*/
	IF Not Exists(SELECT SUB_LOC_NUMBER FROM APP_UMBRELLA_REAL_ESTATE_SUB_LOC 
		WHERE SUB_LOC_NUMBER = @SUB_LOC_NUMBER AND
		CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID)
	BEGIN

		/*Retreiving the maximum SUB LOCATION ID ans setting it in output variable*/
		SELECT @SUB_LOC_ID = IsNull(Max(SUB_LOC_ID),0) + 1 FROM APP_UMBRELLA_REAL_ESTATE_SUB_LOC
	
		INSERT INTO APP_UMBRELLA_REAL_ESTATE_SUB_LOC
		(
			CUSTOMER_ID,
			APP_ID,
			APP_VERSION_ID,
			LOCATION_ID,
			SUB_LOC_ID,
			SUB_LOC_NUMBER,
			SUB_LOC_TYPE,
			SUB_LOC_DESC,
			SUB_LOC_CITY_LIMITS,
			SUB_LOC_INTEREST,
			SUB_LOC_OCCUPIED_PERCENT,
			SUB_LOC_OCC_DESC,
			IS_ACTIVE,
			CREATED_BY,
			CREATED_DATETIME
		)
		VALUES
		(
			@CUSTOMER_ID,
			@APP_ID,
			@APP_VERSION_ID,
			@LOCATION_ID,
			@SUB_LOC_ID,
			@SUB_LOC_NUMBER,
			@SUB_LOC_TYPE,
			@SUB_LOC_DESC,
			@SUB_LOC_CITY_LIMITS,
			@SUB_LOC_INTEREST,
			@SUB_LOC_OCCUPIED_PERCENT,
			@SUB_LOC_OCC_DESC,
			'Y',
			@CREATED_BY,
			@CREATED_DATETIME
		)
	END
END





GO

