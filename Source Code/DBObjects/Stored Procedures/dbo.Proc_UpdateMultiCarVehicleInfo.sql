IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMultiCarVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMultiCarVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_UpdateMultiCarVehicle                            
Created by      : Praveen kasana                            
Date            : 2/22/2007
Purpose         :                            
Revison History :                            
Used In  	: Wolverine                            
           
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_UpdateMultiCarVehicleInfo                        
CREATE PROC dbo.Proc_UpdateMultiCarVehicleInfo   
(                            
@CUSTOMER_ID     int,                            
@APP_ID     int ,                            
@APP_VERSION_ID     smallint
)                            
AS          
BEGIN                        
 declare @CNT INT, @MC INT  ,@MULTI_CAR int

 SELECT CUSTOMER_ID FROM APP_VEHICLES with(NOLOCK)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID AND  
  IS_ACTIVE = 'Y' AND  
  VEHICLE_TYPE_PER = '11334' 

  set @MC = @@ROWCOUNT
  
   
 /* SELECT CUSTOMER_ID FROM APP_VEHICLES with(NOLOCK)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  APP_ID = @APP_ID AND    
  APP_VERSION_ID = @APP_VERSION_ID AND  
  IS_ACTIVE = 'Y' AND  
  VEHICLE_TYPE_PER = '11334' and MULTI_CAR != '11919' -- OTHER CAR ON THIS POLICY  */
-- AND MULTI_CAR <>  '11920'  -- OTHER POLICY WITH WOLVERINE  
  
 --SET @CNT = @@ROWCOUNT  
    
  --IF (@CNT = 1 or @MC = 1) 
IF (@MC = 1) 
BEGIN
	SELECT @MULTI_CAR = MULTI_CAR FROM APP_VEHICLES WITH(NOLOCK)    
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
	APP_ID = @APP_ID AND    
	APP_VERSION_ID = @APP_VERSION_ID AND  
	IS_ACTIVE = 'Y' AND  
	VEHICLE_TYPE_PER = '11334'
	
	IF(@MULTI_CAR = 11919) --Other Policy
	BEGIN
		  UPDATE APP_VEHICLES  
		  SET MULTI_CAR = '11919'  
		  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
		   APP_ID = @APP_ID AND    
		   APP_VERSION_ID = @APP_VERSION_ID AND  
		   VEHICLE_TYPE_PER =   '11334'   -- PPA  
	END
	ELSE
	BEGIN
		UPDATE APP_VEHICLES  
		   SET MULTI_CAR = '11918' -- NOT APPLICABLE  
		   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
		    APP_ID = @APP_ID AND    
		    APP_VERSION_ID = @APP_VERSION_ID AND  
		    VEHICLE_TYPE_PER =   '11334'   -- PPA  
	END
	
	   
	END  
END      
  
  







GO

