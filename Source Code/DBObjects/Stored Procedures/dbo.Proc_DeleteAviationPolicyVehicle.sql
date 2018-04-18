IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAviationPolicyVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAviationPolicyVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
proc name	:dbo.Proc_DeleteAviationPolicyVehicle            
created By	:Pravesh K Chandel
date		: 14 Jan 2010
Purpose		: to delete Pol Aviation Vehicle
*/
CREATE PROC dbo.Proc_DeleteAviationPolicyVehicle
(            
	@CUSTOMER_ID  INT,            
	@POLICY_ID  INT,            
	@POLICY_VERSION_ID INT,            
	@VEHICLE_ID INT            
)            
AS            
BEGIN            

	 DELETE FROM POL_AVIATION_VEHICLE_COVERAGES 
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND
	  POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID 
	--Delete from Vehicle Endorsements          

	 DELETE FROM POL_AVIATION_VEHICLES  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID  AND   
	  POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID       
END     



GO

