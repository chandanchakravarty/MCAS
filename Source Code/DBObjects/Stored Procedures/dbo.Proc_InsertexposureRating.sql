IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertexposureRating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertexposureRating]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name        : dbo.Proc_InsertexposureRating
Created by       : Priya
Date             : 8/18/2005
Purpose    	  :To insert values in APP_GENERAL_EXPOSURE_RATING
Revison History :
Used In 	      : Wolverine
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertexposureRating
(
	@CUSTOMER_ID 	        int,
	@APP_ID     	        int,
	@APP_VERSION_ID         smallint,
	@EXPOSURE_RATING_ID     int OUTPUT,
	@EXPOSURE               DECIMAL(18,2),
	@ADDITIONAL_EXPOSURE    DECIMAL(18,2),
	@RATING_BASE            VARCHAR(25),
	@RATE                   NUMERIC,
	@IS_ACTIVE		nchar(1),
	@CREATED_BY     	int,
	@CREATE_DATETIME     	datetime
	
)
AS
BEGIN
	  select @EXPOSURE_RATING_ID= isnull(max(EXPOSURE_RATING_ID)+1,1) from APP_GENERAL_EXPOSURE_RATING		
          INSERT INTO APP_GENERAL_EXPOSURE_RATING
		(
			CUSTOMER_ID,
			APP_ID,
			APP_VERSION_ID,
			EXPOSURE_RATING_ID,
			EXPOSURE,
			ADDITIONAL_EXPOSURE,
			RATING_BASE,
			RATE,
			IS_ACTIVE,
			CREATED_BY,
			CREATE_DATETIME
		)
		VALUES
		(
			@CUSTOMER_ID,
			@APP_ID,
			@APP_VERSION_ID,
			@EXPOSURE_RATING_ID,
			@EXPOSURE,
			@ADDITIONAL_EXPOSURE,
			@RATING_BASE,
			@RATE,
			@IS_ACTIVE,
			@CREATED_BY,
			@CREATE_DATETIME
		)
END




GO

