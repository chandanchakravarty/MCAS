IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAssigned_RecreationalVehicle_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAssigned_RecreationalVehicle_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC Proc_DeleteAssigned_RecreationalVehicle_Pol  
CREATE  PROC dbo.Proc_DeleteAssigned_RecreationalVehicle_Pol  
(  
 @CustID int,       
 @PolID int,       
 @PolVersionID int,  
 @DriverID int  
   
)  
  
AS  
  
BEGIN  
 DELETE FROM POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE  
 WHERE CUSTOMER_ID = @CustID AND  
  POLICY_ID = @PolID AND  
  POLICY_VERSION_ID = @PolVersionID AND  
  DRIVER_ID = @DriverID  
    
    
   
END  
  





GO

