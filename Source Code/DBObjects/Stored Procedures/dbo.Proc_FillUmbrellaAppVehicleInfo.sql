IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillUmbrellaAppVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillUmbrellaAppVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
Proc Name       : dbo.Proc_FillUmbrellaAppVehicleInfo                
Created by      : Vijay Arora                
Date            : 18-10-2005                
Purpose         : To get the assinged vehicles incase of application from Umbrella                
Revison History :                    
Used In         : Wolverine                    
            
Modified By     : Shafi            
Date            : 06/02/06            
Purpose         : To convert Year Into String              
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
--drop proc Proc_FillUmbrellaAppVehicleInfo 1199,288,1             
CREATE PROCEDURE [dbo].[Proc_FillUmbrellaAppVehicleInfo]                   
(                  
 @CUSTOMER_ID int,                   
 @APP_ID int,                   
 @APP_VERSION_ID int                  
)                  
AS                  
begin                  
                  
declare @AUTO_VEHICLE int        
declare @MOTOR_VEHICLE int        
declare @MOTOR_HOME_VEHICLE int      
      
set @AUTO_VEHICLE = 11956        
set @MOTOR_VEHICLE = 11957        
set @MOTOR_HOME_VEHICLE = 11958      
      
SELECT VEHICLE_ID, convert(varchar,VEHICLE_ID) + ' '  + convert(varchar,isNull(VEHICLE_YEAR,''))  + ' ' + IsNull(MAKE,'')  As MODEL_MAKE FROM APP_UMBRELLA_VEHICLE_INFO                   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID           
AND UPPER(ISNULL(IS_ACTIVE,''))='Y'  AND (VEHICLE_TYPE_PER=@AUTO_VEHICLE OR VEHICLE_TYPE_PER= @MOTOR_HOME_VEHICLE OR MOTORCYCLE_TYPE=@AUTO_VEHICLE OR MOTORCYCLE_TYPE= @MOTOR_HOME_VEHICLE )  
      
      
SELECT VEHICLE_ID, convert(varchar,VEHICLE_ID) + ' '  + convert(varchar,isNull(VEHICLE_YEAR,''))  + ' ' + IsNull(MAKE,'')  As MODEL_MAKE FROM APP_UMBRELLA_VEHICLE_INFO                   
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID           
AND UPPER(ISNULL(IS_ACTIVE,''))='Y'  AND MOTORCYCLE_TYPE=@MOTOR_VEHICLE        
      
SELECT BOAT_ID AS VEHICLE_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS MODEL_MAKE  FROM APP_UMBRELLA_WATERCRAFT_INFO       
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID       
AND UPPER(ISNULL(IS_ACTIVE,''))='Y'           
      
      
      
                  
End


GO

