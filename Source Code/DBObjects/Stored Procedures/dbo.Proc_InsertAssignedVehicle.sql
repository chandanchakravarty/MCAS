IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAssignedVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAssignedVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC Proc_InsertAssignedVehicle 
CREATE PROCEDURE dbo.Proc_InsertAssignedVehicle 
(    
 @CustID int,     
 @AppID int,     
 @AppVersionID int,
 @DriverID smallint,
 @VehID smallint,
 @PrinOccID int
     
)    
AS    
BEGIN
	--Added by manoj rathore on 25th Jun 2009 Itrack # 5847
	DECLARE @DRIVER_DRIV_TYPE VARCHAR(10)
	SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE FROM APP_DRIVER_DETAILS
	WHERE CUSTOMER_ID=@CUSTID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID

	IF(@DRIVER_DRIV_TYPE='11942')
	BEGIN
	SET @PRINOCCID='11931'
	END 

	INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE
	(
		CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID
	)
	VALUES
	(
		@CustID,@AppID,@AppVersionID,@DriverID,@VehID,@PrinOccID
	)
END





GO

