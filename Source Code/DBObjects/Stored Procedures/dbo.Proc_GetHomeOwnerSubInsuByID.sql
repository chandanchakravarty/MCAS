IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeOwnerSubInsuByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeOwnerSubInsuByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetHomeOwnerSubInsuByID
Created by      : Pradeep
Date            : 5/18/2005
Purpose    	:Inserts a record in APP_HOME_OWNER_SUB_INSU
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_GetHomeOwnerSubInsuByID
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@SUB_INSU_ID Int	
)
AS

BEGIN
	SELECT 
		SUB_INSU_ID,
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
		CREATED_DATETIME,
		MODIFIED_BY,
		LAST_UPDATED_DATETIME

	FROM APP_HOME_OWNER_SUB_INSU
	WHERE CUSTOMER_ID = CUSTOMER_ID AND
	      APP_ID = APP_ID AND
	      APP_VERSION_ID = APP_VERSION_ID AND
	      SUb_INSU_ID = @SUB_INSU_ID	

END






GO

