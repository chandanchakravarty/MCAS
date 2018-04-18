IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolPointsAssigned]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolPointsAssigned]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_GetPolPointsAssigned]     
(    
	 @CustID INT,     
	 @PolID INT,     
	 @PolVerID INT,
	 @DriverID INT
)    
AS    
BEGIN    
	SELECT count(*) FROM POL_DRIVER_ASSIGNED_VEHICLE with (NoLock)
	WHERE CUSTOMER_ID = @CustID AND POLICY_ID=@PolID AND POLICY_VERSION_ID=@PolVerID AND DRIVER_ID=@DriverID
	and APP_VEHICLE_PRIN_OCC_ID in (11399,11926,11928,11930)
END    


GO

