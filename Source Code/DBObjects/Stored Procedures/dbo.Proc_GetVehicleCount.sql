IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name       : dbo.Proc_GetVehicleCount                          
Created by      : swastika                          
Date                : 23rd Aug'06                          
Purpose          :                          
Revison History :                          
Modified By : Praveen kasana
Modified Date : 21 jan 2009
Description : Itrack 5310
Used In  : Wolverine                          
                          
          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_GetVehicleCount                      
create PROC dbo.Proc_GetVehicleCount      
(                          
@CUSTOMER_ID     int,                          
@APP_ID     int ,                          
@APP_VERSION_ID     smallint,
@VEHICLE_ID	smallint=null--,
--@Mode varchar(5) = null -- 'ADD', 'EDIT', 'GET'
)                          
AS        
BEGIN   
	   
	 DECLARE @COUNT INT                 
	 DECLARE @COUNT_PERSONAL INT	
	 DECLARE @COUNT_COMM INT	
	 SET @COUNT_PERSONAL = 0
	 SET @COUNT_COMM = 0

	 SELECT @COUNT_PERSONAL = COUNT(*) FROM APP_VEHICLES (NOLOCK)  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
	 APP_ID = @APP_ID AND  
	 APP_VERSION_ID = @APP_VERSION_ID AND
	 IS_ACTIVE = 'Y' AND 
	 USE_VEHICLE =   11332  --personal
	 AND VEHICLE_TYPE_PER !=11870 AND VEHICLE_TYPE_PER != 11337  


	SELECT @COUNT_COMM = COUNT(*) FROM APP_VEHICLES (NOLOCK)  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
	 APP_ID = @APP_ID AND  
	 APP_VERSION_ID = @APP_VERSION_ID AND
	 IS_ACTIVE = 'Y' AND 
	 USE_VEHICLE =   11333 --comm
	 AND VEHICLE_TYPE_COM !=11341 AND VEHICLE_TYPE_COM != 11340  



	 SET @COUNT = @COUNT_PERSONAL + @COUNT_COMM

	
	/*IF @CNT = 1 --or @CNT = 0
	BEGIN
			
		UPDATE APP_VEHICLES
		SET MULTI_CAR = '11918' -- NOT APPLICABLE
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
		APP_ID = @APP_ID AND  
		APP_VERSION_ID = @APP_VERSION_ID AND
		VEHICLE_TYPE_PER =   '11334'   -- PPA
		AND  VEHICLE_ID = @VEHICLE_ID
	END
	ELSE
	BEGIN
		UPDATE APP_VEHICLES
		SET MULTI_CAR = '11919' -- OTHER CAR ON THIS POLICY
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
		APP_ID = @APP_ID AND  
		APP_VERSION_ID = @APP_VERSION_ID AND
		VEHICLE_TYPE_PER =   '11334' -- PPA
		AND  VEHICLE_ID = @VEHICLE_ID
	END*/

/*
	IF(@COUNT > 1)
	BEGIN
		UPDATE APP_VEHICLES
		SET MULTI_CAR = '11919' -- OTHER CAR ON THIS POLICY
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
		APP_ID = @APP_ID AND  
		APP_VERSION_ID = @APP_VERSION_ID AND
		--VEHICLE_TYPE_PER =   '11334' -- PPA AND
		  VEHICLE_ID = @VEHICLE_ID		
	END
	*/
	RETURN @COUNT

END    



GO

