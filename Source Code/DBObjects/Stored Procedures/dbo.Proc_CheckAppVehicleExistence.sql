IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAppVehicleExistence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAppVehicleExistence]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO







/*----------------------------------------------------------
Proc Name          : Dbo.Proc_CheckApplicationExistence
Created by         : Pradeep
Date               : 05/05/2005
Purpose            : To check whether this customer exists
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_CheckAppVehicleExistence
(
	@CUSTOMER_ID Int,
	@APP_ID int,
	@APP_VERSION_ID smallint,
	@VIN NVarChar(75),
	@VEHICLE_ID smallint output
)

AS
BEGIN
			
	SELECT @VEHICLE_ID = VEHICLE_ID
	FROM APP_VEHICLES
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	      APP_ID = @APP_ID  AND
	      APP_VERSION_ID = @APP_VERSION_ID AND
	      VIN = @VIN 			
	
	IF ( @VEHICLE_ID IS NULL )
	BEGIN
		SET @VEHICLE_ID =  -1
		RETURN
	END
	
	

	

END






GO

