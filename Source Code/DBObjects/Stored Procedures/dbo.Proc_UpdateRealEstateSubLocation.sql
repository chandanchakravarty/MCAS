IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRealEstateSubLocation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRealEstateSubLocation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_UpdateRealEstateSubLocation
Created by      :Priya
Date            : 5/30/2005
Purpose    	  :To update records in APP_UMBRELLA_REAL_ESTATE_SUB_LOC table
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateRealEstateSubLocation
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
	@SUB_LOC_OCC_DESC     	nvarchar(510),	
	@MODIFIED_BY     	int,
	@LAST_UPDATED_DATETIME     	datetime
)
AS
BEGIN
	/*Checking for duplcate sub loc number*/
	IF Not Exists(SELECT SUB_LOC_NUMBER FROM APP_UMBRELLA_REAL_ESTATE_SUB_LOC 
			WHERE SUB_LOC_NUMBER = @SUB_LOC_NUMBER AND
			CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID <> @SUB_LOC_ID)
	BEGIN

		UPDATE APP_UMBRELLA_REAL_ESTATE_SUB_LOC
		SET SUB_LOC_NUMBER = @SUB_LOC_NUMBER,
			SUB_LOC_TYPE = @SUB_LOC_TYPE,
			SUB_LOC_DESC = @SUB_LOC_DESC,
			SUB_LOC_CITY_LIMITS = @SUB_LOC_CITY_LIMITS,
			SUB_LOC_INTEREST = @SUB_LOC_INTEREST,
			SUB_LOC_OCCUPIED_PERCENT = @SUB_LOC_OCCUPIED_PERCENT,
			SUB_LOC_OCC_DESC = @SUB_LOC_OCC_DESC,
			MODIFIED_BY = @MODIFIED_BY,
			LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID = @SUB_LOC_ID
	END		
END





GO

