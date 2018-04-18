IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbrellaVehicleCoverageInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbrellaVehicleCoverageInfo]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetBodyTypeFromVINMASTER
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetUmbrellaVehicleCoverageInfo
(
@COVERAGE_ID     int
)
AS
BEGIN
SELECT CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,
COVERAGE_ID,COVERAGE_CODE_ID,LIMIT_1,LIMIT_2,
DEDUCTIBLE_1,WRITTEN_PREMIUM,FULL_TERM_PREMIUM 
FROM  APP_UMBRELLA_VEHICLE_COV_IFNO
WHERE  COVERAGE_ID=@COVERAGE_ID	
END


GO

