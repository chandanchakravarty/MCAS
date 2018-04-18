IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDriverCountForAssignedVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDriverCountForAssignedVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : dbo.Proc_GetDriverCountForAssignedVehicle                        
Created by      : Vikki                        
Date            : 15/02/2006                        
Purpose     :Insert                        
Revison History :                        
Used In  : Wolverine                        
                        
        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
CREATE PROC Dbo.Proc_GetDriverCountForAssignedVehicle    
(                        
@CUSTOMER_ID     int,                        
@APP_ID     int ,                        
@APP_VERSION_ID     smallint,                        
@VEHICLE_ID     smallint,  
@CALLED_FROM VARCHAR(10)=NULL     
)                        
AS                        
BEGIN                    
IF UPPER(@CALLED_FROM)='PPA'  
BEGIN       
 select customer_id From app_driver_details where     
  customer_id=@customer_id and     
  app_id=@app_id and     
  app_version_id = @app_version_id and    
  vehicle_id = @vehicle_id    
 return @@rowcount    
END  
ELSE IF UPPER(@CALLED_FROM)='WAT'  
BEGIN  
  select customer_id From app_watercraft_driver_details where     
  customer_id=@customer_id and     
  app_id=@app_id and     
  app_version_id = @app_version_id and    
  vehicle_id = @vehicle_id    
 return @@rowcount    
END  
ELSE IF UPPER(@CALLED_FROM)='UMB'  
BEGIN  
  select customer_id From app_umbrella_driver_details where     
  customer_id=@customer_id and     
  app_id=@app_id and     
  app_version_id = @app_version_id and    
  vehicle_id = @vehicle_id    
 return @@rowcount    
END  

END  



GO

