IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateSubLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateSubLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateSubLocation
Created by      : Vijay Joshi
Date            : 5/12/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateSubLocation
(
	@CUSTOMER_ID 	int,
	@APP_ID     	int,
	@APP_VERSION_ID smallint,
	@LOCATION_ID    smallint,
	@SUB_LOC_ID     smallint,
	@SUB_LOC_NUMBER int,
	@SUB_LOC_TYPE   nchar(30),
	@SUB_LOC_DESC   nvarchar(200),
	@SUB_LOC_CITY_LIMITS    int,
	@SUB_LOC_INTEREST	int,
	@SUB_LOC_OCCUPIED_PERCENT real,
	--@SUB_LOC_YEAR_BUILT    	int,
	--@SUB_LOC_AREA_IN_SQ_FOOT varchar(10),
	--@SUB_LOC_PROT_CLASS    	varchar(10),
	--@SUB_LOC_HYDRANT_DIST   real,
	--@SUB_LOC_FIRE_DIST     	real,
	--@SUB_LOC_INSIDE_CITY_LIMITS    	nchar(2),
	@SUB_LOC_OCC_DESC     	nvarchar(255),
	--@SUB_LOC_SURROUND_EXP   nvarchar(510),	
	@MODIFIED_BY     	int,
	@LAST_UPDATED_DATETIME     	datetime
)
AS
BEGIN
	/*Checking for duplcate sub loc number*/
	IF  Exists(SELECT SUB_LOC_NUMBER FROM APP_SUB_LOCATIONS 
			WHERE SUB_LOC_NUMBER = @SUB_LOC_NUMBER AND
			CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID <> @SUB_LOC_ID)
	BEGIN
		RETURN -1
	END

	BEGIN

		UPDATE APP_SUB_LOCATIONS
		SET SUB_LOC_NUMBER = @SUB_LOC_NUMBER,
			SUB_LOC_TYPE = @SUB_LOC_TYPE,
			SUB_LOC_DESC = @SUB_LOC_DESC,
			SUB_LOC_CITY_LIMITS = @SUB_LOC_CITY_LIMITS,
			SUB_LOC_INTEREST = @SUB_LOC_INTEREST,
			SUB_LOC_OCCUPIED_PERCENT = @SUB_LOC_OCCUPIED_PERCENT,
			--SUB_LOC_YEAR_BUILT = @SUB_LOC_YEAR_BUILT,
			--SUB_LOC_AREA_IN_SQ_FOOT = @SUB_LOC_AREA_IN_SQ_FOOT,
			--SUB_LOC_PROT_CLASS = @SUB_LOC_PROT_CLASS,
			--SUB_LOC_HYDRANT_DIST = @SUB_LOC_HYDRANT_DIST,
			--SUB_LOC_FIRE_DIST = @SUB_LOC_FIRE_DIST,
			--SUB_LOC_INSIDE_CITY_LIMITS = @SUB_LOC_INSIDE_CITY_LIMITS,
			SUB_LOC_OCC_DESC = @SUB_LOC_OCC_DESC,
			--SUB_LOC_SURROUND_EXP = @SUB_LOC_SURROUND_EXP,
			MODIFIED_BY = @MODIFIED_BY,
			LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID = @SUB_LOC_ID
		
		RETURN 1
	END		
END









GO

