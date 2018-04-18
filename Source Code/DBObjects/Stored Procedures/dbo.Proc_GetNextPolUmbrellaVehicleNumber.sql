IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextPolUmbrellaVehicleNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextPolUmbrellaVehicleNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetNextPolUmbrellaVehicleNumber           
Created by  :Swarup            
Date        :7 Jun,2007          
Purpose     : Returns the next VEHICLE_NUMBER for the     
       CUSTOMER_ID, POLICY_ID and POLICY_VERSION_ID             
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/      
-- drop PROCEDURE dbo.Proc_GetNextPolUmbrellaVehicleNumber 1199,67,1   
CREATE PROCEDURE dbo.Proc_GetNextPolUmbrellaVehicleNumber    
(    
     
 @CUSTOMER_ID Int,    
 @POLICY_ID Int,    
 @POLICY_VERSION_ID SmallInt    
)    
    
As    
    
 DECLARE @MAX BigInt    
     
 SELECT @MAX = ISNULL(MAX(VEHICLE_ID),0)    
 FROM POL_UMBRELLA_VEHICLE_INFO    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND     
  POLICY_VERSION_ID = @POLICY_VERSION_ID     
     
 SELECT @MAX + 1    
    
    


GO

