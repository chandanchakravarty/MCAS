IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_SUBLOCATION_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_SUBLOCATION_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.SubLocation
Created by      : Pradeep Iyer
Date            : 17/8/2005
Purpose    	  :Inserrts or updates records in APP_SUB_LOCATION
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     	Review By          Comments
17 may,2005	Vijay		Checking Duplicay of code for specified customer,app and version
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_Save_SUBLOCATION_ACORD
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
	@CREATED_BY     	int,
	@CREATED_DATETIME     	datetime,
	@SUB_LOC_OCC_DESC     	nvarchar(255),
	@MODIFIED_BY     	int,
	@LAST_UPDATED_DATETIME     	datetime
)
AS
BEGIN
	DECLARE @SUB_LOC_ID_EXISTS smallint

	/*Checking the duplicate sub location number*/
	SELECT @SUB_LOC_ID_EXISTS = SUB_LOC_ID 
		FROM APP_SUB_LOCATIONS 
		WHERE 
		CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID AND
		SUB_LOC_DESC = @SUB_LOC_DESC	
	
	IF @SUB_LOC_ID_EXISTS IS NULL 	
	BEGIN
		/*Retreiving the maximum SUB LOCATION ID ans setting it in output variable*/
		SELECT @SUB_LOC_ID = IsNull(Max(SUB_LOC_ID),0) + 1 
		FROM APP_SUB_LOCATIONS
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID
		
		SELECT @SUB_LOC_NUMBER = IsNull(Max(SUB_LOC_NUMBER),0) + 1 
		FROM APP_SUB_LOCATIONS
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		LOCATION_ID = @LOCATION_ID
	
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
	ELSE
	BEGIN
		UPDATE APP_SUB_LOCATIONS
		SET 
			SUB_LOC_TYPE = @SUB_LOC_TYPE,
			SUB_LOC_DESC = @SUB_LOC_DESC,
			SUB_LOC_CITY_LIMITS = @SUB_LOC_CITY_LIMITS,
			SUB_LOC_INTEREST = @SUB_LOC_INTEREST,
			SUB_LOC_OCCUPIED_PERCENT = @SUB_LOC_OCCUPIED_PERCENT,
			
			SUB_LOC_OCC_DESC = @SUB_LOC_OCC_DESC,
			--SUB_LOC_SURROUND_EXP = @SUB_LOC_SURROUND_EXP,
			MODIFIED_BY = @MODIFIED_BY,
			LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND
			APP_ID = @APP_ID AND
			APP_VERSION_ID = @APP_VERSION_ID AND
			LOCATION_ID = @LOCATION_ID AND
			SUB_LOC_ID = @SUB_LOC_ID_EXISTS
		
		SET @SUB_LOC_ID = @SUB_LOC_ID_EXISTS
	END
END





GO

