IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAssigned_RecreationalVehicle_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAssigned_RecreationalVehicle_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC Proc_InsertAssigned_RecreationalVehicle_Pol  
CREATE PROCEDURE dbo.Proc_InsertAssigned_RecreationalVehicle_Pol
(      
 @CUSTID INT,       
 @POLID INT,       
 @POLVERSIONID INT,  
 @DRIVERID SMALLINT,  
 @REC_VEH_ID SMALLINT,  
 @PRINOCCID INT  
       
)      
AS      
BEGIN  
 INSERT INTO POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE
 (  
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,RECREATIONAL_VEH_ID,POL_REC_VEHICLE_PRIN_OCC_ID  
 )  
 VALUES  
 (  
  @CUSTID,@POLID,@POLVERSIONID,@DRIVERID,@REC_VEH_ID,@PRINOCCID  
 )  
END  
  







GO

