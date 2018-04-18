IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextUmbrellaVehicleNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextUmbrellaVehicleNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_GetNextUmbrellaVehicleNumber         
Created by  :Pradeep          
Date        :17 Jun,2005        
Purpose     : Returns the next VEHICLE_NUMBER for the   
       CUSTOMERID, APPID and APPVersionID           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
-- drop PROCEDURE Proc_GetNextUmbrellaVehicleNumber  
CREATE PROCEDURE Proc_GetNextUmbrellaVehicleNumber  
(  
   
 @CUSTOMER_ID Int,  
 @APP_ID Int,  
 @APP_VERSION_ID SmallInt  
)  
  
As  
  
 DECLARE @MAX BigInt  
   
 SELECT @MAX = ISNULL(MAX(VEHICLE_ID),0)  
 FROM APP_UMBRELLA_VEHICLE_INFO  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID   
   
 SELECT @MAX + 1  
  
  
                               


  



GO

