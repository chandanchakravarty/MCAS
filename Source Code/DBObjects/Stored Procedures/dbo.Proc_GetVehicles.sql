IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   
  
/*----------------------------------------------------------            
Proc Name   : dbo.Proc_GetVehicles  
Created by  : Pradeep  
Date        : 18 October,2005  
Purpose     : Get the Vehicle IDs for coying coverages            
Revison History  :                  
 ------------------------------------------------------------                        
Date     Review By          Comments                      
             
------   ------------       -------------------------*/            
CREATE PROCEDURE dbo.Proc_GetVehicles  
(  
	 @CUSTOMER_ID int,  
	 @APP_ID int,  
	 @APP_VERSION_ID int,  
	 @VEHICLE_ID smallint,
	 @CALLED_FROM VarChar(3)  
)      
AS           
BEGIN            
  
IF ( @CALLED_FROM = 'PPA' OR @CALLED_FROM = 'MOT' OR @CALLED_FROM = 'VEH')
	BEGIN
	
		SELECT   VEHICLE_ID,  
		  INSURED_VEH_NUMBER,  
		  VEHICLE_YEAR,  
		  MAKE,  
		  MODEL,  
		  VIN  
		FROM  APP_VEHICLES   
		   WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
		 APP_ID = @APP_ID AND   
		 APP_VERSION_ID = @APP_VERSION_ID  AND  
		 VEHICLE_ID <> @VEHICLE_ID  
		
	END

IF ( @CALLED_FROM = 'UMB')
	BEGIN
	
		SELECT   VEHICLE_ID,  
		  INSURED_VEH_NUMBER,  
		  VEHICLE_YEAR,  
		  MAKE,  
		  MODEL,  
		  VIN  
		FROM  APP_UMBRELLA_VEHICLE_INFO   
		   WHERE CUSTOMER_ID = @CUSTOMER_ID AND   
		 APP_ID = @APP_ID AND   
		 APP_VERSION_ID = @APP_VERSION_ID  AND  
		 VEHICLE_ID <> @VEHICLE_ID  
		
	END
        
End  
  
  



GO

