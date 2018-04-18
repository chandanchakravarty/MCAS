IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAviationVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAviationVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
/*
proc name	:dbo.Proc_DeleteAviationVehicle            
created By	:Pravesh K Chandel
date		: 12 Jan 2010
Purpose		: to delete Aviation Vehicle
*/
CREATE PROC dbo.Proc_DeleteAviationVehicle
(            
	@CUSTOMER_ID  INT,            
	@APP_ID  INT,            
	@APP_VERSION_ID INT,            
	@VEHICLE_ID INT            
)            
AS            
BEGIN            

	 DELETE FROM APP_AVIATION_VEHICLE_COVERAGES 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID 
	--Delete from Vehicle Endorsements          

	 DELETE FROM APP_AVIATION_VEHICLES  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND   
	  APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID            
END     




GO

