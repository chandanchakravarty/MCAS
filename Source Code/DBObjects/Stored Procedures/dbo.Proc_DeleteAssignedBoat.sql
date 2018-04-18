IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAssignedBoat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAssignedBoat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- DROP PROC Proc_DeleteAssignedBoat  
CREATE  PROC dbo.Proc_DeleteAssignedBoat  
(  
 @CustID int,       
 @AppID int,       
 @AppVersionID int,  
 @DriverID int  
   
)  
  
AS  
  
BEGIN  
 DELETE FROM APP_OPERATOR_ASSIGNED_BOAT  
 WHERE CUSTOMER_ID = @CustID AND  
  APP_ID = @AppID AND  
  APP_VERSION_ID = @AppVersionID AND  
  DRIVER_ID = @DriverID  
    
    
   
END  
  
  
  
  
  



GO

