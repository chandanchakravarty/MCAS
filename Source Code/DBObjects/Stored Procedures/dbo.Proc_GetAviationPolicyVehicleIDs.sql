IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAviationPolicyVehicleIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAviationPolicyVehicleIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------------------------------------------              
Proc Name   : dbo.Proc_GetAviationPolicyVehicleIDs    
Created by  : Pravesh K Chandel     
Date        : 15 Jan 2010
Purpose     : Get the Aviation Vehicle IDs for Policy mandatory info              
Revison History  :     
------------------------------------------------------------------------------------------------                          
Date     Review By          Comments                        
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetAviationPolicyVehicleIDs
(    
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID INT    
)        
AS             
BEGIN              
 SELECT   VEHICLE_ID    
 FROM POL_AVIATION_VEHICLES     WITH(NOLOCK)
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
 	AND IS_ACTIVE='Y'  
 ORDER BY   VEHICLE_ID                
END    
    
  




GO

