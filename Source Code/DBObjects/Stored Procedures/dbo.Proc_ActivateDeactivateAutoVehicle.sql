IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateAutoVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateAutoVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                
Proc Name       : dbo.Proc_ActivateDeactivateAutoVehicle        
Created by      : Priya Arora          
Date            : 16/12/2005                          
Purpose         :Activate/ Deactivate vehicle       
Revison History :                
Used In         : Wolverine                
 Modified By : Ashwani    
 Date  : 7 Feb. 06    
 Purpose : Deassign the vehicle from app_driver_details if it's deactivated    
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC dbo.Proc_ActivateDeactivateAutoVehicle                
(                
@CUSTOMER_ID     INT,                
@APP_ID     INT,                
@APP_VERSION_ID     SMALLINT,                
@VEHICLE_ID     SMALLINT,        
@IS_ACTIVE NCHAR(2),
@CALLED_FROM VARCHAR(10)=NULL       
)                
AS                
BEGIN                
        
UPDATE APP_VEHICLES SET IS_ACTIVE=@IS_ACTIVE WHERE        
 CUSTOMER_ID=@CUSTOMER_ID AND         
 APP_ID=@APP_ID AND        
 APP_VERSION_ID=@APP_VERSION_ID AND        
 VEHICLE_ID=@VEHICLE_ID     
---------------------------------------------------------------    
-- DEASSIGN THE VEHICLE FROM APP_DRIVER_DETAILS IF IT'S DEACTIVATED --- ADDED BY ASHWANI ON <7 FEB. 2006 >    
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID     
   AND VEHICLE_ID=@VEHICLE_ID AND UPPER(IS_ACTIVE)='N'  )    
 BEGIN     
  UPDATE APP_DRIVER_DETAILS     
  SET VEHICLE_ID=NULL    
  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID    
 END    

--The following rule has been commented as it will be called from the underwritting page itself
/*IF (UPPER(@CALLED_FROM)='MOT')
begin
	--Call to proc to set the value at gen info table when there are vehicles having amount>30000
	exec  Proc_MotorGreaterAmountRule @customeR_id,@app_id,@app_version_id
end*/
END        
    
      
    
  





GO

