IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAssignedBoat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAssignedBoat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC Proc_InsertAssignedBoat  
CREATE PROCEDURE dbo.Proc_InsertAssignedBoat
(      
 @CustID int,       
 @AppID int,       
 @AppVersionID int,  
 @DriverID smallint,  
 @BoatID smallint,  
 @PrinOccID int  
       
)      
AS      
BEGIN  
 INSERT INTO APP_OPERATOR_ASSIGNED_BOAT
 (  
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID  
 )  
 VALUES  
 (  
  @CustID,@AppID,@AppVersionID,@DriverID,@BoatID,@PrinOccID  
 )  
END  
  



GO

