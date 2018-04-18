IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolAssignedVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolAssignedVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- DROP PROC Proc_DeletePolAssignedVehicle
CREATE  PROC dbo.Proc_DeletePolAssignedVehicle
(
 @CustID int,     
 @PolID int,     
 @PolVersionID int,
 @DriverID int
 
)

AS

BEGIN
	DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE
	WHERE CUSTOMER_ID = @CustID AND
		POLICY_ID = @PolID AND
		POLICY_VERSION_ID = @PolVersionID AND
		DRIVER_ID = @DriverID
		
		
	
END







GO

