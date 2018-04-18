IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRV_VehicleIDs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRV_VehicleIDs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetRV_VehicleIDs    
Created by  : Manoj Rathore    
Date        : 07 DEC,2006    
Purpose     : Get the RV_Vehicle IDs               
 
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/              
CREATE PROCEDURE dbo.Proc_GetRV_VehicleIDs    
(    
 @CUSTOMER_ID int,    
 @APP_ID int,    
 @APP_VERSION_ID int    
)        
AS             
BEGIN              
    
	SELECT   REC_VEH_ID    
	FROM       APP_HOME_OWNER_RECREATIONAL_VEHICLES     
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
		AND ACTIVE='Y'  
	ORDER BY   REC_VEH_ID                
End    
    




GO

