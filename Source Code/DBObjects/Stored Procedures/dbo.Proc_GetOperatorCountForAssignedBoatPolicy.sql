IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperatorCountForAssignedBoatPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperatorCountForAssignedBoatPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                

Proc Name    : Dbo.Proc_GetOperatorCountForAssignedBoatPolicy                
Created by   : Swastika Gaur    
Date         : 09-03-2006  
Purpose      : Get the Operator count for no. of assigned boats in watercraft policy.    
Used In      : Wolverine                   
            

------   ------------       -------------------------*/     
CREATE PROC Dbo.Proc_GetOperatorCountForAssignedBoatPolicy  
(                      
@CUSTOMER_ID     int,                      
@POL_ID     int ,                      
@POL_VERSION_ID     smallint,                      
@BOAT_ID     smallint
)                      
AS                      
BEGIN   
  DECLARE @COUNT int               
  SELECT @COUNT = COUNT(CUSTOMER_ID) FROM POL_WATERCRAFT_DRIVER_DETAILS WHERE
  CUSTOMER_ID=@CUSTOMER_ID and   
  POLICY_ID=@POL_ID and   
  POLICY_VERSION_ID = @POL_VERSION_ID and  
  VEHICLE_ID = @BOAT_ID  
 RETURN @COUNT  
END




GO

