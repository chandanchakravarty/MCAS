IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperatorCountForAssignedBoat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperatorCountForAssignedBoat]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetOperatorCountForAssignedBoat                          
Created by      : Sumit Chhabra                          
Date            : 05/04/2006                          
Purpose     		: Count no of operators assigned to current boat
Revison History :                          
Used In  : Wolverine                          
                          
          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC Dbo.Proc_GetOperatorCountForAssignedBoat      
(                          
@CUSTOMER_ID     int,                          
@APP_ID     int ,                          
@APP_VERSION_ID     smallint,                          
@BOAT_ID     int
)                          
AS                          
BEGIN    
  SELECT CUSTOMER_ID FROM APP_UMBRELLA_DRIVER_DETAILS WHERE       
  CUSTOMER_ID=@CUSTOMER_ID AND       
  APP_ID=@APP_ID AND       
  APP_VERSION_ID = @APP_VERSION_ID AND      
  OP_VEHICLE_ID = @BOAT_ID
 return @@rowcount      
END    



GO

