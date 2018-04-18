IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOperatorCountForAssignedUmbBoatPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOperatorCountForAssignedUmbBoatPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
    
Proc Name    : Dbo.Proc_GetOperatorCountForAssignedUmbBoatPolicy                    
Created by   : Sumit Chhabra      
Date         : 31-03-2006      
Purpose      : Get the Operator count for no. of assigned boats in umbrella watercraft policy.        
Used In      : Wolverine                       
                
    
------   ------------       -------------------------*/         
CREATE PROC Dbo.Proc_GetOperatorCountForAssignedUmbBoatPolicy      
(                          
@CUSTOMER_ID     int,                          
@POL_ID     int ,                          
@POL_VERSION_ID     smallint,                          
@BOAT_ID     smallint    
)                          
AS                          
BEGIN       
  SELECT CUSTOMER_ID FROM POL_UMBRELLA_DRIVER_DETAILS WHERE    
    CUSTOMER_ID=@CUSTOMER_ID and       
    POLICY_ID=@POL_ID and       
    POLICY_VERSION_ID = @POL_VERSION_ID and      
    OP_VEHICLE_ID = @BOAT_ID      
 RETURN @@ROWCOUNT  
END    
  



GO

