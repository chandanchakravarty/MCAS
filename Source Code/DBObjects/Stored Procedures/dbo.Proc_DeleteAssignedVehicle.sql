IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAssignedVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAssignedVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- DROP PROC Proc_DeleteAssignedVehicle
CREATE  PROC dbo.Proc_DeleteAssignedVehicle
(
 @CustID int,     
 @AppID int,     
 @AppVersionID int,
 @DriverID int
 
)

AS

BEGIN
	DELETE FROM APP_DRIVER_ASSIGNED_VEHICLE
	WHERE CUSTOMER_ID = @CustID AND
		APP_ID = @AppID AND
		APP_VERSION_ID = @AppVersionID AND
		DRIVER_ID = @DriverID
		
		
	
END







GO

