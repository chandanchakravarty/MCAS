IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateAppOtherStructureDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateAppOtherStructureDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
CREATE PROC Proc_ActivateAppOtherStructureDetails
(
@CUSTOMER_ID     INT,
@APP_ID     INT,
@APP_VERSION_ID     SMALLINT,
@DWELLING_ID     SMALLINT,
@OTHER_STRUCTURE_ID     SMALLINT
)
AS
BEGIN
    DECLARE @COVERAGEB INT
DECLARE @IN_VAL INT
DECLARE @DIFF_VAL INT
DECLARE @COVBADD_VAL INT

--find the sum of insuring value before adding new value (this will give old sum)
SELECT @IN_VAL=ISNULL(SUM(ISNULL(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED,0)),0)
	FROM APP_OTHER_STRUCTURE_DWELLING STR4 WITH(NOLOCK)
	WHERE 
	    STR4.PREMISES_LOCATION = 11841 -- Off Primises
	AND STR4.CUSTOMER_ID = @CUSTOMER_ID
	AND STR4.APP_ID = @APP_ID
	AND STR4.APP_VERSION_ID=@APP_VERSION_ID
    and STR4.DWELLING_ID=@DWELLING_ID
    AND ISNULL(STR4.SATELLITE_EQUIPMENT,'0')='2'
	and STR4.IS_ACTIVE='Y'



--find value in coverage b..already saved
SELECT @COVERAGEB=ISNULL(DEDUCTIBLE_1,0) FROM APP_DWELLING_SECTION_COVERAGES
 WHERE  CUSTOMER_ID = @CUSTOMER_ID
	AND APP_ID = @APP_ID
	AND APP_VERSION_ID=@APP_VERSION_ID
    AND DWELLING_ID=@DWELLING_ID
    AND COVERAGE_CODE_ID IN(774,794)



--change in value of coverage b with 
SET @DIFF_VAL=ISNULL(@COVERAGEB,0) - ISNULL(@IN_VAL,0)
    
	UPDATE APP_OTHER_STRUCTURE_DWELLING
	SET IS_ACTIVE = 'Y'
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND 
	APP_ID=@APP_ID AND 
	APP_VERSION_ID=@APP_VERSION_ID AND 
	DWELLING_ID=@DWELLING_ID AND
	OTHER_STRUCTURE_ID=@OTHER_STRUCTURE_ID 

     SELECT @IN_VAL=ISNULL(SUM(ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED),0)
	FROM APP_OTHER_STRUCTURE_DWELLING STR4 WITH(NOLOCK)
	WHERE 
	    STR4.PREMISES_LOCATION = 11841 -- Off Primises
	AND STR4.CUSTOMER_ID = @CUSTOMER_ID
	AND STR4.APP_ID = @APP_ID
	AND STR4.APP_VERSION_ID=@APP_VERSION_ID
     and STR4.DWELLING_ID=@DWELLING_ID
    AND ISNULL(STR4.SATELLITE_EQUIPMENT,'0')='2'
    and STR4.IS_ACTIVE='Y'


SET @COVBADD_VAL=@IN_VAL+@DIFF_VAL
exec Proc_Update_Additional_CoverageB @CUSTOMER_ID ,@APP_ID,@APP_VERSION_ID,@DWELLING_ID,@COVBADD_VAL
    
END









GO

