IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleCount1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleCount1]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetAppVehicleCount                            
Created by      : swarup                            
Date                : 22nd Dec'06                            
Purpose          :                            
Revison History :                            
Used In  : Wolverine                            
                            
            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_GetAppVehicleCount                        
CREATE PROC dbo.Proc_GetVehicleCount1      
(                          
@CUSTOMER_ID     int,                          
@APP_ID     int ,                          
@APP_VERSION_ID     smallint,
@VEHICLE_ID	smallint=null--,
--@Mode varchar(5) = null -- 'ADD', 'EDIT', 'GET'
)                          
AS        
BEGIN                      
	declare @CNT INT, @MC INT

if (@vehicle_id is not null)
begin
	if exists(
			 SELECT CUSTOMER_ID FROM APP_VEHICLES (NOLOCK)  
			 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
			 APP_ID = @APP_ID AND  
			 APP_VERSION_ID = @APP_VERSION_ID AND
			 IS_ACTIVE = 'Y' AND
			 VEHICLE_TYPE_PER =   '11334'  --PPA
			and vehicle_id=@vehicle_id 
			AND MULTI_CAR <>  '11920')  -- OTHER POLICY WITH WOLVERINE
		return 3;

end
	
	 SELECT CUSTOMER_ID FROM APP_VEHICLES (NOLOCK)  
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
	 APP_ID = @APP_ID AND  
	 APP_VERSION_ID = @APP_VERSION_ID AND
	 IS_ACTIVE = 'Y' AND
	 VEHICLE_TYPE_PER =   '11334'  --PPA
--	AND MULTI_CAR <>  '11920'  -- OTHER POLICY WITH WOLVERINE

	 SET @CNT = @@ROWCOUNT

	
		 IF @CNT = 1
			
			UPDATE APP_VEHICLES
			SET MULTI_CAR = '11918' -- NOT APPLICABLE
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
			 APP_ID = @APP_ID AND  
			 APP_VERSION_ID = @APP_VERSION_ID AND
			 VEHICLE_TYPE_PER =   '11334'   -- PPA
			AND  VEHICLE_ID = @VEHICLE_ID
			
		 ELSE
			UPDATE APP_VEHICLES
			SET MULTI_CAR = '11919' -- OTHER CAR ON THIS POLICY
			WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
			 APP_ID = @APP_ID AND  
			 APP_VERSION_ID = @APP_VERSION_ID AND
			 VEHICLE_TYPE_PER =   '11334' -- PPA
			AND  VEHICLE_ID = @VEHICLE_ID
	--    END
	
	     RETURN @CNT

END    




GO

