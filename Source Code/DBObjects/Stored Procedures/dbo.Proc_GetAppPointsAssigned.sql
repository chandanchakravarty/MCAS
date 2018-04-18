IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppPointsAssigned]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppPointsAssigned]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_GetAppPointsAssigned]     
(    
	 @CustID INT,     
	 @AppID INT,     
	 @AppVerID INT,
	 @DriverID INT
)    
AS    
BEGIN    
	SELECT count(*) FROM APP_DRIVER_ASSIGNED_VEHICLE with (NoLock)
	WHERE CUSTOMER_ID = @CustID AND APP_ID=@AppID AND APP_VERSION_ID=@AppVerID AND DRIVER_ID=@DriverID
	and APP_VEHICLE_PRIN_OCC_ID in (11399,11926,11928,11930)
END 


GO

