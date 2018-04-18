IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertWaterCraftVehicleCoverages]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertWaterCraftVehicleCoverages]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertWaterCraftVehicleCoverages
Created by      : Gaurav
Date            : 6/14/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_InsertWaterCraftVehicleCoverages
(
@CUSTOMER_ID     int,
@APP_ID     int,
@APP_VERSION_ID     smallint,
@COVERAGE_ID     int output,
@BOAT_ID	smallint,
@COVERAGE_CODE     nvarchar(50),
@LIMIT     decimal(9),
@DEDUCTIBLE     decimal(9),

@WRITTEN_PREMIUM     decimal(9),
@FULL_TERM_PREMIUM     decimal(9),
@IS_ACTIVE		nchar(1),
@CREATED_BY	int,
@CREATED_DATETIME	DateTime
)
AS
BEGIN
select  @COVERAGE_ID=isnull(Max(COVERAGE_ID),0)+1 from APP_WATERCRAFT_COVERAGE_INFO
where CUSTOMER_ID = @CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID = @APP_VERSION_ID and BOAT_ID = @BOAT_ID
INSERT INTO APP_WATERCRAFT_COVERAGE_INFO
(
CUSTOMER_ID,
APP_ID,
APP_VERSION_ID,
COVERAGE_ID,
BOAT_ID,
COVERAGE_CODE_ID,
LIMIT_1,
DEDUCTIBLE_1 ,
WRITTEN_PREMIUM,
FULL_TERM_PREMIUM,
IS_ACTIVE	,
CREATED_BY	,
CREATED_DATETIME
)
VALUES
(
@CUSTOMER_ID,
@APP_ID,
@APP_VERSION_ID,
@COVERAGE_ID,
@BOAT_ID,
@COVERAGE_CODE,

@LIMIT,

@DEDUCTIBLE,

@WRITTEN_PREMIUM,
@FULL_TERM_PREMIUM,
@IS_ACTIVE	,
@CREATED_BY	,
@CREATED_DATETIME	
)
END


--sp_helptext Proc_InsertWaterCraftVehicleCoverages

GO

