IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertProc_InsertAPP_HOME_OWNER_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertProc_InsertAPP_HOME_OWNER_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertAPP_HOME_OWNER_GEN_INFO
Created by      : Anshuman
Date            : 5/18/2005
Purpose    	: Insert record in table APP_HOME_OWNER_GEN_INFO
Revison History :
Used In 	: Brics
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertProc_InsertAPP_HOME_OWNER_GEN_INFO
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@ANY_FARMING_BUSINESS_COND     nchar(2),
	@DESC_BUSINESS     nvarchar(300),
	@ANY_RESIDENCE_EMPLOYEE     nchar(2),
	@DESC_RESIDENCE_EMPLOYEE     nvarchar(300),
	@ANY_OTHER_RESI_OWNED     nchar(2),
	@DESC_OTHER_RESIDENCE     nvarchar(300),
	@ANY_OTH_INSU_COMP     nchar(2),
	@DESC_OTHER_INSURANCE     nvarchar(300),
	@HAS_INSU_TRANSFERED_AGENCY     nchar(2),
	@DESC_INSU_TRANSFERED_AGENCY     nvarchar(300),
	@ANY_COV_DECLINED_CANCELED     nchar(2),
	@DESC_COV_DECLINED_CANCELED     nvarchar(300),
	@ANIMALS_EXO_PETS_HISTORY     nchar(2),
	@BREED     nvarchar(200),
	@OTHER_DESCRIPTION     nvarchar(200),
	
	@CONVICTION_DEGREE_IN_PAST     nchar(2),
	@IS_ACTIVE     nchar(2),
	@CREATED_BY     int,
	@CREATED_DATETIME     datetime
)
AS
BEGIN
	INSERT INTO APP_HOME_OWNER_GEN_INFO
	(
		CUSTOMER_ID,
		APP_ID,
		APP_VERSION_ID,
		ANY_FARMING_BUSINESS_COND,
		DESC_BUSINESS,
		ANY_RESIDENCE_EMPLOYEE,
		DESC_RESIDENCE_EMPLOYEE,
		ANY_OTHER_RESI_OWNED,
		DESC_OTHER_RESIDENCE,
		ANY_OTH_INSU_COMP,
		DESC_OTHER_INSURANCE,
		HAS_INSU_TRANSFERED_AGENCY,
		DESC_INSU_TRANSFERED_AGENCY,
		ANY_COV_DECLINED_CANCELED,
		DESC_COV_DECLINED_CANCELED,
		ANIMALS_EXO_PETS_HISTORY,
		BREED,
		OTHER_DESCRIPTION,
		
		CONVICTION_DEGREE_IN_PAST,
		IS_ACTIVE,
		CREATED_BY,
		CREATED_DATETIME
	)
	VALUES
	(
		@CUSTOMER_ID,
		@APP_ID,
		@APP_VERSION_ID,
		@ANY_FARMING_BUSINESS_COND,
		@DESC_BUSINESS,
		@ANY_RESIDENCE_EMPLOYEE,
		@DESC_RESIDENCE_EMPLOYEE,
		@ANY_OTHER_RESI_OWNED,
		@DESC_OTHER_RESIDENCE,
		@ANY_OTH_INSU_COMP,
		@DESC_OTHER_INSURANCE,
		@HAS_INSU_TRANSFERED_AGENCY,
		@DESC_INSU_TRANSFERED_AGENCY,
		@ANY_COV_DECLINED_CANCELED,
		@DESC_COV_DECLINED_CANCELED,
		@ANIMALS_EXO_PETS_HISTORY,
		@BREED,
		@OTHER_DESCRIPTION,
		
		@CONVICTION_DEGREE_IN_PAST,
		@IS_ACTIVE,
		@CREATED_BY,
		@CREATED_DATETIME
	)
END

GO

