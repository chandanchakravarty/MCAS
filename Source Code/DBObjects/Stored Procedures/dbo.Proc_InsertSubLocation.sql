IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertSubLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertSubLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.SubLocation
Created by      : Vijay Joshi
Date            : 5/12/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     	Review By          Comments
17 may,2005	Vijay		Checking Duplicay of code for specified customer,app and version
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertSubLocation
(
	@CUSTOMER_ID 	int,
	@APP_ID     	int,
	@APP_VERSION_ID smallint,
	@LOCATION_ID    smallint,
	@SUB_LOC_ID     smallint	OUTPUT,
	@SUB_LOC_NUMBER int,
	@SUB_LOC_TYPE   nchar(15),
	@SUB_LOC_DESC   nvarchar(100),
	@SUB_LOC_CITY_LIMITS    int,
	@SUB_LOC_INTEREST	int,
	@SUB_LOC_OCCUPIED_PERCENT real,
	--@SUB_LOC_YEAR_BUILT    	int,
	--@SUB_LOC_AREA_IN_SQ_FOOT varchar(10),
	--@SUB_LOC_PROT_CLASS    	varchar(10),
	--@SUB_LOC_HYDRANT_DIST   real,
	--@SUB_LOC_FIRE_DIST     	real,
	--@SUB_LOC_INSIDE_CITY_LIMITS    	nchar(2),
	@SUB_LOC_OCC_DESC     	varchar(255),
	--@SUB_LOC_SURROUND_EXP   nvarchar(510),	
	@CREATED_BY     	int,
	@CREATED_DATETIME     	datetime
)
AS
BEGIN
	/*Checking the duplicate sub location number*/
	IF  Exists(SELECT SUB_LOC_NUMBER FROM APP_SUB_LOCATIONS 
		WHERE SUB_LOC_NUMBER = @SUB_LOC_NUMBER AND
		CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID	
		)
	BEGIN
		SET @SUB_LOC_ID = -1
		RETURN -1
	END
	ELSE
	BEGIN

		/*Retreiving the maximum SUB LOCATION ID ans setting it in output variable*/
		SELECT @SUB_LOC_ID = IsNull(Max(SUB_LOC_ID),0) + 1 
		FROM APP_SUB_LOCATIONS
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID
	
		INSERT INTO APP_SUB_LOCATIONS
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
			--SUB_LOC_YEAR_BUILT,
			--SUB_LOC_AREA_IN_SQ_FOOT,
			--SUB_LOC_PROT_CLASS,
			--SUB_LOC_HYDRANT_DIST,
			--SUB_LOC_FIRE_DIST,
			--SUB_LOC_INSIDE_CITY_LIMITS,
			SUB_LOC_OCC_DESC,
			--SUB_LOC_SURROUND_EXP,
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
			--@SUB_LOC_YEAR_BUILT,
			--@SUB_LOC_AREA_IN_SQ_FOOT,
			--@SUB_LOC_PROT_CLASS,
			--@SUB_LOC_HYDRANT_DIST,
			--@SUB_LOC_FIRE_DIST,
			--@SUB_LOC_INSIDE_CITY_LIMITS,
			@SUB_LOC_OCC_DESC,
			--@SUB_LOC_SURROUND_EXP,
			'Y',
			@CREATED_BY,
			@CREATED_DATETIME
		)
	END
END




GO

