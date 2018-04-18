IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUnitClearance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUnitClearance]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetUnitClearance
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_GetUnitClearance
(
	@CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int,
	@FUEL_ID int
)
AS
BEGIN
SELECT CUSTOMER_ID,APP_ID,APP_VERSION_ID,FUEL_ID,STOVE_INSTALL_SPEC,DIST_REAR_WALL_FEET,DIST_REAR_WALL_INCHES,DIST_LEFT_WALL_FEET,
DIST_LEFT_WALL_INCHES,DIST_RIGHT_WALL_FEET,DIST_RIGHT_WALL_INCHES,DIST_BOTTOM_FLOOR_FEET,DIST_BOTTOM_FLOOR_INCHES,
DIA_PIPE_FEET,DIA_PIPE_INCHES,FRONT_PROTECTION_FEET,FRONT_PROTECTION_INCHES,STOVE_WALL_FEET,STOVE_WALL_INCHES,
TOP_CEILING_FEET,TOP_CEILING_INCHES,SHORT_DIST_WALL_FEET,SHORT_DIST_WALL_INCHES,SHORT_DIST_CEILING_FEET,
SHORT_DIST_CEILING_INCHES,DIST_COMBUSTIBLE_FEET,DIST_COMBUSTIBLE_INCHES
FROM  APP_HOME_OWNER_UNIT_CLEARANCE
WHERE CUSTOMER_ID=@CUSTOMER_ID  AND 
	 APP_ID=@APP_ID  AND 
	APP_VERSION_ID=@APP_VERSION_ID  AND 
	FUEL_ID=@FUEL_ID 
END


GO

