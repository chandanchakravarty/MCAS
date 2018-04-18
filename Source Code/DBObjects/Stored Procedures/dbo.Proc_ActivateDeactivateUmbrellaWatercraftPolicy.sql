IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUmbrellaWatercraftPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUmbrellaWatercraftPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC DBO.Proc_ActivateDeactivateUmbrellaWatercraftPolicy                    
    
(                    
    
@CUSTOMER_ID     INT,                    
    
@POLICY_ID     INT,                    
    
@POLICY_VERSION_ID     SMALLINT,                    
    
@BOAT_ID     SMALLINT,            
    
@IS_ACTIVE NCHAR(2)            
    
)                    
    
AS                    
    
BEGIN                    
    
            
    
UPDATE POL_UMBRELLA_WATERCRAFT_INFO SET IS_ACTIVE=@IS_ACTIVE WHERE            
    
 CUSTOMER_ID=@CUSTOMER_ID AND             
    
 POLICY_ID=@POLICY_ID AND            
    
 POLICY_VERSION_ID=@POLICY_VERSION_ID AND            
    
 BOAT_ID=@BOAT_ID        
    
 IF @IS_ACTIVE='N'      
    
 BEGIN         
    
  UPDATE POL_UMBRELLA_DRIVER_DETAILS         
    
  SET OP_VEHICLE_ID=NULL        
    
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND OP_VEHICLE_ID=@BOAT_ID        
    
 END        
    
END        
    
  



GO

