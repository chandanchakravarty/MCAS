IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAssignedBoat_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAssignedBoat_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC Proc_InsertAssignedBoat  
CREATE PROCEDURE dbo.Proc_InsertAssignedBoat_Pol
(      
 @CustID int,       
 @PolID int,       
 @PolVersionID int,  
 @DriverID smallint,  
 @BoatID smallint,  
 @PrinOccID int  
       
)      
AS      
BEGIN  
 INSERT INTO POL_OPERATOR_ASSIGNED_BOAT
 (  
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,BOAT_ID,APP_VEHICLE_PRIN_OCC_ID  
 )  
 VALUES  
 (  
  @CustID,@PolID,@PolVersionID,@DriverID,@BoatID,@PrinOccID  
 )  
END  
  





GO

