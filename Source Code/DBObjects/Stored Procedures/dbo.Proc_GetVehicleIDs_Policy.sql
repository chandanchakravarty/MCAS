IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleIDs_Policy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleIDs_Policy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name    : Proc_GetVehicleIDs_Policy  
Created by   : Praveen Singh   
Date         : 10/01/2006      
Purpose      : Get the Vehicle IDs for policy   
Revison History  :       
------------------------------------------------------------                            
Date     Review By          Comments                          
                 
------   ------------       -------------------------*/   
--DROP PROCEDURE dbo.Proc_GetVehicleIDs_Policy                
CREATE PROCEDURE dbo.Proc_GetVehicleIDs_Policy      
(      
 @CUSTOMER_ID int,      
 @POL_ID int,      
 @POL_VERSION_ID int      
)          
AS               
BEGIN                
     
SELECT     
 VEHICLE_ID,ISNULL(MAKE,'') AS MAKE,ISNULL(MODEL,'') AS MODEL,ISNULL(VIN,'') AS VIN, ISNULL(VEHICLE_YEAR,'') AS VEHICLE_YEAR    
FROM      
 POL_VEHICLES  with(nolock)     
WHERE   
 CUSTOMER_ID = @CUSTOMER_ID AND   
 POLICY_ID=@POL_ID AND  
 POLICY_VERSION_ID=@POL_VERSION_ID AND   
 IS_ACTIVE='Y'    
ORDER BY     
 VEHICLE_ID                  
End      
      
  
  
  
  





GO

