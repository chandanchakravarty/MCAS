IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUMBRELLA_REC_VEH_COV]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUMBRELLA_REC_VEH_COV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO















/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertAPP_HOME_OWNER_REC_VEH_COV
Created by      : Pradeep
Date            : 5/18/2005
Purpose    	:Inserts a record in APP_HOME_OWNER_SUB_INSU
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE            PROC Dbo.Proc_InsertUMBRELLA_REC_VEH_COV
(
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@REC_VEH_ID smallint,
	@COVERAGE_ID int,
	@COVERAGE_CODE_ID NVarChar(50),
	@LIMIT DECIMAL(18,2),
	@DEDUCTIBLE DECIMAL(18,2),
	@WRITTEN_PREMIUM DECIMAL(18,2),
	@FULL_TERM_PREMIUM DECIMAL(18,2),
	@CREATED_BY     int
)
AS

DECLARE @COVERAGE_ID_MAX smallint

BEGIN
	
	IF ( @COVERAGE_ID = -1 )
	BEGIN
		
		SELECT  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 
		FROM APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES
		where CUSTOMER_ID = @CUSTOMER_ID and 
			APP_ID=@APP_ID and 
			APP_VERSION_ID = @APP_VERSION_ID 
			and REC_VEH_ID = @REC_VEH_ID

		INSERT INTO APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES
		(
			CUSTOMER_ID,
			APP_ID,
			APP_VERSION_ID,
			REC_VEH_ID,
			COVERAGE_ID,
			COVERAGE_CODE,
			LIMIT,
			DEDUCTIBLE,
			WRITTEN_PREMIUM,
			FULL_TERM_PREMIUM,
			IS_ACTIVE,
			CREATED_BY,
			CREATED_DATETIME
		)
		VALUES
		(
			@CUSTOMER_ID,
			@APP_ID,
			@APP_VERSION_ID,
			@REC_VEH_ID,
			@COVERAGE_ID_MAX,
			@COVERAGE_CODE_ID,
			@LIMIT,
			@DEDUCTIBLE,
			@WRITTEN_PREMIUM,
			@FULL_TERM_PREMIUM,
			'Y',
			@CREATED_BY,
			GetDate()
		)
	RETURN 1

	END	
	
	--Update
	UPDATE APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES
	SET
		CUSTOMER_ID = @CUSTOMER_ID,
		APP_ID =  @APP_ID,
		APP_VERSION_ID = @APP_VERSION_ID,
		REC_VEH_ID = @REC_VEH_ID,
		COVERAGE_ID = @COVERAGE_ID,
		COVERAGE_CODE = @COVERAGE_CODE_ID,
		LIMIT = @LIMIT,
		DEDUCTIBLE = @DEDUCTIBLE,
		WRITTEN_PREMIUM = @WRITTEN_PREMIUM, 
		FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM,
		MODIFIED_BY = @CREATED_BY,
		LAST_UPDATED_DATETIME = GetDate()
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	      APP_ID =  @APP_ID AND
	      APP_VERSION_ID =  @APP_VERSION_ID AND
	      REC_VEH_ID = @REC_VEH_ID AND			
	      COVERAGE_ID =  @COVERAGE_ID	
		
END
















GO

