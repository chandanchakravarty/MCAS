IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POLICY_HOME_SECTION1_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POLICY_HOME_SECTION1_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_SAVE_POLICY_HOME_SECTION1_COVERAGES
Created by      : Anurag verma
Date            : 15/11/2005
Purpose    	:Inserts a record in Pol_DWELLING_SECTION1_COVERAGES
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE           PROC Dbo.Proc_SAVE_POLICY_HOME_SECTION1_COVERAGES
(
	@CUSTOMER_ID     int,
	@POL_ID     int,
	@POL_VERSION_ID     smallint,
	@DWELLING_ID smallint,
	@COVERAGE_ID int,
	@COVERAGE_CODE_ID int,
	@LIMIT_1 Decimal(18,2),
	@LIMIT_2 Decimal(18,2),
	@LIMIT_1_TYPE NVarChar(5),
	@LIMIT_2_TYPE NVarChar(5),
	@DEDUCTIBLE_1 DECIMAL(18,2),
	@DEDUCTIBLE_2 DECIMAL(18,2),
	@DEDUCTIBLE_1_TYPE NVarChar(5),
	@DEDUCTIBLE_2_TYPE NVarChar(5),
	@WRITTEN_PREMIUM DECIMAL(18,2),
	@FULL_TERM_PREMIUM DECIMAL(18,2),
	@COVERAGE_TYPE nchar(10)
)
AS

DECLARE @COVERAGE_ID_MAX smallint
BEGIN
	
	IF ( @COVERAGE_ID = -1 )
	BEGIN
		
		select  @COVERAGE_ID_MAX = isnull(Max(COVERAGE_ID),0)+1 from POL_DWELLING_SECTION_COVERAGES
		where CUSTOMER_ID = @CUSTOMER_ID and 
			POLICY_ID=@POL_ID and 
			POLICY_VERSION_ID = @POL_VERSION_ID 
			and DWELLING_ID = @DWELLING_ID

IF NOT EXISTS
		(
			SELECT * FROM POL_DWELLING_SECTION_COVERAGES
			where CUSTOMER_ID = @CUSTOMER_ID and 
				POLICY_ID=@POL_ID and 
				POLICY_VERSION_ID = @POL_VERSION_ID 
				and DWELLING_ID = @DWELLING_ID AND
				COVERAGE_CODE_ID = @COVERAGE_CODE_ID
		)
		
		BEGIN
		INSERT INTO POL_DWELLING_SECTION_COVERAGES
		(
			CUSTOMER_ID,
			POLICY_ID,
			POLICY_VERSION_ID,
			DWELLING_ID,
			COVERAGE_ID,
			COVERAGE_CODE_ID,
			LIMIT_1_TYPE,
			LIMIT_2_TYPE,
			DEDUCTIBLE_1_TYPE,
			DEDUCTIBLE_2_TYPE,
			LIMIT_1,
			LIMIT_2,
			DEDUCTIBLE_1,
			DEDUCTIBLE_2,	
			WRITTEN_PREMIUM,
			FULL_TERM_PREMIUM,
			COVERAGE_TYPE
		)
		VALUES
		(
			@CUSTOMER_ID,
			@POL_ID,
			@POL_VERSION_ID,
			@DWELLING_ID,
			@COVERAGE_ID_MAX,
			@COVERAGE_CODE_ID,
			@LIMIT_1_TYPE,
			@LIMIT_2_TYPE,
			@DEDUCTIBLE_1_TYPE,
			@DEDUCTIBLE_2_TYPE,
			@LIMIT_1,
			@LIMIT_2,
			@DEDUCTIBLE_1,
			@DEDUCTIBLE_2,	
			@WRITTEN_PREMIUM,
			@FULL_TERM_PREMIUM,
			@COVERAGE_TYPE
		)
	RETURN 1
END
	END	
	
	--Update
	UPDATE POL_DWELLING_SECTION_COVERAGES
	SET
		CUSTOMER_ID = @CUSTOMER_ID,
		POLICY_ID =  @POL_ID,
		POLICY_VERSION_ID = @POL_VERSION_ID,
		COVERAGE_CODE_ID = @COVERAGE_CODE_ID,
		LIMIT_1_TYPE = @LIMIT_1_TYPE,
		LIMIT_2_TYPE = @LIMIT_2_TYPE,
		LIMIT_1 = @LIMIT_1,
		LIMIT_2 = @LIMIT_2,
		DEDUCTIBLE_1_TYPE = @DEDUCTIBLE_1_TYPE,
		DEDUCTIBLE_2_TYPE = @DEDUCTIBLE_2_TYPE,
		DEDUCTIBLE_1 = @DEDUCTIBLE_1,
		DEDUCTIBLE_2 = @DEDUCTIBLE_2,
		WRITTEN_PREMIUM = @WRITTEN_PREMIUM, 
		FULL_TERM_PREMIUM = @FULL_TERM_PREMIUM,
COVERAGE_TYPE=@COVERAGE_TYPE
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
		POLICY_ID = @POL_ID AND
		POLICY_VERSION_ID = @POL_VERSION_ID AND
		COVERAGE_ID = @COVERAGE_ID
		
END




GO

