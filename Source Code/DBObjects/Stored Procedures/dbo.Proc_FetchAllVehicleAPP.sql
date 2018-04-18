IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchAllVehicleAPP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchAllVehicleAPP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**************************************************************
Proc Name   : dbo.Proc_FetchAllVehicleAPP
Created by  : Shafi
Date        : 29-8-06
Purpose     : Get the All Vehicle asssociated with an Application
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
***************************************************************/  
--DROP PROC Proc_FetchAllDwellingsAPP
CREATE PROCEDURE dbo.Proc_FetchAllVehicleAPP
(          
  @CUSTOMER_ID int,          
  @APP_ID int,          
  @APP_VERSION_ID int          
)              
AS                   
BEGIN                    
	SELECT   
	VEHICLE_ID, 
    VEHICLE_ID AS RISK_ID
	FROM  APP_VEHICLES WITH(NOLOCK) 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND           
	APP_ID = @APP_ID AND           
	APP_VERSION_ID = @APP_VERSION_ID  


END 








GO

