IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleIDs_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleIDs_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
     
/*---------------------------------------------------------------------------------------------              
Proc Name   : dbo.Proc_GetVehicleIDs_Pol    
Created by  : Ashwani     
Date        : 01 Mar 2006 
Purpose     : Get the Vehicle IDs for Policy mandatory info              
Revison History  :     
------------------------------------------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetVehicleIDs_Pol    
(    
 @CUSTOMER_ID INT,    
 @POLICY_ID INT,    
 @POLICY_VERSION_ID INT    
)        
AS             
BEGIN              
 SELECT   VEHICLE_ID    
 FROM POL_VEHICLES     
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
 	AND IS_ACTIVE='Y'  
 ORDER BY   VEHICLE_ID                
END    
    
  



GO

