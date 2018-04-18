IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_GENERAL_EXPOSURE_RATING]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_GENERAL_EXPOSURE_RATING]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo. Proc_UpdateGENERAL_EXPOSURE_RATING
Created by      : Priya
Date            : 8/22/2005
Purpose    	  Updating record to APP_GENERAL_EXPOSURE_RATING
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE  Proc_UpdateAPP_GENERAL_EXPOSURE_RATING
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int,
	@EXPOSURE_RATING_ID int,
	@EXPOSURE DECIMAL(18,2),
	@ADDITIONAL_EXPOSURE DECIMAL(18,2),
	@RATING_BASE VARCHAR(25),
	@RATE NUMERIC,
	@IS_ACTIVE	nchar(1),
	@MODIFIED_BY int,
	@LAST_UPDATED_DATETIME datetime
	
)
AS
BEGIN
UPDATE  APP_GENERAL_EXPOSURE_RATING
SET
EXPOSURE=@EXPOSURE,
ADDITIONAL_EXPOSURE=@ADDITIONAL_EXPOSURE,
RATING_BASE=@RATING_BASE,
RATE=@RATE,
IS_ACTIVE=@IS_ACTIVE,
MODIFIED_BY=@MODIFIED_BY,
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME


WHERE  CUSTOMER_ID=@CUSTOMER_ID 
AND        APP_ID=@APP_ID 
AND        APP_VERSION_ID=@APP_VERSION_ID
AND       EXPOSURE_RATING_ID=@EXPOSURE_RATING_ID
END




GO

