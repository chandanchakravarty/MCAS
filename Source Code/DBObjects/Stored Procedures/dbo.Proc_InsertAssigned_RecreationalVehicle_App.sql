IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAssigned_RecreationalVehicle_App]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAssigned_RecreationalVehicle_App]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC Proc_InsertAssigned_RecreationalVehicle_App  
CREATE PROCEDURE dbo.Proc_InsertAssigned_RecreationalVehicle_App
(      
 @CUSTID INT,       
 @APPID INT,       
 @APPVERSIONID INT,  
 @DRIVERID SMALLINT,  
 @REC_VEH_ID SMALLINT,  
 @PRINOCCID INT  
       
)      
AS      
BEGIN  
 INSERT INTO APP_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE
 (  
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,RECREATIONAL_VEH_ID,APP_REC_VEHICLE_PRIN_OCC_ID  
 )  
 VALUES  
 (  
  @CUSTID,@APPID,@APPVERSIONID,@DRIVERID,@REC_VEH_ID,@PRINOCCID  
 )  
END  
  








GO

