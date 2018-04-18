IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_HOME_OWNER_SCH_ITEMS_CVGS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_HOME_OWNER_SCH_ITEMS_CVGS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.APP_HOME_OWNER_SCH_ITEMS_CVGS
Created by      : Vijay Joshi
Date            : 5/18/2005
Purpose    	:Insert values in APP_HOME_OWNER_SCH_ITEMS_CVGS Table
Revison History :
Used In 	: Wolverine
Modify By	:Vijay
Modify On	: 1 June,2005
Purpose		: Change datatype of category from nchar to int
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateAPP_HOME_OWNER_SCH_ITEMS_CVGS
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@ITEM_ID     smallint,
	@CATEGORY    int,
	@DETAILED_DESC     nvarchar(510),
	@SN_DETAILS     nvarchar(50),
	@AMOUNT_OF_INSURANCE     decimal(9),
	@PREMIUM     decimal(9),
	@RATE     decimal(9),
	@APPRAISAL     nchar(2),
	@APPRAISAL_DESC  varchar(50),
	@PURCHASE_APPRAISAL_DATE     datetime,
	@BREAKAGE_COVERAGE     nchar(2),
	@BREAKAGE_DESC varchar(50),
	@MODIFIED_BY     int,
	@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
	UPDATE APP_HOME_OWNER_SCH_ITEMS_CVGS
		SET CATEGORY = @CATEGORY,
		DETAILED_DESC = @DETAILED_DESC, 
		SN_DETAILS = @SN_DETAILS, 
		AMOUNT_OF_INSURANCE = @AMOUNT_OF_INSURANCE, 
		PREMIUM = @PREMIUM,
		RATE = @RATE, 
		APPRAISAL = @APPRAISAL, 
		APPRAISAL_DESC =@APPRAISAL_DESC ,
		PURCHASE_APPRAISAL_DATE = @PURCHASE_APPRAISAL_DATE, 
		BREAKAGE_COVERAGE = @BREAKAGE_COVERAGE,
		BREAKAGE_DESC=@BREAKAGE_DESC,
		MODIFIED_BY = @MODIFIED_BY, 
		LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		APP_ID = @APP_ID AND
		APP_VERSION_ID = @APP_VERSION_ID AND
		ITEM_ID = @ITEM_ID
END


GO

